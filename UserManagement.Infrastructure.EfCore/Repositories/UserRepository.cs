using System.Reflection;
using System.Transactions;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.AddressAgg;
using UserManagement.Domain.RoleAgg;
using UserManagement.Domain.UserAgg;

namespace UserManagement.Infrastructure.EfCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AccountContext _context;

        public UserRepository(AccountContext context)
        {
            _context = context;
        }
        public bool IsExistByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistByPhoneNumber(string phoneNumber)
        {
            return _context.Users.Any(u => u.PhoneNumber == phoneNumber.Replace(" ", ""));
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByActivationCode(string activationCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActivationCode == activationCode);
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(long id)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == id);
        }

        public User GetUserWithRole(long userId)
        {
            return _context.Users.Include(u => u.Role).SingleOrDefault(u => u.UserId == userId);
        }

        public User GetDeletedUser(long userId)
        {
            return _context.Users.IgnoreQueryFilters().SingleOrDefault(u => u.IsDeleted && u.UserId == userId);
        }

        public long GetUserWalletBalance(long userId)
        {
            var deposit = _context.Transactions.Where(t => t.UserId == userId && t.TypeId == TransactionTypes.Deposit && t.IsSucceeded).Sum(t => t.Amount);
            var withdraw = _context.Transactions.Where(t => t.UserId == userId && t.TypeId == TransactionTypes.Withdraw && t.IsSucceeded).Sum(t => t.Amount);
            return deposit - withdraw;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public List<User> GetUsers(string fullName = "", string email = "", string phoneNumber = "")
        {
            IQueryable<User> result = _context.Users.Include(u=>u.Role);

            if (!string.IsNullOrEmpty(fullName))
                result = result.Where(u => u.FirstName.Contains(fullName) || u.LastName.Contains(fullName));

            if (!string.IsNullOrEmpty(email))
                result = result.Where(u => u.Email == email);

            if (!string.IsNullOrEmpty(phoneNumber))
                result=result.Where(u => u.PhoneNumber == phoneNumber);

            return result.ToList();
        }

        public List<User> GetDeletedUsers(string fullName = "", string email = "", string phoneNumber = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Include(u=>u.Role).Where(u=>u.IsDeleted);

            if (!string.IsNullOrEmpty(fullName))
                result = result.Where(u => u.FirstName.Contains(fullName) || u.LastName.Contains(fullName));

            if (!string.IsNullOrEmpty(email))
                result = result.Where(u => u.Email == email);

            if (!string.IsNullOrEmpty(phoneNumber))
                result=result.Where(u => u.PhoneNumber == phoneNumber);

            return result.ToList();
        }

        public bool IsExistUserByRole(long roleId)
        {
            return _context.Users.Any(u => u.RoleId == roleId);
        }

        public User GetUserByRoleId(long roleId)
        {
            return _context.Users.SingleOrDefault(u => u.RoleId == roleId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
