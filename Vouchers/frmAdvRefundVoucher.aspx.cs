using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmAdvRefundVoucher : System.Web.UI.Page
{
    #region Declarations

    AdvanceRefundModel objARModel;

    public DataTable VsDtCashBankAccount
    {
        get { return (DataTable)ViewState["dtCashBankAccount"]; }
        set { ViewState["dtCashBankAccount"] = value; }
    }

    public DataTable VsDtAccountHead
    {
        get { return (DataTable)ViewState["dtAccountHead"]; }
        set { ViewState["dtAccountHead"] = value; }

    }

    DataTable VsdtNarration
    {
        get { return (DataTable)ViewState["dtNarration"]; }
        set { ViewState["dtNarration"] = value; }
    }

    DataTable VsdtAdvanceRefund
    {
        get { return (DataTable)ViewState["dtAdvanceRefund"]; }
        set { ViewState["dtAdvanceRefund"] = value; }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtVoucherNo.Focus();
            ViewState["VchType"] = 15;
            BindAllDDl();
            if (ddlPayMode.SelectedValue == "Cheque")
            {
                lblPayModeNo.Text = "Cheque No.";
                lblPayModeDate.Text = "Cheque Date";
                txtReceivedNo.MaxLength = 8;
                txtReceivedNo.Attributes.Add("placeholder", "Cheque No.");
                txtReceivedNo.CssClass = "numberonly";
            }
            txtVoucherDate.Text = CommonCls.ConvertDateDB(DateTime.Now);
        }
        lblMsg.Text = lblMsg.CssClass = "";
    }

    void BindAllDDl()
    {
        try
        {
            objARModel = new AdvanceRefundModel();
            objARModel.Ind = 11;
            objARModel.OrgID = GlobalSession.OrgID;
            objARModel.BrID = GlobalSession.BrID;
            objARModel.YrCD = GlobalSession.YrCD;
            objARModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("AdvanceRefund/BindAllDDl");
            DataSet dsBindAllDDl = CommonCls.ApiPostDataSet(uri, objARModel);
            if (dsBindAllDDl.Tables.Count > 0)
            {
                //---------------------------------------------tableName--------------------------------------------------------------
                VsDtCashBankAccount = dsBindAllDDl.Tables[0];
                VsDtAccountHead = dsBindAllDDl.Tables[1];
                VsdtNarration = dsBindAllDDl.Tables[2];
                DataTable dtLastVoucherNo = dsBindAllDDl.Tables[3];

                //--------------------------------------------BindCashBank Account--------------------------------------------------
                if (VsDtCashBankAccount.Rows.Count > 0)
                {
                    ddlCashBankAccount.DataSource = VsDtCashBankAccount;
                    ddlCashBankAccount.DataTextField = "AccName";
                    ddlCashBankAccount.DataValueField = "AccCode";
                    ddlCashBankAccount.DataBind();
                    ddlCashBankAccount.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                }

                //--------------------------------------------Bind Account Head-----------------------------------------------------
                if (VsDtAccountHead.Rows.Count > 0)
                {
                    ddlAccountHead.DataSource = VsDtAccountHead;
                    ddlAccountHead.DataTextField = "AccName";
                    ddlAccountHead.DataValueField = "AccCode";
                    ddlAccountHead.DataBind();
                    ddlAccountHead.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                }

                //-----------------------------------------------Bind Narration------------------------------------------------------
                if (VsdtNarration.Rows.Count > 0)
                {
                    cbNarration.DataSource = VsdtNarration;
                    cbNarration.DataTextField = "NarrationDesc";
                    cbNarration.DataBind();
                    cbNarration.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                }

                //---------------------------------------------Last Voucher No And Date-----------------------------------------------
                if (dtLastVoucherNo.Rows.Count > 0)
                {
                    if (dtLastVoucherNo.Rows[0]["LastNo"].ToString() != "0")
                    {
                        lblVoucherAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucherNo.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtLastVoucherNo.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetpayMode();
        ddlPayMode.Focus();
    }

    private void SetpayMode()
    {
        if (ddlPayMode.SelectedValue == "Cheque")
        {
            lblPayModeNo.Text = "Cheque No.";
            lblPayModeDate.Text = "Cheque Date";
            txtReceivedNo.MaxLength = 8;
            txtReceivedNo.Attributes.Add("placeholder", "Cheque No.");
            txtReceivedNo.CssClass = "numberonly";
        }
        else
        {
            lblPayModeNo.Text = "UTR No.";
            lblPayModeDate.Text = "UTR Date";
            txtReceivedNo.MaxLength = 16;
            txtReceivedNo.Attributes.Add("placeholder", "UTR No.");
            txtReceivedNo.CssClass = "";
        }
        txtReceivedNo.Text = txtReceivedDate.Text = "";
    }

    protected void btnSearchVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            objARModel = new AdvanceRefundModel();
            objARModel.Ind = 1;
            objARModel.OrgID = GlobalSession.OrgID;
            objARModel.BrID = GlobalSession.BrID;
            objARModel.YrCD = GlobalSession.YrCD;
            objARModel.DocNo = CommonCls.ConvertIntZero(txtVoucherNo.Text);

            string uri = string.Format("AdvanceRefund/LoadAdvanceRefundInfo");
            DataSet dsLARInfo = CommonCls.ApiPostDataSet(uri, objARModel);
            if (dsLARInfo.Tables.Count > 0)
            {
                DataTable dtBasic = dsLARInfo.Tables[0];
                DataTable dtARInfo = dsLARInfo.Tables[1];

                if (dtBasic.Rows.Count > 0)
                {
                    //txtVoucherDate.Text = CommonCls.ConvertDateDB(dtBasic.Rows[0]["VoucharDate"]);
                    ddlCashBankAccount.SelectedValue = dtBasic.Rows[0]["CashBankCode"].ToString();
                    ddlAccountHead.SelectedValue = dtBasic.Rows[0]["PartyCode"].ToString();
                    ddlCashBankAccount_SelectedIndexChanged(sender, e);
                    ddlAccountHead_SelectedIndexChanged(sender, e);
                    ddlPayMode_SelectedIndexChanged(sender, e);
                }
                if (dtARInfo.Rows.Count > 0)
                {
                    grdAdvanceRefund.DataSource = dtARInfo;
                    grdAdvanceRefund.DataBind();
                }
                if (dtBasic.Rows.Count > 0 && dtARInfo.Rows.Count > 0)
                {
                    txtVoucherNo.Enabled = btnSearchVoucher.Enabled = false;
                    divGRDAdvanceRefund.Visible = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    ShowMessage("Invalid Voucher No.", false);
                    txtVoucherNo.Focus();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlCashBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow[] dr = VsDtCashBankAccount.Select("AccCode='" + ddlCashBankAccount.SelectedValue + "'");
        int ind = Convert.ToInt32(dr[0]["Ind"]);
        if (ind == 1)
        {
            divPayMode.Visible = false;
        }
        else if (ind == 2)
        {
            divPayMode.Visible = true;
        }
        ddlCashBankAccount.Focus();
    }

    protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow[] dr = VsDtAccountHead.Select("AccCode='" + ddlAccountHead.SelectedValue + "'");
        ddlGSTINNo.Items.Clear();
        foreach (DataRow row in dr)
        {
            if (!string.IsNullOrEmpty(row["GSTIN"].ToString()))
            {
                ddlGSTINNo.Items.Add(row["GSTIN"].ToString());
            }
        }
        if (dr.Length > 1)
        {
            ddlGSTINNo.Items.Insert(0, "-Select-");
        }
    }

    protected void cbPartyAdvance_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbPartyRefund = (CheckBox)sender;
        GridViewRow GvRow = (GridViewRow)cbPartyRefund.NamingContainer;
        Label lblTaxRate = (Label)GvRow.FindControl("lblTaxRate");
        TextBox txtRefundAmount = (TextBox)GvRow.FindControl("txtRefundAmount");

        if (txtRefundAmount.Enabled == true)
            txtRefundAmount.Enabled = false;
        else
            txtRefundAmount.Enabled = true;
    }

    decimal refundAmt = 0;
    protected void txtRefundAmount_TextChanged(object sender, EventArgs e)
    {
        TextBox txtPartyRefund = (TextBox)sender;
        GridViewRow GvRow = (GridViewRow)txtPartyRefund.NamingContainer;
        Label lblTaxRate = (Label)GvRow.FindControl("lblTaxRate");
        Label lblReceivedAmount = (Label)GvRow.FindControl("lblReceivedAmount");
        TextBox txtRefundAmount = (TextBox)GvRow.FindControl("txtRefundAmount");
        if (CommonCls.ConvertDecimalZero(txtRefundAmount.Text) > CommonCls.ConvertDecimalZero(lblReceivedAmount.Text))
        {
            ShowMessage("Refund Amount Can't Be Greater Than Received Amount = '" + lblReceivedAmount.Text + "'", false);
            txtRefundAmount.Focus();
            return;
        }
        else
            refundAmt = CommonCls.ConvertDecimalZero(lblReceivedAmount.Text) - CommonCls.ConvertDecimalZero(txtRefundAmount.Text);

        txtTotalRefundAmount.Text = "0";
        foreach (GridViewRow grdRow in grdAdvanceRefund.Rows)
        {
            CheckBox cbPartyAdvance = (CheckBox)grdRow.FindControl("cbPartyAdvance");
            if (cbPartyAdvance.Checked == true)
            {
                TextBox txtFinalRefundAmt = (TextBox)grdRow.FindControl("txtRefundAmount");
                txtTotalRefundAmount.Text = Convert.ToString(CommonCls.ConvertDecimalZero(txtTotalRefundAmount.Text) + CommonCls.ConvertDecimalZero(txtFinalRefundAmt.Text));
            }
        }
        txtRefundAmount.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        lblMsg.Text = lblMsg.CssClass = "";
        CreateAR();
        if (!BTNSaveValidation())
        {
            return;
        }
        objARModel = new AdvanceRefundModel();
        objARModel.Ind = 2;
        objARModel.OrgID = GlobalSession.OrgID;
        objARModel.BrID = GlobalSession.BrID;
        objARModel.YrCD = GlobalSession.YrCD;
        objARModel.VchType = CommonCls.ConvertIntZero(ViewState["VchType"]);

        objARModel.VchDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
        objARModel.CashBankCode = Convert.ToInt32(ddlCashBankAccount.SelectedValue);
        objARModel.AccountCode = CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue.ToString());
        objARModel.GSTIN = ddlGSTINNo.SelectedItem == null ? "" : ddlGSTINNo.SelectedItem.Text;
        objARModel.VchNarration = cbNarration.Text;
        objARModel.NetAmount = CommonCls.ConvertDecimalZero(txtTotalRefundAmount.Text);

        if (ddlPayMode.SelectedValue == "Cheque")
        {
            objARModel.ChequeNo = CommonCls.ConvertIntZero(txtReceivedNo.Text);
            objARModel.ChequeDate = CommonCls.ConvertToDate(txtReceivedDate.Text);
        }
        else
        {
            objARModel.UTRNo = txtReceivedNo.Text;
            objARModel.UTRDate = CommonCls.ConvertToDate(txtReceivedDate.Text);
        }

        objARModel.DtAR = VsdtAdvanceRefund;
        objARModel.UserID = GlobalSession.UserID;
        objARModel.IP = GlobalSession.IP;

        string uri = string.Format("AdvanceRefund/SaveAdvanceRefund");
        DataTable dtSaveAR = CommonCls.ApiPostDataTable(uri, objARModel);
        if (dtSaveAR.Rows.Count > 0)
        {
            if (dtSaveAR.Rows[0]["ReturnInd"].ToString() == "0")
            {
                ShowMessage("Record Not Save, Please Try Again.", false);
                return;
            }
            else if (dtSaveAR.Rows[0]["ReturnInd"].ToString() == "1")
            {
                ClearAll();
                lblVoucherAndDate.Text = "Last Voucher No. & Date : " + dtSaveAR.Rows[0]["DocMaxNo"].ToString() + " - " + Convert.ToDateTime(dtSaveAR.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                ShowMessage("Record Save Successfully.", true);
                return;
            }
            else if (dtSaveAR.Rows[0]["ReturnInd"].ToString() == "2")
            {
                ShowMessage("Duplicate Record.", false);
                return;
            }
        }
        else
        {
            ShowMessage("Record Not Save, Please Try Again.", false);
            return;
        }
    }

    bool BTNSaveValidation()
    {
        if (string.IsNullOrEmpty(txtVoucherDate.Text))
        {
            ShowMessage("Enter Voucher Date.", false);
            txtVoucherDate.Focus();
            return false;
        }
        if (CommonCls.ConvertIntZero(ddlCashBankAccount.SelectedValue) == 0)
        {
            ShowMessage("Select Cash / Bank Account.", false);
            ddlCashBankAccount.Focus();
            return false;
        }
        if (CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue) == 0)
        {
            ShowMessage("Select Account Head.", false);
            ddlAccountHead.Focus();
            return false;
        }
        if (ddlGSTINNo.SelectedItem != null)
        {
            if (ddlGSTINNo.SelectedValue == "" || ddlGSTINNo.SelectedValue == "-Select-")
            {
                ShowMessage("Select GSTIn No.", false);
                ddlGSTINNo.Focus();
                return false;
            }
        }
        if (VsdtAdvanceRefund == null || VsdtAdvanceRefund.Rows.Count <= 0)
        {
            ShowMessage("Refund Advance Received Amount Can't Be Null.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtTotalRefundAmount.Text) || Convert.ToDecimal(txtTotalRefundAmount.Text) < 0)
        {
            ShowMessage("Refundable Amount Can Not Be Negative Please Check Entry.", false);
            return false;
        }
        return true;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ClearAll()
    {
        txtVoucherNo.Enabled = btnSearchVoucher.Enabled = true;
        grdAdvanceRefund.DataSource = new DataTable();
        grdAdvanceRefund.DataBind();
        divGRDAdvanceRefund.Visible = btnSave.Enabled = false;
        divPayMode.Visible = true;
        txtVoucherNo.Text = txtVoucherDate.Text = txtReceivedNo.Text = txtReceivedDate.Text =
            txtTotalRefundAmount.Text = lblMsg.Text = lblMsg.CssClass = "";
        ddlCashBankAccount.ClearSelection();
        ddlAccountHead.ClearSelection();
        ddlGSTINNo.ClearSelection();
        ddlGSTINNo.DataSource = new DataTable();
        ddlGSTINNo.DataBind();
        ddlPayMode.ClearSelection();
        cbNarration.ClearSelection();
        txtVoucherNo.Focus();
        VsdtAdvanceRefund = null;
    }

    void CreateAR()
    {
        foreach (GridViewRow grdRow in grdAdvanceRefund.Rows)
        {
            CheckBox cbPartyAdvance = (CheckBox)grdRow.FindControl("cbPartyAdvance");
            if (cbPartyAdvance.Checked == true)
            {
                DataTable dtGRDAdvanceRefund = new DataTable();
                if (VsdtAdvanceRefund == null)
                    dtGRDAdvanceRefund = DTAdvanceRefundSchema();
                else
                    dtGRDAdvanceRefund = VsdtAdvanceRefund;

                Label lblVoucharNo = (Label)grdRow.FindControl("lblVoucharNo");
                Label lblVoucherDate = (Label)grdRow.FindControl("lblVoucherDate");
                Label lblTaxRate = (Label)grdRow.FindControl("lblTaxRate");
                Label lblReceivedAmount = (Label)grdRow.FindControl("lblReceivedAmount");
                TextBox txtFinalRefundAmt = (TextBox)grdRow.FindControl("txtRefundAmount");

                DataRow DrGRDAdvanceRefund = dtGRDAdvanceRefund.NewRow();
                DrGRDAdvanceRefund["CompanyID"] = GlobalSession.OrgID;
                DrGRDAdvanceRefund["BranchID"] = GlobalSession.BrID;
                DrGRDAdvanceRefund["ItemName"] = "";
                DrGRDAdvanceRefund["ItemUnit"] = "";
                DrGRDAdvanceRefund["ItemID"] = 0;
                DrGRDAdvanceRefund["HsnSacCode"] = 0;
                DrGRDAdvanceRefund["GoodsServiceInd"] = 0;
                DrGRDAdvanceRefund["ItemQty"] = 0;
                DrGRDAdvanceRefund["ItemUnitID"] = 0;
                DrGRDAdvanceRefund["ItemRate"] = 0;
                DrGRDAdvanceRefund["ItemAmt"] = CommonCls.ConvertDecimalZero(txtFinalRefundAmt.Text);
                DrGRDAdvanceRefund["TaxRate"] = CommonCls.ConvertDecimalZero(lblTaxRate.Text);
                DrGRDAdvanceRefund["IGSTTax"] = 0;
                DrGRDAdvanceRefund["IGSTTaxAmt"] = 0;
                DrGRDAdvanceRefund["CGSTTax"] = 0;
                DrGRDAdvanceRefund["CGSTTaxAmt"] = 0;
                DrGRDAdvanceRefund["SGSTTax"] = 0;
                DrGRDAdvanceRefund["SGSTTaxAmt"] = 0;
                DrGRDAdvanceRefund["CESSTax"] = 0;
                DrGRDAdvanceRefund["CESSTaxAmt"] = 0;
                DrGRDAdvanceRefund["ItemRemark"] = "";
                DrGRDAdvanceRefund["ExtraInd"] = 0;
                dtGRDAdvanceRefund.Rows.Add(DrGRDAdvanceRefund);

                VsdtAdvanceRefund = dtGRDAdvanceRefund;
            }
        }
    }

    #region Schema

    DataTable DTAdvanceRefundSchema()
    {
        DataTable dtAR = new DataTable();
        dtAR.Columns.Add("CompanyID", typeof(int));
        dtAR.Columns.Add("BranchID", typeof(int));
        dtAR.Columns.Add("ItemName", typeof(string));
        dtAR.Columns.Add("ItemUnit", typeof(string));
        dtAR.Columns.Add("ItemID", typeof(long));
        dtAR.Columns.Add("HsnSacCode", typeof(int));
        dtAR.Columns.Add("GoodsServiceInd", typeof(int));
        dtAR.Columns.Add("ItemQty", typeof(decimal));
        dtAR.Columns.Add("ItemUnitID", typeof(int));
        dtAR.Columns.Add("ItemRate", typeof(decimal));
        dtAR.Columns.Add("ItemAmt", typeof(decimal));
        dtAR.Columns.Add("TaxRate", typeof(decimal));
        dtAR.Columns.Add("IGSTTax", typeof(decimal));
        dtAR.Columns.Add("IGSTTaxAmt", typeof(decimal));
        dtAR.Columns.Add("CGSTTax", typeof(decimal));
        dtAR.Columns.Add("CGSTTaxAmt", typeof(decimal));
        dtAR.Columns.Add("SGSTTax", typeof(decimal));
        dtAR.Columns.Add("SGSTTaxAmt", typeof(decimal));
        dtAR.Columns.Add("CESSTax", typeof(decimal));
        dtAR.Columns.Add("CESSTaxAmt", typeof(decimal));
        dtAR.Columns.Add("ItemRemark", typeof(string));
        dtAR.Columns.Add("ExtraInd", typeof(int));

        return dtAR;
    }

    #endregion

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}