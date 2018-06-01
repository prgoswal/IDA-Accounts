<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmUpdSalesReturn.aspx.cs" Inherits="Modifiaction_frmUpdSalesReturn" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function PaChange() {
            try {
                var ddlPa = document.getElementById('<%=ddlPA.ClientID%>');
                var collRemark = document.getElementById('CollapseRemark');
                if (ddlPa.value == "1") {
                    b
                    collRemark.colSpan = 17;
                } else {
                    collRemark.colSpan = 11;
                }
            } catch (e) {

            }
        }

        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
            PaChange();

            $('#collapse_tr').click(function () {
                var ddlPa = document.getElementById('<%=ddlPA.ClientID%>');
                var collRemark = document.getElementById('CollapseRemark');

                if (collRemark.style.visibility == "hidden") {
                    collRemark.style.visibility = "visible";

                } else {
                    collRemark.style.visibility = "hidden";
                }
                $('#<%=txtItemRemark.ClientID%>').focus();
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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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

                <h3 class="text-center head">Update SALES RETURN VOUCHER 
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>

                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                    <asp:Panel CssClass="input-group col-xs-3" runat="server">
                                        <asp:TextBox ID="txtSearchVoucher" MaxLength="7" CssClass="form-control numberonly" placeholder="Search Voucher No." runat="server" />
                                        <div class="input-group-btn">
                                            <asp:LinkButton ID="btnSearchVoucher" CssClass="btn btn-primary" OnClick="btnSearchVoucher_Click" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                    <br />
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
                                                <th class="ti_col5">GSTIN</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr class="align_with_combobox">
                                                <td class="ti_col3">
                                                    <%--<cc1:ComboBox AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ddlIncomeHead_SelectedIndexChanged" 
                                                        CssClass="relative_gt" ID="ddlIncomeHead" runat="server" Width="250px" CaseSensitive="False" 
                                                        Style="text-transform: uppercase">
                                                    </cc1:ComboBox>--%>
                                                    <asp:DropDownList AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlIncomeHead_SelectedIndexChanged" ID="ddlIncomeHead" runat="server" />
                                                </td>
                                                <td class="ti_col6">
                                                    <asp:DropDownList ID="ddlInvoiceSeries" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceSeries_SelectedIndexChanged" runat="server" />
                                                    <asp:TextBox ID="txtInvoiceSeries" CssClass="text-uppercase Series" MaxLength="10" Enabled="false" placeholder="Invoice Series" runat="server" />
                                                </td>
                                                <td class="ti_col1">
                                                    <asp:TextBox ID="txtinvoiceNo" MaxLength="7" runat="server" placeholder="Invoice No." Enabled="false" CssClass="numberonly"></asp:TextBox></td>

                                                <td class="ti_col2">
                                                    <asp:TextBox ID="txtInvoiceDate" Enabled="false" CssClass="datepicker" placeholder="DD/MM/YYYY" MaxLength="10" Style="width: 100%" runat="server" /></td>

                                                <td class="ti_col4">
                                                    <cc1:ComboBox ID="ddlSalesto" Enabled="false" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesto_SelectedIndexChanged1" Style="text-transform: uppercase"></cc1:ComboBox>
                                                    <%--<asp:DropDownList ID="ddlSalesto" CssClass="chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesto_SelectedIndexChanged1" runat="server" />--%>
                                                    <asp:TextBox ID="txtSalesto" Enabled="false" AutoPostBack="true" OnTextChanged="txtSalesto_TextChanged" CssClass="text-uppercase" MaxLength="45" Visible="false" placeholder="Sales To" runat="server" />
                                                </td>

                                                <td class="ti_col5">
                                                    <asp:DropDownList ID="ddlGstinNo" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlGstinNo_SelectedIndexChanged" runat="server" />
                                                    <asp:TextBox ID="txtGstinNo" Enabled="false" Visible="false" CssClass="GSTIN text-uppercase" AutoPostBack="true" OnTextChanged="txtGstinNo_TextChanged" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                    <div id="tblShippingDetail" runat="server" visible="false">
                                        <table class="table table-voucher gstr_sales_invoice_2 table-bordered tdb0 thp0">
                                            <thead>
                                                <tr>
                                                    <th colspan="7" style="background: #1c75bf; color: #fff">&nbsp</th>
                                                </tr>
                                                <tr>
                                                    <th class="ti_col1">Shipping Address</th>
                                                    <th class="ti_col2">Dispatch Location</th>
                                                    <th style="width: 10%" id="thCCCode" runat="server" visible="false">Cost Centre</th>

                                                    <th class="ti_col3">Order No.</th>
                                                    <th class="ti_col4">Order Date</th>
                                                    <th class="ti_col5">TDS</th>
                                                    <th class="ti_col6">TCS</th>
                                                    <th class="ti_col7">RCM</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="align_with_combobox">
                                                    <td class="ti_col1">
                                                        <cc1:ComboBox CssClass="relative_gt" Enabled="false" ID="ddlShippingAdd" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" runat="server"></cc1:ComboBox>
                                                        <%--<asp:DropDownList ID="ddlShippingAdd" runat="server"></asp:DropDownList>--%>
                                                        <asp:TextBox Visible="false" Enabled="false" ID="txtShippingAdd" MaxLength="135" runat="server" />
                                                    </td>

                                                    <td class="ti_col2">
                                                        <asp:DropDownList ID="ddlLocation" Enabled="false" runat="server"></asp:DropDownList></td>

                                                    <td style="width: 10%" visible="false" id="tdCCCode" runat="server">
                                                        <asp:DropDownList ID="ddlCostCenter" Enabled="false"  runat="server" /></td>

                                                    <td class="ti_col3">
                                                        <asp:TextBox ID="txtorderNo" Enabled="false" MaxLength="18" placeholder="Order No." runat="server"></asp:TextBox></td>

                                                    <td class="ti_col4">
                                                        <asp:TextBox ID="txtOrderDate" Enabled="false" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" /></td>

                                                    <td class="ti_col5">
                                                        <asp:DropDownList ID="ddlTds" Enabled="false" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td class="ti_col6">
                                                        <asp:DropDownList ID="ddlTCS" Enabled="false" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td class="ti_col7">
                                                        <asp:DropDownList ID="ddlRCM" Enabled="false" runat="server">
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
                                                <th class="w8" rowspan="2">Discount</th>
                                                <th class="w6" rowspan="2">Taxable</th>
                                                <th class="w4" rowspan="2">PA</th>
                                                <th class="w5" rowspan="2" id="thRate1" runat="server" visible="false">Tax Rate</th>
                                                <th class="w5" rowspan="2" id="thRate2" runat="server" visible="false">CGST</th>
                                                <th class="w5" rowspan="2" id="thRate3" runat="server" visible="false">SGST</th>
                                                <th class="w5" rowspan="2" id="thRate4" runat="server" visible="false">IGST</th>
                                                <th class="w5" rowspan="2" id="thRate5" runat="server" visible="false">CESS</th>
                                                <th class="w5" rowspan="2" id="thRate6" runat="server" visible="false">ISD</th>
                                                <th class="w2" style="border-bottom-color: transparent" rowspan="2"><span style="visibility: hidden">TitleTitle</span></th>
                                            </tr>
                                            <tr>
                                                <th class="w3">Qty</th>
                                                <th class="w5">Unit</th>
                                                <th class="w3">Qty</th>
                                                <th class="w5">Unit</th>
                                            </tr>
                                        </thead>

                                        <tbody>
                                            <tr class="align_with_combobox">
                                                <td>
                                                    <cc1:ComboBox ID="ddlItemName" runat="server" Enabled="false" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" />
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
                                                    <asp:TextBox ID="txtFree" Enabled="false" MaxLength="8" CssClass="Decimal4" OnTextChanged="txtFree_TextChanged" AutoPostBack="true" runat="server" Text="0"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtRate" Enabled="false" MaxLength="7" runat="server" CssClass="Money" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtItemAmt" MaxLength="7" runat="server" CssClass="Money" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDiscount" Enabled="false" CssClass="Money w60 DiscountJS" Style="float: left;" MaxLength="7" runat="server" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true" />
                                                    <asp:DropDownList ID="ddlDiscount" Enabled="false" CssClass="w40 DiscountJS" Style="float: left;" AutoPostBack="true" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="Rs." />
                                                        <asp:ListItem Value="%" />
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtItemTaxableAmt" MaxLength="7" runat="server" CssClass="Money" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPA" Enabled="false" CssClass="itm_papicker" OnSelectedIndexChanged="ddlPA_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Selected="True" Value="0" Text="No" />
                                                        <asp:ListItem Value="1" Text="Yes" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="dtTax" runat="server" visible="false">
                                                    <asp:TextBox ID="txtTax" MaxLength="3" OnTextChanged="txtTax_TextChanged" AutoPostBack="true" runat="server" CssClass="Money"></asp:TextBox></td>
                                                <td id="dtCGSTAmt" runat="server" visible="false">
                                                    <asp:TextBox ID="txtCGSTAmt" MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                <td id="dtSGSTAmt" runat="server" visible="false">
                                                    <asp:TextBox ID="txtSGSTAmt" MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                <td id="dtIGSTAmt" runat="server" visible="false">
                                                    <asp:TextBox ID="txtIGSTAmt" MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                <td id="dtCESSAmt" runat="server" visible="false">
                                                    <asp:TextBox ID="txtCESSAmt" MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>
                                                <td id="dtddlIsd" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlIsd" runat="server">
                                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 6%; background: transparent">
                                                    <div style="width: 100%; border: 1px solid transparent; height: 25px; border-bottom-color: #eee; border-right-color: #eee;"></div>
                                                </td>
                                            </tr>

                                            <tr id="collapse_me_tr">
                                                <td colspan="11" id="CollapseRemark" style="visibility: hidden; border-right-color: transparent">
                                                    <asp:TextBox ID="txtItemRemark" MaxLength="145" runat="server" placeholder="Enter Remark..." />
                                                </td>

                                                <td style="position: relative; border-bottom-color: transparent; border-right-color: transparent; text-align: center; width: 6%">
                                                    <div style="position: absolute; top: -58px; width: 57px; padding-bottom: 4px; border-bottom: 1px solid transparent">
                                                        <a href="javascript:void(0)" id="collapse_tr" class="sctooltip"><i class="fa fa-cog"></i><span class="sctooltiptext">Add Item Remark</span></a><br />
                                                        <asp:Button ID="btnAddItemDetail" CssClass="btn btn-xs  btn-primary" Text="Add" OnClick="btnAddItemDetail_Click" Enabled="false" runat="server" />
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

                                    <asp:GridView ID="gvItemDetail" CssClass="gstr_sales_item table-bordered first_tr_hide" AutoGenerateColumns="false" OnRowCommand="gvItemDetail_RowCommand" OnRowDataBound="gvItemDetail_RowDataBound" runat="server">
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
                                                        <th rowspan="2" class="w5">PA</th>
                                                        <th rowspan="2" class="w5">Tax Rate</th>
                                                        <th rowspan="2" class="w5">CGST</th>
                                                        <th rowspan="2" class="w5">SGST</th>
                                                        <th rowspan="2" class="w5">IGST</th>
                                                        <th rowspan="2" class="w5">CESS</th>
                                                        <th rowspan="2" class="w5">ISD</th>
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
                                            <asp:BoundField DataField="PADesc" />
                                            <asp:BoundField DataField="TaxRate" />
                                            <asp:BoundField DataField="CGSTTaxAmt" />
                                            <asp:BoundField DataField="SGSTTaxAmt" />
                                            <asp:BoundField DataField="IGSTTaxAmt" />
                                            <asp:BoundField DataField="CESSTaxAmt" />
                                            <asp:BoundField DataField="ISDDesc" />
                                            <asp:TemplateField ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" CommandName="EditItems" CommandArgument='<%#Container.DataItemIndex %>' Text="Edit" CssClass="btn btn-primary btn-sxs" runat="server" />
                                                    <asp:Button ID="btnDelete" CssClass="btn btn-danger btn-sxs" CommandName="RemoveItems" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#1c75bf" ForeColor="#FAFCFD" />
                                    </asp:GridView>
                                </div>
                                <asp:HiddenField ID="hfHSNSACCode" runat="server" />
                                <asp:HiddenField ID="hfGoodsAndServiceInd" runat="server" />
                                <asp:HiddenField ID="hfOriginalQty" runat="server" />
                                <asp:HiddenField ID="hfVoucherNo" runat="server" />
                                <asp:HiddenField ID="hfVoucherDate" runat="server" />
                                <asp:HiddenField ID="hfPurchaseSalesRecordID" runat="server" />
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
                                                <asp:LinkButton ID="btnShowTransport" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowTransport_Click" runat="server"> &#xf067; Transportation Details</asp:LinkButton>
                                            </div>
                                            <asp:Panel ID="pnlTransport" runat="server" Visible="false" CssClass="grid_collapse">
                                                <table class="table-bordered" style="width: 600px">
                                                    <tr class="inf_head hidden">
                                                        <td colspan="100%" class="checkbox_container">
                                                            <asp:CheckBox ID="CbTransDetail" OnCheckedChanged="CbTransDetail_CheckedChanged" AutoPostBack="true" Text="Transportation Details Available" runat="server" /></td>
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

                                        <%--<div class="col-sm-12 mb10 grid_collapse_container">
                                            <div class="grid_collapse_opener">
                                                <asp:LinkButton ID="btnShowFreeItem" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowFreeItem_Click" runat="server"> &#xf067; Additional Free Items</asp:LinkButton>
                                            </div>
                                            <asp:Panel ID="divFree" runat="server" Visible="false" CssClass="grid_collapse">
                                                <table class="table table-voucher gstr_sales_other table-bordered tbl_Free tdb0 thp0" id="Table4" runat="server" style="width: 600px">
                                                    <tbody>
                                                        <tr class="align_with_combobox">
                                                            <td class="other_col1">
                                                                <cc1:combobox id="ddlFreeItemName" onselectedindexchanged="ddlFreeItemName_SelectedIndexChanged" autopostback="true" runat="server" width="250px" placeholder="p" cssclass="relative_gt" autocompletemode="SuggestAppend" casesensitive="False" style="text-transform: uppercase"></cc1:combobox>
                                                            </td>
                                                            <td class="other_col2">
                                                                <asp:DropDownList ID="ddlFreeUnit" Enabled="false" runat="server" /></td>
                                                            <td class="other_col3 r">
                                                                <asp:TextBox ID="txtFreeQty" MaxLength="8" placeholder="Qty" CssClass="Decimal4" runat="server" /></td>
                                                            <td class="w9 c" style="width: 10px">
                                                                <asp:Button ID="btnFreeAdd" CssClass="btn btn-sxs btn-primary btncss" OnClick="btnFreeAdd_Click" Text="Add" runat="server" />
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
                                        </div>--%>
                                        <div class="col-sm-12 mb10 grid_collapse_container">
                                            <div class="grid_collapse_opener">
                                                <asp:LinkButton ID="btnShoOtherCharge" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShoOtherCharge_Click" runat="server"> &#xf067; Other Charges</asp:LinkButton>
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
                                                                <asp:Button ID="btnAdd" CssClass=" btn btn-sxs  btn-primary" Text="Add" OnClick="btnAdd_Click" Enabled="false" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                                <%--GridView For Other Charge--%>
                                                <asp:GridView ID="gvotherCharge" CssClass="table table-voucher gstr_sales_other table-bordered" AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gvotherCharge_RowCommand" runat="server" Style="width: 600px">
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
                                    </div>




                                    <div class="amount_table_right">

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
                                                            <asp:TextBox ID="txtGross" Enabled="false" runat="server" placeholder="Net" CssClass="Money"></asp:TextBox></td>
                                                        <td class="amount_col2">
                                                            <asp:TextBox ID="txtTaxable" Enabled="false" runat="server" placeholder="Tax" CssClass="Money"></asp:TextBox></td>
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
                                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" ValidationGroup="a" runat="server" Enabled="false" Text="Update" CssClass="btn btn-primary btn-space-right" Visible="false" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        </div>
                                        <span><b>*PA  </b>- Provisional Assesment, </span>
                                        <span><b>*ISD </b>- Input Service Distributor, </span>
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
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <%--<asp:PostBackTrigger ControlID="btnYes" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>
