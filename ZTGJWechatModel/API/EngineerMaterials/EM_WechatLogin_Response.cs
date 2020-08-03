using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_WechatLogin_Response : ApiBase_Response
    {
        public string Token { set; get; }
        public WechatLogin_Response_data data { set; get; }
    }
    public class WechatLogin_Response_data {
        public string F_ID { set; get; }
        public string F_UserName { set; get; }
        public string F_DeptId { set; get; }
        public string F_Email { set; get; }
        public string F_Mobile { set; get; }
        public string F_Gender { set; get; }
        public string F_EnabledMark { set; get; }
        public string F_IsEngineer { set; get; }
        public string F_CreateTime { set; get; }
        public string F_CreateUserId { set; get; }
        public string F_ModifyTime { set; get; }
        public string F_ModifyUserId { set; get; }
        public string F_RoleIds { set; get; }
        public string F_EngineerCode { set; get; }
        public string F_UnionId { set; get; }
        public string F_WechatName { set; get; }
        public string F_WechatMobile { set; get; }
    }
}
