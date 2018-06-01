
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BudgetAmount
/// </summary>
public class BudgetAmountModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCode { get; set; }
    public int SectionCD { get; set; }
    public int SubSectionCD { get; set; }
    public int BudgetHeadCD { get; set; }
    public decimal PropBudgetAmtDr { get; set; }
    public decimal PropBudgetAmtCr { get; set; }
    public decimal SancBudgetAmtDr { get; set; }
    public decimal SancBudgetAmtCr { get; set; }
    public decimal RevBudgetAmtDr { get; set; }
    public decimal RevBudgetAmtCr { get; set; }
    public decimal Prop2BudgetAmtCr { get; set; }
    public decimal Prop2BudgetAmtDr { get; set; }
    public decimal Sanc2BudgetAmtCr { get; set; }
    public decimal Sanc2BudgetAmtDr { get; set; }
    public decimal Actual2budgetAmtDr { get; set; }
    public decimal Actual2budgetAmtcr { get; set; }
    public decimal Actual3budgetAmtDr { get; set; }
    public decimal Actual3budgetAmtCr { get; set; }
    public int UserID { get; set; }
    public string IP { get; set; }
}