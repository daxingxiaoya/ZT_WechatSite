using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.OfficialAccount
{
    public class RequestExpressMsgPushModel
    {
        /// <summary>
        /// 签名，相当于key
        /// </summary>
        public string sgin { set; get; }

        public string openid { set; get; }

        public string unionid { set; get; }

        public string ToMobilePhone { set; get; }

        public string data { set; get; }

    }

}
