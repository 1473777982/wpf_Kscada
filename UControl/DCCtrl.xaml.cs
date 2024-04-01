using common.tag;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace R2R.UControl
{
    /// <summary>
    /// DCCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class DCCtrl : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_ADL = new System.Windows.Threading.DispatcherTimer();

        #region 属性
        public string boxName
        {
            get { return (string)GetValue(_boxName); }
            set { SetValue(_boxName, value); }
        }
        public static readonly DependencyProperty _boxName =
           DependencyProperty.Register("DCName", typeof(string), typeof(DCCtrl), new UIPropertyMetadata("", OnDCNameChanged));
        private static void OnDCNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as DCCtrl)?.dcNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void dcNameChanged(string oldValue, string newValue)
        {

            try
            {
                set_U.varName = boxName + "_set_ADL_U";//CA1_set_ADL_U
                set_I.varName = boxName + "_set_ADL_I";
                set_P.varName = boxName + "_set_ADL_P";
                value_U.varName = boxName + "_value_ADL_U";//CA1_value_ADL_U
                value_I.varName = boxName + "_value_ADL_I";
                value_P.varName = boxName + "_value_ADL_P";
                value_Setpoint.varName = boxName + "_value_ADL_Setpoint";
                value_Arc.varName = boxName + "_signal_ADL_HardArc";
                limit_bit0.varName = limit_bit2.varName = limit_bit3.varName = limit_bit4.varName
                                   = limit_bit5.varName = limit_bit6.varName = boxName + "_signal_ADL_LimitInfo";
            }
            catch (Exception)
            {
               
            }
            
        }

        #endregion
        public DCCtrl()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                dispatcherTimer_ADL.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dispatcherTimer_ADL.Tick += new EventHandler(TimeAction);
                dispatcherTimer_ADL.Start();
            }
        }
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                cnnState.Background = tag_manager.Current.getTagbit(boxName + "_signal_ADL_power", 7) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                enState.Background = tag_manager.Current.getTagbit(boxName + "_signal_ADL_power", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                interLock.Background = tag_manager.Current.getTagbit(boxName + "_signal_ADL_power", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                errState.Background = tag_manager.Current.getTagbit(boxName + "_signal_ADL_power", 3) == 1 ? Mwin.brush_alarm : Mwin.brush_tagRead;
                var realmode = Convert.ToInt16(tag_manager.Current.getTagValue(boxName + "_signal_ADL_WorkMode"));
                switch (realmode)
                {
                    case 0:
                        actMode.varName = "电压模式";
                        break;
                    case 1:
                        actMode.varName = "电流模式";
                        break;
                    case 2:
                        actMode.varName = "功率模式";
                        break;
                }

                stair_enable.Background = Convert.ToBoolean(tag_manager.Current.getTagValue(boxName + "_set_ADL_Stair_enable")) ? Mwin.brush_run : Mwin.brush_general;
                Stair_1.varName = boxName + "_set_ADL_Stair_1";
                Stair_2.varName = boxName + "_set_ADL_Stair_2";
                Stair_3.varName = boxName + "_set_ADL_Stair_3";
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
                            tag_manager.Current.setTagValue(boxName + "_set_ADL_WorkMode", 2);
                            break;
                        case 1:
                            tag_manager.Current.setTagValue(boxName + "_set_ADL_WorkMode", 0);
                            break;
                        case 2:
                            tag_manager.Current.setTagValue(boxName + "_set_ADL_WorkMode", 1);
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
                tag_manager.Current.pulseTagbit(boxName + "_set_ADL_state", 1, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit(boxName + "_set_ADL_state", 2, 1000);
            }
            catch (Exception)
            {

            }

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit(boxName + "_set_ADL_state", 3, 1000);
            }
            catch (Exception)
            {

            }
        }

        private void stair_enable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(tag_manager.Current.getTagValue(boxName + "_set_ADL_Stair_enable")))//判断使能状态  
                {
                    tag_manager.Current.setTagValue(boxName + "_set_ADL_Stair_enable", 0);
                }
                else
                {
                    tag_manager.Current.setTagValue(boxName + "_set_ADL_Stair_enable", 1);
                }
            }
            catch (System.Exception)
            {

            }
        }
    }
}
