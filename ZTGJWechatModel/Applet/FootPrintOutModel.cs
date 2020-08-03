using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    public class FootPrintOutModel
    {
        public string CreateDate { set; get; }
        public List<CollectAndFootPrintModel> footlist { set; get; }
    }
}
