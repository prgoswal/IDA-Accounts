<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdSupplierAdvancePayment.aspx.cs" Inherits="Modifiaction_frmUpdSupplierAdvancePayment" %>

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
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>
            <div class="content-wrapper">

                <h3 class="text-center head">SUPPLIER ADVANCE PAYMENT UPDATION 
                    <span class="invoiceHead">
                        <%-- <asp:Label ID="lblLastVno" runat="server"></asp:Label>--%>
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" />

                    </span>
                </h3>

                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                    <div style="margin-bottom: 10px;">
                                        <asp:TextBox ID="txtVoucherNo" CssClass="numberonly" MaxLength="7" placeholder="Search Voucher No." runat="server" />
                                        <asp:LinkButton runat="server" ID="btngo" OnClick="btngo_Click" CssClass="btn btn-sxs btn-primary"><i class="fa r fa-search"></i>Go</asp:LinkButton>
                                    </div>
                                    <table class=" table-bordered table_sar mb20" style="margin-bottom: 10px">

                                        <tr class="inf_head">
                                            <th class="inf_head" style="width: 20%">Voucher Date</th>
                                            <th class="inf_head" style="width: 30%">Cash/Bank Account</th>
                                            <th class="inf_head" style="width: 30%">Account Head</th>
                                            <th class="inf_head" style="width: 20%">GSTIN</th>
                                        </tr>
                                        <tr class="align_with_combobox">
                                            <td class="">
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker Required" runat="server" data-group="Save" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%"></asp:TextBox>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="ddlCashBankAccount" CssClass="Required" AutoPostBack="true" OnSelectedIndexChanged="ddlCashBankAccount_SelectedIndexChanged" placeholder="Select Cash/Bank Account" data-group="Save" Style="width: 100%" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="">
                                                <asp:DropDownList ID="txtAccountHead" CssClass="chzn-select pull-left relative_gt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged">
                                                </asp:DropDownList>

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
                                                <td class="" style="width: 35%">
                                                    <asp:DropDownList ID="ddlItem" CssClass="chzn-select pull-left" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" data-group="a" runat="server" Style="width: 50%!important;"></asp:DropDownList>


                                                </td>
                                                <td class="" style="width: 8%">
                                                    <asp:DropDownList ID="ddlRate" runat="server">
                                                    </asp:DropDownList>

                                                </td>

                                                <td class="" style="width: 8%">
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
                                                    <asp:Button ID="btnItemAdd" runat="server" Text="Add" OnClick="btnItemAdd_Click" CssClass="btn btn-primary btn-xs "></asp:Button>
                                                </td>
                                            </tr>
                                    </table>
                                    </tbody>
                               
                                    <div id="divGrid" runat="server" style="max-height: 134px; overflow-y: scroll; margin-bottom: 20px">
                                        <asp:GridView ID="grditemTax" runat="server" AutoGenerateColumns="false" OnRowCommand="grditemTax_RowCommand" CssClass="table-gstr table-bordered" Style="width: 100%">
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
                                                        <asp:Label ID="lblHsnSacCode" Text='<%#Eval("HsnSacNo") %>' runat="server"></asp:Label>
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

                                                <asp:TemplateField HeaderText="Remark" ItemStyle-CssClass=" ifpa" ItemStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExtraInd" Text='<%#Eval("ExtraInd") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblRemark" Text='<%#Eval("ItemRemark") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass=" text-center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" CssClass="btn btn-sxs btn-info" CommandName="EditItemRow" CommandArgument='<%#Container.DataItemIndex %>' Text="Edit" runat="server" />
                                                        <asp:LinkButton ID="btnDeleteID" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-sxs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                        <asp:HiddenField ID="hfHsnSacNo" runat="server" />


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
                                                <th class="col1 l">Narration</th>
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
                                                <th class="amount_col1">Gross Amount </th>
                                                <th class="amount_col2">Tax Amount</th>

                                                <th class="amount_col3">Net Amount </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="amount_col1">
                                                    <asp:TextBox ID="txtNet" Enabled="false" runat="server" placeholder="Net" CssClass="Money"></asp:TextBox></td>
                                                <td class="amount_col2">
                                                    <asp:TextBox ID="txtTaxable" Enabled="false" runat="server" placeholder="Tax" CssClass="Money"></asp:TextBox></td>
                                                <td class="amount_col3">
                                                    <asp:TextBox ID="txtGross" Enabled="false" runat="server" placeholder="Gross" CssClass="Money"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-left">
                                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-warning" Enabled="false" Visible="true" />
                                        </div>
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Enabled="false" CssClass="btn btn-primary btn-space-right" Visible="true" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-danger btn-space-right" />
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

