// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function Success(inputName, modalName, res, toastMessage) {
    $(inputName).text(res);
    $(modalName).removeClass("remodal-is-opened");
    $(modalName).addClass("remodal-is-closed");
    $("div.remodal-is-opened").addClass("remodal-is-closed");
    $("div.remodal-is-opened").css("display", "none");
    $("div.remodal-is-opened").removeClass("remodal-is-opened");
    iziToast.success({
        message: toastMessage,
        rtl: true,
        position: 'bottomCenter',
        timeout: 3000
    });
}
function WarningMessage(message) {
    iziToast.warning({
        message: message,
        rtl: true,
        position: 'bottomCenter',
        timeout: 3000
    });
}
$(function () {
    $("#btnName").click(function (e) {
        var isValidFirstName = $("#FullNameCommand_FirstName").valid();
        var isValidLastName = $("#FullNameCommand_LastName").valid();
        if (isValidFirstName && isValidLastName) {
            var data = $("#frmUserFullName").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserFullName",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nameInp", "#nameModal", res, "نام و نام خانوادگی شما با موفقیت ویرایش شد");
                }
            });
        }
    });
});

$(function () {
    $("#btnNationalNumber").click(function (e) {
        var isValidNAtionalNumber = $("#NationalNumberCommand_NationalNumber").valid();
        if (isValidNAtionalNumber) {
            var data = $("#frmUserNationalNumber").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserNationalNumber",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nationalNumberInp", "#nationalnumberModal", res, "کد ملی شما با موفقیت ویرایش شد");
                }
            });
        }
    });
});
$(function () {
    $("#btnEmail").click(function (e) {
        var isValidEmail = $("#EmailCommand_Email").valid();
        if (isValidEmail) {
            var data = $("#frmUserEmail").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserEmail",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    var delay = 3000;
                    Success("#emailInp", "#emailModal", res, "ایمیل شما با موفقیت ویرایش شد");
                    setTimeout(function () { window.location.reload(); }, delay);
                },
                error: function (error) {
                    WarningMessage(error.responseText);
                }
            });
        }
    });
});
$(function () {
    $("#btnPhoneNumber").click(function (e) {
        var isValid = $("#PhoneNumberCommand_PhoneNumber").valid();
        if (isValid) {
            var data = $("#frmUserPhoneNumber").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserPhoneNumber",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    var delay = 3000;
                    Success("#phoneNumberInp", "#phoneNumberModal", res, "شماره همراه شما با موفقیت ویرایش شد");
                    setTimeout(function () { window.location.reload(); }, delay);
                },
                error: function (error) {
                    WarningMessage(error.responseText);
                }
            });
        }
    });
});
$(function () {
    $("#btnBirthDate").click(function (e) {
        var isValidYear = $("#BirthDateCommand_BirthYear").valid();
        var isValidMonth = $("#month").valid();
        var isValidDay = $("#BirthDateCommand_BirthDay").valid();
        if (isValidYear && isValidMonth && isValidDay) {
            var data = $("#frmUserBirthDate").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserBirthDate",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#birthDateInp", "#birthDateModal", res, "تاریخ تولد شما با موفقیت ویرایش شد");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
                }
            });
        }
    });
});
$(function () {
    $("#btnSaveRefundType").click(function (e) {
        var isValidAccountNumber = $("#RefundCommand_AccountNumber").valid();

        if (isValidAccountNumber) {
            var data = $("#frmUserRefundType").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserRefundType",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#refundTypeInp", "#refundTypeModal", res, "نحوه بازگشت وجه با موفقیت ثبت شد");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
                }
            });
        }
    });
});

$(function () {
    $("#btnSavePassword").click(function (e) {
        var data = $("#frmUserPassword").serialize();
        var isValidCurrentPassword = $("#PasswordCommand_CurrentPassword").valid();
        var isValidNewPassword = $("#PasswordCommand_NewPassword").valid();
        var isValidRepeatNewPassword = $("#PasswordCommand_RepeatNewPassword").valid();
        if (isValidCurrentPassword && isValidNewPassword && isValidRepeatNewPassword) {
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/ConfirmInformations/ConfirmUserPassword",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#passwordInp", "#userPasswordModal", res, "رمز عبور شما با موفقیت تغییر یافت از این پس می توانید با استفاده از این رمز عبور وارد سایت شوید");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
                }
            });
        }
    });
});

