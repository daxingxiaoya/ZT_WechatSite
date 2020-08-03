using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel
{
    public class AccessToken
    {
        /// <summary>
        /// access_token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 有效时间 单位：秒
        /// </summary>
        public string expires_in { get; set; }
    }
}
