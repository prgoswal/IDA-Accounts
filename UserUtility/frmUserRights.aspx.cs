using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmUserRights : System.Web.UI.Page
{
    public static int editsavecheck = 0;
    DataTable dt;//getLevel,getmenu;
    static DataTable getLevel, getmenu, allotedTable;

    UserModel objUserModel;

    public bool IsSuccess
    {
        get { return Convert.ToBoolean(Session["IsSuccess"]); }
        set { Session["IsSuccess"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            editsavecheck = 0;
            BindUserProfile();//For User Profile Purpose
            BindDocumentMenu();//For User Menu Purpose
            Matrix();//For User Profile & Menu (Both) Diplayed Into Grid Purpose - With Checkbox           
            ShowProfile();
            //GridMatrix.HeaderRow.Cells[3].Text = "READ";
            //GridMatrix.HeaderRow.Cells[4].Text = "WRITE";
            //GridMatrix.HeaderRow.Cells[5].Text = "READ";
            //GridMatrix.HeaderRow.Cells[6].Text = "WRITE";
            //GridMatrix.HeaderRow.Cells[7].Text = "UPDATE";
            //GridMatrix.HeaderRow.Cells[8].Text = "CANCEL";
            //GridMatrix.HeaderRow.Cells[17].Text = "SHOW";
            //GridMatrix.HeaderRow.Cells[18].Text = "HIDE";
            //GridMatrix.HeaderRow.Cells[19].Text = "SHOW";
            //GridMatrix.HeaderRow.Cells[20].Text = "HIDE";
            //GridMatrix.HeaderRow.Cells[21].Text = "SHOW";
            //GridMatrix.HeaderRow.Cells[22].Text = "HIDE";
            ////string s = GridMatrix.HeaderRow.Cells[4].Text.ToString().Replace("WRITE MASTER", "WRITE");

            if (IsSuccess)
            {
                Label1.Text = "Rights Allotted Successfully";
                IsSuccess = false;
            }
        }
    }





    public DataTable BindUserProfile()
    {
        try
        { 
            objUserModel = new UserModel()
            {
                Ind = 11,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,

            };

            string uri = string.Format("User/GetUserDetails");

            var response1 = CommonCls.ApiPostDataTable(uri, objUserModel);
            getLevel = null;

            getLevel = response1;
            getLevel.Columns["LevelDescription"].ColumnName = " ";

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return getLevel;
    }


    public void Matrix()
    {
        try
        {
            dt = new DataTable();
            dt = getLevel.Copy();

            for (int j = 0; j < getmenu.Rows.Count; j++)
                dt.Columns.Add(new DataColumn(getmenu.Rows[j][1].ToString(), typeof(bool)));



            //dt.Columns.Add("ColumnName", typeof(string));
            //dt.Rows.Add("DEGREE");
            //dt.Rows.Add("PROVISIONAL DEGREE");
            //dt.Rows.Add("PASSING");
            //dt.Rows.Add("MERIT");
            //dt.Rows.Add("ATTEMPT");
            //dt.Rows.Add("MEDAL");
            //dt.Rows.Add("DUPLICATE");
            //dt.Rows.Add("DEGREE");


            //dt.Columns["DEGREE CERTIFICATE"].ColumnName = "DEGREE";
            //dt.Columns["PROVISIONAL DEGREE CERTIFICATE"].ColumnName = "PROVISIONAL DEGREE";
            //dt.Columns["PASSING CERTIFICATE"].ColumnName = "PASSING";
            //dt.Columns["MERIT CERTIFICATE"].ColumnName = "MERIT";
            //dt.Columns["ATTEMPT CERTIFICATE"].ColumnName = "ATTEMPT";
            //dt.Columns["MEDAL CERTIFICATE"].ColumnName = "MEDAL";
            //dt.Columns["DUPLICATE MARKSHEET"].ColumnName = "DUPLICATE";
            //dt.Columns["MARKSHEET VERIFICATION CERTIFICATE / LETTER"].ColumnName = "MARKSHEET VERIFICATION";
            //dt.Columns["DEGREE VERIFICATION CERTIFICATE / LETTER"].ColumnName = "DEGREE VERIFICATION";
            //dt.Columns["DATE OF DECLARATION CERTIFICATE / LETTER"].ColumnName = "DATE OF DECLARATION ";
            //dt.Columns["MEDIUM OF INSTRUCTION CERTIFICATE / LETTER"].ColumnName = "MEDIUM OF INSTRUCTION";





            GridMatrix.DataSource = dt;
            GridMatrix.DataBind();

            //For Hiding Profile Level Value From Grid
            GridMatrix.HeaderRow.Cells[1].Visible = false;
            foreach (GridViewRow gvr in GridMatrix.Rows)
            {
                gvr.Cells[1].Visible = false;
                //gvr.Cells[2].Width = 150;
            }



        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    public DataTable ShowProfile()
    {
        try
        {
            objUserModel = new UserModel()
            {
                Ind = 14,
                OrgID = GlobalSession.OrgID,

            };
            string uri = string.Format("User/GetAllottedUserDetails");
            var response1 = CommonCls.ApiPostDataTable(uri, objUserModel);
            allotedTable = response1;
            DataTable dt = new DataTable();
            dt = getLevel.Copy();
            Label1.Text = "";
            for (int i = 0; i < getmenu.Rows.Count; i++)
            {
                for (int j = 0; j < allotedTable.Rows.Count; j++)
                {
                    if (Convert.ToInt32(getmenu.Rows[i][0]) == Convert.ToInt32(allotedTable.Rows[j][1]))
                    {
                        for (int K = 0; K < getLevel.Rows.Count; K++)
                        {

                            if (Convert.ToInt32(getLevel.Rows[K][0]) == Convert.ToInt32(allotedTable.Rows[j][0]))
                            {
                                CheckBox chkRemove = (CheckBox)GridMatrix.Rows[K].Cells[i + 3].Controls[0];
                                chkRemove.Checked = true;
                                break;
                                //chkRemove.CheckedChanged += new EventHandler(MyCheck_CheckedChanged);
                                //chkRemove.AutoPostBack = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return allotedTable;
    }


    public DataTable BindDocumentMenu()
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("ItemID", typeof(int));
            dt.Columns.Add("DocumentType", typeof(string));

            dt.Rows.Add(11, "READ MASTER");
            dt.Rows.Add(12, "WRITE MASTER");

            dt.Rows.Add(21, "READ VOUCHER");
            dt.Rows.Add(22, "WRITE VOUCHER");
            dt.Rows.Add(23, "UPDATE VOUCHER");
            dt.Rows.Add(24, "CANCEL VOUCHER");

            dt.Rows.Add(31, "BOOKS SHOW");
            dt.Rows.Add(32, "BOOKS HIDE");

            dt.Rows.Add(41, "LEDGER SHOW");
            dt.Rows.Add(42, "LEDGER HIDE");

            dt.Rows.Add(51, "Final Acc SHOW");
            dt.Rows.Add(52, "Final Acc HIDE");

            //9series for item 
            dt.Rows.Add(91, "Item SHOW");
            dt.Rows.Add(92, "Item HIDE");

            dt.Rows.Add(61, "Others SHOW");
            dt.Rows.Add(62, "Others HIDE");

            dt.Rows.Add(71, "Utility SHOW");
            dt.Rows.Add(72, "Utility HIDE");

            dt.Rows.Add(81, "GST SHOW");
            dt.Rows.Add(82, "GST HIDE");

            var response1 = dt;
            getmenu = null;
            getmenu = response1;
            //objUserModel = new UserModel()
            //{
            //    Ind = 11,
            //    OrgID = GlobalSession.OrgID,
            //    BrID = GlobalSession.BrID,

            //};

            //string uri = string.Format("User/GetUserDetails");
            //var response1 = CommonCls.ApiPostDataTable(uri, objUserModel);

            //getmenu = null;
            //getmenu = response1;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        return getmenu;
    }
    int rIndex;

    protected void GridMatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int flag = 0;
            string GridMerixRowID = "";
            string ColID = "";
            Label1.Text = "";
            if (e.CommandName == "btnEdit")
            {
                LinkButton btEdit = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)btEdit.NamingContainer;
                rIndex = gvRow.RowIndex;
                LinkButton lbEdit = (LinkButton)GridMatrix.Rows[rIndex].FindControl("linkbtnEdit");
                if (editsavecheck == 0 || lbEdit.Text == "Save")
                {
                    if (lbEdit.Text != "Save" && rIndex >= 0)
                    {
                        editsavecheck = 1;//for one time one active edit button
                        GridMatrix.Rows[rIndex].BackColor = Color.Beige;
                        EnabledDisabledCheckbox(rIndex, true);
                        lbEdit.Text = "Save";
                    }
                    else if (lbEdit.Text == "Save")
                    {

                        GridMerixRowID = gvRow.Cells[1].Text.ToString();
                        if (flag == 0)
                        {
                            objUserModel = new UserModel();
                            objUserModel.OrgID = GlobalSession.OrgID;
                            objUserModel.Ind = 13;
                            objUserModel.UserID = Convert.ToInt32(GridMerixRowID);
                            string uri = string.Format("User/DeleteUserRights");
                            var response1 = CommonCls.ApiPostDataTable(uri, objUserModel);
                        }
                        for (int k = 0; k < getmenu.Rows.Count; k++)
                        {
                            CheckBox chkRemove = (CheckBox)GridMatrix.Rows[rIndex].Cells[k + 3].Controls[0];
                            string tempName = GridMatrix.HeaderRow.Cells[k + 3].Text.ToString();
                            DataRow[] row = getmenu.Select("DocumentType='" + tempName + "'");


                            if (chkRemove.Checked == true)
                            {
                                ColID = row[0].Table.Rows[k][0].ToString();
                                flag = 1;
                                objUserModel = new UserModel();
                                objUserModel.OrgID = GlobalSession.OrgID;
                                objUserModel.Ind = 12;
                                objUserModel.UserID = Convert.ToInt32(GridMerixRowID);
                                objUserModel.MenuID = Convert.ToInt32(ColID);
                                string uri1 = string.Format("User/SaveUserRightsDetails");
                                DataTable dt = CommonCls.ApiPostDataTable(uri1, objUserModel);
                                if (dt.Rows[0][0].ToString() != "")
                                {
                                    //lblmsg.Text = "User Already Exist";
                                }
                                else
                                {
                                    // lblmsg.Text = "Profile Created Successfully";
                                }

                            }
                        }

                        EnabledDisabledCheckbox(rIndex, false);
                        editsavecheck = 0;
                        lbEdit.Text = "Edit";
                        foreach (GridViewRow r in GridMatrix.Rows)
                        {
                            r.BackColor = System.Drawing.Color.Transparent;
                        }
                        //Label1.Text = "Rights Allotted Successfully";
                        IsSuccess = true;
                        Response.Redirect("~/UserUtility/frmUserRights.aspx");
                    }
                }
                else
                {
                    Label1.Text = "Please Save Record Before Edit";
                    // editsavecheck = 0;
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    private void EnabledDisabledCheckbox(int gridrowindex, bool isEnabled)
    {
        for (int k = 0; k < getmenu.Rows.Count; k++)
        {
            CheckBox chk = (CheckBox)GridMatrix.Rows[gridrowindex].Cells[k + 3].Controls[0];
            if (chk != null)
            {
                chk.Enabled = isEnabled;
                //  chk.AutoPostBack = true;
                //  chk.CheckedChanged += new EventHandler(MyCheck_CheckedChanged);
            }
        }

    }
    protected void GridMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbSave = (LinkButton)GridMatrix.FindControl("linkbtnSave");
            if (lbSave != null)
            {
                lbSave.Visible = false;
            }
            else
            {
                return;
            }
        }
    }

    protected void btnfinalsave_Click(object sender, EventArgs e)
    {

    }

    //public event EventHandler MyCheck_CheckedChanged;
    //void MyCheck_CheckedChanged(object sender, EventArgs e)
    //{
    //    lblmsg.Text = "called";

    //    for (int k = 0; k < getmenu.Rows.Count; k++)
    //    {
    //        CheckBox chkRemove = (CheckBox)GridMatrix.Rows[rIndex].Cells[k + 3].Controls[0];
    //        if (chkRemove.Checked == true)
    //        {

    //        }
    //    }
    //}




    //public DataTable ShowProfile()
    //{
    //    try
    //    {
    //        HttpClient HClient = new HttpClient();
    //        HClient.BaseAddress = new Uri(DataAcces.Url);
    //        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
    //        objpl.Ind = 13;
    //        var uri1 = string.Format("api/DocumentApproval/ShowApproval/?Ind={0}", 13);
    //        var response1 = HClient.GetAsync(uri1).Result;
    //        allotedTable = null;
    //        if (response1.IsSuccessStatusCode)
    //            allotedTable = response1.Content.ReadAsAsync<DataTable>().Result;
    //        DataTable dt = new DataTable();
    //        dt = getLevel.Copy();
    //        Label1.Text = "";
    //        for (int i = 0; i < getmenu.Rows.Count; i++)
    //        {
    //            for (int j = 0; j < allotedTable.Rows.Count; j++)
    //            {
    //                if (Convert.ToInt32(getmenu.Rows[i][0]) == Convert.ToInt32(allotedTable.Rows[j][1]))
    //                {
    //                    for (int K = 0; K < getLevel.Rows.Count; K++)
    //                    {

    //                        if (Convert.ToInt32(getLevel.Rows[K][0]) == Convert.ToInt32(allotedTable.Rows[j][0]))
    //                        {
    //                            CheckBox chkRemove = (CheckBox)GridMatrix.Rows[K].Cells[i + 3].Controls[0];
    //                            chkRemove.Checked = true;
    //                            break;
    //                        }
    //                    }

    //                }
    //            }


    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblmsg.Text = ex.Message;
    //    }
    //    return allotedTable;
    //}

}