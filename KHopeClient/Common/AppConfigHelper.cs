using System;
using System.Xml;

namespace KHopeClient
{
    public class AppConfigHelper
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string ServiceUrl
        {
            get
            {
                string url = MemoryCacheHelper.GetCacheItem<string>("ServiceUrl"
                                              , delegate () { return GetXml("ServiceUrl"); }
                                              , new TimeSpan(0, 30, 0));
                return url;
            }
        }

        #region public static string GetXml(string paramKey) 获取App.config值
        //防止多线程同时调用出现进程占用问题
        private static object getXml = new object();

        /// <summary>
        ///  获取App.config值
        /// </summary>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        private static string GetXml(string paramKey)
        {
            lock (getXml)
            {
                XmlDocument myXml = new XmlDocument();
                myXml.Load(AppDomain.CurrentDomain.BaseDirectory + "KHopeClient.exe.config");
                XmlNodeList mXmlNodeList = myXml.SelectNodes("configuration/appSettings/add");
                foreach (XmlNode myXmlNode in mXmlNodeList)
                {
                    if (myXmlNode.Attributes["key"].Value.ToLower() == paramKey.ToLower())
                    {
                        return myXmlNode.Attributes["value"].Value;
                    }
                }
            }
            return string.Empty;
        }
        #endregion
    }
}
