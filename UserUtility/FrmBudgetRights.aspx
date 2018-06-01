<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="FrmBudgetRights.aspx.cs" Inherits="UserUtility_FrmBudgetRights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">
        function onLoad() {
            var $rows = $('#ContentPlaceHolder1_chkAvailable tr[class!="header"]');
            $('#ContentPlaceHolder1_txtfilter').keyup(function () {
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return ! ~text.indexOf(val);
                }).hide();
            });
        }


        function EndRequest(sender, args) {
            onLoad();
        }
    </script>



    <script type="text/javascript">
        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= ChkSelected.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(onLoad);
            </script>
            <div class="content-wrapper form-control-mini">
                <h3 class="text-center" style="padding: 5px">Budget Assign For Cost Centre/Scheme  
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-sm-offset-3">
                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Sub-Section &nbsp;<i class="text-danger">*</i></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlUserName" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3" style="display: none;">
                                            <div class="form-group row">
                                                <label class="col-sm-10">According To&nbsp;<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlAccordingTo" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAccordingTo_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="2" Selected="True" Text="Sub-Section Wise" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body budgetright-row">
                                <div class="budgetright-1 table-bordered">
                                    <div class="panel-heading" style="margin-left: -15px; border-bottom: 1px solid; margin-bottom: -1px; background: #1c75bf; color: #fff; margin-right: -15px;">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <h4 class="text-center head" style="margin: 0">Available
                                           Cost Centre
                                                </h4>
                                            </div>
                                            <div class="col-sm-6 pos-rel">

                                                <asp:TextBox ID="txtfilter" runat="server" placeholder="Search..." CssClass="search-filter"></asp:TextBox>
                                                <i class="fa fa-search"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <style>
                                        .budgetright-row {
                                            padding-left: 15px;
                                            padding-right: 15px;
                                        }

                                        .budgetright-1 {
                                            float: left;
                                            width: calc(50% - 70px);
                                            padding-left: 15px;
                                            padding-right: 15px;
                                        }

                                        .budgetright-2 {
                                            float: left;
                                            width: 140px;
                                            padding-left: 15px;
                                            padding-right: 15px;
                                        }

                                        .budgetright-3 {
                                            float: left;
                                            width: calc(50% - 70px);
                                            padding-left: 15px;
                                            padding-right: 15px;
                                        }

                                        td input[type="checkbox"] {
                                            margin-top: 4px;
                                            display: block;
                                            float: left;
                                            margin-right: 4px;
                                            cursor: pointer;
                                            width: 5%;
                                        }

                                            td input[type="checkbox"] + label {
                                                user-select: none;
                                                display: block;
                                                cursor: pointer;
                                                float: left;
                                                width: 94%;
                                                font-weight: normal;
                                                font-size: 12px;
                                            }

                                            td input[type="checkbox"]:checked + label {
                                                font-weight: bold;
                                            }

                                        .checkbox_float tr {
                                            border-bottom: 1px solid #ddd;
                                        }

                                            .checkbox_float tr td {
                                                padding-left: 6px;
                                                padding-right: 6px;
                                                padding-bottom: 3px;
                                                padding-top: 3px;
                                            }

                                        .pos-rel {
                                            position: relative;
                                        }

                                        .search-filter {
                                            position: absolute;
                                            top: -3px;
                                            right: 7px;
                                            width: 246px;
                                            border: 1px solid #074375;
                                            padding-left: 5px;
                                            padding-right: 20px;
                                            border-radius: 3px;
                                            color: #000;
                                        }

                                            .search-filter + .fa {
                                                position: absolute;
                                                color: #a0758a;
                                                right: 13px;
                                                top: 2px;
                                            }
                                    </style>
                                    <div class="row over" style="height: 352px; overflow-y: scroll; border: 1px solid #ccc; padding: 0px">
                                        <asp:CheckBoxList ID="chkAvailable" class="header" runat="server" CssClass="checkbox_float">
                                        </asp:CheckBoxList>
                                    </div>

                                </div>
                                <div class="budgetright-2" style="padding-top: 100px;">
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAdd_Click" />
                                    <div class="form-group"></div>
                                    <asp:Button ID="btnAddAl" Text="Add-All" runat="server" CssClass="btn btn-primary  btn-block" OnClick="btnAddAl_Click" />
                                    <div class="form-group"></div>
                                    <asp:Button ID="btnRemove" Text="Remove" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnRemove_Click" />
                                    <div class="form-group"></div>
                                    <asp:Button ID="btnRemvAl" Text="Remove-All" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnRemvAl_Click" />

                                </div>


                                <div class="budgetright-3 table-bordered">
                                    <div class="panel-heading" style="margin-left: -15px; border-bottom: 1px solid; margin-bottom: -1px; background: #1c75bf; color: #fff; margin-right: -15px;">

                                        <h4 class="text-center head" style="margin: 0">Allocated
                                             Cost Centre
                                        </h4>
                                    </div>
                                    <div class="row" style="height: 352px; overflow-y: scroll; border: 1px solid #ccc; padding: 0px">
                                        <asp:CheckBoxList ID="ChkSelected" runat="server" CssClass="checkbox_float">
                                        </asp:CheckBoxList>
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

                    <asp:Panel runat="server" ID="pnlAddAll" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
                        <div class="panel panel-primary bodyContent" style="width: 34%; padding: 0">
                            <div class="panel-heading" style="background-color: #f05050">
                                <div class="panel-heading">
                                    <i class="fa fa-info-circle"></i>
                                    Are You Sure for Add all Available Cost Centre ?
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="text-right">
                                    <asp:Button ID="btnAddYes" OnClick="btnAddYes_Click" CssClass="btn btn-primary" Text="Yes" runat="server" />
                                    <asp:Button ID="btnAddNo" OnClick="btnAddNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlRemoveAll" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
                        <div class="panel panel-primary bodyContent" style="width: 34%; padding: 0">
                            <div class="panel-heading" style="background-color: #f05050">
                                <div class="panel-heading">
                                    <i class="fa fa-info-circle"></i>
                                    Are You Sure for Remove all Allocated Cost Centre ?
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="text-right">
                                    <asp:Button ID="btnRemoveYes" OnClick="btnRemoveYes_Click" CssClass="btn btn-primary" Text="Yes" runat="server" />
                                    <asp:Button ID="btnRemoveNo" OnClick="btnRemoveNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
