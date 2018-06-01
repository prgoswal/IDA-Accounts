<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmSupplierAdvReceived.aspx.cs" Inherits="Vouchers_frmSupplierAdvReceived" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

                <h3 class="text-center head">CUSTOMER ADVANCE RECEIVED 
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" />
                    </span>
                </h3>
                <style>
                   
                </style>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                    <table class=" table-bordered table_sar mb20" style="margin-bottom:10px">
                                       
                                        <tr  class="inf_head">
                                            <th  class="inf_head" style="width:20%">Voucher Date</th>
                                            <th  class="inf_head" style="width:30%">Cash/Bank Account</th>
                                            <th  class="inf_head" style="width:30%">Account Head</th>
                                            <th  class="inf_head" style="width:20%">GSTIN</th>
                                        </tr>
                                        <tr>
                                            <td class="">
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker Required" runat="server" data-group="Save" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlCashBankAccount" CssClass="Required" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankAccount_SelectedIndexChanged" placeholder="Select Cash/Bank Account" data-group="Save" Style="width: 100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="txtAccountHead" CssClass="chzn-select pull-left" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged"></asp:DropDownList>

                                                <%-- <cc1:combobox id="txtAccountHead"
                                                    autocompletemode="SuggestAppend"
                                                    cssclass="inpt autoCopleteBox relative_gt"
                                                    runat="server" width="250px"
                                                    autopostback="true" placeholder="Account"
                                                    casesensitive="False"
                                                    style="text-transform: uppercase">
                                                    </cc1:combobox>--%> 

                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlGSTINNo" CssClass="" data-group="a" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>

                                    <table class=" table-bordered table_sar2 mb20" style="width: 100%">

                                        <tbody>
                                            <tr class="inf_head">
                                                <th colspan="7">Item Detail</th>
                                            </tr>
                                            <tr>
                                                <td class="table_sar2_col1" style="width: 35%">
                                                    <asp:DropDownList ID="ddlItem" CssClass="chzn-select pull-left" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" data-group="a" runat="server" Style="width: 50%!important;"></asp:DropDownList>

                                                    <%--<cc1:combobox id="ddlItem" runat="server"
                                                    width="20px" placeholder="p" cssclass="relative_gt"
                                                    autocompletemode="SuggestAppend" casesensitive="False"
                                                    style="text-transform: uppercase">
                                                     </cc1:combobox>--%>
                                                </td>
                                                <td class="table_sar2_col4" style="width: 8%">
                                                    <asp:DropDownList ID="ddlRate" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="table_sar2_col2" style="width: 8%">
                                                    <%-- <asp:TextBox ID="txtQty" runat="server"></asp:TextBox> --%>
                                                    <asp:TextBox ID="txtQty" MaxLength="8" CssClass="Decimal4" runat="server" placeholder="Quantity"></asp:TextBox></td>
                                                </td> 

                                            <td class="table_sar2_col2">
                                                <asp:TextBox ID="txtItemAmt" MaxLength="9" runat="server"
                                                    placeholder="Item Amount" CssClass="Money">
                                                </asp:TextBox>
                                            </td>
                                                <td class="">
                                                    <asp:TextBox ID="txtRemark" MaxLength="100" runat="server" placeholder="Add Remark">
                                                    </asp:TextBox>
                                                </td>

                                                <td class="table_sar2_col3 c">
                                                    <asp:Button ID="btnItemAdd" runat="server" OnClick="btnItemAdd_Click" Text="Add" CssClass="btn btn-primary btn-xs "></asp:Button>
                                                </td>
                                            </tr>
                                    </table>
                                    </tbody>
                                   <%-- <table class=" table-bordered table1_sar mb20">
                                        <tr class="inf_head text-center">
                                            <td colspan="9">Tax Detail</td>
                                        </tr>
                                        <tr>
                                            <th class="table1_sar_col1 l">Item Name</th>
                                            <th class="table1_sar_col2 l">HSN /SAC Code</th>
                                            <th class="table1_sar_col3 r">Amount</th>
                                            <th class="table1_sar_col4 r">Tax Rate</th>
                                            <th class="table1_sar_col5 r">CGST</th>
                                            <th class="table1_sar_col6 r">SGST</th>
                                            <th class="table1_sar_col7 r">IGST</th>
                                            <th class="table1_sar_col8 r">CESS</th>
                                            <th class="table1_sar_col9">
                                                <asp:Button ID="btnAdd" runat="server" Text-="Add" CssClass="btn btn-primary btn-xs" /></th>
                                        </tr>
                                        <tr>
                                            <td class="table1_sar_col1 ">
                                                <cc1:combobox id="ddlItemName" runat="server"
                                                    width="20px" placeholder="p" cssclass="relative_gt"
                                                    autocompletemode="SuggestAppend" casesensitive="False"
                                                    style="text-transform: uppercase">
                                                     </cc1:combobox>
                                            </td>
                                            <td class="table1_sar_col2 ">
                                                <asp:TextBox ID="txtHsncode" runat="server"></asp:TextBox></td>
                                            <td class="table1_sar_col3 r">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="Money" MaxLength="9"></asp:TextBox></td>
                                            <td class="table1_sar_col4 r">
                                                <asp:TextBox ID="txtTaxRate" runat="server" CssClass="Money" MaxLength="9"></asp:TextBox></td>
                                            <td class="table1_sar_col5 r">
                                                <asp:TextBox ID="txtCGSTAmt" MaxLength="9" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                            <td class="table1_sar_col6  r">
                                                <asp:TextBox ID="txtSGSTAmt" MaxLength="9" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                            <td class="table1_sar_col7 r">
                                                <asp:TextBox ID="txtIGSTAmt" MaxLength="9" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                            <td class="table1_sar_col8 r">
                                                <asp:TextBox ID="txtCESSAmt" MaxLength="9" runat="server" placeholder="Amount" CssClass="Money"></asp:TextBox></td>
                                            <td class="table1_sar_col9 c">
                                                <asp:Button ID="btnDel" runat="server" Text="Del" CssClass="btn btn-danger btn-xs" Style="width: 38px;" /></td>
                                        </tr>

                                    </table>--%>

                                    <div id="divGrid" runat="server" style="max-height: 134px; overflow-y: scroll; margin-bottom: 20px">
                                        <asp:GridView ID="grditemTax" runat="server" AutoGenerateColumns="false" OnRowCommand="grditemTax_RowCommand" CssClass="table-gstr table-bordered">
                                            <HeaderStyle CssClass="inf_head" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Eval("CompanyID") %>' Visible="false" runat="server" />
                                                        <asp:Label Text='<%#Eval("BranchID") %>' Visible="false" runat="server" />
                                                        <asp:Label Text='<%#Eval("ItemUnit") %>' Visible="false" runat="server" />
                                                        <asp:Label Text='<%#Eval("ItemUnitID") %>' Visible="false" runat="server" />
                                                        <asp:Label Text='<%#Eval("GoodsServiceInd") %>' Visible="false" runat="server" />
                                                        <asp:Label Text='<%#Eval("ItemRate") %>' Visible="false" runat="server" />
                                                        <asp:Label ID="lblItemID" Text='<%#Eval("ItemID") %>' CssClass="hidden" runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemName" Text='<%#Eval("ItemName") %>' CssClass="txtFontSize" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HSN/SAC&nbsp;Code" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHsnSacCode" Text='<%#Eval("HsnSacCode") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Text='<%#Eval("ItemQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" Text='<%#Eval("ItemAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tax Rate" ItemStyle-CssClass=" ifpa" ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IGST" ItemStyle-CssClass=" ifpa" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGSTTax" Text='<%#Eval("IGSTTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblIGST" Text='<%#Eval("IGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CGST" ItemStyle-CssClass=" ifpa" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGSTTax" Text='<%#Eval("CGSTTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCGST" Text='<%#Eval("CGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SGST" ItemStyle-CssClass=" ifpa" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSGSTTax" Text='<%#Eval("SGSTTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblSGST" Text='<%#Eval("SGSTTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CESS" ItemStyle-CssClass=" ifpa" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCESSTax" Text='<%#Eval("CESSTax") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCESS" Text='<%#Eval("CESSTaxAmt") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remark" ItemStyle-CssClass=" ifpa" ItemStyle-Width="27%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExtraInd" Text='<%#Eval("ExtraInd") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblRemark" Text='<%#Eval("ItemRemark") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-Width="4%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDeleteID" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <style>
                                        .sar_table3 {
                                            width: 50%;
                                        }

                                        .sar_table3 {
                                            width: 50%;
                                            float: left;
                                        }
                                    </style>
                                    <table class="sar_table3 table-bordered ">
                                        <tbody>
                                            <tr class="inf_head">
                                                <td colspan="4"></td>
                                            </tr>
                                            <tr style="height: 35px;">
                                                <div id="trPayMode" runat="server">
                                                    <td colspan="2" style="width: 20%"><b>Received By</b>
                                                        <asp:DropDownList ID="ddlPayMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged">
                                                            <asp:ListItem Value="Cheque" Text="Cheque" />
                                                            <asp:ListItem Value="UTR" Text="RTGS/NEFT" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="col3">
                                                        <asp:Label ID="lblPayModeNo" runat="server">Cheque No.</asp:Label>
                                                        <asp:TextBox ID="txtReceivedNo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="col4">
                                                        <asp:Label ID="lblPayModeDate" runat="server">Cheque Date</asp:Label>
                                                        <asp:TextBox ID="txtReceivedDate" MaxLength="10" placeholder=" DD/MM/YYYY" runat="server" CssClass="datepicker"></asp:TextBox>
                                                    </td>
                                                </div>

                                            </tr>

                                            <tr>
                                                <td class="col1 l text-bold">Narration</td>
                                                <td colspan="3">
                                                    <cc1:ComboBox ID="txtNarration" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table class="table sar_table3 gstr_sales_amount table-bordered pull-right">
                                        <thead>
                                            <tr>
                                                <th colspan="100%" class="inf_head">Amount</th>
                                            </tr>
                                            <tr>
                                                <th class="amount_col1">Gross</th>
                                                <th class="amount_col2">Tax</th>
                                                <th class="amount_col3">Net Amount</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="amount_col1">
                                                    <asp:TextBox ID="txtGross" Enabled="false" runat="server" placeholder="Gross" CssClass="Money"></asp:TextBox>

                                                </td>
                                                <td class="amount_col2">
                                                    <asp:TextBox ID="txtTaxable" Enabled="false" runat="server" placeholder="Tax" CssClass="Money"></asp:TextBox>

                                                </td>

                                                <td class="amount_col3">
                                                    <asp:TextBox ID="txtNet" Enabled="false" runat="server" placeholder="Net" CssClass="Money"></asp:TextBox>
                                                </td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary btn-space-right" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" class="btn btn-danger btn-space-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>













