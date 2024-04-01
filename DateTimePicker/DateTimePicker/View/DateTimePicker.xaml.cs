using System;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DateTimePicker
{

    [ToolboxBitmap(typeof(DateTimePicker_view), "DateTimePicker.bmp")]  
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>    
    public partial class DateTimePicker_view : UserControl
    {
        public DateTimePicker_view()
        {
            InitializeComponent();
        }


        public ICommand TimeChangeCommand
        {
            get { return (ICommand)GetValue(TimeChangeCommandProperty); }
            set { SetValue(TimeChangeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeChangeCommandProperty =
            DependencyProperty.Register("TimeChangeCommand", typeof(ICommand), typeof(DateTimePicker_view), new PropertyMetadata(null));

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public DateTimePicker_view(string txt)
            : this()
        {
           // this.textBox1.Text = txt;
        
        }

        #region 事件

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, RoutedEventArgs e)
        {    
            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }

            TDateTimeView dtView = new TDateTimeView(DateTimeStr);// TDateTimeView  构造函数传入日期时间
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {

                textBlock1.Text = dateTimeStr;
                DateTimeStr = dateTimeStr;
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭

            };

            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        }
        /// <summary>
        /// The delete event
        /// </summary>
        public static readonly RoutedEvent TimeTextChangedEvent =
             EventManager.RegisterRoutedEvent("TimeTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DateTimePicker_view));

        /// <summary>
        /// 时间改变的操作.
        /// </summary>
        public event RoutedEventHandler TimeTextChanged
        {
            add
            {
                AddHandler(TimeTextChangedEvent, value);
            }

            remove
            {
                RemoveHandler(TimeTextChangedEvent, value);
            }
        }
        /// <summary>
        /// DateTimePicker 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //DateTime dt = DateTime.Now;
            //textBlock1.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");//"yyyyMMddHHmmss"
            //DateTimeStr = dt;            
          //  DateTime = Convert.ToDateTime(textBlock1.Text);
        }
        public void TimeTextChanged_Click(object sender, TextChangedEventArgs e)
        {
            ButtonAutomationPeer bam = new ButtonAutomationPeer(TimeChangeBtn);
            IInvokeProvider iip = bam.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            iip.Invoke();
        }
        #endregion

        #region 属性

        /// <summary>
        /// 日期时间
        /// </summary>
        public string DateTimeStr
        {
            get { return (string)GetValue(DateTimeProperty); }
            set
            {
                SetValue(DateTimeProperty, value);
            }
        }
        // Using a DependencyProperty as the backing store for DateTimeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTimeStr", typeof(string), typeof(DateTimePicker_view));
        #endregion
    }
}
