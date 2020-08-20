using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.Bussiness;
using ZTGJWechatModel.Bussiness;

namespace ZTGJWechatBll.Bussiness
{
    public class WechatBll
    {
        #region 默认构造
        private WechatDal wechatdal = new WechatDal();
        #endregion

        /// <summary>
        /// 根据来源获取未绑定用户列表
        /// </summary>
        /// <param name="source">em:工程师管理系统  stock:库存系统</param>
        /// <returns></returns>
        public List<Bus_GetUsers_response> GetUnboundUsers(string source)
        {
            return wechatdal.GetUnboundUsers(source);
        }

        public bool BindingUsers(Bus_BindingUsers_Request req)
        {
            return wechatdal.BindingUsers(req);
        }
    }
}
