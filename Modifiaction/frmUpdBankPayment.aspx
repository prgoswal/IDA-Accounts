<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmUpdBankPayment.aspx.cs" Inherits="Modifiaction_frmUpdBankPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/VouchersReport.ascx" TagPrefix="uc1" TagName="VouchersReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            /*top: -40px;
            right: 0;*/
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
        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
            $('#<%=txtVoucherDate.ClientID%>').click(function () {
                $('#<%=txtVoucherDate.ClientID%>').datepicker('show');
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
            <div class="content-wrapper">
                <h3 class="text-center head">BANK PAYMENT UPDATION
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">

                    <div class="col-xs-12">

                        <div class="panel panel-default">

                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <table class="search_updCash_table table-bordered">
                                        <tr>
                                            <td>Transaction&nbsp;No&nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSearchVNo" MaxLength="8" runat="server" CssClass="numberonly"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="ReqtxtSearchVNo" runat="server" ControlToValidate="txtSearchVNo" ErrorMessage="Enter Transaction Number !" ValidationGroup="btnSave" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sxs" OnClick="btnGo_Click" runat="server" Text="Search" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12">
                                    <table class="cashpayment_table1 table-bordered">
                                        <tr>
                                            <th colspan="100%" class="inf_head"></th>
                                        </tr>
                                        <tr>
                                            <th style="width: 10%">
                                                <label>Transaction Date</label></th>
                                            <th style="width: 17%" class="hidden">
                                                <label class="hidden">Last Transaction No.</label></th>
                                            <th style="width: 17%">
                                                <label>Cash Account</label></th>
                                            <th style="width: 33%" id="thCCCode" runat="server" visible="false">&nbsp;<label>Cost Centre</label></th>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtVoucherDate" CssClass="datepicker" MaxLength="10" placeholder="DD/MM/YYYY" Style="width: 100%" runat="server" />

                                                <asp:RequiredFieldValidator ID="ReqtxtVoucherDate" runat="server" ControlToValidate="txtVoucherDate" ErrorMessage="Enter Transaction Date !" ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 17%" class="hidden">
                                                <asp:TextBox ID="txtLastVoucherNo" CssClass="numberonly hidden" Enabled="false" placeholder=" Enter Last Transaction No" Style="width: 100%" runat="server" />
                                            </td>


                                            <td style="width: 17%">
                                                <asp:DropDownList ID="ddlBankAccount" Style="width: 100%" runat="server"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="ReqddlBankAccount" runat="server" ControlToValidate="ddlBankAccount" InitialValue="0" ErrorMessage="Select Bank Account!" ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%" visible="false" id="tdCCCode" runat="server">
                                                <asp:DropDownList ID="ddlCostCenter" runat="server" />


                                                <asp:RequiredFieldValidator ID="ReqddlCostCentre" runat="server" ControlToValidate="ddlCostCenter" InitialValue="0" ErrorMessage="Select Cost Centre!" ValidationGroup="btnSave" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <table id="tbl_id" class="bankpayment_table2  table-bordered">
                                        <thead>
                                            <tr class="inf_head">
                                                <th class="col1">Account Head</th>
                                                <th class="col2">Capital/Revenue</th>
                                                <%--<th class="col2">Service No.</th>--%>
                                                <th style="display: none;" class="col3">GSTIN</th>
                                                <th class="col4">Bill No.</th>
                                                <th class="col5">Bill Date</th>
                                                <th class="col6">Amount</th>
                                                <th class="col7">Dr/Cr</th>
                                                <th class="col8"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="col1">
                                                    <asp:DropDownList ID="txtAccountHead" CssClass="chzn-select pull-left" runat="server"></asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="ReqtxtAccountHead" runat="server" ControlToValidate="txtAccountHead" InitialValue="0" ErrorMessage="Enter Account Head." ValidationGroup="btnAdd" Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                </td>


                                                <td class="col2">
                                                    <asp:DropDownList ID="ddlCapitalRevenue" runat="server">
                                                        <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Revenue" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Capital" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                                <%-- <td class="col2">--%>
                                                <%--<asp:TextBox ID="txtServiceNo" runat="server"></asp:TextBox>--%>


                                                <%-- </td>--%>
                                                <td class="col3" style="display: none;">
                                                    <asp:DropDownList ID="ddlGSTINNo" CssClass="" runat="server"></asp:DropDownList>
                                                </td>

                                                <td class="col4">
                                                    <asp:TextBox ID="txtInVoiceNo" CssClass="inpt AlphaNum " MaxLength="16" Style="width: 100%" runat="server" />
                                                </td>

                                                <td class="col5">
                                                    <asp:TextBox ID="txtInvoiceDate" CssClass="inpt datepicker " MaxLength="10" placeholder="DD/MM/YYYY" runat="server" />
                                                </td>

                                                <td class="col6">
                                                    <asp:TextBox ID="txtAmount" CssClass="inpt Money" MaxLength="13" Style="width: 100%" runat="server" />


                                                    <asp:RequiredFieldValidator ID="ReqtxtAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount." ValidationGroup="btnAdd" CssClass="gst-ac-error"></asp:RequiredFieldValidator>


                                                    <asp:RegularExpressionValidator ControlToValidate="txtAmount" ID="regExVal" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="GrpValid" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                </td>

                                                <td class="col7">
                                                    <asp:DropDownList ID="ddlCrOrDr" CssClass="inpt" Style="width: 100%; height: 27px;" runat="server">
                                                        <asp:ListItem Selected="True" Text="Dr" />
                                                        <asp:ListItem Text="Cr" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="col8">
                                                    <asp:Button ID="btnAdd" Text="Add" OnClick="btnAdd_Click" class="btn btn-primary btn-xs add_click add_btn" runat="server" ValidationGroup="btnAdd" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:GridView ID="gvBankPayment" CssClass="bankpayment_table2_grid table-bordered" OnRowCommand="gvBankPayment_RowCommand" AutoGenerateColumns="false" runat="server" ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="col1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountHeadID" Text='<%#Eval("AccCode") %>' CssClass="hidden" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAccountHead" Style="width: 100%; height: 27px;" Text='<%#Eval("Accname") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCapitalRevenue" Style="width: 100%;" Text='<%#Eval("IsCapitalRevenue") %>' Visible="false" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCapitalRevenueName" Style="width: 100%;" Text='<%#Eval("IsCapitalRevenuenName") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col3" Visible="false">
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
                                            <asp:TemplateField ItemStyle-CssClass="col6 r">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col7 c">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRCR" Text='<%#Eval("DrCr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="col8">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" Text="Edit" CommandName="EditRow" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-xs btn-info add_btn" runat="server"></asp:LinkButton>
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

                                    <table class="cashpayment_table3 table-bordered" onsubmit="return false;">
                                         <tbody>
                                            <tr>
                                                <td colspan="3">
                                                    <label>Service No.</label>
                                                    <asp:TextBox ID="txtServiceNo" Width="155px" Enabled="false" runat="server" />
                                                    <asp:Button Style="margin-top: -1px; font-family: FontAwesome; padding: 1PX 12PX; height: 25PX; width: CALC(100% - 160PX);" ID="btnServiceNo" runat="server" class="btn btn-primary btn-xs add_click add_btn" Text="&#xf002;" OnClick="btnServiceNo_Click" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Party Name</label>
                                                    <asp:TextBox ID="txtPartyName" Enabled="false" Width="320px" runat="server" />
                                                </td>
                                                <td colspan="2">
                                                    <label>Party Address</label>
                                                    <asp:TextBox ID="txtPartyAddress" Enabled="false" Width="270px" runat="server" />
                                                </td>

                                                <td colspan="2">
                                                    <label>Party GSTIN</label>
                                                    <asp:TextBox ID="txtpartyGstIN" Enabled="false" Width="180px" runat="server" />
                                                </td>

                                                <td colspan="2">
                                                    <label>Net Amount</label>
                                                    <asp:TextBox ID="txtInvoiceTotalAmount" Enabled="false" Width="120px" Text="0" CssClass="inpt Invoice_date" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <style>
                                        .marginZero {
                                            margin-bottom: 0px;
                                        }
                                    </style>
                                    <%--<td colspan="4" class="colspan_amount text-right">
                                        <label>Net Amount</label>
                                        <asp:TextBox ID="txtInvoiceTotalAmount" Enabled="false" Text="0" CssClass="inpt Money" runat="server" />
                                    </td>--%>
                                    <table class="bankpayment_table3 table-bordered">
                                        <tbody>
                                            <tr>
                                                <td colspan="2">
                                                    <label class="inf_head marginZero">IDA Ref. No.</label>
                                                    <asp:TextBox ID="txtIDARefNo" runat="server" MaxLength="9"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <label class="inf_head marginZero">Paid By</label>
                                                    <asp:DropDownList ID="ddlPayMode" AutoPostBack="true" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="Cheque" Text="Cheque" />
                                                        <asp:ListItem Value="UTR" Text="RTGS/NEFT" />
                                                    </asp:DropDownList>
                                                </td>

                                                <td colspan="2">
                                                    <asp:Label ID="lblPayModeNo" runat="server">Cheque No.</asp:Label>
                                                    <asp:TextBox ID="txtReceivedNo" runat="server"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblPayModeDate" runat="server">Cheque Date</asp:Label>
                                                    <asp:TextBox ID="txtReceivedDate" MaxLength="10" placeholder=" DD/MM/YYYY" runat="server" CssClass="datepicker"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Label1" runat="server">Cheque Drawn</asp:Label>
                                                    <asp:TextBox ID="txtChequeDrawn" MaxLength="75" CssClass="AlphaNum" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="11">
                                                    <label style="margin-right: 10px; margin-top: 5px;">Narration</label>
                                                    <style>
                                                        .text-uppercase input {
                                                            text-transform: uppercase;
                                                        }
                                                    </style>
                                                    <asp:TextBox ID="txtNarration" placeholder="Enter Narration" CssClass="text-uppercase" MaxLength="120" Width="50%" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                        <%--<tbody>
                                            <tr>
                                                <td class="col1 inf_head">IDA Ref. No.
                                                </td>
                                                <td class="col2">
                                                    <asp:TextBox ID="txtIDARefNo" runat="server" MaxLength="9"></asp:TextBox>
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
                                                <td class="col5">
                                                    <asp:Label ID="Label1" runat="server">Cheque Drawn</asp:Label>
                                                    <asp:TextBox ID="txtChequeDrawn" CssClass="AlphaNum" MaxLength="75" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="11">
                                                    <label style="margin-right: 10px; margin-top: 5px;">Narration</label>
                                                    <style>
                                                        .text-uppercase input {
                                                            text-transform: uppercase;
                                                        }
                                                    </style>
                                                    <asp:TextBox ID="txtNarration" placeholder="Enter Narration" CssClass="text-uppercase" MaxLength="120" Width="50%" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>--%>
                                    </table>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button Text="Reject" ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="btn btn-warning" />
                                            <asp:Button Text="Approved" ID="btnApproved" OnClick="btnApproved_Click" runat="server" CssClass="btn btn-success" />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary btn-space-right" Visible="true" ValidationGroup="btnSave" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Visible="false" Text="Clear" CssClass="btn btn-danger btn-space-right" />
                                            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-danger btn-space-right" />
                                        </div>
                                    </div>
                                </div>
                            </div>


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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:VouchersReport runat="server" ID="VouchersReport" />
</asp:Content>
