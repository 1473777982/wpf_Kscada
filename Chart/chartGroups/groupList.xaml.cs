using DynamicDataDisplay.Common.Auxiliary;
using R2R.UControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace R2R.Chart
{

    /// <summary>
    /// groupList.xaml 的交互逻辑
    /// </summary>
    public partial class groupList : Page
    {
        ObservableCollection<groupItem> GroupItems { set; get; }
        public groupList(ObservableCollection<groupItem> groups)
        {
            InitializeComponent();
            GroupItems = groups;
            listView_groupName.ItemsSource = GroupItems;
        }

        private void AddCommand_OnExecute(object parameter)
        {
            GroupItems.Add(new groupItem { groupName = "newgroup", lineNames = new ObservableCollection<string>() });
        }

        private void EditCommand_OnExecute(object parameter)
        {
            try
            {
                groupItem group = listView_groupName.SelectedItem as groupItem;
                var gpEdit = new group_Edit(group);
                gpEdit.ShowDialog();
            }
            catch (Exception)
            {

            }

        }
        private void DeleteCommand_OnExecute(object parameter)
        {
            try
            {
                groupItem group = listView_groupName.SelectedItem as groupItem;
                GroupItems.Remove(group);
                listbox.ItemsSource = null;
            }
            catch (Exception)
            {
                MessageBox.Show("删除失败!");
            }

        }

        private void listView_key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            MenuItem Mitem1 = contMenu.Items[1] as MenuItem;//contMenu
            MenuItem Mitem2 = contMenu.Items[2] as MenuItem;
            var item = listView.SelectedItem as groupItem;
            if (item != null)
            {
                Mitem1.Visibility = System.Windows.Visibility.Visible;
                Mitem2.Visibility = System.Windows.Visibility.Visible;
                listbox.ItemsSource = item.lineNames;
            }
            else
            {
                Mitem1.Visibility = System.Windows.Visibility.Collapsed;
                Mitem2.Visibility = System.Windows.Visibility.Collapsed;
            }
           
        }

        private void listView_groupName_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null)
            {
                HitTestResult hitTestResult = VisualTreeHelper.HitTest(listView, e.GetPosition(listView));
                if (hitTestResult != null && hitTestResult.VisualHit is ListViewItem)
                {
                    // 右键点击了ListView的项
                    ListViewItem listViewItem = hitTestResult.VisualHit as ListViewItem;
                    listViewItem.IsSelected = true;
                }
                else
                {
                    // 右键点击了空白处
                    listView.SelectedItems.Clear();
                }
            }
        }

    }

    public class groupItem : helper.NotifyChanged
    {
        private string _groupName;
        private bool _vertical_log;
        private ObservableCollection<string> _lineNames;

        public string groupName { get => _groupName; set => SetProperty(ref _groupName, value); }
        public bool vertical_log { get => _vertical_log; set => SetProperty(ref _vertical_log, value); }
        public ObservableCollection<string> lineNames { get => _lineNames; set => SetProperty(ref _lineNames, value); }
    }

    public class DelegateCommand : ICommand
    {
        public Func<object, bool> CanExecuteDelegate { set; get; }

        public Action<object> ExecuteDelegate { set; get; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }

       public event EventHandler CanExecuteChanged;

    }
}
