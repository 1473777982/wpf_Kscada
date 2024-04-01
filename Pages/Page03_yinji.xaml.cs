using System.Windows.Controls;

namespace R2R
{
    /// <summary>
    /// Page03_yinji.xaml 的交互逻辑
    /// </summary>
    public partial class Page03_yinji : Page
    {
        //string PS1 = "C100", PS2 = "C101_A", PS3 = "C101_B", PS4 = "C102_A", PS5 = "C102_B",
        //       PS6 = "C103_A", PS7 = "C103_B", PS8 = "C200", PS9 = "C201_A", PS10 = "C201_B",
        //       PS11 = "C202_A", PS12 = "C202_B", PS13 = "C203_A", PS14 = "C203_B";
        //DispatcherTimer dispatcherTimer_P03 = new DispatcherTimer();

        public Page03_yinji()
        {
            InitializeComponent();
            //dispatcherTimer_P03.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //dispatcherTimer_P03.Tick += new EventHandler(TimeAction);
            //dispatcherTimer_P03.Start();



        }
        //private void TimeAction(object sender, EventArgs e)
        //{
        //    try
        //    {




        //        //if ((string)ADSDictionary_1000["PS_" + PS2 + "_state_pulseON"][5] == "true")//PS_C201_B_state_pulseON
        //        //    bt_pulse_1.Content = "ON";
        //        //else
        //        //    bt_pulse_1.Content = "OFF";

        //        //if ((string)ADSDictionary_1000["PS_" + PS3 + "_state_pulseON"][5] == "true")//PS_C201_B_state_pulseON
        //        //    bt_pulse_2.Content = "ON";
        //        //else
        //        //    bt_pulse_2.Content = "OFF";

        //        //if ((string)ADSDictionary_1000["PS_" + PS9 + "_state_pulseON"][5] == "true")//PS_C201_B_state_pulseON
        //        //    bt_pulse_3.Content = "ON";
        //        //else
        //        //    bt_pulse_3.Content = "OFF";

        //        //if ((string)ADSDictionary_1000["PS_" + PS10 + "_state_pulseON"][5] == "true")//PS_C201_B_state_pulseON
        //        //    bt_pulse_4.Content = "ON";
        //        //else
        //        //    bt_pulse_4.Content = "OFF";


        //    }
        //    catch
        //    {

        //    }
        //}

        ////private void bt_start_content( string PS, Button bt_start)
        ////{
        ////    if ((string)ADSDictionary_1000["PS_" + PS + "_state_running"][5] == "true")//PS_C100_state_running
        ////        bt_start.Content = "Running";
        ////    else
        ////        bt_start.Content = "stopped";
        ////}
        ////private void bt_CGGmotion_content(string PS, Button bt_start) //bt_CGG_start2
        ////{
        ////    if ((string)ADSDictionary_1000["GCC_" + PS + "_motion"][5] == "true")////CGG_C101_A_motion
        ////        bt_start.Content = "Running";
        ////    else
        ////        bt_start.Content = "stopped";    
        ////}

        ////当前电源
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    if (!comboBox.IsLoaded)
        //        return;
        //    string name = (sender as ComboBox).Name;
        //    switch (name)
        //    {
        //        case "combo1":
        //            //object comboBoxItem =(sender as ComboBox).SelectedItem;
        //            //PS1 =  comboBoxItem.ToString();
        //            PS1 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //                        ////后期据PLC程序改为变量address
        //            break;
        //        case "combo2":
        //            PS2 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();

        //            break;
        //        case "combo3":
        //            PS3 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo4":
        //            PS4 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo5":
        //            PS5 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo6":
        //            PS6 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo7":
        //            PS7 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo8":
        //            PS8 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo9":
        //            PS9 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo10":
        //            PS10 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo11":
        //            PS11 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo12":
        //            PS12 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo13":
        //            PS13 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //        case "combo14":
        //            PS14 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //            break;
        //    }
        //}

