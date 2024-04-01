using common;
using common.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.XPath;
using TwinCAT.Ads.TypeSystem;
using DataType = common.DataType;

namespace communication
{
    public class InovanceConnection : IConnection
    {


        #region 变量
        DateTime m_stopTime = DateTime.Now;
        private Dictionary<string, runTag> aliveTags = new Dictionary<string, runTag>(StringComparer.OrdinalIgnoreCase);
        private List<runTag> writeTags = new List<runTag>();
        volatile bool m_ready;
        int timeout = 60;
        int cyctime = 500;
        string serverAdr = "";
        int port = 5100;
        int nNetId;
        string tcpName = "";
        Device device;
        int groupCapacity_Q = 200; // Q分组容量
        int groupCapacity_M = 60; // M分组容量

        #endregion

        /// <summary>
        ///  初始化连接参数和变量字典
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="channels"></param>
        public InovanceConnection( Device device, bool m_ready)
        {
            this.device = device;
            tcpName = device.DeviceName;
            timeout = device.Timeout;
            cyctime = device.CycTime;
            nNetId = device.NetID;
            this.m_ready = m_ready;
            if (timeout != 0)
            {
                if (timeout < 30) timeout = 30;
                if (timeout > 300) timeout = 300;
            }
            this.port = device.TCPPort;
            this.serverAdr = device.IPAdress;

            if (device.runTags.Count > 0)
            {
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
                sortTags();
                Start();

            }

        }


        List<Dictionary<string, runTag>> list_tags_sorted_Q;
        List<Dictionary<string, runTag>> list_tags_sorted_M;
        Dictionary<string, runTag> dic_Q;
        Dictionary<string, runTag> dic_M;
        List<Thread> threads = new List<Thread>();
       

        //阻塞读取线程  另一种方法
        //ManualResetEventSlim blockEvent = new ManualResetEventSlim(false);

