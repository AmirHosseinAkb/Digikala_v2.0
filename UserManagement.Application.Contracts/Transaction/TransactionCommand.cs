using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace UserManagement.Application.Contracts.Transaction
{
    public class TransactionCommand
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Range(5000,int.MaxValue,ErrorMessage=ValidationMessages.IntegerRange)]
        public int Amount { get; set; }
    }
}
