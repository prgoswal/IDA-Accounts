<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBrsOpeningBalDiff.aspx.cs" Inherits="UserUtility_frmBrsOpeningBalDiff" %>

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
                <h3 class="text-center head">Opening Reconciliation Difference Entry 
            <span class="invoiceHead">
                <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default col-sm-8 col-sm-offset-2">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 ">Select Bank Name</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlBankAccount" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 ">Select Criteria</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCriteria_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="-----Select Criteria-----" Value="0" Selected="True" />
                                                        <asp:ListItem Text="Chq.Issued But Not Presented For Payment." Value="1" />
                                                        <asp:ListItem Text="Chq.Deposited in Bank But Not Collected." Value="2" />
                                                        <asp:ListItem Text="Chq.Interest Credited In Bank Statement But Not In Bank Book." Value="3" />
                                                        <asp:ListItem Text="Chq.Deposited Credited In Bank Statement But Not In Bank Book." Value="4" />
                                                        <asp:ListItem Text="Bank Charges Debited In Bank Statement But Not In Bank Book." Value="5" />
                                                        <asp:ListItem Text="Interest Debited In Bank Statement But Not In Bank Book." Value="6" />
                                                        <asp:ListItem Text="Chq.Return Debited In Bank Statement But Not In Bank Book." Value="7" />
                                                        <asp:ListItem Text="Interest Credited In Bank Statement But Not In Bank Book. " Value="8" />
                                                        <asp:ListItem Text="Bank Charges/Interest Debited In Bank Statement. " Value="9" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 ">Cheque/UTR No.</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtChqNo" runat="server" Text="0" placeholder="Cheque/UTR No" MaxLength="16" CssClass="form-control "></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 alphaonly">Cheque/UTR Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtChqDate" runat="server" CssClass="form-control  datepicker" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 ">Dr Amount</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDrAmount" runat="server" Text="0" placeholder="Dr Amount" CssClass="form-control Money" MaxLength="9"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 alphaonly">Cr Amount</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCrAmount" runat="server" Text="0" CssClass="form-control  Money" MaxLength="9" placeholder="Cr Amount"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group row" style="display: none">
                                                <label class="col-sm-2 ">Narration</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtnarr" runat="server" CssClass="form-control" MaxLength="150" placeholder="Narration"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="DivVoucher" runat="server" style="display: none">
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 ">Voucher No.</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtVoucherno" runat="server" CssClass="form-control numberonly" MaxLength="6" placeholder="Voucher No"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="col-sm-4 alphaonly">Voucher Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtVoucherDate" runat="server" CssClass="form-control  datepicker" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
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
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" class="btn btn-primary btn-space-right" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="btn btn-danger btn-space-right" Text="Clear" />
                                        </div>
                                    </div>
                                </div>
                            </div>





                            <style>
                                .reportPopUp {
                                    top: 0px;
                                    bottom: 0;
                                    padding-top: 20px;
                                    width: 100%;
                                    height: 100%;
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


                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlPassword" CssClass="modalPop" Visible="false" DefaultButton="btnSubmit">
                <div class="panel panel-primary bodyContent p0" style="max-width: 30%">
                    <div class="panel-heading" style="text-align: center">
                        <b>Verification Password</b>
                    </div>
                    <br />
                    <div class="panel-body">
                        <div class="col-sm-12">
                            <div class="col-sm-5"><b>Enter Password</b></div>
                            <div class="col-sm-7">
                                <asp:TextBox ID="txtPassword" MaxLength="10" TextMode="Password" ValidationGroup="Pass" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="col-sm-12 text-right">
                            <asp:Label ID="lblPassMsg" CssClass="text-danger lblMsg" runat="server" />
                            <asp:Button ID="btnSubmit" ValidationGroup="Pass" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return Validation()" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

