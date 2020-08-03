using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.OfficialAccount;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    public class UserInfoDal
    {
        /// <summary>
        /// 根据用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public ResponseUserInfo GetOAUserInfo(string openid, string access_token)
        {
            string param = string.Format("?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            string res = HttpHelper.HttpGet(WechatRequestUrlUtil.UserInfoUrl + param);//get请求token
            if (!string.IsNullOrEmpty(res))
            {
                ResponseUserInfo u = JsonConvert.DeserializeObject<ResponseUserInfo>(res);
                if (u.errcode != 0)
                {
                    LogHelper.WarnLog("警告：根据用户基本信息异常，异常代码" + u.errcode + "，异常描述" + u.errmsg);
                }
                return u;
            }
            else
            {
                return null;
            }
        }






    }
}
