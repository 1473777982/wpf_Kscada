using common.tag;
using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace R2R.UControl
{
    /// <summary>
    /// serveControl.xaml 的交互逻辑
    /// </summary>
    public partial class serveControl : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_serve = new System.Windows.Threading.DispatcherTimer();

        string boxname;
        string servename;
        int servebitIndex = 0;

        #region 属性
        public string serveName
        {
            get { return (string)GetValue(_serveName); }
            set { SetValue(_serveName, value); }
        }
        public static readonly DependencyProperty _serveName =
           DependencyProperty.Register("serveName", typeof(string), typeof(serveControl), new UIPropertyMetadata("", OnServeNameChanged));
        private static void OnServeNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as serveControl)?.serveNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void serveNameChanged(string oldValue, string newValue)
        {
            //if (base.IsLoaded)
            //{ 
            // mame更改后重新设置标签
            try
            {
                
                button_forward.IsEnabled = false;
                button_backward.IsEnabled = false;
                button_jog_p.IsEnabled = false;
                button_jog_n.IsEnabled = false;

                boxname = newValue.Substring(0, 3);
                servename = newValue.Substring(3);               
                speed_value.varName = boxname + "_value" + servename + "_speed";
                position_value.varName = boxname + "_value" + servename + "_position";
                

                switch (servename)
                {
                    case "_serve_SLP":
                        servebitIndex = 1;
                        button_forward.Content = "上升";
                        button_backward.Content = "下降";
                        distance.varName = boxname + "_set_serve_SLP_Distance";
                        stateStr.varName = boxname + "_value_serve_errMsg_SLP";
                        button_gear.Visibility = Visibility.Hidden;
                        break;
                    case "_serve_FZB":
                        servebitIndex = 2;
                        button_forward.Content = "打开";
                        button_backward.Content = "关闭";
                        distance.varName = boxname + "_set_serve_FZB_Distance";
                        stateStr.varName = boxname + "_value_serve_errMsg_FZB";
                        button_gear.Visibility = Visibility.Hidden;
                        break;
                    case "_serve_HB1":
                        servebitIndex = 3;
                        button_forward.Content = "上升";
                        button_backward.Content = "下降";
                        distance.varName = boxname + "_set_serve_HB1_Distance";
                        stateStr.varName = boxname + "_value_serve_errMsg_HB1";
                        button_gear.Visibility = Visibility.Hidden;
                        break;
                    case "_serve_HB2":
                        servebitIndex = 4;
                        button_forward.Content = "上升";
                        button_backward.Content = "下降";
                        distance.varName = boxname + "_set_serve_HB2_Distance";
                        stateStr.varName = boxname + "_value_serve_errMsg_HB2";
                        button_gear.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
                combo.SelectedIndex = -1;
                
            }
            catch
            {

            }
            //}
        }
        #endregion
        public serveControl()
        {
            InitializeComponent();           
           
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                dispatcherTimer_serve.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dispatcherTimer_serve.Tick += new EventHandler(TimeAction);
                dispatcherTimer_serve.Start();
            }
        }

        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                button_enable.Background = tag_manager.Current.getTagbit(boxname + "_signal_serve_power", servebitIndex) == 1 ? Mwin.brush_run : Mwin.brush_general;
                //CA1_signal_serve_SLP
                state0.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state1.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state2.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 2) == 1 ? Mwin.brush_alarm : Mwin.brush_tagRead;
                state3.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state4.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 4) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state5.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 5) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state6.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 6) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                button_gear.Background = Convert.ToBoolean(tag_manager.Current.getTagValue(boxname + "_signal" + servename + "_gear")) ? Mwin.brush_run : Mwin.brush_general;
            }
            catch (Exception)
            {

            }
          
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }
            string s = "";
            switch (combo.SelectedIndex)
            {
                //case -1:
                //    button_forward.IsEnabled = false;
                //    button_backward.IsEnabled = false;
                //    button_jog_p.IsEnabled = true;
                //    button_jog_n.IsEnabled = true;
                //    s = "jog";
                    //break;
                case 0://位置
                    button_forward.IsEnabled = true;
                    button_backward.IsEnabled = true;
                    button_jog_p.IsEnabled = false;
                    button_jog_n.IsEnabled = false;
                    button_start.IsEnabled = false;
                    App.servemode[boxname][servebitIndex] = 0;
                    s = "abs";
                    break;
                case 1://jog
                    button_forward.IsEnabled = false;
                    button_backward.IsEnabled = false;
                    button_jog_p.IsEnabled = true;
                    button_jog_n.IsEnabled = true;
                    button_start.IsEnabled = false;
                    App.servemode[boxname][servebitIndex] = 1;
                    s = "jog";
                    break;
                case 2://相对位移
                    button_forward.IsEnabled = false;
                    button_backward.IsEnabled = false;
                    button_jog_p.IsEnabled = false;
                    button_jog_n.IsEnabled = false;
                    button_start.IsEnabled = true;
                    App.servemode[boxname][servebitIndex] = 2;
                    s = "jog";
                    break;
            }

            //speed_set.varName = serveName + "_set_speed_" + s;  //BoxName + "_serve_SLP";
            //position_set.varName = serveName + "_set_position_" + s;
        }

        private void button_enable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tag_manager.Current.getTagbit(boxname + "_set_serve_power", servebitIndex) == 0)
                {
                    tag_manager.Current.setTagbit(boxname + "_set_serve_power", servebitIndex, 1);
                }
                else
                {
                    tag_manager.Current.setTagbit(boxname + "_set_serve_power", servebitIndex, 0);
                }
            }
            catch (System.Exception)
            {

            }
        }
        private void button_reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit(boxname + "_set_serve_reset", servebitIndex, 1000);
            }
            catch
            {

            }
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.pulseTagbit(boxname + "_set_serve_stop", servebitIndex, 1000);
            }
            catch
            {

            }
        }


        //private void button_forward_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        switch (combo.SelectedIndex)
        //        {
        //            case 0:
        //                tag_manager.Current.pulseTagbit(boxname + "_set" + servename + "_abs", 1, 1000);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        //private void button_backward_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        switch (combo.SelectedIndex)
        //        {

        //            case 0:
        //                tag_manager.Current.pulseTagbit(boxname + "_set" + servename + "_abs", 2, 1000);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}

       
        //JOG+
        private void button_jog_p_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_jog", 1, 1);
                ((Button)sender).CaptureMouse();
            }
            catch (System.Exception)
            {

            }
            
        }

        private void button_jog_p_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_jog", 1, 0);
                ((Button)sender).ReleaseMouseCapture();
            }
            catch (System.Exception)
            {

            }
            
        }

        //JOG-
        private void button_jog_n_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_jog", 2, 1);
                ((Button)sender).CaptureMouse();
            }
            catch (System.Exception)
            {

            }
           
        }

        private void button_jog_n_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_jog", 2, 0);
                ((Button)sender).ReleaseMouseCapture();
            }
            catch (System.Exception)
            {

            }
            
        }

        private void button_forward_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 1, 1);
                ((Button)sender).CaptureMouse();
            }
            catch (System.Exception)
            {

            }
        }

        private void button_forward_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 1, 0);
                ((Button)sender).ReleaseMouseCapture();
            }
            catch (System.Exception)
            {

            }
        }

        private void button_backward_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 2, 1);
                ((Button)sender).CaptureMouse();
            }
            catch (System.Exception)
            {

            }
        }

        private void button_backward_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 2, 0);
                ((Button)sender).ReleaseMouseCapture();
            }
            catch (System.Exception)
            {

            }
        }


        private void button_start_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_rel", 1, 0);
                ((Button)sender).ReleaseMouseCapture();
            }
            catch (System.Exception)
            {

            }
        }

        private void button_start_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagbit(boxname + "_set" + servename + "_rel", 1, 1);
                ((Button)sender).ReleaseMouseCapture();
            }
            catch (System.Exception)
            {

            }
        }

        private void button_gear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var gear = Convert.ToBoolean(tag_manager.Current.getTagValue(boxname + "_signal_serve_HB2_gear"));//CA1_signal_serve_HB2_gear
                if (gear)
                {
                    tag_manager.Current.pulseTagbit(boxname + "_set_serve_HB2_gear", 2, 1000); //CA1_set_serve_HB2_gear
                }
                else
                {
                    tag_manager.Current.pulseTagbit(boxname + "_set_serve_HB2_gear", 1, 1000);
                }
            }
            catch (Exception)
            {

            }
           
        }
    }
}
