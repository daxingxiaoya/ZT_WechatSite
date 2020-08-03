using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class WayBillInfo_Response : ApiBase_Response
    {
        public string data { set; get; }
        public List<WayBillInfoItem> datalist {
            get
            {
                return JsonConvert.DeserializeObject<List<WayBillInfoItem>>(data);
            }
        }
    }
    public class WayBillInfoItem
    {
        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { set; get; }
        /// <summary>
        /// 运输公司
        /// </summary>
        public string ExpressCompany { set; get; }
        /// <summary>
        /// 寄件人
        /// </summary>
        public string Sender { set; get; }
        /// <summary>
        /// 寄 固话
        /// </summary>
        public string Phone { set; get; }
        /// <summary>
        /// 寄 手机号
        /// </summary>
        public string FromMobilePhone { set; get; }
        /// <summary>
        /// 寄 公司
        /// </summary>
        public string Company { set; get; }
        /// <summary>
        /// 寄 省份
        /// </summary>
        public string Province { set; get; }
        /// <summary>
        /// 寄 市
        /// </summary>
        public string City { set; get; }
        /// <summary>
        /// 寄 区县
        /// </summary>
        public string County { set; get; }
        /// <summary>
        /// 寄 详细地址
        /// </summary>
        public string DetailAddress { set; get; }
        /// <summary>
        /// 寄 邮编
        /// </summary>
        public string PostCode { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { set; get; }
        /// <summary>
        /// 可到达时间
        /// </summary>
        public string CanArriveDate { set; get; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver { set; get; }
        /// <summary>
        /// 收 固话
        /// </summary>
        public string RPhone { set; get; }
        /// <summary>
        /// 收 手机号
        /// </summary>
        public string ToMobilePhone { set; get; }
        /// <summary>
        /// 收 公司
        /// </summary>
        public string RCompany { set; get; }
        /// <summary>
        /// 收 省份
        /// </summary>
        public string RProvince { set; get; }
        /// <summary>
        /// 收 城市
        /// </summary>
        public string RCity { set; get; }
        /// <summary>
        /// 收 区县
        /// </summary>
        public string RCounty { set; get; }
        /// <summary>
        /// 收 地址详细信息
        /// </summary>
        public string RDetail { set; get; }
        /// <summary>
        /// 收 邮政编码
        /// </summary>
        public string RPostCode { set; get; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { set; get; }
        /// <summary>
        /// 商品尺寸
        /// </summary>
        public string GoodsSize { set; get; }
        /// <summary>
        /// 目的地（三位数字）
        /// </summary>
        public string DestCode { set; get; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ServiceTypeText { set; get; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentText { set; get; }
        /// <summary>
        /// 体积重量
        /// </summary>
        public string VolumeWt { set; get; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { set; get; }
        /// <summary>
        /// 包裹件数
        /// </summary>
        public string PackageCount { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { set; get; }
        /// <summary>
        /// 工作单
        /// </summary>
        public string JobCode { set; get; }
    }
}
