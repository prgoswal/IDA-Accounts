using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;



public partial class UserUtility_frmRegistration : System.Web.UI.Page
{

    RegistrationModel objRegistrationModel;

    public DataTable VsDtBranches
    {
        get { return (DataTable)ViewState["DtBranches"]; }
        set { ViewState["DtBranches"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFullName.Focus();
            LoadData();
        }
        lblMsg.Text = lblMsg.CssClass = "";
    }
    void LoadData()
    {
        objRegistrationModel = new RegistrationModel();
        objRegistrationModel.Ind = 1;
        objRegistrationModel.OrgID = GlobalSession.OrgID;

        string uri = string.Format("AddGSTIN/LoadData");
        DataSet dsLoadData = CommonCls.ApiPostDataSet(uri, objRegistrationModel);
        if (dsLoadData.Tables.Count > 0)
        {
            DataTable dtLoadState = dsLoadData.Tables[0];
            VsDtBranches = dsLoadData.Tables[1];
            DataTable dtStatePanNo = dsLoadData.Tables[2];

            ddlState.DataSource = dtLoadState;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateID";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("-- State --", "0"));


            hfStateID.Value = dtStatePanNo.Rows[0]["StateID"].ToString();
            hfPanNo.Value = dtStatePanNo.Rows[0]["PANNo"].ToString();
            //chkLstBranch.DataSource = VsDtBranches;
            //chkLstBranch.DataTextField = "BranchName";
            //chkLstBranch.DataValueField = "BranchID";
            //chkLstBranch.DataBind();

            //dtStatePanNo.Rows[0][""];
            //dtStatePanNo.Rows[0][""];

            if (GlobalSession.UnRegisterClient == 1)
            {
                ShowMessageOnPopUp("You are not Authorized For Add GSTIN. Please Contact To Admin.", false, "../Defaults/Default.aspx");
                return;
            }
        }
        else
        {
            ShowMessage("No Data Found.", false);
        }
    }

    void ShowMessageOnPopUp(string Message, bool type, string href)
    {
        object sender = UpdatePanel1;
        Message = Message.Replace("'", "");
        Message = Server.HtmlEncode(Message).Replace(Environment.NewLine, "<br />");
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "ShowMessage", "$(function() { openModal('" + Message + "','" + type + "','" + href + "'); });", true);
    }

    #region

    public DataTable VsDtSeries
    {
        get { return (DataTable)ViewState["VsDtSeries"]; }
        set { ViewState["VsDtSeries"] = value; }
    }

    protected void btnAddSeries_Click(object sender, EventArgs e)
    {
        if (!ValidateAddSeries())
        {
            return;
        }

        if (VsDtSeries == null)
        {
            VsDtSeries = new DataTable();
            VsDtSeries.Columns.Add("CompanyID", typeof(int));
            VsDtSeries.Columns.Add("BranchID", typeof(int));
            VsDtSeries.Columns.Add("SeriesTypeInd", typeof(int));
            VsDtSeries.Columns.Add("CashCreditInd", typeof(int));
            VsDtSeries.Columns.Add("Series", typeof(string));
            VsDtSeries.Columns.Add("SerialNoManualInd", typeof(int));
            VsDtSeries.Columns.Add("SerialNo", typeof(int));
        }

        DataRow drSeries = VsDtSeries.NewRow();
        drSeries["SeriesTypeInd"] = ddlSeriesType.SelectedValue;
        drSeries["CashCreditInd"] = CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == -1 ? 3 : CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue);
        drSeries["Series"] = CommonCls.ConvertIntZero(ddlSrNoAuto.SelectedValue) == 2 ? txtSeries.Text.ToUpper() : "";
        drSeries["SerialNoManualInd"] = 2;
        drSeries["SerialNo"] = CommonCls.ConvertIntZero(txtSerialNo.Text);

        VsDtSeries.Rows.Add(drSeries);
        gvCreateSeries.DataSource = VsDtSeries;
        gvCreateSeries.DataBind();

