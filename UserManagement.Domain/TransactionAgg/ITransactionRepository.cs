using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.TransactionAgg
{
    public interface ITransactionRepository
    {
        List<Transaction> GetUserTransactions(long userId);
        long AddTransaction(Transaction transaction);
        Transaction GetUserTransaction(long transactionId,long userId);
        void SaveChanges();
    }
}
