using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemOpenningStockModel
/// </summary>
public class ItemOpenningStockModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int ItemID { get; set; }
    public int User { get; set; }
    public string IP { get; set; }

    public DataTable DtItem { get; set; }
}