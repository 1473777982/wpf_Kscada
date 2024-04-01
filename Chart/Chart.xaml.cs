using common;
using common.tag;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static R2R.UControl.code_chart;

namespace R2R.Chart
{
    /// <summary>
    /// Page_Chart.xaml 的交互逻辑
    /// </summary>
    public partial class Chart : Window
    {
        public static ObservableCollection<groupItem> collection_real { get; set; }
        public static ObservableCollection<groupItem> collection_his { get; set; }
        public Chart()
        {
            InitializeComponent();
            collection_real = new ObservableCollection<groupItem>();
            collection_his = new ObservableCollection<groupItem>();
            try
            {
                var js1 = jsonFile.GetJsonFile(@"chartGroup_real.json");
                collection_real = JsonConvert.DeserializeObject<ObservableCollection<groupItem>>(js1);
                var js2 = jsonFile.GetJsonFile(@"chartGroup_his.json");
                collection_his = JsonConvert.DeserializeObject<ObservableCollection<groupItem>>(js2);
            }
            catch (Exception)
            {
                MessageBox.Show("加载曲线组失败");
            }
            combox_real.DataContext = collection_real;
            combox_his.DataContext = collection_his;
        }

        private void Button_Click_gSet(object sender, RoutedEventArgs e)
        {
            var winChartGroup = new chartGroups();
            winChartGroup.ShowDialog();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    combox_real.Visibility = Visibility.Visible;
                    combox_his.Visibility = Visibility.Collapsed;
                    tb_real.Visibility = Visibility.Visible;
                    tb_his.Visibility = Visibility.Collapsed;
                    enable_Vlog.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    combox_real.Visibility = Visibility.Collapsed;
                    combox_his.Visibility = Visibility.Visible;
                    tb_real.Visibility = Visibility.Collapsed;
                    tb_his.Visibility = Visibility.Visible;
                    enable_Vlog.Visibility = Visibility.Collapsed;
                    break;
                default:
                    combox_real.Visibility = Visibility.Visible;
                    combox_his.Visibility = Visibility.Collapsed;
                    tb_real.Visibility = Visibility.Visible;
                    tb_his.Visibility = Visibility.Collapsed;
                    enable_Vlog.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void combox_real_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var varlist = new List<var_detail>();
            ComboBox box = (ComboBox)sender;
            var item = box.SelectedItem as groupItem;
            foreach (var v in item.lineNames)
            {
                if (communicationTag.Current.Dic_ranTags.ContainsKey(v))
                {
                    varlist.Add(new var_detail() { describe = (string)communicationTag.Current.Dic_ranTags[v].description, name = v, unit = (string)communicationTag.Current.Dic_ranTags[v].unit });
                }             
            }
            realchart.list_tags = varlist;
            realchart.vertical_log = item.vertical_log;
        }


        private void combox_his_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var varlist = new List<var_detail>();
            ComboBox box = (ComboBox)sender;
            var item = box.SelectedItem as groupItem;
            foreach (var v in item.lineNames)
            {
                if (communicationTag.Current.Dic_ranTags.ContainsKey(v))
                {
                    varlist.Add(new var_detail() { describe = (string)communicationTag.Current.Dic_ranTags[v].description, name = v, unit = (string)communicationTag.Current.Dic_ranTags[v].unit });
                }
            }
            hischart.list_tags = varlist;
            hischart.vertical_log = item.vertical_log;
        }

        private void enable_Vlog_Checked(object sender, RoutedEventArgs e)
        {
            var a= tabCtrl.SelectedIndex;
            var b = tabCtrl.SelectedItem;
            var c = tabCtrl.SelectedValue;
            var d = tabCtrl.SelectedContent;
            if (tabCtrl.SelectedIndex == 0)
            {
                realchart.vertical_log = true;
            }
            else
            {
                hischart.vertical_log = true;
            }                      
        }

        private void enable_Vlog_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tabCtrl.SelectedIndex == 0)
            {
                realchart.vertical_log = false;
            }
            else
            {
                hischart.vertical_log = false;
            }          
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            collection_real = null;
            collection_his = null;
        }
    }
}
