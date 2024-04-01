using MySql.Data.MySqlClient;
using Panuon.UI.Silver;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace R2R.helper
{
    public class Mysqlhelper
    {
        public MySqlConnection _mySqlConn;
        /// <summary>
        ///大批量数据插入,返回成功插入行数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据表</param>
        /// <returns>返回成功插入行数</returns>
        public int BulkInsert(DataTable table, string connectionString)
        {
            if (string.IsNullOrEmpty(table.TableName)) throw new Exception("请给DataTable的TableName属性附上表名称");
            if (table.Rows.Count == 0) return 0;
            int insertCount = 0;
            string tmpPath = Path.GetTempFileName();
            string csv = DataTableToCsv(table);
            File.WriteAllText(tmpPath, csv);
            // MySqlTransaction tran = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {

                try
                {

                    conn.Open();
                    //tran = conn.BeginTransaction();
                    MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                    {
                        FieldTerminator = ",",
                        FieldQuotationCharacter = '"',
                        EscapeCharacter = '"',
                        LineTerminator = "\r\n",
                        FileName = tmpPath,
                        NumberOfLinesToSkip = 0,
                        TableName = table.TableName,
                    };
                    //bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToArray());
                    insertCount = bulk.Load();
                    // tran.Commit();
                }
                catch (MySqlException ex)
                {
                    // if (tran != null) tran.Rollback();
                    throw ex;
                }
            }
            File.Delete(tmpPath);
            return insertCount;
        }
        ///将DataTable转换为标准的CSV  
        /// </summary>  
        /// <param name="table">数据表</param>  
        /// <returns>返回标准的CSV</returns>  
        private static string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。  
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。  
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。  
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
        /// <summary>
        /// 创建新的数据库
        /// NewDatabase("localhost", "root", "pwd", "newtest1");
        /// </summary>
        public void NewDatabase(string dbSource, string dbUid, string dbPwd, string dbName, string connectionString)
        {
            //创建连接字符串con
            //MySqlConnection con = new MySqlConnection("Data Source=" + dbSource + ";Persist Security Info=yes;UserId=" + dbUid + "; PWD=" + dbPwd + ";");
            Connect(connectionString);
            //创建数据库的执行语句
            MySqlCommand cmd = new MySqlCommand("CREATE DATABASE " + dbName, _mySqlConn);
            //_mySqlConn.Open();
            //执行语句
            int res = cmd.ExecuteNonQuery();
            // con.Close();
            Disconnect();
        }
        /// <summary>
        /// 创建数据表
        /// CREATE TABLE `mydb1`.`pvd1` (`名称` VARCHAR(100) NOT NULL,`数据类型` VARCHAR(100) NULL,`通讯地址` VARCHAR(100) NULL,PRIMARY KEY(`名称`))ENGINE = InnoDB DEFAULT CHARACTER SET = utf8;
        /// NewDatatable("localhost", "root", "pwd", "newtest1", "newtb2");
        /// </summary>
        public void NewDatatable(string column1, string column2, string column3, string dbName, string tbName, string connectionString)
        {
            //创建连接字符串con
            //MySqlConnection con = new MySqlConnection("Data Source=" + dbSource + ";Persist Security Info=yes;UserId=" + dbUid + "; PWD=" + dbPwd + ";");
            Connect(connectionString);
            string newTableCMD = " CREATE TABLE " + tbName + "." + dbName + " (" + column1 + " VARCHAR(100) NOT NULL," + column2 + " VARCHAR(100) NULL," + column3 + " VARCHAR(100) NULL,PRIMARY KEY(" + column1 + "))ENGINE = InnoDB DEFAULT CHARACTER SET = utf8";
            MySqlCommand cmd = new MySqlCommand(newTableCMD, _mySqlConn);
            //_mySqlConn.Open();
            //MySqlCommand cmdUseDB = new MySqlCommand("USE " + dbName, con);
            //cmdUseDB.ExecuteNonQuery();
            int res = cmd.ExecuteNonQuery();
            // con.Close();
            Disconnect();
        }
        /// <summary>
        /// 单行传递SQL语句，可以用来创建表、查询等，返回执行结果
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public int ExecuteCommand(string sqlString, string connectionString)
        {
            Connect(connectionString);
            lock (_mySqlConn)
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, _mySqlConn))
                {
                    int Rcmd = cmd.ExecuteNonQuery();
                    Disconnect();
                    return Rcmd;
                }

            }

        }
        /// <summary>
        /// 多行语句执行
        /// </summary>
        /// <param name="sqlStrings"></param>
        /// <param name="connectionString"></param>
        public void ExecuteCommand(string[] sqlStrings, string connectionString)
        {
            Connect(connectionString);
            lock (_mySqlConn)
            {
                using (MySqlTransaction transaction = _mySqlConn.BeginTransaction())
                {
                    foreach (var sqlString in sqlStrings)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(sqlString, _mySqlConn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();

                }
            }
            Disconnect();
        }
        //update 逐条写数据库速度太慢，批量写
        public void Update(string table, DataTable dataTable, string connectionString)
        {
            Connect(connectionString);
            try
            {
                var myDataTable = new DataTable();
                lock (_mySqlConn)
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + table, _mySqlConn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(myDataTable);
                            using (MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter))
                            {
                                adapter.Update(dataTable);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            Disconnect();
        }
        /// <summary>
        /// 查询数据库数据
        /// </summary>
        public DataTable Select(string sql, string connectionString)
        {
            Connect(connectionString);
            DataTable dt = new DataTable();
            try
            {

                lock (_mySqlConn)
                {
                    using (MySqlDataAdapter command = new MySqlDataAdapter(sql, _mySqlConn))
                    {
                        command.Fill(dt);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            Disconnect();
            return dt;
        }
        /// <summary>
        /// 包含源DataTable组成的DataSet，访问时从第索引1开始
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet Select(StringBuilder strSql, int pageSize, string connectionString)
        {
            Connect(connectionString);
            DataSet srcDataSet = new DataSet();
            try
            {
                using (MySqlDataAdapter command = new MySqlDataAdapter(strSql.ToString(), _mySqlConn))
                {
                    command.Fill(srcDataSet, "ds");
                    DataSet distDataSet = srcDataSet;
                    int number = distDataSet.Tables[0].Rows.Count / pageSize;
                    if ((distDataSet.Tables[0].Rows.Count - number * pageSize) > 0)
                        number++;

                    DataTable[] dts = new DataTable[number];
                    int i = 0;
                    for (i = 0; i < number; i++)
                    {
                        dts[i] = srcDataSet.Tables[0].Clone();
                        dts[i].TableName = string.Format("dt{0}", i);
                    }

                    i = 0;
                    foreach (DataRow r in srcDataSet.Tables[0].Rows)
                    {
                        dts[i / pageSize].Rows.Add(r.ItemArray);
                        i++;
                    }

                    foreach (DataTable dt in dts)
                        srcDataSet.Tables.Add(dt);
                }
                Disconnect();
                return srcDataSet;
            }
            catch (MySqlException)
            {
                Disconnect();
                throw;
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }

        }
        /// <summary>
        /// 根据DataTable获得列名
        /// </summary>
        /// <param name="dt">表对象</param>
        /// <returns>返回结果的数据列数组</returns>
        public string[] GetColumnsByDataTable(DataTable dt)
        {
            string[] strColumns = null;
            try
            {
                if (dt != null)
                {
                    if (dt.Columns.Count > 0)
                    {
                        int columnNum = 0;
                        columnNum = dt.Columns.Count;
                        strColumns = new string[columnNum];
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            strColumns[i] = dt.Columns[i].ColumnName;
                        }
                    }

                }
                return strColumns;
            }
            catch (Exception)
            {

                return strColumns = null;
            }


        }
        /// <summary>
        /// 根据数据库IP、名称、账号、密码连接数据库
        /// </summary>
        /// <param name="server"></param>
        /// <param name="dbName"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// string connectionString = string.Format("server={0};port=3306;database={1};uid={2};pwd={3};Charset=utf8;",vserver, dbName, uid, pwd);
        public void Connect(string connectionString)
        {
            //string connectionString = string.Format("server={0};port=3306;database={1};uid={2};pwd={3};Charset=utf8;",
            //    server, dbName, uid, pwd);

            try
            {
                _mySqlConn = new MySqlConnection(connectionString);
                CheckConnection();
            }
            catch (Exception)
            {
                MessageBoxX.Show("连接数据库错误", "提示"); ;
            }
        }
        private void CheckConnection()
        {
            if (!_mySqlConn.Ping())
                _mySqlConn.Open();
        }
        /// <summary>
        /// 断开MySql链接
        /// </summary>
        public void Disconnect()
        {
            if (_mySqlConn == null)
                return;

            _mySqlConn.Close();
        }
    }
}
