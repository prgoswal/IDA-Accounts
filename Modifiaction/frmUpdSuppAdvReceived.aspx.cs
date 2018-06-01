using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdSuppAdvReceived : System.Web.UI.Page
{
    UpdateAdvSuppReceivedModel plObjUpdAdvSuppReceived;
    DataTable dtItemgrdview, DTrow;
    public DataTable VsDtCashBankAcc
    {
        get { return (DataTable)ViewState["dtCashBankAcc"]; }
        set { ViewState["dtCashBankAcc"] = value; }
    }
    public DataTable VsDtAcchead
    {
        get { return (DataTable)ViewState["dtAccountHead"]; }
        set { ViewState["dtAccountHead"] = value; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtVoucherNo.Focus();
            ViewState["VchType"] = 13;
            FillAllDropDown();
            SetpayMode();
            EnabledControl();
            BindCancelReason();
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
        lblMsg.CssClass = "";
        lblMsg.Text = "";
    }

    void TotalAmtFormat()
    {
        txtGross.Text = CommonCls.ConvertDecimalZero(txtGross.Text).ToString("0.00");
        txtNet.Text = CommonCls.ConvertDecimalZero(txtNet.Text).ToString("0.00");
        txtTaxable.Text = CommonCls.ConvertDecimalZero(txtTaxable.Text).ToString("0.00");
    }
    private void BindCancelReason()
    {
        try
        {

            plObjUpdAdvSuppReceived = new UpdateAdvSuppReceivedModel();
            plObjUpdAdvSuppReceived.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, plObjUpdAdvSuppReceived);
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

    private void FillAllDropDown()
    {
        try
        {
            plObjUpdAdvSuppReceived = new UpdateAdvSuppReceivedModel();
            plObjUpdAdvSuppReceived.OrgID = GlobalSession.OrgID;
            plObjUpdAdvSuppReceived.BrID = GlobalSession.BrID;
            plObjUpdAdvSuppReceived.YrCD = GlobalSession.YrCD;
            plObjUpdAdvSuppReceived.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateSupplierAdvReceived/AdUpdSuppRec");
            DataSet dsallTable = CommonCls.ApiPostDataSet(uri, plObjUpdAdvSuppReceived);
            if (dsallTable.Tables.Count > 0)
            {
                //---------------------------------------------tableName--------------------------------------------------------------
                VsDtCashBankAcc = dsallTable.Tables["CashBankAccount"];
                VsDtAcchead = dsallTable.Tables["AccountHead"];
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
                txtAccountHead.DataSource = VsDtAcchead;
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
                //---------------------------------------------Last Voucher No-----------------------------------------------

                if (dtLastVoucherNo != null)
                {
                    if (dtLastVoucherNo.Rows.Count > 0)
                    {
                        //if (dtLastVoucherNo.Rows[0]["LastNo"].ToString() == "0")
                        //{
                        //    return;
                        //}
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

    protected void ddlCashBankAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        visiblePayMode();
    }
    public bool visiblePayMode()
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
        ddlCashBankAccount.Focus();

        return trPayMode.Visible;
    }


    private void SetpayMode()
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
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetpayMode();
        txtReceivedNo.Focus();
    }


    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gstin();
    }
    public void Gstin()
    {
        DataRow[] dr = VsDtAcchead.Select("AccCode='" + txtAccountHead.SelectedValue + "'");
        foreach (DataRow row in dr)
        {
            ddlGSTINNo.Items.Add(row["GSTIN"].ToString());
        }
        if (dr.Length > 1)
        {
            ddlGSTINNo.Items.Insert(0, "-Select-");
            ddlGSTINNo.SelectedIndex = 0;
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e) //------ItemSelectIndexChanged------- 
    {
        TaxRate();
    }
    public void TaxRate()
    {
        try
        {
            ddlRate.Items.Clear();
            plObjUpdAdvSuppReceived = new UpdateAdvSuppReceivedModel();
            plObjUpdAdvSuppReceived.Ind = 11;
            plObjUpdAdvSuppReceived.OrgID = GlobalSession.OrgID;
            plObjUpdAdvSuppReceived.BrID = GlobalSession.BrID;
            plObjUpdAdvSuppReceived.VchType = Convert.ToInt32(ViewState["VchType"]);
            plObjUpdAdvSuppReceived.AccountCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue.ToString());
            plObjUpdAdvSuppReceived.ItemID = Convert.ToInt32(ddlItem.SelectedItem.Value);

            //if (ddlGSTINNo.SelectedItem != null)
            //{
            //    if (!string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value))
            //    {
            //        plObjAdvSuppReceived.GSTIN = ddlGSTINNo.SelectedItem.Text;
            //    }
            //}
            string uri = string.Format("UpdateSupplierAdvReceived/FillItemRate");
            DataTable dtItems = CommonCls.ApiPostDataTable(uri, plObjUpdAdvSuppReceived);
            if (dtItems.Rows.Count > 0)
            {
                ViewState["dtItems"] = dtItems;

                ddlRate.DataSource = dtItems;
                ddlRate.DataTextField = "TaxRate";
                ddlRate.DataValueField = "ItemID";
                ddlRate.DataBind();
                if (dtItems.Rows.Count > 1)
                    ddlRate.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });


                ddlRate.Focus();

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtVoucherNo.Text))
            {
                txtVoucherNo.Focus();
                ShowMessage("Enter Voucher No..!", false);
                return;
            }

            plObjUpdAdvSuppReceived = new UpdateAdvSuppReceivedModel();
            plObjUpdAdvSuppReceived.Ind = 2;
            plObjUpdAdvSuppReceived.OrgID = GlobalSession.OrgID;
            plObjUpdAdvSuppReceived.BrID = GlobalSession.BrID;
            plObjUpdAdvSuppReceived.YrCD = GlobalSession.YrCD;
            plObjUpdAdvSuppReceived.VchType = Convert.ToInt32(ViewState["VchType"]);
            plObjUpdAdvSuppReceived.DocNo = Convert.ToInt32(txtVoucherNo.Text);

            string uri = string.Format("UpdateSupplierAdvReceived/SearchingProcess");
            DataTable dtSearchData = CommonCls.ApiPostDataTable(uri, plObjUpdAdvSuppReceived);
            if (dtSearchData.Rows.Count > 0)
            {

                if (dtSearchData.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ShowMessage("This Voucher No. Already Canceled.", false);
                    txtVoucherNo.Focus();
                    return;
                }


                txtVoucherDate.Text = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["VoucharDate"].ToString());
                ddlCashBankAccount.SelectedValue = dtSearchData.Rows[0]["PurchaseSaleCode"].ToString();
                txtAccountHead.SelectedValue = dtSearchData.Rows[0]["AccountCode"].ToString();
                Gstin();
                ddlGSTINNo.SelectedValue = dtSearchData.Rows[0]["GSTIN"].ToString();

                if (visiblePayMode())
                {
                    if (CommonCls.ConvertDateDB(dtSearchData.Rows[0]["ChequeDate"].ToString()) != string.Empty)
                    {
                        ddlPayMode.SelectedValue = "Cheque";
                        SetpayMode();
                        txtReceivedNo.Text = dtSearchData.Rows[0]["ChequeNo"].ToString();
                        txtReceivedDate.Text = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["ChequeDate"].ToString());
                    }
                    else if (CommonCls.ConvertDateDB(dtSearchData.Rows[0]["UTRDate"].ToString()) != string.Empty)
                    {
                        ddlPayMode.SelectedValue = "UTR";
                        SetpayMode();
                        txtReceivedNo.Text = dtSearchData.Rows[0]["UTRNo"].ToString();
                        txtReceivedDate.Text = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["UTRDate"].ToString());
                    }
                }
                //if (ddlPayMode.SelectedValue == "Cheque")
                //{
                //    txtReceivedNo.Text = dtSearchData.Rows[0]["ChequeNo"].ToString(); //CommonCls.ConvertIntZero(dtSearchData.Rows[0]["ChequeNo"].ToString());
                //    txtReceivedDate.Text = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["ChequeDate"].ToString());

                //    //plObjUpdAdvSuppReceived.ChequeNo =   CommonCls.ConvertIntZero(dtSearchData.Rows[0]["ChequeNo"].ToString());
                //    //plObjUpdAdvSuppReceived.ChequeDate = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["ChequeNo"].ToString());
                //}
                //else
                //{
                //    txtReceivedNo.Text = dtSearchData.Rows[0]["ChequeNo"].ToString(); //CommonCls.ConvertIntZero(dtSearchData.Rows[0]["ChequeNo"].ToString());
                //    txtReceivedDate.Text = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["ChequeDate"].ToString());

                //    //plObjUpdAdvSuppReceived.UTRNo =  dtSearchData.Rows[0]["UTRNo"].ToString();
                //    //plObjUpdAdvSuppReceived.UTRDate = CommonCls.ConvertDateDB(dtSearchData.Rows[0]["UTRDate"].ToString());
                //}

                if (txtNarration.Items.FindByText(dtSearchData.Rows[0]["Narration"].ToString()) == null)
                {
                    if (!string.IsNullOrEmpty(dtSearchData.Rows[0]["Narration"].ToString()))
                    {
                        txtNarration.Items.Add(dtSearchData.Rows[0]["Narration"].ToString());
                        txtNarration.SelectedValue = dtSearchData.Rows[0]["Narration"].ToString();
                    }
                }
                else
                {
                    txtNarration.SelectedValue = dtSearchData.Rows[0]["Narration"].ToString();
                }
                //txtNarration.Text = dtSearchData.Rows[0]["Narration"].ToString();
                txtGross.Text = dtSearchData.Rows[0]["GrossAmount"].ToString();
                txtTaxable.Text = dtSearchData.Rows[0]["TaxAmount"].ToString();
                txtNet.Text = dtSearchData.Rows[0]["NetAmount"].ToString();

                dtSearchData.Columns.Add("ExtraInd", typeof(int));

                txtVoucherNo.Enabled = false;
                btngo.Enabled = false;


                grditemTax.DataSource = ViewState["grditemTax"] = dtSearchData;
                grditemTax.DataBind();

                btnSave.Enabled = true;
                DisabledControl();
                TotalAmtFormat();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    public void DisabledControl()
    {
        ddlCashBankAccount.Enabled = false;
        ddlGSTINNo.Enabled = false;
        txtAccountHead.Enabled = false;
    }
    public void EnabledControl()
    {
        ddlCashBankAccount.Enabled = true;
        ddlGSTINNo.Enabled = true;
        txtAccountHead.Enabled = true;
    }

    DataTable Creatgrddt()
    {
        dtItemgrdview = new DataTable();
        dtItemgrdview.Columns.Add("CompanyID", typeof(int));
        dtItemgrdview.Columns.Add("BranchID", typeof(int));
        dtItemgrdview.Columns.Add("ItemName", typeof(string));
        // dtItemgrdview.Columns.Add("ItemUnit", typeof(string));
        dtItemgrdview.Columns.Add("ItemID", typeof(long));
        dtItemgrdview.Columns.Add("HsnSacNo", typeof(int));
        dtItemgrdview.Columns.Add("GoodsServiceInd", typeof(int));
        dtItemgrdview.Columns.Add("ItemQty", typeof(decimal));
        dtItemgrdview.Columns.Add("ItemUnitID", typeof(int));
        //dtItemgrdview.Columns.Add("ItemRate", typeof(decimal));
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

    protected void btnItemAdd_Click(object sender, EventArgs e)//----ButtonItemAdd IN Gridview----
    {
        try
        {
            if (ddlItem.SelectedValue == "0")
            {
                ddlItem.Focus();
                ShowMessage("Select ItemName..!", false);
                return;
            }
            if (string.IsNullOrEmpty(txtItemAmt.Text))
            {
                txtItemAmt.Focus();
                ShowMessage("Enter Item Amount..!", false);
                return;
            }
            //dtItemgrdview = (DataTable)ViewState["grditemTax"];
            //if (dtItemgrdview != null && dtItemgrdview.Rows.Count > 0)
            //{
            //    DataRow[] rows = dtItemgrdview.Select("ItemID=" + ddlItem.SelectedItem.Value);
            //    if (rows.Count() >= 1)
            //    {
            //        ddlItem.Focus();
            //        //ClearAll();
            //        ShowMessage("This Item Name  Already Exist.", false);
            //        return;
            //    }
            //}
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
            dr["ItemID"] = CommonCls.ConvertIntZero(ddlItem.SelectedItem.Value); //;
            dr["HsnSacNo"] = lblHsnSacCode;// ;
            dr["GoodsServiceInd"] = dtItems.Rows[0]["GoodsServiceIndication"].ToString();
            dr["ItemQty"] = CommonCls.ConvertDecimalZero(txtQty.Text);
            dr["ItemUnitID"] = CommonCls.ConvertIntZero(dtItems.Rows[0]["ItemUnitID"]);
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

    void TaxCal() //----Tax Calculation------
    {

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
    void CalculateTotalAmount()//----Total Amount Calculation----
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

            txtNet.Text = (gross + taxableAmt).ToString();

            TotalAmtFormat();
        }
    }
    void clearAfterAdd()
    {
        ddlItem.SelectedValue = "0";
        txtItemAmt.Text = "";
        ddlRate.Items.Clear();
        txtRemark.Text = "";
        txtQty.Text = "";
        //ddlRate.ClearSelection();
    }
    protected void grditemTax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditItemRow")
            {
                DTrow = (DataTable)ViewState["grditemTax"];
                DataRow drItems = DTrow.Rows[rowIndex];

                ddlItem.SelectedValue = drItems["ItemID"].ToString();
                hfHsnSacNo.Value = drItems["HSNSACNo"].ToString();
                TaxRate();
                if (ddlRate.Items.FindByValue(drItems["ItemID"].ToString()) != null)
                {
                    ddlRate.SelectedValue = drItems["ItemID"].ToString();
                }

                //if (ddlRate.Items.FindByText(drItems["TaxRate"].ToString()) == null)
                //{
                //    if (!string.IsNullOrEmpty(drItems["TaxRate"].ToString()))
                //    {
                //        ddlRate.Items.Add(drItems["TaxRate"].ToString());
                //        ddlRate.SelectedValue = drItems["TaxRate"].ToString();
                //    }
                //}
                //else
                //{
                //    ddlRate.SelectedValue = drItems["ItemID"].ToString();
                //}


                //ddlRate.SelectedValue = drItems["TaxRate"].ToString();
                txtQty.Text = drItems["ItemQty"].ToString();
                txtItemAmt.Text = Convert.ToString(CommonCls.ConvertDecimalZero(drItems["ItemAmt"].ToString()) +
                    (CommonCls.ConvertDecimalZero(drItems["IGSTTaxAmt"].ToString()) + CommonCls.ConvertDecimalZero(drItems["CGSTTaxAmt"].ToString()) +
                    CommonCls.ConvertDecimalZero(drItems["SGSTTaxAmt"].ToString()) + CommonCls.ConvertDecimalZero(drItems["CESSTaxAmt"].ToString())));

                //txtItemAmt.Text = drItems["ItemAmt"].ToString();
                txtRemark.Text = drItems["ItemRemark"].ToString();

                DTrow.Rows[rowIndex].Delete();
                grditemTax.DataSource = DTrow;
                grditemTax.DataBind();
                CalculateTotalAmount();

            }

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

            if (ddlPayMode.SelectedValue == "Cheque")
            {
                if (CommonCls.ConvertIntZero(txtReceivedNo.Text) != 0)
                {
                    if (string.IsNullOrEmpty(txtReceivedDate.Text))
                    {
                        txtReceivedDate.Focus();
                        ShowMessage("Enter Cheque Date.", false);
                        return;
                    }
                    bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                    if (!ReceivedDate) // For Voucher Date Between Financial Year.
                    {
                        txtReceivedDate.Focus();
                        ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtReceivedDate.Text))
                {
                    if (CommonCls.ConvertIntZero(txtReceivedNo.Text) == 0)
                    {
                        txtReceivedNo.Focus();
                        ShowMessage("Enter Cheque No.", false);
                        return;
                    }
                    bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                    if (!ReceivedDate) // For Voucher Date Between Financial Year.
                    {
                        txtReceivedDate.Focus();
                        ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                        return;
                    }
                }
            }

            if (ddlPayMode.SelectedValue == "UTR")
            {
                if (!string.IsNullOrEmpty(txtReceivedNo.Text))
                {
                    if (string.IsNullOrEmpty(txtReceivedDate.Text))
                    {
                        txtReceivedDate.Focus();
                        ShowMessage("Enter RTGS/NEFT Date", false);
                        return;
                    }
                    bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                    if (!ReceivedDate) // For Voucher Date Between Financial Year.
                    {
                        txtReceivedDate.Focus();
                        ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtReceivedDate.Text))
                {
                    if (string.IsNullOrEmpty(txtReceivedNo.Text))
                    {
                        txtReceivedNo.Focus();
                        ShowMessage("Enter RTGS/NEFT No.", false);
                        return;
                    }
                    bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                    if (!ReceivedDate) // For Voucher Date Between Financial Year.
                    {
                        txtReceivedDate.Focus();
                        ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                        return;
                    }
                }
            }

            #region Old
            //if (!string.IsNullOrEmpty(txtReceivedNo.Text))
            //{
            //    if (string.IsNullOrEmpty(txtReceivedDate.Text))
            //    {
            //        if (ddlPayMode.SelectedValue == "Cheque")
            //        {
            //            ShowMessage("Enter Cheque Date.", false);
            //            return;
            //        }
            //        else if (ddlPayMode.SelectedValue == "UTR")
            //        {
            //            ShowMessage("Enter RTGS/NEFT Date", false);
            //            return;
            //        }
            //    }

            //    bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
            //    if (!ReceivedDate) // For Voucher Date Between Financial Year.
            //    {
            //        if (ddlPayMode.SelectedValue == "Cheque")
            //        {
            //            ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
            //            return;
            //        }
            //        else if (ddlPayMode.SelectedValue == "UTR")
            //        {
            //            ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
            //            return;
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(txtReceivedDate.Text))
            //{
            //    if (string.IsNullOrEmpty(txtReceivedNo.Text))
            //    {
            //        if (ddlPayMode.SelectedValue == "Cheque")
            //        {
            //            ShowMessage("Enter Cheque No.", false);
            //            return;
            //        }
            //        else if (ddlPayMode.SelectedValue == "UTR")
            //        {
            //            ShowMessage("Enter RTGS/NEFT No.", false);
            //            return;
            //        }
            //    }

            //    bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
            //    if (!ReceivedDate) // For Voucher Date Between Financial Year.
            //    {
            //        if (ddlPayMode.SelectedValue == "Cheque")
            //        {
            //            ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
            //            return;
            //        }
            //        else if (ddlPayMode.SelectedValue == "UTR")
            //        {
            //            ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
            //            return;
            //        }
            //    }
            //}
            #endregion

            plObjUpdAdvSuppReceived = new UpdateAdvSuppReceivedModel();
            plObjUpdAdvSuppReceived.Ind = 3;
            plObjUpdAdvSuppReceived.OrgID = GlobalSession.OrgID;
            plObjUpdAdvSuppReceived.BrID = GlobalSession.BrID;
            plObjUpdAdvSuppReceived.YrCD = GlobalSession.YrCD;
            plObjUpdAdvSuppReceived.User = GlobalSession.UserID;
            plObjUpdAdvSuppReceived.IP = GlobalSession.IP;
            plObjUpdAdvSuppReceived.VchType = Convert.ToInt32(ViewState["VchType"]);
            plObjUpdAdvSuppReceived.DocNo = CommonCls.ConvertIntZero(txtVoucherNo.Text);
            plObjUpdAdvSuppReceived.VchDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            plObjUpdAdvSuppReceived.CashBankCode = Convert.ToInt32(ddlCashBankAccount.SelectedValue);
            plObjUpdAdvSuppReceived.AccountCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue.ToString());
            plObjUpdAdvSuppReceived.GSTIN = ddlGSTINNo.SelectedItem == null ? "" : ddlGSTINNo.SelectedItem.Text;
            plObjUpdAdvSuppReceived.VchNarration = txtNarration.Text;
            plObjUpdAdvSuppReceived.NetAmount = CommonCls.ConvertDecimalZero(txtNet.Text);
            plObjUpdAdvSuppReceived.TaxAmount = CommonCls.ConvertDecimalZero(txtTaxable.Text);
            plObjUpdAdvSuppReceived.GrossAmount = CommonCls.ConvertDecimalZero(txtGross.Text);
            //plObjAdvSuppReceived.ItemID = Convert.ToInt32(ddlItem.SelectedItem.Value);
            if (ddlPayMode.SelectedValue == "Cheque")
            {
                plObjUpdAdvSuppReceived.ChequeNo = CommonCls.ConvertIntZero(txtReceivedNo.Text);
                plObjUpdAdvSuppReceived.ChequeDate = CommonCls.ConvertToDate(txtReceivedDate.Text);
            }
            else
            {
                plObjUpdAdvSuppReceived.UTRNo = txtReceivedNo.Text;
                plObjUpdAdvSuppReceived.UTRDate = CommonCls.ConvertToDate(txtReceivedDate.Text);
            }
            plObjUpdAdvSuppReceived.TblAdvItems = (DataTable)ViewState["grditemTax"];

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("YrCode"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("YrCode");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("VoucharNo"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("VoucharNo");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("VoucharDate"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("VoucharDate");
            }

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("AccountCode"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("AccountCode");
            }

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("AccName"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("AccName");
            }

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("GSTIN"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("GSTIN");
            }

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("PurchaseSaleCode"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("PurchaseSaleCode");
            }

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("AccName1"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("AccName1");
            }

            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("ChequeNo"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("ChequeNo");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("ChequeDate"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("ChequeDate");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("UTRNo"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("UTRNo");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("UTRDate"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("UTRDate");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("Narration"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("Narration");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("GrossAmount"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("GrossAmount");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("TaxAmount"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("TaxAmount");
            }
            if (plObjUpdAdvSuppReceived.TblAdvItems.Columns.Contains("NetAmount"))
            {
                plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("NetAmount");
            }

            plObjUpdAdvSuppReceived.TblAdvItems.Columns["CompanyID"].SetOrdinal(0);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["BranchID"].SetOrdinal(1);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemName"].SetOrdinal(2);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemUnit"].SetOrdinal(3);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemID"].SetOrdinal(4);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["HSNSACNo"].SetOrdinal(5);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["GoodsServiceInd"].SetOrdinal(6);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemQty"].SetOrdinal(7);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemUnitID"].SetOrdinal(8);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemRate"].SetOrdinal(9);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemAmt"].SetOrdinal(10);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["TaxRate"].SetOrdinal(11);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["IGSTTax"].SetOrdinal(12);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["IGSTTaxAmt"].SetOrdinal(13);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["SGSTTax"].SetOrdinal(14);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["SGSTTaxAmt"].SetOrdinal(15);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["CGSTTax"].SetOrdinal(16);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["CGSTTaxAmt"].SetOrdinal(17);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["CESSTax"].SetOrdinal(18);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["CESSTaxAmt"].SetOrdinal(19);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ItemRemark"].SetOrdinal(20);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns["ExtraInd"].SetOrdinal(21);
            plObjUpdAdvSuppReceived.TblAdvItems.Columns.Remove("CancelInd");

            string uri = string.Format("UpdateSupplierAdvReceived/SaveProcess");
            DataTable dtSaveing = CommonCls.ApiPostDataTable(uri, plObjUpdAdvSuppReceived);
            if (dtSaveing.Rows.Count > 0)
            {
                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtSaveing.Rows[0]["DocMaxNo"].ToString() + " - " + Convert.ToDateTime(dtSaveing.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                ShowMessage("Data SucessFully Saved", true);

                SavingFinalClear();
            }
            else
            {
                ShowMessage("Data Not Save.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e) //---------Button Clear------ 
    {
        ClearAll();
    }
    public void SavingFinalClear()//----------------Clear After Saving-------------------------
    {
        txtVoucherDate.Text = "";
        txtAccountHead.ClearSelection();
        ddlItem.ClearSelection();
        txtItemAmt.Text = "";
        ViewState["grditemTax"] = null;
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
        txtVoucherNo.Enabled = true;
        btngo.Enabled = true;
        txtVoucherNo.Text = "";
        clearAfterAdd();
        txtVoucherNo.Focus();
        EnabledControl();
        btnSave.Enabled = false;
    }

    public void ClearAll()
    {
        txtVoucherDate.Text = "";
        txtAccountHead.ClearSelection();
        ddlItem.ClearSelection();
        txtItemAmt.Text = "";
        grditemTax.DataSource = null;
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
        txtVoucherNo.Enabled = true;
        btngo.Enabled = true;
        txtVoucherNo.Text = "";
        clearAfterAdd();
        txtVoucherNo.Focus();
        EnabledControl();
        btnSave.Enabled = false;
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
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
            plObjUpdAdvSuppReceived = new UpdateAdvSuppReceivedModel();
            plObjUpdAdvSuppReceived.Ind = 12;
            plObjUpdAdvSuppReceived.OrgID = GlobalSession.OrgID;
            plObjUpdAdvSuppReceived.BrID = GlobalSession.BrID;
            plObjUpdAdvSuppReceived.VchType = 13;
            plObjUpdAdvSuppReceived.DocNo = Convert.ToInt32(txtVoucherNo.Text);
            plObjUpdAdvSuppReceived.CancelReason = ddlCancelReason.SelectedItem.Text;


            string uri = string.Format("UpdateSupplierAdvReceived/CancelVoucher");

            DataTable dtSave = CommonCls.ApiPostDataTable(uri, plObjUpdAdvSuppReceived);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ClearAll();
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar No. - " + plObjUpdAdvSuppReceived.DocNo + " is Cancel successfully ", true);
                }
                else if (dtSave.Rows[0]["CancelInd"].ToString() == "0")
                {
                    ClearAll();
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel  ", true);
                }
            }
            else
            {
                ShowMessage("Voucher Is Not Cancelled", false);
            }
        }
        catch (Exception)
        {
            pnlConfirmInvoice.Visible = false;
            ShowMessage("Record Not Cancel Please Try Again.", false);
        }

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        txtCancelReason.Text = "";
    }
}