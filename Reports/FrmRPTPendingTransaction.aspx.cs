using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmRPTPendingTransaction : System.Web.UI.Page
{
    #region Declaration

    RPTPendingTransactionModel objRPTPTrans;

    DataTable VsdtPendingTransaction
    {
        get { return (DataTable)ViewState["dtPendingTransaction"]; }
        set { ViewState["dtPendingTransaction"] = value; }
    }

    #endregion

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
        try
        {
            DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

            if (txtFromDate.Text.Trim() == "")
            {
                ShowMessage("Enter From Date.",false);
                txtFromDate.Focus();
                return;
            }
            else if (txtToDate.Text.Trim() == "")
            {
                ShowMessage("Enter To Date.",false);
                txtToDate.Focus();
                return;
            }

            else if (F_Date > T_Date)
            {
                ShowMessage("From Date Should Not Be Greater Than To Date.", false);
                txtFromDate.Focus();
                return;
            }

            objRPTPTrans = new RPTPendingTransactionModel();
            objRPTPTrans.Ind = 1;
            objRPTPTrans.OrgID = GlobalSession.OrgID;
            objRPTPTrans.BrID = GlobalSession.BrID;
            objRPTPTrans.FromDate = CommonCls.ConvertToDate(txtFromDate.Text);
            objRPTPTrans.ToDate = CommonCls.ConvertToDate(txtToDate.Text);

            string uri = string.Format("RPTPendingTransaction/BindPendingTransaction");
            DataSet dsPendingVouchers = CommonCls.ApiPostDataSet(uri, objRPTPTrans);
            if (dsPendingVouchers.Tables.Count > 0)
            {
                VsdtPendingTransaction = dsPendingVouchers.Tables[0];

                if (VsdtPendingTransaction.Rows.Count > 0)
                {
                    grdPendingTransaction.DataSource = VsdtPendingTransaction;
                    grdPendingTransaction.DataBind();

                    if (grdPendingTransaction.Rows.Count > 0)
                    {
                        lblTotalPendingSince.Text = grdPendingTransaction.Rows.Count.ToString();
                        divShorting.Visible = divPendingSince.Visible = btnPrint.Visible = tblPendingTransHeader.Visible = divGrd.Visible = true;//pnlPendingTransaction.Visible = true;//tblPendingTransHeader.Visible = 
                    }
                    else
                    {
                        lblTotalPendingSince.Text = grdPendingTransaction.Rows.Count.ToString();
                        divShorting.Visible = divPendingSince.Visible = btnPrint.Visible = tblPendingTransHeader.Visible = divGrd.Visible = false;//pnlPendingTransaction.Visible = false;//tblPendingTransHeader.Visible = 
                    }

                    //TransactionAmountPointTwoZero();
                }
                else
                {
                    grdPendingTransaction.DataSource = new DataTable();
                    grdPendingTransaction.DataBind();
                    tblPendingTransHeader.Visible = true;
                    divShorting.Visible = divPendingSince.Visible = btnPrint.Visible = divGrd.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    //void TransactionAmountPointTwoZero()
    //{ 
    //    foreach (GridViewRow gvRow in grdPendingTransaction.Rows)
    //    {
    //        Label lblTransactionAmount = (Label)gvRow.FindControl("lblTransactionAmount");

    //        lblTransactionAmount.Text = CommonCls.ConverToCommas(lblTransactionAmount.Text);
    //    }
    //}

    protected void rdAll_CheckedChanged(object sender, EventArgs e)
    {
        if (VsdtPendingTransaction.Rows.Count > 0)
        {
            grdPendingTransaction.DataSource = VsdtPendingTransaction;
            grdPendingTransaction.DataBind();
        }
        lblTotalPendingSince.Text = grdPendingTransaction.Rows.Count.ToString();
    }

    protected void rdAudit_CheckedChanged(object sender, EventArgs e)
    {
        if (VsdtPendingTransaction.Rows.Count > 0)
        {
            DataView dvAudit = new DataView(VsdtPendingTransaction);
            dvAudit.RowFilter = "IsAudit=1 Or IsSendToAudit=1";
            if (dvAudit.ToTable().Rows.Count > 0)
            {
                grdPendingTransaction.DataSource = dvAudit.ToTable();
                grdPendingTransaction.DataBind();
            }
        }
        lblTotalPendingSince.Text = grdPendingTransaction.Rows.Count.ToString();
    }

    protected void rdCashier_CheckedChanged(object sender, EventArgs e)
    {
        if (VsdtPendingTransaction.Rows.Count > 0)
        {
            DataView dvCashier = new DataView(VsdtPendingTransaction);
            dvCashier.RowFilter = "IsFinal=0 And IsSendToAudit=1";
            if (dvCashier.ToTable().Rows.Count > 0)
            {
                grdPendingTransaction.DataSource = dvCashier.ToTable();
                grdPendingTransaction.DataBind();
            }
        }
        lblTotalPendingSince.Text = grdPendingTransaction.Rows.Count.ToString();
    }
    protected void rdAO_CheckedChanged(object sender, EventArgs e)
    {
        if (VsdtPendingTransaction.Rows.Count > 0)
        {
            DataView dvAO = new DataView(VsdtPendingTransaction);
            dvAO.RowFilter = "IsFinal=1";
            if (dvAO.ToTable().Rows.Count > 0)
            {
                grdPendingTransaction.DataSource = dvAO.ToTable();
                grdPendingTransaction.DataBind();
            }
        }
        lblTotalPendingSince.Text = grdPendingTransaction.Rows.Count.ToString();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = lblTotalPendingSince.Text = "";
        txtFromDate.Text = GlobalSession.YrStartDate;
        txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
        txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();

        VsdtPendingTransaction = null;
        grdPendingTransaction.DataSource = new DataTable();
        grdPendingTransaction.DataBind();

        divShorting.Visible = divPendingSince.Visible = btnPrint.Visible = tblPendingTransHeader.Visible = divGrd.Visible = false;
    }
}