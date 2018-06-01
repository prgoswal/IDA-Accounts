<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDemoProfileCreation.aspx.cs" Inherits="frmProfileCreation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accounting System :: Login</title>
    <link rel="shortcut icon" href="Content/css/images/logo_single_3v1_icon.ico" type="image/x-icon" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <link href="Content/css/style.css" rel="stylesheet" />
    <link href="Content/css/custom.css" rel="stylesheet" />
    <style>
        legend {
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

        /*input[type="text"]::placeholder {
            color: black !important;
        }*/

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

            function PopOverError(id, plac, msg) {
                try {
                    $(id).popover({
                        title: 'Error!',
                        trigger: 'manual',
                        placement: plac,
                        content: function () {
                            var message = msg; //"Allow Numbers Only!";
                            return message;
                        }
                    });
                    $(id).popover("show");
                } catch (e) { }
            }


            $('.GSTINNO').blur(function (e) {
                var id = ('#' + this.id);
                var val = $(id).val();
                if (val.length >= 1) {
                    if (val.length < 15) {
                        PopOverError(id, 'top', 'Invalid GSTIN.');
                        event.preventDefault();
                        $(id).focus();
                        return false;
                    } else {
                        $(id).popover('destroy');
                        return true;
                    }
                }
            });

            $('.GSTINNO').keypress(function (e) {

                try {
                    var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
                    var id = ('#' + this.id);

                    $(id).popover('destroy');
                    if ($(id).val().length < 2) {

                        if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                            PopOverError(id, 'top', 'Allow Numbers Only!');
                            //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                            return false;
                        }
                    }
                    else if ($(id).val().length < 7) {

                        if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                            $(id).popover('destroy');
                        } else {
                            PopOverError(id, 'top', 'Allow Characters Only!');
                            //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                            return false;
                        }
                    }
                    else if ($(id).val().length < 11) {

                        if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                            PopOverError(id, 'top', 'Allow Numbers Only!');
                            //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                            return false;
                        }
                    }
                    else if ($(id).val().length < 12) {

                        if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                            $(id).popover('destroy');
                        } else {
                            PopOverError(id, 'top', 'Allow Characters Only!');
                            //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                            return false;
                        }
                    }

                    if ($(id).val().length == 15) {
                        PopOverError(id, 'top', 'Allow 15 Characters Only!');
                        return false;
                    }
                    else {
                    }

                } catch (e) {

                }
            });

            //$('.PanNo').blur(function (e) {
            //    var id = ('#' + this.id);
            //    var val = $(id).val();
            //    if (val.length >= 1) {
            //        if (val.length < 10) {
            //            PopOverError(id, 'top', 'Invalid Pan No.');
            //            event.preventDefault();
            //            $(id).focus();
            //            return false;
            //        } else {
            //            $(id).popover('destroy');
            //            return true;
            //        }
            //    }
            //});

            //$('.PanNo').keypress(function (e) {
            //    try {
            //        var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            //        var id = ('#' + this.id);
            //        $(id).popover('destroy');

            //        if ($(id).val().length < 5) {

            //            if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123)) {
            //                $(id).popover('destroy');
            //            } else {
            //                PopOverError(id, 'top', 'Allow Characters Only!');
            //                return false;
            //            }
            //        }
            //        else if ($(id).val().length < 9) {

            //            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
            //                PopOverError(id, 'top', 'Allow Numbers Only!');
            //                return false;
            //            }
            //        }
            //        else if ($(id).val().length < 10) {

            //            if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
            //                $(id).popover('destroy');
            //            } else {
            //                PopOverError(id, 'top', 'Allow Characters Only!');
            //                return false;
            //            }
            //        }

            //        if ($(id).val().length == 10) {
            //            PopOverError(id, 'top', 'Allow 10 Characters Only!');
            //            return false;
            //        }
            //        else {
            //        }

            //    } catch (e) {

            //    }
            //});

            $('.Extra12').keypress(function (e) {

                try {
                    var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
                    var id = ('#' + this.id);
                    $(id).popover('destroy');
                    if ((chCode > 47 && chCode < 58) || (chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode === 46 || chCode == 32) {

                        if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                            $(id).popover('destroy');
                            return;
                        }

                        var txt = $(id).val();
                    } else {
                        PopOverError(id, 'top', 'Special Character Not Allowed!');
                        event.preventDefault();
                    }
                } catch (e) {

                }

            });
            $('.Extra12').blur(function (e) {
                var id = ('#' + this.id);
                $(id).popover('destroy');
            });

            //// For Numeri Value Only \\\\\\
            $('.numberonly').keypress(function (event) {
                
                try {
                    var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
                    var id = ('#' + this.id);
                    if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                        PopOverError(id, 'top', 'Allow Numbers Only!');
                        return false;
                    }
                    else {
                        $(id).popover('destroy');
                        return true;
                    }
                } catch (e) {

                }
            });


            $('.numberonly').blur(function (e) {
                try {
                    var id = ('#' + this.id);
                    $(id).popover('destroy');
                } catch (e) { }
            });

            //// For Valid Date Allow \\\\\\
            $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });

            $('.datepicker').blur(function (e) {
                try {

                    var id = ('#' + this.id);
                    var date = $(id).val();
                    $(id).popover('destroy');
                    var valid = true;
                    if (date.length <= 0 || date == '' || date == undefined) {
                        return false;
                    }
                    if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                        valid = true;
                    } else {
                        valid = false;
                    }

                    if (valid) {
                        $(id).popover('destroy');
                    } else {
                        PopOverError(id, 'top', 'Invalid Date.');
                    }
                } catch (e) { }
            });
            $('.datepicker').keypress(function (e) {
                try {
                    $(id).popover('destroy');
                    var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
                    var id = ('#' + this.id);
                    if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                        PopOverError(id, 'top', 'Enter Valid Key For Date.');
                        return false; //Non Numeric Value Return Directly;
                    }
                    else {
                        if ($(id).val() === undefined) {
                            event.preventDefault();
                            return;
                        }
                        if (e.key == "/") {
                            PopOverError(id, 'top', 'This Key Is Invalid!');
                            event.preventDefault();
                            return false;
                        }
                        if (e.keyCode != 8) {

                            var DateVal = $(id).val();
                            if (e.keyCode == 191) {
                                var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                                PopOverError(id, 'top', 'Enter Valid Date!');
                                $(id).val(corr);
                                event.preventDefault();
                                return false;
                            }

                            if ($(id).val().length == 2) {
                                if ($(id).val() < 1 || $(id).val() > 31) {
                                    $(id).val("")
                                    PopOverError(id, 'top', 'Enter Valid Day!');
                                    event.preventDefault();
                                    return false;
                                }
                                $(id).val($(id).val() + "/");
                            } else if ($(id).val().length == 5) {
                                var month = $(id).val().substring(3, 6);
                                if (month < 1 || month > 12) {
                                    var corr = $(id).val().replace("/" + month, "");
                                    $(id).val(corr);
                                    PopOverError(id, 'top', 'Enter Valid Month!');
                                    event.preventDefault();
                                    return false;
                                }
                                $(id).val($(id).val() + "/");
                            } else if ($(id).val().length == 10) {
                                var Inputyear = $(id).val().substring(6, 11);
                                var NowYear = new Date().getUTCFullYear();
                                if (Inputyear < 1900 || Inputyear > NowYear) {
                                    var corr = $(id).val().replace(Inputyear, "");
                                    $(id).val(corr);
                                    PopOverError(id, 'top', 'Enter Valid Year!');
                                    event.preventDefault();
                                    return false;
                                }
                            }
                            else { $(id).popover('destroy'); }
                        }
                    }
                } catch (e) { }
            });

            //// For Amount upto 2 decimal Place \\\
            $('.Money').blur(function (e) {
                try {
                    var id = ('#' + this.id);
                    $(id).popover('destroy');
                } catch (e) { }
            });
            $('.Money').keypress(function (e) {
                try {
                    var id = ('#' + this.id);
                    var val = $(id).val();
                    var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
                    var id = ('#' + this.id);
                    $(id).popover('destroy');

                    if (chCode != 46) {
                        if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                            PopOverError(id, 'top', 'Enter Valid Amount');
                            return false; //Non Numeric Value Return Directly;
                        }
                    }

                    if (val.indexOf(".") != -1 && val.indexOf(".") + 3 == val.length && event.keyCode != 8 && event.keyCode != 17 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 9) {
                        PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                        event.preventDefault();
                        return false;
                    }
                    if (event.keyCode == 46 && val == "") {
                        PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                        event.preventDefault();
                        return false;
                    }
                    if (val.split(".").length > 1 && event.keyCode == 46) {
                        PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                        event.preventDefault();
                        return false;
                    }
                } catch (e) {

                }
            });

            //// Disable Pasting IN Text Box \\\\
            $('input.numberonly, input.datepicker, input.Money').bind('copy paste', function (e) {
                e.preventDefault();
            });

            $("input").attr("autocomplete", "off");
            $(".datepicker").attr("autocomplete", "off");
            $(".Money").attr("autocomplete", "off");

        }
    </script>
    <script type="text/javascript">
        function RequiredValidation() {

            //------------------------------------------------------ Start Basic Info Validation ------------------------------------------------------//

            //------------------For Company Name -------------------//

            if ($('#<%=txtCompanyName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Company Name.');
                $('#<%=txtCompanyName.ClientID%>').focus();
                return false;
            }

            //------------------For Org Type -------------------//

            if ($('#<%=ddlOrgType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select Org Type.');
                $('#<%=ddlOrgType.ClientID%>').focus();
                return false;
            }

            //------------------For Business Nature -------------------//

            if ($('#<%=ddlBussiNature.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select Business Nature.');
                $('#<%=ddlBussiNature.ClientID%>').focus();
                return false;
            }

            //------------------For Business Type -------------------//

            if ($('#<%=ddlBussiType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select Business Type.');
                $('#<%=ddlBussiType.ClientID%>').focus();
                return false;
            }

            //------------------For Address -------------------//

            if ($('#<%=txtAddress.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Address.');
                $('#<%=txtAddress.ClientID%>').focus();
                return false;
            }

            //------------------For City -------------------//

            if ($('#<%=txtCityCompany.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter City.');
                $('#<%=txtCityCompany.ClientID%>').focus();
                return false;
            }

            //------------------For Company State -------------------//

            if ($('#<%=ddlStateCompany.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select State.');
                $('#<%=ddlStateCompany.ClientID%>').focus();
                return false;
            }

            //------------------For Company Pin Code -------------------//


            if ($('#<%=txtPincodeCompany.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Pincode.');
                $('#<%=txtPincodeCompany.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtPincodeCompany.ClientID%>').val() != '' && $('#<%=txtPincodeCompany.ClientID%>').val().length != 6) {
                $('#<%=lblMsg.ClientID%>').html('Please Enter 6 Digit Company Pin Code.');
                $('#<%=txtPincodeCompany.ClientID%>').focus();
                return false;
            }

            //------------------For Pan No -------------------//

         <%--   if ($('#<%=txtPanNo.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Pan No.');
                $('#<%=txtPanNo.ClientID%>').focus();
                return false;
            }--%>

            //------------------For Landline number -------------------//

         <%--   if ($('#<%=txtLandLineNo.ClientID%>').val() != '' && $('#<%=txtLandLineNo.ClientID%>').val().length != 11) {
                $('#<%=lblMsg.ClientID%>').html('Please Enter 11 Digit Landline No.');
                $('#<%=txtLandLineNo.ClientID%>').focus();
                return false;
            }--%>

            //------------------------------------------------------ End Basic Info Validation ------------------------------------------------------//


            //------------------------------------------------------ Start GST Info Validation ------------------------------------------------------//

            //------------------For GSTIN No -------------------//

           <%-- if ($('#<%=txtGSTIN.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter GSTIN No.');
                $('#<%=txtGSTIN.ClientID%>').focus();
                return false;
            }

            //------------------For Registration Address -------------------//

            if ($('#<%=txtRegAddress.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Registration Address.');
                $('#<%=txtRegAddress.ClientID%>').focus();
                return false;
            }

            //------------------For Registration Date -------------------//

            if ($('#<%=txtRegDate.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Registration Date.');
                $('#<%=txtRegDate.ClientID%>').focus();
                return false;
            }

            //------------------For GST Info City -------------------//

            if ($('#<%=txtCityGSTIN.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter GST Info City.');
                $('#<%=txtCityGSTIN.ClientID%>').focus();
                return false;
            }

            //------------------For GST Info State -------------------//

            if ($('#<%=ddlStateGSTIN.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select GST Info State.');
                $('#<%=ddlStateGSTIN.ClientID%>').focus();
                return false;
            }

            //------------------For GST Info PinCode -------------------//

            if ($('#<%=txtPincodeGSTIN.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter GST Info Pincode.');
                $('#<%=txtPincodeGSTIN.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtPincodeGSTIN.ClientID%>').val() != '' && $('#<%=txtPincodeGSTIN.ClientID%>').val().length != 6) {
                $('#<%=lblMsg.ClientID%>').html('Please Enter 6 Digit GST Info Pin Code.');
                $('#<%=txtPincodeGSTIN.ClientID%>').focus();
                return false;
            }

            //------------------For GST Info Authorized Signatory -------------------//

            if ($('#<%=txtAuthorizedSign.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Authorized Signatory.');
                $('#<%=txtAuthorizedSign.ClientID%>').focus();
                return false;
            }

            //------------------For GST Info Signatory Designation -------------------//

            if ($('#<%=txtAuthorizedDesi.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Signatory Designation.');
                $('#<%=txtAuthorizedDesi.ClientID%>').focus();
                return false;
            }--%>

            //------------------------------------------------------ End GST Info Validation ------------------------------------------------------//


            //--------------------------------------------------- Start Contact Person Validation ---------------------------------------------------//



            //------------------For Contact Person Mobile number -------------------//

          <%--  if ($('#<%=txtMobileNoPerson.ClientID%>').val() != '' && $('#<%=txtMobileNoPerson.ClientID%>').val().length != 10) {
                $('#<%=lblMsg.ClientID%>').html('Please Enter 10 Digit Contact Person Mobile No.');
                $('#<%=txtMobileNoPerson.ClientID%>').focus();
                return false;
            }--%>

            //--------------------------------------------------- End Contact Person Validation ---------------------------------------------------//


            //------------------For No. Of Copy To Be Printed -------------------//

            <%--if ($('#<%=txtNoPrintedCopy.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter No. Of Copy To Be Printed.');
                $('#<%=txtNoPrintedCopy.ClientID%>').focus();
                return false;
            }

            //------------------For Copy Type -------------------//

            if ($('#<%=ddlCopyType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select Copy Type.');
                $('#<%=ddlCopyType.ClientID%>').focus();
                return false;
            }

            //------------------For Composition Effective Date -------------------//

            if ($('#<%=txtCompoEffDate.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Composition Effective Date.');
                $('#<%=txtCompoEffDate.ClientID%>').focus();
                return false;
            }--%>
        }
    </script>
    <script type="text/javascript">
        function ValidPanNo() {

            var panNo = document.getElementById('<%=txtPanNo.ClientID%>').value;
            if (panNo != "") {
                var regex = /^[A-Z]{5}\d{4}[A-Z]{1}$/;  //this is the pattern of regular expersion
                if (regex.test(panNo) == false) {
                    $('#<%=lblMsg.ClientID%>').html('Please enter valid pan number for ex. (ABCDE4512A)');
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
                                <img src="Content/css/images/logo.png" alt="Image" class="img-rounded img-responsive logo-oswal" style="height: 60px; width: 100px" />
                            </div>
                        </div>
                        <div class="col-sm-6 text-white text-center">

                            <h2 class="product_name">Set Company Profile</h2>
                        </div>
                    </nav>

                    <div class="container-fluid">
                        <div class="wpr">
                            <div class="content-wrapper">
                                <%--<h3 class="text-center p5" style="padding: 5px; margin-bottom: 5px;">Company Profile<span class="invoiceHead"><asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span></h3>--%>

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
                                                    <asp:TextBox ID="txtLandLineNo" Enabled="false" CssClass="form-control" placeholder="Landline No." MaxLength="11" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtFaxNo" Enabled="false" CssClass="form-control" placeholder="Fax No." runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtEmail" Enabled="false" CssClass="form-control" placeholder="Email" runat="server" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtPanNo" Enabled="false"  MaxLength="10" CssClass="form-control PanNo" placeholder="Pan No." runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtTanNo" Enabled="false" CssClass="form-control" placeholder="Tan No." MaxLength="10" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCINNo" Enabled="false" CssClass="form-control" placeholder="CIN No." MaxLength="21" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtImportExportCode" Enabled="false" CssClass="form-control " placeholder="Import/Export Code" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <legend>GST Information<span class="pull-right">
                                            <asp:CheckBox ID="chkSameAsAbove" Text="Same As Above" AutoPostBack="true" OnCheckedChanged="chkSameAsAbove_CheckedChanged" runat="server" /></span></legend>
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtGSTIN" CssClass="form-control GSTINNO text-uppercase" placeholder="GSTIN No." MaxLength="15" runat="server" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtRegAddress" CssClass="form-control" placeholder="Registration Address" MaxLength="130" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtRegDate" CssClass="form-control datepicker" placeholder="Registration Date" MaxLength="10" runat="server" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtCityGSTIN" CssClass="form-control Alphaonly" placeholder="City" MaxLength="25" runat="server" />
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlStateGSTIN" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtPincodeGSTIN" CssClass="form-control numberonly" placeholder="Pincode" MaxLength="6" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtAuthorizedSign" CssClass="form-control" MaxLength="45" placeholder="Authorized Signatory" runat="server" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtAuthorizedDesi" CssClass="form-control" MaxLength="30" placeholder="Signatory Designation" runat="server" />
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
                                                <label class="col-sm-4">Inventory Management</label>
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlInventoryManag" Enabled="false" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0" Text="No" />
                                                        <asp:ListItem Value="1" Text="Yes" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4">Sales Invoice Series Available</label>
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlSIServiceAvailable"  Enabled="false" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSIServiceAvailable_SelectedIndexChanged" runat="server">
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
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4">Sales Invoice On PrePrinted Stationary</label>
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlSIOnPrePrinted" Enabled="false" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSIOnPrePrinted_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="0" Text="No" />
                                                        <asp:ListItem Value="1" Text="Yes" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3">
                                                    <button id="btnCopyTypeToggle" runat="server" disabled="disabled" class="form-control input-sm cb-button" type="button">Customer Copy<i class="caret pull-right" ></i></button>
                                                    <div id="divdrop" class="collapse cb-div">
                                                        <div>
                                                            <%--<asp:CheckBox Checked="false" ID="cbSelectAllCopy" AutoPostBack="true" Text="Select All" onclick="Closedrop(this);" OnCheckedChanged="cbSelectAllCopy_CheckedChanged" runat="server" />--%>
                                                            <asp:CheckBoxList onclick="Closedrop(this);"  CssClass="cb-list" ID="cbCopyType" AutoPostBack="true" OnSelectedIndexChanged="cbCopyType_SelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                                        </div>
                                                    </div>

                                                    <%--<button id="btnCopyTypeToggle" runat="server" disabled="disabled" data-toggle="collapse" data-target="#divdrop" class="form-control input-sm" type="button">Copy Type</button>
                                                    <div id="divdrop" class="collapse pos-abs">
                                                        <div>
                                                            <asp:CheckBoxList ID="cbCopyType" AutoPostBack="true" OnSelectedIndexChanged="cbCopyType_SelectedIndexChanged" Enabled="false" runat="server"></asp:CheckBoxList>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                
                                                <div class="col-sm-1 pr-0">
                                                    <asp:TextBox ID="txtExtra1" CssClass="form-control Extra12" MaxLength="30" Enabled="false" placeholder="Extra1" runat="server" />
                                                </div>
                                                <div class="col-sm-1 pr-0">
                                                    <asp:TextBox ID="txtExtra2" CssClass="form-control Extra12" MaxLength="30" Enabled="false" placeholder="Extra2" runat="server" />
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:TextBox ID="txtNoPrintedCopy" Text="1" Enabled="false" CssClass="form-control" placeholder="No. of Copies To be Printed" runat="server" />
                                                </div>
                                                    
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4">Composition Opted</label>
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlComposiOpted"  Enabled="false" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlComposiOpted_SelectedIndexChanged" placeholder="Person Name" runat="server">
                                                        <asp:ListItem Value="0" Text="No" />
                                                        <asp:ListItem Value="1" Text="Yes" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtCompoEffDate" Enabled="false" CssClass="form-control datepicker" placeholder="Composition Effective Date" MaxLength="10" runat="server" />
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
                                                        <span style="color: red; font-weight: normal; font-size: 12px"></span></label>
                                                    <div class="col-sm-9">
                                                        <asp:FileUpload ID="fuCompanyLogo" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="col-sm-3">
                                                    <asp:Image ID="imgCompanyLogo" CssClass="img-responsive" runat="server" />
                                                    <asp:Label ID="lblImageName" runat="server" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:Button ID="btnUploadCompanyLogo" Enabled="false" Text="Upload" CssClass="btn btn-primary btn-space-right" OnClick="btnUploadCompanyLogo_Click" runat="server" />
                                                </div>
                                                <br />
                                            </div>
                                            <div class="col-sm-6" style="padding-right: 0">
                                                <legend>Additional Information To Be Print On Invoice</legend>
                                                <div class="form-group">
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtInvoiceCaption1" Enabled="false"  CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlInvoicePrint1" Enabled="false"  CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Print No" Value="0" />
                                                            <asp:ListItem Text="Print Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtInvoiceCaption2" Enabled="false"  CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlInvoicePrint2" Enabled="false"  CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Print No" Value="0" />
                                                            <asp:ListItem Text="Print Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtInvoiceCaption3" Enabled="false"  CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlInvoicePrint3" Enabled="false"  CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Print No" Value="0" />
                                                            <asp:ListItem Text="Print Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtInvoiceCaption4" Enabled="false"  CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlInvoicePrint4" Enabled="false"  CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Print No" Value="0" />
                                                            <asp:ListItem Text="Print Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtInvoiceCaption5" Enabled="false"  CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="ddlInvoicePrint5" Enabled="false"  CssClass="form-control" runat="server">
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
                                                            <asp:BoundField ItemStyle-CssClass="" DataField="Terms" ItemStyle-Width="75%" />
                                                            <asp:TemplateField ItemStyle-CssClass="text-center" ItemStyle-Width="25%">
                                                                <ItemTemplate>
                                                                    <asp:Button CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex  %>' CssClass="btn btn-danger" Text="Del" Style="padding: 2px 8px" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
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
                                                                                    <asp:CheckBox ID="chkSelectOnce" Enabled="false" runat="server" />
                                                                                    <asp:Label ID="lblFormatID" Text='<%#Eval("FormatID") %>' Visible="false" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField ItemStyle-CssClass="hidden" DataField="FormatID" />
                                                                            <asp:BoundField ItemStyle-CssClass="" DataField="FormatName" />
                                                                            <asp:TemplateField ItemStyle-CssClass=" text-center">
                                                                                <ItemTemplate>
                                                                                    <asp:Button CommandName="View" Enabled="false" CommandArgument='<%#Eval("FormatPath") %>' target="_blank" CssClass="btn btn-xs btn-primary btn-sxs" Text="View" runat="server" />
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
                                                    <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary btn-space-right" OnClick="btnSave_Click" OnClientClick="return RequiredValidation()" runat="server" />
                                                    <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger btn-space-right" OnClick="btnClear_Click" runat="server" />
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
                    <script src="Content/js/jquery.min.js"></script>
                    <script src="Content/js/jquery-ui.js"></script>
                    <script src="Content/js/bootstrap.min.js"></script>
                    <script src="Content/js/app.js"></script>
                    <script src="Content/js/index.js"></script>
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
                        z-index: 1040;
                         position: relative
                    }

                    .objPop {
                        width: 100%;
                        height: 100%;
                    }

                    .reportPopUp {
                        width: 100%;
                        top: 0px;
                        bottom: 0;
                        padding-top: 20px;
                        background-color: rgba(0, 0, 0, 0.63);
                        position: fixed;
                        z-index: 1940;
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
