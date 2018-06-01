using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdSalesReturn : System.Web.UI.Page
{
    #region Declarations

    DataTable dtgrdview;
    UpdateSalesReturnModel ObjUpdSaleRModel;
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
    DataTable VsdtNarration
    {
        get { return (DataTable)ViewState["NarrationDesc"]; }
        set { ViewState["NarrationDesc"] = value; }
    }
    DataTable VsDtOrgInvoicNoAndDate
    {
        get { return (DataTable)ViewState["DtOrgInvoicNoAndDate"]; }
        set { ViewState["DtOrgInvoicNoAndDate"] = value; }
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
                ddlIncomeHead.Focus();
                //ddlPA.SelectedValue = "1";
                //SelectionPA();
                //CallReport("7845687", "2017/06/15"); //For Report Check.
                EnableOnIncomeHead();
                if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
                {
                    btnUpdate.Visible = true;
                    //  btnCancel.Visible = true;
                }
                if (Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || GlobalSession.IsAdmin == 1)
                {
                    btnUpdate.Visible = true;
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
        ObjUpdSaleRModel = new UpdateSalesReturnModel();
        ObjUpdSaleRModel.Ind = 11;
        ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
        ObjUpdSaleRModel.BrID = GlobalSession.BrID;
        ObjUpdSaleRModel.YrCD = GlobalSession.YrCD;
        ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        string uri = string.Format("UpdateSalesReturn/BindAll");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, ObjUpdSaleRModel);
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

            // For Narration Info Taken
            if (dtNarration.Rows.Count > 0)
            {
                txtNarration.DataSource = dtNarration;
                txtNarration.DataTextField = "NarrationDesc";
                txtNarration.DataBind();
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
                if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 1) //Serial No Auto Generate No.2
                {
                    txtinvoiceNo.Enabled = true;
                }
                else
                {
                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["InvoiceNo"]).ToString();
                    //txtinvoiceNo.Enabled = false;
                }

                switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                {
                    case 1: /// Manual Series
                        ddlInvoiceSeries.Visible = ddlInvoiceSeries.Enabled = false;
                        txtInvoiceSeries.Visible = true;
                        txtInvoiceSeries.Enabled = false;

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

                        ddlInvoiceSeries.Visible = true;
                        ddlInvoiceSeries.Enabled = false;
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

    protected void btnSearchVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSearchVoucher.Text))
            {
                ShowMessage("Enter Voucher No.", false);
                txtSearchVoucher.Focus();
                return;
            }

            ObjUpdSaleRModel = new UpdateSalesReturnModel();
            ObjUpdSaleRModel.Ind = 1;
            ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
            ObjUpdSaleRModel.BrID = GlobalSession.BrID;
            ObjUpdSaleRModel.YrCD = GlobalSession.YrCD;
            //ObjUpdSaleRModel.VchType = ViewState["VchType"];
            ObjUpdSaleRModel.DocNo = Convert.ToInt32(txtSearchVoucher.Text);
            string uri = string.Format("UpdateSalesReturn/LoadSRBasicDetails");

            DataSet dsSRBasicDetails = CommonCls.ApiPostDataSet(uri, ObjUpdSaleRModel);
            if (dsSRBasicDetails.Tables.Count > 0)
            {
                DataTable dtBasicDetails = dsSRBasicDetails.Tables[0];
                DataTable dtItemDetails = dsSRBasicDetails.Tables[1];
                DataTable dtOtherCharges = dsSRBasicDetails.Tables[2];
                VsDtOrgInvoicNoAndDate = dsSRBasicDetails.Tables[3];

                if (dtBasicDetails.Rows.Count > 0)
                {
                    ddlIncomeHead.SelectedValue = dtBasicDetails.Rows[0]["PurchaseSaleCode"].ToString();
                    ddlSalesto.SelectedValue = dtBasicDetails.Rows[0]["AccountCode"].ToString();
                    ddlSalesto_SelectedIndexChanged1(sender, e);
                    ddlGstinNo.SelectedValue = dtBasicDetails.Rows[0]["AccGst"].ToString();
                    ddlLocation.SelectedValue = dtBasicDetails.Rows[0]["WarehouseID"].ToString();
                    ddlTds.SelectedValue = dtBasicDetails.Rows[0]["TDSApplicable"].ToString();
                    ddlTCS.SelectedValue = dtBasicDetails.Rows[0]["TCSApplicable"].ToString();
                    ddlRCM.SelectedValue = dtBasicDetails.Rows[0]["RCMApplicable"].ToString();
                    if (GlobalSession.CCCode == 1)
                    {
                        ddlCostCenter.SelectedValue = CommonCls.ConvertIntZero(dtBasicDetails.Rows[0]["CCCode"]).ToString();
                    }


                    switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                    {
                        case 1: /// Manual Series
                            txtInvoiceSeries.Text = dtBasicDetails.Rows[0]["Series"].ToString();
                            break;

                        case 2: /// Available Series
                            if (ddlInvoiceSeries.Items.FindByValue(dtBasicDetails.Rows[0]["Series"].ToString()) != null)
                                ddlInvoiceSeries.SelectedValue = dtBasicDetails.Rows[0]["Series"].ToString();
                            break;

                        case 3: /// Default Series
                            if (ddlInvoiceSeries.Items.FindByValue(dtBasicDetails.Rows[0]["Series"].ToString()) != null)
                                ddlInvoiceSeries.SelectedValue = dtBasicDetails.Rows[0]["Series"].ToString();
                            break;
                    }
                    //txtInvoiceSeries.Text = dtBasicDetails.Rows[0]["InvoiceSeries"].ToString();
                    txtinvoiceNo.Text = dtBasicDetails.Rows[0]["InvoiceNo"].ToString();
                    txtInvoiceDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["InvoiceDate"]);
                    hfVoucherNo.Value = dtBasicDetails.Rows[0]["VoucharNo"].ToString();
                    hfVoucherDate.Value = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["VoucharDate"]);
                    txtorderNo.Text = dtBasicDetails.Rows[0]["OrderNo"].ToString();
                    txtOrderDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["OrderDate"]);

                    if (dtBasicDetails.Rows[0]["TransportID"].ToString() != "")
                    {
                        ddlTansportID.SelectedValue = dtBasicDetails.Rows[0]["TransportID"].ToString();
                        txtTransportDate.Text = CommonCls.ConvertDateDB(dtBasicDetails.Rows[0]["TransportDate"]);
                        txtVehicleNo.Text = dtBasicDetails.Rows[0]["VehicleNo"].ToString();
                        txtTransportName.Text = dtBasicDetails.Rows[0]["TransporterName"].ToString();

                        btnShowTransport_Click(sender, e);
                    }

                    txtGross.Text = dtBasicDetails.Rows[0]["GrossAmount"].ToString();
                    txtTaxable.Text = dtBasicDetails.Rows[0]["TaxAmount"].ToString();
                    txtNet.Text = dtBasicDetails.Rows[0]["NetAmount"].ToString();

                    if (VsdtNarration == null)
                    {
                        VsdtNarration = new DataTable();
                        VsdtNarration.Columns.Add("NarrationDesc", typeof(string));
                    }
                    VsdtNarration.Rows.Add(dtBasicDetails.Rows[0]["NarrationDesc"].ToString());
                    txtNarration.DataSource = VsdtNarration;
                    txtNarration.DataValueField = "NarrationDesc";
                    txtNarration.DataBind();
                    txtNarration.SelectedValue = dtBasicDetails.Rows[0]["NarrationDesc"].ToString();

                    hfPurchaseSalesRecordID.Value = dtBasicDetails.Rows[0]["PurchaseSaleRecordID"].ToString();
                }
                if (dtItemDetails.Rows.Count > 0)
                {
                    dtItemDetails.Columns.Add("PADesc", typeof(string));
                    dtItemDetails.Columns.Add("ISDDesc", typeof(string));

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
                if (dtOtherCharges.Rows.Count > 0)
                {
                    gvotherCharge.DataSource = VsdtSundri = dtOtherCharges;
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

                    btnShoOtherCharge_Click(sender, e);
                }
                if (dtBasicDetails.Rows.Count > 0 && dtItemDetails.Rows.Count > 0)
                {
                    txtSearchVoucher.Enabled = btnSearchVoucher.Enabled = false;
                    btnAdd.Enabled = btnUpdate.Enabled = true;
                    //txtVoucherDate.Focus();
                }
                else
                {
                    ShowMessage("Invalid Voucher No.", false);
                    txtInvoiceSeries.Focus();
                    return;
                }
            }
            else
            {
                ShowMessage("Invalid Voucher No.", false);
                txtInvoiceSeries.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
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
            }
        }
        else
        {
            txtinvoiceNo.Text = "";
        }
        ddlInvoiceSeries.Focus();
    }

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
        ////    txtInvoiceDate.Focus();     
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

            //txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = txtFree.Enabled = true;
            //ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
        }
        else
        {
            //txtSalesto.Visible = txtGstinNo.Visible = txtShippingAdd.Visible = false;
            //txtorderNo.Enabled = txtOrderDate.Enabled = ddlTds.Enabled = ddlTCS.Enabled = txtFree.Enabled = true;
            //ddlSalesto.Visible = ddlGstinNo.Visible = ddlShippingAdd.Visible = true;
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
            ObjUpdSaleRModel = new UpdateSalesReturnModel();
            ObjUpdSaleRModel.Ind = 1;
            ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
            ObjUpdSaleRModel.BrID = GlobalSession.BrID;
            ObjUpdSaleRModel.YrCD = GlobalSession.YrCD;
            ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjUpdSaleRModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);

            string uri = string.Format("UpdateSalesReturn/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, ObjUpdSaleRModel);
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
                //ddlItemName.Enabled = false;
                ddlGstinNo.Focus();
                return;
            }
        }
        //ddlItemName.Enabled = true;
        FillShippingAddress();
        Filllocation();
        ddlGstinNo.Focus();
    }
    void FillShippingAddress()
    {
        ObjUpdSaleRModel = new UpdateSalesReturnModel();
        ObjUpdSaleRModel.Ind = 4;
        ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
        ObjUpdSaleRModel.BrID = GlobalSession.BrID;
        ObjUpdSaleRModel.YrCD = GlobalSession.YrCD;
        ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjUpdSaleRModel.AccCode = Convert.ToInt32(ddlSalesto.SelectedValue);
        ObjUpdSaleRModel.GSTIN = ddlGstinNo != null ?
                 ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
                 ddlGstinNo.SelectedValue : "" : "";

        string uri = string.Format("UpdateSalesReturn/FillShippingAddress");
        DataTable dtShipping = CommonCls.ApiPostDataTable(uri, ObjUpdSaleRModel);
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
            ObjUpdSaleRModel = new UpdateSalesReturnModel();
            ObjUpdSaleRModel.Ind = 2;
            ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
            ObjUpdSaleRModel.BrID = GlobalSession.BrID;
            ObjUpdSaleRModel.YrCD = GlobalSession.YrCD;
            ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjUpdSaleRModel.GSTIN = "0";
            //ObjSaleModel.GSTIN = ddlGstinNo != null ?
            //    ddlGstinNo.Items.Count > 0 && !string.IsNullOrEmpty(ddlGstinNo.SelectedValue) ?
            //    ddlGstinNo.SelectedValue : "" : "",


            string uri = string.Format("UpdateSalesReturn/Filllocation");
            DataTable dtLocation = CommonCls.ApiPostDataTable(uri, ObjUpdSaleRModel);
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
                ObjUpdSaleRModel = new UpdateSalesReturnModel();
                ObjUpdSaleRModel.Ind = 15;
                string uri = string.Format("Master/Master");
                DataTable dtState = CommonCls.ApiPostDataTable(uri, ObjUpdSaleRModel);
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
            if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(hfOriginalQty.Value))
            {
                txtQty.Focus();
                ShowMessage("Item Quantity Cannot be greater than Original Item Quantity = " + hfOriginalQty.Value, false);
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



            BindGvItemDetail();
            CalculateTotalAmount();
            ClearItemDetailTable();
            SelectionPA();
            ddlItemName.Focus();
            btnAddItemDetail.Text = "Add";
            btnAddItemDetail.Enabled = false;

            foreach (GridViewRow grdRow in gvItemDetail.Rows)
            {
                Button btnEdit = (Button)grdRow.FindControl("btnEdit");
                btnEdit.Enabled = true;

                Button btnDelete = (Button)grdRow.FindControl("btnDelete");
                btnDelete.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void CalculateTotalAmount()
    {
        var cal = Calculation.CalculateTotalAmount(VsdtGvItemDetail, VsdtSundri);
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
        ddlDiscount.ClearSelection();
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
                TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);
            else
                TaxRate = CommonCls.ConvertDecimalZero(VsdtItems.Rows[0]["TaxRate"]);
            if (ddlPA.SelectedValue == "1")
                TaxRate = CommonCls.ConvertDecimalZero(txtTax.Text);

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


            DrGvItemDetail["DiscountValue"] = CommonCls.ConvertDecimalZero(txtDiscount.Text);
            if (ddlDiscount.SelectedValue == "%")
                DrGvItemDetail["DiscountType"] = "1";
            else
                DrGvItemDetail["DiscountType"] = "0";

            DrGvItemDetail["DiscountAmt"] = CommonCls.ConvertDecimalZero(hfDiscountAmount.Value);

            DrGvItemDetail["NetAmt"] = Convert.ToDecimal(txtItemTaxableAmt.Text);
            DrGvItemDetail["PADesc"] = ddlPA.SelectedItem.Text;
            DrGvItemDetail["TaxRate"] = TaxRate;

            DrGvItemDetail["IGSTTaxAmt"] = IgstAmt;
            DrGvItemDetail["SGSTTaxAmt"] = SgstAmt;
            DrGvItemDetail["CGSTTaxAmt"] = CgstAmt;
            DrGvItemDetail["CESSTaxAmt"] = CessAmt;

            DrGvItemDetail["ISDDesc"] = ddlIsd.SelectedItem.Text;

            DrGvItemDetail["ItemID"] = Convert.ToInt32(ddlItemName.SelectedValue);
            DrGvItemDetail["GoodsServiceInd"] = Convert.ToInt32(dtItems.Rows[0]["GoodsServiceIndication"]);
            DrGvItemDetail["ItemUnitID"] = Convert.ToInt32(ddlUnitName.SelectedValue);
            DrGvItemDetail["IGSTTax"] = IgstRat;
            DrGvItemDetail["SGSTTax"] = SgstRat;
            DrGvItemDetail["CGSTTax"] = CgstRat;
            DrGvItemDetail["CESSTax"] = CessRat;
            DrGvItemDetail["ItemRemark"] = txtItemRemark.Text;
            DrGvItemDetail["ISDApplicable"] = Convert.ToInt16(ddlIsd.SelectedValue);
            DrGvItemDetail["PA"] = Convert.ToInt16(ddlPA.SelectedValue);
            DrGvItemDetail["ItemMinorUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemMinorUnitID"].ToString());
            DrGvItemDetail["ItemMinorQty"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["ItemMinorUnitQty"].ToString());
            DrGvItemDetail["ExtraInd"] = 0;//CommonCls.ConvertIntZero(dtItems.Rows[0]["StockMaintainInd"].ToString());
            DrGvItemDetail["OrgQty"] = hfOriginalQty.Value;
            dtGvItemDetail.Rows.Add(DrGvItemDetail);
            gvItemDetail.DataSource = VsdtGvItemDetail = dtGvItemDetail;
            gvItemDetail.DataBind();
            //VsdtItems = null;
        }
    }
    void CalculateRate()
    {
        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        item.ItemFree = CommonCls.ConvertDecimalZero(txtFree.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtRate.Text);
        item.ItemDiscount = CommonCls.ConvertDecimalZero(txtDiscount.Text);

        string discountValue = "";
        if (ddlDiscount.SelectedValue == "%")
            discountValue = "1";
        else if (ddlDiscount.SelectedValue == "Rs.")
            discountValue = "0";

        item.DiscountInPerc = Convert.ToInt16(discountValue) == 1;

        StructItems GetItem = Calculation.CalculateRate(item);
        txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
        txtItemAmt.Text = GetItem.ItemAmount.ToString();
        hfDiscountAmount.Value = GetItem.DiscountValue.ToString();
    }

    protected void gvItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItems")
        {
            DataTable dtGvItemDetail = VsdtGvItemDetail;
            dtGvItemDetail.Rows[rowIndex].Delete();
            VsdtGvItemDetail = dtGvItemDetail;
            gvItemDetail.DataSource = dtGvItemDetail;
            gvItemDetail.DataBind();
            CalculateTotalAmount();
        }
        if (e.CommandName == "EditItems")
        {
            DataTable dtEditItemsDetails = VsdtGvItemDetail;
            DataRow drEditItemsDetails = dtEditItemsDetails.Rows[rowIndex];

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
            ddlPA.SelectedValue = drEditItemsDetails["PA"].ToString();

            txtTax.Text = drEditItemsDetails["TaxRate"].ToString();
            txtCGSTAmt.Text = drEditItemsDetails["CGSTTaxAmt"].ToString();
            txtSGSTAmt.Text = drEditItemsDetails["SGSTTaxAmt"].ToString();
            txtIGSTAmt.Text = drEditItemsDetails["IGSTTaxAmt"].ToString();
            txtCESSAmt.Text = drEditItemsDetails["CESSTaxAmt"].ToString();

            hfOriginalQty.Value = drEditItemsDetails["OrgQty"].ToString();

            txtMinorUnitQty.Enabled = false;

            btnAddItemDetail.Text = "Update";

            dtEditItemsDetails.Rows.RemoveAt(rowIndex);

            gvItemDetail.DataSource = VsdtGvItemDetail = dtEditItemsDetails;
            gvItemDetail.DataBind();

            CalculateTotalAmount();
            btnAddItemDetail.Enabled = true;

            foreach (GridViewRow grdRow in gvItemDetail.Rows)
            {
                Button btnEdit = (Button)grdRow.FindControl("btnEdit");
                btnEdit.Enabled = false;

                Button btnDelete = (Button)grdRow.FindControl("btnDelete");
                btnDelete.Enabled = false;
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
                if (lblDiscountType.Text == "%")
                    lblDiscountType.Text = "%";
                else if (lblDiscountType.Text == "Rs.")
                    lblDiscountType.Text = "Rs.";
                else if (lblDiscountType.Text == "0")
                    lblDiscountType.Text = "Rs.";
                else if (lblDiscountType.Text == "1")
                    lblDiscountType.Text = "%";
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

            ObjUpdSaleRModel = new UpdateSalesReturnModel();
            ObjUpdSaleRModel.Ind = 11;
            ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
            ObjUpdSaleRModel.BrID = GlobalSession.BrID;
            ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjUpdSaleRModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());

            ObjUpdSaleRModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjUpdSaleRModel.ItemID = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);

            if (ddlGstinNo.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
                    ObjUpdSaleRModel.GSTIN = ddlGstinNo.SelectedValue;
            }
            if (ddlIncomeHead.SelectedValue == CashSalesAcc)
                ObjUpdSaleRModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

            string uri = string.Format("UpdateSalesReturn/FillItemSellRate");
            DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjUpdSaleRModel);
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
            CalculateRate();
            TaxCal();

            if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(hfOriginalQty.Value))
            {
                txtQty.Focus();
                ShowMessage("Item Quantity Cannot be greater than Original Item Quantity = " + hfOriginalQty.Value, false);
                return;
            }
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
    protected void btnUpdate_Click(object sender, EventArgs e)
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

            ObjUpdSaleRModel = new UpdateSalesReturnModel();
            ObjUpdSaleRModel.Ind = 2;
            ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
            ObjUpdSaleRModel.BrID = GlobalSession.BrID;
            ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            ObjUpdSaleRModel.EntryType = 2;
            ObjUpdSaleRModel.YrCD = GlobalSession.YrCD;
            ObjUpdSaleRModel.PurchaseSalesRecordID = Convert.ToInt64(hfPurchaseSalesRecordID.Value);
            ObjUpdSaleRModel.User = GlobalSession.UserID;
            ObjUpdSaleRModel.IP = GlobalSession.IP;

            ObjUpdSaleRModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
            ObjUpdSaleRModel.PartyName = txtSalesto.Text.ToUpper();
            ObjUpdSaleRModel.PartyGSTIN = txtGstinNo.Text.ToUpper();

            ObjUpdSaleRModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            ObjUpdSaleRModel.PartyAddress = ddlIncomeHead.SelectedValue == CashSalesAcc ? txtShippingAdd.Text.ToUpper() :
                                        ddlShippingAdd.SelectedItem != null ? ddlShippingAdd.SelectedItem.Text.ToUpper() : "";

            ObjUpdSaleRModel.WareHouseID = Convert.ToInt32(ddlLocation.SelectedValue);
            ObjUpdSaleRModel.TransName = txtTransportName.Text;
            ObjUpdSaleRModel.PONo = txtorderNo.Text;
            ObjUpdSaleRModel.DocNo = Convert.ToInt32(txtSearchVoucher.Text);
            ObjUpdSaleRModel.DocDate = CommonCls.ConvertToDate(CommonCls.ConvertDateDB(VsDtOrgInvoicNoAndDate.Rows[0]["InvoiceDate"].ToString()));
            ObjUpdSaleRModel.OrgVoucherNo = Convert.ToInt32(VsDtOrgInvoicNoAndDate.Rows[0]["InvoiceNo"].ToString());
            ObjUpdSaleRModel.OrgVoucherDate = CommonCls.ConvertToDate(CommonCls.ConvertDateDB(VsDtOrgInvoicNoAndDate.Rows[0]["InvoiceDate"].ToString()));
            ObjUpdSaleRModel.InvoiceSeries = txtInvoiceSeries.Text;

            ObjUpdSaleRModel.InvoiceSeries = txtInvoiceSeries.Text;
            switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
            {
                case 1: /// Manual Series
                    ObjUpdSaleRModel.InvoiceSeries = txtInvoiceSeries.Text.ToUpper();
                    break;

                case 2: /// Available Series
                    ObjUpdSaleRModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;

                case 3: /// Default Series
                    ObjUpdSaleRModel.InvoiceSeries = ddlInvoiceSeries.SelectedItem.Text;
                    break;
            }

            ObjUpdSaleRModel.DtSales = DtSalesSchema();
            ObjUpdSaleRModel.DtItems = DtItemsSchema();
            ObjUpdSaleRModel.DtSundries = DtSundriesSchema();

            ObjUpdSaleRModel.DtSales = CreateSaleData();

            //if (VsdtGvFreeItem != null && VsdtGvFreeItem.Rows.Count > 0)
            //{
            //    foreach (DataRow item in VsdtGvFreeItem.Rows)
            //    {
            //        VsdtGvItemDetail.Rows.Add(item.ItemArray);
            //    }
            //}

            ObjUpdSaleRModel.DtItems = VsdtGvItemDetail;

            if (ObjUpdSaleRModel.DtItems != null)
            {
                if (ObjUpdSaleRModel.DtItems.Columns.Contains("OrgRate"))
                {
                    ObjUpdSaleRModel.DtItems.Columns.Remove("OrgRate");
                }
                if (ObjUpdSaleRModel.DtItems.Columns.Contains("OrgQty"))
                {
                    ObjUpdSaleRModel.DtItems.Columns.Remove("OrgQty");
                }
                if (ObjUpdSaleRModel.DtItems.Columns.Contains("CCCode"))
                {
                    ObjUpdSaleRModel.DtItems.Columns.Remove("CCCode");
                }
            }

            ObjUpdSaleRModel.DtSundries = VsdtSundri;

            if ((ObjUpdSaleRModel.DtSundries == null) || (ObjUpdSaleRModel.DtSundries.Rows.Count <= 0))
            {
                ObjUpdSaleRModel.DtSundries = DtSundriesSchema();
                DataRow drSaleSundri = ObjUpdSaleRModel.DtSundries.NewRow();
                drSaleSundri["SundriCode"] = 0;
                drSaleSundri["SundriHead"] = "0";
                drSaleSundri["SundriInd"] = "0";
                drSaleSundri["SundriAmt"] = 0;
                ObjUpdSaleRModel.DtSundries.Rows.Add(drSaleSundri);
            }

            string uri = string.Format("UpdateSalesReturn/UpdateSalesReturnVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, ObjUpdSaleRModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {

                    ClearAll();
                    ShowMessage("Record Update successfully", true);// for Invoice No. " + InvoiceNo, true);

                    //string LastVNO, LastVDate, InvoiceSeries = ""; //InvoiceNo, InvoiceDate, InvoiceName, 
                    //InvoiceNo = dtSave.Rows[0]["LastInvoiceNo"].ToString();
                    //InvoiceDate = Convert.ToDateTime(dtSave.Rows[0]["LastInvoiceDate"].ToString()).ToString("dd/MM/yyyy");
                    //InvoiceName = dtSave.Rows[0]["InvoiceName"].ToString();
                    //LastVNO = dtSave.Rows[0]["DocMaxNo"].ToString();
                    //LastVDate = Convert.ToDateTime(dtSave.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                    //if (!string.IsNullOrEmpty(ObjUpdSaleRModel.InvoiceSeries))
                    //    InvoiceSeries = ObjUpdSaleRModel.InvoiceSeries + "-";

                    ////lblInvoiceAndDate.Text = "Last Invoice No. & Date " + InvoiceSeries + InvoiceNo + " - " + InvoiceDate;
                    //lblInvoiceAndDate.Text = "Last Voucher No. & Date " + LastVNO + " - " + LastVDate;


                    //if (VsdtSeries.Rows.Count > 0)
                    //{
                    //    if (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SerailNoInd"]) == 2) //Serial No Auto Generate No.2
                    //    {
                    //        string LastInvoiceNo = "";
                    //        foreach (DataRow item in VsdtSeries.Rows)
                    //        {
                    //            if (CommonCls.ConvertIntZero(txtinvoiceNo.Text) == CommonCls.ConvertIntZero(item["InvoiceNo"]))
                    //            {
                    //                item["InvoiceNo"] = CommonCls.ConvertIntZero(InvoiceNo) + 1;
                    //                LastInvoiceNo = (CommonCls.ConvertIntZero(InvoiceNo) + 1).ToString();
                    //            }
                    //        }

                    //        switch (CommonCls.ConvertIntZero(VsdtSeries.Rows[0]["SeriesType"]))
                    //        {
                    //            case 1: /// Manual Series
                    //                txtinvoiceNo.Text = LastInvoiceNo;
                    //                break;

                    //            case 2: /// Available Series
                    //                ddlInvoiceSeries.DataSource = VsdtSeries;
                    //                ddlInvoiceSeries.DataTextField = "Series";
                    //                ddlInvoiceSeries.DataBind();
                    //                if (VsdtSeries.Rows.Count > 1)
                    //                    ddlInvoiceSeries.Items.Insert(0, new ListItem("-- Select --", "0"));
                    //                if (ddlInvoiceSeries.SelectedValue == "0")
                    //                {
                    //                    txtinvoiceNo.Text = "";
                    //                }
                    //                else
                    //                {
                    //                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                    //                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
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
                    //                    txtinvoiceNo.Text = "";
                    //                }
                    //                else
                    //                {
                    //                    DataRow dr1 = VsdtSeries.Select("Series='" + ddlInvoiceSeries.SelectedValue + "'")[0];
                    //                    txtinvoiceNo.Text = CommonCls.ConvertIntZero(dr1["InvoiceNo"]) == 0 ? "" : (dr1["InvoiceNo"]).ToString();
                    //                }
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        txtinvoiceNo.Text = "";
                    //    }
                    //}


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
        return true;
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

        drCreateSaleData["InvoiceNo"] = Convert.ToInt32(txtinvoiceNo.Text);
        drCreateSaleData["InvoiceDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
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
        //    txtinvoiceNo.Text = "";
        //}

        txtInvoiceDate.Text = txtorderNo.Text = txtOrderDate.Text = "";
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

        VsdtGvItemDetail = VsdtSundri = null;
        gvItemDetail.DataSource = gvotherCharge.DataSource = new DataTable();
        gvItemDetail.DataBind(); gvotherCharge.DataBind();

        ddlHeadName.ClearSelection();
        ddlAddLess.ClearSelection();
        txtOtherChrgAmount.Text = "0";

        txtGross.Text = txtTaxable.Text = txtNet.Text = txtAddLess.Text = "0";
        txtNarration.ClearSelection();
        lblMsg.Text = "";

        pnlTransport.Visible = CbTransDetail.Checked = false;
        CBTransDetailInit();


        ddlItemName.Enabled = false;

        txtSalesto.Text = txtShippingAdd.Text = txtGstinNo.Text = "";

        //ddlSalesto.Enabled = ddlGstinNo.Enabled = ddlIncomeHead.Enabled = true;
        //txtSalesto.Enabled = txtGstinNo.Enabled = true;

        txtSearchVoucher.Text = "";
        txtSearchVoucher.Enabled = btnSearchVoucher.Enabled = true;

        txtinvoiceNo.Focus();
        txtinvoiceNo.Text = "";
        ddlLocation.ClearSelection();
        ddlIncomeHead.ClearSelection();
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
        dtItems.Columns.Add("OrgQty", typeof(string));

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

    //    ObjUpdSaleRModel = new UpdateSalesReturnModel();
    //    ObjUpdSaleRModel.Ind = 11;
    //    ObjUpdSaleRModel.OrgID = GlobalSession.OrgID;
    //    ObjUpdSaleRModel.BrID = GlobalSession.BrID;
    //    ObjUpdSaleRModel.VchType = Convert.ToInt32(ViewState["VchType"]);
    //    ObjUpdSaleRModel.PartyCode = CommonCls.ConvertIntZero(ddlSalesto.SelectedValue.ToString());
    //    // ObjSaleModel.GSTIN= ddlGstinNo.SelectedItem.Text;
    //    ObjUpdSaleRModel.ByCashSale = ddlIncomeHead.SelectedValue == CashSalesAcc ? 1 : 0;
    //    ObjUpdSaleRModel.ItemID = CommonCls.ConvertIntZero(ddlFreeItemName.SelectedValue);

    //    if (ddlGstinNo.SelectedItem != null)
    //    {
    //        if (!string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
    //            ObjUpdSaleRModel.GSTIN = ddlGstinNo.SelectedValue;
    //    }
    //    if (ddlIncomeHead.SelectedValue == CashSalesAcc)
    //        ObjUpdSaleRModel.GSTIN = txtGstinNo.Text.ToUpper().ToString();

    //    string uri = string.Format("UpdateSalesReturn/FillItemSellRate");
    //    DataSet dsItems = CommonCls.ApiPostDataSet(uri, ObjUpdSaleRModel);
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