using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RCMLiabilityModel
/// </summary>
public class RCMLiabilityModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BRID { get; set; }
    public int YRCD { get; set; }
    public int MonthID { get; set; }
    public int YearID { get; set; }
    public int YearCode { get; set; }

    public string GSTIN { get; set; }

    public DataTable DtRCM
    {
        get { return _DtRCM; }
        set { _DtRCM = value; }
    }

    private DataTable _DtRCM = new DataTable();
}