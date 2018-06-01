using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmAddGSTIN : System.Web.UI.Page
{
    AddGSTINModel objAddGSTINModel;

    public DataTable VsDtAddGSTINInBranch
    {
        get { return (DataTable)ViewState["dtAddGSTINInBranch"]; }
        set { ViewState["dtAddGSTINInBranch"] = value; }
    }
    public DataTable VsDtBranches
    {
        get { return (DataTable)ViewState["DtBranches"]; }
        set { ViewState["DtBranches"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtGSTIN.Focus();
            LoadData();
        }
        lblMsg.Text = lblMsg.CssClass = "";
    }

    void LoadData()
    {
        objAddGSTINModel = new AddGSTINModel();
        objAddGSTINModel.Ind = 1;
        objAddGSTINModel.OrgID = GlobalSession.OrgID;

        string uri = string.Format("AddGSTIN/LoadData");
        DataSet dsLoadData = CommonCls.ApiPostDataSet(uri, objAddGSTINModel);
        if (dsLoadData.Tables.Count > 0)
        {
            DataTable dtLoadState = dsLoadData.Tables[0];
            VsDtBranches = dsLoadData.Tables[1];
            DataTable dtStatePanNo = dsLoadData.Tables[2];

            ddlState.DataSource = dtLoadState;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateID";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("-- State --", "0"));


            hfStateID.Value = dtStatePanNo.Rows[0]["StateID"].ToString();
            hfPanNo.Value = dtStatePanNo.Rows[0]["PANNo"].ToString();
            //chkLstBranch.DataSource = VsDtBranches;
            //chkLstBranch.DataTextField = "BranchName";
            //chkLstBranch.DataValueField = "BranchID";
            //chkLstBranch.DataBind();

            //dtStatePanNo.Rows[0][""];
            //dtStatePanNo.Rows[0][""];

            if (GlobalSession.UnRegisterClient == 1)
            {
                ShowMessageOnPopUp("You are not Authorized For Add GSTIN. Please Contact To Admin.", false, "../Defaults/Default.aspx");
                return;
            }
        }
        else
        {
            ShowMessage("No Data Found.", false);
        }
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (VsDtBranches.Rows.Count > 0)
        {
            DataView dvBranchs = new DataView(VsDtBranches);
            dvBranchs.RowFilter = "StateID=" + ddlState.SelectedValue;
            if (dvBranchs.ToTable().Rows.Count > 0)
            {
                chkLstBranch.DataSource = dvBranchs;
                chkLstBranch.DataTextField = "BranchName";
                chkLstBranch.DataValueField = "BranchID";
                chkLstBranch.DataBind();
                pnlBranch.Visible = true;
            }
            else
            {
                chkLstBranch.DataSource = dvBranchs;
                chkLstBranch.DataTextField = "BranchName";
                chkLstBranch.DataValueField = "BranchID";
                chkLstBranch.DataBind();
                pnlBranch.Visible = false;
            }

        }
        ddlState.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        lblMsg.Text = lblMsg.CssClass = "";
        if (!ValidationBtnSate())
        {
            return;
        }

        if (!GstInValid(ddlState.SelectedValue))
        {
            return;
        }

        objAddGSTINModel = new AddGSTINModel();
        objAddGSTINModel.Ind = 2;
        objAddGSTINModel.OrgID = GlobalSession.OrgID;

        objAddGSTINModel.GSTIN = txtGSTIN.Text;
        objAddGSTINModel.RegAddr = txtRegAddress.Text;
        objAddGSTINModel.RegDate = CommonCls.ConvertToDate(txtRegDate.Text);
        objAddGSTINModel.City = txtCity.Text;
        objAddGSTINModel.StateID = Convert.ToInt32(ddlState.SelectedValue);
        objAddGSTINModel.PinCode = CommonCls.ConvertIntZero(txtPinCode.Text);
        objAddGSTINModel.AuthorizedSignatury = txtAuthorizedSign.Text;
        objAddGSTINModel.SignaturyDesignation = txtAuthorizedDesi.Text;
        objAddGSTINModel.DtSeries = VsDtSeries;

        objAddGSTINModel.User = GlobalSession.UserID;
        objAddGSTINModel.IP = GlobalSession.IP;

        DataTable dtAddGSTINInBranchBranch = new DataTable();
        VsDtAddGSTINInBranch = null;
        if (VsDtAddGSTINInBranch == null)
        {
            dtAddGSTINInBranchBranch = new DataTable();
            dtAddGSTINInBranchBranch.Columns.Add("BranchID", typeof(int));
            dtAddGSTINInBranchBranch.Columns.Add("BranchName", typeof(string));
            dtAddGSTINInBranchBranch.Columns.Add("ExtraInd", typeof(int));
        }
        else
            dtAddGSTINInBranchBranch = VsDtAddGSTINInBranch;

        foreach (ListItem lstItem in chkLstBranch.Items)
        {
            if (lstItem.Selected)
            {
                DataRow drBranch = dtAddGSTINInBranchBranch.NewRow();
                drBranch["BranchID"] = Convert.ToInt32(lstItem.Value);
                drBranch["BranchName"] = lstItem.Text;
                drBranch["ExtraInd"] = 1;
                dtAddGSTINInBranchBranch.Rows.Add(drBranch);
            }
        }
        VsDtAddGSTINInBranch = dtAddGSTINInBranchBranch;
        objAddGSTINModel.DtAddGSTINInBranch = VsDtAddGSTINInBranch;

        if (objAddGSTINModel.DtAddGSTINInBranch == null || objAddGSTINModel.DtAddGSTINInBranch.Rows.Count <= 0)
        {
            DataRow drBranch = objAddGSTINModel.DtAddGSTINInBranch.NewRow();
            drBranch["BranchID"] = 0;
            drBranch["BranchName"] = "";
            drBranch["ExtraInd"] = 0;
            objAddGSTINModel.DtAddGSTINInBranch.Rows.Add(drBranch);
        }

        string uri = string.Format("AddGSTIN/SaveGSTIN");
        DataTable dtSaveGSTIN = CommonCls.ApiPostDataTable(uri, objAddGSTINModel);
        if (dtSaveGSTIN.Rows.Count > 0)
        {
            if (dtSaveGSTIN.Rows[0]["ReturnInd"].ToString() == "1")
            {
                ClearAll();
                ShowMessage("GSTIN Save Successfully.", true);
            }
            else if (dtSaveGSTIN.Rows[0]["ReturnInd"].ToString() == "2")
            {
                ShowMessage("Duplicate GSTIN No.", false);
                txtGSTIN.Focus();
            }
        }
    }

    bool ValidationBtnSate()
    {
        if (string.IsNullOrEmpty(txtGSTIN.Text))
        {
            txtGSTIN.Focus();
            ShowMessage("Enter GSTIN No.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtRegAddress.Text))
        {
            txtRegAddress.Focus();
            ShowMessage("Enter Address.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtRegDate.Text))
        {
            txtRegDate.Focus();
            ShowMessage("Enter Date.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtCity.Text))
        {
            txtCity.Focus();
            ShowMessage("Enter City.", false);
            return false;
        }
        if (Convert.ToInt32(ddlState.SelectedValue) == 0)
        {
            ddlState.Focus();
            ShowMessage("Select State.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtPinCode.Text))
        {
            txtPinCode.Focus();
            ShowMessage("Enter PinCode.", false);
            return false;
        }
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
                ddlSrNoAuto.Focus();
                return false;
            }
            DataRow[] drCash = VsDtSeries.Select("CashCreditInd=1");
            if (drCash.Count() == 0)
            {
                ShowMessage("Atlease One Series Compulsory For Cash.", false);
                ddlSrNoAuto.Focus();
                return false;
            }
        }
        //if (CommonCls.(txtGSTIN.Text))
        //{
        //    txtGSTIN.Focus();
        //    ShowMessage("Enter GSTIN No.", false);
        //    return false;
        //}
        return true;
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
                if (!CommonCls.GSTINIsValid(txtGSTIN.Text))
                {
                    ShowMessage("Invalid GSTIN Format.", false);
                    return false;
                }

                string firstTwo = txtGSTIN.Text.Substring(0, 2);
                string nextTen = txtGSTIN.Text.Substring(2, 10).ToUpper();

                if (stateValue != firstTwo)
                {
                    ShowMessage("GSTIN No Or State Code Not Match.", false);
                    return false;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ClearAll()
    {
        txtGSTIN.Text = txtRegAddress.Text = txtRegDate.Text = txtCity.Text = txtPinCode.Text = txtAuthorizedDesi.Text = txtAuthorizedSign.Text = "";
        ddlState.ClearSelection();
        txtGSTIN.Focus();

        VsDtAddGSTINInBranch = null;
        pnlBranch.Visible = false;

        ClearSeries();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    #region

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
        drSeries["CashCreditInd"] = CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == -1 ? 3 : CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue);
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
                btnAddSeries.Enabled = ddlSrNoAuto.Enabled = false;

                txtSeries.Text = txtSerialNo.Text = "";
                ddlSrNoAuto.ClearSelection();
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
                break;

            case 3: /// Default Series
                break;
        }
        return true;
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
    }

    protected void gvCreateSeries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblCashCreditInd = (Label)e.Row.FindControl("lblCashCreditInd");
                if (string.IsNullOrEmpty(lblCashCreditInd.Text) || CommonCls.ConvertIntZero(lblCashCreditInd.Text) == 3)
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
    }

    #endregion
}