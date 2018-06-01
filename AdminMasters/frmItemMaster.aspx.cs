using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMasters_frmItemMaster : System.Web.UI.Page
{
    ItemMasterModel objItemMaster;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            try
            {
                ddlMinorGroup.Focus();
                LoadItemMasterDDL();
                divItemGroupDesc.Visible = false;
                //btnSave.Enabled = false;
                //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin==1)
                //{
                //    btnSave.Visible = true;
                //    Session["MasterWrite"] = 0;
                //}

                if (GlobalSession.StockMaintaineByMinorUnit)
                {
                    ddlIsUnitInd.Enabled = false;

                    ddlIsUnitInd.SelectedValue = "1";
                    txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = txtSecQty.Enabled = true;
                }
                else
                {
                    ddlIsUnitInd.Enabled = true;
                    ddlIsUnitInd.SelectedValue = "0";
                    txtSecQty.Enabled = false;
                    txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error : Internal Server Error!", false);
            }
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
                ddlMinorGroup.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlItemUnit.DataSource = ddlPriUnit.DataSource = dsItemMaster.Tables["ItemUnit"];
                ddlItemUnit.DataTextField = ddlPriUnit.DataTextField = "UnitName";
                ddlItemUnit.DataValueField = ddlPriUnit.DataValueField = "UnitID";
                ddlItemUnit.DataBind(); ddlPriUnit.DataBind();
                ddlItemUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlPriUnit.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlWarehouse.DataSource = dsItemMaster.Tables["Warehouse"];
                ddlWarehouse.DataTextField = "WareHouseAddress";
                ddlWarehouse.DataValueField = "WareHouseID";
                ddlWarehouse.DataBind();
                ddlWarehouse.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlMinorUnit.DataSource = ddlSecUnit.DataSource = ViewState["dtMinorUnit"] = dsItemMaster.Tables["ItemUnit"];
                ddlMinorUnit.DataTextField = ddlSecUnit.DataTextField = "UnitName";
                ddlMinorUnit.DataValueField = ddlSecUnit.DataValueField = "UnitID";
                ddlMinorUnit.DataBind(); ddlSecUnit.DataBind();

                ddlMinorUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlSecUnit.Items.Insert(0, new ListItem("-- Select --", "0"));

                ddlTaxrate.DataSource = dsItemMaster.Tables["TaxRate"];
                ddlTaxrate.DataTextField = "TaxRateDesc";
                ddlTaxrate.DataValueField = "TaxRate";
                ddlTaxrate.DataBind();
                ddlTaxrate.Items.Insert(0, new ListItem("-- Select --", "-1"));

                double TaxValue = 0;
                foreach (ListItem item in ddlTaxrate.Items)
                {
                    if (item.Value == "0")
                    {
                        item.Value = TaxValue.ToString();
                        TaxValue = (TaxValue + 0.01);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
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
                        lblItemGroup.Text = "(Main Group) " + row["MainGrName"].ToString() + " --> (Sub Group) " + row["SubGrName"].ToString();
                        hfMainGrCode.Value = row["MainGrCode"].ToString();
                        hfSubGrCode.Value = row["SubGrCode"].ToString();
                        hfItemGroupID.Value = row["ItemGroupID"].ToString();
                        divItemGroupDesc.Visible = true;
                    }
                    txtItemName.Focus();
                }
            }
            else
            {
                divItemGroupDesc.Visible = false;
            }
        }
    }

    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemType.SelectedValue != "0")
        {
            txtHSNSACCode.Text = "";

            if (ddlItemType.SelectedValue == "1")
                txtHSNSACCode.MaxLength = 8;
            else
                txtHSNSACCode.MaxLength = 6;

            txtHSNSACCode.Focus();
            ddlItemType.Focus();
        }
    }

    protected void lnkHSNSACCodeSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblHSNSACErrorMSG.Text = "";
            objItemMaster = new ItemMasterModel();

            if (ddlItemType.SelectedValue == "0")
            {
                ShowMessage("Select Item Type.", false);
                ddlItemType.Focus();
                return;
            }

            //if (ddlItemType.SelectedValue == "1")
            //{
            //    long hsnCode = "";
            //    if (txtHSNSACCode.Text.Length == 2)
            //    {
            //        hsnCode = txtHSNSACCode.Text + "000000";
            //    }
            //    else if (txtHSNSACCode.Text.Length == 4)
            //    {
            //        hsnCode = txtHSNSACCode.Text + "0000";
            //    }
            //    else if (txtHSNSACCode.Text.Length == 8)
            //    {
            //        hsnCode = txtHSNSACCode.Text;
            //    }
            //    else
            //    {
            //        txtHSNSACCode.Focus();
            //        ShowMessage("Invalid HSN Code", false);
            //        return;
            //    }

            //    if (!string.IsNullOrEmpty(hsnCode))
            //    {
            //        objItemMaster.HSNSACCode1 = hsnCode;
            //    }
            //}
            //else if (ddlItemType.SelectedValue == "2")
            //{
            //    long sacCode = "";
            //    if (txtHSNSACCode.Text.Length == 2)
            //    {
            //        sacCode = txtHSNSACCode.Text + "0000";
            //    }
            //    else if (txtHSNSACCode.Text.Length == 4)
            //    {
            //        sacCode = txtHSNSACCode.Text + "00";
            //    }
            //    else if (txtHSNSACCode.Text.Length == 6)
            //    {
            //        sacCode = txtHSNSACCode.Text;
            //    }
            //    else
            //    {
            //        txtHSNSACCode.Focus();
            //        ShowMessage("Invalid Sac Code", false);
            //        return;
            //    }

            //    if (!string.IsNullOrEmpty(sacCode))
            //    {
            //        objItemMaster.HSNSACCode1 = sacCode;
            //    }
            //}

            objItemMaster.Ind = 30;
            objItemMaster.HSNSACCode1 = Convert.ToInt64(txtHSNSACCode.Text);

            string uri = string.Format("ItemMaster/LoadHSNSCDesc");
            DataTable dtHSNSACDesc = CommonCls.ApiPostDataTable(uri, objItemMaster);
            if (dtHSNSACDesc.Rows.Count > 0)
            {
                txtHSNSACDesc.Text = dtHSNSACDesc.Rows[0]["HSNSACDesc"].ToString();
                ddlTaxrate.SelectedValue = dtHSNSACDesc.Rows[0]["TAXRATE"].ToString();
                //btnSave.Enabled = true;
                ddlWarehouse.Focus();
            }
            else
            {
                ddlTaxrate.SelectedValue = "-1";

                if (ddlItemType.SelectedValue == "1")
                {
                    txtHSNSACCode.Focus();
                    txtHSNSACDesc.Text = "";
                    //ShowMessage("Invalid HSN Code!",false);
                }
                else
                {
                    txtHSNSACCode.Focus();
                    txtHSNSACDesc.Text = "";
                    //ShowMessage("Invalid SAC Code!",false);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlHSNSACCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWarehouse.Focus();
    }

    protected void btnAddItemOSE_Click(object sender, EventArgs e)
    {
        bool IsValid = ValidationBtnAddItemOSE();
        if (!IsValid)
        {
            return;
        }
        BindItemOSEGrid();
        ClearAllAfterBtnAddItemOSE();
        ddlWarehouse.Focus();
    }


    bool ValidationBtnAddItemOSE()
    {
        if (ddlItemUnit.SelectedValue == ddlMinorUnit.SelectedValue)
        {
            ShowMessage("Item Primary Unit & Secondary Unit Could Not Be Same!", false);
            ddlMinorUnit.Focus();
            return false;
        }

        if (CommonCls.ConvertIntZero(ddlItemUnit.SelectedValue) == 0)
        {
            ddlItemUnit.Focus();
            ShowMessage("Select Item Unit.", false);
            return false;
        }
        if (ddlWarehouse.SelectedValue == "0")
        {
            ddlWarehouse.Focus();
            ShowMessage("Select Warehouse.", false);
            return false;
        }
        if (ViewState["dtItemOSE"] != null)
        {
            DataTable dtItemOSE = (DataTable)ViewState["dtItemOSE"];
            DataRow[] rows = dtItemOSE.Select("WarehouseId='" + ddlWarehouse.SelectedItem.Value + "' And OpeningQty='" + txtOpeningQty.Text + "' And OpRate='" + txtOpeningRate.Text + "'");
            if (rows.Count() >= 1)
            {
                ShowMessage("This Qty And Rate Alredy Added On Same Warehouse.", false); //Repeated Record Occurred
                ddlWarehouse.Focus();
                ClearAllAfterBtnAddItemOSE();
                return false;
            }
        }
        if (CommonCls.ConvertDecimalZero(txtOpeningQty.Text) == 0)
        {
            txtOpeningQty.Focus();
            ShowMessage("Enter Opening Qty.", false);
            return false;
        }
        if (CommonCls.ConvertDecimalZero(txtOpeningRate.Text) == 0)
        {
            txtOpeningRate.Focus();
            ShowMessage("Enter Opening Rate.", false);
            return false;
        }

        if (GlobalSession.StockMaintaineByMinorUnit)
        {
            if (CommonCls.ConvertIntZero(txtSecQty.Text) == 0)
            {
                txtSecQty.Focus();
                ShowMessage("Enter Opening Secondary Qty.", false);
                return false;
            }
            if (CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue) == 0)
            {
                ShowMessage("Select Secondary Unit.", false);
                ddlMinorUnit.Focus();
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtOpeningDate.Text))
        {
            txtOpeningDate.Focus();
            ShowMessage("Enter Opening Date.", false);
            return false;
        }
        return true;
    }

    DataTable DtItemOSESchema()
    {
        DataTable dtItemOSE = new DataTable();

        //dtItemOSE.Columns.Add("CompanyID", typeof(int));
        //dtItemOSE.Columns.Add("WarehouseID", typeof(int));
        //dtItemOSE.Columns.Add("Warehouse", typeof(string));
        //dtItemOSE.Columns.Add("OpeningQty", typeof(int));
        //dtItemOSE.Columns.Add("OpeningRate", typeof(decimal));
        //dtItemOSE.Columns.Add("OpeningDate", typeof(string));


        dtItemOSE.Columns.Add("OrgId", typeof(string));
        dtItemOSE.Columns.Add("ItemID", typeof(string));
        dtItemOSE.Columns.Add("WareHouseId", typeof(string));
        dtItemOSE.Columns.Add("WareHouseName", typeof(string));
        dtItemOSE.Columns.Add("OpeningUnit", typeof(string));
        dtItemOSE.Columns.Add("OpeningQty", typeof(string));
        dtItemOSE.Columns.Add("OpeningMinorUnit", typeof(string));
        dtItemOSE.Columns.Add("OpeningMinorQty", typeof(string));
        dtItemOSE.Columns.Add("OpRate", typeof(string));
        dtItemOSE.Columns.Add("OpDate", typeof(string));
        return dtItemOSE;
    }

    void BindItemOSEGrid()
    {
        DataTable dtItemOSE = new DataTable();
        if (ViewState["dtItemOSE"] == null)
        {
            dtItemOSE = DtItemOSESchema();
        }
        else
        {
            dtItemOSE = (DataTable)ViewState["dtItemOSE"];
        }

        DataRow DrItemOSE = dtItemOSE.NewRow();
        DrItemOSE["OrgId"] = Convert.ToInt32(GlobalSession.OrgID);
        DrItemOSE["ItemID"] = 0;
        DrItemOSE["WarehouseId"] = Convert.ToInt32(ddlWarehouse.SelectedValue);
        DrItemOSE["WareHouseName"] = ddlWarehouse.SelectedItem.Text;
        DrItemOSE["OpeningUnit"] = CommonCls.ConvertIntZero(ddlPriUnit.SelectedValue);//ddlItemUnit.SelectedValue;
        DrItemOSE["OpeningQty"] = CommonCls.ConvertDecimalZero(txtOpeningQty.Text);
        if (GlobalSession.StockMaintaineByMinorUnit)
        {
            DrItemOSE["OpeningMinorUnit"] = CommonCls.ConvertIntZero(ddlSecUnit.SelectedValue);//ddlMinorUnit.SelectedValue;
            DrItemOSE["OpeningMinorQty"] = CommonCls.ConvertDecimalZero(txtSecQty.Text);
        }
        else
        {
            DrItemOSE["OpeningMinorUnit"] = 0;
            DrItemOSE["OpeningMinorQty"] = 0;
        }
        DrItemOSE["OpRate"] = CommonCls.ConvertDecimalZero(txtOpeningRate.Text);
        DrItemOSE["OpDate"] = CommonCls.ConvertToDate(txtOpeningDate.Text);
        dtItemOSE.Rows.Add(DrItemOSE);
        grdItemMaster.DataSource = ViewState["dtItemOSE"] = dtItemOSE;
        grdItemMaster.DataBind();
    }

    void ClearAllAfterBtnAddItemOSE()
    {
        ddlWarehouse.ClearSelection();
        txtOpeningQty.Text = txtOpeningRate.Text = txtOpeningDate.Text = txtSecQty.Text = "";
    }

    protected void grdItemMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveRow")
        {
            DataTable dtItemOSE = (DataTable)(ViewState["dtItemOSE"]);
            DataRow row = dtItemOSE.Rows[rowIndex];
            dtItemOSE.Rows[rowIndex].Delete();
            ViewState["dtItemOSE"] = grdItemMaster.DataSource = dtItemOSE;
            grdItemMaster.DataBind();
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


            bool IsValid = ValidationBtnSave();
            if (!IsValid)
            {
                return;
            }

            objItemMaster = new ItemMasterModel();
            objItemMaster.Ind = 3;
            objItemMaster.OrgID = GlobalSession.OrgID;
            objItemMaster.YrCD = GlobalSession.YrCD;
            objItemMaster.ItemMainID = Convert.ToInt32(hfMainGrCode.Value);
            objItemMaster.ItemSubID = Convert.ToInt32(hfSubGrCode.Value);
            objItemMaster.ItemMinorID = Convert.ToInt32(ddlMinorGroup.SelectedValue);
            objItemMaster.ItemGroupID = Convert.ToInt32(hfItemGroupID.Value);
            objItemMaster.ItemName = txtItemName.Text;
            objItemMaster.ItemShortName = txtShortName.Text;
            objItemMaster.ItemUnitID = Convert.ToInt32(ddlItemUnit.SelectedValue);
            objItemMaster.GoodServiceInd = Convert.ToInt32(ddlItemType.SelectedValue);
            objItemMaster.ItemSellingRate = CommonCls.ConvertDecimalZero(txtSellingRate.Text);
            objItemMaster.ItemDesc = txtItemDescription.Text == null ? "" : txtItemDescription.Text;
            objItemMaster.HSNSACCode = txtHSNSACCode.Text; //ddlHSNSACCode.SelectedValue;
            objItemMaster.ItemCode = Convert.ToInt64(hfMainGrCode.Value + hfSubGrCode.Value + ddlMinorGroup.SelectedValue);
            objItemMaster.User = GlobalSession.UserID;
            objItemMaster.IP = GlobalSession.IP;
            objItemMaster.DtItemOpening = (DataTable)ViewState["dtItemOSE"];
            objItemMaster.ItemMinorUnitID = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
            objItemMaster.ItemMinorUnitQyt = CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text);
            objItemMaster.TaxRate = CommonCls.ConvertDecimalZero(ddlTaxrate.SelectedValue) < CommonCls.ConvertDecimalZero("0.05") ? 0 : CommonCls.ConvertDecimalZero(ddlTaxrate.SelectedValue);
            objItemMaster.TaxRateDesc = ddlTaxrate.SelectedItem.Text;
            objItemMaster.StockMaintainInd = 0;


            if (objItemMaster.DtItemOpening != null)
            {
                if (objItemMaster.DtItemOpening.Columns.Contains("ItemID"))
                    objItemMaster.DtItemOpening.Columns.Remove("ItemID");
                if (objItemMaster.DtItemOpening.Columns.Contains("WareHouseName"))
                    objItemMaster.DtItemOpening.Columns.Remove("WareHouseName");
                //if (objItemMaster.DtItemOpening.Columns.Contains("Warehouse"))
                //{
                //    objItemMaster.DtItemOpening.Columns.Remove("Warehouse");
                //}
            }
            else
            {
                objItemMaster.DtItemOpening = DtItemOSESchema();
                DataRow drItemOpening = objItemMaster.DtItemOpening.NewRow();

                if (objItemMaster.DtItemOpening.Columns.Contains("ItemID"))
                    objItemMaster.DtItemOpening.Columns.Remove("ItemID");
                if (objItemMaster.DtItemOpening.Columns.Contains("WareHouseName"))
                    objItemMaster.DtItemOpening.Columns.Remove("WareHouseName");

                drItemOpening["OpeningUnit"] = "";
                drItemOpening["OpeningMinorUnit"] = "";
                drItemOpening["OpeningMinorQty"] = "";

                drItemOpening["OrgId"] = 0;
                drItemOpening["WarehouseId"] = 0;
                drItemOpening["OpeningQty"] = 0;
                drItemOpening["OpRate"] = 0;
                drItemOpening["OpDate"] = "";
                objItemMaster.DtItemOpening.Rows.Add(drItemOpening);
                //objItemMaster.DtItemOpening.Columns.Remove("Warehouse");
            }

            string uri = string.Format("ItemMaster/SaveItem");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, objItemMaster);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["Column1"].ToString() == "1")
                {
                    ClearAll();
                    ShowMessage("Item Save successfully.", true);
                    ddlMinorGroup.Focus();
                }
                else if (dtSave.Rows[0]["Column1"].ToString() == "2")
                {
                    ShowMessage("Item Already Added.", false);
                    txtItemName.Focus();
                }
                else if (dtSave.Rows[0]["Column1"].ToString() == "3")
                {
                    ShowMessage("Invalid HSN / SAC Code.", false);
                    txtHSNSACCode.Focus();
                }
            }
            else
            {
                ShowMessage("Record Not Save Please Try Again.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidationBtnSave()
    {

        if (string.IsNullOrEmpty(txtItemName.Text))
        {
            txtItemName.Focus();
            ShowMessage("Enter Item Name.", false);
            return false;
        }
        if (ddlItemUnit.SelectedValue == "0")
        {
            ddlItemUnit.Focus();
            ShowMessage("Select Item Unit.", false);
            return false;
        }

        if (Convert.ToInt16(ddlIsUnitInd.SelectedValue) == 1)
        {
            if (CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue) == 0)
            {
                ddlMinorUnit.Focus();
                ShowMessage("Select Item Secondary Unit.", false);
                return false;
            }

            //if (CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text) == 0)
            //{
            //    txtMinorUnitQty.Focus();
            //    ShowMessage("Enter Secondary Unit Qty.", false);
            //    return false;
            //}
        }
        //if (GlobalSession.StockMaintaineByMinorUnit)
        //{
        //}
        //else
        //{
        //    if (CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue) > 0)
        //    {
        //        if (CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text) == 0)
        //        {
        //            txtMinorUnitQty.Focus();
        //            ShowMessage("Enter Calculation Factor.", false);
        //            return false;
        //        }
        //    }
        //}

        if (CommonCls.ConvertIntZero(ddlItemUnit.SelectedValue) == CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue))
        {
            ShowMessage("Item Primary Unit & Secondary Unit Could Not Be Same!", false);
            ddlMinorUnit.Focus();
            return false;
        }

        if (ddlItemType.SelectedValue == "0")
        {
            ddlItemType.Focus();
            ShowMessage("Select Item Type.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtHSNSACCode.Text))
        {
            txtHSNSACCode.Focus();
            ShowMessage("Enter HSN / SAC Code.", false);
            return false;
        }

        if (CommonCls.ConvertDecimalZero(ddlTaxrate.SelectedValue) == -1)
        {
            ddlTaxrate.Focus();
            ShowMessage("Select Tax Rate.", false);
            return false;
        }

        return true;
    }

    void ClearAll()
    {
        txtMinorUnitQty.Text = "";
        ddlMinorUnit.ClearSelection();

        lblMsg.Text = "";
        ddlMinorGroup.ClearSelection();
        txtItemName.Text = txtShortName.Text = txtSellingRate.Text = txtItemDescription.Text = txtHSNSACCode.Text = txtHSNSACDesc.Text = "";
        ddlItemUnit.ClearSelection();
        ddlItemType.ClearSelection();
        ddlTaxrate.ClearSelection();
        ViewState["dtItemOSE"] = ViewState["HSNSACCode"] = null;
        grdItemMaster.DataSource = new DataTable();   //ddlHSNSACCode.DataSource =
        grdItemMaster.DataBind();
        divItemGroupDesc.Visible = false;
        lblItemGroup.Text = "";
        //btnSave.Enabled = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void ddlMinorUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CommonCls.ConvertIntZero(ddlItemUnit.SelectedValue) == 0)
        {
            ShowMessage("Select Item Primary Unit First!", false);
            ddlMinorUnit.SelectedValue = "0";
            ddlItemUnit.Focus();
            return;
        }

        if (ddlItemUnit.SelectedValue == ddlMinorUnit.SelectedValue)
        {
            ShowMessage("Item Primary Unit & Secondary Unit Could Not Be Same!", false);
            ddlMinorUnit.Focus();
            return;
        }
        ddlSecUnit.SelectedValue = ddlMinorUnit.SelectedValue;
        ddlMinorUnit.Focus();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemUnit.SelectedValue == ddlMinorUnit.SelectedValue)
        {
            ShowMessage("Item Primary Unit & Secondary Unit Could Not Be Same!", false);
            ddlMinorUnit.Focus();
            return;
        }
        ddlWarehouse.Focus();
    }
    protected void grdItemMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.RowIndex >= 0)
        //    {
        //        DataRow dr = ((DataRowView)e.Row.DataItem).Row;


        //        Label lblPrimaryUnit = (Label)e.Row.FindControl("lblPrimaryUnit");
        //        ddlPriUnit.SelectedValue = lblPrimaryUnit.Text;
        //        lblPrimaryUnit.Text = ddlPriUnit.SelectedItem.Text;

        //        Label lblSecondryUnit = (Label)e.Row.FindControl("lblSecondryUnit");
        //        if (lblSecondryUnit.Text != "0")
        //        {
        //            ddlsecunit.SelectedValue = lblSecondryUnit.Text;
        //            lblSecondryUnit.Text = ddlsecunit.SelectedItem.Text;
        //        }
        //    }
        //}
    }
    protected void ddlItemUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPriUnit.SelectedValue = ddlItemUnit.SelectedValue;
        ddlItemUnit.Focus();
    }
    protected void ddlIsUnitInd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlIsUnitInd.SelectedValue) == 1)
            txtMinorUnitQty.Enabled = txtSecQty.Enabled = ddlMinorUnit.Enabled = true;
        else
            txtMinorUnitQty.Enabled = txtSecQty.Enabled = ddlMinorUnit.Enabled = false;

        ddlIsUnitInd.Focus();
    }
}