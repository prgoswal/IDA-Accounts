using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BranchMasterModel
/// </summary>
public class BranchMasterModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int YrCD { get; set; }
    public string BranchName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int StateID { get; set; }
    public int PinCode { get; set; }
    public string InvoiceNoSeries  { get; set; }
    public long InvoiceNo { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int GSTINID { get; set; }
    public string GSTIN { get; set; }

    public DataTable DtSeries
    {
        get { return InitDtSeries; }
        set { InitDtSeries = value; }
    }

    private DataTable InitDtSeries = new DataTable();
}