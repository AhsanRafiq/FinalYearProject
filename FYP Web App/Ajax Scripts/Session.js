jQuery(document).ready(function () {


    //list();


});
////


var sessionDeleteID = "";
var isUpdated = "0";
var sessionUpdateID = "";

function list() {
    jQuery.ajax({

        url: "/Session/List",
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
                    html += "<td>" + item.SessionId + "</td>";
                    html += "<td>" + item.SessionProgram + "</td>";
                    html += "<td>" + item.SessionStartDay + "-" + item.SessionStartMonth + "-" + item.SessionStartYear + "</td>";
                    html += "<td>" + item.SessionEndDay + "-" + item.SessionEndMonth + "-" + item.SessionEndYear + "</td>";
                    html += "<td>" + '<button type="button" onclick="update(' + "'" + item.Id + "'" + "," + ("'" + item.SessionStartDay + "-" + item.SessionStartMonth + "-" + item.SessionStartYear + "'") + "," + ("'" + item.SessionEndDay + "-" + item.SessionEndMonth + "-" + item.SessionEndYear + "'") + "," + "'" + item.SessionProgram + "'" + ')" rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit"></button>'
                    + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                    '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });

}
function insert() {


   


    var result = validate();


    if (result == false) {
        return false;

    }

    var sessionStartDate = $("#startDate").val().split("-");
    var sessionEndDate = $("#endDate").val().split("-");

    var sessionObj = {

        SessionId: sessionStartDate[2] + "-" + sessionEndDate[2],
        SessionProgram: $("#programSelect").val(),
        SessionStartDay: sessionStartDate[0],
        SessionStartMonth: sessionStartDate[1],
        SessionStartYear: sessionStartDate[2],
        SessionEndDay: sessionEndDate[0],
        SessionEndMonth: sessionEndDate[1],
        SessionEndYear: sessionEndDate[2]
    };

    $.ajax({
        url: "/Session/Insert",
        data: JSON.stringify(sessionObj),
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

            if (result == "1") {

                if (isUpdated == "0") {

                    $("#resposeOfUpdation").modal("show");

                    list();
                    clearText();
                }
            }


        },
        error: function (errormessage) {


            alert("Error");
        }
    });

}



function validate() {
    var isValid = true;
    if ($("#startDate").val().trim() == "") {
        $("#startDate").css("border-color", "Red");

        isValid = false;

    }
    else {
        $("#startDate").css("border-color", "lightgrey");
    }
    if ($("#endDate").val().trim() == "") {
        $("#endDate").css("border-color", "Red");
        isValid = false;

    }
    else {
        $("#endDate").css("border-color", "lightgrey");
    }
    if ($("#programSelect").val().trim() == "") {
        $("#programSelect").css("border-color", "Red");
        isValid = false;

    }
    else {
        $("#programSelect").css("border-color", "lightgrey");
    }

    return isValid;
}


function clearText() {
    $("#startDate").val("");
    $("#endDate").val("");
    $("#programSelect").value("");
}

function deleteConfirm(sessionId) {

   
    $("#confirmDelete").modal("show");
    sessionDeleteID = sessionId;




}

function deleteSession() {




    $.ajax({
        url: "/Session/Delete",
        data: { 'sessionID': String(sessionDeleteID) },
        type: "POST",
        dataType: "json",
        success: function (result) {

            sessionDeleteID = "";



            if (result == "547") {

                alert("CAnnot be delete because it is referenced");
            }
            if (result == "1") {


                $("#myModal").modal('show');
            }




        },
        error: function (result) { }
    });



}


function update(Id, startDate, endDate, program) {

    sessionUpdateID = Id;
    $("#startDateUpdate").val(startDate);
    $("#endDateUpdate").val(endDate);
    $("#programSelectUpdate").val( program);
    $("#UpdateModal").modal("show");
}

function updateSession() {


    var sessionStartDate = $("#startDateUpdate").val().split("-");
    var sessionEndDate = $("#endDateUpdate").val().split("-");

    var sessionUpdatedObj = {
        Id: sessionUpdateID,
        SessionId: sessionStartDate[2] + "-" + sessionEndDate[2],
        SessionProgram: $("#programSelectUpdate").val(),
        SessionStartDay: sessionStartDate[0],
        SessionStartMonth: sessionStartDate[1],
        SessionStartYear: sessionStartDate[2],
        SessionEndDay: sessionEndDate[0],
        SessionEndMonth: sessionEndDate[1],
        SessionEndYear: sessionEndDate[2]
    };



    $.ajax({
        url: "/Session/Update",
        data: JSON.stringify(sessionUpdatedObj),
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

// Clear Text for Updation
function clearTextUpdate() {
    $("#startDateUpdate").val("");
    $("#endDateUpdate").val("");
    $("#programSelectUpdate").value("");
}

function referesh() {

    list();


}