using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.Transaction
{
    public class TransactionViewModel
    {
        public long TypeId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool IsSucceeded { get; set; }
        public string CreationDate { get; set; }
        public bool IsForPayOrder { get; set; }

    }
}
