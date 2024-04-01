using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace R2R.protocols
{
    /// <summary>
    /// DeviceDialog_ADS.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceDialog_default : Window
    {
        public static bool box1, box2, box3;
        static devData devData { get; set; }
        public DeviceDialog_default()
        {
            InitializeComponent();

            DataContext = this;
            devData = new devData();
            okButton.DataContext = devData;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            enableCheck.IsChecked = Enabled;
            MonitorCheck.IsChecked = monitor;
        }

        public string deviceName { get; set; }
        public bool Enabled { get; set; }
        public int Cyctime { get; set; }
        public string IPaddress { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public bool monitor { get; set; }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            if (deviceTextbox.Text == "")
            {
                deviceTextbox.Focus();
                DialogResult = false;
                return;
            }
            else
            {
                deviceName = deviceTextbox.Text;
                Enabled = (bool)enableCheck.IsChecked;
                monitor = (bool)MonitorCheck.IsChecked;
                if (!string.IsNullOrEmpty(cyctime.Text))
                {
                    Cyctime = Convert.ToInt32(cyctime.Text);
                }
                else
                {
                    Cyctime = 1000;
                }
                IPaddress = IP.Text;
                Port = Convert.ToInt32(port.Text);
                if (!string.IsNullOrEmpty(timeout.Text))
                {
                    Timeout = Convert.ToInt32(timeout.Text);
                }
                else
                {
                    Timeout = 5000;
                }
                DialogResult = true;
                Close();
            }
        }

        public static void checkclick()
        {
            if (box1 == true && box2 == true && box3 == true)
            {
                devData.clickable = true;
            }
            else
            {
                devData.clickable = false;
            }
        }

    }
    public class devData : helper.NotifyChanged
    {

        bool _clickable;
        public bool clickable { get => _clickable; set => SetProperty(ref _clickable, value); }

    }
    public class nameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                DeviceDialog_default.box1 = false;
                DeviceDialog_default.checkclick();
                return new ValidationResult(false, "不能为空！");
            }

            DeviceDialog_default.box1 = true;
            DeviceDialog_default.checkclick();
            return new ValidationResult(true, null);
        }
    }
    public class ipRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                DeviceDialog_default.box2 = false;
                DeviceDialog_default.checkclick();
                return new ValidationResult(false, "不能为空！");
            }
            //else
            //{
            //    Regex reg = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            //    if (!reg.IsMatch(value as string))
            //    {
            //        DeviceDialog_default.box2 = false;
            //        DeviceDialog_default.checkclick();
            //        return new ValidationResult(false, "ip格式不正确！");
            //    }
            //}
            DeviceDialog_default.box2 = true;
            DeviceDialog_default.checkclick();
            return new ValidationResult(true, null);
        }
    }
    public class portRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                DeviceDialog_default.box3 = false;
                DeviceDialog_default.checkclick();
                return new ValidationResult(false, "不能为空！");
            }
            else
            {
                Regex reg = new Regex("^[0-9]*$");
                if (!reg.IsMatch(value as string))
                {
                    DeviceDialog_default.box3 = false;
                    DeviceDialog_default.checkclick();
                    return new ValidationResult(false, "端口格式不正确！");
                }
            }
            DeviceDialog_default.box3 = true;
            DeviceDialog_default.checkclick();
            return new ValidationResult(true, null);
        }
    }
}
