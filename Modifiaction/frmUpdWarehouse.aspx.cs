using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Updation_frmUpdWarehouse : System.Web.UI.Page
{
    UpdateWarehouseModel objUpdWarehouse;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillWarehouse();

            FillState();
            //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            //{
            //    btnSave.Visible = true;
            //    Session["MasterWrite"] = 0;

            //}
        }
    }

    private void FillState()
    {
        try
        {
            objUpdWarehouse = new UpdateWarehouseModel();

            objUpdWarehouse.Ind = 15;
            objUpdWarehouse.OrgID = GlobalSession.OrgID;
            objUpdWarehouse.BrID = GlobalSession.BrID;
            objUpdWarehouse.YrCD = GlobalSession.YrCD;


            string uri = string.Format("UpdateWarehouse/StateList");
            DataTable Warehousestatelist = CommonCls.ApiPostDataTable(uri, objUpdWarehouse);
            if (Warehousestatelist.Rows.Count > 0)
            {
                ddlstate.DataSource = Warehousestatelist;
                ddlstate.DataTextField = "StateName";
                ddlstate.DataValueField = "StateID";
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, new ListItem("-- Select --", "0000"));
            }
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }

    private void FillWarehouse()
    {
        try
        {
            objUpdWarehouse = new UpdateWarehouseModel();
            objUpdWarehouse.Ind = 2;
            objUpdWarehouse.OrgID = GlobalSession.OrgID;
            objUpdWarehouse.BrID = GlobalSession.BrID;
            objUpdWarehouse.YrCD = GlobalSession.YrCD;

            string uri = string.Format("UpdateWarehouse/fillWarehouse");
            DataTable dtWareHouse = CommonCls.ApiPostDataTable(uri, objUpdWarehouse);
            if (dtWareHouse.Rows.Count > 0)
            {
                ddlwarehouse.DataSource = dtWareHouse;
                ddlwarehouse.DataTextField = "WareHouseAddress";
                ddlwarehouse.DataValueField = "WareHouseID";
                ddlwarehouse.DataBind();
                ddlwarehouse.Items.Insert(0, new ListItem("-Select-", "0"));
                //ddlItemName.Items.Insert(0, "-Select-");
                //ddlwarehouse.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            if (ddlwarehouse.SelectedValue != "0")
            {
                txtWhouseAdd.Enabled = true;
                ddlstate.Enabled = true;
                txtCity.Enabled = true;
                txtPin.Enabled = true;
                btnSave.Enabled = true;
                btnClear.Enabled = true;

                objUpdWarehouse = new UpdateWarehouseModel();
                objUpdWarehouse.Ind = 2;
                objUpdWarehouse.OrgID = GlobalSession.OrgID;
                objUpdWarehouse.BrID = GlobalSession.BrID;
                objUpdWarehouse.YrCD = GlobalSession.YrCD;
                objUpdWarehouse.WareHouseID = Convert.ToInt32(ddlwarehouse.SelectedItem.Value);

                string uri = string.Format("UpdateWarehouse/SearchControl");
                DataTable dtWareHouseSearch = CommonCls.ApiPostDataTable(uri, objUpdWarehouse);
                if (dtWareHouseSearch.Rows.Count > 0)
                {
                    ViewState["dtWareHouseSearch"] = dtWareHouseSearch;
                    txtWhouseAdd.Text = dtWareHouseSearch.Rows[0]["Address"].ToString();
                    ddlstate.SelectedValue = dtWareHouseSearch.Rows[0]["StateID"].ToString();
                    txtCity.Text = dtWareHouseSearch.Rows[0]["City"].ToString();
                    txtPin.Text = dtWareHouseSearch.Rows[0]["PinCode"].ToString();


                    // txtRate.Text = dtItems.Rows[0]["ItemSellingRate"].ToString();
                    //ddlwarehouse.DataSource = dtWareHouseSearch;
                    //ddlwarehouse.DataTextField = "WareHouseAddress";
                    //ddlwarehouse.DataValueField = "WareHouseID";
                    //ddlwarehouse.DataBind();
                    //ddlwarehouse.Items.Insert(0, new ListItem("-Select-", "0"));
                    //ddlItemName.Items.Insert(0, "-Select-");
                    //ddlwarehouse.SelectedIndex = 0;
                }
            } 

            else
            {
                ddlstate.ClearSelection();
                txtCity.Text = "";
                txtWhouseAdd.Text = "";
                txtPin.Text = "";

                txtWhouseAdd.Enabled = false;
                ddlstate.Enabled = false;
                txtCity.Enabled = false;
                txtPin.Enabled = false;
                btnSave.Enabled = false;
                btnClear.Enabled = false;
                ddlstate.ClearSelection();
                txtCity.Text = "";
                txtWhouseAdd.Text = "";
                txtPin.Text = "";

            }
                    
        }
        catch (Exception ex)
        {

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

            if (string.IsNullOrEmpty(txtWhouseAdd.Text.Trim()))
            {
                txtWhouseAdd.Focus();
                lblMsg.Text = "WareHouse Address Is Requred..! ";
                return;
            }

            if (ddlstate.SelectedItem.Value == "0000")
            {
                ddlstate.Focus();
                lblMsg.Text = "Select State..!";
                return;
            }
            if (string.IsNullOrEmpty(txtCity.Text))
            {
                txtCity.Focus();
                lblMsg.Text = "City Name Is Requred..! ";
                return;
            }
            if (string.IsNullOrEmpty(txtPin.Text))
            {
                txtPin.Focus();
                lblMsg.Text = "Pin Code Is Requred..! ";
                return;
            }
            objUpdWarehouse = new UpdateWarehouseModel();

            objUpdWarehouse.Ind = 3;
            objUpdWarehouse.OrgID = GlobalSession.OrgID; // Convert.ToInt32(Session["OrgID"]),
            objUpdWarehouse.BrID = GlobalSession.BrID;  // Convert.ToInt32(Session["BrID"]),
            objUpdWarehouse.YrCD = GlobalSession.YrCD; //Convert.ToInt32(Session["YrCD"]),
            objUpdWarehouse.User = GlobalSession.UserID;//Convert.ToInt32(Session["UserId"]),
            objUpdWarehouse.IP = GlobalSession.IP;     // Convert.ToString( Session["IP"]), 
            objUpdWarehouse.WareHouseID = Convert.ToInt32(ddlwarehouse.SelectedItem.Value);
            
            objUpdWarehouse.Address = txtWhouseAdd.Text;
            objUpdWarehouse.StateID = Convert.ToInt32(ddlstate.SelectedItem.Value);
            objUpdWarehouse.City = txtCity.Text;
            objUpdWarehouse.PinCode = Convert.ToInt32(txtPin.Text);
            string uri = string.Format("UpdateWarehouse/SaveWarehouseData");
            DataTable WarehouSaveProcess = CommonCls.ApiPostDataTable(uri, objUpdWarehouse);
            if (WarehouSaveProcess.Rows.Count > 0)
            {
                lblMsg.Text = "Data Is SucessFully Updated..!";
                AfterSaveClear();
            }
            else
            {
                lblMsg.Text = " Data Is Not  Update..!";
            }
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }

    }

    public void ClearAllcontrol()
    {
        ddlwarehouse.ClearSelection();
        ddlwarehouse.Focus();
        ddlstate.ClearSelection();
        txtCity.Text = "";
        txtWhouseAdd.Text = "";
        txtPin.Text = "";
        lblMsg.Text = "";
        txtWhouseAdd.Enabled = false;
        ddlstate.Enabled = false;
        txtCity.Enabled = false;
        txtPin.Enabled = false;
        btnSave.Enabled = false;
        btnClear.Enabled = false;
    } 
    public void AfterSaveClear()
    {
        ddlwarehouse.ClearSelection();
        ddlwarehouse.Focus();
        ddlstate.ClearSelection();
        txtCity.Text = "";
        txtWhouseAdd.Text = "";
        txtPin.Text = "";
        txtWhouseAdd.Enabled = false;
        ddlstate.Enabled = false;
        txtCity.Enabled = false;
        txtPin.Enabled = false;
        btnSave.Enabled = false;
        btnClear.Enabled = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllcontrol();
    }
}
