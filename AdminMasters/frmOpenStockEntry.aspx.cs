using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmOpenStockEntry : System.Web.UI.Page
{
    ItemOpenningStockModel objplOpenStock;
    DataTable gridOpenStockenDT;

    public DataTable VsDtOpenStock 
    {
        get { return (DataTable)ViewState["gridOpenStock"]; }
        set { ViewState["gridOpenStock"] = value; } 
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = lblmsg.CssClass = ""; 

        if (!IsPostBack)
        {
            FillItemName();
            FillUnitName();
            FillWareHouse();

            VsDtOpenStock = null;

            //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            //{
            //    btnSave.Visible = true;
            //    Session["MasterWrite"] = 0;
            //}
        }
    }
    void FillUnitName()
    {
        objplOpenStock = new ItemOpenningStockModel()
        {

            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
        };

        string uri = string.Format("ItemOpenStock/FillUnit");

        DataSet dsItemUnit = CommonCls.ApiPostDataSet(uri, objplOpenStock);
        if (dsItemUnit.Tables.Count > 0)
        {
            DataTable dtPrimaryUnit = dsItemUnit.Tables[0];
            DataTable dtSecondryUnit = dsItemUnit.Tables[0];

            ddlPriUnit.DataSource = dtPrimaryUnit;
            ddlPriUnit.DataTextField = "UnitName";
            ddlPriUnit.DataValueField = "UnitID";
            ddlPriUnit.DataBind();
            ddlPriUnit.Items.Insert(0, new ListItem("-- Select --", "0"));

            ddlsecunit.DataSource = dtSecondryUnit;
            ddlsecunit.DataTextField = "UnitName";
            ddlsecunit.DataValueField = "UnitID";
            ddlsecunit.DataBind();
            ddlsecunit.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
    }
    void FillItemName()
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
            ddlItemName.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
            ddlItemName.SelectedIndex = 0;
        }

    }
    void FillWareHouse()
    {
        objplOpenStock = new ItemOpenningStockModel()
        {
            Ind = 2,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
        };

        string uri = string.Format("ItemOpenStock/FillWareHouse");
        DataTable dtWareHouse = CommonCls.ApiPostDataTable(uri, objplOpenStock);
        if (dtWareHouse.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = dtWareHouse;
            ddlwarehouse.DataTextField = "WareHouseAddress";
            ddlwarehouse.DataValueField = "WareHouseID";
            ddlwarehouse.DataBind();
            ddlwarehouse.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
            ddlwarehouse.SelectedIndex = 0;
        }
    }

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CommonCls.ConvertIntZero(ddlItemName.SelectedValue) > 0)
        {
            FillItemDetail();
            fillGrid();
        }
        else
        {
            gridOpenStock.DataSource = null;
            gridOpenStock.DataBind();
            //grdOpenStockEntry.DataSource = null;
            //grdOpenStockEntry.DataBind();
        }
        ddlwarehouse.Focus();
    }
    void FillItemDetail()
    {
        DataTable dtItemDetail = new DataTable();
        dtItemDetail = (DataTable)ViewState["ItemNameList"];
        DataRow[] dr = dtItemDetail.Select("ItemID='" + ddlItemName.SelectedValue + "'");
        lblDisc.Text = dr[0]["ItemDescription"].ToString();
        ddlPriUnit.SelectedValue = CommonCls.ConvertIntZero(dr[0]["ItemUnitID"]).ToString();
        if (CommonCls.ConvertIntZero(dr[0]["ItemUnitID"].ToString()) == 0)
        {
            txtPriQty.Enabled = false;
        }
        else
        {
            txtPriQty.Enabled = true;
        }
        ddlsecunit.SelectedValue = CommonCls.ConvertIntZero(dr[0]["ItemMinorUnitID"]).ToString();
        if (CommonCls.ConvertIntZero(dr[0]["ItemMinorUnitID"]) == 0)
        {
            txtsecQty.Enabled = false;
        }
        else
        {
            txtsecQty.Enabled = true;
        }

        // ddlItemName.SelectedValue;
    }

    DataTable createdtGrdDT()     //Create DataTable..
    {
        gridOpenStockenDT = new DataTable();
        //gridOpenStockenDT.Columns.Add("ItemUnitID", typeof(string));
        gridOpenStockenDT.Columns.Add("CompanyID", typeof(string));
        gridOpenStockenDT.Columns.Add("WareHouseID", typeof(string));
        gridOpenStockenDT.Columns.Add("Address", typeof(string));
        gridOpenStockenDT.Columns.Add("ItemUnitID", typeof(string));
        gridOpenStockenDT.Columns.Add("ItemOpeningQty", typeof(string));
        gridOpenStockenDT.Columns.Add("ItemMinorUnitID", typeof(string));
        gridOpenStockenDT.Columns.Add("ItemMinorUnitQty", typeof(string));
        gridOpenStockenDT.Columns.Add("ItemOpeningRate", typeof(string));
        gridOpenStockenDT.Columns.Add("ItemOpeningDate", typeof(string));




        //gridOpenStockenDT = new DataTable();
        //gridOpenStockenDT.Columns.Add("OrgId", typeof(string));
        //gridOpenStockenDT.Columns.Add("ItemID", typeof(string));
        //gridOpenStockenDT.Columns.Add("WareHouseId", typeof(string));
        //gridOpenStockenDT.Columns.Add("WareHouseName", typeof(string));
        //gridOpenStockenDT.Columns.Add("OpeningUnit", typeof(string));
        //gridOpenStockenDT.Columns.Add("OpeningQty", typeof(string));
        //gridOpenStockenDT.Columns.Add("OpeningMinorUnit", typeof(string));
        //gridOpenStockenDT.Columns.Add("OpeningMinorQty", typeof(string));
        //gridOpenStockenDT.Columns.Add("OpRate", typeof(string));
        //gridOpenStockenDT.Columns.Add("OpDate", typeof(string));
        return gridOpenStockenDT;
    }
    protected void gridOpenStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "RemoveRow")
            {
                gridOpenStockenDT = VsDtOpenStock;
                gridOpenStockenDT.Rows[rowIndex].Delete();

                //ViewState["grdData"] = gridOpenStockenDT;
                gridOpenStock.DataSource = VsDtOpenStock = gridOpenStockenDT;
                gridOpenStock.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = lblmsg.CssClass = ""; 

            if (CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
            {
                ddlItemName.Focus();
                ShowMessage("Fill Item Name..!", false);
                return;
            }
            if (CommonCls.ConvertIntZero(ddlwarehouse.SelectedValue) == 0)
            {
                ddlwarehouse.Focus();
                ShowMessage("Select WareHouse ..!", false);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtPriQty.Text) == 0)
            {
                txtPriQty.Focus();
                ShowMessage("Select Item Opening Quantity ..!", false);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtPriQty.Text) == 0)
            {
                txtPriQty.Focus();
                txtPriQty.Text = "";
                ShowMessage("Enter Opening Item Quantity..!", false);
                return;
            }
            if (CommonCls.ConvertDecimalZero(txtOpRate.Text) == 0)
            {
                txtOpRate.Focus();
                txtOpRate.Text = "";
                ShowMessage("Enter Item Rate..! ", false);
                return;
            }

            if (VsDtOpenStock == null)
            {
               VsDtOpenStock = createdtGrdDT();
            }
            

            if (VsDtOpenStock.Rows.Count > 0)
            {
                foreach (DataRow row in VsDtOpenStock.Rows)
                {
                    //decimal opRate = Convert.ToDecimal(row["OpRate"].ToString());
                    //int itemID = Convert.ToInt32(row["ItemID"].ToString());
                    //int Warehousetext = Convert.ToInt32(row["WareHouseId"].ToString());
                    decimal opRate = Convert.ToDecimal(row["ItemOpeningRate"].ToString());
                    int itemID = Convert.ToInt32(row["ItemUnitID"].ToString());
                    int Warehousetext = Convert.ToInt32(row["WareHouseID"].ToString());
                    if (Convert.ToInt32(ddlItemName.SelectedValue) == itemID && opRate == Convert.ToDecimal(txtOpRate.Text) && Warehousetext == Convert.ToInt32(ddlwarehouse.SelectedValue))
                    {
                        ShowMessage("Rate allready exist for this item.", false);
                        return;
                    }
                }
            }
            DataRow dr = VsDtOpenStock.NewRow();
            #region Old
            //dr["OrgId"] = GlobalSession.OrgID;
            //dr["ItemID"] = ddlItemName.SelectedValue;
            //dr["WareHouseId"] = ddlwarehouse.SelectedItem.Value;
            //dr["WareHouseName"] = ddlwarehouse.SelectedItem.Text;
            //dr["OpeningUnit"] = ddlPriUnit.SelectedValue;
            //dr["OpeningQty"] = txtPriQty.Text;
            //if (ddlsecunit.SelectedValue == "0")
            //{
            //    dr["OpeningMinorUnit"] = 0;
            //}
            //else
            //{
            //    dr["OpeningMinorUnit"] = ddlsecunit.SelectedValue;
            //}

            //dr["OpeningMinorQty"] = txtsecQty.Text;
            //dr["OpRate"] = txtOpRate.Text;
            //dr["OpDate"] = CommonCls.ConvertToDate(txtDate.Text);
            #endregion

            dr["CompanyID"] = GlobalSession.OrgID;
            dr["ItemUnitID"] = ddlItemName.SelectedValue;
            dr["WareHouseID"] = ddlwarehouse.SelectedItem.Value;
            dr["Address"] = ddlwarehouse.SelectedItem.Text;
            dr["ItemUnitID"] = ddlPriUnit.SelectedValue;
            dr["ItemOpeningQty"] = txtPriQty.Text;
            if (ddlsecunit.SelectedValue == "0")
            {
                dr["ItemMinorUnitID"] = 0;
            }
            else
            {
                dr["ItemMinorUnitID"] = ddlsecunit.SelectedValue;
            }

            dr["ItemMinorUnitQty"] = "0";
            dr["ItemOpeningRate"] = txtOpRate.Text;
            dr["ItemOpeningDate"] = CommonCls.ConvertToDate(txtDate.Text);
            VsDtOpenStock.Rows.Add(dr);
            gridOpenStock.DataSource = VsDtOpenStock;
            gridOpenStock.DataBind();
            clearGrid();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    void fillGrid()
    {
        objplOpenStock = new ItemOpenningStockModel()
        {
            Ind = 32,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
            ItemID = Convert.ToInt32(ddlItemName.SelectedValue),
        };

        string uri = string.Format("ItemOpenStock/FillGridItem");
        DataTable FillGridItemName = CommonCls.ApiPostDataTable(uri, objplOpenStock);
        if (FillGridItemName.Rows.Count > 0)
        {
            //grdOpenStockEntry.DataSource = FillGridItemName;
            //grdOpenStockEntry.DataBind();

             FillGridItemName.Columns["CompanyID"].SetOrdinal(0);
             FillGridItemName.Columns["WareHouseID"].SetOrdinal(1);
             FillGridItemName.Columns["Address"].SetOrdinal(2);
             FillGridItemName.Columns["ItemUnitID"].SetOrdinal(3);
             FillGridItemName.Columns["ItemOpeningQty"].SetOrdinal(4);
             FillGridItemName.Columns["ItemMinorUnitID"].SetOrdinal(5);
             FillGridItemName.Columns["ItemMinorUnitQty"].SetOrdinal(6);
             FillGridItemName.Columns["ItemOpeningRate"].SetOrdinal(7);
             FillGridItemName.Columns["ItemOpeningDate"].SetOrdinal(8);

             if (FillGridItemName.Columns.Contains("ItemOpeningRate1"))
             {
                 FillGridItemName.Columns.Remove("ItemOpeningRate1");
             }
             if (FillGridItemName.Columns.Contains("UnitName"))
             {
                 FillGridItemName.Columns.Remove("UnitName");
             }
             foreach (DataRow item in FillGridItemName.Rows)
             {
                 item["ItemOpeningDate"] = CommonCls.ConvertToDate(item["ItemOpeningDate"].ToString());
             }

            gridOpenStock.DataSource = VsDtOpenStock = FillGridItemName;
            gridOpenStock.DataBind();
        }
        else
        {
            gridOpenStock.DataSource = null;
            gridOpenStock.DataBind();
            //grdOpenStockEntry.DataSource = null;
            //grdOpenStockEntry.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            objplOpenStock = new ItemOpenningStockModel();

            if (CommonCls.ConvertIntZero(ddlItemName.SelectedValue) == 0)
            {
                ddlItemName.Focus();
                ShowMessage("Fill Item Detail..", false);
                return;
            }
            if (gridOpenStock.Rows.Count > 0)
            {

                objplOpenStock.Ind = 31;
                objplOpenStock.OrgID = GlobalSession.OrgID;
                objplOpenStock.YrCD = GlobalSession.YrCD;
                objplOpenStock.User = GlobalSession.UserID;
                objplOpenStock.IP = GlobalSession.IP;
                objplOpenStock.ItemID = Convert.ToInt32(ddlItemName.SelectedValue);

                objplOpenStock.DtItem = VsDtOpenStock;

                if (objplOpenStock.DtItem.Columns.Contains("ItemID"))
                {
                    objplOpenStock.DtItem.Columns.Remove("ItemID");
                }
                //if (objplOpenStock.DtItem.Columns.Contains("WareHouseName"))
                //{
                //    objplOpenStock.DtItem.Columns.Remove("WareHouseName");
                //}
                if (objplOpenStock.DtItem.Columns.Contains("Address"))
                {
                    objplOpenStock.DtItem.Columns.Remove("Address");
                }
               

                string uri = string.Format("ItemOpenStock/SaveStockEntry");
                DataTable dtSaveopenStockEntry = CommonCls.ApiPostDataTable(uri, objplOpenStock);
                if (dtSaveopenStockEntry.Rows.Count > 0)
                {
                    ShowMessage("Data Is SucessFully Saved..", true);
                    ClearAfterSaved();
                    VsDtOpenStock = null;
                    // fillGrid();

                }
                else
                {
                    ShowMessage("Data Is Not Saved..", false);

                }
            }
            else
            {
                ShowMessage("Fill All Item Detail..", false);

            }
            fillGrid();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    public void ClearAfterSaved()
    {
        ddlwarehouse.ClearSelection();
        txtPriQty.Text = "";
        txtOpRate.Text = "";
        ddlItemName.ClearSelection();
        gridOpenStock.DataSource = new DataTable();

        ddlPriUnit.ClearSelection(); ddlsecunit.ClearSelection();
        lblDisc.Text = "";

        gridOpenStock.DataBind();
        //grdOpenStockEntry.DataSource = null;
        //grdOpenStockEntry.DataBind();
    }
    public void clearGrid()
    {
        ddlwarehouse.ClearSelection();
        //ddlPriUnit.ClearSelection();
        //ddlsecunit.ClearSelection();
        txtPriQty.Text = txtsecQty.Text = txtOpRate.Text = txtDate.Text = lblmsg.Text = lblmsg.CssClass = ""; 
    }
    public void ClearAll()
    {
        ddlItemName.Focus();
        ddlItemName.ClearSelection();
        gridOpenStockenDT = new DataTable();
        gridOpenStock.DataSource = gridOpenStockenDT;
        gridOpenStock.DataBind();
        VsDtOpenStock = null;
        lblmsg.Text = lblmsg.CssClass = txtPriQty.Text = txtOpRate.Text = lblDisc.Text = "";
        ddlwarehouse.ClearSelection();
        ddlPriUnit.ClearSelection();
        ddlsecunit.ClearSelection();
        //grdOpenStockEntry.DataSource = null;
        //grdOpenStockEntry.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void gridOpenStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblPrimaryUnit = (Label)e.Row.FindControl("lblPrimaryUnit");
                if (CommonCls.ConvertIntZero(lblPrimaryUnit.Text) == 0)
                {
                    lblPrimaryUnit.Text = "";
                }
                else
                {
                    ddlPriUnit.SelectedValue = lblPrimaryUnit.Text;
                    lblPrimaryUnit.Text = ddlPriUnit.SelectedItem.Text;
                }

                Label lblSecondryUnit = (Label)e.Row.FindControl("lblSecondryUnit");
                if (CommonCls.ConvertIntZero(lblSecondryUnit.Text) == 0)
                {
                    lblSecondryUnit.Text = "";
                }
                else
                {
                    ddlsecunit.SelectedValue = lblSecondryUnit.Text;
                    lblSecondryUnit.Text = ddlsecunit.SelectedItem.Text;
                }

                //Label lblWarehouseId = (Label)e.Row.FindControl("lblWarehouseId");
                ////Label lblWarehouseName = (Label)e.Row.FindControl("lblWarehouseName");
                //if (CommonCls.ConvertIntZero(lblWarehouseId.Text) == 0)
                //{
                //    ddlwarehouse.SelectedValue = CommonCls.ConvertIntZero(lblWarehouseId.Text).ToString();
                //    lblWarehouseId.Text = ddlwarehouse.SelectedItem.Text;
                //}
            }
        }
    }
    public void ShowMessage(string Message, bool type)
    {
        lblmsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblmsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        //    object sender = UpdatePanel1;
        //    Message = Message.Replace("'", "");
        //    Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "'); });", true);
    }
}