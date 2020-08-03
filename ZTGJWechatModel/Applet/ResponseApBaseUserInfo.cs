using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.OfficialAccount;

namespace ZTGJWechatModel.Applet
{
    public class ResponseApBaseUserInfo : ResponseBase
    {
        public string session_key { set; get; }
        public string openid { set; get; }
        public string unionid { set; get; }
    }
}
