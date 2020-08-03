using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatModel.API
{
    public class CreateOrder_Request : ApiBase_Request
    {
        public CreateOrderHead head { set; get; }
        public List<CreateOrderLine> lines { set; get; }
    }

    public class CreateOrderHead {
        public CreateOrderHead() {
            VendorName = "";
            ReceiverName = "";
            ReceiverMobile = "";
            ReceiverCountry = "";
            ReceiverProvince = "";
            ReceiverCity = "";
            ReceiverAddress = "";
            ReceiverZip = "";
            ReceiverContactor = "";
            ReceiverTel = "";
            ReceiverFax = "";
            ReceiverEmail = "";
            ID = 0;
            Status = 0;
            ServiceLevelName = "";
            WayBillNumber = "";
            ContractNo = "";
            RefCode = "";
            OutTypeName = "";
            Weight = 0;
            PackagePcs = 0;
            Amount = 0;
            RequireCancel = true;
            OOB = true;
            isEDI = true;

            TransportAgentName = "";TransportTypeName = "";WeightUnitCode = "";Volume = "";CurrencyCode = "";CreatePerson = "";
            EngineerInfo = "";Ext1 = "";PackageCode = "";WeightUnitCH = "";EngineerNumber = "";Ext2 = "";PackageCH = "";PodPerson = "";CurrencyCH = "";WebJobCode = ""; PodDate = "";
        }
        public string VendorName { set; get; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string ReceiverName { set; get; }
        /// <summary>
        /// 收件 手机号
        /// </summary>
        public string ReceiverMobile { set; get; }
        /// <summary>
        /// 收件国家
        /// </summary>
        public string ReceiverCountry { set; get; }
        /// <summary>
        /// 收件省份
        /// </summary>
        public string ReceiverProvince { set; get; }
        /// <summary>
        /// 收件城市
        /// </summary>
        public string ReceiverCity { set; get; }
        /// <summary>
        /// 区
        /// </summary>
        public string ReceiverDistrict { set; get; }
        /// <summary>
        /// 收件详细地址
        /// </summary>
        public string ReceiverAddress { set; get; }
        /// <summary>
        /// 收件邮编
        /// </summary>
        public string ReceiverZip { set; get; }
        public string ReceiverContactor { set; get; }
        public string ReceiverTel { set; get; }
        /// <summary>
        /// 收件传真
        /// </summary>
        public string ReceiverFax { set; get; }
        /// <summary>
        /// 收件邮箱
        /// </summary>
        public string ReceiverEmail { set; get; }
        
        public int ID { set; get; }
        public int Status{ set; get; }
        public string ServiceLevelName { set; get; }
        public string WayBillNumber { set; get; }
        public string ContractNo { set; get; }
        public string RefCode { set; get; }
        public string OutTypeName { set; get; }
        public string TransportAgentName { set; get; }
        public string TransportTypeName { set; get; }
        public int Weight { set; get; }
        public string WeightUnitCode { set; get; }
        public string Volume { set; get; }
        public int PackagePcs { set; get; }
        public int Amount { set; get; }
        public string CurrencyCode { set; get; }
        public string CreatePerson { set; get; }
        public string CreateDate { set; get; }
        public string ReservationDate { set; get; }
        public string LimitDate { set; get; }
        public bool RequireCancel { set; get; }
        public string Remark { set; get; }
        public string EngineerInfo { set; get; }
        public string Ext1 { set; get; }
        public string PackageCode { set; get; }
        public string WeightUnitCH { set; get; }
        public string EngineerNumber { set; get; }
        public string EngineerMobile { set; get; }
        public string Ext2 { set; get; }
        public string PackageCH { set; get; }
        
        public string PodPerson { set; get; }
        public string CurrencyCH { set; get; }
        public bool OOB { set; get; }
        public string WebJobCode { set; get; }
        public bool isEDI { set; get; }
        public string PodDate { set; get; }
        public List<AppWebOutLine> AppWebOutLines { set; get; }
    }

    public class AppWebOutLine {
        public int ID { set; get; }
        public int WebOutHeadID { set; get; }
        public int InventoryID { set; get; }
        public string ProductCode { set; get; }
        public string ModelCode { set; get; }
        public string ProductName { set; get; }
        public string ProductDescrCH { set; get; }
        public string ProductDescrEN { set; get; }
        public string SerialNumber { set; get; }
        public string H2000Index { set; get; }
        public string HSCode { set; get; }
        public string UnitPrice { set; get; }
        public string CurrencyCH { set; get; }
        public string CurrencyEN { set; get; }
        public string ShipQty { set; get; }
        public string QuantityUnitCH { set; get; }
        public string QuantityUnitEN { set; get; }
        public string LegalShipQty { set; get; }
        public string LegalUnitCode { set; get; }
        public string LocationName { set; get; }
        public string Bin { set; get; }
        public string PackageCode { set; get; }
        public string ShipCode { set; get; }
        public string ProductLength { set; get; }
        public string ProductWidth { set; get; }
        public string ProductHeight { set; get; }
        public string NetWeight { set; get; }
        public string GrossWeight { set; get; }
        public string WeightUnitCode { set; get; }
        public string VolumeWeight { set; get; }
        public string MadeIn { set; get; }
        public string MadeInEN { set; get; }
        public string ProductStatus { set; get; }
        public string Remark { set; get; }
        public string QuantityUnitCode { set; get; }
        public string LegalUnitEN { set; get; }
        public string Ext3 { set; get; }
        public string ProductCodeC { set; get; }
        public string WeightUnitEN { set; get; }
        public string IsGroup { set; get; }
        public string RMACode { set; get; }
        public string Ext1 { set; get; }
        public string LineIndex { set; get; }
        public string CurrencyCode { set; get; }
        public string H2000Book { set; get; }
        public string WeightUnitCH { set; get; }
        public string PackageCH { set; get; }
        public string POTypeName { set; get; }
        public string MadeInCode { set; get; }
        public string PONumber { set; get; }
        public string PackageEN { set; get; }
        public string SAP_POSNR { set; get; }
        public string LegalUnitCH { set; get; }
        public string Ext2 { set; get; }
        public string AppWebOutHead { set; get; }
    }

    public class CreateOrderLine
    {
        public CreateOrderLine() {
            ID = 0;
            WebOutHeadID = 0;
            InventoryID = 0;
            UnitPrice = 0;
            ShipQty = 0;
            LegalShipQty = 0;
            ProductLength = 0;
            ProductWidth = 0;
            ProductHeight = 0;
            NetWeight = 0;
            GrossWeight = 0;
            VolumeWeight = 0;
            ProductStatus = 0;
            IsGroup = true;
            LineIndex = 0;
            ModelCode = "";ProductName = "";ProductDescrCH = "";ProductDescrEN = "";SerialNumber = "";H2000Index = ""; HSCode = "";
            CurrencyCH = "";CurrencyEN = "";QuantityUnitCH = "";QuantityUnitEN = "";LegalUnitCode = "";LocationName = "";Bin = ""; PackageCode = "";
            ShipCode = "";WeightUnitCode = "";MadeIn = "";MadeInEN = "";Remark = "";QuantityUnitCode = "";LegalUnitEN = ""; Ext3 = "";
            ProductCodeC = "";WeightUnitEN = "";RMACode = "";Ext1 = "";CurrencyCode = "";H2000Book = ""; WeightUnitCH = "";
            PackageCH = "";POTypeName = "";MadeInCode = "";PONumber = "";PackageEN = "";SAP_POSNR = "";LegalUnitCH = "";Ext2 = "";
        }
        public int ID { set; get; }
        public int WebOutHeadID { set; get; }
        public int InventoryID { set; get; }
        public string ProductCode { set; get; }
        public string ModelCode { set; get; }
        public string ProductName { set; get; }
        public string ProductDescrCH { set; get; }
        public string ProductDescrEN { set; get; }
        public string SerialNumber { set; get; }
        public string H2000Index { set; get; }
        public string HSCode { set; get; }
        public int UnitPrice { set; get; }
        public string CurrencyCH { set; get; }
        public string CurrencyEN { set; get; }
        public int ShipQty { set; get; }
        public string QuantityUnitCH { set; get; }
        public string QuantityUnitEN { set; get; }
        public int LegalShipQty { set; get; }
        public string LegalUnitCode { set; get; }
        public string LocationName { set; get; }
        public string Bin { set; get; }
        public string PackageCode { set; get; }
        public string ShipCode { set; get; }
        public int ProductLength { set; get; }
        public int ProductWidth { set; get; }
        public int ProductHeight { set; get; }
        public int NetWeight { set; get; }
        public int GrossWeight { set; get; }
        public string WeightUnitCode { set; get; }
        public int VolumeWeight { set; get; }
        public string MadeIn { set; get; }
        public string MadeInEN { set; get; }
        public int ProductStatus { set; get; }
        public string Remark { set; get; }
        public string QuantityUnitCode { set; get; }
        public string LegalUnitEN { set; get; }
        public string Ext3 { set; get; }
        public string ProductCodeC { set; get; }
        public string WeightUnitEN { set; get; }
        public bool IsGroup { set; get; }
        public string RMACode { set; get; }
        public string Ext1 { set; get; }
        public int LineIndex { set; get; }
        public string CurrencyCode { set; get; }
        public string H2000Book { set; get; }
        public string WeightUnitCH { set; get; }
        public string PackageCH { set; get; }
        public string POTypeName { set; get; }
        public string MadeInCode { set; get; }
        public string PONumber { set; get; }
        public string PackageEN { set; get; }
        public string SAP_POSNR { set; get; }
        public string LegalUnitCH { set; get; }
        public string Ext2 { set; get; }
        public CreateOrderHead AppWebOutHead { set; get; }
        
    }

}
