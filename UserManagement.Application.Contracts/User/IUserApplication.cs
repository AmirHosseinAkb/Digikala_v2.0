using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using UserManagement.Application.Contracts.User.Administration;
using UserManagement.Application.Contracts.User.UserPanel;

namespace UserManagement.Application.Contracts.User
{
    public interface IUserApplication
    {
        OperationResult RegisterAndLogin(RegisterAndLoginCommand command);
        OperationResult Register(RegisterCommand command);
        OperationResult Login(LoginCommand command);
        OperationResult ForgetPassword(ForgetPasswordCommand command);
        OperationResult ResetPassword(ResetPasswordCommand command);
        bool ActiveAccount(string activationCode);
        bool IsExistByEmail(string email);
        bool IsExistEmailOrPhoneNumber(string email = "", string phoneNumber = "");
        void SignOut();

        #region UserPanel

        UserInformationsViewModel GetUserInformationsForShow();
        SidebarInformationsViewModel GetUserSidebarInformationsForShow();
        string ConfirmUserFullName(FullNameCommand command);
        OperationResult ConfirmUserEmail(EmailCommand command);
        OperationResult ConfirmUserPhoneNumber(PhoneNumberCommand command);
        string ConfirmUserNationalNumber(NationalNumberCommand command);
        string ConfirmUserBirthDate(BirthDateCommand command);
        OperationResult ConfirmUserPassword(PasswordCommand command);
        OperationResult ConfirmUserRefundType(RefundCommand command);

        #endregion

        #region Administration

        Tuple<List<UserAdminInformationsViewModel>, int, int, int> GetUsersAdminInformationsForShow(int pageId = 1, string fullName = ""
            , string email = "", string phoneNumber = "", int take = 20);
        Tuple<List<UserAdminInformationsViewModel>, int, int, int> GetDeletedUsersAdminInformationsForShow(int pageId = 1, string fullName = ""
            , string email = "", string phoneNumber = "", int take = 20);

        OperationResult AddUserFromAdmin(CreateUserCommand command,long roleId);

        OperationResult EditUserFromAdmin(EditUserCommand command, long roleId);

        string GetCurrentUserRoleTitle();
        void DeleteUser(long userId);

        void ReturnUser(long userId);

        bool IsExistUserByRoleId(long roleId);

        #endregion
    }
}
