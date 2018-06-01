using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdBankPaymentModel
/// </summary>
public class UpdBankPaymentModel
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
    public string CancelReason { get; set; }
    // Using Multi Purpose Things
    //public System.Data.DataTable Dt { get; set; }

    public string Dt { get; set; }

    public int CCCode { get; set; }

    public string IDARefNo { get; set; }

    public int DeptID { get; set; }
    public int SubDeptID { get; set; }

    public int TransactionNo { get; set; }
    public string TransactionDate { get; set; }
    public string ApprovalDate { get; set; }
    public string ApprovalRemark { get; set; }
    public int UserLevel { get; set; }
    public int ApprovalCount { get; set; }
    public int IsConfirmed { get; set; }
    public int IsRejected { get; set; }
    public int IsAudit { get; set; }
    public int IsSendToAudit { get; set; }
    public int IsFinal { get; set; }
    public int BankPayVoucherInd { get; set; }
    public string ChequeDrawn { get; set; }
}