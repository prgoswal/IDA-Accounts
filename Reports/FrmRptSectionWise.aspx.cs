using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmRptSectionWise : System.Web.UI.Page
{
    BudgetReportModel ObjRptModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (GlobalSession.IsAdmin >= 0 && GlobalSession.DepartmentID != 0)
            {
                ddlReportName.Items.RemoveAt(3);
                ddlReportName.Items.RemoveAt(2);
                ddlReportName.Items.RemoveAt(1);
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedValue == "0")
        {
            lblErrorMsg.Text = "Select Financial Year.";
            ddlYear.Focus();
            return;
        }
        if (ddlReportName.SelectedValue == "0")
        {
            lblErrorMsg.Text = "Select Report Name.";
            ddlReportName.Focus();
            return;
        }

        else
        {
            ddlYear.Enabled = false;
            Hashtable HT = new Hashtable();
            HT.Add("RptInd", ddlReportName.SelectedValue);
            HT.Add("CompanyID", GlobalSession.OrgID);
            HT.Add("BranchID", GlobalSession.BrID);

            if (ddlYear.SelectedValue == "1")
            {
                HT.Add("YrCD", GlobalSession.YrCD);
            }
            else
            {
                HT.Add("YrCD", GlobalSession.BudgetYrCD);
            }
            HT.Add("CompanyName", "कार्यालय इंदौर विकास प्राधिकारी, इंदौर");
            //HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("UserID", GlobalSession.UserID);
            // HT.Add("LedgerType", ddlLedgerType.SelectedValue);
            Session["HT"] = HT;

            Session["format"] = "Pdf";
            Session["FileName"] = "SectiobWiseSummary";
            if (ddlReportName.SelectedValue == "1")
            {
                Session["Report"] = "RptBudgetMainSummary";
                //HT.Add("ReportHeading","");
            }
            else if (ddlReportName.SelectedValue == "2")
            {
                Session["Report"] = "RptSectiobWiseSummary";
                HT.Add("ReportHeading", "Section Wise Summary");
            }
            else if (ddlReportName.SelectedValue == "3")
            {
                HT.Add("GroupID", ddlInEx.SelectedValue);
                Session["Report"] = "RptSubSectiobWiseSummary";
                HT.Add("ReportHeading", "Sub-Section Wise Summary");
            }
            else if (ddlReportName.SelectedValue == "4")
            {
                Session["Report"] = "RptBudgetDetail";
                //HT.Add("ReportHeading", "Section/Sub-Section Wise Budget Head-Detail");
            }
            else if (ddlReportName.SelectedValue == "5")
            {
                Session["Report"] = "RptBudgetSummary-2";
                //HT.Add("ReportHeading", "Section/Sub-Section Wise Budget Head-Detail");
            }
            else if (ddlReportName.SelectedValue == "6")
            {
                Session["Report"] = "RptGroupWiseSummary";
                //HT.Add("ReportHeading", "Section/Sub-Section Wise Budget Head-Detail");
            }

            else if (ddlReportName.SelectedValue == "8")
            {
                HT.Add("BudgetHead", ddlBudgetHead.SelectedValue);
                Session["Report"] = "RptBudgetHeadSchemeWiseReport";
            }
            else
                if (ddlReportName.SelectedValue == "14")
                {

                    Session["Report"] = "RptExpenceSummary";
                }
                else
                    if (ddlReportName.SelectedValue == "13")
                    {

                        Session["Report"] = "RptIncomeSummary";
                    }
                    else if (ddlReportName.SelectedValue == "15")
                    {
                        Session["Report"] = "RptGroupWiseSummary2";
                    }
                    else if (ddlReportName.SelectedValue == "16")
                    {
                        HT.Add("BudgetHead", ddlSectionName.SelectedValue);
                        Session["Report"] = "RptBudgetHeadSchemeWiseReport2";
                    }

                    else if (ddlReportName.SelectedValue == "18")
                    {

                        Session["Report"] = "RptIncomewithMainSummarySummary";
                    }
                    else
                    {
                        Session["Report"] = "RptIdaDptSubDpt";
                        //HT.Add("ReportHeading", "Section/Sub-Section Wise Budget Head-Detail");
                    }
            Response.Redirect("FrmReportViewer.aspx");
        }

    }

    protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportName.SelectedValue == "8")
        {
            LoadAccountHead();
            ddlBudgetHead.Enabled = true;
            divDropDown.Visible = true;
            ddlInEx.Enabled = false;
            divINExDropDown.Visible = false;
            ddlSectionName.Enabled = false;
            divSectionCD.Visible = false;
        }
        else if (ddlReportName.SelectedValue == "3")
        {
            ddlInEx.Enabled = true;
            divINExDropDown.Visible = true;
            ddlBudgetHead.Enabled = false;
            divDropDown.Visible = false;
            ddlSectionName.Enabled = false;
            divSectionCD.Visible = false;
        }
        else if (ddlReportName.SelectedValue == "16")
        {
            LoadSectionCd();
            ddlSectionName.Enabled = true;
            divSectionCD.Visible = true;

            ddlInEx.Enabled = false;
            divINExDropDown.Visible = false;
            ddlBudgetHead.Enabled = false;
            divDropDown.Visible = false;
        }
        else
        {
            ddlBudgetHead.ClearSelection();
            ddlBudgetHead.Enabled = false;
            divDropDown.Visible = false;
            ddlInEx.Enabled = false;
            divINExDropDown.Visible = false;

            ddlSectionName.ClearSelection();
            ddlSectionName.Enabled = false;
            divSectionCD.Visible = false;
        }
    }


    void LoadAccountHead()
    {
        try
        {
            ObjRptModel = new BudgetReportModel()
            {
                RptInd = 7,
                CompanyID = GlobalSession.OrgID,
                BranchID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
            };
            String uri = string.Format("Report/AccountHeadLoad");
            DataTable dtBudgetHead = CommonCls.ApiPostDataTable(uri, ObjRptModel);
            if (dtBudgetHead.Rows.Count > 0)
            {
                ddlBudgetHead.DataSource = dtBudgetHead;
                ddlBudgetHead.DataTextField = "AccName";
                ddlBudgetHead.DataValueField = "BudgetHeadCD";
                ddlBudgetHead.DataBind();
                //ddlBudgetHead.Items.Insert(-1, new ListItem("--Select--", "-1"));
                ddlBudgetHead.Items.Insert(0, new ListItem("-- All --", "0"));
            }
        }
        catch
        {

        }
    }



    void LoadSectionCd()
    {
        try
        {
            ObjRptModel = new BudgetReportModel()
            {
                RptInd = 17,
                CompanyID = GlobalSession.OrgID,
                BranchID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
            };
            String uri = string.Format("Report/FillSectionName");
            DataTable dtSectionName = CommonCls.ApiPostDataTable(uri, ObjRptModel);
            if (dtSectionName.Rows.Count > 0)
            {
                ddlSectionName.DataSource = dtSectionName;
                ddlSectionName.DataTextField = "SectionNameHindi";
                ddlSectionName.DataValueField = "SectionCD";
                ddlSectionName.DataBind();
                //ddlBudgetHead.Items.Insert(-1, new ListItem("--Select--", "-1"));
                ddlSectionName.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch
        {

        }
    }


}