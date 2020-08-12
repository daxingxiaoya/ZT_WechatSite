using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    /// <summary>
    /// 版 本 Jointac v1 开发框架
    /// Copyright (c) 2012-2017 上海展通国际物流有限公司
    /// 创建人：jointac
    /// 日 期：2019.08.05
    /// 描 述：后台调用接口
    /// </summary>
    public class HttpMethods
    {
        /// <summary>
        /// 创建HttpClient
        /// </summary>
        /// <returns></returns>
        public static HttpClient CreateHttpClient(string url, IDictionary<string, string> cookies = null)
        {
            HttpClient httpclient;
            HttpClientHandler handler = new HttpClientHandler();
            var uri = new Uri(url);
            if (cookies != null)
            {
                foreach (var key in cookies.Keys)
                {
                    string one = key + "=" + cookies[key];
                    handler.CookieContainer.SetCookies(uri, one);
                }
            }
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                httpclient = new HttpClient(handler);
            }
            else
            {
                httpclient = new HttpClient(handler);
            }
            return httpclient;
        }
        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="jsonData">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, string jsonData)
        {
            HttpClient httpClient = CreateHttpClient(url);
            var postData = new StringContent(jsonData, Encoding.UTF8);
            //postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Task<string> result = httpClient.PostAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }

        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values">键值对</param>
        /// <returns></returns>
        public static string Post(string url, IEnumerable<KeyValuePair<string, string>> values)
        {
            HttpClient httpClient = CreateHttpClient(url);
            var postData = new FormUrlEncodedContent(values);
            postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            
            //postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Task<string> result = httpClient.PostAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }

        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="req">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, byte[] req)
        {
            HttpClient httpClient = CreateHttpClient(url);
            var postData = new ByteArrayContent(req);
            Task<string> result = httpClient.PostAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }

        /// <summary>
        /// get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            HttpClient httpClient = CreateHttpClient(url);
            Task<string> result = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
            return result.Result;
        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        public static void HttpDownloadFile(string url, string path, Action<int> method)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();

            long totalBytes = response.ContentLength;

            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            long count = size;
            int current = 0;
            while (size > 0)
            {
                count += size;
                int prec = (int)((count * 100) / totalBytes);
                if (prec > current)
                {
                    current = prec;
                    method(prec);
                }
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
        }

        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string param = null, string contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request;

            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = contentType;//"application/json"
            request.Accept = "*/*";
            request.Timeout = 30000;
            request.ServicePoint.ConnectionLimit = int.MaxValue;
            request.AllowAutoRedirect = false;

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(param);
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }

            return responseStr;
        }
        #region 头部带参数Token请求 - 工程师物料
        /// <summary>
        /// get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Get_EM(string url, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(url);
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Add("Token", token);
            }
            Task<string> result = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
            return result.Result;
        }
        /// <summary>
        /// EM post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="jsonData">请求参数</param>
        /// <returns></returns>
        public static string Post_EM(string url, string jsonData, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(url);
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Add("Token", token);
            }
            var postData = new StringContent(jsonData, Encoding.UTF8);

            postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Task<string> result = httpClient.PostAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }
        /// <summary>
        /// EM post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="jsonData">请求参数</param>
        /// <returns></returns>
        public static string DELETE_EM(string url, string jsonData, string token = "")
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "DELETE";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            request.Timeout = 30000;
            request.ServicePoint.ConnectionLimit = int.MaxValue;
            request.AllowAutoRedirect = false;
            request.Headers.Add("Token", token);

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(jsonData);
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return responseStr;
        }
        /// <summary>
        /// EM post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="jsonData">请求参数</param>
        /// <returns></returns>
        public static string PUT_EM(string url, string jsonData, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(url);
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Add("Token", token);
            }
            var postData = new StringContent(jsonData, Encoding.UTF8);

            postData.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Task<string> result = httpClient.PutAsync(url, postData).Result.Content.ReadAsStringAsync();
            return result.Result;
        }
        #endregion
    }
}
