using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmSection : System.Web.UI.Page
{
    SectionSubSectionModel objCostCentre;

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
            objCostCentre = new SectionSubSectionModel();
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

    private void LoadMainCost()
    {
        try
        {
            objCostCentre = new SectionSubSectionModel();
            objCostCentre.Ind = 1;
            objCostCentre.OrgID = GlobalSession.OrgID;
            objCostCentre.BrID = GlobalSession.BrID;
            string uri = string.Format("CostCentre/LoadMainCost");
            DataTable GroupFillGrid = CommonCls.ApiPostDataTable(uri, objCostCentre);
            if (GroupFillGrid.Rows.Count > 0)
            {
                ddlMainGroup.DataSource = GroupFillGrid;
                ddlMainGroup.DataTextField = "CostCentreName";
                ddlMainGroup.DataValueField = "CostCentreID";
                ddlMainGroup.DataBind();
                //if (GroupFillGrid.Rows.Count > 1)
                //{
                ddlMainGroup.Items.Insert(0, new ListItem("-- Select --", "0"));
                //}
                //ddlMainGroup.SelectedIndex = 0;
                divddlMainGrp.Visible = true;
            }
            else
            {
                ShowMessage("There is no Sub Cost Centre.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";
        if (CommonCls.ConvertIntZero(ddlGroupType.SelectedValue) == 2)
        {

            LoadMainCost();
        }
        else
        {
            divddlMainGrp.Visible = false;
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
            if (divddlMainGrp.Visible == true)
            {
                if (CommonCls.ConvertIntZero(ddlMainGroup.SelectedValue) == 0)
                {
                    ShowMessage("Select Sub Cost Centre.", false);
                    ddlMainGroup.Focus();
                    return;
                }
            }

            objCostCentre = new SectionSubSectionModel();
            objCostCentre.OrgID = GlobalSession.OrgID;
            objCostCentre.BrID = GlobalSession.BrID;
            objCostCentre.User = GlobalSession.UserID;
            objCostCentre.IP = GlobalSession.IP;
            objCostCentre.CostCentreName = txtName.Text;
            if (CommonCls.ConvertIntZero(ddlGroupType.SelectedValue) == 1)
            {
                objCostCentre.ParentCostCentreID = 0;
            }
            else
            {
                objCostCentre.ParentCostCentreID = CommonCls.ConvertIntZero(ddlMainGroup.SelectedValue);
            }
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
        ddlMainGroup.ClearSelection();
        ddlGroupType.ClearSelection();
        divddlMainGrp.Visible = false;
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
            objCostCentre = new SectionSubSectionModel();
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
            if (CommonCls.ConvertIntZero(((Label)grdCostCentreCreation.Rows[index].FindControl("lblParentCostCentreID")).Text) == 0)
            {
                divddlMainGrp.Visible = false;
                ddlGroupType.SelectedValue = "1";
            }
            else
            {
                ddlGroupType.SelectedValue = "2";
                ddlGroupType_SelectedIndexChanged(sender, e);
                divddlMainGrp.Visible = true;
                ddlMainGroup.SelectedValue = ((Label)grdCostCentreCreation.Rows[index].FindControl("lblParentCostCentreID")).Text;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
}