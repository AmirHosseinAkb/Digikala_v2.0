using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.UserAgg;

namespace UserManagement.Domain.TransactionAgg
{
    public class Transaction
    {
        public long TransactionId { get; private set; }
        public long TypeId { get; private set; }
        public long UserId { get;private set; }
        public int Amount { get; private set; }
        public string Description { get; private set; }
        public DateTime CreationDate { get;private set; }
        public bool IsSucceeded { get; private set; }
        public bool IsForPayOrder { get; private set; } = false;
        public User User { get; set; }
        public TransactionType TransactionType { get; set; }

        protected Transaction()
        {
            
        }

        public Transaction(long typeId,long userId,int amount,string description,bool isSucceeded)
        {
            TypeId = typeId;
            UserId = userId;
            Amount=amount;
            Description = description;
            IsSucceeded=isSucceeded;
            CreationDate = DateTime.Now;
            IsForPayOrder = false;
        }

        public void Confirm()
        {
            IsSucceeded = true;
        }

        public void CheckForOrder()
        {
            IsForPayOrder = true;
        }
        
    }
}
