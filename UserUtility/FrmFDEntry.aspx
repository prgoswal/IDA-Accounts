<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmFDEntry.aspx.cs" Inherits="FDControl_FrmFDEntry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Star {
            color: red;
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
            top: -33px;
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
    <script type="text/javascript">


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

            <script type="text/javascript" lang="javascript">
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">Fixed Deposit Entry
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="col-sm-offset-1 col-sm-10">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-sm-12">







                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-8">
                                                            <label class="col-sm-12 alphaonly" style="padding: 0%;">Bank<i class="Star">*</i></label>

                                                            <asp:DropDownList ID="ddlAccountHead" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="reqddlAccountHead" runat="server" ControlToValidate="ddlAccountHead" InitialValue="0" ValidationGroup="btn" ErrorMessage="Select Bank." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label class="col-sm-12 alphaonly" style="padding: 0%;">Scheme Type<i class="Star">*</i></label>

                                                            <asp:TextBox ID="txtSchemeType" placeHolder="Scheme Type" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqtxtSchemeType" runat="server" ControlToValidate="txtSchemeType" ValidationGroup="btn" ErrorMessage="Enter Scheme Type." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-12 alphaonly">Deposit Amount & Date<i class="Star">*</i></label>

                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtDepositAmt" placeHolder="Deposit Amount" MaxLength="15" runat="server" CssClass="form-control Money"></asp:TextBox>


                                                            <asp:RequiredFieldValidator ID="reqtxtDepositAmt" runat="server" ControlToValidate="txtDepositAmt" ValidationGroup="btn" ErrorMessage="Enter Deposit Amount." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                            <asp:RegularExpressionValidator ControlToValidate="txtDepositAmt" ID="regExVal" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="btn" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                        </div>

                                                        <div class="col-sm-6">

                                                            <asp:TextBox ID="txtDespositDate" runat="server" CssClass="form-control datepicker " MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqtxtDespositDate" runat="server" ControlToValidate="txtDespositDate" ValidationGroup="btn" Display="Dynamic" ErrorMessage="Enter Deposit Date." CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group row">
                                                    <div class="col-sm-6">



                                                        <div class="col-sm-4">
                                                            <label class="col-sm-12 alphaonly" style="padding-left: 0;">Rate Of Interest<i class="Star">*</i></label>
                                                            <asp:TextBox ID="txtROI" runat="server" placeHolder="ROI" CssClass="form-control Money" MaxLength="5"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="reqtxtROI" runat="server" ControlToValidate="txtROI" ValidationGroup="btn" ErrorMessage="Enter Rate Of Intrest." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                            <asp:RegularExpressionValidator ControlToValidate="txtROI" ID="regExValtxtROI" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="btn" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                        </div>
                                                        <div class="col-sm-8" style="padding-right: 0;">
                                                            <label class="col-sm-12 alphaonly">Deposite Term<i class="Star">*</i></label>
                                                            <div class="col-sm-4">

                                                                <asp:TextBox ID="txtDepositeYear" placeHolder="Year" runat="server" CssClass="form-control number" MaxLength="3"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDespositDate" ValidationGroup="btn" ErrorMessage="Enter Deposit Date." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>--%>

                                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Only Numeric." ControlToValidate="txtDepositeYear" CssClass="gst-ac-error"
                                                                    ValidationExpression="^\d+$" ValidationGroup="btn"></asp:RegularExpressionValidator>

                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtDepositeMonth" placeHolder="Month" runat="server" CssClass="form-control number" MaxLength="3"></asp:TextBox>
                                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Only Numeric." ControlToValidate="txtDepositeMonth" CssClass="gst-ac-error" Display="Dynamic"
                                                                    ValidationExpression="^\d+$" ValidationGroup="btn"></asp:RegularExpressionValidator>

                                                                <asp:DropDownList ID="ddlDepositTerm" Style="display: none;" runat="server" CssClass="form-control">
                                                                    <asp:ListItem runat="server" Value="365" Text="Days"></asp:ListItem>
                                                                    <asp:ListItem runat="server" Value="12" Text="Months"></asp:ListItem>
                                                                    <asp:ListItem runat="server" Value="1" Text="Year"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-4">

                                                                <asp:TextBox ID="txtDepositeDay" placeHolder="Day" runat="server" CssClass="form-control number" MaxLength="3"></asp:TextBox>

                                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Only Numeric." ControlToValidate="txtDepositeDay" CssClass="gst-ac-error" Display="Dynamic" ValidationGroup="btn" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>




                                                    </div>


                                                    <div class="col-sm-6">
                                                        <label class="col-sm-12 alphaonly">Maturity Amount & Date<i class="Star">*</i></label>

                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtMaturityAmt" placeHolder="Maturity Amount" runat="server" CssClass="form-control Money" MaxLength="15"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaturityAmt" ValidationGroup="btn" Display="Dynamic" ErrorMessage="Enter Maturity Amount." CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                                            <asp:RegularExpressionValidator ControlToValidate="txtDepositAmt" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="btn" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                        </div>

                                                        <div class="col-sm-6">

                                                            <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="form-control datepicker " MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="reqtxtMaturityDate" runat="server" ControlToValidate="txtMaturityDate" ValidationGroup="btn" Display="Dynamic" ErrorMessage="Enter Maturity Date." CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <div class="col-sm-6">
                                                            <label class="col-sm-12 alphaonly" style="padding-left: 0;">FDR Number</label>
                                                            <asp:TextBox ID="txtFDNo" MaxLength="15" placeHolder="FDR Number" runat="server" CssClass="form-control"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="reqtxtFDNo" runat="server" ControlToValidate="txtFDNo" ValidationGroup="btn" ErrorMessage="Enter FD Number." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <label class="col-sm-6 alphaonly">FDR Account Number</label>
                                                        <div class="col-sm-6">

                                                            <asp:TextBox ID="txtFDRAccNo" runat="server" placeHolder="FDR Account Number" CssClass="form-control" MaxLength="15"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="reqtxtFDRAccNo" runat="server" ControlToValidate="txtFDRAccNo" ValidationGroup="btn" ErrorMessage="Enter FDR Account Number." Display="Dynamic" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">


                                                        <div class="col-sm-4">
                                                            <label class="col-sm-12 alphaonly" style="padding: 0;">Is OD Lien<i class="Star">*</i></label>
                                                            <asp:RadioButtonList ID="rbtnIsODLien" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnIsODLien_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                        <label class="col-sm-8 alphaonly">Lien Amount & Date </label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtLienAmount" placeHolder="Lien Amount" runat="server" CssClass="form-control Money" MaxLength="15"></asp:TextBox>

                                                            <asp:RegularExpressionValidator ControlToValidate="txtLienAmount" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Amount." Display="Dynamic" ValidationGroup="btn" CssClass="gst-ac-error" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" />
                                                        </div>
                                                        <div class="col-sm-4">

                                                            <asp:TextBox ID="txtLienDate" runat="server" CssClass="form-control datepicker " MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                </div>


                                                <div class="form-group row">
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control" placeHolder="Source of fund"></asp:TextBox>
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
                                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                            <asp:Button ID="btnSave" ValidationGroup="btn" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                            <asp:Button ID="btnclear" CssClass="btn btn-danger" runat="server" Text="Clear" OnClick="btnclear_Click" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


