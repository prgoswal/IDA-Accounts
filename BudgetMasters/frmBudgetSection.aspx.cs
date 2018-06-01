using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BudgetMasters_frmBudgetSection : System.Web.UI.Page
{
    BudgetSectionModel objBudgetSection;
    DataTable VSSection
    {
        get { return (DataTable)ViewState["VSSection"]; }
        set { ViewState["VSSection"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAll();
            txtSectionName.Focus();
        }
    }

    private void BindAll()
    {
        try
        {
            objBudgetSection = new BudgetSectionModel();
            objBudgetSection.Ind = 11;
            objBudgetSection.OrgID = GlobalSession.OrgID;
            objBudgetSection.BrID = GlobalSession.BrID;
            string uri = string.Format("BudgetSection/BindAll");
            DataTable dtSubSection = CommonCls.ApiPostDataTable(uri, objBudgetSection);
            if (dtSubSection.Rows.Count > 0)
            {
                grdSection.DataSource = dtSubSection;
                grdSection.DataBind();
                VSSection = dtSubSection;


                lstSeaction.DataSource = dtSubSection;
                lstSeaction.DataTextField = "SectionName";
                lstSeaction.DataValueField = "SectionName";
                lstSeaction.DataBind();


                //pnlSectionGrid.Visible = true;

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtSectionName.Text))
            {
                ShowMessage("Enter Section Name.", false);
                txtSectionName.Focus();
                return;
            }


            //if (!string.IsNullOrEmpty(txtSectionName.Text))
            //{
            //    if (Regex.IsMatch((txtSectionName.Text).ToUpper().Trim(), @"/^[ A-Za-z0-9-%().*/&]*$/"))
            //    {
            //        ShowMessage("Enter Valid Section Name.", false);
            //        txtSectionName.Focus();
            //        return;
            //    }
            //    else
            //    {

            //    }
            //}
            if (string.IsNullOrEmpty(txtSectionNameHindi.Text))
            {
                ShowMessage("Enter Section Name(Hindi).", false);
                txtSectionNameHindi.Focus();
                return;
            }

            if (VSSection != null)
            {
                bool IsValid = CheckSectionNameExisting();
                if (!IsValid)
                {
                    return;
                }
            }
            objBudgetSection = new BudgetSectionModel();
            objBudgetSection.OrgID = GlobalSession.OrgID;
            objBudgetSection.BrID = GlobalSession.BrID;
            objBudgetSection.User = GlobalSession.UserID;
            objBudgetSection.IP = GlobalSession.IP;
            objBudgetSection.SectionName = (txtSectionName.Text).ToUpper().Trim();
            objBudgetSection.SectionNameHindi = (txtSectionNameHindi.Text).Trim();
            objBudgetSection.ParentSectionID = 0;
            string uri;
            if (hfSectionID.Value == "0")
            {

                objBudgetSection.Ind = 1;//For Saving Data
                uri = string.Format("BudgetSection/SaveBudgetSection");
            }
            else
            {
                objBudgetSection.Ind = 2;//For Update Data
                objBudgetSection.SectionID = CommonCls.ConvertIntZero(hfSectionID.Value);
                uri = string.Format("BudgetSection/UpdateBudgetSection");
            }

            DataTable dtSection = CommonCls.ApiPostDataTable(uri, objBudgetSection);
            if (dtSection.Rows.Count > 0)
            {
                ShowMessage("Record Save successfully.", true);
                ClearAll();
                hfSectionID.Value = "0";
                BindAll();
            }
            else
            {
                ShowMessage("Record not Save successfully.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }



    private bool CheckSectionNameExisting()
    {
        try
        {
            string SectionName = (txtSectionName.Text).Trim();
            string SectionNameHindi = (txtSectionNameHindi.Text).Trim();

            DataTable dtGRD = VSSection;
            if (dtGRD.Rows.Count > 0)
            {
                DataView dv31 = new DataView(dtGRD);
                dv31.RowFilter = "SectionID Not = " + hfSectionID.Value + "";
                if (dv31.Count > 0)
                {
                    for (int i = 0; i < dv31.ToTable().Rows.Count; i++)
                    {
                        if (SectionName == (dv31.ToTable().Rows[i]["SectionName"].ToString()))
                        {
                            ShowMessage("Section Name is already exits.", false);
                            txtSectionName.Focus();
                            return false;
                        }
                    }
                }
            }



            DataTable dtGRD2 = VSSection;
            if (dtGRD2.Rows.Count > 0)
            {
                DataView dv = new DataView(dtGRD);
                dv.RowFilter = "SectionID Not = " + hfSectionID.Value + "";
                if (dv.Count > 0)
                {
                    for (int i = 0; i < dv.ToTable().Rows.Count; i++)
                    {
                        if (SectionNameHindi == (dv.ToTable().Rows[i]["SectionNameHindi"].ToString()))
                        {
                            ShowMessage("Section Name(Hindi) is already exits.", false);
                            txtSectionNameHindi.Focus();
                            return false;
                        }
                    }
                }
            }






            //for (int i = 0; i < VSSection.Rows.Count; i++)
            //{
            //    if (SectionName == (VSSection.Rows[i]["SectionName"].ToString()))
            //    {
            //        ShowMessage("Section Name is already exits.", false);
            //        txtSectionName.Focus();
            //        return false;
            //    }
            //}

            //for (int j = 0; j < VSSection.Rows.Count; j++)
            //{
            //    if (SectionNameHindi == (VSSection.Rows[j]["SectionNameHindi"].ToString()))
            //    {
            //        ShowMessage("Section Name(Hindi) is already exits.", false);
            //        txtSectionNameHindi.Focus();
            //        return false;
            //    }
            //}

            return true;

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }

    private void ClearAll()
    {
        txtSectionNameHindi.Text = txtSectionName.Text = "";
        hfSectionID.Value = "0";
        txtSectionName.Focus();
       // divListSection.Style.Add("display", "");
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearAll();
        lblMsg.Text = lblMsg.CssClass = "";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int index = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
            hfSectionID.Value = ((Label)grdSection.Rows[index].FindControl("lblSectionID")).Text;
            txtSectionName.Text = ((Label)grdSection.Rows[index].FindControl("lblSectionName")).Text;
            txtSectionNameHindi.Text = ((Label)grdSection.Rows[index].FindControl("lblSectionNameHindi")).Text;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    int index = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
        //    objBudgetSection.SectionID = CommonCls.ConvertIntZero(((Label)grdSection.Rows[index].FindControl("lblSectionID")).Text);
        //    objBudgetSection.Ind = 3;//For Delete
        //    objBudgetSection.OrgID = GlobalSession.OrgID;
        //    objBudgetSection.BrID = GlobalSession.BrID;
        //    string uri = string.Format("BudgetSection/DeleteBudgetSection");
        //    DataTable dtSection = CommonCls.ApiPostDataTable(uri, objBudgetSection);
        //    if (dtSection.Rows.Count > 0)
        //    {
        //        ShowMessage("Record Delete successfully.", true);
        //        BindAll();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ShowMessage(ex.Message, false);
        //}
    }

}