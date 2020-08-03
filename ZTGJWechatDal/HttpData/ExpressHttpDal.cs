using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.API;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    /// <summary>
    /// 物流接口
    /// </summary>
    public class ExpressHttpDal
    {
        /// <summary>
        /// 运单查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public WayBillList_Response WaybillList(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.waybilllist, req);
            return JsonConvert.DeserializeObject<WayBillList_Response>(res);
        }

        /// <summary>
        /// 运单详情
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public WayBillInfo_Response WaybillInfo(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.waybillinfo, req);
            return JsonConvert.DeserializeObject<WayBillInfo_Response>(res);
        }

        /// <summary>
        /// 运单跟踪
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public WaybillTrack_Response WaybillTrack(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.waybilltrack, req);
            return JsonConvert.DeserializeObject<WaybillTrack_Response>(res);
        }

        /// <summary>
        /// 运单号查询备件详情
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public ExpressProducts_Response ExpressProducts(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.expressproducts, req);
            return JsonConvert.DeserializeObject<ExpressProducts_Response>(res);
        }

    }
}
