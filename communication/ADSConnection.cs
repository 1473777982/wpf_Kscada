using common;
using common.helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.Ads;
using TwinCAT.Ads.SumCommand;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;
using TwinCAT.TypeSystem.Generic;

namespace communication
{
    /// <summary>
    /// 驱动通讯组 每组最大1万个标签
    /// </summary>
    public sealed class ADSConnection : IConnection
    {


        #region 变量
        DateTime m_stopTime = DateTime.Now;
        private Dictionary<string, runTag> aliveTags = new Dictionary<string, runTag>(StringComparer.OrdinalIgnoreCase);
        private TcAdsClient client = null;
        private List<runTag> writeTags = new List<runTag>();
        int timeout = 60;
        int cyctime = 500;
        string serverAdr = "";
        int port = 5100;
        string tcpName = "";
        Timer timer = null;
        volatile bool m_ready = false; //rename
        volatile bool m_inited;  //rename
        Thread readthread = null;
        Device device;
        int state = -1;

        #endregion

        SymbolCollection symbols = new SymbolCollection();
        ReadOnlySymbolCollection allSymbols;

        /// <summary>
        ///  初始化连接参数和变量字典
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="channels"></param>
        public ADSConnection(Device device)
        {
            this.device = device;
            tcpName = device.DeviceName;
            timeout = device.Timeout;
            cyctime = device.CycTime;
            if (timeout != 0)
            {
                if (timeout < 30) timeout = 30;
                if (timeout > 300) timeout = 300;
            }
            this.port = device.TCPPort;
            this.serverAdr = device.IPAdress;

            foreach (runTag rt in device.runTags)
            {
                if (!string.IsNullOrEmpty(rt.address))
                {
                    if (!aliveTags.ContainsKey(rt.address))
                    {
                        aliveTags.Add(rt.address, rt);
                        rt.SetConnectGroup(this);
                    }
                }
            }
            Start();
            //if (plug.DebugMode)
            //    plug.AddDebugInfo(tcpName + " 启动线程");
        }




