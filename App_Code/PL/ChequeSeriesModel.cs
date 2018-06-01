using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for ChequeSeriesModel
/// </summary>
public class ChequeSeriesModel
{
	 public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
   
    public int BankCode { get; set; }
    public int ChequeFrom { get; set; }
    public int ChequeTo{ get; set; }
    public int Diffrence { get; set; }

    public DataTable DtChkSeries { get; set; }
}