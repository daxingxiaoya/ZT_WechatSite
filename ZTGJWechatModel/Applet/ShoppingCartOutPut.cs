using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    public class ShoppingCartOutPut
    {
        /// <summary>
        /// 库区
        /// </summary>
        public string LocationName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public List<SubShoppingCartOutPut> ShoppingCartData { set; get; }
    }
    public class SubShoppingCartOutPut
    {
        /// <summary>
        /// 
        /// </summary>
        public string unionid { set; get; }
        /// <summary>
        /// 供应商公司
        /// </summary>
        public string VendName { set; get; }
        /// <summary>
        /// 库区
        /// </summary>
        public string LocationName { set; get; }
        /// <summary>
        /// 备件号
        /// </summary>
        public string ProductCode { set; get; }
        /// <summary>
        /// 商品序列号
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
        /// 库存
        /// </summary>
        public int StockQty { set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnitCH { set; get; }
        /// <summary>
        /// 是否选中 0未选中 1选中
        /// </summary>
        public int Ischecked { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int number { set; get; }
        /// <summary>
        /// 备件封面图片地址
        /// </summary>
        public string ImgUrl { set; get; }

    }
}
