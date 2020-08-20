using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Bussiness
{
    public class Bus_BindingUsers_Request
    {
        /// <summary>
        /// 微信唯一id
        /// </summary>
        public string unionid { set; get; }
        /// <summary>
        /// 来源  em:工程师管理系统  stock:库存系统
        /// </summary>
        public string source { set; get; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string companycode { set; get; }
    }
}
