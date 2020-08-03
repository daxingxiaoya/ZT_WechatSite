using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils.ConvertHelper
{
    public static class UserConvert
    {
        /// <summary>
        /// 关注公众号的状态  0未关注  1关注
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string StatusTxt(int status) {
            string res = "";
            switch (status)
            {
                case 0:
                    res = "未关注";
                    break;
                case 1:
                    res = "已关注";
                    break;
                default:
                    res = status.ToString();
                    break;
            }
            return res;

        }
        /// <summary>
        /// 用户授权状态 0待审核 1审核通过 2禁用
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string EmpowerStatusTxt(int empowerStatus)
        {
            string res = "";
            switch (empowerStatus)
            {
                case 0:
                    res = "待审核";
                    break;
                case 1:
                    res = "审核通过";
                    break;
                case 2:
                    res = "禁用";
                    break;
                default:
                    res = empowerStatus.ToString();
                    break;
            }
            return res;

        }
    }
}
