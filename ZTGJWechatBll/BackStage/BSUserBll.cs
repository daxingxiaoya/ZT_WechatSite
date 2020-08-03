using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.BackStage;
using ZTGJWechatModel.BackStage;

namespace ZTGJWechatBll.BackStage
{
    public class BSUserBll
    {
        private BSUserDal bsudal = new BSUserDal();
        /// <summary>
        /// 获取后台用户
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public BackStageUsersModel GetBSUserInfo(string acc, string pwd) {
            return bsudal.GetBSUserInfo(acc, pwd);
        }

        /// <summary>
        ///  获取当前登录用户信息
        /// </summary>
        /// <value>
        /// The login user information.
        /// </value>
        public static UserLoginResponseModel LoginUserInfo
        {
            get
            {
                UserLoginResponseModel resu = null;
                var ses = System.Web.HttpContext.Current.Session["LoginUserInfo"];
                if (ses != null)
                {
                    resu = JsonConvert.DeserializeObject<UserLoginResponseModel>(ses.ToString());
                }
                return resu;
            }
        }

    }
}
