using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.HttpData;
using ZTGJWechatModel.API;
using ZTGJWechatModel.API.EngineerMaterials;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Applet.EM
{
    public class EM_OldReturnBll
    {
        private EngineerMaterialsHttpDal emhttpdal = new EngineerMaterialsHttpDal();

        #region OldReturnOrder
        /// <summary>
        /// 旧件退回 列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldReturnOrder(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldReturnOrder_Request orreq = JsonConvert.DeserializeObject<EM_OldReturnOrder_Request>(req);
                string orreqstr = "?pageIndex=" + orreq.pageIndex + "&pageSize=" + orreq.pageSize;
                if (!string.IsNullOrEmpty(orreq.extension))
                {
                    orreqstr += "&extension=" + orreq.extension;
                }
                if (!string.IsNullOrEmpty(orreq.search))
                {
                    orreqstr += "&search=" + orreq.search;
                }
                if (!string.IsNullOrEmpty(orreq.start))
                {
                    orreqstr += "&start=" + orreq.start;
                }
                if (!string.IsNullOrEmpty(orreq.end))
                {
                    orreqstr += "&end=" + orreq.end;
                }

                EM_OldReturnOrder_Response response = emhttpdal.EM_OldReturnOrder(token, orreqstr);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", total = response.data.Total, rows = response.data.List });
                }
                else {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldReturnOrder异常警告：" + response.msg + "，请求参数" + req);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldReturnOrder异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 删除旧件单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldReturnOrder_Del(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldReturnOrder_Response response = emhttpdal.EM_OldReturnOrder_Del(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldReturnOrder_Del异常警告：" + response.msg + "，请求参数" + req);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldReturnOrder_Del异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 旧件单详情
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_LineInfo(string token, string req)
        {
            string res = "";
            try
            {
                EM_LineInfo_Request orreq = JsonConvert.DeserializeObject<EM_LineInfo_Request>(req);
                string orreqstr = "?headId=" + orreq.headId;
                EM_LineInfo_Response response = emhttpdal.EM_LineInfo(token, orreqstr);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", total = response.data.Count, rows = response.data });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_LineInfo异常警告：" + response.msg + "，请求参数" + req);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_LineInfo异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldReturnOrder_DeleteLine(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldReturnOrder_Response response = emhttpdal.EM_OldReturnOrder_DeleteLine(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldReturnOrder_DeleteLine异常警告：" + response.msg + "，请求参数" + req);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldReturnOrder_DeleteLine异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 新增旧件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldReturnOrder_Add(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldReturnOrder_Add_Response response = emhttpdal.EM_OldReturnOrder_Add(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", data = response.data });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldRequest_Add异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldRequest_Add异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 更新旧件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldReturnOrder_PUT(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldReturnOrder_Add_Response response = emhttpdal.EM_OldReturnOrder_PUT(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", data = response.data });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldReturnOrder_PUT异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldReturnOrder_PUT异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 更新旧件
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldReturnOrder_UpdateSend(string token, string req)
        {
            string res = "";
            try
            {
                ApiBase_Response response = emhttpdal.EM_OldReturnOrder_UpdateSend(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldReturnOrder_UpdateSend异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldReturnOrder_UpdateSend异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        #endregion

        #region Engineer
        /// <summary>
        /// 工程师下拉列表(退回)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_EngineerListReturn(string token)
        {
            string res = "";
            try
            {
                EM_EngineerListReturn_response response = emhttpdal.EM_EngineerListReturn(token);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", edata = response.data });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_EngineerListReturn异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_EngineerListReturn异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        #endregion

        #region OldRequestReturn
        /// <summary>
        /// 指令列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldRequest(string token ,string req)
        {
            string res = "";
            try
            {
                EM_OldRequest_Request orreq = JsonConvert.DeserializeObject<EM_OldRequest_Request>(req);
                string reqstr = "?pageIndex=" + orreq.pageIndex + "&pageSize=" + orreq.pageSize;
                if (!string.IsNullOrEmpty(orreq.search))
                {
                    reqstr += "&search=" + orreq.search;
                }
                if (!string.IsNullOrEmpty(orreq.extension))
                {
                    reqstr += "&extension=" + orreq.extension;
                }
                if (!string.IsNullOrEmpty(orreq.start))
                {
                    reqstr += "&start=" + orreq.start;
                }
                if (!string.IsNullOrEmpty(orreq.end))
                {
                    reqstr += "&end=" + orreq.end;
                }
                EM_OldRequest_Response response = emhttpdal.EM_OldRequest(token, reqstr);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", total = response.data.Total, rows = response.data.List });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldRequest异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldRequest异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 删除指令
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldRequest_Del(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldRequest_Response response = emhttpdal.EM_OldRequest_Del(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldRequest_Del异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldRequest_Del异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 新增指令
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldRequest_Add(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldRequest_Add_Response response = emhttpdal.EM_OldRequest_Add(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", data = response.data });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldRequest_Add异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldRequest_Add异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 修改指令
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EM_OldRequest_Up(string token, string req)
        {
            string res = "";
            try
            {
                EM_OldRequest_Add_Response response = emhttpdal.EM_OldRequest_Up(token, req);
                if (response.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", data = response.data });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg });
                    LogHelper.ErrorLog("EM_OldRequest_Up异常警告：" + response.msg);
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog("EM_OldRequest_Up异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        #endregion

    }
}
