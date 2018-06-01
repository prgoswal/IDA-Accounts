using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OfflineUtilityModel
/// </summary>
public class OfflineUtilityModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public string CompanyName { get; set; }
    public string BranchName { get; set; }
    public int User { get; set; }

    public int SeriesList { get; set; }
    private DataTable DtBinding_SeriesList = new DataTable();

    public DataTable Binding_SeriesList 
    {
        get { return DtBinding_SeriesList; }
        set { DtBinding_SeriesList = value; }
    }

    
}
public class Data
{
    public string DetailData { get; set; }
}