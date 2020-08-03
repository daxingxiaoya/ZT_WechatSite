using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    /// <summary>
	/// 收藏和足跡 实体类
	/// </summary>
	[Serializable]
    public partial class CollectAndFootPrintModel
    {
        public CollectAndFootPrintModel()
        { }
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
        private string _imgurl = "";
        private string _createdate = "";
        private DateTime _createtime = DateTime.Now;
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
        /// 供应商
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
        /// 序列号
        /// </summary>
        public string SerialNumber
        {
            set { _serialnumber = value; }
            get { return _serialnumber; }
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber
        {
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
        /// 备件封面图片地址
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        
        /// <summary>
        /// 创建日期 yyyy-MM-dd
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}
