using System.Reflection.Metadata.Ecma335;
using _01_Framework.Application;
using UserManagement.Application.Contracts.Role;
using UserManagement.Domain.RoleAgg;
using UserManagement.Domain.UserAgg;

namespace UserManagement.Application
{
    public class RoleApplication:IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleApplication(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        public List<RoleViewModel> GetRoles()
        {
            return _roleRepository.GetRoles().Select(r => new RoleViewModel()
            {
                RoleId = r.RoleId,
                RoleTitle = r.RoleTitle,
            }).ToList();
        }

        public OperationResult Create(CreateRoleCommand command)
        {
            var result = new OperationResult();
            if (_roleRepository.IsExistRoleByTitle(command.RoleTitle))
                return result.Failed(ApplicationMessages.DuplicatedRole);

            var permissions = new List<Permission>();
            foreach (int permissionCode in command.PermissionCodes)
            {
                permissions.Add(new Permission(permissionCode));
            }
            var role = new Role(command.RoleTitle,permissions);
            _roleRepository.Add(role);
            return result.Succeeded();
        }

        public EditRoleCommand GetRoleForEdit(long roleId)
        {
            var role = _roleRepository.GetRoleById(roleId);
            return new EditRoleCommand()
            {
                RoleId = role.RoleId,
                RoleTitle = role.RoleTitle,
                PermissionCodes = role.Permissions.Select(p => p.PermissionCode).ToList()
            };
        }

        public OperationResult Edit(EditRoleCommand Command)
        {
            var result = new OperationResult();
            var role = _roleRepository.GetRoleById(Command.RoleId);
            if (role.RoleTitle != Command.RoleTitle)
            {
                if (_roleRepository.IsExistRoleByTitle(Command.RoleTitle))
                    return result.Failed(ApplicationMessages.DuplicatedRole);
            }

            var permissions = new List<Permission>();
            if (Command.PermissionCodes != null)
            {
                foreach (var code in Command.PermissionCodes)
                {
                    permissions.Add(new Permission(code));
                }
            }
            
            role.Edit(Command.RoleTitle,permissions);
            _roleRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Delete(long roleId)
        {
            var result = new OperationResult();

            if ( roleId==0 ||_roleRepository.GetRoleById(roleId) == null)
                return result.NullResult(ApplicationMessages.RoleNotExist);

            if (_userRepository.IsExistUserByRole(roleId))
                return result.Failed(ApplicationMessages.SomeUsersExistWithThisRole);

            var role = _roleRepository.GetRoleById(roleId);
            _roleRepository.DeleteRolePermissions(role);
            role.Delete();
            _roleRepository.SaveChanges();
            return result.Succeeded();
        }
    }
}
