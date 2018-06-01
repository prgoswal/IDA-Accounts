using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmYearActivation : System.Web.UI.Page
{
    DataTable VsItemTransfer
    {
        get { return (DataTable)ViewState["dtItemTransfer"]; }
        set { ViewState["dtItemTransfer"] = value; }
    }

    OpeningBlcTransferModel objOpeningBlcTransfer;
    NewYearActivationModel objNewYearActivation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LoadFinancialYear();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, false);
            }
        }
    }

    private void LoadFinancialYear()
    {
        try
        {
            objOpeningBlcTransfer = new OpeningBlcTransferModel();
            objOpeningBlcTransfer.Ind = 11;
            objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
            string uri = string.Format("NewYearActivation/LoadFinancialYear");
            DataTable dtFinancialYear = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);
            if (dtFinancialYear.Rows.Count > 0)
            {
                ddlFinancialYr.SelectedValue = dtFinancialYear.Rows[0]["YearFromTo"].ToString();
                ddlFinancialYr.Enabled = false;
                btnSave.Enabled = false;
                // divBlcTransfer.Visible = true;

                if (GlobalSession.YrCD == CommonCls.ConvertIntZero(dtFinancialYear.Rows[0]["YrCode"].ToString()))
                {
                    divBlcTransfer.Visible = true;
                }
                else
                {
                    divBlcTransfer.Visible = false;
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
            objOpeningBlcTransfer = new OpeningBlcTransferModel();
            objOpeningBlcTransfer.Ind = 1;
            objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
            objOpeningBlcTransfer.BrID = GlobalSession.BrID;
            objOpeningBlcTransfer.YrCode = 18;
            objOpeningBlcTransfer.YearFromTo = ddlFinancialYr.SelectedItem.Text;
            objOpeningBlcTransfer.ActiveID = 1;
            objOpeningBlcTransfer.UserID = GlobalSession.UserID;
            objOpeningBlcTransfer.IPAddress = GlobalSession.IP;
            objOpeningBlcTransfer.YrStartDate = CommonCls.ConvertToDate("01/04/2018");
            objOpeningBlcTransfer.YrEndDate = CommonCls.ConvertToDate("31/03/2019");

            string uri = string.Format("NewYearActivation/SaveFinancialYear");
            DataTable dtYear = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);
            if (dtYear.Rows.Count > 0)
            {
                if (dtYear.Rows[0][0].ToString() == "1")
                {

                    pnlYearActivation.Visible = true;
                    ShowMessage("Financial Year is Activated For Year " + ddlFinancialYr.SelectedValue + ".For Transfer Your Opening Balance Please Login with New Financial Year.", true);
                    LoadFinancialYear();
                }
            }
            else
            {
                ShowMessage("Data is not saved successfully", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            string uri;
            objOpeningBlcTransfer = new OpeningBlcTransferModel();
            objOpeningBlcTransfer.Ind = 1;
            objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
            objOpeningBlcTransfer.BrID = GlobalSession.BrID;
            objOpeningBlcTransfer.YrCode = GlobalSession.YrCD - 1;
            objOpeningBlcTransfer.VoucharDateFrom = "20" + objOpeningBlcTransfer.YrCode + "/04/01";
            objOpeningBlcTransfer.VoucharDateto = "20" + GlobalSession.YrCD + "/03/31";

            //objNewYearActivation.YrCode = GlobalSession.YrCD;
            //objNewYearActivation.VoucharDateFrom = Convert.ToDateTime(GlobalSession.YrStartDate.ToString()).ToString("yyyy/MM/dd");
            //objNewYearActivation.VoucharDateto = Convert.ToDateTime(GlobalSession.YrEndDate.ToString()).ToString("yyyy/MM/dd");

            objOpeningBlcTransfer.Reportorder = 1;

            uri = string.Format("NewYearActivation/LoadClosingBalance");
            DataTable dtClosingBlc = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);

            if (dtClosingBlc.Rows.Count > 0)
            {

                if (dtClosingBlc.Columns.Contains("accGrpcode"))
                {
                    dtClosingBlc.Columns.Remove("accGrpcode");
                }

                objOpeningBlcTransfer = new OpeningBlcTransferModel();
                objOpeningBlcTransfer.Ind = 12;
                objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
                objOpeningBlcTransfer.BrID = GlobalSession.BrID;

                objOpeningBlcTransfer.User = GlobalSession.UserID;
                objOpeningBlcTransfer.IP = GlobalSession.IP;
                objOpeningBlcTransfer.VChType = 0;
                objOpeningBlcTransfer.ClosingStock = CommonCls.ConvertDecimalZero(txtClosingStockValue.Text);

                objOpeningBlcTransfer.YrCode = 18;
                objOpeningBlcTransfer.YrStartDate = Convert.ToDateTime("01/04/2018".ToString()).ToString("yyyy/MM/dd");
                objOpeningBlcTransfer.VoucharDate = Convert.ToDateTime("01/04/2018".ToString()).ToString("yyyy/MM/dd");
                objOpeningBlcTransfer.Narration = "OP. BALANCE";

                objOpeningBlcTransfer.dtOpeningBlc = JsonConvert.SerializeObject(dtClosingBlc);

                uri = string.Format("NewYearActivation/SaveOpeningBalance");
                DataTable dtOpeningBlc = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);
                if (dtOpeningBlc.Rows[0][0].ToString() == "1")
                {

                    bool ItemTransfer = ItemTrasferForOpeningBlc();
                    if (ItemTransfer == true)
                    {
                        //lblOpeningBlc.Text = "Opening Balance is Transfer Successfully..";
                        divOpeningBlc.Visible = true;
                        chkOpeningBlc.Checked = true;
                        chkItemOpeningBlc.Checked = true;
                        ShowMessage("Opening Balance is Transfer Successfully Please check Trial Balance.", true);


                    }
                    else
                    {
                        ShowMessage("Opening Balance is not Transfer Try Again.", false);
                    }
                }
                else
                {
                    ShowMessage("Opening Balance is not Transfer Try Again.", false);
                }
            }
            else
            {
                ShowMessage("Server Error.Contact To Administrator", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private bool ItemTrasferForOpeningBlc()
    {
        try
        {
            string uri = string.Empty;
            objOpeningBlcTransfer = new OpeningBlcTransferModel();
            objOpeningBlcTransfer.Ind = 1;
            objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
            objOpeningBlcTransfer.BrID = GlobalSession.BrID;
            objOpeningBlcTransfer.YrCode = (GlobalSession.YrCD - 1);

            uri = string.Format("NewYearActivation/TrasferItemOpeningBlc");
            DataTable dtItemTransfer = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);
            if (dtItemTransfer.Rows.Count > 0)
            {
                //dtItemTransfer = VsItemTransfer;
                DataView dvItemTransfer = new DataView(dtItemTransfer);
                dvItemTransfer.RowFilter = "GroupID  = " + 2 + "";
                VsItemTransfer = dvItemTransfer.ToTable();

                bool ItemDetail = GetItemOpeningBlc();
                if (ItemDetail == true)
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }

        }
        catch (Exception)
        {
            return false;

        }
    }

    private bool GetItemOpeningBlc()
    {
        try
        {
            objOpeningBlcTransfer = new OpeningBlcTransferModel();
            objOpeningBlcTransfer.Ind = 13;
            objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
            objOpeningBlcTransfer.BrID = GlobalSession.BrID;
            objOpeningBlcTransfer.YrCode = GlobalSession.YrCD;
            //objOpeningBlcTransfer.dtItemDetail = VsItemTransfer;
            objOpeningBlcTransfer.IP = GlobalSession.IP;
            objOpeningBlcTransfer.User = GlobalSession.UserID;
            objOpeningBlcTransfer.EntryDate = Convert.ToDateTime(GlobalSession.YrStartDate).ToString("yyyy/MM/dd");
            objOpeningBlcTransfer.dtItemDetail = JsonConvert.SerializeObject(VsItemTransfer);

            string uri = string.Format("NewYearActivation/ItemDetails");
            DataTable dtItemDetails = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);
            if (dtItemDetails.Rows[0][0].ToString() == "1")
            {
                // lblItemOpeningBlc.Text = "Item Opening Balance is Transfer Successfully..";
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmLogin.aspx");
    }
}