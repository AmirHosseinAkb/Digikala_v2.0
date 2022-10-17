using System.Diagnostics.CodeAnalysis;
using Nancy.Localization;
using ShopManagement.Domain.Services;
using UserManagement.Application.Contracts.Transaction;

namespace ShopManagement.Infrastructure.AccountAcl
{
    public class ShopTransactionAcl:IShopTransactionAcl
    {
        private readonly ITransactionApplication _transactionApplication;

        public ShopTransactionAcl(ITransactionApplication transactionApplication)
        {
            _transactionApplication = transactionApplication;
        }
        public long AddTransaction(int amount, long orderId)
        {
            return _transactionApplication.AddWithdrawTransaction(amount, orderId);
        }
    }
}
