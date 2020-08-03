using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class OrderInfo_Request : ApiBase_Request
    {
        public OrderInfo_Request() {
            keyValue = "";
            OrderType = 0;
            OrderBy = "";
            Sort = "";
            Page = 0;
            PageSize = 0;
        }
        public string keyValue { set; get; }
        /// <summary>
        /// 订单状态 0全部 1处理中 2已完成
        /// </summary>
        public int OrderType { set; get; }
        public string OrderBy { set; get; }
        public string Sort { set; get; }
        public int Page { set; get; }
        public int PageSize { set; get; }
    }
}
    