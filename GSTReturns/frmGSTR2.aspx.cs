using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmGSTR2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadGSTIN();
    }

    void LoadGSTIN()
    {
        try
        {
            GSTINSubmitModel objplGstinSubmit = new GSTINSubmitModel();
            string uri = string.Format("GSTINSubmit/FillGSTR1");
            DataSet dsGSTR1 = CommonCls.ApiPostDataSet(uri, objplGstinSubmit);
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
                    Grd4A.DataSource = Dv41;
                    Grd4A.DataBind();

                    // ---------- Record 42 ---------- \\
                    DataView Dv42 = new DataView(dt1);
                    Dv42.RowFilter = "RecordNo=42";
                    Grd4B.DataSource = Dv42;
                    Grd4B.DataBind();

                    // ---------- Record 43 ---------- \\
                    DataView Dv43 = new DataView(dt1);
                    Dv43.RowFilter = "RecordNo=43";
                    grd4C.DataSource = Dv43;
                    grd4C.DataBind();
                }
                #endregion

                #region Table 2 Record - 5
                if (dsGSTR1.Tables[1].Rows.Count > 0)
                {
                    DataTable dt2 = dsGSTR1.Tables[1];

                    // ---------- Record 51 ---------- \\
                    DataView Dv51 = new DataView(dt2);
                    Dv51.RowFilter = "RecordNo=51";
                    grd5A.DataSource = Dv51;
                    grd5A.DataBind();

                    // ---------- Record 52 ---------- \\
                    DataView Dv52 = new DataView(dt2);
                    Dv52.RowFilter = "RecordNo=52";
                    grd5B.DataSource = Dv52;
                    grd5B.DataBind();
                }
                #endregion

                #region Table 3 Record - 6
                if (dsGSTR1.Tables[2].Rows.Count > 0)
                {
                    DataTable dt3 = dsGSTR1.Tables[2];

                    // ---------- Record 61 ---------- \\
                    DataView Dv61 = new DataView(dt3);
                    Dv61.RowFilter = "RecordNo=61";
                    grd6A.DataSource = Dv61;
                    grd6A.DataBind();

                    // ---------- Record 62 ---------- \\
                    DataView Dv62 = new DataView(dt3);
                    Dv62.RowFilter = "RecordNo=62";
                    grd6B.DataSource = Dv62;
                    grd6B.DataBind();

                    // ---------- Record 63 ---------- \\
                    DataView Dv63 = new DataView(dt3);
                    Dv63.RowFilter = "RecordNo=63";
                    grd6C.DataSource = Dv63;
                    grd6C.DataBind();
                }
                #endregion

                #region Table 4 Record - 7
                if (dsGSTR1.Tables[3].Rows.Count > 0)
                {
                    DataTable dt4 = dsGSTR1.Tables[3];

                    // ---------- Record 71 ---------- \\
                    DataView Dv71 = new DataView(dt4);
                    Dv71.RowFilter = "RecordNo=71";
                    grd7A.DataSource = Dv71;
                    grd7A.DataBind();

                    // ---------- Record 72 ---------- \\
                    DataView Dv72 = new DataView(dt4);
                    Dv72.RowFilter = "RecordNo=72";
                    grd7B.DataSource = Dv72;
                    grd7B.DataBind();
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
                    grd11.DataSource = Dv11A;
                    grd11.DataBind();


                    // ---------- Record 11 A2 ---------- \\
                    DataView Dv11A2 = new DataView(dt4);
                    Dv11A2.RowFilter = "RecordNo=112";
                    grd11Amd.DataSource = Dv11A2;
                    grd11Amd.DataBind();
                }
                #endregion

                #region Table 7 Record - 12
                if (dsGSTR1.Tables[6].Rows.Count > 0)
                {
                    DataTable dt12 = dsGSTR1.Tables[6];
                    grd12.DataSource = dt12;
                    grd12.DataBind();
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

                #region Table 9 Record - 9
                if (dsGSTR1.Tables[8].Rows.Count > 0)
                {
                    DataTable dtSummary = dsGSTR1.Tables[8];
                    grd9.DataSource = dtSummary;
                    grd9.DataBind();
                }
                #endregion
            }
            else
            {
                ShowMessageOnPopUp("Internal Server Errror Press Yes For Going to Home Page.", false, "../Defaults/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            ShowMessageOnPopUp("Internal Server Errror Press Yes For Going to Home Page.", false, "../Defaults/Default.aspx");
        }
    }
    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = grd9;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }
}