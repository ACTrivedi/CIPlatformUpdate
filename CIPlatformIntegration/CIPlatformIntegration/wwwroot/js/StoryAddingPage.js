﻿

function getOptionValue() {

    $("#inputState").find("option:selected").each(function (i, v) {
        var missionIdSelected = $(v).val();

        $.ajax({
            url: '/StoryListing/DraftDecide',
            type: "POST",
            dataType: "json",
            data: {
                missionIdSelected: missionIdSelected
            },
            success: function (res) {

                document.getElementById("disabled").disabled = false;


                console.log(res.value.title);
                console.log(res.value.date);
                console.log(res.value.description);
                console.log(res.value.path);


                //For Title
                $('#title').val(res.value.title);

                //For Date
                var shortDate = res.value.date.split(" ")[0];
                var shortDateFormat = shortDate.split("-").reverse().join("-");
                $('#date').val(shortDateFormat);

                //For Description
                tinyMCE.get('default').setContent(res.value.description);



                //For Images
                let images = ""
                for (var i = 0; i < res.value.path.length; i++) {
                    console.log(i);
                    var videoIdPattern = /^(?:https?:\/\/)?(?:www\.)?youtu(?:be\.com|\.be)\/(?:watch\?v=)?([a-zA-Z0-9_-]{11})$/;
                    var match = res.value.path[i].match(videoIdPattern);

                    if (match !== null) {

                        // Do something with the video ID, such as appending it to a URL field

                        $('#videoURL').val(res.value.path[i]);
                    }
                    else {
                        console.log("Images");

                        images += `<div class="image">
                    <img src="${res.value.path[i]}" alt="image">
                    <span onclick="removeimg(${i})">&times;</span>
                    </div>`

                        console.log(res.value.path[i]);

                    }


                }

                $("output").append(images);



            },
            error: function () {

            }
        });
    })
}


Dropzone.options.dropzoneForm =
{
    init: function () {
        this.on("complete", function (data) {
            //var res = eval('(' + data.xhr.responseText + ')');   
            var res = JSON.parse(data.xhr.responseText);
        });
    }
};