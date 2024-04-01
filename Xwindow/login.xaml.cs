using common;
using R2R.helper;
using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace R2R.Xwindow
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        Mwin window = null;
        IntPtr handle;
        public Login()
        {
            InitializeComponent();


            window = new Mwin();
            window.Show();
            window.Visibility = Visibility.Hidden;
            handle = new WindowInteropHelper(Application.Current.MainWindow).Handle;


            //this.WindowState = System.Windows.WindowState.Normal;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            //this.ResizeMode = System.Windows.ResizeMode.NoResize;
            ////this.Topmost = true;
            //this.Left = 0.0;
            //Top = 0.0;
            //Width = SystemParameters.PrimaryScreenWidth;
            //Height = SystemParameters.PrimaryScreenHeight;
        }
        private void yes_Click(object sender, RoutedEventArgs e)
        {
            //暂时取消登录操作
            UserData.permission = 200;
            Mwin.user_alive.Username = "admin";
            App.Log.Info("用户登录成功,用户: " + User.Text);
            Mwin.user_alive.Username = User.Text;
            window.Visibility = Visibility.Visible;
            Close();
            SetForegroundWindow(handle);
            return;

            login_verify();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            Application.Current.Shutdown();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login_verify();
            }
        }
        private void login_verify()
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
                    window.Visibility = Visibility.Visible;
                    Close();
                    SetForegroundWindow(handle);

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

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hnd);
    }

}
