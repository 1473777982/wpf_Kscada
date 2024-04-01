using R2R.Alarm;
using System;
using System.Data;

namespace R2R.helper
{
    public class Operations
    {
        #region   log alarm 操作
        //添加操作记录
        public static void addoperationTOsql(string operation)
        {
            string sql = "INSERT INTO mydb1.operations ( operation, users ) VALUES ('" + operation + "','" + Mwin.user_alive.Username + "')";
            //App.mysqlhelper.ExecuteCommand(sql, App.connectionString_mySQL);
        }
        //添加Log函数
        public static void addLog(string operation)
        {
            App.Log.Info(DateTime.Now.ToString() + " " + operation + " " + Mwin.user_alive.Username);
        }
        //添加用户报警
        public static void AddAlarm(string des)
        {
            lock (Alarm_active.Table_alarm)
            {
                DataRow row = Alarm_active.Table_alarm.NewRow();
                row[0] = des;
                row[1] = DateTime.Now;
                row[2] = 0;
                row[3] = "others";
                row[4] = Mwin.user_alive.Username;
                Alarm_active.Table_alarm.Rows.Add(row);
                insert_alarm_row_SQL(row);
            }

        }
        //删除用户报警
        public static void DelAlarm(string des)
        {
            lock (Alarm_active.Table_alarm)
            {
                var row = Alarm_active.Table_alarm.Select("description=" + "'" + des + "'")[0];
                Alarm_active.Table_alarm.Rows.Remove(row);
                Alarm_active.Table_alarm.AcceptChanges();
            }
        }
        //添加报警记录
        public static void insert_alarm_row_SQL(DataRow dataRow)
        {
            //INSERT INTO alarm ( [description], [value], [varname], [user]) VALUES ( N'q', N'w', N'e', N'r')
            sqlClientHelper.ExecteNonQuery(CommandType.Text, "INSERT INTO alarm ( [description], [value], [varname], [user]) VALUES ( N'"
                + Convert.ToString(dataRow["description"])
                + "', N'"
                + Convert.ToString(dataRow["value"])
                + "', N'"
                + Convert.ToString(dataRow["varname"])
                + "', N'"
                + Convert.ToString(dataRow["user"])
                + "')");
        }
        #endregion
    }
}
