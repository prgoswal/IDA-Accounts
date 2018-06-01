<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmAdvanceLiability.aspx.cs" Inherits="GSTReturns_frmAdvanceLiability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">function LoadAllScript() { LoadBasic(); ChoosenDDL(); }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">Advance Liability <span class="invoiceHead"><asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span> </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">

                            <div class="panel-body">
                                <div class="row form-group">
                                    <div class="col-xs-12 mnth">
                                        <table class="pdf_show mrbotm40 font12" style="width: 1000px;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 5%; text-align: right; padding-right: 3px"><b>Month</b></td>
                                                    <td style="width: 8%">
                                                        <asp:DropDownList ID="ddlMonth" runat="server">
                                                            <asp:ListItem Value="0" Text="--Select--" />
                                                            <asp:ListItem Value="7" Text="July" />
                                                            <asp:ListItem Value="8" Text="August" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 5%; text-align: right; padding-right: 3px"><b>Year</b></td>
                                                    <td style="width: 8%">
                                                        <asp:DropDownList ID="ddlYear" runat="server">
                                                            <asp:ListItem Value="17" Text="2017" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 38%">
                                                        <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sxs" Text="Go" OnClick="btnGo_Click" runat="server" />
                                                        <asp:Label ID="lblMsg" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-xs-8 col-xs-offset-2">
                                        <table class="table-bordered inf_head">
                                            <tr>
                                                <th class="w10">Tax Rate</th>
                                                <th class="w30">Party Name</th>
                                                <th class="w20">Advance Amount (Rs.)</th>
                                                <th class="w20">Adjusted Amount (Rs.)</th>
                                                <th class="w20">Balance Amount (Rs.)</th>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GVAdvanceLiability" ShowHeader="false" AutoGenerateColumns="false" CssClass="table-bordered mb0" runat="server">
                                            <Columns>
                                                <asp:BoundField DataField="TaxRate"     ItemStyle-CssClass="w10 r"  />
                                                <asp:BoundField DataField="PartyName"     ItemStyle-CssClass="w30 c"  />
                                                <asp:BoundField DataField="AdvAmount"   ItemStyle-CssClass="w20 r" DataFormatString="{0:N}" />
                                                <asp:BoundField DataField="AdjAmount"     ItemStyle-CssClass="w20 r" DataFormatString="{0:N}"  />
                                                <asp:BoundField DataField="BalanceAmount" ItemStyle-CssClass="w20 r" DataFormatString="{0:N}" />
                                            </Columns>
                                        </asp:GridView>

                                        <table class="table-bordered text-bold" style="margin-top:20px; font-size:16px">
                                            <tr>
                                                <td class="w40 text-right">Total Unadjusted Advance Amount (Rs.)</td>
                                                <td class="w20 r"> <asp:Label ID="lblTotalAdvAmt" runat="server" /> </td>
                                                <td class="w20 r"> <asp:Label ID="lblTotalAdjAmt" runat="server" /> </td>
                                                <td class="w20 r"> <asp:Label ID="lblTotalBalAmt" runat="server" /> </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <table class="" style="width: 100%">
                                            <tr>
                                                <th colspan="6" style="width: 59%">&nbsp;</th>
                                                <th style="width: 08%">
                                                    <asp:Label ID="lblTotalAmount" Text="Total Amount : " Visible="false" runat="server"></asp:Label></th>
                                                <th style="width: 08%; text-align: right">
                                                    <asp:Label ID="lblTotalAmountValue" runat="server" /></th>
                                                <th style="width: 06%; text-align: right">
                                                    <asp:Label ID="lblSpecificAmount" runat="server" /></th>
                                                <th style="width: 06%; text-align: right">
                                                    <asp:Label ID="lblURDAmount" runat="server" /></th>
                                                <th style="width: 06%; text-align: right">
                                                    <asp:Label ID="lblExemptedAmount" runat="server" /></th>
                                                <th style="width: 04%"></th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
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
                            <div class="panel-footer">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-xs-2"></div>
                                                <div class="col-xs-10">
                                                    <div class="pull-right">
                                                        <%--<asp:Label ID="Label1" CssClass="text-danger" runat="server" />--%>
                                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary btn-space-right" Enabled="false" runat="server" />
                                                        <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />
                                                    </div>
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

