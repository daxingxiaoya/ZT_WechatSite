using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ZTGJWechatModel;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.Stock
{
    public class StockDal
    {
        /// <summary>
        /// 根据uid获取权限内的库区信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<ReservoirAreaexportModel> GetRAByUId(string uid)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,ReservoirAreaNo,AnotherName,VendName,Enable,CreateTime,UpdateTime
                    from dbo.ReservoirArea where Enable=0 and Id in (select col from F_SplitSTR((select powerReArea from Users where unionid=@unionid),',')) ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", uid);
                return connection.Query<ReservoirAreaexportModel>(sqlQuery, dp).ToList();
            }
        }

        /// <summary>
        /// 根据uid获取权限内的库区信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<ReservoirAreaModel> GetRAList(string vendname)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,ReservoirAreaNo,AnotherName,VendName,Enable,CreateTime,UpdateTime
                    from dbo.ReservoirArea where Enable=0 and VendName=@VendName ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@VendName", vendname);
                return connection.Query<ReservoirAreaModel>(sqlQuery, dp).ToList();
            }
        }


    }
}
