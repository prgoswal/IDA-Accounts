<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUserRights.aspx.cs" Inherits="UserUtility_frmUserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                <div class="row">
                    <div class="panel panel-default" style="margin-bottom: 0" id="pnlGSTINGrid" runat="server">
                        <div class="panel-body">
                            <h3 class="text-center">User Rights </h3>
                            <br />

                            <div class="col-md-12">

                                <style>
                                    .user-rights-grid {
                                        width: 100%;
                                    }

                                        .user-rights-grid th {
                                            background: #1c75bf;
                                            color: #fff;
                                        }

                                        .user-rights-grid tr td, .user-rights-grid tr th {
                                            padding: 0px !important;
                                            text-align: center;
                                            vertical-align: middle !important;
                                            word-break: break-all;
                                        }

                                    .user-rights-grid-hide-first-tr tr:first-child {
                                        display: none;
                                    }

                                    .user-rights-grid-hide-first-tr {
                                        margin-top: -2px;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(1), .user-rights-grid tr.alg th:nth-child(1) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(2), .user-rights-grid tr.alg th:nth-child(2) {
                                        width: 12%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(3), .user-rights-grid tr.alg th:nth-child(3) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(4), .user-rights-grid tr.alg th:nth-child(4) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(5), .user-rights-grid tr.alg th:nth-child(5) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(6), .user-rights-grid tr.alg th:nth-child(6) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(7), .user-rights-grid tr.alg th:nth-child(7) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(8), .user-rights-grid tr.alg th:nth-child(8) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(9), .user-rights-grid tr.alg th:nth-child(9) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(10), .user-rights-grid tr.alg th:nth-child(10) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(11), .user-rights-grid tr.alg th:nth-child(11) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(12), .user-rights-grid tr.alg th:nth-child(12) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(13), .user-rights-grid tr.alg th:nth-child(13) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(14), .user-rights-grid tr.alg th:nth-child(14) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(15), .user-rights-grid tr.alg th:nth-child(15) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(16), .user-rights-grid tr.alg th:nth-child(16) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(17), .user-rights-grid tr.alg th:nth-child(17) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(18), .user-rights-grid tr.alg th:nth-child(18) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(19), .user-rights-grid tr.alg th:nth-child(19) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(20), .user-rights-grid tr.alg th:nth-child(20) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(21), .user-rights-grid tr.alg th:nth-child(21) {
                                        width: 4%;
                                    }

                                    .user-rights-grid tr.alg td:nth-child(22), .user-rights-grid tr.alg th:nth-child(22) {
                                        width: 4%;
                                    }
                                </style>
                                <table class="table table-striped table-bordered user-rights-grid">
                                    <tr>
                                        <th rowspan="3">Action</th>
                                        <th rowspan="3">User</th>
                                        <th colspan="2" rowspan="2">Master</th>
                                        <th colspan="4" rowspan="2">Voucher</th>
                                        <th colspan="10">Reports</th>
                                        <th colspan="2" rowspan="2">Utility</th>
                                        <th colspan="2" rowspan="2">GST Return</th>
                                    </tr>
                                    <tr>
                                        <th colspan="2">Books</th>
                                        <th colspan="2">Ledger</th>
                                        <th colspan="2">Final Accounts</th>
                                        <th colspan="2">Item</th>
                                        <th colspan="2">Others</th>
                                    </tr>
                                    <tr>
                                        <th>Read</th>
                                        <th>Write</th>
                                        <th>Read</th>
                                        <th>Write</th>
                                        <th>Update</th>
                                        <th>Cancel</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                        <th>Show</th>
                                        <th>Hide</th>
                                    </tr>
                                    <tr class="alg" style="visibility: hidden;">
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                        <th></th>
                                    </tr>
                                </table>
                                <asp:GridView ID="GridMatrix" runat="server" CssClass="table table-striped table-bordered  user-rights-grid user-rights-grid-hide-first-tr" OnRowCommand="GridMatrix_RowCommand" OnRowDataBound="GridMatrix_RowDataBound" ShowHeader="true">
                                    <%--CssClass="table table-striped table-bordered"--%>
                                    <RowStyle CssClass="alg" />
                                    <Columns>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ACTION">

                                            <ItemTemplate>


                                                <asp:LinkButton ID="linkbtnEdit" Font-Bold="true" runat="server" CommandName="btnEdit" CommandArgument='<%#Eval("ItemID") %>'>Edit</asp:LinkButton>



                                                <%--<asp:LinkButton ID="btnupdate"  CommandName="UpdateRow" CommandArgument='<%#Eval("ShopCodeOdp" ) %>' runat="server">Update</asp:LinkButton>--%>
                                            </ItemTemplate>


                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>


                                <br />
                                <asp:Label ID="Label1" CssClass="text-success" runat="server"></asp:Label>
                                <asp:Button ID="btnfinalsave" runat="server" class="btn btn-primary" Visible="false" Text="Save" OnClick="btnfinalsave_Click" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

