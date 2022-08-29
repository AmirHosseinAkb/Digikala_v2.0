$(".largeModal").click(function () {
    $("#ModalDialog").css("width", "80%");
});
$(".smallModal").click(function () {
    $("#ModalDialog").css("width", "40%");
});

function GetRoleForDelete(roleId) {
    $.ajax({
        url: '/Administration/Roles/Delete?roleId=' + roleId,
        type: "Get",
        success: function () {
            $("#ModalContent").load('/Administration/Roles/Delete?roleId=' + roleId);
            $("#MainModal").modal('show');
        },
        error: function (error) {
            sweetAlert("پیغام", error.responseText, "error");
        }
    })
}

$("#btnCreateProduct").click(function () {
    $("#ModalContent").load('/Administration/Shop/Products/Create');
    $("#MainModal").modal('show');
});
function CreateGroup(parentId = null) {
    $("#ModalContent").load('/Administration/Shop/ProductGroups/Create?parentId=' + parentId);
    $("#MainModal").modal('show');
}

function EditGroup(groupId) {
    $("#ModalContent").load('/Administration/Shop/ProductGroups/Edit?groupId=' + groupId);
    $("#MainModal").modal('show');
}

function GetProductForEdit(productId) {
    $("#ModalContent").load('/Administration/Shop/Products/Edit?productId=' + productId);
    $("#MainModal").modal('show');
}


function GetProductForDelete(productId, productTitle) {
    $("#ModalContent").load('/Administration/Shop/Products/Delete?productId=' + productId);
    $("#MainModal").modal('show');
    $("#productTitle").text(productTitle);
}

function GetGroupForDelete(groupId) {
    $.ajax({
        url: '/Administration/Shop/ProductGroups/Delete?groupId=' + groupId,
        method: "Get",
        success: function (response) {
            $("#ModalContent").load('/Administration/Shop/ProductGroups/Delete?groupId=' + groupId);
            $("#MainModal").modal('show');
        },
        error: function (error) {
            sweetAlert("پیغام", error.responseText, "error");
        }
    });
}

function AddProductColor(productId) {
    $("#ModalContent").load('/Administration/Shop/ProductColors/Create?productId=' + productId);
    $("#MainModal").modal('show');
}

function ShowProductImages(productId) {
    $("#ModalContent").load('/Administration/Shop/ProductImages?productId=' + productId);
    $("#MainModal").modal('show');
}

function CreateImage(productId) {
    $("#ModalContent").load('/Administration/Shop/ProductImages/Create?productId=' + productId);
    $("#MainModal").modal('show');
}
function ChangeInventory(inventoryId,isForDecrease) {
    $("#ModalContent").load('/Administration/Inventories/ChangeInventory?inventoryId=' + inventoryId+"&isForDecrease="+isForDecrease);
    $("#MainModal").modal('show');
}

function ShowInventoryHistories(productId) {
    $("#ModalContent").load('/Administration/Inventories/ShowInventoryHistories?productId=' + productId);
    $("#MainModal").modal('show');
}
//function DeleteImage(imageId, productId) {
//    $(".btnDeleteImage").preventDefault();
//    $.ajax({
//        type: "Post",
//        url: "/Administration/Shop/ProductImages/Delete?imageId=" + imageId,
//        beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
//        success: function (data) {
//            if (data.isSucceeded) {
//                $("#MainModal").modal('toggle');
//                $("#ModalContent").load('/Administration/Shop/ProductImages?productId=' + productId);
//            }
//            else {
//                sweetAlert("پیغام", data.message, "error");
//            }
//        },
//        error: function (error) {
//            sweetAlert("پیغام", error.responseText, "error");
//        }
//    });
//}

function CreateGroupDetails(groupId) {
    $("#ModalContent").load('/Administration/Shop/ProductGroups/GroupDetails?groupId=' + groupId);
    $("#MainModal").modal('show');
}

var IsForEditDetail = false;
$(document).on("submit", 'form[id="frmDetail"]', function (e) {
    e.preventDefault();
    var isValidForm = $(this).valid();
    var groupId = $("#detailGroupId").val();
    if (isValidForm) {
        var data = $("#frmDetail").serialize();
        if (IsForEditDetail == false) {
            $.ajax({
                type: "Post",
                url: "/Administration/Shop/ProductGroups/GroupDetails/Create",
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                data: data,
                dataType: "json",
                success: function (data) {
                    if (data.isSucceeded) {
                        CreateGroupDetails(groupId);
                    }
                    else {
                        sweetAlert("پیغام", data.message, "error");
                    }
                },
                error: function (error) {
                    sweetAlert("پیغام", error.responseText, "error");
                }
            })
        }
        else {
            $.ajax({
                type: "Post",
                url: "/Administration/Shop/ProductGroups/GroupDetails/Edit",
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                data: data,
                dataType: "json",
                success: function (data) {
                    if (data.isSucceeded) {
                        CreateGroupDetails(groupId);
                    }
                    else {
                        sweetAlert("پیغام", data.message, "error");
                    }
                },
                error: function (error) {
                    sweetAlert("پیغام", error.responseText, "error");
                }
            })
        }

    }
})

function GetDetailForEdit(detailId, groupId, title) {
    $("#detailGroupId").val(groupId);
    $("#command_DetailTitle").val(title);
    $("#command_DetailId").val(detailId);
    $("#btnDetail").val("ویرایش");
    IsForEditDetail = true;
}


function GetProductForDetails(productId) {
    $("#ModalContent").load('/Administration/Shop/Products/ProductDetails?productId=' + productId);
    $("#MainModal").modal('show');
}
$(document).on("submit", 'form[data-ajax="true"]',
    function (e) {
        var form = $(this);
        var isValidForm = form.valid();
        if (isValidForm) {
            e.preventDefault();
            const method = form.attr("method").toLocaleLowerCase();
            const url = form.attr("action");
            var action = form.attr("data-action");

            if (method === "get") {
                const data = form.serializeArray();
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
                    enctype: "multipart/form-data",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                    success: function (data) {
                        CallBackHandler(data, action, form);
                    },
                    error: function (data) {
                        alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
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
                hideModal();
                const refereshUrl = form.attr("data-refereshurl");
                const refereshDiv = form.attr("data-refereshdiv");
                get(refereshUrl, refereshDiv);
            }
            break;
        case "setValue":
            {
                const element = form.data("element");
                $(`#${element}`).html(data);
            }
            break;
        default:
    }
}

$("#IndexGroup").change(function () {
    $("#IndexPrimaryGroup").empty();
    $("#IndexSecondaryGroup").empty();
    $.getJSON("/Administration/Shop/Products/GetSubGroups?id=" + $("#IndexGroup :selected").val(),
        function (data) {

            $.each(data,
                function () {
                    $("#IndexPrimaryGroup").append('<option value=' + this.value + '>' + this.text + '</option>');

                });
        });
});
$("#IndexPrimaryGroup").change(function () {
    $("#IndexSecondaryGroup").empty();
    $.getJSON("/Administration/Shop/Products/GetSubGroups?id=" + $("#IndexPrimaryGroup :selected").val(),
        function (data) {

            $.each(data,
                function () {
                    $("#IndexSecondaryGroup").append('<option value=' + this.value + '>' + this.text + '</option>');

                });
        });
});

