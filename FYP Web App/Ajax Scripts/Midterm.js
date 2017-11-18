function saveMidterm() {
    var dateTime =  $("#dateTimeOfMidterm").val().split('T');
    var dateParts = dateTime[0].split('-');
    var year = dateParts[0];
    var month = dateParts[1];
    var day = dateParts[2];
    var selectedSections = "";
    $("#sectionSelectOptions :selected").each(function () {
        selectedSections = selectedSections + $(this).val() + " ";
    });
    var isEmpty = CheckEmptiness();
    if (isEmpty == true) {

        alert("Some Fields are empty");
        return;
    }
    var midterm = {
        SessionId: $("#sessionSelectOptions").val(),
        SemesterId: $("#semesterSelectOption").val(),
        CourseId: $("#subjectSelectOptions").val(),
        StartDay: day,
        StartMonth: month,
        StartYear: year,
        SectionNames: selectedSections,
        Time: dateTime[1]
    }
    jQuery.ajax({

        url: "/MidTerm/Insert",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(midterm),
        success: function (result) {

            if (result == '0') {
                alert("MidTerm with this name already exist");
                return;
            }
            if (result == '1') {
                alert("MidTerm Added");
                $("#dateTimeOfMidterm").val("");
            }
        }
    });
}
function CheckEmptiness() {
    var materialName = $('#dateTimeOfMidterm').val();
    if (materialName.trim().length == 0) {
        return true;
    }
    var sessionSelectOptions = $('#sessionSelectOptions').val();
    if (sessionSelectOptions.trim().length == 0) {
        return true;
    }




    var semesterSelectOption = $('#semesterSelectOption').val();
    if (semesterSelectOption.trim().length == 0) {
        return true;
    }

    var subjectSelectOptions = $('#subjectSelectOptions').val();
    if (subjectSelectOptions.trim().length == 0) {
        return true;
    }
}
function getSectionForMidterm() {


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
}
function displayMidterm() {
    jQuery.ajax({

        url: "/MidTerm/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterSelectOption").val() },
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
                    html += "<td>" + item.StartDay + "-" + item.StartMonth + "-" + item.StartYear + "</td>";
                    html += "<td>" + item.Time+ "</td>";
                    html += "<td>" + '<button type="button" onclick="updateMidterm(' + "'" + item.Id + "'" + ')" rel="tooltip" title="View" class="btn btn-primary fa fa-edit"></button>'
                         +  "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                            '<button type="button" rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" onclick="deleteConfirmMidterm(' + "'" + item.Id + "'" + ')"></button>' + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);

        },
    });


}

var midtermId = "";
function updateMidterm(id) {


    midtermId = id;
   
    $("#myUpdateModal").modal('show');

}

function UpdateConfrim() {

    var dateTime = $("#dateTimeUpdate").val().split('T');
    var dateParts = dateTime[0].split('-');
    var year = dateParts[0];
    var month = dateParts[1];
    var day = dateParts[2];
    var selectedSections = "";
    var isEmpty = CheckEmptinessUpdate();
    if (isEmpty == true) {

        alert("Some Fields are empty");
        return;
    }
    jQuery.ajax({

        url: "/MidTerm/Update",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { 'id': midtermId, 'startDay': day, 'startMonth': month, 'startYear': year, 'time': dateTime[1]},
        success: function (result) {
            if (result == '0') {
                alert("Midterm with this name already exist");
                return;
            }
            if (result == '1') {
                alert("Midterm Updated");
                $("#dateTimeUpdate").val("");
            }
        }
    });
}
function CheckEmptinessUpdate() {
    var dateTimeUpdate = $('#dateTimeUpdate').val();
    if (dateTimeUpdate.trim().length == 0) {
        return true;
    }

}

var midtermDeleteId = "";
function deleteConfirmMidterm(id) {

    $("#myDeleteModal").modal('show');

    midtermDeleteId = id;

}

function deleteMidterm() {
    $.ajax({
        url: "/MidTerm/Delete",
        data: { 'id': midtermDeleteId },
        type: "GET",
        dataType: "json",
        success: function (result) {
            midtermDeleteId = "";
            $("#myDeleteModalShow").modal("show");
        },
        error: function (result) { }
    });



}


