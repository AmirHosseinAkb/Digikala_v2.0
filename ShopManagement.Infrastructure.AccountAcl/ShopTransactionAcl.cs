using System.Diagnostics.CodeAnalysis;
using _01_Framework.Infrastructure;
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
        public long AddTransaction(int amount,long orderId)
        {
            return _transactionApplication.AddWithdrawTransaction(amount,orderId);
        }

        public void confirmTransaction(long transactionId)
        {
            _transactionApplication.ConfirmTransacttion(transactionId);
        }

        public long GetUserWalletBallance()
        {
            var userTransactions = _transactionApplication.GetUserTransactionsForShow();
            var deposits= userTransactions.Where(t=> t.TypeId == TransactionTypes.Deposit && t.IsSucceeded && !t.IsForPayOrder)
                .Sum(t =>t.Amount );
            var withdraws=userTransactions.Where(t=> t.TypeId == TransactionTypes.Withdraw && t.IsSucceeded && !t.IsForPayOrder)
                .Sum(t =>t.Amount );
            return deposits - withdraws;
        }

        public void AddtransactionForPayOrder(int amount, long orderId)
        {
            _transactionApplication.AddTransactionForPayOrder(amount, orderId);
        }
    }
}
