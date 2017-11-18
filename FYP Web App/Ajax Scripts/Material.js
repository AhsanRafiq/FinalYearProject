function saveMaterial()
{
    alert("called");
    var isEmpty = CheckEmptiness();
    if (isEmpty == true) {

        alert("Some Fields are empty");
        return;
    }
    debugger;
    var material ={
        sessionId: $("#sessionSelectOptions").val(),
        semesterId: $("#semesterSelectOption").val(), 
        courseId: $("#subjectSelectOptions").val(),
        teacherId: $("#teacherSelectOptions").val(),
        materialName: $("#materialName").val(),
        materialDescription: $("#materialDescription").val()
    }
  
    jQuery.ajax({

        url: "/Material/Insert",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(material),
        success: function (result) {
            debugger;

            if (result == '0')
            {
                alert("Material with this name already exist");
                return;
            }
            if (result == '1') {
                alert("Material Added");

               $("#materialName").val(""),
               $("#materialDescription").val("")
            }
        }
    });

   
}
function validateAddValues()
{
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
    $('#materialName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9A-Za-z() -]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });

}
function CheckEmptiness()
{
    var materialName = $('#materialName').val();
    if (materialName.trim().length == 0) {
        return true;
    }
    var materialDescription = $('#materialDescription').val();
    if (materialDescription.trim().length == 0) {
        return true;
    }
}
var materialId = "";
function deleteConfirmMaterial(id) {

    $("#myDeleteModal").modal('show');

    materialId = id;

}
function deleteMaterial() {

    $.ajax({
        url: "/Material/Delete",
        data: { 'id': materialId },
        type: "GET",
        dataType: "json",
        success: function (result) {
            materialId = "";
            $("#myDeleteModalShow").modal("show");
        },
        error: function (result) { }
    });



}
function displayMaterial()
{
    jQuery.ajax({

        url: "/Material/List",
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
                    html += "<td>" + item.MaterialName + "</td>";
                    html += "<td>" + '<button type="button" onclick="view(' + "'" + item.MaterialName + "'" + "," + "'" + item.MaterialDescription + "'" + ')" rel="tooltip" title="View" class="fa fa-eye"></button>'
                        + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button type="button" onclick="updateMaterial(' + "'" + item.Id + "'" + "," + "'" + item.MaterialName + "'" + "," + "'" + item.MaterialDescription + "'" + ')" rel="tooltip" title="View" class="fa fa-edit"></button>'
                        + "<nbsp/><nbsp/><nbsp/><nbsp/>" +
                        '<button type="button" rel="tooltip" title="Delete" class="fa fa-trash-o" onclick="deleteConfirmMaterial(' + "'" + item.Id + "'" + ')"></button>' + "</td>";

                    html += "</tr>";
                });
            }

            $("tbody").html(html);

            },    
    });


}
function view(materialName,materialDescription)
{   
    $("#materialName").text(materialName);
    $("#materialDescription").text(materialDescription);
    $("#myViewModal").modal('show');
}
var materialId = "";
function updateMaterial(id,materialName, materialDescription) {

   
    materialId = id;
    $("#updateMaterialName").val(materialName);
    $("#updateMaterialDescription").val(materialDescription);
    $("#myUpdateModal").modal('show');

}
function UpdateConfrim()
{
   
    var isEmpty = CheckEmptinessUpdate();
    if (isEmpty == true) {

        alert("Some Fields are empty");
        return;
    }
    jQuery.ajax({

        url: "/Material/Update",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { 'id': materialId, materialName: $("#updateMaterialName").val(), materialDescription: $("#updateMaterialDescription").val()},
        success: function (result) {
            if (result == '0') {
                alert("Material with this name already exist");
                return;
            }
            if (result == '1') {
                alert("Material Updated");

                $("#materialName").val(""),
                $("#materialDescription").val("")
            }
        }
    });




}
function CheckEmptinessUpdate()
{
    var updateMaterialName = $('#updateMaterialName').val();
    if (updateMaterialName.trim().length == 0) {
        return true;
    }
    var updateMaterialDescription = $('#updateMaterialDescription').val();
    if (updateMaterialDescription.trim().length == 0) {
        return true;
    }
}
function validateUpdateValues() {
    $('#updateMaterialName').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9A-Za-z() -]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only numbers.'); return ''; }));
    });

}
