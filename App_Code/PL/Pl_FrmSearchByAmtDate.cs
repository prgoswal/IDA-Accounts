using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pl_FrmSearchByAmtDate
/// </summary>
public class Pl_FrmSearchByAmtDate
{
	public Pl_FrmSearchByAmtDate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public string DocNo { set; get; }
    public string InvoiceDateFrom { set; get; }
    public string InvoiceDateTo { set; get; }
    public decimal MinAmount { set; get; }
    public decimal MaxAmount { set; get; }
}