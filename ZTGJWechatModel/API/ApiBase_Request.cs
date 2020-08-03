using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    /// <summary>
    /// api请求基础参数
    /// </summary>
    public class ApiBase_Request
    {
        /// <summary>
        /// 接口秘钥
        /// </summary>
        public string key { set; get; }
        /// <summary>
        /// 微信uid
        /// </summary>
        public string UnionId { set; get; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { set; get; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { set; get; }
    }
}