        public void sortTags()
        {

            switch (device.CpuType)
            {
                case "FX3U":
                    FX3U_sort();
                    break;
                case "FX5U":
                    FX5U_sort();
                    break;
                case "AM":
                    AM_sort();
                    break;
                default:
                    break;
            }



        }
        //3U 型 PLC分组
        public void FX3U_sort()
        {

        }
        //5U 型 PLC分组
        public void FX5U_sort()
        {

        }
        //AM 型 PLC分组
        public void AM_sort()
        {

            list_tags_sorted_Q = new List<Dictionary<string, runTag>>();
            list_tags_sorted_M = new List<Dictionary<string, runTag>>();
            dic_Q = new Dictionary<string, runTag>();
            dic_M = new Dictionary<string, runTag>();
            int Q_min = 0, Q_max = 0, M_min = 0, M_max = 0;
            int v1 = 0;
            int v2 = 0;
            SoftElemType softElemType = SoftElemType.AM_QX;

            //获得range
            foreach (var item in aliveTags)
            {
                var t = item.Value.address.Substring(0, 2).ToLower();
                var v = item.Value.address.Substring(2).ToLower();

                if (item.Value.address.Substring(0, 1).ToLower() == "q")
                {
                    #region  Q

                    dic_Q.Add(item.Value.address, item.Value);
                    switch (t) // v1:实际Q区地址，v2:乘10后通讯用地址
                    {
                        case "qx":
                            v1 = Convert.ToInt32(v.Substring(0, v.Length - 2));
                            v2 = Convert.ToInt32(v.Substring(0, v.Length - 2)) * 10 + Convert.ToInt32(v.Substring(v.Length - 1, 1));
                            softElemType = SoftElemType.AM_QX;
                            break;
                        case "qb":
                            v1 = Convert.ToInt32(v);
                            v2 = Convert.ToInt32(v) * 10;
                            softElemType = SoftElemType.AM_QB;
                            break;
                        case "qw":
                            v1 = Convert.ToInt32(v) * 2;
                            v2 = Convert.ToInt32(v) * 20;
                            softElemType = SoftElemType.AM_QW;
                            break;
                        case "qd":
                            v1 = Convert.ToInt32(v) * 4;
                            v2 = Convert.ToInt32(v) * 40;
                            softElemType = SoftElemType.AM_QD;
                            break;
                    }
                    if (v1 > Q_max)
                    {
                        Q_max = v1;
                    }
                    else if (v1 < Q_min)
                    {
                        Q_min = v1;
                    }
                    #endregion
                }
                else if (item.Value.address.Substring(0, 1).ToLower() == "m")
                {
                    #region  M

                    dic_M.Add(item.Value.address, item.Value);
                    switch (t)//v1:实际M区地址，v2:处理后通讯用地址
                    {
                        case "mx":
                            v1 = Convert.ToInt32(v.Substring(0, v.Length - 2)) / 2;
                            v2 = Convert.ToInt32(v.Substring(0, v.Length - 2)) * 10 + Convert.ToInt32(v.Substring(v.Length - 1, 1));
                            softElemType = SoftElemType.AM_MX;
                            break;
                        case "mb":
                            v1 = Convert.ToInt32(v) / 2;
                            v2 = Convert.ToInt32(v);
                            softElemType = SoftElemType.AM_MB;
                            break;
                        case "mw":
                            v1 = Convert.ToInt32(v);
                            v2 = Convert.ToInt32(v);
                            softElemType = SoftElemType.AM_MW;
                            break;
                        case "md":
                            v1 = Convert.ToInt32(v) * 2;
                            v2 = Convert.ToInt32(v) * 2;
                            softElemType = SoftElemType.AM_MD;
                            break;
                        case "ml":
                            v1 = Convert.ToInt32(v) * 4;
                            v2 = Convert.ToInt32(v) * 4;
                            softElemType = SoftElemType.AM_MD;
                            break;
                    }
                    if (v1 > M_max)
                    {
                        M_max = v1;
                    }
                    else if (v1 < M_min)
                    {
                        M_min = v1;
                    }
                    #endregion

                }
                item.Value.elemType = softElemType;
                item.Value.nRealAddr = v1;
                item.Value.nModAddr = v2;
            }
            #region   //Q类型分组   按照200个byte分组
            int current_Q = 0;
            var start_Q = (Q_min / groupCapacity_Q) * groupCapacity_Q;
            var end_Q = (Q_max / groupCapacity_Q) * groupCapacity_Q + groupCapacity_Q;
            int range_Q = end_Q / groupCapacity_Q - start_Q / groupCapacity_Q;



            if (range_Q > 0)
            {
                for (int i = 0; i < range_Q; i++)
                {
                    list_tags_sorted_Q.Add(new Dictionary<string, runTag>());
                }
            }
            else
            {
                list_tags_sorted_Q.Add(new Dictionary<string, runTag>());
            }

            foreach (var item in dic_Q)
            {
                current_Q = item.Value.nRealAddr;
                int index = current_Q / groupCapacity_Q - start_Q / groupCapacity_Q;
                list_tags_sorted_Q[index].Add(item.Value.address, item.Value);
            }

            #endregion

            #region  //M类型分组

            int current_M = 0;
            var start_M = (M_min / groupCapacity_M) * groupCapacity_M;
            var end_M = (M_max / groupCapacity_M) * groupCapacity_M + groupCapacity_M;
            int range_M = end_M / groupCapacity_M - start_M / groupCapacity_M;



            if (range_M > 0)
            {
                for (int i = 0; i < range_M; i++)
                {
                    list_tags_sorted_M.Add(new Dictionary<string, runTag>());
                }
            }
            else
            {
                list_tags_sorted_M.Add(new Dictionary<string, runTag>());
            }

            foreach (var item in dic_M)
            {

                current_M = item.Value.nRealAddr;
                int index = current_M / groupCapacity_M - start_M / groupCapacity_M;
                list_tags_sorted_M[index].Add(item.Value.address, item.Value);

            }
            #endregion



        }



        public class ThreadParams
        {
            public Dictionary<string, runTag> dic { get; }
            public SoftElemType elemType { get; }

            public ThreadParams(Dictionary<string, runTag> dic, SoftElemType elemType)
            {
                this.dic = dic;
                this.elemType = elemType;
            }
        }




