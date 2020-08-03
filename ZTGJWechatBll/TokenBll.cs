using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.HttpData;
using ZTGJWechatModel;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Cache;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatBll
{
    public class TokenBll
    {
        private TokenDal tokendal = new TokenDal();

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public string GetOAAccessToken()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string accesstoken = string.Empty;
            string redisKey = RedisKeys.AccessTokenKey;//AccessTokenkey
            try
            {
                string content = RedisHelper.Get<string>(redisKey,(long)0);//获取redis缓存

                if (content == null || content.ToString() == "")
                {
                    accesstoken = tokendal.GetOAAccessToken();
                    if (!string.IsNullOrEmpty(accesstoken))
                    {
                        //写入缓存 缓存110分钟    access_token微信那边120分钟过期
                        RedisHelper.Set<string>(redisKey, accesstoken, new TimeSpan(0, 110, 0), (long)0);
                    }
                }
                else
                {
                    accesstoken = content.ToString();
                    if (accesstoken.Contains("access_token"))
                    {
                        accesstoken = JsonConvert.DeserializeObject<AccessToken>(accesstoken).access_token;
                    }
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                LogHelper.ErrorLog("GetOAAccessToken异常：" + ex.Message + "，用时" + sw.ElapsedMilliseconds + "毫秒，异常追踪：" + ex.StackTrace);
            }
            return accesstoken;
        }

        /// <summary>
        /// 清理AccessToken缓存
        /// </summary>
        public void ClearCacheAccessToken()
        {
            string cacheKey = CacheKey.AccessToken;//AccessTokenkey
            DataCacheUtil.RemoveCache(cacheKey);
        }


    }
}
