using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Updation_frmUpdGroupMaster : System.Web.UI.Page
{
    UpdateGroupMasterModel objUpdplGrptype;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
        //{
        //    btnUpdate.Visible = true;
        //    Session["MasterWrite"] = 0;

        //}
    }
    public void fillGrid()
    {
        try
        {
            objUpdplGrptype = new UpdateGroupMasterModel();
            objUpdplGrptype.Ind = 4;
            objUpdplGrptype.OrgID = GlobalSession.OrgID;
            objUpdplGrptype.YrCD = GlobalSession.YrCD;
            objUpdplGrptype.GrType = Convert.ToInt32(ddlGrpType.SelectedValue);

            string uri = string.Format("UpdateGroupTypeMaster/FillGrid");
            DataTable GroupFillGrid = CommonCls.ApiPostDataTable(uri, objUpdplGrptype);
            if (GroupFillGrid.Rows.Count > 0)
            {
                grdGroupType.DataSource = GroupFillGrid;
                grdGroupType.DataBind();

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void ddlGrpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGrpType.SelectedValue == "0")
        {
            lblgrtypeName.Text = "";
            grdGroupType.DataSource = null;
            grdGroupType.DataBind();
        }
        if (ddlGrpType.SelectedValue == "1")
        {
            lblgrtypeName.Text = "Available Main GroupType";
        }
        else if (ddlGrpType.SelectedValue == "2")
        {
            lblgrtypeName.Text = "Available Sub GroupType";
        }
        else if (ddlGrpType.SelectedValue == "3")
        {
            lblgrtypeName.Text = "Available Minor GroupType";
        }

        fillGrid();

    }
    int ItemGroupID
    {
        get { return CommonCls.ConvertIntZero(ViewState["ItemGroupID"]); }
        set { ViewState["ItemGroupID"] = value; }
    }
    protected void grdGroupType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            ItemGroupID = 0;
            txtItmGrpName.Enabled = true;
            int rowIndex = Convert.ToInt32(e.NewEditIndex);
            Label lblGroupId = (Label)grdGroupType.Rows[rowIndex].FindControl("lblGroupId");
            Label lblGroupName = (Label)grdGroupType.Rows[rowIndex].FindControl("lblGroupName");
            ItemGroupID = Convert.ToInt32(lblGroupId.Text);
            txtItmGrpName.Text = lblGroupName.Text;
            txtItmGrpName.Focus();
            lblMsg.Text = "";
            //grdGroupType.Rows[rowIndex].CssClass = "bg-primary";
        }
        catch (Exception ex)
        {

        }
    }

    public void AllClear()
    {
        ddlGrpType.Focus();
        ddlGrpType.ClearSelection();
        txtItmGrpName.Text = "";
        lblMsg.Text = "";
        lblgrtypeName.Text = "";
        grdGroupType.DataSource = null;
        grdGroupType.DataBind();
        txtItmGrpName.Enabled = false;

    }
    public void clear()
    {
        ddlGrpType.ClearSelection();
        txtItmGrpName.Text = "";
        txtItmGrpName.Enabled = false;
        //grdGroupType.DataSource = null;
        //grdGroupType.DataBind();
        lblgrtypeName.Text = "";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        AllClear();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (ddlGrpType.SelectedValue == "0")
            {
                ShowMessage("Group name is required.", false);
                ddlGrpType.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtItmGrpName.Text))
            {
                ShowMessage("Edit item is required.", false);
                txtItmGrpName.Focus();
                return;
            }
            if (ItemGroupID != 0)
            {
                objUpdplGrptype = new UpdateGroupMasterModel();

                objUpdplGrptype.Ind = 5;
                objUpdplGrptype.OrgID = GlobalSession.OrgID;
                objUpdplGrptype.BrID = GlobalSession.BrID;
                objUpdplGrptype.YrCD = GlobalSession.YrCD;
                objUpdplGrptype.User = GlobalSession.UserID;
                objUpdplGrptype.IP = GlobalSession.IP;
                objUpdplGrptype.GrType = Convert.ToInt32(ddlGrpType.SelectedItem.Value);
                objUpdplGrptype.ItemGroupID = ItemGroupID;
                objUpdplGrptype.GrDesc = txtItmGrpName.Text;
                string uri = string.Format("UpdateGroupTypeMaster/UpdateProcess");
                DataTable GroupTypeUpdateProcess = CommonCls.ApiPostDataTable(uri, objUpdplGrptype);
                if (GroupTypeUpdateProcess.Rows.Count > 0)
                {
                    ShowMessage("Data Is SucessFully Updated..!", true);
                    ItemGroupID = 0;
                }
                else
                {
                    ShowMessage(" Data Is Not Updated SucessFully..!", false);
                }
                fillGrid();
                clear();
            }

        }
        catch (Exception ex)
        {

        }
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = Message;
        lblMsg.CssClass = type ? "alert alert-info" : "alert alert-danger";

        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }
}