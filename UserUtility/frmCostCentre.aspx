<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmCostCentre.aspx.cs" Inherits="UserUtility_frmCostCentre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Contesnt2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfCostCentreID" runat="server" Value="0" />
    <div class="content-wrapper">
        <h3 class="text-center head">Cost Centre Creation
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
                                            <label class="col-sm-4 alphaonly">Cost Centre Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control " placeholder="Cost Centre Name" MaxLength="30"></asp:TextBox>
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
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Save" />
                                            <asp:Button ID="btnclear" CssClass="btn btn-danger" OnClick="btnclear_Click" runat="server" Text="Clear" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="panel panel-default" style="margin-bottom: 0" id="pnlCostCentreGrid" runat="server" visible="true">
                    <div class="panel-body">
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
                        <asp:GridView ID="grdCostCentreCreation" CssClass="usertb first_tr_hide" AutoGenerateColumns="false" runat="server" OnRowCommand="grdCostCentreCreation_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <tr>
                                            <th class="inf_head" style="width: 12.05%;">Cost Centre Name </th>
                                            <th class="inf_head" style="width: 6.05%;">Action</th>
                                        </tr>
                                        <tr class="hide_my_pdosi">
                                        </tr>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostCentreID" Text='<%#Eval("CostCentreID") %>' Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblName" Text='<%#Eval("CostCentreName") %>' Visible="false" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostCentreName" Text='<%#Eval("CostCentreName") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="c">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" Text="Edit" OnClick="btnEdit_Click" CssClass="btn btn-primary fa r fa-floppy-o" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#1c75bf" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