        /// <summary>
        /// 连接PLC 启动read线程和write定时器
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            bool result = false;
            if (m_ready)
            {
                if (list_tags_sorted_Q.Count > 0)
                {
                    for (int i = 0; i < list_tags_sorted_Q.Count; i++)
                    {
                        if (list_tags_sorted_Q[i].Count > 0)
                        {
                            Thread thread = new Thread(new ParameterizedThreadStart(CommunicationThread));
                            thread.Name = list_tags_sorted_Q[i].First().Key;
                            threads.Add(thread);
                            thread.Start(new ThreadParams(list_tags_sorted_Q[i], SoftElemType.ELEM_QX));

                            //tasks.Add(Task.Run(() => CommunicationThread(list_tags_sorted_Q[i], SoftElemType.ELEM_QX)));
                            //Thread.Sleep(1000);
                        }
                    }
                }
                if (list_tags_sorted_M.Count > 0)
                {
                    for (int i = 0; i < list_tags_sorted_M.Count; i++)
                    {
                        if (list_tags_sorted_M[i].Count > 0)
                        {
                            Thread thread = new Thread(new ParameterizedThreadStart(CommunicationThread));
                            thread.Name = list_tags_sorted_M[i].First().Key;
                            threads.Add(thread);
                            thread.Start(new ThreadParams(list_tags_sorted_M[i], SoftElemType.ELEM_MW));

                            //tasks.Add(Task.Run(() => CommunicationThread(list_tags_sorted_M[i], SoftElemType.ELEM_MW)));
                            //Thread.Sleep(1000);
                        }

                    }
                }
            }

            if (m_ready)
            {
                Thread thread = new Thread(() =>
                {
                    WriteToDevice();
                });
                thread.Name = "writeINOVANCE";
                threads.Add(thread);
                thread.Start();

                // tasks.Add(Task.Factory.StartNew(() => WriteToDevice()));
            }

            result = true;
            // 等待所有线程完成
            //Task.WaitAll(tasks.ToArray());

            return result;

        }

