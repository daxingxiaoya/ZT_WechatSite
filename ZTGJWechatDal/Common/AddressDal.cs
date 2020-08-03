using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZTGJWechatModel.Common;
using ZTGJWechatUtils;

namespace ZTGJWechatDal.Common
{
    public class AddressDal
    {
        #region select
        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="reqm"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<AddressModel> GetAddress(AddressModel reqm)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,unionid,name,phone,country,province,city,area,address,alias,isdefault,status,CreateTime,UpdateTime
                    FROM Address with(nolock) where unionid=@unionid ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", reqm.unionid);
                if (reqm.isdefault == 1)
                {
                    sqlQuery += " and isdefault=@isdefault ";
                    dp.Add("@isdefault", reqm.isdefault);
                }
                if (reqm.Id != 0)
                {
                    sqlQuery += " and Id=@Id ";
                    dp.Add("@Id", reqm.Id);
                }
                if (!string.IsNullOrEmpty(reqm.name))
                {
                    sqlQuery += " and name like '%" + reqm.name + "%' ";
                }
                if (!string.IsNullOrEmpty(reqm.phone))
                {
                    sqlQuery += " and phone like '%" + reqm.phone + "%' ";
                }
                sqlQuery += " order by UpdateTime desc ";
                return connection.Query<AddressModel>(sqlQuery, dp).ToList();
            }
        }
        
        #endregion

        #region 新增和修改
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddAddress(AddressModel model)
        {
            string sql = " INSERT INTO Address ";
            sql += " (unionid,name,phone,country,province,city,area,address,alias,isdefault,status,CreateTime,UpdateTime) ";
            sql += " VALUES ";
            sql += " (@unionid,@name,@phone,@country,@province,@city,@area,@address,@alias,@isdefault,@status,@CreateTime,@UpdateTime); ";
            sql += " SELECT CAST(SCOPE_IDENTITY() AS INT) ";
            //如果是默认地址，则取消原来默认地址
            if (model.isdefault == 1)
            {
                UpdateDefaultAddressAll(model.unionid);
            }
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                model.Id = connection.Query<int>(sql, model).Single();
                bool result = model.Id > 0;
                return result;
            }
        }
        /// <summary>
        /// 编辑地址
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateAddress(AddressModel model)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Address set ");
            strSql.Append(" name=@name,phone=@phone,country=@country,province=@province,city=@city,area=@area,address=@address,alias=@alias, ");
            strSql.Append(" isdefault=@isdefault,UpdateTime=@UpdateTime ");
            strSql.Append(" WHERE Id=@Id and unionid=@unionid ");
            dp.Add("@Id", model.Id, DbType.Int32, ParameterDirection.Input);
            dp.Add("@unionid", model.unionid, DbType.String, ParameterDirection.Input);

            dp.Add("@name", model.name, DbType.String, ParameterDirection.Input);
            dp.Add("@phone", model.phone, DbType.String, ParameterDirection.Input);
            dp.Add("@country", model.country, DbType.String, ParameterDirection.Input);
            dp.Add("@province", model.province, DbType.String, ParameterDirection.Input);
            dp.Add("@city", model.city, DbType.String, ParameterDirection.Input);
            dp.Add("@area", model.area, DbType.String, ParameterDirection.Input);
            dp.Add("@address", model.address, DbType.String, ParameterDirection.Input);
            dp.Add("@alias", model.alias, DbType.String, ParameterDirection.Input);

            dp.Add("@isdefault", model.isdefault, DbType.Int32, ParameterDirection.Input);
            dp.Add("@UpdateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
            //如果是默认地址，则取消原来默认地址
            if (model.isdefault == 1)
            {
                UpdateDefaultAddressAll(model.unionid);
            }
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                return result;
            }
        }
        /// <summary>
        /// 更改地址为默认
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateDefaultAddress(string uid, int id)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@unionid", uid, DbType.String, ParameterDirection.Input);
            result = UpdateDefaultAddressAll(uid);
            if (result)
            {
                strSql.Append(" UPDATE Address SET isdefault=1,UpdateTime=@UpdateTime WHERE Id=@Id and unionid=@unionid ");
                dp.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
                dp.Add("@UpdateTime", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
                {
                    result = connection.Execute(strSql.ToString(), dp) > 0;
                }
            }
            return result;
        }
        /// <summary>
        /// 修改为全部不默认
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UpdateDefaultAddressAll(string uid)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" UPDATE Address SET isdefault=0 WHERE unionid=@unionid; ");
            dp.Add("@unionid", uid, DbType.String, ParameterDirection.Input);
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                return connection.Execute(strSql.ToString(), dp) > 0;
            }
        }

        #endregion

        #region 删除
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelAddress(string uid,string id)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" DELETE FROM Address ");
            strSql.Append(" WHERE Id=@Id and unionid=@unionid ");
            dp.Add("@unionid", uid);
            dp.Add("@Id", Convert.ToInt32(id));

            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                bool result = connection.Execute(strSql.ToString(), dp) > 0;
                return result;
            }
        }
        #endregion
    }
}
