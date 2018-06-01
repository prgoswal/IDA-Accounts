using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlFinancialYear
/// </summary>
public class FinancialYearModel
{
    public int YrCode { get; set; }
    public string YearFromTo { get; set; }
    public DateTime YrStartDate { get; set; }
    public DateTime YrEndDate { get; set; }
    public int ActiveID { get; set; }
    public int UserID { get; set; }
    public string IPAddress { get; set; }
    public DateTime EntryDate { get; set; }
}