<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmChecqueSeries.aspx.cs" Inherits="AdminMasters_frmChecqueSeries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .checkno_form {
            text-align: left;
            padding-left: 40px;
            padding-right: 0;
            width: 20%;
        }

        .checkno_to {
            margin-left: -19px;
        }

        .checkno_to_text {
            padding-left: 0;
            width: 23%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Cheque Series
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div style="max-width: 750px; margin: 0 auto">
                    <div class="panel panel-default mb0">
                        <div class="panel-body"><%--style="min-height: 150px"--%>
                            <div class="form-horizontal form-horizontal-label-left">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Bank&nbsp;Name</label>
                                            <div class="col-sm-9 gst-ac-error-holder">
                                                <asp:DropDownList ID="ddlbank" runat="server" Style="width: 95%; margin-left: 25px;" AutoPostBack="true" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged"  CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-3  control-label checkno_form">Cheque No.&nbsp;From</label>
                                            <div class="col-sm-3 gst-ac-error-holder">
                                                <asp:TextBox ID="txtChequefrom" placeholder="" MaxLength="8" CssClass="form-control numberonly" runat="server" />
                                            </div>
                                            <label class="col-sm-2  control-label  checkno_to">Cheque No.&nbsp;To</label>
                                            <div class="col-sm-3 gst-ac-error-holder checkno_to_text">
                                                <asp:TextBox ID="txtChequeto" placeholder="" MaxLength="8" CssClass="form-control numberonly" runat="server" />
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="btnAdd" Style="margin-left: -22px;" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="text-right">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnAdd" Style="margin-top: -90px; margin-right: 63px;" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <style>
                            .first_tr_hide tbody tr:first-child {
                                display: none;
                            }

                            .hide_my_pdosi + tr {
                                display: none;
                            }

                            .first_tr_hide tr td:first-child {
                                display: none;
                            }
                        </style>
                        <asp:GridView ID="grdchequeseries" CssClass="table-sm table-sm-4 table-bordered usertb table-pd-lr first_tr_hide" ShowHeader="true" AutoGenerateColumns="false" 
                            HeaderStyle-CssClass="hidden" runat="server" OnRowCommand="grdchequeseries_RowCommand" Style="width: 85%; margin-left: 41px; margin-bottom: 5px;">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <tr>
                                            <th class="inf_head w10">Sr.&nbsp;No.</th>
                                            <th class="inf_head w30">Cheque&nbsp;No.&nbsp;From</th>
                                            <th class="inf_head w30">Cheque&nbsp;No.&nbsp;To</th>
                                            <th class="inf_head w30">Action</th>
                                        </tr>
                                        <tr class="hide_my_pdosi">
                                        </tr>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNOFrom" Text='<%#Eval("ChequeFrom") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNoTo" Text='<%#Eval("ChequeTo") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" CommandName="EditItemRow" CommandArgument='<%#Container.DataItemIndex %>' Text="Edit" ControlStyle-CssClass="btn btn-sxs btn-info" runat="server" />
                                        <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <div class="panel-footer">
                            <div class="text-right">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnclear" CssClass="btn btn-danger" runat="server" Text="Clear" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfTransportID" runat="server" Value="0" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>