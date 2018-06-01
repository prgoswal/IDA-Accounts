<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUserRoll.aspx.cs" Inherits="UserUtility_frmUserRoll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="content-wrapper">
        <h3 class="text-center" style="padding: 5px">ROLL CREATION
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group row">
                                        <label class="col-sm-12 alphaonly">Add New Roll</label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtuser" runat="server" MaxLength="20" CssClass="form-control" placeholder="User Profile"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group row">
                                        <label class="col-sm-12 alphaonly">&nbsp;</label>
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary" runat="server" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-danger" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <style type="text/css">
                            .usertb {
                                width: 100%;
                            }
                        </style>
                    </div>
                    <div class="row">
                        <div class="panel panel-default" style="margin-bottom: 0" id="pnlUserGrid" runat="server" visible="true">
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
                                <asp:GridView ID="GridMatrix" align="center" runat="server" CssClass="table table-striped table-bordered">

                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="ACTION">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkbtnEdit" runat="server" Font-Bold="true" CommandName="btnEdit" CommandArgument='<%#Eval("RecordID" ) %>'>EDIT</asp:LinkButton>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%--     <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRollDesc" Text='<%#Eval("RollDesc") %>' runat="server"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>




                </div>
            </div>

            <%--<div class="row">
        <div class="col-lg-offset-2 col-md-offset-2 "></div>
        <div class="col-lg-8 col-sm-offset-2">
            <div class="user-creation">
                <h3 class="text-center">Profile Creation</h3>
                <hr class="changecolor" />
                <div class="row">
                     <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                    <div class="form-group">
                        <div class="col-md-3 col-md-offset-2">
                         
                        Add New Profile

                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtuser" runat="server" MaxLength="20" CssClass="form-control" placeholder="User Profile"></asp:TextBox>
                        </div>
                        <div class="col-md-3 ">
                          
                            <asp:Button ID="btnadd" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnadd_Click" />
                            <br />
                             <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    
                    <br />

                   
                    <div class="col-md-10 col-md-offset-1">
                        <asp:Label ID="label1" runat="server" CssClass="text-success"></asp:Label>
                        <asp:GridView ID="GridMatrix" align="center"   runat="server" CssClass="table table-striped table-bordered" OnRowCommand="GridMatrix_RowCommand" OnRowDataBound="GridMatrix_RowDataBound">
                           
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="ACTION">
                                 
                                    <ItemTemplate >
                                        <asp:LinkButton ID="linkbtnEdit" runat="server" Font-Bold="true" CssClass="bg-warning text-info"  CommandName="btnEdit" CommandArgument='<%#Eval("ItemId" ) %>'>EDIT</asp:LinkButton>
                      
                                    </ItemTemplate>
                                           
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                        <br />
                       
                        <asp:Button ID="btnfinalsave" runat="server" class="btn btn-primary" Visible="false" Text="Save" OnClick="btnfinalsave_Click" />

                    </div>
</ContentTemplate>
                    </asp:UpdatePanel>

                    
                    <div class="col-md-10 col-md-offset-1">
                        <asp:Label ID="lblMyMsg" runat="server"></asp:Label>
                    </div>




                    <div class="col-md-4 hidden">
                        <center>
                    <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server"
                        TabIndex="2" >Allow 
                    </asp:LinkButton>
                               <br />
                          
                      </center>
                    </div>
                    


                </div>
            </div>
        </div>




    </div>--%>
</asp:Content>





