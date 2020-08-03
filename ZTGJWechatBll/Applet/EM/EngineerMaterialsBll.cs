﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Applet.EM
{
    public class EngineerMaterialsBll
    {
        private EM_UserInfoBll ubll = new EM_UserInfoBll();

        public string MethodIssue(string method, string token, string reqdata)
        {
            string res = "";
            try
            {
                switch (method)
                {
                    case "EMWechatLogin":
                        res = ubll.EM_WechatLogin(reqdata);
                        break;
                    case "EMUpMobileNickName":
                        res = ubll.EM_UpMobileNickName(token, reqdata);
                        break;
                    default:
                        res = JsonConvert.SerializeObject(new { code = 10002, msg = "未找到方法名：" + method });
                        break;
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障"});
                LogHelper.ErrorLog("EngineerMaterialsBll==>MethodIssue异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }




    }
}