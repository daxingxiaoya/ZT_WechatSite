using System;


namespace ZTGJWechatModel
{
    /// <summary>
    /// ControlSwitch:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GlobalConfiguration
    {
        public GlobalConfiguration()
        { }
        #region Model
        private int _id;
        private string _controlkey = "";
        private string _controvalue;
        private string _remark = "";
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
        /// <summary>
        /// key
        /// </summary>
        public string ControlKey
        {
            set { _controlkey = value; }
            get { return _controlkey; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string ControValue
        {
            set { _controvalue = value; }
            get { return _controvalue; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string StatusTxt
        {
            get { return Status==0?"正常":"已删除"; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }
}


