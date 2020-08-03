using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTGJWechatDal.BackStage;
using ZTGJWechatDal.HttpData;
using ZTGJWechatDal.Stock;
using ZTGJWechatModel.API;
using ZTGJWechatModel.Applet;
using ZTGJWechatModel.Common;

namespace ZTGJWechatBll.BackStage
{
    public class ReservoirAreaBll
    {
        private ReservoirAreaDal areadal = new ReservoirAreaDal();
        private HttpCommonDal httpcomdal = new HttpCommonDal();
        private StockDal stockdal = new StockDal();

        /// <summary>
        /// 获取公司
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public List<VendCompanyModel> GetCompany(string shortname = "", string fullname = "")
        {
            return areadal.GetCompany(shortname, fullname);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCompany(VendCompanyModel model) {
            return areadal.AddCompany(model);
        }

        /// <summary>
        /// 根据uid获取权限内的库区信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<ReservoirAreaModel> GetRAList(string uid) { 
            return stockdal.GetRAList(uid);
        }
        /// <summary>
        /// 获取库区 全部
        /// </summary>
        /// <param name="vendname"></param>
        /// <param name="rano"></param>
        /// <returns></returns>
        public List<ReservoirAreaModel> GetLocs(string vendname = "", string rano = "")
        {
            return areadal.GetLocs(vendname, rano);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddLoc(ReservoirAreaModel model) {
            return areadal.AddLoc(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateLoc(ReservoirAreaModel model) {
            return areadal.UpdateLoc(model);
        }

        /// <summary>
        /// 公司名称信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Companys_Response companys(string req)
        {
            return httpcomdal.companys(req);
        }

        /// <summary>
        /// 库区
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Locations_Response locations(string req)
        {
            return httpcomdal.locations(req);
        }

    }
}
