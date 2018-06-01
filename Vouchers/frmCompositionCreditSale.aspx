<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmCompositionCreditSale.aspx.cs" Inherits="Vouchers_frmCompositionCreditSale" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();

            $('#collapse_tr').click(function () {

                var collRemark = document.getElementById('CollapseRemark');

                if (collRemark.style.visibility == "hidden") {
                    collRemark.style.visibility = "visible";

                } else {
                    collRemark.style.visibility = "hidden";
                }

            });

            $('#<%=txtSalesto.ClientID%>').blur(function (e) {

                var id = '#' + this.id;
                if (this.value == "") {
                    PopOverError(id, 'top', 'Enter Sales To.');
                    $(this).focus();
                    event.preventDefault();
                } else {
                    $(id).popover('destroy');
                }
            });
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>

            <div class="content-wrapper">

                <h3 class="text-center head">Bill Of Supply 
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>

                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">

                                    <table class="table table-voucher gstr_sales_invoice table-bordered tdb0 thp0 ">
                                        <thead>
                                            <tr>
                                                <th colspan="6" class="inf_head">Invoice Details</th>
                                            </tr>
                                            <tr>
                                                <th class="ti_col3">Income Head</th>
                                                <th class="ti_col6">Invoice Series</th>
                                                <th class="ti_col1">Invoice No.</th>
                                                <th class="ti_col2">Invoice Date</th>
                                                <th class="ti_col4">Sales to</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="align_with_combobox">
                                                <td class="ti_col3">
                                                    <%--<cc1:ComboBox AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ddlIncomeHead_SelectedIndexChanged" 
                                                        CssClass="relative_gt" ID="ddlIncomeHead" runat="server" Width="250px" CaseSensitive="False" 
                                                        Style="text-transform: uppercase">
                                                    </cc1:ComboBox>--%>
                                                    <asp:DropDownList AutoPostBack="true" ID="ddlIncomeHead" runat="server" OnSelectedIndexChanged="ddlIncomeHead_SelectedIndexChanged" />
                                                </td>
                                                <td class="ti_col6">
                                                    <asp:DropDownList ID="ddlInvoiceSeries" Enabled="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlInvoiceSeries_SelectedIndexChanged" />

                                                    <asp:TextBox ID="txtInvoiceSeries" CssClass="text-uppercase Series" MaxLength="10" Enabled="false" placeholder="Invoice Series" runat="server" />

                                                </td>
                                                <td class="ti_col1">
                                                    <asp:TextBox ID="txtinvoiceNo" MaxLength="7" runat="server" placeholder="Invoice No." CssClass="numberonly"></asp:TextBox></td>

                                                <td class="ti_col2">
                                                    <asp:TextBox ID="txtInvoiceDate" CssClass="datepicker" placeholder="DD/MM/YYYY" MaxLength="10" Style="width: 100%" runat="server" /></td>

                                                <td class="ti_col4">
                                                    <cc1:ComboBox ID="ddlSalesto" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesto_SelectedIndexChanged1" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    <%--<asp:DropDownList ID="ddlSalesto" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesto_SelectedIndexChanged1" runat="server" />--%>
                                                    <asp:TextBox ID="txtSalesto" AutoPostBack="true" OnTextChanged="txtSalesto_TextChanged" CssClass="text-uppercase" MaxLength="45" Visible="false" placeholder="Sales To" runat="server" />
                                                </td>


                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="tblShippingDetail" runat="server" visible="true">
                                        <table class="table table-voucher gstr_sales_invoice_2 table-bordered tdb0 thp0">
                                            <thead>
                                                <tr>
                                                    <th colspan="7" style="background: #1c75bf; color: #fff">&nbsp</th>
                                                </tr>
                                                <tr>
                                                    <th class="ti_col1">Shipping Address</th>
                                                    <th class="ti_col2">Dispatch Location</th>
                                                    <th class="ti_col3">Order No.</th>
                                                    <th class="ti_col4">Order Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="align_with_combobox">
                                                    <td class="ti_col1">
                                                        <cc1:ComboBox CssClass="relative_gt" ID="ddlShippingAdd" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" runat="server"></cc1:ComboBox>
                                                        <%--<asp:DropDownList ID="ddlShippingAdd" runat="server"></asp:DropDownList>--%>
                                                        <asp:TextBox Visible="false" ID="txtShippingAdd" MaxLength="135" runat="server" />
                                                    </td>

                                                    <td class="ti_col2">
                                                        <asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>

                                                    <td class="ti_col3">
                                                        <asp:TextBox ID="txtorderNo" MaxLength="18" placeholder="Order No." runat="server"></asp:TextBox></td>

                                                    <td class="ti_col4">
                                                        <asp:TextBox ID="txtOrderDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />

                                                    </td>


                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <style>
                                        .all_discount_checkbox {display:block;margin:0 auto;user-select:none;}
                                        .all_discount_checkbox label,.all_discount_checkbox input[type="checkbox"] {cursor:pointer}
                                    </style>
                                    <table class="table table-voucher table-bordered table-form gstr_sales_item tdb0 thp0">
                                        <thead>

                                            <tr>
                                                <th colspan="100%" class="inf_head">Item Detail</th>
                                            </tr>
                                            <tr>
                                                <th class="w22" rowspan="2">Item Name</th>
                                                <th colspan="2">Primary</th>
                                                <th colspan="2">Secondary</th>
                                                <th class="w5" rowspan="2">Free</th>
                                                <th class="w5" rowspan="2">Rate</th>
                                                <th class="w5" rowspan="2">Net Amount</th>
                                                <%--<th class="w8" rowspan="2">Discount</th>--%>

                                                <th class="w8" rowspan="2">Discount 
                                                    <span class="all_discount_checkbox">
                                                        <label for="ContentPlaceHolder1_chkDiscount">

                                                            <asp:CheckBox ID="chkDiscount" OnCheckedChanged="chkDiscount_CheckedChanged" AutoPostBack="true" runat="server" />
                                                            All</label></span>
                                                </th>
                                                <th class="w6" rowspan="2">Amount</th>
                                                <th rowspan="2"></th>
                                                <tr>
                                                    <th class="w6">Qty</th>
                                                    <th class="w7">Unit</th>
                                                    <th class="w6">Qty</th>
                                                    <th class="w7">Unit</th>
                                                </tr>
                                        </thead>

                                        <tbody>
                                            <tr class="align_with_combobox">
                                                <td>
                                                    <cc1:ComboBox ID="ddlItemName" runat="server" Enabled="false" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" OnTextChanged="txtQty_TextChanged" MaxLength="8" CssClass="Decimal4" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUnitName" Enabled="false" runat="server"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txtMinorUnitQty" MaxLength="8" CssClass="Decimal4" Enabled="false" runat="server" /></td>
                                                <td>
                                                    <asp:DropDownList Enabled="false" ID="ddlMinorUnit" runat="server" /></td>
                                                <td>
                                                    <asp:TextBox ID="txtFree" MaxLength="8" CssClass="Decimal4" AutoPostBack="true" runat="server" Text="0" OnTextChanged="txtFree_TextChanged"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtRate" MaxLength="7" runat="server" CssClass="Money" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtItemAmt" MaxLength="7" runat="server" CssClass="Money" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDiscount" OnTextChanged="txtDiscount_TextChanged" CssClass="Money w60 DiscountJS" Style="float: left;" MaxLength="7" runat="server" AutoPostBack="true" />
                                                    <asp:DropDownList ID="ddlDiscount" CssClass="w40 DiscountJS" Style="float: left;" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Selected="True" Text="%" />
                                                        <asp:ListItem Value="0" Text="Rs." />
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtItemTaxableAmt" MaxLength="7" runat="server" CssClass="Money" Enabled="false"></asp:TextBox></td>

                                                <td style="position: relative; border-bottom-color: transparent; border-right-color: transparent; text-align: center; width: 6%">

                                                    <a href="javascript:void(0)" id="collapse_tr" class="sctooltip"><i class="fa fa-cog"></i><span class="sctooltiptext">Add Item Remark</span></a>
                                                    <asp:Button ID="btnAddItemDetail" runat="server" CssClass="btn btn-xs  btn-primary " Text="Add" OnClick="btnAddItemDetail_Click" />
                                </div>
                                </td>

                                            </tr>

                                        </tbody>
                                    </table>
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

                                <asp:GridView ID="gvItemDetail" CssClass="gstr_sales_item table-bordered first_tr_hide" AutoGenerateColumns="false" runat="server" OnRowCommand="gvItemDetail_RowCommand" OnRowDataBound="gvItemDetail_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <tr class="inf_head">
                                                    <th rowspan="2" class="w13">Item Name</th>
                                                    <th rowspan="2" class="w12">HSNSACCode</th>
                                                    <th colspan="2">Primary</th>
                                                    <th colspan="2">Secondary</th>
                                                    <th rowspan="2" class="w5">Free</th>
                                                    <th rowspan="2" class="w5">Rate</th>
                                                    <th rowspan="2" class="w5">Amount</th>
                                                    <th rowspan="2" class="w5">Discount</th>

                                                    <th rowspan="2" class="w4">Discount Type</th>
                                                    <th rowspan="2" class="w4">Discount Amount</th>

                                                    <th rowspan="2" class="w5">Taxable</th>
                                                    <%--   <th rowspan="2" class="w5">PA</th>
                                                         <th rowspan="2" class="w5">Tax Rate</th>
                                                         <th rowspan="2" class="w5">CGST</th>
                                                         <th rowspan="2" class="w5">SGST</th>
                                                         <th rowspan="2" class="w5">IGST</th>
                                                         <th rowspan="2" class="w5">CESS</th>
                                                         <th rowspan="2" class="w5">ISD</th>--%>
                                                    <th rowspan="2" class="w5"></th>
                                                </tr>
                                                <tr class="hide_my_pdosi inf_head">
                                                    <th class="w5">Qty</th>
                                                    <th class="w5">Unit</th>
                                                    <th class="w5">Qty</th>
                                                    <th class="w5">Unit</th>
                                                </tr>
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ItemName" />
                                        <asp:BoundField DataField="HSNSACCode" />
                                        <asp:BoundField DataField="ItemQty" />
                                        <asp:BoundField DataField="ItemUnit" />
                                        <asp:BoundField DataField="ItemMinorQty" />
                                        <asp:BoundField DataField="ItemMinorUnit" />
                                        <asp:BoundField DataField="FreeQty" />
                                        <asp:BoundField DataField="ItemRate" />
                                        <asp:BoundField DataField="ItemAmount" />

                                        <asp:BoundField DataField="DiscountValue" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscountType" Text='<%#Eval("DiscountType") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DiscountAmt" />

                                        <asp:BoundField DataField="NetAmt" />

                                        <asp:TemplateField ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn btn-danger btn-sxs" CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#1c75bf" ForeColor="#FAFCFD" />
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

                                <div class="left_additional">
                                    <div class="col-sm-12 mb10 grid_collapse_container">
                                        <div class="grid_collapse_opener">
                                            <asp:LinkButton data-toggle="collapse" data-target="#demo" ID="btnShowTransport" OnClick="btnShowTransport_Click" CssClass="btn-asp-text p1012 point fa-asp text-left" runat="server"> &#xf067; Transportation Details</asp:LinkButton>
                                        </div>
                                        <asp:Panel ID="pnlTransport" runat="server" Visible="false" CssClass="grid_collapse">
                                            <table class="table-bordered" style="width: 600px">
                                                <tr class="inf_head hidden">
                                                    <td colspan="100%" class="checkbox_container">
                                                        <asp:CheckBox ID="CbTransDetail" AutoPostBack="true" Text="Transportation Details Available" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td width="15%">
                                                        <asp:DropDownList Enabled="false" ID="ddlTansportID" runat="server"></asp:DropDownList></td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" CssClass="datepicker" ID="txtTransportDate" runat="server" placeholder="Transportation Date" />
                                                        <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTransportDate" Mask="99/99/9999 99:99:99" MaskType="DateTime" UserDateFormat="DayMonthYear" MessageValidatorTip="true"></cc1:MaskedEditExtender>--%>
                                                    </td>
                                                    <td width="15%">
                                                        <asp:TextBox Enabled="false" ID="txtVehicleNo" MaxLength="15" runat="server" placeholder="Vehicle No." /></td>
                                                    <td width="30%">
                                                        <asp:TextBox Enabled="false" ID="txtTransportName" MaxLength="45" runat="server" placeholder="Transporter Name" /></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </div>

                                    <div class="col-sm-12 mb10 grid_collapse_container">
                                        <div class="grid_collapse_opener">
                                            <asp:LinkButton ID="btnShowFreeItem" CssClass="btn-asp-text p1012 point fa-asp text-left" runat="server" OnClick="btnShowFreeItem_Click"> &#xf067; Additional Free Items</asp:LinkButton>
                                        </div>
                                        <asp:Panel ID="divFree" runat="server" Visible="false" CssClass="grid_collapse">
                                            <table class="table table-voucher gstr_sales_other table-bordered tbl_Free tdb0 thp0" id="Table4" runat="server" style="width: 600px">
                                                <tbody>
                                                    <tr class="align_with_combobox">
                                                        <td class="other_col1">
                                                            <cc1:ComboBox ID="ddlFreeItemName" OnSelectedIndexChanged="ddlFreeItemName_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                        </td>
                                                        <td class="other_col2">
                                                            <asp:DropDownList ID="ddlFreeUnit" Enabled="false" runat="server" /></td>
                                                        <td class="other_col3 r">
                                                            <asp:TextBox ID="txtFreeQty" MaxLength="8" placeholder="Qty" CssClass="Decimal4" runat="server" /></td>
                                                        <td class="w9 c" style="width: 10px">
                                                            <asp:Button ID="btnFreeAdd" OnClick="btnFreeAdd_Click" CssClass="btn btn-sxs btn-primary btncss" Text="Add" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <asp:GridView ID="gvFreeItem" CssClass="table table-voucher gstr_sales_other table-bordered tbl_Free" ShowHeader="false" OnRowCommand="gvFreeItem_RowCommand" AutoGenerateColumns="false" runat="server" Style="width: 600px">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-CssClass="other_col1" DataField="ItemName" />
                                                    <asp:BoundField ItemStyle-CssClass="other_col2" DataField="ItemUnit" />
                                                    <asp:BoundField ItemStyle-CssClass="other_col3" DataField="ItemQty" />
                                                    <asp:TemplateField ItemStyle-CssClass="w9 c" ItemStyle-Width="10px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger add_btn" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-12 mb10 grid_collapse_container">
                                        <div class="grid_collapse_opener">
                                            <asp:LinkButton ID="btnShoOtherCharge" CssClass="btn-asp-text p1012 point fa-asp text-left" runat="server" OnClick="btnShoOtherCharge_Click"> &#xf067; Other Charges</asp:LinkButton>
                                        </div>
                                        <asp:Panel ID="pnlOtherCharge" runat="server" Visible="false" CssClass="grid_collapse">
                                            <table class="table table-voucher gstr_sales_other table-bordered tdb0 thp0" style="width: 600px">

                                                <tbody>
                                                    <tr class="align_with_combobox">
                                                        <td class="other_col1">
                                                            <cc1:ComboBox ID="ddlHeadName" runat="server" Width="250px" placeholder="Head Name" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                        </td>
                                                        <td class="other_col2">
                                                            <asp:DropDownList ID="ddlAddLess" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem>Add</asp:ListItem>
                                                                <asp:ListItem>Less</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td class="other_col3 r">
                                                            <asp:TextBox CssClass="Money" ID="txtOtherChrgAmount" MaxLength="7" runat="server" placeholder="Amount"></asp:TextBox></td>

                                                        <td class="other_col4" style="width: 10px">
                                                            <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" CssClass=" btn btn-sxs  btn-primary" Text="Add" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                            <%--GridView For Other Charge--%>
                                            <asp:GridView ID="gvotherCharge" CssClass="table table-voucher gstr_sales_other table-bordered" AutoGenerateColumns="false" OnRowCommand="gvotherCharge_RowCommand" ShowHeader="false" runat="server" Style="width: 600px">
                                                <Columns>
                                                    <asp:BoundField DataField="SundriCode" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SundriHead" ItemStyle-CssClass="other_col1" />
                                                    <asp:BoundField DataField="SundriInd" ItemStyle-CssClass="other_col2" />
                                                    <asp:BoundField DataField="SundriAmt" ItemStyle-CssClass="other_col3" />
                                                    <asp:TemplateField ItemStyle-CssClass="other_col4" ItemStyle-Width="10px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </asp:Panel>
                                    </div>

                                    <div class="col-sm-12 mb10 grid_collapse_container">
                                        <div class="grid_collapse_opener p1012  point">
                                            <asp:LinkButton ID="btnShoBankDetail" OnClick="btnShoBankDetail_Click" runat="server"><i class="fa r fa-plus" aria-hidden="true"></i>Bank Details</asp:LinkButton>
                                        </div>
                                        <div class="grid_collapse">
                                            <asp:Panel ID="pnlBankDetail" runat="server" Visible="false" CssClass="grid_collapse">
                                                <table class="table table-voucher gstr_sales_other  tbl_Charges tdb0" id="Table1" visible="true" runat="server">
                                                    <tbody>
                                                        <tr class="align_with_combobox">
                                                            <td class="w30">
                                                                <asp:TextBox ID="txtPartyBank" MaxLength="45" placeholder="Bank Name" runat="server" />
                                                            </td>
                                                            <td class="w30">
                                                                <asp:TextBox ID="txtPartyIFSC" MaxLength="25" placeholder="IFSC Code" runat="server" />
                                                            </td>

                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                    <asp:Panel ID="pnlParentAdvance" class="col-sm-12 mb10 grid_collapse_container" runat="server">
                                        <div class="grid_collapse_opener">
                                            <asp:LinkButton ID="btnShowPartyAdvance" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowPartyAdvance_Click" runat="server"> &#xf067; Advance Received</asp:LinkButton>
                                        </div>
                                        <asp:Panel ID="pnlPartyAdvance" runat="server" Visible="false" CssClass="grid_collapse">
                                            <asp:GridView ID="GvPartyAdvance" RowStyle-BackColor="" HeaderStyle-CssClass="inf_head" CssClass=" table-voucher gstr_sales_other table-bordered tbl_Free w91" OnRowDataBound="GvPartyAdvance_RowDataBound" ShowHeader="true" AutoGenerateColumns="false" runat="server" Style="width: 600px">
                                                <Columns>
                                                    <%--<asp:BoundField ItemStyle-CssClass="w10" DataField="PartyCode"   HeaderText="" />--%>
                                                    <asp:TemplateField ItemStyle-CssClass="w15 c" HeaderText="Voucher No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucharNo" Text='<%#Eval("VoucharNo") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-CssClass="w17 c" DataField="VoucharDate" HeaderText="Voucher Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:TemplateField ItemStyle-CssClass="w10 c" HeaderText="Tax Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-CssClass="w18 c" DataField="AdvAmount" HeaderText="Advance Amount" DataFormatString="{0:C}" />

                                                    <asp:TemplateField ItemStyle-CssClass="w10 c hidden" HeaderStyle-CssClass="hidden" HeaderText="Advance Remaining">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAdjAdvAmount" Text='<%#Eval("AdjAdvAmount","{0:C}") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField ItemStyle-CssClass="w18 c" DataField="AdjAdvAmount"   HeaderText="Advance Remaining" />--%>
                                                    <asp:TemplateField ItemStyle-CssClass="w5 c">
                                                        <ItemTemplate>
                                                            <asp:CheckBox Enabled="false" ID="cbPartyAdvance" OnCheckedChanged="cbPartyAdvance_CheckedChanged" AutoPostBack="true" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>




                                <div class="amount_table_right">

                                    <div class="pull-right">
                                        <table class="table table-voucher gstr_sales_amount table-bordered pull-right tdb0 thp0">
                                            <thead>
                                                <tr>
                                                    <th colspan="100%" class="inf_head">Amount</th>
                                                </tr>
                                                <tr>
                                                    <th class="amount_col3">Item </th>
                                                    <th class="amount_col3">Discount </th>
                                                    <th class="amount_col3">Taxable</th>
                                                    <th class="amount_col2">Add/Less</th>
                                                    <th class="amount_col1">Net </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>

                                                    <td class="amount_col1">
                                                        <asp:TextBox ID="txtItemAmount" Enabled="false" runat="server" placeholder="Item" CssClass="Money"></asp:TextBox></td>
                                                    <td class="amount_col1">
                                                        <asp:TextBox ID="txtDiscountAmount" Enabled="false" runat="server" placeholder="Discount" CssClass="Money"></asp:TextBox></td>
                                                    <td class="amount_col1">
                                                        <asp:TextBox ID="txtGross" Enabled="false" runat="server" placeholder="Net" CssClass="Money"></asp:TextBox></td>
                                                    <td class="amount_col2">
                                                        <asp:TextBox ID="txtAddLess" Enabled="false" runat="server" placeholder="Add/Less" CssClass="Money"></asp:TextBox></td>
                                                    <td class="amount_col3">
                                                        <asp:TextBox ID="txtNet" Enabled="false" runat="server" placeholder="Gross" CssClass="Money"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <table class="table table-voucher gstr_sales_narration table-bordered pull-right">

                                            <tbody>
                                                <tr>
                                                    <td class="col1" style="font-weight: bold">Narration</td>
                                                    <td class="col2">
                                                        <cc1:ComboBox ID="txtNarration" runat="server" MaxLength="70" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>

                                </div>
                            </div>

                        </div>
                        <div class="panel-footer">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="pull-right">
                                        <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                        <%--<cc1:MaskedEditValidator  CssClass="text-danger" ID="MaskedEditValidator1" runat="server" ControlToValidate="txtTransportDate" ControlExtender="MaskedEditExtender1" IsValidEmpty="true" EmptyValueMessage="Input Date and Time" InvalidValueMessage="Invalid Date And Time" ValidationGroup="a" ></cc1:MaskedEditValidator>--%>
                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" ValidationGroup="a" runat="server" Text="Save" class="btn btn-primary btn-space-right" />
                                        <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        <asp:LinkButton ID="btnCancelInvoice" runat="server" OnClick="btnCancelInvoice_Click" CssClass="btn btn-warning btn-cancel-invoice"><b>Cancel<br />Invoice</b></asp:LinkButton>
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
            </div>
            <asp:HiddenField ID="hfSaleInvoiceManually" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <%--<asp:PostBackTrigger ControlID="btnYes" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>

