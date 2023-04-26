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

                    //For Delete,Decline and approve from outside story start
                   

                    //for outer Delete
                    var deleteStory = document.querySelectorAll('.deleteStory');

                    deleteStory.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            var value = $(this).attr('value');
                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                data:
                                {
                                    storyId: value
                                },
                                url: '/Admin/storyDelete',

                                success:
                                    function (res) {
                                        $('.storyArea').html('');
                                        $('.storyArea').append(res);

                                        let storytable = new DataTable('#storyDataTable', {
                                            lengthChange: false,

                                        });

                                        $('.storyClick').click();

                                    },
                                failure:
                                    function () {

                                    }
                            });

                        });

                    });

                    //for outer Approve
                    var approveStory = document.querySelectorAll('.approveStory');

                    approveStory.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            

                                var value = $(this).attr('value');



                                $.ajax({
                                    type: 'POST',
                                    dataType: 'HTML',
                                    data:
                                    {
                                        storyId: value
                                    },
                                    url: '/Admin/storyApprove',

                                    success:
                                        function (res) {
                                            $('.storyArea').html('');
                                            $('.storyArea').append(res);

                                            let storytable = new DataTable('#storyDataTable', {
                                                lengthChange: false,

                                            });

                                            $('.storyClick').click();

                                        },
                                    failure:
                                        function () {

                                        }
                                });




                           

                        });

                    });

                    //for outer Decline
                    var declineStory = document.querySelectorAll('.declineStory');

                    declineStory.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            

                                var value = $(this).attr('value');
                                $.ajax({
                                    type: 'POST',
                                    dataType: 'HTML',
                                    data:
                                    {
                                        storyId: value
                                    },
                                    url: '/Admin/storyDecline',

                                    success:
                                        function (res) {
                                            $('.storyArea').html('');
                                            $('.storyArea').append(res);

                                            let storytable = new DataTable('#storyDataTable', {
                                                lengthChange: false,

                                            });
                                            $('.storyClick').click();

                                        },
                                    failure:
                                        function () {

                                        }
                                });


                            






                        });

                    });


                    

                    //For Delete,Decline and approve from outside story ends





                    var viewButton = document.querySelectorAll('.viewButton');

                    viewButton.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            

                            var value = $(this).attr('value');
                            console.log(value);


                            $.ajax({
                                url: '/Admin/storyDetail',
                                type: "post",

                                data: {
                                    storyId: value
                                },

                                dataType: 'HTML',
                                success: function (res) {
                                   
                                    console.log(res);
                                    $('.storyArea').html('');
                                    $('.storyArea').append(res);

                                    //For Back
                                    $('.back').on('click', function () {                                       
                                        $('.storyClick').click();
                                    });


                                    //For Story Delete
                                    $('.deleteStory').on('click', function () {

                                        var value = $(this).attr('value');
                                        $.ajax({
                                            type: 'POST',
                                            dataType: 'HTML',
                                            data:
                                            {
                                                storyId: value
                                            },
                                            url: '/Admin/storyDelete',

                                            success:
                                                function (res) {
                                                    $('.storyArea').html('');
                                                    $('.storyArea').append(res);

                                                    let storytable = new DataTable('#storyDataTable', {
                                                        lengthChange: false,

                                                    });

                                                    $('.storyClick').click();

                                                },
                                            failure:
                                                function () {

                                                }
                                        });


                                    });


                                    //For Decline
                                    $('.declineStory').on('click', function () {

                                        var value = $(this).attr('value');
                                        $.ajax({
                                            type: 'POST',
                                            dataType: 'HTML',
                                            data:
                                            {
                                                storyId: value
                                            },
                                            url: '/Admin/storyDecline',

                                            success:
                                                function (res) {
                                                    $('.storyArea').html('');
                                                    $('.storyArea').append(res);

                                                    let storytable = new DataTable('#storyDataTable', {
                                                        lengthChange: false,

                                                    });
                                                    $('.storyClick').click();

                                                },
                                            failure:
                                                function () {

                                                }
                                        });


                                    });


                                    //For Approve
                                    $('.approveStory').on('click', function () {

                                        var value = $(this).attr('value');

                                       

                                            $.ajax({
                                                type: 'POST',
                                                dataType: 'HTML',
                                                data:
                                                {
                                                    storyId: value
                                                },
                                                url: '/Admin/storyApprove',

                                                success:
                                                    function (res) {
                                                        $('.storyArea').html('');
                                                        $('.storyArea').append(res);

                                                        let storytable = new DataTable('#storyDataTable', {
                                                            lengthChange: false,

                                                        });

                                                        $('.storyClick').click();

                                                    },
                                                failure:
                                                    function () {

                                                    }
                                            });
                                       



                                    });






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





    //For Mission Theme


    let missionsThemeDataTable = new DataTable('#missionsThemeDataTable', {
        lengthChange: false,
        pageLength: 7,

    });




       
    $('.missionThemeClick').on('click', function () {
                    
              

        $.ajax({
            type: 'POST',
            dataType: 'HTML',
            url: '/Admin/missionThemeMain',

            success:
                function (res) {

                    $('.missionThemeArea').html('');
                    $('.missionThemeArea').append(res);

                    let missionsThemeDataTable = new DataTable('#missionsThemeDataTable', {
                        lengthChange: false,
                        pageLength: 7,
                    });


                    //For theme Edit start
                    var value
                    $('.themeEditButton').on('click', function () {

                        value = $(this).attr('value');

                        $.ajax({
                            type: 'POST',
                            dataType: 'JSON',
                            data:
                            {
                                missionThemeId: value
                            },
                            url: '/Admin/missionThemeEditButton',

                            success:
                                function(res) {
                                
                                console.log("Heyy" + res);
                                $('#editTitle').val(res);

                            },
                            failure:
                                function () {

                                }
                        });




                    });
                    //For theme Edit end

                    //For Edit mission Theme


                    $('#editMissionTheme').on('click', function () {

                        var missionThemeId = value;
                        var editTitle = $('#editTitle').val();

                        console.log(editTitle);

                        $.ajax({
                            type: 'POST',
                            dataType: 'HTML',
                            url: '/Admin/AdminEditMissionTheme',

                            data: {

                                missionThemeId: missionThemeId,
                                editTitle: editTitle,
                            },


                            success:
                                function (res) {

                                    $('.missionThemeArea').html('');
                                    $('.missionThemeArea').append(res);

                                    let missionsThemeDataTable = new DataTable('#missionsThemeDataTable', {
                                        lengthChange: false,
                                        pageLength: 7,
                                    });

                                    location.reload();
                                   
                                    


                                    $('.missionThemeClick').click();
                                    



                                },
                            failure:
                                function () {

                                }
                        });


                    });




                    //For theme approve start
                    var approveMissionTheme = document.querySelectorAll('.approveMissionTheme');

                    approveMissionTheme.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            var value = $(this).attr('value');

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                data:
                                {
                                    missionThemeId: value
                                },
                                url: '/Admin/missionThemeApprove',

                                success:
                                    function (res) {
                                        $('.missionThemeArea').html('');
                                        $('.missionThemeArea').append(res);

                                        let missionsThemeDataTable = new DataTable('#missionsThemeDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,
                                        });

                                        missionsThemeDataTable.ajax.reload();

                                        $('.missionThemeClick').click();

                                    },
                                failure:
                                    function () {

                                    }
                            });






                        });

                    });
                    //For theme approve end

                    //For theme delete start
                    var deleteMissionTheme = document.querySelectorAll('.deleteMissionTheme');

                    deleteMissionTheme.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            var value = $(this).attr('value');

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                data:
                                {
                                    missionThemeId: value
                                },
                                url: '/Admin/missionThemeDelete',

                                success:
                                    function (res) {
                                        $('.missionThemeArea').html('');
                                        $('.missionThemeArea').append(res);

                                        let missionsThemeDataTable = new DataTable('#missionsThemeDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,
                                        });

                                        $('.missionThemeClick').click();

                                    },
                                failure:
                                    function () {

                                    }
                            });






                        });

                    });
                    //For theme delete end

                    //For theme add start

                    $('#addMissionTheme').on('click', function () {
                        $('.btn-close').click();
                        var Title = $('#Title').val();
                        console.log(Title);                                              
                       
                        if (Title.trim() == "") {
                            $('#titleSpan').removeClass("d-none");
                            
                        }
                        else {
                                                                            
                        $.ajax({
                            type: 'POST',
                            dataType: 'HTML',
                            url: '/Admin/AdminAddMissionTheme',

                            data: {
                                Title: Title,
                            },


                            success:
                                function (res) {

                                    

                                    $('.missionThemeArea').html('');
                                    $('.missionThemeArea').append(res);

                                    let missionsThemeDataTable = new DataTable('#missionsThemeDataTable', {
                                        lengthChange: false,
                                        pageLength: 7,
                                    });

                                   

                                    $('.missionThemeClick').click();



                                },
                            failure:
                                function () {

                                }
                        });

                        }
                    });

                    //For theme add end
                        
                   
                   


                },
            failure:
                function () {

                }
        });


    });






    //For Mission Skills


    let missionSkillsDataTable = new DataTable('#missionSkillsDataTable', {
        lengthChange: false,
        pageLength: 7,

    });





    $('.missionSkillsClick').on('click', function () {



        $.ajax({
            type: 'POST',
            dataType: 'HTML',
            url: '/Admin/missionSkillsMain',

            success:
                function (res) {

                    $('.missionSkillsArea').html('');
                    $('.missionSkillsArea').append(res);

                    let missionSkillsDataTable = new DataTable('#missionSkillsDataTable', {
                        lengthChange: false,
                        pageLength: 7,

                    });                   



                    //For skill delete start
                    var deleteMissionSkills = document.querySelectorAll('.deleteMissionSkills');

                    deleteMissionSkills.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            var value = $(this).attr('value');

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                data:
                                {
                                    missionSkillsId: value
                                },
                                url: '/Admin/missionSkillsDelete',

                                success:
                                    function (res) {
                                        $('.missionSkillArea').html('');
                                        $('.missionSkillArea').append(res);

                                        let missionSkillsDataTable = new DataTable('#missionSkillsDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,

                                        });

                                        $('.missionSkillsClick').click();

                                    },
                                failure:
                                    function () {

                                    }
                            });






                        });

                    });
                    //For skill delete end


                    // For skill Add
                    $('#addMissionSkills').on('click', function () {

                        $('.cancelPrompt').click();
                        var skillName = $('#skillName').val();
                        console.log(skillName);

                        if (skillName.trim() == "") {
                            $('#skillSpan').removeClass("d-none");

                        }
                        else {

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                url: '/Admin/AdminAddMissionSkills',

                                data: {
                                    skillName: skillName,
                                },


                                success:
                                    function (res) {



                                        $('.missionSkillsArea').html('');
                                        $('.missionSkillsArea').append(res);

                                        let missionSkillsDataTable = new DataTable('#missionSkillsDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,

                                        });



                                        $('.missionSkillsClick').click();



                                    },
                                failure:
                                    function () {

                                    }
                            });

                        }
                    });


                    //For skill Add end

                    //For Mission Skill Edit start
                    var value = 0;
                    $('.skillsEditButton').on('click', function () {

                         value = $(this).attr('value');

                        $.ajax({
                            type: 'POST',
                            dataType: 'JSON',
                            data:
                            {
                                missionSkillId: value
                            },
                            url: '/Admin/missionSkillEditButton',

                            success:
                                function (res) {

                                    console.log("Heyy" + res);
                                    $('#editSkillName').val(res);

                                },
                            failure:
                                function () {

                                }
                        });




                    });
                    //For Mission Skill Edit end


                    //For Edit skill 


                    $('#editMissionSkills').on('click', function () {
                        $('.cancelPrompt').click();

                        var missionSkillsId = value;
                        var editSkillName = $('#editSkillName').val();
                        var SelectStatus = $('#SelectStatus').val();


                        $.ajax({
                            type: 'POST',
                            dataType: 'HTML',
                            url: '/Admin/AdminEditMissionSkills',

                            data: {

                                missionSkillsId: missionSkillsId,
                                editSkillName: editSkillName,
                                SelectStatus: SelectStatus,
                            },


                            success:
                                function (res) {

                                    $('.missionSkillsArea').html('');
                                    $('.missionSkillsArea').append(res);

                                    let missionSkillsDataTable = new DataTable('#missionSkillsDataTable', {
                                        lengthChange: false,
                                        pageLength: 7,

                                    });

                                    


                                    $('.missionSkillsClick').click();




                                },
                            failure:
                                function () {

                                }
                        });


                    });




                },
            failure:
                function () {

                }
        });


    });




    //For Mission CMS


    let CMSDataTable = new DataTable('#CMSDataTable', {
        lengthChange: false,
        pageLength: 7,

    });





    $('.CMSClick').on('click', function () {



        $.ajax({
            type: 'POST',
            dataType: 'HTML',
            url: '/Admin/CMSMain',

            success:
                function (res) {

                    $('.CMSArea').html('');
                    $('.CMSArea').append(res);


                    let CMSDataTable = new DataTable('#CMSDataTable', {
                        lengthChange: false,
                        pageLength: 7,

                    });






                    //For CMS approve start
                    var approveCMS = document.querySelectorAll('.approveCMS');

                    approveCMS.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            var value = $(this).attr('value');

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                data:
                                {
                                    CMSId: value
                                },
                                url: '/Admin/CMSApprove',

                                success:
                                    function (res) {
                                        $('.CMSArea').html('');
                                        $('.CMSArea').append(res);

                                        let CMSDataTable = new DataTable('#CMSDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,

                                        });

                                        $('.CMSClick').click();

                                    },
                                failure:
                                    function () {

                                    }
                            });






                        });

                    });
                    //For CMS approve end

                    //For CMS delete start
                    var deleteCMS = document.querySelectorAll('.deleteCMS');

                    deleteCMS.forEach(function (i) {

                        console.log(i);

                        i.addEventListener('click', function () {

                            var value = $(this).attr('value');

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                data:
                                {
                                    CMSId: value
                                },
                                url: '/Admin/CMSDelete',

                                success:
                                    function (res) {
                                        $('.CMSArea').html('');
                                        $('.CMSArea').append(res);

                                        let CMSDataTable = new DataTable('#CMSDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,

                                        });

                                        $('.CMSClick').click();

                                    },
                                failure:
                                    function () {

                                    }
                            });






                        });

                    });
                    //For CMS delete end


                    // For CMS Add
                    $('#addCMS').on('click', function () {

                        $('.cancelPrompt').click();
                        var CMSTitle = $('#CMSTitle').val();
                        var CMSDescription = $('#CMSDescription').val();
                        var CMSSlug = $('#CMSSlug').val();
                        var SelectStatus = $('#SelectStatus').val();
                        
                        console.log(CMSTitle);

                        if (CMSTitle.trim() == "") {
                            $('#skillSpan').removeClass("d-none");

                        }
                        else {

                            $.ajax({
                                type: 'POST',
                                dataType: 'HTML',
                                url: '/Admin/AdminAddCMS',

                                data: {
                                    CMSTitle: CMSTitle,
                                    CMSDescription: CMSDescription,
                                    CMSSlug: CMSSlug,
                                    SelectStatus: SelectStatus,

                                },


                                success:
                                    function (res) {



                                        $('.CMSArea').html('');
                                        $('.CMSArea').append(res);

                                        let CMSDataTable = new DataTable('#CMSDataTable', {
                                            lengthChange: false,
                                            pageLength: 7,

                                        });



                                        $('.CMSClick').click();



                                    },
                                failure:
                                    function () {

                                    }
                            });

                        }
                    });


                    //For CMS Add end

                    //For CMS Edit start
                    var value = 0;
                    $('.CMSEditButton').on('click', function () {

                        value = $(this).attr('value');

                        $.ajax({
                            type: 'POST',
                            dataType: 'JSON',
                            data:
                            {
                                CMSId : value
                            },
                            url: '/Admin/CMSEditButton',

                            success:
                                function (res) {
                                    console.log(res);
                                    
                                    $('#CMSTitleEdit').val(res.cmsTitle);
                                    $('#CMSDescriptionEdit').val(res.cmsDescription);
                                    $('#CMSSlugEdit').val(res.cmsSlug);
                                    $('#SelectStatusEdit').val(res.selectStatus);

                                    
                                    
                                    

                                },
                            failure:
                                function () {

                                }
                        });




                    });
                    //ForCMS Edit end


                    //For Edit CMS 


                    $('#editCMS').on('click', function () {
                        $('.cancelPrompt').click();

                        var CMSId = value;
                        var CMSTitleEdit =  $('#CMSTitleEdit').val();
                        var CMSDescriptionEdit =  $('#CMSDescriptionEdit').val();
                        var CMSSlugEdit =  $('#CMSSlugEdit').val();
                        var SelectStatusEdit =  $('#SelectStatusEdit').val();
                      

                        $.ajax({
                            type: 'POST',
                            dataType: 'HTML',
                            url: '/Admin/AdminEditCMS',

                            data: {

                                CMSId: CMSId,
                                CMSTitleEdit: CMSTitleEdit,
                                CMSDescriptionEdit: CMSDescriptionEdit,
                                CMSSlugEdit: CMSSlugEdit,
                                SelectStatusEdit: SelectStatusEdit,
                               
                            },


                            success:
                                function (res) {

                                    $('.CMSArea').html('');
                                    $('.CMSArea').append(res);

                                    let CMSDataTable = new DataTable('#CMSDataTable', {
                                        lengthChange: false,
                                        pageLength: 7,

                                    });

                                    $('.CMSClick').click();




                                },
                            failure:
                                function () {

                                }
                        });


                    });




                },
            failure:
                function () {

                }
        });








    });









    //For Mission CMS


    let missionDataTable = new DataTable('#missionDataTable', {
        lengthChange: false,
        pageLength: 7,

    });





    $('.missionClick').on('click', function () {



        $.ajax({
            type: 'POST',
            dataType: 'HTML',
            url: '/Admin/missionMain',

            success:
                function (res) {

                    $('.missionArea').html('');
                    $('.missionArea').append(res);


                    let missionDataTable = new DataTable('#missionDataTable', {
                        lengthChange: false,
                        pageLength: 7,

                    });

                    //Mission Add Start
                    $('.missionViewButton').on('click', function () {
                        $.ajax({
                            type: 'POST',
                            dataType: 'HTML',
                           
                            url: '/Admin/addMission',

                            success:
                                function (res) {

                                    $('.missionArea').html('');
                                    $('.missionArea').append(res);

                                    let missionDataTable = new DataTable('#missionDataTable', {
                                        lengthChange: false,
                                        pageLength: 7,

                                    });

                                    /*$('.missionClick').click();*/

                                },
                            failure:
                                function () {

                                }
                        });

                    });
                    // Mission Add End


                    //Mission Edit Start                 
                    var missionId = $('#editMissionButton').val();
                    $('.approveMission').on('click', function () {
                        $.ajax({
                            type: 'POST',
                            dataType: 'HTML',
                            data:
                            {
                                missionId: missionId,
                            },
                            url: '/Admin/editMission',

                            success:
                                function (res) {

                                    $('.missionArea').html('');
                                    $('.missionArea').append(res);

                                    let missionDataTable = new DataTable('#missionDataTable', {
                                        lengthChange: false,
                                        pageLength: 7,

                                    });

                                    /*$('.missionClick').click();*/

                                },
                            failure:
                                function () {

                                }
                        });

                    });
                    //Mission Edit End
                    







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




