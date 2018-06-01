using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSalesInvoiceReport : System.Web.UI.Page
{

    PL_MultiInvoice plobjMultiInvoice = new PL_MultiInvoice();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtInvoiceDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        }
    }

    void LoadItemMasterDDL()
    {
        try
        {
            plobjMultiInvoice.Ind = 2;
            plobjMultiInvoice.OrgID = GlobalSession.OrgID;
            plobjMultiInvoice.BrID = GlobalSession.BrID;
            plobjMultiInvoice.YrCD = GlobalSession.YrCD;

            string uri = string.Format("MultiInvoice/Method_MultiInvoice");
            DataTable dtMultiInvoice = CommonCls.ApiPostDataTable(uri, plobjMultiInvoice);
            if (dtMultiInvoice.Rows.Count > 0)
            {
                gvItem.DataSource = dtMultiInvoice;
                gvItem.DataBind();
                lblTotalAmt.Text = "Total Search No Of Recored : " + dtMultiInvoice.Rows.Count.ToString();
                btnShow.Visible = true;
                btnSearch.Enabled = false;
                pnlGrid.Visible = true;
                if (rbSearch.SelectedValue == "0")
                {
                    txtInvoiceDate.Enabled = false;
                    txtInvoiceNo.Enabled = false;
                }
                else
                {
                    txtFromDate.Enabled = false;
                    txtToDate.Enabled = false;
                }

            }
            else
            {
                lblErrorMsg.Text = "No Recored Found..!!! ";
            }
        }
        catch (Exception ex)
        {

        }
    }



    protected void rbSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Enabled = true;
        txtInvoiceDate.Enabled = true;
        txtInvoiceNo.Enabled = true;
        txtToDate.Enabled = true;
        btnSearch.Enabled = true;
        lblTotalAmt.Text = "";
        pnlGrid.Visible = false;
        lblErrorMsg.Text = "";
        gvItem.DataSource = null;
        gvItem.DataBind();
        txtFromDate.Text = "";
        txtInvoiceDate.Text = "";
        txtInvoiceNo.Text = "";
        txtToDate.Text = "";

        if (rbSearch.SelectedValue == "0")
        {
            pnlInvoiceNo.Visible = true;
            pnlForBetweenDate.Visible = false;
            btnSearch.Visible = true;
            btnClearSearch.Visible = true;
            btnShow.Visible = false;
            txtInvoiceDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();

        }
        else
        {
            btnSearch.Visible = true;
            btnClearSearch.Visible = true;
            btnShow.Visible = false;
            pnlInvoiceNo.Visible = false;
            pnlForBetweenDate.Visible = true;
            txtFromDate.Text = GlobalSession.YrStartDate;
            txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        }
    }

    protected void btnShow_Click1(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";

        string str = string.Empty;
        string strname = string.Empty;
        foreach (GridViewRow gvrow in gvItem.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("ckBox");
            if (chk != null & chk.Checked)
            {
                str = gvItem.DataKeys[gvrow.RowIndex].Value.ToString();
            }
        }

        if (str == "")
        {
            str = "0";
            lblErrorMsg.Text = "Please Select At Least One CheckBox ";
            return;
        }


        Hashtable HT = new Hashtable();

        if (rbSearch.SelectedValue == "0")
        {
            if (txtInvoiceNo.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Please Enter Invoice No. ";
                txtInvoiceNo.Focus();
                return;
            }
            else if (txtInvoiceDate.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Please Enter Invoice Date ";
                txtInvoiceDate.Focus();
                return;
            }

            HT.Add("invoiceno", txtInvoiceNo.Text);
            HT.Add("invoiceDate", txtInvoiceDate.Text.Substring(6, 4) + "/" + txtInvoiceDate.Text.Substring(3, 2) + "/" + txtInvoiceDate.Text.Substring(0, 2));
            HT.Add("vNO", str);

            HT.Add("invoiceDateFrom", "");
            HT.Add("invoiceDateto", "");
        }

        else
        {
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

            DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
            if (F_Date > T_Date)
            {
                lblErrorMsg.Text = "From Date Should Not Be Greater Than To Date";
                txtFromDate.Focus();
                return;
            }

            HT.Add("invoiceDateFrom", txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            HT.Add("invoiceDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
            HT.Add("vNO", str);
            HT.Add("invoiceno", 0);
            HT.Add("invoiceDate", "");
        }


        HT.Add("Ind", 1);
        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "SALES INVOICE ");
        HT.Add("cashsalesind", 1);

        HT.Add("Doctype", 6);
        Session["HT"] = HT;
        Session["format"] = "Pdf";
        Session["FileName"] = "SalesInvoice";
        //Session["Report"] = "RptSalesInvoice";
        if (GlobalSession.CompositionOpted == 1 || GlobalSession.UnRegisterClient == 1)
            Session["Report"] = GlobalSession.InvoiceRptName + "_BOS_UnReg";
        else
            Session["Report"] = GlobalSession.InvoiceRptName;

        Response.Redirect("FrmReportViewer.aspx");
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        if (rbSearch.SelectedValue == "0")
        {
            plobjMultiInvoice.InvoiceNo = txtInvoiceNo.Text;
            plobjMultiInvoice.InvoiceDateFrom = txtInvoiceDate.Text.Substring(6, 4) + "/" + txtInvoiceDate.Text.Substring(3, 2) + "/" + txtInvoiceDate.Text.Substring(0, 2);

            if (txtInvoiceNo.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Please Enter Invoice No. ";
                txtInvoiceNo.Focus();
                return;
            }
            else if (txtInvoiceDate.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Please Enter Invoice Date ";
                txtInvoiceDate.Focus();
                return;
            }
        }
        else
        {
            plobjMultiInvoice.InvoiceDateFrom = txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2);
            plobjMultiInvoice.InvoiceDateTo = txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2);

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
        }

        LoadItemMasterDDL();
    }
    //protected void ckBox_CheckedChanged(object sender, EventArgs e)
    //{
    //    var activeCheckBox = sender as CheckBox;
    //    if (activeCheckBox != null)
    //    {
    //        var isChecked = activeCheckBox.Checked;
    //        var tempCheckBox = new CheckBox();
    //        foreach (GridViewRow gvRow in gvItem.Rows)
    //        {
    //            tempCheckBox = gvRow.FindControl("ckBox") as CheckBox;
    //            if (tempCheckBox != null)
    //            {
    //                tempCheckBox.Checked = !isChecked;
    //            }
    //        }
    //        if (isChecked)
    //        {
    //            activeCheckBox.Checked = true;
    //            ViewState["gv_CheckItem"] = activeCheckBox.Text;

    //        }
    //    }
    //}
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        lblTotalAmt.Text = "";
        gvItem.DataSource = null;
        gvItem.DataBind();
        btnShow.Visible = false;
        btnSearch.Enabled = true;
        pnlGrid.Visible = false;

        if (rbSearch.SelectedValue == "0")
        {

            txtInvoiceDate.Enabled = true;
            txtInvoiceNo.Enabled = true;

            txtInvoiceNo.Text = "";
            txtInvoiceDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        }
        else
        {
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtFromDate.Text = GlobalSession.YrStartDate;
            txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        }
    }
}