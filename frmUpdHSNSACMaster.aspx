<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUpdHSNSACMaster.aspx.cs" Inherits="frmUpdHSNSACMaster" %>

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
    <style type="text/css">
        .h25 {
            height: 25px !important;
        }

        .p5 {
        }

        td.r {
            text-align: right;
        }

        b.panelh {
            background-color: #1c75bf;
            color: #fff;
            display: inline-block;
            width: 100%;
            padding-left: 10px;
        }

        .grid_head {
            background-color: #e8f4ff;
            color: #000;
        }

        .client_info_table1 input, .client_info_table1 select {
            border: 1px solid black !important;
            padding-left: 5px !important;
            border-radius: 2px;
        }

        td {
            padding: 1px;
            padding-top: 4px;
        }

        table, td {
            border: 0;
        }

        .client_info_table1_7 td {
            border: 1px solid #999;
        }

        .client_info_table1_8 td {
            border: 1px solid #999;
        }

        .client_info_table1_col1 {
            width: 55%;
            vertical-align: top;
        }

        .client_info_table1_col2 {
            vertical-align: top;
            width: 45%;
        }

        .client_info_table1_1 {
            width: 100%;
        }

        .client_info_table1_1_col1,
        .client_info_table1_1_col3,
        .client_info_table1_1_col5 {
            width: 15%;
        }

        .client_info_table1_1_col2,
        .client_info_table1_1_col4,
        .client_info_table1_1_col6 {
            width: 18%;
        }

        .client_info_table1_3 {
            width: 100%;
        }

        .client_info_table1_3_col1 {
            width: 16%;
        }

        .client_info_table1_3_col2 {
            width: 16%;
        }

        .client_info_table1_3_col3 {
            width: 9%;
        }

        .client_info_table1_3_col4 {
            width: 18%;
        }

        .client_info_table1_3_col5 {
            width: 8%;
        }

        .client_info_table1_3_col6 {
            width: 29%;
        }

        .client_info_table1_4 {
            width: 100%;
        }

        .client_info_table1_4_col1,
        .client_info_table1_4_col3 {
            width: 20%;
        }

        .client_info_table1_4_col2,
        .client_info_table1_4_col4 {
            width: 30%;
        }

        .client_info_table1_5 {
            width: 100%;
        }

        .client_info_table1_5_col1 {
            width: 20%;
        }

        .client_info_table1_5_col2 {
            width: 12%;
        }

        .client_info_table1_5_col3 {
            width: 16%;
        }

        .client_info_table1_5_col4 {
            width: 16%;
        }

        .client_info_table1_5_col5 {
            width: 16%;
        }

        .client_info_table1_5_col6 {
            width: 16%;
        }

        .client_info_table1_6 {
            width: 100%;
        }

        .client_info_table1_6_col1 {
            width: 20%;
        }

        .client_info_table1_6_col2 {
            width: 12%;
        }

        .client_info_table1_6_col3 {
            width: 16%;
        }

        .client_info_table1_6_col4 {
            width: 16%;
        }

        .client_info_table1_6_col5 {
            width: 16%;
        }

        .client_info_table1_6_col6 {
            width: 16%;
        }

        .client_info_table1_7 {
            width: 100%;
        }

        .client_info_table1_7_col1 {
            width: 16%;
        }

        .client_info_table1_7_col2 {
            width: 14%;
        }

        .client_info_table1_7_col3 {
            width: 26%;
        }

        .client_info_table1_7_col4 {
            width: 16%;
        }

        .client_info_table1_7_col5 {
            width: 16%;
        }

        .client_info_table1_7_col6 {
            width: 08%;
        }


        .client_info_table1_8 {
            width: 100%;
        }

        .client_info_table1_8_col1 {
            width: 20%;
        }

        .client_info_table1_8_col2 {
            width: 30%;
        }

        .client_info_table1_8_col3 {
            width: 20%;
        }

        .client_info_table1_8_col4 {
            width: 20%;
        }

        .client_info_table1_8_col5 {
            width: 10%;
        }

        .label_gst_align {
            margin-bottom: -2px;
            cursor: pointer;
            -webkit-touch-callout: none; /* iOS Safari */
            -webkit-user-select: none; /* Safari */
            -khtml-user-select: none; /* Konqueror HTML */
            -moz-user-select: none; /* Firefox */
            -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
        }

        input[type=checkbox]:checked + span, input[type=radio]:checked + span {
            border-color: transparent;
            background-color: transparent;
        }

        .align_checkbox {
            float: left;
        }

        .align_label {
            display: block;
            margin-top: 0px;
            float: left;
        }

        .disable_gstin_not_available_container {
            position: relative;
        }

        .disable_gstin_not_available_layer {
            z-index: 999999999999999999999 !important;
            display: none;
            background: rgba(255, 255, 255, 0.72);
            position: absolute;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
        }

            .disable_gstin_not_available_layer.active {
                display: block;
            }

        .pointer_none {
            pointer-events: none;
            cursor: not-allowed;
            -webkit-touch-callout: none; /* iOS Safari */
            -webkit-user-select: none; /* Safari */
            -khtml-user-select: none; /* Konqueror HTML */
            -moz-user-select: none; /* Firefox */
            -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                  supported by Chrome and Opera */
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

            $('.GSTIN').blur(function (e) {
                var id = ('#' + this.id);
                var val = $(id).val();
                if (val.length >= 1) {
                    if (val.length < 16) {
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

            $('.GSTIN').keypress(function (e) {

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

                    if ($(id).val().length == 16) {
                        PopOverError(id, 'top', 'Allow 16 Characters Only!');
                        return false;
                    }
                    else {
                    }

                } catch (e) {

                }
            });

            $('.PanNo').blur(function (e) {
                var id = ('#' + this.id);
                var val = $(id).val();
                if (val.length >= 1) {
                    if (val.length < 10) {
                        PopOverError(id, 'top', 'Invalid Pan No.');
                        event.preventDefault();
                        $(id).focus();
                        return false;
                    } else {
                        $(id).popover('destroy');
                        return true;
                    }
                }
            });

            $('.PanNo').keypress(function (e) {
                try {
                    var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
                    var id = ('#' + this.id);
                    $(id).popover('destroy');

                    if ($(id).val().length < 5) {

                        if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123)) {
                            $(id).popover('destroy');
                        } else {
                            PopOverError(id, 'top', 'Allow Characters Only!');
                            return false;
                        }
                    }
                    else if ($(id).val().length < 9) {

                        if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                            PopOverError(id, 'top', 'Allow Numbers Only!');
                            return false;
                        }
                    }
                    else if ($(id).val().length < 10) {

                        if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                            $(id).popover('destroy');
                        } else {
                            PopOverError(id, 'top', 'Allow Characters Only!');
                            return false;
                        }
                    }

                    if ($(id).val().length == 10) {
                        PopOverError(id, 'top', 'Allow 10 Characters Only!');
                        return false;
                    }
                    else {
                    }

                } catch (e) {

                }
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
        function ValidationSearch() {

            //------------------For HSN / SAC -------------------//

            if ($('#<%=ddlGoodsAndServices.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('Select HSN / SAC.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class='fa fa-info-circle fa-lg'></i>');
                $('#<%=ddlGoodsAndServices.ClientID%>').focus();
                return false;
            }

            //------------------For HSN / SAC Code -------------------//

            if ($('#<%=txtGoodsAndServicesCode.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter HSN / SAC Code.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').append('<i class='fa fa-info-circle fa-lg'></i>');
                $('#<%=txtGoodsAndServicesCode.ClientID%>').focus();
                return false;
            }
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

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                            <h2 class="product_name">HSN / SAC (For Update)</h2>
                        </div>
                    </nav>
                    <div class="container_fluid">
                        <div class="row">
                            <div class="col-sm-offset-3 col-sm-6">
                                <div class="panel panel-default" style="margin-top: 15px;margin-bottom:15px">
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group row">
                                                        <label class="col-sm-3">Goods / Services</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlGoodsAndServices" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGoodsAndServices_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Goods</asp:ListItem>
                                                                <asp:ListItem Value="2">Services</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div id="divddlMainGrp" class="form-group row" runat="server">
                                                        <label class="col-sm-3"><asp:Label ID="lblGoodsAndServicesCode" runat="server" Text="Goods Code"></asp:Label></label>
                                                        <div class="col-sm-9 input-group" style="padding-left: 16px; padding-right: 16px;">
                                                            <asp:TextBox ID="txtGoodsAndServicesCode" CssClass="form-control numberonly" placeholder="Goods Code" runat="server" />
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkGoodsAndServicesCodeSearch" CssClass="btn btn-primary" OnClick="lnkGoodsAndServicesCodeSearch_Click" OnClientClick="return ValidationSearch();" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div id="divddlsubgrp" class="form-group row" runat="server">
                                                        <label class="col-sm-3">Item Description</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox ID="txtItemDescription" TextMode="MultiLine" Rows="8" CssClass="form-control" placeholder="Item Description" MaxLength="400" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group row">
                                                        <label class="col-sm-3">Tax Rate</label>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox ID="txtTaxRate" MaxLength="5" CssClass="form-control Money" placeholder="%" AutoPostBack="true" OnTextChanged="txtTaxRate_TextChanged" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group row">
                                                        <div class="col-sm-3"></div>
                                                        <div class="col-sm-3">
                                                            IGST
                                                        </div>
                                                        <div class="col-sm-3">
                                                            CGST
                                                        </div>
                                                        <div class="col-sm-3">
                                                            SGST
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="form-group row">
                                                        <div class="col-sm-3"></div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtIGST" CssClass="form-control Money" Enabled="false" placeholder="IGST" runat="server" />
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtCGST" CssClass="form-control Money" Enabled="false" placeholder="CGST" runat="server" />
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtSGST" CssClass="form-control Money" Enabled="false" placeholder="SGST" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer">
                                        <div class="text-right">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <asp:Label ID="lblMsg" CssClass="col-sm-7 text-danger" runat="server"></asp:Label>
                                                        <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" Enabled="false" runat="server" />
                                                        <asp:Button ID="btnclear" CssClass="btn btn-danger" Text="Clear" OnClick="btnclear_Click" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <footer class="footer" style="position:static">
                        <span>© 2017 All rights reserved 2016-2017 Oswal Computers And Consultants Pvt. Ltd..</span>
                    </footer>
                    <script src="Content/js/jquery.min.js"></script>
                    <script src="Content/js/jquery-ui.js"></script>
                    <script src="Content/js/bootstrap.min.js"></script>
                    <script src="Content/js/app.js"></script>
                    <script src="Content/js/index.js"></script>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
