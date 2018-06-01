<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmCompanyBranchMaster.aspx.cs" Inherits="UserUtility_frmCompanyBranchMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        legend, .Head {
            margin-bottom: 10px;
            font-size: 15px;
            background: #1c75bf;
            color: white;
            padding: 0px 8px;
        }

        .form-control {
            height: 26px;
            padding: 2px 10px;
        }

        .panel {
            /*height: 510px;
            overflow: auto;*/
        }

        /*input[type="text"]::placeholder {
            color: black !important;
        }*/

        .form-group {
            margin-bottom: 8px;
        }

        .table-choose-invoice-format tbody tr td {
            padding: 4px;
        }

        .table-terms tbody tr td {
            padding: 4px;
        }
    </style>
    <script type="text/javascript">

        function LoadAllScript() {
            LoadBasic();
        }
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For GSTIN -------------------//

            if ($('#<%=ddlGSTINNO.ClientID%>').val() != null) {

                if ($('#<%=ddlGSTINNO.ClientID%>').val() == '0') {
                    $('#<%=lblMsg.ClientID%>').html('Select GSTIN No.');
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                    $('#<%=ddlGSTINNO.ClientID%>').focus();
                    return false;
                }
            }

            //------------------For Branch Name -------------------//

            if ($('#<%=txtBranchName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Branch Name.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtBranchName.ClientID%>').focus();
                return false;
            }

            //------------------For Address -------------------//

            if ($('#<%=txtAddress.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Address.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtAddress.ClientID%>').focus();
                return false;
            }

            //------------------For City -------------------//

            if ($('#<%=txtCity.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter City.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtCity.ClientID%>').focus();
                return false;
            }

            //------------------For State -------------------//

            if ($('#<%=ddlState.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select State.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=ddlState.ClientID%>').focus();
                return false;
            }

            //------------------For Pin Code -------------------//

            if ($('#<%=txtPinCode.ClientID%>').val().length != 6) {
                $('#<%=lblMsg.ClientID%>').html('Please Enter 6 Digit Pin Code.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtPinCode.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center" style="padding: 5px">Company Branch Master
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">GSTIN &nbsp;</label><%--<span class="text-danger">*</span>--%>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlGSTINNO" CssClass="form-control text-uppercase" AutoPostBack="true" OnSelectedIndexChanged="ddlGSTINNO_SelectedIndexChanged" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Branch Name &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtBranchName" placeholder="Branch Name" CssClass="form-control" runat="server" />
                                                    <cc1:FilteredTextBoxExtender ID="ftbeBranchName" runat="server" TargetControlID="txtBranchName" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Address &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtAddress" MaxLength="130" placeholder="Address" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">City &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtCity" MaxLength="30" placeholder="City" CssClass="form-control" runat="server" />
                                                    <cc1:FilteredTextBoxExtender ID="ftbeCity" runat="server" TargetControlID="txtCity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">State &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Pincode &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtPinCode" MaxLength="6" placeholder="Pincode" CssClass="form-control numberonly" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-left">
                                            Mandatory Fields <span class="text-danger">*</span>
                                        </div>
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return Validation()" runat="server" />
                                            <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
