using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZTGJWechatModel.API
{
    /// <summary>
    /// 运单查询返回参数
    /// </summary>
    public class WayBillList_Response : ApiBase_Response
    {
        public string data { set; get; }
        public List<WayBillListItem> dataList {
            get {
                return JsonConvert.DeserializeObject<List<WayBillListItem>>(data);
            }
        }
    }

    public class WayBillListItem
    {
        /// <summary>
        /// 工作单号
        /// </summary>
        public string JobCode { set; get; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { set; get; }
        /// <summary>
        /// 运输公司
        /// </summary>
        public string ExpressCompany { set; get; }
        /// <summary>
        /// 寄件人
        /// </summary>
        public string Sender { set; get; }
        /// <summary>
        /// 寄件城市
        /// </summary>
        public string ShippingCity { set; get; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver { set; get; }
        /// <summary>
        /// 收件城市
        /// </summary>
        public string ReceivingCity { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { set; get; }
        /// <summary>
        /// 运输状态
        /// </summary>
        public string StatusText { set; get; }
        public string EstimatedServiceTime { set; get; }
        public int rowNumber { set; get; }
    }

}
