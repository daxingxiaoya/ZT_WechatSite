using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTGJWechatBll.BackStage;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Applet;
using ZTGJWechatModel.Common;
using ZTGJWechatUtils;

namespace ZTGJWechatWebsite.Controllers.BackStage
{
    public class DataManageController : Controller
    {
        private BSUserBll bsubll = new BSUserBll();
        private ReservoirAreaBll areabll = new ReservoirAreaBll();
        /// <summary>
        /// 库区列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult ReservoirAreaList()
        {
            var userInfo = BSUserBll.LoginUserInfo;
            if (userInfo == null)
            {
                return View("../Home/Login");
            }
            ViewBag.LoginAccount = userInfo.Account;

            //List<VendCompanyModel> comlist = new List<VendCompanyModel>();
            //comlist = areabll.GetCompany();
            //ViewBag.CompanyList = comlist;
            return View();
        }

        /// <summary>
        /// 公司数据
        /// </summary>
        /// <returns></returns>
        public string GetComList() {
            string res = "";
            try
            {
                List<VendCompanyModel> comlist = areabll.GetCompany();
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", rows = comlist, count = comlist.Count });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障," + ex.Message });
                LogHelper.ErrorLog(ex.Message + "" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 公司数据同步
        /// </summary>
        /// <returns></returns>
        public string ComSync()
        {
            string res = "";
            try
            {
                int addnum = 0;//新增公司个数
                ApiBase_Request comreq = new ApiBase_Request() {
                    key= AppSettingUtil.InsideApiKey2
                };
                var comapires = areabll.companys(JsonConvert.SerializeObject(comreq));
                if (comapires.code == 200 && comapires.data.Count > 0)
                {
                    foreach (var item in comapires.data)
                    {
                        var comlist = areabll.GetCompany();
                        if (comlist.Where(c => c.VendName == item.ShortName && c.FullName == item.FullName).ToList().Count == 0)
                        {
                            VendCompanyModel comm = new VendCompanyModel()
                            {
                                VendName = item.ShortName,
                                FullName = item.FullName,
                                CreateTime = DateTime.Now
                            };
                            if (areabll.AddCompany(comm))
                            {
                                addnum += 1;
                            }
                        }
                    }
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "同步成功，新增COM" + addnum + "个", addnmu = addnum });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "同步失败：companys接口异常" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "同步失败：系统故障," + ex.Message });
                LogHelper.ErrorLog(ex.Message + "" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 库区数据同步
        /// </summary>
        /// <returns></returns>
        public string LOCSync(string vendname)
        {
            string res = "";
            try
            {
                ApiBase_Request locreq = new ApiBase_Request();
                locreq.key = AppSettingUtil.InsideApiKey2;

                int addnum = 0, upnum = 0;//新增、修改库区个数
                List<ReservoirAreaModel> loclist = new List<ReservoirAreaModel>();
                Locations_Response locres = new Locations_Response();
                if (!string.IsNullOrEmpty(vendname))
                {
                    locreq.CompanyName = vendname;
                    locres = areabll.locations(JsonConvert.SerializeObject(locreq));
                    loclist = areabll.GetLocs(vendname);//本地loc

                    int saddnum = 0, supnum = 0;//新增、修改库区个数
                    SubDataSync(loclist, locres,out saddnum, out supnum);
                    addnum += saddnum;
                    upnum += supnum;
                }
                else
                {
                    var comlist = areabll.GetCompany();
                    foreach (var item in comlist)
                    {
                        locreq.CompanyName = item.VendName;
                        locres = areabll.locations(JsonConvert.SerializeObject(locreq));
                        loclist = areabll.GetLocs(item.VendName);//本地loc

                        int saddnum = 0, supnum = 0;//新增、修改库区个数
                        SubDataSync(loclist, locres, out saddnum, out supnum);
                        addnum += saddnum;
                        upnum += supnum;
                    }
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "同步成功，新增LOC" + addnum + "个，更新LOC" + upnum + "个", });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "同步失败：系统故障," + ex.Message });
                LogHelper.ErrorLog(ex.Message + "" + ex.StackTrace);
            }
            return res;
        }

        private void SubDataSync(List<ReservoirAreaModel> loclist, Locations_Response locres, out int addnum, out int upnum)
        {
            int outaddnum = 0, outupnum = 0;
            if (locres.code == 200)
            {
                foreach (var item in locres.data)
                {
                    ReservoirAreaModel ram = new ReservoirAreaModel();
                    //筛选库区号
                    var locnolist = loclist.Where(l => l.ReservoirAreaNo == item.Code).ToList();
                    if (locnolist.Count > 0)//判断是否需要修改库区别名
                    {
                        //如果别名不一样则修改
                        if (locnolist.Where(o => o.AnotherName == item.FieldZN).ToList().Count == 0)
                        {
                            ram.VendName = loclist[0].VendName;
                            ram.ReservoirAreaNo = item.Code;
                            ram.AnotherName = item.FieldZN;
                            ram.UpdateTime = DateTime.Now;
                            if (areabll.UpdateLoc(ram))
                            {
                                outupnum += 1;
                            }
                        }
                    }
                    else//新增库区
                    {
                        ram.VendName = loclist[0].VendName;
                        ram.ReservoirAreaNo = item.Code;
                        ram.AnotherName = item.FieldZN;
                        ram.Enable = 0;
                        ram.CreateTime = DateTime.Now;
                        ram.UpdateTime = DateTime.Now;
                        if (areabll.AddLoc(ram))
                        {
                            outaddnum += 1;
                        }
                    }
                }

            }
            addnum = outaddnum;
            upnum = outupnum;
        }



    }
}