<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmCashReceipt.aspx.cs" Inherits="frmCashReceipt" %>

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


    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script>

        //$(document).ready(function () {
        //    $('.gcombobox').find('option').each(function () {
        //        $('#browsers').append("<option value='" + $(this).html() + "' data-fixvalue='" + $(this).attr('value') + "'></option>");
        //    });
        //    $('#browsers-picker').change(function () {
        //        var f = $(this).val();
        //        var t = $(this).attr('data-fixvalue');
        //        alert(t);
        //        $('.gcombobox').find('option').each(function () {
        //            var k = $(this).html();
        //            if (k == f) {
        //                $(k).attr('checked', 'checked')
        //            }
        //        });

        //    });
        //});
        function LoadAllScript() {


            //$(".gtoggle").on("click", function () {
            //    $("#combobox").toggle();
            //});
            LoadBasic();
            ChoosenDDL();

            $('#<%=txtVoucherDate.ClientID%>').focus(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });

            $('#<%=txtVoucherDate.ClientID%>').click(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });

        }
    </script>

    <script type="text/javascript">
        $(function () {
            $(document).keydown(function (e) {
                return (e.which || e.keyCode) != 116;
            });
        });
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
                <h3 class="text-center head">CASH RECEIPT
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">

                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <table class="cashreceipt_table1 table-bordered">
                                        <tr>
                                            <th colspan="100%" class="inf_head"></th>
                                        </tr>
                                        <tr>
                                            <th style="width: 10%; display: none;">
                                                <label>For Back Date Entry</label>
                                            </th>

                                            <th style="width: 10%; display: none;">
                                                <label visible="false" runat="server" id="thVNo">Voucher Number</label>
                                            </th>

                                            <th style="width: 10%">
                                                <label>Voucher Date</label>
                                            </th>

                                            <th style="width: 17%" class="hidden">
                                                <label class="hidden">Last Voucher No.</label>
                                            </th>

                                            <th style="width: 17%">
                                                <label>Cash Account</label>
                                            </th>

                                            <%-- <th style="width: 10%">
                                                <label>Service No</label></th>--%>

                                            <th style="width: 33%" id="thCCCode" runat="server" visible="false">&nbsp;<label>Cost Centre</label></th>

                                        </tr>
                                        <tr>
                                            <style>
                                                .checkbox_container_CashRec {
                                                    padding-left: 11px;
                                                    float: left;
                                                    display: block;
                                                }

                                                    .checkbox_container_CashRec input[type="checkbox"] {
                                                        float: left;
                                                        display: block;
                                                        margin-top: 4px;
                                                        cursor: pointer;
                                                    }

                                                    .checkbox_container_CashRec label {
                                                        font-weight: normal;
                                                        margin-bottom: 0px;
                                                        float: left;
                                                        margin-left: 6px;
                                                        display: block;
                                                        cursor: pointer;
                                                        user-select: none;
                                                    }

                                                .brk_all {
                                                    word-break: break-all;
                                                }

                                                table.brk_all {
                                                    width: 100%;
                                                }
                                            </style>

                                            <td style="width: 10%; font-size: 13px; margin-bottom: 20px!important; display: none;">
                                                <span class="checkbox_container_CashRec">
                                                    <asp:CheckBox ID="chkBankEntry" runat="server" OnCheckedChanged="chkBankEntry_CheckedChanged" AutoPostBack="true" Text="(If Yes- Tick Here)" /></span>
                                                <%--<input type="checkbox" id="chkBankEntry" name="BankEntry" onclick="ForBackEntry();">--%>
                                            </td>

                                            <td style="width: 10%; display: none;">
                                                <span visible="false" runat="server" id="tdVNo">
                                                    <asp:TextBox ID="txtVoucherNo" MaxLength="10" placeholder="Voucher Number" Style="width: 100%" runat="server" CssClass="numberonly" /></span>
                                            </td>

                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />

                                                <asp:RequiredFieldValidator ID="ReqtxtVoucherDate" runat="server" ControlToValidate="txtVoucherDate" ErrorMessage="Enter Voucher Date!" ValidationGroup="btnsave" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </td>

                                            <td style="width: 17%" class="hidden">
                                                <asp:TextBox ID="txtLastVoucherNo" CssClass="numberonly hidden" Enabled="false" placeholder=" Enter Last Voucher No" Style="width: 100%" runat="server" />
                                            </td>

                                            <td style="width: 17%">
                                                <asp:DropDownList ID="ddlCashAccount" Style="width: 100%" runat="server"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="ReqddlCashAccount" runat="server" ControlToValidate="ddlCashAccount" InitialValue="0" ErrorMessage="Select Cash Account!" ValidationGroup="GrpValid" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </td>

                                            <%--<td style="width: 10%"   runat="server">
                                            <asp:TextBox ID="txtServiceNo"  runat="server"></asp:TextBox>
                                            </td>--%>

                                            <td style="width: 33%" visible="false" id="tdCCCode" runat="server">
                                                <%--<asp:DropDownList ID="ddlCostCenter" runat="server" />--%>


                                                <asp:DropDownList ID="ddlCostCenter" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="ReqddlCostCenter" runat="server" ControlToValidate="ddlCostCenter" InitialValue="0" ErrorMessage="Select Cost Centre!" ValidationGroup="btnsave" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <table id="tbl_id" class="table-bordered brk_all">
                                        <thead>
                                            <tr class="inf_head">
                                                <th style="width: 30%">Account Head</th>
                                                <th style="width: 10%">Capital/Revenue</th>
                                                <th style="width: 10%; display: none;">GSTIN</th>
                                                <th style="width: 10%">Demand No.</th>
                                                <th style="width: 10%">Demand Date</th>
                                                <th style="width: 10%">Amount (In Rs.)</th>
                                                <th style="width: 10%">Dr/Cr</th>
                                                <th style="width: 10%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="col1">
                                                    <style>
                                                        .chzn-single-top .chzn-single {
                                                            height: 25px !important;
                                                        }

                                                        .chzn-single-top .chzn-container {
                                                            width: 130px !important;
                                                        }

                                                        .align_with_combobox td {
                                                            border: 0 !important;
                                                        }
                                                    </style>
                                                    <%--<asp:DropDownList ID="txtAccountHead" runat="server" CssClass="chzn-select" OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged" Width="300px"></asp:DropDownList>--%>

                                                    <%--<cc1:ComboBox ID="txtAccountHead" CssClass="inpt autoCopleteBox relative_gt" runat="server" Width="250px" AutoPostBack="true" placeholder="p" OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>--%>


                                                    <asp:DropDownList ID="txtAccountHead" CssClass="chzn-select" runat="server"></asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="ReqtxtAccountHead" runat="server" ControlToValidate="txtAccountHead" InitialValue="0" ErrorMessage="Enter Account Head." ValidationGroup="GrpValid" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCapitalRevenue" runat="server">
                                                        <asp:ListItem Text="-select-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Revenue" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Capital" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:DropDownList ID="ddlGSTINNo" runat="server"></asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtInVoiceNo" CssClass="inpt AlphaNum" MaxLength="16" Style="width: 100%" runat="server" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtInvoiceDate" CssClass="inpt datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtAmount" CssClass="inpt Money" MaxLength="13" Style="width: 100%" runat="server" />


                                                    <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage=" Please Enter Amount." ValidationGroup="GrpValid" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ControlToValidate="txtAmount" ID="regExVal" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="GrpValid" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddlCrOrDr" CssClass="inpt" Style="width: 100%; height: 25px;" runat="server">
                                                        <asp:ListItem Selected="True" Text="Cr" />
                                                        <asp:ListItem Text="Dr" />
                                                    </asp:DropDownList>
                                                </td>

                                                <td class="col8">
                                                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" Text="Add" class="btn btn-primary btn-xs add_click add_btn" runat="server" ValidationGroup="GrpValid" />
                                                </td>

                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:GridView ID="gvCashReceipt" CssClass="table-bordered brk_all" AutoGenerateColumns="false" runat="server" ShowHeader="false" OnRowCommand="gvCashReceipt_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountHeadID" Text='<%#Eval("AccHeadValue") %>' CssClass="hidden" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAccountHead" Style="width: 100%; height: 27px;" Text='<%#Eval("AcctHeadText") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCapitalRevenue" Style="width: 100%;" Text='<%#Eval("IsCapitalRevenue") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartyID" Text='<%#Eval("PartyID") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblBillNos" Text='<%#Eval("BillNos") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTIN" Text='<%#Eval("GSTIN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" Style="width: 100%" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceDate" Style="width: 100%" Text='<%#Eval("InvoiceDate") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRCR" Text='<%#Eval("DrCr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%">
                                                <ItemTemplate>
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
                                </div>
                            </div>

                            <div id="panel1" class="panel-body">
                                <div class="col-sm-12">
                                    <table class="cashreceipt_table3 table-bordered">
                                        <tbody>
                                            <tr>
                                                <td colspan="3">
                                                    <label>Service No</label>
                                                    <asp:TextBox ID="txtServiceNo" Width="155px" Enabled="false" runat="server" />

                                                    <asp:Button Style="margin-top: -1px; font-family: FontAwesome; padding: 1PX 12PX; height: 25PX; width: CALC(100% - 160PX);" ID="btnServiceNo" runat="server" class="btn btn-primary btn-xs add_click add_btn" Text="&#xf002;" OnClick="btnServiceNo_Click" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Party Name</label>
                                                    <asp:TextBox ID="txtPartyName" Enabled="false" Width="380px" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Party Address</label>
                                                    <asp:TextBox ID="txtPartyAddress" Enabled="false" Width="270px" runat="server" />
                                                </td>

                                                <td colspan="2">
                                                    <label>Party GSTIN</label>
                                                    <asp:TextBox ID="txtpartyGstIN" Enabled="false" Width="180px" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Net Amount</label>
                                                    <asp:TextBox ID="txtInvoiceTotalAmount" Width="120px" Enabled="false" CssClass="inpt Money" runat="server" />
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
                                </div>
                            </div>

                            <asp:HiddenField ID="hfLastVoucherDate" runat="server" />
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" class="btn btn-primary btn-space-right" ValidationGroup="btnsave" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>

