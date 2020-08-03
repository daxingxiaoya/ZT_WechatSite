using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ZTGJWechatBll;
using ZTGJWechatBll.OfficialAccount;
using ZTGJWechatModel;
using ZTGJWechatModel.EnumModel;
using ZTGJWechatModel.OfficialAccount;
using ZTGJWechatUtils;
using ZTGJWechatUtils.ConvertHelper;

namespace ZTGJWechatWebsite.Controllers
{
    public class ApiOAController : Controller
    {
        #region 默认构造器
        private UsersBll usersbll = new UsersBll();
        private TokenBll tokenbll = new TokenBll();
        private string access_token = string.Empty;//token
        private ResponseUserInfo wx_UInfo = new ResponseUserInfo();//微信的用户信息
        private List<UsersModel> users = new List<UsersModel>();//sql数据库用户信息
        private MessagePushBll msgpushbll = new MessagePushBll();
        #endregion

        #region 公众号验证+交互接口
        public void wx()
        {
            try
            {
                LogHelper.InfoLog("Http请求类型：" + HttpContext.Request.RequestType);
                #region get验证
                if (HttpContext.Request.RequestType == "GET")
                {
                    #region 验证请求来源是否是微信
                    string signature = Request["signature"]?.ToString();
                    string timestamp = Request["timestamp"]?.ToString();
                    string nonce = Request["nonce"]?.ToString();
                    string echostr = Request["echostr"]?.ToString();
                    string token = "JointacWechatServer";
                    List<string> list = new List<string>() { token, timestamp, nonce };
                    list.Sort();
                    string data = string.Join("", list);
                    byte[] temp1 = Encoding.UTF8.GetBytes(data);
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    byte[] temp2 = sha.ComputeHash(temp1);

                    var hashCode = BitConverter.ToString(temp2);
                    hashCode = hashCode.Replace("-", "").ToLower();

                    if (hashCode == signature)
                    {
                        Response.Write(echostr);
                        Response.End();
                    }
                    #endregion
                }
                #endregion
                else
                {
                    //ProcessPost(Request);
                    ProcessPostNew(Request);
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("公众号验证接口主入口异常：" + ex.Message + "，追踪：" + ex.StackTrace);
            }
        }

        private void ProcessPostNew(HttpRequestBase request)
        {
            try
            {
                var istream = request.InputStream;
                byte[] temp = new byte[istream.Length];
                istream.Read(temp, 0, (int)istream.Length);
                string postXml = Encoding.UTF8.GetString(temp);//微信XML请求信息
                LogHelper.InfoLog("微信XML请求信息: " + postXml);

                string logstr = "";//日志信息
                string resxml = "欢迎关注上海展通国际物流有限公司!";//返回信息

                XElement xdoc = XElement.Parse(postXml);//xml解析
                MsgType type = (MsgType)Enum.Parse(typeof(MsgType), xdoc.Element("MsgType").Value.ToUpper());//消息类型
                switch (type)
                {
                    case MsgType.TEXT:
                        TextMessage textmsg = OAConvert.ConvertObj<TextMessage>(postXml);
                        resxml = BackCommon.BackText(textmsg.FromUserName, textmsg.ToUserName, int.Parse(textmsg.CreateTime), "text", "文字用于沟通客服和查询快递信息，当前信息:" + textmsg.Content);
                        logstr += "TEXT信息";
                        break;
                    case MsgType.IMAGE:
                        //ImgMessage imgmsg = OAConvert.ConvertObj<ImgMessage>(postXml);
                        //resxml = BackCommon.BackText(imgmsg.FromUserName, imgmsg.ToUserName, int.Parse(imgmsg.CreateTime), "text", "图片临时素材地址：" + imgmsg.PicUrl);
                        //logstr += "IMAGE信息";
                        break;
                    case MsgType.VIDEO:
                        //VideoMessage videomsg = OAConvert.ConvertObj<VideoMessage>(postXml);
                        //resxml = BackCommon.BackText(videomsg.FromUserName, videomsg.ToUserName, int.Parse(videomsg.CreateTime), "text", "视频媒体ID" + videomsg.MediaId);
                        //logstr += "VIDEO信息";
                        break;
                    case MsgType.VOICE:
                        //VoiceMessage voicemsg = OAConvert.ConvertObj<VoiceMessage>(postXml);
                        //resxml = BackCommon.BackText(voicemsg.FromUserName, voicemsg.ToUserName, int.Parse(voicemsg.CreateTime), "text", "语音识别结果：" + voicemsg.Recognition);
                        //logstr += "VOICE信息";
                        break;
                    case MsgType.LINK:
                        //LinkMessage linkmsg = OAConvert.ConvertObj<LinkMessage>(postXml);
                        //resxml = BackCommon.BackText(linkmsg.FromUserName, linkmsg.ToUserName, int.Parse(linkmsg.CreateTime), "text", "LINK地址：" + linkmsg.Url);
                        //logstr += "LINK信息";
                        break;
                    case MsgType.LOCATION:
                        //LocationMessage locationmsg = OAConvert.ConvertObj<LocationMessage>(postXml);
                        //resxml = BackCommon.BackText(locationmsg.FromUserName, locationmsg.ToUserName, int.Parse(locationmsg.CreateTime), "text", "位置识别结果：" + locationmsg.Label + "，经度" + locationmsg.Location_Y + "，纬度" + locationmsg.Location_X);
                        //logstr += "LOCATION信息";
                        break;
                    case MsgType.EVENT://事件类型
                        {
                            var eventtype = (Event)Enum.Parse(typeof(Event), xdoc.Element("Event").Value.ToUpper());
                            BaseMessage xmlmsg = null;//微信请求公用信息
                            EventNormalMenuMessage eventNomal = null;//普通菜单事件click和view
                            switch (eventtype)
                            {
                                case Event.SUBSCRIBE://关注
                                    xmlmsg = OAConvert.ConvertObj<BaseMessage>(postXml);//请求数据解析
                                    logstr += xmlmsg.FromUserName + "关注了公众号";
                                    #region 用户信息新增或者更新
                                    bool resstate = false;
                                    users = usersbll.GetUserInfo(xmlmsg.FromUserName);//查询用户信息
                                    if (users.Count > 0)//有用户信息则修改用户订阅状态
                                    {
                                        resstate = usersbll.UpdateStatus(users[0].id, 1);//修改用户订阅状态 0未关注 1关注
                                    }
                                    else//存储用户信息
                                    {
                                        access_token = tokenbll.GetOAAccessToken();//获取token 
                                        wx_UInfo = usersbll.GetOAUserInfo(xmlmsg.FromUserName, access_token);//去微信获取用户信息
                                        if (wx_UInfo != null & wx_UInfo.subscribe != 0)
                                        {
                                            resstate = usersbll.AddUser(WUserInfoToUsers(wx_UInfo));//新增用户数据
                                        }
                                    }
                                    if (resstate)
                                    {
                                        logstr += ",更改用戶数据成功";
                                        resxml = BackCommon.BackText(xmlmsg.FromUserName, xmlmsg.ToUserName, int.Parse(xmlmsg.CreateTime), "text", "欢迎关注上海展通国际物流有限公司!\r\n便捷查件：请在对话框输入快递单号查询");
                                    }
                                    else
                                    {
                                        LogHelper.WarnLog(logstr + "但是" + (users.Count > 0 ? "新增" : "更改") + "数据失败");
                                    }
                                    #endregion
                                    break;
                                case Event.UNSUBSCRIBE://取消关注
                                    xmlmsg = OAConvert.ConvertObj<BaseMessage>(postXml);//请求数据解析
                                    users = usersbll.GetUserInfo(xmlmsg.FromUserName);//查询用户信息
                                    if (users.Count > 0)//有用户信息则修改用户订阅状态
                                    {
                                        resstate = usersbll.UpdateStatus(users[0].id, 0);//修改用户订阅状态 0未关注 1关注（取关）
                                    }
                                    logstr += xmlmsg.FromUserName + "取消关注";
                                    break;
                                case Event.CLICK:
                                    //eventNomal = OAConvert.ConvertObj<EventNormalMenuMessage>(postXml);
                                    //logstr += eventNomal.FromUserName + "CLICK事件，内容：" + JsonConvert.SerializeObject(eventNomal);
                                    break;
                                case Event.VIEW:
                                    //eventNomal = OAConvert.ConvertObj<EventNormalMenuMessage>(postXml);
                                    //logstr += eventNomal.FromUserName + "VIEW事件，内容：" + JsonConvert.SerializeObject(eventNomal);
                                    break;
                                case Event.LOCATION://上报地理位置事件
                                    //EventLocationMessage eventLocation = OAConvert.ConvertObj<EventLocationMessage>(postXml);
                                    //logstr += eventLocation.FromUserName + "上报地理位置事件事件，纬度：" + eventLocation.Latitude + "，经度：" + eventLocation.Longitude;
                                    break;
                                //case Event.LOCATION_SELECT: 
                                //    break;
                                case Event.SCAN:
                                    //xmlmsg = OAConvert.ConvertObj<EventScanMessage>(postXml);
                                    //logstr += "扫描带参数二维码事件";
                                    break;
                                case Event.SCANCODE_WAITMSG:
                                    //xmlmsg = OAConvert.ConvertObj<EventScanMenuMessage>(postXml);
                                    //logstr += "扫描推送事件";
                                    break;
                                default:
                                    //logstr += "未能识别的EVENT事件类型";
                                    break;
                            }
                        }
                        break;
                    default:
                        logstr += "未能识别的MsgType消息类型";
                        break;
                }
                LogHelper.InfoLog(logstr);
                Response.Write(resxml);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("ProcessPost" + ex.Message + "，追踪：" + ex.StackTrace);
            }
        }

        #endregion

        #region 模板消息
        public string ExpressMessagePush(string reqdata)
        {
            ApiResponseBase response = new ApiResponseBase();
            try
            {
                RequestExpressMsgPushModel reqmsgpush = JsonConvert.DeserializeObject<RequestExpressMsgPushModel>(reqdata);//解析请求数据

                if (AppSettingUtil.Sign == reqmsgpush.sgin)
                {
                    #region 根据请求参数查询用户信息
                    UsersModel requ = new UsersModel()
                    {
                         mobilephone = reqmsgpush.ToMobilePhone,
                         openid = reqmsgpush.openid,
                         unionid = reqmsgpush.unionid
                    };
                    List<UsersModel> ulist= usersbll.GetUserInfo(requ);
                    #endregion
                    if (ulist.Count > 0)
                    {
                        var msgData = new
                        {
                            touser = ulist[0].openid,//接收人的公众号openid
                            template_id = AppSettingUtil.TemplateId,//模板id
                            miniprogram = new
                            {
                                appid = AppSettingUtil.ApAppId,//小程序appid
                                pagepath = "pages/Home/Home"//小程序页面跳转地址，可带参数
                            },
                            data = reqmsgpush.data
                        };
                        access_token = tokenbll.GetOAAccessToken();//获取token 
                        string res = msgpushbll.MessagePush(access_token, JsonConvert.SerializeObject(msgData));
                        ResponseBase resb = JsonConvert.DeserializeObject<ResponseBase>(res);
                        if (resb.errcode != 0)
                        {
                            LogHelper.WarnLog("推送微信模板消息失败，详情" + res + ",请求数据" + reqdata);
                        }
                        response.code = resb.errcode;
                        response.msg = resb.errmsg;
                    }
                    else
                    {
                        response.code = 10002;
                        response.msg = "未找到要发送的公众号用户";
                    }
                }
                else
                {
                    response.code = 10001;
                    response.msg = "未授权";
                }
            }
            catch (Exception ex)
            {
                response.code = 10003;
                response.msg = ex.Message;
                LogHelper.ErrorLog(ex.Message + ",追踪" + ex.StackTrace);
            }
            return JsonConvert.SerializeObject(response);
        }

        #endregion

        #region 数据转换
        /// <summary>
        /// 微信用户信息转换成本地用户信息
        /// </summary>
        /// <returns></returns>
        private UsersModel WUserInfoToUsers(ResponseUserInfo wx_userinfo)
        {
            UsersModel resu = new UsersModel()
            {
                nickname = wx_userinfo.nickname,
                openid = wx_userinfo.openid,
                unionid = wx_userinfo.unionId,
                sex = wx_userinfo.sex,
                country = wx_userinfo.country,
                province = wx_userinfo.province,
                city = wx_userinfo.city,
                language = wx_userinfo.language,
                remark = wx_userinfo.remark,
                headimgurl = wx_userinfo.headimgurl,

                status = 0,
                verificationCode = "",
                mobilephone = "",
                bindname = "",
                bindstatus = 0,
                createtime = DateTime.Now,
                updatetime = DateTime.Now
            };
            return resu;
        }
        #endregion

    }
}