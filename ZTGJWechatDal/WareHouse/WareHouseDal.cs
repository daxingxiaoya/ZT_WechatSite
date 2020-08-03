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

namespace ZTGJWechatDal.WareHouse
{
    public class WareHouseDal
    {
        #region select
        /// <summary>
        /// WareHouse
        /// </summary>
        /// <param name="state">数据状态 0正常 1删除</param>
        /// <returns></returns>
        public List<WareHouseModel> GetWareHouse(string state = "")
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                string sqlQuery = @" select Id,Name,Address,Phone,TelPhone,Email,Facsimile,PostalCode,Remark,SortNum,State,CreateTime,UpdateTime
                    FROM WareHouse with(nolock) where 1=1 ";
                DynamicParameters dp = new DynamicParameters();
                if (!string.IsNullOrEmpty(state))
                {
                    sqlQuery += "  and State=@state  ";
                    dp.Add("@state", state);
                }
                return connection.Query<WareHouseModel>(sqlQuery, dp).ToList();
            }
        }
        #endregion
    }
}
