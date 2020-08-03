using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.OfficialAccount
{
    public class ResponseBase
    {
        public ResponseBase() {
            errcode = 0;
            errmsg = "";
        }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
