using System.Windows.Controls;

namespace R2R
{
    /// <summary>
    /// Page05_dianyuan.xaml 的交互逻辑
    /// </summary>
    public partial class Page05_dianyuan : Page
    {
        //#region
        //DispatcherTimer dispatcherTimer_P05 = new DispatcherTimer();
        //string PS_Name = "C100";

        //int comboindex;
        public Page05_dianyuan()
        {

            InitializeComponent();
            //dispatcherTimer_P05.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //dispatcherTimer_P05.Tick += new EventHandler(TimeAction);
            //dispatcherTimer_P05.Start();
            //Mytb_01.VAaddress = "PS_" + PS_Name + "_point_set"; //后期据PLC程序改为变量address
            //Mytb_01.VAaddress = "PS_" + PS_Name + "_point_set";
            //Mytb_02.VAaddress = "PS_" + PS_Name + "_P_set";
            //Mytb_03.VAaddress = "PS_" + PS_Name + "_U_set";
            //Mytb_04.VAaddress = "PS_" + PS_Name + "_I_set";
            //Mytb_09.VAaddress = "PS_" + PS_Name + "_Psum_set";
            //Mytb_10.VAaddress = "PS_" + PS_Name + "_Uoffset_set";
            //Mytb_11.VAaddress = "PS_" + PS_Name + "_freq_set";
            //Mytb_12.VAaddress = "PS_" + PS_Name + "_pulsetime_set";
            //Mytb_14.VAaddress = "PS_" + PS_Name + "_Uacctime_set";
            //Mytb_15.VAaddress = "PS_" + PS_Name + "_Udectime_set";
            //Mytb_16.VAaddress = "PS_" + PS_Name + "_Iacctime_set";
            //Mytb_17.VAaddress = "PS_" + PS_Name + "_Idectime_set";
            //Mytb_18.VAaddress = "PS_" + PS_Name + "_Pacctime_set";
            //Mytb_19.VAaddress = "PS_" + PS_Name + "_Pdectime_set";

            //dockPanel_RealChart.Visibility = Visibility.Visible;
            //dockPanel_HisChart.Visibility = Visibility.Hidden;

        }
        //#region 界面控制
        //private void TimeAction(object sender, EventArgs e)
        //{
        //    try
        //    {




        //    }
        //    catch (Exception)
        //    {

        //    }

        //    //界面控件显示和隐藏
        //    if (PS_Name == "C101_A" || PS_Name == "C101_B" || PS_Name == "C201_A" || PS_Name == "C201_B")
        //    {
        //        t11.Visibility = Visibility.Visible;
        //        t12.Visibility = Visibility.Visible;
        //        t13.Visibility = Visibility.Visible;

        //        tb_11.Visibility = Visibility.Visible;
        //        tb_12.Visibility = Visibility.Visible;
        //        tb_13.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        t11.Visibility = Visibility.Collapsed;
        //        t12.Visibility = Visibility.Collapsed;
        //        t13.Visibility = Visibility.Collapsed;

        //        tb_11.Visibility = Visibility.Collapsed;
        //        tb_12.Visibility = Visibility.Collapsed;
        //        tb_13.Visibility = Visibility.Collapsed;
        //    }
        //    if (R2R_tag.UserData.permission > 50)
        //    {
        //        t14.Visibility = Visibility.Visible;
        //        t15.Visibility = Visibility.Visible;
        //        t16.Visibility = Visibility.Visible;
        //        t17.Visibility = Visibility.Visible;
        //        t18.Visibility = Visibility.Visible;
        //        t19.Visibility = Visibility.Visible;


        //    }
        //    else
        //    {
        //        t14.Visibility = Visibility.Collapsed;
        //        t15.Visibility = Visibility.Collapsed;
        //        t16.Visibility = Visibility.Collapsed;
        //        t17.Visibility = Visibility.Collapsed;
        //        t18.Visibility = Visibility.Collapsed;
        //        t19.Visibility = Visibility.Collapsed;

        //    }

