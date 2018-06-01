using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdvSuppPaymentModel
/// </summary>
public class AdvSuppPaymentModel
{
    public int Ind { get; set; }
    public string VoucherDate { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int VchType { get; set; }
    public int YrCD { get; set; }
    public string VchDate { get; set; }
    public int CashBankCode { get; set; }
    public int AccountCode { get; set; }
    public string GSTIN { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal GrossAmount { get; set; }
    public int ChequeNo { get; set; }
    public string ChequeDate { get; set; }
    public string UTRNo { get; set; }
    public string UTRDate { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public string VchNarratio { get; set; }
    public int EntryType { get; set; }
    public int ItemID { get; set; }

    // datatable TblAdvItems [AdvanceItemDetail] READONLY 
    public DataTable TblAdvItems { get; set; }

}