using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    public class AppletCommonDal
    {
        private ResponseApBaseUserInfo apbaseu = new ResponseApBaseUserInfo();
        /// <summary>
        /// 根据js_code获取小程序用户基本信息 session、openid、unionid
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public ResponseApBaseUserInfo GetApBaseUserInfo(string js_code)
        {
            string param = string.Format("?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", AppSettingUtil.ApAppId, AppSettingUtil.ApAppSecret, js_code);
            string res = HttpHelper.HttpGet(WechatRequestUrlUtil.GetApSessionKey + param);
            if (!string.IsNullOrEmpty(res))
            {
                apbaseu = JsonConvert.DeserializeObject<ResponseApBaseUserInfo>(res);
            }
            return apbaseu;
        }

        /// <summary>
        /// 小程序用户对应的Session_Key
        /// </summary>
        /// <param name="js_code"></param>
        /// <returns></returns>
        public string GetSession_Key(string js_code)
        {
            apbaseu = GetApBaseUserInfo(js_code);
            if (apbaseu!=null)
            {
                return apbaseu.session_key;
            }
            else
            {
                return "";
            }
        }

    }
}
