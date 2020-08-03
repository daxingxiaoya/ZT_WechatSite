using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal;
using ZTGJWechatDal.HttpData;
using ZTGJWechatModel;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatBll.Applet
{
    public class ApCommonBll
    {
        private AppletCommonDal apcomdal = new AppletCommonDal();
        private UsersDal userdal = new UsersDal();
        private HttpCommonDal httpcomdal = new HttpCommonDal();

        public string GetSession_Key(string code)
        {
            string res = "";
            try
            {
                ResponseApBaseUserInfo apbaseu= apcomdal.GetApBaseUserInfo(code);
                res = JsonConvert.SerializeObject(apbaseu);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("错误信息：" + ex.StackTrace + "\r\n" + ex.Message);
            }
            return res;
        }
        /// <summary>
        /// 小程序用户信息
        /// </summary>
        /// <returns></returns>
        public string GetAppletUserInfo(string reqdata)
        {
            ResponseUserInfo res = new ResponseUserInfo();
            try
            {
                //RequestUserInfo reqstr = new RequestUserInfo();
                RequestUserInfo reqstr = JsonConvert.DeserializeObject<RequestUserInfo>(reqdata);//请求参数反序列化
                LogHelper.InfoLog("GetAppletUserInfo请求参数:" + reqdata);
                string userdata = AES_decrypt(reqstr.userEncryptedData, reqstr.session_key, reqstr.userIv);//微信用户信息解密
                WeChatUserData user = JsonConvert.DeserializeObject<WeChatUserData>(userdata);
                string phonedata = AES_decrypt(reqstr.phoneEncryptedData, reqstr.session_key, reqstr.phoneIv);//联系方式数据解密
                WeChatPhoneData phone = JsonConvert.DeserializeObject<WeChatPhoneData>(phonedata);
                LogHelper.InfoLog("用户信息解密:"+ userdata+ "，联系方式数据解密:"+ phonedata);

                UsersModel userinfo = new UsersModel();
                List<UsersModel> users = userdal.GetUserInfo(new UsersModel() { unionid = reqstr.unionid });
                if (users.Count > 0)
                {
                    if (string.IsNullOrEmpty(users[0].mobilephone) || string.IsNullOrEmpty(users[0].appletopenid))//手机号或者小程序openid为空则需要修改数据库的用户信息
                    {
                        userdal.UpdateUserInfo(userinfo);
                    }
                    userinfo = UserDataChange(user, phone, reqstr.session_key);//入库数据转换组装 
                    res = UserDataChange(userinfo); //输出数据转换组装 
                    res.states = "0";
                    res.msg = "查询成功";
                    res.empowerStatus = users[0].empowerStatus;
                    res.mobilephone = users[0].mobilephone;
                    res.companyname = users[0].companyname;
                }
                else//数据库没有当前用户去入库
                {
                    userinfo = UserDataChange(user, phone, reqstr.session_key);//入库数据转换组装 
                    if (userdal.AddUser(userinfo))
                    {
                        res.states = "0";
                        res.msg = "操作成功";
                    }
                    else
                    {
                        res.states = "1";
                        res.msg = "开小差了...";
                    }
                    res = UserDataChange(userinfo);//输出数据转换组装 
                    res.empowerStatus = 0;
                    res.mobilephone = "";
                    res.companyname = "";
                }
            }
            catch (Exception ex)
            {
                res.states = "-1";
                res.msg = "开小差了...";
                LogHelper.ErrorLog("错误信息：" + ex.StackTrace + "\r\n" + ex.Message);
            }
            return JsonConvert.SerializeObject(res);
        }

        #region 数据处理
        /// <summary>
        /// 数据转换组装 入库数据
        /// </summary>
        /// <returns></returns>
        private UsersModel UserDataChange(WeChatUserData user, WeChatPhoneData phone,string session_key)
        {
            UsersModel resu = new UsersModel() {
                mobilephone = phone.purePhoneNumber,
                nickname = user.nickName,
                country=user.country,
                city = user.city,
                province = user.province,
                appletopenid = user.openId,
                unionid = user.unionId,
                session_key = session_key,
                headimgurl = user.avatarUrl,
                language = user.language,
                sex = Convert.ToInt32(!string.IsNullOrEmpty(user.gender) ? user.gender : "0")
            };
            return resu;
        }
        /// <summary>
        /// 数据转换组装 输出数据
        /// </summary>
        /// <returns></returns>
        private ResponseUserInfo UserDataChange(UsersModel user)
        {
            ResponseUserInfo resu = new ResponseUserInfo()
            {
                mobilephone = user.mobilephone,
                nickname = user.nickname,
                city = user.city,
                province = user.province,
                country = user.country,
                gender = user.sex.ToString()
            };
            return resu;
        }
        /// <summary>
        /// 微信数据解密
        /// </summary>
        /// <param name="encryptedDataStr"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string AES_decrypt(string encryptedDataStr, string key, string iv)
        {
            RijndaelManaged rijalg = new RijndaelManaged();
            //-----------------    
            //设置 cipher 格式 AES-128-CBC    
            rijalg.KeySize = 128;
            rijalg.Padding = PaddingMode.PKCS7;
            rijalg.Mode = CipherMode.CBC;
            
            rijalg.Key = Convert.FromBase64String(key);
            rijalg.IV = Convert.FromBase64String(iv);

            byte[] encryptedData = Convert.FromBase64String(encryptedDataStr);
            //解密    
            ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);

            string result = "";

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        result = srDecrypt.ReadToEnd();
                    }
                }
            }

            return result;
        }
        #endregion

        #region 验证码
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public string SendVerificationCode(string phonenum)
        {
            string res = "";
            bool sendstate = false;//发送短信状态
            try
            {
                string redisKey = RedisKeys.CAPTCHAkey + phonenum;//key
                string content = RedisHelper.Get<string>(redisKey, (long)0);//获取redis缓存

                string contxt = "";
                if (string.IsNullOrEmpty(content))
                {
                    res = new Random().Next(100000, 999999).ToString();
                    contxt = "验证码" + res + "，请在5分钟内使用";
                    sendstate = httpcomdal.SMSSending(phonenum, contxt);
                    if (sendstate)
                    {
                        //写入缓存 缓存5分钟
                        RedisHelper.Set<string>(redisKey, res, new TimeSpan(0, 5, 0), (long)0);
                    }
                }
                else
                {
                    res = content;
                    contxt = "验证码" + res + "，请在5分钟内使用";
                    sendstate = httpcomdal.SMSSending(phonenum, contxt);
                }

                if (sendstate)
                {
                    res = JsonConvert.SerializeObject(new { code = 0, msg = "ok" });
                }
                else
                {
                    res = JsonConvert.SerializeObject(new { code = 10002, msg = "发送验证码失败" });
                }
            }
            catch (Exception ex)
            {
                res = JsonConvert.SerializeObject(new { code = 10003, msg = ex.Message });
                LogHelper.ErrorLog(ex.Message + "，" + ex.StackTrace);
            }
            return res;
        }
        #endregion



    }
}
