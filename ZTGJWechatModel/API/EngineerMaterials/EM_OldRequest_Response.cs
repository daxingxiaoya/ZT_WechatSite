using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_OldRequest_Response : ApiBase_Response
    {
        public OldRequestData data { set; get; }
    }

    public class OldRequestData {
        public int Total { set; get; }
        public int TotalPage { set; get; }
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
        public List<OldRequestDataListItem> List { set; get; }
    }
    public class OldRequestDataListItem
    {
        public string F_ReturnClaimUser { set; get; }
        public string F_ReturnClaimDate { set; get; }
        public string F_ReturnAddress { set; get; }
        public string F_EngineerCode { set; get; }
        public string F_WorkNo { set; get; }
        public string F_WorkCompleteDate { set; get; }
        public string F_DeviceNumber { set; get; }
        public string F_MaterialNo { set; get; }
        public string F_Description { set; get; }
        public int F_RequestQuantity { set; get; }
        public int F_ReturnQuantity { set; get; }
        public string F_QuantityUnit { set; get; }
        public string F_BatchNumber { set; get; }
        public string F_SerialNumber { set; get; }
        public int F_ReturnStatus { set; get; }
        public string F_ReturnLastDate { set; get; }
        public string F_Remark { set; get; }
        public int F_DeleteMark { set; get; }
        public string F_ID { set; get; }
        public string F_CreateUserId { set; get; }
        public string F_CreateTime { set; get; }
        public string F_ModifyUserId { set; get; }
        public string F_ModifyTime { set; get; }
    }

}
