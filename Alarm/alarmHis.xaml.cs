using R2R.helper;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace R2R.Alarm
{
    /// <summary>
    /// alarmHis.xaml 的交互逻辑
    /// </summary>
    public partial class alarmHis : Window
    {
        string tableName;
        string name_timecolumn;
        string sql_timeRange;
        DataTable dt { get; set; } //查询到的结果
        DateTime start = new DateTime();
        DateTime end = new DateTime();
        public alarmHis()
        {
            InitializeComponent();
            tableName = "alarm";
            name_timecolumn = "time";
            sql_timeRange = "SELECT * FROM " + tableName + " WHERE " + name_timecolumn + " >= @StartDate AND " + name_timecolumn + " <= @EndDate";
            dt = new DataTable();
            dataGrid_alarmhis.ItemsSource = dt.DefaultView;
        }

        //DateTime start = new DateTime(2023, 7, 1, 10, 0, 0); // 开始时间，例：2023年7月1日10点整
        //DateTime end = new DateTime(2023, 7, 1, 10, 30, 0); // 结束时间，例：2023年7月1日10点30分整

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
