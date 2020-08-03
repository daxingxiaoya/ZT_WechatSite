using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.Express;
using ZTGJWechatDal.HttpData;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Common
{
    public class ExpressBll
    {
        private ExpressDal expdal = new ExpressDal();
        private ExpressHttpDal exphttpdal = new ExpressHttpDal();

        #region 来源于微信数据库
        /// <summary>
        /// 根据uid获取快递查询历史
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="modular">内容模块 express:快递,reservoirarea:库区</param>
        /// <returns></returns>
        public string GetSearchHistory(string uid, string modular)
        {
            string res = string.Empty;
            try
            {
                List<SearchHistory> hislist = expdal.GetExpressSearchHistory(uid);
                if (hislist.Count > 0)
                {
                    hislist = hislist.Where(h => h.modular == modular).ToList();//根据模块内容查
                    hislist = hislist.GroupBy(p => new { p.keyword })
                                    .Select(g => g.First())
                                    .ToList();//去重
                    hislist = hislist.Take(5).ToList();//取前五条历史
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = hislist.Count, data = hislist });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = 0 });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 新增搜索历史
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddSearchHistory(SearchHistory model) {
            return expdal.AddExpressSearchHistory(model);
        }
        #endregion

        #region 来源于三方
        /// <summary>
        /// 根据手机号获取运单列表
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string WaybillList(string reqdata)
        {
            string res = string.Empty;
            try
            {
                WayBillList_Request req = JsonConvert.DeserializeObject<WayBillList_Request>(reqdata);
                req.key = AppSettingUtil.InsideApiKey2;
                //req.PhoneNumber = req.PhoneNumber.Replace("15827002712", "18717764701");
                WayBillList_Response reswbl = exphttpdal.WaybillList(JsonConvert.SerializeObject(req));
                if (reswbl.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = reswbl.dataList.Count, data = reswbl.dataList });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = reswbl.msg, count = 0 });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 根据运单号获取运单详情
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string WaybillInfo(string reqdata)
        {
            string res = string.Empty;
            try
            {
                WayBillList_Request req = JsonConvert.DeserializeObject<WayBillList_Request>(reqdata);
                req.key = AppSettingUtil.InsideApiKey2;
                //req.PhoneNumber = req.PhoneNumber.Replace("15827002712", "18717764701");
                WayBillInfo_Response reswbl = exphttpdal.WaybillInfo(JsonConvert.SerializeObject(req));
                if (reswbl.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = reswbl.datalist.Count, data = reswbl.datalist });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = reswbl.msg, count = 0 });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 运单追踪
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string WaybillTrack(string reqdata)
        {
            string res = string.Empty;
            try
            {
                WayBillList_Request req = JsonConvert.DeserializeObject<WayBillList_Request>(reqdata);
                req.key = AppSettingUtil.InsideApiKey2;
                //req.PhoneNumber = req.PhoneNumber.Replace("15827002712", "18717764701");
                WaybillTrack_Response reswbl = exphttpdal.WaybillTrack(JsonConvert.SerializeObject(req));
                if (reswbl.code == 200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = reswbl.datalist.Count, data = reswbl.datalist });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = reswbl.msg, count = 0 });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        #endregion

    }
}
