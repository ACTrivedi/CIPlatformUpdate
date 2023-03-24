alert("Javascript is loaded")


$("#sharestorybtn").on('click', function () {
    Swal.fire({
        title: 'Are you sure?',
        text: "You want to post the story!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
           

            var title = document.getElementById("title").value;
            var date = document.getElementById("date").value;
            var storydescription = tinyMCE.get("default").getContent();
            var videoURL = document.getElementById("videoURL").value;

            var selectedFromDropdown = $('.form-select').find(":selected").val();

            console.log(selectedFromDropdown)

            $.ajax({
                url: '/StoryListing/StoryAddingPageCall',
                type: 'POST',
                data: { title: title, date: date, storydescription: storydescription, videoURL: videoURL, selectedFromDropdown: selectedFromDropdown },
                success: function () {
                 alert("success")
                },
                error: function (error) {
                    
                }

            });


            
        }
    })
    
});

/*
$("#sharestorybtn").on('click', function () {
         alert("button is clicked");
        var title=document.getElementById("title").value;
        var date=document.getElementById("date").value;
        var storydescription=tinyMCE.get("default").getContent();
    var videoURL = document.getElementById("videoURL").value;

         var selectedFromDropdown=$('.form-select').find(":selected").val();

         console.log(selectedFromDropdown)

            $.ajax({
                url: '/StoryListing/StoryAddingPageCall',
               type: 'POST',
                data: { title :title,date:date,storydescription:storydescription,videoURL:videoURL,selectedFromDropdown:selectedFromDropdown },
                success: function (data) {
                   
                },
                error: function (error) {

                }

            });
    });*/