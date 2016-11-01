$(document).ready(function () {
    $("#myListsLink").click(function () {
        $("#myListForm").submit();
    });

    $("#myCreateLink").click(function () {
        $("#myCreateForm").submit();
    });

    $("#my-button-myLists").click(function () {
        $("#myListForm").submit();
    });

    $("#goBackToMyLists").click(function () {
        console.log("ok");
        $("#myListForm").submit();
    });

    $("#my-button-yes").click(function () {
        $("#mySaveListForm").submit();
    });

    $("#mySortByDateLink").click(function () {
        $("#sortType").prop("value", "date");
        $("#mySortForm").submit();
    });

    $("#mySortByMarketLink").click(function () {
        $("#sortType").prop("value", "market");
        $("#mySortForm").submit();
    });

    /* Set colors to Status column */

    $('td:contains("Failed")').css('color', 'red');
    $('td:contains("Ready")').css('color', 'green');
    $('td:contains("Queued")').css('color', 'yellow');
    $('td:contains("Waiting")').css('color', 'orange');

    /* Enable-Disable links */    
    
    $('.my-status tr').each(function () {
        if ($('td:contains("Ready")') == false) {

        }
    });

});