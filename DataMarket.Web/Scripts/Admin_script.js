$(document).ready(function () {

    $(function () {
        if (sessionStorage.length > 0 && $(".pagination li").hasClass('active')) {
            $('.my-button').removeAttr('disabled');
            for (var i = 0; i < sessionStorage.length; i++) {
                checkBoxValue = sessionStorage.key(i);
                var isChecked = sessionStorage.getItem(checkBoxValue) === "true" ? true : false;
                $("input[value=" + checkBoxValue + "]").prop("checked", isChecked || false);
                if (isChecked == true) {
                    $('label[for=' + checkBoxValue + ']').text("Enabled").css('font-style', 'italic').css('color', 'crimson');
                }
                else {
                    $('label[for=' + checkBoxValue + ']').text("Disabled").css('font-style', 'italic').css('color', 'crimson');
                }
            }
        }
        else {
            $('.my-button').attr('disabled');
        }
        
    });

    $("input[type=checkbox]").change(function () {
        $('.my-button').removeAttr('disabled');
        var selected = $(this);
        if ($(this).is(":checked")) {
            sessionStorage.setItem(selected[0].value, "true");           
            $('label[for=' + $(this).attr('id') + ']').text("Enabled").css('font-style', 'italic').css('color', 'crimson');
        }
        else {
            sessionStorage.setItem(selected[0].value, "false");
            $('label[for=' + $(this).attr('id') + ']').text("Disabled").css('font-style', 'italic').css('color', 'crimson');
        }
        Session();
    });

    //$("input[type=checkbox]").each(function () {
    //    var selected = $(this);
    //    if ($(this).is(":checked")) {
    //        sessionStorage.setItem(selected[0].value, "true");            
    //    }
    //    else {
    //        sessionStorage.setItem(selected[0].value, "false");
    //    }
    //});

    function Session() {
        console.log("load started: ");
        console.log(sessionStorage);
        for (var i = 0; i < sessionStorage.length; i++) {
            checkBoxValue = sessionStorage.key(i);
            var isChecked = sessionStorage.getItem(checkBoxValue) === "true" ? true : false;
            $("input[value=" + checkBoxValue + "]").prop("checked", isChecked || false);
        }
    };

    //$(function() {
    //    console.log("load started: ");
    //    console.log(sessionStorage);
    //    for (var i = 0; i < sessionStorage.length; i++) {
    //        checkBoxValue = sessionStorage.key(i);
    //        var isChecked = sessionStorage.getItem(checkBoxValue) === "true" ? true : false;
    //        $("input[value=" + checkBoxValue + "]").prop("checked", isChecked || false);
    //    }
    //});

    $('.my-button').click(function () {       
        SendStorage();
    });

    function SendStorage() {
        var list = [];

        for (var i = 0; i < sessionStorage.length; i++) {
            checkBoxValue = sessionStorage.key(i);
            var isChecked = sessionStorage.getItem(checkBoxValue) === "true" ? true : false;
            if (isChecked == true) {
                $('label[for=' + checkBoxValue + ']').text("Enabled").css('font-style', 'normal').css('color', 'white');
            }
            else {
                $('label[for=' + checkBoxValue + ']').text("Disabled").css('font-style', 'normal').css('color', 'white');
            }
            var obj = { UserId: checkBoxValue, IsEnabled: isChecked };
            list.push(obj);
        }

        if (list.length != 0) {
            var json = { list };

            $.ajax({
                method: "POST",
                url: '/Logged/AdminSaveChanges',
                contentType: 'application/json',
                dataType: "json",
                data: JSON.stringify(json),
                async: false,
            });
            sessionStorage.clear();
        }
    };

});