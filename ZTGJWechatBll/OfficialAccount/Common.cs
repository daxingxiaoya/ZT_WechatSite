using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Xml;
using ZTGJWechatModel.OfficialAccount;

namespace ZTGJWechatBll.OfficialAccount
{
    public class Conmmon
    {
        #region unix时间转换为datetime
        /// <summary>  
        /// unix时间转换为datetime  
        /// </summary>  
        /// <param name="timeStamp"></param>  
        /// <returns></returns>  
        public static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion

        #region  datetime转换为unixtime
        /// <summary>  
        /// datetime转换为unixtime  
        /// </summary>  
        /// <param name="time"></param>  
        /// <returns></returns>  
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        #endregion

        #region Post 提交调用抓取
        /// <summary>  
        /// Post 提交调用抓取  
        /// </summary>  
        /// <param name="url">提交地址</param>  
        /// <param name="param">参数</param>  
        /// <returns>string</returns>  
        public static string webRequestPost(string url, string param)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理   

                Stream strm = wr.GetResponseStream();

                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

                string line;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
        }
        #endregion

        #region 调用百度地图，返回坐标信息
        /// <summary>  
        /// 调用百度地图，返回坐标信息  
        /// </summary>  
        /// <param name="y">经度</param>  
        /// <param name="x">纬度</param>  
        /// <returns></returns>  
        public static string GetMapInfo(string x, string y)
        {
            try
            {
                string res = string.Empty;
                string parame = string.Empty;
                string url = "http://maps.googleapis.com/maps/api/geocode/xml";
                parame = "latlng=" + x + "," + y + "&language=zh-CN&sensor=false";//此key为个人申请  
                res = webRequestPost(url, parame);

                XmlDocument doc = new XmlDocument();

                doc.LoadXml(res);
                XmlElement rootElement = doc.DocumentElement;
                string Status = rootElement.SelectSingleNode("status").InnerText;
                if (Status == "OK")
                {
                    //仅获取城市  
                    XmlNodeList xmlResults = rootElement.SelectSingleNode("/GeocodeResponse").ChildNodes;
                    for (int i = 0; i < xmlResults.Count; i++)
                    {
                        XmlNode childNode = xmlResults[i];
                        if (childNode.Name == "status")
                        {
                            continue;
                        }

                        string city = "0";
                        for (int w = 0; w < childNode.ChildNodes.Count; w++)
                        {
                            for (int q = 0; q < childNode.ChildNodes[w].ChildNodes.Count; q++)
                            {
                                XmlNode childeTwo = childNode.ChildNodes[w].ChildNodes[q];

                                if (childeTwo.Name == "long_name")
                                {
                                    city = childeTwo.InnerText;
                                }
                                else if (childeTwo.InnerText == "locality")
                                {
                                    return city;
                                }
                            }
                        }
                        return city;
                    }
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return "0";
            }

            return "0";
        }
        #endregion

        #region 验证微信签名
        /// <summary>  
        /// 验证微信签名  
        /// </summary>  
        /// * 将token、timestamp、nonce三个参数进行字典序排序  
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密  
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
        /// <returns></returns>  
        private static bool CheckSignature(string signature, string timestamp, string nonce, string Token)
        {

            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序  
            string tmpStr = string.Join("", ArrTmp);
#pragma warning disable CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
#pragma warning restore CS0618 // 'FormsAuthentication.HashPasswordForStoringInConfigFile(string, string)' is obsolete: 'The recommended alternative is to use the Membership APIs, such as Membership.CreateUser. For more information, see http://go.microsoft.com/fwlink/?LinkId=252463.'
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string Valid(string signature, string timestamp, string nonce, string Token, string echoStr)
        {
            string NameStr = string.Empty;

            if (CheckSignature(signature, timestamp, nonce, Token))
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    NameStr = echoStr;
                }
            }
            return NameStr;
        }

        #endregion

        #region 封装请求类 
        /// <summary>
        /// 封装请求类 
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public static RequestXML BackRequestXML(string postStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);
            XmlElement rootElement = doc.DocumentElement;

            XmlNode MsgType = rootElement.SelectSingleNode("MsgType");


            RequestXML requestXML = new RequestXML();

            requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
            requestXML.MsgType = MsgType.InnerText;

