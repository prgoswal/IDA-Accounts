<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmAdvRefundVoucher.aspx.cs" Inherits="Vouchers_frmAdvRefundVoucher" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">ADVANCE REFUND VOUCHER
                    <asp:Label ID="lblVoucherAndDate" CssClass="invoiceHead" runat="server"></asp:Label>
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                    <table class="table-bordered mb20" style="width: 400px; margin-bottom: 10px">
                                        <tr>
                                            <th style="width: 60%">Advance Received Voucher No.</th>
                                            <td style="width: 37%">
                                                <asp:TextBox ID="txtVoucherNo" placeholder="Voucher No." CssClass="text-right numberonly" MaxLength="7" runat="server" /></td>
                                            <td style="width: 3%">
                                                <asp:Button ID="btnSearchVoucher" Text="Go" CssClass="btn btn-primary btn-xs" Style="margin-left: -2px; padding-top: 2px; padding-bottom: 2px; padding-right: 5px; padding-left: 4px;" OnClick="btnSearchVoucher_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table class=" table-bordered table_sar mb20" style="margin-bottom: 10px">
                                        <tr class="inf_head">
                                            <td colspan="7"></td>
                                        </tr>
                                        <tr class="inf_head">
                                            <th style="width: 10%">Voucher Date</th>
                                            <th style="width: 37%">Cash/Bank Account</th>
                                            <th style="width: 38%">Account Head</th>
                                            <th style="width: 15%">GSTIN</th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker" placeholder="DD/MM/YYYY" runat="server" /></td>
                                            <td>
                                                <asp:DropDownList ID="ddlCashBankAccount" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankAccount_SelectedIndexChanged" runat="server" /></td>
                                            <td>
                                                <asp:DropDownList ID="ddlAccountHead" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged" runat="server" /></td>
                                            <td>
                                                <asp:DropDownList ID="ddlGSTINNo" Enabled="false" runat="server" /></td>
                                        </tr>
                                    </table>
                                    <style>
                                        .trr {
                                            max-width: 500px;
                                            width: 100%;
                                            float: none;
                                            margin: 0 auto;
                                            margin-bottom: 0px;
                                        }
                                    </style>
                                    <div id="divGRDAdvanceRefund" style="margin-bottom: 15px" visible="false" runat="server">
                                        <table class="table-bordered table_aav trr">
                                            <tr class="inf_head">
                                                <th colspan="4" class="inf_head">Refund Advance Received Amount</th>
                                            </tr>
                                            <tr class="inf_head">
                                                <td style="width: 07%"></td>
                                                <th style="width: 31%">Tax Rate</th>
                                                <th style="width: 31%">Received Amount</th>
                                                <th style="width: 31%">Refund Amount</th>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="grdAdvanceRefund" RowStyle-BackColor="" HeaderStyle-CssClass="inf_head" CssClass="table-bordered table_aav mb20 trr" ShowHeader="false" AutoGenerateColumns="false" runat="server" Style="width: 600px">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="c" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbPartyAdvance" AutoPostBack="true" OnCheckedChanged="cbPartyAdvance_CheckedChanged" runat="server" />
                                                        <asp:Label ID="lblVoucharNo" Text='<%#Eval("VoucharNo") %>' Visible="false" runat="server" />
                                                        <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate", "{0:dd/MM/yyyy}") %>' Visible="false" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="text-right" ItemStyle-Width="31%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="text-right" ItemStyle-Width="31%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReceivedAmount" Text='<%#Eval("AdvAmount") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="text-right" ItemStyle-Width="31%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRefundAmount" placeholder="Refund Amount" CssClass="Money" MaxLength="9" Enabled="false" AutoPostBack="true" OnTextChanged="txtRefundAmount_TextChanged" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <style>
                                        .sar_table3 {
                                            width: 85%;
                                        }

                                        .sar_table3 {
                                            width: 85%;
                                            float: left;
                                        }
                                        .sar_table4 {
                                            width: 15%;
                                        }

                                        .sar_table4 {
                                            width: 15%;
                                            float: left;
                                        }
                                    </style>
                                    <table class="sar_table3 table-bordered ">
                                        <tbody>
                                            <tr class="inf_head">
                                                <td colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <div id="divPayMode" runat="server">
                                                    <th class="col1 l">Refund By
                                                    </th>
                                                    <td class="col2">
                                                        <asp:DropDownList ID="ddlPayMode" Style="margin-top: 21px" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" runat="server">
                                                            <asp:ListItem Value="Cheque" Text="Cheque" />
                                                            <asp:ListItem Value="UTR" Text="RTGS/NEFT" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="col3">
                                                        <asp:Label ID="lblPayModeNo" runat="server">Cheque No.</asp:Label>
                                                        <asp:TextBox ID="txtReceivedNo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="col4">
                                                        <asp:Label ID="lblPayModeDate" runat="server">Cheque Date</asp:Label>
                                                        <asp:TextBox ID="txtReceivedDate" MaxLength="10" placeholder=" DD/MM/YYYY" runat="server" CssClass="datepicker"></asp:TextBox>
                                                    </td>
                                                </div>
                                            </tr>
                                            <tr>
                                                <th class="col1 l">Narration</th>
                                                <td colspan="3">
                                                    <cc1:ComboBox ID="cbNarration" runat="server" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table sar_table4 gstr_sales_amount table-bordered pull-right">
                                        <thead>
                                            <tr>
                                                <th class="inf_head">Refundable Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="amount_col3">
                                                    <asp:TextBox ID="txtTotalRefundAmount" Enabled="false" runat="server" placeholder="Refundable Amount" CssClass="Money"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary btn-space-right" Enabled="false" OnClick="btnSave_Click" runat="server" />
                                            <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />
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

