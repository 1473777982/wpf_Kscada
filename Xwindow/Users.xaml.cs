using common;
using Panuon.UI.Silver;
using R2R.helper;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace R2R.Xwindow
{
    /// <summary>
    /// Users.xaml 的交互逻辑
    /// </summary>
    public partial class Users : WindowX
    {
        public Users()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            users_verify();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                users_verify();
            }
        }
        private void users_verify()
        {
            var table = sqlClientHelper.GetTable(CommandType.Text, sqlClientHelper.sqlString_select_Users, null)[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (User.Text == (string)table.Rows[i]["UserName"] &&
                    Password.Text == (string)table.Rows[i]["Password"])
                {
                    UserData.permission = (int)table.Rows[i]["UserLevel"];
                    App.Log.Info("用户登录成功,用户: " + User.Text);
                    Mwin.user_alive.Username = User.Text;
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("用户名或密码错误");
        }
        private void User_GotFocus(object sender, RoutedEventArgs e)
        {
            User.Text = "";
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            Password.Text = "";
        }
    }
}
