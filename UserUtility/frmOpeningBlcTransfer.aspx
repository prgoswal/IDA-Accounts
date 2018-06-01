<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmOpeningBlcTransfer.aspx.cs" Inherits="UserUtility_frmOpeningBlcTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Opening Balance Transfer
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
                                            <label class="col-sm-4 alphaonly">Opening Balance Transfer</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                <asp:Button ID="btnTransfer" OnClick="btnTransfer_Click" CssClass="btn btn-primary" runat="server" Text="Transfer" />

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
</asp:Content>

