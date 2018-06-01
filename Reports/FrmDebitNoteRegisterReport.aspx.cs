using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDebitNoteRegisterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
        DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

        if (txtFromDate.Text.Trim() == "")
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

            Hashtable HT = new Hashtable();

            HT.Add("Ind", 9);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("ReportHeading", "Debit Note Register");
            HT.Add("DocTypeID", 9);
            HT.Add("VoucharDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            HT.Add("VoucharDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "DebitNoteRegister";
            if (ddl.SelectedValue == "0")
            {
                Session["Report"] = "RptDebitNoteRegister";//Report Name
            }
            else
            {
                Session["Report"] = "RptDebitCreditNoteDeatils";//Report Name
            }
            Response.Redirect("FrmReportViewer.aspx");
        }
    }
}