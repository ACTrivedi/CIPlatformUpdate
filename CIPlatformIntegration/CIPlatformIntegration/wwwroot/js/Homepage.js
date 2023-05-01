/*// A $( document ).ready() block.
$(document).ready(function () {
    var cards = document.getElementsByClassName("card1");
    $('#list-btn').on('click', function () {
        alert();
        document.querySelector(".card-cont").classList.toggle("new");

        for (var i = 0; i < cards.length; i++) {
            cards[i].classList.toggle("old");

        }

        var x = document.getElementsByClassName("col-info");

        for (var i = 0; i < x.length; i++) {
            x[i].classList.toggle("col-lg-12", "col-md-12", "col-sm-12");
            x[i].classList.toggle("col-md-12");
            x[i].classList.toggle("col-sm-12");


        }

        var y = document.getElementsByClassName("cardimage");

        for (var i = 0; i < y.length; i++) {
            y[i].classList.add("cardimagelist");

        }
    });
   *//* document.getElementById("list-btn").onclick = function () {*//*
        //document.getElementById("abc").classList.add("col-3");
        //document.getElementById("xyz").classList.add("col-9");
       

        *//*  document.querySelector(".card1").classList.toggle("old");
              document.querySelector(".col-info").classList.add("col-lg-12");
              document.querySelector(".col-info").classList.add("col-md-12");
              document.querySelector(".col-info").classList.add("col-sm-12");
              document.querySelector(".cardimage").classList.add("cardimagelist");*//*

    *//*}*//*
});




document.getElementById("grid-btn").onclick = function () {

    document.querySelector(".card-cont").classList.remove("new");

    for (var i = 0; i < cards.length; i++) {
        cards[i].classList.remove("old");

    }

    var x = document.getElementsByClassName("col-info");

    for (var i = 0; i < x.length; i++) {
        x[i].classList.remove("col-lg-12", "col-md-12", "col-sm-12");

    }

    var y = document.getElementsByClassName("cardimage");

    for (var i = 0; i < y.length; i++) {
        y[i].classList.remove("cardimagelist");

    }
}



*//*
document.getElementById("grid-btn").onclick = function () {
    document.querySelector(".card-cont").classList.remove("new");
    document.querySelector(".card1").classList.remove("old");
   
        document.querySelector(".col-info").classList.remove("col-lg-12");
       
        document.querySelector(".col-info").classList.remove("col-md-12");
        document.querySelector(".col-info").classList.remove("col-sm-12");

        console.log("ajdbjs");
    }
*//*



var searchedvalue = document.getElementById("searching").value;

if (searchedvalue == "Aakash") {
    console.log(searchedvalue);
    document.getElementById("maindivvv").classList.toggle("maindiv");
}

*/





