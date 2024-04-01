using common;
using Newtonsoft.Json;
using System;
using System.Windows;

namespace R2R.Chart
{
    /// <summary>
    /// chartGroups.xaml 的交互逻辑
    /// </summary>
    public partial class chartGroups : Window
    {
        public chartGroups()
        {
            InitializeComponent();
            tabControl.SelectedIndex = 0;
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            tabitem1.Content = new groupList(Chart.collection_real);
            tabitem2.Content = new groupList(Chart.collection_his);
            this.DataContext = this;
        }
        private void click_save(object sender, RoutedEventArgs e)
        {
            try
            {
                var js1 = JsonConvert.SerializeObject(Chart.collection_real);
                jsonFile.WriteJsonFile(@"chartGroup_real.json", js1);
                var js2 = JsonConvert.SerializeObject(Chart.collection_his);
                jsonFile.WriteJsonFile(@"chartGroup_his.json", js2);
                MessageBox.Show("保存曲线组成功");
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败，请检查设置参数");
            }

        }
    }
}
