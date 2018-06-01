<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmGSTR2.aspx.cs" Inherits="frmGSTR2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script>
          $(document).ready(function () {
              $('.btn_back_url').attr('href', 'frmTaxPayerSearch.aspx');
              $('.gstr_sitemap').append('<li class="breadcrumb-item active">Client</li>');
              $('.liImportExcel').show();
          });

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section id="gstr2_page">
        <!-- START Page content-->
        <div class="content-wrapper">
           
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
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
                                                    <th class="c01" rowspan="2">GSTIN Of Supplier</th>
                                                    <th class="c02" colspan="3">Invoice Detail</th>
                                                    <th class="c05" rowspan="2">Rate</th>
                                                    <th class="c06" rowspan="2">Taxable Value</th>
                                                    <th class="c07" colspan="4">Amount Tax</th>
                                                    <th class="c11" rowspan="2">Place Of Supply (Name Of State)</th>
                                                    <th class="c12" rowspan="2">Whether input or input service / Capital goods ( incl plant and machinery ) / Ineligible for ITC </th>
                                                    <th class="c13" colspan="4">Amount of ITC available</th>
                                                </tr>
                                                <tr>
                                                    <th class="c02">No</th>
                                                    <th class="c03">Date</th>
                                                    <th class="c04">Value</th>
                                                    <th class="c07">Integrated tax</th>
                                                    <th class="c08">Central Tax</th>
                                                    <th class="c09">State / UTTax</th>
                                                    <th class="c10">CESS</th>
                                                    <th class="c13">Integrated tax</th>
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
                                                    <th class="c12">12</th>
                                                    <th class="c13">13</th>
                                                    <th class="c14">14</th>
                                                    <th class="c15">15</th>
                                                    <th class="c16">16</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td colspan="16" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table3" ID="GrdTable3" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c16 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>                                                    
                                                </tr>
                                            </tbody>
                                        </table>
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
                                                    <th class="c05" rowspan="2">Rate</th>
                                                    <th class="c06" rowspan="2">Taxable Value</th>
                                                    <th class="c07" colspan="4">Amount Tax</th>
                                                    <th class="c11" rowspan="2">Place Of Supply(Name Of State)</th>
                                                    <th class="c12" rowspan="2">Whether input or input service/ Capital goods (incl plant and machinery)/ Ineligible for ITC</th>
                                                    <th class="c13" colspan="4">Amount of ITC available</th>
                                                </tr>
                                                <tr>
                                                    <th class="c02">No</th>
                                                    <th class="c03">Date</th>
                                                    <th class="c04">Value</th>
                                                    <th class="c07">Integrated tax</th>
                                                    <th class="c08">Central Tax</th>
                                                    <th class="c09">State/UTTax</th>
                                                    <th class="c10">CESS</th>
                                                    <th class="c13">Integrated tax</th>
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
                                                    <th class="c12">12</th>
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
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table4" ID="Grd4A" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                  <asp:BoundField DataField="PurchGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c16 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                  
                                                </tr>
                                              
                                                <tr>
                                                    <td class="c01" colspan="16">4B. Inward supplies received from an unregistered supplier</td>
                                                </tr>
                                                <tr>
                                                     <td colspan="16" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table4"  ID="Grd4B" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                  <asp:BoundField DataField="PurchGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c16 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td class="c01" colspan="16">4C. Import of service</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="16" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table4" ID="grd4C" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                 <asp:BoundField DataField="PurchGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c16 text-center" />
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
                                                    <th class="c05" rowspan="2">Rate</th>
                                                    <th class="c06" rowspan="2">Taxable value</th>
                                                    <th class="c07" colspan="2">Amount</th>
                                                    <th class="c09" rowspan="2">Whether input / Capital goods ( incl. plant and machinery ) / Ineligible for ITC</th>
                                                    <th class="c10" colspan="2">Amount of ITC available</th>
                                                </tr>
                                                <tr>
                                                    <th class="c02">No.</th>
                                                    <th class="c03">Date</th>
                                                    <th class="c04">Value</th>
                                                    <th class="c07">Integrated tax</th>
                                                    <th class="c08">CESS</th>
                                                    <th class="c10">Integrated tax</th>
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
                                                    <th class="c09">9</th>
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
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table5" ID="grd5A" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c11 text-center" />
                                                               
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>                                                    
                                                </tr>                                               
                                                <tr>
                                                    <td class="c01" colspan="11">5B. Received from SEZ</td>
                                                </tr>
                                                <tr>
                                                     <td colspan="11" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table5" ID="grd5B" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchGSTIN" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c11 text-center" />
                                                               
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
                                                    <th class="c08" rowspan="2">Rate</th>
                                                    <th class="c09" rowspan="2">Taxable value</th>
                                                    <th class="c10" colspan="4">Amount</th>
                                                    <th class="c14" rowspan="2">Place of supply</th>
                                                    <th class="c15" rowspan="2">Whether input or input service / Capital goods / Ineligible for ITC )</th>
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
                                                    <th class="c15">15</th>
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
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table6" ID="grd6A" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                 <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c16 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c17 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c18 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c19 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>                                                   
                                                </tr>
                                                <tr>
                                                    <td class="c01" colspan="19">6B. Supplies by way of import of goods or goods received from SEZ [Information furnished in Table 5 of earlier returns]-If details furnished earlier were incorrect</td>
                                                </tr>
                                                <tr>
                                                     <td colspan="19" class="p0 b0">
                                                        <asp:GridView runat="server"  CssClass="table table-gstr table_psm table6" ID="grd6B" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c16 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c17 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c18 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c19 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>                                                   
                                                </tr>
                                                <tr>
                                                    <td class="c01" colspan="19">6C. Debit Notes/Credit Notes [original]</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="19" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table6" ID="grd6C" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                     <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c16 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c17 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c18 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c19 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>                                                   
                                                </tr>
                                                <tr>
                                                    <td class="c01" colspan="19">6D. Debit Notes/ Credit Notes [amendment of debit notes/credit notes furnished in earlier tax periods]</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="19" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table6" ID="grd6D" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                               <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c03 text-center" />
                                                                 <asp:BoundField DataField="PurchGSTIN"  ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceNo" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceDate" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="CurrInvoiceValue" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="TaxableValue" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="IGSTAmt" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="CGSTAmt" ItemStyle-CssClass="c11 text-center" />
                                                                <asp:BoundField DataField="SGSTAmt" ItemStyle-CssClass="c12 text-center" />
                                                                <asp:BoundField DataField="CessAmt" ItemStyle-CssClass="c13 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c14 text-center" />
                                                                <asp:BoundField DataField="ITCDescDesc" ItemStyle-CssClass="c15 text-center" />
                                                                <asp:BoundField DataField="ITCIGSTTax" ItemStyle-CssClass="c16 text-center" />
                                                                <asp:BoundField DataField="ITCCGSTTax" ItemStyle-CssClass="c17 text-center" />
                                                                <asp:BoundField DataField="ITCSGSTTax" ItemStyle-CssClass="c18 text-center" />
                                                                <asp:BoundField DataField="ITCCessTax" ItemStyle-CssClass="c19 text-center" />
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
                                            <span class="gstr_row_desc blue">Supplies received from composition taxable person and other exempt/Nil rated/Non GST supplies received</span>
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
                                                    <th class="c04">Nil Rated supply</th>
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
                                                        <asp:GridView  CssClass="table table-gstr table_psm table7" runat="server" ID="grd7A" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Tbl7Desc" ItemStyle-CssClass="c01 text-left" />
                                                                <asp:BoundField DataField="ComposAmt" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="ExempAmt" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="NilRatedAmt" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="NonGstAmt" ItemStyle-CssClass="c05 text-center" />
                                                               
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
                                                        <asp:GridView CssClass="table table-gstr table_psm table7" runat="server" ID="grd7B" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Tbl7Desc" ItemStyle-CssClass="c01 text-left" />
                                                                <asp:BoundField DataField="ComposAmt" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="ExempAmt" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="NilRatedAmt" ItemStyle-CssClass="c04 text-center" />
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
                                                    <th class="c04">Integrated Tax</th>
                                                    <th class="c05">Central Tax</th>
                                                    <th class="c06">State/ UT Tax</th>
                                                    <th class="c07">Cess</th>
                                                    <th class="c08">Integrated Tax</th>
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
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table8" ID="grd8" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="GSTINISD" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="ISDDocNo" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="ISDDocDate" ItemStyle-CssClass="c03 text-center" />
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
                                                    <th class="c05">Integrated Tax</th>
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
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table9" ID="grd9" AutoGenerateColumns="false" ShowHeader="false">
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
                                                    <th class="c01" rowspan="2">Rate</th>
                                                    <th class="c02" rowspan="2">Gross Advance Paid</th>
                                                    <th class="c03" rowspan="2">Place of supply (Name of State)</th>
                                                    <th class="c04" colspan="7">Amount</th>
                                                </tr>
                                                <tr>

                                                    <th class="c04">Integrated Tax</th>
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
                                                    <td class="c01" colspan="10">10A (1). Intra-State supplies (Rate Wise)</td>
                                                </tr>
                                                <tr>
                                                      <td colspan="10" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table10" ID="grd10A1" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="Rate" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c07 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="c01" colspan="10">10A (2). Inter -State Supplies (Rate Wise)</td>
                                                </tr>
                                                <tr>
                                                      <td colspan="10" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table10" ID="grd10A2" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                 <asp:BoundField DataField="Rate" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c03 text-center" />
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
                                                    <td class="c01" colspan="10">10B (1). Intra-State Supplies (Rate Wise)</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="10" class="p0 b0">
                                                        <asp:GridView runat="server" CssClass="table table-gstr table_psm table10" ID="grd10b1" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                 <asp:BoundField DataField="Rate" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="IGSTTax" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="CGSTTax" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="SGSTTax" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="CessTax" ItemStyle-CssClass="c07 text-center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td class="c01" colspan="10">10B (2). Intra-State Supplies (Rate Wise)</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="10" class="p0 b0">
                                                        <asp:GridView CssClass="table table-gstr table_psm table10" runat="server" ID="grd10b2" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                  <asp:BoundField DataField="Rate" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c03 text-center" />
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
                                                        <asp:GridView CssClass="table table-gstr table_psm table10" runat="server" ID="grd10Amd" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                 <asp:BoundField DataField="Rate" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="GrossAdvPaid" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="PosStateDesc" ItemStyle-CssClass="c03 text-center" />
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
                                                        <asp:GridView CssClass="table table-gstr table_psm table11" runat="server" ID="grd11" AutoGenerateColumns="false" ShowHeader="false">
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


                                                    <th class="c03">Integrated</th>
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

                                        <table class="table table-bordered table-gstr table_psm table13">
                                            <thead>
                                               
                                                <tr>
                                                    <th class="c01">Sr. No.</th>
                                                    <th class="c02">HSN</th>
                                                    <th class="c03">Description</th>
                                                    <th class="c04">UQC</th>
                                                    <th class="c05">Total</th>
                                                    <th class="c06">Total</th>
                                                    <th class="c07">Total</th>
                                                    <th class="c08" colspan="4">Amount</th>
                                                </tr>
                                                <tr>
                                                    <th class="c01">1</th>
                                                    <th class="c02">2</th>
                                                    <th class="c03">(Optional if HSN is furnished)</th>
                                                    <th class="c04">4</th>
                                                    <th class="c05">Quantity</th>
                                                    <th class="c06">value</th>
                                                    <th class="c07">Taxable Value</th>



                                                    <th class="c08">Integrated Tax</th>
                                                    <th class="c09">Central Tax</th>
                                                    <th class="c10">State/UT Tax</th>
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
                                                        <asp:GridView runat="server" ID="grd13" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c01 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c02 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c03 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c04 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c05 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c06 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c07 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c08 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c09 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c10 text-center" />
                                                                <asp:BoundField DataField="" ItemStyle-CssClass="c11 text-center" />
                                                               
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
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!-- END Page content-->
    </section>
</asp:Content>

