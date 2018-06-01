using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GSTReturns_frmGSTR1Entry : System.Web.UI.Page
{
    Gstr1EntryModel objGstr1;
    DataTable dtGstr1grdview;
    int rowIndex
    {
        get { return CommonCls.ConvertIntZero(ViewState["rowIndex"]); }
        set { ViewState["rowIndex"] = value; }
    }
    DataTable VSDtGstr1Data
    {
        get { return (DataTable)ViewState["Gstr1dt"]; }
        set { ViewState["Gstr1dt"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.CssClass = "";
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            FillGstin();
        }

    }


    public void FillGstin()
    {
        try
        {

            objGstr1 = new Gstr1EntryModel();
            {
                objGstr1.Ind = 1;
                objGstr1.OrgID = GlobalSession.OrgID;//11;
                objGstr1.BrID = GlobalSession.BrID;
            }

            string uri = string.Format("Gstr1Entry/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, objGstr1);
            if (dtGSTIN.Rows.Count > 0)
            {
                if (dtGSTIN.Rows.Count > 1)
                {
                    ddlGstin.DataSource = dtGSTIN;
                    ddlGstin.DataValueField = "GSTIN";
                    ddlGstin.DataBind();
                    ddlGstin.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                    ddlGstin.Enabled = true;
                }
                else
                {
                    ddlGstin.DataSource = dtGSTIN;
                    ddlGstin.DataValueField = "GSTIN";
                    ddlGstin.DataBind();
                    ddlGstin.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    } 


    protected void btnShow_Click(object sender, EventArgs e) //Button Show
    {
        try
        {
            if (ddlMonth.SelectedValue == "0")
            {
                ddlMonth.Focus();
                ShowMessage("Select Month", false);
                return;
            }

            objGstr1 = new Gstr1EntryModel();
            objGstr1.Ind = 3;
            objGstr1.OrgID = GlobalSession.OrgID;//11;
            objGstr1.BrID = GlobalSession.BrID;
            objGstr1.YrCD = GlobalSession.YrCD;
            objGstr1.GSTIN = ddlGstin.SelectedItem.Text;
            objGstr1.MonthYear = ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text;

            string uri = string.Format("Gstr1Entry/Gstr1Search");
            DataTable dtGstrSearch = CommonCls.ApiPostDataTable(uri, objGstr1);
            if (dtGstrSearch.Rows.Count > 0)
            {
                txtTrName.Text = dtGstrSearch.Rows[0]["TradeName"].ToString();
                txtAgrTurnOver.Text = dtGstrSearch.Rows[0]["AgreegateTurnover"].ToString();
                txtAgetoLsQtr.Text = dtGstrSearch.Rows[0]["AgreegateTurnoverLstQtr"].ToString();

                GrdGstr1.DataSource = VSDtGstr1Data = dtGstrSearch;// VSDtGstr1Data;
                GrdGstr1.DataBind();

                EnabledControl();
            }
            EnabledControl();
        } 
        catch(Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    } 
    void EnabledControl() //Enabled Controll
    {
        txtAgetoLsQtr.Enabled = true;
        txtAgrTurnOver.Enabled = true;
        txtTrName.Enabled = true;
        btnSave.Enabled = true;
    }  

    void DisabledControl()
    {
        txtAgetoLsQtr.Enabled =  false;
        txtAgrTurnOver.Enabled = false;
        txtTrName.Enabled =      false;
        btnSave.Enabled =        false;
    }



    DataTable Gstr1dt() //gstr1 DtSchema
    {
        dtGstr1grdview = new DataTable();
        dtGstr1grdview.Columns.Add("GSTIN", typeof(string));
        dtGstr1grdview.Columns.Add("MonthID", typeof(int));
        dtGstr1grdview.Columns.Add("YearID", typeof(int));
        dtGstr1grdview.Columns.Add("MonthYear", typeof(string));
        dtGstr1grdview.Columns.Add("tradeName", typeof(string));
        dtGstr1grdview.Columns.Add("AgreegateTurnover", typeof(decimal));
        dtGstr1grdview.Columns.Add("AgreegateTurnoverLstQtr", typeof(decimal));
        dtGstr1grdview.Columns.Add("DeliveryChallantypeId", typeof(int));
        dtGstr1grdview.Columns.Add("DeliveryChallantype", typeof(string));
        dtGstr1grdview.Columns.Add("SrNoFrom", typeof(string));
        dtGstr1grdview.Columns.Add("SrNoTo", typeof(string));
        dtGstr1grdview.Columns.Add("Total", typeof(int));
        dtGstr1grdview.Columns.Add("Cancelled", typeof(int));
        dtGstr1grdview.Columns.Add("NetIssued", typeof(int));

        return dtGstr1grdview;
    }
    void Addgstr1Detail()
    {
        if (VSDtGstr1Data == null)
        {
            VSDtGstr1Data = Gstr1dt();
        }
        DataRow DrGstr1 = VSDtGstr1Data.NewRow();
        DrGstr1["GSTIN"] = ddlGstin.SelectedValue;
        DrGstr1["MonthID"] = ddlMonth.SelectedValue;
        DrGstr1["YearID"] = ddlYear.SelectedItem.Text;
        DrGstr1["MonthYear"] = ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text;
        DrGstr1["tradeName"] = txtTrName.Text;
        DrGstr1["AgreegateTurnover"] = CommonCls.ConvertDecimalZero(txtAgrTurnOver.Text);
        DrGstr1["AgreegateTurnoverLstQtr"] = CommonCls.ConvertDecimalZero(txtAgetoLsQtr.Text);
        DrGstr1["DeliveryChallantypeId"] = ddlDelieveryChallan.SelectedValue;
        DrGstr1["DeliveryChallantype"] = ddlDelieveryChallan.SelectedItem.Text;
        DrGstr1["SrNoFrom"] = txtSrNofrm.Text;
        DrGstr1["SrNoTo"] = txtSrnoTo.Text;
        DrGstr1["Total"] = txtTotalNo.Text;
        DrGstr1["Cancelled"] = txtCancelled.Text;
        DrGstr1["NetIssued"] = txtnetIssue.Text;

        VSDtGstr1Data.Rows.InsertAt(DrGstr1, rowIndex);
        GrdGstr1.DataSource = VSDtGstr1Data;
        GrdGstr1.DataBind();
        ClearAfterAdd();

        //objGstr1.Dtgstr1.Rows.Add(DrGstr1);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //string s = "Month='" + ddlMonth.SelectedValue + "' Year='" + ddlYear.SelectedItem.Text + "' DeliveryChallantypeId='" + ddlDelieveryChallan.SelectedValue + "'";
            //DataRow[] dr = VSDtGstr1Data.Select("Month='" + ddlMonth.SelectedValue + "' And Year='" + ddlYear.SelectedItem.Text + "' And DeliveryChallantypeId='" + ddlDelieveryChallan.SelectedValue + "'");
            //if (dr.Count() > 0)
            //{
            //    ShowMessage("Delivery Challan Already Taken.", false);
            //    ddlDelieveryChallan.Focus();
            //    return;
            //}

            if (ddlDelieveryChallan.SelectedValue == "0")
            {
                ShowMessage("Select Delievery Challan name", false);
                ddlDelieveryChallan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtSrNofrm.Text))
            {
                ShowMessage("Enter Serial No From", false);
                return;
            }
            if (string.IsNullOrEmpty(txtSrnoTo.Text))
            {
                ShowMessage("Enter Serial No To", false);
                return;
            }
            if (string.IsNullOrEmpty(txtTotalNo.Text))
            {
                ShowMessage("Enter Total No", false);
                return;
            }
            if (string.IsNullOrEmpty(txtCancelled.Text))
            {
                ShowMessage("Enter Cancelled No", false);
                return;
            }

            if (string.IsNullOrEmpty(txtnetIssue.Text))
            {
                ShowMessage("Enter NetIssued No", false);
                return;
            }

            Addgstr1Detail();
        }


        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    protected void GrdGstr1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveItem")
        {
            DataTable dtGstr1Detail = VSDtGstr1Data;
            dtGstr1Detail.Rows[rowIndex].Delete();
            VSDtGstr1Data = dtGstr1Detail;
            GrdGstr1.DataSource = dtGstr1Detail;
            GrdGstr1.DataBind();
        }
    }
    void ClearAfterAdd()
    {
        ddlDelieveryChallan.SelectedValue = "0";
        txtSrNofrm.Text = txtSrnoTo.Text = txtTotalNo.Text = txtCancelled.Text = txtnetIssue.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e) //Button Save----------------------
    {
        try
        {
            if (ddlMonth.SelectedValue == "0")
            {
                ddlMonth.Focus();
                ShowMessage("Select Month", false);
                return;
            }
            if (string.IsNullOrEmpty(txtTrName.Text))
            {
                txtTrName.Focus();
                ShowMessage("Enter Trade Name", false);
                return;
            }
            if (string.IsNullOrEmpty(txtAgrTurnOver.Text))
            {
                txtAgrTurnOver.Focus();
                ShowMessage("Enter Agreegate Turnover ", false);
                return;
            }

            if (string.IsNullOrEmpty(txtAgetoLsQtr.Text))
            {
                txtAgetoLsQtr.Focus();
                ShowMessage("Enter Agreegate Last Quarterly Turnover ", false);
                return;
            }

            objGstr1 = new Gstr1EntryModel();
            objGstr1.Ind = 2;
            objGstr1.OrgID = GlobalSession.OrgID;//11;
            objGstr1.BrID = GlobalSession.BrID;
            objGstr1.YrCD = GlobalSession.YrCD;
            objGstr1.User = GlobalSession.UserID;
            objGstr1.IP = GlobalSession.IP;
            objGstr1.Dtgstr1 = VSDtGstr1Data;

            if (objGstr1.Dtgstr1.Columns.Contains("GSTR1ROWID"))
           {
                objGstr1.Dtgstr1.Columns.Remove("GSTR1ROWID");
            }
            if (objGstr1.Dtgstr1.Columns.Contains("CompanyID"))
            {
                objGstr1.Dtgstr1.Columns.Remove("CompanyID");
            }
            if (objGstr1.Dtgstr1.Columns.Contains("BranchID"))
            {
                objGstr1.Dtgstr1.Columns.Remove("BranchID");
            }

            if (objGstr1.Dtgstr1.Columns.Contains("YrCD"))
            {
                objGstr1.Dtgstr1.Columns.Remove("YrCD");

            }
            if (objGstr1.Dtgstr1.Columns.Contains("UserID"))
            {
                objGstr1.Dtgstr1.Columns.Remove("UserID");
            }
            if (objGstr1.Dtgstr1.Columns.Contains("IPAddress"))
            {
                objGstr1.Dtgstr1.Columns.Remove("IPAddress");
            }
            if (objGstr1.Dtgstr1.Columns.Contains("EntryDate"))
            {
                objGstr1.Dtgstr1.Columns.Remove("EntryDate");
            }
            if (objGstr1.Dtgstr1.Columns.Contains("EntryTime"))
            {
                objGstr1.Dtgstr1.Columns.Remove("EntryTime");
            }


            string uri = string.Format("Gstr1Entry/Gstr1Saved");
            DataTable dtGstrSaved = CommonCls.ApiPostDataTable(uri, objGstr1);
            if (dtGstrSaved.Rows.Count > 0)
            {
                if (dtGstrSaved.Rows[0]["result"].ToString() == "1")
                {

                    ShowMessage("Data Is Sucessfully Saved", true);

                }

                else if (dtGstrSaved.Rows[0]["result"].ToString() == "2")
                {
                    ShowMessage("Data Is alrady Exist in Database", false);
                    return;
                }
            }
            clearAll();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    void clearAll()
    {
        ddlMonth.Focus();
        ddlDelieveryChallan.SelectedValue = "0";
        txtSrNofrm.Text = txtSrnoTo.Text = txtTotalNo.Text = txtCancelled.Text = txtnetIssue.Text = "";
        //ddlGstin.DataSource = new DataTable();
        //ddlGstin.DataBind();  
        VSDtGstr1Data = null;
        GrdGstr1.DataSource = null;

        GrdGstr1.DataSource = new DataTable();
        GrdGstr1.DataBind();

        ddlMonth.ClearSelection();
        ddlYear.ClearSelection();
        txtTrName.Text = txtAgetoLsQtr.Text = txtAgrTurnOver.Text = "";
        DisabledControl();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearAll();
    }
    void ShowMessage(string Message, bool type)
    {
        lblmsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblmsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}