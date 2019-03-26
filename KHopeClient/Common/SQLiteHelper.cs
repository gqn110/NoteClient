using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace KHopeClient
{
    public class SQLiteHelper
    {
        public SQLiteConnection GetSQLiteConnection()
        {
            SQLiteConnection mySQLiteConnection = new SQLiteConnection();
            try
            {
                string file = AppDomain.CurrentDomain.BaseDirectory + "khope.db";
                if (!Directory.Exists(file.Substring(0, file.LastIndexOf("\\"))))
                    Directory.CreateDirectory(file.Substring(0, file.LastIndexOf("\\")));
                if (!File.Exists(file)) //判断数据库文件是否存在
                    SQLiteConnection.CreateFile(file);

                mySQLiteConnection = new SQLiteConnection("Data Source = " + file + ";Pooling=true;FailIfMissing=false;Version=3;");
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
            return mySQLiteConnection;
        }

        #region ExecuteSql
        /// <summary>
        /// 执行sql不返回
        /// </summary>
        /// <param name="sqlStr"></param>
        public void ExecuteSql(SQLiteConnection conn, string sqlStr)
        {
            bool isClose = conn == null;
            try
            {
                if (isClose)
                {
                    conn = this.GetSQLiteConnection();
                    conn.Open();
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                }
                SQLiteCommand m_cmd = new SQLiteCommand(conn);
                m_cmd.CommandText = sqlStr;
                m_cmd.ExecuteNonQuery();
                m_cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (isClose)
                    conn.Close();
            }
        }
        #endregion

        #region ExecID
        /// <summary>
        /// 执行sql返回主键ID
        /// </summary>
        /// <param name="sqlStr"></param>
        public string ExecID(SQLiteConnection conn, string sqlStr)
        {
            bool isClose = conn == null;
            string resultValue = string.Empty;
            try
            {
                if (isClose)
                {
                    conn = this.GetSQLiteConnection();
                    conn.Open();
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                }
                SQLiteCommand m_cmd = new SQLiteCommand(conn);
                m_cmd.CommandText = sqlStr + "select last_insert_rowid();";
                object res = m_cmd.ExecuteScalar();
                if (!string.IsNullOrEmpty(res.ToString()))//返回主键ID
                {
                    resultValue = res.ToString();
                }
                m_cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (isClose)
                    conn.Close();
            }
            return resultValue;
        }
        #endregion

        #region ExecValue
        /// <summary>
        /// 执行SQL返回INT值
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public int ExecValue(SQLiteConnection conn, string sqlStr)
        {
            bool isClose = conn == null;
            int resultValue = 0;
            try
            {
                if (isClose)
                {
                    conn = this.GetSQLiteConnection();
                    conn.Open();
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                }
                SQLiteCommand m_cmd = new SQLiteCommand(conn);
                m_cmd.CommandText = sqlStr;
                object res = m_cmd.ExecuteScalar();
                if (res != null && !string.IsNullOrEmpty(res.ToString()))//返回INT
                {
                    resultValue = Convert.ToInt32(res.ToString());
                }
                m_cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (isClose)
                    conn.Close();
            }
            return resultValue;
        }
        #endregion

        #region ExecDataSet
        /// <summary>
        /// 执行sql返回DetaSet
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public DataSet ExecDataSet(SQLiteConnection conn, string sqlStr)
        {
            bool isClose = conn == null;
            DataSet ds = new DataSet();
            try
            {
                if (isClose)
                {
                    conn = this.GetSQLiteConnection();
                    conn.Open();
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                }
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);
                cmd.Dispose();
                ds.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (isClose)
                    conn.Close();
            }
            return ds;
        }
        #endregion

        #region  public int SaveDataTable(DataTable dt, string tableName) 保存DataTable
        /// <summary>
        /// 保存DataTable 
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>影响的行数</returns>
        public int SaveDataTable(SQLiteConnection conn, DataTable dt, string tableName)
        {
            bool isClose = conn == null;
            SQLiteTransaction _tran = null;
            try
            {
                if (isClose)
                {
                    conn = this.GetSQLiteConnection();
                    conn.Open();
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                }
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM " + tableName;
                SQLiteDataAdapter oda = new SQLiteDataAdapter(command);
                SQLiteCommandBuilder ocb = new SQLiteCommandBuilder(oda);
                oda.InsertCommand = ocb.GetInsertCommand();
                oda.UpdateCommand = ocb.GetUpdateCommand();
                oda.DeleteCommand = ocb.GetDeleteCommand();

                _tran = conn.BeginTransaction();
                command.Transaction = _tran;
                int result = oda.Update(dt);
                _tran.Commit();
                return result;
            }
            catch (Exception ex)
            {
                _tran.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_tran != null)
                    _tran.Dispose();
                if (isClose)
                    conn.Close();
            }
        }
        #endregion
    }
}
