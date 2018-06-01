using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StockTransferModel
/// </summary>
public class StockTransferModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int YrCD { get; set; }
    public int VchType { get; set; }

    //public string VchDate { get; set; }
    //public string GSTIN { get; set; }
    //public decimal GrossAmount { get; set; }
    //public decimal TaxAmount { get; set; }
    //public decimal NetAmount { get; set; }
    //public int TransportID { get; set; }
    //public string VehicleNo { get; set; }
    //public string TransportDate { get; set; }
    //public string TransporterName { get; set; }
    public string TransferNo { get; set; }
    public string TransferDate { get; set; }

    public int TransferFromWarehouseID { get; set; }
    public int TransferToWarehouseID { get; set; }

    public int CCCodeTransferFrom { get; set; }
    public int CCCodeTransferTo    { get; set; }


    public string Narration { get; set; }
    public int ItemID { get; set; }
    public int WarehouseID { get; set; }
    public int UserID { get; set; }
    public string IP { get; set; }

    public DataTable DtItemDetail
    {
        get { return InitDtItemDetail; }
        set { InitDtItemDetail = value; }
    }

    private DataTable InitDtItemDetail = new DataTable();
}