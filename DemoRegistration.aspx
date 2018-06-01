<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DemoRegistration.aspx.cs" Inherits="DemoRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="Content/css/images/logo_single_3v1_icon.ico" type="image/x-icon" />
    
    <title>Account</title>
    <link href="Content/css/style.css" rel="stylesheet" />
    <link href="Content/css/custom.css" rel="stylesheet" />
    <%--<script src="Content/js/jquery.min.js"></script>--%>
    
</head>
<body class="login_page" style="background-image: url('Content/css/images/LoginRegBG.png'); background-size: cover;">

    <form id="form1" runat="server">
        <div class="wpr">

            <nav role="navigation" class="navbar navbar-default ac_nav">

                <div class="rows header ">
                    <div class="col-sm-3">
                        <img src="Content/css/images/logo.png" alt="Image" class="img-rounded img-responsive logo-oswal" style="height: 60px; width:100px;">
                    </div>
                </div>
                <div class="col-sm-6 text-white text-center">
                    <h2 class="product_name" style="font-size: 30px;">Integrated Accounting Application</h2>
                </div>


            </nav>

            <div class="container-fluid">

                <div class="row demo">
                    <div class="form-signin form-horizontal" id="loginform">

                        <div class="error_div ac_hidden">
                            <div class="alert alert-danger error_msg"></div>
                        </div>
                        <div class="form-horizontal">
                            <div class="col-sm-12 form-group text-center">

                                <h3 style="color: #1c75bf;"><b><u>Request For Demo</u></b></h3>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Full Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control Alphaonly text-uppercase" placeholder="Full Name"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="" placeholder="Full Name">--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Organization Type</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList CssClass="form-control" ID="ddlOrgtype" runat="server">
                                                <asp:ListItem Text="-- Select --" Value="0" Selected="True" />
                                                <asp:ListItem Text="INDIVIDUAL" />
                                                <asp:ListItem Text="CA/TAX CONSULTANT" />
                                                <asp:ListItem Text="BUSSINESS OWNER" />
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtOrgType" runat="server" MaxLength="30" placeholder="Organization Type"></asp:TextBox>--%>
                                            <%--<input type="text" class="form-control" name="" placeholder="Organization Type">--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4">Organization Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtOrgName" runat="server" MaxLength="70" CssClass="form-control  text-uppercase Alphaonly" placeholder="Organization Name"></asp:TextBox>
                                            <%-- <input type="text" class="form-control" name="" placeholder="Organization Name">--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Email</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control Email" placeholder="Email" MaxLength="35"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="" placeholder="Email">--%>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Mobile No.</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMobilNo" CssClass="form-control numberonly" runat="server" MaxLength="10" placeholder="Enter 10 digit mobile no."></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="" placeholder="Mobile No.">--%>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" runat="server" CssClass=" text-danger"></asp:Label>
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary  btn-space-right" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Load()" />
                                        </div>
                                    </div>
                                </div>

                        </div>
                    </div>
                </div>

            </div>

        </div>
        <div class="loading" id="loader" ></div>
        <script src="Content/js/jquery.min.js"></script>
        <script src="Content/js/bootstrap.min.js"></script>
        <script src="Content/js/app.js"></script>
        <script src="Content/js/index.js"></script>
        <script>
            function Validation() {
                
                lblMsClass = $('#<%=lblMsg.ClientID%>');

                if ($('#<%=txtName.ClientID%>').val() == '') {
                    
                    $('#<%=lblMsg.ClientID%>').html('Please Enter Name');
                    lblMsClass.addClass("alert alert-danger");
                    $('#<%=txtName.ClientID%>').focus();
                    return false;
                }

                if ($('#<%=txtOrgName.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').html('Please Enter OrgName');
                    lblMsClass.addClass("alert alert-danger");
                $('#<%=txtOrgName.ClientID%>').focus();
                return false;
            }

             <%--if ($('#<%=txtOrgType.ClientID%>').val() == '') {
                 $('#<%=lblMsg.ClientID%>').html('Please Enter OrgType');
                 $('#<%=txtEmail.ClientID%>').focus();
                 return false;
             }--%>

                if ($('#<%=txtEmail.ClientID%>').val() == '') {
                    $('#<%=lblMsg.ClientID%>').html('Please Enter Email Address');
                    lblMsClass.addClass("alert alert-danger");
                    $('#<%=txtEmail.ClientID%>').focus();
                    return false;
                }

                if ($('#<%=txtEmail.ClientID%>').val() != '' || $('#<%=txtEmail.ClientID%>').val() == '') {
                    var emailRegex = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i);
                    var emailAddress = document.getElementById("<%= txtEmail.ClientID %>").value;
                    var valid = emailRegex.test(emailAddress);
                    if (!valid) {
                        $('#<%=lblMsg.ClientID%>').html('Please Enter Valid Email Address');
                        lblMsClass.addClass("alert alert-danger");
                        $('#<%=txtEmail.ClientID%>').focus();
                        return false;
                    }
                }
                if ($('#<%=txtMobilNo.ClientID%>').val().length != 10) {
                    $('#<%=lblMsg.ClientID%>').html('Please Enter 10 Digit Mobile No.');
                    lblMsClass.addClass("alert alert-danger");
                    $('#<%=txtMobilNo.ClientID%>').focus();
                    return false;                  
                   
             }
             return true;
         }

         function Load() {
             
             var a = Validation();
             if (a == true) {
                 $('#<%=lblMsg.ClientID%>').html('');
                 document.getElementById('loader').classList.add('active');
                 return true;
             }
             return false;
         }
        </script>
        <footer class="footer">
            <span>© 2017 All rights reserved 2016-2017 Oswal Computers And Consultants Pvt. Ltd.</span>
        </footer>

    </form>
</body>
</html>
