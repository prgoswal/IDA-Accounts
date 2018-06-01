
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BudgetAmount
/// </summary>
public class OpeningBlcTransferModel
{
    public int Ind { get; set; }
    public int YrCode { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int VChType { get; set; }
    public string YrStartDate { get; set; }
    public string VoucharDate { get; set; }
    public int VoucharNo { get; set; }
    public string Narration { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
    public string EntryDate { get; set; }
    public decimal ClosingStock { get; set; }

    //public DataTable dtOpeningBlc { get; set; }
    //public DataTable dtItemDetail { get; set; }

    public string dtOpeningBlc { get; set; }
    public string dtItemDetail { get; set; }

    public string VoucharDateFrom { get; set; }

    public string VoucharDateto { get; set; }
    public int Reportorder { get; set; }
    public string YearFromTo { get; set; }
    public string YrEndDate { get; set; }
    public int ActiveID { get; set; }
    public int UserID { get; set; }
    public string IPAddress { get; set; }
}