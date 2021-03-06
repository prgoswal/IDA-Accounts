﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmCashSalesVoucher : System.Web.UI.Page
{
    CashSalesModel objCashSalesModel = new CashSalesModel();
    DataTable dtGrdOtherCharges, dtGrdAdditionalItems;
    #region Declarations
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

    DataTable VsDtItemSellRate
    {
        get { return (DataTable)ViewState["DtItems"]; }
        set { ViewState["DtItems"] = value; }
    }

    DataTable VsdtSundri
    {
        get { return (DataTable)ViewState["dtSundri"]; }
        set { ViewState["dtSundri"] = value; }
    }

    DataTable VsdtGvFreeItem
    {
        get { return (DataTable)ViewState["dtGvFreeItem"]; }
        set { ViewState["dtGvFreeItem"] = value; }
    }
    DataTable VsdtSeries
    {
        get { return (DataTable)ViewState["dtSeries"]; }
        set { ViewState["dtSeries"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            try
            {
                //CallReport("8", "2017/07/31", "RptSalesInvoice");
                ViewState["VchType"] = 6;
                BindAll();
                BindCancelReason();

                txtInvoiceDate.Text = CommonCls.ConvertDateDB(DateTime.Now);
                if (ddlInvoiceSeries.Visible && ddlInvoiceSeries.Enabled)
                    ddlInvoiceSeries.Focus();
                else
                    txtInvoiceDate.Focus();


                //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
                //{
                //    btnSave.Visible = false;

                //}
                //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
                //{
                //    btnSave.Visible = true;
                //}
                //if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
                //{
                //    btnCancelInvoice.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, false);
            }
        }
    }

    private void BindCancelReason()
    {
        try
        {

            objCashSalesModel = new CashSalesModel();
            objCashSalesModel.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
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
        objCashSalesModel = new CashSalesModel();
        objCashSalesModel.Ind = 11;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.YrCD = GlobalSession.YrCD;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.ByCashSales = 1;
        string uri = string.Format("CashSales/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
        if (dsBindAll.Tables.Count > 0)
        {
            DataTable dtWareHouse = dsBindAll.Tables[0];
            DataTable dtNarration = dsBindAll.Tables[1];
            DataTable dtInvoiceNoDate = dsBindAll.Tables[2];
            DataTable dtIncomeHead = dsBindAll.Tables[3];
            DataTable dtPartyName = dsBindAll.Tables[4];
            DataTable dtSundriesHeadName = dsBindAll.Tables[5];
            DataTable dtItem = dsBindAll.Tables[6];
            DataTable dtUnit = dsBindAll.Tables[7];
            DataTable dtTransport = dsBindAll.Tables[8];
            //DataTable dtInvoiceSeries = dsBindAll.Tables[9];
            VsdtSeries = dsBindAll.Tables[10];
            DataTable dtStates = dsBindAll.Tables[11];
            DataTable dtCashAccount = dsBindAll.Tables[12];
            DataTable dtBrokerList = dsBindAll.Tables[14];
            DataTable dtCostCenter = dsBindAll.Tables[13];

            if (dtStates.Rows.Count > 0)
            {
                ddlState.DataSource = dtStates;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateID";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

            if (dtWareHouse.Rows.Count > 0)
            {
                ddlLocation.DataSource = dtWareHouse;
                ddlLocation.DataTextField = "WareHouseAddress";
                ddlLocation.DataValueField = "WareHouseID";
                ddlLocation.DataBind();
                if (dtWareHouse.Rows.Count > 1)
                {
                    ddlLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }

            if (dtNarration.Rows.Count > 0)
            {
                ddlNarration.DataSource = dtNarration;
                ddlNarration.DataTextField = "NarrationDesc";
                ddlNarration.DataBind();
            }
            // For Last Invoice / Voucher No. Info Taken
            if (dtInvoiceNoDate.Rows.Count > 0)
            {
                if (dtInvoiceNoDate.Rows[0]["LastNo"].ToString() != "0")
                {
                    lblInvoiceAndDate.Text = "Last Invoice No. & Date : " + dtInvoiceNoDate.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtInvoiceNoDate.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                }
            }
            //if (dtInvoiceNo.Rows.Count > 0)
            //{
            //    if (dtInvoiceNo.Rows[0]["LastNo"].ToString() != "0")
            //    {
            //        lblInvoiceAndDate.Text = "Last Invoice No.: " + dtInvoiceNo.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtInvoiceNo.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
            //        LastInvoiceNo = dtInvoiceNo.Rows[0]["LastNo"].ToString();
            //        txtinvoiceNo.Text = "CASH" + "\\" + (Convert.ToInt64(LastInvoiceNo) + 1);
            //    }
            //}
            //else
            //{
            //    txtinvoiceNo.Text = "CASH" + "\\" + 1;
            //}

            //// For Income Account Head List 
            //if (dtIncomeHead.Rows.Count > 0)
            //{
            //    ddlIncomeHead.DataSource = dtIncomeHead;
            //    ddlIncomeHead.DataTextField = "AccName";
            //    ddlIncomeHead.DataValueField = "AccCode";
            //    ddlIncomeHead.DataBind();
            //    if (dtIncomeHead.Rows.Count > 1)
            //        ddlIncomeHead.Items.Insert(0, new ListItem { Text = "-Select Income Head-", Value = "0" });
            //    ddlIncomeHead.SelectedIndex = 0;
            //}

            if (dtPartyName.Rows.Count > 0)
            {
                ddlPartyName.DataSource = dtPartyName;
                ddlPartyName.DataTextField = "AccName";
                ddlPartyName.DataValueField = "AccCode";
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            if (dtSundriesHeadName.Rows.Count > 0)
            {
                ddlOtherChargesHeadName.DataSource = dtSundriesHeadName;
                ddlOtherChargesHeadName.DataTextField = "SundriHeadName";
                ddlOtherChargesHeadName.DataValueField = "AccCode";
                ddlOtherChargesHeadName.DataBind();
                ddlOtherChargesHeadName.Items.Insert(0, new ListItem("-- Select --", "0"));
                //ddlOtherChargesHeadName.SelectedIndex = 0;
            }
            if (dtItem.Rows.Count > 0)
            {
                ddlItemName.DataSource = dtItem;
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "ItemID";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("-- Select --", "0"));
                //ddlItemName.SelectedIndex = 0;
            }
            if (dtItem.Rows.Count > 0)
            {
                ddlFreeItemName.DataSource = dtItem;
                ddlFreeItemName.DataTextField = "ItemName";
                ddlFreeItemName.DataValueField = "ItemID";
                ddlFreeItemName.DataBind();
                ddlFreeItemName.Items.Insert(0, new ListItem("-- Select --", "0"));
                //ddlFreeItemName.SelectedIndex = 0;
            }
            if (dtUnit.Rows.Count > 0)
            {
                ddlUnit.DataSource = dtUnit;
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "UnitID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            if (dtUnit.Rows.Count > 0)
            {
                ddlMinorUnit.DataSource = dtUnit;
                ddlMinorUnit.DataTextField = "UnitName";
                ddlMinorUnit.DataValueField = "UnitID";
                ddlMinorUnit.DataBind();
                ddlMinorUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            if (dtUnit.Rows.Count > 0)
            {
                ddlFreeUnit.DataSource = dtUnit;
                ddlFreeUnit.DataTextField = "UnitName";
                ddlFreeUnit.DataValueField = "UnitID";
                ddlFreeUnit.DataBind();
                ddlFreeUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            if (dtTransport.Rows.Count > 0)
            {
                ddlTransportationType.DataSource = dtTransport;
                ddlTransportationType.DataTextField = "TransportMode";
                ddlTransportationType.DataValueField = "TransportID";
                ddlTransportationType.DataBind();
                ddlTransportationType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            if (dtCashAccount.Rows.Count > 0)
            {
                ddlCashAccount.DataSource = dtCashAccount;
                ddlCashAccount.DataTextField = "AccName";
                ddlCashAccount.DataValueField = "AccCode";
                ddlCashAccount.DataBind();
                if (dtCashAccount.Rows.Count > 1)
                {
                    ddlCashAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
                    ddlCashAccount.Enabled = true;
                }
                else
                    ddlCashAccount.Enabled = false;
            }

            if (dtBrokerList.Rows.Count > 0)
            {
                ddlBroker.DataSource = dtBrokerList;
                ddlBroker.DataTextField = "AccName";
                ddlBroker.DataValueField = "AccCode";
                ddlBroker.DataBind();
                ddlBroker.Items.Insert(0, new ListItem("-- Select Broker --", "0"));

            }

            // Cost Center List
            if (GlobalSession.CCCode == 1)
            {
                ddlCostCenter.Visible = true;
                //tdCCCode.Visible = true;
                if (dtCostCenter.Rows.Count > 0)
                {
                    ddlCostCenter.DataSource = dtCostCenter;
                    ddlCostCenter.DataTextField = "CostCentreName";
                    ddlCostCenter.DataValueField = "CostCentreID";
                    ddlCostCenter.DataBind();
                    ddlCostCenter.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }

            #region Series Selection

            if (VsdtSeries.Rows.Count == 0 || CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]) == 0)
            {
                txtInvoiceSeries.Visible = false;
                ShowMessageOnPopUp("Your Invoice Series Not Set. Press Yes For Setting Invoice Series.", false, "../Modifiaction/frmUpdateProfileCreation.aspx");
                return;
            }

            if (VsdtSeries.Rows.Count > 0)
            {

                if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 1) //Serial No Auto Generate When 1 Else 2 Munually
                {
                    txtinvoiceNo.Enabled = true;
                }
                else
                {
                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["InvoiceNo"]).ToString();
                    txtinvoiceNo.Enabled = false;
                }

                switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                {
                    case 1: /// Manual Series
                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = false;
                        txtInvoiceSeries.Visible = txtInvoiceSeries.Enabled = true;
                        //txtinvoiceNo.Visible = true;
                        break;

                    case 2: /// Available Series

                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
                        //ddlInvoiceSeries.DataValueField = "InvoiceNo";
                        ddlInvoiceSeries.DataBind();
                        if (VsdtSeries.Rows.Count > 0)
                        {
                            ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                            txtinvoiceNo.Text = "";
                        }
                        else
                        {
                            txtinvoiceNo.Text = ddlInvoiceSeries.SelectedValue;
                        }

                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = true;
                        txtInvoiceSeries.Visible = false;

                        break;

                    case 3: /// Default Series

                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
                        //ddlInvoiceSeries.DataValueField = "InvoiceNo";
                        ddlInvoiceSeries.DataBind();
                        ddlInvoiceSeries.Visible = true;
                        txtInvoiceSeries.Visible = false;

                        //if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 1)
                        //{
                        //    txtinvoiceNo.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtinvoiceNo.Text = CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["InvoiceNo"]).ToString();
                        //    txtinvoiceNo.Enabled = false;
                        //}

                        break;
                }
            }
            else
            {
                ddlInvoiceSeries.Visible = false;
            }

            #endregion
        }
        else
        {
            ddlInvoiceSeries.Visible = false;
            ShowMessage("Internet Connection Or Server Error!", false);
        }
    }

    protected void ddlInvoiceSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInvoiceSeries.SelectedIndex != 0)
        {
            if (CommonCls.ConvertIntZero(VsdtSeries.Rows[ddlInvoiceSeries.SelectedIndex - 1]["SerailNoInd"]) == 1)
            {
                txtinvoiceNo.Enabled = true;
                txtinvoiceNo.Text = "";
            }
            else
            {
                DataRow dr = VsdtSeries.Rows[ddlInvoiceSeries.SelectedIndex - 1];
                txtinvoiceNo.Text = dr["InvoiceNo"].ToString();//ddlInvoiceSeries.SelectedValue;
                txtinvoiceNo.Enabled = false;

                //txtinvoiceNo.Enabled = false;
                //txtinvoiceNo.Text = ddlInvoiceSeries.SelectedValue;
            }
        }
        else
        {
            txtinvoiceNo.Text = "";
        }
        ddlInvoiceSeries.Focus();
    }

    protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPartyName.SelectedValue == "" || ddlPartyName.SelectedValue == "0")
            {
                ShowMessage("Select Sales To.", false);
                return;
            }

            //tblShippingDetail.Visible = true;
            objCashSalesModel = new CashSalesModel();
            objCashSalesModel.Ind = 1;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.YrCD = GlobalSession.YrCD;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.AccCode = Convert.ToInt32(ddlPartyName.SelectedValue);

            string uri = string.Format("CashSales/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
            if (dtGSTIN.Rows.Count > 0)
            {
                if (dtGSTIN.Rows.Count > 1)
                {
                    ddlGstinNo.DataSource = dtGSTIN;
                    ddlGstinNo.DataValueField = "GSTIN";
                    ddlGstinNo.DataBind();
                    ddlGstinNo.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                    ddlGstinNo.SelectedIndex = 0;
                    txtPartyDetailMobileNo.Focus();
                }
                else
                {
                    ddlGstinNo.DataSource = dtGSTIN;
                    ddlGstinNo.DataValueField = "GSTIN";
                    ddlGstinNo.DataBind();
                    ddlGstinNo.SelectedValue = dtGSTIN.Rows[0]["GSTIN"].ToString();

                    FillShippingAddress();
                    txtPartyDetailMobileNo.Focus();
                }
            }
            else
            {
                ddlGstinNo.DataSource = dtGSTIN;
                ddlGstinNo.DataBind();
                FillShippingAddress();

                txtPartyDetailMobileNo.Focus();
            }

            //DataTable dtSalesTo = VsdtSalesTo; // (DataTable)ViewState["dtSalesTo"];
            //int TDS = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["TDSApplicable"].ToString());
            //int TCS = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["ISDApplicable"].ToString());
            //int RCM = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["RCMApplicable"].ToString());
            //if (TDS == 0)
            //{
            //    ddlTds.SelectedValue = "0";
            //}
            //if (TCS == 0)
            //{
            //    ddlTCS.SelectedValue = "0";
            //}
            //if (RCM == 0)
            //{
            //    ddlRCM.SelectedValue = "0";
            //}
            //ddlGstinNo.Focus();
            //ddlSalesto.Focus();
            //ddlItemName.Enabled = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlGstinNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGstinNo.SelectedValue != null)
        {
            if (ddlGstinNo.SelectedValue == "0")
            {
                ShowMessage("Select GSTIN No.", false);
                ddlItemName.Enabled = false;
                ddlGstinNo.Focus();
                return;
            }
        }

        if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
        {
            if (!CommonCls.GSTINIsValid(ddlGstinNo.Text))
            {
                ShowMessage("Invalid GSTIN.", false);
                ddlState.SelectedValue = "0";
                ddlGstinNo.Focus();
                return;
            }
            if (ddlState.Items.FindByValue(ddlGstinNo.Text.Substring(0, 2)) != null)
                ddlState.SelectedValue = ddlGstinNo.Text.Substring(0, 2);
            else
                ddlState.SelectedValue = "0";

        }

        ddlItemName.Enabled = true;
        FillShippingAddress();
        ddlGstinNo.Focus();
    }

    void FillShippingAddress()
    {
        objCashSalesModel = new CashSalesModel();
        objCashSalesModel.Ind = 4;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.YrCD = GlobalSession.YrCD;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.AccCode = Convert.ToInt32(ddlPartyName.SelectedValue);
        objCashSalesModel.GSTIN = ddlGstinNo != null ?
                 ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
                 ddlGstinNo.SelectedValue : "" : "";

        string uri = string.Format("CashSales/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
        if (dtShipping.Rows.Count > 0)
        {
            ddlShippingAdd.DataSource = dtShipping;
            ddlShippingAdd.DataTextField = "POSAddress";
            ddlShippingAdd.DataValueField = "AccPOSID";
            ddlShippingAdd.DataBind();
            if (dtShipping.Rows.Count > 1)
            {
                ddlShippingAdd.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
            }
            ddlShippingAdd.SelectedIndex = 0;
        }
        else
        {
            ddlShippingAdd.DataSource = dtShipping;
            ddlShippingAdd.DataBind();
            //ddlShippingAdd.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
            //ddlShippingAdd.SelectedIndex = 0;
        }
    }

    protected void btnGSTINInvoice_Click(object sender, EventArgs e)
    {
        if (tblGSTINInvoice.Visible == false)
        {
            tblGSTINInvoice.Visible = true;
            ddlPartyName.Focus();
        }
        else
        {
            tblGSTINInvoice.Visible = false;
            btnGSTINInvoice.Focus();
        }
    }

    protected void btnTransportDetail_Click(object sender, EventArgs e)
    {
        if (tblTransportDetail.Visible == false)
        {
            tblTransportDetail.Visible = true;
            ddlTransportationType.Focus();
        }
        else
        {
            tblTransportDetail.Visible = false;
            btnTransportDetail.Focus();
        }
    }

    protected void btnOtherCharge_Click(object sender, EventArgs e)
    {
        if (tblOtherCharge.Visible == false)
        {
            tblOtherCharge.Visible = grdOtherCharge.Visible = true;
            ddlOtherChargesHeadName.Focus();
        }
        else
        {
            tblOtherCharge.Visible = grdOtherCharge.Visible = false;
            btnOtherCharge.Focus();
        }
    }

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
            {
                if (ddlGstinNo.SelectedItem != null && !CommonCls.GSTINIsValid(ddlGstinNo.SelectedItem.Text))
                {
                    ShowMessage("Invalid GSTIN", false);
                    ddlGstinNo.Focus();
                    return;
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
                {
                    objCashSalesModel = new CashSalesModel();
                    objCashSalesModel.Ind = 15;
                    string uriGSTIn = string.Format("Master/Master");
                    DataTable dtState = CommonCls.ApiPostDataTable(uriGSTIn, objCashSalesModel);
                    if (dtState.Rows.Count > 0)
                    {
                        if (dtState.Select("StateID =" + ddlGstinNo.SelectedItem.Text.Substring(0, 2)).Count() == 0)
                        {
                            ShowMessage("Invalid GSTIN No.", false);
                            ddlGstinNo.Focus();
                            return;
                        }
                        //ddlItemName.Enabled = true;
                    }
                }
                txtShippingAdd.Focus();
            }
            catch (Exception ex)
            {
                ShowMessage("Invalid GSTIN.", false);
                ddlGstinNo.ClearSelection();
                ddlGstinNo.Focus();
            }

            if (ddlPartyName.Text != "0" && !string.IsNullOrEmpty(ddlPartyName.Text))
            {
                if (string.IsNullOrEmpty(ddlGstinNo.Text))
                {
                    if (CommonCls.ConvertIntZero(ddlState.SelectedValue) == 0)
                    {
                        ShowMessage("Select GSTIN Or State.", false);
                        ddlGstinNo.Focus();
                        return;
                    }
                }
            }
            else if (CommonCls.ConvertIntZero(ddlState.SelectedValue) > 0)
            {
                if (string.IsNullOrEmpty(ddlPartyName.Text))
                {
                    ShowMessage("Select Party Name.", false);
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(ddlGstinNo.Text))
            {
                if (string.IsNullOrEmpty(ddlPartyName.Text))
                {
                    ShowMessage("Select Party Name.", false);
                    return;
                }
            }


            if (!string.IsNullOrEmpty(ddlGstinNo.Text))
            {
                if (ddlState.Items.FindByValue(ddlGstinNo.Text.Substring(0, 2)) != null)
                    ddlState.SelectedValue = ddlGstinNo.Text.Substring(0, 2);
            }

            txtQty.Text = "";
            txtItemDetailTaxableAmount.Text = "0";
            //  txtDiscount.Text = "0";
            txtItemAmount.Text = "0";
            VsDtItemSellRate = VsdtItems = null;

            objCashSalesModel = new CashSalesModel();
            objCashSalesModel.Ind = 11;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.ItemID = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);

            if (!string.IsNullOrEmpty(ddlGstinNo.Text))
                objCashSalesModel.GSTIN = ddlGstinNo.SelectedValue;
            objCashSalesModel.PartyState = CommonCls.ConvertIntZero(ddlState.SelectedValue);

            objCashSalesModel.ByCashSales = 1;

            string uri = string.Format("CashSales/FillItemSellRate");
            DataSet dsSaleRate = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
            if (dsSaleRate.Tables.Count > 0)
            {
                VsdtItems = dsSaleRate.Tables[0];

                ddlUnit.SelectedValue = dsSaleRate.Tables[0].Rows[0]["ItemUnitID"].ToString();
                txtItemDetailRate.Text = dsSaleRate.Tables[0].Rows[0]["ItemSellingRate"].ToString();
                ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();

                //txtItemDetailsTaxRate.Text = dsSaleRate.Tables[0].Rows[0]["TaxRate"].ToString();
                //txtCGST.Text = dsSaleRate.Tables[0].Rows[0]["CGSTRate"].ToString();
                //txtSGST.Text = dsSaleRate.Tables[0].Rows[0]["SGSTRate"].ToString();
                //txtIGST.Text = dsSaleRate.Tables[0].Rows[0]["IGSTRate"].ToString();
                //txtCESS.Text = dsSaleRate.Tables[0].Rows[0]["Cess"].ToString();

                if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                {
                    VsDtItemSellRate = dsSaleRate.Tables[1];
                    TaxWithInRange();
                }
                else
                    FillTaxRate(VsdtItems.Rows[0]);

                TaxCal();

                //if (GlobalSession.StockMaintaineByMinorUnit)
                //{
                //    if (CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                //        txtMinorQty.Enabled = true;
                //    else
                //        txtMinorQty.Enabled = false;

                //    ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();
                //}
                //else
                //{
                //    ddlMinorUnit.SelectedValue = "0";
                //}
                if (GlobalSession.StockMaintaineByMinorUnit)
                {
                    ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();
                    if (CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                    {
                        txtMinorQty.Text = "";//CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["ItemMinorUnitQty"]).ToString();
                        txtMinorQty.Enabled = true;//false;
                    }
                    else
                    {
                        txtMinorQty.Text = "";
                        txtMinorQty.Enabled = false;
                    }
                }
                else
                {
                    //txtMinorUnitQty.Text = "";
                    //if (CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                    //    txtMinorUnitQty.Enabled = true;
                    //else
                    //    txtMinorUnitQty.Enabled = false;
                    //ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();

                    txtMinorQty.Enabled = false;
                    txtMinorQty.Text = "";
                    ddlMinorUnit.SelectedValue = "0";
                }
                //ViewState["HSNSACCode"] = Convert.ToInt32(dsItems.Tables[0].Rows[0]["HSNSACCode"].ToString());
            }
            txtQty.Focus();
        }

        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void TaxWithInRange()
    {
        if (VsDtItemSellRate != null && VsDtItemSellRate.Rows.Count > 0)
        {
            int rowIndex = 0;
            decimal MaxRate = 0, MinRate = 0;
            decimal InsertedRate = CommonCls.ConvertDecimalZero(txtItemDetailRate.Text);

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
        txtItemDetailsTaxRate.Text = drRate["TaxRate"].ToString();
        txtCGST.Text = drRate["CGSTRate"].ToString();
        txtSGST.Text = drRate["SGSTRate"].ToString();
        txtIGST.Text = drRate["IGSTRate"].ToString();
        txtCESS.Text = drRate["Cess"].ToString();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //TaxCal();
            CalculateRate();
            TaxCal();
            if (txtFree.Enabled)
                txtFree.Focus();
            else
                txtItemDetailRate.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage("Select Item First!", false);
        }
    }

    void CalculateRate()
    {

        //StructItems item = new StructItems();
        //item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        //item.ItemFree = CommonCls.ConvertDecimalZero(txtFree.Text);
        //item.ItemRate = CommonCls.ConvertDecimalZero(txtItemDetailRate.Text);
        //if (ddlDiscount.SelectedValue == "1")
        //{
        //    item.DiscountType = true;
        //    item.ItemDiscount = CommonCls.ConvertIntZero(txtDiscount.Text);
        //}
        //else if (ddlDiscount.SelectedValue == "0")
        //{
        //    item.DiscountType = false;
        //    item.ItemDiscount = CommonCls.ConvertDecimalZero(txtDiscount.Text);
        //}

        //StructItems GetItem = Calculation.PercentageOrRupees(item);
        //txtItemDetailTaxableAmount.Text = GetItem.ItemTaxable.ToString();
        //txtItemAmount.Text = GetItem.ItemAmount.ToString();
        //hfDiscountAmount.Value = GetItem.DiscountValue.ToString();


        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        item.ItemFree = CommonCls.ConvertDecimalZero(txtFree.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtItemDetailRate.Text);
        item.ItemDiscount = CommonCls.ConvertDecimalZero(txtDiscount.Text);
        item.DiscountInPerc = Convert.ToInt16(ddlDiscount.SelectedValue) == 1;

        StructItems GetItem = Calculation.CalculateRate(item);
        txtItemDetailTaxableAmount.Text = GetItem.ItemTaxable.ToString();
        txtItemAmount.Text = GetItem.ItemAmount.ToString();
        hfDiscountAmount.Value = GetItem.DiscountValue.ToString();
    }

    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {
        //decimal CgstAmt = 0, SgstAmt = 0, IgstAmt = 0, CessAmt = 0;

        //decimal MaxAmt = Convert.ToDecimal(txtItemDetailTaxableAmount.Text);

        //int devidedBy;
        //decimal TaxBy = 0;

        //DataTable dtItems = new DataTable();

        //if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
        //    dtItems = VsDtItemSellRate;
        //else
        //    dtItems = VsdtItems;

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(txtItemDetailTaxableAmount.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtItemDetailsTaxRate.Text);

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["Cess"]);

        StructItems GetItem = Calculation.TaxCal(item);
        txtCGST.Text = (CgstAmt = GetItem.ItemCGSTAmt).ToString();
        txtSGST.Text = (SgstAmt = GetItem.ItemSGSTAmt).ToString();
        txtIGST.Text = (IgstAmt = GetItem.ItemIGSTAmt).ToString();
        txtCESS.Text = (CessAmt = GetItem.ItemCESSAmt).ToString();

        CgstRat = GetItem.ItemCGSTRate;
        SgstRat = GetItem.ItemSGSTRate;
        IgstRat = GetItem.ItemIGSTRate;
        CessRat = GetItem.ItemCESSRate;

    }

    protected void btnAddItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemName.SelectedItem == null || ddlItemName.SelectedValue == "0")
            {
                ddlItemName.Focus();
                ShowMessage("Select Item Name.", false);
                return;
            }

            if (ddlItemName == null || ddlItemName.SelectedValue == null || Convert.ToInt32(ddlItemName.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
            {
                ddlItemName.Focus();
                ShowMessage("Item Value Not Available", false);
                return;
            }

            if (CommonCls.ConvertDecimalZero(txtQty.Text) == 0)
            {
                txtQty.Focus();
                ShowMessage("Enter Item Quantity", false);
                return;
            }
            if (ddlUnit.SelectedValue == "0")
            {
                ddlUnit.Focus();
                ShowMessage("Select Item Unit ", false);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtItemDetailRate.Text) == 0)
            {
                txtItemDetailRate.Focus();
                ShowMessage("Enter Item Rate", false);
                return;
            }

            if (CommonCls.ConvertIntZero(ddlDiscount.SelectedValue) == 1)
            {
                if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > 100)
                {
                    txtDiscount.Focus();
                    ShowMessage("Discount Not Greater Than 100%.", false);
                    return;
                }
            }

            //if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > CommonCls.ConvertDecimalZero(txtItemAmount.Text))
            //{
            //    ShowMessage("Discount Not Greater Than To Item Amount.", false);
            //    CalculateRate();
            //    TaxCal();
            //    txtDiscount.Focus();
            //    return;
            //}

            if (GlobalSession.StockMaintaineByMinorUnit)
            {
                if (VsdtItems != null && CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) != 0)
                {
                    if (CommonCls.ConvertDecimalZero(txtMinorQty.Text) == 0)
                    {
                        ShowMessage("Enter Minor Unit Qty.", false);
                        txtMinorQty.Focus();
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(ddlGstinNo.Text))
            {
                if (ddlState.Items.FindByValue(ddlGstinNo.Text.Substring(0, 2)) != null)
                    ddlState.SelectedValue = ddlGstinNo.Text.Substring(0, 2);
            }

            BindGRDItemDetails();
            CalculateTotalAmount();
            ClearItemDetailTable();
            //SelectionPA();
            ddlItemName.Focus();
            ddlPartyName.Enabled = txtPartyDetailMobileNo.Enabled = ddlGstinNo.Enabled = ddlShippingAdd.Enabled = ddlState.Enabled = false;
            if (chkDiscount.Checked == true)
            {

                txtDiscount.Enabled = false;
            }
            else
            {

                txtDiscount.Text = "0";
            }
            chkDiscount.Enabled = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void BindGRDItemDetails()
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
            decimal TaxRate = 0;
            if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);
            else
                TaxRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["TaxRate"]);
            //if (ddlPA.SelectedValue == "1")
            //    TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);

            DataTable dtItems = VsdtItems;

            DataRow DrGrdItemDetail = dtGvItemDetail.NewRow();
            TaxCal();

            DrGrdItemDetail["ItemName"] = ddlItemName.SelectedItem.Text;
            DrGrdItemDetail["HSNSACCode"] = dtItems.Rows[0]["HSNSACCode"];
            DrGrdItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            DrGrdItemDetail["FreeQty"] = CommonCls.ConvertDecimalZero(txtFree.Text);
            DrGrdItemDetail["ItemUnit"] = ddlUnit.SelectedItem.Text;
            DrGrdItemDetail["ItemRate"] = Convert.ToDecimal(txtItemDetailRate.Text);
            DrGrdItemDetail["ItemAmount"] = Convert.ToDecimal(txtItemAmount.Text);

            DrGrdItemDetail["ItemMinorUnit"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue) == 0 ? "" : ddlMinorUnit.SelectedItem.Text; //DrGrdItemDetail["ItemSecondaryUnit"]

            //DrGrdItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(txtMinorQty.Text);
            //DrGrdItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
            if (GlobalSession.StockMaintaineByMinorUnit)
            {
                DrGrdItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(txtMinorQty.Text);
                DrGrdItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
            }
            else
            {
                DrGrdItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemMinorUnitID"].ToString());
                DrGrdItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["ItemMinorUnitQty"].ToString());
            }

            DrGrdItemDetail["DiscountValue"] = CommonCls.ConvertDecimalZero(txtDiscount.Text);
            DrGrdItemDetail["DiscountType"] = ddlDiscount.SelectedValue;
            DrGrdItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(hfDiscountAmount.Value);

            DrGrdItemDetail["NetAmt"] = Convert.ToDecimal(txtItemDetailTaxableAmount.Text);
            DrGrdItemDetail["PADesc"] = "";
            DrGrdItemDetail["TaxRate"] = TaxRate;

            DrGrdItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGrdItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGrdItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGrdItemDetail["CESSTaxAmt"] = CessAmt;

            DrGrdItemDetail["ISDDesc"] = "";

            DrGrdItemDetail["ItemID"] = Convert.ToInt32(ddlItemName.SelectedValue);
            DrGrdItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGrdItemDetail["ItemUnitID"] = Convert.ToInt32(ddlUnit.SelectedValue);
            DrGrdItemDetail["IGSTTax"] = IgstRat;
            DrGrdItemDetail["SGSTTax"] = SgstRat;
            DrGrdItemDetail["CGSTTax"] = CgstRat;
            DrGrdItemDetail["CESSTax"] = CessRat;
            DrGrdItemDetail["ItemRemark"] = txtItemRemark.Text;
            DrGrdItemDetail["ISDApplicable"] = 0;
            DrGrdItemDetail["PA"] = 0;
            DrGrdItemDetail["ExtraInd"] = 0;//CommonCls.ConvertIntZero(dtItems.Rows[0]["StockMaintainInd"].ToString());

            dtGvItemDetail.Rows.Add(DrGrdItemDetail);
            grdItemDetails.DataSource = VsdtGvItemDetail = dtGvItemDetail;
            grdItemDetails.DataBind();
            //VsdtItems = null;
        }
    }

    void CalculateTotalAmount()
    {
        var cal = Calculation.CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);

        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtGross.Text = cal.TotalGross.ToString();
        txtTax.Text = cal.TotalTaxable.ToString();
        txtNetAmount.Text = cal.TotalAllNet.ToString();
        txtItem.Text = cal.ItemAmount.ToString();
        txtDiscountAmount.Text = cal.ItemDiscount.ToString();
        CalCulateBrokerAmount();
    }

    void ClearItemDetailTable()
    {
        ddlItemName.ClearSelection();
        //ddlItemName.Dispose();
        txtQty.Text = "0";
        txtFree.Text = "0";
        ddlUnit.ClearSelection();
        txtItemAmount.Text = "0";
        txtItemDetailRate.Text = "";
        txtItemDetailTaxableAmount.Text = "";
        // txtDiscount.Text = "0";
        ddlDiscount.ClearSelection();
        txtItemDetailsTaxRate.Text = "";
        //ddlPA.SelectedValue = "0";
        //ddlIsd.ClearSelection();
        txtCGST.Text = "";
        txtSGST.Text = "";
        txtIGST.Text = "";
        txtCESS.Text = "";
        //txtTaxAmt.Text = "";
        txtItemRemark.Text = "";
        ddlItemName.Focus();
    }

    DataTable DtItemsSchema()
    {
        DataTable dtItems = new DataTable();

        dtItems.Columns.Add("ItemName", typeof(string));
        dtItems.Columns.Add("ItemUnit", typeof(string));
        dtItems.Columns.Add("ItemMinorUnit", typeof(string));
        dtItems.Columns.Add("PADesc", typeof(string));
        dtItems.Columns.Add("ISDDesc", typeof(string));
        dtItems.Columns.Add("ItemID", typeof(int));
        dtItems.Columns.Add("HSNSACCode", typeof(string));
        dtItems.Columns.Add("GoodsServiceInd", typeof(int));
        dtItems.Columns.Add("ItemQty", typeof(decimal));
        dtItems.Columns.Add("FreeQty", typeof(decimal));
        dtItems.Columns.Add("ItemUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorQty", typeof(decimal));
        dtItems.Columns.Add("ItemRate", typeof(decimal));
        dtItems.Columns.Add("ItemAmount", typeof(decimal));
        dtItems.Columns.Add("DiscountValue", typeof(decimal)); //0 Pending default;
        dtItems.Columns.Add("DiscountType", typeof(int)); //0 Pending default;
        dtItems.Columns.Add("DiscountAmt", typeof(decimal));
        dtItems.Columns.Add("NetAmt", typeof(decimal));
        dtItems.Columns.Add("PA", typeof(int));
        dtItems.Columns.Add("TaxRate", typeof(decimal));
        dtItems.Columns.Add("IGSTTax", typeof(decimal));
        dtItems.Columns.Add("IGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("SGSTTax", typeof(decimal));
        dtItems.Columns.Add("SGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("CGSTTax", typeof(decimal));
        dtItems.Columns.Add("CGSTTaxAmt", typeof(decimal));
        dtItems.Columns.Add("CESSTax", typeof(decimal));
        dtItems.Columns.Add("CESSTaxAmt", typeof(decimal));
        dtItems.Columns.Add("ISDApplicable", typeof(int));
        dtItems.Columns.Add("ItemRemark", typeof(string));//0 Pending default;
        dtItems.Columns.Add("ExtraInd", typeof(int));

        //dtItems.Columns.Add("PADesc", typeof(string));
        //dtItems.Columns.Add("ISDDesc", typeof(string));
        //dtItems.Columns.Add("PA", typeof(int));
        //dtItems.Columns.Add("ISDApplicable", typeof(int));
        //dtItems.Columns.Add("ItemMinorUnitID", typeof(int));
        //dtItems.Columns.Add("ItemMinorQty", typeof(decimal));
        //dtItems.Columns.Add("StockMaintainInd", typeof(int));

        return dtItems;
    }

    protected void txtItemDetailRate_TextChanged(object sender, EventArgs e)
    {
        if (VsdtItems == null)
        {
            ShowMessage("Select Item First.", false);
            ddlItemName.Focus();
            return;
        }
        TaxWithInRange();
        CalculateRate();
        TaxCal();
        txtDiscount.Focus();
    }

    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        CalculateRate();
        if (!string.IsNullOrEmpty(txtQty.Text))
            TaxCal();

        txtDiscount.Focus();
    }

    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateRate();
        if (!string.IsNullOrEmpty(txtQty.Text))
            TaxCal();

        ddlDiscount.Focus();
    }



    protected void grdItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dtGrdItemDetail = VsdtGvItemDetail;
            if (e.CommandName == "RemoveItem")
            {

                dtGrdItemDetail.Rows[rowIndex].Delete();
                VsdtGvItemDetail = dtGrdItemDetail;
                grdItemDetails.DataSource = dtGrdItemDetail;
                grdItemDetails.DataBind();
                CalculateTotalAmount();
            }
            if (dtGrdItemDetail.Rows.Count == 0)
            {
                chkDiscount.Checked = false;
                chkDiscount_CheckedChanged(sender, e);
            }

            if (grdItemDetails.Rows.Count <= 0)
            {
                ddlPartyName.Enabled = txtPartyDetailMobileNo.Enabled = ddlGstinNo.Enabled = ddlShippingAdd.Enabled = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAddOtherCharges_Click(object sender, EventArgs e)
    {
        tblOtherCharge.Visible = true;
        tblTransportDetail.Visible = tblGSTINInvoice.Visible = false;

        if ((ddlOtherChargesHeadName.SelectedItem == null) || (string.IsNullOrEmpty(ddlOtherChargesHeadName.SelectedValue)) || ddlOtherChargesHeadName.SelectedValue == "0")
        {
            ddlOtherChargesHeadName.Focus();
            ShowMessage("Select Head.", false);
            return;
        }

        if ((ddlOtherChargesAddLess.SelectedValue == "0") || string.IsNullOrEmpty(ddlOtherChargesAddLess.SelectedValue))
        {
            ddlOtherChargesAddLess.Focus();
            ShowMessage("Select Add/Less Option.", false);
            return;
        }
        if (string.IsNullOrEmpty(txtOtherChargesAmount.Text) || Convert.ToDecimal(txtOtherChargesAmount.Text) <= 0)
        {
            txtOtherChargesAmount.Focus();
            ShowMessage("Enter Amount.", false);
            return;
        }

        if (VsdtSundri != null) // First Time DataTable Create For Grid
        {
            dtGrdOtherCharges = VsdtSundri;
            DataRow[] rows = dtGrdOtherCharges.Select("SundriCode=" + ddlOtherChargesHeadName.SelectedValue);
            if (rows.Count() >= 1)
            {
                ddlOtherChargesHeadName.Focus();
                ShowMessage("This Charge Already.", false);
                return;
            }
        }

        if (VsdtSundri == null) // First Time DataTable Create For Grid
        {
            dtGrdOtherCharges = DtSundriesSchema();
        }
        else
        {
            dtGrdOtherCharges = VsdtSundri;
        }
        DataRow dr = dtGrdOtherCharges.NewRow();
        dr["SundriCode"] = ddlOtherChargesHeadName.SelectedValue;
        dr["SundriHead"] = ddlOtherChargesHeadName.SelectedItem.Text;
        dr["SundriInd"] = ddlOtherChargesAddLess.SelectedItem.Text;
        dr["SundriAmt"] = txtOtherChargesAmount.Text;
        dtGrdOtherCharges.Rows.Add(dr);
        VsdtSundri = dtGrdOtherCharges;
        grdOtherCharge.DataSource = dtGrdOtherCharges;
        grdOtherCharge.DataBind();
        CalculateTotalAmount();
        ddlOtherChargesHeadName.ClearSelection();
        ddlOtherChargesAddLess.ClearSelection();
        txtOtherChargesAmount.Text = "0";
        ddlOtherChargesHeadName.Focus();
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

    protected void grdOtherCharge_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtGrdOtherCharges = VsdtSundri;
                dtGrdOtherCharges.Rows[rowIndex].Delete();

                VsdtSundri = dtGrdOtherCharges;
                grdOtherCharge.DataSource = dtGrdOtherCharges;
                grdOtherCharge.DataBind();
                CalculateTotalAmount();
                if (grdOtherCharge.Rows.Count < 0)
                {
                    tblOtherCharge.Visible = false;
                    tblTransportDetail.Visible = tblGSTINInvoice.Visible = false;
                }
                else
                {
                    tblOtherCharge.Visible = true;
                    tblTransportDetail.Visible = tblGSTINInvoice.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    //protected void ddlAdditionalItems_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtQty.Text = "";
    //        txtItemDetailTaxableAmount.Text = "0";
    //        txtDiscount.Text = "0";
    //        txtItemAmount.Text = "0";
    //        VsDtItemSellRate = VsdtItems = null;

    //        objCashSalesModel = new CashSalesModel();
    //        objCashSalesModel.Ind = 2;
    //        objCashSalesModel.OrgID = GlobalSession.OrgID;
    //        objCashSalesModel.BrID = GlobalSession.BrID;
    //        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
    //        objCashSalesModel.ItemID = CommonCls.ConvertIntZero(ddlAdditionalItems.SelectedValue);
    //        objCashSalesModel.ByCashSales = 1;

    //        string uri = string.Format("CashSales/FillItemSellRate");
    //        DataTable dtItems = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
    //        if (dtItems.Rows.Count > 0)
    //        {
    //            ddlAdditionalUnit.SelectedValue = dtItems.Rows[0]["ItemUnitID"].ToString();
    //        }
    //        txtAdditionalQty.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}

    //protected void btnAddAdditionalItems_Click(object sender, EventArgs e)
    //{
    //    if ((ddlAdditionalItems.SelectedItem == null) || (string.IsNullOrEmpty(ddlAdditionalItems.SelectedValue)) || ddlAdditionalItems.SelectedValue == "0")
    //    {
    //        ddlAdditionalItems.Focus();
    //        ShowMessage("Select Additional Free Items.", false);
    //        return;
    //    }

    //    if ((ddlAdditionalUnit.SelectedValue == "0") || string.IsNullOrEmpty(ddlAdditionalUnit.SelectedValue))
    //    {
    //        ddlAdditionalUnit.Focus();
    //        ShowMessage("Select Additional Free Unit.", false);
    //        return;
    //    }
    //    if (string.IsNullOrEmpty(txtAdditionalQty.Text) || Convert.ToDecimal(txtAdditionalQty.Text) <= 0)
    //    {
    //        txtOtherChargesAmount.Focus();
    //        ShowMessage("Enter Additional Free Qty.", false);
    //        return;
    //    }

    //    //if (VsdtSundri != null) // First Time DataTable Create For Grid
    //    //{
    //    //    dtGrdOtherCharges = VsdtSundri;
    //    //    DataRow[] rows = dtGrdOtherCharges.Select("SundriCode=" + ddlOtherChargesHeadName.SelectedValue);
    //    //    if (rows.Count() >= 1)
    //    //    {
    //    //        ddlOtherChargesHeadName.Focus();
    //    //        ShowMessage("This Charge Already.", false);
    //    //        return;
    //    //    }
    //    //}

    //    if (VsdtAdditionaFreeItems == null) // First Time DataTable Create For Grid
    //    {
    //        dtGrdAdditionalItems = DtAdditionalItemsSchema();
    //    }
    //    else
    //    {
    //        dtGrdAdditionalItems = VsdtAdditionaFreeItems;
    //    }
    //    DataRow drAdditionalItems = dtGrdAdditionalItems.NewRow();
    //    drAdditionalItems["ItemID"] = ddlAdditionalItems.SelectedValue;
    //    drAdditionalItems["ItemName"] = ddlAdditionalItems.SelectedItem.Text;
    //    drAdditionalItems["ItemUnitName"] = ddlAdditionalUnit.SelectedValue;
    //    drAdditionalItems["ItemUnitID"] = ddlAdditionalUnit.SelectedValue;
    //    drAdditionalItems["Qty"] = txtAdditionalQty.Text;
    //    dtGrdAdditionalItems.Rows.Add(drAdditionalItems);
    //    VsdtAdditionaFreeItems = dtGrdAdditionalItems;
    //    grdAdditionalItems.DataSource = dtGrdAdditionalItems;
    //    grdAdditionalItems.DataBind();
    //    //CalculateTotalAmount();
    //    ddlAdditionalItems.ClearSelection();
    //    ddlAdditionalUnit.ClearSelection();
    //    txtAdditionalQty.Text = "";
    //    ddlAdditionalItems.Focus();
    //}

    DataTable DtAdditionalItemsSchema()
    {
        DataTable dtAdditionalItems = new DataTable();

        dtAdditionalItems.Columns.Add("ItemID", typeof(int));
        dtAdditionalItems.Columns.Add("ItemName", typeof(string));
        dtAdditionalItems.Columns.Add("ItemUnitName", typeof(string));
        dtAdditionalItems.Columns.Add("ItemUnitID", typeof(int));
        dtAdditionalItems.Columns.Add("Qty", typeof(decimal));
        return dtAdditionalItems;
    }

    //protected void grdAdditionalItems_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int rowIndex = Convert.ToInt32(e.CommandArgument);
    //    if (e.CommandName == "RemoveRow")
    //    {
    //        dtGrdAdditionalItems = VsdtAdditionaFreeItems;
    //        dtGrdAdditionalItems.Rows[rowIndex].Delete();

    //        VsdtAdditionaFreeItems = dtGrdAdditionalItems;
    //        grdAdditionalItems.DataSource = dtGrdAdditionalItems;
    //        grdAdditionalItems.DataBind();
    //    }
    //}

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue == "0")
        {
            txtPMRemark.Enabled = false;
            txtPMRemark.Text = "";
        }
        else if (ddlPaymentMode.SelectedValue == "1" || ddlPaymentMode.SelectedValue == "2")
            txtPMRemark.Enabled = true;
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
        txtQty.Text = "";
        txtItemDetailTaxableAmount.Text = "0";
        txtDiscount.Text = "0";
        txtItemAmount.Text = "0";
        VsDtItemSellRate = VsdtItems = null;

        objCashSalesModel = new CashSalesModel();
        objCashSalesModel.Ind = 11;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);
        objCashSalesModel.ByCashSales = 1;

        string uri = string.Format("CashSales/FillItemSellRate");
        DataSet dsSaleRate = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
        if (dsSaleRate.Tables.Count > 0)
        {
            VsdtItems = dsSaleRate.Tables[0];
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
                ShowMessage("Insert Item Detail First.", false);
                ddlItemName.Focus();
                return;
            }

            if (CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue) == 0)
            {
                ShowMessage("Select Item Name First.", false);
                ddlFreeItemName.Focus();
                return;
            }
            if (CommonCls.ConvertIntZero(ddlFreeUnit.SelectedValue) == 0)
            {
                ShowMessage("Select Item Name First.", false);
                ddlFreeItemName.Focus();
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtFreeQty.Text) == 0)
            {
                ShowMessage("Enter Free Qty.", false);
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
                    ShowMessage("This Item Already Exist.", false);
                    ddlFreeItemName.Focus();
                    return;
                }
            }

            DataRow drItem = VsdtGvFreeItem.NewRow();
            //drItem["PurchaseCode"] = 0;

            drItem["ItemID"] = ddlFreeItemName.SelectedValue;
            drItem["ItemName"] = ddlFreeItemName.SelectedItem.Text;
            drItem["ItemUnitID"] = ddlFreeUnit.SelectedValue;
            drItem["ItemUnit"] = ddlFreeUnit.SelectedItem.Text;
            drItem["ItemQty"] = CommonCls.ConvertDecimalZero(txtFreeQty.Text);
            drItem["ExtraInd"] = 1; //This For FreeItemInd
            drItem["GoodsServiceInd"] = VsdtItems.Rows[0]["GoodsServiceIndication"];
            drItem["HSNSACCode"] = VsdtItems.Rows[0]["HSNSACCode"];// ViewState["FreeHSNSACCode"].ToString();
            VsdtGvFreeItem.Rows.Add(drItem);
            gvFreeItem.DataSource = VsdtGvFreeItem;
            gvFreeItem.DataBind();
            ClearFreeItem();
            ddlFreeItemName.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (!BtnSaveValidation())
            {
                return;
            }

            objCashSalesModel = new CashSalesModel();
            objCashSalesModel.Ind = 1;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.YrCD = GlobalSession.YrCD;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.User = GlobalSession.UserID;
            objCashSalesModel.IP = GlobalSession.IP;

            objCashSalesModel.EntryType = 1;

            objCashSalesModel.ByCashSales = 1;

            objCashSalesModel.DtAdjAdvance = CreateAdvanceDt();

            //ObjSaleModel.InvoiceSeries = txtInvoiceSeries.Text;
            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    objCashSalesModel.InvoiceSeries = txtInvoiceSeries.Text.ToUpper();
                    break;

                case 2: /// Available Series
                    objCashSalesModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    objCashSalesModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;
            }

            //objCashSalesModel.InvoiceSeries = "CASH";
            objCashSalesModel.PartyName = ddlPartyName.SelectedItem == null ? "" : ddlPartyName.SelectedItem.Text; //txtPartyDetailPartyName.Text;
            objCashSalesModel.PartyMobileNo = CommonCls.ConvertLongZero(txtPartyDetailMobileNo.Text);
            objCashSalesModel.PartyGSTIN = ddlGstinNo.Text.ToUpper();
            objCashSalesModel.PayMode = ddlPaymentMode.SelectedItem.Text;
            objCashSalesModel.PayModeRemark = txtPMRemark.Text;

            objCashSalesModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            objCashSalesModel.PartyAddress = ddlShippingAdd.SelectedItem == null ? "" : ddlShippingAdd.SelectedItem.Text; //ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
            //ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            objCashSalesModel.WareHouseID = CommonCls.ConvertIntZero(ddlLocation.SelectedValue);
            objCashSalesModel.PONo = "";

            objCashSalesModel.BrokerageID = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
            objCashSalesModel.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerRate.Text);
            objCashSalesModel.BrokerageGSTIN = string.IsNullOrEmpty(ddlBrokerGSTIN.SelectedValue) ? "" : ddlBrokerGSTIN.SelectedValue;

            objCashSalesModel.BrokerageAmount = CommonCls.ConvertDecimalZero(txtBrokerAmount.Text);

            objCashSalesModel.TransName = txtTransportationName.Text;
            objCashSalesModel.PartyState = CommonCls.ConvertIntZero(ddlState.SelectedValue);

            objCashSalesModel.DtCashSales = DtCashSalesSchema();
            objCashSalesModel.DtItems = DtItemsSchema();
            objCashSalesModel.DtSundries = DtSundriesSchema();

            objCashSalesModel.DtCashSales = CreateCashSalesData();

            if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            {
                foreach (DataRow item in VsdtGvFreeItem.Rows)
                {
                    //VsdtGvItemDetail.NewRow();
                    VsdtGvItemDetail.Rows.Add(item.ItemArray);
                }
            }

            objCashSalesModel.DtItems = VsdtGvItemDetail;
            objCashSalesModel.DtSundries = VsdtSundri;
            if ((objCashSalesModel.DtSundries == null) || (objCashSalesModel.DtSundries.Rows.Count == 0))
            {
                objCashSalesModel.DtSundries = DtSundriesSchema();
                DataRow drSaleSundri = objCashSalesModel.DtSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                objCashSalesModel.DtSundries.Rows.Add(drSaleSundri);
            }

            string uri = string.Format("CashSales/SaveCashSalesVoucher");
            DataTable dtSaveCashSales = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
            if (dtSaveCashSales.Rows.Count > 0)
            {
                if (dtSaveCashSales.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    chkDiscount.Enabled = true;
                    chkDiscount.Checked = false;
                    chkDiscount_CheckedChanged(sender, e);
                    string InvoiceNo, InvoiceDate, InvoiceName, LastVNO, InvoiceSeries = "";
                    InvoiceNo = dtSaveCashSales.Rows[0]["LastInvoiceNo"].ToString();
                    InvoiceDate = Convert.ToDateTime(dtSaveCashSales.Rows[0]["LastInvoiceDate"].ToString()).ToString("dd/MM/yyyy");
                    InvoiceName = dtSaveCashSales.Rows[0]["InvoiceName"].ToString();
                    LastVNO = dtSaveCashSales.Rows[0]["DocMaxNo"].ToString();
                    ShowMessage("Record Save successfully for Invoice No. " + InvoiceNo, true);

                    if (!string.IsNullOrEmpty(objCashSalesModel.InvoiceSeries))
                        InvoiceSeries = objCashSalesModel.InvoiceSeries + "-";

                    lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;


                    if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 2) //Serial No Auto Generate No.2
                    {
                        string LastInvoiceNo = "";
                        foreach (DataRow item in VsdtSeries.Rows)
                        {
                            if (CommonCls.ConvertIntZero(txtinvoiceNo.Text) == CommonCls.ConvertIntZero(item["InvoiceNo"]))
                            {
                                item["InvoiceNo"] = CommonCls.ConvertIntZero(InvoiceNo) + 1;
                                LastInvoiceNo = (CommonCls.ConvertIntZero(InvoiceNo) + 1).ToString();
                            }
                        }

                        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                        {
                            case 1: /// Manual Series
                                txtinvoiceNo.Text = LastInvoiceNo;
                                break;

                            case 2: /// Available Series
                                ddlInvoiceSeries.DataSource = VsdtSeries;
                                ddlInvoiceSeries.DataTextField = "Series";
                                ddlInvoiceSeries.DataBind();
                                if (VsdtSeries.Rows.Count > 1)
                                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                                if (ddlInvoiceSeries.SelectedValue == "0")
                                {
                                    txtinvoiceNo.Text = "";
                                }
                                else
                                {
                                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                                }
                                break;

                            case 3: /// Default Series
                                ddlInvoiceSeries.DataSource = VsdtSeries;
                                ddlInvoiceSeries.DataTextField = "Series";
                                ddlInvoiceSeries.DataBind();
                                if (VsdtSeries.Rows.Count > 1)
                                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                                if (ddlInvoiceSeries.SelectedValue == "0")
                                {
                                    txtinvoiceNo.Text = "";
                                }
                                else
                                {
                                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                                }
                                break;
                        }
                    }
                    else
                    {
                        txtinvoiceNo.Text = "";
                    }

                    //txtinvoiceNo.Text = ("CASH\\" + (CommonCls.ConvertLongZero(InvoiceNo) + 1)).ToString();
                    //txtinvoiceNo.Text = (CommonCls.ConvertLongZero(InvoiceNo) + 1).ToString();

                    ddlItemName.Focus();
                    CallReport(InvoiceNo, CommonCls.ConvertToDate(InvoiceDate), InvoiceName, InvoiceSeries, LastVNO);

                }
                else if (dtSaveCashSales.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Voucher No Duplicate.", false);
                }
            }
            else
            {
                ShowMessage("Record Not Save Please Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            //ShowMessage("Internal Server Error!", false);
            ShowMessage(ex.Message, false);
        }

    }

    private DataTable CreateAdvanceDt()
    {
        DataTable VsdtAdvance = new DataTable();
        VsdtAdvance.Columns.Add("AdvRecPayID");
        VsdtAdvance.Columns.Add("AdvRecPayDesc");
        VsdtAdvance.Columns.Add("PartyCode");
        VsdtAdvance.Columns.Add("PartyGSTIN");
        VsdtAdvance.Columns.Add("VoucharNo");
        VsdtAdvance.Columns.Add("VoucharDate");
        VsdtAdvance.Columns.Add("TaxRate");
        VsdtAdvance.Columns.Add("AdvAmount");

        DataRow drAdv = VsdtAdvance.NewRow();
        drAdv["AdvRecPayID"] = 0;
        drAdv["AdvRecPayDesc"] = "";
        drAdv["PartyCode"] = 0;
        drAdv["PartyGSTIN"] = "";
        drAdv["VoucharNo"] = 0;
        drAdv["VoucharDate"] = "";
        drAdv["TaxRate"] = 0;
        drAdv["AdvAmount"] = 0;
        VsdtAdvance.Rows.Add(drAdv);

        return VsdtAdvance;
    }

    bool BtnSaveValidation()
    {
        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
        {
            case 1: /// Manual Series
                if (string.IsNullOrEmpty(txtInvoiceSeries.Text))
                {
                    txtInvoiceSeries.Focus();
                    ShowMessage("Enter Invoice Series.", false);
                    return false;
                }
                if (string.IsNullOrEmpty(txtinvoiceNo.Text) || (Convert.ToInt32(txtinvoiceNo.Text) == 0))
                {
                    txtinvoiceNo.Focus();
                    ShowMessage("Enter Invoice No.", false);
                    return false;
                }
                break;

            case 2: /// Available Series
                if (ddlInvoiceSeries.SelectedValue == "0") //Convert.ToInt32(ddlInvoiceSeries.SelectedValue) == 0
                {
                    ddlInvoiceSeries.Focus();
                    ShowMessage("Select Invoice Series.", false);
                    return false;
                }
                break;

            case 3: /// Default Series                
                if (string.IsNullOrEmpty(txtinvoiceNo.Text) || (Convert.ToInt32(txtinvoiceNo.Text) == 0))
                {
                    txtinvoiceNo.Focus();
                    ShowMessage("Enter Invoice No.", false);
                    return false;
                }
                break;
        }


        if (GlobalSession.CCCode == 1)
        {
            if (ddlCostCenter.SelectedValue == "0")
            {
                ShowMessage("Select Cost Centre!", false);
                ddlCostCenter.Focus();
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtInvoiceDate.Text))
        {
            ShowMessage("Enter Invoice Date.", false);
            txtInvoiceDate.Focus();
            return false;
        }
        bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtInvoiceDate.Focus();
            ShowMessage("Invoice Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
            return false;
        }
        if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
        {
            ShowMessage("Item Detail Can Not Be Null.", false);
            ddlItemName.Focus();
            return false;
        }
        if (!string.IsNullOrEmpty(txtTransportationDate.Text))
        {
            bool ValidTransportationDate = CommonCls.CheckFinancialYrDate(txtTransportationDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidTransportationDate) // For Transportation Date Between Financial Year.
            {
                txtTransportationDate.Focus();
                ShowMessage("Transportation Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return false;
            }
        }


        if (ddlBroker.SelectedValue != "0")
        {

            if (ddlBrokerGSTIN.SelectedValue != null)
            {
                if (ddlBrokerGSTIN.SelectedValue == "0")
                {
                    ShowMessage("Select Broker GSTIN No.", false);

                    ddlBrokerGSTIN.Focus();
                    return false;
                }
            }


            if (string.IsNullOrEmpty(txtBrokerRate.Text) || txtBrokerRate.Text == "0")
            {
                ShowMessage("Enter Brokerage Rate!", false);
                return false;
            }
            if (string.IsNullOrEmpty(txtBrokerAmount.Text) || txtBrokerAmount.Text == "0")
            {
                ShowMessage("Enter Brokerage Amount!", false);
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

                CashSalesModel objCashSalesModel;
                objCashSalesModel = new CashSalesModel();
                objCashSalesModel.Ind = 6;
                objCashSalesModel.OrgID = GlobalSession.OrgID;
                objCashSalesModel.BrID = GlobalSession.BrID;
                objCashSalesModel.AccCode = CommonCls.ConvertIntZero(ddlPartyName.SelectedValue);

                if (CommonCls.ConvertIntZero(ddlPartyName.SelectedValue) == 0)
                {
                    return true;
                }

                string uri = string.Format("PurchaseVoucher/CheckGSTIN_Number");

                DataSet dtStatePanNo = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
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
            throw ex;
            //ShowMessage(ex.Message, false);
        }
        return false;
    }

    DataTable DtCashSalesSchema()
    {
        DataTable dtCashSales = new DataTable();

        dtCashSales.Columns.Add("AccCode", typeof(int));
        dtCashSales.Columns.Add("AccGst", typeof(string));
        dtCashSales.Columns.Add("SalePurchaseCode", typeof(int));
        dtCashSales.Columns.Add("AccPOSID", typeof(int));
        dtCashSales.Columns.Add("GSTIN", typeof(string));
        dtCashSales.Columns.Add("WareHouseID", typeof(int));
        dtCashSales.Columns.Add("OrderNo", typeof(int));
        dtCashSales.Columns.Add("OrderDate", typeof(string));
        dtCashSales.Columns.Add("InvoiceNo", typeof(string));
        dtCashSales.Columns.Add("InvoiceDate", typeof(string));
        dtCashSales.Columns.Add("TDSApplicable", typeof(int));
        dtCashSales.Columns.Add("TCSApplicable", typeof(int));
        dtCashSales.Columns.Add("RCMApplicable", typeof(int));
        dtCashSales.Columns.Add("GrossAmt", typeof(decimal));
        dtCashSales.Columns.Add("TaxAmt", typeof(decimal));
        dtCashSales.Columns.Add("NetAmt", typeof(decimal));
        dtCashSales.Columns.Add("RoundOffAmt", typeof(decimal));
        dtCashSales.Columns.Add("TransportID", typeof(int));
        dtCashSales.Columns.Add("VehicleNo", typeof(string));
        dtCashSales.Columns.Add("WayBillNo", typeof(int));
        dtCashSales.Columns.Add("TransportDate", typeof(string));
        dtCashSales.Columns.Add("DocDesc", typeof(string));

        return dtCashSales;
    }

    DataTable CreateCashSalesData()
    {
        DataTable dtCreateCashSalesData = new DataTable();

        dtCreateCashSalesData = DtCashSalesSchema(); //new DataTable();
        DataRow drCashSales = dtCreateCashSalesData.NewRow();

        drCashSales["AccCode"] = CommonCls.ConvertIntZero(ddlCashAccount.SelectedValue); //919000;
        drCashSales["AccGst"] = ddlGstinNo.SelectedItem == null ? "" : ddlGstinNo.SelectedItem.Text;
        drCashSales["SalePurchaseCode"] = 930001;
        drCashSales["AccPOSID"] = 0;
        drCashSales["WarehouseID"] = CommonCls.ConvertIntZero(ddlLocation.SelectedValue); ;
        drCashSales["OrderNo"] = 0;
        drCashSales["OrderDate"] = "";

        //string[] splitTXTInvoice = txtinvoiceNo.Text.Split('\\');
        //drCashSales["InvoiceNo"] = CommonCls.ConvertIntZero(splitTXTInvoice[1]);

        drCashSales["InvoiceNo"] = Convert.ToInt32(txtinvoiceNo.Text);
        drCashSales["InvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        drCashSales["TDSApplicable"] = 0;
        drCashSales["TCSApplicable"] = 0;
        drCashSales["RCMApplicable"] = 0;
        drCashSales["GrossAmt"] = CommonCls.ConvertDecimalZero(txtGross.Text);
        drCashSales["TaxAmt"] = CommonCls.ConvertDecimalZero(txtTax.Text);
        drCashSales["NetAmt"] = CommonCls.ConvertDecimalZero(txtNetAmount.Text);
        drCashSales["RoundOffAmt"] = 0;

        drCashSales["TransportID"] = CommonCls.ConvertIntZero(ddlTransportationType.SelectedValue);
        drCashSales["VehicleNo"] = txtTransportationVehicleNo.Text;
        drCashSales["WayBillNo"] = 0;
        drCashSales["TransportDate"] = CommonCls.ConvertToDate(txtTransportationDate.Text);// + " " + txtTransportationDate.Text.Substring(11, 8);

        drCashSales["DocDesc"] = ddlNarration.Text;

        dtCreateCashSalesData.Rows.Add(drCashSales);

        return dtCreateCashSalesData;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        chkDiscount_CheckedChanged(sender, e);
    }

    void ClearAll()
    {
        txtDiscount.Enabled = true;
        chkDiscount.Checked = false;
        ddlMinorUnit.ClearSelection();
        ddlGstinNo.DataSource = new DataTable();
        ddlGstinNo.DataBind();
        txtMinorQty.Text = "";

        ddlState.Enabled = true;
        ddlState.ClearSelection();
        ddlInvoiceSeries.ClearSelection();
        //txtinvoiceNo.Text = "";

        ddlPartyName.ClearSelection();
        ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        txtPartyDetailMobileNo.Text = "";
        txtInvoiceDate.Text = CommonCls.ConvertDateDB(DateTime.Now);
        VsdtGvItemDetail = VsdtSundri = VsdtGvFreeItem = null;
        grdItemDetails.DataSource = grdOtherCharge.DataSource = gvFreeItem.DataSource = new DataTable();
        grdItemDetails.DataBind();
        grdOtherCharge.DataBind();
        gvFreeItem.DataBind();
        ddlTransportationType.ClearSelection();
        txtTransportationDate.Text = txtTransportationVehicleNo.Text = txtTransportationName.Text = txtBrokerRate.Text = txtBrokerAmount.Text = "";
        ddlOtherChargesHeadName.ClearSelection();
        ddlOtherChargesAddLess.ClearSelection();
        txtOtherChargesAmount.Text = "";
        ddlFreeItemName.ClearSelection();
        ddlFreeUnit.ClearSelection();
        txtFreeQty.Text = "";
        ddlPaymentMode.ClearSelection();
        txtPMRemark.Text = "";
        txtGross.Text = txtTax.Text = txtAddLess.Text = txtNetAmount.Text = "";
        ddlNarration.ClearSelection();

        tblGSTINInvoice.Visible = tblOtherCharge.Visible = tblTransportDetail.Visible = divFree.Visible =
            grdOtherCharge.Visible = false;
        ddlPartyName.Enabled = txtPartyDetailMobileNo.Enabled = ddlGstinNo.Enabled = ddlShippingAdd.Enabled = true;

        pnlBroker.Visible = false;
        ddlBroker.ClearSelection();
        ddlBrokerGSTIN.ClearSelection();
        ddlBrokerGSTIN.Enabled = false;
        txtBrokerAmount.Enabled = false;
        txtBrokerRate.Enabled = false;
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    public void CallReport(string InvoiceNo, string InvoiceDate, string InvoiceName, string InvoiceSeries, string LastVNO)
    {

        Hashtable HT = new Hashtable();
        HT.Add("Ind", 1);

        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "CASH SALES INVOICE ");

        HT.Add("Doctype", 6);
        HT.Add("invoiceno", Convert.ToInt32(InvoiceNo));
        HT.Add("invoiceDate", InvoiceDate);
        HT.Add("invoiceDateFrom", "");
        HT.Add("invoiceDateto", "");
        HT.Add("cashsalesind", 1);
        HT.Add("vNO", LastVNO);
        //HT.Add("InvoiceSeries", InvoiceSeries);

        VouchersReport.ReportName = InvoiceName;
        VouchersReport.FileName = "SalesInvoice";
        VouchersReport.ReportHeading = "Cash Sales Invoice";
        VouchersReport.HashTable = HT;

        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print Invoice";

        VouchersReport.ShowReport();
    }
    protected void txtFree_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (VsdtItems == null)
            {
                ShowMessage("Select Item First.", false);
                ddlItemName.Focus();
                return;
            }
            CalculateRate();
            TaxCal();
            txtItemDetailRate.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }
    protected void grdItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblDiscountType = (Label)e.Row.FindControl("lblDiscountType");
                ddlDiscount.SelectedValue = lblDiscountType.Text;
                lblDiscountType.Text = ddlDiscount.SelectedItem.Text;
                //if (Convert.ToInt16(ddlDiscount.SelectedValue) == 1)
                //    lblDiscountType.Text = ddlDiscount.SelectedItem.Text;
                //else
                //    lblDiscountType.Text = ddlDiscount.SelectedItem.Text;
            }
        }
    }
    protected void btnCancelInvoice_Click(object sender, EventArgs e)
    {
        if (ddlInvoiceSeries.SelectedValue != "0")
        {
            pnlConfirmInvoice.Visible = true;
            pnlConfirmInvoice.Focus();
        }
        else
        {
            ShowMessage("Select Invoice Number.", false);
        }


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

            objCashSalesModel = new CashSalesModel();
            objCashSalesModel.Ind = 2;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.YrCD = GlobalSession.YrCD;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.User = GlobalSession.UserID;
            objCashSalesModel.IP = GlobalSession.IP;

            objCashSalesModel.EntryType = 1;

            objCashSalesModel.ByCashSales = 1;
            objCashSalesModel.CancelReason = ddlCancelReason.SelectedItem.Text;


            objCashSalesModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;


            objCashSalesModel.PartyName = ""; //txtPartyDetailPartyName.Text;
            objCashSalesModel.PartyMobileNo = 0;
            objCashSalesModel.PartyGSTIN = "";
            objCashSalesModel.PayMode = "";
            objCashSalesModel.PayModeRemark = "";

            objCashSalesModel.PartyAddress = ""; //ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
            //ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            objCashSalesModel.WareHouseID = 0;
            objCashSalesModel.PONo = "";
            objCashSalesModel.TransName = "";

            objCashSalesModel.DtCashSales = DtCashSalesSchema();
            objCashSalesModel.DtCashSales = CreateCashSalesDataCancel();

            objCashSalesModel.DtItems = DtItemsSchema();
            DataRow drItems = objCashSalesModel.DtItems.NewRow();
            drItems["ItemName"] = "";

            drItems["ItemUnit"] = "";
            drItems["ItemMinorUnit"] = "";
            drItems["PADesc"] = "";
            drItems["ISDDesc"] = "";
            drItems["ItemID"] = 0;
            drItems["HSNSACCode"] = "";
            drItems["GoodsServiceInd"] = 0;
            drItems["ItemQty"] = 0;
            drItems["FreeQty"] = 0.00;
            drItems["ItemUnitID"] = 0;
            drItems["ItemMinorUnitID"] = 0;
            drItems["ItemMinorQty"] = 0.00;
            drItems["ItemRate"] = 0.00;
            drItems["ItemAmount"] = 0.00;
            drItems["DiscountValue"] = 0.00; //0 Pending default;
            drItems["DiscountType"] = 0; //0 Pending default;
            drItems["DiscountAmt"] = 0.00;
            drItems["NetAmt"] = 0.00;
            drItems["PA"] = 0;
            drItems["TaxRate"] = 0.00;
            drItems["IGSTTax"] = 0.00;
            drItems["IGSTTaxAmt"] = 0.00;
            drItems["SGSTTax"] = 0.00;
            drItems["SGSTTaxAmt"] = 0.00;
            drItems["CGSTTax"] = 0.00;
            drItems["CGSTTaxAmt"] = 0.00;
            drItems["CESSTax"] = 0.00;
            drItems["CESSTaxAmt"] = 0.00;
            drItems["ISDApplicable"] = 0;
            drItems["ItemRemark"] = "";//0 Pending default;
            drItems["ExtraInd"] = 0;
            objCashSalesModel.DtItems.Rows.Add(drItems);



            string uri = string.Format("CashSales/CancelCashSalesVoucherNo");
            DataTable dtSaveCashSales = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
            if (dtSaveCashSales.Rows.Count > 0)
            {
                if (dtSaveCashSales.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();

                    string InvoiceNo, InvoiceDate, InvoiceName, LastVNO, InvoiceSeries = "";
                    InvoiceNo = dtSaveCashSales.Rows[0]["LastInvoiceNo"].ToString();
                    InvoiceDate = Convert.ToDateTime(dtSaveCashSales.Rows[0]["LastInvoiceDate"].ToString()).ToString("dd/MM/yyyy");
                    InvoiceName = dtSaveCashSales.Rows[0]["InvoiceName"].ToString();
                    LastVNO = dtSaveCashSales.Rows[0]["DocMaxNo"].ToString();
                    ShowMessage("This Invoice No. Is Cancel." + InvoiceNo, true);
                    pnlConfirmInvoice.Visible = false;

                    if (!string.IsNullOrEmpty(objCashSalesModel.InvoiceSeries))
                        InvoiceSeries = objCashSalesModel.InvoiceSeries + "-";

                    lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;


                    if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 2) //Serial No Auto Generate No.2
                    {
                        string LastInvoiceNo = "";
                        foreach (DataRow item in VsdtSeries.Rows)
                        {
                            if (CommonCls.ConvertIntZero(txtinvoiceNo.Text) == CommonCls.ConvertIntZero(item["InvoiceNo"]))
                            {
                                item["InvoiceNo"] = CommonCls.ConvertIntZero(InvoiceNo) + 1;
                                LastInvoiceNo = (CommonCls.ConvertIntZero(InvoiceNo) + 1).ToString();
                            }
                        }

                        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                        {
                            case 1: /// Manual Series
                                txtinvoiceNo.Text = LastInvoiceNo;
                                break;

                            case 2: /// Available Series
                                ddlInvoiceSeries.DataSource = VsdtSeries;
                                ddlInvoiceSeries.DataTextField = "Series";
                                ddlInvoiceSeries.DataBind();
                                if (VsdtSeries.Rows.Count > 1)
                                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                                if (ddlInvoiceSeries.SelectedValue == "0")
                                {
                                    txtinvoiceNo.Text = "";
                                }
                                else
                                {
                                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                                }
                                break;

                            case 3: /// Default Series
                                ddlInvoiceSeries.DataSource = VsdtSeries;
                                ddlInvoiceSeries.DataTextField = "Series";
                                ddlInvoiceSeries.DataBind();
                                if (VsdtSeries.Rows.Count > 1)
                                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                                if (ddlInvoiceSeries.SelectedValue == "0")
                                {
                                    txtinvoiceNo.Text = "";
                                }
                                else
                                {
                                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                                }
                                break;
                        }
                    }
                    else
                    {
                        txtinvoiceNo.Text = "";
                    }

                    //txtinvoiceNo.Text = ("CASH\\" + (CommonCls.ConvertLongZero(InvoiceNo) + 1)).ToString();
                    //txtinvoiceNo.Text = (CommonCls.ConvertLongZero(InvoiceNo) + 1).ToString();

                    ddlItemName.Focus();
                    CallReport(InvoiceNo, CommonCls.ConvertToDate(InvoiceDate), InvoiceName, InvoiceSeries, LastVNO);

                }
                else if (dtSaveCashSales.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Voucher No Duplicate.", false);
                }
            }
            else
            {
                ShowMessage("Record Not Save Please Try Again.", false);
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    private DataTable CreateCashSalesDataCancel()
    {
        DataTable dtCreateCashSalesData = new DataTable();

        dtCreateCashSalesData = DtCashSalesSchema(); //new DataTable();
        DataRow drCashSales = dtCreateCashSalesData.NewRow();

        drCashSales["AccCode"] = 0; //919000;
        drCashSales["AccGst"] = "";
        drCashSales["SalePurchaseCode"] = 0;
        drCashSales["AccPOSID"] = 0;
        drCashSales["WarehouseID"] = 0;
        drCashSales["OrderNo"] = 0;
        drCashSales["OrderDate"] = "";

        //string[] splitTXTInvoice = txtinvoiceNo.Text.Split('\\');
        //drCashSales["InvoiceNo"] = CommonCls.ConvertIntZero(splitTXTInvoice[1]);

        drCashSales["InvoiceNo"] = Convert.ToInt32(txtinvoiceNo.Text);
        drCashSales["InvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        drCashSales["TDSApplicable"] = 0;
        drCashSales["TCSApplicable"] = 0;
        drCashSales["RCMApplicable"] = 0;
        drCashSales["GrossAmt"] = 0;
        drCashSales["TaxAmt"] = 0;
        drCashSales["NetAmt"] = 0;
        drCashSales["RoundOffAmt"] = 0;

        drCashSales["TransportID"] = 0;
        drCashSales["VehicleNo"] = "";
        drCashSales["WayBillNo"] = 0;
        drCashSales["TransportDate"] = "";// + " " + txtTransportationDate.Text.Substring(11, 8);

        drCashSales["DocDesc"] = "";

        dtCreateCashSalesData.Rows.Add(drCashSales);

        return dtCreateCashSalesData;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
    }
    protected void chkDiscount_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
        {
            ddlItemName.Focus();
            chkDiscount.Checked = false;

        }

        if (chkDiscount.Checked == true)
        {
            ddlDiscount.SelectedValue = "1";
            ddlDiscount_SelectedIndexChanged(sender, e);
            ddlDiscount.Enabled = false;
            txtDiscount.Focus();
        }
        else
        {
            txtDiscount.Enabled = true;
            ddlDiscount.Enabled = true;
            txtDiscount.Text = "0";
            chkDiscount.Enabled = true;
        }

    }


    #region Broker
    protected void btnShowBroker_Click(object sender, EventArgs e)
    {
        if (pnlBroker.Visible)
        {
            pnlBroker.Visible = false;
            ddlBroker.SelectedValue = "0";
            ddlBroker_SelectedIndexChanged(sender, e);
        }
        else
        {
            pnlBroker.Visible = true;
        }
    }

    protected void ddlBroker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrokerAmount.Text = txtBrokerRate.Text = "";
            if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
            {
                ShowMessage("Item Can Not Be Null.", false);
                ddlBroker.ClearSelection();
                ddlItemName.Focus();
                return;
            }
            if (ddlBroker.SelectedValue != "0")
            {

                objCashSalesModel = new CashSalesModel();
                objCashSalesModel.Ind = 7;
                objCashSalesModel.OrgID = GlobalSession.OrgID;
                objCashSalesModel.BrID = GlobalSession.BrID;
                objCashSalesModel.AccCode = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
                string uri = string.Format("CashSales/FillBrokerDetail");
                DataSet dsBrokerGSTINDetail = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
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
                            ddlBrokerGSTIN.Items.Insert(0, new ListItem { Text = "-Select GSTIN-", Value = "0" });
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
            ShowMessage(ex.Message, false);
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
}