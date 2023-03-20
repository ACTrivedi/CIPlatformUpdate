$(document).ready(function () {


    /* 1. Visualizing things on Hover - See next part for action on click */
    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently mouse on

        // Now highlight all the stars that's not after the current hovered star
        $(this).parent().children('li.star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });

    }).on('mouseout', function () {
        $(this).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    });


    /* 2. Action to perform on click */
    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently selected
        var stars = $(this).parent().children('li.star');

        for (i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

        // JUST RESPONSE (Not needed)
        var ratingValue = parseInt($('#stars li.selected').last().data('value'), 10);
        var msg = "";
        if (ratingValue > 1) {
            msg = "Thanks! You rated this " + ratingValue + " stars.";
        }
        else {
            msg = "We will improve ourselves. You rated this " + ratingValue + " stars.";
        }
        responseMessage(msg);

    });















   

   
    GetSkills();
    GetCountry();
    $('#Country').change(function () {
        var id = $(this).val();
        
        $('#City').empty();
        $('#City').append('<Option>Select City</Option>')
        $.ajax({
            type: 'POST',
            url: '/Home/City?id=' + id,

            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                
                $.each(result, function (i, data) {
                    $('#City').append(' <input type="checkbox" value="' +  data.name + '"><label for="item1">' + data.name +'</label><br>');
                });
             
            },
            error: function (ex) {
                var r = jQuery.parseJSON(response.responseText);
                alert("Message: " + r.Message);
                alert("StackTrace: " + r.StackTrace);
                alert("ExceptionType: " + r.ExceptionType);
            }
        });
    });
    GetTheme();
  
   
});


function GetCountry()
    {
  
    $.ajax({
        type: 'GET',
        url: '/Home/Country',
        dataType: 'json',
        contentType: "application/json; charset=utf-8", 
        success: function (data) {
            console.log(data);
            $.each(data, function (i, data) {
                $('#Country').append(' <Option value= ' + data.countryId + '>' + data.name + '</Option>');
            });
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });
}


function GetSkills() {

    $.ajax({
        type: 'GET',
        url: '/Home/Skill',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            $.each(data, function (i, data) {
                $('#Skill').append(' <Option value= ' + data.skillId + '>' + data.skillName + '</Option>');
            });
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });
}




function GetTheme() {

    $.ajax({
        type: 'GET',
        url: '/Home/MissionTheme',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            $.each(data, function (i, data) {
                $('#theme').append('<input type="checkbox" id="item1"  value="'+ data.missionThemeId +'"><label for="item1">'+ data.title+'</label><br>');
            });
        },
        error: function (ex) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
        }
    });
}


