using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.AddressAgg
{
    public interface IAddressRepository
    {
        bool IsExistAddress(long userId,string postCode);
        void Add(Address address);
        List<Address> GetUserAddresses(long userId);
        Address? GetUserAddress(long addressId, long userId);
        Address GetUserDefaultAddress(long userId);
        Address GetAddressById(long addressId);
        void SaveChanges();
    }
}
