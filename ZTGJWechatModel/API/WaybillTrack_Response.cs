using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class WaybillTrack_Response : ApiBase_Response
    {
        public string data { set; get; }
        public List<WaybillTrackItem> datalist
        {
            get
            {
                return JsonConvert.DeserializeObject<List<WaybillTrackItem>>(data);
            }
        }
    }

    public class WaybillTrackItem {
        /// <summary>
        /// 时间点
        /// </summary>
        public string datetime { set; get; }
        /// <summary>
        /// 运输详情
        /// </summary>
        public string scan { set; get; }
        /// <summary>
        /// 地点
        /// </summary>
        public string location { set; get; }
        /// <summary>
        /// 运单状态
        /// </summary>
        public string describe { set; get; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string waybillnumber { set; get; }
    }
}
