using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class protocol_tag : NotifyChanged
    {
        private string _ProtoName = "";
        private ProtoType _protoID;
        private ObservableCollection<Device_tag> _Devices;

        public string ProtoName { get => _ProtoName; set => SetProperty(ref _ProtoName, value); }
        public ProtoType protoID { get => _protoID; set => SetProperty(ref _protoID, value); }
        public ObservableCollection<Device_tag> Devices { get => _Devices; set => SetProperty(ref _Devices, value); }
        public protocol_tag()
        {
            Devices = new ObservableCollection<Device_tag>();
        }
    }
}
