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

public partial class Modifiaction_frmUpdBankReceipt : System.Web.UI.Page
{
    DataTable dtgrdview, dtAccGstin;
    UpdateBankReceiptModel plUpdbankrec;
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

    public static string strcon = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
    ////create new sqlconnection and connection to database by using connection string from web.config file  
    SqlConnection con = new SqlConnection(strcon);

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ServiceNoTaken = 0;
            txtVoucherDate.Focus();
            ViewState["VchType"] = 3;
            BindAllBankReceiptDDL();
            BindCancelReason();

            //LoadNarration();
            //LoadAccountHead();
            //LoadLastVoucherNo();
            //LoadBankAccount();
            SetPayMode();
            txtSearchVNo.Focus();
            //VouchersReport.function("7845687", "2017/06/15");

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
            ShowMessage("This DataBase is not available Please contact to system administrator", false);
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
            plUpdbankrec = new UpdateBankReceiptModel();
            plUpdbankrec.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
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

    void BindAllBankReceiptDDL()
    {
        try
        {
            plUpdbankrec = new UpdateBankReceiptModel();
            plUpdbankrec.Ind = 11;
            plUpdbankrec.OrgID = GlobalSession.OrgID;
            plUpdbankrec.BrID = GlobalSession.BrID;
            plUpdbankrec.YrCD = GlobalSession.YrCD;
            plUpdbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateBankReceipt/BindAllUpdBankReceiptDDL");
            DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plUpdbankrec);
            if (dsBindAllCRDDL.Tables.Count > 0)
            {
                DataTable dtAccountHead = dsBindAllCRDDL.Tables[0];
                DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];
                DataTable dtNarration = dsBindAllCRDDL.Tables[2];
                DataTable dtCashAccount = dsBindAllCRDDL.Tables[3];
                DataTable dtCostCenter = dsBindAllCRDDL.Tables[4];
                DataTable dtServiceList = dsBindAllCRDDL.Tables[5];

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
                ViewState["dtsevicelist"] = VSServiceList = dtServiceList;

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
                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucher.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtLastVoucher.Rows[0]["LastDate"]);
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
                        ddlCostCenter.DataTextField = "CostCentreName";
                        ddlCostCenter.DataValueField = "CostCentreID";
                        ddlCostCenter.DataBind();
                        ddlCostCenter.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #region Old Bindings
    //void LoadLastVoucherNo() // Last Voucher No Insert
    //{
    //    try
    //    {
    //        plUpdbankrec = new UpdateBankReceiptModel()
    //        {
    //            Ind = 7,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };


    //        string uri = string.Format("BankReceipt/LastVoucherNo");
    //        DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
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
    //void LoadBankAccount()
    //{
    //    try
    //    {
    //        plUpdbankrec = new UpdateBankReceiptModel()
    //        {
    //            Ind = 4,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("UpdateBankReceipt/LoadBankAccount");
    //        DataTable dtBankAccount = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
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

    //void LoadAccountHead()
    //{
    //    try
    //    {
    //        plUpdbankrec = new UpdateBankReceiptModel()
    //        {
    //            Ind = 5,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("UpdateBankReceipt/AccountHead");
    //        DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
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

