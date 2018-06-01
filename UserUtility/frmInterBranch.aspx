<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmInterBranch.aspx.cs" Inherits="Vouchers_frmInter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();

            $('#<%=txtVoucherDate.ClientID%>').click(function () {

                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfRangeInd" runat="server" />
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
                <h3 class="text-center head">Inter Branch
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">

                    <div class="col-xs-12">
                        <div class="panel panel-default">

                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <table class="bankpayment_table1 table-bordered">
                                        <tr>
                                            <th colspan="100%" class="inf_head"></th>
                                        </tr>
                                        <tr>
                                            <th style="width: 10%">
                                                <label>Voucher Date</label></th>
                                            <th style="width: 29%">&nbsp;</th>
                                            <th style="width: 28%">City Branch Name</th>

                                            <th style="width: 33%">
                                                <label>Bank Account</label></th>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />
                                            </td>
                                            <td style="width: 28%">
                                                <asp:TextBox ID="txtLastVoucherNo" CssClass="numberonly hidden" ReadOnly="true" placeholder=" Enter Last Voucher No" Style="width: 100%" runat="server" />
                                            </td>
                                            <td style="width: 29%">
                                                <asp:DropDownList ID="ddlBranchList" OnSelectedIndexChanged="ddlBranchList_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlBankAccount" placeholder="Select Bank" Style="width: 100%" runat="server"></asp:DropDownList>
                                                <%--CssClass="chzn-select"--%>
                                            </td>
                                        </tr>
                                    </table>


                                    <table id="tbl_id" class="bankpayment_table2  table-bordered">
                                        <thead>
                                            <tr class="inf_head">
                                                <th class="col1">Account Head</th>
                                                <th class="col2"></th>
                                                <th class="col3">GSTIN</th>
                                                <th class="col4">Invoice No.</th>
                                                <th class="col5">Invoice Date</th>
                                                <th class="col6">Amount</th>
                                                <th class="col7">Dr/Cr</th>
                                                <th class="col8"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="col1">


                                                    <cc1:ComboBox ID="txtAccountHead" runat="server" Width="250px" AutoPostBack="true" placeholder="p" CssClass="relative_gt" OnSelectedIndexChanged="txtAccountHead_SelectedIndexChanged" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>
                                                </td>
                                                <td class="col2">
                                                    <div id="divPartySelect" runat="server">
                                                        <asp:DropDownList ID="ddlSecondaryParty" runat="server" Visible="false"></asp:DropDownList>
                                                        <button id="btnRegToggle" runat="server" visible="false" data-toggle="collapse" data-target="#divdrop" class="form-control input-sm" type="button"></button>
                                                        <div id="divdrop" class="collapse pos-abs">
                                                            <div>
                                                                <asp:CheckBoxList ID="CbOutstandingBill" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="col3">
                                                    <asp:DropDownList ID="ddlGSTINNo" CssClass="" runat="server"></asp:DropDownList>
                                                </td>

                                                <td class="col4">
                                                    <asp:TextBox ID="txtInVoiceNo" CssClass="inpt numberonly " MaxLength="7" Style="width: 100%" runat="server" />
                                                </td>

                                                <td class="col5">
                                                    <asp:TextBox ID="txtInvoiceDate" CssClass="inpt datepicker " MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                </td>

                                                <td class="col6 r">
                                                    <asp:TextBox ID="txtAmount" CssClass="inpt Money" MaxLength="9" Style="width: 100%" runat="server" />
                                                </td>

                                                <td class="col7">
                                                    <asp:DropDownList ID="ddlCrOrDr" CssClass="inpt" Style="width: 100%; height: 27px;" runat="server">
                                                        <asp:ListItem Selected="True" Text="" />

                                                        <asp:ListItem Text="Cr" />
                                                        <asp:ListItem Text="Dr" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="col8">
                                                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" Text="Add" class="btn btn-primary btn-xs add_click add_btn" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                    <asp:GridView ID="gvInterBranch" CssClass="bankreceipt_table2_grid table-bordered" AutoGenerateColumns="false" runat="server" ShowHeader="false" OnRowCommand="gvInterBranch_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="col1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountHeadID" Text='<%#Eval("AccHeadValue") %>' CssClass="hidden" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAccountHead" Style="width: 100%; height: 27px;" Text='<%#Eval("AcctHeadText") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartyID" Text='<%#Eval("PartyID") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblBillNos" Text='<%#Eval("BillNos") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTIN" Text='<%#Eval("GSTIN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNo" Style="width: 100%" Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceDate" Style="width: 100%" Text='<%#Eval("InvoiceDate") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRCR" Text='<%#Eval("DrCr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col8">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>

                                    <table class="bankpayment_table3 table-bordered">
                                        <tbody>
                                            <tr>
                                                <td colspan="4" class="colspan_amount text-right">
                                                    <label>Net Amount</label>
                                                    <asp:TextBox ID="txtInvoiceTotalAmount" Enabled="false" Text="0" CssClass="inpt Money" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col1 inf_head">Paid By
                                                </td>
                                                <td class="col2">
                                                    <asp:DropDownList ID="ddlPayMode" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" runat="server">
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
                                            </tr>

                                            <tr>
                                                <td class="col1">Narration</td>
                                                <td colspan="3">
                                                    <%--            <cc1:ComboBox ID="txtNarration" runat="server" Width="250px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase"></cc1:ComboBox>--%>


                                                    <asp:TextBox ID="txtNarration" runat="server" Width="250px" CssClass="relative_gt" Style="text-transform: uppercase"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <span class="note_font"></span>
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" class="btn btn-primary btn-space-right" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
