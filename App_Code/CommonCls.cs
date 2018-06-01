using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;

/// <summary>
/// All Common Funtion
/// </summary>
public abstract class CommonCls
{

    public static String EncodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        for (int i = 1; i <= Pwd.Length; i++)
        {
            if (i < 2)
            {
                Calc = 100;
            }
            else if (i < 4)
            {
                Calc = 200;
            }
            else if (i < 6)
            {
                Calc = 300;
            }
            else if (i < 8)
            {
                Calc = 400;
            }
            else if (i < 10)
            {
                Calc = 500;
            }
            byte[] bt = Encoding.ASCII.GetBytes(Pwd.ToString().Substring(i - 1, 1));
            FnlStr = FnlStr + Convert.ToInt32(bt[0] + Calc + i).ToString();
        }
        return FnlStr;
    }

    public static String DecodePassword(String Pwd)
    {
        int Calc = 0; String FnlStr = "";
        int i = 1; int j = 1; int A = 0; byte[] bt; String Str = ""; ;
        while (i <= Pwd.Length)
        {
            if (j < 2)
            {
                Calc = 100;
            }
            else if (j < 4)
            {
                Calc = 200;
            }
            else if (j < 6)
            {
                Calc = 300;
            }
            else if (j < 8)
            {
                Calc = 400;
            }
            else if (j < 10)
            {
                Calc = 500;
            }
            A = Convert.ToInt32(Pwd.ToString().Substring(i - 1, 3)) - Calc - j;
            bt = new byte[1];
            bt[0] = (byte)A;
            Str = Encoding.ASCII.GetString(bt);
            FnlStr = FnlStr + Str;
            i = i + 3;
            j++;
        }
        return FnlStr;
    }

    public static string GetIP()
    {

        //string IP = HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;

        string ipV4, ipV6 = ""; string strHostName = "";
        strHostName = Dns.GetHostName();           // Get Computer Name
        string hostadd = Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        ipV4 = addr[2].ToString();
        //ipV4 = addr[1].ToString();
        ipV6 = addr[0].ToString();
        return ipV4;
    }

    #region Calling API

    public static DataSet ApiGetDataSet(string URL)
    {
        DataSet ds;
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"].ToString());
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = URL;
            var response = HClient.GetAsync(uri).Result;
            var resData = response.Content.ReadAsStringAsync().Result.ToString();
            ds = new DataSet();
            ds = JsonConvert.DeserializeObject<DataSet>(resData);
            return ds;
        }
        catch (Exception)
        {
            ds = new DataSet();
            return ds;
        }
    }

    public static DataSet ApiPostDataSet(string URL, object obj)
    {
        DataSet ds;
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"].ToString());
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var uri = URL;
            var response = HClient.PostAsJsonAsync(uri, obj).Result;

            var resData = response.Content.ReadAsStringAsync().Result.ToString();
            ds = new DataSet();
            ds = JsonConvert.DeserializeObject<DataSet>(resData);
            return ds;
        }
        catch (Exception)
        {
            ds = new DataSet();
            return ds;
        }
    }

    public static DataTable ApiGetDataTable(string URL)
    {
        DataTable dt;
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"].ToString());
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var uri = URL;
            var response = HClient.GetAsync(uri).Result;
            var resData = response.Content.ReadAsStringAsync().Result.ToString();
            dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(resData);
            return dt;
        }
        catch
        {
            dt = new DataTable();
            return dt;
        }
    }

    public static DataTable ApiPostDataTable(string URL, object obj)
    {
        DataTable dt;
        try
        {
            HttpClient HClient = new HttpClient();
            HClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"].ToString());
            HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var uri = URL;
            var response = HClient.PostAsJsonAsync(uri, obj).Result;

            var resData = response.Content.ReadAsStringAsync().Result.ToString();
            dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(resData);
            return dt;
        }
        catch
        {
            dt = new DataTable();
            return dt;
        }
    }

    #endregion

    #region All Conversions

    public static string ConvertToDate(string Date)
    {
        try
        {
            return Date.Substring(6, 4) + "/" + Date.Substring(3, 2) + "/" + Date.Substring(0, 2);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string ConvertToDate(object Date)
    {
        try
        {
            return Date.ToString().Substring(6, 4) + "/" + Date.ToString().Substring(3, 2) + "/" + Date.ToString().Substring(0, 2);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string ConvertDateDB(string Date)
    {
        try
        {
            Date = Convert.ToDateTime(Date).ToString("dd/MM/yyyy");
            Date = Date.Replace("-", "/");
            if (Date.Substring(0, 10) == "01-01-1900" || Date.Substring(0, 10) == "01/01/1900")
            {
                return "";
            }
            return Date;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string ConvertDateDB(object Date)
    {
        try
        {
            Date = Convert.ToDateTime(Date).ToString("dd/MM/yyyy");
            Date = Date.ToString().Replace("-", "/");
            if (Date.ToString().Substring(0, 10) == "01-01-1900" || Date.ToString().Substring(0, 10) == "01/01/1900")
            {
                return "";
            }
            return Date.ToString();
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static decimal ConvertDecimalZero(string val)
    {
        decimal convertTo;
        try
        {
            if (string.IsNullOrEmpty(val))
            {
                return 0;
            }
            convertTo = Convert.ToDecimal(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static decimal ConvertDecimalZero(object val)
    {
        decimal convertTo;
        try
        {
            if (val == string.Empty)
            {
                return 0;
            }
            convertTo = Convert.ToDecimal(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static int ConvertIntZero(string val)
    {
        int convertTo;
        try
        {
            if (string.IsNullOrEmpty(val))
            {
                return 0;
            }
            convertTo = Convert.ToInt32(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static int ConvertIntZero(object val)
    {
        int convertTo;
        try
        {
            if (val == string.Empty)
            {
                return 0;
            }
            convertTo = Convert.ToInt32(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static long ConvertLongZero(string val)
    {
        long convertTo;
        try
        {
            if (string.IsNullOrEmpty(val))
            {
                return 0;
            }
            convertTo = Convert.ToInt64(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static long ConvertLongZero(object val)
    {
        long convertTo;
        try
        {
            if (val == string.Empty)
            {
                return 0;
            }
            convertTo = Convert.ToInt64(val);
            return convertTo;
        }
        catch
        {
            return 0;
        }
    }

    public static string ConverToCurrency(string Amount)
    {
        decimal parsed = decimal.Parse(ConvertDecimalZero(Amount).ToString(), CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("hi-IN");
        string text = string.Format(hindi, "{0:c}", parsed);
        return text;
    }

    public static string ConverToCurrency(object Amount)
    {
        decimal parsed = decimal.Parse(ConvertDecimalZero(Amount).ToString(), CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("hi-IN");
        string text = string.Format(hindi, "{0:c}", parsed);
        return text;
    }

    public static decimal ConverFromCurrency(string Amount)
    {
        try
        {

            //decimal abc = 10.95m;
            //string cur = abc.ToString("C2");

            //string a = "रु 36,000.00";
            //// Convert back to decimal
            //decimal value = decimal.Zero;
            //if (decimal.TryParse(a, System.Globalization.NumberStyles.Currency,
            //    System.Globalization.CultureInfo.CurrentCulture.NumberFormat, out value))
            //{
            //    ShowMessage(value.ToString(), true);
            //}
            return Decimal.Parse(Amount, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, new CultureInfo("hi-IN"));
        }
        catch (Exception ex)
        {
            if (ex is FormatException) throw ex;
            else return 0;
        }
    }

    public static decimal ConverFromCurrency(object Amount)
    {
        try
        {
            return Decimal.Parse(Amount.ToString(), NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, new CultureInfo("hi-IN"));
        }
        catch (Exception ex)
        {
            if (ex is FormatException) throw ex;
            else return 0;
        }
    }

    public static string ConverToCommas(object Amount)
    {
        decimal parsed = decimal.Parse(ConvertDecimalZero(Amount).ToString(), NumberStyles.Currency | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("hi-IN");
        string text = parsed.ToString("N");
        //string Check = string.Format(hindi, "{0:#,##0.00}", parsed);

        //decimal checkParsed = decimal.Parse(ConvertDecimalZero(Amount).ToString(), CultureInfo.CurrentCulture.NumberFormat.NumberGroupSizes)
        return text;
    }

    public int this[object val]
    {
        get
        {
            int convertTo;
            try
            {
                if (val == string.Empty)
                {
                    return 0;
                }
                convertTo = Convert.ToInt32(val);
                return convertTo;
            }
            catch
            {
                return 0;
            }
        }
    }

    #endregion

    #region Validation

    public static int CountEmptyRows(DataTable source)
    {
        int countrow = 0;
        DataTable dt1 = source.Clone(); //copy the structure 
        for (int i = 0; i <= source.Rows.Count - 1; i++) //iterate through the rows of the source
        {
            DataRow currentRow = source.Rows[i];  //copy the current row 
            foreach (var colValue in currentRow.ItemArray)//move along the columns 
            {
                if (!string.IsNullOrEmpty(colValue.ToString())) // if there is a value in a column, copy the row and finish
                {
                    countrow++;
                    dt1.ImportRow(currentRow);
                    break; //breaknd get a new row                        
                }
            }
        }
        return countrow;
    }

    public static DataTable RemoveEmptyRows(DataTable source)
    {
        DataTable dt1 = source.Clone(); //copy the structure 
        for (int i = 0; i <= source.Rows.Count - 1; i++) //iterate through the rows of the source
        {
            DataRow currentRow = source.Rows[i];  //copy the current row 
            foreach (var colValue in currentRow.ItemArray)//move along the columns 
            {
                if (!string.IsNullOrEmpty(colValue.ToString())) // if there is a value in a column, copy the row and finish
                {
                    dt1.ImportRow(currentRow);
                    break; //breaknd get a new row                        
                }
            }
        }
        return dt1;
    }

    public static bool CheckFinancialYrDate(string selectedDate, string startDate, string endDate)
    {
        try
        {
            DateTime EndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
            DateTime StartDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            DateTime SelectDate = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", null);
            DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            if (SelectDate > EndDate || SelectDate < StartDate || SelectDate > CurrentDate)
            {
                return false;
            }
            else return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool GSTINIsValid(string GSTIN)
    {
        if (Regex.IsMatch(GSTIN.ToUpper(), @"[0-9]{2}[(A-Z)]{5}\d{4}[(A-Z)]{1}[0-9A-Z]{3}"))
        {
            return true;
        }
        return false;
    }

    public static bool PanIsValid(string GSTIN)
    {
        if (Regex.IsMatch(GSTIN.ToUpper(), @"[(A-Z)]{5}\d{4}[(A-Z)]{1}"))
        {
            return true;
        }
        return false;
    }

    public static bool CheckGUIDIsValid()
    {
        return true;

        UserModel objCUModel = new UserModel();
        objCUModel.Ind = 9;

        objCUModel.OrgID = GlobalSession.OrgID;
        objCUModel.BrID = GlobalSession.BrID;
        objCUModel.UserID = GlobalSession.UserID;

        string uri = string.Format("User/CheckGuidDetails");
        DataTable dtMatchGuidDetails = ApiPostDataTable(uri, objCUModel);

        if (dtMatchGuidDetails.Rows.Count > 0)
        {
            if (dtMatchGuidDetails.Rows[0]["GUID"].ToString() == GlobalSession.GUID)
            {
                return true;
            }
            else
            {
                ShowModal("This User has Logedin in Another Machine.", false, "../Logout.aspx", "OK");
                //cs.RegisterStartupScript(page.GetType(), "WitoutUpdatePanel", "$(function() { ReLogin('" + IsLogedIn + "'); });", true);
                return false;
            }
        }
        else
        {
            ShowModal("This User has Logedin in Another Machine.", false, "../Logout.aspx", "OK");
            return false;
        }
    }

    #endregion

    #region Show Modal On PopUp

    public static void ShowModal(string Message, bool ShowClose, string href)
    {
        Page page = HttpContext.Current.Handler as Page;
        Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterStartupScript(page, page.GetType(), "WithUpdatePanel", "$(function() { openModal('" + Message + "','" + ShowClose + "','" + href + "'); });", true);
        //cs.RegisterStartupScript(page.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + false + "','" + href + "'); });", true);
    }

    public static void ShowModal(string Message, bool ShowClose, string href, string btnName)
    {
        Page page = HttpContext.Current.Handler as Page;
        Message = Message.Replace("'", "");
        //Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterStartupScript(page, page.GetType(), "WithUpdatePanel", "$(function() { openModal('" + Message + "','" + ShowClose + "','" + href + "','" + btnName + "'); });", true);
        //cs.RegisterStartupScript(page.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + false + "','" + href + "'); });", true);
    }

    #endregion

    #region Calling Script Functions

    /// <summary>
    /// Call JavaScript function Which is Pre Build
    /// </summary>
    /// <param name="functionName">Pass Function Name Like - MyFunction</param>
    public static void ScriptFunction(string functionName)
    {
        Page page = HttpContext.Current.Handler as Page;
        ScriptManager.RegisterStartupScript(page, page.GetType(), functionName, "$(function() { " + functionName + "(); });", true);
    }

    /// <summary>
    /// Call JavaScript function Which is Pre Build
    /// </summary>
    /// <param name="functionName">Pass Function Name Like - MyFunction</param>
    /// <param name="parameters">Pass Parameters Like - para1,param2,.... </param>
    public static void ScriptFunction(string functionName, string parameters)
    {
        Page page = HttpContext.Current.Handler as Page;
        ScriptManager.RegisterStartupScript(page, page.GetType(), functionName, "$(function() { " + functionName + "('" + parameters + "'); });", true);
    }

    /// <summary>
    /// Call Javascript 
    /// </summary>
    /// <param name="scriptLine">Ex. - $('#id').focus(); </param>
    public static void ScriptInInline(string scriptLine)
    {
        Page page = HttpContext.Current.Handler as Page;
        ScriptManager.RegisterStartupScript(page, page.GetType(), scriptLine, "$(function() { " + scriptLine + " });", true);
    }

    #endregion

    #region GstinNumber

    public static String GSTINFORMAT_REGEX = "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}";
    public static String GSTN_CODEPOINT_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static bool gstinvalid(string GSTIN, string stateid, string PANNo)
    {
        string statevalue;
        if (GSTIN.Count() == 15)
        {
            if (stateid.Length == 1)
            {
                statevalue = "0" + Convert.ToString(stateid);
            }
            else
            {
                statevalue = Convert.ToString(stateid);
            }
            if (!string.IsNullOrEmpty(GSTIN))
            {
                string firsttwo = GSTIN.Substring(0, 2);
                string nextten = GSTIN.Substring(2, 10).ToUpper();

                if (statevalue != firsttwo)
                {

                    return false;
                }
                if (!string.IsNullOrEmpty(PANNo))
                {
                    if (PANNo != nextten)
                    {
                        return false;
                    }
                }
                else
                {

                }
            }
        }
        else
        {

            return false;
        }
        return true;
    }

    public static Boolean validGSTIN(String gstin)
    {
        Boolean isValidFormat = false;
        if (checkPattern(gstin, GSTINFORMAT_REGEX))
        {

            isValidFormat = verifyCheckDigit(gstin);
        }
        return isValidFormat;

    }

    private static Boolean verifyCheckDigit(String gstinWCheckDigit)
    {
        Boolean isCDValid = false;
        String newGstninWCheckDigit = getGSTINWithCheckDigit(gstinWCheckDigit.Substring(0, gstinWCheckDigit.Length - 1));

        if (gstinWCheckDigit.Trim().Equals(newGstninWCheckDigit))
        {
            isCDValid = true;
        }
        return isCDValid;
    }

    public static String getGSTINWithCheckDigit(String gstinWOCheckDigit)
    {
        int factor = 2;
        int sum = 0;
        int checkCodePoint = 0;
        char[] cpChars;
        char[] inputChars;

        try
        {
            if (gstinWOCheckDigit == null)
            {
                throw new Exception("GSTIN supplied for checkdigit calculation is null");
            }
            cpChars = GSTN_CODEPOINT_CHARS.ToCharArray();
            inputChars = gstinWOCheckDigit.Trim().ToUpper().ToCharArray();

            int mod = cpChars.Length;
            for (int i = inputChars.Length - 1; i >= 0; i--)
            {
                int codePoint = -1;
                for (int j = 0; j < cpChars.Length; j++)
                {
                    if (cpChars[j] == inputChars[i])
                    {
                        codePoint = j;
                    }
                }
                int digit = factor * codePoint;
                factor = (factor == 2) ? 1 : 2;
                digit = (digit / mod) + (digit % mod);
                sum += digit;
            }
            checkCodePoint = (mod - (sum % mod)) % mod;
            String str = gstinWOCheckDigit + cpChars[checkCodePoint];
            return gstinWOCheckDigit + cpChars[checkCodePoint];
        }
        finally
        {
            inputChars = null;
            cpChars = null;
        }
    }

    Regex r = new Regex(GSTINFORMAT_REGEX, RegexOptions.IgnoreCase);
    public static Boolean checkPattern(String inputval, String regxpatrn)
    {
        Regex r = new Regex(GSTINFORMAT_REGEX, RegexOptions.IgnoreCase);
        // Match the regular expression pattern against a text string.
        Match m = r.Match(inputval);
        return m.Success;

    }

    #endregion

    public static DataTable GetAllottedMenuDetails()
    {
        UserModel objCUModel = new UserModel();
        objCUModel.Ind = 15;
        objCUModel.OrgID = GlobalSession.OrgID;
        objCUModel.UserID = GlobalSession.UserID;

        string uri = string.Format("User/GetAllottedMenuDetails");
        DataTable dtMatchGuidDetails = ApiPostDataTable(uri, objCUModel);

        return dtMatchGuidDetails;
    }

    public static string ApiPostString(string URL, Data obj)
    {
        //DataTable dt;
        string result;

        HttpClient HClient = new HttpClient();
        HClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"].ToString());
        HClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var uri = URL;
        var response = HClient.PostAsJsonAsync(uri, obj).Result;

        var resData = response.Content.ReadAsStringAsync().Result.ToString();
        //dt = new DataTable();
        result = JsonConvert.DeserializeObject<string>(resData);
        return result;

    }

    public static string strcon = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

    public static DataTable dtSearchServiceNo(int ServiceNo, int ServiceId)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda;
        if (con.State == ConnectionState.Closed)
            con.Open();

        if (ServiceId == 1) //For Salary
            cmd = new SqlCommand("Select * From MstPlot where ServiceNo='" + ServiceNo + "'", con);
        else if(ServiceId == 2) //For Lease
            cmd = new SqlCommand("Select * From MstPlot where ServiceNo='" + ServiceNo + "'", con);
        else if(ServiceId == 3) //For Rent
            cmd = new SqlCommand("Select * From MstPlot where ServiceNo='" + ServiceNo + "'", con);

        sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);

        return dt;
    }
    
    public static DataTable ServiceNoAccordingToAccountCode(DataTable dtSalary, DataTable dtLease, DataTable dtRent, int AccCode)
    {
        DataTable dt = new DataTable();
        if (dt == null || dt.Rows.Count <= 0)
        {
            dt = BankPaymentSchema();
        }

        //Salary Code
        DataView dvSalary = new DataView(dtSalary);
        dvSalary.RowFilter = "AccCode=" + AccCode + "";

        //Lease Code
        DataView dvLease = new DataView(dtLease);
        dvLease.RowFilter = "AccCode=" + AccCode + "";

        //Rent Code
        DataView dvRent = new DataView(dtRent);
        dvRent.RowFilter = "AccCode=" + AccCode + "";

        if (dvSalary.ToTable().Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr["ReturnInd"] = 1;
            dr["AccCode"] = dvSalary.ToTable().Rows[0]["AccCode"].ToString();
            dt.Rows.Add(dr);
        }
        else if (dvLease.ToTable().Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr["ReturnInd"] = 2;
            dr["AccCode"] = dvLease.ToTable().Rows[0]["AccCode"].ToString();
            dt.Rows.Add(dr);
        }
        else if (dvRent.ToTable().Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr["ReturnInd"] = 3;
            dr["AccCode"] = dvRent.ToTable().Rows[0]["AccCode"].ToString();
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public static DataTable BankPaymentSchema()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("ReturnInd", typeof(int));
        dt.Columns.Add("AccCode", typeof(int));
        
        return dt;
    }
}