<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmClientGstin.aspx.cs" Inherits="frmGstinlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="col-sm-offset-2 col-sm-8 ">
            <div class="panel panel-default" style="margin-top:20px">
                <style>
                    .gstlist {
                        width: 100%;
                    }
                    th.inverse, td.inverse{
                        background:#1c75bf;
                        color:#fff
                    }
                    .gstlist tr td {
                        font-size:12px;
                    }
                    .gstlist_col1 {
                        width: 10%;
                    }

                    .gstlist_col2 {
                        width: 25%;
                    }

                    .gstlist_col3 {
                        width: 50%;
                    }

                    .gstlist_col4 {
                        width: 15%;
                    }
                </style>
                
                <div class="table-responsive">
                    <asp:GridView ID="gridgstin" runat="server" CssClass="table table-bordered text-black gstlist" AutoGenerateColumns="false" OnRowCommand="gridgstin_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="inverse hidden" ItemStyle-CssClass="hidden">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrId" Text='<%#Eval("BranchID") %>' runat="server" CssClass="hidden"></asp:Label>
                                    <asp:Label ID="lblASPClientCode" Text='<%#Eval("ASPClientCode") %>' runat="server" CssClass="hidden"></asp:Label>
                                    <asp:Label ID="lblASPClientCodeODP" Text='<%#Eval("ASPClientCodeODP") %>' runat="server" CssClass="hidden"></asp:Label>
                                    <asp:Label ID="lblASPCACode" Text='<%#Eval("ASPCACode") %>' runat="server" CssClass="hidden"></asp:Label>
                                    <asp:Label ID="lblASPCACodeODP" Text='<%#Eval("ASPCACodeODP") %>' runat="server" CssClass="hidden"></asp:Label>                               
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="inverse" ItemStyle-CssClass="gstlist_col1 bac text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSno" Text='<%#Eval("SrNo") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTIN" HeaderStyle-CssClass="inverse" ItemStyle-CssClass="gstlist_col2 text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblGstin" Text='<%#Eval("GSTIN") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ADDRESS" HeaderStyle-CssClass="inverse" ItemStyle-CssClass="gstlist_col3">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" Text='<%#Eval("Addr") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderStyle-CssClass="inverse" ItemStyle-CssClass="gstlist_col4 text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" Text="Select" CssClass="btn btn-sxs btn-primary" CommandName="SelectRow" CommandArgument='<%#Container.DataItemIndex %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

