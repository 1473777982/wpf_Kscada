using Panuon.UI.Silver.Core;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace R2R.UControl
{
    public class code_chart
    {
        /// <summary>
        /// piont 定义
        /// </summary>
        public struct Point_with_datetime
        {
            public DateTime Date { get; set; }
            public double Rate { get; set; }
        }

        public struct var_detail
        {
            public string unit;
            public string name;
            public string describe;
        }

        public class DataGrid_Row_defin : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            bool _IsEnabled;
            string _Name;
            string _unit;
            string _describe;

            //public DataGrid_Row_defin(bool isenabled, TextBlock textblock,string name, string unit, string detail)
            //{
            //    IsEnabled = isenabled;
            //    textBlock = textblock;
            //    Name = name;
            //    Unit = unit;
            //    Detail = detail;
            //}



            [DisplayName("可见")]
            public bool IsEnabled
            {
                get { return _IsEnabled; }
                set
                {
                    if (_IsEnabled != value)
                    {
                        _IsEnabled = value;
                        OnPropertyChanged("IsEnabled");
                    }
                }
            }

            [DisplayName("标签名称")]
            [ColumnWidth("0.2*")]
            [ReadOnlyColumn]
            public string Name
            {
                get { return _Name; }
                set
                {
                    if (_Name != value)
                    {
                        _Name = value;
                        OnPropertyChanged("Name");
                    }
                }
            }
            [DisplayName("单位")]
            [ReadOnlyColumn]
            public string Unit
            {
                get { return _unit; }
                set
                {
                    if (_unit != value)
                    {
                        _unit = value;
                        OnPropertyChanged("Unit");
                    }
                }
            }

            //[DisplayName("线宽")]
            ////[ColumnWidth("0.5*")]
            //[ReadOnlyColumn]
            //public int xiankuan { get; set; }

            //[DisplayName("当前值")]
            ////[ColumnWidth("0.5*")]
            //[ReadOnlyColumn]
            //public int valueNow { get; set; }


            [DisplayName("描述")]
            [ColumnWidth("0.3*")]
            [ReadOnlyColumn]
            public string Describe
            {
                get { return _describe; }
                set
                {
                    if (_describe != value)
                    {
                        _describe = value;
                        OnPropertyChanged("Detail");
                    }
                }
            }
        }
        /// <summary>
        /// double转换time类型
        /// </summary>
        public class TimeConver : IValueConverter
        {
            //当值从绑定源传播给绑定目标时，调用方法Convert
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null)
                    return DependencyProperty.UnsetValue;

                DateTime time = DateTime.FromOADate((double)value);
                //	//return time.ToString("HH:mm:ss");
                return time.ToString("G");
            }

            //当值从绑定目标传播给绑定源时，调用此方法ConvertBack
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string str = value as string;
                DateTime txtDate;
                if (DateTime.TryParse(str, out txtDate))
                {
                    return txtDate;
                }
                return DependencyProperty.UnsetValue;
            }
        }

    }
}
