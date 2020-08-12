using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_OldRequest_Add_Request
    {
        public string F_ID { set; get; }
        /// <summary>
        /// 指令人
        /// </summary>
        public string F_ReturnClaimUser { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string F_ReturnClaimDate { set; get; }
        /// <summary>
        /// 退回地
        /// </summary>
        public string F_ReturnAddress { set; get; }
        /// <summary>
        /// 工程师代码
        /// </summary>
        public string F_EngineerCode { set; get; }
        /// <summary>
        /// 工作单
        /// </summary>
        public string F_WorkNo { set; get; }
        public string F_WorkCompleteDate { set; get; }
        public string F_DeviceNumber { set; get; }
        public string F_MaterialNo { set; get; }
        public string F_Description { set; get; }
        public int F_RequestQuantity { set; get; }
        public int F_ReturnQuantity { set; get; }
        public string F_QuantityUnit { set; get; }
        public string F_BatchNumber  { set; get; }
        public string F_SerialNumber { set; get; }
        public int F_ReturnStatus { set; get; }
        public string F_ReturnLastDate { set; get; }
        public string F_Remark { set; get; }
        public int F_DeleteMark { set; get; }
    }
}
