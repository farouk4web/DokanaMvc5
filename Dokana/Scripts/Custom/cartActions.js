$(document).ready(function () {

    // show
    var countOfCartItems = parseInt($("#cartItemsCount").text());
    if (countOfCartItems === 0) {
        $(".cart .noItemsMsg").show();
        $(".cartDetails .noItemsMsg").show();
    } else {
        $(".cart .noItemsMsg").hide();
        $(".cartDetails .noItemsMsg").hide();
    }

    var authMsg = "Authorization has been denied for this request.",
        signFirstMsg = $("#signToAddProduct").text(),
        currencey = $("#siteCurrencey").data("currencey");


    // add new item to shopping cart
    $(".addToCart").on("click", function () {
        var addToCartbtn = $(this),
            idOfProduct = addToCartbtn.attr("data-productId");

        $.ajax({
            url: `/api/ShoppingCart/`,
            method: "post",
            data: {
                ProductId: idOfProduct
            },
            success: function (item) {

                if (item.Message === authMsg) {
                    bootbox.alert(signFirstMsg);
                } else {
                    // get current count of items in cart and update it
                    var badge = $("#cartItemsCount"),
                        countOfItems = parseInt(badge.text()) + 1;

                    badge.text(countOfItems);


                    // add and remove classes 
                    addToCartbtn.attr("disabled", "true");
                    addToCartbtn.addClass("disabled");


                    // hide sorry msg
                    $(".cart .dropdown-menu").find("li.noItemsMsg").hide();

                    // add new item to shopping cart
                    $(".cart .dropdown-menu").find("li:first-of-type").after(`
                    <li>
                        <div class="cartItem">
                            <a href="/Products/details/${item.ProductId}" class="windowLink"></a>
                            <div class="row">
                                <div class="col-sm-3">
                                    <img src="${item.ImgSrc}" class="img-responsive" alt="" />
                                </div>

                                <div class="col-sm-6">
                                    <h4 class="productName">${item.Name}</h4>
                                    <p class="price">
                                        <b>${currencey}</b>
                                        <span>${item.Price}</span>
                                    </p>
                                </div>

                                <div class="col-sm-3">
                                    <div class="actions text-center">
                                        <i class="glyphicon glyphicon-minus cartItemBtn text-left" data-action="removeOne" data-itemId="${item.Id}"></i>
                                        <strong>${item.Amount}</strong>
                                        <i class="glyphicon glyphicon-plus cartItemBtn text-right" data-action="addOne" data-itemId="${item.Id}"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>

                    `);

                }
            },
            error: function (e) {
                console.log(`errorr is:- ${e.status}`);
                bootbox.alert(signFirstMsg);
            }
        });
    });


    // Change Amount value plus 1 OR minus 1
    $(".cartItemBtn").on("click", function () {

        var btn = $(this),
            itemId = btn.attr("data-itemId"),
            action = btn.attr("data-action"),
            unitPrice = parseInt(btn.parents(".cartItem").find(".price span").text()),
            currentAmount = parseInt(btn.siblings("strong").text()),
            currentTotal = parseInt($(".total").text()),
            newTotal = 0,
            newAmount = 0;

        if (action === "addOne") {
            newAmount = currentAmount + 1;
            newTotal = currentTotal + unitPrice;
        }
        else if (action === "removeOne") {
            newAmount = currentAmount - 1;
            newTotal = currentTotal - unitPrice;
        }


        $.ajax({
            url: `/api/ShoppingCart/${itemId}`,
            method: "put",
            data: {
                amount: newAmount
            },
            success: function (amount) {
                btn.siblings("strong").text(amount);

                // change count of items 
                var badge = $("#cartItemsCount"),
                    cartItemsCount = parseInt(badge.text());

                cartItemsCount = action === "addOne" ? cartItemsCount + 1 : cartItemsCount - 1;

                badge.text(cartItemsCount);
                $(".productsCount").text(cartItemsCount);
                $(".total").text(newTotal);

                if (amount === 0) {
                    // show sorry msg if shopping cart doesnot contain any item
                    if (cartItemsCount === 0) {
                        btn.parents(".item").siblings(".noItemsMsg").show();
                        $(".total").text(0);
                    }
                    btn.parents(".item").remove();
                }

            }
        });


    });


    // remove all items from my shopping cart
    $(".removeAllCartItems").on("click", function () {

        $.ajax({
            url: `/api/ShoppingCart/1`,
            method: "delete",
            success: function () {
                $("#cartItemsCount").text(0);
                $(".productsCount").text(0);
                $(".total").text(0);

                // remove all items in navbar and shopping cart view
                $(".cart .dropdown-menu li.item").remove();
                $(".cartDetails .item").remove();

                // show no item message in navbar and shopping cart view
                $(".cart .dropdown-menu").find("li.noItemsMsg").show();
                $(".cartDetails").find(".noItemsMsg").show();
            }
        });

    });

});