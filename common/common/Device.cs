using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{

    /// <summary>
    /// 服务器配置
    /// </summary>
    public  class Device : NotifyChanged
    {
        private ProtoType _protoID;
        private bool _Enable = true;
        private string _DeviceName ="";
        private string _IPAdress;
        private int _TCPPort;
        private int _Timeout;
        private int _CycTime;
        private ObservableCollection<runTag> _runTags;
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
        public ObservableCollection<runTag> runTags { get => _runTags; set => SetProperty(ref _runTags, value); }
        public string CpuType { get => _cpuType; set => SetProperty(ref _cpuType, value); }
        public int NetID { get => _netID; set => SetProperty(ref _netID, value); }
        public ByteOrder ByteOrder { get => _byteOrder; set => SetProperty(ref _byteOrder, value); }     
        public bool opcMonitor
        { get => _opcMonitor; set => SetProperty(ref _opcMonitor, value); }
        public bool cnnSte { get; set; } //tcp通讯连接状态
        public Device()
        {
            runTags = new ObservableCollection<runTag>();
        }
    }
}
