using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.Bussiness;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.Bussiness
{
    public class WechatDal
    {
        /// <summary>
        /// 根据来源获取未绑定用户列表
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<Bus_GetUsers_response> GetUnboundUsers(string source)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                string sqlQuery = " select nickname,unionid,companyname,emcompany,stockcompany,mobilephone FROM Users with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                if (source == "em")
                {
                    sqlQuery += " and emcompany='' ";
                }
                else if (source == "stock")
                {
                    sqlQuery += " and stockcompany='' ";
                }
                else {
                    sqlQuery += " and  emcompany='' or stockcompany='' ";
                }
                
                //dp.Add("@openid", openid);

                return connection.Query<Bus_GetUsers_response>(sqlQuery, dp).ToList();
            }
        }

        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="status">0未关注 1关注</param>
        /// <returns></returns>
        public bool BindingUsers(Bus_BindingUsers_Request req)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Users SET ");
            if (req.source == "em")
            {
                strSql.Append(" emcompany=@emcompany, ");
                dp.Add("@emcompany", req.companycode, DbType.String, ParameterDirection.Input);
            }
            else if(req.source == "stock") {
                strSql.Append(" stockcompany=@stockcompany, ");
                dp.Add("@stockcompany", req.companycode, DbType.String, ParameterDirection.Input);
            }
            strSql.Append(" updatetime=@updatetime ");
            strSql.Append(" WHERE unionid=@unionid ");
            dp.Add("@unionid", req.unionid, DbType.String, ParameterDirection.Input);
            dp.Add("@updatetime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                return connection.Execute(strSql.ToString(), dp) > 0; 
            }
        }

    }
}
