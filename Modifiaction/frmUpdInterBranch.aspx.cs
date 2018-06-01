using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifiaction_frmUpdInterBranch : System.Web.UI.Page
{
    DataTable dtgrdview;
    DataSet dtgridview;

    DataTable dtAccGstin;
    UpdateInterBranchModel objUpdInterBranch;
    //public static int RangeInd = 0;
    int RowIndex
    {
        get { return ViewState["rowIndex"] == null ? -1 : Convert.ToInt16(ViewState["rowIndex"]); }
        set { ViewState["rowIndex"] = value; }
    }


    int OldRange
    {
        get { return (int)ViewState["oldRange"]; }
        set { ViewState["oldRange"] = value; }
    }



    DataTable VsdtRange
    {
        get { return (DataTable)ViewState["dtRange"]; }
        set { ViewState["dtRange"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GlobalSession.YrCD == null)
        {
            Response.Redirect("frmLogin.aspx");
        }
        if (!IsPostBack)
        {
            txtSearchVNo.Focus();
            ViewState["VchType"] = 4;
            ViewState["RangeInd"] = "";
            //LoadBankAccount();
            //LoadAccountHead();
            //LoadNarration();
            LoadLastVoucherNo();
            LoadBranchName();
            BindCancelReason();
            SetPayMode();
        }
        lblMsg.CssClass = "";
        lblMsg.Text = "";
    }
    private void BindCancelReason()
    {
        try
        {

            objUpdInterBranch = new UpdateInterBranchModel();
            objUpdInterBranch.Ind = 100;
            string uri = string.Format("Master/Master");
            DataTable dtCancelReason = CommonCls.ApiPostDataTable(uri, objUpdInterBranch);
            if (dtCancelReason.Rows.Count > 0)
            {
                ddlCancelReason.DataSource = dtCancelReason;
                ddlCancelReason.DataTextField = "CancelReason";
                ddlCancelReason.DataValueField = "CancelID";
                ddlCancelReason.DataBind();
                if (dtCancelReason.Rows.Count > 1)
                {
                    ddlCancelReason.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
            else
            {

            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchVNo.Text == "" || Convert.ToInt32(txtSearchVNo.Text) == 0)
            {
                txtSearchVNo.Focus();
                ShowMessage("Please Enter Voucher No.", false);
                return;
            }

            objUpdInterBranch = new UpdateInterBranchModel()
           {
               Ind = 5,
               OrgID = GlobalSession.OrgID,
           };
            objUpdInterBranch.DocNo = Convert.ToInt32(txtSearchVNo.Text);
            string uri = string.Format("UpdateInterBranch/SearchInterBranch");
            DataSet dtgridview = CommonCls.ApiPostDataSet(uri, objUpdInterBranch);
            if (dtgridview.Tables[0].Rows.Count > 0)
            {
                if (dtgridview.Tables[0].Rows[0]["CancelInd"].ToString() == "1")
                {
                    ShowMessage("This Voucher No. Already Canceled.", false);
                    txtSearchVNo.Focus();
                    return;
                }

                txtSearchVNo.Enabled = true;
                ViewState["dtgridview"] = dtgridview;
                ViewState["dtgrdview"] = dtgridview.Tables[1];
                if (dtgridview.Tables[1].Rows.Count > 0)
                {
                    OldRange = CommonCls.ConvertIntZero(dtgridview.Tables[1].Rows[0]["AccCode"].ToString());
                    ddlBranchList.SelectedValue = dtgridview.Tables[1].Rows[0]["CityBranchID"].ToString();
                    gvInterBranch.DataSource = dtgridview.Tables[1];
                    gvInterBranch.DataBind();
                }
                txtVoucherDate.Text = Convert.ToDateTime(dtgridview.Tables[0].Rows[0]["VoucharDate"].ToString()).ToString("dd/MM/yyyy");
                txtNarration.Items.Insert(0, dtgridview.Tables[0].Rows[0]["Narration"].ToString());
                if (dtgridview.Tables[0].Rows.Count > 0)
                {
                    ddlBankAccount.SelectedValue = dtgridview.Tables[0].Rows[0]["CashBankCode"].ToString();
                }
                if (CommonCls.ConvertIntZero(dtgridview.Tables[0].Rows[0]["ChequeNo"].ToString()) != 0)
                {
                    ddlPayMode.SelectedValue = "Cheque";
                    SetPayMode();
                    txtReceivedNo.Text = dtgridview.Tables[0].Rows[0]["ChequeNo"].ToString();
                    txtReceivedDate.Text = Convert.ToDateTime(dtgridview.Tables[0].Rows[0]["ChequeDate"].ToString()).ToString("dd/MM/yyyy");

                }
                else if (!string.IsNullOrEmpty(dtgridview.Tables[0].Rows[0]["UtrNo"].ToString()) && dtgridview.Tables[0].Rows[0]["UtrNo"].ToString() != "0")
                {
                    ddlPayMode.SelectedValue = "UTR";
                    SetPayMode();
                    txtReceivedNo.Text = dtgridview.Tables[0].Rows[0]["UtrNo"].ToString();
                    txtReceivedDate.Text = Convert.ToDateTime(dtgridview.Tables[0].Rows[0]["UtrDate"].ToString()).ToString("dd/MM/yyyy");
                }

                CalculateTotalInvoiceAmount();
                txtNarration.SelectedIndex = 0;

                ddlBranchList_SelectedIndexChanged(sender, e);
                txtSearchVNo.Enabled = false;
                ddlBranchList.Enabled = false;
                ddlBankAccount.Enabled = false;
                btnSearch.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                ShowMessage("Voucher Not Found.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    private void LoadBranchName()
    {
        try
        {
            objUpdInterBranch = new UpdateInterBranchModel()
            {
                Ind = 2,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
            };
            string uri = string.Format("UpdateInterBranch/LoadBranchList");
            DataSet dtBranchList = CommonCls.ApiPostDataSet(uri, objUpdInterBranch);
            if (dtBranchList.Tables[0].Rows.Count > 0)
            {
                ddlBranchList.DataSource = dtBranchList.Tables[0];
                ddlBranchList.DataTextField = "BranchName";
                ddlBranchList.DataValueField = "BranchID";
                ddlBranchList.DataBind();

                if (dtBranchList.Tables[0].Rows.Count > 1)
                    ddlBranchList.Items.Insert(0, new ListItem("-- Select --", "0000"));
            }

            if (dtBranchList.Tables[1].Rows.Count > 0)
            {
                ddlBankAccount.DataSource = dtBranchList.Tables[1];
                ddlBankAccount.DataTextField = "AccName";
                ddlBankAccount.DataValueField = "AccCode";
                ddlBankAccount.DataBind();

                if (dtBranchList.Tables[0].Rows.Count > 1)
                    ddlBankAccount.Items.Insert(0, new ListItem("-- Select --", "0000"));
            }
            if (dtBranchList.Tables[2].Rows.Count > 0)
            {
                VsdtRange = dtBranchList.Tables[2];
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void SetPayMode()
    {

        if (ddlPayMode.SelectedValue == "Cheque")
        {
            lblPayModeNo.Text = "Cheque No.";
            lblPayModeDate.Text = "Cheque Date";
            txtReceivedNo.MaxLength = 8;
            txtReceivedNo.CssClass = "numberonly";
        }
        else
        {
            lblPayModeNo.Text = "UTR No.";
            lblPayModeDate.Text = "UTR Date";
            txtReceivedNo.MaxLength = 16;
            txtReceivedNo.CssClass = "";//.Replace("numberonly", "");
        }
        txtReceivedNo.Text = txtReceivedDate.Text = "";
    }

    void LoadLastVoucherNo() // Last Voucher No Insert
    {
        try
        {
            objUpdInterBranch = new UpdateInterBranchModel()
            {
                Ind = 4,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
            };
            string uri = string.Format("UpdateInterBranch/LastVoucherNo");
            DataTable dtVoucher = CommonCls.ApiPostDataTable(uri, objUpdInterBranch);
            if (dtVoucher.Rows.Count > 0)
            {
                if (dtVoucher.Rows[0]["LastNo"].ToString() == "0")
                {
                    return;
                }
                txtLastVoucherNo.Text = dtVoucher.Rows[0][0].ToString();
                lblInvoiceAndDate.Text = "Last Voucher No. & Date : " + dtVoucher.Rows[0]["LastNo"].ToString() + " - " + Convert.ToDateTime(dtVoucher.Rows[0]["LastDate"].ToString()).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    public void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void txtAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }

            if (Convert.ToInt32(txtAccountHead.SelectedItem.Value) <= 0)
            {
                ShowMessage("Account Head Not Available", false);
                return;
            }


            dtAccGstin = (DataTable)(ViewState["dtAccGstin"]);
            if (dtAccGstin != null)
            {
                DataView DVAccGstin = new DataView(dtAccGstin);
                DVAccGstin.RowFilter = "AccCode=" + txtAccountHead.SelectedItem.Value;
                DataTable DTFilter = DVAccGstin.ToTable();

                if ((DTFilter.Rows.Count > 0) && (DTFilter.Rows.Count == 1))
                {
                    ddlGSTINNo.DataSource = DTFilter;
                    ddlGSTINNo.DataTextField = "GSTIN";
                    ddlGSTINNo.DataBind();
                    ddlGSTINNo.SelectedIndex = 0;
                }
                else if (DTFilter.Rows.Count > 1)
                {
                    ddlGSTINNo.DataSource = DTFilter;
                    ddlGSTINNo.DataTextField = "GSTIN";
                    ddlGSTINNo.DataBind();
                    ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0000"));
                    ddlGSTINNo.SelectedIndex = 0;
                    ddlGSTINNo.Focus();
                }
                else
                {
                    ddlGSTINNo.DataSource = null;
                    ddlGSTINNo.DataBind();
                }
            }
            objUpdInterBranch = new UpdateInterBranchModel()
            {
                Ind = 6,
                OrgID = GlobalSession.OrgID,
                BrID = GlobalSession.BrID,
                YrCD = GlobalSession.YrCD,
                VchType = Convert.ToInt32(ViewState["VchType"]),
                AccCode = Convert.ToInt32(txtAccountHead.SelectedItem.Value),
            };
            string uri = string.Format("UpdateInterBranch/PartySelect");
            DataSet dsPartySelect = CommonCls.ApiPostDataSet(uri, objUpdInterBranch);
            if (dsPartySelect.Tables.Count > 0)
            {
                divPartySelect.Visible = true; //Party Selection Section Visible
                btnRegToggle.Visible = false;   //Outstanding Bill UnVisible
                ddlSecondaryParty.Visible = false; //SecondaryParty UnVisible

                if (dsPartySelect.Tables[0].TableName == "SecondaryParty") //SecondaryParty Visible
                {
                    ddlSecondaryParty.Visible = true;
                    ddlSecondaryParty.DataSource = dsPartySelect;
                    ddlSecondaryParty.DataTextField = "PartyName";
                    ddlSecondaryParty.DataValueField = "PartyID";
                    ddlSecondaryParty.DataBind();
                }
                else if (dsPartySelect.Tables[0].TableName == "OutstandingBill") //Outstanding Bill Visible
                {
                    btnRegToggle.Visible = true;
                    CbOutstandingBill.DataSource = dsPartySelect;
                    CbOutstandingBill.DataTextField = "BillAmt";
                    CbOutstandingBill.DataValueField = "BillNo";
                    CbOutstandingBill.DataBind();
                }
                else
                {
                    divPartySelect.Visible = false;
                    ddlSecondaryParty.DataSource = CbOutstandingBill.DataSource = null;
                    ddlSecondaryParty.DataBind(); CbOutstandingBill.DataBind();
                }
            }
            ddlGSTINNo.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(lblMsg.Text, false);
        }
    }
    protected void ddlBranchList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                objUpdInterBranch = new UpdateInterBranchModel()
                {
                    Ind = 1,
                    OrgID = GlobalSession.OrgID,
                    BrID = Convert.ToInt32(ddlBranchList.SelectedValue),
                    YrCD = GlobalSession.YrCD,

                };

                string uri = string.Format("UpdateInterBranch/AccountHead");
                DataTable dtAccGstin = CommonCls.ApiPostDataTable(uri, objUpdInterBranch);
                if (dtAccGstin.Rows.Count > 0)
                {
                    ViewState["dtAccGstin"] = dtAccGstin;
                    DataView dvAccCode = new DataView(dtAccGstin);
                    DataTable dtAccList = dvAccCode.ToTable(true, "AccCode", "AccName");

                    txtAccountHead.DataSource = dtAccList;
                    txtAccountHead.DataTextField = "AccName";
                    txtAccountHead.DataValueField = "AccCode";
                    txtAccountHead.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, false);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            // lblMsg.Text = lblMsg.CssClass = "";
            if (ddlBankAccount.SelectedItem.Value == txtAccountHead.SelectedValue) // Check Account Head & Bank Account.
            {
                txtAccountHead.Focus();
                ShowMessage("Account Head And Bank Account Can Not Be Same.", false);
                return;
            }

            if (string.IsNullOrEmpty(txtAccountHead.Text)) // For Account Head Not Null Or Empty
            {
                txtAccountHead.Focus();
                ShowMessage("Enter Account Head.", false);
                return;
            }

            if (string.IsNullOrEmpty(ddlCrOrDr.SelectedValue)) // For Dr/Cr Amount
            {
                ddlCrOrDr.Focus();
                ShowMessage("Select Dr/Cr Amount.", false);
                return;
            }

            try
            {
                if (txtAccountHead.SelectedItem.Value == null || Convert.ToInt32(txtAccountHead.SelectedItem.Value) == 0) // For Account Head Code Not Null Or Empty
                {
                    txtAccountHead.Focus();
                    ShowMessage("Account Value Not Available", false);
                    return;
                }
            }
            catch (Exception)
            {
                txtAccountHead.Focus();
                ShowMessage("Account Value Not Available", false);
                return;
            }

            if (ddlGSTINNo.Items.Count > 0)
            {
                if (ddlGSTINNo.SelectedItem.Value == "0000")
                {
                    ddlGSTINNo.Focus();
                    ShowMessage("Please Select GSTIN.", false);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) <= 0)
            {
                txtAmount.Focus();
                ShowMessage("Please Enter Amount.", false);
                return;
            }

            if (!string.IsNullOrEmpty(txtInVoiceNo.Text))
            {
                if (string.IsNullOrEmpty(txtInvoiceDate.Text))
                {
                    txtInvoiceDate.Focus();
                    ShowMessage("Please Select Invoice Date.", false);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Invoice Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                    txtInvoiceDate.Focus();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtInvoiceDate.Text))
            {
                if (string.IsNullOrEmpty(txtInVoiceNo.Text))
                {
                    txtInVoiceNo.Focus();
                    ShowMessage("Please Select Invoice No.", false);
                    return;
                }
                bool ValidDate = CommonCls.CheckFinancialYrDate(txtInvoiceDate.Text, "01/01/2000", DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ValidDate) // For Voucher Date Between Financial Year.
                {
                    ShowMessage("Invoice Date Should Be Within Financial Year Date Or Not More Than Todays Date!", false);
                    txtInvoiceDate.Focus();
                    return;
                }
            }

            // Disable Select If Alerady Exist AccountHead
            string InvoiceNo = CommonCls.ConvertIntZero(txtInVoiceNo.Text) == 0 ? "" : txtInVoiceNo.Text;
            dtgrdview = (DataTable)ViewState["dtgrdview"];
            if (dtgrdview != null && dtgrdview.Rows.Count > 0)
            {

                if (RowIndex >= 0)
                {
                    dtgrdview.Rows[RowIndex].Delete();
                }
                DataRow[] rows = dtgrdview.Select("AccCode=" + txtAccountHead.SelectedItem.Value + " AND  InvoiceNo='" + InvoiceNo + "'");
                if (rows.Count() >= 1)
                {

                    if (InvoiceNo == string.Empty)
                    {
                        txtAccountHead.Focus();
                        ShowMessage("This Account Head With Blank Or 0 Invoice No. Already Exist.", false);
                        return;
                    }
                    txtAccountHead.Focus();
                    ShowMessage("This Account Head With Given Invoice No. Already Exist.", false);
                    return;
                }
            }

            if (ViewState["dtgrdview"] == null)
            {
                CreatGridDt();
            }
            else
            {
                dtgrdview = (DataTable)ViewState["dtgrdview"];
            }

            int Isvalid = 0;
            int RangeFrom = CommonCls.ConvertIntZero(VsdtRange.Rows[0]["BankFrom"].ToString());
            int RangeTo = CommonCls.ConvertIntZero(VsdtRange.Rows[0]["BankTo"].ToString());

            //When no rows in grid
            if (gvInterBranch.Rows.Count == 0)
            {
                if (gvInterBranch.Rows.Count == 0)
                {
                    Isvalid = 0;

                    if (CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) > RangeFrom && CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) < RangeTo)
                    {
                        ViewState["RangeInd"] = "1";
                    }
                    else
                    {
                        ViewState["RangeInd"] = "0";
                    }

                }
                else
                {
                    if (gvInterBranch.Rows.Count > 0)
                    {
                        if (ViewState["RangeInd"].ToString() == "1" && (CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) < RangeFrom || CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) > RangeTo))
                        {
                            ShowMessage("You Can't Use Bank And Other Than Bank Accounts On Same Voucher.", false);
                            Isvalid = 1;
                            txtAccountHead.Focus();
                        }
                        else if (ViewState["RangeInd"].ToString() == "0" && (CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) > RangeFrom
                            && CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) < RangeTo))
                        {
                            ShowMessage("You Can't Use Bank And Other Than Bank Accounts On Same Voucher.", false);
                            Isvalid = 1;
                            txtAccountHead.Focus();
                        }
                        //else
                        //{
                        //    ShowMessage("", false);
                        //    Isvalid = 0;
                        //}
                    }
                }
            }
            else
            {
                //when rows in grid
                if (gvInterBranch.Rows.Count != 0)
                {
                    if (OldRange > RangeFrom && OldRange < RangeTo)
                    {
                        ViewState["RangeInd"] = "1";
                    }
                    else
                    {
                        ViewState["RangeInd"] = "0";
                    }
                }
                //else
                //{
                if (gvInterBranch.Rows.Count > 0)
                {
                    if (ViewState["RangeInd"].ToString() == "1" && (CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) < RangeFrom || CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) > RangeTo))
                    {
                        ShowMessage("You Can't Use Bank And Other Than Bank Accounts On Same Voucher.", false);
                        Isvalid = 1;
                        txtAccountHead.Focus();
                    }
                    else if (ViewState["RangeInd"].ToString() == "0" && (CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) > RangeFrom
                        && CommonCls.ConvertIntZero(txtAccountHead.SelectedValue) < RangeTo))
                    {
                        ShowMessage("You Can't Use Bank And Other Than Bank Accounts On Same Voucher.", false);
                        Isvalid = 1;
                        txtAccountHead.Focus();

                    }
                    //else
                    //{
                    //    ShowMessage("", false);
                    //    Isvalid = 0;
                    //}
                }
                //    }
            }
            if (Isvalid == 0)
            {
                DataRow dr = dtgrdview.NewRow();
                dr["AccName"] = txtAccountHead.SelectedItem.Text;
                dr["AccCode"] = txtAccountHead.SelectedItem.Value;
                dr["GSTIN"] = string.IsNullOrEmpty(ddlGSTINNo.SelectedItem.Value) ? "" : ddlGSTINNo.SelectedItem.Value;
                //if (divPartySelect.Visible == true)
                //{
                //    dr["PartyID"] = "0";
                //    //ddlSecondaryParty.Items.Count > 0 ?
                //    //            !string.IsNullOrEmpty(ddlSecondaryParty.SelectedItem.Value) ?
                //    //            ddlSecondaryParty.SelectedItem.Value : "" : "";
                //    //string BillNos = "";
                //    //for (int i = 0; i < CbOutstandingBill.Items.Count; i++)
                //    //{
                //    //    if (CbOutstandingBill.Items[i].Selected)
                //    //    {
                //    //        if (i == 0)
                //    //            BillNos = CbOutstandingBill.Items[i].Value;
                //    //        else
                //    //            BillNos += "," + CbOutstandingBill.Items[i].Value;
                //    //    }
                //    //} 
                //}
                //dr["BillNos"] = BillNos;
                dr["InvoiceNo"] = InvoiceNo;
                dr["InvoiceDate"] = txtInvoiceDate.Text;
                dr["Amount"] = txtAmount.Text;
                dr["AmountType"] = ddlCrOrDr.SelectedItem.Value;

                if (RowIndex >= 0)
                {
                    //  dtgrdview.Rows.RemoveAt(RowIndex);
                    dtgrdview.Rows.InsertAt(dr, RowIndex);
                    RowIndex = -1;
                }
                else
                {
                    dtgrdview.Rows.Add(dr);
                }
                gvInterBranch.DataSource = ViewState["dtgrdview"] = dtgrdview;
                gvInterBranch.DataBind();

                //ViewState["dtgrdview"] = dtgrdview;
                //gvInterBranch.DataSource = dtgrdview;
                //gvInterBranch.DataBind();
                CalculateTotalInvoiceAmount();
                ClearAll();
                txtAccountHead.Focus();
                ddlBranchList.Enabled = false;
                ddlBankAccount.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    void ClearAll()
    {
        txtInVoiceNo.Text =
        txtInvoiceDate.Text =
        txtAmount.Text =
            lblMsg.Text = string.Empty;

        ddlGSTINNo.DataSource = new DataTable(); ddlGSTINNo.DataBind();
        ddlCrOrDr.ClearSelection();
        txtAccountHead.ClearSelection();
        ddlBankAccount.Enabled = true;
        ddlBranchList.Enabled = true;

    }

    void CalculateTotalInvoiceAmount() // For Invoice Amount Dr - CR
    {
        decimal crAmount = 0;
        decimal drAmount = 0;
        dtgrdview = (DataTable)ViewState["dtgrdview"];
        for (int rowIndex = 0; dtgrdview.Rows.Count > rowIndex; rowIndex++)
        {
            string DrCr = dtgrdview.Rows[rowIndex]["AmountType"].ToString();
            if (DrCr == "Cr")
            {
                crAmount = crAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex]["Amount"]);
            }
            else
            {
                drAmount = drAmount + Convert.ToDecimal(dtgrdview.Rows[rowIndex]["Amount"]);
            }
        }
        txtInvoiceTotalAmount.Text = Convert.ToString(crAmount - drAmount);
    }


    DataTable CreatGridDt() // Create Grid Structure
    {
        dtgrdview = new DataTable();
        dtgrdview.Columns.Add("AccCode", typeof(string));
        dtgrdview.Columns.Add("AccName", typeof(string));
        dtgrdview.Columns.Add("PartyID", typeof(string));
        dtgrdview.Columns.Add("BillNos", typeof(string));
        dtgrdview.Columns.Add("GSTIN", typeof(string));
        dtgrdview.Columns.Add("InvoiceNo", typeof(string));
        dtgrdview.Columns.Add("InvoiceDate", typeof(string));
        dtgrdview.Columns.Add("Amount", typeof(string));
        dtgrdview.Columns.Add("AmountType", typeof(string));
        return dtgrdview;
    }

    protected void gvInterBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditRow")
            {
                DataSet dtgridview = (DataSet)ViewState["dtgridview"];
                DataRow drgridview = dtgridview.Tables[1].Rows[rowIndex];

                txtAccountHead.SelectedValue = drgridview["AccCode"].ToString();

                FillGSTIN();

                ddlGSTINNo.SelectedValue = drgridview["GSTIN"].ToString();
                txtInVoiceNo.Text = CommonCls.ConvertIntZero(drgridview["InvoiceNo"].ToString()) != 0 ? drgridview["InvoiceNo"].ToString() : "";
                txtInvoiceDate.Text = drgridview["InvoiceDate"].ToString();
                txtAmount.Text = drgridview["Amount"].ToString();
                ddlCrOrDr.SelectedValue = drgridview["AmountType"].ToString();
                RowIndex = rowIndex;
                gvInterBranch.DataSource = ViewState["dtgridview"] = dtgridview;
                gvInterBranch.DataBind();

                foreach (GridViewRow row in gvInterBranch.Rows)
                {
                    if (row.RowIndex != rowIndex)
                    {
                        LinkButton editButton = (LinkButton)row.FindControl("lnkEdit");
                        editButton.Enabled = false;
                        LinkButton deleteButton = (LinkButton)row.FindControl("btnDelete");
                        deleteButton.Enabled = false;
                    }
                    else
                    {
                        row.BackColor = Color.Bisque;
                    }
                }
            }
            else if (e.CommandName == "RemoveRow")
            {

                //  dtgridview = (DataSet)ViewState["dtgridview"];
                dtgrdview = (DataTable)ViewState["dtgrdview"];
                dtgrdview.Rows[rowIndex].Delete();
                gvInterBranch.DataSource = ViewState["dtgrdview"] = dtgrdview;
                gvInterBranch.DataBind();
            }
            CalculateTotalInvoiceAmount();
            txtAccountHead.Focus();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    void FillGSTIN()
    {
        dtAccGstin = (DataTable)(ViewState["dtAccGstin"]);
        DataView DVAccGstin = new DataView(dtAccGstin);
        DVAccGstin.RowFilter = "AccCode=" + txtAccountHead.SelectedItem.Value;
        DataTable DTFilter = DVAccGstin.ToTable();

        if ((DTFilter.Rows.Count > 0) && (DTFilter.Rows.Count == 1))
        {
            ddlGSTINNo.DataSource = DTFilter;
            ddlGSTINNo.DataTextField = "GSTIN";
            ddlGSTINNo.DataBind();
            ddlGSTINNo.SelectedIndex = 0;
        }
        else if (DTFilter.Rows.Count > 1)
        {
            ddlGSTINNo.DataSource = DTFilter;
            ddlGSTINNo.DataTextField = "GSTIN";
            ddlGSTINNo.DataBind();
            ddlGSTINNo.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlGSTINNo.SelectedIndex = 0;
            ddlGSTINNo.Focus();
        }
        else
        {
            ddlGSTINNo.DataSource = null;
            ddlGSTINNo.DataBind();
        }
        ddlGSTINNo.Focus();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

        txtSearchVNo.Enabled = true;
        btnSearch.Enabled = true;
        txtSearchVNo.Text = "";
        ddlBranchList.ClearSelection();
        ClearOnSave();
        ClearAll();
    }

    void ClearOnSave()
    {
        ClearAll();
        txtNarration.ClearSelection();
        txtNarration.Dispose();
        gvInterBranch.DataSource = ViewState["dtgrdview"] = null;
        gvInterBranch.DataBind();
        txtSearchVNo.Enabled = true;
        btnSearch.Enabled = true;
        txtSearchVNo.Text = "";

        txtVoucherDate.Text =
           txtReceivedDate.Text =
           txtReceivedNo.Text =
           txtInvoiceTotalAmount.Text = "";
        ddlBankAccount.ClearSelection();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CommonCls.CheckGUIDIsValid())
            {
                return;
            }
            if (ViewState["dtgrdview"] == null || ((DataTable)ViewState["dtgrdview"]).Rows.Count <= 0)
            {
                txtVoucherDate.Focus();
                ShowMessage("Insert Voucher Details!", false);
                return;
            }

            if (string.IsNullOrEmpty(txtVoucherDate.Text))
            {
                txtVoucherDate.Focus();
                ShowMessage("Enter Voucher Date!", false);
                return;
            }

            if (ddlBranchList.SelectedValue == "0000")
            {
                ddlBranchList.Focus();
                ShowMessage("Select City Branch Name", false);
                return;
            }

            bool ValidDate = CommonCls.CheckFinancialYrDate(txtVoucherDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!ValidDate)
            {
                txtVoucherDate.Focus();
                ShowMessage("Voucher Date Should Be Within Financial Year Date!", false);
                return;
            }

            if (Convert.ToInt32(ddlBankAccount.SelectedItem.Value) <= 0) // For Bank Account Selection Cumpulsory.
            {
                ddlBankAccount.Focus();
                ShowMessage("Select Bank Account!", false);
                return;
            }

            if (!string.IsNullOrEmpty(txtReceivedNo.Text))
            {
                if (string.IsNullOrEmpty(txtReceivedDate.Text))
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Enter Cheque Date.", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("Enter RTGS/NEFT Date", false);
                        return;
                    }
                }

                bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ReceivedDate) // For Voucher Date Between Financial Year.
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtReceivedDate.Text))
            {
                if (string.IsNullOrEmpty(txtReceivedNo.Text))
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Enter Cheque No.", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("Enter RTGS/NEFT No.", false);
                        return;
                    }
                }

                bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                if (!ReceivedDate) // For Voucher Date Between Financial Year.
                {
                    if (ddlPayMode.SelectedValue == "Cheque")
                    {
                        ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false);
                        return;
                    }
                    else if (ddlPayMode.SelectedValue == "UTR")
                    {
                        ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false);
                        return;
                    }
                }
            }
            dtgrdview = (DataTable)ViewState["dtgrdview"];
            DataTable dtBranch = InterBranchSchema();

            foreach (DataRow item in dtgrdview.Rows)
            {
                DataRow dr = dtBranch.NewRow();                                                  //Data Activity Indication
                dr["OrgID"] = GlobalSession.OrgID; //Company/End User ID
                dr["BrID"] = GlobalSession.BrID;   //Branch ID
                dr["VchType"] = 0;                              //Document/Voucher Type
                dr["YrCD"] = GlobalSession.YrCD;  //Financial Year Code (2017-17)
                dr["DocDate"] = CommonCls.ConvertToDate(txtVoucherDate.Text);//Voucher Date
                dr["DocNo"] = string.IsNullOrEmpty(txtLastVoucherNo.Text) ? 0 : Convert.ToInt32(txtLastVoucherNo.Text);
                dr["AccCode"] = Convert.ToInt32(item["AccCode"]);  //Selected Account Head Code
                //  dr["AccGst"] = item["GSTIN"].ToString();                         //Selected Account Head GSTIN No. (If Available)
                dr["AccCode2"] = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);  //Selected Bank Account Code
                dr["InterBrID"] = Convert.ToInt32(ddlBranchList.SelectedItem.Value);  //Selected Bank Account Code
                dr["BankCode"] = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);  //Selected Bank Account Code


                if (!string.IsNullOrEmpty(item["InvoiceNo"].ToString()))
                {
                    dr["RefNo"] = Convert.ToInt32(item["InvoiceNo"]); //Invoice No.
                    dr["RefDate"] = !string.IsNullOrEmpty(item["InvoiceDate"].ToString()) ? CommonCls.ConvertToDate(item["InvoiceDate"].ToString()) : ""; //Invoice Date
                }

                if (item["AmountType"].ToString() == "Cr")
                    dr["AmountCr"] = Convert.ToDecimal(item["Amount"]);  //Dr Amount
                else
                    dr["AmountDr"] = Convert.ToDecimal(item["Amount"]);  //Cr Amount	
                dr["AdvanceInd"] = 0;//item["DrCr"] == "Dr" ? 1 : 0;    //AgainstAdvanceInd (If Receipt Made For Against Advance Taken From Debitor/Creditor Then It Goes 1 Else 0)

                if (ddlPayMode.SelectedValue == "Cheque")
                {
                    dr["ChequeNo"] = string.IsNullOrEmpty(txtReceivedNo.Text) ? 0 : Convert.ToInt64(txtReceivedNo.Text);
                    dr["ChequeDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
                }
                else if (ddlPayMode.SelectedValue == "UTR")
                {
                    dr["UTRNo"] = txtReceivedNo.Text;
                    dr["UTRDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
                }

                dr["DocDesc"] = txtNarration.Text;                      //Narration
                dr["EntryType"] = 2;//                                  //EntryType - 1-Entry/2-Ammendment/3-Cancel
                dr["User"] = GlobalSession.UserID;  //UserID - User ID (Entry By Which User)
                dr["IP"] = GlobalSession.IP;                           //IPAddress - Client Machine IP Address

                //dr["BillNos"] = item["BillNos"].ToString();
                //if (Convert.ToInt32(item["PartyID"].ToString()) > 0)
                //{
                //    dr["PartyID"] = item["PartyID"].ToString();
                //}

                dtBranch.Rows.Add(dr);
            }

            objUpdInterBranch = new UpdateInterBranchModel();
            objUpdInterBranch.Ind = 7;
            objUpdInterBranch.OrgID = GlobalSession.OrgID;
            objUpdInterBranch.BrID = GlobalSession.BrID;
            objUpdInterBranch.VchType = Convert.ToInt32(ViewState["VchType"]);
            objUpdInterBranch.YrCD = GlobalSession.YrCD;

            if (Convert.ToInt64(txtInvoiceTotalAmount.Text) < 0)
            {
                objUpdInterBranch.AmountDr = Convert.ToInt64(txtInvoiceTotalAmount.Text) * -1;
                objUpdInterBranch.AmountCr = 0;
            }
            else if (Convert.ToInt64(txtInvoiceTotalAmount.Text) > 0)
            {
                objUpdInterBranch.AmountDr = 0;
                objUpdInterBranch.AmountCr = Convert.ToInt64(txtInvoiceTotalAmount.Text);
            }

            objUpdInterBranch.IP = GlobalSession.IP;
            objUpdInterBranch.DocNo = CommonCls.ConvertIntZero(txtSearchVNo.Text);

            objUpdInterBranch.DocDesc = txtNarration.Text;
            objUpdInterBranch.InterBrID = Convert.ToInt32(ddlBranchList.SelectedItem.Value);
            objUpdInterBranch.BankCode = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);
            objUpdInterBranch.Dt = dtBranch;
            objUpdInterBranch.AccCode2 = Convert.ToInt32(ddlBankAccount.SelectedItem.Value);
            objUpdInterBranch.DocDate = CommonCls.ConvertToDate(txtVoucherDate.Text);
            objUpdInterBranch.AdvanceInd = 0;
            objUpdInterBranch.EntryType = 2;
            objUpdInterBranch.User = GlobalSession.UserID;
            objUpdInterBranch.IP = GlobalSession.IP;


            string uri = string.Format("UpdateInterBranch/UpdateInterBranch");
            DataTable dtSaveInterBranch = CommonCls.ApiPostDataTable(uri, objUpdInterBranch);
            if (dtSaveInterBranch.Rows.Count > 0)
            {
                if (dtSaveInterBranch.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ddlBankAccount.ClearSelection();
                    txtInvoiceTotalAmount.Text = "0";
                    txtReceivedDate.Text = txtReceivedNo.Text = "";
                    ddlPayMode.ClearSelection();
                    gvInterBranch.DataSource = null;
                    gvInterBranch.DataBind();
                    ViewState["grdData"] = null;

                    txtNarration.Dispose();
                    ClearAll();
                    LoadLastVoucherNo();

                    string VoucherNo, VoucherDate;
                    VoucherNo = dtSaveInterBranch.Rows[0]["DocMaxNo"].ToString();
                    VoucherDate = Convert.ToDateTime(dtSaveInterBranch.Rows[0]["DocDate"].ToString()).ToString("dd/MM/yyyy");

                    ShowMessage("Record Successfully Updated With Voucher No.  " + VoucherNo, true);
                    lblInvoiceAndDate.Text = "Last Voucher No. & Date " + VoucherNo + " - " + VoucherDate;

                    txtVoucherDate.Focus();
                }
                else
                {
                    ShowMessage("Record Not Update. Please Try Again.", false);
                }
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }


    DataTable InterBranchSchema()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("OrgID", typeof(int));
        dt.Columns.Add("BrID", typeof(int));
        dt.Columns.Add("VchType", typeof(int));
        dt.Columns.Add("YrCD", typeof(int));
        dt.Columns.Add("DocDate", typeof(string));
        dt.Columns.Add("DocNo", typeof(int));
        dt.Columns.Add("AccCode", typeof(int));
        dt.Columns.Add("AccGst", typeof(string));
        dt.Columns.Add("AccCode2", typeof(int));
        dt.Columns.Add("RefNo", typeof(int));
        dt.Columns.Add("RefDate", typeof(string));
        dt.Columns.Add("AmountDr", typeof(decimal));
        dt.Columns.Add("AmountCr", typeof(decimal));
        dt.Columns.Add("AdvanceInd", typeof(int));
        dt.Columns.Add("ChequeNo", typeof(int));
        dt.Columns.Add("ChequeDate", typeof(string));
        dt.Columns.Add("UTRNo", typeof(string));
        dt.Columns.Add("UTRDate", typeof(string));
        dt.Columns.Add("DocDesc", typeof(string));
        dt.Columns.Add("EntryType", typeof(int));
        dt.Columns.Add("User", typeof(int));
        dt.Columns.Add("IP", typeof(string));
        dt.Columns.Add("InterBrID", typeof(int));
        dt.Columns.Add("BankCode", typeof(int));

        return dt;
    }
    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPayMode();
        txtReceivedNo.Focus();

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCancelReason.SelectedValue == "")
            {
                ShowMessage("Enter Cancel Reason", false);
                pnlConfirmInvoice.Visible = false;
                return;
            }
            UpdateInterBranchModel objUpdInterBranch;
            objUpdInterBranch = new UpdateInterBranchModel();
            objUpdInterBranch.Ind = 6;
            objUpdInterBranch.OrgID = GlobalSession.OrgID;
            objUpdInterBranch.CancelReason = ddlCancelReason.SelectedItem.Text;
            objUpdInterBranch.DocNo = Convert.ToInt32(txtSearchVNo.Text);

            string uri = string.Format("UpdateInterBranch/CancelVoucher");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, objUpdInterBranch);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["CancelInd"].ToString() == "1")
                {
                    pnlConfirmInvoice.Visible = false;
                    ClearOnSave();
                    ClearAll();
                    ShowMessage("Vouchar No. - " + objUpdInterBranch.DocNo + " is Cancel successfully ", true);
                }
                else
                {
                    pnlConfirmInvoice.Visible = false;
                    ShowMessage("Vouchar is not Cancel ", true);
                }
            }
            else
            {
                ShowMessage("Voucher Is Not Cancelled", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        txtCancelReason.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!CommonCls.CheckGUIDIsValid())
        {
            return;
        }

        pnlConfirmInvoice.Visible = true;
        pnlConfirmInvoice.Focus();
    }
}