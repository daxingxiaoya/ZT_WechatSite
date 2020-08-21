using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel;
using ZTGJWechatUtils;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatDal.Common
{
    public class GlobalConfigurationDal
    {
        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public List<GlobalConfiguration> GetAllGlobalConfiguration()
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,ControlKey,ControValue,Remark,Status,CreateTime,UpdateTime FROM GlobalConfiguration with(nolock) where Status=0 ";
                DynamicParameters dp = new DynamicParameters();
                return connection.Query<GlobalConfiguration>(sqlQuery, dp).ToList();
            }
        }
        /// <summary>
        /// 获取配置带分页
        /// </summary>
        /// <returns></returns>
        public List<GlobalConfiguration> GetGCByKey(string id, string key, int page = 1, int psize = 200)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select * from( select row_number()over(order by UpdateTime desc) rownumber,
                         Id,ControlKey,ControValue,Remark,Status,CreateTime,UpdateTime FROM GlobalConfiguration with(nolock) where Status=0 ";
                DynamicParameters dp = new DynamicParameters();
                if (!string.IsNullOrEmpty(key))
                {
                    sqlQuery += " and ControlKey=@ControlKey ";
                    dp.Add("@ControlKey", key);
                }
                if (!string.IsNullOrEmpty(id))
                {
                    sqlQuery += " and Id=@Id ";
                    dp.Add("@Id", Convert.ToInt32(id));
                }
                sqlQuery += ")a where rownumber between (@PageSize*(@PageIndex-1)+1) and @PageSize*@PageIndex";
                dp.Add("@PageSize", psize);
                dp.Add("@PageIndex", page);
                return connection.Query<GlobalConfiguration>(sqlQuery, dp).ToList();
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool UpdateGC(GlobalConfiguration m)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE GlobalConfiguration SET ");
            strSql.Append(" ControlKey=@ControlKey, ");
            strSql.Append(" ControValue=@ControValue, ");
            strSql.Append(" Remark=@Remark, ");
            strSql.Append(" UpdateTime=@UpdateTime ");
            strSql.Append(" WHERE Id=@Id  ");
            dp.Add("@Id", m.Id, DbType.String, ParameterDirection.Input);

            dp.Add("@ControlKey", m.ControlKey);
            dp.Add("@ControValue", m.ControValue);
            dp.Add("@Remark", m.Remark);

            dp.Add("@UpdateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.GlobalConfiguration); //清空用户信息缓存
                }
                return result;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGC(GlobalConfiguration m)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                const string sql = @" INSERT INTO GlobalConfiguration(ControlKey,ControValue,Remark,Status,CreateTime,UpdateTime)
                    VALUES (@ControlKey,@ControValue,@Remark,@Status,@CreateTime,@UpdateTime);  
                    SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                m.Id = connection.Query<int>(sql, m).Single();
                bool result = m.Id > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.GlobalConfiguration); //清空用户信息缓存
                }
                return result;
            }
        }

    }
}
