using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTGJWechatBll;
using ZTGJWechatBll.Applet;
using ZTGJWechatBll.Applet.EM;
using ZTGJWechatBll.Common;
using ZTGJWechatUtils;

namespace ZTGJWechatWebsite.Controllers 
{
    public class ApiApController : Controller
    {
        #region 默认构造器
        private ApCommonBll apcombll = new ApCommonBll();//小程序公用
        private UsersBll usersbll = new UsersBll();//微信用户
        private GlobalConfigurationBll gcbll = new GlobalConfigurationBll();//全局设置
        private ExpressBll expbll = new ExpressBll();//物流
        private StockBll stockbll = new StockBll();//库存
        private ShoppingCartBll scbll = new ShoppingCartBll();//购物车
        private CollectAndFootPrintBll candfbll = new CollectAndFootPrintBll();//收藏和足迹
        private OrderBll orderbll = new OrderBll();//订单
        private StaticDataBll staticdatabll = new StaticDataBll();//静态数据
        private AddressBll addrbll = new AddressBll();//地址
        private EngineerMaterialsBll embll = new EngineerMaterialsBll();//工程师物料管理
        #endregion

        /// <summary>
        /// 获取sessionokey
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSession_Key(){
            return Content(apcombll.GetSession_Key(Request["code"]));
        }

        #region 用戶信息
        /// <summary>
        /// 小程序用户信息(解密操作)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAppletUserInfo() {
            return Content(apcombll.GetAppletUserInfo(Request["reqdata"]));
        }

        /// <summary>
        /// 小程序用户的后台授权和基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserEmpower(){
            return Content(usersbll.GetUserEmpower(Request["uid"]));
        }

        /// <summary>
        /// 小程序用户 修改用户名和公司名
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateUser(){
            return Content(usersbll.UpdateUserCompany(Request["reqdata"]));
        }

        /// <summary>
        /// 根据key获取值
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public ActionResult GetValueByGC() {
            return Content(gcbll.GetValueByGC(Request["key"]));
        }
        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult SendVerificationCode()
        {
            return Content(apcombll.SendVerificationCode(Request["phonenum"]));
        }
        /// <summary>
        /// 小程序用户 修改手机号
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateUserPhone()
        {
            return Content(usersbll.UpdateUserPhone(Request["reqdata"]));
        }
        #endregion

        #region 物流
        /// <summary>
        /// 快递查询历史
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExpressSearchHistory()
        {
            return Content(expbll.GetSearchHistory(Request["uid"], "express"));
        }
        /// <summary>
        /// 根据手机号获取运单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult WaybillList()
        {
            return Content(expbll.WaybillList(Request["reqdata"].ToString()));
        }
        /// <summary>
        /// 运单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult WaybillInfo()
        {
            return Content(expbll.WaybillInfo(Request["reqdata"].ToString()));
        }
        /// <summary>
        /// 运单追踪
        /// </summary>
        /// <returns></returns>
        public ActionResult WaybillTrack()
        {
            return Content(expbll.WaybillTrack(Request["reqdata"].ToString()));
        }
        #endregion

        #region 库存
        /// <summary>
        /// 库区查询历史
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReservoirAreaSearchHistory()
        {
            return Content(expbll.GetSearchHistory(Request["uid"], "reservoirarea"));
        }
        /// <summary>
        /// 根据uid获取权限内的库区信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRAByUId()
        {
            return Content(stockbll.GetRAByUId(Request["uid"]));
        }
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <returns></returns>
        public ActionResult StockSearch(string req) {
            return Content(stockbll.StockSearch(Request["reqdata"]));
        }
        /// <summary>
        /// 备件基础信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Product(string req)
        {
            return Content(stockbll.Product(Request["reqdata"]));
        }
        
        #endregion

        #region 下单
        /// <summary>
        /// 根据uid获取用户购物车商品
        /// </summary>
        /// <returns></returns>
        public ActionResult GetShoppingCart()
        {
            return Content(scbll.GetShoppingCart(Request["uid"]));
        }
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult AddShoppingCart()
        {
            return Content(scbll.AddShoppingCart(Request["reqdata"]));
        }
        /// <summary>
        /// 修改购物车商品选中状态
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCartChecked()
        {
            return Content(scbll.UpdateShoppingCartChecked(Request["reqdata"]));
        }
        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="reqdata"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult DelShoppingCart(string reqdata)
        {
            return Content(scbll.DelShoppingCart(Request["reqdata"]));
        }
        /// <summary>
        /// 订单
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderInfo() {
            return Content(orderbll.OrderInfo(Request["reqdata"]));
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrder()
        {
            return Content(orderbll.CreateOrder(Request["reqdata"]));
        }
        #endregion

        #region 足迹和收藏
        /// <summary>
        /// 获取收藏或者足迹带分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCOrFByPage()
        {
            return Content(candfbll.GetCOrFByPage(Request["uid"].ToString(), Convert.ToInt32(Request["page"]), Convert.ToInt32(Request["pagesize"]), Convert.ToInt32(Request["type"])));
        }
        /// <summary>
        /// 获取用户收藏 带分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCollectByPage()
        {
            return Content(candfbll.GetCollectByPage(Request["uid"].ToString(), Convert.ToInt32(Request["page"]), Convert.ToInt32(Request["pagesize"])));
        }

        /// <summary>
        /// 获取用户足迹 带分页
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="type">0收藏 1足迹</param>
        /// <returns></returns>
        public ActionResult GetFootPrintByPage()
        {
            return Content(candfbll.GetFootPrintByPage(Request["uid"].ToString()));
        }
        /// <summary>
        /// 新增足迹
        /// </summary>
        /// <returns></returns>
        public ActionResult AddFootPrint()
        {
            return Content(candfbll.AddFootPrint(Request["reqdata"]));
        }
        /// <summary>
        /// 添加/取消收藏
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrCancelCollect()
        {
            return Content(candfbll.AddOrCancelCollect(Request["reqdata"], Request["type"]));
        }
        /// <summary>
        /// 删除足迹
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public ActionResult DelFootPrint(string reqdata)
        {
            return Content(candfbll.DelFootPrint(Request["reqdata"]));
        }
        #endregion

        #region 基础
        /// <summary>
        /// 获取地址相关静态数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArealist()
        {
            return Content(staticdatabll.GetArealist());
        }

        #region 地址簿
        /// <summary>
        /// 获取用户地址（地址簿/默认地址）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddress()
        {
            return Content(addrbll.GetAddress(Request["uid"], Request["type"], Request["key"], Request["id"]));
        }
        /// <summary>
        /// 添加/修改地址
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrUpAddress()
        {
            return Content(addrbll.AddOrUpAddress(Request["reqdata"]));
        }
        /// <summary>
        /// 更改地址为默认
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateDefaultAddress()
        {
            return Content(addrbll.UpdateDefaultAddress(Request["uid"], Request["id"]));
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <returns></returns>
        public ActionResult DelAddress()
        {
            return Content(addrbll.DelAddress(Request["uid"], Request["id"]));
        }
        #endregion

        #endregion

        #region 工程师物料管理

        /// <summary>
        /// 获取sessionokey
        /// </summary>
        /// <returns></returns>
        public ActionResult EngineerMaterials()
        {
            return Content(embll.MethodIssue(Request["Method"], Request["Token"], Request["Reqdata"]));
        }
        #endregion
    }
}