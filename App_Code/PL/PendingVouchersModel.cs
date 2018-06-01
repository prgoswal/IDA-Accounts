using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PendingVouchersModel
/// </summary>
public class PendingVouchersModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int VchType { get; set; }
    public int TransactionNo { get; set; }
    public string TransactionDate { get; set; }
    public int BankPayVType { get; set; }
    public int BankPayTNo { get; set; }
    public string ApprovalDate { get; set; }
    public string ApprovalRemark { get; set; }
    public int UserLevel { get; set; }
    public int IsFinal { get; set; }
    public int IsAudit { get; set; }
    public int IsAdmin { get; set; }
    public int DepartmentID { get; set; }
    public int SubDepartmentID { get; set; }
}