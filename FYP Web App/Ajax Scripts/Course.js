function register() {

    $('#courseCode').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#courseName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });

    $('#creditHour').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });


    $('#courseCodeUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#courseNameUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });

    $('#creditHourUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });

}

function list() {
    jQuery.ajax({

        url: "/Course/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { sessionId: $('#sessionSelectOptions').val(), semesterId: $('#semesterNoSelect').val() },
        success: function (result) {

            var html = "";
            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "</tr>";

            }
            else {
                $.each(result, function (key, item) {

                    html += "<tr>";
                    html += "<td>" + item.CourseCode + "</td>";
                    html += "<td>" + item.CourseName + "</td>";
                    html += "<td>" + item.CreditHour + "</td>";
                    html += "<td>" + '<button type="button" onclick="update(' + item.Id + "," + "'" + item.CourseCode + "'" + "," + "'" + item.CourseName + "'" + "," + "'" + item.CreditHour + "'" + ')" rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit"></button>'
                    + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                    '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });

}

function refresh() {


    list();

}
var subjectDeleteID = "";
function deleteConfirm(subjectId) {

    $("#confirmDelete").modal("show");
    subjectDeleteID = subjectId;




}

function deleteCourse() {
    $.ajax({
        url: "/Course/Delete",
        data: { 'courseId': subjectDeleteID },
        type: "GET",
        dataType: "json",
        success: function (result) {

            subjectDeleteID = "";
            if (result == 547) {

                alert("Cannot be deleted because it is referenced");

            }


            $("#myDeleteModal").modal("show");




        },
        error: function (result) { }
    });



}
var subjectUpdateId = "";
function update(id, courseCode, courseName, creditHour) {


    $("#courseCodeUpdate").val(courseCode);
    $("#courseNameUpdate").val(courseName);
    $("#creditHourUpdate").val(creditHour);
    $("#UpdateModal").modal("show");
    subjectUpdateId = id;



}

function updateCourse() {


    if (CheckEmptyUpdate()) {

        alert("Some Fields are empty");
        return;
    }


    var subjectOject = {

        Id: subjectUpdateId,
        SessionId: $("#sessionSelectOptionsUpdate").val(),
        SemesterId: $("#semesterNoSelectUpdate").val(),
        CourseCode: $("#courseCodeUpdate").val(),
        CourseName: $("#courseNameUpdate").val(),
        CreditHour: $("#creditHourUpdate").val(),

    }



    $.ajax({
        url: "/Course/Update",
        data: JSON.stringify(subjectOject),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result == '0') {
               
                $("#primaryKeyViolationModal").modal("show");

            }
            if (result == '1') {
                alert(result);
                $("#courseCodeUpdate").val("");
                $("#courseNameUpdate").val("");
                $("#creditHourUpdate").val("");
                $("#semesterNoSelectUpdate").val("");
                $("#updateSubjectResponse").modal("show");
            }
           
           

        }
    });



}


function insert() {

   

    if (CheckEmpty()) {

        alert("Some Fields are empty");
        return;
    }


    var subjectOject = {

        SessionId: $("#sessionSelectOptions").val(),
        SemesterId: $("#semesterNoSelect").val(),
        CourseCode: $("#courseCode").val(),
        CourseName: $("#courseName").val(),
        CreditHour: $("#creditHour").val(),

    }



    $.ajax({
        url: "/Course/Insert",
        data: JSON.stringify(subjectOject),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            if (result == '0') {

                $("#primaryKeyViolationModal").modal("show");

            }

            if (result == '1') {
              
                $("#courseCode").val("");
                $("#courseName").val("");
                $("#creditHour").val("");
                $("#resposeOfUpdation").modal("show");
            }
        }
    });







}

function getSessions() {

    jQuery.ajax({

        url: "/Course/GetSessions",

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

    var url = "/Course/GetSemesters";
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


        }
    });

}

function getSemestersById(id) {
    _sessionId = id;
    getSemesters();
}



function getSessionsUpdate() {

    jQuery.ajax({

        url: "/Course/GetSessions",

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


        }

    });



}

var _sessionIdUpdate = "";
function getSemestersUpdate() {

    var url = "/Course/GetSemesters";
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


        }






    });

}

function getSemestersByIdUpdate(id) {
    _sessionIdUpdate = id;
    getSemestersUpdate();
}


function CheckEmpty()
{

    var SessionId = $("#sessionSelectOptions").val();
    var SemesterId = $("#semesterNoSelect").val();
    var CourseCode = $("#courseCode").val();
    var CourseName = $("#courseName").val();
    var CreditHour = $("#creditHour").val();

   if (SessionId == 0) {
       return true;
   }

   if (SemesterId == 0) {
       return true;
   }

   if (CourseCode == 0) {
       return true;
   }

   if (CourseName == 0) {
       return true;
   }

   if (CreditHour == 0) {
       return true;
   }
}


function CheckEmptyUpdate() {

    var SessionId = $("#sessionSelectOptions").val();
    var SemesterId = $("#semesterNoSelect").val();
    var CourseCode = $("#courseCodeUpdate").val();
    var CourseName = $("#courseNameUpdate").val();
    var CreditHour = $("#creditHourUpdate").val();

    if (SessionId == 0) {
        return true;
    }

    if (SemesterId == 0) {
        return true;
    }

    if (CourseCode == 0) {
        return true;
    }

    if (CourseName == 0) {
        return true;
    }

    if (CreditHour == 0) {
        return true;
    }
}

