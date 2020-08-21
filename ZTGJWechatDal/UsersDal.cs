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
using ZTGJWechatUtils.Helper;
using ZTGJWechatUtils.Redis;

namespace ZTGJWechatDal
{
    /// <summary>
    /// sql用户数据来源
    /// </summary>
    public class UsersDal
    {
        #region select
        /// <summary>
        /// 根据openid获取sql数据库用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<UsersModel> GetUserInfo(string openid)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select id,nickname,openid,unionid,companyname,mobilephone,empowerStatus,powerApMenu,powerReArea,status,sex,country,province,city,language,
                    remark,headimgurl,verificationCode,bindname,bindstatus,createtime,updatetime
                    FROM Users with(nolock) where openid=@openid ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@openid", openid);
                return connection.Query<UsersModel>(sqlQuery, dp).ToList();
            }
        }
        /// <summary>
        /// 根据调件获取sql数据库用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<UsersModel> GetUserInfo(UsersModel u)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select id,nickname,openid,appletopenid,unionid,session_key,companyname,emcompany,stockcompany,mobilephone,empowerStatus,powerApMenu,powerReArea,status,sex,country,province,city,language,
                    remark,headimgurl,verificationCode,bindname,bindstatus,createtime,updatetime
                    FROM Users with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                sqlQuery += GetUserWhere(u, out dp);
                return connection.Query<UsersModel>(sqlQuery, dp).ToList();
            }
        }

        private string GetUserWhere(UsersModel u, out DynamicParameters outdp)
        {
            string ressql = "";
            DynamicParameters resdq = new DynamicParameters();
            if (u != null)
            {
                if (u.id != 0)
                {
                    ressql += " and id=@id ";
                    resdq.Add("@id", u.id);
                }
                if (!string.IsNullOrEmpty(u.mobilephone))
                {
                    ressql += " and mobilephone=@mobilephone ";
                    resdq.Add("@mobilephone", u.mobilephone);
                }
                if (!string.IsNullOrEmpty(u.openid))
                {
                    ressql += " and openid=@openid ";
                    resdq.Add("@openid", u.openid);
                }
                if (!string.IsNullOrEmpty(u.unionid))
                {
                    ressql += " and unionid=@unionid ";
                    resdq.Add("@unionid", u.unionid);
                }
            }
            outdp = resdq;
            return ressql;
        }

        /// <summary>
        /// 根据uid获取用户授权信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<UsersModel> GetUserEmpower(string uid)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select nickname,companyname,emcompany,stockcompany,unionid,empowerStatus,powerApMenu,headimgurl,mobilephone FROM Users with(nolock) where unionid=@unionid ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", uid);
                return connection.Query<UsersModel>(sqlQuery, dp).ToList();
            }
        }

        /// <summary>
        /// 获取sql数据库微信用户信息 带分页
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<UsersModel> GetUsersByPage(int index,int pagesize,out int totalcount)
        {
            totalcount = 0;
            totalcount = GetTotalCount();
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select * from( select row_number()over(order by createtime desc) rownumber,
                        id,nickname,openid,unionid,companyname,emcompany,stockcompany,mobilephone,empowerStatus,powerApMenu,powerReArea,status,sex,country,province,city,language,
                        remark,headimgurl,verificationCode,bindname,bindstatus,createtime,updatetime 
                        FROM Users with(nolock) where 1=1 
                    )a where rownumber between (@PageSize*(@PageIndex-1)+1) and @PageSize*@PageIndex ";
                DynamicParameters dp = new DynamicParameters();
                //dp.Add("@openid", openid);
                dp.Add("@PageSize", pagesize);
                dp.Add("@PageIndex", index);
                return connection.Query<UsersModel>(sqlQuery, dp).ToList();
            }
        }
        private int GetTotalCount() {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select count(1) FROM Users with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                //dp.Add("@openid", openid);
                return connection.ExecuteScalar<int>(sqlQuery, dp);
            }
        }
        /// <summary>
        /// 微信用户统计
        /// </summary>
        /// <returns></returns>
        public List<UsersStatistics> WechatUsersStatistics() {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                string sqlQuery = @" select count(1) as total,
                    sum(case when emcompany!='' and stockcompany!='' then 1 else 0 end) as completetotal,
                    sum(case when emcompany!='' then 1 else 0 end) as emtotal,
                    sum(case when stockcompany!='' then 1 else 0 end) as stocktotal,
                    sum(case when status=1 then 1 else 0 end) as oafollowtotal 
                    from Users with(nolock) ";
                DynamicParameters dp = new DynamicParameters();
                //dp.Add("@openid", openid);
                return connection.Query<UsersStatistics>(sqlQuery, dp).ToList();
            }
        }

        #endregion

        #region insert
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(UsersModel model)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                const string sql = @" INSERT INTO Users
                            (nickname,openid,appletopenid,unionid,session_key,companyname,emcompany,stockcompany,mobilephone,empowerStatus,powerApMenu,powerReArea,verificationCode,
                            sex,country,province,city,language,remark,headimgurl,status,bindname,bindstatus,createtime,updatetime)
                    VALUES (@nickname,@openid,@appletopenid,@unionid,@session_key,@companyname,@emcompany,@stockcompany,@mobilephone,@empowerStatus,@powerApMenu,@powerReArea,@verificationCode,
                            @sex,@country,@province,@city,@language,@remark,@headimgurl,@status,@bindname,@bindstatus,@createtime,@updatetime);  
                    SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                model.id = connection.Query<int>(sql, model).Single();
                bool result = model.id > 0;
                return result;
            }
        }
        #endregion

        #region update
        /// <summary>
        /// 更新订阅状态
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="status">0未关注 1关注</param>
        /// <returns></returns>
        public bool UpdateStatus(string uid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Users SET ");
            strSql.Append(" status=@status, ");
            strSql.Append(" updatetime=@updatetime ");
            strSql.Append(" WHERE id=@id ");
            dp.Add("@id", uid, DbType.Int32, ParameterDirection.Input);
            dp.Add("@status", status, DbType.Int32, ParameterDirection.Input);
            dp.Add("@updatetime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.UserEmpowerKey + uid); //清空缓存
                }
                return result;
            }
        }
        /// <summary>
        /// 根据小程序用户数据更新本地数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(UsersModel u)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Users SET ");
            strSql.Append(" appletopenid=@appletopenid, ");
            strSql.Append(" session_key=@session_key, ");
            strSql.Append(" mobilephone=@mobilephone, ");
            strSql.Append(" updatetime=@updatetime ");
            strSql.Append(" WHERE openid=@openid or unionid=@unionid or appletopenid=@appletopenid ");
            dp.Add("@openid", u.openid, DbType.String, ParameterDirection.Input);
            dp.Add("@unionid", u.unionid, DbType.String, ParameterDirection.Input);
            dp.Add("@appletopenid", u.appletopenid, DbType.String, ParameterDirection.Input);

            dp.Add("@appletopenid", u.appletopenid, DbType.String, ParameterDirection.Input);
            dp.Add("@session_key", u.session_key, DbType.String, ParameterDirection.Input);
            dp.Add("@mobilephone", u.mobilephone, DbType.String, ParameterDirection.Input);

            dp.Add("@updatetime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.UserEmpowerKey + u.unionid); //清空用户信息缓存
                }
                return result;
            }
        }
        /// <summary>
        /// 小程序用户更新公司名和用户名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserCompany(ZTGJWechatModel.Applet.RequestUserInfo u)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Users SET ");
            strSql.Append(" nickname=@nickname, ");
            strSql.Append(" companyname=@companyname, ");
            strSql.Append(" updatetime=@updatetime ");
            strSql.Append(" WHERE unionid=@unionid  ");
            dp.Add("@unionid", u.unionid, DbType.String, ParameterDirection.Input);

            dp.Add("@nickname", u.usaername, DbType.String, ParameterDirection.Input);
            dp.Add("@companyname", u.companyname, DbType.String, ParameterDirection.Input);

            dp.Add("@updatetime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.UserEmpowerKey + u.unionid); //清空用户信息缓存
                }
                return result;
            }
        }
        /// <summary>
        /// 小程序用户 手机号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserPhone(ZTGJWechatModel.Applet.RequestUserInfo u)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Users SET ");
            strSql.Append(" mobilephone=@mobilephone, ");
            strSql.Append(" updatetime=@updatetime ");
            strSql.Append(" WHERE unionid=@unionid  ");
            dp.Add("@unionid", u.unionid, DbType.String, ParameterDirection.Input);

            dp.Add("@mobilephone", u.newphone, DbType.String, ParameterDirection.Input);

            dp.Add("@updatetime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.UserEmpowerKey + u.unionid); //清空用户信息缓存
                }
                return result;
            }
        }
        /// <summary>
        /// 小程序用户 用户权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserEmpower(UsersModel u)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Users SET ");
            strSql.Append(" empowerStatus=@empowerStatus, ");
            strSql.Append(" powerApMenu=@powerApMenu, ");
            strSql.Append(" powerReArea=@powerReArea, ");
            strSql.Append(" updatetime=@updatetime ");
            strSql.Append(" WHERE unionid=@unionid  ");
            dp.Add("@unionid", u.unionid, DbType.String, ParameterDirection.Input);

            dp.Add("@empowerStatus", u.empowerStatus);
            dp.Add("@powerApMenu", u.powerApMenu);
            dp.Add("@powerReArea", u.powerReArea);

            dp.Add("@updatetime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                if (result)
                {
                    RedisHelper.Remove(RedisKeys.UserEmpowerKey + u.unionid); //清空用户信息缓存
                }
                return result;
            }
        }
        #endregion

    }
}
