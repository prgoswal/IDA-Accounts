using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GSTReturns_frmGSTR3B : System.Web.UI.Page
{
    Gstr3BModel objGstr3BModel;

    #region Declaration

    public DataTable VSDV31
    {
        get { return (DataTable)ViewState["VSdv31"]; }
        set { ViewState["VSdv31"] = value; }
    }
    public DataTable VSDV32
    {
        get { return (DataTable)ViewState["VSdv32"]; }
        set { ViewState["VSdv32"] = value; }
    }
    public DataTable VSDV4
    {
        get { return (DataTable)ViewState["VSdv4"]; }
        set { ViewState["VSdv4"] = value; }
    }
    public DataTable VSDV5
    {
        get { return (DataTable)ViewState["VSdv5"]; }
        set { ViewState["VSdv5"] = value; }
    }
    public DataTable VSSuplliesDetail
    {
        get { return (DataTable)ViewState["VSSuplliesDetail"]; }
        set { ViewState["VSSuplliesDetail"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        EnableControl();
        if (!IsPostBack)
        {
            FillGstin();
        }

        lblMsg.CssClass = "";
        lblMsg.Text = "";
        lblCompanyName.Text = GlobalSession.OrgName;
    }

    public void FillGstin()
    {
        try
        {

            objGstr3BModel = new Gstr3BModel()
           {
               Ind = 2,
               OrgID = GlobalSession.OrgID,//11,
               BrID = GlobalSession.BrID,
               YrCD = GlobalSession.YrCD,

           };

            string uri = string.Format("Gstr3B/FillGistnNo");
            DataTable dtGSTIN = CommonCls.ApiPostDataTable(uri, objGstr3BModel);
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

        }
    }

    

    public void GetGSTR3BData()
    {
        try
        {
            if (ddlMonth.SelectedValue == "0")
            {
                ddlMonth.Focus();
                ShowMessage("Select Month..!", false);
                return;
            }
            if (ddlYear.SelectedValue == "0")
            {
                ddlYear.Focus();
                ShowMessage("Select Year..!", false);
                return;
            }
            objGstr3BModel = new Gstr3BModel();
            objGstr3BModel.Ind = 1;
            objGstr3BModel.OrgID = GlobalSession.OrgID; //11;
            objGstr3BModel.BrID = GlobalSession.BrID; //16;
            objGstr3BModel.YrCD = GlobalSession.YrCD; //17;
            objGstr3BModel.GSTIN = ddlGstin.SelectedValue; //;
            objGstr3BModel.TaxYear = Convert.ToInt32(ddlYear.SelectedItem.Text); //2017;
            objGstr3BModel.TaxMonth = Convert.ToInt32(ddlMonth.SelectedValue); //17;

            string uri = string.Format("Gstr3B/GetGSTR3BData");
            DataSet dsGetGSTR3BData = CommonCls.ApiPostDataSet(uri, objGstr3BModel);
            if (dsGetGSTR3BData.Tables.Count > 0)
            {

                DataTable dtGRD = dsGetGSTR3BData.Tables[1];
                if (dtGRD.Rows.Count > 0)
                {
                    lblDate.Text = CommonCls.ConvertDateDB(dtGRD.Rows[0]["ImportDate"].ToString());
                    DataView dv31 = new DataView(dtGRD);
                    dv31.RowFilter = "TableItemNM = 3.1";
                    Grd31.DataSource = VSDV31 = dv31.ToTable();
                    Grd31.DataBind();

                    DataView dv32 = new DataView(dtGRD);
                    dv32.RowFilter = "TableItemNM = 3.2";
                    Grd32.DataSource = VSDV32 = dv32.ToTable();
                    Grd32.DataBind();

                    DataView dv4 = new DataView(dtGRD);
                    dv4.RowFilter = "TableItemNM = 4.0";
                    Grd4.DataSource = VSDV4 = dv4.ToTable();
                    Grd4.DataBind();

                    DataView dv5 = new DataView(dtGRD);
                    dv5.RowFilter = "TableItemNM = 5.0";
                    Grd5.DataSource = VSDV5 = dv5.ToTable();
                    Grd5.DataBind();



                    DataTable dtGRDSumm = dsGetGSTR3BData.Tables[2];
                    if (dtGRDSumm.Rows.Count > 0)
                    {
                        DataView dvSummary = new DataView(dtGRDSumm);
                        GrdSummary.DataSource = dvSummary;
                        GrdSummary.DataBind();
                    }

                    VSSuplliesDetail = dsGetGSTR3BData.Tables[4];

                    //-------------------------------6.1 For payment-------------------------------------- 
                    //DataTable dtGRDS6 = dsGetGSTR3BData.Tables[3];
                    //if (dtGRDS6.Rows.Count > 0)
                    //{
                    //    DataView dv6Pay = new DataView(dtGRDS6);

                    //    Grd6.DataSource = dv6Pay;
                    //    Grd6.DataBind();
                    //}
                }
                disableControl();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        GetGSTR3BData();
        pnlGrids.Visible = true;
    }

    public void EnableControl()
    {
        ddlMonth.Enabled = true;
        ddlYear.Enabled = true;
        ddlGstin.Enabled = true;
        btnGo.Enabled = true;
    }
    public void disableControl()
    {
        ddlMonth.Enabled = false;
        ddlYear.Enabled = false;
        ddlGstin.Enabled = false;
        btnGo.Enabled = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMonth.ClearSelection();
        ddlYear.ClearSelection();

        //ddlGstin.DataSource = new DataTable();
        //ddlGstin.DataBind(); 

        Grd31.DataSource = new DataTable();
        Grd31.DataBind();

        Grd32.DataSource = new DataTable();
        Grd32.DataBind();

        Grd4.DataSource = new DataTable();
        Grd4.DataBind();

        Grd5.DataSource = new DataTable();
        Grd5.DataBind();

        EnableControl();
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void btnCloseViesDetail_Click(object sender, EventArgs e)
    {
        PnlViesDetail.Visible = false;
    }

    string ConverToCurrency(string Amount)
    {
        decimal parsed = decimal.Parse(CommonCls.ConvertDecimalZero(Amount).ToString(), CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("hi-IN");
        string text = string.Format(hindi, "{0:c}", parsed);
        return text;
    }

    protected void SupplieDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ////// Clear First Contents //////
        lblTaxable.Text = lblIGST.Text = lblCGST.Text = lblSGST.Text = lblCess.Text = "";


        string TableHeadDescCD, TableHeadDesc = "";
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RowClickGrd31")
        {
            TableHeadDescCD = this.Grd31.DataKeys[rowIndex].Values["TableHeadDescCD"].ToString();
            TableHeadDesc = this.Grd31.DataKeys[rowIndex].Values["TableHeadDesc"].ToString();

            DataRow GrdRow = VSDV31.Rows[rowIndex];
            lblTaxable.Text = ConverToCurrency(GrdRow["TotalTaxValue"].ToString());
            lblIGST.Text = ConverToCurrency(GrdRow["IGSTAmt"].ToString());
            lblCGST.Text = ConverToCurrency(GrdRow["CGSTAmt"].ToString());
            lblSGST.Text = ConverToCurrency(GrdRow["SGSTAmt"].ToString());
            lblCess.Text = ConverToCurrency(GrdRow["Cesstax"].ToString());

            ShowSuppliersDetail(TableHeadDescCD);

        }
        else if (e.CommandName == "RowClickGrd32")
        {
            TableHeadDescCD = this.Grd32.DataKeys[rowIndex].Values["TableHeadDescCD"].ToString();
            TableHeadDesc = this.Grd32.DataKeys[rowIndex].Values["TableHeadDesc"].ToString();

            DataRow GrdRow = VSDV32.Rows[rowIndex];
            lblTaxable.Text = ConverToCurrency(GrdRow["TotalTaxValue"].ToString());
            lblIGST.Text = ConverToCurrency(GrdRow["IGSTAmt"].ToString());
            lblCGST.Text = ConverToCurrency(GrdRow["CGSTAmt"].ToString());
            lblSGST.Text = ConverToCurrency(GrdRow["SGSTAmt"].ToString());
            lblCess.Text = ConverToCurrency(GrdRow["Cesstax"].ToString());

            ShowSuppliersDetail(TableHeadDescCD);
        }

        lblViewDetailHeading.Text = TableHeadDesc;//GrdRow["TableHeadDesc"].ToString();
        PnlViesDetail.Visible = true;
    }

    void ShowSuppliersDetail(string TableHeadDescCD)
    {

        lblddlMonth.Text = ddlMonth.SelectedItem.Text;
        lblddlYear.Text = ddlYear.SelectedItem.Text;
        lblddlGstin.Text = ddlGstin.SelectedItem.Text;
        lbllblCompanyName.Text = lblCompanyName.Text;
        lbllblDate.Text = lblDate.Text;

        if (VSSuplliesDetail.Rows.Count > 0)
        {
            DataView DvSupDetail = new DataView(VSSuplliesDetail);
            DvSupDetail.RowFilter = "RecordNo=" + TableHeadDescCD;
            if (DvSupDetail.Count > 0)
            {
                Grd31OnPopup.DataSource = DvSupDetail.ToTable();
                Grd31OnPopup.DataBind();

                PnlSuplliesNoRecord.Visible = false;
                pnlSuplliesRecord.Visible = true;
            }
            else
            {
                Grd31OnPopup.DataSource = null;
                Grd31OnPopup.DataBind();

                PnlSuplliesNoRecord.Visible = true;
                pnlSuplliesRecord.Visible = false;
            }
        }
        else
        {
            Grd31OnPopup.DataSource = null;
            Grd31OnPopup.DataBind();

            PnlSuplliesNoRecord.Visible = true;
            pnlSuplliesRecord.Visible = false;
        }
    }
}