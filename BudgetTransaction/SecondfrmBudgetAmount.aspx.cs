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


    DataTable VSMainSection
    {
        get { return (DataTable)ViewState["VSMainSection"]; }
        set { ViewState["VSMainSection"] = value; }
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
    NewBudgetAmountModel objNewBudgetAmountModel;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                BindAll();
                BindAllocatedScheme();

                lblPropAmt1819.Text = "Proposed Budget Amount For 20" + GlobalSession.BudgetYrCD + "- 20" + (GlobalSession.BudgetYrCD + 1);
                lblPropQuaAmt.Text = "Proposed Budget Amount For Quarter  (January-March)";
                lblRevBudgetAmt1718.Text = "Revised Budget Amount 20" + (GlobalSession.BudgetYrCD - 1) + "- 20" + (GlobalSession.BudgetYrCD);
                lblActualUpto17.Text = "Actual Budget Amount Upto December 20" + (GlobalSession.BudgetYrCD - 1);
                lblPropBudgetAmount1718.Text = "Proposed Budget Amount 20" + (GlobalSession.BudgetYrCD - 1) + "-20" + (GlobalSession.BudgetYrCD);

                lblBudgetAmt.Text = GlobalSession.BudgetAmount;
                if (GlobalSession.IsAdmin != 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID > 0)
                {
                    txtSectionName_TextChanged(sender, e);

                }
                if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0)
                {
                    if (string.IsNullOrEmpty(txtSubSection.Text))
                    {
                        txtSectionName_TextChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, false);
            }
        }
    }

    private void BindAllocatedScheme()
    {
        objNewBudgetAmountModel = new NewBudgetAmountModel();
        objNewBudgetAmountModel.Ind = 12;
        string uri = string.Format("NewBudgetAmount/BindAllocatedScheme");
        DataTable dtScheme = CommonCls.ApiPostDataTable(uri, objNewBudgetAmountModel);
        if (dtScheme.Rows.Count > 0)
        {
            DataView dv31 = new DataView(dtScheme);
            dv31.RowFilter = "DepartmentName Not = 'Not Allocated' And SubDepartmentName Not = 'Not Allocated'";
            grdScheme.DataSource = dv31.ToTable();
            grdScheme.DataBind();
        }
    }

    private void BindAll()
    {
        try
        {
            objNewBudgetAmountModel = new NewBudgetAmountModel();
            objNewBudgetAmountModel.Ind = 1;
            objNewBudgetAmountModel.OrgID = GlobalSession.OrgID;
            objNewBudgetAmountModel.BrID = GlobalSession.BrID;
            objNewBudgetAmountModel.DeptID = GlobalSession.DepartmentID;

            string uri = string.Format("NewBudgetAmount/BindAll");
            DataSet dsSubSection = CommonCls.ApiPostDataSet(uri, objNewBudgetAmountModel);

            if (dsSubSection.Tables.Count > 0)
            {
                VSMainSection = dsSubSection.Tables[2];
                VSSSearchubSection = dsSubSection.Tables[3];
                DataTable dtGRD = dsSubSection.Tables[2];
                if (dtGRD.Rows.Count > 0)
                {
                    DataView dvSection = new DataView(dsSubSection.Tables[2]);
                    dvSection.RowFilter = "DeptParentID > 0";
                    VSSection = dvSection.ToTable();
                    lstSection.DataSource = dvSection.ToTable();
                    lstSection.DataTextField = "DeptName";
                    lstSection.DataValueField = "DeptName";
                    lstSection.DataBind();



                    ddlNewSubSection.DataSource = dvSection.ToTable();
                    ddlNewSubSection.DataTextField = "DeptName";
                    ddlNewSubSection.DataValueField = "DeptID";
                    ddlNewSubSection.DataBind();
                    if (dvSection.ToTable().Rows.Count > 1)
                    {
                        ddlNewSubSection.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }

                //DataTable dtSubSection = dsSubSection.Tables[0];

                //if (dtSubSection.Rows.Count > 0)
                //{
                //    DataView dv31 = new DataView(dtSubSection);
                //    dv31.RowFilter = "ParentSectionID > 0";
                //    VSSubSection = dv31.ToTable();
                //    lstSubSection.DataSource = VSSubSection;
                //    lstSubSection.DataTextField = "SectionName";
                //    lstSubSection.DataValueField = "SectionName";
                //    lstSubSection.DataBind();
                //}

                if (dsSubSection.Tables[1].Rows.Count > 0)
                {
                    VSBudgetHead = dsSubSection.Tables[1];
                    lstBudgetHead.DataSource = dsSubSection.Tables[1];
                    lstBudgetHead.DataTextField = "AccName";
                    lstBudgetHead.DataValueField = "AccName";
                    lstBudgetHead.DataBind();
                }

                if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0)
                {
                    DataView dvSection = new DataView(VSMainSection);
                    dvSection.RowFilter = "DeptParentID =" + GlobalSession.DepartmentID;
                    VSSection = dvSection.ToTable();
                    lstSection.DataSource = dvSection.ToTable();
                    lstSection.DataTextField = "DeptName";
                    lstSection.DataValueField = "DeptName";
                    lstSection.DataBind();

                    ddlNewSubSection.DataSource = dvSection.ToTable();
                    ddlNewSubSection.DataTextField = "DeptName";
                    ddlNewSubSection.DataValueField = "DeptName";
                    ddlNewSubSection.DataBind();



                    if (dvSection.ToTable().Rows.Count == 1)
                    {
                        lstSection.SelectedValue = dvSection.ToTable().Rows[0]["DeptID"].ToString();
                        txtSectionName.Text = dvSection.ToTable().Rows[0]["DeptName"].ToString();
                    }
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

    private void CheckBudgetAmountIsExist()
    {
        try
        {

            objNewBudgetAmountModel = new NewBudgetAmountModel();
            objNewBudgetAmountModel.Ind = 3;
            objNewBudgetAmountModel.OrgID = GlobalSession.OrgID;
            objNewBudgetAmountModel.BrID = GlobalSession.BrID;
            objNewBudgetAmountModel.YrCode = GlobalSession.BudgetYrCD;//Bugdet Year for new session (18)
            objNewBudgetAmountModel.SectionCD = CommonCls.ConvertIntZero(SectionID);
            objNewBudgetAmountModel.SubSectionCD = CommonCls.ConvertIntZero(SubSectionID);
            objNewBudgetAmountModel.BudgetHeadCD = CommonCls.ConvertIntZero(BudgetHeadID);
            string uri = string.Format("NewBudgetAmount/CheckBudgetAmount");
            DataTable dtSubSection = CommonCls.ApiPostDataTable(uri, objNewBudgetAmountModel);
            if (dtSubSection.Rows.Count > 0)
            {

                //userType = user.Type == 0 ? "Admin"

                txtActualUptoBudgetAmtDr.Text = lblActualUptoBudgetAmtDr.Text = (dtSubSection.Rows[0]["ActualUptoBudgetAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["ActualUptoBudgetAmtDr"].ToString();

                txtActualUptoBudgetAmtCr.Text = lblActualUptoBudgetAmtCr.Text = (dtSubSection.Rows[0]["ActualUptoBudgetAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["ActualUptoBudgetAmtCr"].ToString();

                txtPropLastQtrBudgetAmtDr.Text = lblPropLastQtrBudgetAmtDr.Text = (dtSubSection.Rows[0]["PropLastQtrBudgetAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropLastQtrBudgetAmtDr"].ToString();

                txtPropLastQtrBudgetAmtCr.Text = lblPropLastQtrBudgetAmtCr.Text = (dtSubSection.Rows[0]["PropLastQtrBudgetAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropLastQtrBudgetAmtCr"].ToString();

                txtPropBudgetCapitalAmtDr.Text = lblPropBudgetAmtDr.Text = (dtSubSection.Rows[0]["PropBudgetCapitalAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropBudgetCapitalAmtDr"].ToString();

                txtPropBudgetCapitalAmtCr.Text = lblPropBudgetAmtCr.Text = (dtSubSection.Rows[0]["PropBudgetCapitalAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropBudgetCapitalAmtCr"].ToString();

                txtSanc2BudgetAmtDr.Text = lblSanc2BudgetAmtDr.Text = (dtSubSection.Rows[0]["Sanc2BudgetAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["Sanc2BudgetAmtDr"].ToString();

                txtSanc2BudgetAmtCr.Text = lblSanc2BudgetAmtCr.Text = (dtSubSection.Rows[0]["Sanc2BudgetAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["Sanc2BudgetAmtCr"].ToString();

                txtProp2BudgetAmtDr.Text = lblProp2BudgetAmtDr.Text = (dtSubSection.Rows[0]["Prop2BudgetAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["Prop2BudgetAmtDr"].ToString();

                txtProp2BudgetAmtCr.Text = lblProp2BudgetAmtCr.Text = (dtSubSection.Rows[0]["Prop2BudgetAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["Prop2BudgetAmtCr"].ToString();

                txtPropBudgetRevenueAmtDr.Text = lblPropBudgetRevenueAmtDr.Text = (dtSubSection.Rows[0]["PropBudgetRevenueAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropBudgetRevenueAmtDr"].ToString();

                txtPropBudgetRevenueAmtCr.Text = lblPropBudgetRevenueAmtCr.Text = (dtSubSection.Rows[0]["PropBudgetRevenueAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropBudgetRevenueAmtCr"].ToString();

                txtPropBudgetTotalAmtCr.Text = (dtSubSection.Rows[0]["PropBudgetAmtCr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropBudgetAmtCr"].ToString();
                txtPropBudgetTotalAmtDr.Text = (dtSubSection.Rows[0]["PropBudgetAmtDr"].ToString() == "-1") ? "" : dtSubSection.Rows[0]["PropBudgetAmtDr"].ToString();

                //txtActualUptoBudgetAmtDr.Text = lblActualUptoBudgetAmtDr.Text = dtSubSection.Rows[0]["ActualUptoBudgetAmtDr"].ToString();

                //txtActualUptoBudgetAmtCr.Text = lblActualUptoBudgetAmtCr.Text = dtSubSection.Rows[0]["ActualUptoBudgetAmtCr"].ToString();
                //txtPropLastQtrBudgetAmtDr.Text = lblPropLastQtrBudgetAmtDr.Text = dtSubSection.Rows[0]["PropLastQtrBudgetAmtDr"].ToString();
                //txtPropLastQtrBudgetAmtCr.Text = lblPropLastQtrBudgetAmtCr.Text = dtSubSection.Rows[0]["PropLastQtrBudgetAmtCr"].ToString();
                //txtPropBudgetCapitalAmtDr.Text = lblPropBudgetAmtDr.Text = dtSubSection.Rows[0]["PropBudgetCapitalAmtDr"].ToString();
                //txtPropBudgetCapitalAmtCr.Text = lblPropBudgetAmtCr.Text = dtSubSection.Rows[0]["PropBudgetCapitalAmtCr"].ToString();
                //txtSanc2BudgetAmtDr.Text = lblSanc2BudgetAmtDr.Text = dtSubSection.Rows[0]["Sanc2BudgetAmtDr"].ToString();
                //txtSanc2BudgetAmtCr.Text = lblSanc2BudgetAmtCr.Text = dtSubSection.Rows[0]["Sanc2BudgetAmtCr"].ToString();
                //txtProp2BudgetAmtDr.Text = lblProp2BudgetAmtDr.Text = dtSubSection.Rows[0]["Prop2BudgetAmtDr"].ToString();
                //txtProp2BudgetAmtCr.Text = lblProp2BudgetAmtCr.Text = dtSubSection.Rows[0]["Prop2BudgetAmtCr"].ToString();
                //txtPropBudgetRevenueAmtDr.Text = lblPropBudgetRevenueAmtDr.Text = dtSubSection.Rows[0]["PropBudgetRevenueAmtDr"].ToString();
                //txtPropBudgetRevenueAmtCr.Text = lblPropBudgetRevenueAmtCr.Text = dtSubSection.Rows[0]["PropBudgetRevenueAmtCr"].ToString();
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
        txtActualUptoBudgetAmtDr.Text = lblActualUptoBudgetAmtDr.Text =
         txtActualUptoBudgetAmtCr.Text = lblActualUptoBudgetAmtCr.Text =
         txtPropLastQtrBudgetAmtDr.Text = lblPropLastQtrBudgetAmtDr.Text =
         txtPropLastQtrBudgetAmtCr.Text = lblPropLastQtrBudgetAmtCr.Text =
         txtPropBudgetCapitalAmtDr.Text = lblPropBudgetAmtDr.Text =
         txtPropBudgetCapitalAmtCr.Text = lblPropBudgetAmtCr.Text =
                    txtSanc2BudgetAmtDr.Text = lblSanc2BudgetAmtDr.Text =
                    txtSanc2BudgetAmtCr.Text = lblSanc2BudgetAmtCr.Text =
         txtProp2BudgetAmtDr.Text = lblProp2BudgetAmtDr.Text =
         txtProp2BudgetAmtCr.Text = lblProp2BudgetAmtCr.Text = txtPropBudgetRevenueAmtCr.Text = txtPropBudgetRevenueAmtDr.Text = lblPropBudgetRevenueAmtDr.Text =
                                                                                                                         lblPropBudgetRevenueAmtCr.Text = "";
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

            if (ddlSubSection.SelectedValue == "0")
            {
                ShowMessage("Select Sub-Section Name.", false);
                ddlSubSection.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtSubSection.Text))
            {
                ShowMessage("Select Cost Centre Name.", false);
                txtSubSection.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtBudgetHead.Text))
            {
                ShowMessage("Select Budget Head Name.", false);
                txtBudgetHead.Focus();
                return;
            }
            CalCulateRevisedBudgetAmt();
            objNewBudgetAmountModel = new NewBudgetAmountModel();
            objNewBudgetAmountModel.Ind = 21;
            objNewBudgetAmountModel.OrgID = GlobalSession.OrgID;
            objNewBudgetAmountModel.BrID = GlobalSession.BrID;
            objNewBudgetAmountModel.YrCode = GlobalSession.BudgetYrCD;
            objNewBudgetAmountModel.SectionCD = 0;
            objNewBudgetAmountModel.SubSectionCD = CommonCls.ConvertIntZero(SubSectionID);
            objNewBudgetAmountModel.BudgetHeadCD = CommonCls.ConvertIntZero(BudgetHeadID);
            objNewBudgetAmountModel.SecCode = CommonCls.ConvertIntZero(SectionID);

            objNewBudgetAmountModel.UserID = GlobalSession.UserID;
            objNewBudgetAmountModel.IP = "";


            //Current Year Capital  
            if (txtPropBudgetCapitalAmtDr.Text == "" && txtPropBudgetCapitalAmtDr.Enabled == true)
                objNewBudgetAmountModel.PropBudgetCapitalAmtDr = -1;
            else
                objNewBudgetAmountModel.PropBudgetCapitalAmtDr = CommonCls.ConvertDecimalZero(txtPropBudgetCapitalAmtDr.Text);

            if (txtPropBudgetCapitalAmtCr.Text == "" && txtPropBudgetCapitalAmtCr.Enabled == true)
                objNewBudgetAmountModel.PropBudgetCapitalAmtCr = -1;
            else
                objNewBudgetAmountModel.PropBudgetCapitalAmtCr = CommonCls.ConvertDecimalZero(txtPropBudgetCapitalAmtCr.Text);

            //Current Year Revenue
            if (txtPropBudgetRevenueAmtDr.Text == "" && txtPropBudgetRevenueAmtDr.Enabled == true)
                objNewBudgetAmountModel.PropBudgetRevenueAmtDr = -1;
            else
                objNewBudgetAmountModel.PropBudgetRevenueAmtDr = CommonCls.ConvertDecimalZero(txtPropBudgetRevenueAmtDr.Text);

            if (txtPropBudgetRevenueAmtCr.Text == "" && txtPropBudgetRevenueAmtCr.Enabled == true)
                objNewBudgetAmountModel.PropBudgetRevenueAmtCr = -1;
            else
                objNewBudgetAmountModel.PropBudgetRevenueAmtCr = CommonCls.ConvertDecimalZero(txtPropBudgetRevenueAmtCr.Text);


            // Propose Budget Amt
            if (txtProp2BudgetAmtDr.Text == "" && txtProp2BudgetAmtDr.Enabled == true)
                objNewBudgetAmountModel.Prop2BudgetAmtDr = -1;
            else
                objNewBudgetAmountModel.Prop2BudgetAmtDr = CommonCls.ConvertDecimalZero(txtProp2BudgetAmtDr.Text);


            if (txtProp2BudgetAmtCr.Text == "" && txtProp2BudgetAmtCr.Enabled == true)
                objNewBudgetAmountModel.Prop2BudgetAmtCr = -1;
            else
                objNewBudgetAmountModel.Prop2BudgetAmtCr = CommonCls.ConvertDecimalZero(txtProp2BudgetAmtCr.Text);


            //Actual Busget Amt

            if (txtActualUptoBudgetAmtCr.Text == "" && txtActualUptoBudgetAmtCr.Enabled == true)
                objNewBudgetAmountModel.ActualUptoBudgetAmtCr = -1;
            else
                objNewBudgetAmountModel.ActualUptoBudgetAmtCr = CommonCls.ConvertDecimalZero(txtActualUptoBudgetAmtCr.Text);

            if (txtActualUptoBudgetAmtDr.Text == "" && txtActualUptoBudgetAmtDr.Enabled == true)
                objNewBudgetAmountModel.ActualUptoBudgetAmtDr = -1;
            else
                objNewBudgetAmountModel.ActualUptoBudgetAmtDr = CommonCls.ConvertDecimalZero(txtActualUptoBudgetAmtDr.Text);


            //Qtr Amt

            if (txtPropLastQtrBudgetAmtCr.Text == "" && txtPropLastQtrBudgetAmtCr.Enabled == true)
                objNewBudgetAmountModel.PropLastQtrBudgetAmtCr = -1;
            else
                objNewBudgetAmountModel.PropLastQtrBudgetAmtCr = CommonCls.ConvertDecimalZero(txtPropLastQtrBudgetAmtCr.Text);


            if (txtPropLastQtrBudgetAmtDr.Text == "" && txtPropLastQtrBudgetAmtDr.Enabled == true)
                objNewBudgetAmountModel.PropLastQtrBudgetAmtDr = -1;
            else
                objNewBudgetAmountModel.PropLastQtrBudgetAmtDr = CommonCls.ConvertDecimalZero(txtPropLastQtrBudgetAmtDr.Text);


            //Revised
            //if (txtSanc2BudgetAmtCr.Text == "" && txtSanc2BudgetAmtCr.Enabled == true)
            //    objNewBudgetAmountModel.Sanc2BudgetAmtCr = -1;
            //else
            //    objNewBudgetAmountModel.Sanc2BudgetAmtCr = CommonCls.ConvertDecimalZero(txtSanc2BudgetAmtCr.Text);

            //if (txtSanc2BudgetAmtDr.Text == "" && txtSanc2BudgetAmtDr.Enabled == true)
            //    objNewBudgetAmountModel.Sanc2BudgetAmtDr = -1;
            //else
            //    objNewBudgetAmountModel.Sanc2BudgetAmtDr = CommonCls.ConvertDecimalZero(txtSanc2BudgetAmtDr.Text);

            objNewBudgetAmountModel.Sanc2BudgetAmtCr = (CommonCls.ConvertDecimalZero(objNewBudgetAmountModel.ActualUptoBudgetAmtCr) + CommonCls.ConvertDecimalZero(objNewBudgetAmountModel.PropLastQtrBudgetAmtCr));


            objNewBudgetAmountModel.Sanc2BudgetAmtDr = (CommonCls.ConvertDecimalZero(objNewBudgetAmountModel.ActualUptoBudgetAmtDr) + CommonCls.ConvertDecimalZero(objNewBudgetAmountModel.PropLastQtrBudgetAmtDr));



            if (txtPropBudgetCapitalAmtCr.Text == "" && txtPropBudgetRevenueAmtCr.Text == "" && txtPropBudgetCapitalAmtCr.Enabled == true && txtPropBudgetRevenueAmtCr.Enabled == true)
            {
                objNewBudgetAmountModel.PropBudgetAmtCr = -1;
            }
            else
            {
                decimal PropBudgetAmtCr = (CommonCls.ConvertDecimalZero(txtPropBudgetCapitalAmtCr.Text) + CommonCls.ConvertDecimalZero(txtPropBudgetRevenueAmtCr.Text));
                objNewBudgetAmountModel.PropBudgetAmtCr = CommonCls.ConvertDecimalZero(PropBudgetAmtCr);
            }


            if (txtPropBudgetCapitalAmtDr.Text == "" && txtPropBudgetRevenueAmtDr.Text == "" && txtPropBudgetCapitalAmtDr.Enabled == true && txtPropBudgetRevenueAmtDr.Enabled == true)
            {
                objNewBudgetAmountModel.PropBudgetAmtDr = -1;
            }
            else
            {
                decimal PropBudgetAmtDr = (CommonCls.ConvertDecimalZero(txtPropBudgetCapitalAmtDr.Text) + CommonCls.ConvertDecimalZero(txtPropBudgetRevenueAmtDr.Text));
                objNewBudgetAmountModel.PropBudgetAmtDr = CommonCls.ConvertDecimalZero(PropBudgetAmtDr);
            }

            //objNewBudgetAmountModel.PropBudgetAmtCr = CommonCls.ConvertDecimalZero(txtPropBudgetCapitalAmtCr.Text + txtPropBudgetRevenueAmtCr.Text);
            //objNewBudgetAmountModel.PropBudgetAmtDr = CommonCls.ConvertDecimalZero(txtPropBudgetCapitalAmtDr.Text + txtPropBudgetRevenueAmtDr.Text);

            string uri = string.Format("NewBudgetAmount/SaveBudget");

            DataTable dtOpeningBalance = CommonCls.ApiPostDataTable(uri, objNewBudgetAmountModel);
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


        lblMsg.Text = lblMsg.CssClass = "";



        ClearAll();

        //if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0)
        //{

        //    ddlSubSection.ClearSelection();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("SectionName", typeof(string));
        //    lstSubSection.DataSource = dt;
        //    lstSubSection.DataTextField = "SectionName";
        //    lstSubSection.DataValueField = "SectionName";
        //    lstSubSection.DataBind();
        //}

        if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID == 0 && GlobalSession.SubDeptID == 0)
        {

            txtSectionName.Text = txtSubSection.Text = "";
            ddlSubSection.ClearSelection();
            divListBudgetHead.Visible = false; divListSubSection.Visible = false;
            divListSection.Visible = true;
            txtSectionName.Focus();

        }
        if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0)
        {
            txtBudgetHead.Text = txtSectionName.Text = txtSubSection.Text = "";
            divListBudgetHead.Visible = divListSubSection.Visible = false;
            divListSection.Visible = true;
            txtSectionName.Enabled = true;

        }

        if (GlobalSession.IsAdmin == 0 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID > 0)
        {
            txtSubSection.Text = txtBudgetHead.Text = "";
            divListBudgetHead.Visible = divListSection.Visible = false;
            divListSubSection.Visible = true;
        }
    }

    private void ClearAll()
    {
        lblSectionNameHindi.Text = lblSubSectionNameHindi.Text = lblBudgetHead.Text = lblProp2BudgetAmtDr.Text = lblActualUptoBudgetAmtDr.Text = lblPropLastQtrBudgetAmtDr.Text = lblSanc2BudgetAmtDr.Text = lblPropBudgetAmtDr.Text = lblPropBudgetRevenueAmtDr.Text = lblPropBudgetRevenueAmtCr.Text = lblProp2BudgetAmtCr.Text = lblActualUptoBudgetAmtCr.Text = lblPropLastQtrBudgetAmtCr.Text = lblSanc2BudgetAmtCr.Text = lblPropBudgetAmtCr.Text = "";
        txtActualUptoBudgetAmtDr.Text =
        txtPropLastQtrBudgetAmtCr.Text =
        txtPropBudgetCapitalAmtCr.Text =
        txtProp2BudgetAmtCr.Text = txtSanc2BudgetAmtDr.Text = txtSanc2BudgetAmtCr.Text = "";
        txtActualUptoBudgetAmtCr.Text =
        txtPropLastQtrBudgetAmtDr.Text =
        txtPropBudgetCapitalAmtDr.Text =
        txtProp2BudgetAmtDr.Text = txtPropBudgetRevenueAmtDr.Text = txtPropBudgetRevenueAmtCr.Text = txtPropBudgetTotalAmtDr.Text = txtPropBudgetTotalAmtCr.Text = "";
        txtPropLastQtrBudgetAmtCr.Enabled =
            txtPropLastQtrBudgetAmtDr.Enabled =
         txtActualUptoBudgetAmtCr.Enabled =
          txtActualUptoBudgetAmtDr.Enabled =
         txtPropBudgetCapitalAmtDr.Enabled =
         txtPropBudgetCapitalAmtCr.Enabled =
        txtSanc2BudgetAmtDr.Enabled =
        txtSanc2BudgetAmtCr.Enabled =
        txtProp2BudgetAmtDr.Enabled =
        txtProp2BudgetAmtCr.Enabled = txtPropBudgetRevenueAmtDr.Enabled = txtPropBudgetRevenueAmtCr.Enabled = txtSectionName.Enabled = false;

        //For Account Admin

        if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID == 0 && GlobalSession.SubDeptID == 0)
        {
            txtBudgetHead.Text = "";//txtSubSection.Text = txtSectionName.Text = "";
            //ddlSubSection.ClearSelection();

            DataTable dt = new DataTable();
            dt.Columns.Add("SectionName", typeof(string));
            ddlSubSection.DataSource = dt;
            ddlSubSection.DataTextField = "SectionName";
            ddlSubSection.DataValueField = "SectionName";
            ddlSubSection.DataBind();
            divListBudgetHead.Visible = true; divListSubSection.Visible = false;
            divListSection.Visible = false;
            txtSectionName.Enabled = true;
            txtBudgetHead.Focus();
        }
        //For Department Admin

        if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0)
        {
            txtBudgetHead.Text = "";
            divListSection.Visible = divListSubSection.Visible = false;
            divListBudgetHead.Visible = true;
            txtSectionName.Enabled = true;
        }

        //For Sub-Department Admin

        if (GlobalSession.IsAdmin == 0 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID > 0)
        {
            txtBudgetHead.Text = "";
            divListSubSection.Visible = divListSection.Visible = false;
            divListBudgetHead.Visible = true;
        }
        lstBudgetHead.ClearSelection();
    }

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
                    dtGRD.Rows[i]["DeptName"] = dtGRD.Rows[i]["DeptName"].ToString().Replace("%", "_").Trim();
                }

                DataView ReplaceSectionTable = dv31;

                if (GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID > 0)
                {
                    SectionID = GlobalSession.DepartmentID;
                    ReplaceSectionTable.RowFilter = "DeptID =" + GlobalSession.SubDeptID + "";
                    lblSectionNameHindi.Text = ReplaceSectionTable.ToTable().Rows[0]["DeptNameHindi"].ToString();
                    SectionID = CommonCls.ConvertIntZero(ReplaceSectionTable.ToTable().Rows[0]["DeptID"].ToString());
                    txtSectionName.Text = ReplaceSectionTable.ToTable().Rows[0]["DeptName"].ToString();
                    txtSectionName.Enabled = false;


                }
                else
                {

                    ReplaceSectionTable.RowFilter = "DeptName LIKE '" + ReplaceSectionName + "%'";
                    lblSectionNameHindi.Text = ReplaceSectionTable.ToTable().Rows[0]["DeptNameHindi"].ToString();
                    SectionID = CommonCls.ConvertIntZero(ReplaceSectionTable.ToTable().Rows[0]["DeptID"].ToString());
                    txtSectionName.Text = ReplaceSectionTable.ToTable().Rows[0]["DeptName"].ToString();

                }


            }
            ddlSubSection.Focus();
            if (GlobalSession.DepartmentID != 0)
            {
                divListSection.Visible = false;
                divListSubSection.Visible = true;
                divListBudgetHead.Visible = false;
            }
            else
            {

                divListSection.Visible = true;
                divListBudgetHead.Visible = false;
                divListSubSection.Visible = false;
            }
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


        lblMsg.Text = lblMsg.CssClass = "";
        DataTable dtSubSection = VSSSearchubSection;

        if (dtSubSection.Rows.Count > 0)
        {
            DataView dv31 = new DataView(dtSubSection);
            dv31.RowFilter = "SubDeptID =" + SectionID + "";
            VSSubSection = dv31.ToTable();
            if (dv31.ToTable().Rows.Count > 0)
            {
                lstSubSection.DataSource = VSSubSection;
                lstSubSection.DataTextField = "SectionName";
                lstSubSection.DataValueField = "SectionName";
                lstSubSection.DataBind();
                divListSection.Visible = false;
                divListBudgetHead.Visible = false;
                divListSubSection.Visible = true;
            }
            else
            {
                ShowMessage("There is no Cost Centre For This Sub-Section.", false);

                lstSubSection.DataSource = VSSubSection;
                lstSubSection.DataTextField = "SectionName";
                lstSubSection.DataValueField = "SectionName";
                lstSubSection.DataBind();
            }

        }





        lstSection.ClearSelection();
        SectionName = "";
    }

    protected void lstSubSection_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            DataTable dtGRD = VSSubSection;
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

                #region Replacing
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
                    txtActualUptoBudgetAmtCr.Enabled = txtPropLastQtrBudgetAmtCr.Enabled = txtPropBudgetCapitalAmtCr.Enabled = txtProp2BudgetAmtCr.Enabled = txtPropBudgetRevenueAmtCr.Enabled = true;
                    lblActualUptoBudgetAmtCr.Visible = lblPropLastQtrBudgetAmtCr.Visible = lblPropBudgetAmtCr.Visible = lblProp2BudgetAmtCr.Visible = lblSanc2BudgetAmtCr.Visible = lblPropBudgetRevenueAmtCr.Visible = true;
                    txtActualUptoBudgetAmtDr.Enabled = txtPropLastQtrBudgetAmtDr.Enabled = txtPropBudgetCapitalAmtDr.Enabled = txtProp2BudgetAmtDr.Enabled = txtPropBudgetRevenueAmtDr.Enabled = txtProp2BudgetAmtDr.Enabled = false;
                    lblActualUptoBudgetAmtDr.Visible = lblPropLastQtrBudgetAmtDr.Visible = lblPropBudgetAmtDr.Visible = lblPropBudgetRevenueAmtDr.Visible = lblProp2BudgetAmtDr.Visible = lblSanc2BudgetAmtDr.Visible = false;
                }
                else if (dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "23" || dv31.ToTable().Rows[0]["MainGroupID"].ToString() == "25")
                {
                    txtActualUptoBudgetAmtCr.Enabled = txtPropLastQtrBudgetAmtCr.Enabled = txtPropBudgetCapitalAmtCr.Enabled = txtProp2BudgetAmtCr.Enabled = txtPropBudgetRevenueAmtCr.Enabled = false;
                    txtActualUptoBudgetAmtDr.Enabled = txtPropLastQtrBudgetAmtDr.Enabled = txtPropBudgetCapitalAmtDr.Enabled = txtProp2BudgetAmtDr.Enabled = txtPropBudgetRevenueAmtDr.Enabled = txtProp2BudgetAmtDr.Enabled = true;
                    lblActualUptoBudgetAmtDr.Visible = lblPropLastQtrBudgetAmtDr.Visible = lblPropBudgetAmtDr.Visible = lblPropBudgetRevenueAmtDr.Visible = lblProp2BudgetAmtDr.Visible = lblSanc2BudgetAmtDr.Visible = true;
                    lblActualUptoBudgetAmtCr.Visible = lblPropLastQtrBudgetAmtCr.Visible = lblPropBudgetAmtCr.Visible = lblProp2BudgetAmtCr.Visible = lblSanc2BudgetAmtCr.Visible = lblPropBudgetRevenueAmtCr.Visible = false;
                }
            }

            CheckBudgetAmountIsExist();
            //CalCulateRevisedBudgetAmt();

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

    protected void txtPropLastQtrBudgetAmtDr_TextChanged(object sender, EventArgs e)
    {
        CalCulateRevisedBudgetAmt();
    }

    private void CalCulateRevisedBudgetAmt()
    {
        if (txtActualUptoBudgetAmtDr.Text == "" && txtPropLastQtrBudgetAmtDr.Text == "")
        {
            txtSanc2BudgetAmtDr.Text = "";
        }
        else
        {
            decimal Sanc2BudgetAmtDr = (CommonCls.ConvertDecimalZero(txtActualUptoBudgetAmtDr.Text) + CommonCls.ConvertDecimalZero(txtPropLastQtrBudgetAmtDr.Text));
            txtSanc2BudgetAmtDr.Text = (Sanc2BudgetAmtDr).ToString();
        }
        if (txtActualUptoBudgetAmtCr.Text == "" && txtPropLastQtrBudgetAmtCr.Text == "")
        {
            txtSanc2BudgetAmtDr.Text = "";
        }
        else
        {
            decimal Sanc2BudgetAmtCr = (CommonCls.ConvertDecimalZero(txtActualUptoBudgetAmtCr.Text) + CommonCls.ConvertDecimalZero(txtPropLastQtrBudgetAmtCr.Text));
            txtSanc2BudgetAmtCr.Text = (Sanc2BudgetAmtCr).ToString();
        }
    }

    protected void txtPropLastQtrBudgetAmtCr_TextChanged(object sender, EventArgs e)
    {
        CalCulateRevisedBudgetAmt();
    }

    protected void ddlSubSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";
            DataTable dtSubSection = VSSSearchubSection;

            if (dtSubSection.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtSubSection);
                dv31.RowFilter = "DepartmentID =" + SectionID + "And SubDeptID =" + ddlSubSection.SelectedValue + "";
                VSSubSection = dv31.ToTable();
                if (dv31.ToTable().Rows.Count > 0)
                {
                    lstSubSection.DataSource = VSSubSection;
                    lstSubSection.DataTextField = "SectionName";
                    lstSubSection.DataValueField = "SectionName";
                    lstSubSection.DataBind();
                    divListSection.Visible = false;
                    divListBudgetHead.Visible = false;
                    divListSubSection.Visible = true;
                }
                else
                {
                    ShowMessage("There is no Cost Centre For This Sub-Section.", false);

                    lstSubSection.DataSource = VSSubSection;
                    lstSubSection.DataTextField = "SectionName";
                    lstSubSection.DataValueField = "SectionName";
                    lstSubSection.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void lnkbtnShowScheme_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = true;
    }
    protected void ddlNewSubSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        DataView dvCostCentre = new DataView(VSSSearchubSection);
        dvCostCentre.RowFilter = "SubDeptID =" + ddlNewSubSection.SelectedValue + "";
        ddlNewCostCentre.DataSource = dvCostCentre.ToTable();
        ddlNewCostCentre.DataTextField = "SectionName";
        ddlNewCostCentre.DataValueField = "SectionID";
        ddlNewCostCentre.DataBind();
        if (dvCostCentre.ToTable().Rows.Count > 1)
        {
            ddlNewCostCentre.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
    }
    protected void ddlNewCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}