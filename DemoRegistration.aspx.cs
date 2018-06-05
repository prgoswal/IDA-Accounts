using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DemoRegistration : System.Web.UI.Page
{
    DemoRegistrationModel ObjReg;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
//btn submit code
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        try
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                ShowMessage("Enter Full Name..!", false);
                txtName.Focus();
                return;
            }

            if (ddlOrgtype.SelectedIndex == 0)
            {
                ShowMessage(" Select Organization Type..!", false);
                ddlOrgtype.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtOrgName.Text))
            {
                ShowMessage("Enter Organization Name..!", false);
                txtOrgName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                ShowMessage("Please Enter EmailId", false);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMobilNo.Text))
            {
                ShowMessage("Please Enter MobileNo",false);
                txtMobilNo.Focus();
                return;
            }
            if (txtMobilNo.MaxLength < 10)
            {
                ShowMessage("Please Enter 10 Digit Mobile No.",false);
                txtMobilNo.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(txtOrgType.Text))
            //{
            //    lblMsg.Text = "Please Enter EmailId";
            //    txtOrgType.Focus();
            //    return;
            //}

            ObjReg = new DemoRegistrationModel();
            ObjReg.Ind = 1;
            ObjReg.User = txtName.Text.Trim();
            ObjReg.OrgType = ddlOrgtype.SelectedItem.Value;
            ObjReg.OrgName = txtOrgName.Text;
            ObjReg.MobNo = Convert.ToInt64(txtMobilNo.Text);
            ObjReg.Email = txtEmail.Text;

            DataTable dtSaveUser = CommonCls.ApiPostDataTable("DemoRegistration/Registration", ObjReg);
            if (dtSaveUser.Rows.Count > 0)
            {
                if (dtSaveUser.Rows[0]["RecordID"].ToString() == "0")
                {
                    ShowMessage("Email Address Already Registered.",false);
                }
                else
                {
                    if (SendEmailAndMessage())
                    {
                        GlobalSession.SendStatus = true;
                        //Response.Redirect("http://demo.gstsaathiaccounts.in/frmLoginDemo.aspx");
                        Response.Redirect("https://www.gstsaathiaccounts.in");
                        
                    }
                    else
                    {
                        ShowMessage("There Was Error Please Try Again.",false);
                    }
                }
            }
        }
        catch (Exception ex)
        {// when any runtime error accoured.
            ShowMessage("There Was Error Please Try Again.",false);
        }
    }
// this function send mails according to the mailing System.
    bool SendEmailAndMessage()
    {
        try
        {
            string EmailMessage = "Dear " + txtName.Text + "<br /> <br />" +

                                            "Thank you for showing interest in \"GST-Saathi\" Integrated Accounting Application. Your 60 days trial has been activated." + 
                                            //"We will be sending you the demo link and login credentials shortly. " +
                                            "<br /><br /><br /><br />" + 

                                            //"To access the application please go to : " + "http://115.124.113.64/GSTSaathiAccount/frmLogin.aspx  " + "<br /><br />" +
                                            "To access the application please go to : " + "https://www.gstsaathiaccounts.in" + "<br /><br />" +

                                            "User id : " + txtEmail.Text + "<br />" +
                                            "Password :a1@" + "<br /><br />" +
                                            //"Valid Only For 48 Hours." +"<br /><br />" +
                                            //"Valid For 60 Days." + "<br /><br />" +

                                            "Regards" + "<br /><br />" +


                                            "Team - \"GST-Saathi\"" + "<br />" +

                                            "Missed Call No. : 08030-63-63-48" + "<br />" +
                                            "Phone No. : +91-975502-44-44" + "<br /><br />" +
                                            "60, Electronics Complex" + "<br />" +
                                            "Pardeshipura" + "<br />" +
                                            "Indore-452010 (M.P.)" + "<br />" +
                                            "India." + "<br />" +
                                            "http://www.gstsaathi.com";

            string MobileMessage = "Dear " + txtName.Text + Environment.NewLine + "Thank you for showing interest in \"GST-Saathi\" Integrated Accounting Application. Your 60 days trial has been activated." + Environment.NewLine +
                //" We will be sending you the demo link and login credentials shortly.";// + Environment.NewLine;
                                "Login Id : " + txtEmail.Text + Environment.NewLine + "Password : a1@ " + Environment.NewLine +
                                //"Valid Only For 48 Hours." + Environment.NewLine +
                                //"Valid For 60 Days." + Environment.NewLine +

                                "Link : " + "https://www.gstsaathiaccounts.in";
                                //"Demo Link : " + "https://goo.gl/KQrjX8";
                                //"Demo Link : " + "https://goo.gl/oL29jy";
                                //"Demo Link : " + "http://bit.ly/2sJSFJ7 ";

        SendRequest.SendHtmlFormattedEmail(EmailMessage, txtEmail.Text);
        SendRequest.SendMessage(txtMobilNo.Text, MobileMessage);
        return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }
}
