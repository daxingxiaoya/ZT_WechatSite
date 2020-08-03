using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Common
{
    /// <summary>
	/// VendCompanyModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class VendCompanyModel
    {
        public VendCompanyModel()
        { }
        #region Model
        private int _id;
        private string _vendname = "";
        private string _fullname = "";
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
        /// 供应商简称
        /// </summary>
        public string VendName
        {
            set { _vendname = value; }
            get { return _vendname; }
        }
        /// <summary>
        /// 供应商全称
        /// </summary>
        public string FullName
        {
            set { _fullname = value; }
            get { return _fullname; }
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
