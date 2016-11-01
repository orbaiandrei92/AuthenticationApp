$(document).ready(function () {

        $('#result-form').validate({
            rules: {
                    list: {
                        required: true,
                        maxlength: 50
                    }                
            },
            messages: {
                    list: {
                        required: " Please name your list!",
                        maxlength: "Your list name cannot have more than 50 characters!"
                    }              
            },
            //submitHandler: function (form) {
            //    alert('valid');
            //}
        });

});