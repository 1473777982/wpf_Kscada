using common.tag;
using System.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace R2R.UControl
{
    /// <summary>
    /// serveControl_rol.xaml 的交互逻辑
    /// </summary>
    public partial class serveControl_rol : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_serve = new System.Windows.Threading.DispatcherTimer();

        string boxname;
        string servename;
        int servebitIndex = 3;

        #region 属性
        public string serveName
        {
            get { return (string)GetValue(_serveName); }
            set { SetValue(_serveName, value); }
        }
        public static readonly DependencyProperty _serveName =
           DependencyProperty.Register("serveName", typeof(string), typeof(serveControl_rol), new UIPropertyMetadata("", OnServeNameChanged));
        private static void OnServeNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as serveControl_rol)?.serveNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void serveNameChanged(string oldValue, string newValue)
        {
            //if (base.IsLoaded)
            //{ 
            // mame更改后重新设置标签
            try
            {
                
                //button_forward.IsEnabled = false;
                //button_backward.IsEnabled = false;
                button_jog_p.IsEnabled = false;
                button_jog_n.IsEnabled = false;

                boxname = newValue.Substring(0, 3);
                servename = newValue.Substring(3);
                speed_value.varName = boxname + "_value" + servename + "_speed";
                position_value.varName = boxname + "_value" + servename + "_position";                
                combo.SelectedIndex = -1;
                stateStr.varName = boxname + "_value_serve_errMsg_XZ";
                vel_set.varName = boxname + "_set_serve_XZ_vel_low";
            }
            catch
            {

            }
            //}
        }
        #endregion
        public serveControl_rol()
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
                button_start.Background = tag_manager.Current.getTagbit(boxname + "_set_serve_XZ_vel", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;

                //CA4_signal_serve_XZ
                state0.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state1.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state2.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 2) == 1 ? Mwin.brush_alarm : Mwin.brush_tagRead;
                state3.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state4.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 4) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state5.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 5) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                state6.Background = tag_manager.Current.getTagbit(boxname + "_signal" + servename, 6) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
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
                case 0://速度
                    //button_forward.IsEnabled = true;
                    //button_backward.IsEnabled = true;
                    button_jog_p.IsEnabled = false;
                    button_jog_n.IsEnabled = false;
                    button_start.IsEnabled = true;
                    App.servemode[boxname][servebitIndex] = 0;
                    s = "vel";
                    break;
                case 1://jog
                    //button_forward.IsEnabled = false;
                    //button_backward.IsEnabled = false;
                    button_jog_p.IsEnabled = true;
                    button_jog_n.IsEnabled = true;
                    button_start.IsEnabled = false;
                    App.servemode[boxname][servebitIndex] = 1;
                    s = "jog";
                    break;
            }

            
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

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value = tag_manager.Current.getTagbit(boxname + "_set_serve_XZ_vel", 1);
                if (value == 1)
                {
                    tag_manager.Current.setTagbit(boxname + "_set_serve_XZ_vel", 1, 0);
                }
                else
                {
                    tag_manager.Current.setTagbit(boxname + "_set_serve_XZ_vel", 1, 1);
                }
                
            }
            catch (Exception)
            {

            }
        }

        //private void button_forward_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 1, 1);
        //        ((Button)sender).CaptureMouse();
        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //}

        //private void button_forward_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 1, 0);
        //        ((Button)sender).ReleaseMouseCapture();
        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //}

        //private void button_backward_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 2, 1);
        //        ((Button)sender).CaptureMouse();
        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //}

        //private void button_backward_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        tag_manager.Current.setTagbit(boxname + "_set" + servename + "_abs", 2, 0);
        //        ((Button)sender).ReleaseMouseCapture();
        //    }
        //    catch (System.Exception)
        //    {

        //    }
        //}


        //    private void button_start_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //    {
        //        try
        //        {
        //            tag_manager.Current.setTagbit(boxname + "_set" + servename + "_rel", 1, 0);
        //            ((Button)sender).ReleaseMouseCapture();
        //        }
        //        catch (System.Exception)
        //        {

        //        }
        //    }

        //    private void button_start_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //    {
        //        try
        //        {
        //            tag_manager.Current.setTagbit(boxname + "_set" + servename + "_rel", 1, 1);
        //            ((Button)sender).ReleaseMouseCapture();
        //        }
        //        catch (System.Exception)
        //        {

        //        }
        //    }      
    }
}
