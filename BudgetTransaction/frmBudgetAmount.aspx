<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBudgetAmount.aspx.cs" Inherits="BudgetTransaction_frmBudgetAmount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();
        }

    </script>

    <script> //For ListBox FIlter
        function FilterSection() {
            debugger;
            var input, filter, listBox, option, a, i;
            input = document.getElementById("<%=txtSectionName.ClientID%>");

            filter = input.value.toUpperCase();

            listBox = document.getElementById('<%=lstSection.ClientID %>');

            option = listBox.getElementsByTagName("option");

            for (i = 0; i < option.length; i++) {
                a = option[i];

                if (a.value.toUpperCase().indexOf(filter) > -1) {
                    option[i].style.display = "";
                } else {
                    option[i].style.display = "none";

                }
            }
        }
    </script>


    <script> //For ListBox FIlter
        function FilterSubsection() {
            debugger;
            var input, filter, listBox, option, a, i;
            input = document.getElementById("<%=txtSubSection.ClientID%>");

            filter = input.value.toUpperCase();

            listBox = document.getElementById('<%=lstSubSection.ClientID %>');

            option = listBox.getElementsByTagName("option");

            for (i = 0; i < option.length; i++) {
                a = option[i];

                if (a.value.toUpperCase().indexOf(filter) > -1) {
                    option[i].style.display = "";
                } else {
                    option[i].style.display = "none";

                }
            }
        }
    </script>

    <script> //For ListBox FIlter
        function FilterBudgetHead() {
            debugger;
            var input, filter, listBox, option, a, i;
            input = document.getElementById("<%=txtBudgetHead.ClientID%>");

            filter = input.value.toUpperCase();

            listBox = document.getElementById('<%=lstBudgetHead.ClientID %>');

            option = listBox.getElementsByTagName("option");

            for (i = 0; i < option.length; i++) {
                a = option[i];

                if (a.value.toUpperCase().indexOf(filter) > -1) {
                    option[i].style.display = "";
                } else {
                    option[i].style.display = "none";

                }
            }
        }
    </script>

    <%--   <script>
        function myFunction() {
           //alert(1);
            $("#divListSection").show();
            $("#divListSubSection").hide();
            $("#divListBudgetHead").hide();

            //$("#divListSection").css("display", "");
            //$("#divListSubSection").css("display", "none");
            //$("#divListBudgetHead").css("display", "none");

        }

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">Budget Amount (Sub-Section Wise)
                </h3>
                <style>
                    .table-pd-47 tr td, .table-pd-47 tr th {
                        padding: 4px 7px;
                        border: 0 !important;
                    }

                    .plr0 {
                        padding-left: 0;
                        padding-right: 0;
                    }
                </style>
                <div class="container_fluid">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel-body plr0">
                                    <div class="">
                                        <table class="table-sm table-bordered table-pd-47" style="border: 0">
                                            <tr class="min-height-60">
                                                <td class="b0 text-bold">Section Name </td>
                                                <td colspan="5" class="b0 align_with_combobox">
                                                    <%--<asp:DropDownList ID="ddlSection" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" CssClass="mtb10 form-control" runat="server"></asp:DropDownList>--%>

                                                    <cc1:ComboBox ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" Width="20px" placeholder="p" OnTextChanged="ddlSection_TextChanged" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" Visible="false" />

                                                    <asp:TextBox ID="txtSectionName" onfocusin="Filter();" onkeyup="FilterSection()" placeholder="Section Name" CssClass="FilterAccountHead form-control" AutoPostBack="true" MaxLength="50" runat="server" OnTextChanged="txtSectionName_TextChanged" />





                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="b0 p0"></td>
                                                <td colspan="5" class="b0 p0" style="padding-left: 15px!important">
                                                    <asp:Label ID="lblSectionNameHindi" Style="padding-left: 15px!important" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="b0 text-bold">Sub-Section Name </td>
                                                <td colspan="5">
                                                    <%--<asp:DropDownList ID="ddlSubSection" OnSelectedIndexChanged="ddlSubSection_SelectedIndexChanged" AutoPostBack="true" CssClass="mtb10 form-control" runat="server"></asp:DropDownList>--%>


                                                    <cc1:ComboBox ID="ddlSubSection" runat="server" OnSelectedIndexChanged="ddlSubSection_SelectedIndexChanged" AutoPostBack="true" Width="20px" placeholder="p" CssClass="relative_gt" AutoCompleteMode="SuggestAppend" CaseSensitive="False" Style="text-transform: uppercase" Visible="false" />



                                                    <asp:TextBox ID="txtSubSection" onfocusin="Filter();" onkeyup="FilterSubsection()" placeholder="Sub-Section Name" CssClass="FilterAccountHead form-control" MaxLength="50" AutoPostBack="true" runat="server" OnTextChanged="txtSubSection_TextChanged" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="b0 p0"></td>
                                                <td colspan="5" class="b0 p0" style="padding-left: 15px!important">
                                                    <asp:Label ID="lblSubSectionNameHindi" Style="padding-left: 15px!important" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="b0 text-bold">Budget Head </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlBudgetHead" OnSelectedIndexChanged="ddlBudgetHead_SelectedIndexChanged" AutoPostBack="true" CssClass="mtb10 form-control" runat="server" Visible="false"></asp:DropDownList>


                                                    <asp:TextBox ID="txtBudgetHead" onfocusin="Filter();" onkeyup="FilterBudgetHead()" placeholder="Budget Head Name" CssClass="FilterAccountHead form-control" MaxLength="50" AutoPostBack="true" runat="server" OnTextChanged="txtBudgetHead_TextChanged" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="b0"></td>
                                                <td colspan="5" class="b0 p0" style="padding-left: 15px!important">
                                                    <asp:Label ID="lblBudgetHead" Style="padding-left: 15px!important" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr style="height: 1px;">
                                                <td class="b0"></td>
                                                <td class="b0" style="width: 100px"></td>
                                                <td colspan="5"></td>
                                            </tr>

                                            <tr class="inf_head">
                                                <th style="width: 40%" colspan="2"></th>
                                                <th style="width: 15%">Dr Amount</th>
                                                <th style="width: 15%">Cr Amount</th>
                                                <th colspan="2">Rs. (<asp:Label ID="lblBudgetAmt" runat="server"></asp:Label>) </th>
                                            </tr>
                                            <tr>
                                                <%--<td>Actual Amount 2015-2016</td>--%>

                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblActualAmt1516" runat="server"></asp:Label></td>

                                                <td>


                                                    <asp:TextBox ID="txtAcutal3DrAmt" MaxLength="8" CssClass="Money form-control form-control-sm" runat="server" Enabled="false" />
                                                    <%--<asp:RegularExpressionValidator ID="revDecimals" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtAcutal3DrAmt" Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>--%>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAcutal3CrAmt" Enabled="false" MaxLength="8" CssClass="inpt Money form-control form-control-sm" runat="server" />

                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtAcutal3CrAmt" Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>--%>

                                                </td>
                                                <td style="width: 15%">
                                                    <asp:Label ID="lblAcutal3DrAmt" Visible="false" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:Label ID="lblAcutal3CrAmt" Visible="false" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td>Proposed Budget Amount 2016-2017</td>--%>
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropBudgetAmt1617" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProposed2DrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProposed2CrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProposed2DrAmt" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblProposed2CrAmt" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <%--<td>Sanctioned Budget Amount 2016-2017</td>--%>
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblSancBudgetAmt1617" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtSanctioned2DrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtSanctioned2CrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSanctioned2DrAmt" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblSanctioned2CrAmt" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <%--<td>Actual Amount 2016-2017</td>--%>
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblActualAmt1617" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtActual2DrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtActual2CrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblActual2DrAmt" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblActual2CrAmt" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <%--<td>Proposed Budget Amount 2017-2018</td>--%>
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropBudgetAmount1718" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProposedDrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProposedCrAmt" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProposedDrAmt" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblProposedCrAmt" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <div class="text-right">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-12">

                                                    <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Save" />
                                                    <asp:Button ID="btnclear" CssClass="btn btn-danger" OnClick="btnclear_Click" runat="server" Text="Clear" />


                                                </div>


                                            </div>
                                        </div>
                                    </div>


                                    <%--       <div style="width: 20%;" id="divListSection" onclick="ClickOnSectionDiv();">


                                      

                                    </div>--%>
                                </div>



                            </div>
                        </div>


                        <div class="col-sm-4">
                            <div class="panel panel-default">
                                <div class="panel-body  plr0 p0">

                                    <div class="col-xs-12" id="divListSection" runat="server">
                                        <div class="row" style="background: #fff;">
                                            <b style="font-size: 15px; padding: 5px; display: block">Section List</b>
                                            <asp:ListBox ID="lstSection" AutoPostBack="true" OnSelectedIndexChanged="lstSection_SelectedIndexChanged" runat="server" Style="color: #000; width: 100%; height: 500px; background: #9ccef9; top: 0px;"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" id="divListSubSection" runat="server" visible="false">
                                        <div class="row" style="background: #fff;">
                                            <b style="font-size: 15px; padding: 5px; display: block">Sub-Section List </b>
                                            <asp:ListBox ID="lstSubSection" AutoPostBack="true" OnSelectedIndexChanged="lstSubSection_SelectedIndexChanged" runat="server" Style="color: #000; width: 100%; height: 500px; background: #9ccef9; top: 0px;"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" id="divListBudgetHead" runat="server" visible="false">
                                        <div class="row" style="background: #fff;">
                                            <b style="font-size: 15px; padding: 5px; display: block">Budget Head List </b>
                                            <asp:ListBox ID="lstBudgetHead" AutoPostBack="true" OnSelectedIndexChanged="lstBudgetHead_SelectedIndexChanged" runat="server" Style="color: #000; width: 100%; height: 500px; background: #9ccef9; top: 0px;"></asp:ListBox>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>

                    </div>


                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

