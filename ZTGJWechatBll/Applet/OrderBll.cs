using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.HttpData;
using ZTGJWechatDal.Order;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatBll.Applet
{
    public class OrderBll
    {
        private OrderHttpDal orderhttpdal = new OrderHttpDal();
        private ShoppingCartDal shoppingcartdal = new ShoppingCartDal();

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string OrderInfo(string reqdata) {
            string res = "";
            try
            {
                OrderInfo_Request req = JsonConvert.DeserializeObject<OrderInfo_Request>(reqdata);
                req.key = AppSettingUtil.InsideApiKey2;
                //req.PhoneNumber = req.PhoneNumber.Replace("15827002712", "18717764701");
                OrderInfo_Response oinforesponse = orderhttpdal.OrderInfo(JsonConvert.SerializeObject(req));

                List<OrderModel> olist = new List<OrderModel>();
                if (oinforesponse.code == 200 && oinforesponse.data.head.Count > 0)
                {
                    foreach (var item in oinforesponse.data.head)
                    {
                        OrderModel om = new OrderModel() {
                            HeadID = item.HeadID,
                            JobCode = item.JobCode,
                            WebJobCode=item.WebJobCode,
                            WaybillNumber = item.WaybillNumber,
                            ExpressCompany = item.ExpressCompany,
                            ShipperAddress = item.ShipperAddress,
                            ShipperDate = string.IsNullOrEmpty(item.ShipperDate) ? "" : Convert.ToDateTime(item.ShipperDate).ToString("yyyy-MM-dd HH:mm"),
                            ReceiverAddress = item.ReceiverAddress,
                            ReceiverContactor = item.ReceiverContactor,
                            ReceiverMobile = item.ReceiverMobile,
                            OrderType = item.OrderType,
                            OStatusImg = AppSettingUtil.PublicImg + "/OoderStatusImg/" + ((item.OrderType.Contains("已签收") || item.OrderType.Contains("完成")) ? "success" : "pack") + ".png",
                            CompleteDate = string.IsNullOrEmpty(item.CompleteDate) ? "" : Convert.ToDateTime(item.CompleteDate).ToString("yyyy-MM-dd HH:mm"),
                            CreateDate = Convert.ToDateTime(item.CreateDate).ToString("yyyy-MM-dd HH:mm")
                        };
                        var oinfos = oinforesponse.data.lines.Where(o => o.HeadID == item.HeadID).ToList();
                        om.OrderInfos = new List<OrderInfo>();
                        foreach (var subitem in oinfos)
                        {
                            OrderInfo oinfo = new OrderInfo() {
                                LocationName = subitem.LocationName,
                                ProductCode = subitem.ProductCode,
                                ProductDescrEN = subitem.ProductDescrEN,
                                Quantity = subitem.Quantity,
                                QuantityUnit = subitem.QuantityUnit,
                                SerialNumber = subitem.SerialNumber,
                                BatchNumber = subitem.BatchNumber,
                                ImgUrl = string.IsNullOrEmpty(subitem.ImgUrl) ? "" : subitem.ImgUrl
                            };
                            om.OrderInfos.Add(oinfo);
                        }
                        olist.Add(om);
                    }
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", count = olist.Count, rows = olist });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("OrderInfo异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string CreateOrder(string reqdata)
        {
            string res = "";
            try
            {
                Request_CreateOrder req = JsonConvert.DeserializeObject<Request_CreateOrder>(reqdata);

                #region 下单参数
                CreateOrder_Request request = new CreateOrder_Request();
                request.key = AppSettingUtil.InsideApiKey2;
                request.UnionId = req.UnionId;
                //request.PhoneNumber = req.PhoneNumber.Replace("15827002712", "18717764701");
                request.PhoneNumber = req.PhoneNumber;
                request.CompanyName = req.CompanyName;
                request.head = new CreateOrderHead() {
                    VendorName=req.CompanyName,
                    ReceiverName = req.addr.alias,
                    ReceiverContactor = req.addr.name,
                    ReceiverMobile = req.addr.phone,
                    ReceiverCountry = string.IsNullOrEmpty(req.addr.country) ? "" : req.addr.country,
                    ReceiverProvince = req.addr.province,
                    ReceiverCity = req.addr.city,
                    ReceiverDistrict= req.addr.area,
                    ReceiverAddress = req.addr.address,
                    CreateDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    Remark = req.oremark
                };
                request.lines = new List<CreateOrderLine>();
                foreach (var item in req.goods)
                {
                    CreateOrderLine crealine = new CreateOrderLine() {
                        ProductCode=item.ProductCode,
                        ProductDescrEN=item.ProductDescrEN,
                        SerialNumber=item.SerialNumber,
                        ShipQty=item.number,
                        LocationName=item.LocationName
                    };
                    request.lines.Add(crealine);
                }
                #endregion

                CreateOrder_Response response = orderhttpdal.CreateOrder(JsonConvert.SerializeObject(request));
                if (response.code==200)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", jobcode = response.data });
                    removecart(req);//移除购物车商品
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = response.msg.Replace("<br />", "") });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog("CreateOrder异常：" + ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 移除购物车商品
        /// </summary>
        /// <param name="coreq"></param>
        private void removecart(Request_CreateOrder coreq) {
            try
            {
                foreach (var item in coreq.goods)
                {
                    shoppingcartdal.DelShoppingCart(item);
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("CreateOrder==>removecart异常：" + ex.Message + "，" + ex.StackTrace);
            }
        }


    }
}