$("#checkoutPayment01").click(function () {
    $("#RefundCommand_AccountNumber").show();
});
$("#checkoutPayment02").click(function () {
    $("#RefundCommand_AccountNumber").hide();
    $("#accountNumberValidation").hide();
});
/*********************Email And Phone Number Existance *****************************/

$("#btnCreateUser").click(function (e) {
    $("#frmCreateUser").validate();
    var isValidForm = $("#frmCreateUser").valid();
    var isValidEmail = false;

    if (isValidForm) {
        e.preventDefault();
        if ($("#CreateUserCommand_Email").val() != "") {
            $.ajax({
                type: "Get",
                url: "/Administration/Users/IsExistEmailOrPhoneNumber?email=" + $("#CreateUserCommand_Email").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این ایمیل از قبل وجود دارد", "error");
                }
                else {
                    if ($("#CreateUserCommand_PhoneNumber").val() == "") {
                        $("#frmCreateUser").submit();
                    }
                    else {
                        isValidEmail = true;
                    }
                }
            });
        }
        if ($("#CreateUserCommand_PhoneNumber").val() != "") {
            $.ajax({
                type: "Get",
                url: "/Administration/Users/IsExistEmailOrPhoneNumber?phoneNumber=" + $("#CreateUserCommand_PhoneNumber").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این شماره تلفن از قبل وجود دارد", "error");
                }
                else {
                    if ($("#CreateUserCommand_Email").val() != "") {
                        if (isValidEmail) {
                            $("#frmCreateUser").submit();
                        }
                    }
                    else {
                        $("#frmCreateUser").submit();
                    }

                }
            });
        }
    }
});
/***********************************Edit User Informations From Admin************************************/
function GetUserInformationsForEdit(firstName, lastName, email, phoneNumber, avatarName, userId, roleId) {

    localStorage.setItem("UserEmail", email);
    localStorage.setItem("PhoneNumber", phoneNumber);
    $("#userIdInp").val(userId);

    var inputs = $(".editRoleRadio");

    inputs.each(function (i) {
        this.checked = false;
    });
    inputs[roleId - 1].checked = true;

    $("#EditUserCommand_FirstName").val(firstName);
    $("#EditUserCommand_LastName").val(lastName);
    $("#EditUserCommand_Email").val(email);
    $("#EditUserCommand_PhoneNumber").val(phoneNumber);
    $("#editImgAvatar").attr("src", "/UserAvatar/" + avatarName);
    $("#modalEditUser").modal('show');
}
/*****************************Edit User PhoneNumber and Email Existence******************************/

$("#btnEditUser").click(function (e) {
    $("#frmEditUser").validate();
    var isValidForm = $("#frmEditUser").valid();
    var isValidEmailInEdit = false;

    if (isValidForm) {
        e.preventDefault();
        if ($("#EditUserCommand_Email").val() !== "" && $("#EditUserCommand_Email").val() !== localStorage.getItem("UserEmail")) {
            $.ajax({
                type: "Get",
                url: "/Administration/Users/IsExistEmailOrPhoneNumber?email=" + $("#EditUserCommand_Email").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این ایمیل از قبل وجود دارد", "error");
                }
                else if ($("#EditUserCommand_PhoneNumber").val() == "") {
                    $("#frmEditUser").submit();
                }
                else if (!($("#EditUserCommand_PhoneNumber").val() != "" && $("#EditUserCommand_PhoneNumber").val() != localStorage.getItem("PhoneNumber"))) {
                    $("#frmEditUser").submit();
                }
                else {
                    isValidEmailInEdit = true;
                }
            });
        }

        if ($("#EditUserCommand_PhoneNumber").val() != "" && $("#EditUserCommand_PhoneNumber").val() != localStorage.getItem("PhoneNumber")) {
            $.ajax({
                type: "Get",
                url: "/Administration/Users/IsExistEmailOrPhoneNumber?phoneNumber=" +
                    $("#EditUserCommand_PhoneNumber").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این شماره تلفن از قبل وجود دارد", "error");
                } else if ($("#EditUserCommand_Email").val() === "" ||
                    $("#EditUserCommand_Email").val() == localStorage.getItem("UserEmail")) {
                    $("#frmEditUser").submit();
                } else if (isValidEmailInEdit) {
                    $("#frmEditUser").submit();
                }
            });
        }
        else if ($("#EditUserCommand_Email").val() == "" || $("#EditUserCommand_Email").val() == localStorage.getItem("UserEmail")) {
            $("#frmEditUser").submit();
        }
        else if (isValidEmailInEdit) {
            $("#frmEditUser").submit();
        }
    }
});
/****************************************************************************/

