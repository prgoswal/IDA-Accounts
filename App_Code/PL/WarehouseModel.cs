using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WarehouseModel
/// </summary>
public class WarehouseModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int VchType { get; set; }
    public int GSTINID { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int StateID { get; set; }
    public int PinCode { get; set; }

    public int YrCD { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public int EntryType { get; set; }
}
