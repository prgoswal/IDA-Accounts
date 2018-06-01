using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmGroupLedgerReport : System.Web.UI.Page
{
    JournalVoucherModel objJVModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = GlobalSession.YrStartDate;
            txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
            LoadAccountHead();
        }
    }

    private void LoadAccountHead()
    {
        try
        {
            objJVModel = new JournalVoucherModel()
            {
                Ind = 20,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = 0,
                //VchType = Convert.ToInt32(Session["VchType"]),
            };

            string uri = string.Format("JournalVoucher/LoadControlAccount");
            DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, objJVModel);
            if (dtAccGstin.Rows.Count > 0)
            {
                ViewState["dtAccGstin"] = dtAccGstin;
                DataView dvAccCode = new DataView(dtAccGstin);
                DataTable dtAccList = dvAccCode.ToTable(true, "recordid", "TBRptName");

                ddlGroupLedger.DataSource = dtAccList;
                ddlGroupLedger.DataTextField = "TBRptName";
                ddlGroupLedger.DataValueField = "recordid";
                ddlGroupLedger.DataBind();
            }
        }
        catch (Exception ex)
        {
            //  ShowMessage(ex.Message, false);
        }
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
        else if (ddlGroupLedger.SelectedValue == "")
        {
            lblErrorMsg.Text = "Please Select At Least One Group Name";
            ddlGroupLedger.Focus();
            return;
        }

        else
        {
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("Acccode", 0);
            HT.Add("VoucharDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            HT.Add("VoucharDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
            HT.Add("ReportHeading", ddlGroupLedger.SelectedItem.Text);
            HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("GroupCode", ddlGroupLedger.SelectedValue);

            Session["HT"] = HT;

            Session["format"] = "Pdf";
            Session["FileName"] = "AccountLedger";
            Session["Report"] = "RptGroupLedger";
            Response.Redirect("FrmReportViewer.aspx");

        }
    }


    protected void ddlCashAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}