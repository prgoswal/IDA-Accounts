using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmSupplierAdvPayment : System.Web.UI.Page
{
    #region Declartion
    DataTable dtItemgrdview, dtAccGstin;
    AdvSuppPaymentModel plAdvSupPayment;

    public DataTable VsDtCashBankAcc
    {
        get { return (DataTable)ViewState["dtCashBankAcc"]; }
        set { ViewState["dtCashBankAcc"] = value; }
    }

    public DataTable VsDtAccheadGstin
    {
        get { return (DataTable)ViewState["dtAccountHead"]; }
        set { ViewState["dtAccountHead"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txtVoucherDate.Focus();
            ViewState["VchType"] = 14;
            FillAllDropDown();
            SetPayMode();

            //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
            //{
            //    btnSave.Visible = false;

            //}
            //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
            //{
            //    btnSave.Visible = true;
            //}

        }
        lblMsg.CssClass = "";
        lblMsg.Text = "";
    }

    private void FillAllDropDown()
    {
        try
        {
            plAdvSupPayment = new AdvSuppPaymentModel();
            plAdvSupPayment.OrgID = GlobalSession.OrgID;
            plAdvSupPayment.BrID = GlobalSession.BrID;
            plAdvSupPayment.YrCD = GlobalSession.YrCD;
            plAdvSupPayment.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("AdvancedSuppPayment/AdSuppPay");
            DataSet dsallTable = CommonCls.ApiPostDataSet(uri, plAdvSupPayment);
            if (dsallTable.Tables.Count > 0)
            {
                //---------------------------------------------tableName--------------------------------------------------------------\\
                VsDtCashBankAcc = dsallTable.Tables["CashBankAccount"];
                VsDtAccheadGstin = dsallTable.Tables["AccountHead"];
                DataTable dtItemList = dsallTable.Tables["ItemList"];
                DataTable dtNarration = dsallTable.Tables["Narration"];
                DataTable dtLastVoucherNo = dsallTable.Tables["LastVoucherNo"];

                //--------------------------------------------BindCashBank Account--------------------------------------------------
                ddlCashBankAccount.DataSource = VsDtCashBankAcc;
                ddlCashBankAccount.DataTextField = "AccName";
                ddlCashBankAccount.DataValueField = "AccCode";
                ddlCashBankAccount.DataBind();
                ddlCashBankAccount.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });

                //--------------------------------------------Bind Account Head-----------------------------------------------------
                txtAccountHead.DataSource = VsDtAccheadGstin;
                txtAccountHead.DataTextField = "AccName";
                txtAccountHead.DataValueField = "AccCode";
                txtAccountHead.DataBind();
                txtAccountHead.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });

                //-----------------------------------------------BindItemList-------------------------------------------------------
                ddlItem.DataSource = dtItemList;
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemID";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });

                //-----------------------------------------------Bind Narration------------------------------------------------------
                txtNarration.DataSource = dtNarration;
                txtNarration.DataTextField = "NarrationDesc";
                txtNarration.DataBind();
                txtNarration.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });

                //-----------------------------------------------Last Voucher No-------------------------------------------  
                if (dtLastVoucherNo != null)
                {
                    if (dtLastVoucherNo.Rows.Count > 0)
                    {
                        if (dtLastVoucherNo.Rows[0]["LastNo"].ToString() == "0")
                        {
                            return;
                        }
                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucherNo.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtLastVoucherNo.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    // protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e) //Account Head Select Index Changed 
    //{
    //    try 
    //    {
    //        dtAccGstin = (DataTable)(ViewState["dtAccGstin"]);
    //        DataView DVAccGstin = new DataView(dtAccGstin);
    //        DVAccGstin.RowFilter = "AccCode=" + txtAccountHead.SelectedItem.Value;
    //        DataTable DTFilter = DVAccGstin.ToTable();

    //        if ((DTFilter.Rows.Count > 0) && (DTFilter.Rows.Count == 1))
    //        {
    //            ddlGSTINNo.DataSource = DTFilter;
    //            ddlGSTINNo.DataTextField = "GSTIN";
    //            ddlGSTINNo.DataBind();
    //            ddlGSTINNo.SelectedIndex = 0;
    //        }
    //        else if (DTFilter.Rows.Count > 1)
    //        {
    //            ddlGSTINNo.DataSource = DTFilter;
    //            ddlGSTINNo.DataTextField = "GSTIN";
    //            ddlGSTINNo.DataBind();
    //            ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0"));
    //            ddlGSTINNo.SelectedIndex = 0;
    //            ddlGSTINNo.Focus();
    //        }
    //        else
    //        {
    //            ddlGSTINNo.DataSource = null;
    //            ddlGSTINNo.DataBind();
    //        }

    //    } 
    //     catch(Exception ex)
    //    {

    //    }
    //}


    void SetPayMode()   //ddlPayMode
    {

        if (ddlPayMode.SelectedValue == "Cheque")
        {

            lblPayModeNo.Text = "Cheque No.";
            lblPayModeDate.Text = "Cheque Date";
            txtReceivedNo.MaxLength = 8;
            txtReceivedNo.CssClass = "numberonly";
        }
        else
        {
            lblPayModeNo.Text = "UTR No.";
            lblPayModeDate.Text = "UTR Date";
            txtReceivedNo.MaxLength = 16;
            txtReceivedNo.CssClass = "";//.Replace("numberonly", "");
        }
        txtReceivedNo.Text = txtReceivedDate.Text = "";
    }
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e) //PayModeSelectIndexChanged
    {
        SetPayMode();
        txtReceivedNo.Focus();
    }
    DataTable Creatgrddt()
    {
        dtItemgrdview = new DataTable();
        dtItemgrdview.Columns.Add("CompanyID", typeof(int));
        dtItemgrdview.Columns.Add("BranchID", typeof(int));
        dtItemgrdview.Columns.Add("ItemName", typeof(string));
        dtItemgrdview.Columns.Add("ItemUnit", typeof(string));
        dtItemgrdview.Columns.Add("ItemID", typeof(long));
        dtItemgrdview.Columns.Add("HsnSacCode", typeof(int));
        dtItemgrdview.Columns.Add("GoodsServiceInd", typeof(int));
        dtItemgrdview.Columns.Add("ItemQty", typeof(decimal));
        dtItemgrdview.Columns.Add("ItemUnitID", typeof(int));
        dtItemgrdview.Columns.Add("ItemRate", typeof(decimal));
        dtItemgrdview.Columns.Add("ItemAmt", typeof(decimal));
        dtItemgrdview.Columns.Add("TaxRate", typeof(decimal));
        dtItemgrdview.Columns.Add("IGSTTax", typeof(decimal));
        dtItemgrdview.Columns.Add("IGSTTaxAmt", typeof(decimal));
        dtItemgrdview.Columns.Add("CGSTTax", typeof(decimal));
        dtItemgrdview.Columns.Add("CGSTTaxAmt", typeof(decimal));
        dtItemgrdview.Columns.Add("SGSTTax", typeof(decimal));
        dtItemgrdview.Columns.Add("SGSTTaxAmt", typeof(decimal));
        dtItemgrdview.Columns.Add("CESSTax", typeof(decimal));
        dtItemgrdview.Columns.Add("CESSTaxAmt", typeof(decimal));
        dtItemgrdview.Columns.Add("ItemRemark", typeof(string));
        dtItemgrdview.Columns.Add("ExtraInd", typeof(int));
        return dtItemgrdview;
    }
    protected void btnItemAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ddlItem.Focus();
            if (ddlItem.SelectedValue == "0")
            {
                ddlItem.Focus();
                ShowMessage("Select ItemName..!", false);

                return;
            }

            if (ddlItem.Text == "-Select-")
            {
                ddlItem.Focus();
                ShowMessage("Select ItemName..!", false);

                return;
            }
            //if (ddlRate.Text == "--Select--")
            //{
            //    ddlRate.Focus();
            //    ShowMessage("Select ItemRate..!", false);
            //    return;
            //}
            if (CommonCls.ConvertIntZero(ddlRate.SelectedValue) == 0)
            {
                ddlRate.Focus();
                ShowMessage("Select ItemRate..!", false);
                return;
            }

            if (string.IsNullOrEmpty(txtItemAmt.Text))
            {
                txtItemAmt.Focus();
                ShowMessage("Enter Item Amount..!", false);
                return;
            }
            dtItemgrdview = (DataTable)ViewState["grditemTax"];
            if (dtItemgrdview != null && dtItemgrdview.Rows.Count > 0)
            {
                DataRow[] rows = dtItemgrdview.Select("ItemID=" + ddlItem.SelectedItem.Value);
                if (rows.Count() >= 1)
                {
                    ddlItem.Focus();
                    //ClearAll();
                    ShowMessage("This Item Name  Already Exist.", false);
                    return;
                }
            }
            if (ViewState["grditemTax"] == null)
            {
                Creatgrddt();
            }
            else
            {
                dtItemgrdview = (DataTable)ViewState["grditemTax"];
            }

            DataTable dtItems = (DataTable)ViewState["dtItems"];
            TaxCal();
            string lblHsnSacCode = dtItems.Rows[0]["HSNSACCode"].ToString();
            string lblTaxRate = ddlRate.SelectedItem.Text;
            string lblIGST = dtItems.Rows[0]["IGSTRate"].ToString();
            string lblCGST = dtItems.Rows[0]["CGSTRate"].ToString();
            string lblSGST = dtItems.Rows[0]["SGSTRate"].ToString();
            string lblCESS = dtItems.Rows[0]["Cess"].ToString();

            DataTable dt = (DataTable)dtItemgrdview;//ViewState["dtItemgrdview"]; 
            DataRow dr = dt.NewRow();
            dr["CompanyID"] = GlobalSession.OrgID;
            dr["BranchID"] = GlobalSession.BrID;
            dr["ItemName"] = ddlItem.SelectedItem.Text;//;
            dr["ItemUnit"] = "";
            dr["ItemID"] = ddlItem.SelectedItem.Value; //;
            dr["HsnSacCode"] = lblHsnSacCode;// ;
            dr["GoodsServiceInd"] = dtItems.Rows[0]["GoodsServiceIndication"].ToString();
            dr["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            dr["ItemUnitID"] = dtItems.Rows[0]["ItemUnitID"].ToString();
            dr["ItemRate"] = 0;
            dr["ItemAmt"] = CommonCls.ConvertDecimalZero(txtItemAmt.Text);
            dr["TaxRate"] = CommonCls.ConvertDecimalZero(lblTaxRate);
            dr["IGSTTax"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"].ToString());
            dr["IGSTTaxAmt"] = IGSTTaxAmt;
            dr["CGSTTax"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"].ToString());
            dr["CGSTTaxAmt"] = CGSTTaxAmt;
            dr["SGSTTax"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"].ToString());
            dr["SGSTTaxAmt"] = SGSTTaxAmt;
            dr["CESSTax"] = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"].ToString());
            dr["CESSTaxAmt"] = CESSTaxAmt;
            dr["ItemRemark"] = txtRemark.Text;
            dr["ExtraInd"] = 0;
            dt.Rows.Add(dr);
            grditemTax.DataSource = ViewState["grditemTax"] = dt;
            grditemTax.DataBind();

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        CalculateTotalAmount();
        clearAfterAdd();
    }
    
    decimal CGSTTaxAmt, SGSTTaxAmt, IGSTTaxAmt, CESSTaxAmt;
    decimal CgstRat, SgstRat, IgstRat, CessRat;

    void TaxCal() //Tax Calculation
    {
        //decimal CgstAmt = 0, SgstAmt = 0, IgstAmt = 0, CessAmt = 0;

        decimal MaxAmt = Convert.ToDecimal(txtItemAmt.Text);

        int devidedBy;
        decimal TaxBy;

        DataTable dtItems = (DataTable)ViewState["dtItems"];
        TaxBy = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["TaxRate"].ToString());

        decimal dtCGST = 0, dtSGST = 0, dtIGST = 0, dtCESS = 0;
        dtCGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["CGSTRate"].ToString());
        dtSGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["SGSTRate"].ToString());
        dtIGST = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["IGSTRate"].ToString());
        dtCESS = CommonCls.ConvertDecimalZero(dtItems.Rows[0]["Cess"].ToString());

        int ValidCG = (dtCGST > 0 ? 1 : 0);
        int ValidSG = (dtSGST > 0 ? 1 : 0);
        int ValidIG = (dtIGST > 0 ? 1 : 0);
        int ValidCess = (dtCESS > 0 ? 1 : 0);

        devidedBy = ValidCG + ValidSG + ValidIG + ValidCess;

        if (ValidCG == 1)
        {
            CgstRat = TaxBy / devidedBy;
            CGSTTaxAmt = Math.Round(((MaxAmt * CgstRat) / 100), 2);
        }
        if (ValidIG == 1)
        {
            IgstRat = TaxBy / devidedBy;
            IGSTTaxAmt = Math.Round(((MaxAmt * IgstRat) / 100), 2);
        }
        if (ValidSG == 1)
        {
            SgstRat = TaxBy / devidedBy;
            SGSTTaxAmt = Math.Round(((MaxAmt * SgstRat) / 100), 2);
        }
        if (ValidCess == 1)
        {
            CessRat = TaxBy / devidedBy;
            CESSTaxAmt = Math.Round(((MaxAmt * CessRat) / 100), 2);
        }
        IGSTTaxAmt = Convert.ToDecimal(IGSTTaxAmt.ToString());
        CGSTTaxAmt = Convert.ToDecimal(CGSTTaxAmt.ToString());
        SGSTTaxAmt = Convert.ToDecimal(SGSTTaxAmt.ToString());
        CESSTaxAmt = Convert.ToDecimal(CESSTaxAmt.ToString());

        txtItemAmt.Text = Convert.ToString(CommonCls.ConvertDecimalZero(txtItemAmt.Text) - (IGSTTaxAmt + CGSTTaxAmt + SGSTTaxAmt + CESSTaxAmt));
    }
    void CalculateTotalAmount()
    {
        Decimal Igst = 0, Sgst = 0, Cgst = 0, Cess = 0;
        Decimal netAmt = 0;

        if (grditemTax != null)
        {
            DataTable dtGrdItems = (DataTable)ViewState["grditemTax"];

            foreach (DataRow item in dtGrdItems.Rows)
            {
                Igst += Convert.ToDecimal(item["IGSTTaxAmt"]);
                Sgst += Convert.ToDecimal(item["SGSTTaxAmt"]);
                Cgst += Convert.ToDecimal(item["CGSTTaxAmt"]);
                Cess += Convert.ToDecimal(item["CESSTaxAmt"]);
                //Taxable += Convert.ToDecimal(item["NetAmt"]); 
                netAmt += Convert.ToDecimal(item["ItemAmt"]);
            }

            txtGross.Text = netAmt.ToString();
            txtTaxable.Text = (Igst + Sgst + Cgst + Cess).ToString();
            decimal taxableAmt = Convert.ToDecimal(txtTaxable.Text);
            decimal gross = Convert.ToDecimal(txtGross.Text);

            txtNet.Text = Convert.ToString(gross + taxableAmt);
        }
    }
    void clearAfterAdd()
    {
        ddlItem.SelectedValue = "0";
        // ddlRate.SelectedIndex=0; 
        ddlRate.Items.Clear();
        txtItemAmt.Text = "";
        txtRemark.Text = "";
        txtQty.Text = "";
        ddlItem.Focus();

    }
    protected void grditemTax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtItemgrdview = (DataTable)ViewState["grditemTax"];
                dtItemgrdview.Rows[rowIndex].Delete();

                ViewState["grditemTax"] = dtItemgrdview;
                grditemTax.DataSource = dtItemgrdview;
                grditemTax.DataBind();
                CalculateTotalAmount();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlRate.Items.Clear();
            plAdvSupPayment = new AdvSuppPaymentModel();
            plAdvSupPayment.Ind = 11;
            plAdvSupPayment.OrgID = GlobalSession.OrgID;
            plAdvSupPayment.BrID = GlobalSession.BrID;
            plAdvSupPayment.VchType = Convert.ToInt32(ViewState["VchType"]);
            plAdvSupPayment.AccountCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue.ToString());
            plAdvSupPayment.ItemID = Convert.ToInt32(ddlItem.SelectedItem.Value);

            //if (ddlGSTINNo.SelectedItem != null)
            //{
            //    if (!string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value))
            //    {
            //        plAdvSupPayment.GSTIN = Convert.ToInt32(ddlGSTINNo.SelectedItem.Value);
            //    }
            //}

            string uri = string.Format("AdvancedSuppPayment/FillItemRate");
            DataTable dtItems = CommonCls.ApiPostDataTable(uri, plAdvSupPayment);
            if (dtItems.Rows.Count > 0)
            {
                ViewState["dtItems"] = dtItems;

                ddlRate.DataSource = dtItems;
                ddlRate.DataTextField = "TaxRate";
                ddlRate.DataValueField = "ItemID";
                ddlRate.DataBind();
                if (dtItems.Rows.Count > 1)
                    ddlRate.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });

            }
            ddlRate.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (string.IsNullOrEmpty(txtVoucherDate.Text))
            {
                txtVoucherDate.Focus();
                ShowMessage("Enter Voucher Date..!", false);
                return;
            }
            bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate) // For Voucher Date Between Financial Year.
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return;
            }
            if (ddlCashBankAccount.SelectedItem.Value == null || Convert.ToInt32(ddlCashBankAccount.SelectedItem.Value) == 0) // For Account Head Code Not Null Or Empty
            {
                ddlCashBankAccount.Focus();
                ShowMessage("Select CashBank Account", false);
                return;
            }
            if (string.IsNullOrEmpty(txtAccountHead.Text)) // For Account Head Not Null Or Empty
            {
                txtAccountHead.Focus();
                ShowMessage("Enter Account Head.", false);
                return;
            }

            try // For txtAccount Head Value Shouldn't null,0 or Garbage.
            {
                if (txtAccountHead.SelectedItem.Value == null || Convert.ToInt32(txtAccountHead.SelectedItem.Value) == 0) // For Account Head Code Not Null Or Empty
                {
                    txtAccountHead.Focus();
                    ShowMessage("Account Value Not Available", false);
                    return;
                }
            }
            catch (Exception)
            {
                txtAccountHead.Focus();
                ShowMessage("This Account Head Value Not Available.", false);
                return;
            }
            if (!string.IsNullOrEmpty(txtReceivedNo.Text))
            {
                if (string.IsNullOrEmpty(txtReceivedDate.Text))
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Enter Cheque Date.", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("Enter RTGS/NEFT Date", false);
                        return;
                    }
                }
                bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ReceivedDate) // For Voucher Date Between Financial Year.
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtReceivedDate.Text))
            {
                if (string.IsNullOrEmpty(txtReceivedNo.Text))
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Enter Cheque No.", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("Enter RTGS/NEFT No.", false);
                        return;
                    }
                }

                bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ReceivedDate) // For Voucher Date Between Financial Year.
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                        return;
                    }
                }
            }


            if (ddlGSTINNo.SelectedValue.ToUpper() != "")
            {
                bool GSTINNumber = CheckGSTINNumber_Validation();
                if (GSTINNumber == false)
                {
                    ShowMessage("Invalid GSTIN No.", false);
                    return;
                }
            }
            plAdvSupPayment = new AdvSuppPaymentModel();
            plAdvSupPayment.Ind = 2;
            plAdvSupPayment.OrgID = GlobalSession.OrgID;
            plAdvSupPayment.BrID = GlobalSession.BrID;
            plAdvSupPayment.YrCD = GlobalSession.YrCD;
            plAdvSupPayment.User = GlobalSession.UserID;
            plAdvSupPayment.IP = GlobalSession.IP;
            plAdvSupPayment.VchType = Convert.ToInt32(ViewState["VchType"]);
            plAdvSupPayment.VchDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            plAdvSupPayment.CashBankCode = Convert.ToInt32(ddlCashBankAccount.SelectedValue);
            plAdvSupPayment.AccountCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue.ToString());
            plAdvSupPayment.GSTIN = ddlGSTINNo.SelectedItem == null ? "" : ddlGSTINNo.SelectedItem.Text;
            plAdvSupPayment.VchNarratio = txtNarration.Text;
            plAdvSupPayment.NetAmount = CommonCls.ConvertDecimalZero(txtNet.Text);
            plAdvSupPayment.TaxAmount = CommonCls.ConvertDecimalZero(txtTaxable.Text);
            plAdvSupPayment.GrossAmount = CommonCls.ConvertDecimalZero(txtGross.Text);
            plAdvSupPayment.ItemID = Convert.ToInt32(ddlItem.SelectedItem.Value);
            if (ddlPayMode.SelectedValue == "Cheque")
            {
                plAdvSupPayment.ChequeNo = CommonCls.ConvertIntZero(txtReceivedNo.Text);
                plAdvSupPayment.ChequeDate = CommonCls.ConvertToDate(txtReceivedDate.Text);
            }
            else
            {
                plAdvSupPayment.UTRNo = txtReceivedNo.Text;
                plAdvSupPayment.UTRDate = CommonCls.ConvertToDate(txtReceivedDate.Text);
            }

            plAdvSupPayment.TblAdvItems = (DataTable)ViewState["grditemTax"];

            string uri = string.Format("AdvancedSuppPayment/SaveingpaymentProcess");
            DataTable dtSaveing = CommonCls.ApiPostDataTable(uri, plAdvSupPayment);
            if (dtSaveing.Rows.Count > 0)
            {
                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtSaveing.Rows[0]["DocMaxNo"].ToString() + " - " + Convert.ToDateTime(dtSaveing.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                ShowMessage("Data SucessFully Saved", true);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        SavingFinalClear();
    }

    private bool CheckGSTINNumber_Validation()
    {
        try
        {

            //check GSTIN Number Expression
            bool CheckGSTIN_Expression = CommonCls.validGSTIN(ddlGSTINNo.SelectedValue.ToUpper());
            if (CheckGSTIN_Expression == true)
            {
                SalesModel ObjSaleModel;
                ObjSaleModel = new SalesModel();
                ObjSaleModel.Ind = 6;
                ObjSaleModel.OrgID = GlobalSession.OrgID;
                ObjSaleModel.BrID = GlobalSession.BrID;
                ObjSaleModel.AccCode = Convert.ToInt32(txtAccountHead.SelectedValue);

                string uri = string.Format("PurchaseVoucher/CheckGSTIN_Number");

                DataSet dtStatePanNo = CommonCls.ApiPostDataSet(uri, ObjSaleModel);
                if (dtStatePanNo.Tables[0].Rows.Count > 0)
                {


                    string PANNo = dtStatePanNo.Tables[1].Rows[0]["PanNo"].ToString();
                    DataTable dtState = dtStatePanNo.Tables[0];
                    DataRow[] drStates = dtState.Select("StateID=" + ddlGSTINNo.SelectedValue.Substring(0, 2));
                    string StateID = (drStates.Count() > 0) ? drStates[0]["StateID"].ToString() : "";


                    if (CheckGSTIN_Expression == true && !string.IsNullOrEmpty(PANNo.ToUpper()))
                    {

                        //check GSTIN Number by Statid an panNo 
                        bool CheckGSTIN_Number = CommonCls.gstinvalid(ddlGSTINNo.SelectedValue.ToUpper(), StateID, PANNo.ToUpper());
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
    public void SavingFinalClear()//----------------Saving After Saving-------------------------
    {
        txtVoucherDate.Text = "";
        txtAccountHead.ClearSelection();
        ddlItem.ClearSelection();
        txtItemAmt.Text = "";
        ViewState["grditemTax"]=null;
        grditemTax.DataSource = new DataTable();
        grditemTax.DataBind();
        txtNarration.ClearSelection();
        txtNet.Text = "";
        txtTaxable.Text = "";
        txtGross.Text = "";
        txtReceivedNo.Text = "";
        txtReceivedDate.Text = "";
        ddlGSTINNo.Items.Clear();
        ddlCashBankAccount.ClearSelection();
        txtRemark.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    public void ClearAll()
    {
        txtAccountHead.ClearSelection();
        ddlItem.ClearSelection();
        txtNarration.ClearSelection();
        ddlRate.Items.Clear();
        ddlCashBankAccount.ClearSelection();
        ViewState["grditemTax"] = null;

        ddlGSTINNo.DataSource = new DataTable();
        ddlGSTINNo.DataBind();
        grditemTax.DataSource = new DataTable();
        grditemTax.DataBind();

        txtItemAmt.Text = txtNet.Text = txtTaxable.Text = txtGross.Text = txtReceivedNo.Text =
        txtReceivedDate.Text = txtQty.Text = txtVoucherDate.Text = lblMsg.Text = txtRemark.Text = "";
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void ddlCashBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow[] dr = VsDtCashBankAcc.Select("AccCode='" + ddlCashBankAccount.SelectedValue + "'");
        int ind = Convert.ToInt32(dr[0]["Ind"]);
        if (ind == 1)
        {
            trPayMode.Visible = false;

        }
        else if (ind == 2)
        {
            trPayMode.Visible = true;

        }
    }
    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataRow[] dr = VsDtAccheadGstin.Select("AccCode='" + txtAccountHead.SelectedValue + "'");
        ddlGSTINNo.Items.Clear();
        foreach (DataRow row in dr)
        {
            if (!string.IsNullOrEmpty(row["GSTIN"].ToString()))
            {
                ddlGSTINNo.Items.Add(row["GSTIN"].ToString());
            }
        }
        if (dr.Length > 1)
        {
            ddlGSTINNo.Items.Insert(0, "-Select-");
            ddlGSTINNo.SelectedIndex = 0;
        }
    }
}
