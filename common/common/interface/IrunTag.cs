using System;
using System.ComponentModel;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;

namespace common
{
    /// <summary>
    /// 单个变量
    /// </summary>
    public interface IrunTag : INotifyPropertyChanged
    {
        ProtoType protoType { get; set; }
        string DeviceName { get; set; }
        string name { get; set; }
        DataType tagType { get; }
        string address { get; set; }
        object value { get; set; }
        object defaultvalue { get; set; }   
        double limithigh { get; set; }
        double limitlow { get; set; }
        AlarmType alarmType { get; set; }
        double alarmhigh { get; set; }
        double alarmlow { get; set; }
        bool archive { get; set; }
        bool logout { get; set; }
        string description { get; set; }
        string unit { get; set; }
        Symbol handle { get; set; }
        bool flagState { get; set; }
        int nModAddr { get; set; }
        int nRealAddr { get; set; }

        SoftElemType elemType { get; set; }

        event EventHandler ValueChanged;
        
      

    }
}