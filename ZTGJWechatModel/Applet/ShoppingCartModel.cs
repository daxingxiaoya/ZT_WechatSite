using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel()
        {
        }
        #region Model
        private int _id;
        private string _unionid = "";
        private string _vendname = "";
        private string _locationname = "";
        private string _productcode = "";
        private string _serialnumber = "";
        private string _batchnumber = "";
        private string _productname = "";
        private string _productdescren = "";
        private string _productdescrch = "";
        private int _stockqty = 0;
        private string _quantityunitch = "";
        private int _ischecked = 0;
        private int _number = 0;
        private string _imgurl = "";
        private DateTime _createtime = DateTime.Now;
        private DateTime _updatetime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string unionid
        {
            set { _unionid = value; }
            get { return _unionid; }
        }
        /// <summary>
        /// 供应商公司
        /// </summary>
        public string VendName
        {
            set { _vendname = value; }
            get { return _vendname; }
        }
        /// <summary>
        /// 库区
        /// </summary>
        public string LocationName
        {
            set { _locationname = value; }
            get { return _locationname; }
        }
        /// <summary>
        /// 备件号
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 商品序列号
        /// </summary>
        public string SerialNumber { 
            set { _serialnumber = value; }
            get { return _serialnumber; }
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber {
            set { _batchnumber = value; }
            get { return _batchnumber; }
        }
        /// <summary>
        /// 备件名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 英文描述
        /// </summary>
        public string ProductDescrEN
        {
            set { _productdescren = value; }
            get { return _productdescren; }
        }
        /// <summary>
        /// 中文描述
        /// </summary>
        public string ProductDescrCH
        {
            set { _productdescrch = value; }
            get { return _productdescrch; }
        }
        /// <summary>
        /// 库存
        /// </summary>
        public int StockQty
        {
            set { _stockqty = value; }
            get { return _stockqty; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnitCH
        {
            set { _quantityunitch = value; }
            get { return _quantityunitch; }
        }
        /// <summary>
        /// 是否选中 0未选中 1选中
        /// </summary>
        public int Ischecked
        {
            set { _ischecked = value; }
            get { return _ischecked; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 备件封面图片地址
        /// </summary>
        public string ImgUrl {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

        #region 用于添加或者删除的参数
        /// <summary>
        /// 添加类型 0为添加 1为删除
        /// </summary>
        public int addtype { set; get; }
        /// <summary>
        /// pro:备件详情页 cart:购物车
        /// </summary>
        public string page { set; get; }
        /// <summary>
        /// 数量（添加购物车用）
        /// </summary>
        public int num { set; get; }
        #endregion
    }
}
