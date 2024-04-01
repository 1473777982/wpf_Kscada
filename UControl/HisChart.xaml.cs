using DynamicDataDisplay;
using DynamicDataDisplay.Charts;
using DynamicDataDisplay.Charts.Axes.Numeric;
using DynamicDataDisplay.DataSources;
using InfluxDB.Client;
using Microsoft.Win32;
using MiniExcelLibs;
using NodaTime;
using R2R.protocols;
using R2R.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static R2R.UControl.code_chart;

namespace R2R.UControl
{
    /// <summary>
    /// HisChart.xaml 的交互逻辑
    /// </summary>
    public partial class HisChart : UserControl
    {
        #region  属性      
        public List<var_detail> list_tags
        {
            get { return (List<var_detail>)GetValue(_list); }
            set { SetValue(_list, value); }
        }
        public static readonly DependencyProperty _list =
           DependencyProperty.Register("list_tags", typeof(List<var_detail>), typeof(HisChart),
                 typeMetadata: new FrameworkPropertyMetadata(
                          defaultValue: null,
                          flags: FrameworkPropertyMetadataOptions.AffectsMeasure,
                          propertyChangedCallback: new PropertyChangedCallback(list_changed)));
        private static void list_changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is List<var_detail> newValue)
            {
                if ((DataList_hischart != null))
                {
                    try
                    {
                        if (newValue != null)
                        {
                            DataList_hischart.Clear();
                            //DataGrid显示数据和内容         
                            for (int i = 0; i < newValue.Count; i++)
                            {
                                DataList_hischart.Add(new DataGrid_Row_defin()
                                {
                                    IsEnabled = true,
                                    Name = newValue[i].name,
                                    Unit = newValue[i].unit,
                                    Describe = newValue[i].describe,
                                });
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"载入数据出错：{ex.Message}");
                    }
                }

            }
        }

