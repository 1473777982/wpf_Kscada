using common;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace R2R.protocols
{
    /// <summary>
    /// DeviceDialog_modbus.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceDialog_modbus : Window
    {
        public static bool box1, box2, box3;
        static devData devData { get; set; }
        public DeviceDialog_modbus()
        {
            InitializeComponent();
            DataContext = this;
            devData = new devData();
            okButton.DataContext = devData;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            enableCheck.IsChecked = Enabled;
            switch (ByteOrder)
            {
                case ByteOrder.ABCD:
                    box_ByteOrder.SelectedIndex = 0;
                    break;
                case ByteOrder.CDAB:
                    box_ByteOrder.SelectedIndex = 1;
                    break;
                case ByteOrder.BADC:
                    box_ByteOrder.SelectedIndex = 2;
                    break;
                case ByteOrder.DCBA:
                    box_ByteOrder.SelectedIndex = 3;
                    break;
                default:
                    break;
            }


            switch (CpuType)
            {
                case "3U":
                    box_CpuType.SelectedIndex = 0;
                    break;
                case "5U":
                    box_CpuType.SelectedIndex = 1;
                    break;
                case "AM":
                    box_CpuType.SelectedIndex = 2;
                    break;
                default:
                    box_CpuType.SelectedIndex = 2;
                    break;
            }

            switch (protoType)
            {
                case ProtoType.ADS:
                    break;
                case ProtoType.INOVANCE:
                    box_ByteOrder.SelectedIndex = 0;
                    box_ByteOrder.IsEnabled = false;
                    break;
                case ProtoType.MODBUS:
                    box_CpuType.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }
        public string deviceName { get; set; }
        public bool Enabled { get; set; }
        public int Cyctime { get; set; }
        public string IPaddress { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public ByteOrder ByteOrder { get; set; }
        public int NetID { get; set; }
        public string CpuType { get; set; }
        public ProtoType protoType { get; set; }

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
                if (!string.IsNullOrEmpty(box_NetID.Text))
                {
                    NetID = Convert.ToInt32(box_NetID.Text);
                }
                else
                {
                    NetID = 1;
                }
                switch (box_CpuType.SelectedIndex)
                {
                    case 0:
                        CpuType = "3U";
                        break;
                    case 1:
                        CpuType = "5U";
                        break;
                    case 2:
                        CpuType = "AM";
                        break;
                    default:
                        CpuType = "AM";
                        break;
                }
                switch (box_ByteOrder.SelectedIndex)
                {
                    case 0:
                        ByteOrder = ByteOrder.ABCD;
                        break;
                    case 1:
                        ByteOrder = ByteOrder.CDAB;
                        break;
                    case 2:
                        ByteOrder = ByteOrder.BADC;
                        break;
                    case 3:
                        ByteOrder = ByteOrder.CDAB;
                        break;
                    default:
                        ByteOrder = ByteOrder.ABCD;
                        break;
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

    public class nameRule_modbus : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                DeviceDialog_modbus.box1 = false;
                DeviceDialog_modbus.checkclick();
                return new ValidationResult(false, "不能为空！");
            }

            DeviceDialog_modbus.box1 = true;
            DeviceDialog_modbus.checkclick();
            return new ValidationResult(true, null);
        }
    }
    public class ipRule_modbus : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                DeviceDialog_modbus.box2 = false;
                DeviceDialog_modbus.checkclick();
                return new ValidationResult(false, "不能为空！");
            }
            else
            {
                Regex reg = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
                if (!reg.IsMatch(value as string))
                {
                    DeviceDialog_modbus.box2 = false;
                    DeviceDialog_modbus.checkclick();
                    return new ValidationResult(false, "ip格式不正确！");
                }
            }
            DeviceDialog_modbus.box2 = true;
            DeviceDialog_modbus.checkclick();
            return new ValidationResult(true, null);
        }
    }
    public class portRule_modbus : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                DeviceDialog_modbus.box3 = false;
                DeviceDialog_modbus.checkclick();
                return new ValidationResult(false, "不能为空！");
            }
            else
            {
                Regex reg = new Regex("^[0-9]*$");
                if (!reg.IsMatch(value as string))
                {
                    DeviceDialog_modbus.box3 = false;
                    DeviceDialog_modbus.checkclick();
                    return new ValidationResult(false, "端口格式不正确！");
                }
            }
            DeviceDialog_modbus.box3 = true;
            DeviceDialog_modbus.checkclick();
            return new ValidationResult(true, null);
        }
    }
}
