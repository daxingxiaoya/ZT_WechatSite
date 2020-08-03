using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Common
{
    /// <summary>
    /// 静态数据
    /// </summary>
    public class StaticDataBll
    {
        /// <summary>
        /// 获取地址相关静态数据
        /// </summary>
        /// <returns></returns>
        public string GetArealist()
        {
            string res = "";
            try
            {
                //公众号菜单文件路径
                string jsonfile = System.Web.HttpContext.Current.Request.MapPath("\\config\\menu.json");
                string Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "app_data\\";
                //读取json文件
                StreamReader objReader = new StreamReader(Path + "areaList.json");
                string areaList = objReader.ReadToEnd();
                objReader.Close();

                //读取json文件
                StreamReader objReader2 = new StreamReader(Path + "foramtProvince.json");
                string foramtProvince = objReader2.ReadToEnd();
                objReader2.Close();

                //读取json文件
                StreamReader objReader3 = new StreamReader(Path + "zipCode.json");
                string zipCode = objReader3.ReadToEnd();
                objReader3.Close();
                

                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok",
                    areaList = JsonConvert.DeserializeObject<List<ZTGJWechatModel.StaticData.areaList>>(areaList).OrderBy(o=>o.name).OrderByDescending(o => o.sortnum),
                    foramtProvince = JsonConvert.DeserializeObject<List<ZTGJWechatModel.StaticData.foramtProvince>>(foramtProvince).OrderBy(o => o.name),
                    zipCode = JsonConvert.DeserializeObject<List<ZTGJWechatModel.StaticData.zipCode>>(zipCode).OrderBy(o => o.name)
                });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("GetArealist异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        
    }
}
