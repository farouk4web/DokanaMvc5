$(document).ready(function () {

    let removeCategoryWarningMsg = $("#removeCategoryWarningMsg").text();


    $("#js_remove_category").on("click", function () {
        bootbox.confirm(removeCategoryWarningMsg, function (result) {
            if (result) {
                $("#removeCategoryForm").submit();
            }
        });

    });



});