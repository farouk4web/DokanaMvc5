$(document).ready(function () {

    var currentStarPosition = 0;
    var idOfProduct = 0;

    $(".stars").on("click", "i", function () {
        currentStarPosition = $(this).attr("data-position");
        idOfProduct = $(this).attr("data-productId");
        var starsGroup = $(this).parents(".stars");
        starsGroup.find("i").removeClass("active");

        for (var i = 1; i < 6; i++) {
            if (i <= currentStarPosition) {
                var starItem = starsGroup.find(`i[data-position="${i}"]`);
                starItem.addClass("active");
            }
        }

        starsGroup.siblings("span.starsErrorMsg").remove();

    });

    // send ajax request to add user REVIEW 
    $("#commentForm").submit(function (e) {
        e.preventDefault();
        var comment = $("#RateForm_Comment"),
            commentContent = $("#RateForm_Comment").val(),
            authMsg = "Authorization has been denied for this request.",
            signFirstMsg = $("#signToAddProduct").text(),
            requiredStarsMsg = $("#requiredStarsMsg").text();

        if (currentStarPosition === 0) {
            $(this).find(".starsErrorMsg").remove();

            $(this).find(".stars").after(`<span class="starsErrorMsg field-validation-error">${requiredStarsMsg}</span>`);
        }

        if (currentStarPosition !== 0 && commentContent.length >= 3) {
            // send ajax request to our api

            $.ajax({
                url: "/api/Reviews/",
                method: "post",
                data: {
                    productId: idOfProduct,
                    starsCount: currentStarPosition,
                    comment: commentContent
                },
                success: function (dto) {
                    if (dto.Message === authMsg) {
                        bootbox.alert(signFirstMsg);
                    } else {
                        //append review to reviews 
                        $(".reviews").prepend(`
                        <div class="rate">
                            <div class="row">
                                <div class="col-sm-6">
                                    <a href="/users/UserAccount/${dto.UserId}">
                                        <img src="${dto.UserImageSrc}" alt="" width="50" height="50" class="img-circle img-responsive img-thumbnail" />
                                        <b>${dto.UserFullName}</b>
                                    </a>
                                </div>
                                <div class="col-sm-6 text-right">

                                    ${Array(dto.StarsCount).fill().map(() => `<i class="glyphicon glyphicon-star"></i>`).join('')}

                                    <br /><span>(${dto.PublishDate})</span>

                                </div>
                            </div>

                            <p>
                                ${dto.Comment}
                            </p>
                        </div>
                                `);


                        comment.val("");
                        currentStarPosition = 0;

                        $(".stars i").removeClass("active");
                        $(".reviews .sorryMsg").remove();
                    }
                },
                error: function (e) {
                    bootbox.alert(e.responseJSON.Message);
                }

            });
        }

    });


});