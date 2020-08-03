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
    public class StockHttpDal
    {
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public StockInfo_Response stockinfo(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.stockinfo, req);
            return JsonConvert.DeserializeObject<StockInfo_Response>(res);
        }
        /// <summary>
        /// 备件基础信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Product_Response product(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.product, req);
            return JsonConvert.DeserializeObject<Product_Response>(res);
        }

    }
}
