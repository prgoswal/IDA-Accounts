using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NewBudgetAmountModel
/// </summary>
public class NewBudgetAmountModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCode { get; set; }
    public int SectionCD { get; set; }
    public int SubSectionCD { get; set; }
    public int BudgetHeadCD { get; set; }

    public int SecCode { get; set; }

    public decimal PropBudgetAmtDr { get; set; }
    public decimal PropBudgetAmtCr { get; set; }

    public decimal Prop2BudgetAmtDr { get; set; }
    public decimal Prop2BudgetAmtCr { get; set; }

    public decimal Sanc2BudgetAmtDr { get; set; }
    public decimal Sanc2BudgetAmtCr { get; set; }

    public decimal ActualUptoBudgetAmtDr { get; set; }
    public decimal ActualUptoBudgetAmtCr { get; set; }

    public decimal PropLastQtrBudgetAmtDr { get; set; }
    public decimal PropLastQtrBudgetAmtCr { get; set; }

    public decimal PropBudgetCapitalAmtDr { get; set; }
    public decimal PropBudgetCapitalAmtCr { get; set; }

    public decimal PropBudgetRevenueAmtDr { get; set; }
    public decimal PropBudgetRevenueAmtCr { get; set; }

    public int UserID { get; set; }
    public string IP { get; set; }

    public int DeptID { get; set; }
}