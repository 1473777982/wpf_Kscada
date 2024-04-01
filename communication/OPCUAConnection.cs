using common;
using common.helper;
using Opc.Ua;
using Opc.Ua.Client;
using OpcUaHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace communication
{
    public class OPCUAConnection : IConnection
    {
        #region 变量
        DateTime m_stopTime = DateTime.Now;
        private Dictionary<string, runTag> aliveTags = new Dictionary<string, runTag>(StringComparer.OrdinalIgnoreCase);
        private OpcUaClient m_OpcUaClient = null;
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
        bool opcMonitor;

        #endregion

        List<string> subNodeIds;
        List<NodeId> nodeIds;

        public OPCUAConnection(Device device)
        {

            this.device = device;
            tcpName = device.DeviceName;
            timeout = device.Timeout;
            cyctime = device.CycTime;
            this.opcMonitor = device.opcMonitor;
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
        public bool Start()
        {
            bool result = false;
            readthread = null;
            ConnectToServer();
            if (m_OpcUaClient!= null)
            {
                if (m_OpcUaClient.Connected)
                {
                    m_ready = true;

                    //序列化
                    try
                    {
                        m_OpcUaClient?.RemoveAllSubscription();
                        subNodeIds = new List<string>();
                        foreach (var item in aliveTags.Values)
                        {
                            subNodeIds.Add(item.address);
                        }
                        if (opcMonitor)
                        {
                            m_OpcUaClient.AddSubscription(device.DeviceName, subNodeIds.ToArray(), SubCallBack);
                        }
                        else
                        {
                            nodeIds = new List<NodeId>();
                            foreach (var item in aliveTags.Values)
                            {
                                nodeIds.Add(new NodeId(item.address));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        logHepler.addLog_common("OPCUConnection " + tcpName + ex.Message);
                    }


                    readthread = new Thread(new ThreadStart(OPCUAVariablesThread));
                    readthread.IsBackground = true;
                    readthread.Start();
                    //timer = new Timer(TimerCallback, null, 2000, 2000);
                    result = true;
                }
                else
                {
                    logHepler.addLog_common("OPCUA:连接PLC失败" + serverAdr + port);
                }

            }


            return result;
        }

        private void ConnectToServer()
        {
            if (serverAdr != "")
            {
                string pattern = @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(\d{1,5})";

                Match match = Regex.Match(serverAdr, pattern);

                if (match.Success)
                {
                    string ipAddress = match.Groups[1].Value;
                    string port = match.Groups[2].Value;
                    if (communicat.CheckConnect(ipAddress, int.Parse(port)))
                    {
                        if (m_OpcUaClient != null)
                        {
                            m_OpcUaClient.Disconnect();
                            m_OpcUaClient = null;
                        }
                        m_OpcUaClient = new OpcUaClient();
                        m_OpcUaClient.UserIdentity = new UserIdentity(new AnonymousIdentityToken());
                        m_OpcUaClient.OpcStatusChange += M_OpcUaClient_OpcStatusChanged;
                        connect();
                    }
                }
                    
            }
            
        }
        private void matchIP(string input)
        {
            string pattern = @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(\d{1,5})";

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string ipAddress = match.Groups[1].Value;
                string port = match.Groups[2].Value;
            }
        }
        private async void connect()
        {
            try
            {
                await m_OpcUaClient.ConnectServer(serverAdr);

            }
            catch (Exception ex)
            {
                logHepler.addLog_common("OPCUAConnection " + tcpName + " " + serverAdr + " " + port.ToString() + ex.Message);
            }
        }
        void OPCUAVariablesThread()
        {
            while (m_ready)
            {
                if (m_OpcUaClient.Connected)
                {
                    if (!opcMonitor)
                    {
                        try
                        {
                            // dataValues按顺序定义的值，每个值里面需要重新判断类型
                            List<DataValue> dataValues = m_OpcUaClient.ReadNodes(nodeIds.ToArray());
                            // 然后遍历你的数据信息
                            for (int i = 0; i < dataValues.Count; i++)
                            {
                                object value = dataValues[i].Value;
                                var flg = dataValues[i].StatusCode.ToString();
                                aliveTags[nodeIds[i].ToString()].refresh(value, flg.Equals("Good"));
                            }

                        }
                        catch (Exception)
                        {

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
        bool WriteToDevice()
        {
            bool result = true;
            if (m_OpcUaClient.Connected)
            {
                #region Write

               

                lock (writeTags)
                {
                    if (writeTags != null && writeTags.Count > 0)
                    {
                        List<string> tags = new List<string>(); 
                        List<object> writevalues = new List<object>();
                        for (int i = 0; i < writeTags.Count; i++)
                        {
                            if (writeTags[i].WriteValue != null)
                            {
                                tags.Add(writeTags[i].address);
                                writevalues.Add(writeTags[i].WriteValue);
                            }


                            if (writevalues.Count > 0)
                            {
                               
                                try
                                {
                                    m_OpcUaClient.WriteNodes(tags.ToArray(),writevalues.ToArray());
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
        /// OPC 客户端的状态变化后的消息提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_OpcUaClient_OpcStatusChanged(object sender, OpcUaStatusEventArgs e)
        {
            if (m_ready)
            {
                if (!m_OpcUaClient.Connected)
                {            
                    SetChannelToBad();
                    logHepler.addLog_common(e.ToString());
                    connect();
                }
            }

           
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

        private void SubCallBack(string key, MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs eventArgs)
        {
            MonitoredItemNotification notification = eventArgs.NotificationValue as MonitoredItemNotification;
            string nodeId = monitoredItem.StartNodeId.ToString();

            int index = subNodeIds.IndexOf(nodeId);
            if (index >= 0)
            {
                try
                {
                    var flg = notification.Value.StatusCode.ToString();
                    aliveTags[nodeId].refresh(notification.Value.WrappedValue.Value, flg.Equals("Good"));
                }
                catch (Exception)
                {
                }
            }
        }
        public bool WriteValue(runTag rt)
        {
            lock (writeTags)
            {
                if (!writeTags.Contains(rt))
                    writeTags.Add(rt);
            }
            return true;
        }
        public void Stop()
        {
            m_OpcUaClient?.RemoveAllSubscription();
            m_OpcUaClient.Disconnect();
        }

        public bool CheckStoped()
        {
            if (m_OpcUaClient != null)
            {
                m_OpcUaClient?.RemoveAllSubscription();
                m_OpcUaClient.Disconnect();
            }
            return true;
        }
    }
}
