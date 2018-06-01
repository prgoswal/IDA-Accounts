<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmDuplicateInvoice.aspx.cs" Inherits="UserUtility_frmDuplicateInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $('.cashpayment_activeme').addClass('active');
        });
        function LoadAllScript() {

            LoadBasic();

        }
    </script>
    <style>
        .btn-sxs1 {
            padding: 2px 10px;
        }

        .pnlgrd-Conten {
            overflow: auto;
            max-height: 355px;
        }

        /*.pnlgrd-Conten::-webkit-scrollbar {
                display: none;
            }*/

        .padding_rt_5 {
            padding-right: 5px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <h3 class="text-center head">Duplicate Invoice
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
                                                <td style="width: 10%; text-align: right; padding-right: 3px"><b>Last Month</b></td>

                                                <td style="width: 5%">

                                                    <asp:TextBox ID="txtMonthyear" runat="server" Enabled="false"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddlMnthyear" runat="server"></asp:DropDownList></td>--%>
                                                    <%--<td style="width: 5%; text-align: right; padding-right: 3px"><b>From</b></td>
                                                <td style="width: 10%">
                                                    <asp:TextBox ID="txtfrmDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox></td>
                                                <td style="width: 7%; text-align: right; padding-right: 3px"><b>To</b></td>
                                                <td style="width: 11%;">
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox></td>--%>
                                                    <td style="width: 20%; padding-left: 5px">
                                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" CssClass="btn btn-primary btn-sxs1" Text="Show" />

                                                        <%--<a href="#" class="btn btn-danger btn-sxs1" style="margin-left:4px">Clear</a></td>--%>
                                            </tr>

                                        </tbody>
                                    </table>

                                    <table class="table-bordered inf_head td_padding_rtl_5">
                                        <tr>

                                            <th rowspan="2" style="width: 15%">Invoice No </th>
                                            <th rowspan="2" style="width: 15%">Invoice Date </th>
                                            <th colspan="2" style="width: 20%">Amount</th>
                                            <th rowspan="2" style="width: 20%">Party name</th>
                                            <th rowspan="2" style="width: 30%">Narration</th>
                                    </table>
                                    <asp:Panel ID="pnlgrid" CssClass="pnlgrd-Conten scroll_gst" runat="server">
                                        <asp:GridView runat="server" CssClass-="table-bordered td_padding_rtl_5" ID="grdDupInvoice" ShowHeader="false" AutoGenerateColumns="false">
                                            <HeaderStyle CssClass="inf_head" />
                                            <Columns>

                                                <%-- <asp:TemplateField ItemStyle-Width="4%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label Text="01" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceSeries" Text='<%#Eval("InvoiceSeries") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-CssClass="text-right padding_rt_5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" Text='<%# Eval("NetAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyCode" CssClass="hidden" Text='<%#Eval("AccountCode") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblPartyName" Text='<%#Eval("AccName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNarr" Text='<%#Eval("Narration") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#Eval("Checked") %>' AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>


                                    <asp:Panel ID="Panel1" CssClass="pnlgrd-Conten" runat="server">
                                        <asp:GridView runat="server" CssClass-="table-bordered td_padding_rtl_5" ID="GrdDuplicate" ShowHeader="false" AutoGenerateColumns="false">
                                            <HeaderStyle CssClass="inf_head" />
                                            <Columns>

                                                <%-- <asp:TemplateField ItemStyle-Width="4%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label Text="01" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="15%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-CssClass="text-right padding_rt_5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" Text='<%# Eval("NetAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartyCode" CssClass="hidden" Text='<%#Eval("AccountCode") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblPartyName" Text='<%#Eval("AccName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNarr" Text='<%#Eval("Narration") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-space-right" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="btn btn-danger btn-space-right" Text="Clear" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

