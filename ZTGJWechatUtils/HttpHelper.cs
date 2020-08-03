using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    public abstract class HttpHelper
    {
        private static HttpClient _client;
        /// <summary>
        /// PostAsync请求
        /// </summary>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string strURL, string strParm, string contentType = "application/x-www-form-urlencoded", int timeout = 10)
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("Accept-Charset", "utf-8");
                _client.Timeout = new TimeSpan(0, 0, 0, timeout, 0);
            }
            StringContent postContent = new StringContent(strParm, Encoding.UTF8);
            postContent.Headers.Remove("Content-Type");
            postContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var taskHttpResponseMessage = await _client.PostAsync(strURL, postContent).ConfigureAwait(false);
            var responseData = await taskHttpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseData;
        }

        #region post 请求
        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="strParm"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string HttpPostEx(string strURL, string strParm, string contentType = "application/x-www-form-urlencoded", int timeout = 10)
        {
            string res_data = string.Empty;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(strParm);
                request = (HttpWebRequest)HttpWebRequest.Create(strURL);//创建请求实例

                if (string.IsNullOrEmpty(contentType))
                    contentType = "application/x-www-form-urlencoded";

                request.Timeout = timeout * 1000;   //超时
                request.ReadWriteTimeout = timeout * 1000;
                request.Method = "POST";//请求方法
                request.ContentType = contentType;//请求类型
                request.ContentLength = postBytes.Length;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.ServicePoint.ConnectionLimit = 65500;
                request.AllowWriteStreamBuffering = false;
                request.Proxy = null;
                request.AutomaticDecompression = DecompressionMethods.GZip;

                // Set up the connection to optimize for web services and receive compressed responses.
                ServicePointManager.UseNagleAlgorithm = false;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DefaultConnectionLimit = 512;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    res_data = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (null != response) response.Close();
                if (null != request) request.Abort();
                response = null; request = null;

            }
            return res_data;
        }
        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="strParm"></param>
        /// <returns></returns>
        public static string HttpPost(string strURL, string strParm, string contentType = "application/x-www-form-urlencoded")
        {
            string res_data = string.Empty;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(strParm);
                request = (HttpWebRequest)HttpWebRequest.Create(strURL);//创建请求实例

                request.Timeout = 200000;   //超时
                request.ReadWriteTimeout = 50000;
                request.Method = "POST";//请求方法
                request.ContentType = contentType;//请求类型
                request.ContentLength = postBytes.Length;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.ServicePoint.ConnectionLimit = 65500;
                request.AllowWriteStreamBuffering = false;
                request.Proxy = null;
                request.AutomaticDecompression = DecompressionMethods.GZip;

                // Set up the connection to optimize for web services and receive compressed responses.
                ServicePointManager.UseNagleAlgorithm = false;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DefaultConnectionLimit = 512;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    res_data = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (null != response) response.Close();
                if (null != request) request.Abort();
                response = null; request = null;

            }
            return res_data;
        }
        #endregion

        /// <summary>
        /// POST请求 HttpClient
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="strParm"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string HttpClientPost(string strURL, string strParm, string contentType = "application/x-www-form-urlencoded", int timeout = 10)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(contentType))
                contentType = "application/x-www-form-urlencoded";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, timeout);
                    HttpContent httpContent = new StringContent(strParm, Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    Uri address = new Uri(strURL);
                    var response = client.PostAsync(address, httpContent);
                    result = response.Result.Content.ReadAsStringAsync().Result;//返回值
                }
            }
            catch (Exception ex)
            {
                result = "操作超时"+ ex.Message;
            }
            return result;
        }

        #region get 请求
        /// <summary>
        /// get 请求
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="strParm"></param>
        /// <returns></returns>
        public static string HttpGet(string strURL)
        {
            HttpWebRequest request = null;
            string retString = "";
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                request.UserAgent = null;
                request.Timeout = 200000;
                response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                response.Close();
                response.Dispose();

            }
            catch (Exception ex)
            {
                retString = ex.Message;
            }
            finally
            {
                if (null != response) response.Close();
                if (null != request) request.Abort();
                response = null; request = null;

            }
            return retString;

        }
        #endregion

        #region 指定Post地址使用Get 方式获取
        /// <summary>
        /// 指定Post地址使用Get 方式获取
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>
        /// <returns></returns>
        public static string PostByGet(string url, string content)
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        #endregion

        #region 解决问题：基础连接已经关闭: 未能为 SSL/TLS 安全通道建立信任关系。
        /// <summary>
        /// Sets the cert policy.
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }
        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }

        #endregion
    }
}
