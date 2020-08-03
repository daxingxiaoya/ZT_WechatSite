using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZTGJWechatModel.Applet;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.Order
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCartDal
    {
        #region 
        /// <summary>
        /// 根据uid获取用户购物车商品
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<ShoppingCartModel> GetShoppingCart(string uid)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,unionid,VendName,LocationName,ProductCode,SerialNumber,BatchNumber,ProductName,ProductDescrEN,ProductDescrCH,StockQty,QuantityUnitCH,Ischecked,number,ImgUrl,CreateTime,UpdateTime  
                    FROM ShoppingCart with(nolock) where unionid=@unionid ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", uid);
                return connection.Query<ShoppingCartModel>(sqlQuery, dp).ToList();
            }
        }
        /// <summary>
        /// 根据uid和备件获取用户购物车商品
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ShoppingCartModel GetShoppingCartByPro(string uid, string procode, string locname, string serialnumber, string batchNumber)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,unionid,VendName,LocationName,ProductCode,SerialNumber,BatchNumber,ProductName,ProductDescrEN,ProductDescrCH,StockQty,QuantityUnitCH,Ischecked,number,ImgUrl,CreateTime,UpdateTime 
                    FROM ShoppingCart with(nolock) where unionid=@unionid and ProductCode=@ProductCode and LocationName=@LocationName and SerialNumber=@SerialNumber and BatchNumber=@BatchNumber ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", uid);
                dp.Add("@ProductCode", procode);
                dp.Add("@LocationName", locname);
                dp.Add("@SerialNumber", serialnumber);
                dp.Add("@BatchNumber", batchNumber);
                return connection.Query<ShoppingCartModel>(sqlQuery, dp).FirstOrDefault();
            }
        }
        #endregion

        #region insert
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddShoppingCart(ShoppingCartModel model)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                const string sql = @" INSERT INTO ShoppingCart(unionid,VendName,LocationName,ProductCode,SerialNumber,BatchNumber,ProductName,ProductDescrEN,ProductDescrCH,
                        StockQty,QuantityUnitCH,Ischecked,number,ImgUrl,CreateTime,UpdateTime)
                    VALUES (@unionid,@VendName,@LocationName,@ProductCode,@SerialNumber,@BatchNumber,@ProductName,@ProductDescrEN,@ProductDescrCH,
                        @StockQty,@QuantityUnitCH,@Ischecked,@number,@ImgUrl,@CreateTime,@UpdateTime);  
                    SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                model.Id = connection.Query<int>(sql, model).Single();
                bool result = model.Id > 0;
                return result;
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改购物车数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateShoppingCart(ShoppingCartModel model)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE ShoppingCart SET ");
            strSql.Append(" number=@number, ");
            strSql.Append(" UpdateTime=@UpdateTime ");
            strSql.Append(" WHERE unionid=@unionid and ProductCode=@ProductCode and LocationName=@LocationName and SerialNumber=@SerialNumber and BatchNumber=@BatchNumber ");
            dp.Add("@unionid", model.unionid, DbType.String, ParameterDirection.Input);
            dp.Add("@ProductCode", model.ProductCode, DbType.String, ParameterDirection.Input);
            dp.Add("@LocationName", model.LocationName, DbType.String, ParameterDirection.Input);
            dp.Add("@SerialNumber", model.SerialNumber, DbType.String, ParameterDirection.Input);
            dp.Add("@BatchNumber", model.BatchNumber, DbType.String, ParameterDirection.Input);

            dp.Add("@number", model.number, DbType.Int32, ParameterDirection.Input);
            dp.Add("@UpdateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                return result;
            }
        }
        /// <summary>
        /// 修改购物车商品选中状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateShoppingCartChecked(ShoppingCartModel model)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE ShoppingCart SET ");
            strSql.Append(" Ischecked=@Ischecked, ");
            strSql.Append(" UpdateTime=@UpdateTime ");
            strSql.Append(" WHERE unionid=@unionid and ProductCode=@ProductCode and LocationName=@LocationName and SerialNumber=@SerialNumber and BatchNumber=@BatchNumber ");
            dp.Add("@unionid", model.unionid, DbType.String, ParameterDirection.Input);
            dp.Add("@ProductCode", model.ProductCode, DbType.String, ParameterDirection.Input);
            dp.Add("@LocationName", model.LocationName, DbType.String, ParameterDirection.Input);
            dp.Add("@SerialNumber", model.SerialNumber, DbType.String, ParameterDirection.Input);
            dp.Add("@BatchNumber", model.BatchNumber, DbType.String, ParameterDirection.Input);

            dp.Add("@Ischecked", model.Ischecked, DbType.Int32, ParameterDirection.Input);
            dp.Add("@UpdateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                return result;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DelShoppingCart(ShoppingCartModel model)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" DELETE FROM ShoppingCart ");
            strSql.Append(" WHERE unionid=@unionid and ProductCode=@ProductCode and LocationName=@LocationName and SerialNumber=@SerialNumber and BatchNumber=@BatchNumber ");
            dp.Add("@unionid", model.unionid);
            dp.Add("@ProductCode", model.ProductCode);
            dp.Add("@LocationName", model.LocationName);
            dp.Add("@SerialNumber", model.SerialNumber);
            dp.Add("@BatchNumber", model.BatchNumber);

            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                return result;
            }
        }
        #endregion

    }
}
