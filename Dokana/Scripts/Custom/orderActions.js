$(document).ready(function () {
    let warningMsg = $("#warningMsg").text();

    let ConfirmOrderSuccessMsg = $("#isConfirmed").text();
    let shippingOrderSuccessMsg = $("#isShipping").text();
    let deliveredOrderSuccessMsg = $("#isDelivered").text();

    // canceling Order
    $("#js_remove_order").on("click", function () {

        bootbox.confirm(warningMsg, function (result) {
            if (result) {
                $("#cancel_order_form").submit();
            }
        });

    });


    // order levels 
    $("#js_confirm_Order").on("click", function () {
        let badge = $(this);
        let orderId = badge.data("order_id");

        bootbox.confirm(warningMsg, function (result) {
            if (result) {
                $.ajax({
                    url: `/api/orders/ConfirmOrder/${orderId}`,
                    method: "post",
                    success: function () {
                        badge.text(ConfirmOrderSuccessMsg);
                        badge.addClass("active");
                    },
                    error: function (error) {
                        console.log("status error is: " + error.status);
                    }
                });
            }
        });

    });

    $("#js_shipping_Order").on("click", function () {
        let badge = $(this);
        let orderId = badge.data("order_id");

        bootbox.confirm(warningMsg, function (result) {
            if (result) {
                $.ajax({
                    url: `/api/orders/ShippingOrder/${orderId}`,
                    method: "post",
                    success: function () {
                        badge.text(shippingOrderSuccessMsg);
                        badge.addClass("active");
                    },
                    error: function (error) {
                        console.log("status error is: " + error.status);
                    }
                });
            }
        });

    });

    $("#js_delivered_Order").on("click", function () {
        let badge = $(this);
        let orderId = badge.data("order_id");

        bootbox.confirm(warningMsg, function (result) {
            if (result) {
                $.ajax({
                    url: `/api/orders/DeliveredOrder/${orderId}`,
                    method: "post",
                    success: function () {
                        badge.text(deliveredOrderSuccessMsg);
                        badge.addClass("active");
                    },
                    error: function (error) {
                        console.log("status error is: " + error.status);
                    }
                });
            }
        });

    });
});