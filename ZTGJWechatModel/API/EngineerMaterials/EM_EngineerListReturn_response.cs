using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_EngineerListReturn_response : ApiBase_Response
    {
        public List<EMELRData> data { set; get; }
    }
    public class EMELRData{
        public string label { set; get; }
        public string value { set; get; }
        public string key { set; get; }
    }
}
