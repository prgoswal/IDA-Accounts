function startTime() {
    var today = new Date();
    var acDate = today.getDate();
    var acMonth = today.getMonth() + 1;
    var acYear = today.getFullYear();
    var acHours = today.getHours();
    var acMinutes = today.getMinutes();
    var acSeconds = today.getSeconds();
    acMinutes = checkTime(acMinutes);
    acSeconds = checkTime(acSeconds);
    acDate = checkTime(acDate);
    acMonth = checkTime(acMonth);
    document.getElementById('logintime').innerHTML =
        acDate + "/" + acMonth + "/" + acYear + "&nbsp; " + acHours + ":" + acMinutes + ":" + acSeconds;
    var t = setTimeout(startTime, 500);
}
function checkTime(i) {
    if (i < 10) {
        i = "0" + i
    }; // add zero in front of numbers < 10
    return i;
}
function mode() {
    var x = document.getElementsByClassName("pay_mode").value;
    document.getElementById("rtgs_n").innerHTML = x + " No.";
    document.getElementById("rtgs_d").innerHTML = x + " Date";
}
function nextclick(userid, year) {

    $('.password_parent').removeClass('ac_hidden');
    $('.btn_login_parent').removeClass('ac_hidden');
    $('.input_pass').focus();
    $('.username').html(userid);
    $('.fin_year').html(year);
    $('.error_div').addClass('ac_hidden');
    $('.div_details').removeClass('ac_hidden');
    $('.select_parent').addClass('ac_hidden');
    $('.userid_parent').addClass('ac_hidden');
    $('.btn_next_parent').addClass('ac_hidden');
    ShowLoginError("User Already Logged-In. Please Logout First!");
}
function ShowLoginError(msg) {
    $('.error_div').removeClass('ac_hidden');
    $('.error_msg').html(msg);
}
function LoginRequestDemo(msg, link) {
    $('.error_div').removeClass('ac_hidden');

}



function ChooseYear() {

    $('.Year_parent').removeClass('ac_hidden');
    $('.btn_final_login_parent_Year').removeClass('ac_hidden');
    var year = $('.select-year option:selected').html();
    var userid = $('.user-id').val();
    $('.password_parent').addClass('ac_hidden');
    $('.btn_login_parent').addClass('ac_hidden');
    $('.username').html(userid);
    $('.fin_year').html(year);
    $('.error_div').addClass('ac_hidden');
    $('.div_details').removeClass('ac_hidden');
    $('.select_parent').addClass('ac_hidden');
    $('.userid_parent').addClass('ac_hidden');
    $('.btn_next_parent').addClass('ac_hidden');
    $('.select-year').focus();
}

function ContinueClickForYear() {
    debugger;
    var YearValue = $('.select-year').val();
    if (YearValue == '0') {
        ChooseYear();
        $('.error_div').removeClass('ac_hidden');
        $('.error_msg').html('Please Select Financial Year');
        return false;
    } else {
        ChooseYear();
        $('.error_div').addClass('ac_hidden');
        $('.error_msg').html('');
    }
}
function nextclickSuccess(userid, year) {

    $('.password_parent').removeClass('ac_hidden');
    $('.btn_login_parent').removeClass('ac_hidden');
    $('.input_pass').focus();
    $('.username').html(userid);
    $('.fin_year').html(year);
    $('.error_div').addClass('ac_hidden');
    $('.div_details').removeClass('ac_hidden');
    $('.select_parent').addClass('ac_hidden');
    $('.userid_parent').addClass('ac_hidden');
    $('.btn_next_parent').addClass('ac_hidden');
}
function ChooseBranch() {

    $('.branch_parent').removeClass('ac_hidden');
    $('.btn_final_login_parent').removeClass('ac_hidden');
    var year = $('.select-year option:selected').html();
    var userid = $('.user-id').val();
    $('.password_parent').addClass('ac_hidden');
    $('.btn_login_parent').addClass('ac_hidden');
    $('.username').html(userid);
    $('.fin_year').html(year);
    $('.error_div').addClass('ac_hidden');
    $('.div_details').removeClass('ac_hidden');
    $('.select_parent').addClass('ac_hidden');
    $('.userid_parent').addClass('ac_hidden');
    $('.btn_next_parent').addClass('ac_hidden');
    $('.select-branch').focus();
}
function ContinueClick() {

    var branchValue = $('.select-branch').val();
    if (branchValue == '0') {
        ChooseBranch();
        $('.error_div').removeClass('ac_hidden');
        $('.error_msg').html('Please Select Branch');
        return false;
    } else {
        ChooseBranch();
        $('.error_div').addClass('ac_hidden');
        $('.error_msg').html('');
    }
}

