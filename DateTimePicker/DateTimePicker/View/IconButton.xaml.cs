using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DateTimePicker
{

    [System.ComponentModel.DesignTimeVisible(false)]//在工具箱中 隐藏该窗体
    public partial class IconButton : UserControl
    {
        public IconButton()
        {
            InitializeComponent();

            this.button.Click += delegate
            {
                RoutedEventArgs newEvent = new RoutedEventArgs(IconButton.ClickEvent, this);
                this.RaiseEvent(newEvent);
            };
        }

        #region 图标
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon",
            typeof(string),
            typeof(IconButton),
            new PropertyMetadata(string.Empty, OnIconChanged));
        public string Icon
        {
            set { SetValue(IconProperty, value); }
            get { return (string)GetValue(IconProperty); }
        }
        private static void OnIconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            IconButton btn = obj as IconButton;
            if (btn == null)
            {
                return;
            }
            btn.icon.Source = new BitmapImage(new Uri((string)args.NewValue, UriKind.Relative));
        }
        #endregion

        #region 命令

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
            typeof(ICommand),
            typeof(IconButton),
            new PropertyMetadata(null, OnSelectCommandChanged));
        public ICommand Command
        {
            set { SetValue(CommandProperty, value); }
            get { return (ICommand)GetValue(CommandProperty); }
        }
        private static void OnSelectCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            IconButton btn = obj as IconButton;
            if (btn == null)
            {
                return;
            }
            btn.button.Command = (ICommand)args.NewValue;
        }

        #endregion

        #region 点击事件
        public static readonly RoutedEvent ClickEvent =
          EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
          typeof(RoutedEventHandler), typeof(IconButton));

        public event RoutedEventHandler Click
        {
            add
            {
                base.AddHandler(ClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(ClickEvent, value);
            }

        }
        #endregion
        
    }
}
