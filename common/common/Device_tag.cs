using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Device_tag : NotifyChanged
    {
        private ProtoType _protoID;
        private bool _Enable = true;
        private string _DeviceName = "";
        private string _IPAdress;
        private int _TCPPort;
        private int _Timeout;
        private int _CycTime;
        private List<taginfo> _infotags;
        private string _cpuType;
        private int _netID;
        private ByteOrder _byteOrder;
        private bool _opcMonitor = true;

        public ProtoType protoType { get => _protoID; set => SetProperty(ref _protoID, value); }
        public bool Enable
        { get => _Enable; set => SetProperty(ref _Enable, value); }
        public string DeviceName
        { get => _DeviceName; set => SetProperty(ref _DeviceName, value); }
        public string IPAdress { get => _IPAdress; set => SetProperty(ref _IPAdress, value); }
        public int TCPPort { get => _TCPPort; set => SetProperty(ref _TCPPort, value); }
        public int Timeout { get => _Timeout; set => SetProperty(ref _Timeout, value); }
        public int CycTime { get => _CycTime; set => SetProperty(ref _CycTime, value); }
        public List<taginfo> infotags { get => _infotags; set => SetProperty(ref _infotags, value); }
        public string CpuType { get => _cpuType; set => SetProperty(ref _cpuType, value); }
        public int NetID { get => _netID; set => SetProperty(ref _netID, value); }
        public ByteOrder ByteOrder { get => _byteOrder; set => SetProperty(ref _byteOrder, value); }
        public bool opcMonitor
        { get => _opcMonitor; set => SetProperty(ref _opcMonitor, value); }

    }
}
