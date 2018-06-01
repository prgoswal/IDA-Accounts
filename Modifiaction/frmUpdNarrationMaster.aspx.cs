using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Updation_frmUpdNarrationMaster : System.Web.UI.Page
{
    UpdateNarrationModel objUpdNarraModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillVoucherType();
        }
    }

    private void FillVoucherType()
    {
        try
        {
            objUpdNarraModel = new UpdateNarrationModel()
            {
                Ind = 22,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                // YrCD = GlobalSession.YrCD,
                //VchType = Convert.ToInt32(Session["VchType"]),
            };

            string uri = string.Format("UpdateNarration/FillVoucher");
            DataTable NarrationVoucherlist = CommonCls.ApiPostDataTable(uri, objUpdNarraModel);
            if (NarrationVoucherlist.Rows.Count > 0)
            {
                ddlFillVoucher.DataSource = NarrationVoucherlist;
                ddlFillVoucher.DataTextField = "DocTypeDesc";
                ddlFillVoucher.DataValueField = "DocTypeID";
                ddlFillVoucher.DataBind();
                ddlFillVoucher.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

            if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            {
                btnUpdate.Visible = true;
                Session["MasterWrite"] = 0;

            }
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }
    private void FillGrid()
    {
        try
        {
            objUpdNarraModel = new UpdateNarrationModel()
           {
               Ind = 1,
               OrgID = GlobalSession.OrgID,
               BrID = GlobalSession.BrID,
               YrCD = GlobalSession.YrCD,
               DocTypeID = Convert.ToInt32(ddlFillVoucher.SelectedValue),
           };

            string uri = string.Format("UpdateNarration/FillGrid");
            DataTable WarehouseFillGridlist = CommonCls.ApiPostDataTable(uri, objUpdNarraModel);
            if (WarehouseFillGridlist.Rows.Count > 0)
            {
                grdNarration.DataSource = WarehouseFillGridlist;
                grdNarration.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlFillVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }
    //protected void grdNarration_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        int rowIndex = Convert.ToInt32(e.CommandArgument);
    //        if (e.CommandName == "Edit")
    //        {
    //            Label lblDesc = (Label)grdNarration.Rows[rowIndex].FindControl("lblDesc");
    //            txtSnarration.Text = lblDesc.Text;
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    int NarrationID
    {
        get { return CommonCls.ConvertIntZero(ViewState["NarrationID"]); }
        set { ViewState["NarrationID"] = value; }
    }
    protected void grdNarration_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            NarrationID = 0;
            txtSnarration.Enabled = true;
            int rowIndex = Convert.ToInt32(e.NewEditIndex);
            Label lblNarrtionId = (Label)grdNarration.Rows[rowIndex].FindControl("lblNarrtionId");
            Label lblDesc = (Label)grdNarration.Rows[rowIndex].FindControl("lblDesc");
            NarrationID = Convert.ToInt32(lblNarrtionId.Text);
            txtSnarration.Text = lblDesc.Text;
            txtSnarration.Focus();
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (ddlFillVoucher.SelectedValue == "0")
            {
                ddlFillVoucher.Focus();
                lblMsg.Text = "Select Voucher Type..!";
                return;
            }
            if (string.IsNullOrEmpty(txtSnarration.Text))
            {
                lblMsg.Text = "Edit item is required..!";
               // ShowMessage("Edit item is required.", false);
                txtSnarration.Focus();
                return;
            }

            if (NarrationID != 0)
            {
                objUpdNarraModel = new UpdateNarrationModel()
                {
                    Ind = 3,
                    OrgID = GlobalSession.OrgID,
                    BrID = GlobalSession.BrID,
                    YrCD = GlobalSession.YrCD,
                    User = GlobalSession.UserID,
                    IP = GlobalSession.IP,
                };

                objUpdNarraModel.DocTypeID = Convert.ToInt32(ddlFillVoucher.SelectedItem.Value);
                objUpdNarraModel.NarrationID = NarrationID;
                objUpdNarraModel.NarrDesc = txtSnarration.Text;
                string uri = string.Format("UpdateNarration/UpdateProcess");
                DataTable NarrationUpdateProcess = CommonCls.ApiPostDataTable(uri, objUpdNarraModel);
                if (NarrationUpdateProcess.Rows.Count > 0)
                {
                    lblMsg.Text = "Data Is SucessFully Updated..!";
                    NarrationID = 0;
                }
                else
                {
                    lblMsg.Text = " Data Is NOt Updated SucessFully..!";
                }
                FillGrid();
                clear();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    void clear()
    {
        ddlFillVoucher.ClearSelection();
        txtSnarration.Text = "";
        txtSnarration.Enabled = false;
       // lblMsg.Text = "";
    }

    void clearall()
    {
        ddlFillVoucher.Focus();
        ddlFillVoucher.ClearSelection();
        txtSnarration.Text = "";
        lblMsg.Text = "";
        grdNarration.DataSource = null;
        grdNarration.DataBind();
        txtSnarration.Enabled = false;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearall();
    }
}