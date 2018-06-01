using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmStockTransfer : System.Web.UI.Page
{
    # region Declaration

    StockTransferModel objSTModel;

    DataTable VsDtWarehouseStateCodeTransferFrom
    {
        get { return (DataTable)ViewState["dtWarehouseStateCodeTransferFrom"]; }
        set { ViewState["dtWarehouseStateCodeTransferFrom"] = value; }
    }

    DataTable VsDtWarehouseStateCodeTransferTo
    {
        get { return (DataTable)ViewState["dtWarehouseStateCodeTransferTo"]; }
        set { ViewState["dtWarehouseStateCodeTransferTo"] = value; }
    }

    DataTable VsDtItemAndUnit
    {
        get { return (DataTable)ViewState["DtItemAndUnit"]; }
        set { ViewState["DtItemAndUnit"] = value; }
    }

    DataTable VsDtItemSellRate
    {
        get { return (DataTable)ViewState["DtItems"]; }
        set { ViewState["DtItems"] = value; }
    }

    DataTable VsdtItems
    {
        get { return (DataTable)ViewState["dtItems"]; }
        set { ViewState["dtItems"] = value; }
    }

    DataTable VsdtAddItemsDetails
    {
        get { return (DataTable)ViewState["dtAddItemsDetails"]; }
        set { ViewState["dtAddItemsDetails"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["VchType"] = 12;
            txtTransferDate.Text = CommonCls.ConvertDateDB(DateTime.Now);
            BindStockTransferDDL();
            txtTransferNo.Focus();
            //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
            //{
            //    btnSave.Visible = false;

            //}
            //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
            //{
            //    btnSave.Visible = true;
            //}

        }
        lblMsg.Text = lblMsg.CssClass = "";
    }

    void BindStockTransferDDL()
    {
        try
        {
            objSTModel = new StockTransferModel();
            objSTModel.Ind = 11;
            objSTModel.OrgID = GlobalSession.OrgID;
            objSTModel.BrID = GlobalSession.BrID;
            objSTModel.YrCD = GlobalSession.YrCD;
            objSTModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("StockTransfer/BindStockTransferDDL");
            DataSet dsBindSTDDL = CommonCls.ApiPostDataSet(uri, objSTModel);
            if (dsBindSTDDL.Tables.Count > 0)
            {
                if (dsBindSTDDL.Tables[0].Rows.Count <= 1)
                {
                    ShowMessageOnPopUp("Please Open Warehouse. Press Yes For Open New Warehouse.", false, "../UserUtility/Warehouse.aspx");
                    return;
                }
                VsDtWarehouseStateCodeTransferFrom = dsBindSTDDL.Tables[0];
                VsDtWarehouseStateCodeTransferTo = dsBindSTDDL.Tables[0];
                DataTable dtNarration = dsBindSTDDL.Tables[1];
                DataTable dtVoucherNoDate = dsBindSTDDL.Tables[2];
                DataTable dtLastTransferNo = dsBindSTDDL.Tables[3];

                DataTable dtCostCenter = dsBindSTDDL.Tables[4];


                // For TransferFrom
                if (VsDtWarehouseStateCodeTransferFrom.Rows.Count > 0)
                {
                    ddlTransferFrom.DataSource = VsDtWarehouseStateCodeTransferFrom;
                    ddlTransferFrom.DataTextField = "WareHouseAddress";
                    ddlTransferFrom.DataValueField = "WareHouseID";
                    ddlTransferFrom.DataBind();
                    if (VsDtWarehouseStateCodeTransferFrom.Rows.Count > 1)
                    {
                        ddlTransferFrom.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }

                // For TransferTo
                if (VsDtWarehouseStateCodeTransferTo.Rows.Count > 0)
                {
                    ddlTransferTo.DataSource = VsDtWarehouseStateCodeTransferTo;
                    ddlTransferTo.DataTextField = "WareHouseAddress";
                    ddlTransferTo.DataValueField = "WareHouseID";
                    ddlTransferTo.DataBind();
                    if (VsDtWarehouseStateCodeTransferTo.Rows.Count > 1)
                    {
                        ddlTransferTo.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }

                // For Narration
                //if (dtNarration.Rows.Count > 0)
                //{
                //    ddlNarration.DataSource = dtNarration;
                //    ddlNarration.DataTextField = "NarrationDesc";
                //    ddlNarration.DataBind();
                //}

                // For Last Voucher No. And Date Info Taken
                if (dtVoucherNoDate.Rows.Count > 0)
                {
                    if (dtVoucherNoDate.Rows[0]["LastNo"].ToString() != "0")
                    {
                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucherNoDate.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtVoucherNoDate.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                        long voucherNo = CommonCls.ConvertLongZero(dtVoucherNoDate.Rows[0]["LastNo"].ToString());
                    }
                }
                if (dtLastTransferNo.Rows.Count > 0)
                {
                    if (dtLastTransferNo.Rows[0]["InvoiceNo"].ToString() == "0")
                        txtTransferNo.Text = "1";
                    else
                        txtTransferNo.Text = Convert.ToString(Convert.ToInt32(dtLastTransferNo.Rows[0]["InvoiceNo"].ToString()) + 1);
                }


                // Cost Center List
                if (GlobalSession.CCCode == 1)
                {
                    trCCCode.Visible = true;
                    if (dtCostCenter.Rows.Count > 0)
                    {
                        ddlCCCodeFrom.DataSource = dtCostCenter;
                        ddlCCCodeFrom.DataTextField = "CostCentreName";
                        ddlCCCodeFrom.DataValueField = "CostCentreID";
                        ddlCCCodeFrom.DataBind();
                        ddlCCCodeFrom.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }


                    if (dtCostCenter.Rows.Count > 0)
                    {
                        ddlCCCodeTo.DataSource = dtCostCenter;
                        ddlCCCodeTo.DataTextField = "CostCentreName";
                        ddlCCCodeTo.DataValueField = "CostCentreID";
                        ddlCCCodeTo.DataBind();
                        ddlCCCodeTo.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlTransferFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTransferFrom.SelectedValue != "0")
        {
            if (ddlTransferFrom.SelectedValue != ddlTransferTo.SelectedValue)
            {
                DataView dvTransferFrom = new DataView(VsDtWarehouseStateCodeTransferFrom);
                dvTransferFrom.RowFilter = "WareHouseID=" + ddlTransferFrom.SelectedValue;
                if (dvTransferFrom.ToTable().Rows.Count > 0)
                {
                    lblTransferFromStateCode.Text = dvTransferFrom.ToTable().Rows[0]["StateID"].ToString();
                    smTransferFromStateCode.Visible = lblTransferFromStateCode.Visible = true;
                }

                objSTModel = new StockTransferModel();
                objSTModel.Ind = 12;
                objSTModel.OrgID = GlobalSession.OrgID;
                objSTModel.YrCD = GlobalSession.YrCD;
                objSTModel.WarehouseID = CommonCls.ConvertIntZero(ddlTransferFrom.SelectedValue);

                string uri = string.Format("StockTransfer/BindItemAndUnitDDL");
                DataSet dsItemAndUnit = CommonCls.ApiPostDataSet(uri, objSTModel);
                if (dsItemAndUnit.Tables.Count > 0)
                {
                    VsDtItemAndUnit = dsItemAndUnit.Tables[0];

                    if (VsDtItemAndUnit.Rows.Count > 0)
                    {
                        ddlItemName.DataSource = VsDtItemAndUnit;
                        ddlItemName.DataTextField = "ItemName";
                        ddlItemName.DataValueField = "ItemID";
                        ddlItemName.DataBind();
                        ddlItemName.Items.Insert(0, new ListItem("-- Select --", "0"));

                        ddlUnitName.DataSource = VsDtItemAndUnit;
                        ddlUnitName.DataTextField = "UnitName";
                        ddlUnitName.DataValueField = "ItemUnitID";
                        ddlUnitName.DataBind();
                        ddlUnitName.Items.Insert(0, new ListItem("-- Select --", "0"));


                        DataView dvItemAndUnit = new DataView(VsDtItemAndUnit);
                        dvItemAndUnit.ToTable(true, "ItemMinorUnitID", "ItemMinorUnitName");

                        ddlMinorUnitName.DataSource = dvItemAndUnit;
                        ddlMinorUnitName.DataTextField = "ItemMinorUnitName";
                        ddlMinorUnitName.DataValueField = "ItemMinorUnitID";
                        ddlMinorUnitName.DataBind();
                        ddlMinorUnitName.Items.Insert(0, new ListItem("-- Select --", "0"));

                        ddlItemName.Enabled = btnAddItemDetail.Enabled = true;
                    }
                    else
                    {
                        ddlItemName.DataSource = ddlUnitName.DataSource = ddlMinorUnitName.DataSource = new DataTable();
                        ddlItemName.DataBind(); ddlUnitName.DataBind(); ddlMinorUnitName.DataBind();
                        ddlItemName.Enabled = btnAddItemDetail.Enabled = false;
                    }
                }
                ddlTransferFrom.Focus();
            }
            else
            {
                ShowMessage("Transfer From (Warehouse) And Transfer To (Warehouse) Can Not Be Same.", false);
                ddlTransferFrom.ClearSelection();
                ddlTransferFrom.Focus();
                smTransferFromStateCode.Visible = lblTransferFromStateCode.Visible = false;
                ddlItemName.Enabled = false;
                ddlItemName.DataSource = ddlUnitName.DataSource = new DataTable();
                ddlItemName.DataBind(); ddlUnitName.DataBind();
                return;
            }
        }
        else
        {
            ddlItemName.Enabled = false;
            ddlItemName.DataSource = ddlUnitName.DataSource = new DataTable();
            ddlItemName.DataBind(); ddlUnitName.DataBind();
        }
    }

    protected void ddlTransferTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTransferTo.SelectedValue != ddlTransferFrom.SelectedValue)
        {
            DataView dvTransferTo = new DataView(VsDtWarehouseStateCodeTransferTo);
            dvTransferTo.RowFilter = "WareHouseID=" + ddlTransferTo.SelectedValue;
            if (dvTransferTo.ToTable().Rows.Count > 0)
            {
                lblTransferToStateCode.Text = dvTransferTo.ToTable().Rows[0]["StateID"].ToString();
                smTransferToStateCode.Visible = lblTransferToStateCode.Visible = true;
            }
            ddlTransferTo.Focus();
        }
        else
        {
            ShowMessage("Transfer From (Warehouse) And Transfer To (Warehouse) Can Not Be Same.", false);
            ddlTransferTo.ClearSelection();
            ddlTransferTo.Focus();
            smTransferToStateCode.Visible = lblTransferToStateCode.Visible = false;
            return;
        }
    }

    #region Add Item Details

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataView dvItemAndUnit = new DataView(VsDtItemAndUnit);
            dvItemAndUnit.ToTable(true, "ItemMinorUnitID", "ItemMinorUnitName");
            dvItemAndUnit.RowFilter = "ItemID=" + ddlItemName.SelectedValue;
            if (dvItemAndUnit.ToTable().Rows.Count > 0)
            {
                txtItemRate.Text = dvItemAndUnit.ToTable().Rows[0]["ItemOpeningRate"].ToString();
                ddlUnitName.SelectedValue = dvItemAndUnit.ToTable().Rows[0]["ItemUnitID"].ToString();
                if (ddlMinorUnitName.SelectedItem != null)
                {
                    if (CommonCls.ConvertIntZero(dvItemAndUnit.ToTable().Rows[0]["ItemMinorUnitID"]) > 0)
                    {
                        txtMinorQty.Enabled = true;
                        ddlMinorUnitName.SelectedValue = dvItemAndUnit.ToTable().Rows[0]["ItemMinorUnitID"].ToString();
                    }
                    else
                    {
                        txtMinorQty.Text = "";
                        txtMinorQty.Enabled = false;
                        ddlMinorUnitName.ClearSelection();
                    }
                }
            }
            txtQty.Focus();
        }

        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataRow[] dRow = VsDtItemAndUnit.Select("ItemID=" + CommonCls.ConvertIntZero(ddlItemName.SelectedValue));
            if (dRow.Count() > 0)
            {
                if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(dRow[0]["OpQty"].ToString()))
                {
                    txtQty.Focus();
                    ShowMessage("Item Quantity Cannot be greater than Original Item Quantity = " + dRow[0]["OpQty"].ToString(), false);
                    return;
                }
            }

            CalculateRate();

            txtQty.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage("Select Item First!", false);
        }
    }

    void CalculateRate()
    {
        StructItems item = new StructItems();
        item.ItemQty = CommonCls.ConvertDecimalZero(txtQty.Text);
        item.ItemRate = CommonCls.ConvertDecimalZero(txtItemRate.Text);
        StructItems GetItem = Calculation.CalculateRate(item);
        txtItemTaxableAmt.Text = GetItem.ItemTaxable.ToString();
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
            {
                ddlItemName.Focus();
                ShowMessage("Select Item Name.", false);
                txtItemRate.Text = "0";
                return;
            }
            CalculateRate();

            txtItemRate.Focus();

        }
        catch (Exception ex)
        {
            ShowMessage("Select Item First!", false);
        }
    }

    protected void btnAddItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationBtnAddItemDetail())
            {
                return;
            }
            BindItemDetailGrid();
            ClearAfterBtnAddItemDetail();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidationBtnAddItemDetail()
    {
        if (ddlItemName.SelectedItem == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
        {
            ddlItemName.Focus();
            ShowMessage("Select Item Name.", false);
            return false;
        }

        if (ddlItemName == null || CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
        {
            ddlItemName.Focus();
            ShowMessage("Item Value Not Available", false);
            return false;
        }

        if (CommonCls.ConvertDecimalZero(txtQty.Text) == 0)
        {
            txtQty.Focus();
            ShowMessage("Enter Item Quantity", false);
            return false;
        }
        DataRow[] dRow = VsDtItemAndUnit.Select("ItemID=" + CommonCls.ConvertIntZero(ddlItemName.SelectedValue));
        if (dRow.Count() > 0)
        {
            if (CommonCls.ConvertDecimalZero(txtQty.Text) > CommonCls.ConvertDecimalZero(dRow[0]["OpQty"].ToString()))
            {
                txtQty.Focus();
                ShowMessage("Item Quantity Cannot be greater than Original Item Quantity = " + dRow[0]["OpQty"].ToString(), false);
                return false;
            }
        }
        if (CommonCls.ConvertIntZero(ddlUnitName.SelectedValue) == 0)
        {
            ddlUnitName.Focus();
            ShowMessage("Select Item Unit ", false);
            return false;
        }
        //if (CommonCls.ConvertDecimalZero(txtItemRate.Text) == 0)
        //{
        //    txtItemRate.Focus();
        //    ShowMessage("Enter Item Rate", false);
        //    return false;
        //}
        return true;
    }

    void BindItemDetailGrid()
    {
        DataTable dtAddItemDetail = new DataTable();
        if (VsdtAddItemsDetails == null)
        {
            dtAddItemDetail = DtItemDetailSchema();
        }
        else
        {
            dtAddItemDetail = VsdtAddItemsDetails;
        }
        DataRow DrAddItemDetail = dtAddItemDetail.NewRow();
        DrAddItemDetail["ItemID"] = CommonCls.ConvertIntZero(ddlItemName.SelectedValue);
        DrAddItemDetail["ItemName"] = ddlItemName.SelectedItem.Text;
        DrAddItemDetail["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
        DrAddItemDetail["ItemUnitID"] = CommonCls.ConvertIntZero(ddlUnitName.SelectedValue);
        DrAddItemDetail["ItemUnit"] = CommonCls.ConvertIntZero(ddlUnitName.SelectedValue) == 0 ? "" : ddlUnitName.SelectedItem.Text;
        DrAddItemDetail["ItemMinorQty"] = string.IsNullOrEmpty(txtMinorQty.Text) ? 0 : CommonCls.ConvertDecimalZero(txtMinorQty.Text);
        DrAddItemDetail["ItemMinorUnitID"] = ddlMinorUnitName.SelectedItem == null ? 0 : ddlMinorUnitName.SelectedValue == "0" ? 0 : CommonCls.ConvertDecimalZero(ddlMinorUnitName.SelectedValue);
        DrAddItemDetail["ItemMinorUnit"] = ddlMinorUnitName.SelectedItem == null ? "" : ddlMinorUnitName.SelectedValue == "0" ? "" : ddlMinorUnitName.SelectedItem.Text;
        DrAddItemDetail["ItemRate"] = CommonCls.ConvertDecimalZero(txtItemRate.Text);
        DrAddItemDetail["NetAmt"] = CommonCls.ConvertDecimalZero(txtItemTaxableAmt.Text);
        DrAddItemDetail["ItemRemark"] = txtItemRemark.Text;
        dtAddItemDetail.Rows.Add(DrAddItemDetail);
        grdItemDetails.DataSource = VsdtAddItemsDetails = dtAddItemDetail;
        grdItemDetails.DataBind();
    }

    void ClearAfterBtnAddItemDetail()
    {
        ddlItemName.ClearSelection();
        ddlUnitName.ClearSelection();
        ddlMinorUnitName.ClearSelection();
        txtQty.Text = txtMinorQty.Text = txtItemRate.Text = txtItemTaxableAmt.Text = txtItemRemark.Text = "";
        txtMinorQty.Enabled = ddlMinorUnitName.Enabled = false;
        ddlItemName.Focus();
    }

    public static CalculateAll CalculateTotalAmount(DataTable dtItems)
    {
        CalculateAll cal = new CalculateAll();
        if (dtItems != null)
        {
            DataTable dtGrdItems = (DataTable)(dtItems);

            foreach (DataRow item in dtGrdItems.Rows)
            {
                cal.IGST += 0;
                cal.SGST += 0;
                cal.CGST += 0;
                cal.CESS += 0;
                cal.ItemAmount += 0;
                cal.ItemTaxable += CommonCls.ConvertDecimalZero(item["NetAmt"].ToString());
                cal.ItemDiscount += 0;
            }
        }

        cal.TotalGross = (cal.ItemTaxable);
        cal.TotalTaxable = (cal.IGST + cal.SGST + cal.CGST + cal.CESS);
        cal.TotalAllNet = (cal.TotalGross + cal.TotalTaxable + cal.TotalSundriAddLess);

        return cal;
    }

    protected void grdItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveRow")
        {
            DataTable dtGvItemDetail = VsdtAddItemsDetails;
            dtGvItemDetail.Rows[rowIndex].Delete();
            VsdtAddItemsDetails = dtGvItemDetail;
            grdItemDetails.DataSource = dtGvItemDetail;
            grdItemDetails.DataBind();
        }
    }

    protected void grdItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    DataTable DtItemDetailSchema()
    {
        DataTable dtItems = new DataTable();
        dtItems.Columns.Add("ItemID", typeof(int));
        dtItems.Columns.Add("ItemName", typeof(string));
        dtItems.Columns.Add("ItemQty", typeof(decimal));
        dtItems.Columns.Add("ItemUnitID", typeof(int));
        dtItems.Columns.Add("ItemUnit", typeof(string));
        dtItems.Columns.Add("ItemMinorQty", typeof(decimal));
        dtItems.Columns.Add("ItemMinorUnitID", typeof(int));
        dtItems.Columns.Add("ItemMinorUnit", typeof(string));
        dtItems.Columns.Add("ItemRate", typeof(decimal));
        dtItems.Columns.Add("NetAmt", typeof(decimal));
        dtItems.Columns.Add("ItemRemark", typeof(string));

        return dtItems;
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationAfterSave())
            {
                return;
            }

            objSTModel = new StockTransferModel();
            objSTModel.Ind = 1;
            objSTModel.OrgID = GlobalSession.OrgID;
            objSTModel.BrID = GlobalSession.BrID;
            objSTModel.YrCD = GlobalSession.YrCD;
            objSTModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            objSTModel.TransferNo = txtTransferNo.Text;
            objSTModel.TransferDate = CommonCls.ConvertToDate(txtTransferDate.Text);

            objSTModel.TransferFromWarehouseID = CommonCls.ConvertIntZero(ddlTransferFrom.SelectedValue);
            objSTModel.TransferToWarehouseID = CommonCls.ConvertIntZero(ddlTransferTo.SelectedValue);

            objSTModel.CCCodeTransferFrom = CommonCls.ConvertIntZero(ddlCCCodeFrom.SelectedValue);
            objSTModel.CCCodeTransferTo = CommonCls.ConvertIntZero(ddlCCCodeTo.SelectedValue);


            objSTModel.Narration = txtNarration.Text;
            objSTModel.UserID = GlobalSession.UserID;
            objSTModel.IP = GlobalSession.IP;
            objSTModel.DtItemDetail = VsdtAddItemsDetails;

            string uri = string.Format("StockTransfer/SaveStockTransfer");
            DataTable dtSaveStockTransfer = CommonCls.ApiPostDataTable(uri, objSTModel);
            if (dtSaveStockTransfer.Rows.Count > 0)
            {
                if (dtSaveStockTransfer.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveStockTransfer.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSaveStockTransfer.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                    ShowMessage("Record Save successfully with voucher no. " + VoucherNo, true);
                    txtTransferNo.Focus();
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date " + VoucherNo + " - " + VoucherDate;
                    txtTransferNo.Text = Convert.ToString(Convert.ToInt32(txtTransferNo.Text) + 1);
                }
                else if (dtSaveStockTransfer.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("Duplicate Transfer No.", false);
                    txtTransferNo.Focus();
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidationAfterSave()
    {
        if (string.IsNullOrEmpty(txtTransferNo.Text))
        {
            ShowMessage("Enter Transfer No.", false);
            txtTransferNo.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtTransferDate.Text))
        {
            ShowMessage("Enter Transfer Date.", false);
            txtTransferDate.Focus();
            return false;
        }
        if (CommonCls.ConvertIntZero(ddlTransferFrom.SelectedValue) == 0)
        {
            ShowMessage("Select Transfer From.", false);
            ddlTransferFrom.Focus();
            return false;
        }
        if (CommonCls.ConvertIntZero(ddlTransferTo.SelectedValue) == 0)
        {
            ShowMessage("Select Transfer To.", false);
            ddlTransferTo.Focus();
            return false;
        }


        if (GlobalSession.CCCode == 1)
        {
            if (CommonCls.ConvertIntZero(ddlCCCodeFrom.SelectedValue) != 0)
            {
                if (CommonCls.ConvertIntZero(ddlCCCodeTo.SelectedValue) == 0)
                {
                    ShowMessage("Select Cost Centre Transfer To.", false);
                    ddlCCCodeTo.Focus();
                    return false;

                }
            }
            if (CommonCls.ConvertIntZero(ddlCCCodeTo.SelectedValue) != 0)
            {
                if (CommonCls.ConvertIntZero(ddlCCCodeFrom.SelectedValue) == 0)
                {
                    ShowMessage("Select Cost Centre Transfer From.", false);
                    ddlCCCodeFrom.Focus();
                    return false;

                }

            }
            if (CommonCls.ConvertIntZero(ddlCCCodeFrom.SelectedValue) != 0 && CommonCls.ConvertIntZero(ddlCCCodeTo.SelectedValue) != 0)
            {
                if (CommonCls.ConvertIntZero(ddlCCCodeFrom.SelectedValue) == CommonCls.ConvertIntZero(ddlCCCodeTo.SelectedValue))
                {
                    ShowMessage("Cost Centre Transfer From And Cost Centre Transfer To Can Not Be Same.", false);
                    ddlCCCodeFrom.Focus();
                    return false;
                }
            }
        }



        if (VsdtAddItemsDetails == null || VsdtAddItemsDetails.Rows.Count <= 0)
        {
            ShowMessage("Item Can Not Be Null.", false);
            ddlItemName.Focus();
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
        lblTransferFromStateCode.Text = lblTransferToStateCode.Text = txtItemRate.Text =
            txtItemTaxableAmt.Text = txtQty.Text = txtItemRemark.Text = "";
        ddlTransferFrom.ClearSelection(); ddlTransferTo.ClearSelection();
        VsdtAddItemsDetails = null;
        ddlItemName.DataSource = ddlUnitName.DataSource = grdItemDetails.DataSource = new DataTable();
        ddlItemName.DataBind(); ddlUnitName.DataBind(); grdItemDetails.DataBind();
        txtNarration.Text = "";
        ddlItemName.Enabled = btnAddItemDetail.Enabled = false;

        smTransferFromStateCode.Visible = lblTransferFromStateCode.Visible = smTransferToStateCode.Visible =
            lblTransferToStateCode.Visible = false;
        ddlCCCodeFrom.ClearSelection();
        ddlCCCodeTo.ClearSelection();

    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }
}