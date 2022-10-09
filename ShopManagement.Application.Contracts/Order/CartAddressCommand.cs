using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartAddressCommand
    {
        [Required]
        [Range(0,long.MaxValue,ErrorMessage = ValidationMessages.IntegerValue)]
        public long AddressId { get; set; }
    }
}
