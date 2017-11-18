
//Working
function getSessions() {


    jQuery.ajax({

        url: "/Paper/GetSessions",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            var $el = $("#sessionSelectOptions");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#sessionSelectOptions option:first").attr('selected', 'selected');
            $("#sessionSelectOptions option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SessionId));
            });



            $('#sessionSelectOptions').selectpicker('refresh');

        }

    });



}
//
var _sessionId = "";
function getSemesters() {

    var url = "/Paper/GetSemesters";
    jQuery.ajax({

        url: url,

        type: "GET",

        data: { sessionId: _sessionId },

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {


            var $el = $("#semesterSelectOption");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#semesterSelectOption option:first").attr('selected', 'selected');
            $("#semesterSelectOption option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SemesterNo));
            });

            $('#semesterSelectOption').selectpicker('refresh');


        },
        failure: function (result) {
            console.log(result.responseText);
        },
        error: function (result) {
            console.log(result.responseText);
        }



       


    });
    getSection();

}
//
function getSemestersById(id) {
    _sessionId = id;
    getSemesters();
}
//
function getSection() {

    
    jQuery.ajax({

        url: "/Paper/GetSection",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterSelectOption").val() },
        success: function (result) {

            var $el = $("#sectionSelectOptions");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SectionName));
            });

            //  $('#sectionSelectOptions').trigger("chosen:updated");
            $('#sectionSelectOptions').selectpicker('refresh');


        }

    });


    getCourse();
}
//
function getCourse() {

    jQuery.ajax({

        url: "/Paper/GetCourse",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterSelectOption").val() },
        success: function (result) {

            var $el = $("#subjectSelectOptions");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#subjectSelectOptions option:first").attr('selected', 'selected');
            $("#subjectSelectOptions option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.CourseName));
            });

            $('#subjectSelectOptions').selectpicker('refresh');



        }

    });





}
//
function getTeachers() {

    jQuery.ajax({

        url: "/Paper/GetTeacher",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterSelectOption").val(), courseId: $("#subjectSelectOptions").val() },
        success: function (result) {

            var $el = $("#teacherSelectOptions");
            $el.empty(); // remove old options

            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.FirstName + " " + value.LastName));
            });

            $('#teacherSelectOptions').selectpicker('refresh');


        }

    });


}
function validationSetPaper() {
    $('#questionNo').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });
    $('#paperName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9 ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only aplhabets and letters.'); return ''; }));
    });

    $('#timeAllowed').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });

    $('#password').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only alphabets and numbers.'); return ''; }));
    });

    $('#totalMarks').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only alphabets and numbers.'); return ''; }));
    });

}
function validateCreatePaper()
{ 
    $('#marks').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers'); return ''; }));
    });
}
function CheckEmptinessOfCreatePaper()
{

    var quesionText = $("#quesionText").val();

    if (jQuery.trim(quesionText).length == 0) {
        return true;
    }
    var marks = $("#marks").val();

    if (jQuery.trim(marks).length == 0) {
        return true;
    }
    var resources = $("#resources").val();

    if (jQuery.trim(resources).length == 0) {
        return true;
    }
    var correctOptionOne = $("#correctOptionOne").val();

    if (jQuery.trim(correctOptionOne).length == 0) {
        return true;
    }

    var correctOptionTwo = $("#correctOptionTwo").val();

    if (jQuery.trim(correctOptionTwo).length == 0) {
        return true;
    }

    var correctOptionThree = $("#correctOptionThree").val();

    if (jQuery.trim(correctOptionThree).length == 0) {
        return true;
    }

    var correctOptionFour = $("#correctOptionFour").val();

    if (jQuery.trim(correctOptionFour).length == 0) {
        return true;
    }


}
function CheckEmptinessInsert() {
    var sessionSelectOptions = $("#sessionSelectOptions").val();

    if (jQuery.trim(sessionSelectOptions).length == 0) {
        return true;
    }
    var semesterSelectOption = $("#semesterSelectOption").val();

    if (jQuery.trim(semesterSelectOption).length == 0) {
        return true;
    }
    var sectionSelectOptions = $("#sectionSelectOptions").val();

    if (jQuery.trim(sectionSelectOptions).length == 0) {
        return true;
    }
    var subjectSelectOptions = $("#subjectSelectOptions").val();

    if (jQuery.trim(subjectSelectOptions).length == 0) {
        return true;
    }

    var teacherSelectOptions = $("#teacherSelectOptions").val();

    if (jQuery.trim(teacherSelectOptions).length == 0) {
        return true;
    }

    var questionNo = $("#questionNo").val();

    if (jQuery.trim(questionNo).length == 0) {
        return true;
    }

    var paperName = $("#paperName").val();

    if (jQuery.trim(paperName).length == 0) {
        return true;
    }


    var timeAllowed = $("#timeAllowed").val();

    if (jQuery.trim(timeAllowed).length == 0) {
        return true;
    }

    var password = $("#password").val();

    if (jQuery.trim(password).length == 0) {
        return true;
    }

    var totalMarks = $("#totalMarks").val();

    if (jQuery.trim(totalMarks).length == 0) {
        return true;
    }
}
function UploadImage() {
    debugger;
    var data = new FormData();
    var files = $('[type="file"]').get(0).files;
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        alert("File Found");
        data.append("file", files[0]);
    }

    $.ajax({
        type: "POST",
        dataType: 'json',
        url: '/Paper/UploadImage',
        timeout: 2000,
        data: data,
        contentType: 'application/json',
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {

            //show content
            alert('Success!')
        }
    });
}
//
function passDataToAction() {
    var isEmpty = CheckEmptinessInsert();
    if (isEmpty) {
        alert("Some Fields are empty");
        return;
    }
    var selectedSections = "";
    $("#sectionSelectOptions :selected").each(function () {
        selectedSections = selectedSections + $(this).val() + " ";
    });
    jQuery.ajax({
        url: "/Paper/CreatePaper",
        type: "Get",
        data:{
            "sessionId": $("#sessionSelectOptions").val(), "semesterId": $("#semesterSelectOption").val(), "sectionNames": selectedSections, "courseId": $("#subjectSelectOptions").val(),
            "teacherId": $("#teacherSelectOptions").val(), "noOfQuestions": $("#questionNo").val(), "paperName": $("#paperName").val(),
            "timeAllowed": $("#timeAllowed").val(), "active": $("#active").val(), "password": $("#password").val(), "totalMarks": $("#totalMarks").val()
        },
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        cache: false,
        success: function (result) {
            window.history.pushState("", "Create Paper", "/Paper/CreatePaper");
            $("html").html(result);
        }
    });
}
var nextQuestionNo = "";
function addQuestion() {

    var isEmpty = CheckEmptinessOfCreatePaper();
    if (isEmpty) {


        alert("Some fields are empty");
        return;

    }
    var questionLimit = $("#maxQuestions").text();
    var optionOne = {
        Text: $("#optionOne").val(),
        Correct: $('#correctOptionOne').is(':checked')
    }
    var optionTwo = {
        Text: $("#optionTwo").val(),
        Correct: $('#correctOptionTwo').is(':checked')
    }
    var optionThree = {
        Text: $("#optionThree").val(),
        Correct: $('#correctOptionThree').is(':checked')
    }
    var optionFour = {
        Text: $("#optionFour").val(),
        Correct: $('#correctOptionFour').is(':checked')
    }
    var QuestionObject = {

        Text: $("#quesionText").val(),
        OptionOne: optionOne,
        OptionTwo: optionTwo,
        OptionThree: optionThree,
        OptionFour: optionFour,
        Resources: $("#resources").val(),
        Marks: $("#marks").val()
    }
    $.ajax({
        url: "/Paper/AddQuestion",
        type: "POST",
        data: JSON.stringify(QuestionObject),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            alert("Question Added");
            var qutestionLimitReached = parseInt(result);
            nextQuestionNo = qutestionLimitReached;
            qutestionLimitReached = qutestionLimitReached - 1;
           if (qutestionLimitReached == questionLimit) {
                $("#submitBtn").css("visibility", "visible");

                $("#addBtn").css("visibility", "hidden");
                $("#saveBtn").css("visibility", "hidden");
            }
            else if (result > '0') {
                $("#addBtn").css("visibility", "visible");
                $("#previousBtn").css("visibility", "visible");
                $("#nextBtn").css("visibility", "visible");
                $("#saveBtn").css("visibility", "hidden");

            }
        },
        error: function (result) {


            alert("Error " + result);
        }


    });
}
function addNext() {
    $("#questionNo").text("Question No." + nextQuestionNo);
    clearFields();
    $("#addBtn").css("visibility", "hidden");
    $("#saveBtn").css("visibility", "visible");

}
function clearFields() {
    $("#optionOne").val(""),
    $('#correctOptionOne').prop('checked', true);
    $("#optionTwo").val(""),
    $('#correctOptionTwo').prop('checked', false);
    $("#optionThree").val(""),
    $('#correctOptionThree').prop('checked', false);
    $("#optionFour").val(""),
    $('#correctOptionFour').prop('checked', false);
    $("#resources").val(""),
    $("#marks").val(""),
    $("#quesionText").val(""),
    $("#file").val("")
}
function next() {


    jQuery.ajax({

        url: "/Paper/Next",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            $("#questionNo").text("Question No." + (result.Index + 1));
            $("#quesionText").val(result.Text);
            $("#marks").val(result.Marks);
            $("#resources").val(result.Resources);
            $("#optionOne").val(result.OptionOne.Text);
            $("#optionTwo").val(result.OptionTwo.Text);
            $("#optionThree").val(result.OptionThree.Text);
            $("#optionFour").val(result.OptionFour.Text);
            if (result.OptionOne.Correct == 1) {
                $("#correctOptionOne").prop("checked", true);

            }
            else if (result.OptionTwo.Correct == 1) {

                $("#correctOptionTwo").prop("checked", true);

            }
            else if (result.OptionThree.Correct == 1) {


                $("#correctOptionThree").prop("checked", true);

            }
            else if (result.OptionFour.Correct == 1) {


                $("#correctOptionFour").prop("checked", true);

            }
            $("#addBtn").css("visibility", "Visible");

        },
        error: function (result) {

            alert("Error");
        }

    });



}
function previous() {


    jQuery.ajax({

        url: "/Paper/Previous",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        success: function (result) {

            $("#questionNo").text("Question No." + (result.Index + 1));
            $("#quesionText").val(result.Text);
            $("#marks").val(result.Marks);
            $("#resources").val(result.Resources);
            $("#optionOne").val(result.OptionOne.Text);
            $("#optionTwo").val(result.OptionTwo.Text);
            $("#optionThree").val(result.OptionThree.Text);
            $("#optionFour").val(result.OptionFour.Text);

            if (result.OptionOne.Correct == 1) {

                $("#correctOptionOne").prop("checked", true);

            }
            else if (result.OptionTwo.Correct == 1) {

                $("#correctOptionTwo").prop("checked", true);

            }
            else if (result.OptionThree.Correct == 1) {


                $("#correctOptionThree").prop("checked", true);

            }
            else if (result.OptionFour.Correct == 1) {


                $("#correctOptionFour").prop("checked", true);

            }
            $("#addBtn").css("visibility", "Visible");
        },
        error: function (result) {

            alert("Error");
        }

    });



}
function submit() {
    jQuery.ajax({

        url: "/Paper/Submit",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        cache: false,

        success: function (result) {

            if (result == '1') {


                alert("Paper Added Successfully");
            }
        },


    });


}
function readURL(input) {

    alert("Called");
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah')
                .attr('src', e.target.result)
                .width(150)
                .height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
function list() {
    jQuery.ajax({

        url: "/Paper/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";

            }
            else {
                $.each(result, function (key, item) {

                    html += "<tr>";
                    html += "<td>" + item.Subject + "</td>";
                    html += "<td>" +
                    '<button rel="tooltip" title="Delete" class="fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>"
                    + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                    '<button rel="tooltip" title="View"  class="fa fa-edit" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";
                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });

}
var paperId = "";
function deleteConfirm(id) {

    $("#confirmDelete").modal('show');

    paperId = id;

}
function deletePaper() {

    $.ajax({
        url: "/Paper/Delete",
        data: { 'paperId': String(paperId) },
        type: "POST",
        dataType: "json",
        success: function (result) {

            sessionDeleteID = "";



            $("#myModal").modal("show");




        },
        error: function (result) { }
    });



}
function setPaper() {

    jQuery.ajax({

        url: "/Paper/SetPaper",

        type: "GET",

        data: "",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {




        }

    });


}
function getPaper() {


    alert($("#teacherId").val());
    debugger;
        jQuery.ajax({

        url: "/Paper/GetPaperNames",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val(),
            "courseId": $("#subjectSelectOptions").val(),
            "teacherId": $("#teacherId").val()
        },

        dataType: "json",

        success: function (result) {

            debugger;
            var $el = $("#paperSelectOptions");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#paperSelectOptions option:first").attr('selected', 'selected');
            $("#paperSelectOptions option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.PaperName));
            });

            $('#paperSelectOptions').selectpicker('refresh');
        },
        error: function(result) {

            alert(result);
        }

    });



}
function displayPaper() {
    
    jQuery.ajax({

        url: "/Paper/DisplayPaper",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        data: { 'paperId': $("#paperSelectOptions").val() },

        dataType: "json",

        success: function (result) {
            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";

            }
            else {
                $.each(result, function (key, item) {

                    html += "<tr>";
                    html += "<td>" + item.SectionNames + "</td>";
                    html += "<td>" + item.NoOfQuestions + "</td>";
                    html += "<td>" + item.TimeAllowed + "</td>";
                    html += "<td>" + item.Active + "</td>";
                    html += "<td>" + item.TotalMarks + "</td>";
                    html += "<td>" + item.Password + "</td>";
                    html += "<td>" + '<button type="button" onclick="update('+ "'"+ item.Id +"'"+')" rel="tooltip" title="Edit" class="fa fa-edit"></button>'
                        + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button rel="tooltip" title="Delete" class="fa fa-eye" aria-hidden="true" onclick=generatePaper(' + "'" + item.Id + "'" + ")></button>"
                        + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button rel="tooltip" title="Delete" class="fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);



        },
        error: function (result) {

            alert("Error");
        }

    });


}
var paperUpdateId = "";
function update(id) {

    paperUpdateId = id;
    $("#UpdateModal").modal("show");

    

}
function updatePaper() {

 
    jQuery.ajax({

        url: "/Paper/UpdatePaper",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: {
            'paperId': paperUpdateId,
            'active': $("#paperSelectUpdate").val(),
            'password': $("#passwordUpdate").val()
        },

        dataType: "json",

        success: function (result) {

            alert("Updated");

        },
        error: function (result) {

            alert("Error");
        }

    });


}
function validateDisplayPaper() {


        $('#passwordUpdate').keyup(function() {
            var $th = $(this);
            $th.val($th.val().replace(/[^a-zA-Z1-9]/g,
                function(str) {
                    alert('You typed " ' + str + ' ".\n\nPlease use only alphabets and numbers.');
                    return '';
                }));
        });


    }
function CheckEmptinessForList() {


        var passwordUpdate = $("#passwordUpdate").val();

        if (jQuery.trim(passwordUpdate).length == 0) {

            return true;
        }
        return false;

    }
function generatePaper(id) {
    jQuery.ajax({

        url: "/Paper/GetPaperForGeneration",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        data: { 'paperId': $("#paperSelectOptions").val() },

        dataType: "json",

        success: function (result) {

            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";

            }
            else {
                html += "<div id='checkboxes'>"
                $.each(result, function (key, item) {
                    
                    html += "<lable> Marks : " + item.Marks + "</label>";
                    html += "<br>";
                    html += "<input type='Checkbox' value="+"'"+item.Id+"'"+"/>" ;
                    html += "<lable>: " + item.Text + "</label>";
                    html += "<br/>";
                    html += "<img src=" + "'" + item.QuestionImage + "'" + "  height='100' width='200'/>";
                    html += "<br>";
                    if (item.OptionOne.Correct) {
                        html += "<input type='radio'  checked>";
                    } else {
                        html += "<input type='radio' >";
                    }
                    html += "<lable> " + item.OptionOne.Text + "</label>";
                    html += "<br>";

                    if (item.OptionTwo.Correct) {
                        html += "<input type='radio'  checked>";
                    } else {
                        html += "<input type='radio' >";
                    }
                    html += "<lable> " + item.OptionTwo.Text + "</label>";

                    html += "<br>";

                    if (item.OptionThree.Correct ) {
                        html += "<input type='radio'  checked>";
                    } else {
                        html += "<input type='radio' >";
                    }
                    html += "<lable> " + item.OptionThree.Text + "</label>";

                    html += "<br>";

                    if (item.OptionFour.Correct) {
                        html += "<input type='radio'  checked>";
                    } else {
                        html += "<input type='radio'>";
                    }
                    html += "<lable>" + item.OptionFour.Text + "</label>";
                    html += "<hr/>";
                });
                html += "</div>";
            }

            $("#paper").html(html);
            $("#generatePaperModal").modal('show');


        },
        error: function (result) {

            alert("Error");
        }

    });

      


}
var selected="";
function generateNewPaper() {
    selected = [];
    alert("Called");
    $('#checkboxes input[Type=checkbox]:checked').each(function () {
        selected.push($(this).attr('value'));
        alert(selected);
    });

   

    $("#generateModal").modal('show');





}
function generateAfterPaper() {

    var selectedSections = [];
    $("#sectionSelectOptionsUpdate :selected").each(function () {
        selectedSections.push($(this).attr('value'));
    });



    debugger;
    var paperDetailObj = {

        SessionId: $("#sessionSelectOptionsUpdate").val(),
        SemesterId: $("#semesterSelectOptionUpdate").val(),
        Sections: selectedSections,
        CourseId: $("#subjectSelectOptionsUpdate").val(),
        TeacherId: $("#teacherSelectOptionsUpdate").val(),
        NoOfQuestions: $("#questionNoUpdate").val(),
        PaperName: $("#paperNameUpdate").val(),
        TimeAllowed: $("#timeAllowedUpdate").val(),
        Active: $("#activeUpdate").val(),
        Password: $("#passwordUpdate1").val(),
        TotalMarks: $("#totalMarksUpdate").val(),
        QuestionList: selected
        
    }
  



    jQuery.ajax({

        url: "/Paper/GeneratePaper",

        type: "POST",

        data:  JSON.stringify(paperDetailObj) ,

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {




        }

    });

}
// For Update
function getSessionsUpdate() {


    jQuery.ajax({

        url: "/Paper/GetSessions",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            var $el = $("#sessionSelectOptionsUpdate");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#sessionSelectOptionsUpdate option:first").attr('selected', 'selected');
            $("#sessionSelectOptionsUpdate option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SessionId));
            });



            $('#sessionSelectOptionsUpdate').selectpicker('refresh');

        }

    });



}
var _sessionIdUpdate = "";
function getSemestersUpdate() {

    var url = "/Paper/GetSemesters";
    jQuery.ajax({

        url: url,

        type: "GET",

        data: { sessionId: _sessionId },

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {


            var $el = $("#semesterSelectOptionUpdate");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#semesterSelectOptionUpdate option:first").attr('selected', 'selected');
            $("#semesterSelectOptionUpdate option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SemesterNo));
            });

            $('#semesterSelectOptionUpdate').selectpicker('refresh');


        },
        failure: function (result) {
            console.log(result.responseText);
        },
        error: function (result) {
            console.log(result.responseText);
        }






    });

}
//
function getSemestersByIdUpdate(id) {
    _sessionIdUpdate = id;
    getSemestersUpdate();
}

