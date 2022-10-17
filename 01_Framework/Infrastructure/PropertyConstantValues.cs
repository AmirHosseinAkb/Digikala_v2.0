using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Infrastructure
{
    public static class Roles
    {
        public static long Administrator = 1;
        public static long AdminAssistant = 2;
        public static long CommonUser = 3;
    }
    public static class RefundTypes
    {
        public const byte PayToAccountNumber = 1;
        public const byte PayToWallet = 2;
    }

    public static class TransactionTypes
    {
        public const int Deposit = 1;
        public const int Withdraw = 2;
    }

    public static class UserPermissions
    {
        public const int UsersList = 100;
        public const int CreateUser = 101;
        public const int EditUser = 102;
        public const int DeleteUser = 103;
        public const int DeletedUsersList = 104;
        public const int ReturnUser = 105;
        public const int SearchUser = 106;
    }

    public static class RolePermissions
    {
        public const int RolesList = 200;
        public const int CreateRole = 201;
        public const int EditRole = 202;
        public const int DeleteRole = 203;
    }

    public static class PaymentTypes
    {
        public const byte PayFromWallet = 1;
        public const byte PayFromBank = 2;
    }

    public static class OrderStatuses
    {
        public const byte NotPaid = 1;
        public const byte IsWaiting = 2;
        public const byte OrderSent = 3;
    }
}
