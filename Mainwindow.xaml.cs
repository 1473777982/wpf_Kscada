using common;
using GlobalHotKeyDemo;
using Panuon.UI.Silver;
using R2R.Alarm;
using R2R.helper;
using R2R.historyvalues;
using R2R.OperationRecord;
using R2R.Pages;
using R2R.protocols;
using R2R.Windows;
using R2R.Xwindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace R2R
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Mwin : Window, IComponentConnector
    {
        public static SolidColorBrush brush_run;
        public static SolidColorBrush brush_alarm;
        public static SolidColorBrush brush_warn;
        public static SolidColorBrush brush_general;
        public static SolidColorBrush brush_white;
        public static SolidColorBrush brush_tagRead;
        public static System.Threading.Timer timer_historyValues = new System.Threading.Timer(historyValues.historyValues_to_influx, 1, 10000, Timeout.Infinite);
        public static System.Threading.Timer timer_alarm = new System.Threading.Timer(Alarm_active.alarm_now, 1, 10000, Timeout.Infinite);
        public static System.Threading.Timer timer_operation_logs = new System.Threading.Timer(save_operation_logs.operationLog, 1, 10000, Timeout.Infinite);

        public static MW_R2RDataSet mW_R2RDataSet = new MW_R2RDataSet();  //sql模板

        public static currentUser user_alive = new currentUser();//界面刷新对象
        Page16_chamber_sputter page_CAbox = new Page16_chamber_sputter();
        

        DispatcherTimer Clock_mianwindow = new DispatcherTimer();
        DispatcherTimer timer_alarmText = new DispatcherTimer();

        Page07_peifang page07_Peifang = new Page07_peifang();
        public static bool mainwin_loaded;
        public Mwin()
        {
            InitializeComponent();


            MainFrame.Content = new Page0_main_copy(); //初始化加载界面  
            win2.DataContext = user_alive;
            Clock_mianwindow.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Clock_mianwindow.Tick += new EventHandler(clock_flash);
            Clock_mianwindow.Start();

            timer_alarmText.Interval = new TimeSpan(0, 0, 0, 1);
            timer_alarmText.Tick += new EventHandler(alarmText);
            timer_alarmText.Start();
            Operations.addLog("mainwindow初始化完成");
            //addoperationTOsql("AppStart");
            Operations.addLog("AppStart");
            mainwin_loaded = true;
        }

        private void Page07_Peifang_ChangeBgr(string colorstr)
        {
            button_mian.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorstr));
        }

        // 刷新数据
        public void clock_flash(object sender, EventArgs e)
        {
            FrontTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            tb_Alarm.Text = "";
        }
        //报警信息显示
        public void alarmText(object sender, EventArgs e)
        {
            try
            {
                var lon = Alarm_active.Table_alarm.Rows.Count;
                tb_larm_cont.Text = "当前报警(" + lon.ToString() + ")";

                if (lon > 0)
                {
                    var random = new Random();
                    int id = random.Next(0, lon);
                    tb_Alarm.Text = Alarm_active.Table_alarm.Rows[id]["description"].ToString();
                }
                else
                {
                    tb_Alarm.Text = "";
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// ADSl通讯状态改变event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void AdsClient_ConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        //{
        //    if (e.OldState.Equals(TwinCAT.ConnectionState.Connected))
        //    {
        //        adsClient.Connect(AMSNETID, PORT); //此处填写AMSNETID和PORT口
        //    }
        //}

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
        //全屏 + 快捷键事件
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            brush_run = (SolidColorBrush)FindResource("Color_Running");
            brush_alarm = (SolidColorBrush)FindResource("Color_Alarm");
            brush_warn = (SolidColorBrush)FindResource("Color_Warn");
            brush_general = (SolidColorBrush)FindResource("Color_General");
            brush_white = (SolidColorBrush)FindResource("Color_tagCtrl_write");
            brush_tagRead = (SolidColorBrush)FindResource("Color_tagCtrl_read");

            //this.WindowState = System.Windows.WindowState.Normal;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            //this.ResizeMode = System.Windows.ResizeMode.CanResize;
            //WindowXCaption.SetHeight(this, 0);

            ////this.Topmost = true;
            //this.Left = 0.0;
            //Top = 0.0;
            //Width = SystemParameters.PrimaryScreenWidth;
            //Height = SystemParameters.PrimaryScreenHeight;

            HotKeySettingsManager.Instance.RegisterGlobalHotKeyEvent += Instance_RegisterGlobalHotKeyEvent;

            //从json文件读取实时值
            var_Latest.pickup();

            //监控系统是否空闲，否则退出登录
            ActivityMonitor monitor = new ActivityMonitor();
            monitor.IdleTimeOut = App.usertimeout;
            //2 seconds
            //monitor.OnIdle += delegate { Console.WriteLine("On idle"); };
            monitor.OnIdle += userLogout;
            monitor.Start();

           
        }
        private void win2_Closed(object sender, EventArgs e)
        {
            forceOut();
        }

        //扩展界面操作按钮
        public void BtnWindow_Click(object sender, RoutedEventArgs e)
        {

            Window window = null;
            switch ((sender as Button).Name)
            {
                case "systemInfo"://系统参数
                    MainFrame.Content = new Page09_SystemParameter();
                    break;
                case "alarm"://报警
                    MainFrame.Content = new Alarm_active();
                    break;
                case "network"://网络拓扑
                    MainFrame.Content = new Page11_NET();
                    break;
                case "operationHis"://操作记录
                    ShowWindow(typeof(operationHis));
                    break;
                case "maintenance": // 保养
                    ShowWindow(typeof(maintenance));                    
                    break;          
                case "tags": //变量设置
                    ShowWindow(typeof(R2R.protocols.protocols));
                    break;
                case "peifang"://配方
                    MainFrame.Content = new Page07_peifang();
                    break;
                case "chart": //曲线
                    ShowWindow(typeof(Chart.Chart));
                    break;
                case "IO": //IO
                    ShowWindow(typeof(IO));
                    break;
                case "limit": //阈值
                    ShowWindow(typeof(limitSetting));
                    break;

            }
            //if (window != null)
            //{

            //    ShowWindow(window.GetType());

            //   //window.ShowDialog();

            //}
            menual.IsOpen = false;

        }

        //主界面操作按钮
        
        public void Button_Click_Front(object sender, RoutedEventArgs e)
        {

            Window window = null;
            var name = (sender as RadioButton).Content.ToString();
            page_CAbox.BoxName = name;
            if (name == "CA2")
            {
                page_CAbox.serve_SLP.Header = "加热盘";
            }
            else
            {
                page_CAbox.serve_SLP.Header = "水冷盘";
            }
            if (name == "CA4")
            {
                page_CAbox.MFC_Ar_text.Text = "500sccm";
            }
            else
            {
                page_CAbox.MFC_Ar_text.Text = "200sccm";
            }
            switch (name)
            {
                case "主界面":
                    MainFrame.Content = new Page0_main_copy();
                    break;
                case "CA1":
                    page_CAbox.serve_HB1.Visibility = Visibility.Visible;
                    page_CAbox.serve_HB2.Visibility = Visibility.Visible;
                    page_CAbox.serve_XZ.Visibility = Visibility.Collapsed;
                    page_CAbox.ChamberLid.Visibility = Visibility.Visible;
                    page_CAbox.heater.Visibility = Visibility.Collapsed;
                    page_CAbox.ADL.Visibility = Visibility.Visible;
                    page_CAbox.RF.Visibility = Visibility.Collapsed;
                    page_CAbox.DC1.Visibility = Visibility.Collapsed;
                    page_CAbox.DC2.Visibility = Visibility.Collapsed;
                    page_CAbox.tempe.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_HB.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG1.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_O2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_N2.Visibility = Visibility.Collapsed;                 
                    page_CAbox.MFC_O2.Visibility= Visibility.Collapsed;
                    page_CAbox.MFC_N2.Visibility= Visibility.Collapsed;
                    MainFrame.Content = page_CAbox;
                    break;
                case "CA2":
                    page_CAbox.serve_HB1.Visibility = Visibility.Visible;
                    page_CAbox.serve_HB2.Visibility = Visibility.Visible;
                    page_CAbox.serve_XZ.Visibility = Visibility.Collapsed;
                    page_CAbox.ChamberLid.Visibility = Visibility.Visible;
                    page_CAbox.heater.Visibility = Visibility.Visible;
                    page_CAbox.ADL.Visibility = Visibility.Visible;
                    page_CAbox.RF.Visibility = Visibility.Visible;
                    page_CAbox.DC1.Visibility = Visibility.Collapsed;
                    page_CAbox.DC2.Visibility = Visibility.Collapsed;
                    page_CAbox.tempe.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_HB.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG1.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_O2.Visibility = Visibility.Visible;
                    page_CAbox.MFC_N2.Visibility = Visibility.Collapsed;                
                    page_CAbox.MFC_O2.Visibility = Visibility.Visible;
                    page_CAbox.MFC_N2.Visibility = Visibility.Collapsed;
                    MainFrame.Content = page_CAbox;
                    break;
                case "CA3":
                    page_CAbox.serve_HB1.Visibility = Visibility.Visible;
                    page_CAbox.serve_HB2.Visibility = Visibility.Visible;
                    page_CAbox.serve_XZ.Visibility = Visibility.Collapsed;
                    page_CAbox.ChamberLid.Visibility = Visibility.Visible;
                    page_CAbox.heater.Visibility = Visibility.Collapsed;
                    page_CAbox.ADL.Visibility = Visibility.Visible;
                    page_CAbox.RF.Visibility = Visibility.Visible;
                    page_CAbox.DC1.Visibility = Visibility.Collapsed;
                    page_CAbox.DC2.Visibility = Visibility.Collapsed;
                    page_CAbox.tempe.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_HB.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG1.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_O2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_N2.Visibility = Visibility.Visible;                  
                    page_CAbox.MFC_O2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_N2.Visibility = Visibility.Visible;
                    MainFrame.Content = page_CAbox;
                    break;
                case "CA4"://蒸镀
                    page_CAbox.serve_HB1.Visibility = Visibility.Collapsed;
                    page_CAbox.serve_HB2.Visibility = Visibility.Collapsed;
                    page_CAbox.serve_XZ.Visibility = Visibility.Visible;
                    page_CAbox.ChamberLid.Visibility = Visibility.Collapsed;
                    page_CAbox.heater.Visibility = Visibility.Collapsed;
                    page_CAbox.ADL.Visibility = Visibility.Collapsed;
                    page_CAbox.RF.Visibility = Visibility.Collapsed;
                    page_CAbox.DC1.Visibility = Visibility.Visible;
                    page_CAbox.DC2.Visibility = Visibility.Visible;
                    page_CAbox.tempe.Visibility = Visibility.Visible;
                    page_CAbox.cylinder_HB.Visibility = Visibility.Visible;
                    page_CAbox.cylinder_GG1.Visibility = Visibility.Visible;
                    page_CAbox.cylinder_GG2.Visibility = Visibility.Visible;
                    page_CAbox.MFC_O2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_N2.Visibility = Visibility.Collapsed;              
                    page_CAbox.MFC_O2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_N2.Visibility = Visibility.Collapsed;
                    MainFrame.Content = page_CAbox;
                    break;
                case "CA5"://备用
                    page_CAbox.serve_HB1.Visibility = Visibility.Visible;
                    page_CAbox.serve_HB2.Visibility = Visibility.Visible;
                    page_CAbox.serve_XZ.Visibility = Visibility.Collapsed;
                    page_CAbox.ChamberLid.Visibility = Visibility.Visible;
                    page_CAbox.heater.Visibility = Visibility.Collapsed;
                    page_CAbox.ADL.Visibility = Visibility.Visible;
                    page_CAbox.RF.Visibility = Visibility.Visible;
                    page_CAbox.DC1.Visibility = Visibility.Collapsed;
                    page_CAbox.DC2.Visibility = Visibility.Collapsed;
                    page_CAbox.tempe.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_HB.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG1.Visibility = Visibility.Collapsed;
                    page_CAbox.cylinder_GG2.Visibility = Visibility.Collapsed;
                    page_CAbox.MFC_O2.Visibility = Visibility.Visible;
                    page_CAbox.MFC_N2.Visibility = Visibility.Visible;                   
                    page_CAbox.MFC_O2.Visibility = Visibility.Visible;
                    page_CAbox.MFC_N2.Visibility = Visibility.Visible;
                    MainFrame.Content = page_CAbox;
                    break;
                case "ROBOT":
                    MainFrame.Content = new Page19_robot();
                    break;
                case "外围":
                    MainFrame.Content = new Page20_around();
                    break;
                case "用户登录":
                    window = new Xwindow.Users();
                    break;
            }


            if (window != null)
            {
                window.ShowDialog();
            }

        }
        //其他操作按钮
        public void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window window = null;
            switch ((sender as MenuItem).Header.ToString())
            {
                case "用户登录":
                    window = new Xwindow.Users();
                    break;
                case "用户退出":
                    userLogout(this, EventArgs.Empty);
                    break;
                case "用户管理":
                    window = new User_manager();
                    break;
                case "退出程序":
                    //保存实时值至json
                    var_Latest.backup();
                    forceOut();
                    Close();
                    break;
            }
            if (window != null)
            {

                window.ShowDialog();

            }
        }
        private void MenuItem_Click_monitor(object sender, RoutedEventArgs e)
        {
            new tag_monitor().Show();
        }
        private void userLogout(object sender, EventArgs e)
        {
            if (user_alive.Username != "")
            {
                UserData.permission = 0;
                user_alive.Username = "";
                AutoCloseMsgBox("用户退出", 5000);
                Task.Run(() => { MessageBox.Show("超时未操作，用户已退出", "用户退出"); });
            }
        }
        private async void AutoCloseMsgBox(string titleTxt, int delay_ms)
        {
            await Task.Delay(delay_ms);
            await Task.Run(() => WndHelper.FindAndKillWindow(titleTxt));
        }

        //其他菜单
        private void Button_Click_More(object sender, RoutedEventArgs e)
        {

            menual.PlacementTarget = More;
            menual.IsOpen = true;

        }

        #region *************************************快捷键****************************************
        /// <summary>
        /// 当前窗口句柄
        /// </summary>
        private IntPtr m_Hwnd = new IntPtr();

        /// <summary>
        /// 记录快捷键注册项的唯一标识符
        /// </summary>
        private Dictionary<EHotKeySetting, int> m_HotKeySettings = new Dictionary<EHotKeySetting, int>();

        /// <summary>
        /// WPF窗体的资源初始化完成，并且可以通过WindowInteropHelper获得该窗体的句柄用来与Win32交互后调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            // 获取窗体句柄
            m_Hwnd = new WindowInteropHelper(this).Handle;
            HwndSource hWndSource = HwndSource.FromHwnd(m_Hwnd);
            // 添加处理程序
            if (hWndSource != null) hWndSource.AddHook(WndProc);
        }

        /// <summary>
        /// 所有控件初始化完成后调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            // 注册热键
            InitHotKey();
        }

        /// <summary>
        /// 通知注册系统快捷键事件处理函数
        /// </summary>
        /// <param name="hotKeyModelList"></param>
        /// <returns></returns>
        private bool Instance_RegisterGlobalHotKeyEvent(ObservableCollection<HotKeyModel> hotKeyModelList)
        {
            return InitHotKey(hotKeyModelList);
        }

        /// <summary>
        /// 打开快捷键设置窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void MenuItem_Click_hotkey(object sender, RoutedEventArgs e)
        {
            var win = HotKeySettingsWindow.CreateInstance();
            if (!win.IsVisible)
            {
                win.ShowDialog();
            }
            else
            {
                win.Activate();
            }
        }
        /// <summary>
        /// 初始化注册快捷键
        /// </summary>
        /// <param name="hotKeyModelList">待注册热键的项</param>
        /// <returns>true:保存快捷键的值；false:弹出设置窗体</returns>
        private bool InitHotKey(ObservableCollection<HotKeyModel> hotKeyModelList = null)
        {
            var list = hotKeyModelList ?? HotKeySettingsManager.Instance.LoadDefaultHotKey();
            // 注册全局快捷键
            string failList = HotKeyHelper.RegisterGlobalHotKey(list, m_Hwnd, out m_HotKeySettings);
            if (string.IsNullOrEmpty(failList))
                return true;
            MessageBoxResult mbResult = MessageBox.Show(string.Format("无法注册下列快捷键\n\r{0}是否要改变这些快捷键？", failList), "提示", MessageBoxButton.YesNo);
            // 弹出热键设置窗体
            var win = HotKeySettingsWindow.CreateInstance();
            if (mbResult == MessageBoxResult.Yes)
            {
                if (!win.IsVisible)
                {
                    win.ShowDialog();
                }
                else
                {
                    win.Activate();
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 窗体回调函数，接收所有窗体消息的事件处理函数
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="msg">消息</param>
        /// <param name="wideParam">附加参数1</param>
        /// <param name="longParam">附加参数2</param>
        /// <param name="handled">是否处理</param>
        /// <returns>返回句柄</returns>
        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wideParam, IntPtr longParam, ref bool handled)
        {
            switch (msg)
            {
                case HotKeyManager.WM_HOTKEY:
                    int sid = wideParam.ToInt32();
                    if (sid == m_HotKeySettings[EHotKeySetting.全屏])
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                        this.WindowStyle = System.Windows.WindowStyle.None;
                        this.ResizeMode = System.Windows.ResizeMode.NoResize;
                        WindowXCaption.SetHeight(this, 0);
                        //this.Topmost = true;
                        this.Left = 0.0;
                        Top = 0.0;
                        Width = SystemParameters.PrimaryScreenWidth;
                        Height = SystemParameters.PrimaryScreenHeight;
                    }
                    else if (sid == m_HotKeySettings[EHotKeySetting.最小化])
                    {
                        this.WindowState = System.Windows.WindowState.Minimized;
                        this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                    }
                    else if (sid == m_HotKeySettings[EHotKeySetting.最大化])
                    {
                        this.WindowState = System.Windows.WindowState.Maximized;
                        this.ResizeMode = System.Windows.ResizeMode.CanResize;
                        this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                        WindowXCaption.SetHeight(this, 30);

                    }
                    else if (sid == m_HotKeySettings[EHotKeySetting.截图])
                    {

                        RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)win2.ActualWidth, (int)win2.ActualHeight, 96d, 96d, PixelFormats.Default);
                        targetBitmap.Render(win2);
                        PngBitmapEncoder saveEncoder = new PngBitmapEncoder();
                        saveEncoder.Frames.Add(BitmapFrame.Create(targetBitmap));
                        // 指定存储位置
                        var dialog = new Microsoft.Win32.SaveFileDialog();
                        // 指定后缀
                        dialog.Filter = "PNG图片|*.png";
                        if (dialog.ShowDialog().GetValueOrDefault())
                        {
                            string filePath = dialog.FileName;
                            try
                            {
                                // 保存文件
                                System.IO.FileStream fs = System.IO.File.Open(filePath, System.IO.FileMode.OpenOrCreate);
                                saveEncoder.Save(fs);
                                fs.Close();
                                MessageBox.Show("导出成功");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("请检查存储位置", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else if (sid == m_HotKeySettings[EHotKeySetting.退出程序])
                    {
                        forceOut();
                        Close();

                    }
                    else if (sid == m_HotKeySettings[EHotKeySetting.变量监控])
                    {
                        //protocols.tag_monitor tag_Nonitor = new protocols.tag_monitor();
                        //tag_Nonitor.Show();
                        var dia = new protocols.tag_monitor();
                        dia.Topmost = true;
                        dia.Show();

                    }
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }



        #endregion
        private void forceOut()
        {
            foreach (var item in communication.communicat.connectionGroups)
            {
                item.CheckStoped();
            }
            Process.GetCurrentProcess().Kill();
            Application.Current.Shutdown();
        }


        #region  ShowWindow 
        public static void ShowWindow(Type windowType)
        {
            // 遍历当前已打开的窗口集合
            foreach (Window window in Application.Current.Windows)
            {
                // 判断是否已经打开了指定的窗口
                if (window.GetType() == windowType)
                {
                    // 如果已经打开，则将窗口显示在最上层
                    window.BringToFront();
                    return;
                }
            }

            // 如果未打开，则创建新窗口实例并显示
            Window newWindow = (Window)Activator.CreateInstance(windowType);
            newWindow.Show();
        }

       


        #endregion

    }
    public static class WindowExtensions
    {
        // 扩展方法用于将窗口显示在最上层
        public static void BringToFront(this Window window)
        {
            if (window.WindowState == WindowState.Minimized)
            {
                window.WindowState = WindowState.Normal;
            }

            window.Activate();
            window.Topmost = true;
            window.Topmost = false;
            window.Focus();
        }
    }

    public class currentUser : helper.NotifyChanged
    {
        public string _username;
        public string Username
        { get => _username; set => SetProperty(ref _username, value); }
    }
    //binding接口
   
}
