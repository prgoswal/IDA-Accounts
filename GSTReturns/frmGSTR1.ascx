<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmGSTR1.ascx.cs" Inherits="GSTReturns_frmGSTR1"  %>

<!-- Modal -->
<asp:Panel ID="pnlGSTR1" Visible="false" runat="server">


    <script type="text/javascript">

        function PrintPanel() {
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("printPanel");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title>GSTR-1 Summary</title>');
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

    <div class="modalPop">
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
                    <span class="modalRight hidden">
                        <button class="btn btn-default" onclick="GenerateExcel();">
                            Import Excel
                            <span class="fa fa-file-excel-o"></span>
                        </button>
                    </span>
                    <asp:LinkButton ID="btnPrint" runat="server" style="margin: -8px 10px 0 0;float:right;" CssClass="btn btn-info btn-sxs1 hidden-print" OnClientClick="return PrintPanel();" ><span class="fa fa-file-excel-o"></span> Print</asp:LinkButton>
                </div>
            </div>
            <div class="bodyContent">
                <section>
                    <!-- START Page content-->

                    <div class="content-wrapper" id="">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div>
                                        <div id="printPanel">
                                            <style>
                                                #gstr1_page .table4{width:100%}#gstr1_page .table4 .c01{width:12%}#gstr1_page .table4 .c02,#gstr1_page .table4 .c03,#gstr1_page .table4 .c04,#gstr1_page .table4 .c05,#gstr1_page .table4 .c06,#gstr1_page .table4 .c07,#gstr1_page .table4 .c08{width:8.9%}#gstr1_page .table4 .c09,#gstr1_page .table4 .c10,#gstr1_page .table4 .c11{width:8.5%}#gstr1_page .table5{width:100%}#gstr1_page .table5 .c01,#gstr1_page .table5 .c02,#gstr1_page .table5 .c03,#gstr1_page .table5 .c04,#gstr1_page .table5 .c05,#gstr1_page .table5 .c06,#gstr1_page .table5 .c07,#gstr1_page .table5 .c08{width:12.5%}#gstr1_page .table6{width:100%}#gstr1_page .table6 .c01{width:13%}#gstr1_page .table6 .c02,#gstr1_page .table6 .c03,#gstr1_page .table6 .c04,#gstr1_page .table6 .c05,#gstr1_page .table6 .c06,#gstr1_page .table6 .c07,#gstr1_page .table6 .c08,#gstr1_page .table6 .c09{width:8.3%}#gstr1_page .table7{width:100%}#gstr1_page .table7 .c01,#gstr1_page .table7 .c02,#gstr1_page .table7 .c03,#gstr1_page .table7 .c04,#gstr1_page .table7 .c05,#gstr1_page .table7 .c06{width:16.5%}#gstr1_page .table8{width:100%}#gstr1_page .table8 .c01{width:40%}#gstr1_page .table8 .c02,#gstr1_page .table8 .c03,#gstr1_page .table8 .c04{width:20%}#gstr1_page .table9{width:100%}#gstr1_page .table9 .c01{width:11.25%}#gstr1_page .table9 .c02{width:10.25%}#gstr1_page .table9 .c03{width:6%}#gstr1_page .table9 .c04{width:11.25%}#gstr1_page .table9 .c05{width:4.25%}#gstr1_page .table9 .c06,#gstr1_page .table9 .c07,#gstr1_page .table9 .c08,#gstr1_page .table9 .c09,#gstr1_page .table9 .c10,#gstr1_page .table9 .c11{width:5%}#gstr1_page .table9 .c12{width:6%}#gstr1_page .table9 .c13,#gstr1_page .table9 .c14,#gstr1_page .table9 .c15{width:6.25%}#gstr1_page .table9 .c16{width:2.25%}#gstr1_page .table10{width:100%}#gstr1_page .table10 .c01,#gstr1_page .table10 .c02,#gstr1_page .table10 .c03,#gstr1_page .table10 .c04,#gstr1_page .table10 .c05,#gstr1_page .table10 .c06{width:16.5%}#gstr1_page .table11{width:100%}#gstr1_page .table11 .c01,#gstr1_page .table11 .c02,#gstr1_page .table11 .c03,#gstr1_page .table11 .c04,#gstr1_page .table11 .c05,#gstr1_page .table11 .c06,#gstr1_page .table11 .c07{width:14.2%}#gstr1_page .table12{width:100%}#gstr1_page .table12 .c01,#gstr1_page .table12 .c02,#gstr1_page .table12 .c03,#gstr1_page .table12 .c04,#gstr1_page .table12 .c05,#gstr1_page .table12 .c06,#gstr1_page .table12 .c07,#gstr1_page .table12 .c08,#gstr1_page .table12 .c09,#gstr1_page .table12 .c10,#gstr1_page .table12 .c11{width:9%}#gstr1_page .table13{width:100%}#gstr1_page .table13 .c01{width:4%}#gstr1_page .table13 .c02{width:50%}#gstr1_page .table13 .c03,#gstr1_page .table13 .c04,#gstr1_page .table13 .c05,#gstr1_page .table13 .c06,#gstr1_page .table13 .c07{width:9.2%}.gstr_row_desc,.gstr_row_title{font-size:13px}#gstr2_page .table-bordered>tbody>tr>td,#gstr2_page .table-bordered>tbody>tr>th,#gstr2_page .table-bordered>tfoot>tr>td,#gstr2_page .table-bordered>tfoot>tr>th,#gstr2_page .table-bordered>thead>tr>td,#gstr2_page .table-bordered>thead>tr>th{border:1px solid #828282;padding:1px}#gstr2_page .table-bordered>tbody>tr>th,#gstr2_page .table-bordered>tfoot>tr>th,#gstr2_page .table-bordered>thead>tr>th{background:#1c75bf;color:#fff;border-color:#fff}#gstr2_page .table3{width:100%;font-size:12px}@media(min-width:1100px){#gstr2_page .table3{width:100%;font-size:12px}}#gstr2_page .table3 .c01,#gstr2_page .table3 .c02,#gstr2_page .table3 .c03,#gstr2_page .table3 .c04,#gstr2_page .table3 .c05,#gstr2_page .table3 .c06,#gstr2_page .table3 .c07,#gstr2_page .table3 .c08,#gstr2_page .table3 .c09,#gstr2_page .table3 .c10,#gstr2_page .table3 .c11,#gstr2_page .table3 .c12,#gstr2_page .table3 .c13,#gstr2_page .table3 .c14,#gstr2_page .table3 .c15,#gstr2_page .table3 .c16{width:6.25%}#gstr2_page .table4{width:1085px;font-size:12px}@media(min-width:1100px){#gstr2_page .table4{width:100%;font-size:12px}}#gstr2_page .table4 .c01{width:122px}#gstr2_page .table4 .c02{width:26px}#gstr2_page .table4 .c03,#gstr2_page .table4 .c04,#gstr2_page .table4 .c05,#gstr2_page .table4 .c06,#gstr2_page .table4 .c07,#gstr2_page .table4 .c08,#gstr2_page .table4 .c09,#gstr2_page .table4 .c10,#gstr2_page .table4 .c11,#gstr2_page .table4 .c12,#gstr2_page .table4 .c13,#gstr2_page .table4 .c14,#gstr2_page .table4 .c15,#gstr2_page .table4 .c16{width:66px}#gstr2_page .table5{width:1085px;font-size:12px}
                                                
                                                .w1{width:1% !important}.w2{width:2% !important}.w3{width:3% !important}.w4{width:4% !important}.w5{width:5% !important}.w6{width:6% !important}.w7{width:7% !important}.w8{width:8% !important}.w9{width:9% !important}.w10{width:10% !important}.w11{width:11% !important}.w12{width:12% !important}.w13{width:13% !important}.w14{width:14% !important}.w15{width:15% !important}.w16{width:16% !important}.w17{width:17% !important}.w18{width:18% !important}.w19{width:19% !important}.w20{width:20% !important}.w21{width:21% !important}.w22{width:22% !important}.w23{width:23% !important}.w24{width:24% !important}.w25{width:25% !important}.w26{width:26% !important}.w27{width:27% !important}.w28{width:28% !important}.w29{width:29% !important}.w30{width:30% !important}.w31{width:31% !important}.w32{width:32% !important}.w33{width:33% !important}.w34{width:34% !important}.w35{width:35% !important}.w36{width:36% !important}.w37{width:37% !important}.w38{width:38% !important}.w39{width:39% !important}.w40{width:40% !important}.w41{width:41% !important}.w42{width:42% !important}.w43{width:43% !important}.w44{width:44% !important}.w45{width:45% !important}.w46{width:46% !important}.w47{width:47% !important}.w48{width:48% !important}.w49{width:49% !important}.w50{width:50% !important}.w51{width:51% !important}.w52{width:52% !important}.w53{width:53% !important}.w54{width:54% !important}.w55{width:55% !important}.w56{width:56% !important}.w57{width:57% !important}.w58{width:58% !important}.w59{width:59% !important}.w60{width:60% !important}.w61{width:61% !important}.w62{width:62% !important}.w63{width:63% !important}.w64{width:64% !important}.w65{width:65% !important}.w66{width:66% !important}.w67{width:67% !important}.w68{width:68% !important}.w69{width:69% !important}.w70{width:70% !important}.w71{width:71% !important}.w72{width:72% !important}.w73{width:73% !important}.w74{width:74% !important}.w75{width:75% !important}.w76{width:76% !important}.w77{width:77% !important}.w78{width:78% !important}.w79{width:79% !important}.w80{width:80% !important}.w81{width:81% !important}.w82{width:82% !important}.w83{width:83% !important}.w84{width:84% !important}.w85{width:85% !important}.w86{width:86% !important}.w87{width:87% !important}.w88{width:88% !important}.w89{width:89% !important}.w90{width:90% !important}.w91{width:91% !important}.w92{width:92% !important}.w93{width:93% !important}.w94{width:94% !important}.w95{width:95% !important}.w96{width:96% !important}.w97{width:97% !important}.w98{width:98% !important}.w99{width:99% !important}.w100{width:100% !important}
                                                .table-bordered{border: 1px solid #eee;}.b0 {border:0;}.p0 {padding:0;}.table{width:100%} .r, .text-right{text-align:right;}.text-center{text-align:center;}.table tr th,table tr td {border: 1px solid #eee;font-size: 12px;}                                                
                                            </style>
                                            <div id="gstr1_page">
                                        <table class="table2excel">
                                            <tr>
                                                <td>
                                                    <!-- START Summary-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#tableSummery">
                                                                    <span class="gstr_row_title">GSTR1 Summary :</span>
                                                                    <span class="gstr_row_desc blue"></span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="tableSummery" class="panel-collapse collapse in" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class=" table table-bordered table-gstr table_psm table4">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="w60">Report Desc</th>
                                                                            <th class="w10">Invoice Count</th>
                                                                            <th class="w10">CGST Amount</th>
                                                                            <th class="w10">SGST Amount</th>
                                                                            <th class="w10">IGST Amount</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="p0 b0 w100" colspan="5">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="GrdSummary" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ReportDesc" DataField="ReportDesc" ItemStyle-CssClass="w60" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceCnt" ItemStyle-CssClass="w10 text-center" />
                                                                                        <asp:BoundField HeaderText="CGST" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="w10 r text-right" />
                                                                                        <asp:BoundField HeaderText="SGST" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="w10 r text-right" />
                                                                                        <asp:BoundField HeaderText="IGST" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="w10 r text-right" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                     <!-- END Summary-->
                                                    <!-- START Table 4-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table4">
                                                                    <span class="gstr_row_title">Table 4:</span>
                                                                    <span class="gstr_row_desc blue">Taxable outward supplies made to registered persons (including UIN-holders) other than supplies covered by Table 6</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table4" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class=" table table-bordered table-gstr table_psm table4">
                                                                    <thead>
                                                                        <tr>
                                                                            <th rowspan="2">GSTIN/UIN</th>
                                                                            <th colspan="3">Invoice</th>
                                                                            <th rowspan="2">Rate</th>
                                                                            <th rowspan="2">Taxable&nbsp;Value</th>
                                                                            <th colspan="4">Amount in INR.</th>
                                                                            <th rowspan="2">POS</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>No.</th>
                                                                            <th>Date</th>
                                                                            <th>Value</th>
                                                                            <th>Integrated Tax</th>
                                                                            <th>Central Tax</th>
                                                                            <th>State / UT Tax</th>
                                                                            <th>Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                            <th class="c08">(8)</th>
                                                                            <th class="c09">(9)</th>
                                                                            <th class="c10">(10)</th>
                                                                            <th class="c11">(11)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th">
                                                                            <td colspan="11">4A. Supplies other than those :- (i) attracting reverse charge and (ii) supplies made through e-commerce operator</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR14A" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}" ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="TaxableValue" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="IGST" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c07 r text-right" />
                                                                                        <asp:BoundField HeaderText="CGST" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r text-right" />
                                                                                        <asp:BoundField HeaderText="SGST" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c10 text-center" />
                                                                                        <asp:BoundField HeaderText="POS" DataField="PartyStateDesc" ItemStyle-CssClass="c11  text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td colspan="11"></td>
                                                                        </tr>--%>
                                                                        
                                                                        <tr class="sub_th">
                                                                            <td colspan="11">4B. Supplies attracting tax on reverse charge basis :</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR4B" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="TaxableValue" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="IGST" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c07 r text-right" />
                                                                                        <asp:BoundField HeaderText="CGST" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r text-right" />
                                                                                        <asp:BoundField HeaderText="SGST" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c10 text-center" />
                                                                                        <asp:BoundField HeaderText="POS" DataField="PartyStateDesc" ItemStyle-CssClass="c11  text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td colspan="11"></td>
                                                                        </tr>  --%>                                                                     
                                                                        <tr class="sub_th">
                                                                            <td colspan="11">4C. Supplies made through e-commerce operator attracting TCS (operator wise, rate wise) : </td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR14C" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="TaxableValue" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="IGST" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c07 r text-right" />
                                                                                        <asp:BoundField HeaderText="CGST" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r text-right" />
                                                                                        <asp:BoundField HeaderText="SGST" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c10 text-center" />
                                                                                        <asp:BoundField HeaderText="POS" DataField="PartyStateDesc" ItemStyle-CssClass="c11  text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>

                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 4-->
                                                    <!-- START Table 5-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table5">
                                                                    <span class="gstr_row_title">Table 5:</span>
                                                                    <span class="gstr_row_desc blue">Taxable outward inter-State supplies to un-registered persons where the invoice value is more than Rs 2.5 lakh </span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table5" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table5">
                                                                    <thead>
                                                                        <tr>
                                                                            <th rowspan="2">POS</th>
                                                                            <th colspan="3">Invoice</th>
                                                                            <th rowspan="2">Rate</th>
                                                                            <th rowspan="2">Taxable&nbsp;Value</th>
                                                                            <th colspan="2">Amount in INR.</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>No.</th>
                                                                            <th>Date</th>
                                                                            <th>Value</th>
                                                                            <th>Integrated Tax</th>
                                                                            <th>Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                            <th class="c08">(8)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th">
                                                                            <td colspan="8">5A. Outward supplies (other than supplies made through e-commerce operator, rate wise)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR15A" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="POS" DataField="PartyStateDesc" ItemStyle-CssClass="c01  text-center" />
                                                                                        <%--<asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />--%>
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="TaxableValue" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="IGST" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c07 r text-right" />
                                                                                        <%--<asp:BoundField HeaderText="CGST" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r text-right" />--%>
                                                                                        <%--<asp:BoundField HeaderText="SGST" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />--%>
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 text-center" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr class="sub_th">
                                                                            <td colspan="8">5B. Supplies made through e-commerce operator attracting TCS (operator wise, rate wise) : </td>
                                                                        </tr>
                                                                       
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR15B" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="POS" DataField="PartyStateDesc" ItemStyle-CssClass="c01  text-center" />
                                                                                        <%--<asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />--%>
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="TaxableValue" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="IGST" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c07 r text-right" />
                                                                                        <%--<asp:BoundField HeaderText="CGST" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r text-right" />--%>
                                                                                        <%--<asp:BoundField HeaderText="SGST" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />--%>
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 text-center" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 5-->
                                                    <!-- START Table 6-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table6">
                                                                    <span class="gstr_row_title">Table 6:</span>
                                                                    <span class="gstr_row_desc blue">Zero rated supplies and Deemed Exports  </span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table6" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table6">
                                                                    <thead>
                                                                        <tr>
                                                                            <th rowspan="2">GSTIN of recipient</th>
                                                                            <th colspan="3">Invoice Details</th>
                                                                            <th colspan="2">Shipping bill/ Bill of export</th>
                                                                            <th colspan="3">Integrated Tax</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>No.</th>
                                                                            <th>Date</th>
                                                                            <th>Value</th>
                                                                            <th>No.</th>
                                                                            <th>Date</th>
                                                                            <th>Rate</th>
                                                                            <th>Taxable Value</th>
                                                                            <th>Amount</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                            <th class="c08">(8)</th>
                                                                            <th class="c09">(9)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th">
                                                                            <td colspan="9">6A. Exports</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR16A" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="BillGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01  text-center" />
                                                                                        <%--<asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />--%>
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 r" />
                                                                                        <asp:BoundField HeaderText="ShippingBillNo" DataField="ShippingBillNo" ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="ShippingBillDate" DataField="ShippingBillDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c06  text-center" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c07  text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r" />
                                                                                        <%--<asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 text-center" />--%>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr class="sub_th">
                                                                            <td colspan="9">6B. Supplies made to SEZ unit or SEZ Developer </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR16B" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="BillGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01  text-center" />
                                                                                        <%--<asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />--%>
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 r" />
                                                                                        <asp:BoundField HeaderText="ShippingBillNo" DataField="ShippingBillNo" ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="ShippingBillDate" DataField="ShippingBillDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c06  text-center" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c07  text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r" />
                                                                                        <%--<asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 text-center" />--%>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr class="sub_th">
                                                                            <td colspan="9" class="p0 b0">6C. Deemed exports </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="11">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4" runat="server" ShowHeader="false" ID="grdGSTR16C" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="BillGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01  text-center" />
                                                                                        <%--<asp:BoundField HeaderText="ClientGSTIN" DataField="PartyGSTIN" ItemStyle-CssClass="c01" />--%>
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="InvoiceValue" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 r" />
                                                                                        <asp:BoundField HeaderText="ShippingBillNo" DataField="ShippingBillNo" ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="ShippingBillDate" DataField="ShippingBillDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c06  text-center" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c07  text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 r" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r" />
                                                                                        <%--<asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c08 text-center" />--%>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 6-->
                                                    <!-- START Table 7-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table7">
                                                                    <span class="gstr_row_title">Table 7:</span>
                                                                    <span class="gstr_row_desc blue">Taxable supplies (Net of debit notes and credit notes) to unregistered persons other than the supplies covered in Table 5</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table7" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table7">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="c01" rowspan="2">Rate of tax</th>
                                                                            <th class="c02" rowspan="2">Total Taxable value</th>
                                                                            <th colspan="4">Amount</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c03">Integrated</th>
                                                                            <th class="c04">Central Tax</th>
                                                                            <th class="c05">State Tax/UT Tax</th>
                                                                            <th class="c06">Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">7A Intra-State supplies</td>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">7A(1) Consolidated rate wise outward supplies [including supplies made through e-commerce operator attracting TCS]</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="grd7A1" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">7A(2) Out of supplies mentioned at 7A(1), value of supplies made through e-Commerce Operators attracting TCS (operator wise, rate wise)</td>
                                                                        </tr>
                                                                       
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="grd7A2" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                          
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">7B. Inter-State Supplies where invoice value is upto Rs 2.5 Lakh [Rate wise]</td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="grd7B1" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">7B (2). Out of the supplies mentioned in 7B (1), the supplies made through e-Commerce Operators (operator wise, rate wise)</td>
                                                                        </tr>                                                                        
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="grd7B2" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 7-->
                                                    <!-- START Table 8-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <%-- <a href="#" class="btn btn-info pull-right btn-xs" style="margin-left: 10px;"><i class="fa r fa-pencil"></i>Modify</a>--%>
                                                            <%--<a href="#" class="btn btn-warning pull-right btn-xs" data-toggle="modal" data-target="#inputModal"><i class="fa r fa-download"></i>Import Data</a>--%>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table8">
                                                                    <span class="gstr_row_title">Table 8:</span>
                                                                    <span class="gstr_row_desc blue">Nil rated, exempted and non GST outward supplies</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table8" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table8">
                                                                    <thead>

                                                                        <tr>
                                                                            <th class="c01">Description</th>
                                                                            <th class="c02">Nil Rated Supplies</th>
                                                                            <th class="c03">Exempted (Other than Nil rated/non-GST supply)</th>
                                                                            <th class="c04">Non-GST supplies</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="4" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table8" runat="server" ShowHeader="false" ID="grd8" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="TypeDesc" DataField="TypeDesc" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="NilRatedAmt" DataField="NilRatedAmt" ItemStyle-CssClass="c02 r" />
                                                                                        <asp:BoundField HeaderText="EXEMPTEDAmt" DataField="EXEMPTEDAmt" ItemStyle-CssClass="c03 r" />
                                                                                        <asp:BoundField HeaderText="NonGSTAmt" DataField="NonGSTAmt" ItemStyle-CssClass="c04 r" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <%--    <tr>
                                                            <td class="c01">8A. Inter-State supplies to registered persons</td>
                                                            <td class="c02"></td>
                                                            <td class="c03"></td>
                                                            <td class="c04"></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="c01">8B. Intra- State supplies to registered persons</td>
                                                            <td class="c02"></td>
                                                            <td class="c03"></td>
                                                            <td class="c04"></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="c01">8C. Inter-State supplies to unregistered persons</td>
                                                            <td class="c02"></td>
                                                            <td class="c03"></td>
                                                            <td class="c04"></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="c01">8D. Intra-State supplies to unregistered persons</td>
                                                            <td class="c02"></td>
                                                            <td class="c03"></td>
                                                            <td class="c04"></td>
                                                        </tr>--%>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 8-->
                                                    <!-- START Table 9-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <%-- <a href="#" class="btn btn-info pull-right btn-xs" style="margin-left: 10px;"><i class="fa r fa-pencil"></i>Modify</a>--%>
                                                            <%--<a href="#" class="btn btn-warning pull-right btn-xs" data-toggle="modal" data-target="#inputModal"><i class="fa r fa-download"></i>Import Data</a>--%>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table9">
                                                                    <span class="gstr_row_title">Table 9:</span>
                                                                    <span class="gstr_row_desc blue">Amendments to taxable outward supply details furnished in returns for earlier tax periods in Table 4, 5 and 6 [including debit notes, credit notes, refund vouchers issued during current period and amendments thereof]</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table9" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table9">
                                                                    <thead>

                                                                        <tr>
                                                                            <th colspan="3">Details of original document</th>
                                                                            <th colspan="6">Revised details of document or details of original Debit/Credit Notes or refund vouchers</th>
                                                                            <th rowspan="3">Rate</th>
                                                                            <th rowspan="3">Taxable Value</th>
                                                                            <th colspan="4">Amount</th>
                                                                            <th rowspan="3">POS</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th rowspan="2">GSTIN</th>
                                                                            <th rowspan="2">Inv. No</th>
                                                                            <th rowspan="2">Inv. Date</th>
                                                                            <th rowspan="2">GSTIN</th>
                                                                            <th colspan="2">Invoice</th>
                                                                            <th colspan="2">Shipping bill</th>
                                                                            <th rowspan="2">Value</th>
                                                                            <th rowspan="2">Integrated Tax</th>
                                                                            <th rowspan="2">Central Tax</th>
                                                                            <th rowspan="2">State / UT Tax</th>
                                                                            <th rowspan="2">Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>No.</th>
                                                                            <th>Date</th>
                                                                            <th>No.</th>
                                                                            <th>Date</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                            <th class="c08">(8)</th>
                                                                            <th class="c09">(9)</th>
                                                                            <th class="c10">(10)</th>
                                                                            <th class="c11">(11)</th>
                                                                            <th class="c12">(12)</th>
                                                                            <th class="c13">(13)</th>
                                                                            <th class="c14">(14)</th>
                                                                            <th class="c15">(15)</th>
                                                                            <th class="c16">(16)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th">
                                                                            <td colspan="16">9A. If the invoice/Shipping bill details furnished earlier were incorrect </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="16" class="p0 b0">

                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table9" runat="server" ShowHeader="false" ID="grd9A" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="GSTIN" DataField="AmdGSTIN" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="AmdInvoiceNo" DataField="AmdInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="AmdInvoiceDate" DataField="AmdInvoiceDate" ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="BillGSTIN" DataField="BillGSTIN" ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="ShippingBillNo" DataField="ShippingBillNo" ItemStyle-CssClass="c07 r text-right" />
                                                                                        <asp:BoundField HeaderText="ShippingBillDate" DataField="ShippingBillDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c08 r text-right" />
                                                                                        <asp:BoundField HeaderText="CurrInvoiceVal" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c10 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c11  text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c12  text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c13  text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c14  text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c15  text-center" />
                                                                                        <asp:BoundField HeaderText="PosStateDesc" DataField="PartyStateDesc" ItemStyle-CssClass="c16 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                     

                                                                        <tr class="sub_th">
                                                                            <td colspan="16">9B. Debit Notes/Credit Notes/Refund voucher [original] </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="16" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table9" runat="server" ShowHeader="false" ID="grd9B" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="AmdGSTIN" DataField="AmdGSTIN" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="AmdInvoiceNo" DataField="AmdInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="AmdInvoiceDate" DataField="AmdInvoiceDate" ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="BillGSTIN" DataField="BillGSTIN" ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="ShippingBillNo" DataField="ShippingBillNo" ItemStyle-CssClass="c07 r text-right" />
                                                                                        <asp:BoundField HeaderText="ShippingBillDate" DataField="ShippingBillDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c08 r text-right" />
                                                                                        <asp:BoundField HeaderText="CurrInvoiceVal" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c10 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c11  text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c12  text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c13  text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c14  text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c15  text-center" />
                                                                                        <asp:BoundField HeaderText="PosStateDesc" DataField="PartyStateDesc" ItemStyle-CssClass="c16 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>

                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="16">9C. Debit Notes/Credit Notes/Refund voucher [amendments thereof]  </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="16" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table9" runat="server" ShowHeader="false" ID="grd9C" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="AmdGSTIN" DataField="AmdGSTIN" ItemStyle-CssClass="c01" />
                                                                                        <asp:BoundField HeaderText="AmdInvoiceNo" DataField="AmdInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="AmdInvoiceDate" DataField="AmdInvoiceDate" ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="BillGSTIN" DataField="BillGSTIN" ItemStyle-CssClass="c04  text-right" />
                                                                                        <asp:BoundField HeaderText="InvoiceNo" DataField="InvoiceNo" ItemStyle-CssClass="c05 r text-center" />
                                                                                        <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c06 r text-right" />
                                                                                        <asp:BoundField HeaderText="ShippingBillNo" DataField="ShippingBillNo" ItemStyle-CssClass="c07 r text-right" />
                                                                                        <asp:BoundField HeaderText="ShippingBillDate" DataField="ShippingBillDate" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-CssClass="c08 r text-right" />
                                                                                        <asp:BoundField HeaderText="CurrInvoiceVal" DataField="InvoiceAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c09 r text-right" />
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c10 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c11  text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c12  text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c13  text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c14  text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c15  text-center" />
                                                                                        <asp:BoundField HeaderText="PosStateDesc" DataField="PartyStateDesc" ItemStyle-CssClass="c16 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                           
                                                                        </tr>

                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 9-->
                                                    <!-- START Table 10-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <%-- <a href="#" class="btn btn-info pull-right btn-xs" style="margin-left: 10px;"><i class="fa r fa-pencil"></i>Modify</a>--%>
                                                            <%--<a href="#" class="btn btn-warning pull-right btn-xs" data-toggle="modal" data-target="#inputModal"><i class="fa r fa-download"></i>Import Data</a>--%>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table10">
                                                                    <span class="gstr_row_title">Table 10:</span>
                                                                    <span class="gstr_row_desc blue">Amendments to taxable outward supplies to unregistered persons furnished in returns for earlier tax periods in Table 7</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table10" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table10">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="c01" rowspan="2">Rate of tax</th>
                                                                            <th class="c02" rowspan="2">Total Taxable value</th>
                                                                            <th colspan="4">Amount</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c03">Integrated</th>
                                                                            <th class="c04">Central Tax</th>
                                                                            <th class="c05">State Tax/UT Tax</th>
                                                                            <th class="c06">Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th">
                                                                            <td colspan="2">Tax period for which the details are being revised</td>
                                                                            <td colspan="4">July</td>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">10A. Intra-State Supplies [including supplies made through e-commerce operator attracting TCS] [Rate wise]</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="Grd10A" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            <%-- <td class="c01">&nbsp;</td>
                                                            <td class="c02">&nbsp;</td>
                                                            <td class="c03">&nbsp;</td>
                                                            <td class="c04">&nbsp;</td>
                                                            <td class="c05">&nbsp;</td>
                                                            <td class="c06">&nbsp;</td>--%>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">10A (1). Out of supplies mentioned at 10A, value of supplies made through e-Commerce Operators attracting TCS (operator wise, rate wise)</td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="Grd10A1" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            <%-- <td class="c01">&nbsp;</td>
                                                            <td class="c02">&nbsp;</td>
                                                            <td class="c03">&nbsp;</td>
                                                            <td class="c04">&nbsp;</td>
                                                            <td class="c05">&nbsp;</td>
                                                            <td class="c06">&nbsp;</td>--%>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">10B. Inter-State Supplies [including supplies made through e-commerce operator attracting TCS] [Rate wise]</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">Place of Supply (Name of State)</td>
                                                                            <td colspan="4">MP</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="Grd10B" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                           
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="6">10B (1). Out of supplies mentioned at 10B, value of supplies made through e-Commerce Operators attracting TCS (operator wise, rate wise)</td>
                                                                        </tr>
                                                                       
                                                                        <tr>
                                                                            <td colspan="6" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table7" runat="server" ShowHeader="false" ID="Grd10B1" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="Rate" DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField HeaderText="Taxableval" DataField="TaxableAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTTax" DataField="IGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField HeaderText="CGSTTax" DataField="CGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField HeaderText="SGSTTax" DataField="SGSTAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="Cess" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 10-->
                                                    <!-- START Table 11-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <%-- <a href="#" class="btn btn-info pull-right btn-xs" style="margin-left: 10px;"><i class="fa r fa-pencil"></i>Modify</a>--%>
                                                            <%--<a href="#" class="btn btn-warning pull-right btn-xs" data-toggle="modal" data-target="#inputModal"><i class="fa r fa-download"></i>Import Data</a>--%>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table11">
                                                                    <span class="gstr_row_title">Table 11:</span>
                                                                    <span class="gstr_row_desc blue">Amendments to taxable outward supplies to unregistered persons furnished in returns for earlier tax periods in Table 7</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table11" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table11">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="c01" rowspan="2">Rate</th>
                                                                            <th class="c02" rowspan="2">Gross Advance Received/adjusted</th>
                                                                            <th class="c03" rowspan="2">Place of supply (Name of State)</th>
                                                                            <th colspan="4">Amount</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c04">Integrated</th>
                                                                            <th class="c05">Central Tax</th>
                                                                            <th class="c06">State Tax/UT Tax</th>
                                                                            <th class="c07">Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr class="sub_th big">
                                                                            <td colspan="7">(i) Information for the current tax period</td>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="7">11A. Advance amount received in the tax period for which invoice has not been issued (tax amount to be added to output tax liability)</td>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="7">11A (1). Intra-State supplies (Rate Wise)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7" class="p0 b0">
                                                                                <asp:GridView runat="server" ShowHeader="false" ID="grd11A1" CssClass="table table-bordered table-gstr table_psm table11" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField DataField="GrossReceived" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 l" />
                                                                                        <asp:BoundField DataField="IGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField DataField="CGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField DataField="SGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 text-center" />
                                                                                        <asp:BoundField DataField="Cess" ItemStyle-CssClass="c07 text-center" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="7">11A (2). Inter-State Supplies (Rate Wise)</td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td colspan="7" class="p0 b0">
                                                                                <asp:GridView runat="server" ShowHeader="false" ID="grd11A2" CssClass="table table-bordered table-gstr table_psm table11" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField DataField="GrossReceived" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 l" />
                                                                                        <asp:BoundField DataField="IGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField DataField="CGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField DataField="SGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 text-center" />
                                                                                        <asp:BoundField DataField="Cess" ItemStyle-CssClass="c07 text-center" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            <%-- <td class="c01">&nbsp;</td>
                                                            <td class="c02">&nbsp;</td>
                                                            <td class="c03">&nbsp;</td>
                                                            <td class="c04">&nbsp;</td>
                                                            <td class="c05">&nbsp;</td>
                                                            <td class="c06">&nbsp;</td>
                                                            <td class="c07">&nbsp;</td>--%>
                                                                        </tr>

                                                                        <tr class="sub_th">
                                                                            <td colspan="7">11B. Advance amount received in earlier tax period and adjusted against the supplies being shown in this tax period in Table Nos. 4, 5, 6 and 7</td>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="7">11B (1). Intra-State Supplies (Rate Wise)</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="7" class="p0 b0">
                                                                                <asp:GridView runat="server" ShowHeader="false" ID="grd11B1" CssClass="table table-bordered table-gstr table_psm table11" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField DataField="GrossReceived" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 l" />
                                                                                        <asp:BoundField DataField="IGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField DataField="CGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField DataField="SGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 text-center" />
                                                                                        <asp:BoundField DataField="Cess" ItemStyle-CssClass="c07 text-center" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            <%--                                                    <td class="c01">&nbsp;</td>
                                                            <td class="c02">&nbsp;</td>
                                                            <td class="c03">&nbsp;</td>
                                                            <td class="c04">&nbsp;</td>
                                                            <td class="c05">&nbsp;</td>
                                                            <td class="c06">&nbsp;</td>
                                                            <td class="c07">&nbsp;</td>--%>
                                                                        </tr>
                                                                        <tr class="sub_th">
                                                                            <td colspan="7">11B (2). Inter-State Supplies (Rate Wise)</td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td colspan="7" class="p0 b0">
                                                                                <asp:GridView runat="server" ShowHeader="false" ID="grd11B2" CssClass="table table-bordered table-gstr table_psm table11" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="TaxRate" DataFormatString="{0:C}"  ItemStyle-CssClass="c01 text-center" />
                                                                                        <asp:BoundField DataField="GrossReceived" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField DataField="PartyStateDesc" ItemStyle-CssClass="c03 l" />
                                                                                        <asp:BoundField DataField="IGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField DataField="CGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField DataField="SGST" DataFormatString="{0:C}"  ItemStyle-CssClass="c06 text-center" />
                                                                                        <asp:BoundField DataField="Cess" ItemStyle-CssClass="c07 text-center" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            <%--                                                    <td class="c01">&nbsp;</td>
                                                            <td class="c02">&nbsp;</td>
                                                            <td class="c03">&nbsp;</td>
                                                            <td class="c04">&nbsp;</td>
                                                            <td class="c05">&nbsp;</td>
                                                            <td class="c06">&nbsp;</td>
                                                            <td class="c07">&nbsp;</td>--%>
                                                                        </tr>
                                                                        <tr class="sub_th big">
                                                                            <td colspan="7">(ii)  Amendment of information furnished in Table No. 11[1] in GSTR-1 statement for earlier tax periods [Furnish revised information]</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="c01">Month</td>
                                                                            <td class="c02"></td>
                                                                            <td colspan="4">Amendment relating to information furnished in S.No.(select)</td>
                                                                            <td class="c07">11A(1)11A(2)11B(1)11B(2)
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="c01">&nbsp;</td>
                                                                            <td class="c02">&nbsp;</td>
                                                                            <td class="c03">&nbsp;</td>
                                                                            <td class="c04">&nbsp;</td>
                                                                            <td class="c05">&nbsp;</td>
                                                                            <td class="c06">&nbsp;</td>
                                                                            <td class="c07">&nbsp;</td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 11-->
                                                    <!-- START Table 12-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <%-- <a href="#" class="btn btn-info pull-right btn-xs" style="margin-left: 10px;"><i class="fa r fa-pencil"></i>Modify</a>--%>
                                                            <%--<a href="#" class="btn btn-warning pull-right btn-xs" data-toggle="modal" data-target="#inputModal"><i class="fa r fa-download"></i>Import Data</a>--%>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table12">
                                                                    <span class="gstr_row_title">Table 12:</span>
                                                                    <span class="gstr_row_desc blue">HSN-wise summary of outward supplies</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table12" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table12">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="c01" rowspan="2">Sr. No.</th>
                                                                            <th class="c02" rowspan="2">HSN</th>
                                                                            <th class="c03" rowspan="2">Description (Optional if HSN is provided)</th>
                                                                            <th class="c04" rowspan="2">UQC</th>
                                                                            <th class="c05" rowspan="2">Total Quantity</th>
                                                                            <th class="c06" rowspan="2">Total value</th>
                                                                            <th class="c07" rowspan="2">Total Taxable Value</th>
                                                                            <th colspan="4">Amount</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c08">Integrated Tax</th>
                                                                            <th class="c09">Central Tax</th>
                                                                            <th class="c10">State/UT Tax</th>
                                                                            <th class="c11">Cess</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                            <th class="c08">(8)</th>
                                                                            <th class="c09">(9)</th>
                                                                            <th class="c10">(10)</th>
                                                                            <th class="c11">(11)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="11" class="p0 b0">
                                                                                <asp:GridView CssClass="table table-bordered table-gstr table_psm table4 break" runat="server" ShowHeader="false" ID="grdHSNSumm12" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField  ItemStyle-CssClass="c01 text-center">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:BoundField HeaderText="HSNSACCode" DataField="HSNSACCode" ItemStyle-CssClass="c02 text-center" />
                                                                                        <asp:BoundField HeaderText="HSNSACCode" DataField="HSNSACDesc" ItemStyle-CssClass="c03" />
                                                                                        <asp:BoundField HeaderText="UQC" DataField="UQC" ItemStyle-CssClass="c04 r" />
                                                                                        <asp:BoundField HeaderText="ItemQty" DataField="ItemQty" ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField HeaderText="GrossAmount" DataField="GrossAmount" ItemStyle-CssClass="c06  text-center" />
                                                                                        <asp:BoundField HeaderText="TaxableAmount" DataField="TaxableAmount" ItemStyle-CssClass="c07  text-center" />
                                                                                        <asp:BoundField HeaderText="IGSTAmount" DataField="IGSTAmount" ItemStyle-CssClass="c08 r" />
                                                                                        <asp:BoundField HeaderText="CGSTAmount" DataField="CGSTAmount" ItemStyle-CssClass="c09 r" />
                                                                                        <asp:BoundField HeaderText="SGSTAmount" DataField="SGSTAmount" ItemStyle-CssClass="c10 r" />
                                                                                        <asp:BoundField HeaderText="CessAmount" DataField="CessAmount" DataFormatString="{0:C}"  ItemStyle-CssClass="c11 r" />

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                        <%-- <tr>
                                                            <td class="c01">&nbsp;</td>
                                                            <td class="c02">&nbsp;</td>
                                                            <td class="c03">&nbsp;</td>
                                                            <td class="c04">&nbsp;</td>
                                                            <td class="c05">&nbsp;</td>
                                                            <td class="c06">&nbsp;</td>
                                                            <td class="c07">&nbsp;</td>
                                                            <td class="c08">&nbsp;</td>
                                                            <td class="c09">&nbsp;</td>
                                                            <td class="c10">&nbsp;</td>
                                                            <td class="c11">&nbsp;</td>

                                                        </tr>--%>
                                                                    </tbody>

                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <!-- END Table 12-->
                                                    <!-- START Table 13-->
                                                    <div class="panel panel-default panel-demo">
                                                        <div class="panel-heading" style="padding-bottom: 14px">
                                                            <a href="#" data-perform="panel-refresh" data-spinner="traditional" title="" class="pull-right noExl" data-original-title="Refresh Panel">
                                                                <em class="fa fa-refresh"></em>
                                                            </a>

                                                            <%-- <a href="#" class="btn btn-info pull-right btn-xs" style="margin-left: 10px;"><i class="fa r fa-pencil"></i>Modify</a>--%>
                                                            <%--<a href="#" class="btn btn-warning pull-right btn-xs" data-toggle="modal" data-target="#inputModal"><i class="fa r fa-download"></i>Import Data</a>--%>
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" href="#table13">
                                                                    <span class="gstr_row_title">Table 13:</span>
                                                                    <span class="gstr_row_desc blue">Documents issued during the tax period</span>
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="table13" class="panel-collapse collapse" style="clear: both; width: 100%">
                                                            <div class="panel-table">

                                                                <table class="table table-bordered table-gstr table_psm table13">
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="c01" rowspan="2">Sr. No.</th>
                                                                            <th class="c02" rowspan="2">Nature of document</th>
                                                                            <th class="c03" colspan="2">Sr. No.</th>
                                                                            <th class="c05" rowspan="2">Total number</th>
                                                                            <th class="c06" rowspan="2">Cancelled</th>
                                                                            <th class="c07" rowspan="2">Net issued</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c03">From</th>
                                                                            <th class="c04">To</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th class="c01">(1)</th>
                                                                            <th class="c02">(2)</th>
                                                                            <th class="c03">(3)</th>
                                                                            <th class="c04">(4)</th>
                                                                            <th class="c05">(5)</th>
                                                                            <th class="c06">(6)</th>
                                                                            <th class="c07">(7)</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="p0 b0" colspan="7">
                                                                                <asp:GridView runat="server" ShowHeader="false" ID="grd13" CssClass="table table-bordered table-gstr table_psm table11" AutoGenerateColumns="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField ItemStyle-CssClass="c01 text-center">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex+1 %>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="DocIssueTypeDesc" ItemStyle-CssClass="c02 l" />
                                                                                        <asp:BoundField DataField="FromNo" ItemStyle-CssClass="c03 text-center" />
                                                                                        <asp:BoundField DataField="ToNo" ItemStyle-CssClass="c04 text-center" />
                                                                                        <asp:BoundField DataField="TotalNo" ItemStyle-CssClass="c05 text-center" />
                                                                                        <asp:BoundField DataField="Cancel" ItemStyle-CssClass="c06 text-center" />
                                                                                        <asp:BoundField DataField="NetIssue" ItemStyle-CssClass="c07 text-center" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>                                                                            
                                                                        </tr>                                                                        
                                                                    </tbody>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- END Table 13-->
                                                </td>
                                            </tr>
                                        </table>
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
