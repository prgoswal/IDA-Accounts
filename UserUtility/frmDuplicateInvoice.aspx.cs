using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmDuplicateInvoice : System.Web.UI.Page
{
    DuplicateInvoiceModel ObjDupInvoicePl;
    DataTable DuplicateGrdDt, DtItemAdd;

    public DataTable VsDtShowData
    {
        get { return (DataTable)ViewState["dtVsDtShowData"]; }
        set { ViewState["dtVsDtShowData"] = value; }
    }

    public DataTable VsDtGetDate
    {
        get { return (DataTable)ViewState["dtLoadgetDate"]; }
        set { ViewState["dtLoadgetDate"] = value; }
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ViewState["VoucherType"] = 6;
            LoadMonthYear();
        }
    }

    void LoadMonthYear()
    {
        try
        {
            ObjDupInvoicePl = new DuplicateInvoiceModel();
            ObjDupInvoicePl.Ind = 1;
            string uri = string.Format("DuplicateInvoice/LoadGetDate");
            DataTable dtLoadgetDate = CommonCls.ApiPostDataTable(uri, ObjDupInvoicePl);
            if (dtLoadgetDate.Rows.Count > 0)
            {
                VsDtGetDate = dtLoadgetDate;
                //dtLoadgetDate = (DataTable)ViewState["VsDtGetDate"];
                txtMonthyear.Text = VsDtGetDate.Rows[0]["MonthYear"].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowMessage("ex.Message", false);
        }
    } 

    protected void btnShow_Click(object sender, EventArgs e)//Button Show Click
    {
        try
        {
            ObjDupInvoicePl = new DuplicateInvoiceModel();
            ObjDupInvoicePl.Ind = 2;
            ObjDupInvoicePl.OrgID = GlobalSession.OrgID;
            ObjDupInvoicePl.VoucherType = Convert.ToInt32(ViewState["VoucherType"]);
            ObjDupInvoicePl.IstDate = CommonCls.ConvertToDate(CommonCls.ConvertDateDB(VsDtGetDate.Rows[0]["IstDate"]));
            ObjDupInvoicePl.LastDate = CommonCls.ConvertToDate(CommonCls.ConvertDateDB(VsDtGetDate.Rows[0]["LastDate"]));

            string uri = string.Format("DuplicateInvoice/ShowData");
            DataTable dtShowData = CommonCls.ApiPostDataTable(uri, ObjDupInvoicePl);
            if (dtShowData.Rows.Count > 0)
            {
                dtShowData.Columns.Add("Checked", typeof(bool));
                foreach (DataRow item in dtShowData.Rows)
                {
                    item["Checked"] = false;                    
                }
                grdDupInvoice.DataSource = VsDtShowData = dtShowData;
                grdDupInvoice.DataBind();
            }
            else
            {
                VsDtShowData = null;
                grdDupInvoice.DataSource = new DataTable();
                grdDupInvoice.DataBind();
                ShowMessage("No Data Found", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }  

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)//Hecked Changed event
    {
        try
        {
            foreach (GridViewRow rw in grdDupInvoice.Rows)
            {
                CheckBox chkBx = (CheckBox)rw.FindControl("chkSelect");
                if (chkBx != null && chkBx.Checked)
                {
                    Label lblInvoiceSeries = (Label)rw.FindControl("lblInvoiceSeries");
                    Label lblInvoiceNo = (Label)rw.FindControl("lblInvoiceNo");
                    Response.Redirect("../Modifiaction/frmUpdSales.aspx?InSer=" + CommonCls.EncodePassword(lblInvoiceSeries.Text) + "&&InNo=" + CommonCls.EncodePassword(lblInvoiceNo.Text));
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

        //try
        //{
        //    int pos = 0;
        //    foreach (GridViewRow rw in grdDupInvoice.Rows)
        //    {
        //        CheckBox chkBx = (CheckBox)rw.FindControl("chkSelect");
        //        if (chkBx != null && chkBx.Checked)
        //        {
        //            DataRow drNew = VsDtShowData.NewRow();
        //            DataRow dr = VsDtShowData.Rows[pos];
        //            dr["Checked"] = true;
        //            drNew.ItemArray = dr.ItemArray;
        //            drNew["Checked"] = false;
        //            VsDtShowData.Rows.InsertAt(drNew, pos + 1);
        //        }
        //        pos++;
        //    }
        //    grdDupInvoice.DataSource = VsDtShowData;
        //    grdDupInvoice.DataBind();
        //} 
        //catch(Exception ex)
        //{
        //    ShowMessage(ex.Message, false);
        //}
    } 

    DataTable createGrdDT()
    {
        DuplicateGrdDt = new DataTable();
        DuplicateGrdDt.Columns.Add("CompanyID", typeof(int));
        DuplicateGrdDt.Columns.Add("InvoiceNo", typeof(string));
        DuplicateGrdDt.Columns.Add("InvoiceDate", typeof(string));
        DuplicateGrdDt.Columns.Add("Accountcode", typeof(int));
        DuplicateGrdDt.Columns.Add("AccName", typeof(int));
        DuplicateGrdDt.Columns.Add("NetAmount", typeof(int));
        DuplicateGrdDt.Columns.Add("Narration", typeof(string));

        return DuplicateGrdDt;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //txtMonthyear.Text = "";
        VsDtShowData = null;
        grdDupInvoice.DataSource = new DataTable();
        grdDupInvoice.DataBind();
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}