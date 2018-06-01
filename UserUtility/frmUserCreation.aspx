<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmUserCreation.aspx.cs" Inherits="frmUserCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function UserCreationValidation() {

            //------------------For Branch -------------------//

            if ($('#<%=ddlBranch.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Your Branch.');
                $('#<%=ddlBranch.ClientID%>').focus();
                return false;
            }

            //------------------For Full Name -------------------//

            if ($('#<%=txtFullName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Full Name.');
                $('#<%=txtFullName.ClientID%>').focus();
                return false;
            }

            //------------------For Mobile number -------------------//

            if ($('#<%=txtMobileNo.ClientID%>').val().length != 10) {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter 10 Digit Mobile No.');
                $('#<%=txtMobileNo.ClientID%>').focus();
                return false;
            }

            //------------------For Valid Email -------------------//

            if ($('#<%=txtEmailAddress.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Email Address.');
                $('#<%=txtEmailAddress.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtEmailAddress.ClientID%>').val() != '' || $('#<%=txtEmailAddress.ClientID%>').val() == '') {
                var emailRegex = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i);
                var emailAddress = document.getElementById("<%= txtEmailAddress.ClientID %>").value;
                var valid = emailRegex.test(emailAddress);
                if (!valid) {
                    $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                    $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Valid Email Address.');
                    $('#<%=txtEmailAddress.ClientID%>').focus();
                    return false;
                }
            }

            //------------------For Designation -------------------//

            if ($('#<%=ddlDesignation.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Date Of Expiry.');
                $('#<%=ddlDesignation.ClientID%>').focus();
                return false;
            }

            //------------------For Date Of Expiry -------------------//

            if ($('#<%=ddlDateOfExpiry.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Date Of Expiry.');
                $('#<%=ddlDateOfExpiry.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <h3 class="text-center" style="padding: 5px">CREATE USERS
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-4" style="display: none;">
                                            <div class="form-group row">
                                                <label class="col-sm-12 alphaonly">Branch</label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlBranch" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 alphaonly">User Roll</label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlUserRoll" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 alphaonly">Department</label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 ">Sub-Department</label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlSubDepartment" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 ">Full Name</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="Full Name" runat="server" />
                                                    <cc1:FilteredTextBoxExtender ID="Txtfather_FilteredTextBoxExtender" runat="server" FilterMode="ValidChars" TargetControlID="txtFullName" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ. "></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 ">Mobile No.</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtMobileNo" CssClass="form-control numberonly" MaxLength="10" placeholder="Mobile No." runat="server" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMobileNo" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 ">Email Adderss</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtEmailAddress" CssClass="form-control Email" MaxLength="45" placeholder="Email Adderss" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 ">Designation</label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlDesignation" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtDesignation" CssClass="form-control" placeholder="Designation" runat="server" />--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12 ">Date Of Expiry</label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlDateOfExpiry" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem>No Expiry</asp:ListItem>
                                                        <asp:ListItem Value="10">10 Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="text-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return UserCreationValidation()" Text="Save" CssClass="btn btn-primary" runat="server" />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-danger" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <style type="text/css">
                            .usertb {
                                width: 100%;
                            }
                        </style>
                    </div>
                    <div class="row">
                        <div class="panel panel-default" style="margin-bottom: 0" id="pnlUserGrid" runat="server" visible="false">
                            <div class="panel-body">
                                <style>
                                    .first_tr_hide tbody tr:first-child {
                                        display: none;
                                    }

                                    .hide_my_pdosi + tr {
                                        display: none;
                                    }

                                    .first_tr_hide tr td:first-child {
                                        display: none;
                                    }
                                </style>
                                <asp:GridView ID="grdUserCreation" CssClass="usertb first_tr_hide" AutoGenerateColumns="false" OnRowDataBound="grdUserCreation_RowDataBound" OnRowCommand="grdUserCreation_RowCommand" runat="server">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <tr>
                                                    <th class="inf_head" style="width: 12.05%; display: none;">Branch</th>

                                                    <th class="inf_head" style="width: 6.05%;">Department</th>
                                                    <th class="inf_head" style="width: 6.05%;">Sub-Department</th>
                                                    <th class="inf_head" style="width: 15.05%;">User&nbsp;Name</th>
                                                    <th class="inf_head" style="width: 12.05%;">Mobile&nbsp;No.</th>
                                                    <th class="inf_head" style="width: 18.05%;">Email&nbsp;Address</th>
                                                    <th class="inf_head" style="width: 15.05%;">Designation</th>
                                                    <th class="inf_head" style="width: 12.05%;">Date&nbsp;of&nbsp;Expiry</th>
                                                    <th class="inf_head" style="width: 6.05%;">Status</th>
                                                    <th class="inf_head" style="width: 6.05%;">Action</th>
                                                </tr>
                                                <tr class="hide_my_pdosi">
                                                </tr>
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserID" Text='<%#Eval("UserID") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblBranchID" Text='<%#Eval("BranchID") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblCompanyID" Text='<%#Eval("CompanyID") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblBranchName" Text='<%#Eval("BranchName") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblDeptName" Text='<%#Eval("DeptName") %>' runat="server"></asp:Label>


                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubDeptName" Text='<%#Eval("SubDeptName") %>' runat="server"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" Text='<%#Eval("UserName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobileNo" Text='<%#Eval("UserMobileNo") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmailAddress" Text='<%#Eval("UserEmailAddr") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation" Text='<%#Eval("UserDesignation") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateOfExpiry" Text='<%#Eval("ExpirtDate") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" Text='<%#Eval("IsActive") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="c">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAction" CommandName="Action" CommandArgument='<%#Container.DataItemIndex %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#1c75bf" ForeColor="White" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>


                    <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
                        <div class="panel panel-primary bodyContent" style="width: 34%; padding: 0">
                            <div class="panel-heading" style="background-color: #27c24c">
                                <i class="fa fa-check-circle fa-lg">

                                    <asp:Label ID="lblPopUpMsg" Font-Size="15px" runat="server"> </asp:Label>
                                </i>

                            </div>



                            <div class="panel-body">
                                <div class="mb10">

                                    <i class="fa fa-question-circle" style="font-size: 18px; padding-top: 7px;">You want to give the User Rights of this user.</i>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="text-right">


                                    <asp:Button ID="btnYes" OnClick="btnYes_Click" CssClass="btn btn-primary" Text="Yes" runat="server" />
                                    <asp:Button ID="btnNo" OnClick="btnNo_Click" CssClass="btn btn-danger" Text="No" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
