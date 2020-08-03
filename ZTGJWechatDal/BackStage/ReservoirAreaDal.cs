using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatModel.Applet;
using ZTGJWechatModel.BackStage;
using ZTGJWechatModel.Common;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.BackStage
{
    public class ReservoirAreaDal
    {
        #region 公司
        /// <summary>
        /// 获取公司
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public List<VendCompanyModel> GetCompany(string shortname="", string fullname = "")
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,VendName,FullName,CreateTime FROM VendCompany with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                if (!string.IsNullOrEmpty(shortname))
                {
                    sqlQuery += " and VendName=@VendName ";
                    dp.Add("@VendName", shortname);
                }
                if (!string.IsNullOrEmpty(fullname))
                {
                    sqlQuery += " and FullName=@FullName ";
                    dp.Add("@FullName", fullname);
                }
                return connection.Query<VendCompanyModel>(sqlQuery, dp).ToList();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCompany(VendCompanyModel model)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                const string sql = @" INSERT INTO VendCompany(VendName,FullName,CreateTime)
                    VALUES (@VendName,@FullName,@CreateTime);  
                    SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                model.Id = connection.Query<int>(sql, model).Single();
                bool result = model.Id > 0;
                return result;
            }
        }

        #endregion

        #region 库区
        /// <summary>
        /// 获取库区
        /// </summary>
        /// <param name="vendname"></param>
        /// <param name="rano"></param>
        /// <returns></returns>
        public List<ReservoirAreaModel> GetLocs(string vendname = "", string rano = "")
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,ReservoirAreaNo,AnotherName,VendName,Enable,CreateTime,UpdateTime FROM ReservoirArea with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                if (!string.IsNullOrEmpty(vendname))
                {
                    sqlQuery += " and VendName=@VendName ";
                    dp.Add("@VendName", vendname);
                }
                if (!string.IsNullOrEmpty(rano))
                {
                    sqlQuery += " and ReservoirAreaNo=@ReservoirAreaNo ";
                    dp.Add("@ReservoirAreaNo", rano);
                }
                return connection.Query<ReservoirAreaModel>(sqlQuery, dp).ToList();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddLoc(ReservoirAreaModel model)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                const string sql = @" INSERT INTO ReservoirArea(ReservoirAreaNo,AnotherName,VendName,Enable,CreateTime,UpdateTime)
                    VALUES (@ReservoirAreaNo,@AnotherName,@VendName,@Enable,@CreateTime,@UpdateTime);  
                    SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                model.Id = connection.Query<int>(sql, model).Single();
                bool result = model.Id > 0;
                return result;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateLoc(ReservoirAreaModel model)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE ReservoirArea set ");
            strSql.Append(" AnotherName=@AnotherName, ");
            strSql.Append(" UpdateTime=@UpdateTime ");
            strSql.Append(" WHERE VendName=@VendName and ReservoirAreaNo=@ReservoirAreaNo ");
            dp.Add("@VendName", model.VendName, DbType.String, ParameterDirection.Input);
            dp.Add("@ReservoirAreaNo", model.ReservoirAreaNo, DbType.String, ParameterDirection.Input);

            dp.Add("@AnotherName", model.AnotherName, DbType.String, ParameterDirection.Input);
            dp.Add("@UpdateTime", model.UpdateTime, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                return result;
            }
        }
        #endregion


    }
}
