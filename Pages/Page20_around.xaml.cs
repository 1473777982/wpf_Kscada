using common.tag;
using System;
using System.Windows.Controls;
using System.Xml.Linq;

namespace R2R.Pages
{
    /// <summary>
    /// Page20_around.xaml 的交互逻辑
    /// </summary>
    public partial class Page20_around : Page
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_page20 = new System.Windows.Threading.DispatcherTimer();
        public Page20_around()
        {
            InitializeComponent();
            dispatcherTimer_page20.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_page20.Tick += new EventHandler(TimeAction);
            dispatcherTimer_page20.Start();
        }
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                sp1_run.Background = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump1_sp")) == 2 ? Mwin.brush_run : Mwin.brush_tagRead ;
                rp1_run.Background = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump1_rp")) == 2 ? Mwin.brush_run : Mwin.brush_tagRead;
                sp2_run.Background = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump2_sp")) == 2 ? Mwin.brush_run : Mwin.brush_tagRead;
                rp2_run.Background = Convert.ToInt16(tag_manager.Current.getTagValue("Gen_signal_pump2_rp")) == 2 ? Mwin.brush_run : Mwin.brush_tagRead;

                status1_0.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_1.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_2.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_3.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_4.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 4) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_5.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 5) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_6.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 6) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status1_7.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_1", 7) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

                status2_0.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_1.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_2.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_3.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_4.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 4) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_5.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 5) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_6.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 6) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status2_7.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_2", 7) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

                status3_0.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 0) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_1.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 1) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_2.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 2) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_3.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 3) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_4.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 4) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_5.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 5) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_6.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 6) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;
                status3_7.Background = tag_manager.Current.getTagbit("Gen_signal_glove_status_3", 7) == 1 ? Mwin.brush_run : Mwin.brush_tagRead;

                Ar_on.Background = tag_manager.Current.getTagbit("Gen_signal_ArtGas", 3) == 1 ? Mwin.brush_run : Mwin.brush_general;
                O2_on.Background = tag_manager.Current.getTagbit("Gen_signal_ArtGas", 2) == 1 ? Mwin.brush_run : Mwin.brush_general;
                N2_on.Background = tag_manager.Current.getTagbit("Gen_signal_ArtGas", 1) == 1 ? Mwin.brush_run : Mwin.brush_general;

            }
            catch (Exception)
            {

            }
        }
        private void Pump_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var name = (sender as Button).Name;
            try
            {
                if (!Convert.ToBoolean(tag_manager.Current.getTagValue(name)))
                {
                    tag_manager.Current.pulseTagValue(name, 1000);
                    //(sender as Button).Background = Mwin.brush_general;
                }

            }
            catch (System.Exception)
            {

            }
        }

        private void zongfa_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var name = (sender as Button).Name;
            var subname = name.Substring(0, 2); 
            if (name.Contains("on"))
            {
                
                try
                {
                    tag_manager.Current.pulseTagbit("Gen_set_ctrl_GV_" + subname, 1, 1000);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    tag_manager.Current.pulseTagbit("Gen_set_ctrl_GV_" + subname, 2, 1000);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
