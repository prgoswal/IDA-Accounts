using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserModel objCUModel = new UserModel();
            objCUModel.Ind = 8;
            objCUModel.LoginID = GlobalSession.UserLoginID;
            objCUModel.LoginPass = "0";

            string uri = string.Format("User/Logout");
            DataTable dtUserStatus = CommonCls.ApiPostDataTable(uri, objCUModel);
            if (dtUserStatus.Rows.Count > 0)
            {
                if (dtUserStatus.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    
                }
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            //new GlobalSession();
            GlobalSession.LogOut();
            Response.Redirect("~/frmLogin.aspx");
        }

    }
}