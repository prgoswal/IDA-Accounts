<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmRegistration.aspx.cs" Inherits="UserUtility_frmRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style>
        legend, .Head {
            margin-bottom: 10px;
            font-size: 15px;
            background: #1c75bf;
            color: white;
            padding: 0px 8px;
            margin-left: 16px;
            border-radius: 5px;
            margin-top: 5px;
        }

        .form-control {
            height: 26px;
            padding: 2px 10px;
        }

        .form-group {
            margin-bottom: 8px;
        }

        .table-choose-invoice-format tbody tr td {
            padding: 4px;
        }

        .table-terms tbody tr td {
            padding: 4px;
        }
    </style>
    <script type="text/javascript">

        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();


            SeriesInit('#<%=ddlSeriesType.ClientID%>');

            function SeriesInit(seriesType) {
                debugger;
                $('#pnlTextSeries').addClass('hidden');
                $('#pnlddlCashCredit').addClass('hidden');

                var Desc = "";
                $('#divDefault').removeClass('hidden');
                if ($(seriesType).val() == "0") {
                    $('#divDefault').addClass('hidden');
                }
                else if ($(seriesType).val() == "1") {
                    $('#pnlddlCashCredit').removeClass('hidden');
                    Desc = "<ul>";
                    Desc += "<li>In this, the user will enter invoice series manually on Sales Invoice</li>";
                    Desc += "<li>In Profile setting he has choice to select Cash/Credit or both and give the starting  no from which series is to be start.</li>";
                    Desc += "</ul>";
                    $('#SeriesDesc').html(Desc);
                }
                else if ($(seriesType).val() == "2") {
                    $('#pnlTextSeries').removeClass('hidden');
                    $('#pnlddlCashCredit').removeClass('hidden');
                    Desc = "<ul>";
                    Desc = "<li>Here Client has to choice to select multiple series & select starting invoice no individually.</li>";
                    Desc += "</ul>";
                    $('#SeriesDesc').html(Desc);
                }
                else if ($(seriesType).val() == "3") {

                    $('#pnlTextSeries').removeClass('hidden');
                    Desc = "<ul>";
                    Desc = "<li>In this, on profile setting, starting no. of invoice & Unique no. will continue.</li>";
                    Desc += "</ul>";
                    $('#SeriesDesc').html(Desc);
                }
            }

            $('#<%=ddlSeriesType.ClientID%>').change(function () {
                SeriesInit('#' + this.id);
            });

            function SeriesValidation() {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For GSTIN -------------------//

            if ($('#<%=txtGSTIN.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter GSTIN No.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtGSTIN.ClientID%>').focus();
                return false;
            }

            //------------------For Address -------------------//

            if ($('#<%=txtRegAddress.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Address.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtRegAddress.ClientID%>').focus();
                return false;
            }

            //------------------For Date -------------------//

            if ($('#<%=txtRegDate.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Date.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtRegDate.ClientID%>').focus();
                return false;
            }

            //------------------For City -------------------//

            if ($('#<%=txtCity.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter City.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtCity.ClientID%>').focus();
                return false;
            }

            //------------------For State -------------------//

            if ($('#<%=ddlState.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select State.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=ddlState.ClientID%>').focus();
                return false;
            }

            //------------------For Pin Code -------------------//

            if ($('#<%=txtPinCode.ClientID%>').val().length != 6) {
                $('#<%=lblMsg.ClientID%>').html('Please Enter 6 Digit Pin Code.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class="fa fa-info-circle fa-lg"></i>');
                $('#<%=txtPinCode.ClientID%>').focus();
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
            <div class="content-wrapper">
                <h3 class="text-center" style="padding: 5px">Registration
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">



                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Full Name&nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="Full Name" MaxLength="20" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">User ID&nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtUserId" CssClass="form-control" placeholder="User ID" MaxLength="15" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Password&nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Password" MaxLength="20" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Mobile No.&nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtMobileNo" CssClass="form-control numberonly" placeholder="Mobile No." MaxLength="10" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">GSTIN&nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtGSTIN" CssClass="form-control GSTIN text-uppercase" placeholder="GSTIN No." MaxLength="15" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Registration Address &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtRegAddress" CssClass="form-control" placeholder="Registration Address" MaxLength="130" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Registration Date &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtRegDate" CssClass="form-control datepicker" placeholder="DD/MM/YYYY" MaxLength="10" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">City &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtCity" CssClass="form-control Alphaonly" placeholder="City" MaxLength="25" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">State &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlState" CssClass="form-control" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">PinCode &nbsp;<span class="text-danger">*</span></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtPinCode" CssClass="form-control numberonly" placeholder="PinCode" MaxLength="6" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Authorized Signatory </label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtAuthorizedSign" CssClass="form-control" placeholder="Authorized Signatory" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Signatory Designation</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtAuthorizedDesi" CssClass="form-control" placeholder="Signatory Designation" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <div class="col-md-11 Head">
                                                    <span class="col-md-3" style="margin-top: 3px;">Series Type &nbsp;<span class="text-danger">*</span></span>
                                                    <div id="pnlSerisType" class="col-md-8" style="margin: 2px;">
                                                        <asp:DropDownList ID="ddlSeriesType" CssClass="form-control" runat="server" placeholder="Select Series Type">
                                                            <asp:ListItem Value="0" Text="--Select--" />
                                                            <asp:ListItem Value="1" Text="Manual Series" />
                                                            <asp:ListItem Value="2" Text="Multiple Series" />
                                                            <asp:ListItem Value="3" Text="Numeric Series" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divDefault" class="hidden">
                                                <div class="col-md-12 p0">
                                                    <div class="col-md-12 p0">
                                                        <div class="panel-group">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" href="#pnlDescription">Description</a>
                                                                    </h4>
                                                                </div>
                                                                <div id="pnlDescription" class="panel-collapse collapse in">
                                                                    <div class="panel-body" id="SeriesDesc">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div id="pnlddlCashCredit" class="col-md-3">
                                                        Sales Type
                                                        <asp:DropDownList ID="ddlCashCredit" CssClass="form-control" placeholder="Select Account Type" runat="server">
                                                            <asp:ListItem Value="-1" Text="--Select--" />
                                                            <asp:ListItem Value="1" Text="Cash" />
                                                            <asp:ListItem Value="0" Text="Credit" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="pnlTextSeries" class="col-md-3 hidden">
                                                        Series
                                                        <asp:TextBox CssClass="form-control text-uppercase Series" ID="txtSeries" placeholder="Series" runat="server" />
                                                    </div>
                                                    <div id="pnlSrNoAuto" class="col-md-3 hidden">
                                                        Sr. No. Type
                                                        <asp:DropDownList CssClass="form-control " ID="ddlSrNoAuto" placeholder="Sr. No. Generate Type" runat="server">
                                                            <asp:ListItem Value="0" Text="--Select--" />
                                                            <asp:ListItem Value="1" Text="Manual" />
                                                            <asp:ListItem Value="2" Selected="True" Text="Auto Generate" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div id="pnlSerialNo" class="col-md-3">
                                                        Starting Sr. No.
                                                        <asp:TextBox CssClass="form-control numberonly" ID="txtSerialNo" MaxLength="10" placeholder="Starting Sr. No." runat="server" />
                                                    </div>
                                                    <div id="pnlbtnAdd" class="col-md-3">
                                                        <br />
                                                        <asp:Button ID="btnAddSeries" CssClass="btn btn-primary btn-sm" Text="Add" OnClientClick="return SeriesValidation()" OnClick="btnAddSeries_Click" runat="server" />
                                                        <asp:Button ID="btnClearSeries" CssClass="btn btn-danger btn-sm" Text="Clear" runat="server" OnClick="btnClearSeries_Click" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvCreateSeries" HeaderStyle-CssClass="inf_head text-center" CssClass="table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="gvCreateSeries_RowCommand" OnRowDataBound="gvCreateSeries_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acc Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCashCreditInd" Text='<%#Eval("CashCreditInd") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Series">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeries" Text='<%#Eval("Series") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sr. No. Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNoAutoInd" Text='<%#Eval("SerialNoManualInd") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" Text='<%#Eval("SerialNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Button CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-danger btn-xs" Text="Del" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlBranch" Visible="false" CssClass="col-sm-4" runat="server">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Branch Name</label>
                                                <div class="col-sm-12">
                                                    <asp:CheckBoxList ID="chkLstBranch" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-left">
                                            Mandatory Fields <span class="text-danger">*</span>
                                        </div>
                                        <div class="pull-right">
                                           
                                            <asp:Label ID="lblMessage" CssClass="text-danger" runat="server" />
                                            
                                            
                                               <asp:Button ID="btncheck" Text="Check Net Connection" CssClass="btn btn-primary"  runat="server"  OnClick="btncheck_Click"/>
                                            

                                               <asp:Button ID="btnShow" Text="Show MAC Address" CssClass="btn btn-primary"  runat="server"  OnClick="btnShow_Click"/>
                                                 <asp:Button ID="btnGenerateOTP" Text="Show OTP" CssClass="btn btn-primary"  runat="server"  OnClick="btnGenerateOTP_Click"/>
                                            
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" OnClientClick="return Validation()" runat="server" />
                                            <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfPanNo" runat="server" />
                                <asp:HiddenField ID="hfStateID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                SeriesInit('#<%=ddlSeriesType.ClientID%>');

                function SeriesInit(seriesType) {
                    debugger;
                    $('#pnlTextSeries').addClass('hidden');
                    $('#pnlddlCashCredit').addClass('hidden');

                    var Desc = "";
                    $('#divDefault').removeClass('hidden');
                    if ($(seriesType).val() == "0") {
                        $('#divDefault').addClass('hidden');
                    }
                    else if ($(seriesType).val() == "1") {
                        $('#pnlddlCashCredit').removeClass('hidden');
                        Desc = "<ul>";
                        Desc += "<li>In this, the user will enter invoice series manually on Sales Invoice</li>";
                        Desc += "<li>In Profile setting he has choice to select Cash/Credit or both and give the starting  no from which series is to be start.</li>";
                        Desc += "</ul>";
                        $('#SeriesDesc').html(Desc);
                    }
                    else if ($(seriesType).val() == "2") {
                        $('#pnlTextSeries').removeClass('hidden');
                        $('#pnlddlCashCredit').removeClass('hidden');
                        Desc = "<ul>";
                        Desc = "<li>Here Client has to choice to select multiple series & select starting invoice no individually.</li>";
                        Desc += "</ul>";
                        $('#SeriesDesc').html(Desc);
                    }
                    else if ($(seriesType).val() == "3") {

                        $('#pnlTextSeries').removeClass('hidden');
                        Desc = "<ul>";
                        Desc = "<li>In this, on profile setting, starting no. of invoice & Unique no. will continue.</li>";
                        Desc += "</ul>";
                        $('#SeriesDesc').html(Desc);
                    }

                }

                $('#<%=ddlSeriesType.ClientID%>').change(function () {
                    SeriesInit('#' + this.id);
                });

                function SeriesValidation() {
                    return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

