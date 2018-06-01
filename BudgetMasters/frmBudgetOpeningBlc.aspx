<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBudgetOpeningBlc.aspx.cs" Inherits="BudgetMasters_frmBudgetOpeningBlc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control.form-control-sm {
            padding: 4px 11px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="content-wrapper">
                <h3 class="text-center head">Budget Opening Balance<asp:Label id="lblYear" runat="server"></asp:Label>
                </h3>

                <div class="container_fluid">
                    <div class="row">
                        <div style="max-width: 475px; margin: 0 auto">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-8 alphaonly">Opening Balance For</label>
                                                    <label class="col-sm-4 alphaonly">
                                                        Amount
                                                        <asp:Label ID="lblAmtType" runat="server"></asp:Label>
                                                    </label>
                                                </div>

                                            </div>

                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <asp:Label class="col-sm-8 alphaonly" ID="lblOBalBg1718" runat="server"></asp:Label>
                                                    <%-- <label class="col-sm-4 alphaonly">Opening Balance for 2017-2018(Budgeted)</label>--%>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtCurrentBudgetAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <asp:Label class="col-sm-8 alphaonly" ID="lblOBalBg1617" runat="server"></asp:Label>
                                                    <%-- <label class="col-sm-4 alphaonly">Opening Balance for 2016-2017(Budgeted)</label>--%>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtPreviousYearBudgetAmt" CssClass="inpt Money form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <asp:Label class="col-sm-8 alphaonly" ID="lblOBalAc1617" runat="server"></asp:Label>
                                                    <%-- <label class="col-sm-4 alphaonly">Opening Balance for 2016-2017(Actual)</label>--%>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtPreviousYearActualAmt" CssClass="inpt Money form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12" style="display:none;">
                                                <div class="form-group row">
                                                    <asp:Label class="col-sm-8 alphaonly" ID="lblOBalAc1516" runat="server"></asp:Label>
                                                    <%-- <label class="col-sm-4 alphaonly">Opening Balance for 2015-2016(Actual)</label>--%>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtPreviousYear2ActualAmt" CssClass="inpt Money form-control form-control-sm" runat="server"></asp:TextBox>
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
                                                    <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" runat="server" Text="Save" />
                                                    <%-- <asp:Button ID="btnclear" CssClass="btn btn-danger" OnClick="btnclear_Click" runat="server" Text="Clear" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfOpeningBalID" runat="server" Value="0" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

