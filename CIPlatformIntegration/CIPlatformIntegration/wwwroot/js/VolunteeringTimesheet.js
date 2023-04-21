$('.edit').on('click', function () {
    var value = $(this).attr('id');
    console.log(value);



    $.ajax({
        url: '/StoryListing/VolunteeringTimesheetEdit',
        type: "POST",
        dataType: "json",
        data: {
            selectedModelFromTimesheet: value
        },
        success: function (data) {



            var date = data.date; // Assuming date is a property in the returned JSON data
            var formattedDate = date.split("-").reverse().join("-"); // Converting dd-mm-yyyy to yyyy-mm-dd
            $('#date').val(formattedDate);

            $('#timesheetCheckForTime').val(data.timesheetCheckForTime1);



            var mission = data.missionTitle; // Assuming value is a property in the returned JSON data
            var option = new Option(mission, value, true, true); // Set the "selected" attribute to true
            $('#mission').append(option); // Appending the new option element to the select field


            $('#hours').val(data.hour);
            $('#minute').val(data.minute);
            $('#message').val(data.message);

            alert("success");
        }


    });

});





$('.delete').on('click', function () {

    var value = $(this).attr('id');
    console.log(value);


    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/StoryListing/VolunteeringTimesheetDelete',
                type: "POST",

                data: {
                    selectedModelFromTimesheet: value
                },
                success: function (data) {


                    location.reload();

                }


            });

        }
    })




});




/*For GoalBase Missions*/
$('.editGoal').on('click', function () {
    var valueGoal = $(this).attr('id');
    console.log(valueGoal);



    $.ajax({
        url: '/StoryListing/VolunteeringTimesheetGoalEdit',
        type: "POST",
        dataType: "json",
        data: {
            selectedModelFromTimesheet: valueGoal
        },
        success: function (data) {



            var date = data.date; // Assuming date is a property in the returned JSON data
            var formattedDate = date.split("-").reverse().join("-"); // Converting dd-mm-yyyy to yyyy-mm-dd
            $('#dateGoal').val(formattedDate);

            $('#timesheetCheckForGoal').val(data.timesheetCheckForGoal);



            var mission = data.missionTitle; // Assuming value is a property in the returned JSON data
            var missionId = data.missionId;
            var option = new Option(mission, missionId, true, true); // Set the "selected" attribute to true
            $('#missionGoal').append(option); // Appending the new option element to the select field

            $('#actionGoal').val(data.action);

            $('#messageGoal').val(data.message);

            alert("success");
        }


    });

});





$('.deleteGoal').on('click', function () {


    alert("GoalDelete");

    var valueGoalDelete = $(this).attr('id');
    console.log(valueGoalDelete);


    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/StoryListing/VolunteeringTimesheetGoalDelete',
                type: "POST",

                data: {
                    selectedModelFromTimesheet: valueGoalDelete
                },
                success: function (data) {


                    location.reload();

                }


            });

        }
    })




});
