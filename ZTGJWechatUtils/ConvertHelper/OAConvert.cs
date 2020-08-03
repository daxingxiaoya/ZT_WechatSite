using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZTGJWechatModel.EnumModel;

namespace ZTGJWechatUtils.ConvertHelper
{
    public class OAConvert
    {
        public static T ConvertObj<T>(string xmlstr)
        {
            XElement xdoc = XElement.Parse(xmlstr);
            var type = typeof(T);
            var t = Activator.CreateInstance<T>();
            foreach (XElement element in xdoc.Elements())
            {
                var pr = type.GetProperty(element.Name.ToString());
                if (pr != null)
                {
                    if (element.HasElements)
                    {//这里主要是兼容微信新添加的菜单类型。nnd，竟然有子属性，所以这里就做了个子属性的处理
                        foreach (var ele in element.Elements())
                        {
                            pr = type.GetProperty(ele.Name.ToString());
                            pr.SetValue(t, Convert.ChangeType(ele.Value, pr.PropertyType), null);
                        }
                        continue;
                    }
                    if (pr.PropertyType.Name != null & pr.PropertyType.Name == "MsgType")//获取消息模型
                    {
                        pr.SetValue(t, (MsgType)Enum.Parse(typeof(MsgType), element.Value.ToUpper()), null);
                        continue;
                    }
                    if (pr.PropertyType.Name != null & pr.PropertyType.Name == "Event")//获取事件类型。
                    {
                        pr.SetValue(t, (Event)Enum.Parse(typeof(Event), element.Value.ToUpper()), null);
                        continue;
                    }
                    pr.SetValue(t, Convert.ChangeType(element.Value, pr.PropertyType), null);
                }
            }
            return t;
        }


    }
}
