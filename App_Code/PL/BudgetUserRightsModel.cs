using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BudgetRightsModel
/// </summary>
public class BudgetUserRightsModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    //public DataTable TblUserRights { get; set; }
    public int DepartmentID { get; set; }
    public int SubDeptID { get; set; }
    public int YrCD { get; set; }



    public string TblUserRights { get; set; }
    public int BrID { get; set; }
}