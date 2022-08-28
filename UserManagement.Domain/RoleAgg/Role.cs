using UserManagement.Domain.UserAgg;

namespace UserManagement.Domain.RoleAgg
{
    public class Role
    {
        public long RoleId { get; private set; }
        public string RoleTitle { get; private set; }

        public bool IsDeleted { get; private set; }

        public List<User> Users { get; private set; }
        public List<Permission> Permissions { get;private set; }

        protected Role()
        {
            
        }

        public Role(string roleTitle,List<Permission> permissions)
        {
            RoleTitle=roleTitle;
            Permissions = permissions;
            IsDeleted = false;
        }

        public Role(long roleId, string roleTitle)
        {
            RoleId = roleId;
            RoleTitle=roleTitle;
        }

        public void Edit(string roleTitle, List<Permission> permissions)
        {
            RoleTitle = roleTitle;
            Permissions = permissions;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
