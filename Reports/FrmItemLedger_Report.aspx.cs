using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmItemLedger_Report : System.Web.UI.Page
{
    ItemOpenningStockModel objplOpenStock;
    ItemMasterModel objItemMaster;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtAsOnDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();

            LoadItemMasterDDL();
            FillItemName();
        }
    }
    void LoadItemMasterDDL()
    {
        try
        {
            objItemMaster = new ItemMasterModel();
            objItemMaster.OrgID = GlobalSession.OrgID;
            objItemMaster.BrID = GlobalSession.BrID;
            objItemMaster.YrCD = GlobalSession.YrCD;
            string uri = string.Format("ItemMaster/ItemMasterDDL");
            DataSet dsItemMaster = CommonCls.ApiPostDataSet(uri, objItemMaster);
            if (dsItemMaster.Tables.Count > 0)
            {
                ddlMinorGroup.DataSource = ViewState["ItemGroup"] = dsItemMaster.Tables["ItemGroup"];
                ddlMinorGroup.DataTextField = "MinorGrName";
                ddlMinorGroup.DataValueField = "MinorGrCode";
                ddlMinorGroup.DataBind();
                ddlMinorGroup.Items.Insert(0, new ListItem { Text = "---  All  ---", Value = "0" });
                ddlMinorGroup.SelectedIndex = 0;


                ddlWarehouse.DataSource = dsItemMaster.Tables["Warehouse"];
                ddlWarehouse.DataTextField = "WareHouseAddress";
                ddlWarehouse.DataValueField = "WareHouseID";
                ddlWarehouse.DataBind();
                ddlWarehouse.Items.Insert(0, new ListItem { Text = "---  All  ---", Value = "0" });
                ddlWarehouse.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlMinorGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["ItemGroup"] != null)
        {
            if (ddlMinorGroup.SelectedValue != "0")
            {
                DataTable dtItemGroup = (DataTable)ViewState["ItemGroup"];
                if (dtItemGroup != null)
                {
                    int rowIndex = ddlMinorGroup.SelectedIndex;
                    DataRow row = dtItemGroup.Rows[rowIndex - 1];
                    if (row != null)
                    {
                        //  lblItemGroup.Text = dtItemGroup.Rows[0]["MainGrName"].ToString() + dtItemGroup.Rows[0]["SubGrName"].ToString();
                        lblItemGroup.Text = "(Main Group) " + row["MainGrName"].ToString() + " --> (Sub Group) " + row["SubGrName"].ToString();
                        hfMainGrCode.Value = row["MainGrCode"].ToString();
                        hfSubGrCode.Value = row["SubGrCode"].ToString();
                        hfItemGroupID.Value = row["ItemGroupID"].ToString();
                    }
                }
            }
        }
    }

    private void FillItemName()
    {
        objplOpenStock = new ItemOpenningStockModel()
        {
            Ind = 30,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
        };

        string uri = string.Format("ItemOpenStock/FillItemName");
        DataTable dtItemName = CommonCls.ApiPostDataTable(uri, objplOpenStock);
        if (dtItemName.Rows.Count > 0)
        {
            ddlItemName.DataSource = ViewState["ItemNameList"] = dtItemName;
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "ItemID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem { Text = "---  All  ---", Value = "0" });
            ddlItemName.SelectedIndex = 0;
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtAsOnDate.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please Enter As On Date ";
            txtAsOnDate.Focus();
            return;
        }

        else
        {

            Session["Report"] = "RptItemLedger";
            Hashtable HT = new Hashtable();
            HT.Add("Ind", 1);
            HT.Add("OrgID", GlobalSession.OrgID);
            HT.Add("BrID", GlobalSession.BrID);
            HT.Add("yrcode", GlobalSession.YrCD);
            HT.Add("Itemcd", ddlItemName.SelectedValue);
            HT.Add("HSNSACNo", "");
            HT.Add("CompanyName", GlobalSession.OrgName);
            HT.Add("BranchName", GlobalSession.BrName);
            HT.Add("ReportHeading", "Stock Ledger ");
            HT.Add("asonDate", txtAsOnDate.Text.Substring(6, 4) + "/" + txtAsOnDate.Text.Substring(3, 2) + "/" + txtAsOnDate.Text.Substring(0, 2));

            HT.Add("ItemMainGroupID", 0);
            HT.Add("ItemSubGroupID", 0);
            HT.Add("ItemMinorGroupID", ddlMinorGroup.SelectedValue);
            HT.Add("WareHouseID", ddlWarehouse.SelectedValue);

            Session["HT"] = HT;
            Session["format"] = "Pdf";
            Session["FileName"] = "ItemLedger";
            Response.Redirect("FrmReportViewer.aspx");
        }
    }

}