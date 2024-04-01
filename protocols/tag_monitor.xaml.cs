using common;
using common.tag;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace R2R.protocols
{
    /// <summary>
    /// tag_monitor.xaml 的交互逻辑
    /// </summary>
    public partial class tag_monitor : Window
    {
        ObservableCollection<protocol> protocol_Tags { get; set; }
        public tag_monitor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            protocol_Tags = communicationTag.RunProtos;
            tree1.DataContext = protocol_Tags;
        }
        Device testdevice = new Device();
        private void tree1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tv = (TreeView)sender;
            var t = tv.SelectedItem;
            if (t.GetType() == typeof(Device))
            {
                testdevice = t as Device;
                varDataGrid.ItemsSource = (t as Device).runTags;
                total.Text = (t as Device).runTags.Count.ToString();
            }
        }
        #region  search
        search search = null;
        int id = 0;
        int matchindex;
        List<runTag> matchitems = null;
        private void find_Click(object sender, RoutedEventArgs e)
        {
            var source = varDataGrid.ItemsSource as List<runTag>;
            if (source.Count > 0)
            {
                if (search == null)
                {
                    search = new search();
                    search.Show();
                    search.Action = (s, button) =>
                    {
                        try
                        {
                            if (s != "")
                            {
                                // Regex方式
                                //Regex reg = new Regex(s, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                //for (int i = 0; i < source.Count; i++)
                                //{
                                //    var tf = source[i];
                                //    matchindex = i;
                                //    if (reg.IsMatch(tf.name) || reg.IsMatch(tf.address) || reg.IsMatch(tf.description))
                                //    {
                                //        varDataGrid.SelectedItems.Clear();
                                //        varDataGrid.SelectedIndex = i;
                                //        varDataGrid.ScrollIntoView(varDataGrid.SelectedItem);
                                //        break;
                                //    }
                                //}

                                matchitems = null;
                                // Use the Select method to find all rows matching the filter.
                                matchitems = source.Where(a => a.name.Contains(s) || a.address.Contains(s) || a.description.Contains(s) || a.value.ToString().Contains(s)).ToList();
                                if (matchitems.Count == 0)
                                {
                                    return;
                                }
                                id = 0;
                            }
                            if (matchitems.Count > 0)
                            {
                                if (!button)
                                {
                                    var index = source.IndexOf(matchitems[id]);
                                    if (index <= source.Count - 1)
                                    {
                                        varDataGrid.SelectedIndex = index;
                                        varDataGrid.ScrollIntoView(varDataGrid.SelectedItem);
                                        id++;
                                        if (id == matchitems.Count)
                                        {
                                            id = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var index = source.IndexOf(matchitems[matchitems.Count - id]);
                                    if (index <= varDataGrid.Items.Count - 1)
                                    {
                                        varDataGrid.SelectedIndex = index;
                                        varDataGrid.ScrollIntoView(varDataGrid.SelectedItem);
                                        id++;
                                        if (id == matchitems.Count)
                                        {
                                            id = 0;
                                        }
                                    }
                                }

                            }
                        }
                        catch (Exception)
                        {

                        }
                    };
                    search.Show();
                }

            }

        }
        #endregion

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            contextMenu.IsOpen = false;
        }
    }
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value == null || value.ToString() == "")
            //{
            //    // 返回灰色
            //    return new SolidColorBrush(Colors.Gray);
            //}
            //else
            //{
            //    // 返回蓝色
            //    return new SolidColorBrush(Colors.Blue);
            //}
            
            if ( !(bool)value)
            {
                // 返回灰色
                return new SolidColorBrush(Colors.Gray);
            }
            else
            {
                // 返回蓝色
                return new SolidColorBrush(Colors.Blue);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
