<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmGSTR3B.aspx.cs" Inherits="GSTReturns_frmGSTR3B" Culture="hi-IN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .li_brck {
            display: block;
            font-size: 75%;
            margin-top: -7px;
        }

        .mrbotm40 {
            margin-bottom: 30px;
        }

        .btn_g {
            margin: 0 auto;
            display: block;
            min-width: 35px;
        }

        .font12 {
            font-size: 12px;
        }

        .align_with_combobox td:not(.ajax__combobox_textboxcontainer) input, .align_with_combobox td:not(.ajax__combobox_textboxcontainer) select {
            margin-top: 0px;
            height: 20px;
            padding: 0;
        }

        .btn0 {
            margin-top: 0px;
            height: 20px;
            padding: 0;
            min-width: 35px;
        }

        .align_with_combobox td {
            vertical-align: top !important;
        }

        .btn-sxs1 {
            padding: 2px 10px;
        }

        .w601 {
            width: 601px;
        }

        .w115 {
            width: 115px;
            text-align: right;
        }

        .wt115 {
            width: 115px;
        }

        .w220 {
            width: 220px;
            text-align: right;
        }

        .wt220 {
            width: 220px;
        }
        /*.w212{
            width:206px;
        }
        .w166{
            width:162px;
        }
        .w194{
            width:189px;
        }*/
        .w360 {
            width: 360px;
        }
        /*.w300{
            width:300px;
        }
         .w217{
             width:217px;
         }*/
        .w477 {
            width: 477px;
        }

        .w460 {
            width: 460px;
        }

        td.r, th.r {
            text-align: right;
        }

        td.c, th.c {
            text-align: center;
        }

        .Landscape {
            width: 100%;
            height: 100%;
            margin: 0% 0% 0% 0%;
            filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=1);
        }



        .gridtooltip {
            position: relative;
            display: inline-block;
            color: #337ab7;
        }

            .gridtooltip:hover {
                color: #23527c;
                cursor: pointer;
                text-decoration: underline;
            }

            .gridtooltip .gridtooltiptext {
                visibility: hidden;
                width: 77px;
                background-color: black;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 1px 0;
                position: absolute;
                z-index: 1;
                top: 2px;
                left: 100.1%;
                font-size: 12px;
            }

            .gridtooltip:hover .gridtooltiptext {
                visibility: visible;
            }
    </style>
    <script>
        function ShowDetailGV() {
            debugger
            $('#detailsModal').modal();
        }
    </script>
    <script>
        function LoadAllScript() {
            
            $('#sum').hide();
            $('#sum_open').click(function () {
                $('#sum').toggle();
            });            
        }
    </script>
    <script type="text/javascript">

        function PrintPanel() {
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=Chakaria.ClientID %>");
            var printWindow = window.open('', '', 'height=220,width=1200');
            printWindow.document.write('<html><head><title>GSTR 3B Summary</title>');
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
                <h3 class="text-center p5" style="padding: 5px; margin-bottom: 5px;">GSTR - 3B </h3>
                <div class="container_fluid">
                    <div class="row">
                        <div class="panel panel-default">
                            <div class="panel-body Landscape" id="pnl_Priview">

                                <asp:UpdatePanel runat="server" ID="deepak">
                                    <ContentTemplate>
                                        <asp:Panel ID="Chakaria" runat="server">


                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <table class="pdf_show mrbotm40 font12" style="width: 100%; margin-bottom: 10px">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="100%" style="text-align: right">
                                                                    <b>
                                                                        <asp:Label ID="lblDate" runat="server"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 5%; text-align: right; padding-right: 3px"><b>Month</b></td>
                                                                <td style="width: 10%">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server">
                                                                        <asp:ListItem Value="0" Text="--Select--" />
                                                                        <%-- <asp:ListItem Value="1" Text="January" />
                                                                        <asp:ListItem Value="2" Text="February" />
                                                                        <asp:ListItem Value="3" Text="March" />
                                                                        <asp:ListItem Value="4" Text="April" />
                                                                        <asp:ListItem Value="5" Text="May" />
                                                                        <asp:ListItem Value="6" Text="June" />--%>
                                                                        <asp:ListItem Value="7" Text="July" />
                                                                        <asp:ListItem Value="8" Text="August" />
                                                                        <asp:ListItem Value="9" Text="September" />
                                                                      <%-- <asp:ListItem Value="10" Text="October" />
                                                                        <asp:ListItem Value="11" Text="November" />
                                                                        <asp:ListItem Value="12" Text="December" />--%>
                                                                    </asp:DropDownList></td>
                                                                <td style="width: 5%; text-align: right; padding-right: 3px"><b>Year</b></td>
                                                                <td style="width: 10%">
                                                                    <asp:DropDownList ID="ddlYear" runat="server">
                                                                        <asp:ListItem Value="17" Text="2017" />
                                                                        <%-- <asp:ListItem Value="0" Text="--Select--" />
                                                                        <asp:ListItem Value="18" Text="2018" />
                                                                        <asp:ListItem Value="19" Text="2019" />
                                                                        <asp:ListItem Value="20" Text="2020" />--%>
                                                                    </asp:DropDownList></td>

                                                                <td style="width: 7%; text-align: right; padding-right: 3px"><b>GSTIN</b></td>
                                                                <td style="width: 11%;"><b>
                                                                    <asp:DropDownList runat="server" ID="ddlGstin">
                                                                    </asp:DropDownList></b></td>

                                                                <td style="width: 20%; padding-left: 5px">
                                                                    <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sxs1 hidden-print" Text="Go" runat="server" OnClick="btnGo_Click" />
                                                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info btn-sxs1 hidden-print" Text="Print" OnClientClick="return PrintPanel();" />
                                                                    <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-danger  btn-sxs1 hidden-print" OnClick="btnClear_Click" runat="server" />
                                                                </td>
                                                                <td style="width: 50%; text-align: right; padding-right: 3px"><b>
                                                                    <asp:Label ID="lblCompanyName" runat="server"></asp:Label></b> </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="100%" style="text-align: right; position: relative">
                                                                    <small style="position: absolute; margin-top: -7px; right: 0">Amount in Rs.</small>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </div>

                                            <div id="pnlGrids" runat="server" visible="false">
                                                <div class="row">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 1px solid #eee; margin-bottom: 10px; margin-top: 10px; float: left;"></div>
                                                </div>
                                                <div class="col-xs-12 inf_head"><span style="font-weight: bold; cursor: pointer; user-select: none" id="sum_open"><i class="fa r fa-plus"></i>GSTR 3B Summary</span></div>
                                                <div class="col-xs-12" id="sum">
                                                    <div class="row">

                                                        <asp:GridView ID="GrdSummary" runat="server" AutoGenerateColumns="false" CssClass="table-bordered" ShowHeader="true" Width="100%">


                                                            <Columns>

                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Description" DataField="RecDesc" ItemStyle-CssClass="w40 " />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Total&nbsp;Taxable&nbsp;Value" HtmlEncode="false" DataFormatString="{0:C}" DataField="TotalTaxValue" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Integrated Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="IGSTAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Central Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="CGSTAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="State/UT Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="SGSTAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Cess" HtmlEncode="false" DataFormatString="{0:C}" DataField="CessAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Inter-State Supplies" HtmlEncode="false" DataFormatString="{0:C}" DataField="InterStateAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Intra-State Supplies" HtmlEncode="false" DataFormatString="{0:C}" DataField="IntraStateAmt" ItemStyle-CssClass="w115 r" />


                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 3px solid #989898; margin-bottom: 10px; margin-top: 15px; float: left;"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <label><small>3.1  Details of Outward Supplies and inward supplies liable to reverse charge</small></label>

                                                        <asp:GridView ID="Grd31" DataKeyNames="TableHeadDescCD,TableHeadDesc" runat="server" AutoGenerateColumns="false" CssClass="table-bordered" ShowHeader="true" Width="100%" OnRowCommand="SupplieDetail_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="inf_head" HeaderText="Nature of Supplies" ItemStyle-CssClass="w601">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnViewDetail" runat="server"  CommandName="RowClickGrd31" CommandArgument='<%#Container.DataItemIndex%>' CssClass="gridtooltip">
                                                                            <asp:Label ID="lblHeadDesc" Text='<%# Eval("TableHeadDesc")%>' runat="server" />
                                                                            <span class="gridtooltiptext">View Detail</span>
                                                                        </asp:LinkButton>
                                                                        <asp:Label Visible="false" ID="lblHeadDescCD" Text='<%# Eval("TableHeadDescCD")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Total&nbsp;Taxable&nbsp;Value" HtmlEncode="false" DataFormatString="{0:C}" DataField="TotalTaxValue" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Integrated Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="IGSTAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Central Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="CGSTAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="State/UT Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="SGSTAmt" ItemStyle-CssClass="w115 r" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Cess" HtmlEncode="false" DataFormatString="{0:C}" DataField="Cesstax" ItemStyle-CssClass="w115 r" />
                                                                
                                                            </Columns>
                                                        </asp:GridView>                                                        

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 1px solid #eee; margin-bottom: 10px; margin-top: 15px; float: left;"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <label><small>3.2 Of the supplies shown in 3.1 (a) above, details of inter-State supplies made to unregistered persons, composition taxable persons and UIN holders</small></label>

                                                        <asp:GridView ID="Grd32" DataKeyNames="TableHeadDescCD,TableHeadDesc"  runat="server" AutoGenerateColumns="false" CssClass="table-bordered" ShowHeader="true" OnRowCommand="SupplieDetail_RowCommand" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="w360" HeaderText="" HeaderStyle-CssClass="inf_head">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton  CssClass="gridtooltip" CommandName="RowClickGrd32"  CommandArgument='<%#Container.DataItemIndex%>' runat="server" > 
                                                                            <%#Eval("TableHeadDesc") %>
                                                                            <span class="gridtooltiptext">View Detail</span>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField HtmlEncode="false" DataFormatString="{0:C}" DataField="TableHeadDesc" ItemStyle-CssClass="w360" HeaderText="" HeaderStyle-CssClass="inf_head" />--%>
                                                                <asp:BoundField HtmlEncode="false" DataFormatString="{0:C}" DataField="PosStateDesc" ItemStyle-CssClass="wt115" HeaderText="Place of Supply (State/UT)" HeaderStyle-CssClass="inf_head" />
                                                                <asp:BoundField HtmlEncode="false" DataFormatString="{0:C}" DataField="TotalTaxValue" ItemStyle-CssClass="w115" HeaderText="Total Taxable value" HeaderStyle-CssClass="inf_head" />
                                                                <asp:BoundField HtmlEncode="false" DataFormatString="{0:C}" DataField="IGSTAmt" ItemStyle-CssClass="w115" HeaderText="Amount of Integrated Tax" HeaderStyle-CssClass="inf_head" />
                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 1px solid #eee; margin-bottom: 10px; margin-top: 15px; float: left;"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <label><small>4. Eligible ITC</small></label>

                                                        <asp:GridView ID="Grd4" runat="server" AutoGenerateColumns="false" CssClass="table-bordered w100" ShowHeader="true" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Details" HtmlEncode="false" DataFormatString="{0:C}" DataField="TableHeadDesc" ItemStyle-CssClass="w477" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Integrated Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="IGSTAmt" ItemStyle-CssClass="w115" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Central Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="CGSTAmt" ItemStyle-CssClass="w115" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="State/UT Tax" HtmlEncode="false" DataFormatString="{0:C}" DataField="SGSTAmt" ItemStyle-CssClass="w115" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Cess" HtmlEncode="false" DataFormatString="{0:C}" DataField="Cesstax" ItemStyle-CssClass="w115" />

                                                            </Columns>

                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 1px solid #eee; margin-bottom: 10px; margin-top: 15px; float: left;"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <label><small>5. Values of exempt, nil-rated non-GST inward supplies</small></label>

                                                        <asp:GridView ID="Grd5" runat="server" AutoGenerateColumns="false" CssClass="table-bordered w100" ShowHeader="true" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Nature of Supplies" DataField="TableHeadDesc" ItemStyle-CssClass="w460" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Inter-State supplies" HtmlEncode="false" DataFormatString="{0:C}" DataField="InterStateAmt" ItemStyle-CssClass="w220" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Intra-State Supplies" HtmlEncode="false" DataFormatString="{0:C}" DataField="IntraStateAmt" ItemStyle-CssClass="w220" />
                                                            </Columns>

                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                                <!-- 6.1 hidden for payment tax -->
                                                <div class="row hidden">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 1px solid #eee; margin-bottom: 10px; margin-top: 15px; float: left;"></div>
                                                </div>
                                                <div class="row hidden">
                                                    <div class="col-xs-12">
                                                        <label><small>6.1 Payment of tax</small></label>

                                                        <asp:GridView ID="Grd6" runat="server" CssClass="table-bordered" AutoGenerateColumns="false" Width="100%">
                                                            <Columns>



                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Description" DataField="Description" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Total Tax" DataField="taxPayble" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="IGST Amt" DataField="IGST" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="CGST Amt" DataField="CGST" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="SGST Amt" DataField="SGST" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="CESS" DataField="Cess" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Tax paid TDS/TCS" DataField="TDSTCSInd" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Tax/cess paid in cash" DataField="TaxCessPaidInCash" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="Interest" DataField="Interest" />
                                                                <asp:BoundField HeaderStyle-CssClass="inf_head" HeaderText="LateFees" DataField="LateFee" />


                                                            </Columns>

                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                                <div class="row hidden">
                                                    <div style="clear: both; height: 1px; width: 100%; border-top: 1px solid #eee; margin-bottom: 10px; margin-top: 15px; float: left;"></div>
                                                </div>
                                                <!-- 6.1 hidden for payment tax -->
                                            </div>



                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>


                            <div class="panel-footer text-right">
                                <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
                                <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary hidden" OnClientClick="return Validation()" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <style>
                .bodyPop {
                    position:fixed;
                    display: grid;
                    background: white;
                    width: 84%;
                    margin: auto;
                    border: 1px solid grey;
                    border-radius: 6px;
                    box-shadow: 3px 4px 13px rgba(0, 0, 0, 0.4);
                    padding: 15px;
                    z-index: 1040;
                    left: 10%;
                }
                .reportPopUp {
                    background-color: #000;
                    left: 0;
                    right: 0;
                    top: 60px;
                    bottom: 0;
                    padding-top: 20px;
                    background-color: rgba(0, 0, 0, 0.63);
                   position:absolute;
                    z-index: 1940;
                }

                
                
                .bodyPop-Content{
                    overflow:auto;
                    max-height:310px;                   
                }
                .tblfbg tr td{
                        background: #eee;
                        border-right: 1px solid #ccc7c7 !important;                       
                        padding:4px;
                        
                }
                .bodyPop-Content .table tr td,.tblfbg.table tr td{
                    padding:4px;
                }
                .fontSupplie{
                    font-size:11px;
                }
                
            </style>
            
            <script>
               
                function AddTHEAD() {debugger
                    table = document.getElementById("<%=Grd31OnPopup.ClientID%>");
                    /// For Header Report Name tr2
                    var head = document.createElement("THEAD");
                    var tr2 = document.createElement("tr");
                    //var th = document.createElement("th");
                    var span = document.createElement("span");
                    //th.colSpan = "100%";
                    //th.style = "center";
                    //th.style.border = "none";
                    //span.innerText = "Heading";
                    //span.innerText += 
                    //th.appendChild(span);
                    //tr2.appendChild(th);
                    head.appendChild(tr2);
                    //////////////////////////

                    head.appendChild(table.rows[0]);
                    table.insertBefore(head, table.childNodes[0]);
                }
                function PrintDiv() {
                    AddTHEAD();
                    var divContents = document.getElementById('<%=PnlViesDetail.ClientID%>').innerHTML;
                        var printWindow = window.open('', '', 'height=400,width=800');
                        printWindow.document.write('<html><head><title></title>');
                        printWindow.document.write('</head><body>');
                        printWindow.document.write('<style>table {font-size:10px;} .PrintHide,a,.hideOnPrint{display:none;} .text-center{text-align: center;} .noExl{display:none;} table{width:100%;border: none;} table tr td{border:1px solid black;} table tr th{border:1px solid black;} </style>')
                        printWindow.document.write(divContents);
                        printWindow.document.write('</body></html>');
                        printWindow.document.close();
                        printWindow.print();
                    }
            </script>
            <asp:Panel runat="server" ID="PnlViesDetail" CssClass="reportPopUp" Visible="false" TabIndex="-1">
                <style>
                    @media print
                    {
                        th
                        {
                            color: black;
                            background-color: white;
                        }
                        THEAD
                        {
                            display: table-header-group;
                            border: solid 1px black;
                        }
                        .w100{
                            display:none;
                        }
                        .hideOnPrint{
                            display:none;
                        }
                        
                    }
                </style>
                <div class="bodyPop" style="">
                    <div class="col-md-12">
                        <div class="pull-right hideOnPrint">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="PrintDiv();" CssClass="font-awesome-font btn btn-info"><i class="fa r fa-print"></i> Print</asp:LinkButton>
                            <asp:Button CssClass="btn btn-danger" ID="btnCloseViesDetail" OnClick="btnCloseViesDetail_Click" Text="&#xf00d; Close" runat="server" Style="font-family: FontAwesome,'Source Sans Pro', sans-serif" />
                        </div>
                        <h4 class="modal-title">
                            <asp:Label ID="lblViewDetailHeading" runat="server" /><br />
                        </h4>
                        <div>
                            Month :<asp:Label ID="lblddlMonth" runat="server" />
                            Year :<asp:Label ID="lblddlYear" runat="server" /><br />
                            GSTIN :<asp:Label ID="lblddlGstin" runat="server" /><br />
                            Company Name :<asp:Label ID="lbllblCompanyName" runat="server" /><br />
                            Date :<asp:Label ID="lbllblDate" runat="server" />
                        </div>
                        <asp:Panel ID="pnlSuplliesRecord" runat="server">

                            <table class="inf_head table-bordered hideOnPrint fontSupplie">
                                <tr>
                                    <th style="width:7%" >Invoice No</th>
                                    <th style="width:9%" >Invoice Date</th>
                                    <th style="width:15%" >Party GSTIN</th>
                                    <th style="width:8%" >Taxable</th>
                                    <th style="width:8%" >IGST</th>
                                    <th style="width:8%" >CGST</th>
                                    <th style="width:8%" >SGST</th>
                                    <th style="width:8%" >Cess</th>
                                    <th style="width:12%" >Place Of Supply</th>
                                    <th style="width:11%" >Inter/Intra <br /> State</th>
                                    <th style="width:6%" >Doc Type</th>
                                </tr>                     
                            </table>

                            <div>
                             <asp:Panel CssClass="bodyPop-Content scroll_gst" runat="server">
                                <asp:GridView ID="Grd31OnPopup" CssClass="table table-bordered table-hover mb0 fontSupplie" ShowHeader="true" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="InvoiceNo"         ItemStyle-Width="7%" HeaderText="Invoice No"           HeaderStyle-CssClass="inf_head hidden" />
                                    <asp:BoundField DataField="InvoiceDate"       ItemStyle-Width="9%" HeaderText="Invoice Date"         HeaderStyle-CssClass="inf_head hidden" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="PartyGSTIN"        ItemStyle-Width="15%" HeaderText="Party GSTIN"         HeaderStyle-CssClass="inf_head hidden" />
                                    <asp:BoundField DataField="TaxableAmount"     ItemStyle-Width="8%" HeaderText="Taxable"              HeaderStyle-CssClass="inf_head hidden" HtmlEncode="false" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="IGSTAmount"        ItemStyle-Width="8%" HeaderText="IGST"                 HeaderStyle-CssClass="inf_head hidden" HtmlEncode="false" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="CGSTAmount"        ItemStyle-Width="8%" HeaderText="CGST"                 HeaderStyle-CssClass="inf_head hidden" HtmlEncode="false" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="SGSTAmount"        ItemStyle-Width="8%" HeaderText="SGST"                 HeaderStyle-CssClass="inf_head hidden" HtmlEncode="false" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="CessAmount"        ItemStyle-Width="8%" HeaderText="Cess"                 HeaderStyle-CssClass="inf_head hidden" HtmlEncode="false" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="PlaceOfSupply"     ItemStyle-Width="12%" HeaderText="Place Of Supply"     HeaderStyle-CssClass="inf_head hidden" />
                                    <asp:BoundField DataField="Inter/Intra State" ItemStyle-Width="11%" HeaderText="Inter/Intra State"   HeaderStyle-CssClass="inf_head hidden" />
                                    <asp:BoundField DataField="DocType"           ItemStyle-Width="6%" HeaderText="Doc Type"             HeaderStyle-CssClass="inf_head hidden" />                                
                                </Columns>
                            </asp:GridView>
                            </asp:Panel>
                            </div>
                           
                            <table class="table table-bordered tblfbg fontSupplie">
                                <tr>
                                    <td colspan="3" style="width:31%; text-align:center; font-weight:bold;">Total</td>                                    
                                    <td style="width:8%"><asp:Label  ID="lblTaxable" runat="server" /></td>
                                    <td style="width:8%"><asp:Label  ID="lblIGST" runat="server" /></td>
                                    <td style="width:8%"><asp:Label  ID="lblCGST" runat="server" /></td>
                                    <td style="width:8%"><asp:Label  ID="lblSGST" runat="server" /></td>
                                    <td style="width:8%"><asp:Label  ID="lblCess" runat="server" /></td>
                                    <td colspan="3" style="width:29%">&nbsp;</td>                                    
                                </tr>                     
                            </table>
                         </asp:Panel>
                        <asp:Panel CssClass="text-center fa-2x text-danger" ID="PnlSuplliesNoRecord" runat="server"><span class="fa fa-info-circle"></span> No Record Found.</asp:Panel>
                     </div>        
                </div>    
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


