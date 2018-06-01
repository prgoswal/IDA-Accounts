using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GSTReturns_frmGSTR1 : System.Web.UI.UserControl
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
    public DataSet VSdsGSTR1
    {
        get { return (DataSet)ViewState["VSdsGSTR1"]; }
        set { ViewState["VSdsGSTR1"] = value; }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    LoadGSTIN();
        //}
    }

  
    public bool LoadGSTIN(GSTINSubmitModel objplGstinSubmit)
    {
        try
        {
            string uri = string.Format("GSTINSubmit/FillGSTR1");
            DataSet dsGSTR1 = VSdsGSTR1 = CommonCls.ApiPostDataSet(uri, objplGstinSubmit);
            //if (false)
            if (dsGSTR1.Tables.Count > 0)
            {
                #region Table 1 Record - 4
                if (dsGSTR1.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = dsGSTR1.Tables[0];

                    // ---------- Record 41 ---------- \\
                    DataView Dv41 = new DataView(dt1);
                    Dv41.RowFilter = "RecordNo=41";
                    grdGSTR14A.DataSource = Dv41;
                    grdGSTR14A.DataBind();

                    // ---------- Record 42 ---------- \\
                    DataView Dv42 = new DataView(dt1);
                    Dv42.RowFilter = "RecordNo=42";
                    grdGSTR4B.DataSource = Dv42;
                    grdGSTR4B.DataBind();

                    // ---------- Record 43 ---------- \\
                    DataView Dv43 = new DataView(dt1);
                    Dv43.RowFilter = "RecordNo=43";
                    grdGSTR14C.DataSource = Dv43;
                    grdGSTR14C.DataBind();
                }
                #endregion

                #region Table 2 Record - 5
                if (dsGSTR1.Tables[1].Rows.Count > 0)
                {
                    DataTable dt2 = dsGSTR1.Tables[1];

                    // ---------- Record 51 ---------- \\
                    DataView Dv51 = new DataView(dt2);
                    Dv51.RowFilter = "RecordNo=51";
                    grdGSTR15A.DataSource = Dv51;
                    grdGSTR15A.DataBind();

                    // ---------- Record 52 ---------- \\
                    DataView Dv52 = new DataView(dt2);
                    Dv52.RowFilter = "RecordNo=52";
                    grdGSTR15B.DataSource = Dv52;
                    grdGSTR15B.DataBind();
                }
                #endregion

                #region Table 3 Record - 6
                if (dsGSTR1.Tables[2].Rows.Count > 0)
                {
                    DataTable dt3 = dsGSTR1.Tables[2];

                    // ---------- Record 61 ---------- \\
                    DataView Dv61 = new DataView(dt3);
                    Dv61.RowFilter = "RecordNo=61";
                    grdGSTR16A.DataSource = Dv61;
                    grdGSTR16A.DataBind();

                    // ---------- Record 62 ---------- \\
                    DataView Dv62 = new DataView(dt3);
                    Dv62.RowFilter = "RecordNo=62";
                    grdGSTR16B.DataSource = Dv62;
                    grdGSTR16B.DataBind();

                    // ---------- Record 63 ---------- \\
                    DataView Dv63 = new DataView(dt3);
                    Dv63.RowFilter = "RecordNo=63";
                    grdGSTR16C.DataSource = Dv63;
                    grdGSTR16C.DataBind();
                }
                #endregion

                #region Table 4 Record - 7
                if (dsGSTR1.Tables[3].Rows.Count > 0)
                {
                    DataTable dt4 = dsGSTR1.Tables[3];

                    // ---------- Record 71 ---------- \\
                    DataView Dv71 = new DataView(dt4);
                    Dv71.RowFilter = "RecordNo=71";
                    grd7A1.DataSource = Dv71;
                    grd7A1.DataBind();

                    // ---------- Record 72 ---------- \\
                    DataView Dv72 = new DataView(dt4);
                    Dv72.RowFilter = "RecordNo=72";
                    grd7A2.DataSource = Dv72;
                    grd7A2.DataBind();

                    // ---------- Record 73 ---------- \\
                    DataView Dv73 = new DataView(dt4);
                    Dv73.RowFilter = "RecordNo=73";
                    grd7B1.DataSource = Dv73;
                    grd7B1.DataBind();

                    // ---------- Record 74 ---------- \\
                    DataView Dv74 = new DataView(dt4);
                    Dv74.RowFilter = "RecordNo=74";
                    grd7B2.DataSource = Dv74;
                    grd7B2.DataBind();
                }
                #endregion

                #region Table 5 Record - 8
                if (dsGSTR1.Tables[4].Rows.Count > 0)
                {
                    DataTable dt5 = dsGSTR1.Tables[4];
                    grd8.DataSource = dt5;
                    grd8.DataBind();
                }
                #endregion

                #region Table 6 Record - 11
                if (dsGSTR1.Tables[5].Rows.Count > 0)
                {
                    DataTable dt4 = dsGSTR1.Tables[5];

                    // ---------- Record 11 A1 ---------- \\
                    DataView Dv11A = new DataView(dt4);
                    Dv11A.RowFilter = "RecordNo=111";
                    grd11A1.DataSource = Dv11A;
                    grd11A1.DataBind();


                    // ---------- Record 11 A2 ---------- \\
                    DataView Dv11A2 = new DataView(dt4);
                    Dv11A2.RowFilter = "RecordNo=112";
                    grd11A2.DataSource = Dv11A2;
                    grd11A2.DataBind();

                    // ---------- Record 11 B1 ---------- \\
                    DataView Dv11B1 = new DataView(dt4);
                    Dv11B1.RowFilter = "RecordNo=113";
                    grd11B1.DataSource = Dv11B1;
                    grd11B1.DataBind();

                    // ---------- Record 11 B2 ---------- \\
                    DataView Dv11B2 = new DataView(dt4);
                    Dv11B2.RowFilter = "RecordNo=114";
                    grd11B2.DataSource = Dv11B2;
                    grd11B2.DataBind();
                }
                #endregion

                #region Table 7 Record - 12
                if (dsGSTR1.Tables[6].Rows.Count > 0)
                {
                    DataTable dt12 = dsGSTR1.Tables[6];
                    grdHSNSumm12.DataSource = dt12;
                    grdHSNSumm12.DataBind();
                }
                #endregion

                #region Table 8 Record - 13
                if (dsGSTR1.Tables[7].Rows.Count > 0)
                {
                    DataTable dt13 = dsGSTR1.Tables[7];
                    grd13.DataSource = dt13;
                    grd13.DataBind();
                }
                #endregion

                #region Table 9 Record - Summary
                if (dsGSTR1.Tables[8].Rows.Count > 0)
                {
                    DataTable dtSummary = dsGSTR1.Tables[8];
                    GrdSummary.DataSource = dtSummary;
                    GrdSummary.DataBind();
                }
                #endregion

                pnlGSTR1.Visible = true;
            }
            else
            {
                pnlGSTR1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            
           // throw;
        }
        return pnlGSTR1.Visible;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        pnlGSTR1.Visible = false;
    }

}

