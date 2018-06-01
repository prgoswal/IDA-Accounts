using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Warehouse : System.Web.UI.Page
{
    WarehouseModel objplWarehouse;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillState();
            // FillGSTINNo();
        }
    }

    //private void FillGSTINNo()
    //{
    //    try
    //    {
    //        objplWarehouse = new WarehouseModel()
    //        {
    //            Ind = 21,
    //            OrgID = Convert.ToInt32(Session["OrgID"]),
    //            BrID = Convert.ToInt32(Session["BrID"]),
    //            YrCD = Convert.ToInt32(Session["YrCD"]),
    //            VchType = Convert.ToInt32(Session["VchType"]),
    //        };

    //        string uri = string.Format("Warehouse/CompnyGSTIN");
    //        DataTable WarehouseGSTINlist = CommonCls.ApiPostDataTable(uri, objplWarehouse);
    //        if (WarehouseGSTINlist.Rows.Count > 0)
    //        {
    //            ddlGSTINNo.DataSource = WarehouseGSTINlist;
    //            ddlGSTINNo.DataTextField = "GSTIN";
    //            ddlGSTINNo.DataValueField = "GSTINID";
    //            ddlGSTINNo.DataBind();
    //            ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0000"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // ShowMessage(ex.Message, false);
    //    }
    //}

    private void FillState()
    {
        try
        {
            objplWarehouse = new WarehouseModel()
            {
                Ind = 15,
                OrgID = GlobalSession.OrgID,// Convert.ToInt32(Session["OrgID"]),
                BrID = GlobalSession.BrID,//Convert.ToInt32(Session["BrID"]),
                YrCD = GlobalSession.YrCD,//Convert.ToInt32(Session["YrCD"]),
            };

            string uri = string.Format("Warehouse/StateList");
            DataTable Warehousestatelist = CommonCls.ApiPostDataTable(uri, objplWarehouse);
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
                lblMsg.Text = "Insert WareHouseAddress..! ";
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
                lblMsg.Text = "Insert City Name..! ";
                return;
            }
            if (string.IsNullOrEmpty(txtPin.Text))
            {
                txtPin.Focus();
                lblMsg.Text = "Insert Pin Code..! ";
                return;
            }
            objplWarehouse = new WarehouseModel()
            {
                Ind = 1,
                OrgID = GlobalSession.OrgID,// Convert.ToInt32(Session["OrgID"]),
                BrID = GlobalSession.BrID,// Convert.ToInt32(Session["BrID"]),
                YrCD = GlobalSession.YrCD,//Convert.ToInt32(Session["YrCD"]),
                //VchType = Convert.ToInt32(Session["VchType"]),
                User = GlobalSession.UserID,//Convert.ToInt32(Session["UserId"]),
                IP = GlobalSession.IP, // Convert.ToString( Session["IP"]),         
            };

            // objplWarehouse.GSTINID = Convert.ToInt32(ddlGSTINNo.SelectedItem.Value);
            objplWarehouse.Address = txtWhouseAdd.Text;
            objplWarehouse.StateID = Convert.ToInt32(ddlstate.SelectedItem.Value);
            objplWarehouse.City = txtCity.Text;
            objplWarehouse.PinCode = Convert.ToInt32(txtPin.Text);
            string uri = string.Format("Warehouse/SaveWarehouseData");
            DataTable WarehouSaveProcess = CommonCls.ApiPostDataTable(uri, objplWarehouse);
            if (WarehouSaveProcess.Rows.Count > 0)
            {
                lblMsg.Text = "Data Is SucessFully Saved..!";

            }
            else
            {
                lblMsg.Text = " Data Is NOt Saved SucessFully..!";
            }
            ClearAll();
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllcontrol();
    }

    public void ClearAll()
    {

        ddlstate.ClearSelection();
        txtCity.Text = "";
        txtWhouseAdd.Text = "";
        txtPin.Text = "";
        //lblMsg.Text = "";
    }
    public void ClearAllcontrol()
    {
        txtWhouseAdd.Focus();
        ddlstate.ClearSelection();
        txtCity.Text = "";
        txtWhouseAdd.Text = "";
        txtPin.Text = "";
        lblMsg.Text = "";
    }
}





