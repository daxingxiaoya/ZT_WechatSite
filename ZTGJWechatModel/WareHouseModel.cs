using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel
{
    /// <summary>
	/// warehouse:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    public partial class WareHouseModel
    {
        public WareHouseModel()
        { }
        #region Model
        private int _id;
        private string _name = "";
        private string _address = "";
        private string _phone = "";
        private string _telphone = "";
        private string _email = "";
        private string _facsimile = "";
        private string _postalcode = "";
        private string _remark = "";
        private int _sortnum = 0;
        private DateTime _createtime = DateTime.Now;
        private DateTime _updatetime;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string Facsimile
        {
            set { _facsimile = value; }
            get { return _facsimile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostalCode
        {
            set { _postalcode = value; }
            get { return _postalcode; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
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

    }
}
