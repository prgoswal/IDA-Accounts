<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" CodeFile="frmItemMaster.aspx.cs" Inherits="AdminMasters_frmItemMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function LoadAllScript() {
            LoadBasic();
            ChoosenDDL();
        }
    </script>
    <script type="text/javascript">

        function Validation() {

            //------------------For Item Name -------------------//

            if ($('#<%=ddlMinorGroup.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Minor Group.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlMinorGroup.ClientID%>').focus();
                return false;
            }

            //------------------For Item Name -------------------//

            if ($('#<%=txtItemName.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Enter Item Name.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=txtItemName.ClientID%>').focus();
                return false;
            }

            //------------------For Item Unit -------------------//

            if ($('#<%=ddlItemUnit.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Item Unit.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlItemUnit.ClientID%>').focus();
                return false;
            }

            //------------------For Selling Rate -------------------//

            <%--if ($('#<%=txtSellingRate.ClientID%>').val() == '') {
                $('#<%=lblMsg.ClientID%>').html('Enter Selling Rate.');
                $('#<%=txtSellingRate.ClientID%>').focus();
                return false;
            }--%>

            //------------------For Item Type -------------------//

            if ($('#<%=ddlItemType.ClientID%>').val() == '0') {
                $('#<%=lblMsg.ClientID%>').html('<i class="fa fa-info-circle fa-lg"></i> Select Item Type.');
                $('#<%=lblMsg.ClientID%>').addClass('alert alert-danger');
                $('#<%=ddlItemType.ClientID%>').focus();
                return false;
            }
        }

    </script>
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

        .form-control-mini .form-control {
            padding: 2px 10px;
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upAccountHead" runat="server">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(LoadAllScript);
            </script>
            <div class="content-wrapper form-control-mini">
                <h3 class="text-center" style="padding: 5px">Item Master
                </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Minor Group<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlMinorGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlMinorGroup_SelectedIndexChanged" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Item Name<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtItemName" CssClass="form-control Alphaonly" placeholder="Item Name" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Short Name</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtShortName" CssClass="form-control Alphaonly" placeholder="Short Name" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divItemGroupDesc" visible="false" class="row" runat="server">
                                        <div class="col-sm-4" style="margin-top: -22px; margin-bottom: -17px;">
                                            <div class="form-group row">
                                                &nbsp;&nbsp;<asp:Label ID="lblItemGroup" class="col-sm-12" ForeColor="#27c24c" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Item Primary Unit<i class="text-danger">*</i></label>
                                                <div class="col-sm-12">
                                                    <asp:DropDownList ID="ddlItemUnit" CssClass="form-control" OnSelectedIndexChanged="ddlItemUnit_SelectedIndexChanged" AutoPostBack="true" placeholder="Item Unit" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <style>
                                                .label-with-checkbox-container {
                                                    margin-bottom: 4px;
                                                }

                                                    .label-with-checkbox-container label {
                                                        margin-bottom: 0px;
                                                    }


                                                .cbSell select.form-control, .cbSell input.form-control {
                                                    height: 27px;
                                                    padding: 2px 12px;
                                                }

                                                .h-22 {
                                                    height: 22px;
                                                    padding: 0px 12px;
                                                }
                                                .gpa4{
                                                    padding:2px 1px !important;
                                                }
                                            </style>
                                            <div class="row">

                                                <div id="divMinorGp" runat="server">
                                                    
                                                    <div class="col-sm-7" style="display:none;">
                                                        <div class="form-group row">
                                                            <label class="col-sm-12">Item Secondary Unit</label>
                                                            <div class="col-sm-4 cbSell" style="padding-right:0">
                                                                <asp:DropDownList ID="ddlIsUnitInd"  CssClass="form-control gpa4" AutoPostBack="true" OnSelectedIndexChanged="ddlIsUnitInd_SelectedIndexChanged" runat="server">
                                                                    <asp:ListItem Value="0" Selected="True" Text="No" />
                                                                    <asp:ListItem Value="1" Text="Yes" />
                                                                </asp:DropDownList>
                                                            </div> 
                                                            <div class="col-sm-8 cbSell">
                                                                <asp:DropDownList ID="ddlMinorUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMinorUnit_SelectedIndexChanged" CssClass="form-control col-xs-3" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="form-group row">
                                                            <label class="col-sm-12">
                                                                <asp:Label ID="lblSecFacQty" runat="server" />Calculation Factor</label>
                                                            <div class="col-sm-12 cbSell">
                                                                <asp:TextBox ID="txtMinorUnitQty" CssClass="form-control Money h-22" MaxLength="9" Text="0" placeholder="Selling Rate" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div style="margin-top: -11px;">
                                                            <label style="font-size: 11px;">Secondary Unit If Applicable</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <label class="col-sm-12">Selling Rate</label>
                                                        <div class="col-sm-12">
                                                            <asp:TextBox ID="txtSellingRate" CssClass="form-control Money" MaxLength="9" Text="0" placeholder="Selling Rate" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group row">
                                                        <label class="col-sm-12">Item Type<i class="text-danger">*</i></label>
                                                        <div class="col-sm-12">
                                                            <asp:DropDownList ID="ddlItemType" AutoPostBack="true" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="None" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Goods" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Services" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group row">
                                                <label class="col-sm-12">Item Description</label>
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtItemDescription" CssClass="form-control" MaxLength="90" placeholder="Item Description" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group">
                                                        <label class="col-sm-12">HSN/SAC Code<i class="text-danger">*</i></label>
                                                        <div class="col-sm-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtHSNSACCode" CssClass="form-control numberonly EvenLength" MaxLength="8" placeholder="HSN / SAC Code" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <asp:LinkButton ID="lnkHSNSACCodeSearch" OnClick="lnkHSNSACCodeSearch_Click" CssClass="btn btn-primary" runat="server" Style="padding: 2px 5px;"><i class="fa fa-search"></i></asp:LinkButton>
                                                                </span>
                                                            </div>
                                                            <div class="pull-left">
                                                            <asp:Label ID="lblHSNSACErrorMSG" ForeColor="Red" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="pull-right">
                                                            <asp:HyperLink ID="hlnkSearchCode" Text="Search Code" runat="server"></asp:HyperLink>
                                                        </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>

                                                <div class="col-sm-6">
                                                    <div class="row">
                                                        <label class="col-sm-12">&nbsp;</label>
                                                        <asp:TextBox ID="txtHSNSACDesc" TextMode="MultiLine" CssClass="form-control" runat="server" Style="height: 48px; margin-left: 3px;" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="row">
                                                        <label class="col-sm-12">Tax Rate</label>
                                                        <div class="col-sm-12">
                                                                <asp:DropDownList ID="ddlTaxrate" runat="server" CssClass="form-control">  </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                <div class="col-sm-12">


                                    <table class="Item_table1 table-bordered">
                                        <thead class="hd">
                                            <tr>
                                                <th colspan="8" class="text-center"><strong>Item Opening Stock Details</strong></th>
                                            </tr>
                                            <tr>
                                                <th class="Item_table1_col1" rowspan="2"><strong class="r">WareHouse</strong></th>
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
                                            <td class="Item_table1_col1 r">
                                                <asp:DropDownList ID="ddlWarehouse" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" runat="server" /></td>
                                            <td class="Item_table1_col2 r">
                                                <asp:DropDownList ID="ddlPriUnit" runat="server" Enabled="false"></asp:DropDownList></td>
                                            <td class="Item_table1_col3 r">
                                                <asp:TextBox ID="txtOpeningQty" MaxLength="8" CssClass="Decimal4" placeholder="Opening Qty" runat="server" /></td>
                                            <td class="Item_table1_col4 r">
                                                <asp:DropDownList ID="ddlSecUnit" runat="server" Enabled="false"></asp:DropDownList></td>
                                            <td class="Item_table1_col5 r">
                                                <asp:TextBox ID="txtSecQty" MaxLength="8" CssClass="Decimal4" placeholder="Opening Minor Qty" runat="server"></asp:TextBox></td>
                                            <td class="Item_table1_col6 r">
                                                <asp:TextBox ID="txtOpeningRate" MaxLength="9" CssClass="Money" placeholder="Opening Rate" runat="server"></asp:TextBox></td>
                                            <td class="Item_table1_col7 r">
                                                <asp:TextBox ID="txtOpeningDate" CssClass="datepicker" MaxLength="10" placeholder="Opening Date" runat="server"></asp:TextBox></td>
                                            <td class="Item_table1_col6 c">
                                                <asp:Button ID="btnAddItemOSE" CssClass="btn btn-xs btn-primary add_btn" Text="Add" OnClick="btnAddItemOSE_Click" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                    <%--<table class="table table-voucher gstr_sales_invoice table-bordered hidden">
                                        <thead>
                                            <tr>
                                                <th colspan="12" class="inf_head">Item Opening Stock Details</th>
                                            </tr>
                                            <tr>
                                                <th colspan="8" class="ti_col1 inf_head">Warehouse</th>
                                                <th colspan="1" class="ti_col2 inf_head">Opening Qty</th>
                                                <th colspan="1" class="ti_col3 inf_head">Opening Rate</th>
                                                <th colspan="1" class="ti_col2 inf_head">Minor Qty</th>
                                                <th colspan="1" class="ti_col3 inf_head">Minor Rate</th>
                                                <th colspan="1" class="ti_col4 inf_head">Opening Date</th>
                                                <th colspan="1" class="ti_col5 inf_head"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="8" class="ti_col1"><asp:DropDownList ID="ddlWarehouse" runat="server"></asp:DropDownList></td>
                                                <td colspan="1" class="ti_col2"><asp:TextBox ID="txtOpeningQty" MaxLength="8" CssClass="Decimal4" placeholder="Opening Qty" runat="server" /></td>
                                                <td colspan="1" class="ti_col3"><asp:TextBox ID="txtOpeningRate" MaxLength="9" CssClass="Decimal4" placeholder="Opening Rate" runat="server"></asp:TextBox></td>

                                                <td colspan="1" class="ti_col4"><asp:TextBox ID="txtMinorQty" MaxLength="8"  CssClass="Decimal4" placeholder="Opening Minor Qty" runat="server"></asp:TextBox></td>
                                                <td colspan="1" class="ti_col3"><asp:TextBox ID="txtMinorRate" MaxLength="9" CssClass="Decimal4" placeholder="Opening Minor Rate" runat="server"></asp:TextBox></td>

                                                <td colspan="1" class="ti_col4"><asp:TextBox ID="txtOpeningDate" CssClass="datepicker" MaxLength="10" placeholder="Opening Date" runat="server"></asp:TextBox></td>
                                                <td colspan="1" class="ti_col5">
                                                    <asp:Button ID="btnAddItemOSE" CssClass="btn btn-xs btn-primary add_btn" Text="Add" OnClick="btnAddItemOSE_Click" runat="server" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>--%>
                                    <br />
                                    <asp:GridView ID="grdItemMaster" AutoGenerateColumns="false" ShowHeader="false" OnRowCommand="grdItemMaster_RowCommand" OnRowDataBound="grdItemMaster_RowDataBound" CssClass="table table-voucher gstr_sales_invoice table-bordered" runat="server">
                                        <Columns>
                                            <%--<asp:BoundField DataField="OrgId" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="WarehouseId" Visible="false" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="WareHouseName" HeaderText="Warehouse" ItemStyle-CssClass="Item_table1_col1" />
                                            <asp:BoundField DataField="OpeningUnit" HeaderText="Warehouse"   ItemStyle-CssClass="Item_table1_col2 r" />
                                            <asp:BoundField DataField="OpeningQty" HeaderText="Opening Qty"  ItemStyle-CssClass="Item_table1_col3 r" />
                                            <asp:BoundField DataField="OpeningMinorUnit"  HeaderText="Opening Qty" ItemStyle-CssClass="Item_table1_col4 r" />
                                            <asp:BoundField DataField="OpeningMinorQty"  HeaderText="Opening Qty"  ItemStyle-CssClass="Item_table1_col5 r" />
                                            <asp:BoundField DataField="OpRate" HeaderText="Opening Rate" ItemStyle-CssClass="Item_table1_col6 r" />
                                            <asp:BoundField DataField="OpDate" HeaderText="Opening Date" ItemStyle-CssClass="Item_table1_col7 " />--%>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OrgId") %>' runat="server" />
                                                    <asp:Label Text='<%#Eval("WarehouseId") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col1">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("WareHouseName") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col2 r">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OpeningUnit") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col3 r">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OpeningQty") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col4 r">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OpeningMinorUnit") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col5 r">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OpeningMinorQty") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col6 r">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OpRate") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col7">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Eval("OpDate") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="Item_table1_col6 c">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" Text="Del" CommandName="RemoveRow" CommandArgument='<%#Container.DataItemIndex %>' CssClass="btn btn-xs btn-danger add_btn" runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                                        
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="pull-right">
                                            <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" OnClientClick="return Validation()" Text="Save" CssClass="btn btn-primary btn-space-right" runat="server"  />
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-danger btn-space-right" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:HiddenField ID="hfMainGrCode" runat="server" />
                                <asp:HiddenField ID="hfSubGrCode" runat="server" />
                                <asp:HiddenField ID="hfItemGroupID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
