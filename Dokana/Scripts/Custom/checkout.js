$(document).ready(function () {
    $(".method").on("click", "input[type='radio']", function () {

        var idOfMethod = parseInt($(this).attr("id"));

        if (idOfMethod === 1) {
            console.log("cach on Delivary");

            //$(this).parents(".siteForm")
            //    .find("input")
            //    .removeClass("input-validation-error")
            //    .addClass("valid");
        }

        //$(this).parents(".method").find("input").addClass("input-validation-error");
    });
});