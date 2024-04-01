using common;
using common.tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace controls
{
    /// <summary>
    /// tagSignal.xaml 的交互逻辑
    /// </summary>
    public partial class tagSignal : UserControl
    {
        #region 属性
        public bool Blink  //闪烁
        {
            get { return (bool)GetValue(_Blink); }
            set { SetValue(_Blink, value); }
        }
        public static readonly DependencyProperty _Blink =
           DependencyProperty.Register("Blink", typeof(bool), typeof(tagSignal), new PropertyMetadata(false));
        public string varName
        {
            get { return (string)GetValue(_varName); }
            set { SetValue(_varName, value); }
        }
        public static readonly DependencyProperty _varName =
           DependencyProperty.Register("varName", typeof(string), typeof(tagSignal), new UIPropertyMetadata("TagName", OnChannelNameChanged));
        private static void OnChannelNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as tagSignal)?.OnChannelNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void OnChannelNameChanged(string oldValue, string newValue)
        {

            if (base.IsLoaded)
            {
                tagSig.ToolTip = this.varName;

                TextBlockRecording.Text = Text;
                if (ch != null)
                {
                    ch.ValueChanged -= Channel_ValueChanged;
                    ch = null;
                }

                var cc = communicationTag.Current.Get_runTag(newValue);
                ch = cc;
                if (ch != null)
                {
                    try
                    {
                        ClockState state = tbStoryboard.GetCurrentState();
                        if (Convert.ToBoolean(ch.value))
                        {
                            if (Blink)
                            {
                                if (state == ClockState.Stopped)
                                {
                                    tbStoryboard.Begin();
                                }
                            }
                            else
                            {
                                tbStoryboard.Stop();
                                tagBack.Background = new SolidColorBrush(sigColor1);
                                
                            }
                        }
                        else
                        {
                            tbStoryboard.Stop();
                            tagBack.Background = this.Background;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    this.ch.ValueChanged += Channel_ValueChanged;
                }
                else
                {
                    //base.Content = varName;
                    TextBlockRecording.Text = newValue;
                }
            }
        }

        //public SolidColorBrush sigColor
        //{
        //    get { return (SolidColorBrush)GetValue(_Color); }
        //    set { SetValue(_Color, value); }
        //}
        //public static readonly DependencyProperty _Color =
        //   DependencyProperty.Register("sigColor", typeof(SolidColorBrush), typeof(tagSignal), new PropertyMetadata(Brushes.Gray));

        public Color sigColor1
        {
            get { return (Color)GetValue(_Color1); }
            set { SetValue(_Color1, value); }
        }
        public static readonly DependencyProperty _Color1 =
           DependencyProperty.Register("sigColor1", typeof(Color), typeof(tagSignal), new PropertyMetadata(Colors.LightGreen));

        public string Text
        {
            get { return (string)GetValue(_Text); }
            set { SetValue(_Text, value); }
        }
        public static readonly DependencyProperty _Text =
           DependencyProperty.Register("Text", typeof(string), typeof(tagSignal), new PropertyMetadata("文本"));

        #endregion

        // thread timer   
        // 动画
        private Storyboard tbStoryboard;
        IrunTag ch;

        public tagSignal()
        {
            InitializeComponent();
        }

        private void tagSig_Loaded(object sender, RoutedEventArgs e)
        {
            tagSig.ToolTip = this.varName;

            var keyFrames = new ColorAnimationUsingKeyFrames();
            keyFrames.Duration = TimeSpan.FromSeconds(2);
            keyFrames.KeyFrames.Add(new DiscreteColorKeyFrame(
                    sigColor1, // Target value (KeyValue)
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0))) // KeyTime
                    );
            keyFrames.KeyFrames.Add(new DiscreteColorKeyFrame(
                ((SolidColorBrush)this.Background).Color,
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.0))) // KeyTime
                    );//(Color)ColorConverter.ConvertFromString("#FF80FFFF"), // Target value (KeyValue)
            keyFrames.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(keyFrames, tagSig);
            Storyboard.SetTargetProperty(
                keyFrames, new PropertyPath("(UserControl.Background).(SolidColorBrush.Color)"));
            //Storyboard.SetTargetProperty(
            //    keyFrames, new PropertyPath(tagBack.Background));
            tbStoryboard = new Storyboard();
            tbStoryboard.Children.Add(keyFrames);
           

            if (communicationTag.Dic_taginfos.ContainsKey(varName))
            {
                
                var cc = communicationTag.Current.Get_runTag(this.varName);
                this.ch = cc;
                if (this.ch != null)
                {
                    TextBlockRecording.Text = Text;
                    tbStoryboard.Begin();
                    //ClockState state = tbStoryboard.GetCurrentState();
                    //SetText();
                    try
                    {
                        if (Convert.ToBoolean(ch.value))
                        {
                            if (Blink)
                            {
                                tbStoryboard.Begin();
                            }
                            else
                            {
                                tbStoryboard.Stop();
                                tagBack.Background = new SolidColorBrush(sigColor1);
                                
                            }
                        }
                        else
                        {
                            tbStoryboard.Stop();
                        }
                    }
                    catch (Exception)
                    {
                    }

                    this.ch.ValueChanged += Channel_ValueChanged;
                }
                else
                {
                    //base.Content = varName;
                    TextBlockRecording.Text = this.varName;
                }
            }

        }

        ~tagSignal()
        {
            if (ch != null)
            {
                ch.ValueChanged -= Channel_ValueChanged;
                ch = null;
            }
            tbStoryboard.Stop();
            //dispatcherTimer_tagsig.Stop();  

        }

        private void tagSig_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ch != null)
            {
                ch.ValueChanged -= Channel_ValueChanged;
                ch = null;
            }
            tbStoryboard.Stop();
            //dispatcherTimer_tagsig.Stop();         
        }
        //ch 值发生改变时执行
        private void Channel_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool blink = false;
                base.Dispatcher.Invoke(new Action(() => { blink = Blink; })); //线程冲突，采用action避免
                ClockState state = tbStoryboard.GetCurrentState();
                //base.Dispatcher.Invoke(new Action(() => { state = tbStoryboard.GetCurrentState(); }));
                if (ch == null)
                {
                    (sender as IrunTag).ValueChanged -= Channel_ValueChanged;
                    return;
                }
                if (Convert.ToBoolean(ch.value))
                {

                    if (blink)
                    {

                        if (state != ClockState.Active)
                        {
                            Action method = () => { tbStoryboard.Begin(); };
                            base.Dispatcher.Invoke(method, null);
                        }
                    }
                    else
                    {
                        Action method = () => { tbStoryboard.Stop(); };
                        base.Dispatcher.Invoke(method, null);
                        base.Dispatcher.Invoke(new Action(() => { tagBack.Background = new SolidColorBrush(sigColor1); }));
                    }
                }
                else
                {

                    Action method = () => { tbStoryboard.Stop(); };
                    base.Dispatcher.Invoke(method, null);
                    base.Dispatcher.Invoke(new Action(() => { tagBack.Background = this.Background; }));


                }
            }
            catch (Exception)
            {

            }

        }
    }
}
