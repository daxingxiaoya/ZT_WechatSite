using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.StaticData
{
    public class areaList
    {
        public string name { set; get; }
        public string fullname { set; get; }
        public string pinyin { set; get; }
        public List<city> city { set; get; }
        public int sortnum { set; get; }
    }
    public class city {
        public string name { set; get; }
        public List<string> area { set; get; }
    }
}
