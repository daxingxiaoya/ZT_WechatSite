using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.WareHouse;
using ZTGJWechatModel;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatBll
{
    public class WareHouseBll
    {
        private WareHouseDal whdal = new WareHouseDal();

        /// <summary>
        /// WareHouse
        /// </summary>
        /// <param name="state">数据状态 0正常 1删除</param>
        /// <returns></returns>
        public string GetWareHouse()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string redisKey = RedisKeys.WareHouseKey;//key

            string res = "";
            List<WareHouseModel> whouselist = new List<WareHouseModel>();
            try
            {
                string content = RedisHelper.Get<string>(redisKey, (long)0);//获取redis缓存

                if (content == null || content.ToString() == "")
                {
                    whouselist = whdal.GetWareHouse("0");
                    if (whouselist.Count>0)
                    {
                        //写入缓存 缓存24小時    access_token微信那边120分钟过期
                        RedisHelper.Set<string>(redisKey, JsonConvert.SerializeObject(whouselist), new TimeSpan(24, 0, 0), (long)0);
                    }
                }
                else
                {
                    whouselist = JsonConvert.DeserializeObject<List<WareHouseModel>>(content);
                }
                foreach (var item in whouselist)
                {
                    res += "<h3>" + item.Name + "</h3>";

                    res += " <ul class='listing'> ";
                    res += " <li>库房地址：" + item.Address + "</li>";
                    if (!string.IsNullOrEmpty(item.PostalCode))
                    {
                        res += " <li>邮编：" + item.PostalCode + "</li>";
                    }
                    if (!string.IsNullOrEmpty(item.TelPhone))
                    {
                        res += " <li>电话：" + item.TelPhone + "</li>";
                    }
                    if (!string.IsNullOrEmpty(item.Facsimile))
                    {
                        res += " <li>传真：" + item.Facsimile + "</li>";
                    }
                    if (!string.IsNullOrEmpty(item.Phone))
                    {
                        res += " <li>HOTLINE：" + item.Phone + "</li>";
                    }
                    if (!string.IsNullOrEmpty(item.Email))
                    {
                        res += " <li>邮箱：" + item.Email + "</li>";
                    }
                    res += "</ul>";

                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                LogHelper.ErrorLog("GetOAAccessToken异常：" + ex.Message + "，用时" + sw.ElapsedMilliseconds + "毫秒，异常追踪：" + ex.StackTrace);
            }
            return res;
        }

    }
}
