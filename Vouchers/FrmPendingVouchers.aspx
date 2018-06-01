<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="FrmPendingVouchers.aspx.cs" Inherits="Vouchers_FrmPendingVouchers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $('.tooltipmaker').each(function () {
                $(this).append('<i></i>')
            });
        })
    </script>
    <style>
        /*error message popup*/
        td {
            position: relative;
        }

        .gst-ac-error {
            background: #f05050;
            color: #fff;
            padding: 4px;
            border-radius: 4px;
            border: 1px solid #fff;
            position: absolute;
            Z-INDEX: 2;
            left: 0;
            box-shadow: 0px 0px 5px 2px rgba(0,0,0,0.5);
            top: calc(100% + 4px);
        }

            .gst-ac-error::after {
                content: '';
                display: block;
                position: absolute;
                bottom: 100%;
                left: 50%;
                width: 0;
                Z-INDEX: 2;
                transform: translateX(-50%);
                height: 0;
                border-top: 10px solid transparent;
                border-right: 10px solid transparent;
                border-bottom: 10px solid #f05050;
                border-left: 10px solid transparent;
            }
    </style>

    <script>
        $(document).ready(function () {
            $('.cashpayment_activeme').addClass('active');
        });
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
            $(document).ready(function () {
                $('.onlyonecheck input').on('change', function () {
                    debugger;
                    $('.onlyonecheck input').not(this).prop('checked', false);
                });

                $('.tooltipmaker').each(function () {
                    $(this).append('<i></i>')
                });

            });
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $(document).keydown(function (e) {
                return (e.which || e.keyCode) != 116;
            });
        });
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
            <div class="content-wrapper form-control-mini">
                <h3 class="text-center head">PENDING TRANSACTION
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <div id="divScroll" runat="server">
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

                                            .marginLeft {
                                                margin-left: 5px;
                                            }

                                            .checkboxtohyper input {
                                                display: none;
                                            }

                                            .checkboxtohyper label {
                                                display: block;
                                                cursor: pointer;
                                                color: #0075BB;
                                                font-weight: normal;
                                                margin: 0 auto;
                                                padding: 0;
                                                text-align: center;
                                                position: relative;
                                            }

                                                .checkboxtohyper label:hover, .checkboxtohyper input:checked + label {
                                                    text-decoration: underline;
                                                }

                                            .tooltipmaker {
                                                position: relative;
                                                display: block;
                                                user-select: none;
                                            }

                                                .tooltipmaker label:after {
                                                    visibility: hidden;
                                                    width: 157px;
                                                    display: block;
                                                    position: absolute;
                                                    content: 'Want To Perform Any Action Click On It.';
                                                    background: #000;
                                                    color: #fff;
                                                    border-radius: 4px;
                                                    padding: 1px 12px;
                                                    left: 47px;
                                                    top: 50%;
                                                    transform: translateY(-50%);
                                                    pointer-events: none;
                                                    z-index: 99999;
                                                }

                                                .tooltipmaker i {
                                                    pointer-events: none;
                                                    visibility: hidden;
                                                    top: 50%;
                                                    transform: translateY(-50%);
                                                    display: block;
                                                    height: 0;
                                                    width: 0;
                                                    position: absolute;
                                                    left: 27px;
                                                    border-top: 10px solid transparent;
                                                    border-right: 10px solid #000;
                                                    border-bottom: 10px solid transparent;
                                                    border-left: 10px solid transparent;
                                                }

                                                .tooltipmaker:hover label:after, .tooltipmaker:hover i {
                                                    visibility: visible;
                                                }
                                        </style>
                                        <style>
                                            .divOveFlow {
                                                max-height: 400px;
                                                overflow-y: scroll;
                                            }

                                            .brk-all {
                                                word-break: break-all;
                                            }

                                                .brk-all tr td, .brk-all tr th {
                                                    padding: 1px;
                                                    font-size: 13px;
                                                }
                                        </style>
                                        <div id="divPendingSince" runat="server" visible="false" style="float: right; margin-right: 17px">
                                            <label style="float: left">Total Pending Transaction : &nbsp;</label>
                                            <asp:Label ID="lblTotalPendingSince" runat="server" Style="background-color: red; border-radius: 100%; color: white; text-align: center; padding: 3px; font-size: 12px;"></asp:Label><label style="float: right"></label>
                                        </div>
                                        <style>
                                            .marginRigth {
                                                margin-right: 10px;
                                            }
                                        </style>
                                        <div style="padding-right: 17px">
                                            <table id="tblPendingListHeader" runat="server" visible="false" class="table table-voucher table-bordered table-form gstr_sales_item onlyonecheck brk-all" style="margin-bottom: 0 !important">
                                                <tr class="inf_head">
                                                    <th id="thTransaction" runat="server">Transaction</th>
                                                    <th id="thReferenceNo" runat="server">Reference&nbsp;No.</th>
                                                    <th id="thPartyName" runat="server">Party&nbsp;Name</th>
                                                    <th id="thDepartmentName" runat="server">Department&nbsp;</th>
                                                    <th id="thNarration" runat="server">Narration</th>
                                                    <th id="thAction" runat="server">Action</th>
                                                </tr>
                                                <tr class="inf_head">
                                                    <th id="thTransNo" runat="server">No.</th>
                                                    <th id="thTransDate" runat="server">Date</th>
                                                    <th id="thTransType" runat="server">Type</th>
                                                    <th id="thTransAmount" runat="server">Amount (Rs.)</th>
                                                    <th id="thView" runat="server"></th>
                                                    <th id="thPrintTrans" runat="server"></th>
                                                    <th id="thSendToAudit" runat="server"></th>
                                                    <th id="thAudit" runat="server"></th>
                                                    <th id="thUpdate" runat="server"></th>
                                                    <th id="thBankPay" runat="server"></th>
                                                    <th id="thApprove" runat="server"></th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="max-height: 275px; overflow-y: scroll">
                                            <asp:GridView ID="grdPendingVouchers" CssClass="table table-voucher table-bordered table-form gstr_sales_item onlyonecheck brk-all" AutoGenerateColumns="false"
                                                OnRowCommand="grdPendingVouchers_RowCommand" OnRowDataBound="grdPendingVouchers_RowDataBound" runat="server" EmptyDataText="Record Not Found." ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="c checkboxtohyper">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPerformOperation" AutoPostBack="true" OnCheckedChanged="chkPerformOperation_CheckedChanged" runat="server" Text='<%#Eval("TransactionNo") %>' CssClass="tooltipmaker"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" Text='<%#Eval("TransactionDate") %>' runat="server"></asp:Label>
                                                            <%--<asp:Label ID="lblTransYear" Text='<%#Eval("TransYear") %>' Visible="false" runat="server"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocType" Text='<%#Eval("DocTypeID") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocTypeDesc" Text='<%#Eval("DocumentType") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="r Money">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionAmount" Text='<%#Eval("NetAmount","{0:###,###.00}") %>' CssClass="Money marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferenceNo" Text='<%#Eval("Remark3") %>' CssClass="Money marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeptCD" Text='<%#Eval("DeptCD") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsFinal" Text='<%#Eval("IsFinal") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsAudit" Text='<%#Eval("IsAudit") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsSendToAudit" Text='<%#Eval("IsSendToAudit") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblAccCode" Text='<%#Eval("AccCode") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblCCCode" Text='<%#Eval("CCCode") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblBankPayInd" Text='<%#Eval("BankPayInd") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPurchaseBankPayInd" Text='<%#Eval("PurchaseBankPayInd") %>' Visible="false" runat="server"></asp:Label>

                                                            <asp:Label ID="lblChequeNo" Text='<%#Eval("ChequeNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblChequeDate" Text='<%#Eval("ChequeDate") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblUTRNo" Text='<%#Eval("UTRNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblUTRDate" Text='<%#Eval("UTRDate") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPartyName" Text='<%#Eval("PartyName") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblLastApprovedBy" Text='<%#Eval("LastApprovedBy") %>' Visible="false" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblDepartmentName" Text='<%#Eval("DepartmentName") %>' Visible="false" CssClass="marginLeft" runat="server"></asp:Label>
                                                            <asp:Label ID="lblSubDepartmentName" Text='<%#Eval("SubDepartmentName") %>' Visible="false" CssClass="marginLeft" runat="server"></asp:Label>--%>
                                                            <asp:Label ID="lblDeptAndSubDeptName" Text='<%#Eval("DeptName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNarration" Text='<%#Eval("Narration") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" Text="View" CommandName="ViewRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-primary add_btn" runat="server"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnTransactionPrint" Text="Print" CommandName="PrintTransRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-info add_btn" runat="server"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnSendToAudit" Text="Send To Audit" CommandName="SendToAuditRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-success add_btn" runat="server"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnAudit" Text="Audit" CommandName="AuditRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-warning add_btn" runat="server"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnUpdate" Text="Update" CommandName="UpdateRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-primary add_btn" runat="server"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnBankPay" Text="Bank Pay" CommandName="BankPayRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-info add_btn" runat="server"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnApprove" Text="Approve" CommandName="ApproveRow" CommandArgument='<%#Container.DataItemIndex %>' Enabled="false" Visible="false" CssClass="btn btn-xs btn-success add_btn" runat="server"></asp:Button>
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
                                        </div>
                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-sm-12">
                                                <div class="pull-right">
                                                    <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divCT" runat="server" visible="false" style="float: left">
                                            <label>Approved Transaction(s) List</label>
                                        </div>
                                        <div id="divCompleteVoucher" runat="server" visible="false" style="float: right; margin-right: 17px">
                                            <label style="float: left">Total Approved Transaction(s) : &nbsp;</label>
                                            <asp:Label ID="lblTotalCompleteTransaction" runat="server" Style="background-color: red; border-radius: 100%; color: white; text-align: center; padding: 3px; font-size: 12px;"></asp:Label><label style="float: right"></label>
                                        </div>
                                        <div style="padding-right: 17px">
                                            <table id="tblCompleteTransaction" runat="server" visible="false" class="table table-voucher table-bordered table-form gstr_sales_item onlyonecheck brk-all" style="margin-bottom: 0 !important">
                                                <tr class="inf_head">
                                                    <th colspan="4">Transaction</th>
                                                    <th rowspan="2" style="width: 10%">Reference&nbsp;No.</th>
                                                    <th rowspan="2" style="width: 33%">Party&nbsp;Name</th>
                                                    <th colspan="2">Voucher</th>
                                                    <th colspan="2">For Printing</th>
                                                </tr>
                                                <tr class="inf_head">
                                                    <th style="width: 4%">No.</th>
                                                    <th style="width: 7%">Date</th>
                                                    <th style="width: 7%">Type</th>
                                                    <th style="width: 8%">Amount (Rs.)</th>
                                                    <th style="width: 8%">No.</th>
                                                    <th style="width: 7%">Date</th>
                                                    <th style="width: 8%">&nbsp;</th>
                                                    <th style="width: 8%">&nbsp;</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="max-height: 275px; overflow-y: scroll">
                                            <asp:GridView ID="grdCompleteTransactionList" CssClass="table table-voucher table-bordered table-form gstr_sales_item onlyonecheck  brk-all" AutoGenerateColumns="false"
                                                OnRowCommand="grdCompleteTransactionList_RowCommand" OnRowDataBound="grdCompleteTransactionList_RowDataBound" EmptyDataText="Record Not Found." ShowHeader="false" runat="server">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="c w4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransaction" Text='<%#Eval("TransactionNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c w7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" Text='<%#Eval("TransactionDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c w7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocType" Text='<%#Eval("DocTypeID") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocTypeDesc" Text='<%#Eval("DocumentType") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="r Money w8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionAmount" Text='<%#Eval("NetAmount","{0:###,###.00}") %>' CssClass="Money marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="w10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferenceNo" Text='<%#Eval("Remark3") %>' CssClass="Money marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="w33">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBankPayVoucherInd" Text='<%#Eval("BankPayVoucherInd") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPartyName" Text='<%#Eval("PartyName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="r Money w8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherNo" Text='<%#Eval("VoucharNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="r Money w7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherDate" Text='<%#Eval("VoucharDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c w16">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPurchasePrint" Text="Purchase" CommandName="PrintPurchaseRow" CommandArgument='<%#Container.DataItemIndex %>' Visible="false" CssClass="btn btn-xs btn-primary add_btn" runat="server"></asp:Button>
                                                            <asp:Button ID="btnBankPayPrint" Text="Bank Pay" CommandName="PrintBankPayRow" CommandArgument='<%#Container.DataItemIndex %>' Visible="false" CssClass="btn btn-xs btn-info add_btn" runat="server"></asp:Button>
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
                                        </div>
                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-sm-12">
                                                <div class="pull-right">
                                                    <asp:Label ID="lblCompleteListMSG" CssClass="text-danger" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divSTA" runat="server" visible="false" style="float: left">
                                            <label>Send To Audit List</label>
                                        </div>
                                        <div id="divTSTAT" runat="server" visible="false" style="float: right; margin-right: 17px">
                                            <label style="float: left">Total Send To Audit Transaction(s) : &nbsp;</label>
                                            <asp:Label ID="lblTotalSTAT" runat="server" Style="background-color: red; border-radius: 100%; color: white; text-align: center; padding: 3px; font-size: 12px;"></asp:Label><label style="float: right"></label>
                                        </div>
                                        <div style="padding-right: 17px">
                                            <table id="tblSTA" runat="server" visible="false" class="table table-voucher table-bordered table-form gstr_sales_item onlyonecheck brk-all" style="margin-bottom: 0 !important">
                                                <tr class="inf_head">
                                                    <th colspan="4">Transaction</th>
                                                    <th rowspan="2" style="width: 10%">Reference&nbsp;No.</th>
                                                    <th rowspan="2" style="width: 15%">Party&nbsp;Name</th>
                                                    <th rowspan="2" style="width: 15%">Department&nbsp;</th>
                                                    <th rowspan="2" style="width: 26%">Narration</th>
                                                    <th rowspan="2" style="width: 8%">For&nbsp;Printing</th>
                                                </tr>
                                                <tr class="inf_head">
                                                    <th style="width: 4%">No.</th>
                                                    <th style="width: 7%">Date</th>
                                                    <th style="width: 7%">Type</th>
                                                    <th style="width: 8%">Amount (Rs.)</th>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="max-height: 275px; overflow-y: scroll">
                                            <asp:GridView ID="grdSTA" CssClass="table table-voucher table-bordered table-form gstr_sales_item onlyonecheck  brk-all" AutoGenerateColumns="false"
                                                EmptyDataText="Record Not Found." ShowHeader="false" OnRowCommand="grdSTA_RowCommand" runat="server">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="c w4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionNo" Text='<%#Eval("TransactionNo") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c w7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" Text='<%#Eval("TransactionDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c w7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocType" Text='<%#Eval("DocTypeID") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocTypeDesc" Text='<%#Eval("DocumentType") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="r Money w8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionAmount" Text='<%#Eval("NetAmount","{0:###,###.00}") %>' CssClass="Money marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="w10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferenceNo" Text='<%#Eval("Remark3") %>' CssClass="Money marginRigth" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="w15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeptCD" Text='<%#Eval("DeptCD") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsFinal" Text='<%#Eval("IsFinal") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsAudit" Text='<%#Eval("IsAudit") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblIsSendToAudit" Text='<%#Eval("IsSendToAudit") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblAccCode" Text='<%#Eval("AccCode") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblCCCode" Text='<%#Eval("CCCode") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblBankPayInd" Text='<%#Eval("BankPayInd") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPurchaseBankPayInd" Text='<%#Eval("PurchaseBankPayInd") %>' Visible="false" runat="server"></asp:Label>

                                                            <asp:Label ID="lblChequeNo" Text='<%#Eval("ChequeNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblChequeDate" Text='<%#Eval("ChequeDate") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblUTRNo" Text='<%#Eval("UTRNo") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblUTRDate" Text='<%#Eval("UTRDate") %>' Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblPartyName" Text='<%#Eval("PartyName") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblLastApprovedBy" Text='<%#Eval("LastApprovedBy") %>' Visible="false" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="w15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeptAndSubDeptName" Text='<%#Eval("DeptName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="w26">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNarration" Text='<%#Eval("Narration") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="c w8">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" Text="Print" CommandName="PrintRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-info add_btn" runat="server"></asp:Button>
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
                                        </div>
                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-sm-12">
                                                <div class="pull-right">
                                                    <asp:Label ID="lblSTAMSG" CssClass="text-danger" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $('.onlyonecheck input').on('change', function () {
                            debugger;
                            $('.onlyonecheck input').not(this).prop('checked', false);
                        });
                    })
                </script>
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
                <asp:Label ID="lblTransactionNo" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblTransactionDate" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblDoumentTypeID" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblDocumentType" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblNarration" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblTransactionAmount" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblPartyName" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblAccCode" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblCCCode" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblPurchaseBankPayInd" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblBankPayTransactionNo" Visible="false" runat="server"></asp:Label>
                <asp:Panel runat="server" ID="pnlAuditConfirmation" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
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
                                                            Do You Want To Audit Transaction No. &nbsp;<asp:Label ID="lblAuditTransactionNo" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="pull-right">
                                                        <asp:Button ID="btnAuditYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnAuditYes_Click" runat="server" />
                                                        <asp:Button ID="btnAuditNo" CssClass="btn btn-danger" Text="No" OnClick="btnAuditNo_Click" runat="server" />
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
                <asp:Panel runat="server" ID="pnlApproval" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
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
                                                            Do You Want To Approve Transaction No. &nbsp;<asp:Label ID="lblApprovalTransactionNo" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="pull-right">
                                                        <asp:Label ID="lblApprovalMSG" CssClass="text-danger lblMsg" runat="server" />
                                                        <asp:Button ID="btnApprovalYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnApprovalYes_Click" runat="server" />
                                                        <asp:Button ID="btnApprovalNo" CssClass="btn btn-danger" Text="No" OnClick="btnApprovalNo_Click" runat="server" />
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
                <asp:Panel runat="server" ID="pnlSendToAudit" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
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
                                                            Are You Sure To Send Transaction No.  &nbsp;<asp:Label ID="lblSendToAuditTransactionNo" runat="server" />&nbsp; For Audit?
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="pull-right">
                                                        <asp:Label ID="lblSendToAuditlMSG" CssClass="text-danger lblMsg" runat="server" />
                                                        <asp:Button ID="btnSendToAuditYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnSendToAuditYes_Click" runat="server" />
                                                        <asp:Button ID="btnSendToAuditNo" CssClass="btn btn-danger" Text="No" OnClick="btnSendToAuditNo_Click" runat="server" />
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
                <asp:Panel runat="server" ID="pnlBankPay" CssClass="reportPopUp" Visible="false" Style="position: absolute; left: 0; right: 0">
                    <div class="panel panel-primary bodyPop" style="width: 60%; padding: 0; position: center; background-color: #0075BB">
                        <div class="content-wrapper form-control-mini">
                            <div class="panel-heading" style="text-align: center; margin-top: -18px; font-size: 18px; color: white;">
                                <asp:Label ID="lblBankPaymentHeader" Text="Bank Payment" runat="server"></asp:Label>
                            </div>
                            <div class="container_fluid">
                                <div class="row">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="form-horizontal">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4">Transaction&nbsp;Date&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtTransactionDate" Style="margin-left: 42px;" placeholder="DD/MM/YYYY" CssClass="datepicker form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4" style="margin-left: 40px; margin-right: -40px;">Bank&nbsp;Account&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlBankAccount" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4">Party&nbsp;Name&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPartyName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4">Net&nbsp;Amount&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtNetAmount" Enabled="false" CssClass="Money form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4">Paid&nbsp;By&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-7">
                                                                <asp:DropDownList ID="ddlPayMode" CssClass="form-control" Style="margin-left: 40px" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" runat="server">
                                                                    <asp:ListItem Value="Cheque" Text="Cheque" />
                                                                    <asp:ListItem Value="UTR" Text="RTGS/NEFT" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4">
                                                                <asp:Label ID="lblPayModeNo" Style="margin-left: 10px" runat="server">Cheque&nbsp;No.</asp:Label>&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox ID="txtReceivedNo" CssClass="numberonly form-control" Style="margin-left: 20px" MaxLength="8" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group row">
                                                            <label class="col-sm-4">
                                                                <asp:Label ID="lblPayModeDate" Style="margin-left: -15px" runat="server">Cheque&nbsp;Date</asp:Label>&nbsp;<i class="text-danger">*</i></label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtReceivedDate" MaxLength="10" placeholder="DD/MM/YYYY" CssClass="datepicker form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-group row">
                                                            <label class="col-sm-2">
                                                                <asp:Label ID="Label1" runat="server">Narration&nbsp;<i class="text-danger">*</i></asp:Label></label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtNarration" placeholder="Narration" CssClass="form-control text-uppercase" MaxLength="80" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="pull-right">
                                                        <asp:Label ID="lblBankPayMSG" CssClass="text-danger lblMsg" runat="server" />
                                                        <asp:Button ID="btnBankPaySave" CssClass="btn btn-primary" Text="Save" OnClick="btnBankPaySave_Click" runat="server" />
                                                        <asp:Button ID="btnBankPayClear" CssClass="btn btn-danger" Text="Clear" OnClick="btnBankPayClear_Click" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>
