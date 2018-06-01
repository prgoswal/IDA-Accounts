using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmRCMLiability : System.Web.UI.Page
{
    RCMLiabilityModel objRCMLiaModel;

    DataTable VsdtRCMLiability
    {
        get { return (DataTable)ViewState["dtRCMLiability"]; }
        set { ViewState["dtRCMLiability"] = value; }
    }

    DataTable VsdtDateWise
    {
        get { return (DataTable)ViewState["dtDateWise"]; }
        set { ViewState["dtDateWise"] = value; }
    }

    DataTable VsdtRateWise
    {
        get { return (DataTable)ViewState["dtRateWise"]; }
        set { ViewState["dtRateWise"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGSTIN();
        }
        
        lblMsg.Text = lblMsg.CssClass = "";
    }

    void LoadGSTIN()
    {
        try
        {
            objRCMLiaModel = new RCMLiabilityModel();
            objRCMLiaModel.Ind = 3;
            objRCMLiaModel.OrgID = GlobalSession.OrgID;
            objRCMLiaModel.BRID = GlobalSession.BrID;
            objRCMLiaModel.YRCD = GlobalSession.YrCD;
            string uri = string.Format("RCMLiability/BindGSTIN");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, objRCMLiaModel);
            if (dtGSTIN.Rows.Count > 0)
            {
                if (dtGSTIN.Rows.Count > 1)
                {
                    ddlGSTINNO.DataSource = dtGSTIN;
                    ddlGSTINNO.DataValueField = "GSTIN";
                    ddlGSTINNO.DataBind();
                    ddlGSTINNO.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                }
                else
                {
                    ddlGSTINNO.DataSource = dtGSTIN;
                    ddlGSTINNO.DataValueField = "GSTIN";
                    ddlGSTINNO.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #region View 1
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonth.SelectedItem != null)
            {
                if (ddlMonth.SelectedValue == "0")
                {
                    ShowMessage("Select Month.", false);
                    ddlMonth.Focus();
                    return;
                }
            }
            if (ddlYear.SelectedItem != null)
            {
                if (ddlYear.SelectedValue == "0")
                {
                    ShowMessage("Select Year.", false);
                    ddlYear.Focus();
                    return;
                }
            }
            if (ddlGSTINNO.SelectedItem != null)
            {
                if (ddlGSTINNO.SelectedValue == "0")
                {
                    ShowMessage("Select GSTIN.", false);
                    ddlGSTINNO.Focus();
                    return;
                }
            }

            objRCMLiaModel = new RCMLiabilityModel();
            objRCMLiaModel.Ind = 1;
            objRCMLiaModel.OrgID = GlobalSession.OrgID;
            objRCMLiaModel.MonthID = Convert.ToInt32(ddlMonth.SelectedValue);
            //objRCMLiaModel.YearCode = Convert.ToInt32(ddlYear.SelectedValue);
            objRCMLiaModel.YearID = Convert.ToInt32(ddlYear.SelectedItem.Text);
            objRCMLiaModel.GSTIN = ddlGSTINNO.SelectedValue;
            string uri = string.Format("RCMLiability/DisplayPVItemRecord");
            DataSet dsDisplayPVItemRecord = CommonCls.ApiPostDataSet(uri, objRCMLiaModel);
            if (dsDisplayPVItemRecord.Tables.Count > 0)
            {
                DataTable dtSummary = dsDisplayPVItemRecord.Tables[0];
                DataTable dtLiability = dsDisplayPVItemRecord.Tables[1];

                if (dtSummary.Rows.Count > 0)
                {
                    lblTotalPurchase.Text = dtSummary.Rows[0]["TotalPurchase"].ToString();
                    lblRegisteredPurchase.Text = dtSummary.Rows[0]["RegisteredPurchase"].ToString();
                    lblUnregisteredPurchase.Text = dtSummary.Rows[0]["UnRegisteredPurchase"].ToString();
                    lblZeroNilExeNonGST.Text = dtSummary.Rows[0]["ZeroNilExemptedNonGSTAmount"].ToString();
                    lblRCMLiabilityAmount.Text = dtSummary.Rows[0]["RCMAmount"].ToString();
                    divSummary.Visible = true;
                }
                if (dtLiability.Rows.Count > 0)
                {
                    dtLiability.Columns.Add("Specific", typeof(decimal));
                    dtLiability.Columns.Add("URD", typeof(decimal));
                    dtLiability.Columns.Add("Exmpted", typeof(decimal));

                    grdRCMLiability.DataSource = VsdtRCMLiability = dtLiability;
                    grdRCMLiability.DataBind();

                    lblTotalAmount.Visible = true;
                }

                btnSave1.Enabled = true;
                ddlMonth.Enabled = ddlYear.Enabled = btnGo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void txtURD_TextChanged(object sender, EventArgs e)
    {
        TextBox txtUrdAmount = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txtUrdAmount.Parent.Parent;
        Label lblItemAmount = (Label)gvRow.FindControl("lblItemAmount");
        TextBox txtSpecificAmount = (TextBox)gvRow.FindControl("txtSpecific");
        if (CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) > 0)
        {
            txtSpecificAmount.Text = Convert.ToString(CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) - CommonCls.ConvertDecimalZero(txtUrdAmount.Text));
        }
        txtUrdAmount.Focus();
    }

    protected void txtExempted_TextChanged(object sender, EventArgs e)
    {
        TextBox txtExemptedAmount = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txtExemptedAmount.Parent.Parent;
        Label lblItemAmount = (Label)gvRow.FindControl("lblItemAmount");
        TextBox txtSpecificAmount = (TextBox)gvRow.FindControl("txtSpecific");
        if (CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) > 0)
        {
            txtSpecificAmount.Text = Convert.ToString(CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) - CommonCls.ConvertDecimalZero(txtExemptedAmount.Text));
        }
        txtExemptedAmount.Focus();
    }

    decimal finalSpecificAmount = 0, finalUrdAmount = 0, finalExemptedAmount = 0;
    protected void grdRCMLiability_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";

        int rowIndex = Convert.ToInt32(e.CommandArgument);
        Label lblItemAmount = (Label)grdRCMLiability.Rows[rowIndex].FindControl("lblItemAmount");
        TextBox txtSpecificAmount = (TextBox)grdRCMLiability.Rows[rowIndex].FindControl("txtSpecific");
        TextBox txtURD = (TextBox)grdRCMLiability.Rows[rowIndex].FindControl("txtURD");
        TextBox txtExempted = (TextBox)grdRCMLiability.Rows[rowIndex].FindControl("txtExempted");
        Button btnEdit = (Button)grdRCMLiability.Rows[rowIndex].FindControl("btnEdit");
        Button btnSubmit = (Button)grdRCMLiability.Rows[rowIndex].FindControl("btnSave");
        if (e.CommandName == "EditRow")
        {
            txtSpecificAmount.Enabled = txtURD.Enabled = txtExempted.Enabled = true;

            btnEdit.Visible = false;
            btnSubmit.Visible = true;
            foreach (GridViewRow grdRow in grdRCMLiability.Rows)
            {
                Button btnEditEnabledFalse = (Button)grdRow.FindControl("btnEdit");
                btnEditEnabledFalse.Enabled = false;
            }

            btnSave1.Enabled = false;
            btnSubmit.Focus();
        }
        if (e.CommandName == "SaveRow")
        {
            if (CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) < 0)
            {
                ShowMessage("Specific Amount Can Not Be Negative Please Check.", false);
                txtSpecificAmount.Focus();
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtURD.Text) < 0)
            {
                ShowMessage("URD Amount Can Not Be Negative Please Check.", false);
                txtURD.Focus();
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtExempted.Text) < 0)
            {
                ShowMessage("Exempted Amount Can Not Be Negative Please Check.", false);
                txtExempted.Focus();
                return;
            }
            // || CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) < 0 || CommonCls.ConvertDecimalZero(txtURD.Text) < 0 || CommonCls.ConvertDecimalZero(txtExempted.Text) < 0

            decimal rowTotalAmount = 0;
            rowTotalAmount = CommonCls.ConvertDecimalZero(txtSpecificAmount.Text) + CommonCls.ConvertDecimalZero(txtURD.Text) + CommonCls.ConvertDecimalZero(txtExempted.Text);
            if (Convert.ToDecimal(lblItemAmount.Text) != rowTotalAmount)
            {
                decimal differenceAmount = Convert.ToDecimal(lblItemAmount.Text) - rowTotalAmount;
                ShowMessage("Total Of (Specific, Urd and Exempted) Amount Not Equals To Item Amount. (Difference Amount = " + differenceAmount + ")", false);
                txtSpecificAmount.Focus();
                return;
            }

            foreach (GridViewRow grdRow in grdRCMLiability.Rows)
            {
                TextBox txtFinalSpecificAmount = (TextBox)grdRow.FindControl("txtSpecific");
                TextBox txtFinalURDAmount = (TextBox)grdRow.FindControl("txtURD");
                TextBox txtFinalExemptedAmount = (TextBox)grdRow.FindControl("txtExempted");

                finalSpecificAmount = finalSpecificAmount + Convert.ToDecimal(txtFinalSpecificAmount.Text);
                finalUrdAmount = finalUrdAmount + Convert.ToDecimal(txtFinalURDAmount.Text);
                finalExemptedAmount = finalExemptedAmount + Convert.ToDecimal(txtFinalExemptedAmount.Text);

                lblSpecificAmount.Text = Convert.ToString(finalSpecificAmount);
                lblURDAmount.Text = Convert.ToString(finalUrdAmount);
                lblExemptedAmount.Text = Convert.ToString(finalExemptedAmount);

            }

            foreach (GridViewRow grdRow in grdRCMLiability.Rows)
            {
                Button btnEditEnabledFalse = (Button)grdRow.FindControl("btnEdit");
                btnEditEnabledFalse.Enabled = true;
            }

            txtSpecificAmount.Enabled = txtURD.Enabled = txtExempted.Enabled = false;
            btnEdit.Visible = true;
            btnSubmit.Visible = false;
            btnSave1.Enabled = true;
            btnEdit.Focus();
        }
    }

    decimal itemAmount = 0, specificAmount = 0, urdAmount = 0, exemptedAmount = 0;
    protected void grdRCMLiability_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblItemAmount = (Label)e.Row.FindControl("lblItemAmount");
                itemAmount = itemAmount + CommonCls.ConvertDecimalZero(lblItemAmount.Text);
                lblTotalAmountValue.Text = Convert.ToString(itemAmount);

                TextBox txtSpecific = (TextBox)e.Row.FindControl("txtSpecific");
                specificAmount = specificAmount + CommonCls.ConvertDecimalZero(txtSpecific.Text);
                lblSpecificAmount.Text = Convert.ToString(specificAmount);

                TextBox txtURD = (TextBox)e.Row.FindControl("txtURD");
                urdAmount = urdAmount + CommonCls.ConvertDecimalZero(txtURD.Text);
                lblURDAmount.Text = Convert.ToString(urdAmount);

                TextBox txtExempted = (TextBox)e.Row.FindControl("txtExempted");
                exemptedAmount = exemptedAmount + CommonCls.ConvertDecimalZero(txtExempted.Text);
                lblExemptedAmount.Text = Convert.ToString(exemptedAmount);

                //Label lblItemAmount = (Label)e.Row.FindControl("lblItemAmount");
                //itemAmount = itemAmount + CommonCls.ConverFromCurrency(lblItemAmount.Text);
                //lblTotalAmountValue.Text = CommonCls.ConverToCurrency(itemAmount);

                //TextBox txtSpecific = (TextBox)e.Row.FindControl("txtSpecific");
                //specificAmount = specificAmount + CommonCls.ConverFromCurrency(txtSpecific.Text);
                //lblSpecificAmount.Text = CommonCls.ConverToCurrency(specificAmount);

                //TextBox txtURD = (TextBox)e.Row.FindControl("txtURD");
                //urdAmount = urdAmount + CommonCls.ConvertDecimalZero(txtURD.Text);
                //lblURDAmount.Text = CommonCls.ConverToCurrency(urdAmount);

                //TextBox txtExempted = (TextBox)e.Row.FindControl("txtExempted");
                //exemptedAmount = exemptedAmount + CommonCls.ConverFromCurrency(txtExempted.Text);
                //lblExemptedAmount.Text = CommonCls.ConverToCurrency(exemptedAmount);
            }
        }
    }

    protected void grdRCMLiability_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        MultView1.ActiveViewIndex = 1;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ClearAll();
    }

    void ClearAll()
    {
        lblMsg.Text = lblMsg.CssClass = "";

        btnSave1.Enabled = false;
        ddlMonth.ClearSelection();
        ddlYear.ClearSelection();
        lblTotalPurchase.Text = lblRegisteredPurchase.Text = lblUnregisteredPurchase.Text = lblRCMLiabilityAmount.Text = lblTotalAmountValue.Text =
            lblSpecificAmount.Text = lblURDAmount.Text = lblExemptedAmount.Text = lblDateWiseTotalAmount.Text = lblRateWiseSpecificTotalAmount.Text =
            lblCGSTTotalAmount.Text = lblSGSTTotalAmount.Text = "";
        divSummary.Visible = lblTotalAmount.Visible = false;
        VsdtRCMLiability = VsdtDateWise = VsdtRateWise = null;
        grdRCMLiability.DataSource = gvRCMDateWise.DataSource = gvRCMRateWise.DataSource = new DataTable();
        grdRCMLiability.DataBind(); gvRCMDateWise.DataBind(); gvRCMRateWise.DataBind();
        ddlMonth.Enabled = ddlYear.Enabled = btnGo.Enabled = true;

        MultView1.ActiveViewIndex = 0;
    }
    #endregion

    #region View 2
    protected void View2_Activate(object sender, EventArgs e)
    {
        int rowIndex = 0;
        foreach (GridViewRow item in grdRCMLiability.Rows)
        {
            VsdtRCMLiability.Rows[rowIndex]["Specific"] = CommonCls.ConvertDecimalZero((item.FindControl("txtSpecific") as TextBox).Text);
            VsdtRCMLiability.Rows[rowIndex]["URD"] = CommonCls.ConvertDecimalZero((item.FindControl("txtURD") as TextBox).Text);
            VsdtRCMLiability.Rows[rowIndex]["Exmpted"] = CommonCls.ConvertDecimalZero((item.FindControl("txtExempted") as TextBox).Text);
            rowIndex++;
        }
        DataView view = new DataView(VsdtRCMLiability);
        DataTable table = view.ToTable(true, "VoucharDate", "TaxRate");
        table.Columns.Add("Specific", typeof(decimal));

        foreach (DataRow item in table.Rows)
        {
            item["VoucharDate"] = item["VoucharDate"];
            item["TaxRate"] = item["TaxRate"];
            item["Specific"] = VsdtRCMLiability.Compute("Sum(Specific)", "VoucharDate = '" + item["VoucharDate"] + "' And TaxRate = '" + item["TaxRate"] + "'");
        }

        table.DefaultView.Sort = "VoucharDate Asc";

        gvRCMDateWise.DataSource = VsdtDateWise = table;
        gvRCMDateWise.DataBind();

        lblDateWiseTotalAmount.Text = VsdtDateWise.Compute("Sum(Specific)", "Specific > 5000").ToString();

        for (int i = 1; i < VsdtDateWise.Rows.Count; i++)
        {
            if (CommonCls.ConvertDecimalZero(VsdtDateWise.Rows[i]["Specific"].ToString()) <= 5000)
            {

            }
        }

        MultView1.ActiveViewIndex = 1;


        pnlStepChange.Visible = btnPrevious.Enabled = true;
    }

    protected void gvRCMDateWise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblSpecific = (Label)e.Row.FindControl("lblSpecific");
                if (CommonCls.ConvertDecimalZero(lblSpecific.Text) <= 5000)
                {
                    e.Row.Attributes["style"] = "background-color: Red; color : white";
                }
            }
        }
    }

    protected void btnSave2_Click(object sender, EventArgs e)
    {
        MultView1.ActiveViewIndex = 2;
    }

    #endregion

    #region View 3
    protected void View3_Activate(object sender, EventArgs e)
    {
        for (int i = 1; i < VsdtDateWise.Rows.Count; i++)
        {
            if (CommonCls.ConvertDecimalZero(VsdtDateWise.Rows[i]["Specific"].ToString()) <= 5000)
            {
                VsdtDateWise.Rows[i].Delete();
                i--;
            }
        }

        DataView view = new DataView(VsdtDateWise);
        DataTable table = view.ToTable(true, "TaxRate");

        table.Columns.Add("OrgID", typeof(int));
        table.Columns.Add("MonthID", typeof(int));
        table.Columns.Add("YearID", typeof(int));
        table.Columns.Add("GSTIN", typeof(string));
        table.Columns.Add("SpecificAmt", typeof(decimal));
        table.Columns.Add("CGSTAmt", typeof(decimal));
        table.Columns.Add("SGSTAmt", typeof(decimal));
        table.Columns.Add("UserID", typeof(int));
        table.Columns.Add("IPAddr", typeof(string));

        int rcmRowNo = 1;

        foreach (DataRow item in table.Rows)
        {
            item["OrgID"] = GlobalSession.OrgID;
            item["MonthID"] = Convert.ToInt32(ddlMonth.SelectedValue);
            item["YearID"] = Convert.ToInt32(ddlYear.SelectedItem.Text);
            item["GSTIN"] = "";
            item["SpecificAmt"] = VsdtDateWise.Compute("Sum(Specific)", "TaxRate = '" + item["TaxRate"] + "'");
            decimal CGSTSGST = CommonCls.ConvertDecimalZero(item["SpecificAmt"].ToString()) / 2;
            item["CGSTAmt"] = item["SGSTAmt"] = CGSTSGST;
            item["UserID"] = rcmRowNo;
            item["IPAddr"] = GlobalSession.IP;
            rcmRowNo++;
        }
        rcmRowNo = 1;

        table.DefaultView.Sort = "TaxRate Asc";

        table.Columns["OrgID"].SetOrdinal(0);
        table.Columns["MonthID"].SetOrdinal(1);
        table.Columns["YearID"].SetOrdinal(2);
        table.Columns["GSTIN"].SetOrdinal(3);
        table.Columns["TaxRate"].SetOrdinal(4);
        table.Columns["SpecificAmt"].SetOrdinal(5);
        table.Columns["CGSTAmt"].SetOrdinal(6);
        table.Columns["SGSTAmt"].SetOrdinal(7);
        table.Columns["UserID"].SetOrdinal(8);
        table.Columns["IPAddr"].SetOrdinal(9);

        for (int j = 1; j < table.Rows.Count; j++)
        {
            if (CommonCls.ConvertDecimalZero(table.Rows[j]["TaxRate"].ToString()) == 0)
            {
                table.Rows[j].Delete();
                j--;
            }
        }

        gvRCMRateWise.DataSource = VsdtRateWise = table;
        gvRCMRateWise.DataBind();

        lblRateWiseSpecificTotalAmount.Text = table.Compute("Sum(SpecificAmt)", "").ToString();
        lblCGSTTotalAmount.Text = table.Compute("Sum(CGSTAmt)", "").ToString();
        lblSGSTTotalAmount.Text = table.Compute("Sum(SGSTAmt)", "").ToString();

        pnlStepChange.Visible = true;
        btnPrevious.Enabled = true;
    }

    DataTable DtRCMLiabilitySchema()
    {
        DataTable dtRCMLiability = new DataTable();

        dtRCMLiability.Columns.Add("OrgID", typeof(int));
        dtRCMLiability.Columns.Add("MonthID", typeof(int));
        dtRCMLiability.Columns.Add("YearID", typeof(int));
        dtRCMLiability.Columns.Add("GSTIN", typeof(string));

        dtRCMLiability.Columns.Add("TaxRate", typeof(decimal));
        dtRCMLiability.Columns.Add("SpecificAmt", typeof(decimal));
        dtRCMLiability.Columns.Add("CGSTAmt", typeof(decimal));
        dtRCMLiability.Columns.Add("SGSTAmt", typeof(decimal));

        dtRCMLiability.Columns.Add("UserID", typeof(int));
        dtRCMLiability.Columns.Add("IPAddr", typeof(string));

        return dtRCMLiability;
    }

    protected void btnSave3_Click(object sender, EventArgs e)
    {

        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        if (VsdtRateWise == null || VsdtRateWise.Rows.Count <= 0)
        {
            ShowMessage("Purchase Voucher Detail - Tax Rate Wise Can Not Be Null.", false);
            return;
        }
        objRCMLiaModel = new RCMLiabilityModel();
        objRCMLiaModel.Ind = 2;
        objRCMLiaModel.OrgID = GlobalSession.OrgID;
        objRCMLiaModel.BRID = GlobalSession.BrID;
        objRCMLiaModel.YRCD = GlobalSession.YrCD;
        objRCMLiaModel.MonthID = Convert.ToInt32(ddlMonth.SelectedValue);
        //objRCMLiaModel.YearCode = Convert.ToInt32(ddlYear.SelectedValue);
        objRCMLiaModel.YearID = Convert.ToInt32(ddlYear.SelectedItem.Text);
        objRCMLiaModel.GSTIN = "";

        objRCMLiaModel.DtRCM = VsdtRateWise;

        string uri = string.Format("RCMLiability/SaveRCMLiability");
        DataTable dtSave = CommonCls.ApiPostDataTable(uri, objRCMLiaModel);
        if (dtSave.Rows.Count > 0)
        {
            if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
            {
                ClearAll();

                ShowMessage("Record Submited Successfully.", true);
                MultView1.ActiveViewIndex = 0;
                btnPrevious.Visible = false;
            }
            else if (dtSave.Rows[0]["ReturnInd"].ToString() == "0")
            {
                ShowMessage("Record Not Submited, Please Try Again.", true);
            }
        }
    }

    #endregion

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        MultView1.ActiveViewIndex--;
        if (MultView1.ActiveViewIndex == 0)
        {
            pnlStepChange.Visible = false;
        }
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        MultView1.ActiveViewIndex++;
    }
}