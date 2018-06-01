<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxCalculator.ascx.cs" Inherits="TaxCalculator" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#clr_all').click(function () {
            ClearAll();
        })
        $('.close').click(function () {
            ClearAll();
        })
        $('#clr_btn').click(function () {
            ClearAll();
        })
    });
    function ClearAll() {
        $('#<%= txtitemamount.ClientID %>').val('');
        $('#<%= txtrate.ClientID %>').val('');
        $('#<%= txtitemamountbefore.ClientID %>').val('');
        $('#<%= txtIGSt.ClientID %>').val('');
        $('#<%= txtSGST.ClientID %>').val('');
        $('#<%= txtCGSt.ClientID %>').val('');
        $('#<%= ddltype.ClientID %>').val('0');
    }
    function ConvertInt(val) {
        try {
            return parseFloat(val);
        }
        catch (e) {
            return 0;
        }
    }
    $(document).ready(function () {
        $('#<%= ddltype.ClientID %>').change(function () {
            $('#Show').click(function () {
                return false;
            });
            var ddltype = $(this).val();
            if (ddltype == '0') {
                $('#lbl_before_tax').html(" &nbsp; ");
                ClearAll();
            }
            else if (ddltype == '1') {
                $('#Show').click(function () {
                    $('#lbl_before_tax').html("( Before Tax )");
                    if ($('#<%= ddltype.ClientID %>').val() == "0") {
                        $('#lbl_before_tax').html(" &nbsp; ");
                        $('#<%= ddltype.ClientID %>').focus();
                    }
                    else if ($('#<%= txtitemamount.ClientID %>').val() == "") {
                        $('#<%= txtitemamount.ClientID %>').focus();
                    }
                    else if ($('#<%= txtrate.ClientID %>').val() == "") {
                        $('#<%= txtrate.ClientID %>').focus();
                    }
                    else {
                        var ItemAmount = $('#<%= txtitemamount.ClientID %>').val();
                        var Rate = $('#<%= txtrate.ClientID %>').val();
                        var rate = ConvertInt(Rate) + 100;
                        var TaxbleAmount = (ItemAmount / rate) * 100;
                        var Total = ItemAmount - TaxbleAmount;
                        var SGST = Total / 2;
                        $('#<%= txtitemamountbefore.ClientID %>').val(TaxbleAmount.toFixed(2).toString());
                        $('#<%= txtIGSt.ClientID %>').val(Total.toFixed(2).toString());
                        $('#<%= txtSGST.ClientID %>').val(SGST.toFixed(2).toString());
                        $('#<%= txtCGSt.ClientID %>').val(SGST.toFixed(2).toString());
                    }
                })
    }
    else if (ddltype == '2') {
        $('#Show').click(function () {
            $('#lbl_before_tax').html("( Taxable )");
            if ($('#<%= ddltype.ClientID %>').val() == "0") {
                $('#lbl_before_tax').html(" &nbsp; ");
                $('#<%= ddltype.ClientID %>').focus();
                }
                else if ($('#<%= txtitemamount.ClientID %>').val() == "") {
                $('#<%= txtitemamount.ClientID %>').focus();
                }
                else if ($('#<%= txtrate.ClientID %>').val() == "") {
                    $('#<%= txtrate.ClientID %>').focus();
            }
            else {
                var ItemAmount = $('#<%= txtitemamount.ClientID %>').val();
                var Rate = $('#<%= txtrate.ClientID %>').val();
                var rate = ConvertInt(Rate) + 100;
                var TaxbleAmount = (ItemAmount * rate) / 100;
                var Total = TaxbleAmount - ItemAmount;
                var SGST = Total / 2;
                $('#<%= txtitemamountbefore.ClientID %>').val(TaxbleAmount.toFixed(2).toString());
                $('#<%= txtIGSt.ClientID %>').val(Total.toFixed(2).toString());
                $('#<%= txtSGST.ClientID %>').val(SGST.toFixed(2).toString());
                $('#<%= txtCGSt.ClientID %>').val(SGST.toFixed(2).toString());
            }
        })
}
else {
    ClearAll();
    return false;
}
        });
    });
</script>
<div class="modal fade" id="ModalCalc" role="dialog" data-backdrop="static">
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
        <div class="modal-content">
            <div class="modal-header" style="background: #0094ff; font-family: 'Times New Roman'; color: white;">
                <button type="button" class="close" data-dismiss="modal" style="color: white; opacity: 10;">&times;</button>
                <h4 class="modal-title">Tax Calculator</h4>
            </div>
            <div class="modal-body">
                <div id="dvTaxcal" runat="server" class="row">
                    <div class="bg_color">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group  col-sm-3 col-xs-12">
                                    <label for="email">Select Type</label>
                                    <asp:DropDownList ID="ddltype" CssClass="form-control fm-control" runat="server">
                                        <asp:ListItem Text="--Select--" Value="0" />
                                        <asp:ListItem Text="INCLUSIVE TAX" Value="1" />
                                        <asp:ListItem Text="EXCLUSIVE TAX" Value="2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="email">Taxable Amount</label>
                                    <asp:TextBox ID="txtitemamount" MaxLength="9" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="pwd">Rate:</label>
                                    <asp:TextBox ID="txtrate" MaxLength="5" class="form-control Money" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3  col-xs-12">
                                    <label for="pwd">&nbsp;</label><br />
                                    <input type="button" class="btn btn-primary tex-btn" value="Show" id="Show" />&nbsp;
                                    <input type="button" class="btn btn-danger tex-btn pull-right" value="Clear" id="clr_btn" />
                                </div>
                            </div>
                        </div>
                        <hr class="hr" />
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group col-sm-3 br-right  col-xs-12">
                                    <label for="email">
                                        Item Amount:<br />
                                        <small><span id="lbl_before_tax">&nbsp;</span></small></label>
                                    <asp:TextBox ID="txtitemamountbefore" class="form-control Money" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3 br-right  col-xs-12">
                                    <label for="email">
                                        IGST:<br />
                                        <small>Amount</small></label>
                                    <asp:TextBox ID="txtIGSt" class="form-control Money" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3 npd-top10  col-xs-12">
                                    <label for="email">
                                        SGST:<br />
                                        <small>Amount</small></label>
                                    <asp:TextBox ID="txtSGST" class="form-control Money" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-3 npd-top10  col-xs-12">
                                    <label for="email">
                                        CGST:<br />
                                        <small>Amount</small></label>
                                    <asp:TextBox ID="txtCGSt" class="form-control Money" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="clr_all" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>

    </div>
</div>