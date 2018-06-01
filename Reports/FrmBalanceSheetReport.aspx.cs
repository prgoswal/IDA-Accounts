using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmBalanceSheetReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
            BalanceSheetOnLoad();
        }
    }

    void BalanceSheetOnLoad()
    {
        MasterModel ObjMM = new MasterModel();
        ObjMM.Ind = 41;
        ObjMM.OrgID = GlobalSession.OrgID;
        ObjMM.BrID = GlobalSession.BrID;
        ObjMM.YrCD = GlobalSession.YrCD;
        ObjMM.StartDate = CommonCls.ConvertToDate(txtToDate.Text);

        string uri = string.Format("Report/BalanceSheetOnLoad");
        DataTable dtBalanceSheet = CommonCls.ApiPostDataTable(uri, ObjMM);
        if (dtBalanceSheet.Rows.Count > 0)
        {
            txtClosingStockValue.Text = CommonCls.ConverToCommas(dtBalanceSheet.Rows[0]["ClosingStockValue"]);
            btnShow.Enabled = true;
        }
        else
        {
            CommonCls.ShowModal("Internal Server Error! Reload The Page.", false, "../Reports/FrmBalanceSheetReport.aspx", "Ok");
            btnShow.Enabled = false;
        }
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        BalanceSheetOnLoad();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (CommonCls.ConvertToDate(txtToDate.Text) == string.Empty)
            {
                txtToDate.Focus();
                lblErrorMsg.Text = "Enter Date In Valid Format dd/MM/yyyy.";
                return;
            }

            DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
            DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

            if (txtFromDate.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Please Enter From Date ";
                txtFromDate.Focus();
                return;
            }
            else if (txtToDate.Text.Trim() == "")
            {
                lblErrorMsg.Text = "Please Enter To Date ";
                txtToDate.Focus();
                return;
            }

            else if (F_Date > T_Date)
            {
                lblErrorMsg.Text = "From Date Should Not Be Greater Than To Date";
                txtFromDate.Focus();
                return;
            }
            else
            {
                Session["Report"] = "RptBalanceSheet";//Report Name
                Hashtable HT = new Hashtable();

                HT.Add("Ind", 1);
                HT.Add("OrgID", GlobalSession.OrgID);
                HT.Add("BrID", GlobalSession.BrID);
                HT.Add("yrcode", GlobalSession.YrCD);
                HT.Add("CompanyName", GlobalSession.OrgName);
                HT.Add("BranchName", GlobalSession.BrName);
                HT.Add("ReportHeading", "BALANCE SHEET");
                HT.Add("Reportorder", 1);
                HT.Add("VoucharDateFrom", "2017/04/01");
                HT.Add("VoucharDateto", txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

                HT.Add("ClosingStockValue", CommonCls.ConvertDecimalZero(txtClosingStockValue.Text)); // As Per Nagori Sir @10-10-2017

                Session["HT"] = HT;
                Session["format"] = "Pdf";
                Session["FileName"] = "BalanceSheet";
                Response.Redirect("FrmReportViewer.aspx");
            }
        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = ex.Message;
        }
    }

}