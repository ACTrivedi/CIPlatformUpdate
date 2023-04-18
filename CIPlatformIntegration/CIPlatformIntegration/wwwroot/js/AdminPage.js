$(document).ready(function () {
    

    let table = new DataTable('#userDataTable', {
        lengthChange: false,
       
    });

    $('#userDataTable_filter input').attr("placeholder", "Search");

    //var searchText = $('#userDataTable_filter label').text();
    //var labelText = searchText.replace("Search: ", "");

    
    //$('#userDataTable_filter label').text(labelText);
    
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
console.log(hour);
var am_pm = d.getHours() >= 12 ? "PM" : "AM";

// A $( document ).ready() block.
$(document).ready(function () {

    $("#date").append(day + "," + " " + " " + month + " " + date + ", " + year + ", " + hour + ":" + min + ", " + am_pm);


   
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




