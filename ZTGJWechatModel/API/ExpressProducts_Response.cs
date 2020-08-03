using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    /// <summary>
    /// 运单 备件详情
    /// </summary>
    public class ExpressProducts_Response : ApiBase_Response
    {
        public string data { set; get; }
        public List<ExpressProductsItem> dataList
        {
            get
            {
                return JsonConvert.DeserializeObject<List<ExpressProductsItem>>(data);
            }
        }
    }

    public class ExpressProductsItem {
        public string ShipQty { set; get; }
        public string ProductCode { set; get; }
        public string QuantityUnitCH { set; get; }
    }

}
