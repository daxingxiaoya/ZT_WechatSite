using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    public class TokenDal
    {
        /// <summary>
        /// 根据appid获取公众号access_token
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public string GetOAAccessToken()
        {
            string param = string.Format("?grant_type=client_credential&appid={0}&secret={1}", AppSettingUtil.OAAppId, AppSettingUtil.OAAppSecret);
            string res = HttpHelper.HttpGet(WechatRequestUrlUtil.AccessTokenUrl + param);//get请求token
            if (!string.IsNullOrEmpty(res))
            {
                AccessToken token = JsonConvert.DeserializeObject<AccessToken>(res);
                return token.access_token;
            }
            else
            {
                return "";
            }
        }


    }
}
