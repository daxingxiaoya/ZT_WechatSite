using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    /// <summary>
    /// 数据库连接配置对象
    /// </summary>
    public class DBConnectionStringConfig
    {
        private static DBConnectionStringConfig _default;

        /// <summary>
        /// 默认配置对象
        /// </summary>
        public static DBConnectionStringConfig Default { get { return _default; } }

        /// <summary>
        /// DirectFareCenter
        /// </summary>
        public string WechatServerDBReadConnStr { set; get; }

        /// <summary>
        /// 初始化配置对象
        /// </summary>
        public static void InitDefault(DBConnectionStringConfig defaultConfig)
        {
            _default = defaultConfig;
        }
    }
}
