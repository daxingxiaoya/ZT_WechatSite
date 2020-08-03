using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.Common;
using ZTGJWechatModel;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatBll.Common
{
    public class GlobalConfigurationBll
    {
        private GlobalConfigurationDal gcdal = new GlobalConfigurationDal();

        /// <summary>
        /// 根据key获取值
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetValueByGC(string key)
        {
            string res = "";
            try
            {
                List<GlobalConfiguration> gclist = new List<GlobalConfiguration>();

                string redisKey = RedisKeys.GlobalConfiguration;//key
                string content = RedisHelper.Get<string>(redisKey, (long)0);//获取redis缓存

                if (string.IsNullOrEmpty(content))
                {
                    gclist = gcdal.GetAllGlobalConfiguration();
                    if (gclist.Count > 0)
                    {
                        //写入缓存 缓存6小时
                        RedisHelper.Set<string>(redisKey, JsonConvert.SerializeObject(gclist), new TimeSpan(6, 0, 0), (long)0);
                    }
                }
                else
                {
                    gclist = JsonConvert.DeserializeObject<List<GlobalConfiguration>>(content);
                }

                if (gclist.Count > 0)
                {
                    res = gclist.Where(g => g.ControlKey == key).FirstOrDefault().ControValue;
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", val = res });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "无配置", val = "" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message, val = "" });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 根据key获取值
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetValueTxtByGC(string key)
        {
            string res = "";
            try
            {
                List<GlobalConfiguration> gclist = new List<GlobalConfiguration>();

                string redisKey = RedisKeys.GlobalConfiguration;//key
                string content = RedisHelper.Get<string>(redisKey, (long)0);//获取redis缓存

                if (string.IsNullOrEmpty(content))
                {
                    gclist = gcdal.GetAllGlobalConfiguration();
                    if (gclist.Count > 0)
                    {
                        //写入缓存 缓存6小时
                        RedisHelper.Set<string>(redisKey, JsonConvert.SerializeObject(gclist), new TimeSpan(6, 0, 0), (long)0);
                    }
                }
                else
                {
                    gclist = JsonConvert.DeserializeObject<List<GlobalConfiguration>>(content);
                }

                if (gclist.Count > 0)
                {
                    res = gclist.Where(g => g.ControlKey == key).FirstOrDefault().ControValue;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool UpdateGC(GlobalConfiguration m) {
            return gcdal.UpdateGC(m);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGC(GlobalConfiguration m) {
            return gcdal.AddGC(m);
        }
        /// <summary>
        /// 获取配置带分页
        /// </summary>
        /// <returns></returns>
        public List<GlobalConfiguration> GetGCByKey(string id, string key = "", int page = 1, int psize = 200)
        {
            return gcdal.GetGCByKey(id, key, page, psize);
        }

    }
}
