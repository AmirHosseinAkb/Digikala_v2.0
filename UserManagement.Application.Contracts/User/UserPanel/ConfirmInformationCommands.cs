using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using FoolProof.Core;

namespace UserManagement.Application.Contracts.User.UserPanel
{
    public class FullNameCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string LastName { get; set; }
    }

    public class NationalNumberCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [StringLength(10,MinimumLength = 10,ErrorMessage =ValidationMessages.NationalNumberLength )]
        [Range(0,long.MaxValue,ErrorMessage = ValidationMessages.IntegerValue)]
        public string NationalNumber { get; set; }
    }

    public class EmailCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        public string Email { get; set; }
    }

    public class PhoneNumberCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(0,long.MaxValue,ErrorMessage = ValidationMessages.IntegerValue)]
        [StringLength(11,MinimumLength = 11,ErrorMessage =ValidationMessages.PhoneNumberLength )]
        public string PhoneNumber { get; set; }
    }

    public class BirthDateCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1300, 1400,ErrorMessage =ValidationMessages.BirthYearRange)]
        public int BirthYear { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, 12,ErrorMessage =ValidationMessages.BirthYearRange)]
        public int BirthMonth { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, 31,ErrorMessage =ValidationMessages.BirthYearRange)]
        public int BirthDay { get; set; }
    }

    public class RefundCommand
    {
        public byte RefundType { get; set; }
        
        [RequiredIf(nameof(RefundType),1,ErrorMessage = ValidationMessages.IsRequired)]
        public string AccountNumber { get; set; }
    }

    public class PasswordCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(50,ErrorMessage = ValidationMessages.MaxLength)]
        [MinLength(8,ErrorMessage = ValidationMessages.PasswordMinLength)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(30,ErrorMessage = ValidationMessages.MaxLength)]
        [MinLength(8,ErrorMessage = ValidationMessages.PasswordMinLength)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(50,ErrorMessage = ValidationMessages.MaxLength)]
        [MinLength(8,ErrorMessage = ValidationMessages.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword),ErrorMessage = ValidationMessages.InvalidPasswordCompare)]
        public string RepeatNewPassword { get; set; }
    }
}
