using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils.Redis
{
    public static class RedisKeys
    {
        /// <summary>
        /// 全局配置 数据存储位置redis-->0
        /// </summary>
        public static string GlobalConfiguration
        {
            get { return "GlobalConfiguration"; }
        }
        /// <summary>
        /// access_token缓存键 数据存储位置redis-->0
        /// </summary>
        public static string AccessTokenKey
        {
            get { return "AccessTokenKey"; }
        }
        /// <summary>
        /// WareHouseKey 数据存储位置redis-->0
        /// </summary>
        public static string WareHouseKey
        {
            get { return "WareHouseKey"; }
        }
        /// <summary>
        /// 用户授权key  数据存储位置redis-->0
        /// </summary>
        public static string UserEmpowerKey
        {
            get { return "UEmpowerKey"; }
        }
        /// <summary>
        /// 验证码key
        /// </summary>
        public static string CAPTCHAkey
        {
            get { return "CAPTCHA"; }
        }
    }
}
