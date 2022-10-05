using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.Address
{
    public class AddressViewModel
    {
        public long AddressId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string PostCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
