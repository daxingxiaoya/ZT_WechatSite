using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.Order;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Applet
{
    public class CollectAndFootPrintBll
    {
        private CollectAndFootPrintDal cfdal = new CollectAndFootPrintDal();

        #region 添加足迹
        /// <summary>
        /// 添加足迹
        /// </summary>
        /// <param name="proreq"></param>
        public string AddFootPrint(string reqdata)
        {
            string res = "";
            try
            {
                CollectAndFootPrintModel proreq = JsonConvert.DeserializeObject<CollectAndFootPrintModel>(reqdata);
                proreq.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
                proreq.CreateTime = DateTime.Now;
                bool s = cfdal.AddCollectOrFootPrint(proreq, "FootPrint");

                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", addstate = s });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("添加足迹异常：" + ex.Message + "，" + ex.StackTrace + "，参数：" + reqdata);
            }
            return res;
        }
        #endregion

        /// <summary>
        /// 添加或取消收藏
        /// </summary>
        /// <param name="reqdata"></param>
        /// <param name="type">0添加 1取消</param>
        /// <returns></returns>
        public string AddOrCancelCollect(string reqdata, string type)
        {
            string res = "";
            try
            {
                CollectAndFootPrintModel proreq = JsonConvert.DeserializeObject<CollectAndFootPrintModel>(reqdata);
                proreq.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
                proreq.CreateTime = DateTime.Now;
                bool resb = false;
                if (type == "0")//添加收藏
                {
                    resb = cfdal.AddCollectOrFootPrint(proreq, "Collect");
                }
                else//取消收藏
                {
                    resb = cfdal.DelCollectOrFootPrint(proreq, "Collect");
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", addstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("添加取消收藏异常：" + ex.Message + "，" + ex.StackTrace + "，参数：" + reqdata);
            }
            return res;
        }

        /// <summary>
        /// 删除足迹
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string DelFootPrint(string reqdata)
        {
            string res = "";
            try
            {
                CollectAndFootPrintModel proreq = JsonConvert.DeserializeObject<CollectAndFootPrintModel>(reqdata);
                bool resb = cfdal.DelCollectOrFootPrint(proreq, "FootPrint");
                
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", addstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("添加取消收藏异常：" + ex.Message + "，" + ex.StackTrace + "，参数：" + reqdata);
            }
            return res;
        }

        /// <summary>
        /// 获取用户足迹或收藏 带分页
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="type">0收藏 1足迹</param>
        /// <returns></returns>
        public string GetCOrFByPage(string uid, int index, int pagesize, int type)
        {
            string res = "";
            try
            {
                int totalcount = 0;
                List<CollectAndFootPrintModel> list = cfdal.GetCOrFByPage(uid, out totalcount,(type==0? "Collect" : "FootPrint"), index, pagesize);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = totalcount, rows = list });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, count = 0, msg = "系统故障" });
                LogHelper.ErrorLog("查询收藏或者足迹异常：" + ex.Message + "," + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 获取用户收藏 带分页
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="type">0收藏 1足迹</param>
        /// <returns></returns>
        public string GetCollectByPage(string uid, int index, int pagesize)
        {
            string res = "";
            try
            {
                int totalcount = 0;
                List<CollectAndFootPrintModel> list = cfdal.GetCOrFByPage(uid, out totalcount, "Collect", index, pagesize);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = totalcount, rows = list });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, count = 0, msg = "系统故障" });
                LogHelper.ErrorLog("查询收藏或者足迹异常：" + ex.Message + "," + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 获取用户足迹 带分页
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="type">0收藏 1足迹</param>
        /// <returns></returns>
        public string GetFootPrintByPage(string uid)
        {
            string res = "";
            try
            {
                int totalcount = 0;
                List<CollectAndFootPrintModel> list = cfdal.GetCOrFByPage(uid, out totalcount, "FootPrint");
                var datearr = list.GroupBy(g => g.CreateDate);
                List<FootPrintOutModel> outlist = new List<FootPrintOutModel>();
                foreach (var item in datearr)
                {
                    FootPrintOutModel outm = new FootPrintOutModel();
                    outm.CreateDate = item.FirstOrDefault().CreateDate;
                    outm.footlist = new List<CollectAndFootPrintModel>();
                    foreach (var sitem in item)
                    {
                        CollectAndFootPrintModel m = sitem;
                        outm.footlist.Add(m);
                    }
                    outlist.Add(outm);
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = totalcount, rows = outlist });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, count = 0, msg = "系统故障" });
                LogHelper.ErrorLog("查询收藏或者足迹异常：" + ex.Message + "," + ex.StackTrace);
            }
            return res;
        }

    }
}
