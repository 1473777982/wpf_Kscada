using common;
using common.tag;
using InfluxDB.Client;
using log4net;
using Newtonsoft.Json;
using Panuon.UI.Silver;
using R2R.helper;
using R2R.Xwindow;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace R2R
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private Mutex _mutex;
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        static Xwindow.SplashWindow splashWindow = null;
        static Login login = null;
        public static Dictionary<string, List<int>> servemode;
        Dictionary<string, Dictionary<string, string>> config;
        #region  系统参数
        #region sql mysql
        public static string server, dbName, uid, pwd;
        #endregion

        #region influxDB
        public static InfluxDBClient influxDBClient;
        public static string org, bucket, measurement, username, password, Token;
        #endregion

        // usertimeout
        public static int usertimeout = 60000;

        #endregion

        public void initConfig()
        {
            try
            {
                server = config["mysql"]["server"];
                dbName = config["mysql"]["dbName"];
                uid = config["mysql"]["uid"];
                pwd = config["mysql"]["pwd"];
            }
            catch (Exception)
            {
                Operations.addLog("mysql读取配置参数失败");
            }

            try
            {
                org = config["influx"]["org"];
                bucket = config["influx"]["bucket"];
                measurement = config["influx"]["measurement"];
                username = config["influx"]["username"];
                password = config["influx"]["password"];
                Token = config["influx"]["Token"];
            }
            catch (Exception)
            {
                Operations.addLog("mysql读取配置参数失败");
            }
            try
            {
                usertimeout = Convert.ToInt32(config["usertimeout"]["time"]);
            }
            catch (Exception)
            {
                Operations.addLog("usertimeout读取配置参数失败");
            }
        }
        public static string connectionString_mySQL = string.Format("server={0};port=3306;database={1};uid={2};pwd={3};Charset=utf8;", server, dbName, uid, pwd);

        

        protected override void OnStartup(StartupEventArgs e)
        {
            const string mutexName = "App_GTLD"; // 用于确保唯一性的Mutex名称

            _mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                // 如果Mutex已经存在，表示程序已经在运行中，可以在此处处理重复启动的逻辑
                MessageBox.Show("程序已经在运行中！");
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
            base.OnStartup(e);
            Thread thread = new Thread(() =>
            {
                splashWindow = new Xwindow.SplashWindow();
                splashWindow.ShowDialog();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            //UI样式设定
            MessageBoxX.MessageBoxXConfigurations.Add("WarningTheme", new Panuon.UI.Silver.Core.MessageBoxXConfigurations()
            {
                MessageBoxIcon = Panuon.UI.Silver.MessageBoxIcon.Warning,
            });

            //初始化数据库参数
            config = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonFile.GetJsonFile(@"config.json"));
            initConfig();

            //连接PLC  初始化变量
            communicationTag communicationTag = new communicationTag();
            communicationTag.Load();
            communicationTag.Current.LoadFromFile();
            communicationTag.Current.CreateRuntimeTag();
            communication.communicat.Connect();

            //连接  influxdb
            influxDBClient = new InfluxDBClient("http://localhost:8086", App.Token);

            //加载登录界面
            login = new Login();
            login.Show();
            login.Visibility = Visibility.Hidden;
            login.ContentRendered += (s, ev) =>
            {
                CloseSplash();
                login.Visibility = Visibility.Visible;
                IntPtr handle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                SetForegroundWindow(handle);
            };
            servemode = new Dictionary<string, List<int>>
            {
                {"CA1",new List<int>(){-1,-1,-1,-1,-1}},
                {"CA2",new List<int>(){-1,-1,-1,-1,-1}},
                {"CA3",new List<int>(){-1,-1,-1,-1,-1}},
                {"CA4",new List<int>(){-1,-1,-1,-1,-1}},
                {"CA5",new List<int>(){-1,-1,-1,-1,-1}},

            };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _mutex?.Close();
            base.OnExit(e);
        }
        public static void CloseSplash()
        {
            splashWindow.Dispatcher.Invoke(() =>
            {
                splashWindow.Close();
            });
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hnd);
    }
}
