using KHopeClient.IService;
using KHopeClient.Model;
using System;
using System.Data;
using System.Data.SQLite;

namespace KHopeClient.Service
{
    public class KHParamService : IKHParamService
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Save(string name, string value)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                KHParam param = GetByName(name);
                if (param != null)
                {
                    sqliteHelper.ExecuteSql(conn, @"update kh_param set name='" + param.Name
                                                                                   + "',value='" + param.Value
                                                                                   + "',description='" + param.Description
                                                                                   + "',enabled=1,create_date='" + param.CreateDate
                                                                                   + "',update_date='" + param.UpdateDate
                                                                                   + "' where id = '" + param.ID + "';");
                }
                else
                {
                    sqliteHelper.ExecID(conn, @"insert into kh_param (id,name,value,description,enabled
                                                                            ,create_date,update_date) values(null,'"
                                                                + name
                                                                + "','" + value
                                                                + "',NULL,'" + 1
                                                                + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                + "',NULL);");
                }

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
        /// <returns></returns>
        public KHParam GetByName(string name)
        {
            KHParam param = null;
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                DataSet ds = sqliteHelper.ExecDataSet(conn, "select * from kh_param where name='" + name + "';");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    param = new KHParam();
                    param.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    param.Name = ds.Tables[0].Rows[0]["name"].ToString();
                    param.Value = ds.Tables[0].Rows[0]["value"].ToString();
                    param.Description = ds.Tables[0].Rows[0]["description"].ToString();
                    param.Enabled = Convert.ToInt32(ds.Tables[0].Rows[0]["enabled"].ToString());
                    param.CreateDate = ds.Tables[0].Rows[0]["create_date"].ToString();
                    param.UpdateDate = ds.Tables[0].Rows[0]["update_date"].ToString();
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
            return param;
        }
        #endregion

    }
}
