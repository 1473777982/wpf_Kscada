using System;
using System.Windows;
using System.Windows.Controls;

namespace R2R.protocols
{
    /// <summary>
    /// search.xaml 的交互逻辑
    /// </summary>
    public partial class search : Window
    {
        public Action<string, bool> Action { get; set; }
        public search()
        {
            InitializeComponent();
        }

        #region  搜索_数据源为datatable 同一个界面
        //int id = 0;
        //string sort_temp;
        //DataRow[] foundRows = null;
        ////搜索按钮——刷新datagrid
        //public void search_Click(object sender, RoutedEventArgs e)
        //{
        //    search_next(dataGrid1, App.table_1_value, sender);
        //}
        ////判断上一个、下一个并跳转
        //private void search_next(DataGrid dataGrid, DataTable table, object sender)
        //{
        //    if (string.IsNullOrWhiteSpace(textBox_search.Text))
        //    {
        //        return;
        //    }
        //    //CollectionViewSource.GetDefaultView(dataGrid.ItemsSource).Refresh();           
        //    try
        //    {
        //        if (sort_temp == null || textBox_search.Text != sort_temp)
        //        {
        //            foundRows = null;
        //            // Use the Select method to find all rows matching the filter.
        //            foundRows = table.Select("name LIKE '%" + textBox_search.Text + "%'");//"Name LIKE '%jo%'"
        //            if (foundRows.Length == 0)
        //            {
        //                MessageBoxX.Show("查找失败", "提示");
        //                return;
        //            }
        //            id = 0;
        //        }
        //        if (foundRows != null)
        //        {
        //            if ((sender as Button).Name == "button_search_next")
        //            {
        //                var index = table.Rows.IndexOf(foundRows[id]);
        //                if (index <= dataGrid.Items.Count - 1)
        //                {
        //                    dataGrid.SelectedIndex = index;
        //                    //dataGrid.UpdateLayout();
        //                    dataGrid.ScrollIntoView(dataGrid.SelectedItem);
        //                    id++;
        //                    if (id == foundRows.Length)
        //                    {
        //                        id = 0;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                var index = table.Rows.IndexOf(foundRows[foundRows.Length - id]);
        //                if (index <= dataGrid.Items.Count - 1)
        //                {
        //                    dataGrid.SelectedIndex = index;
        //                    //dataGrid.UpdateLayout();
        //                    dataGrid.ScrollIntoView(dataGrid.SelectedItem);
        //                    id++;
        //                    if (id == foundRows.Length)
        //                    {
        //                        id = 0;
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    sort_temp = textBox_search.Text;
        //}

        //private void search_next1(DataGrid dataGrid, DataTable table, object sender)
        //{
        //    if (string.IsNullOrWhiteSpace(textBox_search.Text))
        //    {
        //        return;
        //    }
        //    //CollectionViewSource.GetDefaultView(dataGrid.ItemsSource).Refresh();           
        //    try
        //    {
        //        if (sort_temp == null || textBox_search.Text != sort_temp)
        //        {
        //            foundRows = null;
        //            // Use the Select method to find all rows matching the filter.
        //            foundRows = table.Select("name LIKE '%" + textBox_search.Text + "%'");//"Name LIKE '%jo%'"
        //            if (foundRows.Length == 0)
        //            {
        //                MessageBoxX.Show("查找失败", "提示");
        //                return;
        //            }
        //            id = 0;
        //        }
        //        if (foundRows != null)
        //        {
        //            if ((sender as Button).Name == "button_search_next")
        //            {
        //                var index = table.Rows.IndexOf(foundRows[id]);
        //                if (index <= dataGrid.Items.Count - 1)
        //                {
        //                    dataGrid.SelectedIndex = index;
        //                    //dataGrid.UpdateLayout();
        //                    dataGrid.ScrollIntoView(dataGrid.SelectedItem);
        //                    id++;
        //                    if (id == foundRows.Length)
        //                    {
        //                        id = 0;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                var index = table.Rows.IndexOf(foundRows[foundRows.Length - id]);
        //                if (index <= dataGrid.Items.Count - 1)
        //                {
        //                    dataGrid.SelectedIndex = index;
        //                    //dataGrid.UpdateLayout();
        //                    dataGrid.ScrollIntoView(dataGrid.SelectedItem);
        //                    id++;
        //                    if (id == foundRows.Length)
        //                    {
        //                        id = 0;
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    sort_temp = textBox_search.Text;
        //}

        #endregion

        #region  搜索_数据源为list<>   搜索框在子界面
        public void search_Click(object sender, RoutedEventArgs e)
        {
            bool buttonClicked;
            if (string.IsNullOrWhiteSpace(textBox_search.Text))
            {
                return;
            }
            if ((sender as Button).Name == "button_search_next")
            {
                buttonClicked = false;//下一个
            }
            else
            {
                buttonClicked = true;//上一个
            }
            Action?.Invoke(textBox_search.Text, buttonClicked);
        }
        #endregion
    }
}
