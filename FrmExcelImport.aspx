<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmExcelImport.aspx.cs" Inherits="AdminMasters_FrmExcelImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section>
        <div class="content-wrapper">
            <%-- <cc1:UserControl runat="server" ID="wucTaxPayerStrip" />--%>
            <div class="modal fade" id="ProgressModal" role="dialog">
                <div class="modal-dialog modal-sm" style="width: 235px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Processing Please Wait.</h4>
                        </div>
                        <div class="modal-body">
                            <img style="height: 85px;" src="../Content/img/loader4.gif" />
                        </div>
                        <div class="modal-footer">
                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                        </div>
                    </div>
                </div>
            </div>
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <style>
                                .ctrl_label {
                                    margin-right: 10px;
                                    cursor: pointer;
                                }
                            </style>
                            <div class="col-xs-12">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-sm-3 control-label mb15" style="text-align: left">Select Company Name:</label>
                                        <div class="col-xs-12 col-sm-4 mb15">
                                            <asp:DropDownList ID="ddlCompanyname" CssClass="form-control" OnSelectedIndexChanged="ddlCompanyname_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1 lbl">
                                            <asp:Label ID="lblCompanyId" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-sm-3 control-label mb15" style="text-align: left">Select Excel Type :</label>
                                        <div class="col-xs-12 col-sm-8 mb15">
                                            <label class="control-label ctrl_label" for="ContentPlaceHolder1_RbtAccMaster">
                                                <asp:RadioButton ID="RbtAccMaster" runat="server" Checked="true" GroupName="a" Text="" />Account Master Excel</label>
                                            <label class="control-label ctrl_label" for="ContentPlaceHolder1_RbtItemMaster">
                                                <asp:RadioButton ID="RbtItemMaster" runat="server" GroupName="a" Text="" />Item Master Excel</label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-12 col-sm-3 control-label mb15" style="text-align: left">Select Excel File :</label>
                                        <div class="col-xs-12 col-sm-4 mb15">
                                            <asp:FileUpload accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel,text/comma-separated-values, text/csv, application/csv" runat="server" ID="fileExcel" AllowMultiple="false" CssClass="form-control" />
                                            <asp:Label runat="server" ID="lblmsg" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="col-xs-12 col-sm-5 mb15">
                                            <asp:Button ID="btnImport" OnClick="btnImport_Click" OnClientClick="return ShowPopup();" CssClass="btn-primary btn" runat="server" Text="Import" />
                                            <%-- <asp:Button ID="btnClear" OnClick="btnClear_Click" class="btn-info btn" runat="server" Text="Clear" />
                                    <asp:Button ID="btnExit" OnClick="btnExit_Click" class="btn-danger btn" runat="server" Text="Exit" />--%>
                                            <%-- <asp:Button ID="BtnExcelMapping"  class="btn-info btn" OnClick="BtnExcelMapping_Click" runat="server" Text="Excel Mapping" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
        </div>
    </section>
</asp:Content>

