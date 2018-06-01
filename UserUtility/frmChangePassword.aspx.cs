using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmChangePassword : System.Web.UI.Page
{
    UserModel objUCModel;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtcurrentPass.Text = txtchangePass.Text = txtconfirmPass.Text = "";
        lblmsg.Text = "";
    }
    bool Flag = false;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }
        if (string.IsNullOrEmpty(txtcurrentPass.Text))
        {
            lblmsg.Text = "Insert Current Password.";
            return;
        }

        Flag = CheckCurrentPass();
        if (!Flag)
        {
            lblmsg.Text = "Current Password Not Matched.";
            return;
        }

        if (string.IsNullOrEmpty(txtchangePass.Text))
        {
            lblmsg.Text = "Insert Change Password.";
            return;
        }

        if (string.IsNullOrEmpty(txtconfirmPass.Text))
        {
            lblmsg.Text = "Insert Confirm Password.";
            return;
        }

        if (txtchangePass.Text != txtconfirmPass.Text)
        {
            lblmsg.Text = "New Password And Confirm Password not match.";
            return;
        }

        objUCModel = new UserModel();
        objUCModel.Ind = 5;
        objUCModel.BrID = GlobalSession.BrID; // Convert.ToInt32(Session["BrID"]);
        objUCModel.OrgID = GlobalSession.OrgID; // Convert.ToInt32(Session["OrgID"]);
        objUCModel.UserID = GlobalSession.UserID; // Convert.ToInt32(Session["UserID"]);
        objUCModel.ChangePass = CommonCls.EncodePassword(txtchangePass.Text.Trim());
        string uri = string.Format("User/ChangePassword");
        DataTable dtChangePassword = CommonCls.ApiPostDataTable(uri, objUCModel);
        if (dtChangePassword.Rows.Count > 0)
        {
            lblmsg.Text = "Password Change SucessFully";
            Response.Redirect("~/Logout.aspx");
        }
        else
        {
            lblmsg.Text = "Password Not Change.";
        }

    }

    bool CheckCurrentPass()
    {
        objUCModel = new UserModel();
        objUCModel.Ind = 3;
        objUCModel.LoginID = GlobalSession.UserName; //Session["UserName"].ToString();
        objUCModel.LoginPass = CommonCls.EncodePassword(txtcurrentPass.Text);
        string uri = string.Format("User/MatchLoginDetails");
        DataTable dtmatchlogin = CommonCls.ApiPostDataTable(uri, objUCModel);
        if (dtmatchlogin.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}