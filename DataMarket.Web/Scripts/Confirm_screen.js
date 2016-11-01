function myFunction() {
    var option = true;
    var savedFilterId = $("#myId").val();
    var x = "DeleteList/Logged/" +myId;
    if (confirm("Are you sure you want to delete the list?")) {
        var json = { savedFilterId, option };

        $.ajax({
            method: "POST",
            url: '/Logged/DeleteList',
            contentType: 'application/json',
            dataType: "json",
            data: JSON.stringify(json),
            async: false,
            success: function (ReturnURL) {
                location.replace(ReturnURL)
            }
        });
    }
    else {
        return false;
    }
}