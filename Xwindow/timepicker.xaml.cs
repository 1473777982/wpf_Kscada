using System;
using System.Windows;

namespace R2R.Xwindow
{
    /// <summary>
    /// timepicker.xaml 的交互逻辑
    /// </summary>
    public partial class timepicker : Window
    {
        public delegate void add_chart_line(DateTime dateTime1, DateTime dateTime2);
        public event add_chart_line add_line;
        public timepicker()
        {
            InitializeComponent();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            var dateTime1 = DateTimePicker1.SelectedDateTime.ToUniversalTime();
            var dateTime2 = DateTimePicker2.SelectedDateTime.ToUniversalTime();
            add_line?.Invoke(dateTime1, dateTime2);
            Close();
        }
    }
}
