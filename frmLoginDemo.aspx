<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmLoginDemo.aspx.cs" Inherits="frmLogin" %>

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
</head>
<body class="login_page" style="background-image: url('Content/css/images/LoginRegBG.jpg'); background-size: cover;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <%--<div class="wrapper">--%>
        <div class="wpr">

            <nav role="navigation" class="navbar navbar-default ac_nav">

                <div class="rows header ">
                    <div class="col-sm-3">
                        <img src="Content/css/images/logo.png" alt="Image" class="img-rounded img-responsive logo-oswal" style="height: 60px; width: 100px" />
                    </div>
                </div>
                <div class="col-sm-6 text-white text-center">
                    <h2 class="product_name">Integrated Accounting Application</h2>
                </div>
            </nav>
            
            <div class="container-fluid">
                <div class="col-sm-offset-4 col-sm-4 col-xs-12">
                    <div class="wpr">
                        <div class="">
                            <div class="row login">
                                <div class="form-signin form-horizontal  animated fadeIn" id="loginform">

                                    <div class="error_div ac_hidden">
                                        <div class="alert alert-danger error_msg"></div>
                                    </div>

                                    <div class="row text-center">
                                        <img src="Content/img/dummy2.png" alt="User" class="img-circle user_image" />
                                    </div>


                                    <div class="col-sm-12">

                                        <asp:Panel ID="pnlUserID" runat="server">
                                            <div class="form-group select_parent hidden">
                                                <asp:DropDownList ID="ddlFinancialYear" CssClass="form-control select-year" runat="server"></asp:DropDownList>
                                            </div>

                                            <div class="form-group userid_parent">
                                                <asp:TextBox ID="txtUserID" CssClass="form-control user-id" placeholder="User Name" runat="server" />
                                            </div>

                                            <div class="form-group btn_next_parent">
                                                <asp:Button ID="btnNext" CssClass="btn btn-block btn-primary btn_next next-click" OnClick="btnNext_Click" Text="Next" runat="server" />
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlUserPassword" runat="server" DefaultButton="btnLogin">
                                            <div class="form-group div_details ac_hidden animated fadeIn">
                                                <span class=""><span class="welcomeyear text-black hidden">Financial Year :
                                                    <asp:Label ID="lblFinancialYr" CssClass="fin_year" runat="server"></asp:Label></span>
                                                    <br />
                                                    <span class="welcome text-black">Welcome <span class="username"></span>!</span>
                                                </span>
                                            </div>

                                            <div class="form-group password_parent ac_hidden animated fadeIn">
                                                <asp:TextBox ID="txtPassword" CssClass="form-control input_pass" TextMode="Password" placeholder="Password" runat="server" />
                                            </div>

                                            <div class="form-group btn_login_parent ac_hidden animated fadeIn">
                                                <asp:Button ID="btnLogin" CssClass="btn btn-block btn-primary btn_login" OnClick="btnLogin_Click" Text="Login" runat="server" />
                                                <a href="#!" class="btn_back hidden"><i class="fa r fa-chevron-left"></i>Back</a>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="pnlBranch" runat="server" >
                                            <div class="form-group branch_parent ac_hidden animated fadeIn">
                                                <asp:DropDownList ID="ddlBranch" CssClass="form-control select-branch" runat="server"></asp:DropDownList>
                                            </div>
                                            <div class="form-group btn_final_login_parent ac_hidden animated fadeIn">
                                                <asp:Button ID="btnBranchSubmit" CssClass="btn btn-block btn-primary btn_final_login" Text="Continue" OnClick="btnBranchSubmit_Click" runat="server" />
                                                <a href="#" id="btnBack2" class="btn_back_login hidden"><i class="fa r fa-chevron-left"></i>Back</a>
                                            </div>

                                        </asp:Panel>
                                        <div>
                                            <asp:Label ID="lblRegInfo" CssClass="text-info" runat="server" />
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

            <div class="modal fade" id="ModalInfo" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" id="header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" id="title"></h4>
                        </div>
                        <div class="modal-body">
                            <h4><i class="fa fa-exclamation-circle"></i><span id="txtMsg"></span></h4>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="ModalLoader" data-backdrop="static" role="dialog">
                <div style="display: flex; height: 100%; width: 100%">
                    <div style="margin: auto; color: white;">
                        <i class="fa fa-5x fa-spinner fa-pulse fa-fw"></i>
                        <br />
                        <br />
                        <span class="fa fa-3x ">Loading...</span>
                    </div>
                </div>
            </div>
            <script src="Content/js/jquery.min.js"></script>
            <script src="Content/js/jquery-ui.js"></script>
            <script src="Content/js/bootstrap.min.js"></script>
            <script src="Content/js/app.js"></script>
            <script src="Content/js/index.js"></script>
            <script>
                function Loading() {
                    
                    try {
                        $('#ModalLoader').modal();
                    } catch (e) { }
                }
            </script>
        </div>
    </form>
</body>
</html>
