$(document).ready(function () {
  
   
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