        private void CommunicationThread(object parameters)
        {
            ThreadParams threadParams = (ThreadParams)parameters;
            var VARdic = threadParams.dic;
            var VARelemType = threadParams.elemType;

            //logHepler.addLog_common("threadstart" + VARdic.First().Value.DeviceName);
            // 创建一个字节数组来保存读取到的数据
            byte[] pBuf = new byte[2000];
            int nCount = 0, nStartAddr = 0;
            while (m_ready)
            {
                // 阻塞其他线程
                Monitor.Enter(communicat.lockObject);  //作用同lock

                //if (writeTags.Count > 0)
                //{
                //    blockEvent.Wait();
                //}
                //else
                //{
                //    blockEvent.Set();
                //}

                try
                {
                    if (VARelemType == SoftElemType.ELEM_MW)
                    {

                        nCount = groupCapacity_M * 2;
                        nStartAddr = VARdic.First().Value.nRealAddr / groupCapacity_M * groupCapacity_M;
                        int nRet = communicat.Am600_Read_Soft_Elem(VARelemType, nStartAddr, nCount, pBuf, nNetId);
                        if (nRet != 1)
                        {
                            logHepler.addLog_common("INOVANCE readERR:"+ VARdic.First().Key + DateTime.Now);
                            //if (VARdic.First().Value.flagState)
                            //{
                            //    foreach (var item in VARdic)
                            //    {
                            //        if (!object.Equals(item.Value.value, null))
                            //        {
                            //            item.Value.DoUpdate(null, false);
                            //        }

                            //    }
                            //}
                               
                        }
                        else
                        {
                            foreach (var item in VARdic)
                            {
                                int step = (item.Value.nRealAddr - nStartAddr) * 2;
                                //try
                                //{
                                if (item.Value.tagType == DataType.BOOL)
                                {
                                    if (item.Value.elemType == SoftElemType.AM_MX)
                                    {
                                        var p = pBuf[step + item.Value.nModAddr / 10 % 2];
                                        int bit = Convert.ToInt16(item.Value.address.Substring(item.Value.address.Length - 1, 1));
                                        var atlast = (p >> (bit)) & 1;
                                        if (atlast == 1)
                                        {
                                            item.Value.refresh(true, true);
                                        }
                                        else
                                        {
                                            item.Value.refresh(false, true);
                                        }
                                    }
                                    if (item.Value.elemType == SoftElemType.AM_MB)
                                    {
                                        byte[] databuf = new byte[1] { 0 };
                                        databuf[0] = pBuf[step + item.Value.nModAddr % 2];
                                        item.Value.refresh(BitConverter.ToBoolean(databuf, 0), true);
                                    }
                                }
                                else if (item.Value.tagType == common.DataType.BYTE)
                                {
                                    byte[] databuf = new byte[1] { 0 };
                                    databuf[0] = pBuf[step + item.Value.nModAddr % 2];
                                    item.Value.refresh(databuf[0], true);
                                }
                                else if (item.Value.tagType == DataType.INT)
                                {
                                    byte[] databuf = new byte[2] { 0, 0 };
                                    databuf[0] = pBuf[step];
                                    databuf[1] = pBuf[step + 1];
                                    item.Value.refresh(BitConverter.ToInt16(databuf, 0), true);
                                }
                                else if (item.Value.tagType == DataType.WORD || item.Value.tagType == DataType.UINT)
                                {
                                    byte[] databuf = new byte[2] { 0, 0 };
                                    databuf[0] = pBuf[step];
                                    databuf[1] = pBuf[step + 1];
                                    item.Value.refresh(BitConverter.ToUInt16(databuf, 0), true);
                                }
                                else if (item.Value.tagType == DataType.DINT)
                                {
                                    byte[] databuf = new byte[4] { 0, 0, 0, 0 };
                                    databuf[0] = pBuf[step];
                                    databuf[1] = pBuf[step + 1];
                                    databuf[2] = pBuf[step + 2];
                                    databuf[3] = pBuf[step + 3];
                                    item.Value.refresh(BitConverter.ToInt32(databuf, 0), true);
                                }
                                else if (item.Value.tagType == DataType.REAL)
                                {
                                    byte[] databuf = new byte[4] { 0, 0, 0, 0 };
                                    databuf[0] = pBuf[step];
                                    databuf[1] = pBuf[step + 1];
                                    databuf[2] = pBuf[step + 2];
                                    databuf[3] = pBuf[step + 3];
                                    item.Value.refresh(BitConverter.ToSingle(databuf, 0), true);
                                }
                                else if (item.Value.tagType == DataType.LREAL)
                                {
                                    byte[] databuf = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                                    databuf[0] = pBuf[step];
                                    databuf[1] = pBuf[step + 1];
                                    databuf[2] = pBuf[step + 2];
                                    databuf[3] = pBuf[step + 3];
                                    databuf[4] = pBuf[step + 4];
                                    databuf[5] = pBuf[step + 5];
                                    databuf[6] = pBuf[step + 6];
                                    databuf[7] = pBuf[step + 7];
                                    item.Value.refresh(BitConverter.ToDouble(databuf, 0), true);
                                }
                                else if (item.Value.tagType == DataType.STRING)
                                {

                                }
                                //}
                                //catch (Exception ex)
                                //{
                                //    item.Value.flagState = false;
                                //    logHepler.addLog_common("INOVANCE Error:\"" + ex.Message);
                                //}

                            }
                        }

                    }
                    if (VARelemType == SoftElemType.ELEM_QX)
                    {
                        nCount = groupCapacity_Q * 8;
                        nStartAddr = VARdic.First().Value.nRealAddr / groupCapacity_Q * groupCapacity_Q * 10;


                        int nRet = communicat.Am600_Read_Soft_Elem(VARelemType, nStartAddr, nCount, pBuf, nNetId);
                        if (nRet != 1)
                        {
                            logHepler.addLog_common("INOVANCE readERR:" + VARdic.First().Key + DateTime.Now);
                            //if (VARdic.First().Value.flagState)
                            //{
                            //    foreach (var item in VARdic)
                            //    {
                            //        if (!object.Equals(item.Value.value, null))
                            //        {
                            //            item.Value.DoUpdate(null, false);
                            //        }
                            //    }
                            //}
                           
                        }
                        else
                        {
                            foreach (var item in VARdic)
                            {
                                //try
                                //{
                                if (item.Value.tagType == DataType.BOOL)
                                {
                                    if (item.Value.address.ToLower().Substring(0, 2) == "qx")
                                    {
                                        int bit = Convert.ToInt16(item.Value.address.Substring(item.Value.address.Length - 1, 1));
                                        byte[] databuf = new byte[1] { 0 };
                                        databuf[0] = pBuf[(item.Value.nRealAddr - nStartAddr / 10) * 8 + bit];
                                        item.Value.refresh(BitConverter.ToBoolean(databuf, 0), true);
                                    }
                                }
                                //}
                                //catch (Exception ex)
                                //{
                                //    item.Value.flagState = false;
                                //    logHepler.addLog_common("INOVANCE Error:\"" + ex.Message);
                                //}
                            }
                        }
                    }
                    
                }
                catch
                {

                }
                finally
                {
                    // 释放锁
                    Monitor.Exit(communicat.lockObject);
                }

                // 延时一段时间后继续下一次读取
                Thread.Sleep(cyctime);
            }


        }


