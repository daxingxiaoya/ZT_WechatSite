using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_OldReturnOrder_Response : ApiBase_Response
    {
        public EM_OldReturnOrder_Response_data data { set; get; }
    }

    public class EM_OldReturnOrder_Response_data {
        public int Total { set; get; }
        public int TotalPage { set; get; }
        public List<EM_OldReturnOrder_Response_data_list> List { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public int Skip { set; get; }
        public string OrderField { set; get; }
        public string Search { set; get; }
        public string Start { set; get; }
        public string End { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public string Extension { set; get; }
    }
    public class EM_OldReturnOrder_Response_data_list {
        public int F_ReturnStatus { set; get; }
        public string F_ShipProvince { set; get; }
        public string F_ShipCity { set; get; }
        public string F_ShipDistrict { set; get; }
        public string F_ShipAddress { set; get; }
        public string F_ShipContact { set; get; }
        public string F_ShipMobile { set; get; }
        public string F_ShipCompany { set; get; }
        public string F_ReceiveCompany { set; get; }
        public string F_ReceiveProvince { set; get; }
        public string F_ReceiveCity { set; get; }
        public string F_ReceiveDistrict { set; get; }
        public string F_ReceiveAddress { set; get; }
        public string F_ReceiveContact { set; get; }
        public string F_ReceiveMobile { set; get; }
        public string F_WaybillAgent { set; get; }
        public string F_WaybillNumber { set; get; }
        public string F_ETA { set; get; }
        public string F_OldReturnNumber { set; get; }
        public int F_DeleteMark { set; get; }
        public string F_ID { set; get; }
        public string F_CreateUserId { set; get; }
        public string F_CreateTime { set; get; }
        public string F_ModifyUserId { set; get; }
        public string F_ModifyTime { set; get; }
    }
}