        public bool vertical_log
        {
            get { return (bool)GetValue(_vertical_log); }
            set { SetValue(_vertical_log, value); }
        }
        public static readonly DependencyProperty _vertical_log =
           DependencyProperty.Register("vertical_log", typeof(bool), typeof(HisChart), new PropertyMetadata(false, new PropertyChangedCallback(OnVerticalLogChanged)));
        private static void OnVerticalLogChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HisChart control = d as HisChart;
            control.OnVerticalLogChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnVerticalLogChanged(bool oldValue, bool newValue)
        {
            // 触发vertical_log改变事件的处理逻辑
            try
            {
                if (newValue)
                {
                    // 对数坐标
                    plotter_his.DataTransform = new Log10YTransform();
                    VerticalAxis axis = new VerticalAxis//定义对数坐标轴
                    {
                        TicksProvider = new LogarithmNumericTicksProvider(10),
                        LabelProvider = new UnroundingLabelProvider()
                    };
                    plotter_his.MainVerticalAxis = axis;//设定纵轴为axis：对数坐标                                                      
                    plotter_his.AxisGrid.DrawVerticalMinorTicks = true; //纵轴刻度 
                }
                else
                {
                    // 设置普通坐标
                    plotter_his.AxisGrid.DrawVerticalMinorTicks = false; //纵轴刻度                  
                    VerticalAxis axis = new VerticalAxis//定义对数坐标轴
                    {
                        TicksProvider = new NumericTicksProvider(),
                        LabelProvider = new ExponentialLabelProvider()
                    };
                    plotter_his.MainVerticalAxis = axis;//设定纵轴为axis：对数坐标                                                      
                    plotter_his.DataTransform = new IdentityTransform();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("当前状态不允许切换对数坐标");
            }

        }
        public string vertical_titlename
        {
            get { return (string)GetValue(_vertical_titlename); }
            set { SetValue(_vertical_titlename, value); }
        }
        public static readonly DependencyProperty _vertical_titlename =
           DependencyProperty.Register("vertical_titlename", typeof(string), typeof(HisChart), new PropertyMetadata(""));
        //public ConcurrentDictionary<string, DataRow> Dic
        //{
        //    get { return (ConcurrentDictionary<string, DataRow>)GetValue(_Dic); }
        //    set { SetValue(_Dic, value); }
        //}
        //public static readonly DependencyProperty _Dic =
        //   DependencyProperty.Register("Dic", typeof(string), typeof(HisChart));
        #endregion

        public HisChart()
        {
            InitializeComponent();

            // 设置初始时上半部分的高度为660
            grid_Hchart.RowDefinitions[1].Height = new GridLength(660);
            // 将当前系统时间转换为 double 类型的数值
            double currentTimeValue = dateAxis_his.ConvertToDouble(DateTime.Now);
            // 设置 X 轴的初始显示时间
            plotter_his.Viewport.Visible = new DataRect(currentTimeValue, -8, 0.1, 12);
        }


        #region 历史曲线 **************************************************************************   
        DispatcherTimer dispatcherTimer_realchart = new DispatcherTimer();
        List<LineGraph> lineGraphs = new List<LineGraph>();
        List<ObservableDataSource<Point>> lst_currentDataFrame = new List<ObservableDataSource<Point>>();
        List<DataFollowChart> followCharts_his = new List<DataFollowChart>();
        Point point = new Point(0, 0);
        HorizontalDateTimeAxis axis = new HorizontalDateTimeAxis();
        List<List<Point>> points_Export = new List<List<Point>>();
        List<var_detail> list_tags_temp = new List<var_detail>();
        public static ObservableCollection<DataGrid_Row_defin> DataList_hischart { get; set; } // 表格list
        private void plotter_his_Loaded(object sender, RoutedEventArgs e)
        {
            // 订阅鼠标滚轮事件
            plotter_his.Viewport.PropertyChanged += Viewport_PropertyChanged;
            //DataContext = this;
            // setting custom position of legend:
            // it will be placed not in the top right corner of plotter,
            // but in the top left one
            dateAxis_his.LabelProvider.LabelStringFormat = "G";

            //if (vertical_log)
            //{
            //    // 对数坐标
            //    plotter_his.DataTransform = new Log10YTransform();
            //    VerticalAxis axis = new VerticalAxis//定义对数坐标轴
            //    {
            //        TicksProvider = new LogarithmNumericTicksProvider(10),
            //        LabelProvider = new UnroundingLabelProvider()
            //    };
            //    plotter_his.MainVerticalAxis = axis;//设定纵轴为axis：对数坐标
            //    plotter_his.AxisGrid.DrawVerticalMinorTicks = true; //纵轴刻度              
            //}
            if (vertical_titlename != null)
            {
                VerticalAxisTitle title = new VerticalAxisTitle()
                {
                    Content = vertical_titlename
                };
                plotter_his.Children.Add(title);
            }
            DataList_hischart = new ObservableCollection<DataGrid_Row_defin>();
            DgCustom_his.ItemsSource = DataList_hischart;
            refresh_DataGrid();
            dispatcherTimer_realchart.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_realchart.Tick += new EventHandler(TimeAction);
            dispatcherTimer_realchart.Start();
        }
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                if (DgCustom_his.Items.Count > 0 || lineGraphs.Count > 0 || followCharts_his.Count > 0)
                {
                    for (int i = 0; i < DgCustom_his.Items.Count; i++)
                    {
                        DataGridCell cell = DataGridHelper.GetCell(DgCustom_his, i, 0);
                        if (cell != null)
                        {
                            CheckBox checkBox = cell.Content as CheckBox;
                            if (checkBox.IsChecked == true)
                            {
                                lineGraphs[i].Visibility = Visibility.Visible;
                                followCharts_his[i].Visibility = Visibility.Visible;
                            }
                            else
                            {
                                lineGraphs[i].Visibility = Visibility.Hidden;
                                followCharts_his[i].Visibility = Visibility.Hidden;
                            }
                        }

                    }
                }

            }
            catch (Exception)
            {

            }
           
