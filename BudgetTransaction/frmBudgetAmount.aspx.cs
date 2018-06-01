using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BudgetTransaction_frmBudgetAmount : System.Web.UI.Page
{

    #region Decleration
    DataTable VSSubSection
    {
        get { return (DataTable)ViewState["VSSubSection"]; }
        set { ViewState["VSSubSection"] = value; }
    }

    DataTable VSSSearchubSection
    {
        get { return (DataTable)ViewState["VSSSearchubSection"]; }
        set { ViewState["VSSSearchubSection"] = value; }
    }

    DataTable VSSection
    {
        get { return (DataTable)ViewState["VSSection"]; }
        set { ViewState["VSSection"] = value; }
    }
    int SectionID
    {
        get { return (int)ViewState["SectionID"]; }
        set { ViewState["SectionID"] = value; }
    }

    string SectionName
    {
        get { return (string)ViewState["SectionName"]; }
        set { ViewState["SectionName"] = value; }
    }
    string SubSectionName
    {
        get { return (string)ViewState["SubSectionName"]; }
        set { ViewState["SubSectionName"] = value; }
    }



    int SubSectionID
    {
        get { return (int)ViewState["SubSectionID"]; }
        set { ViewState["SubSectionID"] = value; }
    }

    int BudgetHeadID
    {
        get { return (int)ViewState["BudgetHeadID"]; }
        set { ViewState["BudgetHeadID"] = value; }
    }

    DataTable VSBudgetHead
    {
        get { return (DataTable)ViewState["VSBudgetHead"]; }
        set { ViewState["VSBudgetHead"] = value; }
    }
    BudgetAmountModel objBudgetAmountModel;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindAll();
                txtSectionName.Focus();


                //divListSection.Style.Add("display", "");

                int yrcd = GlobalSession.YrCD;
                int yrcdpluse = yrcd + 1;
                int yrcdremove = yrcd - 1;
                int val = yrcdremove - 1;
                lblActualAmt1617.Text = "Actual Amount 20" + yrcdremove + "- 20" + yrcd;
                lblSancBudgetAmt1617.Text = "Sanctioned Budget Amount 20" + yrcdremove + "- 20" + yrcd;

                lblPropBudgetAmt1617.Text = "Proposed Budget Amount  20" + yrcdremove + "- 20" + yrcd;
                lblActualAmt1516.Text = "Actual Amount 20" + val + "- 20" + yrcdremove;
                lblPropBudgetAmount1718.Text = "Proposed Budget Amount 20" + yrcd + "-20" + yrcdpluse;

                lblBudgetAmt.Text = GlobalSession.BudgetAmount;
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, false);
            }
        }
    }



    private void BindAll()
    {
        try
        {
            objBudgetAmountModel = new BudgetAmountModel();
            objBudgetAmountModel.Ind = 1;
            objBudgetAmountModel.OrgID = GlobalSession.OrgID;
            objBudgetAmountModel.BrID = GlobalSession.BrID;
            string uri = string.Format("BudgetAmount/BindAll");
            DataSet dsSubSection = CommonCls.ApiPostDataSet(uri, objBudgetAmountModel);

            if (dsSubSection.Tables.Count > 0)
            {
                VSSubSection = dsSubSection.Tables[0];
                DataTable dtGRD = dsSubSection.Tables[0];
                if (dtGRD.Rows.Count > 0)
                {
                    DataView dv31 = new DataView(dtGRD);
                    dv31.RowFilter = "ParentSectionID = 0";

                    ddlSection.DataSource = dv31.ToTable();
                    ddlSection.DataTextField = "SectionName";
                    ddlSection.DataValueField = "SectionID";
                    ddlSection.DataBind();
                    ddlSection.Items.Insert(0, new ListItem("-- Select --", "0"));
                    VSSection = dv31.ToTable();

                    lstSection.DataSource = VSSection;
                    lstSection.DataTextField = "SectionName";
                    lstSection.DataValueField = "SectionName";
                    lstSection.DataBind();

                }

                if (dsSubSection.Tables[1].Rows.Count > 0)
                {
                    ddlBudgetHead.DataSource = dsSubSection.Tables[1];
                    ddlBudgetHead.DataTextField = "AccName";
                    ddlBudgetHead.DataValueField = "AccCode";
                    ddlBudgetHead.DataBind();
                    ddlBudgetHead.Items.Insert(0, new ListItem("-- Select --", "0"));
                    VSBudgetHead = dsSubSection.Tables[1];


                    lstBudgetHead.DataSource = dsSubSection.Tables[1];
                    lstBudgetHead.DataTextField = "AccName";
                    lstBudgetHead.DataValueField = "AccName";
                    lstBudgetHead.DataBind();
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
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSubSectionNameHindi.Text = ""; lblMsg.Text = "";

        if (ddlSection.SelectedValue == "0")
        {
            lblSectionNameHindi.Text = "";
            ddlSubSection.ClearSelection();
            return;
        }
        DataTable dtGRD = VSSubSection;
        if (dtGRD.Rows.Count > 0)
        {
            DataView dv31 = new DataView(dtGRD);
            dv31.RowFilter = "ParentSectionID = " + ddlSection.SelectedValue + "";
            ddlSubSection.DataSource = dv31.ToTable();
            ddlSubSection.DataTextField = "SectionName";
            ddlSubSection.DataValueField = "SectionID";
            ddlSubSection.DataBind();
            ddlSubSection.Items.Insert(0, new ListItem("-- Select --", "0"));

            dv31.RowFilter = "SectionID = " + ddlSection.SelectedValue + "";
            lblSectionNameHindi.Text = dv31.ToTable().Rows[0]["SectionNameHindi"].ToString();
        }
        ddlSubSection.ClearSelection();
        ddlBudgetHead.ClearSelection();

    }
    protected void ddlSubSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubSection.SelectedValue == "0")
        {
            lblSubSectionNameHindi.Text = "";
            return;
        }
        DataTable dtGRD = VSSubSection;
        if (dtGRD.Rows.Count > 0)
        {
            DataView dv31 = new DataView(dtGRD);
            dv31.RowFilter = "SectionID = " + ddlSubSection.SelectedValue + "";
            lblSubSectionNameHindi.Text = dv31.ToTable().Rows[0]["SectionNameHindi"].ToString();
        }
    }
    protected void ddlBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBudgetHead.SelectedValue == "0")
            {
                return;
            }


            DataTable dtGRD = VSBudgetHead;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);
                dv31.RowFilter = "AccCode = " + ddlBudgetHead.SelectedValue + "";
                lblBudgetHead.Text = dv31.ToTable().Rows[0]["AccNameHindi"].ToString();
                if (dv31.Count > 0)
                {
                    dv31.ToTable();
                }


                if (dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "21" || dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "24")//For LIABILITY Account Head
                {
                    txtAcutal3CrAmt.Enabled = txtProposed2CrAmt.Enabled = txtSanctioned2CrAmt.Enabled = txtActual2CrAmt.Enabled = txtProposedCrAmt.Enabled = true;
                    lblAcutal3CrAmt.Visible = lblProposed2CrAmt.Visible = lblSanctioned2CrAmt.Visible = lblActual2CrAmt.Visible = lblProposedCrAmt.Visible = true;
                    txtAcutal3DrAmt.Enabled = txtProposed2DrAmt.Enabled = txtSanctioned2DrAmt.Enabled = txtActual2DrAmt.Enabled = txtProposedDrAmt.Enabled = false;
                    lblAcutal3DrAmt.Visible = lblProposed2DrAmt.Visible = lblSanctioned2DrAmt.Visible = lblActual2DrAmt.Visible = lblProposedDrAmt.Visible = false;
                }
                else if (dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "23" || dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "25")
                {
                    txtAcutal3CrAmt.Enabled = txtProposed2CrAmt.Enabled = txtSanctioned2CrAmt.Enabled = txtActual2CrAmt.Enabled = txtProposedCrAmt.Enabled = false;
                    txtAcutal3DrAmt.Enabled = txtProposed2DrAmt.Enabled = txtSanctioned2DrAmt.Enabled = txtActual2DrAmt.Enabled = txtProposedDrAmt.Enabled = true;
                    lblAcutal3DrAmt.Visible = lblProposed2DrAmt.Visible = lblSanctioned2DrAmt.Visible = lblActual2DrAmt.Visible = lblProposedDrAmt.Visible = true;
                    lblAcutal3CrAmt.Visible = lblProposed2CrAmt.Visible = lblSanctioned2CrAmt.Visible = lblActual2CrAmt.Visible = lblProposedCrAmt.Visible = false;

                }
            }

            CheckBudgetAmountIsExist();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void CheckBudgetAmountIsExist()
    {
        try
        {
            if (ddlSection.SelectedValue == "0")
            {
                ShowMessage("Select Section Name.", false);
                ddlSection.Focus();
                ddlBudgetHead.ClearSelection();
                return;
            }

            if (ddlSubSection.SelectedValue == "0")
            {
                ShowMessage("Select Sub-Section Name.", false);
                ddlSubSection.Focus();
                ddlBudgetHead.ClearSelection();
                return;
            }


            objBudgetAmountModel = new BudgetAmountModel();
            objBudgetAmountModel.Ind = 3;
            objBudgetAmountModel.OrgID = GlobalSession.OrgID;
            objBudgetAmountModel.BrID = GlobalSession.BrID;
            objBudgetAmountModel.YrCode = GlobalSession.YrCD;
            //objBudgetAmountModel.SectionCD = CommonCls.ConvertIntZero(ddlSection.SelectedValue);
            //objBudgetAmountModel.SubSectionCD = CommonCls.ConvertIntZero(ddlSubSection.SelectedValue);
            //objBudgetAmountModel.BudgetHeadCD = CommonCls.ConvertIntZero(ddlBudgetHead.SelectedValue);
            objBudgetAmountModel.SectionCD = CommonCls.ConvertIntZero(SectionID);
            objBudgetAmountModel.SubSectionCD = CommonCls.ConvertIntZero(SubSectionID);
            objBudgetAmountModel.BudgetHeadCD = CommonCls.ConvertIntZero(BudgetHeadID);
            string uri = string.Format("BudgetAmount/CheckBudgetAmount");
            DataTable dtSubSection = CommonCls.ApiPostDataTable(uri, objBudgetAmountModel);

            if (dtSubSection.Rows.Count > 0)
            {
                txtAcutal3DrAmt.Text = lblAcutal3DrAmt.Text = dtSubSection.Rows[0]["Actual3BudgetAmtDr"].ToString();
                txtAcutal3CrAmt.Text = lblAcutal3CrAmt.Text = dtSubSection.Rows[0]["Actual3BudgetAmtCr"].ToString();
                txtProposed2DrAmt.Text = lblProposed2DrAmt.Text = dtSubSection.Rows[0]["Prop2BudgetAmtDr"].ToString();
                txtProposed2CrAmt.Text = lblProposed2CrAmt.Text = dtSubSection.Rows[0]["Prop2BudgetAmtCr"].ToString();
                txtSanctioned2DrAmt.Text = lblSanctioned2DrAmt.Text = dtSubSection.Rows[0]["Sanc2BudgetAmtDr"].ToString();
                txtSanctioned2CrAmt.Text = lblSanctioned2CrAmt.Text = dtSubSection.Rows[0]["Sanc2BudgetAmtCr"].ToString();
                txtActual2DrAmt.Text = lblActual2DrAmt.Text = dtSubSection.Rows[0]["Actual2BudgetAmtDr"].ToString();
                txtActual2CrAmt.Text = lblActual2CrAmt.Text = dtSubSection.Rows[0]["Actual2BudgetAmtCr"].ToString();
                txtProposedDrAmt.Text = lblProposedDrAmt.Text = dtSubSection.Rows[0]["PropBudgetAmtDr"].ToString();
                txtProposedCrAmt.Text = lblProposedCrAmt.Text = dtSubSection.Rows[0]["PropBudgetAmtCr"].ToString();

            }
            else
            {
                ClearFields();

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void ClearFields()
    {
        txtAcutal3DrAmt.Text = lblAcutal3DrAmt.Text =
 txtAcutal3CrAmt.Text = lblAcutal3CrAmt.Text =
 txtProposed2DrAmt.Text = lblProposed2DrAmt.Text =
 txtProposed2CrAmt.Text = lblProposed2CrAmt.Text =
 txtSanctioned2DrAmt.Text = lblSanctioned2DrAmt.Text =
 txtSanctioned2CrAmt.Text = lblSanctioned2CrAmt.Text =
 txtActual2DrAmt.Text = lblActual2DrAmt.Text =
 txtActual2CrAmt.Text = lblActual2CrAmt.Text =
 txtProposedDrAmt.Text = lblProposedDrAmt.Text =
 txtProposedCrAmt.Text = lblProposedCrAmt.Text = "";



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (string.IsNullOrEmpty(txtSectionName.Text))
            {
                ShowMessage("Select Section Name.", false);
                txtSectionName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSubSection.Text))
            {
                ShowMessage("Select Sub-Section Name.", false);
                txtSubSection.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtBudgetHead.Text))
            {
                ShowMessage("Select Budget Head Name.", false);
                txtBudgetHead.Focus();
                return;
            }
            //if (txtProposedDrAmt.Enabled == true)
            //{
            //    if (string.IsNullOrEmpty(txtProposedDrAmt.Text) || Convert.ToDecimal(txtProposedDrAmt.Text) <= 0)
            //    {
            //        ShowMessage("Enter Proposed Budget Amount 2017-2018.", false);
            //        txtProposedDrAmt.Focus();
            //        return;
            //    }
            //}




            //if (txtProposedCrAmt.Enabled == true)
            //{

            //    if (string.IsNullOrEmpty(txtProposedCrAmt.Text) || Convert.ToDecimal(txtProposedCrAmt.Text) <= 0)
            //    {
            //        ShowMessage("Enter Proposed Budget Amount 2017-2018.", false);
            //        txtProposedCrAmt.Focus();
            //        return;
            //    }

            //}

            objBudgetAmountModel = new BudgetAmountModel();
            objBudgetAmountModel.Ind = 2;
            objBudgetAmountModel.OrgID = GlobalSession.OrgID;
            objBudgetAmountModel.BrID = GlobalSession.BrID;
            objBudgetAmountModel.YrCode = GlobalSession.YrCD;
            //objBudgetAmountModel.SectionCD = CommonCls.ConvertIntZero(ddlSection.SelectedValue);
            //objBudgetAmountModel.SubSectionCD = CommonCls.ConvertIntZero(ddlSubSection.SelectedValue);
            objBudgetAmountModel.SectionCD = CommonCls.ConvertIntZero(SectionID);
            objBudgetAmountModel.SubSectionCD = CommonCls.ConvertIntZero(SubSectionID);
            //objBudgetAmountModel.BudgetHeadCD = CommonCls.ConvertIntZero(ddlBudgetHead.SelectedValue);

            objBudgetAmountModel.BudgetHeadCD = CommonCls.ConvertIntZero(BudgetHeadID);

            objBudgetAmountModel.UserID = GlobalSession.UserID;
            objBudgetAmountModel.IP = "";

            objBudgetAmountModel.Actual3budgetAmtDr = CommonCls.ConvertDecimalZero(txtAcutal3DrAmt.Text);
            objBudgetAmountModel.Actual3budgetAmtCr = CommonCls.ConvertDecimalZero(txtAcutal3CrAmt.Text);
            objBudgetAmountModel.Prop2BudgetAmtDr = CommonCls.ConvertDecimalZero(txtProposed2DrAmt.Text);
            objBudgetAmountModel.Prop2BudgetAmtCr = CommonCls.ConvertDecimalZero(txtProposed2CrAmt.Text);
            objBudgetAmountModel.Sanc2BudgetAmtDr = CommonCls.ConvertDecimalZero(txtSanctioned2DrAmt.Text);
            objBudgetAmountModel.Sanc2BudgetAmtCr = CommonCls.ConvertDecimalZero(txtSanctioned2CrAmt.Text);
            objBudgetAmountModel.Actual2budgetAmtDr = CommonCls.ConvertDecimalZero(txtActual2DrAmt.Text);
            objBudgetAmountModel.Actual2budgetAmtcr = CommonCls.ConvertDecimalZero(txtActual2CrAmt.Text);
            objBudgetAmountModel.PropBudgetAmtDr = CommonCls.ConvertDecimalZero(txtProposedDrAmt.Text);
            objBudgetAmountModel.PropBudgetAmtCr = CommonCls.ConvertDecimalZero(txtProposedCrAmt.Text);

            string uri = string.Format("BudgetAmount/SaveBudget");

            DataTable dtOpeningBalance = CommonCls.ApiPostDataTable(uri, objBudgetAmountModel);
            if (dtOpeningBalance.Rows.Count > 0)
            {
                ShowMessage("Record Save successfully.", true);
                ClearAll();
            }
            else
            {
                ShowMessage("Record Not Save successfully.", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearAll();
        lblMsg.Text = lblMsg.CssClass = "";
        ClearFields();
        BindAll();
    }

    private void ClearAll()
    {
        ddlSection.ClearSelection();
        ddlSubSection.ClearSelection();
        ddlBudgetHead.ClearSelection();
        lblSectionNameHindi.Text = lblSubSectionNameHindi.Text = lblBudgetHead.Text = "";
        txtAcutal3CrAmt.Text = txtProposed2CrAmt.Text = txtSanctioned2CrAmt.Text = txtActual2CrAmt.Text = txtProposedCrAmt.Text = "";
        txtAcutal3DrAmt.Text = txtProposed2DrAmt.Text = txtSanctioned2DrAmt.Text = txtActual2DrAmt.Text = txtProposedDrAmt.Text = "";
        ddlSection.Focus();
        txtAcutal3DrAmt.Enabled =
        txtAcutal3CrAmt.Enabled =
        txtProposed2DrAmt.Enabled =
        txtProposed2CrAmt.Enabled = txtSanctioned2DrAmt.Enabled =
        txtSanctioned2CrAmt.Enabled =
        txtActual2DrAmt.Enabled =
        txtActual2CrAmt.Enabled =
        txtProposedDrAmt.Enabled =
        txtProposedCrAmt.Enabled = false;

        SectionID = SubSectionID = BudgetHeadID = 0;
        txtSectionName.Text = txtSubSection.Text = txtBudgetHead.Text = "";

        //divListSection.Style.Add("display", "");
        //divListSubSection.Style.Add("display", "none");
        //divListBudgetHead.Style.Add("display", "none");


        divListSubSection.Visible = false;
        divListSection.Visible = true;
        divListBudgetHead.Visible = false;
    }

    protected void ddlSection_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtGRD = VSSection;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);

                dv31.RowFilter = "SectionName LIKE '%" + ddlSection.SelectedItem.Text + "%'";
                ddlSection.DataSource = dv31.ToTable();
                ddlSection.DataTextField = "SectionName";
                ddlSection.DataValueField = "SectionID";
                ddlSection.DataBind();


            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    //protected void txtSection_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dtGRD = VSSection;
    //        if (dtGRD.Rows.Count > 0)
    //        {
    //            DataView dv31 = new DataView(dtGRD);
    //            //DataTable dt = ds.Tables[0].Select("Name LIKE 'Rob%'").CopyToDataTable();

    //            //DataTable dt = (dtGRD.Rows[0]["SectionName"]).ToString().Select("Name LIKE 'Rob%'").CopyToDataTable();

    //            dv31.RowFilter = "SectionName LIKE '" + txtSection.Text + "%'";
    //            ddlSection.DataSource = dv31.ToTable();
    //            ddlSection.DataTextField = "SectionName";
    //            ddlSection.DataValueField = "SectionID";
    //            ddlSection.DataBind();

    //            //DataRow[] filteredRows = tb.Select("CREATOR LIKE '%" + searchstring + "%'");
    //            //DataRow[] foundRows = tb.Select("FIRSTNAME,LASTNAME,NAME,COMPANY,TIMEFROM,TIMETO,CREATOR Like '%" + searchstring + "%'");

    //            //tb = foundRows.CopyToDataTable();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}




    protected void lstSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtSectionName_TextChanged(sender, e);

        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message, false);
        }

    }

    private bool FillSubSection()
    {
        try
        {

            if (!string.IsNullOrEmpty(txtSubSection.Text))
            {
                txtSubSection.Text = "";
                lblSubSectionNameHindi.Text = "";
            }
            string ReplaceSectionName = string.Empty;

            if (lstSection.SelectedValue == "")
            {
                SectionName = (txtSectionName.Text).ToUpper().Trim();
            }
            else
            {
                SectionName = (lstSection.SelectedValue).ToUpper().Trim();
            }
            ReplaceSectionName = SectionName.Replace("%", "_");//replacing string by another character
            //txtSectionName.Text = SectionName;
            DataTable dtGRD = VSSection;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);

                for (int i = 0; i <= dtGRD.Rows.Count - 1; i++)
                {
                    dtGRD.Rows[i]["SectionName"] = dtGRD.Rows[i]["SectionName"].ToString().Replace("%", "_").Trim();
                }

                DataView ReplaceSectionTable = dv31;

                ReplaceSectionTable.RowFilter = "SectionName LIKE '" + ReplaceSectionName + "%'";
                lblSectionNameHindi.Text = ReplaceSectionTable.ToTable().Rows[0]["SectionNameHindi"].ToString();
                SectionID = CommonCls.ConvertIntZero(ReplaceSectionTable.ToTable().Rows[0]["SectionId"].ToString());
                txtSectionName.Text = ReplaceSectionTable.ToTable().Rows[0]["SectionName"].ToString();
            }
            txtSubSection.Focus();


            //divListSubSection.Style.Add("display", "");
            //divListBudgetHead.Style.Add("display", "none");
            //divListSection.Style.Add("display", "none");

            divListSection.Visible = false;
            divListBudgetHead.Visible = false;
            divListSubSection.Visible = true;
            return true;

        }
        catch (Exception ex)
        {
            ShowMessage("This Section name is incorrect", false);
            return false;

        }
    }
    protected void txtSectionName_TextChanged(object sender, EventArgs e)
    {
        bool Status = FillSubSection();
        if (!Status)
        {
            return;
        }

        //lblSubSectionNameHindi.Text = ""; lblMsg.Text = "";


        DataTable dtGRD = VSSubSection;
        if (dtGRD.Rows.Count > 0)
        {
            DataView dv31 = new DataView(dtGRD);
            dv31.RowFilter = "ParentSectionID = " + SectionID + "";

            lstSubSection.DataSource = dv31.ToTable();
            lstSubSection.DataTextField = "SectionName";
            lstSubSection.DataValueField = "SectionName";
            lstSubSection.DataBind();


            VSSSearchubSection = dv31.ToTable();

            //dv31.RowFilter = "SectionID = " + ddlSection.SelectedValue + "";
            //lblSectionNameHindi.Text = dv31.ToTable().Rows[0]["SectionNameHindi"].ToString();
        }
        //ddlSubSection.ClearSelection();
        //ddlBudgetHead.ClearSelection();
        lstSection.ClearSelection();
        SectionName = "";
    }



    protected void lstSubSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        //GetSubSectionID();
        bool Status = GetSubSectionID();
        if (!Status)
        {
            return;
        }

    }

    private bool GetSubSectionID()
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

            string ReplaceSubSectionName = string.Empty;
            if (lstSubSection.SelectedValue == "")
            {
                SubSectionName = (txtSubSection.Text).ToUpper().Trim();
            }
            else
            {
                SubSectionName = (lstSubSection.SelectedValue).ToUpper().Trim();
            }
            ReplaceSubSectionName = SubSectionName.Replace("%", "_");//replacing string by another character
            //txtSubSection.Text = SubSectionName;
            DataTable dtGRD = VSSSearchubSection;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);

                for (int i = 0; i <= dtGRD.Rows.Count - 1; i++)
                {
                    dtGRD.Rows[i]["SectionName"] = dtGRD.Rows[i]["SectionName"].ToString().Replace("%", "_").Trim();
                }

                DataView ReplaceSubSectionTable = dv31;

                ReplaceSubSectionTable.RowFilter = "SectionName LIKE '" + ReplaceSubSectionName + "%'";
                lblSubSectionNameHindi.Text = ReplaceSubSectionTable.ToTable().Rows[0]["SectionNameHindi"].ToString();
                SubSectionID = CommonCls.ConvertIntZero(ReplaceSubSectionTable.ToTable().Rows[0]["SectionId"].ToString());
                txtSubSection.Text = ReplaceSubSectionTable.ToTable().Rows[0]["SectionName"].ToString().Replace("_", "%");
                //txtSubSection.Text = ReplaceBudgetHeadTable.ToTable().Rows[0]["SectionName"].ToString();
            }
            txtBudgetHead.Focus();

            //divListBudgetHead.Style.Add("display", "");
            //divListSection.Style.Add("display", "none");
            //divListSubSection.Style.Add("display", "none");


            divListBudgetHead.Visible = true;
            divListSection.Visible = false;
            divListSubSection.Visible = false;
            lstSubSection.ClearSelection();

            SubSectionName = "";
            return true;

        }
        catch (Exception ex)
        {
            ShowMessage("This Sub-Section name is incorrect", false);
            return false;
        }
    }
    protected void txtSubSection_TextChanged(object sender, EventArgs e)
    {
        bool Status = GetSubSectionID();
        if (!Status)
        {
            return;
        }
    }
    protected void lstBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool Status = GetBudgetHeadID();
        if (!Status)
        {
            return;
        }
    }

    private bool GetBudgetHeadID()
    {

        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

            string BudgetHeadName = string.Empty;
            string ReplaceBudgetHeadName = string.Empty;
            if (lstBudgetHead.SelectedValue == "")
            {
                BudgetHeadName = (txtBudgetHead.Text).ToUpper().Trim();
            }
            else
            {
                BudgetHeadName = (lstBudgetHead.SelectedValue).ToUpper().Trim();
            }
            ReplaceBudgetHeadName = BudgetHeadName.Replace("%", "_");

            //txtSubSection.Text = SubSectionName;
            DataTable dtGRD = VSBudgetHead;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);

                #region
                // for Replacing String Loop Start

                for (int i = 0; i <= dtGRD.Rows.Count - 1; i++)
                {
                    dtGRD.Rows[i]["AccName"] = dtGRD.Rows[i]["AccName"].ToString().Replace("%", "_").Trim();
                }
                DataView ReplaceBudgetHeadTable = dv31;



                #endregion
                //dv31.RowFilter = "AccName LIKE '" + BudgetHeadName + "%'";
                ReplaceBudgetHeadTable.RowFilter = "AccName LIKE '" + ReplaceBudgetHeadName + "%'";

                lblBudgetHead.Text = ReplaceBudgetHeadTable.ToTable().Rows[0]["AccNameHindi"].ToString();
                BudgetHeadID = CommonCls.ConvertIntZero(ReplaceBudgetHeadTable.ToTable().Rows[0]["AccCode"].ToString());
                txtBudgetHead.Text = ReplaceBudgetHeadTable.ToTable().Rows[0]["AccName"].ToString().Replace("_", "%");
                //txtBudgetHead.Text = ReplaceBudgetHeadTable.ToTable().Rows[0]["AccName"].ToString();


                if (dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "21" || dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "24")//For LIABILITY Account Head
                {
                    txtAcutal3CrAmt.Enabled = txtProposed2CrAmt.Enabled = txtSanctioned2CrAmt.Enabled = txtActual2CrAmt.Enabled = txtProposedCrAmt.Enabled = true;
                    lblAcutal3CrAmt.Visible = lblProposed2CrAmt.Visible = lblSanctioned2CrAmt.Visible = lblActual2CrAmt.Visible = lblProposedCrAmt.Visible = true;
                    txtAcutal3DrAmt.Enabled = txtProposed2DrAmt.Enabled = txtSanctioned2DrAmt.Enabled = txtActual2DrAmt.Enabled = txtProposedDrAmt.Enabled = false;
                    lblAcutal3DrAmt.Visible = lblProposed2DrAmt.Visible = lblSanctioned2DrAmt.Visible = lblActual2DrAmt.Visible = lblProposedDrAmt.Visible = false;
                }
                else if (dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "23" || dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "25")
                {
                    txtAcutal3CrAmt.Enabled = txtProposed2CrAmt.Enabled = txtSanctioned2CrAmt.Enabled = txtActual2CrAmt.Enabled = txtProposedCrAmt.Enabled = false;
                    txtAcutal3DrAmt.Enabled = txtProposed2DrAmt.Enabled = txtSanctioned2DrAmt.Enabled = txtActual2DrAmt.Enabled = txtProposedDrAmt.Enabled = true;
                    lblAcutal3DrAmt.Visible = lblProposed2DrAmt.Visible = lblSanctioned2DrAmt.Visible = lblActual2DrAmt.Visible = lblProposedDrAmt.Visible = true;
                    lblAcutal3CrAmt.Visible = lblProposed2CrAmt.Visible = lblSanctioned2CrAmt.Visible = lblActual2CrAmt.Visible = lblProposedCrAmt.Visible = false;

                }
            }

            CheckBudgetAmountIsExist();



            txtBudgetHead.Focus();
            return true;
        }
        catch (Exception ex)
        {
            ShowMessage("This Budget Head is incorrect", false);
            return false;
        }
    }
    protected void txtBudgetHead_TextChanged(object sender, EventArgs e)
    {
        bool Status = GetBudgetHeadID();
        if (!Status)
        {
            return;
        }
    }
}