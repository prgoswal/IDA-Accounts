using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserUtility_frmOpeningBlcTransfer : System.Web.UI.Page
{
    OpeningBlcTransferModel objOpeningBlcTransfer;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    void ShowMessage(string Message, bool type)
    {
        lblMsg.Text = (type ? "<i class='fa fa-check-circle fa-lg'></i> " : "<i class='fa fa-info-circle fa-lg'></i> ") + Message;
        lblMsg.CssClass = type ? "alert alert-success" : "alert alert-danger";
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            string uri;
            objOpeningBlcTransfer = new OpeningBlcTransferModel();
            objOpeningBlcTransfer.Ind = 1;
            objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
            objOpeningBlcTransfer.BrID = GlobalSession.BrID;
            objOpeningBlcTransfer.YrCode = GlobalSession.YrCD;
            objOpeningBlcTransfer.VoucharDateFrom = "20" + objOpeningBlcTransfer.YrCode + "/04/01";
            objOpeningBlcTransfer.VoucharDateto = "20" + GlobalSession.BudgetYrCD + "/03/31";

            //objNewYearActivation.YrCode = GlobalSession.YrCD;
            //objNewYearActivation.VoucharDateFrom = Convert.ToDateTime(GlobalSession.YrStartDate.ToString()).ToString("yyyy/MM/dd");
            //objNewYearActivation.VoucharDateto = Convert.ToDateTime(GlobalSession.YrEndDate.ToString()).ToString("yyyy/MM/dd");

            objOpeningBlcTransfer.Reportorder = 1;

            uri = string.Format("OpeningBlcTransfer/LoadClosingBalance");
            DataTable dtClosingBlc = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);

            if (dtClosingBlc.Rows.Count > 0)
            {

                if (dtClosingBlc.Columns.Contains("accGrpcode"))
                {
                    dtClosingBlc.Columns.Remove("accGrpcode");
                }

                objOpeningBlcTransfer = new OpeningBlcTransferModel();
                objOpeningBlcTransfer.Ind = 12;
                objOpeningBlcTransfer.OrgID = GlobalSession.OrgID;
                objOpeningBlcTransfer.BrID = GlobalSession.BrID;
                objOpeningBlcTransfer.User = GlobalSession.UserID;
                objOpeningBlcTransfer.IP = GlobalSession.IP;
                objOpeningBlcTransfer.VChType = 0;
                objOpeningBlcTransfer.YrCode = GlobalSession.BudgetYrCD;
                objOpeningBlcTransfer.YrStartDate = Convert.ToDateTime("01/04/2018".ToString()).ToString("yyyy/MM/dd");
                objOpeningBlcTransfer.VoucharDate = Convert.ToDateTime("01/04/2018".ToString()).ToString("yyyy/MM/dd");
                objOpeningBlcTransfer.Narration = "OP. BALANCE";

                objOpeningBlcTransfer.dtOpeningBlc = JsonConvert.SerializeObject(dtClosingBlc);

                //objOpeningBlcTransfer.dtOpeningBlc = dtClosingBlc;

                uri = string.Format("OpeningBlcTransfer/SaveOpeningBalance");
                DataTable dtOpeningBlc = CommonCls.ApiPostDataTable(uri, objOpeningBlcTransfer);
                if (dtOpeningBlc.Rows[0][0].ToString() == "1")
                {
                    ShowMessage("Opening Balance is Transfer Successfully Please check Trial Balance.", true);

                }
                else
                {
                    ShowMessage("Opening Balance is not Transfer Try Again.", false);
                }
            }
            else
            {
                ShowMessage("Server Error.Contact To Administrator", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }
}