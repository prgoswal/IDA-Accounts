<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdGroupMaster.aspx.cs" Inherits="Updation_frmUpdGroupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Item Group Updation Master

        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-12 alphaonly">Group Type</label>
                                                    <div class="col-sm-12">
                                                        <asp:DropDownList ID="ddlGrpType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlGrpType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Main Group</asp:ListItem>
                                                            <asp:ListItem Value="2">Sub Group</asp:ListItem>
                                                            <asp:ListItem Value="3">Minor Group</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-12 alphaonly">Item Group Name</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtItmGrpName" runat="server" CssClass="form-control" Enabled="false" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="GridHeader" class="col-md-12" runat="server">
                                                <asp:Label ID="lblgrtypeName" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-md-12" style="overflow: auto; height: 200px;">
                                                <asp:GridView ID="grdGroupType" CssClass="table table-UpdGroptype table-bordered" ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" PageSize="10" runat="server" OnRowEditing="grdGroupType_RowEditing">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGroupId" Text='<%#Eval("ItemGroupID") %>' CssClass="hidden" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGroupName" Text='<%#Eval("GroupName") %>' runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="3px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-primary btn-xs" Text="Edit" CommandName="Edit" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="text-right">
                                    <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" Visible="true"/>
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

