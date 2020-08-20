using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
    public partial class UsersModel
    {
        public UsersModel()
        { }
        #region Model
        private int _id;
        private string _nickname = "";
        private string _openid = "";
        private string _appletopenid = "";
        private string _unionid = "";
        private string _session_key = "";
        private string _companyname = "";
        private string _emcompany = "";
        private string _stockcompany = "";
        private string _mobilephone = "";
        private int _empowerStatus = 0;
        private string _powerApMenu = "";
        private string _powerReArea = "";
        private string _verificationcode = "";
        private int _sex = 0;
        private string _country = "";
        private string _province = "";
        private string _city = "";
        private string _language = "";
        private string _remark = "";
        private string _headimgurl = "";
        private int _status = 0;
        private string _bindname = "";
        private int _bindstatus = 0;
        private DateTime _createtime = DateTime.Now;
        private DateTime _updatetime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 小程序的openid
        /// </summary>
        public string appletopenid
        {
            set { _appletopenid = value; }
            get { return _appletopenid; }
        }
        /// <summary>
        /// 用户的标识，公众号和小程序唯一
        /// </summary>
        public string unionid
        {
            set { _unionid = value; }
            get { return _unionid; }
        }
        /// <summary>
        /// 小程序的session_key
        /// </summary>
        public string session_key
        {
            set { _session_key = value; }
            get { return _session_key; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string companyname
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 工程师公司名
        /// </summary>
        public string emcompany
        {
            set { _emcompany = value; }
            get { return _emcompany; }
        }
        /// <summary>
        /// 库存公司名
        /// </summary>
        public string stockcompany
        {
            set { _stockcompany = value; }
            get { return _stockcompany; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilephone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 用户授权状态 0待审核 1审核通过 2禁用
        /// </summary>
        public int empowerStatus
        {
            set { _empowerStatus = value; }
            get { return _empowerStatus; }
        }
        /// <summary>
        /// 小程序菜单权限
        /// </summary>
        public string powerApMenu
        {
            set { _powerApMenu = value; }
            get { return _powerApMenu; }
        }
        /// <summary>
        /// 库区权限
        /// </summary>
        public string powerReArea
        {
            set { _powerReArea = value; }
            get { return _powerReArea; }
        }

        /// <summary>
        /// 关注公众号的状态  0未关注  1关注
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 所在国家
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 所在省份
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language
        {
            set { _language = value; }
            get { return _language; }
        }
        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 微信头像地址
        /// </summary>
        public string headimgurl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }
        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode
        {
            set { _verificationcode = value; }
            get { return _verificationcode; }
        }
        /// <summary>
        /// 绑定账号名
        /// </summary>
        public string bindname
        {
            set { _bindname = value; }
            get { return _bindname; }
        }
        /// <summary>
        /// 绑定账号状态  0未绑定 1绑定
        /// </summary>
        public int bindstatus
        {
            set { _bindstatus = value; }
            get { return _bindstatus; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }
}
