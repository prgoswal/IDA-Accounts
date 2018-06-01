using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdDebitNote : System.Web.UI.Page
{
    UpdDebitNoteModel ObjUpdDNModel;
    //static int rowIndex;
    int rowIndex
    {
        get { return CommonCls.ConvertIntZero(ViewState["rowIndex"]); }
        set { ViewState["rowIndex"] = value; }
    }

    DataTable VSItemsData
    {
        get { return (DataTable)ViewState["dtItemsBind"]; }
        set { ViewState["dtItemsBind"] = value; }
    }
    DataTable VSDtFinalItemData
    {
        get { return (DataTable)ViewState["DtFinalItem"]; }
        set { ViewState["DtFinalItem"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ViewState["VchType"] = 9;
            AllBinding();
            BindCancelReason();

            txtVoucherNo.Focus();
            txtDrAmount.Enabled = true;
            txtVoucherNo.Enabled = true;
            txtInVoiceNo.Enabled = true;
            txtInvoiceDate.Enabled = true;
            btngo.Enabled = true;

            if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
            {
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
            }
            if (Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || GlobalSession.IsAdmin == 1)
            {
                btnUpdate.Visible = true;
            }
        }
    }

    private void BindCancelReason()
    {
        try
        {
            ObjUpdDNModel = new UpdDebitNoteModel();
            ObjUpdDNModel.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, ObjUpdDNModel);
            if (dtCancelReason.Rows.Count > 0)
            {
                ddlCancelReason.DataSource = dtCancelReason;
                ddlCancelReason.DataTextField = "CancelReason";
                ddlCancelReason.DataValueField = "CancelID";
                ddlCancelReason.DataBind();
                if (dtCancelReason.Rows.Count > 1)
                {
                    ddlCancelReason.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
            else
            {

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    void AllBinding()
    {
        ObjUpdDNModel = new UpdDebitNoteModel();
        ObjUpdDNModel.Ind = 11;
        ObjUpdDNModel.OrgID = GlobalSession.OrgID;
        ObjUpdDNModel.BrID = GlobalSession.BrID;
        ObjUpdDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjUpdDNModel.YrCD = GlobalSession.YrCD;

        string uri = string.Format("UpdDebitNoteVoucher/BindAllDebitNoteDDL");

        DataSet dsAllBind = CommonCls.ApiPostDataSet(uri, ObjUpdDNModel);
        if (dsAllBind.Tables.Count > 0)
        {
            //-----------------------------------------TableName------------------------------------------
            DataTable dtAccounthead = dsAllBind.Tables["AccountHead"];
            DataTable dtNarration = dsAllBind.Tables["Narration"];
            DataTable dtItemunit = dsAllBind.Tables["ItemUnit"];
            DataTable dtIssueReason = dsAllBind.Tables["Issue Reason"];
            DataTable dtLastVoucherNo = dsAllBind.Tables["LastVoucherNo"];
            DataTable dtCostCenter = dsAllBind.Tables[5];

            //---------------------------------------------Account Head Binding------------------------------------ 

            ddlAccountHead.DataSource = dtAccounthead;
            ddlAccountHead.DataTextField = "AccName";
            ddlAccountHead.DataValueField = "AccCode";
            ddlAccountHead.DataBind();
            if (dtAccounthead.Rows.Count > 1)
                ddlAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));

            //--------------------------------------- NarrationS Bindings-----------------------------------------------

            ddlNarration.DataSource = dtNarration;
            ddlNarration.DataTextField = "NarrationDesc";
            ddlNarration.DataBind();

            //----------------------------------------------------------Item Unit binding---------------------------------------------
            ddlUnit.DataSource = dtItemunit;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "UnitID";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("-- Select --", "0"));

            //-----------------------------------Issue Reason--------------------------------------------------------------------- 

            ddlresion.DataSource = dtIssueReason;
            ddlresion.DataTextField = "ReasonDesc";
            ddlresion.DataValueField = "ReasonId";
            ddlresion.DataBind();
            if (dtIssueReason.Rows.Count > 1)
                ddlresion.Items.Insert(0, new ListItem("-- Select --", "0"));

            //---------------------------------------- Last VoucherNo & Date Bindings---------------------------------------------

            if (dtLastVoucherNo != null)
            {
                if (dtLastVoucherNo.Rows[0]["LastNo"].ToString() == "0")
                {
                    return;
                }
                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucherNo.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtLastVoucherNo.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
            }


            // Cost Center List
            if (GlobalSession.CCCode == 1)
            {
                thCCCode.Visible = true;
                tdCCCode.Visible = true;
                if (dtCostCenter.Rows.Count > 0)
                {
                    ddlCostCenter.DataSource = dtCostCenter;
                    ddlCostCenter.DataTextField = "CostCentreName";
                    ddlCostCenter.DataValueField = "CostCentreID";
                    ddlCostCenter.DataBind();
                    ddlCostCenter.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }

        }
    }
    protected void ddlAgainst_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAgainst.SelectedValue == "0")
            {
                ddlPartyAccount.DataSource = new DataTable();
                ddlPartyAccount.DataBind();

                ShowMessage("Select Debit Note Against.", false);
                return;
            }

            LoadPartyAccount();
            ddlAgainst.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage(lblMsg.Text, false);
        }
    }

    void LoadPartyAccount()
    {
        ObjUpdDNModel = new UpdDebitNoteModel();
        //ObjDNModel.Ind = ddlAccountHead.SelectedValue == "943500" ? 11 : 10; // For Party Account Accourding To accountHead.
        ObjUpdDNModel.Ind = ddlAgainst.SelectedValue == "2" ? 11 : 10; // For Party Account Accourding To Sales/Purchase Debit Note.

        ObjUpdDNModel.OrgID = GlobalSession.OrgID;
        ObjUpdDNModel.BrID = GlobalSession.BrID;
        ObjUpdDNModel.YrCD = GlobalSession.YrCD;

        string uri = string.Format("UpdDebitNoteVoucher/LoadPartyAccount");
        DataTable dtPartyAccount = CommonCls.ApiPostDataTable(uri, ObjUpdDNModel);
        if (dtPartyAccount.Rows.Count > 0)
        {
            ddlPartyAccount.DataSource = dtPartyAccount;
            ddlPartyAccount.DataTextField = "AccName";
            ddlPartyAccount.DataValueField = "AccCode";
            ddlPartyAccount.DataBind();
            ddlPartyAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        else
        {
            ddlPartyAccount.DataSource = dtPartyAccount;
            ddlPartyAccount.DataBind();
        }
    }

    void FillGstinNo()
    {
        try
        {
            ObjUpdDNModel = new UpdDebitNoteModel();
            ObjUpdDNModel.Ind = 1;
            ObjUpdDNModel.OrgID = GlobalSession.OrgID;
            ObjUpdDNModel.BrID = GlobalSession.BrID;
            ObjUpdDNModel.YrCD = GlobalSession.YrCD;
            ObjUpdDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjUpdDNModel.AccCode = CommonCls.ConvertIntZero(ddlPartyAccount.SelectedValue);

            string uri = string.Format("UpdDebitNoteVoucher/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, ObjUpdDNModel);
            if (dtGSTIN.Rows.Count > 0)
            {
                ddlGstinNo.DataSource = dtGSTIN;
                ddlGstinNo.DataValueField = "GSTIN";
                ddlGstinNo.DataBind();
                if (dtGSTIN.Rows.Count > 1)
                    ddlGstinNo.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlGstinNo.Focus();
            }
            else
            {
                ddlGstinNo.DataSource = new DataTable();
                ddlGstinNo.DataBind();
            }
            ddlGstinNo.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    protected void ddlPartyAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGstinNo();
    }


    protected void CBGSTImapactTaken_CheckedChanged(object sender, EventArgs e)
    {
        CBImpacChange();
    }

    void CBImpacChange()
    {
        if (CBGSTImapactTaken.Checked)
        {
            btnSearchInvoice.CssClass = "btn btn-sxs btn-primary";
            txtDrAmount.Enabled = false;
        }
        else
        {
            btnSearchInvoice.CssClass = "btn btn-sxs btn-primary disabled";
            txtDrAmount.Enabled = true;
            divItemData.Visible = false;
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        try
        {

            if (string.IsNullOrEmpty(txtVoucherNo.Text))
            {
                txtVoucherNo.Focus();
                ShowMessage("Please Enter Voucher No.", false);
                return;
            }
            ObjUpdDNModel = new UpdDebitNoteModel();
            ObjUpdDNModel.Ind = 3;
            ObjUpdDNModel.OrgID = GlobalSession.OrgID;
            ObjUpdDNModel.BrID = GlobalSession.BrID;
            ObjUpdDNModel.YrCD = GlobalSession.YrCD;
            ObjUpdDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjUpdDNModel.VoucherNo = Convert.ToInt32(txtVoucherNo.Text);
            string uri = string.Format("UpdDebitNoteVoucher/SearchData");
            DataSet dsSearchData = CommonCls.ApiPostDataSet(uri, ObjUpdDNModel);

            if (dsSearchData.Tables.Count > 0)
            {
                DataTable dtDebit = dsSearchData.Tables[0];
                txtVoucherDate.Text = CommonCls.ConvertDateDB(dtDebit.Rows[0]["VoucharDate"].ToString());

                if (GlobalSession.CCCode == 1)
                {
                    ddlCostCenter.SelectedValue = CommonCls.ConvertIntZero(dsSearchData.Tables[0].Rows[0]["CCCode"]).ToString();
                }


                if (dtDebit.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ShowMessage("This Invoice No. Already Canceled.", false);
                    txtVoucherNo.Focus();
                    return;
                }

                if (Convert.ToDecimal(dtDebit.Rows[0]["AccCode"].ToString()) > 500000)
                {
                    ddlAgainst.SelectedValue = "2";
                }
                else if (Convert.ToDecimal(dtDebit.Rows[0]["AccCode"].ToString()) > 0 || Convert.ToDecimal(dtDebit.Rows[0]["AccCode"].ToString()) < 500000)
                {
                    ddlAgainst.SelectedValue = "1";
                }
                LoadPartyAccount();
                ddlPartyAccount.SelectedValue = dtDebit.Rows[0]["AccCode"].ToString();
                FillGstinNo();
                ddlGstinNo.SelectedValue = dtDebit.Rows[0]["GSTIN"].ToString();

                ddlAccountHead.SelectedValue = dtDebit.Rows[0]["CashBankCode"].ToString();
                txtDrAmount.Text = (dtDebit.Rows[0]["DrAmount"].ToString());

                if (ddlNarration.Items.FindByText(dtDebit.Rows[0]["Narration"].ToString()) == null)
                {
                    if (!string.IsNullOrEmpty(dtDebit.Rows[0]["Narration"].ToString()))
                    {
                        ddlNarration.Items.Add(dtDebit.Rows[0]["Narration"].ToString());
                        ddlNarration.SelectedValue = dtDebit.Rows[0]["Narration"].ToString();
                    }
                }
                else
                {
                    ddlNarration.SelectedValue = dtDebit.Rows[0]["Narration"].ToString();
                }

                ddlresion.SelectedValue = CommonCls.ConvertIntZero(dtDebit.Rows[0]["IssueReasonId"]).ToString();
                ddlPreIssue.SelectedValue = CommonCls.ConvertIntZero(dtDebit.Rows[0]["PreGstId"]) == 0 ? "2" : dtDebit.Rows[0]["PreGstId"].ToString();

                // ddlNarration.Text = dtDebit.Rows[0]["Narration"].ToString();
                txtInVoiceNo.Text = CommonCls.ConvertIntZero(dtDebit.Rows[0]["InvoiceNo"]) == 0 ? "" : CommonCls.ConvertIntZero(dtDebit.Rows[0]["InvoiceNo"]).ToString();
                txtInvoiceDate.Text = CommonCls.ConvertDateDB(dtDebit.Rows[0]["InvoiceDate"].ToString());


                DataTable dtItemsBind = dsSearchData.Tables[1];

                if (dtItemsBind.Rows.Count > 0)
                {
                    dtItemsBind.Columns.Add("ItemMinorUnit", typeof(string));

                    dtItemsBind.Columns["ItemName"].SetOrdinal(0);
                    dtItemsBind.Columns["ItemUnit"].SetOrdinal(1);
                    dtItemsBind.Columns["ItemMinorUnit"].SetOrdinal(2);
                    dtItemsBind.Columns["ItemID"].SetOrdinal(3);
                    dtItemsBind.Columns["HSNSACCode"].SetOrdinal(4);
                    dtItemsBind.Columns["GoodsServiceInd"].SetOrdinal(5);
                    dtItemsBind.Columns["ItemQty"].SetOrdinal(6);
                    dtItemsBind.Columns["FreeQty"].SetOrdinal(7);
                    dtItemsBind.Columns["ItemUnitID"].SetOrdinal(8);
                    dtItemsBind.Columns["ItemMinorUnitID"].SetOrdinal(9);
                    dtItemsBind.Columns["ItemMinorQty"].SetOrdinal(10);
                    dtItemsBind.Columns["ItemRate"].SetOrdinal(11);
                    dtItemsBind.Columns["ItemAmt"].SetOrdinal(12);
                    dtItemsBind.Columns["DiscountValue"].SetOrdinal(13);
                    dtItemsBind.Columns["DiscountType"].SetOrdinal(14);
                    dtItemsBind.Columns["DiscountAmt"].SetOrdinal(15);
                    dtItemsBind.Columns["NetAmt"].SetOrdinal(16);
                    dtItemsBind.Columns["TaxRate"].SetOrdinal(17);
                    dtItemsBind.Columns["IGSTTax"].SetOrdinal(18);
                    dtItemsBind.Columns["IGSTTaxAmt"].SetOrdinal(19);
                    dtItemsBind.Columns["SGSTTax"].SetOrdinal(20);
                    dtItemsBind.Columns["SGSTTaxAmt"].SetOrdinal(21);
                    dtItemsBind.Columns["CGSTTax"].SetOrdinal(22);
                    dtItemsBind.Columns["CGSTTaxAmt"].SetOrdinal(23);
                    dtItemsBind.Columns["CESSTax"].SetOrdinal(24);
                    dtItemsBind.Columns["CESSTaxAmt"].SetOrdinal(25);
                    dtItemsBind.Columns["ItemRemark"].SetOrdinal(26);
                    dtItemsBind.Columns["ExtraInd"].SetOrdinal(27);
                    dtItemsBind.Columns["OriginalInvoiceSeries"].SetOrdinal(28);
                    dtItemsBind.Columns["OriginalInvoiceNo"].SetOrdinal(29);
                    dtItemsBind.Columns["OriginalInvoiceDate"].SetOrdinal(30);
                    dtItemsBind.Columns["IncomeExpensecode"].SetOrdinal(31);

                    gvItemDetail.DataSource = VSItemsData = dtItemsBind;
                    gvItemDetail.DataBind();

                    txtInVoiceNo.Text = VSItemsData.Rows[0]["OriginalInvoiceNo"].ToString();
                    txtInvoiceDate.Text = CommonCls.ConvertDateDB(VSItemsData.Rows[0]["OriginalInvoiceDate"].ToString());
                    CBGSTImapactTaken.Checked = true;
                    //CBImpacChange(); 
                    CBGSTImapactTaken.Enabled = false;
                    txtInVoiceNo.Enabled = false;
                    txtInvoiceDate.Enabled = false;
                    txtDrAmount.Enabled = false;
                    divItemData.Visible = true;
                    ddlAccountHead.Visible = false;
                    ddlAgainst.Enabled = false;
                    ddlPartyAccount.Enabled = false;

                    if (gvItemDetail.Rows.Count > 0)
                    {
                        ddlCostCenter.Enabled = false;
                    }
                }
                else
                {
                    gvItemDetail.DataSource = VSItemsData = new DataTable();
                    gvItemDetail.DataBind();
                    divItemData.Visible = false;
                    CBImpacChange();

                    //if (gvFinalItemDetail.Rows.Count >= 1)
                    //{
                    //    divItemData.Visible = true;
                    //    gvItemDetail.DataSource = new DataTable();
                    //    gvItemDetail.DataBind();
                    //}
                    //else
                    //{
                    //ShowMessage("Items Not Available For This Invoice No", false);
                    //}
                }
            }
            btnCancel.Enabled = true;
            txtVoucherNo.Enabled = false;
            btngo.Enabled = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "EditItemRow")
        {
            DataRow drItems = VSItemsData.Rows[rowIndex];
            hfRowInd.Value = Convert.ToString(rowIndex);
            hfItemID.Value = drItems["ItemID"].ToString();
            txtItem.Text = drItems["ItemName"].ToString();
            txtHSN.Text = drItems["HSNSACCode"].ToString();
            txtQty.Text = drItems["ItemQty"].ToString();
            hfItemUnitID.Value = drItems["ItemUnitID"].ToString();
            //ddlUnit.SelectedItem.Text = drItems["UnitName"].ToString();
            ddlUnit.SelectedValue = drItems["ItemUnitID"].ToString();
            txtRate.Text = drItems["ItemRate"].ToString();

            //txtDiffAmt.Text = drItems["ItemAmount"].ToString();
            decimal r = Convert.ToDecimal(drItems["ItemAmt"].ToString());
            txtDiffAmt.Text = Math.Round(r, 2).ToString();

            txtTaxRate.Text = drItems["TaxRate"].ToString();
            txtCGSTAmt.Text = drItems["CGSTTaxAmt"].ToString();
            txtSGSTAmt.Text = drItems["SGSTTaxAmt"].ToString();
            txtIGSTAmt.Text = drItems["IGSTTaxAmt"].ToString();
            txtCessAmt.Text = drItems["CESSTaxAmt"].ToString();

            hfCGSTTax.Value = drItems["CGSTTax"].ToString();
            hfSGSTTax.Value = drItems["SGSTTax"].ToString();
            hfIGSTTax.Value = drItems["IGSTTax"].ToString();
            hfCESSTax.Value = drItems["CESSTax"].ToString();
            hfOrgQty.Value = drItems["OrgQty"].ToString();
            hfOrgRate.Value = drItems["OrgRate"].ToString();
            hfExpenseIncomeHead.Value = drItems["IncomeExpensecode"].ToString();

            //gvItemDetail.Rows[rowIndex].BackColor = Color.Bisque;

            //txtTaxableAmt.Text = drItems["NetAmt"].ToString();

            VSItemsData.Rows[rowIndex].Delete();
            gvItemDetail.DataSource = VSItemsData;
            gvItemDetail.DataBind();
            btnSaveITEM.Enabled = true;

            // Button btnEdit = (Button)gvItemDetail.Rows[rowIndex].FindControl("btnEdit");
            //btnEdit.Enabled = false;
            //DisableControl();
        }
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtGVFinalItemDetail = VSItemsData;
            dtGVFinalItemDetail.Rows[rowIndex].Delete();
            VSItemsData = dtGVFinalItemDetail;
            gvItemDetail.DataSource = dtGVFinalItemDetail;
            gvItemDetail.DataBind();
            CalculateTotalAmount();
        }
    }

    protected void btnSaveITEM_Click(object sender, EventArgs e)
    {
        if (!ValidOnAddItem())
        {
            return;
        }

        try
        {
            AddEditItem();
            ClearAfterSave();
            /// CalculateTotalAmount();

        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }

    bool ValidOnAddItem()
    {
        if (txtQty.Text == "0" || txtRate.Text == "0")
        {
            txtQty.Focus();
            ShowMessage("Difference Item Quantity And item Rate Cannot be Zero.", false);
            return false;
        }
        if (Convert.ToDecimal(txtQty.Text) < 0 || CommonCls.ConvertDecimalZero(txtRate.Text) < 0)
        {
            txtQty.Focus();
            ShowMessage("Difference Item Quantity And ItemRate Cannot be Negative value.", false);
            return false;
        }
        if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(hfOrgQty.Value))
        {
            txtQty.Focus();
            ShowMessage("Difference Item Quantity Cannot be greater than Original Item Quantity. " + hfOrgQty.Value, false);
            return false;
        }
        if (CommonCls.ConvertDecimalZero(txtRate.Text) > CommonCls.ConvertDecimalZero(hfOrgRate.Value))
        {
            txtRate.Focus();
            ShowMessage("Difference Item Rate Cannot be greater than Original Item Rate." + hfOrgRate.Value, false);
            return false;
        }
        return true;
        //if (CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()) < CommonCls.ConvertDecimalZero(txtQty.Text))
        //{
        //    txtQty.Focus();
        //    ShowMessage("Difference Item Quantity Cannot be greater than Original Item Quantity.", false);
        //    return false;
        //} 

    }
    //    if (string.IsNullOrEmpty(txtItem.Text))
    //    {
    //        ShowMessage("Enter Item", false);
    //        return false;
    //    }

    //    DataRow dr = VSItemsData.Rows[rowIndex];
    //    if (CommonCls.ConvertDecimalZero(dr["ItemRate"].ToString()) == CommonCls.ConvertDecimalZero(txtRate.Text))
    //    {
    //        if (CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()) == CommonCls.ConvertDecimalZero(txtQty.Text))
    //        {
    //            txtQty.Focus();
    //            //txtRate.Focus();
    //            ShowMessage("Change Item Rate Or Qty.", false);
    //            return false;
    //        }
    //    }

    //if (CommonCls.ConvertDecimalZero(dr["ItemRate"].ToString()) < CommonCls.ConvertDecimalZero(txtRate.Text))
    //{
    //    txtRate.Focus();
    //    ShowMessage("Difference Item Rate Cannot be greater than Original Item Rate.", false);
    //    return false;
    //}
    //if (CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()) < CommonCls.ConvertDecimalZero(txtQty.Text))
    //{
    //    txtQty.Focus();
    //    ShowMessage("Difference Item Quantity Cannot be greater than Original Item Quantity.", false);
    //    return false;
    //}
    //    //------------------------------------------------------------------------Check Item Already In Grid Or Not--------------------------------
    //    if (VSItemsData == null)
    //    {
    //        VSItemsData = DtItemsSchema();
    //    }

    //    if (VSItemsData.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in VSItemsData.Rows)
    //        {

    //            int OriginalInvoiceNo = Convert.ToInt32(row["OriginalInvoiceNo"].ToString());
    //            string OriginalInvoiceDate = row["OriginalInvoiceDate"].ToString();
    //            int ItemId = Convert.ToInt32(row["ItemID"].ToString());
    //            if (Convert.ToInt32(hfItemID.Value) == ItemId && OriginalInvoiceNo == Convert.ToInt32(txtInVoiceNo.Text) && OriginalInvoiceDate == CommonCls.ConvertToDate(txtInvoiceDate.Text))
    //            {
    //                ShowMessage("This Item allready exist", false);
    //                return false;
    //            }
    //        }
    //    }

    //    return true;
    //}

    void AddEditItem()
    {
        if (VSItemsData == null)
        {
            VSItemsData = DtItemsSchema();
        }

        DataRow drItems = VSItemsData.NewRow();

        // drItems["CompanyID"] = GlobalSession.OrgID;
        //drItems["BranchID"] = GlobalSession.BrID; 

        drItems["ItemName"] = txtItem.Text;
        drItems["ItemUnit"] = ddlUnit.SelectedItem.Text;
        drItems["ItemMinorUnit"] = "";
        drItems["ItemID"] = CommonCls.ConvertIntZero(hfItemID.Value);
        drItems["HSNSACCode"] = txtHSN.Text;
        drItems["GoodsServiceInd"] = 0;
        drItems["ItemQty"] = txtQty.Text;
        drItems["FreeQty"] = 0;
        drItems["ItemUnitID"] = CommonCls.ConvertIntZero(hfItemUnitID.Value);//ddlUnit.SelectedValue;
        drItems["ItemMinorUnitID"] = 0;
        drItems["ItemMinorQty"] = 0;
        drItems["ItemRate"] = txtRate.Text;
        drItems["ItemAmt"] = Math.Round(Convert.ToDecimal(txtDiffAmt.Text), 2);
        drItems["DiscountValue"] = 0;
        drItems["DiscountType"] = 0;
        drItems["DiscountAmt"] = 0;
        drItems["NetAmt"] = txtDiffAmt.Text;
        drItems["TaxRate"] = txtTaxRate.Text;
        drItems["CGSTTax"] = CommonCls.ConvertDecimalZero(hfCGSTTax.Value);
        drItems["CGSTTaxAmt"] = CommonCls.ConvertDecimalZero(txtCGSTAmt.Text);
        drItems["SGSTTax"] = CommonCls.ConvertDecimalZero(hfSGSTTax.Value);
        drItems["SGSTTaxAmt"] = CommonCls.ConvertDecimalZero(txtSGSTAmt.Text);
        drItems["IGSTTax"] = CommonCls.ConvertDecimalZero(hfIGSTTax.Value);
        drItems["IGSTTaxAmt"] = CommonCls.ConvertDecimalZero(txtIGSTAmt.Text);
        drItems["CESSTax"] = CommonCls.ConvertDecimalZero(hfCESSTax.Value);
        drItems["CESSTaxAmt"] = CommonCls.ConvertDecimalZero(txtCessAmt.Text);
        drItems["ItemRemark"] = "";
        drItems["ExtraInd"] = 0;
        drItems["OriginalInvoiceSeries"] = 0;
        drItems["OriginalInvoiceNo"] = txtInVoiceNo.Text;
        drItems["OriginalInvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        drItems["OrgQty"] = hfOrgQty.Value;
        drItems["OrgRate"] = hfOrgRate.Value;
        drItems["IncomeExpensecode"] = hfExpenseIncomeHead.Value;
        VSItemsData.Rows.Add(drItems);
        gvItemDetail.DataSource = VSItemsData;
        gvItemDetail.DataBind();
        txtDrAmount.Enabled = false;
        hfOrgQty.Value = "";
        hfOrgRate.Value = "";
        hfCGSTTax.Value = hfSGSTTax.Value = hfIGSTTax.Value = hfCESSTax.Value = "";
        CalculateTotalAmount();
    }
    DataTable DtItemsSchema()
    {
        DataTable dtItems = new DataTable();

        //dtItems.Columns.Add("CompanyID", typeof(string));
        //dtItems.Columns.Add("BranchID", typeof(string)); 

        dtItems.Columns.Add("ItemName", typeof(string));
        dtItems.Columns.Add("ItemUnit", typeof(string));
        dtItems.Columns.Add("ItemMinorUnit", typeof(string));
        dtItems.Columns.Add("ItemID", typeof(int));
        dtItems.Columns.Add("HSNSACCode", typeof(string));
        dtItems.Columns.Add("GoodsServiceInd", typeof(int));
        dtItems.Columns.Add("ItemQty", typeof(decimal));
        dtItems.Columns.Add("FreeQty", typeof(decimal));
        dtItems.Columns.Add("ItemUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorQty", typeof(decimal));
        dtItems.Columns.Add("ItemRate", typeof(decimal));
        dtItems.Columns.Add("ItemAmt", typeof(decimal));
        dtItems.Columns.Add("DiscountType", typeof(int)); //0 Pending default;
        dtItems.Columns.Add("DiscountAmt", typeof(decimal));
        dtItems.Columns.Add("NetAmt", typeof(decimal));
        dtItems.Columns.Add("DiscountValue", typeof(int)); //0 Pending default;
        dtItems.Columns.Add("TaxRate", typeof(decimal));
        dtItems.Columns.Add("IGSTTax", typeof(decimal));
        dtItems.Columns.Add("IGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("SGSTTax", typeof(decimal));
        dtItems.Columns.Add("SGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("CGSTTax", typeof(decimal));
        dtItems.Columns.Add("CGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("CESSTax", typeof(decimal));
        dtItems.Columns.Add("CESSTaxAmt", typeof(decimal));
        dtItems.Columns.Add("ItemRemark", typeof(string));//0 Pending default; 
        dtItems.Columns.Add("ExtraInd", typeof(string));//0 Pending default;
        dtItems.Columns.Add("OriginalInvoiceSeries", typeof(string));
        dtItems.Columns.Add("OriginalInvoiceNo", typeof(int));
        dtItems.Columns.Add("OriginalInvoiceDate", typeof(string));
        dtItems.Columns.Add("OrgQty", typeof(string));
        dtItems.Columns.Add("OrgRate", typeof(string));
        dtItems.Columns.Add("ExpenseIncomecode", typeof(int));

        return dtItems;

    }

    //protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int rowIndex = Convert.ToInt32(e.CommandArgument);
    //    if (e.CommandName == "RemoveItem")
    //    {
    //        DataTable dtGVFinalItemDetail = VSItemsData;
    //        dtGVFinalItemDetail.Rows[rowIndex].Delete();
    //        VSItemsData = dtGVFinalItemDetail;
    //        gvItemDetail.DataSource = dtGVFinalItemDetail;
    //        gvItemDetail.DataBind();
    //        CalculateTotalAmount();
    //    }
    //}

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        CalculateRate();
        TaxCal();
        if (CommonCls.ConvertDecimalZero(txtRate.Text) > CommonCls.ConvertDecimalZero(hfOrgRate.Value))
        {
            txtRate.Focus();
            ShowMessage("Difference Item Rate Cannot be greater than Original Item Rate=." + hfOrgRate.Value, false);
            return;
        }
    }
    void CalculateRate()
    {
        decimal DiffAmount = 0;

        DiffAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtRate.Text);
        txtDiffAmt.Text = Math.Round(DiffAmount, 2).ToString();
    }

    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {
        try
        {
            StructItems item = new StructItems();
            item.ItemTaxable = CommonCls.ConvertDecimalZero(txtDiffAmt.Text);
            item.ItemRate = CommonCls.ConvertDecimalZero(txtTaxRate.Text);

            item.ItemCGSTRate = CommonCls.ConvertDecimalZero(hfCGSTTax.Value);
            item.ItemSGSTRate = CommonCls.ConvertDecimalZero(hfSGSTTax.Value);
            item.ItemIGSTRate = CommonCls.ConvertDecimalZero(hfIGSTTax.Value);
            item.ItemCESSRate = CommonCls.ConvertDecimalZero(hfCESSTax.Value);

            StructItems GetItem = Calculation.TaxCal(item);
            txtCGSTAmt.Text = (CgstAmt = GetItem.ItemCGSTAmt).ToString();
            txtSGSTAmt.Text = (SgstAmt = GetItem.ItemSGSTAmt).ToString();
            txtIGSTAmt.Text = (IgstAmt = GetItem.ItemIGSTAmt).ToString();
            txtCessAmt.Text = (CessAmt = GetItem.ItemCESSAmt).ToString();

            CgstRat = GetItem.ItemCGSTRate;
            SgstRat = GetItem.ItemSGSTRate;
            IgstRat = GetItem.ItemIGSTRate;
            CessRat = GetItem.ItemCESSRate;

        }
        catch (Exception ex)
        {

        }
    }
    void CalculateTotalAmount()
    {
        Decimal Igst = 0, Sgst = 0, Cgst = 0, Cess = 0;
        Decimal netAmt = 0;

        if (VSItemsData != null)
        {
            DataTable dtGrdFinalItems = VSItemsData;

            foreach (DataRow item in dtGrdFinalItems.Rows)
            {
                Igst += Convert.ToDecimal(item["IGSTTaxAmt"]);
                Sgst += Convert.ToDecimal(item["SGSTTaxAmt"]);
                Cgst += Convert.ToDecimal(item["CGSTTaxAmt"]);
                Cess += Convert.ToDecimal(item["CESSTaxAmt"]);
                //Taxable += Convert.ToDecimal(item["NetAmt"]); 
                netAmt += Convert.ToDecimal(item["ItemAmt"]);
            }
            txtDrAmount.Text = (Igst + Sgst + Cgst + Cess + netAmt).ToString();


            //txtNet.Text = netAmt.ToString();
            //txtTaxable.Text = (Igst + Sgst + Cgst + Cess).ToString();
            //decimal taxableAmt = Convert.ToDecimal(txtTaxable.Text);
            //decimal NatAmt = Convert.ToDecimal(txtNet.Text);
            //txtGross.Text = Convert.ToString (NatAmt + taxableAmt);
        }
    }
    // static bool NotSameInd;
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        CalculateRate();
        TaxCal();
        if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(hfOrgQty.Value))
        {
            txtQty.Focus();
            ShowMessage("Difference Item Quantity Cannot be greater than Original Item Quantity=." + hfOrgQty.Value, false);
            return;
        }
        txtQty.Focus();

    }

    DataTable CreateDebitNote(UpdDebitNoteModel ObjUpdDNModel)
    {
        DataTable dtDebitNote = DtDebitNote();
        DataRow drDebitNote = dtDebitNote.NewRow();
        drDebitNote["OrgID"] = ObjUpdDNModel.OrgID;
        drDebitNote["BrID"] = ObjUpdDNModel.BrID;
        drDebitNote["VchType"] = ObjUpdDNModel.VchType;
        drDebitNote["YrCD"] = ObjUpdDNModel.YrCD;
        drDebitNote["DocDate"] = ObjUpdDNModel.VoucherDate;
        drDebitNote["DocNo"] = 0;
        drDebitNote["AccCode"] = Convert.ToInt32(ddlPartyAccount.SelectedItem.Value);
        if (ddlGstinNo.SelectedItem == null || string.IsNullOrEmpty(ddlGstinNo.SelectedItem.Value))
        {
            drDebitNote["AccGst"] = "";
        }
        else
        {
            drDebitNote["AccGst"] = ddlGstinNo.SelectedItem.Value;
        }

        drDebitNote["AccCode2"] = Convert.ToInt32(ddlAccountHead.SelectedItem.Value);
        //if (CBGSTImapactTaken.Checked)
        //{
        //}
        //else
        //{
        //    drDebitNote["RefNo"] = txtInVoiceNo.Text == null ? 0 : Convert.ToInt32(txtInVoiceNo.Text);
        //    drDebitNote["RefDate"] = txtInvoiceDate.Text == null ? "" : CommonCls.ConvertToDate(txtInvoiceDate.Text);
        //}
        drDebitNote["RefNo"] = CommonCls.ConvertIntZero(txtInVoiceNo.Text);
        drDebitNote["RefDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);

        drDebitNote["AmountDr"] = Convert.ToDecimal(txtDrAmount.Text);
        //drDebitNote["AmountCr"] = ObjDNModel.AmountCr;
        drDebitNote["DocDesc"] = ObjUpdDNModel.DocDesc;
        drDebitNote["EntryType"] = ObjUpdDNModel.EntryType;
        drDebitNote["UserID"] = ObjUpdDNModel.User;
        drDebitNote["IP"] = ObjUpdDNModel.IP;
        dtDebitNote.Rows.Add(drDebitNote);
        return dtDebitNote;
    }

    DataTable DtDebitNote()
    {
        DataTable DtDebit = new DataTable();
        DtDebit.Columns.Add("OrgID", typeof(int));
        DtDebit.Columns.Add("BrID", typeof(int));
        DtDebit.Columns.Add("VchType", typeof(int));
        DtDebit.Columns.Add("YrCD", typeof(int));
        DtDebit.Columns.Add("DocDate", typeof(string));
        DtDebit.Columns.Add("DocNo", typeof(int));
        DtDebit.Columns.Add("AccCode", typeof(int));
        DtDebit.Columns.Add("AccGst", typeof(string));
        DtDebit.Columns.Add("AccCode2", typeof(int));
        DtDebit.Columns.Add("RefNo", typeof(int));
        DtDebit.Columns.Add("RefDate", typeof(string));
        DtDebit.Columns.Add("AmountDr", typeof(decimal));
        DtDebit.Columns.Add("AmountCr", typeof(decimal));
        DtDebit.Columns.Add("DocDesc", typeof(string));
        DtDebit.Columns.Add("EntryType", typeof(int));
        DtDebit.Columns.Add("UserID", typeof(int));
        DtDebit.Columns.Add("IP", typeof(string));
        return DtDebit;
    }
    bool ValidOnSubmit()
    {
        bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            ShowMessage("Voucher Date Should Be Within Financial Year Date", false);
            return false;
        }

        if (ddlAgainst == null || ddlAgainst.SelectedItem == null || ddlAgainst.SelectedItem.Value == null || Convert.ToInt32(ddlAgainst.SelectedItem.Value) <= 0)
        {
            ShowMessage("Select DebitNote Against..", false);
            return false;
        }


        if (ddlPartyAccount == null || ddlPartyAccount.SelectedItem == null || ddlPartyAccount.SelectedItem.Value == null || Convert.ToInt32(ddlPartyAccount.SelectedItem.Value) <= 0)
        {
            ShowMessage("Select Party Account.", false);
            return false;
        }

        if (ddlGstinNo != null || ddlGstinNo.SelectedItem != null || ddlGstinNo.Items.Count > 0)// GSTIN No Not Select.
        {
            if (ddlGstinNo.Items.Count > 1 && ddlGstinNo.SelectedItem.Value == "0")
            {
                ShowMessage("Select Gstin No", false);
                return false;
            }

            if (!CBGSTImapactTaken.Checked)
            {

                if (ddlAccountHead == null || ddlAccountHead.SelectedItem == null || ddlAccountHead.SelectedItem.Value == null || Convert.ToInt32(ddlAccountHead.SelectedItem.Value) <= 0)
                {
                    ShowMessage("Income Head Not Available.", false);
                    return false;
                }
            }

        }

        if (GlobalSession.CCCode == 1)
        {
            if (ddlCostCenter.SelectedValue == "0")
            {
                ShowMessage("Select Cost Centre!", false);
                ddlCostCenter.Focus();
                ddlCostCenter.Enabled = true;
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtDrAmount.Text) || Convert.ToDecimal(txtDrAmount.Text) <= 0)
        {
            ShowMessage("Enter Difference Amount.", false);
            return false;
        }

        if (!string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Not Null
        {
            if (string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Copulsory On InvoiceNo
            {
                txtInvoiceDate.Focus();
                ShowMessage("Please Select Invoice Date.", false);
                return false;
            }
        }
        if (!string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Not Null
        {
            bool ValidInvoiceDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidInvoiceDate) // For Voucher Date Between Financial Year.
            {
                ShowMessage("Invoice Date Should Be Within Financial Year Date And Not More Than Todays Date!", false);
                return false;
            }

            if (string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Copulsory On InvoiceDate
            {
                txtInVoiceNo.Focus();
                ShowMessage("Please Enter Invoice No.", false);
                return false;
            }
        }
        if (CommonCls.ConvertIntZero(ddlresion.SelectedValue) == 0)
        {
            ddlresion.Focus();
            ShowMessage("Select Issue Reason", false);
            return false;
        }

        if (CBGSTImapactTaken.Checked)
        {
            if (CommonCls.ConvertIntZero(txtInVoiceNo.Text) == 0 || CommonCls.ConvertToDate(txtInvoiceDate.Text) == string.Empty)
            {
                txtInVoiceNo.Focus();
                ShowMessage("Enter Invoice No. & Enter Valid Date.", false);
                return false;
            }
        }



        if (ddlGstinNo.SelectedValue.ToUpper() != "")
        {
            bool GSTINNumber = CheckGSTINNumber_Validation();
            if (GSTINNumber == false)
            {
                ShowMessage("Invalid GSTIN No.", false);
                return false;
            }
        }
        return true;
    }

    private bool CheckGSTINNumber_Validation()
    {
        try
        {

            //check GSTIN Number Expression
            bool CheckGSTIN_Expression = CommonCls.validGSTIN(ddlGstinNo.SelectedValue.ToUpper());
            if (CheckGSTIN_Expression == true)
            {
                SalesModel ObjSaleModel;
                ObjSaleModel = new SalesModel();
                ObjSaleModel.Ind = 6;
                ObjSaleModel.OrgID = GlobalSession.OrgID;
                ObjSaleModel.BrID = GlobalSession.BrID;
                ObjSaleModel.AccCode = Convert.ToInt32(ddlPartyAccount.SelectedItem.Value);

                string uri = string.Format("PurchaseVoucher/CheckGSTIN_Number");

                DataSet dtStatePanNo = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
                if (dtStatePanNo.Tables[0].Rows.Count > 0)
                {


                    string PANNo = dtStatePanNo.Tables[1].Rows[0]["PanNo"].ToString();
                    DataTable dtState = dtStatePanNo.Tables[0];
                    DataRow[] drStates = dtState.Select("StateID=" + ddlGstinNo.SelectedValue.Substring(0, 2));
                    string StateID = (drStates.Count() > 0) ? drStates[0]["StateID"].ToString() : "";


                    if (CheckGSTIN_Expression == true && !string.IsNullOrEmpty(PANNo.ToUpper()))
                    {

                        //check GSTIN Number by Statid an panNo 
                        bool CheckGSTIN_Number = CommonCls.gstinvalid(ddlGstinNo.SelectedValue.ToUpper(), StateID, PANNo.ToUpper());
                        if (CheckGSTIN_Number == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }

            }
            return false;
        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message, false);
        }
        return false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (!ValidOnSubmit())
            {
                return;
            }

            ObjUpdDNModel = new UpdDebitNoteModel();
            ObjUpdDNModel.Ind = 4;
            ObjUpdDNModel.OrgID = GlobalSession.OrgID;
            ObjUpdDNModel.BrID = GlobalSession.BrID;
            ObjUpdDNModel.YrCD = GlobalSession.YrCD;
            ObjUpdDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjUpdDNModel.VoucherDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            ObjUpdDNModel.DocDesc = ddlNarration.SelectedItem == null ? "" : ddlNarration.SelectedItem.Text;
            ObjUpdDNModel.EntryType = 2;
            ObjUpdDNModel.User = GlobalSession.UserID;
            ObjUpdDNModel.IP = GlobalSession.IP;
            ObjUpdDNModel.VoucherNo = Convert.ToInt32(txtVoucherNo.Text);
            ObjUpdDNModel.issueReasonId = CommonCls.ConvertIntZero(ddlresion.SelectedValue);
            ObjUpdDNModel.PreGstId = CommonCls.ConvertIntZero(ddlPreIssue.SelectedValue);

            ObjUpdDNModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);


            ObjUpdDNModel.DtDebitNote = CreateDebitNote(ObjUpdDNModel);

            if (CBGSTImapactTaken.Checked)
            {
                ObjUpdDNModel.GSTOpted = 1;
                ObjUpdDNModel.DtItems = VSItemsData;
                txtDrAmount.Enabled = false;
            }
            else
            {
                ObjUpdDNModel.GSTOpted = 0;
                ObjUpdDNModel.InvoiceNo = CommonCls.ConvertIntZero(txtInVoiceNo.Text);
                ObjUpdDNModel.InvoiceDate = CommonCls.ConvertToDate(txtInvoiceDate.Text);
                if ((ObjUpdDNModel.DtItems == null) || (ObjUpdDNModel.DtItems.Rows.Count <= 0))
                {
                    ObjUpdDNModel.DtItems = DtItemsSchema();
                    DataRow drDTItemBlank = ObjUpdDNModel.DtItems.NewRow();

                    drDTItemBlank["ItemName"] = "";//txtItem.Text;
                    drDTItemBlank["ItemUnit"] = 0;//ddlUnit.SelectedItem.Text;
                    drDTItemBlank["ItemMinorUnit"] = 0;
                    drDTItemBlank["ItemID"] = 0;//CommonCls.ConvertIntZero(hfItemID.Value);
                    drDTItemBlank["HSNSACCode"] = 0;//txtHSN.Text;
                    drDTItemBlank["GoodsServiceInd"] = 0;
                    drDTItemBlank["ItemQty"] = 0;//txtQty.Text;
                    drDTItemBlank["FreeQty"] = 0;
                    drDTItemBlank["ItemUnitID"] = 0;//ddlUnit.SelectedValue;
                    drDTItemBlank["ItemMinorUnitID"] = 0;
                    drDTItemBlank["ItemMinorQty"] = 0;
                    drDTItemBlank["ItemRate"] = 0;//txtRate.Text;
                    drDTItemBlank["ItemAmt"] = 0;//txtDiffAmt.Text;
                    drDTItemBlank["DiscountValue"] = 0;
                    drDTItemBlank["DiscountType"] = 0;
                    drDTItemBlank["DiscountAmt"] = 0;
                    drDTItemBlank["NetAmt"] = 0;//txtDiffAmt.Text;
                    drDTItemBlank["TaxRate"] = 0;//txtTaxRate.Text;
                    drDTItemBlank["CGSTTax"] = 0;//CommonCls.ConvertDecimalZero(txtCGSTTax.Text);
                    drDTItemBlank["CGSTTaxAmt"] = 0;//CommonCls.ConvertDecimalZero(txtCGSTAmt.Text);
                    drDTItemBlank["SGSTTax"] = 0;//CommonCls.ConvertDecimalZero(txtSGSTTax.Text);
                    drDTItemBlank["SGSTTaxAmt"] = 0;//CommonCls.ConvertDecimalZero(txtSGSTAmt.Text);
                    drDTItemBlank["IGSTTax"] = 0;//CommonCls.ConvertDecimalZero(txtIGSTTax.Text);
                    drDTItemBlank["IGSTTaxAmt"] = 0;//CommonCls.ConvertDecimalZero(txtIGSTAmt.Text);
                    drDTItemBlank["CESSTax"] = 0;//CommonCls.ConvertDecimalZero(txtCessTax.Text);
                    drDTItemBlank["CESSTaxAmt"] = 0;//CommonCls.ConvertDecimalZero(txtCessAmt.Text);
                    drDTItemBlank["ItemRemark"] = "";
                    drDTItemBlank["ExtraInd"] = 0;
                    drDTItemBlank["OriginalInvoiceSeries"] = "";
                    drDTItemBlank["OriginalInvoiceNo"] = 0;//txtInVoiceNo.Text == null ? 0 : Convert.ToInt32(txtInVoiceNo.Text); 
                    drDTItemBlank["OriginalInvoiceDate"] = "";//txtInvoiceDate.Text==null ? "" : CommonCls.ConvertToDate(txtInvoiceDate.Text);// CommonCls.ConvertToDate(txtInvoiceDate.Text); 
                    drDTItemBlank["ExpenseIncomecode"] = 0;

                    ObjUpdDNModel.DtItems.Rows.Add(drDTItemBlank);
                    //ObjUpdDNModel.DtItems = VSDtFinalItemData;
                }
            }

            if (ObjUpdDNModel.DtItems.Columns.Contains("ITCApplicable"))
            {
                ObjUpdDNModel.DtItems.Columns.Remove("ITCApplicable");
            }
            if (ObjUpdDNModel.DtItems.Columns.Contains("ItemInd"))
            {
                ObjUpdDNModel.DtItems.Columns.Remove("ItemInd");
            }
            if (ObjUpdDNModel.DtItems.Columns.Contains("OrgRate"))
            {
                ObjUpdDNModel.DtItems.Columns.Remove("OrgRate");
            }
            if (ObjUpdDNModel.DtItems.Columns.Contains("OrgQty"))
            {
                ObjUpdDNModel.DtItems.Columns.Remove("OrgQty");
            }
            if (ObjUpdDNModel.DtItems.Columns.Contains("CCCode"))
            {
                ObjUpdDNModel.DtItems.Columns.Remove("CCCode");
            }

            string uri = string.Format("UpdDebitNoteVoucher/UpdateDebitNote");
            DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, ObjUpdDNModel);

            if (dtVoucher.Rows.Count > 0)
            {

                ClearAll();

                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucher.Rows[0]["DocMaxNo"].ToString() + " - " + Convert.ToDateTime(dtVoucher.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                ShowMessage("Record Save Successfully.", true);

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    public void ClearAfterSave()
    {
        txtItem.Text = "";
        txtHSN.Text = "";
        txtQty.Text = "";
        txtQty.Text = "";
        // ddlAgainst.ClearSelection();
        ddlUnit.ClearSelection();
        txtRate.Text = "";
        txtDiffAmt.Text = "";
        txtTaxRate.Text = "";
        txtCGSTAmt.Text = "";
        txtSGSTAmt.Text = "";
        txtIGSTAmt.Text = "";
        txtCessAmt.Text = "";

        rowIndex = -1;
    }
    void ClearAll()
    {
        divItemData.Visible = false;
        CBImpacChange();
        CBGSTImapactTaken.Checked = false;
        txtVoucherDate.Text = txtInVoiceNo.Text = txtInvoiceDate.Text = txtDrAmount.Text = "";

        ddlAccountHead.ClearSelection();
        ddlNarration.ClearSelection();
        ddlPartyAccount.DataSource = new DataTable();
        ddlPartyAccount.DataBind();
        ddlresion.ClearSelection();
        ddlPreIssue.ClearSelection();
        ddlAgainst.ClearSelection();
        txtDrAmount.Enabled = true;
        ddlGstinNo.DataSource = new DataTable();
        ddlGstinNo.DataBind();
        VSDtFinalItemData = null;
        // ddlAccountHead.Enabled = true ; 
        ddlAccountHead.Visible = true;
        ddlAgainst.Enabled = true;
        ddlPartyAccount.Enabled = true;

        txtVoucherNo.Text = "";
        gvItemDetail.DataSource = new DataTable();
        gvItemDetail.DataBind();
        txtVoucherNo.Focus();
        txtVoucherNo.Enabled = btngo.Enabled = true;
        ddlCostCenter.Enabled = true;
        ddlCostCenter.ClearSelection();

        ClearAfterSave();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    public void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";

        //CheckMsg = true;
        //lblMsg.ForeColor = type ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }



    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCancelReason.SelectedValue == "")
            {
                ShowMessage("Enter Cancel Reason", false);
                pnlConfirmInvoice.Visible = false;
                return;
            }
            UpdDebitNoteModel ObjUpdDNModel;
            ObjUpdDNModel = new UpdDebitNoteModel();
            ObjUpdDNModel.Ind = 12;
            ObjUpdDNModel.OrgID = GlobalSession.OrgID;
            ObjUpdDNModel.BrID = GlobalSession.BrID;
            ObjUpdDNModel.VchType = 9;
            ObjUpdDNModel.VoucherNo = Convert.ToInt32(txtVoucherNo.Text);
            ObjUpdDNModel.CancelReason = ddlCancelReason.SelectedItem.Text;

            string uri = string.Format("UpdDebitNoteVoucher/CancelVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjUpdDNModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ClearAll();
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar No. - " + ObjUpdDNModel.VoucherNo + " is Cancel successfully ", true);
                }
                else if (dtSave.Rows[0]["CancelInd"].ToString() == "0")
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel.", true);
                    //ClearAll();
                }
            }
            else
            {
                ShowMessage("Voucher Is Not Cancelled", false);
            }
        }
        catch (Exception)
        {
            ShowMessage("Record Not Cancel Please Try Again.", false);
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        txtCancelReason.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        pnlConfirmInvoice.Visible = true;
        pnlConfirmInvoice.Focus();

    }
}