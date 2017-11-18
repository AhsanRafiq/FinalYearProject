function register() {

    $('#rollNumber').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#name').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });





    $('#fatherName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });




    $('#creditHour').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });




    $('#rollNumberUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#nameUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });





    $('#fatherNameUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });




    $('#creditHourUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });




}


function getSessions() {

    jQuery.ajax({

        url: "/Student/GetSessions",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            var $el = $("#sessionSelectOptions");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SessionId));
            });

            $('#sessionSelectOptions').selectpicker('refresh');



        }

    });



}
var _sessionId = "";
function getSemesters() {

    var $el = $("#sectionSelect");
    $el.empty(); // remove old options
    var url = "/Student/GetSemesters";

    jQuery.ajax({

        url: url,

        type: "GET",

        data: { sessionId: _sessionId },

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {


            var $el = $("#semesterNoSelect");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SemesterNo));
            });

            $('#semesterNoSelect').selectpicker('refresh');

           


        },
        failure: function (result) {
            console.log(result.responseText);
        },
        error: function (result) {
            console.log(result.responseText);
        }






    });

}

function getSemestersById(id) {


    _sessionId = id;

    getSemesters();
}

function getSection()
{
    

    jQuery.ajax({

        url: "/Student/GetSection",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterNoSelect").val() },
        success: function (result) {

            var $el = $("#sectionSelect");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SectionName));
            });

            $('#sectionSelect').selectpicker('refresh');


        }

    });

}


function insert() {

    var isEmpty = CheckEmptinessInsert();

    if (isEmpty)
    {
        alert("Some Fields are empty");
        return;
    }


    var studentObject = {
        SessionId: $("#sessionSelectOptions").val(),
        SemesterId: $("#semesterNoSelect").val(),
        SectionId: $("#sectionSelect").val(),
        RollNumber :$("#rollNumber").val(),
        Name: $("#name").val(),
        FatherName: $("#fatherName").val(),
        Password: $("#password").val(),
    }




    jQuery.ajax({

        url: "/Student/Insert",

        type: "POST",

        data: JSON.stringify(studentObject),

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            if (result == '1') {
                $("#rollNumber").val("");
                $("#name").val("");
                $("#fatherName").val("")
                $("#password").val("");
                $("#resposeOfUpdation").modal("show");
             

            }

            if (result == '0') {


                $("#primaryKeyViolationModal").modal("show");
            }

        }






    });

}
function list() {

    jQuery.ajax({
        url: "/Student/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { sessionId: $('#sessionSelectOptions').val(), semesterId: $('#semesterNoSelect').val(), sectionId: $('#sectionSelect').val() },
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
                    html += "<td>" + item.RollNumber + "</td>";
                    html += "<td>" + item.Name + "</td>";
                    html += "<td>" + item.FatherName + "</td>";
                    html += "<td>" + '<button type="button" onclick="update(' + item.Id + "," + "'" + item.SessionId + "'" + "," + "'" + item.SemesterId + "'" + "," + "'" + item.SectionId + "'" + "," + "'" + item.RollNumber + "'" + "," + "'" + item.Name + "'" + "," + "'" + item.FatherName + "'" + "," + "'" + item.Password + "'" + ')" rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit"></button>'
                    + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                    '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });

}

var studentUpdateId = "";

function update(id, sessionId, semesterId, sectionId,rollNumber,name,fatherName,password) {
    $("#rollNumberUpdate").val(rollNumber);
    $("#nameUpdate").val(name);
    $("#fatherNameUpdate").val(fatherName);
    $("#passwordUpdate").val(password);
    $("#studentImage").attr("src","../../StudentImages/"+rollNumber+".png");
    $("#UpdateModal").modal("show");
    studentUpdateId = id;
}

function updateStudent() {


    var isEmpty = CheckEmptinessUpdate();

    if (isEmpty) {
        alert("Some Fields are empty");
        return;
    }


    var studentOject = {

        Id: studentUpdateId,
        SessionId: $("#sessionSelectOptionsUpdate").val(),
        SemesterId: $("#semesterNoSelectUpdate").val(),
        SectionId: $("#sectionSelectUpdate").val(),
        RollNumber: $("#rollNumberUpdate").val(),
        Name: $("#nameUpdate").val(),
        FatherName: $("#fatherNameUpdate").val(),
        Password: $("#passwordUpdate").val(),
    }



    $.ajax({
        url: "/Student/Update",
        data: JSON.stringify(studentOject),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result == '0') {

                $("#primaryKeyViolationModal").modal("show");

            }
            if (result == '1') {
              
                $("#rollNumberUpdate").val("");
               $("#nameUpdate").val("");
               $("#fatherNameUpdate").val("");
               $("#passwordUpdate").val("");
               
                $("#updateSubjectResponse").modal("show");
            }


        },
        error: function (result) { }
    });



}

