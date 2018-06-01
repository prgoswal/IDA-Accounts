<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmGstinDetails.aspx.cs" Inherits="UserUtility_frmGstinDetails" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="row">
            <div class="panel panel-default" style="margin-bottom: 0" id="pnlGSTINGrid" runat="server">
                <div class="panel-body">
                    <asp:UpdatePanel ID="upGRDGSTIN" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdGSTINDetail" AutoGenerateColumns="false" runat="server">
                                <Columns>

                                    <asp:TemplateField HeaderText="S No" ItemStyle-CssClass="c1">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AccGSTINID" ItemStyle-CssClass="c1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccGSTINID" Text='<%#Eval("AccGSTINID") %>' Visible="false" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CompanyStateID" ItemStyle-CssClass="c1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyStateID" Text='<%#Eval("PartyStateID") %>' Visible="false" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PANNo" ItemStyle-CssClass="c1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyPanNo" Text='<%#Eval("PartyPanNo") %>' Visible="false" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AccCode" ItemStyle-CssClass="c1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCode" Text='<%#Eval("AccCode") %>' Visible="false" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Party Name" ItemStyle-CssClass="c1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccName" Text='<%#Eval("AccName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GSTIN No.">
                                        <ItemTemplate>

                                            <%--    <cc1:ComboBox ID="ddlGSTIN" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGSTIN_SelectedIndexChanged" Style="text-transform: uppercase"></cc1:ComboBox>
                                            <asp:TextBox ID="txtGSTIN" AutoPostBack="true" OnTextChanged="txtGSTIN_TextChanged" CssClass="text-uppercase" MaxLength="16" Text='<%#Eval("GSTIN") %>' Visible="false" placeholder="GSTIN No." runat="server" />--%>

                                            <%--<asp:Label ID="lblGSTIN" Text='<%#Eval("GSTIN") %>' runat="server"></asp:Label>--%>


                                            <asp:TextBox ID="txtGSTIN" AutoPostBack="true" OnTextChanged="txtGSTIN_TextChanged" runat="server" CssClass="text-uppercase GSTIN" MaxLength="16" Text='<%#Eval("GSTIN") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration Address" ItemStyle-CssClass="c3">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblRegistrationAddress" Text='<%#Eval("RegistrationAddress") %>' runat="server"></asp:Label>--%>


                                            <asp:TextBox ID="txtRegistrationAddress" runat="server" CssClass="text-uppercase" MaxLength="16" Text='<%#Eval("RegistrationAddress") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration City" ItemStyle-CssClass="c8">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblCity" Text='<%#Eval("City") %>' runat="server"></asp:Label>--%>


                                            <asp:TextBox ID="txtCity" runat="server" CssClass="text-uppercase" MaxLength="16" Text='<%#Eval("City") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" ItemStyle-CssClass="c4">
                                        <ItemTemplate>

                                            <asp:DropDownList ID="ddlState" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            <%--<asp:Label ID="lblStateID" Text='<%#Eval("StateID") %>' runat="server"></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Valid/Invalid" ItemStyle-CssClass="c6">
                                        <ItemTemplate>

                                            <asp:Button ID="btnGSTINStatus" Text="Valid" CssClass="btn btn-success" CommandName="Action" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#1c75bf" ForeColor="White" />
                            </asp:GridView>
                        </ContentTemplate>



                        <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAction" />
                            </Triggers>--%>
                    </asp:UpdatePanel>
                    <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>

