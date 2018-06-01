using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GSTReturns_frmAdvanceLiability : System.Web.UI.Page
{
    #region Declaration

    AdvanceLiabilityModel objAdvLiaModel;

    DataTable VsdtAdvanceLia
    {
        get { return (DataTable)ViewState["dtAdvanceLia"]; }
        set { ViewState["dtAdvanceLia"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedItem != null)
        {
            if (ddlMonth.SelectedValue == "0")
            {
                ShowMessage("Select Month.", false);
                ddlMonth.Focus();
                return;
            }
        }
        if (ddlYear.SelectedItem != null)
        {
            if (ddlYear.SelectedValue == "0")
            {
                ShowMessage("Select Year.", false);
                ddlYear.Focus();
                return;
            }
        }

        objAdvLiaModel = new AdvanceLiabilityModel();
        objAdvLiaModel.Ind = 1;
        objAdvLiaModel.OrgID = GlobalSession.OrgID;
        objAdvLiaModel.BrID = GlobalSession.BrID;
        objAdvLiaModel.MonthID = Convert.ToInt32(ddlMonth.SelectedValue);
        objAdvLiaModel.YearID = Convert.ToInt32(ddlYear.SelectedItem.Text);
        string uri = string.Format("AdvanceLiability/DisplayAdvance");
        DataSet dsAdvance = CommonCls.ApiPostDataSet(uri, objAdvLiaModel);
        if (dsAdvance.Tables.Count > 0)
        {
            VsdtAdvanceLia = dsAdvance.Tables[0];
            if (VsdtAdvanceLia.Rows.Count > 0)
            {
                GVAdvanceLiability.DataSource = VsdtAdvanceLia;
                GVAdvanceLiability.DataBind();
                //ShowingGroupingDataInGridView(GVAdvanceLiability.Rows, 0, 2);

                lblTotalAdvAmt.Text = CommonCls.ConverToCommas(VsdtAdvanceLia.Compute("Sum(AdvAmount)", string.Empty).ToString());
                lblTotalAdjAmt.Text = CommonCls.ConverToCommas(VsdtAdvanceLia.Compute("Sum(AdjAmount)", string.Empty).ToString());
                lblTotalBalAmt.Text = CommonCls.ConverToCommas(VsdtAdvanceLia.Compute("Sum(BalanceAmount)", string.Empty).ToString());
                ddlMonth.Enabled = ddlYear.Enabled = btnGo.Enabled = false;
                btnSave.Enabled = true;
            }
            else
            {
                ShowMessage("No Record Found.", false);
                ddlMonth.Enabled = ddlYear.Enabled = btnGo.Enabled = true;
                btnSave.Enabled = false;
            }
        }
        else
        {
            ShowMessage("Internal Server Error", false);
            ddlMonth.Enabled = ddlYear.Enabled = btnGo.Enabled = true;
            btnSave.Enabled = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        if (VsdtAdvanceLia == null || VsdtAdvanceLia.Rows.Count <= 0)
        {
            ShowMessage("Advance Liability Can Not Be Null.", false);
            return;
        }
        if (CommonCls.ConvertDecimalZero(lblTotalBalAmt.Text) <= 0)
        {
            ShowMessage("Balance Amount Can Not Be Negative Or Zero, Please Check Entry.", false);
            return;
        }

        objAdvLiaModel = new AdvanceLiabilityModel();
        objAdvLiaModel.Ind = 2;
        objAdvLiaModel.OrgID = GlobalSession.OrgID;
        objAdvLiaModel.BrID = GlobalSession.BrID;
        objAdvLiaModel.YrCD = GlobalSession.YrCD;
        objAdvLiaModel.MonthID = 10;// Convert.ToInt32(ddlMonth.SelectedValue);
        objAdvLiaModel.YearID = Convert.ToInt32(ddlYear.SelectedItem.Text);

        string uri = string.Format("AdvanceLiability/SaveAdvance");
        DataTable dtSave = CommonCls.ApiPostDataTable(uri, objAdvLiaModel);
        if (dtSave.Rows.Count > 0)
        {
            if (CommonCls.ConvertIntZero(dtSave.Rows[0]["LastNo"].ToString()) > 0)
            {
                ShowMessage("Record Save Successfully.", true);
                btnClear_Click(sender, e);
            }
            else if (CommonCls.ConvertIntZero(dtSave.Rows[0]["LastNo"].ToString()) == 0)
            {
                ShowMessage("Record Not Save, Please Try Again.", false);
            }
        }
        else
        {
            ShowMessage("Record Not Save Please Try Again.", false);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        GVAdvanceLiability.DataSource = null;
        GVAdvanceLiability.DataBind();
        ddlMonth.ClearSelection();
        ddlYear.ClearSelection();
        lblTotalAdvAmt.Text = lblTotalAdjAmt.Text = lblTotalBalAmt.Text = "";
        ddlMonth.Enabled = ddlYear.Enabled = btnGo.Enabled = true;
        btnSave.Enabled = false;
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        System.Collections.ArrayList lst = new System.Collections.ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            //gridViewRows[i].Cells[0].CssClass = "w30 c";
            //gridViewRows[i].Cells[1].CssClass = "w10 r";
            //gridViewRows[i].Cells[2].CssClass = "w30 c";
            //gridViewRows[i].Cells[3].CssClass = "w20 r";
            //gridViewRows[i].Cells[4].CssClass = "w20 c";

            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
}