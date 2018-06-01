using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMasters_frmBudgetYrActivation : System.Web.UI.Page
{
 BudgetYearActivationModel objBudgetYearActivation;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadFinancialYear();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void LoadFinancialYear()
    {
        try
        {
            objBudgetYearActivation = new BudgetYearActivationModel();
            objBudgetYearActivation.Ind = 11;

            string uri = string.Format("BudgetYearActivation/LoadFinancialYear");
            DataTable dtFinancialYear = CommonCls.ApiPostDataTable(uri, objBudgetYearActivation);
            if (dtFinancialYear.Rows.Count > 0)
            {
                txtOrderNo.Text = dtFinancialYear.Rows[0]["BudgetOrderNumber"].ToString();
                txtOrderDate.Text = CommonCls.ConvertDateDB(dtFinancialYear.Rows[0]["BudgetOrderDate"].ToString());
                ddlFinancialYr.SelectedValue = dtFinancialYear.Rows[0]["YearFromTo"].ToString();
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
            objBudgetYearActivation = new BudgetYearActivationModel();
            objBudgetYearActivation.Ind = 1;

            objBudgetYearActivation.YrCode = 18;
            objBudgetYearActivation.YearFromTo = ddlFinancialYr.SelectedValue;
            objBudgetYearActivation.ActiveID = 1;
            objBudgetYearActivation.UserID = GlobalSession.UserID;
            objBudgetYearActivation.IPAddress = GlobalSession.IP;
            objBudgetYearActivation.AccountInd = 0;
            objBudgetYearActivation.BudgetInd = 1;

            objBudgetYearActivation.YrStartDate = CommonCls.ConvertToDate("01/04/2018");
            objBudgetYearActivation.YrEndDate = CommonCls.ConvertToDate("31/03/2019");

            objBudgetYearActivation.BudgetOrderNumber = txtOrderNo.Text;
            objBudgetYearActivation.BudgetOrderDate = CommonCls.ConvertToDate(txtOrderDate.Text);
            objBudgetYearActivation.AccountingOrderDate = "";
            objBudgetYearActivation.AccountingOrderNumber = "";
            objBudgetYearActivation.AccountingEntryDate = "";
            string uri = string.Format("BudgetYearActivation/SaveFinancialYear");
            DataTable dtYear = CommonCls.ApiPostDataTable(uri, objBudgetYearActivation);
            if (dtYear.Rows.Count > 0)
            {
                if (dtYear.Rows[0][0].ToString() == "1")
                {
                    ShowMessage("Financial Year is Activated For Year" + ddlFinancialYr.SelectedValue, true);
                }
            }
            else
            {
                ShowMessage("Data is not saved successfully", false);
            }



        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
}