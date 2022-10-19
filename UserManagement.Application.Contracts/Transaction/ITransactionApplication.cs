using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application.ZarinPal;

namespace UserManagement.Application.Contracts.Transaction
{
    public interface ITransactionApplication
    {
        List<TransactionViewModel> GetUserTransactionsForShow();
        long AddTransaction(int amount);
        PaymentResponse TransactionPayment(int amount);
        VerificationResponse TransactionVerification(long transactionId,string authority);
        void ConfirmTransacttion(long transactionId);
        long AddWithdrawTransaction(int amount,long orderId);

    }
}
