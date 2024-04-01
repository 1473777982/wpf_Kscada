using System.ComponentModel;
using System.IdentityModel.Tokens;
using System.Windows;

namespace R2R.Windows
{
    /// <summary>
    /// open_close.xaml 的交互逻辑
    /// </summary>
    public partial class open_close : Window
    {
        public new bool? DialogResult { get; set; }
        string name ="";
        public open_close()
        {
            InitializeComponent();
            Title = name;
            DialogResult = null;
        }
        public open_close(string name)
        {
            InitializeComponent();
            this.name = name;
            Title = name;
            DialogResult = null;
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
           DialogResult = true; Close();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult= false; Close(); 
        }


     
    }
}
