using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.OfficialAccount
{
    public class ResponseUserInfo : ResponseBase
    {
        private int _subscribe = 0;
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int subscribe
        {
            set { _subscribe = value; }
            get { return _subscribe; }
        }
        private string _openid = "";
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        private string _unionid = "";
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionId
        {
            set { _unionid = value; }
            get { return _unionid; }
        }
        private string _nickname;
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        private int _sex;
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        private string _city = "";
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        private string _country = "";
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }
        private string _province = "";
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        private string _language = "";
        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language
        {
            set { _language = value; }
            get { return _language; }
        }
        private string _headimgurl = "";
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }
        private string _subscribe_time;
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public string subscribe_time
        {
            set { _subscribe_time = value; }
            get { return _subscribe_time; }
        }
        private string _remark = "";
        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        private string _groupid;
        /// <summary>
        /// 用户所在的分组ID（暂时兼容用户分组旧接口）
        /// </summary>
        public string groupid
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        private List<int> _tagid_list = new List<int>();
        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        public List<int> tagid_list
        {
            set { 
                foreach (var item in value)
                {
                    _tagid_list.Add(item);
                }
            }
            get { return _tagid_list; }
        }
        private string _subscribe_scene;
        /// <summary>
        /// 返回用户关注的渠道来源，
        /// ADD_SCENE_SEARCH 公众号搜索，
        /// ADD_SCENE_ACCOUNT_MIGRATION 公众号迁移，
        /// ADD_SCENE_PROFILE_CARD 名片分享，
        /// ADD_SCENE_QR_CODE 扫描二维码，
        /// ADD_SCENE_PROFILE_LINK 图文页内名称点击，
        /// ADD_SCENE_PROFILE_ITEM 图文页右上角菜单，
        /// ADD_SCENE_PAID 支付后关注，ADD_SCENE_OTHERS 其他
        /// </summary>
        public string subscribe_scene
        {
            set { _subscribe_scene = value; }
            get { return _subscribe_scene; }
        }
        private string _qr_scene;
        /// <summary>
        /// 二维码扫码场景（开发者自定义）
        /// </summary>
        public string qr_scene
        {
            set { _qr_scene = value; }
            get { return _qr_scene; }
        }
        private string _qr_scene_str;
        /// <summary>
        /// 二维码扫码场景描述（开发者自定义）
        /// </summary>
        public string qr_scene_str
        {
            set { _qr_scene_str = value; }
            get { return _qr_scene_str; }
        }

    }
}
