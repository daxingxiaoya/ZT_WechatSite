using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_OldRequest_Request
    {
        public int pageIndex { set; get; }
        public int pageSize { set; get; }
        public string orderField { set; get; }
        public string search { set; get; }
        public string start { set; get; }
        public string end { set; get; }
        public string extension { set; get; }
    }
}