function getTeachersUpdate() {

    jQuery.ajax({

        url: "/Paper/GetTeacher",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptionsUpdate").val(), semesterId: $("#semesterSelectOptionUpdate").val(), courseId: $("#subjectSelectOptionsUpdate").val() },
        success: function (result) {

            var $el = $("#teacherSelectOptionsUpdate");
            $el.empty(); // remove old options

            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.FirstName + " " + value.LastName));
            });

            $('#teacherSelectOptionsUpdate').selectpicker('refresh');


        }

    });


}

function validationSetPaperUpdate() {
    $('#questionNoUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });
    $('#paperNameUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9 ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only aplhabets and letters.'); return ''; }));
    });

    $('#timeAllowedUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });

    $('#passwordUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only alphabets and numbers.'); return ''; }));
    });

    $('#totalMarksUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only alphabets and numbers.'); return ''; }));
    });

}

function getSectionUpdate() {


    jQuery.ajax({

        url: "/Paper/GetSection",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptionsUpdate").val(), semesterId: $("#semesterSelectOptionUpdate").val() },
        success: function (result) {

            var $el = $("#sectionSelectOptionsUpdate");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SectionName));
            });

            //  $('#sectionSelectOptions').trigger("chosen:updated");
            $('#sectionSelectOptionsUpdate').selectpicker('refresh');


        }

    });


    getCourseUpdate();
}

