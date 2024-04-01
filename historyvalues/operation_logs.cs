using common;
using common.tag;
using R2R.helper;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;

namespace R2R.historyvalues
{
    internal class save_operation_logs
    {
        static ConcurrentDictionary<string, runTag> logs = new ConcurrentDictionary<string, runTag>();
        public static void operationLog(object obj)
        {
            if (!Mwin.mainwin_loaded)
            {
                return;
            }
            if (logs.Count == 0 && communicationTag.Current.Dic_ranTags_logout.Count > 0)
            {
                foreach (var tag in communicationTag.Current.Dic_ranTags_logout)
                {
                    logs.TryAdd(tag.Key, new runTag());
                }
            }
            if (communicationTag.Current.Dic_ranTags_logout.Count > 0)
            {
                foreach (var item in communicationTag.Current.Dic_ranTags_logout)
                {
                    if (item.Value.value != null)
                    {

                        if (item.Value.tagType == DataType.BOOL)
                        {
                            if (Convert.ToBoolean(item.Value.value) != Convert.ToBoolean(logs[item.Key].value))
                            {
                                insert_row_SQL(item.Value);
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(item.Value.value) != Convert.ToDouble(logs[item.Key].value))
                            {
                                insert_row_SQL(item.Value);
                            }
                        }

                        logs[item.Key].value = item.Value.value;
                    }
                }
            }
            Mwin.timer_operation_logs.Change(200, Timeout.Infinite);
        }
        static void insert_row_SQL(runTag tag)
        {
            //INSERT INTO alarm ( [description], [value], [varname], [user]) VALUES ( N'q', N'w', N'e', N'r')
            sqlClientHelper.ExecteNonQuery(CommandType.Text, "INSERT INTO operation ( [operation], [value], [varname], [user]) VALUES ( N'"
                + Convert.ToString(tag.description)
                + "', N'"
                + Convert.ToString(tag.value)
                + "', N'"
                + Convert.ToString(tag.name)
                + "', N'"
                + Convert.ToString(Mwin.user_alive.Username)
                + "')");
        }
    }
}
