<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmOpenStockEntry.aspx.cs" Inherits="Vouchers_frmOpenStockEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .hd {
            background-color: #1c75bf;
            color: #fff;
        }

        .bill_table1 {
            width: 100%;
            margin: 11px;
        }

        .bill_table1_col1 {
            width: 25%;
        }

        .bill_table1_col2 {
            width: 25%;
        }

        .bill_table1_col3 {
            width: 25%;
        }

        .bill_table1_col4 {
            width: 25%;
        }

        .fnt {
            font-size: 12px;
        }

        .r {
            padding-left: 2px;
        }

        .Item_table1 {
            width: 100%;
            min-width: 425px;
            font-size: 13px !important;
        }

        .Item_table1_col1 {
            width: 18%;
        }

        .Item_table1_col2 {
            width: 11%;
        }

        .Item_table1_col3 {
            width: 13%;
        }

        .Item_table1_col4 {
            width: 11%;
        }

        .Item_table1_col5 {
            width: 13%;
        }

        .Item_table1_col6 {
            width: 12%;
        }

        .Item_table1_col7 {
            width: 16%;
        }

        .Item_table1_col8 {
            width: 05%;
        }


        td span.item_overflow131 {
            overflow: hidden;
            text-overflow: ellipsis;
            width: 90px;
            white-space: nowrap;
            display: inline-block;
            border: 0;
        }

        td span.item_overflow200 {
            overflow: hidden;
            text-overflow: ellipsis;
            width: 170px;
            white-space: nowrap;
            display: inline-block;
            border: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <h3 class="text-center p5">Item Opening Stock Entry
        </h3>
    </div>
    <div class="container_fluid">
        <div class="col-xs-12">
            <div class="row">
                <div class="col-sm-12">
                   
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-sm-12">
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <label class="col-sm-3 alphaonly control-label">Item Name</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" CssClass="relative_gt chzn-select" Width="250px" DropDownStyle="Simple"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <label class="col-sm-3 alphaonly control-label">Description</label>
                                                        <div class="col-sm-9">
                                                            <asp:Label ID="lblDisc" runat="server" style="padding-top: 8px;display: inline-block;"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <table class="Item_table1 table-bordered">
                                                        <thead class="hd">
                                                            <tr>
                                                                <th colspan="8" class="text-center"><strong>Item Opening Stock Details</strong></th>
                                                            </tr>
                                                            <tr>
                                                                <th class="Item_table1_col1" rowspan="2"><strong class="r">IDA Address</strong></th>
                                                                <th class="Item_table1_col2" colspan="2"><strong class="r">Primary</strong></th>
                                                                <th class="Item_table1_col4" colspan="2"><strong class="r">Secondary</strong></th>
                                                                <th class="Item_table1_col6" rowspan="2"><strong class="r">Rate</strong></th>
                                                                <th class="Item_table1_col7" rowspan="2"><strong class="r">Date</strong></th>
                                                                <th class="Item_table1_col8" rowspan="2"><strong class="r">&nbsp;</strong></th>
                                                            </tr>
                                                            <tr>

                                                                <th class="Item_table1_col2"><strong class="r">Unit</strong></th>
                                                                <th class="Item_table1_col3"><strong class="r">Qty</strong></th>
                                                                <th class="Item_table1_col4"><strong class="r">Unit</strong></th>
                                                                <th class="Item_table1_col5"><strong class="r">Qty</strong></th>
                                                            </tr>
                                                        </thead>
                                                        <tr>
                                                            <td class="Item_table1_col1 r"><asp:DropDownList ID="ddlwarehouse" runat="server"></asp:DropDownList></td>
                                                            <td class="Item_table1_col2 r"><asp:DropDownList ID="ddlPriUnit" runat="server" Enabled="false"></asp:DropDownList></td>
                                                            <td class="Item_table1_col3 r"><asp:TextBox ID="txtPriQty" runat="server" CssClass="numberonly" MaxLength="7" placeholder="Qty" Enabled="false"></asp:TextBox></td>
                                                            <td class="Item_table1_col4 r"><asp:DropDownList ID="ddlsecunit" runat="server" Enabled="false" ></asp:DropDownList></td>
                                                            <td class="Item_table1_col5 r"><asp:TextBox ID="txtsecQty" runat="server" CssClass="numberonly" MaxLength="7" placeholder="Qty" Enabled="false"></asp:TextBox></td>
                                                            <td class="Item_table1_col6 r"><asp:TextBox ID="txtOpRate" runat="server" CssClass="Money" MaxLength="9" placeholder="Rate"></asp:TextBox></td>
                                                            <td class="Item_table1_col7 r"><asp:TextBox ID="txtDate" runat="server" CssClass="datepicker" MaxLength="10" placeholder="Date"></asp:TextBox></td>
                                                            <td class="Item_table1_col6 ">
                                                                <asp:Button ID="btnAdd" runat="server" Style="width: 100%" CssClass="btn btn-primary btn-xs fa r fa-plus" Text="Add" OnClick="btnAdd_Click"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                    <div style="overflow-y: scroll; min-height: 80px; width: calc(100% + 7px); max-height: 106px;">
                                                        <asp:GridView ID="gridOpenStock" CssClass="Item_table1 table-bordered" runat="server" AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="gridOpenStock_RowCommand" OnRowDataBound="gridOpenStock_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAddress" Text='<%#Eval("Address") %>'  title='<%#Eval("WareHouseID") %>' runat="server" CssClass="item_overflow131"></asp:Label>
                                                                        <asp:Label ID="lblWarehouseId" Text='<%#Eval("WareHouseID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col2 r">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrimaryUnit" Text='<%#Eval("ItemUnitID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col3 r">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrimaryQty" Text='<%#Eval("ItemOpeningQty") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col4 r">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSecondryUnit" Text='<%#Eval("ItemMinorUnitID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col5 r">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSecondryQty" Text='<%#Eval("ItemMinorUnitQty") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col6 r">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOpRate" Text='<%#Eval("ItemOpeningRate") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col7 r">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOpDate" Text='<%#Eval("ItemOpeningDate") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="Item_table1_col8">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Del" CssClass="btn btn-sxs btn-danger" Style="width: 100%" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="pull-right">
                                            <div class="error_div ac_hidden">
                                                <div class="alert alert-danger error_msg"></div>
                                            </div>
                                            <asp:Label ID="lblmsg" runat="server" CssClass=" text-danger "></asp:Label>
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" Text="Save" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear" OnClick="btnClear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    
                </div>
                <div class="col-sm-12 hidden">
                   
                        <div class="panel panel-default">
                            <div class="panel-heading text-center">
                                <span class="panel-title">Available Items in Warehouse </span>
                            </div>
                            <style>
                                .table-stock-list {
                                    width: 100%;
                                }

                                    .table-stock-list tr td, .table-stock-list tr th {
                                        padding: 3px;
                                        padding-left: 6px;
                                        padding-right: 6px;
                                    }

                                /*.table-stock-list tr td.column1, .table-stock-list tr th.column1 {width:181px}
                                        .table-stock-list tr td.column2, .table-stock-list tr th.column2 {width:95px}
                                        .table-stock-list tr td.column3, .table-stock-list tr th.column3 {width:75px}
                                        .table-stock-list tr td.column4, .table-stock-list tr th.column4 {width:81px}
                                        .table-stock-list tr td.column5, .table-stock-list tr th.column5 {width:86px}*/
                            </style>
                            <%--   <div class="panel-body" style="padding: 0; max-height: 30px; overflow-y: scroll; overflow-x: hidden">
                                <table class="table-bordered table-stock-list">
                                    <tr>
                                        <th class="column1">Warehouse Name</th> 
                                        <th class="column2">Open&nbsp;Unit</th>
                                        <th class="column2">Open&nbsp;Qty</th> 
                                        <th class="column3">Minor&nbsp;Unit</th>
                                        <th class="column3">Minor&nbsp;Qty</th>
                                        <th class="column4">Rate</th>
                                        <th class="column5">Date</th>
                                    </tr>
                                </table>
                            </div>--%>
                            
                            <%--<div class="panel-table" style=" width: 100%; padding: 0; max-height: 107px; overflow-y: scroll; overflow-x: scroll">
                                <asp:GridView ID="grdOpenStockEntry" CssClass="table-bordered table-stock-list" style="font-size:12px" ShowHeader="true" AutoGenerateColumns="false" AllowPaging="false" PageSize="10" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="WareHouseName" ItemStyle-CssClass="column1">

                                            <ItemTemplate>
                                                <asp:Label ID="lblWareHouseName" Text='<%#Eval("Address") %>' runat="server" CssClass="item_overflow200" title='<%#Eval("Address") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Open Unit" ItemStyle-CssClass="column2">

                                            <ItemTemplate>
                                                <asp:Label ID="lblOpeningunit" Text='<%#Eval("UnitName") %>' runat="server" CssClass="item_overflow200" title='<%#Eval("UnitName") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Quantity" ItemStyle-CssClass="column3">

                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" Text='<%#Eval("ItemOpeningQty") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item MinorUnit" ItemStyle-CssClass="column4">

                                            <ItemTemplate>
                                                <asp:Label ID="lblOpeningunit" Text='<%#Eval("UnitName") %>' runat="server" CssClass="item_overflow200" title='<%#Eval("UnitName") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Minor Quantity" ItemStyle-CssClass="column5">

                                            <ItemTemplate>
                                                <asp:Label ID="lblMinorQuantity" Text='<%#Eval("ItemMinorUnitQty") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate" ItemStyle-CssClass="column6">

                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" Text='<%#Eval("ItemOpeningRate") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-CssClass="column7">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" Text='<%#Eval("ItemOpeningDate") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>--%>
                        </div>
                   
                </div>
            </div>
        </div>
   </asp:Content>

