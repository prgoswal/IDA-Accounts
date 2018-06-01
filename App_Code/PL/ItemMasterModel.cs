using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemMasterModel
/// </summary>
public class ItemMasterModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int ItemMainID { get; set; }
    public int ItemSubID { get; set; }
    public int ItemMinorID { get; set; }
    public int ItemGroupID { get; set; }
    public string ItemName { get; set; }
    public string ItemShortName { get; set; }
    public int ItemUnitID { get; set; }
    public decimal ItemSellingRate { get; set; }
    public int GoodServiceInd { get; set; }
    public string ItemDesc { get; set; }
    public string HSNSACCode { get; set; }
    public long ItemCode { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int EntryType { get; set; }
    public long HSNSACCode1 { get; set; }

    public int ItemMinorUnitID { get; set; }
    public decimal ItemMinorUnitQyt { get; set; }
    public int StockMaintainInd { get; set; }
    public decimal TaxRate { get; set; }
    public string TaxRateDesc { get; set; }

    public DataTable DtItemOpening
    {
        get { return InitDtItemOpening; }
        set { InitDtItemOpening = value; }
    }
    private DataTable InitDtItemOpening = new DataTable();

}