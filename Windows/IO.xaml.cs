using common.tag;
using InfluxDB.Client.Api.Domain;
using MiniExcelLibs;
using R2R.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace R2R.Windows
{
    /// <summary>
    /// IO.xaml 的交互逻辑
    /// </summary>
    public partial class IO : Window
    {
        DispatcherTimer dispatcherTimer_IO = new DispatcherTimer();
        DataTable table_var_i, table_var_o, table_UI_i, table_UI_o;
        public IO()
        {
            InitializeComponent();
            table_var_i = new DataTable();
            table_var_o = new DataTable();

            string Path_i = "sheet_IO.xlsx";
            table_var_i = MiniExcel.QueryAsDataTable(Path_i, sheetName: "I");
            table_UI_i = table_var_i.Copy();

            string Path_o = "sheet_IO.xlsx";
            table_var_o = MiniExcel.QueryAsDataTable(Path_o, sheetName: "O");
            table_UI_o = table_var_o.Copy();

            dispatcherTimer_IO.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer_IO.Tick += new EventHandler(TimeAction);
            dispatcherTimer_IO.Start();
            var l = dtGrid_o.IsLoaded;
            var l1 = dtGrid_i.IsLoaded;
        }


        private void dtGrid_i_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            forceDialog(dataGrid, table_var_i);

        }

        private void dtGrid_o_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            forceDialog(dataGrid, table_var_o);

        }

        private void forceDialog(DataGrid dataGrid,DataTable table)
        {
            if (dataGrid.IsLoaded)
            {
                if (dataGrid != null)
                {
                    var cellInfo = dataGrid.CurrentCell;
                    if (cellInfo != null)
                    {
                        var row = dataGrid.Items.IndexOf(cellInfo.Item);
                        var column = cellInfo.Column.DisplayIndex;
                        var name = Convert.ToString(table.Rows[row][column]);
                        if (name.Contains("force"))
                        {
                            var dialog = new io_force(name);
                            var mousePosition = Mouse.GetPosition(this);
                            DialogPosition.setDialogPosition(dialog, mousePosition);
                            dialog.ShowDialog();
                        }
                        //MessageBox.Show($"Row: {row}, Column: {column}");
                    }
                }
            }
        }

        private void dtGrid_i_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn textColumn = e.Column as DataGridTextColumn;
            if (textColumn != null)
            {
                textColumn.ElementStyle = new Style(typeof(TextBlock), textColumn.ElementStyle);
                textColumn.ElementStyle.Setters.Add(new Setter(TextBlock.BackgroundProperty, new Binding(e.PropertyName) { Converter = new BoolToColorConverter() }));
            }
        }

        private void dtGrid_o_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn textColumn = e.Column as DataGridTextColumn;
            if (textColumn != null)
            {
                textColumn.ElementStyle = new Style(typeof(TextBlock), textColumn.ElementStyle);
                textColumn.ElementStyle.Setters.Add(new Setter(TextBlock.BackgroundProperty, new Binding(e.PropertyName) { Converter = new BoolToColorConverter() }));
            }
        }

        private void TimeAction(object sender, EventArgs e)
        {
            try
            {
                //if (dtGrid_i.IsLoaded)
                //{
                for (int i = 0; i < table_var_i.Rows.Count; i++)
                {
                    for (int j = 0; j < table_var_i.Rows[i].ItemArray.Count(); j++)
                    {
                        if (!DBNull.Value.Equals(table_var_i.Rows[i][j]))
                        {
                            var name = Convert.ToString(table_var_i.Rows[i][j]);
                            if (name.Length > 10)
                            {
                                if (name.Substring(name.Length-11) == "description")
                                {
                                    var value = Convert.ToString(tag_manager.Current.getTagValue(name));
                                    table_UI_i.Rows[i][j] = value;
                                }
                                else
                                {
                                    if (name.Substring(0, 2) == "in")
                                    {
                                        var value = Convert.ToBoolean(tag_manager.Current.getTagValue(name));
                                        table_UI_i.Rows[i][j] = value;

                                    }
                                }
                                    
                            }
                        }
                    }
                }
                //}
                //if (dtGrid_o.IsLoaded)
                //{
                for (int i = 0; i < table_var_o.Rows.Count; i++)
                {
                    for (int j = 0; j < table_var_o.Rows[i].ItemArray.Count(); j++)
                    {
                        if (!DBNull.Value.Equals(table_var_o.Rows[i][j]))
                        {
                            var name = Convert.ToString(table_var_o.Rows[i][j]);
                            if (name.Length > 10)
                            {
                                if (name.Substring(name.Length - 11) == "description")
                                {
                                    var value = Convert.ToString(tag_manager.Current.getTagValue(name));
                                    table_UI_o.Rows[i][j] = value;
                                }
                                else
                                {
                                    if (name.Substring(0, 3) == "out")
                                    {
                                        var value = Convert.ToBoolean(tag_manager.Current.getTagValue(name));
                                        table_UI_o.Rows[i][j] = value;
                                    }
                                }
                               
                            }
                        }
                    }
                }
                //}

            }
            catch (Exception)
            {

            }


        }

        private void Grid_i_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < table_var_i.Rows.Count; i++)
            {
                for (int j = 0; j < table_var_i.Rows[i].ItemArray.Count(); j++)
                {
                    if (!DBNull.Value.Equals(table_var_i.Rows[i][j]))
                    {
                        var name = Convert.ToString(table_var_i.Rows[i][j]);
                        if (name.Length > 10)
                        {
                            if (name.Substring(name.Length - 11) == "description")
                            {
                                table_UI_i.Rows[i][j] = "";
                            }
                            else
                            {
                                if (name.Substring(0, 2) == "in")
                                {
                                    table_UI_i.Rows[i][j] = false;
                                }
                            }
                            
                        }
                    }
                }
            }

            dtGrid_i.ItemsSource = table_UI_i.DefaultView;
        }


        private void Grid_o_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < table_var_o.Rows.Count; i++)
            {
                for (int j = 0; j < table_var_o.Rows[i].ItemArray.Count(); j++)
                {
                    if (!DBNull.Value.Equals(table_var_o.Rows[i][j]))
                    {
                        var name = Convert.ToString(table_var_o.Rows[i][j]);
                        if (name.Length > 10)
                        {
                            if (name.Substring(name.Length - 11) == "description")
                            {
                                table_UI_o.Rows[i][j] = "";
                            }
                            else
                            {
                                if (name.Substring(0, 3) == "out")
                                {
                                    table_UI_o.Rows[i][j] = false;
                                }
                            }
                           
                        }
                    }
                }
            }
            dtGrid_o.ItemsSource = table_UI_o.DefaultView;
        }
    }
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    return Brushes.Green;
                }
                else
                {
                    return null;
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
