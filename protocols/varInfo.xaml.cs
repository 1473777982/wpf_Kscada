using common;
using common.tag;
using Panuon.UI.Silver;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace R2R.protocols
{
    /// <summary>
    /// ADSclient_addvar.xaml 的交互逻辑
    /// </summary>
    public partial class varInfo : WindowX
    {
        public static tagdata tagdata { get; set; }
        public static bool topic1, topic2;
        Device_tag source;
        taginfo taginfo1;
        bool add;

        public varInfo(Device_tag source, bool add) // if source为空则报错，需解决
        {
            InitializeComponent();
            this.source = source;
            this.add = add;
            tagdata = new tagdata();
        }
        public varInfo(taginfo taginfo, bool add)
        {
            tagdata = new tagdata();
            InitializeComponent();
            this.taginfo1 = taginfo;
            this.add = add;
            var_name.Text = taginfo1.name;
            tagdata.name = taginfo1.name;
            tagdata.address = taginfo1.address;
            var_type.SelectedIndex = (int)taginfo1.tagType;
            var_address.Text = taginfo1.address;
            var_defaultvalue.Text = taginfo1.defaultvalue.ToString();
            var_high.Text = taginfo1.limithigh.ToString();
            var_low.Text = taginfo1.limitlow.ToString();
            var_alarm.SelectedIndex = (int)taginfo1.alarmType;
            var_alarmhigh.Text = taginfo1.alarmhigh.ToString();
            var_alarmlow.Text = taginfo1.alarmlow.ToString();
            var_logout.IsChecked = taginfo1.logout;
            var_archive.IsChecked = taginfo1.archive;
            var_description.Text = taginfo1.description;
            var_unit.Text = taginfo1.unit;
        }
        private void WindowX_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = tagdata;
        }
        private void yes_Click(object sender, RoutedEventArgs e)
        {
            taginfo t;
            if (var_name.Text == null || var_address == null)
            {
                MessageBoxX.Show("变量名称或地址不能为空", "提示");
                return;
            }
            if (var_type.SelectedItem == null)
            {
                MessageBoxX.Show("变量类型不能为空", "提示");
                return;
            }
            else
            {
                string _archive = var_archive.IsChecked == true ? "True" : "False";
                string _logout = var_logout.IsChecked == true ? "True" : "False";
                t = checkInput();
                if (add)
                {
                    if (!communicationTag.Dic_taginfos.Keys.Contains(var_name.Text))
                    {
                        t.protoType = source.protoType;
                        t.DeviceName = source.DeviceName;
                        source.infotags.Add(t);
                        communicationTag.Dic_taginfos.TryAdd(t.name, t);
                    }
                    else
                    {
                        MessageBox.Show("已存在变量名: " + var_name.Text);
                    }
                }
                else
                {
                    t.protoType = taginfo1.protoType;
                    t.DeviceName = taginfo1.DeviceName;
                    taginfo1.name = t.name;
                    taginfo1.tagType = t.tagType;
                    taginfo1.address = t.address;
                    taginfo1.defaultvalue = t.defaultvalue;
                    taginfo1.limithigh = t.limithigh;
                    taginfo1.limitlow = t.limitlow;
                    taginfo1.alarmType = t.alarmType;
                    taginfo1.alarmhigh = t.alarmhigh;
                    taginfo1.alarmlow = t.alarmlow;
                    taginfo1.logout = t.logout;
                    taginfo1.archive = t.archive;
                    taginfo1.description = t.description;
                    taginfo1.unit = t.unit;
                }
            }
            R2R.protocols.protocols.devices_changed[t.DeviceName] = 2;
            DialogResult = true;
            this.Close();
        }
        private taginfo checkInput()
        {
            taginfo tf;
            tf = new taginfo()
            {
                name = var_name.Text,
                tagType = (DataType)var_type.SelectedIndex,
                address = var_address.Text,
                defaultvalue = string.IsNullOrWhiteSpace(var_defaultvalue.Text) ? 0.0 : Convert.ToDouble(var_defaultvalue.Text),
                limithigh = string.IsNullOrWhiteSpace(var_high.Text) ? 0.0 : Convert.ToDouble(var_high.Text),
                limitlow = string.IsNullOrWhiteSpace(var_low.Text) ? 0.0 : Convert.ToDouble(var_low.Text),
                alarmType = (AlarmType)var_alarm.SelectedIndex,
                alarmhigh = string.IsNullOrWhiteSpace(var_alarmhigh.Text) ? 0.0 : Convert.ToDouble(var_alarmhigh.Text),
                alarmlow = string.IsNullOrWhiteSpace(var_alarmlow.Text) ? 0.0 : Convert.ToDouble(var_alarmlow.Text),
                logout = (bool)var_logout.IsChecked,
                archive = (bool)var_archive.IsChecked,
                description = var_description.Text,
                unit = var_unit.Text
            };
            return tf;
        }
        public static void checkclick()
        {
            if (topic1 == true && topic2 == true)
            {
                tagdata.clickable = true;
            }
            else
            {
                tagdata.clickable = false;
            }
        }
        private void no_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

    public class tagdata : helper.NotifyChanged
    {
        string _name;
        string _address;
        bool _clickable;
        public string name { get => _name; set => SetProperty(ref _name, value); } //{ get => _DeviceName; set => SetProperty(ref _DeviceName, value); }
        public string address { get => _address; set => SetProperty(ref _address, value); }
        public bool clickable { get => _clickable; set => SetProperty(ref _clickable, value); }
    }

    public class tagRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                varInfo.topic1 = false;
                varInfo.checkclick();
                return new ValidationResult(false, "不能为空！");
            }
            //else
            //{
            //    Regex reg = new Regex("^[a-zA-Z][\u4E00-\u9FA5a-zA-Z0-9_.]*$");
            //    if (!reg.IsMatch(value as string))
            //    {
            //        varInfo.topic1 = false;
            //        varInfo.checkclick();
            //        return new ValidationResult(false, "以英文字母开头，只能包含中文、英文字母、数字、下划线！");
            //    }
            //}
            varInfo.topic1 = true;
            varInfo.checkclick();
            return new ValidationResult(true, null);
        }
    }

    public class addressRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string))
            {
                varInfo.topic2 = false;
                varInfo.checkclick();
                return new ValidationResult(false, "不能为空！");
            }
            varInfo.topic2 = true;
            varInfo.checkclick();
            return new ValidationResult(true, null);
        }
    }
}
