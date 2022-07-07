jQuery(function ($) {
    // Load dialog on page load
    //$('#basic-modal-content').modal();

    // Load dialog on click
    $('#basic-modal .basic').click(function (e) {
        $('#basic-modal-content').modal();

        return false;
    });

    $('#basic-modal2 .basic').click(function (e) {
        alert('testing alert ');
        $('#basic-modal-content2').modal();

        return false;
    });
});