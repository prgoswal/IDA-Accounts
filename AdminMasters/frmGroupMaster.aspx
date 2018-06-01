<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmGroupMaster.aspx.cs" Inherits="Vouchers_frmGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ScriptManager ID="scriptMan1" runat="server"></asp:ScriptManager>--%>

    <div class="content-wrapper">
        <h3 class="text-center head">Item Group Master
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="col-sm-offset-3 col-sm-6">
                    <%-- <div class="col-lg-12"> 
                        <div class="col-lg-6">--%>
                    <div class="panel panel-default">
                        <div class="panel-body">

                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Group Type</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlGroupType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Main Group</asp:ListItem>
                                                    <asp:ListItem Value="2">Sub Group</asp:ListItem>
                                                    <asp:ListItem Value="3">Minor Group</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div id="divddlMainGrp" class="form-group row" runat="server" visible="false">
                                            <label class="col-sm-4 alphaonly">Select Main Group</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMainGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMainGroup_SelectedIndexChanged"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div id="divddlsubgrp" class="form-group row" runat="server" visible="false">
                                            <label class="col-sm-4 alphaonly">Select Sub Group</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSubGroup" CssClass="form-control" Enabled="false" runat="server"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Item Group Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control " placeholder="Group Name" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="text-right">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblMsg" CssClass="col-sm-7 text-danger" runat="server"></asp:Label>
                                            <asp:Button ID="btnSave" CssClass="btn btn-primary fa r fa-floppy-o" runat="server" Text="Save" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnclear" CssClass="btn btn-danger fa r fa-close" runat="server" Text="Clear" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--<div class="col-lg-6">
                    <asp:GridView ID="grdItemGroup" CssClass="table table-ItemGroup" ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" PageSize="10" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupId" Text='<%#Eval("ItemGroupCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate> 
                                        <asp:Label ID="lblGroupName" Text='<%#Eval("GroupName") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns> 
                    </asp:GridView> --%>
            </div>
        </div>
    </div>
</asp:Content>

