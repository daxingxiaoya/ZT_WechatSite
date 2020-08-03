using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.StaticData
{
    public class zipCode
    {
        public string id { set; get; }
        public string name { set; get; }
        public List<child1> child { set; get; }
    }

    public class child1
    {
        public string id { set; get; }
        public string name { set; get; }
        public List<child2> child { set; get; }
    }

    public class child2
    {
        public string id { set; get; }
        public string name { set; get; }
        public string zipcode { set; get; }

    }

}
