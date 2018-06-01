<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBudgetAmtTransaction.aspx.cs" Inherits="BudgetTransaction_frmBudgetAmtTransaction" %>

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

        function sum() {
            var txtFirstNumberValue = document.getElementById('ContentPlaceHolder1_txtActualUptoBudgetAmtDr').value;
            var txtSecondNumberValue = document.getElementById('ContentPlaceHolder1_txtPropLastQtrBudgetAmtDr').value;
            if (txtFirstNumberValue == "")
                txtFirstNumberValue = 0;
            if (txtSecondNumberValue == "")
                txtSecondNumberValue = 0;

            //var result = Math.round(parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue)).toFixed(2);

            var result = (parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue)).toFixed(2);

            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_txtSanc2BudgetAmtDr').value = result;
            }
        }

        function sumCr() {
            var txtFirstNumberValue = document.getElementById('ContentPlaceHolder1_txtActualUptoBudgetAmtCr').value;
            var txtSecondNumberValue = document.getElementById('ContentPlaceHolder1_txtPropLastQtrBudgetAmtCr').value;
            if (txtFirstNumberValue == "")
                txtFirstNumberValue = 0;
            if (txtSecondNumberValue == "")
                txtSecondNumberValue = 0;

            var result = (parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue)).toFixed(2);
            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_txtSanc2BudgetAmtCr').value = result;
            }
        }

        function SumPropBudgetTotalAmtDr() {
            var txtFirstNumberValue = document.getElementById('ContentPlaceHolder1_txtPropBudgetCapitalAmtDr').value;
            var txtSecondNumberValue = document.getElementById('ContentPlaceHolder1_txtPropBudgetRevenueAmtDr').value;
            if (txtFirstNumberValue == "")
                txtFirstNumberValue = 0;
            if (txtSecondNumberValue == "")
                txtSecondNumberValue = 0;

            var result = (parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue)).toFixed(2);
            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_txtPropBudgetTotalAmtDr').value = result;
            }
        }

        function SumPropBudgetTotalAmtCr() {
            var txtFirstNumberValue = document.getElementById('ContentPlaceHolder1_txtPropBudgetCapitalAmtCr').value;
            var txtSecondNumberValue = document.getElementById('ContentPlaceHolder1_txtPropBudgetRevenueAmtCr').value;
            if (txtFirstNumberValue == "")
                txtFirstNumberValue = 0;
            if (txtSecondNumberValue == "")
                txtSecondNumberValue = 0;

            var result = (parseFloat(txtFirstNumberValue) + parseFloat(txtSecondNumberValue)).toFixed(2);
            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_txtPropBudgetTotalAmtCr').value = result;
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



                                            <tr class="input-sm-tr">
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropAmt1819" runat="server"></asp:Label>
                                                    <span style="font-weight: bold; float: right">Capital :</span></td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtPropBudgetCapitalAmtDr" onkeyup="SumPropBudgetTotalAmtDr();" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />


                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtPropBudgetCapitalAmtCr" onkeyup="SumPropBudgetTotalAmtCr();" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />

                                                </td>

                                                <td>
                                                    <asp:Label ID="lblPropBudgetAmtDr" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblPropBudgetAmtCr" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr class="input-sm-tr">
                                                <td colspan="2">
                                                    <span style="font-weight: bold; float: right">Revenue :</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtPropBudgetRevenueAmtDr" onkeyup="SumPropBudgetTotalAmtDr();" MaxLength="8" CssClass="inpt Money form-control form-control-sm" /></td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtPropBudgetRevenueAmtCr" onkeyup="SumPropBudgetTotalAmtCr();" MaxLength="8" CssClass="inpt Money form-control form-control-sm" /></td>
                                                <td>
                                                    <asp:Label ID="lblPropBudgetRevenueAmtDr" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblPropBudgetRevenueAmtCr" Visible="false" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr class="input-sm-tr">
                                                <td colspan="2"><span style="font-weight: bold; float: right">Total :</span></td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtPropBudgetTotalAmtDr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" /></td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtPropBudgetTotalAmtCr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" /></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr id="trPropBudgetAmount1718" runat="server" class="input-sm-tr" style="background: #fff0d5; border-bottom: 1px solid #fff">

                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropBudgetAmount1718new" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProp2BudgetAmtDr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtProp2BudgetAmtCr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProp2BudgetAmtDr" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblProp2BudgetAmtCr" Visible="false" runat="server"></asp:Label></td>
                                            </tr>

                                            <tr class="input-sm-tr" style="background: #fff0d5; border-bottom: 1px solid #fff">


                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblActualUpto17" runat="server"></asp:Label></td>

                                                <td>


                                                    <asp:TextBox ID="txtActualUptoBudgetAmtDr" onkeyup="sum();" Enabled="false" MaxLength="8" CssClass="Money form-control form-control-sm" runat="server" />


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtActualUptoBudgetAmtCr" onkeyup="sumCr();" Enabled="false" MaxLength="8" CssClass="inpt Money form-control form-control-sm" runat="server" />



                                                </td>
                                                <td style="width: 15%">
                                                    <asp:Label ID="lblActualUptoBudgetAmtDr" Visible="false" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:Label ID="lblActualUptoBudgetAmtCr" Visible="false" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="input-sm-tr" style="background: #fff0d5; border-bottom: 1px solid #fff">

                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropQuaAmt" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox Enabled="false" runat="server" ID="txtPropLastQtrBudgetAmtDr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" onkeyup="sum();" />
                                                </td>
                                                <td>
                                                    <asp:TextBox Enabled="false" runat="server" ID="txtPropLastQtrBudgetAmtCr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" onkeyup="sumCr();" />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPropLastQtrBudgetAmtDr" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblPropLastQtrBudgetAmtCr" Visible="false" runat="server"></asp:Label></td>
                                            </tr>




                                            <tr id="trRevBudgetAmt1718" runat="server" class="input-sm-tr" style="background: #ffdd9e; border-bottom: 1px solid #fff">

                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblRevBudgetAmt1718" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtSanc2BudgetAmtDr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Enabled="false" ID="txtSanc2BudgetAmtCr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSanc2BudgetAmtDr" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblSanc2BudgetAmtCr" Visible="false" runat="server"></asp:Label></td>
                                            </tr>

                                            <%---------------------------------------------------------------------------------------------------------------------------------%>
                                        
                                          <%--      <tr>
                                                    <td colspan="2">
                                                        <asp:Label class="alphaonly" ID="lblActualAmt1516" runat="server"></asp:Label></td>

                                                    <td>
                                                        <asp:TextBox ID="txtAcutal3DrAmt" MaxLength="8" CssClass="Money form-control form-control-sm" runat="server" Enabled="false" />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAcutal3CrAmt" Enabled="false" MaxLength="8" CssClass="inpt Money form-control form-control-sm" runat="server" />

                                                    </td>
                                                    <td style="width: 15%">
                                                        <asp:Label ID="lblAcutal3DrAmt" Visible="false" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 15%">
                                                        <asp:Label ID="lblAcutal3CrAmt" Visible="false" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
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
                                                </tr>--%>

                                          
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


