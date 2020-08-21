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
        /// （旧）审核状态
        /// （新）用户小程序使用状态 
        /// </summary>
        public int empowerStatus { set; get; }
        public string empowerStatusTxt
        {
            get
            {
                return empowerStatus == 0 ? "待审核绑定" : "使用中";
            }
        }
        ///// <summary>
        ///// 小程序菜单权限
        ///// </summary>
        //public string powerApMenu { set; get; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string companyname { set; get; }
        /// <summary>
        /// 工程师物料绑定公司
        /// </summary>
        public string emcompany { set; get; }
        /// <summary>
        /// 订单和库存绑定公司
        /// </summary>
        public string stockcompany { set; get; }
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
        /// <summary>
        /// 关注状态 0未关注，1已关注
        /// </summary>
        public int oastatus { set; get; }
    }
}
