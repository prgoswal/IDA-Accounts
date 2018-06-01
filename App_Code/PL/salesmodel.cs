using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SalesModel
/// </summary>
public class SalesModel
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
    public string CancelReason { get; set; }
    public int AdvRecPayID { get; set; }
    public int BrokerageID { get; set; }
    public decimal BrokerageRate { get; set; }
    public string BrokerageGSTIN { get; set; }
    public decimal BrokerageAmount { get; set; }
  


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
    public DataTable DtAdjAdvance
    {
        get { return InitDtAdjAdvance; }
        set { InitDtAdjAdvance = value; }
    }

    private DataTable InitDtSales = new DataTable();
    private DataTable InitDtSundries = new DataTable();
    private DataTable InitDtItems = new DataTable();
    private DataTable InitDtAdjAdvance = new DataTable();


    public int CCCode { get; set; }
}