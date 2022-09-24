using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.AddressAgg;

namespace UserManagement.Infrastructure.EfCore.Repositories
{
    public class AddressRepository:IAddressRepository
    {
        private readonly AccountContext _context;

        public AddressRepository(AccountContext context)
        {
            _context = context;
        }

        public bool IsExistAddress(long userId,string postCode)
        {
            return _context.Addresses.Any(a =>a.UserId==userId && a.PostCode == postCode);
        }

        public void Add(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public List<Address> GetUserAddresses(long userId)
        {
            return _context.Addresses.Include(a=>a.User).Where(a => a.UserId == userId).ToList();
        }

        public Address? GetUserAddress(long addressId, long userId)
        {
            return _context.Addresses.SingleOrDefault(a => a.AddressId == addressId && a.UserId == userId);
        }

        public Address GetUserDefaultAddress(long userId)
        {
            return _context.Addresses.Single(a => a.IsDefault);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
