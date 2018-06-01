using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmEWayBill : System.Web.UI.Page
{
    UserModel objUCModel;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objUCModel = new UserModel();
        objUCModel.Ind = 1;
        string uri = string.Format("EWayBill/LoadEWayBill");

        DataTable dtUser = CommonCls.ApiPostDataTable(uri, objUCModel);
        if (dtUser.Rows.Count > 0)
        {
          
        }
    }
}