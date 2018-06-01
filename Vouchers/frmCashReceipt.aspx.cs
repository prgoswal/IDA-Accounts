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

public partial class frmCashReceipt : System.Web.UI.Page
{
    DataTable dt;
    DataTable dtgrdview, dtAccGstin;
    CashReceiptModel plcashrec;

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
    DataTable VSServiceNo
    {
        get { return (DataTable)ViewState["VSServiceNo"]; }
        set { ViewState["VSServiceNo"] = value; }
    }
    DataTable VSPlotServiceNo
    {

        get { return (DataTable)ViewState["VSPlotServiceNo"]; }
        set { ViewState["VSPlotServiceNo"] = value; }
    }

    int ServiceNoTaken
    {
        get { return (int)ViewState["ServiceNoTaken"]; }
        set { ViewState["ServiceNoTaken"] = value; }
    }



    DataTable VSPlotServiceDetails
    {
        get { return (DataTable)ViewState["VSPlotServiceDetails"]; }
        set { ViewState["VSPlotServiceDetails"] = value; }
    }

    public static string strcon = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
    //create new sqlconnection and connection to database by using connection string from web.config file  
    SqlConnection con = new SqlConnection(strcon);


    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ServiceNoTaken = 0;
            txtVoucherDate.Focus();
            ViewState["VchType"] = 1;
            //BindPloteServiceNo();
            //BindServiceDetail();
            BindAllCashReceiptDDL();
            //LoadCashAccount();
            //LoadAccountHead();
            //LoadNarration();
            //LoadLastVoucherNo();
        }


        //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
        //{
        //    btnSave.Visible = false;

        //}
        //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
        //{
        //    btnSave.Visible = true;
        //}

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

    void BindAllCashReceiptDDL()
    {
        try
        {
            plcashrec = new CashReceiptModel();
            plcashrec.Ind = 11;
            plcashrec.OrgID = GlobalSession.OrgID;
            plcashrec.BrID = GlobalSession.BrID;
            plcashrec.YrCD = GlobalSession.YrCD;
            plcashrec.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("CashReceipt/BindAllCashReceiptDDL");
            DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plcashrec);
            if (dsBindAllCRDDL.Tables.Count > 0)
            {
                DataTable dtAccountHead = dsBindAllCRDDL.Tables[0];
                DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];
                DataTable dtNarration = dsBindAllCRDDL.Tables[2];
                DataTable dtCashAccount = dsBindAllCRDDL.Tables[3];
                DataTable dtCostCenter = dsBindAllCRDDL.Tables[4];

                DataTable dtServiceList = dsBindAllCRDDL.Tables[5];
                DataTable dtServiceNo = dsBindAllCRDDL.Tables[6];

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
                    txtAccountHead.Items.Insert(0, new ListItem("---Select---", "0"));
                }
                ViewState["dtsevicelist"] = VSServiceList = dtServiceList;
                ViewState["VSServiceNo"] = VSServiceNo = dtServiceNo;



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
                        hfLastVoucherDate.Value = CommonCls.ConvertDateDB(dtLastVoucher.Rows[0]["LastDate"]);
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
            ShowMessage(ex.Message, false);
        }
    }

    void LoadLastVoucherNo() // Last Voucher No Insert
    {
        try
        {
            plcashrec = new CashReceiptModel()
            {
                Ind = 7,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
            };


            string uri = string.Format("CashReceipt/LastVoucherNo");
            DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, plcashrec);
            if (dtVoucher.Rows.Count > 0)
            {
                if (dtVoucher.Rows[0]["LastNo"].ToString() == "0")
                {
                    return;
                }
                txtLastVoucherNo.Text = dtVoucher.Rows[0][0].ToString();
                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucher.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtVoucher.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #region Old
    //void LoadNarration() // Narrations List Bind
    //{
    //    try
    //    {
    //        plcashrec = new CashReceiptModel()
    //        {
    //            Ind = 6,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };            

    //        string uri = string.Format("CashReceipt/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, plcashrec);
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
    //        plcashrec = new CashReceiptModel()
    //        {
    //            Ind = 5,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("CashReceipt/AccountHead");
    //        DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, plcashrec);
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



    //void LoadCashAccount() // Cash List Bind
    //{
    //    try
    //    {
    //        plcashrec = new CashReceiptModel()
    //        {
    //            Ind = 4,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("CashReceipt/LoadCashAccount");
    //        DataTable dtCashAccount = CommonCls.ApiPostDataTable(uri, plcashrec);
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
            //if (string.IsNullOrEmpty(txtAccountHead.Text)) // For Account Head Not Null Or Empty
            //{
            //    txtAccountHead.Focus();
            //    ShowMessage("Enter Account Head.", false);
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtServiceNo.Text))
            //{
            //    txtServiceNo.Focus();
            //    ShowMessage("Enter Service No.", false);
            //    return;
            //}

            if (ddlCashAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Cash Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Cash Account Can Not Be Same.", false);
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
            //    ShowMessage("Account Value Not Available", false);
            //    return;
            //}


            if (!string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Not Null
            {
                if (string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Copulsory On InvoiceNo
                {
                    txtInvoiceDate.Focus();
                    ShowMessage("Please Select Demand  Date.", false);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Demand  Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
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



            //Commented by ashish sir subhash sir and nagori sir 16-02-2018
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

            //Service Number Concept Start  (Check service number if availabe the set attribute otherwise add)

            DataTable dtAddServiceList = VSServiceList;
            DataView dvAddServiceList = new DataView(dtAddServiceList);

            dvAddServiceList.RowFilter = "Acccode = " + txtAccountHead.SelectedValue + "";

            if (dvAddServiceList.ToTable().Rows.Count > 0)
            {
                if (ServiceNoTaken == 1)
                {
                    ShowMessage("Two Control Account is not add on one Cash Receipt", false);
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
                DataRow[] rows = dtgrdview.Select("AccHeadValue=" + txtAccountHead.SelectedValue + " AND  InvoiceNo='" + InvoiceNo + "'");
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
            dr["AccHeadValue"] = txtAccountHead.SelectedItem.Value;          //error
            //dr["GSTIN"] = string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value) ? "" : ddlGSTINNo.SelectedItem.Value;

            //if (divPartySelect.Visible == true)
            //{
            //    dr["PartyID"] = ddlSecondaryParty.Items.Count > 0 ?
            //                        !string.IsNullOrEmpty(ddlSecondaryParty.SelectedItem.Value) ?
            //                        ddlSecondaryParty.SelectedItem.Value : "" : "";

            //    string BillNos = "";
            //    for (int i = 0; i < CbOutstandingBill.Items.Count; i++)
            //    {
            //        if (CbOutstandingBill.Items[i].Selected)
            //        {
            //            if (i == 0)
            //                BillNos = CbOutstandingBill.Items[i].Value;
            //            else
            //                BillNos += "," + CbOutstandingBill.Items[i].Value;
            //        }
            //    }
            //}


            dr["InvoiceNo"] = InvoiceNo;
            //dr["InvoiceNo"] = txtInVoiceNo.Text;
            dr["InvoiceDate"] = txtInvoiceDate.Text;
            dr["Amount"] = txtAmount.Text;
            dr["DrCr"] = ddlCrOrDr.SelectedItem.Value;
            dr["PartyName"] = txtPartyName.Text;
            //dr["serviceNo"] = txtServiceNo.Text;

            if (ddlCapitalRevenue.SelectedValue == "0")
            {
                dr["IsCapitalRevenue"] = 0;
            }
            else
            {
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(ddlCapitalRevenue.SelectedValue);
            }

            dtgrdview.Rows.Add(dr);
            ViewState["grdData"] = dtgrdview;
            gvCashReceipt.DataSource = dtgrdview;
            gvCashReceipt.DataBind();
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
        plcashrec = new CashReceiptModel();

        plcashrec.Ind = 12;
        plcashrec.OrgID = GlobalSession.OrgID;
        plcashrec.BrID = GlobalSession.BrID;
        plcashrec.YrCD = GlobalSession.YrCD;
        plcashrec.AccCode = CommonCls.ConvertIntZero(txtAccountHead.SelectedValue);
        string uri = string.Format("CashReceipt/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, plcashrec);
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
        plcashrec = new CashReceiptModel();
        plcashrec.Ind = 11;
        plcashrec.OrgID = GlobalSession.OrgID;
        plcashrec.BrID = GlobalSession.BrID;
        plcashrec.YrCD = GlobalSession.YrCD;
        plcashrec.VchType = Convert.ToInt32(ViewState["VchType"]);

        string uri = string.Format("CashReceipt/BindAllCashReceiptDDL");
        DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plcashrec);
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
            // btnSave.Enabled = false;

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
                    txtServiceNo.Enabled = true;
                    txtServiceNo.Focus();
                    ShowMessage("Invalid Service Number Please search first.", false);
                    return;
                }
            }

            #endregion







            if (ViewState["grdData"] == null || ((DataTable)ViewState["grdData"]).Rows.Count <= 0)
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

            if (string.IsNullOrEmpty(txtInvoiceTotalAmount.Text) || Convert.ToDecimal(txtInvoiceTotalAmount.Text) <= 0) //Invoice Total Amount.
            {
                ShowMessage("Voucher Net Amount Is Invalid. Please Check Your Entries.", false);
                return;
            }

            dtgrdview = (DataTable)ViewState["grdData"];
            DataTable dtcash = CashReceiptSchema();

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow dr = dtcash.NewRow();
                dr["OrgID"] = GlobalSession.OrgID;        //Company/End User ID
                dr["BrID"] = GlobalSession.BrID;          //Branch ID
                dr["VchType"] = Convert.ToInt32(ViewState["VchType"]);    //Document/Voucher Type
                dr["YrCD"] = GlobalSession.YrCD;          //Financial Year Code (2017-17)

                //dr["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date
                //dr["DocNo"] = CommonCls.ConvertIntZero(txtLastVoucherNo.Text);   //Voucher No.
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
                dr["AccCode2"] = Convert.ToInt32(ddlCashAccount.SelectedItem.Value);  //Selected Cash Account Code
                    
                dr["RefNo"] = 0; 
                dr["RefDate"] = "";

                if (item["DrCr"].ToString() == "Cr")
                    dr["AmountCr"] = Convert.ToDecimal(item["Amount"]);  //Dr Amount
                else
                    dr["AmountDr"] = Convert.ToDecimal(item["Amount"]);  //Cr Amount	
                dr["AdvanceInd"] = 0;//item["DrCr"] == "Dr" ? 1 : 0;     //AgainstAdvanceInd (If Receipt Made For Against Advance Taken From Debitor/Creditor Then It Goes 1 Else 0)
                dr["DocDesc"] = txtNarration.Text;                       //Narration
                dr["EntryType"] = 1;//                                   //EntryType - 1-Entry/2-Ammendment/3-Cancel
                dr["User"] = GlobalSession.UserID;         //UserID - User ID (Entry By Which User)
                dr["IP"] = GlobalSession.IP;                     //IPAddress - Client Machine IP Address
                dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(item["IsCapitalRevenue"]) == 0 ? 0 : item["IsCapitalRevenue"];

                dr["BillNo"] = item["InvoiceNo"].ToString();//Invoice No.
                dr["BillDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : ""; //Invoice Date

                //dr["IsCapitalRevenue"] = CommonCls.ConvertIntZero(ddlCapitalRevenue.SelectedValue);
                //dr["BillNos"] = item["BillNos"].ToString();
                //if (Convert.ToInt32(item["PartyID"].ToString()) > 0)
                //{
                //    dr["PartyID"] = item["PartyID"].ToString();
                //}
                dtcash.Rows.Add(dr);
            }

            plcashrec = new CashReceiptModel();

            plcashrec.Ind = 1;
            plcashrec.OrgID = GlobalSession.OrgID;
            plcashrec.BrID = GlobalSession.BrID;
            plcashrec.VchType = Convert.ToInt32(ViewState["VchType"]);
            plcashrec.YrCD = GlobalSession.YrCD;
            //plcashrec.Dt = dtcash;
            plcashrec.Dt = JsonConvert.SerializeObject(dtcash);
            plcashrec.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

            plcashrec.PartyName = txtPartyName.Text;
            plcashrec.PartyAddress = txtPartyAddress.Text;
            plcashrec.PartyGstIN = txtpartyGstIN.Text;
            plcashrec.ServiceNo = CommonCls.ConvertIntZero(txtServiceNo.Text);
            // plcashrec.CapitalRevenue = Convert.ToInt32(ddlCapitalRevenue.SelectedValue);

            string uri = string.Format("CashReceipt/SaveCashReceipt");
            DataTable dtSaveCashReceipt = CommonCls.ApiPostDataTable(uri, plcashrec);
            if (dtSaveCashReceipt.Rows.Count > 0)
            {
                if (dtSaveCashReceipt.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    ClearOnSave();
                    chkBankEntry_CheckedChanged(sender, e);
                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveCashReceipt.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSaveCashReceipt.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                    ShowMessage("Record Save Successfully With Voucher No. " + VoucherNo, true);
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date " + VoucherNo + " - " + VoucherDate;

                    hfLastVoucherDate.Value = VoucherDate;

                    CallReport(VoucherNo, VoucherDate);

                }
                else if (dtSaveCashReceipt.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    ShowMessage("No Record Found For Given Vouchar No." + txtVoucherNo.Text + " & Date" + txtVoucherDate.Text, false);
                    txtVoucherNo.Focus();
                }

                else if (dtSaveCashReceipt.Rows[0]["ReturnInd"].ToString() == "3")
                {
                    ShowMessage(" Back Date Vouchar Already Available For Given Vouchar No." + txtVoucherNo.Text + " & Date." + txtVoucherDate.Text + " Please Give Correct Vouchar No. ", false);
                    txtVoucherNo.Focus();
                }

                //else if (dtSaveCashReceipt.Rows[0]["ReturnInd"].ToString() == "9")
                //{
                //    ShowMessage("Exceeded entry for Voucher Date - " + txtVoucherDate.Text, false);//" Use Minimum Date - " + CommonCls.ConvertDateDB(dtSaveCashReceipt.Rows[0]["DocDate"]) 
                //    txtVoucherDate.Focus();
                //}
                else
                {
                    ShowMessage("Record Not Saved. Please Try Again.", false);
                }
            }
            else
            {
                ShowMessage("Record Not Saved. Please Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        //   btnSave.Enabled = true;

    }



    void ClearOnSave()
    {
        txtServiceNo.Enabled = false;
        ddlCashAccount.ClearSelection();

        txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = txtNarration.Text = "";
        ddlCostCenter.ClearSelection();
        txtInvoiceTotalAmount.Text = "0";
        gvCashReceipt.DataSource = null;
        gvCashReceipt.DataBind();
        ViewState["grdData"] = null;
        //LoadLastVoucherNo();
        tdVNo.Visible = false;
        thVNo.Visible = false;
        chkBankEntry.Checked = false;
        txtInvoiceDate.Text = txtVoucherDate.Text = "";
        ServiceNoTaken = 0;
    }

    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkBankEntry.Checked == true)
            {
                thVNo.Visible = true;
                tdVNo.Visible = true;
            }

            if (Convert.ToInt32(txtAccountHead.SelectedItem.Value) <= 0)
            {
                ShowMessage("Account Head Not Available.", false);
                return;
            }

            if (ddlCashAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Cash Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Cash Account Can Not Be Same.", false);
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
                    ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0000"));
                    ddlGSTINNo.SelectedIndex = 0;
                    ddlGSTINNo.Focus();
                }
                else
                {
                    ddlGSTINNo.DataSource = null;
                    ddlGSTINNo.DataBind();
                }

                plcashrec = new CashReceiptModel()
                {
                    Ind = 6,
                    OrgID = GlobalSession.OrgID,
                    BrID = GlobalSession.BrID,
                    YrCD = GlobalSession.YrCD,
                    VchType = Convert.ToInt32(ViewState["VchType"]),
                    AccCode = Convert.ToInt32(txtAccountHead.SelectedItem.Value),
                };

                string uri = string.Format("CashReceipt/PartySelect");
                DataSet dsPartySelect = CommonCls.ApiPostDataSet(uri, plcashrec);
                //divPartySelect.Visible = true; //Party Selection Section Visible
                //btnRegToggle.Visible = false;   //Outstanding Bill UnVisible
                //ddlSecondaryParty.Visible = false; //SecondaryParty UnVisible
                //if (dsPartySelect.Tables.Count > 0)
                //{

                //    if (dsPartySelect.Tables[0].TableName == "SecondaryParty") //SecondaryParty Visible
                //    {
                //        ddlSecondaryParty.Visible = true;
                //        ddlSecondaryParty.DataSource = dsPartySelect;
                //        ddlSecondaryParty.DataTextField = "PartyName";
                //        ddlSecondaryParty.DataValueField = "PartyID";
                //        ddlSecondaryParty.DataBind();
                //    }
                //    else if (dsPartySelect.Tables[0].TableName == "OutstandingBill") //Outstanding Bill Visible
                //    {
                //        btnRegToggle.Visible = true;
                //        CbOutstandingBill.DataSource = dsPartySelect;
                //        CbOutstandingBill.DataTextField = "BillAmt";
                //        CbOutstandingBill.DataValueField = "BillNo";
                //        CbOutstandingBill.DataBind();
                //    }
                //    else
                //    {
                //        divPartySelect.Visible = false;
                //        ddlSecondaryParty.DataSource = CbOutstandingBill.DataSource = null;
                //        ddlSecondaryParty.DataBind(); CbOutstandingBill.DataBind();
                //    }
                //}
                ddlGSTINNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void gvCashReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtgrdview = (DataTable)ViewState["grdData"];
                dtgrdview.Rows[rowIndex].Delete();

                ViewState["grdData"] = dtgrdview;
                gvCashReceipt.DataSource = dtgrdview;
                gvCashReceipt.DataBind();

                CalculateTotalInvoiceAmount();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

            txtServiceNo.Enabled = false;
            gvCashReceipt.DataSource = ViewState["grdData"] = null;
            gvCashReceipt.DataBind();
            ClearAll();
            ddlCostCenter.ClearSelection();
            txtVoucherDate.Text =
            txtInvoiceTotalAmount.Text = txtVoucherNo.Text = "";
            chkBankEntry.Checked = false;
            chkBankEntry_CheckedChanged(sender, e);
            ddlCashAccount.ClearSelection();
            txtServiceNo.Text = txtpartyGstIN.Text = txtPartyAddress.Text = txtPartyName.Text = txtNarration.Text = "";
            ServiceNoTaken = 0;
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
        txtInvoiceTotalAmount.Text = Convert.ToString(crAmount - drAmount);
    }

    void ClearAll() // Clear Selection On Add Click.
    {
        txtInVoiceNo.Text = txtInvoiceDate.Text = txtAmount.Text = lblMsg.Text = txtNarration.Text = string.Empty;
        ddlGSTINNo.DataSource = new DataTable(); ddlGSTINNo.DataBind();
        ddlCrOrDr.ClearSelection();
        // txtServiceNo.Text = "";
        txtAccountHead.ClearSelection();


        //txtPartyName.Text = string.Empty;
        //txtPartyAddress.Text = string.Empty;
        //txtpartyGstIN.Text = string.Empty;
    }

    DataTable CashReceiptSchema()
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

    void ShowMessage(string Message, bool type)
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
        HT.Add("Heading", "Cash Receipt");
        HT.Add("Ind", 1);
        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);

        HT.Add("DocTypeID", Convert.ToInt16(ViewState["VchType"]));
        HT.Add("Voucharno", VoucherNo);
        HT.Add("VoucharDate", VoucherDate.Substring(6, 4) + "/" + VoucherDate.Substring(3, 2) + "/" + VoucherDate.Substring(0, 2));

        VouchersReport.ReportName = "RptVoucher";
        VouchersReport.FileName = "Voucher";
        VouchersReport.ReportHeading = "Cash Receipt";
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
                ServiceNoTaken = 1;
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
}