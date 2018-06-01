<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $('body').removeClass('aside-collapsed');
            if ($(window).width() < 768) {
                $('body').addClass('aside-collapsed');
            }
            $('.section_loader').addClass('bgm');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-sm-12">
        <div class="col-sm-5 welcomegst">
            <%--<img src="../Content/css/images/GST.png " style="margin-top:30px;height: 150px; width: 260px;" />--%>
            <%--<div class="col-sm-12 text-black"><strong>Integrated Accounting Application</strong></div>--%>
        </div>
        <div class="col-sm-4  alert">
            <div class="row">
                <div class="col-sm-12 col-md-12">
                    <h4>
                        <asp:Label ID="lblRemark" Text="" runat="server" />

                    </h4>
                </div>

            </div>

        </div>
    </div>
    <style>
        .alert p {
            padding-top: 10px;
        }

        .alert {
            margin-top: 40px !important;
            color: black !important;
        }

        .bottom_logo {
            height: 60px;
            width: 60px;
            float: left;
        }

        @media (min-width:768px) {
            .bottom_info {
                color: #000;
                position: absolute;
                bottom: 0;
                right: 0;
                float: right;
            }


            .mainin {
                float: right;
                width: 580px;
            }
            .aside-collapsed .mainin {
                float: right;
                width: 400px;
            }
        }
    </style>

    <div class="bottom_info">
        <div class="mainin mycontent-left">
            <img src="../Content/css/images/logo1.png" class="bottom_logo" />
            <strong class="mainin2"><span class="text-center">An Initiative of<br />
                Oswal Computers & Consultants Pvt. Ltd.<br />
                ISO 9001:2008 | ISO 27001:2013</span></strong>
        </div>

    </div>


</asp:Content>

