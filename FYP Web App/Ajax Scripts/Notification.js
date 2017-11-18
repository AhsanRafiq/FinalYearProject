function saveNotification() {
    var isEmpty = CheckEmptiness();
    if (isEmpty == true) {

        alert("Some Fields are empty");
        return;
    }

    debugger;
    var notification = {
        sessionId: $("#sessionSelectOptions").val(),
        semesterId: $("#semesterSelectOption").val(),
        courseId: $("#subjectSelectOptions").val(),
        teacherId: $("#teacherSelectOptions").val(),
        notificationName: $("#notificationName").val(),
        notificationDescription: $("#notificationDescription").val()
    }

    jQuery.ajax({

        url: "/Notification/Insert",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(notification),
        success: function (result) {
            debugger;

            if (result == '0') {
                alert("Notification with this name already exist");
                return;
            }
            if (result == '1') {
                alert("Notification Added");

                $("#notificationName").val(""),
                $("#notificationDescription").val("")
            }
        }
    });


}
function CheckEmptiness() {
    var notificationName = $('#notificationName').val();
    if (notificationName.trim().length == 0) {
        return true;
    }
    var notificationDescription = $('#notificationDescription').val();
    if (notificationDescription.trim().length == 0) {
        return true;
    }
}
function validateAddValues() {
    $('#sessionSelectOptions').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });
    $('#semesterSelectOption').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-8]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only aplhabets and letters.'); return ''; }));
    });
    $('#subjectSelectOptions').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^A-Za-z]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only aplhabets and letters.'); return ''; }));
    });
    $('#notificationName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9A-Za-z() -]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });

}
function displayNotification() {
    jQuery.ajax({

        url: "/Notification/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { sessionId: $("#sessionSelectOptions").val(), semesterId: $("#semesterSelectOption").val(), courseId: $("#subjectSelectOptions").val() },
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
                    html += "<td>" + item.NotificationName + "</td>";
                    html += "<td>" + '<button type="button" onclick="view(' + "'" + item.NotificationName + "'" + "," + "'" + item.NotificationDescription + "'" + ')" rel="tooltip" title="View" class="fa fa-eye"></button>'
                        + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button type="button" onclick="updateNotification(' + "'" + item.Id + "'" + "," + "'" + item.NotificationName + "'" + "," + "'" + item.NotificationDescription + "'" + ')" rel="tooltip" title="View" class="fa fa-edit"></button>'
                        + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button type="button" rel="tooltip" title="Delete" class="fa fa-trash-o" onclick="deleteConfirmMaterial(' + "'" + item.Id + "'" + ')"></button>' + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);

        },
    });


}
function view(notificationName, notificationDescription) {
    $("#notificationName").text(notificationName);
    $("#notificationDescription").text(notificationDescription);
    $("#myViewModal").modal('show');
}
var notificationId = "";
function updateNotification(id, notificationName, notificationDescription) {
    notificationId = id;
    $("#updateNotificationName").val(notificationName);
    $("#updateNotificationDescription").val(notificationDescription);
    $("#myUpdateModal").modal('show');
}
function UpdateConfrim() {

    var isEmpty = CheckEmptinessUpdate();
    if (isEmpty == true) {

        alert("Some Fields are empty");
        return;
    }
    jQuery.ajax({

        url: "/Notification/Update",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { 'id': notificationId, notificationName: $("#updateNotificationName").val(), notificationDescription: $("#updateNotificationDescription").val() },
        success: function (result) {
            if (result == '0') {
                alert("Notification with this name already exist");
                return;
            }
            if (result == '1') {
                alert("Notification Updated");

                $("#updateNotificationName").val(""),
                $("#updateNotificationDescription").val("")
            }
        }
    });




}
function CheckEmptinessUpdate() {
    var updateNotificationName = $('#updateNotificationName').val();
    if (updateNotificationName.trim().length == 0) {
        return true;
    }
    var updateNotificationDescription = $('#updateNotificationDescription').val();
    if (updateNotificationDescription.trim().length == 0) {
        return true;
    }
}
function validateUpdateValues() {
    $('#updateNotificationName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9A-Za-z() -]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });

}

var notificationDeleteId = "";
function deleteConfirmMaterial(id) {

    $("#myDeleteModal").modal('show');

    notificationDeleteId = id;

}

function deleteNotification()
{
    $.ajax({
        url: "/Notification/Delete",
        data: { 'id': notificationDeleteId },
        type: "GET",
        dataType: "json",
        success: function (result) {
            notificationDeleteId = "";
            $("#myDeleteModalShow").modal("show");
        },
        error: function (result) { }
    });


}