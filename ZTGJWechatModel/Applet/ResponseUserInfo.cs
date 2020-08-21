using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    [DataContract]
    public class ResponseUserInfo
    {
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int code { get; set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        [DataMember]
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string mobilephone { get; set; }
        /// <summary>
        /// （旧）用户授权状态 0待审核 1审核通过 2禁用 
        /// （新）用户小程序状态 0待审核绑定  1使用中
        /// </summary>
        [DataMember]
        public int empowerStatus { get; set; }
        public string empowerStatusTxt { get {
                return empowerStatus == 0 ? "待审核绑定" : "使用中";
            }
        }
        /// <summary>
        /// 公司名
        /// </summary>
        [DataMember]
        public string companyname { get; set; }
        /// <summary>
        /// 工程师物料绑定公司
        /// </summary>
        [DataMember]
        public string emcompany { get; set; }
        /// <summary>
        /// 订单和库存绑定公司
        /// </summary>
        [DataMember]
        public string stockcompany { get; set; }
        /// <summary>
        /// 关注状态 0未关注，1已关注
        /// </summary>
        public int oastatus { set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WeChatUserData
    {
        public string openId { get; set; }
        public string unionId { get; set; }
        public string nickName { get; set; }
        public string gender { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class WeChatPhoneData
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// purePhoneNumber 纯电话号码
        /// </summary>
        public string purePhoneNumber { get; set; }
        /// <summary>
        /// countryCode
        /// </summary>
        public string countryCode { get; set; }
    }
}
