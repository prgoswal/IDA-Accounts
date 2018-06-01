<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmCashSaleReturn.aspx.cs" Inherits="Vouchers_frmCashSaleReturn" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .inf_head {
            background-color: #bdbdbd !important;
            border-color: #bdbdbd !important;
        }

        th.inf_head {
            border-right: 1px solid #fff !important;
            border-bottom: 1px solid #fff !important;
        }

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

        .grid_collapse_container_gstin {
            float: right;
            margin-right: 16px;
        }

        @media (max-width:880px) {
            .grid_collapse_container_gstin {
                float: left;
                margin-right: 0;
            }
        }

        .grid_collapse_opener {
            width: 154px;
            float: left;
            padding-top: 4px;
        }

        .grid_collapse {
            width: 600px;
            float: left;
        }

        .grid_collapse_container_gstin {
            width: 892px;
        }

        .grid_collapse_gstin {
            width: 780px;
            float: left;
        }

        .grid_collapse_opener_gstin {
            width: 112px;
            float: left;
            padding-top: 4px;
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

        .gstin_collapse {
            width: 75%;
            float: left;
        }

        .after_gstin_collapse {
            width: 25%;
            float: left;
        }

        @media (max-width: 990px) {

            .half_1_table {
                float: none;
                width: 100%;
                margin-top: 10px;
                margin-bottom: 10px;
            }

            .half_2_table {
                float: none;
                width: 100%;
                margin-top: 10px;
                margin-bottom: 10px;
            }
        }
    </style>
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
            //ChoosenDDL();
        }
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For Invoice Details -------------------//

            if ($('#<%=txtInvoiceDate.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Invoice Date.');
                $('#<%=txtInvoiceDate.ClientID%>').focus();
                return false;
            }
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
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">CASH SALES RETURN VOUCHER 
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" />
                    </span>
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-heading">
                                <div class="col-md-12  form-group">
                                    <asp:Panel ID="pnlSeraching" runat="server">

                                        <asp:Panel ID="pnlInvoiceseries" CssClass="col-xs-2" Style="padding-left: 0" runat="server">
                                            <asp:DropDownList ID="ddlInvoiceSeriesFind" CssClass="form-control" runat="server" />
                                            <asp:TextBox ID="txtInvoiceSeriesFind" placeholder="Invoice Series" CssClass="form-control text-uppercase" runat="server" />
                                        </asp:Panel>

                                        <asp:Panel CssClass="input-group col-xs-3" DefaultButton="btnSearchInvoice" runat="server">
                                            <asp:TextBox ID="txtSearchInvoice" MaxLength="7" CssClass="form-control numberonly" placeholder="Search Invoice No." runat="server" />
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="btnSearchInvoice" CssClass="btn btn-primary" OnClick="btnSearchInvoice_Click" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Panel ID="pnlBodyContent" runat="server" Enabled="false">
                                <div class="panel-body">
                                    <div style="padding-left: 15px">

                                        <table class="table table-voucher sales2_invoice table-bordered gstr_sales_invoice tdb0" style="width: 300px; float: left">
                                            <tbody>
                                                <tr>
                                                    <td class="w25">
                                                        <asp:DropDownList ID="ddlCashAccount" runat="server" />
                                                    </td>
                                                    <td class="w25">
                                                        <asp:DropDownList ID="ddlInvoiceSeries" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceSeries_SelectedIndexChanged" runat="server" />
                                                        <asp:TextBox ID="txtInvoiceSeries" CssClass="text-uppercase Series" MaxLength="10" Enabled="false" placeholder="Invoice Series" runat="server" />
                                                    </td>
                                                    <td class="w26">
                                                        <asp:TextBox ID="txtinvoiceNo" MaxLength="7" runat="server" placeholder="Invoice No." Enabled="false" CssClass="numberonly"></asp:TextBox></td>
                                                    <td class="w24">
                                                        <asp:TextBox ID="txtInvoiceDate" CssClass="datepicker" placeholder="DD/MM/YYYY" MaxLength="10" Style="width: 100%" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:DropDownList ID="ddlCostCenter" Enabled="false" Visible="false" runat="server" />

                                        <div class="grid_collapse_container_gstin mb10">
                                            <div class="grid_collapse_opener_gstin p1012 point">
                                                <asp:LinkButton ID="btnGSTINInvoice" OnClick="btnGSTINInvoice_Click" runat="server"><i class="fa r fa-plus" aria-hidden="true"></i>GSTIN Invoice</asp:LinkButton>
                                            </div>
                                            <div class="grid_collapse_gstin">
                                                <table class="table table-voucher gstr_sales_other Gstin_tbl tdb0 thp0" id="tblGSTINInvoice" visible="false" runat="server">
                                                    <tr class="align_with_combobox">
                                                        <td class="w26">
                                                            <cc1:ComboBox ID="ddlPartyName" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" Style="text-transform: uppercase" placeholder="Party Name"></cc1:ComboBox>
                                                        </td>
                                                        <td class="w11">
                                                            <asp:TextBox ID="txtPartyDetailMobileNo" MaxLength="10" placeholder="Mobile No." CssClass="numberonly" runat="server" />
                                                        </td>
                                                        <td class="w17">
                                                            <cc1:ComboBox ID="ddlGstinNo" placeholder="GSTIN No." AutoCompleteMode="SuggestAppend" MaxLength="15" CssClass="relative_gt" CaseSensitive="False" AutoPostBack="true" OnSelectedIndexChanged="ddlGstinNo_SelectedIndexChanged" runat="server" Style="text-transform: uppercase"></cc1:ComboBox>
                                                        </td>
                                                        <td class="w24">
                                                            <cc1:ComboBox CssClass="relative_gt" placeholder="Shipping Address" ID="ddlShippingAdd" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" runat="server"></cc1:ComboBox>
                                                        </td>
                                                        <td class="w22">
                                                            <asp:DropDownList ID="ddlState" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="after_gstin_collapse">
                                        <style>
                                            .sales2_invoice {
                                                width: 90%;
                                            }
                                        </style>


                                        <div id="Div1" runat="server" visible="false">
                                            <table class="table table-voucher gstr_sales_invoice_2 table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th colspan="7" style="background: #1c75bf; color: #fff">&nbsp</th>
                                                    </tr>
                                                    <tr>
                                                        <th class="ti_col1">Shipping Address</th>
                                                        <th class="ti_col2">Dispatch Location</th>
                                                        <th class="ti_col3">Order No.</th>
                                                        <th class="ti_col4">Order Date</th>
                                                        <th class="ti_col5">TDS</th>
                                                        <th class="ti_col6">TCS</th>
                                                        <th class="ti_col7">RCM</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="ti_col1">
                                                            <asp:TextBox Visible="false" ID="txtShippingAdd" MaxLength="135" runat="server" />
                                                        </td>

                                                        <td class="ti_col2">
                                                            <%--<asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>--%>

                                                            <td class="ti_col3">
                                                                <asp:TextBox ID="TextBox1" MaxLength="18" placeholder="Order No." runat="server"></asp:TextBox></td>

                                                            <td class="ti_col4">
                                                                <asp:TextBox ID="TextBox2" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />
                                                            </td>

                                                            <td class="ti_col5">
                                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td class="ti_col6">
                                                                <asp:DropDownList ID="DropDownList2" runat="server">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td class="ti_col7">
                                                                <asp:DropDownList ID="DropDownList3" runat="server">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <style>
                                            .sales2_invoice {
                                                width: 90%;
                                            }
                                        </style>
                                        <div id="tblShippingDetail" runat="server" visible="false">
                                            <table class="table table-voucher gstr_sales_invoice_2 table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th colspan="7" style="background: #1c75bf; color: #fff">&nbsp</th>
                                                    </tr>
                                                    <tr>
                                                        <th class="ti_col1">Shipping Address</th>
                                                        <th class="ti_col2">Dispatch Location</th>
                                                        <th class="ti_col3">Order No.</th>
                                                        <th class="ti_col4">Order Date</th>
                                                        <th class="ti_col5">TDS</th>
                                                        <th class="ti_col6">TCS</th>
                                                        <th class="ti_col7">RCM</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="ti_col1">
                                                            <asp:TextBox Visible="false" ID="txtShippingAddress" MaxLength="135" runat="server" />
                                                        </td>
                                                        <td class="ti_col2">
                                                            <asp:DropDownList ID="ddlDispatchLocation" runat="server"></asp:DropDownList>
                                                        </td>
                                                        <td class="ti_col3">
                                                            <asp:TextBox ID="txtOrderNo" MaxLength="18" placeholder="Order No." runat="server" />
                                                        </td>
                                                        <td class="ti_col4">
                                                            <asp:TextBox ID="txtOrderDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />
                                                        </td>
                                                        <td class="ti_col5">
                                                            <asp:DropDownList ID="ddlTDS" runat="server">
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
                                                        <td class="ti_col7">
                                                            <asp:DropDownList ID="ddlRCM" runat="server">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <style>
                                            .supd_table tr td:nth-child(1) {
                                                display: none;
                                            }

                                            .supd_table .ajax__combobox_textboxcontainer {
                                                display: table-cell !important;
                                            }
                                        </style>
                                        <asp:GridView ID="gvItemDetail" CssClass="gstr-table table-bordered supd_table mb10" AutoGenerateColumns="false" OnRowCommand="gvItemDetail_RowCommand" OnRowDataBound="gvItemDetail_RowDataBound" runat="server">
                                            <HeaderStyle CssClass="inverse" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr class="inf_head">
                                                            <th rowspan="2" class="w14">Item Name</th>
                                                            <th colspan="2">Primary</th>
                                                            <th colspan="2">Secondary</th>
                                                            <th rowspan="2" class="w5">Free</th>
                                                            <th rowspan="2" class="w5">Rate</th>
                                                            <th rowspan="2" class="w5">Net Amount</th>
                                                            <th colspan="3">Discount</th>
                                                            <th rowspan="2" class="w5">Taxable </th>
                                                            <th rowspan="2" class="w5">Tax Rate</th>
                                                            <th rowspan="2" class="w5">CGST</th>
                                                            <th rowspan="2" class="w5">SGST</th>
                                                            <th rowspan="2" class="w5">IGST</th>
                                                            <th rowspan="2" class="w5">CESS</th>
                                                            <th rowspan="2" class="w8">&nbsp;</th>
                                                        </tr>
                                                        <tr class="inf_head">
                                                            <th class="w4">Qty.</th>
                                                            <th class="w5">Unit</th>
                                                            <th class="w4">Qty.</th>
                                                            <th class="w5">Unit</th>
                                                            <th class="w5">Value</th>
                                                            <th class="w5">Type</th>
                                                            <th class="w5">Amt</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <EditItemTemplate>
                                                        <tr class="align_with_combobox">
                                                            <td></td>
                                                            <td class="">
                                                                <cc1:ComboBox ID="ddlItemName" runat="server" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true" Width="20px" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                            </td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtQty" Text='<%#Eval("ItemQty") %>' MaxLength="8" CssClass="Decimal4" runat="server" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                            <td class="">
                                                                <asp:DropDownList ID="ddlUnitName" Enabled="false" runat="server"></asp:DropDownList></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtMinorUnitQty" Text='<%#Eval("ItemMinorQty") %>' MaxLength="8" CssClass="Decimal4" Enabled="false" runat="server" /></td>
                                                            <td class="r">
                                                                <asp:DropDownList ID="ddlMinorUnit" Enabled="false" runat="server" /></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtFree" Text='<%#Eval("FreeQty") %>' MaxLength="8" CssClass="Decimal4" OnTextChanged="txtFree_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtRate" Text='<%#Eval("ItemRate") %>' MaxLength="7" runat="server" CssClass="Money" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtItemAmt" Text='<%#Eval("ItemAmount") %>' MaxLength="7" runat="server" CssClass="Money" Enabled="false"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtDiscount" Text='<%#Eval("DiscountValue") %>' CssClass="Money" MaxLength="7" runat="server" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true" /></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDiscount" AutoPostBack="true" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged" runat="server">
                                                                    <asp:ListItem Value="0" Selected="True" Text="Rs." />
                                                                    <asp:ListItem Value="1" Text="%" />
                                                                </asp:DropDownList></td>
                                                            <td>
                                                                <asp:TextBox ID="txtDiscountAmt" Enabled="false" Value='<%#Eval("DiscountAmt") %>' runat="server" />
                                                            </td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtItemTaxableAmt" Text='<%#Eval("NetAmt") %>' MaxLength="7" runat="server" CssClass="Money" Enabled="false"></asp:TextBox></td>

                                                            <td class="r">
                                                                <asp:TextBox ID="txtTax" Text='<%#Eval("TaxRate") %>' MaxLength="3" Enabled="false" OnTextChanged="txtTax_TextChanged" AutoPostBack="true" runat="server" CssClass="Money"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtCGSTAmt" Text='<%#Eval("CGSTTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtSGSTAmt" Text='<%#Eval("SGSTTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtIGSTAmt" Text='<%#Eval("IGSTTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                            <td class="r">
                                                                <asp:TextBox ID="txtCESSAmt" Text='<%#Eval("CESSTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>

                                                            <td class="c bbct"></td>
                                                        </tr>
                                                        <tr id="collapse_me_tr">
                                                            <td></td>
                                                            <td colspan="17">
                                                                <asp:TextBox ID="txtItemRemark" Text='<%#Eval("ItemRemark") %>' MaxLength="145" runat="server" placeholder="Enter Remark..." /></td>
                                                            <td class="c btct">
                                                                <asp:LinkButton ID="btnSave" Text="Update" CommandName="RowUpdate" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn sbtn-26" runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <td>
                                                            <asp:Label ID="ddlItemName" Text='<%#Eval("ItemName") %>' title='<%#Eval("ItemName") %>' runat="server" CssClass="item_overflow"></asp:Label></td>
                                                        <%--<td class="hiddent"><asp:Label ID="txtHSNSACCode"      Text='<%#Eval("HSNSACCode") %>' runat="server" ></asp:Label></td>--%>
                                                        <td class="r">
                                                            <asp:Label ID="txtQty" Text='<%#Eval("ItemQty") %>' runat="server"></asp:Label></td>
                                                        <td class="c">
                                                            <asp:Label ID="ddlUnitName" Text='<%#Eval("ItemUnit") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtItemMinorQty" Text='<%#Eval("ItemMinorQty") %>' runat="server"></asp:Label></td>
                                                        <td class="c">
                                                            <asp:Label ID="txtItemMinorUnit" Text='<%#Eval("ItemMinorUnit") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtFree" Text='<%#Eval("FreeQty") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtRate" Text='<%#Eval("ItemRate") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtItemAmt" Text='<%#Eval("ItemAmount") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtDiscountValue" Text='<%#Eval("DiscountValue") %>' runat="server"></asp:Label></td>
                                                        <td class="c">
                                                            <asp:Label ID="txtDiscountType" Text='<%#Eval("DiscountType") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtDiscountAmt" Text='<%#Eval("DiscountAmt") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtItemTaxableAmt" Text='<%#Eval("NetAmt") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtTax" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtCGSTAmt" Text='<%#Eval("CGSTTaxAmt") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtSGSTAmt" Text='<%#Eval("SGSTTaxAmt") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtIGSTAmt" Text='<%#Eval("IGSTTaxAmt") %>' runat="server"></asp:Label></td>
                                                        <td class="r">
                                                            <asp:Label ID="txtCESSAmt" Text='<%#Eval("CESSTaxAmt") %>' runat="server"></asp:Label></td>
                                                        <td class="c">
                                                            <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="RowEdit" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn" runat="server"></asp:LinkButton>
                                                            <asp:LinkButton ID="btnDelete" Text="&nbsp;Del" Visible="false" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <style>
                                        .Gstin_tbl {
                                            width: 100%;
                                        }

                                        .mrgn_b {
                                            margin-bottom: 5px;
                                        }

                                        .Party {
                                            width: 100%;
                                        }

                                        .inf_head tr th {
                                            color: #fff !important;
                                        }

                                        .tbl_Charges {
                                            width: 50%;
                                        }

                                            .tbl_Charges.tbl_Charges_col1 {
                                                width: 25%;
                                            }

                                            .tbl_Charges.tbl_Charges_col2 {
                                                width: 25%;
                                            }

                                            .tbl_Charges.tbl_Charges_col3 {
                                                width: 25%;
                                            }

                                            .tbl_Charges.tbl_Charges_col4 {
                                                width: 25%;
                                            }

                                        .trn_tbl {
                                            width: 75%;
                                        }

                                            .trn_tbl .trn_tbl_col1 {
                                                width: 25%;
                                            }

                                            .trn_tbl .trn_tbl_col2 {
                                                width: 25%;
                                            }

                                            .trn_tbl .trn_tbl_col3 {
                                                width: 25%;
                                            }

                                            .trn_tbl .trn_tbl_col4 {
                                                width: 25%;
                                            }

                                        .pd0 {
                                            padding: 0px !important;
                                        }

                                        .mb10 {
                                            margin-bottom: 15px;
                                            min-height: 22px !important;
                                        }

                                        .point {
                                            cursor: pointer;
                                        }



                                        .f13 {
                                            font-size: 13px !important;
                                        }
                                    </style>
                                    <div class="col-sm-12 mb10 grid_collapse_container">
                                        <div class="grid_collapse_opener p1012  point">
                                            <asp:LinkButton ID="btnTransportDetail" OnClick="btnTransportDetail_Click" runat="server"><i class="fa r fa-plus" aria-hidden="true"></i>Transport Detail</asp:LinkButton>
                                        </div>
                                        <div class="grid_collapse">
                                            <table class=" trn_tbl tdb0" id="tblTransportDetail" visible="false" runat="server">
                                                <tbody>
                                                    <tr>
                                                        <td class="trn_tbl_col1">
                                                            <asp:DropDownList ID="ddlTransportationType" runat="server"></asp:DropDownList>
                                                        </td>
                                                        <td class="trn_tbl_col2">
                                                            <asp:TextBox ID="txtTransportationDate" placeholder="DD/MM/YYYY" CssClass="datepicker" runat="server" />
                                                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTransportationDate" Mask="99/99/9999 99:99:99" MaskType="DateTime" UserDateFormat="DayMonthYear" MessageValidatorTip="true"></cc1:MaskedEditExtender>--%>
                                                        </td>
                                                        <td class="trn_tbl_col3">
                                                            <asp:TextBox ID="txtTransportationVehicleNo" MaxLength="15" placeholder="Vehicle No." runat="server" />
                                                        </td>
                                                        <td class="trn_tbl_col4">
                                                            <asp:TextBox ID="txtTransportationName" MaxLength="45" placeholder="Transporter Name" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 mb10 grid_collapse_container">
                                        <div class="grid_collapse_opener p1012  point">
                                            <asp:LinkButton ID="btnOtherCharge" OnClick="btnOtherCharge_Click" runat="server"><i class="fa r fa-plus" aria-hidden="true"></i>Other Charges</asp:LinkButton>
                                        </div>
                                        <div class="grid_collapse">
                                            <table class="table table-voucher gstr_sales_other  tbl_Charges tdb0" id="tblOtherCharge" visible="false" runat="server">
                                                <tbody>
                                                    <tr class="align_with_combobox">
                                                        <td class="w30">
                                                            <cc1:ComboBox ID="ddlOtherChargesHeadName" runat="server" Width="250px" placeholder="Other Charges" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                        </td>
                                                        <td class="w30">
                                                            <asp:DropDownList ID="ddlOtherChargesAddLess" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem>Add</asp:ListItem>
                                                                <asp:ListItem>Less</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="w30">
                                                            <asp:TextBox ID="txtOtherChargesAmount" MaxLength="9" placeholder="Amount" CssClass="Money" runat="server" />
                                                        </td>
                                                        <td class="text-center w10">
                                                            <asp:Button ID="btnAddOtherCharges" Text="Add" CssClass="btn btn-sxs btn-primary btncss" OnClick="btnAddOtherCharges_Click" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <asp:GridView ID="grdOtherCharge" CssClass="table table-voucher gstr_sales_other table-bordered" AutoGenerateColumns="false" ShowHeader="false" Visible="false" OnRowCommand="grdOtherCharge_RowCommand" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="SundriCode" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SundriHead" ItemStyle-CssClass="w30" />
                                                    <asp:BoundField DataField="SundriInd" ItemStyle-CssClass="w30" />
                                                    <asp:BoundField DataField="SundriAmt" ItemStyle-CssClass="w30" />
                                                    <asp:TemplateField ItemStyle-CssClass="w10 text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" Text="Del" Enabled="true" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                    <style>
                                        .mb10 {
                                            margin-bottom: 10px;
                                        }

                                        .point {
                                            cursor: pointer;
                                        }

                                        .grid_collapse table {
                                            width: 600px !important;
                                        }
                                    </style>
                                    <div class="col-sm-12 mb10">
                                        <div class="grid_collapse_opener p1012  point">
                                            <asp:LinkButton ID="btnShowFreeItem" OnClick="btnShowFreeItem_Click" runat="server"><i class="fa r fa-plus" aria-hidden="true"></i>Additional Free Items</asp:LinkButton>
                                        </div>
                                        <asp:Panel CssClass="grid_collapse" ID="divFree" runat="server" Visible="false">
                                            <table class="table table-voucher gstr_sales_other table-bordered tbl_Free tdb0" id="Table4" runat="server">
                                                <tbody>
                                                    <tr class="align_with_combobox">
                                                        <td class="w30">
                                                            <cc1:ComboBox ID="ddlFreeItemName" OnSelectedIndexChanged="ddlFreeItemName_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="250px" placeholder="Item Name" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                        </td>
                                                        <td class="w30">
                                                            <asp:DropDownList ID="ddlFreeUnit" Enabled="false" runat="server" />
                                                        </td>
                                                        <td class="w30 r">
                                                            <asp:TextBox ID="txtFreeQty" MaxLength="8" CssClass="Decimal4" runat="server" />
                                                        </td>
                                                        <td class="w10 text-center c">
                                                            <asp:Button ID="btnFreeAdd" CssClass="btn btn-sxs btn-primary btncss" OnClick="btnFreeAdd_Click" Text="Add" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <asp:GridView ID="gvFreeItem" CssClass="table table-voucher gstr_sales_other table-bordered tbl_Free" ShowHeader="false" OnRowCommand="gvFreeItem_RowCommand" AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-CssClass="w30" DataField="ItemName" />
                                                    <asp:BoundField ItemStyle-CssClass="w30" DataField="ItemUnit" />
                                                    <asp:BoundField ItemStyle-CssClass="w30" DataField="ItemQty" />
                                                    <asp:TemplateField ItemStyle-CssClass="w10 text-center c">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-sm-12 mb10">
                                        <div class="grid_collapse_opener p1012  point">
                                            <asp:LinkButton ID="btnShowBroker" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowBroker_Click" runat="server"> &#xf067; Broker Details</asp:LinkButton>
                                        </div>
                                        <asp:Panel ID="pnlBroker" runat="server" Visible="false" CssClass="grid_collapse">
                                            <table class="table-bordered" style="width: 600px">
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
                                            </table>
                                        </asp:Panel>

                                    </div>
                                    <div class="col-sm-12">
                                        <div class="half_1_table">
                                            <table class="table table-voucher gstr_sales_other table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="w25 inf_head">Payment Mode</th>
                                                        <th class="w75 inf_head">Remark</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPaymentMode" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem Text="Cash" Value="0" />
                                                                <asp:ListItem Text="Card" Value="1" />
                                                                <asp:ListItem Text="Other" Value="2" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPMRemark" Enabled="false" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table class="table table-voucher gstr_sales_other table-bordered tdb0 thp0">
                                                <thead>
                                                    <tr>
                                                        <th class="b0" colspan="100%">&nbsp;</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th class="w25">Dispatch&nbsp;Location
                                                        </th>
                                                        <td class="w75">
                                                            <asp:DropDownList ID="ddlLocation" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="half_2_table">
                                            <table class="table table-voucher gstr_sales_amount table-bordered pull-right">
                                                <thead>
                                                    <tr>
                                                        <th colspan="100%" class="inf_head">Amount</th>
                                                    </tr>
                                                    <tr>
                                                        <th class="amount_col3">Item </th>
                                                        <th class="amount_col3">Discount </th>

                                                        <th class="amount_col3">Taxable</th>
                                                        <th class="amount_col2">Tax</th>
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
                                                            <asp:TextBox ID="txtGross" Enabled="false" runat="server" placeholder="Gross" CssClass="Money"></asp:TextBox>
                                                        </td>
                                                        <td class="amount_col2">
                                                            <asp:TextBox ID="txtTax" Enabled="false" runat="server" placeholder="Tax" CssClass="Money"></asp:TextBox>
                                                        </td>
                                                        <td class="amount_col2">
                                                            <asp:TextBox ID="txtAddLess" Enabled="false" runat="server" placeholder="Add/Less" CssClass="Money"></asp:TextBox>
                                                        </td>
                                                        <td class="amount_col3">
                                                            <asp:TextBox ID="txtNetAmount" Enabled="false" runat="server" placeholder="Net Amount" CssClass="Money"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                            <table class="table table-voucher gstr_sales_narration table-bordered pull-right thp0">

                                                <tbody>
                                                    <tr class="align_with_combobox">
                                                        <td class="col1" style="font-weight: bold">Narration</td>
                                                        <td class="col2">
                                                            <cc1:ComboBox ID="ddlNarration" runat="server" Width="250px" placeholder="Narration" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
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


                                            <div class="pull-left">
                                                <asp:Button ID="btnCancel" Enabled="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                            </div>
                                            <div class="pull-right">
                                                <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                <%--<cc1:MaskedEditValidator  CssClass="text-danger" ID="MaskedEditValidator1" runat="server" ControlToValidate="txtTransportationDate" ControlExtender="MaskedEditExtender1" IsValidEmpty="true" EmptyValueMessage="Input Date and Time" InvalidValueMessage="Invalid Date And Time" ValidationGroup="a"></cc1:MaskedEditValidator>--%>
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Save" class="btn btn-primary btn-space-right" runat="server" />
                                                <asp:Button ID="btnClear" Text="Clear" class="btn btn-danger btn-space-right" runat="server" OnClick="btnClear_Click" />
                                            </div>
                                            <%--  <span><b>*PA  </b>- Provisional Assesment, </span>
                                <span><b>*ISD </b>- Input Service Distributor, </span>
                                <span><b>*RCM </b>- Reverse Charge Mechanism  </span>--%>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
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
