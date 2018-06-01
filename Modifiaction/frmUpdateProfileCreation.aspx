<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmUpdateProfileCreation.aspx.cs" Inherits="Modifiaction_frmUpdateProfileCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        legend, .Head {
            margin-bottom: 10px;
            font-size: 15px;
            background: #1c75bf;
            color: white;
            padding: 0px 8px;
        }

        .form-control {
            height: 26px;
            padding: 2px 10px;
        }

        .panel {
            /*height: 510px;
            overflow: auto;*/
        }

        input[type="text"]::placeholder {
            color: black !important;
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
            //ChoosenDDL();

            SeriesInit('#<%=ddlSeriesType.ClientID%>');

            function SeriesInit(seriesType) {
                debugger;
                $('#pnlTextSeries').addClass('hidden');
                $('#pnlddlCashCredit').addClass('hidden');
                //$('#pnlSrNoAuto').addClass('hidden');
                //$('#ddlSrNoAuto').removeAttr("disabled");
                //$('#txtSerialNo').removeAttr('disabled');

                var Desc = "";
                $('#divDefault').removeClass('hidden');
                if ($(seriesType).val() == "0") {
                    $('#divDefault').addClass('hidden');
                }
                else if ($(seriesType).val() == "1") {
                    //$('#pnlSrNoAuto').removeClass('hidden');
                    $('#pnlddlCashCredit').removeClass('hidden');
                    //$('#pnlTextSeries').removeClass('hidden');
                    Desc = "<ul>";
                    Desc += "<li>In this, the user will enter invoice series manually on Sales Invoice</li>";
                    Desc += "<li>In Profile setting he has choice to select Cash/Credit or both and give the starting  no from which series is to be start.</li>";
                    Desc += "</ul>";
                    $('#SeriesDesc').html(Desc);

                    //$('#ddlSrNoAuto').val("1");
                }
                else if ($(seriesType).val() == "2") {
                    $('#pnlTextSeries').removeClass('hidden');
                    $('#pnlddlCashCredit').removeClass('hidden');
                    Desc = "<ul>";
                    Desc = "<li>Here Client has to choice to select multiple series & select starting invoice no individually.</li>";
                    Desc += "</ul>";
                    $('#SeriesDesc').html(Desc);

                    //$('#ddlSrNoAuto').val("0");
                }
                else if ($(seriesType).val() == "3") {

                    //$('#pnlSrNoAuto').removeClass('hidden');
                    //$('#ddlSrNoAuto').val("0");
                    //$('#txtSerialNo').attr('disabled', 'disabled');
                    $('#pnlTextSeries').removeClass('hidden');
                    Desc = "<ul>";
                    Desc = "<li>In this, on profile setting, starting no. of invoice & Unique no. will continue.</li>";
                    Desc += "</ul>";
                    $('#SeriesDesc').html(Desc);
                }

                //SrNoAutoInit($('#ddlSrNoAuto').val());
            }

            $('#<%=ddlSeriesType.ClientID%>').change(function () {
                SeriesInit('#' + this.id);
            });

            //$('#ddlSrNoAuto').change(function () {
            //    SrNoAutoInit(this.value);
            //})

            //function SrNoAutoInit(srNoAuto) {
            //    debugger
            //    if (srNoAuto == "1") {
            //        $('#txtSerialNo').val('');
            //        //$('#txtSerialNo').attr('disabled', 'disabled');
            //    }
            //    else {
            //        $('#txtSerialNo').removeAttr('disabled');
            //    }
            //}

            function Validation() {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function RequiredValidation() {

            //------------------------------------------------------ Start Basic Info Validation ------------------------------------------------------//

            //------------------For Company Name -------------------//

            if ($('#<%=txtCompanyName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Company Name.');
                $('#<%=txtCompanyName.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtCompanyName.ClientID%>').val().length < 8) {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter Atleast 8 Digit Company Name.');
                $('#<%=txtCompanyName.ClientID%>').focus();
                return false;
            }

            //------------------For Org Type -------------------//

            if ($('#<%=ddlOrgType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Org Type.');
                $('#<%=ddlOrgType.ClientID%>').focus();
                return false;
            }

            //------------------For Business Nature -------------------//

            if ($('#<%=ddlBussiNature.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Business Nature.');
                $('#<%=ddlBussiNature.ClientID%>').focus();
                return false;
            }

            //------------------For Business Type -------------------//

            if ($('#<%=ddlBussiType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Business Type.');
                $('#<%=ddlBussiType.ClientID%>').focus();
                return false;
            }

            //------------------For Address -------------------//

            if ($('#<%=txtAddress.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Address.');
                $('#<%=txtAddress.ClientID%>').focus();
                return false;
            }

            //------------------For City -------------------//

            if ($('#<%=txtCityCompany.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter City.');
                $('#<%=txtCityCompany.ClientID%>').focus();
                return false;
            }

            //------------------For Company State -------------------//

            if ($('#<%=ddlStateCompany.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select State.');
                $('#<%=ddlStateCompany.ClientID%>').focus();
                return false;
            }

            //------------------For Company Pin Code -------------------//


            if ($('#<%=txtPincodeCompany.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Pincode.');
                $('#<%=txtPincodeCompany.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtPincodeCompany.ClientID%>').val() != '' && $('#<%=txtPincodeCompany.ClientID%>').val().length != 6) {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter 6 Digit Company Pin Code.');
                $('#<%=txtPincodeCompany.ClientID%>').focus();
                return false;
            }

            //------------------For Pan No -------------------//

            if ($('#<%=txtPanNo.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Pan No.');
                $('#<%=txtPanNo.ClientID%>').focus();
                return false;
            }


            //------------------For Company Type -------------------//

            if ($('#<%=ddlCompanyType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Company Type.');
                $('#<%=ddlCompanyType.ClientID%>').focus();
                return false;
            }


            //------------------------------------------------------ End Basic Info Validation ------------------------------------------------------//


            //------------------------------------------------------ Start GST Info Validation ------------------------------------------------------//

            //------------------For GSTIN No -------------------//
            if ($('#<%=ddlCompanyType.ClientID%>').val() != '3') {
                if ($('#<%=txtGSTIN.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter GSTIN No.');
                    $('#<%=txtGSTIN.ClientID%>').focus();
                    return false;
                }

                //------------------For Registration Address -------------------//

                if ($('#<%=txtRegAddress.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Registration Address.');
                    $('#<%=txtRegAddress.ClientID%>').focus();
                    return false;
                }

                //------------------For Registration Date -------------------//

                if ($('#<%=txtRegDate.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Registration Date.');
                    $('#<%=txtRegDate.ClientID%>').focus();
                    return false;
                }

                //------------------For GST Info City -------------------//

                if ($('#<%=txtCityGSTIN.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter GST Info City.');
                    $('#<%=txtCityGSTIN.ClientID%>').focus();
                    return false;
                }

                //------------------For GST Info State -------------------//

                if ($('#<%=ddlStateGSTIN.ClientID%>').val() == '0') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select GST Info State.');
                    $('#<%=ddlStateGSTIN.ClientID%>').focus();
                    return false;
                }

                //------------------For GST Info PinCode -------------------//

                if ($('#<%=txtPincodeGSTIN.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter GST Info Pincode.');
                    $('#<%=txtPincodeGSTIN.ClientID%>').focus();
                    return false;
                }

                if ($('#<%=txtPincodeGSTIN.ClientID%>').val() != '' && $('#<%=txtPincodeGSTIN.ClientID%>').val().length != 6) {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter 6 Digit GST Info Pin Code.');
                    $('#<%=txtPincodeGSTIN.ClientID%>').focus();
                    return false;
                }

                //------------------For GST Info Authorized Signatory -------------------//

                if ($('#<%=txtAuthorizedSign.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Authorized Signatory.');
                    $('#<%=txtAuthorizedSign.ClientID%>').focus();
                    return false;
                }

                //------------------For GST Info Signatory Designation -------------------//

                if ($('#<%=txtAuthorizedDesi.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Signatory Designation.');
                    $('#<%=txtAuthorizedDesi.ClientID%>').focus();
                    return false;
                }
            }

            //------------------------------------------------------ End GST Info Validation ------------------------------------------------------//


            //--------------------------------------------------- Start Contact Person Validation ---------------------------------------------------//

            //------------------For Contact Person Mobile number -------------------//

            if ($('#<%=txtMobileNoPerson.ClientID%>').val() != '' && $('#<%=txtMobileNoPerson.ClientID%>').val().length != 10) {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter 10 Digit Contact Person Mobile No.');
                $('#<%=txtMobileNoPerson.ClientID%>').focus();
                return false;
            }

            //--------------------------------------------------- End Contact Person Validation ---------------------------------------------------//


            //--------------------------------------------------- Start Turnover Validation ---------------------------------------------------//

            if ($('#<%=ddlTurnover.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Turnover.');
                $('#<%=ddlTurnover.ClientID%>').focus();
                return false;
            }

            //--------------------------------------------------- End Turnover Validation ---------------------------------------------------//

            //--------------------------------------------------- Start Bank Information Validation ---------------------------------------------------//

            <%--if ($('#<%=txtBankName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Bank Name.');
                $('#<%=txtBankName.ClientID%>').focus();
                return false;
            }
            if ($('#<%=txtIFSCCode.ClientID%>').val() == '' ){
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter IFSC Code.');
                $('#<%=txtIFSCCode.ClientID%>').focus();
                return false;
            }
            if ($('#<%=txtAccountNumber.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Account Number.');
                $('#<%=txtAccountNumber.ClientID%>').focus();
                return false;
            }--%>

            //--------------------------------------------------- End Bank Information Validation ---------------------------------------------------//
        }
    </script>
    <script type="text/javascript">
        function ValidPanNo() {

            var panNo = document.getElementById('<%=txtPanNo.ClientID%>').value;
            if (panNo != "") {
                var regex = /^[A-Z]{5}\d{4}[A-Z]{1}$/;  //this is the pattern of regular expersion
                if (regex.test(panNo) == false) {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please enter valid pan number for ex. (ABCDE4512A).');
                    $('#<%=txtPanNo.ClientID%>').val("");
                    $('#<%=txtPanNo.ClientID%>').focus();
                    return false;
                }
                $('#<%=lblMsg.ClientID%>').html('');
                $('#<%=txtTanNo.ClientID%>').focus();
            }
            else {
                $('#<%=lblMsg.ClientID%>').html('');
            }
        }


        function ClickCheckBox(value) {
            if (document.getElementById('ContentPlaceHolder1_chkColumn1').checked == true && document.getElementById('ContentPlaceHolder1_txtColumn1').value == '') {
                document.getElementById('ContentPlaceHolder1_txtColumn1').value = value;
            } else if (document.getElementById('ContentPlaceHolder1_chkColumn2').checked == true && document.getElementById('ContentPlaceHolder1_txtColumn2').value == '') {
                document.getElementById('ContentPlaceHolder1_txtColumn2').value = value;
            } else if (document.getElementById('ContentPlaceHolder1_chkColumn3').checked == true && document.getElementById('ContentPlaceHolder1_txtColumn3').value == '') {
                document.getElementById('ContentPlaceHolder1_txtColumn3').value = value;
            } else if (document.getElementById('ContentPlaceHolder1_chkColumn4').checked == true && document.getElementById('ContentPlaceHolder1_txtColumn4').value == '') {
                document.getElementById('ContentPlaceHolder1_txtColumn4').value = value;
            }
        }



    </script>
    <script type="text/javascript">
        function ToUpper(ctrl) {
            var panNo = ctrl.value;
            ctrl.value = panNo.toUpperCase();
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
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>

            <div class="container-fluid">
                <div class="wpr">
                    <div class="content-wrapper">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <style>
                                    .required_span {
                                        color: red;
                                        font-size: 14px;
                                        position: absolute;
                                        top: -10px;
                                        right: 8px;
                                    }

                                    .required_span_label {
                                        color: red;
                                        font-size: 14px;
                                    }
                                </style>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCompanyName" CssClass="form-control" placeholder="Company Name" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtShortName" CssClass="form-control" placeholder="Short Name" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlOrgType" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                            </asp:DropDownList><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlBussiNature" CssClass="form-control" runat="server">
                                            </asp:DropDownList><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlBussiType" CssClass="form-control" runat="server">
                                            </asp:DropDownList><span class="required_span">*</span>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="Address" MaxLength="130" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtCityCompany" CssClass="form-control Alphaonly" placeholder="City" MaxLength="25" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlStateCompany" CssClass="form-control" runat="server">
                                            </asp:DropDownList><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPincodeCompany" CssClass="form-control numberonly" placeholder="Pincode" MaxLength="6" runat="server" /><span class="required_span">*</span>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLandLineNo" CssClass="form-control AlphaNum" placeholder="Landline No." MaxLength="40" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtFaxNo" CssClass="form-control numberonly" placeholder="Fax No." runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtEmail" CssClass="form-control Email" placeholder="Email" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtPanNo" onfocusout="ValidPanNo()" onkeyup="ToUpper(this)" MaxLength="10" CssClass="form-control PanNo" placeholder="Pan No." runat="server" /><span class="required_span">*</span>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtTanNo" CssClass="form-control" placeholder="Tan No." MaxLength="10" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCINNo" CssClass="form-control" placeholder="CIN No." MaxLength="21" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtImportExportCode" CssClass="form-control numberonly" placeholder="Import/Export Code" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlCompanyType" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0" Text="-- Company Type --" />
                                                <asp:ListItem Value="1" Text="Normal Business" />
                                                <asp:ListItem Value="2" Text="Composition Opted" />
                                                <asp:ListItem Value="3" Text="UnRegistered" />
                                            </asp:DropDownList><span class="required_span">*</span>
                                        </div>
                                    </div>
                                </div>

                                <legend>GST Information<span class="pull-right">
                                    <asp:CheckBox ID="chkSameAsAbove" Text="Same As Above" AutoPostBack="true" OnCheckedChanged="chkSameAsAbove_CheckedChanged" runat="server" /></span></legend>
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtGSTIN" CssClass="form-control GSTIN text-uppercase" placeholder="GSTIN No." MaxLength="15" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtRegAddress" CssClass="form-control" placeholder="Registration Address" MaxLength="130" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtRegDate" CssClass="form-control datepicker" placeholder="Registration Date" MaxLength="10" runat="server" /><span class="required_span">*</span>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtCityGSTIN" CssClass="form-control Alphaonly" placeholder="City" MaxLength="25" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlStateGSTIN" CssClass="form-control" runat="server">
                                            </asp:DropDownList><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtPincodeGSTIN" CssClass="form-control numberonly" placeholder="Pincode" MaxLength="6" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtAuthorizedSign" CssClass="form-control" placeholder="Authorized Signatory" runat="server" /><span class="required_span">*</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtAuthorizedDesi" CssClass="form-control" placeholder="Signatory Designation" runat="server" /><span class="required_span">*</span>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-horizontal">

                                    <div class="col-sm-6" style="padding-left: 0">
                                        <legend>Contact Person</legend>
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtPersonName" CssClass="form-control Alphaonly" placeholder="Person Name" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtDesiPerson" CssClass="form-control" placeholder="Designation Person" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtEmailPerson" CssClass="form-control Email" placeholder="Email" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtMobileNoPerson" CssClass="form-control numberonly" placeholder="Mobile No." MaxLength="10" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6" style="padding-right: 0">
                                        <legend>Alternate Contact Person</legend>
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtPersonNameAlter" CssClass="form-control Alphaonly" placeholder="Person Name" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtDesiPersonAlter" CssClass="form-control" placeholder="Designation Person" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtEmailPersonAlter" CssClass="form-control Email" placeholder="Email" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtMobileNoPersonAlter" CssClass="form-control numberonly" placeholder="Mobile No." MaxLength="10" runat="server" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <hr style="width: 100%;" />

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-4">Inventory Management<span class="required_span_label">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlInventoryManag" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0" Text="No" />
                                                <asp:ListItem Value="1" Text="Yes" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4">Stock Maintain In Secondary Unit<span class="required_span_label">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlStockMISecUnit" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0" Text="No" />
                                                <asp:ListItem Value="1" Text="Yes" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="col-sm-10" style="margin-left: -14px;">Cheque Series Applicable On Bank Payment<span class="required_span_label">*</span></label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlChequeSeriesApplicable" CssClass="form-control" Style="margin-left: 25px;" runat="server">
                                                    <asp:ListItem Value="0" Text="No" />
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <label class="col-sm-4">Sales Invoice Series Available<span class="required_span_label">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSIServiceAvailable" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSIServiceAvailable_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Value="0" Text="No" />
                                                <asp:ListItem Value="1" Text="Yes" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtInvioceSeries" Enabled="false" CssClass="form-control" placeholder="Invoice Series" MaxLength="10" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtStartingNo" Enabled="false" CssClass="form-control numberonly" placeholder="Starting No." MaxLength="6" runat="server" />
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <label class="col-sm-4">Sales Invoice On PrePrinted Stationary<span class="required_span_label">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSIOnPrePrinted" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSIOnPrePrinted_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Value="0" Text="No" />
                                                <asp:ListItem Value="1" Text="Yes" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">


                                            <button id="btnCopyTypeToggle" runat="server" onclick="Opendrop();" class="form-control input-sm cb-button" type="button">Copy Type<i class="caret pull-right"></i></button>
                                            <div id="divdrop" class="collapse cb-div">
                                                <div>
                                                    <%--<asp:CheckBox Checked="false" ID="cbSelectAllCopy" AutoPostBack="true" Text="Select All" onclick="Closedrop(this);" OnCheckedChanged="cbSelectAllCopy_CheckedChanged" runat="server" />--%>
                                                    <asp:CheckBoxList onclick="Closedrop(this);" CssClass="cb-list" ID="cbCopyType" AutoPostBack="true" OnSelectedIndexChanged="cbCopyType_SelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </div>

                                            <asp:Panel ID="divCopyType" class="pos-abs" Style="width: 90%; display: none; border: 1px solid #1c75bf;" runat="server">
                                            </asp:Panel>
                                        </div>

                                        <div class="col-sm-1 pr-0">
                                            <asp:TextBox ID="txtExtra1" CssClass="form-control Extra12" MaxLength="30" Enabled="false" placeholder="Extra1" runat="server" />
                                        </div>
                                        <div class="col-sm-1 pr-0">
                                            <asp:TextBox ID="txtExtra2" CssClass="form-control Extra12" MaxLength="30" Enabled="false" placeholder="Extra2" runat="server" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:TextBox ID="txtNoPrintedCopy" Enabled="false" CssClass="form-control" placeholder="No. of Copies To be Printed" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4">Composition Opted<span class="required_span_label">*</span></label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlComposiOpted" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlComposiOpted_SelectedIndexChanged" placeholder="Person Name" runat="server">
                                                <asp:ListItem Value="0" Text="No" />
                                                <asp:ListItem Value="1" Text="Yes" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCompoEffDate" Enabled="false" CssClass="form-control datepicker" placeholder="Composition Effective Date" MaxLength="10" runat="server" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlCompositionCategory" Enabled="false" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <hr style="width: 100%;" />

                                <div class="form-horizontal">
                                    <div class="col-sm-6" style="padding-left: 0">
                                        <legend>Upload Company Logo</legend>
                                        <div class="form-group">
                                            <label class="col-sm-3">
                                                Company Logo 
                                                        <br />
                                                <span style="color: red; font-weight: normal; font-size: 12px">(PNG/JPG/JPEG/BMP)</span></label>
                                            <div class="col-sm-9">
                                                <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:Image ID="imgCompanyLogo" CssClass="img-responsive" runat="server" />
                                            <asp:Label ID="lblImageName" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblUpdateImageName" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:Button ID="btnUploadCompanyLogo" Text="Upload" CssClass="btn btn-primary btn-space-right" OnClick="btnUploadCompanyLogo_Click" runat="server" />
                                        </div>
                                        <br />
                                    </div>
                                    <div class="col-sm-6" style="padding-right: 0">
                                        <legend>Additional Information To Be Print On Invoice</legend>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtInvoiceCaption1" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlInvoicePrint1" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Print No" Value="0" />
                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtInvoiceCaption2" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlInvoicePrint2" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Print No" Value="0" />
                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtInvoiceCaption3" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlInvoicePrint3" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Print No" Value="0" />
                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtInvoiceCaption4" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlInvoicePrint4" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Print No" Value="0" />
                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtInvoiceCaption5" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlInvoicePrint5" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Print No" Value="0" />
                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <hr style="width: 100%;" />

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <legend>Invoice Terms & Condition</legend>
                                            <label class="col-sm-2">Terms</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtTerms" CssClass="form-control Required" data-Group="termsAdd" placeholder="Terms" MaxLength="150" runat="server" />
                                            </div>
                                            <div class="col-sm-3 text-center">
                                                <asp:Button ID="btnAddTerms" CssClass="btn btn-primary btn-space-right" Style="padding: 2px 8px" data-Group="termsAdd" OnClick="btnAddTerms_Click" Text="Add" runat="server" />
                                            </div>
                                            <br />
                                            <br />
                                            <asp:GridView ID="gvTermsCon" ShowHeader="false" CssClass="table table-bordered table-terms" OnRowCommand="gvTermsCon_RowCommand" AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-CssClass="hidden" DataField="UserID" />
                                                    <asp:BoundField ItemStyle-CssClass="hidden" DataField="IP" />
                                                    <asp:BoundField ItemStyle-CssClass="hidden" DataField="TermID" />
                                                    <asp:BoundField ItemStyle-CssClass="" DataField="Terms" ItemStyle-Width="75%" />
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" ItemStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:Button CommandName="EditRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-primary" Text="Edit" Style="padding: 2px 8px" runat="server" />
                                                            <asp:Button CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-danger" Text="Del" Style="padding: 2px 8px" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hfTermID" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">

                                                <legend>Choose Invoice Format (Only One Format Can Choose)</legend>
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="grdReportFormats" ShowHeader="false" CssClass="table table-bordered table-choose-invoice-format" OnRowCommand="grdReportFormats_RowCommand" AutoGenerateColumns="false" runat="server">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-CssClass="">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelectOnce" onclick="UncheckOthers(this);" runat="server" />
                                                                            <asp:Label ID="lblFormatID" Text='<%#Eval("FormatID") %>' Visible="false" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-CssClass="hidden" DataField="FormatID" />
                                                                    <asp:BoundField ItemStyle-CssClass="" DataField="FormatName" />
                                                                    <asp:TemplateField ItemStyle-CssClass=" text-center">
                                                                        <ItemTemplate>
                                                                            <asp:Button CommandName="View" CommandArgument='<%#Eval("FormatPath") %>' target="_blank" CssClass="btn btn-xs btn-primary btn-sxs" Text="View" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hfFormatID" runat="server" />
                                                <asp:HiddenField ID="hfCustomerCopy" runat="server" />
                                                <asp:HiddenField ID="hfBusinessCopy" runat="server" />
                                                <asp:HiddenField ID="hfDuplicateCopy" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="col-md-12 Head">
                                                    <span class="col-md-3" style="margin-top: 3px;">Series Type</span>
                                                    <div id="pnlSerisType" class="col-md-8" style="margin: 2px;">
                                                        <asp:DropDownList ID="ddlSeriesType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSeriesType_SelectedIndexChanged" runat="server" placeholder="Select Series Type">
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
                                                    <div class="col-md-12  p0">
                                                        <div class="panel-group">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a data-toggle="collapse" href="#pnlDescription">Description</a>
                                                                    </h4>
                                                                </div>
                                                                <div id="pnlDescription" class="panel-collapse collapse in">
                                                                    <div class="panel-body" id="SeriesDesc">
                                                                        <%--Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.--%>
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
                                                    <asp:TextBox CssClass="form-control text-uppercase Series" MaxLength="15" ID="txtSeries" placeholder="Series" runat="server" />
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
                                                        <asp:Button ID="btnAddSeries" OnClientClick="return Validation()" OnClick="btnAddSeries_Click" CssClass="btn btn-primary btn-sm" Text="Add" runat="server" />
                                                        <asp:Button ID="btnClearSeries" OnClick="btnClearSeries_Click" CssClass="btn btn-danger btn-sm" Text="Clear" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvCreateSeries" HeaderStyle-CssClass="inf_head text-center" CssClass="table-bordered" AutoGenerateColumns="false" OnRowCommand="gvCreateSeries_RowCommand" OnRowDataBound="gvCreateSeries_RowDataBound" runat="server">
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
                                                                        <%--<asp:Button CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-danger btn-xs" Text="Del" runat="server" />--%>
                                                                        <asp:Button CommandName="EditRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-primary btn-xs" Text="Edit" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <legend>Turnover <span style="margin-left: 42%;">Print HSN/SAC Code</span></legend>
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlTurnover" AutoPostBack="true" OnSelectedIndexChanged="ddlTurnover_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0" Text="-- Select --" />
                                                            <asp:ListItem Value="1" Text="Below 1.5 Cr" />
                                                            <asp:ListItem Value="2" Text="Above 1.5 Cr - Upto 5 Cr" />
                                                            <asp:ListItem Value="3" Text="Above 5 Cr" />
                                                        </asp:DropDownList><span class="required_span">*</span>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlPrintTurnover" CssClass="form-control" Enabled="false" runat="server">
                                                            <asp:ListItem Text="No" Value="0" />
                                                            <asp:ListItem Text="Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <legend>Bank Information</legend>
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtBankName" CssClass="form-control" placeholder="Bank Name" MaxLength="45" runat="server" />
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtIFSCCode" CssClass="form-control" placeholder="IFSC Code" MaxLength="11" runat="server" />
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtAccountNumber" CssClass="form-control" placeholder="Account Numbre" MaxLength="20" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <legend>&nbsp;</legend>
                                            <div class="form-HoriZontal">
                                                <div class="form-group">

                                                    <div class="col-sm-12">
                                                        <asp:CheckBox ID="ChkSsTaken" runat="server" Text="Store System Is Taken" />
                                                    </div>

                                                    <div class="col-sm-12">
                                                        <asp:CheckBox ID="ChkBsObtain" runat="server" Text="Brokerage System Obtain" />
                                                    </div>


                                                    <div class="col-sm-12">
                                                        <asp:CheckBox ID="ChkCostCenter" runat="server" Text="Cost-Centre(SubSection) Concept Is Taken" />
                                                    </div>

                                                    <div class="col-sm-12">
                                                        <asp:CheckBox ID="ChkBudget" runat="server" Text="Budget Concept Taken" />
                                                    </div>

                                                </div>
                                            </div>

                                            <legend>Budget Amount</legend>
                                            <div class="form-horizontal">
                                                <div class="col-sm-12">
                                                    <div class="form-group">

                                                        <label class="col-sm-3">Column No.</label>

                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="ddlColumnNo" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlColumnNo_SelectedIndexChanged">
                                                                <asp:ListItem Text="1" Value="1" Selected="True" />
                                                                <asp:ListItem Text="2" Value="2" />
                                                                <asp:ListItem Text="3" Value="3" />
                                                                <asp:ListItem Text="4" Value="4" />
                                                                <asp:ListItem Text="5" Value="5" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label class="col-sm-3">Budget Amount</label>

                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="ddlAmount" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                <asp:ListItem Text="----Select----" Value="0" Selected="True" />
                                                                <asp:ListItem Text="In Thousand" Value="In Thousand" />
                                                                <asp:ListItem Text="In Lakh" Value="In Lakh" />
                                                                <asp:ListItem Text="In Crore" Value="In Crore" />
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-12">
                                                            <div class="col-sm-9" id="divColumn1" runat="server" style="display: none;">
                                                                <asp:CheckBox ID="chkColumn1" onclick="ClickCheckBox('Actual Amount 2015-2016');" Text="Actual Amount 2015-2016" runat="server" />
                                                                <asp:TextBox ID="txtColumn1" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-9" id="divColumn2" runat="server" style="display: none;">
                                                                <asp:CheckBox ID="chkColumn2" onclick="ClickCheckBox('Proposed Budget Amount 2016-2017');" Text="Proposed Budget Amount 2016-2017" runat="server" />
                                                                <asp:TextBox ID="txtColumn2" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-9" id="divColumn3" runat="server" style="display: none;">
                                                                <asp:CheckBox ID="chkColumn3" onclick="ClickCheckBox('Sanctioned Budget Amount 2016-2017');" Text="Sanctioned Budget Amount 2016-2017" runat="server" />
                                                                <asp:TextBox ID="txtColumn3" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-9" id="divColumn4" runat="server" style="display: none;">
                                                                <asp:CheckBox ID="chkColumn4" onclick="ClickCheckBox('Actual Amount 2016-2017');" runat="server" Text="Actual Amount 2016-2017" />
                                                                <asp:TextBox ID="txtColumn4" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-9" id="divColumn5" runat="server">
                                                                <asp:CheckBox ID="chkColumn5" Checked="true" Text="Proposed Budget Amount 2017-2018" runat="server" />
                                                                <asp:TextBox ID="txtColumn5" Text="Proposed Budget Amount 2017-2018" CssClass="form-control" runat="server"> </asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnUpdate" Text="Update" CssClass="btn btn-primary btn-space-right" OnClick="btnUpdate_Click" OnClientClick="return RequiredValidation()" runat="server" />
                                            <%--<asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="footer">
                <span>© 2017 All rights reserved 2016-2017 Oswal Computers And Consultants Pvt. Ltd..</span>
            </footer>
            <script type="text/javascript">
                function UncheckOthers(objchkbox) {
                    //Get the parent control of checkbox which is the checkbox list
                    var objchkList = objchkbox.parentNode.parentNode.parentNode;
                    //Get the checkbox controls in checkboxlist
                    var chkboxControls = objchkList.getElementsByTagName("input");
                    //Loop through each check box controls
                    for (var i = 0; i < chkboxControls.length; i++) {
                        //Check the current checkbox is not the one user selected
                        if (chkboxControls[i] != objchkbox && objchkbox.checked) {
                            //Uncheck all other checkboxes
                            chkboxControls[i].checked = false;
                        }
                    }
                }
            </script>
            <%--<script>
                SeriesInit('#ddlSeriesType');

                function SeriesInit(seriesType) {
                    debugger;
                    $('#pnlTextSeries').addClass('hidden');
                    $('#pnlddlCashCredit').addClass('hidden');
                    $('#pnlSrNoAuto').addClass('hidden');
                    //$('#ddlSrNoAuto').removeAttr("disabled");
                    //$('#txtSerialNo').removeAttr('disabled');

                    var Desc = "";

                    $('#divDefault').removeClass('hidden');
                    if ($(seriesType).val() == "0") {
                        $('#divDefault').addClass('hidden');
                    }
                    else if ($(seriesType).val() == "1") {
                        $('#pnlSrNoAuto').removeClass('hidden');
                        $('#pnlddlCashCredit').removeClass('hidden');
                        //$('#pnlTextSeries').removeClass('hidden');

                        Desc = "Manual Series.";
                        $('#SeriesDesc').text(Desc);

                        //$('#ddlSrNoAuto').val("1");
                    }
                    else if ($(seriesType).val() == "2") {
                        $('#pnlTextSeries').removeClass('hidden');
                        $('#pnlddlCashCredit').removeClass('hidden');

                        Desc = "Series Available.";
                        $('#SeriesDesc').text(Desc);

                        $('#ddlSrNoAuto').val("0");
                    }
                    else if ($(seriesType).val() == "3") {

                        $('#pnlSrNoAuto').removeClass('hidden');
                        $('#ddlSrNoAuto').val("0");
                        $('#txtSerialNo').attr('disabled', 'disabled');

                        Desc = "Default Series.";
                        $('#SeriesDesc').text(Desc);
                    }

                    SrNoAutoInit($('#ddlSrNoAuto').val());
                }

                $('#ddlSeriesType').change(function () {
                    SeriesInit('#' + this.id);
                });

                $('#ddlSrNoAuto').change(function () {
                    SrNoAutoInit(this.value);
                })

                function SrNoAutoInit(srNoAuto) {
                    debugger
                    if (srNoAuto == "1") {
                        $('#txtSerialNo').val('');
                        $('#txtSerialNo').attr('disabled', 'disabled');
                    }
                    else {
                        $('#txtSerialNo').removeAttr('disabled');
                    }
                }

                function Validation() {
                    return true;
                }
                </script>--%>

            <script>
                SeriesInit('#<%=ddlSeriesType.ClientID%>');

                function SeriesInit(seriesType) {
                    debugger;
                    $('#pnlTextSeries').addClass('hidden');
                    $('#pnlddlCashCredit').addClass('hidden');
                    //$('#pnlSrNoAuto').addClass('hidden');
                    //$('#ddlSrNoAuto').removeAttr("disabled");
                    //$('#txtSerialNo').removeAttr('disabled');

                    var Desc = "";
                    $('#divDefault').removeClass('hidden');
                    if ($(seriesType).val() == "0") {
                        $('#divDefault').addClass('hidden');
                    }
                    else if ($(seriesType).val() == "1") {
                        //$('#pnlSrNoAuto').removeClass('hidden');
                        $('#pnlddlCashCredit').removeClass('hidden');
                        //$('#pnlTextSeries').removeClass('hidden');
                        Desc = "<ul>";
                        Desc += "<li>In this, the user will enter invoice series manually on Sales Invoice</li>";
                        Desc += "<li>In Profile setting he has choice to select Cash/Credit or both and give the starting  no from which series is to be start.</li>";
                        Desc += "</ul>";
                        $('#SeriesDesc').html(Desc);

                        //$('#ddlSrNoAuto').val("1");
                    }
                    else if ($(seriesType).val() == "2") {
                        $('#pnlTextSeries').removeClass('hidden');
                        $('#pnlddlCashCredit').removeClass('hidden');
                        Desc = "<ul>";
                        Desc = "<li>Here Client has to choice to select multiple series & select starting invoice no individually.</li>";
                        Desc += "</ul>";
                        $('#SeriesDesc').html(Desc);

                        //$('#ddlSrNoAuto').val("0");
                    }
                    else if ($(seriesType).val() == "3") {

                        //$('#pnlSrNoAuto').removeClass('hidden');
                        //$('#ddlSrNoAuto').val("0");
                        //$('#txtSerialNo').attr('disabled', 'disabled');
                        $('#pnlTextSeries').removeClass('hidden');
                        Desc = "<ul>";
                        Desc = "<li>In this, on profile setting, starting no. of invoice & Unique no. will continue.</li>";
                        Desc += "</ul>";
                        $('#SeriesDesc').html(Desc);
                    }

                    //SrNoAutoInit($('#ddlSrNoAuto').val());
                }

                $('#<%=ddlSeriesType.ClientID%>').change(function () {
                    SeriesInit('#' + this.id);
                });

                //$('#ddlSrNoAuto').change(function () {
                //    SrNoAutoInit(this.value);
                //})

                //function SrNoAutoInit(srNoAuto) {
                //    debugger
                //    if (srNoAuto == "1") {
                //        $('#txtSerialNo').val('');
                //        //$('#txtSerialNo').attr('disabled', 'disabled');
                //    }
                //    else {
                //        $('#txtSerialNo').removeAttr('disabled');
                //    }
                //}

                function Validation() {
                    return true;
                }
            </script>

            <style>
                .bodyPop {
                    display: grid;
                    background: white;
                    width: 90%;
                    height: 90%;
                    margin: auto;
                    border: 1px solid grey;
                    border-radius: 6px;
                    box-shadow: 3px 4px 13px rgba(0, 0, 0, 0.4);
                    padding: 15px;
                    z-index: 1040;
                    position: relative;
                }

                .objPop {
                    width: 100%;
                    height: 100%;
                }

                .reportPopUp {
                    width: 83%;
                    height: 100%;
                    top: 0px;
                    bottom: 0;
                    padding-top: 110px;
                    background-color: rgba(0, 0, 0, 0.63);
                    position: fixed;
                    z-index: 1940;
                }
            </style>
            <asp:Panel runat="server" ID="pnlReportFormatPdf" CssClass="reportPopUp" Visible="false" TabIndex="-1">


                <div class="bodyPop">
                    <asp:Button ID="btnClose" Text="&times;" OnClick="btnClose_Click" CssClass="btn btn-danger" runat="server" Style="position: absolute; padding: 0; border-radius: 13%; height: 30px; width: 30px; font-size: 20px; right: 0; top: 0;" />
                    <asp:Literal ID="ltEmbed" runat="server" />
                </div>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadCompanyLogo" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
