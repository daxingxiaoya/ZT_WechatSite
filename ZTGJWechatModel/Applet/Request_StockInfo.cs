using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.API;

namespace ZTGJWechatModel.Applet
{
    public class Request_StockInfo : ApiBase_Request
    {
        /// <summary>
        /// 备件号
        /// </summary>
        public string ProductCode { set; get; }
        /// <summary>
        /// 库区集合
        /// </summary>
        public List<string> LocationNamesArr { set; get; }
        /// <summary>
        /// 排序方式 0默认 1正序 2倒序
        /// </summary>
        public int sort { set; get; }
    }
}
