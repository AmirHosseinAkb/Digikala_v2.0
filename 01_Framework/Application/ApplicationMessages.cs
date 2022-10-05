using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public  class ApplicationMessages
    {
        public const string DuplicatedEmail = "این ایمیل از قبل وجود دارد";
        public const string DuplicatedPhone = "این شماره همراه از قبل وجود دارد";
        public const string DuplicatedRole = "این نقش از قبل وجود دارد";
        public const string DuplicatedProduct = "این محصول از قبل وجود دارد";
        public const string DuplicatedGroup = "این گروه محصول از قبل وجود دارد";
        public const string DuplicatedColor = "این رنگ محصول از قبل وجود دارد";
        public const string DuplicatedGroupDetail = "این ویژگی از قبل در این گروه وجود دارد";
        public const string DuplicatedBrand = "این برند وجود دارد";
        public const string DuplicatedDiscount = "این کد تخفیف از قبل وجود دارد";
        public const string DuplicatedAddress = "این آدرس از قبل وجود دارد";
        public const string RecordNotFound = "نتیجه ای یافت نشد";
        public static string WrongUserPass = "رمز عبور وارد شده صحیح نمی باشد";
        public const string InvalidEmailOrPhoneNumber = "ایمیل یا شماره همراه را بصورت صحیح وارد کنید";
        public const string InvalidPhoneNumber = "شماره همراه را بصورت صحیح وارد کنید";
        public const string InvalidEmail = "ایمیل را بصورت صحیح وارد کنید";
        public const string InvalidAccountNumber = "شماره حساب خود را بصورت صحیح وارد کنید";
        public const string ProcessFailed = "عملیات با شکست مواجه شد لطفا دوباره امتحان نمایید";
        public const string ActivationProcessFailed = "عملیات فعال سازی با شکست مواجه شد لطفا بعدا دوباره امتحان نمایید.";
        public const string UserIsNotActive = "حساب کاربری شما فعال نمی باشد لطفا ابتدا حساب کاربری خود را فعال نمایید";
        public const string InvalidCurrentPassword = "رمز عبور فعلی خود را بصورت صحیح وارد کنید";
        public const string IdenticalEmailEntered = "ایمیلی غیر از ایمیل فعلی تان را وارد کنید";
        public const string IdenticalPhoneNumberEntered = "شماره ای غیر از شماره همراه فعلی تان را وارد کنید";
        public const string RoleNotExist = "نقش انتخاب شده وجود ندارد";
        public const string SomeUsersExistWithThisRole = "کاربرانی دارای این نقش می باشند لذا نمی توانید این نقش را حذف کنید";
        public const string CantDecreaseInventory = "موجودی محصول کمتر از مقداری است  که میخواهید کم کنید";
        public const string DateTimeFormatIsNotCorrect = "فرمت تاریخ صحیح نمی باشد";
        public const string EndDateShouldBeGreaterThanStartDate = "تاریخ پایان باید بعد از تاریخ شروع باشد";
        public const string ProductDontExistInStockForCurrentCount = "متاسفانه از محصول مورد نظر به اندازه وارد شده در انبار نداریم!";
        public const string InventoryCantBeZero = "موجودی نمی تواند صفر باشد در صورتی که می خواهید ایتم را حذف کنید از دکمه حذف استفاده کنید";
        public const string AddressIsDefault = "ابتدا ادرس پیشفرض برای خود انتخاب کنید سپس می توانید این ادرس را حذف کنید";

    }
}
