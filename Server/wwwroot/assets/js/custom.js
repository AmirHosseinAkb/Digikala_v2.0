////(function ($) {
////    "use strict";
////    let THEME = {};

////    /*====== Example ======*/
////    THEME.Example = function () {
////        // Write your code here
////    };
////    /*====== end Example ======*/

////    $(window).on("load", function () { });
////    $(document).ready(function () {
////        THEME.Example();
////    });
////})(jQuery);

$(document).off().on("submit", 'form[data-ajax="true"]',
    function (e) {
        var form = $(this);
        var isValidForm = form.valid();
        if (isValidForm) {
            e.preventDefault();
            var method = form.attr("method").toLocaleLowerCase();
            var url = form.attr("action");
            var action = form.attr("data-action");

            if (method === "get") {
                var data = form.serializeArray();
                $.get(url,
                    data,
                    function (data) {
                        CallBackHandler(data, action, form);
                    });
            } else {
                var formData = new FormData(this);
                $.ajax({
                    url: url,
                    type: "post",
                    data: formData,
                    async: false,
                    enctype: "multipart/form-data",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                    success: function (data) {
                        CallBackHandler(data, action, form);
                    },
                    error: function (data) {
                        alert("عملیات با شکست مواجه شد");
                    }
                });
            }
        }
        return false;
    });

function CallBackHandler(data, action, form) {
    switch (action) {
        case "Message":
            alert(data.Message);
            break;
        case "Refresh":
            if (data.isSucceeded == true) {
                window.location.reload();
            } else {
                alert(data.message);
            }
            break;
        case "RefereshList":
            {
                var refereshUrl = form.attr("data-refereshurl");
                var refereshDiv = form.attr("data-refereshdiv");
                get(refereshUrl, refereshDiv);
            }
            break;
        case "setValue":
            {
                var element = form.data("element");
                $(`#${element}`).html(data);
            }
            break;
        default:
    }
}

$(".addressLabel").click(function (e) {
    debugger;
    if ($(e.target).is('a')==false) {
        $.ajax({
            url: "/UserPanel/Addresses/SetDefaultAddress?addressId=" + $(this).attr("id"),
            type: "get",
            dataType: "json",
            success: function (data) {
                if (data.isSucceeded == false) {
                    alert(data.message);
                }
            },
            error: function (error) {
                alert(error.responseText);
            }
        })
    }
});