using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class OrderInfo_Response : ApiBase_Response
    {
        public datainfo data { set; get; }
        
    }
    public class datainfo {
        public List<Orderhead> head { set; get; }
        public List<Orderline> lines { set; get; }
    }
    public class Orderhead {
        public int HeadID { set; get; }
        /// <summary>
        /// 工作单
        /// </summary>
        public string JobCode { set; get; }
        /// <summary>
        /// web工作单
        /// </summary>
        public string WebJobCode { set; get; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderType { set; get; }
        /// <summary>
        /// 发货地址
        /// </summary>
        public string ShipperAddress { set; get; }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string ReceiverAddress { set; get; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiverContactor { set; get; }
        /// <summary>
        /// s手机电话
        /// </summary>
        public string ReceiverMobile { set; get; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { set; get; }
        /// <summary>
        /// 运输公司
        /// </summary>
        public string ExpressCompany { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { set; get; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public string ShipperDate { set; get; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public string CompleteDate { set; get; }
    }
    public class Orderline
    {
        public int HeadID { set; get; }
        /// <summary>
        /// 备件号
        /// </summary>
        public string ProductCode { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnit { set; get; }
        /// <summary>
        /// 英文描述
        /// </summary>
        public string ProductDescrEN { set; get; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { set; get; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { set; get; }
        /// <summary>
        /// 库区
        /// </summary>
        public string LocationName { set; get; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { set; get; }
    }

}
