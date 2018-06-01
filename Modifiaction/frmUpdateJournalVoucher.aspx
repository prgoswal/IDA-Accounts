<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmUpdateJournalVoucher.aspx.cs" Inherits="Modifiaction_frmUpdateJournalVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
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
                <h3 class="text-center head">Update Journal Voucher
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-sm-12">

                                    <table class="search_updCash_table table-bordered">
                                        <tbody>
                                            <tr>
                                                <td>Voucher&nbsp;No-
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVoucherNo" placeholder="Voucher No." MaxLength="8" CssClass="numberonly" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary btn-sxs pull-right" Style="margin-top: 1px" OnClick="btnSearch_Click" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-xs-12">
                                    <!-- content -->
                                    <table class="jv_voucher_date table-bordered">
                                        <tr class="inf_head">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="jv_voucher_date_col1">Voucher Date</td>
                                            <td class="jv_voucher_date_col2">
                                                <asp:TextBox ID="txtVoucherDate" MaxLength="10" placeholder="DD/MM/YYYY" CssClass="datepicker" runat="server" />
                                            </td>

                                            <td style="width: 17%" id="thCCCode" runat="server" visible="false" class="jv_voucher_date_col1">&nbsp;Cost Centre</td>

                                            <td style="width: 61%" visible="false" id="tdCCCode" runat="server">
                                                <%--<asp:DropDownList ID="ddlCostCenter" runat="server" />--%>
                                                <asp:DropDownList ID="ddlCostCenter" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="jv_voucher_date_col3" style="border: 0; width: 0"></td>
                                        </tr>
                                    </table>
                                    <table class="jv_account_head table-bordered">

                                        <tr class="inf_head">
                                            <th class="c jv_account_head_col1">Account Head</th>
                                            <th class="c jv_account_head_col2">Invoice No</th>
                                            <th class="c jv_account_head_col3">Invoice Date</th>
                                            <th class="c jv_account_head_col4">Amount</th>
                                            <th class="c jv_account_head_col5">Cr/Dr</th>
                                            <th class="c jv_account_head_col6"></th>
                                        </tr>
                                        <tr>
                                            <td class="jv_account_head_col1">
                                                <%--<cc1:ComboBox ID="ddlAccountHead" CssClass="inpt autoCopleteBox relative_gt" runat="server" Width="250px" AutoPostBack="true" placeholder="p" OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged"
                                                    AutoCompleteMode="SuggestAppend" CaseSensitive="False" Enabled="false" ItemInsertLocation="Append" Style="text-transform: uppercase">
                                                </cc1:ComboBox>--%>

                                                <asp:DropDownList ID="ddlAccountHead" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                            </td>
                                            <td class="jv_account_head_col2">
                                                <asp:TextBox ID="txtInVoiceNo" CssClass="inpt AlphaNum" Enabled="false" MaxLength="16" Style="width: 100%" runat="server" />
                                            </td>
                                            <td class="jv_account_head_col3">
                                                <asp:TextBox ID="txtInvoiceDate" CssClass="inpt datepicker" Enabled="false" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                            </td>
                                            <td class="jv_account_head_col4">
                                                <asp:TextBox ID="txtAmount" MaxLength="13" CssClass="Money" Enabled="false" runat="server" />
                                            </td>
                                            <td class="jv_account_head_col5">
                                                <asp:DropDownList ID="ddlDrCr" CssClass="inpt" Enabled="false" Style="width: 100%; height: 27px;" runat="server">
                                                    <asp:ListItem Text="Cr" Value="0" />
                                                    <asp:ListItem Text="Dr" Value="1" />
                                                </asp:DropDownList>
                                            </td>
                                            <td class="jv_account_head_col6 c">
                                                <asp:Button ID="btnAdd" Text="Add" Enabled="false" CssClass="btn btn-sxs btn-primary" OnClick="btnAdd_Click" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hfISGL" runat="server" />
                                    <style>
                                        .marginTop {
                                            margin-top: 2px;
                                        }
                                    </style>

                                    <asp:GridView ID="grdUpdJournalVoucher" CssClass="jv_account_head table-bordered" OnRowCommand="grdUpdJournalVoucher_RowCommand"
                                        OnRowDataBound="grdUpdJournalVoucher_RowDataBound" AutoGenerateColumns="false" ShowHeader="false" runat="server">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="other_col1">
                                                <ItemTemplate>
                                                    <table class="table-bordered">
                                                        <tr>
                                                            <td class="jv_account_head_col1">
                                                                <asp:Label ID="lblAccCode" Text='<%#Eval("AccCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                                                <asp:Label ID="lblAccName" Text='<%#Eval("AccName") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td class="jv_account_head_col2">
                                                                <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td class="jv_account_head_col3">
                                                                <asp:Label ID="lblInvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td class="jv_account_head_col4">
                                                                <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td class="jv_account_head_col5">
                                                                <asp:Label ID="lblDRCR" Text='<%#Eval("DrCr") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td class="jv_account_head_col6 c">
                                                                <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="RowEdit" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn" runat="server"></asp:LinkButton>
                                                                <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger add_btn"
                                                                    runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table class="table-bordered">
                                                        <tr>
                                                            <td class="jv_account_head_col1">
                                                                <asp:Label ID="lblAccCode" Text='<%#Eval("AccCode") %>' CssClass="hidden" runat="server" />
                                                                <cc1:ComboBox ID="ddlAccountHead" CssClass="inpt autoCopleteBox relative_gt" runat="server" Width="250px" placeholder="p" AutoCompleteMode="SuggestAppend" CaseSensitive="False" ItemInsertLocation="Append" Style="text-transform: uppercase">
                                                                </cc1:ComboBox>
                                                            </td>
                                                            <td class="jv_account_head_col2">
                                                                <asp:TextBox ID="txtInvoiceNo" Text='<%#Eval("InvoiceNo") %>' CssClass="inpt numberonly" MaxLength="7" Style="width: 100%" runat="server" />
                                                            </td>
                                                            <td class="jv_account_head_col3">
                                                                <asp:TextBox ID="txtInvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' CssClass="inpt datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                            </td>
                                                            <td class="jv_account_head_col4">
                                                                <asp:TextBox ID="txtAmount" Text='<%#Eval("Amount") %>' MaxLength="13" CssClass="Money" runat="server" />
                                                            </td>
                                                            <td class="jv_account_head_col5">
                                                                <asp:DropDownList ID="ddlDrCr" CssClass="inpt" Style="width: 100%; height: 27px;" runat="server">
                                                                    <asp:ListItem Text="Cr" />
                                                                    <asp:ListItem Text="Dr" />
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="jv_account_head_col6 c">
                                                                <asp:LinkButton ID="btnSave" Text="Save" CommandName="RowUpdate" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn"
                                                                    runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">

                                        <div class="pull-left">
                                            <asp:Button Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="btn btn-warning" Enabled="false" Visible="true" />
                                        </div>
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnUpdate" OnClientClick="LoadActive()" OnClick="btnUpdate_Click" runat="server" Text="Update" class="btn btn-primary btn-space-right" Visible="true" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--  <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="modalPop" Visible="false">
                <div class="panel panel-primary bodyContent" style="width: 30%; padding: 0">
                    <div class="panel-heading">
                        <i class="fa fa-info-circle"></i>
                        Are You Sure for Cancel this Vouchar 
                    </div>
                    <br />
                    <div>
                        Reason for Cancel
                    <asp:TextBox ID="txtCancelReason" TextMode="MultiLine" MaxLength="99" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqtxtCancelReason" ControlToValidate="txtCancelReason" ErrorMessage="**" ForeColor="Red" Display="Dynamic" ValidationGroup="btnYes" />

                    </div>

                    <div class="panel-body">
                        <div class="text-right">
                            <asp:Button ID="btnYes" OnClick="btnYes_Click" ValidationGroup="btnYes" CssClass="btn btn-primary" Text="Yes" runat="server" />
                            <asp:Button ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>--%>


            <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
                <div class="panel panel-primary bodyContent" style="width: 30%; padding: 0; overflow: visible; max-height: initial">
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
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>