        switch (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue))
        {
            case 1:

                ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = false;
                if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == 0)
                    ddlCashCredit.SelectedValue = "1";
                else
                    ddlCashCredit.SelectedValue = "0";

                txtSerialNo.Text = "";
                txtSerialNo.Focus();
                break;

            case 2:
                txtSeries.Text = txtSerialNo.Text = "";
                ddlCashCredit.Focus();
                break;

            case 3:
                btnAddSeries.Enabled = ddlSrNoAuto.Enabled = false;

                txtSeries.Text = txtSerialNo.Text = "";
                ddlSrNoAuto.ClearSelection();
                break;
        }

        ddlSeriesType.Enabled = false;
    }

    void SeriesInit()
    {
        object sender = UpdatePanel1;
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "SeriesInit", "SeriesInit('#" + ddlSeriesType.ClientID + "');", true);
    }

    bool ValidateAddSeries()
    {
        if (CommonCls.ConvertIntZero(txtSerialNo.Text) == 0)
        {
            ShowMessage("Enter Serial No", false);
            txtSerialNo.Focus();
            return false;
        }

        switch (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue))
        {
            case 0:
                ShowMessage("Select Series Type.", false);
                ddlSeriesType.Focus();
                return false;


            case 1: /// Manual Series

                if (VsDtSeries != null && VsDtSeries.Rows.Count == 2)
                {
                    ShowMessage("Not Allow After Cash & Credit Add.", false);
                    ddlCashCredit.Focus();
                    return false;
                }
                if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == -1)
                {
                    ShowMessage("Select Account Type", false);
                    ddlCashCredit.Focus();
                    return false;
                }
                break;

            case 2: /// Available Series

                if (VsDtSeries != null && VsDtSeries.Rows.Count > 0)
                {
                    DataRow[] dr = VsDtSeries.Select("Series='" + txtSeries.Text.ToUpper() + "'");
                    if (dr.Count() > 0)
                    {
                        ShowMessage("Series Can Not Be Same.", false);
                        txtSeries.Focus();
                        return false;
                    }
                }

                if (CommonCls.ConvertIntZero(ddlCashCredit.SelectedValue) == -1)
                {
                    ShowMessage("Select Account Type", false);
                    ddlCashCredit.Focus();
                    return false;
                }
                break;

            case 3: /// Default Series
                break;
        }
        return true;
    }

    protected void gvCreateSeries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "RemoveRow")
        {
            VsDtSeries.Rows[rowIndex].Delete();
            gvCreateSeries.DataSource = VsDtSeries;
            gvCreateSeries.DataBind();
            if (CommonCls.ConvertIntZero(ddlSeriesType.SelectedValue) == 1)
            {
                if (VsDtSeries.Rows.Count > 0)
                {
                    if (CommonCls.ConvertIntZero(VsDtSeries.Rows[0]["CashCreditInd"]) == 0)
                        ddlCashCredit.SelectedValue = "1";
                    else
                        ddlCashCredit.SelectedValue = "0";
                }
                else
                {
                    ddlCashCredit.Enabled = ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = ddlSeriesType.Enabled = true; //txtSerialNo.Enabled =
                }
            }
        }
    }

    protected void gvCreateSeries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lblCashCreditInd = (Label)e.Row.FindControl("lblCashCreditInd");
                if (string.IsNullOrEmpty(lblCashCreditInd.Text) || CommonCls.ConvertIntZero(lblCashCreditInd.Text) == 3)
                    lblCashCreditInd.Text = "";
                else
                {
                    ddlCashCredit.SelectedValue = lblCashCreditInd.Text;
                    lblCashCreditInd.Text = ddlCashCredit.SelectedItem.Text;
                }

                Label lblSrNoAutoInd = (Label)e.Row.FindControl("lblSrNoAutoInd");
                if (CommonCls.ConvertIntZero(lblSrNoAutoInd.Text) == 0)
                    lblSrNoAutoInd.Text = "";
                else
                {
                    ddlSrNoAuto.SelectedValue = lblSrNoAutoInd.Text;
                    lblSrNoAutoInd.Text = ddlSrNoAuto.SelectedItem.Text;
                }
            }
        }
    }

    protected void btnClearSeries_Click(object sender, EventArgs e)
    {
        ClearSeries();
    }

    void ClearSeries()
    {
        SeriesInit();
        btnAddSeries.Enabled = ddlSrNoAuto.Enabled = ddlCashCredit.Enabled = true;

        ddlSeriesType.Enabled = true;
        gvCreateSeries.DataSource = VsDtSeries = null;
        gvCreateSeries.DataBind();
        ddlCashCredit.ClearSelection();
        ddlSeriesType.ClearSelection();
        ddlSrNoAuto.ClearSelection();
        lblMsg.Text = "";
        lblMsg.Visible = false;
        txtSerialNo.Text = txtSeries.Text = string.Empty;
    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    #endregion

    protected void btnGenerateOTP_Click(object sender, EventArgs e)
    {
        // declare array string to generate random string with combination of small,capital letters and numbers
        char[] charArr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string strrandom = string.Empty;
        Random objran = new Random();
        //int noofcharacters = Convert.ToInt32(txtCharacters.Text);
        for (int i = 0; i < 4; i++)
        {
            //It will not allow Repetation of Characters
            int pos = objran.Next(1, charArr.Length);
            if (!strrandom.Contains(charArr.GetValue(pos).ToString()))
                strrandom += charArr.GetValue(pos);
            else
                i--;
        }
        lblMessage.Text = "GENERATED OTP :-" + strrandom;
    }

    #region MAC Address

    //string MACADRESS;
    //[DllImport("Iphlpapi.dll")]
    //private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    //[DllImport("Ws2_32.dll")]
    //private static extern Int32 inet_addr(string ip);

    //public string GetMACAddress()
    //{
    //    string userip = Request.UserHostAddress;
    //    string strClientIP = Request.UserHostAddress.ToString().Trim();
    //    Int32 ldest = inet_addr(strClientIP);
    //    Int32 lhost = inet_addr("");
    //    Int64 macinfo = new Int64();
    //    Int32 len = 6;
    //    int res = SendARP(ldest, 0, ref macinfo, ref len);
    //    string mac_src = macinfo.ToString("X");

    //    while (mac_src.Length < 12)
    //    {
    //        mac_src = mac_src.Insert(0, "0");
    //    }

    //    string mac_dest = "";

    //    for (int i = 0; i < 11; i++)
    //    {
    //        if (0 == (i % 2))
    //        {
    //            if (i == 10)
    //            {
    //                mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
    //            }
    //            else
    //            {
    //                mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
    //            }
    //        }
    //    }
    //    MACADRESS = mac_dest;
    //    lblMessage.Text = "MACADRESS OF MAchine :-" + MACADRESS;
    //    return MACADRESS;
    //}


    //public string GetMACAddress()
    //{
    //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    //    String sMacAddress = string.Empty;
    //    foreach (NetworkInterface adapter in nics)
    //    {
    //        if (sMacAddress == String.Empty)// only return MAC Address from first card  
    //        {
    //            IPInterfaceProperties properties = adapter.GetIPProperties();
    //            sMacAddress = adapter.GetPhysicalAddress().ToString();
    //        }


    //    }
    //    lblMessage.Text = "MACADRESS OF MAchine :-" + sMacAddress;
    //    return sMacAddress;
    //}



    //public static PhysicalAddress GetMACAddress()
    //{
    //    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
    //    {
    //        // Only consider Ethernet network interfaces
    //        if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
    //            nic.OperationalStatus == OperationalStatus.Up)
    //        {
    //            //  lblMessage.Text = "MACADRESS OF MAchine :-" + nic.GetPhysicalAddress;
    //            return nic.GetPhysicalAddress();
    //        }
    //    }

    //    return null;
    //}
    public string GetMACAddress1()
    {
        ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objMOC = objMOS.Get();
        string macAddress = String.Empty;
        foreach (ManagementObject objMO in objMOC)
        {
            object tempMacAddrObj = objMO["MacAddress"];

            if (tempMacAddrObj == null) //Skip objects without a MACAddress
            {
                continue;
            }
            if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
            {
                macAddress = tempMacAddrObj.ToString();
            }
            objMO.Dispose();
        }
        macAddress = macAddress.Replace(":", "");

        lblMessage.Text = "MACADRESS OF MAchine :-" + macAddress;
        return macAddress;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        GetMACAddress1();
        //  lblMessage.Text = (GetMACAddress().ToString());
    }
    #endregion
    #region OLD OTP
    //string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };  
    //string sRandomOTP = GenerateRandomOTP(8,saAllowedCharacters);



    //private static string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
    //{

    //    string sOTP = String.Empty;

    //    string sTempChars = String.Empty;

    //    Random rand = new Random();

    //    for (int i = 0; i < iOTPLength; i++)
    //    {

    //        int p = rand.Next(0, saAllowedCharacters.Length);

    //        sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

    //        sOTP += sTempChars;

    //    }

    //    return sOTP;

    //}
    #endregion
    #region Check Net Connection

    public static bool CheckForInternetConnection()
    {
        try
        {
            using (var client = new System.Net.WebClient())
            {
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
        }
        catch
        {
            return false;
        }
    }
    protected void btncheck_Click(object sender, EventArgs e)
    {
        bool check = CheckForInternetConnection();
        if (check == true)
        {
            lblMsg.Text = "connection open";
        }
        else
        {
            lblMsg.Text = "connection off";
        }
    }

    #endregion
}