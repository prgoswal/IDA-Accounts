<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmItemMaster_Report.aspx.cs" Inherits="Reports_FrmItemLedger_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" style="height: 75%">
        <h3 class="text-center head">Item Master
        </h3>
        <div class="container_fluid">

            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-horizontal">

                            <div class="row">
                                <div class="col-sm-6" style="display: none">

                                    <div class="form-group row">
                                        <label class="col-sm-3">Minor Group</label>
                                        <div class="col-sm-9">
                                            <cc1:ComboBox ID="ddlMinorGroup" AutoPostBack="true" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" OnSelectedIndexChanged="ddlMinorGroup_SelectedIndexChanged" Style="text-transform: uppercase"></cc1:ComboBox>
                                            <asp:Label ID="lblItemGroup" class="col-sm-12" ForeColor="#27c24c" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3">Item Name</label>
                                        <div class="col-sm-9">
                                            <cc1:ComboBox ID="ddlItemName" AutoPostBack="true" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <asp:Button Text="Show" ID="btnShow" Width="130px" runat="server" class="btn btn-sxs btn-primary" OnClick="btnShow_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <%--  <div class="row" style="text-align: center">
                                <asp:Button Text="Show" ID="btnShow" Width="130px" runat="server" class="btn btn-sxs btn-primary" OnClick="btnShow_Click"></asp:Button>
                            </div>--%>
                        <div class="row">

                            <div style="text-align: center">
                                <asp:Label Style="font-weight: bold; color: red; font-size: medium" ID="lblErrorMsg" runat="server" />
                                <asp:HiddenField ID="hfMainGrCode" runat="server" />
                                <asp:HiddenField ID="hfSubGrCode" runat="server" />
                                <asp:HiddenField ID="hfItemGroupID" runat="server" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>

    </div>
</asp:Content>

