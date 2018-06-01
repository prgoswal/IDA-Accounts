using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdateProfileCreationModel
/// </summary>
public class UpdateProfileCreationModel
{
    public int Ind { get; set; }
    public int CompanyID { get; set; }
    public string CompName { get; set; }
    public string ShortName { get; set; }
    public int OrgType { get; set; }
    public int BusiNature { get; set; }
    public int BusiType { get; set; }
    public string Addr { get; set; }
    public string City { get; set; }
    public int State { get; set; }
    public int Pin { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string EMail { get; set; }
    public string PAN { get; set; }
    public string TAN { get; set; }
    public string CIN { get; set; }
    public string IECode { get; set; }
    public int ExportCtg { get; set; }
    public string ContactName { get; set; }
    public string ContactDesg { get; set; }
    public string ContactEmail { get; set; }
    public string ContactMobile { get; set; }
    public string AltPerson { get; set; }
    public string AltPersonDesg { get; set; }
    public string AltPersonEmail { get; set; }
    public string AltMobile { get; set; }
    public int Composition { get; set; }
    public string CompositionDate { get; set; }
    public string GSTIN { get; set; }
    public string RegDate { get; set; }
    public string RegAddr { get; set; }
    public string RegCity { get; set; }
    public int RegState { get; set; }
    public int RegPin { get; set; }
    public string RegAuthPerson { get; set; }
    public string RegAuthDesg { get; set; }
    public string InvoiceNoSeries { get; set; }
    public int InvoiceOnPrePrinted { get; set; }
    public int InvoiceNo { get; set; }
    public int YrCD { get; set; }
    public int User { get; set; }
    public string IP { get; set; }

    public string InvoiceCaption1 { get; set; }
    public int InvoicePrint1 { get; set; }
    public string InvoiceCaption2 { get; set; }
    public int InvoicePrint2 { get; set; }
    public string InvoiceCaption3 { get; set; }
    public int InvoicePrint3 { get; set; }
    public string InvoiceCaption4 { get; set; }
    public int InvoicePrint4 { get; set; }
    public string InvoiceCaption5 { get; set; }
    public int InvoicePrint5 { get; set; }
    public string CompanyLogo { get; set; }
    public int ReportFormat { get; set; }

    public int InvoiceCopyNo { get; set; }
    public string InvoiceCopy1Header { get; set; }
    public string InvoiceCopy2Header { get; set; }
    public string InvoiceCopy3Header { get; set; }
    public string InvoiceCopy4Header { get; set; }
    public string InvoiceCopy5Header { get; set; }

    public int CompositionCategoryID { get; set; }
    public decimal CompositionTaxRate { get; set; }
    public int StcokMaintaneByMinorUnit { get; set; }

    public int UnRegistered { get; set; }
    public int TurnoverID { get; set; }
    public string TurnoverDescription { get; set; }
    public int PrintHSNSACCode { get; set; }

    public string BankName { get; set; }
    public string IFSCCode { get; set; }
    public string AccountNumber { get; set; }
    public int CompanyType { get; set; }
    public int CCCode { get; set; }
    public int SSIsTaken { get; set; }
    public int BSObtain { get; set; }
    public int CostCenterTaken { get; set; }

    public int BudgetConcept { get; set; }


    public DataTable DtTerms
    {
        get { return InitDtTerms; }
        set { InitDtTerms = value; }
    }

    public DataTable DtSeries
    {
        get { return InitDtSeries; }
        set { InitDtSeries = value; }
    }

    private DataTable InitDtTerms = new DataTable();
    private DataTable InitDtSeries = new DataTable();

    public string HeadingColumn1 { get; set; }

    public string HeadingColumn2 { get; set; }

    public string HeadingColumn3 { get; set; }

    public string HeadingColumn4 { get; set; }

    public string HeadingColumn5 { get; set; }

    public int ColumnNumber { get; set; }

    public string BudgetAmount { get; set; }

    public int BankPayChqSeriesInd { get; set; }
}