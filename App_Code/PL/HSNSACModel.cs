using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HSNSACModel
/// </summary>
public class HSNSACModel
{
    public int Ind { get; set; }
    public int GoodsServiceInd { get; set; }
    public string HSNSACCode { get; set; }
    public string HSNSACDesc { get; set; }
    public decimal TaxRate { get; set; }
    public decimal IGRate { get; set; }
    public decimal SGRate { get; set; }
    public decimal CGRate { get; set; }
}