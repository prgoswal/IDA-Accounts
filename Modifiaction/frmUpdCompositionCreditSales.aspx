<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdCompositionCreditSales.aspx.cs" Inherits="Modifiaction_frmUpdSales" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function LoadAllScript() {
            LoadBasic();
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

                <h3 class="text-center head">UPDATE SALES VOUCHER 
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>

                <div class="container_fluid">
                    <div class="row">
                        <asp:Panel runat="server" Visible="false" ID="PnlAllContent"></asp:Panel>

                        <div class="panel panel-default pmb0">
                            <div class="panel-heading pb0">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlSeraching" runat="server">

                                        <asp:Panel ID="pnlInvoiceseries" CssClass="col-xs-2" runat="server">
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

                                    <div class="col-xs-12">


                                        <br />

                                        <table class="table table-voucher gstr_sales_invoice table-bordered tdb0 thp0">
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
                                                    <%--<th class="ti_col5">GSTIN</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="align_with_combobox">
                                                    <td class="ti_col3">
                                                        <cc1:ComboBox Enabled="false"
                                                            AutoCompleteMode="SuggestAppend"
                                                            AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlIncomeHead_SelectedIndexChanged"
                                                            CssClass="relative_gt"
                                                            ID="ddlIncomeHead" runat="server"
                                                            Width="250px"
                                                            CaseSensitive="False"
                                                            Style="text-transform: uppercase">
                                                        </cc1:ComboBox>
                                                    </td>
                                                    <td class="ti_col6">
                                                        <asp:DropDownList ID="ddlInvoiceSeries" Enabled="false" AutoPostBack="true" runat="server" />
                                                        <asp:TextBox ID="txtInvoiceSeries" CssClass="text-uppercase Series" MaxLength="10" Enabled="false" placeholder="Invoice Series" runat="server" />
                                                    </td>
                                                    <td class="ti_col1">
                                                        <asp:TextBox ID="txtinvoiceNo" MaxLength="7" Enabled="false" runat="server" CssClass="numberonly"></asp:TextBox></td>
                                                    <td class="ti_col2">
                                                        <asp:TextBox ID="txtInvoiceDate" CssClass="datepicker" placholder="DD/MM/YYYY" MaxLength="10" Style="width: 100%" runat="server" /></td>

                                                    <td class="ti_col4">
                                                        <cc1:ComboBox ID="ddlSalesto" Enabled="false" AutoCompleteMode="SuggestAppend" CssClass="relative_gt" CaseSensitive="False" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesto_SelectedIndexChanged1" Style="text-transform: uppercase"></cc1:ComboBox>
                                                        <asp:TextBox ID="txtSalesto" Enabled="false" AutoPostBack="true" OnTextChanged="txtSalesto_TextChanged" CssClass="text-uppercase" MaxLength="45" Visible="false" runat="server" />
                                                    </td>


                                                </tr>
                                            </tbody>
                                        </table>

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
                                                        <th class="ti_col4">Order&nbsp;Date</th>
                                                        <%--<th class="ti_col5">TDS</th>
                                                        <th class="ti_col6">TCS</th>
                                                        <th class="ti_col7">RCM</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="ti_col1">
                                                            <cc1:ComboBox CssClass="relative_gt" ID="ddlShippingAdd" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" runat="server"></cc1:ComboBox>
                                                            <asp:TextBox Visible="false" ID="txtShippingAdd" MaxLength="135" runat="server" />
                                                        </td>

                                                        <td class="ti_col2">
                                                            <asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>

                                                        <td class="ti_col3">
                                                            <asp:TextBox ID="txtorderNo" MaxLength="18" runat="server"></asp:TextBox></td>

                                                        <td class="ti_col4">
                                                            <asp:TextBox ID="txtOrderDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" /></td>

                                                        <%--  <td class="ti_col5">
                                                            <asp:DropDownList ID="ddlTds" runat="server">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>--%>

                                                        <%--   <td class="ti_col6">
                                                            <asp:DropDownList ID="ddlTCS" runat="server">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>--%>

                                                        <%--       <td class="ti_col7">
                                                            <asp:DropDownList ID="ddlRCM" runat="server">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>--%>
                                                    </tr>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>



                                    <%-- Item Details --%>
                                    <style>
                                        .supd_table tr td:nth-child(1) {
                                            display: none;
                                        }

                                        .supd_table .ajax__combobox_textboxcontainer {
                                            display: table-cell !important;
                                        }
                                    </style>
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvItemDetail" CssClass="gstr-table table-bordered supd_table mb10" AutoGenerateColumns="false" OnRowCommand="gvItemDetail_RowCommand" OnRowDataBound="gvItemDetail_RowDataBound" runat="server">
                                            <HeaderStyle CssClass="inverse" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr class="inf_head">
                                                            <th rowspan="2" class="w22">Item Name</th>
                                                            <th colspan="2">Primary</th>
                                                            <th colspan="2">Secondary</th>
                                                            <th rowspan="2" class="w5">Free</th>
                                                            <th rowspan="2" class="w6">Rate</th>
                                                            <th rowspan="2" class="w6">Net Amount</th>
                                                            <th colspan="3">Discount</th>
                                                            <th rowspan="2" class="w6">Taxable </th>
                                                            <%--   <th rowspan="2" class="w4">PA</th>
                                                            <th rowspan="2" class="w5">Tax Rate</th>
                                                            <th rowspan="2" class="w4">CGST</th>
                                                            <th rowspan="2" class="w4">SGST</th>
                                                            <th rowspan="2" class="w4">IGST</th>
                                                            <th rowspan="2" class="w4">CESS</th>
                                                            <th rowspan="2" class="w4">ISD</th>--%>
                                                            <th rowspan="2" class="w8">&nbsp;</th>
                                                        </tr>
                                                        <tr class="inf_head">
                                                            <th class="w6">Qty.</th>
                                                            <th class="w7">Unit</th>
                                                            <th class="w6">Qty.</th>
                                                            <th class="w7">Unit</th>
                                                            <th class="w6">Value</th>
                                                            <th class="w7">Type</th>
                                                            <th class="w6">Amt</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <EditItemTemplate>
                                                        <tr class="align_with_combobox">
                                                            <td></td>
                                                            <td class="">
                                                                <cc1:ComboBox ID="ddlItemName" runat="server" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true" Width="50px" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
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
                                                            <%--<td class="c">
                                                                <asp:DropDownList ID="ddlPA" CssClass="itm_papicker" OnSelectedIndexChanged="ddlPA_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                    <asp:ListItem Value="0" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                            <%--<td class="r"><asp:TextBox ID="txtTax" Text='<%#Eval("TaxRate") %>' MaxLength="3" Enabled="false" OnTextChanged="txtTax_TextChanged" AutoPostBack="true" runat="server" CssClass="Money"></asp:TextBox></td>--%>
                                                            <%--<td class="r"><asp:TextBox ID="txtCGSTAmt" Text='<%#Eval("CGSTTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>--%>
                                                            <%--<td class="r"><asp:TextBox ID="txtSGSTAmt" Text='<%#Eval("SGSTTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>--%>
                                                            <%--<td class="r">                                                            <asp:TextBox ID="txtIGSTAmt" Text='<%#Eval("IGSTTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>--%>
                                                            <%--<td class="r"><asp:TextBox ID="txtCESSAmt" Text='<%#Eval("CESSTaxAmt") %>' MaxLength="7" runat="server" Enabled="false" CssClass="Money"></asp:TextBox></td>--%>
                                                            <%--<td id="dtddlIsd" runat="server">
                                                                <asp:DropDownList ID="ddlIsd" runat="server">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                            <td class="c bbct">
                                                                <asp:LinkButton ID="btnSave" Text="Save" CommandName="RowUpdate" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn " runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr id="collapse_me_tr">
                                                            <td></td>
                                                            <td colspan="19">
                                                                <asp:TextBox ID="txtItemRemark" Text='<%#Eval("ItemRemark") %>' MaxLength="145" runat="server" placeholder="Enter Remark..." /></td>

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
                                                        <%--<td class="c"><asp:Label ID="ddlPA" Text='<%#Eval("PADesc") %>' runat="server"></asp:Label></td>--%>
                                                        <%--<td class="r"><asp:Label ID="txtTax" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label></td>--%>
                                                        <%--<td class="r"><asp:Label ID="txtCGSTAmt" Text='<%#Eval("CGSTTaxAmt") %>' runat="server"></asp:Label></td>--%>
                                                        <%--<td class="r"><asp:Label ID="txtSGSTAmt" Text='<%#Eval("SGSTTaxAmt") %>' runat="server"></asp:Label></td>--%>
                                                        <%--<td class="r"><asp:Label ID="txtIGSTAmt" Text='<%#Eval("IGSTTaxAmt") %>' runat="server"></asp:Label></td>--%>
                                                        <%--<td class="r"><asp:Label ID="txtCESSAmt" Text='<%#Eval("CESSTaxAmt") %>' runat="server"></asp:Label></td>--%>
                                                        <%--<td class="c"><asp:Label ID="ddlIsd" Text='<%#Eval("ISDDesc") %>' runat="server"></asp:Label></td>--%>
                                                        <td class="c">
                                                            <asp:LinkButton ID="btnEdit" Text="Edit" CommandName="RowEdit" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-primary add_btn" runat="server"></asp:LinkButton>
                                                            <asp:LinkButton ID="btnDelete" Text="&nbsp;Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <style>
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

                                            <div class="col-sm-12 mb10 grid_collapse_container">
                                                <div class="grid_collapse_opener">
                                                    <asp:LinkButton ID="btnShowFreeItem" CssClass="btn-asp-text p1012 point fa-asp text-left" OnClick="btnShowFreeItem_Click" runat="server"> &#xf067; Additional Free Items</asp:LinkButton>
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
                                                                    <asp:TextBox ID="txtFreeQty" MaxLength="8" CssClass="Decimal4" runat="server" /></td>
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
                                            </div>
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
                                                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass=" btn btn-sxs  btn-primary" Text="Add" />
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
                                                            <%--<th class="amount_col2">Tax</th>--%>
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
                                                            <%--<td class="amount_col2">
                                                                <asp:TextBox ID="txtTaxable" Enabled="false" runat="server" placeholder="Tax" CssClass="Money"></asp:TextBox></td>--%>
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


                                <%-- Finaly Savings & Clear --%>
                                <div class="panel-footer">
                                    <div class="row">
                                        <div class="col-xs-12">


                                            <div class="pull-left">

                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                            </div>
                                            <div class="pull-right">
                                                <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                <%--<cc1:MaskedEditValidator  CssClass="text-danger" ID="MaskedEditValidator1" runat="server" ControlToValidate="txtTransportDate" ControlExtender="MaskedEditExtender1" IsValidEmpty="true" EmptyValueMessage="Input Date and Time" InvalidValueMessage="Invalid Date And Time" ValidationGroup="a" ></cc1:MaskedEditValidator>--%>
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" ValidationGroup="a" runat="server" Text="Update" class="btn btn-primary btn-space-right" />
                                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                            </div>
                                            <span><b>*PA  </b>- Provisional Assesment, </span>
                                            <span><b>*ISD </b>- Input Service Distributor, </span>
                                            <span><b>*RCM </b>- Reverse Charge Mechanism  </span>
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
            <asp:HiddenField ID="hfSaleInvoiceManually" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <%--<asp:PostBackTrigger ControlID="btnYes" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>

