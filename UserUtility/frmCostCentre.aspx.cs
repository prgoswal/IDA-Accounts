using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmCostCentre : System.Web.UI.Page
{
    CostCentreModel objCostCentre;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCostCentreCreation();

        }
    }

    private void LoadCostCentreCreation()
    {
        try
        {
            objCostCentre = new CostCentreModel();
            objCostCentre.Ind = 3;
            objCostCentre.OrgID = GlobalSession.OrgID;
            objCostCentre.BrID = GlobalSession.BrID;
            string uri = string.Format("CostCentre/LoadCostCentreGrid");
            DataTable dtCostCentre = CommonCls.ApiPostDataTable(uri, objCostCentre);
            if (dtCostCentre.Rows.Count > 0)
            {
                grdCostCentreCreation.DataSource = dtCostCentre;
                grdCostCentreCreation.DataBind();
                // pnlCostCentreGrid.Visible = true;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text == "")
            {
                ShowMessage("Enter Cost Centre Name.", false);
                txtName.Focus();
                return;
            }
            objCostCentre = new CostCentreModel();
            objCostCentre.OrgID = GlobalSession.OrgID;
            objCostCentre.BrID = GlobalSession.BrID;
            objCostCentre.User = GlobalSession.UserID;
            objCostCentre.IP = GlobalSession.IP;
            objCostCentre.CostCentreName = txtName.Text;
            string uri;

            if (hfCostCentreID.Value == "0")
            {
                objCostCentre.Ind = 2;
                uri = string.Format("CostCentre/SaveCostCentre");
            }
            else
            {
                objCostCentre.Ind = 4;
                objCostCentre.CostCentreID = CommonCls.ConvertIntZero(hfCostCentreID.Value);
                uri = string.Format("CostCentre/SaveCostCentre");

            }
            DataTable dtCostCentre = CommonCls.ApiPostDataTable(uri, objCostCentre);
            if (dtCostCentre.Rows.Count > 0)
            {
                ShowMessage("Data Is Submitted SucessFully", true);
                ClearText();
                LoadCostCentreCreation();
            }
            else
            {
                ShowMessage("Data Is Not Submit..!", false);
            }
            hfCostCentreID.Value = "0";
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void ClearText()
    {
        txtName.Text = "";
        hfCostCentreID.Value = "0";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearText();
        lblMsg.Text = lblMsg.CssClass = "";
    }

    protected void grdCostCentreCreation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Action")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //GridViewRow gvRow = grdUserCreation.Rows[rowIndex];
            //int CostCentreID = Convert.ToInt32(e.CommandArgument);

            hfCostCentreID.Value = ((Label)grdCostCentreCreation.Rows[rowIndex].FindControl("lblCostCentreID")).Text;
            objCostCentre = new CostCentreModel();
            objCostCentre.Ind = 0;
            objCostCentre.OrgID = GlobalSession.OrgID;
            objCostCentre.BrID = GlobalSession.BrID;
            objCostCentre.User = GlobalSession.UserID;
            objCostCentre.CostCentreID = CommonCls.ConvertIntZero(hfCostCentreID.Value);

            DataTable dtCostCentre = CommonCls.ApiPostDataTable("CostCentre/DeleteCostCentre", objCostCentre);
            if (dtCostCentre.Rows.Count > 0)
            {
                lblMsg.Text = "";
            }
            LoadCostCentreCreation();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int index = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            hfCostCentreID.Value = ((Label)grdCostCentreCreation.Rows[index].FindControl("lblCostCentreID")).Text;
            txtName.Text = ((Label)grdCostCentreCreation.Rows[index].FindControl("lblName")).Text;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    
}