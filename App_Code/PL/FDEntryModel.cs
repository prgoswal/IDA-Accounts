using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for ChequeSeriesModel
/// </summary>
public class FDEntryModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public string DespositDate { get; set; }
    public int BankCode { get; set; }
    public string FDNumber { get; set; }
    public string MaturityDate { get; set; }
    public decimal DepositAmount { get; set; }
    public decimal MaturityAmount { get; set; }
    public decimal ROI { get; set; }
    public string FDRAccNumber { get; set; }
    public int PeriodValue { get; set; }
    public string PeriodType { get; set; }
    public int DepositYear { get; set; }
    public int DepositMonth { get; set; }
    public int DepositDay { get; set; }

    public string SchemeType { get; set; }
    public decimal LienAmount { get; set; }
    public string LienDate { get; set; }
    public string Narration { get; set; }
    public bool IsODLien { get; set; }
    public int User { get; set; }
    public string IP { get; set; }


    public int VchType { get; set; }
}