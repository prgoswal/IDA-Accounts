using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmBrsOpeningBalDiff : System.Web.UI.Page
{
    OpenBrsDiffModel objOpenBrsDiff;

    protected void Page_Load(object sender, EventArgs e)
    { 

        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            pnlPassword.Visible = true;
            pnlPassword.Focus(); 

            LoadBankAccount();
        }
    }

    void LoadBankAccount()
    {
        try
        {
            objOpenBrsDiff = new OpenBrsDiffModel();

            objOpenBrsDiff.Ind = 1;
            objOpenBrsDiff.OrgID = GlobalSession.OrgID;
            objOpenBrsDiff.BrID = GlobalSession.BrID;
            objOpenBrsDiff.YrCD = GlobalSession.YrCD;
            //objOpenBrsDiff.VchType = 3;//Convert.ToInt32(Session["VchType"]) 
            string uri = string.Format("OpenBrsDiffEntry/LoadBankAccount");
            DataTable dtBankAccount = CommonCls.ApiPostDataTable(uri, objOpenBrsDiff);
            if (dtBankAccount.Rows.Count > 0)
            {
                ddlBankAccount.DataSource = dtBankAccount;
                ddlBankAccount.DataTextField = "AccName";
                ddlBankAccount.DataValueField = "AccCode";
                ddlBankAccount.DataBind();
                if (dtBankAccount.Rows.Count > 1)
                   ddlBankAccount.Items.Insert(0, new ListItem("-- Select Bank --", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidOnSerachInvoice() //Validation For Save Button Button.....
    {

        if (CommonCls.ConvertIntZero(ddlBankAccount.SelectedValue) == 0)
        {
            ddlBankAccount.Focus();
            ShowMessage("Please Select Bank Name.", false);
            return false;
        }
        if (CommonCls.ConvertIntZero(ddlCriteria.SelectedValue) == 0)
        {
            ddlCriteria.Focus();
            ShowMessage("Please Select Criteria.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtChqNo.Text))
        {
            txtChqNo.Focus();
            ShowMessage("Please Enter Cheque No.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtChqDate.Text))
        {
            txtChqDate.Focus();
            ShowMessage("Please Enter Cheaque Date.", false);
            return false;
        }

        if (CommonCls.ConvertDecimalZero(txtDrAmount.Text) > 0 && CommonCls.ConvertDecimalZero(txtCrAmount.Text) > 0)
        {
            ShowMessage("Enter Either Dr Amount Or Cr Amount.", false);
            return false;
        }

        if (CommonCls.ConvertDecimalZero(txtDrAmount.Text) == 0)
        {
            if (CommonCls.ConvertDecimalZero(txtCrAmount.Text) == 0)
            {
                ShowMessage("Enter Dr. Amount.", false);
                txtDrAmount.Focus();
                return false;
            }
        }
        if (CommonCls.ConvertDecimalZero(txtCrAmount.Text) == 0)
        {
            if (CommonCls.ConvertDecimalZero(txtDrAmount.Text) == 0)
            {
                ShowMessage("Enter Cr. Amount.", false);
                txtCrAmount.Focus();
                return false;
            }
        }
        // For Voucher Date Between Financial Year.
        //bool ChequeDate = CommonCls.CheckFinancialYrDate(txtChqDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
        //if (!ChequeDate) 
        //{
        //    ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
        //    return false;
        //}

        if (ddlCriteria.SelectedValue == "1" || ddlCriteria.SelectedValue == "2")
        {
            if (string.IsNullOrEmpty(txtVoucherno.Text))
            {
                txtVoucherno.Focus();
                ShowMessage("Please Enter Voucher No.", false);
                return false;
            }
            if (string.IsNullOrEmpty(txtVoucherDate.Text)) // For Voucher Date Not Be Null
            {
                txtVoucherDate.Focus();
                ShowMessage("Enter Voucher Date!", false);
                return false;
            }

            bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate) // For Voucher Date Between Financial Year.
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Should Be Within Financial Year Date!", false);
                return false;
            }
        }
       
        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e) //Save Button...
    {
        if (!ValidOnSerachInvoice())
        {
            return;
        }
        try
        {
            objOpenBrsDiff = new OpenBrsDiffModel();

            objOpenBrsDiff.Ind = 2;
            objOpenBrsDiff.OrgID = GlobalSession.OrgID;
            objOpenBrsDiff.BrID = GlobalSession.BrID;
            objOpenBrsDiff.YrCD = GlobalSession.YrCD;
            objOpenBrsDiff.Acccode = ddlBankAccount.SelectedValue;
            objOpenBrsDiff.BSCriteria = ddlCriteria.SelectedItem.Text;
            objOpenBrsDiff.Narration = txtnarr.Text;
            objOpenBrsDiff.ChequeNo = CommonCls.ConvertIntZero(txtChqNo.Text);
            objOpenBrsDiff.ChequeDate = CommonCls.ConvertToDate(txtChqDate.Text);
            objOpenBrsDiff.CrAmount = CommonCls.ConvertDecimalZero(txtCrAmount.Text);
            objOpenBrsDiff.DrAmount = CommonCls.ConvertDecimalZero(txtDrAmount.Text);
            objOpenBrsDiff.VoucharDateFrom = "";
            objOpenBrsDiff.VoucharDateto = "";
            objOpenBrsDiff.DocNo = CommonCls.ConvertIntZero(txtVoucherno.Text);
            objOpenBrsDiff.DocDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            objOpenBrsDiff.UTRNo = "0";
            objOpenBrsDiff.BSAmount = 0;
            objOpenBrsDiff.BSDate = "";
            objOpenBrsDiff.OpeningDate = "";
            objOpenBrsDiff.OpeningBalance = 0;
            objOpenBrsDiff.ClosingDate = "";
            objOpenBrsDiff.ClosingBalance = 0;
            objOpenBrsDiff.BSInd = 3;
            string uri = string.Format("OpenBrsDiffEntry/SaveOpenBrs");
            DataTable dtOpenBrsSave = CommonCls.ApiPostDataTable(uri, objOpenBrsDiff);
            if (dtOpenBrsSave.Rows.Count > 0)
            {
                ShowMessage("Data Successfully Submited.", true);

                clearAll();
            }
            else
            {
                ShowMessage("Data Not Submit.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearAll();
    }
    void clearAll()
    {
        ddlBankAccount.ClearSelection();
        ddlCriteria.ClearSelection();
        txtChqNo.Text = "";
        txtChqDate.Text = "";
        txtCrAmount.Text = "";
        txtDrAmount.Text = "";
        txtnarr.Text = "";
        txtVoucherno.Text = "";
        txtVoucherDate.Text = "";
        ddlBankAccount.Focus();
        DivVoucher.Style.Add("display", "none");

    }

    
    protected void ddlCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCriteria.SelectedValue=="1" || ddlCriteria.SelectedValue=="2")
        { 
             DivVoucher.Style.Add("display", "block");
        } 
        else
        {
            DivVoucher.Style.Add("display", "none");
        }
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        
    }
    #region Verification Password

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblPassMsg.Text = lblPassMsg.CssClass = "";

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                ShowPassMessage("Enter Password.", false);
                pnlPassword.Visible = true;
                pnlPassword.Focus();
                return;
            }

            string companyName = GlobalSession.OrgName;
            string OrgName = new String(companyName.Where(Char.IsLetter).ToArray());
            string firstFour = OrgName.Substring(0, 4).ToUpper();
            string orgPassword = firstFour + "@257";
            string pass = txtPassword.Text.ToUpper().Trim();
            if (pass == orgPassword)
            {
                txtPassword.Text = "";
                pnlPassword.Visible = false;
                ddlBankAccount.Focus(); 
              
            }
            else
            {
                pnlPassword.Visible = true;
                pnlPassword.Focus();
                ShowPassMessage("Wrong Password, Please Try Again.", false);
                txtPassword.Text = "";
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ShowPassMessage(string Message, bool type)
    {
        lblPassMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblPassMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    #endregion
}