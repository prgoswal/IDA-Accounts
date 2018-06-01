<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="SecondfrmBudgetAmount.aspx.cs" Inherits="BudgetTransaction_frmBudgetAmount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
            LoadBasic();
            closeXd();
            onLoad();
            ChoosenDDL();
        }

    </script>

    <script type="text/javascript">
        function onLoad() {
            var $rows = $('#ContentPlaceHolder1_grdScheme tr[class!="header"]');
            $('#ContentPlaceHolder1_txtfilter').keyup(function () {
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

                $rows.show().filter(function () {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    return ! ~text.indexOf(val);
                }).hide();
            });
        }

        function closeXd() {
            $('.xd-close').click(function () {
                $('.modalPop').css('display', 'none');
                $('.xd-search-box').val('')
            })
        }

        function EndRequest(sender, args) {
            onLoad();
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
        //For ListBox FIlter
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">Budget Amount (Sub-Section Wise)2018-2019
                </h3>
                <style>
                    .text-black {
                        color: #000;
                    }

                    .table-pd-47 tr td, .table-pd-47 tr th {
                        padding: 2px;
                        border: 0 !important;
                    }

                        .table-pd-47 tr td input {
                            padding-left: 4px;
                        }

                    .plr0 {
                        padding-left: 0;
                        padding-right: 0;
                    }

                    .input-sm-tr td input[type="text"] {
                        padding: 2px 3px !important;
                        height: 32px;
                    }

                    .pt4 {
                        padding-top: 4px;
                    }

                    .brk-all {
                        word-break: break-all;
                    }

                    .font-12 {
                        font-size: 12px;
                    }

                    .pd-sm tr td, .pd-sm tr th {
                        padding: 2px 4px !important;
                    }

                    .pos-rel {
                        position: relative;
                    }

                    .xd-search-icon {
                        position: absolute;
                        right: 60px;
                        top: 15px;
                        color: #d0c0c9;
                    }

                    .p0 {
                        padding: 0px !important;
                    }

                    .xd-search-box {
                        float: right;
                        width: 260px;
                        height: 25px;
                        padding: 2px 4px;
                        margin-right: 35px;
                        color: #000;
                    }

                    .xd-close {
                        display: block;
                        border-radius: 100%;
                        background: #ff0000;
                        height: 20px;
                        width: 20px;
                        line-height: 20px;
                        text-align: center;
                        font-size: 16px;
                        position: absolute;
                        right: 10px;
                        top: 12px;
                        font-weight: bold;
                        user-select: none;
                        cursor: pointer;
                        transition: all 0.2s ease;
                        opacity: 0.7;
                        color: #fff;
                    }

                        .xd-close:hover {
                            opacity: 1;
                        }
                </style>
                <div class="container_fluid">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel-body plr0 pt4">
                                    <div class="">
                                        <table class="table-sm table-bordered table-pd-47" style="border: 0">
                                            <tr class="min-height-60">
                                                <td colspan="6">

                                                    <table class="table-sm table-bordered table-pd-47" style="border: 0">
                                                        <tr class="min-height-60">
                                                            <td class="b0 text-bold text-left" style="width: 15%">Sub-Section </td>
                                                            <td class="b0 align_with_combobox" style="width: 35%">

                                                                <asp:TextBox ID="txtSectionName" onfocusin="Filter();" onkeyup="FilterSection()" placeholder="Sub-Section" CssClass="FilterAccountHead form-control" AutoPostBack="true" MaxLength="50" runat="server" OnTextChanged="txtSectionName_TextChanged" />


                                                                <asp:DropDownList ID="ddlNewSubSection" onfocusin="Filter();" onkeyup="FilterSection()" OnSelectedIndexChanged="ddlNewSubSection_SelectedIndexChanged" CssClass="chzn-select pull-left" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                            </td>

                                                            <td class="b0 text-bold text-right" style="width: 20%; display: none;">Sub-Section Name</td>
                                                            <td class="b0 align_with_combobox" style="width: 30%; display: none;">

                                                                <asp:DropDownList ID="ddlSubSection" OnSelectedIndexChanged="ddlSubSection_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td class="b0 p0"></td>
                                                            <td colspan="3" class="b0 p0" style="padding-left: 15px!important">
                                                                <asp:Label ID="lblSectionNameHindi" Style="padding-left: 15px!important" runat="server"></asp:Label></td>
                                                        </tr>


                                                        <tr>
                                                            <td class="b0 text-bold text-left">Cost&nbsp;Centre&nbsp; </td>
                                                            <td colspan="3">

                                                                <asp:TextBox ID="txtSubSection" onfocusin="Filter();" onkeyup="FilterSubsection()" placeholder="Cost Centre " CssClass="FilterAccountHead form-control" MaxLength="50" AutoPostBack="true" runat="server" OnTextChanged="txtSubSection_TextChanged" />

                                                                <asp:DropDownList ID="ddlNewCostCentre" onfocusin="Filter();" onkeyup="FilterSubsection()" OnSelectedIndexChanged="ddlNewCostCentre_SelectedIndexChanged" CssClass="chzn-select pull-left" runat="server" AutoPostBack="true"></asp:DropDownList>


                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td class="b0 p0"></td>
                                                            <td colspan="3" class="b0 p0" style="padding-left: 15px!important">
                                                                <asp:Label ID="lblSubSectionNameHindi" Style="padding-left: 15px!important" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="b0 text-bold text-left">Budget Head </td>
                                                            <td colspan="3">

                                                                <asp:TextBox ID="txtBudgetHead" onfocusin="Filter();" onkeyup="FilterBudgetHead()" placeholder="Budget Head Name" CssClass="FilterAccountHead form-control" MaxLength="50" AutoPostBack="true" runat="server" OnTextChanged="txtBudgetHead_TextChanged" />
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td class="b0"></td>
                                                            <td colspan="3" class="b0 p0" style="padding-left: 15px!important">
                                                                <asp:Label ID="lblBudgetHead" Style="padding-left: 15px!important" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>

                                            </tr>
                                            <tr style="height: 1px;">
                                                <td class="b0"></td>
                                                <td class="b0" style="width: 100px"></td>
                                                <td colspan="5"></td>
                                            </tr>

                                            <tr class="inf_head">
                                                <th style="width: 43%" colspan="2"></th>
                                                <th style="width: 15%">Dr Amount</th>
                                                <th style="width: 15%">Cr Amount</th>
                                                <th colspan="2">Rs. (<asp:Label ID="lblBudgetAmt" runat="server"></asp:Label>) </th>
                                            </tr>


                                            <tr class="input-sm-tr">
                                                <%--<td>Sanctioned Budget Amount 2016-2017</td>--%>
                                                <%--<td>Proposed For 2018-2019</td>--%>
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
                                                <%--<td>Proposed Budget Amount 2017-2018</td>--%>
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropBudgetAmount1718" runat="server"></asp:Label></td>

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
                                                <%--<td>Actual Amount 2015-2016</td>--%>

                                                <%--<td>Actual Up to Dec 2017</td>--%>

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
                                                <%--<td>Proposed Budget Amount 2016-2017</td>--%>
                                                <%--<td>Proposed For Quater (Jan-Mar)</td>--%>
                                                <td colspan="2">
                                                    <asp:Label class="alphaonly" ID="lblPropQuaAmt" runat="server"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox Enabled="false" runat="server" ID="txtPropLastQtrBudgetAmtDr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" onkeyup="sum();" />
                                                    <%--  OnTextChanged="txtPropLastQtrBudgetAmtDr_TextChanged" AutoPostBack="true"--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox Enabled="false" runat="server" ID="txtPropLastQtrBudgetAmtCr" MaxLength="8" CssClass="inpt Money form-control form-control-sm" onkeyup="sumCr();" />
                                                    <%--OnTextChanged="txtPropLastQtrBudgetAmtCr_TextChanged" AutoPostBack="true"--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPropLastQtrBudgetAmtDr" Visible="false" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblPropLastQtrBudgetAmtCr" Visible="false" runat="server"></asp:Label></td>
                                            </tr>




                                            <tr id="trRevBudgetAmt1718" runat="server" class="input-sm-tr" style="background: #ffdd9e; border-bottom: 1px solid #fff">
                                                <%--<td>Actual Amount 2016-2017</td>--%>
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
                                            <b style="font-size: 15px; padding: 5px; display: block">Sub-Section List
                                                <asp:LinkButton ID="lnkbtnShowScheme" runat="server" OnClick="lnkbtnShowScheme_Click" Style="padding: 50px;">Search Scheme</asp:LinkButton>
                                            </b>
                                            <asp:ListBox ID="lstSection" AutoPostBack="true" OnSelectedIndexChanged="lstSection_SelectedIndexChanged" runat="server" Style="color: #000; width: 100%; height: 500px; background: #9ccef9; top: 0px;"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" id="divListSubSection" runat="server" visible="false">
                                        <div class="row" style="background: #fff;">
                                            <b style="font-size: 15px; padding: 5px; display: block">Cost Centre List </b>
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


                        <asp:Panel runat="server" ID="pnlConfirmInvoice" CssClass="modalPop" Visible="false" Style="position: absolute; left: 0; right: 0">
                            <div class="panel panel-primary" style="width: 100%; max-width: 990px; margin: 0 auto">
                                <div class="panel-heading pos-rel" style="background-color: #27c24c;">
                                    Search Scheme     
                                    <asp:TextBox ID="txtfilter" runat="server" placeholder="Search..." CssClass="search-filter form-control xd-search-box"></asp:TextBox><i class="fa fa-search xd-search-icon"></i>
                                    <span class="xd-close">&times;</span>
                                </div>
                                <div class="panel-body p0">
                                    <div style="padding-right: 17px">
                                        <table class="table table-bordered brk-all font-12 pd-sm">
                                            <tr class="inf_head">
                                                <th style="width: 20%">Department</th>
                                                <th style="width: 20%">Sub-Department</th>
                                                <th style="width: 30%">Scheme Name</th>
                                                <th style="width: 30%">Scheme Hindi Name</th>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="mb10" style="max-height: 400px; overflow-y: scroll">
                                        <asp:GridView ID="grdScheme" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered  brk-all font-12 pd-sm" ShowHeader="false">
                                            <Columns>

                                                <asp:TemplateField ItemStyle-CssClass="c" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubDeptName" Text='<%#Eval("DepartmentName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="c" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserName" Text='<%#Eval("SubDepartmentName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNo" Text='<%#Eval("SchemeName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailAddress" Text='<%#Eval("SchemeHindiName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>


                </div>
        </ContentTemplate>
        <%--    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>

