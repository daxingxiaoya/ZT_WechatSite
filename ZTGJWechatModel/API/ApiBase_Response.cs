using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    /// <summary>
    /// api接口基础返回实体类
    /// </summary>
    public class ApiBase_Response
    {
        public int code { set; get; }
        public string msg { set; get; }
    }
}
