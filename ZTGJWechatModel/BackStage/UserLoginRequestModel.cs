using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.BackStage
{
    public class UserLoginRequestModel
    {
        public UserLoginRequestModel() {

        }

        //
        // 摘要:
        //     应用ID,无需外部赋值
        public int AppID { get; }
        //
        // 摘要:
        //     账号
        public string Account { get; set; }
        //
        // 摘要:
        //     加密后的密码
        public string Pwd { get; set; }
    }
}
