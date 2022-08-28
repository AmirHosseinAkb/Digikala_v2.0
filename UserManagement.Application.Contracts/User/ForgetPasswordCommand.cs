using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace UserManagement.Application.Contracts.User
{
    public class ForgetPasswordCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [EmailAddress(ErrorMessage =ValidationMessages.InvalidEmail )]
        public string Email { get; set; }
    }
}
