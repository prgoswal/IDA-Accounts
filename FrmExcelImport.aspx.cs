using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMasters_FrmExcelImport : System.Web.UI.Page
{
    ExcelImportModel obj;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCompanyName();
            ddlCompanyname.SelectedValue = "0";
            lblCompanyId.Text = "";
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCompanyname.SelectedValue) == 0)
        {
            lblmsg.Text = "Please Select Company Name";
        }
        else
        {
            string DelPath = null;
            string conStr = "";
            OleDbConnection connExcel = null;
            try
            {
                lblmsg.Text = "";
                if (fileExcel.PostedFile.FileName != "")
                {
                    string FileName = Path.GetFileName(fileExcel.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileExcel.PostedFile.FileName);
                    string FolderPath = "~/ExcelFile/";
                    if (!Directory.Exists(Server.MapPath("~/ExcelFile")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/ExcelFile"));
                    }
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileExcel.SaveAs(FilePath);
                    switch (Extension)
                    {
                        case ".xls": //Excel windows 97-03
                            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel windows 07
                            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }
                    conStr = String.Format(conStr, FilePath, "Yes");
                    connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dt = new DataTable();
                    cmdExcel.Connection = connExcel;
                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    if (RbtItemMaster.Checked == true)
                    {
                        cmdExcel.CommandText = "SELECT [Head Office],[Item Main Group Desc],[Item Sub Group Desc] ,[Item Minor Group Desc] ,[Item  Name],[Item Name Hindi] ,[Item Short Name] ,[Item Short Name Hindi] ,[Item Unit] ,[Goods Service Indication] ,[Item Selling Rate] ,[HSN SAC Code] ,[Item Op# balance in Qty],[Item Op# Value in INR],[Ref Item code],[Item Description]  From [ItemGroup$] where  [Head Office] IS NOT NULL";
                    }
                    else
                    {
                        cmdExcel.CommandText = "SELECT  " +
                     "   Left([Head Office],15),        " +
                     "   Left([Account GroupName],95),  " +
                     "   Left([Account Name English],45), " +
                     "   Left([Account Name Hindi],100),  " +
                     "   Left([Address],150),             " +
                     "   Left([City],30),                 " +
                     "   Left([State Name],50),           " +
                     "   [PinCode],                       " +
                     "   Left([GSTNo],15),          " +
                     "   Left([PanNo],                     10), " +
                     "   Left([MobileNo],                  10), " +
                     "   Left([Landline No],               15), " +
                     "   Left([Email],                     45), " +
                     "   Left([Contact Person],            45), " +
                     "   Left([Op#Bal Dr],                 9),  " +
                     "   Left([Op#Bal Cr],                 9),  " +
                     "   Left([Financial year],            9),  " +
                     "   [YrCode],                              " +
                     "   Left([Export Category],           15), " +
                     "   Left([ISD Applicable],            5),  " +
                     "   Left([TDS Applicable],            5),  " +
                     "   Left([RCM Applicable],            5),  " +
                     "   Left([TCS Applicable],            5),  " +
                     "   Left([MerchantID],                20), " +
                     "   Left([Bank Name],                 45), " +
                     "   Left([Branch Name],               25), " +
                     "   Left([IFSC Code],                 20), " +
                     "   Left([Bank Account No],           16), " +
                     "   Left([Old Software Account Code], 20), " +
                     "   Left([Remark],100)  " +
                     "   From [Master & Op Bal$]  where  [Head Office]  IS NOT NULL";
                    }
                    oda.SelectCommand = cmdExcel;
                    // oda = new OleDbDataAdapter("Select * FROM [Master & Op Bal$]", connExcel);
                    oda.Fill(dt);
                    connExcel.Close();
                    ExcelImportModel objpl = new ExcelImportModel();
                    //objpl.ClientCode = Convert.ToInt32(GlobalSession.UserID);
                    //objpl.ClientCode = Convert.ToInt32(ddlCompanyname.SelectedValue);
                    objpl.ClientCode = 111;
                    if (RbtItemMaster.Checked == true)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            dt.Rows.RemoveAt(0);
                        }
                        objpl.Ind = 2;
                    }
                    else
                    {
                        dt.Rows.RemoveAt(0);
                        dt.Rows.RemoveAt(0);
                        objpl.Ind = 1;
                    }
                    objpl.dt = dt;
                    DataTable dts = CommonCls.ApiPostDataTable("ExcelImport/ExcelImportData", objpl);
                    if (dts != null)
                    {
                        lblmsg.Text = "Excel File Successfully Uploaded.";
                        ddlCompanyname.SelectedValue = "0";
                        lblCompanyId.Text = "";
                    }
                    else
                    {
                        lblmsg.Text = "Invalid Excel File Format.";
                    }
                }
                else
                {
                    lblmsg.Text = "Please Select Excel File.";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg == "External table is not in the expected format." || msg == "No value given for one or more required parameters.")
                {
                    DelPath = Server.MapPath("~/ExcelFile/" + fileExcel.PostedFile.FileName);
                    connExcel.Close();
                    connExcel.Dispose();
                    if (File.Exists(DelPath))
                    {
                        File.Delete(DelPath);
                    }
                }
                lblmsg.Text = "Invalid Excel Format.";
            }
            finally
            {
                if (fileExcel.PostedFile.FileName != "")
                {
                    connExcel.Close();
                    connExcel.Dispose();
                }
            }
        }
    }

    public void FillCompanyName()
    {
        try
        {
            obj = new ExcelImportModel();
            obj.Ind = 98;
            string uri = string.Format("ExcelImport/FillcompanyNames?Ind=98");
            DataTable GroupTypeMainlist = CommonCls.ApiGetDataTable(uri);
            if (GroupTypeMainlist.Rows.Count > 0)
            {
                DataView dv = new DataView(GroupTypeMainlist);
                ddlCompanyname.DataSource = dv;
                ddlCompanyname.DataTextField = "CompanyName";
                ddlCompanyname.DataValueField = "CompanyID";
                ddlCompanyname.DataBind();
                ddlCompanyname.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ddlCompanyname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var Code = ddlCompanyname.SelectedValue;
            lblCompanyId.Text = Code;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

}