using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTGJWechatBll.BackStage;
using ZTGJWechatBll.Common;
using ZTGJWechatModel;
using ZTGJWechatUtils;

namespace ZTGJWechatWebsite.Controllers.BackStage
{
    public class BSConfigController : Controller
    {
        #region 页面
        /// <summary>
        /// 公众号
        /// </summary>
        /// <returns></returns>
        public ActionResult WeChatOfficialAccoun()
        {
            var userInfo = BSUserBll.LoginUserInfo;
            if (userInfo == null)
            {
                return View("../Home/Login");
            }
            ViewBag.LoginAccount = userInfo.Account;
            return View();
        }
        /// <summary>
        /// 全局配置
        /// </summary>
        /// <returns></returns>
        public ActionResult GlobalConfiguration()
        {
            var userInfo = BSUserBll.LoginUserInfo;
            if (userInfo == null)
            {
                return View("../Home/Login");
            }
            ViewBag.LoginAccount = userInfo.Account;
            return View();
        }
        #endregion

        private GlobalConfigurationBll gcbll = new GlobalConfigurationBll();

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public string GetGCList(string key = "", int page = 1, int pagesize = 200)
        {
            string res = string.Empty;
            try
            {
                int outtotalcount = 0;
                List<GlobalConfiguration> gclist = gcbll.GetGCByKey("", key, page, pagesize);
                string html = string.Empty;
                for (int i = 0; i < gclist.Count; i++)
                {
                    html += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + " pointer'>";
                    html += "  <td class=\"a-center\"><input type=\"checkbox\" class=\"flat\" value=\"" + gclist[i].Id + "\" name=\"table_records\"></td>";
                    html += "  <td>" + gclist[i].ControlKey + "</td>";
                    html += "  <td>" + gclist[i].ControValue + "</td>";
                    html += "  <td>" + gclist[i].Remark + "</td>";
                    html += "  <td>" + gclist[i].UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>";
                    html += "  <td>" + gclist[i].CreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>";
                    html += "</tr>";
                }
                outtotalcount = gclist.Count;
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = outtotalcount, tablehtml = html });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message, count = 0 });
            }
            return res;
        }

        /// <summary>
        /// 获取配置 根据id
        /// </summary>
        /// <returns></returns>
        public string GetGCById(string id)
        {
            string res = string.Empty;
            try
            {
                List<GlobalConfiguration> gclist = gcbll.GetGCByKey(id);
                if (gclist.Count > 0)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", gc = gclist[0] });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "未查询到id是" + id + "的配置" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message, count = 0 });
            }
            return res;
        }

        /// <summary>
        /// 新增或者修改
        /// </summary>
        /// <returns></returns>
        public string AddOrUpGC(string id, string key, string keyval, string remark)
        {
            string res = string.Empty;
            try
            {
                bool resb = false;
                GlobalConfiguration m = new GlobalConfiguration()
                {
                    ControlKey = key,
                    ControValue = keyval,
                    Remark = remark
                };
                LogHelper.InfoLog("AddOrUpGC初始参数：id=" + id + "，key=" + key + "，keyval=" + keyval + "，remark=" + remark);
                if (!string.IsNullOrEmpty(id))//修改
                {
                    m.Id = Convert.ToInt32(id);
                    resb = gcbll.UpdateGC(m);
                }
                else//新增
                {
                    resb = gcbll.AddGC(m);
                }
                LogHelper.InfoLog("AddOrUpGC参数：" + JsonConvert.SerializeObject(m));
                if (resb)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = (string.IsNullOrEmpty(id) ? "新增" : "修改") + "成功！" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = (string.IsNullOrEmpty(id) ? "新增" : "修改") + "失败！" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message, count = 0 });
            }
            return res;
        }

    }
}