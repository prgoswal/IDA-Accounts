using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMasters_frmSundries : System.Web.UI.Page
{
    SundriesModel objSundriModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";

        if (!IsPostBack)
        {
            Loadchklist();
            //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            //{
            //    btnSave.Visible = true;
            //    Session["MasterWrite"] = 0;
            //}
        }
        ChkSelected.Attributes.Add("onclick", "radioMe(event);");
    }

    private void Loadchklist()
    {
        try
        {
            objSundriModel = new SundriesModel();
            objSundriModel.OrgID = GlobalSession.OrgID;
            objSundriModel.YrCD = GlobalSession.YrCD;

            string uri = string.Format(" Sundri/ChkList");
            DataSet dsChklist = CommonCls.ApiPostDataSet(uri, objSundriModel);
            if (dsChklist.Tables.Count > 0)
            {

                DataTable dtChkAviable = dsChklist.Tables["AvailableList"];
                DataTable dtChkAllocated = dsChklist.Tables["AllocatedList"];

                ViewState["dtCheckListAvailable"] = dtChkAviable;
                chkAvailable.DataSource = dtChkAviable;
                chkAvailable.DataTextField = "AccName";
                chkAvailable.DataValueField = "AccCode";
                chkAvailable.DataBind();

                ChkSelected.DataSource = dtChkAllocated;
                ChkSelected.DataTextField = "SundriHeadName";
                ChkSelected.DataValueField = "AccCode";
                ChkSelected.DataBind();
                if (dtChkAllocated.Columns.Count > 0)
                {
                    ViewState["CheckListViewSelect"] = dtChkAllocated;
                }else
                {
                   
                }
                //chkAvailable.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)       //buttaon Add For The List
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

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
                ShowMessage("Please Check First In Available List!", false);
                return;
            }

            DataTable dtCheckList = new DataTable();
            DataTable dtCheckListAvailable = new DataTable();
            if (ViewState["CheckListViewSelect"] == null)
            {

                dtCheckList.Columns.Add("SundriHeadName");
                dtCheckList.Columns.Add("AccCode");

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
            int i = 0;
            dtCheckListAvailable = (DataTable)ViewState["dtCheckListAvailable"];
            foreach (ListItem item in chkAvailable.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = dtCheckList.NewRow();
                    //dr["AccName"] = item.Text;
                    dr["SundriHeadName"] = item.Text;
                    dr["AccCode"] = item.Value.ToString();
                    dtCheckList.Rows.Add(dr);

                    dtCheckListAvailable.Rows.RemoveAt(i);

                    ViewState["dtCheckListAvailable"] = dtCheckListAvailable;
                }
                else { i++; }

            }
            ViewState["CheckListViewSelect"] = dtCheckList;
            ChkSelected.DataSource = dtCheckList;
            ChkSelected.DataValueField = "AccCode";
            ChkSelected.DataTextField = "SundriHeadName";
            ChkSelected.DataBind();

            chkAvailable.DataSource = dtCheckListAvailable;
            chkAvailable.DataTextField = "AccName";
            chkAvailable.DataValueField = "AccCode";
            chkAvailable.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAddAl_Click(object sender, EventArgs e) //button AddAll 
    {
        foreach (ListItem li in chkAvailable.Items)
        {
            li.Selected = true;
        }
        btnAdd_Click(sender, e);
    }
    protected void btnRemove_Click(object sender, EventArgs e) //button Remove For The List  
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

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
                    if (MatchInd(item.Value.ToString()) == 0)
                    {
                        DataRow dr = dtCheckListAvailable.NewRow();

                        dr["AccName"] = item.Text;
                        dr["AccCode"] = item.Value.ToString();

                        dtCheckListAvailable.Rows.Add(dr);

                        dtCheckList.Rows.RemoveAt(i);

                        ViewState["dtCheckListAvailable"] = dtCheckListAvailable;
                        ShowMessage("Item Removed Successfully.", true);
                    }
                    else
                    {
                        i++;
                        ShowMessage("Item Not Removed Because Invoice Created Using That.", false);
                        break;
                    }
                }
                else { i++; }
            }

            ViewState["CheckListViewSelect"] = dtCheckList;
            ChkSelected.DataSource = dtCheckList;
            ChkSelected.DataValueField = "AccCode";
            ChkSelected.DataTextField = "SundriHeadName";
            ChkSelected.DataBind();

            chkAvailable.DataSource = dtCheckListAvailable;
            chkAvailable.DataTextField = "AccName";
            chkAvailable.DataValueField = "AccCode";
            chkAvailable.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public int MatchInd(string SelectedValue)
    {

        objSundriModel = new SundriesModel();
        objSundriModel.Ind = 2;
        objSundriModel.OrgID = GlobalSession.OrgID;
        objSundriModel.SundriCode = int.Parse(SelectedValue);

        string uri = string.Format("Sundri/MatchInd");
        DataTable dsMatchInd = CommonCls.ApiPostDataTable(uri, objSundriModel);

        return int.Parse(dsMatchInd.Rows[0]["InvoiceCnt"].ToString());

    }

    protected void btnRemvAl_Click(object sender, EventArgs e)
    {
        foreach (ListItem lis in ChkSelected.Items)
        {
            lis.Selected = true;
        }
        btnRemove_Click(sender, e);
    }

    DataTable dtForSundriesSchema()     //Create DataTable..
    {
        DataTable dtForSundries = new DataTable();
        dtForSundries.Columns.Add("CompanyID", typeof(int));
        dtForSundries.Columns.Add("AccCode", typeof(int));
        dtForSundries.Columns.Add("AccName", typeof(string));
        dtForSundries.Columns.Add("Ind1", typeof(int));
        dtForSundries.Columns.Add("Ind2", typeof(int));
        dtForSundries.Columns.Add("Remark1", typeof(string));
        dtForSundries.Columns.Add("Remark2", typeof(string));
        dtForSundries.Columns.Add("UserID", typeof(int));
        dtForSundries.Columns.Add("IPAddr", typeof(string));
        return dtForSundries;
    }

    DataTable CreateAccountHeadData()
    {
        DataTable dtForSundries = new DataTable();
        try
        {
            dtForSundries = dtForSundriesSchema(); //New DataTable

            foreach (ListItem item in ChkSelected.Items)
            {
                DataRow drForSundires = dtForSundries.NewRow();

                drForSundires["CompanyID"] = GlobalSession.OrgID;
                drForSundires["AccCode"] = Convert.ToInt32(item.Value);
                drForSundires["AccName"] = item.Text;
                drForSundires["Ind1"] = 0;
                drForSundires["Ind2"] = 0;
                drForSundires["Remark1"] = "";
                drForSundires["Remark2"] = "";
                drForSundires["UserID"] = GlobalSession.UserID;
                drForSundires["IPAddr"] = GlobalSession.IP;
                dtForSundries.Rows.Add(drForSundires);
            }
        }
        catch (Exception)
        {

        }
        return dtForSundries;
    }

    protected void btnSave_Click(object sender, EventArgs e) //button Save 
    {
        lblMsg.Text = lblMsg.CssClass = "";

        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        objSundriModel = new SundriesModel();
        objSundriModel.Ind = 3;
        objSundriModel.OrgID = GlobalSession.OrgID;
        objSundriModel.TblSundries = CreateAccountHeadData();

        string uri = string.Format("Sundri/SaveChkAllocated");
        DataTable dtSaveItem = CommonCls.ApiPostDataTable(uri, objSundriModel);
        if (dtSaveItem.Rows.Count > 0)
        {
            ShowMessage("Data Save Successfully!", true);
        }
        else
        {
            ShowMessage("Data Not Save Successfully!", false);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmSundries.aspx");
    }

    public void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}