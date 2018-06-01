using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmGstinsubmit : System.Web.UI.Page
{
    GSTINSubmitModel objplGstinSubmit;
    public DataSet VSDsGstinInfo
    {
        get { return (DataSet)ViewState["GstinSubmitInfo"]; }
        set { ViewState["GstinSubmitInfo"] = value; }
    }
        
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.CssClass = "";
        if (!IsPostBack)
        {
            lblCompanyName.Text = GlobalSession.OrgName;
            if (Session["GSTIN"] != null)
            {
                lblGstin.Text = Session["GSTIN"].ToString();
                lblAddress.Text = Session["Address"].ToString();

                btnSubmit.Enabled = btnGST2Submit.Enabled = (CommonCls.ConvertIntZero(Session["ASPClientCode"]) != 0); // This Return either true or false;
            }
        }
        fillGrid();
    }

    #region GSTR1 Operations

    void FilGSTR1()
    {
        objplGstinSubmit = new GSTINSubmitModel();
        objplGstinSubmit.ind = 1;
        objplGstinSubmit.OrgID = GlobalSession.OrgID;
        objplGstinSubmit.BrID = GlobalSession.BrID;
        objplGstinSubmit.GSTIN = lblGstin.Text;//"23BCHPJ4094K1ZT";
        objplGstinSubmit.YrCD = GlobalSession.YrCD;
        objplGstinSubmit.TaxMonth = CommonCls.ConvertIntZero(ddlMonth.SelectedValue);
        objplGstinSubmit.TaxYear = CommonCls.ConvertIntZero(ddlYear.SelectedItem.Text);

        frmGSTR1.CompanyName = lblCompanyName.Text;
        frmGSTR1.GSTIN = lblGstin.Text;
        frmGSTR1.GSTRMONTYear = "GSTR-1 For Month " + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text;
        bool DataStatus = frmGSTR1.LoadGSTIN(objplGstinSubmit);
        if (!DataStatus)
        {
            ShowMessage("No Data Found.", false);
        }
        //object sender = btnView;
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { ShowModalGSTR1(); });", true);        
    }

    bool fillGrid()
    {
        objplGstinSubmit = new GSTINSubmitModel()
        {
            ind = 2,
            OrgID = GlobalSession.OrgID,//11,
            BrID = GlobalSession.BrID,//16,
            YrCD = GlobalSession.YrCD,
            GSTIN = lblGstin.Text, // "23AAGFN6111P1Z6",//lblGstin.Text,
            TaxYear = 2017,
            TaxMonth = 7,

        };


        string uri = string.Format("GSTINSubmit/FillGridGstin");
        VSDsGstinInfo = CommonCls.ApiPostDataSet(uri, objplGstinSubmit);
        if (VSDsGstinInfo.Tables.Count > 0)
        {
            if (VSDsGstinInfo.Tables[0].Rows.Count > 0)
            {
                //gridGstinSubmit.DataSource = VSDsGstinInfo.Tables[0];
                //gridGstinSubmit.DataBind();

                //ShowingGroupingDataInGridView(gridGstinSubmit.Rows, 1, 2);
                //return;

                if (VSDsGstinInfo.Tables.Count == 3)
                {
                    VSDsGstinInfo.Tables[2].TableName = "Table3";
                    VSDsGstinInfo.Tables[1].TableName = "Table2";
                    VSDsGstinInfo.Tables[0].TableName = "Table1";

                    frmGSTR1.Visible = btnSubmit.Enabled = true;
                }
                else
                {

                    ShowMessage("Only - " + VSDsGstinInfo.Tables.Count + " Are Available.", false);
                    frmGSTR1.Visible = btnSubmit.Enabled = false;
                    return false;
                }
            }
            else
            {
                ShowMessage("No Records Found.", false);
                return false;
            }
        }
        else
        {
            ShowMessage("No Record Found.", false);
            return false;
        }
        return true;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (CommonCls.ConvertIntZero(ddlMonth.SelectedValue) == 0)
        {
            ShowMessage("Select Month.", false);
            return;
        }
        //fillGrid();
        FilGSTR1();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        try
        {
            if (CommonCls.ConvertIntZero(ddlMonth.SelectedValue) == 0)
            {
                ShowMessage("Select Month.", false);
                return;
            }
            if (Session["ASPClientCode"] == null)
            {
                CommonCls.ShowModal("Session Expired Please Relogin", false, "../Logout.aspx", "Login");
                return;
            }

            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if(!fillGrid())
            {
                return;
            }

            if (VSDsGstinInfo.Tables.Count != 3)
            {
                ShowMessage("Only - " + VSDsGstinInfo.Tables.Count + " Are Available.", false);
                return;
            }          

            objplGstinSubmit = new GSTINSubmitModel();
            objplGstinSubmit.ind = 1;
            objplGstinSubmit.ClientCode = CommonCls.ConvertIntZero(Session["ASPClientCode"]);//13;//GlobalSession.OrgID;//43;
            objplGstinSubmit.ClientCodeOdp = CommonCls.ConvertIntZero(Session["ASPClientCodeODP"]);//112;//GlobalSession.ClientCodeOdp;// 135;
            objplGstinSubmit.CACode = CommonCls.ConvertIntZero(Session["ASPCACode"]); //68;
            objplGstinSubmit.CaCodeOdp = CommonCls.ConvertIntZero(Session["ASPCACodeODP"]); //120;
            objplGstinSubmit.YrCode = 2000 + GlobalSession.YrCD;
            objplGstinSubmit.MonthCD = Convert.ToInt16(ddlMonth.SelectedValue);
            objplGstinSubmit.ClientGSTNNO = lblGstin.Text;// "23AAGFN6111P1Z6";
            objplGstinSubmit.ExcelNo = Convert.ToInt32(objplGstinSubmit.MonthCD.ToString() + GlobalSession.YrCD.ToString() + GlobalSession.OrgID.ToString() + "01");
            objplGstinSubmit.UserCode = 0;
            objplGstinSubmit.ds = VSDsGstinInfo;

            System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] = "http://oswalgstfilingapi.azurewebsites.net/api/";//"http://localhost:55164/api/";
            string uri = string.Format("ImportTaxPayerData/ImportData");
            DataTable dt = CommonCls.ApiPostDataTable(uri, objplGstinSubmit);
            System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] = BaseUrl;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["res"].ToString() == "0")
                {
                    ShowMessage(dt.Rows[0]["ErrorMessage"].ToString(), false);
                }
                else if (dt.Rows[0]["res"].ToString() == "1")
                {
                    ShowMessage("Data Submited!", true);
                }
                else if (dt.Rows[0]["res"].ToString() == "2")
                {

                    gvErrorValidity.DataSource = dt;
                    gvErrorValidity.DataBind();
                    ShowingGroupingDataInGridView(gvErrorValidity.Rows, 0, 2);  

                    ShowMessage("Error Validity.", false);
                    pnlErrorValidity.Visible = true;
                }
                else
                {
                    ShowMessage("Data Not Submited. Please Try Again!", false);
                }
            }
            else
            {
                ShowMessage("Internal Server Error!", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        finally
        {
            System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] = BaseUrl;//"http://localhost:58800/api/";
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (CommonCls.ConvertIntZero(ddlMonth.SelectedValue) == 0)
        {
            ShowMessage("Select Month.", false);
            return;
        }

        objplGstinSubmit = new GSTINSubmitModel();
        objplGstinSubmit.ind = 1;
        objplGstinSubmit.OrgID = GlobalSession.OrgID;
        objplGstinSubmit.BrID = GlobalSession.BrID;
        objplGstinSubmit.GSTIN = lblGstin.Text;//"23BCHPJ4094K1ZT";//
        objplGstinSubmit.YrCD = GlobalSession.YrCD;
        objplGstinSubmit.TaxMonth = CommonCls.ConvertIntZero(ddlMonth.SelectedValue);
        objplGstinSubmit.TaxYear = CommonCls.ConvertIntZero(ddlYear.SelectedItem.Text);

        string uri = string.Format("GSTINSubmit/GetExcelData");
        DataSet dsGSTR1 = CommonCls.ApiPostDataSet(uri, objplGstinSubmit);
        if (dsGSTR1.Tables.Count > 0)
        {
            dsGSTR1.Tables[0].TableName = "b2b";    //4
            dsGSTR1.Tables[1].TableName = "b2cl";   //5
            dsGSTR1.Tables[2].TableName = "b2cs";   //7
            dsGSTR1.Tables[3].TableName = "cdnr";   //9B
            dsGSTR1.Tables[4].TableName = "cdnur";  //9B
            dsGSTR1.Tables[5].TableName = "exp";    //6
            dsGSTR1.Tables[6].TableName = "at";   //11B
            dsGSTR1.Tables[7].TableName = "atadj";   //11B
            dsGSTR1.Tables[8].TableName = "exemp";  //8
            dsGSTR1.Tables[9].TableName = "hsn";    //12
            dsGSTR1.Tables[10].TableName = "docs";   //13
            CreateExcel(dsGSTR1, objplGstinSubmit.GSTIN + "(" + ddlMonth.SelectedValue + "-" + ddlYear.SelectedItem.Text + ")");
        }
        else
        {
            ShowMessage("Internal Server Error.", false);
        }
    }

    void CreateExcel(DataSet ds, string ExcelName)
    {
        try
        {
            using (DataSet ImportDs = ds)
            {
                //Set Name of DataTables.

                using (XLWorkbook wb = new XLWorkbook())
                {
                    CreateXlWorkBook(ImportDs, wb);

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + ExcelName + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Flush();
            //Response.End();
            ShowMessage(ex.Message, false);
        }
    }

    XLWorkbook CreateXlWorkBook(DataSet ImportDs, XLWorkbook wb)
    {
        try
        {
            wb.Worksheets.Add("HELP");
            foreach (DataTable dtDataExcel in ImportDs.Tables)
            {
                DataTable dt = dtDataExcel.Copy();
                if (dt.Rows.Count <= 0)
                {
                    dt.Columns.Add("No records", typeof(string));
                    dt.Rows.Add("No records");
                    throw new Exception("There Is No Records In " + dt.TableName);
                    //ShowMessage("", false);
                }
                else if (dt.Rows.Count == 1)
                {
                    dt = RemoveEmptyRows(dt);
                }
                //Add DataTable as Worksheet.

                IXLWorksheet worksheet = wb.Worksheets.Add(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                /////      Add Header        ///                    
                worksheet.Row(1).InsertRowsAbove(1);
                worksheet.Row(2).InsertRowsAbove(1);
                worksheet.Row(3).InsertRowsAbove(1);

                int UsedCol = worksheet.ColumnsUsed().Count();
                // worksheet.Row(1).Cell(UsedCol).Hyperlink.Cell.Value = "Help";
                // worksheet.Row(1).Cell(UsedCol).Hyperlink.InternalAddress = "www.gstsaathiaccounts.in";
                // worksheet.Row(1).Cell(UsedCol).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


                // Put a Blue Color in a 2 Rows Of Cells
                foreach (var c in Enumerable.Range(1, UsedCol))
                {
                    //worksheet.Cell(2, c).Style.Border.
                    worksheet.Cell(2, c).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 112, 192);
                    worksheet.Cell(2, c).Style.Font.FontColor = XLColor.White;
                    //   worksheet.Cell(2, c).Style.Border.OutsideBorderColor = XLColor.Black;
                }

                // Put a Orange Color in a 4 Rows Of Cells Of Headers of Data
                foreach (var col in Enumerable.Range(1, UsedCol))
                {
                    worksheet.Cell(4, col).Style.Fill.BackgroundColor = XLColor.FromArgb(248, 203, 173);
                    worksheet.Cell(4, col).Style.Font.FontColor = XLColor.Black;
                }

                //  Excel Name Row 1 Cell 1;
                worksheet.Cells("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(0, 112, 192);
                worksheet.Cells("A1").Style.Font.FontColor = XLColor.White;

                #region b2b
                if (worksheet.Name == "b2b")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For B2B(4)";
                    worksheet.Cell("A2").Value = "No. of Recipients";
                    worksheet.Cell("B2").Value = "No. of Invoices";
                    worksheet.Cell("D2").Value = "Total Invoice Value";
                    worksheet.Cell("J2").Value = "Total Taxable Value";
                    worksheet.Cell("K2").Value = "Total Cess";

                    if (dt.Rows.Count > 0)
                    {
                        DataView view = new DataView(dt);

                        worksheet.Cell("A3").Value = view.ToTable(true, "GSTIN/UIN of Recipient").Rows.Count;
                        worksheet.Cell("B3").Value = view.ToTable(true, "Invoice Number").Rows.Count;
                        worksheet.Cell("D3").Value = dt.Compute("SUM([Invoice Value])", string.Empty);
                        worksheet.Cell("J3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);
                        worksheet.Cell("K3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);
                    }
                }
                #endregion

                #region b2cl
                if (worksheet.Name == "b2cl")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For B2CL(5)";
                    worksheet.Cell("A2").Value = "No. of Invoices";
                    worksheet.Cell("C2").Value = "Total Invoice Value";
                    worksheet.Cell("F2").Value = "Total Taxable Value";
                    worksheet.Cell("G2").Value = "Total Cess";


                    DataView view = new DataView(dt);
                    worksheet.Cell("A3").Value = view.ToTable(true, "Invoice Number").Rows.Count;
                    worksheet.Cell("C3").Value = dt.Compute("SUM([Invoice Value])", string.Empty);
                    worksheet.Cell("F3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);
                    worksheet.Cell("G3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);

                }
                #endregion

                #region b2cs
                if (worksheet.Name == "b2cs")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For B2CS(7)";

                    worksheet.Cell("D2").Value = "Total Taxable Value";
                    worksheet.Cell("E2").Value = "Total Cess";


                    DataView view = new DataView(dt);
                    worksheet.Cell("D3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);
                    worksheet.Cell("E3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);

                }
                #endregion

                #region cdnr
                if (worksheet.Name == "cdnr")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For CDNR(9B)";
                    worksheet.Cell("A2").Value = "No. of Recipients";
                    worksheet.Cell("B2").Value = "No. of Invoices";
                    worksheet.Cell("D2").Value = "No. of Notes/Vouchers";
                    worksheet.Cell("I2").Value = "Total Note/Refund Voucher Value";
                    worksheet.Cell("K2").Value = "Total Taxable Value";
                    worksheet.Cell("L2").Value = "Total Cess";


                    DataView view = new DataView(dt);
                    worksheet.Cell("A3").Value = view.ToTable(true, "GSTIN/UIN of Recipient").Rows.Count;
                    worksheet.Cell("B3").Value = view.ToTable(true, "Invoice/Advance Receipt Number").Rows.Count;
                    worksheet.Cell("D3").Value = view.ToTable(true, "Note/Refund Voucher Number").Rows.Count;
                    worksheet.Cell("I3").Value = dt.Compute("SUM([Note/Refund Voucher Value])", string.Empty);
                    worksheet.Cell("K3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);
                    worksheet.Cell("L3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);

                }
                #endregion

                #region cdnur
                if (worksheet.Name == "cdnur")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For CDNUR(9B)";
                    worksheet.Cell("B2").Value = "No. of Notes/Vouchers";
                    worksheet.Cell("E2").Value = "No. of Invoices";
                    worksheet.Cell("I2").Value = "Total Note Value";
                    worksheet.Cell("K2").Value = "Total Taxable Value";
                    worksheet.Cell("L2").Value = "Total Cess";


                    DataView view = new DataView(dt);
                    worksheet.Cell("B3").Value = view.ToTable(true, "Note/Refund Voucher Number").Rows.Count;
                    worksheet.Cell("E3").Value = view.ToTable(true, "Invoice/Advance Receipt Number").Rows.Count;
                    worksheet.Cell("I3").Value = dt.Compute("SUM([Note/Refund Voucher Value])", string.Empty);
                    worksheet.Cell("K3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);
                    worksheet.Cell("L3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);

                }
                #endregion

                #region exp
                if (worksheet.Name == "exp")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For EXP(6)";
                    worksheet.Cell("B2").Value = "No. of Invoices";
                    worksheet.Cell("D2").Value = "Total Note Value";
                    worksheet.Cell("F2").Value = "No. of Shipping Bill";
                    worksheet.Cell("I2").Value = "Total Taxable Value";


                    DataView view = new DataView(dt);
                    worksheet.Cell("B3").Value = view.ToTable(true, "Invoice Number").Rows.Count;
                    worksheet.Cell("D3").Value = dt.Compute("SUM([Invoice Value])", string.Empty);
                    worksheet.Cell("F3").Value = view.ToTable(true, "Shipping Bill Number").Rows.Count;
                    worksheet.Cell("I3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);

                }
                #endregion

                #region at
                if (worksheet.Name == "at")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For Advance Adjusted (11B)";
                    worksheet.Cell("C2").Value = "Total Advance Received";
                    worksheet.Cell("D2").Value = "Total Cess";

                    worksheet.Cell("C3").Value = dt.Compute("SUM([Gross Advance Received])", string.Empty);
                    worksheet.Cell("D3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);

                }
                #endregion

                #region atadj
                if (worksheet.Name == "atadj")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For Advance Adjusted (11B)";
                    worksheet.Cell("C2").Value = "Total Advance Adjusted";
                    worksheet.Cell("D2").Value = "Total Cess";

                    worksheet.Cell("C3").Value = dt.Compute("SUM([Gross Advance Adjusted])", string.Empty).ToString();
                    worksheet.Cell("D3").Value = dt.Compute("SUM([Cess Amount])", string.Empty).ToString();
                }
                #endregion

                #region exemp
                if (worksheet.Name == "exemp")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For Nil rated, exempted and non GST outward supplies (8)";

                    worksheet.Cell("B2").Value = "Total Nil Rated Supplies";
                    worksheet.Cell("C2").Value = "Total Exempted Supplies";
                    worksheet.Cell("D2").Value = "Total Non-GST Supplies";


                    DataView view = new DataView(dt);
                    worksheet.Cell("B3").Value = dt.Compute("SUM([Nil Rated Supplies])", string.Empty);
                    worksheet.Cell("C3").Value = dt.Compute("SUM([Exempted (other than nil rated/non GST supply )])", string.Empty);
                    worksheet.Cell("D3").Value = dt.Compute("SUM([Non-GST supplies])", string.Empty);

                }
                #endregion

                #region hsn
                if (worksheet.Name == "hsn")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary For HSN(12)";
                    worksheet.Cell("A2").Value = "No. of HSN";
                    worksheet.Cell("E2").Value = "Total Value";
                    worksheet.Cell("F2").Value = "Total Taxable Value";
                    worksheet.Cell("G2").Value = "Total Integrated Tax";
                    worksheet.Cell("H2").Value = "Total Central Tax";
                    worksheet.Cell("I2").Value = "Total State/UT Tax";
                    worksheet.Cell("J2").Value = "Total Cess";

                    if (dt.Rows.Count > 0)
                    {
                        DataView view = new DataView(dt);

                        worksheet.Cell("A3").Value = view.ToTable(true, "HSN").Rows.Count;
                        worksheet.Cell("E3").Value = dt.Compute("SUM([Total Value])", string.Empty);
                        worksheet.Cell("F3").Value = dt.Compute("SUM([Taxable Value])", string.Empty);
                        worksheet.Cell("G3").Value = dt.Compute("SUM([Integrated Tax Amount])", string.Empty);
                        worksheet.Cell("H3").Value = dt.Compute("SUM([Central Tax Amount])", string.Empty);
                        worksheet.Cell("I3").Value = dt.Compute("SUM([State/UT Tax Amount])", string.Empty);
                        worksheet.Cell("J3").Value = dt.Compute("SUM([Cess Amount])", string.Empty);

                        worksheet.Column("D").CellsUsed().Style.NumberFormat.Format = "0.00";
                        worksheet.Row(3).Cell("E").Style.NumberFormat.Format = "0.00";
                        worksheet.Row(3).Cell("F").Style.NumberFormat.Format = "0.00";

                        //var validE3 = worksheet.Row(3).Cell("E").DataValidation;
                        //validE3.Decimal.GetType();                    
                        //validE3.ErrorStyle = XLErrorStyle.Warning;
                        //validE3.ErrorTitle = "Number out of range";
                        //validE3.ErrorMessage = "This cell only allows the number 2.";

                        //worksheet.Cell("E3").Style.NumberFormat.Format = "0.00"; //"#,##0.00";
                        //worksheet.Cell("F3").Style.NumberFormat.Format = "0.00"; //"#,##0.00";
                        //worksheet.Range("D4", "D" + worksheet.RowsUsed().Count()).Style.NumberFormat.Format = "0.00";
                        //worksheet.CellsUsed("A").Style.NumberFormat.Format = "0.00";
                        //worksheet.Columns(4,6).CellsUsed().Style.NumberFormat.Format = "0.00";
                        }
                    }
                #endregion

                #region docs
                if (worksheet.Name == "docs")
                {
                    worksheet.Row(1).Cell(1).Value = "Summary of documents issued during the tax period (13)";
                    worksheet.Cell("D2").Value = "Total Number";
                    worksheet.Cell("E2").Value = "Total Cancelled";


                    DataView view = new DataView(dt);
                    //worksheet.Cell("D3").Value = ImportDs.Tables[22].Rows[0]["TotalNumber"].ToString();
                    //worksheet.Cell("E3").Value = ImportDs.Tables[22].Rows[0]["Cancelled"].ToString();

                    worksheet.Cell("D3").Value = dt.Compute("SUM([Total Number])", string.Empty);
                    worksheet.Cell("E3").Value = dt.Compute("SUM([Cancelled])", string.Empty);

                }
                #endregion

                }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return wb;
    }
    
    #endregion
    
    #region GSTR2 Operations

    void ViewGSTR2()
    {
        objplGstinSubmit = new GSTINSubmitModel();
        objplGstinSubmit.ind = 2;
        objplGstinSubmit.OrgID = GlobalSession.OrgID;
        objplGstinSubmit.BrID = GlobalSession.BrID;
        objplGstinSubmit.GSTIN = lblGstin.Text;//"23BCHPJ4094K1ZT";
        objplGstinSubmit.YrCD = GlobalSession.YrCD;
        objplGstinSubmit.TaxMonth = CommonCls.ConvertIntZero(ddlMonth.SelectedValue);
        objplGstinSubmit.TaxYear = CommonCls.ConvertIntZero(ddlYear.SelectedItem.Text);

        frmGSTR2.CompanyName = lblCompanyName.Text;
        frmGSTR2.GSTIN = lblGstin.Text;
        frmGSTR2.GSTRMONTYear = "GSTR-2 For Month " + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedItem.Text;
        bool DataStatus = frmGSTR2.LoadGSTIN(objplGstinSubmit);
        if (!DataStatus)
        {
            ShowMessage("No Data Found.", false);
        }
    }

    protected void btnGST2View_Click(object sender, EventArgs e)
    {
        ViewGSTR2();
    }

    bool fillGridGSTR2()
    {
        objplGstinSubmit = new GSTINSubmitModel()
        {
            ind = 2,
            OrgID = GlobalSession.OrgID,//11,
            BrID = GlobalSession.BrID,//16,
            YrCD = GlobalSession.YrCD,
            GSTIN = lblGstin.Text, // "23AAGFN6111P1Z6",//lblGstin.Text,
            TaxYear = 2017,
            TaxMonth = 7,

        };


        string uri = string.Format("GSTINSubmit/FillGridGSTR2");
        VSDsGstinInfo = CommonCls.ApiPostDataSet(uri, objplGstinSubmit);
        if (VSDsGstinInfo.Tables.Count > 0)
        {
            if (VSDsGstinInfo.Tables[0].Rows.Count > 0)
            {
                //gridGstinSubmit.DataSource = VSDsGstinInfo.Tables[0];
                //gridGstinSubmit.DataBind();

                //ShowingGroupingDataInGridView(gridGstinSubmit.Rows, 1, 2);
                //return;

                if (VSDsGstinInfo.Tables.Count == 5)
                {
                    VSDsGstinInfo.Tables[0].TableName = "dtPurchaseData";
                    VSDsGstinInfo.Tables[1].TableName = "dtISDData";
                    VSDsGstinInfo.Tables[2].TableName = "dtTDSData";
                    VSDsGstinInfo.Tables[3].TableName = "dtTable10Adv";
                    VSDsGstinInfo.Tables[4].TableName = "dtTable11";

                    DataSet DsGSTR2 = new DataSet();
                    foreach (DataTable item in VSDsGstinInfo.Tables)
                    {
                        DataTable dt = new DataTable();
                        dt = RemoveEmptyRows(item);
                        DsGSTR2.Tables.Add(dt);
                    }

                    VSDsGstinInfo = DsGSTR2;
                    
                    //RemoveEmptyRows(VSDsGstinInfo.Tables[1]);
                    //RemoveEmptyRows(VSDsGstinInfo.Tables[2]);
                    //RemoveEmptyRows(VSDsGstinInfo.Tables[3]);
                    //RemoveEmptyRows(VSDsGstinInfo.Tables[4]);

                    frmGSTR2.Visible = btnSubmit.Enabled = true;
                }
                else
                {
                    ShowMessage("Only - " + VSDsGstinInfo.Tables.Count + " Are Available.", false);
                    frmGSTR2.Visible = btnSubmit.Enabled = false;
                    return false;
                }
            }
            else
            {
                ShowMessage("No Records Found.", false);
                return false;
            }
        }
        else
        {
            ShowMessage("No Record Found.", false);
            return false;
        }
        return true;
    }

    protected void btnGST2Submit_Click(object sender, EventArgs e)
    {
        string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"];
        try
        {
            if (CommonCls.ConvertIntZero(ddlMonth.SelectedValue) == 0)
            {
                ShowMessage("Select Month.", false);
                return;
            }
            if (Session["ASPClientCode"] == null)
            {
                CommonCls.ShowModal("Session Expired Please Relogin", false, "../Logout.aspx", "Login");
                //ShowMessage("", false);
                return;
            }

            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }

            if (!fillGridGSTR2())
            {
                return;
            } 

            if (VSDsGstinInfo.Tables.Count != 5)
            {
                ShowMessage("Only - " + VSDsGstinInfo.Tables.Count + " Are Available.", false);
                return;
            }

            objplGstinSubmit = new GSTINSubmitModel();
            objplGstinSubmit.ind = 1;
            objplGstinSubmit.ClientCode = CommonCls.ConvertIntZero(Session["ASPClientCode"]);//43; //GlobalSession.OrgID;//13;// 75 -Sai Plastic 
            objplGstinSubmit.ClientCodeOdp = CommonCls.ConvertIntZero(Session["ASPClientCodeODP"]);//135; //GlobalSession.ClientCodeOdp;//112;// 167-Sai Plastic 
            objplGstinSubmit.CACode = CommonCls.ConvertIntZero(Session["ASPCACode"]);//68;
            objplGstinSubmit.CaCodeOdp = CommonCls.ConvertIntZero(Session["ASPCACodeODP"]);//120;
            objplGstinSubmit.YrCode = 2000 + GlobalSession.YrCD;
            objplGstinSubmit.MonthCD = Convert.ToInt16(ddlMonth.SelectedValue);
            objplGstinSubmit.ClientGSTNNO = lblGstin.Text;// "23AAGFN6111P1Z6";
            objplGstinSubmit.ExcelNo = Convert.ToInt32(objplGstinSubmit.MonthCD.ToString() + GlobalSession.YrCD.ToString() + GlobalSession.OrgID.ToString() + "01");
            objplGstinSubmit.UserCode = 0;
            objplGstinSubmit.ds = VSDsGstinInfo;

            System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] = "http://oswalgstfilingapi.azurewebsites.net/api/";//"http://localhost:55164/api/";
            string uri = string.Format("PurchaseImportData/PurchaseImportData");
            DataTable dt = CommonCls.ApiPostDataTable(uri, objplGstinSubmit);
            System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] = BaseUrl;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["res"].ToString() == "0")
                {
                    ShowMessage(dt.Rows[0]["ErrorMessage"].ToString(), true);
                }
                else if (dt.Rows[0]["res"].ToString() == "1")
                {
                    ShowMessage("Data Submited!", true);
                }
                else if (dt.Rows[0]["res"].ToString() == "2")
                {

                    gvErrorValidity.DataSource = dt;
                    gvErrorValidity.DataBind();
                    ShowingGroupingDataInGridView(gvErrorValidity.Rows, 0, 2);

                    ShowMessage("Error Validity.", false);
                    pnlErrorValidity.Visible = true;
                }
                else
                {
                    ShowMessage("Data Not Submited. Please Try Again!", false);
                }
            }
            else
            {
                ShowMessage("Internal Server Error!", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
        finally
        {
            System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] = BaseUrl;//"http://localhost:58800/api/";
        }
    }

    protected void btnGST2Excel_Click(object sender, EventArgs e)
    {

    }

    #endregion

    public DataTable RemoveEmptyRows(DataTable source)
    {
        DataTable dt1 = source.Clone(); //copy the structure 
        for (int i = 0; i <= source.Rows.Count - 1; i++) //iterate through the rows of the source
        {
            DataRow currentRow = source.Rows[i];  //copy the current row 
            foreach (var colValue in currentRow.ItemArray)//move along the columns 
            {
                if (!string.IsNullOrEmpty(colValue.ToString()) && CommonCls.ConvertDecimalZero(colValue) != 0) // if there is a value in a column, copy the row and finish
                {
                    dt1.ImportRow(currentRow);
                    break; //breaknd get a new row                        
                }
            }
        }
        return dt1;
    }
    protected void btnErrorClose_Click(object sender, EventArgs e)
    {
        pnlErrorValidity.Visible = false;
    }
    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        System.Collections.ArrayList lst = new System.Collections.ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }  
}
