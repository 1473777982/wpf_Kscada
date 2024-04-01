using System;
using System.Linq;
using System.Windows;

namespace R2R.Chart
{
    /// <summary>
    /// group_Edit.xaml 的交互逻辑
    /// </summary>
    public partial class group_Edit : Window
    {
        groupItem gp;
        public group_Edit(groupItem group)
        {
            InitializeComponent();
            if (group != null) { this.gp = group; }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (gp != null)
            {
                tb_gpName.Text = gp.groupName;
                tb_vars.Text = string.Join(",", gp.lineNames.ToArray());
                cbox.IsChecked = gp.vertical_log;
            }
        }

        private void click_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void click_OK(object sender, RoutedEventArgs e)
        {
            try
            {
                gp.lineNames.Clear();
                gp.groupName = tb_gpName.Text;
                var list = tb_vars.Text.Split(',').ToList();
                foreach ( var item in list ) 
                {
                    gp.lineNames.Add(item);
                }
               gp.vertical_log = (bool)cbox.IsChecked;
            }
            catch (Exception)
            {

            }
            Close();
        }


    }
}
