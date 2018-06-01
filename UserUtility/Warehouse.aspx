<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="Warehouse.aspx.cs" Inherits="Warehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/app.js"></script>
    <script src="js/index.js"></script>
    <script src="js/jquery-ui.js"></script>

    <div class="content-wrapper">

        <h3 class="text-center" class="p5">Warehouse Master</h3>

        <div class="container_fluid">
            <div class="row">
                <div class="col-sm-offset-3 col-sm-6">
                    <div class="panel panel-default">
                        <div class="panel-body">

                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-sm-12">
                                       <%-- <div class="form-group row">
                                            <label class="col-sm-4 ">GSTIN</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlGSTINNo" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>--%>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 ">Warehouse Address</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtWhouseAdd" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label class="col-sm-12 ">State</label>
                                            <div class="col-sm-12">
                                                <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label class="col-sm-12 alphaonly">City</label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" MaxLength="30" placeholder="City"></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" placeholder="City"></asp:DropDownList>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label class="col-sm-12 ">PinCode</label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="txtPin" runat="server" CssClass="form-control numberonly" placeholder="PinCode" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="panel-footer">
                            <div class="text-right">
                                <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

