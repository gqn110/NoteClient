using KHopeClient.IService;
using KHopeClient.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace KHopeClient.Service
{
    public class KHNodeService : IKHNodeService
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Add(KHNode param)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                string res = sqliteHelper.ExecID(conn, @"insert into kh_node (id,width,height,location_x,location_y
                                                                            ,font_name,bold,font_size,font_color,content,enabled
                                                                            ,create_date,update_date) values(null,'"
                                                                  + param.Width
                                                                  + "','" + param.Height
                                                                  + "','" + param.LocationX
                                                                  + "','" + param.LocationY
                                                                  + "','" + param.FontName
                                                                  + "','" + param.Bold
                                                                  + "','" + param.FontSize
                                                                  + "','" + param.FontColor
                                                                  + "','" + param.Content
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
        public bool Update(KHNode param)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                sqliteHelper.ExecuteSql(conn, @"update kh_node set width='" + param.Width
                                                                                     + "',height='" + param.Height
                                                                                     + "',location_x='" + param.LocationX
                                                                                     + "',location_y='" + param.LocationY
                                                                                     + "',font_name= '" + param.FontName
                                                                                     + "', bold='" + param.Bold
                                                                                     + "',font_size='" + param.FontSize
                                                                                     + "',font_color='" + param.FontColor
                                                                                     + "',content='" + param.Content
                                                                                     + "',enabled=1,create_date='" + param.CreateDate
                                                                                     + "',update_date='" + param.UpdateDate
                                                                                     + "' where id = '" + param.ID + "';");
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
        public KHNode GetByID(int id)
        {
            KHNode node = null;
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                DataSet ds = sqliteHelper.ExecDataSet(conn, "select * from kh_node where id='" + id + "';");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    node = new KHNode();
                    node.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    node.Width = Convert.ToInt32(ds.Tables[0].Rows[0]["width"].ToString());
                    node.Height = Convert.ToInt32(ds.Tables[0].Rows[0]["height"].ToString());
                    node.LocationX = Convert.ToInt32(ds.Tables[0].Rows[0]["location_x"].ToString());
                    node.LocationY = Convert.ToInt32(ds.Tables[0].Rows[0]["location_y"].ToString());
                    node.FontName = ds.Tables[0].Rows[0]["font_name"].ToString();
                    node.Bold = Convert.ToInt32(ds.Tables[0].Rows[0]["bold"].ToString());
                    node.FontSize = Convert.ToInt32(ds.Tables[0].Rows[0]["font_size"].ToString());
                    node.FontColor = ds.Tables[0].Rows[0]["font_color"].ToString();
                    node.Content = ds.Tables[0].Rows[0]["content"].ToString();
                    node.Enabled = Convert.ToInt32(ds.Tables[0].Rows[0]["enabled"].ToString());
                    node.CreateDate = ds.Tables[0].Rows[0]["create_date"].ToString();
                    node.UpdateDate = ds.Tables[0].Rows[0]["update_date"].ToString();
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
            return node;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<KHNode> GetList()
        {
            List<KHNode> lst = new List<KHNode>();
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                KHNode node = null;
                DataSet ds = sqliteHelper.ExecDataSet(conn, "select * from kh_node where enabled=1;");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    node = new KHNode();
                    node.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                    node.Width = Convert.ToInt32(ds.Tables[0].Rows[i]["width"].ToString());
                    node.Height = Convert.ToInt32(ds.Tables[0].Rows[i]["height"].ToString());
                    node.LocationX = Convert.ToInt32(ds.Tables[0].Rows[i]["location_x"].ToString());
                    node.LocationY = Convert.ToInt32(ds.Tables[0].Rows[i]["location_y"].ToString());
                    node.FontName = ds.Tables[0].Rows[i]["font_name"].ToString();
                    node.Bold = Convert.ToInt32(ds.Tables[0].Rows[i]["bold"].ToString());
                    node.FontSize = Convert.ToInt32(ds.Tables[0].Rows[i]["font_size"].ToString());
                    node.FontColor = ds.Tables[0].Rows[i]["font_color"].ToString();
                    node.Content = ds.Tables[0].Rows[i]["content"].ToString();
                    node.Enabled = Convert.ToInt32(ds.Tables[0].Rows[i]["enabled"].ToString());
                    node.CreateDate = ds.Tables[0].Rows[i]["create_date"].ToString();
                    node.UpdateDate = ds.Tables[0].Rows[i]["update_date"].ToString();
                    lst.Add(node);
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
            return lst;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            SQLiteConnection conn = sqliteHelper.GetSQLiteConnection();
            try
            {
                conn.Open();
                sqliteHelper.ExecuteSql(conn, @"delete from kh_node where id='" + id + "';");
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
    }
}
