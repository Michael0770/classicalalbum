$(document).ready(function () {
    $('#data tbody tr:odd').addClass("odd");
    $('#data tbody tr').mouseover(function () {
        $(this).addClass('dataHover');
    });
    $('#data tbody tr').mouseout(function () {
        $(this).removeClass('dataHover');
    });
});

