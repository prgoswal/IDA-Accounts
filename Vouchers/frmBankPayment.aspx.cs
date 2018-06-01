using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class frmBankPayment : System.Web.UI.Page
{
    #region Declaration

    DataTable dtgrdview, dtAccGstin;
    BankPaymentModel plbankpay;

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

    DataTable VsDtChequeSeries
    {
        get { return (DataTable)ViewState["DtChequeSeries"]; }
        set { ViewState["DtChequeSeries"] = value; }
    }

    DataTable VsdtSalary
    {
        get { return (DataTable)ViewState["dtSalary"]; }
        set { ViewState["dtSalary"] = value; }
    }

    DataTable VsdtLease
    {
        get { return (DataTable)ViewState["dtLease"]; }
        set { ViewState["dtLease"] = value; }
    }

    DataTable VsdtRent
    {
        get { return (DataTable)ViewState["dtRent"]; }
        set { ViewState["dtRent"] = value; }
    }

    int EmpNoTaken  //This Is Used In (ServiceNoAccordingToAccountCode) Method And (gvBankPayment_RowCommand) GridView Row Command
    {
        get { return (int)ViewState["EmpNoTaken"]; }
        set { ViewState["EmpNoTaken"] = value; }
    }

    int AccTypeInd  //This Is Used In (ServiceNoAccordingToAccountCode) Method And (gvBankPayment_RowCommand) GridView Row Command
    {
        get { return (int)ViewState["AccTypeInd"]; }
        set { ViewState["AccTypeInd"] = value; }
    }

    int IncDec  //Increment-Decrement According To Salary,Lease And Rent In (ServiceNoAccordingToAccountCode) Method And (gvBankPayment_RowCommand) GridView Row Command
    {
        get { return (int)ViewState["IncDec"]; }
        set { ViewState["IncDec"] = value; }
    }

    int ServiceID   //This Is Used In (ServiceNoAccordingToAccountCode) Method And (gvBankPayment_RowCommand) GridView Row Command
    {
        get { return (int)ViewState["ServiceID"]; }
        set { ViewState["ServiceID"] = value; }
    }

    public static string strcon = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
    ////create new sqlconnection and connection to database by using connection string from web.config file  
    SqlConnection con = new SqlConnection(strcon);

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ServiceNoTaken = EmpNoTaken = AccTypeInd = IncDec = 0;
            txtVoucherDate.Focus();
            ViewState["VchType"] = 4;

            BindAllBankPaymentDDL();
            SetPayMode();
        }

    }

    void BindAllBankPaymentDDL()
    {
        try
        {
            plbankpay = new BankPaymentModel();
            plbankpay.Ind = 11;
            plbankpay.OrgID = GlobalSession.OrgID;
            plbankpay.BrID = GlobalSession.BrID;
            plbankpay.YrCD = GlobalSession.YrCD;
            plbankpay.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("BankPayment/BindAllBankPaymentDDL");
            DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plbankpay);
            if (dsBindAllCRDDL.Tables.Count > 0)
            {
                DataTable dtAccountHead = dsBindAllCRDDL.Tables[0];
                DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];
                DataTable dtNarration = dsBindAllCRDDL.Tables[2];
                DataTable dtCashAccount = dsBindAllCRDDL.Tables[3];
                DataTable dtCostCentre = dsBindAllCRDDL.Tables[4];
                DataTable dtServiceList = dsBindAllCRDDL.Tables[5];
                VsDtChequeSeries = dsBindAllCRDDL.Tables[7];

                VsdtSalary = dsBindAllCRDDL.Tables[8];
                VsdtLease = dsBindAllCRDDL.Tables[9];
                VsdtRent = dsBindAllCRDDL.Tables[10];

                ViewState["dtsevicelist"] = VSServiceList = dtServiceList;

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
                        lblInvoiceAndDate.Text = "Last Transaction No. & Date : " + dtLastVoucher.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtLastVoucher.Rows[0]["LastDate"]);
                    }
                }

                // Cost Centre
                if (GlobalSession.CCCode == 1)
                {
                    thCCCode.Visible = true;
                    tdCCCode.Visible = true;
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
    //        plbankpay = new BankPaymentModel()
    //        {
    //            Ind = 7,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankPayment/LastVoucherNo");
    //        DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, plbankpay);
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
    //        plbankpay = new BankPaymentModel()
    //        {
    //            Ind = 6,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankPayment/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, plbankpay);
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
    //        plbankpay = new BankPaymentModel()
    //        {
    //            Ind = 5,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankPayment/AccountHead");
    //        DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, plbankpay);
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
    //        plbankpay = new BankPaymentModel()
    //        {
    //            Ind = 4,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("BankPayment/LoadBankAccount");
    //        DataTable dtBankAccount = CommonCls.ApiPostDataTable(uri, plbankpay);
    //        if (dtBankAccount.Rows.Count > 0)
    //        {
    //            ddlBankAccount.DataSource = dtBankAccount;
    //            ddlBankAccount.DataTextField = "AccName";
    //            ddlBankAccount.DataValueField = "AccCode";
    //            ddlBankAccount.DataBind();
    //            if (dtBankAccount.Rows.Count > 1)
    //                ddlBankAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
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
        dtgrdview.Columns.Add("IsCapitalRevenue", typeof(string));
        dtgrdview.Columns.Add("IsCapitalRevenuenName", typeof(string));

        return dtgrdview;
    }

    bool ServiceNoAccordingToAccountCode(DataTable dtSalary, DataTable dtLease, DataTable dtRent, int AccCode)
    {
        int returnInd = 0;
        int accCode1 = 0;

        DataTable dt = CommonCls.ServiceNoAccordingToAccountCode(VsdtSalary, VsdtLease, VsdtRent, CommonCls.ConvertIntZero(txtAccountHead.SelectedValue));

        if (dt != null && dt.Rows.Count > 0)
        { 
            returnInd = CommonCls.ConvertIntZero(dt.Rows[0]["ReturnInd"].ToString());
            accCode1 = CommonCls.ConvertIntZero(dt.Rows[0]["AccCode"].ToString());
        }

        if (returnInd == 1) //For Salary
        {
            if (AccTypeInd == 2 || AccTypeInd == 3)
            {
                if (EmpNoTaken == 1)
                {
                    ShowMessage("Two Control Account is not add on one Bank Payment", false);
                    return false;
                }
            }
            else if (AccCode == accCode1)
            {
                AccTypeInd = ServiceID = 1;
                ServiceNoEnabledTrue();
                return true;
            }
        }
        else if (returnInd == 2) //For Lease
        {
            if (AccTypeInd == 1 || AccTypeInd == 3)
            {
                if (EmpNoTaken == 1)
                {
                    ShowMessage("Two Control Account is not add on one Bank Payment", false);
                    return false;
                }
            }
            else if (AccCode == accCode1)
            {
                AccTypeInd = ServiceID = 2;
                ServiceNoEnabledTrue();
                return true;
            }
        }
        else if (returnInd == 3) //For Rent
        {
            if (AccTypeInd == 1 || AccTypeInd == 2)
            {
                if (EmpNoTaken == 1)
                {
                    ShowMessage("Two Control Account is not add on one Bank Payment", false);
                    return false;
                }
            }
            else if (AccCode == accCode1)
            {
                AccTypeInd = ServiceID = 3;
                ServiceNoEnabledTrue();
                return true;
            }
        }
        return true;
    }

    void ServiceNoEnabledTrue()
    {
        EmpNoTaken = 1;
        txtServiceNo.Enabled = true;
        IncDec = IncDec + 1;
    }


    #region Old

    //bool ServiceNoAccordingToAccountCode(DataTable dtSalary, DataTable dtLease, DataTable dtRent, int AccCode)
    //{
    //    //Salary Code
    //    DataView dvSalary = new DataView(dtSalary);
    //    dvSalary.RowFilter = "AccCode=" + AccCode + "";

    //    //Lease Code
    //    DataView dvLease = new DataView(dtLease);
    //    dvLease.RowFilter = "AccCode=" + AccCode + "";

    //    //Rent Code
    //    DataView dvRent = new DataView(dtRent);
    //    dvRent.RowFilter = "AccCode=" + AccCode + "";

    //    if (dvSalary.ToTable().Rows.Count > 0)
    //    {
    //        if (AccTypeInd == 2 || AccTypeInd == 3)
    //        {
    //            if (EmpNoTaken == 1)
    //            {
    //                ShowMessage("Two Control Account is not add on one Bank Payment", false);
    //                return false;
    //            }
    //        }
    //        else if (AccCode == CommonCls.ConvertIntZero(dvSalary.ToTable().Rows[0]["AccCode"].ToString()))
    //        {
    //            AccTypeInd = ServiceID = 1;
    //            ServiceNoEnabledTrue();
    //            return true;
    //        }
    //    }
    //    else if (dvLease.ToTable().Rows.Count > 0)
    //    {
    //        if (AccTypeInd == 1 || AccTypeInd == 3)
    //        {
    //            if (EmpNoTaken == 1)
    //            {
    //                ShowMessage("Two Control Account is not add on one Bank Payment", false);
    //                return false;
    //            }
    //        }
    //        else if (AccCode == CommonCls.ConvertIntZero(dvLease.ToTable().Rows[0]["AccCode"].ToString()))
    //        {
    //            AccTypeInd = ServiceID = 2;
    //            ServiceNoEnabledTrue();
    //            return true;
    //        }
    //    }
    //    else if (dvRent.ToTable().Rows.Count > 0)
    //    {
    //        if (AccTypeInd == 1 || AccTypeInd == 2)
    //        {
    //            if (EmpNoTaken == 1)
    //            {
    //                ShowMessage("Two Control Account is not add on one Bank Payment", false);
    //                return false;
    //            }
    //        }
    //        else if (AccCode == CommonCls.ConvertIntZero(dvRent.ToTable().Rows[0]["AccCode"].ToString()))
    //        {
    //            AccTypeInd = ServiceID = 3;
    //            ServiceNoEnabledTrue();
    //            return true;
    //        }
    //    }
    //    return true;
    //}

    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }

            if (!string.IsNullOrEmpty(txtInVoiceNo.Text))  // Invoice Amount Not Null
            {
                if (string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Copulsory On InvoiceNo
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

            if (!string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Not Null
            {
                if (string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Copulsory On InvoiceDate
                {
                    txtInVoiceNo.Focus();
                    txtInVoiceNo.Text = "";
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
                    ShowMessage("Two Control Account is not add on one Bank Payment", false);
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

            if (!ServiceNoAccordingToAccountCode(VsdtSalary, VsdtLease, VsdtRent, CommonCls.ConvertIntZero(txtAccountHead.SelectedValue)))
                return;

            
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
                        ShowMessage("This Account Head With Blank Or 0 Bill No. Already Exist.", false);
                        return;
                    }

                    txtAccountHead.Focus();
                    ClearAll();
                    ShowMessage("This Account Head With Given Bill No. Already Exist.", false);
                    return;
                }
            }

            if (ViewState["grdData"] == null) // First Time DataTable Create For Grid
            {
                CreatGridDt();
            }
            else
            {
                dtgrdview = (DataTable)ViewState["grdData"];
            }

            DataRow dr = dtgrdview.NewRow();
            dr["AcctHeadText"] = txtAccountHead.SelectedItem.Text;
            dr["AccHeadValue"] = txtAccountHead.SelectedValue;

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

            if (ddlCapitalRevenue.SelectedValue == "0")
            {
                dr["IsCapitalRevenue"] = "";
                dr["IsCapitalRevenuenName"] = "";
            }
            else
            {
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(ddlCapitalRevenue.SelectedValue);
                dr["IsCapitalRevenuenName"] = ddlCapitalRevenue.SelectedItem.Text;
            }

            dtgrdview.Rows.Add(dr);
            gvBankPayment.DataSource = ViewState["grdData"] = dtgrdview;
            gvBankPayment.DataBind();

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
        plbankpay = new BankPaymentModel();
        plbankpay.Ind = 12;
        plbankpay.OrgID = GlobalSession.OrgID;
        plbankpay.BrID = GlobalSession.BrID;
        plbankpay.YrCD = GlobalSession.YrCD;
        plbankpay.AccCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue);
        string uri = string.Format("BankPayment/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, plbankpay);
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
        plbankpay = new BankPaymentModel();
        plbankpay.Ind = 11;
        plbankpay.OrgID = GlobalSession.OrgID;
        plbankpay.BrID = GlobalSession.BrID;
        plbankpay.YrCD = GlobalSession.YrCD;
        plbankpay.VchType = Convert.ToInt32(ViewState["VchType"]);

        string uri = string.Format("BankPayment/BindAllBankPaymentDDL");
        DataSet dsBindAllBPDDL = CommonCls.ApiPostDataSet(uri, plbankpay);
        if (dsBindAllBPDDL.Tables.Count > 0)
        {
            DataTable dtLastVoucher = dsBindAllBPDDL.Tables[1];

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
            lblMsg.Text = lblMsg.CssClass = "";
            btnSave.Enabled = false;

            if (!ValidationOnBtnSave())
            {
                btnSave.Enabled = true;
                return;
            }

            dtgrdview = (DataTable)ViewState["grdData"];
            DataTable dtbank = BankPaymentSchema();

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow dr = dtbank.NewRow();
                dr["OrgID"] = GlobalSession.OrgID;        //Company/End User ID
                dr["BrID"] = GlobalSession.BrID;          //Branch ID
                dr["VchType"] = Convert.ToInt32(ViewState["VchType"]);    //Document/Voucher Type
                dr["YrCD"] = GlobalSession.YrCD;          //Financial Year Code (2017-17)
                dr["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date

                if (chkBankEntry.Checked == true)//if Back Date Entry is True
                {

                    dr["DocNo"] = CommonCls.ConvertIntZero(txtVoucherNo.Text);   //Voucher No.
                }
                else
                {
                    dr["DocNo"] = 0;   //For Current Voucher
                }

                dr["AccCode"] = Convert.ToInt32(item["AccHeadValue"]);  //Selected Account Head Code
                dr["AccGst"] = item["GSTIN"].ToString();                //Selected Account Head GSTIN No. (If Available)
                dr["AccCode2"] = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);  //Selected Bank Account Code

                dr["RefNo"] = 0; //Invoice No.
                dr["RefDate"] = ""; //Invoice Date

                if (item["DrCr"].ToString() == "Cr")
                    dr["AmountCr"] = Convert.ToDecimal(item["Amount"]);  //Dr Amount
                else
                    dr["AmountDr"] = Convert.ToDecimal(item["Amount"]);  //Cr Amount	


                if (ddlPayMode.SelectedValue == "Cheque")
                {
                    dr["ChequeNo"] = !string.IsNullOrEmpty(txtReceivedNo.Text) ? Convert.ToInt64(txtReceivedNo.Text) : 0;
                    dr["ChequeDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    dr["UTRNo"] = txtReceivedNo.Text;
                    dr["UTRDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
                }

                dr["AdvanceInd"] = 0;//item["DrCr"] == "Dr" ? 1 : 0;     //AgainstAdvanceInd (If Receipt Made For Against Advance Taken From Debitor/Creditor Then It Goes 1 Else 0)
                dr["DocDesc"] = txtNarration.Text;                       //Narration
                dr["EntryType"] = 1;//                                   //EntryType - 1-Entry/2-Ammendment/3-Cancel
                dr["User"] = GlobalSession.UserID;         //UserID - User ID (Entry By Which User)
                dr["IP"] = GlobalSession.IP;                     //IPAddress - Client Machine IP Address
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(item["IsCapitalRevenue"]) == 0 ? 0 : item["IsCapitalRevenue"];

                dr["BillNo"] = item["InvoiceNo"].ToString(); //Invoice No.
                dr["BillDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : ""; //Invoice Date

                dtbank.Rows.Add(dr);
            }

            plbankpay = new BankPaymentModel();
            plbankpay.Ind = 1;
            plbankpay.OrgID = GlobalSession.OrgID;
            plbankpay.BrID = GlobalSession.BrID;
            plbankpay.VchType = Convert.ToInt32(ViewState["VchType"]);
            plbankpay.YrCD = GlobalSession.YrCD;
            plbankpay.CCCode = CommonCls.ConvertIntZero(ddlCostCentre.SelectedValue);
            plbankpay.DeptID = GlobalSession.DepartmentID;
            plbankpay.SubDeptID = GlobalSession.SubDeptID;
            plbankpay.PartyName = txtPartyName.Text;
            plbankpay.PartyAddress = txtPartyAddress.Text;
            plbankpay.PartyGstIN = txtpartyGstIN.Text;
            plbankpay.ServiceNo = CommonCls.ConvertIntZero(txtServiceNo.Text);
            plbankpay.User = GlobalSession.UserID;
            plbankpay.IsFinal = 0;
            plbankpay.ChequeDrawn = txtChequeDrawn.Text;

            if (!string.IsNullOrEmpty(txtIDARefNo.Text))
            {
                plbankpay.IDARefNo = "Old VNo-" + txtIDARefNo.Text;
            }


            //  plbankpay.Dt = dtbank;
            plbankpay.Dt = JsonConvert.SerializeObject(dtbank);

            string uri = string.Format("BankPayment/SaveBankPayment");
            DataTable dtSaveBankPayment = CommonCls.ApiPostDataTable(uri, plbankpay);
            if (dtSaveBankPayment.Rows.Count > 0)
            {
                if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ddlBankAccount.ClearSelection();
                    txtInvoiceTotalAmount.Text = "0";
                    txtReceivedDate.Text = txtReceivedNo.Text = txtVoucherDate.Text = txtNarration.Text = txtChequeDrawn.Text = "";
                    ViewState["grdData"] = null;
                    ddlPayMode.ClearSelection();
                    gvBankPayment.DataSource = null;
                    gvBankPayment.DataBind();
                    txtVoucherDate.Focus();
                    ClearAll();
                    ClearOnSave();
                    chkBankEntry.Checked = false;
                    chkBankEntry_CheckedChanged(sender, e);
                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSaveBankPayment.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                    ShowMessage("Record Save Successfully With Transaction No. " + VoucherNo, true);
                    lblInvoiceAndDate.Text = "Last Transaction No. & Date " + VoucherNo + " - " + VoucherDate;

                    hfLastVoucherDate.Value = VoucherDate;

                    CallReport(VoucherNo, VoucherDate);
                    txtVoucherDate.Focus();
                }



                else if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("No Record Found For Given Transaction No." + txtVoucherNo.Text + " & Date" + txtVoucherDate.Text, false);
                    txtVoucherNo.Focus();
                }

                else if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "3")
                {
                    ShowMessage(" Back Date Transaction Already Available For Given Transaction No." + txtVoucherNo.Text + " & Date." + txtVoucherDate.Text + " Please Give Correct Transaction No. ", false);
                    txtVoucherNo.Focus();
                }
                //else if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "2")
                //{
                //    ShowMessage("Duplicate Cheque No. Please Check.", false);
                //}
                else
                {
                    ShowMessage("Record Not Save Please Try Again.", false);
                }
            }
            else
            {
                ShowMessage("Record Not Save.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        btnSave.Enabled = true;

    }

    bool ValidationOnBtnSave()
    {
        #region ServiceNo
        if (ServiceNoTaken == 1)
        {
            if (string.IsNullOrEmpty(txtPartyName.Text))
            {
                txtServiceNo.Enabled = true;
                txtServiceNo.Focus();
                ShowMessage("Invalid Service Number Please search first.", false);
                return false;
            }
        }
        #endregion

        if (ViewState["grdData"] == null || ((DataTable)ViewState["grdData"]).Rows.Count <= 0)
        {
            txtVoucherDate.Focus();
            ShowMessage("Insert Transaction Details!", false);
            return false;
        }

        //By Ashish sir 16-02-2018 12.00

        if (string.IsNullOrEmpty(txtInvoiceTotalAmount.Text) || Convert.ToDecimal(txtInvoiceTotalAmount.Text) <= 0) //Invoice Total Amount.
        {
            ShowMessage("Transaction Amount Invalid Please Check Your Entries.", false);
            return false;
        }


        bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtVoucherDate.Focus();
            ShowMessage("Transaction Date Should Be Within Financial Year Date!", false);
            return false;
        }

        if (GlobalSession.BankPayChqSeriesInd == 1)
        {
            if (ddlPayMode.SelectedValue == "Cheque")
            {
                if (string.IsNullOrEmpty(txtReceivedNo.Text))
                {
                    ShowMessage("Enter Cheque No.", false);
                    return false;
                }
            }

            if (ddlPayMode.SelectedValue == "Cheque")
            {
                if (!string.IsNullOrEmpty(txtReceivedNo.Text))
                {
                    //DataRow[] row = VsDtChequeSeries.Select("BankCode='" + ddlBankAccount.SelectedValue + "' And ((chequeFrom<='" + txtReceivedNo.Text + "' And chequeto>='" + txtReceivedNo.Text + "') Or (chequeFrom>='" + txtReceivedNo.Text + "' And chequeto<='" + txtReceivedNo.Text + "'))");
                    DataRow[] row = VsDtChequeSeries.Select("BankCode='" + ddlBankAccount.SelectedValue + "' And ((chequeFrom<='" + txtReceivedNo.Text + "' And chequeto>='" + txtReceivedNo.Text + "'))");
                    if (row.Count() <= 0)
                    {
                        ShowMessage("Cheque Series Not Available.", false);
                        txtReceivedNo.Focus();
                        return false;
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(txtReceivedNo.Text))
        {
            if (string.IsNullOrEmpty(txtReceivedDate.Text))
            {
                if (ddlPayMode.SelectedValue == "Cheque")
                {
                    ShowMessage("Enter Cheque Date.", false);
                    return false;
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    ShowMessage("Enter RTGS/NEFT Date", false);
                    return false;
                }
            }

            bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));//local
            if (!ReceivedDate) // For Voucher Date Between Financial Year.
            {
                if (ddlPayMode.SelectedValue == "Cheque")
                {
                    ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                    return false;
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                    return false;
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
                    return false;
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    ShowMessage("Enter RTGS/NEFT No.", false);
                    return false;
                }
            }

            bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ReceivedDate) // For Voucher Date Between Financial Year.
            {
                if (ddlPayMode.SelectedValue == "Cheque")
                {
                    ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                    return false;
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                    return false;
                }
            }
        }

        if (string.IsNullOrEmpty(txtNarration.Text))
        {
            txtNarration.Focus();
            ShowMessage("Enter Narration.", false);
            return false;
        }

        return true;
    }

    void ClearOnSave()
    {
        ServiceNoTaken = EmpNoTaken = 0;
        txtServiceNo.Enabled = false;
        ddlCostCentre.ClearSelection();
    }

    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtAccountHead.SelectedItem.Value) <= 0)
            {
                ShowMessage("Account Head Not Valid", false);
                return;
            }

            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }



            if (CommonCls.ConvertIntZero(txtAccountHead.SelectedItem.Value) < 700000)
            {
                dtAccGstin = (DataTable)(ViewState["dtAccGstin"]);
                DataView DVAccGstin = new DataView(dtAccGstin);
                DVAccGstin.RowFilter = "AccCode=" + txtAccountHead.SelectedItem.Value;
                DataTable DTFilter = DVAccGstin.ToTable();

                if ((DTFilter.Rows.Count > 0) && (DTFilter.Rows.Count == 1))
                {
                    ddlGSTINNo.DataSource = DTFilter;
                    ddlGSTINNo.DataTextField = "GSTIN";
                    ddlGSTINNo.DataBind();
                    ddlGSTINNo.SelectedIndex = 0;
                }
                else if (DTFilter.Rows.Count > 1)
                {
                    ddlGSTINNo.DataSource = DTFilter;
                    ddlGSTINNo.DataTextField = "GSTIN";
                    ddlGSTINNo.DataBind();
                    ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0"));
                    ddlGSTINNo.SelectedIndex = 0;
                    ddlGSTINNo.Focus();
                }
                else
                {
                    ddlGSTINNo.DataSource = null;
                    ddlGSTINNo.DataBind();
                }

                plbankpay = new BankPaymentModel()
                {
                    Ind = 6,
                    OrgID = GlobalSession.OrgID,
                    BrID = GlobalSession.BrID,
                    YrCD = GlobalSession.YrCD,
                    VchType = Convert.ToInt32(ViewState["VchType"]),
                    AccCode = Convert.ToInt32(txtAccountHead.SelectedItem.Value),
                };

                string uri = string.Format("BankPayment/PartySelect");
                DataSet dsPartySelect = CommonCls.ApiPostDataSet(uri, plbankpay);
                if (dsPartySelect.Tables.Count > 0)
                {
                    divPartySelect.Visible = true; //Party Selection Section Visible
                    btnRegToggle.Visible = false;   //Outstanding Bill UnVisible
                    ddlSecondaryParty.Visible = false; //SecondaryParty UnVisible

                    if (dsPartySelect.Tables[0].TableName == "SecondaryParty") //SecondaryParty Visible
                    {
                        ddlSecondaryParty.Visible = true;
                        ddlSecondaryParty.DataSource = dsPartySelect;
                        ddlSecondaryParty.DataTextField = "PartyName";
                        ddlSecondaryParty.DataValueField = "PartyID";
                        ddlSecondaryParty.DataBind();
                        ddlBankAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                    else if (dsPartySelect.Tables[0].TableName == "OutstandingBill") //Outstanding Bill Visible
                    {
                        btnRegToggle.Visible = true;
                        CbOutstandingBill.DataSource = dsPartySelect;
                        CbOutstandingBill.DataTextField = "BillAmt";
                        CbOutstandingBill.DataValueField = "BillNo";
                        CbOutstandingBill.DataBind();
                    }
                    else
                    {
                        divPartySelect.Visible = false;
                        ddlSecondaryParty.DataSource = CbOutstandingBill.DataSource = null;
                        ddlSecondaryParty.DataBind(); CbOutstandingBill.DataBind();
                    }
                }
                ddlGSTINNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ServiceNoEnabledFalse()
    {
        IncDec = CommonCls.ConvertIntZero(IncDec - 1);
        if (IncDec == 0)
        {
            txtServiceNo.Enabled = false;
            EmpNoTaken = AccTypeInd = ServiceID = 0;
        }
    }

    protected void gvBankPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtgrdview = (DataTable)ViewState["grdData"];
                //DataRow dr = dtgrdview.Rows[rowIndex];
                //int AccCode = CommonCls.ConvertIntZero(dr["AccHeadValue"].ToString());

                //if (AccTypeInd == 1)    //For Salary
                //{
                //    DataRow[] row = VsdtSalary.Select("AccCode='" + AccCode + "'");
                //    if (row.Count() > 0)
                //    {
                //        ServiceNoEnabledFalse();
                //    }
                //}
                //else if (AccTypeInd == 2)   //For Lease
                //{
                //    DataRow[] row = VsdtLease.Select("AccCode='" + AccCode + "'");
                //    if (row.Count() > 0)
                //    {
                //        ServiceNoEnabledFalse();
                //    }
                //}
                //else if (AccTypeInd == 3)   //For Rent
                //{
                //    DataRow[] row = VsdtRent.Select("AccCode='" + AccCode + "'");
                //    if (row.Count() > 0)
                //    {
                //        ServiceNoEnabledFalse();
                //    }
                //}

                if (AccTypeInd == 1 || AccTypeInd == 2 || AccTypeInd == 3)  // 1 For Salary, 2 Lease And 3 Form Rent
                    ServiceNoEnabledFalse();

                dtgrdview.Rows[rowIndex].Delete();

                ViewState["grdData"] = dtgrdview;
                gvBankPayment.DataSource = dtgrdview;
                gvBankPayment.DataBind();

                CalculateTotalInvoiceAmount();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void CalculateTotalInvoiceAmount() // For Invoice Amount Dr - CR
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
        txtInvoiceTotalAmount.Text = Convert.ToString(drAmount - crAmount);
    }

    void ClearAll() // Clear Selection On Add Click.
    {
        txtInVoiceNo.Text =
        txtInvoiceDate.Text =
        txtAmount.Text =
        lblMsg.Text = string.Empty;
        ddlGSTINNo.DataSource = new DataTable(); 
        ddlGSTINNo.DataBind();
        ddlCrOrDr.ClearSelection();
        txtAccountHead.ClearSelection();
        btnSave.Enabled = true;
    }

    DataTable BankPaymentSchema()
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtServiceNo.Enabled = false;
            ClearAll();
            chkBankEntry.Checked = false;
            chkBankEntry_CheckedChanged(sender, e);
            gvBankPayment.DataSource = ViewState["grdData"] = null;
            gvBankPayment.DataBind();
            txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = txtNarration.Text = txtVoucherDate.Text =
                txtReceivedDate.Text = txtReceivedNo.Text = txtInvoiceTotalAmount.Text = txtChequeDrawn.Text = "";
            ddlBankAccount.ClearSelection();
            ddlCostCentre.ClearSelection();
            ServiceNoTaken = EmpNoTaken = 0;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
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

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //    object sender = UpdatePanel1;
        //    Message = Message.Replace("'", "");
        //    Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }

    void CallReport(string VoucherNo, string VoucherDate)
    {
        Hashtable HT = new Hashtable();
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "Bank Payment");
        HT.Add("Ind", 2);
        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", 0);//GlobalSession.YrCD);

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
        VouchersReport.ReportHeading = "Bank Payment";
        VouchersReport.HashTable = HT;
        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print?";
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

    //private bool MatchServiceNo()
    //{
    //    try
    //    {
    //        int serviceNo = 0;
    //        DataTable dtServiceNumber = VSPlotServiceNo;
    //        DataView dvServiceno = new DataView(dtServiceNumber);
    //        dvServiceno.RowFilter = "ServiceNo = " + txtServiceNo.Text + "";
    //        if (dvServiceno.ToTable().Rows.Count > 0)
    //        {
    //            serviceNo = CommonCls.ConvertIntZero(dvServiceno.ToTable().Rows[0]["ServiceNo"].ToString());

    //            DataView dv31 = new DataView(VSPlotServiceDetails);
    //            dv31.RowFilter = "ServiceNo  = " + serviceNo + "";
    //            txtPartyName.Text = dv31.ToTable().Rows[0]["PlotHolderNameE"].ToString();
    //            txtPartyAddress.Text = dv31.ToTable().Rows[0]["CAddress1"].ToString();
    //            txtpartyGstIN.Text = dv31.ToTable().Rows[0]["GSTNo"].ToString();
    //            return true;
    //        }
    //        else
    //        {

    //            ShowMessage("Master Informetion of given Service No" + txtServiceNo.Text + " Not Available. Do You Want To Proceed.", false);
    //            txtServiceNo.Focus();
    //            txtPartyName.Text = txtPartyAddress.Text = txtpartyGstIN.Text = "";
    //            return false;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ShowMessage("Internal server error.", false);
    //        return false;
    //    }
    //}

    //private void BindServiceDetail()
    //{
    //    try
    //    {
    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand("Select * From MstPlot", con);
    //        SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
    //        DataTable dtPlotServiceDetails = new DataTable();
    //        Adpt.Fill(dtPlotServiceDetails);
    //        ViewState["VSPlotServiceDetails"] = VSPlotServiceDetails = dtPlotServiceDetails;
    //    }
    //    catch (Exception)
    //    {
    //        ShowMessage("This DataBase is not available Please contact to system administrator", false);
    //        return;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}
    //private bool BindPloteServiceNo()
    //{
    //    try
    //    {
    //        if (con.State == ConnectionState.Closed)
    //        {
    //            con.Open();
    //        }
    //        SqlCommand cmd = new SqlCommand("select * from mstplotserviceNo", con);
    //        SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
    //        DataTable dtPlotServiceNo = new DataTable();
    //        Adpt.Fill(dtPlotServiceNo);
    //        ViewState["VSPlotServiceNo"] = VSPlotServiceNo = dtPlotServiceNo;
    //        con.Close();
    //        return true;
    //    }
    //    catch (Exception)
    //    {

    //        return false;
    //    }
    //    return false;
    //}

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

            VSPlotServiceNo = CommonCls.dtSearchServiceNo(CommonCls.ConvertIntZero(txtServiceNo.Text), ServiceID);

            if (VSPlotServiceNo.Rows.Count > 0)
            {
                txtPartyName.Text = VSPlotServiceNo.Rows[0]["PlotHolderNameE"].ToString();
                txtPartyAddress.Text = VSPlotServiceNo.Rows[0]["CAddress1"].ToString();
                txtpartyGstIN.Text = VSPlotServiceNo.Rows[0]["GSTNo"].ToString();
            }
            else
            {
                txtPartyName.Text = txtPartyAddress.Text = txtpartyGstIN.Text = "";
                ShowMessage("Master Informetion of given Service No" + txtServiceNo.Text + " Not Available.", false);
                return;
            }

            //bool IsStatusServiceNo = BindPloteServiceNo();
            //if (IsStatusServiceNo == false)
            //{
            //    ShowMessage("This DataBase is not available Please contact to system administrator", false);
            //    return;
            //}
            //BindServiceDetail();

            //MatchServiceNo();

        }
        catch (Exception)
        {
            ShowMessage("Internal server error.", false);
        }
    }
}
