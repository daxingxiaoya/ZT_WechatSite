using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTGJWechatBll.Bussiness;
using ZTGJWechatModel.Bussiness;
using ZTGJWechatUtils;

namespace ZTGJWechatWebsite.Controllers.Bussiness
{
    public class WechatController : Controller
    {
        #region 默认构造
        private WechatBll wechatbll = new WechatBll();
        string keypromptmsg = "你没有此接口的权限";
        #endregion

        /// <summary>
        /// 根据来源获取未绑定用户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUnboundUsers()
        {
            string res = "";
            List<Bus_GetUsers_response> responselist = new List<Bus_GetUsers_response>();
            try
            {
                if (IsAuthorityByKey(Request["key"]))
                {
                    string source = Request["source"];//来源  em:工程师管理系统  stock:库存系统
                    responselist = wechatbll.GetUnboundUsers(source);
                    res = JsonConvert.SerializeObject(new { code = 200, msg = "ok", data = responselist });
                }
                else {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = keypromptmsg, data = responselist });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统异常：" + ex.Message, data = responselist });
                LogHelper.ErrorLog("Bussiness==>Wechat==>GetUnboundUsers故障：" + ex.Message + ex.StackTrace);
            }
            return Content(res);
        }
        /// <summary>
        /// 根据来微信UID绑定厂商
        /// </summary>
        /// <returns></returns>
        public ActionResult BindingUsers()
        {
            string res = "";
            List<Bus_GetUsers_response> responselist = new List<Bus_GetUsers_response>();
            try
            {
                if (IsAuthorityByKey(Request["key"]))
                {
                    Bus_BindingUsers_Request req = JsonConvert.DeserializeObject<Bus_BindingUsers_Request>(Request["user"]);//请求的用户参数
                    if (req.source=="em"||req.source=="stock")
                    {
                        if (wechatbll.BindingUsers(req))
                        {
                            res = JsonConvert.SerializeObject(new { code = 200, msg = "ok" });
                        }
                        else
                        {
                            res = JsonConvert.SerializeObject(new { code = 10002, msg = "绑定失败！" });
                        }
                    }
                    else
                    {
                        res = JsonConvert.SerializeObject(new { code = 10002, msg = "参数不符合要求" });
                    }
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = keypromptmsg });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统异常：" + ex.Message });
                LogHelper.ErrorLog("Bussiness==>Wechat==>BindingUsers故障：" + ex.Message + ex.StackTrace + "\r\n请求参数：" + Request["user"]);
            }
            return Content(res);
        }
        #region 公用
        /// <summary>
        /// key是否有权限
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsAuthorityByKey(string key)
        {
            string authoritykey = AppSettingUtil.OutSideKey;
            return key == authoritykey ? true : false;
        }
        #endregion
    }
}