function getCourseUpdate() {

    jQuery.ajax({

        url: "/Paper/GetCourse",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptionsUpdate").val(), semesterId: $("#semesterSelectOptionUpdate").val() },
        success: function (result) {

            var $el = $("#subjectSelectOptionsUpdate");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#subjectSelectOptionsUpdate option:first").attr('selected', 'selected');
            $("#subjectSelectOptionsUpdate option:first").css('display', 'none');
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.CourseName));
            });

            $('#subjectSelectOptionsUpdate').selectpicker('refresh');



        }

    });





}
//  Just for student
function getPaperForStudent() {
    alert("Called");
    jQuery.ajax({
        url: "/Student/GetPaperNames",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val(),
            "courseId": $("#subjectSelectOptions").val(),
        },
        dataType: "json",
        success: function (result) {
            var $el = $("#paperSelectOptions");
            $el.empty(); // remove old options
            $el.append($("<option></option>").attr("value", "").text("Nothing Selected"));
            $("#paperSelectOptions option:first").attr('selected', 'selected');
            $("#paperSelectOptions option:first").css('display', 'none');
            $.each(result, function (key, value) {
                $el.append($("<option></option>").attr("value", value.Id).text(value.PaperName));
            });

            $('#paperSelectOptions').selectpicker('refresh');
        },


    });



}
function getResult()
{
    jQuery.ajax({

        url: "/Student/GetResult",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val(),
            "courseId": $("#subjectSelectOptions").val(),
            "paperId": $("#paperSelectOptions").val(),
            "rollNumber": $("#rollNumber").val()
        },
        success: function (result) {

            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";

            }
            else {
                $.each(result, function (key, item) {

                    html += "<tr>";
                    html += "<td>" + item.MarksObtained + "</td>";
                    html += "<td>" + item.TotalMarks + "</td>";
                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });







}
function getMidterm() {

    jQuery.ajax({
        url: "/Student/GetMidterm",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val()
        },
        dataType: "json",
        success: function (result) {
            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";
            }
            else {
                $.each(result, function (key, item) {
                    html += "<tr>";
                    html += "<td>" + item.CourseName+ "</td>";
                    html += "<td>" + item.StartDay + "-" + item.StartMonth+ "-" +item.StartYear+"</td>";
                    html += "<td>" + item.Time + "</td>";
                    html += "</tr>";
                });
            }

            $("tbody").html(html);
        },


    });



}
function geMaterial() {

    jQuery.ajax({
        url: "/Student/GetMaterial",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val()
        },
        dataType: "json",
        success: function (result) {
            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";
            }
            else {
                $.each(result, function (key, item) {
                    html += "<tr>";
                    html += "<td>" + item.CourseName + "</td>";
                    html += "<td>" + item.TeacherName +"</td>";
                    html += "<td>" + item.MaterialName + "</td>";
                    html += "<td>" + item.MaterialDescription + "</td>";
                    html += "</tr>";
                });
            }

            $("tbody").html(html);
        },


    });



}
function getNotification()
{
    jQuery.ajax({
        url: "/Student/GetNotification",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val()
        },
        dataType: "json",
        success: function (result) {
            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";
            }
            else {
                $.each(result, function (key, item) {
                    html += "<tr>";
                    html += "<td>" + item.CourseName + "</td>";
                    html += "<td>" + item.TeacherName + "</td>";
                    html += "<td>" + item.NotificationName + "</td>";
                    html += "<td>" + item.NotificationDescription + "</td>";
                    html += "</tr>";
                });
            }

            $("tbody").html(html);
        },
    });
}


function displayResult()
{
    

    jQuery.ajax({

        url: "/Paper/DisplayResult",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        data: { 'paperId': $("#paperSelectOptions").val() },

        dataType: "json",

        success: function (result) {
            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";

            }
            else {
                $.each(result, function (key, item) {

                    html += "<tr>";
                    html += "<td>" + item.SessionName + "</td>";
                    html += "<td>" + item.SemesterName + "</td>";
                    html += "<td>" + item.CourseName + "</td>";
                    html += "<td>" + item.PaperName + "</td>";
                    html += "<td>" + item.RollNumber + "</td>";
                    html += "<td>" + item.ObtainedMarks + "</td>";
                    html += "<td>" + item.TotalMarks + "</td>";
                    html += "</tr>";
                });
            }

            $("tbody").html(html);



        },
        error: function (result) {

            alert("Error");
        }

    });





}