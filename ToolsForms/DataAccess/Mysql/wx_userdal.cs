using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatUtils;

namespace ToolsForms.DataAccess.Mysql
{
    public class wx_userdal
    {
        public void getuser() {
            string sqlstr = "";
            DataSet ds = DbHelperMySQL.Query(sqlstr);
        }

         
    }
}
