using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdateAccounHeadModel
/// </summary>
public class UpdateAccounHeadModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int MainGroupID { get; set; }
    public int SubGroupID { get; set; }
    public int AccGroupID { get; set; }
    public int AccSubGroupID { get; set; }
    public long AccCode { get; set; }
    public string AccName { get; set; }
    public string AccNameHindi { get; set; }

    public string Address { get; set; }
    public string City { get; set; }
    public int StateID { get; set; }
    public int PinCode { get; set; }
    public string PanNo { get; set; }
    public long MobileNo { get; set; }
    public string LandlineNo { get; set; }
    public string Email { get; set; }
    public string ContactPerson { get; set; }
    public decimal DrOpBalance { get; set; }
    public decimal CrOpBalance { get; set; }
    public int CategoryID { get; set; }
    public int ISDApplicable { get; set; }
    public int TDSApplicable { get; set; }
    public int RCMApplicable { get; set; }
    public int TCSApplicable { get; set; }
    public string MerchantID { get; set; }
    public string BankName { get; set; }
    public string BranchName { get; set; }
    public string IFSCCode { get; set; }
    public string BankAccountNo { get; set; }
    public int IsAdmin { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int CompositionOpted { get; set; }
    public string ReffPartyCode { get; set; }
    public int IsSubDealer { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal BrokerageRate { get; set; }
    public int TaxCalcForSEZParty { get; set; }
    public string Remark { get; set; }
    public decimal BrokerageLimit { get; set; }
    public int BrokerageType { get; set; }

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