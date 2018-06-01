<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmSalesInvoiceReport.aspx.cs" Inherits="FrmSalesInvoiceReport" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
            function DateValidate() {

                if ($('#<%=rbSearch.ClientID %> input[type=radio]:checked').val() == '1') {

                    if ($('#<%=txtFromDate.ClientID%>').val() == '' || $('#<%=txtFromDate.ClientID%>').val() != '') {

                        var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                        var DateId = document.getElementById("<%= txtFromDate.ClientID %>").value;
                        var valid = DateRegex.test(DateId);
                        if (!valid) {
                            $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Valid From  Date (dd/MM/yyyy)');
                            $('#<%=txtFromDate.ClientID%>').focus();
                            return false;
                        }
                    }

                    if ($('#<%=txtToDate.ClientID%>').val() == '' || $('#<%=txtToDate.ClientID%>').val() != '') {

                        var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                        var DateId = document.getElementById("<%= txtToDate.ClientID %>").value;
                        var valid = DateRegex.test(DateId);
                        if (!valid) {
                            $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Valid To  Date (dd/MM/yyyy)');
                            $('#<%=txtToDate.ClientID%>').focus();
                            return false;
                        }
                    }
                }
                else {
                    if ($('#<%=txtInvoiceNo.ClientID%>').val() == '') {

                        $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Invoice Number');
                    $('#<%=txtInvoiceNo.ClientID%>').focus();
                    return false;
                }


                if ($('#<%=txtInvoiceDate.ClientID%>').val() == '' || $('#<%=txtToDate.ClientID%>').val() != '') {

                        var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                        var DateId = document.getElementById("<%= txtInvoiceDate.ClientID %>").value;
                        var valid = DateRegex.test(DateId);
                        if (!valid) {
                            $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Valid Invoice  Date (dd/MM/yyyy)');
                            $('#<%=txtInvoiceDate.ClientID%>').focus();
                            return false;
                        }
                    }
                }

            }
            $(document).ready(function () {
                $(".check_class input").click(function () {
                    $(".check_class input").not(this).prop('checked', false);
                });
            })
        }
    </script>
    <script>

        
    </script>
    <script>
        
    </script>
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
            <div class="content-wrapper" style="height: 75%">
                <h3 class="text-center head">Sales Invoice 
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default" style="margin-bottom: 5px">
                            <div class="panel-body pt5">
                                <style>
                                    .pdf_show tr td, #ContentPlaceHolder1_rbSearch tr td {
                                        border-color: transparent;
                                    }

                                        #ContentPlaceHolder1_rbSearch tr td:last-child {
                                            text-align: left;
                                        }

                                    .check_class label {
                                        display: none;
                                    }

                                    .pt5 {
                                        padding-top: 5px;
                                    }

                                    .gn13 {
                                        width: 100%;
                                        max-width: 620px;
                                        margin: 0 auto;
                                    }

                                    .gn_12 {
                                        float: left;
                                        width: 85%;
                                    }

                                    .p_d25 {
                                        padding: 2px 5px;
                                        min-width: 40px;
                                    }

                                    .fa-asp {
                                        font-family: FontAwesome,'Roboto',Arial,sans-serif;
                                    }
                                </style>
                                <div class="gn13">
                                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbSearch" OnSelectedIndexChanged="rbSearch_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Selected="True" Text="Search By Invoice Number" />
                                        <asp:ListItem Value="1" Text="Search By Between Date" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="gn13">

                                    <table class="pdf_show gn_12">
                                        <asp:Panel runat="server" ID="pnlInvoiceNo">
                                            <tr>
                                                <td class="pdf_show_col1 pr">Invoice Number&nbsp;<span style="color: red;">*</span></td>
                                                <td class="pdf_show_col2">
                                                    <asp:TextBox ID="txtInvoiceNo" MaxLength="10" CssClass="numberonly" placeholder="Enter Invoice Number" runat="server" />
                                                </td>
                                                <td class="pdf_show_col3 r pr">Invoice Date <span style="color: red;">*</span></td>
                                                <td class="pdf_show_col4 ">
                                                    <asp:TextBox ID="txtInvoiceDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="pnlForBetweenDate" Visible="false">
                                            <tr>
                                                <td class="pdf_show_col1 pr">From Date <span style="color: red;">*</span></td>
                                                <td class="pdf_show_col2 ">
                                                    <asp:TextBox ID="txtFromDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" /></td>

                                                <td class="pdf_show_col3 r pr">To Date <span style="color: red;">*</span></td>
                                                <td class="pdf_show_col4 ">
                                                    <asp:TextBox ID="txtToDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                </td>
                                            </tr>
                                        </asp:Panel>

                                    </table>
                                    <div style="float: right">
                                        <asp:Button Text="&#xf002;" CssClass="btn btn-primary p_d25 fa-asp" ID="btnSearch" OnClick="btnSearch_Click" runat="server" />
                                        <asp:Button Text="Clear" CssClass="btn btn-primary p_d25 fa-asp" OnClick="btnClearSearch_Click" runat="server" ID="btnClearSearch" />
                                    </div>

                                </div>
                            </div>

                            <div class="panel-body">


                                <div style="max-width: 900px; margin: 0 auto" id="pnlGrid" visible="false" runat="server">
                                    <asp:Label Text="" ID="lblTotalAmt" runat="server" Style="float: right; color: green; margin-right: 17px;" />
                                    <table class="table-bordered" style="width: calc(100% - 17px)">
                                        <tr class="inf_head">
                                            <th style="width: 025px;"></th>
                                            <th style="width: 112px;">Invoice Type</th>
                                            <th style="width: 112px;">Invoice Series</th>
                                            <th style="width: 112px;">Invoice No.</th>
                                            <th style="width: 112px;">Invoice Date</th>
                                            <th style="width: 312px;">Party Name</th>
                                            <th style="width: 112px;">Amount</th>
                                        </tr>
                                    </table>

                                    <style>
                                        .tdpadding tr td {
                                            padding: 1px 5px;
                                            word-break: break-all;
                                        }
                                    </style>
                                    <div style="max-width: 900px; margin-left: auto; margin-right: auto; margin-top: 0px; overflow-y: scroll; max-height: 280px;">

                                        <asp:GridView runat="server" CssClass="table-bordered tdpadding" DataKeyNames="VoucharNo" AutoGenerateColumns="False" ID="gvItem" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeader="false">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>

                                                <asp:TemplateField ItemStyle-Width="025px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckBox" runat="server" Text='<%# Eval("VoucharNo") %>' CssClass="check_class" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="112px" DataField="InvoiceType" />
                                                <asp:BoundField ItemStyle-Width="112px" DataField="InvoiceSeries" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField ItemStyle-Width="112px" DataField="InvoiceNo" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField ItemStyle-Width="112px" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="text-center" />
                                                <asp:BoundField ItemStyle-Width="312px" DataField="PartyName" />
                                                <asp:BoundField ItemStyle-Width="112px" DataField="NetAmount" ItemStyle-CssClass="text-right" />
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
                                <div class="col-xs-6 col-xs-offset-3">


                                    <div style="text-align: center">
                                        <asp:Label Style="font-weight: bold; color: #f05050; font-size: medium" ID="lblErrorMsg" runat="server" />
                                    </div>
                                    <br />
                                    <div style="text-align: center">

                                        <asp:Button Text="Show" Visible="false" runat="server" ID="btnShow" CssClass="btn btn-primary" OnClientClick="return DateValidate();" OnClick="btnShow_Click1" />
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



