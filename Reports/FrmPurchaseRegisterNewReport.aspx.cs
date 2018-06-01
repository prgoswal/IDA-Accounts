using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmPurchaseRegisterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = GlobalSession.YrStartDate;
            txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
        DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));


        bool ValidFromDate = CommonCls.CheckFinancialYrDate(txtFromDate.Text, GlobalSession.YrStartDate, GlobalSession.YrEndDate);
        bool ValidToDate = CommonCls.CheckFinancialYrDate(txtToDate.Text, GlobalSession.YrStartDate, GlobalSession.YrEndDate);



        if (!ValidFromDate) // For FromDate  Between Financial Year.
        {
            lblErrorMsg.Text = "From Date Should Be Within Financial Year.";
            txtFromDate.Focus();
            return;
        }
        else if (!ValidToDate) // For FromDate  Between Financial Year.
        {
            lblErrorMsg.Text = "To Date Should Be Within Financial Year.";
            txtToDate.Focus();
            return;
        }
        else if (txtFromDate.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please Enter From Date ";
            txtFromDate.Focus();
            return;
        }
        else if (txtToDate.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please Enter To Date ";
            txtToDate.Focus();
            return;
        }

        else if (F_Date > T_Date)
        {
            lblErrorMsg.Text = "From Date Should Not Be Greater Than To Date";
            txtFromDate.Focus();
            return;
        }
        else
        {
            if (GlobalSession.UnRegisterClient == 1)
                Session["Report"] = "RptPurchaseRegister" + "_BOS_UnReg";
            else
                Session["Report"] = "RptPurchaseRegisterNew";

            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("ReportHeading", "PURCHASE REGISTER ");

            HT.Add("invoiceno", 0);
            HT.Add("invoiceDate", "");
            HT.Add("Doctype", 5);

            HT.Add("invoiceDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            HT.Add("invoiceDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "RptPurchaseRegisterNew";
            Response.Redirect("FrmReportViewer.aspx");

        }
    }
}