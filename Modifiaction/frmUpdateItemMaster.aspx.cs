using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdateItemMaster : System.Web.UI.Page
{
    UpdateItemMasterModel objUpdItemMaster;

    protected void Page_Load(object sender, EventArgs e)
    {

        lblMsg.CssClass = "";
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ddlItems.Focus();
            LoadItemMasterDDL();
            divItemGroupDesc.Visible = false;
            btnUpdate.Enabled = false;
            ddlMinorGroup.Enabled = false;

            ItemsFalse();
            //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            //{
            //    btnUpdate.Visible = true;
            //    Session["MasterWrite"] = 0;

            //}

        }

    }

    void ItemsFalse()
    {
        txtItemName.Enabled = txtShortName.Enabled = ddlItemUnit.Enabled = txtSellingRate.Enabled = ddlItemType.Enabled =
            txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = ddlIsUnitInd.Enabled =
            txtItemDescription.Enabled = txtHSNSACCode.Enabled = txtHSNSACDesc.Enabled = lnkHSNSACCodeSearch.Enabled = ddlTaxrate.Enabled = false;
    }

    void ItemsTrue()
    {
        txtItemName.Enabled = txtShortName.Enabled = ddlItemUnit.Enabled = txtSellingRate.Enabled = ddlItemType.Enabled =
            txtItemDescription.Enabled = txtHSNSACCode.Enabled = txtHSNSACDesc.Enabled = lnkHSNSACCodeSearch.Enabled = ddlTaxrate.Enabled = true;
    }

    void LoadItemMasterDDL()
    {
        try
        {
            objUpdItemMaster = new UpdateItemMasterModel();
            objUpdItemMaster.OrgID = GlobalSession.OrgID;
            objUpdItemMaster.BrID = GlobalSession.BrID;
            objUpdItemMaster.YrCD = GlobalSession.YrCD;
            string uri = string.Format("UpdateItemMaster/ItemMasterDDL");
            DataSet dsItemMaster = CommonCls.ApiPostDataSet(uri, objUpdItemMaster);
            if (dsItemMaster.Tables.Count > 0)
            {
                ddlItems.DataSource = dsItemMaster.Tables["Items"];
                ddlItems.DataTextField = "ItemName";
                ddlItems.DataValueField = "ItemID";
                ddlItems.DataBind();
                ddlItems.Items.Insert(0, new ListItem("-- Select --", "0"));
                //--------------------------------------------MinorGroup-----------------------------------
                ddlMinorGroup.DataSource = ViewState["ItemGroup"] = dsItemMaster.Tables["ItemGroup"];
                ddlMinorGroup.DataTextField = "MinorGrName";
                ddlMinorGroup.DataValueField = "MinorGrCode";
                ddlMinorGroup.DataBind();
                ddlMinorGroup.Items.Insert(0, new ListItem("-- Select --", "0"));
                //--------------------------------Binding Item Unit----------------------------------------
                ddlItemUnit.DataSource = dsItemMaster.Tables["ItemUnit"];
                ddlItemUnit.DataTextField = "UnitName";
                ddlItemUnit.DataValueField = "UnitID";
                ddlItemUnit.DataBind();
                ddlItemUnit.Items.Insert(0, new ListItem("-- Select --", "0"));


                ddlMinorUnit.DataSource = dsItemMaster.Tables["ItemUnit"];
                ddlMinorUnit.DataTextField = "UnitName";
                ddlMinorUnit.DataValueField = "UnitID";
                ddlMinorUnit.DataBind();
                ddlMinorUnit.Items.Insert(0, new ListItem("-- Select --", "0"));

                //------------------------------Binding Texrate--------------------------------------------

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlItems.SelectedValue == "0")
            {
                ShowMessage("Select Item.", false);
                ItemsFalse();
                ddlItems.Focus();
                return;
            }

            ItemsTrue();

            objUpdItemMaster = new UpdateItemMasterModel();
            objUpdItemMaster.Ind = 2;
            objUpdItemMaster.OrgID = GlobalSession.OrgID;
            objUpdItemMaster.BrID = GlobalSession.BrID;
            objUpdItemMaster.YrCD = GlobalSession.YrCD;
            objUpdItemMaster.ItemID = Convert.ToInt32(ddlItems.SelectedValue);
            string uri = string.Format("UpdateItemMaster/LoadItemInfo");
            DataTable dtItemInfo = CommonCls.ApiPostDataTable(uri, objUpdItemMaster);
            if (dtItemInfo.Rows.Count > 0)
            {
                if (ddlMinorGroup.Items.FindByValue(dtItemInfo.Rows[0]["ItemMinorGroupID"].ToString()) != null)
                    ddlMinorGroup.SelectedValue = dtItemInfo.Rows[0]["ItemMinorGroupID"].ToString();
                else
                    ddlMinorGroup.SelectedValue = "0";

                DataTable dtItemGroup = (DataTable)ViewState["ItemGroup"];
                if (dtItemGroup != null)
                {
                    int rowIndex = ddlMinorGroup.SelectedIndex;
                    if (rowIndex > 0)
                    {
                        DataRow row = dtItemGroup.Rows[rowIndex - 1];
                        if (row != null)
                        {
                            lblItemGroup.Text = "(Main Group) " + row["MainGrName"].ToString() + " --> (Sub Group) " + row["SubGrName"].ToString();
                            hfMainGrCode.Value = row["MainGrCode"].ToString();
                            hfSubGrCode.Value = row["SubGrCode"].ToString();
                            hfItemGroupID.Value = row["ItemGroupID"].ToString();
                            divItemGroupDesc.Visible = true;
                        }
                    }
                }

                txtItemName.Text = dtItemInfo.Rows[0]["ItemName"].ToString();
                txtShortName.Text = dtItemInfo.Rows[0]["ItemShortName"].ToString();
                ddlItemUnit.SelectedValue = dtItemInfo.Rows[0]["ItemUnitID"].ToString();
                txtSellingRate.Text = dtItemInfo.Rows[0]["ItemSellingRate"].ToString();
                ddlItemType.SelectedValue = dtItemInfo.Rows[0]["GoodsServiceIndication"].ToString();
                txtItemDescription.Text = dtItemInfo.Rows[0]["ItemDescription"].ToString();
                txtHSNSACCode.Text = dtItemInfo.Rows[0]["HSNSACCode"].ToString();

                if (ddlTaxrate.Items.FindByText(dtItemInfo.Rows[0]["TaxRateType"].ToString()) != null)
                    ddlTaxrate.SelectedValue = ddlTaxrate.Items.FindByText(dtItemInfo.Rows[0]["TaxRateType"].ToString()).Value;

                //ddlTaxrate.SelectedValue = dtItemInfo.Rows[0]["TaxRate"].ToString();
                ddlMinorUnit.SelectedValue = dtItemInfo.Rows[0]["ItemMinorUnitID"].ToString();
                txtMinorUnitQty.Text = dtItemInfo.Rows[0]["ItemMinorUnitQty"].ToString();

                HSNSACCodeSearch(Convert.ToInt64(txtHSNSACCode.Text));

                ddlItems.Enabled = btnSearch.Enabled = false;
                btnUpdate.Enabled = true;

                if (GlobalSession.StockMaintaineByMinorUnit)
                {
                    ddlIsUnitInd.Enabled = false;
                    ddlIsUnitInd.SelectedValue = "1";
                    txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = true;
                }
                else
                {
                    ddlIsUnitInd.Enabled = true;
                    ddlIsUnitInd.SelectedValue = "0";
                    txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlIsUnitInd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlIsUnitInd.SelectedValue) == 1)
            txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = true;
        else
            txtMinorUnitQty.Enabled = ddlMinorUnit.Enabled = false;
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
        ddlMinorUnit.Focus();
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
        }
    }

    protected void lnkHSNSACCodeSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ItemsTrue();
            HSNSACCodeSearch(Convert.ToInt64(txtHSNSACCode.Text));
        }
        catch (Exception ex)
        {

        }
    }

    void HSNSACCodeSearch(long HSNSACCode)
    {
        ItemsTrue();
        lblHSNSACErrorMSG.Text = "";
        objUpdItemMaster = new UpdateItemMasterModel();

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
        //        objUpdItemMaster.HSNSACCode1 = hsnCode;
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
        //        objUpdItemMaster.HSNSACCode1 = sacCode;
        //    }
        //}

        objUpdItemMaster.Ind = 30;
        objUpdItemMaster.BrID = GlobalSession.BrID;
        objUpdItemMaster.YrCD = GlobalSession.YrCD;
        objUpdItemMaster.HSNSACCode1 = HSNSACCode;

        string uri = string.Format("UpdateItemMaster/LoadHSNSCDesc");
        DataTable dtHSNSACDesc = CommonCls.ApiPostDataTable(uri, objUpdItemMaster);
        if (dtHSNSACDesc.Rows.Count > 0)
        {
            txtHSNSACDesc.Text = dtHSNSACDesc.Rows[0]["HSNSACDesc"].ToString();
            btnUpdate.Focus();
            //btnUpdate.Enabled = true;
        }
        else
        {
            if (ddlItemType.SelectedValue == "1")
            {
                txtHSNSACCode.Focus();
                txtHSNSACDesc.Text = "";
                // lblMsg.Text = "Invalid HSN Code!";
                //ShowMessage(lblMsg.Text, false);
            }
            else
            {
                txtHSNSACCode.Focus();
                txtHSNSACDesc.Text = "";
                //lblMsg.Text = "Invalid SAC Code!";
                //ShowMessage(lblMsg.Text, false);
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            Response.Redirect("~/frmLogin.aspx", false);
            return;
        }

        ItemsTrue();
        bool IsValid = ValidationBtnSave();
        if (!IsValid)
        {
            ShowMessage(msgSave, false);
            return;
        }

        objUpdItemMaster = new UpdateItemMasterModel();
        objUpdItemMaster.Ind = 3;
        objUpdItemMaster.OrgID = GlobalSession.OrgID;
        objUpdItemMaster.BrID = GlobalSession.BrID;
        objUpdItemMaster.YrCD = GlobalSession.YrCD;
        objUpdItemMaster.ItemID = Convert.ToInt64(ddlItems.SelectedValue);
        objUpdItemMaster.ItemName = txtItemName.Text;
        objUpdItemMaster.ItemShortName = txtShortName.Text;
        objUpdItemMaster.ItemUnitID = Convert.ToInt16(ddlItemUnit.SelectedValue);
        objUpdItemMaster.GoodServiceInd = Convert.ToInt16(ddlItemType.SelectedValue);
        objUpdItemMaster.ItemSellingRate = CommonCls.ConvertDecimalZero(txtSellingRate.Text);
        objUpdItemMaster.ItemDesc = txtItemDescription.Text;
        objUpdItemMaster.HSNSACCode = txtHSNSACCode.Text == null ? "" : txtHSNSACCode.Text; //ddlHSNSACCode.SelectedValue; 
        objUpdItemMaster.TaxRate = CommonCls.ConvertDecimalZero(ddlTaxrate.SelectedValue) < CommonCls.ConvertDecimalZero("0.05") ? 0 : CommonCls.ConvertDecimalZero(ddlTaxrate.SelectedValue);
        objUpdItemMaster.TaxRateDesc = ddlTaxrate.SelectedItem.Text;

        objUpdItemMaster.ItemMinorUnitID = CommonCls.ConvertIntZero(ddlMinorUnit.SelectedValue);
        objUpdItemMaster.ItemMinorUnitQyt = CommonCls.ConvertDecimalZero(txtMinorUnitQty.Text);

        objUpdItemMaster.ItemCode = Convert.ToInt64(hfMainGrCode.Value + hfSubGrCode.Value + ddlMinorGroup.SelectedValue);
        objUpdItemMaster.User = GlobalSession.UserID;
        objUpdItemMaster.IP = GlobalSession.IP;//CommonCls.GetIP();


        string uri = string.Format("UpdateItemMaster/UpdateItem");
        DataTable dtSave = CommonCls.ApiPostDataTable(uri, objUpdItemMaster);
        if (dtSave.Rows.Count > 0)
        {
            if (dtSave.Rows[0]["Column1"].ToString() == "1")
            {
                ClearAll();
                ShowMessage("Item Update successfully.", true);
                ddlItems.Focus();
                //LoadItemMasterDDL();
            }
            else if (dtSave.Rows[0]["Column1"].ToString() == "2")
            {
                lblMsg.Text = "Item Already Added.";
                ShowMessage(lblMsg.Text, false);
                txtItemName.Focus();
            }
            else if (dtSave.Rows[0]["Column1"].ToString() == "3")
            {
                //lblMsg.Text = "Invalid HSN / SAC Code.";
                ShowMessage(lblMsg.Text, false);
                txtHSNSACCode.Focus();
            }
        }
        else
        {
            ShowMessage("Record Not Save Please Try Again.", false);
        }

        //objItemMaster.DtItemOpening = (DataTable)ViewState["dtItemOSE"];

        //if (objItemMaster.DtItemOpening != null)
        //{
        //    if (objItemMaster.DtItemOpening.Columns.Contains("Warehouse"))
        //    {
        //        objItemMaster.DtItemOpening.Columns.Remove("Warehouse");
        //    }
        //}
        //else
        //{
        //    objItemMaster.DtItemOpening = DtItemOSESchema();
        //    DataRow drItemOpening = objItemMaster.DtItemOpening.NewRow();

        //    drItemOpening["CompanyID"] = 0;
        //    drItemOpening["WarehouseID"] = 0;
        //    drItemOpening["OpeningQty"] = 0;
        //    drItemOpening["OpeningRate"] = 0;
        //    drItemOpening["OpeningDate"] = "";
        //    objItemMaster.DtItemOpening.Rows.Add(drItemOpening);
        //    objItemMaster.DtItemOpening.Columns.Remove("Warehouse");
        //}

        //string uri = string.Format("UpdateItemMaster/UpdateItem");
        //DataTable dtSave = CommonCls.ApiPostDataTable(uri, objItemMaster);
        //if (dtSave.Rows.Count > 0)
        //{
        //    if (dtSave.Rows[0]["Column1"].ToString() == "1")
        //    {
        //        ClearAll();
        //        ShowMessage("Item Save successfully.", true);
        //        ddlMinorGroup.Focus();
        //    }
        //    else if (dtSave.Rows[0]["Column1"].ToString() == "2")
        //    {
        //        lblMsg.Text = "Item Already Added.";
        //        ShowMessage(lblMsg.Text, false);
        //        txtItemName.Focus();
        //    }
        //    else if (dtSave.Rows[0]["Column1"].ToString() == "3")
        //    {
        //        lblMsg.Text = "Invalid HSN / SAC Code.";
        //        ShowMessage(lblMsg.Text, false);
        //        txtHSNSACCode.Focus();
        //    }
        //}
        //else
        //{
        //    lblMsg.Text = "Record Not Save Please Try Again.";
        //}
    }

    string msgSave;
    bool ValidationBtnSave()
    {
        if (ddlMinorGroup.SelectedValue == "0")
        {
            ddlMinorGroup.Focus();
            msgSave = lblMsg.Text = "Select Minor Group.";
            return false;
        }
        if (string.IsNullOrEmpty(txtItemName.Text))
        {
            txtItemName.Focus();
            msgSave = lblMsg.Text = "Enter Item Name.";
            return false;
        }
        if (ddlItemUnit.SelectedValue == "0")
        {
            ddlItemUnit.Focus();
            msgSave = lblMsg.Text = "Select Item Unit.";
            return false;
        }
        if (ddlItemType.SelectedValue == "0")
        {
            ddlItemType.Focus();
            msgSave = lblMsg.Text = "Select Item Type.";
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
        if (string.IsNullOrEmpty(txtHSNSACCode.Text))
        {
            txtHSNSACCode.Focus();
            msgSave = lblMsg.Text = "Enter HSN / SAC Code.";
            return false;
        }
        return true;
    }

    void ClearAll()
    {
        ddlItems.ClearSelection();
        ddlItems.Enabled = btnSearch.Enabled = true;
        ddlMinorGroup.ClearSelection();
        txtItemName.Text = txtShortName.Text = txtSellingRate.Text = txtHSNSACCode.Text = txtHSNSACDesc.Text = lblMsg.Text = "";
        ddlItemUnit.ClearSelection();
        ddlItemType.ClearSelection();
        ViewState["dtItemOSE"] = ViewState["HSNSACCode"] = null;
        //grdItemMaster.DataSource = new DataTable();   //ddlHSNSACCode.DataSource =
        //grdItemMaster.DataBind();
        divItemGroupDesc.Visible = false;
        lblItemGroup.Text = "";
        // btnUpdate.Enabled = false;
        ItemsFalse();


        txtMinorUnitQty.Text = "";
        ddlTaxrate.SelectedValue = "-1";
        ddlMinorUnit.ClearSelection();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}