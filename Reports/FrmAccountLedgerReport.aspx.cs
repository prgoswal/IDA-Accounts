using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAccountLedgerReport : System.Web.UI.Page
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
                Ind = 5,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = 0,
                //VchType = Convert.ToInt32(Session["VchType"]),
            };

            string uri = string.Format("JournalVoucher/AccountHead");
            DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, objJVModel);
            if (dtAccGstin.Rows.Count > 0)
            {
                ViewState["dtAccGstin"] = dtAccGstin;
                DataView dvAccCode = new DataView(dtAccGstin);
                DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

                ddlAccountHead.DataSource = dtAccList;
                ddlAccountHead.DataTextField = "AccName";
                ddlAccountHead.DataValueField = "AccCode";
                ddlAccountHead.DataBind();
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
        else if (ddlAccountHead.SelectedValue == "")
        {
            lblErrorMsg.Text = "Please Select At Least One Head Name";
            ddlAccountHead.Focus();
            return;
        }
        else
        {
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("Acccode", ddlAccountHead.SelectedValue);
            HT.Add("VoucharDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            HT.Add("VoucharDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
            HT.Add("ReportHeading", ddlAccountHead.SelectedItem.Text + " Ledger ");
            HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("GroupCode", 0);

            Session["HT"] = HT;

            Session["format"] = "Pdf";
            Session["FileName"] = "AccountLedger";
            Session["Report"] = "RptAccountLedger";
            Response.Redirect("FrmReportViewer.aspx");

        }
    }


    protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}