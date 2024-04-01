using common.tag;
using System;
using System.Windows;
using System.Windows.Controls;

namespace R2R.Windows
{
    /// <summary>
    /// Win_SP_RP.xaml 的交互逻辑
    /// </summary>
    public partial class Win_SP_RP : Window
    {
        string name;
        public Win_SP_RP()
        {
            InitializeComponent();
        }
        public Win_SP_RP(string name)
        {
            InitializeComponent();
            this.name = name;
            this.Title = name;
        }

        private void Pump_Click(object sender, RoutedEventArgs e)
        {
            var bname = (sender as Button).Name;
            try
            {
                if (!Convert.ToBoolean(tag_manager.Current.getTagValue("Gen_set_" + name + "_" + bname)))
                {
                    tag_manager.Current.pulseTagValue("Gen_set_"+ name +"_"+ bname, 1000);
                }
            }
            catch (System.Exception)
            {

            }
        }
    }
}
