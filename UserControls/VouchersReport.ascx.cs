using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_VouchersReport : System.Web.UI.UserControl
{

    public Hashtable HashTable
    {
        get { return (Hashtable)ViewState["HashTable"]; }
        set { ViewState["HashTable"] = value; }
    }

    public string FileName
    {
        get { return ViewState["FileName"].ToString(); }
        set { ViewState["FileName"] = value; }
    }

    public string ReportName
    {
        get { return ViewState["ReportName"].ToString(); }
        set { ViewState["ReportName"] = value; }
    }

    public string ReportHeading
    {
        set { lblReportHeading.Text = value; }
    }

    public bool AskBeforePrint { get; set; }

    public string AskMessage
    {
        set { lblAskMessage.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //public event EventHandler UserControlButtonClicked;

    // For Sale Invoice Print Confirmation.
    protected void btnYes_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;
        AskBeforePrint = false;
        ShowReport();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        pnlConfirmInvoice.Visible = false;

        BackTpPendingTransaction();

        //UserControlButtonClicked(this, EventArgs.Empty);
        //this.Parent.FindControl("txtVoucherDate").Focus();        

        //CommonCls.ScriptInInline("$('input[type=\"text\"]')[0].focus()");
        CommonCls.ScriptInInline("$('form :input:text:visible:enabled:not(input[class*=filter]):first').focus();");
    }

    void BackTpPendingTransaction()
    {
        if (Session["DocTypeID"] != null)
        {
            Session["TransNo"] = Session["TransDate"] = null;

            if (Session["DocTypeID"].ToString() == "2" || Session["DocTypeID"].ToString() == "4" || Session["DocTypeID"].ToString() == "5")
            {
                Session["DocTypeID"] = null;
                Response.Redirect("../Vouchers/FrmPendingVouchers.aspx");
            }
        }
    }

    protected void btnCloseRpt_Click(object sender, EventArgs e)
    {
        pnlInvoiceReport.Visible = false;

        BackTpPendingTransaction();

        //CommonCls.ScriptInInline("$('input[type=\"text\"]')[0].focus()");
        //CommonCls.ScriptInInline("$('input:enabled')[0].focus()");
        CommonCls.ScriptInInline("$('form :input:text:visible:enabled:not(input[class*=filter]):first').focus();");
    }

    protected void print_Click(object sender, EventArgs e)
    {
        getpdf();
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "CallMyFunction", "$(document).ready(function(){printDocument();});", true);
        CommonCls.ScriptFunction("printDocument");
    }

    //void getpdf()
    //{
    //    frmPrint.Attributes.Remove("src");
    //    Warning[] warnings;
    //    string[] streamids;
    //    string mimeType;
    //    string encoding;
    //    string extension;

    //    byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType,
    //                   out encoding, out extension, out streamids, out warnings);

    //    FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/TempFiles/output" + GlobalSession.UserID.ToString() + ".pdf"),
    //    FileMode.Create);
    //    fs.Write(bytes, 0, bytes.Length);
    //    fs.Close();
    //    string StrPath = "~/TempFiles/output" + GlobalSession.UserID.ToString() + ".pdf";
    //    frmPrint.Attributes["src"] = StrPath;
    //}

    void getpdf()
    {
        string[] files = Directory.GetFiles(Server.MapPath("~/TempFiles"));
        foreach (string s in files)
        {
            if (s.Contains("_" + GlobalSession.UserID.ToString() + "_"))
            {
                File.Delete(s);
            }
        }

        string StrPath = "~/TempFiles/output_" + GlobalSession.UserID.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

        frmPrint.Attributes.Remove("src");
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(StrPath),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();

        frmPrint.Attributes["src"] = StrPath;
    }

    public void ShowReport()
    {
        if (AskBeforePrint)
        {
            pnlConfirmInvoice.Visible = true;
            btnYes.Focus();
            return;
        }

        pnlInvoiceReport.Visible = true;

        ReportViewer1.ShowCredentialPrompts = true;
        Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["ReportLoginName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["ReportServer"].ToString());//"http://occweb02/ReportServer");
        ReportViewer1.ServerReport.ReportServerCredentials = irsc;

        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"].ToString());
        ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportProjectName"].ToString() + ReportName; //Session["Report"].ToString();
        if (HashTable != null)
        {
            ReportParameter[] parm = new ReportParameter[HashTable.Count];
            int i = 0;
            foreach (DictionaryEntry Dt in HashTable)
            {
                parm[i] = new ReportParameter(Convert.ToString(Dt.Key), Convert.ToString(Dt.Value));
                i++;
            }
            ReportViewer1.ServerReport.SetParameters(parm);
            ReportViewer1.ServerReport.Refresh();
            DisposeVS();
            btnPrint.Focus();
        }
    }

    void DisposeVS()
    {
        FileName = ReportName = "";
        HashTable = null;
    }

}