function getSessionsUpdate() {

    jQuery.ajax({

        url: "/Student/GetSessions",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            var $el = $("#sessionSelectOptionsUpdate");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SessionId));
            });

            $('#sessionSelectOptionsUpdate').selectpicker('refresh');


        },


    });



}

var _sessionIdUpdate = "";
function getSemestersUpdate() {

    var url = "/Student/GetSemesters";
    jQuery.ajax({

        url: url,

        type: "GET",

        data: { sessionId: _sessionIdUpdate },

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {


            var $el = $("#semesterNoSelectUpdate");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SemesterNo));
            });

            $('#semesterNoSelectUpdate').selectpicker('refresh');


        },
    });

}

function getSemestersByIdUpdate(id) {
    _sessionIdUpdate = id;
   
    getSemestersUpdate();
}



function getSectionUpdate() {


    jQuery.ajax({

        url: "/Student/GetSection",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
        data: { sessionId: $("#sessionSelectOptionsUpdate").val(), semesterId: $("#semesterNoSelectUpdate").val() },
        success: function (result) {

            var $el = $("#sectionSelectUpdate");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.SectionName));
            });

            $('#sectionSelectUpdate').selectpicker('refresh');


        }

    });

}


var studentDeleteId = "";
function deleteConfirm(studentId) {

    $("#confirmDelete").modal("show");
    studentDeleteId = studentId;

 


}

function deleteStudent() {
    $.ajax({
        url: "/Student/Delete",
        data: { 'studentId': studentDeleteId },
        type: "GET",
        dataType: "json",
        success: function (result) {

            subjectDeleteId = "";
            $("#myDeleteModal").modal("show");

        },
        error: function (result) { }
    });



}


function CheckEmptinessInsert()
{
    var rollNumber = $("#rollNumber").val();

    if (jQuery.trim(rollNumber).length == 0) {
        return true;
    }

    var name = $("#name").val();

    if (jQuery.trim(name).length == 0) {
        return true;
    }


    var fatherName = $("#fatherName").val();

    if (jQuery.trim(fatherName).length == 0) {
        return true;
    }

    var password = $("#password").val();

    if (jQuery.trim(password).length == 0) {
        return true;
    }
}


function CheckEmptinessUpdate() {
    var rollNumber = $("#rollNumberUpdate").val();

    if (jQuery.trim(rollNumber).length == 0) {
        return true;
    }

    var name = $("#nameUpdate").val();

    if (jQuery.trim(name).length == 0) {
        return true;
    }


    var fatherName = $("#fatherNameUpdate").val();

    if (jQuery.trim(fatherName).length == 0) {
        return true;
    }

    var password = $("#passwordUpdate").val();

    if (jQuery.trim(password).length == 0) {
        return true;
    }
}
////////////////--- Student Exam Related Operations---/////////////////////

function displayPaperForTest() {

 
    jQuery.ajax({

        url: "/Student/GetPapers",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: {
            'sessionId': $("#sessionSelectOptions").val(),
            "semesterId": $("#semesterSelectOption").val()
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
            else
            {
                $.each(result, function (key, item) {
                
                    html += "<tr>";
                    html += "<td>" + item.CourseName + "</td>";
                    html += "<td>" + item.PaperName + "</td>";
                    html += "<td>" + item.NoOfQuestions + "</td>";
                    html += "<td>" + item.TimeAllowed + "</td>";
                    html += "<td>" + item.TotalMarks + "</td>";
                    html += "<td>" +
                   '<button type="button" onclick="password(' +
                        "'" +
                        item.Id+
                        "'" +","+
                        "'" +
                        item.CourseName+
                        "'" + "," +
                        "'" +
                        item.PaperName+
                        "'" + "," +
                        "'" +
                        item.NoOfQuestions+
                        "'" + "," +
                        "'" +
                        item.TimeAllowed+
                        "'" + "," +
                        "'" +
                        item.TotalMarks +
                        "'" +')" rel="tooltip" title="Take" class="fa fa-edit" ></button>';
                    html += "</tr>";
                });
            }
            $("tbody").html(html);
        },
        error:function(result) {

           
        }
    });
}


var testId = "";
var course = "";
var paper = "";
var question = "";
var time = "";
var marks = "";
function password(id,courseName,paperName,noOfQuestions,timeAllowed,totalMarks) {


    
    testId = id;

    course = courseName;

    paper = paperName;

    question = noOfQuestions;

    time = timeAllowed;

    marks = totalMarks;

    $("#promptPassword").modal('show');
}
function startTest() {
    
    jQuery.ajax({

        url: "/Student/StartTest",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: {
            'id': testId,'password': $("#testPassword").val(),'paperName': paper,'noOfQuestions': question,
            'courseName': course,'totalMarks': marks,'timeAllowed': time
        },
        success: function (result) {
            if (result == '0') {
                alert("Password is not correct");
            }
            if (result == '1') {
                $("#startTest").modal('show');
            }
        },
        error: function (result) {

           
          
        }
    });


}

