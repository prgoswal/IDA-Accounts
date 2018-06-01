using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmGroupMaster : System.Web.UI.Page
{
    GroupTypeMasterModel objplgroupType;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillGrid();

            //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            //{
                //btnSave.Visible = true;
            //    Session["MasterWrite"] = 0;
            //}
        }
    }

   
    //public void fillGrid()
    //{
    //    try
    //    {
    //        objplgroupType = new GroupTypeMasterModel();
    //        objplgroupType.Ind = 3;
    //        objplgroupType.OrgID = GlobalSession.OrgID;
    //        objplgroupType.YrCD = GlobalSession.YrCD;

    //        string uri = string.Format("GroupTypeMaster/FillMainType");
    //        DataTable GroupFillGrid = CommonCls.ApiPostDataTable(uri, objplgroupType);
    //        if (GroupFillGrid.Rows.Count > 0)
    //        {
    //            grdItemGroup.DataSource = GroupFillGrid;
    //                grdItemGroup.DataBind();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // ShowMessage(ex.Message, false);
    //    }



    //}


    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlGroupType.Focus();
        ddlGroupType.ClearSelection();
        txtName.Text = "";
        lblMsg.Text = "";
        divddlMainGrp.Visible = false;
        divddlsubgrp.Visible = false;
        ddlSubGroup.Items.Clear();
        ddlSubGroup.DataSource = null;


    }
    void clear()
    {
        ddlGroupType.ClearSelection();
        txtName.Text = "";
        ddlMainGroup.ClearSelection();
        ddlSubGroup.ClearSelection();
        ddlSubGroup.Items.Clear();
        divddlMainGrp.Visible = false;
        divddlsubgrp.Visible = false;

    }
    protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable GroupTypeMainlist;
        if (ddlGroupType.SelectedValue == "2")
        {
            divddlMainGrp.Visible = true;
            divddlsubgrp.Visible = false;

            FillMaingroup();
        }
        else if (ddlGroupType.SelectedValue == "3")
        {
            divddlMainGrp.Visible = true;
            divddlsubgrp.Visible = true;
            FillMaingroup();
            // fillSubGroup();
        }
        else if (ddlGroupType.SelectedValue == "1")
        {

            divddlMainGrp.Visible = false;
            divddlsubgrp.Visible = false;
            ddlSubGroup.DataSource = new DataTable();
            ddlSubGroup.DataBind();
        }
        else
        {
            divddlMainGrp.Visible = false;
            divddlsubgrp.Visible = false;
            ddlSubGroup.DataSource = new DataTable();
            ddlSubGroup.DataBind();
        }
    }
    void FillMaingroup()
    {
        try
        {
            objplgroupType = new GroupTypeMasterModel();
            objplgroupType.Ind = 3;
            objplgroupType.OrgID = GlobalSession.OrgID;
            objplgroupType.YrCD = GlobalSession.YrCD;

            string uri = string.Format("GroupTypeMaster/FillMainType");
            DataTable GroupTypeMainlist = CommonCls.ApiPostDataTable(uri, objplgroupType);
            if (GroupTypeMainlist.Rows.Count > 0)
            {

                DataView dv = new DataView(GroupTypeMainlist);
                dv.RowFilter = "GroupType = 1"; // query example = "id = 10"
                ddlMainGroup.DataSource = dv;
                ddlMainGroup.DataTextField = "GroupName";
                ddlMainGroup.DataValueField = "ItemGroupId";
                ddlMainGroup.DataBind();
                ddlMainGroup.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }
    void fillSubGroup()
    {
        try
        {
            objplgroupType = new GroupTypeMasterModel();
            objplgroupType.Ind = 3;
            objplgroupType.OrgID = GlobalSession.OrgID;
            objplgroupType.BrID = GlobalSession.BrID;
            objplgroupType.YrCD = GlobalSession.YrCD;


            string uri = string.Format("GroupTypeMaster/FillMainType");
            DataTable GroupTypeSublist = CommonCls.ApiPostDataTable(uri, objplgroupType);
            if (GroupTypeSublist.Rows.Count > 0)
            {
                int mainGroupID = Convert.ToInt32(ddlMainGroup.SelectedValue);
                DataView dvsub = new DataView(GroupTypeSublist);
                dvsub.RowFilter = "GroupType = 2 AND ParentItemGroupID = " + mainGroupID + "";
                ddlSubGroup.DataSource = dvsub;
                ddlSubGroup.DataTextField = "GroupName";
                ddlSubGroup.DataValueField = "ItemGroupID";
                ddlSubGroup.DataBind();
                ddlSubGroup.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (ddlGroupType.SelectedValue == "0")
            {
                ddlGroupType.Focus();
                lblMsg.Text = "Select Group Type..!";
                return;
            }
            if (ddlGroupType.SelectedValue == "2")
            {
                if (ddlMainGroup.SelectedValue == "0")
                {
                    ddlMainGroup.Focus();
                    lblMsg.Text = "Select  Main Group!";
                    return;
                }
            }
            else if (ddlGroupType.SelectedValue == "3")
            {
                if (ddlMainGroup.SelectedValue == "0")
                {
                    ddlMainGroup.Focus();
                    lblMsg.Text = "Select  Main Group!";
                    return;
                }
                if (ddlSubGroup.SelectedValue == "0")
                {
                    lblMsg.Text = "Select  Sub Group!";
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                txtName.Focus();
                lblMsg.Text = "Enter Group Name..! ";
                return;
            }
            objplgroupType = new GroupTypeMasterModel();
            objplgroupType.Ind = 1;
            objplgroupType.OrgID = GlobalSession.OrgID;
            objplgroupType.User = GlobalSession.UserID;
            objplgroupType.IP = GlobalSession.IP;

            objplgroupType.GrType = Convert.ToInt32(ddlGroupType.SelectedItem.Value);
            if (ddlGroupType.SelectedValue == "1")
            {
                objplgroupType.ParentID = 0;
            }
            else if (ddlGroupType.SelectedValue == "2")
            {
                objplgroupType.ParentID = Convert.ToInt32(ddlMainGroup.SelectedValue);
            }
            else if (ddlGroupType.SelectedValue == "3")
            {
                objplgroupType.ParentID = Convert.ToInt32(ddlSubGroup.SelectedValue);
            }

            //objplgroupType.ParentID = string.IsNullOrEmpty(ddlMainGroup.SelectedValue) ? 0 : ddlSubGroup.SelectedValue;
            //objplgroupType.ParentID = Convert.ToInt32(ddlMainGroup.SelectedValue == null ? "0" : ddlMainGroup.SelectedValue == null ? "0" : ddlSubGroup.SelectedValue); 

            objplgroupType.GrDesc = txtName.Text;
            string uri = string.Format("GroupTypeMaster/SaveGroupItem");
            DataTable GroupMasterSaveProcess = CommonCls.ApiPostDataTable(uri, objplgroupType);
            if (GroupMasterSaveProcess.Rows.Count > 0)
            {
                if (GroupMasterSaveProcess.Rows[0]["RecordID"].ToString() == "1")
                {
                    lblMsg.Text = "Data Is SucessFully Saved..!";
                }
                else if (GroupMasterSaveProcess.Rows[0]["RecordID"].ToString() == "2")
                {
                    lblMsg.Text = "Data Already Added.";
                }
            }
            else
            {
                lblMsg.Text = "Data Is Not Saved..!";
            }
            clear();

        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlMainGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMainGroup.SelectedValue != "0")
        {
            fillSubGroup();
            ddlSubGroup.Enabled = true;
        }
        else
        {
            ddlSubGroup.DataSource = new DataTable();
            ddlSubGroup.DataBind();
            ddlSubGroup.Enabled = false;
        }
    }
}