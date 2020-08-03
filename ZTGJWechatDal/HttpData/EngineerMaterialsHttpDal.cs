using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.API.EngineerMaterials;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.HttpData
{
    public class EngineerMaterialsHttpDal
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_WechatLogin_Response EM_WechatLogin(string req)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_WechatLogin, req);
            return JsonConvert.DeserializeObject<EM_WechatLogin_Response>(res);
        }
        /// <summary>
        /// 修改手机号和微信名称
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public EM_WechatLogin_Response EM_Mobile(string req, string token)
        {
            string res = HttpMethods.Post_EM(InsideApiUrlUtil.EM_Mobile, req, token);
            return JsonConvert.DeserializeObject<EM_WechatLogin_Response>(res);
        }
    }
}
