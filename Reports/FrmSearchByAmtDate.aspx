<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmSearchByAmtDate.aspx.cs" Inherits="Reports_FrmSearchByAmtDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
        }
    </script>
    <style>
        .p_d25 {
            padding: 2px 5px;
            min-width: 40px;
        }

        .fa-asp {
            font-family: FontAwesome,'Roboto',Arial,sans-serif;
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
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center">Search Recored Date & Amount</h3>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <table style="max-width: 650px; margin: 0 auto; margin-bottom: 20px">
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="3" class="text-bold c">Transaction Date</td>
                                <td>&nbsp;</td>
                                <td colspan="3" class="text-bold c">Transaction Amount</td>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="text-bold c">From</td>
                                <td>&nbsp;</td>
                                <td class="text-bold c">To</td>
                                <td>&nbsp;</td>
                                <td class="text-bold c">From</td>
                                <td>&nbsp;</td>
                                <td class="text-bold c">To</td>
                                <td>&nbsp;</td>
                                <td class="text-bold c"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 1%">&nbsp;</td>
                                <td style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" />
                                </td>
                                <td style="width: 1%">&nbsp;</td>
                                <td style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtToDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" />
                                </td>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtMinAmount" CssClass="inpt Money" MaxLength="9" AutoPostBack="true" OnTextChanged="txtMinAmount_TextChanged" />
                                </td>
                                <td style="width: 1%">&nbsp;</td>
                                <td style="width: 15%">
                                    <asp:TextBox runat="server" ID="txtmaxAmount" CssClass="inpt Money" MaxLength="9" />
                                </td>
                                <td style="width: 1%">&nbsp;</td>
                                <td style="width: 1%; text-align: right; padding-right: 8px">
                                    <asp:Button Text="&#xf002;" ID="btnSearch" runat="server" CssClass="btn btn-primary p_d25 fa-asp" OnClick="btnSearch_Click" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Button Text="Clear" ID="btnClearSearch" OnClick="btnClearSearch_Click" runat="server" CssClass="btn btn-primary btn-sxs" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td colspan="3">
                                    <div class="alert alert-danger p0 mt-sm text-center" id="lblDiv" visible="false" runat="server" style="background: #eb6565">
                                        <asp:Label Text="" runat="server" ID="lblErrorMsg" />
                                    </div>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                        <div style="max-width: 900px; margin: 0 auto" id="pnlGrid" visible="false" runat="server">
                            <asp:Label Text="" ID="lblTotalAmt" runat="server" Style="float: right; color: green; margin-right: 17px;" />
                            <table class="table-bordered" style="width: calc(100% - 17px)">
                                <tr class="inf_head">
                                    <th style="width: 100px;">Date</th>
                                    <th style="width: 200px;">Party Name</th>
                                    <th style="width: 130px;">Doc. Type</th>
                                    <th style="width: 140px;">Doc. No.</th>
                                    <th style="width: 140px;">Amount</th>
                                    <th style="width: 090px;">Dr/Cr</th>
                                </tr>
                            </table>

                            <style>
                                .tdpadding tr td {
                                    padding: 1px 5px;
                                    word-break: break-all;
                                }
                            </style>
                            <div style="max-height: 358px; overflow-y: scroll">
                                <asp:GridView runat="server" CssClass="table-bordered tdpadding" DataKeyNames="" ShowHeader="true" AutoGenerateColumns="False" ID="gvItem" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="100px" HeaderStyle-CssClass="hidden" HeaderText="Date" DataField="DocDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField ItemStyle-Width="200px" HeaderStyle-CssClass="hidden" HeaderText="Party Name" DataField="AccName" />
                                        <asp:BoundField ItemStyle-Width="130px" HeaderStyle-CssClass="hidden" HeaderText="Doc. Type" DataField="DocType" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField ItemStyle-Width="140px" HeaderStyle-CssClass="hidden" HeaderText="Doc. No." DataField="DocNo" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField ItemStyle-Width="140px" HeaderStyle-CssClass="hidden" HeaderText="Amount" DataField="Amount" ItemStyle-CssClass="text-right" />
                                        <asp:BoundField ItemStyle-Width="090px" HeaderStyle-CssClass="hidden" HeaderText="Dr/Cr" DataField="DrCr" ItemStyle-CssClass="text-center" />
                                        <%--<asp:BoundField DataField="DocTypeID" Visible="false" />
                            <asp:BoundField DataField="AccCode" Visible="false" />--%>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

