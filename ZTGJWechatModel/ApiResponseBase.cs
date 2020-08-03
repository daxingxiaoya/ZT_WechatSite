using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel
{
    public class ApiResponseBase
    {
        /// <summary>
        /// 状态代码 0成功 10001未授权 10002其他  10003代码逻辑错误
        /// </summary>
        public int code { set; get; }
        /// <summary>
        /// 详情
        /// </summary>
        public string msg { set; get; }
    }
}
