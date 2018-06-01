<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmUpdateItemMaster.aspx.cs" Inherits="Modifiaction_frmUpdateItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>
    <script type="text/javascript">

        function Validation() {

            if ($('#<%=ddlMinorGroup.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Minor Group.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlMinorGroup.ClientID%>').focus();
                return false;
            }

            //------------------For Item Name -------------------//

            if ($('#<%=txtItemName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Item Name.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');

                $('#<%=txtItemName.ClientID%>').focus();
                return false;
            }

            //------------------For Item Unit -------------------//

            if ($('#<%=ddlItemUnit.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Item Unit.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlItemUnit.ClientID%>').focus();
                return false;
            }

            //------------------For Item Type -------------------//

            if ($('#<%=ddlItemType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Item Type.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlItemType.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upAccountHead" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper form-control-mini">
                <style>
                    .form-control-mini .form-control {
                        padding: 2px 10px;
                        height: 27px;
                    }
                </style>
                <h3 class="text-center" style="padding: 5px">Update Item Master
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table class="search_updCash_table table-bordered">
                                                <tbody>
                                                    <tr>
                                                        <td>Items-
                                                        </td>
                                                        <td>
                                                            <style>
                                                                .chzn-single {
                                                                    margin-bottom: -1px;
                                                                }
                                                            </style>
                                                            <asp:DropDownList ID="ddlItems" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-sxs btn-primary pull-right" OnClick="btnSearch_Click" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Minor Group<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlMinorGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlMinorGroup_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Item Name<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtItemName" CssClass="form-control Alphaonly" placeholder="Item Name" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Short Name</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtShortName" CssClass="form-control Alphaonly" placeholder="Short Name" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divItemGroupDesc" visible="false" class="row" runat="server">
                                        <div class="col-sm-4" style="margin-top: -22px; margin-bottom: -17px;">
                                            <div class="form-group row">
                                                &nbsp;&nbsp;<asp:Label ID="lblItemGroup" class="col-sm-12" ForeColor="#27c24c" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Item Unit<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlItemUnit" CssClass="form-control" placeholder="Item Unit" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <style>
                                                .label-with-checkbox-container {
                                                    margin-bottom: 4px;
                                                }

                                                    .label-with-checkbox-container label {
                                                        margin-bottom: 0px;
                                                    }


                                                .cbSell select.form-control, .cbSell input.form-control {
                                                    height: 27px;
                                                    padding: 2px 12px;
                                                }

                                                .h-22 {
                                                    height: 22px;
                                                    padding: 0px 12px;
                                                }

                                                .gpa4 {
                                                    padding: 2px 1px !important;
                                                }
                                            </style>
                                            <div class="row">

                                                <div id="divMinorGp" runat="server">

                                                    <div class="col-sm-7">
                                                        <div class="form-group row">
                                                            <label class="col-sm-12">Item Secondary Unit</label>
                                                            <div class="col-sm-4 cbSell" style="padding-right: 0">
                                                                <asp:DropDownList ID="ddlIsUnitInd" CssClass="form-control gpa4" AutoPostBack="true" OnSelectedIndexChanged="ddlIsUnitInd_SelectedIndexChanged" runat="server">
                                                                    <asp:ListItem Value="0" Selected="True" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-8 cbSell">
                                                                <asp:DropDownList ID="ddlMinorUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMinorUnit_SelectedIndexChanged" CssClass="form-control col-xs-3" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="form-group row">
                                                            <label class="col-sm-12">
                                                                <asp:Label ID="lblSecFacQty" runat="server" />Calculation Factor</label>
                                                            <div class="col-sm-12 cbSell">
                                                                <asp:TextBox ID="txtMinorUnitQty" CssClass="form-control Money h-22" MaxLength="9" Text="0" placeholder="Selling Rate" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div style="margin-top: -11px;">
                                                            <label style="font-size: 11px;">Secondary Unit If Applicable</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <label class="col-sm-12">Selling Rate</label>
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="txtSellingRate" CssClass="form-control Money" MaxLength="9" Text="0" placeholder="Selling Rate" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <label class="col-sm-12">Item Type<i class="text-danger">*</i></label>
                                                        <div class="col-sm-12">
                                                            <asp:DropDownList ID="ddlItemType" AutoPostBack="true" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="None" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Goods" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Services" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Item Description</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtItemDescription" CssClass="form-control" MaxLength="90" placeholder="Item Description" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="col-sm-12">HSN/SAC Code<i class="text-danger">*</i></label>
                                                        <div class="col-sm-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtHSNSACCode" CssClass="form-control numberonly" MaxLength="8" placeholder="HSN / SAC Code" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <asp:LinkButton ID="lnkHSNSACCodeSearch" OnClick="lnkHSNSACCodeSearch_Click" CssClass="btn btn-primary" runat="server" Style="padding: 2px 5px;"><i class="fa fa-search"></i></asp:LinkButton>
                                                                </span>
                                                            </div>
                                                            <div class="pull-left">
                                                                <asp:Label ID="lblHSNSACErrorMSG" ForeColor="Red" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="pull-right">
                                                                <asp:HyperLink ID="hlnkSearchCode" Text="Search Code" runat="server"></asp:HyperLink>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-sm-6">

                                                    <div class="row">
                                                        <label class="col-sm-12">&nbsp;</label>
                                                        <asp:TextBox ID="txtHSNSACDesc" TextMode="MultiLine" CssClass="form-control" runat="server" Style="height: 48px; margin-left: 3px;" />
                                                    </div>

                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <label class="col-sm-12">Tax Rate</label>
                                                        <div class="col-sm-12">
                                                            <asp:DropDownList ID="ddlTaxrate" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="pull-right">
                                        <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                        <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" OnClientClick="return Validation()" Text="Update" CssClass="btn btn-primary btn-space-right" runat="server" Visible="true" />
                                        <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-danger btn-space-right" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:HiddenField ID="hfMainGrCode" runat="server" />
                            <asp:HiddenField ID="hfSubGrCode" runat="server" />
                            <asp:HiddenField ID="hfItemGroupID" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
