using common.tag;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace R2R.Windows
{
    /// <summary>
    /// box_dialog.xaml 的交互逻辑
    /// </summary>
    public partial class box_dialog : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_boxDialog = new System.Windows.Threading.DispatcherTimer();
        string BoxName; //box_CA0
        public box_dialog()
        {
            InitializeComponent();
        }
        public box_dialog(string name)
        {
            InitializeComponent();
            this.BoxName = name.Substring(4);
            Title = name;
            dispatcherTimer_boxDialog.Interval = new TimeSpan(0, 0, 0, 0, 300);
            dispatcherTimer_boxDialog.Tick += new EventHandler(TimeAction);
            dispatcherTimer_boxDialog.Start();
        }
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                MFC_Ar_on.Background = tag_manager.Current.getTagbit(BoxName + "_signal_MFC_Ar", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;
                MFC_O2_on.Background = tag_manager.Current.getTagbit(BoxName + "_signal_MFC_O2", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;
                MFC_N2_on.Background = tag_manager.Current.getTagbit(BoxName + "_signal_MFC_N2", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;
            }
            catch (Exception)
            {

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (BoxName)
            {
                case "CA1":
                    MFC_O2.Visibility = Visibility.Collapsed;
                    MFC_N2.Visibility = Visibility.Collapsed;
                    break;
                case "CA2":
                    MFC_O2.Visibility = Visibility.Visible;
                    MFC_N2.Visibility = Visibility.Collapsed;
                    break;
                case "CA3":
                    MFC_O2.Visibility = Visibility.Collapsed;
                    MFC_N2.Visibility = Visibility.Visible;
                    break;
                case "CA4":
                    MFC_O2.Visibility = Visibility.Collapsed;
                    MFC_N2.Visibility = Visibility.Collapsed;
                    break;
                case "CA5":
                    MFC_O2.Visibility = Visibility.Visible;
                    MFC_N2.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            set_MFC_Ar.varName = BoxName + "_set_MFC_flow_Ar";
            set_MFC_O2.varName = BoxName + "_set_MFC_flow_O2";
            set_MFC_N2.varName = BoxName + "_set_MFC_flow_N2";
            value_MFC_Ar.varName = BoxName + "_value_MFC_flow_Ar";
            value_MFC_O2.varName = BoxName + "_value_MFC_flow_O2";
            value_MFC_N2.varName = BoxName + "_value_MFC_flow_N2";

        }
        private void MFC_on_Click(object sender, RoutedEventArgs e) //CA1_set_ctrl_MFC_Ar
        {
            var name = (sender as Button).Name; //MFC_Ar_on
            var subname = name.Substring(0, name.Length - 3);
            try
            {
                tag_manager.Current.pulseTagbit(BoxName + "_set_ctrl_" + subname, 1, 1000);//bit1
            }
            catch (Exception)
            {

            }
        }

        private void MFC_off_Click(object sender, RoutedEventArgs e)
        {
            var name = (sender as Button).Name; //MFC_Ar_ff
            var subname = name.Substring(0, name.Length - 4);
            try
            {
                tag_manager.Current.pulseTagbit(BoxName + "_set_ctrl_" + subname, 2, 1000);//bit2
            }
            catch (Exception)
            {

            }
        }
    }
}
