
function getSessions() {



    jQuery.ajax({

        url: "/Semester/GetSessions",

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
function insert() {

    var isEmpty = CheckEmptiness();
    if (isEmpty == true) {
        alert("Some Fields are empty");
        return;
    }


   
    var sectionObject = {
        SessionId: $("#sessionSelectOptions").val(),
        SemesterId: $("#semesterNoSelect").val(),
        SectionName: $("#sectionAlphabetSelect").val(),
        Shift: $("#selectShift").val()

    }




    jQuery.ajax({

        url: "/Section/Insert",

        type: "POST",

        data: JSON.stringify(sectionObject),

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            if (result == '1') {
                $("#resposeOfUpdation").modal("show");
             $("#sectionAlphabetSelect").val(""),
               $("#selectShift").val("")

            }

            if (result == '0') {


                $("#primaryKeyViolationModal").modal("show");
            }

        }






    });

}
var _sessionId = "";

function getSemesters() {

    var url = "/Section/GetSemesters";

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

                
                $el.append($("<option></option>").attr("value", value.SemesterNo).text(value.SemesterNo));
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

function list() {

    jQuery.ajax({
        url: "/Section/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var html = "";

            if (result == 0) {
                html += "<tr>";
                html += "<td>" + "</td>";
                html += "<td>  No Records Found  </td>";
                html += "<td>" + "</td>";
                html += "<td>" + "</td>";
                html += "</tr>";
            }
            else {
                $.each(result, function (key, item) {

                    html += "<tr>";
                    html += "<td>" + item.SessionName + "</td>";
                    html += "<td>" + item.SemesterNo + "</td>";
                    html += "<td>" + item.SectionName + "</td>";
                    html += "<td>" + item.Shift + "</td>";
                    html += "<td>" +
                        '<button  rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit" type="button" onclick="update(' + ("'" + item.Id + "'") + "," + ("'" + item.SectionName + "'") + "," + ("'" + item.Shift + "'") + ')"></button>' +
                        "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });


            }
            $("tbody").html(html);
        }

    });


}

var sectionDeleteID = "";
function deleteConfirm(sectionId) {

    $("#confirmDelete").modal("show");
    sectionDeleteID = sectionId;

}

function deleteSection() {




    $.ajax({
        url: "/Section/Delete",
        data: { 'sectionId': sectionDeleteID },
        type: "POST",
        dataType: "json",
        success: function (result) {

            sectionDeleteID = "";



            $("#myDeleteModal").modal("show");




        },
        error: function (result) { }
    });



}

function referesh() {


    list();
}

var sectionUpdateID = "";
function update(id, sectionName, shift) {



    $("#sectionAlphabetSelectUpdate").val(sectionName);
    $("#selectShiftUpdate").val(shift);
    $("#UpdateModal").modal("show");
    sectionUpdateID = id;



}

function updateSection() {


    var isEmpty = CheckEmptinessUpdate();
    if (isEmpty == true)
    {
        alert("Some Fields are empty");
        return;
    }




    var sectionUpdatedObj = {
        Id: sectionUpdateID,
        SessionId: $("#sessionSelectOptions").val(),
        SemesterNo: $("#semesterNoSelect").val(),
        SectionName: $("#sectionAlphabetSelectUpdate").val(),
        Shift: $("#selectShiftUpdate").val()


    };




    $.ajax({
        url: "/Section/Update",
        data: JSON.stringify(sectionUpdatedObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {


            //Primary Key Violation
            if (result == "0") {

                $("#primaryKeyViolationModal").modal("show");
            }


            if (result == "1") {

                $("#sectionAlphabetSelectUpdate").val("");
                $("#selectShiftUpdate").val("");
    
            }

        },
        error: function (errormessage) {


            alert("Error");
        }


    }

   );

}


function CheckEmptinessUpdate()
{

    var sessionSelectOptions = $("#sessionSelectOptions").val();
    if (sessionSelectOptions.trim().length == 0)
    {
        return true;
    }

    var semesterNoSelect = $("#semesterNoSelect").val();
    if (semesterNoSelect.trim().length == 0) {
        return true;
    }

    var sectionAlphabetSelectUpdate = $("#sectionAlphabetSelectUpdate").val();
    if (sectionAlphabetSelectUpdate.trim().length == 0) {
        return true;
    }

    var selectShiftUpdate = $("#selectShiftUpdate").val();
    if (selectShiftUpdate.trim().length == 0) {
        return true;
    }
    return false;
}

function CheckEmptiness() {

    var sessionSelectOptions = $("#sessionSelectOptions").val();
    if (sessionSelectOptions.trim().length == 0) {
        return true;
    }

    var semesterNoSelect = $("#semesterNoSelect").val();
    if (semesterNoSelect.trim().length == 0) {
        return true;
    }

    var sectionAlphabetSelect = $("#sectionAlphabetSelect").val();
    if (sectionAlphabetSelect.trim().length == 0) {
        return true;
    }

    var selectShift = $("#selectShift").val();
    if (selectShift.trim().length == 0) {
        return true;
    }
    return false;
}