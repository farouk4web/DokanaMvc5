$(document).ready(function () {

    $(".productForm .numbersInput input").attr("type", "number");


    // add error msg if user submit product form without photo
    $(".productForm").on("submit", "form", function (e) {
        if ($("#productImage").val() === "" && $("#Product_Id").val() === "0") {
            e.preventDefault();
            $(".clientSideAlert").show();
        }
    });

    // after change product image input and choose photo hide the alert
    $(".productForm #productImage").change(function () {
        $(this).parents("form").find(".clientSideAlert").hide();
    });

});
