<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmOldBalance.aspx.cs" Inherits="UserUtility_FrmOldBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Star {
            color: red;
        }
    </style>
    <script type="text/javascript">


        function LoadAllScript() {
            LoadBasic();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>


            <div class="content-wrapper">
                <h3 class="text-center head">Old Balance Entry
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="col-sm-offset-1 col-sm-10">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-2 alphaonly control-label">Account Head<i class="Star">*</i></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlAccountHead" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>

                                                    <label class="col-sm-2 alphaonly control-label">AVl Balance<i class="Star">*</i></label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtAvlBal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                    </div>

                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 alphaonly control-label">Book No.</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtbookNo" runat="server" CssClass="form-control numberonly NotAllowZero" MaxLength="3"></asp:TextBox>
                                                        <%--<asp:TextBox ID="txtOrd" CssClass="form-control datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />--%>
                                                    </div>

                                                    <label class="col-sm-2 alphaonly control-label">Page No.</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtPageNo" runat="server" CssClass="form-control numberonly NotAllowZero" MaxLength="3"></asp:TextBox>
                                                    </div>

                                                    <label class="col-sm-2 alphaonly control-label">Serial No.</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtSerNo" runat="server" CssClass="form-control numberonly NotAllowZero" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 alphaonly control-label">Reference No.<i class="Star">*</i></label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control numberonly NotAllowZero" MaxLength="8"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2 alphaonly control-label">Tender No.</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txttenderNo" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2 alphaonly control-label">Tender Date</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txttenderDate" runat="server" CssClass="form-control datepicker " MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 alphaonly control-label">Party Name<i class="Star">*</i></label>
                                                    <div class="col-sm-4 ">
                                                        <%--<asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                        <cc1:ComboBox ID="ddlPartyName" runat="server" CssClass="relative_gt input-box-td-ajax" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase">
                                                        </cc1:ComboBox>
                                                        <style>
                                                            .input-box-td-ajax input[type="text"] {
                                                                border: 1px solid #ddd;
                                                                border-radius: 4px;
                                                                height: 34px;
                                                                padding: 6px 12px;
                                                            }
                                                        </style>

                                                        <%--<asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                        <%--<cc1:ComboBox ID="ddlPartyName" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" Style="text-transform: uppercase"></cc1:ComboBox>--%>
                                                    </div>
                                                    <asp:HiddenField ID="hfAfterSaveInd" runat="server" />
                                                    <label class="col-sm-2 alphaonly control-label">Cost Centre</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="ddlcostCentre" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-2 alphaonly control-label">Deduction Date<i class="Star">*</i></label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtopendate" runat="server" CssClass="form-control datepicker " MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                    <label class="col-sm-2 alphaonly control-label">Amount<i class="Star">*</i></label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control numberonly" MaxLength="10"></asp:TextBox>
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
                                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnclear" CssClass="btn btn-danger" runat="server" Text="Clear" OnClick="btnclear_Click" />
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
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

