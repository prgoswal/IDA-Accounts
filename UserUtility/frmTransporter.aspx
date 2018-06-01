<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmTransporter.aspx.cs" Inherits="UserUtility_frmTransporter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .transport-radio-list {
            float: left;
            padding-top: 8px;
        }

            .transport-radio-list table, .transport-radio-list table tbody, .transport-radio-list tbody tr, .transport-radio-list tbody tr td {
                display: block;
                float: left;
                width: auto;
            }

                .transport-radio-list tbody tr td input[type="radio"], .transport-radio-list tbody tr td label {
                    display: block;
                    float: left;
                    cursor: pointer;
                    user-select: none;
                    font-weight: bold;
                }

                    .transport-radio-list tbody tr td input[type="radio"]:checked + label {
                        font-weight: bold;
                    }

                .transport-radio-list tbody tr td input[type="radio"] {
                    margin-right: 2px;
                }

                .transport-radio-list tbody tr td label {
                    margin-right: 8px;
                    margin-top: 0px;
                }

        .transport-radio-gap {
            padding-top: 8px;
        }

            .transport-radio-gap input[type="checkbox"], .transport-radio-gap label {
                display: block;
                float: left;
                cursor: pointer;
                user-select: none;
            }

            .transport-radio-gap input[type="checkbox"] {
                margin-right: 3px;
            }

            .transport-radio-gap label {
                margin-right: 10px;
                margin-top: 0px;
            }

        .form-control-pd-sm {
            padding-left: 5px;
            padding-right: 5px;
        }

        .form-horizontal-label-left .control-label {
            padding-top: 8px;
            text-align: left !important;
        }

        /*error message popup*/
        .gst-ac-error-holder {
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
            top: -40px;
            right: 0;
            box-shadow: 0px 0px 5px 2px rgba(0,0,0,0.5);
        }

            .gst-ac-error::after {
                content: '';
                display: block;
                position: absolute;
                top: 100%;
                left: 50%;
                width: 0;
                Z-INDEX: 2;
                transform: translateX(-50%);
                height: 0;
                border-top: 10px solid #f05050;
                border-right: 10px solid transparent;
                border-bottom: 10px solid transparent;
                border-left: 10px solid transparent;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content-wrapper">
        <h3 class="text-center head">Transporter Details
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div style="max-width: 900px; margin: 0 auto">
                    <div class="panel panel-default mb0">
                        <div class="panel-body" style="min-height: 288px">
                            <div class="form-horizontal form-horizontal-label-left">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">Transportation&nbsp;Name</label>
                                            <div class="col-sm-9 gst-ac-error-holder">
                                                <asp:TextBox ID="txtTransportationName" placeholder="Transportation Name" CssClass=" form-control" MaxLength="49" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtTransportationName" ErrorMessage="Please Enter Transporation Name" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-3  control-label">Owner Name</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtOwner" placeholder="Owner Name" CssClass=" form-control" MaxLength="49" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOwner" ErrorMessage="Please Enter Owner Name" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-3  control-label">Address</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtAddress" placeholder="Address" CssClass=" form-control" TextMode="MultiLine" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" ErrorMessage="Please Enter Address" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <label class="col-sm-1 control-label">City</label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtcity" CssClass=" form-control form-control-pd-sm" placeholder="City" MaxLength="35" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcity" ErrorMessage="Please Enter City" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control form-control-pd-sm"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ControlToValidate="ddlState" ErrorMessage="Please Select State" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <label class="col-sm-1  control-label">Pincode</label>
                                                    <div class="col-sm-2">
                                                        <asp:TextBox ID="txtpincode" placeholder="PinCode" CssClass=" form-control inpt num-only" MaxLength="6" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpincode" ErrorMessage="Please Enter Pincode" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <div class="col-sm-3">
                                                <span class="transport-radio-list">
                                                    <asp:RadioButtonList ID="rdogstreg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdogstreg_SelectedIndexChanged">
                                                        <asp:ListItem Text="GSTIN" Value="1" Selected></asp:ListItem>
                                                        <asp:ListItem Text="Registration ID" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </span>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtgstorreg" CssClass="" runat="server" placeholder="Enter GSTIN" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtgstorreg" ErrorMessage="Please Enter GSTIN" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </div>
                                            <label class="col-sm-2  control-label">Transport&nbsp;Through</label>
                                            <div class="col-sm-4 transport-radio-gap">
                                                <asp:CheckBox ID="chkRoad" Text="Road" name="CHECKBOX_4" Checked runat="server" />
                                                <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Required" Display="Dynamic" ClientValidationFunction = "ValidateCheckBox"></asp:CustomValidator>--%>
                                                <asp:CheckBox ID="chkRail" Text="Rail" name="CHECKBOX_1" runat="server" />
                                                <asp:CheckBox ID="chkAir" Text="Air" name="CHECKBOX_2" runat="server" />
                                                <asp:CheckBox ID="chkShip" Text="Ship" name="CHECKBOX_3" runat="server" />
                                                <asp:Label ID="lblmsgerror" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <script type="text/javascript">
                                        function ValidateCheckBox(sender, args) {
                                            if (document.getElementById("<%=chkRoad.ClientID %>").checked == true) {
                                                args.IsValid = true;
                                            } else {
                                                args.IsValid = false;
                                            }
                                        }
                                    </script>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="text-right">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" onsubmit="return checkCheckBoxes(this);" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnclear" CssClass="btn btn-danger" runat="server" Text="Clear" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfTransportID" runat="server" Value="0" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $(".num-only").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });
    </script>
</asp:Content>

