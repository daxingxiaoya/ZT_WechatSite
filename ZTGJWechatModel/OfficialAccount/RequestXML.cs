using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.OfficialAccount
{
    /// <summary>
    /// 微信请求类 
    /// </summary>
    public class RequestXML
    {
        private string toUserName;
        private string fromUserName;
        private string createTime;
        private string msgType;
        private string content;
        private string location_X;
        private string location_Y;
        private string label;
        private string picUrl;
        private string recognition;
        private string eevent;
        private string eventKey;
        private string scale;
        private string scanResult;
        private string scanType;

        /// <summary>  
        /// 消息接收方微信号，一般为公众平台账号微信号  
        /// </summary>  
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }

        /// <summary>  
        /// 消息发送方微信号  
        /// </summary>  
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }
        
        /// <summary>  
        /// 创建时间  
        /// </summary>  
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>  
        /// 信息类型 地理位置:location,文本消息:text,消息类型:image  
        /// </summary>  
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }

        /// <summary>  
        /// 信息内容  
        /// </summary>  
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        /// <summary>  
        /// 地理位置纬度  
        /// </summary>  
        public string Location_X
        {
            get { return location_X; }
            set { location_X = value; }
        }

        /// <summary>  
        /// 地理位置经度  
        /// </summary>  
        public string Location_Y
        {
            get { return location_Y; }
            set { location_Y = value; }
        }

        /// <summary>  
        /// 地图缩放大小  
        /// </summary>  
        public string Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>  
        /// 地理位置信息  
        /// </summary>  
        public string Label
        {
            get { return label; }
            set { label = value; }
        }
        
        /// <summary>  
        /// 图片链接，开发者可以用HTTP GET获取  
        /// </summary>  
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }
        
        /// <summary>  
        /// 语音识别的结果
        /// </summary>  
        public string Recognition
        {
            get { return recognition; }
            set { recognition = value; }
        }
        
        /// <summary>
        /// 事件
        /// </summary>
        public string Event
        {
            get { return eevent; }
            set { eevent = value; }
        }
        
        /// <summary>
        /// 点击事件
        /// </summary>
        public string EventKey
        {
            get { return eventKey; }
            set { eventKey = value; }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string ScanResult
        {
            get { return scanResult; }
            set { scanResult = value; }
        }
        
        /// <summary>
        /// 获取类型
        /// </summary>
        public string ScanType
        {
            get { return scanType; }
            set { scanType = value; }
        }
    }

    /// <summary>
    /// 定义图文集合
    /// </summary>
    public class PicArticle
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }

}
