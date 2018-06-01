using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmProfileCreation : System.Web.UI.Page
{
    #region Declaration

    ProfileCreationModel objPCModel;

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

    public DataTable VsDtGSTIN
    {
        get { return (DataTable)ViewState["dtGSTIN"]; }
        set { ViewState["dtGSTIN"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompanyName.Focus();
            LoadProfileCreationDDL();
        }

        SeriesInit();

        lblMsg.Text = "";
        lblMsg.CssClass = "";
    }

    void LoadProfileCreationDDL()
    {
        try
        {
            objPCModel = new ProfileCreationModel();
            objPCModel.Ind = 11;

            string uri = string.Format("ProfileCreation/BindAllProfileCreationDDL");
            DataSet dsProfileCreation = CommonCls.ApiPostDataSet(uri, objPCModel);
            if (dsProfileCreation.Tables.Count > 0)
            {
                ddlOrgType.DataSource = dsProfileCreation.Tables["OrganizationType"];
                ddlOrgType.DataTextField = "OrgTypeDesc";
                ddlOrgType.DataValueField = "OrgTypeID";
                ddlOrgType.DataBind();
                ddlOrgType.Items.Insert(0, new ListItem("-- Org Type --", "0"));

                ddlBussiNature.DataSource = dsProfileCreation.Tables["BusinessNature"];
                ddlBussiNature.DataTextField = "BusinessDesc";
                ddlBussiNature.DataValueField = "BusinessID";
                ddlBussiNature.DataBind();
                ddlBussiNature.Items.Insert(0, new ListItem("-- Business Nature --", "0"));

                ddlBussiType.DataSource = dsProfileCreation.Tables["BusinessType"];
                ddlBussiType.DataTextField = "BusinessTypeDesc";
                ddlBussiType.DataValueField = "BusinessTypeID";
                ddlBussiType.DataBind();
                ddlBussiType.Items.Insert(0, new ListItem("-- Business Type --", "0"));

                cbCopyType.DataSource = dsProfileCreation.Tables["CopyType"];
                cbCopyType.DataTextField = "CopyType";
                cbCopyType.DataValueField = "CopyID";
                cbCopyType.DataBind();

                DataTable dtCompanyState = dsProfileCreation.Tables["State"];
                DataTable dtGSTInfoState = dsProfileCreation.Tables["State"];

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

                grdReportFormats.DataSource = dsProfileCreation.Tables["ReportFormats"];
                grdReportFormats.DataBind();

                ddlCompositionCategory.DataSource = VSDTCompositionCategory = dsProfileCreation.Tables["CompositionCategory"];
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
                txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled = txtPincodeGSTIN.Enabled =
                    txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled = btnAddGSTINInfo.Enabled = true;

                txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = false;

                ddlComposiOpted.Enabled = true;

                ddlCompositionCategory.ClearSelection();

                ddlComposiOpted.ClearSelection();
            }
            else if (ddlCompanyType.SelectedValue == "2")
            {
                txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled = txtPincodeGSTIN.Enabled =
                    txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled = ddlComposiOpted.Enabled =
                    txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = btnAddGSTINInfo.Enabled = true;

                ddlComposiOpted.SelectedValue = "1";
            }
            else if (ddlCompanyType.SelectedValue == "3")
            {
                txtGSTIN.Enabled = txtRegAddress.Enabled = txtRegDate.Enabled = txtCityGSTIN.Enabled = ddlStateGSTIN.Enabled = txtPincodeGSTIN.Enabled =
                    txtAuthorizedDesi.Enabled = txtAuthorizedSign.Enabled = chkSameAsAbove.Enabled = ddlComposiOpted.Enabled =
                    txtCompoEffDate.Enabled = ddlCompositionCategory.Enabled = btnAddGSTINInfo.Enabled = false;

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
        lblMsg.CssClass = "";

        liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liInvoiceSetting.Attributes["class"] = "active";
        Invoicesetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnSave.Style.Add("display", "block");
        btnNext.Style.Add("display", "none");
        txtInvoiceCaption1.Focus();
        hfTabInd.Value = "5";

        bool IsValid = ValidationBtnAddTerms();
        if (!IsValid)
        {
            return;
        }

        DataTable dtTerms = new DataTable();
        if (VSTerms == null)
        {
            dtTerms = new DataTable();
            dtTerms.Columns.Add("Terms", typeof(string));
            dtTerms.Columns.Add("UserID", typeof(int));
            dtTerms.Columns.Add("IP", typeof(string));
        }
        else
            dtTerms = VSTerms;

        DataRow drTerms = dtTerms.NewRow();
        drTerms["Terms"] = txtTerms.Text;
        drTerms["UserID"] = GlobalSession.UserID;
        drTerms["IP"] = GlobalSession.IP;
        dtTerms.Rows.Add(drTerms);

        gvTermsCon.DataSource = VSTerms = dtTerms;
        gvTermsCon.DataBind();

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

            dtTerms.Rows.RemoveAt(rowIndex);

            gvTermsCon.DataSource = VSTerms = dtTerms;
            gvTermsCon.DataBind();
        }
        if (e.CommandName == "RemoveRow")
        {
            DataTable dtTerms = VSTerms;
            dtTerms.Rows[rowIndex].Delete();
            gvTermsCon.DataSource = VSTerms = dtTerms;
            gvTermsCon.DataBind();
        }
    }

    protected void ddlSIOnPrePrinted_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSIOnPrePrinted.SelectedValue != "0")
        {
            ddlComposiOpted.Focus();
        }
        else
        {
            ddlComposiOpted.Focus();
        }

        liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liOtherSetting.Attributes["class"] = "active";
        OtherSetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnNext.Style.Add("display", "block");
        btnSave.Style.Add("display", "none");
        ddlSIOnPrePrinted.Focus();
        hfTabInd.Value = "4";
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

        lblNoPrintedCopy.Text = Convert.ToString(noOfCopyCount);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { Closedrop(this); });", true);

        liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liInvoiceSetting.Attributes["class"] = "active";
        Invoicesetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnSave.Style.Add("display", "block");
        btnNext.Style.Add("display", "none");
        txtInvoiceCaption1.Focus();
        hfTabInd.Value = "5";
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

        if (liOtherSetting.Attributes["class"] == "active" || hfTabInd.Value == "4")
        {
            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liOtherSetting.Attributes["class"] = "active";
            OtherSetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            ddlComposiOpted.Focus();
            hfTabInd.Value = "4";
        }
    }

    protected void btnUploadCompanyLogo_Click(object sender, EventArgs e)
    {
        if (fuCompanyLogo.HasFile)
        {
            string folderPath = Server.MapPath("~/CompanyLogo/");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string extension = Path.GetExtension(fuCompanyLogo.FileName);
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".bmp")
            {
                if (!File.Exists(fuCompanyLogo.FileName))
                {
                    try
                    {
                        fuCompanyLogo.SaveAs(MapPath("~/CompanyLogo/" + fuCompanyLogo.FileName));
                        imgCompanyLogo.ImageUrl = "~/CompanyLogo/" + fuCompanyLogo.FileName;
                        lblImageName.Text = "~/CompanyLogo/" + fuCompanyLogo.FileName;
                    }
                    catch (Exception ex)
                    {
                        //lblMsg.Text = ex.Message;
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

        liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liInvoiceSetting.Attributes["class"] = "active";
        Invoicesetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnSave.Style.Add("display", "block");
        btnNext.Style.Add("display", "none");
        txtInvoiceCaption1.Focus();
        hfTabInd.Value = "5";
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
        ddlTurnover.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";
        bool IsValid = ValidationBtnSave();
        if (!IsValid)
        {
            if (hfValidationBtnSaveInd.Value == "1")
            {
                liGSTinfo.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liOtherSetting.Attributes["class"] = "";
                GSTinfo.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = OtherSetting.Attributes["class"] = "tab-pane fade";
                liGeneral.Attributes["class"] = "active";
                General.Attributes["class"] = "tab-pane fade in active";
                btnNext.Style.Add("display", "block");
                btnPrevious.Style.Add("display", "none");
                btnSave.Style.Add("display", "none");
                hfTabInd.Value = "1";
            }
            else if (hfValidationBtnSaveInd.Value == "2")
            {
                liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liOtherSetting.Attributes["class"] = "";
                General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = OtherSetting.Attributes["class"] = "tab-pane fade";
                liGSTinfo.Attributes["class"] = "active";
                GSTinfo.Attributes["class"] = "tab-pane fade in active";
                btnPrevious.Style.Add("display", "block");
                btnNext.Style.Add("display", "block");
                btnSave.Style.Add("display", "none");
                hfTabInd.Value = "3";
            }
            else if (hfValidationBtnSaveInd.Value == "4")
            {
                liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
                General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
                liOtherSetting.Attributes["class"] = "active";
                OtherSetting.Attributes["class"] = "tab-pane fade in active";
                btnPrevious.Style.Add("display", "block");
                btnNext.Style.Add("display", "block");
                btnSave.Style.Add("display", "none");
                hfTabInd.Value = "4";
            }
            else if (hfValidationBtnSaveInd.Value == "5")
            {
                liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
                General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
                liInvoiceSetting.Attributes["class"] = "active";
                Invoicesetting.Attributes["class"] = "tab-pane fade in active";
                btnPrevious.Style.Add("display", "block");
                btnSave.Style.Add("display", "block");
                btnNext.Style.Add("display", "none");
                hfTabInd.Value = "5";
            }
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

        objPCModel = new ProfileCreationModel();

        objPCModel.Ind = 1;
        objPCModel.CompName = txtCompanyName.Text;
        objPCModel.ShortName = txtShortName.Text;
        objPCModel.OrgType = Convert.ToInt32(ddlOrgType.SelectedValue);
        objPCModel.BusiNature = Convert.ToInt32(ddlBussiNature.SelectedValue);
        objPCModel.BusiType = Convert.ToInt32(ddlBussiType.SelectedValue);
        objPCModel.Addr = txtAddress.Text;
        objPCModel.City = txtCityCompany.Text;
        objPCModel.State = Convert.ToInt32(ddlStateCompany.SelectedValue);
        objPCModel.Pin = Convert.ToInt32(txtPincodeCompany.Text);
        objPCModel.Phone = txtLandLineNo.Text;
        objPCModel.Fax = txtFaxNo.Text;
        objPCModel.EMail = txtEmail.Text;
        objPCModel.PAN = txtPanNo.Text;
        objPCModel.TAN = txtTanNo.Text;
        objPCModel.CIN = txtCINNo.Text;
        objPCModel.IECode = txtImportExportCode.Text;
        objPCModel.ExportCtg = 0;
        objPCModel.ContactName = txtPersonName.Text;
        objPCModel.ContactDesg = txtDesiPerson.Text;
        objPCModel.ContactEmail = txtEmailPerson.Text;
        objPCModel.ContactMobile = txtMobileNoPerson.Text;
        objPCModel.AltPerson = txtPersonNameAlter.Text;
        objPCModel.AltPersonDesg = txtDesiPersonAlter.Text;
        objPCModel.AltPersonEmail = txtEmailPersonAlter.Text;
        objPCModel.AltMobile = txtMobileNoPersonAlter.Text;
        objPCModel.Composition = Convert.ToInt32(ddlComposiOpted.SelectedValue);
        objPCModel.CompositionDate = CommonCls.ConvertToDate(txtCompoEffDate.Text);
        objPCModel.GSTIN = string.IsNullOrEmpty(txtGSTIN.Text) ? "" : txtGSTIN.Text;
        objPCModel.RegDate = string.IsNullOrEmpty(CommonCls.ConvertToDate(txtRegDate.Text)) ? "" : CommonCls.ConvertToDate(txtRegDate.Text);
        objPCModel.RegAddr = string.IsNullOrEmpty(txtRegAddress.Text) ? "" : txtRegAddress.Text;
        objPCModel.RegCity = string.IsNullOrEmpty(txtCityGSTIN.Text) ? "" : txtCityGSTIN.Text;
        objPCModel.RegState = Convert.ToInt32(ddlStateGSTIN.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlStateGSTIN.SelectedValue);
        objPCModel.RegPin = string.IsNullOrEmpty(txtPincodeGSTIN.Text) ? 0 : Convert.ToInt32(txtPincodeGSTIN.Text);
        objPCModel.RegAuthPerson = txtAuthorizedSign.Text;
        objPCModel.RegAuthDesg = txtAuthorizedDesi.Text;
        objPCModel.InvoiceNoSeries = "";
        objPCModel.InvoiceNo = 0;
        objPCModel.YrCD = GlobalSession.YrCD;
        objPCModel.User = GlobalSession.UserID;
        objPCModel.IP = GlobalSession.IP;
        objPCModel.InvoiceCaption1 = txtInvoiceCaption1.Text;
        objPCModel.InvoicePrint1 = Convert.ToInt32(ddlInvoicePrint1.SelectedValue);
        objPCModel.InvoiceCaption2 = txtInvoiceCaption2.Text;
        objPCModel.InvoicePrint2 = Convert.ToInt32(ddlInvoicePrint2.SelectedValue);
        objPCModel.InvoiceCaption3 = txtInvoiceCaption3.Text;
        objPCModel.InvoicePrint3 = Convert.ToInt32(ddlInvoicePrint3.SelectedValue);
        objPCModel.InvoiceCaption4 = txtInvoiceCaption4.Text;
        objPCModel.InvoicePrint4 = Convert.ToInt32(ddlInvoicePrint4.SelectedValue);
        objPCModel.InvoiceCaption5 = txtInvoiceCaption5.Text;
        objPCModel.InvoicePrint5 = Convert.ToInt32(ddlInvoicePrint5.SelectedValue);
        objPCModel.CompanyLogo = imgCompanyLogo.ImageUrl.Substring(imgCompanyLogo.ImageUrl.LastIndexOf("/") + 1);
        objPCModel.DtSeries = VsDtSeries; // Series Table
        objPCModel.UnRegistered = ddlCompanyType.SelectedValue == "1" ? 0 : ddlCompanyType.SelectedValue == "2" ? 0 : 1;
        objPCModel.CompanyType = Convert.ToInt32(ddlCompanyType.SelectedValue);
        objPCModel.TurnoverID = Convert.ToInt32(ddlTurnover.SelectedValue);
        objPCModel.TurnoverDescription = ddlTurnover.SelectedItem.Text;
        objPCModel.PrintHSNSACCode = Convert.ToInt32(ddlPrintTurnover.SelectedValue);
        objPCModel.BankName = txtBankName.Text;
        objPCModel.IFSCCode = txtIFSCCode.Text;
        objPCModel.AccountNumber = txtAccountNumber.Text;

        if (cbCopyType.Items[0].Selected)
        {
            objPCModel.InvoiceCopy1Header = cbCopyType.Items[0].Text;
        }
        if (cbCopyType.Items[1].Selected)
        {
            objPCModel.InvoiceCopy2Header = cbCopyType.Items[1].Text;
        }
        if (cbCopyType.Items[2].Selected)
        {
            objPCModel.InvoiceCopy3Header = cbCopyType.Items[2].Text;
        }
        if (cbCopyType.Items[3].Selected)
        {
            objPCModel.InvoiceCopy4Header = txtExtra1.Text;
        }
        if (cbCopyType.Items[4].Selected)
        {
            objPCModel.InvoiceCopy5Header = txtExtra2.Text;
        }


        objPCModel.InvoiceOnPrePrinted = Convert.ToInt32(ddlSIOnPrePrinted.SelectedValue);
        objPCModel.InvoiceCopyNo = string.IsNullOrEmpty(lblNoPrintedCopy.Text) ? 0 : Convert.ToInt32(lblNoPrintedCopy.Text);

        objPCModel.CompositionCategoryID = ddlCompositionCategory.SelectedItem == null ? 0 : Convert.ToInt32(ddlCompositionCategory.SelectedValue);

        if (VSDTCompositionCategory != null) // First Time DataTable Create For Grid
        {
            DataRow[] row = VSDTCompositionCategory.Select("CompositionCategoryID=" + Convert.ToInt32(ddlCompositionCategory.SelectedValue));
            if (row.Count() > 0)
            {
                objPCModel.CompositionTaxRate = CommonCls.ConvertDecimalZero(row[0]["CompositionTaxRate"]);
            }
        }
        objPCModel.StcokMaintaneByMinorUnit = Convert.ToInt32(ddlStockMISecUnit.SelectedValue);

        if (chkUncheck == true)
        {
            objPCModel.ReportFormat = Convert.ToInt32(hfFormatID.Value);
        }
        else
        {
            lblMsg.Text = "Choose Invoice Format!";
            ShowMessage(lblMsg.Text, false);
            liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liInvoiceSetting.Attributes["class"] = "active";
            Invoicesetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnSave.Style.Add("display", "block");
            btnNext.Style.Add("display", "none");
            hfTabInd.Value = "5";
            return;
        }

        if (VSTerms != null && VSTerms.Rows.Count > 0)
        {
            objPCModel.DtTerms = VSTerms;
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
            objPCModel.DtTerms = dtTerms;
        }

        string uri = string.Format("ProfileCreation/SaveCompanyProfile");
        DataTable dtSave = CommonCls.ApiPostDataTable(uri, objPCModel);
        if (dtSave.Rows.Count > 0)
        {
            if (dtSave.Rows[0]["RecordID"].ToString() == "0")
            {
                lblMsg.Text = "Record Not Save Please Try Again.";
                ShowMessage(lblMsg.Text, false);
            }
            else if (dtSave.Rows[0]["RecordID"].ToString() == "1")
            {
                ClearAll();
                Response.Redirect("frmSuccessfullyProfileCreation.aspx?ProfileCreation=" + "Profile Create Successfully!&&OrgName=" + GlobalSession.OrgName);
            }
            else if (dtSave.Rows[0]["RecordID"].ToString() == "2")
            {
                lblMsg.Text = "Duplicate Record.";
                ShowMessage(lblMsg.Text, false);
                txtCompanyName.Focus();
            }
        }
        else
        {
            lblMsg.Text = "Record Not Save Please Try Again.";
            ShowMessage(lblMsg.Text, false);
            liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liInvoiceSetting.Attributes["class"] = "active";
            Invoicesetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnSave.Style.Add("display", "block");
            btnNext.Style.Add("display", "none");
            hfTabInd.Value = "5";
        }
    }

    bool ValidationBtnSave()
    {
        //General Information
        if (string.IsNullOrEmpty(txtCompanyName.Text))
        {
            lblMsg.Text = "Enter Company Name.";
            ShowMessage(lblMsg.Text, false);
            txtCompanyName.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (txtCompanyName.Text.Length < 8)
        {
            lblMsg.Text = "Please Enter Atleast 8 Digit Company Name.";
            ShowMessage(lblMsg.Text, false);
            txtCompanyName.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (ddlOrgType.SelectedValue == "0")
        {
            lblMsg.Text = "Select Org Type.";
            ShowMessage(lblMsg.Text, false);
            ddlOrgType.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (ddlBussiNature.SelectedValue == "0")
        {
            lblMsg.Text = "Select Business Nature.";
            ShowMessage(lblMsg.Text, false);
            ddlBussiNature.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (ddlBussiType.SelectedValue == "0")
        {
            lblMsg.Text = "Select Business Type.";
            ShowMessage(lblMsg.Text, false);
            ddlBussiType.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (string.IsNullOrEmpty(txtAddress.Text))
        {
            lblMsg.Text = "Enter Address.";
            ShowMessage(lblMsg.Text, false);
            txtAddress.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (string.IsNullOrEmpty(txtCityCompany.Text))
        {
            lblMsg.Text = "Enter City.";
            ShowMessage(lblMsg.Text, false);
            txtCityCompany.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (ddlStateCompany.SelectedValue == "0")
        {
            lblMsg.Text = "Select State.";
            ShowMessage(lblMsg.Text, false);
            ddlStateCompany.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (string.IsNullOrEmpty(txtPincodeCompany.Text))
        {
            lblMsg.Text = "Enter PinCode.";
            ShowMessage(lblMsg.Text, false);
            txtPincodeCompany.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (string.IsNullOrEmpty(txtPanNo.Text))
        {
            lblMsg.Text = "Enter Pan No.";
            ShowMessage(lblMsg.Text, false);
            txtPanNo.Focus();
            hfValidationBtnSaveInd.Value = "1";
            return false;
        }
        if (ddlCompanyType.SelectedItem != null)
        {
            if (ddlCompanyType.SelectedValue == "0")
            {
                lblMsg.Text = "Select Company Type.";
                ShowMessage(lblMsg.Text, false);
                ddlCompanyType.Focus();
                hfValidationBtnSaveInd.Value = "1";
                return false;
            }
        }

        //GST Information
        if (ddlCompanyType.SelectedValue != "3")
        {
            if (VsDtGSTIN == null || VsDtGSTIN.Rows.Count <= 0)
            {
                lblMsg.Text = "GSTIN Info Can't Be Null.";
                ShowMessage(lblMsg.Text, false);
                txtGSTIN.Focus();
                hfValidationBtnSaveInd.Value = "2";
                return false;
            }
        }

        //Other Setting
        if (ddlComposiOpted.SelectedValue == "1")
        {
            if (string.IsNullOrEmpty(txtCompoEffDate.Text))
            {
                lblMsg.Text = "Enter Composition Date.";
                ShowMessage(lblMsg.Text, false);
                txtCompoEffDate.Focus();
                hfValidationBtnSaveInd.Value = "4";
                return false;
            }
            if (ddlCompositionCategory.SelectedValue == "0")
            {
                lblMsg.Text = "Select Composition Category.";
                ShowMessage(lblMsg.Text, false);
                ddlCompositionCategory.Focus();
                hfValidationBtnSaveInd.Value = "4";
                return false;
            }
        }
        if (ddlTurnover.SelectedValue == "0")
        {
            lblMsg.Text = "Select Turnover.";
            ShowMessage(lblMsg.Text, false);
            ddlTurnover.Focus();
            hfValidationBtnSaveInd.Value = "4";
            return false;
        }

        //Invoice Setting
        if (cbCopyType.SelectedValue == "")
        {
            lblMsg.Text = "Select Copy Type.";
            ShowMessage(lblMsg.Text, false);
            btnCopyTypeToggle.Focus();
            hfValidationBtnSaveInd.Value = "5";
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
                        hfValidationBtnSaveInd.Value = "5";
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
                        hfValidationBtnSaveInd.Value = "5";
                        return false;
                    }
                }
            }
        }
        if (string.IsNullOrEmpty(lblNoPrintedCopy.Text))
        {
            lblMsg.Text = "Enter No. of Copies To Be Printed.";
            ShowMessage(lblMsg.Text, false);
            lblNoPrintedCopy.Focus();
            hfValidationBtnSaveInd.Value = "5";
            return false;
        }

        if (VsDtSeries == null || VsDtSeries.Rows.Count <= 0)
        {
            ShowMessage("Insert Series Type.", false);
            ddlSeriesType.Focus();
            hfValidationBtnSaveInd.Value = "5";
            return false;
        }
        if (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 2 || CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 1)
        {
            DataRow[] drCredit = VsDtSeries.Select("CashCreditInd=0");
            if (drCredit.Count() == 0)
            {
                ShowMessage("Atlease One Series Compulsory For Credit.", false);
                ddlSrNoAuto.Focus();
                hfValidationBtnSaveInd.Value = "5";
                return false;
            }
            DataRow[] drCash = VsDtSeries.Select("CashCreditInd=1");
            if (drCash.Count() == 0)
            {
                ShowMessage("Atlease One Series Compulsory For Cash.", false);
                ddlSrNoAuto.Focus();
                hfValidationBtnSaveInd.Value = "5";
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

    void ClearAll()
    {
        ClearSeries();

        txtCompanyName.Text = txtCompoEffDate.Text = txtShortName.Text = txtAddress.Text = txtCityCompany.Text = txtPincodeCompany.Text = txtLandLineNo.Text =
            txtFaxNo.Text = txtEmail.Text = txtPanNo.Text = txtTanNo.Text = txtCINNo.Text = txtImportExportCode.Text = "";
        ddlOrgType.ClearSelection();
        ddlBussiNature.ClearSelection();
        ddlBussiType.ClearSelection();
        ddlStateCompany.ClearSelection();

        txtGSTIN.Text = txtRegAddress.Text = txtRegDate.Text = txtCityGSTIN.Text = txtPincodeGSTIN.Text = txtAuthorizedSign.Text = txtAuthorizedDesi.Text = "";
        ddlStateGSTIN.ClearSelection();

        txtPersonName.Text = txtDesiPerson.Text = txtEmailPerson.Text = txtMobileNoPerson.Text = txtPersonNameAlter.Text =
            txtDesiPersonAlter.Text = txtEmailPersonAlter.Text = txtMobileNoPersonAlter.Text = "";//= txtInvioceSeries.Text = txtStartingNo.Text 

        ddlInventoryManag.ClearSelection();
        ddlSIOnPrePrinted.ClearSelection();
        ddlComposiOpted.ClearSelection();
        lblNoPrintedCopy.Text = txtCompoEffDate.Text = "";
        cbCopyType.ClearSelection();
        txtExtra1.Text = txtExtra2.Text = "";
        txtExtra1.Enabled = txtExtra2.Enabled = false;
        btnCopyTypeToggle.Disabled = true;
        cbCopyType.ClearSelection();

        txtInvoiceCaption1.Text = txtInvoiceCaption2.Text = txtInvoiceCaption3.Text = txtInvoiceCaption4.Text = txtInvoiceCaption5.Text = "";
        ddlInvoicePrint1.ClearSelection();
        ddlInvoicePrint2.ClearSelection();
        ddlInvoicePrint3.ClearSelection();
        ddlInvoicePrint4.ClearSelection();
        ddlInvoicePrint5.ClearSelection();

        chkSameAsAbove.Checked = false;

        lblNoPrintedCopy.Enabled = cbCopyType.Enabled = txtCompoEffDate.Enabled = false;

        ViewState["dtTerms"] = null;
        gvTermsCon.DataSource = new DataTable();
        gvTermsCon.DataBind();

        foreach (GridViewRow grdRow in grdReportFormats.Rows)
        {
            CheckBox chkSelectOnce = (CheckBox)grdRow.FindControl("chkSelectOnce");
            if (chkSelectOnce.Checked == true)
            {
                chkSelectOnce.Checked = false;
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();

        string path = Server.MapPath(lblImageName.Text);
        FileInfo file = new FileInfo(path);
        if (file.Exists)//check file exsit or not
        {
            imgCompanyLogo.ImageUrl = null;
            lblImageName.Text = "";
            file.Delete();
        }

        txtCompanyName.Focus();
        lblMsg.Text = "";
        lblMsg.CssClass = "";
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
        liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liInvoiceSetting.Attributes["class"] = "active";
        Invoicesetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnSave.Style.Add("display", "block");
        btnNext.Style.Add("display", "none");
        hfTabInd.Value = "5";

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
        drSeries["CashCreditInd"] = CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue);
        drSeries["Series"] = CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2 ? txtSeries.Text.ToUpper() : "";
        drSeries["SerialNoManualInd"] = 2;
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
                btnAddSeries.Enabled = ddlSrNoAuto.Enabled = false; //= txtSerialNo.Enabled 

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
    }

    bool ValidateAddSeries()
    {
        if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
        {
            ShowMessage("Enter Serial No", false);
            txtSerialNo.Focus();
            return false;
        }

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
                //    ddlSrNoAuto.Focus();
                //    return false;
                //}

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2)
                //{
                //    if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                //    {
                //        ShowMessage("Enter Serial No", false);
                //        txtSerialNo.Focus();
                //        return false;
                //    }
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

                //if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                //{
                //    ShowMessage("Enter Serial No", false);
                //    txtSerialNo.Focus();
                //    return false;
                //}
                break;

            case 3: /// Default Series

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 0)
                //{
                //    ShowMessage("Select Serial No Type.", false);
                //    ddlSrNoAuto.Focus();
                //    return false;
                //}

                //if (CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2)
                //{
                //    if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
                //    {
                //        ShowMessage("Enter Serial No", false);
                //        txtSerialNo.Focus();
                //        return false;
                //    }
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
                Label lblCashCreditInd = (Label)e.Row.FindControl("lblCashCreditInd");
                if (string.IsNullOrEmpty(lblCashCreditInd.Text) || CommonCls.ConvertIntZero(lblCashCreditInd.Text) == -1)
                    lblCashCreditInd.Text = "";
                else
                {
                    ddlCashCredit.SelectedValue = lblCashCreditInd.Text;
                    lblCashCreditInd.Text = ddlCashCredit.SelectedItem.Text;
                }

                Label lblSrNoAutoInd = (Label)e.Row.FindControl("lblSrNoAutoInd");
                if (CommonCls.ConvertIntZero(lblSrNoAutoInd.Text) == 0)
                    lblSrNoAutoInd.Text = "";
                else
                {
                    ddlSrNoAuto.SelectedValue = lblSrNoAutoInd.Text;
                    lblSrNoAutoInd.Text = ddlSrNoAuto.SelectedItem.Text;
                }
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
        btnAddSeries.Enabled = ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = true;

        ddlSeriesType.Enabled = true;
        gvCreateSeries.DataSource = VsDtSeries = null;
        gvCreateSeries.DataBind();
        ddlCashCredit.ClearSelection();
        ddlSeriesType.ClearSelection();
        ddlSrNoAuto.ClearSelection();
        txtSerialNo.Text = txtSeries.Text = string.Empty;

        liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liInvoiceSetting.Attributes["class"] = "active";
        Invoicesetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnSave.Style.Add("display", "block");
        btnNext.Style.Add("display", "none");
        hfTabInd.Value = "5";
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
                    ddlCashCredit.Enabled = ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = ddlSeriesType.Enabled = true; //txtSerialNo.Enabled =
                }
            }
        }
        liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
        General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
        liInvoiceSetting.Attributes["class"] = "active";
        Invoicesetting.Attributes["class"] = "tab-pane fade in active";
        btnPrevious.Style.Add("display", "block");
        btnSave.Style.Add("display", "block");
        btnNext.Style.Add("display", "none");
        hfTabInd.Value = "5";
    }

    #endregion

    #region Btn Previous And Next

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string a = hfTabInd.Value;
        if (liInvoiceSetting.Attributes["class"] == "active" || hfTabInd.Value == "5")
        {
            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liOtherSetting.Attributes["class"] = "active";
            OtherSetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            hfTabInd.Value = "4";
        }
        else if (liOtherSetting.Attributes["class"] == "active" || hfTabInd.Value == "4")
        {
            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = "tab-pane fade";
            liGSTinfo.Attributes["class"] = "active";
            GSTinfo.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            hfTabInd.Value = "3";
        }
        else if (liGSTinfo.Attributes["class"] == "active" || hfTabInd.Value == "3")
        {
            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liOtherSetting.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = OtherSetting.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liContact.Attributes["class"] = "active";
            Contact.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            hfTabInd.Value = "2";
        }
        else if (liContact.Attributes["class"] == "active" || hfTabInd.Value == "2")
        {
            liGSTinfo.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = "";
            GSTinfo.Attributes["class"] = Invoicesetting.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = "tab-pane fade";
            liGeneral.Attributes["class"] = "active";
            General.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "none");
            btnSave.Style.Add("display", "none");
            btnNext.Style.Add("display", "block");
            hfTabInd.Value = "1";
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";

        if (liGeneral.Attributes["class"] == "active" || hfTabInd.Value == "1")
        {
            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                lblMsg.Text = "Enter Company Name.";
                ShowMessage(lblMsg.Text, false);
                txtCompanyName.Focus();
                return;
            }
            if (txtCompanyName.Text.Length < 8)
            {
                lblMsg.Text = "Please Enter Atleast 8 Digit Company Name.";
                ShowMessage(lblMsg.Text, false);
                txtCompanyName.Focus();
                return;
            }
            if (ddlOrgType.SelectedValue == "0")
            {
                lblMsg.Text = "Select Org Type.";
                ShowMessage(lblMsg.Text, false);
                ddlOrgType.Focus();
                return;
            }
            if (ddlBussiNature.SelectedValue == "0")
            {
                lblMsg.Text = "Select Business Nature.";
                ShowMessage(lblMsg.Text, false);
                ddlBussiNature.Focus();
                return;
            }
            if (ddlBussiType.SelectedValue == "0")
            {
                lblMsg.Text = "Select Business Type.";
                ShowMessage(lblMsg.Text, false);
                ddlBussiType.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                lblMsg.Text = "Enter Address.";
                ShowMessage(lblMsg.Text, false);
                txtAddress.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCityCompany.Text))
            {
                lblMsg.Text = "Enter City.";
                ShowMessage(lblMsg.Text, false);
                txtCityCompany.Focus();
                return;
            }
            if (ddlStateCompany.SelectedValue == "0")
            {
                lblMsg.Text = "Select State.";
                ShowMessage(lblMsg.Text, false);
                ddlStateCompany.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPincodeCompany.Text))
            {
                lblMsg.Text = "Enter PinCode.";
                ShowMessage(lblMsg.Text, false);
                txtPincodeCompany.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPanNo.Text))
            {
                lblMsg.Text = "Enter Pan No.";
                ShowMessage(lblMsg.Text, false);
                txtPanNo.Focus();
                return;
            }
            if (ddlCompanyType.SelectedItem != null)
            {
                if (ddlCompanyType.SelectedValue == "0")
                {
                    lblMsg.Text = "Select Company Type.";
                    ShowMessage(lblMsg.Text, false);
                    ddlCompanyType.Focus();
                    return;
                }
            }

            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liOtherSetting.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = OtherSetting.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liContact.Attributes["class"] = "active";
            Contact.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            hfTabInd.Value = "2";
            txtPersonName.Focus();
        }
        else if (liContact.Attributes["class"] == "active" || hfTabInd.Value == "2")
        {
            if (ddlCompanyType.SelectedValue != "3")
            {
                if (VsDtGSTIN == null || VsDtGSTIN.Rows.Count <= 0)
                {
                    ShowMessage("GSTIN Info Can't Be Null.", false);
                    txtGSTIN.Focus();
                    return;
                }
            }

            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = "tab-pane fade";
            liGSTinfo.Attributes["class"] = "active";
            GSTinfo.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            txtGSTIN.Focus();
            hfTabInd.Value = "3";
        }
        else if (liGSTinfo.Attributes["class"] == "active" || hfTabInd.Value == "3")
        {
            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liOtherSetting.Attributes["class"] = "active";
            OtherSetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            ddlInventoryManag.Focus();
            hfTabInd.Value = "4";
        }
        else if (liOtherSetting.Attributes["class"] == "active" || hfTabInd.Value == "4")
        {
            if (ddlComposiOpted.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(txtCompoEffDate.Text))
                {
                    lblMsg.Text = "Enter Composition Date.";
                    ShowMessage(lblMsg.Text, false);
                    txtCompoEffDate.Focus();
                    return;
                }
                if (ddlCompositionCategory.SelectedValue == "0")
                {
                    lblMsg.Text = "Select Composition Category.";
                    ShowMessage(lblMsg.Text, false);
                    ddlCompositionCategory.Focus();
                    return;
                }
            }
            if (ddlTurnover.SelectedValue == "0")
            {
                lblMsg.Text = "Select Turnover.";
                ShowMessage(lblMsg.Text, false);
                ddlTurnover.Focus();
                return;
            }

            liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liInvoiceSetting.Attributes["class"] = "active";
            Invoicesetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnSave.Style.Add("display", "block");
            btnNext.Style.Add("display", "none");
            txtInvoiceCaption1.Focus();
            hfTabInd.Value = "5";
        }
        else if (liOtherSetting.Attributes["class"] == "active" || hfTabInd.Value == "5")
        {
            if (cbCopyType.SelectedValue == "")
            {
                lblMsg.Text = "Select Copy Type.";
                ShowMessage(lblMsg.Text, false);
                btnCopyTypeToggle.Focus();
                return;
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
                            return;
                        }
                    }
                    if (item.Text == "Extra2")
                    {
                        if (string.IsNullOrEmpty(txtExtra2.Text))
                        {
                            lblMsg.Text = "Enter Extra2.";
                            ShowMessage(lblMsg.Text, false);
                            txtExtra2.Focus();
                            return;
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(lblNoPrintedCopy.Text))
            {
                lblMsg.Text = "Enter No. of Copies To Be Printed.";
                ShowMessage(lblMsg.Text, false);
                lblNoPrintedCopy.Focus();
                return;
            }

            if (VsDtSeries == null || VsDtSeries.Rows.Count <= 0)
            {
                ShowMessage("Insert Series Type.", false);
                ddlSeriesType.Focus();
                return;
            }
            if (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 2 || CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 1)
            {
                DataRow[] drCredit = VsDtSeries.Select("CashCreditInd=0");
                if (drCredit.Count() == 0)
                {
                    ShowMessage("Atlease One Series Compulsory For Credit.", false);
                    ddlSrNoAuto.Focus();
                    return;
                }
                DataRow[] drCash = VsDtSeries.Select("CashCreditInd=1");
                if (drCash.Count() == 0)
                {
                    ShowMessage("Atlease One Series Compulsory For Cash.", false);
                    ddlSrNoAuto.Focus();
                    return;
                }
            }

            liGeneral.Attributes["class"] = liOtherSetting.Attributes["class"] = liContact.Attributes["class"] = liGSTinfo.Attributes["class"] = "";
            General.Attributes["class"] = OtherSetting.Attributes["class"] = Contact.Attributes["class"] = GSTinfo.Attributes["class"] = "tab-pane fade";
            liInvoiceSetting.Attributes["class"] = "active";
            Invoicesetting.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnSave.Style.Add("display", "block");
            btnNext.Style.Add("display", "none");
            txtInvoiceCaption1.Focus();
            hfTabInd.Value = "5";
        }
    }

    #endregion

    #region Add GSTIN

    protected void btnAddGSTINInfo_Click(object sender, EventArgs e)
    {
        if (!ValidationAfterBTNAddGSTIN())
        {
            liGeneral.Attributes["class"] = liInvoiceSetting.Attributes["class"] = liContact.Attributes["class"] = liOtherSetting.Attributes["class"] = "";
            General.Attributes["class"] = Invoicesetting.Attributes["class"] = Contact.Attributes["class"] = OtherSetting.Attributes["class"] = "tab-pane fade";
            liGSTinfo.Attributes["class"] = "active";
            GSTinfo.Attributes["class"] = "tab-pane fade in active";
            btnPrevious.Style.Add("display", "block");
            btnNext.Style.Add("display", "block");
            btnSave.Style.Add("display", "none");
            hfTabInd.Value = "3";
            return;
        }

        BindGRDGSTIN();
        ClearAfterBTNAddGSTIN();
        txtGSTIN.Focus();
    }

    bool ValidationAfterBTNAddGSTIN()
    {
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
                lblMsg.Text = "Enter City.";
                ShowMessage(lblMsg.Text, false);
                txtCityGSTIN.Focus();
                return false;
            }
            if (ddlStateGSTIN.SelectedValue == "0")
            {
                lblMsg.Text = "Select State.";
                ShowMessage(lblMsg.Text, false);
                ddlStateGSTIN.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPincodeGSTIN.Text))
            {
                lblMsg.Text = "Enter PinCode.";
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
        return true;
    }
     
    void BindGRDGSTIN()
    {
        DataTable dtGSTINInfo = new DataTable();
        if (VsDtGSTIN == null)
        {
            dtGSTINInfo = DtGSTINSchema();
        }
        else
        {
            dtGSTINInfo = VsDtGSTIN;
        }

        DataRow DrGSTINInfo = dtGSTINInfo.NewRow();
        DrGSTINInfo["GSTIN"] = txtGSTIN.Text.ToUpper();
        DrGSTINInfo["RegistrationAddress"] = txtRegAddress.Text;
        DrGSTINInfo["RegistrationDate"] = CommonCls.ConvertToDate(txtRegDate.Text);
        DrGSTINInfo["City"] = txtCityGSTIN.Text;
        DrGSTINInfo["StateID"] = Convert.ToInt16(ddlStateGSTIN.SelectedItem.Value);
        DrGSTINInfo["PinCode"] = Convert.ToInt32(txtPincodeGSTIN.Text);
        DrGSTINInfo["AuthorizedSignatury"] = txtAuthorizedSign.Text;
        DrGSTINInfo["SignaturyDesignation"] = txtAuthorizedDesi.Text;

        dtGSTINInfo.Rows.Add(DrGSTINInfo);
        grdGSTINInfo.DataSource = VsDtGSTIN = dtGSTINInfo;
        grdGSTINInfo.DataBind();
    }

    DataTable DtGSTINSchema()
    {
        DataTable dtGSTINInfo = new DataTable();
        dtGSTINInfo.Columns.Add("GSTIN", typeof(string));
        dtGSTINInfo.Columns.Add("RegistrationAddress", typeof(string));
        dtGSTINInfo.Columns.Add("RegistrationDate", typeof(string));
        dtGSTINInfo.Columns.Add("City", typeof(string));
        dtGSTINInfo.Columns.Add("StateID", typeof(int));
        dtGSTINInfo.Columns.Add("PinCode", typeof(int));
        dtGSTINInfo.Columns.Add("AuthorizedSignatury", typeof(string));
        dtGSTINInfo.Columns.Add("SignaturyDesignation", typeof(string));

        return dtGSTINInfo;
    }

    void ClearAfterBTNAddGSTIN()
    {
        txtGSTIN.Text = txtRegAddress.Text = txtRegDate.Text = txtCityGSTIN.Text = txtPincodeGSTIN.Text = txtAuthorizedSign.Text = txtAuthorizedDesi.Text = "";
        ddlStateGSTIN.ClearSelection();
    }

    protected void grdGSTINInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdGSTINInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                DataRow dr = ((DataRowView)e.Row.DataItem).Row;

                Label lblStateID = (Label)e.Row.FindControl("lblStateID");

                if (!string.IsNullOrEmpty(lblStateID.Text))
                {
                    ddlStateGSTIN.SelectedValue = lblStateID.Text;
                    lblStateID.Text = ddlStateGSTIN.SelectedItem.Text;
                }
            }
        }
    }

    #endregion
}