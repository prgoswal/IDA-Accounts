<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRegistration.aspx.cs" Inherits="FrmRegistration" %>

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
                        <img src="Content/css/images/logo.png" alt="Image" class="img-rounded img-responsive logo-oswal" style="height: 60px; width: 100px;">
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

                                <h3 style="color: #1c75bf;"><b><u>Registration Form</u></b></h3>
                            </div>
                            <div class="col-sm-4 col-sm-offset-4">
                                <asp:RadioButtonList runat="server" ID="rbList" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Demo" Value="0" Selected="True" />
                                    <asp:ListItem Text="Live" Value="1" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">User Name<span style="color:red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="45" CssClass="form-control alfanumaric text-uppercase" placeholder="Full Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Email Id<span style="color:red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" MaxLength="50"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Mobile No.<span style="color:red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtMobilNo" CssClass="form-control numberonly" runat="server" MaxLength="10" placeholder="Enter 10 digit mobile no."></asp:TextBox>

                                        </div>

                                    </div>
                                </div>

                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <label class="col-sm-4 alphaonly">Payment No.<span style="color:red">*</span></label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPaymnetNo" CssClass="form-control numberonly" runat="server" MaxLength="9" placeholder="Payment Number"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-xs-8 text-right">
                                    <asp:Label ID="lblmsg" runat="server" CssClass=" text-danger"></asp:Label>
                                </div>

                                <div class="col-xs-4">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Load()" />
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="btn btn-danger" Text="Clear" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>


        <div class="loading" id="loader"></div>
        <script src="Content/js/jquery.min.js"></script>
        <script src="Content/js/bootstrap.min.js"></script>
        <script src="Content/js/app.js"></script>
        <script src="Content/js/index.js"></script>
        <script>
            function Validation() {
                
                if ($('#<%=txtName.ClientID%>').val() == '') {
                    $('#<%=lblmsg.ClientID%>').html('Please Enter Name');
                    $('#<%=txtName.ClientID%>').focus();
                    return false;
                }

                if ($('#<%=txtEmail.ClientID%>').val() == '') {
                    $('#<%=lblmsg.ClientID%>').html('Please Enter Email Address');
                    $('#<%=txtEmail.ClientID%>').focus();
                    return false;
                }

                if ($('#<%=txtEmail.ClientID%>').val() != '' || $('#<%=txtEmail.ClientID%>').val() == '') {
                    var emailRegex = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i);
                    var emailAddress = document.getElementById("<%= txtEmail.ClientID %>").value;
                    var valid = emailRegex.test(emailAddress);
                    if (!valid) {
                        $('#<%=lblmsg.ClientID%>').html('Please Enter Valid Email Address');
                        $('#<%=txtEmail.ClientID%>').focus();
                        return false;
                    }
                }
                if ($('#<%=txtMobilNo.ClientID%>').val().length != 10) {
                    $('#<%=lblmsg.ClientID%>').html('Please Enter 10 Digit Mobile No.');
                    $('#<%=txtMobilNo.ClientID%>').focus();
                    return false;

                }
                if ($('#<%=txtPaymnetNo.ClientID%>').val() == '') {
                    $('#<%=lblmsg.ClientID%>').html('Please Enter Payment No.');
                     $('#<%=txtPaymnetNo.ClientID%>').focus();
                     return false;

                 }
                return true;
            }

            function Load() {
                
                var a = Validation();
                if (a == true) {
                    $('#<%=lblmsg.ClientID%>').html('');
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
