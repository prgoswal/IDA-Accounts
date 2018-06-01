using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GlobalSession
/// </summary>
public static class GlobalSession
{
    //static GlobalSession()
    //{
    //    OrgName = "";
    //    UserID = 0;
    //    YrCD = 0;
    //    OrgID = 0;
    //    BrID = 0;
    //    BrName = "";
    //    IsAdmin = 0;
    //    FinancialYr = "";
    //    IP = "";
    //    YrEndDate = "";
    //    YrStartDate = "";
    //    UserName = "";
    //    UserRemark = "";
    //    UserLoginID = "";
    //    SendStatus = false;
    //    StockMaintaineByMinorUnit = false;
    //    CompositionOpted = 0;
    //    ClientCodeOdp = 0;
    //    UnRegisterClient = 0;
    //}

    public static void LogOut()
    {
        OrgName = "";
        UserID = 0;
        YrCD = 0;
        OrgID = 0;
        BrID = 0;
        BrName = "";
        IsAdmin = 0;
        FinancialYr = "";
        IP = "";
        YrEndDate = "";
        YrStartDate = "";
        UserName = "";
        UserRemark = "";
        UserLoginID = "";
        SendStatus = false;
        StockMaintaineByMinorUnit = false;
        CompositionOpted = 0;
        ClientCodeOdp = 0;
        UnRegisterClient = 0;
        HOUser = 0;
        IsInValue = true;
    }

    public static bool IsInValue
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["IsExpire"]); }
        set { HttpContext.Current.Session["IsExpire"] = value; }
    }

    public static string OrgName
    {
        get { return HttpContext.Current.Session["OrgName"].ToString(); }
        set { HttpContext.Current.Session["OrgName"] = value; }
    }
    public static int UserID
    {
        get { return Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString()); }
        set { HttpContext.Current.Session["UserID"] = value; }
    }
    public static int YrCD
    {
        get { return Convert.ToInt32(HttpContext.Current.Session["YrCD"].ToString()); }
        set { HttpContext.Current.Session["YrCD"] = value; }
    }
    public static int OrgID
    {
        get { return Convert.ToInt32(HttpContext.Current.Session["OrgID"].ToString()); }
        set { HttpContext.Current.Session["OrgID"] = value; }
    }
    public static int BrID
    {
        get { return Convert.ToInt32(HttpContext.Current.Session["BrID"].ToString()); }
        set { HttpContext.Current.Session["BrID"] = value; }
    }
    public static string BrName
    {
        get { return HttpContext.Current.Session["BrName"].ToString(); }
        set { HttpContext.Current.Session["BrName"] = value; }
    }
    public static int IsAdmin
    {
        get { return Convert.ToInt32(HttpContext.Current.Session["IsAdmin"].ToString()); }
        set { HttpContext.Current.Session["IsAdmin"] = value; }
    }
    public static string FinancialYr
    {
        get { return HttpContext.Current.Session["FinancialYr"].ToString(); }
        set { HttpContext.Current.Session["FinancialYr"] = value; }
    }
    public static string IP
    {
        get { return HttpContext.Current.Session["IP"].ToString(); }
        set { HttpContext.Current.Session["IP"] = value; }
    }
    public static string YrEndDate
    {
        get { return HttpContext.Current.Session["YrEndDate"].ToString(); }
        set { HttpContext.Current.Session["YrEndDate"] = value; }
    }
    public static string YrStartDate
    {
        get { return HttpContext.Current.Session["YrStartDate"].ToString(); }
        set { HttpContext.Current.Session["YrStartDate"] = value; }
    }
    public static string UserName
    {
        get { return HttpContext.Current.Session["UserName"].ToString(); }
        set { HttpContext.Current.Session["UserName"] = value; }
    }
    public static string UserRemark
    {
        get { return HttpContext.Current.Session["UserRemark"].ToString(); }
        set { HttpContext.Current.Session["UserRemark"] = value; }
    }
    public static bool SendStatus
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["SendStatus"]); }
        set { HttpContext.Current.Session["SendStatus"] = value; }
    }
    public static string UserLoginID
    {
        get { return HttpContext.Current.Session["UserLoginID"].ToString(); }
        set { HttpContext.Current.Session["UserLoginID"] = value; }
    }
    public static string InvoiceRptName
    {
        get { return HttpContext.Current.Session["InvoiceRptName"].ToString(); }
        set { HttpContext.Current.Session["InvoiceRptName"] = value; }
    }
    public static bool StockMaintaineByMinorUnit
    {
        get { return Convert.ToBoolean(HttpContext.Current.Session["StockMaintaineByMinorUnit"]); }
        set { HttpContext.Current.Session["StockMaintaineByMinorUnit"] = value; }
    }
    public static int CompositionOpted
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["CompositionOpted"].ToString()); }
        set { HttpContext.Current.Session["CompositionOpted"] = value; }
    }
    public static int ClientCodeOdp
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["ClientCodeOdp"].ToString()); }
        set { HttpContext.Current.Session["ClientCodeOdp"] = value; }
    }
    public static int UnRegisterClient
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["UnRegisterClient"].ToString()); }
        set { HttpContext.Current.Session["UnRegisterClient"] = value; }
    }
    public static string GUID
    {
        get { return HttpContext.Current.Session["GUID"].ToString(); }
        set { HttpContext.Current.Session["GUID"] = value; }
    }
    public static int HOUser
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["HOUser"].ToString()); }
        set { HttpContext.Current.Session["HOUser"] = value; }
    }

    public static int CCCode
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["CCCode"].ToString()); }
        set { HttpContext.Current.Session["CCCode"] = value; }
    }

    public static int BudgetConcept
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["BudgetConcept"].ToString()); }
        set { HttpContext.Current.Session["BudgetConcept"] = value; }
    }


    public static int BudgetYrCD
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["BudgetYrCD"].ToString()); }
        set { HttpContext.Current.Session["BudgetYrCD"] = value; }
    }
    public static string BudgetAmount
    {
        get { return HttpContext.Current.Session["BudgetAmount"].ToString(); }
        set { HttpContext.Current.Session["BudgetAmount"] = value; }
    }

    public static int DepartmentID
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["DepartmentID"].ToString()); }
        set { HttpContext.Current.Session["DepartmentID"] = value; }
    }


    public static int SubDeptID
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["SubDeptID"].ToString()); }
        set { HttpContext.Current.Session["SubDeptID"] = value; }
    }

    public static int DesignationID
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["DesignationID"].ToString()); }
        set { HttpContext.Current.Session["DesignationID"] = value; }
    }

    public static int BankPayChqSeriesInd
    {
        get { return Convert.ToInt16(HttpContext.Current.Session["BankPayChqSeriesInd"].ToString()); }
        set { HttpContext.Current.Session["BankPayChqSeriesInd"] = value; }
    }
}

//public struct VchType
//{
//    public static int OPENINGBALANCE = 0;
//    public static int CASHRECEIPTVOUCHER = 1;
//    public static int CASHPAYMENTVOUCHER = 2;
//    public static int BANKRECEIPTVOUCHER = 3;
//    public static int BANKPAYMENTVOUCHER = 4;
//    public static int PURCHASEVOUCHER = 5;
//    public static int SALESVOUCHER = 6;
//    public static int PURCHASERETURNVOUCHER = 7;
//    public static int SALESRETURNVOUCHER = 8;
//    public static int DEBITNOTE = 9;
//    public static int CREDITNOTE = 10;
//    public static int JOURNALVOUCHER = 11;
//    public static int STOCKTRANSFER = 12;
//    public static int ADVANCEVOUCHER = 13;
//}