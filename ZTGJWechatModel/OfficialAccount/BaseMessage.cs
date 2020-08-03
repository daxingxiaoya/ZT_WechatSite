using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.EnumModel;

namespace ZTGJWechatModel.OfficialAccount
{
    public class BaseMessage
    {
        public BaseMessage() {
            ToUserName = "";
            FromUserName = "";
            CreateTime = "";
            MsgType = MsgType.NOType;
            Event = Event.NOEVENT;
        }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public Event Event { get; set; }
    }
}
