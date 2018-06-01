<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmGSTR1Entry.aspx.cs" Inherits="GSTReturns_frmGSTR1Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $('.cashpayment_activeme').addClass('active');
        });
        function LoadAllScript() {

            LoadBasic();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center p5" style="padding: 5px; margin-bottom: 5px;">GSTR 1 Entry </h3>
        <div class="row">
            <style>
                .gstr1entry small {
                    display: block;
                    font-size: 11px;
                    font-weight: normal;
                    margin-top: -5px;
                }
            </style>
            <div class="panel panel-default gstr1entry">
                <div class="panel-body">

                    <table style="width: 990px; margin: 0 auto; margin-bottom: 20px">
                        <tr>
                            <td style="width: 12%; padding-right: 5px" class="text-right text-bold">GSTIN</td>
                            <td style="width: 15%"><b>
                                <asp:DropDownList runat="server" ID="ddlGstin">
                                </asp:DropDownList></b></td>

                            <td style="width: 10%">&nbsp;</td>
                            <td style="width: 10%; padding-right: 5px" class="text-right text-bold">Month & Year</td>
                            <td style="width: 11%">
                                <asp:DropDownList ID="ddlMonth" runat="server">
                                    <asp:ListItem Value="0" Text="--Select--" />
                                    <%-- <asp:ListItem Value="1" Text="January" />
                                                                        <asp:ListItem Value="2" Text="February" />
                                                                        <asp:ListItem Value="3" Text="March" />
                                                                        <asp:ListItem Value="4" Text="April" />
                                                                        <asp:ListItem Value="5" Text="May" />
                                                                        <asp:ListItem Value="6" Text="June" />--%>
                                    <asp:ListItem Value="7" Text="July" />
                                    <asp:ListItem Value="8" Text="August" />
                                    <%-- <asp:ListItem Value="9" Text="September" />
                                                                       <asp:ListItem Value="10" Text="October" />
                                                                        <asp:ListItem Value="11" Text="November" />
                                                                        <asp:ListItem Value="12" Text="December" />--%>
                                </asp:DropDownList>

                            </td>
                            <td style="width: 11%">
                                <asp:DropDownList ID="ddlYear" runat="server">
                                    <asp:ListItem Value="17" Text="2017" />
                                    <%-- <asp:ListItem Value="0" Text="--Select--" />
                                                                        <asp:ListItem Value="18" Text="2018" />
                                                                        <asp:ListItem Value="19" Text="2019" />
                                                                        <asp:ListItem Value="20" Text="2020" />--%>
                                </asp:DropDownList> 
                                </td>
                            <td>
                                <asp:Button ID="btnShow" CssClass="btn btn-primary btn-sxs" Text="Show" OnClick="btnShow_Click" runat="server" />
                            </td>
                            <td style="width: 29%">&nbsp;</td>
                        </tr>
                    </table>
                    <table style="width: 990px; margin: 0 auto; margin-bottom: 20px">
                        <tr>
                            <td style="width: 10%; padding-right: 5px" class="text-right text-bold">Trade Name <small>( Optional )</small></td>
                            <td style="width: 22%;">
                                <asp:TextBox ID="txtTrName" CssClass="Alphaonly text-uppercase" MaxLength="45" runat="server" Enabled="false" />
                            </td>
                            <td style="width: 16%; padding-right: 5px" class="text-right text-bold">Agreegate&nbsp;Turnover <small>( of Preceding Financial Year)</small></td>
                            <td style="width: 16%;">
                                <asp:TextBox ID="txtAgrTurnOver" CssClass="Money" MaxLength="11" runat="server" Enabled="false" />
                            </td>

                            <td style="width: 16%; padding-right: 5px" class="text-right text-bold">Agreegate&nbsp;Turnover <small>( April - June 2017)</small></td>
                            <td style="width: 16%;">
                                <asp:TextBox ID="txtAgetoLsQtr" CssClass="Money" MaxLength="11" runat="server" Enabled="false" />
                            </td>

                        </tr>
                    </table>
                    <div style="padding-right: 17px">
                        <table class="table-bordered">
                            <tr class="inf_head">
                                <th style="width: 49%">Delievery Challan Type</th>
                                <th style="width: 12%">Sr. No. From </th>
                                <th style="width: 12%">Sr. No. To</th>
                                <th style="width: 08%">Total No.</th>
                                <th style="width: 08%">Cancelled</th>
                                <th style="width: 08%">Net Issued</th>
                                <th style="width: 03%;">&nbsp;</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlDelieveryChallan" runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="Delivery Challan For Job Work" />
                                        <asp:ListItem Value="2" Text="Delivery Challan For Supply Or Approval" />
                                        <asp:ListItem Value="3" Text="Delivery Challan in Case Of Liquid Gas" />
                                        <asp:ListItem Value="4" Text="Delivery Challan in Case Of Other than by Way Of Supply(Excluding Aboves)" />
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtSrNofrm" CssClass=" alphanum" MaxLength="16" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="txtSrnoTo" CssClass=" alphanum" MaxLength="16" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="txtTotalNo" CssClass="numberonly" MaxLength="6" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="txtCancelled" CssClass="numberonly" MaxLength="6" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="txtnetIssue" CssClass="numberonly" MaxLength="7" runat="server" /></td>
                                <td>
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" OnClick="btnAdd_Click" CssClass="btn btn-sxs btn-primary" /></td>
                            </tr>
                        </table>
                    </div>
                    <div style="max-height: 150px; overflow-y: scroll">
                        <%--<table class="table-bordered">
                        <tr>
                            <td style="width:13%">12</td>
                            <td style="width:13%">12</td>
                            <td style="width:13%">12</td>
                            <td style="width:13%">12</td>
                            <td style="width:13%">12</td>
                            <td style="width:13%">12</td>
                            <td style="width:3%;"><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                        </tr>
                       <tr>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                       </tr>
                           <tr>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                       </tr>
                           <tr>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                       </tr>
                           <tr>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                       </tr>
                           <tr>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                       </tr>

                           <tr>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td>12</td>
                           <td><asp:Button Text="Del" runat="server" CssClass="btn btn-sxs btn-danger"/></td>
                       </tr>
                    </table>--%>
                        <asp:GridView ID="GrdGstr1" runat="server" AutoGenerateColumns="false" CssClass="table-bordered" OnRowCommand="GrdGstr1_RowCommand" ShowHeader="false">
                            <Columns>
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="GSTIN" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="MonthID" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="YearID" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="MonthYear" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="TradeName" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="AgreegateTurnover" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="AgreegateTurnoverLstQtr" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="DeliveryChallanTypeId" />
                                <asp:BoundField ItemStyle-Width="13%" DataField="DeliveryChallanType" />
                                <asp:BoundField ItemStyle-Width="13%" DataField="SrNoFrom" />
                                <asp:BoundField ItemStyle-Width="13%" DataField="SrNoTo" />
                                <asp:BoundField ItemStyle-Width="13%" DataField="Total" />
                                <asp:BoundField ItemStyle-Width="13%" DataField="Cancelled" />
                                <asp:BoundField ItemStyle-Width="13%" DataField="NetIssued" /> 
                               <%-- <asp:BoundField ItemStyle-CssClass="hidden" DataField="CompanyID" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="BranchID" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="YrCD" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="UserID" />
                                <asp:BoundField ItemStyle-CssClass="hidden" DataField="IPAddress" />--%>

                                <asp:TemplateField ItemStyle-CssClass="text-center" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" CssClass="btn btn-danger btn-sxs" CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
                <div class="panel-footer text-right">
                    <asp:Label ID="lblmsg" runat="server" CssClass="text-danger"></asp:Label>
                    <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" Enabled="false" OnClick="btnSave_Click" runat="server" />
                    <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger"  OnClick="btnClear_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

