using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.OfficialAccount
{
    public static class BackCommon
    {
        #region 回复文本消息
        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="Content"></param>
        /// <param name="FuncFlag"></param>
        /// <returns></returns>
        public static string BackText(string FromUserName, string ToUserName, int time, string MsgType, string Content)
        {
            string batext = string.Empty;
            batext = "<xml>" +
                   "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                   "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                   "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                   "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                   "<Content><![CDATA[" + Content + "]]></Content>" + //回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
                   "</xml>";
            return batext;
        }
        #endregion

        #region 返回单图文消息
        /// <summary>
        /// 返回单图文消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="ArticleCount"></param>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="PicUrl"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string BackSinglePicWord(string FromUserName, string ToUserName, int time, string MsgType, int ArticleCount, string Title, string Description, string PicUrl, string Url)
        {
            string batextword = string.Empty;
            StringBuilder sbText = new StringBuilder();
            sbText.Append("<xml>" +
                  "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                  "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                  "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                  "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                  "<ArticleCount><![CDATA[" + ArticleCount + "]]></ArticleCount>" + //图文消息个数，限制为10条以内
                  "<Articles>");

            sbText.Append("<item>" +
                "<Title><![CDATA[" + Title + "]]></Title> " +
                "<Description><![CDATA[" + Description + "]]></Description>" +
                "<PicUrl><![CDATA[" + PicUrl + "]]></PicUrl>" +
                "<Url><![CDATA[" + Url + "]]></Url>" +
               "</item>");
            sbText.Append("</Articles></xml>");
            return sbText.ToString();
        }
        #endregion

        #region 返回图文消息
        /// <summary>
        /// 返回图文消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="ArticleCount"></param>
        /// <param name="FuncFlag"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public static string BackTextWordList(string FromUserName, string ToUserName, int time, string MsgType, int ArticleCount, List<PicArticle> objart)
        {
            string batextword = string.Empty;
            StringBuilder sbText = new StringBuilder();
            sbText.Append("<xml>" +
                  "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                  "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                  "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                  "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                  "<ArticleCount><![CDATA[" + ArticleCount + "]]></ArticleCount>" + //图文消息个数，限制为10条以内
                  "<Articles>");

            foreach (PicArticle obj in objart)
            {
                sbText.Append("<item>" +
                 "<Title><![CDATA[" + obj.Title + "]]></Title> " +
                 "<Description><![CDATA[" + obj.Description + "]]></Description>" +
                 "<PicUrl><![CDATA[" + obj.PicUrl + "]]></PicUrl>" +
                 "<Url><![CDATA[" + obj.Url + "]]></Url>" +
                "</item>");
            }
            sbText.Append("</Articles></xml>");
            return sbText.ToString();
        }
        #endregion

        #region 返回图书单图文消息
        /// <summary>
        /// 返回单图文消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="ArticleCount"></param>
        /// <param name="FuncFlag"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public static string BackSingleBookInfo(string FromUserName, string ToUserName, int time, string MsgType, int ArticleCount, string Title, string Description, string PicUrl, string Url)
        {
            string batextword = string.Empty;
            StringBuilder sbText = new StringBuilder();
            sbText.Append("<xml>" +
                  "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                  "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                  "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                  "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                  "<ArticleCount><![CDATA[" + ArticleCount + "]]></ArticleCount>" + //图文消息个数，限制为10条以内
                  "<Articles>");

            sbText.Append("<item>" +
                "<Title><![CDATA[" + Title + "]]></Title> " +
                "<Description><![CDATA[" + Description + "]]></Description>" +
                "<PicUrl><![CDATA[" + PicUrl + "]]></PicUrl>" +
                "<Url><![CDATA[" + Url + "]]></Url>" +
               "</item>");
            sbText.Append("</Articles></xml>");
            return sbText.ToString();
        }
        #endregion

        #region 返回图书图文消息
        /// <summary>
        /// 返回图书图文消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="ArticleCount"></param>
        /// <param name="FuncFlag"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public static string BackTextBookList(string FromUserName, string ToUserName, int time, string MsgType, int ArticleCount, List<PicArticle> objart)
        {
            string batextword = string.Empty;
            StringBuilder sbText = new StringBuilder();
            sbText.Append("<xml>" +
                  "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                  "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                  "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                  "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                  "<ArticleCount><![CDATA[" + ArticleCount + "]]></ArticleCount>" + //图文消息个数，限制为10条以内
                  "<Articles>");
            sbText.Append("<item>" +
               "<Title><![CDATA[" + "已借图书如下" + "]]></Title> " +
               "<Description><![CDATA[" + "已借图书如下哟" + "]]></Description>" +
               "<PicUrl><![CDATA[" + "https://mmbiz.qlogo.cn/mmbiz/JxNh0ODibMTibEbE6KYEBIWwOYUvMDUq0ykKchv2jFBfAmiaebNPKEf0oIIzaCaDU2UERg8EaMheDUVH8evJL795A/0?wx_fmt=jpeg" + "]]></PicUrl>" +
               "<Url><![CDATA[" + "" + "]]></Url>" +
              "</item>");
            if (objart.Count < 6)
            {
                foreach (PicArticle obj in objart)
                {
                    sbText.Append("<item>" +
                     "<Title><![CDATA[" + obj.Title + "]]></Title> " +
                     "<Description><![CDATA[" + obj.Description + "]]></Description>" +
                     "<PicUrl><![CDATA[" + obj.PicUrl + "]]></PicUrl>" +
                     "<Url><![CDATA[" + obj.Url + "]]></Url>" +
                    "</item>");
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    sbText.Append("<item>" +
                    "<Title><![CDATA[" + objart[i].Title + "]]></Title> " +
                    "<Description><![CDATA[" + objart[i].Description + "]]></Description>" +
                    "<PicUrl><![CDATA[" + objart[i].PicUrl + "]]></PicUrl>" +
                    "<Url><![CDATA[" + objart[i].Url + "]]></Url>" +
                   "</item>");
                }
            }


            sbText.Append("</Articles></xml>");
            return sbText.ToString();
        }
        #endregion

        #region 返回图书图文消息
        /// <summary>
        /// 返回图书图文消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="objart"></param>
        /// <param name="MaxRows"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string SearchBookList(string FromUserName, string ToUserName, int time, string MsgType, List<PicArticle> objart, string MaxRows, string Content)
        {
            string batextword = string.Empty;
            StringBuilder sbText = new StringBuilder();
            sbText.Append("<xml>" +
                  "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                  "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                  "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                  "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                  "<ArticleCount><![CDATA[" + ((objart.Count == 6) ? (objart.Count * 1 + 2) : (objart.Count * 1 + 1)) + "]]></ArticleCount>" + //图文消息个数，限制<=8
                  "<Articles>");
            sbText.Append("<item>" +
               "<Title><![CDATA[" + "关于\"" + Content + "\"共检索到" + MaxRows + "条记录" + "]]></Title> " +
               "<Description><![CDATA[" + "" + "]]></Description>" +
               "<PicUrl><![CDATA[" + "http://www.cmlib.com.cn/lib/WebUI/images/booknew.jpg" + "]]></PicUrl>" + //https://mmbiz.qlogo.cn/mmbiz/JxNh0ODibMTicsdJWo2lWkS35nB1O4W6DiaJExugtiaLWibemIkJtBS6Atqjk2JZ79kG421v07kOuoyJaebul3lXqVQ/0?wx_fmt=jpeg
               "<Url><![CDATA[" + "http://reg.library.sh.cn/SHLIBWX/booklist.jsp?open=oG_RsuDtJfubuLxB74Gfmcb1bE1s&key=" + Content + "]]></Url>" +
              "</item>");
            if (objart.Count > 0)
            {
                foreach (PicArticle obj in objart)
                {
                    sbText.Append("<item>" +
                     "<Title><![CDATA[" + obj.Title + "]]></Title> " +
                     "<Description><![CDATA[" + obj.Description + "]]></Description>" +
                     "<PicUrl><![CDATA[" + obj.PicUrl + "]]></PicUrl>" +
                     "<Url><![CDATA[" + obj.Url + "]]></Url>" +
                    "</item>");
                }
            }
            if (objart.Count == 6)
            {
                sbText.Append("<item>" +
                               "<Title><![CDATA[" + "输入\"@下一页\"转至下6条记录" + "]]></Title> " +
                               "<Description><![CDATA[" + "" + "]]></Description>" +
                               "<PicUrl><![CDATA[" + "" + "]]></PicUrl>" +
                               "<Url><![CDATA[" + "" + "]]></Url>" +
                              "</item>");
            }


            sbText.Append("</Articles></xml>");
            return sbText.ToString();
        }
        #endregion

        #region 返回语音信息
        /// <summary>
        /// 返回语音信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="time"></param>
        /// <param name="MsgType"></param>
        /// <param name="media_id"></param>
        /// <param name="Format"></param>
        /// <param name="Recognition"></param>
        /// <param name="MsgId"></param>
        /// <returns></returns>
        public static string BackVoice(string FromUserName, string ToUserName, int time, string MsgType, string media_id, string Format, string Recognition, string MsgId)
        {
            string batext = string.Empty;
            batext = "<xml>" +
                   "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +   //接收方帐号（收到的OpenID）
                   "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" + //开发者微信号
                   "<CreateTime>" + time + "</CreateTime>" +  //消息创建时间 （整型）
                   "<MsgType><![CDATA[" + MsgType + "]]></MsgType>" +  //text
                   "<MediaId><![CDATA[" + media_id + "]]></MediaId>" +  //语音消息媒体id，可以调用多媒体文件下载接口拉取该媒体
                   "<Format><![CDATA[" + Format + "]]></Format>" + //语音格式：amr
                   "<Recognition>" + Recognition + "</Recognition>" + //语音识别结果，UTF8编码
                   "<MsgId>" + MsgId + "</MsgId>" + //消息id，64位整型
                   "</xml>";
            return batext;
        }
        #endregion
    }
}
