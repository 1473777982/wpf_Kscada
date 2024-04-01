using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class taginfo
    {
        public ProtoType protoType { get; set; }
        public string DeviceName { get; set; }
        public string name{ get; set; }
        public DataType tagType { get; set; }
        public string address{ get; set; }
        public double defaultvalue{ get; set; }
        public double limithigh{ get; set; }
        public double limitlow{ get; set; }
        public AlarmType alarmType { get; set; }
        public double alarmhigh{ get; set; }
        public double alarmlow{ get; set; }
        public bool archive{ get; set; }
        public bool logout{ get; set; }
        public string description{ get; set; }
        public string unit{ get; set; }
        public bool flagState{ get; set; }
    }
}
