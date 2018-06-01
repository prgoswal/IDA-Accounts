$(document).bind('keydown', 'shift+v', function () {
    window.location.href = 'frmDefaultVoucher.aspx';
});
$(document).bind('keydown', 'shift+b', function () {
    window.location.href = 'frmBankPayment.aspx';
});
$(document).bind('keydown', 'shift+t', function () {
    window.location.href = 'frmBankReceipt.aspx';
});
$(document).bind('keydown', 'shift+r', function () {
    window.location.href = 'frmCashReceipt.aspx';
});
$(document).bind('keydown', 'shift+p', function () {
    window.location.href = 'frmCashPayment.aspx';
});
$(document).bind('keydown', 'shift+h', function () {
    window.location.href = 'Dashboard.aspx';
});
$(document).bind('keydown', 'shift+x', function () {
    $(".voucher_key").click();
});