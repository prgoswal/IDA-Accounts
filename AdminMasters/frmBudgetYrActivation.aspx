<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBudgetYrActivation.aspx.cs" Inherits="AdminMasters_frmBudgetYrActivation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <h3 class="text-center head">Budget Year Activation
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="col-sm-offset-3 col-sm-6">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Budget Order No.</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control " placeholder="Order No." MaxLength="25"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Budget Order Date</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtOrderDate" CssClass="form-control datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Budget Financial Year</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlFinancialYr" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="2018-2019" Value="2018-2019" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%-- <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Active Year</label>
                                            <div class="col-sm-8">
                                                <asp:CheckBox ID="chkActiveInd" runat="server" />
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="text-right">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Active" />
                                            <%--<asp:Button ID="btnclear" CssClass="btn btn-danger" OnClick="btnclear_Click" runat="server" Text="Clear" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

