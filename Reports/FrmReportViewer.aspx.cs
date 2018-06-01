using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Drawing.Printing;
public partial class FrmReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfprev.Value = Request.UrlReferrer.AbsoluteUri;
            pnlPrintDraftPatta.Visible = true;

            function();
           // downloadpdf();
        }
    }
    public void function()
    {
        ReportViewer1.ShowCredentialPrompts = true;
        Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["ReportLoginName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString(), ConfigurationManager.AppSettings["ReportServer"].ToString());
        //Microsoft.Reporting.WebForms.IReportServerCredentials irsc = new CustomReportCredentials("oswalrpt\\occplrpt", "PqRs&@#$WXYZ", ConfigurationManager.AppSettings["ReportServer"].ToString());
        ReportViewer1.ServerReport.ReportServerCredentials = irsc;
        Hashtable HT = new Hashtable();
        HT = (Hashtable)Session["HT"];
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"].ToString());
        ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportProjectName"].ToString() + Session["Report"].ToString();
        if (HT != null)
        {
            ReportParameter[] parm = new ReportParameter[HT.Count];
            int i = 0;
            foreach (DictionaryEntry Dt in HT)
            {
                parm[i] = new ReportParameter(Convert.ToString(Dt.Key), Convert.ToString(Dt.Value));
                i++;
            }
            ReportViewer1.ServerReport.SetParameters(parm);
            ReportViewer1.ServerReport.Refresh();
          //  getpdf();
        }

    }
    //public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    //{
    //    private string _UserName;
    //    private string _PassWord;
    //    private string _DomainName;
    //    public CustomReportCredentials(string UserName, string PassWord, string DomainName)
    //    {
    //        _UserName = UserName;
    //        _PassWord = PassWord;
    //        _DomainName = DomainName;
    //    }
    //    public System.Security.Principal.WindowsIdentity ImpersonationUser
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //    }
    //    public ICredentials NetworkCredentials
    //    {
    //        get
    //        {
    //            return new NetworkCredential(_UserName, _PassWord, _DomainName);
    //        }
    //    }
    //    public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
    //    {

    //        authCookie = null;
    //        user = password = authority = null;
    //        return false;
    //    }
    //}
    private void downloadpdf()
    {
        string mimeType, encoding, extension, deviceInfo;
        string[] streamids;
        Microsoft.Reporting.WebForms.Warning[] warnings;

        string format = "";
        if (Session["format"] != null)
        {
            format = Session["format"].ToString(); //Desired format goes here (PDF, Excel, or Image)
        }
        else
        {
            format = "Excel";
        }

        deviceInfo = "<DeviceInfo>+<SimplePageHeaders>True</SimplePageHeaders>+</DeviceInfo>";

        byte[] bytes = ReportViewer1.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
       // Response.Clear();
        System.IO.File.WriteAllBytes(Server.MapPath("~/TempFiles/Print.pdf"), bytes);
        //Attach javascript to the document
       
        frmPrint.Attributes["src"] = "~/TempFiles/Print.pdf";
        //if (format == "Pdf")
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + Session["FileName"] + ".pdf");
        //}
        //else if (format == "Excel")
        //{
        //    Response.ContentType = "application/excel";
        //    Response.AddHeader("Content-disposition", "filename=" + Session["FileName"] + ".xls");
        //}

       // Response.OutputStream.Write(bytes, 0, bytes.Length);

    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect(hfprev.Value);
    }

    //public  = null;
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

    //    FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/TempFiles/output" +GlobalSession.UserID.ToString() + ".pdf"),
    //    FileMode.Create);
    //    fs.Write(bytes, 0, bytes.Length);
    //    fs.Close();

    //    //Open existing PDF
    //  //  Document document = new Document(PageSize.A4);
    //   // PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath("~/TempFiles/output" + Session["UserID"].ToString() + ".pdf"));
    //    //Getting a instance of new PDF writer
    //    //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
    //    //   HttpContext.Current.Server.MapPath("~/TempFiles/Print.pdf"), FileMode.Create));
    //    //document.Open();
    //    //PdfContentByte cb = writer.DirectContent;

    //    //int i = 0;
    //    //int p = 0;
    //    //int n = reader.NumberOfPages;
    //    //Rectangle psize = reader.GetPageSize(1);

    //    //float width = psize.Width;
    //    //float height = psize.Height;

    //    ////Add Page to new document
    //    //while (i < n)
    //    //{
    //    //    document.NewPage();
    //    //    p++;
    //    //    i++;

    //    //    PdfImportedPage page1 = writer.GetImportedPage(reader, i);
    //    //    cb.AddTemplate(page1, 0, 0);
    //    //}
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

    protected void print_Click(object sender, EventArgs e)
    {
        getpdf();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$(document).ready(function(){printDocument();});", true);
    }
}