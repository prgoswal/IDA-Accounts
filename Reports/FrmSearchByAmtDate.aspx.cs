using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Reports_FrmSearchByAmtDate : System.Web.UI.Page
{
    Pl_FrmSearchByAmtDate plobjFrmSearchByAmtDate = new Pl_FrmSearchByAmtDate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = GlobalSession.YrStartDate;
            txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
            txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        }
    }

    void LoadItemMasterDDL()
    {
        try
        {
            plobjFrmSearchByAmtDate.Ind = 1;
            plobjFrmSearchByAmtDate.OrgID = GlobalSession.OrgID;
            plobjFrmSearchByAmtDate.BrID = GlobalSession.BrID;
            plobjFrmSearchByAmtDate.YrCD = GlobalSession.YrCD;
            plobjFrmSearchByAmtDate.DocNo = "0";



            string uri = string.Format("FrmSearchByAmtDate/Method_FrmSearchByAmtDate");
            DataTable dtFrmSearchByAmtDate = CommonCls.ApiPostDataTable(uri, plobjFrmSearchByAmtDate);
            if (dtFrmSearchByAmtDate.Rows.Count > 0)
            {
                gvItem.DataSource = dtFrmSearchByAmtDate;
                gvItem.DataBind();
                lblTotalAmt.Text = "Total Search No Of Recored : " + dtFrmSearchByAmtDate.Rows.Count.ToString();
                Disable();
            }
            else
            {
                lblErrorMsg.Text = "Recored Not Found..!!! ";
                lblDiv.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void Disable()
    {
        btnSearch.Enabled = false;
        pnlGrid.Visible = true;
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        txtmaxAmount.Enabled = false;
        txtMinAmount.Enabled = false;
    }

    string a = "1";
    string b = "1";
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        lblDiv.Visible = false;
        gvItem.DataSource = null;
        gvItem.DataBind();
        pnlGrid.Visible = false;

        if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "" && txtMinAmount.Text.Trim() == "" && txtmaxAmount.Text.Trim() == "")
        {
            lblErrorMsg.Text = "Please Enter Date Or Amount..!!! ";
            lblDiv.Visible = true;
            return;
        }
        if (txtFromDate.Text.Trim() == "" && txtToDate.Text.Trim() == "")
        {
            a = "0";
        }

        if (txtMinAmount.Text.Trim() == "" && txtmaxAmount.Text.Trim() == "")
        {
            b = "0";
        }

        if (a == "1" || b == "1")
        {
            if (a == "1")
            {
                if (txtFromDate.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Please Enter From Date..!!! ";
                    txtFromDate.Focus();
                    lblDiv.Visible = true;
                    return;

                }
                if (txtToDate.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Please Enter To Date..!!! ";
                    txtToDate.Focus();
                    lblDiv.Visible = true;
                    return;
                }

                DateTime F_Date = Convert.ToDateTime(txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2));
                DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));
                if (F_Date > T_Date)
                {
                    lblErrorMsg.Text = "From Date Should Not Be Greater Than To Date";
                    lblDiv.Visible = true;
                    lblErrorMsg.Attributes.Add("style", "font-size: 11px;");
                    txtFromDate.Focus();
                    return;
                }
                plobjFrmSearchByAmtDate.InvoiceDateFrom = txtFromDate.Text.Substring(6, 4) + "/" + txtFromDate.Text.Substring(3, 2) + "/" + txtFromDate.Text.Substring(0, 2);
                plobjFrmSearchByAmtDate.InvoiceDateTo = txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2);
            }
            if (b == "1")
            {

                if (txtMinAmount.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Please Enter Minium Amount..!!! ";
                    txtMinAmount.Focus();
                    lblDiv.Visible = true;
                    return;
                }
                if (txtmaxAmount.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Please Enter Max Amount..!!! ";
                    txtmaxAmount.Focus();
                    lblDiv.Visible = true;
                    return;
                }

                plobjFrmSearchByAmtDate.MinAmount = Convert.ToDecimal(txtMinAmount.Text);
                plobjFrmSearchByAmtDate.MaxAmount = Convert.ToDecimal(txtmaxAmount.Text);
            }
            LoadItemMasterDDL();
        }
    }


    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        btnSearch.Enabled = true;
        pnlGrid.Visible = false;
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        txtmaxAmount.Enabled = true;
        txtMinAmount.Enabled = true;
        gvItem.DataSource = null;
        gvItem.DataBind();
        txtFromDate.Text = GlobalSession.YrStartDate;
        txtFromDate.Text = txtFromDate.Text.Replace("-", "/");
        txtToDate.Text = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        txtMinAmount.Text = "";
        txtmaxAmount.Text = "";
        lblTotalAmt.Text = "";
    }
    protected void txtMinAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtMinAmount.Text.Trim() != "")
        {
            txtmaxAmount.Text = txtMinAmount.Text;
        }
        else
        {
            txtmaxAmount.Text = "";
        }
    }
}