$(document).ready(function () {

    $('.btn_back').click(function () {
        $('.error_div').addClass('ac_hidden');
        $('.password_parent').addClass('ac_hidden');
        $('.btn_login_parent').addClass('ac_hidden');
        $('.username').html('');
        $('.fin_year').html('');
        $('.div_details').addClass('ac_hidden');
        $('.error_div').addClass('ac_hidden');
        $('.select_parent').removeClass('ac_hidden');
        $('.userid_parent').removeClass('ac_hidden');
        $('.btn_next_parent').removeClass('ac_hidden');
    });

    $('.btn_back_login').click(function () {
        $('.branch_parent').addClass('ac_hidden');
        $('.btn_final_login_parent').addClass('ac_hidden');
        $('.error_div').addClass('ac_hidden');
        $('.password_parent').addClass('ac_hidden');
        $('.btn_login_parent').addClass('ac_hidden');
        $('.username').html('');
        $('.fin_year').html('');
        $('.div_details').addClass('ac_hidden');
        $('.error_div').addClass('ac_hidden');
        $('.select_parent').removeClass('ac_hidden');
        $('.userid_parent').removeClass('ac_hidden');
        $('.btn_next_parent').removeClass('ac_hidden');
    });

    $('.btn_login').click(function () {
        var pass = $('.input_pass').val();
        if (pass == '') {
            $('.error_div').removeClass('ac_hidden');
            $('.error_msg').html('Please Enter Password');
            return false;
        } else {
            return;
        }
    })

});
$(document).ready(function () {
    $('#addintable_deep').click(function () {

        var achd = $('#ac_head').val();
        var inno = $('#inv_no').val();
        var indate = $('#inv_d').val();
        var amnt = $('#amt').val();
        var actn = $('#act').val();

        if ((achd == '') || (inno == '') || (indate == '') || (amnt == '') || (actn == '')) {
            alert('Error');
            return false;
        } else {
            $("#tbl_id tbody tr:first").after('<tr><td class="col1">' + achd + '</td><td class="col2">' + inno + '</td><td class="col3">' + indate + '</td><td class="col4">' + amnt + '</td><td class="col5">' + actn + '</td><td class="col6"><a href="#!" class="btn btn-xs btn-danger add_btn"><i class=" add_btn fa fa-trash"></i></a></td></tr>');
            return;
        }
    })
});
$(document).ready(function () {

    $('#addintable').click(function () {
        var name = $('#itm_name').val();
        var qty = $('#itm_qty').val();
        var free = $('#itm_free').val();
        var unit = $('#itm_unit').val();
        var rate = $('#itm_rate').val();
        var itmAmt = $('#itm_itemAmt').val();
        var disAmt = $('#itm_discountAmt').val();
        var taxAmt = $('#itm_taxAmt').val();
        var pa = $('#itm_pa').val();
        var isd = $('#itm_isd').val();
        var rcm = $('#itm_rcm').val();
        var ctax = $('#itm_ctax').val();
        var camt = $('#itm_camt').val();
        var stax = $('#itm_stax').val();
        var samt = $('#itm_samt').val();
        var itax = $('#itm_itax').val();
        var iamt = $('#itm_iamt').val();
        if ((name == '') || (qty == '') || (free == '') || (unit == '') || (rate == '') || (itmAmt == '') || (disAmt == '') || (taxAmt == '') || (pa == '') || (isd == '') || (rcm == '') || (ctax == '') || (camt == '') || (stax == '') || (samt == '') || (itax == '') || (iamt == '')) {
            alert('Error');
            return false;
        } else {
            $("#addtothis tbody tr:first").after('<tr><td class="gstr_col1">' + name + '</td><td class="gstr_col2">' + qty + '</td><td class="gstr_col3">' + free + '</td><td class="gstr_col4">' + unit + '</td><td class="gstr_col5">' + rate + '</td><td class="gstr_col6">' + itmAmt + '</td><td class="gstr_col7">' + disAmt + '</td><td class="gstr_col8">' + taxAmt + '</td><td class="gstr_col9">' + pa + '</td><td class="gstr_col10">' + isd + '</td><td class="gstr_col11">' + rcm + '</td><td class="gstr_col12">' + ctax + '</td><td class="gstr_col13">' + camt + '</td><td class="gstr_col14">' + stax + '</td><td class="gstr_col15">' + samt + '</td><td class="gstr_col16">' + itax + '</td><td class="gstr_col17">' + iamt + '</td><td class="gstr_col18"><a href="#!" class="btn btn-xs btn-danger"><i class="fa fa-trash"></i></a></td></tr>');
            return;
        }
    })
});
$(document).ready(function () {
    $('.rcm_type').change(function () {
        var x = $('.rcm_type').val();
        if (x == 'y') {
            $('.rcmamt').removeAttr('disabled')
            $('.rcmtax').removeAttr('disabled')
        } else {
            $('.rcmamt').attr('disabled', 'disabled')
            $('.rcmtax').attr('disabled', 'disabled')
        }
    })
});
$(document).ready(function () {
    $('.table-invoice-after').hide();
    $('.salestoname').change(function () {
        var p = $(this).val();
        if (p != '0') {
            $('.table-invoice-after').fadeIn(500);
        }
        else {
            $('.table-invoice-after').hide();
        }
    })
});
$(document).ready(function () {
    // $('.ifpa').hide();
    $('.gstr_sales_item').removeClass('gstr_grid_full');
    $('.gstr_sales_item').addClass('gstr_grid_half');
    $('.itm_papicker').change(function () {
        var x = $(this).val();

        if (x == 'y') {
            $('.gstr_sales_item').removeClass('gstr_grid_half');
            $('.gstr_sales_item').addClass('gstr_grid_full');
            //$('.ifpa').show();
            $('.ifpa').removeClass('hidden');
        }
        else {
            $('.gstr_sales_item').removeClass('gstr_grid_full');
            $('.gstr_sales_item').addClass('gstr_grid_half');
            //$('.ifpa').hide();
            $('.ifpa').addClass('hidden');
        }
    })
});
$(document).ready(function () {

    /// For Create Popover \\\
    function PopOverError(id, plac, msg) {
        try {
            $(id).popover({
                title: 'Error!',
                trigger: 'manual',
                placement: plac,
                content: function () {
                    var message = msg; //"Allow Numbers Only!";
                    return message;
                }
            });
            $(id).popover("show");
        } catch (e) { }
    }
    /////////////Zero Not Allow///////////////// 
    $('.NotAllowZero').keyup(function (event) {
        try {
            var currentVal = $(this).val();
            var id = ('#' + this.id);
            if (currentVal.length == 1 && (event.which == 48 || event.which == 96)) {
                currentVal = currentVal.slice(0, -1);
                PopOverError(id, 'top', 'Zero Not Allow!');
                $(this).val(currentVal);
                return false;
            }
            else {
                $(this).val(currentVal);
                $(id).popover('destroy');
                return true;
            }

        }
        catch (e) {

        }
    });
    $('.NotAllowZero').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(this).val(currentVal);
            $(id).popover('destroy');
        } catch (e) { }
    });

    //// For Numeri Value Only \\\\\\s
    $('.numberonly').keypress(function (event) {

        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Allow Numbers Only!');
                return false;
            }
            else {
                $(id).popover('destroy');
                return true;
            }
        } catch (e) {

        }
    });


    $('.numberonly').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(id).popover('destroy');
        } catch (e) { }
    });

    //// For Valid Date Allow \\\\\\
    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });

    $(".setDate.datepicker").datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true, }).datepicker("setDate", "0");

    $('.datepicker').blur(function (e) {
        try {

            var id = ('#' + this.id);
            var date = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (date.length <= 0 || date == '' || date == undefined) {
                return false;
            }
            if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');
            } else {
                PopOverError(id, 'top', 'Invalid Date.');
                $(id).focus();
            }
        } catch (e) { }
    });
    $('.datepicker').keypress(function (e) {
        try {
            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == "/") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 31) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Day!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 5) {
                        var month = $(id).val().substring(3, 6);
                        if (month < 1 || month > 12) {
                            var corr = $(id).val().replace("/" + month, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Month!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });

    $('.dateFormat').blur(function (e) {
        try {

            var id = ('#' + this.id);
            var date = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (date.length <= 0 || date == '' || date == undefined) {
                return false;
            }
            if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');
            } else {
                PopOverError(id, 'top', 'Invalid Date.');
                $(id).focus();
            }
        } catch (e) { }
    });
    $('.dateFormat').keypress(function (e) {
        try {
            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == "/") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 31) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Day!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 5) {
                        var month = $(id).val().substring(3, 6);
                        if (month < 1 || month > 12) {
                            var corr = $(id).val().replace("/" + month, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Month!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });

    //// For Amount upto 2 decimal Place \\\
    $('.Money').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(id).popover('destroy');
        } catch (e) { }
    });
    $('.Money').keypress(function (e) {
        try {
            var id = ('#' + this.id);
            var val = $(id).val();
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);


            if (chCode != 46) {
                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Enter Valid Amount');
                    return false; //Non Numeric Value Return Directly;
                }
            }

            if (val.indexOf(".") != -1 && val.indexOf(".") + 3 == val.length && event.keyCode != 8 && event.keyCode != 17 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 9) {
                PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                event.preventDefault();
                return false;
            }
            if (event.keyCode == 46 && val == "") {
                PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                event.preventDefault();
                return false;
            }
            if (val.split(".").length > 1 && event.keyCode == 46) {
                PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                event.preventDefault();
                return false;
            }
            $(id).popover('destroy');
        } catch (e) {

        }
    });

    //// For Amount upto 3 decimal Place \\\
    $('.Decimal4').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(id).popover('destroy');
        } catch (e) { }
    });
    $('.Decimal4').keypress(function (e) {
        try {
            var id = ('#' + this.id);
            var val = $(id).val();
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);

            if (chCode != 46) {
                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Enter Valid Number');
                    return false; //Non Numeric Value Return Directly;
                }
            }

            if (val.indexOf(".") != -1 && val.indexOf(".") + 5 == val.length && event.keyCode != 8 && event.keyCode != 17 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 9) {
                PopOverError(id, 'top', 'This Allow Only 4 Decimal Place');
                event.preventDefault();
                return false;
            }
            if (event.keyCode == 46 && val == "") {
                PopOverError(id, 'top', 'This Allow Only 4 Decimal Place');
                event.preventDefault();
                return false;
            }
            //if (val.split(".").length > 3 && event.keyCode == 46) {
            if (val.split(".").length > 1 && event.keyCode == 46) {
                PopOverError(id, 'top', 'This Allow Only 4 Decimal Place');
                event.preventDefault();
                return false;
            }

            $(id).popover('destroy');
        } catch (e) {

        }
    });

    //// Disable Pasting IN Text Box \\\\
    $('input.numberonly, input.datepicker, input.Money, input.Email, input.Decimal4, input.AlphaNum').bind('copy paste', function (e) {
        e.preventDefault();
    });

    $("input").attr("autocomplete", "off");
    $(".datepicker").attr("autocomplete", "off");
    $(".Money").attr("autocomplete", "off");

    $('.Email').blur(function (e) {
        try {
            var id = ('#' + this.id);
            var email = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (email.length <= 0 || email == '' || email == undefined) {
                return false;
            }
            if (email.match(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i)) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');

            } else {
                PopOverError(id, 'top', 'Invalid Email Address.');
                $(id).focus();
            }
        } catch (e) { }
    });
    //$(".numberonly").attr("placeholder", "0");
    //$(".datepicker").attr("placeholder", "DD/MM/YYYY");
    //$(".Money").attr("placeholder", "00.00Rs.");

    $('.groupName').keypress(function (e) {

        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            $(id).popover('destroy');
            if ((chCode > 47 && chCode < 58) || (chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode === 46 || chCode == 32) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                    $(id).popover('destroy');
                    return;
                }

                var txt = $(id).val();
                var numb = txt.match(/\d/g);
                if (numb != undefined) {
                    if (numb.length >= 1) {
                        PopOverError(id, 'top', 'Only 1 Number Allow!');
                        event.preventDefault();
                        return false;
                    }
                }
                if (chCode == 46) {
                    if (e.keyCode === 46 && txt.split('.').length === 2) {
                        PopOverError(id, 'top', 'Only 1 "." Allow!');
                        event.preventDefault();
                        return false;
                    }
                }

                //return true;
            } else {
                PopOverError(id, 'top', 'Allow Numbers Only!');
                event.preventDefault();
                //return false;
            }
        } catch (e) {

        }

    });
    $('.groupName').blur(function (e) {
        var id = ('#' + this.id);
        $(id).popover('destroy');
    });

    $('.Alphaonly').keypress(function (e) {
        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                $(id).popover('destroy');
                return true;
            } else {
                PopOverError(id, 'top', 'Allow Characters Only!');
                return false;
            }
        } catch (e) {

        }
    });
    $('.Alphaonly').blur(function (e) {
        var id = ('#' + this.id);
        $(id).popover('destroy');
    });

    $('.Mobile').blur(function (e) {
        try {
            var id = ('#' + this.id);
            var mobile = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (mobile == undefined || mobile == '') {
                return false;
            }


            if (mobile.length == 10) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');

            } else {
                $(id).focus();
                PopOverError(id, 'top', 'Enter 10 Digit Mobile No.');
                e.preventDefault();
                //return false;
            }
        } catch (e) { }
    });

    $('.PinCode').blur(function (e) {
        try {
            var id = ('#' + this.id);
            var pinCode = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (pinCode == undefined || pinCode == '') {
                return false;
            }
            if (pinCode.length == 6) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');

            } else {
                PopOverError(id, 'top', 'Enter 6 Digit PinCode.');
                $(id).focus();
                event.preventDefault();
                return false;
            }
        } catch (e) { }
    });

    $('.Time').keypress(function (e) {
        try {
            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == ":") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf(":"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 24) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Hours!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + ":");
                    } else if ($(id).val().length == 5) {
                        var Second = $(id).val().substring(3, 6);
                        if (Second < 1 || Second > 12) {
                            var corr = $(id).val().replace(":" + Second, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Second!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");

                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });

    $('.GSTIN').blur(function (e) {
        var id = ('#' + this.id);
        var val = $(id).val();
        if (val.length >= 1) {
            val = val.toUpperCase();
            var patt = new RegExp(/[0-9]{2}[(A-Z)]{5}\d{4}[(A-Z)]{1}[1-9A-Z]{1}[Z]{1}[0-9A-Z]{1}$/);
            var GstinIsValid = patt.test(val);

            if (!GstinIsValid) {
                PopOverError(id, 'top', 'Invalid GSTIN.'); // Ex.-12ABCDE1234A1Z1
                event.preventDefault();
                $(id).focus();
                return false;
            }

            $(id).popover('destroy');
            return true;
        }

        $(id).popover('destroy');
        return true;
    });

    $('.GSTIN').keypress(function (e) {

        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);

            //var patt = new RegExp(/[(A-Z)(a-z)(0-9)]$/);
            //var ValidKey = patt.test(event.key);
            //if (!ValidKey) {
            //    PopOverError(id, 'top', 'Invalid Characters!');
            //    return false;
            //}
            debugger
            //$(id).popover('destroy');
            if ($(id).val().length < 2) {

                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Allow Numbers Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 7) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Characters Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 11) {

                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Allow Numbers Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 12) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Characters Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 13) {
                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32 || (chCode > 47 && chCode < 58)) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Alpha Or Numbers Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 14) {
                if (chCode == 90 || chCode == 122) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Z Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 15) {
                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32 || (chCode > 47 && chCode < 58)) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Alpha Or Numbers Only!');
                    return false;
                }
            }

            if ($(id).val().length >= 15) {
                PopOverError(id, 'top', 'Allow 15 Characters Only!');
                return false;
            }
            else {
            }

            $(id).popover('destroy');

        } catch (e) {

        }
    });

    $('.AlphaNum').keypress(function (event) {
        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            $(id).popover('destroy');
            if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32 || (chCode > 47 && chCode < 58)) {
                $(id).popover('destroy');
                return true;
            } else {
                PopOverError(id, 'top', 'Allow Aplha And Number Only!');
                return false;
            }
        } catch (e) {

        }
    });

    $('.AlphaNum').blur(function (e) {
        var id = ('#' + this.id);
        $(id).popover('destroy');
    });

    $('div[placeholder]').each(function (key, data) {

        var inputList = $('#' + data.id).find('input')
        var plac = $('#' + data.id).attr('placeholder');
        for (var i = 0; i < inputList.length - 1; i++) {

            if (plac == "p")
                inputList[i].setAttribute("placeholder", "--Select--");
            else
                inputList[i].setAttribute("placeholder", plac);
        }
    });

    $('.Series').keypress(function (e) {

        var id = ('#' + this.id);
        var val = $(id).val();
        //val = val.toUpperCase();
        var patt = new RegExp(/[A-Za-z0-9/-]$/);
        var IsValidSeries = patt.test(event.key);
        if (!IsValidSeries) {
            PopOverError(id, 'top', 'Invalid Series Key.');
            event.preventDefault();
            $(id).focus();
            return false;
        }
        if (val.length >= 10) {
            PopOverError(id, 'top', 'Only 10 Characters Allow.');
            event.preventDefault();
            $(id).focus();
            return false;
        }
        $(id).popover('destroy');
        return true;
    });

    $('.Series').blur(function (e) {
        var id = ('#' + this.id);
        var val = $(id).val();
        if (val.length >= 1) {
            var patt = new RegExp(/[A-Za-z0-9/-]$/);
            var IsValidSeries = patt.test(val);

            if (!IsValidSeries) {
                PopOverError(id, 'top', 'Invalid Series.');
                event.preventDefault();
                $(id).focus();
                return false;
            }

            $(id).popover('destroy');
            return true;
        }

        $(id).popover('destroy');
        return true;
    });

});

