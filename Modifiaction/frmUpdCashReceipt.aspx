<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdCashReceipt.aspx.cs" Inherits="Modifiaction_frmUpdCashReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        /*error message popup*/
        td {
            position: relative;
        }

        .gst-ac-error {
            background: #f05050;
            color: #fff;
            padding: 4px;
            border-radius: 4px;
            border: 1px solid #fff;
            position: absolute;
            Z-INDEX: 2;
            left: 0;
            box-shadow: 0px 0px 5px 2px rgba(0,0,0,0.5);
            top: calc(100% + 4px);
        }

            .gst-ac-error::after {
                content: '';
                display: block;
                position: absolute;
                bottom: 100%;
                left: 50%;
                width: 0;
                Z-INDEX: 2;
                transform: translateX(-50%);
                height: 0;
                border-top: 10px solid transparent;
                border-right: 10px solid transparent;
                border-bottom: 10px solid #f05050;
                border-left: 10px solid transparent;
            }
    </style>

    <script>
        $(document).ready(function () {
            $('.cashreciept_activeme').addClass('active');
        });
        function LoadAllScript() {
            ChoosenDDL();

            function PopOverError(id, plac, msg) {
                try {
                    $(id).popover({
                        title: 'Error!',
                        trigger: 'manual',
                        placement: plac,
                        content: function () {
                            var message = msg; //"Allow Numbers Only!";
                            return message;
                        }
                    });
                    $(id).popover("show");
                } catch (e) { }
            }

            //// For Numeri Value Only \\\\\\
            $('.numberonly').keypress(function (event) {

                try {
                    var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
                    var id = ('#' + this.id);
                    if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                        PopOverError(id, 'top', 'Allow Numbers Only!');
                        return false;
                    }
                    else {
                        $(id).popover('destroy');
                        return true;
                    }
                } catch (e) {

                }
            });


            $('.numberonly').blur(function (e) {
                try {
                    var id = ('#' + this.id);
                    $(id).popover('destroy');
                } catch (e) { }
            });

            //// For Valid Date Allow \\\\\\
            $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });
            $('#<%=txtVoucherDate.ClientID%>').focus(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });

            $('#<%=txtVoucherDate.ClientID%>').click(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });
            $('.datepicker').blur(function (e) {
                try {

                    var id = ('#' + this.id);
                    var date = $(id).val();
                    $(id).popover('destroy');
                    var valid = true;
                    if (date.length <= 0 || date == '' || date == undefined) {
                        return false;
                    }
                    if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                        valid = true;
                    } else {
                        valid = false;
                    }

                    if (valid) {
                        $(id).popover('destroy');
                    } else {
                        PopOverError(id, 'top', 'Invalid Date.');
                    }
                } catch (e) { }
            });
            $('.datepicker').keypress(function (e) {
                try {
                    $(id).popover('destroy');
                    var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
                    var id = ('#' + this.id);
                    if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                        PopOverError(id, 'top', 'Enter Valid Key For Date.');
                        return false; //Non Numeric Value Return Directly;
                    }
                    else {
                        if ($(id).val() === undefined) {
                            event.preventDefault();
                            return;
                        }
                        if (e.key == "/") {
                            PopOverError(id, 'top', 'This Key Is Invalid!');
                            event.preventDefault();
                            return false;
                        }
                        if (e.keyCode != 8) {

                            var DateVal = $(id).val();
                            if (e.keyCode == 191) {
                                var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                                PopOverError(id, 'top', 'Enter Valid Date!');
                                $(id).val(corr);
                                event.preventDefault();
                                return false;
                            }

                            if ($(id).val().length == 2) {
                                if ($(id).val() < 1 || $(id).val() > 31) {
                                    $(id).val("")
                                    PopOverError(id, 'top', 'Enter Valid Day!');
                                    event.preventDefault();
                                    return false;
                                }
                                $(id).val($(id).val() + "/");
                            } else if ($(id).val().length == 5) {
                                var month = $(id).val().substring(3, 6);
                                if (month < 1 || month > 12) {
                                    var corr = $(id).val().replace("/" + month, "");
                                    $(id).val(corr);
                                    PopOverError(id, 'top', 'Enter Valid Month!');
                                    event.preventDefault();
                                    return false;
                                }
                                $(id).val($(id).val() + "/");
                            } else if ($(id).val().length == 10) {
                                var Inputyear = $(id).val().substring(6, 11);
                                var NowYear = new Date().getUTCFullYear();
                                if (Inputyear < 1900 || Inputyear > NowYear) {
                                    var corr = $(id).val().replace(Inputyear, "");
                                    $(id).val(corr);
                                    PopOverError(id, 'top', 'Enter Valid Year!');
                                    event.preventDefault();
                                    return false;
                                }
                            }
                            else { $(id).popover('destroy'); }
                        }
                    }
                } catch (e) { }
            });

            //// For Amount upto 2 decimal Place \\\
            $('.Money').blur(function (e) {
                try {
                    var id = ('#' + this.id);
                    $(id).popover('destroy');
                } catch (e) { }
            });
            $('.Money').keypress(function (e) {
                try {
                    var id = ('#' + this.id);
                    var val = $(id).val();
                    var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
                    var id = ('#' + this.id);
                    $(id).popover('destroy');

                    if (chCode != 46) {
                        if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                            PopOverError(id, 'top', 'Enter Valid Amount');
                            return false; //Non Numeric Value Return Directly;
                        }
                    }

                    if (val.indexOf(".") != -1 && val.indexOf(".") + 3 == val.length && event.keyCode != 8 && event.keyCode != 17 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 9) {
                        PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                        event.preventDefault();
                        return false;
                    }
                    if (event.keyCode == 46 && val == "") {
                        PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                        event.preventDefault();
                        return false;
                    }
                    if (val.split(".").length > 1 && event.keyCode == 46) {
                        PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                        event.preventDefault();
                        return false;
                    }
                } catch (e) {

                }
            });

            //// Disable Pasting IN Text Box \\\\
            $('input.numberonly, input.datepicker, input.Money').bind('copy paste', function (e) {
                e.preventDefault();
            });

            $("input").attr("autocomplete", "off");
            $(".datepicker").attr("autocomplete", "off");
            $(".Money").attr("autocomplete", "off");
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $(document).keydown(function (e) {
                return (e.which || e.keyCode) != 116;
            });
        });
    </script>
    <script type="text/javascript">
        function ShowMessage(message) {
            alert(message);
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
                <h3 class="text-center head">CASH RECEIPT UPDATION
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <style>
                                .search_updCash_table {
                                    width: 30%;
                                    margin-bottom: 15px;
                                }

                                    .search_updCash_table tr td:nth-child(1) {
                                        width: 20%;
                                    }

                                    .search_updCash_table tr td:nth-child(2) {
                                        width: 65%;
                                    }

                                    .search_updCash_table tr td:nth-child(3) {
                                        width: 15%;
                                    }
                            </style>
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <table class="search_updCash_table table-bordered">
                                        <tr>
                                            <td>Voucher&nbsp;No-
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSearchVNo" MaxLength="8" runat="server" CssClass="numberonly"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ReqtxtSearchVNo" runat="server" ControlToValidate="txtSearchVNo" ErrorMessage="Enter Voucher Number!" ValidationGroup="btnSave" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sxs" runat="server" OnClick="btnGo_Click" Text="Search" />
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12">
                                    <table class="cashreceipt_table1 table-bordered">
                                        <tr>
                                            <th colspan="100%" class="inf_head"></th>
                                        </tr>
                                        <tr>
                                            <th class="col1 ">
                                                <label>Voucher Date</label></th>
                                            <th class="hidden">
                                                <label class="hidden">Last Voucher No.</label></th>


                                            <th class="col3">
                                                <label>Cash Account</label></th>
                                            <th class="col3" id="thCCCode" runat="server" visible="false">&nbsp;<label>Cost Centre</label></th>
                                        </tr>
                                        <tr>
                                            <td class="col1">
                                                <asp:TextBox ID="txtVoucherDate" Enabled="false" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />
                                            </td>
                                            <td class="hidden">
                                                <asp:TextBox ID="txtLastVoucherNo" CssClass="numberonly hidden" Enabled="false" placeholder=" Enter Last Voucher No" Style="width: 100%" runat="server" />
                                            </td>


                                            <td class="col3">
                                                <asp:DropDownList ID="ddlCashAccount" Style="width: 100%" runat="server"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="ReqddlCashAccount" runat="server" ControlToValidate="ddlCashAccount" InitialValue="0" ErrorMessage="Select Cash Account!" ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </td>
                                            <td class="col3" visible="false" id="tdCCCode" runat="server">
                                                <asp:DropDownList ID="ddlCostCenter" runat="server" />

                                                <asp:RequiredFieldValidator ID="ReqddlCostCenter" runat="server" ControlToValidate="ddlCostCenter" InitialValue="0" ErrorMessage="Select Cost Center!" ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <table id="tbl_id" class="cashreceipt_table2 table-bordered">
                                        <thead>
                                            <tr class="inf_head">
                                                <td class="col-lg-4">Account Head
                                                </td>
                                                <%-- <th class="col1">Account Head</th>--%>
                                                <th class="col3">Capital/Revenue</th>
                                                <th style="display: none;" class="col3">GSTIN</th>
                                                <th class="col4">Demand No.</th>
                                                <th class="col5">Demand Date</th>
                                                <th class="col6">Amount</th>
                                                <th class="col7">Dr/Cr</th>
                                                <th class="col8"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="1">
                                                    <%--<cc1:ComboBox ID="txtAccountHead"
                                                        CssClass="inpt autoCopleteBox relative_gt"
                                                        runat="server" Width="250px"
                                                        OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged"
                                                        AutoPostBack="true"
                                                        placeholder="p"
                                                        AutoCompleteMode="SuggestAppend"
                                                        CaseSensitive="False"
                                                        Style="text-transform: uppercase">
                                                    </cc1:ComboBox>--%>

                                                    <asp:DropDownList ID="txtAccountHead" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="ReqtxtAccountHead" runat="server" ControlToValidate="txtAccountHead" InitialValue="0" ErrorMessage="Enter Account Head." ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                </td>
                                                <td colspan="1">
                                                    <asp:DropDownList ID="ddlCapitalRevenue" runat="server">
                                                        <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Revenue" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Capital" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <%--<td class="col2">--%>
                                                <div id="divPartySelect" runat="server">
                                                    <%--<asp:TextBox ID="txtServiceNo" runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlSecondaryParty" runat="server" Visible="false"></asp:DropDownList>
                                                    <button id="btnRegToggle" runat="server" visible="false" data-toggle="collapse" data-target="#divdrop" class="form-control input-sm" type="button"></button>
                                                    <div id="divdrop" class="collapse pos-abs">
                                                        <div>
                                                            <asp:CheckBoxList ID="CbOutstandingBill" runat="server"></asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- </td>--%>
                                                <td class="col3" style="display: none;">
                                                    <asp:DropDownList ID="ddlGSTINNo" runat="server"></asp:DropDownList>
                                                </td>
                                                <td class="col4">
                                                    <asp:TextBox ID="txtInVoiceNo" CssClass="inpt AlphaNum" MaxLength="16" Style="width: 100%" runat="server" />
                                                </td>
                                                <td class="col5">
                                                    <asp:TextBox ID="txtInvoiceDate" CssClass="inpt datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                </td>
                                                <td class="col6">
                                                    <asp:TextBox ID="txtAmount" CssClass="inpt Money" MaxLength="13" Style="width: 100%" runat="server" />

                                                    <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage=" Please Enter Amount." ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ControlToValidate="txtAmount" ID="regExVal" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="btnAdd" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                </td>
                                                <td class="col7">
                                                    <asp:DropDownList ID="ddlCrOrDr" CssClass="inpt" Style="width: 100%; height: 27px;" runat="server">
                                                        <asp:ListItem Selected="True" Text="Cr" />
                                                        <asp:ListItem Text="Dr" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="col8">
                                                    <asp:Button ID="btnAdd" Text="Add" class="btn btn-primary btn-xs add_click add_btn" OnClick="btnAdd_Click" runat="server" ValidationGroup="btnAdd" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:GridView ID="gvCashReceipt" CssClass="cashreceipt_table2_grid table-bordered" AutoGenerateColumns="false" OnRowCommand="gvCashReceipt_RowCommand" runat="server" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="col1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountHeadID" Text='<%#Eval("AccCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAccountHead" Style="width: 100%; height: 27px;" Text='<%#Eval("AccName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCapitalRevenue" Style="width: 100%;" Text='<%#Eval("IsCapitalRevenue") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCapitalRevenueName" Style="width: 100%;" Text='<%#Eval("IsCapitalRevenuenName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col3" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTIN" Text='<%#Eval("GSTIN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" Style="width: 100%" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceDate" Style="width: 100%" Text='<%#Eval("InvoiceDate") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRCR" Text='<%#Eval("DrCr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col8">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" Text="Edit" CommandName="EditRow" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-xs btn-info add_btn" runat="server"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>

                                    <%--<div id="panel1" class="panel-body">
                                        <div class="col-sm-12">--%>

                                    <table class="cashreceipt_table3 table-bordered">
                                        <tbody>
                                            <tr>
                                                <td colspan="3">
                                                    <label>Service No.</label>
                                                    <asp:TextBox ID="txtServiceNo" Width="150px" Enabled="false" runat="server" />
                                                    <asp:Button Style="margin-top: -1px; font-family: FontAwesome; padding: 1PX 12PX; height: 25PX; width: CALC(100% - 160PX);" class="btn btn-primary btn-xs add_click add_btn" Text="&#xf002;" ID="btnServiceNo" runat="server" OnClick="btnServiceNo_Click" />
                                                </td>



                                                <td colspan="2">
                                                    <label>Party Name</label>
                                                    <asp:TextBox ID="txtPartyName" Enabled="false" Width="320px" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Party Address</label>
                                                    <asp:TextBox ID="txtPartyAddress" Enabled="false" Width="270px" runat="server" />
                                                </td>

                                                <td colspan="2">
                                                    <label>Party GSTIN</label>
                                                    <asp:TextBox ID="txtpartyGstIN" Enabled="false" Width="160px" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Net Amount</label>
                                                    <asp:TextBox ID="txtInvoiceTotalAmount" Enabled="false" Width="100px" CssClass="inpt Money" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="11">
                                                    <label style="margin-right: 10px; margin-top: 5px;">Narration</label>
                                                    <style>
                                                        .text-uppercase input {
                                                            text-transform: uppercase;
                                                        }
                                                    </style>
                                                    <asp:TextBox ID="txtNarration" placeholder="Enter Narration" CssClass="text-uppercase" MaxLength="120" Width="50%" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>
                                    <%-- </div>
                                    </div>--%>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfCapitalRevenueInd" runat="server" />
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-left">
                                            <asp:Button Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="btn btn-warning" Enabled="false" Visible="false" />
                                        </div>
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />

                                            <asp:Button ID="btnSave" Visible="true" runat="server" Text="Update" class="btn btn-primary btn-space-right" OnClick="btnSave_Click1" ValidationGroup="btnSave" />


                                            <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger btn-space-right" OnClick="btnClear_Click" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
                <div class="panel panel-primary bodyPop" style="width: 30%; padding: 0">
                    <div class="panel-heading">
                        <i class="fa fa-info-circle"></i>
                        Are You Sure for Cancel this Invoice Number 
                    </div>



                    <div class="panel-body">
                        <div class="mb10">
                            Reason for Cancel
                                             <br />


                            Note: You Can Also Type Here Your Cancel Reason.
                                    <div class="align_with_combobox">


                                        <style>
                                            .relative_gt ul {
                                                overflow-y: scroll !important;
                                            }

                                            .mb10 {
                                                margin-bottom: 15px;
                                            }
                                        </style>

                                        <cc1:ComboBox ID="ddlCancelReason" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" Style="text-transform: uppercase"></cc1:ComboBox>
                                        <%--<asp:DropDownList ID="ddlSalesto" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesto_SelectedIndexChanged1" runat="server" />--%>
                                        <asp:TextBox ID="txtCancelReason" AutoPostBack="true" CssClass="text-uppercase" MaxLength="45" Visible="false" placeholder="Sales To" runat="server" TextMode="MultiLine" />

                                    </div>


                            <%--<asp:TextBox ID="txtCancelReason" MaxLength="99" TextMode="MultiLine" runat="server"></asp:TextBox>--%>
                            <asp:RequiredFieldValidator runat="server" ID="reqtxtCancelReason" ControlToValidate="ddlCancelReason" ErrorMessage="**" ForeColor="Red" Display="Dynamic" ValidationGroup="btnYes" InitialValue="" />
                        </div>

                    </div>
                    <div class="panel-footer">
                        <div class="text-right">
                            <asp:Label ID="lblCancelMsg" CssClass="text-danger lblMsg" runat="server" />
                            <asp:Button ID="btnYes" OnClick="btnYes_Click" ValidationGroup="btnYes" CssClass="btn btn-primary" Text="Yes" runat="server" />
                            <asp:Button ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>

