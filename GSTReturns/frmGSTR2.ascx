<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmGSTR2.ascx.cs" Inherits="GSTReturns_frmGSTR2" %>

<asp:Panel ID="pnlGSTR2" Visible="false" runat="server">
    <script type="text/javascript">

        function PrintPanel() {
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("printPanel");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title>GSTR-2 Summary</title>');
            printWindow.document.write('</head><body class="print_view">');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }
    </script>

    <div class="modalPop" id="">
        <div class="modalDialog">
            <div class="modalHeader" id="ExcelHeader">
                <div class="modalClose">
                    <asp:LinkButton ID="btnClose" OnClick="btnClose_Click" runat="server"><span class="fa fa-times-circle-o" ></span></asp:LinkButton>
                </div>
                <div class="text-center">
                    <asp:Label CssClass="modalTital" ID="lblCompanyName" runat="server" /><br />
                    GSTIN :
                    <asp:Label ID="lblGSTIn" runat="server" />
                    <br />
                    <asp:Label ID="lblGSTRMONTYear" runat="server" />

                    <asp:LinkButton ID="btnPrint" runat="server" style="margin: -8px 10px 0 0;float:right;" CssClass="btn btn-info btn-sxs1 hidden-print" OnClientClick="return PrintPanel();" ><span class="fa fa-file-excel-o"></span> Print</asp:LinkButton>
                </div>
            </div>

            <div class="bodyContent">
                <section >
                    <!-- START Page content-->
                        <div class="content-wrapper">
                            <style>
                                
                            </style>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                         <div id="printPanel">  
                                              <style type="text/css">
                                                /*#gstr2_page .table3{width:100%;font-size:12px}#gstr2_page .table3 .c01,#gstr2_page .table3 .c02,#gstr2_page .table3 .c03,#gstr2_page .table3 .c04,#gstr2_page .table3 .c05,#gstr2_page .table3 .c06,#gstr2_page .table3 .c07,#gstr2_page .table3 .c08,#gstr2_page .table3 .c09,#gstr2_page .table3 .c10,#gstr2_page .table3 .c11,#gstr2_page .table3 .c12,#gstr2_page .table3 .c13,#gstr2_page .table3 .c14,#gstr2_page .table3 .c15,#gstr2_page .table3 .c16{width:6.25%}#gstr2_page .table4 .c01{width:122px}#gstr2_page .table4 .c02{width:26px}#gstr2_page .table4 .c03,#gstr2_page .table4 .c04,#gstr2_page .table4 .c05,#gstr2_page .table4 .c06,#gstr2_page .table4 .c07,#gstr2_page .table4 .c08,#gstr2_page .table4 .c09,#gstr2_page .table4 .c10,#gstr2_page .table4 .c11,#gstr2_page .table4 .c12,#gstr2_page .table4 .c13,#gstr2_page .table4 .c14,#gstr2_page .table4 .c15,#gstr2_page .table4 .c16{width:66px}*/
                                                #gstr2_page .table3 .c01,#gstr2_page .table3 .c02,#gstr2_page .table3 .c03,#gstr2_page .table3 .c04,#gstr2_page .table3 .c05,#gstr2_page .table3 .c06,#gstr2_page .table3 .c07,#gstr2_page .table3 .c08,#gstr2_page .table3 .c09,#gstr2_page .table3 .c10,#gstr2_page .table3 .c11,#gstr2_page .table3 .c12,#gstr2_page .table3 .c13,#gstr2_page .table3 .c14,#gstr2_page .table3 .c15,#gstr2_page .table3 .c16{width:6.25%}#gstr2_page .table4 .c01{width:122px}#gstr2_page .table4 .c02{width:26px}#gstr2_page .table4 .c03,#gstr2_page .table4 .c04,#gstr2_page .table4 .c05,#gstr2_page .table4 .c06,#gstr2_page .table4 .c07,#gstr2_page .table4 .c08,#gstr2_page .table4 .c09,#gstr2_page .table4 .c10,#gstr2_page .table4 .c11,#gstr2_page .table4 .c12,#gstr2_page .table4 .c13,#gstr2_page .table4 .c14,#gstr2_page .table4 .c15,#gstr2_page .table4 .c16{width:66px}#gstr2_page .table5 .c01{width:122px}#gstr2_page .table5 .c02{width:72px}#gstr2_page .table5 .c03,#gstr2_page .table5 .c04,#gstr2_page .table5 .c05,#gstr2_page .table5 .c06,#gstr2_page .table5 .c07,#gstr2_page .table5 .c08,#gstr2_page .table5 .c09,#gstr2_page .table5 .c10,#gstr2_page .table5 .c11{width:98px}#gstr2_page .table6 .c01{width:94px}#gstr2_page .table6 .c02{width:27px}#gstr2_page .table6 .c03{width:77px}#gstr2_page .table6 .c04{width:94px}#gstr2_page .table6 .c05{width:27px}#gstr2_page .table6 .c06{width:77px}#gstr2_page .table6 .c07,#gstr2_page .table6 .c08,#gstr2_page .table6 .c09,#gstr2_page .table6 .c10,#gstr2_page .table6 .c11,#gstr2_page .table6 .c12,#gstr2_page .table6 .c13,#gstr2_page .table6 .c14,#gstr2_page .table6 .c15,#gstr2_page .table6 .c16,#gstr2_page .table6 .c17,#gstr2_page .table6 .c18,#gstr2_page .table6 .c19{width:37px}#gstr2_page .table7{width:100%}#gstr2_page .table7 th{vertical-align:middle}#gstr2_page .table7 .c01,#gstr2_page .table7 .c02,#gstr2_page .table7 .c03,#gstr2_page .table7 .c04,#gstr2_page .table7 .c05{width:20%}#gstr2_page .table8 .c01{width:11%}#gstr2_page .table8 .c02,#gstr2_page .table8 .c03,#gstr2_page .table8 .c04,#gstr2_page .table8 .c05,#gstr2_page .table8 .c06,#gstr2_page .table8 .c07{width:6%}#gstr2_page .table8 .c08{width:9%}#gstr2_page .table8 .c09{width:12%}#gstr2_page .table8 .c10{width:9%}#gstr2_page .table8 .c11{width:8%}#gstr2_page .table9 .c01{width:22%}#gstr2_page .table9 .c02,#gstr2_page .table9 .c03,#gstr2_page .table9 .c04,#gstr2_page .table9 .c06,#gstr2_page .table9 .c07{width:14%}#gstr2_page .table10 .c01{width:6%}#gstr2_page .table10 .c02{width:14%}#gstr2_page .table10 .c03,#gstr2_page .table10 .c04{width:6%}#gstr2_page .table10 .c05{width:14%}#gstr2_page .table10 .c06,#gstr2_page .table10 .c07{width:6%}#gstr2_page .table11 .c01{width:30.5%}#gstr2_page .table11 .c02{width:46%}#gstr2_page .table11 .c03,#gstr2_page .table11 .c04,#gstr2_page .table11 .c05,#gstr2_page .table11 .c06{width:6%}#gstr2_page .table12{width:100%}#gstr2_page .table12 .c01,#gstr2_page .table12 .c02,#gstr2_page .table12 .c03,#gstr2_page .table12 .c04,#gstr2_page .table12 .c05,#gstr2_page .table12 .c06{width:6%}#gstr2_page .table12 .c07,#gstr2_page .table12 .c08{width:9%}#gstr2_page .table12 .c09{width:12%}#gstr2_page .table12 .c10,#gstr2_page .table12 .c11{width:9%}
                                                .w1{width:1% !important}.w2{width:2% !important}.w3{width:3% !important}.w4{width:4% !important}.w5{width:5% !important}.w6{width:6% !important}.w7{width:7% !important}.w8{width:8% !important}.w9{width:9% !important}.w10{width:10% !important}.w11{width:11% !important}.w12{width:12% !important}.w13{width:13% !important}.w14{width:14% !important}.w15{width:15% !important}.w16{width:16% !important}.w17{width:17% !important}.w18{width:18% !important}.w19{width:19% !important}.w20{width:20% !important}.w21{width:21% !important}.w22{width:22% !important}.w23{width:23% !important}.w24{width:24% !important}.w25{width:25% !important}.w26{width:26% !important}.w27{width:27% !important}.w28{width:28% !important}.w29{width:29% !important}.w30{width:30% !important}.w31{width:31% !important}.w32{width:32% !important}.w33{width:33% !important}.w34{width:34% !important}.w35{width:35% !important}.w36{width:36% !important}.w37{width:37% !important}.w38{width:38% !important}.w39{width:39% !important}.w40{width:40% !important}.w41{width:41% !important}.w42{width:42% !important}.w43{width:43% !important}.w44{width:44% !important}.w45{width:45% !important}.w46{width:46% !important}.w47{width:47% !important}.w48{width:48% !important}.w49{width:49% !important}.w50{width:50% !important}.w51{width:51% !important}.w52{width:52% !important}.w53{width:53% !important}.w54{width:54% !important}.w55{width:55% !important}.w56{width:56% !important}.w57{width:57% !important}.w58{width:58% !important}.w59{width:59% !important}.w60{width:60% !important}.w61{width:61% !important}.w62{width:62% !important}.w63{width:63% !important}.w64{width:64% !important}.w65{width:65% !important}.w66{width:66% !important}.w67{width:67% !important}.w68{width:68% !important}.w69{width:69% !important}.w70{width:70% !important}.w71{width:71% !important}.w72{width:72% !important}.w73{width:73% !important}.w74{width:74% !important}.w75{width:75% !important}.w76{width:76% !important}.w77{width:77% !important}.w78{width:78% !important}.w79{width:79% !important}.w80{width:80% !important}.w81{width:81% !important}.w82{width:82% !important}.w83{width:83% !important}.w84{width:84% !important}.w85{width:85% !important}.w86{width:86% !important}.w87{width:87% !important}.w88{width:88% !important}.w89{width:89% !important}.w90{width:90% !important}.w91{width:91% !important}.w92{width:92% !important}.w93{width:93% !important}.w94{width:94% !important}.w95{width:95% !important}.w96{width:96% !important}.w97{width:97% !important}.w98{width:98% !important}.w99{width:99% !important}.w100{width:100% !important}
                                                .table-bordered{border: 1px solid #eee;}.b0 {border:0;}.p0 {padding:0;}.table{width:100%} .r, .text-right{text-align:right;}.text-center{text-align:center;}
                                                .table tr th, table tr td {border: 1px solid #eee;font-size: 9px;-ms-word-break: break-all;word-break: break-all;}
                                                                                                                                                                                                      
                                               .th-vert-middle tr th {vertical-align:middle !important}.word-break-all tr td, .word-break-all tr th {-ms-word-break: break-all;word-break:break-all;}#accordion table tr td, #accordion table tr th {font-size:12px !important}
                                              </style>
                                             <div id="gstr2_page">                            
                                             <div id="accordion" class="panel-group">
                                            <!-- START Table 3-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table3">
                                                            <span class="gstr_row_title">Table 3:</span>
                                                            <span class="gstr_row_desc blue">Inward supplies received from a registered person other than the supplies attracting reverse charge</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table3" class="panel-collapse collapse in" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table3">
                                                            <thead>
                                                                <tr>
                                                                    <th rowspan="2">GSTIN Of Supplier</th>
                                                                    <th colspan="3">Invoice Detail</th>
                                                                    <th rowspan="2">TaxRate</th>
                                                                    <th rowspan="2">Taxable Value</th>
                                                                    <th colspan="4">Amount Tax</th>
                                                                    <th rowspan="2">Place Of Supply (Name Of State)</th>                                                                   
                                                                    <th colspan="4">Amount of ITC available</th>
                                                                </tr>
                                                                <tr>
                                                                    <th>No</th>
                                                                    <th>Date</th>
                                                                    <th>Value</th>
                                                                    <th>IntegTaxRated tax</th>
                                                                    <th>Central Tax</th>
                                                                    <th>State / UTTax</th>
                                                                    <th>CESS</th>
                                                                    <th>IntegTaxRated tax</th>
                                                                    <th>Central Tax</th>
                                                                    <th>State/UTTax</th>
                                                                    <th>CESS</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2&nbsp;</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>
                                                                    <th class="c08">8</th>
                                                                    <th class="c09">9</th>
                                                                    <th class="c10">10</th>
                                                                    <th class="c11">11</th>
                                                                    <th class="c12">12</th>
                                                                    <th class="c13">13</th>
                                                                    <th class="c14">14</th>
                                                                    <th class="c15">15</th>
                                                                </tr>
                                                            </thead>
                                                            
                                                        </table>
                                                         <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table3" ID="GrdTable3" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PartyGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c15 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 3-->

                                            <!-- START Table 4-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>                                   
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table4">
                                                            <span class="gstr_row_title">Table 4:</span>
                                                            <span class="gstr_row_desc blue">. Inward supplies on which tax is to be paid on reverse charge</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table4" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table4">
                                                            <thead>
                                                                <tr>
                                                                    <th class="c01" rowspan="2">GSTIN Of Supplier</th>
                                                                    <th class="c02" colspan="3">Invoice Detail</th>
                                                                    <th class="c05" rowspan="2">TaxRate</th>
                                                                    <th class="c06" rowspan="2">Taxable Value</th>
                                                                    <th class="c07" colspan="4">Amount Tax</th>
                                                                    <th class="c11" rowspan="2">Place Of Supply(Name Of State)</th>
                                                                    <%--<th class="c12" rowspan="2">Whether input or input service/ Capital goods (incl plant and machinery)/ Ineligible for ITC</th>--%>
                                                                    <th class="c13" colspan="4">Amount of ITC available</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c02">No</th>
                                                                    <th class="c03">Date</th>
                                                                    <th class="c04">Value</th>
                                                                    <th class="c07">IntegTaxRated tax</th>
                                                                    <th class="c08">Central Tax</th>
                                                                    <th class="c09">State/UTTax</th>
                                                                    <th class="c10">CESS</th>
                                                                    <th class="c13">IntegTaxRated tax</th>
                                                                    <th class="c14">Central Tax</th>
                                                                    <th class="c15">State/UTTax</th>
                                                                    <th class="c16">CESS</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2&nbsp;</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>
                                                                    <th class="c08">8</th>
                                                                    <th class="c09">9</th>
                                                                    <th class="c10">10</th>
                                                                    <th class="c11">11</th>
                                                                   <%-- <th class="c12">12</th>--%>
                                                                    <th class="c13">13</th>
                                                                    <th class="c14">14</th>
                                                                    <th class="c15">15</th>
                                                                    <th class="c16">16</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td class="c01" colspan="16">4A. Inward supplies received from a registered supplier (attracting reverse charge)</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="16" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table4" ID="Grd4A" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PartyGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c14 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c15 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c16 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                  
                                                                </tr>
                                              
                                                                <tr>
                                                                    <td class="c01" colspan="16">4B. Inward supplies received from an unregistered supplier</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="16" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table4"  ID="Grd4B" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                  <asp:BoundField DataField="PartyGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c14 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c15 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c16 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="16">4C. Import of service</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="16" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table4" ID="grd4C" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                 <asp:BoundField DataField="PartyGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c14 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c15 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c16 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                  
                                                                </tr>


                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 4-->

                                            <!-- START Table 5-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>                                    
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table5">
                                                            <span class="gstr_row_title">Table 5:</span>
                                                            <span class="gstr_row_desc blue">Inputs/Capital goods received from Overseas or from SEZ units on a Bill of Entry</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table5" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table5">
                                                            <thead>
                                                                <tr>
                                                                    <th class="c01" rowspan="2">GSTIN of supplier</th>
                                                                    <th class="c02" colspan="3">Details of bill of entry</th>
                                                                    <th class="c05" rowspan="2">TaxRate</th>
                                                                    <th class="c06" rowspan="2">Taxable value</th>
                                                                    <th class="c07" colspan="2">Amount</th>
                                                                   <%-- <th class="c09" rowspan="2">Whether input / Capital goods ( incl. plant and machinery ) / Ineligible for ITC</th>--%>
                                                                    <th class="c10" colspan="2">Amount of ITC available</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c02">No.</th>
                                                                    <th class="c03">Date</th>
                                                                    <th class="c04">Value</th>
                                                                    <th class="c07">IntegTaxRated tax</th>
                                                                    <th class="c08">CESS</th>
                                                                    <th class="c10">IntegTaxRated tax</th>
                                                                    <th class="c11">CESS</th>
                                                                </tr>

                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>
                                                                    <th class="c08">8</th>
                                                                   <%-- <th class="c09">9</th>--%>
                                                                    <th class="c10">10</th>
                                                                    <th class="c11">11</th>
                                                                </tr>

                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td class="c01" colspan="11">5A. Imports</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="11" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table5" ID="grd5A" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PartyGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c08 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c11 text-center" />
                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>                                                    
                                                                </tr>                                               
                                                                <tr>
                                                                    <td class="c01" colspan="11">5B. Received from SEZ</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="11" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table5" ID="grd5B" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PartyGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c08 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c11 text-center" />
                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>                                                   
                                                                </tr>                                             

                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 5-->

                                            <!-- START Table 6-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>                                    
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table6">
                                                            <span class="gstr_row_title">Table 6:</span>
                                                            <span class="gstr_row_desc blue">Amendments to details of inward supplies furnished in returns for earlier tax periods in Tables 3, 4 and 5 [including debit notes/credit notes issued and their subsequent amendments]</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table6" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table6">
                                                            <thead>
                                                                <tr>
                                                                    <th class="c01" colspan="3">Details of original invoice / Bill of entry No </th>
                                                                    <th class="c04" colspan="4">Revised details of invoice</th>
                                                                    <th class="c08" rowspan="2">TaxRate</th>
                                                                    <th class="c09" rowspan="2">Taxable value</th>
                                                                    <th class="c10" colspan="4">Amount</th>
                                                                    <th class="c14" rowspan="2">Place of supply</th>
                                                                    <%--<th class="c15" rowspan="2">Whether input or input service / Capital goods / Ineligible for ITC )</th>--%>
                                                                    <th class="c16" colspan="4">Amount of ITC available</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">GSTIN</th>
                                                                    <th class="c02">No.</th>
                                                                    <th class="c03">Date</th>
                                                                    <th class="c04">GSTIN</th>
                                                                    <th class="c05">No.</th>
                                                                    <th class="c06">Date</th>
                                                                    <th class="c07">Value</th>
                                                                    <th class="c10">IGST</th>
                                                                    <th class="c11">CGST</th>
                                                                    <th class="c12">SGST</th>
                                                                    <th class="c13">Cess</th>
                                                                    <th class="c16">IGST</th>
                                                                    <th class="c17">CGST</th>
                                                                    <th class="c18">SGST</th>
                                                                    <th class="c19">Cess</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>
                                                                    <th class="c08">8</th>
                                                                    <th class="c09">9</th>
                                                                    <th class="c10">10</th>
                                                                    <th class="c11">11</th>
                                                                    <th class="c12">12</th>
                                                                    <th class="c13">13</th>
                                                                    <th class="c14">14</th>
                                                                   <%-- <th class="c15">15</th>--%>
                                                                    <th class="c16">16</th>
                                                                    <th class="c17">17</th>
                                                                    <th class="c18">18</th>
                                                                    <th class="c19">19</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td class="c01" colspan="19">6A. Supplies other than import of goods or goods received from SEZ [Information furnished in Table 3 and 4 of earlier returns]-If details furnished earlier were incorrect</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="19" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table6" ID="grd6A" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                 <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c06 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c11 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c12 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c16 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c17 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c18 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c19 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="19">6B. Supplies by way of import of goods or goods received from SEZ [Information furnished in Table 5 of earlier returns]-If details furnished earlier were incorrect</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="19" class="p0 b0">
                                                                        <asp:GridView runat="server"  CssClass="table table-bordered table-gstr table_psm table6" ID="grd6B" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c05 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c11 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c12 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c16 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c17 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c18 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c19 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="19">6C. Debit Notes/Credit Notes [original]</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="19" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table6" ID="grd6C" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c06 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c11 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c12 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c16 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c17 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c18 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c19 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="19">6D. Debit Notes/ Credit Notes [amendment of debit notes/credit notes furnished in earlier tax periods]</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="19" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table6" ID="grd6D" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                               <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                 <asp:BoundField DataField="PartyGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="InvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="InvoiceDate" ItemStyle-CssClass="c06 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="InvoiceAmount" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="TaxableAmount" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="IGSTAmount" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="CGSTAmount" ItemStyle-CssClass="c11 text-center" />
                                                                                <asp:BoundField DataField="SGSTAmount" ItemStyle-CssClass="c12 text-center" />
                                                                                <asp:BoundField DataField="CessAmount" ItemStyle-CssClass="c13 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                                <%--<asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />--%>
                                                                                <asp:BoundField DataField="ITCIGSTAmount" ItemStyle-CssClass="c16 text-center" />
                                                                                <asp:BoundField DataField="ITCCGSTAmount" ItemStyle-CssClass="c17 text-center" />
                                                                                <asp:BoundField DataField="ITCSGSTAmount" ItemStyle-CssClass="c18 text-center" />
                                                                                <asp:BoundField DataField="ITCCessAmount" ItemStyle-CssClass="c19 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>        
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 6-->

                                            <!-- START Table 7-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>

                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table7">
                                                            <span class="gstr_row_title">Table 7:</span>
                                                            <span class="gstr_row_desc blue">Supplies received from composition taxable person and other exempt/Nil TaxRated/Non GST supplies received</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table7" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table7">
                                                            <thead>
                                                                <tr>
                                                                    <th class="c01" rowspan="2">Description</th>
                                                                    <th class="c02" colspan="4">Value of supplies received from</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c02">Composition taxable person</th>
                                                                    <th class="c03">Exempt supply</th>
                                                                    <th class="c04">Nil TaxRated supply</th>
                                                                    <th class="c05">Non GST supply</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        7A. Inter-State supplies 
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="5" class="p0 b0">
                                                                        <asp:GridView  CssClass="table table-bordered table-gstr table_psm table7" runat="server" ID="grd7A" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="RecDesc" ItemStyle-CssClass="c01 text-left" />
                                                                                <asp:BoundField DataField="CompoAmount" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="ExmpAmount" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="NILAmount" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="NonGSTAmount" ItemStyle-CssClass="c05 text-center" />
                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        7B. Intra-State supplies 
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="5" class="p0 b0">
                                                                        <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ID="grd7B" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Tbl7Desc" ItemStyle-CssClass="c01 text-left" />
                                                                                <asp:BoundField DataField="ComposAmt" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="ExempAmt" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="NilTaxRatedAmt" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="NonGstAmt" ItemStyle-CssClass="c05 text-center" />
                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 7-->

                                            <!-- START Table 8-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>

                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table8">
                                                            <span class="gstr_row_title">Table 8:</span>
                                                            <span class="gstr_row_desc blue">ISD credit received</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table8" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table8">
                                                            <thead>

                                                                <tr>
                                                                    <th class="c01" rowspan="2">GSTIN of ISD</th>
                                                                    <th class="c02" colspan="2">ISD Document Details</th>
                                                                    <th class="c04" colspan="4">ISD Credit received</th>
                                                                    <th class="c08" colspan="4">Amount of eligible ITC</th>
                                                                </tr>

                                                                <tr>
                                                                    <th class="c02">No.</th>
                                                                    <th class="c03">Date</th>
                                                                    <th class="c04">IntegTaxRated Tax</th>
                                                                    <th class="c05">Central Tax</th>
                                                                    <th class="c06">State/ UT Tax</th>
                                                                    <th class="c07">Cess</th>
                                                                    <th class="c08">IntegTaxRated Tax</th>
                                                                    <th class="c09">Central Tax</th>
                                                                    <th class="c10">State/ UT Tax</th>
                                                                    <th class="c11">Cess</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>
                                                                    <th class="c08">8</th>
                                                                    <th class="c09">9</th>
                                                                    <th class="c10">10</th>
                                                                    <th class="c11">11</th>
                                                                </tr>

                                                            </thead>
                                                            <tbody>

                                                                <tr>
                                                                     <td colspan="11" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table8" ID="grd8" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="GSTINISD" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="ISDDocNo" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="ISDDocDate" ItemStyle-CssClass="c03 text-center" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField DataField="ISDIGST" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="ISDCGST" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="ISDSGST" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="ISDCess" ItemStyle-CssClass="c07 text-center" />
                                                                                <asp:BoundField DataField="EligITCIGST" ItemStyle-CssClass="c08 text-center" />
                                                                                <asp:BoundField DataField="EligITCCGST" ItemStyle-CssClass="c09 text-center" />
                                                                                <asp:BoundField DataField="EligITCSGST" ItemStyle-CssClass="c10 text-center" />
                                                                                <asp:BoundField DataField="EligITCCess" ItemStyle-CssClass="c11 text-center" />
                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>                                                
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 8-->

                                            <!-- START Table 9-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table9">
                                                            <span class="gstr_row_title">Table 9:</span>
                                                            <span class="gstr_row_desc blue">TDS and TCS Credit received </span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table9" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table9">
                                                            <thead>
                                                                <tr>
                                                                    <th class="c01" rowspan="3">GSTIN of Deductor / GSTIN of eCommerce Operator</th>
                                                                    <th class="c02" rowspan="3">Gross Value</th>
                                                                    <th class="c03" rowspan="3">Sales Return</th>
                                                                    <th class="c04" rowspan="3">Net Value</th>
                                                                    <th class="c05" colspan="3">Amount</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c05">IntegTaxRated Tax</th>
                                                                    <th class="c06">Central Tax</th>
                                                                    <th class="c07">State Tax /UT Tax</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                    <th class="c07">7</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>

                                                                <tr>
                                                                     <td colspan="7" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table9" ID="grd9" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="TDSTCSGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="GrossValue" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="SalesReturn" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="NetValue" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="TDSTCSIGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="TDSTCSCGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="TDSTCSSGSTTax" ItemStyle-CssClass="c07 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="7">&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="7">&nbsp;</td>
                                                                </tr>

                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 9-->

                                            <!-- START Table 10-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table10">
                                                            <span class="gstr_row_title">Table 10:</span>
                                                            <span class="gstr_row_desc blue">Consolidated Statement of Advances paid/Advance adjusted on account of receipt of supply</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table10" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table10">
                                                            <thead>

                                                                <tr>
                                                                    <th class="c01" rowspan="2">TaxRate</th>
                                                                    <th class="c02" rowspan="2">Gross Advance Paid</th>
                                                                    <th class="c03" rowspan="2">Place of supply (Name of State)</th>
                                                                    <th class="c04" colspan="7">Amount</th>
                                                                </tr>
                                                                <tr>

                                                                    <th class="c04">IntegTaxRated Tax</th>
                                                                    <th class="c05">Central Tax</th>
                                                                    <th class="c06" colspan="3">State/UT Tax</th>
                                                                    <th class="c07" colspan="2">Cess</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06" colspan="3">6</th>
                                                                    <th class="c07" colspan="2">7</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td class="c01" colspan="10"><b>(I) Information for the current month</b></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="c01" colspan="10">10A. Advance amount paid for reverse charge supplies in the tax period (tax amount to be added to output tax liability)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="10">10A (1). Intra-State supplies (TaxRate Wise)</td>
                                                                </tr>
                                                                <tr>
                                                                      <td colspan="10" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table10" ID="grd10A1" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="GrossReceived" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="IGST" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="CGST" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="SGST" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="Cess" ItemStyle-CssClass="c07 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="10">10A (2). Inter -State Supplies (TaxRate Wise)</td>
                                                                </tr>
                                                                <tr>
                                                                      <td colspan="10" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table10" ID="grd10A2" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                 <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c07 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="10">10B. Advance amount on which tax was paid in earlier period but invoice has been received in the current period [reflected in Table 4 above] </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="10">10B (1). Intra-State Supplies (TaxRate Wise)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="10" class="p0 b0">
                                                                        <asp:GridView runat="server" CssClass="table table-bordered table-gstr table_psm table10" ID="grd10b1" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                 <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c07 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="10">10B (2). Intra-State Supplies (TaxRate Wise)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="10" class="p0 b0">
                                                                        <asp:GridView CssClass="table table-bordered table-gstr table_psm table10" runat="server" ID="grd10b2" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                  <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c07 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>                                                 
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="10"><b>II Amendments of information furnished in Table No. 10 (I) in an earlier month</b> [Furnish revised information]</td>
                                                                </tr>
                                                                <tr>
                                                                      <td colspan="10" class="p0 b0">
                                                                        <asp:GridView CssClass="table table-bordered table-gstr table_psm table10" runat="server" ID="grd10Amd" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                 <asp:BoundField DataField="TaxRate" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c07 text-center" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 10-->

                                            <!-- START Table 11-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table11">
                                                            <span class="gstr_row_title">Table 11:</span>
                                                            <span class="gstr_row_desc blue">Input Tax Credit Reversal / Reclaim </span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table11" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table11">
                                                            <thead>

                                                                <tr>
                                                                    <th rowspan="2" class="c01">Description for reversal of ITC</th>
                                                                    <th rowspan="2" class="c02">To be added to or reduced from output liability</th>
                                                                    <th class="c03" colspan="4">Amount of ITC</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c03">IGST</th>                                                    
                                                                    <th class="c04">CGST</th>                                                    
                                                                    <th class="c05">SGST</th>                                                    
                                                                    <th class="c06">Cess</th>
                                                                </tr>
                                                                <tr>
                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                     <td colspan="6" class="p0 b0">
                                                                        <asp:GridView CssClass="table table-bordered table-gstr table_psm table11" runat="server" ID="grd11" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="DesRevOfITCDesc" ItemStyle-CssClass="c01 text-left" />
                                                                                <asp:BoundField DataField="ToBeAdded" ItemStyle-CssClass="c02 text-left" />
                                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c06 text-center" />
                                                                
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="6"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="c01" colspan="6">B. Amendment of information furnished in Table No 11 at S. No A in an earlier return</td>
                                                                </tr>
                                                                <tr>
                                                                     <td colspan="6" class="p0 b0">
                                                                        <asp:GridView runat="server" ID="grd11Amd" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c06 text-center" />
                                                                
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 11-->

                                            <!-- START Table 12-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table12">
                                                            <span class="gstr_row_title">Table 12:</span>
                                                            <span class="gstr_row_desc blue">Addition and reduction of amount in output tax for mismatch and other reasons </span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table12" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm tabel12">
                                                            <thead>

                                                                <tr>
                                                                    <th class="c01" rowspan="2">Description1</th>
                                                                    <th class="c02" rowspan="2">To be added to or reduced from output liability2</th>
                                                                    <th class="c03" colspan="4">Amount of ITC</th>
                                                                </tr>
                                                                <tr>


                                                                    <th class="c03">IntegTaxRated</th>
                                                                    <th class="c04">Tax Central Tax</th>
                                                                    <th class="c05">State / UT Tax</th>
                                                                    <th class="c06">CESS</th>
                                                                </tr>
                                                                 <tr>


                                                                    <th class="c01">1</th>
                                                                    <th class="c02">2</th>
                                                                    <th class="c03">3</th>
                                                                    <th class="c04">4</th>
                                                                    <th class="c05">5</th>
                                                                    <th class="c06">6</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                               

                                                                <tr>
                                                                     <td colspan="6" class="p0 b0">
                                                                        <asp:GridView runat="server" ID="grd12" AutoGenerateColumns="false" ShowHeader="false">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c01 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c02 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c03 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c04 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c05 text-center" />
                                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c06 text-center" />
                                                                
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>                                               
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 12-->


                                             <!-- START Table 13-->
                                            <div class="panel panel-default panel-demo">
                                                <div class="panel-heading" style="padding-bottom: 14px">
                                                    <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right" data-original-title="Refresh Panel">
                                                        <em class="fa fa-refresh"></em>
                                                    </a>

                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#table13">
                                                            <span class="gstr_row_title">Table 13:</span>
                                                            <span class="gstr_row_desc blue">HSN summary of inward supplies</span>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="table13" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                    <div class="panel-table">

                                                        <table class="table table-bordered table-gstr table_psm table13 th-vert-middle word-break-all font-12">
                                                            <thead>
                                               
                                                                <tr>
                                                                    <th rowspan="2">Sr. No.</th>
                                                                    <th rowspan="2">HSN</th>
                                                                    <th>Description</th>
                                                                    <th rowspan="2">UQC</th>
                                                                    <th>Total</th>
                                                                    <th>Total</th>
                                                                    <th>Total</th>
                                                                    <th colspan="4">Amount</th>
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    
                                                                    <th>(Optional if HSN is furnished)</th>                                                                   
                                                                    <th>Quantity</th>
                                                                    <th>Value</th>
                                                                    <th>Taxable Value</th>
                                                                    <th>IntegTaxRated Tax</th>
                                                                    <th>Central Tax</th>
                                                                    <th>State/UT Tax</th>
                                                                    <th>Cess</th>
                                                                </tr>
                                                                <tr>
                                                                    <th style="width:4%">1</th>
                                                                    <th style="width:9%">2</th>
                                                                    <th style="width:22%">3</th>
                                                                    <th style="width:8%">4</th>
                                                                    <th style="width:8%">5</th>
                                                                    <th style="width:8%">6</th>
                                                                    <th style="width:8%">7</th>
                                                                    <th style="width:8%">8</th>
                                                                    <th style="width:8%">9</th>
                                                                    <th style="width:8%">10</th>
                                                                    <th style="width:8%">11</th>
                                                                </tr>

                                                            </thead>
                                                           
                                                        </table>
                                                        <asp:GridView CssClass="table table-bordered word-break-all " runat="server" ID="grd13" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="c01 text-center" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="HSNSACCode"     ItemStyle-CssClass="c02 text-center" ItemStyle-Width="9%" />
                                                                <asp:BoundField DataField="HSNSACDesc"     ItemStyle-CssClass="c03 text-center" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="UQC"            ItemStyle-CssClass="c04 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="ItemQty"        ItemStyle-CssClass="c05 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="GrossAmount"    ItemStyle-CssClass="c06 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="TaxableAmount"  ItemStyle-CssClass="c07 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="IGSTAmount"     ItemStyle-CssClass="c08 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="CGSTAmount"     ItemStyle-CssClass="c09 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="SGSTAmount"     ItemStyle-CssClass="c10 text-center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="CessAmount"     ItemStyle-CssClass="c11 text-center" ItemStyle-Width="8%" />                                                               
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- END Table 13-->
                                        </div>
                                                 </div>
                                          </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    <!-- END Page content-->
                </section>
            </div>
        </div>
    </div>

</asp:Panel>