    //void LoadNarration()
    //{
    //    try
    //    {
    //        plUpdbankrec = new UpdateBankReceiptModel()
    //        {
    //            Ind = 6,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("UpdateBankReceipt/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
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
    #endregion

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

    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtAccountHead.SelectedItem.Value) <= 0)
            {
                ShowMessage("Account Head Not Valid", false);
                return;
            }

            if (string.IsNullOrEmpty(txtSearchVNo.Text))
            {
                txtAccountHead.ClearSelection();
                txtSearchVNo.Focus();
                ShowMessage("Enter Voucher No. First.", false);
                return;
            }
            if (CommonCls.ConvertIntZero(ddlBankAccount.SelectedValue) == 0)
            {
                ddlBankAccount.Focus();
                ShowMessage("Select Bank Account First.", false);
                return;
            }

            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }

            FillGSTIN();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
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

        return dtgrdview;
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            //if (string.IsNullOrEmpty(txtAccountHead.Text)) // For Account Head Not Null Or Empty
            //{
            //    txtAccountHead.Focus();
            //    ShowMessage("Enter Account Head.", false);
            //    return;
            //}

            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }



            //try // For txtAccount Head Value Shouldn't null,0 or Garbage.
            //{
            //    if (txtAccountHead.SelectedItem.Value == null || Convert.ToInt32(txtAccountHead.SelectedItem.Value) == 0) // For Account Head Code Not Null Or Empty
            //    {
            //        txtAccountHead.Focus();
            //        ShowMessage("Account Value Not Available", false);
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    txtAccountHead.Focus();
            //    ShowMessage("This Account Head Value Not Available.", false);
            //    return;
            //}


            //if (string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) <= 0) // Invoice Amount Shouldn't be Null
            //{
            //    txtAmount.Focus();
            //    ShowMessage("Please Enter Amount.", false);
            //    return;
            //}


            if (!string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Not Null
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
                    ShowMessage("Please Enter Demand No.", false);
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
            //            if (ddlCostCenter.SelectedValue == "0")
            //            {
            //                ShowMessage("Select Cost Centre.", false);
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
                        ShowMessage("This Account Head With Blank Or 0 Invoice No. Already Exist.", false);
                        return;
                    }

                    txtAccountHead.Focus();
                    //ClearAll();
                    ShowMessage("This Account Head With Given Invoice No. Already Exist.", false);
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
            dr["GSTIN"] = "";//string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value) ? "" : ddlGSTINNo.SelectedItem.Value;
            if (divPartySelect.Visible == true)
            {
                dr["PartyID"] = "0";
                //ddlSecondaryParty.Items.Count > 0 ?
                //            !string.IsNullOrEmpty(ddlSecondaryParty.SelectedItem.Value) ?
                //            ddlSecondaryParty.SelectedItem.Value : "" : "";

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
            //dr["BillNos"] = BillNos;

            dr["InvoiceNo"] = InvoiceNo;
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

            if (RowIndex >= 0)
            {
                //  dtgrdview.Rows.RemoveAt(RowIndex);
                dtgrdview.Rows.InsertAt(dr, RowIndex);
                RowIndex = -1;
            }
            else
            {
                dtgrdview.Rows.Add(dr);
            }

            gvBankReceipt.DataSource = ViewState["dtgrdview"] = dtgrdview;
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
        plUpdbankrec = new UpdateBankReceiptModel();
        plUpdbankrec.Ind = 12;
        plUpdbankrec.OrgID = GlobalSession.OrgID;
        plUpdbankrec.BrID = GlobalSession.BrID;
        plUpdbankrec.YrCD = GlobalSession.YrCD;
        plUpdbankrec.AccCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue);
        string uri = string.Format("UpdateBankReceipt/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
        if (dtCashReceipt.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtCashReceipt.Rows[0]["Cnt"].ToString()) > 0)
            {
                return true;
            }
        }
        return false;
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

    protected void gvBankReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
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
                gvBankReceipt.DataSource = ViewState["dtgrdview"] = dtgrdview;
                gvBankReceipt.DataBind();

                foreach (GridViewRow row in gvBankReceipt.Rows)
                {
                    if (row.RowIndex != rowIndex)
                    {
                        //LinkButton editButton = (LinkButton)row.FindControl("lnkEdit");
                        //editButton.Enabled = false;
                        //LinkButton deleteButton = (LinkButton)row.FindControl("btnDelete");
                        //deleteButton.Enabled = false;
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
                gvBankReceipt.DataSource = ViewState["dtgrdview"] = dtgrdview;
                gvBankReceipt.DataBind();
            }
            CalculateTotalInvoiceAmount();
            txtAccountHead.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchVNo.Text == "" || Convert.ToInt32(txtSearchVNo.Text) == 0)
            {
                txtSearchVNo.Focus();
                ShowMessage("Please Enter Voucher No.", false);
                return;
            }
            plUpdbankrec = new UpdateBankReceiptModel()
            {
                Ind = 2,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
            };
            plUpdbankrec.DocNo = Convert.ToInt32(txtSearchVNo.Text);
            string uri = string.Format("UpdateBankReceipt/SearchBankrec");
            DataTable dtgrdview = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
            if (dtgrdview.Rows.Count > 0)
            {
                if (dtgrdview.Rows[0]["CancelInd"].ToString() == "1")
                {
                    ShowMessage("This Voucher No. Already Canceled.", false);
                    txtSearchVNo.Focus();
                    return;
                }

                txtSearchVNo.Enabled = true;
                ViewState["dtgrdview"] = dtgrdview;
                txtVoucherDate.Text = Convert.ToDateTime(dtgrdview.Rows[0]["VoucharDate"].ToString()).ToString("dd/MM/yyyy");
                txtNarration.Text = dtgrdview.Rows[0]["Narration"].ToString();
                ddlBankAccount.SelectedValue = dtgrdview.Rows[0]["BankCode"].ToString();
                txtIDARefNo.Text = dtgrdview.Rows[0]["Remark3"].ToString().Replace("Old VNo-", "");


                if (Convert.ToInt32(dtgrdview.Rows[0]["RevenueServiceNo"].ToString()) > 0)
                {
                    txtServiceNo.Text = dtgrdview.Rows[0]["RevenueServiceNo"].ToString();
                    txtPartyName.Text = dtgrdview.Rows[0]["RevenuePartyName"].ToString();
                    txtPartyAddress.Text = dtgrdview.Rows[0]["RevenuePartyAddress"].ToString();
                    txtpartyGstIN.Text = dtgrdview.Rows[0]["RevenuePartyGSTIN"].ToString();
                    ServiceNoTaken = 1;
                }

                ddlCostCenter.SelectedValue = dtgrdview.Rows[0]["CCCode"].ToString();
                if (CommonCls.ConvertIntZero(dtgrdview.Rows[0]["ChequeNo"].ToString()) != 0)
                {
                    ddlPayMode.SelectedValue = "Cheque";
                    SetPayMode();
                    txtReceivedNo.Text = dtgrdview.Rows[0]["ChequeNo"].ToString();
                    txtReceivedDate.Text = Convert.ToDateTime(dtgrdview.Rows[0]["ChequeDate"].ToString()).ToString("dd/MM/yyyy");

                }
                else if (!string.IsNullOrEmpty(dtgrdview.Rows[0]["UtrNo"].ToString()) && dtgrdview.Rows[0]["UtrNo"].ToString() != "0")
                {
                    ddlPayMode.SelectedValue = "UTR";
                    SetPayMode();
                    txtReceivedNo.Text = dtgrdview.Rows[0]["UtrNo"].ToString();
                    txtReceivedDate.Text = Convert.ToDateTime(dtgrdview.Rows[0]["UtrDate"].ToString()).ToString("dd/MM/yyyy");
                }
                gvBankReceipt.DataSource = dtgrdview;
                gvBankReceipt.DataBind();
                CalculateTotalInvoiceAmount();

                txtSearchVNo.Enabled = false;
                btnGo.Enabled = false;
                btnCancel.Enabled = true;
                ddlCapitalRevenue.SelectedValue = "1";
            }
            else
            {
                ShowMessage("Voucher Not Found.", false);
            }
        }
        catch (Exception ex)
        {
        }
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
        txtInvoiceTotalAmount.Text = Convert.ToString(crAmount - drAmount);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave.Enabled = false;

            #region ServiceNo
            //BindPloteServiceNo();
            //BindServiceDetail();
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
                    ShowMessage("Invalid Service Number Please search first.", false);
                    return;
                }
            }

            #endregion

            if (ViewState["dtgrdview"] == null || ((DataTable)ViewState["dtgrdview"]).Rows.Count <= 0)
            {
                txtAccountHead.Focus();
                ShowMessage("Insert Voucher Details!", false);
                return;
            }



            bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate) // For Voucher Date Between Financial Year.
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return;
            }



            //if (string.IsNullOrEmpty(txtInvoiceTotalAmount.Text) || Convert.ToDecimal(txtInvoiceTotalAmount.Text) <= 0) //Invoice Total Amount.
            //{
            //    ShowMessage("Voucher Net Amount Is Invalid. Please Check Your Entries.", false);
            //    return;
            //}

            dtgrdview = (DataTable)ViewState["dtgrdview"];
            DataTable dtBankRec = BankRecieptSchema();
            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow dr = dtBankRec.NewRow();
                dr["OrgID"] = GlobalSession.OrgID;        //Company/End User ID
                dr["BrID"] = GlobalSession.BrID;          //Branch ID
                dr["VchType"] = Convert.ToInt32(ViewState["VchType"]);    //Document/Voucher Type
                dr["YrCD"] = GlobalSession.YrCD;          //Financial Year Code (2017-17)
                dr["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date
                dr["DocNo"] = CommonCls.ConvertIntZero(txtLastVoucherNo.Text);   //Voucher No.
                dr["AccCode"] = Convert.ToInt32(item["AccCode"]);  //Selected Account Head Code
                dr["AccGst"] = item["GSTIN"].ToString();                //Selected Account Head GSTIN No. (If Available)
                dr["AccCode2"] = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);  //Selected Cash Account Code

                dr["RefNo"] = 0; 
                dr["RefDate"] = ""; 

                if (item["DrCr"].ToString() == "Cr")
                    dr["AmountCr"] = Convert.ToDecimal(item["Amount"]);  //Dr Amount
                else
                    dr["AmountDr"] = Convert.ToDecimal(item["Amount"]);  //Cr Amount	
                dr["AdvanceInd"] = 0;//item["DrCr"] == "Dr" ? 1 : 0;     //AgainstAdvanceInd (If Receipt Made For Against Advance Taken From Debitor/Creditor Then It Goes 1 Else 0)

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

                dr["DocDesc"] = txtNarration.Text;                       //Narration
                dr["EntryType"] = 2;//                                   //EntryType - 1-Entry/2-Ammendment/3-Cancel
                dr["User"] = GlobalSession.UserID;         //UserID - User ID (Entry By Which User)
                dr["IP"] = GlobalSession.IP;                     //IPAddress - Client Machine IP Address
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(item["IsCapitalRevenue"]) == 0 ? 0 : item["IsCapitalRevenue"];
                dr["BillNo"] = item["InvoiceNo"].ToString(); //Invoice No.
                dr["BillDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : ""; //Invoice Date

                dtBankRec.Rows.Add(dr);
            }

            if (dtBankRec.Columns.Contains("IsCapitalRevenuenName"))
                dtBankRec.Columns.Remove("IsCapitalRevenuenName");

            plUpdbankrec = new UpdateBankReceiptModel();
            plUpdbankrec.Ind = 3;
            plUpdbankrec.DocNo = Convert.ToInt32(txtSearchVNo.Text);
            plUpdbankrec.OrgID = GlobalSession.OrgID;
            plUpdbankrec.BrID = GlobalSession.BrID;
            plUpdbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);
            plUpdbankrec.YrCD = GlobalSession.YrCD;
            //plUpdbankrec.Dt = dtBankRec;
            plUpdbankrec.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);
            plUpdbankrec.IDARefNo = txtIDARefNo.Text;

            plUpdbankrec.Dt = JsonConvert.SerializeObject(dtBankRec);
            string uri = string.Format("UpdateBankReceipt/UpdateBankRec");
            DataTable dtSaveBankRec = CommonCls.ApiPostDataTable(uri, plUpdbankrec);
            if (dtSaveBankRec.Rows.Count > 0)
            {
                if (dtSaveBankRec.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    ClearOnSave();
                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveBankRec.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSaveBankRec.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");
                    ShowMessage("Record Successfully Updated With Voucher No. " + VoucherNo, true);
                    CallReport(VoucherNo, VoucherDate);
                    txtVoucherDate.Focus();
                }
                else
                {
                    ShowMessage("Record Not Update. Please Try Again.", false);
                }
            }
            else
            {
                ShowMessage("Record Not Update. Please Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        btnSave.Enabled = true;

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
    void ClearOnSave()
    {
        txtServiceNo.Enabled = false;
        ClearAll();
        gvBankReceipt.DataSource = ViewState["dtgrdview"] = null;
        gvBankReceipt.DataBind();
        txtSearchVNo.Enabled = true;
        btnGo.Enabled = true;
        txtSearchVNo.Text = "";
        txtVoucherDate.Text = txtReceivedDate.Text = txtReceivedNo.Text = txtInvoiceTotalAmount.Text = txtNarration.Text = "";
        ddlBankAccount.ClearSelection();
        ddlCostCenter.ClearSelection();
        ServiceNoTaken = 0;
        txtIDARefNo.Text = "";
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

        VouchersReport.ReportName = "RptVoucher";
        VouchersReport.FileName = "Voucher";
        VouchersReport.ReportHeading = "Bank Receipt";
        VouchersReport.HashTable = HT;
        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print Receipt?";
        VouchersReport.ShowReport();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtServiceNo.Enabled = false;
        txtSearchVNo.Enabled = true;
        btnGo.Enabled = true;
        txtSearchVNo.Text = "";
        ClearOnSave();
        ClearAll();
        txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = "";
        ServiceNoTaken = 0;
        ddlCapitalRevenue.SelectedValue = "1";
        txtSearchVNo.Focus();
        ddlCostCenter.ClearSelection();
    }
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPayMode();
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

            UpdateBankReceiptModel plUpdbankrec;


            plUpdbankrec = new UpdateBankReceiptModel();
            plUpdbankrec.Ind = 5;
            plUpdbankrec.OrgID = GlobalSession.OrgID;
            plUpdbankrec.BrID = GlobalSession.BrID;
            plUpdbankrec.YrCD = GlobalSession.YrCD;
            plUpdbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);

            plUpdbankrec.CancelReason = ddlCancelReason.SelectedItem.Text;
            plUpdbankrec.DocNo = Convert.ToInt32(txtSearchVNo.Text);

            string uri = string.Format("UpdateBankReceipt/CancelVoucher");

            DataTable dtSave = CommonCls.ApiPostDataTable(uri, plUpdbankrec);

            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar No. - " + plUpdbankrec.DocNo + " is Cancel successfully ", true);
                    ClearAll();
                    ClearOnSave();
                }
                else
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel ", true);
                }

                //ClearAll();
            }
            else
            {
                ShowMessage("Voucher Is Not Cancelled", false);
            }
        }
        catch (Exception)
        {
            ShowMessage("Record Not Cancel Please Try Again.", false);
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