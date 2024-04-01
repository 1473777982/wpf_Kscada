using R2R.helper;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace R2R.OperationRecord
{
    /// <summary>
    /// operationHis.xaml 的交互逻辑
    /// </summary>
    public partial class operationHis : Window
    {
        string tableName;
        string name_timecolumn;
        string sql_timeRange;
        DataTable dt { get; set; } //查询到的结果
        DateTime start = new DateTime();
        DateTime end = new DateTime();
        public operationHis()
        {
            InitializeComponent();
            tableName = "operation";
            name_timecolumn = "time";
            sql_timeRange = "SELECT * FROM " + tableName + " WHERE " + name_timecolumn + " >= @StartDate AND " + name_timecolumn + " <= @EndDate";
            dt = new DataTable();
            dataGrid_OperationRecord.ItemsSource = dt.DefaultView;
        }

        private void query_Click(object sender, RoutedEventArgs e)
        {
            start = Convert.ToDateTime(Tpicker1.DateTimeStr);
            end = Convert.ToDateTime(Tpicker2.DateTimeStr);
            dt = sqlClientHelper.getresault(start, end, sql_timeRange);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comb = (ComboBox)sender;
            end = DateTime.Now;
            switch (comb.SelectedIndex)
            {
                case 0:
                    start = end.AddMinutes(-15);
                    break;
                case 1:
                    start = end.AddHours(-1);
                    break;
                case 2:
                    start = end.AddDays(-1);
                    break;
            }
            dt = sqlClientHelper.getresault(start, end, sql_timeRange);
        }

    }
}
