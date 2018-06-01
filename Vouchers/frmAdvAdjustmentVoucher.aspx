<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmAdvAdjustmentVoucher.aspx.cs" Inherits="Vouchers_frmAdvAdjustmentVoucher" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>

            <div class="content-wrapper">

                <h3 class="text-center head">ADVANCE ADJUSTMENT VOUCHER 
                    <span class="invoiceHead">Last Voucher No. &amp; Date : 100 - 01/07/2017</span>
                </h3>

                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default pmb0">
                            <div class="panel-body">
                                <div class="col-xs-12">
                                   
                                       <table class="aav_tbl1 table-bordered mb20">
                                           <tr class="inf_head"><td colspan="5"></td></tr>
                                    <tr>
                                        <th class="aav_tbl1_col1">Account Head</th>
                                        <th class="aav_tbl1_col2">GSTIN</th>
                                        <th class="aav_tbl1_col3">Item Name</th>
                                        <th class="aav_tbl1_col4">HSN</th>
                                        <th class="aav_tbl1_col5"></th>
                                        </tr>
                                        <tr>
                                        <td class="aav_tbl1_col1"><select><option></option></select></td>
                                        <td class="aav_tbl1_col2"><select><option></option></select></td>
                                        <td class="aav_tbl1_col3"><select><option></option></select></td>
                                        <td class="aav_tbl1_col4"><select><option></option></select></td>
                                        <td class="aav_tbl1_col5 c"><a class="btn btn-primary btn-xs">Go</a></td>
                                       
                                        </tr>
                                              
                                    </table>

                                    <table class=" table-bordered table_aav mb20">
                                        <tr class="inf_head"><td colspan="4" ></td></tr>
                                        <tr>
                                            <td class="table_aav_col4"></td>
                                            <th class="table_aav_col1">Unique No</th>
                                            <th class="table_aav_col2">Pending Amount</th>
                                            <th class="table_aav_col3">Adjust Amount</th>
                                           
                                        </tr>
                                         <tr>
                                            <td class="table_aav_col4"><input type="checkbox" name="Unique" value="Bike"></td>
                                            <td class="table_aav_col1"></td>
                                            <td class="table_aav_col2"></td>
                                            <td class="table_aav_col3"><input type="text" placeholder="Adjusted Amount" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="table_aav_col4"><input type="checkbox" name="Unique" value="Bike"></td>
                                            <td class="table_aav_col1"></td>
                                            <td class="table_aav_col2"></td>
                                            <td class="table_aav_col3"><input type="text" placeholder="Adjusted Amount" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td class="table_aav_col4"><input type="checkbox" name="Unique" value="Bike"></td>
                                            <td class="table_aav_col1"></td>
                                            <td class="table_aav_col2"></td>
                                            <td class="table_aav_col3"><input type="text" placeholder="Adjusted Amount" /></td>
                                            
                                        </tr>
                                    </table>

                                      <table class=" table-bordered table_aav3 mb20">
                                        <tr class="inf_head"><td colspan="4" ></td></tr>
                                        <tr>
                                            <td class="table_aav3_col4"></td>
                                            <th class="table_aav3_col1">Invoice No</th>
                                            <th class="table_aav3_col2">Invoice Amount</th>
                                            <th class="table_aav3_col3">Adjust Amount</th>
                                           
                                        </tr>
                                         <tr>
                                            <td class="table_aav3_col4"><input type="checkbox" name="Unique" value="Bike"></td>
                                            <td class="table_aav3_col1"></td>
                                            <td class="table_aav3_col2"></td>
                                            <td class="table_aav3_col3"><input type="text" placeholder="Adjusted Amount"</td>
                                            
                                        </tr>
                                          <tr>
                                            <td class="table_aav3_col4"><input type="checkbox" name="Unique" value="Bike"></td>
                                            <td class="table_aav3_col1"></td>
                                            <td class="table_aav3_col2"></td>
                                            <td class="table_aav3_col3"><input type="text" placeholder="Adjusted Amount" /></td>
                                            
                                        </tr>
                                          <tr>
                                            <td class="table_aav3_col4"><input type="checkbox" name="Unique" value="Bike"></td>
                                            <td class="table_aav3_col1"></td>
                                            <td class="table_aav3_col2"></td>
                                            <td class="table_aav3_col3"><input type="text" placeholder="Adjusted Amount" /></td>
                                            
                                        </tr>
                                    </table>

                                     <table class="aav_tbl4 table-bordered mb20">
                                          
                                    <tr class="inf_head">
                                        <td class="aav_tbl4_col1">Total Amount</td>
                                        <td class="aav_tbl4_col2">Pending Amount</td>
                                        </tr>
                                        <tr>
                                        <td class="aav_tbl4_col1"><input type="text" /></td>
                                        <td class="aav_tbl4_col2"><input type="text" /></td>
                                       
                                        </tr>
                                              
                                    </table>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-space-right" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger btn-space-right" />
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

