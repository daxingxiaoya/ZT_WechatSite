using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_LineInfo_Response : ApiBase_Response
    {
        public List<EM_LineInfo_Item> data { set; get; }
    }

    public class EM_LineInfo_Item {
        public string F_HeadID { set; get; }
        public string F_ReturnClaimUser { set; get; }
        public string F_ReturnClaimDate { set; get; }
        public string F_ReturnAddress { set; get; }
        public string F_EngineerCode { set; get; }
        public string F_WorkNo { set; get; }
        public string F_WorkCompleteDate { set; get; }
        public string F_DeviceNumber { set; get; }
        public string F_MaterialNo { set; get; }
        public string F_Description { set; get; }
        public string F_RequestQuantity { set; get; }
        public string F_ReturnQuantity { set; get; }
        public string F_QuantityUnit { set; get; }
        public string F_BatchNumber { set; get; }
        public string F_SerialNumber { set; get; }
        public string F_ReturnStatus { set; get; }
        public string F_ReturnLastDate { set; get; }
        public string F_Remark { set; get; }
        public string F_IsEngineerAdd { set; get; }
        public string F_DeleteMark { set; get; }
        public string F_ID { set; get; }
        public string F_CreateUserId { set; get; }
        public string F_CreateTime { set; get; }
        public string F_ModifyUserId { set; get; }
        public string F_ModifyTime { set; get; }
    }
}
