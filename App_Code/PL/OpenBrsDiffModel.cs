using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OpenBrsDiffModel
/// </summary>
public class OpenBrsDiffModel
{
     public int Ind { get; set; }
     public int OrgID { get; set; }
     public int BrID { get; set; }
     public int VchType { get; set; }
     public int YrCD { get; set; } 
     public string DocDate{get;set;}
     public int DocNo { get; set; } 
     public string Acccode{get;set;}
     public string Narration{get;set;}
     public int ChequeNo { get; set; }
     public string ChequeDate { get; set; }
     public string BSCriteria { get; set; } 
     public string UTRNo{get;set;}
	 public decimal DrAmount{get;set;}		
	 public decimal CrAmount{get;set;}
     public string BSDate{get;set;}
     public decimal BSAmount{get;set;}
	 public string VoucharDateFrom{get;set;}
	 public string VoucharDateto{get;set;}
	 public string OpeningDate{get;set;}
	 public decimal OpeningBalance{get;set;}
     public string ClosingDate{get;set;}
	 public decimal ClosingBalance{get;set;}
	 public int BSInd { get; set; } 
}