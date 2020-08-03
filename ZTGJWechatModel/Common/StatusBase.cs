using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Common
{
    public class StatusBase
    {
        /// <summary>
        /// 状态 0成功 
        /// </summary>
        public int code { set; get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string msg { set; get; }
    }
}
