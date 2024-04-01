using common.tag;
using Opc.Ua;
using R2R.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace R2R.Pages
{
    /// <summary>
    /// Page0_main.xaml 的交互逻辑
    /// </summary>
    public partial class Page0_main : Page
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_page0 = new System.Windows.Threading.DispatcherTimer();
        public Page0_main()
        {
            InitializeComponent();
            dispatcherTimer_page0.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_page0.Tick += new EventHandler(TimeAction);
            dispatcherTimer_page0.Start();
        }

        //刷新控件状态
        private void TimeAction(object sender, EventArgs e)
        {

        }
        private void Ellipse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Ellipse).Name;
            // 创建和显示弹窗
            if (name.Contains("sp1") || name.Contains("rp1"))
            {
                name = "Pump1";
            }
            else
            {
                name = "Pump2";
            }
            var dialog = new Win_SP_RP(name);
            // 获得鼠标当前位置
            var mousePosition = Mouse.GetPosition(this);
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;

            // 设置弹窗的位置
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;

            // 显示弹窗
            dialog.Show();


        }

        private void box_CA_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Rectangle).Name;

            var mousePosition = Mouse.GetPosition(this);
            var dialog = new box_dialog(name);
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;
            dialog.ShowDialog();
        }

        private void BVGV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Canvas).Name;

            var mousePosition = Mouse.GetPosition(this);
            var dialog = new open_close(name);
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;   
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                //switch
                //tag_manager.Current.setTagValue("_set_cylinder_CBF_work", 1);
            }
        }

        private void Ell_TP_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Ellipse).Name;
            var mousePosition = Mouse.GetPosition(this);
            var dialog = new start_stop(name.Substring(4));
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;
            dialog.ShowDialog();
        }

        private void box_glove_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = Mouse.GetPosition(this);
            var dialog = new gloveBoxCtrl();
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;
            dialog.ShowDialog();
        }

        private void SV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Rectangle).Name;
            var mousePosition = Mouse.GetPosition(this);
            var dialog = new open_close(name);
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                switch (name) 
                {
                    case "SV0":
                        tag_manager.Current.setTagValue("CA0_set_cylinder_CBF_work",1); 
                        break;
                    case "SV1":
                        tag_manager.Current.setTagValue("CA1_set_cylinder_CBF_work", 1);
                        break;
                    case "SV2":
                        tag_manager.Current.setTagValue("CA2_set_cylinder_CBF_work", 1);
                        break;
                    case "SV3":
                        tag_manager.Current.setTagValue("CA3_set_cylinder_CBF_work", 1);
                        break;
                    case "SV4":
                        tag_manager.Current.setTagValue("CA4_set_cylinder_CBF_work", 1);
                        break;
                    case "SV5":
                        tag_manager.Current.setTagValue("CA5_set_cylinder_CBF_work", 1);
                        break;
                    case "SV_glove":
                        tag_manager.Current.setTagValue("glove_set_cylinder_CBF_work", 1);
                        break;
                }
                
            }
            else
            {
                switch (name)
                {
                    case "SV0":
                        tag_manager.Current.setTagValue("CA0_set_cylinder_CBF_home", 1);
                        break;
                    case "SV1":
                        tag_manager.Current.setTagValue("CA1_set_cylinder_CBF_home", 1);
                        break;
                    case "SV2":
                        tag_manager.Current.setTagValue("CA2_set_cylinder_CBF_home", 1);
                        break;
                    case "SV3":
                        tag_manager.Current.setTagValue("CA3_set_cylinder_CBF_home", 1);
                        break;
                    case "SV4":
                        tag_manager.Current.setTagValue("CA4_set_cylinder_CBF_home", 1);
                        break;
                    case "SV5":
                        tag_manager.Current.setTagValue("CA5_set_cylinder_CBF_home", 1);
                        break;
                    case "SV_glove":
                        tag_manager.Current.setTagValue("glove_set_cylinder_CBF_home", 1);
                        break;
                }
            }
        }

        private void Path_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //new open_close().ShowDialog();
        }

    }
}
