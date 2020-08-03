using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    /// <summary>
    /// 运单查询请求参数
    /// </summary>
    public class WayBillList_Request : ApiBase_Request
    {
        public int Page { set; get; }
        public int PageSize { set; get; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { set; get; }
        /// <summary>
        /// 运输公司
        /// </summary>
        public string ExpressCompany { set; get; }
    }
}
