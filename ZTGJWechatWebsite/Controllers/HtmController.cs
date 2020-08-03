using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTGJWechatBll;
using ZTGJWechatModel;

namespace ZTGJWechatWebsite.Controllers
{
    public class HtmController : Controller
    {
        private WareHouseBll whbll = new WareHouseBll();
        public string Getwhouse()
        {
            return whbll.GetWareHouse();
        }
    }
}