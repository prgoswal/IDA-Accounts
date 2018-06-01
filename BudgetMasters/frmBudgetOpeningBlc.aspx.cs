using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BudgetMasters_frmBudgetOpeningBlc : System.Web.UI.Page
{
    OpeningBalanceModel objOpeningBalance;
    string uri;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCurrentBudgetAmt.Focus();
            int yrcd = GlobalSession.BudgetYrCD;//For Current  Budget Year.
            int yrcdpluse = yrcd + 1;
            int yrcdremove = yrcd - 1;
            int val = yrcdremove - 1;
            lblOBalBg1718.Text = "20" + yrcd + "- 20" + yrcdpluse + "(Budgeted)";
            lblOBalBg1617.Text = "20" + yrcdremove + "- 20" + yrcd + "(Budgeted)";
            lblOBalAc1617.Text = "20" + yrcdremove + "- 20" + yrcd + "(Actual)";
            lblOBalAc1516.Text = "20" + val + "- 20" + yrcdremove + "(Actual)";
            if (hfOpeningBalID.Value == "0")
            {
                checkExistingEntry();
            }
            lblAmtType.Text = GlobalSession.BudgetAmount;
            lblYear.Text = " 20" + yrcd + "- 20" + yrcdpluse;

        }
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    private void ClearAll()
    {
        txtCurrentBudgetAmt.Text = txtPreviousYearActualAmt.Text = txtPreviousYearBudgetAmt.Text = txtPreviousYear2ActualAmt.Text = "";
        hfOpeningBalID.Value = "0";
    }

    private DataTable checkExistingEntry()
    {
        objOpeningBalance = new OpeningBalanceModel();
        objOpeningBalance.Ind = 2;//check yrcode acording companyId,BranchID and YearCode
        objOpeningBalance.yrcd = GlobalSession.BudgetYrCD;
        objOpeningBalance.OrgID = GlobalSession.OrgID;
        objOpeningBalance.BrID = GlobalSession.BrID;

        uri = string.Format("OpeningBalance/CheckyrCode");
        DataTable dtOpeningBalance = CommonCls.ApiPostDataTable(uri, objOpeningBalance);
        if (dtOpeningBalance.Rows.Count > 0)
        {
            txtCurrentBudgetAmt.Text = dtOpeningBalance.Rows[0]["CurrentBudgetAmt"].ToString();
            txtPreviousYearBudgetAmt.Text = dtOpeningBalance.Rows[0]["PreviousYearBudgetAmt"].ToString();
            txtPreviousYearActualAmt.Text = dtOpeningBalance.Rows[0]["PreviousYearActualAmt"].ToString();
            txtPreviousYear2ActualAmt.Text = dtOpeningBalance.Rows[0]["PreviousYear2ActualAmt"].ToString();
            // ShowMessage("Record Save successfully.", true);
            hfOpeningBalID.Value = "0";
        }
        else
        {
            //ShowMessage("Record not Save successfully.", false);
        }
        return dtOpeningBalance;
    }

    // protected void btnclear_Click(object sender, EventArgs e)
    // {
    //     txtCurrentBudgetAmt.Focus();
    //     ClearAll();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtCurrentBudgetAmt.Text) || Convert.ToDecimal(txtCurrentBudgetAmt.Text) <= 0)
            {
                ShowMessage("Enter " + lblOBalBg1718.Text + " .", false);
                txtCurrentBudgetAmt.Focus();
                return;
            }

            //if (System.Text.RegularExpressions.Regex.IsMatch(txtCurrentBudgetAmt.Text, "[^0-9]"))
            //{
            //    ShowMessage("Please enter only numbers.", false);
            //    txtCurrentBudgetAmt.Text = txtCurrentBudgetAmt.Text.Remove(txtCurrentBudgetAmt.Text.Length - 1);
            //    txtCurrentBudgetAmt.Focus();
            //    return;
            //}
            //if (System.Text.RegularExpressions.Regex.IsMatch(txtPreviousYearBudgetAmt.Text, "[^0-9]"))
            //{
            //    ShowMessage("Please enter only numbers.", false);
            //    txtPreviousYearBudgetAmt.Text = txtPreviousYearBudgetAmt.Text.Remove(txtPreviousYearBudgetAmt.Text.Length - 1);
            //    txtPreviousYearBudgetAmt.Focus();
            //    return;
            //}
            //if (System.Text.RegularExpressions.Regex.IsMatch(txtPreviousYearActualAmt.Text, "[^0-9]"))
            //{
            //    ShowMessage("Please enter only numbers.", false);
            //    txtPreviousYearActualAmt.Text = txtPreviousYearActualAmt.Text.Remove(txtPreviousYearActualAmt.Text.Length - 1);
            //    txtPreviousYearActualAmt.Focus();
            //    return;
            //}
            //if (System.Text.RegularExpressions.Regex.IsMatch(txtPreviousYear2ActualAmt.Text, "[^0-9]"))
            //{
            //    ShowMessage("Please enter only numbers.", false);
            //    txtPreviousYear2ActualAmt.Text = txtPreviousYear2ActualAmt.Text.Remove(txtPreviousYear2ActualAmt.Text.Length - 1);
            //    txtPreviousYear2ActualAmt.Focus();
            //    return;
            //}

            if (string.IsNullOrEmpty(txtPreviousYearBudgetAmt.Text))
            {
                txtPreviousYearBudgetAmt.Text = "0";
                //ShowMessage(lblOBalBg1617.Text, false);
                //txtPreviousYearBudgetAmt.Focus();
                //return;
            }

            if (string.IsNullOrEmpty(txtPreviousYearActualAmt.Text))
            {
                txtPreviousYearActualAmt.Text = "0";
                //ShowMessage(lblOBalAc1617.Text, false);
                //txtPreviousYearActualAmt.Focus();
                //return;
            }

            if (string.IsNullOrEmpty(txtPreviousYear2ActualAmt.Text))
            {
                txtPreviousYear2ActualAmt.Text = "0";
                //ShowMessage(lblOBalAc1516.Text, false);
                //txtPreviousYear2ActualAmt.Focus();
                //return;
            }

            objOpeningBalance = new OpeningBalanceModel();
            objOpeningBalance.OrgID = GlobalSession.OrgID;
            objOpeningBalance.BrID = GlobalSession.BrID;
            objOpeningBalance.yrcd = GlobalSession.BudgetYrCD;
            objOpeningBalance.CurrentBudgetAmt = Convert.ToDecimal(txtCurrentBudgetAmt.Text);
            objOpeningBalance.PreviousYearBudgetAmt = Convert.ToDecimal(txtPreviousYearBudgetAmt.Text);
            objOpeningBalance.PreviousYearActualAmt = Convert.ToDecimal(txtPreviousYearActualAmt.Text);
            objOpeningBalance.PreviousYear2ActualAmt = Convert.ToDecimal(txtPreviousYear2ActualAmt.Text);


            if (hfOpeningBalID.Value == "0")
            {
                objOpeningBalance.Ind = 1;//For Saving Data
                uri = string.Format("OpeningBalance/SaveOpeningBalance");
            }
            //else
            //{
            //    objOpeningBalance.Ind = 2;//For Update Data
            //    //objOpeningBalance.SectionID = CommonCls.ConvertIntZero(hfSectionID.Value);
            //    uri = string.Format("OpeningBalance/UpdateBudgetSection");
            //}

            DataTable dtOpeningBalance = CommonCls.ApiPostDataTable(uri, objOpeningBalance);
            if (dtOpeningBalance.Rows.Count > 0)
            {
                ShowMessage("Record Save successfully.", true);
                // ClearAll();
                hfOpeningBalID.Value = "0";
                //BindAll();
            }
            else
            {
                ShowMessage("Record not Save successfully.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
}