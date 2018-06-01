using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAccountHead : System.Web.UI.Page
{
    AccountHeadModel objAccountHead;
    public DataTable dtAccountHead
    {
        get { return (DataTable)(ViewState["dtAccountHead"]); }
        set { ViewState["dtAccountHead"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            ddlGroupName.Focus();
            LoadAccoundHeadDDL();
            LoadBranchState();
            //  ManageMenuByUserRights();
        }


        //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
        //{
        //    btnSave.Visible = true;
        //    Session["MasterWrite"] = 0;
        //}

    }


    //private void ManageMenuByUserRights()
    //{
    //    try
    //    {

    //        DataTable dt = CommonCls.GetAllottedMenuDetails();

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 12)
    //            {
    //                btnSave.Visible = true;
    //            }

    //        }
    //    }
    //    catch (Exception  ex)
    //    {
    //        ShowMessage(ex.Message, false);
    //    }
    //}
    public void LoadAccoundHeadDDL()
    {
        try
        {
            objAccountHead = new AccountHeadModel();
            objAccountHead.OrgID = GlobalSession.OrgID;
            objAccountHead.BrID = GlobalSession.BrID;
            objAccountHead.YrCD = GlobalSession.YrCD;
            objAccountHead.IsAdmin = GlobalSession.IsAdmin;
            string uri = string.Format("AccountHead/ClientInformation");
            DataSet dsAccountHead = CommonCls.ApiPostDataSet(uri, objAccountHead);
            if (dsAccountHead.Tables.Count > 0)
            {
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

                dtAccountHead = dsAccountHead.Tables["AccountHead"];
                lstBoxAccountHead.DataSource = dsAccountHead.Tables["AccountHead"];
                lstBoxAccountHead.DataTextField = "AccName";
                lstBoxAccountHead.DataValueField = "AccFilter";
                lstBoxAccountHead.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }





    public void LoadBranchState()
    {
        try
        {
            objAccountHead = new AccountHeadModel();
            objAccountHead.Ind = 3;
            objAccountHead.OrgID = GlobalSession.OrgID;
            objAccountHead.BrID = GlobalSession.BrID;
            objAccountHead.YrCD = GlobalSession.YrCD;
            objAccountHead.IsAdmin = GlobalSession.IsAdmin;

            string uri = string.Format("AccountHead/LoadBranchState");
            DataTable dtBranchState = CommonCls.ApiPostDataTable(uri, objAccountHead);
            if (dtBranchState.Rows.Count > 0)
            {
                hfBranchState.Value = dtBranchState.Rows[0]["BranchState"].ToString();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void txtRegistrationDate_TextChanged(object sender, EventArgs e)
    {
        txtRegistrationAddress.Focus();
        DisplayOrNot();
    }

    void DisplayOrNot()
    {
        if (!string.IsNullOrEmpty(txtAccountHead.Text))
        {
            tdContactInfoHeader.Style.Add("display", "");
            tdContactAndBankBody.Style.Add("display", "");
            tdShippingInfoHeader.Style.Add("display", "");
            tdShippingBody.Style.Add("display", "");
            tdAccountHeadHeader.Style.Add("display", "none");
            tdAccountHeadBody.Style.Add("display", "none");
        }
        //else
        //{
        //    tdAccountHeadHeader.Style.Add("display", "");
        //    tdAccountHeadBody.Style.Add("display", "");
        //    tdContactInfoHeader.Style.Add("display", "none");
        //    tdContactAndBankBody.Style.Add("display", "none");
        //    tdShippingInfoHeader.Style.Add("display", "none");
        //    tdShippingBody.Style.Add("display", "none");
        //}
    }

    protected void ddlGroupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroupName.SelectedItem.Value != "0")
        {
            ddlClientState.ClearSelection();
            //ddlGSTINState.ClearSelection();
            //ddlShippingState.ClearSelection();
            DataTable dtGroupName = (DataTable)ViewState["dsGroupName"];
            if (dtGroupName != null)
            {
                int rowIndex = ddlGroupName.SelectedIndex;
                DataRow row = dtGroupName.Rows[rowIndex - 1];
                if (row != null)
                {
                    lblGroupDescription.Text = row["GroupDesc"].ToString();
                    hfMainGroupCode.Value = row["MainGroupCode"].ToString();
                    hfMainSubGroupCode.Value = row["MainSubGroupCode"].ToString();
                    hfAccSubGroupID.Value = row["AccSubGroupID"].ToString();
                    hfAccGICodeFrom.Value = row["AccGICodeFrom"].ToString();
                    hfAccGICodeTo.Value = row["AccGICodeTo"].ToString();

                    //if (Convert.ToInt32(hfMainGroupCode.Value) > 20)
                    //{
                    //    txtClientAddress.Enabled = txtClientCity.Enabled = ddlClientState.Enabled = txtClientPincode.Enabled =
                    //        txtPanNo.Enabled = ddlCompositionOpted.Enabled = txtOtherDetails.Enabled = ddlTDS.Enabled = ddlISD.Enabled = //= ddlDrCr.Enabled 
                    //        ddlRCM.Enabled = ddlTCS.Enabled = txtMerchantID.Enabled = false;

                    //    txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = false;

                    //    txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = false;

                    //    txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
                    //        ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
                    //        txtDesignation.Enabled = btnAddGSTIN.Enabled = false;

                    //    ddlShippingGSTIN.Enabled = chkSameAsGSTINAddress.Enabled = txtShippingAddress.Enabled = txtShippingCity.Enabled =
                    //        ddlShippingState.Enabled = txtShippingPincode.Enabled = btnAddShipping.Enabled = false;
                    //}
                    //else
                    //{
                    //    txtClientAddress.Enabled = txtClientCity.Enabled = ddlClientState.Enabled = txtClientPincode.Enabled =
                    //        txtPanNo.Enabled = ddlCompositionOpted.Enabled = txtOtherDetails.Enabled = ddlTDS.Enabled = ddlISD.Enabled = //= ddlDrCr.Enabled 
                    //        ddlRCM.Enabled = ddlTCS.Enabled = txtMerchantID.Enabled = true;

                    //    txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = true;

                    //    txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = true;

                    //    txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
                    //        ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
                    //        txtDesignation.Enabled = btnAddGSTIN.Enabled = true;

                    //    ddlShippingGSTIN.Enabled = chkSameAsGSTINAddress.Enabled = txtShippingAddress.Enabled = txtShippingCity.Enabled =
                    //        ddlShippingState.Enabled = txtShippingPincode.Enabled = btnAddShipping.Enabled = true;
                    //}

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

                    //When Group Name is Broker Outside State

                }
                txtAccountHead.Focus();
                DisplayOrNot();
            }
        }
        else
        {
            lblGroupDescription.Text = "";
            DisplayOrNot();
        }
    }

    //protected void txtAccountHead_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtAccountHead.Text != "")
    //    {
    //        if (ViewState["AccountHead"] != null)
    //        {
    //            DataTable dtAccountHead = (DataTable)ViewState["AccountHead"];
    //            DataRow[] row = dtAccountHead.Select("AccName=" + txtAccountHead.Text);
    //            if (row != null)
    //            {

    //            }
    //        }
    //    }
    //    else
    //    {
    //        ShowMessage("Enter Account Head!", false);
    //    }
    //}

    protected void chkGSTINNotAvailable_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (chkGSTINNotAvailable.Checked == true)
        {
            txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
                ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
                txtDesignation.Enabled = btnAddGSTIN.Enabled = ddlShippingGSTIN.Enabled =
                chkSameAsGSTINAddress.Enabled = false;
        }
        else
        {
            txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
                ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
                txtDesignation.Enabled = btnAddGSTIN.Enabled = ddlShippingGSTIN.Enabled =
                chkSameAsGSTINAddress.Enabled = true;
        }
        DisplayOrNot();
    }

    protected void btnAddGSTIN_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        bool IsValid = ValidationBTNAddGSTIN();
        if (!IsValid)
        {
            lblMsg.Text = msgAddGSTIN;
            ShowMessage(lblMsg.Text, false);
            return;
        }

        if (!GstInValid(ddlGSTINState.SelectedValue))
        {
            txtGSTIN.Focus();
            return;
        }

        BindGRDGSTIN();
        ClearAllAfterAddGSTIN();
        txtGSTIN.Focus();
        DisplayOrNot();
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
        if (!ValidDate) // For Shipping Date Between Financial Year.
        {
            lblMsg.Text = "Registration Date Should Be Within Financial Year Date Or Not More Than Todays Date!";
            msgAddGSTIN = lblMsg.Text;
            txtRegistrationDate.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtRegistrationAddress.Text))
        {
            lblMsg.Text = "Enter Registration Address.";
            msgAddGSTIN = lblMsg.Text;
            txtRegistrationAddress.Focus();
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
                Msg = lblMsg.Text;
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
    DataTable DtTermsCondition()
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
                ShowMessage("Enter Terms & Conditions !", false);
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
                dt = (DataTable)ViewState["grdTerms"];
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
            txtTerms.Focus();
        }
        catch (Exception ex)
        {

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

                ViewState["grdData"] = dt;
                grdTerms.DataSource = dt;
                grdTerms.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }

    DataTable DtGSTINSchema()
    {
        DataTable dtGSTINInfo = new DataTable();
        dtGSTINInfo.Columns.Add("GSTINID", typeof(int));
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
            if (!dtGSTINInfo.Columns.Contains("GSTINID"))
            {
                dtGSTINInfo.Columns.Add("GSTINID");
            }
            if (!dtGSTINInfo.Columns.Contains("State"))
            {
                dtGSTINInfo.Columns.Add("State");
            }
        }
        int ddlCount = ddlShippingGSTIN.Items.Count + 1;

        DataRow DrGSTINInfo = dtGSTINInfo.NewRow();
        DrGSTINInfo["GSTINID"] = ddlCount;
        DrGSTINInfo["OrgID"] = GlobalSession.OrgID;
        DrGSTINInfo["BrID"] = GlobalSession.BrID;
        DrGSTINInfo["YrCD"] = GlobalSession.YrCD;
        DrGSTINInfo["AccCode"] = 0;
        DrGSTINInfo["GSTIN"] = txtGSTIN.Text.ToUpper();
        DrGSTINInfo["RegistrationDate"] = CommonCls.ConvertToDate(txtRegistrationDate.Text);
        DrGSTINInfo["RegistrationAddress"] = txtRegistrationAddress.Text;
        DrGSTINInfo["City"] = txtGSTINCity.Text;
        DrGSTINInfo["State"] = ddlGSTINState.SelectedItem.Text;
        DrGSTINInfo["StateID"] = Convert.ToInt16(ddlGSTINState.SelectedItem.Value);
        DrGSTINInfo["PinCode"] = Convert.ToInt32(txtGSTINPinCode.Text);
        DrGSTINInfo["AuthorizedSignatury"] = txtAuthorizedSignatory.Text;
        DrGSTINInfo["SignaturyDesignation"] = txtDesignation.Text;

        dtGSTINInfo.Rows.Add(DrGSTINInfo);
        grdGSTINInformation.DataSource = ViewState["dtGSTINInfo"] = ViewState["dtSameAs"] = dtGSTINInfo;
        grdGSTINInformation.DataBind();
        if (ddlShippingGSTIN.Items.Count >= 0)
        {

        }
        ddlShippingGSTIN.Items.Clear();
        ddlShippingGSTIN.DataSource = null;
        DataTable tempDt = new DataTable();
        tempDt = dtGSTINInfo;
        ddlShippingGSTIN.DataSource = tempDt;
        ddlShippingGSTIN.DataTextField = "GSTIN";
        ddlShippingGSTIN.DataValueField = "GSTINID";
        ddlShippingGSTIN.DataBind();
        ddlShippingGSTIN.Items.Insert(0, new ListItem("-- Select --", "0"));
        ddlCount = 0;
    }

    protected void grdGSTINInformation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtGSTINInfo = (DataTable)(ViewState["dtGSTINInfo"]);
            DataRow row = dtGSTINInfo.Rows[rowIndex];
            if (row != null)
            {
                string gstin = row["GSTIN"].ToString();
                ddlShippingGSTIN.Items.Remove(gstin);
            }
            dtGSTINInfo.Rows[rowIndex].Delete();
            ViewState["dtGSTINInfo"] = dtGSTINInfo;
            grdGSTINInformation.DataSource = dtGSTINInfo;
            grdGSTINInformation.DataBind();
            if (grdGSTINInformation.Rows.Count == 0)
            {
                grdGSTINInformation.DataSource = ViewState["dtGSTINInfo"] = null;
                grdGSTINInformation.DataBind();
            }
        }
        DisplayOrNot();
    }

    void ClearAllAfterAddGSTIN()
    {
        txtGSTIN.Text = txtRegistrationDate.Text = txtRegistrationAddress.Text = txtGSTINCity.Text = txtGSTINPinCode.Text = txtAuthorizedSignatory.Text = txtDesignation.Text = "";
        ddlGSTINState.ClearSelection();
    }

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
                string nextTen = txtGSTIN.Text.Substring(2, 10).ToUpper();

                if (stateValue != firstTwo)
                {
                    ShowMessage("Invalid GSTIN No.", false);
                    return false;
                }
                if (!string.IsNullOrEmpty(txtPanNo.Text))
                {
                    if (txtPanNo.Text != nextTen)
                    {
                        ShowMessage("Invalid GSTIN No.", false);
                        return false;
                    }
                }
                else
                {

                }
            }
        }
        else
        {
            ShowMessage("Enter 15 Digit GSTIN No.", false);
            return false;
        }
        return true;
    }

    protected void chkSameAsGSTINAddress_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (chkSameAsGSTINAddress.Checked == true)
        {
            if (ddlShippingGSTIN.SelectedItem != null)
            {
                int id = Convert.ToInt32(ddlShippingGSTIN.SelectedValue);
                if (id != 0)
                {
                    ddlShippingGSTIN.Enabled = false;
                    ddlShippingState.Enabled = false;
                    txtShippingCity.Enabled = false;
                    txtShippingAddress.Enabled = false;
                    txtShippingPincode.Enabled = false;

                    DataTable dtSameAs = (DataTable)ViewState["dtSameAs"];
                    DataRow[] dr = dtSameAs.Select("GSTINID = " + id + "");
                    if (dr.Length > 0)
                    {
                        foreach (DataRow row in dr)
                        {
                            txtShippingCity.Text = row["City"].ToString();
                            txtShippingAddress.Text = row["RegistrationAddress"].ToString();
                            ddlShippingState.SelectedValue = row["StateID"].ToString();
                            txtShippingPincode.Text = row["PinCode"].ToString();
                        }
                    }
                }
                else
                {
                    ddlShippingGSTIN.Enabled = true;
                    ddlShippingState.Enabled = true;
                    txtShippingCity.Enabled = true;
                    txtShippingAddress.Enabled = true;
                    txtShippingPincode.Enabled = true;

                    txtShippingCity.Text = "";
                    txtShippingAddress.Text = "";
                    ddlShippingState.SelectedIndex = 0;
                    txtShippingPincode.Text = "";
                    ShowMessage("Select Shipping GSTIN.", false);
                    ddlShippingGSTIN.Focus();
                    chkSameAsGSTINAddress.Checked = false;
                }
            }
        }
        else
        {
            ddlShippingGSTIN.Enabled = true;
            ddlShippingState.Enabled = true;
            txtShippingCity.Enabled = true;
            txtShippingAddress.Enabled = true;
            txtShippingPincode.Enabled = true;

            txtShippingCity.Text = "";
            txtShippingAddress.Text = "";
            ddlShippingGSTIN.ClearSelection();
            txtShippingPincode.Text = "";
        }
        DisplayOrNot();
    }

    protected void btnAddShipping_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        bool IsValid = ValidationBTNAddShipping();
        if (!IsValid)
        {
            lblMsg.Text = msgAddShipping;
            ShowMessage(lblMsg.Text, false);
            return;
        }

        if (ddlShippingGSTIN.SelectedItem != null)
        {
            if (ddlShippingGSTIN.SelectedValue != "0")
            {
                if (!ShippingGstInValid(ddlShippingState.SelectedValue))
                {
                    ddlShippingGSTIN.Focus();
                    return;
                }
            }
        }

        BindGRDShipping();
        ClearAllAfterAddShipping();
        ddlShippingGSTIN.Focus();
        chkSameAsGSTINAddress.Checked = false;
        DisplayOrNot();
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

        return dtShippingInfo;
    }

    void BindGRDShipping()
    {
        DataTable dtShippingInfo = new DataTable();
        if (ViewState["dtShippingInfo"] == null)
        {
            dtShippingInfo = DtShippingSchema();
        }
        else
        {
            dtShippingInfo = (DataTable)ViewState["dtShippingInfo"];
            if (!dtShippingInfo.Columns.Contains("State"))
            {
                dtShippingInfo.Columns.Add("State");
            }
        }

        DataRow DrShippingInfo = dtShippingInfo.NewRow();

        DrShippingInfo["OrgID"] = GlobalSession.OrgID;
        DrShippingInfo["BrID"] = GlobalSession.BrID;
        DrShippingInfo["YrCD"] = GlobalSession.YrCD;
        DrShippingInfo["AccCode"] = 0;
        DrShippingInfo["GSTIN"] = ddlShippingGSTIN.SelectedItem == null ? "" : ddlShippingGSTIN.SelectedItem.Text;
        DrShippingInfo["GSTINInd"] = ddlShippingGSTIN.SelectedItem == null ? 0 : Convert.ToInt32(ddlShippingGSTIN.SelectedItem.Value);
        DrShippingInfo["ShippingAddress"] = txtShippingAddress.Text;
        DrShippingInfo["City"] = txtShippingCity.Text;
        DrShippingInfo["State"] = ddlShippingState.SelectedItem.Text;
        DrShippingInfo["StateID"] = Convert.ToInt16(ddlShippingState.SelectedItem.Value);
        DrShippingInfo["Pincode"] = Convert.ToInt32(txtShippingPincode.Text);

        dtShippingInfo.Rows.Add(DrShippingInfo);
        grdShippingInformation.DataSource = ViewState["dtShippingInfo"] = dtShippingInfo;
        grdShippingInformation.DataBind();
    }

    protected void grdShippingInformation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtShippingInfo = (DataTable)(ViewState["dtShippingInfo"]);
            dtShippingInfo.Rows[rowIndex].Delete();
            ViewState["dtShippingInfo"] = dtShippingInfo;
            grdShippingInformation.DataSource = dtShippingInfo;
            grdShippingInformation.DataBind();
            if (grdShippingInformation.Rows.Count == 0)
            {
                grdShippingInformation.DataSource = ViewState["dtShippingInfo"] = null;
                grdShippingInformation.DataBind();
            }
        }
        DisplayOrNot();
    }

    void ClearAllAfterAddShipping()
    {
        ddlShippingGSTIN.ClearSelection();
        txtShippingAddress.Text = txtShippingCity.Text = txtShippingPincode.Text = "";
        ddlShippingState.ClearSelection();

        ddlShippingGSTIN.Enabled = true;
        ddlShippingState.Enabled = true;
        txtShippingCity.Enabled = true;
        txtShippingAddress.Enabled = true;
        txtShippingPincode.Enabled = true;
    }

    bool ShippingGstInValid(string StateID)
    {
        string stateValue;
        if (ddlShippingGSTIN.SelectedItem != null)
        {
            if (ddlShippingGSTIN.SelectedValue != "0")
            {
                if (StateID.Length == 1)
                {
                    stateValue = "0" + Convert.ToString(StateID);
                }
                else
                {
                    stateValue = Convert.ToString(StateID);
                }
                string firstTwo = ddlShippingGSTIN.SelectedItem.Text.Substring(0, 2);

                if (stateValue != firstTwo)
                {
                    ShowMessage("Invalid GSTIN No.", false);
                    return false;
                }
            }
            //else
            //{
            //    ShowMessage("Enter 15 Digit GSTIN No.", false);
            //    return false;
            //}
        }
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        bool IsValid = ValidationBTNSave();
        if (!IsValid)
        {
            lblMsg.Text = Msg;
            ShowMessage(Msg, false);
            return;
        }

        if (!string.IsNullOrEmpty(txtGSTIN.Text))
        {
            pnlConfirm.Visible = true;
            lblConfirmationMSG.Text = "Do You Want To Save GSTIN Information, Please Click On Add Button.";
            btnYes.Focus();
            return;
        }

        objAccountHead = new AccountHeadModel();
        objAccountHead.Ind = 1;
        objAccountHead.OrgID = GlobalSession.OrgID;
        objAccountHead.BrID = GlobalSession.BrID;
        objAccountHead.YrCD = GlobalSession.YrCD;
        objAccountHead.EntryType = 1;
        objAccountHead.User = GlobalSession.UserID;
        objAccountHead.IP = GlobalSession.IP;
        objAccountHead.CompositionOpted = Convert.ToInt16(ddlCompositionOpted.SelectedValue);
        objAccountHead.ReffPartyCode = txtOtherDetails.Text;
        objAccountHead.IsSubDealer = Convert.ToInt16(ddlSubDealer.SelectedValue);
        objAccountHead.DiscountRate = CommonCls.ConvertDecimalZero(txtDiscountRate.Text);

        objAccountHead.AccountHeadHindi = txtAccountHeadHindi.Text;

        //objAccountHead.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerageRate.Text);
        //For Brokerage Rate
        objAccountHead.BrokerageRate = CommonCls.ConvertDecimalZero(txtBrokerRate.Text);

        objAccountHead.TaxCalcForSEZParty = CommonCls.ConvertIntZero(ddlExportCategory.SelectedValue) == 3 ? CommonCls.ConvertIntZero(ddlTaxCalType.SelectedValue) : 0;
        if ((DataTable)ViewState["grdTerms"] == null || ((DataTable)ViewState["grdTerms"]).Rows.Count <= 0)
        {
            objAccountHead.TblAccTerms = DtTermsCondition();
            DataRow Dr = objAccountHead.TblAccTerms.NewRow();
            Dr["OrgID"] = GlobalSession.OrgID;
            Dr["BrID"] = GlobalSession.BrID;
            Dr["AccCode"] = 0;
            Dr["Terms"] = "";
            Dr["UserID"] = GlobalSession.UserID;
            Dr["IP"] = GlobalSession.IP;
            objAccountHead.TblAccTerms.Rows.Add(Dr);
        }
        else
            objAccountHead.TblAccTerms = (DataTable)ViewState["grdTerms"];

        objAccountHead.DtAccount = DtAccountHeadSchema();
        objAccountHead.DtAccGSTIN = DtGSTINSchema();
        objAccountHead.DtAccPOS = DtShippingSchema();
        objAccountHead.DtAccount = CreateAccountHeadData();
        objAccountHead.DtAccGSTIN = (DataTable)ViewState["dtGSTINInfo"];
        objAccountHead.DtAccPOS = (DataTable)ViewState["dtShippingInfo"];
        if (objAccountHead.DtAccGSTIN != null)
        {
            if (objAccountHead.DtAccGSTIN.Columns.Contains("State"))
            {
                objAccountHead.DtAccGSTIN.Columns.Remove("State");
            }
            if (objAccountHead.DtAccGSTIN.Columns.Contains("GSTINID"))
            {
                objAccountHead.DtAccGSTIN.Columns.Remove("GSTINID");
            }
        }
        if (objAccountHead.DtAccPOS != null)
        {
            if (objAccountHead.DtAccPOS.Columns.Contains("State"))
            {
                objAccountHead.DtAccPOS.Columns.Remove("State");
            }
        }

        if ((objAccountHead.DtAccGSTIN == null) || (objAccountHead.DtAccGSTIN.Rows.Count <= 0))
        {
            objAccountHead.DtAccGSTIN = DtGSTINSchema();
            DataRow drGSTINInfo = objAccountHead.DtAccGSTIN.NewRow();

            drGSTINInfo["OrgID"] = GlobalSession.OrgID;
            drGSTINInfo["BrID"] = GlobalSession.BrID;
            drGSTINInfo["YrCD"] = GlobalSession.YrCD;
            drGSTINInfo["AccCode"] = 0;
            drGSTINInfo["GSTIN"] = "";
            drGSTINInfo["RegistrationDate"] = CommonCls.ConvertToDate(txtRegistrationDate.Text);
            drGSTINInfo["RegistrationAddress"] = "";
            drGSTINInfo["City"] = "";
            drGSTINInfo["StateID"] = 0;
            drGSTINInfo["PinCode"] = 0;
            drGSTINInfo["AuthorizedSignatury"] = "";
            drGSTINInfo["SignaturyDesignation"] = "";
            objAccountHead.DtAccGSTIN.Rows.Add(drGSTINInfo);
            objAccountHead.DtAccGSTIN.Columns.Remove("State");
            objAccountHead.DtAccGSTIN.Columns.Remove("GSTINID");
        }

        if ((objAccountHead.DtAccPOS == null) || (objAccountHead.DtAccPOS.Rows.Count <= 0))
        {
            objAccountHead.DtAccPOS = DtShippingSchema();
            DataRow drShippingInfo = objAccountHead.DtAccPOS.NewRow();
            drShippingInfo["OrgID"] = GlobalSession.OrgID;
            drShippingInfo["BrID"] = GlobalSession.BrID;
            drShippingInfo["YrCD"] = GlobalSession.YrCD;
            drShippingInfo["AccCode"] = 0;
            drShippingInfo["GSTINInd"] = 0;
            drShippingInfo["ShippingAddress"] = txtClientAddress.Text;
            drShippingInfo["City"] = txtClientCity.Text;
            drShippingInfo["StateID"] = CommonCls.ConvertIntZero(ddlClientState.SelectedValue);
            drShippingInfo["PinCode"] = CommonCls.ConvertIntZero(txtClientPincode.Text);
            objAccountHead.DtAccPOS.Rows.Add(drShippingInfo);
            objAccountHead.DtAccPOS.Columns.Remove("State");
        }
        string uri = string.Format("AccountHead/SaveAccountHead");
        DataTable dtSave = CommonCls.ApiPostDataTable(uri, objAccountHead);
        if (dtSave.Rows.Count > 0)
        {
            if (dtSave.Rows[0]["Column1"].ToString() == "0")
            {
                lblMsg.Text = "Record Not Save Please Try Again.";
                ShowMessage(lblMsg.Text, false);
                DisplayOrNot();
            }
            else if (dtSave.Rows[0]["Column1"].ToString() == "1")
            {
                ClearAll();
                lblMsg.Text = "Record Save successfully.";
                ShowMessage(lblMsg.Text, true);
                ddlGroupName.Focus();
                LoadAccoundHeadDDL();//11-01-2018
            }
            else if (dtSave.Rows[0]["Column1"].ToString() == "2")
            {
                lblMsg.Text = "Duplicate Record.";
                ShowMessage(lblMsg.Text, false);
                txtAccountHead.Focus();
                DisplayOrNot();
            }

        }
        else
        {
            lblMsg.Text = "Record Not Save Please Try Again.";
            ShowMessage(lblMsg.Text, false);
            DisplayOrNot();
        }
    }

    string Msg;
    bool ValidationBTNSave()
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

        //if (string.IsNullOrEmpty(txtAccountHeadHindi.Text))
        //{
        //    lblMsg.Text = "Enter Account Head(Hindi).";
        //    Msg = lblMsg.Text;
        //    txtAccountHeadHindi.Focus();
        //    return false;
        //}
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

        dtAccountHead.Columns.Add("BrokerageLimit", typeof(decimal));
        dtAccountHead.Columns.Add("Ind2", typeof(string));
        dtAccountHead.Columns.Add("Ind3", typeof(string));
        dtAccountHead.Columns.Add("Remark1", typeof(string));
        dtAccountHead.Columns.Add("Remark2", typeof(string));
        dtAccountHead.Columns.Add("Remark3", typeof(string));



        return dtAccountHead;
    }

    DataTable CreateAccountHeadData()
    {
        DataTable dtAccountHeadData = new DataTable();

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
        drAccountHead["PanNo"] = txtPanNo.Text;
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

        drAccountHead["BrokerageLimit"] = CommonCls.ConvertDecimalZero(txtBrokerageLimit.Text);//For BrokerageLimit
        if (ddlBrokerageType.SelectedValue == "1")
        {
            drAccountHead["Ind2"] = "1";//For BrokerageType is BrokerageRate Then IND2=1 is given 
        }
        else
        {
            drAccountHead["Ind2"] = "2";//For BrokerageType is Absolute Value Then IND2=2 is given 
        }
        drAccountHead["Ind3"] = "0";
        drAccountHead["Remark1"] = "";
        drAccountHead["Remark2"] = "";
        drAccountHead["Remark3"] = "";


        dtAccountHeadData.Rows.Add(drAccountHead);
        return dtAccountHeadData;
    }

    void ClearAll()
    {
        //Client Information
        txtAccountHeadHindi.Text = "";

        lblMsg.Text = "";
        ddlGroupName.ClearSelection();
        txtAccountHead.Text = txtClientAddress.Text = txtClientCity.Text = lblGroupDescription.Text = txtClientPincode.Text =
            txtOpeningBalance.Text = txtMerchantID.Text = txtPanNo.Text = txtOtherDetails.Text = txtBrokerageLimit.Text = txtBrokerRate.Text = "";
        trBrokerage.Visible = false;
        ddlClientState.ClearSelection();
        ddlDrCr.ClearSelection();
        ddlTDS.ClearSelection();
        ddlISD.ClearSelection();
        ddlRCM.ClearSelection();
        ddlTCS.ClearSelection();
        ddlBrokerageType.ClearSelection();
        ViewState["grdTerms"] = null;
        grdTerms.DataSource = new DataTable();
        grdTerms.DataBind();
        //Contact Information

        txtPersonName.Text = txtMobileNo.Text = txtPhone.Text = txtEmail.Text = txtRemark.Text = "";
        txtBank.Text = txtBranch.Text = txtIFSCCode.Text = txtAccountNo.Text = "";
        ddlExportCategory.ClearSelection();

        //Clear GSTINInfo and ShippingInfo ViewState

        ViewState["dtGSTINInfo"] = ViewState["dtShippingInfo"] = null;
        grdGSTINInformation.DataSource = grdShippingInformation.DataSource = ddlShippingGSTIN.DataSource = new DataTable();
        grdGSTINInformation.DataBind();
        grdShippingInformation.DataBind();
        ddlShippingGSTIN.DataBind();
        chkGSTINNotAvailable.Checked = false;

        txtClientAddress.Enabled = txtClientCity.Enabled = ddlClientState.Enabled = txtClientPincode.Enabled =
            txtPanNo.Enabled = ddlDrCr.Enabled = ddlCompositionOpted.Enabled = txtOtherDetails.Enabled =
            ddlTDS.Enabled = ddlISD.Enabled = ddlRCM.Enabled = ddlTCS.Enabled = true;

        txtPersonName.Enabled = txtMobileNo.Enabled = txtPhone.Enabled = txtEmail.Enabled = txtRemark.Enabled = true;

        txtBank.Enabled = txtBranch.Enabled = txtIFSCCode.Enabled = txtAccountNo.Enabled = ddlExportCategory.Enabled = true;

        txtGSTIN.Enabled = txtRegistrationDate.Enabled = txtRegistrationAddress.Enabled = txtGSTINCity.Enabled =
            ddlGSTINState.Enabled = txtGSTINPinCode.Enabled = txtAuthorizedSignatory.Enabled =
            txtDesignation.Enabled = btnAddGSTIN.Enabled = true;

        ddlShippingGSTIN.Enabled = chkSameAsGSTINAddress.Enabled = txtShippingAddress.Enabled = txtShippingCity.Enabled =
            ddlShippingState.Enabled = txtShippingPincode.Enabled = btnAddShipping.Enabled = true;

        tdAccountHeadHeader.Style.Add("display", "none");
        tdAccountHeadBody.Style.Add("display", "none");
        tdContactInfoHeader.Style.Add("display", "");
        tdContactAndBankBody.Style.Add("display", "");
        tdShippingInfoHeader.Style.Add("display", "");
        tdShippingBody.Style.Add("display", "");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        ddlGroupName.Focus();
    }

    protected void ddlTCS_SelectedIndexChanged(object sender, EventArgs e)
    {
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
        btnAddGSTIN.Focus();
        pnlConfirm.Visible = false;
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        ClearAllAfterAddGSTIN();
        btnSave_Click(sender, e);
        pnlConfirm.Visible = false;
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void ddlBrokerageType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}