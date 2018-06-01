using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GSTReturns_frmGSTR2 : System.Web.UI.UserControl
{
    #region Declaration

    public string CompanyName
    {
        set { lblCompanyName.Text = value; }
    }
    public string GSTIN
    {
        set { lblGSTIn.Text = value; }
    }
    public string GSTRMONTYear
    {
        set { lblGSTRMONTYear.Text = value; }
    }
    public DataSet VSdsGSTR2
    {
        get { return (DataSet)ViewState["VSdsGSTR2"]; }
        set { ViewState["VSdsGSTR2"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    void CreateExcel(DataSet ds, string ExcelName)
    {
        try
        {
            using (DataSet ImportDs = ds)
            {
                //Set Name of DataTables.

                using (ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook())
                {
                    foreach (DataTable dtExcel in ImportDs.Tables)
                    {
                        wb.AddWorksheet(dtExcel);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + ExcelName + ".xlsx");
                    using (System.IO.MemoryStream MyMemoryStream = new System.IO.MemoryStream())
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
            
        }
    }

    public bool LoadGSTIN(GSTINSubmitModel objplGstinSubmit)
    {
        try
        {
            string uri = string.Format("GSTINSubmit/FillGSTR2");
            DataSet dsGSTR2 = CommonCls.ApiPostDataSet(uri, objplGstinSubmit);
            //if (false)
            if (dsGSTR2.Tables.Count > 0)
            {
                

                DataTable dt3 = dsGSTR2.Tables[0];
                DataTable dt4A = dsGSTR2.Tables[1];
                DataTable dt4B = dsGSTR2.Tables[2];
                DataTable dt4C = dsGSTR2.Tables[3];
                DataTable dt5A = dsGSTR2.Tables[4];
                DataTable dt5B = dsGSTR2.Tables[5];
                DataTable dt7 = dsGSTR2.Tables[6];
                DataTable dt10 = dsGSTR2.Tables[7];
                DataTable dt13 = dsGSTR2.Tables[8];

                #region Record - 3
                if (dt3.Rows.Count > 0)
                {
                    GrdTable3.DataSource = dt3;
                    GrdTable3.DataBind();
                }
                #endregion

                #region Record - 4 A
                if (dt4A.Rows.Count > 0)
                {
                    Grd4A.DataSource = dt4A;
                    Grd4A.DataBind();
                }
                #endregion

                #region Record - 4 B
                if (dt4B.Rows.Count > 0)
                {
                    Grd4B.DataSource = dt4B;
                    Grd4B.DataBind();
                }
                #endregion

                #region Record - 4 C
                if (dt4C.Rows.Count > 0)
                {
                    grd4C.DataSource = dt4C;
                    grd4C.DataBind();
                }
                #endregion

                #region Record - 5 A
                if (dt5A.Rows.Count > 0)
                {
                    grd5A.DataSource = dt5A;
                    grd5A.DataBind();
                }
                #endregion

                #region Record - 5 B
                if (dt5B.Rows.Count > 0)
                {
                    grd5B.DataSource = dt5B;
                    grd5B.DataBind();
                }
                #endregion
                
                #region Record - 7 A
                if (dt7.Rows.Count > 0)
                {
                    grd7A.DataSource = dt7;
                    grd7A.DataBind();

                    //// ---------- Record 72 ---------- \\
                    //DataView Dv72 = new DataView(dt4);
                    //Dv72.RowFilter = "RecordID=72";
                    //grd7B.DataSource = Dv72;
                    //grd7B.DataBind();
                }
                #endregion
                
                #region Record - 10 A 1
                if (dt10.Rows.Count > 0)
                {
                    grd10A1.DataSource = dt10;
                    grd10A1.DataBind();
                }
                #endregion

                #region Record - 13
                if (dt13.Rows.Count > 0)
                {
                    grd13.DataSource = dt13;
                    grd13.DataBind();
                }
                #endregion

                #region Table 3 Record - 6 Commented
                //if (dsGSTR2.Tables[2].Rows.Count > 0)
                //{
                //    DataTable dt3 = dsGSTR2.Tables[2];

                //    // ---------- Record 61 ---------- \\
                //    DataView Dv61 = new DataView(dt3);
                //    Dv61.RowFilter = "RecordID=61";
                //    grd6A.DataSource = Dv61;
                //    grd6A.DataBind();

                //    // ---------- Record 62 ---------- \\
                //    DataView Dv62 = new DataView(dt3);
                //    Dv62.RowFilter = "RecordID=62";
                //    grd6B.DataSource = Dv62;
                //    grd6B.DataBind();

                //    // ---------- Record 63 ---------- \\
                //    DataView Dv63 = new DataView(dt3);
                //    Dv63.RowFilter = "RecordID=63";
                //    grd6C.DataSource = Dv63;
                //    grd6C.DataBind();
                //}
                #endregion
                
                #region Table 5 Record - 8 Commented
                //if (dsGSTR2.Tables[4].Rows.Count > 0)
                //{
                //    DataTable dt5 = dsGSTR2.Tables[4];
                //    grd8.DataSource = dt5;
                //    grd8.DataBind();
                //}
                #endregion

                #region Table 6 Record - 11 Commented
                //if (dsGSTR2.Tables[5].Rows.Count > 0)
                //{
                //    DataTable dt4 = dsGSTR2.Tables[5];

                //    // ---------- Record 11 A1 ---------- \\
                //    DataView Dv11A = new DataView(dt4);
                //    Dv11A.RowFilter = "RecordID=111";
                //    grd11.DataSource = Dv11A;
                //    grd11.DataBind();


                //    // ---------- Record 11 A2 ---------- \\
                //    DataView Dv11A2 = new DataView(dt4);
                //    Dv11A2.RowFilter = "RecordID=112";
                //    grd11Amd.DataSource = Dv11A2;
                //    grd11Amd.DataBind();
                //}
                #endregion

                #region Table 7 Record - 12 Commented
                //if (dsGSTR2.Tables[6].Rows.Count > 0)
                //{
                //    DataTable dt12 = dsGSTR2.Tables[6];
                //    grd12.DataSource = dt12;
                //    grd12.DataBind();
                //}
                #endregion
                
                #region Table 9 Record - 9 Commented
                //if (dsGSTR2.Tables[8].Rows.Count > 0)
                //{
                //    DataTable dtSummary = dsGSTR2.Tables[8];
                //    grd9.DataSource = dtSummary;
                //    grd9.DataBind();
                //}
                #endregion

                pnlGSTR2.Visible = true;

                //CreateExcel(dsGSTR2, lblGSTRMONTYear.Text);
            }
            else
            {
                return false;
                //ShowMessageOnPopUp("Internal Server Errror Press Yes For Going to Home Page.", false, "../Defaults/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            return false;
            //ShowMessageOnPopUp("Internal Server Errror Press Yes For Going to Home Page.", false, "../Defaults/Default.aspx");
        }
        return true;
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = grd9;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlGSTR2.Visible = false;
    }
}