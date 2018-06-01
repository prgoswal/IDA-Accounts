using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static string ValidGSTINNO = "";

    public static String GSTINFORMAT_REGEX = "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}";
    public static String GSTN_CODEPOINT_CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public void main()
    {
        //Sample valid GSTIN - 09 AAAUP  8175 A 1 Z G;
        //Scanner sc = new Scanner(System.in);		


        Console.Write("Enter GSTIN Number");
        //String gstin = sc.next();
        String gstin = txtGSTIN.Text;
        try
        {
            if (validGSTIN(gstin))
                Response.Write("Valid GSTIN!");

            else
                Response.Write("Invalid GSTIN & Valid GSTIN Is " + ValidGSTINNO);
        }
        catch (Exception e)
        {

        }
    }

    private static Boolean validGSTIN(String gstin)
    {
        Boolean isValidFormat = false;
        if (checkPattern(gstin, GSTINFORMAT_REGEX))
        {
            isValidFormat = verifyCheckDigit(gstin);
        }
        return isValidFormat;

    }

    Regex r = new Regex(GSTINFORMAT_REGEX, RegexOptions.IgnoreCase);



    public static Boolean checkPattern(String inputval, String regxpatrn)
    {

        bool result = false;
        Regex r = new Regex(GSTINFORMAT_REGEX, RegexOptions.IgnoreCase);

        // Match the regular expression pattern against a text string.
        Match m = r.Match(inputval);
        return m.Success;

    }

    private static Boolean verifyCheckDigit(String gstinWCheckDigit)
    {
        Boolean isCDValid = false;
        String newGstninWCheckDigit = getGSTINWithCheckDigit(gstinWCheckDigit.Substring(0, gstinWCheckDigit.Length - 1));
        ValidGSTINNO = newGstninWCheckDigit;

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






    protected void btnCheckValidGSTIN_Click(object sender, EventArgs e)
    {
        //main();


        if (verifyCheckDigit(txtGSTIN.Text))
            Response.Write("Valid GSTIN!");

        else
            Response.Write("Invalid GSTIN & Valid GSTIN Is " + ValidGSTINNO);
    }


    protected void ImportExcel(object sender, EventArgs e)
    {
        //Save the uploaded Excel file.
        string filePath = Server.MapPath("~/TempFiles/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(filePath);

        //Open the Excel file using ClosedXML.
        using (XLWorkbook workBook = new XLWorkbook(filePath))
        {
            //Read the first Sheet from Excel file.
            IXLWorksheet workSheet = workBook.Worksheet(1);

            //Create a new DataTable.
            DataTable dt = new DataTable();

            //Loop through the Worksheet rows.
            bool firstRow = false;

            dt.Columns.Add("AccountCode", typeof(string));
            dt.Columns.Add("GSTIN", typeof(string));
            dt.Columns.Add("Valid", typeof(string));

            //dt.Columns.Add("InvoiceType", typeof(string));
            //dt.Columns.Add("AccountCode", typeof(string));
            //dt.Columns.Add("GSTIN", typeof(string));
            //dt.Columns.Add("Valid", typeof(string));


            foreach (IXLRow row in workSheet.Rows())
            {

                DataRow drNew = dt.NewRow();

                // For Purchase With Column
                //drNew["InvoiceType"] = row.Cell(1).Value;
                //drNew["AccountCode"] = row.Cell(2).Value;
                //drNew["GSTIN"] = row.Cell(3).Value;
                //drNew["Valid"] = validGSTIN(row.Cell(3).Value.ToString());

                // Without Col
                drNew["AccountCode"] = row.Cell(1).Value;
                drNew["GSTIN"] = row.Cell(2).Value;
                drNew["Valid"] = validGSTIN(row.Cell(2).Value.ToString()) ? "1" : "0";

                dt.Rows.Add(drNew);
                //Use the first row to add columns to DataTable.
                //if (firstRow)
                //{
                //    foreach (IXLCell cell in row.Cells())
                //    {
                //        dt.Columns.Add(cell.Value.ToString());
                //    }
                //    firstRow = false;
                //}
                //else
                //{
                //    //Add rows to DataTable.
                //    dt.Rows.Add();
                //    int i = 0;
                //    foreach (IXLCell cell in row.Cells())
                //    {
                //        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                //        i++;
                //    }
                //}

            }


            XLWorkbook xl = new XLWorkbook();
            xl.Worksheets.Add(dt, "Sheet1");
            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileUpload1.FileName + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                xl.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
}