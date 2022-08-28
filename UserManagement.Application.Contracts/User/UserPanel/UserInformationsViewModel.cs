using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.User.UserPanel
{
    public class UserInformationsViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? NationalNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BirthDate { get; set; }
        public byte? RefundType { get; set; }
        public string? AccountNumber { get; set; }
    }
}
