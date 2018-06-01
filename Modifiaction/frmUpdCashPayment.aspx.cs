using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modifiaction_frmUpdCashPayment : System.Web.UI.Page
{
    #region Declaration

    DataTable dtgrdview, dtAccGstin;

    UpdCashPaymentModel plUpdcashpay;

    int RowIndex
    {
        get { return ViewState["rowIndex"] == null ? -1 : Convert.ToInt16(ViewState["rowIndex"]); }
        set { ViewState["rowIndex"] = value; }
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

    string Narration
    {
        get { return (string)ViewState["Narration"]; }
        set { ViewState["Narration"] = value; }
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
            ServiceNoTaken = 0;
            txtVoucherDate.Focus();
            ViewState["VchType"] = 2;
            BindAllCashPaymentDDL();
            BindCancelReason();
            txtSearchVNo.Focus();

            if (Session["TransNo"] != null && Session["TransDate"] != null)
            {
                txtSearchVNo.Text = Session["TransNo"].ToString();
                btnGo_Click(sender, e);

                if (Session["UpdAndView"] == "Upd")
                {
                    if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                    {
                        btnCancel.Visible = btnApproved.Visible = false;
                        //btnApproved.Visible = false;
                    }
                    else if (GlobalSession.DesignationID == 26)
                    {
                        btnCancel.Visible = true;
                        btnApproved.Visible = false;
                    }
                    else
                    {
                        btnCancel.Visible = btnApproved.Visible = false;
                    }
                }
                else if (Session["UpdAndView"] == "View")
                {
                    if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DesignationID > 0)
                    {
                        if (Session["IsSendToAudit"] != null)
                        {
                            if (Session["IsSendToAudit"].ToString() == "1")
                                btnSave.Visible = false;
                        }

                        btnCancel.Visible = btnApproved.Visible = false;
                    }
                    else
                        btnCancel.Visible = btnApproved.Visible = btnSave.Visible = false;
                }
            }

            //if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
            //{
            //    btnSave.Visible = true;
            //    btnCancel.Visible = true;
            //}
            //if (Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || GlobalSession.IsAdmin == 1)
            //{
            //    btnSave.Visible = true;
            //}
            //LoadCashAccount();
            //LoadAccountHead();
            //LoadNarration();
            //LoadLastVoucherNo();
        }
    }
    private void BindServiceDetail()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * From MstPlot", con);
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dtPlotServiceDetails = new DataTable();
            Adpt.Fill(dtPlotServiceDetails);
            ViewState["VSPlotServiceDetails"] = VSPlotServiceDetails = dtPlotServiceDetails;
        }
        catch (Exception)
        {
            ShowMessage("This DataBase is not available Please contact to system administrator", false, lblMsg);
            return;
        }
        finally
        {
            con.Close();
        }
    }


    private void BindCancelReason()
    {
        try
        {
            plUpdcashpay = new UpdCashPaymentModel();
            plUpdcashpay.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
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

    void BindAllCashPaymentDDL()
    {
        try
        {
            plUpdcashpay = new UpdCashPaymentModel();
            plUpdcashpay.Ind = 11;
            plUpdcashpay.OrgID = GlobalSession.OrgID;
            plUpdcashpay.BrID = GlobalSession.BrID;
            plUpdcashpay.YrCD = GlobalSession.YrCD;
            plUpdcashpay.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateCashPayment/BindAllUpdCashPaymentDDL");
            DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plUpdcashpay);
            if (dsBindAllCRDDL.Tables.Count > 0)
            {
                DataTable dtAccountHead = dsBindAllCRDDL.Tables[0];
                DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];
                DataTable dtNarration = dsBindAllCRDDL.Tables[2];
                DataTable dtCashAccount = dsBindAllCRDDL.Tables[3];
                DataTable dtCostCenter = dsBindAllCRDDL.Tables[4];
                DataTable dtServiceList = dsBindAllCRDDL.Tables[5];
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
                    ddlCashAccount.DataSource = dtCashAccount;
                    ddlCashAccount.DataTextField = "AccName";
                    ddlCashAccount.DataValueField = "AccCode";
                    ddlCashAccount.DataBind();
                    if (dtCashAccount.Rows.Count > 1)
                        ddlCashAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
                }

                // Last Voucher No And Date
                if (dtLastVoucher.Rows.Count > 0)
                {
                    if (CommonCls.ConvertIntZero(dtLastVoucher.Rows[0]["LastNo"]) != 0)
                    {
                        txtLastVoucherNo.Text = dtLastVoucher.Rows[0][0].ToString();
                        lblInvoiceAndDate.Text = "Last Transaction No. & Date : " + dtLastVoucher.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtLastVoucher.Rows[0]["LastDate"]);
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
                        ddlCostCenter.DataTextField = "SectionName";
                        ddlCostCenter.DataValueField = "SectionID";
                        ddlCostCenter.DataBind();
                        ddlCostCenter.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }

    #region Old Bindings
    //void LoadLastVoucherNo() // Last Voucher No Insert
    //{
    //    try
    //    {
    //        plUpdcashpay = new UpdCashPaymentModel()
    //        {
    //            Ind = 7,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("CashPayment/LastVoucherNo");
    //        DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
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
    //        ShowMessage(ex.Message, false, lblMsg);
    //    }
    //}

    //void LoadNarration()
    //{
    //    try
    //    {
    //        plUpdcashpay = new UpdCashPaymentModel()
    //        {
    //            Ind = 6,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("UpdateCashPayment/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
    //        if (NarrationList.Rows.Count > 0)
    //        {
    //            txtNarration.DataSource = NarrationList;
    //            txtNarration.DataTextField = "NarrationDesc";
    //            txtNarration.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false, lblMsg);
    //    }
    //}

    //void LoadAccountHead()
    //{
    //    try
    //    {
    //        plUpdcashpay = new UpdCashPaymentModel()
    //        {
    //            Ind = 5,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("UpdateCashPayment/AccountHead");
    //        DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
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
    //        ShowMessage(ex.Message, false, lblMsg);
    //    }
    //}

    //void LoadCashAccount()
    //{
    //    try
    //    {
    //        plUpdcashpay = new UpdCashPaymentModel()
    //        {
    //            Ind = 4,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //            YrCD = GlobalSession.YrCD
    //        };

    //        string uri = string.Format("UpdateCashPayment/LoadCashAccount");
    //        DataTable dtCashAccount = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
    //        if (dtCashAccount.Rows.Count > 0)
    //        {
    //            ddlCashAccount.DataSource = dtCashAccount;
    //            ddlCashAccount.DataTextField = "AccName";
    //            ddlCashAccount.DataValueField = "AccCode";
    //            ddlCashAccount.DataBind();
    //            if (dtCashAccount.Rows.Count > 1)
    //                ddlCashAccount.Items.Insert(0, new ListItem("-- Select --", "0000"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false, lblMsg);
    //    }
    //}
    #endregion

    DataTable CreatGridDt() // Create Grid Structure
    {
        dtgrdview = new DataTable();
        dtgrdview.Columns.Add("AccCode", typeof(string));
        dtgrdview.Columns.Add("AccName", typeof(string));
        dtgrdview.Columns.Add("PartyID", typeof(string));
        dtgrdview.Columns.Add("BillNos", typeof(string));
        dtgrdview.Columns.Add("GSTIN", typeof(string));
        dtgrdview.Columns.Add("InvoiceNo", typeof(string));
        dtgrdview.Columns.Add("InvoiceDate", typeof(string));
        dtgrdview.Columns.Add("Amount", typeof(string));
        dtgrdview.Columns.Add("DrCr", typeof(string));
        dtgrdview.Columns.Add("IsCapitalRevenue", typeof(int));

        // dtgrdview.Columns.Add("CapitalRevenue", typeof(string));
        return dtgrdview;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {


            if (ddlCashAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Cash Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Cash Account Can Not Be Same.", false, lblMsg);
                return;
            }


            if (!string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Not Null
            {
                if (string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Copulsory On InvoiceNo
                {
                    txtInvoiceDate.Focus();
                    ShowMessage("Please Select Bill Date.", false, lblMsg);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Bill Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false, lblMsg);
                    txtInvoiceDate.Focus();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Not Null
            {
                if (string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Copulsory On InvoiceDate
                {
                    txtInVoiceNo.Focus();
                    ShowMessage("Please Enter Bill No.", false, lblMsg);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Bill Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false, lblMsg);
                    txtInvoiceDate.Focus();
                    return;
                }
            }

            // Commented by sir
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
            //            if (ddlCostCenter.SelectedValue == "0")
            //            {
            //                ShowMessage("Select Cost Centre.", false, lblMsg);
            //                ddlCostCenter.Focus();
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
                    ShowMessage("Two Control Account is not add on one Cash Payment", false, lblMsg);
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
            dtgrdview = (DataTable)ViewState["dtgrdview"];
            if (dtgrdview != null && dtgrdview.Rows.Count > 0)
            {
                if (RowIndex >= 0)
                {
                    dtgrdview.Rows[RowIndex].Delete();
                }

                DataRow[] rows = dtgrdview.Select("AccCode=" + txtAccountHead.SelectedItem.Value + " AND  InvoiceNo='" + InvoiceNo + "'");
                if (rows.Count() >= 1)
                {
                    if (InvoiceNo == string.Empty)
                    {
                        txtAccountHead.Focus();
                        ShowMessage("This Account Head With Blank Or 0 Bill No. Already Exist.", false, lblMsg);
                        return;
                    }

                    txtAccountHead.Focus();
                    //ClearAll();
                    ShowMessage("This Account Head With Given Bill No. Already Exist.", false, lblMsg);
                    return;
                }
            }

            if (ViewState["dtgrdview"] == null) // First Time DataTable Create For Grid
            {
                CreatGridDt();
            }
            else
            {
                dtgrdview = (DataTable)ViewState["dtgrdview"];
            }

            DataRow dr = dtgrdview.NewRow();
            dr["AccName"] = txtAccountHead.SelectedItem.Text;
            dr["AccCode"] = txtAccountHead.SelectedItem.Value;
            dr["GSTIN"] = ""; //string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value) ? "" : ddlGSTINNo.SelectedItem.Value;

            if (divPartySelect.Visible == true)
            {
                dr["PartyID"] = "0";

                //string BillNos = "";
                //for (int i = 0; i < CbOutstandingBill.Items.Count; i++)
                //{
                //    if (CbOutstandingBill.Items[i].Selected)
                //    {
                //        if (i == 0)
                //            BillNos = CbOutstandingBill.Items[i].Value;
                //        else
                //            BillNos += "," + CbOutstandingBill.Items[i].Value;
                //    }
                //}
            }
            dr["InvoiceNo"] = InvoiceNo;
            //dr["InvoiceNo"] = txtInVoiceNo.Text;
            dr["InvoiceDate"] = txtInvoiceDate.Text;
            dr["Amount"] = txtAmount.Text;
            dr["DrCr"] = ddlCrOrDr.SelectedItem.Value;
            if (ddlCapitalRevenue.SelectedValue == "0")
            {
                dr["IsCapitalRevenue"] = 0;
                dr["IsCapitalRevenuenName"] = "";
            }
            else
            {
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(ddlCapitalRevenue.SelectedValue);
                dr["IsCapitalRevenuenName"] = ddlCapitalRevenue.SelectedItem.Text;
            }
            //  dr["CapitalRevenue"] = ddlCapitalRevenue.SelectedItem.Text;
            if (RowIndex >= 0)
            {
                dtgrdview.Rows.InsertAt(dr, RowIndex);
                RowIndex = -1;
            }
            else
            {
                dtgrdview.Rows.Add(dr);
            }
            ViewState["dtgrdview"] = dtgrdview;
            gvCashPayment.DataSource = dtgrdview;
            gvCashPayment.DataBind();
            CalculateTotalInvoiceAmount();
            ClearAll();
            txtAccountHead.Focus();

        }

        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }

    private bool CheckBudgetHead()
    {
        plUpdcashpay = new UpdCashPaymentModel();
        plUpdcashpay.Ind = 12;
        plUpdcashpay.OrgID = GlobalSession.OrgID;
        plUpdcashpay.BrID = GlobalSession.BrID;
        plUpdcashpay.YrCD = GlobalSession.YrCD;
        plUpdcashpay.AccCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue);
        string uri = string.Format("UpdateCashPayment/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
        if (dtCashReceipt.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtCashReceipt.Rows[0]["Cnt"].ToString()) > 0)
            {
                return true;
            }
        }
        return false;
    }
    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtAccountHead.SelectedItem.Value) <= 0)
            {
                ShowMessage("Account Head Not Available", false, lblMsg);
                return;
            }
            if (string.IsNullOrEmpty(txtSearchVNo.Text))
            {
                txtAccountHead.ClearSelection();
                txtSearchVNo.Focus();
                ShowMessage("Enter Transaction No. First.", false, lblMsg);
                return;
            }
            if (CommonCls.ConvertIntZero(ddlCashAccount.SelectedValue) == 0)
            {
                ddlCashAccount.Focus();
                ShowMessage("Select Cash Account First.", false, lblMsg);
                return;
            }

            if (ddlCashAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Cash Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Cash Account Can Not Be Same.", false, lblMsg);
                return;
            }


            FillGSTIN();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }
    void FillGSTIN()
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
        ddlGSTINNo.Focus();
    }

    void CalculateTotalInvoiceAmount() // For Invoice Amount Dr - CR
    {
        decimal crAmount = 0;
        decimal drAmount = 0;
        dtgrdview = (DataTable)ViewState["dtgrdview"];
        for (int rowIndex = 0; dtgrdview.Rows.Count > rowIndex; rowIndex++)
        {
            string DrCr = dtgrdview.Rows[rowIndex]["DrCr"].ToString();
            if (DrCr == "Cr")
            {
                crAmount = crAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex]["Amount"]);
            }
            else
            {
                drAmount = drAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex]["Amount"]);
            }
        }
        txtInvoiceTotalAmount.Text = Convert.ToString(drAmount - crAmount);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchVNo.Text == "" || Convert.ToInt32(txtSearchVNo.Text) == 0)
            {
                txtSearchVNo.Focus();
                ShowMessage("Please Enter Transaction No.", false, lblMsg);
                return;
            }
            plUpdcashpay = new UpdCashPaymentModel();
            plUpdcashpay.Ind = 2;
            plUpdcashpay.OrgID = GlobalSession.OrgID;
            plUpdcashpay.BrID = GlobalSession.BrID;
            plUpdcashpay.YrCD = GlobalSession.YrCD;
            plUpdcashpay.VchType = Convert.ToInt32(ViewState["VchType"]);
            plUpdcashpay.DocNo = Convert.ToInt32(txtSearchVNo.Text);

            string uri = string.Format("UpdateCashPayment/SearchCashPay");
            DataTable dtgrdview = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
            if (dtgrdview.Rows.Count > 0)
            {
                if (dtgrdview.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ShowMessage("This Transaction No. Already Canceled.", false, lblMsg);
                    txtSearchVNo.Focus();
                    return;
                }


                txtSearchVNo.Enabled = true;
                ViewState["dtgrdview"] = dtgrdview;
                txtVoucherDate.Text = dtgrdview.Rows[0]["VoucharDate"].ToString();
                txtNarration.Text = dtgrdview.Rows[0]["Narration"].ToString();
                Narration = dtgrdview.Rows[0]["Narration"].ToString();
                ddlCashAccount.SelectedValue = dtgrdview.Rows[0]["CashCode"].ToString();
                ddlCostCenter.SelectedValue = dtgrdview.Rows[0]["CCCode"].ToString();
                if (Convert.ToInt32(dtgrdview.Rows[0]["RevenueServiceNo"].ToString()) > 0)
                {
                    txtServiceNo.Text = dtgrdview.Rows[0]["RevenueServiceNo"].ToString();
                    txtPartyName.Text = dtgrdview.Rows[0]["RevenuePartyName"].ToString();
                    txtPartyAddress.Text = dtgrdview.Rows[0]["RevenuePartyAddress"].ToString();
                    txtpartyGstIN.Text = dtgrdview.Rows[0]["RevenuePartyGSTIN"].ToString();
                    ServiceNoTaken = 1;
                }
                gvCashPayment.DataSource = dtgrdview;
                gvCashPayment.DataBind();
                CalculateTotalInvoiceAmount();

                txtSearchVNo.Enabled = false;
                btnGo.Enabled = false;
                btnCancel.Enabled = true;
                ddlCapitalRevenue.SelectedValue = "1";
            }
            else
            {
                ShowMessage("Transaction Not Found.", false, lblMsg);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }
    protected void gvCashPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditRow")
            {
                DataTable dtgrdview = (DataTable)ViewState["dtgrdview"];
                DataRow drgrdview = dtgrdview.Rows[rowIndex];

                txtAccountHead.SelectedValue = drgrdview["AccCode"].ToString();
                FillGSTIN();
                ddlGSTINNo.SelectedValue = drgrdview["GSTIN"].ToString();
                txtInVoiceNo.Text = drgrdview["InvoiceNo"].ToString();
                txtInvoiceDate.Text = drgrdview["InvoiceDate"].ToString();
                txtAmount.Text = drgrdview["Amount"].ToString();
                ddlCrOrDr.SelectedValue = drgrdview["DrCr"].ToString();
                ddlCapitalRevenue.SelectedValue = CommonCls.ConvertIntZero(drgrdview["IsCapitalRevenue"].ToString()) == 0 ? "0" : drgrdview["IsCapitalRevenue"].ToString();

                RowIndex = rowIndex;
                gvCashPayment.DataSource = ViewState["dtgrdview"] = dtgrdview;
                gvCashPayment.DataBind();

                foreach (GridViewRow row in gvCashPayment.Rows)
                {
                    if (row.RowIndex != rowIndex)
                    {
                        LinkButton editButton = (LinkButton)row.FindControl("lnkEdit");
                        editButton.Enabled = false;

                        LinkButton deleteButton = (LinkButton)row.FindControl("btnDelete");
                        deleteButton.Enabled = false;
                    }
                    else
                    {
                        row.BackColor = Color.Bisque;
                    }
                }
            }
            else if (e.CommandName == "RemoveRow")
            {
                dtgrdview = (DataTable)ViewState["dtgrdview"];
                dtgrdview.Rows[rowIndex].Delete();
                gvCashPayment.DataSource = ViewState["dtgrdview"] = dtgrdview;
                gvCashPayment.DataBind();
            }
            CalculateTotalInvoiceAmount();
            txtAccountHead.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = false;

            if (!ValidationOnBtnSave())
            {
                btnSave.Enabled = true;
                return;
            }

            dtgrdview = (DataTable)ViewState["dtgrdview"];
            DataTable dtcashPay = CashPaymentSchema();

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow dr = dtcashPay.NewRow();
                dr["OrgID"] = GlobalSession.OrgID;        //Company/End User ID
                dr["BrID"] = GlobalSession.BrID;          //Branch ID
                dr["VchType"] = Convert.ToInt32(ViewState["VchType"]);    //Document/Voucher Type
                dr["YrCD"] = GlobalSession.YrCD;          //Financial Year Code (2017-17)
                dr["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date
                dr["DocNo"] = CommonCls.ConvertIntZero(txtLastVoucherNo.Text);   //Voucher No.
                dr["AccCode"] = Convert.ToInt32(item["AccCode"]);  //Selected Account Head Code
                dr["AccGst"] = item["GSTIN"].ToString();                //Selected Account Head GSTIN No. (If Available)
                dr["AccCode2"] = Convert.ToInt32(ddlCashAccount.SelectedItem.Value);  //Selected Cash Account Code

                dr["RefNo"] = 0;
                dr["RefDate"] = "";

                if (item["DrCr"].ToString() == "Cr")
                    dr["AmountCr"] = Convert.ToDecimal(item["Amount"]); //Dr Amount
                else
                    dr["AmountDr"] = Convert.ToDecimal(item["Amount"]); //Cr Amount	
                dr["AdvanceInd"] = 0;                                   //AgainstAdvanceInd (If Receipt Made For Against Advance Taken From Debitor/Creditor Then It Goes 1 Else 0)
                dr["DocDesc"] = txtNarration.Text;                      //Narration
                dr["EntryType"] = 2;                                    //EntryType - 1-Entry/2-Ammendment/3-Cancel
                dr["User"] = GlobalSession.UserID;                      //UserID - User ID (Entry By Which User)
                dr["IP"] = GlobalSession.IP;                            //IPAddress - Client Machine IP Address
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(item["IsCapitalRevenue"].ToString()) == 0 ? "0" : item["IsCapitalRevenue"].ToString();

                dr["BillNo"] = item["InvoiceNo"].ToString();//Invoice No.
                dr["BillDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : ""; //Invoice Date

                dtcashPay.Rows.Add(dr);
            }

            if (dtcashPay.Columns.Contains("IsCapitalRevenuenName"))
                dtcashPay.Columns.Remove("IsCapitalRevenuenName");

            plUpdcashpay = new UpdCashPaymentModel();
            plUpdcashpay.Ind = 3;
            plUpdcashpay.DocNo = Convert.ToInt32(txtSearchVNo.Text);
            plUpdcashpay.OrgID = GlobalSession.OrgID;
            plUpdcashpay.BrID = GlobalSession.BrID;
            plUpdcashpay.VchType = Convert.ToInt32(ViewState["VchType"]);
            plUpdcashpay.YrCD = GlobalSession.YrCD;
            plUpdcashpay.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            plUpdcashpay.DeptID = GlobalSession.DepartmentID;
            plUpdcashpay.SubDeptID = GlobalSession.SubDeptID;

            plUpdcashpay.IsAudit = CommonCls.ConvertIntZero(Session["IsAudit"] == null ? 0 : Session["IsAudit"].ToString() == "1" ? 1 : 0);
            plUpdcashpay.IsSendToAudit = CommonCls.ConvertIntZero(Session["IsSendToAudit"] == null ? 0 : Session["IsSendToAudit"].ToString() == "1" ? 1 : 0);

            plUpdcashpay.PartyName = txtPartyName.Text;
            plUpdcashpay.PartyAddress = txtPartyAddress.Text;
            plUpdcashpay.PartyGstIN = txtpartyGstIN.Text;
            plUpdcashpay.ServiceNo = CommonCls.ConvertIntZero(txtServiceNo.Text);

            plUpdcashpay.Dt = JsonConvert.SerializeObject(dtcashPay);

            string uri = string.Format("UpdateCashPayment/UpdateCashPay");
            DataTable dtSaveCashPay = CommonCls.ApiPostDataTable(uri, plUpdcashpay);
            if (dtSaveCashPay.Rows.Count > 0)
            {
                if (dtSaveCashPay.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    ClearOnSave();

                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveCashPay.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSaveCashPay.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                    ShowMessage("Record Successfully Updated With Transaction No. " + VoucherNo, true, lblMsg);
                    txtVoucherDate.Focus();

                    CallReport(VoucherNo, VoucherDate);
                    //btnBack_Click(sender, e);
                }
                else
                {
                    ShowMessage("Record Not Update. Please Try Again.", false, lblMsg);
                }
            }
            else
            {
                ShowMessage("Record Not Update. Please Try Again.", false, lblMsg);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
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
                ShowMessage("Invalid Service Number Please search first.", false, lblMsg);
                return false;
            }
        }
        #endregion

        if (ViewState["dtgrdview"] == null || ((DataTable)ViewState["dtgrdview"]).Rows.Count <= 0)
        {
            txtAccountHead.Focus();
            ShowMessage("Insert Transaction Details!", false, lblMsg);
            return false;
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtVoucherDate.Focus();
            ShowMessage("Transaction Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false, lblMsg);
            return false;
        }

        if (string.IsNullOrEmpty(txtInvoiceTotalAmount.Text) || Convert.ToDecimal(txtInvoiceTotalAmount.Text) <= 0) //Invoice Total Amount.
        {
            ShowMessage("Transaction Net Amount Is Invalid. Please Check Your Entries.", false, lblMsg);
            return false;
        }

        if (string.IsNullOrEmpty(txtNarration.Text))
        {
            txtNarration.Focus();
            ShowMessage("Enter Narration.", false, lblMsg);
            return false;
        }

        return true;
    }

    void ClearOnSave()
    {
        txtServiceNo.Enabled = false;
        txtNarration.Text = "";
        ddlCashAccount.ClearSelection();
        txtSearchVNo.Enabled = true;
        btnGo.Enabled = true;
        txtInvoiceTotalAmount.Text = "0";
        gvCashPayment.DataSource = null;
        gvCashPayment.DataBind();
        ViewState["dtgrdview"] = null;
        ddlCostCenter.ClearSelection();
        txtVoucherDate.Text = txtSearchVNo.Text = "";

        txtInvoiceDate.Text = "";
        ServiceNoTaken = 0;
        ddlCostCenter.ClearSelection();
        ddlCapitalRevenue.SelectedValue = "1";
        txtPartyName.Text = txtPartyAddress.Text = txtpartyGstIN.Text = "";
        txtServiceNo.Text = "";

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtServiceNo.Enabled = false;
            txtSearchVNo.Enabled = true;
            btnGo.Enabled = true;
            txtSearchVNo.Text = "";

            gvCashPayment.DataSource = ViewState["dtgrdview"] = null;
            gvCashPayment.DataBind();
            ClearAll();
            txtVoucherDate.Text =
                txtInvoiceTotalAmount.Text = "";
            ddlCashAccount.ClearSelection();
            txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = txtNarration.Text = "";
            ServiceNoTaken = 0;
            ddlCostCenter.ClearSelection();
            txtSearchVNo.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false, lblMsg);
        }
    }
    void ClearAll() // Clear Selection On Add Click.
    {
        txtInVoiceNo.Text =
        txtInvoiceDate.Text =
        txtAmount.Text = string.Empty;
        ddlGSTINNo.DataSource = new DataTable(); ddlGSTINNo.DataBind();
        ddlCrOrDr.ClearSelection();
        txtAccountHead.ClearSelection();
    }

    DataTable CashPaymentSchema()
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
        dt.Columns.Add("DocDesc", typeof(string));
        dt.Columns.Add("EntryType", typeof(int));
        dt.Columns.Add("User", typeof(int));
        dt.Columns.Add("IP", typeof(string));
        dt.Columns.Add("IsCapitalRevenue", typeof(int));
        dt.Columns.Add("BillNo", typeof(string));
        dt.Columns.Add("BillDate", typeof(string));

        return dt;
    }
    void CallReport(string VoucherNo, string VoucherDate)
    {
        Hashtable HT = new Hashtable();
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "Cash Payment");
        HT.Add("Ind", 2);
        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", 0);//GlobalSession.YrCD);

        HT.Add("DocTypeID", Convert.ToInt16(ViewState["VchType"]));
        HT.Add("Voucharno", VoucherNo);
        HT.Add("VoucharDate", VoucherDate.Substring(6, 4) + "/" + VoucherDate.Substring(3, 2) + "/" + VoucherDate.Substring(0, 2));

        VouchersReport.ReportName = "RptVoucher";
        VouchersReport.FileName = "Voucher";
        VouchersReport.ReportHeading = "Cash Payment";
        VouchersReport.HashTable = HT;
        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print?";
        VouchersReport.ShowReport();

        Session["DocTypeID"] = Convert.ToInt16(ViewState["VchType"]);
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
                ShowMessage("Enter Rejection Reason.", false, lblCancelMsg);
                pnlConfirmInvoice.Visible = true;
                return;
            }

            UpdCashPaymentModel plUpdcashpay;
            plUpdcashpay = new UpdCashPaymentModel();
            plUpdcashpay.Ind = 6;
            plUpdcashpay.OrgID = GlobalSession.OrgID;
            plUpdcashpay.BrID = GlobalSession.BrID;
            plUpdcashpay.YrCD = GlobalSession.YrCD;
            plUpdcashpay.VchType = Convert.ToInt32(ViewState["VchType"]);
            plUpdcashpay.CancelReason = ddlCancelReason.SelectedItem.Text;
            plUpdcashpay.DocNo = Convert.ToInt32(txtSearchVNo.Text);

            string uri = string.Format("UpdateCashPayment/CancelVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, plUpdcashpay);

            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ClearAll();
                    ClearOnSave();
                    //pnlConfirmInvoice.Visible = false;
                    hypBtnBack.Visible = true;
                    ShowMessage("Transaction No. - " + plUpdcashpay.DocNo + " is Rejected successfully ", true, lblCancelMsg);
                }
                else
                {
                    //pnlConfirmInvoice.Visible = false;
                    hypBtnBack.Visible = true;
                    ShowMessage("This Transaction is not Rejected.", true, lblCancelMsg);
                }
            }
        }
        catch (Exception)
        {
            ShowMessage("Record Not Reject, Please Try Again.", false, lblCancelMsg);
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        txtCancelReason.Text = "";
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
                txtPartyName.Text = dv31.ToTable().Rows[0]["PlotHolderNameE"].ToString();
                txtPartyAddress.Text = dv31.ToTable().Rows[0]["CAddress1"].ToString();
                txtpartyGstIN.Text = dv31.ToTable().Rows[0]["GSTNo"].ToString();
                return true;
            }
            else
            {

                ShowMessage("Master Informetion of given Service No" + txtServiceNo.Text + " Not Available. Do You Want To Proceed.", false, lblMsg);
                txtServiceNo.Focus();
                txtPartyName.Text = txtPartyAddress.Text = txtpartyGstIN.Text = "";
                return false;
            }
        }
        catch (Exception)
        {
            ShowMessage("Internal server error.", false, lblMsg);
            return false;
        }
    }
    private bool BindPloteServiceNo()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from mstplotserviceNo", con);
            SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
            DataTable dtPlotServiceNo = new DataTable();
            Adpt.Fill(dtPlotServiceNo);
            ViewState["VSPlotServiceNo"] = VSPlotServiceNo = dtPlotServiceNo;
            con.Close();
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
                ShowMessage("Enter Service No", false, lblMsg);
                return;
            }
            bool IsStatusServiceNo = BindPloteServiceNo();
            if (IsStatusServiceNo == false)
            {
                ShowMessage("This DataBase is not available Please contact to system administrator", false, lblMsg);
                return;
            }
            BindServiceDetail();

            MatchServiceNo();

        }
        catch (Exception)
        {
            ShowMessage("Internal server error.", false, lblMsg);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Session["TransNo"] = Session["TransDate"] = null;
        Response.Redirect("../Vouchers/FrmPendingVouchers.aspx");
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {
        lblTransactionNo.Text = txtSearchVNo.Text;
        lblTransactionDate.Text = txtVoucherDate.Text;
        lblDocumentType.Text = "Cash Payment";
        lblNarration.Text = Narration;

        pnlApproval.Visible = true;
        pnlConfirmInvoice.Visible = false;
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = txtApprovalDate.Text = txtApprovalRemark.Text = "";
        pnlApproval.Visible = false;
    }

    protected void btnApprovedYes_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = "";

        if (string.IsNullOrEmpty(txtApprovalDate.Text))
        {
            txtApprovalDate.Focus();
            ShowMessage("Enter Approval Date.", false, lblApprovalMSG);
            return;
        }

        if (Convert.ToDateTime(txtApprovalDate.Text) < Convert.ToDateTime(lblTransactionDate.Text))
        {
            txtApprovalDate.Focus();
            ShowMessage("Approval Date Should Not Be Less Then Transaction Date.", false, lblApprovalMSG);
            return;
        }

        if (string.IsNullOrEmpty(txtApprovalRemark.Text))
        {
            txtApprovalRemark.Focus();
            ShowMessage("Enter Approval Remark.", false, lblApprovalMSG);
            return;
        }

        pnlApprovalConfirmation.Visible = true;
    }

    protected void btnApprovalConfirmationYes_Click(object sender, EventArgs e)
    {
        try
        {
            PendingVouchersModel objPendingVouchersModel = new PendingVouchersModel();
            objPendingVouchersModel.OrgID = GlobalSession.OrgID;
            objPendingVouchersModel.BrID = GlobalSession.BrID;
            objPendingVouchersModel.YrCD = GlobalSession.YrCD;
            objPendingVouchersModel.TransactionNo = CommonCls.ConvertIntZero(lblTransactionNo.Text);
            objPendingVouchersModel.TransactionDate = CommonCls.ConvertToDate(lblTransactionDate.Text);
            objPendingVouchersModel.VchType = CommonCls.ConvertIntZero(ViewState["VchType"].ToString());
            objPendingVouchersModel.ApprovalDate = CommonCls.ConvertToDate(txtApprovalDate.Text);
            objPendingVouchersModel.ApprovalRemark = txtApprovalRemark.Text;
            objPendingVouchersModel.User = GlobalSession.UserID;
            objPendingVouchersModel.IP = GlobalSession.IP;
            string uri = "";

            if (GlobalSession.DesignationID == 26) //SUPERINTENDENT
            {
                objPendingVouchersModel.Ind = 2;
                objPendingVouchersModel.IsFinal = 1;
                uri = string.Format("PendingVouchers/DataApproved");
            }
            else if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17) //ACCOUNT OFFICER
            {
                objPendingVouchersModel.Ind = 3;
                objPendingVouchersModel.IsFinal = 2;
                uri = string.Format("PendingVouchers/FinalApproval");
            }

            DataTable dtApproved = CommonCls.ApiPostDataTable(uri, objPendingVouchersModel);
            if (dtApproved.Rows.Count > 0)
            {
                if (dtApproved.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    btnBack_Click(sender, e);
                    ShowMessage("Record Approved Successfully.", true, lblApprovalMSG);
                }
                else if (dtApproved.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    ShowMessage("Record Not Approved, Please Try Again.", false, lblApprovalMSG);
                }
            }
            else
            {
                ShowMessage("Record Not Approved, Please Try Again.", false, lblApprovalMSG);
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, false, lblApprovalMSG);
        }
    }

    protected void btnApprovalConfirmationNo_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = txtApprovalDate.Text = txtApprovalRemark.Text = "";
        pnlApproval.Visible = pnlApprovalConfirmation.Visible = false;
    }

    void ShowMessage(string Message, bool type, Label lblErrorMSG)
    {
        lblErrorMSG.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblErrorMSG.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}