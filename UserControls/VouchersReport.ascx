<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VouchersReport.ascx.cs" Inherits="UserControls_VouchersReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<style>

    .Popup {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 80%;
        height: 700px;
    }

    .lbl {
        font-size: 16px;
        font-style: italic;
        font-weight: bold;
    }

    #ContentPlaceHolder1_lblReport {
        display: inline-block;
        margin-left: 125px;
    }

    #ctl00_ContentPlaceHolder1_ReportViewer1 {
        height: 450px !important;
    }

    #ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
        height: 90% !important;
    }

    [class*="_r10"] {
        margin: 0 auto !important;
    }

    .bodyPop {
        display: grid;
        background: white;
        width: 80%;
        margin: auto;
        border: 1px solid grey;
        border-radius: 6px;
        box-shadow: 3px 4px 13px rgba(0, 0, 0, 0.4);
        padding: 15px;
        z-index: 1040;
    }

    .reportPopUp {
        /*opacity: 0.5;
                    position: fixed;
                    top: 0;
                    right: 0;
                    bottom: 0;
                    left: 0;
                    z-index: 1040;
                    background-color: #000;*/
        top: 60px;
        /*right: 0;*/
        /*left: 0;*/
        bottom: 0;
        padding-top: 20px;
        background-color: rgba(0, 0, 0, 0.63);
        position: fixed;
        /*overflow-y:auto;*/
        z-index: 1940;
    }

    #ContentPlaceHolder1_pnlInvoiceReport table tr td {
        height: initial;
        width: initial;
        padding: initial;
        border: 0;
    }

    #ContentPlaceHolder1_pnlInvoiceReport table {
        border: 0;
    }

    #ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
        overflow-x: hidden !Important;
        border-bottom: 1px solid #ddd;
        padding-right: 5px;
    }

    @media (max-width:768px) {
        #ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
            overflow-x: auto !Important;
            border-bottom: 1px solid #ddd;
        }
    }

    @media print {
        ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
            width: 100%;
        }
    }
</style>
<script>
    function printDocument() {
        $("#<%=frmPrint.ClientID%>").get(0).contentWindow.print();
    }

</script>
<asp:Panel runat="server" ID="pnlInvoiceReport" CssClass="reportPopUp" Visible="false" TabIndex="-1">
    <div class="bodyPop">
        <div class="col-md-12">
            <div style="position: absolute; right: 0; top: 0px;">
                <asp:LinkButton ID="btnPrint" runat="server" CssClass="font-awesome-font btn btn-info" OnClick="print_Click"><i class="fa r fa-print"></i> Print</asp:LinkButton>
                <asp:Button CssClass="btn btn-danger" ID="btnCloseRpt" OnClick="btnCloseRpt_Click" Text="&#xf00d; Close" runat="server" Style="font-family: FontAwesome,'Source Sans Pro', sans-serif" />
            </div>
            <h3 class="text-center h3reset">
                <asp:Label ID="lblReportHeading" runat="server" /></h3>
            <hr />
        </div>
        <rsweb:ReportViewer Height="470px" ShowParameterPrompts="false" Width="100%" ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
    </div>
    <iframe id="frmPrint" name="IframeName" runat="server" style="display: none"></iframe>
</asp:Panel>

<asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
    <div class="panel panel-primary bodyPop" style="width: 30%; padding:0">
      <div class="panel-heading">
          <i class="fa fa-info-circle"></i> <asp:Label ID="lblAskMessage" Text="" runat="server" />           
      </div>
      <div class="panel-body">
          <div class="text-right">
                <asp:Button ID="btnYes" OnClick="btnYes_Click" CssClass="btn btn-primary" Text="Yes" runat="server" />
                <asp:Button ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
            </div>
      </div>
    </div>
</asp:Panel>
