using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_frmVoucherPrinting : System.Web.UI.Page
{
    VoucherPrintingModel objVPModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDocumentType();
            ddlVoucherType.Focus();
        }

        lblMsg.Text = lblMsg.CssClass = "";

        //if (rbVoucherNoWise.Checked == true)
        //{
        //    tdlblVoucherNo.Visible = tdddlVoucherNo.Visible = true;
        //    tdlblFromDate.Visible = tdtxtFromDate.Visible = tdlblToDate.Visible = tdtxtToDate.Visible = false;
        //    txtFromDate.Text = txtToDate.Text = "";
        //}
        //else
        //{
        //    tdlblVoucherNo.Visible = tdddlVoucherNo.Visible = false;
        //    tdlblFromDate.Visible = tdtxtFromDate.Visible = tdlblToDate.Visible = tdtxtToDate.Visible = true;
        //    txtVoucherNo.Text = "";
        //}
    }
    void BindDocumentType()
    {
        try
        {
            objVPModel = new VoucherPrintingModel();
            objVPModel.Ind = 1;

            string uri = string.Format("VoucherPrinting/BindDocumentType");
            DataTable dtBindDocType = CommonCls.ApiPostDataTable(uri, objVPModel);
            if (dtBindDocType.Rows.Count > 0)
            {
                ddlVoucherType.DataSource = dtBindDocType;
                ddlVoucherType.DataTextField = "DocTypeDesc";
                ddlVoucherType.DataValueField = "DocTypeID";
                ddlVoucherType.DataBind();
                ddlVoucherType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";

        if (Convert.ToInt32(ddlVoucherType.SelectedValue) == 0)
        {
            ShowMessage("Select Voucher Type.", false);
            ddlVoucherType.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtVoucherNo.Text))
        {
            ShowMessage("Enter Voucher No.", false);
            txtVoucherNo.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtVoucherDate.Text))
        {
            ShowMessage("Enter Voucher Date.", false);
            txtVoucherDate.Focus();
            return;
        }

        if (CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 1 || CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 2
            || CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 3 || CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 4)
        {
            Session["Report"] = "RptVoucher";//Report Name
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("CompName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("Heading", ddlVoucherType.SelectedItem.Text);
            HT.Add("DocTypeID", Convert.ToInt32(ddlVoucherType.SelectedValue));
            HT.Add("Voucharno", Convert.ToInt32(txtVoucherNo.Text));
            HT.Add("VoucharDate", txtVoucherDate.Text.Substring(6, 4) + "/" + txtVoucherDate.Text.Substring(3, 2) + "/" + txtVoucherDate.Text.Substring(0, 2));
            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "CashBankVoucher";
            Response.Redirect("FrmReportViewer.aspx");
        }
        else if (CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 5 && GlobalSession.UnRegisterClient == 0)
        {
            Session["Report"] = "RptPurchaseVoucher";
            
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);

            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("CompName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("Heading", "PURCHASE VOUCHER");

            HT.Add("Doctype", 5);
            HT.Add("invoiceno", CommonCls.ConvertIntZero(txtVoucherNo.Text));
            HT.Add("invoiceDate", CommonCls.ConvertToDate(txtVoucherDate.Text));
            HT.Add("invoiceDateFrom", "");
            HT.Add("invoiceDateto", "");
            HT.Add("cashsalesind", 1);
            HT.Add("vNO", Convert.ToInt32(txtVoucherNo.Text));

            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "PurchaseVoucher";
            Response.Redirect("FrmReportViewer.aspx");
        }
        else if (CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 5 && GlobalSession.UnRegisterClient == 1)
        {
            Session["Report"] = "RptPurchaseVoucher" + "_BOS_UnReg";
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);

            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("CompName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("Heading", "PURCHASE VOUCHER");

            HT.Add("Doctype", 5);
            HT.Add("invoiceno", Convert.ToInt32(txtVoucherNo.Text));
            HT.Add("invoiceDate", CommonCls.ConvertDateDB(txtVoucherDate.Text));
            HT.Add("invoiceDateFrom", "");
            HT.Add("invoiceDateto", "");
            HT.Add("cashsalesind", 1);
            
            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "PurchaseVoucher";
            Response.Redirect("FrmReportViewer.aspx");
        }
        else if (CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 7)
        {
            //Session["Report"] = "RptPurchaseVoucher";
            //Hashtable HT = new Hashtable();
            //HT.Add("Ind", 1);

            //HT.Add("OrgID", GlobalSession.OrgID);
            //HT.Add("BrID", GlobalSession.BrID);
            //HT.Add("yrcode", GlobalSession.YrCD);
            //HT.Add("CompName", GlobalSession.OrgName);
            //HT.Add("BranchName", GlobalSession.BrName);
            //HT.Add("Heading", "PURCHASE RETURN VOUCHER");

            //HT.Add("Doctype", 5);
            //HT.Add("invoiceno", 0);
            //HT.Add("invoiceDate", "");
            //HT.Add("invoiceDateFrom", "");
            //HT.Add("invoiceDateto", "");
            //HT.Add("cashsalesind", 1);
            //HT.Add("vNO", Convert.ToInt32(txtVoucherNo.Text));

            //Session["HT"] = HT;
            //Session["format"] = "Pdf";
            //Session["FileName"] = "PurchaseReturnVoucher";
            //Response.Redirect("FrmReportViewer.aspx");
        }
        else if (CommonCls.ConvertIntZero(ddlVoucherType.SelectedValue) == 11)
        {
            Session["Report"] = "RptJournalVoucher";//Report Name
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("CompName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("Heading", ddlVoucherType.SelectedItem.Text);
            HT.Add("DocTypeID", Convert.ToInt32(ddlVoucherType.SelectedValue));
            HT.Add("Voucharno", Convert.ToInt32(txtVoucherNo.Text));
            HT.Add("VoucharDate", txtVoucherDate.Text.Substring(6, 4) + "/" + txtVoucherDate.Text.Substring(3, 2) + "/" + txtVoucherDate.Text.Substring(0, 2));
            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "JournalVoucher";
            Response.Redirect("FrmReportViewer.aspx");
        }

        //if (rbVoucherNoWise.Checked == true)
        //{
        //    if (string.IsNullOrEmpty(txtVoucherNo.Text))
        //    {
        //        ShowMessage("Enter Voucher No.", false);
        //        txtVoucherNo.Focus();
        //        return;
        //    }
        //    if (ddlVoucherType.SelectedValue == "1")
        //    {
        //        Hashtable HT = new Hashtable();
        //        HT.Add("Ind", 1);
        //        HT.Add("CompName", GlobalSession.OrgName);
        //        HT.Add("BranchName", GlobalSession.BrName);
        //        HT.Add("OrgID", GlobalSession.OrgID);
        //        HT.Add("BrID", GlobalSession.BrID);
        //        HT.Add("yrcode", GlobalSession.YrCD);
        //        HT.Add("Heading", ddlVoucherType.SelectedItem.Text);
        //        HT.Add("DocTypeID", Convert.ToInt16(ddlVoucherType.SelectedValue));
        //        HT.Add("Voucharno", CommonCls.ConvertIntZero(txtVoucherNo.Text));

        //        Session["HT"] = HT;

        //        Session["format"] = "Pdf";
        //        //Session["FileName"] = "AccountLedger";
        //        //Session["Report"] = "RptAccountLedger";
        //        Response.Redirect("FrmReportViewer.aspx");
                
        //        //VouchersReport.ReportName = "RptVoucher";
        //        //VouchersReport.FileName = "Voucher";
        //        //VouchersReport.ReportHeading = "Cash Receipt";
        //        //VouchersReport.HashTable = HT;
        //        //VouchersReport.ShowReport();
        //    }
        //}
        //else
        //{
        //    if (string.IsNullOrEmpty(txtFromDate.Text))
        //    {
        //        ShowMessage("Enter From Date.", false);
        //        txtFromDate.Focus();
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtToDate.Text))
        //    {
        //        ShowMessage("Enter To Date.", false);
        //        txtToDate.Focus();
        //        return;
        //    }
        //    DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
        //    DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

        //    if (F_Date > T_Date)
        //    {
        //        ShowMessage("From Date Should Not Be Greater Than To Date.", false);
        //        txtFromDate.Focus();
        //        return;
        //    }

        //    Session["Report"] = "RptJVRegister";//Report Name
        //    Hashtable HT = new Hashtable();
        //    HT.Add("Ind", 1);
        //    HT.Add("OrgID", GlobalSession.OrgID);
        //    HT.Add("BrID", GlobalSession.BrID);
        //    HT.Add("yrcode", GlobalSession.YrCD);
        //    HT.Add("CompanyName", GlobalSession.OrgName);
        //    HT.Add("BranchName", GlobalSession.BrName);
        //    HT.Add("ReportHeading", "JOURNAL VOUCHER REGISTER ");


        //    HT.Add("DocTypeID", Convert.ToInt32(ddlVoucherType.SelectedValue));

        //    HT.Add("VoucharDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
        //    HT.Add("VoucharDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
        //    Session["HT"] = HT;
        //    Session["format"] = "Pdf";
        //    Session["FileName"] = "JVRegister";
        //    Response.Redirect("FrmReportViewer.aspx");
        //}
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}