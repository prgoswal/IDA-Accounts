using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FDControl_FrmFDEntry : System.Web.UI.Page
{
    FDEntryModel objFDEntryModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAll();
        }
    }

    private void BindAll()
    {
        try
        {
            objFDEntryModel = new FDEntryModel();
            objFDEntryModel.Ind = 1;
            objFDEntryModel.OrgID = GlobalSession.OrgID;
            objFDEntryModel.BrID = GlobalSession.BrID;
            objFDEntryModel.YrCD = GlobalSession.YrCD;
            objFDEntryModel.VchType = 4;

            string uri = string.Format("FixedDeposit/BindAll");


            DataSet dsFD = CommonCls.ApiPostDataSet(uri, objFDEntryModel);
            if (dsFD.Tables.Count > 0)
            {
                if (dsFD.Tables[0].Rows.Count > 0)
                {

                    ddlAccountHead.DataSource = dsFD.Tables[0];
                    ddlAccountHead.DataTextField = "AccName";
                    ddlAccountHead.DataValueField = "AccCode";
                    ddlAccountHead.DataBind();
                    if (dsFD.Tables[0].Rows.Count > 1)
                    {
                        ddlAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (string.IsNullOrEmpty(txtDepositeYear.Text) && string.IsNullOrEmpty(txtDepositeMonth.Text) && string.IsNullOrEmpty(txtDepositeDay.Text))
            {
                ShowMessage("Enter Deposite Term", false);
                txtDepositeYear.Focus();
                return;
            }


            if (Convert.ToDateTime(txtDespositDate.Text) > Convert.ToDateTime(txtMaturityDate.Text))
            {
                ShowMessage("Maturity Date Should Be Equal to Desposit Date Or More Than Desposit Date!", false);
                txtMaturityDate.Focus();
                return;
            }
            if (rbtnIsODLien.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(txtLienAmount.Text))
                {
                    ShowMessage("Enter Lien Amount.", false);
                    txtLienAmount.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtLienDate.Text))
                {
                    ShowMessage("Enter Lien Date.", false);
                    txtLienDate.Focus();
                    return;
                }
                else if (Convert.ToDateTime(txtDespositDate.Text) >= Convert.ToDateTime(txtLienDate.Text))
                {
                    ShowMessage("Lien Date Should be More Than Desposit Date.", false);
                    txtLienDate.Focus();
                    return;
                }
            }

            objFDEntryModel = new FDEntryModel();
            objFDEntryModel.Ind = 2;
            objFDEntryModel.OrgID = GlobalSession.OrgID;
            objFDEntryModel.BrID = GlobalSession.BrID;
            objFDEntryModel.YrCD = GlobalSession.YrCD;
            objFDEntryModel.DespositDate = CommonCls.ConvertToDate(txtDespositDate.Text);
            objFDEntryModel.BankCode = CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue);
            objFDEntryModel.FDNumber = txtFDNo.Text;
            objFDEntryModel.MaturityDate = CommonCls.ConvertToDate(txtMaturityDate.Text);
            objFDEntryModel.DepositAmount = CommonCls.ConvertDecimalZero(txtDepositAmt.Text);
            objFDEntryModel.MaturityAmount = CommonCls.ConvertDecimalZero(txtMaturityAmt.Text);

            objFDEntryModel.ROI = CommonCls.ConvertDecimalZero(txtROI.Text);
            objFDEntryModel.FDRAccNumber = txtFDRAccNo.Text;

            objFDEntryModel.PeriodValue = 0;
            objFDEntryModel.PeriodType = "";

            objFDEntryModel.SchemeType = txtSchemeType.Text;
            objFDEntryModel.LienDate = CommonCls.ConvertToDate(txtLienDate.Text);
            objFDEntryModel.LienAmount = CommonCls.ConvertDecimalZero(txtLienAmount.Text);




            objFDEntryModel.DepositYear = CommonCls.ConvertIntZero(txtDepositeYear.Text);
            objFDEntryModel.DepositMonth = CommonCls.ConvertIntZero(txtDepositeMonth.Text);
            objFDEntryModel.DepositDay = CommonCls.ConvertIntZero(txtDepositeDay.Text);


            objFDEntryModel.Narration = txtNarration.Text;
            if (rbtnIsODLien.SelectedValue == "1")
            {
                objFDEntryModel.IsODLien = true;
            }
            else
            {
                objFDEntryModel.IsODLien = false;

            }


            objFDEntryModel.User = GlobalSession.UserID;

            objFDEntryModel.IP = GlobalSession.IP;

            string uri = string.Format("FixedDeposit/SaveFixedDeposit");

            DataTable dtFD = CommonCls.ApiPostDataTable(uri, objFDEntryModel);
            if (CommonCls.ConvertIntZero(dtFD.Rows[0]["Column1"].ToString()) > 0)
            {
                ShowMessage("Fixed Deposit is Saved Successfully.", true);
                ClearText();
            }
            else
            {
                ShowMessage("Fixed Deposit is not Saved.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void ClearText()
    {
        ddlAccountHead.ClearSelection();
        txtDepositAmt.Text = "";
        txtDepositeYear.Text = "";
        txtROI.Text = "";
        txtDepositeDay.Text = "";
        txtDepositeMonth.Text = "";
        txtDepositeYear.Text = "";
        txtFDNo.Text = "";
        txtFDRAccNo.Text = "";
        txtNarration.Text = "";
        txtMaturityDate.Text = "";
        txtMaturityAmt.Text = "";
        txtDespositDate.Text = "";
        txtLienAmount.Text = "";
        txtLienDate.Text = "";
        txtSchemeType.Text = "";
        rbtnIsODLien.SelectedValue = "0";
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearText();
        lblMsg.Text = lblMsg.CssClass = "";
    }
    protected void rbtnIsODLien_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnIsODLien.SelectedValue == "1")
        {
            if (string.IsNullOrEmpty(txtLienAmount.Text))
            {
                ShowMessage("Enter Lien Amount.", false);
                txtLienAmount.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLienDate.Text))
            {
                ShowMessage("Enter Lien Date.", false);
                txtLienDate.Focus();
                return;
            }
        }
        else
        {
            lblMsg.CssClass = lblMsg.Text = "";
        }
    }
}