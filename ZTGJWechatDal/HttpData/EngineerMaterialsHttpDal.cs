using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.API.EngineerMaterials;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    public class EngineerMaterialsHttpDal
    {

        #region SysUser
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_WechatLogin_Response EM_WechatLogin(string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_WechatLogin, req);
            return JsonConvert.DeserializeObject<EM_WechatLogin_Response>(res);
        }
        /// <summary>
        /// 修改手机号和微信名称
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_WechatLogin_Response EM_Mobile(string token, string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_Mobile, req, token);
            return JsonConvert.DeserializeObject<EM_WechatLogin_Response>(res);
        }
        #endregion

        #region OldReturnOrder
        /// <summary>
        /// 旧件退回 列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_OldReturnOrder_Response EM_OldReturnOrder(string token, string req)
        {
            string res = HttpMethods.Get_EM(InsideApiUrlUtil.EM_oldreturnorder + req, token);
            return JsonConvert.DeserializeObject<EM_OldReturnOrder_Response>(res);
        }
        /// <summary>
        /// 旧件单详情
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_LineInfo_Response EM_LineInfo(string token, string req)
        {
            string res = HttpMethods.Get_EM(InsideApiUrlUtil.EM_lineinfo + req, token);
            return JsonConvert.DeserializeObject<EM_LineInfo_Response>(res);
        }
        #endregion

        #region Engineer
        /// <summary>
        /// 工程师下拉列表(退回)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_EngineerListReturn_response EM_EngineerListReturn(string token)
        {
            string res = HttpMethods.Get_EM(InsideApiUrlUtil.EM_engineerlistreturn, token);
            return JsonConvert.DeserializeObject<EM_EngineerListReturn_response>(res);
        }
        #endregion

        #region OldRequestReturn
        /// <summary>
        /// 工程师下拉列表(退回)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_OldRequest_Response EM_OldRequest(string token, string req)
        {
            string res = HttpMethods.Get_EM(InsideApiUrlUtil.EM_oldrequest + req, token);
            return JsonConvert.DeserializeObject<EM_OldRequest_Response>(res);
        }
        #endregion

    }
}
