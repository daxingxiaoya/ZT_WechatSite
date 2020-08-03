using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace ZTGJWechatUtils
{
    /// <summary>
    /// 微信请求地址集合
    /// </summary>
    public class WechatRequestUrlUtil
    {
        /// <summary>
        /// access_token地址
        /// GET https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
        /// </summary>
        /// <returns></returns>
        public static string AccessTokenUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/token"; }
        }
        /// <summary>
        /// 获取微信服务器的ip
        /// GET https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string FetWeixinIpUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/getcallbackip"; }
        }
        /// <summary>
        /// 获取公众号的自动回复规则
        /// GET（请使用https协议） https://api.weixin.qq.com/cgi-bin/get_current_autoreply_info?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentAutoreplyInfoUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/get_current_autoreply_info"; }
        }
        /// <summary>
        /// 模板消息推送
        /// http请求方式: POST https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string MessagePush
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/message/template/send"; }
        }

        #region 用户管理
        /// <summary>
        /// 设置用户备注名
        /// POST（请使用https协议） https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string UpdateRemarkUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/user/info/updateremark"; }
        }
        /// <summary>
        /// 获取用户基本信息
        /// GET https://api.weixin.qq.com/cgi-bin/user/info?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
        /// </summary>
        /// <returns></returns>
        public static string UserInfoUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/user/info"; }
        }
        /// <summary>
        /// 获取用户列表
        /// GET（请使用https协议）https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid=NEXT_OPENID
        /// </summary>
        /// <returns></returns>
        public static string UserListUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/user/get"; }
        }
        #endregion

        #region 临时素材
        /// <summary>
        /// 上传临时素材
        /// POST/FORM，使用https https://api.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type=TYPE
        /// </summary>
        /// <returns></returns>
        public static string MediaUpdateUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/media/upload"; }
        }
        /// <summary>
        /// 获取临时素材
        /// GET,https调用 https://api.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
        /// </summary>
        /// <returns></returns>
        public static string MediaGetUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/media/get"; }
        }
        /// <summary>
        /// 群发消息 上传图文消息内的图片获取url,
        /// </summary>
        /// <returns></returns>
        public static string MediaUploadImgUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/media/uploadimg"; }
        }
        /// <summary>
        /// 群发消息 上传图文消息素材
        /// </summary>
        /// <returns></returns>
        public static string mediaUploadnewsUrl
        {
            get { return AppSettingUtil.WechatBaseUrl + "/cgi-bin/media/uploadnews"; }
        }
        #endregion

        #region 小程序
        /// <summary>
        /// 获取小程序session_key
        /// https://api.weixin.qq.com/sns/jscode2session?appid=appid&secret=secret&js_code=js_code&grant_type=authorization_code
        /// </summary>
        /// <returns></returns>
        public static string GetApSessionKey
        {
            get { return AppSettingUtil.WechatBaseUrl + "/sns/jscode2session"; }
        }

        #endregion

    }
}
