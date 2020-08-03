using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.BackStage;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.BackStage
{
    public class BSUserDal
    {
       /// <summary>
       /// 获取后台用户
       /// </summary>
       /// <param name="acc"></param>
       /// <param name="pwd"></param>
       /// <returns></returns>
        public BackStageUsersModel GetBSUserInfo(string acc,string pwd)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select id,Account,Pwd,Remark,CreateTime,UpdateTime FROM BackStageUsers with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                if (!string.IsNullOrEmpty(acc))
                {
                    sqlQuery += " and Account=@Account ";
                    dp.Add("@Account", acc);
                }
                if (!string.IsNullOrEmpty(acc))
                {
                    sqlQuery += " and Pwd=@Pwd ";
                    dp.Add("@Pwd", pwd);
                }
                return connection.Query<BackStageUsersModel>(sqlQuery, dp).ToList().FirstOrDefault();
            }
        }
    }
}
