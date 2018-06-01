using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmGstinlist : System.Web.UI.Page
{
    ClientGstinModel objplClientGstin;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
        
    }

    void fillGrid()
    {
        objplClientGstin = new ClientGstinModel()
        {
            Ind = 1,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
        };
        string uri = string.Format(" ClientCstin/FillGridGstin");
        DataTable FillGridClientGstin = CommonCls.ApiPostDataTable(uri, objplClientGstin);
        if (FillGridClientGstin.Rows.Count > 0)
        {
            gridgstin.DataSource = FillGridClientGstin;
            gridgstin.DataBind();
        }
    }

    protected void gridgstin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvr = gridgstin.Rows[index];
        
        Session["ASPClientCode"] = (gvr.FindControl("lblASPClientCode") as Label).Text;
        Session["ASPClientCodeODP"] = (gvr.FindControl("lblASPClientCodeODP") as Label).Text;
        Session["ASPCACode"] = (gvr.FindControl("lblASPCACode") as Label).Text;
        Session["ASPCACodeODP"] = (gvr.FindControl("lblASPCACodeODP") as Label).Text;

        Session["GSTIN"] = (gvr.FindControl("lblGstin") as Label).Text;
        Session["Address"] = (gvr.FindControl("lblAddress") as Label).Text;
        Response.Redirect("frmGstinsubmit.aspx");

    }
}