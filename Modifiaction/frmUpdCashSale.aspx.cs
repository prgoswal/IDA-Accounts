using AjaxControlToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdCashSale : System.Web.UI.Page
{
    UpdateCashSaleModel objCashSalesModel = new UpdateCashSaleModel();
    DataTable dtGrdOtherCharges, dtGrdAdditionalItems;
    #region Declarations

    int VoucharNo
    {
        get { return CommonCls.ConvertIntZero(ViewState["VoucharNo"]); }
        set { ViewState["VoucharNo"] = value; }
    }

    int rowIndexItem
    {
        get { return CommonCls.ConvertIntZero(ViewState["rowIndexItem"]); }
        set { ViewState["rowIndexItem"] = value; }
    }
    DataTable VsdtItems
    {
        get { return (DataTable)ViewState["dtItems"]; }
        set { ViewState["dtItems"] = value; }
    }
    DataTable VsItemNameList
    {
        get { return (DataTable)ViewState["ItemNameList"]; }
        set { ViewState["ItemNameList"] = value; }
    }
    DataTable VsdtItemUnits
    {
        get { return (DataTable)ViewState["dtItemUnits"]; }
        set { ViewState["dtItemUnits"] = value; }
    }
    DataTable VsdtGvItemDetail
    {
        get { return (DataTable)ViewState["dtGvItemDetail"]; }
        set { ViewState["dtGvItemDetail"] = value; }
    }
    DataTable VsdtNarration
    {
        get { return (DataTable)ViewState["dtNarration"]; }
        set { ViewState["dtNarration"] = value; }
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
                txtSearchInvoice.Focus();
                //if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
                //{
                //    btnSave.Visible = true;
                //    btnCancel.Visible = true;
                //}
                //if (Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || GlobalSession.IsAdmin == 1)
                //{
                //    btnSave.Visible = true;
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
            objCashSalesModel = new UpdateCashSaleModel();
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
        objCashSalesModel = new UpdateCashSaleModel();
        objCashSalesModel.Ind = 11;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.YrCD = GlobalSession.YrCD;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.ByCashSale = 1;
        string uri = string.Format("UpdateCashSales/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
        if (dsBindAll.Tables.Count > 0)
        {
            DataTable dtWareHouse = dsBindAll.Tables[0];
            VsdtNarration = dsBindAll.Tables[1];
            DataTable dtInvoiceNoDate = dsBindAll.Tables[2];
            DataTable dtIncomeHead = dsBindAll.Tables[3];
            DataTable dtPartyName = dsBindAll.Tables[4];
            DataTable dtSundriesHeadName = dsBindAll.Tables[5];
            VsItemNameList = dsBindAll.Tables[6];
            VsdtItemUnits = dsBindAll.Tables[7];
            DataTable dtTransport = dsBindAll.Tables[8];
            //DataTable dtInvoiceSeries = dsBindAll.Tables[9];
            VsdtSeries = dsBindAll.Tables[10];
            DataTable dtStates = dsBindAll.Tables[11];

            DataTable dtUserAuthenticate = dsBindAll.Tables[12];
            DataTable dtCashAccount = dsBindAll.Tables[13];
            DataTable dtBrokerList = dsBindAll.Tables[14];

            DataTable dtCostCenter = dsBindAll.Tables[15];

            // For Checking User Authenticate of Update Sales Invoice.
            if (dtUserAuthenticate.Rows.Count > 0)
            {
                if (CommonCls.ConvertIntZero(dtUserAuthenticate.Rows[0]["IsAuthorized"]) <= 0)
                {
                    ShowMessageOnPopUp("You are not Authenticate for Update Sales Invoice. Contact To Admin." + Environment.NewLine + " Press Yes For Going to Home Page.", false, "../Defaults/Default.aspx");
                    return;
                }
            }

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

            // For Last Invoice / Voucher No. Info Taken
            if (dtInvoiceNoDate.Rows.Count > 0)
            {
                if (dtInvoiceNoDate.Rows[0]["LastNo"].ToString() != "0")
                {
                    lblInvoiceAndDate.Text = "Last Invoice No. & Date : " + dtInvoiceNoDate.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtInvoiceNoDate.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                }
            }

            if (VsdtNarration.Rows.Count > 0)
            {
                ddlNarration.DataSource = VsdtNarration;
                ddlNarration.DataTextField = "NarrationDesc";
                ddlNarration.DataBind();
            }

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

            ddlInvoiceSeriesFind.Enabled = ddlInvoiceSeriesFind.Visible = false;
            txtInvoiceSeriesFind.Enabled = txtInvoiceSeriesFind.Visible = false;

            if (VsdtSeries.Rows.Count == 0 || CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]) == 0)
            {
                txtInvoiceSeries.Visible = false;
                ShowMessageOnPopUp("Your Invoice Series Not Set. Press Yes For Setting Invoice Series.", false, "../Modifiaction/frmUpdateProfileCreation.aspx");
                return;
            }

            if (VsdtSeries != null && VsdtSeries.Rows.Count > 0)
            {
                if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 1) //Serial No Auto Generate No.2
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
                        pnlInvoiceseries.Visible = false;
                        ddlInvoiceSeriesFind.Visible = ddlInvoiceSeriesFind.Enabled = false;
                        txtInvoiceSeriesFind.Visible = txtInvoiceSeriesFind.Enabled = true;

                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = false;
                        txtInvoiceSeries.Visible = true; txtInvoiceSeries.Enabled = true;

                        txtInvoiceSeriesFind.Text = VsdtSeries.Rows[0]["Series"].ToString();

                        break;

                    case 2: /// Available Series
                        pnlInvoiceseries.Visible = true;

                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
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


                        ddlInvoiceSeriesFind.DataSource = VsdtSeries;
                        ddlInvoiceSeriesFind.DataTextField = "Series";
                        ddlInvoiceSeriesFind.DataBind();

                        ddlInvoiceSeriesFind.Visible = ddlInvoiceSeriesFind.Enabled = true;
                        txtInvoiceSeriesFind.Visible = false;
                        break;

                    case 3: /// Default Series
                        //txtInvoiceSeriesFind.Text = VsdtSeries.Rows[0]["Series"].ToString();
                        pnlInvoiceseries.Visible = false;
                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
                        ddlInvoiceSeries.DataBind();
                        ddlInvoiceSeries.Visible = true;
                        txtInvoiceSeries.Visible = false;

                        ddlInvoiceSeriesFind.DataSource = VsdtSeries;
                        ddlInvoiceSeriesFind.DataTextField = "Series";
                        ddlInvoiceSeriesFind.DataBind();

                        ddlInvoiceSeriesFind.Visible = true; ddlInvoiceSeriesFind.Enabled = false;
                        txtInvoiceSeriesFind.Visible = false;
                        break;
                }
                txtinvoiceNo.Enabled = ddlInvoiceSeries.Enabled = false;

            }
            else
            {
                ddlInvoiceSeries.Visible = false;
            }

            #endregion
            //For Broker List
            if (dtBrokerList.Rows.Count > 0)
            {
                ddlBroker.DataSource = dtBrokerList;
                ddlBroker.DataTextField = "AccName";
                ddlBroker.DataValueField = "AccCode";
                ddlBroker.DataBind();
                ddlBroker.Items.Insert(0, new ListItem("-- Select Broker--", "0"));

            }
        }
        else
        {
            ddlInvoiceSeriesFind.Visible = ddlInvoiceSeries.Visible = false;
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
            FillGSTIN();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void FillGSTIN()
    {
        //tblShippingDetail.Visible = true;
        objCashSalesModel = new UpdateCashSaleModel();
        objCashSalesModel.Ind = 1;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.YrCD = GlobalSession.YrCD;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.AccCode = CommonCls.ConvertIntZero(ddlPartyName.SelectedValue);

        string uri = string.Format("UpdateCashSales/FillGistnNo");
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

    protected void ddlGstinNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGstinNo.SelectedValue != null)
        {
            if (ddlGstinNo.SelectedValue == "0")
            {
                ShowMessage("Select GSTIN No.", false);
                ddlGstinNo.Focus();
                return;
            }
        }
        FillShippingAddress();
        ddlGstinNo.Focus();
    }

    void FillShippingAddress()
    {
        objCashSalesModel = new UpdateCashSaleModel();
        objCashSalesModel.Ind = 4;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.YrCD = GlobalSession.YrCD;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.AccCode = CommonCls.ConvertIntZero(ddlPartyName.SelectedValue);
        objCashSalesModel.GSTIN = ddlGstinNo != null ?
                 ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
                 ddlGstinNo.SelectedValue : "" : "";

        string uri = string.Format("UpdateCashSales/FillShippingAddress");
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
            ddlTransportationType.Focus();
            tblTransportDetail.Visible = true;
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
                    objCashSalesModel = new UpdateCashSaleModel();
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

            GvRow.txtQty.Text = "";
            GvRow.txtItemTaxableAmt.Text = GvRow.txtDiscount.Text = GvRow.txtItemAmt.Text = "0";
            VsDtItemSellRate = VsdtItems = null;

            if (CommonCls.ConvertIntZero(GvRow.ddlItemName.SelectedValue) == 0)
                return;

            objCashSalesModel = new UpdateCashSaleModel();
            objCashSalesModel.Ind = 11;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.ItemID = CommonCls.ConvertIntZero(GvRow.ddlItemName.SelectedValue);

            if (!string.IsNullOrEmpty(ddlGstinNo.Text))
                objCashSalesModel.GSTIN = ddlGstinNo.SelectedValue;
            objCashSalesModel.PartyState = CommonCls.ConvertIntZero(ddlState.SelectedValue);

            objCashSalesModel.ByCashSale = 1;

            string uri = string.Format("UpdateCashSales/FillItemSellRate");
            DataSet dsSaleRate = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
            if (dsSaleRate.Tables.Count > 0)
            {
                VsdtItems = dsSaleRate.Tables[0];

                GvRow.ddlUnitName.SelectedValue = dsSaleRate.Tables[0].Rows[0]["ItemUnitID"].ToString();
                GvRow.txtRate.Text = dsSaleRate.Tables[0].Rows[0]["ItemSellingRate"].ToString();
                GvRow.ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();

                //GvRow.txtTax.Text = dsSaleRate.Tables[0].Rows[0]["TaxRate"].ToString();
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
                //    if (Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                //        GvRow.txtMinorUnitQty.Enabled = true;
                //    else
                //        GvRow.txtMinorUnitQty.Enabled = false;

                //    GvRow.ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();
                //}
                //else
                //{
                //    GvRow.ddlMinorUnit.SelectedValue = "0";
                //}

                if (GlobalSession.StockMaintaineByMinorUnit)
                {
                    GvRow.ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();
                    if (CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                    {
                        GvRow.txtMinorUnitQty.Text = "";//CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["ItemMinorUnitQty"]).ToString();
                        GvRow.txtMinorUnitQty.Enabled = true;//false;
                    }
                    else
                    {
                        GvRow.txtMinorUnitQty.Text = "";
                        GvRow.txtMinorUnitQty.Enabled = false;
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

                    GvRow.txtMinorUnitQty.Enabled = false;
                    GvRow.txtMinorUnitQty.Text = "";
                    GvRow.ddlMinorUnit.SelectedValue = "0";

                }

                //ViewState["HSNSACCode"] = Convert.ToInt32(dsItems.Tables[0].Rows[0]["HSNSACCode"].ToString());
            }
            GvRow.txtQty.Focus();
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
            decimal InsertedRate = CommonCls.ConvertDecimalZero(GvRow.txtRate.Text);

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
        GvRow.txtTax.Text = drRate["TaxRate"].ToString();
        GvRow.txtCGSTAmt.Text = drRate["CGSTRate"].ToString();
        GvRow.txtSGSTAmt.Text = drRate["SGSTRate"].ToString();
        GvRow.txtIGSTAmt.Text = drRate["IGSTRate"].ToString();
        GvRow.txtCESSAmt.Text = drRate["Cess"].ToString();
    }

    void CalculateRate()
    {
        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(GvRow.txtQty.Text);
        item.ItemFree = CommonCls.ConvertDecimalZero(GvRow.txtFree.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(GvRow.txtRate.Text);
        item.ItemDiscount = CommonCls.ConvertDecimalZero(GvRow.txtDiscount.Text);
        item.DiscountInPerc = Convert.ToInt16(GvRow.ddlDiscount.SelectedValue) == 1;

        StructItems GetItem = Calculation.CalculateRate(item);
        GvRow.txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
        GvRow.txtItemAmt.Text = GetItem.ItemAmount.ToString();
        GvRow.txtDiscountAmt.Text = GetItem.DiscountValue.ToString();
    }

    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(GvRow.txtItemTaxableAmt.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(GvRow.txtTax.Text);

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["Cess"]);

        StructItems GetItem = Calculation.TaxCal(item);
        GvRow.txtIGSTAmt.Text = (IgstAmt = GetItem.ItemIGSTAmt).ToString();
        GvRow.txtCGSTAmt.Text = (CgstAmt = GetItem.ItemCGSTAmt).ToString();
        GvRow.txtSGSTAmt.Text = (SgstAmt = GetItem.ItemSGSTAmt).ToString();
        GvRow.txtCESSAmt.Text = (CessAmt = GetItem.ItemCESSAmt).ToString();

        CgstRat = GetItem.ItemCGSTRate;
        SgstRat = GetItem.ItemSGSTRate;
        IgstRat = GetItem.ItemIGSTRate;
        CessRat = GetItem.ItemCESSRate;

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
        VsDtItemSellRate = VsdtItems = null;

        objCashSalesModel = new UpdateCashSaleModel();
        objCashSalesModel.Ind = 11;
        objCashSalesModel.OrgID = GlobalSession.OrgID;
        objCashSalesModel.BrID = GlobalSession.BrID;
        objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objCashSalesModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);
        objCashSalesModel.ByCashSale = 1;

        string uri = string.Format("UpdateCashSales/FillItemSellRate");
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
                //ddlItemName.Focus();
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
            drItem["FreeItemInd"] = 1; //This For FreeItemInd
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
            VsdtGvFreeItem.Rows.RemoveAt(rowIndex);
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

    #region  Invoice Searching

    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSearchInvoice.Text))
            {
                txtSearchInvoice.Focus();
                ShowMessage("Enter Invoice No.", false);
                return;
            }

            objCashSalesModel = new UpdateCashSaleModel();
            objCashSalesModel.Ind = 2;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.YrCD = GlobalSession.YrCD;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.InvoiceNo = CommonCls.ConvertIntZero(txtSearchInvoice.Text);

            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    objCashSalesModel.InvoiceSeries = "";//txtInvoiceSeriesFind.Text;
                    break;

                case 2: /// Available Series
                    if (ddlInvoiceSeriesFind == null)
                    {
                        ddlInvoiceSeriesFind.Focus();
                        ShowMessage("Select Invoice Series.", false);
                        return;
                    }
                    objCashSalesModel.InvoiceSeries = ddlInvoiceSeriesFind.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    objCashSalesModel.InvoiceSeries = "";//ddlInvoiceSeriesFind.SelectedItem.Text;
                    break;
            }

            string uri = string.Format("UpdateCashSales/SearchSaleInvoice");
            DataSet dsSearchInvoice = CommonCls.ApiPostDataSet(uri, objCashSalesModel);
            if (dsSearchInvoice.Tables.Count > 0)
            {
                if (dsSearchInvoice.Tables[0].Rows[0]["RecordCnt"].ToString() == "0")
                {
                    ShowMessage("Sale Invoice Not Found.", false);
                    return;
                }

                if (dsSearchInvoice.Tables[0].Rows[0]["RecordCnt"].ToString() == "2")
                {
                    ShowMessage("This Invoice No. Already Canceled.", false);
                    txtSearchInvoice.Focus();
                    return;
                }


                if (dsSearchInvoice.Tables[2].Rows.Count > 0)
                {
                    VsdtGvItemDetail = dsSearchInvoice.Tables[2];

                    DataRow[] drFree0 = dsSearchInvoice.Tables[2].Select("[FreeItemInd] = 0");
                    if (drFree0.Count() > 0)
                    {
                        VsdtGvItemDetail = dsSearchInvoice.Tables[2].Select("[FreeItemInd] = 0").CopyToDataTable();
                        gvItemDetail.DataSource = VsdtGvItemDetail;
                        gvItemDetail.DataBind();
                    }

                    DataRow[] drFree1 = dsSearchInvoice.Tables[2].Select("[FreeItemInd] = 1");
                    if (drFree1.Count() > 0)
                    {
                        VsdtGvFreeItem = dsSearchInvoice.Tables[2].Select("[FreeItemInd] = 1").CopyToDataTable();
                        gvFreeItem.DataSource = VsdtGvFreeItem;
                        gvFreeItem.DataBind();
                    }
                }

                if (dsSearchInvoice.Tables[1].Rows.Count > 0)
                {
                    ViewState["Check"] = dsSearchInvoice.Tables[1];
                    if (CommonCls.ConvertIntZero(dsSearchInvoice.Tables[1].Rows[0]["ByCashSale"]) == 0)
                    {
                        ShowMessageOnPopUp("This Invoice No. Is Credit Sale. Go To Update Credit Sale.", true, "../Modifiaction/frmUpdSales.aspx");
                        return;
                    }

                    if (GlobalSession.CCCode == 1)
                    {
                        ddlCostCenter.SelectedValue = CommonCls.ConvertIntZero(dsSearchInvoice.Tables[1].Rows[0]["CCCode"]).ToString();
                    }
                    FillInvoiceDetail(dsSearchInvoice.Tables[1].Rows[0]);
                }


                if (dsSearchInvoice.Tables[3].Rows.Count > 0)
                {
                    grdOtherCharge.DataSource = VsdtSundri = dsSearchInvoice.Tables[3];
                    grdOtherCharge.DataBind();
                }

                if (dsSearchInvoice.Tables[4].Rows.Count > 0)
                {


                    ddlBroker.SelectedValue = dsSearchInvoice.Tables[4].Rows[0]["BrokerCode"].ToString();
                    ddlBroker_SelectedIndexChanged(sender, e);
                    ddlBrokerGSTIN.SelectedValue = dsSearchInvoice.Tables[4].Rows[0]["BrokerGSTIN"].ToString();
                    txtBrokerAmount.Text = dsSearchInvoice.Tables[4].Rows[0]["BrokerageAmount"].ToString();
                    txtBrokerRate.Text = dsSearchInvoice.Tables[4].Rows[0]["BrokerageRate"].ToString();
                }
                CalculateTotalAmount();
                DefaultGridRowEdit();
                //ddlIncomeHead.Focus();
                gvItemDetail.Enabled = true;


                ddlFreeItemName.DataSource = VsItemNameList;
                ddlFreeItemName.DataTextField = "ItemName";
                ddlFreeItemName.DataValueField = "ItemID";
                ddlFreeItemName.DataBind();

                ddlFreeUnit.DataSource = VsdtItemUnits;
                ddlFreeUnit.DataTextField = "UnitName";
                ddlFreeUnit.DataValueField = "UnitID";
                ddlFreeUnit.DataBind();
                ddlFreeUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
                pnlBodyContent.Enabled = true;
                pnlSeraching.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                ShowMessage("Internal Server Error!", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void FillInvoiceDetail(DataRow drInInfo)
    {
        DataTable dtCheck = (DataTable)ViewState["Check"];

        if (ddlState.Items.FindByValue(drInInfo["PartyState"].ToString()) != null)
            ddlState.SelectedValue = drInInfo["PartyState"].ToString();
        else
            ddlState.SelectedValue = "0";

        if (ddlCashAccount.Items.FindByValue(drInInfo["AccountCode"].ToString()) != null)
            ddlCashAccount.SelectedValue = drInInfo["AccountCode"].ToString();
        else
        {
            if (ddlCashAccount.Items.FindByValue("0") != null)
                ddlCashAccount.SelectedValue = "0";
        }

        VoucharNo = CommonCls.ConvertIntZero(drInInfo["VoucharNo"].ToString());

        txtPartyDetailMobileNo.Text = drInInfo["PartyMobileNo"].ToString();
        if (ddlPaymentMode.Items.FindByText(drInInfo["PayMode"].ToString()) != null)
            ddlPaymentMode.SelectedValue = ddlPaymentMode.Items.FindByText(drInInfo["PayMode"].ToString()).Value;

        txtPMRemark.Text = drInInfo["PayModeRemark"].ToString();

        if (ddlPartyName.Items.FindByText(drInInfo["PartyName"].ToString()) != null)
            ddlPartyName.SelectedValue = ddlPartyName.Items.FindByText(drInInfo["PartyName"].ToString()).Value;
        else if (!string.IsNullOrEmpty(drInInfo["PartyName"].ToString()))
        {
            ddlPartyName.Items.Add(new ListItem(drInInfo["PartyName"].ToString()));
            ddlPartyName.SelectedValue = drInInfo["PartyName"].ToString();
        }

        FillGSTIN();
        if (ddlGstinNo.Items.FindByText(drInInfo["GSTIN"].ToString()) != null)
            ddlGstinNo.SelectedValue = ddlGstinNo.Items.FindByText(drInInfo["GSTIN"].ToString()).Value;
        else if (!string.IsNullOrEmpty(drInInfo["GSTIN"].ToString()) && drInInfo["GSTIN"].ToString().Length > 2)
        {
            //ddlGstinNo.Items.Add(new ListItem(drInInfo["GSTIN"].ToString(), drInInfo["GSTIN"].ToString().Substring(0, 2)));
            //ddlGstinNo.SelectedValue = drInInfo["GSTIN"].ToString().Substring(0, 2);

            ddlGstinNo.Items.Add(new ListItem(drInInfo["GSTIN"].ToString()));
            ddlGstinNo.SelectedValue = drInInfo["GSTIN"].ToString();

            ddlState.SelectedValue = ddlGstinNo.SelectedValue.Substring(0, 2);
        }

        //FillShippingAddress();

        if (ddlShippingAdd.Items.FindByValue(drInInfo["ACCPOSID"].ToString()) != null)
            ddlShippingAdd.SelectedValue = drInInfo["ACCPOSID"].ToString();
        else if (!string.IsNullOrEmpty(drInInfo["PartyAddress"].ToString()))
        {
            ddlShippingAdd.Items.Add(new ListItem(drInInfo["PartyAddress"].ToString()));
            ddlShippingAdd.SelectedValue = drInInfo["PartyAddress"].ToString();
        }
        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
        {
            case 1: /// Manual Series
                txtInvoiceSeries.Text = drInInfo["Series"].ToString();
                break;

            case 2: /// Available Series
                ddlInvoiceSeries.SelectedValue = drInInfo["Series"].ToString();
                break;

            case 3: /// Default Series
                ddlInvoiceSeries.SelectedValue = drInInfo["Series"].ToString();
                break;
        }
        txtinvoiceNo.Text = drInInfo["InvoiceNo"].ToString();
        txtInvoiceDate.Text = CommonCls.ConvertDateDB(drInInfo["InvoiceDate"].ToString());
        if (ddlLocation.Items.FindByValue(drInInfo["WareHouseID"].ToString()) != null)
            ddlLocation.SelectedValue = drInInfo["WareHouseID"].ToString();

        txtOrderNo.Text = drInInfo["PONo"].ToString();
        txtOrderDate.Text = CommonCls.ConvertDateDB(drInInfo["PODate"].ToString());
        ddlTDS.SelectedValue = drInInfo["TDSApplicable"].ToString();
        ddlTCS.SelectedValue = drInInfo["TCSApplicable"].ToString();
        ddlRCM.SelectedValue = drInInfo["RCMApplicable"].ToString();

        if (ddlTransportationType.Items.FindByValue(drInInfo["TransportID"].ToString()) != null)
            ddlTransportationType.SelectedValue = drInInfo["TransportID"].ToString();

        txtTransportationDate.Text = CommonCls.ConvertDateDB(drInInfo["TransportDate"].ToString()); //+ drInInfo["TransportDate"].ToString().Substring(10);
        txtTransportationVehicleNo.Text = drInInfo["VehicleNo"].ToString();
        txtTransportationName.Text = drInInfo["TransporterName"].ToString();


        if (VsdtNarration != null)
        {
            if (VsdtNarration.Rows.Count > 0)
            {
                DataRow[] dr = VsdtNarration.Select("NarrationDesc = '" + drInInfo["Narration"].ToString() + "'");
                if (dr.Count() <= 0)
                {
                    VsdtNarration.Rows.Add(drInInfo["Narration"].ToString());
                    ddlNarration.DataSource = VsdtNarration;
                    ddlNarration.DataBind();
                }
                ddlNarration.SelectedValue = drInInfo["Narration"].ToString();
            }
            else
            {
                if (!VsdtNarration.Columns.Contains("NarrationDesc"))
                {
                    VsdtNarration.Columns.Add("NarrationDesc", typeof(string));
                }
                VsdtNarration.Rows.Add(drInInfo["Narration"].ToString());
                ddlNarration.DataSource = VsdtNarration;
                ddlNarration.DataTextField = "NarrationDesc";
                ddlNarration.DataBind();
                ddlNarration.SelectedValue = drInInfo["Narration"].ToString();
            }
        }
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

            VsdtGvItemDetail = CommonCls.RemoveEmptyRows(VsdtGvItemDetail);

            if (!BtnSaveValidation())
            {
                return;
            }

            objCashSalesModel = new UpdateCashSaleModel();
            objCashSalesModel.Ind = 1;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.YrCD = GlobalSession.YrCD;
            objCashSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objCashSalesModel.User = GlobalSession.UserID;
            objCashSalesModel.IP = GlobalSession.IP;

            objCashSalesModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            objCashSalesModel.EntryType = 1;

            objCashSalesModel.ByCashSale = 1;

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

            objCashSalesModel.PartyAddress = ddlShippingAdd.SelectedItem == null ? "" : ddlShippingAdd.SelectedItem.Text; //ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
            //ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            objCashSalesModel.WareHouseID = CommonCls.ConvertIntZero(ddlLocation.SelectedValue);
            objCashSalesModel.PONo = "";

            objCashSalesModel.BrokerageID = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
            objCashSalesModel.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerRate.Text);
            objCashSalesModel.BrokerageGSTIN = string.IsNullOrEmpty(ddlBrokerGSTIN.SelectedValue) ? "" : ddlBrokerGSTIN.SelectedValue;
            objCashSalesModel.BrokerageAmount = CommonCls.ConvertDecimalZero(txtBrokerAmount.Text);

            objCashSalesModel.DocNo = VoucharNo;
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

            string uri = string.Format("UpdateCashSales/UpdateSaleVoucher");
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
                    ShowMessage("Record Save successfully for Invoice No. " + InvoiceNo, true);

                    if (!string.IsNullOrEmpty(objCashSalesModel.InvoiceSeries))
                        InvoiceSeries = objCashSalesModel.InvoiceSeries + "-";

                    lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;

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
            ShowMessage("Internal Server Error!", false);
        }
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
            GvRow.ddlItemName.Focus();
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
                SalesModel ObjSaleModel;
                ObjSaleModel = new SalesModel();
                ObjSaleModel.Ind = 6;
                ObjSaleModel.OrgID = GlobalSession.OrgID;
                ObjSaleModel.BrID = GlobalSession.BrID;
                ObjSaleModel.AccCode = CommonCls.ConvertIntZero(ddlPartyName.SelectedValue);

                if (CommonCls.ConvertIntZero(ddlPartyName.SelectedValue) == 0)
                {
                    return true;
                }
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

        drCashSales["AccCode"] = 919000;
        drCashSales["AccGst"] = ddlGstinNo.SelectedItem == null ? "" : ddlGstinNo.SelectedItem.Text;
        drCashSales["SalePurchaseCode"] = 930001;
        if (ddlShippingAdd.SelectedItem != null && CommonCls.ConvertIntZero(ddlShippingAdd.SelectedValue) != 0)
        {
            drCashSales["AccPOSID"] = Convert.ToInt32(ddlShippingAdd.SelectedValue);
        }
        drCashSales["WarehouseID"] = ddlLocation.SelectedItem != null ? CommonCls.ConvertIntZero(ddlLocation.SelectedValue) : 0;
        drCashSales["OrderNo"] = 0;
        drCashSales["OrderDate"] = "";
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
    }

    void ClearAll()
    {
        pnlBodyContent.Enabled = false;
        pnlSeraching.Enabled = true;

        ddlGstinNo.DataSource = new DataTable();
        ddlGstinNo.DataBind();
        GvRow.txtMinorUnitQty.Text = "";

        ddlCashAccount.ClearSelection();
        txtInvoiceSeriesFind.Text = txtSearchInvoice.Text = "";
        ddlInvoiceSeries.ClearSelection();
        txtInvoiceSeries.Text = txtinvoiceNo.Text = "";

        ddlPartyName.ClearSelection();
        ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        txtPartyDetailMobileNo.Text = "";
        txtInvoiceDate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
        VsdtGvItemDetail = VsdtSundri = VsdtGvFreeItem = null;
        gvItemDetail.DataSource = grdOtherCharge.DataSource = gvFreeItem.DataSource = new DataTable();
        gvItemDetail.DataBind();
        grdOtherCharge.DataBind();
        gvFreeItem.DataBind();
        ddlTransportationType.ClearSelection();
        txtTransportationDate.Text = txtTransportationVehicleNo.Text = txtTransportationName.Text = "";
        ddlOtherChargesHeadName.ClearSelection();
        ddlOtherChargesAddLess.ClearSelection();
        txtOtherChargesAmount.Text = "";
        ddlFreeItemName.ClearSelection();
        ddlFreeUnit.ClearSelection();
        txtFreeQty.Text = "";
        ddlPaymentMode.ClearSelection();
        txtPMRemark.Text = "";
        txtGross.Text = txtTax.Text = txtAddLess.Text = txtNetAmount.Text = txtBrokerRate.Text = txtBrokerAmount.Text = "";
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
        ddlCostCenter.ClearSelection();
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


    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }

    #region Item Details Change

    void CalculateTotalAmount()
    {
        var cal = Calculation.CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);

        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtGross.Text = cal.TotalGross.ToString();
        txtTax.Text = cal.TotalTaxable.ToString();
        txtNetAmount.Text = cal.TotalAllNet.ToString();
        txtItemAmount.Text = cal.ItemAmount.ToString();
        txtDiscountAmount.Text = cal.ItemDiscount.ToString();
        CalCulateBrokerAmount();
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
        dtItems.Columns.Add("FreeItemInd", typeof(int));

        return dtItems;
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {

            CalculateRate();
            TaxCal();
            if (GvRow.txtFree.Enabled)
                GvRow.txtFree.Focus();
            else
                GvRow.txtRate.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage("Select Item First!", false);
            //ShowMessage(ex.Message, false);
        }
    }
    protected void txtFree_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateRate();
            TaxCal();
            GvRow.txtRate.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        CalculateRate();
        TaxCal();
        GvRow.txtDiscount.Focus();
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        CalculateRate();
        TaxCal();
        GvRow.ddlDiscount.Focus();
    }
    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateRate();
        if (!string.IsNullOrEmpty(GvRow.txtQty.Text))
            TaxCal();

        GvRow.ddlDiscount.Focus();
    }
    protected void txtTax_TextChanged(object sender, EventArgs e)
    {
        TaxCal();
    }

    protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            rowIndexItem = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "RowEdit")
            {
                if (CommonCls.CountEmptyRows(VsdtGvItemDetail) != VsdtGvItemDetail.Rows.Count)
                {
                    VsdtGvItemDetail.Rows.RemoveAt(0);
                    rowIndexItem = rowIndexItem - 1;
                }
                gvItemDetail.DataSource = VsdtGvItemDetail;
                gvItemDetail.EditIndex = rowIndexItem;
                gvItemDetail.DataBind();
                gvItemDetail.Rows[rowIndexItem].CssClass = "edit_tr";
                ddlItemName_SelectedIndexChanged(sender, e);
                GetRowData();
                GridViewRow GVrow = gvItemDetail.Rows[rowIndexItem];
                ComboBox ddlItemName = (ComboBox)GVrow.FindControl("ddlItemName");
                ddlItemName.Focus();
                return;
            }
            else if (e.CommandName == "RemoveRow")
            {
                if (CommonCls.CountEmptyRows(VsdtGvItemDetail) != VsdtGvItemDetail.Rows.Count)
                {
                    VsdtGvItemDetail.Rows.RemoveAt(0);
                    rowIndexItem = rowIndexItem - 1;
                }
                VsdtGvItemDetail.Rows.RemoveAt(rowIndexItem);
                gvItemDetail.DataSource = VsdtGvItemDetail;
                gvItemDetail.DataBind();
            }
            else if (e.CommandName == "RowUpdate")
            {
                if (!AddRowItems(rowIndexItem))
                {
                    return;
                }
                gvItemDetail.Rows[rowIndexItem].CssClass = "edited_tr";
                //gvItemDetail.DataBind(); 
            }
            DefaultGridRowEdit();
            CalculateTotalAmount();
            rowIndexItem = 0;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void gvItemDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvItemDetail.EditIndex == e.Row.RowIndex)
            {
                DataRow drGvItem = VsdtGvItemDetail.Rows[e.Row.RowIndex];

                ComboBox ddlItemName = (ComboBox)e.Row.FindControl("ddlItemName");
                ddlItemName.DataSource = VsItemNameList;
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "ItemID";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlItemName.SelectedValue = drGvItem["ItemID"].ToString();
                ddlItemName.Enabled = true;


                DropDownList ddlUnitName = (DropDownList)e.Row.FindControl("ddlUnitName");
                ddlUnitName.DataSource = VsdtItemUnits;
                ddlUnitName.DataTextField = "UnitName";
                ddlUnitName.DataValueField = "UnitID";
                ddlUnitName.DataBind();
                ddlUnitName.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlUnitName.SelectedValue = drGvItem["ItemUnitID"].ToString();

                DropDownList ddlMinorUnit = (DropDownList)e.Row.FindControl("ddlMinorUnit");
                ddlMinorUnit.DataSource = VsdtItemUnits;
                ddlMinorUnit.DataTextField = "UnitName";
                ddlMinorUnit.DataValueField = "UnitID";
                ddlMinorUnit.DataBind();
                ddlMinorUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlMinorUnit.SelectedValue = drGvItem["ItemUnitID"].ToString();

                DropDownList ddlDiscount = (DropDownList)e.Row.FindControl("ddlDiscount");
                ddlDiscount.SelectedValue = drGvItem["DiscountType"].ToString();
            }

            if (e.Row.RowIndex >= 0)
            {
                Label txtDiscountType = (Label)e.Row.FindControl("txtDiscountType");
                if (txtDiscountType != null)
                {
                    if (txtDiscountType.Text == "1")
                        txtDiscountType.Text = "%";
                    else
                        txtDiscountType.Text = "Rs.";
                }
            }
        }
    }

    void DefaultGridRowEdit()
    {
        if (VsdtGvItemDetail == null)
        {
            VsdtGvItemDetail = DtItemsSchema();
        }
        if (CommonCls.CountEmptyRows(VsdtGvItemDetail) != VsdtGvItemDetail.Rows.Count)
        {
            VsdtGvItemDetail.Rows.RemoveAt(0);
            rowIndexItem = rowIndexItem - 1;
        }
        VsdtGvItemDetail.Rows.InsertAt(VsdtGvItemDetail.NewRow(), 0);
        gvItemDetail.DataSource = VsdtGvItemDetail;
        gvItemDetail.EditIndex = 0;
        gvItemDetail.DataBind();
        LinkButton btnSave = (LinkButton)gvItemDetail.Rows[0].FindControl("btnSave");
        ComboBox ddlItemName = (ComboBox)gvItemDetail.Rows[0].FindControl("ddlItemName");
        btnSave.Text = "Add";
        ddlItemName.Focus();

    }

    void GetRowData()
    {

        GvRow.ddlItemName.ClearSelection();
        GvRow.ddlUnitName.ClearSelection();
        GvRow.txtQty.Text = GvRow.txtFree.Text = GvRow.txtRate.Text = GvRow.txtItemAmt.Text =
        GvRow.txtDiscount.Text = GvRow.txtItemTaxableAmt.Text = GvRow.txtTax.Text = GvRow.txtCGSTAmt.Text =
        GvRow.txtSGSTAmt.Text = GvRow.txtIGSTAmt.Text = GvRow.txtCESSAmt.Text = GvRow.txtItemRemark.Text = "";

        DataRow editedRow = VsdtGvItemDetail.Rows[rowIndexItem];
        GvRow.ddlItemName.SelectedValue = editedRow["ItemID"].ToString();

        GvRow.txtQty.Text = editedRow["ItemQty"].ToString();
        GvRow.ddlUnitName.SelectedValue = editedRow["ItemUnitID"].ToString();
        GvRow.txtRate.Text = editedRow["ItemRate"].ToString();
        GvRow.ddlMinorUnit.SelectedValue = editedRow["ItemMinorUnitID"].ToString();
        GvRow.txtMinorUnitQty.Text = editedRow["ItemMinorQty"].ToString();
        GvRow.txtFree.Text = editedRow["FreeQty"].ToString();
        GvRow.txtItemAmt.Text = editedRow["ItemAmount"].ToString();
        GvRow.txtDiscount.Text = editedRow["DiscountValue"].ToString();
        GvRow.txtDiscountAmt.Text = editedRow["DiscountAmt"].ToString();
        GvRow.txtItemTaxableAmt.Text = editedRow["NetAmt"].ToString();
        GvRow.txtTax.Text = editedRow["TaxRate"].ToString();
        GvRow.txtIGSTAmt.Text = editedRow["IGSTTaxAmt"].ToString();
        GvRow.txtSGSTAmt.Text = editedRow["SGSTTaxAmt"].ToString();
        GvRow.txtCGSTAmt.Text = editedRow["CGSTTaxAmt"].ToString();
        GvRow.txtCESSAmt.Text = editedRow["CessTaxAmt"].ToString();
        GvRow.txtItemRemark.Text = editedRow["ItemRemark"].ToString();

    }

    void ClearGridItem()
    {
        GvRow.ddlItemName.ClearSelection();
        GvRow.ddlUnitName.ClearSelection();
        GvRow.txtQty.Text = GvRow.txtFree.Text = GvRow.txtRate.Text = GvRow.txtItemAmt.Text =
        GvRow.txtDiscount.Text = GvRow.txtItemTaxableAmt.Text = GvRow.txtTax.Text = GvRow.txtCGSTAmt.Text =
        GvRow.txtSGSTAmt.Text = GvRow.txtIGSTAmt.Text = GvRow.txtCESSAmt.Text = GvRow.txtItemRemark.Text = "";
    }

    public struct StructGVItems
    {
        public ComboBox ddlItemName { get; set; }
        public TextBox txtQty { get; set; }
        public DropDownList ddlUnitName { get; set; }
        public TextBox txtMinorUnitQty { get; set; }
        public DropDownList ddlMinorUnit { get; set; }
        public TextBox txtFree { get; set; }
        public TextBox txtRate { get; set; }
        public TextBox txtItemAmt { get; set; }
        public TextBox txtDiscount { get; set; }
        public DropDownList ddlDiscount { get; set; }
        public TextBox txtDiscountAmt { get; set; }
        public TextBox txtItemTaxableAmt { get; set; }
        public TextBox txtTax { get; set; }
        public TextBox txtCGSTAmt { get; set; }
        public TextBox txtSGSTAmt { get; set; }
        public TextBox txtIGSTAmt { get; set; }
        public TextBox txtCESSAmt { get; set; }
        public TextBox txtItemRemark { get; set; }

    }

    public StructGVItems GvRow
    {
        get { return CreateItemStruct(rowIndexItem); }
    }
    StructGVItems CreateItemStruct(int rowIndex)
    {
        StructGVItems GvStruc = new StructGVItems();

        GridViewRow GVrow = gvItemDetail.Rows[rowIndex];
        GvStruc.ddlItemName = (ComboBox)GVrow.FindControl("ddlItemName");
        GvStruc.txtQty = (TextBox)GVrow.FindControl("txtQty");
        GvStruc.ddlUnitName = (DropDownList)GVrow.FindControl("ddlUnitName");
        GvStruc.txtMinorUnitQty = (TextBox)GVrow.FindControl("txtMinorUnitQty");
        GvStruc.ddlMinorUnit = (DropDownList)GVrow.FindControl("ddlMinorUnit");
        GvStruc.txtFree = (TextBox)GVrow.FindControl("txtFree");
        GvStruc.txtRate = (TextBox)GVrow.FindControl("txtRate");
        GvStruc.txtItemAmt = (TextBox)GVrow.FindControl("txtItemAmt");
        GvStruc.txtDiscount = (TextBox)GVrow.FindControl("txtDiscount");
        GvStruc.ddlDiscount = (DropDownList)GVrow.FindControl("ddlDiscount");
        GvStruc.txtDiscountAmt = (TextBox)GVrow.FindControl("txtDiscountAmt");
        GvStruc.txtItemTaxableAmt = (TextBox)GVrow.FindControl("txtItemTaxableAmt");

        GvStruc.txtTax = (TextBox)GVrow.FindControl("txtTax");
        GvStruc.txtCGSTAmt = (TextBox)GVrow.FindControl("txtCGSTAmt");
        GvStruc.txtSGSTAmt = (TextBox)GVrow.FindControl("txtSGSTAmt");
        GvStruc.txtIGSTAmt = (TextBox)GVrow.FindControl("txtIGSTAmt");
        GvStruc.txtCESSAmt = (TextBox)GVrow.FindControl("txtCESSAmt");

        GvStruc.txtItemRemark = (TextBox)GVrow.FindControl("txtItemRemark");
        return GvStruc;
    }

    bool AddRowItems(int rowIndex)
    {
        if (GvRow.ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(GvRow.ddlItemName.SelectedValue) == 0)
        {
            GvRow.ddlItemName.Focus();
            ShowMessage("Select Item Name.", false);
            return false;
        }

        if (GvRow.ddlItemName == null || CommonCls.ConvertIntZero(GvRow.ddlItemName.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
        {
            GvRow.ddlItemName.Focus();
            ShowMessage("Item Value Not Available", false);
            return false;
        }

        if (CommonCls.ConvertDecimalZero(GvRow.txtQty.Text) == 0)
        {
            GvRow.txtQty.Focus();
            ShowMessage("Enter Item Quantity", false);
            return false;
        }
        if (CommonCls.ConvertIntZero(GvRow.ddlUnitName.SelectedValue) == 0)
        {
            GvRow.ddlUnitName.Focus();
            ShowMessage("Select Item Unit ", false);
            return false;
        }
        if (CommonCls.ConvertDecimalZero(GvRow.txtRate.Text) == 0)
        {
            GvRow.txtRate.Focus();
            ShowMessage("Enter Item Rate", false);
            return false;
        }
        if (CommonCls.ConvertIntZero(GvRow.ddlDiscount.SelectedValue) == 1)
        {
            if (CommonCls.ConvertDecimalZero(GvRow.txtDiscount.Text) > 100)
            {
                GvRow.txtDiscount.Focus();
                ShowMessage("Discount Not Greater Than 100%.", false);
                return false;
            }
        }
        if (CommonCls.ConvertDecimalZero(GvRow.txtDiscount.Text) > CommonCls.ConvertDecimalZero(GvRow.txtItemAmt.Text))
        {
            ShowMessage("Discount Not Greater Than To Net Amount.", false);
            CalculateRate();
            TaxCal();
            GvRow.txtDiscount.Focus();
            return false;
        }

        if (GlobalSession.StockMaintaineByMinorUnit)
        {
            if (VsdtItems != null && CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]) != 0)
            {
                if (CommonCls.ConvertDecimalZero(GvRow.txtMinorUnitQty.Text) == 0)
                {
                    ShowMessage("Enter Minor Unit Qty.", false);
                    GvRow.txtMinorUnitQty.Focus();
                    return false;
                }
            }
        }

        if (VsdtItems != null)
        {
            decimal TaxRate = 0;
            if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);
            else
                TaxRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["TaxRate"]);

            DataTable dtItems = VsdtItems;

            DataRow DrGvItemDetail = VsdtGvItemDetail.NewRow();
            TaxCal();
            DrGvItemDetail["ItemName"] = GvRow.ddlItemName.SelectedItem.Text;
            DrGvItemDetail["HSNSACCode"] = dtItems.Rows[0]["HSNSACCode"];
            DrGvItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(GvRow.txtQty.Text);
            DrGvItemDetail["FreeQty"] = CommonCls.ConvertDecimalZero(GvRow.txtFree.Text);
            DrGvItemDetail["ItemUnit"] = GvRow.ddlUnitName.SelectedItem.Text;
            DrGvItemDetail["ItemRate"] = Convert.ToDecimal(GvRow.txtRate.Text);
            DrGvItemDetail["ItemAmount"] = Convert.ToDecimal(GvRow.txtItemAmt.Text);

            DrGvItemDetail["ItemMinorUnit"] = CommonCls.ConvertIntZero(GvRow.ddlMinorUnit.SelectedValue) == 0 ? "" : GvRow.ddlMinorUnit.SelectedItem.Text; //DrGvItemDetail["ItemSecondaryUnit"]

            //DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(GvRow.txtMinorUnitQty.Text);
            //DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(GvRow.ddlMinorUnit.SelectedValue);
            if (GlobalSession.StockMaintaineByMinorUnit)
            {
                DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(GvRow.txtMinorUnitQty.Text);
                DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(GvRow.ddlMinorUnit.SelectedValue);
            }
            else
            {
                DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemMinorUnitID"].ToString());
                DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["ItemMinorUnitQty"].ToString());
            }

            DrGvItemDetail["DiscountValue"] = CommonCls.ConvertDecimalZero(GvRow.txtDiscount.Text);
            DrGvItemDetail["DiscountType"] = GvRow.ddlDiscount.SelectedValue;
            DrGvItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(GvRow.txtDiscountAmt.Text);

            DrGvItemDetail["NetAmt"] = Convert.ToDecimal(GvRow.txtItemTaxableAmt.Text);
            DrGvItemDetail["PADesc"] = "";
            DrGvItemDetail["TaxRate"] = TaxRate;

            DrGvItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGvItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGvItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGvItemDetail["CESSTaxAmt"] = CessAmt;

            DrGvItemDetail["ISDDesc"] = "";

            DrGvItemDetail["ItemID"] = Convert.ToInt32(GvRow.ddlItemName.SelectedValue);
            DrGvItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGvItemDetail["ItemUnitID"] = Convert.ToInt32(GvRow.ddlUnitName.SelectedValue);
            DrGvItemDetail["IGSTTax"] = IgstRat;
            DrGvItemDetail["SGSTTax"] = SgstRat;
            DrGvItemDetail["CGSTTax"] = CgstRat;
            DrGvItemDetail["CESSTax"] = CessRat;
            DrGvItemDetail["ItemRemark"] = GvRow.txtItemRemark.Text;
            DrGvItemDetail["ISDApplicable"] = 0;
            DrGvItemDetail["PA"] = 0;
            //DrGrdItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemMinorUnitID"].ToString());
            //DrGrdItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["ItemMinorUnitQty"].ToString());
            DrGvItemDetail["FreeItemInd"] = 0;//CommonCls.ConvertIntZero(dtItems.Rows[0]["StockMaintainInd"].ToString());

            VsdtGvItemDetail.Rows.RemoveAt(rowIndex);
            VsdtGvItemDetail.Rows.InsertAt(DrGvItemDetail, rowIndex);
            gvItemDetail.DataSource = VsdtGvItemDetail;
            gvItemDetail.DataBind();
            ddlFreeItemName.ClearSelection();
        }
        return true;
    }

    #endregion


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        pnlConfirmInvoice.Visible = true;
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

            objCashSalesModel = new UpdateCashSaleModel();
            objCashSalesModel.Ind = 12;
            objCashSalesModel.OrgID = GlobalSession.OrgID;
            objCashSalesModel.BrID = GlobalSession.BrID;
            objCashSalesModel.DocNo = 6;
            objCashSalesModel.InvoiceSeries = ddlInvoiceSeriesFind.SelectedItem.Text;
            objCashSalesModel.InvoiceNo = Convert.ToInt32(txtinvoiceNo.Text);
            objCashSalesModel.CancelReason = ddlCancelReason.SelectedItem.Text;

            string uri = string.Format("UpdateCashSales/CancelVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, objCashSalesModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Invoice No. - " + objCashSalesModel.InvoiceNo + " is Cancel successfully ", true);
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel for Invoice No. " + objCashSalesModel.InvoiceNo, true);

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
                return;
            }
            if (ddlBroker.SelectedValue != "0")
            {
                objCashSalesModel = new UpdateCashSaleModel();
                objCashSalesModel.Ind = 7;
                objCashSalesModel.OrgID = GlobalSession.OrgID;
                objCashSalesModel.BrID = GlobalSession.BrID;
                objCashSalesModel.AccCode = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
                string uri = string.Format("UpdateCashSales/FillBrokerDetail");
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