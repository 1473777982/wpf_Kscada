using common.tag;
using controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// maintenance.xaml 的交互逻辑
    /// </summary>
    public partial class maintenance : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer_maintenance = new System.Windows.Threading.DispatcherTimer();
        List<tagControl> sets;
        List<tagControl> values; 
        List<TextBlock> states;
        int members = 13;
        public maintenance()
        {
            InitializeComponent();
            sets = new List<tagControl>();
            values = new List<tagControl>();
            states = new List<TextBlock>();
            for (int i = 1; i <= members; i++)
            {
                sets.Add((tagControl)FindName("set"+i));
                values.Add((tagControl)FindName("value" + i));
                states.Add((TextBlock)FindName("state" + i));
            }
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                dispatcherTimer_maintenance.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dispatcherTimer_maintenance.Tick += new EventHandler(TimeAction);
                dispatcherTimer_maintenance.Start();
            }
        }

        private void TimeAction(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                try
                {
                    for (int i = 1; i <= members; i++)
                    {
                        var set = Convert.ToDouble(tag_manager.Current.getTagValue(sets[i-1].varName));
                        var value = Convert.ToDouble(tag_manager.Current.getTagValue(values[i - 1].varName));
                        states[i-1].Background = set <= value ? Mwin.brush_warn : Mwin.brush_tagRead;
                    }
                }
                catch (Exception)
                {

                }
            }
            
        }
        private void FZB_clear_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            try
            {
                tag_manager.Current.pulseTagbit(name, 6, 1000);
            }
            catch (Exception)
            {

            }
            
        }

        private void dianyuan_clear_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            try
            {
                tag_manager.Current.pulseTagbit(name, 4, 1000);
            }
            catch (Exception)
            {

            }
        }

    }
}
