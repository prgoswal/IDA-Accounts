using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlBankReceipt
/// </summary>
public class InterBranchModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int VchType { get; set; }
    public int LastNo { get; set; }
    public int VchRangeFrom { get; set; }
    public int VchRangeTo { get; set; }
    public int YrCD { get; set; }
    public string DocDate { get; set; }
    public int DocNo { get; set; }
    public int AccCode { get; set; }
    public string AccGst { get; set; }
    public int AccCode2 { get; set; }
    public int RefNo { get; set; }
    public string RefDate { get; set; }
    public decimal AmountDr { get; set; }
    public decimal AmountCr { get; set; }
    public int AdvanceInd { get; set; }
    public string DocDesc { get; set; }
    public int EntryType { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int InterBrID { get; set; }
    public int BankCode { get; set; }

    // Using Multi Purpose Things
    public System.Data.DataTable Dt { get; set; }
}