using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmGstinDetails : System.Web.UI.Page
{
    GstinDetailModel objGstinDetail;
    public static String GSTINFORMAT_REGEX = "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}";
    public static String GSTN_CODEPOINT_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadGSTINDetail();
            }
        }
        catch (Exception ee)
        {
            ShowMessage("Error." + ee.Message, false);
        }
    }

    private void LoadGSTINDetail()
    {
        try
        {
            objGstinDetail = new GstinDetailModel()
            {
                Ind = 1,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,

            };
            string uri = string.Format("GSTINDetails/BindAll");
            DataSet dsGSTINDetail = CommonCls.ApiPostDataSet(uri, objGstinDetail);
            if (dsGSTINDetail.Tables.Count > 0)
            {
                if (dsGSTINDetail.Tables[0].Rows.Count > 0)
                {
                    grdGSTINDetail.DataSource = dsGSTINDetail.Tables[0];
                    grdGSTINDetail.DataBind();

                    for (int j = 0; j < dsGSTINDetail.Tables[0].Rows.Count; j++)
                    {
                        DropDownList ddlState = (DropDownList)grdGSTINDetail.Rows[j].FindControl("ddlState");
                        ddlState.DataSource = dsGSTINDetail.Tables[1];
                        ddlState.DataTextField = "StateName";
                        ddlState.DataValueField = "StateID";
                        ddlState.DataBind();
                        ddlState.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
                        ddlState.SelectedIndex = 0;

                        for (int i = 0; i < dsGSTINDetail.Tables[0].Rows.Count; i++)
                        {
                            if (dsGSTINDetail.Tables[0].Rows[j]["StateID"].ToString() == "0")
                            {
                                ddlState.SelectedValue = "0";
                                ddlState.Enabled = true;

                            }
                            else
                            {
                                ddlState.SelectedValue = dsGSTINDetail.Tables[0].Rows[j]["StateID"].ToString();
                                ddlState.Enabled = false;
                            }
                            Button btnGSTINStatus = (Button)grdGSTINDetail.Rows[i].FindControl("btnGSTINStatus");
                            string GSTIN = dsGSTINDetail.Tables[0].Rows[i]["GSTIN"].ToString().ToUpper();
                            string CompanyStateID = dsGSTINDetail.Tables[0].Rows[i]["PartyStateID"].ToString();
                            string PANNo = dsGSTINDetail.Tables[0].Rows[i]["PartyPanNo"].ToString();



                            if (GSTIN != "")
                            {
                                if (PANNo == "")
                                {

                                    bool GSTIN_NO = CommonCls.validGSTIN(GSTIN.ToUpper());
                                    if (GSTIN_NO == true)
                                    {
                                        btnGSTINStatus.Visible = true;
                                        btnGSTINStatus.Text = "Valid";
                                        btnGSTINStatus.CssClass = "btn btn-success";
                                    }
                                    else
                                    {
                                        btnGSTINStatus.Visible = true;
                                        btnGSTINStatus.Text = "Invalid";
                                        btnGSTINStatus.CssClass = "btn btn-danger";
                                    }

                                }
                                else
                                {
                                    //check Gstin Number by Statecode and pan no.

                                    bool GSTINStatus = CommonCls.gstinvalid(GSTIN.ToUpper(), CompanyStateID, PANNo.ToUpper());
                                    if (GSTINStatus == true)
                                    {

                                        //check Gstin Number by Valid gstin expression

                                        bool GSTIN_NO = CommonCls.validGSTIN(GSTIN.ToUpper());
                                        if (GSTIN_NO == true)
                                        {
                                            //    string firsttwo = GSTIN.ToUpper().Substring(0, 2);
                                            //    ddlState.SelectedValue = firsttwo;
                                            //    ddlState.Enabled = false;

                                            btnGSTINStatus.Visible = true;
                                            btnGSTINStatus.Text = "Valid";
                                            btnGSTINStatus.CssClass = "btn btn-success";

                                        }
                                        else
                                        {
                                            btnGSTINStatus.Visible = true;
                                            btnGSTINStatus.Text = "Invalid";
                                            btnGSTINStatus.CssClass = "btn btn-danger";
                                        }
                                    }
                                    else
                                    {
                                        btnGSTINStatus.Visible = true;
                                        btnGSTINStatus.Text = "Invalid";
                                        btnGSTINStatus.CssClass = "btn btn-danger";
                                    }
                                }

                            }
                            else
                            {
                                btnGSTINStatus.Visible = false;
                            }

                        }
                    }
                }
                else
                {

                    ShowMessage("No Record For GSTIN Number", false);
                    btnSave.Visible = false;
                }
            }

        }
        catch (Exception ee)
        {
            ShowMessage("Error." + ee.Message, false);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }
            objGstinDetail = new GstinDetailModel();
            DataTable DSGSTINDetail = new DataTable();
            DSGSTINDetail.Columns.Add("AccGSTINID", typeof(int));
            DSGSTINDetail.Columns.Add("AccCode", typeof(int));

            DSGSTINDetail.Columns.Add("GSTIN", typeof(string));
            DSGSTINDetail.Columns.Add("RegistrationAddress", typeof(string));
            DSGSTINDetail.Columns.Add("City", typeof(string));
            DSGSTINDetail.Columns.Add("StateID", typeof(int));

            for (int i = 0; i < grdGSTINDetail.Rows.Count; i++)
            {
                DropDownList ddlState = (DropDownList)grdGSTINDetail.Rows[i].FindControl("ddlState");
                Label lblAccCode = (Label)grdGSTINDetail.Rows[i].FindControl("lblAccCode");
                Label lblAccGSTINID = (Label)grdGSTINDetail.Rows[i].FindControl("lblAccGSTINID");
                Label lblCompanyStateID = (Label)grdGSTINDetail.Rows[i].FindControl("lblPartyStateID");
                Label lblPANNo = (Label)grdGSTINDetail.Rows[i].FindControl("lblPartyPanNo");



                TextBox txtGSTIN = (TextBox)grdGSTINDetail.Rows[i].FindControl("txtGSTIN");
                TextBox txtRegistrationAddress = (TextBox)grdGSTINDetail.Rows[i].FindControl("txtRegistrationAddress");
                TextBox txtCity = (TextBox)grdGSTINDetail.Rows[i].FindControl("txtCity");

                if (lblPANNo.Text.ToUpper() == "")
                {

                    bool GSTIN_NO = CommonCls.validGSTIN(txtGSTIN.Text.ToUpper());
                    if (GSTIN_NO == true)
                    {

                        DSGSTINDetail.Rows.Add(Convert.ToInt32(lblAccGSTINID.Text), Convert.ToInt32(lblAccCode.Text), txtGSTIN.Text.ToUpper(), txtRegistrationAddress.Text, txtCity.Text, ddlState.SelectedValue);

                    }

                }
                else
                {
                    bool GSTIN_NO = CommonCls.validGSTIN(txtGSTIN.Text.ToUpper());
                    if (GSTIN_NO == true)
                    {

                        bool GSTINStatus = CommonCls.gstinvalid(txtGSTIN.Text.ToUpper(), lblCompanyStateID.Text, lblPANNo.Text.ToUpper());
                        if (GSTINStatus == true)
                        {

                            DSGSTINDetail.Rows.Add(Convert.ToInt32(lblAccGSTINID.Text), Convert.ToInt32(lblAccCode.Text), txtGSTIN.Text.ToUpper(), txtRegistrationAddress.Text, txtCity.Text, ddlState.SelectedValue);
                        }
                    }
                }
            }



            objGstinDetail.Ind = 2;
            objGstinDetail.OrgID = GlobalSession.OrgID;
            objGstinDetail.BrID = GlobalSession.BrID;
            objGstinDetail.DtGstin = DSGSTINDetail;
            string uri = string.Format("GSTINDetails/SaveGSTINDetails");
            DataTable dtGstinDetail = CommonCls.ApiPostDataTable(uri, objGstinDetail);
            if (dtGstinDetail.Rows.Count > 0)
            {
                ShowMessage("This GSTIN Number's are Updated Successfully", true);
            }
            else
            {
                ShowMessage("GSTIN Number's Not Update. Try Again.", true);
            }
        }
        catch (Exception ee)
        {
            ShowMessage("Error." + ee.Message, false);
        }

    }



    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
        lblMsg.Focus();
    }


    protected void txtGSTIN_TextChanged(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < grdGSTINDetail.Rows.Count; i++)
            {
                Button btnGSTINStatus = (Button)grdGSTINDetail.Rows[i].FindControl("btnGSTINStatus");

                TextBox txtGSTIN = ((TextBox)grdGSTINDetail.Rows[i].FindControl("txtGSTIN"));
                Label lblCompanyStateID = ((Label)grdGSTINDetail.Rows[i].FindControl("lblPartyStateID"));
                Label lblPANNo = ((Label)grdGSTINDetail.Rows[i].FindControl("lblPartyPanNo"));
                DropDownList ddlState = ((DropDownList)grdGSTINDetail.Rows[i].FindControl("ddlState"));

                if (txtGSTIN.Text.ToUpper() != "")
                {

                    if (lblPANNo.Text == "")
                    {

                        bool GSTIN_NO = CommonCls.validGSTIN(txtGSTIN.Text.ToUpper());
                        if (GSTIN_NO == true)
                        {
                            string firsttwo = txtGSTIN.Text.ToUpper().Substring(0, 2);

                            ddlState.SelectedValue = firsttwo;
                            ddlState.Enabled = false;


                            btnGSTINStatus.Visible = true;
                            btnGSTINStatus.Text = "Valid";
                            btnGSTINStatus.CssClass = "btn btn-success";

                        }
                        else
                        {
                            btnGSTINStatus.Visible = true;
                            btnGSTINStatus.Text = "Invalid";
                            btnGSTINStatus.CssClass = "btn btn-danger";
                        }

                    }
                    else
                    {


                        //Check Gstin Number by valid Gstin expression.
                        bool CheckvalidGSTIN = CommonCls.gstinvalid(txtGSTIN.Text.ToUpper(), lblCompanyStateID.Text, lblPANNo.Text.ToUpper());
                        if (CheckvalidGSTIN == true)
                        {

                            //check Gstin Number by statcode and panNo.
                            bool GSTINStatus = CommonCls.validGSTIN(txtGSTIN.Text.ToUpper());
                            if (GSTINStatus == true)
                            {
                                btnGSTINStatus.Visible = true;
                                btnGSTINStatus.Text = "Valid";
                                btnGSTINStatus.CssClass = "btn btn-success";
                            }
                            else
                            {
                                btnGSTINStatus.Visible = true;
                                btnGSTINStatus.Text = "Invalid";
                                btnGSTINStatus.CssClass = "btn btn-danger";
                            }
                            //break;
                        }
                        else
                        {
                            btnGSTINStatus.Visible = true;
                            btnGSTINStatus.Text = "Invalid";
                            btnGSTINStatus.CssClass = "btn btn-danger";
                            //break;
                        }
                    }
                }
                else
                {
                    btnGSTINStatus.Visible = false;
                    ddlState.Enabled = true;
                    ddlState.ClearSelection();
                }
            }
        }
        catch (Exception ee)
        {

            ShowMessage("Error." + ee.Message, false);

        }
    }


    //DataTable DtGSTINDetail()
    //{
    //    DataTable dtGSTIN = new DataTable();
    //    dtGSTIN.Columns.Add("AccCode", typeof(int));
    //    dtGSTIN.Columns.Add("City", typeof(string));
    //    dtGSTIN.Columns.Add("GSTIN", typeof(string));
    //    dtGSTIN.Columns.Add("RegistrationAddress", typeof(string));
    //    dtGSTIN.Columns.Add("StateID", typeof(int));
    //    return dtGSTIN;
    //}

    //protected void grdGSTINDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {

    //            objGstinDetailModel = new GstinDetailModel()
    //            {
    //                Ind = 1,
    //                OrgID = GlobalSession.OrgID,
    //                BrID = GlobalSession.BrID,

    //            };
    //            string uri = string.Format("GSTINDetails/BindAll");
    //            DataSet dsGSTINDetail = CommonCls.ApiPostDataSet(uri, objGstinDetailModel);
    //            if (dsGSTINDetail.Tables.Count > 0)
    //            {
    //                DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlState");
    //                ddlState.DataSource = dsGSTINDetail.Tables[1];
    //                ddlState.DataTextField = "StateName";
    //                ddlState.DataValueField = "StateID";
    //                ddlState.DataBind();

    //                ddlState.Items.Insert(0, new ListItem { Text = "-Select-", Value = "0" });
    //                ddlState.SelectedIndex = 0;
    //                for (int i = 0; i < dsGSTINDetail.Tables[0].Rows.Count; i++)
    //                {
    //                    if (dsGSTINDetail.Tables[0].Rows[i]["StateID"].ToString() == "0")
    //                    {
    //                        ddlState.SelectedValue = "0";
    //                    }
    //                    else
    //                    {
    //                        ddlState.SelectedValue = dsGSTINDetail.Tables[0].Rows[i]["StateID"].ToString();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //protected void txtGSTIN_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        //CHECKGSTIN_VALIDATION();

    //        for (int i = 0; i < grdGSTINDetail.Rows.Count; i++)
    //        {
    //            Button btnGSTINStatus = (Button)grdGSTINDetail.Rows[i].FindControl("btnGSTINStatus");
    //            TextBox txtGSTIN = ((TextBox)grdGSTINDetail.Rows[i].FindControl("txtGSTIN"));
    //            Label lblCompanyStateID = ((Label)grdGSTINDetail.Rows[i].FindControl("lblCompanyStateID"));
    //            Label lblPANNo = ((Label)grdGSTINDetail.Rows[i].FindControl("lblPANNo"));


    //            //if (txtGSTIN.Text != "")
    //            //{
    //                bool GSTINStatus = validGSTIN(txtGSTIN.Text);
    //                if (GSTINStatus == true)
    //                {

    //                    bool GSTIN_NO = gstinvalid(txtGSTIN.Text, lblCompanyStateID.Text, lblPANNo.Text);
    //                    if (GSTIN_NO == true)
    //                    {
    //                        btnGSTINStatus.Visible = true;
    //                        btnGSTINStatus.Text = "Valid";
    //                        btnGSTINStatus.CssClass = "btn btn-success";
    //                    }
    //                    else
    //                    {

    //                        btnGSTINStatus.Visible = false;
    //                        btnGSTINStatus.Text = "Invalid";
    //                        btnGSTINStatus.CssClass = "btn btn-danger";

    //                    }


    //                    //btnGSTINStatus.Visible = true;
    //                    //btnGSTINStatus.Text = "Valid";
    //                    //btnGSTINStatus.CssClass = "btn btn-success";
    //                    //break;
    //                }
    //                else
    //                {
    //                    btnGSTINStatus.Visible = true;

    //                    btnGSTINStatus.Text = "Invalid";
    //                    btnGSTINStatus.CssClass = "btn btn-danger";
    //                    //break;
    //                }
    //            //}
    //            //else
    //            //{
    //            //    btnGSTINStatus.Visible = false;
    //            //}
    //        }
    //    }
    //    catch (Exception ee)
    //    {

    //        ShowMessage("Error." + ee.Message, false);

    //    }
    //}

    #region GstinNumber

    //bool gstinvalid(string GSTIN, string stateid, string PANNo)
    //{
    //    string statevalue;
    //    if (GSTIN.Count() == 15)
    //    {
    //        if (stateid.Length == 1)
    //        {
    //            statevalue = "0" + Convert.ToString(stateid);
    //        }
    //        else
    //        {
    //            statevalue = Convert.ToString(stateid);
    //        }
    //        if (!string.IsNullOrEmpty(GSTIN))
    //        {
    //            string firsttwo = GSTIN.Substring(0, 2);
    //            string nextten = GSTIN.Substring(2, 10).ToUpper();

    //            if (statevalue != firsttwo)
    //            {
    //                //ShowMessage("invalid gstin no.", false);
    //                return false;
    //            }
    //            if (!string.IsNullOrEmpty(PANNo))
    //            {
    //                if (PANNo != nextten)
    //                {
    //                    //ShowMessage("invalid gstin no.", false);
    //                    return false;
    //                }
    //            }
    //            else
    //            {

    //            }
    //        }
    //    }
    //    else
    //    {
    //        ShowMessage("enter 15 digit gstin no.", false);
    //        return false;
    //    }
    //    return true;
    //}



    //private void CHECKGSTIN_VALIDATION()
    //{


    //    for (int i = 0; i < grdGSTINDetail.Rows.Count; i++)
    //    {
    //        Button btnGSTINStatus = (Button)grdGSTINDetail.Rows[i].FindControl("btnGSTINStatus");
    //        TextBox txtGSTIN = ((TextBox)grdGSTINDetail.Rows[i].FindControl("txtGSTIN"));
    //        Label lblCompanyStateID = ((Label)grdGSTINDetail.Rows[i].FindControl("lblCompanyStateID"));
    //        Label lblPANNo = ((Label)grdGSTINDetail.Rows[i].FindControl("lblPANNo"));



    //        bool GSTINStatus = validGSTIN(txtGSTIN.Text);
    //        if (GSTINStatus == true)
    //        {

    //            bool GSTIN_NO = gstinvalid(txtGSTIN.Text, lblCompanyStateID.Text, lblPANNo.Text);
    //            if (GSTIN_NO == true)
    //            {
    //                btnGSTINStatus.Visible = true;
    //                btnGSTINStatus.Text = "Valid";
    //                btnGSTINStatus.CssClass = "btn btn-success";
    //            }

    //            else
    //            {

    //                btnGSTINStatus.Visible = false;
    //                btnGSTINStatus.Text = "Invalid";
    //                btnGSTINStatus.CssClass = "btn btn-danger";

    //            }
    //        }
    //        else
    //        {
    //            btnGSTINStatus.Visible = true;

    //            btnGSTINStatus.Text = "Invalid";
    //            btnGSTINStatus.CssClass = "btn btn-danger";
    //            //break;
    //        }

    //    }
    //}

    //public void main(string txtGSTIN)
    //{
    //    //Sample valid GSTIN - 09 AAAUP  8175 A 1 Z G;
    //    //Scanner sc = new Scanner(System.in);		


    //    Console.Write("Enter GSTIN Number");
    //    //String gstin = sc.next();
    //    String gstin = txtGSTIN;
    //    try
    //    {
    //        if (validGSTIN(gstin))
    //            Response.Write("Valid GSTIN!");

    //        else
    //            Response.Write("Invalid GSTIN");
    //    }
    //    catch (Exception e)
    //    {
    //        ShowMessage("Error." + e.Message, false);

    //    }
    //}

    //private static Boolean validGSTIN(String gstin)
    //{
    //    Boolean isValidFormat = false;
    //    if (checkPattern(gstin, GSTINFORMAT_REGEX))
    //    {
    //        isValidFormat = verifyCheckDigit(gstin);
    //    }
    //    return isValidFormat;

    //}

    //private static Boolean verifyCheckDigit(String gstinWCheckDigit)
    //{
    //    Boolean isCDValid = false;
    //    String newGstninWCheckDigit = getGSTINWithCheckDigit(gstinWCheckDigit.Substring(0, gstinWCheckDigit.Length - 1));

    //    if (gstinWCheckDigit.Trim().Equals(newGstninWCheckDigit))
    //    {
    //        isCDValid = true;
    //    }
    //    return isCDValid;
    //}


    //public static String getGSTINWithCheckDigit(String gstinWOCheckDigit)
    //{
    //    int factor = 2;
    //    int sum = 0;
    //    int checkCodePoint = 0;
    //    char[] cpChars;
    //    char[] inputChars;

    //    try
    //    {
    //        if (gstinWOCheckDigit == null)
    //        {
    //            throw new Exception("GSTIN supplied for checkdigit calculation is null");
    //        }
    //        cpChars = GSTN_CODEPOINT_CHARS.ToCharArray();
    //        inputChars = gstinWOCheckDigit.Trim().ToUpper().ToCharArray();

    //        int mod = cpChars.Length;
    //        for (int i = inputChars.Length - 1; i >= 0; i--)
    //        {
    //            int codePoint = -1;
    //            for (int j = 0; j < cpChars.Length; j++)
    //            {
    //                if (cpChars[j] == inputChars[i])
    //                {
    //                    codePoint = j;
    //                }
    //            }
    //            int digit = factor * codePoint;
    //            factor = (factor == 2) ? 1 : 2;
    //            digit = (digit / mod) + (digit % mod);
    //            sum += digit;
    //        }
    //        checkCodePoint = (mod - (sum % mod)) % mod;
    //        String str = gstinWOCheckDigit + cpChars[checkCodePoint];
    //        return gstinWOCheckDigit + cpChars[checkCodePoint];
    //    }
    //    finally
    //    {
    //        inputChars = null;
    //        cpChars = null;
    //    }
    //}

    //Regex r = new Regex(GSTINFORMAT_REGEX, RegexOptions.IgnoreCase);
    //public static Boolean checkPattern(String inputval, String regxpatrn)
    //{

    //    bool result = false;
    //    Regex r = new Regex(GSTINFORMAT_REGEX, RegexOptions.IgnoreCase);

    //    // Match the regular expression pattern against a text string.
    //    Match m = r.Match(inputval);
    //    return m.Success;

    //}
    #endregion


}