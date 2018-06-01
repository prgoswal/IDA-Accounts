<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmSuccessfullyProfileCreation.aspx.cs" Inherits="frmSuccessfullyProfileCreation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Successfully Status!</title>
    <link href="Content/css/custom.css" rel="stylesheet" />
    <style>
        .success_msgbox {
            background:#27ae60;
            color:#fff;
            padding:100px;
            border-radius:5px
        }.success_msgbox p {
           font-size:25px;       
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wpr">
            <nav role="navigation" class="navbar navbar-default ac_nav">
                <div class="rows header ">
                    <div class="col-sm-3">
                        <img src="Content/css/images/logo.png" alt="Image" class="img-rounded img-responsive logo-oswal" style="height: 60px; width: 100px" />
                    </div>
                </div>
                <link href="Content/css/style.css" rel="stylesheet" />
                <div class="col-sm-6 text-white text-center">
                    <h2 class="product_name"><asp:Label ID="lblCompanyName" runat="server"></asp:Label></h2>
                </div>
            </nav>

            <div class="container-fluid">
                <div class="col-sm-6 col-sm-offset-3 success_msgbox">
                    <div class="text-center">
                        <p><i class="fa fa-check-circle" style="font-size:100px"></i></p>
                        <p> <asp:Label ID="lblSuccessfullyMSG" runat="server" /></p>
                        <p><a href="Logout.aspx" class="btn btn-warning btn-lg"><i class="fa fa-sign-in"></i> &nbsp;&nbsp;Login</a></p>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
