using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PL_MultiInvoice
/// </summary>
public class PL_MultiInvoice
{
	public PL_MultiInvoice()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public string InvoiceNo { set; get; }
    public string InvoiceDateFrom { set; get; }
    public string InvoiceDateTo { set; get; }
    public string InvoiceDate { set; get; }


}