        /// <summary>
        /// 连接PLC 
        /// </summary>
        /// <returns></returns>
        bool ConnectToServer()
        {
            bool result = false;
            if (serverAdr != "" && port > 0)
            {
                result = communicat.Init_ETH_String(serverAdr, nNetId, port);
                if (result == true)
                {
                    m_ready = true;
                    result = true;
                }
                else
                {
                    logHepler.addLog_common("INOVANCE " + tcpName + " " + serverAdr + " " + port.ToString() + "连接失败");
                    result = false;
                }

            }
            return result;
        }


        /// <summary>
        /// 写标签
        /// </summary>
        bool WriteToDevice()
        {
            bool result = false;
            SoftElemType softElemType = SoftElemType.AM_QX;
            int nStart = 0;
            int nCount = 0;
            byte[] pBuf = new byte[16];
            byte[] dataBuf;
            while (m_ready)
            {

                #region Write
                lock (writeTags)
                {
                    if (writeTags != null && writeTags.Count > 0)
                    {
                        // 阻塞其他线程
                        Monitor.Enter(communicat.lockObject);
                        try
                        {
                            for (int i = 0; i < writeTags.Count; i++)
                            {
                                //int nRet = Am600_Write_Soft_Elem(elemType, nStartAddr, nCount, pValue, nNetId);
                                //根据地址和类型写入
                                //是否要在runtag结构中添加xieru地址参数，不用每次都判断地址提高效率
                                if (writeTags[i].WriteValue.GetType() == typeof(Boolean) || writeTags[i].WriteValue.GetType() == typeof(byte))
                                {
                                    switch (writeTags[i].elemType)
                                    {
                                        case SoftElemType.AM_QX:
                                            softElemType = SoftElemType.ELEM_QX;
                                            nStart = writeTags[i].nModAddr;
                                            nCount = 1;
                                            pBuf = new byte[1] { 0 };
                                            dataBuf = BitConverter.GetBytes(Convert.ToBoolean(writeTags[i].WriteValue));
                                            pBuf[0] = dataBuf[0];
                                            break;
                                        case SoftElemType.AM_MX:
                                            softElemType = SoftElemType.ELEM_MW;
                                            nStart = writeTags[i].nModAddr / 10 / 2;
                                            nCount = 2;
                                            pBuf = new byte[2] { 0, 0 };
                                            int Ret = communicat.Am600_Read_Soft_Elem(softElemType, nStart, 1, pBuf, nNetId);
                                            if (Ret == 1)
                                            {
                                                if (writeTags[i].nModAddr /10 % 2 == 0)
                                                {
                                                    pBuf[0] = s_SetBit(pBuf[0], 7 - writeTags[i].nModAddr % 10, Convert.ToBoolean(writeTags[i].WriteValue));
                                                }
                                                else
                                                {
                                                    pBuf[1] = s_SetBit(pBuf[1], 7 - writeTags[i].nModAddr % 10, Convert.ToBoolean(writeTags[i].WriteValue));
                                                }
                                            }
                                            break;
                                        case SoftElemType.AM_MB:
                                            softElemType = SoftElemType.ELEM_MW;
                                            nStart = writeTags[i].nModAddr / 2;
                                            nCount = 2;
                                            pBuf = new byte[2] { 0, 0 };
                                            Ret = communicat.Am600_Read_Soft_Elem(softElemType, nStart, 1, pBuf, nNetId);
                                            if (Ret == 1)
                                            {
                                                if (writeTags[i].nModAddr % 2 == 0)
                                                {
                                                    if (writeTags[i].tagType == DataType.BYTE)
                                                    {
                                                        pBuf[0] = BitConverter.GetBytes(Convert.ToByte(writeTags[i].WriteValue))[0];
                                                    }
                                                    else
                                                    {
                                                        pBuf[0] = BitConverter.GetBytes(Convert.ToBoolean(writeTags[i].WriteValue))[0];
                                                    }

                                                }
                                                else
                                                {
                                                    if (writeTags[i].tagType == DataType.BYTE)
                                                    {
                                                        pBuf[1] = BitConverter.GetBytes(Convert.ToByte(writeTags[i].WriteValue))[0];
                                                    }
                                                    else
                                                    {
                                                        pBuf[1] = BitConverter.GetBytes(Convert.ToBoolean(writeTags[i].WriteValue))[0];
                                                    }
                                                }
                                            }
                                            break;
                                    }

                                }
                                else
                                {
                                    switch (writeTags[i].elemType)
                                    {
                                        case SoftElemType.AM_MW:
                                            softElemType = SoftElemType.ELEM_MW;
                                            nStart = writeTags[i].nModAddr;
                                            nCount = 2;
                                            pBuf = new byte[2] { 0, 0 };
                                            if (writeTags[i].tagType == DataType.INT)
                                            {
                                                pBuf = BitConverter.GetBytes(Convert.ToInt16(writeTags[i].WriteValue));
                                            }
                                            if (writeTags[i].tagType == DataType.UINT || writeTags[i].tagType == DataType.WORD)
                                            {
                                                pBuf = BitConverter.GetBytes(Convert.ToUInt16(writeTags[i].WriteValue));
                                            }
                                            break;
                                        case SoftElemType.AM_MD:
                                            softElemType = SoftElemType.ELEM_MW;
                                            nStart = writeTags[i].nModAddr;
                                            nCount = 4;
                                            pBuf = new byte[4] { 0, 0, 0, 0 };
                                            if (writeTags[i].tagType == DataType.DINT)
                                            {
                                                pBuf = BitConverter.GetBytes(Convert.ToInt32(writeTags[i].WriteValue));
                                            }
                                            else
                                            {
                                                pBuf = BitConverter.GetBytes(Convert.ToSingle(writeTags[i].WriteValue));
                                            }
                                            break;
                                        case SoftElemType.AM_ML:
                                            softElemType = SoftElemType.ELEM_MW;
                                            nStart = writeTags[i].nModAddr;
                                            nCount = 8;
                                            pBuf = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                                            pBuf = BitConverter.GetBytes(Convert.ToDouble(writeTags[i].WriteValue));
                                            break;
                                    }
                                }
                                int nRet = communicat.Am600_Write_Soft_Elem(softElemType, nStart, nCount, pBuf, nNetId);
                            }
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            logHepler.addLog_common("AINOVNCE write Error:" + ex.Message);
                        }
                        finally
                        {
                            // 释放锁
                            Monitor.Exit(communicat.lockObject);
                        }
                        writeTags.Clear();
                    }

                }
                #endregion



            }
            return result;
        }

