using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Resources;

namespace UserManagement.Application.Contracts.User
{
    public class RegisterAndLoginCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EmailOrPhone { get; set; }
    }
}
