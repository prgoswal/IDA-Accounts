<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmItemClosingStock_Report.aspx.cs" Inherits="Reports_FrmItemClosingStock_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function DateValidate() {
            if ($('#<%=txtAsOnDate.ClientID%>').val() == '' || $('#<%=txtAsOnDate.ClientID%>').val() != '') {

                var DateRegex = new RegExp(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/);
                var DateId = document.getElementById("<%= txtAsOnDate.ClientID %>").value;
                var valid = DateRegex.test(DateId);
                if (!valid) {
                    $('#<%=lblErrorMsg.ClientID%>').html('Please Enter From  Date (dd/MM/yyyy)');
                    $('#<%=txtAsOnDate.ClientID%>').focus();
                    return false;
                }
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper" style="height: 75%">
        <h3 class="text-center head">Item Closing Stock
        </h3>
        <div class="container_fluid">

            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3">As On Date<i class="text-danger">*</i></label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtAsOnDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3">Ware House</label>
                                        <div class="col-sm-9">
                                            <cc1:ComboBox ID="ddlWarehouse" AutoPostBack="true" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-sm-6" style="display:none" >

                                    <div class="form-group row">
                                        <label class="col-sm-3">Minor Group</label>
                                        <div class="col-sm-9">
                                            <cc1:ComboBox ID="ddlMinorGroup" AutoPostBack="true" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" OnSelectedIndexChanged="ddlMinorGroup_SelectedIndexChanged" Style="text-transform: uppercase"></cc1:ComboBox>
                                            <asp:Label ID="lblItemGroup" class="col-sm-12" ForeColor="#27c24c" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3">Item Name</label>
                                        <div class="col-sm-9">
                                            <cc1:ComboBox ID="ddlItemName" AutoPostBack="true" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="row" style="text-align: center">
                                <asp:Button Text="Show" ID="btnShow" Width="130px" runat="server" OnClientClick="return DateValidate()" class="btn btn-sxs btn-primary" OnClick="btnShow_Click"></asp:Button></td>
                            </div>
                            <div class="row">

                                <div style="text-align: center">
                                    <asp:Label Style="font-weight: bold; color: red; font-size: medium" ID="lblErrorMsg" runat="server" />
                                    <asp:HiddenField ID="hfMainGrCode" runat="server" />
                                    <asp:HiddenField ID="hfSubGrCode" runat="server" />
                                    <asp:HiddenField ID="hfItemGroupID" runat="server" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>

