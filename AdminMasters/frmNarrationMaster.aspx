<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmNarrationMaster.aspx.cs" Inherits="Vouchers_frmNarrationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Narration Master
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
                                                    <label class="col-sm-12 alphaonly">Voucher Type</label>
                                                    <div class="col-sm-12">
                                                        <asp:DropDownList ID="ddlFillVoucher" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFillVoucher_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-12 alphaonly">Standard Narration</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtSnarration" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-6">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="GridHeader" class="col-md-12" runat="server">Available Narration</div>
                                            <div class="col-md-12" style="overflow: auto; height: 200px;">
                                                <asp:GridView ID="grdNarration" CssClass="table table-narration" ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" PageSize="10" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNarrtionId" Text='<%#Eval("NarrationID") %>' CssClass="hidden" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" Text='<%#Eval("NarrationDesc") %>' runat="server"></asp:Label>

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
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
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
