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
    if ($(e.target).is('a') == false) {
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

const cookieName = "cart_items";
function AddToCart(id, title, price, discountRate, imageName, brand) {
    var products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    }
    else {
        products = JSON.parse(products);
    }
    var colorInformations = $('input[name=productColor]:checked').attr("value").split('-');
    var colorName=colorInformations[0];
    var colorCode=colorInformations[1];
    var currentProduct = products.find(p => p.id == id && p.colorName == colorName);
    const count = 1;
    if (currentProduct != undefined) {
        products.find(p => p.id == id).count = parseInt(currentProduct.count) + parseInt(count);
    }
    else {
        const product = {
            id,
            title,
            unitPrice: price,
            discountRate,
            imageName,
            count,
            brand,
            colorName,
            colorCode
        };
        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products), { expires: 10, path: "/" });
    UpdateCart();
}

function UpdateCart() {
    let products = $.cookie(cookieName);
    if (products != undefined) {
        products = JSON.parse(products);
        $(".cartItemsCounter").text(products.length);
    }
    var cartItemsWrapper = $("#cartItemsWrapper");
    cartItemsWrapper.html('');
    var totalCartPrice = 0;
    products.forEach(p => {
        const product = `
               <div class="mini-cart-product">
                    <div class="mini-cart-product-thumbnail">
                        <a href="/Product/${p.id}"><img src="../Products/Images/${p.imageName}" alt=""></a>
                    </div>
                    <div class="mini-cart-product-detail">
                        <div class="mini-cart-product-title">${p.brand} <p style="color:${p.colorCode}">${p.colorName}</p></div>
                        <div class="mini-cart-product-title">
                            <a href="/Product/${p.id}">${p.title}</a>
                        </div>
                        <div class="mini-cart-purchase-info">
                            <div class="mini-cart-product-meta">
                                <span class="fa-num">${p.count} عدد</span>
                            </div>
                            <div class="mini-cart-product-price fa-num">

                                ${parseInt(p.unitPrice).toLocaleString()}<span class="currency">تومان</span>

                            </div>
                        </div>
                        <button onclick="RemoveItem(${p.id})" class="mini-cart-product-remove"></button>
                    </div>
                </div>`;
        cartItemsWrapper.append(product);
        totalCartPrice += parseInt(p.unitPrice.replace(',', '')) * parseInt(p.count);
    })
    var digits = totalCartPrice.toLocaleString();
    $(".totalCartPrice").text(digits);
}

function RemoveItem(id) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    var product = products.find(p => p.id == id);
    var index = products.indexOf(product);
    products.splice(index, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 10, path: "/" });
    UpdateCart();
}