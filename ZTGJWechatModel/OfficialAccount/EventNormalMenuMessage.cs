using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.OfficialAccount
{
    /// <summary>
    /// 普通菜单事件，包括click和view
    /// </summary>
    public class EventNormalMenuMessage : BaseMessage
    {
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }

    }
}
