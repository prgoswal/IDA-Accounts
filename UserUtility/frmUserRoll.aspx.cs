using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmUserRoll : System.Web.UI.Page
{
    UserRollModel objUserRollModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadUserRoll();

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadUserRoll()
    {
        try
        {
            objUserRollModel = new UserRollModel();
            objUserRollModel.Ind = 2;
            objUserRollModel.OrgID = GlobalSession.OrgID;
            objUserRollModel.BrID = GlobalSession.BrID;

            string uri = string.Format("UserRoll/BindUserRoll");
            DataTable dtBranch = CommonCls.ApiPostDataTable(uri, objUserRollModel);
            if (dtBranch.Rows.Count > 0)
            {
                GridMatrix.DataSource = dtBranch;
                GridMatrix.DataBind();
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
    protected void GridMatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void GridMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtuser.Text))
            {
                ShowMessage("Enter User Profile .", false);
                txtuser.Focus();
                return;
            }

            objUserRollModel = new UserRollModel();
            objUserRollModel.Ind = 1;
            objUserRollModel.BrID = GlobalSession.BrID;
            objUserRollModel.OrgID = GlobalSession.OrgID;
            objUserRollModel.RollDesc = txtuser.Text;
            objUserRollModel.IsActive = 1;

            DataTable dtSaveUser = CommonCls.ApiPostDataTable("UserRoll/SaveUserRoll", objUserRollModel);
            if (dtSaveUser.Rows.Count > 0)
            { }


        }
        catch (Exception)
        {

            throw;
        }
    }
}