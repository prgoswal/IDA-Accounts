using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OldBalanceModel
/// </summary>
public class OldBalanceModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int AccCode { get; set; }
    public int BookNo { get; set; }
    public int PageNo { get; set; }
    public int SerialNo { get; set; }
    public int ReferenceNo { get; set; }
    public int TenderNo { get; set; }
    public string TenderDate { get; set; }
    public int PartyCD { get; set; }
    public string PartyName { get; set; }
    public int CostCentreCD { get; set; }
    public string OpeningDate { get; set; }
    public decimal Amount { get; set; }
    public int UserID { get; set; }
    public string IPAddress { get; set; }
    public decimal BSAmount { get; set; }
}