using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_AddInDB_GSTIN_Add : System.Web.UI.UserControl
{
    public DropDownList DropDownList
    {
        get { return ddlGSTIN; }
        set { ddlGSTIN = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}