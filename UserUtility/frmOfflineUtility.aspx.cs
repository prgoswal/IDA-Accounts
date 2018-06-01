using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.IO.Compression;
using Ionic.Zip;



public partial class UserUtility_frmOfflineUtility : System.Web.UI.Page
{
    public class OfflineUtility
    {
        public int OrgID { get; set; }
        public string CompanyName { get; set; }
        public int BrID { get; set; }
        public string BranchName { get; set; }
        public int YrCD { get; set; }
    }


    public class ClsDownloadSuccess
    {
        public int Ind { get; set; }
        public int OrgID { get; set; }
        public int BrID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int DowinloadSuccessInd { get; set; }
        public string Msg { get; set; }
    }

    //string fileLoc = @"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\RequestFile\Request.txt";
    //  string fileLoc = Server.MapPath("~/RequestFile/");
    //@"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\RequestFile\Request.txt";

    OfflineUtilityModel objOfflineUtilityModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadBranch();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void LoadBranch()
    {
        objOfflineUtilityModel = new OfflineUtilityModel();
        objOfflineUtilityModel.Ind = 1;
        objOfflineUtilityModel.OrgID = GlobalSession.OrgID;

        string uri = string.Format("OfflineUtility/LoadBranch");
        DataTable dtLoadData = CommonCls.ApiPostDataTable(uri, objOfflineUtilityModel);
        if (dtLoadData.Rows.Count > 0)
        {
            ddlBranch.DataSource = dtLoadData;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
        }
    }


    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success"

