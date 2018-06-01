using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseModel
/// </summary>
public class CompositionPurchaseModel
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
    public string PONo { get; set; }

    public string PurchaseBillNo { get; set; }
    public string PurchaseBillDate { get; set; }
    public string GRNNo { get; set; }
    public int AdvRecPayID { get; set; }

    public DataTable DtPurchase
    {
        get { return InitDtPurchase; }
        set { InitDtPurchase = value; }
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

    private DataTable InitDtPurchase = new DataTable();
    private DataTable InitDtSundries = new DataTable();
    private DataTable InitDtItems = new DataTable();
    private DataTable InitDtAdjAdvance = new DataTable();

    
}