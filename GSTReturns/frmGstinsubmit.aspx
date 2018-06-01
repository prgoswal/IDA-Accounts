<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmGstinsubmit.aspx.cs" Inherits="frmGstinsubmit" Culture="hi-IN" %>

<%@ Register Src="~/GSTReturns/frmGSTR1.ascx" TagPrefix="uc1" TagName="frmGSTR1" %>
<%@ Register Src="~/GSTReturns/frmGSTR2.ascx" TagPrefix="uc1" TagName="frmGSTR2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .detail {
            height: 50px;
            background-color: #fff;
            margin-top: -16px;
            margin-left: -16px;
            margin-right: -16px;
            padding-top: 15px;
            padding-right: 15px;
            width: calc(100% + 32px);
        }
    </style>
    <div class="content-wrapper">
        <h4 class="text-center text-black detail">GSTIN No:&nbsp<asp:Label ID="lblGstin" runat="server" />
            &nbsp; &nbsp;<b>Address:&nbsp;<asp:Label ID="lblAddress" runat="server" />
            </b></h4>
        <style>
            .duelist {
                width: 100%;
            }

            .duelist_col1 {
                width: 10%;
            }

            .duelist_col2 {
                width: 30%;
            }

            .duelist_col3 {
                width: 20%;
            }

            .duelist_col4 {
                width: 20%;
            }

            .duelist_col5 {
                width: 20%;
            }

            .Landscape {
                width: 100%;
                height: 100%;
                margin: 0% 0% 0% 0%;
                filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=1);
            }

            .font12 {
                font-size: 12px;
            }

            .btn-sxs1 {
                padding: 2px 10px;
            }
        </style>
        <div class="container_fluid">
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body Landscape" id="pnl_Priview">

                        <div class="row">
                            <div class="col-xs-12">
                                <table class="pdf_show mrbotm40 font12" style="width: 100%; margin-bottom: 10px">
                                    <tbody>
                                        <tr>
                                            <td colspan="100%" style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblDate" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5%; text-align: right; padding-right: 3px"><b>Month</b></td>
                                            <td style="width: 10%">
                                                <asp:DropDownList ID="ddlMonth" runat="server">
                                                    <asp:ListItem Value="0" Text="--Select--" />
                                                    <%-- <asp:ListItem Value="1" Text="January" />
                                                        <asp:ListItem Value="2" Text="February" />
                                                        <asp:ListItem Value="3" Text="March" />
                                                        <asp:ListItem Value="4" Text="April" />
                                                        <asp:ListItem Value="5" Text="May" />
                                                        <asp:ListItem Value="6" Text="June" />--%>
                                                    <asp:ListItem Value="7" Text="July" />
                                                    <asp:ListItem Value="8" Text="August" />
                                                    <asp:ListItem Value="9" Text="September" />
                                                    <asp:ListItem Value="10" Text="October" />
                                                             <%--                  <asp:ListItem Value="11" Text="November" />
                                                                        <asp:ListItem Value="12" Text="December" />--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 5%; text-align: right; padding-right: 3px"><b>Year</b></td>
                                            <td style="width: 10%">
                                                <asp:DropDownList ID="ddlYear" runat="server">
                                                    <asp:ListItem Value="17" Text="2017" />
                                                    <%-- <asp:ListItem Value="0" Text="--Select--" />
                                                         <asp:ListItem Value="18" Text="2018" />
                                                         <asp:ListItem Value="19" Text="2019" />
                                                         <asp:ListItem Value="20" Text="2020" />--%>
                                                </asp:DropDownList>
                                            </td>
                                            <%--<td style="width: 7%; text-align: right; padding-right: 3px"><b>GSTIN</b></td>
                                            <td style="width: 11%;"><b>
                                                <asp:DropDownList runat="server" ID="ddlGstin">
                                                </asp:DropDownList></b></td>
                                                
                                                <td style="width: 20%; padding-left: 5px">
                                                <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sxs1 hidden-print" Text="Go" runat="server" />
                                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info btn-sxs1 hidden-print" Text="Print" OnClientClick="return PrintPanel();" />
                                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger  btn-sxs1 hidden-print" runat="server" />
                                            </td>--%>
                                            <td style="width: 50%; text-align: right; padding-right: 3px"><b>
                                                <asp:Label ID="lblCompanyName" runat="server"></asp:Label></b> </td>
                                        </tr>
                                        <tr>
                                            <td colspan="100%" style="text-align: right; position: relative">
                                                <small style="position: absolute; margin-top: -7px; right: 0">Amount in Rs.</small>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class=" col-sm-8 col-sm-offset-2">
            <div class="panel panel-default">
                <div class="panel-body">
                    <table class="table-bordered text-black text-bold duelist">
                        <thead style="background: #1c75bf; color: #fff">
                            <tr>
                                <th class="duelist_col1">S No.</th>
                                <th class="duelist_col2">Returns</th>
                                <th class="duelist_co3">Due Date</th>
                                <th class="duelist_col4">Submit</th>
                                <th class="duelist_col5">Summary</th>
                                <th class="duelist_col5">Generate Excel</th>
                            </tr>
                        </thead>
                        <tbody class="text-center">
                            <tr>
                                <td class="duelist_col1">1</td>
                                <td class="duelist_col2">GSTR1</td>
                                <td class="duelist_col3">Upto 10/10/2017</td>
                                <td class="duelist_col4">
                                    <asp:Button Enabled="false" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-sxs1 btn-primary" Text="Submit" /></td>
                                <td class="duelist_col5">
                                    <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" CssClass="btn btn-sxs1 btn-info" Text="View" /></td>
                                <td class="duelist_col6">
                                    <asp:LinkButton ID="btnExportExcel" runat="server" OnClick="btnExportExcel_Click" CssClass="btn btn-sxs1 btn-default">Excel <i class="fa fa-file-excel-o"></i></asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td class="duelist_col1">2</td>
                                <td class="duelist_col2">GSTR2</td>
                                <td class="duelist_col3">Upto 10/09/2017</td>
                                <td class="duelist_col4">
                                    <asp:Button ID="btnGST2Submit" OnClick="btnGST2Submit_Click" runat="server" CssClass="btn btn-sxs1 btn-primary" Text="Submit" /></td>
                                <%--OnClick="btnGST2Submit_Click" --%>
                                <td class="duelist_col5">
                                    <asp:Button ID="btnGST2View" runat="server" OnClick="btnGST2View_Click" CssClass="btn btn-sxs1 btn-info" Text="View" /></td>
                                <td class="duelist_col6">
                                    <asp:LinkButton ID="btnGST2Excel" runat="server" CssClass="btn btn-sxs1 btn-default">Excel <i class="fa fa-file-excel-o"></i></asp:LinkButton></td>
                                <%--OnClick="btnGST2Excel_Click" --%>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="panel-footer">
                    <div class="panel-body">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>

            </div>

        </div>
        <div id="divgrid" runat="server" class="col-sm-12 " style="overflow: auto; max-height: 290px; min-height: 0px; background-color: white; color: black; margin-bottom: 50px">
            <span class="modalRight">
                        <button class="btn btn-default" onclick="GenerateExcel();">
                            Import Excel
                            <span class="fa fa-file-excel-o"></span>
                        </button>
                    </span>
            <asp:GridView  ID="gridGstinSubmit" Style="margin: 5px; font-size: 10px;" HeaderStyle-BackColor="#1c75bf" HeaderStyle-ForeColor="White" runat="server" CssClass="table table-responsive table-bordered" AutoGenerateColumns="true"></asp:GridView>
            
        </div>

    </div>
    <asp:Panel ID="pnlErrorValidity" Visible="false" runat="server">
        <div class="modalPop">
            <div class="modalDialog">
                <div class="modalHeader" id="ExcelHeader">
                    <div class="modalClose">
                        <asp:LinkButton ID="btnErrorClose" OnClick="btnErrorClose_Click" runat="server"><span class="fa fa-times-circle-o" ></span></asp:LinkButton>
                    </div>
                        <div class="bodyContent">
                        <asp:GridView ID="gvErrorValidity" CssClass="table2excel table table-bordered table-hover" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <%--<asp:BoundField DataField="ErrorCode" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <th>ErrorDesc</th>
                                                <th>ClientGSTNNo</th>
                                                <th>LineNo</th>
                                                <th>CurrInvoiceNo</th>
                                                <th>InvoiceDate</th>
                                                <th>TotInvoiceValue</th>
                                            </tr>
                                            <td><asp:Label Text='<%#Eval("ErrorDesc") %>' runat="server" /></td>
                                            <td><asp:Label Text='<%#Eval("ClientGSTNNo") %>' runat="server" /></td>
                                            <td><asp:Label Text='<%#Eval("LineNo") %>' runat="server" /></td>
                                            <td><asp:Label Text='<%#Eval("CurrInvoiceNo") %>' runat="server" /></td>
                                            <td><asp:Label Text='<%#Eval("InvoiceDate") %>' runat="server" /></td>
                                            <td><asp:Label Text='<%#Eval("TotInvoiceValue") %>' runat="server" /></td>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:BoundField DataField="ErrorDesc" />
                                <asp:BoundField DataField="ClientGSTNNo" />
                                <asp:BoundField DataField="LineNo" />
                                <asp:BoundField DataField="CurrInvoiceNo" />
                                <asp:BoundField DataField="InvoiceDate" />
                                <asp:BoundField DataField="TotInvoiceValue" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <script src="../Content/js/jquery.table2excel.js"></script>
            <script>
                function GenerateExcel() { //// Generate Excel File From Grid.
                    var FileName = "ErrorValidity";
                    //var headerText = CreateHead($(".table2excel"));                    

                    $(".table2excel").table2excel({
                        exclude: ".noExl",
                        name: "Excel Document Name",
                        filename: FileName,
                        fileext: ".xls",
                        exclude_img: true,
                        exclude_links: true,
                        exclude_inputs: true
                    });
                }
            </script>
    </asp:Panel>
    <uc1:frmGSTR1 ID="frmGSTR1" runat="server" />
    <uc1:frmGSTR2 ID="frmGSTR2" runat="server" />
</asp:Content>

