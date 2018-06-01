using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for TransporterDetailModal
/// </summary>
public class TransporterDetailModal
{


    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int yrcd { get; set; }

    public string TransportationName { get; set; }
    public string TransportorName { get; set; }
    public string OwnerName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int Pincode { get; set; }
    public string GSTIN { get; set; }
    public int RegistrationNo { get; set; }

    public int Road { get; set; }
    public int Rail { get; set; }
    public int Air { get; set; }
    public int Ship { get; set; }

    public int UserID { get; set; }
    public DateTime EntryDateTime { get; set; }
    public string IPAddress { get; set; }
}