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
            if (data.isSucceeded == true && data.message == "") {
                var uri = window.location.toString();
                if (uri.indexOf("?") > 0) {
                    var clean_uri = uri.substring(0, uri.indexOf("?"));
                    window.history.replaceState({}, document.title, clean_uri);
                }
                window.location.reload();

            }
            else if (data.isSucceeded == true && data.message != "") {
                iziToast.success({
                    message: data.message,
                    rtl: true,
                    position: 'bottomCenter',
                    timeout: 3000
                });
                setTimeout(window.location.reload(), 3000)
            }
            else {
                iziToast.warning({
                    message: data.message,
                    rtl: true,
                    position: 'bottomCenter',
                    timeout: 3000
                });
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

$("#btnCreateAddress").click(function () {
    $("#mainModalContent").load("/UserPanel/Addresses/Create");
    $("#mainModal").modal('show');
});

function EditAddress(addressId) {
    $("#mainModalContent").load("/UserPanel/Addresses/Edit?addressId=" + addressId);
    $("#mainModal").modal('show');
}

function DeleteAddress(addressId) {
    $("#mainModalContent").load("/UserPanel/Addresses/Delete?addressId=" + addressId);
    $("#mainModal").modal('show');
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
function generateUUIDUsingMathRandom() {
    var d = new Date().getTime();//Timestamp
    var d2 = (performance && performance.now && (performance.now() * 1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if (d > 0) {//Use timestamp until depleted
            r = (d + r) % 16 | 0;
            d = Math.floor(d / 16);
        } else {//Use microseconds since page-load if supported
            r = (d2 + r) % 16 | 0;
            d2 = Math.floor(d2 / 16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

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
    var colorName = colorInformations[0];
    var colorCode = colorInformations[1];
    var currentProduct = products.find(p => p.id == id && p.colorName == colorName);
    var guid = generateUUIDUsingMathRandom();
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
            colorCode,
            guid
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
        var totalPrice = p.unitPrice * p.count;
        var currentPrice = totalPrice - (totalPrice * p.discountRate / 100)
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

                                ${parseInt(currentPrice).toLocaleString()}<span class="currency">تومان</span>

                            </div>
                        </div>
                        <button onclick="RemoveItem(${p.id})" class="mini-cart-product-remove"></button>
                    </div>
                </div>`;
        cartItemsWrapper.append(product);
        totalCartPrice += parseInt(currentPrice);
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

function ChangeItemCount(type, guid, count) {
    var count = $("input[id='" + guid + "']").val();
    var currentCount = parseInt(count);
    if (type == "minus")
        currentCount -= 1;
    else if (type == "plus")
        currentCount += 1;

    $.ajax({
        type: "get",
        url: "/Cart/ChangeItemCount?Guid=" + guid + "&count=" + currentCount,
        dataType: "json",
        success: function (data) {
            if (data.isSucceeded) {
                window.location.reload();
            }
            else {
                iziToast.warning({
                    message: data.message,
                    rtl: true,
                    position: 'topCenter',
                    timeout: 3000
                });
                $("input[class='in-num']").val(count);
            }
        },
        error: function (error) {
            iziToast.warning({
                message: "خطایی رخ داده است لطفا صفحه را نوسازی کرده و دوباره امتحان کنید",
                rtl: true,
                position: 'topCenter',
                timeout: 3000
            });
        }
    });
}

//$("#btnContinue").click(function () {
//    var addressId=$("#addressId").val();
//    window.location.href="/Shipping/ContinueToPayment?addressId="+addressId;
//})