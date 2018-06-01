using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class AdminMasters_frmChecqueSeries : System.Web.UI.Page
{
    ChequeSeriesModel objChkseries;
    int rowIndex
    {
        get { return CommonCls.ConvertIntZero(ViewState["rowIndex"]); }
        set { ViewState["rowIndex"] = value; }
    }

    DataTable VSItemsData
    {
        get { return (DataTable)ViewState["grdData"]; }
        set { ViewState["grdData"] = value; }
    }

    BankReceiptModel plbankrec;
    DataTable dtgrdview;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lblMsg.Text = "";
            lblMsg.CssClass = "";
            ViewState["VchType"] = 3;
            BindAllBankReceiptDDL();

        }
    }

    void BindAllBankReceiptDDL()
    {
        try
        {
            plbankrec = new BankReceiptModel();
            plbankrec.Ind = 11;
            plbankrec.OrgID = GlobalSession.OrgID;
            plbankrec.BrID = GlobalSession.BrID;
            plbankrec.YrCD = GlobalSession.YrCD;
            plbankrec.VchType = Convert.ToInt32(ViewState["VchType"]);

            string uri = string.Format("BankReceipt/BindAllBankReceiptDDL");
            DataSet dsBindAllCRDDL = CommonCls.ApiPostDataSet(uri, plbankrec);
            if (dsBindAllCRDDL.Tables.Count > 0)
            {
                DataTable dtAccountHead = dsBindAllCRDDL.Tables[0];
                DataTable dtLastVoucher = dsBindAllCRDDL.Tables[1];
                DataTable dtNarration = dsBindAllCRDDL.Tables[2];
                DataTable dtCashAccount = dsBindAllCRDDL.Tables[3];



                // Cash List Bind
                if (dtCashAccount.Rows.Count > 0)
                {
                    ddlbank.DataSource = dtCashAccount;
                    ddlbank.DataTextField = "AccName";
                    ddlbank.DataValueField = "AccCode";
                    ddlbank.DataBind();
                    if (dtCashAccount.Rows.Count > 1)
                        ddlbank.Items.Insert(0, new ListItem("-- Select --", "0"));

                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }



    DataTable CreatGridDt() // Create Grid Structure
    {
        dtgrdview = new DataTable();

        dtgrdview.Columns.Add("CompanyID", typeof(int));
        dtgrdview.Columns.Add("BranchID", typeof(int));
        dtgrdview.Columns.Add("BankCode", typeof(int));
        dtgrdview.Columns.Add("chequeFrom", typeof(int));
        dtgrdview.Columns.Add("chequeto", typeof(int));
        dtgrdview.Columns.Add("Diffrence", typeof(int));


        return dtgrdview;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlbank.SelectedValue == "0")
            {
                ShowMessage("Please Select Bank Name.", false);
                ddlbank.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtChequefrom.Text))
            {
                ShowMessage("Please Enter Cheque From.", false);
                txtChequefrom.Focus();
                return;
            }
            else if (CommonCls.ConvertIntZero(txtChequefrom.Text) == 0)    //|| CommonCls.ConvertIntZero(txtChequeto.Text) == 0
            {
                ShowMessage("Cheque No. From Can't Be Zero. ", false);
                return;
            }
            else if (string.IsNullOrEmpty(txtChequeto.Text))
            {
                ShowMessage("Please Enter Cheque To.", false);
                txtChequeto.Focus();
                return;
            }
            else if (CommonCls.ConvertIntZero(txtChequeto.Text) == 0)
            {
                ShowMessage("Cheque No. To Can't Be Zero.", false);
                return;
            }
            else if (CommonCls.ConvertIntZero(txtChequefrom.Text) > CommonCls.ConvertIntZero(txtChequeto.Text))
            {
                ShowMessage("Cheque From Is Less Then to Cheque To.", false);
                return;
            }

            if (ViewState["grdData"] == null)
            {
                CreatGridDt();
            }
            else
            {
                dtgrdview = (DataTable)ViewState["grdData"];
            }

            DataRow[] row = dtgrdview.Select("(chequeFrom<='" + txtChequefrom.Text + "' And chequeto>='" + txtChequefrom.Text + "') Or (chequeFrom<='" + txtChequeto.Text + "' And chequeto>='" + txtChequeto.Text + "') Or (chequeFrom>='" + txtChequefrom.Text + "' And chequeto<='" + txtChequeto.Text + "')");
            if (row.Count() > 0)
            {
                ShowMessage("This Check Series Allready Exist", false);
                return;
            }

            DataRow dr = dtgrdview.NewRow();
            dr["CompanyID"] = GlobalSession.OrgID;
            dr["BranchID"] = GlobalSession.BrID;
            dr["BankCode"] = ddlbank.SelectedValue; ;

            dr["chequeFrom"] = txtChequefrom.Text;
            dr["chequeto"] = txtChequeto.Text;
            int Dif = Convert.ToInt32(dr["chequeto"].ToString()) - Convert.ToInt32(dr["chequeFrom"].ToString()) + 1;
            dr["Diffrence"] = Dif;
            dtgrdview.Rows.Add(dr);
            ViewState["grdData"] = dtgrdview;
            grdchequeseries.DataSource = dtgrdview;
            grdchequeseries.DataBind();
            clearAdd();

            //ClearAll();
            ddlbank.Enabled = false;
            txtChequefrom.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ClearAll()
    {
        txtChequefrom.Text = "";
        txtChequeto.Text = "";
        grdchequeseries.DataSource = "";
        grdchequeseries.DataSource = new DataTable();
        grdchequeseries.DataBind();
        ddlbank.ClearSelection();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlbank.SelectedValue == "0")
            {
                ShowMessage("Please Select Bank Name.", false);
                ddlbank.Focus();
                return;
            }

            if (ViewState["grdData"] == null)
            {
                ShowMessage("Please Click Add Button For Add Checque Series.", false);
                txtChequefrom.Focus();

                return;
            }

            objChkseries = new ChequeSeriesModel();
            objChkseries.Ind = 1;
            objChkseries.OrgID = GlobalSession.OrgID;
            objChkseries.BrID = GlobalSession.BrID;
            objChkseries.BankCode = Convert.ToInt32(ddlbank.SelectedValue);
            if (VSItemsData.Columns.Contains("ID"))
            {
                VSItemsData.Columns.Remove("ID");
            }
            objChkseries.DtChkSeries = VSItemsData;

            string uri = string.Format("ChequeSeries/SaveChkSerices");
            DataTable dtSaveChkList = CommonCls.ApiPostDataTable(uri, objChkseries);
            if (dtSaveChkList.Rows.Count > 0)
            {
                ShowMessage("Data Save Sucessfully.", true);
                ddlbank.Enabled = true;
                //  pnlSectionGrid.Visible = false;
                ClearAll();
            }
            else
            {
                ShowMessage("Data Not Save Sucessfully.", false);
                return;
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    public void clearAdd()
    {
        lblMsg.Text = "";
        lblMsg.CssClass = "";
        txtChequefrom.Text = "";
        txtChequeto.Text = "";
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.CssClass = "";
        ddlbank.ClearSelection();
        txtChequefrom.Text = "";
        txtChequeto.Text = "";
        ddlbank.Enabled = true;
        grdchequeseries.DataSource = "";
        ViewState["grdData"] = null;
        grdchequeseries.DataSource = new DataTable();
        grdchequeseries.DataBind();
        // pnlSectionGrid.Visible = false;

    }

    public void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //object sender = UpdatePanel1;
        //Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }

    protected void grdchequeseries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);


            if (e.CommandName == "EditItemRow")
            {
                DataRow drItems = VSItemsData.Rows[rowIndex];
                txtChequefrom.Text = drItems["chequeFrom"].ToString();
                txtChequeto.Text = drItems["chequeto"].ToString();



                // grdchequeseries.Rows[rowIndex].BackColor = Color.Bisque;
                //VSItemsData.Rows[rowIndex].Delete();
                //grdchequeseries.DataSource = VSItemsData;
                //grdchequeseries.DataBind(); 

            }


            if (e.CommandName == "RemoveRow")
            {
                dtgrdview = (DataTable)ViewState["grdData"];
                dtgrdview.Rows[rowIndex].Delete();

                ViewState["grdData"] = dtgrdview;

                grdchequeseries.DataSource = dtgrdview;
                grdchequeseries.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbank.SelectedValue == "0")
        {

            lblMsg.Text = "";
            lblMsg.CssClass = "";
        }
        else
        {
            lblMsg.Text = "";
            lblMsg.CssClass = "";
            objChkseries = new ChequeSeriesModel();
            objChkseries.Ind = 2;
            objChkseries.OrgID = GlobalSession.OrgID;
            objChkseries.BrID = GlobalSession.BrID;
            objChkseries.BankCode = Convert.ToInt32(ddlbank.SelectedValue);
            // objChkseries.DtChkSeries = VSItemsData;

            string uri = string.Format("ChequeSeries/ddlBankSeries");
            DataTable dtSearchChkList = CommonCls.ApiPostDataTable(uri, objChkseries);
            if (dtSearchChkList.Rows.Count > 0)
            {
                // pnlSectionGrid.Visible = true;
                grdchequeseries.DataSource = ViewState["grdData"] = dtSearchChkList;
                grdchequeseries.DataBind();
            }
            else
            {
                grdchequeseries.DataSource =  new DataTable();
                grdchequeseries.DataBind();
                ViewState["grdData"] = null;
                //ShowMessage("Data Not Available Given Check Series Please Add Cheque Series.", false);
                return;
            }
        }


    }
}