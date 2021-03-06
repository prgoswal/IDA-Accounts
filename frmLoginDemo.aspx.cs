﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmLogin : System.Web.UI.Page
{
    UserModel objCUModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYrCode();
        }
        if (GlobalSession.SendStatus == true)
        {
            GlobalSession.SendStatus = false;
            lblRegInfo.Text = "Thank You for showing interest in \"GST-Saathi\". Login details have been sent to you via sms & email!";
        }
    }

    void LoadYrCode()
    {
        try
        {
            string uri = string.Format("Login/LoadYrCode?Ind={0}", 99);
            DataTable dtfinancial = CommonCls.ApiGetDataTable(uri);
            if (dtfinancial.Rows.Count > 0)
            {
                ViewState["financialYr"] = dtfinancial;
                ddlFinancialYear.DataSource = dtfinancial;
                ddlFinancialYear.DataTextField = "YearFromTo";
                ddlFinancialYear.DataValueField = "YrCode";
                ddlFinancialYear.DataBind();
                if (dtfinancial.Rows.Count > 1)
                {
                    ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Year --", "0000"));
                }
            }
            else
            {
                //ShowMessage("Financial Year Not Load Properly. Please Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            ShowLoginError(ex.Message);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {


            lblRegInfo.Text = "";
            if (ddlFinancialYear.Items.Count > 1 && ddlFinancialYear.SelectedIndex <= 0)
            {
                ShowLoginError("Please Select Financial Year");
                return;
            }
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                ShowLoginError("Please Enter User Name");
                return;
            }

            if (!UserValidation())
            {
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "nextclick", "nextclickSuccess('" + txtUserID.Text + "','" + ddlFinancialYear.SelectedItem.Text + "')", true);
            }
        }
        catch (Exception ex)
        {
            ShowLoginError(ex.Message);
        }
    }

    ///Check All Validation On UserName.
    bool UserValidation()
    {
        objCUModel = new UserModel();
        objCUModel.Ind = 6;
        objCUModel.LoginID = txtUserID.Text.Trim();

        string uri = string.Format("User/UserLoginDetails");
        DataTable dtUserStatus = CommonCls.ApiPostDataTable(uri, objCUModel);
        if (dtUserStatus.Rows.Count > 0)
        {
            if (dtUserStatus.Rows[0]["LockInd"].ToString().ToUpper() == "TRUE") /// Check User Lock Or Not.
            {
                ShowLoginError("User Is Locked Please Contact Admin !");
                return false;
            }
            if (Convert.ToDateTime(dtUserStatus.Rows[0]["UserValidity"].ToString()) < DateTime.Now.Date) /// User Validity Check By Expiry Date.
            {
                ShowLoginError("User Login Date Is Expired.Please Contact to Admin. !");
                return false;
            }
            if (dtUserStatus.Rows[0]["IsActive"].ToString().ToUpper() != "TRUE")  /// Check User Active Or Not.
            {
                ShowLoginError("User Are Not Active!");
                return false;
            }

            if (dtUserStatus.Rows[0]["TransactionNo"].ToString().ToUpper() != "999999")  /// Check User For Demo Request
            {

                ShowLoginError("User Are Not Authenticated For Demo Version!");
                return false;
            }
           


            if (dtUserStatus.Rows[0]["UserLoginStatus"].ToString().ToUpper() == "TRUE") /// Check User Login Status For Forcefully Logout.
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "nextclick", "nextclick('" + txtUserID.Text + "','" + ddlFinancialYear.SelectedItem.Text + "')", true);
                btnLogin.Text = "Logout";
                return false;
            }
            else
            {
                btnLogin.Text = "Login";
                return true;
            }
        }
        else
        {
            ShowLoginError("Invalid User ID !");
            return false;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnLogin.Text == "Logout")
            {
                objCUModel = new UserModel();
                objCUModel.Ind = 7;
                objCUModel.LoginID = txtUserID.Text.Trim();
                objCUModel.LoginPass = CommonCls.EncodePassword(txtPassword.Text.Trim());

                string uri = string.Format("User/Logout");
                DataTable dtUserStatus = CommonCls.ApiPostDataTable(uri, objCUModel);
                if (dtUserStatus.Rows.Count > 0)
                {
                    if (dtUserStatus.Rows[0]["ReturnInd"].ToString() == "1")
                    {
                        btnLogin.Text = "Login";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "nextclick", "nextclickSuccess('" + txtUserID.Text + "','" + ddlFinancialYear.SelectedItem.Text + "')", true);
                    }
                    else
                    {
                        ShowLoginError("Password Wrong!");
                        btnLogin.Text = "Logout";
                    }
                }
                return;
            }
            else
            {

                if (ddlFinancialYear.SelectedItem.Value == "0000")
                {
                    ShowLoginError("Please Select Financial Year.");
                    return;
                }
                if (string.IsNullOrEmpty(txtUserID.Text))
                {
                    ShowLoginError("Please Select Financial Year");
                    return;
                }

                objCUModel = new UserModel();
                objCUModel.Ind = 3;
                objCUModel.LoginID = txtUserID.Text.Trim();
                objCUModel.LoginPass = CommonCls.EncodePassword(txtPassword.Text.Trim());

                string uri = string.Format("User/MatchLoginDetails");
                DataTable dtMatchLoginDetails = CommonCls.ApiPostDataTable(uri, objCUModel);
                if (dtMatchLoginDetails.Rows.Count > 0)
                {
                    if (dtMatchLoginDetails.Rows[0]["UserID"].ToString() == "0")
                    {
                        ShowLoginError("Password Not Match!");
                        return;
                    }

                    GlobalSession.BrID = Convert.ToInt32(dtMatchLoginDetails.Rows[0]["BranchID"].ToString());
                    GlobalSession.UserName = dtMatchLoginDetails.Rows[0]["UserName"].ToString();
                    GlobalSession.IsAdmin = CommonCls.ConvertIntZero(dtMatchLoginDetails.Rows[0]["IsAdmin"].ToString());
                    GlobalSession.UserID = Convert.ToInt32(dtMatchLoginDetails.Rows[0]["UserID"].ToString());
                    GlobalSession.OrgID = Convert.ToInt32(dtMatchLoginDetails.Rows[0]["CompanyID"].ToString());
                    GlobalSession.OrgName = dtMatchLoginDetails.Rows[0]["CompanyName"].ToString();
                    GlobalSession.YrCD = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
                    GlobalSession.BrName = dtMatchLoginDetails.Rows[0]["BranchName"].ToString();
                    try
                    {
                        GlobalSession.IP = "10.0.0.4";//CommonCls.GetIP();
                    }
                    catch (Exception ex)
                    {
                        ShowLoginError(ex.Message);
                    }
                    GlobalSession.UserRemark = dtMatchLoginDetails.Rows[0]["UserRemark"].ToString();
                    GlobalSession.UserLoginID = dtMatchLoginDetails.Rows[0]["UserLoginID"].ToString();
                    GlobalSession.InvoiceRptName = dtMatchLoginDetails.Rows[0]["InvoiceRptName"].ToString();

                    if (dtMatchLoginDetails.Rows[0]["ProfileSet"].ToString() != "1")
                    {
                        Response.Redirect("~/frmDemoProfileCreation.aspx");
                        return;
                    }


                    DataTable lstFinacialYr = (DataTable)ViewState["financialYr"];
                    DataRow selectedFinacialYr = lstFinacialYr.NewRow();
                    selectedFinacialYr = lstFinacialYr.Select("YrCode=" + ddlFinancialYear.SelectedItem.Value)[0];
                    GlobalSession.FinancialYr = ddlFinancialYear.SelectedItem.Text;
                    GlobalSession.YrEndDate = (Convert.ToDateTime(selectedFinacialYr["YrEndDate"])).ToString("dd/MM/yyyy");
                    GlobalSession.YrStartDate = (Convert.ToDateTime(selectedFinacialYr["YrStartDate"])).ToString("dd/MM/yyyy");


                    if (!string.IsNullOrEmpty(dtMatchLoginDetails.Rows[0]["IsAdmin"].ToString()) && Convert.ToInt32(dtMatchLoginDetails.Rows[0]["IsAdmin"].ToString()) == 1) //Pending
                    {
                        // For Admin.
                        objCUModel = new UserModel();
                        objCUModel.Ind = 1;
                        objCUModel.OrgID = Convert.ToInt32(dtMatchLoginDetails.Rows[0]["CompanyID"].ToString());

                        string uri2 = string.Format("User/LoadBranch");
                        DataTable dtLoadBranch = CommonCls.ApiPostDataTable(uri2, objCUModel);
                        if (dtLoadBranch.Rows.Count > 0)
                        {
                            ddlBranch.DataSource = dtLoadBranch;
                            ddlBranch.DataTextField = "BranchName";
                            ddlBranch.DataValueField = "BranchID";
                            ddlBranch.DataBind();
                            if (dtLoadBranch.Rows.Count == 1)
                            {
                                Response.Redirect("Defaults/Default.aspx");
                                return;
                            }

                            if (dtLoadBranch.Rows.Count > 1)
                            {
                                ddlBranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "ChooseBranch", "ChooseBranch()", true);
                            }
                        }

                    }
                    else
                    {   // Not For Admin.
                        Response.Redirect("Defaults/Default.aspx");
                    }
                }
                else
                {
                    ShowLoginError("User Not Active Please Contact Admin."); //Pending
                }
            }
        }
        catch (Exception ex)
        {
            ShowLoginError("Error : Internal Server Error");
        }
    }

    protected void btnBranchSubmit_Click(object sender, EventArgs e)
    {
        if (ddlBranch.Items.Count > 0)
        {
            if (ddlBranch.Items.Count > 1)
            {
                if (ddlBranch.SelectedIndex == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ContinueClick", "ContinueClick()", true);
                    return;
                }
                else
                {
                    GlobalSession.BrID = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                    GlobalSession.BrName = ddlBranch.SelectedItem.Text;
                    Response.Redirect("Defaults/Default.aspx");
                }
            }
            else
            {
                GlobalSession.BrID = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                GlobalSession.BrName = ddlBranch.SelectedItem.Text;
                Response.Redirect("Defaults/Default.aspx");
            }
        }

    }


    void ShowLoginError(string Message)
    {
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ClientScript.RegisterStartupScript(this.GetType(), "ShowLoginError", "$(function() { ShowLoginError('" + Message + "'); });", true);
    }

    public string GetIP()
    {
        string IpDetail = "";
        IpDetail = "Your IP address is: " + Request.ServerVariables["REMOTE_ADDR"].ToString();
        IpDetail += "You are browsing this site with: " + Request.ServerVariables["http_user_agent"].ToString();
        IpDetail += "The method used to call the page: " + Request.ServerVariables["request_method"].ToString();
        IpDetail += "The server's domain name: " + Request.ServerVariables["server_name"].ToString();
        IpDetail += "The server's port: " + Request.ServerVariables["server_port"].ToString();
        IpDetail += "The server's software: " + Request.ServerVariables["server_software"].ToString();
        IpDetail += "The DNS lookup of the IP address is: " + Request.ServerVariables["REMOTE_HOST"].ToString();

        return "";
    }


}
