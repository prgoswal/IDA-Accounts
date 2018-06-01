
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BudgetAmount
/// </summary>
public class BudgetYearActivationModel
{
    public int Ind { get; set; }
    public int YrCode { get; set; }
    public string YearFromTo { get; set; }
    public string YrStartDate { get; set; }
    public string YrEndDate { get; set; }
    public int ActiveID { get; set; }
    public int UserID { get; set; }
    public string IPAddress { get; set; }
    public int AccountInd { get; set; }
    public int BudgetInd { get; set; }
    public string BudgetOrderNumber { get; set; }
    public string BudgetOrderDate { get; set; }
    public string BudgetEntryDate { get; set; }
    public string AccountingOrderNumber { get; set; }
    public string AccountingOrderDate { get; set; }
    public string AccountingEntryDate { get; set; }

}