        public Byte s_SetBit(Byte byTargetByte, int nTargetPos, bool nValue)
        {
            int nValueOfTargetPos = -1;

            if (!(nTargetPos >= 0 && nTargetPos < 8))
            {
                return 0;
            }

            switch (nTargetPos)
            {
                case 0:
                    nValueOfTargetPos = (byTargetByte >> 7) & 0x01;
                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x80);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0x7f);
                    }
                    break;

                case 1:
                    nValueOfTargetPos = (byTargetByte >> 6) & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x40);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xbf);
                    }
                    break;

                case 2:
                    nValueOfTargetPos = (byTargetByte >> 5) & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x20);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xdf);
                    }
                    break;

                case 3:
                    nValueOfTargetPos = (byTargetByte >> 4) & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x10);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xef);
                    }
                    break;

                case 4:
                    nValueOfTargetPos = (byTargetByte >> 3) & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x08);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xf7);
                    }
                    break;

                case 5:
                    nValueOfTargetPos = (byTargetByte >> 2) & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x04);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xfb);
                    }
                    break;

                case 6:
                    nValueOfTargetPos = (byTargetByte >> 1) & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x02);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xfd);
                    }
                    break;

                case 7:
                    nValueOfTargetPos = byTargetByte & 0x01;

                    if (nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte | 0x01);
                    }
                    else if (!nValue)
                    {
                        byTargetByte = Convert.ToByte(byTargetByte & 0xfe);
                    }
                    break;

                default:
                    break;
            }

            if (nValueOfTargetPos != -1)
            {
                return byTargetByte;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {


            if (m_ready)
            {
                if (communicat.Exit_ETH(nNetId))
                {
                    logHepler.addLog_common("INMOVANCE 断开连接完成" + device.DeviceName);
                    m_stopTime = DateTime.Now;
                    m_ready = false;
                }
                else
                {
                    logHepler.addLog_common("INMOVANCE 断开连接失败" + device.DeviceName);
                }
            }


        }

        public bool CheckStoped()
        {
            bool result = false;
            return result;
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
