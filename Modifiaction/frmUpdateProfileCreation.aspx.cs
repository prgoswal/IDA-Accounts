using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdateProfileCreation : System.Web.UI.Page
{
    UpdateProfileCreationModel objUPCModel;

    public DataTable VSTerms
    {
        get { return (DataTable)ViewState["dtTerms"]; }
        set { ViewState["dtTerms"] = value; }
    }

    public DataTable VSDTCompositionCategory
    {
        get { return (DataTable)ViewState["dtCompoCateg"]; }
        set { ViewState["dtCompoCateg"] = value; }
    }

    public DataTable VsDtProfileCreationInfo
    {
        get { return (DataTable)ViewState["dtProfileCreationInfo"]; }
        set { ViewState["dtProfileCreationInfo"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";
        if (!IsPostBack)
        {
            txtCompanyName.Focus();
            LoadProfileCreationDDL();
            LoadProfileCreationInfo();
            ddlColumnNo_SelectedIndexChanged(sender, e);
        }
    }

    void LoadProfileCreationInfo()
    {
        try
        {
            objUPCModel = new UpdateProfileCreationModel();
            objUPCModel.Ind = 3;
            objUPCModel.CompanyID = GlobalSession.OrgID;

            string uri = string.Format("UpdateProfileCreation/LoadProfileCreationInfo");
            DataSet dsUPC = CommonCls.ApiPostDataSet(uri, objUPCModel);
            if (dsUPC.Tables.Count > 0)
            {
                DataTable dtPCInfo = VsDtProfileCreationInfo = dsUPC.Tables["ProfileCreationInfo"];

                txtCompanyName.Text = dtPCInfo.Rows[0]["CompanyName"].ToString();
                txtShortName.Text = dtPCInfo.Rows[0]["CompanyShortName"].ToString();
                ddlOrgType.SelectedValue = dtPCInfo.Rows[0]["OrgTypeID"].ToString();
                ddlBussiNature.SelectedValue = dtPCInfo.Rows[0]["BusinessNatureID"].ToString();
                ddlBussiType.SelectedValue = dtPCInfo.Rows[0]["BusinessTypeID"].ToString();
                txtAddress.Text = dtPCInfo.Rows[0]["Address"].ToString();
                txtCityCompany.Text = dtPCInfo.Rows[0]["City"].ToString();
                ddlStateCompany.SelectedValue = dtPCInfo.Rows[0]["StateID"].ToString();
                txtPincodeCompany.Text = dtPCInfo.Rows[0]["PinCode"].ToString();
                txtLandLineNo.Text = dtPCInfo.Rows[0]["LandlineNo"].ToString();
                txtFaxNo.Text = dtPCInfo.Rows[0]["FaxNo"].ToString();
                txtEmail.Text = dtPCInfo.Rows[0]["Email"].ToString();
                txtPanNo.Text = dtPCInfo.Rows[0]["PANNo"].ToString();
                txtTanNo.Text = dtPCInfo.Rows[0]["TANNo"].ToString();
                txtCINNo.Text = dtPCInfo.Rows[0]["CINNo"].ToString();
                txtImportExportCode.Text = dtPCInfo.Rows[0]["IECCode"].ToString();
                txtGSTIN.Text = dtPCInfo.Rows[0]["GSTIN"].ToString();
                txtRegAddress.Text = dtPCInfo.Rows[0]["RegistrationAddress"].ToString();
                txtRegDate.Text = CommonCls.ConvertDateDB(dtPCInfo.Rows[0]["RegistrationDate"].ToString());
                txtCityGSTIN.Text = dtPCInfo.Rows[0]["GSTINCity"].ToString();
                ddlStateGSTIN.SelectedValue = dtPCInfo.Rows[0]["GSTINStateID"].ToString();
                txtPincodeGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTINPinCode"].ToString() == "0" ? "" : VsDtProfileCreationInfo.Rows[0]["GSTINPinCode"].ToString();
                txtAuthorizedSign.Text = dtPCInfo.Rows[0]["AuthorizedSignatury"].ToString();
                txtAuthorizedDesi.Text = dtPCInfo.Rows[0]["SignaturyDesignation"].ToString();
                txtPersonName.Text = dtPCInfo.Rows[0]["ContactPerson"].ToString();
                txtDesiPerson.Text = dtPCInfo.Rows[0]["PersonDesignation"].ToString();
                txtEmailPerson.Text = dtPCInfo.Rows[0]["PersonEMail"].ToString();
                txtMobileNoPerson.Text = dtPCInfo.Rows[0]["PersonMobileNo"].ToString();
                txtPersonNameAlter.Text = dtPCInfo.Rows[0]["AlternateContactPerson"].ToString();
                txtDesiPersonAlter.Text = dtPCInfo.Rows[0]["AlternatePersonDesignation"].ToString();
                txtEmailPersonAlter.Text = dtPCInfo.Rows[0]["AlternatePersonEMail"].ToString();
                txtMobileNoPersonAlter.Text = dtPCInfo.Rows[0]["AlternatePersonMobileNo"].ToString();

                ddlAmount.SelectedValue = dtPCInfo.Rows[0]["BudgetAmount"].ToString();
                txtColumn1.Text = dtPCInfo.Rows[0]["HeadingColumn1"].ToString();
                txtColumn2.Text = dtPCInfo.Rows[0]["HeadingColumn2"].ToString();
                txtColumn3.Text = dtPCInfo.Rows[0]["HeadingColumn3"].ToString();
                txtColumn4.Text = dtPCInfo.Rows[0]["HeadingColumn4"].ToString();
                txtColumn5.Text = dtPCInfo.Rows[0]["HeadingColumn4"].ToString();
                ddlColumnNo.SelectedValue = dtPCInfo.Rows[0]["ColumnNumber"].ToString();

                if (dtPCInfo.Rows[0]["ColumnNumber"].ToString() == "5")
                {
                    chkColumn1.Checked = chkColumn2.Checked = chkColumn3.Checked = chkColumn4.Checked = chkColumn5.Checked = true;
                }

                if (dtPCInfo.Rows[0]["InvoiceOnPrePrinted"].ToString() == "1")
                {
                    ddlSIOnPrePrinted.SelectedValue = dtPCInfo.Rows[0]["InvoiceOnPrePrinted"].ToString();
                    cbCopyType.Enabled = true;
                }
                else
                {
                    ddlSIOnPrePrinted.SelectedValue = dtPCInfo.Rows[0]["InvoiceOnPrePrinted"].ToString();
                }

                if (dtPCInfo.Rows[0]["CompanyType"].ToString() == "1" || dtPCInfo.Rows[0]["CompanyType"].ToString() == "2")
                {
                    if (dtPCInfo.Rows[0]["CompositionOpted"].ToString() == "0")
                        ddlComposiOpted.Enabled = txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = false;
                    else
                        ddlComposiOpted.Enabled = txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = true;
                }
                else if (dtPCInfo.Rows[0]["CompanyType"].ToString() == "3")
                {
                    txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled =
                        txtPincodeGSTIN.Enabled = txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = ddlComposiOpted.Enabled =
                        txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = chkSameAsAbove.Enabled = false;
                    ddlComposiOpted.SelectedValue = "0";
                }

                ddlCompanyType.SelectedValue = dtPCInfo.Rows[0]["CompanyType"].ToString();
                ddlComposiOpted.SelectedValue = dtPCInfo.Rows[0]["CompositionOpted"].ToString();
                txtCompoEffDate.Text = CommonCls.ConvertDateDB(dtPCInfo.Rows[0]["CompositionEffectiveDate"].ToString());
                ddlCompositionCategory.SelectedValue = dtPCInfo.Rows[0]["CompositionCategoryID"].ToString();

                foreach (ListItem item in cbCopyType.Items)
                {
                    if (dtPCInfo.Rows[0]["InvoiceCopy1"].ToString() == item.Text)
                        item.Selected = true;
                    if (dtPCInfo.Rows[0]["InvoiceCopy2"].ToString() == item.Text)
                        item.Selected = true;
                    if (dtPCInfo.Rows[0]["InvoiceCopy3"].ToString() == item.Text)
                        item.Selected = true;
                    if (item.Text == "Extra1")
                    {
                        if (dtPCInfo.Rows[0]["InvoiceCopy4"].ToString() != "")
                            txtExtra1.Enabled = item.Selected = true;
                    }
                    if (item.Text == "Extra2")
                    {
                        if (dtPCInfo.Rows[0]["InvoiceCopy5"].ToString() != "")
                            txtExtra2.Enabled = item.Selected = true;
                    }
                }

                ddlStockMISecUnit.SelectedValue = dtPCInfo.Rows[0]["StcokMaintaneByMinorUnit"].ToString();

                txtExtra1.Text = dtPCInfo.Rows[0]["InvoiceCopy4"].ToString();
                txtExtra2.Text = dtPCInfo.Rows[0]["InvoiceCopy5"].ToString();
                txtNoPrintedCopy.Text = dtPCInfo.Rows[0]["InvoiceNoOfCopy"].ToString();

                txtInvoiceCaption1.Text = dtPCInfo.Rows[0]["InvoiceCaption1"].ToString();
                ddlInvoicePrint1.SelectedValue = dtPCInfo.Rows[0]["InvoicePrint1"].ToString();
                txtInvoiceCaption2.Text = dtPCInfo.Rows[0]["InvoiceCaption2"].ToString();
                ddlInvoicePrint2.SelectedValue = dtPCInfo.Rows[0]["InvoicePrint2"].ToString();
                txtInvoiceCaption3.Text = dtPCInfo.Rows[0]["InvoiceCaption3"].ToString();
                ddlInvoicePrint3.SelectedValue = dtPCInfo.Rows[0]["InvoicePrint3"].ToString();
                txtInvoiceCaption4.Text = dtPCInfo.Rows[0]["InvoiceCaption4"].ToString();
                ddlInvoicePrint4.SelectedValue = dtPCInfo.Rows[0]["InvoicePrint4"].ToString();
                txtInvoiceCaption5.Text = dtPCInfo.Rows[0]["InvoiceCaption5"].ToString();
                ddlInvoicePrint5.SelectedValue = dtPCInfo.Rows[0]["InvoicePrint5"].ToString();

                if (dtPCInfo.Rows[0]["TurnoverID"].ToString() == "1")
                    ddlPrintTurnover.Enabled = true;
                else
                    ddlPrintTurnover.Enabled = false;

                ddlTurnover.SelectedValue = dtPCInfo.Rows[0]["TurnoverID"].ToString();
                ddlPrintTurnover.SelectedValue = dtPCInfo.Rows[0]["PrintHSNSACCode"].ToString();

                txtBankName.Text = dtPCInfo.Rows[0]["BankName"].ToString();
                txtIFSCCode.Text = dtPCInfo.Rows[0]["IFSCCode"].ToString();
                txtAccountNumber.Text = dtPCInfo.Rows[0]["AccountNumber"].ToString();

                if (dtPCInfo.Rows[0]["BankPayChqSeriesInd"].ToString() == "True")
                    ddlChequeSeriesApplicable.SelectedValue = "1";
                else
                    ddlChequeSeriesApplicable.SelectedValue = "0";

                if (dtPCInfo.Rows[0]["SSIsTaken"].ToString() == "1")
                {
                    ChkSsTaken.Checked = true;
                }
                else
                {
                    ChkSsTaken.Checked = false;
                }

                if (dtPCInfo.Rows[0]["BSObtain"].ToString() == "1")
                {
                    ChkBsObtain.Checked = true;
                }
                else
                {
                    ChkBsObtain.Checked = false;
                }

                if (dtPCInfo.Rows[0]["CCCode"].ToString() == "1")
                {

                    ChkCostCenter.Checked = true;
                }
                else
                {
                    ChkCostCenter.Checked = false;
                }

                if (dtPCInfo.Rows[0]["BudgetConcept"].ToString() == "1")
                {

                    ChkBudget.Checked = true;
                }
                else
                {
                    ChkBudget.Checked = false;
                }

                imgCompanyLogo.ImageUrl = lblImageName.Text = dtPCInfo.Rows[0]["CompanyLogo"].ToString();

                foreach (GridViewRow grdRow in grdReportFormats.Rows)
                {
                    string formatID = ((Label)grdRow.FindControl("lblFormatID")).Text;
                    if (formatID == dtPCInfo.Rows[0]["ReportFormat"].ToString())
                    {
                        CheckBox chkSelectOnce = (CheckBox)grdRow.FindControl("chkSelectOnce");
                        chkSelectOnce.Checked = true;
                    }
                }

                DataTable dtTerms = dsUPC.Tables["Terms"];
                dtTerms.Columns.Add("UserID", typeof(int));
                dtTerms.Columns.Add("IP", typeof(string));

                foreach (DataRow row in dtTerms.Rows)
                {
                    row["UserID"] = GlobalSession.UserID;
                    row["IP"] = GlobalSession.IP;
                }

                gvTermsCon.DataSource = VSTerms = dtTerms;
                gvTermsCon.DataBind();

                if (dsUPC.Tables[3].Rows.Count > 0)
                {
                    ddlSeriesType.SelectedValue = dsUPC.Tables[3].Rows[0]["SeriesTypeInd"].ToString();
                    gvCreateSeries.DataSource = VsDtSeries = dsUPC.Tables[3];
                    gvCreateSeries.DataBind();
                }
                else
                {
                    ShowMessage("You have not any Series.", false);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void LoadProfileCreationDDL()
    {
        try
        {
            objUPCModel = new UpdateProfileCreationModel();
            objUPCModel.Ind = 11;

            string uri = string.Format("UpdateProfileCreation/BindAllUpdProfileCreationDDL");
            DataSet dsUPC = CommonCls.ApiPostDataSet(uri, objUPCModel);
            if (dsUPC.Tables.Count > 0)
            {
                ddlOrgType.DataSource = dsUPC.Tables["OrganizationType"];
                ddlOrgType.DataTextField = "OrgTypeDesc";
                ddlOrgType.DataValueField = "OrgTypeID";
                ddlOrgType.DataBind();
                ddlOrgType.Items.Insert(0, new ListItem("-- Org Type --", "0"));

                ddlBussiNature.DataSource = dsUPC.Tables["BusinessNature"];
                ddlBussiNature.DataTextField = "BusinessDesc";
                ddlBussiNature.DataValueField = "BusinessID";
                ddlBussiNature.DataBind();
                ddlBussiNature.Items.Insert(0, new ListItem("-- Business Nature --", "0"));

                ddlBussiType.DataSource = dsUPC.Tables["BusinessType"];
                ddlBussiType.DataTextField = "BusinessTypeDesc";
                ddlBussiType.DataValueField = "BusinessTypeID";
                ddlBussiType.DataBind();
                ddlBussiType.Items.Insert(0, new ListItem("-- Business Type --", "0"));

                cbCopyType.DataSource = dsUPC.Tables["CopyType"];
                cbCopyType.DataTextField = "CopyType";
                cbCopyType.DataValueField = "CopyID";
                cbCopyType.DataBind();

                DataTable dtCompanyState = dsUPC.Tables["State"];
                DataTable dtGSTInfoState = dsUPC.Tables["State"];

                ddlStateCompany.DataSource = dtCompanyState;
                ddlStateCompany.DataTextField = "StateName";
                ddlStateCompany.DataValueField = "StateID";
                ddlStateCompany.DataBind();
                ddlStateCompany.Items.Insert(0, new ListItem("-- State --", "0"));

                ddlStateGSTIN.DataSource = dtGSTInfoState;
                ddlStateGSTIN.DataTextField = "StateName";
                ddlStateGSTIN.DataValueField = "StateID";
                ddlStateGSTIN.DataBind();
                ddlStateGSTIN.Items.Insert(0, new ListItem("-- State --", "0"));

                grdReportFormats.DataSource = dsUPC.Tables["ReportFormats"];
                grdReportFormats.DataBind();

                ddlCompositionCategory.DataSource = VSDTCompositionCategory = dsUPC.Tables["CompositionCategory"];
                ddlCompositionCategory.DataTextField = "CompositionCategoryDesc";
                ddlCompositionCategory.DataValueField = "CompositionCategoryID";
                ddlCompositionCategory.DataBind();
                ddlCompositionCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlOrgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrgType.SelectedItem.Text == "PRIVATE COMPANY" || ddlOrgType.SelectedItem.Text == "PUBLIC COMPANY")
        {
            txtCINNo.Enabled = false;
            ddlBussiNature.Focus();
        }
        else
        {
            txtCINNo.Enabled = true;
            ddlBussiNature.Focus();
        }
    }

    protected void ddlCompanyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompanyType.SelectedValue != "0")
        {
            if (ddlCompanyType.SelectedValue == "1")
            {
                txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled =
                    txtPincodeGSTIN.Enabled = txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled = true;

                if (VsDtProfileCreationInfo.Rows[0]["CompositionOpted"].ToString() == "0")
                {
                    txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = false;
                    ddlComposiOpted.Enabled = true;
                    ddlComposiOpted.SelectedValue = "0";
                }
                else
                {
                    ddlComposiOpted.Enabled = txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = true;

                    ddlComposiOpted.SelectedValue = VsDtProfileCreationInfo.Rows[0]["CompositionOpted"].ToString();
                    txtCompoEffDate.Text = CommonCls.ConvertDateDB(VsDtProfileCreationInfo.Rows[0]["CompositionEffectiveDate"].ToString());
                    ddlCompositionCategory.SelectedValue = VsDtProfileCreationInfo.Rows[0]["CompositionCategoryID"].ToString();
                }
                txtGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTIN"].ToString();
                txtRegAddress.Text = VsDtProfileCreationInfo.Rows[0]["RegistrationAddress"].ToString();
                txtRegDate.Text = CommonCls.ConvertDateDB(VsDtProfileCreationInfo.Rows[0]["RegistrationDate"].ToString());
                txtCityGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTINCity"].ToString();
                ddlStateGSTIN.SelectedValue = VsDtProfileCreationInfo.Rows[0]["GSTINStateID"].ToString();
                txtPincodeGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTINPinCode"].ToString() == "0" ? "" : VsDtProfileCreationInfo.Rows[0]["GSTINPinCode"].ToString();
                txtAuthorizedSign.Text = VsDtProfileCreationInfo.Rows[0]["AuthorizedSignatury"].ToString();
                txtAuthorizedDesi.Text = VsDtProfileCreationInfo.Rows[0]["SignaturyDesignation"].ToString();
            }
            else if (ddlCompanyType.SelectedValue == "2")
            {
                txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled =
                    txtPincodeGSTIN.Enabled = txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled = true;

                if (VsDtProfileCreationInfo.Rows[0]["CompositionOpted"].ToString() == "0")
                {
                    ddlComposiOpted.SelectedValue = "1";
                    ddlComposiOpted.Enabled = txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = true;
                }
                else
                {
                    ddlComposiOpted.Enabled = txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = true;

                    ddlComposiOpted.SelectedValue = VsDtProfileCreationInfo.Rows[0]["CompositionOpted"].ToString();
                    txtCompoEffDate.Text = CommonCls.ConvertDateDB(VsDtProfileCreationInfo.Rows[0]["CompositionEffectiveDate"].ToString());
                    ddlCompositionCategory.SelectedValue = VsDtProfileCreationInfo.Rows[0]["CompositionCategoryID"].ToString();
                }
                txtGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTIN"].ToString();
                txtRegAddress.Text = VsDtProfileCreationInfo.Rows[0]["RegistrationAddress"].ToString();
                txtRegDate.Text = CommonCls.ConvertDateDB(VsDtProfileCreationInfo.Rows[0]["RegistrationDate"].ToString());
                txtCityGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTINCity"].ToString();
                ddlStateGSTIN.SelectedValue = VsDtProfileCreationInfo.Rows[0]["GSTINStateID"].ToString();
                txtPincodeGSTIN.Text = VsDtProfileCreationInfo.Rows[0]["GSTINPinCode"].ToString() == "0" ? "" : VsDtProfileCreationInfo.Rows[0]["GSTINPinCode"].ToString();
                txtAuthorizedSign.Text = VsDtProfileCreationInfo.Rows[0]["AuthorizedSignatury"].ToString();
                txtAuthorizedDesi.Text = VsDtProfileCreationInfo.Rows[0]["SignaturyDesignation"].ToString();
            }
            else if (ddlCompanyType.SelectedValue == "3")
            {
                txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled =
                    txtPincodeGSTIN.Enabled = txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled =
                    ddlComposiOpted.Enabled = txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = false;

                ddlComposiOpted.SelectedValue = "0";

                txtCompoEffDate.Text = "";

                ddlCompositionCategory.ClearSelection();

                chkSameAsAbove.Checked = false;

                txtGSTIN.Text = txtRegAddress.Text = txtRegDate.Text = txtCityGSTIN.Text = txtPincodeGSTIN.Text =
                    txtAuthorizedDesi.Text = txtAuthorizedSign.Text = "";

                ddlStateGSTIN.ClearSelection();
            }
        }
        else
        {
            txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled =
                txtPincodeGSTIN.Enabled = txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled =
                ddlComposiOpted.Enabled = true;

            txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = false;

            txtCompoEffDate.Text = "";

            ddlCompositionCategory.ClearSelection();

            ddlComposiOpted.ClearSelection();
        }
        ddlCompanyType.Focus();
    }

    protected void chkSameAsAbove_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSameAsAbove.Checked == true)
        {
            txtRegAddress.Text = txtAddress.Text;
            txtCityGSTIN.Text = txtCityCompany.Text;
            ddlStateGSTIN.SelectedValue = ddlStateCompany.SelectedValue;
            txtPincodeGSTIN.Text = txtPincodeCompany.Text;

            txtRegAddress.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled = txtPincodeGSTIN.Enabled = false;

            txtGSTIN.Focus();
        }
        else
        {
            txtRegAddress.Text = txtCityGSTIN.Text = txtPincodeGSTIN.Text = "";
            ddlStateGSTIN.ClearSelection();

            txtRegAddress.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled = txtPincodeGSTIN.Enabled = true;

            chkSameAsAbove.Focus();
        }
    }


    protected void btnAddTerms_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        bool IsValid = ValidationBtnAddTerms();
        if (!IsValid)
        {
            return;
        }

        DataTable dtTerms = new DataTable();
        if (VSTerms == null)
        {
            dtTerms = new DataTable();
            dtTerms.Columns.Add("TermID", typeof(int));
            dtTerms.Columns.Add("Terms", typeof(string));
            dtTerms.Columns.Add("UserID", typeof(int));
            dtTerms.Columns.Add("IP", typeof(string));
        }
        else
            dtTerms = VSTerms;

        DataRow drTerms = dtTerms.NewRow();
        drTerms["TermID"] = string.IsNullOrEmpty(hfTermID.Value) ? 0 : Convert.ToInt32(hfTermID.Value);
        drTerms["Terms"] = txtTerms.Text;
        drTerms["UserID"] = GlobalSession.UserID;
        drTerms["IP"] = GlobalSession.IP;
        dtTerms.Rows.Add(drTerms);

        gvTermsCon.DataSource = VSTerms = dtTerms;
        gvTermsCon.DataBind();

        hfTermID.Value = "";
        txtTerms.Text = "";
        txtTerms.Focus();
    }

    string msgAddTerms;
    bool ValidationBtnAddTerms()
    {
        if (string.IsNullOrEmpty(txtTerms.Text))
        {
            lblMsg.Text = "Enter Terms.";
            ShowMessage(lblMsg.Text, false);
            txtTerms.Focus();
            return false;
        }
        return true;
    }

    protected void gvTermsCon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "EditRow")
        {
            DataTable dtTerms = VSTerms;
            DataRow drTerms = dtTerms.Rows[rowIndex];
            txtTerms.Text = drTerms["Terms"].ToString();
            hfTermID.Value = drTerms["TermID"].ToString();

            dtTerms.Rows.RemoveAt(rowIndex);

            gvTermsCon.DataSource = VSTerms = dtTerms;
            gvTermsCon.DataBind();
        }
        if (e.CommandName == "RemoveRow")
        {
            DataTable dtTerms = VSTerms;
            dtTerms.Rows.RemoveAt(rowIndex);
            //dtTerms.Rows[rowIndex].Delete();
            gvTermsCon.DataSource = VSTerms = dtTerms;
            gvTermsCon.DataBind();
        }
    }

    protected void ddlSIOnPrePrinted_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSIOnPrePrinted.SelectedValue != "0")
        {
            //cbCopyType.Enabled = true;
            btnCopyTypeToggle.Focus();
        }
        else
        {
            // cbCopyType.Enabled = false;
            ddlComposiOpted.Focus();
        }
    }

    protected void cbCopyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int noOfCopyCount = 0;
        foreach (ListItem item in cbCopyType.Items)
        {
            if (item.Selected)
            {
                if (item.Text == "Extra1")
                {
                    txtExtra1.Enabled = true;
                    btnCopyTypeToggle.Focus();
                }
                if (item.Text == "Extra2")
                {
                    txtExtra2.Enabled = true;
                    btnCopyTypeToggle.Focus();
                }
                noOfCopyCount = noOfCopyCount + 1;
            }
            else
            {
                if (!item.Selected)
                {
                    if (item.Text == "Extra1")
                    {
                        txtExtra1.Enabled = false;
                        txtExtra1.Text = "";
                    }
                    if (item.Text == "Extra2")
                    {
                        txtExtra2.Enabled = false;
                        txtExtra2.Text = "";
                    }
                }
            }
        }
        txtNoPrintedCopy.Text = Convert.ToString(noOfCopyCount);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { Closedrop(this); });", true);
    }

    protected void ddlComposiOpted_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlComposiOpted.SelectedValue != "0")
        {
            txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = true;
            txtCompoEffDate.Focus();
        }
        else
        {
            txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = false;
            txtCompoEffDate.Text = "";
            ddlCompositionCategory.ClearSelection();
            txtTerms.Focus();
        }
    }

    //protected void ddlSIServiceAvailable_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSIServiceAvailable.SelectedValue != "0")
    //    {
    //        txtInvioceSeries.Enabled = txtStartingNo.Enabled = true;
    //        txtInvioceSeries.Focus();
    //    }
    //    else
    //    {
    //        txtInvioceSeries.Enabled = txtStartingNo.Enabled = false;
    //        ddlSIOnPrePrinted.Focus();
    //    }
    //}

    protected void btnUploadCompanyLogo_Click(object sender, EventArgs e)
    {

        if (fuCompanyLogo.HasFile)
        {
            string folderPath = Server.MapPath("~/CompanyLogo/");

            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            string extension = Path.GetExtension(fuCompanyLogo.FileName);

            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".bmp")
            {
                if (!File.Exists(fuCompanyLogo.FileName))
                {
                    try
                    {
                        string p = lblImageName.Text;
                        string imgName = p.Substring(p.LastIndexOf("/"));
                        string final = Path.Combine(folderPath, imgName);
                        string path = Server.MapPath(final);

                        FileInfo file = new FileInfo(path);
                        if (file.Exists)//check file exsit or not
                        {
                            imgCompanyLogo.ImageUrl = null;
                            lblImageName.Text = "";
                            file.Delete();
                        }



                        fuCompanyLogo.SaveAs(MapPath("~/CompanyLogo/" + GlobalSession.OrgID + "_" + fuCompanyLogo.FileName));
                        imgCompanyLogo.ImageUrl = "~/CompanyLogo/" + GlobalSession.OrgID + "_" + fuCompanyLogo.FileName;
                        //lblUpdateImageName.Text = "http://oswalapp.centralindia.cloudapp.azure.com/CompanyLOGO/" + GlobalSession.OrgID + "_" + fuCompanyLogo.FileName;
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        fuCompanyLogo.PostedFile.InputStream.Flush();
                        fuCompanyLogo.PostedFile.InputStream.Close();
                        fuCompanyLogo.FileContent.Dispose();
                        fuCompanyLogo.Attributes.Clear();
                        fuCompanyLogo.Dispose();
                    }
                }
            }
            else
            {
                lblMsg.Text = "Invaild Image Type!";
                ShowMessage(lblMsg.Text, false);
            }
        }
    }

    protected void ddlTurnover_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTurnover.SelectedValue != "0")
        {
            if (ddlTurnover.SelectedValue == "1")
            {
                ddlPrintTurnover.Enabled = true;
                ddlPrintTurnover.ClearSelection();
            }
            else
            {
                ddlPrintTurnover.Enabled = false;
                ddlPrintTurnover.SelectedValue = "1";
            }
        }
        else
        {
            ddlPrintTurnover.Enabled = false;
            ddlPrintTurnover.ClearSelection();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        bool IsValid = ValidationBtnSave();
        if (!IsValid)
        {
            return;
        }


        bool chkUncheck = false;
        foreach (GridViewRow grdRow in grdReportFormats.Rows)
        {
            CheckBox chkSelectOnce = (CheckBox)grdRow.FindControl("chkSelectOnce");
            if (chkSelectOnce.Checked == true)
            {
                hfFormatID.Value = ((Label)grdRow.FindControl("lblFormatID")).Text;
                chkUncheck = true;
                break;
            }
        }

        objUPCModel = new UpdateProfileCreationModel();

        objUPCModel.Ind = 1;
        objUPCModel.CompanyID = GlobalSession.OrgID;
        objUPCModel.CompName = txtCompanyName.Text;
        objUPCModel.ShortName = txtShortName.Text;
        objUPCModel.OrgType = Convert.ToInt32(ddlOrgType.SelectedValue);
        objUPCModel.BusiNature = Convert.ToInt32(ddlBussiNature.SelectedValue);
        objUPCModel.BusiType = Convert.ToInt32(ddlBussiType.SelectedValue);
        objUPCModel.Addr = txtAddress.Text;
        objUPCModel.City = txtCityCompany.Text;
        objUPCModel.State = Convert.ToInt32(ddlStateCompany.SelectedValue);
        objUPCModel.Pin = Convert.ToInt32(txtPincodeCompany.Text);
        objUPCModel.Phone = txtLandLineNo.Text;
        objUPCModel.Fax = txtFaxNo.Text;
        objUPCModel.EMail = txtEmail.Text;
        objUPCModel.PAN = txtPanNo.Text;
        objUPCModel.TAN = txtTanNo.Text;
        objUPCModel.CIN = txtCINNo.Text;
        objUPCModel.IECode = txtImportExportCode.Text;
        objUPCModel.ExportCtg = 0;
        objUPCModel.ContactName = txtPersonName.Text;
        objUPCModel.ContactDesg = txtDesiPerson.Text;
        objUPCModel.ContactEmail = txtEmailPerson.Text;
        objUPCModel.ContactMobile = txtMobileNoPerson.Text;
        objUPCModel.AltPerson = txtPersonNameAlter.Text;
        objUPCModel.AltPersonDesg = txtDesiPersonAlter.Text;
        objUPCModel.AltPersonEmail = txtEmailPersonAlter.Text;
        objUPCModel.AltMobile = txtMobileNoPersonAlter.Text;
        objUPCModel.Composition = Convert.ToInt32(ddlComposiOpted.SelectedValue);
        objUPCModel.CompositionDate = CommonCls.ConvertToDate(txtCompoEffDate.Text);
        objUPCModel.GSTIN = string.IsNullOrEmpty(txtGSTIN.Text) ? "" : txtGSTIN.Text;
        objUPCModel.RegDate = string.IsNullOrEmpty(CommonCls.ConvertToDate(txtRegDate.Text)) ? "" : CommonCls.ConvertToDate(txtRegDate.Text);
        objUPCModel.RegAddr = string.IsNullOrEmpty(txtRegAddress.Text) ? "" : txtRegAddress.Text;
        objUPCModel.RegCity = string.IsNullOrEmpty(txtCityGSTIN.Text) ? "" : txtCityGSTIN.Text;
        objUPCModel.RegState = Convert.ToInt32(ddlStateGSTIN.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlStateGSTIN.SelectedValue);
        objUPCModel.RegPin = string.IsNullOrEmpty(txtPincodeGSTIN.Text) ? 0 : Convert.ToInt32(txtPincodeGSTIN.Text);
        objUPCModel.RegAuthPerson = txtAuthorizedSign.Text;
        objUPCModel.RegAuthDesg = txtAuthorizedDesi.Text;
        objUPCModel.InvoiceNoSeries = "";
        objUPCModel.InvoiceNo = 0;
        objUPCModel.YrCD = GlobalSession.YrCD;
        objUPCModel.User = GlobalSession.UserID;
        objUPCModel.IP = GlobalSession.IP;
        objUPCModel.InvoiceCaption1 = txtInvoiceCaption1.Text;
        objUPCModel.InvoicePrint1 = Convert.ToInt32(ddlInvoicePrint1.SelectedValue);
        objUPCModel.InvoiceCaption2 = txtInvoiceCaption2.Text;
        objUPCModel.InvoicePrint2 = Convert.ToInt32(ddlInvoicePrint2.SelectedValue);
        objUPCModel.InvoiceCaption3 = txtInvoiceCaption3.Text;
        objUPCModel.InvoicePrint3 = Convert.ToInt32(ddlInvoicePrint3.SelectedValue);
        objUPCModel.InvoiceCaption4 = txtInvoiceCaption4.Text;
        objUPCModel.InvoicePrint4 = Convert.ToInt32(ddlInvoicePrint4.SelectedValue);
        objUPCModel.InvoiceCaption5 = txtInvoiceCaption5.Text;
        objUPCModel.InvoicePrint5 = Convert.ToInt32(ddlInvoicePrint5.SelectedValue);

        objUPCModel.HeadingColumn1 = txtColumn1.Text;
        objUPCModel.HeadingColumn2 = txtColumn2.Text;
        objUPCModel.HeadingColumn3 = txtColumn3.Text;
        objUPCModel.HeadingColumn4 = txtColumn4.Text;
        objUPCModel.HeadingColumn5 = txtColumn5.Text;
        objUPCModel.ColumnNumber = CommonCls.ConvertIntZero(ddlColumnNo.SelectedValue);
        objUPCModel.BudgetAmount = ddlAmount.SelectedValue;


        objUPCModel.CompanyLogo = imgCompanyLogo.ImageUrl.Substring(imgCompanyLogo.ImageUrl.LastIndexOf("/") + 1);
        objUPCModel.DtSeries = VsDtSeries;

        if (cbCopyType.Items[0].Selected)
        {
            objUPCModel.InvoiceCopy1Header = cbCopyType.Items[0].Text;
        }
        if (cbCopyType.Items[1].Selected)
        {
            objUPCModel.InvoiceCopy2Header = cbCopyType.Items[1].Text;
        }
        if (cbCopyType.Items[2].Selected)
        {
            objUPCModel.InvoiceCopy3Header = cbCopyType.Items[2].Text;
        }
        if (cbCopyType.Items[3].Selected)
        {
            objUPCModel.InvoiceCopy4Header = txtExtra1.Text;
        }
        if (cbCopyType.Items[4].Selected)
        {
            objUPCModel.InvoiceCopy5Header = txtExtra2.Text;
        }


        objUPCModel.InvoiceOnPrePrinted = Convert.ToInt32(ddlSIOnPrePrinted.SelectedValue);
        objUPCModel.InvoiceCopyNo = string.IsNullOrEmpty(txtNoPrintedCopy.Text) ? 0 : Convert.ToInt32(txtNoPrintedCopy.Text);

        objUPCModel.CompositionCategoryID = ddlCompositionCategory.SelectedItem == null ? 0 : Convert.ToInt32(ddlCompositionCategory.SelectedValue);

        objUPCModel.UnRegistered = ddlCompanyType.SelectedValue == "1" ? 0 : ddlCompanyType.SelectedValue == "2" ? 0 : 1;
        objUPCModel.CompanyType = Convert.ToInt32(ddlCompanyType.SelectedValue);
        objUPCModel.TurnoverID = Convert.ToInt32(ddlTurnover.SelectedValue);
        objUPCModel.TurnoverDescription = ddlTurnover.SelectedItem.Text;
        objUPCModel.PrintHSNSACCode = Convert.ToInt32(ddlPrintTurnover.SelectedValue);

        objUPCModel.BankName = txtBankName.Text;
        objUPCModel.IFSCCode = txtIFSCCode.Text;
        objUPCModel.AccountNumber = txtAccountNumber.Text;

        objUPCModel.BankPayChqSeriesInd = CommonCls.ConvertIntZero(ddlChequeSeriesApplicable.SelectedValue);

        if (VSDTCompositionCategory != null) // First Time DataTable Create For Grid
        {
            DataRow[] row = VSDTCompositionCategory.Select("CompositionCategoryID=" + Convert.ToInt32(ddlCompositionCategory.SelectedValue));
            if (row.Count() > 0)
            {
                objUPCModel.CompositionTaxRate = CommonCls.ConvertDecimalZero(row[0]["CompositionTaxRate"]);
            }
        }
        objUPCModel.StcokMaintaneByMinorUnit = Convert.ToInt32(ddlStockMISecUnit.SelectedValue);

        if (chkUncheck == true)
        {
            objUPCModel.ReportFormat = Convert.ToInt32(hfFormatID.Value);
        }
        else
        {
            lblMsg.Text = "Choose Invoice Format!";
            ShowMessage(lblMsg.Text, false);
            return;
        }

        if (VSTerms != null && VSTerms.Rows.Count > 0)
        {
            objUPCModel.DtTerms = VSTerms;
            if (objUPCModel.DtTerms.Columns.Contains("TermID"))
            {
                objUPCModel.DtTerms.Columns.Remove("TermID");
            }
        }
        else
        {
            DataTable dtTerms = new DataTable();
            dtTerms.Columns.Add("Terms", typeof(string));
            dtTerms.Columns.Add("UserID", typeof(int));
            dtTerms.Columns.Add("IP", typeof(string));

            DataRow drTerms = dtTerms.NewRow();
            drTerms["Terms"] = "";
            drTerms["UserID"] = GlobalSession.UserID;
            drTerms["IP"] = GlobalSession.IP;
            dtTerms.Rows.Add(drTerms);
            objUPCModel.DtTerms = dtTerms;
        }
        if (ChkSsTaken.Checked == true)
        {
            objUPCModel.SSIsTaken = 1;
        }
        else
        {
            objUPCModel.SSIsTaken = 0;
        }
        if (ChkBsObtain.Checked == true)
        {
            objUPCModel.BSObtain = 1;
        }
        else
        {
            objUPCModel.BSObtain = 0;
        }
        if (ChkCostCenter.Checked == true)
        {
            objUPCModel.CCCode = 1;
            //objUPCModel.CCCode
        }
        else
        {
            objUPCModel.CCCode = 0;
        }
        if (ChkBudget.Checked == true)
        {
            objUPCModel.BudgetConcept = 1;
        }
        else
        {
            objUPCModel.BudgetConcept = 0;
        }


        string uri = string.Format("UpdateProfileCreation/UpdateCompanyProfile");
        DataTable dtUpdate = CommonCls.ApiPostDataTable(uri, objUPCModel);
        if (dtUpdate.Rows.Count > 0)
        {
            if (dtUpdate.Rows[0]["RecordID"].ToString() == "0")
            {
                ShowMessage("Record Not Save Please Try Again.", false);
            }
            else if (dtUpdate.Rows[0]["RecordID"].ToString() == "1")
            {
                lblUpdateImageName.Text = "";
                Response.Redirect("../frmSuccessfullyProfileCreation.aspx?UpdateProfileCreation=" + "Profile Update Successfully!&&UpdateOrgName=" + GlobalSession.OrgName);
            }
            else if (dtUpdate.Rows[0]["RecordID"].ToString() == "2")
            {
                ShowMessage("Duplicate Record.", false);
                txtCompanyName.Focus();
            }
        }
        else
        {
            ShowMessage("Record Not Save Please Try Again.", false);
        }

    }

    string msgSave;
    bool ValidationBtnSave()
    {
        if (VsDtSeries == null || VsDtSeries.Rows.Count <= 0)
        {
            ShowMessage("Insert Series Type.", false);
            ddlSeriesType.Focus();
            return false;
        }

        if (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 2 || CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 1)
        {
            DataRow[] drCredit = VsDtSeries.Select("CashCreditInd=0");
            if (drCredit.Count() == 0)
            {
                ShowMessage("Atlease One Series Compulsory For Credit.", false);
                txtSerialNo.Focus();
                return false;
            }
            DataRow[] drCash = VsDtSeries.Select("CashCreditInd=1");
            if (drCash.Count() == 0)
            {
                ShowMessage("Atlease One Series Compulsory For Cash.", false);
                txtSerialNo.Focus();
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtCompanyName.Text))
        {
            lblMsg.Text = "Enter Company Name.";
            ShowMessage(lblMsg.Text, false);
            txtCompanyName.Focus();
            return false;
        }
        if (ddlOrgType.SelectedValue == "0")
        {
            lblMsg.Text = "Select Org Type.";
            ShowMessage(lblMsg.Text, false);
            ddlOrgType.Focus();
            return false;
        }
        if (ddlBussiNature.SelectedValue == "0")
        {
            lblMsg.Text = "Select Business Nature.";
            ShowMessage(lblMsg.Text, false);
            ddlBussiNature.Focus();
            return false;
        }
        if (ddlBussiType.SelectedValue == "0")
        {
            lblMsg.Text = "Select Business Type.";
            ShowMessage(lblMsg.Text, false);
            ddlBussiType.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtAddress.Text))
        {
            lblMsg.Text = "Enter Address.";
            ShowMessage(lblMsg.Text, false);
            txtAddress.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtCityCompany.Text))
        {
            lblMsg.Text = "Enter City.";
            ShowMessage(lblMsg.Text, false);
            txtCityCompany.Focus();
            return false;
        }
        if (ddlStateCompany.SelectedValue == "0")
        {
            lblMsg.Text = "Select State.";
            ShowMessage(lblMsg.Text, false);
            ddlStateCompany.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtPincodeCompany.Text))
        {
            lblMsg.Text = "Enter PinCode.";
            ShowMessage(lblMsg.Text, false);
            txtPincodeCompany.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtPanNo.Text))
        {
            lblMsg.Text = "Enter Pan No.";
            ShowMessage(lblMsg.Text, false);
            txtPanNo.Focus();
            return false;
        }
        if (ddlCompanyType.SelectedItem != null)
        {
            if (ddlCompanyType.SelectedValue == "0")
            {
                lblMsg.Text = "Select Company Type.";
                ShowMessage(lblMsg.Text, false);
                ddlCompanyType.Focus();
                return false;
            }
        }
        if (ddlCompanyType.SelectedValue != "3")
        {
            if (string.IsNullOrEmpty(txtGSTIN.Text))
            {
                lblMsg.Text = "Enter GSTIN No.";
                ShowMessage(lblMsg.Text, false);
                txtGSTIN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtRegAddress.Text))
            {
                lblMsg.Text = "Enter Registration Address.";
                ShowMessage(lblMsg.Text, false);
                txtRegAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtRegDate.Text))
            {
                lblMsg.Text = "Enter Registration Date.";
                ShowMessage(lblMsg.Text, false);
                txtRegDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCityGSTIN.Text))
            {
                lblMsg.Text = "Enter GST Info City.";
                ShowMessage(lblMsg.Text, false);
                txtCityGSTIN.Focus();
                return false;
            }
            if (ddlStateGSTIN.SelectedValue == "0")
            {
                lblMsg.Text = "Select GST Info State.";
                ShowMessage(lblMsg.Text, false);
                ddlStateGSTIN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPincodeGSTIN.Text))
            {
                lblMsg.Text = "Enter GST Info PinCode.";
                ShowMessage(lblMsg.Text, false);
                txtPincodeGSTIN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAuthorizedSign.Text))
            {
                lblMsg.Text = "Enter Authorized Signatory.";
                ShowMessage(lblMsg.Text, false);
                txtAuthorizedSign.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAuthorizedDesi.Text))
            {
                lblMsg.Text = "Enter Signatory Designation.";
                ShowMessage(lblMsg.Text, false);
                txtAuthorizedDesi.Focus();
                return false;
            }
        }
        if (cbCopyType.SelectedValue == "")
        {
            lblMsg.Text = "Select Copy Type.";
            ShowMessage(lblMsg.Text, false);
            cbCopyType.Focus();
            return false;
        }

        foreach (ListItem item in cbCopyType.Items)
        {
            if (item.Selected)
            {
                if (item.Text == "Extra1")
                {
                    if (string.IsNullOrEmpty(txtExtra1.Text))
                    {
                        lblMsg.Text = "Enter Extra1.";
                        ShowMessage(lblMsg.Text, false);
                        txtExtra1.Focus();
                        return false;
                    }
                }
                if (item.Text == "Extra2")
                {
                    if (string.IsNullOrEmpty(txtExtra2.Text))
                    {
                        lblMsg.Text = "Enter Extra2.";
                        ShowMessage(lblMsg.Text, false);
                        txtExtra2.Focus();
                        return false;
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(txtNoPrintedCopy.Text))
        {
            lblMsg.Text = "Enter No. of Copies To Be Printed.";
            ShowMessage(lblMsg.Text, false);
            txtNoPrintedCopy.Focus();
            return false;
        }

        if (ddlComposiOpted.SelectedValue == "1")
        {
            if (string.IsNullOrEmpty(txtCompoEffDate.Text))
            {
                lblMsg.Text = "Enter Composition Date.";
                ShowMessage(lblMsg.Text, false);
                txtCompoEffDate.Focus();
                return false;
            }
            if (ddlCompositionCategory.SelectedValue == "0")
            {
                lblMsg.Text = "Select Composition Category.";
                ShowMessage(lblMsg.Text, false);
                ddlCompositionCategory.Focus();
                return false;
            }
        }
        if (ddlTurnover.SelectedValue == "0")
        {
            lblMsg.Text = "Select Turnover.";
            ShowMessage(lblMsg.Text, false);
            ddlTurnover.Focus();
            return false;
        }
        //if (string.IsNullOrEmpty(txtBankName.Text))
        //{
        //    lblMsg.Text = "Enter Bank Name.";
        //    ShowMessage(lblMsg.Text, false);
        //    txtBankName.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtIFSCCode.Text))
        //{
        //    lblMsg.Text = "Enter IFSC Code.";
        //    ShowMessage(lblMsg.Text, false);
        //    txtIFSCCode.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtAccountNumber.Text))
        //{
        //    lblMsg.Text = "Enter Account Number.";
        //    ShowMessage(lblMsg.Text, false);
        //    txtAccountNumber.Focus();
        //    return false;
        //}


        if (!string.IsNullOrEmpty(txtGSTIN.Text))
        {
            bool GSTINNumber = CheckGSTINNumber_Validation();
            if (GSTINNumber == false)
            {

                lblMsg.Text = "Invalid GSTIN No.";
                txtGSTIN.Focus();
                return false;
            }
        }
        return true;
    }

    private bool CheckGSTINNumber_Validation()
    {
        try
        {

            //check GSTIN Number Expression
            bool CheckGSTIN_Expression = CommonCls.validGSTIN(txtGSTIN.Text.ToUpper());
            if (CheckGSTIN_Expression == true)
            {
                if (CheckGSTIN_Expression == true && !string.IsNullOrEmpty(txtPanNo.Text.ToUpper()))
                {

                    //check GSTIN Number by Statid an panNo
                    bool CheckGSTIN_Number = CommonCls.gstinvalid(txtGSTIN.Text.ToUpper(), ddlStateGSTIN.SelectedValue, txtPanNo.Text.ToUpper());
                    if (CheckGSTIN_Number == true)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message, false);
        }
        return false;
    }
    protected void grdReportFormats_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string reportPath = Convert.ToString(e.CommandArgument);
            pnlReportFormatPdf.Visible = true;

            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            ltEmbed.Text = string.Format(embed, ResolveUrl(reportPath));
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlReportFormatPdf.Visible = false;
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }


    #region Series Creation

    public DataTable VsDtSeries
    {
        get { return (DataTable)ViewState["VsDtSeries"]; }
        set { ViewState["VsDtSeries"] = value; }
    }
    protected void btnAddSeries_Click(object sender, EventArgs e)
    {
        if (!ValidateAddSeries())
        {
            return;
        }

        if (VsDtSeries == null)
        {
            VsDtSeries = new DataTable();
            VsDtSeries.Columns.Add("CompanyID", typeof(int));
            VsDtSeries.Columns.Add("BranchID", typeof(int));
            VsDtSeries.Columns.Add("SeriesTypeInd", typeof(int));
            VsDtSeries.Columns.Add("CashCreditInd", typeof(int));
            VsDtSeries.Columns.Add("Series", typeof(string));
            VsDtSeries.Columns.Add("SerialNoManualInd", typeof(int));
            VsDtSeries.Columns.Add("SerialNo", typeof(int));
        }

        DataRow drSeries = VsDtSeries.NewRow();
        drSeries["SeriesTypeInd"] = ddlSeriesType.SelectedValue;
        if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) >= 0)
        {
            drSeries["CashCreditInd"] = CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue);
        }
        drSeries["Series"] = CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2 ? txtSeries.Text.ToUpper() : "";
        drSeries["SerialNoManualInd"] = CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 2 ? 2 : CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue);
        drSeries["SerialNo"] = CommonCls.ConvertIntZero(txtSerialNo.Text);

        VsDtSeries.Rows.Add(drSeries);
        gvCreateSeries.DataSource = VsDtSeries;
        gvCreateSeries.DataBind();

        switch (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue))
        {
            case 1:

                ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = false;
                if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == 0)
                    ddlCashCredit.SelectedValue = "1";
                else
                    ddlCashCredit.SelectedValue = "0";

                txtSerialNo.Text = "";
                txtSerialNo.Focus();
                break;

            case 2:
                txtSeries.Text = txtSerialNo.Text = "";
                ddlCashCredit.Focus();
                break;

            case 3:
                btnAddSeries.Enabled = ddlSrNoAuto.Enabled = txtSerialNo.Enabled = false;

                txtSeries.Text = txtSerialNo.Text = "";
                ddlSrNoAuto.ClearSelection();
                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 0)
                //{

                //}

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2)
                //{
                //    if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                //    {

                //    }
                //}
                break;
        }

        ddlSeriesType.Enabled = false;
    }

    void SeriesInit()
    {
        object sender = UpdatePanel1;
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "SeriesInit", "SeriesInit('#" + ddlSeriesType.ClientID + "');", true);

        //Page page = HttpContext.Current.Handler as Page;
        //ScriptManager.RegisterStartupScript(page, page.GetType(), "SeriesInit", "SeriesInit('#" + ddlSeriesType.ClientID + "');", true);
    }

    bool ValidateAddSeries()
    {
        switch (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue))
        {
            case 0:
                ShowMessage("Select Series Type.", false);
                ddlSeriesType.Focus();
                return false;


            case 1: /// Manual Series

                if (VsDtSeries != null && VsDtSeries.Rows.Count == 2)
                {
                    ShowMessage("Not Allow After Cash & Credit Add.", false);
                    ddlCashCredit.Focus();
                    return false;
                }
                if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == -1)
                {
                    ShowMessage("Select Account Type", false);
                    ddlCashCredit.Focus();
                    return false;
                }

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 0)
                //{
                //    ShowMessage("Enter Serial No", false);
                //    txtSerialNo.Focus();
                //    return false;
                //}

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2)
                //{
                if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                {
                    ShowMessage("Enter Serial No", false);
                    txtSerialNo.Focus();
                    return false;
                }
                //}

                break;

            case 2: /// Available Series

                if (VsDtSeries != null && VsDtSeries.Rows.Count > 0)
                {
                    DataRow[] dr = VsDtSeries.Select("Series='" + txtSeries.Text.ToUpper() + "'");
                    if (dr.Count() > 0)
                    {
                        ShowMessage("Series Can Not Be Same.", false);
                        txtSeries.Focus();
                        return false;
                    }
                }

                if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == -1)
                {
                    ShowMessage("Select Account Type", false);
                    ddlCashCredit.Focus();
                    return false;
                }

                if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                {
                    ShowMessage("Enter Serial No", false);
                    txtSerialNo.Focus();
                    return false;
                }
                break;

            case 3: /// Default Series

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 0)
                //{
                //    ShowMessage("Enter Serial No", false);
                //    txtSerialNo.Focus();
                //    return false;
                //}

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2)
                //{
                if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                {
                    ShowMessage("Enter Serial No", false);
                    txtSerialNo.Focus();
                    return false;
                }
                //}
                break;
        }
        return true;
    }

    protected void gvCreateSeries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (Convert.ToInt16(ddlSeriesType.SelectedValue) == 3)
                {
                    Label lblCashCreditInd = (Label)e.Row.FindControl("lblCashCreditInd");
                    lblCashCreditInd.Text = "";
                }
                else
                {
                    Label lblCashCreditInd = (Label)e.Row.FindControl("lblCashCreditInd");
                    if (string.IsNullOrEmpty(lblCashCreditInd.Text) || CommonCls.ConvertIntZero(lblCashCreditInd.Text) == -1)
                        lblCashCreditInd.Text = "";
                    else if (CommonCls.ConvertIntZero(lblCashCreditInd.Text) == 0)
                        lblCashCreditInd.Text = "Credit";
                    else if (CommonCls.ConvertIntZero(lblCashCreditInd.Text) == 1)
                        lblCashCreditInd.Text = "Cash";
                }
                //else
                //{
                //    ddlCashCredit.SelectedValue = lblCashCreditInd.Text;
                //    lblCashCreditInd.Text = ddlCashCredit.SelectedItem.Text;
                //}


                Label lblSrNoAutoInd = (Label)e.Row.FindControl("lblSrNoAutoInd");
                if (CommonCls.ConvertIntZero(lblSrNoAutoInd.Text) == 0)
                    lblSrNoAutoInd.Text = "";
                else if (CommonCls.ConvertIntZero(lblSrNoAutoInd.Text) == 1)
                    lblSrNoAutoInd.Text = "Manual";
                else if (CommonCls.ConvertIntZero(lblSrNoAutoInd.Text) == 2)
                    lblSrNoAutoInd.Text = "Auto Generate";
                //else 
                //{
                //    ddlSrNoAuto.SelectedValue = lblSrNoAutoInd.Text;
                //    lblSrNoAutoInd.Text = ddlSrNoAuto.SelectedItem.Text;
                //}
            }
        }
    }

    protected void btnClearSeries_Click(object sender, EventArgs e)
    {
        ClearSeries();
    }

    void ClearSeries()
    {
        SeriesInit();
        btnAddSeries.Enabled = ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = txtSerialNo.Enabled = true;

        ddlSeriesType.Enabled = true;
        gvCreateSeries.DataSource = VsDtSeries = null;
        gvCreateSeries.DataBind();
        ddlCashCredit.ClearSelection();
        ddlSeriesType.ClearSelection();
        ddlSrNoAuto.ClearSelection();
        txtSerialNo.Text = txtSeries.Text = string.Empty;
    }

    protected void gvCreateSeries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveRow")
        {
            VsDtSeries.Rows[rowIndex].Delete();
            gvCreateSeries.DataSource = VsDtSeries;
            gvCreateSeries.DataBind();
            if (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 1)
            {
                if (VsDtSeries.Rows.Count > 0)
                {
                    if (CommonCls.ConvertIntZero(VsDtSeries.Rows[0]["CashCreditInd"]) == 0)
                        ddlCashCredit.SelectedValue = "1";
                    else
                        ddlCashCredit.SelectedValue = "0";
                }
                else
                {
                    //ClearSeries();
                    //SeriesInit();
                    ddlCashCredit.Enabled = ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = ddlSeriesType.Enabled = txtSerialNo.Enabled = true;
                }
            }
        }
        else if (e.CommandName == "EditRow")
        {
            ddlCashCredit.SelectedValue = VsDtSeries.Rows[rowIndex]["CashCreditInd"].ToString();
            ddlSrNoAuto.SelectedValue = VsDtSeries.Rows[rowIndex]["SerialNoManualInd"].ToString();
            txtSeries.Text = VsDtSeries.Rows[rowIndex]["Series"].ToString();
            txtSerialNo.Text = VsDtSeries.Rows[rowIndex]["SerialNo"].ToString();
            VsDtSeries.Rows[rowIndex].Delete();
            gvCreateSeries.DataSource = VsDtSeries;
            gvCreateSeries.DataBind();
        }
    }

    protected void ddlSeriesType_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCreateSeries.DataSource = VsDtSeries = null;
        gvCreateSeries.DataBind();
        SeriesInit();
    }

    #endregion

    protected void ddlColumnNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        divColumn1.Style.Add("display", "none");
        divColumn2.Style.Add("display", "none");
        divColumn3.Style.Add("display", "none");
        divColumn4.Style.Add("display", "none");

        if (ddlColumnNo.SelectedValue == "2")
        {
            divColumn1.Style.Add("display", "");
        }
        else if (ddlColumnNo.SelectedValue == "3")
        {
            divColumn1.Style.Add("display", "");
            divColumn2.Style.Add("display", "");
        }
        else if (ddlColumnNo.SelectedValue == "4" || ddlColumnNo.SelectedValue == "5")
        {
            divColumn1.Style.Add("display", "");
            divColumn2.Style.Add("display", "");
            divColumn3.Style.Add("display", "");
            divColumn4.Style.Add("display", "");
        }


    }
}