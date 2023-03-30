

function getOptionValue() {

   


    $("#inputState").find("option:selected").each(function (i, v) {
        var missionIdSelected= $(v).val();
    
       
        $.ajax({
            url: '/StoryListing/StoryByDraft',
            type: "GET",
            data: {
                missionIdSelected: missionIdSelected
            },
            success: function (draftDetails) {

                alert("success");

                console.log(draftDetails.title);
                console.log(draftDetails.path);
                console.log(draftDetails.description);

            },
            error: function () {
                alert("error");
            }
        });
    })
}

