using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmModuleRights : System.Web.UI.Page
{
    public static int editsavecheck = 0;
    static DataTable getLevel, getmenu;
    DataTable dt;//getLevel,getmenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            editsavecheck = 0;
            BindUserProfile();//For User Profile Purpose
            BindDocumentMenu();//For User Menu Purpose
            Matrix();//For User Profile & Menu (Both) Diplayed Into Grid Purpose - With Checkbox           
            //ShowProfile();
            // string s = GridMatrix.HeaderRow.Cells[3].Text.ToString().Replace("DEGREE CERTIFICATE", "DEGREE");
        }
    }

    public void Matrix()
    {
        try
        {
            dt = new DataTable();
            dt = getLevel.Copy();
            for (int j = 0; j < getmenu.Rows.Count; j++)
                dt.Columns.Add(new DataColumn(getmenu.Rows[j][1].ToString(), typeof(bool)));

            //dt.Columns["DEGREE CERTIFICATE"].ColumnName = "DEGREE";
            //dt.Columns["PROVISIONAL DEGREE CERTIFICATE"].ColumnName = "PROVISIONAL DEGREE";
            //dt.Columns["PASSING CERTIFICATE"].ColumnName = "PASSING";
            //dt.Columns["MERIT CERTIFICATE"].ColumnName = "MERIT";
            //dt.Columns["ATTEMPT CERTIFICATE"].ColumnName = "ATTEMPT";
            //dt.Columns["MEDAL CERTIFICATE"].ColumnName = "MEDAL";
            //dt.Columns["DUPLICATE MARKSHEET"].ColumnName = "DUPLICATE";
            //dt.Columns["MARKSHEET VERIFICATION CERTIFICATE / LETTER"].ColumnName = "MARKSHEET VERIFICATION";
            //dt.Columns["DEGREE VERIFICATION CERTIFICATE / LETTER"].ColumnName = "DEGREE VERIFICATION";
            //dt.Columns["DATE OF DECLARATION CERTIFICATE / LETTER"].ColumnName = "DATE OF DECLARATION ";
            //dt.Columns["MEDIUM OF INSTRUCTION CERTIFICATE / LETTER"].ColumnName = "MEDIUM OF INSTRUCTION";

            GridMatrix.DataSource = dt;
            GridMatrix.DataBind();
            //For Hiding Profile Level Value From Grid
            GridMatrix.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow gvr in GridMatrix.Rows)
            {
                gvr.Cells[1].Visible = false;
                gvr.Cells[2].Width = 150;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    public DataTable BindDocumentMenu()
    {
        try
        {
            objUserRollModel = new UserRollModel();
            objUserRollModel.Ind = 3;
            objUserRollModel.OrgID = GlobalSession.OrgID;
            objUserRollModel.BrID = GlobalSession.BrID;

            string uri = string.Format("UserRoll/BindUserRoll");
            DataTable dtBranch = CommonCls.ApiPostDataTable(uri, objUserRollModel);
            getmenu = null;
            getmenu = dtBranch;
            //  getmenu.Rows["DEGREE CERTIFICATE"] = "DEGREE";
            // getmenurow["DEGREE CERTIFICATE"] = "";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return getmenu;
    }
    UserRollModel objUserRollModel;
    public DataTable BindUserProfile()
    {
        try
        {
            objUserRollModel = new UserRollModel();
            objUserRollModel.Ind = 2;
            objUserRollModel.OrgID = GlobalSession.OrgID;
            objUserRollModel.BrID = GlobalSession.BrID;

            string uri = string.Format("UserRoll/BindUserRoll");
            DataTable dtBranch = CommonCls.ApiPostDataTable(uri, objUserRollModel);

            getLevel = dtBranch;
            getLevel.Columns["RollDesc"].ColumnName = "USER PROFILE";

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return getLevel;
    }
    protected void GridMatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbSave = (LinkButton)GridMatrix.FindControl("linkbtnSave");
            if (lbSave != null)
            {
                lbSave.Visible = false;
            }
            else
            {
                return;
            }
        }
    }
}