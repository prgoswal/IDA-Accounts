using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SalesReturnModel
/// </summary>
public class SalesReturnModel
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
    
    public int ByCashSale { get; set; }
    public string PartyName { get; set; }
    public string PartyGSTIN { get; set; }
    public string PartyAddress { get; set; }
    public int WareHouseID { get; set; }
    public string TransName { get; set; }
    public string PONo { get; set; }
    public string InvoiceSeries { get; set; }
    public int InvoiceNo { get; set; }
    public int DocNo { get; set; }
    public string DocDate { get; set; }

    public int OrgVoucherNo { get; set; }
    public string OrgVoucherDate { get; set; }

    public DataTable DtSales
    {
        get { return InitDtSales; }
        set { InitDtSales = value; }
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

    private DataTable InitDtSales = new DataTable();
    private DataTable InitDtSundries = new DataTable();
    private DataTable InitDtItems = new DataTable();


    //public DataTable DtSales { get; set; }
    //public DataTable DtSundries { get; set; }
    //public DataTable DtItems { get; set; }


    //public string DtSales { get; set; }
    //public string DtSundries { get; set; }
    //public string DtItems { get; set; } 
    public int CCCode { get; set; }
}