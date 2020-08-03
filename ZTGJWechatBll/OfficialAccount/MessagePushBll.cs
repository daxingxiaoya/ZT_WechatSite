using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.HttpData;

namespace ZTGJWechatBll.OfficialAccount
{
    /// <summary>
    /// 消息推送
    /// </summary>
    public class MessagePushBll
    {
        private MessagePushDal msgpushdal = new MessagePushDal();
        /// <summary>
        /// 推送模板消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string MessagePush(string token, string reqdata) {
            return msgpushdal.MessagePush(token, reqdata);
        }
    }
}
