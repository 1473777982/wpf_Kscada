using common.tag;
using R2R.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace R2R.Windows
{
    /// <summary>
    /// Win_TP.xaml 的交互逻辑
    /// </summary>
    public partial class Win_TP : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_CAbox = new System.Windows.Threading.DispatcherTimer();
        string name;
        string boxName = "";
        public Win_TP()
        {
            InitializeComponent();
        }
        public Win_TP(string name)
        {
            InitializeComponent();
            dispatcherTimer_CAbox.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_CAbox.Tick += new EventHandler(TimeAction);
            dispatcherTimer_CAbox.Start();
            string subName = name.Substring(4);
           
            switch (name)
            {
                case "Ell_TP6":
                    boxName = "Robot";
                    break;
                case "Ell_TP1":
                    boxName = "CA1";
                    break;
                case "Ell_TP2":
                    boxName = "CA2";
                    break;
                case "Ell_TP3":
                    boxName = "CA3";
                    break;
                case "Ell_TP4":
                    boxName = "CA4";
                    break;
                case "Ell_TP5":
                    boxName = "CA5";
                    break;
                default:
                    break;
            }
            this.Title = subName;
            this.name = boxName + "_set_FZB";
            tagCtr_speed.varName = boxName + "_value_FZB_speed";
            tagCtr_starLimit.varName = boxName + "_set_FZB_VacuumLmt";
            lowSpeed_set.varName = boxName + "_set_FZB_lowSpeed";
            tagCtr_errMessage.varName = boxName + "_err_str_FZB";
        }
        private void TimeAction(object sender, EventArgs e)
        {
            var sig_FZB = Convert.ToInt16(tag_manager.Current.getTagValue(boxName + "_signal_FZB"));

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
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = (string)((Button)sender).Content;
            switch (name)
            {
                case "ON":
                    tag_manager.Current.pulseTagbit(this.name, 1, 1000);
                    break;
                case "OFF":
                    tag_manager.Current.pulseTagbit(this.name, 2, 1000);
                    break;
                case "Reset":
                    tag_manager.Current.pulseTagbit(this.name, 3, 1000);
                    break;
                case "LowSpd":
                    tag_manager.Current.pulseTagbit(this.name, 4, 1000);
                    break;
                case "Para":
                    tag_manager.Current.pulseTagbit(this.name, 5, 1000);
                    break;
                case "Clear":
                    tag_manager.Current.pulseTagbit(this.name, 6, 1000);
                    break;
                case "HiSpd":
                    tag_manager.Current.pulseTagbit(this.name, 7, 1000);
                    break;
                default:
                    break;
            }          
        }
    }
}