            : "alert alert-danger";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedValue != "")
            {
                if (ddlBranch.SelectedValue == "0")
                {
                    ShowMessage("Select Branch.", false);
                    ddlBranch.Focus();
                    return;
                }
            }
            objOfflineUtilityModel = new OfflineUtilityModel();
            objOfflineUtilityModel.Ind = 1;
            objOfflineUtilityModel.OrgID = GlobalSession.OrgID;
            objOfflineUtilityModel.CompanyName = GlobalSession.OrgName;
            objOfflineUtilityModel.BrID = CommonCls.ConvertIntZero(ddlBranch.SelectedValue);
            if (ddlBranch.SelectedValue == "")
            {
                objOfflineUtilityModel.BranchName = "";
            }
            else
            {
                objOfflineUtilityModel.BranchName = ddlBranch.SelectedItem.Text;
            }
            objOfflineUtilityModel.User = GlobalSession.UserID;
            string uri = string.Format("OfflineUtility/SaveOfflineRequest");
            DataTable dtSave = CommonCls.ApiPostDataTable(uri, objOfflineUtilityModel);
            if (dtSave.Rows.Count > 0)
            {
                if (dtSave.Rows[0]["ReturnInd"].ToString() == "1")
                {
                    bool TextFileStatus = CreateTextFileForRequest();
                    if (TextFileStatus == true)
                    {
                        bool ZipStatus = CreateZip();
                        if (ZipStatus == true)
                        {
                            ShowMessage("Request Save Successfully.", true);
                            ddlBranch.ClearSelection();
                            DownloadZip();
                        }
                    }
                    else
                    {
                        ShowMessage("Internal server error.", false);
                    }
                }
                else if (dtSave.Rows[0]["ReturnInd"].ToString() == "0")
                {
                    ShowMessage("This Request is already submitted", false);
                }
            }
            else
            {
                ShowMessage("Request Not Save Successfully.", false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void DownloadZip()
    {
        string filePath = Server.MapPath("~/RequestFile.zip");
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        #region old



        //Response.Clear();
        //Response.ContentType = "application/octet-stream";
        //Response.AppendHeader("content-disposition", "attachment; filename=" + "~/RequestFile.zip");
        //Response.TransmitFile(@"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\RequestFile.zip");


        //string strURL = "~/RequestFile.zip";
        //System.Net.WebClient req = new System.Net.WebClient();
        //HttpResponse response = HttpContext.Current.Response;
        //response.Clear();
        //response.ClearContent();
        //response.ClearHeaders();
        //response.Buffer = true;
        //response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(strURL) + "\"");
        //byte[] data = req.DownloadData(Server.MapPath(strURL));
        //response.BinaryWrite(data);
        // response.End();

        #endregion
    }


    private bool CreateTextFileForRequest()
    {

        try
        {
            string fileLoc = Server.MapPath("~/RequestFile/Request.txt");
            FileStream fs = null;
            if (!File.Exists(fileLoc))
            {
                using (fs = File.Create(fileLoc))
                {

                }
                WriteRequestInfo();
            }
            else
            {
                WriteRequestInfo();
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
            ShowMessage(ex.Message, false);
        }
    }

    private void WriteRequestInfo()
    {
        string fileLoc = Server.MapPath("~/RequestFile/Request.txt");

        try
        {
            using (StreamWriter sw = new StreamWriter(fileLoc))
            {
                OfflineUtility objOfflineUtility = new OfflineUtility
                {
                    
                    OrgID = objOfflineUtilityModel.OrgID,
                    CompanyName = objOfflineUtilityModel.CompanyName,
                    BrID = objOfflineUtilityModel.BrID,
                    BranchName = objOfflineUtilityModel.BranchName
                };

                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objOfflineUtility);

                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
                string RequestText = System.Convert.ToBase64String(plainTextBytes);

                sw.Write(RequestText);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    private bool CreateZip()
    {
        try
        {
            string pathname = Server.MapPath("~/RequestFile/");
            string[] filename = Directory.GetFiles(pathname);
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFiles(filename, "RequestFile");
                zip.Save(Server.MapPath("~/RequestFile.zip"));

            }
            #region old
            //string rarPackage = @"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\Request.zip";
            //Dictionary<int, string> accFiles = new Dictionary<int, string>();
            //accFiles.Add(1, @"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\RequestFile\Request.txt");
            //RarFiles(rarPackage, accFiles);

            //string sFileToZip = @"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\RequestFile\Request.txt";
            //string sZipFile = @"d:\Projects\GST Source\02-12-2017\GSTAccountDemo\GSTAccountDemo\Request.zip";

            //using (FileStream __fStream = File.Open(sZipFile, FileMode.Create))
            //{
            //    GZipStream obj = new GZipStream(__fStream, CompressionMode.Compress);

            //    byte[] bt = File.ReadAllBytes(sFileToZip);
            //    obj.Write(bt, 0, bt.Length);

            //    obj.Close();
            //    obj.Dispose();
            //}
            #endregion
            return true;
        }
        catch (Exception ex)
        {
            return false;
            ShowMessage(ex.Message, false);
        }
    }
    public static string RarFiles(string rarPackagePath, Dictionary<int, string> accFiles)
    {
        string error = "";
        try
        {
            string[] files = new string[accFiles.Count];
            int i = 0;
            foreach (var fList_item in accFiles)
            {
                files[i] = "\"" + fList_item.Value;
                i++;
            }
            string fileList = string.Join("\" ", files);
            fileList += "\"";
            System.Diagnostics.ProcessStartInfo sdp = new System.Diagnostics.ProcessStartInfo();
            string cmdArgs = string.Format("A {0} {1} -ep1 -r",
                String.Format("\"{0}\"", rarPackagePath),
                fileList);
            sdp.ErrorDialog = false;
            sdp.UseShellExecute = true;
            sdp.Arguments = cmdArgs;
            sdp.FileName = @"C:\Users\oswal\AppData\Roaming\Microsoft\Windows\Start Menu\Programs";  //Winrar.exe path
            sdp.CreateNoWindow = false;
            sdp.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(sdp);
            process.WaitForExit();
            error = "OK";
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        return error;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        #region lineSeries
        //Data obj = new Data();

        //obj.DetailData = "eyJTZXJpZXNUeXBlIjoxLCJVc2VyTmFtZSI6IiIsIlVzZXJQYXNzd29yZCI6IlBhc3N3b3JkIiwiT3JnSUQiOjE3LCJCcklEIjoxNDEsIkJpbmRpbmdfU2VyaWVzTGlzdCI6W3siU2FsZXNUeXBlU3RyaW5nIjoiQ2FzaCIsIlNhbGVzVHlwZSI6MCwiU2VyaWVzIjoiQTEiLCJTck5vIjoiMTAxIiwiU2VyaWVzVHlwZSI6MSwiT3JnSUQiOjE3LCJCcklEIjoxNDF9LHsiU2FsZXNUeXBlU3RyaW5nIjoiQ2FzaCIsIlNhbGVzVHlwZSI6MCwiU2VyaWVzIjoiQTIiLCJTck5vIjoiMTAyIiwiU2VyaWVzVHlwZSI6MSwiT3JnSUQiOjE3LCJCcklEIjoxNDF9XSwiTWFjQWRkcmVzcyI6Ijg4QUQ0M0Y4OTMzOSJ9";
        //string uri = string.Format("OfflineSeries/GetData");

        #endregion


        #region OfflineSalesData
        SalesModel objSalesModel = new SalesModel();
        objSalesModel.Ind = 1;
        objSalesModel.OrgID = GlobalSession.OrgID;
        objSalesModel.BrID = GlobalSession.BrID;
        #endregion


        //objSalesModel.YrCD = GlobalSession.YrCD;
        //objSalesModel.VchType = 6;

        //objSalesModel.Ind = 2;
        //objSalesModel.OrgID = GlobalSession.OrgID;
        //objSalesModel.BrID = GlobalSession.BrID;
        //objSalesModel.YrCD = GlobalSession.YrCD;
        //objSalesModel.AccCode = 100000;
        //objSalesModel.AdvRecPayID = 13;





        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objSalesModel);
        Data obj = new Data();

        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
        string RequestText = System.Convert.ToBase64String(plainTextBytes);
        obj.DetailData = RequestText;
        string uri = string.Format("OfflineSalesVoucher/GetSalesData");

        string dtSave = CommonCls.ApiPostString(uri, obj);
        ShowMessage(dtSave.ToString(), true);

    }

    private DataTable CreateSeriesList()
    {
        DataTable DtSeriesListData = new DataTable();

        DtSeriesListData = DtSeriesList(); //new DataTable();
        DataRow drSeriesListData = DtSeriesListData.NewRow();
        drSeriesListData["OrgID"] = GlobalSession.OrgID;
        drSeriesListData["BrID"] = GlobalSession.BrID;
        drSeriesListData["SeriesType"] = 1;
        drSeriesListData["SrNo"] = 101;
        drSeriesListData["Series"] = "IND/101";
        drSeriesListData["SalesType"] = 1;
        DtSeriesListData.Rows.Add(drSeriesListData);

        return DtSeriesListData;
    }

    DataTable DtSeriesList()
    {
        DataTable dtSales = new DataTable();

        dtSales.Columns.Add("OrgID", typeof(int));
        dtSales.Columns.Add("BrID", typeof(int));
        dtSales.Columns.Add("SeriesType", typeof(int));
        dtSales.Columns.Add("SrNo", typeof(string));
        dtSales.Columns.Add("Series", typeof(string));
        dtSales.Columns.Add("SalesType", typeof(int));

        return dtSales;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ClsDownloadSuccess objClsDownloadSuccess = new ClsDownloadSuccess
        {
            OrgID = 17,
            UserName = "abc@gmail.com",
            BrID = 141,
            UserPassword = "1234",
            DowinloadSuccessInd=1,
            Ind=2
        };

        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objClsDownloadSuccess);
        Data obj = new Data();

        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
        string RequestText = System.Convert.ToBase64String(plainTextBytes);
        obj.DetailData = RequestText;
        string uri = string.Format("OfflineSeries/UpdateRecord");

        string dtSave = CommonCls.ApiPostString(uri, obj);
        ShowMessage(dtSave.ToString(), true);

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        #region lineSeries
        Data obj = new Data();

        obj.DetailData = "eyJTZXJpZXNUeXBlIjoxLCJVc2VyTmFtZSI6IiIsIlVzZXJQYXNzd29yZCI6IlBhc3N3b3JkIiwiT3JnSUQiOjE3LCJCcklEIjoxNDEsIkJpbmRpbmdfU2VyaWVzTGlzdCI6W3siU2FsZXNUeXBlU3RyaW5nIjoiQ2FzaCIsIlNhbGVzVHlwZSI6MCwiU2VyaWVzIjoiQTEiLCJTck5vIjoiMTAxIiwiU2VyaWVzVHlwZSI6MSwiT3JnSUQiOjE3LCJCcklEIjoxNDF9LHsiU2FsZXNUeXBlU3RyaW5nIjoiQ2FzaCIsIlNhbGVzVHlwZSI6MCwiU2VyaWVzIjoiQTIiLCJTck5vIjoiMTAyIiwiU2VyaWVzVHlwZSI6MSwiT3JnSUQiOjE3LCJCcklEIjoxNDF9XSwiTWFjQWRkcmVzcyI6Ijg4QUQ0M0Y4OTMzOSJ9";
        string uri = string.Format("OfflineSeries/GetData");
        string dtSave = CommonCls.ApiPostString(uri, obj);
        ShowMessage(dtSave.ToString(), true);

        #endregion

    }
}