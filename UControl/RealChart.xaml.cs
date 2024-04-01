using common.tag;
using DynamicDataDisplay;
using DynamicDataDisplay.Charts;
using DynamicDataDisplay.Charts.Axes.Numeric;
using DynamicDataDisplay.DataSources;
using Microsoft.Win32;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static R2R.UControl.code_chart;

namespace R2R.UControl
{
    /// <summary>
    /// RealChart.xaml 的交互逻辑
    /// </summary>
    public partial class RealChart : UserControl
    {
        static List<var_detail> tagsList = new List<var_detail>();
        static List<object> var_values = new List<object>();
        bool log;
        #region  属性      
        public List<var_detail> list_tags
        {
            get { return (List<var_detail>)GetValue(_list); }
            set { SetValue(_list, value); }
        }
        public static readonly DependencyProperty _list =
           DependencyProperty.Register("list_tags", typeof(List<var_detail>), typeof(RealChart),
                 typeMetadata: new FrameworkPropertyMetadata(
                          defaultValue: null,
                          flags: FrameworkPropertyMetadataOptions.AffectsMeasure,
                          propertyChangedCallback: new PropertyChangedCallback(list_changed)));
        private static void list_changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is List<var_detail> newValue)
            {             
                if ((DataList_realchart != null))
                {
                    try
                    {
                        if (newValue != null)
                        {
                            DataList_realchart.Clear();
                            //DataGrid显示数据和内容         
                            for (int i = 0; i < newValue.Count; i++)
                            {
                                DataList_realchart.Add(new DataGrid_Row_defin()
                                {
                                    IsEnabled = true,
                                    Name = newValue[i].name,
                                    Unit = newValue[i].unit,
                                    Describe = newValue[i].describe,
                                });
                            }
                            if (tagsList == null)
                            {
                                tagsList = new List<var_detail>();
                            }
                            else
                            {
                                tagsList.Clear();
                            }
                            
                            foreach (var item in newValue)
                            {
                                tagsList.Add(item);
                            }
                            if (var_values == null)
                            {
                                var_values = new List<object>();
                            }
                            else
                            {
                                var_values.Clear();                               
                            }
                            foreach (var v in tagsList) { var_values.Add(new object()); }
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
            DependencyProperty.Register("vertical_log", typeof(bool), typeof(RealChart), new PropertyMetadata(false, new PropertyChangedCallback(OnVerticalLogChanged)));
        private static void OnVerticalLogChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RealChart control = d as RealChart;
            control.OnVerticalLogChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        //坐标变换
        protected virtual void OnVerticalLogChanged(bool oldValue, bool newValue)
        {
            // 触发vertical_log改变事件的处理逻辑
            try
            {
                if (newValue)
                {
                    // 对数坐标
                    plotter_real.DataTransform = new Log10YTransform();
                    VerticalAxis axis = new VerticalAxis//定义对数坐标轴
                    {
                        TicksProvider = new LogarithmNumericTicksProvider(10),
                        LabelProvider = new UnroundingLabelProvider()
                    };
                    plotter_real.MainVerticalAxis = axis;//设定纵轴为axis：对数坐标
                    plotter_real.AxisGrid.DrawVerticalMinorTicks = true; //纵轴刻度  
                }
                else
                {
                    // 设置普通坐标
                    plotter_real.AxisGrid.DrawVerticalMinorTicks = false; //纵轴刻度 
                    VerticalAxis axis = new VerticalAxis
                    {
                        TicksProvider = new NumericTicksProvider(),
                        LabelProvider = new ExponentialLabelProvider()
                    };
                    plotter_real.MainVerticalAxis = axis;                                                   
                    plotter_real.DataTransform = new IdentityTransform();
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
           DependencyProperty.Register("vertical_titlename", typeof(string), typeof(RealChart), new PropertyMetadata(""));
        //public ConcurrentDictionary<string, DataRow> Dic
        //{
        //    get { return (ConcurrentDictionary<string, DataRow>)GetValue(_Dic); }
        //    set { SetValue(_Dic, value); }
        //}
        //public static readonly DependencyProperty _Dic =
        //   DependencyProperty.Register("Dic", typeof(string), typeof(RealChart));
        #endregion



        public RealChart()
        {
            InitializeComponent();
            log = vertical_log;
            // 设置初始时上半部分的高度为660
            grid_Rchart.RowDefinitions[1].Height = new GridLength(660);
            // 将当前系统时间转换为 double 类型的数值
            double currentTimeValue = dateAxis_real.ConvertToDouble(DateTime.Now); 
            // 设置 X 轴的初始显示时间
            plotter_real.Viewport.Visible = new DataRect(currentTimeValue, -8, 1, 12);
        }
        #region   实时曲线逻辑

        Thread simThread;
        DispatcherTimer dispatcherTimer_realchart = new DispatcherTimer();
        //List<double> list_tags_double = new List<double>();
        List<LineGraph> lineGraphs = new List<LineGraph>();
        List<DataFollowChart> followCharts_real = new List<DataFollowChart>();
        List<ObservableDataSource<Point>> source = new List<ObservableDataSource<Point>>();
        List<List<Point>> points_Export = new List<List<Point>>();//保存excel使用
        Color[] colors_realchart; // 曲线颜色
        public static ObservableCollection<DataGrid_Row_defin> DataList_realchart { get; set; } // 表格list
        List<var_detail> list_tags_temp = new List<var_detail>();
        private void plotter_real_Loaded(object sender, RoutedEventArgs e)
        {
            // 订阅鼠标滚轮事件
            plotter_real.Viewport.PropertyChanged += Viewport_PropertyChanged;
            //DataContext = this;
            // setting custom position of legend:
            // it will be placed not in the top right corner of plotter,
            // but in the top left one
            dateAxis_real.LabelProvider.LabelStringFormat = "G";
            //if (vertical_log)
            //{
            //    // 对数坐标
            //    plotter_real.DataTransform = new Log10YTransform();
            //    VerticalAxis axis = new VerticalAxis//定义对数坐标轴
            //    {
            //        TicksProvider = new LogarithmNumericTicksProvider(10),
            //        LabelProvider = new UnroundingLabelProvider()
            //    };
            //    plotter_real.MainVerticalAxis = axis;//设定纵轴为axis：对数坐标
            //    plotter_real.AxisGrid.DrawVerticalMinorTicks = true; //纵轴刻度              
            //}
            if (vertical_titlename != null)
            {
                VerticalAxisTitle title = new VerticalAxisTitle()
                {
                    Content = vertical_titlename
                };
                plotter_real.Children.Add(title);
            }
            DataList_realchart = new ObservableCollection<DataGrid_Row_defin>();
            DgCustom_real.ItemsSource = DataList_realchart;
            if (list_tags != null)
            {
                if (tagsList == null)
                {
                    tagsList = new List<var_detail>();
                }
                else
                {
                    tagsList.Clear();
                }

                foreach (var item in list_tags)
                {
                    tagsList.Add(item);
                }
                if (var_values == null)
                {
                    var_values = new List<object>();
                }
                else
                {
                    var_values.Clear();
                }
                foreach (var v in tagsList) { var_values.Add(new object()); }
            }
           
            refresh_DataGrid();
            dispatcherTimer_realchart.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer_realchart.Tick += new EventHandler(TimeAction);
            dispatcherTimer_realchart.Start();
        }
        // 刷新UI显示
        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                if (DgCustom_real.Items.Count > 0 && lineGraphs.Count > 0 && followCharts_real.Count > 0)
                {
                    for (int i = 0; i < DgCustom_real.Items.Count; i++)
                    {
                        DataGridCell cell = DataGridHelper.GetCell(DgCustom_real, i, 0);
                        CheckBox checkBox = cell.Content as CheckBox;
                        if (checkBox.IsChecked == true)
                        {
                            lineGraphs[i].Visibility = Visibility.Visible;
                            followCharts_real[i].Visibility = Visibility.Visible;
                        }
                        else
                        {
                            lineGraphs[i].Visibility = Visibility.Hidden;
                            followCharts_real[i].Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            try 
            {
                if (simThread != null)
                {
                    if (simThread.IsAlive)//if (thread.ThreadState == ThreadState.Running && thread.IsAlive)
                    {
                        button_start.IsEnabled = false;
                    }
                    else
                    {
                        button_start.IsEnabled = true;
                    }
                }
            } 
            catch 
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
                                if (simThread != null)
                                {
                                    simThread.Abort();
                                }
                                plotter_real.Children.RemoveAll<LineGraph>();
                                plotter_real.Children.RemoveAll<DataFollowChart>();
                                followCharts_real.Clear();
                                tb_real.Text = null;
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
        public void refresh_DataGrid()
        {
            //lock (list_tags)
            //{
            if (list_tags != null)
            {
                DataList_realchart.Clear();
                //DataGrid显示数据和内容         
                for (int i = 0; i < list_tags.Count; i++)
                {
                    DataList_realchart.Add(new DataGrid_Row_defin()
                    {
                        IsEnabled = true,
                        Name = list_tags[i].name,
                        Unit = list_tags[i].unit,
                        Describe = list_tags[i].describe,
                    });
                }

            }
            //}
        }

        //实时曲线开始采集
        private void startchart_Click(object sender, RoutedEventArgs e)
        {
            if (list_tags == null)
            {
                return;
            }
            else
            {
                if (list_tags.Count < 1) return;
            }
            //初始化
            source.Clear();
            points_Export.Clear();
            lineGraphs.Clear();
            followCharts_real.Clear();
            plotter_real.Children.RemoveAll<LineGraph>();
            plotter_real.Children.RemoveAll<DataFollowChart>();
            //随机颜色
            colors_realchart = ColorHelper.CreateRandomColors(list_tags.Count);
            
            for (int i = 0; i < list_tags.Count; i++)
            {
                // Create first source
                source.Add(new ObservableDataSource<Point>());
                points_Export.Add(new List<Point>());
                // Set identity mapping of point in collection to point on plot
                source[i].SetXYMapping(p => p);
                // Add all  graphs. Colors are not specified and chosen random
                //添加曲线到面板               
                lineGraphs.Add(plotter_real.AddLineGraph(source[i], colors_realchart[i], 2, list_tags[i].describe));
                followCharts_real.Add(new DataFollowChart(lineGraphs[i]));
                plotter_real.Children.Add(followCharts_real[i]);               
            }
            //plotter_real.RemoveUserElements();

            //启动线程
            if (simThread == null)
            {
                simThread = new Thread(new ThreadStart(Simulation));
                // Start computation process in second thread              
                simThread.IsBackground = true;
                simThread.Start();
            }
            else
            {
                if (simThread.ThreadState == ThreadState.Aborted || simThread.IsAlive == false)
                {
                    simThread = new Thread(new ThreadStart(Simulation));
                    // Start computation process in second thread              
                    simThread.IsBackground = true;
                    simThread.Start();
                }
            }

            // 将当前系统时间转换为 double 类型的数值
            double currentTimeValue = dateAxis_real.ConvertToDouble(DateTime.Now);
            // 设置 X 轴的初始显示时间
            plotter_real.Viewport.Visible = new DataRect(currentTimeValue, -8, 0.1, 12);

            if (!vertical_log)
            {
                plotter_real.FitToView();
            }

        }
        //线程执行函数：描点
        
        private void Simulation()
        {

            while (true)
            {
                try
                {
                    if (tagsList != null)
                    {
                        for (int i = 0; i < tagsList.Count; i++)
                        {
                            Point p0 = new Point();
                            var_values[i] = communicationTag.Current.Dic_ranTags[tagsList[i].name].value;
                            if (var_values[i] != null)
                            {
                                if (var_values[i].GetType() == typeof(double)
                                    || var_values[i].GetType() == typeof(int)
                                    || var_values[i].GetType() == typeof(float)
                                    || var_values[i].GetType() == typeof(Single))
                                {
                                    if (log && Convert.ToDouble(var_values[i]) <= 0)
                                    {
                                        p0 = new Point(dateAxis_real.ConvertToDouble(DateTime.Now), 1);
                                    }
                                    else
                                    {
                                        p0 = new Point(dateAxis_real.ConvertToDouble(DateTime.Now), Convert.ToDouble(var_values[i]));
                                    }

                                    //source[i].AppendAsync(Dispatcher, p0);
                                }
                                else
                                {
                                    if (var_values[i].GetType() == typeof(Boolean)
                                        || var_values[i].GetType() == typeof(bool))
                                    {
                                        var a = (Boolean)var_values[i] == false ? 0.0 : 1.0;
                                        p0 = new Point(dateAxis_real.ConvertToDouble(DateTime.Now), a);
                                        //source[i].AppendAsync(Dispatcher, p0);
                                    }
                                    else
                                    {
                                        p0 = new Point(dateAxis_real.ConvertToDouble(DateTime.Now), 0.0);
                                        //source[i].AppendAsync(Dispatcher, p0);
                                    }
                                }
                                source[i].AppendAsync(Dispatcher, p0);
                                points_Export[i].Add(p0);
                            }

                        }
                    }
                   

                }
                catch (Exception)
                {

                }
                        
                Thread.Sleep(200); // Long-long time for computations...               
            }
        }
        //鼠标跟随
        private void plotter_MouseMove_realChart(object sender, MouseEventArgs e)
        {
            // Get the x and y coordinates of the mouse pointer.
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X;
            double pY = position.Y;
            Thickness myThickness = new Thickness();
            myThickness.Left = pX - dockPanel_RealChart.Margin.Left + 50;
            myThickness.Top = pY - dockPanel_RealChart.Margin.Top;
            myThickness.Right = 0;
            myThickness.Bottom = 0;
            try
            {
                DateTime time;
                string str = null;
                if (followCharts_real[0] != null)
                {
                    verticalLine_real.Value = followCharts_real[0].MarkerPosition.X;//标线横坐标
                    time = dateAxis_real.ConvertFromDouble(followCharts_real[0].MarkerPosition.X);
                    str = time.ToString("G") + "\n";
                }
                for (int i = 0; i < followCharts_real.Count; i++)
                {
                    if (followCharts_real[i] != null)
                    {
                        if (vertical_log)
                        {
                            str += list_tags[i].name + ": " + Math.Pow(10, followCharts_real[i].MarkerPosition.Y).ToString("0.0000E+0") + "\n";
                        }
                        else
                        {
                            str += list_tags[i].name + ": " + followCharts_real[i].MarkerPosition.Y.ToString("F2") + "\n";
                        }
                    }
                }              
                tb_real.Text = str;
                tb_real.Margin = myThickness;
                tb_real.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
            }
        }

        //实时曲线停止采集
        private void stopchart_Click(object sender, RoutedEventArgs e)
        {
            if (simThread != null)
            {
                simThread.Abort();
            }          
        }

        //实时曲线/dataflow清除
        private void removeAll_Click_realchart(object sender, RoutedEventArgs e)
        {
            if (simThread != null)
            {
                simThread.Abort();
            }
            plotter_real.Children.RemoveAll<LineGraph>();
            plotter_real.Children.RemoveAll<DataFollowChart>();
            followCharts_real.Clear();
            tb_real.Text = null;
            points_Export.Clear();
        }

        //实时曲线保存截图
        private void savePng_Click_realchart(object sender, RoutedEventArgs e)
        {
            SaveFrameworkElementToImage_real(grid_chart_real);
        }
        public void SaveFrameworkElementToImage_real(FrameworkElement ui)
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
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
           
            try
            {
                //if (simThread != null)
                //{
                //    simThread.Abort();
                //}
                DataList_realchart = null;
                tagsList = null;
                var_values = null;
            }
            catch (Exception)
            {

            }
           
        }



        //List<object>转为List<double>
        //void list_convert()
        //{
        //    foreach (var item in list_tags)
        //    {
        //        if (item.GetType()==typeof(double)|| item.GetType() == typeof(int)|| item.GetType() == typeof(float)|| item.GetType() == typeof(Single))
        //        {
        //            list_tags_double.Add((double)item);
        //        }
        //        if (item.GetType() == typeof(Boolean)|| item.GetType() == typeof(bool))
        //        {
        //            var a = (Boolean)item == false ? 0.0 : 1.0;
        //            list_tags_double.Add(a);
        //        }
        //    }
        //}

        #endregion

        private void saveExcel_Click_realchart(object sender, RoutedEventArgs e)
        {
            if (simThread != null)
            {
                if (!simThread.IsAlive)
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
                                    
                                    dataTable.Rows.Add(dateAxis_real.ConvertFromDouble(point.X), point.Y);
                                }

                                sheets.Tables.Add(dataTable);
                            }
                            MiniExcel.SaveAs(filePath, sheets);
                        }
                    }
                }
            }

        }

        private void Button_Click_FitToView(object sender, RoutedEventArgs e)
        {
            if (!vertical_log)
            {
                plotter_real.FitToView();
            }
            
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
                        plotter_real.Viewport.Visible = new DataRect(viewport.Visible.XMin, -20, viewport.Visible.XMax - viewport.Visible.XMin, 40);
                    }

                }
            }

        }
    }
}
