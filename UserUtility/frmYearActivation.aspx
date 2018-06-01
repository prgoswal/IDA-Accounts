<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmYearActivation.aspx.cs" Inherits="UserUtility_frmYearActivation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Financial Year Activation
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
                                            <label class="col-sm-4 alphaonly">Financial Year</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlFinancialYr" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--- Select Year ---" Value="0" Selected="True"></asp:ListItem>

                                                    <asp:ListItem Text="2018 - 2019" Value="2018 - 2019"></asp:ListItem>
                                                </asp:DropDownList>
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

        <div class="container_fluid" id="divBlcTransfer" runat="server" visible="false">
            <div class="row">
                <div class="col-sm-offset-3 col-sm-6">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-sm-12">

                                        <div class="form-group row">
                                            <label class="col-sm-5 alphaonly">Closing Stock Value (In Rs.)</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtClosingStockValue" placeholder="Closing Stock Value" runat="server" MaxLength="16" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group row">
                                                <label class="col-sm-5 alphaonly">Opening Balance Transfer</label>
                                                <div class="col-sm-7">

                                                    <asp:Button ID="btnTransfer" OnClick="btnTransfer_Click" CssClass="btn btn-primary" runat="server" Text="Transfer" />
                                                </div>

                                            </div>

                                        </div>

                                        <div id="divOpeningBlc" runat="server" visible="false">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-5 alphaonly">Opening Balance :</label>
                                                    <div class="col-sm-7">

                                                        <asp:CheckBox ID="chkOpeningBlc" runat="server" />
                                                    </div>

                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-5 alphaonly">Item Opening Balance :</label>
                                                    <div class="col-sm-7">

                                                        <asp:CheckBox ID="chkItemOpeningBlc" runat="server" />
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
            </div>

        </div>

        <asp:Panel runat="server" ID="pnlYearActivation" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
            <div class="panel panel-primary bodyContent" style="width: 34%; padding: 0">
                <div class="panel-heading" style="background-color: #27c24c">
                    <i class="fa fa-check-circle fa-lg">New Year Activation
                    </i>

                </div>


                <div class="panel-footer">
                    <div class="text-right">
                        <asp:Button ID="btnOk" OnClick="btnOk_Click" CssClass="btn btn-primary" Text="Ok" runat="server" />

                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
