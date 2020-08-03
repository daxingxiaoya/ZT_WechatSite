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
    public class ShoppingCartBll
    {
        private ShoppingCartDal shoppingcartdal = new ShoppingCartDal();
        private StockHttpDal stockhttpdal = new StockHttpDal();

        #region 获取购物车商品
        /// <summary>
        /// 根据uid获取用户购物车商品
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public string GetShoppingCart(string uid)
        {
            string res = "";
            try
            {
                List<ShoppingCartModel> sclist = shoppingcartdal.GetShoppingCart(uid);
                StockInfo_Response newstock = GetNewStock(sclist);//最新库存
                if (newstock != null && newstock.code == 200)
                {
                    //查库存
                    for (int i = 0; i < sclist.Count; i++)
                    {
                        int newstockqty = 0;
                        //先根据序(列号 批次号 备件号 库区)查库存
                        var stocks = newstock.data.Where(s => 
                                s.LocationName == sclist[i].LocationName && s.ProductCode == sclist[i].ProductCode && 
                                sclist[i].SerialNumber == s.SerialNumber && sclist[i].BatchNumber == s.BatchNumber).FirstOrDefault();
                        if (stocks != null && !string.IsNullOrEmpty(stocks.ProductCode))
                        {
                            newstockqty = Convert.ToInt32(Convert.ToDecimal(stocks.StockQty).ToString("#0"));
                        }
                        sclist[i].StockQty = newstockqty;
                    }
                }
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", goodsCount = sclist.Count, checkedGoodsCount = sclist.Where(s => s.Ischecked == 1).ToList().Count,
                    ShoppingCartData = CartDataFactory(sclist) });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障", count = 0 });
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return res;
        }
        
        /// <summary>
        /// 购物车数据加工输出
        /// </summary>
        /// <param name="clist"></param>
        /// <returns></returns>
        private List<ShoppingCartOutPut> CartDataFactory(List<ShoppingCartModel> clist)
        {
            List<ShoppingCartOutPut> resoutlist = new List<ShoppingCartOutPut>();
            clist = clist.OrderByDescending(o => o.CreateTime).ToList();
            if (clist.Count > 0)
            {
                var groups = clist.GroupBy(g => g.LocationName);
                foreach (var item in groups)
                {
                    ShoppingCartOutPut scop = new ShoppingCartOutPut();
                    List<SubShoppingCartOutPut> sublist = new List<SubShoppingCartOutPut>();
                    foreach (var subitem in item)
                    {
                        SubShoppingCartOutPut subout = new SubShoppingCartOutPut();
                        subout.unionid = subitem.unionid;
                        subout.VendName = subitem.VendName;
                        subout.LocationName = subitem.LocationName;
                        subout.ProductCode = subitem.ProductCode;
                        subout.ProductDescrEN = subitem.ProductDescrEN;
                        subout.SerialNumber = subitem.SerialNumber;
                        subout.BatchNumber = subitem.BatchNumber;
                        subout.StockQty = subitem.StockQty;
                        subout.QuantityUnitCH = subitem.QuantityUnitCH;
                        subout.Ischecked = subitem.Ischecked;
                        subout.number = subitem.number;
                        subout.ImgUrl = subitem.ImgUrl;
                        sublist.Add(subout);
                    }
                    scop.LocationName = sublist[0].LocationName;
                    scop.ShoppingCartData = sublist;
                    resoutlist.Add(scop);
                }
            }
            return resoutlist;
        }
        #endregion

        #region 查库存
        /// <summary>
        /// 获取库存数量
        /// </summary>
        /// <param name="sclist"></param>
        /// <returns></returns>
        private StockInfo_Response GetNewStock(List<ShoppingCartModel> sclist)
        {
            StockInfo_Response resstock = null;
            try
            {
                if (sclist.Count > 0)
                {
                    List<ProLocListItem> list = new List<ProLocListItem>();
                    foreach (var item in sclist)
                    {
                        ProLocListItem proitem = new ProLocListItem()
                        {
                            ProductCode = item.ProductCode,
                            LocationNames = item.LocationName
                        };
                        list.Add(proitem);
                    }
                    StockInfo_Request req = new StockInfo_Request()
                    {
                        UnionId = sclist[0].unionid,
                        CompanyName = sclist[0].VendName,
                        key = AppSettingUtil.InsideApiKey2,
                        list = list
                    };
                    resstock = stockhttpdal.stockinfo(JsonConvert.SerializeObject(req));
                    if (resstock.code != 200 || resstock.data.Count == 0)
                    {
                        resstock = null;
                    }
                }

            }
            catch (Exception ex)
            {
                resstock = null;
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return resstock;
        }

        /// <summary>
        /// 获取库存数量
        /// </summary>
        /// <param name="sclist"></param>
        /// <returns></returns>
        public string GetStockQty(ShoppingCartModel scm)
        {
            string resstockqty = "0";
            try
            {
                List<ShoppingCartModel> stockreq = new List<ShoppingCartModel>();
                stockreq.Add(scm);
                StockInfo_Response stock = GetNewStock(stockreq);//库存数量

                if (stock != null && stock.data.Count > 0)
                {
                    var substock = stock.data.Where(s => s.LocationName == scm.LocationName && s.ProductCode==scm.ProductCode && s.SerialNumber == scm.SerialNumber && s.BatchNumber == scm.BatchNumber).ToList();
                    if (substock.Count > 0)
                    {
                        resstockqty = substock[0].StockQty;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return resstockqty;
        }


        #endregion

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string AddShoppingCart(string reqdata)
        {
            string res = "";
            try
            {
                ShoppingCartModel reqmodel = JsonConvert.DeserializeObject<ShoppingCartModel>(reqdata);

                ShoppingCartModel scm= shoppingcartdal.GetShoppingCartByPro(reqmodel.unionid, reqmodel.ProductCode, reqmodel.LocationName, reqmodel.SerialNumber, reqmodel.BatchNumber);//查询购物车
                int cartnum = 0;//购物车商品数
                bool resb = false;//添加或者删除的状态
                string msgstr = "";
                if (scm != null && !string.IsNullOrEmpty(scm.ProductCode))//修改
                {
                    if (reqmodel.addtype==0)//添加
                    {
                        if (reqmodel.page == "pro")//备件详情页
                        {
                            cartnum = scm.number + reqmodel.num;
                        }
                        else//购物车页
                        {
                            cartnum = reqmodel.num;
                        }
                        string stockqty = GetStockQty(scm);//库存数量
                        if (cartnum <= Convert.ToDecimal(stockqty))
                        {
                            reqmodel.number = cartnum;
                            resb = shoppingcartdal.UpdateShoppingCart(reqmodel);
                        }
                        else
                        {
                            resb = false;
                            msgstr = "加入购物车失败，库存不足！";
                        }
                    }
                    else if (reqmodel.addtype == 1 && scm.num > 1)//购物车数量大于一允许减数量
                    {
                        if (reqmodel.page== "pro")//备件详情页
                        {
                            cartnum = scm.number - reqmodel.num;
                            cartnum = cartnum < 1 ? 1 : cartnum;//避免数量小于1
                        }
                        else//购物车页
                        {
                            cartnum = reqmodel.num;
                        }
                        
                        reqmodel.number = cartnum;
                        resb = shoppingcartdal.UpdateShoppingCart(reqmodel);
                    }
                }
                else//新增
                {
                    reqmodel.number = 1;
                    reqmodel.Ischecked = 0;
                    reqmodel.CreateTime = DateTime.Now;
                    reqmodel.UpdateTime = DateTime.Now;
                    resb = shoppingcartdal.AddShoppingCart(reqmodel);
                }
                if (resb)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "添加购物车成功！" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = string.IsNullOrEmpty(msgstr)? "添加购物车失败！" : msgstr });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 修改购物车商品选中状态
        /// </summary>
        /// <param name="reqdata"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string UpdateShoppingCartChecked(string reqdata) {
            string res = "";
            try
            {
                ShoppingCartModel reqmodel = JsonConvert.DeserializeObject<ShoppingCartModel>(reqdata);

                bool resb = shoppingcartdal.UpdateShoppingCartChecked(reqmodel);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", upstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return res;
        }

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="reqdata"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string DelShoppingCart(string reqdata)
        {
            string res = "";
            try
            {
                ShoppingCartModel reqmodel = JsonConvert.DeserializeObject<ShoppingCartModel>(reqdata);

                bool resb = shoppingcartdal.DelShoppingCart(reqmodel);
                res = JsonConvert.SerializeObject(new { code = 0, msg = "ok", upstate = resb });
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "系统故障" });
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return res;
        }


    }
}
