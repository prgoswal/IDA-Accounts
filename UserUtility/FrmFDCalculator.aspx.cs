using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FDControl_FrmFDCalculator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    protected void ddlDepositTerm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime DepositDate = Convert.ToDateTime(txtDepositDate.Text);

            //DateTime DueDate = DepositDate.AddMonths(Convert.ToInt16(txtMonths.Text)).AddDays(Convert.ToInt16(txtDays.Text));
            //lblDueDate.InnerText = DueDate.ToString("dd/MM/yyyy");

            //int diff2 = CommonCls.ConvertIntZero((DepositDate - DueDate).TotalDays);
            //int diffRate = CommonCls.ConvertIntZero(txtROI.Text) / 365;

            //decimal InterestCal = (CommonCls.ConvertDecimalZero(txtAmtDeposit.Text) * CommonCls.ConvertDecimalZero(diffRate) * CommonCls.ConvertDecimalZero(diff2)) / 100;


            //F5* (1+(F7/(100*4))) ^(F9*(4/(12)))
            //--Select 10000*(1+(6.25/(100*4)))^(15*(4/12))----------Math.Pow(100.00, 3.00);

            if (ddlDepositTerm.SelectedValue == "365")// for days
            {
                DateTime DueDate = DepositDate.AddDays(Convert.ToInt16(txtMonths.Text));
                lblDueDate.InnerText = DueDate.ToString("dd/MM/yyyy");
            }
            else if (ddlDepositTerm.SelectedValue == "12")// for Months
            {
                DateTime DueDate = DepositDate.AddMonths(Convert.ToInt16(txtMonths.Text));
                lblDueDate.InnerText = DueDate.ToString("dd/MM/yyyy");
            }
            else if (ddlDepositTerm.SelectedValue == "1")// for Year
            {
                DateTime DueDate = DepositDate.AddYears(Convert.ToInt16(txtMonths.Text));
                lblDueDate.InnerText = DueDate.ToString("dd/MM/yyyy");
            }

            //decimal day = (CommonCls.ConvertDecimalZero(4) / CommonCls.ConvertDecimalZero(ddlDepositTerm.SelectedValue));
            //double FDP = CommonCls.ConvertIntZero(CommonCls.ConvertIntZero(txtMonths.Text) * day);//CommonCls.ConvertIntZero(Math.Round((day), 4)));

            double FirstTerm = (1 + (Convert.ToDouble(txtROI.Text) / (100 * 4)));


            decimal SecondTerm = (CommonCls.ConvertDecimalZero(txtMonths.Text) * (4 / CommonCls.ConvertDecimalZero(ddlDepositTerm.SelectedValue)));
            double Maturityvalue = Math.Pow(FirstTerm, Convert.ToDouble(SecondTerm));
            double FinalMaturity = Convert.ToDouble(txtAmtDeposit.Text) * Maturityvalue;

            double FinalMaturityValue = Math.Round(FinalMaturity, 4);
            lblMaturityValue.InnerText = FinalMaturityValue.ToString();

        }
        catch (Exception ee)
        {

            throw;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtMonths.Text = "";
        txtDepositDate.Text = "";
        txtROI.Text = "";
        txtAmtDeposit.Text = "";
        lblDueDate.InnerText = lblMaturityValue.InnerText = "";
    }
}