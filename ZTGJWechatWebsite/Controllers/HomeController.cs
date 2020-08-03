using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ZTGJWechatBll.BackStage;
using ZTGJWechatModel.BackStage;
using ZTGJWechatUtils;

namespace ZTGJWechatWebsite.Controllers
{
    public class HomeController : Controller
    {
        private BSUserBll bsubll = new BSUserBll();
        public ActionResult Index()
        {
            var userInfo = BSUserBll.LoginUserInfo;
            if (userInfo == null)
            {
                return View("Login");
            }
            ViewBag.LoginAccount = userInfo.Account;
            return View("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public ActionResult Login(string Account,string Pwd)
        {
            string errormsg = string.Empty;
            try
            {
                var userInfo = BSUserBll.LoginUserInfo;
                //有登录session直接跳转首页
                if (userInfo != null&&!string.IsNullOrEmpty(userInfo.Account))
                {
                    return Redirect("/Home/Index");
                }

                if (!string.IsNullOrEmpty(Account) &&!string.IsNullOrEmpty(Pwd))
                {
                    BackStageUsersModel abu = bsubll.GetBSUserInfo(Account, Pwd);
                    if (abu != null)
                    {
                        UserLoginResponseModel respu = new UserLoginResponseModel()
                        {
                            Account = abu.Account,
                            Remark = abu.Remark
                        };
                        Session["LoginUserInfo"] = JsonConvert.SerializeObject(respu);
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        errormsg = "账号或者密码错误";
                    }
                }
            }
            catch (Exception ex)
            {
                errormsg = "系统故障";
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            ViewBag.ErrorMessage = errormsg;
            return View();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            Session["LoginUserInfo"] = null;
            return Redirect("/Home/Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

    }
}