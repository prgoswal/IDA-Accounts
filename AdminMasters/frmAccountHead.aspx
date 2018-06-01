<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmAccountHead.aspx.cs" Inherits="frmAccountHead" %>

<%@ Register Assembly="TransliterateTextboxControl" Namespace="TransliterateTextboxControl" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            LoadBasic();
            ChoosenDDL();
            ddlExportCategory();
            $(document).ready(function () {
                $('#<%=ddlExportCategory.ClientID%>').change(function (e) {
                    ddlExportCategory();
                });
            })
        }
    </script>

    <script type="text/javascript">

        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                google.elements.transliteration.LanguageCode.HINDI,

                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };
            var control =
            new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['<%= txtAccountHeadHindi.ClientID%>']);
        }

        // here you make the first init when page load
        google.setOnLoadCallback(onLoad);

        // here we make the handlers for after the UpdatePanel update
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
        }

        // this is called to re-init the google after update panel updates.
        function EndRequest(sender, args) {
            onLoad();
        }
        // JavaScript funciton to call inside UpdatePanel  
        //function calldemo() {
        //    // for hindi text

        //    // Load the Google Transliterate API
        //    google.load("elements", "1", {
        //        packages: "transliteration"
        //    });

        //    function onLoad() {
        //        var options = {
        //            sourceLanguage:
        //            google.elements.transliteration.LanguageCode.ENGLISH,
        //            destinationLanguage:
        //            [google.elements.transliteration.LanguageCode.HINDI],
        //            shortcutKey: 'ctrl+e',
        //            transliterationEnabled: true
        //        };

        //        // Create an instance on TransliterationControl with the required
        //        // options.
        //        var control =
        //        new google.elements.transliteration.TransliterationControl(options);

        //        // Enable transliteration in the textbox with id
        //        // 'transliterateTextarea'.
        //        control.makeTransliteratable(['transliterateTextarea']);


        //    }
        //    google.setOnLoadCallback(onLoad);
        //    // end hindi


        //}
    </script>
    <script type="text/javascript">
        function Validation() {

            //------------------For Group Name -------------------//

            if ($('#<%=ddlGroupName.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Group Name.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlGroupName.ClientID%>').focus();
                return false;
            }

            //------------------For Account Head -------------------//

            if ($('#<%=txtAccountHead.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Account Head.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=txtAccountHead.ClientID%>').focus();
                return false;
            }

            //------------------For Phone number -------------------//

            if ($('#<%=txtPhone.ClientID%>').val() != '' && $('#<%=txtPhone.ClientID%>').val().length != 11) {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Please Enter 11 Digit Phone No.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=txtPhone.ClientID%>').focus();
                return false;
            }
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
                $('#<%=txtOpeningBalance.ClientID%>').focus();
            }
            else {
                $('#<%=lblMsg.ClientID%>').html('');
            }
        }

        function DisplayBlock() {
            $('#<%=tdContactInfoHeader.ClientID%>').css("display", "none");
            $('#<%=tdContactAndBankBody.ClientID%>').css("display", "none");
            $('#<%=tdShippingInfoHeader.ClientID%>').css("display", "none");
            $('#<%=tdShippingBody.ClientID%>').css("display", "none");
            $('#<%=tdAccountHeadHeader.ClientID%>').css("display", "");
            $('#<%=tdAccountHeadBody.ClientID%>').css("display", "");
        }
        function DisplayNone() {

            if ($('#<%=txtAccountHead.ClientID%>').val() != '') {
                $('#<%=tdAccountHeadHeader.ClientID%>').css("display", "none");
                $('#<%=tdAccountHeadBody.ClientID%>').css("display", "none");
                $('#<%=tdContactInfoHeader.ClientID%>').css("display", "");
                $('#<%=tdContactAndBankBody.ClientID%>').css("display", "");
                $('#<%=tdShippingInfoHeader.ClientID%>').css("display", "");
                $('#<%=tdShippingBody.ClientID%>').css("display", "");
            }
        }
    </script>
    <script type="text/javascript">
        function ToUpper(ctrl) {
            var panNo = ctrl.value;
            ctrl.value = panNo.toUpperCase();
        }
    </script>
    <script> //For ListBox FIlter
        function Filter() {
            debugger;
            var input, filter, listBox, option, a, i;
            input = document.getElementById("<%=txtAccountHead.ClientID%>");

                      filter = input.value.toUpperCase();

                      listBox = document.getElementById('<%=lstBoxAccountHead.ClientID %>');

            option = listBox.getElementsByTagName("option");

            for (i = 0; i < option.length; i++) {
                a = option[i];
                if (a.value.toUpperCase().indexOf(filter) > -1) {
                    option[i].style.display = "";
                } else {
                    option[i].style.display = "none";

                }
            }
        }
    </script>
    <script>
        function ddlExportCategory() {
            if ($('#<%=ddlExportCategory.ClientID%>').val() == 3) {
                $('#<%=ddlTaxCalType.ClientID%>').removeAttr('disabled');
            } else {
                $('#<%=ddlTaxCalType.ClientID%>').attr('disabled', 'disabled');
            }
        }
        $(document).ready(function () {
            $('#<%=ddlExportCategory.ClientID%>').change(function (e) {
                ddlExportCategory();
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upAccountHead" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(onLoad);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center p5" style="padding: 5px; margin-bottom: 5px;">Account Head
                    <span class="invoiceHead">
                        <asp:Label ID="lblInvoiceAndDate" Text="" runat="server" /></span>
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="table-responsive " style="padding: 5px;">
                                <table class="client_info_table1 table-bordered">
                                    <tr>
                                        <td class="client_info_table1_col1"><b class="panelh">Client Information:</b></td>


                                        <td id="tdContactInfoHeader" runat="server" class="client_info_table1_col2"><b class="panelh">Contact Information:</b></td>
                                        <td id="tdAccountHeadHeader" runat="server" class="client_info_table1_col2" style="display: none"><b class="panelh">Account Head:</b></td>
                                    </tr>
                                    <tr>
                                        <td class="client_info_table1_col1">
                                            <table class="client_info_table1_1 table-bordered">
                                                <tr>
                                                    <td class="client_info_table1_1_col1">Group Name<i class="text-danger">*</i></td>
                                                    <td colspan="5">
                                                        <%--<asp:DropDownList ID="ddlGroupName" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupName_SelectedIndexChanged" runat="server">
                                                             </asp:DropDownList>--%>
                                                              <cc1:ComboBox ID="ddlGroupName" runat="server" Enabled="true" OnSelectedIndexChanged="ddlGroupName_SelectedIndexChanged" AutoPostBack="true" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" />

                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class=" client_info_table1_1_col1"></td>
                                                    <td colspan="5">&nbsp;&nbsp;<asp:Label ID="lblGroupDescription" ForeColor="#27c24c" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_1_col1">Account Head<i class="text-danger">*</i></td>
                                                    <td colspan="5">
                                                        <asp:TextBox ID="txtAccountHead" onfocusin="DisplayBlock();Filter();" onfocusout="DisplayNone()" onkeyup="Filter()" placeholder="Account Head" CssClass="FilterAccountHead" MaxLength="95" runat="server" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="client_info_table1_1_col1">Account(Hindi)</td>
                                                    <td colspan="5">
                                                        <%--<asp:TextBox ID="txtAccountHeadHindi" onfocusin="DisplayBlock();Filter();" onfocusout="DisplayNone()" onkeyup="Filter()" placeholder="Account Head" CssClass="FilterAccountHead" MaxLength="45" runat="server" />--%>

                                                        <cc1:TransliterateTextbox MaxLength="185" ID="txtAccountHeadHindi" CssClass="FilterAccountHead" Text="" placeholder="Account Head(Hindi)"
                                                            runat="server" EnableKeyboard="false" KeyboardLayout="ENGLISH" DestinationLanguage="HINDI" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_1_col1">Address<i id="iAddress" runat="server"></i></td>
                                                    <td colspan="5">
                                                        <asp:TextBox ID="txtClientAddress" MaxLength="130" placeholder="Address" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_1_col1">City</td>
                                                    <td class="client_info_table1_1_col2">
                                                        <asp:TextBox ID="txtClientCity" placeholder="City" MaxLength="30" runat="server" />
                                                        <cc1:FilteredTextBoxExtender ID="ftbeClientCity" runat="server" FilterMode="ValidChars" TargetControlID="txtClientCity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td class="client_info_table1_1_col3 r">State</td>
                                                    <td class="client_info_table1_1_col4">
                                                        <asp:DropDownList ID="ddlClientState" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="client_info_table1_1_col5 r">Pin Code</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:TextBox ID="txtClientPincode" MaxLength="6" placeholder="Pin Code" CssClass="PinCode" runat="server" />
                                                        <cc1:FilteredTextBoxExtender ID="ftbeClientPinCode" runat="server" TargetControlID="txtClientPincode" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_1_col1">PAN No.</td>
                                                    <td class="client_info_table1_1_col2">
                                                        <asp:TextBox ID="txtPanNo" onfocusout="ValidPanNo()" onkeyup="ToUpper(this)" MaxLength="10" CssClass="PanNo" placeholder="PAN No." runat="server" />
                                                    </td>
                                                    <td class="client_info_table1_1_col3">Opening&nbsp;Balance</td>
                                                    <td class="client_info_table1_1_col4">
                                                        <asp:TextBox ID="txtOpeningBalance" placeholder="Opening Balance" MaxLength="10" CssClass="Money" runat="server" />
                                                    </td>
                                                    <td class="client_info_table1_1_col5 r">Dr/Cr</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:DropDownList ID="ddlDrCr" runat="server">
                                                            <asp:ListItem Text="Dr" Value="0" />
                                                            <asp:ListItem Text="Cr" Value="1" />
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <style>
                                                            .app_table {
                                                                width: 100%;
                                                            }

                                                            .app_col0 {
                                                                width: 06%;
                                                            }

                                                            .app_col1 {
                                                                width: 05%;
                                                            }

                                                            .app_col2 {
                                                                width: 10%;
                                                                font-size: 12px;
                                                            }

                                                            .app_col3 {
                                                                width: 05%;
                                                            }

                                                            .app_col4 {
                                                                width: 10%;
                                                                font-size: 12px;
                                                            }

                                                            .app_col5 {
                                                                width: 05%;
                                                            }

                                                            .app_col6 {
                                                                width: 10%;
                                                                font-size: 12px;
                                                            }

                                                            .app_col7 {
                                                                width: 05%;
                                                            }

                                                            .app_col8 {
                                                                width: 12%;
                                                                font-size: 12px;
                                                            }

                                                            .app_col9 {
                                                                width: 13%;
                                                            }

                                                            .app_col10 {
                                                                width: 19%;
                                                            }

                                                            .Bus-col {
                                                                width: 77%;
                                                            }
                                                        </style>
                                                        <table class="app_table table-bordered">
                                                            <tr>
                                                                <td class="app_col0"><b>Applicability:</b></td>
                                                                <td class="app_col1 r">TDS</td>
                                                                <td class="app_col2">
                                                                    <asp:DropDownList ID="ddlTDS" runat="server">
                                                                        <asp:ListItem Selected="True" Text="No" Value="0" />
                                                                        <asp:ListItem Text="Yes" Value="1" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="app_col3 r">ISD</td>
                                                                <td class="app_col4">
                                                                    <asp:DropDownList ID="ddlISD" runat="server">
                                                                        <asp:ListItem Selected="True" Text="No" Value="0" />
                                                                        <asp:ListItem Text="Yes" Value="1" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="app_col5 r">RCM</td>
                                                                <td class="app_col6">
                                                                    <asp:DropDownList Selected="True" ID="ddlRCM" runat="server">
                                                                        <asp:ListItem Text="No" Value="0" />
                                                                        <asp:ListItem Text="Yes" Value="1" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="app_col7 r">TCS</td>
                                                                <td class="app_col8">
                                                                    <asp:DropDownList ID="ddlTCS" AutoPostBack="true" OnSelectedIndexChanged="ddlTCS_SelectedIndexChanged" runat="server">
                                                                        <asp:ListItem Selected="True" Text="No" Value="0" />
                                                                        <asp:ListItem Text="Yes" Value="1" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="app_col9 r">Merchant ID</td>
                                                                <td class="app_col10">
                                                                    <asp:TextBox ID="txtMerchantID" placeholder="ID" Enabled="false" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_1_col1">Composition Opted</td>
                                                    <td class="client_info_table1_1_col2">
                                                        <asp:DropDownList ID="ddlCompositionOpted" runat="server">
                                                            <asp:ListItem Text="No" Value="0" />
                                                            <asp:ListItem Text="Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="client_info_table1_1_col3">Category</td>
                                                    <td class="client_info_table1_1_col4">
                                                        <asp:DropDownList ID="ddlExportCategory" CssClass="Bus-col" runat="server" />
                                                    </td>
                                                    <td class="client_info_table1_1_col5 r">Tax Calculation On Invoice</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:DropDownList ID="ddlTaxCalType" Enabled="false" runat="server">
                                                            <asp:ListItem Value="0" Text="No" />
                                                            <asp:ListItem Value="1" Text="Yes" />
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_1_col5">Other Details</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:TextBox ID="txtOtherDetails" MaxLength="45" placeholder="Other Details" runat="server" />
                                                    </td>
                                                </tr>


                                                <tr id="trBrokerage" runat="server" visible="false">

                                                    <td class="client_info_table1_1_col5">Brokerage Type</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:DropDownList ID="ddlBrokerageType" runat="server" OnSelectedIndexChanged="ddlBrokerageType_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="Brokerage Rate" Value="1" />
                                                            <asp:ListItem Text="Absolute Value" Value="2" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="client_info_table1_1_col5">Brokerage Rate</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:TextBox ID="txtBrokerRate" MaxLength="6" CssClass="Money" placeholder="Brokerage Rate" runat="server" />
                                                    </td>

                                                    <td class="client_info_table1_1_col5">Brokerage Limit</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:TextBox ID="txtBrokerageLimit" MaxLength="6" CssClass="Money" placeholder="Brokerage Limit" runat="server" />
                                                    </td>




                                                </tr>
                                                <tr style="display: none">
                                                    <td class="client_info_table1_1_col1">Sub Dealer</td>
                                                    <td class="client_info_table1_1_col2">
                                                        <asp:DropDownList ID="ddlSubDealer" Enabled="false" runat="server">
                                                            <asp:ListItem Text="No" Value="0" />
                                                            <asp:ListItem Text="Yes" Value="1" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="client_info_table1_1_col3">Discount Rate</td>
                                                    <td class="client_info_table1_1_col4">
                                                        <asp:TextBox ID="txtDiscountRate" Enabled="false" MaxLength="6" placeholder="Discount Rate" CssClass="Money" runat="server" />
                                                    </td>
                                                    <td class="client_info_table1_1_col5 r">Brokerage Rate</td>
                                                    <td class="client_info_table1_1_col6">
                                                        <asp:TextBox ID="txtBrokerageRate" Enabled="false" MaxLength="6" placeholder="Brokerage Rate" CssClass="Money" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdContactAndBankBody" runat="server" class="client_info_table1_col2">
                                            <table class="client_info_table1_2 table-bordered">
                                                <tr>
                                                    <td>
                                                        <table class="client_info_table1_3 table-bordered">
                                                            <tr>
                                                                <td class="client_info_table1_3_col1">Person&nbsp;Name</td>
                                                                <td colspan="5">
                                                                    <asp:TextBox ID="txtPersonName" placeholder="Person Name" MaxLength="45" runat="server" />
                                                                    <cc1:FilteredTextBoxExtender ID="ftbePersonName" runat="server" FilterMode="ValidChars" TargetControlID="txtPersonName" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="client_info_table1_3_col1">Mobile</td>
                                                                <td class="client_info_table1_3_col2">
                                                                    <asp:TextBox ID="txtMobileNo" placeholder="Mobile" MaxLength="10" CssClass="Mobile" runat="server" />
                                                                    <cc1:FilteredTextBoxExtender ID="ftbeMobileNo" runat="server" TargetControlID="txtMobileNo" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="client_info_table1_3_col3">Phone</td>
                                                                <td class="client_info_table1_3_col4">
                                                                    <asp:TextBox ID="txtPhone" placeholder="Phone" MaxLength="11" runat="server" />
                                                                    <cc1:FilteredTextBoxExtender ID="ftbePhone" runat="server" TargetControlID="txtPhone" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="client_info_table1_3_col5">Email</td>
                                                                <td class="client_info_table1_3_col6">
                                                                    <asp:TextBox ID="txtEmail" CssClass="Email" placeholder="Email" MaxLength="45" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="client_info_table1_3_col1">Remark</td>
                                                                <td colspan="5">
                                                                    <asp:TextBox ID="txtRemark" placeholder="Remark" MaxLength="90" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="client_info_table1_3_col1" colspan="6">&nbsp;</td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="border_px"><b class="panelh">Bank Information</b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table class="border_px client_info_table1_4 table-bordered">
                                                            <tr>
                                                                <td class="client_info_table1_4_col1">Bank</td>
                                                                <td class="client_info_table1_4_col2">
                                                                    <asp:TextBox ID="txtBank" placeholder="Bank" MaxLength="45" runat="server" />
                                                                </td>
                                                                <td class="client_info_table1_4_col3 r">Branch</td>
                                                                <td class="client_info_table1_4_col4">
                                                                    <asp:TextBox ID="txtBranch" placeholder="Branch" MaxLength="25" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="client_info_table1_4_col1">IFSC Code</td>
                                                                <td class="client_info_table1_4_col2">
                                                                    <asp:TextBox ID="txtIFSCCode" placeholder="IFSC Code" MaxLength="20" runat="server" />
                                                                </td>
                                                                <td class="client_info_table1_4_col3 r">A/C No.</td>
                                                                <td class="client_info_table1_4_col4">
                                                                    <asp:TextBox ID="txtAccountNo" placeholder="A/C No." MaxLength="16" CssClass="numberonly" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td colspan="4">Export Category
                                                                    <asp:DropDownList ID="ddlExportCategory" CssClass="Bus-col" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>--%>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="border_px"><b class="panelh">Terms & Conditions for the Party</b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table class="border_px client_info_table1_4 table-bordered">
                                                            <tr>
                                                                <td style="width: 10%">Terms</td>
                                                                <td style="width: 80%">
                                                                    <asp:TextBox ID="txtTerms" placeholder="Terms & Conditions for the Party" MaxLength="100" runat="server" />
                                                                </td>
                                                                <td style="width: 10%; text-align: center">
                                                                    <asp:Button ID="btnTermsAdd" runat="server" OnClick="btnTermsAdd_Click" Text="Add" CssClass="btn btn-primary btn-sxs "></asp:Button>
                                                                </td>
                                                            </tr>

                                                            <%--<tr>
                                                                <td colspan="4">Export Category
                                                                    <asp:DropDownList ID="ddlExportCategory" CssClass="Bus-col" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>--%>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <style>
                                                            .client_info_table1_4_th_hider tr th:not(.show_th) {
                                                                display: none;
                                                            }

                                                            .client_info_table1_4_th_hider tr td:first-child {
                                                                display: none;
                                                            }

                                                            .client_info_table1_4_th_hider tr td, .client_info_table1_4_th_hider tr th {
                                                                border: 1px solid #c1c1c1 !important;
                                                            }

                                                            .client_info_table1_7 tr td, .client_info_table1_7 tr th {
                                                                border: 1px solid #c1c1c1 !important;
                                                            }

                                                            .client_info_table1_8 tr td, .client_info_table1_8 tr th {
                                                                border: 1px solid #c1c1c1 !important;
                                                            }
                                                        </style>
                                                        <asp:GridView ID="grdTerms" runat="server" CssClass="client_info_table1_4 client_info_table1_4_th_hider table-bordered" AutoGenerateColumns="false" OnRowCommand="grdTerms_RowCommand">

                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <th colspan="100%" class="show_th">Terms and Conditions</th>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTerms" Text='<%#Eval("Terms") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Button CssClass="btn btn-danger btn-sxs" CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdAccountHeadBody" runat="server" class="client_info_table1_col1" style="display: none">
                                            <table class="client_info_table1_1 table-bordered">
                                                <tr>
                                                    <td colspan="6" style="position: relative">
                                                        <asp:ListBox ID="lstBoxAccountHead" runat="server" Height="208px" Style="color: #000; width: 100%; position: absolute; height: 388px; background: #9ccef9; top: 4px;"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="client_info_table1_col1">
                                            <b class="panelh">GSTIN Information
                                                <label for="gstinnotavailable" class="label_gst_align pull-right">
                                                    <asp:CheckBox ID="chkGSTINNotAvailable" AutoPostBack="true" OnCheckedChanged="chkGSTINNotAvailable_CheckedChanged" CssClass="align_checkbox" runat="server" />
                                                    <span class="align_label">GSTIN Not Available</span>
                                                </label>
                                            </b>
                                        </td>
                                        <td id="tdShippingInfoHeader" runat="server" class="client_info_table1_col2"><b class="panelh">Shipping Information</b></td>
                                    </tr>
                                    <tr>
                                        <td class="client_info_table1_col1 disable_gstin_not_available_container">
                                            <span class="disable_gstin_not_available_layer"></span>
                                            <table class="client_info_table1_5 table-bordered">
                                                <tr>
                                                    <td class="client_info_table1_5_col1">GSTIN</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtGSTIN" placeholder="GSTIN" MaxLength="15" CssClass="GSTIN text-uppercase" runat="server" />
                                                        <td class="client_info_table1_5_col4">Registration&nbsp;Date</td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtRegistrationDate" MaxLength="10" AutoPostBack="true" OnTextChanged="txtRegistrationDate_TextChanged" CssClass="datepicker" placeholder="DD/MM/YYYY" runat="server" />
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_5_col1">Registration Address</td>
                                                    <td colspan="5">
                                                        <asp:TextBox ID="txtRegistrationAddress" placeholder="Address" MaxLength="130" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_5_col1">City</td>
                                                    <td class="client_info_table1_5_col2">
                                                        <asp:TextBox ID="txtGSTINCity" placeholder="City" MaxLength="30" runat="server" />
                                                        <cc1:FilteredTextBoxExtender ID="ftbeGSTINCity" runat="server" FilterMode="ValidChars" TargetControlID="txtGSTINCity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td class="client_info_table1_5_col3 r">State</td>
                                                    <td class="client_info_table1_5_col4">
                                                        <asp:DropDownList ID="ddlGSTINState" runat="server">
                                                            <asp:ListItem Text="Select" Value="0" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="client_info_table1_5_col5 r">Pincode</td>
                                                    <td class="client_info_table1_5_col6">
                                                        <asp:TextBox ID="txtGSTINPinCode" MaxLength="6" placeholder="Pincode" CssClass="PinCode" runat="server" />
                                                        <cc1:FilteredTextBoxExtender ID="ftbeGSTINPinCode" runat="server" TargetControlID="txtGSTINPincode" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_5_col1 ">Authorized Signatory</td>
                                                    <td class="client_info_table1_5_col2 r">
                                                        <asp:TextBox ID="txtAuthorizedSignatory" placeholder="Signatory" runat="server" />
                                                    </td>
                                                    <td class="client_info_table1_5_col3 r">Designation</td>
                                                    <td class="client_info_table1_5_col4 r">
                                                        <asp:TextBox ID="txtDesignation" placeholder="Designation" runat="server" />
                                                    </td>
                                                    <td class=" client_info_table1_5_col4 text-right" colspan="2">
                                                        <asp:Button ID="btnAddGSTIN" Text="Add" CssClass="btn btn-primary btn-xs" OnClick="btnAddGSTIN_Click" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdShippingBody" runat="server" class="client_info_table1_col2">
                                            <table class="client_info_table1_6 table-bordered">
                                                <tr>
                                                    <td class="client_info_table1_6_col1">GSTIN</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddlShippingGSTIN" runat="server" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td colspan="3" class="r">
                                                        <label for="sameasgstin" class="label_gst_align pull-right">
                                                            <span class="align_label pull-right">Same As GSTIN Address</span>
                                                            <asp:CheckBox ID="chkSameAsGSTINAddress" AutoPostBack="true" OnCheckedChanged="chkSameAsGSTINAddress_CheckedChanged" class="align_checkbox pull-right" runat="server" />
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_6_col1">Shipping&nbsp;Address</td>
                                                    <td colspan="5">
                                                        <asp:TextBox ID="txtShippingAddress" placeholder="Address" MaxLength="130" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="client_info_table1_6_col1">City</td>
                                                    <td class="client_info_table1_6_col2">
                                                        <asp:TextBox ID="txtShippingCity" placeholder="City" MaxLength="30" runat="server" />
                                                        <cc1:FilteredTextBoxExtender ID="ftbeShippingCity" runat="server" FilterMode="ValidChars" TargetControlID="txtShippingCity" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ "></cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td class="client_info_table1_6_col3 r">State</td>
                                                    <td class="client_info_table1_6_col4">
                                                        <asp:DropDownList ID="ddlShippingState" runat="server">
                                                            <asp:ListItem Text="Select" Value="0" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="client_info_table1_6_col5 r">Pincode</td>
                                                    <td class="client_info_table1_6_col6">
                                                        <asp:TextBox ID="txtShippingPincode" MaxLength="6" placeholder="Pincode" CssClass="PinCode" runat="server" />
                                                        <cc1:FilteredTextBoxExtender ID="ftbeShippingPinCode" runat="server" TargetControlID="txtShippingPincode" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="text-right" colspan="6">
                                                        <asp:Button ID="btnAddShipping" Text="Add" CssClass="btn btn-primary btn-xs" OnClick="btnAddShipping_Click" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="client_info_table1_col1 disable_gstin_not_available_container">
                                            <span class="disable_gstin_not_available_layer"></span>
                                            <asp:GridView ID="grdGSTINInformation" AutoGenerateColumns="false" CssClass="client_info_table1_7 table-bordered" OnRowCommand="grdGSTINInformation_RowCommand" runat="server" Style="font-size: 12px">
                                                <Columns>
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col1" HeaderStyle-CssClass="grid_head" HeaderText="GSTIN" DataField="GSTIN" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col2" HeaderStyle-CssClass="grid_head" HeaderText="Date" DataField="RegistrationDate" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col3" HeaderStyle-CssClass="grid_head" HeaderText="Address" DataField="RegistrationAddress" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col4" HeaderStyle-CssClass="grid_head" HeaderText="City" DataField="City" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col5" HeaderStyle-CssClass="grid_head" HeaderText="State" DataField="State" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col6" HeaderStyle-CssClass="grid_head" HeaderText="Pincode" DataField="PinCode" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col5" HeaderStyle-CssClass="grid_head" HeaderText="Authorized Signatury" DataField="AuthorizedSignatury" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_7_col6" HeaderStyle-CssClass="grid_head" HeaderText="Designation" DataField="SignaturyDesignation" />

                                                    <asp:BoundField ControlStyle-CssClass="hidden" Visible="false" DataField="OrgID" />
                                                    <asp:BoundField ControlStyle-CssClass="hidden" Visible="false" DataField="BrID" />
                                                    <asp:BoundField ControlStyle-CssClass="hidden" Visible="false" DataField="YrCD" />
                                                    <asp:BoundField ControlStyle-CssClass="hidden" Visible="false" DataField="AccCode" />
                                                    <asp:BoundField ControlStyle-CssClass="hidden" Visible="false" DataField="StateID" />
                                                    <asp:TemplateField ControlStyle-CssClass="btn btn-danger btn-sxs" HeaderStyle-CssClass="grid_head">
                                                        <ItemTemplate>
                                                            <asp:Button CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <td class="client_info_table1_col2">
                                            <asp:GridView ID="grdShippingInformation" AutoGenerateColumns="false" CssClass="client_info_table1_8 table-bordered" OnRowCommand="grdShippingInformation_RowCommand" runat="server" Style="font-size: 12px">
                                                <Columns>
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col1" HeaderStyle-CssClass="grid_head" HeaderText="GSTIN" DataField="GSTIN" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col2" HeaderStyle-CssClass="grid_head" HeaderText="Shipping Address" DataField="ShippingAddress" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col3" HeaderStyle-CssClass="grid_head" HeaderText="City" DataField="City" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col4" HeaderStyle-CssClass="grid_head" HeaderText="State" DataField="State" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5" HeaderStyle-CssClass="grid_head" HeaderText="Pincode" DataField="PinCode" />

                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5 hidden" Visible="false" DataField="OrgID" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5 hidden" Visible="false" DataField="BrID" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5 hidden" Visible="false" DataField="YrCD" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5 hidden" Visible="false" DataField="AccCode" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5 hidden" Visible="false" DataField="StateID" />
                                                    <asp:BoundField ControlStyle-CssClass="client_info_table1_8_col5 hidden" Visible="false" DataField="GSTINInd" />
                                                    <asp:TemplateField ControlStyle-CssClass="btn btn-danger btn-sxs" HeaderStyle-CssClass="grid_head">
                                                        <ItemTemplate>
                                                            <asp:Button CommandName="RemoveItem" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <div class="panel-footer">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="pull-left">
                                                <label>Mandatory Fields&nbsp;<i class="text-danger"> * </i></label>
                                            </div>
                                            <div class="pull-right">
                                                <div class="error_div ac_hidden">
                                                    <div class="alert alert-danger error_msg"></div>
                                                </div>
                                                <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                                <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return Validation()" runat="server" />
                                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:HiddenField ID="hfMainGroupCode" runat="server" />
                                <asp:HiddenField ID="hfMainSubGroupCode" runat="server" />
                                <asp:HiddenField ID="hfAccSubGroupID" runat="server" />
                                <asp:HiddenField ID="hfAccGICodeFrom" runat="server" />
                                <asp:HiddenField ID="hfAccGICodeTo" runat="server" />
                                <asp:HiddenField ID="hfBranchState" runat="server" />
                            </div>
                            <asp:Panel runat="server" ID="pnlConfirm" CssClass="reportPopUp" Visible="false" TabIndex="-1" Style="position: absolute; top: 0; right: 0; left: 0;">
                                <div class="bodyPop" style="width: 30%">
                                    <div class="col-md-12">
                                        <h4>
                                            <asp:Label ID="lblConfirmationMSG" runat="server"></asp:Label></h4>
                                        <hr />
                                        <div class="text-right">
                                            <asp:Button ID="btnYes" CssClass="btn btn-primary" Text="Yes" OnClick="btnYes_Click" runat="server" />
                                            <asp:Button ID="btnNo" CssClass="btn btn-danger" Text="No" OnClick="btnNo_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnYes" />
        </Triggers>
    </asp:UpdatePanel>
    <style>
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

        .reportPopUp {
            top: 60px;
            bottom: 0;
            padding-top: 20px;
            background-color: rgba(0, 0, 0, 0.63);
            position: fixed;
            z-index: 1940;
        }

        #ContentPlaceHolder1_pnlInvoiceReport table tr td {
            height: initial;
            width: initial;
            padding: initial;
            border: 0;
        }

        #ContentPlaceHolder1_pnlInvoiceReport table {
            border: 0;
        }

        #ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
            overflow-x: hidden !Important;
            border-bottom: 1px solid #ddd;
            padding-right: 5px;
        }

        @media (max-width:768px) {
            #ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
                overflow-x: auto !Important;
                border-bottom: 1px solid #ddd;
            }
        }

        @media print {
            ctl00_ContentPlaceHolder1_ReportViewer1_ctl09 {
                width: 100%;
            }
        }
    </style>
</asp:Content>
