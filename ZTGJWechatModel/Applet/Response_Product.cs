using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    public class Response_Product
    {
        public string VendorName { set; get; }
        public string ProductCode { set; get; }
        public string ProductName { set; get; }
        public string ProductDescrEN { set; get; }
        public string ModelCode { set; get; }
        public string QuantityUnitCH { set; get; }
        public string Remark { set; get; }
        public string MadeInCH { set; get; }
        public string SpecificationModel { set; get; }
        public string IsBatchNumberMatch { set; get; }
        public string QuantityUnitConvertCH { set; get; }
        public string MadeInEN { set; get; }
        public string PackageCH { set; get; }
        public string QuantityUnitConvertEN { set; get; }
        public string ProductDescrCH { set; get; }
        public List<string> ImgUrl { set; get; }

        #region 扩展
        /// <summary>
        /// 库存
        /// </summary>
        public string StockQty { set; get; }
        /// <summary>
        /// 购物车数量
        /// </summary>
        public int cartGoodsCount { set; get; }
        /// <summary>
        /// 是否收藏 0未收藏 1收藏
        /// </summary>
        public int IsCollect { set; get; }
        #endregion
    }
}
