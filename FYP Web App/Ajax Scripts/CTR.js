ffunction getSessions() {

  

    jQuery.ajax({

        url: "/CTR/GetSessions",

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
    var url = "/CTR/GetSemesters";

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

function getSection() {
    jQuery.ajax({

        url: "/CTR/GetSection",

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

function getTeachers()
{

    jQuery.ajax({

        url: "/CTR/GetTeacher",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",
       
        success: function (result) {

            var $el = $("#teacherSelect");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.EmployeeId + " - " + value.FirstName));
            });

            $('#teacherSelect').selectpicker('refresh');


        }

    });


}
function insert() {




    var CTRObject = {
        SessionId: $("#sessionSelectOptions").val(),
        SemesterId: $("#semesterNoSelect").val(),
        SectionId: $("#sectionSelect").val(),
        CourseId: $("#courseSelect").val(),
        TeacherId: $("#teacherSelect").val(),

    }




    jQuery.ajax({

        url: "/CTR/Insert",

        type: "POST",

        data: JSON.stringify(CTRObject),

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            if (result == '1') {

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

        url: "/CTR/List",
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
                    html += "<td>" + item.CourseName + "</td>";
                    html += "<td>" + item.EmployeeId + "</td>";
                    html += "<td>" + item.TeacherName + "</td>";
                    html += "<td>" + '<button type="button" onclick="update('+ "'" + item.Id+"'"  + ')" rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit"></button>'
                    + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                    '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });

}

var ctrUpdateId = "";
function update(id) {

    ctrUpdateId = id;
    $("#UpdateModal").modal("show");
}

function updateCTR() {
    var ctrObject = {
        Id: ctrUpdateId,
        SessionId: $("#sessionSelectOptionsUpdate").val(),
        SemesterId: $("#semesterNoSelectUpdate").val(),
        SectionId: $("#sectionSelectUpdate").val(),
        CourseId: $("#courseSelectUpdate").val(),
        TeacherId: $("#teacherSelectUpdate").val(),

    };
    $.ajax({
        url: "/CTR/Update",
        data: JSON.stringify(ctrObject),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {


            //Primary Key Violation
            if (result == "0") {

                $("#primaryKeyViolationModal").modal("show");
            }
            //Wrong Date Entered
            if (result == "4") {

                $("#wrongDateIntervalModal").modal("show");
            }

            if (result == "2") {

                $("#wrongDateIntervalModal").modal("show");
            }

            if (result == "1") {

                $("#updateSessionResponse").modal("show");

            }

        },
        error: function (errormessage) {


            alert("Error");
        }


    }

   );

}
function getSessionsUpdate() {
    jQuery.ajax({
        url: "/CTR/GetSessions",
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

var _sessionId = "";
function getSemestersUpdate() {

    var $el = $("#sectionSelectUpdate");
    $el.empty(); // remove old options
    var url = "/CTR/GetSemesters";

    jQuery.ajax({

        url: url,

        type: "GET",

        data: { sessionId: _sessionId },

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
        failure: function (result) {
            console.log(result.responseText);
        },
        error: function (result) {
            console.log(result.responseText);
        }






    });

}

function getSemestersByIdUpdate(id) {


    _sessionId = id;

    getSemestersUpdate();
}

function getSectionUpdate() {


    jQuery.ajax({

        url: "/CTR/GetSection",

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

    getCourse();

}
function getCourse() {

    jQuery.ajax({

        url: "/CTR/GetCourse",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterNoSelect").val() },
        success: function (result) {

            var $el = $("#courseSelect");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.CourseName));
            });

            $('#courseSelect').selectpicker('refresh');


        }

    });





}
function getTeachersUpdate() {

    alert("Hello");
    jQuery.ajax({

        url: "/CTR/GetTeacher",

        type: "GET",

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            var $el = $("#teacherSelectUpdate");
            $el.empty(); // remove old options
            $.each(result, function (key, value) {


                $el.append($("<option></option>").attr("value", value.Id).text(value.EmployeeId + " - " + value.FirstName));
            });

            $('#teacherSelectUpdate').selectpicker('refresh');


        }

    });


}
var ctrDeleteId = "";
function deleteConfirm(ctrId) {

    $("#confirmDelete").modal("show");
    ctrDeleteId = ctrId;
}
function deleteCtr() {
    $.ajax({
        url: "/CTR/Delete",
        data: { 'ctrId': ctrDeleteId },
        type: "GET",
        dataType: "json",
        success: function (result) {

            ctrDeleteId = "";
            $("#myDeleteModal").modal("show");

        },
        error: function (result) { }
    });



}