using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;
using static System.Net.Mime.MediaTypeNames;

namespace common
{
    public class baseRunTag : IrunTag
    {
        //public baseRunTag(taginfo chinfo, protocol plugin, Type t = null) { }

        public ProtoType protoType { get; set; }
        public string DeviceName { get; set; }
        public string name { get; set; }
        public DataType tagType { get; set; }
        public string address { get; set; }
        public object defaultvalue { get; set; }
        public double limithigh { get; set; }
        public double limitlow { get; set; }
        public AlarmType alarmType { get; set; }
        public double alarmhigh { get; set; }
        public double alarmlow { get; set; }
        public bool archive { get; set; }
        public bool logout { get; set; }
        public string description { get; set; }
        public string unit { get; set; }
        public Symbol handle { get; set; }
        public bool flagState { get; set; }
        public int nModAddr { get; set; } // 非实际地址，处理后通讯用地址
        public int nRealAddr { get; set; } //实际plc内存地址，不分地址类型
        public SoftElemType elemType { get; set; }


        object _value;
        public virtual object value
        {
            get => _value;
            set
            {
                if (!object.Equals(value, this._value))
                {
                    this._value = value;
                    SetProperty(ref _value, value);
                }
            }
        }


        public void refresh(object o, bool f)
        {
            flagState = f;
            if (!object.Equals(o, _value))
            {
                SetProperty(ref _value, o, "value");
                _value = o;
                if (ValueChanged != null)
                {
                    ValueChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        public void refresh(object o)
        {
            if (!object.Equals(o, _value))
            {
                SetProperty(ref _value, o, "value");
                _value = o;
                if (ValueChanged != null)
                {
                    ValueChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        public void refresh(bool f)
        {
            flagState = f;
            if (ValueChanged != null)
            {
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        //判断数据类型并转换类型
        public object ConvertFunction(object obj)
        {
            try
            {
                switch (tagType)//根据变量类型执行相应写入操作
                {
                    case DataType.BOOL:
                        obj = Convert.ToBoolean(obj);
                        break;
                    case DataType.BYTE:
                        obj = Convert.ToByte(obj);
                        break;
                    case DataType.WORD:
                        obj = Convert.ToUInt16(obj);
                        break;
                    case DataType.UINT:
                        obj = Convert.ToUInt16(obj);
                        break;
                    case DataType.INT:
                        obj = Convert.ToInt16(obj);
                        break;
                    case DataType.DINT:
                        obj = Convert.ToInt32(obj);
                        break;
                    case DataType.REAL:
                        obj = Convert.ToSingle(obj);
                        break;
                    case DataType.LREAL:
                        obj = Convert.ToDouble(obj);
                        break;
                    case DataType.STRING:
                        obj = Convert.ToString(obj);
                        break;
                }
                return obj;
            }
            catch (Exception)
            {
                return null;
            }


        }


        public event EventHandler ValueChanged;

        // Implement INotifyPropertyChanged interface.
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
