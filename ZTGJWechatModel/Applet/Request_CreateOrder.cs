using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Common;

namespace ZTGJWechatModel.Applet
{
    public class Request_CreateOrder : ApiBase_Request
    {
        public Request_CreateOrder() {
            oremark = "";
        }
        /// <summary>
        /// 地址
        /// </summary>
        public AddressModel addr { set; get; }
        /// <summary>
        /// 商品
        /// </summary>
        public List<ShoppingCartModel> goods { set; get; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string oremark { set; get; }
    }
}
