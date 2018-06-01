using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MasterModel
/// </summary>
public class MasterModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int VchType { get; set; }
    public int GSTINID { get; set; }
    public int IsAdmin { get; set; }
    public int WarehouseID { get; set; }
    public int HSNSACCode { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}