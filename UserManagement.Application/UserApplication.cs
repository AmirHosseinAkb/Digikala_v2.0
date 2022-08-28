using System.Globalization;
using _01_Framework.Application;
using _01_Framework.Application.Email;
using _01_Framework.Infrastructure;
using _01_Framework.Resources;
using UserManagement.Application.Contracts.User;
using UserManagement.Application.Contracts.User.Administration;
using UserManagement.Application.Contracts.User.UserPanel;
using UserManagement.Domain.RoleAgg;
using UserManagement.Domain.UserAgg;

namespace UserManagement.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IViewRenderService _viewRenderService;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthenticationHelper _authenticationHelper;

        public UserApplication(IUserRepository userRepository, IViewRenderService viewRenderService, IEmailService emailService, IPasswordHasher passwordHasher, IAuthenticationHelper authenticationHelper, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _viewRenderService = viewRenderService;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
            _authenticationHelper = authenticationHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult RegisterAndLogin(RegisterAndLoginCommand command)
        {
            var result = new OperationResult();
            if (command.EmailOrPhone.IsEmail())
            {
                var user = _userRepository.GetUserByEmail(EmailConvertor.FixEmail(command.EmailOrPhone));
                if (user != null)
                {
                    if (!user.IsActive)
                        return result.Failed(ApplicationMessages.UserIsNotActive);
                }

                return result.Succeeded(command.EmailOrPhone);
            }

            else if (command.EmailOrPhone.IsPhoneNumber())
                return result.Succeeded();

            else
                return result.Failed(ApplicationMessages.InvalidEmailOrPhoneNumber);
        }

        public OperationResult Register(RegisterCommand command)
        {
            var operation = new OperationResult();
            if (_userRepository.IsExistByEmail(command.Email))
                return operation.Failed(ApplicationMessages.DuplicatedEmail);

            var activationCode = CodeGenerator.GenerateUniqName();
            var hashedPassword = _passwordHasher.HashMD5(command.Password);
            var user = new User(null, null, EmailConvertor.FixEmail(command.Email), null, hashedPassword
                , Roles.CommonUser, activationCode, CodeGenerator.GenerateRandomNumber());

            var body = _viewRenderService.RenderToStringAsync
                ("_ActivationEmailBody", new EmailViewModel() { Email = command.Email, ActivationCode = activationCode }); // Email Body
            _emailService.SendEmail(command.Email, DataDictionaries.ActiveAccount, body); //Send Email
            _userRepository.Add(user);

            return operation.Succeeded();
        }

        public OperationResult Login(LoginCommand command)
        {
            var operation = new OperationResult();
            var user = _userRepository.GetUserForLogin(EmailConvertor.FixEmail(command.Email!), _passwordHasher.HashMD5(command.Password));

            if (user == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);
            if (!user.IsActive)
                return operation.Failed(ApplicationMessages.UserIsNotActive);
            var permissions = _roleRepository.GetRoleById(user.RoleId).Permissions.Select(p => p.PermissionCode)
                .ToList();
            var authVm = new AuthenticationViewModel(user.UserId, user.RoleId, user.Email, user.PhoneNumber,user.AvatarName,permissions);
            _authenticationHelper.SignIn(authVm);
            return operation.Succeeded();
        }

        public OperationResult ForgetPassword(ForgetPasswordCommand command)
        {
            var result = new OperationResult();
            var user = _userRepository.GetUserByEmail(command.Email);
            if (user == null || !user.IsActive)
                return result.Failed(ApplicationMessages.RecordNotFound);
            var body = _viewRenderService.RenderToStringAsync("_ForgetPasswordEmailBody",
                new EmailViewModel() { Email = command.Email, ActivationCode = user.ActivationCode });
            _emailService.SendEmail(command.Email, DataDictionaries.ResetPassword, body);
            return result.Succeeded();
        }

        public OperationResult ResetPassword(ResetPasswordCommand command)
        {
            var result = new OperationResult();
            var user = _userRepository.GetByActivationCode(command.ActivationCode);
            if (user == null)
                return result.NullResult(ApplicationMessages.RecordNotFound);
            if (user.Password != _passwordHasher.HashMD5(command.CurrentPassword))
                return result.Failed(ApplicationMessages.InvalidCurrentPassword);
            var hashedPassword = _passwordHasher.HashMD5(command.NewPassword);
            user.ResetPassword(hashedPassword);
            _userRepository.SaveChanges();

            return result.Succeeded();
        }

        public bool ActiveAccount(string activationCode)
        {
            var operation = new OperationResult();
            var user = _userRepository.GetByActivationCode(activationCode);
            if (user == null || user.IsActive)
                return false;
            user.Activate();
            user.ChangeActivationCode(CodeGenerator.GenerateUniqName());
            _userRepository.SaveChanges();

            return true;
        }

        public bool IsExistByEmail(string email)
        {
            return _userRepository.IsExistByEmail(EmailConvertor.FixEmail(email));
        }

        public bool IsExistEmailOrPhoneNumber(string email = "", string phoneNumber = "")
        {
            if (!string.IsNullOrWhiteSpace(email))
                return _userRepository.IsExistByEmail(EmailConvertor.FixEmail(email));
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                return _userRepository.IsExistByPhoneNumber(phoneNumber);
            return false;
        }


        public void SignOut()
        {
            _authenticationHelper.SignOut();
        }

        public UserInformationsViewModel GetUserInformationsForShow()
        {
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            return new UserInformationsViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RefundType = user.RefundType,
                BirthDate = user.BirthDate?.ToShamsi(),
                NationalNumber = user.NationalNumber,
                AccountNumber = user.AccountNumber
            };
        }

        public SidebarInformationsViewModel GetUserSidebarInformationsForShow()
        {
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            return new SidebarInformationsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                PhoneNumber = user.PhoneNumber,
                WalletBalance = _userRepository.GetUserWalletBalance(user.UserId),
                AvatarName = user.AvatarName
            };
        }

        public string ConfirmUserFullName(FullNameCommand command)
        {
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            user.ChangeFullName(command.FirstName, command.LastName);
            _userRepository.SaveChanges();
            return user.FirstName + " " + user.LastName;
        }

        public OperationResult ConfirmUserEmail(EmailCommand command)
        {
            var result = new OperationResult();
            if (!command.Email.IsEmail())
                return result.Failed(ApplicationMessages.InvalidEmail);

            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            if (user.Email == EmailConvertor.FixEmail(command.Email))
                return result.Failed(ApplicationMessages.IdenticalEmailEntered);

            if (_userRepository.IsExistByEmail(EmailConvertor.FixEmail(command.Email)))
                return result.Failed(ApplicationMessages.DuplicatedEmail);
            user.ChangeEmail(EmailConvertor.FixEmail(command.Email));
            _userRepository.SaveChanges();
            //Login With New Email
            var permissions = _roleRepository.GetRoleById(user.RoleId).Permissions.Select(p => p.PermissionCode)
                .ToList();
            var authVm = new AuthenticationViewModel(user.UserId, user.RoleId, user.Email, user.PhoneNumber,user.AvatarName,permissions);
            _authenticationHelper.SignOut();
            _authenticationHelper.SignIn(authVm);
            return result.Succeeded();
        }

        public OperationResult ConfirmUserPhoneNumber(PhoneNumberCommand command)
        {
            var result = new OperationResult();

            if (!command.PhoneNumber.IsPhoneNumber())
                return result.Failed(ApplicationMessages.InvalidPhoneNumber);

            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());

            if (user.PhoneNumber == command.PhoneNumber)
                result.Failed(ApplicationMessages.IdenticalPhoneNumberEntered);

            if (_userRepository.IsExistByPhoneNumber(command.PhoneNumber))
                return result.Failed(ApplicationMessages.DuplicatedPhone);

            user.ChangePhoneNumber(command.PhoneNumber);
            _userRepository.SaveChanges(); var permissions = _roleRepository.GetRoleById(user.RoleId).Permissions.Select(p => p.PermissionCode)
                .ToList();
            var authVm = new AuthenticationViewModel(user.UserId, user.RoleId, user.Email, user.PhoneNumber,user.AvatarName,permissions);
            _authenticationHelper.SignOut();
            _authenticationHelper.SignIn(authVm);
            return result.Succeeded();
        }

        public string ConfirmUserNationalNumber(NationalNumberCommand command)
        {
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            user.ChangeNationalNumber(command.NationalNumber);
            _userRepository.SaveChanges();
            return command.NationalNumber;
        }

        public string ConfirmUserBirthDate(BirthDateCommand command)
        {
            var result = new OperationResult();
            var date = new DateTime(command.BirthYear, command.BirthMonth, command.BirthDay, new PersianCalendar());
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            user.ChangeBirthDate(date);
            _userRepository.SaveChanges(); ;
            return date.ToShamsi();
        }

        public OperationResult ConfirmUserPassword(PasswordCommand command)
        {
            var result = new OperationResult();
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            if (user.Password != _passwordHasher.HashMD5(command.CurrentPassword.Replace(" ", "")))
                return result.Failed(ApplicationMessages.InvalidCurrentPassword);
            user.ResetPassword(_passwordHasher.HashMD5(command.NewPassword.Replace(" ", "")));
            _userRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult ConfirmUserRefundType(RefundCommand command)
        {
            var result = new OperationResult();
            var user = _userRepository.GetUserById(_authenticationHelper.GetCurrentUserId());
            if (command.RefundType == RefundTypes.PayToAccountNumber)
            {
                if (command.AccountNumber.IsAccountNumber())
                {
                    user.ChangeRefundType(RefundTypes.PayToAccountNumber);
                    user.ChangeAccountNumber(command.AccountNumber);
                    _userRepository.SaveChanges();
                    return result.Succeeded(DataDictionaries.PayToAccountNumber + " - " + command.AccountNumber);
                }
                return result.Failed(ApplicationMessages.InvalidAccountNumber);
            }
            else if (command.RefundType == RefundTypes.PayToWallet)
            {
                user.ChangeRefundType(RefundTypes.PayToWallet);
                _userRepository.SaveChanges();
                return result.Succeeded(DataDictionaries.PayToWallet);
            }
            else
            {
                return result.Failed(ApplicationMessages.ProcessFailed);
            }

        }

        public Tuple<List<UserAdminInformationsViewModel>, int, int, int> GetUsersAdminInformationsForShow(int pageId = 1, string fullName = "", string email = "", string phoneNumber = "", int take = 20)
        {
            if (take % 20 != 0 || take < 0)
                take = 20;

            int skip = (pageId - 1) * take;
            var query = _userRepository.GetUsers(fullName, EmailConvertor.FixEmail(email), phoneNumber)
                .Skip(skip)
                .Take(take)
                .Select(u => new UserAdminInformationsViewModel()
                {
                    UserId = u.UserId,
                    RoleId = u.RoleId,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    AvatarName = u.AvatarName,
                    FullName = u.FirstName + " " + u.LastName,
                    NationalNumber = u.NationalNumber,
                    RegisterDate = u.RegisterDate.ToShamsi(),
                    RoleTitle = u.Role.RoleTitle,
                    IsActive = u.IsActive
                }).ToList();

            int pageCount = query.Count() / take;
            if (query.Count() % take != 0)
                pageCount++;
            return Tuple.Create(query, pageId, pageCount, take);
        }

        public Tuple<List<UserAdminInformationsViewModel>, int, int, int> GetDeletedUsersAdminInformationsForShow(int pageId = 1, string fullName = "", string email = "",
            string phoneNumber = "", int take = 20)
        {
            int skip = (pageId - 1) * take;
            var query = _userRepository.GetDeletedUsers(fullName, EmailConvertor.FixEmail(email), phoneNumber)
                .Skip(skip)
                .Take(take)
                .Select(u => new UserAdminInformationsViewModel()
                {
                    UserId = u.UserId,
                    RoleId = u.RoleId,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    AvatarName = u.AvatarName,
                    FullName = u.FirstName + " " + u.LastName,
                    NationalNumber = u.NationalNumber,
                    RegisterDate = u.RegisterDate.ToShamsi(),
                    RoleTitle = u.Role.RoleTitle,
                    IsActive = u.IsActive
                }).ToList();

            int pageCount = query.Count() / take;
            if (query.Count() % take != 0)
                pageCount++;
            return Tuple.Create(query, pageId, pageCount, take);
        }

        public OperationResult AddUserFromAdmin(CreateUserCommand command, long roleId)
        {
            var result = new OperationResult();
            if (command.Email.IsEmail())
            {
                if (_userRepository.IsExistByEmail(command.Email))
                    return result.Failed(ApplicationMessages.DuplicatedEmail);
            }


            if (command.PhoneNumber.IsPhoneNumber())
            {
                if (_userRepository.IsExistByPhoneNumber(command.PhoneNumber))
                    return result.Failed(ApplicationMessages.DuplicatedPhone);
            }



            if (_roleRepository.GetRoleById(roleId) == null)
                return result.Failed(ApplicationMessages.RoleNotExist);

            User user;
            string avatarName = DefaultImages.DefaultUserImage;

            if (command.UserAvatar != null)
            {
                avatarName = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.UserAvatar.FileName);

                string imagePath = ImagePathGenerator.GenerateUserImagePath(avatarName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                    command.UserAvatar.CopyTo(stream);
            }

            user = new User(command.FirstName, command.LastName, EmailConvertor.FixEmail(command.Email)
                , command.PhoneNumber, _passwordHasher.HashMD5(command.Password!)
                , roleId, CodeGenerator.GenerateUniqName(), CodeGenerator.GenerateRandomNumber(), avatarName, isActive: true);

            _userRepository.Add(user);
            return result.Succeeded();
        }

        public OperationResult EditUserFromAdmin(EditUserCommand command, long roleId)
        {
            var result = new OperationResult();
            var user = _userRepository.GetUserById(command.UserId);
            if (command.Email.IsEmail())
            {
                if (user.Email != EmailConvertor.FixEmail(command.Email!))
                {
                    if (_userRepository.IsExistByEmail(EmailConvertor.FixEmail(command.Email!)))
                        return result.Failed(ApplicationMessages.DuplicatedEmail);
                }
            }


            if (command.PhoneNumber.IsPhoneNumber())
            {
                if (user.PhoneNumber != command.PhoneNumber)
                {
                    if (_userRepository.IsExistByPhoneNumber(command.PhoneNumber))
                        return result.Failed(ApplicationMessages.InvalidPhoneNumber);
                }
            }


            if (_roleRepository.GetRoleById(roleId) == null)
                return result.Failed(ApplicationMessages.RoleNotExist);

            var avatarName = user.AvatarName;

            if (command.UserAvatar != null)
            {
                string imagePath = "";
                if (user.AvatarName != DefaultImages.DefaultUserImage)
                {
                    imagePath = ImagePathGenerator.GenerateUserImagePath(user.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                avatarName = CodeGenerator.GenerateUniqName() + Path.GetExtension(command.UserAvatar.FileName);

                imagePath = ImagePathGenerator.GenerateUserImagePath(avatarName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                    command.UserAvatar.CopyTo(stream);
            }
            user.Edit(command.FirstName, command.LastName, EmailConvertor.FixEmail(command.Email), command.PhoneNumber
                , _passwordHasher.HashMD5(command.Password!), avatarName, roleId);
            _userRepository.SaveChanges();
            return result.Succeeded();
        }

        public string GetCurrentUserRoleTitle()
        {
            return _userRepository.GetUserWithRole(_authenticationHelper.GetCurrentUserId()).Role.RoleTitle;
        }

        public void DeleteUser(long userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.Deleted();
                _userRepository.SaveChanges();
            }
        }

        public void ReturnUser(long userId)
        {
            var user = _userRepository.GetDeletedUser(userId);
            if (user != null)
            {
                user.Return();
                _userRepository.SaveChanges();
            }
        }

        public bool IsExistUserByRoleId(long roleId)
        {
            var user = _userRepository.GetUserByRoleId(roleId);
            if (user == null)
                return false;
            return true;
        }

        public UserViewModel GetUserBy(long userId)
        {
            var user= _userRepository.GetUserWithRole(userId);
            return new UserViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                RoleTitle = user.Role.RoleTitle
            };
        }
    }
}
