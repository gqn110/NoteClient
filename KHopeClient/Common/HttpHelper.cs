using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KHopeClient
{
    public class HttpHelper
    {
        #region 获取url
        /// <summary>
        /// 获取url
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        private Uri GetUrl(string apiName)
        {
            return new Uri(AppConfigHelper.ServiceUrl + "/" + apiName.TrimStart('/'));
        }
        #endregion

        #region 请求Token
        /// <summary>
        /// 令牌
        /// </summary>
        public string GetToken()
        {
            string token = MemoryCacheHelper.GetCacheItem<string>("Security_BBTToken", delegate () { return ""; }, new TimeSpan(0, 30, 0));
            ////Token为空则再次获取
            //if (string.IsNullOrEmpty(token))
            //{
            //    string mac = CommonMethod.GetMacAddressByDos();
            //    string resultValue = HttpGetNoToken(this._ApiUrl + "SystemBasic/GetToken?Mac=" + mac);
            //    var result = resultValue.FromJSON<BaseResult>();
            //    if (result != null && result.Data != null)
            //    {
            //        token = "Bearer " + result.Data.ToString();
            //        MemoryCacheHelper.Clear("Security_BBTToken");
            //        token = MemoryCacheHelper.GetCacheItem<string>("Security_BBTToken", delegate ()
            //        {
            //            return token;
            //        }, new TimeSpan(0, 30, 0));
            //    }
            //}
            return token;
        }

        /// <summary>
        /// 清空Token
        /// </summary>
        private void ClearToken()
        {
            MemoryCacheHelper.Clear("Security_BBTToken");
        }
        #endregion

        #region Get方式请求
        /// <summary>
        /// Get方式请求
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public string HttpGetNoToken(string controlName)
        {
            string resultValue = "";
            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.Timeout = new TimeSpan(0, 0, 10);
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(this.GetUrl(controlName)).Result;
                    resultValue = response.Content.ReadAsStringAsync().Result;
                }
                catch
                { }
            }
            return resultValue;
        }

        /// <summary>
        /// Get方式请求
        /// </summary>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public string HttpGet(string controlName)
        {
            string resultValue = string.Empty;

            string token = this.GetToken();
            if (string.IsNullOrEmpty(token))
                return resultValue;

            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.Timeout = new TimeSpan(0, 0, 10);
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("Authorization", token);
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(this.GetUrl(controlName)).Result;
                    resultValue = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("401") > -1 || ex.Message.IndexOf("403") > -1)
                        this.ClearToken();
                    throw;
                }
            }
            return resultValue;
        }
        #endregion

        #region Post请求

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="info"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string HttpPost(string controlName, IDictionary<string, string> parameters)
        {
            string resultValue = string.Empty;
            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
                string token = this.GetToken();
                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);
                try
                {
                    HttpResponseMessage response = httpClient.PostAsync(GetUrl(controlName), parameters == null ? null : new FormUrlEncodedContent(parameters)).Result;
                    response.EnsureSuccessStatusCode();
                    resultValue = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("401") > -1 || ex.Message.IndexOf("403") > -1)
                        this.ClearToken();
                    throw;
                }
            }
            return resultValue;
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="url">调用的Api地址</param>  
        /// <param name="requestJson">表单数据（json格式）</param>  
        /// <returns></returns>  
        public string PostResponseJson(string url, object param)
        {
            string resultValue = string.Empty;

            string requestJson = "";
            if (param != null)
                requestJson = CommonMethod.Obj2Json(param);

            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");

                string token = this.GetToken();
                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);

                HttpContent httpContent = new StringContent(requestJson);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                try
                {
                    HttpResponseMessage response = httpClient.PostAsync(this.GetUrl(url), httpContent).Result;
                    //确保HTTP成功状态值
                    response.EnsureSuccessStatusCode();
                    //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                    resultValue = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("401") > -1 || ex.Message.IndexOf("403") > -1)
                        ClearToken();
                    throw;
                }
            }
            return resultValue;
        }

        #endregion
    }
}
