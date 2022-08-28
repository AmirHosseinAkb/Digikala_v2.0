using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.RoleAgg;

namespace UserManagement.Infrastructure.EfCore.Repositories
{
    public class RoleRepository:IRoleRepository
    {
        private readonly AccountContext _context;

        public RoleRepository(AccountContext context)
        {
            _context = context;
        }
        public List<Role> GetRoles()
        {
            return _context.Roles.Include(r=>r.Permissions).ToList();
        }

        public Role GetRoleById(long roleId)
        {
            return _context.Roles.Include(r=>r.Permissions).SingleOrDefault(r=>r.RoleId==roleId);
        }

        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public bool IsExistRoleByTitle(string title)
        {
            return _context.Roles.Any(r => r.RoleTitle == title);
        }

        public void DeleteRolePermissions(Role role)
        {
            role.Permissions.ForEach(p=>_context.Permissions.Remove(p));
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
