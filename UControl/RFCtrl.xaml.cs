using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;
using common.tag;
using System.Xml.Linq;

namespace R2R.UControl
{
    /// <summary>
    /// RFCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class RFCtrl : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_RF = new System.Windows.Threading.DispatcherTimer();
        #region 属性
        public string boxName
        {
            get { return (string)GetValue(_RFName); }
            set { SetValue(_RFName, value); }
        }
        public static readonly DependencyProperty _RFName =
           DependencyProperty.Register("RFName", typeof(string), typeof(RFCtrl), new UIPropertyMetadata("", OnRFNameChanged));
        private static void OnRFNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as RFCtrl)?.rfNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void rfNameChanged(string oldValue, string newValue)
        {
            try
            {
                // mame更改后重新设置标签
                setpoint.varName = boxName + "_set_RF_setpoint";
                actSet.varName = boxName + "_value_RF_Setpoint";
                value1.varName = boxName + "_value_RF_1";
                value2.varName = boxName + "_value_RF_2";
                value3.varName = boxName + "_value_RF_3";
                value_bias.varName = boxName + "_value_RF_DataEx_1";
                value_irf.varName = boxName + "_value_RF_DataEx_3";
                value_urf.varName = boxName + "_value_RF_DataEx_2";
                limit_bit0.varName = limit_bit2.varName = limit_bit3.varName = limit_bit4.varName
                   = limit_bit5.varName = limit_bit6.varName = boxName + "_signal_RF_LimitInfo";

            }
            catch (Exception)
            {

            }
              
                
            
        }
        #endregion
        public RFCtrl()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                dispatcherTimer_RF.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dispatcherTimer_RF.Tick += new EventHandler(TimeAction);
                dispatcherTimer_RF.Start();
            }
        }
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                cnnState.Background = tag_manager.Current.getTagbit(boxName + "_signal_RF_power", 7) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                enState.Background = tag_manager.Current.getTagbit(boxName + "_signal_RF_power", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                interLock.Background = tag_manager.Current.getTagbit(boxName + "_signal_RF_power", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                errState.Background = tag_manager.Current.getTagbit(boxName + "_signal_RF_power", 3) == 1 ? Mwin.brush_alarm : Mwin.brush_tagRead;
                var realmode = Convert.ToInt16(tag_manager.Current.getTagValue(boxName + "_signal_RF_WorkMode"));
                switch (realmode)
                {
                    case 0:
                        actMode.varName = "mode1";
                        break;
                    case 1:
                        actMode.varName = "mode2";
                        break;
                }
                stair_enable.Background = Convert.ToBoolean(tag_manager.Current.getTagValue(boxName + "_set_RF_Stair_enable"))? Mwin.brush_run : Mwin.brush_general;
                Stair_1.varName = boxName + "_set_RF_Stair_1";
                Stair_2.varName = boxName + "_set_RF_Stair_2";
                Stair_3.varName = boxName + "_set_RF_Stair_3";

            }
            catch (Exception)
            {

            }
           
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                ComboBox comboBox = sender as ComboBox;
                try
                {
                    switch (comboBox.SelectedIndex)
                    {
                        case 0:
                            tag_manager.Current.setTagValue(boxName + "_set_RF_workmode", 0);
                            break;
                        case 1:
                            tag_manager.Current.setTagValue(boxName + "_set_RF_workmode", 1);
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
            // CA1_set_RF_power
            try
            {
                tag_manager.Current.pulseTagbit(boxName + "_set_RF_power", 1, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit(boxName + "_set_RF_power", 2, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit(boxName + "_set_RF_power", 3, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void stair_enable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(tag_manager.Current.getTagValue(boxName + "_set_RF_Stair_enable")))//判断使能状态  
                {
                    tag_manager.Current.setTagValue(boxName + "_set_RF_Stair_enable", 0);
                }
                else
                {
                    tag_manager.Current.setTagValue(boxName + "_set_RF_Stair_enable", 1);
                }
            }
            catch (System.Exception)
            {

            }
        }
    }
}
