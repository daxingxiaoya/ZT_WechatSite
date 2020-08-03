using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class Companys_Response : ApiBase_Response
    {
        public List<comdataitem> data { set; get; }
    }
    public class comdataitem {
        public string ShortName { set; get; }
        public string FullName { set; get; }
    }
}
