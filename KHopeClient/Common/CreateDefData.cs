using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace KHopeClient
{
    public class CreateDefData
    {
        #region 创建表
        /// <summary>
        /// 创建表
        /// </summary>
        public void Create()
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                bool isExists = false;
                StringBuilder sbSql = new StringBuilder();

                #region kh_user

                isExists = TableExists(sqliteHelper, conn, "kh_user");
                if (!isExists)
                {
                    sbSql.Append("create table kh_user([id] integer not null,[user_id] nvarchar(50),[user_name] nvarchar(50),[pass_word] nvarchar(50),[enabled] int,[create_date] nvarchar(50),[update_date] nvarchar(50), Primary Key(id) on conflict abort);");
                }
                #endregion

                #region kh_node

                isExists = TableExists(sqliteHelper, conn, "kh_node");
                if (!isExists)
                {
                    sbSql.Append("create table kh_node([id] integer not null,[width] int,[height] int,[location_x] int,[location_y] int,[font_name] nvarchar(50),[bold] int,[font_size] int,[font_color] nvarchar(50),[content] nvarchar(500),[enabled] int,[create_date] nvarchar(50),[update_date] nvarchar(50), Primary Key(id) on conflict abort);");
                }
                #endregion

                #region kh_param

                isExists = TableExists(sqliteHelper, conn, "kh_param");
                if (!isExists)
                {
                    sbSql.Append("create table kh_param([id] integer not null,[name] nvarchar(50),[value] nvarchar(200),[description] nvarchar(200),[enabled] int,[create_date] nvarchar(50),[update_date] nvarchar(50), Primary Key(id) on conflict abort);");
                }
                #endregion

                #region 事务提交脚本
                if (sbSql.Length > 0)
                {
                    SQLiteTransaction transaction = null;
                    try
                    {
                        transaction = conn.BeginTransaction();
                        sqliteHelper.ExecuteSql(conn, sbSql.ToString());
                        transaction.Commit(); //提交
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        CommonMethod.WriteLogErr(ex.Message);
                    }
                    finally
                    {
                        if (transaction != null)
                            transaction.Dispose();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 检查是否添加表
        /// <summary>
        /// 检查是否添加表
        /// </summary>
        /// <param name="sqliteHelper"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool TableExists(SQLiteHelper sqliteHelper, SQLiteConnection conn, string paramTableName)
        {
            bool exists = false;
            if (sqliteHelper == null)
            {
                sqliteHelper = new SQLiteHelper();
            }
            //判断 TerminalInfo 表是否存在
            string sql = "select COUNT(*) from sqlite_master where type='table' and name='" + paramTableName + "'";
            int result = sqliteHelper.ExecValue(conn, sql);
            if (result > 0)
            {
                exists = true;
            }
            return exists;
        }
        #endregion

        #region 检查字段是否存在
        /// <summary>
        /// 检查字段是否存在
        /// </summary>
        /// <param name="sqliteHelper"></param>
        /// <param name="conn"></param>
        /// <param name="paramTableName"></param>
        /// <param name="paramField"></param>
        /// <returns></returns>
        public bool FieldExists(SQLiteHelper sqliteHelper, SQLiteConnection conn, string paramTableName, string paramField)
        {
            bool exists = false;
            if (sqliteHelper == null)
            {
                sqliteHelper = new SQLiteHelper();
            }
            //判断 TerminalInfo 表是否存在
            string sql = "SELECT *  FROM sqlite_master where type='table' and name='" + paramTableName + "'";
            DataSet ds = sqliteHelper.ExecDataSet(conn, sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string value = ds.Tables[0].Rows[0]["sql"].ToString().ToLower();
                if (value.IndexOf("[" + paramField.ToLower() + "]") > -1)
                    exists = true;
            }
            return exists;
        }

        #endregion

        #region 根据表名获取表所有数据
        /// <summary>
        /// 根据表名获取表所有数据
        /// </summary>
        /// <param name="sqliteHelper"></param>
        /// <param name="conn"></param>
        /// <param name="paramTableName"></param>
        /// <returns></returns>
        public DataSet GetAllDataByTableName(SQLiteHelper sqliteHelper, SQLiteConnection conn, string paramTableName)
        {
            if (sqliteHelper == null)
                sqliteHelper = new SQLiteHelper();

            return sqliteHelper.ExecDataSet(conn, "select * from " + paramTableName + ";");
        }
        #endregion

        #region 获取创建表语句
        /// <summary>
        /// 获取创建表的语句
        /// </summary>
        /// <param name="sqliteHelper"></param>
        /// <param name="conn"></param>
        /// <param name="paramTableName"></param>
        /// <returns></returns>
        public string GetCreateSql(SQLiteHelper sqliteHelper, SQLiteConnection conn, string paramTableName)
        {
            string returnValue = string.Empty;
            string sql = "select sql from sqlite_master where type='table' and name='" + paramTableName + "'";
            DataSet dsResult = sqliteHelper.ExecDataSet(conn, sql);
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                returnValue = dsResult.Tables[0].Rows[0]["sql"].ToString();
            }
            return returnValue;
        }
        #endregion
    }
}
