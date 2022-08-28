using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Inventory
{
    public class ChangeInventoryCommand
    {
        public long InventoryId { get; set; }
        public bool IsForDecrease { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1,int.MaxValue,ErrorMessage = ValidationMessages.IntegerValue)]
        public int Count { get; set; }
    }
}
