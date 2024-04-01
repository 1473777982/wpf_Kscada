using common;
using common.helper;
using common.tag;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;


namespace controls
{
    /// <summary>
    /// SetValueDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SetValueDialog : Window
    {

        string varName;
        IrunTag cc;
        public SetValueDialog(String varname)
        {
            InitializeComponent();
            varName = varname;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cc = communicationTag.Current.Get_runTag(this.varName);
            box_type.Text = cc.tagType.ToString();
            box_value_now.Text = Convert.ToString(cc.value);
            box_value_set.Focus();
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            conform();
            Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
            this.Close();
        }

        private void box_value_set_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 在这里处理Enter键按下事件
                conform();
                Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
                this.Close();
            }
            
        }

        private void conform()
        {
            if (varName != null)
            {
                var text = box_value_set.Text;
                try
                {
                    switch (cc.tagType)//根据变量类型执行相应写入操作
                    {
                        case DataType.BOOL:
                            if (checkDataType.CheckBoolean(text))
                            {
                                try
                                {
                                    if (text == "1")
                                    {
                                        cc.value = true;
                                    }
                                    else if (text == "0")
                                    {
                                        cc.value = false;
                                    }
                                    else
                                    {
                                        cc.value = Boolean.Parse(text);
                                    }
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.BYTE:
                            if (checkDataType.CheckByte(text))
                            {
                                try
                                {
                                    cc.value = Byte.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.UINT:
                            if (checkDataType.CheckUInt16(text))
                            {
                                try
                                {
                                    cc.value = UInt16.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.WORD:
                            if (checkDataType.CheckUInt16(text))
                            {
                                try
                                {
                                    cc.value = UInt16.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.INT:
                            if (checkDataType.CheckInt16(text))
                            {
                                try
                                {
                                    cc.value = Int32.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }
                            break;
                        case DataType.DINT:
                            if (checkDataType.CheckInt32(text))
                            {
                                try
                                {
                                    cc.value = Int32.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.REAL:
                            if (checkDataType.CheckSingle(text))
                            {
                                try
                                {
                                    cc.value = Single.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.LREAL:
                            if (checkDataType.CheckDouble(text))
                            {
                                try
                                {
                                    cc.value = double.Parse(text);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("写入失败", "Infomation");
                                }
                            }
                            else
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }

                            break;
                        case DataType.STRING:
                            try
                            {
                                //with strings one has to additionally pass the number of characters
                                //the variable has in the PLC(default 80).
                                cc.value = text;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("输入格式不正确", "Infomation");
                                return;
                            }
                            break;
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        box_value_set.Text = varName;
                        Keyboard.ClearFocus();
                    }
                    catch (Exception)
                    {
                        Keyboard.ClearFocus();//Keyboard.ClearFocus 方法    清除焦点
                    }
                }

            }
        }

    }
}
