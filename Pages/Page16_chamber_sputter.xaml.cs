using common.tag;
using Org.BouncyCastle.Utilities.Collections;
using R2R.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace R2R.Pages
{
    /// <summary>
    /// Page16_chamber_sputter.xaml 的交互逻辑
    /// </summary>
    public partial class Page16_chamber_sputter : Page
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_CAbox = new System.Windows.Threading.DispatcherTimer();
        public event EventHandler NameChanged;
        protected virtual void OnNameChanged()
        {
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        //string boxName;
        private string _BoxName;
        public string BoxName
        {
            get { return _BoxName; }
            set
            {
                if (_BoxName != value)
                {
                    if (Convert.ToBoolean(tag_manager.Current.getTagValue(_BoxName + "_set_LidEnable")))
                    {
                        tag_manager.Current.setTagValue(_BoxName + "_set_LidEnable", 0);
                    }
                    _BoxName = value;
                    OnNameChanged();
                }
            }
        }

        // 定义一个OnPropertyChanged方法，用于触发PropertyChanged事件
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Page16_chamber_sputter()
        {
            InitializeComponent();
            NameChanged += boxNameChanged;

            dispatcherTimer_CAbox.Interval = new TimeSpan(0, 0, 0, 0, 300);
            dispatcherTimer_CAbox.Tick += new EventHandler(TimeAction);
            dispatcherTimer_CAbox.Start();
        }

        public Page16_chamber_sputter(string boxname)
        {
            InitializeComponent();
            NameChanged += boxNameChanged;
            BoxName = boxname;
        }

        private void boxNameChanged(object sender, EventArgs e)
        {
           
            // 在这里处理NameChanged事件
            LidEnable.varName = BoxName + "_set_LidEnable";
            #region test
            //if (BoxName == "CA1")
            //{
            //    LidEnable.varName = "A/IsFault";
            //}
            //else
            //{
            //    LidEnable.varName = "B/IsFault";
            //}
            #endregion
            //电机
            Uctrl_serve_SLP.serveName = BoxName + "_serve_SLP";
            Uctrl_serve_FZB.serveName = BoxName + "_serve_FZB";
            Uctrl_serve_HB1.serveName = BoxName + "_serve_HB1";
            Uctrl_serve_HB2.serveName = BoxName + "_serve_HB2";
            Uctrl_serve_XZ.serveName = BoxName + "_serve_XZ";
            Lid_limit_bit0.varName = Lid_limit_bit2.varName = Lid_limit_bit3.varName = Lid_limit_bit4.varName 
                                   = Lid_limit_bit5.varName = Lid_limit_bit6.varName = BoxName + "_signal_ChamberLid_LimitInfo";
            if (BoxName.Substring(0,2) == "CA")
            {
                Uctrl_serve_SLP.combo.SelectedIndex = App.servemode[BoxName][1];
                Uctrl_serve_FZB.combo.SelectedIndex = App.servemode[BoxName][2];
                Uctrl_serve_HB1.combo.SelectedIndex = App.servemode[BoxName][3];
                Uctrl_serve_HB2.combo.SelectedIndex = App.servemode[BoxName][4];
                Uctrl_serve_XZ.combo.SelectedIndex = App.servemode[BoxName][3];
            }
           
            //Uctrl_serve_XZ.serveName = BoxName + "_serve_XZ";
            //电源
            Uctrl_ADL.boxName = BoxName;
            Uctrl_RF.boxName = BoxName;
            //Uctrl_heater.DCName = BoxName + "_heater";
            //Uctrl_DC2.DCName = BoxName + "_DC2";
            //真空
            value_MPG.varName = BoxName + "_value_MPG";
            value_CDG.varName = BoxName + "_value_CDG";
            value_PSG.varName = BoxName + "_value_PSG";
            //分子泵
            signal_TPM_TX.varName = BoxName + "_signal_TPM_TX";
            value_TPM_I.varName = BoxName + "_value_FZB_I";
            value_TPM_speed.varName = BoxName + "_value_FZB_speed";
            value_TPM_tempe.varName = BoxName + "_value_FZB_tempe";
            tagCtr_starLimit.varName = BoxName + "_set_FZB_VacuumLmt";
            lowSpeed_set.varName = BoxName + "_set_FZB_lowSpeed";
            tagCtr_errMessage.varName = BoxName + "_err_str_FZB";
            FZB_limit_bit0.varName = FZB_limit_bit1.varName = FZB_limit_bit3.varName = FZB_limit_bit4.varName = FZB_limit_bit5.varName
                                   = FZB_limit_bit6.varName = FZB_limit_bit7.varName = BoxName + "_signal_FZB_LimitInfo";

            //MFC
            set_MFC_Ar.varName = BoxName + "_set_MFC_flow_Ar";
            set_MFC_O2.varName = BoxName + "_set_MFC_flow_O2";
            set_MFC_N2.varName = BoxName + "_set_MFC_flow_N2";
            value_MFC_Ar.varName = BoxName + "_value_MFC_flow_Ar";
            value_MFC_O2.varName = BoxName + "_value_MFC_flow_O2";
            value_MFC_N2.varName = BoxName + "_value_MFC_flow_N2";

            //气动阀
            CBF_limit_bit0.varName = CBF_limit_bit1.varName = BoxName + "_signal_cylinder_CBF_LimitInfo";
            HB_limit_bit0.varName = HB_limit_bit1.varName  = BoxName + "_signal_cylinder_HB_LimitInfo";
            GG1_limit_bit0.varName = GG1_limit_bit1.varName = GG1_limit_bit2.varName = GG1_limit_bit3.varName = GG1_limit_bit5.varName = GG1_limit_bit6.varName = BoxName + "_signal_cylinder_GG1_LimitInfo";
            GG2_limit_bit0.varName = GG2_limit_bit1.varName = GG2_limit_bit2.varName = GG2_limit_bit3.varName = GG2_limit_bit5.varName = GG2_limit_bit6.varName = GG2_limit_bit1.varName = BoxName + "_signal_cylinder_GG2_LimitInfo";
            FZBQJ_limit_bit0.varName = FZBQJ_limit_bit1.varName = FZBQJ_limit_bit2.varName = FZBQJ_limit_bit3.varName = FZBQJ_limit_bit5.varName = FZBQJ_limit_bit6.varName = BoxName + "_signal_cylinder_FZB_LimitInfo";
            PKF_limit_bit0.varName = PKF_limit_bit1.varName = PKF_limit_bit2.varName  = BoxName + "_signal_cylinder_PKF_LimitInfo";


        }
        
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                var sig_Ell_MPG = tag_manager.Current.getTagValue(BoxName + "signal_MPG");
                var sig_Ell_CDG = tag_manager.Current.getTagValue(BoxName + "signal_CDG");
                var sig_Ell_PSG = tag_manager.Current.getTagValue(BoxName + "signal_PSG");
                var sig_Path_GV = tag_manager.Current.getTagValue(BoxName + "signal_GV");
                var sig_cylinder_CBF = tag_manager.Current.getTagValue(BoxName + "_signal_cylinder_CBF");
                //var sig_cylinder_HB = tag_manager.Current.getTagValue(BoxName + "_signal_cylinder_HB");
                var sig_cylinder_GG1 = tag_manager.Current.getTagValue(BoxName + "_signal_cylinder_GG1");
                var sig_cylinder_GG2 = tag_manager.Current.getTagValue(BoxName + "_signal_cylinder_GG2");
                var sig_cylinder_FZB = tag_manager.Current.getTagValue(BoxName + "_signal_cylinder_FZB");
                var sig_cylinder_PKF = tag_manager.Current.getTagValue(BoxName + "_signal_cylinder_PKF");


                var sig_FZB = Convert.ToInt16(tag_manager.Current.getTagValue(BoxName + "_signal_FZB"));

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

                if (sig_Ell_MPG != null) Ell_MPG.Fill = (bool)sig_Ell_MPG ? Mwin.brush_run : Mwin.brush_general;
                if (sig_Ell_CDG != null) Ell_CDG.Fill = (bool)sig_Ell_CDG ? Mwin.brush_run : Mwin.brush_general;
                if (sig_Ell_PSG != null) Ell_PSG.Fill = (bool)sig_Ell_PSG ? Mwin.brush_run : Mwin.brush_general;
                if (sig_Path_GV != null) Path_GV.Fill = (bool)sig_Path_GV ? Mwin.brush_run : Mwin.brush_general;
                if (sig_cylinder_CBF != null)
                {
                    CBF_work.Background = Convert.ToInt16(sig_cylinder_CBF) == 1 ? Mwin.brush_run : Mwin.brush_general;// 1 work位 2 home位
                    CBF_home.Background = Convert.ToInt16(sig_cylinder_CBF) == 2 ? Mwin.brush_run : Mwin.brush_general;// 1 work位 2 home位
                }
                //if (sig_cylinder_HB != null)
                //{
                //    HB_work.Background = Convert.ToInt16(sig_cylinder_HB) == 1 ? Mwin.brush_run : Mwin.brush_general;
                //    HB_home.Background = Convert.ToInt16(sig_cylinder_HB) == 2 ? Mwin.brush_run : Mwin.brush_general;
                //}
                if (sig_cylinder_GG1 != null)
                {
                    GG1_work.Background = Convert.ToInt16(sig_cylinder_GG1) == 1 ? Mwin.brush_run : Mwin.brush_general;
                    GG1_home.Background = Convert.ToInt16(sig_cylinder_GG1) == 2 ? Mwin.brush_run : Mwin.brush_general;
                }
                if (sig_cylinder_GG2 != null)
                {
                    GG2_work.Background = Convert.ToInt16(sig_cylinder_GG2) == 1 ? Mwin.brush_run : Mwin.brush_general;
                    GG2_home.Background = Convert.ToInt16(sig_cylinder_GG2) == 2 ? Mwin.brush_run : Mwin.brush_general;
                }
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
                state_QG.Background = tag_manager.Current.getTagbit(BoxName + "_signal_ChamberLid_run", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                heater_state.Background = tag_manager.Current.getTagbit(BoxName + "_signal_heater", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

                MFC_Ar_on.Background = tag_manager.Current.getTagbit(BoxName + "_signal_MFC_Ar", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;
                MFC_O2_on.Background = tag_manager.Current.getTagbit(BoxName + "_signal_MFC_O2", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;
                MFC_N2_on.Background = tag_manager.Current.getTagbit(BoxName + "_signal_MFC_N2", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;

                var realmode_dc1 = Convert.ToInt16(tag_manager.Current.getTagValue("CA4_value_DC1_ActivePowerMode"));
                switch (realmode_dc1)
                {
                    case 0:
                        DC1_actMode.varName = "系统模式";
                        break;
                    case 1:
                        DC1_actMode.varName = "普通模式";
                        break;
                    case 2:
                        DC1_actMode.varName = "梯度模式";
                        break;
                }
                var realmode_dc2 = Convert.ToInt16(tag_manager.Current.getTagValue("CA4_value_DC2_ActivePowerMode"));
                switch (realmode_dc2)
                {
                    case 0:
                        DC2_actMode.varName = "系统模式";
                        break;
                    case 1:
                        DC2_actMode.varName = "普通模式";
                        break;
                    case 2:
                        DC2_actMode.varName = "梯度模式";
                        break;
                }
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
                    tag_manager.Current.pulseTagbit(BoxName + "_set_cylinder_" + aim, 1, 1000);
                }
                else
                {
                    tag_manager.Current.pulseTagbit(BoxName + "_set_cylinder_" + aim, 2, 1000);
                }
            }
            catch
            {

            }
        }

        private void Button_QG_enable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(tag_manager.Current.getTagValue(BoxName + "_set_LidEnable")))
                {
                    tag_manager.Current.setTagValue(BoxName + "_set_LidEnable", 0);
                }
                else
                {
                    tag_manager.Current.setTagValue(BoxName + "_set_LidEnable", 1);
                }
            }
            catch (Exception)
            {

            }
           
           
        }


        private void Button_FZB_Click(object sender, RoutedEventArgs e)
        {
            var name = (string)((Button)sender).Content;
            switch (name)
            {
                case "ON":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 1, 1000);
                    break;
                case "OFF":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 2, 1000);
                    break;
                case "Reset":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 3, 1000);
                    break;
                case "LowSpd":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 4, 1000);
                    break;
                case "Para":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 5, 1000);
                    break;
                case "Clear":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 6, 1000);
                    break;
                case "HiSpd":
                    tag_manager.Current.pulseTagbit(BoxName + "_set_FZB", 7, 1000);
                    break;
                default:
                    break;
            }
        }

        private void Ell_MPG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var name = (sender as Ellipse).Name;//Ell_TP0
            var mousePosition = Mouse.GetPosition(this);
            var dialog = new open_close();
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y;
            dialog.Left = left;
            dialog.Top = top;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                tag_manager.Current.pulseTagbit(BoxName + "_set_MPG", 1, 1000);// CA1_set_MPG
            }
            else
            {
                tag_manager.Current.pulseTagbit(BoxName + "_set_MPG", 2, 1000);
            }
        }

        private void MFC_on_Click(object sender, RoutedEventArgs e) //CA1_set_ctrl_MFC_Ar
        {
            var name = (sender as Button).Name; //MFC_Ar_on
            var subname = name.Substring(0,name.Length-3);
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

        private void heater_on_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit("CA2_set_heater", 1, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void heater_off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit("CA2_set_heater", 2, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void heater_reset_Click(object sender, RoutedEventArgs e)
        {
            tag_manager.Current.pulseTagbit("CA2_set_heater", 3, 1000);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                ComboBox combo = sender as ComboBox;
                var name = combo.Name.Substring(combo.Name.Length-3);//DC1
                try
                {
                    switch (combo.SelectedIndex)
                    {
                        case 0:
                            tag_manager.Current.setTagValue("CA4_set_" + name + "_PowerMode", 0);
                            break;
                        case 1:
                            tag_manager.Current.setTagValue("CA4_set_" + name + "_PowerMode", 1);
                            break;
                        case 2:
                            tag_manager.Current.setTagValue("CA4_set_" + name + "_PowerMode", 2);
                            break;
                    }
                }
                catch (Exception)
                {

                }
               
            }
                
        }

        private void on_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = (sender as Button).Tag.ToString();
                tag_manager.Current.pulseTagbit("CA4_set_" + name + "_EvPower", 1,1000);
            }
            catch (Exception)
            {

            }
        }

        private void off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = (sender as Button).Tag.ToString();
                tag_manager.Current.pulseTagbit("CA4_set_" + name + "_EvPower", 2, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = (sender as Button).Tag.ToString();
                tag_manager.Current.pulseTagbit("CA4_set_" + name + "_EvPower", 3, 1000);
            }
            catch (Exception)
            {

            }
        }
    }
}
