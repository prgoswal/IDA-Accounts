<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmSundries.aspx.cs" Inherits="AdminMasters_frmSundries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .select_all_check {
            margin-left: -28px;
            margin-bottom: -12px;
        }
    </style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <h3 class="text-center head">For Sundries
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-sm-5 table-bordered">
                                    <div class="panel-heading" style="margin-left: -15px; border-bottom: 1px solid; margin-bottom: -1px; background: #1c75bf; color: #fff; margin-right: -15px;">
                                        <h4 class="text-center head">Available</h4>
                                        <%--<div class="select_all_check">
                                        <asp:CheckBox Text="Select All" runat="server" />
                                    </div>--%>
                                    </div>
                                    <style>
                                        td input[type="checkbox"] {
                                            margin-top: 4px;
                                            display: block;
                                            float: left;
                                            margin-right: 4px;
                                            cursor: pointer;
                                        }

                                            td input[type="checkbox"] + label {
                                                user-select: none;
                                                cursor: pointer;
                                            }
                                    </style>
                                    <div class="row over" style="height: 350px; overflow-y: scroll; border: 1px solid #ccc; padding: 6px">
                                        <asp:CheckBoxList ID="chkAvailable" runat="server">
                                        </asp:CheckBoxList>
                                    </div>

                                </div>
                                <div class="col-sm-2" style="padding-top: 100px;">
                                    <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAdd_Click" />
                                    <div class="form-group"></div>
                                    <asp:Button ID="btnAddAl" Text="AddAll" OnClick="btnAddAl_Click" runat="server" CssClass="btn btn-primary  btn-block" Enabled="false" />
                                    <div class="form-group"></div>
                                    <asp:Button ID="btnRemove" Text="Remove" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnRemove_Click" />
                                    <div class="form-group"></div>
                                    <asp:Button ID="btnRemvAl" Text="RemoveAll" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnRemvAl_Click" Enabled="false" />
                                </div>
                                <div class="col-sm-5 table-bordered">
                                    <div class="panel-heading" style="margin-left: -15px; border-bottom: 1px solid; margin-bottom: -1px; background: #1c75bf; color: #fff; margin-right: -15px;">
                                        <h4 class="text-center head">Allocated</h4>
                                    </div>
                                    <div class="row" style="height: 350px; overflow-y: scroll; border: 1px solid #ccc; padding: 6px">
                                        <asp:CheckBoxList ID="ChkSelected" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="text-right">
                                    <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>