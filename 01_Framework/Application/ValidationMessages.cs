using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class ValidationMessages
    {
        public const string IsRequired = "این فیلد نمی تواند خالی باشد";
        public const string MaxFileSize = "فایل حجیم تر از حد مجاز است";
        public const string InvalidFileFormat = "فرمت فایل مجاز نیست";
        public const string MaxLength = "مقدار وارد شده بیش از طول مجاز است";
        public const string MinLength = "مقدار وارد شده کمتر از حد مجاز است";
        public const string PasswordMinLength = "رمز عبور حداقل باید 8 کاراکتر باشد";
        public const string InvalidPasswordCompare = "تکرار رمز عبور صحیح نمی باشد";
        public const string InvalidEmail = "فرمت ایمیل وارد شده صحیح نمی باشد";
        public const string NationalNumberLength = "کدملی باید 10 رقم باشد";
        public const string PhoneNumberLength = "شماره تلفن باید 11 رقم باشد";
        public const string BirthYearRange = "سال تولد باید بین سال های 1300 تا 1400 باشد";
        public const string BirthMonthRange = "ماه تولد باید بین 1 تا 12 باشد";
        public const string BirthDayRange = "روز تولد باید بین روز های 1 و 31 باشد";
        public const string IntegerValue = "لطفا فقط عدد وارد کنید";
        public const string IntegerRange = "مقدار وارد شده در محدوده مجاز نمی باشد";
        public const string EnterEmailOrPhoneNumber = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید";
        public const string PasswordRequiredIfEmailNotEmpty = "در صورت وارد کردن ایمیل رمز عبور را باید وارد کنید";
    }
}
