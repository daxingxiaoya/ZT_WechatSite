using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    /// <summary>
    /// 用户的信息请求参数
    /// </summary>
    public class RequestUserInfo
    {
        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// unionid
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// userinfo encryptedData敏感数据
        /// </summary>
        public string userEncryptedData { get; set; }
        /// <summary>
        /// userinfo iv偏移量
        /// </summary>
        public string userIv { get; set; }
        /// <summary>
        /// phone encryptedData敏感数据
        /// </summary>
        public string phoneEncryptedData { get; set; }
        /// <summary>
        /// phone iv偏移量
        /// </summary>
        public string phoneIv { get; set; }
        /// <summary>
        /// session_key
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatarUrl { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string usaername { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string companyname { get; set; }

        /// <summary>
        /// 原来手机号
        /// </summary>
        public string mobilephone { get; set; }
        /// <summary>
        /// 新手机号
        /// </summary>
        public string newphone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationcode { get; set; }

    }
}
