using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    public class AppSettingUtil
    {
        #region 根据key获取值

        /// <summary>
        /// 根据key获取string值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetVal(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 根据key获取string值,再转换为指定的泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetVal<T>(string key)
        {
            var tempVal = GetVal(key);
            return (T)Convert.ChangeType(tempVal, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 根据key获取string值,再转换为指定的泛型,且值为null时候,返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static T GetVal<T>(string key, T defaultVal)
        {
            var tempVal = GetVal(key);
            if (tempVal == null) return defaultVal;
            return (T)Convert.ChangeType(tempVal, typeof(T), CultureInfo.InvariantCulture);
        }

        #endregion

        #region 公众号配置
        /// <summary>
        /// 公众号APPID
        /// </summary>
        public static string OAAppId
        {
            get { return GetVal("OAAppId"); }
        }
        /// <summary>
        /// 公众号AppSecret
        /// </summary>
        public static string OAAppSecret
        {
            get { return GetVal("OAAppSecret"); }
        }
        /// <summary>
        /// 请求微信基础地址
        /// </summary>
        public static string WechatBaseUrl
        {
            get { return GetVal("WechatBaseUrl"); }
        }
        /// <summary>
        /// 模板id 快递发货通知
        /// </summary>
        public static string TemplateId
        {
            get { return GetVal("TemplateId"); }
        }
        
        #endregion

        /// <summary>
        /// api授权签名
        /// </summary>
        public static string Sign
        {
            get { return GetVal("Sign"); }
        }

        #region 小程序配置
        /// <summary>
        /// 小程序APPID
        /// </summary>
        public static string ApAppId
        {
            get { return GetVal("ApAppId"); }
        }
        /// <summary>
        /// 小程序AppSecret
        /// </summary>
        public static string ApAppSecret
        {
            get { return GetVal("ApAppSecret"); }
        }
        #endregion

        #region mysql配置
        /// <summary>
        /// MySql服务连接字符串
        /// </summary>
        public static string MySqlConnectionStr
        {
            get { return GetVal("MySqlConnectionStr"); }
        }
        #endregion

        #region redis配置
        /// <summary>
        /// redis服务连接字符串
        /// </summary>
        public static string RedisServerConnectionStr
        {
            get { return GetVal("RedisServerConnectionStr"); }
        }
        #endregion

        #region 内部接口
        /// <summary>
        /// 内部api接口基础地址 （手机短信）
        /// </summary>
        public static string InsideApiBaseUrl
        {
            get { return GetVal("InsideApiBaseUrl"); }
        }
        /// <summary>
        /// 内部api接口秘钥 （手机短信）
        /// </summary>
        public static string InsideApiKey
        {
            get { return GetVal("InsideApiKey"); }
        }
        /// <summary>
        /// 内部api接口基础地址 （物流/库存/订单）
        /// </summary>
        public static string InsideApiBaseUrl2
        {
            get { return GetVal("InsideApiBaseUrl2"); }
        }
        /// <summary>
        /// 内部api接口秘钥 （物流/库存/订单）
        /// </summary>
        public static string InsideApiKey2
        {
            get { return GetVal("InsideApiKey2"); }
        }
        /// <summary>
        /// 内部api接口基础地址 （工程师物料管理）
        /// </summary>
        public static string InsideApiBaseUrl3
        {
            get { return GetVal("InsideApiBaseUrl3"); }
        }
        /// <summary>
        /// 内部api接口秘钥 （工程师物料管理）
        /// </summary>
        public static string InsideApiKey3
        {
            get { return GetVal("InsideApiKey3"); }
        }
        #endregion

        public static string PublicImg
        {
            get { return GetVal("PublicImg"); }
        }

        #region 提供给别人
        public static string OutSideKey
        {
            get { return GetVal("OutSideKey"); }
        }
        
        #endregion
    }
}
