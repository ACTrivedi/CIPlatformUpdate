


var searchTerm = "";
let sortValue = "";


$(document).ready(function () {
    
    LoadMission();
});


$("#search").on("keyup", function (e) {

    searchTerm = $("#search").val().toLowerCase();
    LoadMission();
})



function LoadMission(sortValue) {
    var country = [];
    alert(country);
        
    console.log(country);


    $("#countryList").find("input:checked").each(function (i, obj) {

        country.push($(obj).val());


    })
    var city = [];
    $("#cityList").find("input:checked").each(function (i, obj) {

        city.push($(obj).val());

    })
    var theme = [];
    $("#themeList").find("input:checked").each(function (i, obj) {

        theme.push($(obj).val());

    })  



    $.ajax({
        url: '/Home/GetMissions',
        method: "POST",
        dataType: "html",
        data: { "searchTerm": searchTerm, "sortValue": sortValue, "country": country, "city": city, "theme": theme },
        success: function (data) {
            $("#test").html("");
            $("#test").html(data);
        },
        error: function (error) {
            console.log(error);
        }

    })
}