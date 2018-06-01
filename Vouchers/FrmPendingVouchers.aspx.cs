using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vouchers_FrmPendingVouchers : System.Web.UI.Page
{
    #region Declaration

    PendingVouchersModel objPendingVouchersModel;

    DataTable VsdtPendingVouchers
    {
        get { return (DataTable)ViewState["dtPendingVouchers"]; }
        set { ViewState["dtPendingVouchers"] = value; }
    }

    DataTable VsdtOrgPendingVouchers
    {
        get { return (DataTable)ViewState["dtOrgPendingVouchers"]; }
        set { ViewState["dtOrgPendingVouchers"] = value; }
    }

    DataTable VsdtCompleteVouchers
    {
        get { return (DataTable)ViewState["dtCompleteVouchers"]; }
        set { ViewState["dtCompleteVouchers"] = value; }
    }

    DataTable VsdtBankPayAgainstPurchase
    {
        get { return (DataTable)ViewState["dtBankPayAgainstPurchase"]; }
        set { ViewState["dtBankPayAgainstPurchase"] = value; }
    }

    DataTable VsdtCompleteList
    {
        get { return (DataTable)ViewState["dtCompleteList"]; }
        set { ViewState["dtCompleteList"] = value; }
    }

    DataTable VsdtCmpBankPayAgainstPurchase
    {
        get { return (DataTable)ViewState["dtCmpBankPayAgainstPurchase"]; }
        set { ViewState["dtCmpBankPayAgainstPurchase"] = value; }
    }

    DataTable VsdtSTA
    {
        get { return (DataTable)ViewState["dtSTA"]; }
        set { ViewState["dtSTA"] = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["RowIndex"] = null;

            BindAll();
        }
    }

    void BindAll()
    {
        objPendingVouchersModel = new PendingVouchersModel();
        objPendingVouchersModel.Ind = 1;
        objPendingVouchersModel.OrgID = GlobalSession.OrgID;
        objPendingVouchersModel.BrID = GlobalSession.BrID;
        objPendingVouchersModel.YrCD = GlobalSession.YrCD;

        string uri = string.Format("PendingVouchers/BindPendingVouchers");
        DataSet dsPendingVouchers = CommonCls.ApiPostDataSet(uri, objPendingVouchersModel);
        if (dsPendingVouchers.Tables.Count > 0)
        {
            DataView dvPendingVouchers = new DataView(dsPendingVouchers.Tables[0]);
            VsdtCompleteList = dsPendingVouchers.Tables[2];

            VsdtCmpBankPayAgainstPurchase = dsPendingVouchers.Tables[3];

            if (dvPendingVouchers.ToTable().Rows.Count > 0)
            {
                //Filter According To Superintendent
                if (GlobalSession.DepartmentID == 1500 && GlobalSession.DesignationID == 26)
                {
                    dvPendingVouchers.RowFilter = "IsFinal=0 And IsSendToAudit=1 And InvoiceNo<=0";
                    ApprovedList();
                }//Filter According To Account Officer
                else if (GlobalSession.DepartmentID == 1500 && (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17))
                {
                    dvPendingVouchers.RowFilter = "IsFinal=1 And InvoiceNo<=0";
                    ApprovedList();
                }
                else
                {
                    //Filter According To Department User
                    if (GlobalSession.DepartmentID > 0)
                    {
                        //if (GlobalSession.DepartmentID != 1500)
                        //{
                        //    dvPendingVouchers.RowFilter = "IsAudit=0 And DeptCD='" + GlobalSession.DepartmentID + "'";
                        //}
                        //else
                        //{
                        //    dvPendingVouchers.RowFilter = "IsAudit=0 And DeptCD='" + GlobalSession.DepartmentID + "'";
                        //}

                        dvPendingVouchers.RowFilter = "IsFinal=0 And IsSendToAudit=0 And DeptCD='" + GlobalSession.DepartmentID + "'";

                        DataView dvSendToAudit = new DataView(dsPendingVouchers.Tables[0]);
                        dvSendToAudit.RowFilter = "IsSendToAudit=1 And IsAudit=0 And DeptCD='" + GlobalSession.DepartmentID + "'";

                        grdSTA.DataSource = VsdtSTA = dvSendToAudit.ToTable();
                        grdSTA.DataBind();

                        if (grdSTA.Rows.Count > 0)
                            divSTA.Visible = divTSTAT.Visible = tblSTA.Visible = true;
                        else
                            divSTA.Visible = divTSTAT.Visible = tblSTA.Visible = false;

                        lblTotalSTAT.Text = grdSTA.Rows.Count.ToString();
                    }
                }

                VsdtOrgPendingVouchers = VsdtPendingVouchers = dvPendingVouchers.ToTable();
                grdPendingVouchers.DataSource = VsdtPendingVouchers;
                grdPendingVouchers.DataBind();

                ManagePendingVoucherGrid();

                if (grdPendingVouchers.Rows.Count > 0)
                    divPendingSince.Visible = tblPendingListHeader.Visible = true;
                else
                    divPendingSince.Visible = tblPendingListHeader.Visible = false;

                DataTable dtBankAccount = dsPendingVouchers.Tables[4];

                if (dtBankAccount.Rows.Count > 0)
                {
                    ddlBankAccount.DataSource = dtBankAccount;
                    ddlBankAccount.DataTextField = "AccName";
                    ddlBankAccount.DataValueField = "AccCode";
                    ddlBankAccount.DataBind();
                    if (dtBankAccount.Rows.Count > 1)
                        ddlBankAccount.Items.Insert(0, new ListItem("-- Select --", "0"));
                }

                VsdtBankPayAgainstPurchase = dsPendingVouchers.Tables[1];

                Session["RowIndex"] = null;
            }
            else
            {
                ShowMessage("Record Not Found.", false, lblMsg);
                divPendingSince.Visible = tblPendingListHeader.Visible = false;
            }
        }
    }

    void ManagePendingVoucherGrid() //Manage Pending Vouchers GridView Buttons(Enabled Ture / False) According To Superintendent,Account Officer And Department User
    {
        if (Session["RowIndex"] != null)
        {
            bool IsTrue = false;
            CheckBox chkPerformOperation = new CheckBox();
            int rowIndex = 0;

            foreach (GridViewRow gvRow in grdPendingVouchers.Rows)
            {
                chkPerformOperation = (CheckBox)gvRow.FindControl("chkPerformOperation");

                
                rowIndex = gvRow.RowIndex;
                if (rowIndex == CommonCls.ConvertIntZero(Session["RowIndex"].ToString()))
                {
                    chkPerformOperation.Checked = true;
                    IsTrue = true;
                    break;
                }
                continue;
            }

            if (IsTrue == true)
            {
                Label lblIsFinal = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblIsFinal");
                Label lblLastApprovedBy = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblLastApprovedBy");
                Label lblDocType = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblDocType");
                Label lblIsAudit = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblIsAudit");
                Label lblIsSendToAudit = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblIsSendToAudit");
                Label lblBankPayInd = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblBankPayInd");
                Label lblPurchaseBankPayInd = (Label)grdPendingVouchers.Rows[rowIndex].FindControl("lblPurchaseBankPayInd");
                Button btnView = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnView");
                Button btnTransactionPrint = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnTransactionPrint");
                Button btnSendToAudit = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnSendToAudit");
                Button btnAudit = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnAudit");
                Button btnUpdate = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnUpdate");
                Button btnApprove = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnApprove");
                Button btnBankPay = (Button)grdPendingVouchers.Rows[rowIndex].FindControl("btnBankPay");

                //Label lblIsFinal = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblIsFinal");
                //Label lblLastApprovedBy = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblLastApprovedBy");
                //CheckBox chkPerformOperation = (CheckBox)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("chkPerformOperation");
                //Label lblDocType = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblDocType");
                //Label lblIsAudit = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblIsAudit");
                //Label lblIsSendToAudit = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblIsSendToAudit");
                //Label lblBankPayInd = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblBankPayInd");
                //Label lblPurchaseBankPayInd = (Label)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("lblPurchaseBankPayInd");
                //Button btnView = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnView");
                //Button btnTransactionPrint = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnTransactionPrint");
                //Button btnSendToAudit = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnSendToAudit");
                //Button btnAudit = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnAudit");
                //Button btnUpdate = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnUpdate");
                //Button btnApprove = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnApprove");
                //Button btnBankPay = (Button)grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].FindControl("btnBankPay");

                //chkPerformOperation.Checked = true;

                if (chkPerformOperation.Checked == true)
                {
                    if (GlobalSession.DepartmentID == 1500)
                    {
                        //For Account Officer
                        if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                        {
                            if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
                            {
                                btnView.Enabled = btnApprove.Enabled = true;
                                btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = false;
                            }
                            else if (CommonCls.ConvertIntZero(lblDocType.Text) == 4)
                            {
                                if (Convert.ToString(lblBankPayInd.Text) == "True")
                                {
                                    btnSendToAudit.Enabled = btnAudit.Enabled = btnView.Enabled = btnUpdate.Enabled = false;
                                    btnApprove.Enabled = true;
                                }
                                else
                                {
                                    btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = false;
                                    btnView.Enabled = btnApprove.Enabled = true;
                                }
                            }
                            else
                            {
                                btnView.Enabled = btnApprove.Enabled = true;
                                btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = false;
                            }
                            btnView.Focus();

                        }//For Superintendent
                        else if (GlobalSession.DesignationID == 26)
                        {
                            if (CommonCls.ConvertIntZero(lblIsAudit.Text) == 1)
                            {
                                //btnSendToAudit.Enabled = btnAudit.Enabled = false;
                                //btnView.Enabled = btnUpdate.Enabled = true;//btnApprove.Enabled = 

                                if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
                                {
                                    if (Convert.ToString(lblPurchaseBankPayInd.Text) == "True")
                                    {
                                        btnUpdate.Enabled = false;
                                        btnApprove.Enabled = true;
                                    }
                                    else
                                    {
                                        btnUpdate.Enabled = true;
                                        btnApprove.Enabled = false;
                                    }

                                    btnView.Enabled = btnBankPay.Enabled = true;
                                    btnSendToAudit.Enabled = btnAudit.Enabled = false;
                                }
                                else if (CommonCls.ConvertIntZero(lblDocType.Text) == 4)
                                {
                                    if (Convert.ToString(lblBankPayInd.Text) == "True")
                                    {
                                        btnSendToAudit.Enabled = btnAudit.Enabled = btnView.Enabled = btnUpdate.Enabled = false;
                                        btnApprove.Enabled = true;
                                    }
                                    else
                                    {
                                        btnSendToAudit.Enabled = btnAudit.Enabled = false;
                                        btnView.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
                                    }
                                }
                                else
                                {
                                    btnView.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
                                    btnSendToAudit.Enabled = btnAudit.Enabled = false;
                                }

                                //if (Session["UpdAndView"] != null)
                                //{
                                //    if (Session["UpdAndView"].ToString() == "Upd")
                                //        btnUpdate.Focus();
                                //    else
                                //        btnView.Focus();
                                //}
                                //else
                                //    btnUpdate.Focus();
                            }
                            else
                            {
                                btnView.Enabled = btnAudit.Enabled = true;
                                btnSendToAudit.Enabled = btnUpdate.Enabled = btnApprove.Enabled = btnBankPay.Enabled = false;
                            }
                            btnView.Focus();

                        }//For Department User
                        else
                        {
                            if (GlobalSession.DesignationID > 0)
                            {
                                if (CommonCls.ConvertIntZero(lblIsSendToAudit.Text) == 1)
                                {
                                    btnTransactionPrint.Enabled = btnSendToAudit.Enabled = false;
                                    btnView.Enabled = true;
                                }
                                else
                                {
                                    btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                                }
                            }
                            else
                            {
                                btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                                btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
                            }
                            btnView.Focus();
                        }
                    }//For Department User
                    else
                    {
                        if (GlobalSession.DesignationID > 0)
                        {
                            if (CommonCls.ConvertIntZero(lblIsSendToAudit.Text) == 1)
                            {
                                btnTransactionPrint.Enabled = btnSendToAudit.Enabled = false;
                                btnView.Enabled = true;
                            }
                            else
                            {
                                btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                            }
                        }
                        else
                        {
                            btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                            btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
                            //btnView.Focus();
                        }

                        btnView.Focus();
                    }
                    grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].BackColor = System.Drawing.Color.Wheat;
                }
                else
                {
                    grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].BackColor = System.Drawing.Color.White;
                    btnView.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
                }
            }
        }

        if (grdPendingVouchers.Rows.Count > 0)
            lblTotalPendingSince.Text = grdPendingVouchers.Rows.Count.ToString();
    }

    #region Old

    //void ManagePendingVoucherGrid() //Manage Pending Vouchers GridView Buttons(Enabled Ture / False) According To Superintendent,Account Officer And Department User
    //{
    //    foreach (GridViewRow gvRow in grdPendingVouchers.Rows)
    //    {
    //        Label lblIsFinal = (Label)gvRow.FindControl("lblIsFinal");
    //        Label lblLastApprovedBy = (Label)gvRow.FindControl("lblLastApprovedBy");
    //        Label lblTransactionAmount = (Label)gvRow.FindControl("lblTransactionAmount");
    //        Label lblDepartmentName = (Label)gvRow.FindControl("lblDepartmentName");
    //        Label lblSubDepartmentName = (Label)gvRow.FindControl("lblSubDepartmentName");
    //        Label lblDeptAndSubDeptName = (Label)gvRow.FindControl("lblDeptAndSubDeptName");

    //        if (!string.IsNullOrEmpty(lblSubDepartmentName.Text))
    //            lblDeptAndSubDeptName.Text = lblDepartmentName.Text + " - " + lblSubDepartmentName.Text;
    //        else
    //            lblDeptAndSubDeptName.Text = lblDepartmentName.Text + " (Head)";

    //        lblTransactionAmount.Text = Convert.ToString(CommonCls.ConverToCommas(lblTransactionAmount.Text));

    //        CheckBox chkPerformOperation = (CheckBox)gvRow.FindControl("chkPerformOperation");

    //        int rowIndex = 0;
    //        rowIndex = gvRow.RowIndex;

    //        if (Session["RowIndex"] != null)
    //        {
    //            if (rowIndex == CommonCls.ConvertIntZero(Session["RowIndex"].ToString()))
    //            {
    //                chkPerformOperation.Checked = true;

    //                Label lblDocType = (Label)gvRow.FindControl("lblDocType");
    //                Label lblIsAudit = (Label)gvRow.FindControl("lblIsAudit");
    //                Label lblIsSendToAudit = (Label)gvRow.FindControl("lblIsSendToAudit");
    //                Label lblBankPayInd = (Label)gvRow.FindControl("lblBankPayInd");
    //                Label lblPurchaseBankPayInd = (Label)gvRow.FindControl("lblPurchaseBankPayInd");
    //                Button btnView = (Button)gvRow.FindControl("btnView");
    //                Button btnTransactionPrint = (Button)gvRow.FindControl("btnTransactionPrint");
    //                Button btnSendToAudit = (Button)gvRow.FindControl("btnSendToAudit");
    //                Button btnAudit = (Button)gvRow.FindControl("btnAudit");
    //                Button btnUpdate = (Button)gvRow.FindControl("btnUpdate");
    //                Button btnApprove = (Button)gvRow.FindControl("btnApprove");
    //                Button btnBankPay = (Button)gvRow.FindControl("btnBankPay");

    //                if (chkPerformOperation.Checked == true)
    //                {
    //                    if (GlobalSession.DepartmentID == 1500)
    //                    {
    //                        //For Account Officer
    //                        if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
    //                        {
    //                            if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
    //                            {
    //                                btnView.Enabled = btnApprove.Enabled = true;
    //                                btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = false;
    //                            }
    //                            else if (CommonCls.ConvertIntZero(lblDocType.Text) == 4)
    //                            {
    //                                if (Convert.ToString(lblBankPayInd.Text) == "True")
    //                                {
    //                                    btnSendToAudit.Enabled = btnAudit.Enabled = btnView.Enabled = btnUpdate.Enabled = false;
    //                                    btnApprove.Enabled = true;
    //                                }
    //                                else
    //                                {
    //                                    btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = false;
    //                                    btnView.Enabled = btnApprove.Enabled = true;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                btnView.Enabled = btnApprove.Enabled = true;
    //                                btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = false;
    //                            }
    //                            btnView.Focus();

    //                        }//For Superintendent
    //                        else if (GlobalSession.DesignationID == 26)
    //                        {
    //                            if (CommonCls.ConvertIntZero(lblIsAudit.Text) == 1)
    //                            {
    //                                //btnSendToAudit.Enabled = btnAudit.Enabled = false;
    //                                //btnView.Enabled = btnUpdate.Enabled = true;//btnApprove.Enabled = 

    //                                if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
    //                                {
    //                                    if (Convert.ToString(lblPurchaseBankPayInd.Text) == "True")
    //                                    {
    //                                        btnUpdate.Enabled = false;
    //                                        btnApprove.Enabled = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnUpdate.Enabled = true;
    //                                        btnApprove.Enabled = false;
    //                                    }

    //                                    btnView.Enabled = btnBankPay.Enabled = true;
    //                                    btnSendToAudit.Enabled = btnAudit.Enabled = false;
    //                                }
    //                                else if (CommonCls.ConvertIntZero(lblDocType.Text) == 4)
    //                                {
    //                                    if (Convert.ToString(lblBankPayInd.Text) == "True")
    //                                    {
    //                                        btnSendToAudit.Enabled = btnAudit.Enabled = btnView.Enabled = btnUpdate.Enabled = false;
    //                                        btnApprove.Enabled = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        btnSendToAudit.Enabled = btnAudit.Enabled = false;
    //                                        btnView.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    btnView.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
    //                                    btnSendToAudit.Enabled = btnAudit.Enabled = false;
    //                                }

    //                                //if (Session["UpdAndView"] != null)
    //                                //{
    //                                //    if (Session["UpdAndView"].ToString() == "Upd")
    //                                //        btnUpdate.Focus();
    //                                //    else
    //                                //        btnView.Focus();
    //                                //}
    //                                //else
    //                                //    btnUpdate.Focus();
    //                            }
    //                            else
    //                            {
    //                                btnView.Enabled = btnAudit.Enabled = true;
    //                                btnSendToAudit.Enabled = btnUpdate.Enabled = btnApprove.Enabled = btnBankPay.Enabled = false;
    //                            }
    //                            btnView.Focus();

    //                        }//For Department User
    //                        else
    //                        {
    //                            if (GlobalSession.DesignationID > 0)
    //                            {
    //                                if (CommonCls.ConvertIntZero(lblIsSendToAudit.Text) == 1)
    //                                {
    //                                    btnTransactionPrint.Enabled = btnSendToAudit.Enabled = false;
    //                                    btnView.Enabled = true;
    //                                }
    //                                else
    //                                {
    //                                    btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
    //                                btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
    //                            }
    //                            btnView.Focus();
    //                        }
    //                    }//For Department User
    //                    else
    //                    {
    //                        if (GlobalSession.DesignationID > 0)
    //                        {
    //                            if (CommonCls.ConvertIntZero(lblIsSendToAudit.Text) == 1)
    //                            {
    //                                btnTransactionPrint.Enabled = btnSendToAudit.Enabled = false;
    //                                btnView.Enabled = true;
    //                            }
    //                            else
    //                            {
    //                                btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
    //                            btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
    //                            //btnView.Focus();
    //                        }

    //                        btnView.Focus();
    //                    }
    //                    gvRow.BackColor = System.Drawing.Color.Wheat;
    //                }
    //                else
    //                {
    //                    gvRow.BackColor = System.Drawing.Color.White;
    //                    btnView.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
    //                }
    //            }
    //        }
    //    }

    //    if (grdPendingVouchers.Rows.Count > 0)
    //        lblTotalPendingSince.Text = grdPendingVouchers.Rows.Count.ToString();
    //}

    #endregion

    void ApprovedList()
    {
        if (VsdtCompleteList.Rows.Count > 0)
        {
            grdCompleteTransactionList.DataSource = VsdtCompleteList;
            grdCompleteTransactionList.DataBind();

            if (grdCompleteTransactionList.Rows.Count > 0)
            {
                lblTotalCompleteTransaction.Text = grdCompleteTransactionList.Rows.Count.ToString();

                tblCompleteTransaction.Visible = divCT.Visible = divCompleteVoucher.Visible = true;
            }
        }
        else
            tblCompleteTransaction.Visible = divCT.Visible = divCompleteVoucher.Visible = false;
    }

    protected void grdPendingVouchers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dtPendingVouchers = VsdtPendingVouchers;
            DataRow drEditItemsDetails = dtPendingVouchers.Rows[rowIndex];

            lblTransactionNo.Text = drEditItemsDetails["TransactionNo"].ToString();
            lblTransactionAmount.Text = drEditItemsDetails["NetAmount"].ToString();
            lblTransactionDate.Text = drEditItemsDetails["TransactionDate"].ToString();
            lblDocumentType.Text = drEditItemsDetails["DocumentType"].ToString();
            lblNarration.Text = drEditItemsDetails["Narration"].ToString();
            lblDoumentTypeID.Text = drEditItemsDetails["DocTypeID"].ToString();
            lblPartyName.Text = drEditItemsDetails["PartyName"].ToString();
            lblAccCode.Text = drEditItemsDetails["AccCode"].ToString();
            lblCCCode.Text = drEditItemsDetails["CCCode"].ToString();
            lblPurchaseBankPayInd.Text = drEditItemsDetails["PurchaseBankPayInd"].ToString();

            Session["rowIndex"] = rowIndex;

            //For View And Update Transaction
            if (e.CommandName == "UpdateRow" || e.CommandName == "ViewRow")
            {
                Session["TransNo"] = lblTransactionNo.Text;
                Session["TransDate"] = lblTransactionDate.Text;
                Session["IsAudit"] = drEditItemsDetails["IsAudit"].ToString();
                Session["IsSendToAudit"] = drEditItemsDetails["IsSendToAudit"].ToString();
                Session["PurchaseBankPayInd"] = drEditItemsDetails["PurchaseBankPayInd"].ToString();

                if (e.CommandName == "UpdateRow")
                    Session["UpdAndView"] = "Upd";
                else if (e.CommandName == "ViewRow")
                    Session["UpdAndView"] = "View";

                if (drEditItemsDetails["DocTypeID"].ToString() == "2")
                    Response.Redirect("../Modifiaction/frmUpdCashPayment.aspx", false);
                else if (drEditItemsDetails["DocTypeID"].ToString() == "4")
                    Response.Redirect("../Modifiaction/frmUpdBankPayment.aspx", false);
                else if (drEditItemsDetails["DocTypeID"].ToString() == "5")
                    Response.Redirect("../Modifiaction/UpdPurchase.aspx", false);
            }

            //For Print Transaction Number
            if (e.CommandName == "PrintTransRow")
            {
                if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 4 || CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 2)
                {
                    Session["Report"] = "RptVoucher";

                    Hashtable HT = new Hashtable();

                    HT.Add("CompName", GlobalSession.OrgName);
                    HT.Add("BranchName", GlobalSession.BrName);

                    if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 4)
                        HT.Add("Heading", "Bank Payment");
                    else
                        HT.Add("Heading", "Cash Payment");

                    HT.Add("Ind", 2);
                    HT.Add("OrgID", GlobalSession.OrgID);
                    HT.Add("BrID", GlobalSession.BrID);
                    HT.Add("yrcode", 0);

                    HT.Add("DocTypeID", CommonCls.ConvertIntZero(lblDoumentTypeID.Text));
                    HT.Add("Voucharno", CommonCls.ConvertIntZero(lblTransactionNo.Text));
                    HT.Add("VoucharDate", lblTransactionDate.Text.Substring(6, 4) + "/" + lblTransactionDate.Text.Substring(3, 2) + "/" + lblTransactionDate.Text.Substring(0, 2));

                    Session["HT"] = HT;
                    Session["format"] = "Pdf";
                    Session["FileName"] = "PurchaseVoucher";
                    Response.Redirect("../Reports/FrmReportViewer.aspx");
                }
                else if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 5)
                {
                    Session["Report"] = "RptPurchaseVoucher";

                    Hashtable HT = new Hashtable();

                    HT.Add("Ind", 2);
                    HT.Add("OrgID", GlobalSession.OrgID);
                    HT.Add("BrID", GlobalSession.BrID);
                    HT.Add("yrcode", 0);
                    HT.Add("CompName", GlobalSession.OrgName);
                    HT.Add("BranchName", GlobalSession.BrName);
                    HT.Add("Heading", "PURCHASE VOUCHER");

                    HT.Add("Doctype", 5);
                    HT.Add("invoiceno", CommonCls.ConvertIntZero(lblTransactionNo.Text));
                    HT.Add("invoiceDate", lblTransactionDate.Text.Substring(6, 4) + "/" + lblTransactionDate.Text.Substring(3, 2) + "/" + lblTransactionDate.Text.Substring(0, 2));
                    HT.Add("invoiceDateFrom", "");
                    HT.Add("invoiceDateto", "");
                    HT.Add("cashsalesind", 0);
                    HT.Add("vNO", CommonCls.ConvertIntZero(lblTransactionNo.Text));

                    Session["HT"] = HT;
                    Session["format"] = "Pdf";
                    Session["FileName"] = "PurchaseVoucher";
                    Response.Redirect("../Reports/FrmReportViewer.aspx");
                }
            }

            //For Send To Audit Transaction Number
            if (e.CommandName == "SendToAuditRow")
            {
                lblSendToAuditTransactionNo.Text = lblTransactionNo.Text;

                pnlSendToAudit.Visible = true;
            }

            //For Audit Transaction Number
            if (e.CommandName == "AuditRow")
            {
                pnlAuditConfirmation.Visible = true;
                lblAuditTransactionNo.Text = lblTransactionNo.Text;
            }

            //Generate New Bank Payment Transaction Against Purchase Transaction
            if (e.CommandName == "BankPayRow")
            {
                if (Convert.ToString(lblPurchaseBankPayInd.Text) == "True")
                {
                    DataRow[] row = VsdtBankPayAgainstPurchase.Select("InvoiceNo='" + lblTransactionNo.Text + "'");

                    if (row.Count() > 0)
                    {
                        txtTransactionDate.Text = CommonCls.ConvertDateDB(row[0]["TransactionDate"].ToString());
                        lblBankPayTransactionNo.Text = row[0]["TransactionNo"].ToString();
                        ddlBankAccount.SelectedValue = row[0]["BankCode"].ToString();
                        txtPartyName.Text = row[0]["PartyName"].ToString();
                        txtNetAmount.Text = CommonCls.ConverToCommas(row[0]["NetAmount"].ToString());
                        if (CommonCls.ConvertIntZero(row[0]["ChequeNo"].ToString()) > 0)
                        {
                            ddlPayMode.SelectedValue = "Cheque";
                            SetPayMode();
                            txtReceivedNo.Text = row[0]["ChequeNo"].ToString();
                            txtReceivedDate.Text = CommonCls.ConvertDateDB(row[0]["ChequeDate"].ToString());
                        }
                        else
                        {
                            ddlPayMode.SelectedValue = "UTR";
                            SetPayMode();
                            txtReceivedNo.Text = row[0]["UTRNo"].ToString();
                            txtReceivedDate.Text = CommonCls.ConvertDateDB(row[0]["UTRDate"].ToString());
                        }

                        txtNarration.Text = row[0]["Narration"].ToString();

                        lblBankPaymentHeader.Text = "Update Bank Payment";
                        btnBankPaySave.Text = "Update";
                        txtTransactionDate.Focus();
                        pnlBankPay.Visible = true;
                    }
                }
                else
                {
                    txtPartyName.Text = lblPartyName.Text;
                    txtNetAmount.Text = CommonCls.ConverToCommas(lblTransactionAmount.Text);
                    txtNarration.Text = lblNarration.Text;
                    lblBankPaymentHeader.Text = "Bank Payment";
                    btnBankPaySave.Text = "Save";
                    txtTransactionDate.Focus();
                    pnlBankPay.Visible = true;
                }
            }

            //For Transaction Number Approval
            if (e.CommandName == "ApproveRow")
            {
                lblApprovalTransactionNo.Text = lblTransactionNo.Text;
                pnlApproval.Visible = true;
            }

        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, false, lblMsg);
        }
    }

    protected void grdPendingVouchers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Manage Pending Vouchers GridView Column(Visible True / False) According To Superintendent,Account Officer And Department User

            Button btnView = (Button)e.Row.FindControl("btnView");
            Button btnTransactionPrint = (Button)e.Row.FindControl("btnTransactionPrint");
            Button btnSendToAudit = (Button)e.Row.FindControl("btnSendToAudit");
            Button btnAudit = (Button)e.Row.FindControl("btnAudit");
            Button btnUpdate = (Button)e.Row.FindControl("btnUpdate");
            Button btnBankPay = (Button)e.Row.FindControl("btnBankPay");
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");

            if (GlobalSession.DepartmentID == 1500)
            {
                if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                {
                    #region Manage GridView Column According To Account Officer

                    e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = e.Row.Cells[12].Visible = e.Row.Cells[13].Visible = false;

                    thTransaction.ColSpan = 4;
                    thTransNo.Width = "4%";
                    thTransDate.Width = "7%";
                    thTransType.Width = "7%";
                    thTransAmount.Width = "8%";
                    thReferenceNo.Width = "10%";
                    thReferenceNo.RowSpan = 2;
                    thPartyName.Width = "14%";
                    thPartyName.RowSpan = 2;
                    thDepartmentName.Width = "14%";
                    thDepartmentName.RowSpan = 2;
                    thNarration.Width = "25%";
                    thNarration.RowSpan = 2;

                    thAudit.Visible = thPrintTrans.Visible = thSendToAudit.Visible = thUpdate.Visible = thBankPay.Visible = false;

                    thAction.ColSpan = 2;
                    thView.Width = "5%";
                    thApprove.Width = "6%";

                    e.Row.Cells[0].Width = new Unit("4%");
                    e.Row.Cells[1].Width = new Unit("7%");
                    e.Row.Cells[2].Width = new Unit("7%");
                    e.Row.Cells[3].Width = new Unit("8%");
                    e.Row.Cells[4].Width = new Unit("10%");
                    e.Row.Cells[5].Width = new Unit("14%");
                    e.Row.Cells[6].Width = new Unit("14%");
                    e.Row.Cells[7].Width = new Unit("25%");
                    e.Row.Cells[8].Width = new Unit("5%");
                    e.Row.Cells[14].Width = new Unit("6%");

                    #endregion

                    btnView.Visible = btnApprove.Visible = true;
                }
                else if (GlobalSession.DesignationID == 26)
                {
                    #region Manage GridView Column According To Suprientendent

                    e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = false;

                    thTransaction.ColSpan = 4;
                    thTransNo.Width = "4%";
                    thTransDate.Width = "7%";
                    thTransType.Width = "7%";
                    thTransAmount.Width = "8%";
                    thReferenceNo.Width = "8%";
                    thReferenceNo.RowSpan = 2;
                    thPartyName.Width = "10%";
                    thPartyName.RowSpan = 2;
                    thDepartmentName.Width = "10%";
                    thDepartmentName.RowSpan = 2;
                    thNarration.Width = "18%";
                    thNarration.RowSpan = 2;

                    thPrintTrans.Visible = thSendToAudit.Visible = false;

                    thAction.ColSpan = 5;
                    thView.Width = "5%";
                    thAudit.Width = "5%";
                    thUpdate.Width = "6%";
                    thBankPay.Width = "6%";
                    thApprove.Width = "6%";

                    e.Row.Cells[0].Width = new Unit("4%");
                    e.Row.Cells[1].Width = new Unit("7%");
                    e.Row.Cells[2].Width = new Unit("7%");
                    e.Row.Cells[3].Width = new Unit("8%");
                    e.Row.Cells[4].Width = new Unit("8%");
                    e.Row.Cells[5].Width = new Unit("10%");
                    e.Row.Cells[6].Width = new Unit("10%");
                    e.Row.Cells[7].Width = new Unit("18%");
                    e.Row.Cells[8].Width = new Unit("5%");
                    e.Row.Cells[11].Width = new Unit("5%");
                    e.Row.Cells[12].Width = new Unit("6%");
                    e.Row.Cells[13].Width = new Unit("6%");
                    e.Row.Cells[14].Width = new Unit("6%");

                    #endregion

                    btnView.Visible = btnAudit.Visible = btnUpdate.Visible = btnBankPay.Visible = btnApprove.Visible = true;
                }
                else
                {
                    #region Manage GridView Column According To Other Department Users

                    e.Row.Cells[11].Visible = e.Row.Cells[12].Visible = e.Row.Cells[13].Visible = e.Row.Cells[14].Visible = false;

                    thTransaction.ColSpan = 4;
                    thTransNo.Width = "4%";
                    thTransDate.Width = "7%";
                    thTransType.Width = "7%";
                    thTransAmount.Width = "8%";
                    thReferenceNo.Width = "8%";
                    thReferenceNo.RowSpan = 2;
                    thPartyName.Width = "14%";
                    thPartyName.RowSpan = 2;
                    thDepartmentName.Width = "13%";
                    thDepartmentName.RowSpan = 2;
                    thNarration.Width = "20%";
                    thNarration.RowSpan = 2;

                    thAudit.Visible = thApprove.Visible = thUpdate.Visible = thBankPay.Visible = false;

                    thAction.ColSpan = 3;
                    thView.Width = "5%";
                    thPrintTrans.Width = "5%";
                    thSendToAudit.Width = "9%";

                    e.Row.Cells[0].Width = new Unit("4%");
                    e.Row.Cells[1].Width = new Unit("7%");
                    e.Row.Cells[2].Width = new Unit("7%");
                    e.Row.Cells[3].Width = new Unit("8%");
                    e.Row.Cells[4].Width = new Unit("8%");
                    e.Row.Cells[5].Width = new Unit("14%");
                    e.Row.Cells[6].Width = new Unit("13%");
                    e.Row.Cells[7].Width = new Unit("20%");
                    e.Row.Cells[8].Width = new Unit("5%");
                    e.Row.Cells[9].Width = new Unit("5%");
                    e.Row.Cells[10].Width = new Unit("9%");

                    #endregion

                    btnView.Visible = btnTransactionPrint.Visible = btnSendToAudit.Visible = true;
                }
            }
            else if (GlobalSession.DepartmentID != 1500 && GlobalSession.DepartmentID > 0)
            {
                #region Manage GridView Column According To Other Department Users

                e.Row.Cells[11].Visible = e.Row.Cells[12].Visible = e.Row.Cells[13].Visible = e.Row.Cells[14].Visible = false;

                thTransaction.ColSpan = 4;
                thTransNo.Width = "4%";
                thTransDate.Width = "7%";
                thTransType.Width = "7%";
                thTransAmount.Width = "8%";
                thReferenceNo.Width = "8%";
                thReferenceNo.RowSpan = 2;
                thPartyName.Width = "14%";
                thPartyName.RowSpan = 2;
                thDepartmentName.Width = "13%";
                thDepartmentName.RowSpan = 2;
                thNarration.Width = "20%";
                thNarration.RowSpan = 2;

                thAudit.Visible = thApprove.Visible = thUpdate.Visible = thBankPay.Visible = false;

                thAction.ColSpan = 3;
                thView.Width = "5%";
                thPrintTrans.Width = "5%";
                thSendToAudit.Width = "9%";

                e.Row.Cells[0].Width = new Unit("4%");
                e.Row.Cells[1].Width = new Unit("7%");
                e.Row.Cells[2].Width = new Unit("7%");
                e.Row.Cells[3].Width = new Unit("8%");
                e.Row.Cells[4].Width = new Unit("8%");
                e.Row.Cells[5].Width = new Unit("14%");
                e.Row.Cells[6].Width = new Unit("13%");
                e.Row.Cells[7].Width = new Unit("20%");
                e.Row.Cells[8].Width = new Unit("5%");
                e.Row.Cells[9].Width = new Unit("5%");
                e.Row.Cells[10].Width = new Unit("9%");

                #endregion

                btnView.Visible = btnTransactionPrint.Visible = btnSendToAudit.Visible = true;
            }
            else
            {
                #region Manage GridView Column According To Admin

                e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = e.Row.Cells[12].Visible = e.Row.Cells[13].Visible =
                    e.Row.Cells[14].Visible = false;

                thTransaction.ColSpan = 4;
                thTransNo.Width = "4%";
                thTransDate.Width = "7%";
                thTransType.Width = "7%";
                thTransAmount.Width = "8%";
                thReferenceNo.Width = "8%";
                thReferenceNo.RowSpan = 2;
                thPartyName.Width = "16%";
                thPartyName.RowSpan = 2;
                thDepartmentName.Width = "16%";
                thDepartmentName.RowSpan = 2;
                thNarration.Width = "29%";
                thNarration.RowSpan = 2;

                thAudit.Visible = thPrintTrans.Visible = thSendToAudit.Visible = thUpdate.Visible = thBankPay.Visible = thApprove.Visible = false;

                thAction.ColSpan = 2;
                thView.Width = "5%";

                e.Row.Cells[0].Width = new Unit("4%");
                e.Row.Cells[1].Width = new Unit("7%");
                e.Row.Cells[2].Width = new Unit("7%");
                e.Row.Cells[3].Width = new Unit("8%");
                e.Row.Cells[4].Width = new Unit("8%");
                e.Row.Cells[5].Width = new Unit("16%");
                e.Row.Cells[6].Width = new Unit("16%");
                e.Row.Cells[7].Width = new Unit("29%");
                e.Row.Cells[8].Width = new Unit("5%");

                #endregion

                btnView.Visible = true;
            }
        }
    }

    /// <summary>
    /// Transaction Approval By Superintendent And Account Officer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            lblApprovalMSG.Text = lblApprovalMSG.CssClass = "";

            objPendingVouchersModel = new PendingVouchersModel();
            objPendingVouchersModel.OrgID = GlobalSession.OrgID;
            objPendingVouchersModel.BrID = GlobalSession.BrID;
            objPendingVouchersModel.YrCD = GlobalSession.YrCD;

            string bankPayTransactionNo = "";
            string bankPayDocTypeID = "";
            if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 5)
            {
                DataRow[] row = VsdtBankPayAgainstPurchase.Select("InvoiceNo='" + lblTransactionNo.Text + "'");
                if (row.Count() > 0)
                {
                    bankPayTransactionNo = row[0]["TransactionNo"].ToString();
                    bankPayDocTypeID = row[0]["DocTypeID"].ToString();
                }
                objPendingVouchersModel.BankPayTNo = CommonCls.ConvertIntZero(bankPayTransactionNo);
                objPendingVouchersModel.BankPayVType = CommonCls.ConvertIntZero(bankPayDocTypeID);
            }
            else
            {
                objPendingVouchersModel.BankPayTNo = CommonCls.ConvertIntZero(lblTransactionNo.Text);
                objPendingVouchersModel.BankPayVType = CommonCls.ConvertIntZero(lblDoumentTypeID.Text);
            }

            objPendingVouchersModel.VchType = CommonCls.ConvertIntZero(lblDoumentTypeID.Text);
            objPendingVouchersModel.TransactionNo = CommonCls.ConvertIntZero(lblTransactionNo.Text);
            objPendingVouchersModel.TransactionDate = CommonCls.ConvertToDate(lblTransactionDate.Text);
            objPendingVouchersModel.ApprovalDate = "";
            objPendingVouchersModel.ApprovalRemark = "";
            objPendingVouchersModel.User = GlobalSession.UserID;
            objPendingVouchersModel.IP = GlobalSession.IP;

            string uri = "";

            //Indication For Superintendent Approval
            if (GlobalSession.DesignationID == 26)
            {
                objPendingVouchersModel.Ind = 2;
                objPendingVouchersModel.IsFinal = 1;
                uri = string.Format("PendingVouchers/DataApproved");
            }//Indication For Account Officer Approval
            else if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
            {
                objPendingVouchersModel.Ind = 3;
                objPendingVouchersModel.IsFinal = 2;
                uri = string.Format("PendingVouchers/FinalApproval");
            }

            DataTable dtApproved = CommonCls.ApiPostDataTable(uri, objPendingVouchersModel);
            if (dtApproved.Rows.Count > 0)
            {
                if (dtApproved.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    BindAll();
                    pnlApproval.Visible = false;
                    if (GlobalSession.DesignationID == 26)
                        ShowMessage("Record Approved Successfully With Transaction No. " + lblTransactionNo.Text + ".", true, lblMsg);//ShowMessage("Record Approved Successfully With Transaction No. " + lblTransactionNo.Text + " And " + bankPayTransactionNo + ".", true, lblMsg);
                    else if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                    {
                        string VoucherNo;//,BankPayTNo
                        VoucherNo = dtApproved.Rows[0]["DocMaxNo"].ToString();
                        //BankPayTNo = dtApproved.Rows[0]["BankPayTNo"].ToString();
                        ShowMessage("Record Save Successfully With Voucher No. " + VoucherNo, true, lblMsg);
                    }
                }
                else if (dtApproved.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    pnlApproval.Visible = true;
                    ShowMessage("Record Not Approved, Please Try Again.", false, lblMsg);
                }
            }
            else
            {
                pnlApproval.Visible = true;
                ShowMessage("Record Not Approved, Please Try Again.", false, lblMsg);
            }
        }
        catch (Exception Ex)
        {
            ShowMessage(Ex.Message, false, lblMsg);
        }
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        lblApprovalMSG.Text = lblApprovalMSG.CssClass = "";
        pnlApproval.Visible = false;
    }

    void ShowMessage(string Message, bool type, Label lblErrorMsg)
    {
        lblErrorMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblErrorMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }

    protected void chkPerformOperation_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkPerformOperation = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkPerformOperation.NamingContainer;
        Label lblDocType = (Label)row.FindControl("lblDocType");
        Label lblIsAudit = (Label)row.FindControl("lblIsAudit");
        Label lblIsSendToAudit = (Label)row.FindControl("lblIsSendToAudit");
        Label lblBankPayInd = (Label)row.FindControl("lblBankPayInd");
        Label lblPurchaseBankPayInd = (Label)row.FindControl("lblPurchaseBankPayInd");
        Button btnView = (Button)row.FindControl("btnView");
        Button btnAudit = (Button)row.FindControl("btnAudit");
        Button btnTransactionPrint = (Button)row.FindControl("btnTransactionPrint");
        Button btnSendToAudit = (Button)row.FindControl("btnSendToAudit");
        Button btnUpdate = (Button)row.FindControl("btnUpdate");
        Button btnApprove = (Button)row.FindControl("btnApprove");
        Button btnBankPay = (Button)row.FindControl("btnBankPay");

        if (chkPerformOperation.Checked == true)
        {
            if (GlobalSession.DepartmentID == 1500)
            {
                //Manage GridView Row Buttons (Enabled True / False) According To Account Officer
                if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                {
                    if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
                    {
                        btnView.Enabled = btnApprove.Enabled = true;
                        btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = false;
                    }
                    else if (CommonCls.ConvertIntZero(lblDocType.Text) == 4)
                    {
                        if (Convert.ToString(lblBankPayInd.Text) == "True")
                        {
                            btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnView.Enabled = btnUpdate.Enabled = false;
                            btnApprove.Enabled = true;
                        }
                        else
                        {
                            btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = false;
                            btnView.Enabled = btnApprove.Enabled = true;
                        }
                    }
                    else
                    {
                        btnView.Enabled = btnApprove.Enabled = true;
                        btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = false;
                    }
                    //btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = false;
                    //btnView.Enabled = btnApprove.Enabled = true;
                }//Manage GridView Row Buttons (Enabled True / False) According To Superintendent
                else if (GlobalSession.DesignationID == 26)
                {
                    if (CommonCls.ConvertIntZero(lblIsAudit.Text) == 1)
                    {
                        if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
                        {
                            if (Convert.ToString(lblPurchaseBankPayInd.Text) == "True")
                            {
                                btnUpdate.Enabled = false;
                                btnApprove.Enabled = true;
                            }
                            else
                            {
                                btnUpdate.Enabled = true;
                                btnApprove.Enabled = false;
                            }

                            btnView.Enabled = btnBankPay.Enabled = true;
                            btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = false;
                        }
                        else if (CommonCls.ConvertIntZero(lblDocType.Text) == 4)
                        {
                            if (Convert.ToString(lblBankPayInd.Text) == "True")
                            {
                                btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnView.Enabled = btnUpdate.Enabled = false;
                                btnApprove.Enabled = true;
                            }
                            else
                            {
                                btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = false;
                                btnView.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
                            }
                        }
                        else
                        {
                            btnView.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
                            btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = false;
                        }
                    }
                    else
                    {
                        btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
                        btnView.Enabled = btnAudit.Enabled = true;
                    }
                    //btnView.Enabled = true;
                }
                else
                {
                    //Manage GridView Row Buttons (Enabled True / False) According To Department User
                    if (GlobalSession.DesignationID > 0)
                    {
                        if (CommonCls.ConvertIntZero(lblIsSendToAudit.Text) == 1)
                        {
                            btnTransactionPrint.Enabled = btnSendToAudit.Enabled = false;
                            btnView.Enabled = true;
                        }
                        else
                        {
                            btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                        }
                    }
                    else
                    {
                        btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                        btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
                        btnView.Focus();
                    }
                }
            }
            else
            {
                //Manage GridView Row Buttons (Enabled True / False) According To Department User
                if (GlobalSession.DesignationID > 0)
                {
                    if (CommonCls.ConvertIntZero(lblIsSendToAudit.Text) == 1)
                    {
                        btnTransactionPrint.Enabled = btnSendToAudit.Enabled = false;
                        btnView.Enabled = true;
                    }
                    else
                    {
                        btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                    }
                }
                else
                {
                    btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = true;
                    btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
                    btnView.Focus();
                }
                //btnView.Enabled = btnSendToAudit.Enabled = true;
                //btnAudit.Enabled = btnUpdate.Enabled = btnApprove.Enabled = false;
            }
            btnView.Focus();
            row.BackColor = System.Drawing.Color.Wheat;
            //btnView.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnApprove.Enabled = true;
        }
        else
        {
            row.BackColor = System.Drawing.Color.White;
            btnView.Enabled = btnTransactionPrint.Enabled = btnSendToAudit.Enabled = btnAudit.Enabled = btnUpdate.Enabled = btnBankPay.Enabled = btnApprove.Enabled = false;
        }
    }

    protected void btnAuditYes_Click(object sender, EventArgs e)
    {
        SendToAuditOrOnlyAudit();
    }

    void SendToAuditOrOnlyAudit()
    {
        objPendingVouchersModel = new PendingVouchersModel();

        objPendingVouchersModel.OrgID = GlobalSession.OrgID;
        objPendingVouchersModel.BrID = GlobalSession.BrID;
        objPendingVouchersModel.YrCD = GlobalSession.YrCD;
        objPendingVouchersModel.TransactionNo = CommonCls.ConvertIntZero(lblTransactionNo.Text);
        objPendingVouchersModel.TransactionDate = CommonCls.ConvertToDate(lblTransactionDate.Text);
        objPendingVouchersModel.VchType = CommonCls.ConvertIntZero(lblDoumentTypeID.Text);

        //if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DesignationID > 0)
        if (GlobalSession.DepartmentID != 1500)
            objPendingVouchersModel.Ind = 5;
        else
        {
            if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DesignationID > 0)
                objPendingVouchersModel.Ind = 5;
            else
                objPendingVouchersModel.Ind = 4;
        }

        objPendingVouchersModel.IsAudit = 1;
        //objPendingVouchersModel.IsAudit = 2;

        string uri = string.Format("PendingVouchers/AuditPendingRecords");
        DataTable dtAudit = CommonCls.ApiPostDataTable(uri, objPendingVouchersModel);
        if (dtAudit.Rows.Count > 0)
        {
            if (dtAudit.Rows[0]["ReturnInd"].ToString() == "1")
            {
                //if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DesignationID > 0)
                if (GlobalSession.DepartmentID != 1500)
                    ShowMessage("Record Send To Audit Successfully.", true, lblMsg);
                else
                {
                    if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DesignationID > 0)
                        ShowMessage("Record Send To Audit Successfully With Transaction No. " + dtAudit.Rows[0]["TransNo"].ToString() + ".", true, lblMsg);
                    else
                        ShowMessage("Record Audited Successfully With Transaction No. " + dtAudit.Rows[0]["TransNo"].ToString() + ".", true, lblMsg);
                }

                pnlAuditConfirmation.Visible = false;
                pnlSendToAudit.Visible = false;
                BindAll();
                //grdPendingVouchers.Rows[CommonCls.ConvertIntZero(Session["RowIndex"].ToString())].Focus();
            }
            else
            {
                pnlAuditConfirmation.Visible = true;
                ShowMessage("Record Not Audited Successfully, Please Try Again.", false, lblMsg);
            }
        }
        else
        {
            pnlAuditConfirmation.Visible = true;
            ShowMessage("Record Not Audited Successfully, Please Try Again.", false, lblMsg);
        }
    }

    protected void btnAuditNo_Click(object sender, EventArgs e)
    {
        pnlAuditConfirmation.Visible = false;
    }

    protected void btnApprovalYes_Click(object sender, EventArgs e)
    {
        btnYes_Click(sender, e);
    }

    protected void btnApprovalNo_Click(object sender, EventArgs e)
    {
        pnlApproval.Visible = false;
    }

    #region Sorting

    //protected void rbTransNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbTransNo.Checked == true)
    //    {
    //        DataView dvTransNo = new DataView(VsdtPendingVouchers);
    //        dvTransNo.Sort = "TransactionNo ASC";

    //        grdPendingVouchers.DataSource = dvTransNo.ToTable();
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbTransDate_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbTransDate.Checked == true)
    //    {
    //        DataView dvTransDate = new DataView(VsdtPendingVouchers);
    //        dvTransDate.Sort = "TransactionDate ASC, TransactionNo ASC";

    //        VsdtPendingVouchers = dvTransDate.ToTable();

    //        grdPendingVouchers.DataSource = VsdtPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbTransType_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbTransType.Checked == true)
    //    {
    //        DataView dvTransType = new DataView(VsdtPendingVouchers);
    //        dvTransType.Sort = "DocTypeID ASC";

    //        VsdtPendingVouchers = dvTransType.ToTable();

    //        grdPendingVouchers.DataSource = VsdtPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbAmount_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbAmount.Checked == true)
    //    {
    //        DataView dvAmount = new DataView(VsdtPendingVouchers);
    //        dvAmount.Sort = "NetAmount ASC";

    //        VsdtPendingVouchers = dvAmount.ToTable();

    //        grdPendingVouchers.DataSource = VsdtPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbDepartment_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbDepartment.Checked == true)
    //    {
    //        DataView dvDepartment = new DataView(VsdtPendingVouchers);
    //        dvDepartment.Sort = "DepartmentName ASC";

    //        VsdtPendingVouchers = dvDepartment.ToTable();

    //        grdPendingVouchers.DataSource = VsdtPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbALL_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbALL.Checked == true)
    //    {
    //        grdPendingVouchers.DataSource = VsdtOrgPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbBeforeAudit_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbBeforeAudit.Checked == true)
    //    {
    //        DataView dvBeforeAudit = new DataView(VsdtPendingVouchers);
    //        dvBeforeAudit.RowFilter = "IsAudit=0";

    //        VsdtPendingVouchers = dvBeforeAudit.ToTable();

    //        grdPendingVouchers.DataSource = VsdtPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    //protected void rbAfterAudit_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rbAfterAudit.Checked == true)
    //    {
    //        DataView dvAfterAudit = new DataView(VsdtPendingVouchers);
    //        dvAfterAudit.RowFilter = "IsAudit=1";

    //        VsdtPendingVouchers = dvAfterAudit.ToTable();

    //        grdPendingVouchers.DataSource = VsdtPendingVouchers;
    //        grdPendingVouchers.DataBind();

    //        ManagePendingVoucherGrid();
    //    }
    //}

    #endregion

    protected void btnSendToAuditYes_Click(object sender, EventArgs e)
    {
        SendToAuditOrOnlyAudit();
    }

    protected void btnSendToAuditNo_Click(object sender, EventArgs e)
    {
        pnlSendToAudit.Visible = false;
    }

    void SetPayMode()
    {

        if (ddlPayMode.SelectedValue == "Cheque")
        {
            lblPayModeNo.Text = "Cheque&nbsp;No.";
            lblPayModeDate.Text = "Cheque&nbsp;Date";
            txtReceivedNo.MaxLength = 8;
            txtReceivedNo.CssClass = "numberonly form-control";
        }
        else
        {
            lblPayModeNo.Text = "UTR&nbsp;No.";
            lblPayModeDate.Text = "UTR&nbsp;Date";
            txtReceivedNo.MaxLength = 16;
            txtReceivedNo.CssClass = "form-control";
        }
        txtReceivedNo.Text = txtReceivedDate.Text = "";
    }

    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPayMode();
        txtReceivedNo.Focus();
    }

    protected void btnBankPaySave_Click(object sender, EventArgs e)
    {
        try
        {
            lblBankPayMSG.Text = lblBankPayMSG.CssClass = "";

            if (!ValidationOnBtnBakPay())
            {
                return;
            }

            DataTable dtbank = BankPaymentSchema();

            DataRow dr = dtbank.NewRow();
            dr["OrgID"] = GlobalSession.OrgID;
            dr["BrID"] = GlobalSession.BrID;
            dr["VchType"] = 4;
            dr["YrCD"] = GlobalSession.YrCD;
            dr["DocDate"] = CommonCls.ConvertToDate(txtTransactionDate.Text);
            dr["DocNo"] = 0;
            dr["AccCode"] = CommonCls.ConvertIntZero(lblAccCode.Text);
            dr["AccGst"] = "";
            dr["AccCode2"] = CommonCls.ConvertIntZero(ddlBankAccount.SelectedValue);
            dr["RefNo"] = CommonCls.ConvertIntZero(lblTransactionNo.Text);
            dr["RefDate"] = "";

            dr["AmountCr"] = 0;
            dr["AmountDr"] = Convert.ToDecimal(txtNetAmount.Text);

            if (ddlPayMode.SelectedValue == "Cheque")
            {
                dr["ChequeNo"] = !string.IsNullOrEmpty(txtReceivedNo.Text) ? Convert.ToInt64(txtReceivedNo.Text) : 0;
                dr["ChequeDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
            }
            else if (ddlPayMode.SelectedValue == "UTR")
            {
                dr["UTRNo"] = txtReceivedNo.Text;
                dr["UTRDate"] = CommonCls.ConvertToDate(txtReceivedDate.Text);
            }

            dr["AdvanceInd"] = 0;
            dr["DocDesc"] = txtNarration.Text;

            if (Convert.ToString(lblPurchaseBankPayInd.Text) != "True")
                dr["EntryType"] = 1;
            else
                dr["EntryType"] = 2;

            dr["User"] = GlobalSession.UserID;
            dr["IP"] = GlobalSession.IP;
            dr["IsCapitalRevenue"] = 0;
            dr["BillNo"] = "";
            dr["BillDate"] = "";

            dtbank.Rows.Add(dr);

            string uri = "";
            DataTable dtSaveBankPayment;

            //For Bank Payment Entry Purpose
            if (Convert.ToString(lblPurchaseBankPayInd.Text) != "True")
            {
                BankPaymentModel plbankpay = new BankPaymentModel();
                plbankpay.Ind = 1;
                plbankpay.OrgID = GlobalSession.OrgID;
                plbankpay.BrID = GlobalSession.BrID;
                plbankpay.YrCD = GlobalSession.YrCD;
                plbankpay.VchType = 4;
                plbankpay.CCCode = CommonCls.ConvertIntZero(lblCCCode.Text);
                plbankpay.DeptID = GlobalSession.DepartmentID;
                plbankpay.SubDeptID = GlobalSession.SubDeptID;
                plbankpay.IsFinal = 0;
                plbankpay.IsAudit = 1;
                plbankpay.IsSendToAudit = 1;
                plbankpay.BankPayVoucherInd = 1;
                plbankpay.User = GlobalSession.UserID;
                plbankpay.Dt = JsonConvert.SerializeObject(dtbank);

                uri = string.Format("BankPayment/SaveBankPayment");
                dtSaveBankPayment = CommonCls.ApiPostDataTable(uri, plbankpay);
            }//For Bank Payment Updation Purpose
            else
            {
                UpdBankPaymentModel plUpdbankpay = new UpdBankPaymentModel();
                plUpdbankpay.Ind = 3;
                plUpdbankpay.OrgID = GlobalSession.OrgID;
                plUpdbankpay.BrID = GlobalSession.BrID;
                plUpdbankpay.YrCD = GlobalSession.YrCD;
                plUpdbankpay.VchType = 4;
                plUpdbankpay.DocNo = CommonCls.ConvertIntZero(lblBankPayTransactionNo.Text);
                plUpdbankpay.CCCode = CommonCls.ConvertIntZero(lblCCCode.Text);
                plUpdbankpay.DeptID = GlobalSession.DepartmentID;
                plUpdbankpay.SubDeptID = GlobalSession.SubDeptID;
                plUpdbankpay.IsFinal = 0;
                plUpdbankpay.IsAudit = 1;
                plUpdbankpay.IsSendToAudit = 1;
                plUpdbankpay.BankPayVoucherInd = 1;
                plUpdbankpay.User = GlobalSession.UserID;
                plUpdbankpay.Dt = JsonConvert.SerializeObject(dtbank);

                uri = string.Format("UpdateBankPayment/UpdateBankPay");
                dtSaveBankPayment = CommonCls.ApiPostDataTable(uri, plUpdbankpay);
            }

            if (dtSaveBankPayment.Rows.Count > 0)
            {
                if (dtSaveBankPayment.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    ClearOnBtnBankPayClear();
                    pnlBankPay.Visible = false;
                    BindAll();
                    if (Convert.ToString(lblPurchaseBankPayInd.Text) != "True")
                        ShowMessage("Record Save Successfully With Transaction No. " + dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString() + "", true, lblMsg);
                    else
                        ShowMessage("Record Update Successfully With Transaction No. " + dtSaveBankPayment.Rows[0]["DocMaxNo"].ToString() + "", true, lblMsg);
                }
            }

        }
        catch (Exception Ex)
        {

        }
    }

    DataTable BankPaymentSchema()
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
        dt.Columns.Add("IsCapitalRevenue", typeof(int));
        dt.Columns.Add("BillNo", typeof(string));
        dt.Columns.Add("BillDate", typeof(string));

        return dt;
    }

    bool ValidationOnBtnBakPay()
    {
        if (string.IsNullOrEmpty(txtTransactionDate.Text))
        {
            ShowMessage("Enter Transaction Date.", false, lblBankPayMSG);
            txtTransactionDate.Focus();
            return false;
        }

        // For Voucher Date Between Financial Year.
        bool ValidDate = CommonCls.CheckFinancialYrDate(txtTransactionDate.Text, GlobalSession.YrStartDate, DateTime.Now.ToString("dd/MM/yyyy"));
        if (!ValidDate)
        {
            txtTransactionDate.Focus();
            ShowMessage("Transaction Date Should Be Within Financial Year Date!", false, lblBankPayMSG);
            return false;
        }

        if (CommonCls.ConvertIntZero(ddlBankAccount.SelectedValue) == 0)
        {
            ShowMessage("Select Bank Account.", false, lblBankPayMSG);
            ddlBankAccount.Focus();
            return false;
        }

        if (ddlPayMode.SelectedValue == "Cheque")
        {
            if (CommonCls.ConvertIntZero(txtReceivedNo.Text) == 0)
            {
                ShowMessage("Enter Cheque No.", false, lblBankPayMSG);
                txtReceivedNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtReceivedDate.Text))
            {
                ShowMessage("Enter Cheque Date.", false, lblBankPayMSG);
                txtReceivedDate.Focus();
                return false;
            }
        }
        else
        {
            if (string.IsNullOrEmpty(txtReceivedNo.Text))
            {
                ShowMessage("Enter UTR No.", false, lblBankPayMSG);
                txtReceivedNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtReceivedDate.Text))
            {
                ShowMessage("Enter UTR Date.", false, lblBankPayMSG);
                txtReceivedDate.Focus();
                return false;
            }
        }

        bool ReceivedDate = CommonCls.CheckFinancialYrDate(txtReceivedDate.Text, DateTime.Now.AddDays(-89).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));//local
        if (!ReceivedDate) // For Voucher Date Between Financial Year.
        {
            if (ddlPayMode.SelectedValue == "Cheque")
            {
                ShowMessage("Cheque Date Should Be Within 3 Months Or Not More Than Todays Date!", false, lblBankPayMSG);
                return false;
            }
            else if (ddlPayMode.SelectedValue == "UTR")
            {
                ShowMessage("UTR Date Should Be Within Financial Year And Not More Than Todays Date!", false, lblBankPayMSG);
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtNarration.Text))
        {
            ShowMessage("Enter Narration.", false, lblBankPayMSG);
            txtNarration.Focus();
            return false;
        }
        return true;
    }

    protected void btnBankPayClear_Click(object sender, EventArgs e)
    {
        ClearOnBtnBankPayClear();
        pnlBankPay.Visible = false;
    }

    void ClearOnBtnBankPayClear()
    {
        ddlBankAccount.ClearSelection();
        ddlPayMode.ClearSelection();
        SetPayMode();
        txtTransactionDate.Text = txtNetAmount.Text = txtReceivedNo.Text = txtReceivedDate.Text = txtNarration.Text =
            txtPartyName.Text = lblBankPayMSG.Text = lblBankPayMSG.CssClass = "";
    }

    protected void grdCompleteTransactionList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = lblMsg.CssClass = "";

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dtCompleteList = VsdtCompleteList;
            DataRow drCompleteList = dtCompleteList.Rows[rowIndex];

            if (e.CommandName == "PrintBankPayRow")
            {
                Session["Report"] = "RptVoucher";//Report Name
                Hashtable HT = new Hashtable();
                HT.Add("Ind", 1);
                HT.Add("OrgID", GlobalSession.OrgID);
                HT.Add("BrID", GlobalSession.BrID);
                HT.Add("yrcode", GlobalSession.YrCD);
                HT.Add("CompName", GlobalSession.OrgName);
                HT.Add("BranchName", GlobalSession.BrName);
                string VoucherNo = "";
                string VoucherDate = "";
                if (CommonCls.ConvertIntZero(drCompleteList["DocTypeID"].ToString()) == 2)
                {
                    VoucherNo = drCompleteList["VoucharNo"].ToString();
                    VoucherDate = drCompleteList["VoucharDate"].ToString();
                    HT.Add("Heading", "CASH PAYMENT VOUCHER");
                    HT.Add("DocTypeID", 2);
                }
                else if (CommonCls.ConvertIntZero(drCompleteList["DocTypeID"].ToString()) == 4)
                {
                    VoucherNo = drCompleteList["VoucharNo"].ToString();
                    VoucherDate = drCompleteList["VoucharDate"].ToString();
                    HT.Add("Heading", "BANK PAYMENT VOUCHER");
                    HT.Add("DocTypeID", 4);
                }
                else if (CommonCls.ConvertIntZero(drCompleteList["DocTypeID"].ToString()) == 5)
                {
                    DataRow[] bankPayAgainstPurchaseRow = VsdtCmpBankPayAgainstPurchase.Select("InvoiceNo='" + drCompleteList["TransactionNo"].ToString() + "'");
                    if (bankPayAgainstPurchaseRow.Count() > 0)
                    {
                        VoucherNo = bankPayAgainstPurchaseRow[0]["VoucharNo"].ToString();
                        VoucherDate = bankPayAgainstPurchaseRow[0]["VoucharDate"].ToString();
                    }

                    HT.Add("Heading", "BANK PAYMENT VOUCHER");
                    HT.Add("DocTypeID", 4);
                }

                HT.Add("Voucharno", CommonCls.ConvertIntZero(VoucherNo));
                HT.Add("VoucharDate", VoucherDate.Substring(6, 4) + "/" + VoucherDate.Substring(3, 2) + "/" + VoucherDate.Substring(0, 2));
                Session["HT"] = HT;
                Session["format"] = "Pdf";
                Session["FileName"] = "CashBankVoucher";
                Response.Redirect("../Reports/FrmReportViewer.aspx");
            }
            else if (e.CommandName == "PrintPurchaseRow")
            {
                Session["Report"] = "RptPurchaseVoucher";

                Hashtable HT = new Hashtable();
                HT.Add("Ind", 1);

                HT.Add("OrgID", GlobalSession.OrgID);
                HT.Add("BrID", GlobalSession.BrID);
                HT.Add("yrcode", GlobalSession.YrCD);
                HT.Add("CompName", GlobalSession.OrgName);
                HT.Add("BranchName", GlobalSession.BrName);
                HT.Add("Heading", "PURCHASE VOUCHER");

                HT.Add("Doctype", 5);
                HT.Add("invoiceno", CommonCls.ConvertIntZero(drCompleteList["VoucharNo"].ToString()));
                HT.Add("invoiceDate", drCompleteList["VoucharDate"].ToString().Substring(6, 4) + "/" + drCompleteList["VoucharDate"].ToString().Substring(3, 2) + "/" + drCompleteList["VoucharDate"].ToString().Substring(0, 2));
                HT.Add("invoiceDateFrom", "");
                HT.Add("invoiceDateto", "");
                HT.Add("cashsalesind", 1);
                HT.Add("vNO", Convert.ToInt32(drCompleteList["VoucharNo"].ToString()));

                Session["HT"] = HT;
                Session["format"] = "Pdf";
                Session["FileName"] = "PurchaseVoucher";
                Response.Redirect("../Reports/FrmReportViewer.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdCompleteTransactionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btnPurchasePrint = (Button)e.Row.FindControl("btnPurchasePrint");
            Button btnBankPayPrint = (Button)e.Row.FindControl("btnBankPayPrint");
            Label lblDocType = (Label)e.Row.FindControl("lblDocType");
            Label lblTransactionAmount = (Label)e.Row.FindControl("lblTransactionAmount");
            Label lblBankPayVoucherInd = (Label)e.Row.FindControl("lblBankPayVoucherInd");

            //lblTransactionAmount.Text = CommonCls.ConverToCommas(lblTransactionAmount.Text);

            if (CommonCls.ConvertIntZero(lblDocType.Text) == 5)
            {
                btnPurchasePrint.Visible = btnBankPayPrint.Visible = true;

                if (lblBankPayVoucherInd.Text == "True")
                    btnBankPayPrint.Enabled = true;
                else
                    btnBankPayPrint.Enabled = false;

                btnBankPayPrint.Text = "Bank Pay";
            }
            else
            {
                btnPurchasePrint.Visible = false;
                btnBankPayPrint.Visible = true;

                if (CommonCls.ConvertIntZero(lblDocType.Text) == 2)
                    btnBankPayPrint.Text = "Cash Pay";
                else
                    btnBankPayPrint.Text = "Bank Pay";
            }
        }
    }

    protected void grdSTA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PrintRow")
        {
            lblMsg.Text = lblMsg.CssClass = "";

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dtSTA = VsdtSTA;
            DataRow drSTA = dtSTA.Rows[rowIndex];

            lblTransactionNo.Text = drSTA["TransactionNo"].ToString();
            lblTransactionDate.Text = drSTA["TransactionDate"].ToString();
            lblDoumentTypeID.Text = drSTA["DocTypeID"].ToString();

            if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 4 || CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 2)
            {
                Session["Report"] = "RptVoucher";

                Hashtable HT = new Hashtable();

                HT.Add("CompName", GlobalSession.OrgName);
                HT.Add("BranchName", GlobalSession.BrName);

                if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 4)
                    HT.Add("Heading", "Bank Payment");
                else
                    HT.Add("Heading", "Cash Payment");

                HT.Add("Ind", 2);
                HT.Add("OrgID", GlobalSession.OrgID);
                HT.Add("BrID", GlobalSession.BrID);
                HT.Add("yrcode", 0);

                HT.Add("DocTypeID", CommonCls.ConvertIntZero(lblDoumentTypeID.Text));
                HT.Add("Voucharno", CommonCls.ConvertIntZero(lblTransactionNo.Text));
                HT.Add("VoucharDate", lblTransactionDate.Text.Substring(6, 4) + "/" + lblTransactionDate.Text.Substring(3, 2) + "/" + lblTransactionDate.Text.Substring(0, 2));

                Session["HT"] = HT;
                Session["format"] = "Pdf";
                Session["FileName"] = "PurchaseVoucher";
                Response.Redirect("../Reports/FrmReportViewer.aspx");
            }
            else if (CommonCls.ConvertIntZero(lblDoumentTypeID.Text) == 5)
            {
                Session["Report"] = "RptPurchaseVoucher";

                Hashtable HT = new Hashtable();

                HT.Add("Ind", 2);
                HT.Add("OrgID", GlobalSession.OrgID);
                HT.Add("BrID", GlobalSession.BrID);
                HT.Add("yrcode", 0);
                HT.Add("CompName", GlobalSession.OrgName);
                HT.Add("BranchName", GlobalSession.BrName);
                HT.Add("Heading", "PURCHASE VOUCHER");

                HT.Add("Doctype", CommonCls.ConvertIntZero(lblDoumentTypeID.Text));
                HT.Add("invoiceno", CommonCls.ConvertIntZero(lblTransactionNo.Text));
                HT.Add("invoiceDate", lblTransactionDate.Text.Substring(6, 4) + "/" + lblTransactionDate.Text.Substring(3, 2) + "/" + lblTransactionDate.Text.Substring(0, 2));
                HT.Add("invoiceDateFrom", "");
                HT.Add("invoiceDateto", "");
                HT.Add("cashsalesind", 0);
                HT.Add("vNO", CommonCls.ConvertIntZero(lblTransactionNo.Text));

                Session["HT"] = HT;
                Session["format"] = "Pdf";
                Session["FileName"] = "PurchaseVoucher";
                Response.Redirect("../Reports/FrmReportViewer.aspx");
            }
        }
    }
}