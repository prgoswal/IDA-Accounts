using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BudgetMasters_frmBudgetSubSection : System.Web.UI.Page
{
    BudgetSubSectionModel objSubBudgetSection;

    DataTable VSSection
    {
        get { return (DataTable)ViewState["VSSection"]; }
        set { ViewState["VSSection"] = value; }

    }

    DataTable VSSubSection
    {
        get { return (DataTable)ViewState["VSSubSection"]; }
        set { ViewState["VSSubSection"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAll();
            ddlSection.Focus();
        }
    }

    private void BindAll()
    {
        try
        {
            objSubBudgetSection = new BudgetSubSectionModel();
            objSubBudgetSection.Ind = 11;
            objSubBudgetSection.OrgID = GlobalSession.OrgID;
            objSubBudgetSection.BrID = GlobalSession.BrID;
            string uri = string.Format("BudgetSubSection/BindAll");
            DataSet dsSubSection = CommonCls.ApiPostDataSet(uri, objSubBudgetSection);
            if (dsSubSection.Tables.Count > 0)
            {
                if (dsSubSection.Tables[1].Rows.Count > 0)
                {
                    grdSubSection.DataSource = dsSubSection.Tables[1];
                    grdSubSection.DataBind();
                    //pnlSectionGrid.Visible = true;
                    VSSubSection = dsSubSection.Tables[1];

                    lstSubSeaction.DataSource = dsSubSection.Tables[1];
                    lstSubSeaction.DataTextField = "SectionName";
                    lstSubSeaction.DataValueField = "SectionName";
                    lstSubSeaction.DataBind();
                }
                if (dsSubSection.Tables[0].Rows.Count > 0)
                {
                    ddlSection.DataSource = dsSubSection.Tables[0];
                    ddlSection.DataTextField = "SectionName";
                    ddlSection.DataValueField = "SectionID";
                    ddlSection.DataBind();
                    ddlSection.Items.Insert(0, new ListItem("-- Select --", "0"));
                    VSSection = dsSubSection.Tables[0];
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int index = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            hfSectionID.Value = ((Label)grdSubSection.Rows[index].FindControl("lblSectionID")).Text;
            txtSubSectionName.Text = ((Label)grdSubSection.Rows[index].FindControl("lblSubSectionName")).Text;
            txtSubSectionNameHindi.Text = ((Label)grdSubSection.Rows[index].FindControl("lblSubSectionNameHindi")).Text;
            ddlSection.SelectedValue = ((Label)grdSubSection.Rows[index].FindControl("lblParentSectionID")).Text;
            txtSchemeCode.Text = ((Label)grdSubSection.Rows[index].FindControl("lblIDACode")).Text;

            ddlSection_SelectedIndexChanged(sender, e);
            ddlSection.Enabled = false;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlSection.SelectedValue == "0")
            {
                ShowMessage("Select Group Name.", false);
                ddlSection.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSubSectionName.Text))
            {
                ShowMessage("Enter Cost Centre Name.", false);
                txtSubSectionName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSubSectionNameHindi.Text))
            {
                ShowMessage("Enter Cost Centre Name(Hindi).", false);
                txtSubSectionNameHindi.Focus();
                return;
            }

            objSubBudgetSection = new BudgetSubSectionModel();
            objSubBudgetSection.OrgID = GlobalSession.OrgID;
            objSubBudgetSection.BrID = GlobalSession.BrID;
            objSubBudgetSection.User = GlobalSession.UserID;
            objSubBudgetSection.IP = GlobalSession.IP;
            objSubBudgetSection.SubSectionName = (txtSubSectionName.Text).ToUpper().Trim();
            objSubBudgetSection.SubSectionNameHindi = (txtSubSectionNameHindi.Text).Trim();
            objSubBudgetSection.ParentSectionID = CommonCls.ConvertIntZero(ddlSection.SelectedValue);
            objSubBudgetSection.SchemeCode = (txtSchemeCode.Text).Trim();

            string uri;
            if (hfSectionID.Value == "0")
            {
                if (VSSubSection != null)
                {
                    bool IsValid = CheckSectionNameExisting();
                    if (!IsValid)
                    {
                        return;
                    }
                }
                objSubBudgetSection.Ind = 1;//For Saving Data
                uri = string.Format("BudgetSubSection/SaveBudgetSubSection");
            }
            else
            {
                objSubBudgetSection.Ind = 2;//For Update Data
                objSubBudgetSection.SectionID = CommonCls.ConvertIntZero(hfSectionID.Value);
                uri = string.Format("BudgetSubSection/UpdateBudgetSubSection");
            }

            DataTable dtSubSection = CommonCls.ApiPostDataTable(uri, objSubBudgetSection);
            if (dtSubSection.Rows.Count > 0)
            {
                ShowMessage("Record Save successfully.", true);
                hfSectionID.Value = "0";
                BindAll();
                ClearAll();
                ddlSection_SelectedIndexChanged(sender, e);
            }
            else
            {
                ShowMessage("Record not Save successfully.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private bool CheckSectionNameExisting()
    {
        try
        {
            string SectionName = (txtSubSectionName.Text).Trim();
            string SectionNameHindi = (txtSubSectionNameHindi.Text).Trim();

            DataTable dtGRD = VSSubSection;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);

                if (hfSectionID.Value == "0")
                {
                    dv31.RowFilter = "ParentSectionID  = " + ddlSection.SelectedValue + "";
                }
                else
                {
                    dv31.RowFilter = "SectionID Not = " + hfSectionID.Value + "";

                }
                if (dv31.Count > 0)
                {
                    for (int i = 0; i < dv31.ToTable().Rows.Count; i++)
                    {
                        if (SectionName == (dv31.ToTable().Rows[i]["SectionName"].ToString()))
                        {
                            ShowMessage("Cost Centre Name is already exits.", false);
                            txtSubSectionName.Focus();
                            return false;
                        }
                    }
                }
            }



            DataTable dtGRD2 = VSSubSection;
            if (dtGRD2.Rows.Count > 0)
            {
                DataView dv = new DataView(dtGRD);

                if (hfSectionID.Value == "0")
                {
                    dv.RowFilter = "ParentSectionID  = " + ddlSection.SelectedValue + "";
                }
                else
                {
                    dv.RowFilter = "SectionID Not = " + hfSectionID.Value + "";

                }


                //dv.RowFilter = "SectionID Not = " + hfSectionID.Value + "";
                if (dv.Count > 0)
                {
                    for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                    {
                        if (SectionNameHindi == (dv.ToTable().Rows[i]["SectionNameHindi"].ToString()))
                        {
                            ShowMessage("Cost Centre Name(Hindi) is already exits.", false);
                            txtSubSectionNameHindi.Focus();
                            return false;
                        }
                    }
                }
            }






            //for (int i = 0; i < VSSubSection.Rows.Count; i++)
            //{
            //    if (SectionName == (VSSubSection.Rows[i]["SectionName"].ToString()))
            //    {
            //        ShowMessage("Sub Section Name is already exits.", false);
            //        txtSubSectionName.Focus();
            //        return false;
            //    }
            //}

            //for (int j = 0; j < VSSubSection.Rows.Count; j++)
            //{
            //    if (SectionNameHindi == (VSSubSection.Rows[j]["SectionNameHindi"].ToString()))
            //    {
            //        ShowMessage("Sub Section Name(Hindi) is already exits.", false);
            //        txtSubSectionNameHindi.Focus();
            //        return false;
            //    }
            //}

            return true;

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }

    private void ClearAll()
    {
        txtSubSectionName.Text = txtSubSectionNameHindi.Text = lblSectionNameHindi.Text = txtSchemeCode.Text = "";
        // ddlSection.ClearSelection();
        txtSubSectionName.Focus();
        hfSectionID.Value = "0";
        ddlSection.Enabled = true;
        ddlSection.Focus();
        pnlSectionGrid.Visible = false;
        ddlSection.ClearSelection();
        BindAll();

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearAll();
        lblMsg.Text = lblMsg.CssClass = "";
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int SectionNameHindiid = CommonCls.ConvertIntZero(ddlSection.SelectedValue);
            for (int i = 0; i < VSSection.Rows.Count; i++)
            {
                if (SectionNameHindiid == CommonCls.ConvertIntZero(VSSection.Rows[i]["SectionID"].ToString()))
                {
                    lblSectionNameHindi.Text = VSSection.Rows[i]["SectionNameHindi"].ToString();
                }
            }
            if (ddlSection.SelectedValue == "0")
            {
                // lblMsg.Text = lblMsg.CssClass = "";
                lblSectionNameHindi.Text = "";
                pnlSectionGrid.Visible = false;
                lstSubSeaction.Visible = false;
                return;
            }

            // DataTable dtGrd = createSchema();
            //  for (int j = 0; j < VSSubSection.Rows.Count; j++)
            //{
            //    if (ddlSection.SelectedValue == VSSubSection.Rows[j]["ParentSectionID"].ToString())
            //    {
            //        DataRow dr = dtGrd.NewRow();
            //        dr["SectionID"] = VSSubSection.Rows[j]["SectionID"].ToString();
            //        dr["SectionName"] = VSSubSection.Rows[j]["SectionName"].ToString();
            //        dr["SectionNameHindi"] = VSSubSection.Rows[j]["SectionNameHindi"].ToString();
            //        dr["ParentSectionID"] = VSSubSection.Rows[j]["ParentSectionID"].ToString();
            //        dtGrd.Rows.Add(dr);
            //    }

            //}
            //grdSubSection.DataSource = dtGrd;
            //grdSubSection.DataBind();

            if (VSSubSection != null)
            {

                DataTable dtGRD = VSSubSection;
                if (dtGRD.Rows.Count > 0)
                {
                    DataView dv31 = new DataView(dtGRD);
                    dv31.RowFilter = "ParentSectionID = " + ddlSection.SelectedValue + "";
                    if (dv31.Count > 0)
                    {
                        lblMsg.Text = lblMsg.CssClass = "";
                        grdSubSection.DataSource = dv31.ToTable();
                        grdSubSection.DataBind();
                        pnlSectionGrid.Visible = true;

                        lstSubSeaction.Visible = true;
                        lstSubSeaction.DataSource = dv31.ToTable();
                        lstSubSeaction.DataTextField = "SectionName";
                        lstSubSeaction.DataValueField = "SectionName";
                        lstSubSeaction.DataBind();


                    }
                    else
                    {
                        ShowMessage("There is no Cost Centre of this Group.", false);
                        pnlSectionGrid.Visible = false;
                        lstSubSeaction.Visible = false;
                    }
                }
                else
                {
                    ShowMessage("There is no Cost Centre of this Group.", false);
                    pnlSectionGrid.Visible = false;
                    lstSubSeaction.Visible = false;
                }
            }
            else
            {
                ShowMessage("There is no Cost Centre of this Group.", false);
                pnlSectionGrid.Visible = false;
                lstSubSeaction.Visible = false;
            }

            ddlSection.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //private DataTable createSchema()
    //{
    //    DataTable dt = new DataTable();

    //    dt.Columns.Add("SectionID", typeof(int));
    //    dt.Columns.Add("SectionName", typeof(string));
    //    dt.Columns.Add("SectionNameHindi", typeof(string));
    //    dt.Columns.Add("ParentSectionID", typeof(int));
    //    return dt;
    //}
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    int index = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
        //    objSubBudgetSection.SectionID = CommonCls.ConvertIntZero(((Label)grdSubSection.Rows[index].FindControl("lblSectionID")).Text);
        //    objSubBudgetSection.Ind = 3;//For Delete
        //    objSubBudgetSection.OrgID = GlobalSession.OrgID;
        //    objSubBudgetSection.BrID = GlobalSession.BrID;
        //    string uri = string.Format("BudgetSubSection/DeleteBudgetSubSection");
        //    DataTable dtSection = CommonCls.ApiPostDataTable(uri, objSubBudgetSection);
        //    if (dtSection.Rows.Count > 0)
        //    {
        //        ShowMessage("Record Delete successfully.", true);
        //        BindAll();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ShowMessage(ex.Message, false);
        //}
    }
}