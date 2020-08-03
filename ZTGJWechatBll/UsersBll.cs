using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatBll.Applet.EM;
using ZTGJWechatDal;
using ZTGJWechatDal.HttpData;
using ZTGJWechatModel;
using ZTGJWechatModel.OfficialAccount;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatBll
{
    public class UsersBll
    {
        private UserInfoDal userinfodal = new UserInfoDal();//http数据来源
        private UsersDal usersdal = new UsersDal();//sql数据库来源
        private EM_UserInfoBll emubll = new EM_UserInfoBll();

        /// <summary>
        /// 获取公众号的微信用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public ResponseUserInfo GetOAUserInfo(string openid, string access_token)
        {
            return userinfodal.GetOAUserInfo(openid, access_token);
        }

        #region sql数据库User信息
        /// <summary>
        /// 获取sql数据库用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<UsersModel> GetUserInfo(string openid) {
            return usersdal.GetUserInfo(openid);
        }
        /// <summary>
        /// 根据调件获取sql数据库用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<UsersModel> GetUserInfo(UsersModel u) {
            return usersdal.GetUserInfo(u);
        }
        /// <summary>
        /// 存储用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(UsersModel model) {
            return usersdal.AddUser(model);
        }
        /// <summary>
        /// 更新订阅状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="status">0未关注 1关注</param>
        /// <returns></returns>
        public bool UpdateStatus(int uid,int status) {
            return usersdal.UpdateStatus(uid, status);
        }
        /// <summary>
        /// 获取sql数据库微信用户信息 带分页
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<UsersModel> GetUsersByPage(int index, int pagesize, out int totalcount) {
            return usersdal.GetUsersByPage(index,pagesize,out totalcount);
        }
        #endregion

        #region 小程序用
        /// <summary>
        /// 根据uid获取用户授权信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetUserEmpower(string uid) {
            UserEmpower upower = new UserEmpower();
            try
            {
                if (!string.IsNullOrEmpty(uid))
                {
                    string redisKey = RedisKeys.UserEmpowerKey + uid;//key
                    string content = RedisHelper.Get<string>(redisKey, (long)0);//获取redis缓存

                    List<UsersModel> ulist = new List<UsersModel>();

                    if (content == null || content.ToString() == "")
                    {
                        ulist = usersdal.GetUserEmpower(uid);
                        if (ulist.Count > 0)
                        {
                            //写入缓存 缓存12小时
                            RedisHelper.Set<string>(redisKey, JsonConvert.SerializeObject(ulist), new TimeSpan(12, 0, 0), (long)0);
                        }
                    }
                    else
                    {
                        ulist = JsonConvert.DeserializeObject<List<UsersModel>>(content);
                    }
                    if (ulist.Count > 0)
                    {
                        upower.code = 0;
                        upower.msg = "ok";
                        upower.unionid = ulist[0].unionid;
                        upower.powerApMenu = ulist[0].powerApMenu;
                        upower.empowerStatus = ulist[0].empowerStatus;

                        upower.nickname = ulist[0].nickname;
                        upower.companyname = ulist[0].companyname;
                        upower.avatarUrl = ulist[0].headimgurl;
                        upower.mobilephone = ulist[0].mobilephone;
                        upower.companyname = ulist[0].companyname;
                    }
                }
                else
                {
                    upower.code = 10002;
                    upower.msg = "参数不正确";
                }
            }
            catch (Exception ex)
            {
                upower.code = 10002;
                upower.msg = ex.Message;
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return JsonConvert.SerializeObject(upower);
        }
        /// <summary>
        /// 根据uid获取用户公司信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetUserCompany(string uid)
        {
            UserEmpower upower = new UserEmpower();
            try
            {
                if (!string.IsNullOrEmpty(uid))
                {
                    string redisKey = RedisKeys.UserEmpowerKey + uid;//AccessTokenkey
                    string content = RedisHelper.Get<string>(redisKey, (long)1);//获取redis缓存

                    List<UsersModel> ulist = new List<UsersModel>();

                    if (content == null || content.ToString() == "")
                    {
                        ulist = usersdal.GetUserEmpower(uid);
                        if (ulist.Count > 0)
                        {
                            //写入缓存 缓存12小时
                            RedisHelper.Set<string>(redisKey, JsonConvert.SerializeObject(ulist), new TimeSpan(12, 0, 0), (long)1);
                        }
                    }
                    else
                    {
                        ulist = JsonConvert.DeserializeObject<List<UsersModel>>(content);
                    }
                    if (ulist.Count > 0)
                    {
                        upower.code = 0;
                        upower.msg = "ok";
                        upower.companyname = ulist[0].companyname;
                        upower.unionid = ulist[0].unionid;
                        upower.powerApMenu = ulist[0].powerApMenu;
                        upower.empowerStatus = ulist[0].empowerStatus;
                    }
                }
                else
                {
                    upower.code = 10002;
                    upower.msg = "参数不正确";
                }
            }
            catch (Exception ex)
            {
                upower.code = 10002;
                upower.msg = ex.Message;
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return JsonConvert.SerializeObject(upower);
        }

        /// <summary>
        /// 小程序用户更新公司名和用户名
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string UpdateUserCompany(string reqdata)
        {
            string res = "";
            try
            {
                ZTGJWechatModel.Applet.RequestUserInfo req = JsonConvert.DeserializeObject<ZTGJWechatModel.Applet.RequestUserInfo>(reqdata);
                if (usersdal.UpdateUserCompany(req))
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "故障" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message });
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// 小程序用户更新手机号
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        public string UpdateUserPhone(string reqdata)
        {
            string res = "";
            try
            {
                ZTGJWechatModel.Applet.RequestUserInfo req = JsonConvert.DeserializeObject<ZTGJWechatModel.Applet.RequestUserInfo>(reqdata);
                string redisKey = RedisKeys.CAPTCHAkey + req.newphone;//key
                string vcode= RedisHelper.Get<string>(redisKey, (long)0);//获取redis缓存
                if (!string.IsNullOrEmpty(vcode) && req.verificationcode == vcode)
                {
                    if (usersdal.UpdateUserPhone(req))
                    {
                        res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                    }
                    else
                    {
                        res = JsonConvert.SerializeObject(new { code = 10002, msg = "系统故障" });
                    }
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "验证码不正确" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = "服务器开小差了" });
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            return res;
        }
        #endregion

        #region 后台管理
        /// <summary>
        /// 小程序用户 用户权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserEmpower(UsersModel u) {
            return usersdal.UpdateUserEmpower(u);
        }
        #endregion

    }
}
