// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function doDelete(bookingId) {
    var val = confirm('Do you want to delete?');
    if (val) {
        window.location = "/Booking/doDelete?bookingId=" + bookingId;
    }
}

function validateForm() {
    if (document.forms["bookingForm"]["marriageHall"].value == "") {
        alert('Please select the hall to book');
        return false;
    } else if (document.forms["bookingForm"]["fromDate"].value == "") {
        alert('Please select From date.');
        return false;
    } else if (document.forms["bookingForm"]["toDate"].value == "") {
        alert('Please select the To Date.');
        return false;
    }
    return true;
}

function loadBookingDates() {
    var hallId = document.forms["bookingForm"]["marriageHall"].value;
    window.location = '/Booking/doCreateBooking?hallId=' + hallId;
    document.forms["bookingForm"]["marriageHall"].value == hallId;  
}
