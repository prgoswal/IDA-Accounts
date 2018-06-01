<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmModuleRights.aspx.cs" Inherits="UserUtility_frmModuleRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div class="row">
            <div class="panel panel-default" style="margin-bottom: 0" id="pnlGSTINGrid" runat="server">
                <div class="panel-body">
                    <h3 class="text-center">Module Rights </h3>
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-10 col-md-offset-1">
                            <table style="width: 96%; margin-left: 42px; margin-top: -44px; background-color: #526a9c; color: white; border-radius: 5px;">
                                <tr>
                                    <td style="width: 56%; border-radius: 2px; font-size: 20px; font-weight: bold; text-align: center; background-color: #526a9c; color: white; padding-left: 20px">System Modules</td>
                                    <%--  <td style="text-align: center; font-size: 20px; font-weight: bold; background-color: #5c85cf; padding-right: 0px"></td>--%>
                                </tr>
                            </table>
                            <asp:GridView ID="GridMatrix" runat="server" CssClass="table table-striped table-bordered" OnRowCommand="GridMatrix_RowCommand" OnRowDataBound="GridMatrix_RowDataBound">
                                <%--CssClass="table table-striped table-bordered"--%>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ACTION">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkbtnEdit" Font-Bold="true" runat="server" CommandName="btnEdit" CommandArgument='<%#Eval("RecordID" ) %>'>Edit</asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnupdate"  CommandName="UpdateRow" CommandArgument='<%#Eval("ShopCodeOdp" ) %>' runat="server">Update</asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                        </div>
                        <br />
                        <asp:Label ID="Label1" CssClass="text-success" runat="server"></asp:Label>
                        <asp:Button ID="btnfinalsave" runat="server" class="btn btn-primary" Visible="false" Text="Save" />
                    </div>
                    <div class="col-md-10 col-md-offset-1">
                        <asp:Label ID="lblMyMsg" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-4 hidden">
                        <center>
                    <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server"
                        TabIndex="2" >Allow 
                    </asp:LinkButton>
                               <br />
                           <asp:Label ID="lblmsg" runat="server"></asp:Label>
                      </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

