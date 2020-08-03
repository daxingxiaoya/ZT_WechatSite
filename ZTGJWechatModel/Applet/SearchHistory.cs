using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.Applet
{
    public class SearchHistory
    {
        public int Id { set; get; }
        public string unionid { set; get; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { set; get; }
        /// <summary>
        /// 来源 0未知 1公众号 2小程序
        /// </summary>
        public int source { set; get; }
        /// <summary>
        /// 内容模块 express:快递,reservoirarea:库区
        /// </summary>
        public string modular { set; get; }
        public DateTime CreateTime { set; get; }
    }
}
