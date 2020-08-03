using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel;
using ZTGJWechatModel.API;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    public class HttpCommonDal
    {
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="phonenum">手机号</param>
        /// <param name="contxt">内容</param>
        /// <returns></returns>
        public bool SMSSending(string phonenum, string contxt)
        {
            string res = HttpHelper.HttpPost(InsideApiUrlUtil.SMSSending + "?key=" + AppSettingUtil.InsideApiKey + "&to=" + phonenum + "&text=" + contxt, "");//发送短信
            return JsonConvert.DeserializeObject<ApiResponseBase>(res).code == 0 ? true : false;
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Companys_Response companys(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.companys, req);
            return JsonConvert.DeserializeObject<Companys_Response>(res);
        }
        /// <summary>
        /// 库区
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Locations_Response locations(string req)
        {
            string res = HttpMethods.Post(InsideApiUrlUtil.locations, req);
            return JsonConvert.DeserializeObject<Locations_Response>(res);
        }

    }
}
