using R2R.helper;
using System;
using System.Data;
using System.Windows;

namespace R2R.Xwindow
{
    /// <summary>
    /// User_manager.xaml 的交互逻辑
    /// </summary>
    public partial class User_manager : Window
    {
        public User_manager()
        {
            InitializeComponent();
        }

        private void bt_conform_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var table = (dg.ItemsSource as DataView).ToTable();
                sqlClientHelper.ExecteNonQuery(CommandType.Text, sqlClientHelper.sqlString_clear_Users, null);
                sqlClientHelper.uptate_From_table(table, "Users");
                MessageBox.Show("保存完成");
                Operations.addLog("保存用户组完成");
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败");
                Operations.addLog("保存用户组失败");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var table = sqlClientHelper.GetTable(CommandType.Text, sqlClientHelper.sqlString_select_Users, null)[0];
            dg.ItemsSource = table.DefaultView;
        }
    }
}
