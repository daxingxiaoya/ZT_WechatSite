using Newtonsoft.Json;
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
        private EM_OldReturnBll oldreturnbll = new EM_OldReturnBll();

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

                    #region OldReturnOrder
                    case "EMOldReturnOrder":
                        res = oldreturnbll.EM_OldReturnOrder(token, reqdata);
                        break;
                    case "EMOROrderDel":
                        res = oldreturnbll.EM_OldReturnOrder_Del(token, reqdata);
                        break;
                    case "EMLineInfo":
                        res = oldreturnbll.EM_LineInfo(token, reqdata);
                        break;
                    case "EMOROrderDeleteLine":
                        res = oldreturnbll.EM_OldReturnOrder_DeleteLine(token, reqdata);
                        break;
                    case "EMOROrderAdd":
                        res = oldreturnbll.EM_OldReturnOrder_Add(token, reqdata);
                        break;
                    case "EMOROrderPUT":
                        res = oldreturnbll.EM_OldReturnOrder_PUT(token, reqdata);
                        break;
                    case "EMOROrderUpdateSend":
                        res = oldreturnbll.EM_OldReturnOrder_UpdateSend(token, reqdata);
                        break;
                    case "EMOldReturnOrderExpress":
                        res = oldreturnbll.EM_OldReturnOrder_Express(token);
                        break;
                    #endregion

                    #region Engineer
                    case "EMEngineerListReturn":
                        res = oldreturnbll.EM_EngineerListReturn(token);
                        break;
                    #endregion

                    #region OldRequestReturn
                    case "EMOldRequest":
                        res = oldreturnbll.EM_OldRequest(token, reqdata);
                        break;
                    case "EMOldRequestDel":
                        res = oldreturnbll.EM_OldRequest_Del(token, reqdata);
                        break;
                    case "EMOldRequestAdd":
                        res = oldreturnbll.EM_OldRequest_Add(token, reqdata);
                        break;
                    case "EMOldRequestUp":
                        res = oldreturnbll.EM_OldRequest_Up(token, reqdata);
                        break;
                    #endregion

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
