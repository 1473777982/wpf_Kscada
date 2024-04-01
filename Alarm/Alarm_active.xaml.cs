using common;
using common.tag;
using R2R.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace R2R.Alarm
{
    /// <summary>
    /// Alarm.xaml 的交互逻辑
    /// </summary>
    public partial class Alarm_active : Page
    {
        DispatcherTimer timer_alarm = new DispatcherTimer();

        public static DataTable Table_alarm = new DataTable();
        public static DataTable Table_warn = new DataTable();
        //public static DataTable Table_alarm_customer = new DataTable();
        public static DataTable Table_alarm_temp = new DataTable();
        public static DataTable Table_warn_temp = new DataTable();
        public DataTable alarmTable_UI { get; set; }  //报警dataGrid  source
        public DataTable warnTable_UI { get; set; }   //警告dataGrid  source
        public Alarm_active()
        {
            InitializeComponent();
            Table_alarm = Mwin.mW_R2RDataSet.alarm.Clone();
            Table_alarm.PrimaryKey = null;
            Table_alarm.Columns.RemoveAt(0);
            Table_warn = Table_alarm.Clone();
            Table_alarm_temp = Table_alarm.Clone();
            Table_warn_temp = Table_alarm.Clone();
            //Table_alarm_customer = Table_alarm.Clone();

            alarmTable_UI = Table_alarm.Clone();
            warnTable_UI = Table_alarm.Clone();

            timer_alarm.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer_alarm.Tick += new EventHandler(TimeAction);
            timer_alarm.Start();
            dataGrid_Alarm.ItemsSource = alarmTable_UI.DefaultView;
            dataGrid_Warn.ItemsSource = warnTable_UI.DefaultView;
        }
        //刷新alarm dataGrid
        public void TimeAction(object sender, EventArgs e)
        {
            lock (Table_alarm)
            {

                if (Table_alarm.Rows.Count > 0)
                {
                    //Table_alarm.Merge(Table_alarm_customer);  //合并表格
                    //var table = distinctdata(Table_alarm);
                    //foreach (DataRow row in table.Rows)
                    //{
                    //    seprate(row);
                    //}
                    alarmTable_UI = Table_alarm.Copy();
                }
                if (Table_warn.Rows.Count > 0)
                {
                    warnTable_UI = Table_warn.Copy();
                }


            }
        }

        void seprate(DataRow dt)
        {
            //报警
            if (((string)dt["description"]).Contains("报警"))
            {
                alarmTable_UI.Rows.Add(dt.ItemArray);
            }
            //警告
            if (((string)dt["description"]).Contains("警告"))
            {
                warnTable_UI.Rows.Add(dt.ItemArray);
            }
        }
        /// <summary>
        /// 去除datatable重复数据
        /// </summary>
        public DataTable distinctdata(DataTable data)
        {
            DataTable distinctdata = new DataTable();

            List<string> headers = new List<string>();
            string[] headersName;

            foreach (DataColumn colum in data.Columns) //遍历出表头
            {
                headers.Add(colum.ToString());
            }
            headersName = headers.ToArray();

            DataView dv = new DataView(data);
            distinctdata = dv.ToTable(true, headersName); //对表所有字段进行过滤，true表示使用distinct方法
            return distinctdata;
        }


        //Thread  alarm
        public static void alarm_now(object obj)
        {
            if (!Mwin.mainwin_loaded)
            {
                return;
            }
            lock (Table_alarm)
            {
                if (Table_alarm.Rows.Count > 0)
                {
                    Table_alarm.Clear();
                }
                foreach (var item in communicationTag.Current.Dic_ranTags)
                {
                    switch (item.Value.alarmType)
                    {
                        case AlarmType.OnAlarm:
                            if (item.Value.value != null)
                            {
                                if (Convert.ToBoolean(item.Value.value))
                                {
                                    Table_addrow(item);
                                }
                            }
                            break;
                        case AlarmType.OffAlarm:
                            if (!DBNull.Value.Equals(item.Value.value))
                            {
                                if (!Convert.ToBoolean(item.Value.value))
                                {
                                    Table_addrow(item);
                                }
                            }

                            break;
                        case AlarmType.HighAlarm:
                            if (!DBNull.Value.Equals(item.Value.value))
                            {
                                if (Convert.ToDouble(item.Value.value) > Convert.ToDouble(item.Value.alarmhigh))
                                {
                                    Table_addrow(item);
                                }
                            }
                            break;
                        case AlarmType.LowAlarm:
                            if (!DBNull.Value.Equals(item.Value.value))
                            {
                                if (Convert.ToDouble(item.Value.value) < Convert.ToDouble(item.Value.alarmlow))
                                {
                                    Table_addrow(item);
                                }
                            }
                            break;
                    }
                }
                //添加报警历史记录
                if (Table_alarm.Rows.Count > 0)
                {
                    foreach (DataRow row in Table_alarm.Rows)
                    {
                        try
                        {
                            if (Table_alarm_temp.Select("varname=" + "'" + (string)row["varname"] + "'").Length <= 0)
                            {
                                Operations.insert_alarm_row_SQL(row);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                Table_alarm_temp = Table_alarm.Copy();
            }


            Mwin.timer_alarm.Change(1000, Timeout.Infinite);
        }

        //添加行
        static void Table_addrow(KeyValuePair<string, runTag> dt)
        {

            DataRow row = Table_alarm.NewRow();
            row[0] = dt.Value.description;
            row[1] = DateTime.Now;
            row[2] = dt.Value.value;
            row[3] = dt.Value.name;
            row[4] = Mwin.user_alive.Username;
            //报警
            if (dt.Value.description.Contains("报警"))
            {
                Table_alarm.Rows.Add(row);
            }
            //警告
            if (dt.Value.description.Contains("警告"))
            {
                Table_warn.Rows.Add(row);
            }
           
        }



        private void Button_his(object sender, RoutedEventArgs e)
        {
            new alarmHis().Show();
        }
        //确认
        private void conform_Click(object sender, RoutedEventArgs e)
        {

        }
        //复位
        private void reset_Click(object sender, RoutedEventArgs e)
        {

        }
        //消音
        private void mute_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
