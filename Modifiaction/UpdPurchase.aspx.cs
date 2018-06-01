﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_UpdPurchase : System.Web.UI.Page
{
    #region Declarations

    UpdatePurchaseVoucherModel objUpdPurchaseModel;
    DataTable dtGrdOtherCharge, dtGrdItemDetails;
    string CashSalesAcc = "943501"; // Cash Sales Account Value For AccountHead.

    DataTable VsdtPurchaseFrom
    {
        get { return (DataTable)ViewState["dtPurchaseFrom"]; }
        set { ViewState["dtPurchaseFrom"] = value; }
    }
    DataTable VsItemNameList
    {
        get { return (DataTable)ViewState["ItemNameList"]; }
        set { ViewState["ItemNameList"] = value; }
    }
    DataTable VsdtSundri
    {
        get { return (DataTable)ViewState["dtSundri"]; }
        set { ViewState["dtSundri"] = value; }
    }
    DataTable VsdtItems
    {
        get { return (DataTable)ViewState["dtItems"]; }
        set { ViewState["dtItems"] = value; }
    }
    DataTable VsdtGvItemDetail
    {
        get { return (DataTable)ViewState["dtGvItemDetail"]; }
        set { ViewState["dtGvItemDetail"] = value; }
    }
    DataTable VsdtGvFreeItem
    {
        get { return (DataTable)ViewState["dtGvFreeItem"]; }
        set { ViewState["dtGvFreeItem"] = value; }
    }
    DataTable VsDtItemSellRate
    {
        get { return (DataTable)ViewState["DtItems"]; }
        set { ViewState["DtItems"] = value; }
    }
    DataTable VsdtNarration
    {
        get { return (DataTable)ViewState["dtNarration"]; }
        set { ViewState["dtNarration"] = value; }
    }

    DataTable VSBudgetHead
    {
        get { return (DataTable)ViewState["VSBudgetHead"]; }
        set { ViewState["VSBudgetHead"] = value; }
    }

    string Narration
    {
        get { return (string)ViewState["Narration"]; }
        set { ViewState["Narration"] = value; }
    }

    public bool PayModeIsCredit
    {
        get
        {
            if (CommonCls.ConvertIntZero(ddlPayMode.SelectedValue) == 1)
            {
                return true;
            }
            return false;
        }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ViewState["VchType"] = 5;
            ClearAll();
            BindAll();

            //ddlPurchaseFrom.Enabled = txtPurchaseFrom.Enabled = ddlGstinNo.Enabled = txtGstinNo.Enabled = ddlShippingAdd.Enabled =
            //    txtShippingAdd.Enabled = ddlLocation.Enabled = ddlPayMode.Enabled = 
            ddlRCM.Enabled = false;

            txtMinorUnitQty.Enabled = false;

            ddlPayMode.Focus();
            PayModeInit();
            BindCancelReason();

            btnUpdate.Enabled = false;
            btnUpdate.Visible = true;

            if (Session["TransNo"] != null && Session["TransDate"] != null)
            {
                txtSearchVoucher.Text = Session["TransNo"].ToString();
                btnSearchVoucher_Click(sender, e);

                if (Session["UpdAndView"] == "Upd")
                {
                    if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                    {
                        btnCancel.Visible = btnApproved.Visible = btnUpdate.Visible = false;
                        //btnApproved.Visible = false;
                    }
                    else if (GlobalSession.DesignationID == 26)
                    {
                        btnCancel.Visible = btnUpdate.Visible = true;
                        btnApproved.Visible = false;
                    }
                    else
                    {
                        btnCancel.Visible = btnApproved.Visible = false;
                    }
                }
                else if (Session["UpdAndView"] == "View")
                {
                    if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DesignationID > 0)
                    {
                        if (Session["IsSendToAudit"] != null)
                        {
                            if (Session["IsSendToAudit"].ToString() == "1")
                                btnUpdate.Visible = false;
                        }

                        btnCancel.Visible = btnApproved.Visible = false;
                    }
                    else
                        btnCancel.Visible = btnApproved.Visible = btnUpdate.Visible = false;

                    //btnCancel.Visible = btnApproved.Visible = btnUpdate.Visible = false;
                }
            }

            //if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
            //{
            //    btnUpdate.Visible = true;
            //    btnCancel.Visible = true;
            //}
            //if (Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || GlobalSession.IsAdmin == 1)
            //{
            //    btnUpdate.Visible = true;
            //}
        }
        txtSearchVoucher.Focus();
    }

    private void BindCancelReason()
    {
        try
        {
            objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
            objUpdPurchaseModel.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
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

    void BindAll()
    {
        objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
        objUpdPurchaseModel.Ind = 11;
        objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
        objUpdPurchaseModel.BrID = GlobalSession.BrID;
        objUpdPurchaseModel.YrCD = GlobalSession.YrCD;
        objUpdPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("UpdatePurchaseVoucher/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, objUpdPurchaseModel);
        if (dsBindAll.Tables.Count > 0)
        {
            DataTable dtWareHouse = dsBindAll.Tables[0];
            DataTable dtNarration = dsBindAll.Tables[1];
            DataTable dtVoucherNoDate = dsBindAll.Tables[2];
            DataTable dtExpenseHead = dsBindAll.Tables[3];
            VsdtPurchaseFrom = dsBindAll.Tables[4];
            DataTable dtSundrieAccHead = dsBindAll.Tables[5];
            VsItemNameList = dsBindAll.Tables[6];
            DataTable dtItemUnits = dsBindAll.Tables[7];
            DataTable dtItemMinorUnits = dsBindAll.Tables[7];
            DataTable dtShippingAdd = dsBindAll.Tables[8];
            DataTable dtAccountType = dsBindAll.Tables[9];
            DataTable dtCostCenter = dsBindAll.Tables[10];
            DataTable dtBrokerList = dsBindAll.Tables[11];

            // For Warehouse Info Taken
            if (dtWareHouse.Rows.Count > 0)
            {
                ddlLocation.DataSource = dtWareHouse;
                ddlLocation.DataTextField = "WareHouseAddress";
                ddlLocation.DataValueField = "WareHouseID";
                ddlLocation.DataBind();
                if (dtWareHouse.Rows.Count > 1)
                {
                    ddlLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
                    ddlLocation.Enabled = true;
                }
                else
                    ddlLocation.Enabled = false;
            }

            // For Narration Info Taken
            //if (dtNarration.Rows.Count > 0)
            //{
            //    txtNarration.DataSource = VsdtNarration = dtNarration;
            //    txtNarration.DataTextField = "NarrationDesc";
            //    txtNarration.DataBind();
            //}

            // For Last Invoice / Voucher No. Info Taken
            if (dtVoucherNoDate.Rows.Count > 0)
            {
                if (dtVoucherNoDate.Rows[0]["LastNo"].ToString() != "0")
                {
                    lblInvoiceAndDate.Text = "Last Transaction No. & Date : " + dtVoucherNoDate.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtVoucherNoDate.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                }
            }

            // For Income Account Head List 
            if (dtExpenseHead.Rows.Count > 0)
            {
                ddlExpenseHead.DataSource = VSBudgetHead = dtExpenseHead;
                ddlExpenseHead.DataTextField = "AccName";
                ddlExpenseHead.DataValueField = "AccCode";
                ddlExpenseHead.DataBind();
                if (dtExpenseHead.Rows.Count > 1)
                    ddlExpenseHead.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlExpenseHead.SelectedIndex = 0;
            }

            // For Sundries Account Head 
            if (dtSundrieAccHead.Rows.Count > 0)
            {
                ddlHeadName.DataSource = dtSundrieAccHead;
                ddlHeadName.DataTextField = "SundriHeadName";
                ddlHeadName.DataValueField = "AccCode";
                ddlHeadName.DataBind();
                ddlHeadName.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlHeadName.SelectedIndex = 0;

            }

            // For Item Unit 
            if (dtItemUnits.Rows.Count > 0)
            {
                ddlUnitName.DataSource = dtItemUnits;
                ddlUnitName.DataTextField = "UnitName";
                ddlUnitName.DataValueField = "UnitID";
                ddlUnitName.DataBind();
                ddlUnitName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

            // For Free Item Unit 
            if (dtItemUnits.Rows.Count > 0)
            {
                ddlFreeUnit.DataSource = dtItemUnits;
                ddlFreeUnit.DataTextField = "UnitName";
                ddlFreeUnit.DataValueField = "UnitID";
                ddlFreeUnit.DataBind();
                ddlFreeUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

            // For Item Secondary Unit 
            if (dtItemMinorUnits.Rows.Count > 0)
            {
                ddlMinorUnit.DataSource = dtItemMinorUnits;
                ddlMinorUnit.DataTextField = "UnitName";
                ddlMinorUnit.DataValueField = "UnitID";
                ddlMinorUnit.DataBind();
                ddlMinorUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

            // Shipping Address
            if (dtShippingAdd.Rows.Count > 0)
            {
                txtShippingAdd.Text = dtShippingAdd.Rows[0]["BranchAddress"].ToString();
            }

            // For Creditor Account Head List 
            if (VsdtPurchaseFrom.Rows.Count > 0)
            {
                ddlPurchaseFrom.DataSource = VsdtPurchaseFrom;
                ddlPurchaseFrom.DataTextField = "AccName";
                ddlPurchaseFrom.DataValueField = "AccCode";
                ddlPurchaseFrom.DataBind();
                ddlPurchaseFrom.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlPurchaseFrom.SelectedIndex = 0;
            }

            // For Account type Bind.
            if (dtAccountType.Rows.Count > 0)
            {
                ddlPayMode.DataSource = dtAccountType;
                ddlPayMode.DataTextField = "AccName";
                ddlPayMode.DataValueField = "AccCode";
                ddlPayMode.DataBind();
                ddlPayMode.Items.Insert(0, new ListItem("CREDIT", "1"));
            }
            // For Item List
            if (VsItemNameList.Rows.Count > 0)
            {
                ddlItemName.DataSource = VsItemNameList;
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "ItemID";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlItemName.SelectedIndex = 0;
            }

            // Free Item List.
            if (VsItemNameList.Rows.Count > 0)
            {
                ddlFreeItemName.DataSource = VsItemNameList;
                ddlFreeItemName.DataTextField = "ItemName";
                ddlFreeItemName.DataValueField = "ItemID";
                ddlFreeItemName.DataBind();
                ddlFreeItemName.Items.Insert(0, new ListItem { Text = "-Select Item Name-", Value = "0" });
                ddlFreeItemName.SelectedIndex = 0;
            }

            //For Broker List
            if (dtBrokerList.Rows.Count > 0)
            {
                ddlBroker.DataSource = dtBrokerList;
                ddlBroker.DataTextField = "AccName";
                ddlBroker.DataValueField = "AccCode";
                ddlBroker.DataBind();
                ddlBroker.Items.Insert(0, new ListItem("-- Select --", "0"));

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

    protected void btnSearchVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
            objUpdPurchaseModel.Ind = 1;
            objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
            objUpdPurchaseModel.BrID = GlobalSession.BrID;
            objUpdPurchaseModel.YrCD = GlobalSession.YrCD;
            objUpdPurchaseModel.VchType = CommonCls.ConvertIntZero(ViewState["VchType"]);
            objUpdPurchaseModel.VoucherNo = CommonCls.ConvertIntZero(txtSearchVoucher.Text);
            string uri = string.Format("UpdatePurchaseVoucher/LoadPurchaseVoucherDetails");
            DataSet dsPVBasicDetails = CommonCls.ApiPostDataSet(uri, objUpdPurchaseModel);
            if (dsPVBasicDetails.Tables.Count > 0)
            {
                DataTable dtBasicDetails = dsPVBasicDetails.Tables["Basic"];
                DataTable dtItemDetails = dsPVBasicDetails.Tables["ItemDetails"];
                DataTable dtFreeItems = dsPVBasicDetails.Tables["FreeItems"];
                DataTable dtSundiresDetails = dsPVBasicDetails.Tables["Sundries"];

                if (dtBasicDetails.Rows.Count > 0)
                {
                    if (dtBasicDetails.Rows[0]["CancelInd"].ToString() == "1")
                    {
                        ShowMessage("This Transaction No. Already Canceled.", false, lblMsg);
                        txtSearchVoucher.Focus();
                        return;
                    }
                    if (GlobalSession.CCCode == 1)
                    {
                        ddlCostCenter.SelectedValue = CommonCls.ConvertIntZero(dtBasicDetails.Rows[0]["CCCode"]).ToString();
                    }

                    hfPurchaseSaleRecordID.Value = dtBasicDetails.Rows[0]["PurchaseSaleRecordID"].ToString();
                    txtVoucherDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["VoucharDate"]);

                    //txtIDARefNo.Text = dtBasicDetails.Rows[0]["Remark3"].ToString();
                    txtIDARefNo.Text = dtBasicDetails.Rows[0]["Remark3"].ToString().Replace("Old VNo-", "");
                    int byCashSales = Convert.ToInt32(dtBasicDetails.Rows[0]["ByCashSale"].ToString());
                    if (byCashSales == 0)
                    {
                        ddlPayMode.SelectedValue = "1";
                        ddlPurchaseFrom.SelectedValue = dtBasicDetails.Rows[0]["AccountCode"].ToString();

                        ddlGstinNo.DataSource = dtBasicDetails;
                        ddlGstinNo.DataValueField = "GSTIN";
                        ddlGstinNo.DataBind();
                        ddlShippingAdd.SelectedValue = dtBasicDetails.Rows[0]["ACCPOSID"].ToString();

                        txtPurchaseFrom.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
                        ddlPurchaseFrom.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
                    }
                    else
                    {
                        ddlPayMode.SelectedValue = dtBasicDetails.Rows[0]["AccountCode"].ToString();
                        txtPurchaseFrom.Text = dtBasicDetails.Rows[0]["PartyName"].ToString();
                        txtGstinNo.Text = dtBasicDetails.Rows[0]["PartyGSTIN"].ToString();
                        txtShippingAdd.Text = dtBasicDetails.Rows[0]["PartyAddress"].ToString();

                        txtPurchaseFrom.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
                        ddlPurchaseFrom.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = false;
                    }

                    txtOrderNo.Text = dtBasicDetails.Rows[0]["PONo"].ToString();
                    txtOrderDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["PODate"]);

                    txtBillNo.Text = dtBasicDetails.Rows[0]["PurchaseBillNo"].ToString();
                    txtBillDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["PurchaseBillDate"]);

                    ddlLocation.SelectedValue = dtBasicDetails.Rows[0]["WareHouseID"].ToString();
                    txtGRNNo.Text = dtBasicDetails.Rows[0]["GRNNo"].ToString();
                    txtGRNDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["GRNDate"]);

                    ddlTds.SelectedValue = dtBasicDetails.Rows[0]["TDSApplicable"].ToString();
                    ddlTCS.SelectedValue = dtBasicDetails.Rows[0]["TCSApplicable"].ToString();
                    ddlRCM.SelectedValue = dtBasicDetails.Rows[0]["RCMApplicable"].ToString();

                    txtGross.Text = dtBasicDetails.Rows[0]["GrossAmount"].ToString();
                    txtTax.Text = dtBasicDetails.Rows[0]["TaxAmount"].ToString();
                    txtNet.Text = dtBasicDetails.Rows[0]["NetAmount"].ToString();

                    txtNarration.Text = dtBasicDetails.Rows[0]["NarrationDesc"].ToString();
                }

                if (dtItemDetails.Rows.Count > 0)
                {
                    dtItemDetails.Columns.Add("ITCIGSTAmt", typeof(decimal));
                    dtItemDetails.Columns.Add("ITCSGSTAmt", typeof(decimal));
                    dtItemDetails.Columns.Add("ITCCGSTAmt", typeof(decimal));
                    dtItemDetails.Columns.Add("ITCCESSAmt", typeof(decimal));
                    dtItemDetails.Columns.Add("ItemInd2", typeof(string));
                    dtItemDetails.Columns.Add("ItemInd3", typeof(decimal));
                    dtItemDetails.Columns.Add("ItemSecondaryUnit", typeof(string));

                    dtItemDetails.Columns["PurchaseHeadName"].SetOrdinal(0);
                    dtItemDetails.Columns["ItemName"].SetOrdinal(1);
                    dtItemDetails.Columns["ItemUnit"].SetOrdinal(2);
                    dtItemDetails.Columns["ItemSecondaryUnit"].SetOrdinal(3);
                    dtItemDetails.Columns["PurchaseCode"].SetOrdinal(4);
                    dtItemDetails.Columns["ItemID"].SetOrdinal(5);
                    dtItemDetails.Columns["HSNSACCode"].SetOrdinal(6);
                    dtItemDetails.Columns["GoodsServiceInd"].SetOrdinal(7);
                    dtItemDetails.Columns["ItemQty"].SetOrdinal(8);
                    dtItemDetails.Columns["ItemUnitID"].SetOrdinal(9);
                    dtItemDetails.Columns["ItemMinorQty"].SetOrdinal(10);
                    dtItemDetails.Columns["ItemMinorUnitID"].SetOrdinal(11);
                    dtItemDetails.Columns["FreeQty"].SetOrdinal(12);
                    dtItemDetails.Columns["ItemRate"].SetOrdinal(13);
                    dtItemDetails.Columns["ItemAmount"].SetOrdinal(14);
                    dtItemDetails.Columns["DiscountValue"].SetOrdinal(15);
                    dtItemDetails.Columns["DiscountType"].SetOrdinal(16);
                    dtItemDetails.Columns["DiscountAmt"].SetOrdinal(17);
                    dtItemDetails.Columns["NetAmt"].SetOrdinal(18);
                    dtItemDetails.Columns["TaxRate"].SetOrdinal(19);
                    dtItemDetails.Columns["IGSTTax"].SetOrdinal(20);
                    dtItemDetails.Columns["IGSTTaxAmt"].SetOrdinal(21);
                    dtItemDetails.Columns["SGSTTax"].SetOrdinal(22);
                    dtItemDetails.Columns["SGSTTaxAmt"].SetOrdinal(23);
                    dtItemDetails.Columns["CGSTTax"].SetOrdinal(24);
                    dtItemDetails.Columns["CGSTTaxAmt"].SetOrdinal(25);
                    dtItemDetails.Columns["CESSTax"].SetOrdinal(26);
                    dtItemDetails.Columns["CESSTaxAmt"].SetOrdinal(27);
                    dtItemDetails.Columns["ITCApplicable"].SetOrdinal(28);
                    dtItemDetails.Columns["ITCIGSTAmt"].SetOrdinal(29);
                    dtItemDetails.Columns["ITCSGSTAmt"].SetOrdinal(30);
                    dtItemDetails.Columns["ITCCGSTAmt"].SetOrdinal(31);
                    dtItemDetails.Columns["ITCCESSAmt"].SetOrdinal(32);
                    dtItemDetails.Columns["FreeItemInd"].SetOrdinal(33);
                    dtItemDetails.Columns["ItemRemark"].SetOrdinal(34);
                    dtItemDetails.Columns["ItemInd"].SetOrdinal(35);
                    dtItemDetails.Columns["ItemInd2"].SetOrdinal(36);
                    dtItemDetails.Columns["ItemInd3"].SetOrdinal(37);

                    grdItemDetails.DataSource = VsdtGvItemDetail = dtItemDetails;
                    grdItemDetails.DataBind();
                }
                if (dtFreeItems.Rows.Count > 0)
                {
                    if (VsdtGvFreeItem == null)
                    {
                        VsdtGvFreeItem = VsdtGvItemDetail.Clone();
                    }

                    VsdtGvFreeItem.Columns["PurchaseHeadName"].SetOrdinal(0);
                    VsdtGvFreeItem.Columns["ItemName"].SetOrdinal(1);
                    VsdtGvFreeItem.Columns["ItemUnit"].SetOrdinal(2);
                    VsdtGvFreeItem.Columns["ItemSecondaryUnit"].SetOrdinal(3);
                    VsdtGvFreeItem.Columns["PurchaseCode"].SetOrdinal(4);
                    VsdtGvFreeItem.Columns["ItemID"].SetOrdinal(5);
                    VsdtGvFreeItem.Columns["HSNSACCode"].SetOrdinal(6);
                    VsdtGvFreeItem.Columns["GoodsServiceInd"].SetOrdinal(7);
                    VsdtGvFreeItem.Columns["ItemQty"].SetOrdinal(8);
                    VsdtGvFreeItem.Columns["ItemUnitID"].SetOrdinal(9);
                    VsdtGvFreeItem.Columns["ItemMinorQty"].SetOrdinal(10);
                    VsdtGvFreeItem.Columns["ItemMinorUnitID"].SetOrdinal(11);
                    VsdtGvFreeItem.Columns["FreeQty"].SetOrdinal(12);
                    VsdtGvFreeItem.Columns["ItemRate"].SetOrdinal(13);
                    VsdtGvFreeItem.Columns["ItemAmount"].SetOrdinal(14);
                    VsdtGvFreeItem.Columns["DiscountValue"].SetOrdinal(15);
                    VsdtGvFreeItem.Columns["DiscountType"].SetOrdinal(16);
                    VsdtGvFreeItem.Columns["DiscountAmt"].SetOrdinal(17);
                    VsdtGvFreeItem.Columns["NetAmt"].SetOrdinal(18);
                    VsdtGvFreeItem.Columns["TaxRate"].SetOrdinal(19);
                    VsdtGvFreeItem.Columns["IGSTTax"].SetOrdinal(20);
                    VsdtGvFreeItem.Columns["IGSTTaxAmt"].SetOrdinal(21);
                    VsdtGvFreeItem.Columns["SGSTTax"].SetOrdinal(22);
                    VsdtGvFreeItem.Columns["SGSTTaxAmt"].SetOrdinal(23);
                    VsdtGvFreeItem.Columns["CGSTTax"].SetOrdinal(24);
                    VsdtGvFreeItem.Columns["CGSTTaxAmt"].SetOrdinal(25);
                    VsdtGvFreeItem.Columns["CESSTax"].SetOrdinal(26);
                    VsdtGvFreeItem.Columns["CESSTaxAmt"].SetOrdinal(27);
                    VsdtGvFreeItem.Columns["ITCApplicable"].SetOrdinal(28);
                    VsdtGvFreeItem.Columns["ITCIGSTAmt"].SetOrdinal(29);
                    VsdtGvFreeItem.Columns["ITCSGSTAmt"].SetOrdinal(30);
                    VsdtGvFreeItem.Columns["ITCCGSTAmt"].SetOrdinal(31);
                    VsdtGvFreeItem.Columns["ITCCESSAmt"].SetOrdinal(32);
                    VsdtGvFreeItem.Columns["FreeItemInd"].SetOrdinal(33);
                    VsdtGvFreeItem.Columns["ItemRemark"].SetOrdinal(34);
                    VsdtGvFreeItem.Columns["ItemInd"].SetOrdinal(35);
                    VsdtGvFreeItem.Columns["ItemInd2"].SetOrdinal(36);
                    VsdtGvFreeItem.Columns["ItemInd3"].SetOrdinal(37);
                    foreach (DataRow row in dtFreeItems.Rows)
                    {
                        DataRow drItem = VsdtGvFreeItem.NewRow();

                        drItem["ItemID"] = row["ItemID"];
                        drItem["ItemName"] = row["ItemName"];
                        drItem["ItemUnitID"] = row["ItemUnitID"];
                        drItem["ItemUnit"] = row["ItemUnit"];
                        drItem["PurchaseCode"] = row["PurchaseCode"];
                        drItem["ItemQty"] = row["ItemQty"];
                        drItem["FreeItemInd"] = row["FreeItemInd"];

                        foreach (DataRow itemRow in VsdtGvItemDetail.Rows)
                        {
                            drItem["GoodsServiceInd"] = itemRow["GoodsServiceInd"];
                            drItem["HSNSACCode"] = itemRow["HSNSACCode"];
                            break;
                        }
                        VsdtGvFreeItem.Rows.Add(drItem);
                        continue;
                    }

                    gvFreeItem.DataSource = VsdtGvFreeItem; //=  dtFreeItems;
                    gvFreeItem.DataBind();
                    divFree.Visible = true;
                }
                if (dtSundiresDetails.Rows.Count > 0)
                {
                    gvotherCharge.DataSource = VsdtSundri = dtSundiresDetails;
                    gvotherCharge.DataBind();

                    decimal addAmt = 0, lessAmt = 0;
                    foreach (DataRow row in VsdtSundri.Rows)
                    {
                        if (row["SundriInd"].ToString() == "Add")
                        {
                            addAmt += Convert.ToDecimal(row["SundriAmt"]);
                        }
                        else if (row["SundriInd"].ToString() == "Less")
                        {
                            lessAmt += Convert.ToDecimal(row["SundriAmt"]);
                        }
                    }
                    txtAddLess.Text = (addAmt - lessAmt).ToString();
                    //txtNet.Text = Convert.ToString(CommonCls.ConvertDecimalZero(txtNet.Text) + CommonCls.ConvertDecimalZero(txtAddLess.Text));
                }

                if (dsPVBasicDetails.Tables.Count > 0)
                {
                    ddlBroker.SelectedValue = dtBasicDetails.Rows[0]["BrokerageID"].ToString();
                    ddlBroker_SelectedIndexChanged(sender, e);
                    ddlBrokerGSTIN.SelectedValue = dtBasicDetails.Rows[0]["BrokerageGSTIN"].ToString();
                    txtBrokerAmount.Text = dtBasicDetails.Rows[0]["BrokerageAmount"].ToString();
                    txtBrokerRate.Text = dtBasicDetails.Rows[0]["BrokerageRate"].ToString();
                }

                if (dtBasicDetails.Rows.Count > 0 && dtItemDetails.Rows.Count > 0)
                //if (dtBasicDetails.Rows.Count > 0 && dtItemDetails.Rows.Count > 0 && dtFreeItems.Rows.Count > 0 && dtSundiresDetails.Rows.Count > 0)
                {
                    txtSearchVoucher.Enabled = btnSearchVoucher.Enabled = false;
                    btnAddItemDetail.Enabled = btnFreeAdd.Enabled = btnAdd.Enabled = btnUpdate.Enabled = btnCancel.Enabled = true;
                }
                else
                {
                    ShowMessage("Invalid Transaction No.", false, lblMsg);
                    txtSearchVoucher.Focus();
                    return;
                }
            }
            else
            {
                ShowMessage("Invalid Transaction No.", false, lblMsg);
                txtSearchVoucher.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }

    void Filllocation()
    {
        objUpdPurchaseModel = new UpdatePurchaseVoucherModel()
        {
            Ind = 2,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
            VchType = Convert.ToInt32(ViewState["VchType"]),
            GSTIN = "0"
        };

        string uri = string.Format("UpdatePurchaseVoucher/Filllocation");
        DataTable dtLocation = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
        if (dtLocation.Rows.Count > 0)
        {
            if (dtLocation.Rows.Count > 1)
            {
                ddlLocation.DataSource = dtLocation;
                ddlLocation.DataTextField = "WareHouseAddress";
                ddlLocation.DataValueField = "WareHouseID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            else
            {
                ddlLocation.DataSource = dtLocation;
                ddlLocation.DataTextField = "WareHouseAddress";
                ddlLocation.DataValueField = "WareHouseID";
                ddlLocation.DataBind();
            }
        }
    }

    void FillShippingAddress()
    {
        objUpdPurchaseModel = new UpdatePurchaseVoucherModel()
        {
            Ind = 4,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
            VchType = Convert.ToInt32(ViewState["VchType"]),
            AccCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue),
            GSTIN = ddlGstinNo != null ?
                    ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
                    ddlGstinNo.SelectedValue : "" : "",

        };

        string uri = string.Format("UpdatePurchaseVoucher/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
        if (dtShipping.Rows.Count > 0)
        {
            ddlShippingAdd.DataSource = dtShipping;
            ddlShippingAdd.DataTextField = "POSAddress";
            ddlShippingAdd.DataValueField = "AccPOSID";
            ddlShippingAdd.DataBind();
            if (dtShipping.Rows.Count > 1)
                ddlGstinNo.Items.Insert(0, "-Select-");

        }
        else
        {
            ddlShippingAdd.DataSource = dtShipping;
            ddlShippingAdd.DataBind();
        }
    }

    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        PayModeInit();
    }

    void PayModeInit()
    {
        ddlTds.Enabled = ddlTCS.Enabled = false;
        if (PayModeIsCredit)
        {

            txtPurchaseFrom.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
            ddlPurchaseFrom.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;

            tblShippingDetail.Visible = true;
        }
        else
        {
            txtPurchaseFrom.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
            ddlPurchaseFrom.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = false;
        }
        ddlPayMode.Focus();
    }

    protected void txtGstinNo_TextChanged(object sender, EventArgs e)
    {
        if (txtGstinNo.Text.Length >= 1 && txtGstinNo.Text.Length < 15)
        {
            ShowMessage("Invalid GSTIN", false, lblMsg);
            txtGstinNo.Focus();
            return;
        }
        try
        {

            if (!string.IsNullOrEmpty(txtGstinNo.Text) && PayModeIsCredit)
            {
                objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
                objUpdPurchaseModel.Ind = 15;
                string uri = string.Format("Master/Master");
                DataTable dtState = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
                if (dtState.Rows.Count > 0)
                {
                    if (dtState.Select("StateID =" + txtGstinNo.Text.Substring(0, 2)).Count() == 0)
                    {
                        ShowMessage("Invalid GSTIN No.", false, lblMsg);
                        txtGstinNo.Focus();
                        return;
                    }
                }
            }
            txtShippingAdd.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage("Invalid GSTIN.", false, lblMsg);
            txtGstinNo.Text = "";
            txtGstinNo.Focus();
        }
    }

    protected void ddlExpenseHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PayModeIsCredit)
        {
            if (CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) == 0)
            {
                ddlExpenseHead.SelectedIndex = 0;

                ddlPurchaseFrom.Focus();
                ShowMessage("Select Purchase From.", false, lblMsg);
                return;
            }

            if (ddlGstinNo.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(ddlGstinNo.SelectedItem.Text))
                {
                    if (!CommonCls.GSTINIsValid(ddlGstinNo.SelectedItem.Text))
                    {
                        ddlGstinNo.Focus();
                        ShowMessage("Invalid GSTIN.", false, lblMsg);
                        return;
                    }
                }
            }
        }

        if (!PayModeIsCredit)
        {
            if (string.IsNullOrEmpty(txtPurchaseFrom.Text))
            {
                txtPurchaseFrom.Focus();
                ShowMessage("Enter Purchase From.", false, lblMsg);
                return;
            }

            if (!string.IsNullOrEmpty(txtGstinNo.Text) && !CommonCls.validGSTIN(txtGstinNo.Text))
            {
                txtGstinNo.Focus();
                ShowMessage("Invalid GSTIN No.", false, lblMsg);
                return;
            }
        }
        ddlItemName.Enabled = true;
        ddlItemName.Focus();
    }

    protected void ddlPurchaseFrom_SelectedIndexChanged(object sender, EventArgs e)    //ddlSales To Selected Index Chanage Gstin Number 
    {
        try
        {
            tblShippingDetail.Visible = true;
            objUpdPurchaseModel = new UpdatePurchaseVoucherModel()
            {
                Ind = 1,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
                AccCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue)
            };

            string uri = string.Format("UpdatePurchaseVoucher/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
            if (dtGSTIN.Rows.Count > 0)
            {
                if (dtGSTIN.Rows.Count > 1)
                {
                    ddlGstinNo.DataSource = dtGSTIN;
                    ddlGstinNo.DataValueField = "GSTIN";
                    ddlGstinNo.DataBind();
                    ddlGstinNo.Items.Insert(0, new ListItem("-- Select --", "0"));
                    ddlGstinNo.Focus();
                }
                else
                {
                    ddlGstinNo.DataSource = dtGSTIN;
                    ddlGstinNo.DataValueField = "GSTIN";
                    ddlGstinNo.DataBind();
                    FillShippingAddress();
                    ddlGstinNo.Focus();
                }
            }
            else
            {
                FillShippingAddress();
                ddlGstinNo.DataSource = dtGSTIN;
                ddlGstinNo.DataBind();
                ddlGstinNo.Focus();
            }

            DataTable dtPurchaseFrom = VsdtPurchaseFrom;
            int TDS = Convert.ToInt32(dtPurchaseFrom.Rows[ddlPurchaseFrom.SelectedIndex]["TDSApplicable"].ToString());
            int TCS = Convert.ToInt32(dtPurchaseFrom.Rows[ddlPurchaseFrom.SelectedIndex]["ISDApplicable"].ToString());
            int RCM = Convert.ToInt32(dtPurchaseFrom.Rows[ddlPurchaseFrom.SelectedIndex]["RCMApplicable"].ToString());
            if (TDS == 0)
            {
                ddlTds.SelectedValue = "0";
            }
            if (TCS == 0)
            {
                ddlTCS.SelectedValue = "0";
            }
            if (RCM == 0)
            {
                ddlRCM.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Internal Server Error.", false, lblMsg);
        }
    }

    protected void txtPurchaseFrom_TextChanged(object sender, EventArgs e)
    {
        txtGstinNo.Focus();
    }

    protected void ddlGstinNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlGstinNo.SelectedValue != null)
            {
                if (ddlGstinNo.SelectedValue == "0")
                {
                    ShowMessage("Select GSTIN No.", false, lblMsg);
                    ddlGstinNo.Focus();
                    return;
                }
            }
            Filllocation();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Internal Server Error.", false, lblMsg);
        }
    }

    /// <summary>
    /// Item Adding Operations. 
    /// Item Name Change
    /// </summary>
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtDiscount.Text = "";
            if (CommonCls.ConvertIntZero(ddlExpenseHead.SelectedValue) == 0)
            {
                ddlExpenseHead.Focus();
                ShowMessage("Select Expense Head First.", false, lblMsg);
                return;
            }
            objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
            objUpdPurchaseModel.Ind = 11;//1;
            objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
            objUpdPurchaseModel.BrID = GlobalSession.BrID;
            objUpdPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objUpdPurchaseModel.PartyCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue);
            objUpdPurchaseModel.ByCashSale = PayModeIsCredit ? 0 : 1;
            objUpdPurchaseModel.ItemID = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);

            if (PayModeIsCredit)
            {
                if (ddlGstinNo.SelectedItem != null)
                {
                    if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
                    {
                        objUpdPurchaseModel.GSTIN = ddlGstinNo.SelectedValue;
                    }
                }
            }
            else
            {
                objUpdPurchaseModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();
            }

            string uri = string.Format("UpdatePurchaseVoucher/FillItemPurchaseRate");
            DataSet dsItems = CommonCls.ApiPostDataSet(uri, objUpdPurchaseModel);
            if (dsItems.Tables.Count > 0)
            {
                VsdtItems = dsItems.Tables[0];

                ddlUnitName.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
                txtRate.Text = VsdtItems.Rows[0]["ItemSellingRate"].ToString();
                if (VsdtItems.Rows[0]["ItemMinorUnitID"].ToString() == "")
                    ddlMinorUnit.SelectedValue = "0";
                else
                    ddlMinorUnit.SelectedValue = Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();

                if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                {
                    VsDtItemSellRate = dsItems.Tables[1];
                    TaxWithInRange();
                }
                else
                    FillTaxRate(VsdtItems.Rows[0]);

                TaxCal();

                txtMinorUnitQty.Text = "";
                if (VsdtItems.Rows[0]["ItemMinorUnitID"].ToString() != "" && Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                    txtMinorUnitQty.Enabled = true;
                else
                    txtMinorUnitQty.Enabled = false;

                //if (GlobalSession.StockMaintaineByMinorUnit)
                //{
                //}

                ddlPurchaseFrom.Enabled = ddlGstinNo.Enabled = false;
                txtPurchaseFrom.Enabled = txtGstinNo.Enabled = false;

            }
            txtQty.Focus();
            CalculateRate();
        }

        catch (Exception ex)
        {
            ShowMessage("Internal Server Error.", false, lblMsg);
        }
    }

    void TaxWithInRange()
    {
        if (VsDtItemSellRate != null && VsDtItemSellRate.Rows.Count > 0)
        {
            int rowIndex = 0;
            decimal MaxRate = 0, MinRate = 0;
            decimal InsertedRate = CommonCls.ConvertDecimalZero(txtRate.Text);

            DataRow DrWithInRange = VsDtItemSellRate.NewRow();
            foreach (DataRow item in VsDtItemSellRate.Rows)
            {
                rowIndex++;

                MinRate = CommonCls.ConvertDecimalZero(item["RangeFrom"].ToString());
                MaxRate = CommonCls.ConvertDecimalZero(item["RangeTo"].ToString());

                if (InsertedRate >= MinRate && InsertedRate <= MaxRate)
                {
                    FillTaxRate(item);
                    DrWithInRange.ItemArray = item.ItemArray;
                    break;
                }
            }

            VsDtItemSellRate.Rows[rowIndex - 1].Delete();
            VsDtItemSellRate.Rows.InsertAt(DrWithInRange, 0);
        }
    }

    void FillTaxRate(DataRow drRate)
    {
        txtTaxRate.Text = drRate["TaxRate"].ToString();
        txtCGSTAmt.Text = drRate["CGSTRate"].ToString();
        txtSGSTAmt.Text = drRate["SGSTRate"].ToString();
        txtIGSTAmt.Text = drRate["IGSTRate"].ToString();
        txtCESSAmt.Text = drRate["Cess"].ToString();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateRate();
            TaxCal();
            if (txtMinorUnitQty.Enabled)
                txtMinorUnitQty.Focus();
            else
                txtFree.Focus();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false, lblMsg);
        }
    }

    protected void txtFree_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateRate();
            TaxCal();
            txtRate.Focus();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false, lblMsg);
        }
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (VsdtItems == null)
            {
                ShowMessage("Select Item First.", false, lblMsg);
                ddlItemName.Focus();
                return;
            }
            TaxWithInRange();
            CalculateRate();
            TaxCal();
            txtDiscount.Focus();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false, lblMsg);
        }
    }

    bool discountFlag;
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            discountFlag = true;
            if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > CommonCls.ConvertDecimalZero(txtItemAmt.Text))
            {
                ShowMessage("Discount Not Greater Than To Taxable Amount.", false, lblMsg);
                txtDiscount.Text = "0";
                CalculateRate();
                TaxCal();
                txtDiscount.Focus();
                return;
            }
            CalculateRate();
            TaxCal();
            ddlEdit.Focus();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false, lblMsg);
        }

    }

    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateRate();
        if (!string.IsNullOrEmpty(txtQty.Text))
            TaxCal();

        ddlDiscount.Focus();
    }

    protected void txtTaxRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TaxCal();
            if (txtCGSTAmt.Enabled)
                txtCGSTAmt.Focus();
            else
                btnAdd.Focus();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false, lblMsg);
        }
    }

    protected void grdItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtGrdItemDetails = VsdtGvItemDetail;
                dtGrdItemDetails.Rows[rowIndex].Delete();

                VsdtGvItemDetail = dtGrdItemDetails;
                grdItemDetails.DataSource = dtGrdItemDetails;
                grdItemDetails.DataBind();
                CalculateItemTotalAmount();
            }
            if (e.CommandName == "EditRow")
            {
                DataTable dtEditItemsDetails = VsdtGvItemDetail;
                DataRow drEditItemsDetails = dtEditItemsDetails.Rows[rowIndex];

                ddlExpenseHead.SelectedValue = drEditItemsDetails["PurchaseCode"].ToString();
                ddlItemName.SelectedValue = drEditItemsDetails["ItemID"].ToString();

                ddlItemName_SelectedIndexChanged(sender, e);

                hfHSNSACCode.Value = drEditItemsDetails["HSNSACCode"].ToString();
                hfGoodsAndServiceInd.Value = drEditItemsDetails["GoodsServiceInd"].ToString();
                txtQty.Text = drEditItemsDetails["ItemQty"].ToString();
                ddlUnitName.SelectedValue = drEditItemsDetails["ItemUnitID"].ToString();
                txtMinorUnitQty.Text = drEditItemsDetails["ItemMinorQty"].ToString();
                ddlMinorUnit.SelectedValue = drEditItemsDetails["ItemMinorUnitID"].ToString();
                txtFree.Text = drEditItemsDetails["FreeQty"].ToString();
                txtRate.Text = drEditItemsDetails["ItemRate"].ToString();
                txtItemAmt.Text = drEditItemsDetails["ItemAmount"].ToString();
                txtDiscount.Text = drEditItemsDetails["DiscountValue"].ToString();
                ddlDiscount.SelectedValue = drEditItemsDetails["DiscountType"].ToString();
                txtItemTaxableAmt.Text = drEditItemsDetails["NetAmt"].ToString();
                ddlITC.SelectedValue = drEditItemsDetails["ITCApplicable"].ToString();
                if (drEditItemsDetails["ItemInd"].ToString() == "0")
                {
                    ddlEdit.SelectedValue = "0";
                    txtTaxRate.Enabled = txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = false;
                }
                else
                {
                    ddlEdit.SelectedValue = "1";
                    txtTaxRate.Enabled = txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = true;
                }
                txtTaxRate.Text = drEditItemsDetails["TaxRate"].ToString();
                txtCGSTAmt.Text = drEditItemsDetails["CGSTTaxAmt"].ToString();
                txtSGSTAmt.Text = drEditItemsDetails["SGSTTaxAmt"].ToString();
                txtIGSTAmt.Text = drEditItemsDetails["IGSTTaxAmt"].ToString();
                txtCESSAmt.Text = drEditItemsDetails["CESSTaxAmt"].ToString();

                btnAddItemDetail.Text = "Update";

                dtEditItemsDetails.Rows.RemoveAt(rowIndex);

                grdItemDetails.DataSource = VsdtGvItemDetail = dtEditItemsDetails;
                grdItemDetails.DataBind();

                CalculateItemTotalAmount();
                //btnAddItemDetail.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAddItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlExpenseHead.SelectedItem == null || CommonCls.ConvertIntZero(ddlExpenseHead.SelectedValue) == 0)
            {
                ddlExpenseHead.Focus();
                ShowMessage("Select Expense Head.", false, lblMsg);
                return;
            }

            if (ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
            {
                ddlItemName.Focus();
                ShowMessage("Select Item Name.", false, lblMsg);
                return;
            }
            if (ddlItemName == null || ddlItemName.SelectedValue == null || Convert.ToInt32(ddlItemName.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
            {
                ddlItemName.Focus();
                ShowMessage("Item Value Not Available", false, lblMsg);
                return;
            }

            if (CommonCls.ConvertDecimalZero(txtQty.Text) == 0)
            {
                txtQty.Focus();
                ShowMessage("Enter Item Quantity", false, lblMsg);
                return;
            }
            if (CommonCls.ConvertIntZero(ddlUnitName.SelectedValue) == 0)
            {
                ddlUnitName.Focus();
                ShowMessage("Select Item Unit ", false, lblMsg);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtRate.Text) == 0)
            {
                txtRate.Focus();
                ShowMessage("Enter Item Rate", false, lblMsg);
                return;
            }
            if (CommonCls.ConvertIntZero(ddlDiscount.SelectedValue) == 1)
            {
                if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > 100)
                {
                    txtDiscount.Focus();
                    ShowMessage("Discount Not Greater Than 100%.", false, lblMsg);
                    return;
                }
            }
            if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > CommonCls.ConvertDecimalZero(txtItemAmt.Text))
            {
                ShowMessage("Discount Not Greater Than To Taxable Amount.", false, lblMsg);
                CalculateRate();
                TaxCal();
                txtDiscount.Focus();
                return;
            }

            if (GlobalSession.StockMaintaineByMinorUnit)
            {
                if (VsdtItems != null && CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) != 0)
                {
                    if (CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text) == 0)
                    {
                        ShowMessage("Enter Minor Unit Qty.", false, lblMsg);
                        txtMinorUnitQty.Focus();
                        return;
                    }
                }
            }

            DataTable dtGRD = VSBudgetHead;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);

                dv31.RowFilter = "AccCode  = " + ddlExpenseHead.SelectedValue + "";
                if (CommonCls.ConvertIntZero(dv31.ToTable().Rows[0]["MainGroupID"].ToString()) == 25 || CommonCls.ConvertIntZero(dv31.ToTable().Rows[0]["MainGroupID"].ToString()) == 24)
                {
                    bool BudgetHeadStatus = CheckBudgetHead();
                    if (BudgetHeadStatus == true)
                    {
                        if (ddlCostCenter.SelectedValue == "0")
                        {
                            ShowMessage("Select Cost Centre.", false, lblMsg);
                            ddlCostCenter.Focus();
                            return;
                        }
                    }

                }
            }

            BindGvItemDetail();
            CalculateItemTotalAmount();
            ClearItemDetailTable();
            ddlExpenseHead.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }

    private bool CheckBudgetHead()
    {
        objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
        objUpdPurchaseModel.Ind = 12;
        objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
        objUpdPurchaseModel.BrID = GlobalSession.BrID;
        objUpdPurchaseModel.YrCD = GlobalSession.YrCD;
        objUpdPurchaseModel.AccCode = CommonCls.ConvertIntZero(ddlExpenseHead.SelectedValue);
        string uri = string.Format("UpdatePurchaseVoucher/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
        if (dtCashReceipt.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtCashReceipt.Rows[0]["Cnt"].ToString()) > 0)
            {
                return true;
            }
        }
        return false;
    }


    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtTaxRate.Text);

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["Cess"]);

        StructItems GetItem = Calculation.TaxCal(item);
        txtIGSTAmt.Text = (IgstAmt = GetItem.ItemIGSTAmt).ToString();
        txtCGSTAmt.Text = (CgstAmt = GetItem.ItemCGSTAmt).ToString();
        txtSGSTAmt.Text = (SgstAmt = GetItem.ItemSGSTAmt).ToString();
        txtCESSAmt.Text = (CessAmt = GetItem.ItemCESSAmt).ToString();

        CgstRat = GetItem.ItemCGSTRate;
        SgstRat = GetItem.ItemSGSTRate;
        IgstRat = GetItem.ItemIGSTRate;
        CessRat = GetItem.ItemCESSRate;
    }

    void SelectioPA()
    {
        txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = txtItemTaxableAmt.Enabled = txtItemAmt.Enabled = false;
        txtDiscount.Focus();

    }

    void CalculateRate()
    {
        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        item.ItemFree = CommonCls.ConvertDecimalZero(txtFree.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtRate.Text);
        item.ItemDiscount = CommonCls.ConvertDecimalZero(txtDiscount.Text);
        item.DiscountInPerc = item.DiscountInPerc = Convert.ToInt16(ddlDiscount.SelectedValue) == 1;

        StructItems GetItem = Calculation.CalculateRate(item);
        txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
        txtItemAmt.Text = GetItem.ItemAmount.ToString();
        hfDiscountAmount.Value = GetItem.DiscountValue.ToString();
    }

    void BindGvItemDetail()
    {
        DataTable dtGvItemDetail = new DataTable();
        if (VsdtGvItemDetail == null)
        {
            dtGvItemDetail = DtItemsSchema();
        }
        else
        {
            dtGvItemDetail = VsdtGvItemDetail;
        }

        if (VsdtItems != null)
        {
            DataTable dtItems = VsdtItems;

            DataRow DrGvItemDetail = dtGvItemDetail.NewRow();
            DrGvItemDetail["PurchaseHeadName"] = ddlExpenseHead.SelectedItem.Text;
            DrGvItemDetail["ItemName"] = ddlItemName.SelectedItem.Text;
            DrGvItemDetail["ItemUnit"] = ddlUnitName.SelectedItem.Text;
            DrGvItemDetail["ItemSecondaryUnit"] = ddlMinorUnit.SelectedValue;
            DrGvItemDetail["PurchaseCode"] = Convert.ToInt64(ddlExpenseHead.SelectedValue);
            DrGvItemDetail["ItemID"] = Convert.ToInt32(ddlItemName.SelectedValue);
            DrGvItemDetail["HSNSACCode"] = dtItems != null ? dtItems.Rows[0]["HSNSACCode"] : hfHSNSACCode.Value;
            DrGvItemDetail["GoodsServiceInd"] = dtItems != null ? Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]) : Convert.ToInt32(hfGoodsAndServiceInd.Value);
            DrGvItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            DrGvItemDetail["ItemUnitID"] = Convert.ToInt32(ddlUnitName.SelectedValue);
            DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text);
            DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
            DrGvItemDetail["FreeQty"] = CommonCls.ConvertDecimalZero(txtFree.Text);
            DrGvItemDetail["ItemRate"] = CommonCls.ConvertDecimalZero(txtRate.Text);
            DrGvItemDetail["ItemAmount"] = CommonCls.ConvertDecimalZero(txtItemAmt.Text);

            DrGvItemDetail["DiscountValue"] = CommonCls.ConvertDecimalZero(txtDiscount.Text);
            DrGvItemDetail["DiscountType"] = ddlDiscount.SelectedValue;
            DrGvItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(hfDiscountAmount.Value);

            DrGvItemDetail["NetAmt"] = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);

            if (ddlEdit.SelectedValue == "1")
            {
                DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(txtTaxRate.Text);
                DrGvItemDetail["IGSTTaxAmt"] = CommonCls.ConvertDecimalZero(txtIGSTAmt.Text);
                DrGvItemDetail["SGSTTaxAmt"] = CommonCls.ConvertDecimalZero(txtSGSTAmt.Text);
                DrGvItemDetail["CGSTTaxAmt"] = CommonCls.ConvertDecimalZero(txtCGSTAmt.Text);
                DrGvItemDetail["CESSTaxAmt"] = CommonCls.ConvertDecimalZero(txtCESSAmt.Text);
                TaxCal();
            }
            else
            {
                TaxCal();

                if (VsdtItems.Rows[0]["ItemMinorUnitID"].ToString() == "")
                    DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());
                else
                    DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(txtTaxRate.Text);

                DrGvItemDetail["IGSTTaxAmt"] = IgstAmt;
                DrGvItemDetail["SGSTTaxAmt"] = SgstAmt;
                DrGvItemDetail["CGSTTaxAmt"] = CgstAmt;
                DrGvItemDetail["CESSTaxAmt"] = CessAmt;
            }

            DrGvItemDetail["IGSTTax"] = IgstRat;
            DrGvItemDetail["SGSTTax"] = SgstRat;
            DrGvItemDetail["CGSTTax"] = CgstRat;
            DrGvItemDetail["CESSTax"] = CessRat;
            DrGvItemDetail["ITCApplicable"] = Convert.ToInt16(ddlITC.SelectedValue);
            DrGvItemDetail["ITCIGSTAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? IgstAmt : 0;
            DrGvItemDetail["ITCSGSTAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? SgstAmt : 0;
            DrGvItemDetail["ITCCGSTAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? CgstAmt : 0;
            DrGvItemDetail["ITCCESSAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? CessAmt : 0;
            DrGvItemDetail["FreeItemInd"] = 0;
            DrGvItemDetail["ItemRemark"] = txtItemRemark.Text;
            DrGvItemDetail["ItemInd"] = Convert.ToInt16(ddlEdit.SelectedValue);
            DrGvItemDetail["ItemInd2"] = "";
            DrGvItemDetail["ItemInd3"] = 0;

            if (btnAddItemDetail.Text == "Update")
            {
                btnAddItemDetail.Text = "Add";
            }

            dtGvItemDetail.Rows.Add(DrGvItemDetail);
            grdItemDetails.DataSource = VsdtGvItemDetail = dtGvItemDetail;
            grdItemDetails.DataBind();
        }
    }

    protected void grdItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblITC = (Label)e.Row.FindControl("lblITC");
                if (lblITC.Text == "1")
                    lblITC.Text = "Yes";
                else
                    lblITC.Text = "No";

                Label lblEdit = (Label)e.Row.FindControl("lblEdit");
                if (lblEdit.Text == "1")
                    lblEdit.Text = "Yes";
                else
                    lblEdit.Text = "No";

                Label lblMinorUnit = (Label)e.Row.FindControl("lblMinorUnitID");
                if (CommonCls.ConvertIntZero(lblMinorUnit.Text) == 0)
                    lblMinorUnit.Text = "";
                else
                {
                    ddlMinorUnit.SelectedValue = lblMinorUnit.Text;
                    lblMinorUnit.Text = ddlMinorUnit.SelectedItem.Text;
                }

                Label lblDiscountType = (Label)e.Row.FindControl("lblDiscountType");
                ddlDiscount.SelectedValue = lblDiscountType.Text;
                lblDiscountType.Text = ddlDiscount.SelectedItem.Text;
            }
        }
    }

    void ClearItemDetailTable()
    {
        ddlPayMode.Enabled = false;

        ddlExpenseHead.SelectedValue = ddlItemName.SelectedValue = ddlUnitName.SelectedValue = ddlMinorUnit.SelectedValue = "0";
        ddlITC.ClearSelection();
        ddlEdit.ClearSelection();
        txtItemTaxableAmt.Enabled = txtTaxRate.Enabled = txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = false;

        txtQty.Text = txtFree.Text = txtRate.Text = txtItemAmt.Text =
        txtDiscount.Text = txtItemTaxableAmt.Text = txtTaxRate.Text =
        txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text =
        txtItemRemark.Text = txtMinorUnitQty.Text = "";

        VsdtItems = null;
    }

    /// <summary>
    /// Sundrie Grid Operations
    /// </summary>
    protected void gvotherCharge_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtGrdOtherCharge = VsdtSundri;
                dtGrdOtherCharge.Rows[rowIndex].Delete();

                VsdtSundri = dtGrdOtherCharge;
                gvotherCharge.DataSource = dtGrdOtherCharge;
                gvotherCharge.DataBind();
                CalculateItemTotalAmount();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlHeadName.SelectedValue == "")
        {
            ShowMessage("Select Head Name.", false, lblMsg);
            ddlHeadName.Focus();
            return;
        }
        if (Convert.ToInt32(ddlHeadName.SelectedValue) == 0)
        {
            ShowMessage("Select Head Name.", false, lblMsg);
            ddlHeadName.Focus();
            return;
        }
        if (ddlAddLess.SelectedValue == "0")
        {
            ShowMessage("Select Add Less", false, lblMsg);
            ddlAddLess.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtOtherChrgAmount.Text) || Convert.ToDecimal(txtOtherChrgAmount.Text) <= 0)
        {
            ShowMessage("Enter Other Charges Amount.", false, lblMsg);
            txtOtherChrgAmount.Focus();
            return;
        }

        if (VsdtSundri != null) // First Time DataTable Create For Grid
        {
            dtGrdOtherCharge = VsdtSundri;
            DataRow[] rows = dtGrdOtherCharge.Select("SundriCode=" + ddlHeadName.SelectedValue);
            if (rows.Count() >= 1)
            {
                ddlHeadName.Focus();
                ShowMessage("This Charge Already.", false, lblMsg);
                return;
            }
        }

        dtGrdOtherCharge = new DataTable();
        if (VsdtSundri == null) // First Time DataTable Create For Grid
        {
            dtGrdOtherCharge = DtSundriesSchema();
        }
        else
        {
            dtGrdOtherCharge = VsdtSundri;
        }
        DataRow dr = dtGrdOtherCharge.NewRow();
        dr["SundriHead"] = ddlHeadName.SelectedItem.Text;
        dr["SundriCode"] = ddlHeadName.SelectedValue;
        dr["SundriInd"] = ddlAddLess.SelectedItem.Text;
        dr["SundriAmt"] = txtOtherChrgAmount.Text;
        dtGrdOtherCharge.Rows.Add(dr);
        VsdtSundri = dtGrdOtherCharge;
        gvotherCharge.DataSource = dtGrdOtherCharge;
        gvotherCharge.DataBind();
        ClearOtherCharge();
        CalculateItemTotalAmount();
        ddlHeadName.Focus();
    }

    void ClearOtherCharge()
    {
        ddlHeadName.ClearSelection();
        txtOtherChrgAmount.Text = "";
        ddlAddLess.ClearSelection();
    }

    void CalculateItemTotalAmount()
    {
        var cal = CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);
        txtGross.Text = cal.TotalGross.ToString();
        txtTax.Text = cal.TotalTaxable.ToString();
        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtNet.Text = cal.TotalAllNet.ToString();
        CalCulateBrokerAmount();
    }

    public static CalculateAll CalculateTotalAmount(DataTable dtItems, DataTable dtSundri)
    {
        CalculateAll cal = new CalculateAll();
        if (dtItems != null)
        {
            DataTable dtGrdItems = (DataTable)(dtItems);

            foreach (DataRow item in dtGrdItems.Rows)
            {
                cal.IGST += CommonCls.ConvertDecimalZero(item["IGSTTaxAmt"].ToString());
                cal.SGST += CommonCls.ConvertDecimalZero(item["SGSTTaxAmt"].ToString());
                cal.CGST += CommonCls.ConvertDecimalZero(item["CGSTTaxAmt"].ToString());
                cal.CESS += CommonCls.ConvertDecimalZero(item["CESSTaxAmt"].ToString());
                cal.ItemAmount += CommonCls.ConvertDecimalZero(item["ItemAmount"].ToString());
                cal.ItemTaxable += CommonCls.ConvertDecimalZero(item["NetAmt"].ToString());
                cal.ItemDiscount += CommonCls.ConvertDecimalZero(item["DiscountAmt"].ToString());
            }
        }

        decimal SundriAmt = 0;
        if (dtSundri != null)
        {
            foreach (DataRow item in dtSundri.Rows)
            {
                if (item["SundriInd"].ToString() == "Add") //For Sundri Amount Add
                    SundriAmt += CommonCls.ConvertDecimalZero(item["SundriAmt"].ToString());
                else if (item["SundriInd"].ToString() == "Less") //For Sundri Amount Less
                    SundriAmt -= CommonCls.ConvertDecimalZero(item["SundriAmt"].ToString());
            }
            cal.TotalSundriAddLess = SundriAmt;
        }
        cal.TotalGross = cal.ItemTaxable;
        //cal.TotalAllNet = (cal.TotalTaxable + cal.ItemTaxable + cal.TotalSundriAddLess);
        //cal.TotalGross = (cal.ItemAmount - cal.ItemDiscount);
        cal.TotalTaxable = (cal.IGST + cal.SGST + cal.CGST + cal.CESS);
        cal.TotalAllNet = (cal.TotalGross + cal.TotalTaxable + cal.TotalSundriAddLess);

        return cal;
    }

    /// <summary>
    /// Finally Button Update For Submit All Data.
    /// </summary>
    protected void btnUpdate_Click(object sender, EventArgs e) // Update Purchase Voucher
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            bool IsValid = ValidationBTNSAVE();
            if (!IsValid)
            {
                return;
            }

            objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
            objUpdPurchaseModel.Ind = 2;
            objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
            objUpdPurchaseModel.BrID = GlobalSession.BrID;
            objUpdPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            objUpdPurchaseModel.EntryType = 2;
            objUpdPurchaseModel.YrCD = GlobalSession.YrCD;
            objUpdPurchaseModel.User = GlobalSession.UserID;
            objUpdPurchaseModel.IP = GlobalSession.IP;

            objUpdPurchaseModel.ByCashSale = PayModeIsCredit ? 0 : 1;
            objUpdPurchaseModel.PartyName = PayModeIsCredit ? ddlPurchaseFrom.SelectedItem.Text.ToUpper() : txtPurchaseFrom.Text;
            objUpdPurchaseModel.PartyGSTIN = PayModeIsCredit && ddlGstinNo.SelectedItem != null ? ddlGstinNo.SelectedItem.Text.ToUpper() : txtGstinNo.Text.ToUpper();
            objUpdPurchaseModel.PartyAddress = PayModeIsCredit && ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : txtShippingAdd.Text.ToUpper();

            objUpdPurchaseModel.WareHouseID = Convert.ToInt32(ddlLocation.SelectedValue);
            objUpdPurchaseModel.PONo = txtOrderNo.Text;
            objUpdPurchaseModel.PODate = CommonCls.ConvertToDate(txtOrderDate.Text);
            objUpdPurchaseModel.VoucherNo = CommonCls.ConvertIntZero(txtSearchVoucher.Text);
            objUpdPurchaseModel.PurchaseSaleRecordID = Convert.ToInt64(hfPurchaseSaleRecordID.Value);
            objUpdPurchaseModel.AccCode = PayModeIsCredit ? CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) : Convert.ToInt32(ddlPayMode.SelectedValue);
            objUpdPurchaseModel.VoucherDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            objUpdPurchaseModel.PurchaseBillNo = txtBillNo.Text;
            objUpdPurchaseModel.PurchaseBillDate = CommonCls.ConvertToDate(txtBillDate.Text);
            objUpdPurchaseModel.GRNNo = txtGRNNo.Text;
            objUpdPurchaseModel.GRNDate = CommonCls.ConvertToDate(txtGRNDate.Text);

            objUpdPurchaseModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            if (!string.IsNullOrEmpty(txtIDARefNo.Text))
            {
                objUpdPurchaseModel.IDARefNo = "Old VNo-" + txtIDARefNo.Text;
            }

            objUpdPurchaseModel.BrokerageID = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
            objUpdPurchaseModel.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerRate.Text);
            objUpdPurchaseModel.BrokerageGSTIN = string.IsNullOrEmpty(ddlBrokerGSTIN.SelectedValue) ? "" : ddlBrokerGSTIN.SelectedValue;
            objUpdPurchaseModel.BrokerageAmount = CommonCls.ConvertDecimalZero(txtBrokerAmount.Text);

            //objUpdPurchaseModel.IsAudit = CommonCls.ConvertIntZero(Session["IsAudit"] == null ? 0 : Session["IsAudit"].ToString() == "1" ? 1 : Session["IsAudit"].ToString() == "2" ? 2 : 0);
            //CommonCls.ConvertIntZero(Session["IsAudit"] != null ? Session["IsAudit"].ToString() == "True" ? 1 : 0 : 0);

            objUpdPurchaseModel.DeptID = GlobalSession.DepartmentID;
            objUpdPurchaseModel.SubDeptID = GlobalSession.SubDeptID;
            objUpdPurchaseModel.IsAudit = CommonCls.ConvertIntZero(Session["IsAudit"] == null ? 0 : Session["IsAudit"].ToString() == "1" ? 1 : 0);
            objUpdPurchaseModel.IsSendToAudit = CommonCls.ConvertIntZero(Session["IsSendToAudit"] == null ? 0 : Session["IsSendToAudit"].ToString() == "1" ? 1 : 0);
            objUpdPurchaseModel.BankPayVoucherInd = CommonCls.ConvertIntZero(Session["PurchaseBankPayInd"] == null ? 0 : Session["PurchaseBankPayInd"].ToString() == "True" ? 1 : 0);

            objUpdPurchaseModel.DtUpdPurchase = DtPurchaseSchema();
            objUpdPurchaseModel.DtUpdItems = DtItemsSchema();

            objUpdPurchaseModel.DtUpdPurchase = CreatePurchaseData();
            if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            {
                foreach (DataRow item in VsdtGvFreeItem.Rows)
                {
                    VsdtGvItemDetail.Rows.Add(item.ItemArray);
                }
            }

            objUpdPurchaseModel.DtUpdItems = VsdtGvItemDetail;
            if (objUpdPurchaseModel.DtUpdItems != null)
            {
                if (objUpdPurchaseModel.DtUpdItems.Columns.Contains("ItemMinorUnitName"))
                {
                    objUpdPurchaseModel.DtUpdItems.Columns.Remove("ItemMinorUnitName");
                }

                if (objUpdPurchaseModel.DtUpdItems.Columns.Contains("CCCode"))
                {
                    objUpdPurchaseModel.DtUpdItems.Columns.Remove("CCCode");
                }
            }
            objUpdPurchaseModel.DtUpdSundries = VsdtSundri;
            if ((objUpdPurchaseModel.DtUpdSundries == null) || (objUpdPurchaseModel.DtUpdSundries.Rows.Count == 0))
            {
                objUpdPurchaseModel.DtUpdSundries = DtSundriesSchema();
                DataRow drSaleSundri = objUpdPurchaseModel.DtUpdSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                objUpdPurchaseModel.DtUpdSundries.Rows.Add(drSaleSundri);
            }

            string uri = string.Format("UpdatePurchaseVoucher/UpdatePV");
            DataTable dtUpdPurchase = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
            if (dtUpdPurchase.Rows.Count > 0)
            {
                if (dtUpdPurchase.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    string VoucherNo, VoucherDate;//, InvoiceName;
                    VoucherNo = dtUpdPurchase.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtUpdPurchase.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                    ShowMessage("Record Update successfully.", true, lblMsg);
                    txtVoucherDate.Focus();

                    CallReport(VoucherNo, CommonCls.ConvertToDate(VoucherDate));
                    //btnBack_Click(sender, e);
                }
                else if (dtUpdPurchase.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Bill No. Duplicate.", false, lblMsg);
                    txtVoucherDate.Focus();
                }
            }
            else
            {
                ShowMessage("Record Not Save Please Try Again.", false, lblMsg);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Internal Server Error!", false, lblMsg);
        }

    }

    bool ValidationBTNSAVE()
    {
        if (PayModeIsCredit)
        {
            if (CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) == 0)
            {
                ddlPurchaseFrom.Focus();
                ShowMessage("Select Purchase From.", false, lblMsg);
                return false;
            }
        }

        if (!PayModeIsCredit)
        {
            if (string.IsNullOrEmpty(txtPurchaseFrom.Text))
            {
                txtPurchaseFrom.Focus();
                ShowMessage("Enter Purchase From.", false, lblMsg);
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtVoucherDate.Text))
        {
            txtVoucherDate.Focus();
            ShowMessage("Please Enter Transaction Date.", false, lblMsg);
            return false;
        }

        if (string.IsNullOrEmpty(txtBillNo.Text)) // Invoice Number Shouldn't be Null
        {
            txtBillNo.Focus();
            ShowMessage("Please Enter Bill No.", false, lblMsg);
            return false;
        }

        if (string.IsNullOrEmpty(txtBillDate.Text))
        {
            txtBillDate.Focus();
            ShowMessage("Enter Bill Date.", false, lblMsg);
            return false;
        }

        if (GlobalSession.CCCode == 1)
        {
            if (ddlCostCenter.SelectedValue == "0")
            {
                ShowMessage("Select Cost Centre!", false, lblMsg);
                ddlCostCenter.Focus();
                return false;
            }
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtBillDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtBillDate.Focus();
            ShowMessage("Bill Date Should Not Be More Than Todays Date!", false, lblMsg);
            return false;
        }

        if (!string.IsNullOrEmpty(txtOrderDate.Text))
        {
            bool OrderDate = CommonCls.CheckFinancialYrDate(txtOrderDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Should Not Be More Than Todays Date!", false, lblMsg);
                return false;
            }

            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                txtOrderNo.Focus();
                ShowMessage("Order No. Compulsory If Order Date Not Empty.", false, lblMsg);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtOrderNo.Text))
        {
            if (string.IsNullOrEmpty(txtOrderDate.Text))
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Compulsory If Order No. Not Empty.", false, lblMsg);
                return false;
            }

            bool OrderDate = CommonCls.CheckFinancialYrDate(txtOrderDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Should Not Be More Than Todays Date!", false, lblMsg);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtGRNDate.Text))
        {
            bool OrderDate = CommonCls.CheckFinancialYrDate(txtGRNDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtGRNDate.Focus();
                ShowMessage("GRN Date Should Not Be More Than Todays Date!", false, lblMsg);
                return false;
            }

            if (string.IsNullOrEmpty(txtGRNNo.Text))
            {
                txtGRNNo.Focus();
                ShowMessage("GRN No. Compulsory If GRN Date Not Empty.", false, lblMsg);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtGRNNo.Text))
        {
            if (string.IsNullOrEmpty(txtGRNDate.Text))
            {
                txtGRNDate.Focus();
                ShowMessage("GRN Date Compulsory If GRN No. Not Empty.", false, lblMsg);
                return false;
            }

            bool OrderDate = CommonCls.CheckFinancialYrDate(txtGRNDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtGRNDate.Focus();
                ShowMessage("GRN Date Should Not Be More Than Todays Date!", false, lblMsg);
                return false;
            }
        }

        if (ddlLocation.Items.Count > 1)
        {
            if (ddlLocation.SelectedValue == "0" || ddlLocation.SelectedValue == "")
            {
                ddlLocation.Focus();
                ShowMessage("Select Location.", false, lblMsg);
                return false;
            }
        }
        if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
        {
            ShowMessage("Item Detail Can Not Be Null.", false, lblMsg);
            return false;
        }
        if (string.IsNullOrEmpty(txtTax.Text) || Convert.ToDecimal(txtTax.Text) < 0)
        {
            ShowMessage("Taxable Amount Can Not Be Negative Please Check Entry.", false, lblMsg);
            return false;
        }
        if (string.IsNullOrEmpty(txtNet.Text) || Convert.ToDecimal(txtNet.Text) < 0)
        {
            ShowMessage("Net Amount Can Not Be Negative Please Check Entry.", false, lblMsg);
            return false;
        }
        if (string.IsNullOrEmpty(txtGross.Text) || Convert.ToDecimal(txtGross.Text) < 0)
        {
            ShowMessage("Gross Amount Can Not Be Negative Please Check Entry.", false, lblMsg);
            return false;
        }

        if (PayModeIsCredit)
        {
            if (ddlGstinNo.SelectedValue.ToUpper() != "")
            {
                bool GSTINNumber = CheckGSTINNumber_Validation();
                if (GSTINNumber == false)
                {
                    ShowMessage("Invalid GSTIN No.", false, lblMsg);
                    return false;
                }
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(txtGstinNo.Text) && !CommonCls.validGSTIN(txtGstinNo.Text))
            {
                ShowMessage("Invalid GSTIN No.", false, lblMsg);
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtNarration.Text))
        {
            txtNarration.Focus();
            ShowMessage("Enter Narration.", false, lblMsg);
            return false;
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

                objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
                objUpdPurchaseModel.Ind = 6;
                objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
                objUpdPurchaseModel.BrID = GlobalSession.BrID;
                objUpdPurchaseModel.AccCode = Convert.ToInt32(ddlPurchaseFrom.SelectedItem.Value);



                string uri = string.Format("PurchaseVoucher/CheckGSTIN_Number");

                DataSet dtStatePanNo = CommonCls.ApiPostDataSet(uri, objUpdPurchaseModel);
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

            ShowMessage(ex.Message, false, lblMsg);
        }
        return false;
    }

    DataTable CreatePurchaseData()
    {
        DataTable dtCreatePurchaseData = new DataTable();
        try
        {
            dtCreatePurchaseData = DtPurchaseSchema();
            DataRow drCreatePurchaseData = dtCreatePurchaseData.NewRow();

            drCreatePurchaseData["AccCode"] = PayModeIsCredit ? CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) : Convert.ToInt32(ddlPayMode.SelectedValue);

            drCreatePurchaseData["AccGst"] = ddlGstinNo.SelectedItem != null ? !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ? ddlGstinNo.SelectedValue : "" : "";
            drCreatePurchaseData["SalePurchaseCode"] = 0;

            if (ddlShippingAdd.SelectedItem != null && CommonCls.ConvertIntZero(ddlShippingAdd.SelectedValue) != 0)
            {
                drCreatePurchaseData["AccPOSID"] = Convert.ToInt32(ddlShippingAdd.SelectedValue);
            }
            drCreatePurchaseData["GSTIN"] = ddlGstinNo.SelectedItem != null ? !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ? ddlGstinNo.SelectedValue : "" : "";
            drCreatePurchaseData["WarehouseID"] = ddlLocation.SelectedItem != null ? Convert.ToInt32(ddlLocation.SelectedValue) : 0;
            drCreatePurchaseData["OrderNo"] = 0;
            drCreatePurchaseData["OrderDate"] = !string.IsNullOrEmpty(txtOrderDate.Text) ? CommonCls.ConvertToDate(txtOrderDate.Text) : "";
            drCreatePurchaseData["InvoiceNo"] = 0;
            drCreatePurchaseData["InvoiceDate"] = "";
            if (!string.IsNullOrEmpty(txtGRNNo.Text))
            {
                drCreatePurchaseData["GRNNo"] = 0;
                drCreatePurchaseData["GRNDate"] = CommonCls.ConvertToDate(txtGRNDate.Text);
            }
            drCreatePurchaseData["TDSApplicable"] = Convert.ToInt32(ddlTds.SelectedValue);
            drCreatePurchaseData["TCSApplicable"] = Convert.ToInt32(ddlTCS.SelectedValue);
            drCreatePurchaseData["RCMApplicable"] = Convert.ToInt32(ddlRCM.SelectedValue);
            drCreatePurchaseData["GrossAmt"] = Convert.ToDecimal(txtGross.Text);
            drCreatePurchaseData["TaxAmt"] = Convert.ToDecimal(txtTax.Text);
            drCreatePurchaseData["NetAmt"] = Convert.ToDecimal(txtNet.Text);
            drCreatePurchaseData["RoundOffAmt"] = 0;
            drCreatePurchaseData["TransportID"] = 0;
            drCreatePurchaseData["VehicleNo"] = "";
            drCreatePurchaseData["WayBillNo"] = 0;
            drCreatePurchaseData["TransportDate"] = "";
            drCreatePurchaseData["DocDesc"] = txtNarration.Text;


            dtCreatePurchaseData.Rows.Add(drCreatePurchaseData);
        }
        catch (Exception ex) { }
        return dtCreatePurchaseData;

    }

    DataTable DtPurchaseSchema()
    {
        DataTable dtPurchase = new DataTable();

        dtPurchase.Columns.Add("AccCode", typeof(int));
        dtPurchase.Columns.Add("AccGst", typeof(string));
        dtPurchase.Columns.Add("SalePurchaseCode", typeof(int));
        dtPurchase.Columns.Add("AccPOSID", typeof(int));
        dtPurchase.Columns.Add("GSTIN", typeof(string));
        dtPurchase.Columns.Add("WareHouseID", typeof(int));
        dtPurchase.Columns.Add("OrderNo", typeof(int));
        dtPurchase.Columns.Add("OrderDate", typeof(string));
        dtPurchase.Columns.Add("InvoiceNo", typeof(int));
        dtPurchase.Columns.Add("InvoiceDate", typeof(string));
        dtPurchase.Columns.Add("GRNNo", typeof(int));
        dtPurchase.Columns.Add("GRNDate", typeof(string));
        dtPurchase.Columns.Add("TDSApplicable", typeof(int));
        dtPurchase.Columns.Add("TCSApplicable", typeof(int));
        dtPurchase.Columns.Add("RCMApplicable", typeof(int));
        dtPurchase.Columns.Add("GrossAmt", typeof(decimal));
        dtPurchase.Columns.Add("TaxAmt", typeof(decimal));
        dtPurchase.Columns.Add("NetAmt", typeof(decimal));
        dtPurchase.Columns.Add("RoundOffAmt", typeof(decimal));
        dtPurchase.Columns.Add("TransportID", typeof(int));
        dtPurchase.Columns.Add("VehicleNo", typeof(string));
        dtPurchase.Columns.Add("WayBillNo", typeof(int));
        dtPurchase.Columns.Add("TransportDate", typeof(string));
        dtPurchase.Columns.Add("DocDesc", typeof(string));

        return dtPurchase;
    }

    DataTable DtSundriesSchema()
    {
        DataTable dtSundries = new DataTable();

        dtSundries.Columns.Add("SundriCode", typeof(int));
        dtSundries.Columns.Add("SundriHead", typeof(string));
        dtSundries.Columns.Add("SundriInd", typeof(string));
        dtSundries.Columns.Add("SundriAmt", typeof(decimal));
        return dtSundries;
    }

    DataTable DtItemsSchema()
    {
        DataTable dtItems = new DataTable();

        dtItems.Columns.Add("PurchaseHeadName", typeof(string));
        dtItems.Columns.Add("ItemName", typeof(string));
        dtItems.Columns.Add("ItemUnit", typeof(string));
        dtItems.Columns.Add("ItemSecondaryUnit", typeof(string));

        dtItems.Columns.Add("PurchaseCode", typeof(int));
        dtItems.Columns.Add("ItemID", typeof(long));
        dtItems.Columns.Add("HSNSACCode", typeof(string));
        dtItems.Columns.Add("GoodsServiceInd", typeof(int));
        dtItems.Columns.Add("ItemQty", typeof(decimal));
        dtItems.Columns.Add("ItemUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorQty", typeof(decimal));
        dtItems.Columns.Add("ItemMinorUnitID", typeof(int));
        dtItems.Columns.Add("FreeQty", typeof(decimal));
        dtItems.Columns.Add("ItemRate", typeof(decimal));
        dtItems.Columns.Add("ItemAmount", typeof(decimal));
        dtItems.Columns.Add("DiscountValue", typeof(decimal));
        dtItems.Columns.Add("DiscountType", typeof(int));
        dtItems.Columns.Add("DiscountAmt", typeof(decimal));
        dtItems.Columns.Add("NetAmt", typeof(decimal));
        dtItems.Columns.Add("TaxRate", typeof(decimal));
        dtItems.Columns.Add("IGSTTax", typeof(decimal));
        dtItems.Columns.Add("IGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("SGSTTax", typeof(decimal));
        dtItems.Columns.Add("SGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("CGSTTax", typeof(decimal));
        dtItems.Columns.Add("CGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("CESSTax", typeof(decimal));
        dtItems.Columns.Add("CESSTaxAmt", typeof(decimal));

        dtItems.Columns.Add("ITCApplicable", typeof(int));
        //dtItems.Columns.Add("Edit", typeof(int));
        dtItems.Columns.Add("ITCIGSTAmt", typeof(decimal));
        dtItems.Columns.Add("ITCSGSTAmt", typeof(decimal));
        dtItems.Columns.Add("ITCCGSTAmt", typeof(decimal));
        dtItems.Columns.Add("ITCCESSAmt", typeof(decimal));

        dtItems.Columns.Add("FreeItemInd", typeof(int));
        dtItems.Columns.Add("ItemRemark", typeof(string));

        dtItems.Columns.Add("ItemInd", typeof(int));
        dtItems.Columns.Add("ItemInd2", typeof(string));
        dtItems.Columns.Add("ItemInd3", typeof(decimal));

        return dtItems;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        txtSearchVoucher.Focus();
        //ddlPayMode.Focus();
    }

    void ClearAll()
    {
        txtIDARefNo.Text = "";
        divFree.Visible = false;
        ddlPayMode.ClearSelection();
        gvFreeItem.DataSource = VsdtGvFreeItem = null;
        gvFreeItem.DataBind();


        ddlExpenseHead.ClearSelection();
        ddlPurchaseFrom.ClearSelection();

        ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        ddlLocation.ClearSelection();
        ddlGstinNo.DataSource = ddlShippingAdd.DataSource = new DataTable();
        ddlGstinNo.DataBind(); ddlShippingAdd.DataBind();

        txtBillNo.Text = txtBillDate.Text = txtOrderNo.Text = txtOrderDate.Text = txtVoucherDate.Text = "";
        ddlTds.ClearSelection();
        ddlTCS.ClearSelection();
        ddlRCM.ClearSelection();

        ddlItemName.ClearSelection();
        ddlUnitName.ClearSelection();

        txtGRNNo.Text = txtGRNDate.Text = "";

        txtFree.Text = txtQty.Text = txtRate.Text = txtItemAmt.Text = txtDiscount.Text = txtTax.Text = txtItemTaxableAmt.Text =
            txtTaxRate.Text = txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text = "";
        txtItemRemark.Text = "";

        VsdtGvItemDetail = VsdtSundri = null;
        grdItemDetails.DataSource = gvotherCharge.DataSource = new DataTable();
        grdItemDetails.DataBind(); gvotherCharge.DataBind();

        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";
        ddlMinorUnit.ClearSelection();

        txtGross.Text = txtTax.Text = txtAddLess.Text = txtNet.Text = "0";
        txtNarration.Text = "";

        txtPurchaseFrom.Text = txtShippingAdd.Text = txtGstinNo.Text = txtBrokerRate.Text = txtBrokerAmount.Text = "";

        //ddlPurchaseFrom.Enabled = ddlGstinNo.Enabled = false;
        //txtPurchaseFrom.Enabled = txtGstinNo.Enabled = false;

        PayModeInit();

        //txtVoucherDate.Focus();
        txtSearchVoucher.Focus();
        txtSearchVoucher.Enabled = btnSearchVoucher.Enabled = true;
        txtSearchVoucher.Text = "";
        btnAddItemDetail.Enabled = btnFreeAdd.Enabled = btnAdd.Enabled = btnUpdate.Enabled = false;
        btnAddItemDetail.Text = "Add";
        pnlBroker.Visible = false;
        ddlBroker.ClearSelection();
        ddlBrokerGSTIN.ClearSelection();
        ddlBrokerGSTIN.Enabled = false;
        txtBrokerAmount.Enabled = false;
        txtBrokerRate.Enabled = false;
        ddlCostCenter.ClearSelection();
    }

    public void CallReport(string InvoiceNo, string InvoiceDate)
    {

        Hashtable HT = new Hashtable();
        HT.Add("Ind", 2);

        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", 0);//GlobalSession.YrCD);
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "PURCHASE VOUCHER");

        HT.Add("Doctype", 5);
        HT.Add("invoiceno", Convert.ToInt32(InvoiceNo));
        HT.Add("invoiceDate", InvoiceDate);
        HT.Add("invoiceDateFrom", "");
        HT.Add("invoiceDateto", "");
        HT.Add("cashsalesind", 1);

        VouchersReport.ReportName = "RptPurchaseVoucher";
        VouchersReport.FileName = "PurchaseVoucher";
        VouchersReport.ReportHeading = "Purchase Transaction";
        VouchersReport.HashTable = HT;

        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print?";

        VouchersReport.ShowReport();

        Session["DocTypeID"] = Convert.ToInt16(ViewState["VchType"]);
    }

    #region Free Item Qty Add

    protected void btnShowFreeItem_Click(object sender, EventArgs e)
    {
        if (divFree.Visible)
            divFree.Visible = false;
        else
            divFree.Visible = true;

        btnShowFreeItem.Focus();
    }

    protected void ddlFreeItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearItemDetailTable();

        objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
        objUpdPurchaseModel.Ind = 11;
        objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
        objUpdPurchaseModel.BrID = GlobalSession.BrID;
        objUpdPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objUpdPurchaseModel.PartyCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue);
        objUpdPurchaseModel.ByCashSale = PayModeIsCredit ? 0 : 1;
        objUpdPurchaseModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);

        if (ddlGstinNo.SelectedItem != null)
        {
            if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
            {
                objUpdPurchaseModel.GSTIN = ddlGstinNo.SelectedValue;
            }
        }

        if (PayModeIsCredit)
        {
            objUpdPurchaseModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();
        }

        string uri = string.Format("UpdatePurchaseVoucher/FillItemPurchaseRate");
        DataSet dsItems = CommonCls.ApiPostDataSet(uri, objUpdPurchaseModel);
        if (dsItems.Tables.Count > 0)
        {
            VsdtItems = dsItems.Tables[0];
            ddlFreeUnit.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
        }
        txtFreeQty.Focus();
    }

    protected void btnFreeAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
            {
                ClearFreeItem();
                ShowMessage("Insert Item Detail First.", false, lblMsg);
                ddlExpenseHead.Focus();
                return;
            }

            if (CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue) == 0)
            {
                ShowMessage("Select Item Name First.", false, lblMsg);
                ddlFreeItemName.Focus();
                return;
            }
            if (CommonCls.ConvertIntZero(ddlFreeUnit.SelectedValue) == 0)
            {
                ShowMessage("Select Item Name First.", false, lblMsg);
                ddlFreeItemName.Focus();
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtFreeQty.Text) == 0)
            {
                ShowMessage("Enter Free Qty.", false, lblMsg);
                txtFreeQty.Focus();
                return;
            }

            if (VsdtGvFreeItem == null)
            {
                VsdtGvFreeItem = VsdtGvItemDetail.Clone();
            }

            if (VsdtGvFreeItem.Rows.Count > 0)
            {
                DataRow[] DuplicateRow = VsdtGvFreeItem.Select("ItemID='" + ddlFreeItemName.SelectedValue + "'");
                if (DuplicateRow.Count() == 1)
                {
                    ShowMessage("This Item Already Exist.", false, lblMsg);
                    ddlFreeItemName.Focus();
                    return;
                }
            }

            DataRow drItem = VsdtGvFreeItem.NewRow();

            drItem["ItemID"] = ddlFreeItemName.SelectedValue;
            drItem["ItemName"] = ddlFreeItemName.SelectedItem.Text;
            drItem["ItemUnitID"] = ddlFreeUnit.SelectedValue;
            drItem["ItemUnit"] = ddlFreeUnit.SelectedItem.Text;
            drItem["PurchaseCode"] = 0;
            drItem["ItemQty"] = CommonCls.ConvertDecimalZero(txtFreeQty.Text);
            drItem["FreeItemInd"] = 1;
            drItem["GoodsServiceInd"] = VsdtItems.Rows[0]["GoodsServiceIndication"];
            drItem["HSNSACCode"] = VsdtItems.Rows[0]["HSNSACCode"];
            VsdtGvFreeItem.Rows.Add(drItem);
            gvFreeItem.DataSource = VsdtGvFreeItem;
            gvFreeItem.DataBind();
            ClearFreeItem();
            ddlFreeItemName.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }

    protected void gvFreeItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveRow")
        {
            VsdtGvFreeItem.Rows[rowIndex].Delete();
            gvFreeItem.DataSource = VsdtGvFreeItem;
            gvFreeItem.DataBind();
            ddlFreeItemName.Focus();
        }
    }

    void ClearFreeItem()
    {
        ddlFreeItemName.ClearSelection();
        ddlFreeUnit.ClearSelection();
        txtFreeQty.Text = "";
        VsdtItems = null;
    }

    #endregion
    protected void ddlEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlEdit.SelectedValue) == 0)
        {
            txtItemTaxableAmt.Enabled = txtTaxRate.Enabled = txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = false;
            txtFree_TextChanged(sender, e);

            if (Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString() == "0")
            {
                txtTaxRate.Text = VsdtItems.Rows[0]["TaxRate"].ToString();
            }
            else
            {
                if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"].ToString()) == 1)
                {
                    TaxWithInRange();
                }
                else
                    FillTaxRate(VsdtItems.Rows[0]);
            }

            TaxCal();
        }
        else
        {
            txtItemTaxableAmt.Enabled = txtTaxRate.Enabled = txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = true;
        }
        ddlEdit.Focus();
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
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCancelReason.SelectedValue == "")
            {
                ShowMessage("Enter Cancel Reason", false, lblCancelMsg);
                pnlConfirmInvoice.Visible = true;
                return;
            }
            objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
            objUpdPurchaseModel.Ind = 3;
            objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
            objUpdPurchaseModel.BrID = GlobalSession.BrID;
            objUpdPurchaseModel.VchType = 5;
            objUpdPurchaseModel.VoucherNo = Convert.ToInt32(txtSearchVoucher.Text);
            objUpdPurchaseModel.CancelReason = ddlCancelReason.SelectedItem.Text;

            string uri = string.Format("UpdatePurchaseVoucher/CancelVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, objUpdPurchaseModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ClearAll();
                    //pnlConfirmInvoice.Visible = false;
                    hypBtnBack.Visible = true;
                    ShowMessage("Transaction No. - " + objUpdPurchaseModel.VoucherNo + " is Cancel successfully ", true, lblCancelMsg);
                }
                else if (dtSave.Rows[0]["CancelInd"].ToString() == "0")
                {
                    //pnlConfirmInvoice.Visible = false;
                    hypBtnBack.Visible = true;
                    ShowMessage("Transaction No. is not Cancel Please Try Again. ", true, lblCancelMsg);
                    //ClearAll();
                }
            }
            else
            {
                ShowMessage("Transaction No. Is Not Cancelled", false, lblCancelMsg);
            }
        }
        catch (Exception)
        {
            ShowMessage("Record Not Cancel Please Try Again.", false, lblCancelMsg);
        }

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;

    }

    #region Broker
    protected void btnShowBroker_Click(object sender, EventArgs e)
    {
        if (pnlBroker.Visible)
            pnlBroker.Visible = false;
        else
            pnlBroker.Visible = true;
    }

    protected void ddlBroker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrokerAmount.Text = txtBrokerRate.Text = "";
            if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
            {
                ShowMessage("Item Can Not Be Null.", false, lblMsg);
                ddlBroker.ClearSelection();
                return;
            }
            if (ddlBroker.SelectedValue != "0")
            {
                objUpdPurchaseModel = new UpdatePurchaseVoucherModel();
                objUpdPurchaseModel.Ind = 7;
                objUpdPurchaseModel.OrgID = GlobalSession.OrgID;
                objUpdPurchaseModel.BrID = GlobalSession.BrID;
                objUpdPurchaseModel.AccCode = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
                string uri = string.Format("SalesVauchar/FillBrokerDetail");
                DataSet dsBrokerGSTINDetail = CommonCls.ApiPostDataSet(uri, objUpdPurchaseModel);
                if (dsBrokerGSTINDetail.Tables.Count > 0)
                {
                    DataTable dtBrokerDetail = dsBrokerGSTINDetail.Tables[0];
                    DataTable dtBrokerGSTINDetail = dsBrokerGSTINDetail.Tables[1];
                    if (dtBrokerDetail.Rows.Count > 0)
                    {
                        txtBrokerRate.Text = dtBrokerDetail.Rows[0]["BrokerageRate"].ToString();
                        txtBrokerRate.Enabled = true;
                        txtBrokerAmount.Enabled = true;
                        CalCulateBrokerAmount();

                        if (dtBrokerDetail.Rows[0]["BrokerageType"].ToString() == "2")
                        {
                            txtBrokerRate.Enabled = true;

                        }
                        else
                        {
                            txtBrokerRate.Enabled = false;


                        }
                    }
                    #region GSTIN
                    if (dtBrokerGSTINDetail.Rows.Count > 0)
                    {
                        if (dtBrokerGSTINDetail.Rows.Count > 1)
                        {
                            ddlBrokerGSTIN.DataSource = dtBrokerGSTINDetail;
                            ddlBrokerGSTIN.DataValueField = "GSTIN";
                            ddlBrokerGSTIN.DataBind();
                            ddlBrokerGSTIN.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                            ddlBrokerGSTIN.SelectedIndex = 0;
                            ddlBrokerGSTIN.Focus();

                        }
                        else
                        {
                            ddlBrokerGSTIN.DataSource = dtBrokerGSTINDetail;
                            ddlBrokerGSTIN.DataValueField = "GSTIN";
                            ddlBrokerGSTIN.DataBind();
                            ddlBrokerGSTIN.Focus();
                        }
                        ddlBrokerGSTIN.Enabled = true;
                    }
                    else
                    {
                        ddlBrokerGSTIN.DataSource = dtBrokerGSTINDetail;
                        ddlBrokerGSTIN.DataBind();
                        ddlBrokerGSTIN.Focus();
                    }
                    #endregion
                }
            }
            else
            {
                txtBrokerAmount.Enabled = ddlBrokerGSTIN.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }

    private void CalCulateBrokerAmount()
    {
        if (ddlBroker.SelectedValue != "0")
        {
            decimal BrokerAmount = ((CommonCls.ConvertDecimalZero(txtBrokerRate.Text) * CommonCls.ConvertDecimalZero(txtGross.Text)) / 100);
            txtBrokerAmount.Text = BrokerAmount.ToString();
        }
    }
    protected void txtBokerRate_TextChanged(object sender, EventArgs e)
    {
        CalCulateBrokerAmount();
    }
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TransNo"] = Session["TransDate"] = null;
        Response.Redirect("../Vouchers/FrmPendingVouchers.aspx");
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {
        lblTransactionNo.Text = txtSearchVoucher.Text;
        lblTransactionDate.Text = txtVoucherDate.Text;
        lblDocumentType.Text = "Purchase";
        lblNarration.Text = Narration;

        pnlApproval.Visible = true;
        pnlConfirmInvoice.Visible = false;
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = txtApprovalDate.Text = txtApprovalRemark.Text = "";
        pnlApproval.Visible = false;
    }

    protected void btnApprovedYes_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = "";

        if (string.IsNullOrEmpty(txtApprovalDate.Text))
        {
            txtApprovalDate.Focus();
            ShowMessage("Enter Approval Date.", false, lblApprovalMSG);
            return;
        }

        if (Convert.ToDateTime(txtApprovalDate.Text) < Convert.ToDateTime(lblTransactionDate.Text))
        {
            txtApprovalDate.Focus();
            ShowMessage("Approval Date Should Not Be Less Then Transaction Date.", false, lblApprovalMSG);
            return;
        }

        if (string.IsNullOrEmpty(txtApprovalRemark.Text))
        {
            txtApprovalRemark.Focus();
            ShowMessage("Enter Approval Remark.", false, lblApprovalMSG);
            return;
        }

        pnlApprovalConfirmation.Visible = true;
    }

    protected void btnApprovalConfirmationYes_Click(object sender, EventArgs e)
    {
        try
        {
            PendingVouchersModel objPendingVouchersModel = new PendingVouchersModel();
            objPendingVouchersModel.Ind = 2;
            objPendingVouchersModel.OrgID = GlobalSession.OrgID;
            objPendingVouchersModel.BrID = GlobalSession.BrID;
            objPendingVouchersModel.YrCD = GlobalSession.YrCD;
            objPendingVouchersModel.TransactionNo = CommonCls.ConvertIntZero(lblTransactionNo.Text);
            objPendingVouchersModel.TransactionDate = CommonCls.ConvertToDate(lblTransactionDate.Text);
            objPendingVouchersModel.VchType = CommonCls.ConvertIntZero(ViewState["VchType"].ToString());
            objPendingVouchersModel.ApprovalDate = CommonCls.ConvertToDate(txtApprovalDate.Text);
            objPendingVouchersModel.ApprovalRemark = txtApprovalRemark.Text;
            objPendingVouchersModel.User = GlobalSession.UserID;
            objPendingVouchersModel.IP = GlobalSession.IP;

            string uri = "";

            if (GlobalSession.DesignationID == 26) //SUPERINTENDENT
            {
                objPendingVouchersModel.Ind = 2;
                objPendingVouchersModel.IsFinal = 1;
                uri = string.Format("PendingVouchers/DataApproved");
            }
            else if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17) //ACCOUNT OFFICER
            {
                objPendingVouchersModel.Ind = 3;
                objPendingVouchersModel.IsFinal = 2;
                uri = string.Format("PendingVouchers/FinalApproval");
            }

            DataTable dtApproved = CommonCls.ApiPostDataTable(uri, objPendingVouchersModel);
            if (dtApproved.Rows.Count > 0)
            {
                if (dtApproved.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    btnBack_Click(sender, e);
                    ShowMessage("Record Approved Successfully.", true, lblApprovalMSG);
                }
                else if (dtApproved.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    ShowMessage("Record Not Approved, Please Try Again.", false, lblApprovalMSG);
                }
            }
            else
            {
                ShowMessage("Record Not Approved, Please Try Again.", false, lblApprovalMSG);
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, false, lblApprovalMSG);
        }
    }

    protected void btnApprovalConfirmationNo_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = txtApprovalDate.Text = txtApprovalRemark.Text = "";
        pnlApproval.Visible = pnlApprovalConfirmation.Visible = false;
    }

    void ShowMessage(string Message, bool type, Label lblErrorMSG)
    {
        lblErrorMSG.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblErrorMSG.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}