/* Save Selected CheckBoxes in SessionStorage */

$(document).ready(function () {
    var putResultInSessionStorage = function () {
        console.log("putResultInSessionStorage started:");
        if (sessionStorage != undefined) {
            var selected = $(this);
            if (selected.is(":checked")) {
                sessionStorage.setItem(selected[0].value, "true");
            }
            else {
                sessionStorage.removeItem(selected[0].value);
            }
            console.log(sessionStorage);
        }
        getCount();
    };

    function putAllInSessionStorage() {
        console.log("putAllInSessionStorage started:");
        if (sessionStorage != undefined) {
            var selected = $("input[type = checkbox]");
            for (var i = 0; i < selected.length; i++) {
                if (selected.is(":checked")) {
                    sessionStorage.setItem(selected[i].value, "true");
                }
                else {
                    sessionStorage.removeItem(selected[i].value);
                }
            }
            console.log(sessionStorage);
        }
        getCount();
    };

    

    $("input[type=checkbox]").on("change", putResultInSessionStorage);

    $(function () {
        console.log("load started: ");
        console.log(sessionStorage);
        for (var i = 0; i < sessionStorage.length; i++) {
            checkBoxValue = sessionStorage.key(i);
            var isChecked = sessionStorage.getItem(checkBoxValue) === "true" ? true : false;
            $("input[value=" + checkBoxValue + "]").prop("checked", isChecked || false);                    
        }       
        getCount();
    });

    /* Selected Group/Filter */

    $(function () {
        var groupId = document.getElementById("groupId").value;
        var filterId = document.getElementById("filterId").value;       
        var htmlObjectGroup = document.getElementById("Group" + groupId)
        var htmlObjectFilter = document.getElementById("Filter" + filterId)
        htmlObjectGroup.style.background = "#808080";
        htmlObjectFilter.style.background = "#808080";
        htmlObjectGroup.style.opacity = "0.5";
        htmlObjectFilter.style.opacity = "0.5";
    });

    $("#my-button-selectAll").click(function () {
        if ($(this).val() == 'Select All') {
            $("input:checkbox").prop('checked', "checked");
            $(this).val('Deselect All');
        }
        else {
            $("input:checkbox").prop('checked', "");
            $(this).val('Select All');
        }
        putAllInSessionStorage();
    });

    $('input:checkbox').change(function () {
        if ($(this).prop("checked") == false) {
            $(".my-button").val('Select All');
        }
    });
    
    function getCount() {
        console.log("getCount started");
        var list = { 'myCheckboxes': [] };
        //$("input:checked").each(function () {
        //    list['myCheckboxes'].push($(this).val());
        //});
        for (var i = 0; i < sessionStorage.length; i++) {
            list['myCheckboxes'].push(sessionStorage.key(i));
        }
        if (list['myCheckboxes'].length != 0) {
            console.log(list['myCheckboxes'])

            $('#loading').html('<img src="/Content/Images/loader.gif">').load();
            var json = { selectedFilters: list['myCheckboxes'] };

            $.ajax({
                method: "POST",
                url: '/getCountAndSaveDataMarketListInProgres/getCount',
                contentType: 'application/json',
                dataType: "json",
                data: JSON.stringify(json),
                async: false,
                success: function (data) {
                    var t;
                    $('#countValue').text(data);
                    $('#loading').html(' ');

                }

            });
        }

        else {
            data = 0;
            $('#countValue').text(data)
        }
    };

    //$('.checkFilter').click(getCount);
    });

function deleteListInProgressItem(idToRemove) {
    console.log("deleteListInProgressItem: "+ idToRemove);
    if (sessionStorage != null) {
        sessionStorage.removeItem(idToRemove);
    }
    console.log(sessionStorage);
    $("#myDeleteListInProgressItemForm"+idToRemove).submit();
};



