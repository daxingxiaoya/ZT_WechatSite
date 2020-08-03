using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    /// <summary>
    /// 消息推送
    /// </summary>
    public class MessagePushDal
    {
        /// <summary>
        /// 推送模板消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string MessagePush(string token, string reqdata)
        {
            string res = HttpHelper.HttpPost(WechatRequestUrlUtil.MessagePush + "?access_token=" + token, reqdata);//get请求token
            return res;
        }

    }
}
