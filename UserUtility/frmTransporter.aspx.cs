using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserUtility_frmTransporter : System.Web.UI.Page
{
    TransporterDetailModal objTransporterDetail;
    string uri;
    DataTable VSState
    {
        get { return (DataTable)ViewState["VSState"]; }
        set { ViewState["VSState"] = value; }
    }
    public PlaceHolder ph = new PlaceHolder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GstINOrREgCss();
            BindAll();
            txtTransportationName.Focus();
        }
    }

    private void BindAll()
    {
        try
        {
            objTransporterDetail = new TransporterDetailModal();
            objTransporterDetail.Ind = 2; //For Bind State Dropdown;
            string uri = string.Format("Transporter/BindAll");
            DataSet dsState = CommonCls.ApiPostDataSet(uri, objTransporterDetail);
            if (dsState.Tables.Count > 0)
            {
                //if (dsState.Tables[1].Rows.Count > 0)
                //{
                //    grdSubSection.DataSource = dsState.Tables[1];
                //    grdSubSection.DataBind();
                //    //pnlSectionGrid.Visible = true;
                //    VSSubSection = dsState.Tables[1];

                //    lstSubSeaction.DataSource = dsState.Tables[1];
                //    lstSubSeaction.DataTextField = "SectionName";
                //    lstSubSeaction.DataValueField = "SectionName";
                //    lstSubSeaction.DataBind();
                //}
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    ddlState.DataSource = dsState.Tables[0];
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateID";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("---- Select State ----", "0"));
                    VSState = dsState.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    private void ClearAll()
    {
        txtTransportationName.Text = txtOwner.Text = txtAddress.Text = txtcity.Text = txtpincode.Text = txtgstorreg.Text = "";
        ddlState.ClearSelection();
       // rdogstreg.ClearSelection();
        chkRoad.Checked = false;
        chkRail.Checked = false;
        chkAir.Checked = false;
        chkShip.Checked = false;
        hfTransportID.Value = "0";
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkRoad.Checked == false && chkRail.Checked == false && chkAir.Checked == false && chkShip.Checked == false)
            {
                ShowMessage("Select Alteast One Transport Through ", false);
                chkRoad.Focus();
                return;
            }
            objTransporterDetail = new TransporterDetailModal();
            objTransporterDetail.OrgID = GlobalSession.OrgID;
            objTransporterDetail.BrID = GlobalSession.BrID;
            objTransporterDetail.yrcd = GlobalSession.BudgetYrCD;
            objTransporterDetail.UserID = GlobalSession.UserID;
            objTransporterDetail.IPAddress = GlobalSession.IP;

            DateTime today = DateTime.Today; // As DateTime
            string s_today = today.ToString("dd/MM/yyyy hh:mm tt"); // As String
            objTransporterDetail.EntryDateTime = Convert.ToDateTime(s_today);

            objTransporterDetail.TransportationName = txtTransportationName.Text;
            objTransporterDetail.OwnerName = txtOwner.Text;
            objTransporterDetail.Address = txtAddress.Text;
            objTransporterDetail.City = txtcity.Text;
            objTransporterDetail.State = ddlState.SelectedValue;
            objTransporterDetail.Pincode = CommonCls.ConvertIntZero(txtpincode.Text);
            if (rdogstreg.SelectedValue == "1")
            {
                objTransporterDetail.GSTIN = txtgstorreg.Text;
            }
            else if (rdogstreg.SelectedValue == "2")
            {
                objTransporterDetail.RegistrationNo = CommonCls.ConvertIntZero(txtgstorreg.Text);
            }



            if (chkRoad.Checked == true)
                objTransporterDetail.Road = 1;
            else
                objTransporterDetail.Road = 0;

            if (chkRail.Checked == true)
                objTransporterDetail.Rail = 1;
            else
                objTransporterDetail.Rail = 0;

            if (chkAir.Checked == true)
                objTransporterDetail.Air = 1;
            else
                objTransporterDetail.Air = 0;

            if (chkShip.Checked == true)
                objTransporterDetail.Ship = 1;
            else
                objTransporterDetail.Ship = 0;

            if (hfTransportID.Value == "0")
            {
                objTransporterDetail.Ind = 1;//For Saving Data
                uri = string.Format("Transporter/SaveTranspoter");
            }
            DataTable dtTransporterDetail = CommonCls.ApiPostDataTable(uri, objTransporterDetail);
            if (dtTransporterDetail.Rows.Count > 0)
            {
                ShowMessage("Record Save successfully.", true);
                ClearAll();
                hfTransportID.Value = "0";
                chkRoad.Checked = true;
                
            }
            else
            {
                ShowMessage("Record not Save successfully.", false);
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void rdogstreg_SelectedIndexChanged(object sender, EventArgs e)
    {
        GstINOrREgCss();
    }

    public void GstINOrREgCss()
    {
        if (rdogstreg.SelectedValue == "1")
        {
            txtgstorreg.Text = "";
            txtgstorreg.MaxLength = 15;
            txtgstorreg.Attributes["placeholder"] = "Enter GSTIN";
            txtgstorreg.CssClass = "form-control GSTIN text-uppercase";
           // txtgstorreg.CssClass = "form-control inpt form-control-pd-sm";
            RequiredFieldValidator6.Text ="Please Enter GSTIN";
        }
        else if (rdogstreg.SelectedValue == "2")
        {
            txtgstorreg.Text = "";
            txtgstorreg.MaxLength = 20;
            //txtgstorreg.CssClass = "form-control GSTIN text-uppercase";
            txtgstorreg.CssClass = "form-control inpt num-only form-control-pd-sm";
            txtgstorreg.Attributes["placeholder"] = "Enter Registration ID";
            RequiredFieldValidator6.Text = "Please Enter Registration ID";
        }
    }
}