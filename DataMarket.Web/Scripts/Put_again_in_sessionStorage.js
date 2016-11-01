$(document).ready(function () {
    $(function () {
        var filterValueIds = $(".filterValueIds");
        for (var i = 0; i < filterValueIds.length; i++) {
            sessionStorage.setItem(filterValueIds[i].value, true);
        }
        console.log(sessionStorage);
    });

    $('.arrow-down').click(function () {
        var myClick = "." + $(this).val();
            $(myClick).toggle(function () {
                $(this).css("display") == "none"
            });           
    });

});