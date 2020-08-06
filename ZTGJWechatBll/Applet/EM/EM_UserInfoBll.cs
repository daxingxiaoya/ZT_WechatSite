using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.HttpData;
using ZTGJWechatModel.API.EngineerMaterials;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Applet.EM
{
    public class EM_UserInfoBll
    {
        private EngineerMaterialsHttpDal emhttpdal = new EngineerMaterialsHttpDal();

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_WechatLogin(string req)
        {
            string res = "";
            try
            {
                string msgstr = "";
                var response = emhttpdal.EM_WechatLogin(req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = response.msg, token = response.Token });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg, token = "" });
                    LogHelper.WarnLog(msgstr + "\r\n请求参数：reqdata==>" + req);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_WechatLogin异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 修改手机号和微信名称
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_UpMobileNickName(string token, string req)
        {
            string res = "";
            try
            {
                string msgstr = "";
                var response = emhttpdal.EM_Mobile(token, req);
                if (response.code == 200)
                {
                    msgstr = response.msg;
                    res = JsonConvert.SerializeObject(new { code = 0, msg = msgstr, token = response.Token });
                }
                else
                {
                    msgstr = "修改手机号信息失败：" + response.msg;
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = msgstr });
                    LogHelper.WarnLog(msgstr + "\r\n请求参数：Token==>" + token + "，reqdata==>" + req);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_UpMobileNickName异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
    }
}
