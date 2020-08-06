using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API.EngineerMaterials
{
    public class EM_OldReturnOrder_Request
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex { set; get; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { set; get; }
        /// <summary>
        /// 排序字段 example: ",F_CreateTime desc"
        /// </summary>
        public string orderField { set; get; }
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string search { set; get; }  
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end { set; get; }
        /// <summary>
        /// 扩展字段 1:系统单号 2：快递单号 3：收件公司
        /// </summary>
        public string extension { set; get; }
    }
}
