<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdBankReceipt.aspx.cs" Inherits="Modifiaction_frmUpdBankReceipt" %>

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
            $('.bankreciept_activeme').addClass('active');
        });
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();

            //// For Valid Date Allow \\\\\\
            $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });
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
                <h3 class="text-center head">BANK RECEIPT UPDATION
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
                                                    <asp:Button ID="btnGo" OnClick="btnGo_Click" CssClass="btn btn-primary btn-sxs" runat="server" Text="Search" />
                                                </td>
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </div>
                                    <table class="cashpayment_table1 table-bordered">
                                        <tr>
                                            <th colspan="100%" class="inf_head"></th>
                                        </tr>
                                        <tr>
                                            <th class="col1">
                                                <label>Voucher Date</label></th>
                                            <th class="hidden">
                                                <label class="hidden">Last Voucher No.</label></th>
                                            <th class="col3">
                                                <label>Bank Account</label></th>
                                            <th class="col3" id="thCCCode" runat="server" visible="false">&nbsp;<label>Cost Centre</label></th>
                                        </tr>
                                        <tr>
                                            <td class="col1">
                                                <asp:TextBox ID="txtVoucherDate" Enabled="false" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />


                                                <asp:RequiredFieldValidator ID="ReqtxtVoucherDate" runat="server" ControlToValidate="txtVoucherDate" ErrorMessage="Enter Voucher Date!" ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="hidden">
                                                <asp:TextBox ID="txtLastVoucherNo" CssClass="numberonly hidden" Enabled="false" placeholder=" Enter Last Voucher No" Style="width: 100%" runat="server" />
                                            </td>


                                            <td class="col3">
                                                <asp:DropDownList ID="ddlBankAccount" Style="width: 100%" runat="server"></asp:DropDownList>


                                                <asp:RequiredFieldValidator ID="ReqddlBankAccount" runat="server" ControlToValidate="ddlBankAccount" InitialValue="0" ErrorMessage="Select Bank Account!" ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </td>
                                            <td class="col3" visible="false" id="tdCCCode" runat="server">
                                                <asp:DropDownList ID="ddlCostCenter" runat="server" />



                                                <asp:RequiredFieldValidator ID="ReqddlCostCentre" runat="server" ControlToValidate="ddlCostCenter" InitialValue="0" ErrorMessage="Select Cost Centre!" ValidationGroup="btnSave" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <table id="tbl_id" class="bankreceipt_table2 table-bordered">
                                        <thead>
                                            <tr class="inf_head">
                                                <td class="col-lg-4">Account Head
                                                </td>

                                                <th class="col3">Capital/Revenue</th>

                                                <%--<th class="col1">Account Head</th>
                                                <th class="col2">Service No.</th>--%>
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
                                                <td class="col1">
                                                    <%--<cc1:ComboBox ID="txtAccountHead" runat="server" Width="250px" AutoPostBack="true" placeholder="p" CssClass="relative_gt" OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>--%>


                                                    <asp:DropDownList ID="txtAccountHead" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="ReqtxtAccountHead" runat="server" ControlToValidate="txtAccountHead" InitialValue="0" ErrorMessage="Enter Account Head." ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                </td>


                                                <td colspan="1">
                                                    <asp:DropDownList ID="ddlCapitalRevenue" runat="server">
                                                        <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Revenue" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Capital" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <%--<td class="col2">--%>
                                                <%--<asp:TextBox ID="txtServiceNo" runat="server"></asp:TextBox>--%>
                                                <div id="divPartySelect" runat="server">
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
                                                    <asp:TextBox ID="txtAmount" CssClass="inpt Money" Style="width: 100%" runat="server" MaxLength="13" />

                                                    <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage=" Please Enter Amount." ValidationGroup="btnAdd" CssClass="gst-ac-error"></asp:RequiredFieldValidator>


                                                    <asp:RegularExpressionValidator ControlToValidate="txtAmount" ID="regExVal" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="GrpValid" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                </td>

                                                <td class="col7">
                                                    <asp:DropDownList ID="ddlCrOrDr" CssClass="inpt" Style="width: 100%; height: 27px;" runat="server">
                                                        <asp:ListItem Selected="True" Text="Cr" />
                                                        <asp:ListItem Text="Dr" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="col8">
                                                    <asp:Button ID="btnAdd" Text="Add" OnClick="btnAdd_Click" class="btn btn-primary btn-xs add_click add_btn" runat="server" ValidationGroup="btnAdd" />
                                                    <%--<a href="#!" id="addintable_deep" class="btn btn-primary btn-xs add_click add_btn" style="width: 100%; height: 27px;">Add</a>--%></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:GridView ID="gvBankReceipt" CssClass="bankreceipt_table2_grid table-bordered" AutoGenerateColumns="false" OnRowCommand="gvBankReceipt_RowCommand" runat="server" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="col1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountHeadID" Text='<%#Eval("AccCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAccountHead" Style="width: 100%; height: 27px;" Text='<%#Eval("Accname") %>' runat="server"></asp:Label>
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


                                    <table class="cashpayment_table3 table-bordered">
                                        <tbody>
                                            <tr>
                                                <td colspan="3">
                                                    <label>Service No.</label>
                                                    <asp:TextBox ID="txtServiceNo" Width="155px" Enabled="false" runat="server" />
                                                    <asp:Button Style="margin-top: -1px; font-family: FontAwesome; padding: 1PX 12PX; height: 25PX; width: CALC(100% - 160PX);" ID="btnServiceNo" runat="server" class="btn btn-primary btn-xs add_click add_btn" Text="&#xf002;" OnClick="btnServiceNo_Click" />
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
                                                    <asp:TextBox ID="txtpartyGstIN" Enabled="false" Width="180px" runat="server" />
                                                </td>

                                                <td colspan="2">
                                                    <label>Net Amount</label>
                                                    <asp:TextBox ID="txtInvoiceTotalAmount" Enabled="false" Width="120px" Text="0" CssClass="inpt Invoice_date" runat="server" />
                                                </td>
                                            </tr>

                                        </tbody>

                                    </table>
                                    <table class="bankreceipt_table3 table-bordered">
                                        <tbody>
                                            <%--     <tr>
                                                        <td colspan="2">
                                                            <label>Service No.</label>
                                                            <asp:TextBox ID="txtServiceNo" Enabled="false" runat="server" />
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Button Style="margin-top: 23px;" ID="btnServiceNo" runat="server" Width="100%" class="btn btn-primary btn-xs add_click add_btn" Text="Service No" OnClick="btnServiceNo_Click" />
                                                        </td>
                                                        <td colspan="3">
                                                            <label>Party Name</label>
                                                            <asp:TextBox ID="txtPartyName" Enabled="false" Width="250px" runat="server" />
                                                        </td>
                                                        <td colspan="3">
                                                            <label>Party Address</label>
                                                            <asp:TextBox ID="txtPartyAddress" Enabled="false" Width="275px" runat="server" />
                                                        </td>
                                                        <td colspan="1">
                                                            <label>Party GSTIN</label>
                                                            <asp:TextBox ID="txtpartyGstIN" Enabled="false" Width="155px" runat="server" />
                                                        </td>

                                                        <td colspan="1">
                                                            <label>Net Amount</label>
                                                            <asp:TextBox ID="txtInvoiceTotalAmount" Enabled="false" Width="100px" Text="0" CssClass="inpt Money" runat="server" />
                                                        </td>
                                                    </tr>--%>

                                            <tr>

                                                <td colspan="2">
                                                    <label class="inf_head">IDA Ref. No.</label>
                                                    <asp:TextBox ID="txtIDARefNo" runat="server" MaxLength="9"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <label class="inf_head">Received By</label>
                                                    <asp:DropDownList ID="ddlPayMode" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="Cheque" Text="Cheque" />
                                                        <asp:ListItem Value="UTR" Text="RTGS/NEFT" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblPayModeNo" runat="server">Cheque No.</asp:Label>
                                                    <asp:TextBox ID="txtReceivedNo" runat="server"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblPayModeDate" runat="server">Cheque Date</asp:Label>
                                                    <asp:TextBox ID="txtReceivedDate" MaxLength="10" placeholder=" DD/MM/YYYY" runat="server" CssClass="datepicker"></asp:TextBox>
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

                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-left">
                                            <asp:Button Text="Cancel" Enabled="false" ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="btn btn-warning" Visible="false" />
                                        </div>
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Update" class="btn btn-primary btn-space-right" Visible="true" ValidationGroup="btnSave" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
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

