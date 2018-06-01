using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_FrmBudgetRights : System.Web.UI.Page
{
    BudgetUserRightsModel objBRModel; DataTable dtSectionName;

    DataTable VSSection
    {
        get { return (DataTable)ViewState["VSSection"]; }
        set { ViewState["VSSection"] = value; }
    }


    DataTable VSFilterSection
    {
        get { return (DataTable)ViewState["VSFilterSection"]; }
        set { ViewState["VSFilterSection"] = value; }
    }
    DataTable VSFilterSubSection
    {
        get { return (DataTable)ViewState["VSFilterSubSection"]; }
        set { ViewState["VSFilterSubSection"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = lblMsg.CssClass = "";
            BindAllDDL();
        }
        //ChkSelected.Attributes.Add("onclick", "radioMe(event);");
    }

    void BindAllDDL()
    {
        try
        {
            objBRModel = new BudgetUserRightsModel();
            objBRModel.Ind = 1;
            objBRModel.OrgID = GlobalSession.OrgID;
            objBRModel.BrID = GlobalSession.BrID;
            string uri = string.Format("BudgetUserRights/BindAllDDL");
            DataSet dsBindAllDDl = CommonCls.ApiPostDataSet(uri, objBRModel);
            if (dsBindAllDDl.Tables.Count > 0)
            {
                DataTable dtUserName = dsBindAllDDl.Tables[0];
                dtSectionName = VSSection = dsBindAllDDl.Tables[1];

                if (dtUserName.Rows.Count > 0)
                {
                    ddlUserName.DataSource = dtUserName;
                    ddlUserName.DataTextField = "DeptName";
                    ddlUserName.DataValueField = "DeptID";
                    ddlUserName.DataBind();

                    if (dtUserName.Rows.Count > 1)
                        ddlUserName.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
                else
                {
                    ShowMessage("There is no any Designation.", false);
                }
                if (dtSectionName.Rows.Count > 0)
                {
                    ViewState["dtCheckListAvailable"] = dtSectionName;
                    chkAvailable.DataSource = VSFilterSection = dtSectionName;
                    chkAvailable.DataTextField = "SectionName";
                    chkAvailable.DataValueField = "SectionID";
                    chkAvailable.DataBind();
                }
                else
                {
                    ShowMessage("There is no any section.", false);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlUserName.SelectedValue == "0")
        {
            ShowMessage("Select Designation.", false);
            ddlUserName.Focus();
            return;
        }
        if (ddlAccordingTo.SelectedValue == "0")
        {
            ShowMessage("Select According To Rights.", false);
            ddlAccordingTo.Focus();
            return;
        }

        objBRModel = new BudgetUserRightsModel();
        objBRModel.Ind = 2;
        objBRModel.OrgID = GlobalSession.OrgID;
        objBRModel.BrID = GlobalSession.BrID;
        objBRModel.YrCD = GlobalSession.BudgetYrCD;

        objBRModel.DepartmentID = CommonCls.ConvertIntZero(ddlUserName.SelectedValue);
        objBRModel.SubDeptID = CommonCls.ConvertIntZero(ddlUserName.SelectedValue);

        DataTable dt = CreateBudgetRightsData();
        if (dt == null)
        {
            ShowMessage("Allocated Rights first then submit", false);
            return;
        }
        objBRModel.TblUserRights = JsonConvert.SerializeObject(dt);
        //objBRModel.TblUserRights = CreateBudgetRightsData();
        string uri = string.Format("BudgetUserRights/SaveBudgetRights");
        DataSet dtSaveItem = CommonCls.ApiPostDataSet(uri, objBRModel);
        if (dtSaveItem.Tables[0].Rows.Count > 0)
        {
            ShowMessage("Data Save Successfully!", true);
            ClearAll();
            VSSection = dtSaveItem.Tables[1];
        }
        else
        {
            ShowMessage("Data not saved successfully.", false);
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        lblMsg.Text = lblMsg.CssClass = "";
    }

    private void ClearAll()
    {
        ddlUserName.ClearSelection();
        ddlAccordingTo.ClearSelection();
        chkAvailable.DataSource = null;
        chkAvailable.DataTextField = null;
        chkAvailable.DataValueField = null;
        chkAvailable.DataBind();
        // lblAvailable.Text = lblAllocated.Text = "";
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        ChkSelected.DataSource = dt;
        ChkSelected.DataTextField = "Name";
        ChkSelected.DataValueField = "ID";
        ChkSelected.DataBind();
        chkAvailable.DataBind();
        ddlAccordingTo.Enabled = true;
        VSFilterSection = VSFilterSubSection = null;
        ViewState["CheckListViewSelect"] = ViewState["dtCheckListAvailable"] = null;
        pnlRemoveAll.Visible = pnlAddAll.Visible = false;
    }
    protected void ddlAccordingTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserName.SelectedValue == "0")
            {
                ShowMessage("Select Designation.", false);
                ddlUserName.Focus();
                ddlAccordingTo.SelectedValue = "0";
                return;
            }

            //if (ddlAccordingTo.SelectedValue == "0")
            //{
            //    lblAllocated.Text = lblAvailable.Text = "";
            //    return;
            //}
            //lblAllocated.Text = lblAvailable.Text = ddlAccordingTo.SelectedItem.Text;
            if (VSSection.Rows.Count > 0)
            {
                DataView dvSection = new DataView(VSSection);
                if (ddlAccordingTo.SelectedValue == "1")
                {
                    dvSection.RowFilter = "ParentSectionID =0";
                    dvSection.Sort = "SectionName";
                    ViewState["dtCheckListAvailable"] = dvSection.ToTable();
                    chkAvailable.DataSource = VSFilterSection = dvSection.ToTable();
                    chkAvailable.DataTextField = "SectionName";
                    chkAvailable.DataValueField = "SectionID";
                    chkAvailable.DataBind();

                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("SectionID", typeof(int));
                    //dt.Columns.Add("UserSectionName", typeof(string));
                    //ChkSelected.DataSource = dt;
                    //ChkSelected.DataTextField = "UserSectionName";
                    //ChkSelected.DataValueField = "SectionID";
                    //ChkSelected.DataBind();

                }
                else if (ddlAccordingTo.SelectedValue == "2")
                {
                    dvSection.RowFilter = "ParentSectionID >0";
                    dvSection.Sort = "SectionName";
                    ViewState["dtCheckListAvailable"] = dvSection.ToTable();
                    chkAvailable.DataSource = VSFilterSection = dvSection.ToTable();
                    chkAvailable.DataTextField = "SectionName";
                    chkAvailable.DataValueField = "SectionID";
                    chkAvailable.DataBind();

                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("SectionID", typeof(int));
                    //dt.Columns.Add("UserSectionName", typeof(string));
                    //ChkSelected.DataSource = dt;
                    //ChkSelected.DataTextField = "UserSectionName";
                    //ChkSelected.DataValueField = "SectionID";
                    //ChkSelected.DataBind();
                }
                else
                {
                    ShowMessage("Something Went Wrong. Please Contact to Administrator.", false);
                }
            }

            else
            {
                ShowMessage("Section & Sub-Section Not Available. Please Open It.", false);
            }
            // ViewState["dtCheckListAvailable"] = null;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            txtfilter.Text = "";
            bool flag = false;
            foreach (ListItem item in chkAvailable.Items)
            {
                if (item.Selected)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                ShowMessage(" Please Check First In Available List! ", false);
                return;
            }
            else lblMsg.Text = lblMsg.CssClass = "";

            DataTable dtCheckList = new DataTable();
            DataTable dtCheckListAvailable = new DataTable();
            if (ViewState["CheckListViewSelect"] == null)
            {
                dtCheckList.Columns.Add("UserSectionName");
                dtCheckList.Columns.Add("SectionID");
            }
            else
            {
                //if (((DataTable)ViewState["CheckListViewSelect"]).Columns.Count==0)
                //{
                //    dtCheckList.Columns.Add("AccName");
                //    dtCheckList.Columns.Add("AccCode");
                //}
                dtCheckList = (DataTable)ViewState["CheckListViewSelect"];
            }
            //test start 
            //if (ViewState["dtCheckListAvailable"] == null)
            //{

            //    dtCheckListAvailable.Columns.Add("SectionName");
            //    dtCheckListAvailable.Columns.Add("SectionID");

            //}
            //else
            //{
            //    dtCheckListAvailable = (DataTable)ViewState["dtCheckListAvailable"];
            //}
            //test end

            int i = 0;
            dtCheckListAvailable = (DataTable)ViewState["dtCheckListAvailable"];
            foreach (ListItem item in chkAvailable.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = dtCheckList.NewRow();
                    //dr["AccName"] = item.Text;
                    dr["UserSectionName"] = item.Text;
                    dr["SectionID"] = item.Value.ToString();
                    dtCheckList.Rows.Add(dr);
                    dtCheckListAvailable.Rows.RemoveAt(i);
                    ViewState["dtCheckListAvailable"] = dtCheckListAvailable;
                }
                else { i++; }
            }

            ViewState["CheckListViewSelect"] = dtCheckList;

            //DataView dvSelected = dtCheckList.DefaultView;
            //dvSelected.Sort = "UserSectionName";
            //DataTable sortedDTSelected = dvSelected.ToTable();
            //ViewState["CheckListViewSelect"] = sortedDTSelected;

            ChkSelected.DataSource = dtCheckList;
            ChkSelected.DataValueField = "SectionID";
            ChkSelected.DataTextField = "UserSectionName";
            ChkSelected.DataBind();
            if (dtCheckList.Rows.Count > 0)
            {
                ddlAccordingTo.Enabled = false;
            }

            //DataView dvAvailable = dtCheckListAvailable.DefaultView;
            //dvAvailable.Sort = "SectionName";
            //DataTable sortedDTAvailable = dvAvailable.ToTable();

            chkAvailable.DataSource = dtCheckListAvailable;
            chkAvailable.DataTextField = "SectionName";
            chkAvailable.DataValueField = "SectionID";
            chkAvailable.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnAddAl_Click(object sender, EventArgs e)
    {
        pnlAddAll.Visible = true;
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dtCheckList = new DataTable();
            DataTable dtCheckListAvailable = new DataTable();
            if (ViewState["CheckListViewSelect"] == null)
            {
                dtCheckList.Columns.Add("AccName");
                dtCheckList.Columns.Add("AccCode");
            }
            else
            {
                dtCheckList = (DataTable)ViewState["CheckListViewSelect"];
            }
            int i = 0;
            dtCheckListAvailable = (DataTable)ViewState["dtCheckListAvailable"];
            foreach (ListItem item in ChkSelected.Items)
            {
                if (item.Selected)
                {
                    //if (MatchInd(item.Value.ToString()) == 0)
                    //{
                    DataRow dr = dtCheckListAvailable.NewRow();

                    dr["SectionName"] = item.Text;
                    dr["SectionID"] = item.Value.ToString();

                    dtCheckListAvailable.Rows.Add(dr);

                    dtCheckList.Rows.RemoveAt(i);

                    ViewState["dtCheckListAvailable"] = dtCheckListAvailable;
                    ShowMessage("Item Removed Successfully.", false);
                    //}
                    //else
                    //{
                    //    i++;
                    //    lblMsg.Text = "Item Not Removed Because Invoice Created Using That.";
                    //    break;
                    //}
                }
                else { i++; }
            }

            ViewState["CheckListViewSelect"] = dtCheckList;

            //DataView dvSelected = dtCheckList.DefaultView;
            //dvSelected.Sort = "UserSectionName";
            //DataTable sortedDTSelected = dvSelected.ToTable();

            //ViewState["CheckListViewSelect"] = sortedDTSelected;

            ChkSelected.DataSource = dtCheckList;
            ChkSelected.DataValueField = "SectionID";
            ChkSelected.DataTextField = "UserSectionName";
            ChkSelected.DataBind();

            if (dtCheckList.Rows.Count == 0)
            {
                ddlAccordingTo.Enabled = true;
            }

            //DataView dvAvailable = dtCheckListAvailable.DefaultView;
            //dvAvailable.Sort = "SectionName";
            //DataTable sortedDTAvailable = dvAvailable.ToTable();

            chkAvailable.DataSource = dtCheckListAvailable;
            chkAvailable.DataTextField = "SectionName";
            chkAvailable.DataValueField = "SectionID";
            chkAvailable.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    //public int MatchInd(string SelectedValue)
    //{

    //    objSundriModel = new SundriesModel();
    //    objSundriModel.Ind = 2;
    //    objSundriModel.OrgID = GlobalSession.OrgID;
    //    objSundriModel.SundriCode = int.Parse(SelectedValue);

    //    string uri = string.Format("Sundri/MatchInd");
    //    DataTable dsMatchInd = CommonCls.ApiPostDataTable(uri, objSundriModel);

    //    return int.Parse(dsMatchInd.Rows[0]["InvoiceCnt"].ToString());

    //}
    protected void btnRemvAl_Click(object sender, EventArgs e)
    {
        pnlRemoveAll.Visible = true;

    }



    DataTable dtForBudgetRightsSchema()     //Create DataTable..
    {
        DataTable dtForBudgetRights = new DataTable();
        dtForBudgetRights.Columns.Add("CompanyID", typeof(int));
        dtForBudgetRights.Columns.Add("UserID", typeof(int));
        dtForBudgetRights.Columns.Add("SectionID", typeof(int));
        dtForBudgetRights.Columns.Add("SectionType", typeof(int));
        dtForBudgetRights.Columns.Add("IPAddress", typeof(string));
        dtForBudgetRights.Columns.Add("CreatedBy", typeof(int));
        dtForBudgetRights.Columns.Add("DepartmentID", typeof(int));
        dtForBudgetRights.Columns.Add("SubDeptID", typeof(int));
        return dtForBudgetRights;
    }

    DataTable CreateBudgetRightsData()
    {
        DataTable dtForBudgetRights = new DataTable();
        try
        {
            dtForBudgetRights = dtForBudgetRightsSchema(); //New DataTable

            foreach (ListItem item in ChkSelected.Items)
            {
                DataRow drForBudgetRights = dtForBudgetRights.NewRow();

                drForBudgetRights["CompanyID"] = GlobalSession.OrgID;
                drForBudgetRights["UserID"] = 0;
                drForBudgetRights["SectionID"] = Convert.ToInt32(item.Value);
                if (ddlAccordingTo.SelectedValue == "1")
                {
                    drForBudgetRights["SectionType"] = 1;
                }
                else
                {
                    drForBudgetRights["SectionType"] = 2;
                }
                drForBudgetRights["IPAddress"] = GlobalSession.IP;
                drForBudgetRights["CreatedBy"] = GlobalSession.UserID;
                drForBudgetRights["DepartmentID"] = ddlUserName.SelectedValue;
                drForBudgetRights["SubDeptID"] = 0;
                dtForBudgetRights.Rows.Add(drForBudgetRights);
            }
        }
        catch (Exception)
        {

        }
        return dtForBudgetRights;
    }
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        try
        {
            //if (ddlAccordingTo.Enabled == true)
            //{
            //    ddlAccordingTo.Enabled = false;
            //}
            //ddlAccordingTo.Enabled = true;
            //ddlAccordingTo.SelectedValue = "0";

            if (ddlUserName.SelectedValue == "0")
            {
                ddlUserName.Focus();
                return;
            }
            objBRModel = new BudgetUserRightsModel();
            objBRModel.Ind = 11;
            objBRModel.OrgID = GlobalSession.OrgID;
            objBRModel.DepartmentID = CommonCls.ConvertIntZero(ddlUserName.SelectedValue);
            //objBRModel.SubDeptID = CommonCls.ConvertIntZero(ddlUserName.SelectedValue);


            string uri = string.Format("BudgetUserRights/AllotedBudgetRights");
            DataTable dtAllotedBudgetRights = CommonCls.ApiPostDataTable(uri, objBRModel);
            if (dtAllotedBudgetRights.Rows.Count > 0)
            {
                ddlAccordingTo.SelectedValue = dtAllotedBudgetRights.Rows[0]["SectionType"].ToString();
                ddlAccordingTo.Enabled = false;

                ChkSelected.DataSource = dtAllotedBudgetRights;
                ChkSelected.DataTextField = "UserSectionName";
                ChkSelected.DataValueField = "SectionID";
                ChkSelected.DataBind();
                if (dtAllotedBudgetRights.Columns.Count > 0)
                {
                    ViewState["CheckListViewSelect"] = dtAllotedBudgetRights;
                }
                else
                {

                }
                ddlAccordingTo_SelectedIndexChanged(sender, e);

                DataTable dtSection = VSFilterSection;
                DataView dvSection = new DataView(dtSection);

                foreach (DataRow rowAlloted in dtAllotedBudgetRights.Rows)
                {

                    foreach (DataRow rowUnAlloted in dtSection.Rows)
                    {
                        if (rowUnAlloted["SectionID"].ToString() == rowAlloted["SectionID"].ToString())
                        {
                            dtSection.Rows.Remove(rowUnAlloted);
                            break;
                        }
                    }
                    continue;
                }
                //if (dtSection.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtAllotedBudgetRights.Rows.Count; i++)
                //    {
                //        for (int j = 0; j < dtSection.Rows.Count; j++)
                //        {
                //            dvSection.RowFilter = "SectionID  =" + dtAllotedBudgetRights.Rows[i]["SectionID"] + "";
                //        }
                //    }
                //}


                //foreach (DataRow rows1 in dvSection.ToTable().Rows)
                //{
                //    foreach (DataRow rows2 in dtAllotedBudgetRights.Rows)
                //    {
                //        dvSection.RowFilter = "SectionID Not =" + rows2["SectionID"] + "";
                //    }
                //}



                //DataView ReplaceSubSectionTable = dvSection;

                ViewState["dtCheckListAvailable"] = dtSection;
                chkAvailable.DataSource = dtSection;
                chkAvailable.DataTextField = "SectionName";
                chkAvailable.DataValueField = "SectionID";
                chkAvailable.DataBind();

            }
            else
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("SectionID", typeof(int));
                dt.Columns.Add("UserSectionName", typeof(string));
                ChkSelected.DataSource = dt;
                ViewState["CheckListViewSelect"] = dt;
                ChkSelected.DataTextField = "UserSectionName";
                ChkSelected.DataValueField = "SectionID";
                ChkSelected.DataBind();
                ddlAccordingTo_SelectedIndexChanged(sender, e);
            }
            //ViewState["CheckListViewSelect"] = null;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnAddYes_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkAvailable.Items)
        {
            li.Selected = true;
        }
        btnAdd_Click(sender, e);

        pnlAddAll.Visible = false;
    }
    protected void btnAddNo_Click(object sender, EventArgs e)
    {
        pnlAddAll.Visible = false;

    }
    protected void btnRemoveYes_Click(object sender, EventArgs e)
    {
        foreach (ListItem lis in ChkSelected.Items)
        {
            lis.Selected = true;
        }
        btnRemove_Click(sender, e);
        pnlRemoveAll.Visible = false;

    }
    protected void btnRemoveNo_Click(object sender, EventArgs e)
    {
        pnlRemoveAll.Visible = false;
    }
}