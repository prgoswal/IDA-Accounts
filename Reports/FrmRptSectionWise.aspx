<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmRptSectionWise.aspx.cs" Inherits="Reports_FrmRptSectionWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>


        function LoadAllScript() {
            ChoosenDDL();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        Sys.Application.add_load(LoadAllScript)
    </script>
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
                                    <td class="pdf_show_col1 r pr">Year:</td>
                                    <td class="pdf_show_col2 r">
                                        <asp:DropDownList ID="ddlYear" runat="server">
                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="2017-2018" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2018-2019" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="pdf_show_col1 r pr">Report Name:</td>
                                    <td class="pdf_show_col2 r">

                                        <asp:DropDownList ID="ddlReportName" runat="server" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="----Select----" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Budget Main Summary" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Budget Summary-2" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Budget Summary-2 Detail" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Expense Summary" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="Income Summary" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="Income Summary (With Main Summary)" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="Group/Cost Centre Wise Summary" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Gruop/Cost Centre/Budget Head Wise Detail" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Budget Head /Cost Centre Wise Summary" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Budget Head /Cost Centre Wise Summary2" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="Cost Centre Assigning Detail" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="Group Wise Summary-2" Value="15"></asp:ListItem>

                                        </asp:DropDownList>


                                        <%--<asp:ListItem Text="Group Wise Summary" Value="2"></asp:ListItem>--%>

                                        <style>
                                            .chzn-container {
                                                width: 220px;
                                            }

                                            .chzn-single {
                                                text-align: center !important;
                                            }

                                            .chzn-drop {
                                                text-align: left !important;
                                            }
                                        </style>
                                        <div runat="server" id="divDropDown" class="tblaln" visible="false" style="width: 220px;">
                                            <td class="pdf_show_col1 r pr">Budget Head Name: </td>
                                            <td class="pdf_show_col2 r">
                                                <%--<asp:DropDownList ID="ddlBudgetHead" runat="server" Enabled="false"></asp:DropDownList>--%>


                                                <asp:DropDownList ID="ddlBudgetHead" CssClass="chzn-select" Enabled="false" runat="server"></asp:DropDownList>
                                            </td>
                                        </div>

                                        <div runat="server" id="divINExDropDown" visible="false">
                                            <td class="pdf_show_col1 r pr">Income/Expense: </td>
                                            <td class="pdf_show_col2 r">
                                                <asp:DropDownList ID="ddlInEx" runat="server" Enabled="false">
                                                    <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Income" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Expense" Value="2"></asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </div>



                                        <div runat="server" id="divSectionCD" visible="false">
                                            <td class="pdf_show_col1 r pr">Section Name </td>
                                            <td class="pdf_show_col2 r">
                                                <asp:DropDownList ID="ddlSectionName" runat="server" Enabled="false">
                                                </asp:DropDownList>

                                            </td>
                                        </div>

                                        <td class="pdf_show_col7 c">
                                            <asp:Button Text="Show" ID="btnShow" Width="130px" runat="server" class="btn btn-sxs btn-primary" OnClick="btnShow_Click"></asp:Button></td>
                                </tr>
                            </table>
                            <div style="text-align: center">
                                <asp:Label Style="font-weight: bold; color: red; font-size: medium" ID="lblErrorMsg" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </div>
</asp:Content>

