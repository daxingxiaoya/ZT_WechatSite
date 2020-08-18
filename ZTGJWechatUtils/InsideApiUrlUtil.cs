using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    public class InsideApiUrlUtil
    {
        /// <summary>
        /// 发送短信
        /// Post http://192.168.192.22/WechatApi/jointac/wechat/private/getsms {key:"秘钥", to:"手机" ,text:"内容"}
        /// </summary>
        /// <returns></returns>
        public static string SMSSending
        {
            get { return AppSettingUtil.InsideApiBaseUrl + "/jointac/wechat/private/getsms"; }
        }
        #region 快递 订单 库存
        /// <summary>
        /// 运单查询
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/waybilllist 
        /// </summary>
        /// <returns></returns>
        public static string waybilllist
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/waybilllist"; }
        }
        /// <summary>
        /// 运单详细信息
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/waybillinfo 
        /// </summary>
        /// <returns></returns>
        public static string waybillinfo
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/waybillinfo"; }
        }
        /// <summary>
        /// 运单跟踪信息
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/waybilltrack 
        /// </summary>
        /// <returns></returns>
        public static string waybilltrack
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/waybilltrack"; }
        }
        /// <summary>
        /// 运单号查询备件详情
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/expressproducts 
        /// </summary>
        /// <returns></returns>
        public static string expressproducts
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/expressproducts"; }
        }
        /// <summary>
        /// 公司名称信息
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/companys
        /// </summary>
        /// <returns></returns>
        public static string companys
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/companys"; }
        }
        /// <summary>
        /// 库区信息
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/locations
        /// </summary>
        /// <returns></returns>
        public static string locations
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/locations"; }
        }
        /// <summary>
        /// 库存信息
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/stockinfo
        /// </summary>
        /// <returns></returns>
        public static string stockinfo
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/stockinfo"; }
        }
        /// <summary>
        /// 查询备件基础信息
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/product
        /// </summary>
        /// <returns></returns>
        public static string product
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/product"; }
        }

        /// <summary>
        /// 订单查询
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/orderinfo
        /// </summary>
        /// <returns></returns>
        public static string orderinfo
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/orderinfo"; }
        }
        /// <summary>
        /// 微信下单
        /// Post https://192.168.192.23:4443/api/jointac/wechat/private/createorder
        /// </summary>
        /// <returns></returns>
        public static string createorder
        {
            get { return AppSettingUtil.InsideApiBaseUrl2 + "/createorder"; }
        }
        #endregion

        #region 工程师物料管理
        #region SysUser
        /// <summary>
        /// 微信登录
        /// Post http://192.168.192.26/JointacEngineer/api/user/wechat
        /// </summary>
        /// <returns></returns>
        public static string EM_WechatLogin
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/user/wechat"; }
        }
        /// <summary>
        /// 微信修改绑定手机号
        /// Post http://192.168.192.26/JointacEngineer/api/user/wechat
        /// </summary>
        /// <returns></returns>
        public static string EM_Mobile
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/user/wechat/mobile"; }
        }
        #endregion

        #region OldReturnOrder
        /// <summary>
        /// 旧件退回列表
        /// Get http://192.168.192.26/JointacEngineer/api/oldreturnorder
        /// </summary>
        /// <returns></returns>
        public static string EM_oldreturnorder
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder"; }
        }
        /// <summary>
        /// 旧件单详情
        /// Get http://192.168.192.26/JointacEngineer/api/oldreturnorder
        /// </summary>
        /// <returns></returns>
        public static string EM_lineinfo
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder/lineinfo"; }
        }
        /// <summary>
        /// 删除旧件单
        /// Delete http://192.168.192.26/JointacEngineer/api/oldreturnorder
        /// </summary>
        /// <returns></returns>
        public static string EM_oldreturnorder_del
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder"; }
        }
        /// <summary>
        /// 新增旧件单
        /// POST http://192.168.192.26/JointacEngineer/api/oldreturnorder
        /// </summary>
        /// <returns></returns>
        public static string EM_lineinfo_add
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder"; }
        }
        /// <summary>
        /// 更新旧件单
        /// PUT http://192.168.192.26/JointacEngineer/api/oldreturnorder
        /// </summary>
        /// <returns></returns>
        public static string EM_lineinfo_put
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder"; }
        }
        /// <summary>
        /// 删除行
        /// Delete http://192.168.192.26/JointacEngineer/api/oldreturnorder/deleteline
        /// </summary>
        /// <returns></returns>
        public static string EM_oldreturnorder_deleteline
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder/deleteline"; }
        }
        /// <summary>
        /// 发货
        /// POST http://192.168.192.26/JointacEngineer/api/oldreturnorder/updatesend
        /// </summary>
        /// <returns></returns>
        public static string EM_oldreturnorder_updatesend
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder/updatesend"; }
        }
        /// <summary>
        /// 获取快递公司
        /// GET http://192.168.192.174:8092/api/oldreturnorder/express
        /// </summary>
        /// <returns></returns>
        public static string EM_oldreturnorder_express
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldreturnorder/express"; }
        }
        #endregion

        #region Engineer
        /// <summary>
        /// 工程师下拉列表(退回)
        /// Get http://192.168.192.26/JointacEngineer/api/engineer/engineerlistreturn
        /// </summary>
        /// <returns></returns>
        public static string EM_engineerlistreturn
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/engineer/engineerlistreturn"; }
        }
        #endregion

        #region OldRequestReturn
        /// <summary>
        /// 列表
        /// Get http://192.168.192.26/JointacEngineer/api/oldrequest
        /// </summary>
        /// <returns></returns>
        public static string EM_oldrequest
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldrequest"; }
        }
        /// <summary>
        /// 删除退件指令
        /// DELETE http://192.168.192.26/JointacEngineer/api/oldrequest
        /// </summary>
        /// <returns></returns>
        public static string EM_oldrequest_del
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldrequest"; }
        }
        /// <summary>
        /// 添加退件指令
        /// Post http://192.168.192.26/JointacEngineer/api/oldrequest
        /// </summary>
        /// <returns></returns>
        public static string EM_oldrequest_add
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldrequest"; }
        }
        /// <summary>
        /// 更新退件指令
        /// PUT http://192.168.192.26/JointacEngineer/api/oldrequest
        /// </summary>
        /// <returns></returns>
        public static string EM_oldrequest_up
        {
            get { return AppSettingUtil.InsideApiBaseUrl3 + "/oldrequest"; }
        }
        #endregion

        #endregion
    }
}
