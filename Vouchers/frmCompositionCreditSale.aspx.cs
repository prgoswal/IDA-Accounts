using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmCompositionCreditSale : System.Web.UI.Page
{
    #region Declarations

    DataTable dtgrdview;
    CompositionSalesModel ObjCompositionSalesModel;
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
                ViewState["VchType"] = 6;
                ClearAll();
                BindAll();
                ddlIncomeHead.Focus();
                BindCancelReason();

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
                //ddlPA.SelectedValue = "1";
                //SelectionPA();
                //CallReport("7845687", "2017/06/15"); //For Report Check.
                EnableOnIncomeHead();
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error: Internal Server Error!", false);
        }
    }
    private void BindCancelReason()
    {
        try
        {
            ObjCompositionSalesModel = new CompositionSalesModel();
            ObjCompositionSalesModel.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, ObjCompositionSalesModel);
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


    void EnableOnIncomeHead()
    {
        if (ddlIncomeHead.SelectedValue != CashSalesAcc)
        {
            //txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = true;
            //ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = false;
            //txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = txtFree.Enabled = false;
            tblShippingDetail.Visible = true;

            txtorderNo.Enabled = txtOrderDate.Enabled = txtFree.Enabled = true;//ddlTds.Enabled = ddlTCS.Enabled =
            ddlSalesto.Visible = ddlShippingAdd.Visible = true;//ddlGstinNo.Visible =
        }
        else
        {
            txtSalesto.Visible = txtShippingAdd.Visible = false;//txtGstinNo.Visible = 
            txtorderNo.Enabled = txtOrderDate.Enabled = txtFree.Enabled = true;//ddlTds.Enabled = ddlTCS.Enabled =
            ddlSalesto.Visible = ddlShippingAdd.Visible = true;//ddlGstinNo.Visible =
        }
    }

    protected void txtSalesto_TextChanged(object sender, EventArgs e)
    {
        ddlItemName.Enabled = true;
        //txtGstinNo.Focus();
    }

    string LastInvoiceNo;
    void BindAll()
    {
        ObjCompositionSalesModel = new CompositionSalesModel();
        ObjCompositionSalesModel.Ind = 11;
        ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
        ObjCompositionSalesModel.BrID = GlobalSession.BrID;
        ObjCompositionSalesModel.YrCD = GlobalSession.YrCD;
        ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("CompositionSalesVouchar/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, ObjCompositionSalesModel);
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
                //if (VsdtSalesTo.Rows.Count == 1)
                //{
                //    ddlSalesto.SelectedValue = VsdtSalesTo.Rows[0]["AccCode"].ToString();
                //}
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
                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = false;
                        txtInvoiceSeries.Visible = txtInvoiceSeries.Enabled = true;

                        break;

                    case 2: /// Available Series

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

                        break;

                    case 3: /// Default Series

                        ddlInvoiceSeries.DataSource = VsdtSeries;
                        ddlInvoiceSeries.DataTextField = "Series";
                        ddlInvoiceSeries.DataBind();
                        ddlInvoiceSeries.Visible = true;
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

    void ClearAll()
    {
        txtDiscount.Enabled = true;
        chkDiscount.Checked = false;

        /// Clear Advance Items
        VsdtRemainAmt = VsdtAdvance = null;
        GvPartyAdvance.DataSource = null;
        GvPartyAdvance.DataBind();
        pnlPartyAdvance.Visible = false;
        /// ---------------------

        divFree.Visible = false;
        gvFreeItem.DataSource = VsdtGvFreeItem = null;
        gvFreeItem.DataBind();

        ddlIncomeHead.ClearSelection();
        ddlSalesto.ClearSelection();

        //ddlGstinNo.ClearSelection();
        ddlShippingAdd.ClearSelection();
        ddlLocation.ClearSelection();
        //ddlGstinNo.DataSource = ddlShippingAdd.DataSource = new DataTable();
        //ddlGstinNo.DataBind(); ddlShippingAdd.DataBind();

        //if (hfSaleInvoiceManually.Value != "1")
        //{
        //    txtinvoiceNo.Text = "";
        //}

        txtInvoiceDate.Text = txtorderNo.Text = txtOrderDate.Text = "";
        // ddlTds.ClearSelection();
        // ddlTCS.ClearSelection();
        //ddlRCM.ClearSelection();

        txtPartyBank.Text = txtPartyIFSC.Text = "";
        ddlItemName.ClearSelection();
        ddlUnitName.ClearSelection();
        //  ddlPA.ClearSelection();
        // ddlIsd.ClearSelection();

        txtFree.Text = "0";
        txtQty.Text = txtRate.Text = txtItemAmt.Text = txtDiscount.Text = txtItemTaxableAmt.Text = txtDiscount.Text=txtItemAmount.Text= "0";
        //  txtTax.Text = txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text = "0";
        // txtItemRemark.Text = "";

        VsdtGvItemDetail = VsdtSundri = null;
        gvItemDetail.DataSource = gvotherCharge.DataSource = new DataTable();
        gvItemDetail.DataBind(); gvotherCharge.DataBind();

        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";

        txtGross.Text = txtNet.Text = txtAddLess.Text = "0";//txtTaxable.Text =
        txtNarration.ClearSelection();
        lblMsg.Text = "";

        CbTransDetail.Checked = false;
        CBTransDetailInit();


        ddlItemName.Enabled = false;

        txtSalesto.Text = txtShippingAdd.Text = "";// txtGstinNo.Text = "";

        ddlSalesto.Enabled = ddlIncomeHead.Enabled = true;// ddlGstinNo.Enabled =
        txtSalesto.Enabled = true;// txtGstinNo.Enabled = true;

        txtinvoiceNo.Focus();
    }

    protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtGvItemDetail = VsdtGvItemDetail;
            dtGvItemDetail.Rows[rowIndex].Delete();
            VsdtGvItemDetail = dtGvItemDetail;
            gvItemDetail.DataSource = dtGvItemDetail;
            gvItemDetail.DataBind();
            CalculateTotalAmount();


            if (dtGvItemDetail.Rows.Count == 0)
            {
                chkDiscount.Checked = false;
                chkDiscount_CheckedChanged(sender, e);
            }
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
    protected void gvItemDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblDiscountType = (Label)e.Row.FindControl("lblDiscountType");
                ddlDiscount.SelectedValue = lblDiscountType.Text;
                lblDiscountType.Text = ddlDiscount.SelectedItem.Text;
            }
        }
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

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
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
            }
        }
        else
        {
            txtinvoiceNo.Text = "";
        }
        ddlInvoiceSeries.Focus();
    }
    protected void ddlIncomeHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIncomeHead.SelectedValue == CashSalesAcc)
        {
            txtSalesto.Visible = txtShippingAdd.Visible = true;//txtGstinNo.Visible =
            ddlSalesto.Visible = false; ddlShippingAdd.Visible = false;//ddlGstinNo.Visible =
            txtorderNo.Enabled = txtOrderDate.Enabled = txtFree.Enabled = false;//ddlTds.Enabled = ddlTCS.Enabled =
            tblShippingDetail.Visible = true;
        }
        else
        {
            txtorderNo.Enabled = txtOrderDate.Enabled = txtFree.Enabled = true;//ddlTds.Enabled = ddlTCS.Enabled =
            txtSalesto.Visible = txtShippingAdd.Visible = false;//txtGstinNo.Visible =
            ddlSalesto.Visible = ddlShippingAdd.Visible = true;//ddlGstinNo.Visible =
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
        ////    txtInvoiceDate.Focus();     
        ddlIncomeHead.Focus();
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
            ObjCompositionSalesModel = new CompositionSalesModel();
            ObjCompositionSalesModel.Ind = 1;
            ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
            ObjCompositionSalesModel.BrID = GlobalSession.BrID;
            ObjCompositionSalesModel.YrCD = GlobalSession.YrCD;
            ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjCompositionSalesModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);
            ObjCompositionSalesModel.AdvRecPayID = 13;

            //string uri = string.Format("CompositionSalesVouchar/FillGistnNo");
            string uri = string.Format("CompositionSalesVouchar/FillGistnNoWithDetail");
            DataSet dsGSTINWithDetail = CommonCls.ApiPostDataSet(uri, ObjCompositionSalesModel);
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
            ddlShippingAdd.Focus();
            ddlItemName.Enabled = true;

            DataTable dtSalesTo = VsdtSalesTo; // (DataTable)ViewState["dtSalesTo"];
            int TDS = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["TDSApplicable"].ToString());
            int TCS = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["ISDApplicable"].ToString());
            int RCM = Convert.ToInt32(dtSalesTo.Rows[ddlSalesto.SelectedIndex]["RCMApplicable"].ToString());
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

    void FillShippingAddress()
    {
        ObjCompositionSalesModel = new CompositionSalesModel();
        ObjCompositionSalesModel.Ind = 4;
        ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
        ObjCompositionSalesModel.BrID = GlobalSession.BrID;
        ObjCompositionSalesModel.YrCD = GlobalSession.YrCD;
        ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjCompositionSalesModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);

        ObjCompositionSalesModel.GSTIN = "";
        //ObjCompositionSalesModel.GSTIN = ddlGstinNo != null ?
        //         ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
        //         ddlGstinNo.SelectedValue : "" : "";

        string uri = string.Format("CompositionSalesVouchar/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, ObjCompositionSalesModel);
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

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)// Fill Record Item List
    {
        try
        {
            txtQty.Text = "";
            txtItemTaxableAmt.Text = txtItemAmt.Text = "0";//txtDiscount.Text =
            VsDtItemSellRate = VsdtItems = null;

            ObjCompositionSalesModel = new CompositionSalesModel();
            ObjCompositionSalesModel.Ind = 11;
            ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
            ObjCompositionSalesModel.BrID = GlobalSession.BrID;
            ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjCompositionSalesModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());

            ObjCompositionSalesModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjCompositionSalesModel.ItemID = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);

            //if (ddlGstinNo.SelectedItem != null)
            //{
            //    if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
            //        ObjSaleModel.GSTIN = ddlGstinNo.SelectedValue;
            //}
            //if (ddlIncomeHead.SelectedValue == CashSalesAcc)
            //    ObjSaleModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

            string uri = string.Format("CompositionSalesVouchar/FillItemSellRate");
            DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjCompositionSalesModel);
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
                    // FillTaxRate(VsdtItems.Rows[0]);

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

                ddlSalesto.Enabled = ddlIncomeHead.Enabled = false;//ddlGstinNo.Enabled = 
                txtSalesto.Enabled = false;// txtGstinNo.Enabled = false;
            }
            txtQty.Focus();
        }

        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    decimal CgstAmt, SgstAmt, IgstAmt, CessAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;
    void TaxCal()
    {
        decimal MaxAmt = Convert.ToDecimal(txtItemTaxableAmt.Text);
        // decimal TaxBy;

        DataTable dtItems = new DataTable();

        if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
            dtItems = VsDtItemSellRate;
        else
            dtItems = VsdtItems;

        //if (ddlPA.SelectedValue == "1")
        //    TaxBy = CommonCls.ConvertDecimalZero(txtTax.Text);
        //else
        //    TaxBy = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());

        StructItems item = new StructItems();
        item.ItemTaxable = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);
        //item.ItemRate = TaxBy;

        item.ItemCGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"]);
        item.ItemSGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"]);
        item.ItemIGSTRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"]);
        item.ItemCESSRate = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"]);

        StructItems GetItem = Calculation.TaxCal(item);
        //txtIGSTAmt.Text = (IgstAmt = GetItem.ItemIGSTAmt).ToString();
        //txtCGSTAmt.Text = (CgstAmt = GetItem.ItemCGSTAmt).ToString();
        //txtSGSTAmt.Text = (SgstAmt = GetItem.ItemSGSTAmt).ToString();
        //txtCESSAmt.Text = (CessAmt = GetItem.ItemCESSAmt).ToString();

        CgstRat = GetItem.ItemCGSTRate;
        SgstRat = GetItem.ItemSGSTRate;
        IgstRat = GetItem.ItemIGSTRate;
        CessRat = GetItem.ItemCESSRate;
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

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //TaxCal();
            CalculateRate();
            TaxCal();
            if (txtMinorUnitQty.Enabled)
                txtMinorUnitQty.Focus();
            else
                txtFree.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage("Select Item First!", false);
            //ShowMessage(ex.Message, false);
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


        StructItems GetItem = CalculateCompositionRate(item);
        //StructItems GetItem = Calculation.CalculateRate(item);
        txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
        txtItemAmt.Text = GetItem.ItemAmount.ToString();
        hfDiscountAmount.Value = GetItem.DiscountValue.ToString();
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
            if (VsdtItems == null)
            {
                ShowMessage("Select Item First.", false);
                ddlItemName.Focus();
                //ddlPA.SelectedValue = "0";
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
            //  ddlPA.SelectedValue = "0";
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
            //ddlPA.SelectedValue = "0";
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
    protected void btnAddItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation
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
            #endregion

            BindGvItemDetail();
            CalculateTotalAmount();
            ClearItemDetailTable();
            AdvanceAdjustment();
            EnablePartyAdvanceCB();
            ddlItemName.Focus();
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

    void ClearItemDetailTable()
    {
        VsdtItems = null;
        ddlItemName.ClearSelection();
        ddlMinorUnit.ClearSelection();
        ddlUnitName.ClearSelection();
        //ddlIsd.ClearSelection();
      //  txtDiscount.Enabled = false;
        txtQty.Text = txtFree.Text = txtItemAmt.Text = "0"; //txtDiscount.Text = "0";
        txtRate.Text = txtItemTaxableAmt.Text = txtMinorUnitQty.Text = ""; //txtTax.Text = "";
        // txtCGSTAmt.Text = txtSGSTAmt.Text = txtIGSTAmt.Text = txtCESSAmt.Text = "";
        txtItemTaxableAmt.Text = "";//txtItemRemark.Text = "";

        //ddlPA.SelectedValue = "0";
        txtDiscount.Focus();
        // SelectionPA();
        ddlItemName.Focus();
    }

    void CalculateTotalAmount()
    {
        var cal = Calculation.CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);
        txtAddLess.Text = cal.TotalSundriAddLess.ToString();
        txtGross.Text = cal.TotalGross.ToString();
        //txtTaxable.Text = cal.TotalTaxable.ToString();
        txtNet.Text = cal.TotalAllNet.ToString();
        txtItemAmount.Text = cal.ItemAmount.ToString();
        txtDiscountAmount.Text = cal.ItemDiscount.ToString();
        #region Old
        //Decimal Igst = 0, Sgst = 0, Cgst = 0, Cess = 0, Taxable = 0;

        //if (VsdtGvItemDetail != null)
        //{
        //    DataTable dtGrdItems = VsdtGvItemDetail;

        //    foreach (DataRow item in dtGrdItems.Rows)
        //    {
        //        Igst += Convert.ToDecimal(item["IGSTTaxAmt"]);
        //        Sgst += Convert.ToDecimal(item["SGSTTaxAmt"]);
        //        Cgst += Convert.ToDecimal(item["CGSTTaxAmt"]);
        //        Cess += Convert.ToDecimal(item["CESSTaxAmt"]);
        //        Taxable += Convert.ToDecimal(item["NetAmt"]);
        //    }

        //    txtTotalIGST.Text = Igst.ToString();
        //    txtTotalSGST.Text = Sgst.ToString();
        //    txtTotalCGST.Text = Cgst.ToString();
        //    txtTotalCESS.Text = Cess.ToString();
        //    //txtTaxable.Text = (Igst + Sgst + Cgst + Cess).ToString();
        //    //txtNet.Text = (Igst + Sgst + Cgst + Cess + Taxable).ToString();
        //}

        //decimal SundriAmt = 0;
        //if (VsdtSundri != null)
        //{
        //    DataTable dtSundri = VsdtSundri;

        //    foreach (DataRow item in dtSundri.Rows)
        //    {
        //        if (item["SundriInd"].ToString() == "Add") //For Sundri Amount Add
        //            SundriAmt += Convert.ToDecimal(item["SundriAmt"]);
        //        else if (item["SundriInd"].ToString() == "Less") //For Sundri Amount Less
        //            SundriAmt -= Convert.ToDecimal(item["SundriAmt"]);
        //    }
        //    txtTotalSundriAmt.Text = SundriAmt.ToString();
        //    txtAddLess.Text = SundriAmt.ToString();
        //}

        //txtGross.Text = txtTotalTaxable.Text = Taxable.ToString();
        //txtTaxable.Text = (Igst + Sgst + Cgst + Cess).ToString();
        //txtNet.Text = (Igst + Sgst + Cgst + Cess + Taxable + SundriAmt).ToString();
        #endregion
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
            decimal TaxRate = 0;
            if (Convert.ToInt16(VsdtItems.Rows[0]["TaxCalcType"]) == 1)
                TaxRate = CommonCls.ConvertDecimalZero(0);
            else
                TaxRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["TaxRate"]);
            //if (ddlPA.SelectedValue == "1")
            //    TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);

            DataTable dtItems = VsdtItems;

            DataRow DrGvItemDetail = dtGvItemDetail.NewRow();
            TaxCal();
            DrGvItemDetail["ItemName"] = ddlItemName.SelectedItem.Text;
            DrGvItemDetail["HSNSACCode"] = dtItems.Rows[0]["HSNSACCode"];
            DrGvItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            DrGvItemDetail["FreeQty"] = CommonCls.ConvertDecimalZero(txtFree.Text);
            DrGvItemDetail["ItemUnit"] = ddlUnitName.SelectedItem.Text;
            DrGvItemDetail["ItemRate"] = Convert.ToDecimal(txtRate.Text);
            DrGvItemDetail["ItemAmount"] = Convert.ToDecimal(txtItemAmt.Text);

            DrGvItemDetail["ItemMinorUnit"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue) == 0 ? "" : ddlMinorUnit.SelectedItem.Text; //DrGvItemDetail["ItemSecondaryUnit"]
            DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text);
            DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
            //DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemMinorUnitID"].ToString());
            //DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["ItemMinorUnitQty"].ToString());


            DrGvItemDetail["DiscountValue"] = CommonCls.ConvertDecimalZero(txtDiscount.Text);
            DrGvItemDetail["DiscountType"] = ddlDiscount.SelectedValue;
            DrGvItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(hfDiscountAmount.Value);

            DrGvItemDetail["NetAmt"] = Convert.ToDecimal(txtItemTaxableAmt.Text);
            DrGvItemDetail["PADesc"] = "";
            DrGvItemDetail["TaxRate"] = TaxRate;

            DrGvItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGvItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGvItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGvItemDetail["CESSTaxAmt"] = CessAmt;

            DrGvItemDetail["ISDDesc"] = "";

            DrGvItemDetail["ItemID"] = Convert.ToInt32(ddlItemName.SelectedValue);
            DrGvItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGvItemDetail["ItemUnitID"] = Convert.ToInt32(ddlUnitName.SelectedValue);
            DrGvItemDetail["IGSTTax"] = IgstRat;
            DrGvItemDetail["SGSTTax"] = SgstRat;
            DrGvItemDetail["CGSTTax"] = CgstRat;
            DrGvItemDetail["CESSTax"] = CessRat;
            DrGvItemDetail["ItemRemark"] = "";
            DrGvItemDetail["ISDApplicable"] = Convert.ToInt16(0);
            DrGvItemDetail["PA"] = Convert.ToInt16(0);

            DrGvItemDetail["ExtraInd"] = 0;//CommonCls.ConvertIntZero(dtItems.Rows[0]["StockMaintainInd"].ToString());
            dtGvItemDetail.Rows.Add(DrGvItemDetail);
            gvItemDetail.DataSource = VsdtGvItemDetail = dtGvItemDetail;
            gvItemDetail.DataBind();
            //VsdtItems = null;
        }
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

        ObjCompositionSalesModel = new CompositionSalesModel();
        ObjCompositionSalesModel.Ind = 11;
        ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
        ObjCompositionSalesModel.BrID = GlobalSession.BrID;
        ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjCompositionSalesModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());
        // ObjSaleModel.GSTIN= ddlGstinNo.SelectedItem.Text;
        ObjCompositionSalesModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
        ObjCompositionSalesModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);

        //if (ddlGstinNo.SelectedItem != null)
        //{
        //    if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
        //        ObjSaleModel.GSTIN = ddlGstinNo.SelectedValue;
        //}
        //if (ddlIncomeHead.SelectedValue == CashSalesAcc)
        //    ObjSaleModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

        string uri = string.Format("CompositionSalesVouchar/FillItemSellRate");
        DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjCompositionSalesModel);
        if (dsItems != null && dsItems.Tables.Count > 0)
        {
            if (dsItems.Tables[0].Rows.Count > 0)
            {
                VsdtItems = dsItems.Tables[0];
                ddlFreeUnit.SelectedValue = VsdtItems.Rows[0]["ItemUnitID"].ToString();
            }
        }
        txtFreeQty.Focus();
    }


    protected void btnShoOtherCharge_Click(object sender, EventArgs e)
    {
        if (pnlOtherCharge.Visible)
            pnlOtherCharge.Visible = false;
        else
            pnlOtherCharge.Visible = true;

        btnShoOtherCharge.Focus();

    }
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

            ObjCompositionSalesModel = new CompositionSalesModel();
            ObjCompositionSalesModel.Ind = 1;
            ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
            ObjCompositionSalesModel.BrID = GlobalSession.BrID;
            ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            ObjCompositionSalesModel.EntryType = 1;
            ObjCompositionSalesModel.YrCD = GlobalSession.YrCD;
            ObjCompositionSalesModel.User = GlobalSession.UserID;
            ObjCompositionSalesModel.IP = GlobalSession.IP;

            ObjCompositionSalesModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjCompositionSalesModel.PartyName = txtSalesto.Text.ToUpper();
            // ObjCompositionSalesModel.PartyGSTIN = txtGstinNo.Text.ToUpper();

            ObjCompositionSalesModel.PartyAddress = ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
                                        ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            ObjCompositionSalesModel.WareHouseID = Convert.ToInt32(ddlLocation.SelectedValue);
            ObjCompositionSalesModel.TransName = txtTransportName.Text;
            ObjCompositionSalesModel.PONo = txtorderNo.Text;

            //BankDetail

            ObjCompositionSalesModel.PartyBank = txtPartyBank.Text;
            ObjCompositionSalesModel.PartyIFSC = txtPartyIFSC.Text;
            ObjCompositionSalesModel.DtAdjAdvance = CreateAdvanceDt();

            //ObjSaleModel.InvoiceSeries = txtInvoiceSeries.Text;
            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    ObjCompositionSalesModel.InvoiceSeries = txtInvoiceSeries.Text.ToUpper();
                    break;

                case 2: /// Available Series
                    ObjCompositionSalesModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    ObjCompositionSalesModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;
            }


            ObjCompositionSalesModel.DtSales = DtSalesSchema();
            ObjCompositionSalesModel.DtItems = DtItemsSchema();
            ObjCompositionSalesModel.DtSundries = DtSundriesSchema();

            ObjCompositionSalesModel.DtSales = CreateSaleData();
            if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            {
                foreach (DataRow item in VsdtGvFreeItem.Rows)
                {
                    VsdtGvItemDetail.Rows.Add(item.ItemArray);
                }
            }

            ObjCompositionSalesModel.DtItems = VsdtGvItemDetail;
            ObjCompositionSalesModel.DtSundries = VsdtSundri;

            if ((ObjCompositionSalesModel.DtSundries == null) || (ObjCompositionSalesModel.DtSundries.Rows.Count <= 0))
            {
                ObjCompositionSalesModel.DtSundries = DtSundriesSchema();
                DataRow drSaleSundri = ObjCompositionSalesModel.DtSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                ObjCompositionSalesModel.DtSundries.Rows.Add(drSaleSundri);
            }

            string uri = string.Format("CompositionSalesVouchar/SaveSalesVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjCompositionSalesModel);
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
                    if (!string.IsNullOrEmpty(ObjCompositionSalesModel.InvoiceSeries))
                        InvoiceSeries = ObjCompositionSalesModel.InvoiceSeries + "-";

                    lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;


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


                    //if (hfSaleInvoiceManually.Value == "1")
                    //{
                    //    txtinvoiceNo.Text = (Convert.ToInt64(dtSave.Rows[0]["LastInvoiceNo"].ToString()) + 1).ToString();
                    //}

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

        //if (string.IsNullOrEmpty(txtinvoiceNo.Text)) // Invoice Number Shouldn't be Null
        //{
        //    txtinvoiceNo.Focus();

        //    ShowMessage("Enter Invoice No.", false);

        //    return false;
        //}

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
        //if (string.IsNullOrEmpty(txtTaxable.Text) || Convert.ToDecimal(txtTaxable.Text) < 0)
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
        return true;
    }

    DataTable CreateSaleData()
    {
        DataTable dtCreateSaleData = new DataTable();

        dtCreateSaleData = DtSalesSchema(); //new DataTable();
        DataRow drCreateSaleData = dtCreateSaleData.NewRow();

        //drCreateSaleData["AccCode"] = ddlIncomeHead.SelectedValue == CashSalesAcc ? 919000 : Convert.ToInt32(ddlSalesto.SelectedValue);

        if (ddlSalesto.SelectedValue != "")
        {

            drCreateSaleData["AccCode"] = ddlIncomeHead.SelectedValue == CashSalesAcc ? 919000 : Convert.ToInt32(ddlSalesto.SelectedValue);
        }
        else
        {
            drCreateSaleData["AccCode"] = 0;
        }

        drCreateSaleData["AccGst"] = "";
        //drCreateSaleData["AccGst"] = ddlGstinNo.SelectedItem != null ? !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ? ddlGstinNo.SelectedValue : "" : "";
        drCreateSaleData["SalePurchaseCode"] = Convert.ToInt32(ddlIncomeHead.SelectedValue);
        if (ddlShippingAdd.SelectedItem != null && CommonCls.ConvertIntZero(ddlShippingAdd.SelectedValue) != 0)
        {
            drCreateSaleData["AccPOSID"] = Convert.ToInt32(ddlShippingAdd.SelectedValue);
        }
        drCreateSaleData["WarehouseID"] = ddlLocation.SelectedItem != null ? Convert.ToInt32(ddlLocation.SelectedValue) : 0;
        drCreateSaleData["OrderNo"] = 0;//!string.IsNullOrEmpty(txtorderNo.Text) ? Convert.ToInt32(txtorderNo.Text) : 0;
        drCreateSaleData["OrderDate"] = !string.IsNullOrEmpty(txtOrderDate.Text) ? CommonCls.ConvertToDate(txtOrderDate.Text) : "";

        drCreateSaleData["InvoiceNo"] = Convert.ToInt32(txtinvoiceNo.Text);
        drCreateSaleData["InvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        drCreateSaleData["TDSApplicable"] = Convert.ToInt32(0);
        drCreateSaleData["TCSApplicable"] = Convert.ToInt32(0);
        drCreateSaleData["RCMApplicable"] = Convert.ToInt32(0);
        drCreateSaleData["GrossAmt"] = Convert.ToDecimal(txtGross.Text);
        drCreateSaleData["TaxAmt"] = Convert.ToDecimal(0);
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
    protected void btnShoBankDetail_Click(object sender, EventArgs e)
    {
        if (pnlBankDetail.Visible)
            pnlBankDetail.Visible = false;
        else
            pnlBankDetail.Visible = true;

        txtPartyBank.Focus();

    }
    protected void btnCancelInvoice_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        if (ddlInvoiceSeries.SelectedValue != "0" && txtInvoiceDate.Text != "")
        {
            pnlConfirmInvoice.Visible = true;
            pnlConfirmInvoice.Focus();
        }
        else
        {
            ShowMessage("Select Invoice Series and Invoice Date.", false);
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

            ObjCompositionSalesModel = new CompositionSalesModel();
            ObjCompositionSalesModel.Ind = 2;
            ObjCompositionSalesModel.OrgID = GlobalSession.OrgID;
            ObjCompositionSalesModel.BrID = GlobalSession.BrID;
            ObjCompositionSalesModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            ObjCompositionSalesModel.EntryType = 1;
            ObjCompositionSalesModel.YrCD = GlobalSession.YrCD;
            ObjCompositionSalesModel.User = GlobalSession.UserID;
            ObjCompositionSalesModel.IP = GlobalSession.IP;
            ObjCompositionSalesModel.CancelReason = ddlCancelReason.SelectedItem.Text;

            ObjCompositionSalesModel.ByCashSale = 0;
            ObjCompositionSalesModel.PartyName = "";

            ObjCompositionSalesModel.PartyAddress = "";

            ObjCompositionSalesModel.WareHouseID = 0;
            ObjCompositionSalesModel.TransName = "";
            ObjCompositionSalesModel.PONo = "";

            //BankDetail

            ObjCompositionSalesModel.PartyBank = "";
            ObjCompositionSalesModel.PartyIFSC = "";

            ObjCompositionSalesModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
            ObjCompositionSalesModel.DtSales = DtSalesSchema();
            ObjCompositionSalesModel.DtItems = DtItemsSchema();


            ObjCompositionSalesModel.DtSales = CreateSaleDataCancel();

            DataRow drItems = ObjCompositionSalesModel.DtItems.NewRow();
            drItems["ItemName"] = "";
            drItems["ItemName"] = "";
            drItems["ItemUnit"] = "";
            drItems["ItemMinorUnit"] = "";
            drItems["PADesc"] = "";
            drItems["ISDDesc"] = "";
            drItems["ItemID"] = 0;
            drItems["HSNSACCode"] = "";
            drItems["GoodsServiceInd"] = 0;
            drItems["ItemQty"] = 0.00;
            drItems["FreeQty"] = 0.00;
            drItems["ItemUnitID"] = 0;
            drItems["ItemMinorUnitID"] = 0;
            drItems["ItemMinorQty"] = 0.00;
            drItems["ItemRate"] = 0.00;
            drItems["ItemAmount"] = 0.00;
            drItems["DiscountValue"] = 0.00;
            drItems["DiscountType"] = 0;
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
            drItems["ItemRemark"] = "";
            drItems["ExtraInd"] = 0;
            ObjCompositionSalesModel.DtItems.Rows.Add(drItems);

            string uri = string.Format("CompositionSalesVouchar/CancelSalesVoucherNo");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjCompositionSalesModel);
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

                    ShowMessage("This Invoice No. Is Cancel. " + InvoiceNo, true);
                    if (!string.IsNullOrEmpty(ObjCompositionSalesModel.InvoiceSeries))
                        InvoiceSeries = ObjCompositionSalesModel.InvoiceSeries + "-";
                    pnlConfirmInvoice.Visible = false;
                    txtInvoiceDate.Text = "";

                    lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;


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


                    //if (hfSaleInvoiceManually.Value == "1")
                    //{
                    //    txtinvoiceNo.Text = (Convert.ToInt64(dtSave.Rows[0]["LastInvoiceNo"].ToString()) + 1).ToString();
                    //}

                    //CallReport(InvoiceNo, CommonCls.ConvertToDate(InvoiceDate), InvoiceName, InvoiceSeries, LastVNO);
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Duplicate Invoice No.", false);
                    txtinvoiceNo.Focus();
                }
            }
            else
            {
                ShowMessage("Invoice Number is not Cancel Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    private DataTable CreateSaleDataCancel()
    {
        DataTable dtCreateSaleData = new DataTable();
        dtCreateSaleData = DtSalesSchema(); //new DataTable();
        DataRow drCreateSaleData = dtCreateSaleData.NewRow();
        drCreateSaleData["AccCode"] = 0;
        drCreateSaleData["AccGst"] = "";
        drCreateSaleData["SalePurchaseCode"] = 0;
        drCreateSaleData["AccPOSID"] = 0;
        drCreateSaleData["WarehouseID"] = 0;
        drCreateSaleData["OrderNo"] = 0;//!string.IsNullOrEmpty(txtorderNo.Text) ? Convert.ToInt32(txtorderNo.Text) : 0;
        drCreateSaleData["OrderDate"] = "";

        drCreateSaleData["InvoiceNo"] = Convert.ToInt32(txtinvoiceNo.Text);
        drCreateSaleData["InvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        drCreateSaleData["TDSApplicable"] = 0;
        drCreateSaleData["TCSApplicable"] = 0;
        drCreateSaleData["RCMApplicable"] = 0;
        drCreateSaleData["GrossAmt"] = 0;
        drCreateSaleData["TaxAmt"] = 0;
        drCreateSaleData["NetAmt"] = 0;
        drCreateSaleData["RoundOffAmt"] = 0;

        drCreateSaleData["TransportID"] = 0;
        drCreateSaleData["VehicleNo"] = "";
        drCreateSaleData["WayBillNo"] = 0;
        drCreateSaleData["TransportDate"] = "";// + " " + txtTransportDate.Text.Substring(11, 8) : "";

        drCreateSaleData["DocDesc"] = txtNarration.Text;
        //drCreateSaleData["UserID"] = GlobalSession.UserID;
        //drCreateSaleData["IP"] = CommonCls.GetIP();
        dtCreateSaleData.Rows.Add(drCreateSaleData);
        return dtCreateSaleData;

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        txtCancelReason.Text = "";
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();

        chkDiscount_CheckedChanged(sender, e);
    }
}