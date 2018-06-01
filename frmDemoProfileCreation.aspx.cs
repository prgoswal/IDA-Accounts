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
    ProfileCreationModel objPCModel;

    public DataTable VSTerms
    {
        get { return (DataTable)ViewState["dtTerms"]; }
        set { ViewState["dtTerms"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompanyName.Focus();
            LoadProfileCreationDDL();
        }
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
                //cbCopyType.Items.Insert(0, new ListItem("-- Copy Type --", "0"));

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
                //CreateReportFormatsGrid(dsProfileCreation.Tables["ReportFormats"]);
            }
        }
        catch (Exception ex)
        {

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

    bool GstInValid()
    {
        string stateValue;

        if (ddlStateCompany.SelectedValue.Length == 1)
        {
            stateValue = "0" + Convert.ToString(ddlStateCompany.SelectedValue);
        }
        else
        {
            stateValue = Convert.ToString(ddlStateCompany.SelectedValue);
        }
        if (!string.IsNullOrEmpty(txtGSTIN.Text))
        {
            string firstTwo = txtGSTIN.Text.Substring(0, 2);
            string nextTen = txtGSTIN.Text.Substring(2, 10).ToUpper();

            if (stateValue != firstTwo)
            {
                lblMsg.Text = "Invalid GSTIN No.";
                return false;
            }
            if (txtPanNo.Text != nextTen)
            {
                lblMsg.Text = "Matching With Pan No.";
                return false;
            }
        }


        return true;
    }

    protected void btnAddTerms_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        bool IsValid = ValidationBtnAddTerms();
        if (!IsValid)
        {
            lblMsg.Text = msgAddTerms;
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
            msgAddTerms = lblMsg.Text = "Enter Terms.";
            txtTerms.Focus();
            return false;
        }
        return true;
    }

    protected void gvTermsCon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
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
            cbCopyType.Enabled = true;
            btnCopyTypeToggle.Disabled = false;
            btnCopyTypeToggle.Focus();
        }
        else
        {
            cbCopyType.Enabled = false;
            btnCopyTypeToggle.Disabled = true;
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
            txtCompoEffDate.Enabled = true;
            txtCompoEffDate.Focus();
        }
        else
        {
            txtCompoEffDate.Enabled = false;
            txtTerms.Focus();
        }
    }

    protected void ddlSIServiceAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSIServiceAvailable.SelectedValue != "0")
        {
            txtInvioceSeries.Enabled = txtStartingNo.Enabled = true;
            txtInvioceSeries.Focus();
        }
        else
        {
            txtInvioceSeries.Enabled = txtStartingNo.Enabled = false;
            ddlSIOnPrePrinted.Focus();
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
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        bool IsValid = ValidationBtnSave();
        if (!IsValid)
        {
            lblMsg.Text = msgSave;
            return;
        }

        if (!GstInValid())
        {
            txtGSTIN.Focus();
            return;
        }

        bool chkUncheck = false;
        //foreach (GridViewRow grdRow in grdReportFormats.Rows)
        //{
        //    CheckBox chkSelectOnce = (CheckBox)grdRow.FindControl("chkSelectOnce");
        //    if (chkSelectOnce.Checked == true)
        //    {
    //    hfFormatID.Value = "1";            //((Label)grdRow.FindControl("lblFormatID")).Text;
        //        chkUncheck = true;
        //        break;
        //    }
        //}

        objPCModel = new ProfileCreationModel();

        objPCModel.Ind = 1;
        objPCModel.CompName = txtCompanyName.Text;
        objPCModel.ShortName = txtShortName.Text;
        objPCModel.OrgType = Convert.ToInt32(ddlOrgType.SelectedValue);
        objPCModel.BusiNature = Convert.ToInt32(ddlBussiNature.SelectedValue);
        objPCModel.BusiType = Convert.ToInt32(ddlBussiType.SelectedValue);
        objPCModel.Addr = txtAddress.Text + " Demo Version";
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
        objPCModel.GSTIN = txtGSTIN.Text;
        objPCModel.RegDate = CommonCls.ConvertToDate(txtRegDate.Text);
        objPCModel.RegAddr = txtRegAddress.Text;
        objPCModel.RegCity = txtCityGSTIN.Text;
        objPCModel.RegState = Convert.ToInt32(ddlStateGSTIN.SelectedValue);
        objPCModel.RegPin = txtPincodeGSTIN.Text == "" ? 0 : Convert.ToInt32(txtPincodeGSTIN.Text);
        objPCModel.RegAuthPerson = txtAuthorizedSign.Text;
        objPCModel.RegAuthDesg = txtAuthorizedDesi.Text;
        objPCModel.InvoiceNoSeries = txtInvioceSeries.Text;
        objPCModel.InvoiceNo = string.IsNullOrEmpty(txtStartingNo.Text) ? 0 : Convert.ToInt32(txtStartingNo.Text);
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
        objPCModel.CompanyLogo = lblImageName.Text;

        //foreach (ListItem item in cbCopyType.Items)
        //{
        //    if (item.Selected)
        //    {
        //        if (item.Text == "Customer Copy")
        //        {
        //            hfCustomerCopy.Value = item.Text;
        //        }
        //        if (item.Text == "Business Copy")
        //        {
        //            hfBusinessCopy.Value = item.Text;
        //        }
        //        if (item.Text == "Duplicate Copy")
        //        {
        //            hfDuplicateCopy.Value = item.Text;
        //        }

        //    }
        //}


        objPCModel.InvoiceCopy1Header = "Customer Copy";

        //if (cbCopyType.Items[1].Selected)
        //{
        //    objPCModel.InvoiceCopy2Header = cbCopyType.Items[1].Text;
        //}
        //if (cbCopyType.Items[2].Selected)
        //{
        //    objPCModel.InvoiceCopy3Header = cbCopyType.Items[2].Text;
        //}
        //if (cbCopyType.Items[3].Selected)
        //{
        //    objPCModel.InvoiceCopy4Header = txtExtra1.Text;
        //}
        //if (cbCopyType.Items[4].Selected)
        //{
        //    objPCModel.InvoiceCopy5Header = txtExtra2.Text;
        //}


        objPCModel.InvoiceOnPrePrinted = Convert.ToInt32(ddlSIOnPrePrinted.SelectedValue);
        objPCModel.InvoiceCopyNo = string.IsNullOrEmpty(txtNoPrintedCopy.Text) ? 0 : Convert.ToInt32(txtNoPrintedCopy.Text);
        objPCModel.ReportFormat = 1;
        //objPCModel.InvoiceCopy1Header = hfCustomerCopy.Value;
        //objPCModel.InvoiceCopy2Header = hfBusinessCopy.Value;
        //objPCModel.InvoiceCopy3Header = hfDuplicateCopy.Value;
        //objPCModel.InvoiceCopy4Header = txtExtra1.Text;
        //objPCModel.InvoiceCopy5Header = txtExtra2.Text;

        //if (chkUncheck == true)
        //{
        //    objPCModel.ReportFormat = Convert.ToInt32(hfFormatID.Value);
        //}
        //else
        //{
        //    lblMsg.Text = "Choose Invoice Format!";
        //    return;
        //}

        if (VSTerms != null)
        {
            objPCModel.DtTerms = VSTerms;//(DataTable)ViewState["dtTerms"];
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
            }
            else if (dtSave.Rows[0]["RecordID"].ToString() == "1")
            {
                ClearAll();
                Response.Redirect("frmSuccessfullyProfileCreation.aspx?ProfileCreation=" + "Profile Create Successfully!&&OrgName=" + GlobalSession.OrgName);
            }
            else if (dtSave.Rows[0]["RecordID"].ToString() == "2")
            {
                lblMsg.Text = "Duplicate Record.";
                txtCompanyName.Focus();
            }

        }
        else
        {
            lblMsg.Text = "Record Not Save Please Try Again.";
        }

    }

    string msgSave;
    bool ValidationBtnSave()
    {
        if (string.IsNullOrEmpty(txtCompanyName.Text))
        {
            msgSave = lblMsg.Text = "Enter Company Name.";
            txtCompanyName.Focus();
            return false;
        }
        if (ddlOrgType.SelectedValue == "0")
        {
            msgSave = lblMsg.Text = "Select Org Type.";
            ddlOrgType.Focus();
            return false;
        }
        if (ddlBussiNature.SelectedValue == "0")
        {
            msgSave = lblMsg.Text = "Select Business Nature.";
            ddlBussiNature.Focus();
            return false;
        }
        if (ddlBussiType.SelectedValue == "0")
        {
            msgSave = lblMsg.Text = "Select Business Type.";
            ddlBussiType.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtAddress.Text))
        {
            msgSave = lblMsg.Text = "Enter Address.";
            txtAddress.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtCityCompany.Text))
        {
            msgSave = lblMsg.Text = "Enter City.";
            txtCityCompany.Focus();
            return false;
        }
        if (ddlStateCompany.SelectedValue == "0")
        {
            msgSave = lblMsg.Text = "Select State.";
            ddlStateCompany.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtPincodeCompany.Text))
        {
            msgSave = lblMsg.Text = "Enter PinCode.";
            txtPincodeCompany.Focus();
            return false;
        }
        //if (string.IsNullOrEmpty(txtPanNo.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter Pan No.";
        //    txtPanNo.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtGSTIN.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter GSTIN No.";
        //    txtGSTIN.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtRegAddress.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter Registration Address.";
        //    txtRegAddress.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtRegDate.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter Registration Date.";
        //    txtRegDate.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtCityGSTIN.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter GST Info City.";
        //    txtCityGSTIN.Focus();
        //    return false;
        //}
        //if (ddlStateGSTIN.SelectedValue == "0")
        //{
        //    msgSave = lblMsg.Text = "Select GST Info State.";
        //    ddlStateGSTIN.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtPincodeGSTIN.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter GST Info PinCode.";
        //    txtPincodeGSTIN.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtAuthorizedSign.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter Authorized Signatory.";
        //    txtAuthorizedSign.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtAuthorizedDesi.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter Signatory Designation.";
        //    txtAuthorizedDesi.Focus();
        //    return false;
        //}


        if (ddlSIServiceAvailable.SelectedValue == "1")
        {
            if (string.IsNullOrEmpty(txtInvioceSeries.Text))
            {
                msgSave = lblMsg.Text = "Enter Invoice Series.";
                txtInvioceSeries.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtStartingNo.Text))
            {
                msgSave = lblMsg.Text = "Enter Starting No.";
                txtStartingNo.Focus();
                return false;
            }
        }

        //if (cbCopyType.SelectedValue == "")
        //{
        //    msgSave = lblMsg.Text = "Select Copy Type.";
        //    cbCopyType.Focus();
        //    return false;
        //}


        //foreach (ListItem item in cbCopyType.Items)
        //{
        //    if (item.Selected)
        //    {
        //        if (item.Text == "Extra1")
        //        {
        //            if (string.IsNullOrEmpty(txtExtra1.Text))
        //            {
        //                msgSave = lblMsg.Text = "Enter Extra1.";
        //                txtExtra1.Focus();
        //                return false;
        //            }
        //        }
        //        if (item.Text == "Extra2")
        //        {
        //            if (string.IsNullOrEmpty(txtExtra2.Text))
        //            {
        //                msgSave = lblMsg.Text = "Enter Extra2.";
        //                txtExtra2.Focus();
        //                return false;
        //            }
        //        }
        //    }
        //}


        //if (string.IsNullOrEmpty(txtNoPrintedCopy.Text))
        //{
        //    msgSave = lblMsg.Text = "Enter No. of Copies To Be Printed.";
        //    txtNoPrintedCopy.Focus();
        //    return false;
        //}


        if (ddlComposiOpted.SelectedValue == "1")
        {
            if (string.IsNullOrEmpty(txtCompoEffDate.Text))
            {
                msgSave = lblMsg.Text = "Enter Composition Date.";
                txtCompoEffDate.Focus();
                return false;
            }
        }
        return true;
    }

    void ClearAll()
    {
        txtCompanyName.Text = txtCompoEffDate.Text = txtShortName.Text = txtAddress.Text = txtCityCompany.Text = txtPincodeCompany.Text = txtLandLineNo.Text =
            txtFaxNo.Text = txtEmail.Text = txtPanNo.Text = txtTanNo.Text = txtCINNo.Text = txtImportExportCode.Text = "";
        ddlOrgType.ClearSelection();
        ddlBussiNature.ClearSelection();
        ddlBussiType.ClearSelection();
        ddlStateCompany.ClearSelection();

        txtGSTIN.Text = txtRegAddress.Text = txtRegDate.Text = txtCityGSTIN.Text = txtPincodeGSTIN.Text = txtAuthorizedSign.Text = txtAuthorizedDesi.Text = "";
        ddlStateGSTIN.ClearSelection();

        txtPersonName.Text = txtDesiPerson.Text = txtEmailPerson.Text = txtMobileNoPerson.Text = txtPersonNameAlter.Text =
            txtDesiPersonAlter.Text = txtEmailPersonAlter.Text = txtMobileNoPersonAlter.Text = txtInvioceSeries.Text = txtStartingNo.Text = "";

        ddlInventoryManag.ClearSelection();
        ddlSIServiceAvailable.ClearSelection();
        ddlSIOnPrePrinted.ClearSelection();
        ddlComposiOpted.ClearSelection();
        txtCompoEffDate.Text = "";
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

        txtInvioceSeries.Enabled = txtStartingNo.Enabled = txtNoPrintedCopy.Enabled = cbCopyType.Enabled = txtCompoEffDate.Enabled = false;

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
}