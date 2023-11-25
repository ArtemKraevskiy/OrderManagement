// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function reloadOrderTable() {
    $('#orderTableContainer').html('<div class="loader"></div>');
    var url = $('#orderTableContainer').data('url');
    var formData = $('#orderSearchForm').serialize();
    $('#orderTableContainer').load(url, formData);
}

function reloadOrderSearchForm() {
    $('#orderSearchContainer').html('<div class="loader"></div>');
    var url = $('#orderSearchContainer').data('url');
    $('#orderSearchContainer').load(url);
}

function reloadOrderDetails(orderId) {
    $('#orderDetailsBody').html('<div class="loader"></div>');
    var url = $('#orderDetailsBody').data('url');
    $('#orderDetailsBody').load(url, {
        Id: orderId
    });
    $("#orderDetailsModal").modal('show');
}

//function reloadOrderItemTable(orderId) {
//    var url = $('#orderItemTableContainer').data('url');
//    $('#orderItemTableContainer').html('<div class="loader"></div>');
//    $('#orderItemTableContainer').load(url, {
//        OrderId: orderId
//    });
//}

function reloadProviderTable() {
    $('#providerTableContainer').html('<div class="loader"></div>');
    var url = $('#providerTableContainer').data('url');
    $('#providerTableContainer').load(url, $('#providerSearchForm').serialize());
}