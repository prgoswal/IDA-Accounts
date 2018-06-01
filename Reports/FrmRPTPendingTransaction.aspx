<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="FrmRPTPendingTransaction.aspx.cs" Inherits="Reports_FrmRPTPendingTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function PrintPanel() {
            var panel = document.getElementById("<%=pnldata.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1000');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('<style> @media print {@page{size:landscape;margin: 0.2in 0.2in 0.2in 0.2in;}.table-bordered1 {border: 1px solid #918f8f;Width:100%}.table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {border: 1px solid #918f8f;}.table-bordered1 > thead > tr > th, .table-bordered1 > thead > tr > td {border-bottom-width: 2px;}.width4{width:4%;}.width8{width:8%;}.width9{width:9%}.width10{width:10%}.width15{width:15%}.width18{width:18%}.width29{width:29%}.pr17{padding-right:0px;}.pTransHeader{color:black}</style>');
            //printWindow.document.write('<style> @media print {@page{size:landscape;margin: 0.2in 0.2in 0.2in 0.2in;} table tr td,table tr th{font-size:10px;font-family:Arial;border:0.5px solid #000 !important;} .toptable{ margin-left:5px; margin-right:15px;} tr{border-top:1px solid #000 !important;}th {color:#000;background:#fff}.xbrk {word-break:break-all}.xaddress{width:150px}</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            printWindow.print();
            return false;
        }

        function DateValidate() {
            if ($('#<%=txtFromDate.ClientID%>').val() == '' || $('#<%=txtFromDate.ClientID%>').val() != '') {

                var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                var DateId = document.getElementById("<%= txtFromDate.ClientID %>").value;
                var valid = DateRegex.test(DateId);
                if (!valid) {
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter From  Date (dd/MM/yyyy)');
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=txtFromDate.ClientID%>').focus();
                    return false;
                }
            }

            if ($('#<%=txtToDate.ClientID%>').val() == '' || $('#<%=txtToDate.ClientID%>').val() != '') {

                var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                var DateId = document.getElementById("<%= txtToDate.ClientID %>").value;
                var valid = DateRegex.test(DateId);
                if (!valid) {
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter Valid  Date (dd/MM/yyyy)');
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=txtToDate.ClientID%>').focus();
                    return false;
                }
            }

        }
    </script>
    <script>
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>
    <style>
        .table-bordered1 {
            border: 1px solid #918f8f; 
        }

            .table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {
                border: 1px solid #918f8f;
            }

            .table-bordered1 > thead > tr > th, .table-bordered1 > thead > tr > td {
                border-bottom-width: 2px;
            }

        .first_tr_hide tbody tr:first-child {
            display: none;
        }

        .hide_my_pdosi + tr {
            display: none;
        }

        .first_tr_hide tr td:first-child {
            display: none;
        }

        .calign {
            text-align: center;
        }

        .ralign {
            text-align: right;
        }

        .width4 {
            width: 4%;
        }

        .width7 {
            width: 7%;
        }

        .width8 {
            width: 8%;
        }

        .width9 {
            width: 9%;
        }

        .width10 {
            width: 10%;
        }

        .width18 {
            width: 18%;
        }

        .width15 {
            width: 15%;
        }

        .width29 {
            width: 29%;
        }

        .marginRigth {
            margin-right: 5px;
        }

        .brk-all {
            word-break: break-all;
        }

            .brk-all tr td, .brk-all tr th {
                padding: 1px;
                font-size: 13px;
            }

        .scroll-on-web {
            max-height: 320px;
            overflow-y: scroll;
            display: inline-block;
        }
        body {
            overflow-x: hidden;
        }

        .pr17 {
            padding-right: 17px;
        }
        .pTransHeader
        {
            color: White; background-color: #006699; font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">PENDING TRANSACTION
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <table class="pdf_show">
                                        <tr>
                                            <td class="pdf_show_col1 r pr">From Date</td>
                                            <td class="pdf_show_col2 r">
                                                <asp:TextBox ID="txtFromDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" /></td>
                                            <td class="pdf_show_col3 r pr">To Date</td>
                                            <td class="pdf_show_col4 r">
                                                <asp:TextBox ID="txtToDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                            </td>
                                            <td class="pdf_show_col7" style="padding-left: 10px;">
                                                <asp:Button Text="Show" ID="btnShow" runat="server" OnClientClick="return DateValidate()" class="btn btn-sxs btn-primary" OnClick="btnShow_Click"></asp:Button>
                                                <asp:Button Text="Clear" ID="btnClear" runat="server" class="btn btn-sxs btn-danger" OnClick="btnClear_Click"></asp:Button>
                                                <asp:Button ID="btnPrint" Text="Print" CssClass="btn btn-sxs btn-info" Visible="false" OnClientClick="PrintPanel();" runat="server"></asp:Button></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div id="divShorting" runat="server" visible="false" style="float: left;">
                                        <label>Sort&nbsp;By&nbsp;:-&nbsp;</label>
                                        <asp:RadioButton ID="rdAll" GroupName="Short" Checked="true" Text="All" AutoPostBack="true" OnCheckedChanged="rdAll_CheckedChanged" runat="server" />&nbsp;
                                        <asp:RadioButton ID="rdAudit" GroupName="Short" Text="Audit" AutoPostBack="true" OnCheckedChanged="rdAudit_CheckedChanged" runat="server" />&nbsp;
                                        <asp:RadioButton ID="rdCashier" GroupName="Short" Text="Cashier" AutoPostBack="true" OnCheckedChanged="rdCashier_CheckedChanged" runat="server" />&nbsp;
                                        <asp:RadioButton ID="rdAO" GroupName="Short" Text="AO" AutoPostBack="true" OnCheckedChanged="rdAO_CheckedChanged" runat="server" />&nbsp;
                                    </div>
                                    <div id="divPendingSince" runat="server" visible="false" style="float: right; margin-right: 17px">
                                        <label style="float: left">Total Pending Transaction : &nbsp;</label>
                                        <asp:Label ID="lblTotalPendingSince" runat="server" Style="background-color: red; border-radius: 100%; color: white; text-align: center; padding: 3px; font-size: 12px;"></asp:Label><label style="float: right"></label>
                                    </div>
                                    <div id="pnldata" runat="server">
                                        <div class="pr17">
                                            <table id="tblPendingTransHeader" class="table-bordered1" visible="false" runat="server" style="width: 100%">
                                                <tr class="pTransHeader">
                                                    <th colspan="4">Transaction</th>
                                                    <th rowspan="2" class="width10">Reference&nbsp;No.</th>
                                                    <th rowspan="2" class="width18">Party&nbsp;Name</th>
                                                    <th rowspan="2" class="width15">User&nbsp;Name</th>
                                                    <th rowspan="2" class="width29">Narration</th>
                                                </tr>
                                                <tr class="pTransHeader">
                                                    <th class="width4">No.</th>
                                                    <th class="width8">Date</th>
                                                    <th class="width9">Type</th>
                                                    <th class="width8">Amount (Rs.)</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="divGrd" class="scroll-on-web" visible="false" runat="server">
                                            <asp:GridView ID="grdPendingTransaction" CssClass="table-bordered1" AutoGenerateColumns="false"
                                                runat="server" EmptyDataText="Record Not Found." ShowHeader="false" Width="100%">
                                                <Columns>
                                                    <%--<asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <th colspan="4">Transaction</th>
                                                                <th rowspan="2" style="width: 10%">Reference&nbsp;No.</th>
                                                                <th rowspan="2" style="width: 18%">Party&nbsp;Name</th>
                                                                <th rowspan="2" style="width: 16%">User&nbsp;Name</th>
                                                                <th rowspan="2" style="width: 30%">Narration</th>
                                                            </tr>
                                                            <tr class="hide_my_pdosi">
                                                                <th style="width: 4%">No.</th>
                                                                <th style="width: 7%">Date</th>
                                                                <th style="width: 7%">Type</th>
                                                                <th style="width: 8%">Amount (Rs.)</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField ItemStyle-CssClass="calign width4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionNo" runat="server" Text='<%#Eval("TransactionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="calign width8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" Text='<%#Eval("TransactionDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="calign width9">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocType" Text='<%#Eval("DocTypeID") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocTypeDesc" Text='<%#Eval("DocumentType") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="ralign width8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionAmount" Text='<%#Eval("NetAmount","{0:###,###.00}") %>' CssClass="marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="width10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferenceNo" Text='<%#Eval("Remark3") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="width18">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeptCD" Text='<%#Eval("DeptCD") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsFinal" Text='<%#Eval("IsFinal") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsAudit" Text='<%#Eval("IsAudit") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsSendToAudit" Text='<%#Eval("IsSendToAudit") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblAccCode" Text='<%#Eval("AccCode") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblCCCode" Text='<%#Eval("CCCode") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblBankPayInd" Text='<%#Eval("BankPayInd") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPurchaseBankPayInd" Text='<%#Eval("PurchaseBankPayInd") %>' Visible="false" runat="server"></asp:Label>

                                                            <asp:Label ID="lblChequeNo" Text='<%#Eval("ChequeNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblChequeDate" Text='<%#Eval("ChequeDate") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblUTRNo" Text='<%#Eval("UTRNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblUTRDate" Text='<%#Eval("UTRDate") %>' Visible="false" runat="server"></asp:Label>

                                                            <asp:Label ID="lblPartyName" Text='<%#Eval("PartyName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="width15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastApprovedBy" Text='<%#Eval("LastApprovedBy") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblDepartmentName" Text='<%#Eval("DepartmentName") %>' Visible="false" CssClass="marginLeft" runat="server"></asp:Label>
                                                            <asp:Label ID="lblSubDepartmentName" Text='<%#Eval("SubDepartmentName") %>' Visible="false" CssClass="marginLeft" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="width29">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNarration" Text='<%#Eval("Narration") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
