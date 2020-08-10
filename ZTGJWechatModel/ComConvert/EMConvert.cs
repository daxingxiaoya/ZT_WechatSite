using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.ComConvert
{
    public static class EMConvert
    {
        public static string F_ReturnStatusToTxt(int s)
        {
            string res = "";
            switch (s)
            {
                case 0:
                    res = "未发货";
                    break;
                case 1:
                    res = "已发货";
                    break;
                case 2:
                    res = "已签收";
                    break;
                default:
                    res = s.ToString();
                    break;
            }
            return res;
        }
    }
}
