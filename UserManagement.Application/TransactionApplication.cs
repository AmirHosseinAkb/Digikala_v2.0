using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Application.ZarinPal;
using _01_Framework.Infrastructure;
using _01_Framework.Resources;
using UserManagement.Application.Contracts.Transaction;
using UserManagement.Domain.TransactionAgg;

namespace UserManagement.Application
{
    public class TransactionApplication:ITransactionApplication
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IZarinpalFactory _zarinpalFactory;

        public TransactionApplication(ITransactionRepository transactionRepository, IAuthenticationHelper authenticationHelper, IZarinpalFactory zarinpalFactory)
        {
            _transactionRepository = transactionRepository;
            _authenticationHelper = authenticationHelper;
            _zarinpalFactory = zarinpalFactory;
        }

        public List<TransactionViewModel> GetUserTransactionsForShow()
        {
            return _transactionRepository.GetUserTransactions(_authenticationHelper.GetCurrentUserId())
                .Select(t=>new TransactionViewModel()
                {
                    TypeId = t.TypeId,
                    Amount = t.Amount,
                    IsSucceeded = t.IsSucceeded,
                    CreationDate = t.CreationDate.ToShamsi(),
                    Description = t.Description,
                    IsForPayOrder = t.IsForPayOrder
                }).ToList();
        }

        public long AddTransaction(int amount)
        {
            var transaction = new Transaction(TransactionTypes.Deposit, _authenticationHelper.GetCurrentUserId(), amount,
                DataDictionaries.PaymentDescription, false);
            return _transactionRepository.AddTransaction(transaction);
        }

        public PaymentResponse TransactionPayment(int amount)
        {
            var transactionId = AddTransaction(amount);
            var paymentResponse =
                _zarinpalFactory.CreatePaymentRequest(transactionId, amount,
                    DataDictionaries.PaymentDescription);
            return paymentResponse;
        }

        public VerificationResponse TransactionVerification(long transactionId,string authority)
        {
            var transaction = _transactionRepository.GetUserTransaction(transactionId,_authenticationHelper.GetCurrentUserId());
            var verificationResponse=_zarinpalFactory.CreateVerificationRequest(transaction.Amount,authority);
            return verificationResponse;
        }

        public void ConfirmTransacttion(long transactionId)
        {
            var transaction = _transactionRepository.GetUserTransaction(transactionId,_authenticationHelper.GetCurrentUserId());
            transaction.Confirm();
            _transactionRepository.SaveChanges();
        }

        public long AddWithdrawTransaction(int amount,long orderId)
        {
            var transaction=new Transaction(TransactionTypes.Withdraw,_authenticationHelper.GetCurrentUserId(),amount,DataDictionaries.WithdrawTransactionDescription+orderId,false);
            transaction.CheckForOrder();
            _transactionRepository.AddTransaction(transaction);
            return transaction.TransactionId;
        }

        public void AddTransactionForPayOrder(int orderSum, long orderId)
        {
            var transaction=new Transaction(TransactionTypes.Withdraw,_authenticationHelper.GetCurrentUserId(),orderSum,DataDictionaries.WithdrawTransactionDescription+orderId,true);
            _transactionRepository.AddTransaction(transaction);
        }

        public TransactionViewModel GetTransactionById(long transactionId)
        {
            var transaction=_transactionRepository.GetUserTransaction(transactionId, _authenticationHelper.GetCurrentUserId());
            return new TransactionViewModel()
            {
                TypeId = transaction.TypeId,
                IsSucceeded = transaction.IsSucceeded,
                Amount = transaction.Amount,
                CreationDate = transaction.CreationDate.ToShamsi(),
                Description = transaction.Description,
            };
        }
    }
}
