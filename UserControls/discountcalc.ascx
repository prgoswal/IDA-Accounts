<%@ Control Language="C#" AutoEventWireup="true" CodeFile="discountcalc.ascx.cs" Inherits="UserControls_discountcalc" %>

<div class="modal fade" id="ModalDisCalc" role="dialog" data-backdrop="static">
    <div class="modal-dialog modal-dialog-calc">
        <style>
            @media (min-width:800px) {
                .modal-dialog-calc {
                    width: 56%;
                }
            }

            .fm-control {
                padding-left: 1px;
            }

            .br-right {
                border-right: 1px solid #e5e5e5;
                padding-top: 10px;
                margin-bottom: 0;
                height: 110px;
            }

            .modal-title {
                font-family: sans-serif;
            }

            .hr {
                margin-top: 5px;
                margin-bottom: 0;
                border-top: 1px solid #e5e5e5;
            }

            .modal-body {
                position: relative;
                padding: 15px;
                padding-bottom: 0;
            }

            .npd-top10 {
                padding-top: 10px;
            }

            @media only screen and (max-width : 1024px) {
                .modal-dialog-calc {
                    width: 68%;
                }
            }

            @media only screen and (max-width : 768px) {
                .modal-dialog-calc {
                    width: 90%;
                }
            }
        </style>

        <script>
            function Call(id) {

                var x = document.getElementsByTagName("tr");
                var txt = "";
                var i;



                for (i = 1; i < x.length; i++) {
                    if (document.getElementById("myTable").rows[i].cells[0].children[0] == undefined) {
                        continue;
                    }

                    //alert(1);
                    var a = (document.getElementById("myTable").rows[i].cells[0].children[0].value);
                    //alert(a);
                    price = (document.getElementById("myTable").rows[i].cells[0].children[0].value);

                    //alert(document.getElementById("myTable").rows[i].cells[1].children[0].value);
                    dis = (document.getElementById("myTable").rows[i].cells[1].children[0].value);


                    var a = price * dis / 100;
                    var AfterDis = Math.round(price - a).toFixed(2);
                    //var AfterDis =price - a;

                    (document.getElementById("myTable").rows[i].cells[2].children[0].value) = AfterDis;

                    //alert(document.getElementById("myTable").rows[i].cells[3].children[0]);
                    //if (document.getElementById("myTable").rows[i].cells[3].children[0] == undefined) {
                    //    continue;
                    //}

                    if (i != 3) {
                        document.getElementById("myTable").rows[i].cells[3].children[0].style.display = "block";
                    }
                }
            }

            function insRow() {
                var id = 'myTable';
                var filas = document.getElementById("myTable").rows.length;

                var x = document.getElementById(id).insertRow(filas);
                var y = x.insertCell(0);
                var z = x.insertCell(1);
                var a = x.insertCell(2);
                var b = x.insertCell(3);


                b.innerHTML = ' <input type="button" id="lnkbtnShow" class="w100 btn-primary"  style="display:none" onclick="insRow()" value="Give More Discount" />';

                y.innerHTML = ' <input type="number" id="txtPrice" value="" class="w100" />';

                z.innerHTML = ' <input type="number" onchange="Call()" class="w100" id="txtDis" />';
                a.innerHTML = '<input type="number" onkeyup="this.value = minmax(this.value, 0, 100)" id="txtAfterDis" class="w100" disabled="disabled" />';



                var x = document.getElementsByTagName("tr");
                var txt = "";
                var i;



                for (i = 1; i < x.length; i++) {
                    var price;
                    if (i != 1) {
                        document.getElementById("myTable").rows[i].cells[0].children[0].setAttribute("disabled", false);
                    }

                    if (document.getElementById("myTable").rows[i].cells[0].children[0] == undefined) {
                        continue;
                    }
                    price = (document.getElementById("myTable").rows[i].cells[2].children[0].value);
               
                    if (price != '') {

                        y.innerHTML = '<input type="number" id="txtPrice" class="w100" value="' + price + '" />';

                    }
                   
                    if (i != x.length - 1) {
                        document.getElementById("myTable").rows[i].cells[3].children[0].setAttribute("disabled", false);
                    }

                }
            }
            function minmax(value, min, max) {
                if (parseInt(value) < min || isNaN(parseInt(value))) {
                    
                    return 0;
                }
                else if (parseInt(value) > max)
                    return 100;
                else return value;
            }

            function ResetTable() {

              

                var table = document.getElementById("myTable");
                var TableRows = document.getElementById('myTable').rows;
                var rowCount = table.rows.length;

                for (var i = rowCount - 1; i > 0; i--) {
                    table.deleteRow(i);
                }
                insRow();
              
            }


        
        </script>


        <div class="modal-content">
            <div class="modal-header" style="background: #0094ff; font-family: 'Times New Roman'; color: white;">
                <button type="button" class="close" data-dismiss="modal" style="color: white; opacity: 10;">&times;</button>
                <h4 class="modal-title">Discount Calculator</h4>
            </div>
            <div class="modal-body">
                <div id="dvTaxcal" runat="server" class="row">
                    <div class="bg_color">
                        <div class="row">
                            <div class="col-xs-12">
                                <table id="myTable" class="table-bordered">
                                    <%-- <tr style="background-color: #1c75bf; color: #fff;">
                                        <td colspan="4">&nbsp;</td>
                                    </tr>--%>
                                    <tr>
                                        <th>Goods Price</th>
                                        <th>Discount(in %)</th>
                                        <th>After Discount</th>
                                        <%--<th>&nbsp;</th>--%>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="number" id="txtPrice" value="" class="w100" /></td>
                                        <td>
                                            <input type="number" onkeyup="this.value = minmax(this.value, 0, 100)" onchange="Call('myTable')" id="txtDis" class="w100" /></td>
                                        <td>
                                            <input type="number" id="txtAfterDis" disabled="disabled" class="w100" /></td>
                                        <td>
                                            <input type="button" id="lnkbtnShow" style="display: none" onclick="insRow()" value="Give More Discount" class="w100 btn-primary" />
                                        </td>
                                    </tr>


                                </table>
                            </div>
                        </div>

                        <%--  <div class="row hidden">
                            <div class="col-xs-12">
                                <div class="form-group  col-sm-3 col-xs-12">
                                    <label for="email">Goods Prices</label>
                                     <asp:TextBox ID="TextBox1" MaxLength="9" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="email">Discount(in %)</label>
                                    <asp:TextBox ID="TextBox2" MaxLength="9" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="pwd">Amount(After Discount)</label>
                                    <asp:TextBox ID="TextBox3" MaxLength="5" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="pwd">&nbsp;</label><br />
                                    <input type="button" class="btn btn-primary tex-btn" value="Give more Discount" id="Show" />&nbsp;
                                   
                                </div>
                            </div>
                        </div>

                         <div class="row hidden">
                            <div class="col-xs-12">
                                <div class="form-group  col-sm-3 col-xs-12">
                                    <label for="email">Goods Prices</label>
                                     <asp:TextBox ID="TextBox4" MaxLength="9" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="email">Discount(in %)</label>
                                    <asp:TextBox ID="TextBox6" MaxLength="9" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="pwd">After Discount</label>
                                    <asp:TextBox ID="TextBox7" MaxLength="5" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="pwd">&nbsp;</label><br />
                                    <input type="button" class="btn btn-primary tex-btn" value="Give more Discount" id="Show" />&nbsp;
                                   
                                </div>
                            </div>
                        </div>
                        --%>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <input type="button" onclick="ResetTable()" class="btn btn-danger tex-btn" value="Clear" id="clr_btn" />
                <button type="button" id="clr_all" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>

    </div>
</div>
