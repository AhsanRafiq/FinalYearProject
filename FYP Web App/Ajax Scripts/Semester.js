




function list() {

    jQuery.ajax({
        url: "/Semester/List",

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
                    html += "<td>" + item.SessionName + "</td>";
                    html += "<td>" + item.SemesterNo + "</td>";
                    html += "<td>" + item.SemesterStartDay + "-" + item.SemesterStartMonth + "-" + item.SemesterStartYear + "</td>";
                    html += "<td>" + item.SemesterEndDay + "-" + item.SemesterEndMonth + "-" + item.SemesterEndYear + "</td>";

                    html += "<td>" + item.Active + "</td>";
                    html += "<td>" +
                        '<button  rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit" type="button" onclick="update(' + "'" + item.Id + "'" + "," + ("'" + item.SemesterStartDay + "-" + item.SemesterStartMonth + "-" + item.SemesterStartYear + "'") + "," + ("'" + item.SemesterEndDay + "-" + item.SemesterEndMonth + "-" + item.SemesterEndYear + "'") + ')"></button>' +
                        "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";
                    html += "</tr>";
                });

                $("tbody").html(html);
            }
        }

    });


}


function referesh() {


    list();
}


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

    var valid = validate();

    if (valid == false) {
        alert("Please fill the text boxes");
        return;

    }

    var semesterStartDate = $("#startDate").val().split("-");
    var semesterEndDate = $("#endDate").val().split("-");

    var sessionid = $("#sessionSelectOptions").val();




    var SemesterObject = {

        SessionId: sessionid,
        SemesterNo: $("#semesterNoSelect").val(),
        SemesterStartDay: semesterStartDate[0],
        SemesterStartMonth: semesterStartDate[1],
        SemesterStartYear: semesterStartDate[2],
        SemesterEndDay: semesterEndDate[0],
        SemesterEndMonth: semesterEndDate[1],
        SemesterEndYear: semesterEndDate[2],
        Active: $("#selectActive").val()

    }




    jQuery.ajax({

        url: "/Semester/Insert",

        type: "POST",

        data: JSON.stringify(SemesterObject),

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            if (result == '2') {

                $("#resposeOfActive").modal("show");               
            }
            if (result == '0') {


                $("#primaryKeyViolationModal").modal("show");
            }
            if (result == '1') {
                $("#resposeOfUpdation").modal("show");
                $("#startDate").val("");
                $("#endDate").val("");
            }
            

        }






    });

}

function validate() {
    var isValid = true;


    if ($("#sessionSelectOptions").val().trim() == "") {
        $("#selectSessionOptions").css("border", "solid 1px red");

        isValid = false;
    }
    else {
        $("#selectSessionOptions").css("border", "solid 0px lightgrey");
    }
    //----------------------//
    if ($("#semesterNoSelect").val().trim() == "") {
        $("#selectSemesterNo").css("border", "solid 1px red");

        isValid = false;
    }
    else {
        $("#selectSemesterNo").css("border", "solid 0px red");
    }


    //----------------------//
    if ($("#startDate").val().trim() == "") {
        $("#startDate").css("border-color", "Red");


        isValid = false;

    }
    else {
        $("#startDate").css("border-color", "lightgrey");

    }


    //----------------------//
    if ($("#startDate").val().trim() == "") {
        $("#startDate").css("border-color", "Red");

        isValid = false;

    }
    else {
        $("#startDate").css("border-color", "lightgrey");
    }

    //----------------------//
    if ($("#endDate").val().trim() == "") {
        $("#endDate").css("border-color", "Red");
        isValid = false;

    }
    else {
        $("#endDate").css("border-color", "lightgrey");
    }

    //----------------------//
    if ($("#selectActive").val().trim() == "") {
        $("#activeSelect").css("border", "solid 1px red");
        isValid = false;
    }
    else {
        $("#activeSelect").css("border", "solid 0px red");
    }

    return isValid;
}


var semesterDeleteID = "";
function deleteConfirm(semesterId) {


    $("#confirmDelete").modal("show");
    semesterDeleteID = semesterId;

}

function deleteSession() {

    $.ajax({
        url: "/Semester/Delete",
        data: { 'semesterId': String(semesterDeleteID) },
        type: "POST",
        dataType: "json",
        success: function (result) {


            if (result == "547") {

                alert("Cannot be Deleted because it is referenced");
            }



            sectionDeleteID = "";

            if(result == "1"){

                $("#myDeleteModal").modal("show");
            }



        },
        error: function (result) { }
    });



}


var semesterUpdateID = "";
function update(id, startDate, endDate) {


    $("#startDateUpdate").val(startDate);
    $("#endDateUpdate").val(endDate);
    $("#UpdateModal").modal("show");
    semesterUpdateID = id;



}

function updateSemester() {

    var valid = CheckTheEmptiness();
    if (valid == false) {
        alert("Please fill the text boxes");
        return;

    }

    var semesterStartDate = $("#startDateUpdate").val().split("-");
    // alert(semesterStartDate);
    var semesterEndDate = $("#endDateUpdate").val().split("-");
    /// alert(semesterEndDate);
    var sessionId = $("#sessionSelectOptions").val();
    var active = $("#selectActive").val();
    //  alert(active);
    var semesterNo = $("#semesterNoSelect").val();
    // alert(semesterNo);


    // alert(semesterUpdateID);
    var semesterUpdatedObj = {
        Id: semesterUpdateID,
        SessionId: sessionId,
        SemesterNo: semesterNo,
        SemesterStartDay: semesterStartDate[0],
        SemesterStartMonth: semesterStartDate[1],
        SemesterStartYear: semesterStartDate[2],
        SemesterEndDay: semesterEndDate[0],
        SemesterEndMonth: semesterEndDate[1],
        SemesterEndYear: semesterEndDate[2],
        Active: active


    };

    $.ajax({
        url: "/Semester/Update",
        data: JSON.stringify(semesterUpdatedObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {


            //Primary Key Violation
            if (result == "0") {

                $("#primaryKeyViolationModal").modal("show");
            }
            if (result == '2') {

                $("#resposeOfActive").modal("show");
            }

            if (result == "1") {

                $("#updateSemesterResponse").modal("show");
                $("#startDateUpdate").val("");
                $("#endDateUpdate").val("");
                $("#sessionSelectOptions").val("");
                $("#selectActive").val("");
                $("#semesterNoSelect").val("");
            }

        },
        error: function (errormessage) {


            alert("Error");
        }


    }

   );

}

function CheckTheEmptiness()
{

    var semesterStartDate = $("#startDateUpdate").val();
    if (semesterStartDate.trim() == "") {
        return false;

    }
    var semesterEndDate = $("#endDateUpdate").val();

    if (semesterEndDate.trim() == "") {
        return false;

    }





}

