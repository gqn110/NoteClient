using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace KHopeClient
{
    public class CommonMethod
    {
        #region public static void WriteLogErr(string myErrEx) 系统错误日志
        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="myEx"></param>
        public static void WriteLogErr(string myErrEx)
        {
            try
            {
                StackTrace st = new StackTrace();
                string methodName = st.GetFrame(1).GetMethod().Name;
                string className = st.GetFrame(1).GetMethod().DeclaringType.ToString();

                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Err.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine("");
                m_streamWriter.WriteLine("类名：" + className + "; 方法：" + methodName + "; 时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + " 错误信息：" + myErrEx);
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch
            { }
        }
        #endregion

        #region public static string Obj2Json<T>(T data)  List<T>转Json
        /// <summary>
        ///List<T>转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Obj2Json<T>(T data)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}
