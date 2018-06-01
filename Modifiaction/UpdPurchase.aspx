<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="UpdPurchase.aspx.cs" Inherits="Modifiaction_UpdPurchase" %>

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
                debugger;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(Datetime);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">UPDATE PURCHASE VOUCHER
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                    <asp:Panel CssClass="input-group col-xs-3" runat="server">
                                        <asp:TextBox ID="txtSearchVoucher" MaxLength="7" CssClass="form-control numberonly" placeholder="Search Transaction No." runat="server" />
                                        <div class="input-group-btn">
                                            <asp:LinkButton ID="btnSearchVoucher" CssClass="btn btn-primary" OnClick="btnSearchVoucher_Click" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </asp:Panel>

                                    <br />
                                    <table class="table table-voucher table-bordered table-form gstr_sales_item gstr_purchase_voucher">
                                        <thead>
                                            <tr>
                                                <th colspan="100%" style="background: #1c75bf; color: #fff">Invoice Details</th>
                                            </tr>
                                            <tr>
                                                <th style="width: 14%">Type</th>
                                                <th style="width: 7%">Transaction Date</th>
                                                <th style="width: 7%">Bill No.</th>
                                                <th style="width: 7%">Bill Date</th>
                                                <th style="width: 22%">Purchase From</th>
                                                <th style="width: 11%">GSTIN</th>
                                                <th style="width: 22%">Shipped From</th>
                                                <th id="thCCCode" runat="server" visible="false" style="width: 8%">Cost Centre</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlPayMode" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" runat="server"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txtVoucherDate" MaxLength="10" CssClass="datepicker" placeholder="DD/MM/YYYY" runat="server" /></td>
                                                <td>
                                                    <asp:TextBox ID="txtBillNo" MaxLength="16" placeholder="Bill No." runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtBillDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" /></td>
                                                <td>
                                                    <cc1:ComboBox ID="ddlPurchaseFrom" AutoPostBack="true" OnSelectedIndexChanged="ddlPurchaseFrom_SelectedIndexChanged" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="false" Style="text-transform: uppercase" runat="server"></cc1:ComboBox>
                                                    <asp:TextBox ID="txtPurchaseFrom" OnTextChanged="txtPurchaseFrom_TextChanged" AutoPostBack="true" CssClass="text-uppercase" Visible="false" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGstinNo" AutoPostBack="true" OnSelectedIndexChanged="ddlGstinNo_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                    <asp:TextBox ID="txtGstinNo" CssClass="GSTIN text-uppercase" AutoPostBack="true" OnTextChanged="txtGstinNo_TextChanged" Visible="false" runat="server" />
                                                </td>
                                                <td>
                                                    <cc1:ComboBox ID="ddlShippingAdd" CssClass="relative_gt" runat="server"></cc1:ComboBox>
                                                    <asp:TextBox ID="txtShippingAdd" MaxLength="135" runat="server" />
                                                </td>
                                                <td id="tdCCCode" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlCostCenter" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="tblShippingDetail" runat="server" visible="false">
                                        <table class="table table-voucher gstr_sales_invoice_2 table-bordered" style="margin-top: 20px">
                                            <thead>
                                                <tr>
                                                    <th colspan="100%" class="inf_head">&nbsp;</th>
                                                </tr>
                                                <tr>
                                                    <th style="width: 20%">Location</th>
                                                    <th class="ti_col3">GRN No.</th>
                                                    <th class="ti_col4">GRN&nbsp;Date</th>
                                                    <th class="ti_col5">Order No.</th>
                                                    <th class="ti_col6">Order&nbsp;Date</th>
                                                    <th class="ti_col7">TDS</th>
                                                    <th class="ti_col8">TCS</th>
                                                    <th class="ti_col9">RCM</th>
                                                    <th style="width: 8%">IDA Ref. No.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="ti_col3">
                                                        <asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>
                                                    <td class="ti_col4">
                                                        <asp:TextBox ID="txtGRNNo" MaxLength="16" placeholder="GRN NO." Enabled="false" runat="server" /></td>
                                                    <td class="ti_col5">
                                                        <asp:TextBox ID="txtGRNDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Enabled="false" runat="server" /></td>
                                                    <td class="ti_col4">
                                                        <asp:TextBox ID="txtOrderNo" MaxLength="16" placeholder="Order No." runat="server"></asp:TextBox></td>
                                                    <td class="ti_col5">
                                                        <asp:TextBox ID="txtOrderDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" /></td>
                                                    <td class="ti_col6">
                                                        <asp:DropDownList ID="ddlTds" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ti_col6">
                                                        <asp:DropDownList ID="ddlTCS" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ti_col6">
                                                        <asp:DropDownList ID="ddlRCM" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Panel runat="server">

                                                            <asp:TextBox ID="txtIDARefNo" MaxLength="9" runat="server" />

                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfHSNSACCode" runat="server" />
                                <asp:HiddenField ID="hfGoodsAndServiceInd" runat="server" />
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
                                                <th rowspan="2" style="width: 8%">Expense Head</th>
                                                <th rowspan="2" style="width: 10%">Item Name</th>
                                                <th colspan="2">Primary</th>
                                                <th colspan="2">Secondary</th>
                                                <th rowspan="2" style="width: 3%">Free</th>
                                                <th rowspan="2" style="width: 5%">Rate</th>
                                                <th rowspan="2" style="width: 5%">Item Amount</th>
                                                <th rowspan="2" style="width: 5%">Discount<br />
                                                    <span style="font-size: 11px">% / Amt.</span></th>
                                                <th rowspan="2" style="width: 3%">Edit</th>
                                                <th rowspan="2" style="width: 5%">Taxable</th>
                                                <th rowspan="2" style="width: 3%">ITC</th>
                                                <th rowspan="2" style="width: 3%" id="thTaxRate" runat="server">Tax Rate</th>
                                                <th rowspan="2" style="width: 5%" id="thCGST" runat="server">CGST</th>
                                                <th rowspan="2" style="width: 5%" id="thSGST" runat="server">SGST</th>
                                                <th rowspan="2" style="width: 5%" id="thIGST" runat="server">IGST</th>
                                                <th rowspan="2" style="width: 5%" id="thCESS" runat="server">CESS</th>
                                                <th rowspan="2" style="width: 2%; border-bottom-color: transparent">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>

                                            </tr>
                                            <tr>
                                                <th style="width: 4%">Qty</th>
                                                <th style="width: 5%">Unit</th>
                                                <th style="width: 4%">Qty</th>
                                                <th style="width: 5%">Unit</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="form-tr">
                                                <td>
                                                    <cc1:ComboBox ID="ddlExpenseHead" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenseHead_SelectedIndexChanged" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                                <td>
                                                    <cc1:ComboBox ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" MaxLength="8" CssClass="Decimal4" runat="server" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUnitName" Enabled="false" runat="server"></asp:DropDownList></td>
                                                <td>
                                                    <asp:TextBox ID="txtMinorUnitQty" MaxLength="8" CssClass="Decimal4" Enabled="false" runat="server" /></td>
                                                <td>
                                                    <asp:DropDownList Enabled="false" ID="ddlMinorUnit" runat="server" /></td>
                                                <td>
                                                    <asp:TextBox ID="txtFree" MaxLength="8" CssClass="Decimal4" runat="server" OnTextChanged="txtFree_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtRate" MaxLength="13" runat="server" Text="0" CssClass="Money" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtItemAmt" MaxLength="13" Enabled="false" runat="server" Text="0" CssClass="Money"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDiscount" MaxLength="9" runat="server" CssClass="Money w53" Text="0" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlDiscount" CssClass="DiscountJS w40" Style="" AutoPostBack="true" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="0" Selected="True" Text="Rs." />
                                                        <asp:ListItem Value="1" Text="%" />
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                                                    <td>
                                                        <asp:DropDownList ID="ddlEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEdit_SelectedIndexChanged" Style="font-size: 12px; padding: 0">
                                                            <asp:ListItem Value="0" Text="No" Selected="True" />
                                                            <asp:ListItem Value="1" Text="Yes" />
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:TextBox ID="txtItemTaxableAmt" MaxLength="13" Enabled="false" runat="server" CssClass="Money" Text="0"></asp:TextBox></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlITC" runat="server" Style="font-size: 12px; padding: 0">
                                                            <asp:ListItem Value="1" Text="Yes" Selected="True" />
                                                            <asp:ListItem Value="0" Text="No" />
                                                        </asp:DropDownList></td>
                                                    <td id="tdTax" runat="server">
                                                        <asp:TextBox ID="txtTaxRate" Enabled="false" MaxLength="5" CssClass="Money" runat="server"></asp:TextBox></td>
                                                    <%--AutoPostBack="true" OnTextChanged="txtTaxRate_TextChanged"--%>
                                                    <td id="tdCGSTAmt" runat="server">
                                                        <asp:TextBox ID="txtCGSTAmt" MaxLength="13" Enabled="false" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                                    <td id="tdSGSTAmt" runat="server">
                                                        <asp:TextBox ID="txtSGSTAmt" MaxLength="13" Enabled="false" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                                    <td id="tdIGSTAmt" runat="server">
                                                        <asp:TextBox ID="txtIGSTAmt" MaxLength="13" Enabled="false" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                                    <td id="tdCESSAmt" runat="server">
                                                        <asp:TextBox ID="txtCESSAmt" MaxLength="13" Enabled="false" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                                    <td style="background: initial; border-bottom-color: #eee"></td>
                                            </tr>
                                            <tr id="collapse_me_tr">
                                                <td colspan="18" id="CollapseRemark" style="visibility: hidden; border-right-color: transparent">
                                                    <asp:TextBox ID="txtItemRemark" MaxLength="145" runat="server" placeholder="Enter Remark..." />
                                                </td>
                                                <td style="width: 80px; position: relative; text-align: center; border-bottom-color: transparent; border-right-color: transparent">
                                                    <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                    <div style="position: absolute; top: -54px; left: 50%; transform: translateX(-50%);">
                                                        <a href="javascript:void(0)" id="collapse_tr" class="sctooltip"><i class="fa fa-cog"></i><span class="sctooltiptext">Add Item Remark</span></a><br />
                                                        <asp:Button ID="btnAddItemDetail" CssClass="btn btn-xs btn-primary" Text="Add" Enabled="false" OnClick="btnAddItemDetail_Click" runat="server" />
                                                    </div>
                                                </td>

                                            </tr>

                                        </tbody>
                                    </table>
                                    <asp:HiddenField ID="hfPurchaseSaleRecordID" runat="server" />
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
                                        <asp:GridView ID="grdItemDetails" HeaderStyle-CssClass="hidden" CssClass="table table-voucher table-bordered table-form gstr_sales_item first_tr_hide" OnRowCommand="grdItemDetails_RowCommand" OnRowDataBound="grdItemDetails_RowDataBound" AutoGenerateColumns="false" runat="server" ShowHeader="True">
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
                                                            <th rowspan="2" class="inf_head" style="width: 5%;">Item Amount</th>
                                                            <%--<th rowspan="2" class="inf_head" style="width: 5%">Discount</th>--%>
                                                            <th colspan="3" class="inf_head">Discount</th>

                                                            <th rowspan="2" class="inf_head" style="width: 2%;">Edit</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">Taxable</th>
                                                            <th rowspan="2" class="inf_head" style="width: 2%;">ITC</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">Tax Rate</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">CGST</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">SGST</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">IGST</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%">CESS</th>
                                                            <th rowspan="2" class="inf_head" style="width: 5%; text-align: center"></th>
                                                        </tr>
                                                        <tr class="hide_my_pdosi">
                                                            <th class="inf_head" style="width: 5%">Qty</th>
                                                            <th class="inf_head" style="width: 5%">Unit</th>
                                                            <th class="inf_head" style="width: 5%">Qty</th>
                                                            <th class="inf_head" style="width: 5%">Unit</th>

                                                            <th class="inf_head w4">Value</th>
                                                            <th class="inf_head w3">Type</th>
                                                            <th class="inf_head w4">Amount</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurchaseCode" Text='<%#Eval("PurchaseCode") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblExpenseHead" Text='<%#Eval("PurchaseHeadName") %>' CssClass="txtFontSize" runat="server"></asp:Label>
                                                        <asp:Label ID="lblFreeItemInd" Text='<%#Eval("FreeItemInd") %>' Visible="false" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemID" Text='<%#Eval("ItemID") %>' Visible="false" runat="server"></asp:Label>
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
                                                        <asp:Label ID="lblUnitID" Text='<%#Eval("ItemUnitID") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblUnitName" Text='<%#Eval("ItemUnit") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinotQty" Text='<%#Eval("ItemMinorQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinorUnitID" Text='<%#Eval("ItemMinorUnitID") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblMinorUnitName" Text='<%#Eval("ItemMinorUnitName") %>' runat="server"></asp:Label>
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
                                                        <asp:Label ID="lblDiscount" Text='<%#Eval("DiscountValue") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscountType" Text='<%#Eval("DiscountType") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscountAmt" Text='<%#Eval("DiscountAmt") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEdit" Text='<%#Eval("ItemInd") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxable" Text='<%#Eval("NetAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITC" Text='<%#Eval("ITCApplicable") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGSTTax" Text='<%#Eval("CGSTTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCGSTTaxAmt" Text='<%#Eval("CGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSGSTTax" Text='<%#Eval("SGSTTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblSGSTTaxAmt" Text='<%#Eval("SGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGSTTax" Text='<%#Eval("IGSTTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblIGSTTaxAmt" Text='<%#Eval("IGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCESSTax" Text='<%#Eval("CESSTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCESSTaxAmt" Text='<%#Eval("CESSTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="EditRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-primary add_btn" runat="server"></asp:LinkButton>
                                                        <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
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
                                <asp:HiddenField ID="hfItemRecordID" runat="server" />
                                <asp:HiddenField ID="hfItemInd" runat="server" />
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
                                    <asp:LinkButton ID="btnShowFreeItem" CssClass="btn-asp-text p1012 col-xs-2 point fa-asp text-left" Text="&#xf067; Additional Free Items" OnClick="btnShowFreeItem_Click" runat="server" />
                                    <asp:Panel CssClass="col-xs-10" ID="divFree" runat="server" Visible="false">
                                        <table class="table table-voucher gstr_sales_other table-bordered tbl_Free" id="Table4" runat="server">
                                            <tbody>
                                                <tr>
                                                    <td class="other_col1">
                                                        <cc1:ComboBox ID="ddlFreeItemName" OnSelectedIndexChanged="ddlFreeItemName_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="250px" placeholder="Item Name" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    </td>
                                                    <td class="other_col2">
                                                        <asp:DropDownList ID="ddlFreeUnit" Enabled="false" runat="server" /></td>
                                                    <td class="other_col3 r">
                                                        <asp:TextBox ID="txtFreeQty" MaxLength="8" CssClass="Decimal4" placeholder="Qty" runat="server" /></td>
                                                    <td class="w9 c">
                                                        <asp:Button ID="btnFreeAdd" CssClass="btn btn-xs btn-primary btncss" OnClick="btnFreeAdd_Click" Text="Add" Enabled="false" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:GridView ID="gvFreeItem" HeaderStyle-CssClass="inf_head" CssClass="table table-voucher gstr_sales_other table-bordered tbl_Free thp0" ShowHeader="true" OnRowCommand="gvFreeItem_RowCommand" AutoGenerateColumns="false" runat="server">
                                            <Columns>
                                                <%--<asp:BoundField ItemStyle-CssClass="other_col1 hidden" DataField="ItemID" />--%>
                                                <asp:BoundField ItemStyle-CssClass="other_col1" DataField="ItemName" HeaderText="Item Name" />
                                                <asp:BoundField ItemStyle-CssClass="other_col2" DataField="ItemUnit" HeaderText="Unit" />
                                                <asp:BoundField ItemStyle-CssClass="other_col3" DataField="ItemQty" HeaderText="Qty" />
                                                <asp:TemplateField ItemStyle-CssClass="w9 c">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDel" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>

                                <div class="col-sm-12 mb10" style="display: none;">
                                    <div class="grid_collapse_opener">

                                        <asp:LinkButton ID="btnShowBroker" CssClass="btn-asp-text p1012 col-xs-2 point fa-asp text-left" Text="&#xf067; Broker Details" OnClick="btnShowBroker_Click" runat="server" />
                                    </div>
                                    <asp:Panel CssClass="col-xs-10" ID="pnlBroker" runat="server" Visible="false">
                                        <table class="table table-voucher gstr_sales_other table-bordered tbl_Free" id="Table1" runat="server">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 15%">
                                                        <asp:DropDownList ID="ddlBroker" OnSelectedIndexChanged="ddlBroker_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                                                    <td style="width: 20%">
                                                        <asp:DropDownList ID="ddlBrokerGSTIN" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td style="width: 15%">
                                                        <asp:TextBox ID="txtBrokerRate" MaxLength="5" runat="server" placeholder="Rate" CssClass="Money" OnTextChanged="txtBokerRate_TextChanged" AutoPostBack="true" Enabled="false" /></td>
                                                    <td style="width: 30%">
                                                        <asp:TextBox ID="txtBrokerAmount" MaxLength="45" runat="server" placeholder="Amount" CssClass="Money" Enabled="false" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </div>

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
                                                        <cc1:ComboBox ID="ddlHeadName" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    </td>
                                                    <td class="other_col2">
                                                        <asp:DropDownList ID="ddlAddLess" runat="server">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Add</asp:ListItem>
                                                            <asp:ListItem Value="2">Less</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="other_col3 r">
                                                        <asp:TextBox ID="txtOtherChrgAmount" MaxLength="13" runat="server"></asp:TextBox></td>
                                                    <td class="other_col4">
                                                        <asp:Button ID="btnAdd" runat="server" CssClass=" btn btn-xs btn-primary" Text="Add" Enabled="false" OnClick="btnAdd_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:GridView ID="gvotherCharge" CssClass="table table-voucher gstr_sales_other table-bordered" AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gvotherCharge_RowCommand" runat="server">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="other_col1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeadNameID" Text='<%#Eval("SundriCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                                        <asp:Label ID="lblHeadName" Style="width: 100%; height: 27px;" Text='<%#Eval("SundriHead") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="other_col2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddLessInd" Text='<%#Eval("SundriInd") %>' runat="server"></asp:Label>
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
                                                    <th class="amount_col2">Tax</th>
                                                    <th class="amount_col2">Add/Less</th>
                                                    <th class="amount_col1">Net Amount</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="amount_col1">
                                                        <asp:TextBox ID="txtGross" Enabled="false" placeholder="Gross" CssClass="Money" runat="server"></asp:TextBox></td>
                                                    <td class="amount_col2">
                                                        <asp:TextBox ID="txtTax" Enabled="false" placeholder="Taxable" CssClass="Money" runat="server"></asp:TextBox></td>
                                                    <td class="amount_col2">
                                                        <asp:TextBox ID="txtAddLess" Enabled="false" runat="server" CssClass="Money"></asp:TextBox></td>
                                                    <td class="amount_col3">
                                                        <asp:TextBox ID="txtNet" Enabled="false" placeholder="Net" CssClass="Money" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="table table-voucher table-narration table-bordered pull-right">
                                            <tbody>
                                                <tr>
                                                    <td class="col1" style="font-weight: bold">Narration</td>
                                                    <td class="col2">
                                                        <style>
                                                            .text-uppercase input {
                                                                text-transform: uppercase;
                                                            }
                                                        </style>
                                                        <asp:TextBox ID="txtNarration" placeholder="Enter Narration" CssClass="text-uppercase" MaxLength="120" Width="100%" runat="server" />
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
                                        <%--<div class="pull-left">
                                            
                                        </div>--%>
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button Text="Reject" ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="btn btn-warning" />
                                            <asp:Button Text="Approved" ID="btnApproved" OnClick="btnApproved_Click" runat="server" CssClass="btn btn-success" />
                                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Text="Update" Enabled="false" CssClass="btn btn-primary btn-space-right" Visible="false" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Visible="false" Text="Clear" CssClass="btn btn-danger btn-space-right" />
                                            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-danger btn-space-right" />
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
                        <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
                            <div class="panel panel-primary bodyPop" style="width: 30%; padding: 0">
                                <div class="panel-heading">
                                    <i class="fa fa-info-circle"></i>
                                    Are You Sure for Rejection this Transaction Number 
                                </div>
                                <div class="panel-body">
                                    <div class="mb10">
                                        Reason for Rejection
                                             <br />


                                        Note: You Can Also Type Here Your Rejection Reason.
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
                                        <asp:Button ID="btnYes" OnClick="btnYes_Click" ValidationGroup="btnYes" CssClass="btn btn-primary" Text="Yes" runat="server" />
                                        <asp:Button ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
                                        <br />
                                        <br />
                                        <asp:Label ID="lblCancelMsg" CssClass="text-danger lblMsg" runat="server"></asp:Label><asp:HyperLink ID="hypBtnBack" Visible="false" Style="margin-left: 5px;" runat="server" NavigateUrl="~/Vouchers/FrmPendingVouchers.aspx" Text="Back"></asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <style>
                            .reportPopUp {
                                top: 60px;
                                bottom: 0;
                                padding-top: 20px;
                                background-color: rgba(0, 0, 0, 0.63);
                                position: fixed;
                                z-index: 1940;
                            }

                            .bodyPop {
                                display: grid;
                                background: white;
                                width: 80%;
                                margin: auto;
                                border: 1px solid grey;
                                border-radius: 6px;
                                box-shadow: 3px 4px 13px rgba(0, 0, 0, 0.4);
                                padding: 15px;
                                z-index: 1040;
                            }
                        </style>
                        <asp:Panel runat="server" ID="pnlApproval" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
                            <div class="panel panel-primary bodyPop" style="width: 80%; padding: 0; position: center; background-color: #0075BB">
                                <div class="content-wrapper form-control-mini">
                                    <div class="panel-heading" style="text-align: center; margin-top: -18px; font-size: 18px; color: white;">
                                        <div><b></b></div>
                                        <div style="float: right; margin-top: -14px; margin-right: -30px;">
                                            <asp:LinkButton ID="lnkClose" ValidationGroup="Pass" OnClick="lnkClose_Click" runat="server"><i class="fa fa-times-circle text-danger"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="container_fluid">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="form-horizontal">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="col-sm-3">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-7">Transaction&nbsp;No.</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:Label ID="lblTransactionNo" CssClass="form-control" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-5">Transaction&nbsp;Date</label>
                                                                        <div class="col-sm-7">
                                                                            <asp:Label ID="lblTransactionDate" CssClass="form-control" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-4">Document&nbsp;Type</label>
                                                                        <div class="col-sm-8">
                                                                            <asp:Label ID="lblDoumentTypeID" Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblDocumentType" CssClass="form-control" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3">Narration</label>
                                                                        <div class="col-sm-9" style="margin-left: -103px; width: 85.2%;">
                                                                            <asp:Label ID="lblNarration" CssClass="form-control" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-5" style="margin-left: 6px;">Approval Date<i class="text-danger">*</i></label>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblApprovalCount" runat="server" Visible="false"></asp:Label>
                                                                            <asp:TextBox ID="txtApprovalDate" CssClass="datepicker form-control" placeholder="DD/MM/YYYY" MaxLength="10" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3">Approval Remark<i class="text-danger">*</i></label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtApprovalRemark" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-footer">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="pull-left">
                                                                <label class="text-danger">Note :- (*) Is A Mandatory(Compulsory) Fields.</label>
                                                            </div>
                                                            <div class="pull-right">
                                                                <asp:Label ID="lblApprovalMSG" CssClass="text-danger lblMsg" runat="server" />
                                                                <asp:Button ID="btnApprovedYes" ValidationGroup="Pass" CssClass="btn btn-primary" Text="Yes" OnClick="btnApprovedYes_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlApprovalConfirmation" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
                            <div class="panel panel-primary bodyPop" style="width: 25%; padding: 0; position: center; background-color: #0075BB">
                                <div class="content-wrapper form-control-mini">
                                    <div class="panel-heading" style="text-align: center; margin-top: -18px; font-size: 18px; color: white;">
                                    </div>
                                    <div class="container_fluid">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="form-horizontal">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <div class="form-group row" style="margin: 1px">
                                                                    Do You Want To Approve This Transaction No.
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-footer">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="pull-right">
                                                                <asp:Button ID="btnApprovalConfirmationYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnApprovalConfirmationYes_Click" runat="server" />
                                                                <asp:Button ID="btnApprovalConfirmationNo" CssClass="btn btn-danger" Text="No" OnClick="btnApprovalConfirmationNo_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfSaleInvoiceManually" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>

