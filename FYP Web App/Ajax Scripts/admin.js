function changePassword()
{
    var isEmpty = validateTextboxes();
    if (isEmpty) {
        alert("Some fields are empty");
        return;
    }

    $.ajax({
        url: "/Admin/ChangePassword",
        data: { 'oldPassword': $("#oldPassword").val(), 'newPassword': $("#newPassword").val() },
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result == '1')
            {
                alert("Password Changed");

                $("#oldPassword").val("");

                $("#newPassword").val("");
            }
            if (result == '0')
            {
                alert("Old Password is incorrect");
            } 
        },
        error: function (result) {
        }
    });
}

function validateTextboxes()
{
    var oldPassword = $('#oldPassword').val();
    if (oldPassword.trim().length == 0) {
        return true;
    }
    var newPassword = $('#newPassword').val();
    if (newPassword.trim().length == 0) {
        return true;
    }
}

