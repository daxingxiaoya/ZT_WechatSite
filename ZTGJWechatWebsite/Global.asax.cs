using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitDbConnectionStringConfig();
        }

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        private static void InitDbConnectionStringConfig()
        {
            DBConnectionStringConfig dbConnectionStringConfig = new DBConnectionStringConfig();
            dbConnectionStringConfig.WechatServerDBReadConnStr = ConfigurationManager.ConnectionStrings["WechatServerConnection"].ToString();
            DBConnectionStringConfig.InitDefault(dbConnectionStringConfig);
        }
        
    }
}
