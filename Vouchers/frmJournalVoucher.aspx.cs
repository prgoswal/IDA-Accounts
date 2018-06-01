using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmJournalVoucher : System.Web.UI.Page
{
    JournalVoucherModel objJVModel;
    DataTable dtgrdview;

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


    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            txtVoucherDate.Focus();
            txtDrAmount.Text = txtCrAmount.Text = "0";
            ViewState["VchType"] = 11;
            BindAllJVDDL();
            //LoadNarration();
            //LoadAccountHead();
            //LoadLastVoucherNo();
            //if (Convert.ToInt32(Session["VoucherRead"].ToString()) == 21)
            //{
            //    btnSave.Visible = false;

            //}
            //if (Convert.ToInt32(Session["VoucherWrite"].ToString()) == 22 || Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24)
            //{
            //    btnSave.Visible = true;
            //}

        }
    }

    void BindAllJVDDL()
    {
        try
        {
            objJVModel = new JournalVoucherModel();
            objJVModel.Ind = 11;
            objJVModel.OrgID = GlobalSession.OrgID;
            objJVModel.BrID = GlobalSession.BrID;
            objJVModel.YrCD = GlobalSession.YrCD;
            objJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("JournalVoucher/BindAllJVDDL");
            DataSet dsBindAllJV = CommonCls.ApiPostDataSet(uri, objJVModel);
            if (dsBindAllJV.Tables.Count > 0)
            {
                DataTable dtAccountHead = VSBudgetHead = dsBindAllJV.Tables[0];
                DataTable dtLastVoucherNo = dsBindAllJV.Tables[1];
                DataTable dtNarration = dsBindAllJV.Tables[2];
                DataTable dtCostCenter = dsBindAllJV.Tables[3];

                if (dtAccountHead.Rows.Count > 0)
                {
                    ViewState["dtAccGstin"] = dtAccountHead;
                    DataView dvAccCode = new DataView(dtAccountHead);
                    DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

                    ddlAccountHead.DataSource = dtAccList;
                    ddlAccountHead.DataTextField = "AccName";
                    ddlAccountHead.DataValueField = "AccCode";
                    ddlAccountHead.DataBind();
                    ddlAccountHead.Items.Insert(0, new ListItem("---Select---", "0"));
                }

                //if (dtNarration.Rows.Count > 0)
                //{
                //    txtNarration.DataSource = dtNarration;
                //    txtNarration.DataTextField = "NarrationDesc";
                //    txtNarration.DataBind();
                //}


                // Last Voucher No And Date
                if (dtLastVoucherNo.Rows.Count > 0)
                {
                    if (CommonCls.ConvertIntZero(dtLastVoucherNo.Rows[0]["LastNo"]) != 0)
                    {
                        hfLastVoucherDate.Value = CommonCls.ConvertDateDB(dtLastVoucherNo.Rows[0]["LastDate"]);
                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucherNo.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtLastVoucherNo.Rows[0]["LastDate"]);
                    }
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
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void LoadLastVoucherNo()
    {
        try
        {
            objJVModel = new JournalVoucherModel()
            {
                Ind = 7,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
            };


            string uri = string.Format("JournalVoucher/LastVoucherNo");
            DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, objJVModel);
            if (dtVoucher.Rows.Count > 0)
            {
                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucher.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtVoucher.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #region Old

    //private void LoadAccountHead()
    //{
    //    try
    //    {
    //        objJVModel = new JournalVoucherModel()
    //        {
    //            Ind = 35,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("JournalVoucher/AccountHead");
    //        DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, objJVModel);
    //        if (dtAccGstin.Rows.Count > 0)
    //        {
    //            ViewState["dtAccGstin"] = dtAccGstin;
    //            DataView dvAccCode = new DataView(dtAccGstin);
    //            DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

    //            ddlAccountHead.DataSource = dtAccList;
    //            ddlAccountHead.DataTextField = "AccName";
    //            ddlAccountHead.DataValueField = "AccCode";
    //            ddlAccountHead.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}

    //private void LoadNarration()
    //{
    //    try
    //    {
    //        objJVModel = new JournalVoucherModel()
    //        {
    //            Ind = 6,
    //            OrgID = GlobalSession.OrgID,
    //            BrID = GlobalSession.BrID,
    //            YrCD = GlobalSession.YrCD,
    //            VchType = Convert.ToInt32(ViewState["VchType"]),
    //        };

    //        string uri = string.Format("JournalVoucher/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, objJVModel);
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

    protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtInVoiceNo.Focus();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlAccountHead.Text)) // For Account Head Not Null Or Empty
        {
            ddlAccountHead.Focus();
            ShowMessage("Enter Account Head.", false);
            return;
        }
        if (ViewState["DtGrdView"] != null)
        {
            DataTable dtAccount = (DataTable)ViewState["DtGrdView"];
            DataRow[] rows = dtAccount.Select("AccHeadValue=" + ddlAccountHead.SelectedItem.Value);
            if (rows.Count() >= 1)
            {
                ddlAccountHead.ClearSelection();
                ddlAccountHead.Focus();
                ShowMessage("This Account Head Alerady Added.", false);
                return;
            }
        }
        try // For txtAccount Head Value Shouldn't null,0 or Garbage.
        {
            if (ddlAccountHead.SelectedItem.Value == null || Convert.ToInt32(ddlAccountHead.SelectedItem.Value) == 0) // For Account Head Code Not Null Or Empty
            {
                ddlAccountHead.Focus();
                ShowMessage("Account Value Not Available", false);
                return;
            }
        }
        catch (Exception)
        {
            ddlAccountHead.Focus();
            ShowMessage("This Account Head Value Not Available.", false);
            return;
        }
        if (string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) == 0)
        {
            txtAmount.Focus();
            ShowMessage("Enter Amount", false);
            return;
        }
        if (!string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Not Null
        {
            if (string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Copulsory On InvoiceNo
            {
                txtInvoiceDate.Focus();
                ShowMessage("Please Enter Invoice Date.", false);
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Not Null
        {
            if (string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Copulsory On InvoiceDate
            {
                txtInVoiceNo.Focus();
                ShowMessage("Please Enter Invoice No.", false);
                return;
            }
        }
        if (txtInvoiceDate.Text != "")
        {
            bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate) // For Voucher Date Between Financial Year.
            {
                txtInvoiceDate.Focus();
                ShowMessage("Invoice Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return;
            }
        }


        DataTable dtGRD = VSBudgetHead;
        if (dtGRD.Rows.Count > 0)
        {
            DataView dv31 = new DataView(dtGRD);

            dv31.RowFilter = "AccCode  = " + ddlAccountHead.SelectedValue + "";
            if (CommonCls.ConvertIntZero(dv31.ToTable().Rows[0]["MainGroupID"].ToString()) == 25 || CommonCls.ConvertIntZero(dv31.ToTable().Rows[0]["MainGroupID"].ToString()) == 24)
            {
                bool BudgetHeadStatus = CheckBudgetHead();
                if (BudgetHeadStatus == true)
                {
                    if (ddlCostCenter.SelectedValue == "0")
                    {
                        ShowMessage("Select Cost Centre.", false);
                        ddlCostCenter.Focus();
                        return;
                    }
                }

            }
        }

        AddRowInGrid();
        CalculateTotalInvoiceAmount();
        ClearAllHeadValue();

        //As Per Nagori Sir
        //if (grdJournalVoucher.Rows.Count == 0)
        //{
        //    if (Convert.ToInt32(ddlAccountHead.SelectedItem.Value) >= 900000)
        //    {
        //        hfISGL.Value = "1";
        //        AddRowInGrid();
        //        CalculateTotalInvoiceAmount();
        //        ClearAllHeadValue();
        //    }
        //    else if (Convert.ToInt32(ddlAccountHead.SelectedValue) > 700000 && Convert.ToInt32(ddlAccountHead.SelectedValue) < 900000)
        //    {
        //        hfISGL.Value = "2";
        //        AddRowInGrid();
        //        CalculateTotalInvoiceAmount();
        //        ClearAllHeadValue();
        //    }
        //    else
        //    {
        //        hfISGL.Value = "0";
        //        AddRowInGrid();
        //        CalculateTotalInvoiceAmount();
        //        ClearAllHeadValue();
        //    }
        //}
        //else if (grdJournalVoucher.Rows.Count >= 1)
        //{
        //    if (Convert.ToInt32(ddlAccountHead.SelectedItem.Value) > 900000 && hfISGL.Value == "0")
        //    {
        //        ddlAccountHead.Focus();
        //        ShowMessage("You can not use Subsidary and General Ledger Account on Same Voucher", false);
        //        return;
        //    }
        //    else if (Convert.ToInt32(ddlAccountHead.SelectedValue) <= 700000 && hfISGL.Value == "1")
        //    {
        //        ddlAccountHead.Focus();
        //        ShowMessage("You can not use Subsidary and General Ledger Account on Same Voucher", false);
        //        return;
        //    }
        //    else if (Convert.ToInt32(ddlAccountHead.SelectedItem.Value) <= 700000 && hfISGL.Value == "2")
        //    {
        //        ddlAccountHead.Focus();
        //        ShowMessage("You can not use Subsidary and General Ledger Account on Same Voucher", false);
        //        return;
        //    }
        //    else if (Convert.ToInt32(ddlAccountHead.SelectedValue) > 700000 && Convert.ToInt32(ddlAccountHead.SelectedValue) < 900000)
        //    {
        //        DataTable dtJV = (DataTable)ViewState["DtGrdView"];
        //        bool jvAH = false;
        //        foreach (DataRow row in dtJV.Rows)
        //        {
        //            int accCode = Convert.ToInt32(row["AccHeadValue"].ToString());
        //            if (accCode > 942000 && accCode < 999999)
        //            {
        //                jvAH = true;
        //                break;
        //            }
        //        }
        //        if (jvAH == true)
        //        {
        //            AddRowInGrid();
        //            CalculateTotalInvoiceAmount();
        //            ClearAllHeadValue();
        //        }
        //        else
        //        {
        //            ddlAccountHead.Focus();
        //            ShowMessage("Not Allowed!", false);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        AddRowInGrid();
        //        CalculateTotalInvoiceAmount();
        //        ClearAllHeadValue();
        //    }
        //}
    }


    private bool CheckBudgetHead()
    {
        objJVModel = new JournalVoucherModel();
        objJVModel.Ind = 12;
        objJVModel.OrgID = GlobalSession.OrgID;
        objJVModel.BrID = GlobalSession.BrID;
        objJVModel.YrCD = GlobalSession.YrCD;
        objJVModel.AccCode = CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue);
        string uri = string.Format("JournalVoucher/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, objJVModel);
        if (dtCashReceipt.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtCashReceipt.Rows[0]["Cnt"].ToString()) > 0)
            {
                return true;
            }
        }
        return false;
    }
    public void AddRowInGrid()
    {
        if (ViewState["DtGrdView"] != null)
        {
            dtgrdview = (DataTable)ViewState["DtGrdView"];
        }
        else
        {
            CreatGridDt();
        }
        DataRow dr = dtgrdview.NewRow();
        dr["AcctHeadText"] = ddlAccountHead.SelectedItem.Text;
        dr["AccHeadValue"] = ddlAccountHead.SelectedItem.Value;
        dr["InvoiceNo"] = txtInVoiceNo.Text;
        dr["InvoiceDate"] = txtInvoiceDate.Text;
        dr["Amount"] = txtAmount.Text;
        dr["DrCr"] = ddlDrCr.SelectedItem.Value;
        dtgrdview.Rows.Add(dr);
        grdJournalVoucher.DataSource = ViewState["DtGrdView"] = dtgrdview;
        grdJournalVoucher.DataBind();
    }

    void ClearAllHeadValue()
    {
        ddlAccountHead.ClearSelection();
        txtInVoiceNo.Text = txtInvoiceDate.Text = txtAmount.Text = "";
        ddlDrCr.ClearSelection();
        ddlAccountHead.Focus();
    }

    void LastVoucherDate()
    {
        objJVModel = new JournalVoucherModel();
        objJVModel.Ind = 11;
        objJVModel.OrgID = GlobalSession.OrgID;
        objJVModel.BrID = GlobalSession.BrID;
        objJVModel.YrCD = GlobalSession.YrCD;
        objJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

        string uri = string.Format("JournalVoucher/BindAllJVDDL");
        DataSet dsBindAllJVDDL = CommonCls.ApiPostDataSet(uri, objJVModel);
        if (dsBindAllJVDDL.Tables.Count > 0)
        {
            DataTable dtLastVoucher = dsBindAllJVDDL.Tables[1];

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
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }


        if (chkBankEntry.Checked == false)
        {
            if (!string.IsNullOrEmpty(hfLastVoucherDate.Value))
            {
                DateTime VoucherDate = DateTime.ParseExact(txtVoucherDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime lastVoucherDate = DateTime.ParseExact(hfLastVoucherDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture); //server

                if (VoucherDate < lastVoucherDate)
                {
                    txtVoucherDate.Focus();
                    ShowMessage("Please Use Back Date Entry Option.", false);
                    return;
                }
            }

            //string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

            //if (CommonCls.ConvertDateDB(txtVoucherDate.Text) != CommonCls.ConvertDateDB(CurrentDate))
            //{
            //    txtVoucherDate.Focus();
            //    ShowMessage("Please Enter Current Date", false);
            //    return;
            //}
        }


        if (chkBankEntry.Checked == true)
        {
            if (txtVoucherNo.Text == "" || Convert.ToInt32(txtVoucherNo.Text) == 0)
            {
                txtVoucherNo.Focus();
                ShowMessage("Please Enter Voucher No.", false);
                return;
            }
            if (txtVoucherDate.Text == "")
            {
                txtVoucherDate.Focus();
                ShowMessage("Please Enter Voucher Date.", false);
                return;
            }

            LastVoucherDate();

            DateTime CurrentDate = DateTime.ParseExact(CommonCls.ConvertDateDB(VsdtLastVoucherDate.Rows[0]["LastDate"]), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime VDate = DateTime.ParseExact(txtVoucherDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

            if (VDate >= CurrentDate)
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Is Should Not Be Current Date For Back Date Entry", false);
                return;
            }

            //string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

            //if (CommonCls.ConvertDateDB(txtVoucherDate.Text) == CommonCls.ConvertDateDB(CurrentDate))
            //{
            //    txtVoucherDate.Focus();
            //    ShowMessage("Voucher Date Is Should Not Be Current Date For Back Date Entry", false);
            //    return;
            //}

        }


        if (string.IsNullOrEmpty(txtVoucherDate.Text)) // For Voucher Date Not Be Null
        {
            txtVoucherDate.Focus();
            ShowMessage("Enter Voucher Date!", false);
            return;
        }


        if (GlobalSession.CCCode == 1)
        {
            if (ddlCostCenter.SelectedValue == "0")
            {
                ShowMessage("Select Cost Centre!", false);
                return;
            }
        }
        bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate) // For Voucher Date Between Financial Year.
        {
            txtVoucherDate.Focus();
            ShowMessage("Voucher Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
            return;
        }
        if (Convert.ToDecimal(txtDrAmount.Text) > 0 && Convert.ToDecimal(txtCrAmount.Text) > 0)
        {
            if (Convert.ToDecimal(txtDrAmount.Text) == Convert.ToDecimal(txtCrAmount.Text))
            {
                objJVModel = new JournalVoucherModel();
                objJVModel.Ind = 1;
                objJVModel.OrgID = GlobalSession.OrgID;
                objJVModel.BrID = GlobalSession.BrID;
                objJVModel.YrCD = GlobalSession.YrCD;
                objJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);
                objJVModel.IDARefNo = "Old VNo-" + txtIDARefNo.Text; //IDA Ref. No.

                //if (!string.IsNullOrEmpty(txtIDARefNo.Text))
                //{
                //    objJVModel.IDARefNo = "Old VNo-" + txtIDARefNo.Text;
                //}

                //objJVModel.DtJV = CreateJVData();
                objJVModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

                DataTable DtJVoucher = CreateJVData();
                objJVModel.DtJV = JsonConvert.SerializeObject(DtJVoucher);


                string uri = string.Format("JournalVoucher/SaveJV");
                DataTable dtSaveBankPayment = CommonCls.ApiPostDataTable(uri, objJVModel);
                if (dtSaveBankPayment.Rows.Count > 0)
                {
                    if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "1")
                    {
                        ClearAll();
                        txtIDARefNo.Text = "";
                        chkBankEntry.Checked = false;
                        chkBankEntry_CheckedChanged(sender, e);
                        string VoucherNo, VoucherDate;
                        VoucherNo = dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString();
                        VoucherDate = Convert.ToDateTime(dtSaveBankPayment.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString() + " - " + Convert.ToDateTime(dtSaveBankPayment.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                        hfLastVoucherDate.Value = Convert.ToDateTime(dtSaveBankPayment.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                        ShowMessage("Data Submitted Successfully With Voucher No. " + dtSaveBankPayment.Rows[0]["DocMaxNo"], true);
                        //CallReport(dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString(), Convert.ToDateTime(dtSaveBankPayment.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy"));

                        CallReport(VoucherNo, VoucherDate);
                    }


                    else if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "2")
                    {
                        ShowMessage("No Record Found For Given Vouchar No." + txtVoucherNo.Text + " & Date" + txtVoucherDate.Text, false);
                        txtVoucherNo.Focus();
                    }

                    else if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "3")
                    {
                        ShowMessage(" Back Date Vouchar Already Available For Given Vouchar No." + txtVoucherNo.Text + " & Date." + txtVoucherDate.Text + " Please Give Correct Vouchar No. ", false);
                        txtVoucherNo.Focus();
                    }

                    else if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "0")
                    {
                        ShowMessage("Record Not Save Please Try Again.", false);
                    }
                }
            }
            else
            {
                ShowMessage("Amount MissMatch on Voucher!", false);
            }
        }
        else
        {
            ShowMessage("Amount MissMatch on Voucher!", false);
        }
    }

    DataTable CreatGridDt() // Create Grid Structure
    {
        dtgrdview = new DataTable();
        dtgrdview.Columns.Add("AccHeadValue", typeof(string));
        dtgrdview.Columns.Add("AcctHeadText", typeof(string));
        dtgrdview.Columns.Add("InvoiceNo", typeof(string));
        dtgrdview.Columns.Add("InvoiceDate", typeof(string));
        dtgrdview.Columns.Add("Amount", typeof(string));
        dtgrdview.Columns.Add("DrCr", typeof(string));
        return dtgrdview;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        chkBankEntry.Checked = false;
        chkBankEntry_CheckedChanged(sender, e);
        txtIDARefNo.Text = "";
    }

    protected void grdJournalVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                dtgrdview = (DataTable)ViewState["DtGrdView"];
                dtgrdview.Rows[rowIndex].Delete();

                ViewState["DtGrdView"] = dtgrdview;
                grdJournalVoucher.DataSource = dtgrdview;
                grdJournalVoucher.DataBind();
            }
            CalculateTotalInvoiceAmount();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void CalculateTotalInvoiceAmount() // For Invoice Amount Dr / CR
    {
        decimal crAmount = 0;
        decimal drAmount = 0;
        for (int rowIndex = 0; dtgrdview.Rows.Count > rowIndex; rowIndex++)
        {
            string DrCr = dtgrdview.Rows[rowIndex][5].ToString();
            if (DrCr == "Cr")
            {
                crAmount = crAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex][4]);
            }
            else
            {
                drAmount = drAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex][4]);
            }
        }
        txtCrAmount.Text = crAmount.ToString();
        txtDrAmount.Text = drAmount.ToString();
    }

    void ClearAll()
    {
        txtVoucherDate.Text = "";
        ViewState["DtGrdView"] = null;
        grdJournalVoucher.DataSource = new DataTable();
        grdJournalVoucher.DataBind();
        txtNarration.Text = "";
        txtDrAmount.Text = txtCrAmount.Text = "0";
        ddlAccountHead.ClearSelection();
        txtVoucherDate.Focus();
        ddlCostCenter.ClearSelection();
    }

    DataTable DtJVSchema()
    {
        DataTable dtPurchase = new DataTable();
        dtPurchase.Columns.Add("OrgID", typeof(int));
        dtPurchase.Columns.Add("BrID", typeof(int));
        dtPurchase.Columns.Add("VchType", typeof(int));
        dtPurchase.Columns.Add("YrCD", typeof(int));
        dtPurchase.Columns.Add("DocDate", typeof(string));
        dtPurchase.Columns.Add("DocNo", typeof(int));
        dtPurchase.Columns.Add("AccCode", typeof(int));
        dtPurchase.Columns.Add("AccGst", typeof(string));
        dtPurchase.Columns.Add("AccCode2", typeof(int));
        dtPurchase.Columns.Add("InvoiceNo", typeof(int));
        dtPurchase.Columns.Add("InvoiceDate", typeof(string));
        dtPurchase.Columns.Add("AmountDr", typeof(decimal));
        dtPurchase.Columns.Add("AmountCr", typeof(decimal));
        dtPurchase.Columns.Add("AdvanceInd", typeof(int));
        dtPurchase.Columns.Add("DocDesc", typeof(string));
        dtPurchase.Columns.Add("EntryType", typeof(int));
        dtPurchase.Columns.Add("UserID", typeof(int));
        dtPurchase.Columns.Add("IP", typeof(string));
        dtPurchase.Columns.Add("BillNo", typeof(string));
        dtPurchase.Columns.Add("BillDate", typeof(string));
        return dtPurchase;
    }

    DataTable CreateJVData()
    {
        DataTable dtCJVData = new DataTable();
        try
        {
            dtCJVData = DtJVSchema(); //new DataTable();

            dtgrdview = (DataTable)ViewState["DtGrdView"];

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow drCreateJVData = dtCJVData.NewRow();
                drCreateJVData["OrgID"] = GlobalSession.OrgID;
                drCreateJVData["BrID"] = GlobalSession.BrID;
                drCreateJVData["VchType"] = Convert.ToInt32(ViewState["VchType"]);
                drCreateJVData["YrCD"] = GlobalSession.YrCD;
                drCreateJVData["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date;
                // drCreateJVData["DocNo"] = "0";   //Voucher No.



                if (chkBankEntry.Checked == true)//if Back Date Entry is True
                {

                    drCreateJVData["DocNo"] = CommonCls.ConvertIntZero(txtVoucherNo.Text);   //Voucher No.
                }
                else
                {
                    drCreateJVData["DocNo"] = "0";   //For Current Voucher
                }


                drCreateJVData["AccCode"] = CommonCls.ConvertIntZero(item["AccHeadValue"]);  //Selected Account Head Code
                drCreateJVData["AccGst"] = "";                
                drCreateJVData["AccCode2"] = "0";

                drCreateJVData["InvoiceNo"] = 0;
                drCreateJVData["InvoiceDate"] = "";

                if (item["DrCr"].ToString() == "Dr")
                    drCreateJVData["AmountDr"] = CommonCls.ConvertDecimalZero(item["Amount"]);
                else
                    drCreateJVData["AmountCr"] = CommonCls.ConvertDecimalZero(item["Amount"]);

                drCreateJVData["AdvanceInd"] = "0";
                drCreateJVData["DocDesc"] = txtNarration.Text;
                drCreateJVData["EntryType"] = 1;
                drCreateJVData["UserID"] = GlobalSession.UserID;
                drCreateJVData["IP"] = GlobalSession.IP;
                drCreateJVData["BillNo"] = CommonCls.ConvertIntZero(item["InvoiceNo"]);
                drCreateJVData["BillDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : "";

                dtCJVData.Rows.Add(drCreateJVData);
            }
        }
        catch (Exception ex)
        {

        }
        return dtCJVData;
    }

    void CallReport(string VoucherNo, string VoucherDate)
    {
        Hashtable HT = new Hashtable();
        HT.Add("CompName", GlobalSession.OrgName);
        HT.Add("BranchName", GlobalSession.BrName);
        HT.Add("Heading", "JOURNAL VOUCHER");
        HT.Add("Ind", 1);
        HT.Add("OrgID", GlobalSession.OrgID);
        HT.Add("BrID", GlobalSession.BrID);
        HT.Add("yrcode", GlobalSession.YrCD);

        HT.Add("DocTypeID", 11);
        HT.Add("Voucharno", VoucherNo);
        HT.Add("VoucharDate", VoucherDate.Substring(6, 4) + "/" + VoucherDate.Substring(3, 2) + "/" + VoucherDate.Substring(0, 2));

        VouchersReport.ReportName = "RptJournalVoucher";
        VouchersReport.FileName = "JournalVoucher";
        VouchersReport.ReportHeading = "Journal Voucher";
        VouchersReport.HashTable = HT;
        VouchersReport.AskBeforePrint = true;
        VouchersReport.AskMessage = "Do You Want to Print Receipt?";
        VouchersReport.ShowReport();
    }

    //public void CallReport(string Voucharno, string VoucharDate)
    //{

    //    Hashtable HT = new Hashtable();

    //    HT.Add("CompName", GlobalSession.OrgName);
    //    HT.Add("BranchName", GlobalSession.BrName);
    //    HT.Add("Heading", "JOURNAL VOUCHER");
    //    HT.Add("Ind", 1);
    //    HT.Add("OrgID", GlobalSession.OrgID);
    //    HT.Add("BrID", GlobalSession.BrID);
    //    HT.Add("yrcode", GlobalSession.YrCD);
    //    HT.Add("DocTypeID", 11);
    //    HT.Add("Voucharno", Convert.ToInt32(Voucharno));
    //    HT.Add("VoucharDate", VoucharDate);


    //    VouchersReport.ReportName = "RptJournalVoucher";
    //    VouchersReport.FileName = "JournalVoucher";
    //    VouchersReport.ReportHeading = "Journal Voucher";
    //    VouchersReport.HashTable = HT;

    //    VouchersReport.AskBeforePrint = true;
    //    VouchersReport.AskMessage = "Do You Want to Print Voucher";

    //    VouchersReport.ShowReport();
    //}


    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
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


}