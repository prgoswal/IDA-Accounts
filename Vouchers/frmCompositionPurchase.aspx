<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmCompositionPurchase.aspx.cs" Inherits="frmPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Datetime() {
            LoadBasic();
            ChoosenDDL();

            $('#collapse_tr').click(function () {
                var collRemark = document.getElementById('CollapseRemark');
                if (collRemark.style.visibility == "hidden") {
                    collRemark.style.visibility = "visible";
                } else {
                    collRemark.style.visibility = "hidden";
                }
                $('#<%=txtItemRemark.ClientID%>').focus();
            });

            $('#<%=txtPurchaseFrom.ClientID%>').blur(function (e) {

                var id = '#' + this.id;
                if (this.value == "") {
                    PopOverError(id, 'top', 'Enter Purchase From.');
                    $(this).focus();
                    event.preventDefault();
                } else {
                    $(id).popover('destroy');
                }

            });
            $('#ContentPlaceHolder1_ddlGstinNo_ddlGstinNo_TextBox').addClass("GSTIN text-uppercase");
        }
        $('#ContentPlaceHolder1_ddlGstinNo_ddlGstinNo_TextBox').addClass("GSTIN text-uppercase");
    </script>
    <style type="text/css">
        table {
            font-size: 13px !important;
        }

        .tblt1 .tblt1 {
            width: 1087px;
        }

        .tblt1 .ti_col2 {
            width: 488px;
        }

        .tblt1 .ti_col3 {
            width: 283px;
        }

        .tblt1 .ti_col4 {
            width: 80px;
        }

        .tblt1 .ti_col5 {
            width: 86px;
        }

        .tblt1 .ti_col6 {
            width: 50px;
        }

        .tblt1 .ti_col6 {
            width: 50px;
        }

        .tblt1 .ti_col6 {
            width: 50px;
        }

        .other_t {
            width: 520px;
        }

        .other_col1 {
            width: 160px;
        }

        .other_col2 {
            width: 160px;
        }

        .other_col3 {
            width: 160px;
        }

        .other_col4 {
            width: 30px;
        }

        .txtFontSize {
            font-size: 11px;
        }

        .invoice {
            float: right;
            font-size: 15px;
            color: chocolate;
            font-weight: bold;
        }

        .gstr_purchase_voucher .ti_col3 {
            width: 6.5%;
        }

        .gstr_purchase_voucher .ti_col6 {
            width: 25%;
        }

        .gstr_purchase_voucher .ti_col1 {
            width: 17%;
        }

        .gstr_purchase_voucher .ti_col2 {
            width: 6.5%;
        }

        .gstr_purchase_voucher .ti_col4 {
            width: 25%;
        }

        .gstr_purchase_voucher .ti_col5 {
            width: 12%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:HiddenField ID="hfLastVoucherDate" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(Datetime);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">PURCHASE VOUCHER
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">



                                <div class="col-xs-12">



                                    <table class="table table-voucher table-bordered table-form gstr_sales_item gstr_purchase_voucher" style="width: 50%;">
                                        <thead>
                                            <tr>
                                                <th colspan="100%" style="background: #1c75bf; color: #fff">&nbsp;</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 33%">For Back Date Entry</th>
                                                <th style="width: 33%">
                                                    <label visible="false" runat="server" id="thVNo">Voucher Number</label></th>
                                                <th style="width: 33%">Voucher Date</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <style>
                                                .checkbox_container_CashRec {
                                                    padding-left: 35px;
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
                                                <td>
                                                    <span class="checkbox_container_CashRec">
                                                        <asp:CheckBox ID="chkBankEntry" runat="server" OnCheckedChanged="chkBankEntry_CheckedChanged" AutoPostBack="true" Text="(If Yes- Tick Here)" /></span>
                                                </td>

                                                <td>
                                                    <span visible="false" runat="server" id="tdVNo">
                                                        <asp:TextBox ID="txtVoucherNo" MaxLength="10" placeholder="Voucher Number" Style="width: 100%" runat="server" CssClass="numberonly" /></span></td>
                                                <td>
                                                    <asp:TextBox ID="txtVoucherDate" MaxLength="10" CssClass="datepicker" placeholder="DD/MM/YYYY" runat="server" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table table-voucher table-bordered table-form gstr_sales_item gstr_purchase_voucher">
                                        <thead>
                                            <tr>
                                                <th colspan="100%" style="background: #1c75bf; color: #fff">Invoice Details</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 14%">Pay Mode</th>
                                                <%--<th style="width: 7%">Voucher Date</th>--%>
                                                <th style="width: 7%">Bill No.</th>
                                                <th style="width: 7%">Bill Date</th>
                                                <th style="width: 26%">Purchase From</th>
                                                <th style="width: 26%">Shipped From</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlPayMode" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                                                <%--<td>
                                                    <asp:TextBox ID="txtVoucherDate" MaxLength="10" CssClass="datepicker" placeholder="DD/MM/YYYY" runat="server" /></td>--%>
                                                <td>
                                                    <asp:TextBox ID="txtBillNo" MaxLength="16" placeholder="Bill No." runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtBillDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" /></td>
                                                <td>
                                                    <cc1:ComboBox ID="ddlPurchaseFrom" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPurchaseFrom_SelectedIndexChanged" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    <asp:TextBox ID="txtPurchaseFrom" AutoPostBack="true" CssClass="text-uppercase" Visible="false" runat="server" />
                                                </td>
                                                <td>
                                                    <cc1:ComboBox ID="ddlShippingAdd" CssClass="relative_gt" runat="server"></cc1:ComboBox>
                                                    <asp:TextBox ID="txtShippingAdd" MaxLength="135" Visible="false" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="tblShippingDetail" runat="server">
                                        <%--visible="false"--%>
                                        <table class="table table-voucher gstr_sales_invoice_2 table-bordered" style="margin-top: 20px">
                                            <thead>
                                                <tr>
                                                    <th colspan="100%" class="inf_head">&nbsp;</th>
                                                </tr>
                                                <tr>

                                                    <th class="ti_col2">Location</th>
                                                    <th class="ti_col3">GRN No.</th>
                                                    <th class="ti_col4">GRN&nbsp;Date</th>
                                                    <th class="ti_col5">Order No.</th>
                                                    <th class="ti_col6">Order&nbsp;Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="ti_col3">
                                                        <asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>
                                                    <td class="ti_col4">
                                                        <asp:TextBox ID="txtGRNNo" MaxLength="16" placeholder="GRN NO." runat="server" /></td>
                                                    <td class="ti_col5">
                                                        <asp:TextBox ID="txtGRNDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" /></td>
                                                    <td class="ti_col4">
                                                        <asp:TextBox ID="txtOrderNo" MaxLength="16" placeholder="Order No." runat="server"></asp:TextBox></td>
                                                    <td class="ti_col5">
                                                        <asp:TextBox ID="txtOrderDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-xs-12">

                                    <style>
                                        .gstr_sales_item {
                                            width: 100%;
                                        }

                                            .gstr_sales_item .gstr_col1 {
                                                width: 16%;
                                            }

                                            .gstr_sales_item .gstr_col2 {
                                                width: 16%;
                                            }

                                            .gstr_sales_item .gstr_col3 {
                                                width: 4%;
                                            }

                                            .gstr_sales_item .gstr_col4 {
                                                width: 3%;
                                            }

                                            .gstr_sales_item .gstr_col5 {
                                                width: 8%;
                                            }

                                            .gstr_sales_item .gstr_col6 {
                                                width: 5%;
                                            }

                                            .gstr_sales_item .gstr_col8 {
                                                width: 7%;
                                            }

                                            .gstr_sales_item .gstr_col9 {
                                                width: 6%;
                                            }

                                            .gstr_sales_item .gstr_col10 {
                                                width: 5%;
                                            }

                                            .gstr_sales_item .gstr_col11 {
                                                width: 4%;
                                            }

                                            .gstr_sales_item .gstr_col12 {
                                                width: 5%;
                                            }

                                            .gstr_sales_item .gstr_col13 {
                                                width: 4%;
                                            }

                                            .gstr_sales_item .gstr_col14 {
                                                width: 5%;
                                            }

                                            .gstr_sales_item .gstr_col15 {
                                                width: 4%;
                                            }
                                    </style>


                                    <table class="table table-voucher table-bordered table-form gstr_sales_item" id="addtothis" style="margin-bottom: 80px;">
                                        <thead>
                                            <tr>
                                                <th colspan="100%" style="background: #1c75bf; color: #fff">Item Detail</th>
                                            </tr>
                                            <tr>
                                                <th rowspan="5" style="width: 25%">Expense Head</th>
                                                <th rowspan="4" style="width: 20%">Item Name</th>
                                                <th colspan="2">Primary</th>
                                                <th colspan="2">Secondary</th>
                                                <th rowspan="3" style="width: 4%">Free</th>
                                                <th rowspan="3" style="width: 6%">Rate</th>
                                                <th rowspan="3" style="width: 6%">Net Amount</th>
                                                <th rowspan="3" style="width: 6%">Discount<br />
                                                    <span style="font-size: 11px">% / Amt.</span></th>
                                                <th rowspan="3" style="width: 6%">Amount</th>
                                                <th rowspan="3" style="width: 4%" id="thITC" runat="server" visible="false">ITC</th>

                                            </tr>
                                            <tr>
                                                <th style="width: 5%">Qty</th>
                                                <th style="width: 6%">Unit</th>
                                                <th style="width: 5%">Qty</th>
                                                <th style="width: 6%">Unit</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="form-tr">
                                                <td>
                                                    <%--<asp:DropDownList ID="ddlExpenseHead" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                                    <cc1:ComboBox ID="ddlExpenseHead" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenseHead_SelectedIndexChanged" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                                <td>
                                                    <%--<asp:DropDownList ID="ddlItemName" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                                    <cc1:ComboBox ID="ddlItemName" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" MaxLength="8" CssClass="Decimal4" runat="server" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUnitName" Enabled="false" runat="server"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txtMinorUnitQty" MaxLength="8" CssClass="Decimal4" Enabled="false" runat="server" /></td>
                                                <td>
                                                    <asp:DropDownList Enabled="false" ID="ddlMinorUnit" runat="server" /></td>
                                                <td>
                                                    <asp:TextBox ID="txtFree" MaxLength="8" CssClass="Decimal4" runat="server" AutoPostBack="true" Text="0" OnTextChanged="txtFree_TextChanged"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtRate" MaxLength="9" runat="server" Text="0" CssClass="Money" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtItemAmt" MaxLength="9" Enabled="false" runat="server" Text="0" CssClass="Money"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDiscount" MaxLength="9" runat="server" CssClass="Money" Text="0" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox></td>
                                                <%-- <td>
                                                    <asp:DropDownList ID="ddlEdit" runat="server" AutoPostBack="true" Style="font-size: 12px; padding: 0">
                                                        <asp:ListItem Value="0" Text="No" Selected="True" />
                                                        <asp:ListItem Value="1" Text="Yes" />
                                                    </asp:DropDownList></td>--%>
                                                <td>
                                                    <asp:TextBox ID="txtItemTaxableAmt" MaxLength="9" Enabled="false" runat="server" CssClass="Money" Text="0"></asp:TextBox></td>
                                                <td id="tdITC" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlITC" runat="server" Style="font-size: 12px; padding: 0">
                                                        <asp:ListItem Value="0" Text="No" Selected="True" />
                                                        <asp:ListItem Value="1" Text="Yes" />
                                                    </asp:DropDownList></td>
                                            </tr>

                                            <tr id="collapse_me_tr">
                                                <td colspan="18" id="CollapseRemark" style="visibility: hidden; border-right-color: transparent">
                                                    <asp:TextBox ID="txtItemRemark" MaxLength="145" runat="server" placeholder="Enter Remark..." />
                                                </td>
                                                <td style="width: 80px; position: relative; text-align: center; border-bottom-color: transparent; border-right-color: transparent">
                                                    <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                    <div style="position: absolute; top: -54px; left: 50%; transform: translateX(-50%);">
                                                        <a href="javascript:void(0)" id="collapse_tr" class="sctooltip"><i class="fa fa-cog"></i><span class="sctooltiptext">Add Item Remark</span></a><br />
                                                        <asp:Button ID="btnAddItemDetail" runat="server" CssClass="btn btn-xs btn-primary" Text="Add" OnClick="btnAddItemDetail_Click" />
                                                    </div>
                                                </td>

                                            </tr>

                                        </tbody>
                                    </table>
                                    <asp:HiddenField ID="hfTaxRate" runat="server" />
                                    <asp:HiddenField ID="hfCGSTAmt" runat="server" />
                                    <asp:HiddenField ID="hfSGSTAmt" runat="server" />
                                    <asp:HiddenField ID="hfIGSTAmt" runat="server" />
                                    <asp:HiddenField ID="hfCESSAmt" runat="server" />
                                    <div id="divScroll" runat="server" style="margin-bottom: 20px; overflow: auto;">
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
                                        <asp:GridView ID="grdItemDetails" HeaderStyle-CssClass="hidden" CssClass="table table-voucher table-bordered table-form gstr_sales_item first_tr_hide" AutoGenerateColumns="false" runat="server" ShowHeader="True" OnRowCommand="grdItemDetails_RowCommand" OnRowDataBound="grdItemDetails_RowDataBound">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th rowspan="2" class="inf_head" style="width: 12%;">Expense Head</th>
                                                            <th rowspan="2" class="inf_head" style="width: 13%;">Item Name</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">Goods /
                                                                <br>
                                                                Service Code</th>
                                                            <th colspan="2" class="inf_head">Primary</th>
                                                            <th colspan="2" class="inf_head">Secondary</th>
                                                            <th rowspan="2" class="inf_head" style="width: 4%;">Free</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">Rate</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%;">Net Amount</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">Discount</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">Amount</th>
                                                            <th rowspan="2" class="inf_head" style="width: 2%;" id="thITC" runat="server" visible="false">ITC</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%; text-align: center"></th>
                                                        </tr>
                                                        <tr class="hide_my_pdosi">
                                                            <th class="inf_head" style="width: 5%">Qty</th>
                                                            <th class="inf_head" style="width: 5%">Unit</th>
                                                            <th class="inf_head" style="width: 5%">Qty</th>
                                                            <th class="inf_head" style="width: 5%">Unit</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpenseHead" Text='<%#Eval("PurchaseHeadName") %>' CssClass="txtFontSize" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemID" Text='<%#Eval("ItemID") %>' CssClass="hidden" runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemName" Text='<%#Eval("ItemName") %>' CssClass="txtFontSize" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHSNSACCode" Text='<%#Eval("HSNSACCode") %>' CssClass="txtFontSize" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Text='<%#Eval("ItemQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnit" Text='<%#Eval("ItemUnit") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinotQty" Text='<%#Eval("ItemMinorQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinorUnit" Text='<%#Eval("ItemMinorUnitID") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFree" Text='<%#Eval("FreeQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" Text='<%#Eval("ItemRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" Text='<%#Eval("ItemAmount") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscount" Text='<%#Eval("DiscountAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEdit" Text='<%#Eval("ItemInd") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxable" Text='<%#Eval("NetAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITC" Text='<%#Eval("ITCApplicable") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGST" Text='<%#Eval("CGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--    <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSGST" Text='<%#Eval("SGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGST" Text='<%#Eval("IGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCESS" Text='<%#Eval("CESSTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteID" Text="Del" CommandName="DeleteRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <RowStyle ForeColor="#000066" />
                                            <HeaderStyle BackColor="#1c75bf" ForeColor="#FAFCFD" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <style>
                                    .tbl_Free {
                                        width: 50%;
                                    }

                                        .tbl_Free input {
                                            height: 20px !important;
                                            vertical-align: top;
                                        }

                                    .btncss {
                                        height: 20px;
                                        padding: 1px 5px;
                                        margin-left: 4px;
                                    }

                                    .tdb0 tr td {
                                        border: 0 !important;
                                    }

                                    .thp0 tr th {
                                        padding: 0px 0px !important;
                                    }

                                    .mb10 {
                                        margin-bottom: 10px;
                                    }

                                    .point {
                                        cursor: pointer;
                                    }
                                </style>
                                <div class="col-sm-12 mb10">
                                    <div class="grid_collapse_opener">
                                        <asp:LinkButton ID="btnShowFreeItem" CssClass="col-xs-2 btn-asp-text p1012 point fa-asp text-left" runat="server" OnClick="btnShowFreeItem_Click"> &#xf067; Additional Free Items</asp:LinkButton>
                                    </div>
                                    <asp:Panel CssClass="col-xs-10" ID="divFree" runat="server" Visible="false">
                                        <table class="table table-voucher gstr_sales_other table-bordered tbl_Free" id="Table4" runat="server">
                                            <tbody>
                                                <tr>
                                                    <td class="other_col1">
                                                        <%--<asp:DropDownList ID="ddlFreeItemName" runat="server"></asp:DropDownList>--%>
                                                        <cc1:ComboBox ID="ddlFreeItemName" OnSelectedIndexChanged="ddlFreeItemName_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="250px" placeholder="Item Name" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    </td>
                                                    <td class="other_col2">
                                                        <asp:DropDownList ID="ddlFreeUnit" Enabled="false" runat="server" /></td>
                                                    <td class="other_col3 r">
                                                        <asp:TextBox ID="txtFreeQty" MaxLength="8" CssClass="Decimal4" placeholder="Qty" runat="server" /></td>
                                                    <td class="w9 c">
                                                        <asp:Button ID="btnFreeAdd" CssClass="btn btn-xs btn-primary btncss" Text="Add" runat="server" OnClick="btnFreeAdd_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:GridView ID="gvFreeItem" HeaderStyle-CssClass="inf_head" CssClass="table table-voucher gstr_sales_other table-bordered tbl_Free thp0" ShowHeader="true" AutoGenerateColumns="false" runat="server" OnRowCommand="gvFreeItem_RowCommand">
                                            <Columns>
                                                <asp:BoundField ItemStyle-CssClass="other_col1" DataField="ItemName" HeaderText="Item Name" />
                                                <asp:BoundField ItemStyle-CssClass="other_col2" DataField="ItemUnit" HeaderText="Unit" />
                                                <asp:BoundField ItemStyle-CssClass="other_col3" DataField="ItemQty" HeaderText="Qty" />
                                                <asp:TemplateField ItemStyle-CssClass="w9 c">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>

                                </div>
                                <asp:Panel ID="pnlParentAdvance" class="col-sm-12 mb10 grid_collapse_container" runat="server">
                                    <div class="grid_collapse_opener">
                                        <asp:LinkButton ID="btnShowPartyAdvance" CssClass="col-xs-2 btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowPartyAdvance_Click" runat="server"> &#xf067; Advance Received</asp:LinkButton>
                                    </div>
                                    <asp:Panel ID="pnlPartyAdvance" runat="server" Visible="false" CssClass="grid_collapse col-xs-10">
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

                                                <asp:TemplateField ItemStyle-CssClass="w10 c hidden" HeaderStyle-CssClass=" hidden" HeaderText="Advance Remaining">
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
                                <div class="col-xs-12">
                                    <div class="half_1_table">
                                        <table class="table table-voucher gstr_sales_other table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="100%" class="inf_head">Other Charges</th>
                                                </tr>
                                                <tr>
                                                    <th class="other_col1">Head Name</th>
                                                    <th class="other_col2">Add/Less</th>
                                                    <th class="other_col3">Amount</th>
                                                    <th class="other_col4"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="other_col1">
                                                        <asp:DropDownList ID="ddlHeadName" runat="server"></asp:DropDownList>
                                                        <%--<cc1:ComboBox ID="ddlHeadName" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>--%>
                                                    </td>
                                                    <td class="other_col2">
                                                        <asp:DropDownList ID="ddlAddLess" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Add</asp:ListItem>
                                                            <asp:ListItem Value="2">Less</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="other_col3 r">
                                                        <asp:TextBox ID="txtOtherChrgAmount" MaxLength="9" runat="server"></asp:TextBox></td>
                                                    <td class="other_col4">
                                                        <asp:Button ID="btnAdd" runat="server" CssClass=" btn btn-xs btn-primary" Text="Add" OnClick="btnAdd_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:GridView ID="gvotherCharge" CssClass="table table-voucher gstr_sales_other table-bordered" AutoGenerateColumns="false" ShowHeader="false" runat="server" OnRowCommand="gvotherCharge_RowCommand">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="other_col1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeadNameID" Text='<%#Eval("SundriCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                                        <asp:Label ID="lblHeadName" Style="width: 100%; height: 27px;" Text='<%#Eval("SundriHead") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="other_col2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ddlAddLess" Text='<%#Eval("SundriInd") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="other_col3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOtherChrgAmount" Text='<%#Eval("SundriAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="other_col4">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                    </div>
                                    <div class="half_2_table">
                                        <table class="table table-voucher gstr_sales_amount table-bordered pull-right">
                                            <thead>
                                                <tr>
                                                    <th colspan="100%" class="inf_head">Amount</th>
                                                </tr>
                                                <tr>
                                                    <th class="amount_col3">Gross</th>
                                                    <%--<th class="amount_col2">Tax</th>--%>
                                                    <th class="amount_col2">Add/Less</th>
                                                    <th class="amount_col1">Net Amount</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="amount_col1">
                                                        <asp:TextBox ID="txtGross" Enabled="true" placeholder="Gross" CssClass="Money" runat="server"></asp:TextBox></td>
                                                    <%--<td class="amount_col2">
                                                        <asp:TextBox ID="txtTax" Enabled="true" placeholder="Taxable" CssClass="Money" runat="server"></asp:TextBox></td>--%>
                                                    <td class="amount_col2">
                                                        <asp:TextBox ID="txtAddLess" Enabled="true" runat="server" CssClass="Money"></asp:TextBox></td>
                                                    <td class="amount_col3">
                                                        <asp:TextBox ID="txtNet" Enabled="true" placeholder="Net" CssClass="Money" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="table table-voucher table-narration table-bordered pull-right">
                                            <tbody>
                                                <tr>
                                                    <td class="col1" style="font-weight: bold">Narration</td>
                                                    <td class="col2">
                                                        <cc1:ComboBox ID="txtNarration" runat="server" Width="250px" placeholder="Narration" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                            </div>

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
                                <div class="row">
                                    <div class="col-xs-12">
                                        <span><b>*RCM </b>- Reverse Charge Mechanism  </span>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfSaleInvoiceManually" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>
