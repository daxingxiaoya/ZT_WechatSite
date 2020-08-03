using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    /// <summary>
    /// 备件基础信息返回参数
    /// </summary>
    public class Product_Response : ApiBase_Response
    {
        public List<ProductItem> data { set; get; }
    }

    public class ProductItem {
        public int ID { set; get; }
        public string VendorName { set; get; }
        public string ProductCode { set; get; }
        public string ProductCodeC { set; get; }
        public string ProductName { set; get; }
        public string ProductDescrEN { set; get; }
        public string ModelCode { set; get; }
        public string UnitPrice { set; get; }
        public string CurrencyCH { set; get; }
        public string QuantityUnitCode { set; get; }
        public string QuantityUnitCH { set; get; }
        public string QuantityUnitEN { set; get; }
        public string QuantityUnitConvert { set; get; }
        public string PackageCode { set; get; }
        public string Length { set; get; }
        public string Width { set; get; }
        public string Height { set; get; }
        public string NetWeight { set; get; }
        public string GrossWeight { set; get; }
        public string WeightUnitCode { set; get; }
        public string CreatePerson { set; get; }
        public string CreateDate { set; get; }
        public string H2000Index { set; get; }
        public string HSCode { set; get; }
        public string IsSerialNumberMatch { set; get; }
        public string IsGroup { set; get; }
        public string IsActive { set; get; }
        public string Remark { set; get; }
        public string ProductChineseName { set; get; }
        public string PackageEN { set; get; }
        public string SVIDDesc { set; get; }
        public string IsFirstClassify { set; get; }
        public string minPriceUnit { set; get; }
        public string maxPrice { set; get; }
        public string ExtDisplay { set; get; }
        public string MadeInCH { set; get; }
        public string Class { set; get; }
        public string SpecificationModel { set; get; }
        public string H2000BookStatus { set; get; }
        public string minPrice { set; get; }
        public string IsBatchNumberMatch { set; get; }
        public string Brand { set; get; }
        public string UseTo { set; get; }
        public string FirstClassifyDate { set; get; }
        public string QuantityUnitConvertCode { set; get; }
        public string WeightUnitCH { set; get; }
        public string EstimateUnit { set; get; }
        public string maxPriceUnit { set; get; }
        public string QuantityUnitConvertCH { set; get; }
        public string CurrencyEN { set; get; }
        public string UpdateByEDI { set; get; }
        public string ExtCode { set; get; }
        public string MadeInEN { set; get; }
        public string PackageCH { set; get; }
        public string EstimateNetWeight { set; get; }
        public string AutoPrintBarcode { set; get; }
        public string goods { set; get; }
        public string SVID { set; get; }
        public string ProductParentID { set; get; }
        public string Principle { set; get; }
        public string LastClassifyDate { set; get; }
        public string WeightUnitEN { set; get; }
        public string EstimateGrossWeight { set; get; }
        public string H2000Book { set; get; }
        public string CurrencyCode { set; get; }
        public string MadeInCode { set; get; }
        public string QuantityUnitConvertEN { set; get; }
        public string ProductDescrCH { set; get; }
        public List<string> ImgUrl { set; get; }
    }

}
