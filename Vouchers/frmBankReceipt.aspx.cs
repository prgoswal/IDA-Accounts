using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class frmBankReciept : System.Web.UI.Page
{

    DataTable dtgrdview, dtAccGstin;
    BankReceiptModel plbankrec;

    DataTable VsdtLastVoucherDate
    {
        get { return (DataTable)ViewState["dtLastVoucherDate"]; }
        set { ViewState["dtLastVoucherDate"] = value; }
    }

    DataTable VSBudgetHead
    {
        get { return (DataTable)ViewState["VSBudgetHead"]; }
        set { ViewState["VSBudgetHead"] = value; }
    }
    DataTable VSServiceList
    {
        get { return (DataTable)ViewState["VSServiceList"]; }
        set { ViewState["VSServiceList"] = value; }
    }


    DataTable VSPlotServiceNo
    {

        get { return (DataTable)ViewState["VSPlotServiceNo"]; }
        set { ViewState["VSPlotServiceNo"] = value; }
    }

    DataTable VSPlotServiceDetails
    {

        get { return (DataTable)ViewState["VSPlotServiceDetails"]; }
        set { ViewState["VSPlotServiceDetails"] = value; }
    }

    int ServiceNoTaken
    {
        get { return (int)ViewState["ServiceNoTaken"]; }
        set { ViewState["ServiceNoTaken"] = value; }
    }

    public static string strcon = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
    ////create new sqlconnection and connection to database by using connection string from web.config file  
    SqlConnection Con = new SqlConnection(strcon);

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (GlobalSession.YrCD == null)
        {
            Response.Redirect("frmLogin.aspx");
        }
        if (!IsPostBack)
        {
            ServiceNoTaken = 0;
            txtVoucherDate.Focus();
            ViewState["VchType"] = 3;

            BindAllBankReceiptDDL();
            //LoadBankAccount();
            //LoadAccountHead();
            //LoadNarration();
            //LoadLastVoucherNo();

            //----------------
            //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
            //{
            //    btnSave.Visible = false;

            //}
            //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
            //{
            //    btnSave.Visible = true;
            //}

            SetPayMode();
        }



    }

    private void BindServiceDetail()
    {
        try
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * From MstPlot", Con);
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dtPlotServiceDetails = new DataTable();
            Adpt.Fill(dtPlotServiceDetails);
            ViewState["VSPlotServiceDetails"] = VSPlotServiceDetails = dtPlotServiceDetails;
        }
        catch (Exception)
        {
            ShowMessage("This DataBase is not available Please contact to system administrator", false);
            return;
        }
        finally
        {
            Con.Close();
        }
    }



    void BindAllBankReceiptDDL()
    {
        try
        {
            plbankrec = new BankReceiptModel();
            plbankrec.Ind = 11;
            plbankrec.OrgID = GlobalSession.OrgID;
            plbankrec.BrID = GlobalSession.BrID;
            plbankrec.YrCD = GlobalSession.YrCD;
            plbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);


            string uri = string.Format("BankReceipt/BindAllBankReceiptDDL");
            DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plbankrec);
            if (dsBindAllCRDDL.Tables.Count > 0)
            {
                DataTable dtAccountHead = dsBindAllCRDDL.Tables[0];
                DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];
                DataTable dtNarration = dsBindAllCRDDL.Tables[2];
                DataTable dtCashAccount = dsBindAllCRDDL.Tables[3];
                DataTable dtCostCentre = dsBindAllCRDDL.Tables[4];

                DataTable dtServiceList = dsBindAllCRDDL.Tables[5];
                DataTable dtServiceNo = dsBindAllCRDDL.Tables[6];
                ViewState["dtsevicelist"] = VSServiceList = dtServiceList;
                //ViewState["VSServiceNo"] = VSServiceNo = dtServiceNo;

                // Account Head List Bind
                if (dtAccountHead.Rows.Count > 0)
                {
                    ViewState["dtAccGstin"] = VSBudgetHead = dtAccountHead;
                    DataView dvAccCode = new DataView(dtAccountHead);
                    DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

                    txtAccountHead.DataSource = dtAccList;
                    txtAccountHead.DataTextField = "AccName";
                    txtAccountHead.DataValueField = "AccCode";
                    txtAccountHead.DataBind();
                    txtAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));


                }

                // Narrations List Bind
                //if (dtNarration.Rows.Count > 0)
                //{
                //    txtNarration.DataSource = dtNarration;
                //    txtNarration.DataTextField = "NarrationDesc";
                //    txtNarration.DataBind();
                //}

                // Cash List Bind
                if (dtCashAccount.Rows.Count > 0)
                {
                    ddlBankAccount.DataSource = dtCashAccount;
                    ddlBankAccount.DataTextField = "AccName";
                    ddlBankAccount.DataValueField = "AccCode";
                    ddlBankAccount.DataBind();
                    if (dtCashAccount.Rows.Count > 1)
                        ddlBankAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
                }

                // Last Voucher No And Date
                if (dtLastVoucher.Rows.Count > 0)
                {
                    if (CommonCls.ConvertIntZero(dtLastVoucher.Rows[0]["LastNo"]) != 0)
                    {
                        txtLastVoucherNo.Text = dtLastVoucher.Rows[0][0].ToString();
                        hfLastVoucherDate.Value = CommonCls.ConvertDateDB(dtLastVoucher.Rows[0]["LastDate"]);
                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucher.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtLastVoucher.Rows[0]["LastDate"]);
                    }
                }

                if (GlobalSession.CCCode == 1)
                {
                    thCCCode.Visible = true;
                    tdCCCode.Visible = true;
                    // Cost Centre
                    if (dtCostCentre.Rows.Count > 0)
                    {
                        ddlCostCentre.DataSource = dtCostCentre;
                        ddlCostCentre.DataTextField = "CostCentreName";
                        ddlCostCentre.DataValueField = "CostCentreID";
                        ddlCostCentre.DataBind();
                        ddlCostCentre.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #region Old

    //void LoadLastVoucherNo() // Last Voucher No Insert
    //{
    //    try
    //    {
    //        plbankrec = new BankReceiptModel()
    //        {
    //            Ind = 7,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };


    //        string uri = string.Format("BankReceipt/LastVoucherNo");
    //        DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, plbankrec);
    //        if (dtVoucher.Rows.Count > 0)
    //        {
    //            if (dtVoucher.Rows[0]["LastNo"].ToString() == "0")
    //            {
    //                return;
    //            }
    //            txtLastVoucherNo.Text = dtVoucher.Rows[0][0].ToString();
    //            lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucher.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtVoucher.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}

    //void LoadNarration() // Narrations List Bind
    //{
    //    try
    //    {
    //        plbankrec = new BankReceiptModel()
    //        {
    //            Ind = 6,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankReceipt/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, plbankrec);
    //        if (NarrationList.Rows.Count > 0)
    //        {
    //            txtNarration.DataSource = NarrationList;
    //            txtNarration.DataTextField = "NarrationDesc";
    //            txtNarration.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}

    //void LoadAccountHead() // Account Head List Bind
    //{
    //    try
    //    {
    //        plbankrec = new BankReceiptModel()
    //        {
    //            Ind = 5,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankReceipt/AccountHead");
    //        DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, plbankrec);
    //        if (dtAccGstin.Rows.Count > 0)
    //        {
    //            ViewState["dtAccGstin"] = dtAccGstin;
    //            DataView dvAccCode = new DataView(dtAccGstin);
    //            DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

    //            txtAccountHead.DataSource = dtAccList;
    //            txtAccountHead.DataTextField = "AccName";
    //            txtAccountHead.DataValueField = "AccCode";
    //            txtAccountHead.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}



    //void LoadBankAccount() // Bank List Bind
    //{
    //    try
    //    {
    //        plbankrec = new BankReceiptModel()
    //        {
    //            Ind = 4,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankReceipt/LoadBankAccount");
    //        DataTable dtBankAccount = CommonCls.ApiPostDataTable(uri, plbankrec);
    //        if (dtBankAccount.Rows.Count > 0)
    //        {
    //            ddlBankAccount.DataSource = dtBankAccount;
    //            ddlBankAccount.DataTextField = "AccName";
    //            ddlBankAccount.DataValueField = "AccCode";
    //            ddlBankAccount.DataBind();

    //            if (dtBankAccount.Rows.Count > 1)
    //                ddlBankAccount.Items.Insert(0, new ListItem("-- Select --", "0000"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}

    #endregion

    DataTable CreatGridDt() // Create Grid Structure
    {
        dtgrdview = new DataTable();
        dtgrdview.Columns.Add("AccHeadValue", typeof(string));
        dtgrdview.Columns.Add("AcctHeadText", typeof(string));
        dtgrdview.Columns.Add("PartyID", typeof(string));
        dtgrdview.Columns.Add("BillNos", typeof(string));
        dtgrdview.Columns.Add("GSTIN", typeof(string));
        dtgrdview.Columns.Add("InvoiceNo", typeof(string));
        dtgrdview.Columns.Add("InvoiceDate", typeof(string));
        dtgrdview.Columns.Add("Amount", typeof(string));
        dtgrdview.Columns.Add("DrCr", typeof(string));
        dtgrdview.Columns.Add("PartyName", typeof(string));
        //dtgrdview.Columns.Add("serviceNo", typeof(string));
        dtgrdview.Columns.Add("IsCapitalRevenue", typeof(string));
        return dtgrdview;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation

            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }

            if (!string.IsNullOrEmpty(txtInVoiceNo.Text))
            {
                if (string.IsNullOrEmpty(txtInvoiceDate.Text))
                {
                    txtInvoiceDate.Focus();
                    ShowMessage("Please Select Demand Date.", false);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Demand Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                    txtInvoiceDate.Focus();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtInvoiceDate.Text))
            {
                if (string.IsNullOrEmpty(txtInVoiceNo.Text))
                {
                    txtInVoiceNo.Focus();
                    ShowMessage("Please Enter Invoice No.", false);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Demand Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                    txtInvoiceDate.Focus();
                    return;
                }
            }



            //Comented by ashish sir 16-02-2018 11:56

            //DataTable dtGRD = VSBudgetHead;
            //if (dtGRD.Rows.Count > 0)
            //{
            //    DataView dv31 = new DataView(dtGRD);

            //    dv31.RowFilter = "AccCode  = " + txtAccountHead.SelectedValue + "";
            //    if (CommonCls.ConvertIntZero(dv31.ToTable().Rows[0]["MainGroupID"].ToString()) == 25 || CommonCls.ConvertIntZero(dv31.ToTable().Rows[0]["MainGroupID"].ToString()) == 24)
            //    {
            //        bool BudgetHeadStatus = CheckBudgetHead();
            //        if (BudgetHeadStatus == true)
            //        {
            //            if (ddlCostCentre.SelectedValue == "0")
            //            {
            //                ShowMessage("Select Cost Centre.", false);
            //                ddlCostCentre.Focus();
            //                return;
            //            }
            //        }

            //    }
            //}

            //******************Service Number Concept Start  (Check service number if availabe the set attribute otherwise add)

            DataTable dtAddServiceList = VSServiceList;
            DataView dvAddServiceList = new DataView(dtAddServiceList);

            dvAddServiceList.RowFilter = "Acccode = " + txtAccountHead.SelectedValue + "";

            if (dvAddServiceList.ToTable().Rows.Count > 0)
            {
                if (ServiceNoTaken == 1)
                {
                    ShowMessage("Two Control Account is not add on one Bank Receipt", false);
                    return;
                }

                if (CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) == CommonCls.ConvertIntZero(dvAddServiceList.ToTable().Rows[0]["AccCode"].ToString()))
                {
                    txtServiceNo.MaxLength = CommonCls.ConvertIntZero(dvAddServiceList.ToTable().Rows[0]["Length"].ToString());
                    if (dvAddServiceList.ToTable().Rows[0]["Type"].ToString() == "Number")
                    {
                        txtServiceNo.CssClass = "inpt Money";
                        ServiceNoTaken = 1; //This set for acccode is availabe otherwise 0 
                    }
                }
            }

            // Disable Select If Alerady Exist AccountHead
            string InvoiceNo = txtInVoiceNo.Text;
            dtgrdview = (DataTable)ViewState["grdData"];
            if (dtgrdview != null && dtgrdview.Rows.Count > 0)
            {
                DataRow[] rows = dtgrdview.Select("AccHeadValue=" + txtAccountHead.SelectedItem.Value + " AND  InvoiceNo='" + InvoiceNo + "'");
                if (rows.Count() >= 1)
                {
                    if (InvoiceNo == string.Empty)
                    {
                        txtAccountHead.Focus();
                        ShowMessage("This Account Head With Blank Or 0 Invoice No. Already Exist.", false);
                        return;
                    }

                    txtAccountHead.Focus();
                    ClearAll();
                    ShowMessage("This Account Head With Given Invoice No. Already Exist.", false);
                    return;
                }
            }

            #endregion


            if (ViewState["grdData"] == null)
            {
                CreatGridDt();
            }
            else
            {
                dtgrdview = (DataTable)ViewState["grdData"];
            }

            DataRow dr = dtgrdview.NewRow();
            dr["AcctHeadText"] = txtAccountHead.SelectedItem.Text;
            dr["AccHeadValue"] = txtAccountHead.SelectedItem.Value;
            //dr["GSTIN"] = string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value) ? "" : ddlGSTINNo.SelectedItem.Value;

            dr["PartyID"] = ddlSecondaryParty.Items.Count > 0 ?
                                !string.IsNullOrEmpty(ddlSecondaryParty.SelectedItem.Value) ?
                                ddlSecondaryParty.SelectedItem.Value : "" : "";

            string BillNos = "";
            for (int i = 0; i < CbOutstandingBill.Items.Count; i++)
            {
                if (CbOutstandingBill.Items[i].Selected)
                {
                    if (i == 0)
                        BillNos = CbOutstandingBill.Items[i].Value;
                    else
                        BillNos += "," + CbOutstandingBill.Items[i].Value;
                }
            }

            dr["BillNos"] = BillNos;
            dr["InvoiceNo"] = InvoiceNo;
            dr["InvoiceDate"] = txtInvoiceDate.Text;
            dr["Amount"] = txtAmount.Text;
            dr["DrCr"] = ddlCrOrDr.SelectedItem.Value;
            dr["PartyName"] = txtPartyName.Text;
            //dr["serviceNo"] = txtServiceNo.Text;

            if (ddlCapitalRevenue.SelectedValue == "0")
            {
                dr["IsCapitalRevenue"] = "";
            }
            else
            {
                dr["IsCapitalRevenue"] = ddlCapitalRevenue.SelectedValue;
            }
            dtgrdview.Rows.Add(dr);
            ViewState["grdData"] = dtgrdview;
            gvBankReceipt.DataSource = dtgrdview;
            gvBankReceipt.DataBind();
            CalculateTotalInvoiceAmount();
            ClearAll();
            txtAccountHead.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private bool CheckBudgetHead()
    {
        plbankrec = new BankReceiptModel();
        plbankrec.Ind = 12;
        plbankrec.OrgID = GlobalSession.OrgID;
        plbankrec.BrID = GlobalSession.BrID;
        plbankrec.YrCD = GlobalSession.YrCD;
        plbankrec.AccCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue);
        string uri = string.Format("BankReceipt/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, plbankrec);
        if (dtCashReceipt.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtCashReceipt.Rows[0]["Cnt"].ToString()) > 0)
            {
                return true;
            }
        }
        return false;
    }

    void LastVoucherDate()
    {
        plbankrec = new BankReceiptModel();
        plbankrec.Ind = 11;
        plbankrec.OrgID = GlobalSession.OrgID;
        plbankrec.BrID = GlobalSession.BrID;
        plbankrec.YrCD = GlobalSession.YrCD;
        plbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);

        string uri = string.Format("BankReceipt/BindAllBankReceiptDDL");
        DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plbankrec);
        if (dsBindAllCRDDL.Tables.Count > 0)
        {
            DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //btnSave.Enabled = false;

            #region ServiceNo
            //if (ServiceNoTaken == 1)
            //{

            //    if (string.IsNullOrEmpty(txtServiceNo.Text)) // For Voucher Date Not Be Null
            //    {
            //        txtServiceNo.Enabled = true;
            //        txtServiceNo.Focus();
            //        ShowMessage("Enter Service No", false);
            //        return;
            //    }
            //    else
            //    {
            //        bool IsStatusServiceNo = MatchServiceNo();
            //        if (IsStatusServiceNo == false)
            //        {
            //            ShowMessage("Master Informetion of given Service No" + txtServiceNo.Text + " Not Available. Do You Want To Proceed.", false);
            //            txtServiceNo.Focus();
            //            txtPartyName.Text = txtPartyAddress.Text = txtpartyGstIN.Text = "";
            //            return;
            //        }
            //    }
            //}

            if (ServiceNoTaken == 1)
            {
                if (string.IsNullOrEmpty(txtPartyName.Text))
                {
                    txtServiceNo.Enabled = true;
                    txtServiceNo.Focus();
                    ShowMessage("Invalid Service Number Please search first.", false);
                    return;
                }
            }


            #endregion
            //DateTime dateTime = DateTime.ParseExact(txtVoucherDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); //server
            //dateTime = dateTime.AddDays(-89); //server

            //if (chkBankEntry.Checked == false)
            //{
            //    if (!string.IsNullOrEmpty(hfLastVoucherDate.Value))
            //    {
            //        DateTime VoucherDate = DateTime.ParseExact(txtVoucherDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        DateTime lastVoucherDate = DateTime.ParseExact(hfLastVoucherDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture); //server

            //        if (VoucherDate < lastVoucherDate)
            //        {
            //            txtVoucherDate.Focus();
            //            ShowMessage("Please Use Back Date Entry Option.", false);
            //            return;
            //        }
            //    }
            //}







            if (ViewState["grdData"] == null || ((DataTable)ViewState["grdData"]).Rows.Count <= 0)
            {
                txtVoucherDate.Focus();
                ShowMessage("Insert Voucher Details!", false);
                return;
            }


            //By Ashish sir 16-02-2018 12.00

            if (string.IsNullOrEmpty(txtInvoiceTotalAmount.Text) || Convert.ToDecimal(txtInvoiceTotalAmount.Text) <= 0) //Invoice Total Amount.
            {
                ShowMessage("Voucher Amount Invalid Please Check Your Entries.", false);
                return;
            }
            //if (string.IsNullOrEmpty(txtVoucherDate.Text))
            //{
            //    txtVoucherDate.Focus();
            //    ShowMessage("Enter Voucher Date!", false);
            //    return;
            //}

            bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate)
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Should Be Within Financial Year Date!", false);
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

                //bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, (dateTime).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy")); //server
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
                //bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, (dateTime).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));//server
                bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));//local


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


            dtgrdview = (DataTable)ViewState["grdData"];
            DataTable dtbank = BankRecieptSchema();

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow dr = dtbank.NewRow();                                                  //Data Activity Indication
                dr["OrgID"] = GlobalSession.OrgID; //Company/End User ID
                dr["BrID"] = GlobalSession.BrID;   //Branch ID
                dr["VchType"] = 3;                              //Document/Voucher Type
                dr["YrCD"] = GlobalSession.YrCD;  //Financial Year Code (2017-17)
                dr["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date
                // dr["DocNo"] = string.IsNullOrEmpty(txtLastVoucherNo.Text) ? 0 : Convert.ToInt32(txtLastVoucherNo.Text);

                if (chkBankEntry.Checked == true)//if Back Date Entry is True
                {

                    dr["DocNo"] = CommonCls.ConvertIntZero(txtVoucherNo.Text);   //Voucher No.
                }
                else
                {
                    dr["DocNo"] = 0;   //For Current Voucher
                }

                dr["AccCode"] = Convert.ToInt32(item["AccHeadValue"]);  //Selected Account Head Code
                dr["AccGst"] = item["GSTIN"].ToString();                         //Selected Account Head GSTIN No. (If Available)
                dr["AccCode2"] = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);  //Selected Bank Account Code

                dr["RefNo"] = 0;
                dr["RefDate"] = "";

                if (item["DrCr"].ToString() == "Cr")
                    dr["AmountCr"] = Convert.ToDecimal(item["Amount"]);  //Dr Amount
                else
                    dr["AmountDr"] = Convert.ToDecimal(item["Amount"]);  //Cr Amount	
                dr["AdvanceInd"] = 0;//item["DrCr"] == "Dr" ? 1 : 0;    //AgainstAdvanceInd (If Receipt Made For Against Advance Taken From Debitor/Creditor Then It Goes 1 Else 0)

                if (ddlPayMode.SelectedValue == "Cheque")
                {
                    dr["ChequeNo"] = string.IsNullOrEmpty(txtReceivedNo.Text) ? 0 : Convert.ToInt64(txtReceivedNo.Text);
                    dr["ChequeDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    dr["UTRNo"] = txtReceivedNo.Text;
                    dr["UTRDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
                }

                dr["DocDesc"] = txtNarration.Text;                      //Narration
                dr["EntryType"] = 1;//                                  //EntryType - 1-Entry/2-Ammendment/3-Cancel
                dr["User"] = GlobalSession.UserID;  //UserID - User ID (Entry By Which User)
                dr["IP"] = GlobalSession.IP;                           //IPAddress - Client Machine IP Address
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(item["IsCapitalRevenue"]) == 0 ? 0 : item["IsCapitalRevenue"];

                dr["BillNo"] = item["InvoiceNo"].ToString(); //Invoice No.
                dr["BillDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : ""; //Invoice Date

                //dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(ddlCapitalRevenue.SelectedValue);

                //dr["BillNos"] = item["BillNos"].ToString();
                //if (Convert.ToInt32(item["PartyID"].ToString()) > 0)
                //{
                //    dr["PartyID"] = item["PartyID"].ToString();
                //}

                dtbank.Rows.Add(dr);
            }

            plbankrec = new BankReceiptModel();
            plbankrec.Ind = 1;
            plbankrec.OrgID = GlobalSession.OrgID;
            plbankrec.BrID = GlobalSession.BrID;
            plbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);
            plbankrec.YrCD = GlobalSession.YrCD;
            plbankrec.CCCode = CommonCls.ConvertIntZero(ddlCostCentre.SelectedValue);
            plbankrec.PartyName = txtPartyName.Text;
            plbankrec.PartyAddress = txtPartyAddress.Text;
            plbankrec.PartyGstIN = txtpartyGstIN.Text;
            plbankrec.ServiceNo = CommonCls.ConvertIntZero(txtServiceNo.Text);
            if (!string.IsNullOrEmpty(txtIDARefNo.Text))
            {
                plbankrec.IDARefNo = "Old VNo-" + txtIDARefNo.Text;
            }

            plbankrec.Dt = JsonConvert.SerializeObject(dtbank);

            string uri = string.Format("BankReceipt/SaveBankReceipt");
            DataTable dtSaveBankReceipt = CommonCls.ApiPostDataTable(uri, plbankrec);
            if (dtSaveBankReceipt.Rows.Count > 0)
            {
                if (dtSaveBankReceipt.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ddlBankAccount.ClearSelection();
                    txtInvoiceTotalAmount.Text = "0";
                    txtReceivedDate.Text = txtReceivedNo.Text = txtVoucherDate.Text = txtNarration.Text = "";
                    ddlPayMode.ClearSelection();
                    gvBankReceipt.DataSource = null;
                    gvBankReceipt.DataBind();
                    ViewState["grdData"] = null;
                    ClearAll();
                    ClearOnSave();
                    //LoadLastVoucherNo();
                    chkBankEntry.Checked = false;
                    chkBankEntry_CheckedChanged(sender, e);
                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveBankReceipt.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = CommonCls.ConvertDateDB(dtSaveBankReceipt.Rows[0]["DocDate"]);

                    ShowMessage("Record Save Successfully With Voucher No. " + VoucherNo, true);
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date " + VoucherNo + " - " + VoucherDate;

                    hfLastVoucherDate.Value = VoucherDate;

                    CallReport(VoucherNo, VoucherDate);
                    txtVoucherDate.Focus();


                }

                else if (dtSaveBankReceipt.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("No Record Found For Given Vouchar No." + txtVoucherNo.Text + " & Date" + txtVoucherDate.Text, false);
                    txtVoucherNo.Focus();
                }

                else if (dtSaveBankReceipt.Rows[0]["ReturnInd"].ToString() == "3")
                {
                    ShowMessage(" Back Date Vouchar Already Available For Given Vouchar No." + txtVoucherNo.Text + " & Date." + txtVoucherDate.Text + " Please Give Correct Vouchar No. ", false);
                    txtVoucherNo.Focus();
                }
                else
                {
                    ShowMessage("Record Not Save Please Try Again.", false);
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage("Error :" + ex.Message, false);
        }

        btnSave.Enabled = true;
    }

    //protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
    //        {
    //            txtAccountHead.Focus();
    //            ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
    //            return;
    //        }

    //        if (Convert.ToInt32(txtAccountHead.SelectedItem.Value) <= 0)
    //        {
    //            ShowMessage("Account Head Not Available", false);
    //            return;
    //        }

    //        //if (CommonCls.ConvertIntZero(txtAccountHead.SelectedItem.Value) < 700000)
    //        //{
    //        //    dtAccGstin = (DataTable)(ViewState["dtAccGstin"]);
    //        //    if (dtAccGstin != null)
    //        //    {
    //        //        DataView DVAccGstin = new DataView(dtAccGstin);
    //        //        DVAccGstin.RowFilter = "AccCode=" + txtAccountHead.SelectedItem.Value;
    //        //        DataTable DTFilter = DVAccGstin.ToTable();

    //        //        if ((DTFilter.Rows.Count > 0) && (DTFilter.Rows.Count == 1))
    //        //        {
    //        //            ddlGSTINNo.DataSource = DTFilter;
    //        //            ddlGSTINNo.DataTextField = "GSTIN";
    //        //            ddlGSTINNo.DataBind();
    //        //            ddlGSTINNo.SelectedIndex = 0;
    //        //        }
    //        //        else if (DTFilter.Rows.Count > 1)
    //        //        {
    //        //            ddlGSTINNo.DataSource = DTFilter;
    //        //            ddlGSTINNo.DataTextField = "GSTIN";
    //        //            ddlGSTINNo.DataBind();
    //        //            ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0000"));
    //        //            ddlGSTINNo.SelectedIndex = 0;
    //        //            ddlGSTINNo.Focus();
    //        //        }
    //        //        else
    //        //        {
    //        //            ddlGSTINNo.DataSource = null;
    //        //            ddlGSTINNo.DataBind();
    //        //        }
    //        //    }
    //        //    plbankrec = new BankReceiptModel()
    //        //    {
    //        //        Ind = 6,
    //        //        OrgID = GlobalSession.OrgID,
    //        //        BrID = GlobalSession.BrID,
    //        //        YrCD = GlobalSession.YrCD,
    //        //        VchType = Convert.ToInt32(ViewState["VchType"]),
    //        //        AccCode = Convert.ToInt32(txtAccountHead.SelectedItem.Value),
    //        //    };
    //        //    string uri = string.Format("BankReceipt/PartySelect");
    //        //    DataSet dsPartySelect = CommonCls.ApiPostDataSet(uri, plbankrec);
    //        //    if (dsPartySelect.Tables.Count > 0)
    //        //    {
    //        //        divPartySelect.Visible = true; //Party Selection Section Visible
    //        //        btnRegToggle.Visible = false;   //Outstanding Bill UnVisible
    //        //        ddlSecondaryParty.Visible = false; //SecondaryParty UnVisible

    //        //        if (dsPartySelect.Tables[0].TableName == "SecondaryParty") //SecondaryParty Visible
    //        //        {
    //        //            ddlSecondaryParty.Visible = true;
    //        //            ddlSecondaryParty.DataSource = dsPartySelect;
    //        //            ddlSecondaryParty.DataTextField = "PartyName";
    //        //            ddlSecondaryParty.DataValueField = "PartyID";
    //        //            ddlSecondaryParty.DataBind();
    //        //        }
    //        //        else if (dsPartySelect.Tables[0].TableName == "OutstandingBill") //Outstanding Bill Visible
    //        //        {
    //        //            btnRegToggle.Visible = true;
    //        //            CbOutstandingBill.DataSource = dsPartySelect;
    //        //            CbOutstandingBill.DataTextField = "BillAmt";
    //        //            CbOutstandingBill.DataValueField = "BillNo";
    //        //            CbOutstandingBill.DataBind();
    //        //        }
    //        //        else
    //        //        {
    //        //            divPartySelect.Visible = false;
    //        //            ddlSecondaryParty.DataSource = CbOutstandingBill.DataSource = null;
    //        //            ddlSecondaryParty.DataBind(); CbOutstandingBill.DataBind();
    //        //        }
    //        //    }
    //        //    ddlGSTINNo.Focus();
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(lblMsg.Text, false);
    //    }
    //}

    protected void gvBankReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtgrdview = (DataTable)ViewState["grdData"];
                dtgrdview.Rows[rowIndex].Delete();

                ViewState["grdData"] = dtgrdview;
                gvBankReceipt.DataSource = dtgrdview;
                gvBankReceipt.DataBind();

                CalculateTotalInvoiceAmount();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(lblMsg.Text, false);
        }
    }

    void CalculateTotalInvoiceAmount()
    {
        decimal crAmount = 0;
        decimal drAmount = 0;
        for (int rowIndex = 0; dtgrdview.Rows.Count > rowIndex; rowIndex++)
        {
            string DrCr = dtgrdview.Rows[rowIndex][8].ToString();
            if (DrCr == "Cr")
            {
                crAmount = crAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex][7]);
            }
            else
            {
                drAmount = drAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex][7]);
            }
        }
        txtInvoiceTotalAmount.Text = Convert.ToString(crAmount - drAmount);
    }
    void ClearOnSave()
    {
        txtServiceNo.Enabled = false;
        ServiceNoTaken = 0;
        ddlCostCentre.ClearSelection();
        txtIDARefNo.Text = "";

    }


    void ClearAll() // Clear Selection On Add Click.
    {
        txtInVoiceNo.Text =
        txtInvoiceDate.Text =
        txtAmount.Text =
        lblMsg.Text =
         string.Empty;
        ddlGSTINNo.DataSource = new DataTable(); ddlGSTINNo.DataBind();
        ddlCrOrDr.ClearSelection();
        // txtServiceNo.Text = "";
        txtAccountHead.ClearSelection();
        txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = txtNarration.Text = "";
        //ddlCostCentre.ClearSelection();
        //ServiceNoTaken = 0;

        //txtPartyName.Text = string.Empty;
        //txtPartyAddress.Text = string.Empty;
        //txtpartyGstIN.Text = string.Empty;
    }

    DataTable BankRecieptSchema()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("OrgID", typeof(int));
        dt.Columns.Add("BrID", typeof(int));
        dt.Columns.Add("VchType", typeof(int));
        dt.Columns.Add("YrCD", typeof(int));
        dt.Columns.Add("DocDate", typeof(string));
        dt.Columns.Add("DocNo", typeof(int));
        dt.Columns.Add("AccCode", typeof(int));

        //dt.Columns.Add("PartyID", typeof(int));
        //dt.Columns.Add("BillNos", typeof(string));

        dt.Columns.Add("AccGst", typeof(string));
        dt.Columns.Add("AccCode2", typeof(int));
        dt.Columns.Add("RefNo", typeof(int));
        dt.Columns.Add("RefDate", typeof(string));
        dt.Columns.Add("AmountDr", typeof(decimal));
        dt.Columns.Add("AmountCr", typeof(decimal));
        dt.Columns.Add("AdvanceInd", typeof(int));
        dt.Columns.Add("ChequeNo", typeof(int));
        dt.Columns.Add("ChequeDate", typeof(string));
        dt.Columns.Add("UTRNo", typeof(string));
        dt.Columns.Add("UTRDate", typeof(string));
        dt.Columns.Add("DocDesc", typeof(string));
        dt.Columns.Add("EntryType", typeof(int));
        dt.Columns.Add("User", typeof(int));
        dt.Columns.Add("IP", typeof(string));
        dt.Columns.Add("IsCapitalRevenue", typeof(int));
        dt.Columns.Add("BillNo", typeof(string));
        dt.Columns.Add("BillDate", typeof(string));

        return dt;
    }

    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPayMode();
        txtReceivedNo.Focus();
    }

    void SetPayMode()
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtServiceNo.Enabled = false;
            ClearAll();
            gvBankReceipt.DataSource = ViewState["grdData"] = null;
            gvBankReceipt.DataBind();
            chkBankEntry.Checked = false;
            chkBankEntry_CheckedChanged(sender, e);
            txtVoucherDate.Text =
                txtReceivedDate.Text =
                txtReceivedNo.Text =
                txtInvoiceTotalAmount.Text = "";
            ddlBankAccount.ClearSelection();
            txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = txtNarration.Text = "";
            ServiceNoTaken = 0;
            ddlCostCentre.ClearSelection();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    public void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }

    void CallReport(string VoucherNo, string VoucherDate)
    {
        Hashtable HT = new Hashtable();
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "Bank Receipt");
        HT.Add("Ind", 1);
        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);

        HT.Add("DocTypeID", Convert.ToInt16(ViewState["VchType"]));
        HT.Add("Voucharno", VoucherNo);
        HT.Add("VoucharDate", VoucherDate.Substring(6, 4) + "/" + VoucherDate.Substring(3, 2) + "/" + VoucherDate.Substring(0, 2));
        //Session["HT"] = HT;
        //Session["format"] = "Pdf";
        //Session["FileName"] = "Voucher";
        //Session["Report"] = "RptVoucher";
        //Response.Redirect("FrmReportViewer.aspx");


        VouchersReport.ReportName = "RptVoucher";
        VouchersReport.FileName = "Voucher";
        VouchersReport.ReportHeading = "Bank Receipt";
        VouchersReport.HashTable = HT;
        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print Receipt?";
        VouchersReport.ShowReport();
    }


    protected void chkBankEntry_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBankEntry.Checked == true)
        {
            thVNo.Visible = true;
            tdVNo.Visible = true;
        }
        else
        {
            txtVoucherNo.Text = "";
            thVNo.Visible = false;
            tdVNo.Visible = false;
        }
    }


    private bool MatchServiceNo()
    {
        try
        {
            int serviceNo = 0;
            DataTable dtServiceNumber = VSPlotServiceNo;
            DataView dvServiceno = new DataView(dtServiceNumber);
            dvServiceno.RowFilter = "ServiceNo = " + txtServiceNo.Text + "";
            if (dvServiceno.ToTable().Rows.Count > 0)
            {
                serviceNo = CommonCls.ConvertIntZero(dvServiceno.ToTable().Rows[0]["ServiceNo"].ToString());

                DataView dv31 = new DataView(VSPlotServiceDetails);
                dv31.RowFilter = "ServiceNo  = " + serviceNo + "";

                // txtPartyName.Enabled = txtPartyAddress.Enabled = txtpartyGstIN.Enabled = true;
                //txtPartyName.Text = dv31.ToTable().Rows[0]["PlotHolderNameE"].ToString();
                //txtPartyAddress.Text = dv31.ToTable().Rows[0]["CAddress1"].ToString();
                //txtpartyGstIN.Text = dv31.ToTable().Rows[0]["GSTNo"].ToString();
                return true;
            }
            else
            {

                ShowMessage("Master Informetion of given Service No" + txtServiceNo.Text + " Not Available. Do You Want To Proceed.", false);
                txtServiceNo.Focus();
                txtPartyName.Text = txtPartyAddress.Text = txtpartyGstIN.Text = "";
                return false;
            }
        }
        catch (Exception)
        {
            ShowMessage("Internal server error.", false);
            return false;
        }
    }
    private bool BindPloteServiceNo()
    {
        try
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from mstplotserviceNo", Con);
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dtPlotServiceNo = new DataTable();
            Adpt.Fill(dtPlotServiceNo);
            ViewState["VSPlotServiceNo"] = VSPlotServiceNo = dtPlotServiceNo;
            Con.Close();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
        return false;
    }

    protected void btnServiceNo_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtServiceNo.Text)) // For Voucher Date Not Be Null
            {
                txtServiceNo.Focus();
                ShowMessage("Enter Service No", false);
                return;
            }
            bool IsStatusServiceNo = BindPloteServiceNo();
            if (IsStatusServiceNo == false)
            {
                ShowMessage("This DataBase is not available Please contact to system administrator", false);
                return;
            }
            BindServiceDetail();

            MatchServiceNo();
        }
        catch (Exception)
        {
            ShowMessage("Internal server error.", false);
        }
    }

}