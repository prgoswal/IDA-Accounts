using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdatePurchaseReturnVoucherModel
/// </summary>
public class UpdatePurchaseReturnVoucherModel
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
    public string PODate { get; set; }

    public string PurchaseBillNo { get; set; }
    public string PurchaseBillDate { get; set; }

    public string GRNNo { get; set; }
    public string GRNDate { get; set; }

    public long PurchaseSaleRecordID { get; set; }

    public int VoucherNo { get; set; }

    public int DocNo { get; set; }
    public string DocDate { get; set; }

    public int OrgVoucherNo { get; set; }
    public string OrgVoucherDate { get; set; }

    public string CancelReason { get; set; }

    public DataTable DtUpdPurchase
    {
        get { return InitDtUdpPurchase; }
        set { InitDtUdpPurchase = value; }
    }
    public DataTable DtUpdSundries
    {
        get { return InitDtUpdSundries; }
        set { InitDtUpdSundries = value; }
    }
    public DataTable DtUpdItems
    {
        get { return InitDtUpdItems; }
        set { InitDtUpdItems = value; }
    }

    private DataTable InitDtUdpPurchase = new DataTable();
    private DataTable InitDtUpdSundries = new DataTable();
    private DataTable InitDtUpdItems = new DataTable();

    public int CCCode { get; set; }
}