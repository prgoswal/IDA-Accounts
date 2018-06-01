using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseModel
/// </summary>
public class BudgetSectionModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public string SectionName { get; set; }
    public string SectionNameHindi { get; set; }
    public int ParentSectionID { get; set; }
    public int SectionID { get; set; }

}