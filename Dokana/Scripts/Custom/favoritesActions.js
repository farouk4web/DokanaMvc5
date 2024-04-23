$(document).ready(function () {
    //add and remove product to and  from favrites products
    $(document).on("click", ".favoriteBtn", function () {

        var btn = $(this),
            authMsg = "Authorization has been denied for this request.",
            errorMsg = $("#errMsg").text(),
            signFirstMsg = $("#signToAddProduct").text(),
            idOfProduct = btn.attr("data-productId");


        if (btn.attr("data-action") === "add") {
            $.ajax({
                url: "/api/favorites",
                method: 'post',
                data: {
                    ProductId: idOfProduct
                },
                success: function (dto) {
                    if (dto.Message === authMsg) {
                        bootbox.alert(signFirstMsg);
                    } else {
                        btn.addClass("active").attr("data-action", "remove");
                    }
                },
                error: function (e) {
                    bootbox.dialog(errorMsg);
                    console.log(`errorr is:- ${e}`);
                }
            });
                
        } else if (btn.attr("data-action") === "remove") {
            $.ajax({
                url: "/api/favorites/" + idOfProduct,
                method: "delete",
                success: function () {
                    btn.removeClass("active").attr("data-action", "add");
                },
                error: function (e) {
                    bootbox.dialog(errorMsg);
                    console.log(`errorr is:- ${e}`);
                }
            });
        }
    });

});