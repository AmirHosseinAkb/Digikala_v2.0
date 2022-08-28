using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace UserManagement.Application.Contracts.User
{
    public class ResetPasswordCommand
    {
        public string? ActivationCode { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(50,ErrorMessage = ValidationMessages.MaxLength)]
        [MinLength(8,ErrorMessage = ValidationMessages.PasswordMinLength)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(50,ErrorMessage = ValidationMessages.MaxLength)]
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