        //}
        //private void EllipseFill(Ellipse ellipse, String str)
        //{
        //    if (str == "true")
        //    {
        //        ellipse.Fill = Brushes.Green;
        //    }
        //    else
        //        ellipse.Fill = Brushes.LightGray;
        //}
        //private void RadioButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    switch (((RadioButton)sender).Content)
        //    {
        //        case "C100":
        //            PS_Name = "C100";
        //            setVARaddress();
        //            break;
        //        case "C101_A":
        //            PS_Name = "C101_A";
        //            setVARaddress();
        //            break;
        //        case "C101_B":
        //            PS_Name = "C101_B";
        //            setVARaddress();
        //            break;
        //        case "C102_A":
        //            PS_Name = "C102_A";
        //            setVARaddress();
        //            break;
        //        case "C102_B":
        //            PS_Name = "C102_B";
        //            setVARaddress();
        //            break;
        //        case "C103_A":
        //            PS_Name = "C103_A";
        //            setVARaddress();
        //            break;
        //        case "C103_B":
        //            PS_Name = "C103_B";
        //            setVARaddress();
        //            break;
        //        case "C200":
        //            PS_Name = "C200";
        //            setVARaddress();
        //            break;
        //        case "C201_A":
        //            PS_Name = "C201_A";
        //            setVARaddress();
        //            break;
        //        case "C201_B":
        //            PS_Name = "C201_B";
        //            setVARaddress();
        //            break;
        //        case "C202_A":
        //            PS_Name = "C202_A";
        //            setVARaddress();
        //            break;
        //        case "C202_B":
        //            PS_Name = "C202_B";
        //            setVARaddress();
        //            break;
        //        case "C203_A":
        //            PS_Name = "C203_A";
        //            setVARaddress();
        //            break;
        //        case "C203_B":
        //            PS_Name = "C203_B";
        //            setVARaddress();
        //            break;
        //        default:
        //            PS_Name = "C100";
        //            setVARaddress();
        //            break;
        //    }
        //}

        //private void setVARaddress()  //后期据PLC程序改为变量address
        //{


        //}
        ////打开电压曲线界面
        //private void RadioButton_Checked_chart(object sender, RoutedEventArgs e)
        //{

        //}
        ////打开电表界面
        //private void RadioButton_Checked_energy(object sender, RoutedEventArgs e)
        //{

        //}
        ////点击空白，清除焦点
        //private void Page05dianyuan_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
        //}

        ////模式__获得选取的index
        //private void combobox_dianyuan_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    comboindex = (sender as ComboBox).SelectedIndex;
        //}
        ////模式选择
        //private void combobox_dianyuan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    if (!comboBox.IsLoaded)
        //        return;
        //    try
        //    {
        //        switch (combobox_dianyuan.SelectedIndex)
        //        {
        //            case 0:
        //                App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_mode_P"), 1);

        //                break;
        //            case 1:
        //                App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_mode_U"), 1);

        //                break;
        //            case 2:
        //                App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_mode_I"), 1);

        //                break;
        //            default:
        //                App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_mode_P"), 1);

        //                break;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //MessageBoxX.Show("模式更改失败", "");
        //        (sender as ComboBox).SelectedIndex = comboindex;
        //    }

        //}

