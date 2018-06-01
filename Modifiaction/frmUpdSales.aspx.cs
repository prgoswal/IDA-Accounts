using AjaxControlToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdSales : System.Web.UI.Page
{
    #region Declarations

    DataTable dtgrdview;
    UpdateSalesModel ObjSaleModel;


    string CashSalesAcc = "930001"; // Cash Sales Account Value For AccountHead.

    int VoucharNo
    {
        get { return (int)ViewState["VoucharNo"]; }
        set { ViewState["VoucharNo"] = value; }
    }

    int rowIndexItem
    {
        get { return (int)ViewState["rowIndexItem"]; }
        set { ViewState["rowIndexItem"] = value; }
    }

    DataTable VsdtSalesTo
    {
        get { return (DataTable)ViewState["dtSalesTo"]; }
        set { ViewState["dtSalesTo"] = value; }
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
    DataTable VsDtItemSellRate
    {
        get { return (DataTable)ViewState["DtItems"]; }
        set { ViewState["DtItems"] = value; }
    }
    DataTable VsdtSundriAccHead
    {
        get { return (DataTable)ViewState["dtSundriAccHead"]; }
        set { ViewState["dtSundriAccHead"] = value; }
    }
    DataTable VsdtNarration
    {
        get { return (DataTable)ViewState["dtNarration"]; }
        set { ViewState["dtNarration"] = value; }
    }
    DataTable VsdtSeries
    {
        get { return (DataTable)ViewState["dtSeries"]; }
        set { ViewState["dtSeries"] = value; }
    }
    DataTable VsdtGvFreeItem
    {
        get { return (DataTable)ViewState["dtGvFreeItem"]; }
        set { ViewState["dtGvFreeItem"] = value; }
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
                ViewState["VchType"] = 6;
                BindAll();
                BindCancelReason();

                if (!string.IsNullOrEmpty(Request.QueryString["InSer"]) && !string.IsNullOrEmpty(Request.QueryString["InNo"]))
                {
                    string InvoiceSeries = CommonCls.DecodePassword(Request.QueryString["InSer"].ToString());
                    string InvoiceNo = CommonCls.DecodePassword(Request.QueryString["InNo"].ToString());

                    if (ddlInvoiceSeriesFind.Visible == true)
                    {
                        ddlInvoiceSeriesFind.SelectedValue = InvoiceSeries;
                        txtSearchInvoice.Text = InvoiceNo;
                        btnSearchInvoice_Click(sender, e);
                    }
                    else
                    {
                        txtInvoiceSeriesFind.Text = InvoiceSeries;
                        txtSearchInvoice.Text = InvoiceNo;
                        btnSearchInvoice_Click(sender, e);
                    }
                    btnSave.Text = "Save";

                    if (ddlInvoiceSeries.SelectedItem != null)
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
                        }
                    }
                    else
                    {
                        txtinvoiceNo.Text = "";
                    }
                    ddlInvoiceSeries.Focus();
                    lblHeading.Text = "SALES VOUCHER";
                    divSearch.Visible = false;
                    btnClear.Visible = false;
                    btnBack.Visible = true;
                    btnBack.Enabled = true;

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
                else
                    ClearAll();

                txtSearchInvoice.Focus();
            }
            catch (Exception)
            {
                ShowMessage("Internal Server Error.", false);
                txtInvoiceSeriesFind.Visible = txtInvoiceSeries.Visible = false;
            }
        }
    }
    private void BindCancelReason()
    {
        try
        {
            ObjSaleModel = new UpdateSalesModel();
            ObjSaleModel.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
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

    string LastInvoiceNo;
    void BindAll()
    {
        ObjSaleModel = new UpdateSalesModel();
        ObjSaleModel.Ind = 11;
        ObjSaleModel.OrgID = GlobalSession.OrgID;
        ObjSaleModel.BrID = GlobalSession.BrID;
        ObjSaleModel.YrCD = GlobalSession.YrCD;
        ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("UpdateSalesVoucher/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
        if (dsBindAll.Tables.Count > 0)
        {
            DataTable dtWareHouse = dsBindAll.Tables[0];
            VsdtNarration = dsBindAll.Tables[1];
            DataTable dtInvoiceNoDate = dsBindAll.Tables[2];
            DataTable dtIncomeHead = dsBindAll.Tables[3];
            VsdtSalesTo = dsBindAll.Tables[4];
            VsdtSundriAccHead = dsBindAll.Tables[5];
            VsItemNameList = dsBindAll.Tables[6];
            VsdtItemUnits = dsBindAll.Tables[7];
            DataTable dtTransMode = dsBindAll.Tables[8];
            VsdtSeries = dsBindAll.Tables[10];
            DataTable dtCostCenter = dsBindAll.Tables[15];

            DataTable dtUserAuthenticate = dsBindAll.Tables[12];
            DataTable dtBrokerList = dsBindAll.Tables[14];
            // For Checking User Authenticate of Update Sales Invoice.
            if (dtUserAuthenticate.Rows.Count > 0)
            {
                if (CommonCls.ConvertIntZero(dtUserAuthenticate.Rows[0]["IsAuthorized"]) <= 0)
                {
                    ShowMessageOnPopUp("You are not Authenticate for Update Sales Invoice. Contact To Admin." + Environment.NewLine + " Press Yes For Going to Home Page.", false, "../Defaults/Default.aspx");
                    return;
                }
            }

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
                }
            }

            // For Narration Info Taken
            if (VsdtNarration.Rows.Count > 0)
            {
                txtNarration.DataSource = VsdtNarration;
                txtNarration.DataTextField = "NarrationDesc";
                txtNarration.DataBind();
            }

            // For Last Invoice / Voucher No. Info Taken
            if (dtInvoiceNoDate.Rows.Count > 0)
            {
                if (dtInvoiceNoDate.Rows[0]["LastNo"].ToString() != "0")
                {
                    lblInvoiceAndDate.Text = "Last Invoice No. & Date : " + dtInvoiceNoDate.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtInvoiceNoDate.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                    LastInvoiceNo = dtInvoiceNoDate.Rows[0]["LastNo"].ToString();
                }
            }

            // For Income Account Head List 
            if (dtIncomeHead.Rows.Count > 0)
            {
                ddlIncomeHead.DataSource = dtIncomeHead;
                ddlIncomeHead.DataTextField = "AccName";
                ddlIncomeHead.DataValueField = "AccCode";
                ddlIncomeHead.DataBind();
                if (dtIncomeHead.Rows.Count > 1)
                    ddlIncomeHead.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlIncomeHead.SelectedIndex = 0;
            }

            // For Sundries Account Head 
            if (VsdtSundriAccHead.Rows.Count > 0)
            {
                ddlHeadName.DataSource = VsdtSundriAccHead;
                ddlHeadName.DataTextField = "SundriHeadName";
                ddlHeadName.DataValueField = "AccCode";
                ddlHeadName.DataBind();
                ddlHeadName.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlHeadName.SelectedIndex = 0;

            }

            // For Transportation Mode 
            if (dtTransMode.Rows.Count > 0)
            {
                ddlTansportID.DataSource = dtTransMode;
                ddlTansportID.DataTextField = "TransportMode";
                ddlTansportID.DataValueField = "TransportID";
                ddlTansportID.DataBind();
                ddlTansportID.Items.Insert(0, new ListItem("-- Select --", "0"));
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
                ddlBroker.Items.Insert(0, new ListItem("-- Select Broker --", "0"));

            }

            if (GlobalSession.CCCode == 1)
            {
                thCCCode.Visible = true;
                tdCCCode.Visible = true;
                // Cost Center List
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
        else
        {
            ShowMessage("Internal Server Error!", false);
        }
    }

    #region Previouse Operations

    protected void ddlIncomeHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOnIncomeHead();
    }
    void FillOnIncomeHead()
    {
        if (ddlIncomeHead.SelectedValue == CashSalesAcc)
        {
            txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
            ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = false;
            txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = false;
            tblShippingDetail.Visible = true;
        }
        else
        {
            txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = true;
            txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
            ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
        }

        // For Debitor Account Head List 
        if (VsdtSalesTo.Rows.Count > 0)
        {
            ddlSalesto.DataSource = VsdtSalesTo;
            ddlSalesto.DataTextField = "AccName";
            ddlSalesto.DataValueField = "AccCode";
            ddlSalesto.DataBind();
        }

        txtInvoiceDate.Focus();
    }

    protected void txtSalesto_TextChanged(object sender, EventArgs e)
    {
        txtGstinNo.Focus();
    }

    protected void ddlSalesto_SelectedIndexChanged1(object sender, EventArgs e)
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
        if (ddlSalesto.SelectedValue == "" || ddlSalesto.SelectedValue == "0")
        {
            ShowMessage("Select Sales To.", false);
            return;
        }

        tblShippingDetail.Visible = true;
        ObjSaleModel = new UpdateSalesModel();
        ObjSaleModel.Ind = 1;
        ObjSaleModel.OrgID = GlobalSession.OrgID;
        ObjSaleModel.BrID = GlobalSession.BrID;
        ObjSaleModel.YrCD = GlobalSession.YrCD;
        ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjSaleModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);

        string uri = string.Format("UpdateSalesVoucher/FillGistnNo");
        DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
        if (dtGSTIN.Rows.Count > 0)
        {
            if (dtGSTIN.Rows.Count > 1)
            {
                ddlGstinNo.DataSource = dtGSTIN;
                ddlGstinNo.DataValueField = "GSTIN";
                ddlGstinNo.DataBind();
                ddlGstinNo.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                ddlGstinNo.SelectedIndex = 0;
                ddlGstinNo.Focus();

            }
            else
            {
                ddlGstinNo.DataSource = dtGSTIN;
                ddlGstinNo.DataValueField = "GSTIN";
                ddlGstinNo.DataBind();
                FillShippingAddress();
                ddlShippingAdd.Focus();
            }
        }
        else
        {
            ddlGstinNo.DataSource = dtGSTIN;
            ddlGstinNo.DataBind();
            FillShippingAddress();

            ddlShippingAdd.Focus();
        }

        DataTable dtSalesTo = VsdtSalesTo; // (DataTable)ViewState["dtSalesTo"];
        int TDS = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["TDSApplicable"].ToString());
        int TCS = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["ISDApplicable"].ToString());
        int RCM = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["RCMApplicable"].ToString());
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

    protected void txtGstinNo_TextChanged(object sender, EventArgs e)
    {

        if (txtGstinNo.Text.Length >= 1 && txtGstinNo.Text.Length < 15)
        {
            ShowMessage("Invalid GSTIN", false);
            txtGstinNo.Focus();
            return;
        }
        try
        {
            if (!string.IsNullOrEmpty(txtGstinNo.Text) && ddlIncomeHead.SelectedValue == CashSalesAcc)
            {
                ObjSaleModel = new UpdateSalesModel();
                ObjSaleModel.Ind = 15;
                string uri = string.Format("UpdateSalesVoucher/CheckState");
                DataTable dtState = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
                if (dtState.Rows.Count > 0)
                {
                    if (dtState.Select("StateID =" + txtGstinNo.Text.Substring(0, 2)).Count() == 0)
                    {
                        ShowMessage("Invalid GSTIN No.", false);
                        txtGstinNo.Focus();
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
            txtGstinNo.Text = "";
            txtGstinNo.Focus();
        }
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
        Filllocation();
        ddlGstinNo.Focus();
    }
    void FillShippingAddress()
    {
        ObjSaleModel = new UpdateSalesModel();
        ObjSaleModel.Ind = 4;
        ObjSaleModel.OrgID = GlobalSession.OrgID;
        ObjSaleModel.BrID = GlobalSession.BrID;
        ObjSaleModel.YrCD = GlobalSession.YrCD;
        ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjSaleModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);
        ObjSaleModel.GSTIN = ddlGstinNo != null ?
                 ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
                 ddlGstinNo.SelectedValue : "" : "";

        string uri = string.Format("UpdateSalesVoucher/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
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
    void Filllocation()
    {
        try
        {
            ObjSaleModel = new UpdateSalesModel();
            ObjSaleModel.Ind = 2;
            ObjSaleModel.OrgID = GlobalSession.OrgID;
            ObjSaleModel.BrID = GlobalSession.BrID;
            ObjSaleModel.YrCD = GlobalSession.YrCD;
            ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjSaleModel.GSTIN = "0";
            //ObjSaleModel.GSTIN = ddlGstinNo != null ?
            //    ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
            //    ddlGstinNo.SelectedValue : "" : "",


            string uri = string.Format("UpdateSalesVoucher/Filllocation");
            DataTable dtLocation = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
            if (dtLocation.Rows.Count > 0)
            {
                ddlLocation.DataSource = dtLocation;
                ddlLocation.DataTextField = "WareHouseAddress";
                ddlLocation.DataValueField = "WareHouseID";
                ddlLocation.DataBind();
                if (dtLocation.Rows.Count > 1)
                {
                    ddlLocation.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
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

            ObjSaleModel = new UpdateSalesModel();
            ObjSaleModel.Ind = 2;
            ObjSaleModel.OrgID = GlobalSession.OrgID;
            ObjSaleModel.BrID = GlobalSession.BrID;
            ObjSaleModel.YrCD = GlobalSession.YrCD;
            ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjSaleModel.InvoiceNo = CommonCls.ConvertIntZero(txtSearchInvoice.Text);

            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    ObjSaleModel.InvoiceSeries = "";//txtInvoiceSeriesFind.Text;
                    break;

                case 2: /// Available Series
                    if (ddlInvoiceSeriesFind == null)
                    {
                        ddlInvoiceSeriesFind.Focus();
                        ShowMessage("Select Invoice Series.", false);
                        return;
                    }
                    ObjSaleModel.InvoiceSeries = ddlInvoiceSeriesFind.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    ObjSaleModel.InvoiceSeries = "";//ddlInvoiceSeriesFind.SelectedItem.Text;
                    break;
            }

            string uri = string.Format("UpdateSalesVoucher/SearchSaleInvoice");
            DataSet dsSearchInvoice = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
            if (dsSearchInvoice.Tables.Count > 0)
            {
                ddlCostCenter.SelectedValue = dsSearchInvoice.Tables[1].Rows[0]["CCCode"].ToString();
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

                if (dsSearchInvoice.Tables[1].Rows.Count > 0)
                {
                    ViewState["Check"] = dsSearchInvoice.Tables[1];
                    if (CommonCls.ConvertIntZero(dsSearchInvoice.Tables[1].Rows[0]["ByCashSale"]) == 1)
                    {
                        ShowMessageOnPopUp("This Invoice No. Is Cash Sale. Go To Update Cash Sale.", true, "../Modifiaction/frmUpdCashSale.aspx");
                        return;
                    }


                    FillInvoiceDetail(dsSearchInvoice.Tables[1].Rows[0]);


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

                if (dsSearchInvoice.Tables[3].Rows.Count > 0)
                {
                    gvotherCharge.DataSource = VsdtSundri = dsSearchInvoice.Tables[3];
                    gvotherCharge.DataBind();
                }

                if (dsSearchInvoice.Tables[4].Rows.Count > 0)
                {
                    //ddlCostCenter.SelectedValue = dtgrdview.Rows[0]["CCCode"].ToString();
                    ddlBroker.SelectedValue = dsSearchInvoice.Tables[4].Rows[0]["BrokerCode"].ToString();
                    ddlBroker_SelectedIndexChanged(sender, e);
                    ddlBrokerGSTIN.SelectedValue = dsSearchInvoice.Tables[4].Rows[0]["BrokerGSTIN"].ToString();
                    txtBrokerAmount.Text = dsSearchInvoice.Tables[4].Rows[0]["BrokerageAmount"].ToString();
                    txtBrokerRate.Text = dsSearchInvoice.Tables[4].Rows[0]["BrokerageRate"].ToString();
                }
                CalculateTotalAmount();
                DefaultGridRowEdit();
                ddlIncomeHead.Focus();
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

        VoucharNo = CommonCls.ConvertIntZero(drInInfo["VoucharNo"].ToString());

        if (ddlIncomeHead.Items.FindByValue(drInInfo["PurchaseSaleCode"].ToString()) != null)
            ddlIncomeHead.SelectedValue = drInInfo["PurchaseSaleCode"].ToString();

        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
        {
            case 1: /// Manual Series
                txtInvoiceSeries.Text = drInInfo["Series"].ToString();
                break;

            case 2: /// Available Series
                if (ddlInvoiceSeries.Items.FindByValue(drInInfo["Series"].ToString()) != null)
                    ddlInvoiceSeries.SelectedValue = drInInfo["Series"].ToString();
                break;

            case 3: /// Default Series
                if (ddlInvoiceSeries.Items.FindByValue(drInInfo["Series"].ToString()) != null)
                    ddlInvoiceSeries.SelectedValue = drInInfo["Series"].ToString();
                break;
        }

        txtinvoiceNo.Text = drInInfo["InvoiceNo"].ToString();
        txtInvoiceDate.Text = CommonCls.ConvertDateDB(drInInfo["InvoiceDate"].ToString());

        FillOnIncomeHead();

        if (ddlIncomeHead.SelectedValue == CashSalesAcc)
        {
            txtSalesto.Text = drInInfo["PartyName"].ToString();
            txtGstinNo.Text = drInInfo["PartyGSTIN"].ToString();
            txtShippingAdd.Text = drInInfo["PartyAddress"].ToString();
        }
        else
        {
            if (ddlSalesto.Items.FindByValue(drInInfo["AccountCode"].ToString()) != null)
                ddlSalesto.SelectedValue = drInInfo["AccountCode"].ToString();
            FillGSTIN();

            if (ddlGstinNo.Items.FindByValue(drInInfo["GSTIN"].ToString()) != null)
                ddlGstinNo.SelectedValue = drInInfo["GSTIN"].ToString();

            FillShippingAddress();

            if (ddlShippingAdd.Items.FindByValue(drInInfo["ACCPOSID"].ToString()) != null)
                ddlShippingAdd.SelectedValue = drInInfo["ACCPOSID"].ToString();
        }

        if (ddlLocation.Items.FindByValue(drInInfo["WareHouseID"].ToString()) != null)
            ddlLocation.SelectedValue = drInInfo["WareHouseID"].ToString();



        txtorderNo.Text = drInInfo["PONo"].ToString();
        txtOrderDate.Text = CommonCls.ConvertDateDB(drInInfo["PODate"].ToString());
        ddlTds.SelectedValue = drInInfo["TDSApplicable"].ToString();
        ddlTCS.SelectedValue = drInInfo["TCSApplicable"].ToString();
        ddlRCM.SelectedValue = drInInfo["RCMApplicable"].ToString();
        CbTransDetail.Checked = Convert.ToBoolean(CommonCls.ConvertIntZero(drInInfo["TransportID"].ToString()) > 0);
        CBTransDetailInit();
        if (ddlTansportID.Items.FindByValue(drInInfo["TransportID"].ToString()) != null)
            ddlTansportID.SelectedValue = drInInfo["TransportID"].ToString();

        txtTransportDate.Text = CommonCls.ConvertDateDB(drInInfo["TransportDate"].ToString()); //+ drInInfo["TransportDate"].ToString().Substring(10);
        txtVehicleNo.Text = drInInfo["VehicleNo"].ToString();
        txtTransportName.Text = drInInfo["TransporterName"].ToString();





        if (VsdtNarration != null)
        {
            if (VsdtNarration.Rows.Count > 0)
            {
                DataRow[] dr = VsdtNarration.Select("NarrationDesc = '" + drInInfo["Narration"].ToString() + "'");
                if (dr.Count() <= 0)
                {
                    VsdtNarration.Rows.Add(drInInfo["Narration"].ToString());
                    txtNarration.DataSource = VsdtNarration;
                    txtNarration.DataBind();
                }
                txtNarration.SelectedValue = drInInfo["Narration"].ToString();
            }
            else
            {
                if (!VsdtNarration.Columns.Contains("NarrationDesc"))
                {
                    VsdtNarration.Columns.Add("NarrationDesc", typeof(string));
                }
                VsdtNarration.Rows.Add(drInInfo["Narration"].ToString());
                txtNarration.DataSource = VsdtNarration;
                txtNarration.DataTextField = "NarrationDesc";
                txtNarration.DataBind();
                txtNarration.SelectedValue = drInInfo["Narration"].ToString();
            }
        }
    }

    #endregion

    #region Sundri Op

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if ((ddlHeadName.SelectedItem == null) || (string.IsNullOrEmpty(ddlHeadName.SelectedValue)) || ddlHeadName.SelectedValue == "0")
        {
            ddlHeadName.Focus();
            ShowMessage("Select Head.", false);
            return;
        }

        if ((ddlAddLess.SelectedValue == "0") || string.IsNullOrEmpty(ddlAddLess.SelectedValue))
        {
            ddlAddLess.Focus();
            ShowMessage("Select Add/Less Option.", false);
            return;
        }
        if (string.IsNullOrEmpty(txtOtherChrgAmount.Text) || Convert.ToDecimal(txtOtherChrgAmount.Text) <= 0)
        {
            txtOtherChrgAmount.Focus();
            ShowMessage("Enter Amount.", false);
            return;
        }

        if (VsdtSundri != null) // First Time DataTable Create For Grid
        {
            dtgrdview = VsdtSundri;
            DataRow[] rows = dtgrdview.Select("SundriCode=" + ddlHeadName.SelectedValue);
            if (rows.Count() >= 1)
            {
                ShowMessage("This Charge Already.", false);
                return;
            }
        }

        if (VsdtSundri == null) // First Time DataTable Create For Grid
        {
            dtgrdview = DtSundriesSchema();
        }
        else
        {
            dtgrdview = VsdtSundri;
        }
        DataRow dr = dtgrdview.NewRow();
        dr["SundriHead"] = ddlHeadName.SelectedItem.Text;
        dr["SundriCode"] = ddlHeadName.SelectedValue;
        dr["SundriInd"] = ddlAddLess.SelectedItem.Text;
        dr["SundriAmt"] = txtOtherChrgAmount.Text;
        dtgrdview.Rows.Add(dr);
        VsdtSundri = dtgrdview;
        gvotherCharge.DataSource = dtgrdview;
        gvotherCharge.DataBind();
        CalculateTotalAmount();
        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";
        ddlHeadName.Focus();
    }
    protected void gvotherCharge_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtgrdview = VsdtSundri;
                dtgrdview.Rows[rowIndex].Delete();

                VsdtSundri = dtgrdview;
                gvotherCharge.DataSource = dtgrdview;
                gvotherCharge.DataBind();
                CalculateTotalAmount();
            }
        }
        catch (Exception ex)
        {

        }
    }
    void ClearotherCharge()
    {
        ddlHeadName.ClearSelection();
        txtOtherChrgAmount.Text = "";
    }

    protected void gvotherCharge_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RowEdit")
            {
                gvotherCharge.EditIndex = rowIndex;
                gvotherCharge.DataSource = VsdtSundri;
                gvotherCharge.DataBind();
                gvotherCharge.Rows[rowIndex].CssClass = "edit_tr";
            }
            else if (e.CommandName == "RemoveRow")
            {
                VsdtSundri.Rows[rowIndex].Delete();
                gvotherCharge.DataSource = VsdtSundri;
                gvotherCharge.DataBind();
            }
            else if (e.CommandName == "RowUpdate")
            {
                AddRowSundri(rowIndex);
                gvotherCharge.Rows[rowIndex].CssClass = "edited_tr";
            }
            CalculateTotalAmount();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void AddRowSundri(int rowIndex)
    {
        Label lblSundriCode = (Label)gvotherCharge.Rows[rowIndex].FindControl("lblSundriCode");
        ComboBox CbSundriHead = (ComboBox)gvotherCharge.Rows[rowIndex].FindControl("ddlSundriHead");
        DropDownList ddlAddLess = (DropDownList)gvotherCharge.Rows[rowIndex].FindControl("ddlAddLess");
        TextBox txtSundriAmt = (TextBox)gvotherCharge.Rows[rowIndex].FindControl("txtSundriAmt");

        DataRow drSundri = VsdtSundri.NewRow();
        drSundri["SundriCode"] = CbSundriHead.SelectedValue;
        drSundri["SundriHead"] = CbSundriHead.SelectedItem.Text;
        drSundri["SundriInd"] = ddlAddLess.SelectedValue;
        drSundri["SundriAmt"] = txtSundriAmt.Text;


        VsdtSundri.Rows[rowIndex].Delete();
        VsdtSundri.Rows.InsertAt(drSundri, rowIndex);

        gvotherCharge.EditIndex = -1;
        gvotherCharge.DataSource = VsdtSundri;
        gvotherCharge.DataBind();
    }

    protected void gvotherCharge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && gvotherCharge.EditIndex == e.Row.RowIndex)
        {
            DataRow drSundri = VsdtSundri.Rows[e.Row.RowIndex];

            ComboBox CbSundriHead = (ComboBox)e.Row.FindControl("ddlSundriHead");
            CbSundriHead.DataSource = VsdtSundriAccHead;
            CbSundriHead.DataValueField = "AccCode";
            CbSundriHead.DataTextField = "SundriHeadName";
            CbSundriHead.DataBind();
            CbSundriHead.SelectedValue = drSundri["SundriCode"].ToString();

            DropDownList ddlAddLess = (DropDownList)e.Row.FindControl("ddlAddLess");
            ddlAddLess.SelectedValue = drSundri["SundriInd"].ToString();

        }
    }

    #endregion

    #region Item Details Updation

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

    protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
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
            SelectionPA();
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

    void GetRowData()
    {

        GvRow.ddlItemName.ClearSelection();
        GvRow.ddlUnitName.ClearSelection();
        GvRow.ddlPA.ClearSelection();
        GvRow.ddlIsd.ClearSelection();
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
        GvRow.ddlPA.SelectedValue = editedRow["PA"].ToString();
        GvRow.txtTax.Text = editedRow["TaxRate"].ToString();
        GvRow.txtIGSTAmt.Text = editedRow["IGSTTaxAmt"].ToString();
        GvRow.txtSGSTAmt.Text = editedRow["SGSTTaxAmt"].ToString();
        GvRow.txtCGSTAmt.Text = editedRow["CGSTTaxAmt"].ToString();
        GvRow.txtCESSAmt.Text = editedRow["CessTaxAmt"].ToString();
        GvRow.ddlIsd.SelectedValue = editedRow["ISDApplicable"].ToString();
        GvRow.txtItemRemark.Text = editedRow["ItemRemark"].ToString();

    }

    void ClearGridItem()
    {
        GvRow.ddlItemName.ClearSelection();
        GvRow.ddlUnitName.ClearSelection();
        GvRow.ddlPA.ClearSelection();
        GvRow.ddlIsd.ClearSelection();
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
        public DropDownList ddlPA { get; set; }
        public TextBox txtTax { get; set; }
        public TextBox txtCGSTAmt { get; set; }
        public TextBox txtSGSTAmt { get; set; }
        public TextBox txtIGSTAmt { get; set; }
        public TextBox txtCESSAmt { get; set; }
        public DropDownList ddlIsd { get; set; }
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
        GvStruc.ddlPA = (DropDownList)GVrow.FindControl("ddlPA");
        GvStruc.txtTax = (TextBox)GVrow.FindControl("txtTax");
        GvStruc.txtCGSTAmt = (TextBox)GVrow.FindControl("txtCGSTAmt");
        GvStruc.txtSGSTAmt = (TextBox)GVrow.FindControl("txtSGSTAmt");
        GvStruc.txtIGSTAmt = (TextBox)GVrow.FindControl("txtIGSTAmt");
        GvStruc.txtCESSAmt = (TextBox)GVrow.FindControl("txtCESSAmt");
        GvStruc.ddlIsd = (DropDownList)GVrow.FindControl("ddlIsd");
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
            if (CommonCls.ConvertIntZero(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                TaxRate = CommonCls.ConvertDecimalZero(GvRow.txtTax.Text);
            else
                TaxRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["TaxRate"]);
            if (GvRow.ddlPA.SelectedValue == "1")
                TaxRate = CommonCls.ConvertDecimalZero(GvRow.txtTax.Text);
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
            DrGvItemDetail["PADesc"] = GvRow.ddlPA.SelectedItem.Text;
            DrGvItemDetail["TaxRate"] = TaxRate;

            DrGvItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGvItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGvItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGvItemDetail["CESSTaxAmt"] = CessAmt;

            DrGvItemDetail["ISDDesc"] = GvRow.ddlIsd.SelectedItem.Text;

            DrGvItemDetail["ItemID"] = Convert.ToInt32(GvRow.ddlItemName.SelectedValue);
            DrGvItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGvItemDetail["ItemUnitID"] = Convert.ToInt32(GvRow.ddlUnitName.SelectedValue);
            DrGvItemDetail["IGSTTax"] = IgstRat;
            DrGvItemDetail["SGSTTax"] = SgstRat;
            DrGvItemDetail["CGSTTax"] = CgstRat;
            DrGvItemDetail["CESSTax"] = CessRat;
            DrGvItemDetail["ItemRemark"] = GvRow.txtItemRemark.Text;
            DrGvItemDetail["ISDApplicable"] = Convert.ToInt16(GvRow.ddlIsd.SelectedValue);
            DrGvItemDetail["PA"] = Convert.ToInt16(GvRow.ddlPA.SelectedValue);
            DrGvItemDetail["FreeItemInd"] = 0;//CommonCls.ConvertIntZero(dtItems.Rows[0]["StockMaintainInd"].ToString());

            VsdtGvItemDetail.Rows.RemoveAt(rowIndex);
            VsdtGvItemDetail.Rows.InsertAt(DrGvItemDetail, rowIndex);
            gvItemDetail.DataSource = VsdtGvItemDetail;
            gvItemDetail.DataBind();
            ddlFreeItemName.ClearSelection();
        }
        return true;
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

                DropDownList ddlPA = (DropDownList)e.Row.FindControl("ddlPA");
                ddlPA.SelectedValue = drGvItem["PADesc"].ToString();

                DropDownList ddlIsd = (DropDownList)e.Row.FindControl("ddlIsd");
                ddlIsd.SelectedValue = drGvItem["ISDDesc"].ToString();

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

    /// <summary>
    /// All AMOUNT Calculation On Text Change.
    /// 0 ItemName 1 Qty, 2 Rate, 3 DISCOUNT, Tax.
    /// </summary>
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GvRow.txtQty.Text = "";
            GvRow.txtItemTaxableAmt.Text = GvRow.txtDiscount.Text = GvRow.txtItemAmt.Text = "0";
            VsDtItemSellRate = VsdtItems = null;

            ObjSaleModel = new UpdateSalesModel();
            ObjSaleModel.Ind = 11;
            ObjSaleModel.OrgID = GlobalSession.OrgID;
            ObjSaleModel.BrID = GlobalSession.BrID;
            ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjSaleModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());

            ObjSaleModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjSaleModel.ItemID = CommonCls.ConvertIntZero(GvRow.ddlItemName.SelectedValue);

            if (ddlGstinNo.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
                    ObjSaleModel.GSTIN = ddlGstinNo.SelectedValue;
            }
            if (ddlIncomeHead.SelectedValue == CashSalesAcc)
                ObjSaleModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

            string uri = string.Format("UpdateSalesVoucher/FillItemSellRate");
            DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
            if (dsItems.Tables[0].Rows.Count > 0)
            {
                VsdtItems = dsItems.Tables[0];

                GvRow.ddlUnitName.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
                GvRow.txtRate.Text = VsdtItems.Rows[0]["ItemSellingRate"].ToString();

                if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                {
                    VsDtItemSellRate = dsItems.Tables[1];
                    TaxWithInRange();
                }
                else
                    FillTaxRate(VsdtItems.Rows[0]);

                TaxCal();

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

                ddlSalesto.Enabled = ddlGstinNo.Enabled = ddlIncomeHead.Enabled = false;
                txtSalesto.Enabled = txtGstinNo.Enabled = false;
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

            //VsDtItemSellRate.Rows.RemoveAt(rowIndex - 1);
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
    protected void ddlPA_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectionPA();
        TaxCal();
        if (GvRow.txtTax.Enabled)
        {
            GvRow.txtTax.Focus();
        }
        else
        {
            GvRow.ddlPA.Focus();
        }
    }
    protected void txtTax_TextChanged(object sender, EventArgs e)
    {
        TaxCal();
        GvRow.ddlPA.SelectedValue = "1";
        GvRow.ddlIsd.Focus();
    }


    void SelectionPA()
    {
        if (GvRow.ddlPA.SelectedValue == "1")
            GvRow.txtTax.Enabled = true;
        else
            GvRow.txtTax.Enabled = false;
    }

    #endregion

    #region Transportation Details

    protected void CbTransDetail_CheckedChanged(object sender, EventArgs e)
    {
        CBTransDetailInit();
    }

    void CBTransDetailInit()
    {
        if (CbTransDetail.Checked)
        {
            txtTransportDate.Enabled = ddlTansportID.Enabled = txtVehicleNo.Enabled = txtTransportName.Enabled = true;
            ddlTansportID.Focus();
        }
        else
        {
            txtTransportDate.Enabled = ddlTansportID.Enabled = txtVehicleNo.Enabled = txtTransportName.Enabled = true;
            txtTransportDate.Text = txtVehicleNo.Text = txtTransportName.Text = "";
            ddlTansportID.ClearSelection();
        }
    }

    #endregion

    #region On Saving Sales

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            VsdtGvItemDetail = CommonCls.RemoveEmptyRows(VsdtGvItemDetail);

            bool IsValid = ValidationBTNSAVE();
            if (!IsValid)
            {
                return;
            }

            ObjSaleModel = new UpdateSalesModel();

            if (!string.IsNullOrEmpty(Request.QueryString["InSer"]) && !string.IsNullOrEmpty(Request.QueryString["InNo"]))
                ObjSaleModel.EntryType = 1;
            else
                ObjSaleModel.EntryType = 2;

            ObjSaleModel.Ind = 1;
            ObjSaleModel.OrgID = GlobalSession.OrgID;
            ObjSaleModel.BrID = GlobalSession.BrID;
            ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            ObjSaleModel.YrCD = GlobalSession.YrCD;
            ObjSaleModel.User = GlobalSession.UserID;
            ObjSaleModel.IP = GlobalSession.IP;

            ObjSaleModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjSaleModel.PartyName = txtSalesto.Text.ToUpper();
            ObjSaleModel.PartyGSTIN = txtGstinNo.Text.ToUpper();

            ObjSaleModel.PartyAddress = ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
                                        ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            ObjSaleModel.WareHouseID = CommonCls.ConvertIntZero(ddlLocation.SelectedValue);
            ObjSaleModel.TransName = txtTransportName.Text;
            ObjSaleModel.DocNo = VoucharNo;
            ObjSaleModel.PONo = txtorderNo.Text;
            ObjSaleModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);
            ObjSaleModel.BrokerageID = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
            ObjSaleModel.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerRate.Text);


            ObjSaleModel.BrokerageGSTIN = string.IsNullOrEmpty(ddlBrokerGSTIN.SelectedValue) ? "" : ddlBrokerGSTIN.SelectedValue;
            ObjSaleModel.BrokerageAmount = CommonCls.ConvertDecimalZero(txtBrokerAmount.Text);


            //ObjSaleModel.InvoiceSeries = txtInvoiceSeries.Text;
            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    ObjSaleModel.InvoiceSeries = txtInvoiceSeries.Text.ToUpper();
                    break;

                case 2: /// Available Series
                    ObjSaleModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    ObjSaleModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;
            }

            ObjSaleModel.DtSales = DtSalesSchema();
            ObjSaleModel.DtItems = DtItemsSchema();
            ObjSaleModel.DtSundries = DtSundriesSchema();

            ObjSaleModel.DtSales = CreateSaleData();
            if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            {
                foreach (DataRow item in VsdtGvFreeItem.Rows)
                {
                    VsdtGvItemDetail.Rows.Add(item.ItemArray);
                }
            }

            ObjSaleModel.DtItems = VsdtGvItemDetail;
            ObjSaleModel.DtSundries = VsdtSundri;

            if ((ObjSaleModel.DtSundries == null) || (ObjSaleModel.DtSundries.Rows.Count <= 0))
            {
                ObjSaleModel.DtSundries = DtSundriesSchema();
                DataRow drSaleSundri = ObjSaleModel.DtSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                ObjSaleModel.DtSundries.Rows.Add(drSaleSundri);
            }
            string uri = "";

            if (!string.IsNullOrEmpty(Request.QueryString["InSer"]) && !string.IsNullOrEmpty(Request.QueryString["InNo"]))
                uri = string.Format("UpdateSalesVoucher/SaveSalesVoucher");
            else
                uri = string.Format("UpdateSalesVoucher/UpdateSaleVoucher");

            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();

                    string InvoiceNo, InvoiceDate, InvoiceName, LastVNO, InvoiceSeries = "";
                    InvoiceNo = dtSave.Rows[0]["LastInvoiceNo"].ToString();
                    InvoiceDate = Convert.ToDateTime(dtSave.Rows[0]["LastInvoiceDate"].ToString()).ToString("dd/MM/yyyy");
                    InvoiceName = dtSave.Rows[0]["InvoiceName"].ToString();
                    LastVNO = dtSave.Rows[0]["DocMaxNo"].ToString();

                    ShowMessage("Record Save successfully for Invoice No. " + InvoiceNo, true);
                    if (!string.IsNullOrEmpty(ObjSaleModel.InvoiceSeries))
                        InvoiceSeries = ObjSaleModel.InvoiceSeries + "-";

                    lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;

                    if (!string.IsNullOrEmpty(Request.QueryString["InSer"]) && !string.IsNullOrEmpty(Request.QueryString["InNo"]))
                    {
                        if (VsdtSeries.Rows.Count > 0)
                        {
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
                        }
                    }

                    CallReport(InvoiceNo, CommonCls.ConvertToDate(InvoiceDate), InvoiceName, InvoiceSeries, LastVNO);
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Duplicate Invoice No.", false);
                    txtinvoiceNo.Focus();
                }
            }
            else
            {
                ShowMessage("Record Not Save Please Try Again.", false);
            }
            //ClientScript.RegisterStartupScript(this.GetType(), "loadDeActive", "LoadDeActive()", true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidationBTNSAVE()
    {
        if (string.IsNullOrEmpty(txtinvoiceNo.Text)) // Invoice Number Shouldn't be Null
        {
            txtinvoiceNo.Focus();

            ShowMessage("Enter Invoice No.", false);

            return false;
        }

        if (string.IsNullOrEmpty(txtInvoiceDate.Text))
        {
            txtInvoiceDate.Focus();
            ShowMessage("Enter Invoice Date.", false);
            return false;
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtInvoiceDate.Focus();
            ShowMessage("Voucher Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
            return false;
        }

        if (!string.IsNullOrEmpty(txtOrderDate.Text))
        {
            bool OrderDate = CommonCls.CheckFinancialYrDate(txtOrderDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return false;
            }

            if (string.IsNullOrEmpty(txtorderNo.Text))
            {
                txtorderNo.Focus();
                ShowMessage("Order No Compulsory If Order No Not Empty.", false);
                return false;
            }
        }


        if (GlobalSession.CCCode == 1)
        {
            if (ddlCostCenter.SelectedValue == "0")
            {
                ShowMessage("Select Cost Centre!", false);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtorderNo.Text))
        {
            if (string.IsNullOrEmpty(txtOrderDate.Text))
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Compulsory If Order Date Not Empty.", false);
                return false;
            }

            bool OrderDate = CommonCls.CheckFinancialYrDate(txtOrderDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return false;
            }
        }

        if (ddlLocation.Items.Count > 1)
        {
            if (ddlLocation.SelectedValue == "0" || ddlLocation.SelectedValue == "")
            {
                ShowMessage("Select Dispatch Location.", false);
                return false;
            }
        }

        if (ddlIncomeHead != null && ddlIncomeHead.Items.Count > 1)
        {
            if (ddlIncomeHead.SelectedIndex <= 0)
            {

                ShowMessage("Select Income Head.", false);
                return false;
            }
        }

        if ((ddlIncomeHead.SelectedValue != CashSalesAcc))
        {
            if (ddlSalesto == null || ddlSalesto.SelectedValue == "" || ddlSalesto.SelectedValue == "0")
            {
                ShowMessage("Sales Not Available.", false);
                return false;
            }
        }

        if (ddlIncomeHead.SelectedValue == CashSalesAcc && string.IsNullOrEmpty(txtSalesto.Text))
        {
            ShowMessage("Enter Sales To Name.", false);
            return false;
        }

        if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
        {
            ShowMessage("Item Can Not Be Null.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtTaxable.Text) || Convert.ToDecimal(txtTaxable.Text) < 0)
        {
            ShowMessage("Taxable Amount Can Not Be Negative Please Check Entry.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtNet.Text) || Convert.ToDecimal(txtNet.Text) < 0)
        {
            ShowMessage("Net Amount Can Not Be Negative Please Check Entry.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtGross.Text) || Convert.ToDecimal(txtGross.Text) < 0)
        {
            ShowMessage("Gross Amount Can Not Be Negative Please Check Entry.", false);
            return false;
        }

        if (CbTransDetail.Checked) // Transportation Validation
        {
            if (ddlTansportID.SelectedValue == "0")
            {
                ShowMessage("Select Transportation", false);
                return false;
            }


            if (string.IsNullOrEmpty(txtTransportDate.Text))
            {
                ShowMessage("Enter Transportation Date.", false);
                return false;
            }

            if (txtTransportDate.Text.Length != 10)
            {
                ShowMessage("Enter Valid Transport Date!", false);
                txtTransportDate.Focus();
                return false;
            }
            bool ValidTransDate = CommonCls.CheckFinancialYrDate(txtTransportDate.Text.Substring(0, 10), "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidTransDate)
            {
                ShowMessage("Enter Valid Date!", false);
                txtTransportDate.Focus();
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
                ObjSaleModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedItem.Value);

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

    DataTable CreateSaleData()
    {
        DataTable dtCreateSaleData = new DataTable();

        dtCreateSaleData = DtSalesSchema(); //new DataTable();
        DataRow drCreateSaleData = dtCreateSaleData.NewRow();

        //drCreateSaleData["DocDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        //drCreateSaleData["DocNo"] = 0;
        drCreateSaleData["AccCode"] = ddlIncomeHead.SelectedValue == CashSalesAcc ? 919000 : Convert.ToInt32(ddlSalesto.SelectedValue);
        drCreateSaleData["AccGst"] = ddlGstinNo.SelectedItem != null ? !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ? ddlGstinNo.SelectedValue : "" : "";
        drCreateSaleData["SalePurchaseCode"] = Convert.ToInt32(ddlIncomeHead.SelectedValue);
        if (ddlShippingAdd.SelectedItem != null && CommonCls.ConvertIntZero(ddlShippingAdd.SelectedValue) != 0)
        {
            drCreateSaleData["AccPOSID"] = Convert.ToInt32(ddlShippingAdd.SelectedValue);
        }
        // drCreateSaleData["GSTIN"] = "";
        drCreateSaleData["WarehouseID"] = ddlLocation.SelectedItem != null ? CommonCls.ConvertIntZero(ddlLocation.SelectedValue) : 0;
        drCreateSaleData["OrderNo"] = 0;//txtorderNo.Text; // !string.IsNullOrEmpty(txtorderNo.Text) ? Convert.ToInt32(txtorderNo.Text) : 0;
        drCreateSaleData["OrderDate"] = !string.IsNullOrEmpty(txtOrderDate.Text) ? CommonCls.ConvertToDate(txtOrderDate.Text) : "";
        drCreateSaleData["InvoiceNo"] = Convert.ToInt32(txtinvoiceNo.Text);
        drCreateSaleData["InvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        drCreateSaleData["TDSApplicable"] = Convert.ToInt32(ddlTds.SelectedValue);
        drCreateSaleData["TCSApplicable"] = Convert.ToInt32(ddlTCS.SelectedValue);
        drCreateSaleData["RCMApplicable"] = Convert.ToInt32(ddlRCM.SelectedValue);
        drCreateSaleData["GrossAmt"] = Convert.ToDecimal(txtGross.Text);
        drCreateSaleData["TaxAmt"] = Convert.ToDecimal(txtTaxable.Text);
        drCreateSaleData["NetAmt"] = Convert.ToDecimal(txtNet.Text);
        drCreateSaleData["RoundOffAmt"] = 0;

        drCreateSaleData["TransportID"] = CommonCls.ConvertIntZero(ddlTansportID.SelectedValue);
        drCreateSaleData["VehicleNo"] = txtVehicleNo.Text;
        drCreateSaleData["WayBillNo"] = CommonCls.ConvertIntZero(ddlTansportID.SelectedValue);
        //drCreateSaleData["TransportDate"] = CbTransDetail.Checked ? CommonCls.ConvertToDate(txtTransportDate.Text.Substring(0, 10)) + " " + txtTransportDate.Text.Substring(11, 8) : "";
        drCreateSaleData["TransportDate"] = txtTransportDate.Text.Length == 10 ? CommonCls.ConvertToDate(txtTransportDate.Text.Substring(0, 10)) : ""; //+ " " + txtTransportDate.Text.Substring(11, 8) : "";
        drCreateSaleData["DocDesc"] = txtNarration.Text;
        //drCreateSaleData["UserID"] = GlobalSession.UserID;
        //drCreateSaleData["IP"] = CommonCls.GetIP();

        dtCreateSaleData.Rows.Add(drCreateSaleData);


        return dtCreateSaleData;

    }

    #endregion

    #region All Calculation

    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {
        decimal MaxAmt = Convert.ToDecimal(GvRow.txtItemTaxableAmt.Text);
        decimal TaxBy;

        DataTable dtItems = new DataTable();

        if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
            dtItems = VsDtItemSellRate;
        else
            dtItems = VsdtItems;

        if (GvRow.ddlPA.SelectedValue == "1")
            TaxBy = CommonCls.ConvertDecimalZero(GvRow.txtTax.Text);
        else
            TaxBy = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(GvRow.txtItemTaxableAmt.Text);
        item.ItemRate = TaxBy;

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"]);

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

    void CalculateTotalAmount()
    {
        var cal = Calculation.CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);
        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtGross.Text = cal.TotalGross.ToString();
        txtTaxable.Text = cal.TotalTaxable.ToString();
        txtNet.Text = cal.TotalAllNet.ToString();
        txtItemAmount.Text = cal.ItemAmount.ToString();
        txtDiscountAmount.Text = cal.ItemDiscount.ToString();
        CalCulateBrokerAmount();

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

    #endregion

    #region Schemas

    DataTable DtSalesSchema()
    {
        DataTable dtSales = new DataTable();
        //dtSales.Columns.Add("DocDate", typeof(string));
        //dtSales.Columns.Add("DocNo", typeof(int));

        dtSales.Columns.Add("AccCode", typeof(int));
        dtSales.Columns.Add("AccGst", typeof(string));
        dtSales.Columns.Add("SalePurchaseCode", typeof(int));
        dtSales.Columns.Add("AccPOSID", typeof(int));
        dtSales.Columns.Add("GSTIN", typeof(string));
        dtSales.Columns.Add("WareHouseID", typeof(int));
        dtSales.Columns.Add("OrderNo", typeof(string));
        dtSales.Columns.Add("OrderDate", typeof(string));
        dtSales.Columns.Add("InvoiceNo", typeof(string));
        dtSales.Columns.Add("InvoiceDate", typeof(string));
        dtSales.Columns.Add("TDSApplicable", typeof(int));
        dtSales.Columns.Add("TCSApplicable", typeof(int));
        dtSales.Columns.Add("RCMApplicable", typeof(int));
        dtSales.Columns.Add("GrossAmt", typeof(decimal));
        dtSales.Columns.Add("TaxAmt", typeof(decimal));
        dtSales.Columns.Add("NetAmt", typeof(decimal));
        dtSales.Columns.Add("RoundOffAmt", typeof(decimal));
        dtSales.Columns.Add("TransportID", typeof(int));
        dtSales.Columns.Add("VehicleNo", typeof(string));
        dtSales.Columns.Add("WayBillNo", typeof(int));
        dtSales.Columns.Add("TransportDate", typeof(string));
        dtSales.Columns.Add("DocDesc", typeof(string));
        //dtSales.Columns.Add("UserID", typeof(int));
        //dtSales.Columns.Add("IP", typeof(string));

        return dtSales;
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

    #endregion

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
        ObjSaleModel = new UpdateSalesModel();
        ObjSaleModel.Ind = 1;
        ObjSaleModel.OrgID = GlobalSession.OrgID;
        ObjSaleModel.BrID = GlobalSession.BrID;
        ObjSaleModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjSaleModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());
        ObjSaleModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
        ObjSaleModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);

        if (ddlGstinNo.SelectedItem != null)
        {
            if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
                ObjSaleModel.GSTIN = ddlGstinNo.SelectedValue;
        }
        if (ddlIncomeHead.SelectedValue == CashSalesAcc)
            ObjSaleModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

        string uri = string.Format("UpdateSalesVoucher/FillItemSellRate");
        DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
        if (dsItems.Tables[0].Rows.Count > 0)
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


    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ClearAll()
    {


        if (!string.IsNullOrEmpty(Request.QueryString["InSer"]) && !string.IsNullOrEmpty(Request.QueryString["InNo"]))
        {
            pnlBodyContent.Enabled = true;
            btnSave.Enabled = false;
            btnBack.Enabled = true;
        }
        else
        {
            pnlBodyContent.Enabled = false;
            pnlSeraching.Enabled = true;
        }
        ddlInvoiceSeries.ClearSelection();
        txtinvoiceNo.Text = txtInvoiceSeries.Text = "";
        ddlInvoiceSeriesFind.ClearSelection();
        txtInvoiceSeriesFind.Text = "";
        VsdtGvFreeItem = null;
        gvFreeItem.DataSource = VsdtGvFreeItem;
        gvFreeItem.DataBind();

        gvItemDetail.Enabled = false;
        VoucharNo = 0;
        rowIndexItem = 0;

        ddlIncomeHead.ClearSelection();
        ddlSalesto.ClearSelection();
        ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        ddlLocation.ClearSelection();
        ddlGstinNo.DataSource = ddlShippingAdd.DataSource = new DataTable();
        ddlGstinNo.DataBind(); ddlShippingAdd.DataBind();

        if (hfSaleInvoiceManually.Value != "1")
        {
            txtinvoiceNo.Text = "";
        }

        txtInvoiceDate.Text = txtorderNo.Text = txtOrderDate.Text = txtBrokerRate.Text = txtBrokerAmount.Text = "";
        ddlTds.ClearSelection();
        ddlTCS.ClearSelection();
        ddlRCM.ClearSelection();

        VsdtGvItemDetail = VsdtItems = VsdtSundri = null;
        gvItemDetail.DataSource = gvotherCharge.DataSource = new DataTable();
        gvItemDetail.DataBind(); gvotherCharge.DataBind();

        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";

        txtGross.Text = txtTaxable.Text = txtNet.Text = txtAddLess.Text = txtDiscountAmount.Text = txtItemAmount.Text = "0";
        txtNarration.ClearSelection();
        lblMsg.Text = "";

        CbTransDetail.Checked = false;
        CBTransDetailInit();


        txtSalesto.Text = txtShippingAdd.Text = txtGstinNo.Text = "";

        //if (btnSave.Text == "Save")
        //    btnSave.Text = "Update";

        //ddlSalesto.Enabled = ddlGstinNo.Enabled = true;
        //txtSalesto.Enabled = txtGstinNo.Enabled = true;

        DefaultGridRowEdit();
        txtSearchInvoice.Text = "";
        txtSearchInvoice.Focus();
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
        HT.Add("Heading", "SALES INVOICE");

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
        VouchersReport.ReportHeading = "Sales Invoice";
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

    protected void btnShoOtherCharge_Click(object sender, EventArgs e)
    {
        if (pnlOtherCharge.Visible)
            pnlOtherCharge.Visible = false;
        else
            pnlOtherCharge.Visible = true;

        btnShoOtherCharge.Focus();

    }
    protected void btnShowTransport_Click(object sender, EventArgs e)
    {
        if (pnlTransport.Visible)
            pnlTransport.Visible = false;
        else
            pnlTransport.Visible = true;

        if (CbTransDetail.Checked)
            CbTransDetail.Checked = false;
        else
            CbTransDetail.Checked = true;

        //CBTransDetailInit();

        btnShowTransport.Focus();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../UserUtility/frmDuplicateInvoice.aspx");
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
                ShowMessage("Enter Cancel Reason", false);
                pnlConfirmInvoice.Visible = false;
                return;
            }
            ObjSaleModel = new UpdateSalesModel();
            ObjSaleModel.Ind = 12;
            ObjSaleModel.OrgID = GlobalSession.OrgID;
            ObjSaleModel.BrID = GlobalSession.BrID;
            ObjSaleModel.DocNo = 6;
            ObjSaleModel.InvoiceSeries = ddlInvoiceSeriesFind.SelectedItem.Text;
            ObjSaleModel.InvoiceNo = Convert.ToInt32(txtinvoiceNo.Text);
            ObjSaleModel.CancelReason = ddlCancelReason.SelectedItem.Text;

            string uri = string.Format("UpdateSalesVoucher/CancelVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjSaleModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is Cancel successfully for Invoice No. " + ObjSaleModel.InvoiceNo, true);
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel for Invoice No. " + ObjSaleModel.InvoiceNo, true);
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

                ObjSaleModel = new UpdateSalesModel();
                ObjSaleModel.Ind = 7;
                ObjSaleModel.OrgID = GlobalSession.OrgID;
                ObjSaleModel.BrID = GlobalSession.BrID;
                ObjSaleModel.AccCode = CommonCls.ConvertIntZero(ddlBroker.SelectedValue);
                string uri = string.Format("UpdateSalesVoucher/FillBrokerDetail");
                DataSet dsBrokerGSTINDetail = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
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

