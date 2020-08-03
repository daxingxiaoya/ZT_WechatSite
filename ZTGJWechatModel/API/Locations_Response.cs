using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class Locations_Response : ApiBase_Response
    {
        public List<locdataitem> data { set; get; }
    }
    public class locdataitem
    {
        public string Code { set; get; }
        public string FieldCH { set; get; }
        public string FieldZN { set; get; }
    }
}
