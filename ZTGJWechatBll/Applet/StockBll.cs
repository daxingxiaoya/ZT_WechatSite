using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.HttpData;
using ZTGJWechatDal.Order;
using ZTGJWechatDal.Stock;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Applet
{
    public class StockBll
    {
        private StockDal stockdal = new StockDal();
        private StockHttpDal stockhttpdal = new StockHttpDal();
        private ShoppingCartBll shopcartbll = new ShoppingCartBll();
        private ShoppingCartDal shopcartdal = new ShoppingCartDal();
        private CollectAndFootPrintDal cfdal = new CollectAndFootPrintDal();

        /// <summary>
        /// 根据uid获取权限内的库区信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetRAByUId(string uid)
        {
            string res = "";
            try
            {
                List<ReservoirAreaexportModel> ralist = stockdal.GetRAByUId(uid);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = ralist.Count, ReservoirAreaList = ralist });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string StockSearch(string reqdata)
        {
            string res = "";
            try
            {
                //LogHelper.InfoLog("StockSearch参数:" + reqdata);
                Request_StockInfo req = JsonConvert.DeserializeObject<Request_StockInfo>(reqdata);//小程序请求参数反序列化

                StockInfo_Request reqtoapi = new StockInfo_Request();//库存查询参数
                reqtoapi.UnionId = req.UnionId;
                reqtoapi.CompanyName = req.CompanyName;
                //reqtoapi.PhoneNumber = req.PhoneNumber.Replace("15827002712", "18717764701");
                reqtoapi.PhoneNumber = req.PhoneNumber;
                reqtoapi.key = AppSettingUtil.InsideApiKey2;
                reqtoapi.list = new List<ProLocListItem>(){
                    new ProLocListItem (){
                        ProductCode=req.ProductCode,
                        LocationNames=string.Join(",", req.LocationNamesArr)
                    }
                };

                StockInfo_Response resstock = stockhttpdal.stockinfo(JsonConvert.SerializeObject(reqtoapi));
                if (resstock.data.Count>0)
                {
                    if (req.sort == 1)//正序
                    {
                        resstock.data = resstock.data.OrderBy(r => r.StockQty).ToList();
                    }
                    else if (req.sort == 2)//倒序
                    {
                        resstock.data = resstock.data.OrderByDescending(r => r.StockQty).ToList();
                    }
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = resstock.data.Count, StockData = resstock.data });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 备件基础信息
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string Product(string reqdata)
        {
            string res = "";
            try
            {
                Product_Request reqd = JsonConvert.DeserializeObject<Product_Request>(reqdata);
                //reqd.PhoneNumber = reqd.PhoneNumber.Replace("15827002712", "18717764701");
                reqd.key = AppSettingUtil.InsideApiKey2;
                Product_Response prespon = stockhttpdal.product(JsonConvert.SerializeObject(reqd));
                if (prespon.code == 200)
                {
                    Response_Product proinfo = ProductFactory(prespon);
                    proinfo.StockQty = GetStockQty(reqd);//库存
                    ShoppingCartModel scm = shopcartdal.GetShoppingCartByPro(reqd.UnionId, reqd.keyValue, reqd.LocationName, reqd.SerialNumber, reqd.BatchNumber);
                    proinfo.cartGoodsCount = scm != null ? scm.number : 0;//购物车数量
                    CollectAndFootPrintModel cfm = cfdal.GetCollectOrFootPrint(reqd.UnionId, reqd.keyValue, reqd.LocationName, reqd.SerialNumber, reqd.BatchNumber, "Collect");
                    proinfo.IsCollect = cfm == null ? 0 : 1;//收藏状态
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", ProductData = proinfo });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "api异常" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        private Response_Product ProductFactory(Product_Response p)
        {
            Response_Product resp = new Response_Product();
            if (p.data.Count > 0)
            {
                resp.ProductCode = p.data[0].ProductCode;
                resp.ProductName = p.data[0].ProductName;
                resp.ProductDescrCH = p.data[0].ProductDescrCH;
                resp.ProductDescrEN = p.data[0].ProductDescrEN;
                resp.PackageCH = p.data[0].PackageCH;
                resp.VendorName = p.data[0].VendorName;
                resp.SpecificationModel = p.data[0].SpecificationModel;
                resp.MadeInCH = p.data[0].MadeInCH;
                resp.MadeInEN = p.data[0].MadeInEN;
                resp.ModelCode = p.data[0].ModelCode;
                resp.QuantityUnitCH = p.data[0].QuantityUnitCH;
                resp.QuantityUnitConvertCH = p.data[0].QuantityUnitConvertCH;
                resp.QuantityUnitConvertEN = p.data[0].QuantityUnitConvertEN;
                resp.Remark = p.data[0].Remark;
                resp.IsBatchNumberMatch = p.data[0].IsBatchNumberMatch;
                resp.ImgUrl = p.data[0].ImgUrl;
            }
            return resp;
        }
        private string GetStockQty(Product_Request preq)
        {
            ShoppingCartModel red = new ShoppingCartModel()
            {
                unionid = preq.UnionId,
                VendName=preq.CompanyName,
                LocationName = preq.LocationName,
                ProductCode=preq.keyValue,
                SerialNumber=preq.SerialNumber,
                BatchNumber=preq.BatchNumber
            };
            return shopcartbll.GetStockQty(red);
        }


    }
}
