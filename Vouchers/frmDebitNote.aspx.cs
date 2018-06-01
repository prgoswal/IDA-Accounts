using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frnDebitNote : System.Web.UI.Page
{
    DebitNoteModel ObjDNModel;
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
        if (!IsPostBack)
        {
            ViewState["VchType"] = 9;
            ClearAll();
            AllBinding();

            txtVoucherDate.Focus();
            txtDrAmount.Enabled = true;

            if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
            {
                btnSubmit.Visible = false;

            }
            if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
            {
                btnSubmit.Visible = true;
            }

        }

        lblMsg.CssClass = "";
        lblMsg.Text = "";
    }

    /// <summary>
    /// All Data Bindings On Load
    /// </summary>
    void AllBinding()
    {
        ObjDNModel = new DebitNoteModel();
        ObjDNModel.Ind = 11;
        ObjDNModel.OrgID = GlobalSession.OrgID;
        ObjDNModel.BrID = GlobalSession.BrID;
        ObjDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjDNModel.YrCD = GlobalSession.YrCD;

        string uri = string.Format("DebitNoteVoucher/BindAllDebitNoteDDL");

        DataSet dsAllBind = CommonCls.ApiPostDataSet(uri, ObjDNModel);
        if (dsAllBind.Tables.Count > 0)
        {
            //-----------------------------------------TableName------------------------------------------
            DataTable dtAccounthead = dsAllBind.Tables["AccountHead"];
            DataTable dtNarration = dsAllBind.Tables["Narration"];
            DataTable dtItemunit = dsAllBind.Tables["ItemUnit"];
            DataTable dtIssueReason = dsAllBind.Tables["IssueReason"];
            DataTable dtLastVoucherNo = dsAllBind.Tables["LastVoucherNo"];
            DataTable dtCostCenter = dsAllBind.Tables[5];

            //---------------------------------------------Account Head Binding------------------------------------ 

            ddlAccountHead.DataSource = dtAccounthead;
            ddlAccountHead.DataTextField = "AccName";
            ddlAccountHead.DataValueField = "AccCode";
            ddlAccountHead.DataBind();
            if (dtAccounthead.Rows.Count > 1)
                ddlAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));

            //-----------------------------------Issue Reason--------------------------------------------------------------------- 

            ddlresion.DataSource = dtIssueReason;
            ddlresion.DataTextField = "ReasonDesc";
            ddlresion.DataValueField = "ReasonId";
            ddlresion.DataBind();
            if (dtIssueReason.Rows.Count > 1)
                ddlresion.Items.Insert(0, new ListItem("-- Select --", "0"));

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
        ObjDNModel = new DebitNoteModel();
        //ObjDNModel.Ind = ddlAccountHead.SelectedValue == "943500" ? 11 : 10; // For Party Account Accourding To accountHead.
        ObjDNModel.Ind = ddlAgainst.SelectedValue == "2" ? 11 : 10; // For Party Account Accourding To Sales/Purchase Debit Note.

        ObjDNModel.OrgID = GlobalSession.OrgID;
        ObjDNModel.BrID = GlobalSession.BrID;
        ObjDNModel.YrCD = GlobalSession.YrCD;

        string uri = string.Format("DebitNoteVoucher/LoadPartyAccount");
        DataTable dtPartyAccount = CommonCls.ApiPostDataTable(uri, ObjDNModel);
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
    protected void ddlPartyAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ObjDNModel = new DebitNoteModel();
            ObjDNModel.Ind = 1;
            ObjDNModel.OrgID = GlobalSession.OrgID;
            ObjDNModel.BrID = GlobalSession.BrID;
            ObjDNModel.YrCD = GlobalSession.YrCD;
            ObjDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjDNModel.AccCode = Convert.ToInt32(ddlPartyAccount.SelectedValue);

            string uri = string.Format("DebitNoteVoucher/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, ObjDNModel);
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
    /// <summary>
    /// For CheckBox Change Show GRID & Search Button
    /// And SEARCH Data.
    /// </summary>
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

    /// <summary>
    /// Invoice Search Operations
    /// <functions>
    /// ValidOnSerachInvoice()
    /// </functions>
    /// </summary>
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidOnSerachInvoice())
            {
                return;
            }
            BindGvItemDetail();

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidOnSerachInvoice()
    {
        //if (ddlAccountHead == null || ddlAccountHead.SelectedValue == null || ddlAccountHead.SelectedValue == "0")
        //{
        //    ShowMessage("Please Select Party Account.", false);
        //    return false;
        //}
        if (CommonCls.ConvertIntZero(ddlAgainst.SelectedValue) == 0)
        {
            ddlAgainst.Focus();
            ShowMessage("Please Select Debit Note Against.", false);
            return false;
        }

        if (CommonCls.ConvertIntZero(ddlPartyAccount.SelectedValue) == 0)
        {
            ddlPartyAccount.Focus();
            ShowMessage("Please Select Party Account.", false);
            return false;
        }

        if (string.IsNullOrEmpty(txtInVoiceNo.Text))
        {
            txtInVoiceNo.Focus();
            ShowMessage("Please Enter Invoice No.", false);
            return false;
        }

        if (string.IsNullOrEmpty(txtInvoiceDate.Text))
        {
            txtInvoiceDate.Focus();
            ShowMessage("Please Enter Invoice Date.", false);
            return false;
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            ShowMessage("Invoice Date Not More Than Todays Date!", false);
            txtInvoiceDate.Focus();
            return false;
        }

        bool InvoiceDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, DateTime.Now.AddMonths(-18).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
        if (!InvoiceDate)
        {
            ShowMessage("Invoice Date Not Older Then 18 Month", false);
            return false;
        }
        return true;

    }

    void BindGvItemDetail()
    {
        ObjDNModel = new DebitNoteModel();
        ObjDNModel.Ind = 2;

        if (ddlAgainst.SelectedValue == "1")
        {
            ObjDNModel.ByPurchaseSale = 2;
            ObjDNModel.DocNo = Convert.ToInt32(txtInVoiceNo.Text);
            ObjDNModel.DocDate = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        }
        else if (ddlAgainst.SelectedValue == "2")
        {
            ObjDNModel.ByPurchaseSale = 1;
            ObjDNModel.DocNo = Convert.ToInt32(txtInVoiceNo.Text);
            ObjDNModel.DocDate = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        }

        ObjDNModel.OrgID = GlobalSession.OrgID;
        ObjDNModel.BrID = GlobalSession.BrID;
        ObjDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        ObjDNModel.YrCD = GlobalSession.YrCD;

        ObjDNModel.PartyCode = Convert.ToInt32(ddlPartyAccount.SelectedValue);


        string uri = string.Format("DebitNoteVoucher/ItemsBinding");
        DataTable dtItemsBind = CommonCls.ApiPostDataTable(uri, ObjDNModel);
        if (dtItemsBind.Rows.Count > 0)
        {
            gvItemDetail.DataSource = VSItemsData = dtItemsBind;
            gvItemDetail.DataBind();
            if (GlobalSession.CCCode == 1)
            {
                ddlCostCenter.SelectedValue = CommonCls.ConvertIntZero(dtItemsBind.Rows[0]["CCCode"]).ToString();
                ddlCostCenter.Enabled = false;
            }

            txtDrAmount.Enabled = false;
            divItemData.Visible = true;
            ddlAccountHead.Visible = false;

            CBGSTImapactTaken.Enabled = false;
            txtInVoiceNo.Enabled = false;
            txtInvoiceDate.Enabled = false;
            btnSearchInvoice.Enabled = false;
            ddlAgainst.Enabled = false;
            ddlPartyAccount.Enabled = false;

        }
        else
        {
            if (gvFinalItemDetail.Rows.Count >= 1)
            {
                divItemData.Visible = true;
                gvItemDetail.DataSource = new DataTable();
                gvItemDetail.DataBind();
            }
            else
            {
                gvItemDetail.DataSource = VSItemsData = new DataTable();
                gvItemDetail.DataBind();
                divItemData.Visible = false;
                ShowMessage("Items Not Available For This Invoice No", false);
            }
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
            txtHSN.Text = drItems["HSNSACNo"].ToString();
            txtQty.Text = drItems["ItemQty"].ToString();

            hfItemUnitID.Value = drItems["ItemUnitID"].ToString();
            ddlUnit.SelectedItem.Text = drItems["UnitName"].ToString();
            // ddlUnit.SelectedValue = drItems["UnitName"].ToString();
            txtRate.Text = drItems["ItemRate"].ToString();

            //txtDiffAmt.Text = drItems["ItemAmount"].ToString();
            decimal r = Convert.ToDecimal(drItems["ItemAmount"].ToString());
            txtDiffAmt.Text = Math.Round(r, 2).ToString();

            txtTaxRate.Text = drItems["TaxRate"].ToString();
            txtCGSTAmt.Text = drItems["CGSTTaxAmount"].ToString();
            txtSGSTAmt.Text = drItems["SGSTTaxAmount"].ToString();
            txtIGSTAmt.Text = drItems["IGSTTaxAmount"].ToString();
            txtCessAmt.Text = drItems["CESSTaxAmount"].ToString();

            hfCGSTTax.Value = drItems["CGSTTaxRate"].ToString();
            hfSGSTTax.Value = drItems["SGSTTaxRate"].ToString();
            hfIGSTTax.Value = drItems["IGSTTaxRate"].ToString();
            hfCESSTax.Value = drItems["CESSTaxRate"].ToString();
            hfcIncomeExCode.Value = drItems["IncomeExpenseCode"].ToString();
            gvItemDetail.Rows[rowIndex].BackColor = Color.Bisque;

            //txtTaxableAmt.Text = drItems["NetAmt"].ToString();

            //VSItemsData.Rows[rowIndex].Delete();
            //gvItemDetail.DataSource = VSItemsData;
            //gvItemDetail.DataBind(); 
            Button btnEdit = (Button)gvItemDetail.Rows[rowIndex].FindControl("btnEdit");
            btnEdit.Enabled = false;
            btnSaveITEM.Enabled = true;

            DisableControl();
        }
        //btnEdit.Enabled = false;
    }

    public void DisableControl()
    {
        txtVoucherDate.Enabled = false;
        ddlAgainst.Enabled = false;
        ddlPartyAccount.Enabled = false;
        ddlGstinNo.Enabled = false;
    }
    public void EnableControl()
    {
        txtVoucherDate.Enabled = true;
        ddlAgainst.Enabled = true;
        ddlPartyAccount.Enabled = true;
        ddlGstinNo.Enabled = true;
    }


    /// <summary>
    /// Item Detail Edit and Save In Grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 
    /// 
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
            CalculateTotalAmount();

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidOnAddItem()
    {
        if (string.IsNullOrEmpty(txtItem.Text))
        {
            txtItem.Focus();
            ShowMessage("Enter Item.", false);
            return false;
        }

        DataRow dr = VSItemsData.Rows[rowIndex];
        //if (CommonCls.ConvertDecimalZero(dr["ItemRate"].ToString()) == CommonCls.ConvertDecimalZero(txtRate.Text))
        //{
        //    if (CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()) == CommonCls.ConvertDecimalZero(txtQty.Text))
        //    {
        //        txtQty.Focus();
        //        //txtRate.Focus();
        //        ShowMessage("Change Item Rate Or Qty.", false);
        //        return false;
        //    }
        //}

        if (CommonCls.ConvertDecimalZero(dr["ItemRate"].ToString()) < CommonCls.ConvertDecimalZero(txtRate.Text))
        {
            txtRate.Focus();
            ShowMessage("Difference Item Rate Cannot be greater than Original Item Rate.", false);
            return false;
        }
        if (CommonCls.ConvertDecimalZero(dr["ItemQty"].ToString()) < CommonCls.ConvertDecimalZero(txtQty.Text))
        {
            txtQty.Focus();
            ShowMessage("Difference Item Quantity Cannot be greater than Original Item Quantity.", false);
            return false;
        }

        if ((CommonCls.ConvertDecimalZero(txtQty.Text) <= 0) || (CommonCls.ConvertDecimalZero(txtRate.Text) <= 0))
        {
            txtQty.Focus();
            ShowMessage("Difference Item Quantity And item Rate Cannot be Negative Or Zero.", false);
            return false;
        }
        //if (CommonCls.ConvertDecimalZero(txtRate.Text) == 0)
        //{
        //    txtQty.Focus();
        //    ShowMessage("Difference Item Quantity And item Rate Cannot be Zero.", false);
        //    return false;
        //}
        //if (Convert.ToDecimal(txtQty.Text) < 0 || CommonCls.ConvertDecimalZero(txtRate.Text) < 0)
        //{
        //    txtQty.Focus();
        //    ShowMessage("Difference Item Quantity And ItemRate Cannot be Negative value.", false);
        //    return false;
        //}
        //------------------------------------------------------------------------Check Item Already In Grid Or Not--------------------------------
        if (VSDtFinalItemData == null)
        {
            VSDtFinalItemData = DtItemsSchema();
        }

        if (VSDtFinalItemData.Rows.Count > 0)
        {
            foreach (DataRow row in VSDtFinalItemData.Rows)
            {

                int OriginalInvoiceNo = Convert.ToInt32(row["OriginalInvoiceNo"].ToString());
                string OriginalInvoiceDate = row["OriginalInvoiceDate"].ToString();
                int ItemId = Convert.ToInt32(row["ItemID"].ToString());
                if (Convert.ToInt32(hfItemID.Value) == ItemId && OriginalInvoiceNo == Convert.ToInt32(txtInVoiceNo.Text) && OriginalInvoiceDate == CommonCls.ConvertToDate(txtInvoiceDate.Text))
                {
                    ShowMessage("This Item allready exist", false);
                    return false;
                }
            }
        }

        return true;
    }

    void AddEditItem()
    {
        if (VSDtFinalItemData == null)
        {
            VSDtFinalItemData = DtItemsSchema();
        }

        DataRow drItems = VSDtFinalItemData.NewRow();

        // drItems["CompanyID"] = GlobalSession.OrgID;
        //drItems["BranchID"] = GlobalSession.BrID; 

        drItems["ItemName"] = txtItem.Text;
        drItems["ItemUnit"] = ddlUnit.SelectedItem.Text;
        drItems["ItemMinorUnit"] = 0;
        drItems["ItemID"] = CommonCls.ConvertIntZero(hfItemID.Value);
        drItems["HSNSACCode"] = txtHSN.Text;
        drItems["GoodsServiceInd"] = 0;
        drItems["ItemQty"] = txtQty.Text;
        drItems["FreeQty"] = 0;
        drItems["ItemUnitID"] = ddlUnit.SelectedValue;
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
        drItems["IncomeExpenseCode"] = CommonCls.ConvertIntZero(hfcIncomeExCode.Value);

        VSDtFinalItemData.Rows.InsertAt(drItems, rowIndex);
        gvFinalItemDetail.DataSource = VSDtFinalItemData;
        gvFinalItemDetail.DataBind();
        txtDrAmount.Enabled = false;

        gvItemDetail.DataSource = VSItemsData;
        gvItemDetail.DataBind();
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
        dtItems.Columns.Add("IncomeExpenseCode", typeof(int));



        return dtItems;

    }
    protected void gvFinalItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtGVFinalItemDetail = VSDtFinalItemData;
            dtGVFinalItemDetail.Rows[rowIndex].Delete();
            VSDtFinalItemData = dtGVFinalItemDetail;
            gvFinalItemDetail.DataSource = dtGVFinalItemDetail;
            gvFinalItemDetail.DataBind();
            CalculateTotalAmount();
        }
    }

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        CalculateRate();
        TaxCal();
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

            item.ItemCGSTRate = CommonCls.ConvertDecimalZero(VSItemsData.Rows[Convert.ToInt32(hfRowInd.Value)]["CGSTTaxRate"].ToString());
            item.ItemSGSTRate = CommonCls.ConvertDecimalZero(VSItemsData.Rows[Convert.ToInt32(hfRowInd.Value)]["SGSTTaxRate"].ToString());
            item.ItemIGSTRate = CommonCls.ConvertDecimalZero(VSItemsData.Rows[Convert.ToInt32(hfRowInd.Value)]["IGSTTaxRate"].ToString());
            item.ItemCESSRate = CommonCls.ConvertDecimalZero(VSItemsData.Rows[Convert.ToInt32(hfRowInd.Value)]["CessTaxRate"].ToString());

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

        if (VSDtFinalItemData != null)
        {
            DataTable dtGrdFinalItems = VSDtFinalItemData;

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
    }
    /// <summary>
    /// Finaly Submit Data.
    /// <functions>
    /// CreateDebitNote, DtItemSchema, DtDebitNote, ClearAll.
    /// </functions>
    /// </summary>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        if (!ValidOnSubmit())
        {
            return;
        }

        try
        {
            ObjDNModel = new DebitNoteModel();
            ObjDNModel.Ind = 1;
            ObjDNModel.OrgID = GlobalSession.OrgID;
            ObjDNModel.BrID = GlobalSession.BrID;
            ObjDNModel.YrCD = GlobalSession.YrCD;
            ObjDNModel.VchType = Convert.ToInt32(ViewState["VchType"]);
            ObjDNModel.VoucherDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            ObjDNModel.DocDesc = ddlNarration.SelectedItem == null ? "" : ddlNarration.SelectedItem.Text;
            ObjDNModel.EntryType = 1;
            ObjDNModel.User = GlobalSession.UserID;
            ObjDNModel.IP = GlobalSession.IP;

            ObjDNModel.issueReasonId = CommonCls.ConvertIntZero(ddlresion.SelectedValue);
            ObjDNModel.PreGstId = CommonCls.ConvertIntZero(ddlPreIssue.SelectedValue);

            ObjDNModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);


            ObjDNModel.DtDebitNote = CreateDebitNote(ObjDNModel);

            if (CBGSTImapactTaken.Checked)
            {
                ObjDNModel.GSTOpted = 1;
                ObjDNModel.DtItems = VSDtFinalItemData;
                txtDrAmount.Enabled = false;
            }
            else
            {
                ObjDNModel.GSTOpted = 0;

                if ((ObjDNModel.DtItems == null) || (ObjDNModel.DtItems.Rows.Count <= 0))
                {
                    ObjDNModel.DtItems = DtItemsSchema();
                    DataRow drDTItemBlank = ObjDNModel.DtItems.NewRow();

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
                    drDTItemBlank["OriginalInvoiceNo"] = 0;// txtInVoiceNo.Text;
                    drDTItemBlank["OriginalInvoiceDate"] = "";// CommonCls.ConvertToDate(txtInvoiceDate.Text);
                    drDTItemBlank["IncomeExpenseCode"] = 0;
                    ObjDNModel.DtItems.Rows.Add(drDTItemBlank);
                }
            }

            string uri = string.Format("DebitNoteVoucher/SaveDebitNote");
            DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, ObjDNModel);
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

    bool ValidOnSubmit()
    {
        bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            ShowMessage("Voucher Date Should Be Within Financial Year Date", false);
            return false;
        }

        if (ddlAgainst == null || ddlAgainst.SelectedItem == null || ddlAgainst.SelectedValue == null || Convert.ToInt32(ddlAgainst.SelectedValue) <= 0)
        {
            ShowMessage("Select DebitNote Against..", false);
            return false;
        }


        if (ddlPartyAccount == null || ddlPartyAccount.SelectedItem == null || ddlPartyAccount.SelectedValue == null || Convert.ToInt32(ddlPartyAccount.SelectedValue) <= 0)
        {
            ShowMessage("Select Party Account.", false);
            return false;
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
        if (!CBGSTImapactTaken.Checked) //Expense Head Compulsory When Impact Not Taken .
        {
            if (ddlAccountHead == null || CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue) <= 0)
            {
                ShowMessage("Select Income Head.", false);
                ddlAccountHead.Focus();
                return false;
            }
        }

        if (ddlGstinNo != null || ddlGstinNo.SelectedItem != null || ddlGstinNo.Items.Count > 0)// GSTIN No Not Select.
        {
            if (ddlGstinNo.Items.Count > 1 && ddlGstinNo.SelectedValue == "0")
            {
                ShowMessage("Select Gstin No", false);
                return false;
            }

            //if (ddlAccountHead == null || ddlAccountHead.SelectedItem == null || ddlAccountHead.SelectedValue == null || Convert.ToInt32(ddlAccountHead.SelectedValue) <= 0)
            //{
            //    ShowMessage("Income Head Not Available.", false);
            //    return false;
            //}
        }

        if (CommonCls.ConvertDecimalZero(txtDrAmount.Text) <= 0)
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
                ShowMessage("Enter Invoice No & Enter Valid Date.", false);
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

    DataTable CreateDebitNote(DebitNoteModel ObjDNModel)
    {
        DataTable dtDebitNote = DtDebitNote();
        DataRow drDebitNote = dtDebitNote.NewRow();
        drDebitNote["OrgID"] = ObjDNModel.OrgID;
        drDebitNote["BrID"] = ObjDNModel.BrID;
        drDebitNote["VchType"] = ObjDNModel.VchType;
        drDebitNote["YrCD"] = ObjDNModel.YrCD;
        drDebitNote["DocDate"] = ObjDNModel.VoucherDate;
        drDebitNote["DocNo"] = 0;
        drDebitNote["AccCode"] = Convert.ToInt32(ddlPartyAccount.SelectedValue);
        if (ddlGstinNo.SelectedItem == null || string.IsNullOrEmpty(ddlGstinNo.SelectedValue))
        {
            drDebitNote["AccGst"] = "";
        }
        else
        {
            drDebitNote["AccGst"] = ddlGstinNo.SelectedValue;
        }

        drDebitNote["AccCode2"] = Convert.ToInt32(ddlAccountHead.SelectedValue);
        drDebitNote["RefNo"] = CommonCls.ConvertIntZero(txtInVoiceNo.Text);
        drDebitNote["RefDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        //if (CBGSTImapactTaken.Checked)
        //{
        //}
        //else
        //{
        //    drDebitNote["RefNo"] = CommonCls.ConvertIntZero(txtInVoiceNo.Text);
        //    drDebitNote["RefDate"] = CommonCls.ConvertToDate(txtInvoiceDate.Text);
        //}
        drDebitNote["AmountDr"] = CommonCls.ConvertDecimalZero(txtDrAmount.Text);
        //drDebitNote["AmountCr"] = ObjDNModel.AmountCr;
        drDebitNote["DocDesc"] = ObjDNModel.DocDesc;

        drDebitNote["EntryType"] = ObjDNModel.EntryType;
        drDebitNote["UserID"] = ObjDNModel.User;
        drDebitNote["IP"] = ObjDNModel.IP;
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

    /// <summary>
    /// Clear All Data 
    /// </summary> 
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
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
        ddlAgainst.ClearSelection();
        ddlresion.ClearSelection();
        ddlPreIssue.ClearSelection();
        txtDrAmount.Enabled = true;
        ddlGstinNo.DataSource = new DataTable();
        ddlGstinNo.DataBind();
        VSDtFinalItemData = null;

        ddlAccountHead.Visible = true;
        ddlAgainst.Enabled = true;
        ddlPartyAccount.Enabled = true;

        gvFinalItemDetail.DataSource = null;

        gvFinalItemDetail.DataSource = new DataTable();
        gvFinalItemDetail.DataBind();

        gvItemDetail.DataSource = new DataTable();
        gvItemDetail.DataBind();
        txtInvoiceDate.Enabled = true;
        txtInVoiceNo.Enabled = true;
        CBGSTImapactTaken.Enabled = true;

        lblMsg.Text = "";
        txtVoucherDate.Focus();
        EnableControl();
        ClearAfterSave();
        ddlCostCenter.Enabled = true;
        ddlCostCenter.ClearSelection();
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
}