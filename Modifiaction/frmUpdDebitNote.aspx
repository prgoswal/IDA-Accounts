<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdDebitNote.aspx.cs" Inherits="Modifiaction_frmUpdDebitNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();
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

                <h3 class="text-center head">DEBIT NOTE UPDATION <span class="invoiceHead">
                    <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span> </h3>

                <div class="row">

                    <div class="col-xs-12">
                        <div class="panel panel-default">

                            <div class="panel-body">

                                <div class="col-xs-12">
                                    <div style="margin-bottom: 10px;">
                                        <asp:TextBox ID="txtVoucherNo" CssClass="numberonly" MaxLength="7" placeholder="Search Voucher No." runat="server" />
                                        <asp:LinkButton runat="server" ID="btngo" OnClick="btngo_Click" CssClass="btn btn-sxs btn-primary"><i class="fa r fa-search"></i>Go</asp:LinkButton>
                                    </div>
                                    <table class="credit_table table-bordered" style="width: 100%">
                                        <tr>
                                            <td colspan="5" class="text-center inf_head">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <th style="width: 10%">Voucher Date</td>
                                            <th style="width: 20%">Debit Note Against</th>
                                                <th style="width: 40%">Party Account</th>
                                                <th style="width: 15%">GSTIN</th>
                                                <th id="thCCCode" runat="server" visible="false" style="width: 15%">Cost Centre</th>
                                        </tr>
                                        <tr>
                                            <td class="">
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker Required" data-group="Save" MaxLength="10" placeholder=" DD/MM/YYYY" runat="server" /></td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlAgainst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAgainst_SelectedIndexChanged" data-group="Save">
                                                    <asp:ListItem Text="--Select--" Value="0" />
                                                    <asp:ListItem Text="Sales" Value="1" />
                                                    <asp:ListItem Text="Purchase" Value="2" />
                                                </asp:DropDownList>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlPartyAccount" CssClass="chzn-search Required" data-group="Save" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyAccount_SelectedIndexChanged" runat="server"></asp:DropDownList></td>
                                            <%--<cc1:ComboBox ID="ddlPartyAccount" CssClass="inpt autoCopleteBox relative_gt" runat="server" Width="250px" AutoPostBack="true" placeholder="p" OnSelectedIndexChanged="ddlPartyAccount_SelectedIndexChanged" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>--%>
                                            <td class="">
                                                <asp:DropDownList ID="ddlGstinNo" CssClass="" data-group="Save" runat="server"></asp:DropDownList></td>

                                            <td class="" id="tdCCCode" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlCostCenter" CssClass="" data-group="Save" runat="server"></asp:DropDownList></td>
                                        </tr>
                                    </table>

                                    <table class="original_invoice_table  table-bordered" style="float: left; margin-bottom: 25px">
                                        <tr>
                                            <td colspan="4" class="text-center inf_head">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <th class="original_invoice_col1 c">GST Impact Taken</th>
                                            <th class="c" colspan="2">Voucher No. and&nbsp; Date</th>
                                            <th class="original_invoice_col3 "></th>
                                        </tr>
                                        <tr>
                                            <td class="original_invoice_col1 c">
                                                <asp:CheckBox ID="CBGSTImapactTaken" runat="server" /></td>
                                            <td class="original_invoice_col2">
                                                <asp:TextBox ID="txtInVoiceNo" CssClass="numberonly" MaxLength="7" placeholder="Number" runat="server" /></td>
                                            <td class="original_invoice_col3">
                                                <asp:TextBox ID="txtInvoiceDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" runat="server" /></td>
                                            <td class="original_invoice_col4 c">
                                                <asp:LinkButton ID="btnSearchInvoice" CssClass="btn btn-sxs btn-primary" runat="server"><i class="fa r fa-search"></i>Search</asp:LinkButton></td>
                                        </tr>

                                    </table>
                                    <table class="original_invoice_table  table-bordered" style="margin-left: 10px; float: right; margin-bottom: 25px">
                                        <tr>
                                            <td colspan="4" class="text-center inf_head">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <th>Income/Expense Head</th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAccountHead" CssClass="chzn-search Required" data-group="Save" runat="server"></asp:DropDownList></td>
                                        </tr>

                                    </table>
                                    <div id="divItemData" runat="server">
                                        <table class="table-bordered">

                                            <tr class="inf_head">
                                                <th class="c item_invoice_col1" rowspan="2">Item</th>
                                                <th class="c item_invoice_col2" rowspan="2">HSN</th>
                                                <th class="c item_invoice_col3" rowspan="2">Difference<br />
                                                    Qty</th>
                                                <th class="c item_invoice_col4" rowspan="2">Unit</th>
                                                <th class="c item_invoice_col4" rowspan="2">Difference
                                                    <br />
                                                    Rate</th>
                                                <%--<th class="c item_invoice_col5" rowspan="2" >Difference<br/>Rate</th>--%>
                                                <%--<th class="c item_invoice_col6" rowspan="2" >Taxable <br />Amount</th>--%>
                                                <th class="c item_invoice_col6" rowspan="2">Difference
                                                    <br />
                                                    Amount</th>
                                                <th class="c item_invoice_col7" rowspan="2">Tax Rate</th>
                                                <th class="c item_invoice_col8 hidden">CGST</th>
                                                <th class="c item_invoice_col9">CGST</th>
                                                <th class="c item_invoice_col10 hidden">SGST</th>
                                                <th class="c item_invoice_col9">SGST</th>
                                                <th class="c item_invoice_col12 hidden">IGST</th>
                                                <th class="c item_invoice_col9">IGST</th>
                                                <th class="c item_invoice_col14 hidden">CESS</th>
                                                <th class="c item_invoice_col9">CESS</th>
                                                <th class="c item_invoice_col16" rowspan="2">Save</th>
                                            </tr>
                                            <tr class="inf_head">
                                                <th class="c hidden item_invoice_col8">Tax</th>
                                                <th class="c item_invoice_col9">Amount</th>
                                                <th class="c hidden item_invoice_col10">Tax</th>
                                                <th class="c item_invoice_col11">Amount</th>
                                                <th class="c hidden item_invoice_col12">Tax</th>
                                                <th class="c item_invoice_col13">Amount</th>
                                                <th class="c hidden item_invoice_col14">Tax</th>
                                                <th class="c item_invoice_col15">Amount</th>
                                            </tr>
                                            <tr>
                                                <td class="item_invoice_col1">
                                                    <asp:TextBox CssClass="numberonly" ID="txtItem" runat="server" type="text" placeholder="Item" Enabled="false" /></td>
                                                <td class="item_invoice_col2">
                                                    <asp:TextBox CssClass="numberonly" ID="txtHSN" runat="server" type="text" placeholder="HSN" Enabled="false" /></td>
                                                <td class="item_invoice_col3">
                                                    <asp:TextBox CssClass="Decimal4" ID="txtQty" OnTextChanged="txtQty_TextChanged" AutoPostBack="true" runat="server" type="text" placeholder="Qty" /></td>
                                                <td class="item_invoice_col4">
                                                    <asp:DropDownList ID="ddlUnit" runat="server" Enabled="false" /></td>
                                                <td class="item_invoice_col5">
                                                    <asp:TextBox CssClass="Money" ID="txtRate" runat="server" type="text" placeholder="Rate" OnTextChanged="txtRate_TextChanged" AutoPostBack="true" /></td>
                                                <%--<td class="item_invoice_col6" ><asp:TextBox CssClass="Money" ID="txtTaxableAmt" runat="server" type="text" placeholder="Amount"/></td>--%>
                                                <td class="item_invoice_col7">
                                                    <asp:TextBox CssClass="Money" ID="txtDiffAmt" runat="server" type="text" placeholder="Amount" Enabled="false" /></td>
                                                <td class="item_invoice_col8">
                                                    <asp:TextBox CssClass="Money" ID="txtTaxRate" runat="server" type="text" placeholder="Tax" Enabled="false" /></td>
                                                <td class="hidden item_invoice_col9">
                                                    <asp:TextBox CssClass="Money" ID="txtCGSTTax" runat="server" type="text" placeholder="Tax" Enabled="false" /></td>
                                                <td class="item_invoice_col10">
                                                    <asp:TextBox CssClass="Money" ID="txtCGSTAmt" runat="server" type="text" placeholder="Amount" Enabled="false" /></td>
                                                <td class="hidden item_invoice_col11">
                                                    <asp:TextBox CssClass="Money" ID="txtSGSTTax" runat="server" type="text" placeholder="Tax" Enabled="false" /></td>
                                                <td class="item_invoice_col12">
                                                    <asp:TextBox CssClass="Money" ID="txtSGSTAmt" runat="server" type="text" placeholder="Amount" Enabled="false" /></td>
                                                <td class="hidden item_invoice_col13">
                                                    <asp:TextBox CssClass="Money" ID="txtIGSTTax" runat="server" type="text" placeholder="Tax" Enabled="false" /></td>
                                                <td class="item_invoice_col14">
                                                    <asp:TextBox CssClass="Money" ID="txtIGSTAmt" runat="server" type="text" placeholder="Amount" Enabled="false" /></td>
                                                <td class="hidden item_invoice_col15">
                                                    <asp:TextBox CssClass="Money" ID="txtCessTax" runat="server" type="text" placeholder="Tax" Enabled="false" /></td>
                                                <td class="item_invoice_col16">
                                                    <asp:TextBox CssClass="Money" ID="txtCessAmt" runat="server" type="text" placeholder="Amount" Enabled="false" /></td>
                                                <th class="item_invoice_col17 c">
                                                    <asp:LinkButton ID="btnSaveITEM" CssClass="btn btn-sxs btn-primary" OnClick="btnSaveITEM_Click" Enabled="false" runat="server"><i class="fa r fa-floppy-o"></i>Save</asp:LinkButton>
                                                </th>
                                            </tr>

                                        </table>
                                        <table class="item_invoice_table_gridview table-bordered hidden">

                                            <tr class="inf_head">
                                                <th class="c item_invoice_gridview_col1" rowspan="2">Item</th>
                                                <th class="c item_invoice_gridview_col2" rowspan="2">HSN</th>
                                                <th class="c item_invoice_gridview_col3" rowspan="2">Qty</th>
                                                <th class="c item_invoice_gridview_col4" rowspan="2">Unit</th>
                                                <th class="c item_invoice_gridview_col5" rowspan="2">Rate</th>
                                                <th class="c item_invoice_gridview_col6" rowspan="2">Taxable
                                                    <br />
                                                    Amount</th>
                                                <%-- <th class="c item_invoice_gridview_col7" rowspan="2" >Difference <br />Amount</th>--%>
                                                <th class="c item_invoice_gridview_col8" rowspan="2">Tax Rate</th>
                                                <th class="c item_invoice_gridview_col9">CGST</th>
                                                <th class="c item_invoice_gridview_col10">CGST</th>
                                                <th class="c item_invoice_gridview_col11">SGST</th>
                                                <th class="c item_invoice_gridview_col12">SGST</th>
                                                <th class="c item_invoice_gridview_col13">IGST</th>
                                                <th class="c item_invoice_gridview_col14">IGST</th>
                                                <th class="c item_invoice_gridview_col15">CESS</th>
                                                <th class="c item_invoice_gridview_col16">CESS</th>
                                                <th class="c item_invoice_gridview_col17" rowspan="2">Edit</th>
                                                <th class="c item_invoice_gridview_col16" rowspan="2">CESS</th>
                                                <th class="c item_invoice_gridview_col16" rowspan="2">CESS</th>
                                            </tr>
                                            <tr class="inf_head">
                                                <th class="c item_invoice_gridview_col9">Tax</th>
                                                <th class="c item_invoice_gridview_col10">Amount</th>
                                                <th class="c item_invoice_gridview_col12">Tax</th>
                                                <th class="c item_invoice_gridview_col11">Amount</th>
                                                <th class="c item_invoice_gridview_col13">Tax</th>
                                                <th class="c item_invoice_gridview_col14">Amount</th>
                                                <th class="c item_invoice_gridview_col15">Tax</th>
                                                <th class="c item_invoice_gridview_col16">Amount</th>
                                            </tr>

                                        </table>
                                        <asp:HiddenField ID="hfItemID" runat="server" />
                                        <asp:HiddenField ID="hfItemUnitID" runat="server" />
                                        <asp:HiddenField ID="hfCGSTTax" runat="server" />
                                        <asp:HiddenField ID="hfSGSTTax" runat="server" />
                                        <asp:HiddenField ID="hfIGSTTax" runat="server" />
                                        <asp:HiddenField ID="hfCESSTax" runat="server" />
                                        <asp:HiddenField ID="hfExpenseIncomeHead" runat="server" />

                                        <%--<asp:GridView ID="gvFinalItemDetail" CssClass="gstr_sales_item table-bordered" AutoGenerateColumns="false" OnRowCommand="gvFinalItemDetail_RowCommand" runat="server">
                                            <Columns>
                                                <asp:BoundField HeaderText="ItemName" DataField="ItemName" />
                                                <asp:BoundField HeaderText="HsnCode" DataField="HSNSACCode" />
                                                <asp:BoundField HeaderText="ItemQty" DataField="ItemQty" />
                                                <asp:BoundField HeaderText="ItemUnit" DataField="ItemUnit" />
                                                <asp:BoundField HeaderText="Rate" DataField="ItemRate" />
                                                <asp:BoundField HeaderText="Amount" DataField="ItemAmt" />
                                                <asp:BoundField HeaderText="Tax Rate" DataField="TaxRate" />
                                                <asp:BoundField HeaderText="CGST" DataField="CGSTTaxAmt" />
                                                <asp:BoundField HeaderText="SGST" DataField="SGSTTaxAmt" />
                                                <asp:BoundField HeaderText="IGST" DataField="IGSTTaxAmt" />
                                                <asp:BoundField HeaderText="CESS" DataField="CESSTaxAmt" />

                                                <%-- <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="CompanyID" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="BranchID" />--%>
                                        <%-- <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="OriginalInvoiceSeries" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="OriginalInvoiceNo" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="OriginalInvoiceDate" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="ItemID" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="ItemUnitID" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="CGSTTax" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="SGSTTax" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="IGSTTax" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="CESSTax" />

                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="DiscountAmt" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="NetAmt" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="ItemRemark" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="GoodsServiceInd" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="DiscountValue" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="DiscountType" />
                                                <%--  <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="ISDApplicable" />
                                                    <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="PA" />

                                                <asp:TemplateField ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDel" CssClass="btn btn-danger btn-sxs" CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" /> 
                                                          <asp:Button ID="btnEdit" CommandName="EditItemRow" CommandArgument='<%#Container.DataItemIndex %>' Text="Edit" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 

                                            </Columns>
                                            <HeaderStyle BackColor="#1c75bf" ForeColor="#FAFCFD" />
                                        </asp:GridView>--%>

                                        <asp:HiddenField ID="hfRowInd" runat="server" />
                                        <asp:GridView ID="gvItemDetail" CssClass="table-bordered 2" runat="server" OnRowCommand="gvItemDetail_RowCommand" AutoGenerateColumns="false" Style="margin-top: 15px">
                                            <Columns>
                                                <%--  <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="CompanyID" />
                                                <asp:BoundField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataField="BranchID" />--%>
                                                <%--<asp:BoundField HeaderText="S" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden" DataField="InvoiceNo" />
                                                <asp:BoundField HeaderText="S" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden" DataField="InvoiceDate" />--%>
                                                <asp:BoundField ItemStyle-Width="30%" HeaderText="Item" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="ItemName" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="HSN" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="HSNSACCode" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="Qty" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="ItemQty" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="Free" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="FreeQty" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="Unit" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="ItemUnit" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="Item Rate" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="item_invoice_gridview_col5" DataField="ItemRate" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="Taxable Amt" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="ItemAmt" />
                                                <%--<asp:BoundField ItemStyle-CssClass="item_invoice_gridview_col7"  DataField="DiffAmt" />--%>
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="Tax Rate" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="TaxRate" />
                                                <asp:BoundField ItemStyle-Width="0%" HeaderText="CGST Tax" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden " DataField="CGSTTax" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="CGST Amt" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="CGSTTaxAmt" />
                                                <asp:BoundField ItemStyle-Width="0%" HeaderText="SGST Tax" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden " DataField="SGSTTax" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="SGST Amt" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="SGSTTaxAmt" />
                                                <asp:BoundField ItemStyle-Width="0%" HeaderText="IGST Tax" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden " DataField="IGSTTax" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="IGST Amt" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="IGSTTaxAmt" />
                                                <asp:BoundField ItemStyle-Width="0%" HeaderText="CESS Tax" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden " DataField="CESSTax" />
                                                <asp:BoundField ItemStyle-Width="5%" HeaderText="CESS Amt" HeaderStyle-CssClass="inf_head" ItemStyle-CssClass="" DataField="CESSTaxAmt" />

                                                <asp:BoundField ItemStyle-Width="0%" HeaderText="CESS Amt" HeaderStyle-CssClass="hidden inf_head" ItemStyle-CssClass="hidden" DataField="IncomeExpenseCode" />


                                                <%--<asp:BoundField DataField="FreeQty" />--%>
                                                <%--<asp:BoundField DataField="ItemAmount" />--%>
                                                <%--<asp:BoundField DataField="PADesc" />--%>
                                                <%--<asp:BoundField DataField="ISDDesc" />--%>

                                                <asp:TemplateField ItemStyle-Width="6%" HeaderText="Edit" HeaderStyle-CssClass="inf_head">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" CssClass="btn btn-sxs btn-info" CommandName="EditItemRow" CommandArgument='<%#Container.DataItemIndex %>' Text="Edit" runat="server" />
                                                        <asp:Button ID="btnDel" CssClass="btn btn-danger btn-sxs" CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:HiddenField ID="hfOrgRate" runat="server" />
                                    <asp:HiddenField ID="hfOrgQty" runat="server" />
                                    <table class="credit_amount_narration_table table-bordered">
                                        <tr>
                                            <td colspan="4" class="text-center inf_head">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <th style="width: 15%">Difference Amount</th>
                                            <th style="width: 53%">Narration</th>
                                            <th style="width: 22%">Issue Reason</th>
                                            <th style="width: 20%">Pre GST</th>
                                        </tr>
                                        <tr>
                                            <td class="">
                                                <%--<input type="text" placeholder="Amount">--%>
                                                <asp:TextBox ID="txtDrAmount" MaxLength="7" placeholder="Amount" runat="server" />
                                            </td>
                                            <td class=""><%--<input type="text" placeholder="Narration">--%>
                                                <cc1:ComboBox ID="ddlNarration" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" DropDownStyle="Simple" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlresion" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlPreIssue" runat="server">
                                                    <asp:ListItem Text="Yes" Value="1" />
                                                    <asp:ListItem Text="No" Value="2" Selected="True" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-left">

                                            <asp:Button ID="btnCancel" Enabled="false" Text="Cancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" Visible="false" />
                                        </div>
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Text="Update" class="btn btn-primary btn-space-right" Visible="false" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


