using System.Windows;
using System.Windows.Controls;

namespace R2R
{
    /// <summary>
    /// Page06_dianji.xaml 的交互逻辑
    /// </summary>
    public partial class Page06_dianji : Page
    {
        public Page06_dianji()
        {
            InitializeComponent();
            combo1.SelectionChanged += Combo1_SelectionChanged;
        }
        private void Combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("123"); ;
        }

        private void schanged(object sender, SelectionChangedEventArgs e)
        {
            var item = TabControl1.SelectedItem as TabItem;
            var header = item.Header; //主要是在后端获取到当前的TabItem的Heade 
            if (header.ToString() == "TabItem1")
            {

            }
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            bt1.Background = System.Windows.Media.Brushes.Blue;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
