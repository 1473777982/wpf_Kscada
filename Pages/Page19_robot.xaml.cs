using common.tag;
using Opc.Ua;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace R2R.Pages
{
    /// <summary>
    /// Page19_robot.xaml 的交互逻辑
    /// </summary>
    public partial class Page19_robot : Page
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_Robot = new System.Windows.Threading.DispatcherTimer();
        public Page19_robot()
        {
            InitializeComponent();
            dispatcherTimer_Robot.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_Robot.Tick += new EventHandler(TimeAction);
            dispatcherTimer_Robot.Start();
        }
        private void TimeAction(object sender, EventArgs e)
        {
            var sig_FZB = Convert.ToInt16(tag_manager.Current.getTagValue("Robot_signal_FZB"));
            switch (sig_FZB)
            {
                case 0:
                    tblock_FZB_state.Background = Mwin.brush_white; //Undefined
                    tblock_FZB_state.Text = "Undefined";
                    break;
                case 1:
                    tblock_FZB_state.Background = Mwin.brush_white;//Stopped
                    tblock_FZB_state.Text = "Stopped";
                    break;
                case 2:
                    tblock_FZB_state.Background = Mwin.brush_run;//RotateAcc
                    tblock_FZB_state.Text = "RotateAcc";
                    break;
                case 3:
                    tblock_FZB_state.Background = Mwin.brush_run;//Rotateidle
                    tblock_FZB_state.Text = "Rotateidle";
                    break;
                case 4:
                    tblock_FZB_state.Background = Mwin.brush_run;//Rotate
                    tblock_FZB_state.Text = "Rotate";
                    break;
                case 5:
                    tblock_FZB_state.Background = Mwin.brush_alarm;//ErrStopped
                    tblock_FZB_state.Text = "ErrStopped";
                    break;
                case 6:
                    tblock_FZB_state.Background = Mwin.brush_alarm;//ErrIdle
                    tblock_FZB_state.Text = "ErrIdle";
                    break;
                case 7:
                    tblock_FZB_state.Background = Mwin.brush_alarm;//ErrDec
                    tblock_FZB_state.Text = "ErrDec";
                    break;
            }

            ell_0_0.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA0", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_0_1.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA0", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_0_2.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA0", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_0_3.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA0", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

            ell_1_0.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA1", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_1_1.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA1", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_1_2.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA1", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_1_3.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA1", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

            ell_2_0.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA2", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_2_1.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA2", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_2_2.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA2", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_2_3.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA2", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

            ell_3_0.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA3", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_3_1.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA3", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_3_2.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA3", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_3_3.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA3", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

            ell_4_0.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA4", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_4_1.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA4", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_4_2.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA4", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_4_3.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA4", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

            ell_5_0.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA5", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_5_1.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA5", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_5_2.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA5", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
            ell_5_3.Fill = tag_manager.Current.getTagbit("Robot_signal_slotState_CA5", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
           
            var sig_cylinder_FZB = tag_manager.Current.getTagValue("Robot_signal_cylinder_FZB");
            var sig_cylinder_PKF = tag_manager.Current.getTagValue("Robot_signal_cylinder_PKF");
            if (sig_cylinder_FZB != null)
            {
                FZB_work.Background = Convert.ToInt16(sig_cylinder_FZB) == 1 ? Mwin.brush_run : Mwin.brush_general;
                FZB_home.Background = Convert.ToInt16(sig_cylinder_FZB) == 2 ? Mwin.brush_run : Mwin.brush_general;
            }
            if (sig_cylinder_PKF != null)
            {
                PKF_work.Background = Convert.ToInt16(sig_cylinder_PKF) == 1 ? Mwin.brush_run : Mwin.brush_general;
                PKF_home.Background = Convert.ToInt16(sig_cylinder_PKF) == 2 ? Mwin.brush_run : Mwin.brush_general;
            }
        }
        private void Button_FZB_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var name = (string)((Button)sender).Content;
            switch (name)
            {
                case "ON":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 1, 1000);
                    break;
                case "OFF":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 2, 1000);
                    break;
                case "Reset":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 3, 1000);
                    break;
                case "LowSpd":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 4, 1000);
                    break;
                case "Para":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 5, 1000);
                    break;
                case "Clear":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 6, 1000);
                    break;
                case "HiSpd":
                    tag_manager.Current.pulseTagbit("Robot_set_FZB", 7, 1000);
                    break;
                default:
                    break;
            }
        }

        //

        private void Ell_MPG_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Button_robot_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string buttonName = (sender as Button).Content.ToString();
            if (buttonName == "Enable")
            {
                try
                {
                    if (Convert.ToBoolean(tag_manager.Current.getTagValue("Robot_set_ctrl_enable")))
                    {
                        tag_manager.Current.setTagValue("Robot_set_ctrl_enable", 0);
                    }
                    else
                    {
                        tag_manager.Current.setTagValue("Robot_set_ctrl_enable", 1);
                    }
                   
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    tag_manager.Current.pulseTagValue("Robot_set_ctrl_" + buttonName, 1000);
                }
                catch (Exception)
                {

                }
            }           
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var box = comboBox.SelectedIndex;
            try
            {
                tag_manager.Current.setTagValue("Robot_set_ctrl_ChamberID", box);
            }
            catch (Exception)
            {

            }
        }

        //气缸
        private void cylinder_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            string aim = name.Substring(0, name.Length - 5);
            string action = name.Substring(name.Length - 4);
            try
            {
                if (action == "home")
                {
                    tag_manager.Current.pulseTagbit("Robot_set_cylinder_" + aim, 1, 1000);
                }
                else
                {
                    tag_manager.Current.pulseTagbit("Robot_set_cylinder_" + aim, 2, 1000);
                }
            }
            catch
            {

            }
        }
    }
}
