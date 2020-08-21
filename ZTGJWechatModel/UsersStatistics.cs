using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel
{
    public class UsersStatistics
    {
        public UsersStatistics() {
            total = 0; completetotal = 0; emtotal = 0; stocktotal = 0; oafollowtotal = 0;
        }
        /// <summary>
        /// 用户总量
        /// </summary>
        public int total { set; get; }
        /// <summary>
        /// 小程序完整功能用户量
        /// </summary>
        public int completetotal { set; get; }
        /// <summary>
        /// 工程师物料功能使用数量
        /// </summary>
        public int emtotal { set; get; }
        /// <summary>
        /// 库存订单功能使用数量
        /// </summary>
        public int stocktotal { set; get; }
        /// <summary>
        /// 公众号关注数量
        /// </summary>
        public int oafollowtotal { set; get; }
    }
}
