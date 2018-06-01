using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CashSaleReturnModel
/// </summary>
public class CashSaleReturnModel
{
    public int Ind { get; set; }
    public string VoucherDate { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int VchType { get; set; }
    public int YrCD { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int EntryType { get; set; }

    public decimal ItemAmt { get; set; }
    public int ItemID { get; set; }
    public int AccCode { get; set; }
    public int PartyCode { get; set; }
    public string GSTIN { get; set; }

    public string PayMode { get; set; }
    public string PayModeRemark { get; set; }
    public int InvoiceNo { get; set; }
    public string InvoiceSeries { get; set; }
    public string CancelReason { get; set; }

    public int ByCashSale { get; set; }
    public string PartyName { get; set; }
    public string PartyGSTIN { get; set; }
    public string PartyAddress { get; set; }
    public long PartyMobileNo { get; set; }
    public int WareHouseID { get; set; }
    public string TransName { get; set; }
    public string PONo { get; set; }
    public int PartyState { get; set; }

   // public DataSet ds = new DataSet();

    //public DataTable DtCashSales { get; set; }
    //public DataTable DtSundries { get; set; }
    //public DataTable DtItems { get; set; }
    //public DataTable DtAdjAdvance { get; set; } 


    public DataTable DtCashSales
    {
        get { return InitDtCashSales; }
        set { InitDtCashSales = value; }
    }
    public DataTable DtSundries
    {
        get { return InitDtSundries; }
        set { InitDtSundries = value; }
    }
    public DataTable DtItems
    {
        get { return InitDtItems; }
        set { InitDtItems = value; }
    }
    public DataTable DtAdjAdvance
    {
        get { return InitDtAdjAdvance; }
        set { InitDtAdjAdvance = value; }
    }

    private DataTable InitDtCashSales = new DataTable();
    private DataTable InitDtSundries = new DataTable();
    private DataTable InitDtItems = new DataTable();
    private DataTable InitDtAdjAdvance = new DataTable();


    public int BrokerageID { get; set; }

    public decimal BrokerageRate { get; set; }

    public string BrokerageGSTIN { get; set; }

    public decimal BrokerageAmount { get; set; }


    public int CCCode { get; set; }
}