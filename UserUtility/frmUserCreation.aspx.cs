using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUserCreation : System.Web.UI.Page
{
    UserModel objUCModel;

    DataTable VSDepartment
    {
        get { return (DataTable)ViewState["VSDepartment"]; }
        set { ViewState["VSDepartment"] = value; }
    }

    DataTable VSSubDepartment
    {
        get { return (DataTable)ViewState["VSSubDepartment"]; }
        set { ViewState["VSSubDepartment"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBranch();
            LoadUserCreationGRD();
            LoadUserRoll();
        }

        lblMsg.Text = lblMsg.CssClass = "";
    }

    UserRollModel objUserRollModel;

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
                ddlUserRoll.DataSource = dtBranch;
                ddlUserRoll.DataTextField = "RollDesc";
                ddlUserRoll.DataValueField = "RecordID";
                ddlUserRoll.DataBind();
                ddlUserRoll.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void LoadBranch()
    {
        try
        {
            objUCModel = new UserModel();
            objUCModel.Ind = 16;
            objUCModel.OrgID = GlobalSession.OrgID;
            string uri = string.Format("User/BindUCBranch");
            DataSet dsBranch = CommonCls.ApiPostDataSet(uri, objUCModel);
            if (dsBranch.Tables.Count > 0)
            {
                VSDepartment = dsBranch.Tables[0];
                VSSubDepartment = dsBranch.Tables[1];
                DataTable dtDesignation = dsBranch.Tables[2];

                ddlDepartment.DataSource = VSDepartment;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptCD";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("-- Select --", "0"));

                if (dtDesignation.Rows.Count > 0)
                {
                    ddlDesignation.DataSource = dtDesignation;
                    ddlDesignation.DataTextField = "DesgDesc";
                    ddlDesignation.DataValueField = "DesgID";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void LoadUserCreationGRD()
    {
        try
        {
            objUCModel = new UserModel();
            objUCModel.Ind = 1;
            objUCModel.OrgID = GlobalSession.OrgID;
            objUCModel.BrID = GlobalSession.BrID;
            string uri = string.Format("User/BindUCGrid");
            DataTable dtUser = CommonCls.ApiPostDataTable(uri, objUCModel);
            if (dtUser.Rows.Count > 0)
            {
                grdUserCreation.DataSource = dtUser;
                grdUserCreation.DataBind();
                pnlUserGrid.Visible = true;
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
            lblMsg.Text = lblMsg.CssClass = "";

            if (ddlDepartment.SelectedValue == "0")
            {
                lblMsg.Text = "Select Department.";
                ShowMessage(lblMsg.Text, false);
                ddlDepartment.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                lblMsg.Text = "Enter Full Name";
                ShowMessage(lblMsg.Text, false);
                txtFullName.Focus();
                return;
            }

            if (txtFullName.Text.Length <= 5)
            {
                lblMsg.Text = "Enter Atleast 5 Digit User Name";
                ShowMessage(lblMsg.Text, false);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMobileNo.Text))
            {
                lblMsg.Text = "Enter 10 Digit Mobile No.";
                ShowMessage(lblMsg.Text, false);
                txtMobileNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEmailAddress.Text))
            {
                lblMsg.Text = "Enter Email Address.";
                ShowMessage(lblMsg.Text, false);
                txtEmailAddress.Focus();
                return;
            }

            if (CommonCls.ConvertIntZero(ddlDesignation.SelectedValue) == 0)
            {
                lblMsg.Text = "Select Designation.";
                ShowMessage(lblMsg.Text, false);
                ddlDesignation.Focus();
                return;
            }

            DateTime ExpiryDate = new DateTime();
            if (ddlDateOfExpiry.Items.Count > 0)
            {
                if (ddlDateOfExpiry.SelectedItem.Value == "0")
                {
                    lblMsg.Text = "Select Expiry Date.";
                    ShowMessage(lblMsg.Text, false);
                    ddlDateOfExpiry.Focus();
                    return;
                }
                else
                {
                    if (ddlDateOfExpiry.SelectedItem.Text == "No Expiry")
                    {
                        ExpiryDate = new DateTime(2099, 01, 01);
                    }
                    else
                    {
                        int Increaseday = Convert.ToInt16(ddlDateOfExpiry.SelectedItem.Value.ToString());
                        DateTime dateNow = DateTime.Now;
                        ExpiryDate = dateNow.AddDays(Increaseday);
                    }
                }
            }
            objUCModel = new UserModel();
            objUCModel.Ind = 2;
            objUCModel.AcessName = txtFullName.Text.Trim();
            //objUCModel.BrID = Convert.ToInt32(ddlBranch.SelectedItem.Value);
            objUCModel.BrID = GlobalSession.BrID;
            objUCModel.OrgID = GlobalSession.OrgID;
            objUCModel.ContactNo = txtMobileNo.Text;
            objUCModel.ContactAddr = txtEmailAddress.Text;
            objUCModel.LoginID = txtEmailAddress.Text;
            //objUCModel.AdminLevel = "0";
            objUCModel.Remark = "";
            objUCModel.LoginPass = CommonCls.EncodePassword("a1@"); //Pending
            objUCModel.ExpiryDate = ExpiryDate;
            objUCModel.DepartmentID = CommonCls.ConvertIntZero(ddlDepartment.SelectedValue);
            objUCModel.SubDeptID = CommonCls.ConvertIntZero(ddlSubDepartment.SelectedValue);
            if (ddlSubDepartment.SelectedValue == "0")
            {
                objUCModel.AdminLevel = "1";
            }
            else
            {
                objUCModel.AdminLevel = "0";
            }

            objUCModel.Designation = ddlDesignation.SelectedItem.Text;
            objUCModel.DesignationID = CommonCls.ConvertIntZero(ddlDesignation.SelectedValue);

            DataTable dtSaveUser = CommonCls.ApiPostDataTable("User/SaveUser", objUCModel);
            if (dtSaveUser.Rows.Count > 0)
            {
                if (dtSaveUser.Rows[0]["RecordID"].ToString() == "1")
                {
                    //lblMsg.Text = "User Create Successfully With Login Id :" + txtEmailAddress.Text.ToString() + " & Password : " + txtFullName.Text.ToString().Substring(0, 4) + "@123";
                    //pnlConfirmInvoice.Visible = true;
                    lblPopUpMsg.Text = "User Create Successfully With Login Id :" + txtEmailAddress.Text.ToString() + " & Password : " + "a1@";

                    //ShowMessage(lblMsg.Text, true);
                    ClearAll();
                }
                else if (dtSaveUser.Rows[0]["RecordID"].ToString() == "2")
                    ShowMessage("Another User Already Registered With Given EMail Address", false);
                else if (dtSaveUser.Rows[0]["RecordID"].ToString() == "0")
                    ShowMessage("Record Not Save. Please Try Again", false);

            }
            LoadUserCreationGRD();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ClearAll()
    {
        lblMsg.Text = lblMsg.CssClass = "";
        ddlBranch.ClearSelection();
        txtFullName.Text = txtMobileNo.Text = txtEmailAddress.Text = "";
        ddlDateOfExpiry.ClearSelection();
        ddlDepartment.ClearSelection();
        ddlSubDepartment.ClearSelection();
        ddlDesignation.ClearSelection();
    }

    protected void grdUserCreation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (e.Row.FindControl("lblStatus") as Label);
            Button btnStatus = (e.Row.FindControl("btnAction") as Button);
            if (lblStatus.Text == "True")
            {
                btnStatus.Text = "InActive";
                btnStatus.CssClass = "btn btn-danger btn-xs btn-min-cu";
            }
            else
            {
                btnStatus.Text = "Active";
                btnStatus.CssClass = "btn btn-success btn-xs btn-min-cu";
            }
        }
    }

    protected void grdUserCreation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Action")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //GridViewRow gvRow = grdUserCreation.Rows[rowIndex];
            //int UserID = Convert.ToInt32(e.CommandArgument);
            Label lblOrgID = grdUserCreation.Rows[rowIndex].FindControl("lblCompanyID") as Label;
            Label lblBrID = grdUserCreation.Rows[rowIndex].FindControl("lblBranchID") as Label;
            Label lblUserID = grdUserCreation.Rows[rowIndex].FindControl("lblUserID") as Label;
            Label lblStatus = grdUserCreation.Rows[rowIndex].FindControl("lblStatus") as Label;
            objUCModel = new UserModel();
            objUCModel.Ind = 4;
            objUCModel.OrgID = Convert.ToInt32(lblOrgID.Text);
            objUCModel.BrID = Convert.ToInt32(lblBrID.Text);
            objUCModel.UserID = Convert.ToInt32(lblUserID.Text);
            if (lblStatus.Text == "True")
            {
                objUCModel.ActiveInd = 0;
            }
            else
            {
                objUCModel.ActiveInd = 1;
            }
            objUCModel.Remark = "";
            DataTable dtSaveUser = CommonCls.ApiPostDataTable("User/ChangeUserStatus", objUCModel);
            if (dtSaveUser.Rows.Count > 0)
            {
                lblMsg.Text = "";
            }
            LoadUserCreationGRD();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        lblPopUpMsg.Text = string.Empty;
        pnlConfirmInvoice.Visible = false;
        Response.Redirect("~/UserUtility/frmUserRights.aspx", false);
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        lblPopUpMsg.Text = string.Empty;
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataView dvSubDepartment = new DataView(VSSubDepartment);
            int a = CommonCls.ConvertIntZero(ddlDepartment.SelectedValue.Substring(0, 3).ToString() + 1);

            DataRow[] deptParentID = dvSubDepartment.ToTable().Select("DeptCD='" + a + "'");
            if (deptParentID.Count() > 0)
            {
                dvSubDepartment.RowFilter = "DeptParentID =" + deptParentID[0]["DeptParentID"] + "";

                ddlSubDepartment.DataSource = dvSubDepartment.ToTable();
                ddlSubDepartment.DataTextField = "DeptName";
                ddlSubDepartment.DataValueField = "DeptCD";
                ddlSubDepartment.DataBind();
                ddlSubDepartment.Items.Insert(0, new ListItem("-- Head Of Department --", "0"));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}