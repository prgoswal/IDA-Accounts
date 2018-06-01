using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdvanceRefundModel
/// </summary>
public class AdvanceRefundModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int DocNo { get; set; }
    public int VchType { get; set; }

    public string VchDate { get; set; }
    public int CashBankCode { get; set; }
    public int AccountCode { get; set; }
    public string GSTIN { get; set; }
    public decimal NetAmount { get; set; }
    
    public int ChequeNo { get; set; }
    public string ChequeDate { get; set; }
    public string UTRNo { get; set; }
    public string UTRDate { get; set; }
    public string VchNarration { get; set; }

    public int UserID { get; set; }
    public string IP { get; set; }

    public DataTable DtAR
    {
        get { return InitDtAR; }
        set { InitDtAR = value; }
    }

    private DataTable InitDtAR = new DataTable();
}