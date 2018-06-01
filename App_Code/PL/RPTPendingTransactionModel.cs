using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RPTPendingTransactionModel
/// </summary>
public class RPTPendingTransactionModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
}