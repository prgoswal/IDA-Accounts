using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdateAccountHead : System.Web.UI.Page
{
    UpdateAccounHeadModel objUpdAccountHead;

    protected void Page_Load(object sender, EventArgs e)
    {

        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ddlGroupName.Enabled = false;
            LoadAccoundHeadDDL();
            ddlAccountHead.Focus();
            LoadBranchState();
            divGSTINInfo.Visible = false;
            divShippingInfo.Visible = false;
            grdShippingInformation.Enabled = true;
            grdGSTINInformation.Enabled = true;
        }
        //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
        //{
        //    btnUpdate.Visible = true;
        //    Session["MasterWrite"] = 0;

        //}

    }

    void ClientInformationEnabledFalse()
    {
        txtClientAddress.Enabled = txtClientCity.Enabled = txtClientPincode.Enabled = txtAccountHead.Enabled =
            txtPanNo.Enabled = ddlTDS.Enabled = ddlISD.Enabled = ddlCompositionOpted.Enabled =
            ddlRCM.Enabled = ddlTCS.Enabled = txtOtherDetails.Enabled = btnUpdate.Enabled = false;

        txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = false;

        txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = false;
    }

    void ClientInformationEnabledTrue()
    {
        txtClientAddress.Enabled = txtClientCity.Enabled = txtClientPincode.Enabled = txtAccountHead.Enabled =
            txtPanNo.Enabled = ddlTDS.Enabled = ddlISD.Enabled = ddlCompositionOpted.Enabled =
            ddlRCM.Enabled = ddlTCS.Enabled = txtOtherDetails.Enabled = btnUpdate.Enabled = true;

        txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = true;

        txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = true;
    }

    void GSTINInformationEnabledTrue()
    {
        txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
            ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
            txtDesignation.Enabled = btnAddGSTIN.Enabled = btnUpdate.Enabled = true;
    }

    void GSTINInformationEnabledFalse()
    {
        txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
            ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
            txtDesignation.Enabled = btnAddGSTIN.Enabled = btnUpdate.Enabled = false;
    }

    void ShippingInformationEnabledTrue()
    {
        ddlShippingGSTIN.Enabled = txtShippingAddress.Enabled = txtShippingCity.Enabled = btnUpdate.Enabled =
            ddlShippingState.Enabled = txtShippingPincode.Enabled = btnAddShipping.Enabled = true;
    }

    void ShippingInformationEnabledFalse()
    {
        ddlShippingGSTIN.Enabled = txtShippingAddress.Enabled = txtShippingCity.Enabled = btnUpdate.Enabled =
            ddlShippingState.Enabled = txtShippingPincode.Enabled = btnAddShipping.Enabled = false;
    }

    public void LoadBranchState()
    {
        try
        {
            objUpdAccountHead = new UpdateAccounHeadModel();
            objUpdAccountHead.Ind = 8;
            objUpdAccountHead.OrgID = GlobalSession.OrgID;
            objUpdAccountHead.BrID = GlobalSession.BrID;
            objUpdAccountHead.YrCD = GlobalSession.YrCD;
            objUpdAccountHead.IsAdmin = GlobalSession.IsAdmin;

            string uri = string.Format("UpdateAccountHead/LoadBranchState");
            DataTable dtBranchState = CommonCls.ApiPostDataTable(uri, objUpdAccountHead);
            if (dtBranchState.Rows.Count > 0)
            {
                hfBranchState.Value = dtBranchState.Rows[0]["BranchState"].ToString();
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    public void LoadAccoundHeadDDL()
    {
        try
        {
            objUpdAccountHead = new UpdateAccounHeadModel();
            objUpdAccountHead.OrgID = GlobalSession.OrgID;
            objUpdAccountHead.BrID = GlobalSession.BrID;
            objUpdAccountHead.YrCD = GlobalSession.YrCD;
            objUpdAccountHead.IsAdmin = GlobalSession.IsAdmin;
            string uri = string.Format("UpdateAccountHead/BindAccountHeadDDL");
            DataSet dsAccountHead = CommonCls.ApiPostDataSet(uri, objUpdAccountHead);
            if (dsAccountHead.Tables.Count > 0)
            {
                ddlAccountHead.DataSource = dsAccountHead.Tables["AccountHead"];
                ddlAccountHead.DataTextField = "AccFilter";
                ddlAccountHead.DataValueField = "AccCode";
                ddlAccountHead.DataBind();
                ddlAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));

                DataTable dtClientState = dsAccountHead.Tables["State"];
                DataTable dtGSTINState = dsAccountHead.Tables["State"];
                DataTable dtShippingState = dsAccountHead.Tables["State"];

                ddlClientState.DataSource = dtClientState;
                ddlClientState.DataTextField = "StateName";
                ddlClientState.DataValueField = "StateID";
                ddlClientState.DataBind();
                ddlClientState.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlGSTINState.DataSource = dtGSTINState;
                ddlGSTINState.DataTextField = "StateName";
                ddlGSTINState.DataValueField = "StateID";
                ddlGSTINState.DataBind();
                ddlGSTINState.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlShippingState.DataSource = dtShippingState;
                ddlShippingState.DataTextField = "StateName";
                ddlShippingState.DataValueField = "StateID";
                ddlShippingState.DataBind();
                ddlShippingState.Items.Insert(0, new ListItem("-- Select --", "0"));

                ViewState["dsGroupName"] = ddlGroupName.DataSource = dsAccountHead.Tables["GroupName"];
                ddlGroupName.DataTextField = "AccGroupHead";
                ddlGroupName.DataValueField = "AccGroupID";
                ddlGroupName.DataBind();
                ddlGroupName.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlExportCategory.DataSource = dsAccountHead.Tables["ExportCategory"];
                ddlExportCategory.DataTextField = "CategoryDesc";
                ddlExportCategory.DataValueField = "CategoryID";
                ddlExportCategory.DataBind();
                ddlExportCategory.Items.Insert(0, new ListItem("-- Select --", "0"));

                lstBoxAccountHead.DataSource = dsAccountHead.Tables["AccountHead"];
                lstBoxAccountHead.DataTextField = "AccName";
                lstBoxAccountHead.DataValueField = "AccFilter";
                lstBoxAccountHead.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void DisplayOrNot()
    {
        if (!string.IsNullOrEmpty(txtAccountHead.Text))
        {
            tdAccountHeadHeader.Style.Add("display", "none");
            tdAccountHeadBody.Style.Add("display", "none");
            tdContactInfoHeader.Style.Add("display", "");
            tdContactAndBankBody.Style.Add("display", "");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        rbBasicInfo.Enabled = rbGSTINInfo.Enabled = rbShippingInfo.Enabled = false;
        if (ddlAccountHead.SelectedValue != "0")
        {
            DisplayOrNot();

            if (rbBasicInfo.Checked == true)
            {
                ClientInformationEnabledTrue();

                LoadBasicInfo();
                rbBasicInfo.Enabled = true;
            }
            else if (rbGSTINInfo.Checked == true)
            {
                GSTINInformationEnabledTrue();

                LoadGSTINInfo();
                rbGSTINInfo.Enabled = true;
            }
            else if (rbShippingInfo.Checked == true)
            {
                ShippingInformationEnabledTrue();

                LoadShippingInfo();
                rbShippingInfo.Enabled = true;
            }
            btnSearch.Enabled = false;
            ddlAccountHead.Enabled = false;
        }
        else
        {
            ShowMessage("Select Account Head.", false);
            rbBasicInfo.Enabled = rbGSTINInfo.Enabled = rbShippingInfo.Enabled = true;
            ddlAccountHead.Focus();
            DisplayOrNot();
        }
    }

    void LoadBasicInfo()
    {
        objUpdAccountHead = new UpdateAccounHeadModel();
        objUpdAccountHead.Ind = 1;
        objUpdAccountHead.OrgID = GlobalSession.OrgID;
        objUpdAccountHead.BrID = GlobalSession.BrID;
        objUpdAccountHead.IsAdmin = GlobalSession.IsAdmin;
        objUpdAccountHead.AccCode = Convert.ToInt64(ddlAccountHead.SelectedValue);
        string uri = string.Format("UpdateAccountHead/LoadBasicInfo");
        DataSet dsBasicInfo = CommonCls.ApiPostDataSet(uri, objUpdAccountHead);
        if (dsBasicInfo.Tables[0].Rows.Count > 0)
        {
            DataTable dtBasicInfo = dsBasicInfo.Tables[0];
            DataTable dtTerms = dsBasicInfo.Tables[1];

            ddlGroupName.SelectedValue = dtBasicInfo.Rows[0]["AccGroupID"].ToString();

            DataTable dtGroupName = (DataTable)ViewState["dsGroupName"];
            if (dtGroupName != null)
            {
                DataRow[] row = dtGroupName.Select("AccGroupID=" + Convert.ToInt32(ddlGroupName.SelectedValue));
                if (row != null)
                {
                    lblGroupDescription.Text = row[0]["GroupDesc"].ToString();
                    hfMainGroupCode.Value = row[0]["MainGroupCode"].ToString();
                    hfMainSubGroupCode.Value = row[0]["MainSubGroupCode"].ToString();
                    hfAccSubGroupID.Value = row[0]["AccSubGroupID"].ToString();
                    hfAccGICodeFrom.Value = row[0]["AccGICodeFrom"].ToString();
                    hfAccGICodeTo.Value = row[0]["AccGICodeTo"].ToString();

                    ddlDrCr.Enabled = true;

                    if (Convert.ToInt32(hfMainGroupCode.Value) > 20)
                    {
                        txtClientAddress.Enabled = txtClientCity.Enabled = ddlClientState.Enabled = txtClientPincode.Enabled =
                            txtPanNo.Enabled = ddlTDS.Enabled = ddlISD.Enabled =
                            ddlRCM.Enabled = ddlTCS.Enabled = txtMerchantID.Enabled = false;

                        txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = false;

                        txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = false;
                    }
                    else
                    {
                        txtClientAddress.Enabled = txtClientCity.Enabled = ddlClientState.Enabled = txtClientPincode.Enabled =
                            txtPanNo.Enabled = ddlTDS.Enabled = ddlISD.Enabled =
                            ddlRCM.Enabled = ddlTCS.Enabled = txtMerchantID.Enabled = true;

                        txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = true;

                        txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = true;
                    }

                    if ((Convert.ToInt32(hfAccGICodeFrom.Value) >= 100000 && Convert.ToInt32(hfAccGICodeTo.Value) < 200000)
                        || (Convert.ToInt32(hfAccGICodeFrom.Value) >= 500000 && Convert.ToInt32(hfAccGICodeTo.Value) < 600000))     //Debtors / Creditors WithIn State
                    {
                        ddlClientState.SelectedValue = hfBranchState.Value;
                        ddlClientState.Enabled = false;
                    }
                    else if ((Convert.ToInt32(hfAccGICodeFrom.Value) >= 200000 && Convert.ToInt32(hfAccGICodeTo.Value) < 250000)
                        || (Convert.ToInt32(hfAccGICodeFrom.Value) >= 600000 && Convert.ToInt32(hfAccGICodeTo.Value) < 650000))     //Debtors / Creditors OutSide State
                    {
                        ddlClientState.Items.FindByValue(hfBranchState.Value).Attributes.Add("style", "display:none");
                    }
                    else if ((Convert.ToInt32(hfAccGICodeFrom.Value) >= 250000 && Convert.ToInt32(hfAccGICodeTo.Value) < 300000)
                        || (Convert.ToInt32(hfAccGICodeFrom.Value) >= 650000 && Convert.ToInt32(hfAccGICodeTo.Value) < 660000))     //Debtors / Creditors OutSide Country
                    {
                        ddlClientState.SelectedValue = "98";
                        ddlClientState.Enabled = false;
                    }
                    else
                    {

                    }
                }
            }

            txtAccountHead.Text = dtBasicInfo.Rows[0]["AccName"].ToString();
            txtAccountHeadHindi.Text = dtBasicInfo.Rows[0]["AccNameHindi"].ToString();

            txtClientAddress.Text = dtBasicInfo.Rows[0]["Address"].ToString();
            txtClientCity.Text = dtBasicInfo.Rows[0]["City"].ToString();
            txtBrokerRate.Text = dtBasicInfo.Rows[0]["BrokerageRate"].ToString();
            txtBrokerageLimit.Text = dtBasicInfo.Rows[0]["BrokerageLimit"].ToString();

            if (dtBasicInfo.Rows[0]["BrokerageType"].ToString() == "1")
            {
                ddlBrokerageType.SelectedValue = "1";
            }
            else
            {
                ddlBrokerageType.SelectedValue = "2";
            }


            //When Group Name is Broker Within State
            if (hfMainGroupCode.Value == "11" && hfMainSubGroupCode.Value == "30445" && hfAccSubGroupID.Value == "0" && hfAccGICodeFrom.Value == "660000" && hfAccGICodeTo.Value == "660499")
            {
                trBrokerage.Visible = true;
            }
            else
            {
                if (hfMainGroupCode.Value == "11" && hfMainSubGroupCode.Value == "30445" && hfAccSubGroupID.Value == "0" && hfAccGICodeFrom.Value == "660500" && hfAccGICodeTo.Value == "660999")
                {
                    trBrokerage.Visible = true;
                }
                else
                {
                    trBrokerage.Visible = false;
                }

            }
            ddlClientState.SelectedValue = string.IsNullOrEmpty(dtBasicInfo.Rows[0]["StateID"].ToString()) ? "0" : dtBasicInfo.Rows[0]["StateID"].ToString();
            txtClientPincode.Text = dtBasicInfo.Rows[0]["PinCode"].ToString();
            txtPanNo.Text = dtBasicInfo.Rows[0]["PanNo"].ToString();
            ddlTaxCalType.SelectedValue = CommonCls.ConvertIntZero(dtBasicInfo.Rows[0]["TaxCalcForSEZParty"]).ToString();

            if (dtBasicInfo.Rows[0]["DrOpBalance"].ToString() != "0")
            {
                txtOpeningBalance.Text = dtBasicInfo.Rows[0]["DrOpBalance"].ToString();
                ddlDrCr.SelectedValue = "0";
            }
            else
            {
                txtOpeningBalance.Text = dtBasicInfo.Rows[0]["CrOpBalance"].ToString();
                ddlDrCr.SelectedValue = "1";
            }

            ddlTDS.SelectedValue = dtBasicInfo.Rows[0]["TDSApplicable"].ToString();
            ddlISD.SelectedValue = dtBasicInfo.Rows[0]["ISDApplicable"].ToString();
            ddlRCM.SelectedValue = dtBasicInfo.Rows[0]["RCMApplicable"].ToString();

            if (dtBasicInfo.Rows[0]["TCSApplicable"].ToString() != "" && dtBasicInfo.Rows[0]["TCSApplicable"].ToString() != "0")
            {
                ddlTCS.SelectedValue = dtBasicInfo.Rows[0]["TCSApplicable"].ToString();
                txtMerchantID.Text = dtBasicInfo.Rows[0]["MerchantID"].ToString();
                txtMerchantID.Enabled = true;
            }
            else
            {
                ddlTCS.SelectedValue = string.IsNullOrEmpty(dtBasicInfo.Rows[0]["TCSApplicable"].ToString()) ? "0" : dtBasicInfo.Rows[0]["TCSApplicable"].ToString();
                txtMerchantID.Text = dtBasicInfo.Rows[0]["MerchantID"].ToString();
                txtMerchantID.Enabled = false;
            }

            txtPersonName.Text = dtBasicInfo.Rows[0]["ContactPerson"].ToString();

            if (dtBasicInfo.Rows[0]["MobileNo"].ToString() == "0")
            {
                txtMobileNo.Text = "";
            }
            else
            {
                txtMobileNo.Text = dtBasicInfo.Rows[0]["MobileNo"].ToString();
            }

            txtPhone.Text = dtBasicInfo.Rows[0]["LandlineNo"].ToString();
            txtEmail.Text = dtBasicInfo.Rows[0]["Email"].ToString();
            txtRemark.Text = dtBasicInfo.Rows[0]["Remark1"].ToString();

            txtBank.Text = dtBasicInfo.Rows[0]["BankName"].ToString();
            txtBranch.Text = dtBasicInfo.Rows[0]["BranchName"].ToString();
            txtIFSCCode.Text = dtBasicInfo.Rows[0]["IFSCCode"].ToString();

            ddlCompositionOpted.SelectedValue = string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompositionOpted"].ToString()) ? "0" : dtBasicInfo.Rows[0]["CompositionOpted"].ToString();
            txtOtherDetails.Text = dtBasicInfo.Rows[0]["ReffPartyCode"].ToString();

            if (dtBasicInfo.Rows[0]["BankAccountNo"].ToString() == "0")
                txtAccountNo.Text = "";
            else
                txtAccountNo.Text = dtBasicInfo.Rows[0]["BankAccountNo"].ToString();


            if (dtTerms == null || dtTerms.Rows.Count <= 0)
            {
                dtTerms = DtTermsCondition();
            }
            else
            {
                dtTerms.Columns.Add("UserID", typeof(int));
                dtTerms.Columns.Add("IP", typeof(string));
                foreach (DataRow item in dtTerms.Rows)
                {
                    item["UserID"] = GlobalSession.UserID;
                    item["IP"] = GlobalSession.IP;
                }
            }
            grdTerms.DataSource = ViewState["grdTerms"] = dtTerms;
            grdTerms.DataBind();

            if (dtBasicInfo.Rows[0]["CategoryID"].ToString() == "")
                ddlExportCategory.SelectedValue = "0";
            else
                ddlExportCategory.SelectedValue = dtBasicInfo.Rows[0]["CategoryID"].ToString();

            txtAccountHead.Focus();
        }
        else
        {
            ShowMessage("Record Not Found.", false);
        }
    }

    void LoadGSTINInfo()
    {
        objUpdAccountHead = new UpdateAccounHeadModel();
        objUpdAccountHead.Ind = 2;
        objUpdAccountHead.OrgID = GlobalSession.OrgID;
        objUpdAccountHead.BrID = GlobalSession.BrID;
        objUpdAccountHead.IsAdmin = GlobalSession.IsAdmin;
        objUpdAccountHead.AccCode = Convert.ToInt64(ddlAccountHead.SelectedValue);
        string uri = string.Format("UpdateAccountHead/LoadGSTINInfo");
        DataTable dtGSTINInfo = CommonCls.ApiPostDataTable(uri, objUpdAccountHead);
        if (dtGSTINInfo.Rows.Count > 0)
        {
            dtGSTINInfo.Columns.Add("OrgID", typeof(int));
            dtGSTINInfo.Columns.Add("BrID", typeof(int));
            dtGSTINInfo.Columns.Add("YrCD", typeof(int));
            dtGSTINInfo.Columns.Add("Ind", typeof(int));

            dtGSTINInfo.Columns["OrgID"].SetOrdinal(0);
            dtGSTINInfo.Columns["BrID"].SetOrdinal(1);
            dtGSTINInfo.Columns["YrCD"].SetOrdinal(2);
            dtGSTINInfo.Columns["AccCode"].SetOrdinal(3);
            dtGSTINInfo.Columns["GSTIN"].SetOrdinal(4);
            dtGSTINInfo.Columns["RegistrationDate"].SetOrdinal(5);
            dtGSTINInfo.Columns["RegistrationAddress"].SetOrdinal(6);
            dtGSTINInfo.Columns["City"].SetOrdinal(7);
            dtGSTINInfo.Columns["State"].SetOrdinal(8);
            dtGSTINInfo.Columns["StateID"].SetOrdinal(9);
            dtGSTINInfo.Columns["PinCode"].SetOrdinal(10);
            dtGSTINInfo.Columns["AuthorizedSignatury"].SetOrdinal(11);
            dtGSTINInfo.Columns["SignaturyDesignation"].SetOrdinal(12);
            dtGSTINInfo.Columns["GSTINInd"].SetOrdinal(13);
            dtGSTINInfo.Columns["Ind"].SetOrdinal(14);
            foreach (DataRow drRow in dtGSTINInfo.Rows)
            {
                drRow["OrgID"] = GlobalSession.OrgID;
                drRow["BrID"] = GlobalSession.BrID;
                drRow["YrCD"] = GlobalSession.YrCD;
                drRow["Ind"] = 0;
            }
            grdGSTINInformation.DataSource = ViewState["dtGSTINInfo"] = dtGSTINInfo;
            grdGSTINInformation.DataBind();
        }
    }

    void LoadShippingInfo()
    {
        objUpdAccountHead = new UpdateAccounHeadModel();
        objUpdAccountHead.Ind = 3;
        objUpdAccountHead.OrgID = GlobalSession.OrgID;
        objUpdAccountHead.BrID = GlobalSession.BrID;
        objUpdAccountHead.IsAdmin = GlobalSession.IsAdmin;
        objUpdAccountHead.AccCode = Convert.ToInt64(ddlAccountHead.SelectedValue);
        string uri = string.Format("UpdateAccountHead/LoadShippingInfo");
        DataSet dsShippingInfo = CommonCls.ApiPostDataSet(uri, objUpdAccountHead);
        DataTable dtGSTIN = dsShippingInfo.Tables[1];
        if (dtGSTIN.Rows.Count > 0)
        {
            ddlShippingGSTIN.DataSource = dtGSTIN;
            ddlShippingGSTIN.DataTextField = "GSTIN";
            ddlShippingGSTIN.DataValueField = "AccGSTINID";
            ddlShippingGSTIN.DataBind();
            ddlShippingGSTIN.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        if (dsShippingInfo.Tables[0].Rows.Count > 0)
        {
            DataTable dtShippingInfo = dsShippingInfo.Tables[0];

            dtShippingInfo.Columns.Add("OrgID", typeof(int));
            dtShippingInfo.Columns.Add("BrID", typeof(int));
            dtShippingInfo.Columns.Add("YrCD", typeof(int));
            dtShippingInfo.Columns.Add("Ind", typeof(int));

            dtShippingInfo.Columns["OrgID"].SetOrdinal(0);
            dtShippingInfo.Columns["BrID"].SetOrdinal(1);
            dtShippingInfo.Columns["YrCD"].SetOrdinal(2);
            dtShippingInfo.Columns["AccCode"].SetOrdinal(3);
            dtShippingInfo.Columns["GSTIN"].SetOrdinal(4);
            dtShippingInfo.Columns["GSTINInd"].SetOrdinal(5);
            dtShippingInfo.Columns["ShippingAddress"].SetOrdinal(6);
            dtShippingInfo.Columns["City"].SetOrdinal(7);
            dtShippingInfo.Columns["State"].SetOrdinal(8);
            dtShippingInfo.Columns["StateID"].SetOrdinal(9);
            dtShippingInfo.Columns["PinCode"].SetOrdinal(10);
            dtShippingInfo.Columns["POSID"].SetOrdinal(11);
            dtShippingInfo.Columns["Ind"].SetOrdinal(12);

            foreach (DataRow drRow in dtShippingInfo.Rows)
            {
                drRow["OrgID"] = GlobalSession.OrgID;
                drRow["BrID"] = GlobalSession.BrID;
                drRow["YrCD"] = GlobalSession.YrCD;
                drRow["Ind"] = 0;
            }

            grdShippingInformation.DataSource = ViewState["dtShippingInfo"] = dtShippingInfo;
            grdShippingInformation.DataBind();


        }
        else
        {

            ShowMessage("Record Not Found.", false);
        }
    }

    protected void btnAddGSTIN_Click(object sender, EventArgs e)
    {
        GSTINInformationEnabledTrue();
        bool IsValid = ValidationBTNAddGSTIN();
        if (!IsValid)
        {
            lblMsg.Text = msgAddGSTIN;
            ShowMessage(msgAddGSTIN, false);
            return;
        }

        if (!GstInValid(ddlGSTINState.SelectedValue))
        {
            txtGSTIN.Focus();
            ShowMessage(msgGSTIN, false);
            return;
        }

        BindGRDGSTIN();
        ClearAllAfterAddGSTIN();
        txtGSTIN.Focus();
        DisplayOrNot();
        btnUpdate.Enabled = true;
        grdGSTINInformation.Enabled = true;

    }

    string msgAddGSTIN;
    bool ValidationBTNAddGSTIN()
    {
        if (string.IsNullOrEmpty(txtGSTIN.Text))
        {
            lblMsg.Text = "Enter GSTIN No.";
            msgAddGSTIN = lblMsg.Text;
            txtGSTIN.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(txtRegistrationDate.Text))
        {
            txtRegistrationDate.Focus();
            lblMsg.Text = "Enter Registration Date.";
            msgAddGSTIN = lblMsg.Text;
            txtRegistrationDate.Focus();
            return false;
        }

        bool ValidDate = CommonCls.CheckFinancialYrDate(txtRegistrationDate.Text, "01/01/2016", DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate)
        {
            lblMsg.Text = "Registration Date Should Be Within Financial Year Date Or Not More Than Todays Date!";
            msgAddGSTIN = lblMsg.Text;
            txtRegistrationDate.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtGSTINCity.Text))
        {
            lblMsg.Text = "Enter GSTIN City.";
            msgAddGSTIN = lblMsg.Text;
            txtGSTINCity.Focus();
            return false;
        }
        if (ddlGSTINState.SelectedItem.Value == "0")
        {
            lblMsg.Text = "Select GSTIN State.";
            msgAddGSTIN = lblMsg.Text;
            ddlGSTINState.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtGSTINPinCode.Text))
        {
            lblMsg.Text = "Enter GSTIN PinCode.";
            msgAddGSTIN = lblMsg.Text;
            txtGSTINPinCode.Focus();
            return false;
        }


        if (!string.IsNullOrEmpty(txtGSTIN.Text))
        {
            bool GSTINNumber = CheckGSTINNumber_Validation();
            if (GSTINNumber == false)
            {

                lblMsg.Text = "Invalid GSTIN No.";
                msgAddGSTIN = lblMsg.Text;
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
                    bool CheckGSTIN_Number = CommonCls.gstinvalid(txtGSTIN.Text.ToUpper(), ddlGSTINState.SelectedValue, txtPanNo.Text.ToUpper());
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

    DataTable DtGSTINSchema()
    {
        DataTable dtGSTINInfo = new DataTable();
        dtGSTINInfo.Columns.Add("OrgID", typeof(int));
        dtGSTINInfo.Columns.Add("BrID", typeof(int));
        dtGSTINInfo.Columns.Add("YrCD", typeof(int));
        dtGSTINInfo.Columns.Add("AccCode", typeof(int));
        dtGSTINInfo.Columns.Add("GSTIN", typeof(string));
        dtGSTINInfo.Columns.Add("RegistrationDate", typeof(string));
        dtGSTINInfo.Columns.Add("RegistrationAddress", typeof(string));
        dtGSTINInfo.Columns.Add("City", typeof(string));
        dtGSTINInfo.Columns.Add("State", typeof(string));
        dtGSTINInfo.Columns.Add("StateID", typeof(int));
        dtGSTINInfo.Columns.Add("PinCode", typeof(int));
        dtGSTINInfo.Columns.Add("AuthorizedSignatury", typeof(string));
        dtGSTINInfo.Columns.Add("SignaturyDesignation", typeof(string));
        dtGSTINInfo.Columns.Add("GSTINInd", typeof(int));
        dtGSTINInfo.Columns.Add("Ind", typeof(int));

        return dtGSTINInfo;
    }

    void BindGRDGSTIN()
    {
        DataTable dtGSTINInfo = new DataTable();
        if (ViewState["dtGSTINInfo"] == null)
        {
            dtGSTINInfo = DtGSTINSchema();
        }
        else
        {
            dtGSTINInfo = (DataTable)ViewState["dtGSTINInfo"];
        }

        DataRow DrGSTINInfo = dtGSTINInfo.NewRow();
        DrGSTINInfo["OrgID"] = GlobalSession.OrgID;
        DrGSTINInfo["BrID"] = GlobalSession.BrID;
        DrGSTINInfo["YrCD"] = GlobalSession.YrCD;
        DrGSTINInfo["AccCode"] = Convert.ToInt32(ddlAccountHead.SelectedValue);
        DrGSTINInfo["GSTIN"] = txtGSTIN.Text.ToUpper();
        DrGSTINInfo["RegistrationDate"] = CommonCls.ConvertToDate(txtRegistrationDate.Text);
        DrGSTINInfo["RegistrationAddress"] = txtRegistrationAddress.Text;
        DrGSTINInfo["City"] = txtGSTINCity.Text;
        DrGSTINInfo["State"] = ddlGSTINState.SelectedItem.Text;
        DrGSTINInfo["StateID"] = Convert.ToInt16(ddlGSTINState.SelectedItem.Value);
        DrGSTINInfo["PinCode"] = Convert.ToInt32(txtGSTINPinCode.Text);
        DrGSTINInfo["AuthorizedSignatury"] = txtAuthorizedSignatory.Text;
        DrGSTINInfo["SignaturyDesignation"] = txtDesignation.Text;

        if (btnAddGSTIN.Text == "Update")
        {
            if (hfEditInd.Value != "2")
            {
                DrGSTINInfo["GSTINInd"] = Convert.ToInt32(hfGSTINInd.Value);
                DrGSTINInfo["Ind"] = 1;
            }
            else
            {
                DrGSTINInfo["GSTINInd"] = 0;
                DrGSTINInfo["Ind"] = 2;
            }
            btnAddGSTIN.Text = "Add";
        }
        else if (btnAddGSTIN.Text == "Add")
        {
            DrGSTINInfo["GSTINInd"] = 0;
            DrGSTINInfo["Ind"] = 2;
        }

        hfEditInd.Value = "";

        dtGSTINInfo.Rows.Add(DrGSTINInfo);
        grdGSTINInformation.DataSource = ViewState["dtGSTINInfo"] = ViewState["dtSameAs"] = dtGSTINInfo;
        grdGSTINInformation.DataBind();

    }

    protected void grdGSTINInformation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "EditRow")
        {
            hfEditInd.Value = "0";

            DataTable dtGSTINInfo = (DataTable)ViewState["dtGSTINInfo"];
            DataRow drGSTINInfo = dtGSTINInfo.Rows[rowIndex];


            txtGSTIN.Text = drGSTINInfo["GSTIN"].ToString();
            hfGSTINInd.Value = drGSTINInfo["GSTINInd"].ToString();
            txtRegistrationDate.Text = Convert.ToDateTime(drGSTINInfo["RegistrationDate"].ToString()).ToString("dd/MM/yyyy");
            txtRegistrationAddress.Text = drGSTINInfo["RegistrationAddress"].ToString();
            txtGSTINCity.Text = drGSTINInfo["City"].ToString();
            ddlGSTINState.SelectedValue = drGSTINInfo["StateID"].ToString();
            txtGSTINPinCode.Text = drGSTINInfo["PinCode"].ToString();
            txtAuthorizedSignatory.Text = drGSTINInfo["AuthorizedSignatury"].ToString();
            txtDesignation.Text = drGSTINInfo["SignaturyDesignation"].ToString();

            btnAddGSTIN.Text = "Update";

            hfEditInd.Value = drGSTINInfo["Ind"].ToString();

            dtGSTINInfo.Rows.RemoveAt(rowIndex);

            grdGSTINInformation.DataSource = ViewState["dtGSTINInfo"] = dtGSTINInfo;
            grdGSTINInformation.DataBind();

            GSTINInformationEnabledTrue();
            btnUpdate.Enabled = false;
            grdGSTINInformation.Enabled = false;
        }
        DisplayOrNot();
    }

    void ClearAllAfterAddGSTIN()
    {
        txtGSTIN.Text = txtRegistrationDate.Text = txtRegistrationAddress.Text = txtGSTINCity.Text = txtGSTINPinCode.Text = txtAuthorizedSignatory.Text = txtDesignation.Text = "";
        ddlGSTINState.ClearSelection();
    }

    string msgGSTIN;
    bool GstInValid(string StateID)
    {
        string stateValue;
        if (txtGSTIN.Text.Count() == 15)
        {
            if (StateID.Length == 1)
            {
                stateValue = "0" + Convert.ToString(StateID);
            }
            else
            {
                stateValue = Convert.ToString(StateID);
            }
            if (!string.IsNullOrEmpty(txtGSTIN.Text))
            {
                string firstTwo = txtGSTIN.Text.Substring(0, 2);

                if (stateValue != firstTwo)
                {
                    msgGSTIN = "GSTIN No Or State Code Not Match.";
                    txtGSTIN.Focus();
                    return false;
                }
            }
        }
        else
        {
            msgGSTIN = "Enter 15 Digit GSTIN No.";
            return false;
        }
        return true;
    }

    //protected void chkSameAsGSTINAddress_CheckedChanged(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    if (chkSameAsGSTINAddress.Checked == true)
    //    {
    //        ddlShippingGSTIN.Enabled = false;
    //        ddlShippingState.Enabled = false;
    //        txtShippingCity.Enabled = false;
    //        txtShippingAddress.Enabled = false;
    //        txtShippingPincode.Enabled = false;
    //        if (ddlShippingGSTIN.SelectedValue != "")
    //        {
    //            int id = Convert.ToInt32(ddlShippingGSTIN.SelectedValue);
    //            if (id != 0)
    //            {
    //                DataTable dtSameAs = (DataTable)ViewState["dtSameAs"];
    //                DataRow[] dr = dtSameAs.Select("GSTINID = " + id + "");
    //                if (dr.Length > 0)
    //                {
    //                    foreach (DataRow row in dr)
    //                    {
    //                        txtShippingCity.Text = row["City"].ToString();
    //                        txtShippingAddress.Text = row["RegistrationAddress"].ToString();
    //                        ddlShippingState.SelectedValue = row["StateID"].ToString();
    //                        txtShippingPincode.Text = row["PinCode"].ToString();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                ddlShippingGSTIN.Enabled = true;
    //                ddlShippingState.Enabled = true;
    //                txtShippingCity.Enabled = true;
    //                txtShippingAddress.Enabled = true;
    //                txtShippingPincode.Enabled = true;

    //                txtShippingCity.Text = "";
    //                txtShippingAddress.Text = "";
    //                ddlShippingState.SelectedIndex = 0;
    //                txtShippingPincode.Text = "";
    //            }
    //        }
    //    }
    //    else
    //    {
    //        ddlShippingGSTIN.Enabled = true;
    //        ddlShippingState.Enabled = true;
    //        txtShippingCity.Enabled = true;
    //        txtShippingAddress.Enabled = true;
    //        txtShippingPincode.Enabled = true;

    //        txtShippingCity.Text = "";
    //        txtShippingAddress.Text = "";
    //        ddlShippingState.SelectedIndex = 0;
    //        txtShippingPincode.Text = "";
    //    }
    //    DisplayOrNot();
    //}

    //protected void chkAddNeew_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkAddNeew.Checked == true)
    //    {
    //        //ddlShippingGSTIN.Visible = false;
    //        txtShippingGSTIN.Visible = true;
    //    }
    //    else
    //    {
    //        //ddlShippingGSTIN.Visible = true;
    //        txtShippingGSTIN.Visible = false;
    //    }
    //}

    protected void btnAddShipping_Click(object sender, EventArgs e)
    {
        ShippingInformationEnabledTrue();
        bool IsValid = ValidationBTNAddShipping();
        if (!IsValid)
        {
            lblMsg.Text = msgAddShipping;
            ShowMessage(lblMsg.Text, false);
            return;
        }
        if (!ShippingGstInValid(ddlShippingState.SelectedValue))
        {
            ddlShippingGSTIN.Focus();
            return;
        }
        BindGRDShipping();
        ddlShippingGSTIN.Focus();

        ClearAllAfterAddShipping();
        DisplayOrNot();
        btnUpdate.Enabled = true;
        grdShippingInformation.Enabled = true;

    }

    string msgAddShipping;
    bool ValidationBTNAddShipping()
    {
        if (ddlShippingGSTIN.SelectedItem != null)
        {
            if (ddlShippingGSTIN.SelectedItem.Value == "0")
            {
                lblMsg.Text = "Select Shipping GSTIN.";
                msgAddShipping = lblMsg.Text;
                ddlShippingGSTIN.Focus();
                return false;
            }
        }
        if (string.IsNullOrEmpty(txtShippingCity.Text))
        {
            lblMsg.Text = "Enter Shipping City.";
            msgAddShipping = lblMsg.Text;
            txtShippingCity.Focus();
            return false;
        }
        if (ddlShippingState.SelectedItem.Value == "0")
        {
            lblMsg.Text = "Select Shipping State.";
            msgAddShipping = lblMsg.Text;
            ddlShippingState.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtShippingPincode.Text))
        {
            lblMsg.Text = "Enter Shipping PinCode.";
            msgAddShipping = lblMsg.Text;
            txtShippingPincode.Focus();
            return false;
        }
        return true;
    }

    DataTable DtShippingSchema()
    {
        DataTable dtShippingInfo = new DataTable();
        dtShippingInfo.Columns.Add("OrgID", typeof(int));
        dtShippingInfo.Columns.Add("BrID", typeof(int));
        dtShippingInfo.Columns.Add("YrCD", typeof(int));
        dtShippingInfo.Columns.Add("AccCode", typeof(int));
        dtShippingInfo.Columns.Add("GSTIN", typeof(string));
        dtShippingInfo.Columns.Add("GSTINInd", typeof(int));
        dtShippingInfo.Columns.Add("ShippingAddress", typeof(string));
        dtShippingInfo.Columns.Add("City", typeof(string));
        dtShippingInfo.Columns.Add("State", typeof(string));
        dtShippingInfo.Columns.Add("StateID", typeof(int));
        dtShippingInfo.Columns.Add("PinCode", typeof(int));
        dtShippingInfo.Columns.Add("POSID", typeof(int));
        dtShippingInfo.Columns.Add("Ind", typeof(int));

        return dtShippingInfo;
    }

    void BindGRDShipping()
    {
        DataTable dtShippingnfo = new DataTable();
        if (ViewState["dtShippingInfo"] == null)
        {
            dtShippingnfo = DtShippingSchema();
        }
        else
        {
            dtShippingnfo = (DataTable)ViewState["dtShippingInfo"];
        }

        DataRow DrShippingInfo = dtShippingnfo.NewRow();

        DrShippingInfo["OrgID"] = GlobalSession.OrgID;
        DrShippingInfo["BrID"] = GlobalSession.BrID;
        DrShippingInfo["YrCD"] = GlobalSession.YrCD;
        DrShippingInfo["AccCode"] = Convert.ToInt32(ddlAccountHead.SelectedValue);

        DrShippingInfo["GSTIN"] = ddlShippingGSTIN.SelectedItem == null ? "" : ddlShippingGSTIN.SelectedItem.Text;

        if (btnAddShipping.Text == "Update")
        {
            if (hfShippingEditInd.Value != "2")
            {
                DrShippingInfo["GSTINInd"] = Convert.ToInt32(hfShippinfGSTINInd.Value);
                DrShippingInfo["POSID"] = Convert.ToInt32(hfPOSID.Value);
                DrShippingInfo["Ind"] = 1;
            }
            else
            {
                //DataTable dtGSTINInd = (DataTable)ViewState["dtShippingInfo"];
                //DataRow[] gstinIndRow = dtGSTINInd.Select("POSID= " + Convert.ToInt32(ddlShippingGSTIN.SelectedValue));

                DataTable dtGSTINInd;
                if (ViewState["dtShippingInfo"] != null)
                {
                    dtGSTINInd = (DataTable)ViewState["dtShippingInfo"];
                }
                else
                {
                    dtGSTINInd = DtShippingSchema();
                }
                DataRow[] gstinIndRow = dtGSTINInd.Select("POSID= " + Convert.ToInt32(ddlShippingGSTIN.SelectedValue));
                if (gstinIndRow.Count() > 0)
                {
                    hfShippinfGSTINInd.Value = gstinIndRow[0]["GSTINInd"].ToString();

                }

                DrShippingInfo["GSTINInd"] = !string.IsNullOrEmpty(hfShippinfGSTINInd.Value) ? Convert.ToInt32(hfShippinfGSTINInd.Value) : 0;
                DrShippingInfo["POSID"] = 0;
                DrShippingInfo["Ind"] = 2;
            }
            btnAddShipping.Text = "Add";
        }
        else if (btnAddGSTIN.Text == "Add")
        {
            DataTable dtGSTINInd;
            if (ViewState["dtShippingInfo"] != null)
            {
                dtGSTINInd = (DataTable)ViewState["dtShippingInfo"];
            }
            else
            {
                dtGSTINInd = DtShippingSchema();
            }
            DataRow[] gstinIndRow = dtGSTINInd.Select("POSID= " + Convert.ToInt32(ddlShippingGSTIN.SelectedValue));
            if (gstinIndRow.Count() > 0)
            {
                hfShippinfGSTINInd.Value = gstinIndRow[0]["GSTINInd"].ToString();
            }

            DrShippingInfo["GSTINInd"] = !string.IsNullOrEmpty(hfShippinfGSTINInd.Value) ? Convert.ToInt32(hfShippinfGSTINInd.Value) : 0;
            DrShippingInfo["POSID"] = 0;
            DrShippingInfo["Ind"] = 2;
        }

        DrShippingInfo["ShippingAddress"] = txtShippingAddress.Text;
        DrShippingInfo["City"] = txtShippingCity.Text;
        DrShippingInfo["State"] = ddlShippingState.SelectedItem.Text;
        DrShippingInfo["StateID"] = Convert.ToInt16(ddlShippingState.SelectedItem.Value);
        DrShippingInfo["Pincode"] = Convert.ToInt32(txtShippingPincode.Text);

        dtShippingnfo.Rows.Add(DrShippingInfo);
        grdShippingInformation.DataSource = ViewState["dtShippingInfo"] = dtShippingnfo;
        grdShippingInformation.DataBind();

        hfShippingEditInd.Value = hfShippinfGSTINInd.Value = "";
    }

    protected void grdShippingInformation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "EditRow")
        {
            DataTable dtShippingInfo = (DataTable)ViewState["dtShippingInfo"];
            DataRow drShippingInfo = dtShippingInfo.Rows[rowIndex];

            hfPOSID.Value = drShippingInfo["POSID"].ToString();
            hfShippinfGSTINInd.Value = drShippingInfo["GSTINInd"].ToString();
            hfShippingEditInd.Value = drShippingInfo["Ind"].ToString();
            ddlShippingGSTIN.SelectedValue = drShippingInfo["POSID"].ToString();
            txtShippingAddress.Text = drShippingInfo["ShippingAddress"].ToString();
            txtShippingCity.Text = drShippingInfo["City"].ToString();
            ddlShippingState.SelectedValue = drShippingInfo["StateID"].ToString();
            txtShippingPincode.Text = drShippingInfo["PinCode"].ToString();

            btnAddShipping.Text = "Update";

            dtShippingInfo.Rows.RemoveAt(rowIndex);

            grdShippingInformation.DataSource = ViewState["dtShippingInfo"] = dtShippingInfo;
            grdShippingInformation.DataBind();

            ShippingInformationEnabledTrue();
            btnUpdate.Enabled = false;
            grdShippingInformation.Enabled = false;

        }
        DisplayOrNot();
    }

    void ClearAllAfterAddShipping()
    {
        txtShippingAddress.Text = txtShippingCity.Text = txtShippingPincode.Text = "";
        ddlShippingState.ClearSelection();

        ddlShippingState.Enabled = true;
        txtShippingCity.Enabled = true;
        txtShippingAddress.Enabled = true;
        txtShippingPincode.Enabled = true;

        ddlShippingGSTIN.ClearSelection();
    }

    bool ShippingGstInValid(string StateID)
    {
        string stateValue;
        if (ddlShippingGSTIN.SelectedItem != null && ddlShippingGSTIN.SelectedItem.Text != "")
        {
            if (ddlShippingGSTIN.SelectedItem.Text.Count() == 15)
            {
                if (StateID.Length == 1)
                {
                    stateValue = "0" + Convert.ToString(StateID);
                }
                else
                {
                    stateValue = Convert.ToString(StateID);
                }
                if (ddlShippingGSTIN.SelectedValue != "0")
                {
                    string firstTwo = ddlShippingGSTIN.SelectedItem.Text.Substring(0, 2);

                    if (stateValue != firstTwo)
                    {
                        ShowMessage("GSTIN No Or State Code Not Match.", false);
                        ddlShippingGSTIN.Focus();
                        return false;
                    }
                }
            }
            else
            {
                ShowMessage("Enter 15 Digit GSTIN No.", false);
                return false;
            }
        }
        return true;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        objUpdAccountHead = new UpdateAccounHeadModel();

        //Update Client Information

        if (rbBasicInfo.Checked == true)
        {
            bool IsValid = ValidationBTNUpdate();
            if (!IsValid)
            {
                lblMsg.Text = Msg;
                ShowMessage(Msg, false);
                ClientInformationEnabledTrue();
                return;
            }

            objUpdAccountHead.Ind = 4;
            objUpdAccountHead.OrgID = GlobalSession.OrgID;
            objUpdAccountHead.BrID = GlobalSession.BrID;
            objUpdAccountHead.YrCD = GlobalSession.YrCD;
            objUpdAccountHead.MainGroupID = Convert.ToInt16(hfMainGroupCode.Value);
            objUpdAccountHead.SubGroupID = Convert.ToInt32(hfMainSubGroupCode.Value);
            objUpdAccountHead.AccGroupID = Convert.ToInt32(ddlGroupName.SelectedItem.Value);
            objUpdAccountHead.AccSubGroupID = Convert.ToInt32(hfAccSubGroupID.Value);
            objUpdAccountHead.AccCode = Convert.ToInt32(ddlAccountHead.SelectedValue);
            objUpdAccountHead.AccName = txtAccountHead.Text;
            objUpdAccountHead.AccNameHindi = txtAccountHeadHindi.Text;

            objUpdAccountHead.Address = txtClientAddress.Text;
            objUpdAccountHead.City = txtClientCity.Text;
            objUpdAccountHead.StateID = Convert.ToInt32(ddlClientState.SelectedValue);
            objUpdAccountHead.PinCode = string.IsNullOrEmpty(txtClientPincode.Text) ? 0 : Convert.ToInt32(txtClientPincode.Text);
            objUpdAccountHead.PanNo = txtPanNo.Text;
            objUpdAccountHead.TaxCalcForSEZParty = CommonCls.ConvertIntZero(ddlExportCategory.SelectedValue) == 3 ? CommonCls.ConvertIntZero(ddlTaxCalType.SelectedValue) : 0;
            if (ddlDrCr.SelectedItem.Text == "Dr")
            {
                objUpdAccountHead.DrOpBalance = string.IsNullOrEmpty(txtOpeningBalance.Text) ? 0 : txtOpeningBalance.Text == "0" ? 0 : Convert.ToDecimal(txtOpeningBalance.Text);
            }
            else
            {
                objUpdAccountHead.CrOpBalance = string.IsNullOrEmpty(txtOpeningBalance.Text) ? 0 : txtOpeningBalance.Text == "0" ? 0 : Convert.ToDecimal(txtOpeningBalance.Text);
            }

            objUpdAccountHead.ContactPerson = txtPersonName.Text;
            objUpdAccountHead.MobileNo = string.IsNullOrEmpty(txtMobileNo.Text) ? 0 : Convert.ToInt64(txtMobileNo.Text);
            objUpdAccountHead.LandlineNo = txtPhone.Text;
            objUpdAccountHead.Email = txtEmail.Text;
            objUpdAccountHead.Remark = txtRemark.Text;

            objUpdAccountHead.ISDApplicable = Convert.ToInt16(ddlTDS.SelectedItem.Value);
            objUpdAccountHead.TDSApplicable = Convert.ToInt16(ddlISD.SelectedItem.Value);
            objUpdAccountHead.RCMApplicable = Convert.ToInt16(ddlRCM.SelectedItem.Value);
            objUpdAccountHead.TCSApplicable = Convert.ToInt16(ddlTCS.SelectedItem.Value);
            objUpdAccountHead.MerchantID = string.IsNullOrEmpty(txtMerchantID.Text) ? "" : txtMerchantID.Text;
            objUpdAccountHead.CategoryID = ddlExportCategory.SelectedItem.Value == "0" ? 0 : Convert.ToInt16(ddlExportCategory.SelectedItem.Value);
            objUpdAccountHead.BankName = txtBank.Text;
            objUpdAccountHead.BranchName = txtBranch.Text;
            objUpdAccountHead.IFSCCode = txtIFSCCode.Text;
            objUpdAccountHead.BankAccountNo = txtAccountNo.Text;
            objUpdAccountHead.User = GlobalSession.UserID;
            objUpdAccountHead.IP = GlobalSession.IP;
            objUpdAccountHead.CompositionOpted = Convert.ToInt16(ddlCompositionOpted.SelectedValue);
            objUpdAccountHead.ReffPartyCode = txtOtherDetails.Text;
            objUpdAccountHead.IsSubDealer = Convert.ToInt16(ddlSubDealer.SelectedValue);
            objUpdAccountHead.DiscountRate = CommonCls.ConvertDecimalZero(txtDiscountRate.Text);
            //objUpdAccountHead.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerageRate.Text);

            //For BrokerageRate

            objUpdAccountHead.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerRate.Text);
            objUpdAccountHead.BrokerageLimit = CommonCls.ConvertDecimalZero(txtBrokerageLimit.Text);
            objUpdAccountHead.BrokerageType = CommonCls.ConvertIntZero(ddlBrokerageType.SelectedValue);




            if ((DataTable)ViewState["grdTerms"] == null || ((DataTable)ViewState["grdTerms"]).Rows.Count <= 0)
            {
                objUpdAccountHead.TblAccTerms = DtTermsCondition();
                DataRow Dr = objUpdAccountHead.TblAccTerms.NewRow();
                Dr["OrgID"] = GlobalSession.OrgID;
                Dr["BrID"] = GlobalSession.BrID;
                Dr["AccCode"] = 0;
                Dr["Terms"] = "";
                Dr["UserID"] = GlobalSession.UserID;
                Dr["IP"] = GlobalSession.IP;
                objUpdAccountHead.TblAccTerms.Rows.Add(Dr);
            }
            else
            {
                objUpdAccountHead.TblAccTerms = (DataTable)ViewState["grdTerms"];
            }


            string uri = string.Format("UpdateAccountHead/UpdateClientInfo");
            DataTable dtUpdate = CommonCls.ApiPostDataTable(uri, objUpdAccountHead);
            if (dtUpdate.Rows.Count > 0)
            {
                if (dtUpdate.Rows[0]["Column1"].ToString() == "0")
                {
                    lblMsg.Text = "Record Not Save Please Try Again.";
                    ShowMessage(lblMsg.Text, false);
                }
                else if (dtUpdate.Rows[0]["Column1"].ToString() == "1")
                {
                    ClearAll();
                    lblMsg.Text = "Client Information Update successfully.";
                    ShowMessage(lblMsg.Text, true);
                    ddlAccountHead.Focus();
                    LoadAccoundHeadDDL();
                    btnSearch.Enabled = true;
                }
                else if (dtUpdate.Rows[0]["Column1"].ToString() == "2")
                {
                    lblMsg.Text = "Duplicate Record.";
                    ShowMessage(lblMsg.Text, false);
                    txtAccountHead.Focus();
                }

            }
            else
            {
                lblMsg.Text = "Record Not Save Please Try Again.";
                ShowMessage(lblMsg.Text, false);
            }
        }
        else if (rbGSTINInfo.Checked == true) //Update GSTIN Information
        {
            objUpdAccountHead.Ind = 5;
            objUpdAccountHead.OrgID = GlobalSession.OrgID;
            objUpdAccountHead.BrID = GlobalSession.BrID;
            objUpdAccountHead.User = GlobalSession.UserID;
            objUpdAccountHead.IP = GlobalSession.IP;
            objUpdAccountHead.DtAccGSTIN = DtGSTINSchema();
            objUpdAccountHead.DtAccGSTIN = (DataTable)ViewState["dtGSTINInfo"];
            //objUpdAccountHead.DtAccGSTIN.Columns.
            if (objUpdAccountHead.DtAccGSTIN != null)
            {
                if (objUpdAccountHead.DtAccGSTIN.Columns.Contains("State"))
                {
                    objUpdAccountHead.DtAccGSTIN.Columns.Remove("State");
                }
                //if (objUpdAccountHead.DtAccGSTIN.Columns.Contains("GSTINID"))
                //{
                //    objUpdAccountHead.DtAccGSTIN.Columns.Remove("GSTINID");
                //}
            }
            else
            {
                objUpdAccountHead.DtAccGSTIN = DtGSTINSchema();
                DataRow drGSTINInfo = objUpdAccountHead.DtAccGSTIN.NewRow();

                drGSTINInfo["OrgID"] = GlobalSession.OrgID;
                drGSTINInfo["BrID"] = GlobalSession.BrID;
                drGSTINInfo["YrCD"] = GlobalSession.YrCD;
                drGSTINInfo["AccCode"] = 0;
                drGSTINInfo["GSTIN"] = "";
                drGSTINInfo["RegistrationDate"] = string.IsNullOrEmpty(txtRegistrationDate.Text) ? "" : CommonCls.ConvertToDate(txtRegistrationDate.Text);
                drGSTINInfo["RegistrationAddress"] = "";
                drGSTINInfo["City"] = "";
                drGSTINInfo["StateID"] = 0;
                drGSTINInfo["PinCode"] = 0;
                drGSTINInfo["AuthorizedSignatury"] = "";
                drGSTINInfo["SignaturyDesignation"] = "";
                objUpdAccountHead.DtAccGSTIN.Rows.Add(drGSTINInfo);
                objUpdAccountHead.DtAccGSTIN.Columns.Remove("State");
                objUpdAccountHead.DtAccGSTIN.Columns.Remove("GSTINID");
            }

            string uri = string.Format("UpdateAccountHead/UpdateGSTINInfo");
            DataTable dtUpdate = CommonCls.ApiPostDataTable(uri, objUpdAccountHead);
            if (dtUpdate.Rows.Count > 0)
            {
                if (dtUpdate.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    lblMsg.Text = "Record Not Save Please Try Again.";
                }
                else if (dtUpdate.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    lblMsg.Text = "GSTIN Information Update successfully.";
                    ShowMessage(lblMsg.Text, true);
                    ddlGroupName.Focus();
                }
                else if (dtUpdate.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    lblMsg.Text = "Duplicate Record.";
                    txtAccountHead.Focus();
                }
            }
            else
            {
                lblMsg.Text = "Record Not Save Please Try Again.";
            }
        }
        else if (rbShippingInfo.Checked == true)   //Update Shipping Information
        {
            objUpdAccountHead.Ind = 6;
            objUpdAccountHead.OrgID = GlobalSession.OrgID;
            objUpdAccountHead.BrID = GlobalSession.BrID;
            objUpdAccountHead.User = GlobalSession.UserID;
            objUpdAccountHead.IP = GlobalSession.IP;
            objUpdAccountHead.DtAccPOS = DtShippingSchema();
            objUpdAccountHead.DtAccPOS = (DataTable)ViewState["dtShippingInfo"];

            if (objUpdAccountHead.DtAccPOS != null)
            {
                if (objUpdAccountHead.DtAccPOS.Columns.Contains("State"))
                {
                    objUpdAccountHead.DtAccPOS.Columns.Remove("State");
                }
            }
            else
            {
                objUpdAccountHead.DtAccPOS = DtShippingSchema();
                DataRow drShippingInfo = objUpdAccountHead.DtAccPOS.NewRow();
                drShippingInfo["OrgID"] = GlobalSession.OrgID;
                drShippingInfo["BrID"] = GlobalSession.BrID;
                drShippingInfo["YrCD"] = GlobalSession.YrCD;
                drShippingInfo["AccCode"] = 0;
                drShippingInfo["GSTIN"] = string.IsNullOrEmpty(ddlShippingGSTIN.SelectedItem.Text) ? "" : ddlShippingGSTIN.SelectedItem.Text;
                drShippingInfo["GSTINInd"] = 0;
                drShippingInfo["ShippingAddress"] = txtClientAddress.Text;
                drShippingInfo["City"] = txtClientCity.Text;
                drShippingInfo["StateID"] = Convert.ToInt32(ddlClientState.SelectedItem.Value);
                drShippingInfo["PinCode"] = string.IsNullOrEmpty(txtClientPincode.Text) ? 0 : Convert.ToInt32(txtClientPincode.Text);
                drShippingInfo["POSID"] = 0;
                drShippingInfo["Ind"] = 0;
                objUpdAccountHead.DtAccPOS.Rows.Add(drShippingInfo);
                objUpdAccountHead.DtAccPOS.Columns.Remove("State");
            }

            string uri = string.Format("UpdateAccountHead/UpdateShippingInfo");
            DataTable dtUpdate = CommonCls.ApiPostDataTable(uri, objUpdAccountHead);
            if (dtUpdate.Rows.Count > 0)
            {
                if (dtUpdate.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    lblMsg.Text = "Record Not Save Please Try Again.";
                    ShowMessage(lblMsg.Text, false);
                }
                else if (dtUpdate.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearAll();
                    lblMsg.Text = "Shipping Information Update successfully.";
                    ShowMessage(lblMsg.Text, true);
                    ddlGroupName.Focus();
                }
                else if (dtUpdate.Rows[0]["ReturnInd"].ToString() == "2")
                {
                    lblMsg.Text = "Duplicate Record.";
                    ShowMessage(lblMsg.Text, false);
                    txtAccountHead.Focus();
                }
            }
            else
            {
                lblMsg.Text = "Record Not Save Please Try Again.";
                ShowMessage(lblMsg.Text, false);
            }
        }
    }

    string Msg;
    bool ValidationBTNUpdate()
    {
        if (ddlGroupName.SelectedItem.Value == "0")
        {
            lblMsg.Text = "Select Group Name.";
            Msg = lblMsg.Text;
            ddlGroupName.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtAccountHead.Text))
        {
            lblMsg.Text = "Enter Account Head.";
            Msg = lblMsg.Text;
            txtAccountHead.Focus();
            return false;
        }
        if (Convert.ToInt32(hfMainGroupCode.Value) < 20)
        {
            if (string.IsNullOrEmpty(txtClientAddress.Text))
            {
                lblMsg.Text = "Enter Client Address.";
                Msg = lblMsg.Text;
                txtClientAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtClientCity.Text))
            {
                lblMsg.Text = "Enter Client City.";
                Msg = lblMsg.Text;
                txtClientCity.Focus();
                return false;
            }
            if (ddlClientState.SelectedItem.Value == "0")
            {
                lblMsg.Text = "Select Client State.";
                Msg = lblMsg.Text;
                ddlClientState.Focus();
                return false;
            }
        }


        #region Brokerage_Validation

        if (ddlBrokerageType.SelectedItem.Value == "1")
        {

            if (hfMainGroupCode.Value == "11" && hfMainSubGroupCode.Value == "30445" && hfAccSubGroupID.Value == "0" && hfAccGICodeFrom.Value == "660000" && hfAccGICodeTo.Value == "660499")
            {
                if (string.IsNullOrEmpty(txtBrokerRate.Text) || CommonCls.ConvertDecimalZero(txtBrokerRate.Text) == 0)
                {
                    lblMsg.Text = "Enter Brokerage Rate.";
                    Msg = lblMsg.Text;
                    txtBrokerRate.Focus();
                    return false;
                }
                //if (string.IsNullOrEmpty(txtBrokerageLimit.Text))
                //{
                //    lblMsg.Text = "Enter Brokerage Limit.";
                //    Msg = lblMsg.Text;
                //    txtBrokerageLimit.Focus();
                //    return false;
                //}

            }

            if (hfMainGroupCode.Value == "11" && hfMainSubGroupCode.Value == "30445" && hfAccSubGroupID.Value == "0" && hfAccGICodeFrom.Value == "660500" && hfAccGICodeTo.Value == "660999")
            {
                if (string.IsNullOrEmpty(txtBrokerRate.Text) || CommonCls.ConvertDecimalZero(txtBrokerRate.Text) == 0)
                {
                    lblMsg.Text = "Enter Brokerage Rate.";
                    Msg = lblMsg.Text;
                    txtBrokerRate.Focus();
                    return false;
                }
                //if (string.IsNullOrEmpty(txtBrokerageLimit.Text))
                //{
                //    lblMsg.Text = "Enter Brokerage Limit.";
                //    Msg = lblMsg.Text;
                //    txtBrokerageLimit.Focus();
                //    return false;
                //}

            }
        }

        #endregion

        return true;
    }

    DataTable DtAccountHeadSchema()
    {
        DataTable dtAccountHead = new DataTable();
        dtAccountHead.Columns.Add("OrgID", typeof(int));
        dtAccountHead.Columns.Add("BrID", typeof(int));
        dtAccountHead.Columns.Add("YrCD", typeof(int));
        dtAccountHead.Columns.Add("MainGroupID", typeof(int));
        dtAccountHead.Columns.Add("SubGroupID", typeof(int));
        dtAccountHead.Columns.Add("AccGroupID", typeof(int));
        dtAccountHead.Columns.Add("AccSubGroupID", typeof(int));
        dtAccountHead.Columns.Add("AccName", typeof(string));
        dtAccountHead.Columns.Add("Address", typeof(string));
        dtAccountHead.Columns.Add("City", typeof(string));
        dtAccountHead.Columns.Add("StateID", typeof(int));
        dtAccountHead.Columns.Add("PinCode", typeof(int));
        dtAccountHead.Columns.Add("PanNo", typeof(string));
        dtAccountHead.Columns.Add("DrOpBalance", typeof(decimal));
        dtAccountHead.Columns.Add("CrOpBalance", typeof(decimal));
        dtAccountHead.Columns.Add("ContactPerson", typeof(string));
        dtAccountHead.Columns.Add("MobileNo", typeof(long));
        dtAccountHead.Columns.Add("LandlineNo", typeof(string));
        dtAccountHead.Columns.Add("Email", typeof(string));
        dtAccountHead.Columns.Add("TDSApplicable", typeof(int));
        dtAccountHead.Columns.Add("ISDApplicable", typeof(int));
        dtAccountHead.Columns.Add("RCMApplicable", typeof(int));
        dtAccountHead.Columns.Add("TCSApplicable", typeof(int));
        dtAccountHead.Columns.Add("MerchantID", typeof(string));
        dtAccountHead.Columns.Add("CategoryID", typeof(int));
        dtAccountHead.Columns.Add("BankName", typeof(string));
        dtAccountHead.Columns.Add("BranchName", typeof(string));
        dtAccountHead.Columns.Add("IFSCCode", typeof(string));
        dtAccountHead.Columns.Add("BankAccountNo", typeof(string));
        dtAccountHead.Columns.Add("EntryType", typeof(int));
        dtAccountHead.Columns.Add("UserID", typeof(int));
        dtAccountHead.Columns.Add("IP", typeof(string));

        return dtAccountHead;
    }

    DataTable CreateAccountHeadData()
    {
        DataTable dtAccountHeadData = new DataTable();
        try
        {
            dtAccountHeadData = DtAccountHeadSchema(); //New DataTable

            DataRow drAccountHead = dtAccountHeadData.NewRow();

            drAccountHead["OrgID"] = GlobalSession.OrgID;
            drAccountHead["BrID"] = GlobalSession.BrID;
            drAccountHead["YrCD"] = GlobalSession.YrCD;
            drAccountHead["MainGroupID"] = Convert.ToInt16(hfMainGroupCode.Value);
            drAccountHead["SubGroupID"] = Convert.ToInt32(hfMainSubGroupCode.Value);
            drAccountHead["AccGroupID"] = Convert.ToInt32(ddlGroupName.SelectedItem.Value);
            drAccountHead["AccSubGroupID"] = Convert.ToInt32(hfAccSubGroupID.Value);
            drAccountHead["AccName"] = txtAccountHead.Text;
            drAccountHead["Address"] = txtClientAddress.Text;
            drAccountHead["City"] = txtClientCity.Text;
            drAccountHead["StateID"] = Convert.ToInt16(ddlClientState.SelectedItem.Value);
            drAccountHead["PinCode"] = string.IsNullOrEmpty(txtClientPincode.Text) ? 0 : Convert.ToInt32(txtClientPincode.Text);
            drAccountHead["PanNo"] = txtPanNo.Text; ;
            if (ddlDrCr.SelectedItem.Text == "Dr")
            {
                drAccountHead["DrOpBalance"] = string.IsNullOrEmpty(txtOpeningBalance.Text) ? 0 : txtOpeningBalance.Text == "0" ? 0 : Convert.ToDecimal(txtOpeningBalance.Text);
            }
            else
            {
                drAccountHead["CrOpBalance"] = string.IsNullOrEmpty(txtOpeningBalance.Text) ? 0 : txtOpeningBalance.Text == "0" ? 0 : Convert.ToDecimal(txtOpeningBalance.Text);
            }
            drAccountHead["ContactPerson"] = txtPersonName.Text;
            drAccountHead["MobileNo"] = string.IsNullOrEmpty(txtMobileNo.Text) ? 0 : Convert.ToInt64(txtMobileNo.Text);
            drAccountHead["LandlineNo"] = txtPhone.Text;
            drAccountHead["Email"] = txtEmail.Text;
            drAccountHead["TDSApplicable"] = Convert.ToInt16(ddlTDS.SelectedItem.Value);
            drAccountHead["ISDApplicable"] = Convert.ToInt16(ddlISD.SelectedItem.Value);
            drAccountHead["RCMApplicable"] = Convert.ToInt16(ddlRCM.SelectedItem.Value);
            drAccountHead["TCSApplicable"] = Convert.ToInt16(ddlTCS.SelectedItem.Value);
            drAccountHead["MerchantID"] = string.IsNullOrEmpty(txtMerchantID.Text) ? "" : txtMerchantID.Text;
            drAccountHead["CategoryID"] = ddlExportCategory.SelectedItem.Value == "0" ? 0 : Convert.ToInt16(ddlExportCategory.SelectedItem.Value);
            drAccountHead["BankName"] = txtBank.Text;
            drAccountHead["BranchName"] = txtBranch.Text;
            drAccountHead["IFSCCode"] = txtIFSCCode.Text;
            drAccountHead["BankAccountNo"] = txtAccountNo.Text;
            drAccountHead["EntryType"] = 1;
            drAccountHead["UserID"] = GlobalSession.UserID;
            drAccountHead["IP"] = GlobalSession.IP;
            dtAccountHeadData.Rows.Add(drAccountHead);
        }
        catch (Exception)
        {

        }
        return dtAccountHeadData;
    }

    void ClearAll()
    {
        //Client Information
        txtAccountHeadHindi.Text = "";

        trBrokerage.Visible = false;
        lblMsg.Text = "";
        ddlAccountHead.ClearSelection();
        ddlAccountHead.Enabled = true;
        ddlGroupName.ClearSelection();
        txtAccountHead.Text = txtClientAddress.Text = txtClientCity.Text = lblGroupDescription.Text = txtClientPincode.Text =
            txtOpeningBalance.Text = txtMerchantID.Text = txtPanNo.Text = txtBrokerRate.Text = txtBrokerageLimit.Text = "";
        ddlClientState.ClearSelection();
        ddlDrCr.ClearSelection();
        ddlTDS.ClearSelection();
        ddlISD.ClearSelection();
        ddlRCM.ClearSelection();
        ddlTCS.ClearSelection();
        ddlBrokerageType.ClearSelection();

        btnUpdate.Enabled = true;
        btnSearch.Enabled = true;

        //Contact Information

        txtPersonName.Text = txtMobileNo.Text = txtPhone.Text = txtEmail.Text = txtRemark.Text = "";
        txtBank.Text = txtBranch.Text = txtIFSCCode.Text = txtAccountNo.Text = "";
        ddlExportCategory.ClearSelection();

        txtGSTIN.Text = txtRegistrationDate.Text = txtRegistrationAddress.Text = txtGSTINCity.Text = txtGSTINPinCode.Text = txtAuthorizedSignatory.Text = txtDesignation.Text = "";
        ddlGSTINState.ClearSelection();

        //Clear GSTINInfo and ShippingInfo ViewState

        txtShippingAddress.Text = txtShippingCity.Text = txtShippingPincode.Text = "";
        ddlShippingState.ClearSelection();
        ViewState["grdTerms"] = null;
        grdTerms.DataSource = new DataTable();
        grdTerms.DataBind();
        ViewState["dtGSTINInfo"] = ViewState["dtShippingInfo"] = null;
        grdGSTINInformation.DataSource = grdShippingInformation.DataSource = new DataTable();
        grdGSTINInformation.DataBind();
        grdShippingInformation.DataBind();

        ClientInformationEnabledFalse();

        GSTINInformationEnabledFalse();

        ShippingInformationEnabledFalse();

        rbBasicInfo.Checked = true;
        divBasicInfo.Visible = true;
        rbBasicInfo.Enabled = true;

        rbGSTINInfo.Enabled = true;
        rbGSTINInfo.Checked = false;
        divGSTINInfo.Visible = false;

        rbShippingInfo.Enabled = true;
        rbShippingInfo.Checked = false;
        divShippingInfo.Visible = false;

        tdAccountHeadHeader.Style.Add("display", "none");
        tdAccountHeadBody.Style.Add("display", "none");
        tdContactInfoHeader.Style.Add("display", "");
        tdContactAndBankBody.Style.Add("display", "");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        ddlAccountHead.Focus();
    }

    protected void ddlTCS_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClientInformationEnabledTrue();

        if (ddlTCS.SelectedItem.Value == "1")
        {
            txtMerchantID.Enabled = true;
            txtMerchantID.Focus();
        }
        else
        {
            txtMerchantID.Enabled = false;
            txtPersonName.Focus();
        }
        DisplayOrNot();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    DataTable DtTermsCondition() //Create DataTable For Terms ConditionS
    {
        DataTable dtTermsInfo = new DataTable();

        dtTermsInfo.Columns.Add("OrgID", typeof(int));
        dtTermsInfo.Columns.Add("BrID", typeof(int));
        dtTermsInfo.Columns.Add("AccCode", typeof(int));
        dtTermsInfo.Columns.Add("Terms", typeof(string));
        dtTermsInfo.Columns.Add("UserID", typeof(int));
        dtTermsInfo.Columns.Add("IP", typeof(string));
        return dtTermsInfo;
    }

    DataTable dt;
    protected void btnTermsAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtTerms.Text))
            {
                txtTerms.Focus();
                ShowMessage("Enter Terms Condition ItemName..!", false);
                // lblMsg.Text = "";
                return;
            }

            if (ViewState["grdTerms"] == null)
            {
                dt = new DataTable();
                dt = DtTermsCondition();
            }
            else
            {

                DataTable dtVS = (DataTable)ViewState["grdTerms"];
                dt = DtTermsCondition();
                for (int i = 0; i < dtVS.Rows.Count; i++)
                {
                    DataRow DrVS = dt.NewRow();
                    DrVS["OrgID"] = GlobalSession.OrgID;
                    DrVS["BrID"] = GlobalSession.BrID;
                    DrVS["AccCode"] = 0;
                    DrVS["Terms"] = dtVS.Rows[i]["Terms"].ToString();
                    DrVS["UserID"] = GlobalSession.UserID;
                    DrVS["IP"] = GlobalSession.IP;

                    dt.Rows.Add(DrVS);
                }
                //grdTerms.DataSource = ViewState["grdTerms"] = dt;
                //grdTerms.DataBind();
                //txtTerms.Text = "";
            }
            DataRow Dr = dt.NewRow();
            Dr["OrgID"] = GlobalSession.OrgID;
            Dr["BrID"] = GlobalSession.BrID;
            Dr["AccCode"] = 0;
            Dr["Terms"] = txtTerms.Text;
            Dr["UserID"] = GlobalSession.UserID;
            Dr["IP"] = GlobalSession.IP;

            dt.Rows.Add(Dr);
            grdTerms.DataSource = ViewState["grdTerms"] = dt;
            grdTerms.DataBind();
            txtTerms.Text = "";
            btnUpdate.Enabled = true;

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void grdTerms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveItem")
            {
                dt = (DataTable)ViewState["grdTerms"];
                dt.Rows[rowIndex].Delete();

                ViewState["grdTerms"] = dt;
                grdTerms.DataSource = dt;
                grdTerms.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void rbBasicInfo_CheckedChanged(object sender, EventArgs e)
    {

        ClientInformationEnabledFalse();

        divBasicInfo.Visible = true;
        divGSTINInfo.Visible = false;
        divShippingInfo.Visible = false;

    }
    protected void rbGSTINInfo_CheckedChanged(object sender, EventArgs e)
    {
        GSTINInformationEnabledFalse();

        divGSTINInfo.Visible = true;
        divBasicInfo.Visible = false;
        divShippingInfo.Visible = false;

    }
    protected void rbShippingInfo_CheckedChanged(object sender, EventArgs e)
    {
        ShippingInformationEnabledFalse();

        divShippingInfo.Visible = true;
        divBasicInfo.Visible = false;
        divGSTINInfo.Visible = false;
    }
    protected void btnAddGSTINClear_Click(object sender, EventArgs e)
    {
        txtGSTIN.Text = txtRegistrationDate.Text = txtRegistrationAddress.Text = txtGSTINCity.Text = txtGSTINPinCode.Text = txtDesignation.Text = "";
        grdGSTINInformation.Enabled = true;
        ddlGSTINState.ClearSelection();
        grdGSTINInformation.DataSource = ViewState["dtGSTINInfo"];
        grdGSTINInformation.DataBind();
    }
    protected void btnAddShippingClear_Click(object sender, EventArgs e)
    {
        ddlShippingGSTIN.ClearSelection();
        txtShippingAddress.Text = txtShippingCity.Text = txtShippingPincode.Text = "";
        ddlShippingState.ClearSelection();
        grdShippingInformation.Enabled = true;

    }
}