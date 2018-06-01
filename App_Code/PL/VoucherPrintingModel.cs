using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VoucherPrintingModel
/// </summary>
public class VoucherPrintingModel
{
    public int Ind { get; set; }
    public int DocNo { get; set; }
    public int VchType { get; set; }
    public string VchFromDate { get; set; }
    public string VchToDate { get; set; }
}