            //更换曲线组则清空现有曲线数据
            try
            {
                if (list_tags != null)
                {
                    if (list_tags.Count > 0)
                    {
                        if (list_tags_temp.Count > 0)
                        {
                            if (!list_tags[0].name.Equals(list_tags_temp[0].name))
                            {

                                clearAll();
                                list_tags_temp.Clear();
                                for (int i = 0; i < list_tags.Count; i++)
                                {
                                    list_tags_temp.Add(new var_detail()
                                    {
                                        unit = list_tags[i].unit,
                                        name = list_tags[i].name,
                                        describe = list_tags[i].describe
                                    });
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < list_tags.Count; i++)
                            {
                                list_tags_temp.Add(new var_detail()
                                {
                                    unit = list_tags[i].unit,
                                    name = list_tags[i].name,
                                    describe = list_tags[i].describe
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        //刷新表格
        public void refresh_DataGrid()
        {

            if (list_tags != null)
            {
                DataList_hischart.Clear();
                //DataGrid显示数据和内容         
                for (int i = 0; i < list_tags.Count; i++)
                {
                    DataList_hischart.Add(new DataGrid_Row_defin()
                    {
                        IsEnabled = true,
                        Name = list_tags[i].name,
                        Unit = list_tags[i].unit,
                        Describe = list_tags[i].describe,
                    });
                }
                //DgCustom_his.DataContext = DataGridList_realchart;

            }


        }


        /// <summary>
        /// 查询历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void search_click(object sender, RoutedEventArgs e)
        {
            if (list_tags == null)
            {
                MessageBox.Show("请先选择曲线组");
                return;
            }
            else
            {
                Button_search.IsEnabled = false;
                if (!string.IsNullOrEmpty(Tpicker1.DateTimeStr) && !string.IsNullOrEmpty(Tpicker2.DateTimeStr))
                {
                    var t1 = Convert.ToDateTime(Tpicker1.DateTimeStr);
                    var t2 = Convert.ToDateTime(Tpicker2.DateTimeStr);

                    if (t1 < t2)
                    {
                        chart_addline(t1, t2);
                    }
                    else
                    {
                        MessageBox.Show("stop时间小于start时间");
                    }


                }
                Button_search.IsEnabled = true;
            }


        }
        /// <summary>
        /// 从influxDB获取数据
        /// </summary>
        public async void chart_addline(DateTime dateTime1, DateTime dateTime2)
        {
            clearAll();
            TimeZoneInfo localTime = TimeZoneInfo.Local; //本地时区时间
            //DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, localTime);//完成转换
            //var influxDBClient = new InfluxDBClient("http://localhost:8086", App.Token);

            var color = ColorHelper.CreateRandomColors(list_tags.Count);


            try
            {
                for (int i = 0; i < list_tags.Count; i++)
                {
                    lst_currentDataFrame.Add(new ObservableDataSource<Point>());
                    points_Export.Add(new List<Point>());
                    //getData(influxDBClient, i, dateTime1, dateTime2);


                    //getData为异步执行数据读取，考虑是否需要添加限制条件，保证数据已经读取%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    var lst =  await getData(App.influxDBClient, i, dateTime1, dateTime2);
                    if (lst.Count>0)
                    {
                        lineGraphs.Add(plotter_his.AddLineGraph(lst_currentDataFrame[i], color[i], 2, list_tags[i].describe));
                        followCharts_his.Add(new DataFollowChart(lineGraphs[i]));
                        plotter_his.Children.Add(followCharts_his[i]);
                        plotter_his.FitToView();
                    }
                    else
                    {
                        var win = new hisChart_Err(list_tags[i].name);
                        win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        win.ShowDialog();
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("读取历史数据失败，请修改时间选择");
            }
            
        }
        public async Task<List<Point>> getData(InfluxDBClient influxDBClient, int index, DateTime dateTime1, DateTime dateTime2)
        {
            List<Point> lst = new List<Point>();
            try
            {

                    string timeRange1 = TimeZoneInfo.ConvertTimeToUtc(dateTime1, TimeZoneInfo.Local).ToString("yyyy-MM-ddTHH:mm:ss.ffZ");
                    string timeRange2 = TimeZoneInfo.ConvertTimeToUtc(dateTime2, TimeZoneInfo.Local).ToString("yyyy-MM-ddTHH:mm:ss.ffZ");

                    var fluxQuery = $"from(bucket: \"{App.bucket}\")"
                                  + $"|> range(start:{timeRange1},stop:{timeRange2})"
                                  + $"|> filter(fn: (r) => (r[\"_measurement\"] == \"{App.measurement}\" and r[\"_field\"] == \"value\" and r[\"tag_name\"] == \"{list_tags[index].name}\"))"
                                  + "|> aggregateWindow(every: 1m, fn: mean, createEmpty: true)";
                    var fluxTables = await influxDBClient.GetQueryApi().QueryAsync(fluxQuery, App.org);                   
                    fluxTables.ForEach(fluxTable =>
                    {
                        var fluxRecords = fluxTable.Records;
                        
                        fluxRecords.ForEach(fluxRecord =>
                        {

                            //point.X = axis.ConvertToDouble((DateTime)fluxRecord.GetTimeInDateTime());
                            //point.Y = Convert.ToDouble(fluxRecord.GetValue());
                            //lst_currentDataFrame[index].AppendAsync(Dispatcher, point);
                            //TimeZoneInfo.ConvertTimeFromUtc((DateTime)fluxRecord.GetTimeInDateTime(), localTime);
                            point.X = axis.ConvertToDouble(TimeZoneInfo.ConvertTimeFromUtc((DateTime)fluxRecord.GetTimeInDateTime(), TimeZoneInfo.Local));
                            if (vertical_log)
                            {
                                if (fluxRecord.GetValue() == null || (double)fluxRecord.GetValue() == 0)
                                {
                                    point.Y = 1;
                                }
                                else
                                {
                                    point.Y = Convert.ToDouble(fluxRecord.GetValue());
                                }
                            }
                            else
                            {
                                point.Y = Convert.ToDouble(fluxRecord.GetValue());
                            }
                            lst.Add(point);
                        });
                        lst_currentDataFrame[index].AppendMany(lst);
                        points_Export[index] = lst;
                       
                    });
               
            } 
            catch (Exception)
            {
                
            }
            return lst;
        }
        /// <summary>
        /// 鼠标跟随
        /// 需要为每个单独曲线添加鼠标跟随的显示值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plotter_MouseMove_hisChart(object sender, MouseEventArgs e)
        {
            // Get the x and y coordinates of the mouse pointer.
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X;
            double pY = position.Y;
            Thickness myThickness = new Thickness();
            myThickness.Left = pX - dockPanel_HisChart.Margin.Left + 50;
            myThickness.Top = pY - dockPanel_HisChart.Margin.Top;
            myThickness.Right = 0;
            myThickness.Bottom = 0;
            try
            {
                DateTime time;
                string str = null;
                if (followCharts_his[0] != null)
                {
                    verticalLine_his.Value = followCharts_his[0].MarkerPosition.X;//标线横坐标
                    time = dateAxis_his.ConvertFromDouble(followCharts_his[0].MarkerPosition.X);
                    str = time.ToString("G") + "\n";
                }
                for (int i = 0; i < followCharts_his.Count; i++)
                {
                    if (followCharts_his[i] != null)
                    {
                        if (vertical_log)
                        {
                            str += list_tags[i].name + ": " + Math.Pow(10, followCharts_his[i].MarkerPosition.Y).ToString("0.0000E+0") + "\n";
                        }
                        else
                        {
                            str += list_tags[i].name + ": " + followCharts_his[i].MarkerPosition.Y.ToString("F2") + "\n";
                        }
                    }
                }
                tb_his.Text = str;
                tb_his.Margin = myThickness;
                tb_his.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 将List<Point_with_datetime>转换为EnumerableDataSource<Point_with_datetime>即曲线控件需要的数据类型 IPointDataSource
        /// </summary>
        /// <param name="rates"></param>
        /// <returns></returns>
        private EnumerableDataSource<Point_with_datetime> CreateCurrencyDataSource(List<Point_with_datetime> rates)
        {
            EnumerableDataSource<Point_with_datetime> ds = new EnumerableDataSource<Point_with_datetime>(rates);
            ds.SetXMapping(ci => dateAxis_his.ConvertToDouble(ci.Date));
            ds.SetYMapping(ci => ci.Rate);
            return ds;
        }

        /// <summary>
        /// 清除chart按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeAll_Click_hischart(object sender, RoutedEventArgs e)
        {
            clearAll();
        }
        /// <summary>
        /// 保存截图
        /// </summary>
        /// <param name="ui">控件名称</param>
        /// <param name="filename">图片文件名</param>
        public void SaveFrameworkElementToImage_his(FrameworkElement ui)
        {

            RenderTargetBitmap targetBitmap = new RenderTargetBitmap((int)ui.ActualWidth, (int)ui.ActualHeight, 96d, 96d, PixelFormats.Default);
            targetBitmap.Render(ui);
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
        /// <summary>
        /// 保存截图
        /// </summary>
        /// <param name="ui">控件名称</param>
        /// <param name="filename">图片文件名</param>
        private void savePng_Click_hischart(object sender, RoutedEventArgs e)
        {
            SaveFrameworkElementToImage_his(grid_chart_his);
        }

        //保存表格
        private void saveExcel_Click_hischart(object sender, RoutedEventArgs e)
        {
            if (points_Export.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    string filePath = saveFileDialog.FileName;
                    var sheets = new DataSet();
                    for (int i = 0; i < points_Export.Count; i++)
                    {
                        DataTable dataTable = new DataTable(list_tags[i].name);
                        dataTable.Columns.Add("x", typeof(DateTime)); // Assuming X is of type DateTime
                        dataTable.Columns.Add("y", typeof(double));

                        foreach (var point in points_Export[i])
                        {
                            dataTable.Rows.Add(dateAxis_his.ConvertFromDouble(point.X), point.Y);
                        }

                        sheets.Tables.Add(dataTable);
                    }
                    MiniExcel.SaveAs(filePath, sheets);
                }
            }
        }

        /// <summary>
        /// 时间选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timepicker_click(object sender, RoutedEventArgs e)
        {
            //DatetimePicker win_timepicker = new DatetimePicker();
            //win_timepicker.add_line += getdata;
            //win_timepicker.ShowDialog();
        }



        #endregion*************************************************************************************************************************************************************************

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataList_hischart != null)
                {
                    DataList_hischart = null;
                }
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click_FitToView(object sender, RoutedEventArgs e)
        {
            plotter_his.FitToView();
        }

        private void clearAll()
        {
            plotter_his.Children.RemoveAll<LineGraph>();
            plotter_his.Children.RemoveAll<DataFollowChart>();
            followCharts_his.Clear();
            tb_his.Text = null;
            points_Export.Clear();
            lst_currentDataFrame.Clear();
            lineGraphs.Clear();
            points_Export.Clear();
        }

        private void Viewport_PropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
        {
            if (vertical_log)
            {
                // 检查是否是缩放操作
                if (e.PropertyName == "Visible")
                {
                    // 获取Viewport2D实例
                    var viewport = sender as Viewport2D;

                    // 手动控制X轴和Y轴的缩放范围
                    if (viewport.Visible.YMin < -20 || viewport.Visible.YMax > 20)
                    {
                        plotter_his.Viewport.Visible = new DataRect(viewport.Visible.XMin, -20, viewport.Visible.XMax - viewport.Visible.XMin, 40);
                    }

                }
            }
           
        }
    }
}
