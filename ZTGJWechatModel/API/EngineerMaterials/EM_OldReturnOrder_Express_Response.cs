using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_OldReturnOrder_Express_Response : ApiBase_Response
    {
        public List<Express> data { set; get; }
    }
    public class Express {
        public string expressCode { set; get; }
        public string expressName { set; get; }
    }
}
