using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    public class ReservoirAreaModel
    {
        public int Id { set; get; }
        /// <summary>
        /// 库区号
        /// </summary>
        public string ReservoirAreaNo { set; get; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AnotherName { set; get; }
        /// <summary>
        /// 供应商名
        /// </summary>
        public string VendName { set; get; }
        public int Enable { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime UpdateTime { set; get; }
    }
    public class ReservoirAreaexportModel
    {
        /// <summary>
        /// 库区号
        /// </summary>
        public string ReservoirAreaNo { set; get; }
        /// <summary>
        /// 别名
        /// </summary>
        public string AnotherName { set; get; }
    }
}
