using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils.Cache
{
    public class CacheKey
    {
        /// <summary>
        /// accesstoken
        /// </summary>
        public static string AccessToken
        {
            get { return "AccessToken"; }
        }

        #region 动态key
        /// <summary>
        /// 
        /// </summary>
        public static string t
        {
            get { return "t"; }
        }
        #endregion
    }
}
