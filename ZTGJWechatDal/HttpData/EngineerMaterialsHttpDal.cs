using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.API;
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
        /// 删除退件
        /// </summary>
        /// <returns></returns>
        public EM_OldReturnOrder_Response EM_OldReturnOrder_Del(string token, string req)
        {
            string res = HttpMethods.DELETE_EM(InsideApiUrlUtil.EM_oldreturnorder_del, JsonConvert.SerializeObject(new { ids = req.Split(',') }), token);
            return JsonConvert.DeserializeObject<EM_OldReturnOrder_Response>(res);
        }
        /// <summary>
        /// 旧件单详情 （行详情）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_LineInfo_Response EM_LineInfo(string token, string req)
        {
            string res = HttpMethods.Get_EM(InsideApiUrlUtil.EM_lineinfo + req, token);
            return JsonConvert.DeserializeObject<EM_LineInfo_Response>(res);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <returns></returns>
        public EM_OldReturnOrder_Response EM_OldReturnOrder_DeleteLine(string token, string req)
        {
            string res = HttpMethods.DELETE_EM(InsideApiUrlUtil.EM_oldreturnorder_deleteline, JsonConvert.SerializeObject(new { ids = req.Split(',') }), token);
            return JsonConvert.DeserializeObject<EM_OldReturnOrder_Response>(res);
        }
        /// <summary>
        /// 添加退件
        /// </summary>
        /// <returns></returns>
        public EM_OldReturnOrder_Add_Response EM_OldReturnOrder_Add(string token, string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_oldreturnorder, req, token);
            return JsonConvert.DeserializeObject<EM_OldReturnOrder_Add_Response>(res);
        }
        /// <summary>
        /// 更新退件
        /// </summary>
        /// <returns></returns>
        public EM_OldReturnOrder_Add_Response EM_OldReturnOrder_PUT(string token, string req)
        {
            string res = HttpMethods.PUT_EM(InsideApiUrlUtil.EM_oldreturnorder, req, token);
            return JsonConvert.DeserializeObject<EM_OldReturnOrder_Add_Response>(res);
        }
        /// <summary>
        /// 发货
        /// </summary>
        /// <returns></returns>
        public ApiBase_Response EM_OldReturnOrder_UpdateSend(string token, string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_oldreturnorder_updatesend, JsonConvert.SerializeObject(new { ids = req.Split(',') }), token);
            return JsonConvert.DeserializeObject<ApiBase_Response>(res);
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
        /// 指令列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_OldRequest_Response EM_OldRequest(string token, string req)
        {
            string res = HttpMethods.Get_EM(InsideApiUrlUtil.EM_oldrequest + req, token);
            return JsonConvert.DeserializeObject<EM_OldRequest_Response>(res);
        }
        /// <summary>
        /// 删除退件指令
        /// </summary>
        /// <returns></returns>
        public EM_OldRequest_Response EM_OldRequest_Del(string token, string req)
        {
            string res = HttpMethods.DELETE_EM(InsideApiUrlUtil.EM_oldrequest, JsonConvert.SerializeObject(new { ids= req.Split(',') }), token);
            return JsonConvert.DeserializeObject<EM_OldRequest_Response>(res);
        }
        /// <summary>
        /// 添加退件指令
        /// </summary>
        /// <returns></returns>
        public EM_OldRequest_Add_Response EM_OldRequest_Add(string token, string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_oldrequest, req, token);
            return JsonConvert.DeserializeObject<EM_OldRequest_Add_Response>(res);
        }
        /// <summary>
        /// 更新退件指令
        /// </summary>
        /// <returns></returns>
        public EM_OldRequest_Add_Response EM_OldRequest_Up(string token, string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_oldrequest, req, token);
            return JsonConvert.DeserializeObject<EM_OldRequest_Add_Response>(res);
        }
        #endregion

    }
}
