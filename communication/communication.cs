using common;
using common.helper;
using common.tag;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using TwinCAT.PlcOpen;

namespace communication
{
    /// <summary>
    /// User 驱动  连接PLC 刷新变量
    /// </summary>
    public sealed class communicat 
    {
        #region   引入dll  
        #region //标准库
        [DllImport("StandardModbusApi.dll", EntryPoint = "Init_ETH_String", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Init_ETH_String(string sIpAddr, int nNetId = 0, int IpPort = 502);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Exit_ETH", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Exit_ETH(int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Read_Device_Block", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Read_Device_Block(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H5u_Write_Device_Block", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H5u_Write_Device_Block(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "H3u_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int H3u_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Read_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Read_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);

        [DllImport("StandardModbusApi.dll", EntryPoint = "Am600_Write_Soft_Elem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Am600_Write_Soft_Elem(SoftElemType eType, int nStartAddr, int nCount, byte[] pValue, int nNetId = 0);



        #endregion
        #endregion
        public static ObservableCollection<protocol> protocols = communicationTag.RunProtos;
        public static List<IConnection> connectionGroups = new List<IConnection>();
        public static object lockObject = new object();

        static List<cnnSte_inov> lst_conneced_inov = new List<cnnSte_inov>();
        //static Dictionary<Device, bool> lst_conneced_inov = new Dictionary<Device, bool>();

        //如果从channel外部访问写入值，需要遍历所有tags，耗时太长
        //测试Doupdate方法加override Value模式是否可行
        //外部函数更改Value时执行override后属性，读取线程调用Doupdate执行basechannel的Value属性
        //变量组分为总的变量组和各个Device的分tag组，由分tag组更改总tag组
        //控件取值总tag组

        /// <summary>
        /// 连接驱动
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {
            #region 速度慢 舍弃
            //foreach (protocol protocol in protocols) 
            //{
            //    if (protocol.ProtoName == "INOVANCE")
            //    {

            //        foreach (Device device in protocol.Devices) 
            //        {
            //            if (device.Enable)
            //            {
            //                if (device.runTags.Count>0)
            //                {                               
            //                    if (device.IPAdress != "" && device.TCPPort > 0)
            //                    {
            //                        if (!lst_conneced_inov.Exists(t => t.IPAdress == device.IPAdress))
            //                        {
            //                            var result = communicat.Init_ETH_String(device.IPAdress, device.NetID, device.TCPPort);
            //                            if (result == true)
            //                            {
            //                                lst_conneced_inov.Add(new cnnSte_inov() 
            //                                {
            //                                    state = true,
            //                                    devname = device.DeviceName,
            //                                    IPAdress = device.IPAdress,
            //                                    TCPPort = device.TCPPort,
            //                                    device = device,
            //                                });
            //                                connectionGroups.Add(new InovanceConnection(device,true));
            //                            }
            //                            else
            //                            {
            //                                lst_conneced_inov.Add(new cnnSte_inov()
            //                                {
            //                                    state = false,
            //                                    devname = device.DeviceName,
            //                                    IPAdress = device.IPAdress,
            //                                    TCPPort = device.TCPPort,
            //                                    device = device,
            //                                });
            //                                logHepler.addLog_common("INOVANCE " + device.IPAdress + " " + "连接PLC失败");                                           
            //                            }
            //                        }
            //                        else
            //                        {
            //                            if (CheckConnect(device.IPAdress, device.TCPPort))
            //                            {
            //                                connectionGroups.Add(new InovanceConnection(device, true));
            //                                lst_conneced_inov.Add(new cnnSte_inov()
            //                                {
            //                                    state = true,
            //                                    devname = device.DeviceName,
            //                                    IPAdress = device.IPAdress,
            //                                    TCPPort = device.TCPPort,
            //                                    device = device,
            //                                });
            //                            }
            //                            else
            //                            {
            //                                lst_conneced_inov.Add(new cnnSte_inov()
            //                                {
            //                                    state = false,
            //                                    devname = device.DeviceName,
            //                                    IPAdress = device.IPAdress,
            //                                    TCPPort = device.TCPPort,
            //                                    device = device,
            //                                });
            //                            }
            //                        }                                   
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    if (protocol.ProtoName == "ModbusTCP")
            //    {
            //        foreach (Device device in protocol.Devices)
            //        {
            //            if (device.Enable)
            //            {
            //                if (device.runTags.Count > 0)
            //                {
            //                    connectionGroups.Add(new modbusTCPConnection( device));
            //                }
            //            }
            //        }
            //    }
            //    if (protocol.ProtoName == "TwincatADS")
            //    {
            //        foreach (Device device in protocol.Devices)
            //        {
            //            if (device.Enable)
            //            {
            //                if (device.runTags.Count > 0)
            //                {
            //                    connectionGroups.Add(new ADSConnection( device));
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion


            Parallel.ForEach(protocols, protocol =>
            {
                if (protocol.ProtoName == "INOVANCE")
                {

                    foreach (Device device in protocol.Devices)
                    {
                        if (device.Enable)
                        {
                            if (device.runTags.Count > 0)
                            {
                                if (device.IPAdress != "" && device.TCPPort > 0)
                                {
                                    if (!lst_conneced_inov.Exists(t => t.IPAdress == device.IPAdress))
                                    {
                                        if (CheckConnect(device.IPAdress, device.TCPPort))
                                        {
                                            var result = communicat.Init_ETH_String(device.IPAdress, device.NetID, device.TCPPort);
                                            if (result == true)
                                            {
                                                lst_conneced_inov.Add(new cnnSte_inov()
                                                {
                                                    state = true,
                                                    devname = device.DeviceName,
                                                    IPAdress = device.IPAdress,
                                                    TCPPort = device.TCPPort,
                                                    device = device,
                                                });
                                                connectionGroups.Add(new InovanceConnection(device, true));
                                            }
                                            else
                                            {
                                                lst_conneced_inov.Add(new cnnSte_inov()
                                                {
                                                    state = false,
                                                    devname = device.DeviceName,
                                                    IPAdress = device.IPAdress,
                                                    TCPPort = device.TCPPort,
                                                    device = device,
                                                });
                                                logHepler.addLog_common("INOVANCE " + device.IPAdress + " " + "连接PLC失败");
                                            }
                                        }
                                      
                                    }
                                    else
                                    {
                                        if (CheckConnect(device.IPAdress, device.TCPPort))
                                        {
                                            connectionGroups.Add(new InovanceConnection(device, true));
                                            lst_conneced_inov.Add(new cnnSte_inov()
                                            {
                                                state = true,
                                                devname = device.DeviceName,
                                                IPAdress = device.IPAdress,
                                                TCPPort = device.TCPPort,
                                                device = device,
                                            });
                                        }
                                        else
                                        {
                                            lst_conneced_inov.Add(new cnnSte_inov()
                                            {
                                                state = false,
                                                devname = device.DeviceName,
                                                IPAdress = device.IPAdress,
                                                TCPPort = device.TCPPort,
                                                device = device,
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (protocol.ProtoName == "ModbusTCP")
                {
                    foreach (Device device in protocol.Devices)
                    {
                        if (device.Enable)
                        {
                            if (device.runTags.Count > 0)
                            {
                                connectionGroups.Add(new modbusTCPConnection(device));
                            }
                        }
                    }
                }
                if (protocol.ProtoName == "TwincatADS")
                {
                    foreach (Device device in protocol.Devices)
                    {
                        if (device.Enable)
                        {
                            if (device.runTags.Count > 0)
                            {
                                connectionGroups.Add(new ADSConnection(device));
                            }
                        }
                    }
                }
                if (protocol.ProtoName == "OPCUA")
                {
                    foreach (Device device in protocol.Devices)
                    {
                        if (device.Enable)
                        {
                            if (device.runTags.Count > 0)
                            {
                                connectionGroups.Add(new OPCUAConnection(device));
                            }
                        }
                    }
                }
            });

            Thread thread = new Thread(() =>
            {
                Monitor_ipPort();
            });
            thread.Name = "Monitor_ipPort";
            thread.Start();
            return true;
        }

        public static void Monitor_ipPort()
        {
            while (true)
            {
                try
                {
                    foreach (var item in lst_conneced_inov)
                    {
                        var portSte = CheckConnect(item.IPAdress, item.TCPPort);
                        if (portSte)
                        {
                            if (item.state)
                            {

                            }
                            else
                            {
                                var result = communicat.Init_ETH_String(item.IPAdress, item.NetID, item.TCPPort);
                                if (result == true)
                                {
                                    connectionGroups.Add(new InovanceConnection(item.device, true));
                                    var model = lst_conneced_inov.Where(c => c.devname == item.devname).FirstOrDefault();
                                    model.state = true;
                                }
                            }
                        }
                        else
                        {
                            if (item.state)
                            {
                                foreach (var tag in item.device.runTags)
                                {
                                    tag.refresh(null,false);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
                Thread.Sleep(1000);
            }
           
        }
        /// <summary>
        /// 检查服务器和端口是否可以连接
        /// </summary>
        /// <param name="ipString">服务器ip</param>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public static bool CheckConnect(string ipString, int port)
        {
            bool right = false;
            System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient()
            { SendTimeout = 1000 };
          
            try
            {
                IPAddress ip = IPAddress.Parse(ipString);
                var result = tcpClient.BeginConnect(ip, port, null, null);
                var back = result.AsyncWaitHandle.WaitOne(1000);
                right = tcpClient.Connected;
            }
            catch (Exception)
            {
                //LogHelpter.AddLog($"连接服务{ipString}:{port}失败，设置的超时时间{tcpClient.SendTimeout}毫秒");
                //连接失败
                return false;
            }
            tcpClient.Close();
            tcpClient.Dispose();
            return right;
        }

    }
    public class cnnSte_inov
    {
        public bool state;
        public string devname;
        public string IPAdress { get; set; }
        public int TCPPort { get; set; }
        public int NetID { get; set; }
        public Device device { get; set; }
    }
}
