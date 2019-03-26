using KHopeClient.IService;
using KHopeClient.Model;
using System;
using System.Data;
using System.Data.SQLite;

namespace KHopeClient.Service
{
    public class KHUserService : IKHUserService
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Add(KHUser param)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                string res = sqliteHelper.ExecID(conn, @"insert into kh_user (id,user_id,user_name,pass_word,enabled
                                                                            ,create_date,update_date) values(null,'"
                                                                  + param.UserID
                                                                  + "','" + param.UserName
                                                                  + "','" + param.PassWord
                                                                  + "','" + 1
                                                                  + "','" + param.CreateDate
                                                                  + "','" + param.UpdateDate
                                                                  + "');");
                return Convert.ToInt32(res);
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Update(KHUser param)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                sqliteHelper.ExecuteSql(conn, @"update kh_user set user_id='" + param.UserID
                                                                                     + "',user_name='" + param.UserName
                                                                                     + "',pass_word='" + param.PassWord
                                                                                     + "',enabled='" + param.Enabled
                                                                                     + "',create_date= '" + param.CreateDate
                                                                                     + "',update_date='" + param.UpdateDate + "';");
                return true;
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 获取对象
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public KHUser GetByUserID(string userID)
        {
            KHUser user = null;
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                DataSet ds = sqliteHelper.ExecDataSet(conn, "select * from kh_user where user_id='" + userID + "';");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    user = new KHUser();
                    user.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    user.UserID = ds.Tables[0].Rows[0]["user_id"].ToString();
                    user.UserName = ds.Tables[0].Rows[0]["user_name"].ToString();
                    user.PassWord = ds.Tables[0].Rows[0]["pass_word"].ToString();
                    user.Enabled = Convert.ToInt32(ds.Tables[0].Rows[0]["enabled"].ToString());
                    user.CreateDate = ds.Tables[0].Rows[0]["create_date"].ToString();
                    user.UpdateDate = ds.Tables[0].Rows[0]["update_date"].ToString();
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return user;
        }
        #endregion

        #region 归档所有用户
        /// <summary>
        /// 归档所有用户
        /// </summary>
        public void ArchiveAllUser()
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                sqliteHelper.ExecuteSql(conn, @"update kh_user set enabled='0';");
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

        #region 获取有效的用户
        /// <summary>
        /// 获取有效的用户
        /// </summary>
        /// <returns></returns>
        public KHUser GetByEnabled()
        {
            KHUser user = null;
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                DataSet ds = sqliteHelper.ExecDataSet(conn, "select * from kh_user where enabled=1;");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    user = new KHUser();
                    user.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    user.UserID = ds.Tables[0].Rows[0]["user_id"].ToString();
                    user.UserName = ds.Tables[0].Rows[0]["user_name"].ToString();
                    user.PassWord = ds.Tables[0].Rows[0]["pass_word"].ToString();
                    user.Enabled = Convert.ToInt32(ds.Tables[0].Rows[0]["enabled"].ToString());
                    user.CreateDate = ds.Tables[0].Rows[0]["create_date"].ToString();
                    user.UpdateDate = ds.Tables[0].Rows[0]["update_date"].ToString();
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return user;
        }
        #endregion
    }
}
