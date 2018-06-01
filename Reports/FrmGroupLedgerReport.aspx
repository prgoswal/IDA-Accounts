<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmGroupLedgerReport.aspx.cs" Inherits="FrmGroupLedgerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function DateValidate() {
            if ($('#<%=txtFromDate.ClientID%>').val() == '' || $('#<%=txtFromDate.ClientID%>').val() != '') {

                var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                var DateId = document.getElementById("<%= txtFromDate.ClientID %>").value;
                var valid = DateRegex.test(DateId);
                if (!valid) {
                    $('#<%=lblErrorMsg.ClientID%>').html('Please Enter From  Date (dd/MM/yyyy)');
                    $('#<%=txtFromDate.ClientID%>').focus();
                    return false;
                }
            }

            if ($('#<%=txtToDate.ClientID%>').val() == '' || $('#<%=txtToDate.ClientID%>').val() != '') {

                var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                var DateId = document.getElementById("<%= txtToDate.ClientID %>").value;
                var valid = DateRegex.test(DateId);
                if (!valid) {
                    $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Valid  Date (dd/MM/yyyy)');
                    $('#<%=txtToDate.ClientID%>').focus();
                    return false;
                }
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
    <div class="content-wrapper" style="height: 75%">
        <h3 class="text-center head">Subsidairy Ledger
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="panel panel-default" style="margin-bottom: 5px">
                    <div class="panel-body">
                        <div class="col-xs-12">
                            <table class="pdf_show">
                                <tr>
                                    <td class="pdf_show_col1 r pr">From Date</td>
                                    <td class="pdf_show_col2 r">
                                        <asp:TextBox ID="txtFromDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" /></td>
                                    <td class="pdf_show_col3 r pr">To Date</td>
                                    <td class="pdf_show_col4 r">
                                        <asp:TextBox ID="txtToDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                    </td>

                                    <td class="pdf_show_col5 r pr">Group Name</td>
                                    <td class="pdf_show_col6" style="width: 26.6%">
                                      <cc1:ComboBox ID="ddlGroupLedger" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlCashAccount_SelectedIndexChanged" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                    </td>

                                    <td class="pdf_show_col7 c">
                                        <asp:Button Text="Show" ID="btnShow" Width="130px" runat="server" OnClientClick="return DateValidate()" class="btn btn-sxs btn-primary" OnClick="btnShow_Click"></asp:Button></td>
                                </tr>
                            </table>
                            <div style="text-align: center">
                                <asp:Label Style="font-weight: bold; color: #f05050;; font-size: medium" ID="lblErrorMsg" runat="server" />
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>



    </div>
</asp:Content>