        ////启动按钮
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (false) //抽真空操作进行中 VacuoStart_Cmd
        //            {
        //                MessageBoxResult result = MessageBoxX.Show("您确定要打开电源 " + PS_Name + " 吗？", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //                if (result == MessageBoxResult.OK)
        //                {
        //                    App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_start_set"), 1);//PS_C100_start_set
        //                    (sender as Button).Content = "启动中...";
        //                    Thread.Sleep(1500);
        //                    if ((string)App.DIC_vars["PS_" + PS_Name + "_state_running"][5] == "true")//PS_C100_state_running
        //                    {
        //                        BrushConverter brushConverter = new BrushConverter();
        //                        (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FF5DC76D");
        //                        (sender as Button).Content = "Running";
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行电源打开关闭操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("启动电源 " + PS_Name + " 失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}
        //// 停止按钮
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (false) //抽真空操作进行中 VacuoStart_Cmd
        //            {
        //                MessageBoxResult result = MessageBoxX.Show("您确定要关闭电源 " + PS_Name + " 吗？", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //                if (result == MessageBoxResult.OK)
        //                {
        //                    App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_start_set"), 0);//PS_C100_start_set
        //                    Thread.Sleep(1500);
        //                    if ((string)App.DIC_vars["PS_" + PS_Name + "_state_stopped"][5] == "true")//PS_C100_state_running
        //                    {
        //                        BrushConverter brushConverter = new BrushConverter();
        //                        (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FFB73333");
        //                        (sender as Button).Content = "Stopped";
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行电源打开关闭操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("关闭电源 " + PS_Name + " 失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}

        ///// <summary>
        ///// //复位按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (false) //抽真空操作进行中 VacuoStart_Cmd
        //            {
        //                MessageBoxResult result = MessageBoxX.Show("您确定要复位电源 " + PS_Name + " 吗？", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //                if (result == MessageBoxResult.OK)
        //                {
        //                    App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS_Name + "_start_set"), 0);//PS_C100_start_set
        //                    MessageBoxX.Show("复位完成 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //                }
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行电源故障复位操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("复位电源 " + PS_Name + " 失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}

        ////切换实时曲线控件
        //private void button_realchart(object sender, RoutedEventArgs e)
        //{
        //    dockPanel_HisChart.Visibility = Visibility.Hidden;
        //    dockPanel_RealChart.Visibility = Visibility.Visible;

        //}
        ////切换历史曲线
        //private void Button_hischart(object sender, RoutedEventArgs e)
        //{
        //    dockPanel_RealChart.Visibility = Visibility.Hidden;
        //    dockPanel_HisChart.Visibility = Visibility.Visible;
        //}
        //#endregion

        //#region 历史曲线 **************************************************************************     
        //string bucket = "testBucket";
        //string org = "MWpvd";
        //public static char[] Token = "i-FO97gFEoJFhuYWQPBf5IYjDF3yQEE-QbUEG99v2d9sfCNkADZNT62fKRnDrs1LWjW8zCk5OO68mviYzQbI8A==".ToCharArray();
        //List<DataFollowChart> followChart_his = new List<DataFollowChart>();
        //List<Point_with_datetime> points_1 = new List<Point_with_datetime>();
        ////List<Point_type> points_2 = new List<Point_type>();
        ////List<Point_type> points_3 = new List<Point_type>();
        //public IList<DataGrid_Row_defin> DataGrid_rowList_hischart { get; set; }//list of datagrid rows
        //private void plotter_his_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataContext = this;
        //    // setting custom position of legend:
        //    // it will be placed not in the top right corner of plotter,
        //    // but in the top left one
        //    dateAxis_his.LabelProvider.LabelStringFormat = "G";
        //}
        ///// <summary>
        ///// 从influxDB获取数据
        ///// </summary>
        //public async void getdata(DateTime dateTime1, DateTime dateTime2)
        //{
        //    TimeZoneInfo localTime = TimeZoneInfo.Local; //本地时区时间
        //    //DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, localTime);//完成转换
        //    var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", Token);
        //    using (var writeApi = influxDBClient.GetWriteApi())
        //    {
        //        string timeRange1 = dateTime1.ToString("o");
        //        string timeRange2 = dateTime2.ToString("o");
        //        var flux = "from(bucket: \"testBucket\")"
        //                    + "|> range(start:" + timeRange1 + ", stop:" + timeRange2 + ")"
        //                    + "|> filter(fn: (r) => r._measurement == \"mymem\")  "
        //                    + "|> filter(fn: (r) => r._field == \"I\")"
        //                    + "|> filter(fn: (r) => r.mytag == \"tag1\")";

        //        var fluxTables = await influxDBClient.GetQueryApi().QueryAsync(flux, org);

        //        fluxTables.ForEach(fluxTable =>
        //        {
        //            var fluxRecords = fluxTable.Records;
        //            fluxRecords.ForEach(fluxRecord =>
        //            {
        //                //Console.WriteLine($"{fluxRecord.GetTimeInDateTime()}: {fluxRecord.GetValue()}");
        //                //point.X = fluxRecord.GetTime();
        //                DateTime date = TimeZoneInfo.ConvertTimeFromUtc((DateTime)fluxRecord.GetTimeInDateTime(), localTime);
        //                double rate = Convert.ToDouble(fluxRecord.GetValue());
        //                //currentDataFrame.AppendAsync(Dispatcher, point);
        //                points_1.Add(new Point_with_datetime { Date = date, Rate = rate });
        //            });
        //        });
        //        var line1 = plotter_his.AddLineGraph(CreateCurrencyDataSource(points_1), Colors.Red, 1, "flux");
        //        followChart_his.Add(new DataFollowChart(line1));
        //        plotter_his.Children.Add(followChart_his[0]);
        //    }
        //}
        ///// <summary>
        ///// 鼠标跟随
        ///// 需要为每个单独曲线添加鼠标跟随的显示值
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void plotter_MouseMove_hisChart(object sender, MouseEventArgs e)
        //{
        //    // Get the x and y coordinates of the mouse pointer.
        //    System.Windows.Point position = e.GetPosition(this);
        //    double pX = position.X;
        //    double pY = position.Y;
        //    Thickness myThickness = new Thickness();
        //    myThickness.Left = pX - dockPanel_HisChart.Margin.Left + 50;
        //    myThickness.Top = pY - dockPanel_HisChart.Margin.Top;
        //    myThickness.Right = 0;
        //    myThickness.Bottom = 0;
        //    try
        //    {
        //        DateTime time;
        //        string str = null;
        //        if (followChart_his[0] != null)
        //        {
        //            verticalLine_his.Value = followChart_his[0].MarkerPosition.X;//标线横坐标
        //            time = dateAxis_his.ConvertFromDouble(followChart_his[0].MarkerPosition.X);
        //            str = time.ToString("G") + "\n";
        //        }
        //        foreach (var item in followChart_his)
        //        {
        //            if (item != null)
        //            {
        //                str += item.MarkerPosition.Y.ToString("F2") + "\n";
        //            }
        //        }
        //        //verticalLine_his.Value = followChart1.MarkerPosition.X;//标线横坐标
        //        //Sets the Height/Width of the circle to the mouse coordinates.
        //        //ellipse.Height = ;
        //        //ellipse.Height = pY;
        //        //DateTime time = dateAxis.ConvertFromDouble(followChart1.MarkerPosition.X);
        //        //textblock跟随标线显示曲线值
        //        //tb_his.Text = time.ToString("G") + "\n" +
        //        //followChart1.MarkerPosition.Y.ToString("F2") + "\n" +
        //        //followChart2.MarkerPosition.Y.ToString("F2") + "\n" +
        //        //followChart3.MarkerPosition.Y.ToString("F2") + "\n" +
        //        //followChart4.MarkerPosition.Y.ToString("F2");
        //        tb_his.Text = str;
        //        tb_his.Margin = myThickness;
        //        tb_his.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ///// <summary>
        ///// 将List<Point_type>转换为EnumerableDataSource<Point_type>即曲线控件需要的数据类型 IPointDataSource
        ///// </summary>
        ///// <param name="rates"></param>
        ///// <returns></returns>
        //private EnumerableDataSource<Point_with_datetime> CreateCurrencyDataSource(List<Point_with_datetime> rates)
        //{
        //    EnumerableDataSource<Point_with_datetime> ds = new EnumerableDataSource<Point_with_datetime>(rates);
        //    ds.SetXMapping(ci => dateAxis_his.ConvertToDouble(ci.Date));
        //    ds.SetYMapping(ci => ci.Rate);
        //    return ds;
        //}

        ///// <summary>
        ///// 清除chart按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void removeAll_Click_hischart(object sender, RoutedEventArgs e)
        //{
        //    plotter_his.Children.RemoveAll<LineGraph>();
        //    plotter_his.Children.RemoveAll<DataFollowChart>();
        //}
        ///// <summary>
        ///// 保存截图
        ///// </summary>
        ///// <param name="ui">控件名称</param>
        ///// <param name="filename">图片文件名</param>
        //public void SaveFrameworkElementToImage_his(FrameworkElement ui)
        //{

        //    RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)ui.ActualWidth, (int)ui.ActualHeight, 96d, 96d, PixelFormats.Default);
        //    targetBitmap.Render(ui);
        //    PngBitmapEncoder saveEncoder = new PngBitmapEncoder();
        //    saveEncoder.Frames.Add(BitmapFrame.Create(targetBitmap));
        //    // 指定存储位置
        //    var dialog = new SaveFileDialog();
        //    // 指定后缀
        //    dialog.Filter = "PNG图片|*.png";
        //    if (dialog.ShowDialog().GetValueOrDefault())
        //    {
        //        string filePath = dialog.FileName;
        //        try
        //        {
        //            // 保存文件
        //            System.IO.FileStream fs = System.IO.File.Open(filePath, System.IO.FileMode.OpenOrCreate);
        //            saveEncoder.Save(fs);
        //            fs.Close();
        //            MessageBox.Show("导出成功");
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("请检查存储位置", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 保存截图
        ///// </summary>
        ///// <param name="ui">控件名称</param>
        ///// <param name="filename">图片文件名</param>
        //private void savePng_Click_hischart(object sender, RoutedEventArgs e)
        //{
        //    SaveFrameworkElementToImage_his(grid_chart_his);
        //}
        ///// <summary>
        ///// 时间选择按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void timepicker_click(object sender, RoutedEventArgs e)
        //{
        //    timepicker win_timepicker = new timepicker();
        //    win_timepicker.add_line += getdata;
        //    win_timepicker.ShowDialog();
        //}
        //#endregion*************************************************************************************************************************************************************************

        //#region  实时曲线  ********************************************************************************
        //// observable data sources. Observable data source contains
        //// inside ObservableCollection. Modification of collection instantly modify
        //// visual representation of graph. 
        //ObservableDataSource<Point> source1 = null;
        //public IList<DataGrid_Row_defin> DataList_realchart { get; set; }
        //private Thread simThread;
        //List<DataFollowChart> followChart_real = new List<DataFollowChart>();
        //private void plotter_real_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataContext = this;
        //    // setting custom position of legend:
        //    // it will be placed not in the top right corner of plotter,
        //    // but in the top left one
        //    dateAxis_real.LabelProvider.LabelStringFormat = "G";
        //}
        ////实时曲线清除
        //private void removeAll_Click_realchart(object sender, RoutedEventArgs e)
        //{
        //    plotter_real.Children.RemoveAll<LineGraph>();
        //    plotter_real.Children.RemoveAll<DataFollowChart>();
        //    followChart_real.Clear();
        //}
        ////实时曲线开始采集
        //private void startchart_Click(object sender, RoutedEventArgs e)
        //{
        //    followChart_real.Clear();
        //    plotter_real.Children.RemoveAll<LineGraph>();
        //    plotter_real.Children.RemoveAll<DataFollowChart>();
        //    // Create first source
        //    source1 = new ObservableDataSource<Point>();
        //    // Set identity mapping of point in collection to point on plot
        //    source1.SetXYMapping(p => p);

        //    //color
        //    Color[] colors_realchart = ColorHelper.CreateRandomColors(3);
        //    // Add all three graphs. Colors are not specified and chosen random
        //    var line1 = plotter_real.AddLineGraph(source1, colors_realchart[0], 2, "Data row 1");
        //    followChart_real.Add(new DataFollowChart(line1));
        //    plotter_real.Children.Add(followChart_real[0]);

        //    if (simThread == null)
        //    {
        //        simThread = new Thread(new ThreadStart(Simulation));
        //        // Start computation process in second thread              
        //        simThread.IsBackground = true;
        //        simThread.Start();
        //    }
        //    else
        //    {
        //        if (simThread.ThreadState == ThreadState.Aborted || simThread.IsAlive == false)
        //        {
        //            simThread = new Thread(new ThreadStart(Simulation));
        //            // Start computation process in second thread              
        //            simThread.IsBackground = true;
        //            simThread.Start();
        //        }
        //    }


        //    DataList_realchart = new List<DataGrid_Row_defin>()
        //    {
        //        //new DataGrid_Row_defin(){  IsEnabled = true,  Name = "C200", unit = "V", detail="C200电压曲线"},
        //        //new DataGrid_Row_defin(){  IsEnabled = true,  Name = "C201_A", unit = "V", detail="C201_A电压曲线"},
        //        //new DataGrid_Row_defin(){  IsEnabled = true,  Name = "C201_B", unit = "V", detail="C201_B电压曲线"},
        //    };
        //    DgCustom_real.DataContext = DataList_realchart;
        //    DataGridCell cell_1 = R2R.UControl.DataGridHelper.GetCell(DgCustom_real, 0, 1);
        //    DataGridCell cell_2 = R2R.UControl.DataGridHelper.GetCell(DgCustom_real, 0, 0);
        //    if (cell_1 != null)
        //    {
        //        TextBlock textBlock = cell_1.Content as TextBlock;
        //        textBlock.Background = new SolidColorBrush(colors_realchart[0]);
        //    }
        //    if (cell_2 != null)
        //    {
        //        CheckBox checkBox = cell_2.Content as CheckBox;
        //        checkBox.IsChecked = false;
        //    }
        //}
        //private void Simulation()
        //{
        //    while (true)
        //    {
        //        Random random = new Random();
        //        Point p1 = new Point(dateAxis_real.ConvertToDouble(DateTime.Now), random.NextDouble());
        //        source1.AppendAsync(Dispatcher, p1);
        //        Thread.Sleep(10); // Long-long time for computations...               
        //    }



        //}
        //private void plotter_MouseMove_realChart(object sender, MouseEventArgs e)
        //{
        //    // Get the x and y coordinates of the mouse pointer.
        //    System.Windows.Point position = e.GetPosition(this);
        //    double pX = position.X;
        //    double pY = position.Y;
        //    Thickness myThickness = new Thickness();
        //    myThickness.Left = pX - dockPanel_RealChart.Margin.Left + 50;
        //    myThickness.Top = pY - dockPanel_RealChart.Margin.Top;
        //    myThickness.Right = 0;
        //    myThickness.Bottom = 0;
        //    try
        //    {
        //        DateTime time;
        //        string str = null;
        //        if (followChart_real[0] != null)
        //        {
        //            verticalLine_real.Value = followChart_real[0].MarkerPosition.X;//标线横坐标
        //            time = dateAxis_real.ConvertFromDouble(followChart_real[0].MarkerPosition.X);
        //            str = time.ToString("G") + "\n";
        //        }
        //        foreach (var item in followChart_real)
        //        {
        //            if (item != null)
        //            {
        //                str += item.MarkerPosition.Y.ToString("F2") + "\n";
        //            }
        //        }
        //        tb_real.Text = str;
        //        tb_real.Margin = myThickness;
        //        tb_real.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        ////实时曲线停止采集
        //private void stopchart_Click(object sender, RoutedEventArgs e)
        //{
        //    if (simThread != null)
        //    {
        //        simThread.Abort();
        //    }
        //}
        ////实时曲线保存截图
        //private void savePng_Click_realchart(object sender, RoutedEventArgs e)
        //{
        //    SaveFrameworkElementToImage_real(grid_chart_real);
        //}
        //public void SaveFrameworkElementToImage_real(FrameworkElement ui)
        //{

        //    RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)ui.ActualWidth, (int)ui.ActualHeight, 96d, 96d, PixelFormats.Default);
        //    targetBitmap.Render(ui);
        //    PngBitmapEncoder saveEncoder = new PngBitmapEncoder();
        //    saveEncoder.Frames.Add(BitmapFrame.Create(targetBitmap));
        //    // 指定存储位置
        //    var dialog = new SaveFileDialog();
        //    // 指定后缀
        //    dialog.Filter = "PNG图片|*.png";
        //    if (dialog.ShowDialog().GetValueOrDefault())
        //    {
        //        string filePath = dialog.FileName;
        //        try
        //        {
        //            // 保存文件
        //            System.IO.FileStream fs = System.IO.File.Open(filePath, System.IO.FileMode.OpenOrCreate);
        //            saveEncoder.Save(fs);
        //            fs.Close();
        //            MessageBox.Show("导出成功");
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("请检查存储位置", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
        //private void Page_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    if (simThread != null)
        //    {
        //        simThread.Abort();
        //    }

        //}
        //#endregion*****************************************
        //#endregion
    }

}