using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRegistration : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    SqlConnection con = new SqlConnection("Data Source=oswal.database.windows.net;Initial Catalog=GSTAccount;User ID=OswalAdmin;Password=Hexa1980");
    SqlCommand cmd; SqlDataAdapter da;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblmsg.Text = " Enter Full Name..!";
                txtName.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblmsg.Text = "Please Enter EmailId";
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtMobilNo.Text))
            {
                lblmsg.Text = "Please Enter MobileNo";
                txtMobilNo.Focus();
                return;
            }
            if (txtMobilNo.MaxLength < 10)
            {
                lblmsg.Text = "Please Enter 10 Digit Mobile No.";
                txtMobilNo.Focus();
                return;
            }
            if (txtPaymnetNo.Text.Trim() == "")
            {
                lblmsg.Text = "Please Enter Payment No.";
                txtPaymnetNo.Focus();
                return;
            }

            cmd = new SqlCommand("SPRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (rbList.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Ind", 2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Ind", 1);
            }

            cmd.Parameters.AddWithValue("@UserName", txtName.Text);
            cmd.Parameters.AddWithValue("@MobileNo", txtMobilNo.Text);
            cmd.Parameters.AddWithValue("@EmailAddr", txtEmail.Text);
            cmd.Parameters.AddWithValue("@OrderNo", txtPaymnetNo.Text);
          
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                clear();              
                lblmsg.Text = "Recored Save Succesfully...";
            }
            else
            {
                lblmsg.Text = "Recored Not Save Succesfully...";
            }
        }
        catch (Exception ex)
        {
            // lblmsg.Text = "There Was Error Please Try Again.";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        txtName.Text = "";
        txtMobilNo.Text = "";
        txtEmail.Text = "";
        txtPaymnetNo.Text = "";
    }

}