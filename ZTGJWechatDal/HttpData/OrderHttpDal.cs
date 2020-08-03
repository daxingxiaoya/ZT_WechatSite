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
    public class OrderHttpDal
    {
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public OrderInfo_Response OrderInfo(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.orderinfo, req);
            return JsonConvert.DeserializeObject<OrderInfo_Response>(res);
        }

        /// <summary>
        /// 微信下单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public CreateOrder_Response CreateOrder(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.createorder, req);
            return JsonConvert.DeserializeObject<CreateOrder_Response>(res);
        }

    }
}
