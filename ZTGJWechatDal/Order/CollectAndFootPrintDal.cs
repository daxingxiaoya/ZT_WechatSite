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
    public class CollectAndFootPrintDal
    {
        #region select
        /// <summary>
        /// 获取单条足迹或者收藏
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="procode"></param>
        /// <param name="locname"></param>
        /// <param name="serialnumber"></param>
        /// <param name="batchNumber"></param>
        /// <param name="datatype">Collect收藏 FootPrint足迹</param>
        /// <returns></returns>
        public CollectAndFootPrintModel GetCollectOrFootPrint(string uid, string procode, string locname, string serialnumber, string batchNumber, string datatype)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                var sqlQuery = @" select Id,unionid,VendName,LocationName,ProductCode,SerialNumber,BatchNumber,ProductName,ProductDescrEN,ProductDescrCH,ImgUrl,CreateDate,CreateTime
                    FROM " + datatype + " with(nolock) where unionid=@unionid and ProductCode=@ProductCode and LocationName=@LocationName and SerialNumber=@SerialNumber and BatchNumber=@BatchNumber ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", uid);
                dp.Add("@ProductCode", procode);
                dp.Add("@LocationName", locname);
                dp.Add("@SerialNumber", serialnumber);
                dp.Add("@BatchNumber", batchNumber);
                return connection.Query<CollectAndFootPrintModel>(sqlQuery, dp).FirstOrDefault();
            }
        }
        /// <summary>
        /// 获取用户足迹或收藏 带分页
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <param name="datatype">Collect收藏 FootPrint足迹</param>
        /// <returns></returns>
        public List<CollectAndFootPrintModel> GetCOrFByPage(string uid, out int totalcount, string datatype, int index = 1, int pagesize = 10)
        {
            totalcount = 0;
            List<CollectAndFootPrintModel> reslist = new List<CollectAndFootPrintModel>();

            List<CollectAndFootPrintModel> alllist = new List<CollectAndFootPrintModel>();
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                string sql = " select Id,unionid,VendName,LocationName,ProductCode,SerialNumber,BatchNumber,ProductName,ProductDescrEN,ProductDescrCH,ImgUrl,CreateDate,CreateTime ";
                sql += " FROM "+ datatype + " with(nolock) where unionid=@unionid ";
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@unionid", uid);
                if (datatype == "FootPrint")
                {
                    sql += " and CreateTime>@CreateTime ";
                    dp.Add("@CreateTime", DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd MM:mm:ss"));
                }
                alllist = connection.Query<CollectAndFootPrintModel>(sql, dp).ToList();
            }
            if (alllist.Count > 0)
            {
                var grouplist = alllist.OrderByDescending(o=>o.CreateTime).GroupBy(g => new { g.LocationName, g.ProductCode, g.SerialNumber, g.BatchNumber }).
                    Select(s=>new CollectAndFootPrintModel{
                        Id = s.FirstOrDefault().Id,
                        unionid = s.FirstOrDefault().unionid,
                        VendName = s.FirstOrDefault().VendName,
                        LocationName = s.FirstOrDefault().LocationName,
                        ProductCode = s.FirstOrDefault().ProductCode,
                        SerialNumber = s.FirstOrDefault().SerialNumber,
                        BatchNumber = s.FirstOrDefault().BatchNumber,
                        ProductName = s.FirstOrDefault().ProductName,
                        ProductDescrEN = s.FirstOrDefault().ProductDescrEN,
                        ProductDescrCH = s.FirstOrDefault().ProductDescrCH,
                        ImgUrl= s.FirstOrDefault().ImgUrl,
                        CreateDate= s.FirstOrDefault().CreateDate,
                        CreateTime = s.FirstOrDefault().CreateTime
                    }).ToList();
                foreach (var item in grouplist)
                {
                    reslist.Add(item);
                }
                totalcount = reslist.Count();
                if (datatype != "FootPrint")
                {
                    reslist = reslist.Skip(pagesize * (index - 1)).Take(pagesize * index - 1).ToList();
                }
            }


            return reslist;
        }
        #endregion

        #region 新增和修改
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="datatype">Collect收藏 FootPrint足迹</param>
        /// <returns></returns>
        public bool AddCollectOrFootPrint(CollectAndFootPrintModel model, string datatype)
        {
            using (IDbConnection connection = new SqlConnection(DBConnectionStringConfig.Default.WechatServerDBReadConnStr))
            {
                string sql = " INSERT INTO "+datatype;
                sql += " (unionid,VendName,LocationName,ProductCode,SerialNumber,BatchNumber,ProductName,ProductDescrEN,ProductDescrCH,ImgUrl,CreateDate,CreateTime) ";
                sql += " VALUES ";
                sql += " (@unionid,@VendName,@LocationName,@ProductCode,@SerialNumber,@BatchNumber,@ProductName,@ProductDescrEN,@ProductDescrCH,@ImgUrl,@CreateDate,@CreateTime); ";
                sql += " SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                model.Id = connection.Query<int>(sql, model).Single();
                bool result = model.Id > 0;
                return result;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除足迹或者收藏
        /// </summary>
        /// <param name="model"></param>
        /// <param name="datatype">Collect收藏 FootPrint足迹</param>
        /// <returns></returns>
        public bool DelCollectOrFootPrint(CollectAndFootPrintModel model, string datatype)
        {
            StringBuilder strSql = new StringBuilder();
            DynamicParameters dp = new DynamicParameters();
            strSql.Append(" DELETE FROM " + datatype);
            strSql.Append(" WHERE unionid=@unionid and ProductCode=@ProductCode and LocationName=@LocationName and SerialNumber=@SerialNumber and BatchNumber=@BatchNumber  ");
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
