using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.Common;

namespace ZTGJWechatModel
{
    /// <summary>
    /// 用户授权信息
    /// </summary>
    public class UserEmpower : StatusBase
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string unionid { set; get; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int empowerStatus { set; get; }
        /// <summary>
        /// 小程序菜单权限
        /// </summary>
        public string powerApMenu { set; get; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string companyname { set; get; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string nickname { set; get; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatarUrl { set; get; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobilephone { set; get; }
        
    }
}
