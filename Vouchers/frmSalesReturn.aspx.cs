using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmSalesReturn : System.Web.UI.Page
{
    #region Declarations

    DataTable dtgrdview;
    SalesReturnModel ObjSaleRModel;
    string CashSalesAcc = "930001"; // Cash Sales Account Value For AccountHead.
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
    DataTable VsdtGvFinalItemDetail
    {
        get { return (DataTable)ViewState["dtGvFinalItemDetail"]; }
        set { ViewState["dtGvFinalItemDetail"] = value; }
    }
    DataTable VsDtItemSellRate
    {
        get { return (DataTable)ViewState["DtItems"]; }
        set { ViewState["DtItems"] = value; }
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
        try
        {
            lblMsg.CssClass = "";
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                ViewState["VchType"] = 8;
                ClearAll();
                BindAll();
                txtInvoiceSeries.Focus();
                txtVoucherDate.Text = CommonCls.ConvertDateDB(DateTime.Now);
                EnableOnIncomeHead();
                if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
                {
                    btnSave.Visible = false;

                }
                if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
                {
                    btnSave.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error: Internal Server Error!", false);
        }
    }

    string LastInvoiceNo;
    void BindAll()
    {
        ObjSaleRModel = new SalesReturnModel();
        ObjSaleRModel.Ind = 11;
        ObjSaleRModel.OrgID = GlobalSession.OrgID;
        ObjSaleRModel.BrID = GlobalSession.BrID;
        ObjSaleRModel.YrCD = GlobalSession.YrCD;
        ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("SalesReturn/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, ObjSaleRModel);
        if (dsBindAll.Tables.Count > 0)
        {
            DataTable dtWareHouse = dsBindAll.Tables[0];
            DataTable dtNarration = dsBindAll.Tables[1];
            DataTable dtInvoiceNoDate = dsBindAll.Tables[2];
            DataTable dtIncomeHead = dsBindAll.Tables[3];
            VsdtSalesTo = dsBindAll.Tables[4];
            DataTable dtSundrieAccHead = dsBindAll.Tables[5];
            VsItemNameList = dsBindAll.Tables[6];
            DataTable dtItemUnits = dsBindAll.Tables[7];
            DataTable dtTransMode = dsBindAll.Tables[8];
            DataTable dtInvoiceSeries = dsBindAll.Tables[9];
            VsdtSeries = dsBindAll.Tables[10];
            DataTable dtCostCenter = dsBindAll.Tables[12];
            if (VsItemNameList.Rows.Count <= 0 && VsdtSalesTo.Rows.Count <= 0)
            {
                txtInvoiceSeries.Visible = false;
                ShowMessageOnPopUp("Please Open Party Accounts & Item.  Press Yes For Open Party Accounts.", false, "../AdminMasters/frmAccountHead.aspx");
                return;
            }

            if (VsItemNameList.Rows.Count <= 0)
            {
                txtInvoiceSeries.Visible = false;
                ShowMessageOnPopUp("Please Open Item. Press Yes For Open Item Group", false, "../AdminMasters/frmGroupMaster.aspx");
                return;
            }

            if (VsdtSalesTo.Rows.Count <= 0)
            {
                txtInvoiceSeries.Visible = false;
                ShowMessageOnPopUp("Please Open  Party Accounts. Press Yes For Open Party Accounts.", false, "../AdminMasters/frmAccountHead.aspx");
                return;
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

            // For Narration Info Taken
            if (dtNarration.Rows.Count > 0)
            {
                txtNarration.DataSource = dtNarration;
                txtNarration.DataTextField = "NarrationDesc";
                txtNarration.DataBind();
            }

            // For Last Invoice / Voucher No. Info Taken
            if (dtInvoiceNoDate.Rows.Count > 0)
            {
                if (dtInvoiceNoDate.Rows[0]["LastNo"].ToString() != "0")
                {
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtInvoiceNoDate.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtInvoiceNoDate.Rows[0]["LastDate"]);
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

            //// For Free Item Unit 
            //if (dtItemUnits.Rows.Count > 0)
            //{
            //    ddlFreeUnit.DataSource = dtItemUnits;
            //    ddlFreeUnit.DataTextField = "UnitName";
            //    ddlFreeUnit.DataValueField = "UnitID";
            //    ddlFreeUnit.DataBind();
            //    ddlFreeUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            //}

            // For Item Secondary Unit 
            if (dtItemUnits.Rows.Count > 0)
            {
                ddlMinorUnit.DataSource = dtItemUnits;
                ddlMinorUnit.DataTextField = "UnitName";
                ddlMinorUnit.DataValueField = "UnitID";
                ddlMinorUnit.DataBind();
                ddlMinorUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
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

            #region Change IncomeHead Change

            // For Debitor Account Head List 
            if (VsdtSalesTo.Rows.Count > 0)
            {
                ddlSalesto.DataSource = VsdtSalesTo;
                ddlSalesto.DataTextField = "AccName";
                ddlSalesto.DataValueField = "AccCode";
                ddlSalesto.DataBind();
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

            //// Free Item List.
            //if (VsItemNameList.Rows.Count > 0)
            //{
            //    ddlFreeItemName.DataSource = VsItemNameList;
            //    ddlFreeItemName.DataTextField = "ItemName";
            //    ddlFreeItemName.DataValueField = "ItemID";
            //    ddlFreeItemName.DataBind();
            //    ddlFreeItemName.Items.Insert(0, new ListItem { Text = "-Select Item Name-", Value = "0" });
            //    ddlFreeItemName.SelectedIndex = 0;
            //}
            #endregion

            #region Series Selection

            if (VsdtSeries.Rows.Count == 0 || CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]) == 0)
            {
                txtInvoiceSeries.Visible = false;
                ShowMessageOnPopUp("Your Invoice Series Not Set. Press Yes For Setting Invoice Series.", false, "../Modifiaction/frmUpdateProfileCreation.aspx");
                return;
            }

            if (VsdtSeries != null && VsdtSeries.Rows.Count > 0)
            {

                foreach (DataRow drSeries in VsdtSeries.Rows)
                {
                    drSeries["InvoiceNo"] = CommonCls.ConvertIntZero(drSeries["InvoiceNo"]) - 1;
                }

                if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 1) //Serial No Auto Generate No.2
                {
                    txtSearchInvoice.Enabled = true;
                }
                else
                {
                    txtSearchInvoice.Text = (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["InvoiceNo"])).ToString();
                    //txtSearchInvoice.Enabled = false;
                }

                switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                {
                    case 1: /// Manual Series

                        pnlInvoiceseries.Visible = false;
                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = false;
                        txtInvoiceSeries.Visible = txtInvoiceSeries.Enabled = true;

                        break;

                    case 2: /// Available Series
                        pnlInvoiceseries.Visible = true;

                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
                        ddlInvoiceSeries.DataBind();
                        if (VsdtSeries.Rows.Count > 0)
                        {
                            ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                            txtSearchInvoice.Text = "";
                        }
                        else
                        {
                            txtSearchInvoice.Text = ddlInvoiceSeries.SelectedValue;
                        }

                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = true;
                        txtInvoiceSeries.Visible = false;

                        break;

                    case 3: /// Default Series
                        pnlInvoiceseries.Visible = false;

                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
                        ddlInvoiceSeries.DataBind();
                        ddlInvoiceSeries.Visible = false;
                        txtInvoiceSeries.Visible = false;
                        break;
                }

            }
            else
            {
                ddlInvoiceSeries.Visible = false;
            }

            #endregion

            ddlIncomeHead.Focus();
        }
    }

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

            ObjSaleRModel = new SalesReturnModel();
            ObjSaleRModel.Ind = 1;
            ObjSaleRModel.OrgID = GlobalSession.OrgID;
            ObjSaleRModel.BrID = GlobalSession.BrID;
            ObjSaleRModel.YrCD = GlobalSession.YrCD;
            //ObjSaleRModel.InvoiceSeries = txtInvoiceSeries.Text.Trim();
            ObjSaleRModel.InvoiceNo = Convert.ToInt32(txtSearchInvoice.Text);
            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    ObjSaleRModel.InvoiceSeries = "";//txtInvoiceSeriesFind.Text;
                    break;

                case 2: /// Available Series
                    if (ddlInvoiceSeries == null || string.IsNullOrEmpty(ddlInvoiceSeries.SelectedValue))
                    {
                        ddlInvoiceSeries.Focus();
                        ShowMessage("Select Invoice Series.", false);
                        return;
                    }
                    ObjSaleRModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    ObjSaleRModel.InvoiceSeries = "";//ddlInvoiceSeriesFind.SelectedItem.Text;
                    break;
            }
            string uri = string.Format("SalesReturn/LoadSRBasicDetails");
            DataSet dsSRBasicDetails = CommonCls.ApiPostDataSet(uri, ObjSaleRModel);
            if (dsSRBasicDetails.Tables.Count > 0)
            {
                DataTable dtBasicDetails = dsSRBasicDetails.Tables[0];
                DataTable dtItemDetails = dsSRBasicDetails.Tables[1];
                if (dtBasicDetails.Rows.Count > 0)
                {
                    if (dtBasicDetails.Rows[0]["ByCashSale"].ToString() == "1")
                    {
                        CommonCls.ShowModal("This Invoice No. Related To Cash Sale", true, "../Defaults/Default.aspx", "Go");
                        return;
                    }
                    if (GlobalSession.CCCode == 1)
                    {
                        ddlCostCenter.SelectedValue = CommonCls.ConvertIntZero(dtBasicDetails.Rows[0]["CCCode"]).ToString();
                        ddlCostCenter.Enabled = false;
                    }
                    ddlIncomeHead.SelectedValue = dtBasicDetails.Rows[0]["PurchaseSaleCode"].ToString();
                    ddlSalesto.SelectedValue = dtBasicDetails.Rows[0]["AccountCode"].ToString();
                    ddlSalesto_SelectedIndexChanged1(sender, e);
                    ddlGstinNo.SelectedValue = dtBasicDetails.Rows[0]["AccGst"].ToString();
                    ddlLocation.SelectedValue = dtBasicDetails.Rows[0]["WarehouseID"].ToString();
                    ddlTds.SelectedValue = dtBasicDetails.Rows[0]["TDSApplicable"].ToString();
                    ddlTCS.SelectedValue = dtBasicDetails.Rows[0]["TCSApplicable"].ToString();
                    ddlRCM.SelectedValue = dtBasicDetails.Rows[0]["RCMApplicable"].ToString();
                    hfInvoiceDate.Value = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["InvoiceDate"]);
                    hfVoucherNo.Value = dtBasicDetails.Rows[0]["VoucharNo"].ToString();
                    hfVoucherDate.Value = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["VoucharDate"]);
                }
                if (dtItemDetails.Rows.Count > 0)
                {
                    //dtItemDetails.Columns.Add("PADesc", typeof(string));
                    //dtItemDetails.Columns.Add("ISDDesc", typeof(string));

                    dtItemDetails.Columns["ItemName"].SetOrdinal(0);
                    dtItemDetails.Columns["ItemUnit"].SetOrdinal(1);
                    dtItemDetails.Columns["ItemMinorUnit"].SetOrdinal(2);
                    dtItemDetails.Columns["PADesc"].SetOrdinal(3);
                    dtItemDetails.Columns["ISDDesc"].SetOrdinal(4);
                    dtItemDetails.Columns["ItemID"].SetOrdinal(5);
                    dtItemDetails.Columns["HSNSACCode"].SetOrdinal(6);
                    dtItemDetails.Columns["GoodsServiceInd"].SetOrdinal(7);
                    dtItemDetails.Columns["ItemQty"].SetOrdinal(8);
                    dtItemDetails.Columns["FreeQty"].SetOrdinal(9);
                    dtItemDetails.Columns["ItemUnitID"].SetOrdinal(10);
                    dtItemDetails.Columns["ItemMinorUnitID"].SetOrdinal(11);
                    dtItemDetails.Columns["ItemMinorQty"].SetOrdinal(12);
                    dtItemDetails.Columns["ItemRate"].SetOrdinal(13);
                    dtItemDetails.Columns["ItemAmount"].SetOrdinal(14);
                    dtItemDetails.Columns["DiscountValue"].SetOrdinal(15);
                    dtItemDetails.Columns["DiscountType"].SetOrdinal(16);
                    dtItemDetails.Columns["DiscountAmt"].SetOrdinal(17);
                    dtItemDetails.Columns["NetAmt"].SetOrdinal(18);
                    dtItemDetails.Columns["PA"].SetOrdinal(19);
                    dtItemDetails.Columns["TaxRate"].SetOrdinal(20);
                    dtItemDetails.Columns["IGSTTax"].SetOrdinal(21);
                    dtItemDetails.Columns["IGSTTaxAmt"].SetOrdinal(22);
                    dtItemDetails.Columns["SGSTTax"].SetOrdinal(23);
                    dtItemDetails.Columns["SGSTTaxAmt"].SetOrdinal(24);
                    dtItemDetails.Columns["CGSTTax"].SetOrdinal(25);
                    dtItemDetails.Columns["CGSTTaxAmt"].SetOrdinal(26);
                    dtItemDetails.Columns["CESSTax"].SetOrdinal(27);
                    dtItemDetails.Columns["CESSTaxAmt"].SetOrdinal(28);
                    dtItemDetails.Columns["ISDApplicable"].SetOrdinal(29);
                    dtItemDetails.Columns["ItemRemark"].SetOrdinal(30);
                    dtItemDetails.Columns["ExtraInd"].SetOrdinal(31);

                    gvItemDetail.DataSource = VsdtGvItemDetail = dtItemDetails;
                    gvItemDetail.DataBind();
                }
                if (dtBasicDetails.Rows.Count > 0 && dtItemDetails.Rows.Count > 0)
                {
                    txtInvoiceSeries.Enabled = txtSearchInvoice.Enabled = btnSearchInvoice.Enabled = false;
                    btnAdd.Enabled = btnSave.Enabled = true;
                    txtVoucherDate.Focus();
                }
                else
                {
                    ShowMessage("Invalid Invoice Series And Invoice No.", false);
                    txtInvoiceSeries.Focus();
                    return;
                }
            }
            else
            {
                ShowMessage("Invalid Invoice Series And Invoice No.", false);
                txtInvoiceSeries.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //protected void ddlInvoiceSeries_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlInvoiceSeries.SelectedIndex != 0)
    //    {
    //        if (CommonCls.ConvertIntZero(VsdtSeries.Rows[ddlInvoiceSeries.SelectedIndex - 1]["SerailNoInd"]) == 1)
    //        {
    //            txtSearchInvoice.Enabled = true;
    //            txtSearchInvoice.Text = "";
    //        }
    //        else
    //        {
    //            DataRow dr = VsdtSeries.Rows[ddlInvoiceSeries.SelectedIndex - 1];
    //            txtSearchInvoice.Text = dr["InvoiceNo"].ToString();//ddlInvoiceSeries.SelectedValue;
    //            txtSearchInvoice.Enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        txtSearchInvoice.Text = "";
    //    }
    //    ddlInvoiceSeries.Focus();
    //}

    #region Previous Op

    protected void ddlIncomeHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIncomeHead.SelectedValue == CashSalesAcc)
        {
            txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
            ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = false;
            txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = txtFree.Enabled = false;
            tblShippingDetail.Visible = true;
        }
        else
        {
            txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = txtFree.Enabled = true;
            txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
            ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
        }

        //// For Debitor Account Head List 
        //if (VsdtSalesTo.Rows.Count > 0)
        //{
        //    ddlSalesto.DataSource = VsdtSalesTo;
        //    ddlSalesto.DataTextField = "AccName";
        //    ddlSalesto.DataValueField = "AccCode";
        //    ddlSalesto.DataBind();    
        //}

        //// For Item List
        //if (VsItemNameList.Rows.Count > 0)
        //{
        //    ddlItemName.DataSource = VsItemNameList;
        //    ddlItemName.DataTextField = "ItemName";
        //    ddlItemName.DataValueField = "ItemID";
        //    ddlItemName.DataBind();
        //    ddlItemName.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
        //    ddlItemName.SelectedIndex = 0;
        //}

        //// Free Item List.
        //if (VsItemNameList.Rows.Count > 0)
        //{
        //    ddlFreeItemName.DataSource = VsItemNameList;
        //    ddlFreeItemName.DataTextField = "ItemName";
        //    ddlFreeItemName.DataValueField = "ItemID";
        //    ddlFreeItemName.DataBind();
        //    ddlFreeItemName.Items.Insert(0, new ListItem { Text = "-Select Item Name-", Value = "0" });
        //    ddlFreeItemName.SelectedIndex = 0;
        //}

        ////if (txtInvoiceSeries.Enabled)
        ////    txtInvoiceSeries.Focus();
        ////else 
        ////    txtVoucherDate.Focus();     
        ddlIncomeHead.Focus();
    }
    void EnableOnIncomeHead()
    {
        if (ddlIncomeHead.SelectedValue != CashSalesAcc)
        {
            //txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
            //ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = false;
            //txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = txtFree.Enabled = false;
            tblShippingDetail.Visible = true;

            txtorderNo.Enabled = txtOrderDate.Enabled = true;
            ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
        }
        else
        {
            txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
            txtorderNo.Enabled = txtOrderDate.Enabled = true;
            ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
        }
    }

    protected void ddlSalesto_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlSalesto.SelectedValue == "" || ddlSalesto.SelectedValue == "0")
            {
                ShowMessage("Select Sales To.", false);
                return;
            }

            tblShippingDetail.Visible = true;
            ObjSaleRModel = new SalesReturnModel();
            ObjSaleRModel.Ind = 1;
            ObjSaleRModel.OrgID = GlobalSession.OrgID;
            ObjSaleRModel.BrID = GlobalSession.BrID;
            ObjSaleRModel.YrCD = GlobalSession.YrCD;
            ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjSaleRModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);

            string uri = string.Format("SalesReturn/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, ObjSaleRModel);
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

                    //ddlItemName.Enabled = false;
                }
                else
                {
                    ddlGstinNo.DataSource = dtGSTIN;
                    ddlGstinNo.DataValueField = "GSTIN";
                    ddlGstinNo.DataBind();
                    FillShippingAddress();
                    ddlShippingAdd.Focus();
                    //ddlItemName.Enabled = true;
                }
            }
            else
            {
                ddlGstinNo.DataSource = dtGSTIN;
                ddlGstinNo.DataBind();
                FillShippingAddress();

                ddlShippingAdd.Focus();
                //ddlItemName.Enabled = true;
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
            //ddlGstinNo.Focus();
            //ddlSalesto.Focus();
            //ddlItemName.Enabled = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void txtSalesto_TextChanged(object sender, EventArgs e)
    {
        //ddlItemName.Enabled = true;
        txtGstinNo.Focus();
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
        ddlItemName.Enabled = true;
        FillShippingAddress();
        Filllocation();
        ddlGstinNo.Focus();
    }
    void FillShippingAddress()
    {
        ObjSaleRModel = new SalesReturnModel();
        ObjSaleRModel.Ind = 4;
        ObjSaleRModel.OrgID = GlobalSession.OrgID;
        ObjSaleRModel.BrID = GlobalSession.BrID;
        ObjSaleRModel.YrCD = GlobalSession.YrCD;
        ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjSaleRModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);
        ObjSaleRModel.GSTIN = ddlGstinNo != null ?
                 ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
                 ddlGstinNo.SelectedValue : "" : "";

        string uri = string.Format("SalesReturn/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, ObjSaleRModel);
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
            ObjSaleRModel = new SalesReturnModel();
            ObjSaleRModel.Ind = 2;
            ObjSaleRModel.OrgID = GlobalSession.OrgID;
            ObjSaleRModel.BrID = GlobalSession.BrID;
            ObjSaleRModel.YrCD = GlobalSession.YrCD;
            ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjSaleRModel.GSTIN = "0";
            //ObjSaleModel.GSTIN = ddlGstinNo != null ?
            //    ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
            //    ddlGstinNo.SelectedValue : "" : "",


            string uri = string.Format("SalesReturn/Filllocation");
            DataTable dtLocation = CommonCls.ApiPostDataTable(uri, ObjSaleRModel);
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
                ObjSaleRModel = new SalesReturnModel();
                ObjSaleRModel.Ind = 15;
                string uri = string.Format("Master/Master");
                DataTable dtState = CommonCls.ApiPostDataTable(uri, ObjSaleRModel);
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

    #endregion

    #region Sundri Op
    /// <summary>
    ///  GV Sundrie Oprations
    /// </summary>
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
                ddlHeadName.Focus();
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

    #endregion

    #region Item Details
    /// <summary>
    /// Item Details Add in Grid - btnAddItemDetail_Click()
    /// </summary>
    /// <Functions>
    /// BindGvItemDetail();
    /// CalculateTotalAmount();
    /// ClearItemDetailTable();
    /// Gv RowCommand();
    /// </Functions>   
    protected void btnAddItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
            {
                ddlItemName.Focus();
                ShowMessage("Select Item Name.", false);
                return;
            }

            if (ddlItemName == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
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
            DataRow dr = VsdtGvItemDetail.Rows[CommonCls.ConvertIntZero(hfRowIndex.Value)];

            if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()))
            {
                txtQty.Focus();
                ShowMessage("Item Quantity Cannot be greater than Original Item Quantity = " + dr["ItemQty"].ToString(), false);
                return;
            }
            if (CommonCls.ConvertIntZero(ddlUnitName.SelectedValue) == 0)
            {
                ddlUnitName.Focus();
                ShowMessage("Select Item Unit ", false);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtRate.Text) == 0)
            {
                txtRate.Focus();
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

            if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > CommonCls.ConvertDecimalZero(txtItemAmt.Text))
            {
                ShowMessage("Discount Not Greater Than To Net Amount.", false);
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
                        ShowMessage("Enter Minor Unit Qty.", false);
                        txtMinorUnitQty.Focus();
                        return;
                    }
                }
            }
            if (VsdtGvFinalItemDetail != null)
            {
                DataRow[] dRow = VsdtGvFinalItemDetail.Select("ItemID=" + Convert.ToInt32(ddlItemName.SelectedValue) + "And ItemRate=" + txtRate.Text);
                if (dRow.Count() > 0)
                {
                    //ShowMessage("Item Already Exist.", false);
                    ShowMessage("This Item With Same Rate Already Exist.", false);
                    return;
                }
            }

            BindGvItemDetail();
            CalculateTotalAmount();
            ClearItemDetailTable();
            SelectionPA();
            btnAddItemDetail.Enabled = false;
            ddlItemName.Focus();

            foreach (GridViewRow grdRow in gvItemDetail.Rows)
            {
                Button btnEdit = (Button)grdRow.FindControl("btnEdit");
                btnEdit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void CalculateTotalAmount()
    {
        var cal = Calculation.CalculateTotalAmount(VsdtGvFinalItemDetail, VsdtSundri);
        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtGross.Text = cal.TotalGross.ToString();
        txtTaxable.Text = cal.TotalTaxable.ToString();
        txtNet.Text = cal.TotalAllNet.ToString();
    }

    void ClearItemDetailTable()
    {
        VsdtItems = null;
        ddlItemName.ClearSelection();
        txtMinorUnitQty.Text = "";
        ddlMinorUnit.ClearSelection();
        txtQty.Text = "0";
        txtFree.Text = "0";
        ddlUnitName.ClearSelection();
        txtItemAmt.Text = "0";
        txtRate.Text = "";
        txtItemTaxableAmt.Text = "";
        txtDiscount.Text = "0";
        ddlPA.SelectedValue = "0";
        txtTax.Text = "";
        ddlIsd.ClearSelection();
        txtCGSTAmt.Text = "";
        txtSGSTAmt.Text = "";
        txtIGSTAmt.Text = "";
        txtCESSAmt.Text = "";
        txtItemTaxableAmt.Text = "";
        txtItemRemark.Text = "";
        ddlItemName.Focus();
    }

    void BindGvItemDetail()
    {
        DataTable dtGvFinalItemDetail = new DataTable();
        if (VsdtGvFinalItemDetail == null)
        {
            dtGvFinalItemDetail = DtItemsSchema();
        }
        else
        {
            dtGvFinalItemDetail = VsdtGvFinalItemDetail;
        }

        if (VsdtItems != null)
        {
            decimal TaxRate = 0;
            if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);
            else
                TaxRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["TaxRate"]);
            if (ddlPA.SelectedValue == "1")
                TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);

            DataTable dtItems = VsdtItems;

            DataRow DrGvFinalItemDetail = dtGvFinalItemDetail.NewRow();
            TaxCal();
            DrGvFinalItemDetail["ItemName"] = ddlItemName.SelectedItem.Text;
            DrGvFinalItemDetail["HSNSACCode"] = dtItems.Rows[0]["HSNSACCode"];
            DrGvFinalItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            DrGvFinalItemDetail["FreeQty"] = CommonCls.ConvertDecimalZero(txtFree.Text);
            DrGvFinalItemDetail["ItemUnit"] = ddlUnitName.SelectedItem.Text;
            DrGvFinalItemDetail["ItemRate"] = Convert.ToDecimal(txtRate.Text);
            DrGvFinalItemDetail["ItemAmount"] = Convert.ToDecimal(txtItemAmt.Text);

            DrGvFinalItemDetail["ItemMinorUnit"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue) == 0 ? "" : ddlMinorUnit.SelectedItem.Text; //DrGvItemDetail["ItemSecondaryUnit"]
            DrGvFinalItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text);
            DrGvFinalItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);


            DrGvFinalItemDetail["DiscountValue"] = CommonCls.ConvertDecimalZero(txtDiscount.Text);
            DrGvFinalItemDetail["DiscountType"] = ddlDiscount.SelectedValue;
            DrGvFinalItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(hfDiscountAmount.Value);

            DrGvFinalItemDetail["NetAmt"] = Convert.ToDecimal(txtItemTaxableAmt.Text);
            DrGvFinalItemDetail["PADesc"] = ddlPA.SelectedItem.Text;
            DrGvFinalItemDetail["TaxRate"] = TaxRate;

            DrGvFinalItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGvFinalItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGvFinalItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGvFinalItemDetail["CESSTaxAmt"] = CessAmt;

            DrGvFinalItemDetail["ISDDesc"] = ddlIsd.SelectedItem.Text;

            DrGvFinalItemDetail["ItemID"] = Convert.ToInt32(ddlItemName.SelectedValue);
            DrGvFinalItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGvFinalItemDetail["ItemUnitID"] = Convert.ToInt32(ddlUnitName.SelectedValue);
            DrGvFinalItemDetail["IGSTTax"] = IgstRat;
            DrGvFinalItemDetail["SGSTTax"] = SgstRat;
            DrGvFinalItemDetail["CGSTTax"] = CgstRat;
            DrGvFinalItemDetail["CESSTax"] = CessRat;
            DrGvFinalItemDetail["ItemRemark"] = txtItemRemark.Text;
            DrGvFinalItemDetail["ISDApplicable"] = Convert.ToInt16(ddlIsd.SelectedValue);
            DrGvFinalItemDetail["PA"] = Convert.ToInt16(ddlPA.SelectedValue);
            DrGvFinalItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemMinorUnitID"].ToString());
            DrGvFinalItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["ItemMinorUnitQty"].ToString());
            DrGvFinalItemDetail["ExtraInd"] = 0;
            dtGvFinalItemDetail.Rows.Add(DrGvFinalItemDetail);
            gvFinalItemDetail.DataSource = VsdtGvFinalItemDetail = dtGvFinalItemDetail;
            gvFinalItemDetail.DataBind();
        }
    }

    protected void gvFinalItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtGvItemDetail = VsdtGvFinalItemDetail;
            dtGvItemDetail.Rows[rowIndex].Delete();
            VsdtGvFinalItemDetail = dtGvItemDetail;
            gvFinalItemDetail.DataSource = dtGvItemDetail;
            gvFinalItemDetail.DataBind();
            CalculateTotalAmount();
        }
    }

    protected void gvFinalItemDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblDiscountType = (Label)e.Row.FindControl("lblDiscountType");
                ddlDiscount.SelectedValue = lblDiscountType.Text;
                lblDiscountType.Text = ddlDiscount.SelectedItem.Text;

                Label lblPA = (Label)e.Row.FindControl("lblPA");
                ddlPA.SelectedValue = lblPA.Text;
                lblPA.Text = ddlPA.SelectedItem.Text;
            }
        }
    }

    void CalculateRate()
    {
        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        item.ItemFree = CommonCls.ConvertDecimalZero(txtFree.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtRate.Text);
        item.ItemDiscount = CommonCls.ConvertDecimalZero(txtDiscount.Text);
        item.DiscountInPerc = Convert.ToInt16(ddlDiscount.SelectedValue) == 1;

        StructItems GetItem = Calculation.CalculateRate(item);
        txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
        txtItemAmt.Text = GetItem.ItemAmount.ToString();
        hfDiscountAmount.Value = GetItem.DiscountValue.ToString();
    }

    protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "EditItem")
        {
            DataTable dtEditItemsDetails = VsdtGvItemDetail;
            DataRow drEditItemsDetails = dtEditItemsDetails.Rows[rowIndex];

            ddlItemName.SelectedValue = drEditItemsDetails["ItemID"].ToString();

            ddlItemName_SelectedIndexChanged(sender, e);

            hfHSNSACCode.Value = drEditItemsDetails["HSNSACCode"].ToString();
            hfGoodsAndServiceInd.Value = drEditItemsDetails["GoodsServiceInd"].ToString();
            hfActualQty.Value = txtQty.Text = drEditItemsDetails["ItemQty"].ToString();
            ddlUnitName.SelectedValue = drEditItemsDetails["ItemUnitID"].ToString();
            txtMinorUnitQty.Text = drEditItemsDetails["ItemMinorQty"].ToString();
            ddlMinorUnit.SelectedValue = drEditItemsDetails["ItemMinorUnitID"].ToString();
            txtFree.Text = drEditItemsDetails["FreeQty"].ToString();
            txtRate.Text = drEditItemsDetails["ItemRate"].ToString();
            txtItemAmt.Text = drEditItemsDetails["ItemAmount"].ToString();
            txtDiscount.Text = drEditItemsDetails["DiscountValue"].ToString();
            ddlDiscount.SelectedValue = drEditItemsDetails["DiscountType"].ToString();
            txtItemTaxableAmt.Text = drEditItemsDetails["NetAmt"].ToString();
            ddlPA.SelectedValue = drEditItemsDetails["PA"].ToString();

            txtTax.Text = drEditItemsDetails["TaxRate"].ToString();
            txtCGSTAmt.Text = drEditItemsDetails["CGSTTaxAmt"].ToString();
            txtSGSTAmt.Text = drEditItemsDetails["SGSTTaxAmt"].ToString();
            txtIGSTAmt.Text = drEditItemsDetails["IGSTTaxAmt"].ToString();
            txtCESSAmt.Text = drEditItemsDetails["CESSTaxAmt"].ToString();

            txtMinorUnitQty.Enabled = false;

            //Button btnEdit = (Button)gvItemDetail.Rows[rowIndex].FindControl("btnEdit");
            //btnEdit.Enabled = false;
            btnAddItemDetail.Enabled = true;

            hfRowIndex.Value = Convert.ToString(rowIndex);

            foreach (GridViewRow grdRow in gvItemDetail.Rows)
            {
                Button btnEdit = (Button)grdRow.FindControl("btnEdit");
                btnEdit.Enabled = false;
            }
        }
    }

    protected void gvItemDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblDiscountType = (Label)e.Row.FindControl("lblDiscountType");
                ddlDiscount.SelectedValue = lblDiscountType.Text;
                lblDiscountType.Text = ddlDiscount.SelectedItem.Text;

                Label lblPA = (Label)e.Row.FindControl("lblPA");
                ddlPA.SelectedValue = lblPA.Text;
                lblPA.Text = ddlPA.SelectedItem.Text;
            }
        }
    }

    /// <summary>
    /// All AMOUNT Calculation On Text Change.
    /// 0 ItemName 1 Qty, 2 Rate, 3 DISCOUNT, Tax.
    /// </summary>
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)// Fill Record Item List
    {
        try
        {
            txtQty.Text = "";
            txtItemTaxableAmt.Text = txtDiscount.Text = txtItemAmt.Text = "0";
            VsDtItemSellRate = VsdtItems = null;

            ObjSaleRModel = new SalesReturnModel();
            ObjSaleRModel.Ind = 11;
            ObjSaleRModel.OrgID = GlobalSession.OrgID;
            ObjSaleRModel.BrID = GlobalSession.BrID;
            ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjSaleRModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());

            ObjSaleRModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjSaleRModel.ItemID = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);

            if (ddlGstinNo.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
                    ObjSaleRModel.GSTIN = ddlGstinNo.SelectedValue;
            }
            if (ddlIncomeHead.SelectedValue == CashSalesAcc)
                ObjSaleRModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

            string uri = string.Format("SalesReturn/FillItemSellRate");
            DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjSaleRModel);
            if (dsItems.Tables[0].Rows.Count > 0)
            {
                VsdtItems = dsItems.Tables[0];

                ddlUnitName.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
                txtRate.Text = VsdtItems.Rows[0]["ItemSellingRate"].ToString();

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
                    txtMinorUnitQty.Text = "";
                    if (Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                        txtMinorUnitQty.Enabled = true;
                    else
                        txtMinorUnitQty.Enabled = false;

                    ddlMinorUnit.SelectedValue = CommonCls.ConvertIntZero(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();
                }
                else
                {
                    ddlMinorUnit.SelectedValue = "0";
                }

                ddlSalesto.Enabled = ddlGstinNo.Enabled = ddlIncomeHead.Enabled = false;
                txtSalesto.Enabled = txtGstinNo.Enabled = false;
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

            //VsDtItemSellRate.Rows.RemoveAt(rowIndex - 1);
            VsDtItemSellRate.Rows[rowIndex - 1].Delete();
            VsDtItemSellRate.Rows.InsertAt(DrWithInRange, 0);
        }
    }

    void FillTaxRate(DataRow drRate)
    {
        txtTax.Text = drRate["TaxRate"].ToString();
        txtCGSTAmt.Text = drRate["CGSTRate"].ToString();
        txtSGSTAmt.Text = drRate["SGSTRate"].ToString();
        txtIGSTAmt.Text = drRate["IGSTRate"].ToString();
        txtCESSAmt.Text = drRate["Cess"].ToString();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataRow dr = VsdtGvItemDetail.Rows[CommonCls.ConvertIntZero(hfRowIndex.Value)];

            if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()))
            {
                txtQty.Focus();
                ShowMessage("Item Quantity Cannot be greater than Original Item Quantity = " + dr["ItemQty"].ToString(), false);
                return;
            }

            CalculateRate();
            TaxCal();
            txtQty.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage("Select Item First!", false);
        }
    }

    protected void txtFree_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (VsdtItems == null)
            {
                ShowMessage("Select Item First.", false);
                ddlItemName.Focus();
                ddlPA.SelectedValue = "0";
                return;
            }
            CalculateRate();
            TaxCal();
            txtRate.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        if (VsdtItems == null)
        {
            ShowMessage("Select Item First.", false);
            ddlItemName.Focus();
            ddlPA.SelectedValue = "0";
            return;
        }
        TaxWithInRange();
        CalculateRate();
        TaxCal();
        txtDiscount.Focus();
    }

    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        if (VsdtItems == null)
        {
            ShowMessage("Select Item First.", false);
            ddlItemName.Focus();
            ddlPA.SelectedValue = "0";
            return;
        }
        CalculateRate();
        TaxCal();

        ddlDiscount.Focus();
    }

    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateRate();
        if (!string.IsNullOrEmpty(txtQty.Text))
            TaxCal();

        ddlDiscount.Focus();
    }

    protected void txtTax_TextChanged(object sender, EventArgs e)
    {
        if (VsdtItems == null)
        {
            ShowMessage("Select Item First.", false);
            ddlItemName.Focus();
            ddlPA.SelectedValue = "0";
            return;
        }
        TaxCal();
        ddlPA.SelectedValue = "1";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { PaChange(); });", true);
        ddlIsd.Focus();
    }

    protected void ddlPA_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (VsdtItems == null)
            {
                ShowMessage("Select Item First.", false);
                ddlItemName.Focus();
                ddlPA.SelectedValue = "0";
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                ShowMessage("Enter Quantity First.", false);
                txtQty.Focus();
                ddlPA.SelectedValue = "0";
                return;
            }
            SelectionPA();
            TaxCal();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void SelectionPA()
    {
        if (ddlPA.SelectedValue == "1")
        {
            //thAmt1.Visible = thAmt2.Visible = thAmt3.Visible = thAmt4.Visible = true;
            thRate1.Visible = thRate2.Visible = thRate3.Visible = thRate4.Visible = thRate5.Visible = thRate6.Visible = true;
            dtCESSAmt.Visible = dtCGSTAmt.Visible = dtddlIsd.Visible = dtIGSTAmt.Visible = dtSGSTAmt.Visible = dtTax.Visible = true;

            //tdMinorOnPa.Visible = true; tdMorPa.Visible = true;
            txtTax.Focus();
        }
        else
        {
            //thAmt1.Visible = thAmt2.Visible = thAmt3.Visible = thAmt4.Visible = false;
            thRate1.Visible = thRate2.Visible = thRate3.Visible = thRate4.Visible = thRate5.Visible = thRate6.Visible = false;
            dtCESSAmt.Visible = dtCGSTAmt.Visible = dtddlIsd.Visible = dtIGSTAmt.Visible = dtSGSTAmt.Visible = dtTax.Visible = false;

            ///tdMinorOnPa.Visible = false; tdMorPa.Visible = false;
            txtDiscount.Focus();
        }

        //object sender = UpdatePanel1;
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { PaChange(); });", true);
    }

    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {
        decimal MaxAmt = Convert.ToDecimal(txtItemTaxableAmt.Text);
        decimal TaxBy;

        DataTable dtItems = new DataTable();

        if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
            dtItems = VsDtItemSellRate;
        else
            dtItems = VsdtItems;

        if (ddlPA.SelectedValue == "1")
            TaxBy = CommonCls.ConvertDecimalZero(txtTax.Text);
        else
            TaxBy = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);
        item.ItemRate = TaxBy;

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"]);

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

    #endregion

    #region On Save
    /// <summary>
    /// On Finaly Save
    /// ValidationBTNSAVE Function For CHeck All Validation ON Save.
    /// After Saving ClearAfterSave() For Clear All Fields.
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
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

            ObjSaleRModel = new SalesReturnModel();
            ObjSaleRModel.Ind = 2;
            ObjSaleRModel.OrgID = GlobalSession.OrgID;
            ObjSaleRModel.BrID = GlobalSession.BrID;
            ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            ObjSaleRModel.EntryType = 1;
            ObjSaleRModel.YrCD = GlobalSession.YrCD;
            ObjSaleRModel.User = GlobalSession.UserID;
            ObjSaleRModel.IP = GlobalSession.IP;

            ObjSaleRModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjSaleRModel.PartyName = txtSalesto.Text.ToUpper();
            ObjSaleRModel.PartyGSTIN = txtGstinNo.Text.ToUpper();

            ObjSaleRModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            ObjSaleRModel.PartyAddress = ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
                                        ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            ObjSaleRModel.WareHouseID = Convert.ToInt32(ddlLocation.SelectedValue);
            ObjSaleRModel.TransName = txtTransportName.Text;
            ObjSaleRModel.PONo = txtorderNo.Text;
            ObjSaleRModel.DocDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            ObjSaleRModel.OrgVoucherNo = Convert.ToInt32(hfVoucherNo.Value);
            ObjSaleRModel.OrgVoucherDate = CommonCls.ConvertToDate(hfVoucherDate.Value);
            ObjSaleRModel.InvoiceSeries = txtInvoiceSeries.Text;
            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    ObjSaleRModel.InvoiceSeries = txtInvoiceSeries.Text.ToUpper();
                    break;

                case 2: /// Available Series
                    ObjSaleRModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    ObjSaleRModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;
            }

            ObjSaleRModel.DtSales = DtSalesSchema();
            ObjSaleRModel.DtItems = DtItemsSchema();
            ObjSaleRModel.DtSundries = DtSundriesSchema();

            ObjSaleRModel.DtSales = CreateSaleData();

            //if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            //{
            //    foreach (DataRow item in VsdtGvFreeItem.Rows)
            //    {
            //        VsdtGvItemDetail.Rows.Add(item.ItemArray);
            //    }
            //}

            ObjSaleRModel.DtItems = VsdtGvFinalItemDetail;
            ObjSaleRModel.DtSundries = VsdtSundri;

            if ((ObjSaleRModel.DtSundries == null) || (ObjSaleRModel.DtSundries.Rows.Count <= 0))
            {
                ObjSaleRModel.DtSundries = DtSundriesSchema();
                DataRow drSaleSundri = ObjSaleRModel.DtSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                ObjSaleRModel.DtSundries.Rows.Add(drSaleSundri);
            }

            string uri = string.Format("SalesReturn/SaveSalesReturnVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjSaleRModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {

                    ClearAll();

                    string LastVNO, LastVDate, InvoiceSeries = ""; //InvoiceNo, InvoiceDate, InvoiceName, 
                    //InvoiceNo = dtSave.Rows[0]["LastInvoiceNo"].ToString();
                    //InvoiceDate = Convert.ToDateTime(dtSave.Rows[0]["LastInvoiceDate"].ToString()).ToString("dd/MM/yyyy");
                    //InvoiceName = dtSave.Rows[0]["InvoiceName"].ToString();
                    LastVNO = dtSave.Rows[0]["DocMaxNo"].ToString();
                    LastVDate = Convert.ToDateTime(dtSave.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                    ShowMessage("Record Save successfully", true);// for Invoice No. " + InvoiceNo, true);
                    if (!string.IsNullOrEmpty(ObjSaleRModel.InvoiceSeries))
                        InvoiceSeries = ObjSaleRModel.InvoiceSeries + "-";

                    //lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date " + LastVNO + " - " + LastVDate;


                    //if (VsdtSeries.Rows.Count > 0)
                    //{
                    //    if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 2) //Serial No Auto Generate No.2
                    //    {
                    //        string LastInvoiceNo = "";
                    //        foreach (DataRow item in VsdtSeries.Rows)
                    //        {
                    //            if (CommonCls.ConvertIntZero(txtSearchInvoice.Text) == CommonCls.ConvertIntZero(item["InvoiceNo"]))
                    //            {
                    //                item["InvoiceNo"] = CommonCls.ConvertIntZero(InvoiceNo) + 1;
                    //                LastInvoiceNo = (CommonCls.ConvertIntZero(InvoiceNo) + 1).ToString();
                    //            }
                    //        }

                    //        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                    //        {
                    //            case 1: /// Manual Series
                    //                txtSearchInvoice.Text = LastInvoiceNo;
                    //                break;

                    //            case 2: /// Available Series
                    //                ddlInvoiceSeries.DataSource = VsdtSeries;
                    //                ddlInvoiceSeries.DataTextField = "Series";
                    //                ddlInvoiceSeries.DataBind();
                    //                if (VsdtSeries.Rows.Count > 1)
                    //                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                    //                if (ddlInvoiceSeries.SelectedValue == "0")
                    //                {
                    //                    txtSearchInvoice.Text = "";
                    //                }
                    //                else
                    //                {
                    //                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                    //                    txtSearchInvoice.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                    //                }
                    //                break;

                    //            case 3: /// Default Series
                    //                ddlInvoiceSeries.DataSource = VsdtSeries;
                    //                ddlInvoiceSeries.DataTextField = "Series";
                    //                ddlInvoiceSeries.DataBind();
                    //                if (VsdtSeries.Rows.Count > 1)
                    //                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                    //                if (ddlInvoiceSeries.SelectedValue == "0")
                    //                {
                    //                    txtSearchInvoice.Text = "";
                    //                }
                    //                else
                    //                {
                    //                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                    //                    txtSearchInvoice.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                    //                }
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        txtSearchInvoice.Text = "";
                    //    }
                    //}


                    //if (hfSaleInvoiceManually.Value == "1")
                    //{
                    //    txtSearchInvoice.Text = (Convert.ToInt64(dtSave.Rows[0]["LastInvoiceNo"].ToString()) + 1).ToString();
                    //}

                    //CallReport(InvoiceNo, CommonCls.ConvertToDate(InvoiceDate), InvoiceName, InvoiceSeries, LastVNO);
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Duplicate Invoice No.", false);
                    txtSearchInvoice.Focus();
                }
            }
            else
            {
                ShowMessage("Record Not Save Please Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    bool ValidationBTNSAVE()
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
                if (string.IsNullOrEmpty(txtSearchInvoice.Text) || (Convert.ToInt32(txtSearchInvoice.Text) == 0))
                {
                    txtSearchInvoice.Focus();
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
                if (string.IsNullOrEmpty(txtSearchInvoice.Text) || (Convert.ToInt32(txtSearchInvoice.Text) == 0))
                {
                    txtSearchInvoice.Focus();
                    ShowMessage("Enter Invoice No.", false);
                    return false;
                }
                break;
        }

        if (string.IsNullOrEmpty(txtSearchInvoice.Text)) // Invoice Number Shouldn't be Null
        {
            txtSearchInvoice.Focus();

            ShowMessage("Enter Invoice No.", false);

            return false;
        }

        if (string.IsNullOrEmpty(txtVoucherDate.Text))
        {
            txtVoucherDate.Focus();
            ShowMessage("Enter Invoice Date.", false);
            return false;
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtVoucherDate.Focus();
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
                ddlLocation.Focus();
                ShowMessage("Select Dispatch Location.", false);
                return false;
            }
        }

        if (ddlIncomeHead != null && ddlIncomeHead.Items.Count > 1)
        {
            if (ddlIncomeHead.SelectedIndex <= 0)
            {
                ddlIncomeHead.Focus();
                ShowMessage("Select Income Head.", false);
                return false;
            }
        }

        if ((ddlIncomeHead.SelectedValue != CashSalesAcc))
        {
            if (ddlSalesto == null || CommonCls.ConvertIntZero(ddlSalesto.SelectedValue) == 0)
            {
                ddlSalesto.Focus();
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
                return false;
            }
        }
        //if (ddlGstinNo == null || (ddlGstinNo.Items.Count > 1) ? ddlGstinNo.SelectedIndex <= 0 ? true : false : false )
        //{

        //}

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

                ObjSaleRModel = new SalesReturnModel();
                ObjSaleRModel.Ind = 6;
                ObjSaleRModel.OrgID = GlobalSession.OrgID;
                ObjSaleRModel.BrID = GlobalSession.BrID;
                ObjSaleRModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedItem.Value);



                string uri = string.Format("PurchaseVoucher/CheckGSTIN_Number");

                DataSet dtStatePanNo = CommonCls.ApiPostDataSet(uri, ObjSaleRModel);
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

        drCreateSaleData["AccCode"] = ddlIncomeHead.SelectedValue == CashSalesAcc ? 919000 : Convert.ToInt32(ddlSalesto.SelectedValue);
        drCreateSaleData["AccGst"] = ddlGstinNo.SelectedItem != null ? !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ? ddlGstinNo.SelectedValue : "" : "";
        drCreateSaleData["SalePurchaseCode"] = Convert.ToInt32(ddlIncomeHead.SelectedValue);
        if (ddlShippingAdd.SelectedItem != null && CommonCls.ConvertIntZero(ddlShippingAdd.SelectedValue) != 0)
        {
            drCreateSaleData["AccPOSID"] = Convert.ToInt32(ddlShippingAdd.SelectedValue);
        }
        drCreateSaleData["WarehouseID"] = ddlLocation.SelectedItem != null ? Convert.ToInt32(ddlLocation.SelectedValue) : 0;
        drCreateSaleData["OrderNo"] = 0;//!string.IsNullOrEmpty(txtorderNo.Text) ? Convert.ToInt32(txtorderNo.Text) : 0;
        drCreateSaleData["OrderDate"] = !string.IsNullOrEmpty(txtOrderDate.Text) ? CommonCls.ConvertToDate(txtOrderDate.Text) : "";

        drCreateSaleData["InvoiceNo"] = Convert.ToInt32(txtSearchInvoice.Text);
        drCreateSaleData["InvoiceDate"] = CommonCls.ConvertToDate(hfInvoiceDate.Value);
        drCreateSaleData["TDSApplicable"] = Convert.ToInt32(ddlTds.SelectedValue);
        drCreateSaleData["TCSApplicable"] = Convert.ToInt32(ddlTCS.SelectedValue);
        drCreateSaleData["RCMApplicable"] = Convert.ToInt32(ddlRCM.SelectedValue);
        drCreateSaleData["GrossAmt"] = Convert.ToDecimal(txtGross.Text);
        drCreateSaleData["TaxAmt"] = Convert.ToDecimal(txtTaxable.Text);
        drCreateSaleData["NetAmt"] = Convert.ToDecimal(txtNet.Text);
        drCreateSaleData["RoundOffAmt"] = 0;

        drCreateSaleData["TransportID"] = CbTransDetail.Checked ? Convert.ToInt64(ddlTansportID.SelectedValue) : 0;
        drCreateSaleData["VehicleNo"] = CbTransDetail.Checked ? txtVehicleNo.Text : "";
        drCreateSaleData["WayBillNo"] = CbTransDetail.Checked ? Convert.ToInt64(ddlTansportID.SelectedValue) : 0;
        drCreateSaleData["TransportDate"] = CbTransDetail.Checked ? CommonCls.ConvertToDate(txtTransportDate.Text.Substring(0, 10)) : "";// + " " + txtTransportDate.Text.Substring(11, 8) : "";

        drCreateSaleData["DocDesc"] = txtNarration.Text;
        //drCreateSaleData["UserID"] = GlobalSession.UserID;
        //drCreateSaleData["IP"] = CommonCls.GetIP();

        dtCreateSaleData.Rows.Add(drCreateSaleData);

        return dtCreateSaleData;

    }
    #endregion

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    void ClearAll()
    {
        //divFree.Visible = false;
        //gvFreeItem.DataSource = VsdtGvFreeItem = null;
        //gvFreeItem.DataBind();

        ddlIncomeHead.ClearSelection();
        ddlSalesto.ClearSelection();

        ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        ddlLocation.ClearSelection();
        ddlGstinNo.DataSource = ddlShippingAdd.DataSource = new DataTable();
        ddlGstinNo.DataBind(); ddlShippingAdd.DataBind();

        //if (hfSaleInvoiceManually.Value != "1")
        //{
        //    txtSearchInvoice.Text = "";
        //}

        txtVoucherDate.Text = txtorderNo.Text = txtOrderDate.Text = "";
        ddlTds.ClearSelection();
        ddlTCS.ClearSelection();
        ddlRCM.ClearSelection();

        ddlItemName.ClearSelection();
        ddlUnitName.ClearSelection();
        ddlPA.ClearSelection();
        ddlIsd.ClearSelection();

        txtFree.Text = "0";
        txtQty.Text = txtRate.Text = txtItemAmt.Text = txtDiscount.Text = txtItemTaxableAmt.Text = "0";
        txtTax.Text = txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text = "0";
        txtItemRemark.Text = "";

        VsdtGvFinalItemDetail = VsdtGvItemDetail = VsdtSundri = null;
        gvItemDetail.DataSource = gvFinalItemDetail.DataSource = gvotherCharge.DataSource = new DataTable();
        gvItemDetail.DataBind(); gvFinalItemDetail.DataBind(); gvotherCharge.DataBind();

        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";

        txtGross.Text = txtTaxable.Text = txtNet.Text = txtAddLess.Text = "0";
        txtNarration.ClearSelection();
        lblMsg.Text = "";

        CbTransDetail.Checked = false;
        CBTransDetailInit();


        ddlItemName.Enabled = false;

        txtSalesto.Text = txtShippingAdd.Text = txtGstinNo.Text = "";
        txtInvoiceSeries.Text = "";

        //ddlSalesto.Enabled = ddlGstinNo.Enabled = ddlIncomeHead.Enabled = true;
        //txtSalesto.Enabled = txtGstinNo.Enabled = true;

        txtInvoiceSeries.Enabled = txtSearchInvoice.Enabled = btnSearchInvoice.Enabled = true;
        txtInvoiceSeries.Text = txtSearchInvoice.Text = "";

        btnAddItemDetail.Enabled = btnAdd.Enabled = btnSave.Enabled = false;

        pnlOtherCharge.Visible = false;
        pnlTransport.Visible = CbTransDetail.Checked = false;

        txtSearchInvoice.Focus();
        ddlCostCenter.ClearSelection();
        ddlCostCenter.Enabled = true;
    }

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
        dtItems.Columns.Add("ExtraInd", typeof(int));

        return dtItems;
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
            txtTransportDate.Enabled = ddlTansportID.Enabled = txtVehicleNo.Enabled = txtTransportName.Enabled = false;
            txtTransportDate.Text = txtVehicleNo.Text = txtTransportName.Text = "";
            ddlTansportID.ClearSelection();
        }
    }

    #endregion

    #region Free Item Qty Add

    //protected void btnShowFreeItem_Click(object sender, EventArgs e)
    //{
    //    if (divFree.Visible)
    //        divFree.Visible = false;
    //    else
    //        divFree.Visible = true;

    //    btnShowFreeItem.Focus();
    //}
    //protected void ddlFreeItemName_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    ClearItemDetailTable();

    //    ObjSaleRModel = new SalesReturnModel();
    //    ObjSaleRModel.Ind = 11;
    //    ObjSaleRModel.OrgID = GlobalSession.OrgID;
    //    ObjSaleRModel.BrID = GlobalSession.BrID;
    //    ObjSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
    //    ObjSaleRModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());
    //    // ObjSaleModel.GSTIN= ddlGstinNo.SelectedItem.Text;
    //    ObjSaleRModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
    //    ObjSaleRModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);

    //    if (ddlGstinNo.SelectedItem != null)
    //    {
    //        if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
    //            ObjSaleRModel.GSTIN = ddlGstinNo.SelectedValue;
    //    }
    //    if (ddlIncomeHead.SelectedValue == CashSalesAcc)
    //        ObjSaleRModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

    //    string uri = string.Format("SalesReturn/FillItemSellRate");
    //    DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjSaleRModel);
    //    if (dsItems != null && dsItems.Tables.Count > 0)
    //    {
    //        if (dsItems.Tables[0].Rows.Count > 0)
    //        {
    //            VsdtItems = dsItems.Tables[0];
    //            ddlFreeUnit.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
    //        }
    //    }
    //    txtFreeQty.Focus();
    //}
    //protected void btnFreeAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
    //        {
    //            ClearFreeItem();
    //            ShowMessage("Insert Item Detail First.", false);
    //            ddlItemName.Focus();
    //            return;
    //        }

    //        if (CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue) == 0)
    //        {
    //            ShowMessage("Select Item Name First.", false);
    //            ddlFreeItemName.Focus();
    //            return;
    //        }
    //        if (CommonCls.ConvertIntZero(ddlFreeUnit.SelectedValue) == 0)
    //        {
    //            ShowMessage("Select Item Name First.", false);
    //            ddlFreeItemName.Focus();
    //            return;
    //        }
    //        if (CommonCls.ConvertDecimalZero(txtFreeQty.Text) == 0)
    //        {
    //            ShowMessage("Enter Free Qty.", false);
    //            txtFreeQty.Focus();
    //            return;
    //        }

    //        if (VsdtGvFreeItem == null)
    //        {
    //            VsdtGvFreeItem = VsdtGvItemDetail.Clone();
    //        }

    //        if (VsdtGvFreeItem.Rows.Count > 0)
    //        {
    //            DataRow[] DuplicateRow = VsdtGvFreeItem.Select("ItemID='" + ddlFreeItemName.SelectedValue + "'");
    //            if (DuplicateRow.Count() == 1)
    //            {
    //                ShowMessage("This Item Already Exist.", false);
    //                ddlFreeItemName.Focus();
    //                return;
    //            }
    //        }

    //        DataRow drItem = VsdtGvFreeItem.NewRow();
    //        //drItem["PurchaseCode"] = 0;

    //        drItem["ItemID"] = ddlFreeItemName.SelectedValue;
    //        drItem["ItemName"] = ddlFreeItemName.SelectedItem.Text;
    //        drItem["ItemUnitID"] = ddlFreeUnit.SelectedValue;
    //        drItem["ItemUnit"] = ddlFreeUnit.SelectedItem.Text;
    //        drItem["ItemQty"] = CommonCls.ConvertDecimalZero(txtFreeQty.Text);
    //        drItem["ExtraInd"] = 1; //This For FreeItemInd
    //        drItem["GoodsServiceInd"] = VsdtItems.Rows[0]["GoodsServiceIndication"];
    //        drItem["HSNSACCode"] = VsdtItems.Rows[0]["HSNSACCode"];// ViewState["FreeHSNSACCode"].ToString();
    //        VsdtGvFreeItem.Rows.Add(drItem);
    //        gvFreeItem.DataSource = VsdtGvFreeItem;
    //        gvFreeItem.DataBind();
    //        ClearFreeItem();
    //        ddlFreeItemName.Focus();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}
    //protected void gvFreeItem_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int rowIndex = Convert.ToInt32(e.CommandArgument);
    //    if (e.CommandName == "RemoveRow")
    //    {
    //        VsdtGvFreeItem.Rows[rowIndex].Delete();
    //        gvFreeItem.DataSource = VsdtGvFreeItem;
    //        gvFreeItem.DataBind();
    //        ddlFreeItemName.Focus();
    //    }
    //}

    //void ClearFreeItem()
    //{
    //    ddlFreeItemName.ClearSelection();
    //    ddlFreeUnit.ClearSelection();
    //    txtFreeQty.Text = "";
    //    VsdtItems = null;
    //}

    #endregion

    public void CallReport(string InvoiceNo, string InvoiceDate, string InvoiceName, string InvoiceSeries, string LastVNO)
    {

        Hashtable HT = new Hashtable();
        HT.Add("Ind", 1);

        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "SALES INVOICE ");

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

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
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
            pnlTransport.Visible = CbTransDetail.Checked = false;
        else
            pnlTransport.Visible = CbTransDetail.Checked = true;

        //if (CbTransDetail.Checked)
        //    CbTransDetail.Checked = false;
        //else
        //    CbTransDetail.Checked = true;

        CBTransDetailInit();

        btnShowTransport.Focus();
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }
}