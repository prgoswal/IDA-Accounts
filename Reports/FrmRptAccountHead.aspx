<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmRptAccountHead.aspx.cs" Inherits="Reports_FrmRptAccountHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <div class="content-wrapper" style="height: 75%">
        <h3 class="text-center head">Budget Report
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="panel panel-default" style="margin-bottom: 5px">
                    <div class="panel-body">
                        <div class="col-xs-12">
                            <table class="pdf_show">
                                <tr>
                                    <td class="pdf_show_col1 c pr"><b>Report Name:-Account Head Report</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button Text="Show" ID="btnShow" Width="130px" runat="server" class="btn btn-sxs btn-primary" OnClick="btnShow_Click" ></asp:Button>
                                    </td>
                                   <%-- <td class="pdf_show_col2 r">

                                        <asp:DropDownList ID="ddlReportName" runat="server" >
                                            <asp:ListItem Text="----Select----" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Section Wise Summary" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Sub-Section Wise Summary" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Budget Head Wise Summary" Value="4"></asp:ListItem>
                                        </asp:DropDownList> 
                                        </td>--%>

                                        <%--<td class="pdf_show_col7 c">
                                            </td>--%>
                                </tr>
                            </table>
                            <div style="text-align: center">
                                <asp:Label Style="font-weight: bold; color: red; font-size: medium" ID="lblErrorMsg"  runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

