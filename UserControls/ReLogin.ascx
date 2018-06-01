<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReLogin.ascx.cs" Inherits="UserControls_ReLogin" %>


<div class="modal fade" id="ModalReLogin"  data-backdrop="static" role="dialog" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" id="header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title" id="title">User Session Is Expired Please Relogin First!</h4>
            </div>
            <div class="modal-body">
                <h4><i class="fa fa-exclamation-circle"></i><span id="txtMsg"></span></h4>

                <div class="row login">
                    <div class="form-signin form-horizontal  animated fadeIn" id="loginform">

                        <div class="error_div ac_hidden">

                            <div class="alert alert-danger error_msg">
                                You Are Authenticate Only For Demo Version. Please Click On Below Link :- 
                                            <a href="http://demo.gstsaathiaccounts.in/frmLoginDemo.aspx">http://demo.gstsaathiaccounts.in </a>
                            </div>

                        </div>

                        <div class="row text-center">
                            <img src="../Content/img/dummy2.png" alt="User" class="img-circle user_image" />
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

                            <asp:Panel ID="pnlBranch" runat="server">
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
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
