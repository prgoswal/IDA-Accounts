using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_MainMaster : System.Web.UI.MasterPage
{
    UserModel objUserModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(GlobalSession.OrgName))
        {
            Response.Redirect("~/Logout.aspx");
        }

        if (!IsPostBack)
        {
            if (GlobalSession.IsAdmin == 1)
                lblUserName.Text = GlobalSession.UserName + " (Admin)";
            else if (GlobalSession.IsAdmin == 0)
            {
                lblUserName.Text = GlobalSession.UserName;
                liUpdateProfile.Disabled = true;
            }

            //ManageMenuByUserRights();


            lblOrgName.Text = GlobalSession.OrgName;
            lblFinancialYr.Text = GlobalSession.FinancialYr;

            if (GlobalSession.DepartmentID == 0)
                lblFinaYear.Text = GlobalSession.FinancialYr;


            //lblBranch.Text = GlobalSession.BrName;
            hfCompositionOpted.Value = GlobalSession.CompositionOpted.ToString();
            hfUnregistered.Value = GlobalSession.UnRegisterClient.ToString();

            //if (GlobalSession.IsAdmin == 1 || GlobalSession.HOUser == 1 || GlobalSession.IsAdmin == 0)
            //{
            //    liModification.Visible = true;
            //    liUserCreation.Visible = true;
            //    liUserRights.Visible = true;
            //    liUpdateProfile.Visible = true;
            //}


            //For Account Admin
            if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID == 0 && GlobalSession.SubDeptID == 0 && GlobalSession.DesignationID == 0)
            {
                liBudgetMaster.Visible = true; liBudget.Visible = liReport.Visible = true;
                lblUserName.Text = GlobalSession.UserName + " (Account Admin)";
                liMaster.Visible = liVoucher.Visible = liReport.Visible = liLedger.Visible = liFinalAccounts.Visible = liOthers.Visible = liGst.Visible =
                    liUtility.Visible = liModification.Visible = liVoucherUpdate.Visible = liCalculator.Visible = lipendingVouchers.Visible = true;

                liCashReceipt.Visible = liBankReceipt.Visible = liJournal.Visible = liPurchaseReturn.Visible = liStockTransfer.Visible = true;
                liCashPayment.Visible = liBankPayment.Visible = liPurchase.Visible = false;

            }//for all department head but not a finance department head
            else if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0)
            {
                lblUserName.Text = GlobalSession.UserName + " (Department Head)";

                if (GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26)
                {
                    if (GlobalSession.DepartmentID != 1500)
                    {
                        liVoucher.Visible = liPurchase.Visible = lipendingVouchers.Visible = true;//liCashPayment.Visible = liBankPayment.Visible = 
                    }
                    else
                    {
                        lblUserName.Text = GlobalSession.UserName + " (Department Head)";

                        liMaster.Visible = liVoucher.Visible = liReport.Visible = liLedger.Visible = liFinalAccounts.Visible = liOthers.Visible = liGst.Visible =
                            liUtility.Visible = liModification.Visible = liVoucherUpdate.Visible = liCalculator.Visible = lipendingVouchers.Visible = true;

                        liCashReceipt.Visible = liBankReceipt.Visible = liJournal.Visible = liPurchaseReturn.Visible = liStockTransfer.Visible =
                        liCashPayment.Visible = liBankPayment.Visible = liPurchase.Visible = true;

                        liBudgetMaster.Visible = liReport.Visible = true;//liBudget.Visible = 
                    }

                    lblFinaYear.Text = GlobalSession.FinancialYr + " (Department User)";
                }
                else if (GlobalSession.DesignationID != 11 || GlobalSession.DesignationID != 17 || GlobalSession.DesignationID != 26)
                {
                    if (GlobalSession.DepartmentID == 1500)
                    {
                        lblUserName.Text = GlobalSession.UserName + " (Department Head)";
                        liReport.Visible = lipendingVouchers.Visible = true;//liBudget.Visible = 

                        //if (GlobalSession.BudgetConcept == 1)
                        //    liBudget.Visible = true;
                        //else
                        //    liBudget.Visible = false;

                        if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
                            lblFinaYear.Text = GlobalSession.FinancialYr + " (Account Officer)";//liVoucher.Visible = liCashPayment.Visible = liBankPayment.Visible = true;
                        else if (GlobalSession.DesignationID == 26)
                            lblFinaYear.Text = GlobalSession.FinancialYr + " (Superintendent)";
                        else
                            lblFinaYear.Text = GlobalSession.FinancialYr + " (Department User)";
                    }
                    else
                    {
                        liVoucher.Visible = liPurchase.Visible = lipendingVouchers.Visible = true;//liCashPayment.Visible = liBankPayment.Visible = 
                        lblFinaYear.Text = GlobalSession.FinancialYr + " (Department User)";
                    }
                }

                //if (GlobalSession.BudgetConcept == 1)
                //    liBudget.Visible = true;
                //else
                //    liBudget.Visible = false;

            }
            else if (GlobalSession.IsAdmin == 0 && GlobalSession.DepartmentID == 1500 && (GlobalSession.SubDeptID > 1500 && GlobalSession.SubDeptID < 1600) && GlobalSession.DesignationID > 0)
            {
                lblUserName.Text = GlobalSession.UserName + " (Department User)";
                lblFinaYear.Text = GlobalSession.FinancialYr + " (Department User)";

                liMaster.Visible = liVoucher.Visible = liReport.Visible = liLedger.Visible = liFinalAccounts.Visible = liOthers.Visible = liGst.Visible =
                            liUtility.Visible = liModification.Visible = liVoucherUpdate.Visible = liCalculator.Visible = lipendingVouchers.Visible = true;

                liCashReceipt.Visible = liBankReceipt.Visible = liJournal.Visible = liPurchaseReturn.Visible = liStockTransfer.Visible =
                liCashPayment.Visible = liBankPayment.Visible = liPurchase.Visible = true;

                liBudgetMaster.Visible = liReport.Visible = true;//liBudget.Visible = 
            }
            else if (GlobalSession.IsAdmin == 0 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID > 0)
            {
                lblUserName.Text = GlobalSession.UserName + " (Department User)";
                lblFinaYear.Text = GlobalSession.FinancialYr + " (Department User)";

                liCashReceipt.Visible = liBankReceipt.Visible = liJournal.Visible = liPurchaseReturn.Visible = liStockTransfer.Visible = false;
                liVoucher.Visible = liPurchase.Visible = true;//liCashPayment.Visible = liBankPayment.Visible = 
                lipendingVouchers.Visible = true;
            }

            liBudgetTransaction.Visible = true;

            if (GlobalSession.BudgetConcept == 1)
                liBudget.Visible = true;
            else
                liBudget.Visible = false;

            if (GlobalSession.BankPayChqSeriesInd == 1)
                liChequeSeries.Visible = true;
            else
                liChequeSeries.Visible = false;

            //if (GlobalSession.BudgetConcept == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID > 0)
            
            //for finance department head without designation (Superintendent(26) And Account Officer(11,17))
            //else if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0
            //&& GlobalSession.DesignationID != 11 && GlobalSession.DesignationID != 17 && GlobalSession.DesignationID != 26 && GlobalSession.DepartmentID == 1500)
            //{
            //    lblUserName.Text = GlobalSession.UserName + " (Department Head)";

            //    liMaster.Visible = liVoucher.Visible = liReport.Visible = liLedger.Visible = liFinalAccounts.Visible = liOthers.Visible = liGst.Visible =
            //        liUtility.Visible = liModification.Visible = liVoucherUpdate.Visible = liCalculator.Visible = lipendingVouchers.Visible = true;

            //    liCashReceipt.Visible = liBankReceipt.Visible = liJournal.Visible = liPurchaseReturn.Visible = liStockTransfer.Visible = 
            //    liCashPayment.Visible = liBankPayment.Visible = liPurchase.Visible = true;

            //    liBudgetMaster.Visible = true; liBudget.Visible = liReport.Visible = true;
            //}//for finance department head with designation (Superintendent(26) And Account Officer(11,17))
            //else if (GlobalSession.IsAdmin == 1 && GlobalSession.DepartmentID > 0 && GlobalSession.SubDeptID == 0
            //    && (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17 || GlobalSession.DesignationID == 26))
            //{
            //    lblUserName.Text = GlobalSession.UserName + " (Department Head)";
            //    liBudget.Visible = liReport.Visible = lipendingVouchers.Visible = true;

            //    if (GlobalSession.BudgetConcept == 1)
            //        liBudget.Visible = true;
            //    else
            //        liBudget.Visible = false;

            //    if (GlobalSession.DesignationID == 11 || GlobalSession.DesignationID == 17)
            //        lblFinaYear.Text = GlobalSession.FinancialYr + " (Account Officer)";
            //    else if (GlobalSession.DesignationID == 26)
            //        lblFinaYear.Text = GlobalSession.FinancialYr + " (Superintendent)";
            //}//for sub- department user
            //if (GlobalSession.IsAdmin == 1)
            //{
            //    liBudgetTransaction.Visible = true;
            //}
            //else if (GlobalSession.IsAdmin == 1 && GlobalSession.BudgetConcept == 1)
            //{
            //    liBudget.Visible = liReport.Visible = true;
            //}
            //if (GlobalSession.DepartmentID == 1500 && (GlobalSession.SubDeptID >= 1501 && GlobalSession.SubDeptID < 1600))
            //{
            //    liVoucher.Visible = true;
            //    liReport.Visible = true;
            //    liModification.Visible = true;
            //    liMasterUpdate.Visible = false;
            //}
        }
    }
    #region OLDCode
    //private void ManageMenuByUserRights()
    //{
    //    try
    //    {
    //        Session["MasterWrite"] = 0;
    //        Session["VoucherCancel"] = 0;
    //        Session["VoucherWrite"] = 0;
    //        Session["VoucherUpdate"] = 0;
    //        Session["VoucherRead"] = 0;
    //        #region IsAdmin

    //        if (GlobalSession.IsAdmin == 1 || GlobalSession.IsAdmin == 0)
    //        {
    //            liModification.Visible = true;
    //            liMaster.Visible = true;
    //            liVoucher.Visible = true;
    //            liMasterUpdate.Visible = true;
    //            liVoucherUpdate.Visible = true;
    //            liReport.Visible = true;
    //            liBooks.Visible = true;
    //            liLedger.Visible = true;
    //            liFinalAccounts.Visible = true;
    //            liOthers.Visible = true;
    //            liUtility.Visible = true;
    //            liGst.Visible = true;
    //        }
    //        #endregion

    //        #region Rights
    //        DataTable dt = CommonCls.GetAllottedMenuDetails();

    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                //71 for utility Show
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 71)
    //                {
    //                    liUtility.Visible = true;
    //                }
    //                // 81 For Gst Return show
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 81)
    //                {
    //                    liGst.Visible = true;
    //                }

    //                //52 For Final Acc hide
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 52)
    //                {

    //                }

    //                //51 For Final Acc show
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 51)
    //                {
    //                    liReport.Visible = true;
    //                    liFinalAccounts.Visible = true;
    //                }


    //                //62 For others hide
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 62)
    //                {

    //                }


    //                //61 For Others show
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 61)
    //                {
    //                    liReport.Visible = true;
    //                    liOthers.Visible = true;
    //                }


    //                //42 For Ledger hide
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 42)
    //                {


    //                }

    //                //41 For Ledger show
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 41)
    //                {
    //                    liReport.Visible = true;
    //                    liLedger.Visible = true;

    //                }

    //                //32 for Books hide
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 32)
    //                {

    //                }

    //                //31 for Books Show
    //                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 31)
    //                {
    //                    liReport.Visible = true;
    //                    liBooks.Visible = true;
    //                }

    //                //11 for Master Read
    //                if (Convert.ToInt32(dt.Rows[i]["MenuId"].ToString()) == 11)
    //                {
    //                    liModification.Visible = true;
    //                    liMaster.Visible = true;
    //                    liMasterUpdate.Visible = true;
    //                }


    //                //12 for Master Write
    //                if (Convert.ToInt32(dt.Rows[i]["MenuId"].ToString()) == 12)
    //                {
    //                    liModification.Visible = true;

    //                    liMaster.Visible = true;
    //                    liMasterUpdate.Visible = true;
    //                    Session["MasterWrite"] = Convert.ToInt32(dt.Rows[i]["MenuId"].ToString());
    //                }


    //                //21 For Voucher Read
    //                if (Convert.ToInt32(dt.Rows[i]["MenuId"].ToString()) == 21)
    //                {
    //                    liModification.Visible = true;

    //                    liVoucherUpdate.Visible = true;
    //                    liVoucher.Visible = true;
    //                    Session["VoucherRead"] = Convert.ToInt32(dt.Rows[i]["MenuId"].ToString());

    //                    UserRights.VRead = true;
    //                }

    //                //22 For Voucher Write
    //                if (Convert.ToInt32(dt.Rows[i]["MenuId"].ToString()) == 22)
    //                {
    //                    liModification.Visible = true;

    //                    liVoucher.Visible = true;
    //                    liVoucherUpdate.Visible = true;
    //                    Session["VoucherWrite"] = Convert.ToInt32(dt.Rows[i]["MenuId"].ToString());

    //                    UserRights.VWrite = true;
    //                }

    //                //23 For Voucher Update
    //                if (Convert.ToInt32(dt.Rows[i]["MenuId"].ToString()) == 23)
    //                {
    //                    liModification.Visible = true;

    //                    liVoucher.Visible = true;
    //                    liVoucherUpdate.Visible = true;
    //                    Session["VoucherUpdate"] = Convert.ToInt32(dt.Rows[i]["MenuId"].ToString());

    //                    UserRights.VUpdation = true;
    //                }


    //                //24 For Voucher Cancel
    //                if (Convert.ToInt32(dt.Rows[i]["MenuId"].ToString()) == 24)
    //                {
    //                    liModification.Visible = true;

    //                    liVoucher.Visible = true;
    //                    liVoucherUpdate.Visible = true;
    //                    Session["VoucherCancel"] = Convert.ToInt32(dt.Rows[i]["MenuId"].ToString());

    //                    UserRights.VCancel = true;
    //                }

    //            }
    //        }
    //        else
    //        {
    //            UserRights.VCancel = false;
    //            UserRights.VRead = false;
    //            UserRights.VUpdation = false;
    //            UserRights.VWrite = false;

    //            Session["MasterWrite"] = 0;
    //            Session["VoucherCancel"] = 0;
    //            Session["VoucherWrite"] = 0;
    //            Session["VoucherUpdate"] = 0;
    //            Session["VoucherRead"] = 0;
    //        }
    //        #endregion

    //    }
    //    catch (Exception)
    //    {

    //    }
    //}

    //[System.Web.Services.WebMethod]
    //public static string ValidGstin()
    //{
    //    return CommonCls.CheckGUIDIsValid();
    //}
    #endregion
}
