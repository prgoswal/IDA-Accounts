using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_frmBRS : System.Web.UI.Page
{
    #region Declatration

    BrsBankModel objBrsBank;

    DataTable VsdtBankAccount
    {
        get { return (DataTable)ViewState["dtBankAccount"]; }
        set { ViewState["dtBankAccount"] = value; }
    }

    DataTable VsdtBSEntry
    {
        get { return (DataTable)ViewState["dtBSEntry"]; }
        set { ViewState["dtBSEntry"] = value; }
    }

    DataTable VsdvBAccount
    {
        get { return (DataTable)ViewState["dvBankAccount"]; }
        set { ViewState["dvBankAccount"] = value; }
    }

    DataTable VsdtSummaryDetails
    {
        get { return (DataTable)ViewState["dtSummaryDetails"]; }
        set { ViewState["dtSummaryDetails"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.CssClass = lblMsg.Text = "";
        lblPassMsg.Text = lblPassMsg.CssClass = "";

        if (!IsPostBack)
        {
            pnlPassword.Visible = true;
            pnlPassword.Focus();
            BindAllBRSDDL();
            ddlBankAcc.Enabled = true;
            txtfrmDate.Enabled = true;
            txtToDate.Enabled = true;
            btnShow.Enabled = true;
        }
    }

    void BindAllBRSDDL() // Bank List Bind
    {
        try
        {
            objBrsBank = new BrsBankModel();

            objBrsBank.Ind = 11;
            objBrsBank.OrgID = GlobalSession.OrgID;
            objBrsBank.BrID = GlobalSession.BrID;
            objBrsBank.YrCD = GlobalSession.YrCD;
            objBrsBank.VchType = 3;//Convert.ToInt32(Session["VchType"])


            string uri = string.Format("BrsBank/BindAllBRSDDL");
            DataSet dsBindAllBRSDDL = CommonCls.ApiPostDataSet(uri, objBrsBank);
            if (dsBindAllBRSDDL.Tables.Count > 0)
            {
                DataTable dtBankAccount = dsBindAllBRSDDL.Tables[0];
                DataTable dtBSNarr = dsBindAllBRSDDL.Tables[1];

                if (dtBankAccount.Rows.Count > 0)
                {
                    ddlBankAcc.DataSource = dtBankAccount;
                    ddlBankAcc.DataTextField = "AccName";
                    ddlBankAcc.DataValueField = "AccCode";
                    ddlBankAcc.DataBind();

                    if (dtBankAccount.Rows.Count > 1)
                        ddlBankAcc.Items.Insert(0, new ListItem("-- Select Bank --", "0"));
                }
                if (dtBSNarr.Rows.Count > 0)
                {
                    cbNarration.DataSource = dtBSNarr;
                    //cbNarration.DataTextField = "BSNarrationDesc";
                    cbNarration.DataValueField = "BSNarrationDesc";
                    cbNarration.DataBind();

                    if (dtBSNarr.Rows.Count > 1)
                        cbNarration.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void ddlBankAcc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objBrsBank = new BrsBankModel();
            objBrsBank.Ind = 12;
            objBrsBank.OrgID = GlobalSession.OrgID;
            objBrsBank.BrID = GlobalSession.BrID;
            objBrsBank.YrCD = GlobalSession.YrCD;
            objBrsBank.AccCode = Convert.ToInt32(ddlBankAcc.SelectedValue);

            txtcloseDate.Text = txtToDate.Text;
            string uri = string.Format("BrsBank/BindBRSDateAndBalance");
            DataSet dsDateAndBal = CommonCls.ApiPostDataSet(uri, objBrsBank);

            if (dsDateAndBal.Tables.Count > 0)
            {
                DataTable dtDateAndBal = dsDateAndBal.Tables[0];

                if (dtDateAndBal.Rows.Count > 0)
                {
                    lblOpDate.Text = CommonCls.ConvertDateDB(dtDateAndBal.Rows[0]["OpeningDate"].ToString());
                    lblOpBalance.Text = CommonCls.ConverToCommas(dtDateAndBal.Rows[0]["OpeningBalance"].ToString());
                    lblCloDate.Text = CommonCls.ConvertDateDB(dtDateAndBal.Rows[0]["ClosingDate"].ToString());
                    lblCloBalance.Text = CommonCls.ConverToCommas(dtDateAndBal.Rows[0]["ClosingBalance"].ToString());

                    if (lblOpDate.Text != "" || CommonCls.ConvertDecimalZero(lblOpBalance.Text) > 0
                        && lblCloDate.Text != "" || CommonCls.ConvertDecimalZero(lblCloBalance.Text) > 0)
                    {
                        trDateAndBalance.Visible = trBlankRow.Visible = true;
                        txtfrmDate.Text = CommonCls.ConvertDateDB(Convert.ToDateTime(lblCloDate.Text).AddDays(1));
                    }
                    else
                    {
                        trDateAndBalance.Visible = trBlankRow.Visible = false;
                        lblOpDate.Text = lblOpBalance.Text = lblCloDate.Text = lblCloBalance.Text = txtfrmDate.Text = "";
                    }
                }
                else
                {
                    trDateAndBalance.Visible = trBlankRow.Visible = false;
                    lblOpDate.Text = lblOpBalance.Text = lblCloDate.Text = lblCloBalance.Text = txtfrmDate.Text = "";
                }
            }
            ddlBankAcc.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)//Button Show 
    {
        try
        {

            if (ddlBankAcc.SelectedValue == "0")
            {
                ShowMessage("Please Select Bank Account.", false);
                ddlBankAcc.Focus();
                return;
            }

            if (txtfrmDate.Text.Trim() == "")
            {
                ShowMessage("Please Enter From Date.", false);
                txtfrmDate.Focus();
                return;
            }
            else if (txtToDate.Text.Trim() == "")
            {
                ShowMessage("Please Enter To Date.", false);
                txtToDate.Focus();
                return;
            }
            DateTime F_Date = Convert.ToDateTime(txtfrmDate.Text.Substring(6, 4) + "/" + txtfrmDate.Text.Substring(3, 2) + "/" + txtfrmDate.Text.Substring(0, 2));
            DateTime T_Date = Convert.ToDateTime(txtToDate.Text.Substring(6, 4) + "/" + txtToDate.Text.Substring(3, 2) + "/" + txtToDate.Text.Substring(0, 2));

            if (lblOpDate.Text != "")
            {
                DateTime opDate = Convert.ToDateTime(lblOpDate.Text.Substring(6, 4) + "/" + lblOpDate.Text.Substring(3, 2) + "/" + lblOpDate.Text.Substring(0, 2));

                if (F_Date < opDate)
                {
                    ShowMessage("From Date Should Not Be Less Than Reconciliation Opening Date.", false);
                    txtfrmDate.Focus();
                    return;
                }
            }

            if (lblCloDate.Text != "")
            {
                DateTime cloDate = Convert.ToDateTime(lblCloDate.Text.Substring(6, 4) + "/" + lblCloDate.Text.Substring(3, 2) + "/" + lblCloDate.Text.Substring(0, 2)).AddDays(1);

                if (F_Date > cloDate)
                {
                    ShowMessage("From Date Should Not Be Greater Than Reconciliation Closing Date.", false);
                    txtfrmDate.Focus();
                    return;
                }
            }

            if (T_Date < F_Date)
            {
                ShowMessage("To Date Should Not Be Less Than From Date.", false);
                txtToDate.Focus();
                return;
            }

            objBrsBank = new BrsBankModel();

            objBrsBank.Ind = 1;
            objBrsBank.OrgID = GlobalSession.OrgID;
            objBrsBank.BrID = GlobalSession.BrID;
            objBrsBank.YrCD = GlobalSession.YrCD;
            objBrsBank.AccCode = Convert.ToInt32(ddlBankAcc.SelectedValue);
            objBrsBank.VoucharDateFrom = CommonCls.ConvertToDate(txtfrmDate.Text);
            objBrsBank.VoucharDateto = CommonCls.ConvertToDate(txtToDate.Text);
            txtopenDate.Text = txtfrmDate.Text;

            txtcloseDate.Text = txtToDate.Text;
            string uri = string.Format("BrsBank/LoadGridData");
            DataSet dsBankAccount = CommonCls.ApiPostDataSet(uri, objBrsBank);
            if (dsBankAccount.Tables.Count > 0)
            {
                DataTable dtBankBook = dsBankAccount.Tables[0];
                DataTable dtBankStatement = dsBankAccount.Tables[1];

                if (dtBankBook.Rows.Count > 0)
                {
                    dtBankBook.Columns.Add("MatchInd", typeof(int));

                    if (dtBankBook.Columns.Contains("CompanyID"))
                        dtBankBook.Columns.Remove("CompanyID");

                    if (dtBankBook.Columns.Contains("BranchID"))
                        dtBankBook.Columns.Remove("BranchID");

                    if (dtBankBook.Columns.Contains("BankCode"))
                        dtBankBook.Columns.Remove("BankCode");


                    grdBRS.DataSource = VsdvBAccount = dtBankBook;
                    grdBRS.DataBind();

                    foreach (GridViewRow gvRow in grdBRS.Rows)
                    {
                        Label lblAmtDr = (Label)gvRow.FindControl("lblAmtDr");
                        Label lblAmtCr = (Label)gvRow.FindControl("lblAmtCr");
                        Label lblMatchInd = (Label)gvRow.FindControl("lblMatchInd");
                        TextBox txtAmount = (TextBox)gvRow.FindControl("txtAmount");
                        TextBox txtDate = (TextBox)gvRow.FindControl("txtDate");

                        if (txtDate.Text != "")
                        {
                            if ((CommonCls.ConvertDecimalZero(lblAmtDr.Text) + CommonCls.ConvertDecimalZero(lblAmtCr.Text)) != CommonCls.ConvertDecimalZero(txtAmount.Text))
                            {
                                lblMatchInd.Text = "2";
                                gvRow.Style.Add("background-color", "#ff8a65");
                                txtDate.Style.Add("background-color", "#ff8a65");
                                txtAmount.Style.Add("background-color", "#ff8a65");
                                gvRow.Style.Add("color", "white");
                                txtDate.Style.Add("color", "white");
                                txtAmount.Style.Add("color", "white");
                            }
                            else
                            {
                                gvRow.Style.Add("background-color", "#27c24c");
                                txtDate.Style.Add("background-color", "#27c24c");
                                txtAmount.Style.Add("background-color", "#27c24c");
                                gvRow.Style.Add("color", "white");
                                txtDate.Style.Add("color", "white");
                                txtAmount.Style.Add("color", "white");
                                lblMatchInd.Text = "3";
                            }
                            rdByRow.Enabled = true;
                        }
                        else
                        {
                            gvRow.Style.Add("background-color", "white");
                            txtDate.Style.Add("background-color", "white");
                            txtAmount.Style.Add("background-color", "white");
                            gvRow.Style.Add("color", "black");
                            txtDate.Style.Add("color", "black");
                            txtAmount.Style.Add("color", "black");
                            lblMatchInd.Text = "1";
                        }
                    }

                    foreach (DataRow row in VsdvBAccount.Rows)
                    {
                        decimal drAmt = CommonCls.ConvertDecimalZero(row["DrAmount"].ToString());
                        decimal crAmt = CommonCls.ConvertDecimalZero(row["CrAmount"].ToString());
                        decimal bsAmount = CommonCls.ConvertDecimalZero(row["BSAmount"].ToString());
                        string matchInd = row["MatchInd"].ToString();
                        string bsDate = CommonCls.ConvertDateDB(row["BSDate"].ToString());

                        if (bsDate != "")
                        {
                            if ((drAmt + crAmt) != bsAmount)
                            {
                                row["MatchInd"] = "2";
                            }
                            else
                            {
                                row["MatchInd"] = "3";
                            }
                        }
                        else
                        {
                            row["MatchInd"] = "1";
                        }

                        if (bsAmount <= 0)
                        {
                            row["BSAmount"] = drAmt + crAmt;
                        }
                    }
                }
                if (dtBankStatement.Rows.Count > 0)
                {
                    dtBankStatement.Columns.Add("ExtraInd", typeof(int));

                    foreach (DataRow row in dtBankStatement.Rows)
                    {
                        row["ExtraInd"] = 1;
                    }

                    grdBSEntry.DataSource = VsdtBSEntry = dtBankStatement;
                    grdBSEntry.DataBind();
                    pnlBSE.Visible = true;
                }
                if (dsBankAccount.Tables[2].Rows.Count > 0)
                {
                    DataTable dtOPCOSDate = dsBankAccount.Tables[1];
                    txtopenDate.Text = CommonCls.ConvertDateDB(dsBankAccount.Tables[2].Rows[0]["OpeningDate"].ToString());
                    txtcloseDate.Text = CommonCls.ConvertDateDB(dsBankAccount.Tables[2].Rows[0]["ClosingDate"].ToString());
                    if (txtfrmDate.Text == lblOpDate.Text && txtToDate.Text == lblCloDate.Text)
                    {
                        txtopenBal.Text = CommonCls.ConverToCommas(dsBankAccount.Tables[2].Rows[0]["OpeningBalance"].ToString());
                        txtClosebal.Text = CommonCls.ConverToCommas(dsBankAccount.Tables[2].Rows[0]["ClosingBalance"].ToString());
                    }                    
                }
                if (txtfrmDate.Text != lblOpDate.Text && txtToDate.Text != lblCloDate.Text)
                {
                    txtopenBal.Text = CommonCls.ConverToCommas(lblCloBalance.Text);
                    txtClosebal.Text = CommonCls.ConverToCommas(0);
                }

                ddlBankAcc.Enabled = txtfrmDate.Enabled = txtToDate.Enabled = btnShow.Enabled = false;
                rdBySerial.Enabled = btnAddBSEntry.Enabled = btnSave.Enabled = btnSummary.Enabled = true;
                rdBySerial.Checked = true;
            }
            else
            {
                ShowMessage("Data Is Not Found", false);
            }
            myModal.Style.Add("display", "none");
            myDetailModal.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void txtopenBal_TextChanged(object sender, EventArgs e)
    {
        if (lblCloBalance.Text != "")
        {
            if (CommonCls.ConvertDecimalZero(lblCloBalance.Text) != CommonCls.ConvertDecimalZero(txtopenBal.Text))
            {
                pnlPopUp.Visible = true;
                btnNo.Visible = false;
                lblAlterHeader.Text = "Given Opening Balance Is Not Equal To Last Reconciliation Closing Balance."; //"Opening Balance Is Not Equal To Reconciliation Closing Balance, Are You Sure To Proceed.";
                lblAlterHeader.ForeColor = System.Drawing.Color.White;
                hfMSGInd.Value = "1";
                divpnlPopUp.Style.Add("background-color", "#ee6666");
            }
            else
                ddlDrCrOpenBal.Focus();
        }
    }

    protected void grdBRS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblAmtCr = (Label)e.Row.FindControl("lblAmtCr");
                Label lblAmtDr = (Label)e.Row.FindControl("lblAmtDr");
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                TextBox txtDate = (TextBox)e.Row.FindControl("txtDate");

                lblAmtCr.Text = CommonCls.ConverToCommas(lblAmtCr.Text);
                lblAmtDr.Text = CommonCls.ConverToCommas(lblAmtDr.Text);
                txtAmount.Text = CommonCls.ConverToCommas(txtAmount.Text);
                txtDate.Text = CommonCls.ConvertDateDB(txtDate.Text);
                Label lblMatchInd = (Label)e.Row.FindControl("lblMatchInd");
                if (!string.IsNullOrEmpty(lblMatchInd.Text))
                {
                    if (CommonCls.ConvertIntZero(lblMatchInd.Text) == 3)
                    {
                        e.Row.Style.Add("background-color", "#27c24c");
                        txtDate.Style.Add("background-color", "#27c24c");
                        txtAmount.Style.Add("background-color", "#27c24c");
                        e.Row.Style.Add("color", "white");
                        txtDate.Style.Add("color", "white");
                        txtAmount.Style.Add("color", "white");
                    }
                    else if (CommonCls.ConvertIntZero(lblMatchInd.Text) == 2)
                    {
                        e.Row.Style.Add("background-color", "#ff8a65");
                        txtDate.Style.Add("background-color", "#ff8a65");
                        txtAmount.Style.Add("background-color", "#ff8a65");
                        e.Row.Style.Add("color", "white");
                        txtDate.Style.Add("color", "white");
                        txtAmount.Style.Add("color", "white");
                    }
                    else
                    {
                        e.Row.Style.Add("background-color", "white");
                        txtDate.Style.Add("background-color", "white");
                        txtAmount.Style.Add("background-color", "white");
                        e.Row.Style.Add("color", "black");
                        txtDate.Style.Add("color", "black");
                        txtAmount.Style.Add("color", "black");
                    }
                }
                else
                {
                    if (CommonCls.ConvertDecimalZero(txtAmount.Text) <= 0)
                        txtAmount.Text = (CommonCls.ConvertDecimalZero(lblAmtCr.Text) + CommonCls.ConvertDecimalZero(lblAmtDr.Text)).ToString();

                }

                myModal.Style.Add("display", "none");
                myDetailModal.Style.Add("display", "none");
            }
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        MatchAndMisMatchRowAccordingToDateAndAmount();

        TextBox txtBSDate = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txtBSDate.NamingContainer;
        Label lblVoucherDate = (Label)gvRow.FindControl("lblVoucherDate");
        Label lblAmtDr = (Label)gvRow.FindControl("lblAmtDr");
        Label lblAmtCr = (Label)gvRow.FindControl("lblAmtCr");
        TextBox txtDate = (TextBox)gvRow.FindControl("txtDate");
        TextBox txtAmount = (TextBox)gvRow.FindControl("txtAmount");
        txtAmount.Text = CommonCls.ConverToCommas(txtAmount.Text);
        if (string.IsNullOrEmpty(txtDate.Text) || (!string.IsNullOrEmpty(txtDate.Text) && CommonCls.ConvertDecimalZero(txtAmount.Text) <= 0))
        {
            RowWhite(gvRow, txtDate, txtAmount);
            txtAmount.Focus();
        }
        //if (Convert.ToDateTime(txtDate.Text) <= Convert.ToDateTime(lblVoucherDate.Text))
        //{
        //    ShowMessage("Bank Statement Date Should't Be LessThan And Equal To Voucher Date.", false);
        //    gvRow.Style.Add("background-color", "white");
        //    txtDate.Style.Add("background-color", "white");
        //    txtAmount.Style.Add("background-color", "white");
        //    gvRow.Style.Add("color", "black");
        //    txtDate.Style.Add("color", "black");
        //    txtAmount.Style.Add("color", "black");
        //    txtDate.Focus();
        //}
        else if ((CommonCls.ConvertDecimalZero(lblAmtDr.Text) + CommonCls.ConvertDecimalZero(lblAmtCr.Text)) != CommonCls.ConvertDecimalZero(txtAmount.Text) && CommonCls.ConvertDecimalZero(txtAmount.Text) > 0)
        {
            RowDeepOrangeLighten2(gvRow, txtDate, txtAmount);
            txtAmount.Focus();
        }
        else
        {
            RowGreen(gvRow, txtDate, txtAmount);
            rdByRow.Enabled = true;
            txtAmount.Focus();
        }
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        MatchAndMisMatchRowAccordingToDateAndAmount();

        int rowIndex = 0;
        TextBox txtBSAmt = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txtBSAmt.NamingContainer;
        Label lblAmtDr = (Label)gvRow.FindControl("lblAmtDr");
        Label lblAmtCr = (Label)gvRow.FindControl("lblAmtCr");
        TextBox txtDate = (TextBox)gvRow.FindControl("txtDate");
        TextBox txtAmount = (TextBox)gvRow.FindControl("txtAmount");
        txtAmount.Text = CommonCls.ConverToCommas(txtAmount.Text);
        decimal totalCrDrAmt = CommonCls.ConvertDecimalZero(lblAmtDr.Text) + CommonCls.ConvertDecimalZero(lblAmtCr.Text);
        if (!string.IsNullOrEmpty(txtDate.Text))
        {
            if (totalCrDrAmt != CommonCls.ConvertDecimalZero(txtAmount.Text) && CommonCls.ConvertDecimalZero(txtAmount.Text) > 0)
            {
                RowDeepOrangeLighten2(gvRow, txtDate, txtAmount);
                txtAmount.Focus();
            }
            else if (CommonCls.ConvertDecimalZero(txtAmount.Text) <= 0)
            {
                RowWhite(gvRow, txtDate, txtAmount);
            }
            else
            {
                RowGreen(gvRow, txtDate, txtAmount);
            }
        }
        else
            txtAmount.Focus();

        rowIndex = gvRow.RowIndex;
        int rowCount = 0;
        rowCount = grdBRS.Rows.Count;
        if (rowIndex < (rowCount - 1))
        {
            TextBox txt = grdBRS.Rows[rowIndex + 1].Cells[7].Controls[0].FindControl("txtDate") as TextBox;
            txt.Focus();
        }
        else
            txtAmount.Focus();

        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    void RowWhite(GridViewRow gvRow, TextBox txtDate, TextBox txtAmount)
    {
        gvRow.Style.Add("background-color", "white");
        txtDate.Style.Add("background-color", "white");
        txtAmount.Style.Add("background-color", "white");
        gvRow.Style.Add("color", "black");
        txtDate.Style.Add("color", "black");
        txtAmount.Style.Add("color", "black");
    }

    void RowGreen(GridViewRow gvRow, TextBox txtDate, TextBox txtAmount)
    {
        gvRow.Style.Add("background-color", "#27c24c");
        txtDate.Style.Add("background-color", "#27c24c");
        txtAmount.Style.Add("background-color", "#27c24c");
        gvRow.Style.Add("color", "white");
        txtDate.Style.Add("color", "white");
        txtAmount.Style.Add("color", "white");
    }

    void RowDeepOrangeLighten2(GridViewRow gvRow, TextBox txtDate, TextBox txtAmount)
    {
        gvRow.Style.Add("background-color", "#ff8a65");
        txtDate.Style.Add("background-color", "#ff8a65");
        txtAmount.Style.Add("background-color", "#ff8a65");
        gvRow.Style.Add("color", "white");
        txtDate.Style.Add("color", "white");
        txtAmount.Style.Add("color", "white");
    }

    protected void rdBySerial_CheckedChanged(object sender, EventArgs e)
    {
        DataView dtSort = new DataView(VsdvBAccount);
        dtSort.Sort = "SrNo Asc";
        grdBRS.DataSource = dtSort;
        grdBRS.DataBind();
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    protected void rdByRow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataView dtSort = new DataView(VsdvBAccount);
            dtSort.Sort = "MatchInd Asc";
            grdBRS.DataSource = dtSort;
            grdBRS.DataBind();
            myModal.Style.Add("display", "none");
            myDetailModal.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void MatchAndMisMatchRowAccordingToDateAndAmount()
    {
        VsdvBAccount = new DataTable();

        DataTable dt = new DataTable();
        dt.Columns.Add("SrNo", typeof(int));
        dt.Columns.Add("VoucharNo", typeof(int));
        dt.Columns.Add("VoucharDate", typeof(string));
        dt.Columns.Add("ChequeNo", typeof(string));
        dt.Columns.Add("UTRNo", typeof(string));
        dt.Columns.Add("DrAmount", typeof(decimal));
        dt.Columns.Add("CrAmount", typeof(decimal));
        dt.Columns.Add("Narration", typeof(string));
        dt.Columns.Add("BSDate", typeof(string));
        dt.Columns.Add("BSAmount", typeof(decimal));
        dt.Columns.Add("MatchInd", typeof(int));

        Label lblSrNo = new Label();
        Label lblVoucherNo = new Label();
        Label lblVoucherDate = new Label();
        Label lblChqNo = new Label();
        //Label lblUtrNo = new Label();
        Label lblAmtDr = new Label();
        Label lblAmtCr = new Label();
        Label lblNarr = new Label();
        TextBox txtDate = new TextBox();
        TextBox txtAmount = new TextBox();

        foreach (GridViewRow gvRows in grdBRS.Rows)
        {
            lblSrNo = (Label)gvRows.FindControl("lblSrNo");
            lblVoucherNo = (Label)gvRows.FindControl("lblVoucharNo");
            lblVoucherDate = (Label)gvRows.FindControl("lblVoucherDate");
            lblChqNo = (Label)gvRows.FindControl("lblChqNo");
            //lblUtrNo = (Label)gvRows.FindControl("lblUtrNo");
            lblAmtDr = (Label)gvRows.FindControl("lblAmtDr");
            lblAmtCr = (Label)gvRows.FindControl("lblAmtCr");
            lblNarr = (Label)gvRows.FindControl("lblNarr");
            txtDate = (TextBox)gvRows.FindControl("txtDate");
            txtAmount = (TextBox)gvRows.FindControl("txtAmount");

            AddNewRow(dt, lblSrNo.Text, lblVoucherNo.Text, lblVoucherDate.Text, lblChqNo.Text, lblAmtDr.Text, lblAmtCr.Text, lblNarr.Text, txtDate.Text, txtAmount.Text);
        }
    }

    void AddNewRow(DataTable dt, string SrNo, string voucherNo, string voucherDate, string chequeNo, string drAmt,
        string crAmt, string narration, string bsDate, string bsAmt)
    {
        DataRow dr = dt.NewRow();
        dr["SrNo"] = CommonCls.ConvertIntZero(SrNo);
        dr["VoucharNo"] = CommonCls.ConvertIntZero(voucherNo);
        dr["VoucharDate"] = voucherDate;
        dr["ChequeNo"] = CommonCls.ConvertIntZero(chequeNo);
        dr["UTRNo"] = "0";
        dr["DrAmount"] = CommonCls.ConvertDecimalZero(drAmt);
        dr["CrAmount"] = CommonCls.ConvertDecimalZero(crAmt);
        dr["Narration"] = narration;
        dr["BSDate"] = bsDate;
        dr["BSAmount"] = CommonCls.ConvertDecimalZero(bsAmt);
        if (bsDate != "")
        {
            if ((CommonCls.ConvertDecimalZero(drAmt) + CommonCls.ConvertDecimalZero(crAmt)) == CommonCls.ConvertDecimalZero(bsAmt))
            {
                dr["MatchInd"] = 3;
            }
            else
            {
                dr["MatchInd"] = 2;
            }
        }
        else
        {
            dr["MatchInd"] = 1;
        }

        dt.Rows.Add(dr);
        VsdvBAccount = dt;
    }

    #region Bank Statement Entry

    protected void lnkBSEntry_Click(object sender, EventArgs e)
    {
        if (pnlBSE.Visible)
            pnlBSE.Visible = false;
        else
            pnlBSE.Visible = true;

        lnkBSEntry.Focus();
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    protected void txtDrAmt_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDrAmt.Text))
            txtCrAmt.Enabled = false;
        else
            txtCrAmt.Enabled = true;

        cbNarration.Focus();
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    protected void txtCrAmt_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtCrAmt.Text))
            txtDrAmt.Enabled = false;
        else
            txtDrAmt.Enabled = true;

        cbNarration.Focus();
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    protected void btnAddBSEntry_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationBtnBSEntry())
            {
                myModal.Style.Add("display", "none");
                return;
            }

            DataTable dtBSEntry = new DataTable();

            if (VsdtBSEntry == null || VsdtBSEntry.Rows.Count <= 0)
                dtBSEntry = DtBSEntrySchema();
            else
                dtBSEntry = VsdtBSEntry;

            DataRow drBSEntry = dtBSEntry.NewRow();
            drBSEntry["CompanyID"] = GlobalSession.OrgID;
            drBSEntry["BranchID"] = GlobalSession.BrID;
            drBSEntry["YrCode"] = GlobalSession.YrCD;
            drBSEntry["DateFrom"] = "";
            drBSEntry["DateTo"] = "";
            drBSEntry["BankCode"] = 0;
            drBSEntry["VoucharDate"] = CommonCls.ConvertDateDB(txtStatementDate.Text);
            drBSEntry["VoucharNo"] = 0;
            drBSEntry["Narration"] = cbNarration.Text;
            drBSEntry["ChequeNo"] = "";
            drBSEntry["UTRNo"] = "";
            drBSEntry["DrAmount"] = CommonCls.ConvertDecimalZero(txtDrAmt.Text);
            drBSEntry["CrAmount"] = CommonCls.ConvertDecimalZero(txtCrAmt.Text);
            drBSEntry["BSDate"] = "";
            drBSEntry["BSAmount"] = 0;
            drBSEntry["BSInd"] = 2;
            drBSEntry["ExtraInd"] = 0;

            dtBSEntry.Rows.Add(drBSEntry);
            grdBSEntry.DataSource = VsdtBSEntry = dtBSEntry;
            grdBSEntry.DataBind();

            ClearAfterBSEntry();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    bool ValidationBtnBSEntry()
    {
        if (string.IsNullOrEmpty(txtStatementDate.Text))
        {
            ShowMessage("Enter Statement Date.", false);
            txtStatementDate.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtDrAmt.Text))
        {
            if (string.IsNullOrEmpty(txtCrAmt.Text))
            {
                ShowMessage("Enter Dr. Amount.", false);
                txtDrAmt.Focus();
                return false;
            }
        }
        if (string.IsNullOrEmpty(txtCrAmt.Text))
        {
            if (string.IsNullOrEmpty(txtDrAmt.Text))
            {
                ShowMessage("Enter Cr. Amount.", false);
                txtCrAmt.Focus();
                return false;
            }
        }
        if (string.IsNullOrEmpty(cbNarration.Text))
        {
            ShowMessage("Enter Narration.", false);
            cbNarration.Focus();
            return false;
        }
        //if (string.IsNullOrEmpty(txtBSDate.Text))
        //{
        //    ShowMessage("Enter Bank Statement Date.", false);
        //    txtBSDate.Focus();
        //    return false;
        //}
        //if (string.IsNullOrEmpty(txtBSAmt.Text))
        //{
        //    ShowMessage("Enter Bank Statement Amount.", false);
        //    txtBSAmt.Focus();
        //    return false;
        //}
        return true;
    }

    void ClearAfterBSEntry()
    {
        txtStatementDate.Text = txtDrAmt.Text = txtCrAmt.Text = "";//txtBSDate.Text = txtBSAmt.Text = 
        cbNarration.ClearSelection();
        txtStatementDate.Focus();
        txtDrAmt.Enabled = txtCrAmt.Enabled = true;
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    protected void grdBSEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveRow")
        {
            DataTable dtBSEntry = VsdtBSEntry;
            dtBSEntry.Rows[rowIndex].Delete();
            VsdtBSEntry = dtBSEntry;
            grdBSEntry.DataSource = dtBSEntry;
            grdBSEntry.DataBind();
            myModal.Style.Add("display", "none");
            myDetailModal.Style.Add("display", "none");
        }
    }

    protected void grdBSEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblDrAmt = (Label)e.Row.FindControl("lblDrAmt");
                Label lblCrAmt = (Label)e.Row.FindControl("lblCrAmt");
                TextBox txtBBDate = (TextBox)e.Row.FindControl("txtBBDate");
                TextBox txtBBAmount = (TextBox)e.Row.FindControl("txtBBAmount");
                Button btnDel = (Button)e.Row.FindControl("btnDel");
                Label lblExtraInd = (Label)e.Row.FindControl("lblExtraInd");
                if (CommonCls.ConvertIntZero(lblExtraInd.Text) == 1)
                {
                    txtBBDate.Enabled = txtBBAmount.Enabled = true;
                    btnDel.Enabled = false;
                }
                else
                {
                    txtBBDate.Enabled = txtBBAmount.Enabled = false;
                    btnDel.Enabled = true;
                }

                lblDrAmt.Text = CommonCls.ConverToCommas(lblDrAmt.Text);
                lblCrAmt.Text = CommonCls.ConverToCommas(lblCrAmt.Text);
                txtBBAmount.Text = CommonCls.ConverToCommas((CommonCls.ConvertDecimalZero(lblCrAmt.Text) + CommonCls.ConvertDecimalZero(lblDrAmt.Text)).ToString());
                myModal.Style.Add("display", "none");
                myDetailModal.Style.Add("display", "none");
            }
        }
    }

    #region Bank Statement Entry Schema

    DataTable DtBSEntrySchema()
    {
        DataTable dtBSEntry = new DataTable();
        dtBSEntry.Columns.Add("CompanyID", typeof(int));
        dtBSEntry.Columns.Add("BranchID", typeof(int));
        dtBSEntry.Columns.Add("YrCode", typeof(int));
        dtBSEntry.Columns.Add("DateFrom", typeof(string));
        dtBSEntry.Columns.Add("DateTo", typeof(string));
        dtBSEntry.Columns.Add("BankCode", typeof(int));
        dtBSEntry.Columns.Add("VoucharDate", typeof(string));
        dtBSEntry.Columns.Add("voucharNo", typeof(int));
        dtBSEntry.Columns.Add("Narration", typeof(string));
        dtBSEntry.Columns.Add("ChequeNo", typeof(string));
        dtBSEntry.Columns.Add("UTRNo", typeof(string));
        dtBSEntry.Columns.Add("DrAmount", typeof(decimal));
        dtBSEntry.Columns.Add("CrAmount", typeof(decimal));
        dtBSEntry.Columns.Add("BSDate", typeof(string));
        dtBSEntry.Columns.Add("BSAmount", typeof(decimal));
        dtBSEntry.Columns.Add("BSInd", typeof(int));
        dtBSEntry.Columns.Add("ExtraInd", typeof(int));

        return dtBSEntry;
    }

    #endregion

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateOnSave())
        {
            myModal.Style.Add("display", "none");
            myDetailModal.Style.Add("display", "none");
            return;
        }
        if (CommonCls.ConvertDecimalZero(txtClosebal.Text) == 0)
        {
            pnlPopUp.Visible = true;
            lblAlterHeader.Text = "Closing Balance Is Zero, Are You Sure To Proceed.";
            btnNo.Visible = true;
            hfMSGInd.Value = "2";
            divpnlPopUp.Style.Add("background-color", "#1c75bf");
        }
        else
            SaveBRS();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        pnlPopUp.Visible = false;
        lblAlterHeader.Text = "";
        if (hfMSGInd.Value == "1")
            txtopenBal.Focus();
        else
            SaveBRS();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlPopUp.Visible = false;
        lblAlterHeader.Text = "";
        txtClosebal.Focus();
    }

    void SaveBRS()
    {
        objBrsBank = new BrsBankModel();
        objBrsBank.Ind = 2;
        objBrsBank.OrgID = GlobalSession.OrgID;
        objBrsBank.BrID = GlobalSession.BrID;
        objBrsBank.YrCD = GlobalSession.YrCD;
        objBrsBank.AccCode = CommonCls.ConvertIntZero(ddlBankAcc.SelectedValue);
        objBrsBank.VoucharDateFrom = CommonCls.ConvertToDate(txtfrmDate.Text);
        objBrsBank.VoucharDateto = CommonCls.ConvertToDate(txtToDate.Text);
        objBrsBank.OpeningDate = CommonCls.ConvertToDate(txtopenDate.Text);
        objBrsBank.OpeningBalance = CommonCls.ConvertDecimalZero(txtopenBal.Text);
        objBrsBank.ClosingDate = CommonCls.ConvertToDate(txtcloseDate.Text);
        objBrsBank.ClosingBalance = CommonCls.ConvertDecimalZero(txtClosebal.Text);
        objBrsBank.DtBrs = BRSSchemaWithData();

        string uri = string.Format("BrsBank/SaveBRS");
        DataTable dtBankAccount = CommonCls.ApiPostDataTable(uri, objBrsBank);
        if (dtBankAccount.Rows.Count > 0)
        {
            ShowMessage("Data Successfully Submited.", true);
            pnlAfterSucess.Visible = true;
            //clearAll();
        }
        else
        {
            ShowMessage("Data Not Submit.", false);
            myModal.Style.Add("display", "none");
            myDetailModal.Style.Add("display", "none");
        }
    }

    private bool ValidateOnSave()
    {
        if (CommonCls.ConvertIntZero(ddlBankAcc.SelectedValue) == 0)
        {
            ddlBankAcc.Focus();
            ShowMessage("Select Bank.", false);
            return false;
        }
        if (CommonCls.ConvertToDate(txtfrmDate.Text) == string.Empty)
        {
            ddlBankAcc.Focus();
            ShowMessage("Select From Date.", false);
            return false;
        }
        if (CommonCls.ConvertToDate(txtToDate.Text) == string.Empty)
        {
            ddlBankAcc.Focus();
            ShowMessage("Select To Date.", false);
            return false;
        }

        if (CommonCls.ConvertToDate(txtopenDate.Text) == string.Empty)
        {
            txtopenDate.Focus();
            ShowMessage("Select Opening Date.", false);
            return false;
        }

        if (CommonCls.ConvertToDate(txtcloseDate.Text) == string.Empty)
        {
            txtcloseDate.Focus();
            ShowMessage("Select Closing Date.", false);
            return false;
        }

        return true;
    }

    DataTable BRSSchemaWithData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CompanyID", typeof(int));
        dt.Columns.Add("BranchID", typeof(int));
        dt.Columns.Add("YrCode", typeof(int));
        dt.Columns.Add("DateFrom", typeof(string));
        dt.Columns.Add("DateTo", typeof(string));
        dt.Columns.Add("BankCode", typeof(int));
        dt.Columns.Add("VoucharDate", typeof(string));
        dt.Columns.Add("VoucharNo", typeof(int));
        dt.Columns.Add("Narration", typeof(string));
        dt.Columns.Add("ChequeNo", typeof(string));
        dt.Columns.Add("UTRNo", typeof(string));
        dt.Columns.Add("DrAmount", typeof(decimal));
        dt.Columns.Add("CrAmount", typeof(decimal));
        dt.Columns.Add("BSDate", typeof(string));
        dt.Columns.Add("BSAmount", typeof(decimal));
        dt.Columns.Add("BSInd", typeof(int));

        foreach (GridViewRow item in grdBRS.Rows)
        {
            //if (CommonCls.ConvertToDate((item.FindControl("txtDate") as TextBox).Text) == string.Empty)
            //{
            //    if (CommonCls.ConvertDecimalZero((item.FindControl("txtAmount") as TextBox).Text) <= 0)
            //        continue;
            //}

            if (CommonCls.ConvertToDate((item.FindControl("txtDate") as TextBox).Text) != "")
            {
                if (CommonCls.ConvertDecimalZero((item.FindControl("txtAmount") as TextBox).Text) > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["CompanyID"] = GlobalSession.OrgID;
                    dr["BranchID"] = GlobalSession.BrID;
                    dr["YrCode"] = GlobalSession.YrCD;
                    dr["BankCode"] = CommonCls.ConvertIntZero(ddlBankAcc.SelectedValue);
                    dr["VoucharDate"] = CommonCls.ConvertToDate((item.FindControl("lblVoucherDate") as Label).Text);
                    dr["voucharNo"] = (item.FindControl("lblVoucharNo") as Label).Text;
                    dr["Narration"] = (item.FindControl("lblNarr") as Label).Text;
                    dr["ChequeNo"] = (item.FindControl("lblChqNo") as Label).Text;
                    dr["UTRNo"] = "0";
                    dr["DrAmount"] = CommonCls.ConverFromCurrency((item.FindControl("lblAmtDr") as Label).Text);
                    dr["CrAmount"] = CommonCls.ConverFromCurrency((item.FindControl("lblAmtCr") as Label).Text);
                    dr["BSDate"] = CommonCls.ConvertToDate((item.FindControl("txtDate") as TextBox).Text);
                    dr["BSAmount"] = CommonCls.ConverFromCurrency((item.FindControl("txtAmount") as TextBox).Text);
                    dr["BSInd"] = 1;
                    dt.Rows.Add(dr);
                }
            }
        }

        foreach (GridViewRow row in grdBSEntry.Rows)
        {
            DataRow dr = dt.NewRow();
            dr["CompanyID"] = GlobalSession.OrgID;
            dr["BranchID"] = GlobalSession.BrID;
            dr["YrCode"] = GlobalSession.YrCD;
            dr["BankCode"] = CommonCls.ConvertIntZero(ddlBankAcc.SelectedValue);
            dr["VoucharDate"] = CommonCls.ConvertToDate((row.FindControl("lblStatementDate") as Label).Text);
            dr["voucharNo"] = 0;
            dr["Narration"] = (row.FindControl("lblNarration") as Label).Text;
            dr["ChequeNo"] = "0";
            dr["UTRNo"] = "0";
            dr["DrAmount"] = CommonCls.ConvertDecimalZero((row.FindControl("lblDrAmt") as Label).Text);
            dr["CrAmount"] = CommonCls.ConvertDecimalZero((row.FindControl("lblCrAmt") as Label).Text);
            dr["BSDate"] = CommonCls.ConvertToDate((row.FindControl("txtBBDate") as TextBox).Text);
            dr["BSAmount"] = 0;
            dr["BSInd"] = 2;
            dt.Rows.Add(dr);
        }

        return dt;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clearAll();
    }

    void clearAll()
    {
        txtfrmDate.Text = "";
        txtToDate.Text = "";
        VsdvBAccount = VsdtBSEntry = VsdtBankAccount = null;
        VsdtBSEntry = VsdtBankAccount = new DataTable();
        grdBRS.DataSource = grdBSEntry.DataSource = new DataTable();
        grdBRS.DataBind(); grdBSEntry.DataBind();
        ddlBankAcc.Enabled = true;
        txtfrmDate.Enabled = true;
        txtToDate.Enabled = true;
        btnShow.Enabled = true;
        ddlBankAcc.ClearSelection();
        ddlBankAcc.Focus();
        txtopenBal.Text = "";
        txtopenDate.Text = "";
        txtcloseDate.Text = "";
        txtClosebal.Text = "";
        rdByRow.Checked = rdBySerial.Checked = false;
        rdByRow.Enabled = rdBySerial.Enabled = false;

        txtStatementDate.Text = txtDrAmt.Text = txtCrAmt.Text = "";//txtBSDate.Text = txtBSAmt.Text = 
        cbNarration.ClearSelection();
        txtStatementDate.Focus();
        txtDrAmt.Enabled = txtCrAmt.Enabled = true;

        btnSummary.Enabled = btnSave.Enabled = false;

        trDateAndBalance.Visible = trBlankRow.Visible = false;
        lblOpDate.Text = lblOpBalance.Text = lblCloDate.Text = lblCloBalance.Text = "";

        pnlBSE.Visible = false;
        myModal.Style.Add("display", "none");
        myDetailModal.Style.Add("display", "none");
    }

    #region Summary

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        try
        {
            objBrsBank = new BrsBankModel();

            objBrsBank.Ind = 3;
            objBrsBank.OrgID = GlobalSession.OrgID;
            objBrsBank.BrID = GlobalSession.BrID;
            objBrsBank.YrCD = GlobalSession.YrCD;
            objBrsBank.VchType = 3;//Convert.ToInt32(Session["VchType"])
            objBrsBank.AccCode = Convert.ToInt32(ddlBankAcc.SelectedValue);
            objBrsBank.VoucharDateFrom = CommonCls.ConvertToDate(txtfrmDate.Text);
            objBrsBank.VoucharDateto = CommonCls.ConvertToDate(txtToDate.Text);


            string uri = string.Format("BrsBank/BRSReconciliationSummary");
            DataSet dsBRSSummAndSummDetails = CommonCls.ApiPostDataSet(uri, objBrsBank);
            if (dsBRSSummAndSummDetails.Tables.Count > 0)
            {
                DataTable dtBRSReconciliationSummary = dsBRSSummAndSummDetails.Tables[0];
                VsdtSummaryDetails = dsBRSSummAndSummDetails.Tables[1];

                lblSummaryBankName.Text = ddlBankAcc.SelectedItem.Text;
                lblSummaryAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

                if (dtBRSReconciliationSummary.Rows.Count > 0)
                {
                    DataRow[] row10 = dtBRSReconciliationSummary.Select("RecID=" + "10");
                    if (row10.Count() > 0)
                        lblBalPerBB.Text = CommonCls.ConverToCommas(row10[0]["SummaryAmount"].ToString());
                    else
                        lblBalPerBB.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row20 = dtBRSReconciliationSummary.Select("RecID=" + "20");
                    if (row20.Count() > 0)
                        lblAddChqIssued.Text = CommonCls.ConverToCommas(row20[0]["SummaryAmount"].ToString());
                    else
                        lblAddChqIssued.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row30 = dtBRSReconciliationSummary.Select("RecID=" + "30");
                    if (row30.Count() > 0)
                        lbladdChqCredit.Text = CommonCls.ConverToCommas(row30[0]["SummaryAmount"].ToString());
                    else
                        lbladdChqCredit.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row31 = dtBRSReconciliationSummary.Select("RecID=" + "31");
                    if (row31.Count() > 0)
                        lblAddIntCredit.Text = CommonCls.ConverToCommas(row31[0]["SummaryAmount"].ToString());
                    else
                        lblAddIntCredit.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row37 = dtBRSReconciliationSummary.Select("RecID=" + "37");
                    if (row37.Count() > 0)
                    {
                        lblAddMisMatchDebSide.Text = CommonCls.ConverToCommas(row37[0]["SummaryAmount"].ToString());
                        trAddMisMatchDebSide.Visible = true;
                    }
                    else
                    {
                        lblAddMisMatchDebSide.Text = CommonCls.ConverToCommas(0);
                        trAddMisMatchDebSide.Visible = false;
                    }

                    DataRow[] row38 = dtBRSReconciliationSummary.Select("RecID=" + "38");
                    if (row38.Count() > 0)
                    {
                        lblAddMisMatchCreSide.Text = CommonCls.ConverToCommas(row38[0]["SummaryAmount"].ToString());
                        trAddMisMatchCreSide.Visible = true;
                    }
                    else
                    {
                        lblAddMisMatchCreSide.Text = CommonCls.ConverToCommas(0);
                        trAddMisMatchCreSide.Visible = false;
                    }

                    DataRow[] row39 = dtBRSReconciliationSummary.Select("RecID=" + "39");
                    if (row39.Count() > 0)
                        lblTot1.Text = CommonCls.ConverToCommas(row39[0]["SummaryAmount"].ToString());
                    else
                        lblTot1.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row40 = dtBRSReconciliationSummary.Select("RecID=" + "40");
                    if (row40.Count() > 0)
                        lblLessChqDepo.Text = CommonCls.ConverToCommas(row40[0]["SummaryAmount"].ToString());
                    else
                        lblLessChqDepo.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row50 = dtBRSReconciliationSummary.Select("RecID=" + "50");
                    if (row50.Count() > 0)
                        lbllessChqDebited.Text = CommonCls.ConverToCommas(row50[0]["SummaryAmount"].ToString());
                    else
                        lbllessChqDebited.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row51 = dtBRSReconciliationSummary.Select("RecID=" + "51");
                    if (row51.Count() > 0)
                        lblLessBankChaDeb.Text = CommonCls.ConverToCommas(row51[0]["SummaryAmount"].ToString());
                    else
                        lblLessBankChaDeb.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row52 = dtBRSReconciliationSummary.Select("RecID=" + "52");
                    if (row52.Count() > 0)
                        lblLessIntDeb.Text = CommonCls.ConverToCommas(row52[0]["SummaryAmount"].ToString());
                    else
                        lblLessIntDeb.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row57 = dtBRSReconciliationSummary.Select("RecID=" + "57");
                    if (row57.Count() > 0)
                    {
                        lblLessMisMatchDebSide.Text = CommonCls.ConverToCommas(row57[0]["SummaryAmount"].ToString());
                        trLessMisMatchDebSide.Visible = true;
                    }
                    else
                    {
                        lblLessMisMatchDebSide.Text = CommonCls.ConverToCommas(0);
                        trLessMisMatchDebSide.Visible = false;
                    }

                    DataRow[] row58 = dtBRSReconciliationSummary.Select("RecID=" + "58");
                    if (row58.Count() > 0)
                    {
                        lblLessMisMatchCreSide.Text = CommonCls.ConverToCommas(row58[0]["SummaryAmount"].ToString());
                        trLessMisMatchCreSide.Visible = true;
                    }
                    else
                    {
                        lblLessMisMatchCreSide.Text = CommonCls.ConverToCommas(0);
                        trLessMisMatchCreSide.Visible = false;
                    }

                    DataRow[] row59 = dtBRSReconciliationSummary.Select("RecID=" + "59");
                    if (row59.Count() > 0)
                        lblTot2.Text = CommonCls.ConverToCommas(row59[0]["SummaryAmount"].ToString());
                    else
                        lblTot2.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row60 = dtBRSReconciliationSummary.Select("RecID=" + "60");
                    if (row60.Count() > 0)
                        lblClBalAs.Text = CommonCls.ConverToCommas(row60[0]["SummaryAmount"].ToString());
                    else
                        lblClBalAs.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row69 = dtBRSReconciliationSummary.Select("RecID=" + "69");
                    if (row69.Count() > 0)
                        lblDiffBal.Text = CommonCls.ConverToCommas(row69[0]["SummaryAmount"].ToString());
                    else
                        lblDiffBal.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row70 = dtBRSReconciliationSummary.Select("RecID=" + "70");
                    if (row70.Count() > 0)
                        lblBalAsPerBB.Text = CommonCls.ConverToCommas(row70[0]["SummaryAmount"].ToString());
                    else
                        lblBalAsPerBB.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row80 = dtBRSReconciliationSummary.Select("RecID=" + "80");
                    if (row80.Count() > 0)
                        lblBalAsPerBs.Text = CommonCls.ConverToCommas(row80[0]["SummaryAmount"].ToString());
                    else
                        lblBalAsPerBs.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row89 = dtBRSReconciliationSummary.Select("RecID=" + "89");
                    if (row89.Count() > 0)
                        lblDiifInOpBal.Text = CommonCls.ConverToCommas(row89[0]["SummaryAmount"].ToString());
                    else
                        lblDiifInOpBal.Text = CommonCls.ConverToCommas(0);

                    DataRow[] row90 = dtBRSReconciliationSummary.Select("RecID=" + "90");
                    if (row90.Count() > 0)
                    {
                        if (row90[0]["SummaryAmount"].ToString() == "1")
                            lblMatchUnmatched.Text = "Matched";
                        else
                            lblMatchUnmatched.Text = "UnMatched";
                    }

                    lblAddTotal.Text = CommonCls.ConverToCommas(CommonCls.ConvertDecimalZero(lblAddChqIssued.Text) + CommonCls.ConvertDecimalZero(lbladdChqCredit.Text) +
                        CommonCls.ConvertDecimalZero(lblAddIntCredit.Text) + CommonCls.ConvertDecimalZero(lblAddMisMatchDebSide.Text) +
                        CommonCls.ConvertDecimalZero(lblAddMisMatchCreSide.Text));


                    lblLessTotal.Text = CommonCls.ConverToCommas(CommonCls.ConvertDecimalZero(lblLessChqDepo.Text) + CommonCls.ConvertDecimalZero(lbllessChqDebited.Text)
                        + CommonCls.ConvertDecimalZero(lblLessBankChaDeb.Text) + CommonCls.ConvertDecimalZero(lblLessIntDeb.Text) +
                        CommonCls.ConvertDecimalZero(lblLessMisMatchDebSide.Text) + CommonCls.ConvertDecimalZero(lblLessMisMatchCreSide.Text));

                    myModal.Style.Add("display", "block");
                    myDetailModal.Style.Add("display", "none");
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion

    #region Summary Detail

    //Add: Chq. Issued But Not Presented (LinkNo=1)
    protected void lknAddChqIssued_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lknAddChqIssued.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 1";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    //Add: Chq. Credited In Bank Statement Not In Bank Book (LinkNo=2)
    protected void lnkAddChqCredit_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkAddChqCredit.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 2";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    //Add: Amount MisMatch In Bank Book And Bank Statement (Credit Side) (LinkNo=3)
    protected void lnkAddMisMatchCreSide_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkAddMisMatchCreSide.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 3";
            grdMisMatchSummaryDetails.DataSource = dv31.ToTable();
            grdMisMatchSummaryDetails.DataBind();
        }
        CountChequeNoRows();
        GRDAmtMisMatchSummDetailHideAndShow();
    }

    //Amount MisMatch In Bank Book And Bank Statement (Debit Side) (LinkNo=4)
    protected void lnkAddMisMatchDebSide_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkAddMisMatchDebSide.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 4";
            grdMisMatchSummaryDetails.DataSource = dv31.ToTable();
            grdMisMatchSummaryDetails.DataBind();
        }
        CountChequeNoRows();
        GRDAmtMisMatchSummDetailHideAndShow();
    }

    //Add: Interest Credited By Bank Not In Bank Book (LinkNo=5)
    protected void lnkAddIntCredit_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkAddIntCredit.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 5";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    //Less: Chq. Deposited But Not Collected (LinkNo=6)
    protected void lnkLessChqDepo_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkLessChqDepo.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 6";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    //Less: Chq. Debited In Bank Statement Not In Bank Book (LinkNo=7)
    protected void lnkLessChqDebited_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkLessChqDebited.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 7";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    //Less: Bank Charges Debited By Bank Not In Bank Book (LinkNo=8)
    protected void lnkLessBankChaDeb_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkLessBankChaDeb.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 8";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    //Less: Amount MisMatch In Bank Book And Bank Statement (Credit Side) (LinkNo=9)
    protected void lnkLessMisMatchCreSide_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkLessMisMatchCreSide.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 9";
            grdMisMatchSummaryDetails.DataSource = dv31.ToTable();
            grdMisMatchSummaryDetails.DataBind();
        }
        CountChequeNoRows();
        GRDAmtMisMatchSummDetailHideAndShow();
    }

    //Less: Amount MisMatch In Bank Book And Bank Statement (Debit Side) (LinkNo=10)
    protected void lnkLessMisMatchDebSide_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkLessMisMatchDebSide.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 10";
            grdMisMatchSummaryDetails.DataSource = dv31.ToTable();
            grdMisMatchSummaryDetails.DataBind();
        }
        CountChequeNoRows();
        GRDAmtMisMatchSummDetailHideAndShow();
    }

    //Less: Interest Debited By Bank Not In Bank Book (LinkNo=11)
    protected void lnkLessIntDeb_Click(object sender, EventArgs e)
    {
        myDetailModal.Style.Add("display", "block");

        ClearLabelAndNullDataTable();

        lblParticularSummName.Text = lnkLessIntDeb.Text;
        lblSummDetailsBankName.Text = ddlBankAcc.SelectedItem.Text;
        lblSummDetailAsOnDate.Text = "As On " + CommonCls.ConvertDateDB(txtToDate.Text);

        if (VsdtSummaryDetails != null || VsdtSummaryDetails.Rows.Count > 0)
        {
            DataView dv31 = new DataView(VsdtSummaryDetails);
            dv31.RowFilter = "LinkNo = 11";
            grdSummDetails.DataSource = dv31.ToTable();
            grdSummDetails.DataBind();
        }
        CountChequeNoRows();
        GRDSummDetailHideAndShow();
    }

    protected void grdSummDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblVoucherDate = (Label)e.Row.FindControl("lblVoucherDate");

                lblVoucherDate.Text = CommonCls.ConvertDateDB(lblVoucherDate.Text);
                lblAmount.Text = CommonCls.ConverToCommas(lblAmount.Text);

                lblSummDetailTotalAmt.Text = CommonCls.ConverToCommas(CommonCls.ConvertDecimalZero(lblSummDetailTotalAmt.Text) + CommonCls.ConvertDecimalZero(lblAmount.Text));
            }
        }
    }

    protected void grdMisMatchSummaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblVoucherDate = (Label)e.Row.FindControl("lblVoucherDate");

                Label lblBSAmount = (Label)e.Row.FindControl("lblBSAmount");
                Label lblDiffAmount = (Label)e.Row.FindControl("lblDiffAmount");

                lblBSAmount.Text = CommonCls.ConverToCommas(lblBSAmount.Text);
                lblDiffAmount.Text = CommonCls.ConverToCommas(lblDiffAmount.Text);

                lblVoucherDate.Text = CommonCls.ConvertDateDB(lblVoucherDate.Text);
                lblAmount.Text = CommonCls.ConverToCommas(lblAmount.Text);

                lblAmtMisMatchSummDetailTotalAmt.Text = CommonCls.ConverToCommas(CommonCls.ConvertDecimalZero(lblAmtMisMatchSummDetailTotalAmt.Text) + CommonCls.ConvertDecimalZero(lblAmount.Text));
                lblSummDetailToTBSAmt.Text = CommonCls.ConverToCommas(CommonCls.ConvertDecimalZero(lblSummDetailToTBSAmt.Text) + CommonCls.ConvertDecimalZero(lblBSAmount.Text));
                lblSummDetailToTDiffAmt.Text = CommonCls.ConverToCommas(CommonCls.ConvertDecimalZero(lblSummDetailToTDiffAmt.Text) + CommonCls.ConvertDecimalZero(lblDiffAmount.Text));
            }
        }
    }

    void CountChequeNoRows()
    {
        int chequeRowCount = 0;
        int chequeMisMatchCount = 0;

        foreach (GridViewRow gvRow in grdSummDetails.Rows)
        {
            if (CommonCls.ConvertIntZero((gvRow.FindControl("lblChequeNo") as Label).Text) >= 0)
                chequeRowCount = chequeRowCount + 1;
        }
        lblChequeNoCount.Text = chequeRowCount.ToString();

        //Amount MisMatch
        foreach (GridViewRow gvRow in grdMisMatchSummaryDetails.Rows)
        {
            if (CommonCls.ConvertIntZero((gvRow.FindControl("lblChequeNo") as Label).Text) >= 0)
                chequeMisMatchCount = chequeMisMatchCount + 1;
        }
        lblAmtMisMatchChequeNo.Text = chequeMisMatchCount.ToString();
    }

    void ClearLabelAndNullDataTable()
    {
        grdSummDetails.DataSource = grdMisMatchSummaryDetails.DataSource = new DataTable();
        grdSummDetails.DataBind(); grdMisMatchSummaryDetails.DataBind();
        lblSummDetailTotalAmt.Text = lblChequeNoCount.Text = lblAmtMisMatchChequeNo.Text = lblAmtMisMatchSummDetailTotalAmt.Text =
            lblSummDetailToTBSAmt.Text = lblSummDetailToTDiffAmt.Text = "";
    }

    void GRDSummDetailHideAndShow()
    { 
        grdMisMatchSummaryDetails.Visible = trAmtMisMatch.Visible = false;
        grdSummDetails.Visible = trAmtSummDetails.Visible = true;
    }

    void GRDAmtMisMatchSummDetailHideAndShow()
    {
        grdSummDetails.Visible = trAmtSummDetails.Visible = false;
        grdMisMatchSummaryDetails.Visible = trAmtMisMatch.Visible = true;
    }

    #endregion

    #region Verification Password

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblPassMsg.Text = lblPassMsg.CssClass = "";

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                ShowPassMessage("Enter Password.", false);
                pnlPassword.Visible = true;
                pnlPassword.Focus();
                return;
            }

            string companyName = GlobalSession.OrgName;
            string OrgName = new String(companyName.Where(Char.IsLetter).ToArray());
            string firstFour = OrgName.Substring(0, 4).ToUpper();
            string orgPassword = firstFour + "@257";
            string pass = txtPassword.Text.ToUpper().Trim();
            if (pass == orgPassword)
            {
                txtPassword.Text = "";
                pnlPassword.Visible = false;
                ddlBankAcc.Focus();
            }
            else
            {
                pnlPassword.Visible = true;
                pnlPassword.Focus();
                ShowPassMessage("Wrong Password, Please Try Again.", false);
                txtPassword.Text = "";
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ShowPassMessage(string Message, bool type)
    {
        lblPassMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblPassMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    #endregion

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void btnASYes_Click(object sender, EventArgs e)
    {
        pnlAfterSucess.Visible = false;
        btnSummary_Click(sender, e);
       
    }

    protected void btnASNo_Click(object sender, EventArgs e)
    {
        clearAll();
        pnlAfterSucess.Visible = false;
    }
}