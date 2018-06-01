using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Reports_FrmVoucher_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click1(object sender, EventArgs e)
    {
        Hashtable HT = new Hashtable();

        if (txtVoucherNo.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please Enter Voucher No. ";
            txtVoucherNo.Focus();
            return;
        }
        else if (txtVoucherDate.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please Enter Voucher Date ";
            txtVoucherDate.Focus();
            return;
        }

        else
        {
            HT.Add("CompName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("Heading", "SALES INVOICE ");
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);

            HT.Add("DocTypeID", Convert.ToInt32(Session["VchType"]));
            HT.Add("Voucharno", txtVoucherNo.Text);
            HT.Add("VoucharDate", txtVoucherDate.Text.Substring(6, 4) + "/" + txtVoucherDate.Text.Substring(3, 2) + "/" + txtVoucherDate.Text.Substring(0, 2));
            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "Voucher";
            Session["Report"] = "RptVoucher";
            Response.Redirect("FrmReportViewer.aspx");
        }
    }
}