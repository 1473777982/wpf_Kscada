using common.tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace R2R.Windows
{
    /// <summary>
    /// io_force.xaml 的交互逻辑
    /// </summary>
    public partial class io_force : Window
    {
        string VarName;
        public io_force()
        {
            InitializeComponent();
        }
        public io_force(string VarName)
        {
            InitializeComponent();
            this.VarName = VarName;
        }

        private void force_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagValue(VarName, true);
            }
            catch (Exception)
            {

            }
        }


        private void release_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tag_manager.Current.setTagValue(VarName, false);
            }
            catch (Exception)
            {

            }
        }
    }
}