        /// <summary>
        /// 连接PLC 启动read线程和write定时器
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            bool result = false;
            readthread = null;
            bool c = ConnectToServer();
            if (c)
            {
                m_ready = true;

                try
                {
                    ISymbolLoader loader = SymbolLoaderFactory.Create(client, SymbolLoaderSettings.Default);
                    allSymbols = loader.Symbols;
                    if (allSymbols.Count > 0)
                    {
                        foreach (baseRunTag tag in aliveTags.Values)
                        {
                            ISymbol symbol;
                            if (allSymbols.TryGetInstance(tag.address, out symbol))
                            {
                                symbols.Add(symbol);
                                tag.handle = (Symbol)symbol;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    logHepler.addLog_common("ADSConnection " + tcpName + " Ads Error:\"" + ex.Message);
                }


                readthread = new Thread(new ThreadStart(AdsVariablesThread));
                readthread.IsBackground = true;
                readthread.Start();
                //timer = new Timer(TimerCallback, null, 2000, 2000);
                result = true;
            }
            else
            {
                logHepler.addLog_common("ADS:连接PLC失败" + serverAdr + port);
            }


            return result;
        }

        public  AdsErrorCode adsErrorCode;
        public  StateInfo stateInfo1;

        /// <summary>
        /// 连接PLC  拿到句柄
        /// </summary>
        /// <returns></returns>
        bool ConnectToServer()
        {
            bool result = false;
            if (serverAdr != "" && port > 0)
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
                client = new TcAdsClient();
                client.Timeout = 2000;
                try
                {
                    client.Connect(serverAdr, port);
                    adsErrorCode = client.TryReadState(out stateInfo1);
                    if (adsErrorCode == AdsErrorCode.NoError)
                    {
                        result = true;
                    }
                   
                }
                catch (Exception ex)
                {
                    logHepler.addLog_common("ADSConnection " + tcpName + " " + serverAdr + " " + port.ToString() + ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>      
        void AdsVariablesThread()
        {
            while (m_ready)
            {
                if (client.IsConnected)
                {
                    object[] array_values = null;


                    if (symbols.Count != 0)
                    {
                        SumSymbolRead readCommand = new SumSymbolRead(client, symbols);
                        try
                        {
                            array_values = readCommand.Read();
                        }
                        catch (Exception)
                        {

                        }
                    }

                    if (array_values != null && array_values.Length != 0 && array_values.Length == symbols.Count)
                    {
                       

                        for (int i = 0; i < array_values.Length; i++)
                        {
                            try
                            {
                                runTag tag;
                                if (array_values[i] != null && !(array_values[i] is Array) && this.aliveTags.TryGetValue(symbols[i].InstancePath, out tag))
                                {
                                    tag.refresh(array_values[i], true);
                                }
                            }
                            catch (Exception)
                            {
                            }
                          
                        }                        
                    }
                    else
                    {
                        foreach (var tag in aliveTags.Values)
                        {
                            tag.refresh(null,false);
                        }
                    }
                }
                else
                {
                    m_ready = false;
                    var c = ConnectToServer();
                    if (c)
                    {
                        m_ready = true;

                        try
                        {
                            ISymbolLoader loader = SymbolLoaderFactory.Create(client, SymbolLoaderSettings.Default);
                            allSymbols = loader.Symbols;
                            if (allSymbols.Count > 0)
                            {
                                foreach (baseRunTag tag in aliveTags.Values)
                                {
                                    ISymbol symbol;
                                    if (allSymbols.TryGetInstance(tag.address, out symbol))
                                    {
                                        symbols.Add(symbol);
                                        tag.handle = (Symbol)symbol;
                                    }
                                }
                            }

                        }
                        catch 
                        {
                            //logHepler.addLog_common("ADSConnection " + tcpName + " Ads Error:\"" + ex.Message);
                        }
                    }
                        
                }
                if (writeTags.Count > 0)
                {
                    WriteToDevice();
                }

                Thread.Sleep(this.cyctime);
            }
        }

        /// <summary>
        /// 写值函数
        public bool WriteValue(runTag rt)
        {
            lock (writeTags)
            {
                if (!writeTags.Contains(rt))
                    writeTags.Add(rt);
            }
            return true;
        }
        /// <summary>
        /// 写标签
        /// </summary>
        bool WriteToDevice()
        {
            bool result = true;
            if (client != null && client.IsConnected)
            {
                #region Write
                lock (writeTags)
                {
                    if (writeTags != null && writeTags.Count > 0)
                    {
                        SymbolCollection symbolCollection = new SymbolCollection();
                        List<object> writevalues = new List<object>();
                        for (int i = 0; i < writeTags.Count; i++)
                        {
                            if (writeTags[i].WriteValue != null)
                            {
                                symbolCollection.Add(writeTags[i].handle);
                                writevalues.Add(writeTags[i].WriteValue);
                            }


                            if (writevalues.Count > 0)
                            {
                                SumSymbolWrite sumSymbolWrite = new SumSymbolWrite(this.client, symbolCollection);
                                try
                                {
                                    sumSymbolWrite.Write(writevalues.ToArray());
                                }
                                catch (Exception ex)
                                {
                                    logHepler.addLog_common("ads writeERR" + ex.Message);
                                }
                            }


                        }
                    }
                    writeTags.Clear();
                }
                #endregion
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (readthread != null)
            {
                readthread.Abort();
                readthread = null;
                if (timer != null)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timer.Dispose();
                    timer = null;
                }
                lock (client)
                {
                    client.Disconnect();
                    client.Dispose();
                }
                m_ready = false;
                m_stopTime = DateTime.Now;
            }
        }

        public bool CheckStoped()
        {
            bool result = false;
            if (readthread != null)
            {
                if (readthread.IsAlive)
                {
                    if ((DateTime.Now - m_stopTime).TotalSeconds > 30)
                    {
                        readthread.Abort();
                        readthread = null;
                        lock (client)
                        {
                            if (client != null)
                            {
                                client.Disconnect();
                                client.Dispose();
                            }
                        }
                        result = true;
                    }
                }
                else
                {
                    readthread = null;
                    result = true;
                }
            }
            else
                result = true;
            return result;
        }


        /// <summary>
        /// 更新故障标签
        /// </summary>
        private void SetChannelToBad()
        {
            foreach (baseRunTag ch in aliveTags.Values)
            {
                if (ch.flagState != false)
                    ch.flagState = false;
            }
        }
    }
}
