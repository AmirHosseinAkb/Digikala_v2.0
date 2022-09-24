using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.Address
{
    public class EditAddressCommand:CreateAddressCommand
    {
        public long AddressId { get; set; }
    }
}
