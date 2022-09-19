using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<Address> GetAll()
        {
            return _context.Addresses.Include(a=>a.User).ToList();
        }
    }
}
