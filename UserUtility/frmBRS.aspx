<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBRS.aspx.cs" Inherits="Vouchers_frmBRS" Culture="hi-IN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $('#btncancel').click(function () { $('#<%=myModal.ClientID%>').hide(); });
            $('.cashpayment_activeme').addClass('active');
        });
        $(document).ready(function () {
            $('#btncancelDetail').click(function () { $('#<%=myDetailModal.ClientID%>').hide(); });
            $('.cashpayment_activeme').addClass('active');
        });
        function LoadAllScript() {
            LoadBasic();
            $('#btncancel').click(function () { $('#<%=myModal.ClientID%>').hide(); });
            $('#btncancelDetail').click(function () { $('#<%=myDetailModal.ClientID%>').hide(); });
            $('#btnClose').click(function () { $('#<%=myModal.ClientID%>').hide(); });
        }
    </script>
    <style>
        .a {
            background-color: red;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel() {
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=ReceiptPopUp.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }

        function PrintDeatilPanel() {
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=pnlSummDetails.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For Password -------------------//

            if ($('#<%=txtPassword.ClientID%>').val() == '') {
                $('#<%=lblPassMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Password.');
                $('#<%=lblPassMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=txtPassword.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <style>
        .headerfontblack {
            color: black;
        }

        .btn-sxs1 {
            padding: 2px 10px;
        }

        .pnlgrd-Conten {
            overflow: auto;
            max-height: 355px;
        }

            .pnlgrd-Conten::-webkit-scrollbar {
                display: none;
            }

        .padding_rt_5 {
            padding-right: 5px !important;
        }

        .crdr_txt {
            float: left;
            width: 65% !important;
        }

        .crdr_ddl {
            padding-left: 0;
            float: left;
            width: 35%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
                <h3 class="text-center head">Bank Reconciliation Statement
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <table class="pdf_show mrbotm40 font12" style="width: 100%; margin-bottom: 10px">
                                        <tbody>
                                            <tr>
                                                <td colspan="100%" style="text-align: right"><b><span id="ContentPlaceHolder1_lblDate"></span></b></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%;">&nbsp;</td>
                                                <td style="width: 10%; text-align: right; padding-right: 3px"><b>Bank</b></td>
                                                <td style="width: 10%">
                                                    <asp:DropDownList ID="ddlBankAcc" AutoPostBack="true" OnSelectedIndexChanged="ddlBankAcc_SelectedIndexChanged" runat="server"></asp:DropDownList></td>
                                                <td style="width: 12%; text-align: right; padding-right: 3px"><b>From</b></td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="txtfrmDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox></td>
                                                <td style="width: 14%; text-align: right; padding-right: 3px"><b>To</b></td>
                                                <td style="width: 09%;">
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox></td>
                                                <td style="width: 14%; padding-left: 5px; text-align: left">
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" CssClass="btn btn-primary btn-sxs1" Text="Show" /></td>
                                                <td style="width: 13%"></td>

                                            </tr>
                                            <tr>
                                                <td colspan="100%">&nbsp;</td>
                                            </tr>
                                            <tr id="trDateAndBalance" style="background-color: #e8eef2" visible="false" runat="server">
                                                <td style="font-size: 17px;"><b>Reconciliation&nbsp;Status&nbsp;:</b></td>
                                                <td style="text-align: right; padding-right: 3px">
                                                    <b>
                                                        <asp:Label Text="Opening Date" runat="server" /></b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOpDate" runat="server" />
                                                </td>
                                                <td style="text-align: right; padding-right: 3px">
                                                    <b>
                                                        <asp:Label Text="Opening Balance" runat="server" /></b>
                                                </td>
                                                <td style="text-align: right; padding-right: 3px">
                                                    <asp:Label ID="lblOpBalance" runat="server" />
                                                </td>
                                                <td style="text-align: right; padding-right: 3px">
                                                    <b>
                                                        <asp:Label Text="Closing Date" runat="server" /></b>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCloDate" runat="server" />
                                                </td>
                                                <td style="text-align: right; padding-right: 3px">
                                                    <b>
                                                        <asp:Label Text="Closing Balance" runat="server" /></b>
                                                </td>
                                                <td style="text-align: right; padding-right: 3px">
                                                    <asp:Label ID="lblCloBalance" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trBlankRow" visible="false" runat="server">
                                                <td>&nbsp;</td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 17px;"><b>Bank&nbsp;Statement&nbsp;:</b></td>
                                                <td style="text-align: right; padding-right: 3px"><b>&nbsp;Opening&nbsp;Date</b></td>
                                                <td style="">
                                                    <asp:TextBox ID="txtopenDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox></td>
                                                <td style="text-align: right; padding-right: 3px"><b>Opening Balance</b>
                                                </td>
                                                <td style="">
                                                    <asp:TextBox ID="txtopenBal" CssClass="Money crdr_txt" AutoPostBack="true" OnTextChanged="txtopenBal_TextChanged" runat="server" MaxLength="9"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlDrCrOpenBal" runat="server" CssClass="crdr_ddl">
                                                        <asp:ListItem Value="0" Text="Dr" />
                                                        <asp:ListItem Value="1" Text="Cr" />
                                                    </asp:DropDownList>

                                                </td>
                                                <td style="text-align: right; padding-right: 3px;"><b>Closing Date</b></td>
                                                <td style="">
                                                    <asp:TextBox ID="txtcloseDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox></td>
                                                <td style="text-align: right; padding-right: 3px"><b>Closing Balance</b></td>
                                                <td style="padding-left: 5px">
                                                    <asp:TextBox ID="txtClosebal" runat="server" CssClass="Money crdr_txt" MaxLength="9" Style="width: 100%"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlDrCrCloseBal" runat="server" CssClass="crdr_ddl">
                                                        <asp:ListItem Value="0" Text="Dr" />
                                                        <asp:ListItem Value="1" Text="Cr" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table-bordered inf_head td_padding_rtl_5">
                                        <tr>
                                            <th colspan="6">Bank Book</th>
                                            <th rowspan="2" style="width: 1%; background-color: transparent !important">&nbsp;</th>
                                            <th colspan="2">Bank Statement</th>
                                        </tr>
                                        <tr>
                                            <th style="width: 4%">S.No.</th>
                                            <th style="width: 8%">Voucher Date</th>
                                            <th style="width: 13%">Cheque / UTR No.</th>
                                            <th style="width: 28%">Narration</th>
                                            <th style="width: 11%;" class="padding_rt_5">Dr. Amount (Rs.)</th>
                                            <th style="width: 11%;" class="padding_rt_5">Cr. Amount (Rs.)</th>
                                            <th style="width: 10%;">Date</th>
                                            <th style="width: 14%;">Amount (Rs.)</th>
                                        </tr>
                                    </table>
                                    <style>
                                        .td_padding_rtl_5 tr td:nth-child(4) {
                                            font-size: 12px;
                                        }
                                    </style>
                                    <asp:Panel ID="pnlgrid" CssClass="pnlgrd-Conten" runat="server">
                                        <asp:GridView runat="server" CssClass-="table-bordered td_padding_rtl_5" AllowSorting="True" ID="grdBRS" ShowHeader="false" OnRowDataBound="grdBRS_RowDataBound" AutoGenerateColumns="false">
                                            <HeaderStyle CssClass="inf_head" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="4%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrNo" Text='<%#Eval("SrNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="8%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVoucharNo" Text='<%#Eval("VoucharNo") %>' runat="server" CssClass="hidden"></asp:Label>
                                                        <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="13%" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChqNo" Text='<%#Eval("chequeNo") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="28%" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNarr" Text='<%#Eval("Narration") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="11%" ItemStyle-CssClass="text-right padding_rt_5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmtDr" Text='<%# Eval("DrAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="11%" ItemStyle-CssClass="text-right padding_rt_5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmtCr" Text='<%# Eval("CrAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-BackColor="#cccccc" ItemStyle-CssClass="b0">
                                                    <ItemTemplate>
                                                        <asp:Label Text="" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDate" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Text='<%#Eval("BSDate","{0:dd/MM/yyyy}") %>' MaxLength="10" CssClass="dateFormat" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="14%" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" CssClass="SelectedRow Money" MaxLength="15" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged" Text='<%#Eval("BSAmount") %>' onkeypress="javascript:this.value=Comma(this.value);" runat="server"></asp:TextBox>
                                                        <asp:Label ID="lblMatchInd" Text='<%#Eval("MatchInd") %>' Visible="false" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <script>
                                            
                                        </script>
                                    </asp:Panel>
                                    <div class="col-sm-9" style="margin-top: 5px">
                                        <asp:Label Text="Arranged By : " Style="font-size: 17px; font-weight: bold;" runat="server" />&nbsp; 
                                        <asp:RadioButton ID="rdByRow" Style="margin-left: 35px;" runat="server" GroupName="Arr" Enabled="false" AutoPostBack="true" OnCheckedChanged="rdByRow_CheckedChanged" Text="MisMatch / Match" />&nbsp; 
                                            <asp:RadioButton ID="rdBySerial" runat="server" GroupName="Arr" Enabled="false" AutoPostBack="true" OnCheckedChanged="rdBySerial_CheckedChanged" Text="Serial Number" />
                                    </div>
                                    <div class="col-sm-3" style="margin-top: 5px">
                                        <asp:LinkButton ID="lnkBSEntry" OnClick="lnkBSEntry_Click" Style="margin-left: 46px;" runat="server"><i class="fa r fa-plus" aria-hidden="true"></i>Bank Statement Entry</asp:LinkButton>
                                    </div>
                                    <asp:Panel ID="pnlBSE" Visible="false" runat="server">
                                        <br />
                                        <table class="table-bordered td_padding_rtl_5">
                                            <thead>
                                                <tr class="inf_head">
                                                    <th rowspan="2" style="width: 12%">Statement Date</th>
                                                    <th colspan="2">Amount (Rs.)</th>
                                                    <th rowspan="2" style="width: 34%">Narration</th>
                                                    <th rowspan="2" style="width: 1%; background-color: transparent !important">&nbsp;</th>
                                                    <th colspan="2">Bank Book</th>
                                                    <th colspan="2">&nbsp;</th>
                                                </tr>
                                                <tr class="inf_head">
                                                    <th style="width: 15.1%;" class="">Dr.</th>
                                                    <th style="width: 15.1%;" class="">Cr.</th>
                                                    <th style="width: 9%;">Date</th>
                                                    <th style="width: 12%;">Amount (Rs.)</th>
                                                    <th>&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtStatementDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDrAmt" CssClass="Money" MaxLength="9" AutoPostBack="true" OnTextChanged="txtDrAmt_TextChanged" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCrAmt" CssClass="Money" MaxLength="9" AutoPostBack="true" OnTextChanged="txtCrAmt_TextChanged" runat="server" />
                                                    </td>
                                                    <td>
                                                        <cc1:ComboBox ID="cbNarration" MaxLength="150" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    </td>
                                                    <td style="background-color: #cccccc"></td>
                                                    <%--<td>
                                                        <asp:TextBox ID="txtBSDate" Enabled="false" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBSAmt" Enabled="false" CssClass="Money" MaxLength="9" runat="server" />
                                                    </td>--%>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnAddBSEntry" CssClass="btn btn-primary btn-sxs" Enabled="false" OnClick="btnAddBSEntry_Click" Text="Add" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:GridView ID="grdBSEntry" CssClass="table-bordered td_padding_rtl_5" AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="grdBSEntry_RowCommand" OnRowDataBound="grdBSEntry_RowDataBound" runat="server">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="12%" ItemStyle-CssClass="c">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatementDate" Text='<%# Eval("VoucharDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="14.8%" ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDrAmt" Text='<%# Eval("DrAmount") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="14.8%" ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCrAmt" Text='<%# Eval("CrAmount") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="33.6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNarration" Text='<%# Eval("Narration") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="1%" ItemStyle-BackColor="#cccccc" ItemStyle-CssClass="b0">
                                                    <ItemTemplate>
                                                        <asp:Label Text="" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="9%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBBDate" MaxLength="10" CssClass="dateFormat" placeholder="DD/MM/YYYY" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="12%" ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBBAmount" CssClass="Money" MaxLength="9" runat="server" />
                                                        <asp:Label ID="lblExtraInd" Text='<%# Eval("ExtraInd") %>' Visible="false" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDel" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-danger btn-sxs" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                                <asp:HiddenField ID="hfMisMatchAmtInd" runat="server" />
                                <script>
                                    function Comma(Num) { //function to add commas to textboxes
                                        debugger
                                        Num += '';
                                        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
                                        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
                                        //Num = Num.replace('₹', '');

                                        x = Num.split('.');
                                        x1 = x[0];
                                        x2 = x.length > 1 ? '.' + x[1] : '';
                                        var rgx = /(\d+)(\d{3})/;
                                        while (rgx.test(x1))
                                            x1 = x1.replace(rgx, '$1' + ',' + '$2');
                                        //return '₹' + x1 + x2;
                                        return x1 + x2;
                                    }
                                </script>
                                <style>
                                    input[type=text].rupee {
                                        background-image: url("../Content/css/images/Rs.png");
                                        border: 1px solid #aaa;
                                        padding: 5px;
                                        padding-left: 20px;
                                        background-size: 20px 25px;
                                        background-repeat: no-repeat;
                                    }
                                </style>

                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-right">

                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSummary" runat="server" Enabled="false" OnClick="btnSummary_Click" CssClass="btn btn-warning btn-space-right" Text="Summary" />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary btn-space-right" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="btn btn-danger btn-space-right" Text="Clear" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <%-------------------------------------------Model PopUP----------------------------------------------------%>
                <div class="modal fade in" runat="server" id="myModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                    <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                        <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                            <asp:Panel ID="ReceiptPopUp" runat="server" Width="100%" Height="100%" Visible="true">
                                <div class="modal-header c" style="background-color: #d2dee8; color: #545454;">
                                    <h4 class="modal-title" style="text-align: center">
                                        <asp:Label ID="lblHeader" Text="Bank Reconciliation Summary" runat="server"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <style>
                                        .modal {
                                            display: none;
                                            overflow: hidden;
                                            position: fixed;
                                            top: 0;
                                            right: 0;
                                            bottom: 0;
                                            left: 0;
                                            z-index: 1050;
                                            -webkit-overflow-scrolling: touch;
                                            outline: 0;
                                        }

                                        .fade.in {
                                            opacity: 1;
                                        }

                                        .fade {
                                            opacity: 0;
                                            -webkit-transition: opacity .15s linear;
                                            -o-transition: opacity .15s linear;
                                            transition: opacity .15s linear;
                                        }

                                        .modal-dialog {
                                            width: 600px;
                                            margin: 30px auto;
                                        }

                                        .modal-content {
                                            -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                            box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                        }

                                        .modal-sm {
                                            width: 300px;
                                        }

                                        .modal-body {
                                            position: relative;
                                            padding: 4px 5px !important;
                                        }

                                            .modal-body table {
                                                font-size: 12px;
                                            }

                                        .table-bordered1 {
                                            border: 1px solid #918f8f; /*918f8f*/
                                        }

                                            .table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {
                                                border: 1px solid #918f8f;
                                            }

                                            .table-bordered1 > thead > tr > th, .table-bordered1 > thead > tr > td {
                                                border-bottom-width: 2px;
                                            }
                                    </style>
                                    <table class="table-bordered1" style="width: 100%">
                                        <tr>
                                            <th colspan="4" style="text-align: center">
                                                <asp:Label ID="lblSummaryBankName" runat="server" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <asp:Label ID="lblSummaryAsOnDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <b>
                                                    <asp:Label Text="Particular" runat="server"></asp:Label></b>
                                            </td>
                                            <td colspan="2" style="text-align: center"><b>Amount (Rs.)</b></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">Cl.Balance As Per Bank Book</td>
                                            <td colspan="2" style="text-align: right">
                                                <asp:Label ID="lblBalPerBB" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 74%; text-align: left">
                                                <asp:LinkButton ID="lknAddChqIssued" Text="Add: Chq. Issued But Not Presented" OnClick="lknAddChqIssued_Click" runat="server"></asp:LinkButton></td>
                                            <td colspan="1" style="width: 13%; text-align: right">
                                                <asp:Label ID="lblAddChqIssued" runat="server" /></td>
                                            <td colspan="1" style="width: 13%; text-align: right"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkAddChqCredit" Text="Add: Chq. Credited In Bank Statement Not In Bank Book" OnClick="lnkAddChqCredit_Click" runat="server"></asp:LinkButton></td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lbladdChqCredit" runat="server"></asp:Label></td>
                                            <td colspan="1" style="text-align: right"></td>
                                        </tr>

                                        <tr id="trAddMisMatchCreSide" visible="false" runat="server">
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkAddMisMatchCreSide" Text="Add: Amount MisMatch In Bank Book And Bank Statement (Credit Side)" OnClick="lnkAddMisMatchCreSide_Click" runat="server"></asp:LinkButton></td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblAddMisMatchCreSide" runat="server"></asp:Label></td>
                                            <td colspan="1" style="text-align: right"></td>
                                        </tr>
                                        <tr id="trAddMisMatchDebSide" visible="false" runat="server">
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkAddMisMatchDebSide" Text="Add: Amount MisMatch In Bank Book And Bank Statement (Debit Side)" OnClick="lnkAddMisMatchDebSide_Click" runat="server"></asp:LinkButton></td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblAddMisMatchDebSide" runat="server"></asp:Label></td>
                                            <td colspan="1" style="text-align: right"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkAddIntCredit" Text="Add: Interest Credited By Bank Not In Bank Book" OnClick="lnkAddIntCredit_Click" runat="server"></asp:LinkButton>
                                            </td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblAddIntCredit" runat="server"></asp:Label></td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblAddTotal" Style="color: #27c24c" runat="server"></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td colspan="3" style="text-align: right"><b>Total</b></td>
                                            <td colspan="1" style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTot1" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkLessChqDepo" Text="Less: Chq. Deposited But Not Collected" OnClick="lnkLessChqDepo_Click" runat="server"></asp:LinkButton>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblLessChqDepo" runat="server"></asp:Label></td>
                                                <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkLessChqDebited" Text="Less: Chq. Debited In Bank Statement Not In Bank Book" OnClick="lnkLessChqDebited_Click" runat="server"></asp:LinkButton>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbllessChqDebited" runat="server"></asp:Label></td>
                                                <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkLessBankChaDeb" Text="Less: Bank Charges Debited By Bank Not In Bank Book" OnClick="lnkLessBankChaDeb_Click" runat="server"></asp:LinkButton>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblLessBankChaDeb" runat="server"></asp:Label></td>
                                                <td></td>
                                        </tr>
                                        <tr id="trLessMisMatchCreSide" visible="false" runat="server">
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkLessMisMatchCreSide" Text="Less: Amount MisMatch In Bank Book And Bank Statement (Credit Side)" OnClick="lnkLessMisMatchCreSide_Click" runat="server"></asp:LinkButton>
                                            </td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblLessMisMatchCreSide" runat="server"></asp:Label></td>
                                            <td colspan="1" style="text-align: right"></td>
                                        </tr>
                                        <tr id="trLessMisMatchDebSide" visible="false" runat="server">
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkLessMisMatchDebSide" Text="Less: Amount MisMatch In Bank Book And Bank Statement (Debit Side)" OnClick="lnkLessMisMatchDebSide_Click" runat="server"></asp:LinkButton>
                                            </td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblLessMisMatchDebSide" runat="server"></asp:Label></td>
                                            <td colspan="1" style="text-align: right"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <asp:LinkButton ID="lnkLessIntDeb" Text="Less: Interest Debited By Bank Not In Bank Book" OnClick="lnkLessIntDeb_Click" runat="server"></asp:LinkButton>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblLessIntDeb" runat="server"></asp:Label></td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblLessTotal" Style="color: #27c24c" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: right"><b>Total</b></td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTot2" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: left">Cl. Balance As Per Bank Statement 
                                            </td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblClBalAs" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: right;">Difference In Reconciliation</td>
                                            <td colspan="1" style="text-align: right">
                                                <asp:Label ID="lblDiffBal" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: left;">Opening Balance Difference</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: left;">Balance As Per Bank Book</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblBalAsPerBB" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: left;">Balance As Per Bank Statement</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblBalAsPerBs" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: right;">Difference In Op. Balance</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblDiifInOpBal" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: left;"><b>Reconciliation Status</b></td>
                                            <td colspan="1"><b>
                                                <asp:Label ID="lblMatchUnmatched" runat="server"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <div class="modal-footer">
                                <button type="button" id="btnsuccess" class="font-awesome-font btn btn-info" onclick="PrintPanel();" data-dismiss="modal">Print</button>
                                <input type="button" id="btncancel" class="btn btn-danger" value="Cancel" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>

                <style>
                    .divOveFlow {
                        max-height: 370px;
                        overflow-y: scroll;
                    }
                    .p-r17{
                        padding-right:17px;
                    }
                </style>

                <div class="modal fade in" runat="server" id="myDetailModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                    <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                        <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                            <asp:Panel ID="pnlSummDetails" runat="server" Width="100%" Height="100%" Visible="true">
                                <div class="modal-header c" style="background-color: #d2dee8; color: #545454;">
                                    <h4 class="modal-title" style="text-align: center">
                                        <asp:Label ID="Label1" Text="Bank Reconciliation Summary Details" runat="server"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <style>
                                        .modal {
                                            display: none;
                                            overflow: hidden;
                                            position: fixed;
                                            top: 0;
                                            right: 0;
                                            bottom: 0;
                                            left: 0;
                                            z-index: 1050;
                                            -webkit-overflow-scrolling: touch;
                                            outline: 0;
                                        }

                                        .fade.in {
                                            opacity: 1;
                                        }

                                        .fade {
                                            opacity: 0;
                                            -webkit-transition: opacity .15s linear;
                                            -o-transition: opacity .15s linear;
                                            transition: opacity .15s linear;
                                        }

                                        .modal-dialog {
                                            width: 600px;
                                            margin: 30px auto;
                                        }

                                        .modal-content {
                                            -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                            box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                        }

                                        .modal-sm {
                                            width: 300px;
                                        }

                                        .modal-body {
                                            position: relative;
                                            padding: 4px 5px !important;
                                        }

                                            .modal-body table {
                                                font-size: 12px;
                                            }

                                        .table-bordered1 {
                                            border: 1px solid #918f8f; /*918f8f*/
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
                                        .width10 {
                                            width: 10%;
                                        }

                                        .width65 {
                                            width: 65%;
                                        }
                                        .width50 {
                                            width: 50%;
                                        }
                                        .width15 {
                                            width: 15%;
                                            text-align: right;
                                        }
                                    </style>
                                    <table class="table-bordered1" style="width: 100%">
                                        <tr>
                                            <th colspan="6" style="text-align: center">
                                                <asp:Label ID="lblSummDetailsBankName" runat="server" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <th colspan="6" style="text-align: center">
                                                <asp:Label ID="lblParticularSummName" runat="server" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <th colspan="6" style="text-align: center">
                                                <asp:Label ID="lblSummDetailAsOnDate" runat="server" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div class="divOveFlow">
                                                    <asp:GridView ID="grdSummDetails" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide" OnRowDataBound="grdSummDetails_RowDataBound" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderVD" Text="Voucher Date" runat="server"></asp:Label></th>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderCN" Text="Cheque No." runat="server"></asp:Label></th>
                                                                        <th style="width: 65%"><asp:Label ID="lblHeaderNarration" Text="Narration" runat="server"></asp:Label></th>
                                                                        <th style="width: 15%"><asp:Label ID="lblHeaderAmount" Text="Amount (Rs.)" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                    <tr class="hide_my_pdosi">
                                                                    </tr>
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="calign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChequeNo" Text='<%#Eval("ChequeNo") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNarration" Text='<%#Eval("Narration") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="ralign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="divOveFlow">
                                                    <asp:GridView ID="grdMisMatchSummaryDetails" Visible="false" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide" OnRowDataBound="grdMisMatchSummaryDetails_RowDataBound" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <tr>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderVD" Text="Voucher Date" runat="server"></asp:Label></th>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderCN" Text="Cheque No." runat="server"></asp:Label></th>
                                                                        <th style="width: 50%"><asp:Label ID="lblHeaderNarration" Text="Narration" runat="server"></asp:Label></th>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderAmount" Text="Amount (Rs.)" runat="server"></asp:Label></th>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderBSAmount" Text="BS Amount (Rs.)" runat="server"></asp:Label></th>
                                                                        <th style="width: 10%"><asp:Label ID="lblHeaderDiffAmount" Text="Diff. Amount (Rs.)" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                    <tr class="hide_my_pdosi">
                                                                    </tr>
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="calign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChequeNo" Text='<%#Eval("ChequeNo") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNarration" Text='<%#Eval("Narration") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="ralign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="ralign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBSAmount" Text='<%#Eval("BSAmount") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="ralign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiffAmount" Text='<%#Eval("DiffAmount") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trAmtSummDetails" runat="server">
                                            <td colspan="6">
                                                <div class="p-r17">
                                                    <table class="table-bordered1" style="width: 100%;">
                                                        <tr>
                                                            <td class="width10">
                                                                <b>Total</b>
                                                            </td>
                                                            <td class="width10">
                                                                <asp:Label ID="lblChequeNoCount" runat="server" /></td>
                                                            <td class="width65"></td>
                                                            <td class="width15 ralign">
                                                                <asp:Label ID="lblSummDetailTotalAmt" runat="server" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trAmtMisMatch" runat="server">
                                            <td colspan="6">
                                                <div class="p-r17">
                                                    <table class="table-bordered1" style="width: 100%;">
                                                        <tr>
                                                            <td class="width10">
                                                                <b>Total</b>
                                                            </td>
                                                            <td class="width10">
                                                                <asp:Label ID="lblAmtMisMatchChequeNo" runat="server" /></td>
                                                            <td class="width50"></td>
                                                            <td class="width10 ralign">
                                                                <asp:Label ID="lblAmtMisMatchSummDetailTotalAmt" runat="server" /></td>
                                                            <td class="width10 ralign">
                                                                <asp:Label ID="lblSummDetailToTBSAmt" runat="server" /></td>
                                                            <td class="width10 ralign">
                                                                <asp:Label ID="lblSummDetailToTDiffAmt" runat="server" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <div class="modal-footer">
                                <button type="button" id="btnPrintDetails" class="font-awesome-font btn btn-info" onclick="PrintDeatilPanel();" data-dismiss="modal">Print</button>
                                <input type="button" id="btncancelDetail" class="btn btn-danger" value="Cancel" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>

                <asp:Panel runat="server" ID="pnlPassword" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0" DefaultButton="btnSubmit">
                    <div class="panel panel-primary bodyPop" style="width: 30%; padding: 0; position: center">
                        <div class="panel-heading" style="text-align: center">
                            <b>Verification Password</b>
                        </div>
                        <br />
                        <div class="col-sm-12">
                            <div class="col-sm-5"><b>Enter Password</b></div>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtPassword" MaxLength="10" TextMode="Password" ValidationGroup="Pass" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="text-right">
                                <asp:Label ID="lblPassMsg" CssClass="text-danger lblMsg" runat="server" />
                                <asp:Button ID="btnSubmit" ValidationGroup="Pass" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return Validation()" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlPopUp" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0" DefaultButton="btnSubmit">
                    <div class="panel panel-primary bodyPop" style="width: 30%; padding: 0; position: center">
                        <div id="divpnlPopUp" class="panel-heading" style="text-align: center" runat="server">
                            <b>
                                <asp:Label ID="lblAlterHeader" runat="server"></asp:Label></b>
                        </div>
                        <br />
                        <div class="col-sm-12">
                        </div>
                        <div class="panel-body">
                            <div class="text-right">
                                <asp:Button ID="btnYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnYes_Click" runat="server" />
                                <asp:Button ID="btnNo" CssClass="btn btn-danger" Text="No" Visible="false" OnClick="btnNo_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlAfterSucess" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0" DefaultButton="btnSubmit">
                    <div class="panel panel-primary bodyPop" style="width: 30%; padding: 0; position: center">
                        <div id="divheadingAfterSucess" class="panel-heading" style="text-align: center; background-color: #27c24c; color: white" runat="server">
                            <b><i class="fa fa-check-circle"> Data Successfully Submited.</i></b>
                        </div>
                        <div class="panel-body">
                            <div>
                                <b>Do You Want To See Summary.</b>
                            </div>
                            <div class="text-right">
                                <asp:Button ID="btnASYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnASYes_Click" runat="server" />
                                <asp:Button ID="btnASNo" CssClass="btn btn-danger" Text="No" OnClick="btnASNo_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hfMSGInd" runat="server" />
                <style>
                    .reportPopUp {
                        top: 60px;
                        bottom: 0;
                        padding-top: 20px;
                        background-color: rgba(0, 0, 0, 0.63);
                        position: fixed;
                        z-index: 1940;
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
                </style>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
