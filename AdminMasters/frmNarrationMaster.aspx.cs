using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmNarrationMaster : System.Web.UI.Page
{
    NarrationMasterModel objNarrMastermodel;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = lblMsg.CssClass = "";

        if (!IsPostBack)
        {
            FillVoucherType();
            //if (Convert.ToInt32(Session["MasterWrite"].ToString()) == 12 || GlobalSession.IsAdmin == 1)
            //{
            //    btnSave.Visible = true;
            //    Session["MasterWrite"] = 0;
            //}
        }
    }
    private void FillVoucherType()
    {
        try
        {
            objNarrMastermodel = new NarrationMasterModel()
            {
                Ind = 22,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                // YrCD = GlobalSession.YrCD,
                //VchType = Convert.ToInt32(Session["VchType"]),
            };

            string uri = string.Format("NarrationMaster/FillVoucher");
            DataTable WarehouseVoucherlist = CommonCls.ApiPostDataTable(uri, objNarrMastermodel);
            if (WarehouseVoucherlist.Rows.Count > 0)
            {
                ddlFillVoucher.DataSource = WarehouseVoucherlist;
                ddlFillVoucher.DataTextField = "DocTypeDesc";
                ddlFillVoucher.DataValueField = "DocTypeID";
                ddlFillVoucher.DataBind();
                ddlFillVoucher.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }

    private void FillGrid()
    {
        objNarrMastermodel = new NarrationMasterModel()
        {
            Ind = 1,
            OrgID = GlobalSession.OrgID,
            BrID = GlobalSession.BrID,
            YrCD = GlobalSession.YrCD,
            DocTypeID = Convert.ToInt32(ddlFillVoucher.SelectedValue),
        };

        string uri = string.Format("NarrationMaster/FillGrid");
        DataTable WarehouseFillGridlist = CommonCls.ApiPostDataTable(uri, objNarrMastermodel);
        if (WarehouseFillGridlist.Rows.Count > 0)
        {
            grdNarration.DataSource = WarehouseFillGridlist;
            grdNarration.DataBind();

        }
    }
    protected void ddlFillVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (ddlFillVoucher.SelectedValue == "0")
            {
                ddlFillVoucher.Focus();
                ShowMessage("Select Voucher Type..!", false);
                return;
            }
            if (string.IsNullOrEmpty(txtSnarration.Text.Trim()))
            {
                txtSnarration.Focus();
                ShowMessage("Enter Narration..!", false);
                return;
            }
            objNarrMastermodel = new NarrationMasterModel()
            {
                Ind = 2,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                User = GlobalSession.UserID,
                IP = GlobalSession.IP,
            };

            objNarrMastermodel.DocTypeID = Convert.ToInt32(ddlFillVoucher.SelectedItem.Value);

            objNarrMastermodel.NarrDesc = txtSnarration.Text;
            string uri = string.Format("NarrationMaster/SaveProcess");
            DataTable WarehouSaveProcess = CommonCls.ApiPostDataTable(uri, objNarrMastermodel);
            if (WarehouSaveProcess.Rows.Count > 0)
            {
                ShowMessage("Data Is SucessFully Saved..!", true);

            }
            else
            {
                ShowMessage("Data Is Not Saved SucessFully..!", false);
            }
            FillGrid();
            clear();
        }
        catch (Exception ex)
        {
            // ShowMessage(ex.Message, false);
        }
    }

    void clearall()
    {
        ddlFillVoucher.Focus();
        ddlFillVoucher.ClearSelection();
        txtSnarration.Text = lblMsg.Text = lblMsg.CssClass = "";
        grdNarration.DataSource = null;
        grdNarration.DataBind();
    }
    void clear()
    {
        ddlFillVoucher.ClearSelection();
        txtSnarration.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearall();
    }
    public void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}


