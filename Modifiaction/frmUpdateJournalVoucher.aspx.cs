using AjaxControlToolkit;
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

public partial class Modifiaction_frmUpdateJournalVoucher : System.Web.UI.Page
{
    #region Declaration
    UpdateJournalVoucherModel objUJVModel;
    DataTable dtgrdview;
    int RowIndex
    {
        get { return ViewState["rowIndex"] == null ? -1 : Convert.ToInt16(ViewState["rowIndex"]); }
        set { ViewState["rowIndex"] = value; }
    }

    DataTable VsdtAccountHead
    {
        get { return (DataTable)ViewState["dtJV"]; }
        set { ViewState["dtJV"] = value; }
    }

    DataTable VsdtGV
    {
        get { return (DataTable)ViewState["DtGrdView"]; }
        set { ViewState["DtGrdView"] = value; }
    }

    DataTable VsdtNarration
    {
        get { return (DataTable)ViewState["dtNarration"]; }
        set { ViewState["dtNarration"] = value; }
    }


    DataTable VSBudgetHead
    {
        get { return (DataTable)ViewState["VSBudgetHead"]; }
        set { ViewState["VSBudgetHead"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            txtVoucherDate.Focus();
            txtDrAmount.Text = txtCrAmount.Text = "0";
            ViewState["VchType"] = 11;
            BindAllUpdJVDDL();
            BindCancelReason();

            //LoadNarration();
            //LoadAccountHead();
            //LoadLastVoucherNo();
            txtVoucherNo.Focus();

            //if (Convert.ToInt32(Session["VoucherCancel"].ToString()) == 24 || GlobalSession.IsAdmin == 1)
            //{
            //    btnUpdate.Visible = true;
            //    btnCancel.Visible = true;
            //}
            //if (Convert.ToInt32(Session["VoucherUpdate"].ToString()) == 23 || GlobalSession.IsAdmin == 1)
            //{
            //    btnUpdate.Visible = true;
            //}

        }
    }

    private void BindCancelReason()
    {
        try
        {

            objUJVModel = new UpdateJournalVoucherModel();
            objUJVModel.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, objUJVModel);
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
    void BindAllUpdJVDDL()
    {
        try
        {
            objUJVModel = new UpdateJournalVoucherModel();
            objUJVModel.Ind = 11;
            objUJVModel.OrgID = GlobalSession.OrgID;
            objUJVModel.BrID = GlobalSession.BrID;
            objUJVModel.YrCD = GlobalSession.YrCD;
            objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateJournalVoucher/BindAllUpdJVDDL");
            DataSet dsBindAllJV = CommonCls.ApiPostDataSet(uri, objUJVModel);
            if (dsBindAllJV.Tables.Count > 0)
            {
                DataTable dtAccountHead = VSBudgetHead = dsBindAllJV.Tables[0];
                DataTable dtLastVoucherNo = dsBindAllJV.Tables[1];
                VsdtNarration = dsBindAllJV.Tables[2];
                DataTable dtCostCenter = dsBindAllJV.Tables[3];
                if (dtAccountHead.Rows.Count > 0)
                {
                    ViewState["dtAccGstin"] = dtAccountHead;
                    DataView dvAccCode = new DataView(dtAccountHead);
                    DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

                    ddlAccountHead.DataSource = VsdtAccountHead = dtAccList;
                    ddlAccountHead.DataTextField = "AccName";
                    ddlAccountHead.DataValueField = "AccCode";
                    ddlAccountHead.DataBind();
                    ddlAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));

                }

                // Last Voucher No And Date
                if (dtLastVoucherNo.Rows.Count > 0)
                {
                    if (CommonCls.ConvertIntZero(dtLastVoucherNo.Rows[0]["LastNo"]) != 0)
                    {
                        lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtLastVoucherNo.Rows[0]["LastNo"].ToString() + " - " + CommonCls.ConvertDateDB(dtLastVoucherNo.Rows[0]["LastDate"]);
                    }
                }

                //if (VsdtNarration.Rows.Count > 0)
                //{
                //    txtNarration.DataSource = VsdtNarration;
                //    txtNarration.DataTextField = "NarrationDesc";
                //    txtNarration.DataBind();
                //}


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

    private void LoadLastVoucherNo()
    {
        try
        {
            objUJVModel = new UpdateJournalVoucherModel();
            objUJVModel.Ind = 7;
            objUJVModel.OrgID = GlobalSession.OrgID;
            objUJVModel.BrID = GlobalSession.BrID;
            objUJVModel.YrCD = GlobalSession.YrCD;
            objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateJournalVoucher/LastVoucherNo");
            DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, objUJVModel);
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

    private void LoadAccountHead()
    {
        try
        {
            objUJVModel = new UpdateJournalVoucherModel();
            objUJVModel.Ind = 35;
            objUJVModel.OrgID = GlobalSession.OrgID;
            objUJVModel.BrID = GlobalSession.BrID;
            objUJVModel.YrCD = GlobalSession.YrCD;
            objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateJournalVoucher/AccountHead");
            DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, objUJVModel);
            if (dtAccGstin.Rows.Count > 0)
            {
                ViewState["dtAccGstin"] = dtAccGstin;
                DataView dvAccCode = new DataView(dtAccGstin);
                DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

                ddlAccountHead.DataSource = VsdtAccountHead = dtAccList;
                ddlAccountHead.DataTextField = "AccName";
                ddlAccountHead.DataValueField = "AccCode";
                ddlAccountHead.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //private void LoadNarration()
    //{
    //    try
    //    {
    //        objUJVModel = new UpdateJournalVoucherModel();
    //        objUJVModel.Ind = 6;
    //        objUJVModel.OrgID = GlobalSession.OrgID;
    //        objUJVModel.BrID = GlobalSession.BrID;
    //        objUJVModel.YrCD = GlobalSession.YrCD;
    //        objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

    //        string uri = string.Format("UpdateJournalVoucher/LoadNarration");
    //        DataTable NarrationList = CommonCls.ApiPostDataTable(uri, objUJVModel);
    //        VsdtNarration = NarrationList;
    //        if (NarrationList.Rows.Count > 0)
    //        {
    //            txtNarration.DataSource = VsdtNarration;
    //            txtNarration.DataTextField = "NarrationDesc";
    //            txtNarration.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtVoucherNo.Text))
        {
            txtVoucherNo.Focus();
            ShowMessage("Enter Voucher No.", false);
            return;
        }

        objUJVModel = new UpdateJournalVoucherModel();
        objUJVModel.Ind = 1;
        objUJVModel.OrgID = GlobalSession.OrgID;
        objUJVModel.BrID = GlobalSession.BrID;
        objUJVModel.YrCD = GlobalSession.YrCD;
        objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);
        objUJVModel.DocNo = Convert.ToInt32(txtVoucherNo.Text);

        string uri = string.Format("UpdateJournalVoucher/SearchJV");
        DataTable dtSearch = CommonCls.ApiPostDataTable(uri, objUJVModel);
        if (dtSearch.Rows.Count > 0)
        {
            txtVoucherDate.Text = CommonCls.ConvertDateDB(dtSearch.Rows[0]["VoucharDate"].ToString());

            if (dtSearch.Rows[0]["CancelInd"].ToString() == "1")
            {
                ShowMessage("This Voucher No. Already Canceled.", false);
                txtVoucherNo.Focus();
                return;
            }

            grdUpdJournalVoucher.DataSource = VsdtGV = dtSearch;
            grdUpdJournalVoucher.DataBind();

            txtNarration.Text = dtSearch.Rows[0]["NarrationDesc"].ToString();
            ddlCostCenter.SelectedValue = dtSearch.Rows[0]["CCCode"].ToString();

            txtIDARefNo.Text = dtSearch.Rows[0]["IDARefNo"].ToString().Replace("Old VNo-", "");

            txtVoucherNo.Enabled = btnSearch.Enabled = false;

            decimal crAmount = 0;
            decimal drAmount = 0;

            foreach (DataRow row in dtSearch.Rows)
            {
                if (row["DrCr"].ToString() == "Dr")
                {
                    drAmount = drAmount + CommonCls.ConvertDecimalZero(row["Amount"]);
                }
                else
                {
                    crAmount = crAmount + CommonCls.ConvertDecimalZero(row["Amount"]);
                }

                //As Per Nagori Sir
                //if (Convert.ToInt32(row["AccCode"].ToString()) >= 900000)
                //{
                //    hfISGL.Value = "1";
                //}
                //else if (Convert.ToInt32(row["AccCode"].ToString()) > 700000 && Convert.ToInt32(row["AccCode"].ToString()) < 900000)
                //{
                //    hfISGL.Value = "2";
                //}
                //else
                //{
                //    hfISGL.Value = "0";
                //}
            }
            txtDrAmount.Text = Convert.ToString(drAmount);
            txtCrAmount.Text = Convert.ToString(crAmount);

            ddlAccountHead.Enabled = txtInVoiceNo.Enabled = txtInvoiceDate.Enabled = txtAmount.Enabled =
                ddlDrCr.Enabled = btnAdd.Enabled = btnCancel.Enabled = true;
        }
        else
        {
            ShowMessage("Voucher Not Found.", false);
        }
    }

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

        try // For txtAccount Head Value Shouldn't null,0 or Garbage.
        {
            if (ddlAccountHead.SelectedValue == null || Convert.ToInt32(ddlAccountHead.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
            {
                ddlAccountHead.Focus();
                ShowMessage("Account Value Not Available", false);
                return;
            }
            if (ddlAccountHead.SelectedItem.Value == ddlAccountHead.SelectedItem.Text) // For Account Head Code Not Null Or Empty
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
            if (!ValidDate) // For Invoice Date Between Financial Year.
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
        //if (grdUpdJournalVoucher.Rows.Count == 0)
        //{
        //    if (Convert.ToInt32(ddlAccountHead.SelectedItem.Value) >= 900000)
        //    {
        //        hfISGL.Value = "1";
        //    }
        //    else if (Convert.ToInt32(ddlAccountHead.SelectedValue) > 700000 && Convert.ToInt32(ddlAccountHead.SelectedValue) < 900000)
        //    {
        //        hfISGL.Value = "2";
        //    }
        //    else
        //    {
        //        hfISGL.Value = "0";
        //    }
        //    if (AddRowInGrid())
        //    {
        //        CalculateTotalInvoiceAmount();
        //        ClearAllHeadValue();
        //    }
        //}
        //else if (grdUpdJournalVoucher.Rows.Count >= 1)
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
        //        DataTable dtJV = VsdtGV;
        //        bool jvAH = false;
        //        foreach (DataRow row in dtJV.Rows)
        //        {
        //            int accCode = Convert.ToInt32(row["AccCode"].ToString());
        //            if (accCode > 942000 && accCode < 999999)
        //            {
        //                jvAH = true;
        //                break;
        //            }
        //        }
        //        if (jvAH == true)
        //        {
        //            if (AddRowInGrid())
        //            {
        //                CalculateTotalInvoiceAmount();
        //                ClearAllHeadValue();
        //            }

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
        //        if (AddRowInGrid())
        //        {
        //            CalculateTotalInvoiceAmount();
        //            ClearAllHeadValue();
        //        }
        //    }
        //}
    }
    private bool CheckBudgetHead()
    {
        objUJVModel = new UpdateJournalVoucherModel();
        objUJVModel.Ind = 12;
        objUJVModel.OrgID = GlobalSession.OrgID;
        objUJVModel.BrID = GlobalSession.BrID;
        objUJVModel.YrCD = GlobalSession.YrCD;
        objUJVModel.AccCode = CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue);
        string uri = string.Format("UpdateJournalVoucher/CheckBudgetHead");
        DataTable dtCashReceipt = CommonCls.ApiPostDataTable(uri, objUJVModel);
        if (dtCashReceipt.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtCashReceipt.Rows[0]["Cnt"].ToString()) > 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddRowInGrid()
    {
        if (VsdtGV != null)
        {
            DataTable dtAccount = VsdtGV;
            if (RowIndex >= 0)
            {
                dtAccount.Rows[RowIndex].Delete();
            }
            DataRow[] rows = dtAccount.Select("AccCode=" + ddlAccountHead.SelectedItem.Value);
            if (rows.Count() >= 1)
            {
                ddlAccountHead.ClearSelection();
                ddlAccountHead.Focus();
                ShowMessage("This Account Head Alerady Added.", false);
                return false;
            }
        }

        if (VsdtGV != null)
        {
            dtgrdview = VsdtGV;
        }
        else
        {
            CreatGridDt();
        }
        DataRow dr = dtgrdview.NewRow();
        dr["AccName"] = ddlAccountHead.SelectedItem.Text;
        dr["AccCode"] = ddlAccountHead.SelectedItem.Value;
        dr["InvoiceNo"] = CommonCls.ConvertIntZero(txtInVoiceNo.Text);
        dr["InvoiceDate"] = txtInvoiceDate.Text;
        dr["Amount"] = txtAmount.Text;
        dr["DrCr"] = ddlDrCr.SelectedItem.Text;

        if (RowIndex >= 0)
        {

            dtgrdview.Rows.InsertAt(dr, RowIndex);
            RowIndex = -1;
        }
        else
        {
            dtgrdview.Rows.Add(dr);
        }

        //dtgrdview.Rows.Add(dr);
        grdUpdJournalVoucher.DataSource = VsdtGV = dtgrdview;
        grdUpdJournalVoucher.DataBind();
        return true;
    }

    void ClearAllHeadValue()
    {
        ddlAccountHead.ClearSelection();
        txtInVoiceNo.Text = txtInvoiceDate.Text = txtAmount.Text = "";
        ddlDrCr.ClearSelection();
        ddlAccountHead.Focus();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
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
                objUJVModel = new UpdateJournalVoucherModel();
                objUJVModel.Ind = 2;
                objUJVModel.OrgID = GlobalSession.OrgID;
                objUJVModel.BrID = GlobalSession.BrID;
                objUJVModel.YrCD = GlobalSession.YrCD;
                objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);
                objUJVModel.DocNo = Convert.ToInt32(txtVoucherNo.Text);
                objUJVModel.IDARefNo = "Old VNo-" + txtIDARefNo.Text; //IDA Ref. No.
                objUJVModel.CCCode = CommonCls.ConvertIntZero(ddlCostCenter.SelectedValue);

                DataTable DtJVouchar = CreateJVData();
                objUJVModel.DtJV = JsonConvert.SerializeObject(DtJVouchar);

                string uri = string.Format("UpdateJournalVoucher/UpdateJV");
                DataTable dtSaveBankPayment = CommonCls.ApiPostDataTable(uri, objUJVModel);
                if (dtSaveBankPayment.Rows.Count > 0)
                {
                    if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "1")
                    {
                        ClearAll();
                        txtIDARefNo.Text = "";
                        ShowMessage("Data Update Successfully With Voucher No. " + dtSaveBankPayment.Rows[0]["DocMaxNo"], true);
                        txtVoucherNo.Enabled = btnSearch.Enabled = true;
                        CallReport(dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString(), Convert.ToDateTime(dtSaveBankPayment.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy"));
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

    DataTable CreatGridDt() // Create Grid Structure
    {
        dtgrdview = new DataTable();
        dtgrdview.Columns.Add("AccCode", typeof(string));
        dtgrdview.Columns.Add("AccName", typeof(string));
        dtgrdview.Columns.Add("InvoiceNo", typeof(string));
        dtgrdview.Columns.Add("InvoiceDate", typeof(string));
        dtgrdview.Columns.Add("Amount", typeof(string));
        dtgrdview.Columns.Add("DrCr", typeof(string));
        return dtgrdview;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        txtIDARefNo.Text = "";
    }

    protected void grdUpdJournalVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RowEdit")
            {
                grdUpdJournalVoucher.EditIndex = rowIndex;
                grdUpdJournalVoucher.DataSource = VsdtGV;
                grdUpdJournalVoucher.DataBind();
                grdUpdJournalVoucher.Rows[rowIndex].CssClass = "edit_tr";


            }
            else if (e.CommandName == "RemoveRow")
            {
                VsdtGV.Rows[rowIndex].Delete();
                grdUpdJournalVoucher.DataSource = VsdtGV;
                grdUpdJournalVoucher.DataBind();
            }
            else if (e.CommandName == "RowUpdate")
            {
                if (!AddRowJV(rowIndex))
                {
                    return;
                }
                grdUpdJournalVoucher.Rows[rowIndex].CssClass = "edited_tr";
            }

            CalculateTotalInvoiceAmount();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    bool AddRowJV(int rowIndex)
    {
        Label lblAccCode = (Label)grdUpdJournalVoucher.Rows[rowIndex].FindControl("lblAccCode");
        ComboBox ddlAccountHead = (ComboBox)grdUpdJournalVoucher.Rows[rowIndex].FindControl("ddlAccountHead");
        TextBox txtInVoiceNo = (TextBox)grdUpdJournalVoucher.Rows[rowIndex].FindControl("txtInvoiceNo");
        TextBox txtInvoiceDate = (TextBox)grdUpdJournalVoucher.Rows[rowIndex].FindControl("txtInvoiceDate");
        TextBox txtAmount = (TextBox)grdUpdJournalVoucher.Rows[rowIndex].FindControl("txtAmount");
        DropDownList ddlDrCr = (DropDownList)grdUpdJournalVoucher.Rows[rowIndex].FindControl("ddlDrCr");

        if (string.IsNullOrEmpty(ddlAccountHead.SelectedValue)) // For Account Head Not Null Or Empty
        {
            ddlAccountHead.Focus();
            ShowMessage("Enter Account Head.", false);
            return false;
        }

        try // For txtAccount Head Value Shouldn't null,0 or Garbage.
        {
            if (ddlAccountHead.SelectedItem.Value == null || Convert.ToInt32(ddlAccountHead.SelectedItem.Value) == 0) // For Account Head Code Not Null Or Empty
            {
                ddlAccountHead.Focus();
                ShowMessage("Account Value Not Available", false);
                return false;
            }
            if (ddlAccountHead.SelectedItem.Value == ddlAccountHead.SelectedItem.Text) // For Account Head Code Not Null Or Empty
            {
                ddlAccountHead.Focus();
                ShowMessage("Account Value Not Available", false);
                return false;
            }
        }
        catch (Exception)
        {
            ddlAccountHead.Focus();
            ShowMessage("This Account Head Value Not Available.", false);
            return false;
        }

        if (!string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Not Null
        {
            if (string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Copulsory On InvoiceNo
            {
                txtInvoiceDate.Focus();
                ShowMessage("Please Enter Invoice Date.", false);
                return false;
            }
        }

        if (!string.IsNullOrEmpty(txtInvoiceDate.Text)) // Invoice Date Not Null
        {
            if (string.IsNullOrEmpty(txtInVoiceNo.Text)) // Invoice Amount Copulsory On InvoiceDate
            {
                txtInVoiceNo.Focus();
                ShowMessage("Please Enter Invoice No.", false);
                return false;
            }
        }
        if (txtInvoiceDate.Text != "")
        {
            bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate) // For Invoice Date Between Financial Year.
            {
                txtInvoiceDate.Focus();
                ShowMessage("Invoice Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                return false;
            }
        }
        if (string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) == 0)
        {
            txtAmount.Focus();
            ShowMessage("Enter Amount", false);
            return false;
        }
        if (VsdtGV != null)
        {
            DataTable dtAccount = VsdtGV;
            DataRow selectedRow = dtAccount.Rows[rowIndex];
            if (rowIndex >= 0)
            {
                dtAccount.Rows[rowIndex].Delete();
            }
            DataRow[] rows = dtAccount.Select("AccCode=" + ddlAccountHead.SelectedItem.Value);
            if (rows.Count() >= 1)
            {
                ddlAccountHead.ClearSelection();
                ddlAccountHead.Focus();
                ShowMessage("This Account Head Alerady Added.", false);
                VsdtGV.Rows.InsertAt(selectedRow, rowIndex);
                return false;
            }
        }

        DataRow drJV = VsdtGV.NewRow();
        drJV["AccCode"] = ddlAccountHead.SelectedValue;
        drJV["AccName"] = ddlAccountHead.SelectedItem.Text;
        drJV["InvoiceNo"] = txtInVoiceNo.Text;
        drJV["InvoiceDate"] = txtInvoiceDate.Text;
        drJV["Amount"] = txtAmount.Text;
        drJV["DrCr"] = ddlDrCr.SelectedItem.Text;

        //VsdtGV.Rows[rowIndex].Delete();
        VsdtGV.Rows.InsertAt(drJV, rowIndex);

        grdUpdJournalVoucher.EditIndex = -1;
        grdUpdJournalVoucher.DataSource = VsdtGV;
        grdUpdJournalVoucher.DataBind();
        return true;
    }

    void CalculateTotalInvoiceAmount() // For Invoice Amount Dr / CR
    {
        decimal crAmount = 0;
        decimal drAmount = 0;
        for (int rowIndex = 0; VsdtGV.Rows.Count > rowIndex; rowIndex++)
        {
            string DrCr = VsdtGV.Rows[rowIndex]["DrCr"].ToString();
            if (DrCr == "Cr")
            {
                crAmount = crAmount + Convert.ToDecimal(VsdtGV.Rows[rowIndex]["Amount"]);
            }
            else
            {
                drAmount = drAmount + Convert.ToDecimal(VsdtGV.Rows[rowIndex]["Amount"]);
            }
        }
        txtCrAmount.Text = crAmount.ToString();
        txtDrAmount.Text = drAmount.ToString();
    }

    void ClearAll()
    {
        txtVoucherNo.Enabled = true;
        btnSearch.Enabled = true;
        txtVoucherNo.Text = "";
        txtNarration.Text = "";
        txtVoucherDate.Text = "";
        VsdtGV = null;
        grdUpdJournalVoucher.DataSource = new DataTable();
        grdUpdJournalVoucher.DataBind();
        txtDrAmount.Text = txtCrAmount.Text = "0";
        ddlAccountHead.ClearSelection();
        txtVoucherNo.Focus();
        btnCancel.Enabled = false;
        ddlCostCenter.ClearSelection();

        ddlAccountHead.Enabled = txtInVoiceNo.Enabled = txtInvoiceDate.Enabled = txtAmount.Enabled =
                ddlDrCr.Enabled = btnAdd.Enabled = false;
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

            dtgrdview = VsdtGV;

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow drCreateJVData = dtCJVData.NewRow();
                drCreateJVData["OrgID"] = GlobalSession.OrgID;
                drCreateJVData["BrID"] = GlobalSession.BrID;
                drCreateJVData["VchType"] = Convert.ToInt32(ViewState["VchType"]);
                drCreateJVData["YrCD"] = GlobalSession.YrCD;
                drCreateJVData["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date;
                drCreateJVData["DocNo"] = "0";   //Voucher No.

                drCreateJVData["AccCode"] = Convert.ToInt32(item["AccCode"]);  //Selected Account Head Code
                drCreateJVData["AccGst"] = "";                
                drCreateJVData["AccCode2"] = "0";

                drCreateJVData["InvoiceNo"] = 0;
                drCreateJVData["InvoiceDate"] = "";

                if (item["DrCr"].ToString() == "Dr")
                    drCreateJVData["AmountDr"] = Convert.ToDecimal(item["Amount"]);
                else
                    drCreateJVData["AmountCr"] = Convert.ToDecimal(item["Amount"]);

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

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void grdUpdJournalVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && grdUpdJournalVoucher.EditIndex == e.Row.RowIndex)
        {
            DataRow drJV = VsdtGV.Rows[e.Row.RowIndex];

            ComboBox CbAccountHead = (ComboBox)e.Row.FindControl("ddlAccountHead");
            CbAccountHead.DataSource = VsdtAccountHead;
            CbAccountHead.DataValueField = "AccCode";
            CbAccountHead.DataTextField = "AccName";
            CbAccountHead.DataBind();
            CbAccountHead.SelectedValue = drJV["AccCode"].ToString();
            CbAccountHead.Focus();
            DropDownList ddlDrCr = (DropDownList)e.Row.FindControl("ddlDrCr");

            ddlDrCr.SelectedValue = drJV["DrCr"].ToString();
        }
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

            UpdateJournalVoucherModel objUJVModel;
            if (ddlCancelReason.SelectedValue == "")
            {
                ShowMessage("Enter Cancel Reason", false);
                pnlConfirmInvoice.Visible = false;
                return;
            }

            objUJVModel = new UpdateJournalVoucherModel();
            objUJVModel.Ind = 3;
            objUJVModel.OrgID = GlobalSession.OrgID;
            objUJVModel.BrID = GlobalSession.BrID;
            objUJVModel.CancelReason = txtCancelReason.Text;
            objUJVModel.DocNo = Convert.ToInt32(txtVoucherNo.Text);

            objUJVModel.YrCD = GlobalSession.YrCD;
            objUJVModel.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("UpdateJournalVoucher/CancelVoucher");

            DataTable dtSave = CommonCls.ApiPostDataTable(uri, objUJVModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {

                    ClearAll();
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar No. - " + objUJVModel.DocNo + " is Cancel successfully ", true);
                }
                else
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel ", true);
                }
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
}