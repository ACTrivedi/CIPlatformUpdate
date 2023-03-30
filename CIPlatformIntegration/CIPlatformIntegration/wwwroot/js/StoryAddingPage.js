

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

               


                $('#title').val(draftDetails.title);
               
                tinyMCE.get('default').setContent(draftDetails.description);

                var shortDate = draftDetails.date.split(" ")[0];
                var shortDateFormat = shortDate.split("-").reverse().join("-");
                $('#date').val(shortDateFormat);



                console.log(draftDetails.title);
                console.log(draftDetails.path);
                console.log(draftDetails.description);
                console.log(shortDateFormat)

            },
            error: function () {
                alert("error");
            }
        });
    })
}

