/*const { Alert } = require("../lib/bootstrap/dist/js/bootstrap.bundle");*/

// A $( document ).ready() block.
$(document).ready(function () {
    console.log("ready!");
$('#country').on('change', function () {
   
    $.ajax({
        type: 'POST',
        dataType: "JSON",
        url: '/StoryListing/GetCitiesByCountryId',
        data: {

            Country_id: $('#country').val()
        },
        success:
            function (res) {
                console.log(res);
                $(".city-drop").empty();
                for (var i = 0; i < res.length; i++) {
                    console.log(res[i]);
                    $('.city-drop').append(`<option value="${res[i].cityId}">${res[i].name}</option>`);
                }
                $('.city-drop').removeAttr("disabled");
                $('.city-alert').hide();
            },
        failure:
            function () {

            }
    });
});
});




$(function () {

    $('body').on('click', '.list-group .list-group-item', function () {
        $(this).toggleClass('active');
    });
    $('.list-arrows a').click(function () {
        
        var $button = $(this), actives = '';
        
        if ($button.hasClass('move-left')) {
            actives = $('.list-right ul li.active');
            actives.clone().appendTo('.list-left ul');
            actives.remove();  
        } else if ($button.hasClass('move-right')) {
            actives = $('.list-left ul li.active');
            actives.clone().appendTo('.list-right ul');
            actives.remove();
        }
    });
    $('.dual-list .selector').click(function () {
        var $checkBox = $(this);
        if (!$checkBox.hasClass('selected')) {
            $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
            
        } else {
            $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
           
        }
    });
    $('[name="SearchDualList"]').keyup(function (e) {
        var code = e.keyCode || e.which;
        if (code == '9') return;
        if (code == '27') $(this).val(null);
        var $rows = $(this).closest('.dual-list').find('.list-group li');
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    });

});



//For password

function changePassword() {
    

    var oldPassword = $('#oldPassword').val();
    var newPassword = $('#newPassword').val();
    var confirmNewPassword = $('#confirmNewPassword').val();

    $.ajax({
        url: '/StoryListing/UserChangePassword',
        type: 'POST',
        dataType: 'json',
        data: {
            oldPassword: oldPassword, newPassword: newPassword, confirmNewPassword: confirmNewPassword
        },
        success: function (response) {
            

            // Check if the operation succeeded
            if (response.success) {
                // Do something on success
                console.log('Operation succeeded.');

                Swal.fire({
                    icon: 'success',
                    title: 'Password Changed Successfully',
                   

                })

                $("#cancelPrompt").click();
               

            } else {
                // Do something on failure
                console.log('Operation failed: ' + response.message);

                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'INVALID CREDENTIALS!',

                })

            }



        },
        error: function () {

        }
    });

}



function addToTextArea()
{
   
    var dataValues = []; 

    $('.findByList').find('li').each(function () {
        if ($(this).hasClass('active')) {
            var dataValue = $(this).data('value');
            dataValues.push(dataValue);
        }
        
        
    });

    console.log(dataValues);

    var skillString = dataValues.join(", ");
   
    $('#skillTextArea').val(skillString);
    console.log(skillString);
   
   


    
}






//$(document).ready(function () {
  

//    forCity();
    
               

//           // $("#hdnCountryId").val(countryId); //To set the country id

//           // $("#dropdownCountry").text(countryName);



//            $.ajax({
//                type: "POST",
//                dataType : "JSON",
//                url: '/StoryListing/GetCitiesByCountryId',
//                data: { countryId: countryId },
//                success: function (cities) {
//                    console.log(cities);                                      
                    
//                    var citiesSelect = $("#ddlCity");
//                    citiesSelect.html('');
//                    citiesSelect.append($('<option></option>').val('').html('Select your city'));
                   
                    
//                    $.each(cities, function (i, city) {
//                        console.log(city)
//                        citiesSelect.append($('<option></option>').val(city.cityId).html(city.name));
//                    });
                   
//                }
//            });
       


   

//});

//function forCity() {

//    $("#ddlCity").change(function () {
        
//        var cityId = $(this).val();

//        $("#hdnCityId").val(cityId); //To set the city id

//        var cityName = $(this).find("option:selected").text();
//        console.log("Selected city ID: " + cityId);
//        console.log("Selected city name: " + cityName);
//    });
       
//}





