using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GSTINSubmitModel
/// </summary>
public class GSTINSubmitModel
{
    #region Old
    //public int Ind { get; set; }
    //public int OrgID { get; set; }
    //public int BrID { get; set; }
    //public int YrCD { get; set; }
    //public string GSTIN { get; set; }
    //public int TaxYear { get; set; }
    //public int TaxMonth { get; set; } 
    //public int DocTypeID{get;set;}
    //public string DocDesc{get;set;}
    //public int LineNo { get; set; }
    //public Int64 ExcelNo { get; set; }
    //public string ImportDate { get; set; }
    //public int CACode { get; set; }
    //public int CACodeODP { get; set; }
    //public int BillGSTNType { get; set; }
    //public string BillName { get; set; }
    //public string ShippedGSTNNo { get; set; }
    //public int ShippedGSTNType { get; set; }
    //public string ShippedName { get; set; }
    //public string EcommerceDesc { get; set; }
    //public string ECommGSTNNo { get; set; }
    //public string GSDesc { get; set; }
    //public int ItemCode { get; set; }
    //public string HSNSacDesc { get; set; }
    //public int ItemTypeCode { get; set; }
    //public decimal ItemIGSTOth1 { get; set; }
    //public decimal ItemIGSTOth2 { get; set; }
    //public decimal ItemICGSTOth1 { get; set; }
    //public decimal ItemICGSTOth2 { get; set; }
    //public decimal ItemISGSTOth1 { get; set; }
    //public decimal ItemISGSTOth2 { get; set; }
    //public decimal ItemTotTaxPaid { get; set; }
    //public decimal ItemTotTaxPaidoth1 { get; set; }
    //public decimal ItemTotTaxPaidoth2 { get; set; }
    //public string ReverseChargeDesc { get; set; }
    //public decimal ItemReversePer { get; set; }
    //public decimal ItemReverseAmt { get; set; }
    //public string Blank1 { get; set; }
    //public string Blank2 { get; set; }
    //public string Blank3 { get; set; }
    //public int Blank4 { get; set; }
    //public int Blank5 { get; set; }
    //public int Blank6 { get; set; }
    //public string AdvanceSaleDesc { get; set; }
    //public string AdvREcvOrAdjustDesc { get; set; }
    //public int ZeroDeemedSezCode { get; set; }
    //public int ErrorInd { get; set; }
    #endregion

    public int ind { get; set; }
    public int ClientCode { get; set; }
    public int ClientCodeOdp { get; set; }
    public int CACode { get; set; }
    public int CaCodeOdp { get; set; }
    public int YrCode { get; set; }
    public int MonthCD { get; set; }
    public string ClientGSTNNO { get; set; }
    public int ExcelNo { get; set; }
    public int UserCode { get; set; }

    //public DataSet ds = new DataSet();
    public DataSet ds
    {
        get { return _ds; }
        set { _ds = value; }
    }

    DataSet _ds = new DataSet();

    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public string GSTIN { get; set; }
    public int TaxYear { get; set; }
    public int TaxMonth { get; set; }
}