function Loading() {
    try {
        $('#ModalLoader').modal();
    } catch (e) { }
}
function openModal(errMsg, type, href) {
    debugger
    try {
        if (type == "True") {
            //$('#header').addClass("bg-info");
            //$('#title').html("Success!")
            //$('#txtMsg').html(errMsg);
            //$('#header').removeClass("bg-danger");
            //$('#btnYes').attr('href', "#ModalInfo");
            $('#header').addClass("bg-danger");
            $('#title').html("Error!")
            $('#txtMsg').html(errMsg);
            $('#header').removeClass("bg-info");
            $('#btnYes').attr('href', href);


            var a = $('#ModalInfo').find('*.close');
            a.removeClass('hidden');
        }
        else {
            $('#header').addClass("bg-danger");
            $('#title').html("Error!")
            $('#txtMsg').html(errMsg);
            $('#header').removeClass("bg-info");
            $('#btnYes').attr('href', href);

            //var checkClose = $('div#modal-content').find('*button[data-dismiss="modal"]');
            //console.log(checkClose);            
            //$('div#modal-content').find('*button[data-dismiss="modal"]').removeClass("hidden");

        }

        $('#ModalInfo').modal();
        $("#btnModelClose").blur();
        $("#btnModelClose").focus();
    } catch (e) { }
}
function openModal(errMsg, type, href, btnVal) {
    debugger
    try {
        if (type == "True") {
            //$('#header').addClass("bg-info");
            //$('#title').html("Success!")
            //$('#txtMsg').html(errMsg);
            //$('#header').removeClass("bg-danger");
            //$('#btnYes').attr('href', "#ModalInfo");
            $('#header').addClass("bg-danger");
            $('#title').html("Error!")
            $('#txtMsg').html(errMsg);
            $('#header').removeClass("bg-info");

            var a = $('#ModalInfo').find('*.close');
            a.removeClass('hidden');
        }
        else {
            $('#header').addClass("bg-danger");
            $('#title').html("Error!")
            $('#txtMsg').html(errMsg);
            $('#header').removeClass("bg-info");


            //var checkClose = $('div#modal-content').find('*button[data-dismiss="modal"]');
            //console.log(checkClose);            
            //$('div#modal-content').find('*button[data-dismiss="modal"]').removeClass("hidden");

        }

        $('#btnYes').attr('href', href);
        $('#btnYes').text(btnVal);
        $('#ModalInfo').modal();
        $("#btnModelClose").blur();
        $("#btnModelClose").focus();
    } catch (e) { }
}
function LoadActive() {
    $('.loading').addClass(' active');
}
function PopOverError(id, plac, msg) {
    try {
        $(id).popover({
            title: 'Error!',
            trigger: 'manual',
            placement: plac,
            content: function () {
                var message = msg; //"Allow Numbers Only!";
                return message;
            }
        });
        $(id).popover("show");
    } catch (e) { }
}

