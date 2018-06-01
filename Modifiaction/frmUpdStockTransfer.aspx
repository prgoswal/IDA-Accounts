<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmUpdStockTransfer.aspx.cs" Inherits="Modifiaction_frmUpdStockTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .tdb0 tr td {
            border: 0 !important;
        }

        .thp0 tr th {
            padding: 0px 0px !important;
        }

        .grid_collapse_container {
            width: 1000px;
            clear: both;
        }

        .grid_collapse_opener {
            width: 162px;
            float: left;
        }

        .grid_collapse {
            width: 600px;
            float: left;
        }

        td div.relative_gt {
            top: 0 !important;
        }

        .align_with_combobox td:not(.ajax__combobox_textboxcontainer) input, .align_with_combobox td:not(.ajax__combobox_textboxcontainer) select {
            margin-top: 0px;
        }

        .align_with_combobox td {
            vertical-align: top !important;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">UPDATE STOCK TRANSFER
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="panel panel-default pmb0">
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <asp:Panel CssClass="input-group col-xs-3" runat="server">
                                            <asp:TextBox ID="txtSearchVoucher" MaxLength="7" CssClass="form-control numberonly" placeholder="Search Voucher No." runat="server" />
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </asp:Panel>
                                        <br />
                                        <table class="table table-voucher gstr_sales_invoice table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="4" class="inf_head">Invoice Details</th>
                                                </tr>
                                                <tr>
                                                    <th class="ti_col1">Transfer No.</th>
                                                    <th class="ti_col2">Transfer Date</th>
                                                    <th class="ti_col3">Transfer From</th>
                                                    <th class="ti_col4">Transfer To</th>
                                                    <%--<th class="ti_col5">GSTIN</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="ti_col1">
                                                        <asp:TextBox ID="txtTransferNo" MaxLength="16" Enabled="false" CssClass="text-right AlphaNum" placeholder="Transfer No." runat="server"></asp:TextBox></td>

                                                    <td class="ti_col2">
                                                        <asp:TextBox ID="txtTransferDate" CssClass="datepicker" placholder="DD/MM/YYYY" MaxLength="10" Style="width: 100%" runat="server" /></td>

                                                    <td class="ti_col3">
                                                        <asp:DropDownList ID="ddlTransferFrom" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlTransferFrom_SelectedIndexChanged" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ti_col4">
                                                        <asp:DropDownList ID="ddlTransferTo" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlTransferTo_SelectedIndexChanged" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <%--<td class="ti_col5">
                                                        <asp:DropDownList ID="ddlGSTINNO" Enabled="false" runat="server">
                                                        </asp:DropDownList>
                                                    </td>--%>
                                                </tr>
                                                <tr style="font-size: 10px;">
                                                    <td class="ti_col1">&nbsp;</td>
                                                    <td class="ti_col2">&nbsp;</td>
                                                    <td class="ti_col3 text-center">
                                                        <small id="smTransferFromStateCode" runat="server" visible="false">State Code =
                                                            <asp:Label ID="lblTransferFromStateCode" Visible="false" runat="server"></asp:Label>
                                                        </small>
                                                    </td>
                                                    <td class="ti_col4 text-center">
                                                        <small id="smTransferToStateCode" runat="server" visible="false">State Code =
                                                            <asp:Label ID="lblTransferToStateCode" Visible="false" runat="server"></asp:Label>
                                                        </small>
                                                    </td>
                                                    <%--<td class="ti_col5">&nbsp;</td>--%>
                                                </tr>

                                                <tr id="trCCCode" runat="server" visible="false">
                                                    <td class="ti_col1">&nbsp;</td>

                                                    <td class="ti_col2">&nbsp;</td>

                                                    <td class="ti_col3">Cost Centre Transfer From    
                                                        <asp:DropDownList ID="ddlCCCodeFrom" Enabled="false" AutoPostBack="true" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ti_col4">Cost Centre Transfer To    
                                                        <asp:DropDownList ID="ddlCCCodeTo" Enabled="false" AutoPostBack="true" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-xs-12 item">
                                        <table class="table table-voucher table-bordered table-form gstr_sales_item tdb0">
                                            <thead>
                                                <tr>
                                                    <th colspan="100%" class="inf_head">Item Details</th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 25%;" rowspan="2">Item Name</th>
                                                    <th colspan="2">Primary</th>
                                                    <th colspan="2">Secondary</th>
                                                    <th style="width: 10%;" rowspan="2">Rate</th>
                                                    <th style="width: 10%;" rowspan="2">Amount</th>
                                                    <th style="width: 20%;" rowspan="2">Remark's</th>
                                                    <th style="width: 3%;" rowspan="2"></th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 8%;">Qty</th>
                                                    <th style="width: 8%;">Unit</th>
                                                    <th style="width: 8%;">Qty</th>
                                                    <th style="width: 8%;">Unit</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="align_with_combobox">
                                                    <td>
                                                        <cc1:ComboBox ID="ddlItemName" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase; margin-top: 1px"></cc1:ComboBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQty" MaxLength="8" CssClass="Decimal4" runat="server" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlUnitName" Enabled="false" runat="server"></asp:DropDownList></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMinorQty" Enabled="false" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMinorUnitName" Enabled="false" runat="server"></asp:DropDownList></td>
                                                    <td>
                                                        <asp:TextBox ID="txtItemRate" Enabled="false" MaxLength="13" runat="server" Text="0" CssClass="Money" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtItemTaxableAmt" MaxLength="13" Enabled="false" runat="server" CssClass="Money" Text="0"></asp:TextBox></td>
                                                    <td class="ifpa " id="dtTax" runat="server">
                                                        <asp:TextBox ID="txtItemRemark" MaxLength="145" runat="server" placeholder="Enter Remark..." /></td>
                                                    <td>
                                                        <asp:Button ID="btnAddItemDetail" Enabled="false" CssClass="btn btn-xs btn-primary add_btn" Text="Add" OnClick="btnAddItemDetail_Click" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <style>
                                        .first_tr_hide tbody tr:first-child {
                                            display: none;
                                        }

                                        .hide_my_pdosi + tr {
                                            display: none;
                                        }

                                        .first_tr_hide tr td:first-child {
                                            display: none;
                                        }
                                    </style>
                                    <div class="col-xs-12">
                                        <asp:GridView ID="grdItemDetails" HeaderStyle-CssClass="hidden" CssClass="table table-voucher table-bordered table-form gstr_sales_item first_tr_hide" OnRowCommand="grdItemDetails_RowCommand" OnRowDataBound="grdItemDetails_RowDataBound" AutoGenerateColumns="false" runat="server" ShowHeader="True">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th colspan="100%" class="inf_head">Item Details</th>
                                                        </tr>
                                                        <tr>
                                                            <th style="width: 25%" rowspan="2">Item Name</th>
                                                            <th colspan="2">Primary</th>
                                                            <th colspan="2">Secondary</th>
                                                            <th style="width: 10%" rowspan="2">Rate</th>
                                                            <th style="width: 10%" rowspan="2">Amount</th>
                                                            <th style="width: 20%" rowspan="2">Remark's</th>
                                                            <th style="width: 3%" rowspan="2"></th>
                                                        </tr>
                                                        <tr class="hide_my_pdosi">
                                                            <th style="width: 8%">Qty</th>
                                                            <th style="width: 8%">Unit</th>
                                                            <th style="width: 8%">Qty</th>
                                                            <th style="width: 8%">Unit</th>
                                                        </tr>

                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemID" Text='<%#Eval("ItemID") %>' CssClass="hidden" runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemName" Text='<%#Eval("ItemName") %>' CssClass="txtFontSize" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Text='<%#Eval("ItemQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitID" Text='<%#Eval("ItemUnitID") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblUnit" Text='<%#Eval("ItemUnit") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinorQty" Text='<%#Eval("ItemMinorQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinorUnitID" Text='<%#Eval("ItemMinorUnitID") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblMinorUnit" Text='<%#Eval("ItemMinorUnit") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemRate" Text='<%#Eval("ItemRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemTaxableAmt" Text='<%#Eval("NetAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemRemark" Text='<%#Eval("ItemRemark") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="EditRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <style>
                                        .mb10 {
                                            margin-bottom: 10px;
                                        }

                                        .point {
                                            cursor: pointer;
                                        }

                                        .amount_table_right {
                                            width: 35%;
                                            float: right;
                                        }

                                        .left_additional {
                                            width: 65%;
                                            float: left;
                                        }
                                    </style>
                                    <div class="col-xs-12">
                                        <%--<div class="left_additional">
                                            <div class="col-sm-12 mb10 grid_collapse_container">
                                                <div class="grid_collapse_opener">
                                                    <asp:LinkButton ID="btnShowTransport" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowTransport_Click" runat="server"> &#xf067; Transportation Details</asp:LinkButton>
                                                </div>
                                                <asp:Panel ID="pnlTransport" runat="server" Visible="false" CssClass="grid_collapse">
                                                    <table class="table-bordered" style="width: 595px">
                                                        <tr class="inf_head hidden">
                                                            <td colspan="100%" class="checkbox_container">
                                                                <asp:CheckBox ID="CbTransDetail" OnCheckedChanged="CbTransDetail_CheckedChanged" AutoPostBack="true" Text="Transportation Details Available" runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%">
                                                                <asp:DropDownList Enabled="false" ID="ddlTansportID" runat="server"></asp:DropDownList></td>
                                                            <td width="20%">
                                                                <asp:TextBox Enabled="false" CssClass="datepicker" ID="txtTransportDate" runat="server" placeholder="Transportation Date" />
                                                            </td>
                                                            <td width="15%">
                                                                <asp:TextBox Enabled="false" ID="txtVehicleNo" MaxLength="15" runat="server" placeholder="Vehicle No." /></td>
                                                            <td width="30%">
                                                                <asp:TextBox Enabled="false" ID="txtTransportName" MaxLength="45" runat="server" placeholder="Transporter Name" /></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                            </div>
                                        </div>--%>
                                        <%--<div class="amount_table_right">
                                            <div class="pull-right">
                                                <table class="table table-voucher gstr_sales_amount table-bordered pull-right tdb0 thp0">
                                                    <thead>
                                                        <tr>
                                                            <th colspan="100%" class="inf_head">Amount</th>
                                                        </tr>
                                                        <tr>
                                                            <th class="amount_col3">Gross</th>
                                                            <th class="amount_col2">Tax</th>
                                                            <th class="amount_col2">Add/Less</th>
                                                            <th class="amount_col1">Net Amount</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td class="amount_col1">
                                                                <asp:TextBox ID="txtGrossAmt" Enabled="false" runat="server" placeholder="Net" CssClass="Money"></asp:TextBox></td>
                                                            <td class="amount_col2">
                                                                <asp:TextBox ID="txtTaxAmt" Enabled="false" runat="server" placeholder="Tax" CssClass="Money"></asp:TextBox></td>
                                                            <td class="amount_col2">
                                                                <asp:TextBox ID="txtAddLessAmt" Enabled="false" runat="server" placeholder="Add/Less" CssClass="Money"></asp:TextBox></td>
                                                            <td class="amount_col3">
                                                                <asp:TextBox ID="txtNetAmt" Enabled="false" runat="server" placeholder="Gross" CssClass="Money"></asp:TextBox></td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                                

                                            </div>
                                        </div>--%>
                                        <table class="table table-voucher gstr_sales_narration table-bordered pull-right">
                                            <tbody>
                                                <tr>
                                                    <td class="col1" style="font-weight: bold">Narration</td>
                                                    <td class="col2">
                                                        <asp:TextBox ID="txtNarration" placeholder="Enter Narration" CssClass="text-uppercase" MaxLength="120" Width="70%" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <div class="row">
                                        <div class="col-xs-12" style="margin-top: 5px;">


                                            <div class="pull-left">

                                                <asp:Button ID="btnCancel" Enabled="false" Text="Cancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" Visible="true" />
                                            </div>
                                            <div class="pull-right">
                                                <div class="error_div ac_hidden">
                                                    <div class="alert alert-danger error_msg"></div>
                                                </div>
                                                <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                <asp:Button ID="btnUpdate" Text="Update" Enabled="false" CssClass="btn btn-primary" OnClick="btnUpdate_Click" OnClientClick="return Validation()" runat="server" Visible="true" />
                                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hfHSNSACCode" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
                            <div class="panel panel-primary bodyContent" style="width: 30%; padding: 0">
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
                                        <asp:TextBox ID="txtCancelReason" AutoPostBack="true" CssClass="text-uppercase" MaxLength="45" Visible="false" placeholder="Sales To" runat="server" TextMode="MultiLine" />

                                    </div>

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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
