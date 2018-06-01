<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="FrmFDCalculator.aspx.cs" Inherits="FDControl_FrmFDCalculator" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">


        function LoadAllScript() {
            LoadBasic();
        }


        function calculate() {
            debugger;
            // concantenate values to date_start and date_end hidden inputs

            try {
                var p = document.getElementById('<%=txtAmtDeposit.ClientID%>').value;
                var r = document.getElementById('<%=txtROI.ClientID%>').value;
                var dateString = document.getElementById('<%=txtDepositDate.ClientID%>').value;


                var datearray = dateString.split("/");
                var duedate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
                DepositDate = new Date(duedate);
                //alert(DepositDate);

                //   var a = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();
                //alert(a);
                var numDays = document.getElementById('<%=txtDays.ClientID%>').value;
                DepositDate.setDate(DepositDate.getDate() + parseInt(numDays));

                var dateAfterAddDay = DepositDate.getFullYear() + '-' + (DepositDate.getMonth() + 1) + '-' + DepositDate.getDate();
                //alert("dateAfterAddDay:---------" + dateAfterAddDay);
                var numMonths = document.getElementById('<%=txtMonths.ClientID%>').value;
                //alert(numMonths);


                var d = new Date(dateAfterAddDay);
                //alert(d);

                var result1 = d.setMonth(d.getMonth() + numMonths);
                //var result1 = d.setMonth(numMonths);
                alert("result1" + result1);
                var dateAfterAddMonth = new Date(result1);
                //alert(Z);


                var dd = dateAfterAddMonth.getDate();
                var mm = dateAfterAddMonth.getMonth() + 1;
                var y = dateAfterAddMonth.getFullYear();

                var i = '';
                var j = '';


                if (mm.toString().length == 1) {
                    //alert("length =" + mm.toString().length);
                    mm = "0" + mm;
                }

                if (dd.toString().length == 1) {
                    dd = "0" + dd;
                }
                alert("mm" + mm);

                //var result1 = dateEnd.addMonths(2);
                //   alert("After add Month:-------" + j + '/' + i + '/' + y);
                var finalDate = dd + '/' + mm + '/' + y;
                alert("dss" + finalDate);
                document.getElementById("lblDueDate").innerText = finalDate;

                //x.innerText = finalDate;




                //var p = document.getElementById("p").value;
                //var r = document.getElementById("r").value;

                ////var Firstdate = document.getElementById("DueDate").value;
                //var datearray = dateString.split("/");

                //var duedate = dateString[1] + '/' + dateString[0] + '/' + dateString[2];
                //alert(duedate);
                ////var SecondDate = document.getElementById("UptoDate").value;
                ////var datearray = SecondDate.split("/");
                //var uptodate = finalDate[1] + '/' + finalDate[0] + '/' + finalDate[2];

                var date1 = new Date(dateString);
                alert("dateString" + dateString);
                var date2 = new Date(finalDate);
                alert("finalDate" + finalDate);

                var timeDiff = Math.abs(date1.getTime() - date2.getTime());

                var diffDays = (Math.ceil(timeDiff / (1000 * 3600 * 24)));
                var InterestCal = ((p * r * diffDays) / 36500).toFixed(2);;
                alert(InterestCal);
                document.getElementById("lblMaturityValue").innerText = parseInt(parseInt(InterestCal) + parseInt(p));

            } catch (e) {
                alert(e.message);
            }



<%--
            p = document.getElementById('<%=txtAmtDeposit.ClientID%>').value;
            n = document.getElementById('<%=txtMonths.ClientID%>').value;
            r = document.getElementById('<%=txtROI.ClientID%>').value;
            result = document.getElementById("result");--%>

            //alert("The interest is " + (p * n * r / 100));
        }
    </script>

    <style>
        /*error message popup*/
        .gst-ac-error-holder {
            position: relative;
        }

        .gst-ac-error {
            background: #f05050;
            color: #fff;
            padding: 4px;
            border-radius: 4px;
            border: 1px solid #fff;
            position: absolute;
            Z-INDEX: 2;
            top: -40px;
            right: 0;
            box-shadow: 0px 0px 5px 2px rgba(0,0,0,0.5);
        }

            .gst-ac-error::after {
                content: '';
                display: block;
                position: absolute;
                top: 100%;
                left: 50%;
                width: 0;
                Z-INDEX: 2;
                transform: translateX(-50%);
                height: 0;
                border-top: 10px solid #f05050;
                border-right: 10px solid transparent;
                border-bottom: 10px solid transparent;
                border-left: 10px solid transparent;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center head">Fixed Deposit Calculator
        </h3>
        <div class="container_fluid">
            <div class="row">
                <div class="col-sm-offset-3 col-sm-6">
                    <%-- <div class="col-lg-12"> 
                        <div class="col-lg-6">--%>
                    <div class="panel panel-default">
                        <div class="panel-body" style="height: 326px;">

                            <div class="form-horizontal">
                                <div class="row">



                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Deposit Term</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtMonths" MaxLength="3" runat="server" CssClass="form-control numberonly"></asp:TextBox>
                                                <%--Months--%>
                                                <asp:RequiredFieldValidator ID="reqtxtMonths" runat="server" ControlToValidate="txtMonths" ValidationGroup="btn" ErrorMessage="Enter Month" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-sm-4" style="display: none;">

                                                <asp:TextBox ID="txtDays" MaxLength="3" runat="server" CssClass="form-control numberonly"></asp:TextBox>
                                                Days
                                                <asp:RequiredFieldValidator ID="reqtxtDays" runat="server" ControlToValidate="txtDays" ValidationGroup="btn" ErrorMessage="Enter Days" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-sm-4">

                                                <asp:DropDownList ID="ddlDepositTerm" runat="server" OnSelectedIndexChanged="ddlDepositTerm_SelectedIndexChanged" CssClass="form-control">

                                                    <asp:ListItem runat="server" Value="365" Text="Days"></asp:ListItem>
                                                    <asp:ListItem runat="server" Value="12" Text="Months"></asp:ListItem>
                                                    <asp:ListItem runat="server" Value="1" Text="Year"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Date of Fixed Deposit</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDepositDate" CssClass="form-control datepicker" placeholder="DD/MM/YYYY" MaxLength="10" Style="width: 100%" runat="server" />

                                                <asp:RequiredFieldValidator ID="reqtxtDepositDate" runat="server" ControlToValidate="txtDepositDate" ValidationGroup="btn" ErrorMessage="Enter Deposit Date" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Rate of Interest(%)</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtROI" CssClass="form-control Money" Style="text-align: left;" runat="server" />
                                                <asp:RequiredFieldValidator ID="reqtxtROI" runat="server" ControlToValidate="txtROI" ValidationGroup="btn" ErrorMessage="Enter Interest Rate" CssClass="gst-ac-error"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Amount of Deposit</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAmtDeposit" CssClass="form-control Money" Style="text-align: left;" runat="server" />

                                                <asp:RequiredFieldValidator ID="reqtxtAmtDeposit" runat="server" ControlToValidate="txtAmtDeposit" ValidationGroup="btn" ErrorMessage="Enter Amount" CssClass="gst-ac-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Due Date</label>
                                            <div class="col-sm-8">
                                                <label id="lblDueDate" runat="server"></label>
                                               
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group row">
                                            <label class="col-sm-4 alphaonly">Maturity Value</label>
                                            <div class="col-sm-8">
                                                <label id="lblMaturityValue" runat="server"></label>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="text-right">
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                           
                                            <asp:Button ID="btnCalculate" CssClass="btn btn-primary" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                                            <%--<button type="button" class="btn btn-primary" onclick="calculate();">Calculate</button>--%>
                                            <asp:Button ID="btnClear" CssClass="btn btn-danger" runat="server" Text="Clear" OnClick="btnClear_Click" />


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</asp:Content>


