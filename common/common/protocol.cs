using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace common
{
    public class protocol : NotifyChanged
    {
        private string _ProtoName = "";
        private ProtoType _protoID;
        private List<Device> _Devices;

        public string ProtoName { get => _ProtoName; set => SetProperty(ref _ProtoName, value); }
        public ProtoType protoID { get => _protoID; set => SetProperty(ref _protoID, value); }
        public List<Device> Devices { get => _Devices; set => SetProperty(ref _Devices, value); }

    }
}