function submit()
{
    jQuery.ajax({
        url: "/Student/Submit",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        
        success: function (result) {

            alert("Report is saved");
          
        },


    });
}

function next() {
    jQuery.ajax({
        url: "/Student/Next",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            $("#questionNo").text("Q" + (result.Index + 1) + ":");
            $("#questionText").text(result.Text);
            $("#marks").text(result.Marks);
            $("#questionImage").attr("src", result.QuestionImage);
            $("#optionOne").text(result.OptionOne.Text);
            $("#optionTwo").text(result.OptionTwo.Text);
            $("#optionThree").text(result.OptionThree.Text);
            $("#optionFour").text(result.OptionFour.Text);
            debugger;
            if (result.OptionOne.Correct =='1') {
                $('#correctOptionOne').prop('checked', 'checked');
            }
            else {
                $('#correctOptionOne').attr('checked', false);
            }
            if (result.OptionTwo.Correct == '1') {
                $('#correctOptionTwo').prop('checked', 'checked');
            }
            else {
                $('#correctOptionTwo').attr('checked', false);
            }
            if (result.OptionThree.Correct == '1') {
                $('#correctOptionThree').prop('checked', 'checked');
            }
            else {
                $('#correctOptionThree').attr('checked', false);
            }
            if (result.OptionFour.Correct == '1') {
                $('#correctOptionFour').prop('checked', 'checked');
            }
            else {
                $('#correctOptionFour').attr('checked', false);
            }
        },
        error: function (result) {
            alert("Error");
        }

    });
}
function previous() {
    jQuery.ajax({
        url: "/Student/Previous",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#questionNo").text("Q" + (result.Index + 1) + ":");
            $("#questionText").text(result.Text);
            $("#marks").text(result.Marks);
            $("#questionImage").attr("src", result.QuestionImage);
            $("#optionOne").text(result.OptionOne.Text);
            $("#optionTwo").text(result.OptionTwo.Text);
            $("#optionThree").text(result.OptionThree.Text);
            $("#optionFour").text(result.OptionFour.Text);
            debugger;
            if (result.OptionOne.Correct =='1')
            {
                $('#correctOptionOne').prop('checked', 'checked');
            }
            else {
                $('#correctOptionOne').attr('checked', false);
            }
            if (result.OptionTwo.Correct == '1')
            {
                $('#correctOptionTwo').prop('checked', 'checked');
            }
            else {
                $('#correctOptionTwo').attr('checked', false);
            }
            if (result.OptionThree.Correct == '1')
            {
                $('#correctOptionThree').prop('checked', 'checked');
            }
            else {
                $('#correctOptionThree').attr('checked', false);
            }
            if (result.OptionFour.Correct == '1') {
                $('#correctOptionFour').prop('checked', 'checked');
            }
            else {
                $('#correctOptionFour').attr('checked', false);
            }
        },
        error: function (result) {
            alert("Error");
        }

    });



}

function saveAns()
{

   
    var optionOne = {
        Text: $("#optionOne").text(),
        Correct: $('#correctOptionOne').is(':checked')
    }
    var optionTwo = {
        Text: $("#optionTwo").text(),
        Correct: $('#correctOptionTwo').is(':checked')
    }
    var optionThree = {
        Text: $("#optionThree").text(),
        Correct: $('#correctOptionThree').is(':checked')
    }
    var optionFour = {
        Text: $("#optionFour").text(),
        Correct: $('#correctOptionFour').is(':checked')
    }
    var QuestionObject = {

        Text: $("#questionText").text(),
        OptionOne: optionOne,
        OptionTwo: optionTwo,
        OptionThree: optionThree,
        OptionFour: optionFour,
    }
    $.ajax({
        url: "/Student/SaveAnswer",
        type: "POST",
        data: JSON.stringify(QuestionObject),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
        
        },
        error: function (result) {


            alert("Error " + result);
        }


    });







}
function timeStart() {

   
    $("#time").countdowntimer({

        minutes:parseInt( $("#paperTime").text()),
        seconds: 0,
        size: "lg",
        timeUp: timeIsUp
    });
    function timeIsUp() {

        submit();
    }

   


}



function changePassword()
{
    var isEmpty = validateTextboxes();
    if (isEmpty) {
        alert("Some fields are empty");
        return;
    }
    $.ajax({
        url: "/Student/ChangePassword",
        data: { 'oldPassword': $("#oldPassword").val(), 'newPassword': $("#newPassword").val() },
        type: "GET",
        dataType: "json",
        success: function (result) {
           
             
            
            if (result == '0') {
                alert("Old Password is incorrect");
                return;
            }
            alert("Password Changed");

            $("#oldPassword").val("");

            $("#newPassword").val("");
        },
        error: function (result) {
        }
    });
}



function validateTextboxes() {
    var oldPassword = $('#oldPassword').val();
    if (oldPassword.trim().length == 0) {
        return true;
    }
    var newPassword = $('#newPassword').val();
    if (newPassword.trim().length == 0) {
        return true;
    }
}


