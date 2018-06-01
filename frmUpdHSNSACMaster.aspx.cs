using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUpdHSNSACMaster : System.Web.UI.Page
{
    HSNSACModel objHSNSACModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

        lblMsg.CssClass = "";
        lblMsg.Text = "";
    }

    protected void ddlGoodsAndServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGoodsAndServices.SelectedValue != "0")
        {
            txtGoodsAndServicesCode.Text = txtItemDescription.Text = txtTaxRate.Text = txtIGST.Text = txtCGST.Text = txtSGST.Text = "";

            if (ddlGoodsAndServices.SelectedValue == "1")
            {
                txtGoodsAndServicesCode.MaxLength = 8;
                lblGoodsAndServicesCode.Text = "Goods Code";
                txtGoodsAndServicesCode.Attributes.Add("placeholder", "Goods Code");
            }
            else
            {
                txtGoodsAndServicesCode.MaxLength = 6;
                lblGoodsAndServicesCode.Text = "Services Code";
                txtGoodsAndServicesCode.Attributes.Add("placeholder", "Services Code");
            }

            txtGoodsAndServicesCode.Focus();
        }
    }

    protected void lnkGoodsAndServicesCodeSearch_Click(object sender, EventArgs e)
    {
        if (ddlGoodsAndServices.SelectedValue == "0")
        {
            ddlGoodsAndServices.Focus();
            ShowMessage("Select HSN / SAC.", false);
            return;
        }
        if (string.IsNullOrEmpty(txtGoodsAndServicesCode.Text))
        {
            txtGoodsAndServicesCode.Focus();
            ShowMessage("Enter HSN / SAC Code.", false);
            return;
        }

        objHSNSACModel = new HSNSACModel();
        objHSNSACModel.Ind = 1;

        if (ddlGoodsAndServices.SelectedValue == "1")
        {
            objHSNSACModel.GoodsServiceInd = Convert.ToInt16(ddlGoodsAndServices.SelectedValue);
            objHSNSACModel.HSNSACCode = txtGoodsAndServicesCode.Text;

            //string hsnCode = "";
            //if (txtGoodsAndServicesCode.Text.Length == 2)
            //{
            //    hsnCode = txtGoodsAndServicesCode.Text + "000000";
            //}
            //else if (txtGoodsAndServicesCode.Text.Length == 4)
            //{
            //    hsnCode = txtGoodsAndServicesCode.Text + "0000";
            //}
            //else if (txtGoodsAndServicesCode.Text.Length == 8)
            //{
            //    hsnCode = txtGoodsAndServicesCode.Text;
            //}
            //else
            //{
            //    txtGoodsAndServicesCode.Focus();
            //    ShowMessage("Invalid HSN Code", false);
            //    return;
            //}

            //objHSNSACModel.GoodsServiceInd = Convert.ToInt16(ddlGoodsAndServices.SelectedValue);

            //if (!string.IsNullOrEmpty(hsnCode))
            //{
            //    objHSNSACModel.HSNSACCode = hsnCode;
            //}
        }
        else if (ddlGoodsAndServices.SelectedValue == "2")
        {
            objHSNSACModel.GoodsServiceInd = Convert.ToInt16(ddlGoodsAndServices.SelectedValue);
            objHSNSACModel.HSNSACCode = txtGoodsAndServicesCode.Text;

            //string sacCode = "";
            //if (txtGoodsAndServicesCode.Text.Length == 2)
            //{
            //    sacCode = txtGoodsAndServicesCode.Text + "0000";
            //}
            //else if (txtGoodsAndServicesCode.Text.Length == 4)
            //{
            //    sacCode = txtGoodsAndServicesCode.Text + "00";
            //}
            //else if (txtGoodsAndServicesCode.Text.Length == 6)
            //{
            //    sacCode = txtGoodsAndServicesCode.Text;
            //}
            //else
            //{
            //    txtGoodsAndServicesCode.Focus();
            //    ShowMessage("Invalid Sac Code", false);
            //    return;
            //}

            //if (!string.IsNullOrEmpty(sacCode))
            //{
            //    objHSNSACModel.GoodsServiceInd = Convert.ToInt16(ddlGoodsAndServices.SelectedValue);
            //    objHSNSACModel.HSNSACCode = sacCode;
            //}
        }

        string uri = string.Format("HSNSAC/LoadHSNSACInfo");
        DataTable dtHSNSACSearch = CommonCls.ApiPostDataTable(uri, objHSNSACModel);
        if (dtHSNSACSearch.Rows.Count > 0)
        {
            txtItemDescription.Text = dtHSNSACSearch.Rows[0]["HSNSACDesc"].ToString();
            txtTaxRate.Text = Convert.ToDecimal(dtHSNSACSearch.Rows[0]["TaxRate"].ToString()).ToString("0.00");
            txtIGST.Text = Convert.ToDecimal(dtHSNSACSearch.Rows[0]["IGSTRate"].ToString()).ToString("0.00");
            txtCGST.Text = Convert.ToDecimal(dtHSNSACSearch.Rows[0]["CGSTRate"].ToString()).ToString("0.00");
            txtSGST.Text = Convert.ToDecimal(dtHSNSACSearch.Rows[0]["SGSTRate"].ToString()).ToString("0.00");
            btnUpdate.Enabled = true;
            lnkGoodsAndServicesCodeSearch.Enabled = ddlGoodsAndServices.Enabled = txtGoodsAndServicesCode.Enabled = false;
        }
        else
        {
            if (ddlGoodsAndServices.SelectedValue == "1")
            {
                txtGoodsAndServicesCode.Focus();
                lblMsg.Text = "Invalid HSN Code, Record Not Found!";
                ShowMessage(lblMsg.Text, false);
            }
            else
            {
                txtGoodsAndServicesCode.Focus();
                lblMsg.Text = "Invalid SAC Code, Record Not Found!";
                ShowMessage(lblMsg.Text, false);
            }
        }
    }

    protected void txtTaxRate_TextChanged(object sender, EventArgs e)
    {
        Calculation(string.IsNullOrEmpty(txtTaxRate.Text) ? 0 : Convert.ToDecimal(txtTaxRate.Text));
    }

    void Calculation(decimal taxRate)
    {
        //decimal txRate = taxRate;
        txtTaxRate.Text = txtIGST.Text = taxRate.ToString("0.00");
        decimal divTaxRate = taxRate / 2; 
        txtCGST.Text = divTaxRate.ToString("0.00");
        txtSGST.Text = divTaxRate.ToString("0.00");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return; 
        }

        if (!ValidationBtnUpdate())
        {
            return;
        }

        objHSNSACModel = new HSNSACModel();
        objHSNSACModel.Ind = 2;
        objHSNSACModel.GoodsServiceInd = Convert.ToInt16(ddlGoodsAndServices.SelectedValue);
        objHSNSACModel.HSNSACCode = txtGoodsAndServicesCode.Text;
        objHSNSACModel.HSNSACDesc = txtItemDescription.Text;
        objHSNSACModel.TaxRate = Convert.ToDecimal(txtTaxRate.Text);
        objHSNSACModel.IGRate = Convert.ToDecimal(txtIGST.Text);
        objHSNSACModel.CGRate = Convert.ToDecimal(txtCGST.Text);
        objHSNSACModel.SGRate = Convert.ToDecimal(txtSGST.Text);

        string uri = string.Format("HSNSAC/UpdateHSNSACInfo");
        //DataTable dtUpdateHSNSAC = CommonCls.ApiPostDataTable(uri, objHSNSACModel);
        //if (dtUpdateHSNSAC.Rows.Count > 0)
        //{
        //    if (dtUpdateHSNSAC.Rows[0]["ReturnInd"].ToString() == "1")
        //    {
        //            ddlGoodsAndServices.Focus();
        //            ClearAll();
        //            lblMsg.Text = "Record Update Successfully.";
        //            ShowMessage(lblMsg.Text, false);
        //    }
        //}
        //else
        //{
        //    lblMsg.Text = "Record Not Update, Please Try Again!";
        //    ShowMessage(lblMsg.Text, false);
        //}
    }

    bool ValidationBtnUpdate()
    {
        if (ddlGoodsAndServices.SelectedValue == "0")
        {
            ddlGoodsAndServices.Focus();
            ShowMessage("Select HSN / SAC.", false);
            btnUpdate.Enabled = false;
            return false;
        }
        if (string.IsNullOrEmpty(txtGoodsAndServicesCode.Text))
        {
            txtGoodsAndServicesCode.Focus();
            ShowMessage("Enter HSN / SAC Code.", false);
            btnUpdate.Enabled = false;
            return false;
        }
        if (string.IsNullOrEmpty(txtItemDescription.Text))
        {
            txtItemDescription.Focus();
            ShowMessage("Enter Item Description.", false);
            return false;
        }
        if (string.IsNullOrEmpty(txtTaxRate.Text))
        {
            txtTaxRate.Focus();
            ShowMessage("Enter Rate.", false);
            return false;
        }

        return true;
    }

    void ClearAll()
    {
        ddlGoodsAndServices.ClearSelection();
        txtGoodsAndServicesCode.Text = txtItemDescription.Text = txtTaxRate.Text = txtIGST.Text = txtCGST.Text = txtSGST.Text = "";
        btnUpdate.Enabled = false;
        lnkGoodsAndServicesCodeSearch.Enabled = ddlGoodsAndServices.Enabled = txtGoodsAndServicesCode.Enabled = true;
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
}