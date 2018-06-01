using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class UserModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public string AcessName { get; set; }
    public string ContactNo { get; set; }
    public string ContactAddr { get; set; }
    public string Designation { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string AdminLevel { get; set; }
    public string Remark { get; set; }
    public string LoginID { get; set; }
    public string LoginPass { get; set; }
    public int UserID { get; set; }
    public int ActiveInd { get; set; }
    public string ChangePass { get; set; }

    public int DepartmentID { get; set; }
    public int SubDeptID { get; set; }
    public string GUID { get; set; }

    public int MenuID { get; set; }
    public int DesignationID { get; set; }
}