using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class Product_Request : ApiBase_Request
    {
        public string keyValue { set; get; }
        public string LocationName { set; get; }
        public string SerialNumber { set; get; }
        public string BatchNumber { set; get; }
    }

}
