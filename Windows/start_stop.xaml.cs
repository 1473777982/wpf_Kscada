using System.Windows;

namespace R2R.Windows
{
    /// <summary>
    /// start_stop.xaml 的交互逻辑
    /// </summary>
    public partial class start_stop : Window
    {
        public new bool? DialogResult { get; set; }
        string name;
        public start_stop()
        {
            InitializeComponent();
            DialogResult = null;
        }
        public start_stop(string name)
        {
            InitializeComponent();
            this.name = name;
            Title = name;
            DialogResult = null;
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; Close();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= true; Close();
        }
    }
}