            if (requestXML.MsgType == "text")
            {
                requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
            }
            else if (requestXML.MsgType == "voice")
            {
                requestXML.Content = rootElement.SelectSingleNode("Recognition").InnerText;
            }
            else if (requestXML.MsgType == "location")
            {
                requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
            }
            else if (requestXML.MsgType == "image")
            {
                requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
            }
            else if (requestXML.MsgType == "event")
            {
                requestXML.Event = rootElement.SelectSingleNode("Event").InnerText;
                if (requestXML.Event == "scancode_waitmsg")
                {
                    requestXML.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                    requestXML.ScanResult = rootElement.SelectSingleNode("ScanCodeInfo").SelectSingleNode("ScanResult").InnerText;
                }
                //if (requestXML.Event != "subscribe")
                //{
                //    requestXML.ScanResult = rootElement.SelectSingleNode("ScanCodeInfo").SelectSingleNode("ScanResult").InnerText;
                //    requestXML.ScanType = rootElement.SelectSingleNode("ScanCodeInfo").SelectSingleNode("ScanType").InnerText;
                //}
            }
            return requestXML;
        }
        #endregion

        #region 去除HTML标记
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="Htmlstring">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public string NoHTML(string Htmlstringaa)
        {
            string Htmlstring = Htmlstringaa;
            try
            {
                //删除脚本
                Htmlstring = Htmlstring.Replace("\r\n", "");
                Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring = Htmlstring.Replace("<", "");
                Htmlstring = Htmlstring.Replace(">", "");
                Htmlstring = Htmlstring.Replace("\r\n", "");
                Htmlstring = Htmlstring.Replace("&ldquo;", "");
                Htmlstring = Htmlstring.Replace("&ldquo", "");
                Htmlstring = Htmlstring.Replace("&rdquo;", "");
                Htmlstring = Htmlstring.Replace("&amp;", "");
                Htmlstring = Htmlstring.Replace("lsquo;", "");
                Htmlstring = Htmlstring.Replace("rsquo;", "");
                Htmlstring = Htmlstring.Replace("hellip;", "");
                Htmlstring = Htmlstring.Replace("mdash;", "");
                Htmlstring = Htmlstring.Replace("&sup2;", "");
                Htmlstring = System.Web.HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            }
            catch
            {

            }
            return Htmlstring;
        }
        #endregion

        #region 获取发出请求，并获取网页源代码
        /// <summary>      
        /// /// 获取发出请求，并获取网页源代码    
        /// /// </summary>     
        /// /// <param name="url"></param>       
        /// /// <param name="encoding">System.Text.Encoding</param>    
        /// /// <returns></returns>     
        public string GetWebHtmlContent(string url)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            string textValue = string.Empty;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = HttpContext.Current.Request.Url.Host.ToString();
                request.Timeout = 20000;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    reader = new StreamReader(response.GetResponseStream(), encoding);
                    string html = reader.ReadToEnd();  // 得到所有HTML标签
                    return html;
                }
            }
            catch { }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                {
                    reader.Close();
                }
                if (request != null)
                {
                    request = null;
                }
            }
            return string.Empty;
        }
        #endregion

        #region 换行
        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="Html"></param>
        /// <returns></returns>
        public string BackContent(string Html)
        {
            string Value = Html;
            Value = Value.Replace("<wx换行>", "\n");
            return Value;
        }
        #endregion


        //获取图片
        public string getPicurl(string url)
        {
            Conmmon cn = new Conmmon();
            string content = cn.GetWebHtmlContent(url);
            string temp = System.Text.RegularExpressions.Regex.Split(content, "var msg_cdn_url", System.Text.RegularExpressions.RegexOptions.IgnoreCase)[1];
            string picurlString = System.Text.RegularExpressions.Regex.Split(temp, "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase)[1];
            return picurlString;
        }
        //获取标题
        public string getTitle(string url)
        {
            Conmmon cn = new Conmmon();
            string content = cn.GetWebHtmlContent(url);
            string temp = System.Text.RegularExpressions.Regex.Split(content, "var msg_title", System.Text.RegularExpressions.RegexOptions.IgnoreCase)[1];
            string tileString = System.Text.RegularExpressions.Regex.Split(temp, "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase)[1];
            return tileString;
        }
        //获取描述
        public String getDesc(string url)
        {
            Conmmon cn = new Conmmon();
            string content = cn.GetWebHtmlContent(url);
            string temp = System.Text.RegularExpressions.Regex.Split(content, "var msg_desc", System.Text.RegularExpressions.RegexOptions.IgnoreCase)[1];
            string descString = System.Text.RegularExpressions.Regex.Split(temp, "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase)[1];
            return descString;
        }
    }
}