        ////电源起停
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    string PS;
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (true) //抽真空操作进行中 VacuoStart_Cmd
        //            {
        //                string name = ((Button)sender).Name;//当前按钮名称
        //                switch (name)//根据按钮确定要操作的电源名称
        //                {
        //                    case "bt_start_1":
        //                        //object comboBoxItem =(sender as ComboBox).SelectedItem;
        //                        //PS1 =  comboBoxItem.ToString();
        //                        //PS1 = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
        //                        PS = PS1;
        //                        break;
        //                    case "bt_start_2":
        //                        PS = PS2;
        //                        break;
        //                    case "bt_start_3":
        //                        PS = PS3;
        //                        break;
        //                    case "bt_start_4":
        //                        PS = PS4;
        //                        break;
        //                    case "bt_start_5":
        //                        PS = PS5;
        //                        break;
        //                    case "bt_start_6":
        //                        PS = PS6;
        //                        break;
        //                    case "bt_start_7":
        //                        PS = PS7;
        //                        break;
        //                    case "bt_start_8":
        //                        PS = PS8;
        //                        break;
        //                    case "bt_start_9":
        //                        PS = PS9;
        //                        break;
        //                    case "bt_start_10":
        //                        PS = PS10;
        //                        break;
        //                    case "bt_start_11":
        //                        PS = PS11;
        //                        break;
        //                    case "bt_start_12":
        //                        PS = PS12;
        //                        break;
        //                    case "bt_start_13":
        //                        PS = PS13;
        //                        break;
        //                    case "bt_start_14":
        //                        PS = PS14;
        //                        break;
        //                    default:
        //                        PS = "";
        //                        break;
        //                }
        //                PS_start_stop(PS, sender);
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行电源打开关闭操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("启动或关闭电源失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}
        //private void PS_start_stop(string PS, object sender)
        //{
        //    if ((string)App.DIC_vars["PS_" + PS + "_state_stopped"][5] == "true") //PS_C100_state_stopped
        //    {
        //        MessageBoxResult result = MessageBoxX.Show("您确定要打开电源 " + PS + " 吗？", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_start_set"), 1);//PS_C100_start_set
        //            (sender as Button).Content = "启动中...";
        //            Thread.Sleep(1500);
        //            if ((string)App.DIC_vars["PS_" + PS + "_state_running"][5] == "true")//PS_C100_state_running
        //            {
        //                BrushConverter brushConverter = new BrushConverter();
        //                (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FF5DC76D");
        //                (sender as Button).Content = "Running";
        //            }
        //        }
        //    }
        //    if ((string)App.DIC_vars["PS_" + PS + "_state_running"][5] == "true") //PS_C100_state_running
        //    {
        //        MessageBoxResult result = MessageBoxX.Show("您确定要关闭电源 " + PS + " 吗？", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_start_set"), 0);//PS_C100_stop_set
        //            (sender as Button).Content = "关闭中...";
        //            Thread.Sleep(1500);
        //            if ((string)App.DIC_vars["PS_" + PS + "_state_running"][5] == "false")//PS_C100_state_running
        //            {
        //                BrushConverter brushConverter = new BrushConverter();
        //                (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FFB73333");
        //                (sender as Button).Content = "Stopped";
        //            }
        //        }
        //    }
        //}
        ////模式选择
        //private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    if (!comboBox.IsLoaded)
        //        return;
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (true) //抽真空操作进行中 VacuoStart_Cmd
        //            {

        //                string name = (sender as ComboBox).Name;//确定combo名称
        //                string PS;
        //                switch (name)//根据combo确定要操作的电源名称
        //                {
        //                    case "combo_mode_1":
        //                        PS = PS1;
        //                        break;
        //                    case "combo_mode_2":
        //                        PS = PS2;
        //                        break;
        //                    case "combo_mode_":
        //                        PS = PS3;
        //                        break;
        //                    case "combo_mode_4":
        //                        PS = PS4;
        //                        break;
        //                    case "combo_mode_5":
        //                        PS = PS5;
        //                        break;
        //                    case "combo_mode_6":
        //                        PS = PS6;
        //                        break;
        //                    case "combo_mode_7":
        //                        PS = PS7;
        //                        break;
        //                    case "combo_mode_8":
        //                        PS = PS8;
        //                        break;
        //                    case "combo_mode_9":
        //                        PS = PS9;
        //                        break;
        //                    case "combo_mode_10":
        //                        PS = PS10;
        //                        break;
        //                    case "combo_mode_11":
        //                        PS = PS11;
        //                        break;
        //                    case "combo_mode_12":
        //                        PS = PS12;
        //                        break;
        //                    case "combo_mode_13":
        //                        PS = PS13;
        //                        break;
        //                    case "combo_mode_14":
        //                        PS = PS14;
        //                        break;
        //                    default:
        //                        PS = "";
        //                        break;
        //                }
        //                switch ((sender as ComboBox).SelectedIndex)//判断SelectedIndex
        //                {
        //                    case 0:
        //                        App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_mode_P"), 1);

        //                        break;
        //                    case 1:
        //                        App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_mode_U"), 1);

        //                        break;
        //                    case 2:
        //                        App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_mode_I"), 1);

        //                        break;
        //                    default:
        //                        App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_mode_P"), 1);
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行电源模式选择操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("模式更改失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}
        ////脉冲控制
        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    string PS;
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (true) //抽真空操作进行中 VacuoStart_Cmd
        //            {
        //                string name = ((Button)sender).Name;//当前按钮名称
        //                switch (name)//根据按钮确定要操作的电源名称
        //                {
        //                    case "bt_pulse_1":
        //                        PS = PS2;
        //                        break;
        //                    case "bt_pulse_2":
        //                        PS = PS3;
        //                        break;
        //                    case "bt_pulse_3":
        //                        PS = PS8;
        //                        break;
        //                    case "bt_pulse_4":
        //                        PS = PS9;
        //                        break;
        //                    default:
        //                        PS = "";
        //                        break;
        //                }
        //                PulseChange(PS, sender);
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行电源脉冲打开关闭操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("脉冲操作失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}
        //private void PulseChange(string PS, object sender)
        //{
        //    if ((string)App.DIC_vars["PS_" + PS + "_state_pulseON"][5] == "true") //PS_C201_B_state_pulseON
        //    {
        //        MessageBoxResult result = MessageBoxX.Show("您确定需要关闭 " + PS + " PulseON？", "询问信息", Application.Current.MainWindow, MessageBoxButton.OK);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_pulseON_set"), 0);//PS_C100_start_set
        //            Thread.Sleep(1500);
        //            if ((string)App.DIC_vars["PS_" + PS + "_state_pulseON"][5] == "false")//PS_C201_B_state_pulseOFF
        //            {
        //                BrushConverter brushConverter = new BrushConverter();
        //                (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FF165490");
        //                (sender as Button).Content = "OFF";
        //            }
        //        }
        //    }
        //    if ((string)App.DIC_vars["PS_" + PS + "_state_pulseOFF"][5] == "true") //PS_C201_B_state_pulseON
        //    {
        //        MessageBoxResult result = MessageBoxX.Show("您确定需要打开 " + PS + " PulseON？", "询问信息", Application.Current.MainWindow, MessageBoxButton.OK);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("PS_" + PS + "_pulseON_set"), 1);//PS_C100_start_set
        //            Thread.Sleep(1500);
        //            if ((string)App.DIC_vars["PS_" + PS + "_state_pulseON"][5] == "true")//PS_C201_B_state_pulseON
        //            {
        //                BrushConverter brushConverter = new BrushConverter();
        //                (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FF5DC76D");
        //                (sender as Button).Content = "ON";
        //            }
        //        }
        //    }
        //}

        ////  阴极电机旋转
        //private void bt_CGG_start2_Click(object sender, RoutedEventArgs e)
        //{
        //    string PS;
        //    try
        //    {
        //        if (R2R_tag.UserData.permission < 5)
        //        {
        //            MessageBoxX.Show("用户权限不足", "Infomation", Application.Current.MainWindow, MessageBoxButton.OK);
        //        }
        //        else
        //        {
        //            if (true) //抽真空操作进行中 VacuoStart_Cmd
        //            {
        //                string name = ((Button)sender).Name;//当前按钮名称
        //                switch (name)//根据按钮确定要操作的电源名称
        //                {
        //                    case "bt_CGG_start2":
        //                        PS = PS2;
        //                        break;
        //                    case "bt_CGG_start3":
        //                        PS = PS3;
        //                        break;
        //                    case "bt_CGG_start4":
        //                        PS = PS4;
        //                        break;
        //                    case "bt_CGG_start5":
        //                        PS = PS5;
        //                        break;
        //                    case "bt_CGG_start6":
        //                        PS = PS6;
        //                        break;
        //                    case "bt_CGG_start7":
        //                        PS = PS7;
        //                        break;
        //                    case "bt_CGG_start9":
        //                        PS = PS9;
        //                        break;
        //                    case "bt_CGG_start10":
        //                        PS = PS10;
        //                        break;
        //                    case "bt_CGG_start11":
        //                        PS = PS11;
        //                        break;
        //                    case "bt_CGG_start12":
        //                        PS = PS12;
        //                        break;
        //                    case "bt_CGG_start13":
        //                        PS = PS13;
        //                        break;
        //                    case "bt_CGG_start14":
        //                        PS = PS14;
        //                        break;
        //                    default:
        //                        PS = "";
        //                        break;
        //                }
        //                CGG_motion_start(PS, sender);
        //            }
        //            else
        //            {
        //                MessageBoxX.Show("请先停止抽真空操作，再进行阴极电机操作 ", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBoxX.Show("操作失败", "提示信息：", Application.Current.MainWindow, MessageBoxButton.OK);
        //    }
        //}
        //// 旋转阴极电机启动
        //private void CGG_motion_start(string PS, object sender)
        //{
        //    if ((string)App.DIC_vars["GCC_" + PS + "_motion"][5] == "true") ////CGG_C101_A_motion
        //    {
        //        MessageBoxResult result = MessageBoxX.Show("您确定需要关闭 " + PS + " 阴极旋转？", "询问信息", Application.Current.MainWindow, MessageBoxButton.OK);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("GCC_" + PS + "_motion"), 0);////CGG_C101_A_motion
        //            Thread.Sleep(1500);
        //            if ((string)App.DIC_vars["GCC_" + PS + "_motion"][5] == "false")////CGG_C101_A_motion
        //            {
        //                BrushConverter brushConverter = new BrushConverter();
        //                (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FF165490");
        //                (sender as Button).Content = "OFF";
        //            }
        //        }
        //    }
        //    if ((string)App.DIC_vars["GCC_" + PS + "_motion"][5] == "true") ////CGG_C101_A_motion
        //    {
        //        MessageBoxResult result = MessageBoxX.Show("您确定需要打开 " + PS + " 阴极旋转？", "询问信息", Application.Current.MainWindow, MessageBoxButton.OK);
        //        if (result == MessageBoxResult.OK)
        //        {
        //            App.tcadsClient.WriteAny(App.tcadsClient.CreateVariableHandle("GCC_" + PS + "_motion"), 1);////CGG_C101_A_motion
        //            Thread.Sleep(1500);
        //            if ((string)App.DIC_vars["GCC_" + PS + "_motion"][5] == "true")////CGG_C101_A_motion
        //            {
        //                BrushConverter brushConverter = new BrushConverter();
        //                (sender as Button).Background = (Brush)brushConverter.ConvertFromString("#FF5DC76D");
        //                (sender as Button).Content = "ON";
        //            }
        //        }
        //    }
        //}
    }
}
