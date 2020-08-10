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
    public class EM_OldReturnBll
    {
        private EngineerMaterialsHttpDal emhttpdal = new EngineerMaterialsHttpDal();
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
        /// 旧件退回 列表
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
        /// 旧件退回 列表
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
    }
}
