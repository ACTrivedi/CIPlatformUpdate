$(document).ready(function () {
    

    let table = new DataTable('#userDataTable', {
        lengthChange: false,
       
    });

   

   

    

    $('#userDataTable_filter input').attr("placeholder", "Search");

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


    console.log("Now for edit!");
    $('#countryEdit').on('change', function () {
        alert();
        $.ajax({
            type: 'POST',
            dataType: "JSON",
            url: '/Admin/GetCitiesByCountryId',
            data: {

                Country_id: $('#countryEdit').val()
            },
            success:
                function (res) {
                    console.log(res);
                    $(".city-dropEdit").empty();
                    for (var i = 0; i < res.length; i++) {
                        console.log(res[i]);
                        $('.city-dropEdit').append(`<option value="${res[i].cityId}">${res[i].name}</option>`);
                    }
                    $('.city-dropEdit').removeAttr("disabled");
                    $('.city-alert').hide();
                },
            failure:
                function () {

                }
        });
    });


   
        let missiontable = new DataTable('#missionApplicationDataTable', {
            lengthChange: false,

        });
    



    $('.missionApplicationClick').on('click', function () {
            

       
        $.ajax({
            type: 'POST',
            dataType : 'HTML',
            url: '/Admin/missionApplicationMain',
            
            success:
                function (res) {


                   /* $('.missionApplicationArea').html('');*/
                   
                    $('.missionApplicationArea').html('');
                    $('.missionApplicationArea').append(res);


                   
                        let missiontable = new DataTable('#missionApplicationDataTable', {
                            lengthChange: false,

                        });

                    

                    var missionApplicationApprove = document.querySelectorAll('.missionApplicationApprove');

                    missionApplicationApprove.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            /*$('.missionApplicationApprove').on('click', function () {*/

                                var value = $('.missionApplicationApprove').attr('id');
                                console.log(value);
                                     

                                $.ajax({
                                    url: '/Admin/AdminMissionApplicationApprove',
                                    type: "post",

                                    data: {
                                        missionApplicationId: value
                                    },

                                    dataType: 'HTML',
                                    success: function (res) {

                                        $('.missionApplicationClick').click();

                                    }


                               /* });*/

                            });

                        });

                    });




                    var missionApplicationDelete = document.querySelectorAll('.missionApplicationDelete');

                    missionApplicationDelete.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {
                                                      

                            var value = $('.missionApplicationDelete').attr('id');
                            console.log(value);


                            $.ajax({
                                url: '/Admin/AdminMissionApplicationDelete',
                                type: "post",

                                data: {
                                    missionApplicationId: value
                                },

                                dataType: 'HTML',
                                success: function (res) {

                                    

                                    $('.missionApplicationClick').click();

                                    $('.cancel').click();

                                }
                                                                                          

                            });

                        });

                    });
                                 


                },
            failure:
                function () {

                }
        });


    });


    //For story


    let storytable = new DataTable('#storyDataTable', {
        lengthChange: false,

    });




    $('.storyClick').on('click', function () {

        $.ajax({
            type: 'POST',
            dataType: 'HTML',
            url: '/Admin/storyMain',

            success:
                function (res) {

                    $('.storyArea').html('');
                    $('.storyArea').append(res);

                    let storytable = new DataTable('#storyDataTable', {
                        lengthChange: false,

                    });


                    var viewButton = document.querySelectorAll('.viewButton');

                    viewButton.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            

                            var value = $('.viewButton').val();
                            console.log(value);


                            $.ajax({
                                url: '/Admin/storyDetail',
                                type: "post",

                                data: {
                                    storyId: value
                                },

                                dataType: 'HTML',
                                success: function (res) {
                                    alert("Success");
                                    console.log(res);
                                    $('.storyArea').html('');
                                    $('.storyArea').append(res);

                                }


                                 });

                           

                        });

                    });
                               

                },
            failure:
                function () {

                }
        });


    });


        
});








const days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
const d = new Date();
let day = days[d.getDay()];
let month = months[d.getMonth()];
let date = d.getDate();
let year = d.getFullYear();
let hour = d.getHours();
let min = d.getMinutes();
var am_pm = d.getHours() >= 12 ? "PM" : "AM";

// A $( document ).ready() block.
$(document).ready(function () {

    $("#date").append(day + "," + " " + " " + month + " " + date + ", " + year + ", " + hour + ":" + min + ", " + am_pm);


   
});



$('.userEdit').on('click', function () {
    var value = $(this).attr('id');
    console.log(value);



    $.ajax({
        url: '/Admin/userEditData',
        type: "post",
        dataType: "json",
        data: {
            selectedUserId : value
        },
        success: function (userSearch) {

            console.log(userSearch);

            $('#Ename').val(userSearch[0].firstname);
            $('#Esurname').val(userSearch[0].lastname);
            $('#Eemail').val(userSearch[0].email);
            $('#Epassword').val();
            /*$('#Eavatar').val(userSearch[0].avatar);*/
            $('#Eemployee_id').val(userSearch[0].employee_id);
            $('#Edepartment').val(userSearch[0].department);
            $('#Eprofile_text').val(userSearch[0].profile_text);
            $('#userIdCheckForEdit').val(userSearch[0].selectedUserId);
            $('#Estatus').val(userSearch[0].status);
           
           
        }


    });

});



$('.userDelete').on('click', function () {
    var value = $(this).attr('id');
    console.log(value);



    $.ajax({
        url: '/Admin/adminUserDelete',
        type: "post",
        
        data: {
            selectedUserId: value
        },
        success: function (res) {
            

            /*console.log("succsess");
            var newtable = $($.parseHTML(res)).find('.userTableReload');
            
            $('.userTableReload').html(newtable);*/

            location.reload();
            
           
            
        },
        failure: function (res) {
            alert("Thase thase");
        }


    });

});




function validateForm() {
    // Get the input fields
    var name = document.getElementById("name");
    var surname = document.getElementById("surname");
    var email = document.getElementById("email");
    var password = document.getElementById("password");
    var cityId = document.getElementById("avatar");
    var countryId = document.getElementById("employee_id");
    var department = document.getElementById("department");
    var profileText = document.getElementById("profile_text");


    // Check if the required fields are empty
    if (name.value.trim() == "") {
        $('#Mandatory').html("*Name field can't be empty!");
        name.focus();
        return false;
    }
    if (surname.value.trim() == "") {
        
        $('#Mandatory').html("*Please upload your surname!");
        surname.focus();
        return false;
    }
    if (email.value.trim() == "") {
        
        $('#Mandatory').html("*Please enter your email ID!");
        email.focus();
        return false;
    }
    if (password.value.trim() == "") {
       
        $('#Mandatory').html("*Please enter your password!");
        password.focus();
        return false;
    }
    
    if (countryId.value.trim() == "") {
       
        $('#Mandatory').html("*Please enter your countryId!");
        countryId.focus();
        return false;
    }
    if (department.value.trim() == "") {
        
        $('#Mandatory').html("*Please enter your department!");
        department.focus();
        return false;
    }
    if (profileText.value.trim() == "") {
       
        $('#Mandatory').html("*Please enter your profile text!");
        profileText.focus();
        return false;
    }

    // If all required fields are filled, submit the form
    return true;
}




