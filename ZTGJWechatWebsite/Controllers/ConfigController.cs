using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using ZTGJWechatBll;
using ZTGJWechatUtils;

namespace ZTGJWechatWebsite.Controllers
{
    public class ConfigController : Controller
    {
        #region 默认构造器
        private TokenBll tokenbll = new TokenBll();

        #endregion

        #region 公众号菜单设置
        /// <summary>
        /// 读取并创建自定义菜单
        /// </summary>
        [HttpGet]
        public void AddMenu()
        {
            string res = "";
            try
            {
                //公众号菜单文件路径
                string jsonfile = System.Web.HttpContext.Current.Request.MapPath("\\config\\menu.json");
                //读取Json菜单
                string menu = CommonHelper.Jsonstr(jsonfile);
                //获取access_token
                string access_token = tokenbll.GetOAAccessToken();
                //创建地址
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", access_token);
                //返回结果网页（html）代码
                string strMsg = Implement(url, "POST", menu);
                res = "创建菜单结果：" + strMsg;
                Response.Write(res);
                Response.End();
            }
            catch (Exception ex)
            {
                res = ex.Message;
                LogHelper.ErrorLog("AddMenu异常：" + ex.Message + ",追踪：" + ex.StackTrace);
            }
            //return res;
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public void DeleteMenu()
        {
            try
            {
                // 获取access_token
                string access_token = tokenbll.GetOAAccessToken();
                // 清理菜单地址
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", access_token);
                //返回结果网页（html）代码
                string strMsg = Implement(url, "GET", "");
                Response.Write("删除菜单结果：" + strMsg);
                Response.End();
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("DeleteMenu异常：" + ex.Message + ",追踪：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="reqtype">请求类型  GET/POST</param>
        /// <param name="data">菜单数据</param>
        /// <returns></returns>
        private static string Implement(string url, string reqtype, string data)
        {
            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.Method = reqtype;
            hwr.ContentType = "application/x-www-form-urlencoded";
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(data);

            if (!string.IsNullOrEmpty(data))
            {
                hwr.ContentLength = payload.Length;
                Stream writer = hwr.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();
            }

            var result = hwr.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(result.GetResponseStream(), Encoding.UTF8);
            //返回结果网页（html）代码
            string strMsg = sr.ReadToEnd();

            return strMsg;
        }
        #endregion

        #region ClearCacheAccessToken
        public string ClearCacheAccessToken()
        {
            try
            {
                tokenbll.ClearCacheAccessToken();
                return "seccess";
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("清理Token缓存异常：" + ex.Message + ",追踪：" + ex.StackTrace);
                return ex.Message;
            }

        }
        #endregion

        

    }
}