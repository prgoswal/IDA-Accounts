using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_FrmOldBalance : System.Web.UI.Page
{
    OldBalanceModel objOldBalModel;
    //DataTable dtGrdOtherCharge, dtGrdItemDetails; 

    Int16 IstEntry = 0;

    DataTable VsdtAccountHead
    {
        get { return (DataTable)ViewState["dtAccHead"]; }
        set { ViewState["dtAccHead"] = value; }
    }
    DataTable VsParty
    {
        get { return (DataTable)ViewState["VsParty"]; }
        set { ViewState["VsParty"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ddlAccountHead.Focus();
            hfAfterSaveInd.Value = "0";
            BindAll();
            BindPartyName();

        }

    }

    private void BindPartyName()
    {
        if (VsParty.Rows.Count > 0)
        {
            ddlPartyName.DataSource = VsParty;
            ddlPartyName.DataTextField = "AccName";
            ddlPartyName.DataValueField = "AccCode";
            ddlPartyName.DataBind();
            if (VsParty.Rows.Count > 1)
            {
                ddlPartyName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
    }

    void BindAll()
    {
        objOldBalModel = new OldBalanceModel();
        objOldBalModel.Ind = 1;
        objOldBalModel.OrgID = GlobalSession.OrgID;
        objOldBalModel.BrID = GlobalSession.BrID;
        objOldBalModel.YrCD = GlobalSession.YrCD;

        string uri = string.Format("OldBalanceEntry/BindAllddl");
        DataSet dsBindAll = CommonCls.ApiPostDataSet(uri, objOldBalModel);
        if (dsBindAll.Tables.Count > 0)
        {

            VsdtAccountHead = dsBindAll.Tables[0];
            DataTable dtPartyName = VsParty = dsBindAll.Tables[1];
            DataTable dtCostCenter = dsBindAll.Tables[2];

            if (VsdtAccountHead.Rows.Count > 0)
            {
                if (CommonCls.ConvertIntZero(hfAfterSaveInd.Value) == 0)
                {
                    ddlAccountHead.DataSource = VsdtAccountHead;
                    ddlAccountHead.DataTextField = "AccName";
                    ddlAccountHead.DataValueField = "AccCode";
                    ddlAccountHead.DataBind();
                    if (VsdtAccountHead.Rows.Count > 1)
                    {
                        ddlAccountHead.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }
            }

            //if (dtPartyName.Rows.Count > 0)
            //{
            //    ddlPartyName.DataSource = dtPartyName;
            //    ddlPartyName.DataTextField = "AccName";
            //    ddlPartyName.DataValueField = "AccCode";
            //    ddlPartyName.DataBind();
            //    if (dtPartyName.Rows.Count > 1)
            //    {
            //        ddlPartyName.Items.Insert(0, new ListItem("-- Select --", "0"));
            //    }
            //}

            if (dtCostCenter.Rows.Count > 0)
            {
                ddlcostCentre.DataSource = dtCostCenter;
                ddlcostCentre.DataTextField = "SectionName";
                ddlcostCentre.DataValueField = "SectionId";
                ddlcostCentre.DataBind();
                if (dtCostCenter.Rows.Count > 1)
                {
                    ddlcostCentre.Items.Insert(0, new ListItem("-- Select --", "0"));
                }

            }

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

            //try
            //{
            //    if (string.IsNullOrEmpty(ddlAccountHead.SelectedValue) || CommonCls.ConvertIntZero(ddlAccountHead.SelectedValue) == 0) // For Account Head Code Not Null Or Empty
            //    {
            //        ddlAccountHead.Focus();
            //        ShowMessage("Select Account Head.", false);
            //        return;
            //    }
            //}
            //catch (Exception)
            //{
            //    ddlAccountHead.Focus();
            //    ShowMessage("Account Value Not Available.", false);
            //    return;
            //}

            if (ddlAccountHead.SelectedValue == "0")
            {
                ddlAccountHead.Focus();
                ShowMessage("Select Account Head.", false);
                return;
            }

            if ((IstEntry == 1) && (txtAvlBal.Text == "0"))
            {
                ShowMessage("Enter Available Balance.", false);
                txtAvlBal.Focus();
                return;
            }
            //if (Convert.ToInt32(txtbookNo.Text) <= 0)
            //{
            //    ShowMessage("Enter Book No", false);
            //    txtbookNo.Focus();
            //    return;
            //}
            if (CommonCls.ConvertIntZero(txtbookNo.Text) <= 0)
            {
                ShowMessage("Enter Book No.", false);
                txtbookNo.Focus();
                return;
            }


            if (CommonCls.ConvertIntZero(txtPageNo.Text) <= 0)
            {
                ShowMessage("Enter Page No.", false);
                txtPageNo.Focus();
                return;
            }

            //if (Convert.ToInt32(txtSerNo.Text) <= 0)
            //{
            //    ShowMessage("Enter Serial No", false);
            //    txtSerNo.Focus();
            //    return;
            //}
            if (CommonCls.ConvertIntZero(txtSerNo.Text) <= 0)
            {
                ShowMessage("Enter Serial No.", false);
                txtSerNo.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtRefNo.Text) || Convert.ToInt32(txtRefNo.Text) <= 0)
            //{
            //    ShowMessage("Enter Reference No", false);
            //    txtRefNo.Focus();
            //    return;
            //}

            if (string.IsNullOrEmpty(txtRefNo.Text) || CommonCls.ConvertIntZero(txtRefNo.Text) <= 0)
            {
                ShowMessage("Enter Reference No.", false);
                txtRefNo.Focus();
                return;
            }


            if (string.IsNullOrEmpty(ddlPartyName.SelectedValue) && CommonCls.ConvertIntZero(ddlPartyName.SelectedValue) == 0)
            {
                ShowMessage("Select Party Name.", false);
                ddlPartyName.Focus();
                return;
            }

            //if (ddlPartyName.SelectedItem.Text == null)
            //{
            //    ShowMessage("Select Party Name", false);
            //    ddlPartyName.Focus();
            //    return;
            //}

            if (txtopendate.Text.Trim() == "")
            {
                txtopendate.Focus();
                ShowMessage("Please Enter Opening Date.", false);
                return;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                ShowMessage("Enter Amount No.", false);
                txtAmount.Focus();
                return;
            }

            if (CommonCls.ConvertDecimalZero(txtAvlBal.Text) < CommonCls.ConvertDecimalZero(txtAmount.Text))
            {
                ShowMessage("Amount Is Not Greater-Than Available Amount.", false);
                txtAmount.Focus();
                return;
            }

            objOldBalModel = new OldBalanceModel();
            objOldBalModel.Ind = 2;
            objOldBalModel.OrgID = GlobalSession.OrgID;
            objOldBalModel.BrID = GlobalSession.BrID;
            objOldBalModel.YrCD = GlobalSession.YrCD;
            objOldBalModel.UserID = GlobalSession.UserID;
            objOldBalModel.IPAddress = GlobalSession.IP;
            objOldBalModel.AccCode = Convert.ToInt32(ddlAccountHead.SelectedValue);
            objOldBalModel.BookNo = CommonCls.ConvertIntZero(txtbookNo.Text);
            objOldBalModel.PageNo = CommonCls.ConvertIntZero(txtPageNo.Text);
            objOldBalModel.SerialNo = CommonCls.ConvertIntZero(txtSerNo.Text);
            objOldBalModel.ReferenceNo = Convert.ToInt32(txtRefNo.Text);
            objOldBalModel.TenderNo = CommonCls.ConvertIntZero(txttenderNo.Text);
            objOldBalModel.TenderDate = CommonCls.ConvertToDate(txttenderDate.Text);

            if (ddlPartyName.SelectedValue == ddlPartyName.SelectedItem.Text)
            {
                objOldBalModel.PartyCD = 0;
                objOldBalModel.PartyName = ddlPartyName.SelectedItem.Text;
            }
            else
            {
                objOldBalModel.PartyCD = Convert.ToInt32(ddlPartyName.SelectedValue);
                objOldBalModel.PartyName = ddlPartyName.SelectedItem.Text;
            }

            //objOldBalModel.PartyCD = Convert.ToInt32(ddlPartyName.SelectedValue);
            //objOldBalModel.PartyName = ddlPartyName.SelectedItem.Text; 

            objOldBalModel.CostCentreCD = Convert.ToInt32(ddlcostCentre.SelectedValue);
            objOldBalModel.OpeningDate = CommonCls.ConvertToDate(txtopendate.Text);
            objOldBalModel.Amount = Convert.ToDecimal(txtAmount.Text);
            if (!string.IsNullOrEmpty(ViewState["kkk"].ToString()))
            {
                if (CommonCls.ConvertDecimalZero(ViewState["kkk"].ToString()) == 0)
                {
                    objOldBalModel.BSAmount = Convert.ToDecimal(txtAvlBal.Text);
                }
            }
            //if (IstEntry == 1)
            //{
            //    objOldBalModel.BSAmount = Convert.ToDecimal(txtAvlBal.Text); 
            //}


            string uri = string.Format("OldBalanceEntry/SaveOldBalance");
            DataTable dtSaveOldBal = CommonCls.ApiPostDataTable(uri, objOldBalModel);
            if (dtSaveOldBal.Rows.Count > 0)
            {
                if (dtSaveOldBal.Rows[0]["ResultID"].ToString() == "1")
                {

                    ShowMessage("Record Save Successfully  ", true);

                    int AvlBal = Convert.ToInt32(txtAvlBal.Text);
                    int CurrBal = Convert.ToInt32(txtAmount.Text);
                    int CurrentBal = AvlBal - CurrBal;
                    txtAvlBal.Text = CurrentBal.ToString();
                    if (ViewState["kkk"].ToString() == "0")
                    {
                        //BindAll();
                        Clear();
                    }
                    ClearBeforeSave();

                }
                else
                {
                    ShowMessage("Record Not Save Successfully  ", false);
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlAccountHead.SelectedValue != "0")
        {
            IstEntry = 0;
            DataTable dtGRD = VsdtAccountHead;

            DataView dv31 = new DataView(dtGRD);
            dv31.RowFilter = "AccCode= " + ddlAccountHead.SelectedValue + "";
            if (dv31.ToTable().Rows[0]["BalanceAmt"].ToString() != "0")
            {
                txtAvlBal.Enabled = false;

            }
            else
            {
                txtAvlBal.Enabled = true;

            }
            ViewState["kkk"] = txtAvlBal.Text = dv31.ToTable().Rows[0]["BalanceAmt"].ToString();
            clearSelectchange();

        }
        else
        {

            txtAvlBal.Enabled = false;
        }

        //DataTable dtGRD = VsdtAccountHead;
        ////DataRow[] dr = dtGRD.Select("BalanceAmt="); 

        //IstEntry = 0;
        //if (dtGRD.Rows.Count > 0)
        //{
        //    DataView dvAcc = new DataView(dtGRD);
        //    dvAcc.RowFilter = "AccCode=" + ddlAccountHead.SelectedValue;
        //    if (ddlAccountHead.SelectedValue == "0")
        //    {
        //        txtAvlBal.Text = "";
        //    }
        //    else
        //    {
        //        if (dvAcc[0]["BalanceAmt"].ToString() == "0")
        //        {
        //            txtAvlBal.Enabled = true;
        //            //ViewState["kkk"] = dvAcc[0]["BalanceAmt"].ToString();
        //            //IstEntry = 1;
        //        }
        //        else
        //        {
        //            txtAvlBal.Enabled = false;
        //        }
        //        txtAvlBal.Text = dvAcc[0]["BalanceAmt"].ToString();
        //        ViewState["kkk"] = dvAcc[0]["BalanceAmt"].ToString();
        //        clearSelectchange();
        //    }
        //}
    }

    void ClearBeforeSave() //Clear Before Saving
    {
        txtRefNo.Text = txttenderNo.Text = txttenderDate.Text = "";
        txtopendate.Text = txtAmount.Text = "";
        ddlPartyName.ClearSelection();
        ddlcostCentre.ClearSelection();
        hfAfterSaveInd.Value = "1";
        BindAll();
    }

    void Clear()//All Clear
    {
        ddlAccountHead.ClearSelection();
        txtbookNo.Text = txtPageNo.Text = txtSerNo.Text = "";
        txtRefNo.Text = txttenderNo.Text = txttenderDate.Text = "";
        txtopendate.Text = txtAmount.Text = "";
        ddlPartyName.ClearSelection();
        ddlcostCentre.ClearSelection();
        txtAvlBal.Text = "";
        hfAfterSaveInd.Value = "0";
        BindAll();
    }

    void clearSelectchange() //Clear To Second Entry
    {
        txtbookNo.Text = txtPageNo.Text = txtSerNo.Text = "";
        txtRefNo.Text = txttenderNo.Text = txttenderDate.Text = "";
        txtopendate.Text = txtAmount.Text = "";
        ddlPartyName.ClearSelection();
        ddlcostCentre.ClearSelection();
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}