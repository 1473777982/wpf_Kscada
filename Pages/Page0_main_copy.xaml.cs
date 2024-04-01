using common.tag;
using Opc.Ua;
using R2R.helper;
using R2R.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace R2R.Pages
{
    /// <summary>
    /// Page0_main.xaml 的交互逻辑
    /// </summary>
    public partial class Page0_main_copy : Page
    {
        private readonly Page0_ViewModle _viewModel;
        System.Windows.Threading.DispatcherTimer dispatcherTimer_page0 = new System.Windows.Threading.DispatcherTimer();
        public Page0_main_copy()
        {
            InitializeComponent();
            
            _viewModel = (Page0_ViewModle)DataContext;
            dispatcherTimer_page0.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_page0.Tick += new EventHandler(TimeAction);
            dispatcherTimer_page0.Start();
        }

        //刷新控件状态
        private void TimeAction(object sender, EventArgs e)
        {
            var sig_BV1 = Convert.ToByte(tag_manager.Current.getTagValue("Gen_BV1_signal"));
            var sig_BV2 = Convert.ToByte(tag_manager.Current.getTagValue("Gen_BV2_signal"));
            var sig_SV0 = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_cylinder_CBF"));
            var sig_SV1 = Convert.ToInt16(tag_manager.Current.getTagValue("CA1_signal_cylinder_CBF"));
            var sig_SV2 = Convert.ToInt16(tag_manager.Current.getTagValue("CA2_signal_cylinder_CBF"));
            var sig_SV3 = Convert.ToInt16(tag_manager.Current.getTagValue("CA3_signal_cylinder_CBF"));
            var sig_SV4 = Convert.ToInt16(tag_manager.Current.getTagValue("CA4_signal_cylinder_CBF"));
            var sig_SV5 = Convert.ToInt16(tag_manager.Current.getTagValue("CA5_signal_cylinder_CBF"));


            var sig_sp1 = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump1_sp"));
            var sig_rp1 = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump1_rp"));
            var sig_sp2 = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump2_sp"));
            var sig_rp2 = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump2_rp"));

            var sig_MPG1 = Convert.ToInt32(tag_manager.Current.getTagbit("CA1_signal_MPG", 1));
            var sig_MPG2 = Convert.ToInt32(tag_manager.Current.getTagbit("CA2_signal_MPG", 1)); 
            var sig_MPG3 = Convert.ToInt32(tag_manager.Current.getTagbit("CA3_signal_MPG", 1));
            var sig_MPG4 = Convert.ToInt32(tag_manager.Current.getTagbit("CA4_signal_MPG", 1));
            var sig_MPG5 = Convert.ToInt32(tag_manager.Current.getTagbit("CA5_signal_MPG", 1));

            Int16[] sig_FZB = new Int16[6];
            sig_FZB[0] = Convert.ToInt16(tag_manager.Current.getTagValue("CA1_signal_FZB"));
            sig_FZB[1] = Convert.ToInt16(tag_manager.Current.getTagValue("CA2_signal_FZB"));
            sig_FZB[2] = Convert.ToInt16(tag_manager.Current.getTagValue("CA3_signal_FZB"));
            sig_FZB[3] = Convert.ToInt16(tag_manager.Current.getTagValue("CA4_signal_FZB"));
            sig_FZB[4] = Convert.ToInt16(tag_manager.Current.getTagValue("CA5_signal_FZB"));
            sig_FZB[5] = Convert.ToInt16(tag_manager.Current.getTagValue("Robot_signal_FZB"));
            List<Ellipse> Ell_TP = new List<Ellipse>() {Ell_TP1, Ell_TP2, Ell_TP3, Ell_TP4, Ell_TP5, Ell_TP6};

            var sig_cylinder_GV0 = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_cylinder_PKF"));
            var sig_cylinder_GV1 = Convert.ToInt16(tag_manager.Current.getTagValue("CA1_signal_cylinder_PKF"));
            var sig_cylinder_GV2 = Convert.ToInt16(tag_manager.Current.getTagValue("CA2_signal_cylinder_PKF"));
            var sig_cylinder_GV3 = Convert.ToInt16(tag_manager.Current.getTagValue("CA3_signal_cylinder_PKF"));
            var sig_cylinder_GV4 = Convert.ToInt16(tag_manager.Current.getTagValue("CA4_signal_cylinder_PKF"));
            var sig_cylinder_GV5 = Convert.ToInt16(tag_manager.Current.getTagValue("CA5_signal_cylinder_PKF"));
            var sig_cylinder_GV6 = Convert.ToInt16(tag_manager.Current.getTagValue("Robot_signal_cylinder_PKF"));


            var sig_cylinder_GVT1 = Convert.ToInt16(tag_manager.Current.getTagValue("CA1_signal_cylinder_FZB"));
            var sig_cylinder_GVT2 = Convert.ToInt16(tag_manager.Current.getTagValue("CA2_signal_cylinder_FZB"));
            var sig_cylinder_GVT3 = Convert.ToInt16(tag_manager.Current.getTagValue("CA3_signal_cylinder_FZB"));
            var sig_cylinder_GVT4 = Convert.ToInt16(tag_manager.Current.getTagValue("CA4_signal_cylinder_FZB"));
            var sig_cylinder_GVT5 = Convert.ToInt16(tag_manager.Current.getTagValue("CA5_signal_cylinder_FZB"));
            var sig_cylinder_GVT6 = Convert.ToInt16(tag_manager.Current.getTagValue("Robot_signal_cylinder_FZB"));

            Path_BV1.Fill = sig_BV1 == 1 ? Mwin.brush_run : Mwin.brush_white; //1:work 2:home
            Path_BV2.Fill = sig_BV2 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SP1 = sig_sp1 == 2 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.RP1 = sig_rp1 == 2 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SP2 = sig_sp2 == 2 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.RP2 = sig_rp2 == 2 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SV0 = sig_SV0 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SV1 = sig_SV1 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SV2 = sig_SV2 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SV3 = sig_SV3 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SV4 = sig_SV4 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.SV5 = sig_SV5 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.MPG1 = sig_MPG1 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.MPG2 = sig_MPG2 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.MPG3 = sig_MPG3 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.MPG4 = sig_MPG4 == 1 ? Mwin.brush_run : Mwin.brush_white;
            _viewModel.MPG5 = sig_MPG5 == 1 ? Mwin.brush_run : Mwin.brush_white;

            for (int i = 0; i < 6; i++)
            {
                switch (sig_FZB[i])
                {
                    case 0:
                        Ell_TP[i].Fill = Mwin.brush_white; //Undefined
                        break;
                    case 1:
                        Ell_TP[i].Fill = Mwin.brush_white;//Stopped
                        break;
                    case 2:
                        Ell_TP[i].Fill = Mwin.brush_run;//RotateAcc
                        break;
                    case 3:
                        Ell_TP[i].Fill = Mwin.brush_run;//Rotateidle
                        break;
                    case 4:
                        Ell_TP[i].Fill = Mwin.brush_run;//Rotate
                        break;
                    case 5:
                        Ell_TP[i].Fill = Mwin.brush_alarm;//ErrStopped
                        break;
                    case 6:
                        Ell_TP[i].Fill = Mwin.brush_alarm;//ErrIdle
                        break;
                    case 7:
                        Ell_TP[i].Fill = Mwin.brush_alarm;//ErrDec
                        break;
                }
            }

            path_GV0.Fill = sig_cylinder_GV0 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GV1.Fill = sig_cylinder_GV1 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GV2.Fill = sig_cylinder_GV2 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GV3.Fill = sig_cylinder_GV3 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GV4.Fill = sig_cylinder_GV4 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GV5.Fill = sig_cylinder_GV5 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GV6.Fill = sig_cylinder_GV6 == 1 ? Mwin.brush_run : Mwin.brush_white;

            path_GVT1.Fill = sig_cylinder_GVT1 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GVT2.Fill = sig_cylinder_GVT2 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GVT3.Fill = sig_cylinder_GVT3 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GVT4.Fill = sig_cylinder_GVT4 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GVT5.Fill = sig_cylinder_GVT5 == 1 ? Mwin.brush_run : Mwin.brush_white;
            path_GVT6.Fill = sig_cylinder_GVT6 == 1 ? Mwin.brush_run : Mwin.brush_white;


        }
       

        private void box_CA_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Rectangle).Name;
            var dialog = new box_dialog(name);
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
        }
        private void Ell_TP_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Ellipse).Name;//Ell_TP1
            var dialog = new Win_TP(name);
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();

        }
        private void box_glove_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new gloveBoxCtrl();
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
        }
        private void BV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Canvas).Name; //BV1
            var dialog = new open_close();
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)  //
            {
                tag_manager.Current.pulseTagbit(name + "_set", 2, 1000);
            }
            else
            {
                if (dialog.DialogResult == false)
                {
                    tag_manager.Current.pulseTagbit(name + "_set", 1, 1000);
                }
               
            }

        }
        private void SV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Rectangle).Name;
            var dialog = new open_close();
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                switch (name)
                {
                    case "SV0":
                        tag_manager.Current.pulseTagbit("Gen_set_cylinder_CBF", 2, 1000);
                        break;
                    case "SV1":
                        tag_manager.Current.pulseTagbit("CA1_set_cylinder_CBF", 2, 1000);
                        break;
                    case "SV2":
                        tag_manager.Current.pulseTagbit("CA2_set_cylinder_CBF", 2, 1000);
                        break;
                    case "SV3":
                        tag_manager.Current.pulseTagbit("CA3_set_cylinder_CBF", 2, 1000);
                        break;
                    case "SV4":
                        tag_manager.Current.pulseTagbit("CA4_set_cylinder_CBF", 2, 1000);
                        break;
                    case "SV5":
                        tag_manager.Current.pulseTagbit("CA5_set_cylinder_CBF", 2, 1000);
                        break;
                }

            }
            else
            {
                if (dialog.DialogResult == false)
                {
                    switch (name)
                    {
                        case "SV0":
                            tag_manager.Current.pulseTagbit("Gen_set_cylinder_CBF", 1, 1000);
                            break;
                        case "SV1":
                            tag_manager.Current.pulseTagbit("CA1_set_cylinder_CBF", 1, 1000);
                            break;
                        case "SV2":
                            tag_manager.Current.pulseTagbit("CA2_set_cylinder_CBF", 1, 1000);
                            break;
                        case "SV3":
                            tag_manager.Current.pulseTagbit("CA3_set_cylinder_CBF", 1, 1000);
                            break;
                        case "SV4":
                            tag_manager.Current.pulseTagbit("CA4_set_cylinder_CBF", 1, 1000);
                            break;
                        case "SV5":
                            tag_manager.Current.pulseTagbit("CA5_set_cylinder_CBF", 1, 1000);
                            break;
                    }
                }

            }
        }
        private void GV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Canvas).Name; //GV1
            var dialog = new open_close();
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                switch (name)
                {
                    case "GV0":
                        tag_manager.Current.pulseTagbit("Gen_set_cylinder_PKF", 2, 1000);
                        break;
                    case "GV1":
                        tag_manager.Current.pulseTagbit("CA1_set_cylinder_PKF", 2, 1000);
                        break;
                    case "GV2":
                        tag_manager.Current.pulseTagbit("CA2_set_cylinder_PKF", 2, 1000);
                        break;
                    case "GV3":
                        tag_manager.Current.pulseTagbit("CA3_set_cylinder_PKF", 2, 1000);
                        break;
                    case "GV4":
                        tag_manager.Current.pulseTagbit("CA4_set_cylinder_PKF", 2, 1000);
                        break;
                    case "GV5":
                        tag_manager.Current.pulseTagbit("CA5_set_cylinder_PKF", 2, 1000);
                        break;
                    case "GV6":
                        tag_manager.Current.pulseTagbit("Robot_set_cylinder_PKF", 2, 1000);
                        break;
                }

            }
            else
            {
                if (dialog.DialogResult == false)
                {
                    switch (name)
                    {
                        case "GV0":
                            tag_manager.Current.pulseTagbit("Gen_set_cylinder_PKF", 1, 1000);
                            break;
                        case "GV1":
                            tag_manager.Current.pulseTagbit("CA1_set_cylinder_PKF", 1, 1000);
                            break;
                        case "GV2":
                            tag_manager.Current.pulseTagbit("CA2_set_cylinder_PKF", 1, 1000);
                            break;
                        case "GV3":
                            tag_manager.Current.pulseTagbit("CA3_set_cylinder_PKF", 1, 1000);
                            break;
                        case "GV4":
                            tag_manager.Current.pulseTagbit("CA4_set_cylinder_PKF", 1, 1000);
                            break;
                        case "GV5":
                            tag_manager.Current.pulseTagbit("CA5_set_cylinder_PKF", 1, 1000);
                            break;
                        case "GV6":
                            tag_manager.Current.pulseTagbit("Robot_set_cylinder_PKF", 1, 1000);
                            break;
                    }
                }

            }
        }
        private void GVT_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Canvas).Name; //GV1
            var dialog = new open_close();
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                switch (name)
                {
                    case "GVT6":
                        tag_manager.Current.pulseTagbit("Robot_set_cylinder_FZB", 2, 1000);
                        break;
                    case "GVT1":
                        tag_manager.Current.pulseTagbit("CA1_set_cylinder_FZB", 2, 1000);
                        break;
                    case "GVT2":
                        tag_manager.Current.pulseTagbit("CA2_set_cylinder_FZB", 2, 1000);
                        break;
                    case "GVT3":
                        tag_manager.Current.pulseTagbit("CA3_set_cylinder_FZB", 2, 1000);
                        break;
                    case "GVT4":
                        tag_manager.Current.pulseTagbit("CA4_set_cylinder_FZB", 2, 1000);
                        break;
                    case "GVT5":
                        tag_manager.Current.pulseTagbit("CA5_set_cylinder_FZB", 2, 1000);
                        break;
                }

            }
            else
            {
                if (dialog.DialogResult == false)
                {
                    switch (name)
                    {
                        case "GVT6":
                            tag_manager.Current.pulseTagbit("Robot_set_cylinder_FZB", 1, 1000);
                            break;
                        case "GVT1":
                            tag_manager.Current.pulseTagbit("CA1_set_cylinder_FZB", 1, 1000);
                            break;
                        case "GVT2":
                            tag_manager.Current.pulseTagbit("CA2_set_cylinder_FZB", 1, 1000);
                            break;
                        case "GVT3":
                            tag_manager.Current.pulseTagbit("CA3_set_cylinder_FZB", 1, 1000);
                            break;
                        case "GVT4":
                            tag_manager.Current.pulseTagbit("CA4_set_cylinder_FZB", 1, 1000);
                            break;
                        case "GVT5":
                            tag_manager.Current.pulseTagbit("CA5_set_cylinder_FZB", 1, 1000);
                            break;

                    }
                }

            }
        }
        private void Path_robot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new robot_operation().ShowDialog();
        }
        private void Ellipse_MPG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (sender as Ellipse).Name;
            var dialog = new open_close();
            var mousePosition = Mouse.GetPosition(this);
            DialogPosition.setDialogPosition(dialog, mousePosition);
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                tag_manager.Current.pulseTagbit(name, 1, 1000);
            }
            else
            {
                if (dialog.DialogResult == false)
                {
                    tag_manager.Current.pulseTagbit(name, 2, 1000);
                }
            }
        }
        private void Ellipse_pump_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
            var mousePosition = Mouse.GetPosition(this);
            var newpos = new Point
            {
                X = mousePosition.X - 330,
                Y = mousePosition.Y+50
            };

            DialogPosition.setDialogPosition(dialog, newpos);

            // 显示弹窗
            dialog.ShowDialog();
        }


    }
}
