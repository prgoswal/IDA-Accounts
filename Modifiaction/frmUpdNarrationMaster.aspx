<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdNarrationMaster.aspx.cs" Inherits="Updation_frmUpdNarrationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Narration Updation Master

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
                                                        <asp:DropDownList ID="ddlFillVoucher" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFillVoucher_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-12 alphaonly">Standard Narration</label>
                                                    <div class="col-sm-12">
                                                        <asp:TextBox ID="txtSnarration" runat="server" CssClass="form-control" Enabled="false" MaxLength="50"></asp:TextBox>
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
                                                <asp:GridView ID="grdNarration" CssClass="table table-narration table-bordered" ShowHeader="false" AutoGenerateColumns="false" AllowPaging="false" PageSize="10" runat="server" OnRowEditing="grdNarration_RowEditing">
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
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" Visible="false"/>
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

