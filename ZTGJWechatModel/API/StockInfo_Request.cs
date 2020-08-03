using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class StockInfo_Request : ApiBase_Request
    {
        public List<ProLocListItem> list { set; get; }
        ///// <summary>
        ///// 页码
        ///// </summary>
        //public string Page { set; get; }
        ////大小
        //public string PageSize { set; get; }
    }

    public class ProLocListItem {
        /// <summary>
        /// 备件号
        /// </summary>
        public string ProductCode { set; get; }
        /// <summary>
        /// 库区集合
        /// </summary>
        public string LocationNames { set; get; }
    }
}
