using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmCompanyBranchMaster : System.Web.UI.Page
{
    BranchMasterModel objBMModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";
        if (!IsPostBack)
        {
            LoadState();
        }
    }

    void LoadState()
    {
        try
        {
            objBMModel = new BranchMasterModel();
            objBMModel.Ind = 1;
            objBMModel.OrgID = GlobalSession.OrgID;
            string uri = string.Format("BranchMaster/LoadBMDDL");
            DataSet dsBMDDL = CommonCls.ApiPostDataSet(uri, objBMModel);
            if (dsBMDDL.Tables.Count > 0)
            {
                DataTable dtGSTIN = dsBMDDL.Tables[0];
                DataTable dtState = dsBMDDL.Tables[1];

                if (dtGSTIN.Rows.Count > 0)
                {
                    if (dtGSTIN.Rows.Count > 1)
                    {
                        ddlGSTINNO.DataSource = dtGSTIN;
                        ddlGSTINNO.DataTextField = "GSTIN";
                        ddlGSTINNO.DataValueField = "GSTINID";
                        ddlGSTINNO.DataBind();
                        ddlGSTINNO.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                    else
                    {
                        ddlGSTINNO.DataSource = dtGSTIN;
                        ddlGSTINNO.DataTextField = "GSTIN";
                        ddlGSTINNO.DataValueField = "GSTINID";
                        ddlGSTINNO.DataBind();
                    }
                }
                if (dtState.Rows.Count > 0)
                {
                    ddlState.DataSource = dtState;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateID";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void ddlGSTINNO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlGSTINNO.SelectedItem != null)
            {
                if (Convert.ToInt32(ddlGSTINNO.SelectedValue) == 0)
                {
                    ddlGSTINNO.Focus();
                    txtAddress.Text = txtCity.Text = txtPinCode.Text = "";
                    ddlState.ClearSelection();
                    return;
                }
            }
            objBMModel = new BranchMasterModel();
            objBMModel.Ind = 2;
            objBMModel.OrgID = GlobalSession.OrgID;
            objBMModel.GSTINID = CommonCls.ConvertIntZero(ddlGSTINNO.SelectedValue);

            string uri = string.Format("BranchMaster/LoadGSTINDetails");
            DataTable dtGSTINDetails = CommonCls.ApiPostDataTable(uri, objBMModel);
            if (dtGSTINDetails.Rows.Count > 0)
            {
                txtAddress.Text = dtGSTINDetails.Rows[0]["RegistrationAddress"].ToString();
                txtCity.Text = dtGSTINDetails.Rows[0]["City"].ToString();
                ddlState.SelectedValue = dtGSTINDetails.Rows[0]["StateID"].ToString();
                txtPinCode.Text = dtGSTINDetails.Rows[0]["PinCode"].ToString();
            }
            ddlGSTINNO.Focus();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        if (!ValidationBtnSave())
        {
            return;
        }

        if (ddlGSTINNO.SelectedItem != null)
        {
            if (ddlGSTINNO.SelectedItem.Text != "")
            {
                if (!GstInValid(ddlState.SelectedValue))
                {
                    return;
                }
            }
        }

        objBMModel = new BranchMasterModel();
        objBMModel.Ind = 3;
        objBMModel.OrgID = GlobalSession.OrgID;
        objBMModel.BranchName = txtBranchName.Text;
        objBMModel.Address = txtAddress.Text;
        objBMModel.City = txtCity.Text;
        objBMModel.StateID = Convert.ToInt32(ddlState.SelectedValue);
        objBMModel.PinCode = Convert.ToInt32(txtPinCode.Text);
        objBMModel.InvoiceNoSeries = "";
        objBMModel.InvoiceNo = 0;
        objBMModel.User = GlobalSession.UserID;
        objBMModel.IP = GlobalSession.IP;
        objBMModel.GSTINID = Convert.ToInt32(ddlGSTINNO.SelectedValue);
        objBMModel.GSTIN = ddlGSTINNO.SelectedItem.Text;

        string uri = string.Format("BranchMaster/SaveBranch");
        DataTable dtSave = CommonCls.ApiPostDataTable(uri, objBMModel);
        if (dtSave.Rows.Count > 0)
        {
            if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
            {
                ClearAll();
                ddlGSTINNO.Focus();
                ShowMessage("Record Save successfully.", true);
            }
            else if (dtSave.Rows[0]["ReturnInd"].ToString() == "2")
            {
                txtBranchName.Focus();
                ShowMessage("Branch Already Added.", false);
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
        if (ddlGSTINNO.SelectedItem != null)
        {
            if (ddlGSTINNO.SelectedItem.Text != "")
            {
                if (Convert.ToInt32(ddlGSTINNO.SelectedValue) == 0)
                {
                    ddlGSTINNO.Focus();
                    ShowMessage("Select GSTIN No.", false);
                    return false;
                }
            }
        }
        if (string.IsNullOrEmpty(txtBranchName.Text))
        {
            txtBranchName.Focus();
            ShowMessage("Enter Branch Name.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtAddress.Text))
        {
            txtAddress.Focus();
            ShowMessage("Enter Address.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtCity.Text))
        {
            txtCity.Focus();
            ShowMessage("Enter City.", false);
            return false;
        }
        if (ddlState.SelectedValue == "0")
        {
            ddlState.Focus();
            ShowMessage("Select State.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtPinCode.Text))
        {
            txtPinCode.Focus();
            ShowMessage("Enter Pincode.", false);
            return false;
        }
        return true;
    }

    bool GstInValid(string StateID)
    {
        string stateValue;
        if (ddlGSTINNO.SelectedItem.Text.Count() == 15)
        {
            if (StateID.Length == 1)
            {
                stateValue = "0" + Convert.ToString(StateID);
            }
            else
            {
                stateValue = Convert.ToString(StateID);
            }
            if (!string.IsNullOrEmpty(ddlGSTINNO.SelectedItem.Text))
            {
                if (!CommonCls.GSTINIsValid(ddlGSTINNO.SelectedItem.Text))
                {
                    ShowMessage("Invalid GSTIN Format.", false);
                    return false;
                }

                string firstTwo = ddlGSTINNO.SelectedItem.Text.Substring(0, 2);
                string nextTen = ddlGSTINNO.SelectedItem.Text.Substring(2, 10).ToUpper();

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
        txtBranchName.Text = txtAddress.Text = txtCity.Text = txtPinCode.Text = lblMsg.Text = "";
        ddlGSTINNO.ClearSelection();
        ddlState.ClearSelection();
        ddlGSTINNO.Focus();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}