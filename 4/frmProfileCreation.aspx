<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmProfileCreation.aspx.cs" Inherits="frmProfileCreation" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accounting System :: Login</title>
    <link rel="shortcut icon" href="../Content/css/images/logo_single_3v1_icon.ico" type="image/x-icon" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <link href="../Content/css/style.css" rel="stylesheet" />
    <link href="../Content/css/custom.css" rel="stylesheet" />
    <script src="../Content/js/jquery.min.js"></script>
    <style>
        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            color: #fff !important;
            background-color: rgb(28, 117, 191) !important;
        }

        .tab-content {
            /*background-color: rgba(176, 216, 230, 0.25) !important;*/
        }

            .tab-content .form-control {
                height: 30px;
                line-height: 1.2em;
                padding: 2px 9px;
                font-size: 13px;
            }

            .tab-content .form-group {
                margin-bottom: 10px;
            }
        /*li:hover {
            background-color: rgb(235, 245, 249) !important;
            
            transition: all .1s ease-in-out;
        }

        li > a:hover {
            color: #FFF !important;
            transform: scale(0.9,0.9);
        }*/


        th {
            text-align: center;
            vertical-align: middle !important;
        }

        .th-inverse tr th {
            background: #1c75bf;
            color: #fff;
        }

        .table-sm tr td, .table-sm tr th {
            padding: 2px 8px !important;
        }


        .required_span {
            color: red;
            font-size: 14px;
            position: absolute;
            top: 0px;
            font-weight: normal;
        }

        .required_span_label {
            color: red;
            font-size: 14px;
        }

        .table-choose-invoice-format tbody tr td {
            padding: 4px;
        }

        .table-terms tbody tr td {
            padding: 4px;
        }

        .textbox-dropdown-input-group input[type="text"],
        .textbox-dropdown-input-group textarea {
            width: 75%;
            max-width: 75%;
            float: left;
            border-bottom-right-radius: 0;
            border-top-right-radius: 0;
            height: 37px;
        }

        .textbox-dropdown-input-group select {
            padding: 3px;
            width: 25%;
            float: left;
            border-left: 0;
            border-bottom-left-radius: 0;
            border-top-left-radius: 0;
        }
        /*.nav-tabs > li > a {
            color: #fafafa;
            background-color: rgba(79, 109, 138, 0.64)!important;
        }*/
        .tab-content .btn {
            padding: 5px 18px !important;
        }

        .tab-content .input-group .btn {
            font-size: 13px !important;
        }
    </style>
    <script type="text/javascript">
        function LoadAllScript() {
            LoadBasic();
            SeriesInit('#<%=ddlSeriesType.ClientID%>');

            function SeriesInit(seriesType) {
                debugger;
                $('#pnlTextSeries').addClass('hidden');
                $('#pnlddlCashCredit').addClass('hidden');

                var Desc = "";
                $('#divDefault').removeClass('hidden');
                $('#divDefaultf').removeClass('hidden');
                if ($(seriesType).val() == "0") {
                    $('#divDefault').addClass('hidden');
                    $('#divDefaultf').addClass('hidden');
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

            function Validation() {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function RequiredValidation() {

            <%--//------------------------------------------------------ Start Basic Info Validation ------------------------------------------------------//

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

            //--------------------------------------------------- End Turnover Validation ---------------------------------------------------//--%>
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
    </script>
    <script type="text/javascript">
        function ToUpper(ctrl) {
            var panNo = ctrl.value;
            ctrl.value = panNo.toUpperCase();
        }
    </script>
    <script type="text/javascript">
        function Tabs1() {
            document.getElementById('<%=btnPrevious.ClientID %>').style.display = "none";
            document.getElementById('<%=btnSave.ClientID %>').style.display = "none";
            document.getElementById('<%=btnNext.ClientID %>').style.display = "block";
            $('#<%=hfTabInd.ClientID %>').val(1);
        }
        function Tabs2(Val) {
            debugger;
            document.getElementById('<%=btnPrevious.ClientID %>').style.display = "block";
            document.getElementById('<%=btnSave.ClientID %>').style.display = "none";
            document.getElementById('<%=btnNext.ClientID %>').style.display = "block";
            $('#<%=hfTabInd.ClientID %>').val(2);
        }
        function Tabs3() {
            document.getElementById('<%=btnPrevious.ClientID %>').style.display = "block";
            document.getElementById('<%=btnSave.ClientID %>').style.display = "none";
            document.getElementById('<%=btnNext.ClientID %>').style.display = "block";
            $('#<%=hfTabInd.ClientID %>').val(3);
        }
        function Tabs4() {
            document.getElementById('<%=btnPrevious.ClientID %>').style.display = "block";
            document.getElementById('<%=btnSave.ClientID %>').style.display = "none";
            document.getElementById('<%=btnNext.ClientID %>').style.display = "block";
            $('#<%=hfTabInd.ClientID %>').val(4);
        }
        function Tabs5() {
            document.getElementById('<%=btnPrevious.ClientID %>').style.display = "block";
            document.getElementById('<%=btnSave.ClientID %>').style.display = "block";
            document.getElementById('<%=btnNext.ClientID %>').style.display = "none";
            $('#<%=hfTabInd.ClientID %>').val(5);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" />
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
                <div class="wpr">
                    <nav role="navigation" class="navbar navbar-default ac_nav">
                        <div class="rows header ">
                            <div class="col-sm-3">
                                <img src="../Content/css/images/logo.png" alt="Image" class="img-rounded img-responsive logo-oswal" style="height: 60px; width: 100px" />
                            </div>
                        </div>
                        <div class="col-sm-6 text-white text-center">
                            <h2 class="product_name">Set Company Profile</h2>
                        </div>
                    </nav>
                    <div class="container-fluid">
                        <div class="wpr">
                            <div class="content-wrapper">
                                <div id="dvTab">
                                    <ul class="nav nav-tabs">
                                        <li id="liGeneral" class="active" onclick="return Tabs1();" runat="server"><a aria-controls="General" role="tab" data-toggle="tab" href="#General">General Information</a></li>
                                        <li id="liContact" onclick="return Tabs2();" runat="server"><a aria-controls="Contact" role="tab" data-toggle="tab" href="#Contact">Contact Details</a></li>
                                        <li id="liGSTinfo" onclick="return Tabs3();" runat="server"><a aria-controls="GSTinfo" role="tab" data-toggle="tab" href="#GSTinfo">GST Information</a></li>
                                        <li id="liOtherSetting" onclick="return Tabs4();" runat="server"><a aria-controls="OtherSetting" role="tab" data-toggle="tab" href="#OtherSetting">Other Setting</a></li>
                                        <li id="liInvoiceSetting" onclick="return Tabs5();" runat="server"><a aria-controls="Invoicesetting" role="tab" data-toggle="tab" href="#Invoicesetting">Invoice Setting</a></li>
                                    </ul>
                                    <asp:HiddenField ID="HiddenTab" runat="server" />
                                    <asp:HiddenField ID="hfTabInd" runat="server" />
                                    <asp:HiddenField ID="hfValidationBtnSaveInd" runat="server" />
                                    <div class="panel panel-default" style="border-top: 0; margin-bottom: 0">
                                        <div class="tab-content  form-horizontal" style="padding: 20px;">
                                            <div id="General" class="tab-pane fade in active" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Company Name <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtCompanyName" CssClass="form-control" placeholder="Company Name" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Short Name
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtShortName" CssClass="form-control" placeholder="Short Name" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Organization Type <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlOrgType" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Business Nature <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlBussiNature" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Business Type <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlBussiType" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Address <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="Address" MaxLength="130" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                City <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtCityCompany" CssClass="form-control Alphaonly" placeholder="City" MaxLength="25" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                State <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlStateCompany" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Pincode <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtPincodeCompany" CssClass="form-control numberonly" placeholder="Pincode" MaxLength="6" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Landline No.
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtLandLineNo" CssClass="form-control AlphaNum" placeholder="Landline No." MaxLength="40" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Fax
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtFaxNo" CssClass="form-control numberonly" placeholder="Fax No." runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Email
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtEmail" CssClass="form-control Email" placeholder="Email" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                PAN No. <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtPanNo" onfocusout="ValidPanNo()" onkeyup="ToUpper(this)" MaxLength="10" CssClass="form-control PanNo" placeholder="Pan No." runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                TAN No.
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtTanNo" CssClass="form-control" placeholder="Tan No." MaxLength="10" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                CIN No.
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtCINNo" CssClass="form-control" placeholder="CIN No." MaxLength="21" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Import / Export Code
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtImportExportCode" CssClass="form-control numberonly" placeholder="Import/Export Code" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Company Type <span class="required_span">*</span>
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlCompanyType" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="0" Text="-- Company Type --" />
                                                                    <asp:ListItem Value="1" Text="Normal Business" />
                                                                    <asp:ListItem Value="2" Text="Composition Opted" />
                                                                    <asp:ListItem Value="3" Text="UnRegistered" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Bank Name
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtBankName" CssClass="form-control" placeholder="Bank Name" MaxLength="45" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                IFSC Code
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtIFSCCode" CssClass="form-control" placeholder="IFSC Code" MaxLength="11" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">
                                                                Account Number
                                                            </label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtAccountNumber" CssClass="form-control" placeholder="Account Number" MaxLength="20" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Contact" class="tab-pane fade" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Person Name</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtPersonName" CssClass="form-control Alphaonly" placeholder="Person Name" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Designation</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtDesiPerson" CssClass="form-control" placeholder="Designation Person" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Email</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtEmailPerson" CssClass="form-control Email" placeholder="Email" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Mobile No.</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtMobileNoPerson" CssClass="form-control numberonly" placeholder="Mobile No." MaxLength="10" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Alternate Person Name</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtPersonNameAlter" CssClass="form-control Alphaonly" placeholder="Person Name" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Alternate Person's Designation</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtDesiPersonAlter" CssClass="form-control" placeholder="Designation Person" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Alternate Person's Email.</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtEmailPersonAlter" CssClass="form-control Email" placeholder="Email" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Alternate Person's Mobile No.</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtMobileNoPersonAlter" CssClass="form-control numberonly" placeholder="Mobile No." MaxLength="10" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="GSTinfo" class="tab-pane fade" runat="server">
                                                <legend class="hidden">GST Information<span class="pull-right"><asp:CheckBox ID="chkSameAsAbove" Text="Same As Above" AutoPostBack="true" OnCheckedChanged="chkSameAsAbove_CheckedChanged" runat="server" /></span></legend>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">GSTIN <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtGSTIN" CssClass="form-control GSTIN text-uppercase" placeholder="GSTIN No." MaxLength="15" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Registration Address <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtRegAddress" CssClass="form-control" placeholder="Registration Address" MaxLength="130" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Registration Date <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtRegDate" CssClass="form-control datepicker" placeholder="Registration Date" MaxLength="10" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">City <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtCityGSTIN" CssClass="form-control Alphaonly" placeholder="City" MaxLength="25" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">State <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlStateGSTIN" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Pincode <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtPincodeGSTIN" CssClass="form-control numberonly" placeholder="Pincode" MaxLength="6" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Autorized Signatory <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtAuthorizedSign" CssClass="form-control" placeholder="Authorized Signatory" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Signatory Designation <span class="required_span">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtAuthorizedDesi" CssClass="form-control" placeholder="Signatory Designation" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 text-right">
                                                                <asp:Button ID="btnAddGSTINInfo" Text="Add" CssClass="btn btn-primary btn-sxs" OnClick="btnAddGSTINInfo_Click" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12" style="margin-top: 7px">
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
                                                        <asp:GridView ID="grdGSTINInfo" AutoGenerateColumns="false" CssClass="table table-bordered th-inverse font-12 table-sm first_tr_hide"
                                                            OnRowCommand="grdGSTINInfo_RowCommand" OnRowDataBound="grdGSTINInfo_RowDataBound" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th class="inf_head" style="width: 11.11%;">GSTIN</th>
                                                                            <th class="inf_head" style="width: 15.11%;">Registration&nbsp;Address</th>
                                                                            <th class="inf_head" style="width: 10.11%;">Registration&nbsp;Date</th>
                                                                            <th class="inf_head" style="width: 12.11%;">City</th>
                                                                            <th class="inf_head" style="width: 15.11%;">State</th>
                                                                            <th class="inf_head" style="width: 11.11%;">Pin&nbsp;Code</th>
                                                                            <th class="inf_head" style="width: 13.11%;">Authorized&nbsp;Signatory</th>
                                                                            <th class="inf_head" style="width: 13.11%;">Signatory&nbsp;Designation</th>
                                                                            <th class="inf_head" style="width: 5.11%;">&nbsp;</th>
                                                                        </tr>
                                                                        <tr class="hide_my_pdosi">
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGSTIN" Text='<%#Eval ("GSTIN") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRegAddress" Text='<%#Eval ("RegistrationAddress") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRegDate" Text='<%#Eval ("RegistrationDate") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCity" Text='<%#Eval ("City") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStateID" Text='<%#Eval ("StateID") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPinCode" Text='<%#Eval ("PinCode") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAuthSignatory" Text='<%#Eval ("AuthorizedSignatury") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSignaDesignation" Text='<%#Eval ("SignaturyDesignation") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="c">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnDelete" Text="Del" CssClass="btn btn-danger btn-xs" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="OtherSetting" class="tab-pane fade" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Inventory Management<span class="required_span_label">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlInventoryManag" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="0" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-sm-12">Stock Maintain In Secondary Unit<span class="required_span_label">*</span></label>
                                                            <div class="col-sm-12">
                                                                <asp:DropDownList ID="ddlStockMISecUnit" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="0" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-sm-12">Sales Invoice On PrePrinted Stationary<span class="required_span_label">*</span></label>
                                                            <div class="col-sm-12">
                                                                <asp:DropDownList ID="ddlSIOnPrePrinted" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSIOnPrePrinted_SelectedIndexChanged" runat="server">
                                                                    <asp:ListItem Value="0" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-sm-12">Composition Opted<span class="required_span_label">*</span></label>
                                                            <div class="col-sm-12">
                                                                <asp:DropDownList ID="ddlComposiOpted" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlComposiOpted_SelectedIndexChanged" placeholder="Person Name" runat="server">
                                                                    <asp:ListItem Value="0" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Composition Effective Date</label>
                                                            <div class="col-xs-12">
                                                                <asp:TextBox ID="txtCompoEffDate" Enabled="false" CssClass="form-control datepicker" placeholder="Composition Effective Date" MaxLength="10" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Composition Category</label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlCompositionCategory" Enabled="false" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Turnover<span class="required_span_label">*</span></label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlTurnover" AutoPostBack="true" OnSelectedIndexChanged="ddlTurnover_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="0" Text="-- Select --" />
                                                                    <asp:ListItem Value="1" Text="Below 1.5 Cr" />
                                                                    <asp:ListItem Value="2" Text="Above 1.5 Cr - Upto 5 Cr" />
                                                                    <asp:ListItem Value="3" Text="Above 5 Cr" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Print HSN/SAC Code</label>
                                                            <div class="col-xs-12">
                                                                <asp:DropDownList ID="ddlPrintTurnover" CssClass="form-control" Enabled="false" runat="server">
                                                                    <asp:ListItem Text="No" Value="0" />
                                                                    <asp:ListItem Text="Yes" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Invoicesetting" class="tab-pane fade" runat="server">
                                                <div class="row">
                                                    <label class="col-xs-12">Additional Information To Be Printed On Invoice</label>
                                                    <div class="col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 textbox-dropdown-input-group">
                                                                <asp:TextBox ID="txtInvoiceCaption1" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                                <asp:DropDownList ID="ddlInvoicePrint1" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Print No" Value="0" />
                                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 textbox-dropdown-input-group">
                                                                <asp:TextBox ID="txtInvoiceCaption2" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                                <asp:DropDownList ID="ddlInvoicePrint2" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Print No" Value="0" />
                                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 textbox-dropdown-input-group">
                                                                <asp:TextBox ID="txtInvoiceCaption3" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                                <asp:DropDownList ID="ddlInvoicePrint3" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Print No" Value="0" />
                                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 textbox-dropdown-input-group">
                                                                <asp:TextBox ID="txtInvoiceCaption4" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                                <asp:DropDownList ID="ddlInvoicePrint4" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Print No" Value="0" />
                                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 textbox-dropdown-input-group">
                                                                <asp:TextBox ID="txtInvoiceCaption5" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                                                <asp:DropDownList ID="ddlInvoicePrint5" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="Print No" Value="0" />
                                                                    <asp:ListItem Text="Print Yes" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="divider-hr"></div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-xs-12 ">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Choose Invoice Format (Only One Format Can Be Choosen)</label>
                                                            <div class="col-xs-12">
                                                                <style>
                                                                    .divider-hr {
                                                                        float: none;
                                                                        clear: both;
                                                                        margin-left: -20px;
                                                                        margin-right: -20px;
                                                                        height: 0px;
                                                                        margin-bottom: 3px;
                                                                        margin-top: 3px;
                                                                        border-bottom: 1px solid #c1e3ff;
                                                                    }
                                                                </style>
                                                                <asp:GridView ID="grdReportFormats" ShowHeader="false" CssClass="table table-no-bordered table-choose-invoice-format table-to-div-asd" OnRowCommand="grdReportFormats_RowCommand" AutoGenerateColumns="false" runat="server">
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
                                                                <asp:HiddenField ID="hfFormatID" runat="server" />
                                                                <asp:HiddenField ID="hfCustomerCopy" runat="server" />
                                                                <asp:HiddenField ID="hfBusinessCopy" runat="server" />
                                                                <asp:HiddenField ID="hfDuplicateCopy" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <div class="">
                                                                <div class="col-xs-9">
                                                                    <div class="form-group">
                                                                        <label class="col-xs-12">Company Logo</label>
                                                                        <div class="col-xs-12">
                                                                            <div class="input-group">
                                                                                <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" />
                                                                                <div class="input-group-btn">
                                                                                    <asp:Button ID="btnUploadCompanyLogo" Text="Upload" CssClass="btn btn-primary btn-space-right" OnClick="btnUploadCompanyLogo_Click" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <span style="color: red; font-weight: normal; font-size: 11px">(File Type : PNG / JPG / JPEG / BMP)</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-xs-3">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-12">Preview :</label>
                                                                        <div class="col-xs-12">
                                                                            <asp:Image ID="imgCompanyLogo" runat="server" Style="max-width: 100%; box-shadow: 0px 0px 3px 0px rgba(0, 0, 0, 0.46);" />
                                                                            <asp:Label ID="lblImageName" runat="server" Visible="false"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="divider-hr"></div>
                                                <div class="row">
                                                    <div class="col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-xs-12">Invoice Terms & Condition</label>
                                                            <div class="col-xs-12">
                                                                <div class="input-group" style="width: 90.5%">
                                                                    <asp:TextBox ID="txtTerms" CssClass="form-control Required" data-Group="termsAdd" placeholder="Terms" MaxLength="150" runat="server" />
                                                                    <span class="input-group-btn">
                                                                        <asp:Button ID="btnAddTerms" CssClass="btn btn-primary" data-Group="termsAdd" OnClick="btnAddTerms_Click" Text="Add" runat="server" />
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12">
                                                                <div style="max-height: 113px; overflow-y: scroll">
                                                                    <asp:GridView ID="gvTermsCon" ShowHeader="false" CssClass="table table-bordered table-terms table-hover" OnRowCommand="gvTermsCon_RowCommand" AutoGenerateColumns="false" runat="server">
                                                                        <Columns>
                                                                            <asp:BoundField ItemStyle-CssClass="hidden" DataField="UserID" />
                                                                            <asp:BoundField ItemStyle-CssClass="hidden" DataField="IP" />
                                                                            <asp:BoundField ItemStyle-CssClass="" DataField="Terms" ItemStyle-Width="80%" />
                                                                            <asp:TemplateField ItemStyle-CssClass="text-center" ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <asp:Button CommandName="EditRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-primary" Text="Edit" Style="padding: 2px 8px" runat="server" />
                                                                                    <asp:Button CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-danger" Text="Del" Style="padding: 2px 8px" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 col-xs-12">
                                                        <label>Copy Type</label>
                                                        <label style="margin-left: 138px">Name Of Copy</label>
                                                        <label style="float: right;">
                                                            No. Of Copies To Be Printed :
                                                            <asp:Label ID="lblNoPrintedCopy" Text="0" runat="server" /></label>
                                                        <div class="row">
                                                            <div class="col-xs-4 col-sm-4">
                                                                <div class="form-group">
                                                                    <div class="col-xs-12">
                                                                        <button id="btnCopyTypeToggle" runat="server" onclick="Opendrop();" class="form-control cb-button" type="button">Copy Type<i class="caret pull-right"></i></button>
                                                                        <div id="divdrop" class="collapse cb-div">
                                                                            <div>
                                                                                <asp:CheckBoxList onclick="Closedrop(this);" CssClass="cb-list" ID="cbCopyType" AutoPostBack="true" OnSelectedIndexChanged="cbCopyType_SelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-4 col-sm-4">
                                                                <div class="form-group">
                                                                    <div class="col-xs-12">
                                                                        <asp:TextBox ID="txtExtra1" CssClass="form-control Extra12" MaxLength="30" Enabled="false" placeholder="Extra1" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-4 col-sm-4">
                                                                <div class="form-group">
                                                                    <div class="col-xs-12">
                                                                        <asp:TextBox ID="txtExtra2" CssClass="form-control Extra12" MaxLength="30" Enabled="false" placeholder="Extra2" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="divider-hr"></div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <div class="form-group">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="col-xs-12 col-sm-12">
                                                                    <div class="form-group">
                                                                        <label>Series Type</label>
                                                                        <div id="pnlSerisType">
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
                                                                    <div id="pnlddlCashCredit" class="col-md-3 col-xs-12 hidden">
                                                                        <label>Sales Type</label>
                                                                        <asp:DropDownList ID="ddlCashCredit" CssClass="form-control" placeholder="Select Account Type" runat="server">
                                                                            <asp:ListItem Value="-1" Text="--Select--" />
                                                                            <asp:ListItem Value="1" Text="Cash" />
                                                                            <asp:ListItem Value="0" Text="Credit" />
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div id="pnlTextSeries" class="col-md-2 col-xs-12 hidden">
                                                                        <label>Series</label>
                                                                        <asp:TextBox CssClass="form-control text-uppercase Series" ID="txtSeries" placeholder="Series" runat="server" />
                                                                    </div>
                                                                    <div id="pnlSrNoAuto" class="col-md-2  col-xs-12 hidden">
                                                                        <label>Sr. No. Type</label>
                                                                        <asp:DropDownList CssClass="form-control " ID="ddlSrNoAuto" placeholder="Sr. No. Generate Type" runat="server">
                                                                            <asp:ListItem Value="0" Text="--Select--" />
                                                                            <asp:ListItem Value="1" Text="Manual" />
                                                                            <asp:ListItem Value="2" Selected="True" Text="Auto Generate" />
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div id="pnlSerialNo" class="col-md-3  col-xs-12">
                                                                        <label>Starting Sr. No.</label>
                                                                        <asp:TextBox CssClass="form-control numberonly" ID="txtSerialNo" MaxLength="10" placeholder="Starting Sr. No." runat="server" />
                                                                    </div>
                                                                    <div id="pnlbtnAdd" class="col-md-4  col-xs-12 text-right">
                                                                        <label class="col-xs-12">&nbsp;</label>
                                                                        <asp:Button ID="btnAddSeries" OnClientClick="return Validation()" OnClick="btnAddSeries_Click" CssClass="btn btn-primary" Text="Add" runat="server" />
                                                                        <asp:Button ID="btnClearSeries" OnClick="btnClearSeries_Click" CssClass="btn btn-danger" Text="Clear" runat="server" />
                                                                    </div>
                                                                    <div class="col-xs-12">
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
                                                                                        <asp:Button CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-danger btn-xs" Text="Del" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div id="divDefaultf" class="hidden">
                                                                    <label>Series Description</label>
                                                                    <div class="col-xs-12 p0">
                                                                        <div class="col-xs-12  p0">
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
                                                    <style>
                                                        .ar_btn_container.left {
                                                            float: left;
                                                        }

                                                        .ar_btn_container.right {
                                                            float: right;
                                                        }

                                                        .ar_btn_container .btn {
                                                            float: left;
                                                        }

                                                        .ar_btn_container .alert {
                                                            display: block;
                                                            float: left;
                                                            padding: 7px;
                                                            margin-right: 5px;
                                                            margin-bottom: 0;
                                                        }
                                                    </style>
                                                    <div class="ar_btn_container right">
                                                        <asp:Label ID="lblMsg" CssClass="alert text-danger" runat="server" />
                                                        <asp:Button ID="btnPrevious" Text="Previous" CssClass="btn btn-warning" OnClick="btnPrevious_Click" Style="display: none; margin-right: 5px" runat="server" />
                                                        <asp:Button ID="btnNext" Text="Next" CssClass="btn btn-success" OnClick="btnNext_Click" runat="server" />
                                                        <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return RequiredValidation()" Style="display: none" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                </div>
                <script src="../Content/js/jquery-ui.js"></script>
                <script src="../Content/js/bootstrap.min.js"></script>
                <script src="../Content/js/app.js"></script>
                <script src="../Content/js/index.js"></script>
                </div>
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
                <script>
                    SeriesInit('#<%=ddlSeriesType.ClientID%>');
                    function SeriesInit(seriesType) {
                        $('#pnlTextSeries').addClass('hidden');
                        $('#pnlddlCashCredit').addClass('hidden');

                        var Desc = "";
                        $('#divDefault').removeClass('hidden');
                        $('#divDefaultf').removeClass('hidden');
                        if ($(seriesType).val() == "0") {
                            $('#divDefault').addClass('hidden');
                            $('#divDefaultf').addClass('hidden');
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
                    function Validation() {
                        return true;
                    }
                </script>
                <asp:Panel runat="server" ID="pnlReportFormatPdf" CssClass="reportPopUp" Visible="false" TabIndex="-1">
                    <div class="bodyPop">
                        <asp:Button ID="btnClose" Text="&times;" OnClick="btnClose_Click" CssClass="btn btn-danger" runat="server" Style="position: absolute; padding: 0; border-radius: 13%; height: 30px; width: 30px; font-size: 20px; right: 0; top: 0;" />
                        <asp:Literal ID="ltEmbed" runat="server" />
                    </div>
                </asp:Panel>
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
                        position: relative;
                    }

                    .reportPopUp {
                        width: 100%;
                        padding: 0px;
                        padding-top: 60px;
                        background-color: rgba(0, 0, 0, 0.63);
                        position: fixed;
                        z-index: 1940;
                        top: 0;
                        left: 0;
                        height: 100%;
                    }
                </style>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUploadCompanyLogo" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
