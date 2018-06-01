using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccountHeadModel
/// </summary>
public class AccountHeadModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int VchType { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int EntryType { get; set; }
    public int IsAdmin { get; set; }
    public int CompositionOpted { get; set; }
    public string ReffPartyCode { get; set; }

    public int IsSubDealer { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal BrokerageRate { get; set; }
    public int TaxCalcForSEZParty { get; set; }

    public string AccountHeadHindi { get; set; }

    public DataTable DtAccount
    {
        get { return InitDtAccount; }
        set { InitDtAccount = value; }
    }
    public DataTable DtAccGSTIN
    {
        get { return InitDtAccGSTIN; }
        set { InitDtAccGSTIN = value; }
    }
    public DataTable DtAccPOS
    {
        get { return InitDtAccPOS; }
        set { InitDtAccPOS = value; }
    }

    public DataTable TblAccTerms
    {
        get { return InitTblAccTerms; }
        set { InitTblAccTerms = value; }
    }

    private DataTable InitDtAccount = new DataTable();
    private DataTable InitDtAccGSTIN = new DataTable();
    private DataTable InitDtAccPOS = new DataTable();
    private DataTable InitTblAccTerms = new DataTable();
}