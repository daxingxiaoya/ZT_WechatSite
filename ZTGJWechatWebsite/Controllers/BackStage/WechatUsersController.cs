using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTGJWechatBll;
using ZTGJWechatBll.BackStage;
using ZTGJWechatBll.Common;
using ZTGJWechatModel;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;
using ZTGJWechatUtils.ConvertHelper;

namespace ZTGJWechatWebsite.Controllers.BackStage
{
    public class WechatUsersController : Controller
    {
        private UsersBll ubll = new UsersBll();
        private GlobalConfigurationBll gcbll = new GlobalConfigurationBll();
        private ReservoirAreaBll rabll = new ReservoirAreaBll();

        /// <summary>
        /// 微信用户列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult UsersList()
        {
            var userInfo = BSUserBll.LoginUserInfo;
            if (userInfo == null)
            {
                return View("../Home/Login");
            }
            ViewBag.LoginAccount = userInfo.Account;
            return View();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public string Getuserlist(int page, int pagesize)
        {
            string res = string.Empty;
            try
            {
                int outtotalcount = 0;
                List<UsersModel> ulist = ubll.GetUsersByPage(page, pagesize, out outtotalcount);
                string html = string.Empty;
                for (int i = 0; i < ulist.Count; i++)
                {
                    html += "<tr class='" + (i % 2 == 0 ? "even" : "odd") + " pointer'>";
                    html += "  <td class=\"a-center\"><input type=\"checkbox\" class=\"flat\" value=\"" + ulist[i].id + "\" name=\"table_records\"></td>";
                    html += "  <td>" + ulist[i].nickname + "</td>";
                    html += "  <td>" + ulist[i].companyname + "</td>";
                    html += "  <td>" + ulist[i].mobilephone + "</td>";
                    html += "  <td>" + ulist[i].unionid + "</td>";
                    html += "  <td>" + UserConvert.StatusTxt(ulist[i].status) + "</td>";
                    html += "  <td>" + UserConvert.EmpowerStatusTxt(ulist[i].empowerStatus) + "</td>";
                    html += "  <td>" + ulist[i].createtime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>";
                    html += "</tr>";
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = outtotalcount, tablehtml = html });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message, count = 0 });
            }
            return res;
        }

        /// <summary>
        /// 获取用户信息 根据用户id
        /// </summary>
        /// <returns></returns>
        public string GetUserInfoById(int userid)
        {
            string res = string.Empty;
            try
            {
                List<UsersModel> ulist = ubll.GetUserInfo(new UsersModel() { id = userid });
                if (ulist.Count > 0)
                {
                    #region 小程序菜单
                    string menu = gcbll.GetValueTxtByGC("ApMenu");
                    string[] menuarr = menu.Split(',');
                    string apmenucheckhtml = "";
                    int checkednum = 0;//选中个数
                    if (menuarr.Length > 0)
                    {
                        string sapcheckhtml = "";
                        foreach (var item in menuarr)
                        {
                            if (ulist[0].powerApMenu.Contains(item))
                            {
                                checkednum += 1;
                                sapcheckhtml += "<span class=\"checkspan\"><input type=\"checkbox\" checked=\"checked\" value=\"" + item + "\" name=\"scheck_ApMenu\" /><label>" + item + "</label></span>";
                            }
                            else
                            {
                                sapcheckhtml += "<span class=\"checkspan\"><input type=\"checkbox\" value=\"" + item + "\" name=\"scheck_ApMenu\" /><label>" + item + "</label></span>";
                            }
                            
                        }
                        if (checkednum == menuarr.Length)//全选框选中
                        {
                            apmenucheckhtml += "<span class=\"checkspan\"><input type=\"checkbox\" checked=\"checked\" id=\"selAll_ApMenu\" /><label>全选</label></span>";
                        }
                        else//全选框不选中
                        {
                            apmenucheckhtml += "<span class=\"checkspan\"><input type=\"checkbox\" id=\"selAll_ApMenu\" /><label>全选</label></span>";
                        }
                        apmenucheckhtml += sapcheckhtml;
                    }
                    #endregion

                    #region 库区
                    List<ReservoirAreaModel> ralist = rabll.GetRAList(ulist[0].companyname);
                    LogHelper.InfoLog("库区数据：" + JsonConvert.SerializeObject(ralist));
                    string rahtml = "";
                    int racheckednum = 0;//选中个数
                    if (ralist.Count>0)
                    {
                        string[] powerraarr = ulist[0].powerReArea.Split(',');
                        foreach (var item in ralist)
                        {
                            if (powerraarr.Count(p => p == item.Id.ToString()) > 0)
                            {
                                racheckednum += 1;
                                rahtml += "<span class=\"checkspan\"><input type=\"checkbox\"  checked=\"checked\" value='" + item.Id + "' name=\"scheck_RA\" /><label>" + item.AnotherName + "</label></span>";
                            }
                            else
                            {
                                rahtml += "<span class=\"checkspan\"><input type=\"checkbox\" value='" + item.Id + "' name=\"scheck_RA\" /><label>" + item.AnotherName + "</label></span>";
                            }
                        }
                    }
                    if (ralist.Count == racheckednum)
                    {
                        rahtml = "<span class=\"checkspan\"><input type=\"checkbox\" checked=\"checked\" id=\"selAll_RA\" /><label>全选</label></span>" + rahtml;
                    }
                    else
                    {
                        rahtml = "<span class=\"checkspan\"><input type=\"checkbox\" id=\"selAll_RA\" /><label>全选</label></span>" + rahtml;
                    }
                    #endregion

                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", userinfo = ulist[0], apmenu = apmenucheckhtml, RA = rahtml });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "未查询到id是" + userid + "的用户" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message, count = 0 });
            }
            return res;
        }

        /// <summary>
        /// 用户权限修改
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ApMenus"></param>
        /// <param name="RA"></param>
        /// <param name="radioestatus"></param>
        /// <returns></returns>
        public string UpUserEmpower(string uid, string ApMenus, string RA, int radioestatus)
        {
            string res = string.Empty;
            try
            {
                UsersModel um = new UsersModel();
                um.unionid = uid;
                um.powerApMenu = ApMenus;
                um.empowerStatus = radioestatus;
                um.powerReArea = RA;

                if (ubll.UpdateUserEmpower(um))
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "权限修改失败" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message });
            }
            return res;
        }

    }
}