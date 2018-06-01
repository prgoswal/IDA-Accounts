<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmVoucherPrinting.aspx.cs" Inherits="Reports_frmVoucherPrinting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
        }
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For Voucher Type -------------------//

            if ($('#<%=ddlVoucherType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select Voucher Type.');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlVoucherType.ClientID%>').focus();
                return false;
            }

            //------------------For Voucher Number -------------------//

            if ($('#<%=txtVoucherNo.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Voucher No.');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=txtVoucherNo.ClientID%>').focus();
                return false;
            }

            //------------------For Voucher Date -------------------//

            if ($('#<%=txtVoucherDate.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Voucher Date.');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=txtVoucherDate.ClientID%>').focus();
                return false;
            }

            <%--//------------------For Voucher Number -------------------//

            if ($('#<%=rbVoucherNoWise.ClientID%>').val() == true) {
                if ($('#<%=txtVoucherNo.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').html('Enter Voucher No.');
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                    $('#<%=txtVoucherNo.ClientID%>').focus();
                    return false;
                }
            }
            else {

                //------------------For From Date -------------------//

                if ($('#<%=txtFromDate.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').html('Enter From Date.');
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                    $('#<%=txtFromDate.ClientID%>').focus();
                    return false;
                }

                //------------------For To Date -------------------//

                if ($('#<%=txtToDate.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').html('Enter To Date.');
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                    $('#<%=txtToDate.ClientID%>').focus();
                    return false;
                }
            }--%>
        }
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
                <h3 class="text-center head">Voucher Printing 
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default" style="margin-bottom: 5px">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <table class="pdf_show">
                                            <%--<tr>
                                                <td colspan="100%" style="text-align: center">
                                                    <asp:RadioButton ID="rbVoucherNoWise" Text="Voucher No. Wise" Checked="true" GroupName="VD" AutoPostBack="true" runat="server" />
                                                    <asp:RadioButton ID="rbDateWise" Text="Date Wise" GroupName="VD" AutoPostBack="true" runat="server" Style="margin-left: 5px;" />
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="pdf_show_col1 r pr">Voucher Type<span class="text-danger">*</span></td>
                                                <td class="pdf_show_col2 r">
                                                    <asp:DropDownList ID="ddlVoucherType" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="tdlblVoucherNo" class="pdf_show_col1 r pr" runat="server">Voucher No.<span class="text-danger">*</span></td>
                                                <td id="tdddlVoucherNo" class="pdf_show_col2 r" runat="server">
                                                    <asp:TextBox ID="txtVoucherNo" MaxLength="9" CssClass="numberonly" placeholder="Voucher No." runat="server" />
                                                </td>
                                                <td id="td1" class="pdf_show_col1 r pr" runat="server">Voucher Date<span class="text-danger">*</span></td>
                                                <td id="td2" class="pdf_show_col2 r" runat="server">
                                                    <asp:TextBox ID="txtVoucherDate" MaxLength="10" CssClass="datepicker" placeholder="Voucher Date" runat="server" />
                                                </td>
                                                <td id="tdlblFromDate" class="pdf_show_col1 r pr" visible="false" runat="server">From Date<span class="text-danger">*</span></td>
                                                <td id="tdtxtFromDate" class="pdf_show_col2 r" visible="false" runat="server">
                                                    <asp:TextBox ID="txtFromDate" CssClass="datepicker" MaxLength="10" placeholder="Voucher From Date" runat="server" />
                                                </td>
                                                <td id="tdlblToDate" class="pdf_show_col3 r pr" visible="false" runat="server">To Date<span class="text-danger">*</span></td>
                                                <td id="tdtxtToDate" class="pdf_show_col4 r" visible="false" runat="server">
                                                    <asp:TextBox ID="txtToDate" CssClass="datepicker" MaxLength="10" placeholder="Voucher To Date" runat="server" />
                                                </td>
                                                <td class="pdf_show_col7 c">
                                                    <asp:Button Text="Show" ID="btnShow" Width="130px" class="btn btn-sxs btn-primary" OnClick="btnShow_Click" OnClientClick="return Validation()" runat="server"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-left">
                                            Mandatory Fields <span class="text-danger">*</span>
                                        </div>
                                        <div class="pull-right">
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
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>
