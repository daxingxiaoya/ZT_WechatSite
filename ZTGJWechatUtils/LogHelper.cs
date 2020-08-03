using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    public class LogHelper
    {
        public static readonly log4net.ILog logHanlder4Info = log4net.LogManager.GetLogger("InfoLog");
        public static readonly log4net.ILog logHanlder4Warn = log4net.LogManager.GetLogger("WarnLog");
        public static readonly log4net.ILog logHanlder4Error = log4net.LogManager.GetLogger("ErrorLog");

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static void InfoLog(string info)
        {
            if (logHanlder4Info.IsInfoEnabled)
            {
                logHanlder4Info.Info(info);
            }
            else
            {
                SetConfig();
                logHanlder4Info.Info(info);
            }
        }

        public static void WarnLog(string info)
        {
            if (logHanlder4Warn.IsWarnEnabled)
            {
                logHanlder4Warn.Warn(info);
            }
            else
            {
                SetConfig();
                logHanlder4Warn.Warn(info);
            }
        }

        public static void ErrorLog(string error)
        {
            if (logHanlder4Error.IsErrorEnabled)
            {
                logHanlder4Error.Error(error);
            }
            else
            {
                SetConfig();
                logHanlder4Error.Error(error);
            }
        }
    }
}
