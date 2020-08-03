using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.BackStage
{
    /// <summary>
    /// 后台系统用户
    /// </summary>
    public class BackStageUsersModel
    {
        public BackStageUsersModel()
        {
            Id = 0;
            Account = "";
            Pwd = "";
            Remark = "";
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        public int Id { set; get; }
        public string Account { set; get; }
        public string Pwd { set; get; }
        public string Remark { set; get; }
        public DateTime CreateTime { set; get; }
        public DateTime UpdateTime { set; get; }
    }
}