$(document).ready(function () {

    $('.has-submenu-nav2').click(function () {
        $(this).siblings('li').find('.nav-list').collapse('hide');
    });

    var current_url = $(location).attr('pathname');
    var pagename = '..' + current_url;
    $('aside a').each(function () {
        var href = $(this).attr('href');
        if (href == pagename) {
            $(this).parent('li').addClass("active");
            $(this).parents('ul').addClass("in");
        }
    });
});

function preventBack() { window.history.forward(); }
setTimeout("preventBack()", 0);
window.onunload = function () { null };

var openDrop = false;
document.onmouseup = function (e) { /// Close Checkbox DropDown On Outside Click.
    try {
        var cb = document.getElementById('divdrop');
        if (openDrop == true) {
            openDrop = false;
            return false;
        }
        if (cb.classList.item(2) == 'in') {
            cb.hidden = true;
            cb.classList.remove('in');
        }
    } catch (e) {

    }
}
function Opendrop() { //// Open Checbox DropDown.
    try {
        document.getElementById('divdrop').classList.add('in');
    } catch (e) {

    }
}

function Closedrop(e) { //
    try {
        if (e != 1) {
            document.getElementById('divdrop').classList.add('in');
        }
    } catch (e) {

    }
}

function ChoosenDDL() {
    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    $(".chzn-single").attr("style", " height: 25px;line-height: inherit;");
    $(".chzn-container").attr("style", "display:block;");
}
function LoadBasic() {

    function PopOverError(id, plac, msg) {
        try {
            $(id).popover({
                title: 'Error!',
                trigger: 'manual',
                placement: plac,
                content: function () {
                    var message = msg; //"Allow Numbers Only!";
                    return message;
                }
            });
            $(id).popover("show");
        } catch (e) { }
    }

    $('.PanNo').blur(function (e) {
        var id = ('#' + this.id);
        var val = $(id).val();
        if (val.length >= 1) {
            if (val.length < 10) {
                PopOverError(id, 'top', 'Invalid Pan No.');
                event.preventDefault();
                $(id).focus();
                return false;
            } else {
                $(id).popover('destroy');
                return true;
            }
        }
    });

    $('.PanNo').keypress(function (e) {
        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            $(id).popover('destroy');

            if ($(id).val().length < 5) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123)) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Characters Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 9) {

                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Allow Numbers Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 10) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Characters Only!');
                    return false;
                }
            }

            if ($(id).val().length == 10) {
                PopOverError(id, 'top', 'Allow 10 Characters Only!');
                return false;
            }
            else {
            }

        } catch (e) {

        }
    });

    $('.GSTIN').blur(function (e) {

        var id = ('#' + this.id);
        var val = $(id).val();
        if (val.length >= 1) {
            val = val.toUpperCase();
            var patt = new RegExp(/[0-9]{2}[(A-Z)]{5}\d{4}[(A-Z)]{1}[1-9A-Z]{1}[Z]{1}[0-9A-Z]{1}$/);
            var GstinIsValid = patt.test(val);

            if (!GstinIsValid) {
                PopOverError(id, 'top', 'Invalid GSTIN.');
                event.preventDefault();
                $(id).focus();
                return false;
            }

            $(id).popover('destroy');
            return true;
        }

        $(id).popover('destroy');
        return true;
    });

    $('.GSTIN').keypress(function (e) {

        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);

            //var patt = new RegExp(/[(A-Z)(a-z)(0-9)]$/);
            //var ValidKey = patt.test(event.key);
            //if (!ValidKey) {
            //    PopOverError(id, 'top', 'Invalid Characters!');
            //    return false;
            //}


            if ($(id).val().length < 2) {

                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Allow Numbers Only!');
                    //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                    return false;
                }
            }
            else if ($(id).val().length < 7) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Characters Only!');
                    //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                    return false;
                }
            }
            else if ($(id).val().length < 11) {

                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Allow Numbers Only!');
                    //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                    return false;
                }
            }
            else if ($(id).val().length < 12) {
                debugger

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Characters Only!');
                    //PopOverError(id, 'top', 'Invalid Format \n Ex - 000112251!');
                    //event.preventDefault();
                    return false;
                }
            }
            else if ($(id).val().length < 13) {

                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32 || (chCode > 47 && chCode < 58)) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Alpha Or Numbers Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 14) {

                if (chCode == 90 || chCode == 122) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Z Only!');
                    return false;
                }
            }
            else if ($(id).val().length < 15) {
                if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32 || (chCode > 47 && chCode < 58)) {
                    $(id).popover('destroy');
                } else {
                    PopOverError(id, 'top', 'Allow Alpha Or Numbers Only!');
                    return false;
                }
            }

            if ($(id).val().length >= 15) {
                PopOverError(id, 'top', 'Allow 15 Characters Only!');
                return false;
            }

            $(id).popover('destroy');

        } catch (e) {

        }
    });

    $('.Time').keypress(function (e) {
        try {

            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == ":") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf(":"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 12) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Hours!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + ":");
                    } else if ($(id).val().length == 5) {
                        var mm = $(id).val().substring(3, 6);
                        if (mm < 1 || mm > 12) {
                            var corr = $(id).val().replace(":" + mm, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Second!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");

                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });
    ///////////Zero Not Alloww///////////////////// 

    $('.NotAllowZero').keyup(function (event) {
        try {
            var currentVal = $(this).val();
            var id = ('#' + this.id);
            if (currentVal.length == 1 && (event.which == 48 || event.which == 96)) {
                currentVal = currentVal.slice(0, -1);
                PopOverError(id, 'top', 'Zero Not Allow!');
                $(this).val(currentVal);
                return false;
            }
            else {
                $(this).val(currentVal);
                $(id).popover('destroy');
                return true;
            }

        }
        catch (e) {

        }
    });
    $('.NotAllowZero').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(this).val(currentVal);
            $(id).popover('destroy');
        } catch (e) { }
    });

    //// For Numeri Value Only \\\\\\
    $('.numberonly').keypress(function (event) {

        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Allow Numbers Only!');
                return false;
            }
            else {
                $(id).popover('destroy');
                return true;
            }
        } catch (e) {

        }
    });

    $('.numberonly').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(id).popover('destroy');
        } catch (e) { }
    });
    /// For Alpha & Numeric Only \\\\
    $('.AlphaNum').keypress(function (event) {

        try {
            var chCode = (event.charCode === undefined) ? event.keyCode : event.charCode;
            var id = ('#' + this.id);
            if ((chCode > 64 && chCode < 91) || (chCode > 96 && chCode < 123) || chCode == 32 || (chCode > 47 && chCode < 58)) {
                $(id).popover('destroy');
                return true;
            } else {
                PopOverError(id, 'top', 'Allow Aplha And Number Only!');
                return false;
            }
        } catch (e) {

        }
    });

    $('.AlphaNum').blur(function (e) {
        var id = ('#' + this.id);
        $(id).popover('destroy');
    });

    //// For Valid Date Allow \\\\\\
    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true });
    $(".setDate.datepicker").datepicker({ dateFormat: 'dd/mm/yy', maxDate: '0', changeYear: true, changeMonth: true, }).datepicker("setDate", "0");

    $('.datepicker').blur(function (e) {
        try {

            var id = ('#' + this.id);
            var date = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (date.length <= 0 || date == '' || date == undefined) {
                return false;
            }
            if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');
            } else {
                PopOverError(id, 'top', 'Invalid Date.');
                $(id).focus();
            }
        } catch (e) { }
    });
    $('.datepicker').keypress(function (e) {
        try {
            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == "/") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 31) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Day!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 5) {
                        var month = $(id).val().substring(3, 6);
                        if (month < 1 || month > 12) {
                            var corr = $(id).val().replace("/" + month, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Month!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });

    $('.dateFormat').blur(function (e) {
        try {

            var id = ('#' + this.id);
            var date = $(id).val();
            $(id).popover('destroy');
            var valid = true;
            if (date.length <= 0 || date == '' || date == undefined) {
                return false;
            }
            if (date.match(/^(?:(0[1-9]|[12][0-9]|3[01])[\- \/.](0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                valid = true;
            } else {
                valid = false;
            }

            if (valid) {
                $(id).popover('destroy');
            } else {
                PopOverError(id, 'top', 'Invalid Date.');
                $(id).focus();
            }
        } catch (e) { }
    });
    $('.dateFormat').keypress(function (e) {
        try {
            $(id).popover('destroy');
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);
            if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                PopOverError(id, 'top', 'Enter Valid Key For Date.');
                return false; //Non Numeric Value Return Directly;
            }
            else {
                if ($(id).val() === undefined) {
                    event.preventDefault();
                    return;
                }
                if (e.key == "/") {
                    PopOverError(id, 'top', 'This Key Is Invalid!');
                    event.preventDefault();
                    return false;
                }
                if (e.keyCode != 8) {

                    var DateVal = $(id).val();
                    if (e.keyCode == 191) {
                        var corr = DateVal.slice(0, DateVal.lastIndexOf("/"));
                        PopOverError(id, 'top', 'Enter Valid Date!');
                        $(id).val(corr);
                        event.preventDefault();
                        return false;
                    }

                    if ($(id).val().length == 2) {
                        if ($(id).val() < 1 || $(id).val() > 31) {
                            $(id).val("")
                            PopOverError(id, 'top', 'Enter Valid Day!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 5) {
                        var month = $(id).val().substring(3, 6);
                        if (month < 1 || month > 12) {
                            var corr = $(id).val().replace("/" + month, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Month!');
                            event.preventDefault();
                            return false;
                        }
                        $(id).val($(id).val() + "/");
                    } else if ($(id).val().length == 10) {
                        var Inputyear = $(id).val().substring(6, 11);
                        var NowYear = new Date().getUTCFullYear();
                        if (Inputyear < 1900 || Inputyear > NowYear) {
                            var corr = $(id).val().replace(Inputyear, "");
                            $(id).val(corr);
                            PopOverError(id, 'top', 'Enter Valid Year!');
                            event.preventDefault();
                            return false;
                        }
                    }
                    else { $(id).popover('destroy'); }
                }
            }
        } catch (e) { }
    });

    //// For Amount upto 2 decimal Place \\\
    $('.Money').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(id).popover('destroy');
        } catch (e) { }
    });
    $('.Money').keypress(function (e) {
        try {
            var id = ('#' + this.id);
            var val = $(id).val();
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);


            if (chCode != 46) {
                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Enter Valid Amount');
                    return false; //Non Numeric Value Return Directly;
                }
            }
            if (val.indexOf(".") != -1 && val.indexOf(".") + 3 == val.length && event.keyCode != 8 && event.keyCode != 17 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 9) {
                PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                event.preventDefault();
                return false;
            }
            if (event.keyCode == 46 && val == "") {
                PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                event.preventDefault();
                return false;
            }
            if (val.split(".").length > 1 && event.keyCode == 46) {
                PopOverError(id, 'top', 'This Allow Only Amount Ex-1000.20');
                event.preventDefault();
                return false;
            }
            $(id).popover('destroy');
        } catch (e) {

        }
    });

    //// For Amount upto 4 decimal Place \\\
    $('.Decimal4').blur(function (e) {
        try {
            var id = ('#' + this.id);
            $(id).popover('destroy');
        } catch (e) { }
    });
    $('.Decimal4').keypress(function (e) {
        try {
            var id = ('#' + this.id);
            var val = $(id).val();
            var chCode = (e.charCode === undefined) ? e.keyCode : e.charCode;
            var id = ('#' + this.id);


            if (chCode != 46) {
                if (chCode > 31 && (chCode < 48 || chCode > 57)) {
                    PopOverError(id, 'top', 'Enter Valid Number');
                    return false; //Non Numeric Value Return Directly;
                }
            }

            if (val.indexOf(".") != -1 && val.indexOf(".") + 5 == val.length && event.keyCode != 8 && event.keyCode != 17 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 46 && event.keyCode != 9) {
                PopOverError(id, 'top', 'This Allow Only 4 Decimal Place');
                event.preventDefault();
                return false;
            }
            if (event.keyCode == 46 && val == "") {
                PopOverError(id, 'top', 'This Allow Only 4 Decimal Place');
                event.preventDefault();
                return false;
            }
            //if (val.split(".").length > 3 && event.keyCode == 46) {
            if (val.split(".").length > 1 && event.keyCode == 46) {
                PopOverError(id, 'top', 'This Allow Only 4 Decimal Place');
                event.preventDefault();
                return false;
            }
            $(id).popover('destroy');
        } catch (e) {

        }
    });

    $('.Series').keypress(function (e) {

        var id = ('#' + this.id);
        var val = $(id).val();

        //val = val.toUpperCase();
        var patt = new RegExp(/[A-Za-z0-9/-]$/);
        var IsValidSeries = patt.test(event.key);
        if (!IsValidSeries) {
            PopOverError(id, 'top', 'Invalid Series Key.');
            event.preventDefault();
            $(id).focus();
            return false;
        }
        if (val.length >= 10) {
            PopOverError(id, 'top', 'Only 10 Characters Allow.');
            event.preventDefault();
            $(id).focus();
            return false;
        }

        $(id).popover('destroy');
        return true;
    });
    $('.Series').blur(function (e) {
        var id = ('#' + this.id);
        var val = $(id).val();
        if (val.length >= 1) {
            var patt = new RegExp(/[A-Za-z0-9/-]$/);
            var IsValidSeries = patt.test(val);

            if (!IsValidSeries) {
                PopOverError(id, 'top', 'Invalid Series.');
                event.preventDefault();
                $(id).focus();
                return false;
            }

            $(id).popover('destroy');
            return true;
        }

        $(id).popover('destroy');
        return true;
    });


    //// Disable Pasting IN Text Box \\\\
    $('input.numberonly, input.datepicker, input.Money, input.Decimal4, input.AlphaNum').bind('copy paste', function (e) {
        //PopOverError(('#' + this.id), 'top', 'Do Not Paste');
        e.preventDefault();
    });

    $("input").attr("autocomplete", "off");
    $(".datepicker").attr("autocomplete", "off");
    $(".Money").attr("autocomplete", "off");

    function ReLogin(IsLogedIn) {
        debugger

        $('#ModalReLogin').modal();
    }

    $('div[placeholder]').each(function (key, data) {

        var inputList = $('#' + data.id).find('input')
        var plac = $('#' + data.id).attr('placeholder');
        for (var i = 0; i < inputList.length - 1; i++) {

            if (plac == "p")
                inputList[i].setAttribute("placeholder", "--Select--");
            else
                inputList[i].setAttribute("placeholder", plac);
        }
    });
}

//function ReLogin(IsLogedIn) {
//    
//    $('#ModalReLogin').modal();
//}
//function ReLoginClose(IsLogedIn) {
//    
//    //$('#modal').modal('hide');
//    $("#ModalReLogin .close").click();
//}
$(document).ready(function () {
    $('.ham-nav2').click(function () {
        $('.nav-list.collapse').collapse('hide');
    });

    $('body').click(function (e) {
        if (!$('body').hasClass('aside-collapsed')) {
            return;
        }
        if (!$(e.target).is('nav.sidebar')) {
            if (!$(e.target).is('.aside-collapsed > .nav-list')) {
                if (!$(e.target).is('.nav-list a.accordion-heading')) {
                    $('li.list-item-1 > ul.nav2.collapse.in').removeClass('in');
                    console.clear();
                    console.log(e.target);
                }
            }
        }
    });
});
