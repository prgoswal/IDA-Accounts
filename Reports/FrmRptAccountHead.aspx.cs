using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmRptAccountHead : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        Hashtable HT = new Hashtable();
        HT.Add("Ind", 1);
        HT.Add("CompanyID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("YrCD", GlobalSession.YrCD);
        HT.Add("CompanyName", GlobalSession.OrgName);
        // HT.Add("BranchName", GlobalSession.BrName);
        // HT.Add("LedgerType", ddlLedgerType.SelectedValue);
        Session["HT"] = HT;

        Session["format"] = "Pdf";
        Session["FileName"] = "Account Head Report";
        Session["Report"] = "RptAccountHeadMaster";
        HT.Add("ReportHeading","Account Head Master Report");
        Response.Redirect("FrmReportViewer.aspx");
    }
}