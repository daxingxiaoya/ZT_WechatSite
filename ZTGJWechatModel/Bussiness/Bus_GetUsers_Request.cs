using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Bussiness
{
    public class Bus_GetUsers_Request
    {
        public Bus_GetUsers_Request() {
            page = 1;
            pagesize = 10;
        }
        /// <summary>
        /// 类型  em:工程师管理系统  stock:库存系统
        /// </summary>
        public string BType { set; get; }
        public int page { set; get; }
        public int pagesize { set; get; }
    }
}
