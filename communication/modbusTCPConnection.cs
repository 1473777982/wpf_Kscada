using common;
using common.helper;
using NModbus;
using NModbus.Extensions.Enron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace communication
{
    public class modbusTCPConnection : IConnection
    {

        #region 变量
        TcpClient client;
        IModbusMaster master;
        DateTime m_stopTime = DateTime.Now;
        private Dictionary<string, runTag> aliveTags = new Dictionary<string, runTag>(StringComparer.OrdinalIgnoreCase);
        private List<runTag> writeTags = new List<runTag>();
        volatile bool m_ready = false;
        int timeout = 60;
        int cyctime = 500;
        string serverAdr = "";
        int port = 5100;
        int nNetId;
        byte slaveAddress;
        string tcpName = "";
        Device device;
        ByteOrder ByteOrder;

        //声明CancellationTokenSource对象
        Dictionary<CancellationTokenSource, CancellationToken> cancelsignals = new Dictionary<CancellationTokenSource, CancellationToken>();
        //使用多个CancellationTokenSource进行复合管理
        CancellationTokenSource compositeCancel = new CancellationTokenSource();
        //阻塞读取线程
        ManualResetEventSlim blockEvent = new ManualResetEventSlim(false);

        #endregion

        public modbusTCPConnection( Device device)
        {
            this.device = device;
            tcpName = device.DeviceName;
            timeout = device.Timeout;
            cyctime = device.CycTime;
            ByteOrder = device.ByteOrder;
            slaveAddress = Convert.ToByte(device.NetID);
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
                        }
                    }
                }
                if (ConnectToServer())
                {
                    sortTags();
                    Start();
                }
            }
        }


        /// <summary>
        /// 连接PLC 
        /// </summary>
        /// <returns></returns>
        bool ConnectToServer()
        {
            try
            {
                client = new TcpClient(serverAdr, port);
                var factory = new ModbusFactory();
                master = factory.CreateMaster(client);
                m_ready = true;
                return true;
            }
            catch (Exception)
            {
                logHepler.addLog_common("modbus:连接设备失败" + serverAdr + port);
                return false;
            }
        }

        List<Dictionary<int, runTag>> list_tags_sorted_Coil;
        List<Dictionary<int, runTag>> list_tags_sorted_Input;
        List<Dictionary<int, runTag>> list_tags_sorted_InputRegister;
        List<Dictionary<int, runTag>> list_tags_sorted_HoldingRegister;

        Dictionary<int, runTag> dic_Coil; //0区: 0x0000 - 0xffff
        Dictionary<int, runTag> dic_Input; //1区: 0x10000 - 0x1ffff
        Dictionary<int, runTag> dic_InputRegister;//3区: 0x30000 - 0x3ffff
        Dictionary<int, runTag> dic_HoldingRegister;//4区: 0x40000 - 0x4ffff
        List<Task> tasks = new List<Task>();

        public void sortTags()
        {
            list_tags_sorted_Coil = new List<Dictionary<int, runTag>>();
            list_tags_sorted_Input = new List<Dictionary<int, runTag>>();
            list_tags_sorted_InputRegister = new List<Dictionary<int, runTag>>();
            list_tags_sorted_HoldingRegister = new List<Dictionary<int, runTag>>();
            dic_Coil = new Dictionary<int, runTag>();
            dic_Input = new Dictionary<int, runTag>();
            dic_InputRegister = new Dictionary<int, runTag>();
            dic_HoldingRegister = new Dictionary<int, runTag>();

            //获得range
            foreach (var item in aliveTags)
            {
                int addr = Convert.ToInt32(item.Value.address);
                if (addr < 0xffff)
                {
                    dic_Coil.Add(addr, item.Value);
                }
                else if (addr < 0x1ffff)
                {
                    dic_Input.Add(addr, item.Value);
                }
                else if (addr < 0x3ffff)
                {
                    dic_InputRegister.Add(addr, item.Value);
                }
                else if (addr < 0x4ffff)
                {
                    dic_HoldingRegister.Add(addr, item.Value);
                }

            }

            dic_Coil = dic_Coil.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
            dic_Input = dic_Input.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
            dic_InputRegister = dic_InputRegister.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
            dic_HoldingRegister = dic_HoldingRegister.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);

            cut(dic_Coil, list_tags_sorted_Coil);
            cut(dic_Input, list_tags_sorted_Input);
            cut(dic_InputRegister, list_tags_sorted_InputRegister);
            cut(dic_HoldingRegister, list_tags_sorted_HoldingRegister);

        }

        public void cut(Dictionary<int, runTag> dic, List<Dictionary<int, runTag>> list)
        {
            list.Add(new Dictionary<int, runTag>());
            var first = dic.First();
            var last = dic.Last();
            if (last.Key < 0x30000)
            {
                foreach (var item in dic)
                {
                    var index = item.Key / 2000 - first.Key / 2000;
                    if (index + 1 > list.Count)
                    {
                        list.Add(new Dictionary<int, runTag>());
                    }
                    list[index].Add(item.Key, item.Value);
                }
            }
            else if (last.Key <= 0x4ffff)
            {
                foreach (var item in dic)
                {
                    var index = item.Key / 100 - first.Key / 100;
                    if (index + 1 > list.Count)
                    {
                        list.Add(new Dictionary<int, runTag>());
                    }
                    list[index].Add(item.Key, item.Value);
                }
            }
        }
        public bool Start()
        {
            bool result = false;
            if (ConnectToServer())
            {
                try
                {
                    if (list_tags_sorted_Coil.Count > 0)
                    {
                        for (int i = 0; i < list_tags_sorted_Coil.Count; i++)
                        {
                            var cc = new CancellationTokenSource();
                            cancelsignals.Add(cc, cc.Token);
                            tasks.Add(Task.Factory.StartNew(() => CommunicationThread(list_tags_sorted_Coil[i], SoftElemType.ELEM_QX), compositeCancel.Token));
                        }
                    }
                    if (list_tags_sorted_Input.Count > 0)
                    {
                        var cc = new CancellationTokenSource();
                        cancelsignals.Add(cc, cc.Token);
                        for (int i = 0; i < list_tags_sorted_Input.Count; i++)
                        {
                            tasks.Add(Task.Factory.StartNew(() => CommunicationThread(list_tags_sorted_Input[i], SoftElemType.ELEM_MW), compositeCancel.Token));
                        }
                    }
                    if (list_tags_sorted_InputRegister.Count > 0)
                    {
                        for (int i = 0; i < list_tags_sorted_Coil.Count; i++)
                        {
                            var cc = new CancellationTokenSource();
                            cancelsignals.Add(cc, cc.Token);
                            tasks.Add(Task.Factory.StartNew(() => CommunicationThread(list_tags_sorted_InputRegister[i], SoftElemType.ELEM_QX), compositeCancel.Token));
                        }
                    }
                    if (list_tags_sorted_HoldingRegister.Count > 0)
                    {
                        for (int i = 0; i < list_tags_sorted_Coil.Count; i++)
                        {
                            var cc = new CancellationTokenSource();
                            cancelsignals.Add(cc, cc.Token);
                            tasks.Add(Task.Factory.StartNew(() => CommunicationThread(list_tags_sorted_HoldingRegister[i], SoftElemType.ELEM_QX), compositeCancel.Token));
                        }
                    }
                    if (client != null)
                    {
                        var cc = new CancellationTokenSource();
                        cancelsignals.Add(cc, cc.Token);
                        tasks.Add(Task.Factory.StartNew(() => WriteToDevice(), compositeCancel.Token));
                    }
                    if (cancelsignals.Count > 0)
                    {
                        for (int i = 0; i < cancelsignals.Count; i++)
                        {
                            compositeCancel = CancellationTokenSource.CreateLinkedTokenSource(cancelsignals.Values.ToArray());
                        }
                    }
                    result = true;
                    // 等待所有线程完成
                    Task.WaitAll(tasks.ToArray());
                }
                catch (Exception ex)
                {
                    logHepler.addLog_common("modbusTCP" + ex.Message);
                }

            }
            return result;

        }

        private void CommunicationThread(Dictionary<int, runTag> dic, SoftElemType elemType)
        {

            try
            {

                ushort nCount = 0;
                ushort nStartAddr = 0;
                byte[] doubleBytes = new byte[4]; //字节数组，调整顺序
                byte[] longBytes = new byte[8];

                while (m_ready)
                {
                    if (writeTags.Count > 0)
                    {
                        blockEvent.Wait();
                    }
                    else
                    {
                        blockEvent.Set();
                    }

                    if (dic.First().Key <= 0xffff) //读coils  0区: 0x0000 - 0xffff
                    {
                        nStartAddr = Convert.ToUInt16(dic.First().Key / 2000 * 2000);
                        nCount = 2000;
                        try
                        {
                            bool[] resault;
                            lock (master)
                            {
                                resault = master.ReadCoils(slaveAddress, nStartAddr, nCount);
                            }
                            foreach (var item in dic)
                            {
                                int step = item.Key - nStartAddr;
                                item.Value.refresh(resault[step], true);
                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (dic.First().Key <= 0x1ffff)//读input  1区: 0x10000 - 0x1ffff
                    {
                        nCount = 2000;
                        nStartAddr = Convert.ToUInt16((dic.First().Key - 0x10000) / 2000 * 2000);

                        try
                        {
                            bool[] resault;
                            lock (master)
                            {
                                resault = master.ReadInputs(slaveAddress, nStartAddr, nCount);
                            }
                            foreach (var item in dic)
                            {
                                int step = item.Key - 0x10000 - nStartAddr;
                                item.Value.refresh(resault[step], true);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (dic.First().Key <= 0x3ffff)//读InputRegister  3区: 0x30000 - 0x3ffff
                    {
                        nCount = 100;
                        nStartAddr = Convert.ToUInt16((dic.First().Key - 0x30000) / 100 * 100);

                        try
                        {
                            ushort[] resault;
                            lock (master)
                            {
                                resault = master.ReadInputRegisters(slaveAddress, nStartAddr, nCount);
                            }
                            foreach (var item in dic)
                            {
                                int step = item.Key - 0x30000 - nStartAddr;
                                if (item.Value.tagType == DataType.WORD || item.Value.tagType == DataType.UINT)
                                {
                                    if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                    {
                                        item.Value.refresh(resault[step], true);
                                    }
                                    else
                                    {
                                        byte[] RegisterBytes = BitConverter.GetBytes(resault[step]);
                                        byte[] newBytes =
                                        {
                                            RegisterBytes[1],
                                            RegisterBytes[0]
                                        };
                                        item.Value.refresh(BitConverter.ToUInt16(newBytes, 0), true);
                                    }
                                }
                                else if (item.Value.tagType == DataType.INT)
                                {
                                    if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                    {
                                        item.Value.refresh(resault[step], true);
                                    }
                                    else
                                    {
                                        byte[] RegisterBytes = BitConverter.GetBytes(resault[step]);
                                        byte[] newBytes =
                                        {
                                            RegisterBytes[1],
                                            RegisterBytes[0]
                                        };
                                        item.Value.refresh(BitConverter.ToInt16(newBytes, 0), true);
                                    }
                                }
                                else if (item.Value.tagType == DataType.DINT)
                                {
                                    ushort lowRegister = resault[step];
                                    ushort highRegister = resault[step + 1];
                                    convert_ByteOrder_4(lowRegister, highRegister, doubleBytes);
                                    item.Value.refresh(BitConverter.ToInt32(doubleBytes, 0), true);

                                }
                                else if (item.Value.tagType == DataType.REAL)
                                {
                                    ushort lowRegister = resault[step];
                                    ushort highRegister = resault[step + 1];
                                    convert_ByteOrder_4(lowRegister, highRegister, doubleBytes);
                                    item.Value.refresh(BitConverter.ToSingle(doubleBytes, 0), true);
                                }
                                else if (item.Value.tagType == DataType.LREAL)
                                {
                                    ushort lowRegister = resault[step];
                                    ushort lowHighRegister = resault[step + 1];
                                    ushort highLowRegister = resault[step + 2];
                                    ushort highRegister = resault[step + 3];
                                    convert_ByteOrder_8(lowRegister, lowHighRegister, highLowRegister, highRegister, longBytes);
                                    item.Value.refresh(BitConverter.ToDouble(longBytes, 0), true);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (dic.First().Key <= 0x4ffff)//读HoldingRegister  4区: 0x40000 - 0x4ffff
                    {
                        nCount = 100;
                        nStartAddr = Convert.ToUInt16((dic.First().Key - 0x40000) / 100 * 100);

                        try
                        {
                            ushort[] resault;
                            lock (master)
                            {
                                resault = master.ReadHoldingRegisters(slaveAddress, nStartAddr, nCount);

                            }
                            foreach (var item in dic)
                            {
                                int step = item.Key - 0x40000 - nStartAddr;
                                if (item.Value.tagType == DataType.WORD || item.Value.tagType == DataType.UINT)
                                {
                                    if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                    {
                                        item.Value.refresh(resault[step], true);
                                    }
                                    else
                                    {
                                        byte[] RegisterBytes = BitConverter.GetBytes(resault[step]);
                                        byte[] newBytes =
                                        {
                                            RegisterBytes[1],
                                            RegisterBytes[0]
                                        };
                                        item.Value.refresh(BitConverter.ToUInt16(newBytes, 0), true);
                                    }
                                }
                                else if (item.Value.tagType == DataType.INT)
                                {
                                    if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                    {
                                        item.Value.refresh(resault[step], true);
                                    }
                                    else
                                    {
                                        byte[] RegisterBytes = BitConverter.GetBytes(resault[step]);
                                        byte[] newBytes =
                                        {
                                            RegisterBytes[1],
                                            RegisterBytes[0]
                                        };
                                        item.Value.refresh(BitConverter.ToInt16(newBytes, 0), true);
                                    }
                                }
                                else if (item.Value.tagType == DataType.DINT)
                                {
                                    ushort lowRegister = resault[step];
                                    ushort highRegister = resault[step + 1];
                                    convert_ByteOrder_4(lowRegister, highRegister, doubleBytes);
                                    item.Value.refresh(BitConverter.ToInt32(doubleBytes, 0), true);

                                }
                                else if (item.Value.tagType == DataType.REAL)
                                {
                                    ushort lowRegister = resault[step];
                                    ushort highRegister = resault[step + 1];
                                    convert_ByteOrder_4(lowRegister, highRegister, doubleBytes);
                                    item.Value.refresh(BitConverter.ToSingle(doubleBytes, 0), true);
                                }
                                else if (item.Value.tagType == DataType.LREAL)
                                {
                                    ushort lowRegister = resault[step];
                                    ushort lowHighRegister = resault[step + 1];
                                    ushort highLowRegister = resault[step + 2];
                                    ushort highRegister = resault[step + 3];
                                    convert_ByteOrder_8(lowRegister, lowHighRegister, highLowRegister, highRegister, longBytes);
                                    item.Value.refresh(BitConverter.ToDouble(longBytes, 0), true);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    // 延时一段时间后继续下一次读取
                    Thread.Sleep(cyctime);

                }

            }
            catch (Exception ex)
            {
                logHepler.addLog_common("modbusTCP " + "  Error:\"" + ex.Message);
            }
        }

        public void convert_ByteOrder_4(ushort lowRegister, ushort highRegister, byte[] doubleBytes)
        {
            byte[] lowRegisterBytes = BitConverter.GetBytes(lowRegister);
            byte[] highRegisterBytes = BitConverter.GetBytes(highRegister);
            switch (ByteOrder)
            {
                case ByteOrder.ABCD:
                    doubleBytes[0] = lowRegisterBytes[0];
                    doubleBytes[1] = lowRegisterBytes[1];
                    doubleBytes[2] = highRegisterBytes[0];
                    doubleBytes[3] = highRegisterBytes[1];
                    break;
                case ByteOrder.CDAB:
                    doubleBytes[0] = highRegisterBytes[0];
                    doubleBytes[1] = highRegisterBytes[1];
                    doubleBytes[2] = lowRegisterBytes[0];
                    doubleBytes[3] = lowRegisterBytes[1];
                    break;
                case ByteOrder.BADC:
                    doubleBytes[0] = lowRegisterBytes[1];
                    doubleBytes[1] = lowRegisterBytes[0];
                    doubleBytes[2] = highRegisterBytes[1];
                    doubleBytes[3] = highRegisterBytes[0];
                    break;
                case ByteOrder.DCBA:
                    doubleBytes[0] = highRegisterBytes[1];
                    doubleBytes[1] = highRegisterBytes[0];
                    doubleBytes[2] = lowRegisterBytes[1];
                    doubleBytes[3] = lowRegisterBytes[0];
                    break;
                default:
                    break;
            }
        }

        public void convert_ByteOrder_8(ushort lowRegister, ushort lowHighRegister, ushort highLowRegister, ushort highRegister, byte[] longBytes)
        {
            byte[] highRegisterBytes = BitConverter.GetBytes(highRegister);
            byte[] highLowRegisterBytes = BitConverter.GetBytes(highLowRegister);
            byte[] lowHighRegisterBytes = BitConverter.GetBytes(lowHighRegister);
            byte[] lowRegisterBytes = BitConverter.GetBytes(lowRegister);
            switch (ByteOrder)
            {
                case ByteOrder.ABCD:
                    longBytes[0] = lowRegisterBytes[0];
                    longBytes[1] = lowRegisterBytes[1];
                    longBytes[2] = lowHighRegisterBytes[0];
                    longBytes[3] = lowHighRegisterBytes[1];
                    longBytes[4] = highLowRegisterBytes[0];
                    longBytes[5] = highLowRegisterBytes[1];
                    longBytes[6] = highRegisterBytes[0];
                    longBytes[7] = highRegisterBytes[1];
                    break;
                case ByteOrder.CDAB:
                    longBytes[0] = lowHighRegisterBytes[0];
                    longBytes[1] = lowHighRegisterBytes[1];
                    longBytes[2] = lowRegisterBytes[0];
                    longBytes[3] = lowRegisterBytes[1];
                    longBytes[4] = highRegisterBytes[0];
                    longBytes[5] = highRegisterBytes[1];
                    longBytes[6] = highLowRegisterBytes[0];
                    longBytes[7] = highLowRegisterBytes[1];
                    break;
                case ByteOrder.BADC:
                    longBytes[0] = lowRegisterBytes[1];
                    longBytes[1] = lowRegisterBytes[0];
                    longBytes[2] = lowHighRegisterBytes[1];
                    longBytes[3] = lowHighRegisterBytes[0];
                    longBytes[4] = highLowRegisterBytes[1];
                    longBytes[5] = highLowRegisterBytes[0];
                    longBytes[6] = highRegisterBytes[1];
                    longBytes[7] = highRegisterBytes[0];
                    break;
                case ByteOrder.DCBA:
                    longBytes[0] = lowHighRegisterBytes[1];
                    longBytes[1] = lowHighRegisterBytes[0];
                    longBytes[2] = lowRegisterBytes[1];
                    longBytes[3] = lowRegisterBytes[0];
                    longBytes[4] = highRegisterBytes[1];
                    longBytes[5] = highRegisterBytes[0];
                    longBytes[6] = highLowRegisterBytes[1];
                    longBytes[7] = highLowRegisterBytes[0];
                    break;
                default:
                    break;
            }
        }

        bool WriteToDevice()
        {
            bool result = false;
            byte[] doubleBytes = new byte[4]; //字节数组，调整顺序
            byte[] longBytes = new byte[8];
            while (m_ready)
            {
                #region Write
                lock (master)
                {
                    if (writeTags != null && writeTags.Count > 0)
                    {
                        try
                        {
                            ushort[] data = new ushort[1];
                            for (int i = 0; i < writeTags.Count; i++)
                            {
                                ushort addr = Convert.ToUInt16(writeTags[i].address);
                                if (addr <= 0xffff)
                                {
                                    if (writeTags[i].tagType == DataType.BOOL)
                                        master.WriteSingleCoil(slaveAddress, addr, Convert.ToBoolean(writeTags[i].WriteValue));
                                }
                                else
                                {
                                    int addr1 = Convert.ToUInt16(writeTags[i].address);
                                    ushort addrs =(ushort)( addr - 0x40000);
                                    if (addr1 <= 0x4ffff && addr1 >= 0x40000)
                                    {
                                        byte[] RegisterBytes;
                                        switch (writeTags[i].tagType)
                                        {
                                            case DataType.WORD:
                                                data = new ushort[1];
                                                if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                                {
                                                    data[0] = (ushort)writeTags[i].WriteValue;
                                                }
                                                else
                                                {
                                                    RegisterBytes = BitConverter.GetBytes((ushort)writeTags[i].WriteValue);
                                                    byte[] newBytes =
                                                    {
                                                         RegisterBytes[1],
                                                         RegisterBytes[0]
                                                    };
                                                    data[0] = BitConverter.ToUInt16(newBytes, 0);
                                                }
                                                break;
                                            case DataType.INT:
                                                data = new ushort[1];
                                                if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                                {
                                                    data[0] = (ushort)writeTags[i].WriteValue;
                                                }
                                                else
                                                {
                                                    RegisterBytes = BitConverter.GetBytes((Int16)writeTags[i].WriteValue);
                                                    byte[] newBytes =
                                                    {
                                                         RegisterBytes[1],
                                                         RegisterBytes[0]
                                                    };
                                                    data[0] = BitConverter.ToUInt16(newBytes, 0);
                                                }
                                                break;
                                            case DataType.UINT:
                                                data = new ushort[1];
                                                if (ByteOrder == ByteOrder.ABCD || ByteOrder == ByteOrder.CDAB)
                                                {
                                                    data[0] = (ushort)writeTags[i].WriteValue;
                                                }
                                                else
                                                {
                                                    RegisterBytes = BitConverter.GetBytes((ushort)writeTags[i].WriteValue);
                                                    byte[] newBytes =
                                                    {
                                                         RegisterBytes[1],
                                                         RegisterBytes[0]
                                                    };
                                                    data[0] = BitConverter.ToUInt16(newBytes, 0);
                                                }
                                                break;
                                            case DataType.DINT:
                                                data = new ushort[2];
                                                RegisterBytes = BitConverter.GetBytes((Int32)writeTags[i].WriteValue);
                                                ByteOrder_write_4(data, RegisterBytes);
                                                break;
                                            case DataType.REAL:
                                                data = new ushort[2];
                                                RegisterBytes = BitConverter.GetBytes((Single)writeTags[i].WriteValue);
                                                ByteOrder_write_4(data, RegisterBytes);
                                                break;
                                            case DataType.LREAL:
                                                data = new ushort[4];
                                                RegisterBytes = BitConverter.GetBytes((double)writeTags[i].WriteValue);
                                                //ByteOrder_write_8(data, RegisterBytes);  //未完成，后续修改
                                                break;
                                            default:
                                                break;
                                        }
                                        master.WriteMultipleRegisters(slaveAddress, addrs, data);
                                    }
                                }
                            }
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            logHepler.addLog_common("modbusTCP Error:" + ex.Message);
                        }
                        writeTags.Clear();
                        blockEvent.Set();
                    }

                }
                #endregion
            }
            return result;
        }




        public void ByteOrder_write_4(ushort[] data, byte[] RegisterBytes)
        {
            if (data == null || RegisterBytes == null) return;
            if (data.Length != 2 || RegisterBytes.Length != 4) return;

            switch (ByteOrder)
            {
                case ByteOrder.ABCD:
                   
                    byte[] newBytes_low =
                    {
                           RegisterBytes[0],
                           RegisterBytes[1]
                    };
                    byte[] newBytes_hig =
                    {
                           RegisterBytes[2],
                           RegisterBytes[3]
                    };
                    data[0] = BitConverter.ToUInt16(newBytes_low, 0);
                    data[1] = BitConverter.ToUInt16(newBytes_hig, 0);
                    break;
                case ByteOrder.CDAB:
                    byte[] newBytes_hig1 =
                    {
                           RegisterBytes[0],
                           RegisterBytes[1]
                    };
                    byte[] newBytes_low1 =
                    {
                           RegisterBytes[2],
                           RegisterBytes[3]
                    };
                    data[0] = BitConverter.ToUInt16(newBytes_low1, 0);
                    data[1] = BitConverter.ToUInt16(newBytes_hig1, 0);
                    break;
                case ByteOrder.BADC:
                    byte[] newBytes_low2 =
                    {
                           RegisterBytes[1],
                           RegisterBytes[0]
                    };
                    byte[] newBytes_hig2 =
                    {
                           RegisterBytes[3],
                           RegisterBytes[2]
                    };
                    data[0] = BitConverter.ToUInt16(newBytes_low2, 0);
                    data[1] = BitConverter.ToUInt16(newBytes_hig2, 0);
                    break;
                case ByteOrder.DCBA:
                    byte[] newBytes_hig3 =
                    {
                           RegisterBytes[1],
                           RegisterBytes[0]
                    };
                    byte[] newBytes_low3 =
                    {
                           RegisterBytes[3],
                           RegisterBytes[2]
                    };
                    data[0] = BitConverter.ToUInt16(newBytes_low3, 0);
                    data[1] = BitConverter.ToUInt16(newBytes_hig3, 0);
                    break;
                default:
                    break;
            }
        }

        public void ByteOrder_write_8(ushort[] data, byte[] RegisterBytes)  //未完成，后续修改
        {
            if (data == null || RegisterBytes == null) return;
            if (data.Length != 4 || RegisterBytes.Length != 8) return;

            //switch (ByteOrder)
            //{
            //    case ByteOrder.ABCD:

            //        byte[] newBytes_low =
            //        {
            //               RegisterBytes[0],
            //               RegisterBytes[1]
            //        };
            //        byte[] newBytes_hig =
            //        {
            //               RegisterBytes[2],
            //               RegisterBytes[3]
            //        };
            //        data[0] = BitConverter.ToUInt16(newBytes_low, 0);
            //        data[1] = BitConverter.ToUInt16(newBytes_hig, 0);
            //        break;
            //    case ByteOrder.CDAB:
            //        byte[] newBytes_hig1 =
            //        {
            //               RegisterBytes[0],
            //               RegisterBytes[1]
            //        };
            //        byte[] newBytes_low1 =
            //        {
            //               RegisterBytes[2],
            //               RegisterBytes[3]
            //        };
            //        data[0] = BitConverter.ToUInt16(newBytes_low1, 0);
            //        data[1] = BitConverter.ToUInt16(newBytes_hig1, 0);
            //        break;
            //    case ByteOrder.BADC:
            //        byte[] newBytes_low2 =
            //        {
            //               RegisterBytes[1],
            //               RegisterBytes[0]
            //        };
            //        byte[] newBytes_hig2 =
            //        {
            //               RegisterBytes[3],
            //               RegisterBytes[2]
            //        };
            //        data[0] = BitConverter.ToUInt16(newBytes_low2, 0);
            //        data[1] = BitConverter.ToUInt16(newBytes_hig2, 0);
            //        break;
            //    case ByteOrder.DCBA:
            //        byte[] newBytes_hig3 =
            //        {
            //               RegisterBytes[1],
            //               RegisterBytes[0]
            //        };
            //        byte[] newBytes_low3 =
            //        {
            //               RegisterBytes[3],
            //               RegisterBytes[2]
            //        };
            //        data[0] = BitConverter.ToUInt16(newBytes_low3, 0);
            //        data[1] = BitConverter.ToUInt16(newBytes_hig3, 0);
            //        break;
            //    default:
            //        break;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            //线程停止
            if (cancelsignals.Count > 0)
            {
                cancelsignals.First().Key.Cancel();
                cancelsignals.Clear();
            }
            try
            {
                if (client != null)
                {
                    m_ready = false;
                    client = null;
                    logHepler.addLog_common("modbusTCP 断开连接完成" + device.DeviceName);
                    m_stopTime = DateTime.Now;

                }

            }
            catch (Exception)
            {
                logHepler.addLog_common("modbusTCP 断开连接失败" + device.DeviceName);
            }


        }

        public bool CheckStoped()
        {
            bool result = false;
            if (cancelsignals.Count > 0)
            {

                if ((DateTime.Now - m_stopTime).TotalSeconds > 30)
                {
                    try
                    {
                        cancelsignals.First().Key.Cancel();
                    }
                    catch (Exception)
                    {
                    }
                    if (client != null)
                    {
                        client = null;
                        m_ready = false;
                    }
                    result = true;
                }

            }
            else
                result = true;
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
    }
}
