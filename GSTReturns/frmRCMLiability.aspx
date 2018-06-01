<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmRCMLiability.aspx.cs" Inherits="frmRCMLiability" Culture="hi-IN" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For Month -------------------//

            if ($('#<%=ddlMonth.ClientID%>').val() == '0') {
                $('#<%=ddlMonth.ClientID%>').focus();
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Month.');
                return false;
            }

            //------------------For Year -------------------//

            if ($('#<%=ddlYear.ClientID%>').val() == '0') {
                $('#<%=ddlYear.ClientID%>').focus();
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Year.');
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
            <%--<ul>
                <li id="li1" runat="server"></li>
            </ul>--%>
            <div class="content-wrapper">
                <h3 class="text-center head">RCM Liability Creation Purpose<span class="invoiceHead">
                    <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span> </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
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
                            <asp:Panel ID="pnlStepChange" CssClass="mr0 text-right" Visible="false" runat="server">
                                <asp:LinkButton CssClass="btn btn-default" ID="btnPrevious" OnClick="btnPrevious_Click" ToolTip="Previous" Enabled="false" runat="server"><i class="fa fa-backward" aria-hidden="true"></i></asp:LinkButton>
                                <%--<asp:LinkButton CssClass="btn btn-default" ID="btnForward" OnClick="btnForward_Click" Enabled="false" runat="server"><i class="fa fa-forward" aria-hidden="true"></i></asp:LinkButton>--%>
                            </asp:Panel>
                            <asp:MultiView ID="MultView1" ActiveViewIndex="0" runat="server">
                                <asp:View ID="View1" runat="server">

                                    <div class="panel-body">
                                        <style>
                                            .mnth {
                                                padding-left: 30px;
                                                padding-right: 34px;
                                            }

                                            .mr5 {
                                                margin-right: 5px;
                                                padding: 0px 5px;
                                            }

                                            .check_checkeryes input[type="radio"] {
                                                cursor: pointer;
                                                user-select: none;
                                            }

                                            .check_checkergreen input[type="radio"] + label {
                                                font-weight: normal;
                                                cursor: pointer;
                                                user-select: none;
                                                margin-right: 10px;
                                            }

                                            .check_checkergreen input[type="radio"]:checked + label {
                                                color: green;
                                                font-weight: bold;
                                            }

                                            .check_checkerred input[type="radio"]:checked + label {
                                                color: red;
                                                font-weight: bold;
                                            }

                                            .panel-footer {
                                                padding: 10px 0px;
                                            }
                                            @media (min-width:1280px){
                                            .span_elipsis {
                                                position: relative;
                                            }

                                                .span_elipsis span {
                                                    position: absolute;
                                                    top: 0;
                                                    left: 0;
                                                    display: block;
                                                    width: 183px;
                                                    white-space: nowrap;
                                                    overflow: hidden;
                                                    text-overflow: ellipsis;
                                                    margin-right: -20px;
                                                }
                                                }
                                        </style>
                                        <div class="row form-group">
                                            <div class="col-xs-12 mnth">
                                                <table class="pdf_show mrbotm40 font12" style="width: 1000px;">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 5%; text-align: right; padding-right: 3px"><b>Month</b></td>
                                                            <td style="width: 8%">
                                                                <asp:DropDownList ID="ddlMonth" runat="server">
                                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                                    <%--<asp:ListItem Value="1" Text="January" />
                                                                    <asp:ListItem Value="2" Text="February" />
                                                                    <asp:ListItem Value="3" Text="March" />
                                                                    <asp:ListItem Value="4" Text="April" />
                                                                    <asp:ListItem Value="5" Text="May" />
                                                                    <asp:ListItem Value="6" Text="June" />--%>
                                                                    <asp:ListItem Value="7" Text="July" />
                                                                    <asp:ListItem Value="8" Text="August" />
                                                                    <%--<asp:ListItem Value="9" Text="September" />
                                                                    <asp:ListItem Value="10" Text="October" />
                                                                    <asp:ListItem Value="11" Text="November" />
                                                                    <asp:ListItem Value="12" Text="December" />--%>
                                                                </asp:DropDownList></td>
                                                            <td style="width: 5%; text-align: right; padding-right: 3px"><b>Year</b></td>
                                                            <td style="width: 8%">
                                                                <asp:DropDownList ID="ddlYear" runat="server">
                                                                    <%--<asp:ListItem Value="0" Text="--Select--" />--%>
                                                                    <asp:ListItem Value="17" Text="2017" />
                                                                    <%--<asp:ListItem Value="18" Text="2018" />
                                                                    <asp:ListItem Value="19" Text="2019" />
                                                                    <asp:ListItem Value="20" Text="2020" />--%>
                                                                </asp:DropDownList></td>
                                                            <td style="width: 5%; text-align: right; padding-right: 3px"><b>GSTIN</b></td>
                                                            <td style="width: 14%">
                                                                <asp:DropDownList ID="ddlGSTINNO" CssClass="text-uppercase" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 38%">
                                                                <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sxs" Text="Go" OnClick="btnGo_Click" OnClientClick="return Validation()" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="divSummary" runat="server" visible="false" class="row">
                                            <div class="col-xs-12 form-group">
                                                <table class="table-bordered">
                                                    <tr class="inf_head">
                                                        <th style="width: 20%;">Total Purchases</th>
                                                        <th style="width: 20%;">Registered Purchases</th>
                                                        <th style="width: 20%;">Unregistered Purchases</th>
                                                        <th style="width: 20%;">Zero / Nil / Exempted / Non-GST Rated Purchases (Within Unregistered)</th>
                                                        <th style="width: 20%;">RCM Liability Amount</th>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblTotalPurchase" runat="server" /></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblRegisteredPurchase" runat="server" /></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblUnregisteredPurchase" runat="server" /></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblZeroNilExeNonGST" runat="server" /></td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblRCMLiabilityAmount" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:GridView ID="grdRCMLiability" AutoGenerateColumns="false" CssClass="credit_table table-bordered first_tr_hide mb0" OnRowCommand="grdRCMLiability_RowCommand" OnRowDataBound="grdRCMLiability_RowDataBound" OnRowEditing="grdRCMLiability_RowEditing" Style="width: 100%" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <tr>
                                                                    <th colspan="100%" class="text-center inf_head">Purchase Voucher Detail - Date Wise No. Wise</th>
                                                                </tr>
                                                                <tr class="hide_my_pdosi">
                                                                    <th style="width: 08%">Voucher No.</th>
                                                                    <th style="width: 08%">Voucher Date</th>
                                                                    <th style="width: 08%">Bill No.</th>
                                                                    <th style="width: 08%">Bill Date</th>
                                                                    <th style="width: 13%">Expenses Head</th>
                                                                    <th style="width: 14%">Item Name</th>
                                                                    <th style="width: 08%">Tax Rate</th>
                                                                    <th style="width: 08%">Item Amount</th>
                                                                    <th style="width: 06%">Specific</th>
                                                                    <th style="width: 06%">URD</th>
                                                                    <th style="width: 06%">Exempted</th>
                                                                    <th style="width: 04%; text-align: center"></th>
                                                                </tr>
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVoucherNo" Text='<%#Eval("VoucharNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="c">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseCode" Text='<%#Eval("PurchaseCode") %>' Visible="false" runat="server"></asp:Label>
                                                                <asp:Label ID="lblItemID" Text='<%#Eval("ItemID") %>' Visible="false" runat="server"></asp:Label>
                                                                <asp:Label ID="lblPurchaseBillNo" Text='<%#Eval("PurchaseBillNo") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="c">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseBillDate" Text='<%#Eval("PurchaseBillDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="span_elipsis">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpenseHead" Text='<%#Eval("ExpenseHead") %>' runat="server" title='<%#Eval("ExpenseHead") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="span_elipsis">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemName" Text='<%#Eval("ItemName") %>' runat="server" title='<%#Eval("ItemName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemAmount" Text='<%#Eval("ItemAmount","{0:0.00}") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSpecific" Text='<%#Eval("ItemAmount","{0:0.00}") %>' MaxLength="9" CssClass="Money" Enabled="false" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtURD" Text="0.00" CssClass="Money" AutoPostBack="true" OnTextChanged="txtURD_TextChanged" MaxLength="9" Enabled="false" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="r">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExempted" Text="0.00" AutoPostBack="true" OnTextChanged="txtExempted_TextChanged" CssClass="Money" Enabled="false" MaxLength="9" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEdit" Text="Edit" CommandName="EditRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-primary btn-sxs" runat="server" />
                                                                <asp:Button ID="btnSave" Text="Save" CommandName="SaveRow" Visible="false" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-primary btn-sxs" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 text-right">
                                                <table class="" style="width: 100%">
                                                    <tr>
                                                        <th colspan="6" style="width: 59%">&nbsp;</th>
                                                        <th style="width: 08%">
                                                            <asp:Label ID="lblTotalAmount" Text="Total Amount : " Visible="false" runat="server"></asp:Label></th>
                                                        <th style="width: 08%; text-align: right">
                                                            <asp:Label ID="lblTotalAmountValue" runat="server" /></th>
                                                        <th style="width: 06%; text-align: right">
                                                            <asp:Label ID="lblSpecificAmount" runat="server" /></th>
                                                        <th style="width: 06%; text-align: right">
                                                            <asp:Label ID="lblURDAmount" runat="server" /></th>
                                                        <th style="width: 06%; text-align: right">
                                                            <asp:Label ID="lblExemptedAmount" runat="server" /></th>
                                                        <th style="width: 04%"></th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-xs-2"></div>
                                                <div class="col-xs-10">
                                                    <div class="pull-right">
                                                        <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                                        <asp:Button ID="btnSave1" Text="Save" OnClick="btnSave1_Click" CssClass="btn btn-primary btn-space-right" Enabled="false" runat="server" />
                                                        <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>

                                <asp:View ID="View2" OnActivate="View2_Activate" runat="server">
                                    <div class="panel-body">
                                        <asp:GridView ID="gvRCMDateWise" AutoGenerateColumns="false" CssClass="credit_table table-bordered first_tr_hide mb0" OnRowDataBound="gvRCMDateWise_RowDataBound" Style="width: 100%;max-width:460px;margin:0 auto" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th colspan="100%" class="text-center inf_head">Purchase Voucher Detail - Date Wise</th>
                                                        </tr>
                                                        <tr class="hide_my_pdosi">
                                                            <th style="width: 08%">Voucher Date</th>
                                                            <th style="width: 08%">Tax Rate</th>
                                                            <th style="width: 06%">Specific</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="c" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate","{0:dd/MM/yyyy}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="r" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="r" ItemStyle-Width="40%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecific" Text='<%#Eval("Specific","{0:0.00}") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="row">
                                            <div class="col-xs-12 text-right">
                                                <table class=""  style="width: 100%;max-width:460px;margin:0 auto">
                                                    <tr>
                                                        <th colspan="1" style="width: 59%">&nbsp;</th>
                                                        <th style="width: 08%">
                                                            <label >Total&nbsp;Amount&nbsp;:</label>
                                                        <th style="width: 08%; text-align: right">
                                                            <asp:Label ID="lblDateWiseTotalAmount" runat="server" /></th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-xs-7">
                                                    <div class="pull-left">
                                                        <label style="background-color:red; color:white">
                                                            Entries Are Not Included in RCM Liability Creation Due To UnRegistered Purchase <= Rs. 5000 Per Day.
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-5">
                                                    <div class="pull-right">
                                                        <asp:Button ID="btnSave2" Text="Submit" OnClick="btnSave2_Click" CssClass="btn btn-primary btn-space-right" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>

                                <asp:View ID="View3" OnActivate="View3_Activate" runat="server">
                                    <div class="panel-body">
                                        <asp:GridView ID="gvRCMRateWise" AutoGenerateColumns="false" CssClass="credit_table table-bordered first_tr_hide mb0" Style="width: 100%;max-width:460px;margin:0 auto" runat="server">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <th colspan="100%" class="text-center inf_head">Purchase Voucher Detail - Tax Rate Wise</th>
                                                        </tr>
                                                        <tr class="hide_my_pdosi">
                                                            <th style="width: 25%">Tax Rate</th>
                                                            <th style="width: 25%">Specific</th>
                                                            <th style="width: 25%">CGST</th>
                                                            <th style="width: 25%">SGST</th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaxRate" Text='<%#Eval("TaxRate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecific" Text='<%#Eval("SpecificAmt","{0:0.00}") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGST" Text='<%#Eval("CGSTAmt","{0:0.00}") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="r">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSGST" Text='<%#Eval("SGSTAmt","{0:0.00}") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="row">
                                            <div class="col-xs-12 text-right">
                                                <table class="credit_table table-bordered mb0"  style="width: 100%;max-width:460px;margin:0 auto">
                                                    <tr>
                                                        <th style="width: 25%">
                                                            <label>Total Amount:</label>
                                                        <th style="width: 25%; text-align: right">
                                                            <asp:Label ID="lblRateWiseSpecificTotalAmount" runat="server" /></th>
                                                        <th style="width: 25%; text-align: right">
                                                            <asp:Label ID="lblCGSTTotalAmount" runat="server" /></th>
                                                        <th style="width: 25%; text-align: right">
                                                            <asp:Label ID="lblSGSTTotalAmount" runat="server" /></th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-xs-6"></div>
                                                <div class="col-xs-6">
                                                    <div class="pull-right">
                                                        <asp:Button ID="btnSave3" Text="Create Liability" OnClick="btnSave3_Click" CssClass="btn btn-primary btn-space-right" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>