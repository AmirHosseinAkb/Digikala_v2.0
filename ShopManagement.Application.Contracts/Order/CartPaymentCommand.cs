using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartPaymentCommand
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public byte PaymentType { get; set; }
    }
}
