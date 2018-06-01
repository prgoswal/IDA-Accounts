<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmChangePassword.aspx.cs" Inherits="frmChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //$(document).ready(function() {
        //    $("#save").click(function(){
        //alert('maximum 8 char');
        //});});
    </script>


    <style type="text/css">
       


        .btn-circle {
            width: 30px;
            height: 30px;
            text-align: center;
            padding: 6px 0;
            font-size: 12px;
            line-height: 1.428571429;
            border-radius: 15px;
        }

            .btn-circle.btn-lg {
                width: 50px;
                height: 50px;
                padding: 10px 16px;
                font-size: 18px;
                line-height: 1.33;
                border-radius: 25px;
            }

            .btn-circle.btn-xl {
                width: 70px;
                height: 70px;
                padding: 10px 16px;
                font-size: 24px;
                line-height: 1.33;
                border-radius: 35px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <div class="container_fluid">
                    <div class="row">
                        <div class="col-sm-offset-2 col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="text-center form-group row">
                                        <h3><i class="fa fa-lock" aria-hidden="true"></i>Change Password</h3>
                                    </div>
                                    <div class="form-horizontal col-sm-10">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4">Current Pasword</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtcurrentPass" runat="server" CssClass="form-control" placeholder="Current Pasword" TextMode="Password" ></asp:TextBox>
                                                        <%-- <input type="text" class="form-control" id="currentpass" name="" placeholder="Current Pasword">--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4 ">Change Password</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtchangePass" runat="server" CssClass="form-control" placeholder="Change Password" TextMode="Password"></asp:TextBox>
                                                        <%-- <input type="text" class="form-control" id="changepass" name="" placeholder="Change Password">--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4">Confirm Password</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtconfirmPass" runat="server" CssClass="form-control" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                                                        <%-- <input type="text" class="form-control" id="confirmpass" name="" placeholder="Confirm Password">--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 ">
                                       <%-- <button type="button" class="btn btn-success btn-circle "><i class="fa fa-check"></i></button>
                                        <button type="button" class="btn btn-danger btn-circle hidden "><i class="fa fa-warning "></i></button>--%>
                                    </div>

                                </div>
                                <div class="panel-footer">
                                    <div class="text-center">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="text-danger"></asp:Label>
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary fa r fa-floppy-o" runat="server" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" CssClass="btn btn-danger fa r fa-close" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                        <%-- <a href="#" class="btn btn-success" id="save"><i class="fa r fa-floppy-o"></i>Save</a>--%>

                                        <%--<a href="#" class="btn btn-danger "><i class="fa r fa-close"></i>Clear</a>--%>
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

