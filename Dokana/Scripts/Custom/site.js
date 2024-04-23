$(document).ready(function () {

    var navbarHeight = $(".navbar").outerHeight(),
        footerHeight = $("footer").outerHeight(),
        windowHeight = $(window).height(),
        heightOfMainEL = windowHeight - footerHeight;

    // set the min height of site Body
    $("main").css({
        "paddingTop": navbarHeight + 20,
        "minHeight": heightOfMainEL
    });


    // make dropdown list never desapper
    $(".dropdown-menu").on("click", "*", function (event) {
        event.stopPropagation();
    });


    // rewrite the name of product
    $(".productName").each(function () {
        var productName = $(this);
        if (productName.text().length > 20) {
            var newName = productName.text().substring(0, 20);
            productName.text(newName + "....");
        }
    });


    // set default currency of site
    $(".price").each(function () {
        var currentCurrency = $("#siteCurrencey").data("currencey");
        $(this).children("b").text(currentCurrency);
    });


    // submit form of user account photo
    $("#newAccounPhoto").change(function () {
        $(this).parents("form").submit();
    });


});