// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function doDelete(bookingId) {
    var val = confirm('Do you want to delete?');
    if (val) {
        window.location = "/Booking/doDelete?bookingId=" + bookingId;
    }
}
