using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPDFBankbook : System.Web.UI.Page
{
    BankPaymentModel plbankpay;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txtFromDate.Text = GlobalSession.YrStartDate;
            txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();

            LoadBankAccount();
        }
    }

    void LoadBankAccount() // Bank List Bind
    {
        try
        {
            plbankpay = new BankPaymentModel()
            {
                Ind = 4,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = 3,//Convert.ToInt32(Session["VchType"]),
            };

            string uri = string.Format("Report/LoadBankAccount");
            DataTable dtBankAccount = CommonCls.ApiPostDataTable(uri, plbankpay);
            if (dtBankAccount.Rows.Count > 0)
            {
                ddlBankAccount.DataSource = dtBankAccount;
                ddlBankAccount.DataTextField = "AccName";
                ddlBankAccount.DataValueField = "AccCode";
                ddlBankAccount.DataBind();

                // if (dtBankAccount.Rows.Count > 1)
                //   ddlBankAccount.Items.Insert(0, new ListItem("-- Select Head --", "0"));
            }
        }
        catch { }
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

            string message = string.Empty;
            foreach (ListItem item in ddlBankAccount.Items)
            {
                if (item.Selected)
                {
                    //message += ddlBankAccount.SelectedValue + ",";
                    message += item.Value + ",";

                }
            }
            if (message == "")
            {
                lblErrorMsg.Text = "Please select at least one head name";
                return;
            }

            message = message.Substring(0, message.Length - 1);



            Session["Report"] = "Rpt_Bank_Cash_Book";
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("Acccode", message);
            HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("ReportHeading", "BANK BOOK ");

            HT.Add("VoucharDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            HT.Add("VoucharDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "RptBankBook";
            Response.Redirect("FrmReportViewer.aspx");

        }
    }


    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (cbSelectAll.Checked)
        {
            foreach (ListItem li in ddlBankAccount.Items)
            {
                li.Selected = true;
                lblChangeHeadName.InnerText = "Select All Head";
            }
        }
        else
        {
            foreach (ListItem li in ddlBankAccount.Items)
            {
                li.Selected = false;
                lblChangeHeadName.InnerText = "Please Select Head";
            }
        }
    }
}