//public void ExportTableData(DataSet dsdata)
//{
//    //////////////////////EXPORT_TO_EXCEL//////////////
//    try
//    {
//        string attach = "attachment;filename=MemberList.xls";
//        Response.ClearContent();
//        Response.AddHeader("content-disposition", attach);
//        Response.ContentType = "application/ms-excel";
//        //string headerTable = @"<Table><tr><td>Report Header</td></tr><tr><td>header</td></tr></Table>";
//        //Response.Write(headerTable);

//        if (dsdata != null)
//        {
//            Response.Write("Member List \t");
//            Response.Write("Date : " + DateTime.Now.ToShortDateString() + "\t");
//            Response.Write(System.Environment.NewLine);

//            foreach (DataTable item in dsdata.Tables)
//            {
//                foreach (DataColumn dc in item.Columns)
//                {
//                    Response.Write(dc.ColumnName + "\t");
//                }
//                Response.Write(System.Environment.NewLine);
//                foreach (DataRow dr in item.Rows)
//                {
//                    for (int i = 0; i < item.Columns.Count; i++)
//                    {
//                        Response.Write(dr[i].ToString() + "\t");
//                    }
//                    Response.Write("\n");
//                }
//            }
//            Response.End();
//        }

//    }
//    catch (Exception ex)
//    {
//        //lblErrorMsg.Text = ex.Message;
//    }
//}