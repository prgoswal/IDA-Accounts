<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Interestcalc.ascx.cs" Inherits="UserControls_Interestcalc" %>

<div class="modal fade" id="ModalInterestCalc" role="dialog" data-backdrop="static">
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



            function InterestCalculate() {
                var p = document.getElementById("p").value;
                var r = document.getElementById("r").value;

                var Firstdate = document.getElementById("DueDate").value;
                var datearray = Firstdate.split("/");
                var duedate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];

                var SecondDate = document.getElementById("UptoDate").value;
                var datearray = SecondDate.split("/");
                var uptodate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];

                var date1 = new Date(duedate);
                var date2 = new Date(uptodate);
                var timeDiff = Math.abs(date1.getTime() - date2.getTime());
                var diffDays = (Math.ceil(timeDiff / (1000 * 3600 * 24)));
                var InterestCal = ((p * r * diffDays) / 36500).toFixed(2);;
                document.getElementById("txtInterest").value = InterestCal;

            }
            
            function ClearFields() {
                document.getElementById("p").value = '';
                document.getElementById("r").value = '';
                document.getElementById("txtInterest").value = '';
            }
        </script>


        <div class="modal-content">
            <div class="modal-header" style="background: #0094ff; font-family: 'Times New Roman'; color: white;">
                <button type="button" class="close" data-dismiss="modal" style="color: white; opacity: 10;">&times;</button>
                <h4 class="modal-title">Interest Calculator</h4>
            </div>
            <div class="modal-body">
                <div id="dvTaxcal" runat="server" class="row">
                    <div class="bg_color">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="panel panel-default">
                                    <div class="form-group  col-sm-4 col-xs-12">
                                        <label for="email">Principal Amount</label>
                                        <input type="number" id="p" class="form-control" />
                                    </div>
                                    <div class="form-group col-sm-4  col-xs-12">
                                        <label for="email">Interest Rate</label>
                                        <input type="number" id="r" class="form-control" />
                                    </div>
                                    <div class="form-group col-sm-4  col-xs-12">
                                        <label for="pwd">Invoice Date</label>
                                        <input type="text" class="form-control interest datepicker setDate" placeholder="DD/MM/YYYY" />
                                    </div>
                                    <div class="form-group col-sm-4  col-xs-12">
                                        <label for="pwd">Due Date</label>
                                        <input type="text" class="form-control interest datepicker setDate" id="DueDate" placeholder="DD/MM/YYYY" />
                                    </div>
                                    <div class="form-group col-sm-4  col-xs-12">
                                        <label for="pwd">Upto Date</label>
                                        <input type="text" class="form-control interest datepicker setDate" id="UptoDate" placeholder="DD/MM/YYYY" />
                                    </div>
                                    <div class="form-group col-sm-4  col-xs-12">
                                        <label for="pwd"> Interest Amount</label>
                                        <input type="number" id="txtInterest" disabled="disabled" class="form-control" />
                                    </div>
                                    <div class="form-group col-sm-8  col-xs-12">
                                        <label for="pwd">&nbsp;</label><br />
                                        <button id="btnInterest" type="button" onclick="InterestCalculate()" class="btn btn-primary"> Calculate</button>&nbsp;
                                                 <button id="btnClear" type="button" onclick="ClearFields()" class="btn btn-danger">Clear</button>
                                        <button type="button" id="clr_all" class="btn btn-default" data-dismiss="modal">Close</button>
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
