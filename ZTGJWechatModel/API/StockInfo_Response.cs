using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class StockInfo_Response : ApiBase_Response
    {
        public List<StockInfoItem> data { set; get; }
    }
    public class StockInfoItem
    {
        /// <summary>
        /// 备件号
        /// </summary>
        public string ProductCode { set; get; }
        /// <summary>
        /// 库存
        /// </summary>
        public string StockQty { set; get; }
        public string StockQtyInt { get {
                return Convert.ToDecimal(StockQty).ToString("#0");
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnitCH { set; get; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { set; get; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { set; get; }
        /// <summary>
        /// 英文描述
        /// </summary>
        public string ProductDescrEN { set; get; }
        /// <summary>
        /// 库区
        /// </summary>
        public string LocationName { set; get; }
        public string ImgUrl { set; get; }

    }
}
