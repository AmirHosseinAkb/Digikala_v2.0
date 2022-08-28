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
        long AddTransaction(TransactionCommand command);
        PaymentResponse TransactionPayment(TransactionCommand command);
        VerificationResponse TransactionVerification(long transactionId,string authority);
        void ConfirmTransacttion(long transactionId);
    }
}
