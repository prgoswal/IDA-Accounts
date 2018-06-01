using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPurchase : System.Web.UI.Page
{
    #region Declarations

    CompositionPurchaseModel objPurchaseModel;
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

    DataTable VsdtLastVoucherDate
    {
        get { return (DataTable)ViewState["dtLastVoucherDate"]; }
        set { ViewState["dtLastVoucherDate"] = value; }
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
            ddlItemName.Enabled = false;
            ddlPayMode.Focus();
            PayModeInit();

            //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
            //{
            //    btnSave.Visible = false;

            //}
            //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
            //{
            //    btnSave.Visible = true;
            //}

            //CallReport("6600", "2017/07/31");
        }

    }


    protected void ddlPurchaseFrom_SelectedIndexChanged(object sender, EventArgs e)    //ddlSales To Selected Index Chanage Gstin Number 
    {
        try
        {
            //if (ddlPurchaseFrom.SelectedValue == "" || ddlPurchaseFrom.SelectedValue == "0")
            //{
            //    ShowMessage("Select Purchase From.", false);
            //    return;
            //}

            tblShippingDetail.Visible = true;
            objPurchaseModel = new CompositionPurchaseModel()
            {
                Ind = 1,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
                AccCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue),
                AdvRecPayID = 14
            };

            // string uri = string.Format("PurchaseVoucher/FillGistnNo");
            string uri = string.Format("PurchaseVoucher/FillGistnNoWithDetail");
            DataSet dsGSTINWithDetail = CommonCls.ApiPostDataSet(uri, objPurchaseModel);
            if (dsGSTINWithDetail.Tables.Count > 0)
            {
                //DataTable dtGSTIN = dsGSTINWithDetail.Tables[0];
                VsdtAdvance = dsGSTINWithDetail.Tables[1];

                #region PartDetails
                if (VsdtAdvance.Rows.Count > 0)
                {
                    VsdtAdvance.Columns.Add("CheckedInd", typeof(bool));
                    //VsdtAdvance.Columns.Add("AdjAdvAmount", typeof(decimal));
                    GvPartyAdvance.DataSource = VsdtAdvance;
                    GvPartyAdvance.DataBind();

                    pnlParentAdvance.Visible = true;
                }
                else
                {
                    pnlParentAdvance.Visible = false;
                }

                #endregion
            }

            FillShippingAddress();
            ddlItemName.Enabled = true;
            DataTable dtPurchaseFrom = VsdtPurchaseFrom;
            int TDS = Convert.ToInt32(dtPurchaseFrom.Rows[ddlPurchaseFrom.SelectedIndex]["TDSApplicable"].ToString());
            int TCS = Convert.ToInt32(dtPurchaseFrom.Rows[ddlPurchaseFrom.SelectedIndex]["ISDApplicable"].ToString());
            int RCM = Convert.ToInt32(dtPurchaseFrom.Rows[ddlPurchaseFrom.SelectedIndex]["RCMApplicable"].ToString());

        }
        catch (Exception ex)
        {
            ShowMessage("Internal Server Error.", false);
        }
    }


    void FillShippingAddress()
    {
        objPurchaseModel = new CompositionPurchaseModel()
        {
            Ind = 4,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
            VchType = Convert.ToInt32(ViewState["VchType"]),
            AccCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue),
            GSTIN = "",
            //ddlGstinNo != null ?
            //          ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
            //        ddlGstinNo.SelectedValue : "" : "",

        };

        string uri = string.Format("PurchaseVoucher/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, objPurchaseModel);
        if (dtShipping.Rows.Count > 0)
        {
            ddlShippingAdd.DataSource = dtShipping;
            ddlShippingAdd.DataTextField = "POSAddress";
            ddlShippingAdd.DataValueField = "AccPOSID";
            ddlShippingAdd.DataBind();
            //if (dtShipping.Rows.Count > 1)
            //    ddlGstinNo.Items.Insert(0, "-Select-");
        }
        else
        {
            ddlShippingAdd.DataSource = dtShipping;
            ddlShippingAdd.DataBind();
            ddlShippingAdd.SelectedIndex = 0;
        }
    }

    void BindAll()
    {
        objPurchaseModel = new CompositionPurchaseModel();
        objPurchaseModel.Ind = 11;
        objPurchaseModel.OrgID = GlobalSession.OrgID;
        objPurchaseModel.BrID = GlobalSession.BrID;
        objPurchaseModel.YrCD = GlobalSession.YrCD;
        objPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("CompositionPurchaseVoucher/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, objPurchaseModel);
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
            if (dtNarration.Rows.Count > 0)
            {
                txtNarration.DataSource = dtNarration;
                txtNarration.DataTextField = "NarrationDesc";
                txtNarration.DataBind();
            }

            // For Last Invoice / Voucher No. Info Taken
            if (dtVoucherNoDate.Rows.Count > 0)
            {
                if (dtVoucherNoDate.Rows[0]["LastNo"].ToString() != "0")
                {
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucherNoDate.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtVoucherNoDate.Rows[0]["LastDate"].ToString());

                    hfLastVoucherDate.Value = CommonCls.ConvertDateDB(Convert.ToDateTime(dtVoucherNoDate.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy"));
                }
            }

            // For Income Account Head List 
            if (dtExpenseHead.Rows.Count > 0)
            {
                ddlExpenseHead.DataSource = dtExpenseHead;
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
            }

            // For Account type Bind.
            if (dtAccountType.Rows.Count > 0)
            {
                ddlPayMode.DataSource = dtAccountType;
                ddlPayMode.DataTextField = "AccName";
                ddlPayMode.DataValueField = "AccCode";
                ddlPayMode.DataBind();
                ddlPayMode.Items.Insert(0, new ListItem("CREDIT", "1"));

                //ddlAccountType.DataSource = dtAccountType;
                //ddlAccountType.DataTextField = "AccName";
                //ddlAccountType.DataValueField = "AccCode";
                //ddlAccountType.DataBind();
                //ddlAccountType.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
    }

    void ClearAll()
    {
        /// Clear Advance Items
        VsdtRemainAmt = VsdtAdvance = null;
        GvPartyAdvance.DataSource = null;
        GvPartyAdvance.DataBind();
        pnlPartyAdvance.Visible = false;
        /// ---------------------

        divFree.Visible = false;
        ddlPayMode.ClearSelection();
        gvFreeItem.DataSource = VsdtGvFreeItem = null;
        gvFreeItem.DataBind();


        ddlExpenseHead.ClearSelection();
        ddlPurchaseFrom.ClearSelection();

        //ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        ddlLocation.ClearSelection();
        ddlShippingAdd.DataSource = new DataTable();//ddlGstinNo.DataSource =
        ddlShippingAdd.DataBind();// ddlGstinNo.DataBind();

        txtBillNo.Text = txtBillDate.Text = txtOrderNo.Text = txtOrderDate.Text = txtVoucherDate.Text = "";
        //ddlTds.ClearSelection();
        //ddlTCS.ClearSelection();
        //ddlRCM.ClearSelection();

        ddlItemName.ClearSelection();
        ddlUnitName.ClearSelection();
        //ddlIsd.ClearSelection();

        txtGRNNo.Text = txtGRNDate.Text = "";

        txtFree.Text = txtQty.Text = txtRate.Text = txtItemAmt.Text = txtDiscount.Text = "";// txtTax.Text = "";
        //txtTaxRate.Text = "";// txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text = "";
        txtItemRemark.Text = "";

        VsdtGvItemDetail = VsdtSundri = null;
        grdItemDetails.DataSource = gvotherCharge.DataSource = new DataTable();
        grdItemDetails.DataBind(); gvotherCharge.DataBind();

        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";

        txtGross.Text = txtAddLess.Text = txtNet.Text = "0";//txtTax.Text =
        txtNarration.ClearSelection();
        lblMsg.Text = "";

        ddlItemName.Enabled = false;

        txtPurchaseFrom.Text = txtShippingAdd.Text = "";//txtGstinNo.Text = "";

        ddlPurchaseFrom.Enabled = true;// ddlGstinNo.Enabled = true;
        //txtPurchaseFrom.Enabled = txtGstinNo.Enabled = true;

        ddlPayMode.Enabled = true;
        PayModeInit();
        ddlITC.ClearSelection();
        //ddlEdit.ClearSelection();

        //txtTaxRate.Enabled = false;// txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = false;

        txtVoucherDate.Focus();
    }


    void PayModeInit()
    {
        //ddlTds.Enabled = ddlTCS.Enabled = false;
        if (PayModeIsCredit)
        {

            //txtPurchaseFrom.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
            ddlPurchaseFrom.Visible = ddlShippingAdd.Visible = true;// ddlGstinNo.Visible =

            txtOrderNo.Enabled = txtOrderDate.Enabled = false;//= txtFree.Enabled = ddlTds.Enabled = ddlTCS.Enabled

            tblShippingDetail.Visible = true;
        }
        else
        {
            txtOrderNo.Enabled = txtOrderDate.Enabled = true;//= txtFree.Enabled ddlTds.Enabled = ddlTCS.Enabled = 

            //txtPurchaseFrom.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
            ddlPurchaseFrom.Visible = ddlShippingAdd.Visible = false;//ddlGstinNo.Visible =
            //tblShippingDetail.Visible = true;

        }
        ddlPayMode.Focus();
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }


    protected void ddlExpenseHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PayModeIsCredit)
        {
            if (CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) == 0)
            {
                ddlExpenseHead.SelectedIndex = 0;

                ddlPurchaseFrom.Focus();
                ShowMessage("Select Purchase From.", false);
                return;
            }

            //if (ddlGstinNo.SelectedItem != null)
            //{
            //    if (!string.IsNullOrEmpty(ddlGstinNo.SelectedItem.Text))
            //    {
            //        if (!CommonCls.GSTINIsValid(ddlGstinNo.SelectedItem.Text))
            //        {
            //            ddlGstinNo.Focus();
            //            ShowMessage("Invalid GSTIN.", false);
            //            return;
            //        }
            //    }
            //}
        }

        if (!PayModeIsCredit)
        {
            if (string.IsNullOrEmpty(txtPurchaseFrom.Text))
            {
                txtPurchaseFrom.Focus();
                ShowMessage("Enter Purchase From.", false);
                return;
            }
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
        //txtBillNo.Focus();
        ddlItemName.Focus();
    }


    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (CommonCls.ConvertIntZero(ddlExpenseHead.SelectedValue) == 0)
            {
                ddlExpenseHead.Focus();
                ShowMessage("Select Expense Head First.", false);
                return;
            }
            //VsdtItems = new DataTable();
            objPurchaseModel = new CompositionPurchaseModel();
            objPurchaseModel.Ind = 11;//1;
            objPurchaseModel.OrgID = GlobalSession.OrgID;
            objPurchaseModel.BrID = GlobalSession.BrID;
            objPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            objPurchaseModel.PartyCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue);
            objPurchaseModel.ByCashSale = PayModeIsCredit ? 0 : 1;
            objPurchaseModel.ItemID = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);

            //if (ddlGstinNo.SelectedItem != null)
            //{
            //    if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
            //    {
            //        objPurchaseModel.GSTIN = ddlGstinNo.SelectedValue;
            //    }
            //}

            if (PayModeIsCredit)
            {
                objPurchaseModel.GSTIN = "";//txtGstinNo.Text.ToUpper().ToString();
            }

            string uri = string.Format("CompositionPurchaseVoucher/FillItemPurchaseRate");
            DataSet dsItems = CommonCls.ApiPostDataSet(uri, objPurchaseModel);
            if (dsItems.Tables.Count > 0)
            {
                VsdtItems = dsItems.Tables[0];

                ddlUnitName.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
                txtRate.Text = VsdtItems.Rows[0]["ItemSellingRate"].ToString();

                if (VsdtItems.Rows[0]["ItemMinorUnitID"].ToString() == "")
                    ddlMinorUnit.SelectedValue = "0";
                else
                    ddlMinorUnit.SelectedValue = Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString();

                if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"].ToString()) == 1)
                {
                    VsDtItemSellRate = dsItems.Tables[1];
                    TaxWithInRange();
                }
                else
                    //FillTaxRate(VsdtItems.Rows[0]);

                    TaxCal();

                txtMinorUnitQty.Text = "";
                if (VsdtItems.Rows[0]["ItemMinorUnitID"].ToString() != "" && Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]) > 0)
                    txtMinorUnitQty.Enabled = true;
                //trMinorUnit.Visible = true;
                else
                    txtMinorUnitQty.Enabled = false;

                //if (GlobalSession.StockMaintaineByMinorUnit)
                //{
                //trMinorUnit.Visible = false;
                //}

                ddlPurchaseFrom.Enabled = false;// ddlGstinNo.Enabled = false;
                txtPurchaseFrom.Enabled = false;// txtGstinNo.Enabled = false;

            }
            txtQty.Focus();
        }

        catch (Exception ex)
        {
            ShowMessage("Internal Server Error.", false);
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
                    //FillTaxRate(item);
                    DrWithInRange.ItemArray = item.ItemArray;
                    break;
                }
            }

            //VsDtItemSellRate.Rows.RemoveAt(rowIndex - 1);
            VsDtItemSellRate.Rows[rowIndex - 1].Delete();
            VsDtItemSellRate.Rows.InsertAt(DrWithInRange, 0);
        }
    }


    //void FillTaxRate(DataRow drRate)
    //{
    //    //txtTaxRate.Text = drRate["TaxRate"].ToString();
    //    //txtCGSTAmt.Text = drRate["CGSTRate"].ToString();
    //    //txtSGSTAmt.Text = drRate["SGSTRate"].ToString();
    //    //txtIGSTAmt.Text = drRate["IGSTRate"].ToString();
    //    //txtCESSAmt.Text = drRate["Cess"].ToString();
    //}



    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(0);

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["Cess"]);

        StructItems GetItem = Calculation.TaxCal(item);
        //txtIGSTAmt.Text = (IgstAmt = GetItem.ItemIGSTAmt).ToString();
        //txtCGSTAmt.Text = (CgstAmt = GetItem.ItemCGSTAmt).ToString();
        //txtSGSTAmt.Text = (SgstAmt = GetItem.ItemSGSTAmt).ToString();
        //txtCESSAmt.Text = (CessAmt = GetItem.ItemCESSAmt).ToString();

        CgstRat = GetItem.ItemCGSTRate;
        SgstRat = GetItem.ItemSGSTRate;
        IgstRat = GetItem.ItemIGSTRate;
        CessRat = GetItem.ItemCESSRate;

        #region Old
        //decimal MaxAmt = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);

        //int devidedBy;
        //decimal TaxBy;

        //DataTable dtItems = VsdtItems;
        //if (dtItems == null || dtItems.Rows.Count <= 0)
        //{
        //    return;
        //}
        //decimal dtCGST = 0, dtSGST = 0, dtIGST = 0, dtCESS = 0;
        //TaxBy = CommonCls.ConvertDecimalZero(txtTaxRate.Text);
        ////if (ddlEditItem.SelectedValue == "0")
        //    //TaxBy = CommonCls.ConvertDecimalZero(txtTaxRate.Text);
        ////else
        ////{
        ////    TaxBy = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());

        ////    CgstRat = dtCGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"].ToString());
        ////    SgstRat = dtSGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"].ToString());
        ////    IgstRat = dtIGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"].ToString());
        ////    CessRat = dtCESS = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"].ToString());

        ////    CgstAmt = CommonCls.ConvertDecimalZero(txtCGSTAmt.Text);
        ////    SgstAmt = CommonCls.ConvertDecimalZero(txtSGSTAmt.Text);
        ////    IgstAmt = CommonCls.ConvertDecimalZero(txtIGSTAmt.Text);
        ////    CessAmt = CommonCls.ConvertDecimalZero(txtCESSAmt.Text);

        ////    return;
        ////}

        //dtCGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"].ToString());
        //dtSGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"].ToString());
        //dtIGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"].ToString());
        //dtCESS = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"].ToString());

        //int ValidCG = (dtCGST > 0 ? 1 : 0);
        //int ValidSG = (dtSGST > 0 ? 1 : 0);
        //int ValidIG = (dtIGST > 0 ? 1 : 0);
        //int ValidCess = (dtCESS > 0 ? 1 : 0);

        //devidedBy = ValidCG + ValidSG + ValidIG + ValidCess;

        //if (ValidCG == 1)
        //{
        //    CgstRat = TaxBy / devidedBy;
        //    CgstAmt = Math.Round(((MaxAmt * CgstRat) / 100), 2);
        //}
        //if (ValidIG == 1)
        //{
        //    IgstRat = TaxBy / devidedBy;
        //    IgstAmt = Math.Round(((MaxAmt * IgstRat) / 100), 2);
        //}
        //if (ValidSG == 1)
        //{
        //    SgstRat = TaxBy / devidedBy;
        //    SgstAmt = Math.Round(((MaxAmt * SgstRat) / 100), 2);
        //}
        //if (ValidCess == 1)
        //{
        //    CessRat = TaxBy / devidedBy;
        //    CessAmt = Math.Round(((MaxAmt * CessRat) / 100), 2);
        //}

        //txtIGSTAmt.Text = IgstAmt.ToString();
        //txtCGSTAmt.Text = CgstAmt.ToString();
        //txtSGSTAmt.Text = SgstAmt.ToString();
        //txtCESSAmt.Text = CessAmt.ToString();

        #endregion
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
            ShowMessage("Select Item Name.", false);
        }
    }


    void CalculateRate()
    {
        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        item.ItemFree = CommonCls.ConvertDecimalZero(txtFree.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtRate.Text);
        item.ItemDiscount = CommonCls.ConvertDecimalZero(txtDiscount.Text);

        StructItems GetItem = CalculateCompositionRate(item);
        txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
        txtItemAmt.Text = GetItem.ItemAmount.ToString();
        #region Old
        //decimal qty = 0;
        //decimal Rate = 0;
        //decimal disc = 0;
        //decimal Total = 0;
        //decimal free = 0;
        //if (txtQty.Text != "" && Convert.ToDecimal(txtQty.Text) != 0)
        //{
        //    qty = Convert.ToDecimal(txtQty.Text);
        //}
        //else
        //{
        //    txtItemTaxableAmt.Text = "0";
        //    txtItemAmt.Text = "0";

        //}
        //if (txtRate.Text != "" && Convert.ToDecimal(txtRate.Text) != 0)
        //{
        //    Rate = Convert.ToDecimal(txtRate.Text);
        //}
        //else
        //{
        //    txtItemTaxableAmt.Text = "0";
        //    txtItemAmt.Text = "0";
        //}
        //if (txtDiscount.Text != "" && Convert.ToDecimal(txtDiscount.Text) != 0)
        //{
        //    disc = Convert.ToDecimal(txtDiscount.Text);
        //}
        //try
        //{
        //    free = CommonCls.ConvertDecimalZero(txtFree.Text);
        //    Total = qty * Rate;
        //    if (!discountFlag)
        //    {
        //        txtItemAmt.Text = Math.Round(Total, 2).ToString();
        //    }

        //    if (disc == 0)
        //    {
        //        txtItemTaxableAmt.Text = Math.Round(((qty + free) * Rate), 2).ToString();
        //    }
        //    else
        //    {
        //        if (!discountFlag)
        //        {
        //            if (disc > Total)
        //            {
        //                ShowMessage("Discount amount should be less then item amount", false);
        //                txtDiscount.Focus();
        //                txtDiscount.Text = "0";
        //                return;
        //            }
        //            else
        //            {
        //                txtItemTaxableAmt.Text = Math.Round((((qty + free) * Rate) - disc), 2).ToString();
        //            }
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    ShowMessage(ex.Message, false);
        //}
        #endregion
    }


    public StructItems CalculateCompositionRate(StructItems item)
    {
        item.DiscountValue = item.ItemDiscount;
        if (item.DiscountInPerc)
        {
            item.DiscountValue = (((item.ItemQty) * item.ItemRate) * item.ItemDiscount) / 100;
        }
        item.ItemTaxable = Math.Round((((item.ItemQty) * item.ItemRate) - item.DiscountValue), 2);
        item.ItemAmount = Math.Round((item.ItemQty * item.ItemRate), 2);
        return item;
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
            ShowMessage("Select Item Name.", false);
        }
    }
    protected void btnAddItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation
            if (ddlExpenseHead.SelectedItem == null || CommonCls.ConvertIntZero(ddlExpenseHead.SelectedValue) == 0)
            {
                ddlExpenseHead.Focus();
                ShowMessage("Select Expense Head.", false);
                return;
            }

            if (ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
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

            if (string.IsNullOrEmpty(txtQty.Text) || txtQty.Text == "0")
            {
                txtQty.Focus();
                ShowMessage("Enter Item Quantity", false);
                return;
            }
            if (ddlUnitName.SelectedValue == "0")
            {
                ddlUnitName.Focus();
                ShowMessage("Select Item Unit ", false);
                return;
            }
            if (txtRate.Text == "" || txtRate.Text == "0")
            {
                txtRate.Focus();
                ShowMessage("Enter Item Rate", false);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtDiscount.Text) > CommonCls.ConvertDecimalZero(txtItemAmt.Text))
            {
                ShowMessage("Discount Not Greater Than To Taxable Amount.", false);
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
            #endregion

            BindGvItemDetail();
            CalculateItemTotalAmount();
            ClearItemDetailTable();
            AdvanceAdjustment();
            EnablePartyAdvanceCB();
            ddlExpenseHead.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
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
            DrGvItemDetail["PurchaseCode"] = ddlExpenseHead.SelectedValue;
            DrGvItemDetail["ItemID"] = Convert.ToInt32(ddlItemName.SelectedValue);
            DrGvItemDetail["HSNSACCode"] = dtItems.Rows[0]["HSNSACCode"];
            DrGvItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGvItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            DrGvItemDetail["ItemUnitID"] = Convert.ToInt32(ddlUnitName.SelectedValue);
            DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text);
            DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
            DrGvItemDetail["FreeQty"] = CommonCls.ConvertDecimalZero(txtFree.Text);
            DrGvItemDetail["ItemRate"] = CommonCls.ConvertDecimalZero(txtRate.Text);
            DrGvItemDetail["ItemAmount"] = CommonCls.ConvertDecimalZero(txtItemAmt.Text);
            DrGvItemDetail["DiscountValue"] = 0;
            DrGvItemDetail["DiscountType"] = 0;
            DrGvItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(txtDiscount.Text);
            DrGvItemDetail["NetAmt"] = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);


            //if (ddlEdit.SelectedValue == "1")
            //{
            //    DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(txtTaxRate.Text);//Convert.ToDecimal(dtItems.Rows[0]["TaxRate"].ToString());
            //    DrGvItemDetail["IGSTTaxAmt"] = CommonCls.ConvertDecimalZero(0);
            //    DrGvItemDetail["SGSTTaxAmt"] = CommonCls.ConvertDecimalZero(0);
            //    DrGvItemDetail["CGSTTaxAmt"] = CommonCls.ConvertDecimalZero(0);
            //    DrGvItemDetail["CESSTaxAmt"] = CommonCls.ConvertDecimalZero(0);
            //}
            //else
            //{
            TaxCal();

            //if (Convert.ToInt32(VsdtItems.Rows[0]["ItemMinorUnitID"]).ToString() == "0")
            //    DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());
            //else

            //DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(0);
            DrGvItemDetail["TaxRate"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"]);

            DrGvItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGvItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGvItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGvItemDetail["CESSTaxAmt"] = CessAmt;
            //}

            DrGvItemDetail["IGSTTax"] = IgstRat; //Convert.ToDecimal(dtItems.Rows[0]["IGSTRate"]);
            DrGvItemDetail["SGSTTax"] = SgstRat; //Convert.ToDecimal(dtItems.Rows[0]["SGSTRate"]);
            DrGvItemDetail["CGSTTax"] = CgstRat; //Convert.ToDecimal(dtItems.Rows[0]["CGSTRate"]);
            DrGvItemDetail["CESSTax"] = CessRat; //Convert.ToDecimal(dtItems.Rows[0]["Cess"]);
            DrGvItemDetail["ITCApplicable"] = Convert.ToInt16(ddlITC.SelectedValue);
            DrGvItemDetail["ITCIGSTAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? IgstAmt : 0;
            DrGvItemDetail["ITCSGSTAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? SgstAmt : 0;
            DrGvItemDetail["ITCCGSTAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? CgstAmt : 0;
            DrGvItemDetail["ITCCESSAmt"] = Convert.ToInt16(ddlITC.SelectedValue) == 1 ? CessAmt : 0;
            DrGvItemDetail["FreeItemInd"] = 0;
            DrGvItemDetail["ItemRemark"] = txtItemRemark.Text;
            DrGvItemDetail["ItemInd"] = Convert.ToInt16(0);
            DrGvItemDetail["ItemInd2"] = "";
            DrGvItemDetail["ItemInd3"] = 0;

            dtGvItemDetail.Rows.Add(DrGvItemDetail);
            grdItemDetails.DataSource = VsdtGvItemDetail = dtGvItemDetail;
            grdItemDetails.DataBind();
            //VsdtItems = null;
        }
    }

    void CalculateItemTotalAmount()
    {
        var cal = CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);
        txtGross.Text = cal.TotalGross.ToString();
        //txtTax.Text = cal.TotalTaxable.ToString();
        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtNet.Text = cal.TotalAllNet.ToString();
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

    DataTable DtItemsSchema()
    {
        DataTable dtItems = new DataTable();

        dtItems.Columns.Add("PurchaseHeadName", typeof(string));
        dtItems.Columns.Add("ItemName", typeof(string));
        dtItems.Columns.Add("ItemUnit", typeof(string));
        dtItems.Columns.Add("ItemSecondaryUnit", typeof(string));

        dtItems.Columns.Add("PurchaseCode", typeof(int));
        dtItems.Columns.Add("ItemID", typeof(int));
        dtItems.Columns.Add("HSNSACCode", typeof(string));
        dtItems.Columns.Add("GoodsServiceInd", typeof(int));
        dtItems.Columns.Add("ItemQty", typeof(int));
        dtItems.Columns.Add("ItemUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorQty", typeof(decimal));
        dtItems.Columns.Add("ItemMinorUnitID", typeof(int));
        dtItems.Columns.Add("FreeQty", typeof(int));
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

        #region Old Structure
        //dtItems.Columns.Add("ItemName", typeof(string));
        //dtItems.Columns.Add("ItemUnit", typeof(string));
        //dtItems.Columns.Add("PADesc", typeof(string));
        //dtItems.Columns.Add("ISDDesc", typeof(string));

        //dtItems.Columns.Add("ItemID", typeof(int));
        //dtItems.Columns.Add("HSNSACCode", typeof(string));
        //dtItems.Columns.Add("GoodsServiceInd", typeof(int));
        //dtItems.Columns.Add("ItemQty", typeof(decimal));
        //dtItems.Columns.Add("FreeQty", typeof(decimal));
        //dtItems.Columns.Add("ItemUnitID", typeof(int));
        //dtItems.Columns.Add("ItemRate", typeof(decimal));
        //dtItems.Columns.Add("ItemAmount", typeof(decimal));
        //dtItems.Columns.Add("DiscountValue", typeof(int)); //0 Pending default;
        //dtItems.Columns.Add("DiscountType", typeof(int)); //0 Pending default;
        //dtItems.Columns.Add("DiscountAmt", typeof(decimal));
        //dtItems.Columns.Add("NetAmt", typeof(decimal));
        //dtItems.Columns.Add("PA", typeof(int));
        //dtItems.Columns.Add("TaxRate", typeof(decimal));
        //dtItems.Columns.Add("IGSTTax", typeof(decimal));
        //dtItems.Columns.Add("IGSTTaxAmt", typeof(decimal));
        //dtItems.Columns.Add("SGSTTax", typeof(decimal));
        //dtItems.Columns.Add("SGSTTaxAmt", typeof(decimal));
        //dtItems.Columns.Add("CGSTTax", typeof(decimal));
        //dtItems.Columns.Add("CGSTTaxAmt", typeof(decimal));
        //dtItems.Columns.Add("CESSTax", typeof(decimal));
        //dtItems.Columns.Add("CESSTaxAmt", typeof(decimal));
        //dtItems.Columns.Add("ISDApplicable", typeof(int));
        //dtItems.Columns.Add("ItemRemark", typeof(string));//0 Pending default;
        #endregion
        return dtItems;
    }

    void ClearItemDetailTable()
    {
        //trMinorUnit.Visible = false;
        ddlPayMode.Enabled = false;

        ddlExpenseHead.SelectedValue = ddlItemName.SelectedValue = ddlUnitName.SelectedValue = ddlMinorUnit.SelectedValue = "0";
        ddlITC.ClearSelection();
        //ddlEdit.ClearSelection();
        txtItemTaxableAmt.Enabled = false;// txtTaxRate.Enabled = false;// txtCGSTAmt.Enabled = txtSGSTAmt.Enabled = txtIGSTAmt.Enabled = txtCESSAmt.Enabled = false;

        txtQty.Text = txtFree.Text = txtRate.Text = txtItemAmt.Text =
        txtDiscount.Text = txtItemTaxableAmt.Text = // txtTaxRate.Text =
            // txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text =

        txtItemRemark.Text = txtMinorUnitQty.Text = "";

        VsdtItems = null;
    }

    protected void grdItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "DeleteRow")
            {
                dtGrdItemDetails = VsdtGvItemDetail;
                dtGrdItemDetails.Rows[rowIndex].Delete();

                VsdtGvItemDetail = dtGrdItemDetails;
                grdItemDetails.DataSource = dtGrdItemDetails;
                grdItemDetails.DataBind();
                CalculateItemTotalAmount();


                if (!pnlParentAdvance.Visible)
                {
                    return;
                }
                foreach (DataRow drDelItem in VsdtRemainAmt.Rows)
                {
                    DataRow[] drItemRates = VsdtGvItemDetail.Select("TaxRate=" + drDelItem["TaxRate"]);
                    if (drItemRates.Count() > 0)
                    {
                        DataRow drItemRate = drItemRates[0];
                        if (drItemRate["TaxRate"].ToString() == drDelItem["TaxRate"].ToString())
                        {
                            decimal InvoiceAmount = CommonCls.ConvertDecimalZero(VsdtGvItemDetail.Compute("Sum(ItemAmount)", "TaxRate=" + drDelItem["TaxRate"].ToString()));
                            drDelItem["InvoiceAmt"] = InvoiceAmount;
                            drDelItem["RemainingAmt"] = InvoiceAmount;
                        }
                        else
                        {
                            drDelItem["InvoiceAmt"] = 0;
                            drDelItem["RemainingAmt"] = 0;
                        }
                    }
                    else
                    {
                        drDelItem["InvoiceAmt"] = 0;
                        drDelItem["RemainingAmt"] = 0;
                    }
                }

                AdvanceAdjustment();
                EnablePartyAdvanceCB();

                #region After Remaining
                foreach (DataRow drRemain in VsdtRemainAmt.Rows)
                {
                    //DataRow drRemain = VsdtRemainAmt.Select("TaxRate=" + lblTaxRate.Text)[0];
                    if (CommonCls.ConvertDecimalZero(drRemain["RemainingAmt"]) <= 0)
                    {
                        foreach (GridViewRow GvSelection in GvPartyAdvance.Rows)
                        {
                            decimal gvTax = CommonCls.ConvertDecimalZero((GvSelection.FindControl("lblTaxRate") as Label).Text);
                            if (gvTax == CommonCls.ConvertDecimalZero(drRemain["TaxRate"]))
                            {
                                CheckBox cbSelection = (CheckBox)GvSelection.FindControl("cbPartyAdvance");
                                if (!cbSelection.Checked)
                                {
                                    GvSelection.BackColor = Color.FromArgb(235, 235, 228);
                                    cbSelection.Enabled = false;
                                }
                                else
                                {
                                    GvSelection.BackColor = Color.White;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (GridViewRow GvSelection in GvPartyAdvance.Rows)
                        {
                            decimal gvTax = CommonCls.ConvertDecimalZero((GvSelection.FindControl("lblTaxRate") as Label).Text);
                            if (gvTax == CommonCls.ConvertDecimalZero(drRemain["TaxRate"]))
                            {
                                CheckBox cbSelection = (CheckBox)GvSelection.FindControl("cbPartyAdvance");
                                cbSelection.Enabled = true;
                                GvSelection.BackColor = Color.White;
                            }
                            //else
                            //{
                            //    GvSelection.BackColor = Color.FromArgb(235, 235, 228);
                            //}
                        }
                    }

                }
                #endregion
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataRow drGvItem = VsdtGvItemDetail.Rows[e.Row.RowIndex];
            if (e.Row.RowIndex >= 0)
            {
                Label lblITC = (Label)e.Row.FindControl("lblITC");
                if (lblITC.Text == "1")
                    lblITC.Text = "Yes";
                else
                    lblITC.Text = "No";

                //Label lblEdit = (Label)e.Row.FindControl("lblEdit");
                //if (lblEdit.Text == "1")
                //    lblEdit.Text = "Yes";
                //else
                //    lblEdit.Text = "No";

                Label lblMinorUnit = (Label)e.Row.FindControl("lblMinorUnit");
                if (CommonCls.ConvertIntZero(lblMinorUnit.Text) == 0)
                    lblMinorUnit.Text = "";
                else
                {
                    ddlMinorUnit.SelectedValue = lblMinorUnit.Text;
                    lblMinorUnit.Text = ddlMinorUnit.SelectedItem.Text;
                }
            }
        }
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false);
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
                ShowMessage("Discount Not Greater Than To Taxable Amount.", false);
                txtDiscount.Text = "0";
                CalculateRate();
                TaxCal();
                txtDiscount.Focus();
                return;
            }
            CalculateRate();
            TaxCal();
            //if (txtItemTaxableAmt.Enabled)
            //    txtItemTaxableAmt.Focus();
            //else
            //    btnAddItemDetail.Focus();
            //ddlEdit.Focus();
        }
        catch (Exception ex)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false);
        }

    }
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

        objPurchaseModel = new CompositionPurchaseModel();
        objPurchaseModel.Ind = 11;//1;
        objPurchaseModel.OrgID = GlobalSession.OrgID;
        objPurchaseModel.BrID = GlobalSession.BrID;
        objPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objPurchaseModel.PartyCode = CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue);
        objPurchaseModel.ByCashSale = PayModeIsCredit ? 0 : 1;
        objPurchaseModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);

        //if (ddlGstinNo.SelectedItem != null)
        //{
        //    if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
        //    {
        //        objPurchaseModel.GSTIN = ddlGstinNo.SelectedValue;
        //    }
        //}

        if (PayModeIsCredit)
        {
            objPurchaseModel.GSTIN = "";//txtGstinNo.Text.ToUpper().ToString();
        }

        string uri = string.Format("CompositionPurchaseVoucher/FillItemPurchaseRate");
        DataSet dsItems = CommonCls.ApiPostDataSet(uri, objPurchaseModel);
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
                ShowMessage("Insert Item Detail First.", false);
                ddlExpenseHead.Focus();
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
            drItem["PurchaseCode"] = 0;

            drItem["ItemID"] = ddlFreeItemName.SelectedValue;
            drItem["ItemName"] = ddlFreeItemName.SelectedItem.Text;
            drItem["ItemUnitID"] = ddlFreeUnit.SelectedValue;
            drItem["ItemUnit"] = ddlFreeUnit.SelectedItem.Text;
            drItem["ItemQty"] = CommonCls.ConvertDecimalZero(txtFreeQty.Text);
            drItem["FreeItemInd"] = 1;
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

    void ClearFreeItem()
    {
        ddlFreeItemName.ClearSelection();
        ddlFreeUnit.ClearSelection();
        txtFreeQty.Text = "";
        VsdtItems = null;
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlHeadName.SelectedValue) == 0)
        {
            ShowMessage("Select Head Name.", false);
            ddlHeadName.Focus();
            return;
        }
        if (ddlAddLess.SelectedValue == "0")
        {
            ShowMessage("Select Add Less", false);
            ddlAddLess.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtOtherChrgAmount.Text) || Convert.ToDecimal(txtOtherChrgAmount.Text) <= 0)
        {
            ShowMessage("Enter Other Charges Amount.", false);
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
                ShowMessage("This Charge Already.", false);
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
        //CaluculateOtherChargeAmount(dtGrdOtherCharge);
        ClearOtherCharge();
        CalculateItemTotalAmount();
        ddlHeadName.Focus();
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


    void ClearOtherCharge()
    {
        ddlHeadName.ClearSelection();
        txtOtherChrgAmount.Text = "";
        ddlAddLess.ClearSelection();
    }


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
                //CaluculateOtherChargeAmount(dtGrdOtherCharge);
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnSave_Click(object sender, EventArgs e) // Save Purchase Voucher
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

            objPurchaseModel = new CompositionPurchaseModel();
            objPurchaseModel.Ind = 1;
            objPurchaseModel.OrgID = GlobalSession.OrgID;
            objPurchaseModel.BrID = GlobalSession.BrID;
            objPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            objPurchaseModel.EntryType = 1;
            objPurchaseModel.YrCD = GlobalSession.YrCD;
            objPurchaseModel.User = GlobalSession.UserID;
            objPurchaseModel.IP = GlobalSession.IP;

            objPurchaseModel.ByCashSale = PayModeIsCredit ? 0 : 1;
            objPurchaseModel.PartyName = PayModeIsCredit ? ddlPurchaseFrom.SelectedItem.Text.ToUpper() : txtPurchaseFrom.Text;
            objPurchaseModel.PartyGSTIN = "";//PayModeIsCredit ;&& ddlGstinNo.SelectedItem != null ? ddlGstinNo.SelectedItem.Text.ToUpper() : txtGstinNo.Text.ToUpper();
            objPurchaseModel.PartyAddress = PayModeIsCredit && ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : txtShippingAdd.Text.ToUpper();

            //objPurchaseModel.ByCashSale = PayModeIsCredit ? 1 : 0;
            //objPurchaseModel.PartyGSTIN = PayModeIsCredit ? txtGstinNo.Text.ToUpper() : ddlGstinNo.SelectedItem != null ? ddlGstinNo.SelectedItem.Text.ToUpper() : "";
            //objPurchaseModel.PartyAddress = PayModeIsCredit ? txtShippingAdd.Text.ToUpper() :
            //                                ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            objPurchaseModel.WareHouseID = Convert.ToInt32(ddlLocation.SelectedValue);
            objPurchaseModel.PurchaseBillNo = txtBillNo.Text;
            objPurchaseModel.PurchaseBillDate = CommonCls.ConvertToDate(txtBillDate.Text);
            objPurchaseModel.PONo = txtOrderNo.Text;
            objPurchaseModel.GRNNo = txtGRNNo.Text;
            objPurchaseModel.VoucherDate = CommonCls.ConvertToDate(txtVoucherDate.Text);

            objPurchaseModel.DtPurchase = DtPurchaseSchema();
            objPurchaseModel.DtItems = DtItemsSchema();
            //objPurchaseModel.DtSundries = DtSundriesSchema();
            objPurchaseModel.DtAdjAdvance = CreateAdvanceDt();

            objPurchaseModel.DtPurchase = CreatePurchaseData();
            if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            {
                foreach (DataRow item in VsdtGvFreeItem.Rows)
                {
                    VsdtGvItemDetail.Rows.Add(item.ItemArray);
                }
            }

            objPurchaseModel.DtItems = VsdtGvItemDetail;
            objPurchaseModel.DtSundries = VsdtSundri;
            if ((objPurchaseModel.DtSundries == null) || (objPurchaseModel.DtSundries.Rows.Count == 0))
            {
                objPurchaseModel.DtSundries = DtSundriesSchema();
                DataRow drSaleSundri = objPurchaseModel.DtSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                objPurchaseModel.DtSundries.Rows.Add(drSaleSundri);
            }

            string uri = string.Format("CompositionPurchaseVoucher/SavePurchase");
            DataTable dtSavePurchase = CommonCls.ApiPostDataTable(uri, objPurchaseModel);
            if (dtSavePurchase.Rows.Count > 0)
            {
                if (dtSavePurchase.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    chkBankEntry.Checked = false;
                    chkBankEntry_CheckedChanged(sender, e);
                    string VoucherNo, VoucherDate;//, InvoiceName;
                    VoucherNo = dtSavePurchase.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSavePurchase.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                    //InvoiceName = dtSave.Rows[0]["InvoiceName"].ToString();
                    ShowMessage("Record Save successfully with voucher no. " + VoucherNo, true);
                    txtVoucherDate.Focus();
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date " + VoucherNo + " - " + VoucherDate;
                    hfLastVoucherDate.Value = VoucherDate;
                    CallReport(VoucherNo, CommonCls.ConvertToDate(VoucherDate));

                }
                else if (dtSavePurchase.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    //ClearAll();
                    ShowMessage("Voucher No. Duplicate.", false);
                    txtBillNo.Focus();
                }
                if (dtSavePurchase.Rows[0]["ReturnInd"].ToString() == "12")
                {
                    ShowMessage("No Record Found For Given Vouchar No." + txtVoucherNo.Text + " & Date" + txtVoucherDate.Text, false);
                    txtVoucherNo.Focus();
                }
                else if (dtSavePurchase.Rows[0]["ReturnInd"].ToString() == "13")
                {
                    ShowMessage(" Back Date Vouchar Already Available For Given Vouchar No." + txtVoucherNo.Text + " & Date." + txtVoucherDate.Text + " Please Give Correct Vouchar No. ", false);
                    txtVoucherNo.Focus();
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


    public void CallReport(string InvoiceNo, string InvoiceDate)
    {

        Hashtable HT = new Hashtable();
        HT.Add("Ind", 1);

        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "PURCHASE VOUCHER");

        HT.Add("Doctype", 5);
        HT.Add("invoiceno", Convert.ToInt32(InvoiceNo));
        HT.Add("invoiceDate", InvoiceDate);
        HT.Add("invoiceDateFrom", "");
        HT.Add("invoiceDateto", "");
        HT.Add("cashsalesind", 1);

        VouchersReport.ReportName = "RptPurchaseVoucher" + "_BOS_UnReg";
        VouchersReport.FileName = "PurchaseVoucher";
        VouchersReport.ReportHeading = "Purchase Voucher";
        VouchersReport.HashTable = HT;

        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print Voucher";

        VouchersReport.ShowReport();
    }

    DataTable CreatePurchaseData()
    {
        DataTable dtCreatePurchaseData = new DataTable();

        dtCreatePurchaseData = DtPurchaseSchema(); //new DataTable();
        DataRow drCreatePurchaseData = dtCreatePurchaseData.NewRow();

        //drCreatePurchaseData["AccCode"] = ddlExpenseHead.SelectedValue == CashSalesAcc ? 919000 : CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue);

        drCreatePurchaseData["AccCode"] = PayModeIsCredit ? CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) : Convert.ToInt32(ddlPayMode.SelectedValue);

        drCreatePurchaseData["AccGst"] = "";// ddlGstinNo.SelectedItem != null ? !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ? ddlGstinNo.SelectedValue : "" : "";
        drCreatePurchaseData["SalePurchaseCode"] = 0;//Convert.ToInt32(ddlExpenseHead.SelectedValue);

        if (ddlShippingAdd.SelectedItem != null && CommonCls.ConvertIntZero(ddlShippingAdd.SelectedValue) != 0)
        {
            drCreatePurchaseData["AccPOSID"] = Convert.ToInt32(ddlShippingAdd.SelectedValue);
        }
        drCreatePurchaseData["WarehouseID"] = ddlLocation.SelectedItem != null ? Convert.ToInt32(ddlLocation.SelectedValue) : 0;
        drCreatePurchaseData["OrderNo"] = 0;//txtOrderNo.Text == "" ? 0 : Convert.ToInt32(txtOrderNo.Text);
        drCreatePurchaseData["OrderDate"] = !string.IsNullOrEmpty(txtOrderDate.Text) ? CommonCls.ConvertToDate(txtOrderDate.Text) : "";
        //drCreatePurchaseData["InvoiceNo"] = 0;
        if (chkBankEntry.Checked == true)//if Back Date Entry is True then the help of invoice no sent voucher no
        {

            drCreatePurchaseData["InvoiceNo"] = CommonCls.ConvertIntZero(txtVoucherNo.Text);   //Voucher No.
        }
        else
        {
            drCreatePurchaseData["InvoiceNo"] = 0;   //For Current Voucher
        }
        drCreatePurchaseData["InvoiceDate"] = "";
        drCreatePurchaseData["TDSApplicable"] = Convert.ToInt32(0);
        drCreatePurchaseData["TCSApplicable"] = Convert.ToInt32(0);
        drCreatePurchaseData["RCMApplicable"] = Convert.ToInt32(0);
        drCreatePurchaseData["GrossAmt"] = Convert.ToDecimal(txtGross.Text);
        drCreatePurchaseData["TaxAmt"] = Convert.ToDecimal(0);
        drCreatePurchaseData["NetAmt"] = Convert.ToDecimal(txtNet.Text);
        drCreatePurchaseData["RoundOffAmt"] = 0;
        drCreatePurchaseData["TransportID"] = 0;
        drCreatePurchaseData["VehicleNo"] = "";
        drCreatePurchaseData["WayBillNo"] = 0;
        drCreatePurchaseData["TransportDate"] = "";
        drCreatePurchaseData["DocDesc"] = txtNarration.Text;

        if (!string.IsNullOrEmpty(txtGRNNo.Text))
        {
            drCreatePurchaseData["GRNNo"] = CommonCls.ConvertIntZero(txtGRNNo.Text);
            drCreatePurchaseData["GRNDate"] = CommonCls.ConvertToDate(txtGRNDate.Text);
        }

        //drCreateSaleData["UserID"] = Convert.ToInt32(Session["UserID"]);
        //drCreateSaleData["IP"] = CommonCls.GetIP();

        dtCreatePurchaseData.Rows.Add(drCreatePurchaseData);

        return dtCreatePurchaseData;

    }

    bool ValidationBTNSAVE()
    {


        if (chkBankEntry.Checked == false)
        {
            if (!string.IsNullOrEmpty(hfLastVoucherDate.Value))
            {
                DateTime VoucherDate = DateTime.ParseExact(txtVoucherDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime lastVoucherDate = DateTime.ParseExact(hfLastVoucherDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture); //server

                if (VoucherDate < lastVoucherDate)
                {
                    txtVoucherDate.Focus();
                    ShowMessage("Please Use Back Date Entry Option.", false);
                    return false;
                }
            }
        }


        if (chkBankEntry.Checked == true)
        {
            if (txtVoucherNo.Text == "" || Convert.ToInt32(txtVoucherNo.Text) == 0)
            {
                txtVoucherNo.Focus();
                ShowMessage("Please Enter Voucher No.", false);
                return false;
            }
            if (txtVoucherDate.Text == "")
            {
                txtVoucherDate.Focus();
                ShowMessage("Please Enter Voucher Date.", false);
                return false;
            }

            LastVoucherDate();

            DateTime CurrentDate = DateTime.ParseExact(CommonCls.ConvertDateDB(VsdtLastVoucherDate.Rows[0]["LastDate"]), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime VDate = DateTime.ParseExact(txtVoucherDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

            if (VDate >= CurrentDate)
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Is Should Not Be Current Date For Back Date Entry", false);
                return false;
            }

            //string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

            //if (CommonCls.ConvertDateDB(txtVoucherDate.Text) == CommonCls.ConvertDateDB(CurrentDate))
            //{
            //    txtVoucherDate.Focus();
            //    ShowMessage("Voucher Date Is Should Not Be Current Date For Back Date Entry", false);
            //    return;
            //}

        }
        if (PayModeIsCredit)
        {
            if (CommonCls.ConvertIntZero(ddlPurchaseFrom.SelectedValue) == 0)
            {
                ddlPurchaseFrom.Focus();
                ShowMessage("Select Purchase From.", false);
                return false;
            }
        }

        if (!PayModeIsCredit)
        {
            if (string.IsNullOrEmpty(txtPurchaseFrom.Text))
            {
                txtPurchaseFrom.Focus();
                ShowMessage("Enter Purchase From.", false);
                return false;
            }

            //if (CommonCls.ConvertIntZero(ddlAccountType.SelectedValue) == 0)
            //{
            //    ddlAccountType.Focus();
            //    ShowMessage("Select Account Type.", false);
            //    return false;
            //}
        }

        if (string.IsNullOrEmpty(txtVoucherDate.Text))
        {
            txtVoucherDate.Focus();
            ShowMessage("Please Enter Voucher Date.", false);
            return false;
        }

        //if (ddlExpenseHead != null && ddlExpenseHead.Items.Count > 1)
        //{
        //    if (ddlExpenseHead.SelectedIndex <= 0)
        //    {
        //        ddlExpenseHead.Focus();
        //        ShowMessage("Select Expense Head.", false);
        //        return false;
        //    }
        //}

        if (string.IsNullOrEmpty(txtBillNo.Text)) // Invoice Number Shouldn't be Null
        {
            txtBillNo.Focus();
            ShowMessage("Please Enter Bill No.", false);
            return false;
        }

        if (string.IsNullOrEmpty(txtBillDate.Text))
        {
            txtBillDate.Focus();
            ShowMessage("Enter Bill Date.", false);
            return false;
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtBillDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtBillDate.Focus();
            ShowMessage("Bill Date Should Not Be More Than Todays Date!", false);
            return false;
        }

        if (!string.IsNullOrEmpty(txtOrderDate.Text))
        {
            bool OrderDate = CommonCls.CheckFinancialYrDate(txtOrderDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Should Not Be More Than Todays Date!", false);
                return false;
            }

            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                txtOrderNo.Focus();
                ShowMessage("Order No. Compulsory If Order Date Not Empty.", false);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtOrderNo.Text))
        {
            if (string.IsNullOrEmpty(txtOrderDate.Text))
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Compulsory If Order No. Not Empty.", false);
                return false;
            }

            bool OrderDate = CommonCls.CheckFinancialYrDate(txtOrderDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtOrderDate.Focus();
                ShowMessage("Order Date Should Not Be More Than Todays Date!", false);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtGRNDate.Text))
        {
            bool OrderDate = CommonCls.CheckFinancialYrDate(txtGRNDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtGRNDate.Focus();
                ShowMessage("GRN Date Should Not Be More Than Todays Date!", false);
                return false;
            }

            if (string.IsNullOrEmpty(txtGRNNo.Text))
            {
                txtGRNNo.Focus();
                ShowMessage("GRN No. Compulsory If GRN Date Not Empty.", false);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtGRNNo.Text))
        {
            if (string.IsNullOrEmpty(txtGRNDate.Text))
            {
                txtGRNDate.Focus();
                ShowMessage("GRN Date Compulsory If GRN No. Not Empty.", false);
                return false;
            }

            bool OrderDate = CommonCls.CheckFinancialYrDate(txtGRNDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!OrderDate) // For Voucher Date Between Financial Year.
            {
                txtGRNDate.Focus();
                ShowMessage("GRN Date Should Not Be More Than Todays Date!", false);
                return false;
            }
        }



        //if (ddlExpenseHead.SelectedValue == CashSalesAcc && string.IsNullOrEmpty(txtPurchaseFrom.Text))
        //{
        //    txtPurchaseFrom.Focus();
        //    ShowMessage("Enter Purchase From Name.", false);
        //    return false;
        //}

        if (ddlLocation.Items.Count > 1)
        {
            if (ddlLocation.SelectedValue == "0" || ddlLocation.SelectedValue == "")
            {
                ddlLocation.Focus();
                ShowMessage("Select Location.", false);
                return false;
            }
        }

        if (VsdtGvItemDetail == null || VsdtGvItemDetail.Rows.Count <= 0)
        {
            ShowMessage("Item Detail Can Not Be Null.", false);
            return false;
        }
        //if (string.IsNullOrEmpty(txtTax.Text) || Convert.ToDecimal(txtTax.Text) < 0)
        //{
        //    ShowMessage("Taxable Amount Can Not Be Negative Please Check Entry.", false);
        //    return false;
        //}
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

        return true;
    }



    void LastVoucherDate()
    {

        objPurchaseModel = new CompositionPurchaseModel();
        objPurchaseModel.Ind = 11;
        objPurchaseModel.OrgID = GlobalSession.OrgID;
        objPurchaseModel.BrID = GlobalSession.BrID;
        objPurchaseModel.YrCD = GlobalSession.YrCD;
        objPurchaseModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("CompositionPurchaseVoucher/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, objPurchaseModel);

        if (dsBindAll.Tables.Count > 0)
        {
            DataTable dtLastVoucher = dsBindAll.Tables[2];

            // Last Voucher No And Date
            if (dtLastVoucher.Rows.Count > 0)
            {
                if (CommonCls.ConvertIntZero(dtLastVoucher.Rows[0]["LastNo"]) != 0)
                {
                    VsdtLastVoucherDate = dtLastVoucher;
                }
            }
        }
    }

    DataTable DtPurchaseSchema()
    {
        DataTable dtPurchase = new DataTable();
        //dtPurchase.Columns.Add("DocDate", typeof(string));
        //dtPurchase.Columns.Add("DocNo", typeof(int));

        dtPurchase.Columns.Add("AccCode", typeof(int));
        dtPurchase.Columns.Add("AccGst", typeof(string));
        dtPurchase.Columns.Add("SalePurchaseCode", typeof(int));
        dtPurchase.Columns.Add("AccPOSID", typeof(int));
        dtPurchase.Columns.Add("GSTIN", typeof(string));
        dtPurchase.Columns.Add("WareHouseID", typeof(int));
        dtPurchase.Columns.Add("OrderNo", typeof(int));
        dtPurchase.Columns.Add("OrderDate", typeof(string));
        dtPurchase.Columns.Add("InvoiceNo", typeof(string));
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
        //dtPurchase.Columns.Add("UserID", typeof(int));
        //dtPurchase.Columns.Add("IP", typeof(string));

        return dtPurchase;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        chkBankEntry.Checked = false;
        chkBankEntry_CheckedChanged(sender, e);
        ddlPayMode.Focus();
    }

    #region Party Advance Detail

    int priorityCount
    {
        get { return CommonCls.ConvertIntZero(ViewState["priorityCount"]); }
        set { ViewState["priorityCount"] = value; }
    }

    DataTable VsdtAdvance
    {
        get { return (DataTable)ViewState["dtAdvance"]; }
        set { ViewState["dtAdvance"] = value; }
    }

    DataTable VsdtRemainAmt
    {
        get { return (DataTable)ViewState["VsdtRemainAmt"]; }
        set { ViewState["VsdtRemainAmt"] = value; }
    }

    DataTable ContainRemain()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("TaxRate", typeof(decimal));
        dt.Columns.Add("InvoiceAmt", typeof(decimal));
        dt.Columns.Add("RemainingAmt", typeof(decimal));
        return dt;
    }

    protected void btnShowPartyAdvance_Click(object sender, EventArgs e)
    {
        if (pnlPartyAdvance.Visible)
            pnlPartyAdvance.Visible = false;
        else
            pnlPartyAdvance.Visible = true;
    }

    protected void cbPartyAdvance_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbPartyAdvance = (CheckBox)sender;
        GridViewRow GvRow = (GridViewRow)cbPartyAdvance.NamingContainer;
        Label lblTaxRate = (Label)GvRow.FindControl("lblTaxRate");
        Label lblVoucharNo = (Label)GvRow.FindControl("lblVoucharNo");

        DataRow drChecked = VsdtAdvance.Select("VoucharNo=" + lblVoucharNo.Text + " And TaxRate=" + lblTaxRate.Text)[0];
        //drChecked["AdjPriority"] = ++priorityCount;
        drChecked["CheckedInd"] = cbPartyAdvance.Checked;
        if (!cbPartyAdvance.Checked)
        {
            drChecked["AdjPriority"] = 2;
        }

        DataView dvItem = new DataView(VsdtAdvance);
        dvItem.RowFilter = "TaxRate=" + lblTaxRate.Text;
        //dvItem.Sort = " AdjPriority ASC";

        int Order = 900;
        foreach (DataRow item in dvItem.ToTable().Rows)
        {
            DataRow drSetAdv = VsdtAdvance.Select(" VoucharNo=" + item["VoucharNo"] + "And TaxRate=" + item["TaxRate"])[0];
            int maxcheckno = Convert.ToInt32(VsdtAdvance.Compute("max([AdjPriority])", string.Empty));

            if (!string.IsNullOrEmpty(drSetAdv["CheckedInd"].ToString()) && Convert.ToBoolean(drSetAdv["CheckedInd"]) && Convert.ToInt32(drSetAdv["AdjPriority"]) < 900)
            {
                if (maxcheckno == 2)
                    drSetAdv["AdjPriority"] = Order;
                else
                    drSetAdv["AdjPriority"] = ++maxcheckno;
            }
        }
        //foreach (DataRow item in dvItem.ToTable().Rows)
        //{
        //    DataRow drSetAdv = VsdtAdvance.Select(" VoucharNo=" + item["VoucharNo"] + "And TaxRate=" + item["TaxRate"])[0];
        //    drSetAdv["AdjPriority"] = Order++;
        //}

        AdvanceAdjustment();

        #region After Remaining
        DataRow drRemain = VsdtRemainAmt.Select("TaxRate=" + lblTaxRate.Text)[0];
        if (CommonCls.ConvertDecimalZero(drRemain["RemainingAmt"]) <= 0)
        {
            foreach (GridViewRow GvSelection in GvPartyAdvance.Rows)
            {
                decimal gvTax = CommonCls.ConvertDecimalZero((GvSelection.FindControl("lblTaxRate") as Label).Text);
                if (gvTax == CommonCls.ConvertDecimalZero(drRemain["TaxRate"]))
                {
                    CheckBox cbSelection = (CheckBox)GvSelection.FindControl("cbPartyAdvance");
                    if (!cbSelection.Checked)
                    {
                        GvSelection.BackColor = Color.FromArgb(235, 235, 228);
                        cbSelection.Enabled = false;
                    }
                    else
                    {
                        GvSelection.BackColor = Color.White;
                    }
                }
            }
        }
        else
        {
            foreach (GridViewRow GvSelection in GvPartyAdvance.Rows)
            {
                decimal gvTax = CommonCls.ConvertDecimalZero((GvSelection.FindControl("lblTaxRate") as Label).Text);
                if (gvTax == CommonCls.ConvertDecimalZero(drRemain["TaxRate"]))
                {
                    CheckBox cbSelection = (CheckBox)GvSelection.FindControl("cbPartyAdvance");
                    cbSelection.Enabled = true;
                    GvSelection.BackColor = Color.White;
                }
                //else
                //{
                //    GvSelection.BackColor = Color.FromArgb(235, 235, 228);
                //}
            }
        }
        #endregion
    }

    protected void GvPartyAdvance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                CheckBox cbPartyAdvance = (CheckBox)e.Row.FindControl("cbPartyAdvance");
                if (cbPartyAdvance.Enabled)
                    e.Row.BackColor = Color.White;
                else
                    e.Row.BackColor = Color.FromArgb(235, 235, 228);
            }
        }
    }

    void EnablePartyAdvanceCB()
    {
        if (VsdtAdvance.Rows.Count <= 0)
        {
            return;
        }
        DataView dvItemRate = new DataView(VsdtGvItemDetail);
        DataTable drTaxRates = dvItemRate.ToTable(true, "TaxRate");

        foreach (GridViewRow GvRow in GvPartyAdvance.Rows)
        {
            Label lblTaxRate = (Label)GvRow.FindControl("lblTaxRate");
            CheckBox cbPartyAdvance = (CheckBox)GvRow.FindControl("cbPartyAdvance");
            cbPartyAdvance.Enabled = false;

            GvRow.BackColor = Color.FromArgb(235, 235, 228);
            if (drTaxRates.Rows.Count <= 0)
            {
                GvRow.BackColor = Color.FromArgb(235, 235, 228);
                cbPartyAdvance.Enabled = false;
                cbPartyAdvance.Checked = false;
            }
            else
            {
                foreach (DataRow item in drTaxRates.Rows)
                {
                    if (CommonCls.ConvertDecimalZero(item["TaxRate"]) == CommonCls.ConvertDecimalZero(lblTaxRate.Text))
                    {
                        cbPartyAdvance.Enabled = true;
                        GvRow.BackColor = Color.White;
                    }
                }
            }
        }
    }

    DataTable CreateAdvanceDt()
    {
        try
        {

            if (VsdtAdvance.Rows.Count > 0)
            {
                foreach (GridViewRow gvRows in GvPartyAdvance.Rows)
                {
                    CheckBox cbPartyAdvance = (CheckBox)gvRows.FindControl("cbPartyAdvance");
                    Label lblVoucharNo = (Label)gvRows.FindControl("lblVoucharNo");
                    Label lblTaxRate = (Label)gvRows.FindControl("lblTaxRate");
                    Label lblAdjAdvAmount = (Label)gvRows.FindControl("lblAdjAdvAmount");
                    DataRow[] drCheckedSort = VsdtAdvance.Select(" VoucharNo=" + lblVoucharNo.Text + " And TaxRate=" + lblTaxRate.Text);
                    if (drCheckedSort.Count() > 0)
                    {
                        DataRow drChecked = drCheckedSort[0];
                        if (cbPartyAdvance.Checked)
                        {
                            drChecked["AdvAmount"] = CommonCls.ConvertDecimalZero(drChecked["AdvAmount"]) - CommonCls.ConvertDecimalZero(lblAdjAdvAmount.Text);
                            //drChecked["AdvAmount"] = lblAdjAdvAmount.Text;                        
                        }
                        else
                        {
                            VsdtAdvance.Rows.Remove(drChecked);
                        }
                    }
                }

                if (VsdtAdvance.Columns.Contains("AdjAdvAmount"))
                    VsdtAdvance.Columns.Remove("AdjAdvAmount");
                if (VsdtAdvance.Columns.Contains("CheckedInd"))
                    VsdtAdvance.Columns.Remove("CheckedInd");
                if (VsdtAdvance.Columns.Contains("AdjPriority"))
                    VsdtAdvance.Columns.Remove("AdjPriority");

                if (VsdtAdvance.Rows.Count <= 0)
                {
                    VsdtAdvance = new DataTable();
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
                }
            }
            else
            {
                VsdtAdvance = new DataTable();
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return VsdtAdvance;
    }

    void AdvanceAdjustment()
    {
        try
        {
            if (!pnlParentAdvance.Visible)
            {
                return;
            }
            //VsdtRemainAmt = new DataTable();
            if (VsdtRemainAmt == null)
            {
                VsdtRemainAmt = ContainRemain();
            }

            DataView dvItem = new DataView(VsdtGvItemDetail);
            DataTable dtFilterTaxRate = dvItem.ToTable(true, "TaxRate");

            foreach (DataRow drItems in VsdtGvItemDetail.Rows)
            {
                DataRow[] drGetTaxesRow = VsdtRemainAmt.Select("TaxRate=" + drItems["TaxRate"]);
                DataRow drRemain;
                if (drGetTaxesRow.Count() <= 0)
                {
                    drRemain = VsdtRemainAmt.NewRow();
                    drRemain["TaxRate"] = CommonCls.ConvertDecimalZero(drItems["TaxRate"]);
                    VsdtRemainAmt.Rows.Add(drRemain);
                }

                drRemain = VsdtRemainAmt.Select("TaxRate=" + drItems["TaxRate"])[0];
                decimal InvoiceAmount = CommonCls.ConvertDecimalZero(VsdtGvItemDetail.Compute("Sum(ItemAmount)", "TaxRate=" + drItems["TaxRate"].ToString()));
                drRemain["InvoiceAmt"] = InvoiceAmount;
                drRemain["RemainingAmt"] = InvoiceAmount;
            }

            DataView DvSortByPriority = VsdtAdvance.DefaultView;
            DvSortByPriority.Sort = "AdjPriority ASC";
            DataTable dtSortPriority = DvSortByPriority.ToTable();

            #region Adjust & Remain Amount Calculation
            foreach (DataRow dtRow in dtSortPriority.Rows)
            {
                foreach (GridViewRow GvRow in GvPartyAdvance.Rows)
                {
                    CheckBox cbPartyAdvance = (CheckBox)GvRow.FindControl("cbPartyAdvance");
                    Label lblVoucharNo = (Label)GvRow.FindControl("lblVoucharNo");
                    Label lblTaxRate = (Label)GvRow.FindControl("lblTaxRate");
                    Label lblAdjAdvAmount = (Label)GvRow.FindControl("lblAdjAdvAmount");
                    if (lblVoucharNo.Text == dtRow["VoucharNo"].ToString() && lblTaxRate.Text == dtRow["TaxRate"].ToString())
                    {
                        //DataRow drAdvance = VsdtAdvance.Select("VoucharNo=" + lblVoucharNo.Text + " And TaxRate=" + lblTaxRate.Text)[0];
                        DataRow drAdvance = dtSortPriority.Select("VoucharNo=" + lblVoucharNo.Text + " And TaxRate=" + lblTaxRate.Text)[0];
                        DataRow[] drRemainCount = VsdtRemainAmt.Select("TaxRate=" + lblTaxRate.Text);
                        decimal AdjAdvAmount, RemainingAmt, AdvAmount;
                        if (drRemainCount.Count() > 0)
                        {
                            DataRow drRemain = drRemainCount[0];
                            AdjAdvAmount = CommonCls.ConvertDecimalZero(dtRow["AdjAdvAmount"]);
                            RemainingAmt = CommonCls.ConvertDecimalZero(drRemain["RemainingAmt"]);
                            AdvAmount = CommonCls.ConvertDecimalZero(dtRow["AdvAmount"]);
                            if (cbPartyAdvance.Checked)
                            {
                                if (RemainingAmt > AdvAmount)
                                {
                                    RemainingAmt = RemainingAmt - AdvAmount;
                                    AdjAdvAmount = 0;
                                    //lblAdjAdvAmount.Text = AdjAdvAmount.ToString();
                                }
                                else
                                {
                                    AdjAdvAmount = AdvAmount - RemainingAmt;
                                    RemainingAmt = 0;
                                    //lblAdjAdvAmount.Text = AdjAdvAmount.ToString();
                                }
                            }
                            lblAdjAdvAmount.Text = AdjAdvAmount.ToString();
                            drAdvance["AdjAdvAmount"] = AdjAdvAmount;
                            drRemain["RemainingAmt"] = RemainingAmt;
                            drAdvance["AdvAmount"] = AdvAmount;
                        }
                    }
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
    protected void chkBankEntry_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBankEntry.Checked == true)
        {
            thVNo.Visible = true;
            tdVNo.Visible = true;
            txtVoucherNo.Focus();
        }
        else
        {
            txtVoucherNo.Text = "";
            thVNo.Visible = false;
            tdVNo.Visible = false;
        }
    }
}


