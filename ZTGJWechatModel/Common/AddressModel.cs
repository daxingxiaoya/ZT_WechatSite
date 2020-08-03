using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Common
{
    public partial class AddressModel
    {
        public AddressModel()
        { }
        #region Model
        private int _id;
        private string _unionid = "";
        private string _name = "";
        private string _phone = "";
        private string _country = "";
        private string _province = "";
        private string _city = "";
        private string _area = "";
        private string _address = "";
        private string _alias = "";
        private int _isdefault = 0;
        private int _status = 0;
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
        public string unionid {
            set { _unionid = value; }
            get { return _unionid; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 区县
        /// </summary>
        public string area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 地址别名
        /// </summary>
        public string alias
        {
            set { _alias = value; }
            get { return _alias; }
        }
        /// <summary>
        /// 是否为默认地址 0不是 1是
        /// </summary>
        public int isdefault {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
        /// <summary>
        /// 地址类型：0收件地址 1寄件地址
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
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
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }
}
