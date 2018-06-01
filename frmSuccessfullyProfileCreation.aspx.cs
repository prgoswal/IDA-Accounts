using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmSuccessfullyProfileCreation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblCompanyName.Text = GlobalSession.OrgName;
        if (!string.IsNullOrEmpty(Request.QueryString["ProfileCreation"]))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["OrgName"]))
            {
                lblCompanyName.Text = Request.QueryString["OrgName"].ToString();
                lblSuccessfullyMSG.Text = Request.QueryString["ProfileCreation"].ToString();
            }
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["UpdateProfileCreation"]))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["UpdateOrgName"]))
            {
                lblCompanyName.Text = Request.QueryString["UpdateOrgName"].ToString();
                lblSuccessfullyMSG.Text = Request.QueryString["UpdateProfileCreation"].ToString();
            }
        }

    }
}