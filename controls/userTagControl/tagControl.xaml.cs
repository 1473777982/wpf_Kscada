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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace controls
{
    /// <summary>
    /// tagControl.xaml 的交互逻辑
    /// </summary>
    public partial class tagControl : UserControl
    {
        #region  属性
        public bool setAble
        {
            get { return (bool)GetValue(_setAble); }
            set { SetValue(_setAble, value); }
        }
        public static readonly DependencyProperty _setAble =
           DependencyProperty.Register("setAble", typeof(bool), typeof(tagControl), new PropertyMetadata(true));
        public string varName
        {
            get { return (string)GetValue(_varName); }
            set { SetValue(_varName, value); }
        }
        public static readonly DependencyProperty _varName =
           DependencyProperty.Register("varName", typeof(string), typeof(tagControl), new UIPropertyMetadata("TagName", OnChannelNameChanged));
        private static void OnChannelNameChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            (o as tagControl)?.OnChannelNameChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void OnChannelNameChanged(string oldValue, string newValue)
        {
           
            if (base.IsLoaded)
            {
                Control_tag.ToolTip = this.varName;
                TbCustom.Text = this.varName;

                if (ch != null)
                {
                    ch.ValueChanged -= Channel_ValueChanged;
                    ch = null;
                }

                if (communicationTag.Dic_taginfos.ContainsKey(newValue))
                {
                    ch = communicationTag.Current.Get_runTag(newValue);
                    if (this.ch != null)
                    {
                        if (this.ch.flagState)
                        {
                            try
                            {
                                if (!object.Equals(ch.value, null))
                                {
                                    TbCustom.Text = this.ch.value.ToString();
                                }
                            }
                            catch (Exception)
                            {
                            }
                            this.ch.ValueChanged += Channel_ValueChanged;
                        }
                        else 
                        {
                            TbCustom.Text = "";
                            TbCustom.Background = Brushes.Red;
                        }
                           
                    }
                }
                   
            }
        }
        public string Format
        {
            get { return (string)GetValue(_Format); }
            set { SetValue(_Format, value); }
        }
        public static readonly DependencyProperty _Format =
           DependencyProperty.Register("Format", typeof(string), typeof(tagControl), new PropertyMetadata("{0}"));
        public string UserLevel
        {
            get { return (string)GetValue(_UserLevel); }
            set { SetValue(_UserLevel, value); }
        }
        public static readonly DependencyProperty _UserLevel =
           DependencyProperty.Register("UserLevel", typeof(string), typeof(tagControl), new PropertyMetadata("0"));
        #endregion

        // timer 刷新状态
        IrunTag ch;
        //DispatcherTimer dispatcherTimer_tagControl;
        public tagControl()
        {
            InitializeComponent();
            //if (setAble == true)
            //{
            //    TbCustom.IsReadOnly = false;
            //}
            //else
            //{
            //    TbCustom.IsReadOnly = true;
            //}
        }
        private void tagContro_Loaded(object sender, RoutedEventArgs e)
        {
            Control_tag.ToolTip = this.varName;
            TbCustom.Text = this.varName;
            if (communicationTag.Dic_taginfos.ContainsKey(varName))
            {
                //此处拿到了从json文件加载的channel，
                // 如何同主程序channel对应未知:主程序开线程channel同步json channel
                //  fscada用同一个channel，无需同步
                this.ch = communicationTag.Current.Get_runTag(this.varName);
                if (this.ch != null)
                {
                    if (this.ch.flagState)
                    {
                        try
                        {
                            if (!object.Equals(ch.value, null))
                            {
                                TbCustom.Text = this.ch.value.ToString();
                            }
                        }
                        catch (Exception)
                        {
                        }
                        this.ch.ValueChanged += Channel_ValueChanged;
                    }
                    else
                    {
                        TbCustom.Text = "";
                        TbCustom.Background = Brushes.Red;
                    }
                }
            }
        }
        private void Channel_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ch == null)
                {
                    (sender as IrunTag).ValueChanged -= Channel_ValueChanged;
                    return;
                }
                if (!this.ch.flagState)
                {
                   
                    Action method = () => { TbCustom.Background = Brushes.Red; };
                    base.Dispatcher.Invoke(method, null);
                }
                else
                {
                    if (!object.Equals(ch.value, null))
                    {
                       
                        Action method = () => {
                            if (ch == null)
                            {
                                return;
                            }
                            else
                            {
                                try
                                {
                                    TbCustom.Text = String.Format(Format, ch.value);
                                    TbCustom.Background = this.Background;
                                }
                                catch (Exception)
                                {
                                }
                                
                            }
                             };
                        base.Dispatcher.Invoke(method, null);
                    }
                    else
                    {
                        Action method = () => { TbCustom.Text = this.varName; };
                        base.Dispatcher.Invoke(method, null);
                    }
                }
               
            }
            catch (Exception)
            {

            }

        }
        ~tagControl()
        {
            if (ch != null)
            {
                ch.ValueChanged -= Channel_ValueChanged;
                ch = null;
            }
            // dispatcherTimer_tagControl.Stop();
        }
        private void tagContro_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ch != null)
            {
                try
                {
                    ch.ValueChanged -= Channel_ValueChanged;
                }
                catch (Exception)
                {
                }
                ch = null;
            }
            // dispatcherTimer_tagControl.Stop();
        }
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ch != null)
            {
                if (ch.flagState)
                {
                    try
                    {
                        if (UserData.permission < Int16.Parse(UserLevel))
                        {
                            //TbCustom.IsReadOnly = true;
                            var result = MessageBox.Show("用户权限不足", "Infomation");
                        }
                        else
                        {
                            //TbCustom.IsReadOnly = false;

                            if (setAble)
                            {
                                if (communicationTag.Current.Dic_ranTags.ContainsKey(varName))
                                {
                                    SetValueDialog dlg = new SetValueDialog(varName);
                                    dlg.ShowDialog();
                                }
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        //private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        if (varName != null)
        //        {
        //            if (App.DIC_vars.ContainsKey(varName))
        //            {
        //                //Mainwindow.tcadsClient.WriteSymbol(string symbolPath, object value, bool reloadInfo);//根据变量名称写入PLC
        //                //write by handle
        //                //the second parameter is the object to be written to the PLC variable 
        //                try
        //                {
        //                    switch (App.DIC_vars[varName]["type"])//根据变量类型执行相应写入操作
        //                    {
        //                        case "BOOL":
        //                            if (CheckBoolean(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write("BOOL");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "BYTE":
        //                            if (CheckByte(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write("BYTE");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "UINT":
        //                            if (CheckInt16(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write_log_limit("UINT");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "INT":
        //                            if (CheckInt32(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write_log_limit("INT");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "DINT":
        //                            if (CheckInt32(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write_log_limit("DINT");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "WORD":
        //                            if (CheckInt16(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write_log_limit("WORD");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "REAL":
        //                            if (CheckSingle(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write_log_limit("REAL");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "LREAL":
        //                            if (CheckDouble(TbCustom.Text))
        //                            {
        //                                try
        //                                {
        //                                    ADS_write_log_limit("LREAL");
        //                                }
        //                                catch (Exception)
        //                                {
        //                                    MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                                }
        //                            }
        //                            else
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            break;
        //                        case "STRING":
        //                            try
        //                            {
        //                                //with strings one has to additionally pass the number of characters
        //                                //the variable has in the PLC(default 80).
        //                                ADS_write("STRING");
        //                            }
        //                            catch (Exception)
        //                            {
        //                                MessageBoxX.Show("输入格式不正确", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //                            }
        //                            break;
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    try
        //                    {
        //                        TbCustom.Text = varName;
        //                        Keyboard.ClearFocus();
        //                    }
        //                    catch (Exception)
        //                    {
        //                        Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
        //                    }
        //                }
        //            }
        //        }
        //        Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
        //    }
        //}
        //private void ADS_write_log_limit(string tp)
        //{
        //    try
        //    {
        //        var limitH = (string)App.DIC_vars[varName]["limithigh"];
        //        var limitL = (string)App.DIC_vars[varName]["limitlow"];
        //        var setvalue = float.Parse(TbCustom.Text);
        //        if (!CheckDouble(limitH))
        //        {
        //            limitH = "";
        //        }
        //        if (!CheckDouble(limitL))
        //        {
        //            limitL = "";
        //        }
        //        if (limitL == "")
        //        {
        //            if (limitH == "")
        //            {
        //                ADS_write(tp);
        //            }
        //            else
        //            {
        //                if (setvalue <= float.Parse(limitH) )
        //                {
        //                    ADS_write(tp);
        //                }
        //                else
        //                    MessageBoxX.Show("输入超限", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }

        //        }
        //        else
        //        {
        //            if (limitH == "")
        //            {
        //                if ( setvalue >= float.Parse(limitL))
        //                {
        //                    ADS_write(tp);
        //                }
        //                else
        //                    MessageBoxX.Show("输入超限", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }
        //            else
        //            {
        //                if (setvalue <= float.Parse(limitH) && setvalue >= float.Parse(limitL))
        //                {
        //                    ADS_write(tp);
        //                }
        //                else
        //                    MessageBoxX.Show("输入超限", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("写入失败", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}
        //private void ADS_write(string tp)
        //{
        //    var handle = App.tcadsClient.CreateVariableHandle((string)App.DIC_vars[varName]["address"]);
        //    switch (tp)
        //    {
        //        case "BOOL":
        //            App.tcadsClient.WriteAny(handle, Boolean.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "BYTE":
        //            App.tcadsClient.WriteAny(handle, Byte.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "STRING":
        //            App.tcadsClient.WriteAny(handle, TbCustom.Text, new int[] { TbCustom.Text.Length });
        //            write_log();
        //            break;
        //        case "UINT":
        //            App.tcadsClient.WriteAny(handle, Int16.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "DINT":
        //            App.tcadsClient.WriteAny(handle, Int32.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "INT":
        //            App.tcadsClient.WriteAny(handle, short.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "WORD":
        //            App.tcadsClient.WriteAny(handle, Int16.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "REAL":
        //            App.tcadsClient.WriteAny(handle, Single.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //        case "LREAL":
        //            App.tcadsClient.WriteAny(handle, Double.Parse(TbCustom.Text));
        //            write_log();
        //            break;
        //    }
        //}
        //private void write_log()
        //{
        //    Keyboard.ClearFocus();
        //    Mwin.addLog("变量：" + varName + "写入：" + TbCustom.Text);
        //    //Mainwindow.addoperationTOsql("变量：" + varName + "写入：" + TbCustom.Text);
        //}
        //private bool CheckDouble(string str)
        //{
        //    try
        //    {
        //        double temp;
        //        temp = double.Parse(str);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private bool CheckSingle(string str)
        //{
        //    try
        //    {
        //        Single temp;
        //        temp = Single.Parse(str);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private bool CheckInt32(string str)
        //{
        //    try
        //    {
        //        Int32 temp;
        //        temp = Int32.Parse(str);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private bool CheckInt16(string str)
        //{
        //    try
        //    {
        //        Int16 temp;
        //        temp = Int16.Parse(str);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private bool CheckBoolean(string str)
        //{
        //    try
        //    {
        //        Boolean temp;
        //        temp = Boolean.Parse(str);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private bool CheckByte(string str)
        //{
        //    try
        //    {
        //        Byte temp;
        //        temp = Byte.Parse(str);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        private void TbCustom_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
