function register() {

    $('#employeeId').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#firstName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });





    $('#lastName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });




    $('#designation').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9 ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });




    $('#education').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z-()]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#contactNumber').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });





    $('#postalAddress').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9 ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });


    $('#password').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9A-Za-z]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });





    $('#employeeIdUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#firstNameUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });





    $('#lastNameUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });




    $('#designationUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9 ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });




    $('#educationUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z-()]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
    });


    $('#contactNumberUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9-]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });

    $('#postalAddressUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^a-zA-Z0-9 ]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters.'); return ''; }));
    });


    $('#passwordUpdate').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^1-9A-Za-z]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });
}











function insert() {


    var isEmpty = CheckEmptinessInsert()
    if (isEmpty)
    {
        alert("Some Fields are empty");
        return;
    }



    var isValid = isValidEmailAddress($("#email").val());
    if (!isValid)
    {
        alert("Not Valid");
        return;
    }


    var teacherObject = {
        EmployeeId: $("#employeeId").val(),
        FirstName: $("#firstName").val(),
        LastName: $("#lastName").val(),
        Designation: $("#designation").val(),
        Education: $("#education").val(),
        ContactNumber: $("#contactNumber").val(),
        PostalAddress: $("#postalAddress").val(),
        Email: $("#email").val(),
        Password: $("#password").val()
    }




    jQuery.ajax({

        url: "/Teacher/Insert",

        type: "POST",

        data: JSON.stringify(teacherObject),

        contentType: "application/json;charset=utf-8",

        dataType: "json",

        success: function (result) {

            if (result == '1') {
                $("#employeeId").val("");
                $("#firstName").val("");
                $("#lastName").val("");
                $("#designation").val("");
                $("#education").val("");
                $("#contactNumber").val("");
                $("#postalAddress").val("");
                $("#email").val("");
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

        url: "/Teacher/List",
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
                    html += "<td>" + item.EmployeeId + "</td>";
                    html += "<td>" + item.FirstName + "</td>";
                    html += "<td>" + item.LastName + "</td>";
                    html += "<td>" + item.Designation + "</td>";
                    html += "<td>" + item.Education+ "</td>";
                    html += "<td>" + item.ContactNumber + "</td>";
                    html += "<td>" + item.PostalAddress + "</td>";
                    html += "<td>" + item.Email + "</td>";
                    html += "<td>" + '<button type="button" onclick="update(' + item.Id + "," + "'" + item.EmployeeId + "'" + "," + "'" + item.FirstName + "'" + "," + "'" + item.LastName + "'" + "," + "'" + item.Designation + "'" + "," + "'" + item.Education + "'" + "," + "'" + item.ContactNumber + "'" + "," + "'" + item.PostalAddress + "'" + "," + "'" + item.Email + "'" + "," + "'" + item.Password + "'" + ')" rel="tooltip" title="Edit" class="btn btn-primary fa fa-edit"></button>'
                    + "<nbsp/><nbsp/><nbsp/><nbsp/>"+
                    '<button rel="tooltip" title="Delete" class="btn btn-danger fa fa-trash-o" aria-hidden="true" onclick=deleteConfirm(' + "'" + item.Id + "'" + ")></button>" + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);


        }

    });

}

var teacherUpdateId = "";
function update(id , employeeId ,  firstName ,lastName , designation , education , contactNumber , postalAddress , email,password ) {

    teacherUpdateId = id;
    $("#employeeIdUpdate").val(employeeId);
    $("#firstNameUpdate").val(firstName);
    $("#lastNameUpdate").val(lastName);

    $("#designationUpdate").val(designation);
    $("#educationUpdate").val(education);
    $("#contactNumberUpdate").val(contactNumber);
    $("#postalAddressUpdate").val(postalAddress);
    $("#emailUpdate").val(email);
    $("#passwordUpdate").val(password);
    $("#teacherImage").attr("src", "../../TeacherImages/" + employeeId + ".jpg");
    $("#UpdateModal").modal("show");
}

function updateTeacher() {

    var isEmpty = CheckEmptinessUpdate();
    if (isEmpty)
    {
        alert("Some Fields are empty");
        return;
    }


  
    var teacherObject = {
             Id:teacherUpdateId,
            EmployeeId: $("#employeeIdUpdate").val(),
            FirstName: $("#firstNameUpdate").val(),
            LastName: $("#lastNameUpdate").val(),
            Designation: $("#designationUpdate").val(),
            Education: $("#educationUpdate").val(),
            ContactNumber: $("#contactNumberUpdate").val(),
            PostalAddress: $("#postalAddressUpdate").val(),
            Email: $("#emailUpdate").val(),
            Password: $("#passwordUpdate").val()
        

    };



    $.ajax({
        url: "/Teacher/Update",
        data: JSON.stringify(teacherObject),
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


var teacherDeleteId = "";
function deleteConfirm(teacherId) {


    $("#confirmDelete").modal("show");
    teacherDeleteId = teacherId;




}

function deleteTeacher() {




    $.ajax({
        url: "/Teacher/Delete",
        data: { 'teacherId': String(teacherDeleteId) },
        type: "POST",
        dataType: "json",
        success: function (result) {
            

            if (result == '0') {


                alert("Cannot be delete it is referenced");
                return;
            }



            teacherDeleteId = "";



            $("#myDeleteModal").modal("show");




        },
        error: function (result)
        {


          
        }
    });



}


function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
}

function CheckEmptinessInsert() {

    var employeeId = $("#employeeId").val();
    var  firstName= $("#firstName").val();
    var  lastName= $("#lastName").val();
    var  designation= $("#designation").val();
    var  education= $("#education").val();
    var  contactNumber= $("#contactNumber").val();
    var  postalAddress= $("#postalAddress").val();
    var  email = $("#email").val();
    var password = $("#password").val();


    if (jQuery.trim(employeeId).length == 0) {
        return true;
    }

  

    if (jQuery.trim(firstName).length == 0) {
        return true;
    }




    if (jQuery.trim(lastName).length == 0) {
        return true;
    }



    if (jQuery.trim(designation).length == 0) {
        return true;
    }




    if (jQuery.trim(employeeId).length == 0) {
        return true;
    }



    if (jQuery.trim(education).length == 0) {
        return true;
    }




    if (jQuery.trim(contactNumber).length == 0) {
        return true;
    }



    if (jQuery.trim(email).length == 0) {
        return true;
    }

    if (jQuery.trim(password).length == 0) {
        return true;
    }
}

function CheckEmptinessUpdate() {

    var employeeId = $("#employeeIdUpdate").val();
    var firstName = $("#firstNameUpdate").val();
    var lastName = $("#lastNameUpdate").val();
    var designation = $("#designationUpdate").val();
    var education = $("#educationUpdate").val();
    var contactNumber = $("#contactNumberUpdate").val();
    var postalAddress = $("#postalAddressUpdate").val();
    var email = $("#emailUpdate").val();
    var password = $("#passwordUpdate").val();


    if (jQuery.trim(employeeId).length == 0) {
        return true;
    }



    if (jQuery.trim(firstName).length == 0) {
        return true;
    }




    if (jQuery.trim(lastName).length == 0) {
        return true;
    }



    if (jQuery.trim(designation).length == 0) {
        return true;
    }




    if (jQuery.trim(employeeId).length == 0) {
        return true;
    }



    if (jQuery.trim(education).length == 0) {
        return true;
    }




    if (jQuery.trim(contactNumber).length == 0) {
        return true;
    }



    if (jQuery.trim(email).length == 0) {
        return true;
    }

    if (jQuery.trim(password).length == 0) {
        return true;
    }
}