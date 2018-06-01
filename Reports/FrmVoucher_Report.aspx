<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmVoucher_Report.aspx.cs" Inherits="Reports_FrmVoucher_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        function DateValidate() {


            if ($('#<%=txtVoucherNo.ClientID%>').val() == '') {

                $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Voucher Number');
                $('#<%=txtVoucherNo.ClientID%>').focus();
                return false;
            }


            if ($('#<%=txtVoucherDate.ClientID%>').val() == '' || $('#<%=txtVoucherDate.ClientID%>').val() != '') {

                var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                var DateId = document.getElementById("<%= txtVoucherDate.ClientID %>").value;
                var valid = DateRegex.test(DateId);
                if (!valid) {
                    $('#<%=lblErrorMsg.ClientID%>').html('Please Enter Valid Voucher  Date (dd/MM/yyyy)');
                    $('#<%=txtVoucherDate.ClientID%>').focus();
                    return false;
                }
            }


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" style="height: 75%">
        <h3 class="text-center head">Voucher 
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="panel panel-default" style="margin-bottom: 5px">

                    <div class="panel-body">
                        <div class="col-xs-12">

                            <table class="pdf_show">

                                <tr>
                                    <td class="pdf_show_col1 pr">Voucher Number <span style="color: red;">*</span></td>
                                    <td class="pdf_show_col2">
                                        <asp:TextBox ID="txtVoucherNo" MaxLength="10" CssClass="numberonly" placeholder="Enter Invoice Number" runat="server" />
                                    </td>
                                    <td class="pdf_show_col3 r pr">Voucher Date <span style="color: red;">*</span></td>
                                    <td class="pdf_show_col4 ">
                                        <asp:TextBox ID="txtVoucherDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                    </td>
                                    <td class="pdf_show_col7 c">
                                        <asp:Button Text="Show" runat="server" ID="btnShow" CssClass="btn btn-info" OnClientClick="return DateValidate();" OnClick="btnShow_Click1" /></td>
                                </tr>

                            </table>
                            <div style="text-align: center">
                                <asp:Label Style="font-weight: bold; color: #f05050; font-size: medium" ID="lblErrorMsg" runat="server" />
                            </div>
                            <br />
                            <div style="text-align: center">
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

