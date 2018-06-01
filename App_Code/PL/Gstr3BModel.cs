using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Gstr3BModel
/// </summary>
public class Gstr3BModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public string GSTIN { get; set; }
    public int YrCD { get; set; }
    public int TaxMonth { get; set; }
    public int TaxYear { get; set; }
}