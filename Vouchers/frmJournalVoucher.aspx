<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmJournalVoucher.aspx.cs" Inherits="frmJournalVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();
            $('#<%=txtVoucherDate.ClientID%>').focus(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });

            $('#<%=txtVoucherDate.ClientID%>').click(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });
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
                <h3 class="text-center head">Journal Voucher
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-xs-12">


                                    <table class="bankpayment_table1 table-bordered">
                                        <tr>
                                            <th colspan="100%" class="inf_head"></th>
                                        </tr>
                                        <tr>

                                            <th style="width: 10%; display: none;">
                                                <label>For Back Date Entry</label></th>

                                            <th style="width: 10%; display: none;">
                                                <label visible="false" runat="server" id="thVNo">Voucher Number</label></th>
                                            <th style="width: 10%">
                                                <label>Voucher Date</label></th>
                                            <th style="width: 17%" class="hidden">&nbsp;<label class="hidden">Last Voucher No.</label></th>
                                            <th style="width: 90%" id="thCCCode" runat="server" visible="false">&nbsp;<label>Cost Centre</label></th>
                                            <%--<th style="width: 17%"></th>--%>
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
                                                <asp:TextBox ID="txtVoucherDate" MaxLength="10" placeholder="DD/MM/YYYY" CssClass="datepicker" runat="server" />
                                            </td>
                                            <td style="width: 17%" class="hidden">
                                                <asp:TextBox ID="txtLastVoucherNo" CssClass="numberonly hidden" Enabled="false" placeholder=" Enter Last Voucher No" Style="width: 100%" runat="server" />


                                            </td>
                                            <td style="width: 90%" visible="false" id="tdCCCode" runat="server">
                                                <%--<asp:DropDownList ID="ddlCostCenter" runat="server" />--%>

                                                <asp:DropDownList ID="ddlCostCenter" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                            </td>
                                            <%--<td style="width: 17%"></td>--%>
                                        </tr>
                                    </table>

                                    <!-- content -->
                                    <%--   <table class="jv_voucher_date table-bordered">
                                        <tr class="inf_head">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="jv_voucher_date_col1">Voucher Date</td>
                                            <td class="jv_voucher_date_col2"></td>
                                            <td class="jv_voucher_date_col3"></td>
                                        </tr>
                                    </table>--%>
                                    <table class="jv_account_head table-bordered">

                                        <tr class="inf_head">
                                            <th class="c jv_account_head_col1">Account Head</th>
                                            <th class="c jv_account_head_col2">Invoice No</th>
                                            <th class="c jv_account_head_col3">Invoice Date</th>
                                            <th class="c jv_account_head_col4">Amount (In Rs.)</th>
                                            <th class="c jv_account_head_col5">Cr/Dr</th>
                                            <th class="c jv_account_head_col6"></th>
                                        </tr>
                                        <tr>
                                            <td class="jv_account_head_col1">
                                                <%--<asp:DropDownList ID="ddlAccountHead" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged" Width="250px" CssClass="chzn-select" runat="server" style="height:22px"></asp:DropDownList>--%>
                                                <%--<cc1:ComboBox
                                                    ID="ddlAccountHead"
                                                    CssClass="inpt autoCopleteBox relative_gt"
                                                    runat="server" Width="250px"
                                                    AutoPostBack="true" placeholder="p"
                                                    OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged"
                                                    AutoCompleteMode="SuggestAppend"
                                                    CaseSensitive="False"
                                                    ItemInsertLocation="Append"
                                                    Style="text-transform: uppercase">
                                                </cc1:ComboBox>--%>

                                                <asp:DropDownList ID="ddlAccountHead" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                            </td>
                                            <td class="jv_account_head_col2">
                                                <asp:TextBox ID="txtInVoiceNo" CssClass="inpt AlphaNum" MaxLength="16" Style="width: 100%" runat="server" />
                                            </td>
                                            <td class="jv_account_head_col3">
                                                <asp:TextBox ID="txtInvoiceDate" CssClass="inpt datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                            </td>
                                            <td class="jv_account_head_col4">
                                                <asp:TextBox ID="txtAmount" MaxLength="13" CssClass="Money" runat="server" />
                                            </td>
                                            <td class="jv_account_head_col5">
                                                <asp:DropDownList ID="ddlDrCr" CssClass="inpt" Style="width: 100%; height: 27px;" runat="server">
                                                    <asp:ListItem Text="Cr" />
                                                    <asp:ListItem Text="Dr" />
                                                </asp:DropDownList>
                                            </td>
                                            <td class="jv_account_head_col6 c">
                                                <asp:Button ID="btnAdd" Text="Add" CssClass="btn btn-sxs btn-primary" OnClick="btnAdd_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hfISGL" runat="server" />
                                    <style>
                                        .marginTop {
                                            margin-top: 2px;
                                        }
                                    </style>
                                    <asp:GridView ID="grdJournalVoucher" CssClass="jv_account_head marginTop table-bordered" AutoGenerateColumns="false" runat="server" ShowHeader="false" OnRowCommand="grdJournalVoucher_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="jv_account_head_col1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountHeadID" Text='<%#Eval("AccHeadValue") %>' CssClass="hidden" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAccountHead" Style="width: 100%; height: 27px;" Text='<%#Eval("AcctHeadText") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="jv_account_head_col2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" Style="width: 100%" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="jv_account_head_col3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceDate" Style="width: 100%" Text='<%#Eval("InvoiceDate") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="jv_account_head_col4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="jv_account_head_col5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRCR" Text='<%#Eval("DrCr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="jv_account_head_col6 c">
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
                                    <table class="jv_narration table-bordered">
                                        <tr>
                                            <td class="jv_narration_col1">IDA Ref. No.</td>
                                            <td class="jv_narration_col2"><asp:TextBox ID="txtIDARefNo" MaxLength="9" placeholder="IDA Ref. No." style="width: 60%;" runat="server"></asp:TextBox></td>
                                            <td class="jv_narration_col3 r">Dr Amount </td>
                                            <td class="jv_narration_col4">
                                                <asp:TextBox ID="txtDrAmount" MaxLength="13" Enabled="false" CssClass="Money" runat="server" />
                                                <td class="jv_narration_col5 r">Cr Amount </td>
                                                <td class="jv_narration_col6">
                                                    <asp:TextBox ID="txtCrAmount" MaxLength="13" Enabled="false" CssClass="Money" runat="server" />
                                        </tr>
                                        <tr>
                                            <td class="jv_narration_col1">Narration</td>
                                            <td class="jv_narration_col2" colspan="5">
                                                <asp:TextBox ID="txtNarration" placeholder="Enter Narration" CssClass="text-uppercase" MaxLength="120" Width="70%" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- content -->
                                </div>
                            </div>
                            <asp:HiddenField ID="hfLastVoucherDate" runat="server" />
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" class="btn btn-primary btn-space-right" />
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

    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                    $('.chzn-search input[type="text"]').addClass('form-control');
                    $('.chzn-single').addClass('form-control');
                }
            });
        }
    </script>
</asp:Content>

